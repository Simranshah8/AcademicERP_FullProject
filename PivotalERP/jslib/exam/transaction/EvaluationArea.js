app.controller('EvaluationController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Evaluation';


    OnClickDefault();
    $scope.LoadData = function () {
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.GenderList = GlobalServices.getGenderList();

        $scope.currentPages = {
            Evaluation: 1,
        };


        $scope.searchData = {
            Evaluation: '',
        };

        $scope.perPage = {
            Evaluation: GlobalServices.getPerPageRow(),
        };

        $scope.GetAllnewDetList();

        $scope.newDet = {
            EvaluationId: null,
            Name: '',
            Description: '',
            OderNo: 0,
            IsActive: '',
            Mode: 'save'
        };

    };

    function OnClickDefault() {
        document.getElementById('add-Evaluation-form').style.display = "none";
        document.getElementById('add-Evaluation').onclick = function () {
            document.getElementById('table-section').style.display = "none";
            document.getElementById('add-Evaluation-form').style.display = "block";
        }
        document.getElementById('Evaluationback-btn').onclick = function () {
            document.getElementById('add-Evaluation-form').style.display = "none";
            document.getElementById('table-section').style.display = "block";

        }
    };

    $scope.ClearDetails = function () {
        $timeout(function () {
            $scope.newDet = {
                EvaluationId: null,
                Name: '',
                Description: '',
                OrderNo: 0,
                IsActive: false,
                Mode: 'save'
            };
        });
    };


    $scope.IsValidAddEvaluationArea = function () {
        if ($scope.newDet.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }
        return true;
    }

    $scope.SaveEvaluationArea = function () {
        if ($scope.IsValidAddEvaluationArea() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newDet.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.SaveUpdateEvaluation();
                    }
                });
            } else
                $scope.SaveUpdateEvaluation();
        }
    };

    $scope.SaveUpdateEvaluation = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/SaveEvaluationArea",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newDet }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearDetails();
                $scope.GetAllnewDetList();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }



    $scope.GetAllnewDetList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.AddEvaluationList = [];
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/GetAllEvaluationArea",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.AddEvaluationList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GetEvaluationAreaById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            EvaluationId: refData.EvaluationId
        };
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/GetEvaluationAreaById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newDet = res.data.Data;

                $scope.newDet.Mode = 'Modify';

                document.getElementById('table-section').style.display = "none";
                document.getElementById('add-Evaluation-form').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelEvaluationAreaById = function (refData) {
        Swal.fire({
            title: 'Do you want to delete the selected data?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {

            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();
                var para = {
                    EvaluationId: refData.EvaluationId
                };
                $http({
                    method: 'POST',
                    url: base_url + "Exam/Transaction/DelEvaluationArea",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.ClearDetails();
                        $scope.GetAllnewDetList();
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