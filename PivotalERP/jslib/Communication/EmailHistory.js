
app.controller('EmailHistoryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'EmailHistory';

    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.currentPages = {
            EmailHistory: 1,
        };

        $scope.searchData = {
            EmailHistory: '',
        };

        $scope.perPage = {
            EmailHistory: GlobalServices.getPerPageRow(),
        };


        $scope.newEmailHistory = {
            EmailHistoryId: null,
            Name: '',
            OrderNo: 1,
            Description: '',
            Mode: 'Save'
        };

        //$scope.GetAllEmailHistoryList();
    };

    $scope.ClearEmailHistory = function () {
        $scope.newEmailHistory = {
            EmailHistoryId: null,
            Name: '',
            OrderNo: 1,
            Description: '',
            Mode: 'Save'
        };

    };


    function OnClickDefault() {

        document.getElementById('EmailHistory-form').style.display = "none";

        document.getElementById('add-EmailHistory').onclick = function () {
            document.getElementById('EmailHistory-section').style.display = "none";
            document.getElementById('EmailHistory-form').style.display = "block";
        }
        document.getElementById('back-to-list-EmailHistory').onclick = function () {
            document.getElementById('EmailHistory-form').style.display = "none";
            document.getElementById('EmailHistory-section').style.display = "block";
        }
    };

    //************************* Class *********************************

    $scope.IsValidEmailHistory = function () {
        if ($scope.newEmailHistory.Name.isEmpty()) {
            Swal.fire('Please ! Enter Communication Name');
            return false;
        }

        return true;
    };

    $scope.SaveUpdateEmailHistory = function () {
        if ($scope.IsValidEmailHistory() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newEmailHistory.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateEmailHistory();
                    }
                });
            } else
                $scope.CallSaveUpdateEmailHistory();

        }
    };

    $scope.CallSaveUpdateEmailHistory = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        //if ($scope.newEmailHistory.EmailHistoryDateDet) {
        //    $scope.newEmailHistory.EmailHistoryDate = $scope.newEmailHistory.EmailHistoryDateDet.dateAD;
        //} else
        //    $scope.newEmailHistory.EmailHistoryDate = null;


        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveEmailHistory",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newEmailHistory }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearEmailHistory();
                $scope.GetAllEmailHistoryList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    $scope.GetAllEmailHistoryList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.EmailHistoryList = [];

        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllEmailHistory",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.EmailHistoryList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };

    $scope.GetEmailHistoryId = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            EmailHistoryId: refData.EmailHistoryId
        };

        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/GetEmailHistoryById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newEmailHistory = res.data.Data;
                $scope.newEmailHistory.Mode = 'Modify';
                document.getElementById('EmailHistory-section').style.display = "none";
                document.getElementById('EmailHistory-form').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelEmailHistoryId = function (refData) {
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
                    EmailHistoryId: refData.EmailHistoryId
                };

                $http({
                    method: 'POST',
                    url: base_url + "Infirmary/Creation/DeleteEmailHistory",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess == true) {
                        $scope.GetAllEmailHistoryList();

                    }
                    Swal.fire(res.data.ResponseMSG);

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });


    };

});

