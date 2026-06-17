
app.controller('DiseaseController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Health Issue/Disease';

    OnClickDefault();


    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.SeverityList = [{ id: 1, text: 'Severe' }, { id: 2, text: 'Moderate' }, { id: 3, text: 'Mild' }];
      

        $scope.DiseasesIdToNameMapping = {}

        $scope.currentPages = {
            HealthIssue: 1,
            Vaccine:1
        };

        $scope.searchData = {
            HealthIssue: '',
            Vaccine: '',
        };

        $scope.perPage = {
            HealthIssue: GlobalServices.getPerPageRow(),
            Vaccine: GlobalServices.getPerPageRow(),
        };


        $scope.newHealthIssue = {
            HealthIssueId: null,
            Name: '',
            Severity: null,
            OrderNo: 0,
            Description: '',
            Mode: 'Save'
        };

        $scope.newVaccine = {
            VaccineId: null,
            Name: '',
            CompanyName: '',
            OrderNo: 0,
            VaccineForId: null,
            Description:'',
            Mode: 'Save'
        };


        $scope.GetAllHealthIssueList();
        $scope.GetAllVaccineList();
    };


    $scope.ClearHealthIssue = function () {
        $scope.newHealthIssue = {
            HealthIssueId: null,
            Name: '',
            Severity: null,
            OrderNo: 0,
            Description: '',
            Mode: 'Save'
        };

    };
    $scope.ClearVaccine = function () {
        $scope.newVaccine = {
            VaccineId: null,
            Name: '',
            CompanyName: '',
            OrderNo: 0,
            VaccineForId: null,
            Description: '',
            Mode: 'Save'
        };
    };

    function OnClickDefault() {

        document.getElementById('HealthIssue-form').style.display = "none";
        document.getElementById('Nameform').style.display = "none";

        //Disease section
        document.getElementById('add-HealthIssue').onclick = function () {
            document.getElementById('HealthIssue-section').style.display = "none";
            document.getElementById('HealthIssue-form').style.display = "block";
            /*$scope.ClearHealthIssue();*/
        }

        document.getElementById('back-to-list-HealthIssue').onclick = function () {
            document.getElementById('HealthIssue-form').style.display = "none";
            document.getElementById('HealthIssue-section').style.display = "block";
            /*$scope.ClearHealthIssue();*/
        }

        document.getElementById('add-Vaccine-btn').onclick = function () {
            document.getElementById('Nametable').style.display = "none";
            document.getElementById('Nameform').style.display = "block";
        }

        document.getElementById('back-Vaccine-list').onclick = function () {
            document.getElementById('Nametable').style.display = "block";
            document.getElementById('Nameform').style.display = "none";
        }
    };




    //************************* HealthIssue *********************************
    $scope.IsValidHealthIssue = function () {
        if ($scope.newHealthIssue.Name.isEmpty()) {
            Swal.fire('Please ! Enter HealthIssue Name');
            return false;
        }
        return true;
    }

    $scope.SaveUpdateHealthIssue = function () {
        if ($scope.IsValidHealthIssue() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newHealthIssue.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateHealthIssue();
                    }
                });
            } else
                $scope.CallSaveUpdateHealthIssue();
        }
    };

    $scope.CallSaveUpdateHealthIssue = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveUpdateHealthIssue",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: { jsonData: $scope.newHealthIssue }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearHealthIssue();
                $scope.GetAllHealthIssueList();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }



    $scope.GetAllHealthIssueList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.HealthIssueList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllHealthIssue",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.HealthIssueList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GetHealthIssueById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            HealthIssueId: refData.HealthIssueId
        };
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getHealthIssueById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newHealthIssue = res.data.Data;
                $scope.newHealthIssue.Mode = 'Save';
                document.getElementById('HealthIssue-section').style.display = "none";
                document.getElementById('HealthIssue-form').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelHealthIssueById = function (refData, ind) {
        Swal.fire({
            title: 'Are you sure you want to delete ' + refData.Name + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            //message: 'Are you sure to delete selected Branch :-' + beData.Name,
        }).then((result) => {
            if (result.isConfirmed) {
                var para = { HealthIssueId: refData.HealthIssueId };
                $http({
                    method: 'POST',
                    url: base_url + "Infirmary/Creation/DeleteHealthIssue",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingStatus = "stop";

                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess == true) {
                        $scope.GetAllHealthIssueList();
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }

        });
    }


    //************************* Vaccine *********************************
    $scope.IsValidVaccine = function () {
        if ($scope.newVaccine.Name.isEmpty()) {
            Swal.fire('Please ! Enter VaccineName');
            return false;
        }
        return true;
    }

    $scope.SaveUpdateVaccine = function () {
        if ($scope.IsValidVaccine() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newVaccine.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateVaccine();
                    }
                });
            } else
                $scope.CallSaveUpdateVaccine();
        }
    };

    $scope.CallSaveUpdateVaccine = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveUpdateVaccine",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: { jsonData: $scope.newVaccine }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearVaccine();
                $scope.GetAllVaccineList();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }



    $scope.GetAllVaccineList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.VaccineList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllVaccine",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.VaccineList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GetVaccineById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            VaccineId: refData.VaccineId
        };
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getVaccineById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newVaccine = res.data.Data;
                $scope.newVaccine.Mode = 'Save';
                document.getElementById('Nametable').style.display = "none";
                document.getElementById('Nameform').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelVaccineById = function (refData, ind) {
        Swal.fire({
            title: 'Are you sure you want to delete ' + refData.Name + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            //message: 'Are you sure to delete selected Branch :-' + beData.Name,
        }).then((result) => {
            if (result.isConfirmed) {
                var para = { VaccineId: refData.VaccineId };
                $http({
                    method: 'POST',
                    url: base_url + "Infirmary/Creation/DeleteVaccine",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingStatus = "stop";

                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess == true) {
                        $scope.GetAllVaccineList();
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }

        });
    }

});

