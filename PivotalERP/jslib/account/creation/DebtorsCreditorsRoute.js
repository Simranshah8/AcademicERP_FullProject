app.controller("DebtorsCreditorsRoute", function ($scope, $http, GlobalServices, $timeout) {
    $scope.Title = 'DebtorRoute';

    LoadData();

    function LoadData() {
        $scope.loadingstatus = "stop";
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();


        $scope.perPage = {
            DebtorsCreditorsRoute: GlobalServices.getPerPageRow(),

        };
        $scope.currentPages = {
            DebtorsCreditorsRoute: 1

        };
        $scope.searchData = {
            DebtorsCreditorsRoute: ''
        };
        $scope.beData =
        {
            DebtorsCreditorsRouteId: 0,
            Name: '',
            Alias: '',
            Code: '',
            IsActive: true,
            Mode: 'Save',


        };
        $scope.ClearFields = function () {
            $scope.loadingstatus = "stop";
            $scope.beData =
            {
                DebtorsCreditorsRouteId: 0,
                Name: '',
                Alias: '',
                Code: '',
                Mode: 'Save'

            };
            $('#txtName').focus();
        }

    };



    $scope.GetAllDebtorsCreditorsRoutes = function () {

        $scope.DebtorsCreditorsRouteColl = []; //declare an empty array

        if ($scope.loadingstatus != 'stop') {
            alert('Already Running Process')
            return;
        }

        $scope.loadingstatus = 'running';
        showPleaseWait();

        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetAllDebtorsCreditorsRoute",
            dataType: "json"
        }).then(function (res) {

            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $timeout(function () {
                    $scope.DebtorsCreditorsRouteColl = res.data.Data;
                });
            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });

    }
    $scope.IsValidDebtorCreditorRoute = function () {
        if ($scope.beData.Name.isEmpty()) {
            Swal.fire("Please ! Enter Valid Debtor Creditor Name");
            return false;
        }
        else
            return true;
    }

    $scope.AddNewDebtorsCreditorsRoute = function () {
        if ($scope.IsValidDebtorCreditorRoute() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.beData.Mode;
                Swal.fire({
                    title: 'Do you want to' + saveModify + 'the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateDebtorCreditorsRoute();
                    }

                });
            }
            else
                $scope.CallSaveUpdateDebtorCreditorsRoute();
        }
    };

    $scope.CallSaveUpdateDebtorCreditorsRoute = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();

        $http({
            method: 'POST',
            url: base_url + "Account/Creation/SaveUpdateDebtorsCreditorsRoute",
            headers: { 'content-Type': undefined },

            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: { jsonData: $scope.beData }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearFields();
                $scope.GetAllDebtorsCreditorsRoutes();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.getDebtorsCreditorsRouteById = function (beData) {

        $scope.loadingstatus = "running";

        var para = {
            DebtorRouteId: beData.DebtorRouteId
        };

        $http({
            method: 'POST',
            url: base_url + "Account/Creation/getDebtorsCreditorsRouteById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $timeout(function () {
                    $scope.beData = res.data.Data;
                    $scope.beData.Mode = 'Modify';
                    $('#custom-tabs-four-profile-tab').tab('show');
                });
            } else 
                Swal.fire(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });
    };

    $scope.deleteDebtorsCreditorsRoute = function (refData, ind) {

        Swal.fire({
            title: 'Are you sure you want to delete selected DebtorCreditor Route ' + refData.Name + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();
                var para = { DebtorRouteId: refData.DebtorRouteId };
                $http({
                    method: 'POST',
                    url: base_url + "Account/Creation/DeleteDebtorsRouteType",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess == true) {
                        $scope.DebtorsCreditorsRouteColl.splice(ind, 1);
                    }
            }, function(reason) {
                Swal.fire('Failed' + reason);
        });
    }
});
}
});