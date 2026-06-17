
app.controller('ScheduledController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Scheduled';

    /* OnClickDefault();*/
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();

        $scope.currentPages = {
            Scheduled: 1,
        };

        $scope.searchData = {
            Scheduled: '',
        };

        $scope.perPage = {
            Scheduled: GlobalServices.getPerPageRow(),
        };


        $scope.newScheduled = {
            ScheduledId: null,
            ForUser: 1,
            ForUserId: '',
            ForGroupId: '',
            Mode: 'Save'
        };

        //$scope.GetAllScheduledList();
    };


    $scope.ClearScheduled = function () {
        $scope.newScheduled = {
            ScheduledId: null,
            ForUser: 1,
            ForUserId: '',
            ForGroupId: '',
            Mode: 'Save'
        };
    };



    //************************* Class *********************************

    $scope.IsValidScheduled = function () {
        if ($scope.newScheduled.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }

        return true;
    };

    $scope.SaveUpdateScheduled = function () {
        if ($scope.IsValidScheduled() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newScheduled.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateScheduled();
                    }
                });
            } else
                $scope.CallSaveUpdateScheduled();

        }
    };

    $scope.CallSaveUpdateScheduled = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        //if ($scope.newScheduled.ScheduledDateDet) {
        //    $scope.newScheduled.ScheduledDate = $scope.newScheduled.ScheduledDateDet.dateAD;
        //} else
        //    $scope.newScheduled.ScheduledDate = null;


        $http({
            method: 'POST',
            url: base_url + "Communication/Creation/SaveScheduled",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newScheduled }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearScheduled();
                $scope.GetAllScheduledList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };




});

