
app.controller('HealthCheckupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'HealthCheckup';

    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.InputTypeList = [{ id: 1, text: 'Text' }, { id: 2, text: 'Number' }, { id: 3, text: 'Decimal' }];

        $scope.currentPages = {
            HealthCheckup: 1,
            TestName:1
        };

        $scope.searchData = {
            HealthCheckup: '',
            TestName: ''
        };

        $scope.perPage = {
            HealthCheckup: GlobalServices.getPerPageRow(),
            TestName: GlobalServices.getPerPageRow()
        };


        $scope.newCheckupGroup = {
            HealthCheckupId: null,
            Name: '',
            Description: '',
            ShowinStudentInfirmary: 0,
            StudentInfirmaryOrderNo: 0,
            ShowinEmployeeInfirmary: 0,
            EmployeeInfirmaryOrderNo: 0,
            Mode: 'Save'
        };

        $scope.newTestName = {
            TestNameId: null,
            Name: '',
            CheckupGroupId: null,
            OrderNo: 0,
            InputTextType: null,
            SampleCollection: '',
            SampleUnit: '',
            Description: '',
            TestNameLabDetailColl:[],
            Mode: 'Save'
        };
        $scope.newTestName.TestNameLabDetailColl.push({});

        $scope.GetAllCheckupGroupList();
        $scope.GetAllTestNameList();

    };


    $scope.ClearCheckupGroup = function () {
        $scope.newCheckupGroup = {
            HealthCheckupId: null,
            Name: '',
            Description: '',
            ShowinStudentInfirmary: 0,
            StudentInfirmaryOrderNo: 0,
            ShowinEmployeeInfirmary: 0,
            EmployeeInfirmaryOrderNo: 0,
            Mode: 'Save'
        };

    };

    $scope.ClearTestName = function () {
        $scope.newTestName = {
            TestNameId: null,
            Name: '',
            CheckupGroupId: null,
            OrderNo: 0,
            InputTextType: null,
            SampleCollection: '',
            SampleUnit: '',
            Description: '',
            TestNameLabDetailColl:[],
            Mode: 'Save'
        };
        $scope.newTestName.TestNameLabDetailColl.push({});
    };

    $scope.AddLabValueDetails = function (ind) {
        if ($scope.newTestName.TestNameLabDetailColl) {
            if ($scope.newTestName.TestNameLabDetailColl.length > ind + 1) {
                $scope.newTestName.TestNameLabDetailColl.splice(ind + 1, 0, {
                    FormerLocalLevelName: ''
                })
            } else {
                $scope.newTestName.TestNameLabDetailColl.push({
                    FormerLocalLevelName: ''
                })
            }
        }
    };

    $scope.delLabValueDetails = function (ind) {
        if ($scope.newTestName.TestNameLabDetailColl) {
            if ($scope.newTestName.TestNameLabDetailColl.length > 1) {
                $scope.newTestName.TestNameLabDetailColl.splice(ind, 1);
            }
        }
    };

    function OnClickDefault() {
        document.getElementById('groupform').style.display = "none";
        document.getElementById('Nameform').style.display = "none";

        document.getElementById('add-CheckupGroup-btn').onclick = function () {
            document.getElementById('CheckupGrouptable').style.display = "none";
            document.getElementById('groupform').style.display = "block";
        }
        document.getElementById('back-CheckupGroup-list').onclick = function () {
            document.getElementById('groupform').style.display = "none";
            document.getElementById('CheckupGrouptable').style.display = "block";
        }


        document.getElementById('add-testname-btn').onclick = function () {
            document.getElementById('Nametable').style.display = "none";
            document.getElementById('Nameform').style.display = "block";
        }
        document.getElementById('back-testname-list').onclick = function () {
            document.getElementById('Nameform').style.display = "none";
            document.getElementById('Nametable').style.display = "block";
        }
    }

    //************************* Checkup Group *********************************
    $scope.IsValidCheckupGroup = function () {
        if ($scope.newCheckupGroup.Name.isEmpty()) {
            Swal.fire('Please ! Enter Group Name');
            return false;
        }

        return true;
    };

    $scope.SaveUpdateCheckupGroup = function () {
        if ($scope.IsValidCheckupGroup() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newCheckupGroup.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateCheckupGroup();
                    }
                });
            } else
                $scope.CallSaveUpdateCheckupGroup();

        }
    };

    $scope.CallSaveUpdateCheckupGroup = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveUpdateCheckupGroup",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newCheckupGroup }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearCheckupGroup();
                $scope.GetAllCheckupGroupList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    $scope.GetAllCheckupGroupList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.CheckupGroupList = [];

        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllCheckupGroup",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.CheckupGroupList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };

    $scope.GetCheckupGroupById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            CheckupGroupId: refData.CheckupGroupId
        };

        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/GetCheckupGroupById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newCheckupGroup = res.data.Data;
                $scope.newCheckupGroup.Mode = 'Modify';
                document.getElementById('CheckupGrouptable').style.display = "none";
                document.getElementById('groupform').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelCheckupGroupById = function (refData) {
        Swal.fire({
            title: 'Are you sure you want to delete ' + refData.Name + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                var para = {
                    CheckupGroupId: refData.CheckupGroupId
                };

                $http({
                    method: 'POST',
                    url: base_url + "Infirmary/Creation/DeleteCheckupGroup",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess == true) {
                        $scope.GetAllCheckupGroupList();

                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });


    };


    //************************* Test Name *********************************
    $scope.IsValidTestName = function () {
        if ($scope.newTestName.Name.isEmpty()) {
            Swal.fire('Please ! Enter Test Name');
            return false;
        }

        return true;
    };

    $scope.SaveUpdateTestName = function () {
        if ($scope.IsValidTestName() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newTestName.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateTestName();
                    }
                });
            } else
                $scope.CallSaveUpdateTestName();

        }
    };

    $scope.CallSaveUpdateTestName = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveUpdateTestName",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newTestName }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearTestName();
                $scope.GetAllTestNameList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    $scope.GetAllTestNameList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.TestNameList = [];

        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllTestName",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.TestNameList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };

    $scope.GetTestNameById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            TestNameId: refData.TestNameId
        };

        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/GetTestNameById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newTestName = res.data.Data;

                if (!$scope.newTestName.TestNameLabDetailColl || $scope.newTestName.TestNameLabDetailColl.length == 0) {
                    $scope.newTestName.TestNameLabDetailColl = [];
                    $scope.newTestName.TestNameLabDetailColl.push({});
                }


                $scope.newTestName.Mode = 'Modify';
                document.getElementById('Nametable').style.display = "none";
                document.getElementById('Nameform').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelTestNameById = function (refData) {
        Swal.fire({
            title: 'Are you sure you want to delete ' + refData.Name + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                var para = {
                    TestNameId: refData.TestNameId
                };

                $http({
                    method: 'POST',
                    url: base_url + "Infirmary/Creation/DeleteTestName",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess == true) {
                        $scope.GetAllTestNameList();

                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });


    };


});

