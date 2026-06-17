
app.controller('ApprovalSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'ApprovalSetup';

    /* OnClickDefault();*/
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();       

        $scope.currentPages = {
            ApprovalSetup: 1,
        };

        $scope.searchData = {
            ApprovalSetup: '',
        };

        $scope.perPage = {
            ApprovalSetup: GlobalServices.getPerPageRow(),
        };


        $scope.newApprovalSetup = {
            ApprovalSetupId: null,
            ForUser: 1,
            ForUserId: '',
            ForGroupId: '',
            Mode: 'Save'
        };

        //$scope.GetAllDiseaseList();
    };


    $scope.ClearApprovalSetup = function () {
        $scope.newApprovalSetup = {
            ApprovalSetupId: null,
            ForUser: 1,
            ForUserId: '',
            ForGroupId: '',
            Mode: 'Save'
        };
    };



    //************************* Approval Setup *********************************

    $scope.IsValidApprovalSetup = function () {
        if ($scope.newApprovalSetup.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }

        return true;
    };

    $scope.SaveUpdateApprovalSetup = function () {
        if ($scope.IsValidApprovalSetup() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newApprovalSetup.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateApprovalSetup();
                    }
                });
            } else
                $scope.CallSaveUpdateApprovalSetup();

        }
    };

    $scope.CallSaveUpdateApprovalSetup = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        //if ($scope.newApprovalSetup.ApprovalSetupDateDet) {
        //    $scope.newApprovalSetup.ApprovalSetupDate = $scope.newApprovalSetup.ApprovalSetupDateDet.dateAD;
        //} else
        //    $scope.newApprovalSetup.ApprovalSetupDate = null;


        $http({
            method: 'POST',
            url: base_url + "Communication/Creation/SaveApprovalSetup",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newApprovalSetup }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearApprovalSetup();
                $scope.GetAllApprovalSetupList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };




});

