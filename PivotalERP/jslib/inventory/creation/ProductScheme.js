app.controller('ProductScheme', function ($scope, $filter, $http, $timeout, GlobalServices) {
    $scope.Title = 'Product Scheme';
    var glSrv = GlobalServices;

    $scope.confirmMSG = GlobalServices.getConfirmMSG();

    $scope.LoadData = function () {
        //$scope.confirmMSG = GlobalServices.getConfirmMSG();


        $scope.newProductScheme = {

            Name: '',
            Notes: '',
            Voucher: 0,
            Description: '',
            Area: 0,
            Agent: 0,
            Ledger: 0,
            LedgerGroup: 0,
            Product: 0,
            ProductGroup: 0,
            Mode: 'Save',



        }
    };

    $scope.ClearProductScheme = function () {
        $timeout(function () {
            $scope.newProductScheme = {
                Name: '',
                Notes: '',
                Voucher: 0,
                Description: '',
                Area: 0,
                Agent: 0,
                Ledger: 0,
                LedgerGroup: 0,
                Product: 0,
                ProductGroup: 0,
                Mode: 'Save',

            };
        });
    }


    $scope.IsValidProductScheme = function () {
        if ($scope.newProductScheme.Name.isEmpty()) {
            Swal.fire("Please ! Enter Product Name");
            return false;
        }
        else
            return true;
    }

    $scope.SaveUpdateProductScheme = function () {
        if ($scope.IsValidProductScheme() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newProductScheme.Mode;
                Swal.fire({
                    title: 'Do you want to' + saveModify + 'the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateProductScheme();
                    }

                });
            }
            else
                $scope.CallSaveUpdateProductScheme();
        }
    };

    $scope.CallSaveUpdateProductScheme = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();

        $http({
            method: 'POST',
            url: base_url + "Inventory/Creation/SaveProductScheme",
            headers: { 'content-Type': undefined },

            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: { jsonData: $scope.newProductScheme }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearProductScheme();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }
});