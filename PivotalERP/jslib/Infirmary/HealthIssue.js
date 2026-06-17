
app.controller('HealthIssueController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'HealthIssue';

    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.SeverityList = [{ id: 1, text: 'High' }, { id: 2, text: 'Low' }, { id: 3, text: 'Medium' }];
        $scope.currentPages = {
            HealthIssue: 1,
        };

        $scope.searchData = {
            HealthIssue: '',
        };

        $scope.perPage = {
            HealthIssue: GlobalServices.getPerPageRow(),
        };


        $scope.newHealthIssue = {
            HealthIssueId: null,
            Name: '',
            Severity: '',
            OrderNo: '',
            Description: '',

            Mode: 'Save'
        };

        //$scope.GetAllHealthIssueList();
    };


    $scope.ClearHealthIssue = function () {
        $scope.newHealthIssue = {
            HealthIssueId: null,
            Name: '',
            Severity: '',
            OrderNo: '',
            Description: '',

            Mode: 'Save'
        };

    };


    function OnClickDefault() {

        document.getElementById('HealthIssue-form').style.display = "none";

        //HealthIssue section
        document.getElementById('add-HealthIssue').onclick = function () {
            document.getElementById('HealthIssue-section').style.display = "none";
            document.getElementById('HealthIssue-form').style.display = "block";
            $scope.ClearHealthIssue();
        }

        document.getElementById('back-to-list-HealthIssue').onclick = function () {
            document.getElementById('HealthIssue-form').style.display = "none";
            document.getElementById('HealthIssue-section').style.display = "block";
            $scope.ClearHealthIssue();
        }
    };

    //************************* Class *********************************

    $scope.IsValidHealthIssue = function () {
        if ($scope.newHealthIssue.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }

        return true;
    };

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

        //if ($scope.newHealthIssue.HealthIssueDateDet) {
        //    $scope.newHealthIssue.HealthIssueDate = $scope.newHealthIssue.HealthIssueDateDet.dateAD;
        //} else
        //    $scope.newHealthIssue.HealthIssueDate = null;


        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveHealthIssue",
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
    };

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

    };

    $scope.GetHealthIssueById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            HealthIssueId: refData.HealthIssueId
        };

        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/GetHealthIssueById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newHealthIssue = res.data.Data;
                $scope.newHealthIssue.Mode = 'Modify';
                document.getElementById('HealthIssue-section').style.display = "none";
                document.getElementById('HealthIssue-form').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelHealthIssueById = function (refData) {
        Swal.fire({
            title: 'Do you want to delete the selected data?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                var para = {
                    HealthIssueId: refData.HealthIssueId
                };

                $http({
                    method: 'POST',
                    url: base_url + "Infirmary/Creation/DeleteHealthIssue",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess == true) {
                          $scope.GetAllHealthIssueList();

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

