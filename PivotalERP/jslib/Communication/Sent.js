
app.controller('SentController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Sent';

    /* OnClickDefault();*/
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();

        $scope.currentPages = {
            SentEmail: 1,
            SentSMS:1
        };

        $scope.searchData = {
            Sent: '',
        };

        $scope.perPage = {
            Sent: GlobalServices.getPerPageRow(),
        };

        $scope.newSent = {
            SentId: null,
            ForUser: 1,
            ForUserId: '',
            ForGroupId: '',
            Mode: 'Save'
        };

        //$scope.GetAllSentList();
    };


    $scope.ClearSent = function () {
        $scope.newSent = {
            SentId: null,
            ForUser: 1,
            ForUserId: '',
            ForGroupId: '',
            Mode: 'Save'
        };
    };


    //************************* Class *********************************

    $scope.IsValidSent = function () {
        if ($scope.newSent.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }

        return true;
    };

    $scope.SaveUpdateSent = function () {
        if ($scope.IsValidSent() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newSent.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateSent();
                    }
                });
            } else
                $scope.CallSaveUpdateSent();

        }
    };

    $scope.CallSaveUpdateSent = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        //if ($scope.newSent.SentDateDet) {
        //    $scope.newSent.SentDate = $scope.newSent.SentDateDet.dateAD;
        //} else
        //    $scope.newSent.SentDate = null;


        $http({
            method: 'POST',
            url: base_url + "Communication/Creation/SaveSent",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newSent }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearSent();
                $scope.GetAllSentList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };


});

