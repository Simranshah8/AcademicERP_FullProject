app.controller('AssestmentTypeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'AssestmentType';


    OnClickDefault();
    $scope.LoadData = function () {
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.GenderList = GlobalServices.getGenderList();

        $scope.currentPages = {
            AssestmentType: 1,
        };


        $scope.searchData = {
            AssestmentType: '',
        };

        $scope.perPage = {
            AssestmentType: GlobalServices.getPerPageRow(),
        };

        $scope.GetAllnewDetList();

        $scope.newDet = {
            AssessmentTypeId: null,
            Name: '',
            Description: '',
            OrderNo: 0,
            IsActive: true,
            Mode: 'save'
        };

    };

    function OnClickDefault() {
        document.getElementById('add-Assestment-form').style.display = "none";
        document.getElementById('add-Assestment').onclick = function () {
            document.getElementById('table-section').style.display = "none";
            document.getElementById('add-Assestment-form').style.display = "block";
        }
        document.getElementById('AssestmentTypeback-btn').onclick = function () {
            document.getElementById('add-Assestment-form').style.display = "none";
            document.getElementById('table-section').style.display = "block";

        }
    };

    $scope.ClearDetails = function () {
        $timeout(function () {
            $scope.newDet = {
                AssessmentTypeId: null,
                Name: '',
                Description: '',
                OrderNo: 0,
                IsActive: true,
                Mode: 'save'
            };
        });
    };

    $scope.IsValidAddAssestmentType = function () {
        if ($scope.newDet.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }
        return true;
    }

    $scope.SaveAssestmentType = function () {
        if ($scope.IsValidAddAssestmentType() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newDet.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.SaveUpdateAssestmentType();
                    }
                });
            } else
                $scope.SaveUpdateAssestmentType();
        }
    };

    $scope.SaveUpdateAssestmentType = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/SaveAssessmentType",
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
        $scope.AddAssestmentTypeList = [];
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/GetAllAssessmentType",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.AddAssestmentTypeList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GetExamAssesstmentTypeById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            AssessmentTypeId: refData.AssessmentTypeId
        };
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/GetAssessmentTypeById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newDet = res.data.Data;

                $scope.newDet.Mode = 'Modify';

                document.getElementById('table-section').style.display = "none";
                document.getElementById('add-Assestment-form').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };


    $scope.DelAssestmentTypeById = function (refData) {
        Swal.fire({
            title: 'Do you want to delete the selected data?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {

            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();
                var para = {
                    AssessmentTypeId: refData.AssessmentTypeId
                };
                $http({
                    method: 'POST',
                    url: base_url + "Exam/Transaction/DelAssessmentType",
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