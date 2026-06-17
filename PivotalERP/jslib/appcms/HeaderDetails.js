app.controller('HeaderDetailsController', function ($scope, $http, $timeout, GlobalServices) {
    $scope.Title = 'HeaderDetails';


    $scope.LoadData = function () {
        $scope.confirmMSG = GlobalServices.getConfirmMSG();

        // Initialize before GetHeaderDetails
        $scope.newDet = {
            HeaderDetailId: null,
            CompanyName: '',
            Slogan: '',
            HeaderIsActive: false,
            NameIsActive: false,
            SloganIsActive: false,
            LogoPhoto: '',
            Photo_TMP: [],
            Mode: 'save'
        };

        $scope.GetHeaderDetails();
    };

    $scope.ClearDetails = function () {
        $scope.ClearLogoPhoto();
        $timeout(function () {
            $scope.newDet = {
                HeaderDetailId: null,
                CompanyName: '',
                Slogan: '',
                HeaderIsActive: false,
                NameIsActive: false,
                SloganIsActive: false,
                Mode: 'save'
            };
        });
    };

    $scope.ClearLogoPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newDet.LogoPhoto = null;
                $scope.newDet.Photo_TMP = [];
            });
        });
        $('#imgPhoto1').attr('src', '');
    };

    $scope.IsValidHeaderDetails = function () {
        if ($scope.newDet.CompanyName.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }
        return true;
    }

    $scope.SaveHeaderDetails = function () {
        if ($scope.IsValidHeaderDetails() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newDet.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.SaveUpdateHeaderDetails();
                    }
                });
            } else
                $scope.SaveUpdateHeaderDetails();
        }
    };

    $scope.SaveUpdateHeaderDetails = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var Sphoto = $scope.newDet.Photo_TMP;

        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/SaveHeaderDetails",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                if (data.LogoPhoto && data.LogoPhoto.length > 0)
                    formData.append("Sphoto", data.LogoPhoto[0]);
                return formData;

                return formData;
            },
            data: { jsonData: $scope.newDet, LogoPhoto: Sphoto  }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearDetails();
                $scope.GetHeaderDetails();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }


  

    $scope.GetHeaderDetails = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/GetHeaderDetailsById",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.IsSuccess && res.data.Data) {
                angular.extend($scope.newDet, {
                    HeaderDetailId: res.data.Data.HeaderDetailId || null,
                    CompanyName: res.data.Data.CompanyName || '',
                    Slogan: res.data.Data.Slogan || '',
                    HeaderIsActive: res.data.Data.HeaderIsActive === true,
                    NameIsActive: res.data.Data.NameIsActive === true,
                    SloganIsActive: res.data.Data.SloganIsActive === true,
                    LogoPhoto: res.data.Data.LogoPhoto || '',
                    Photo_TMP: [], 
                    Mode: 'Modify'
                });
            } else {
                // Ensure default values are assigned without replacing the object
                angular.extend($scope.newDet, {
                    HeaderDetailId: null,
                    CompanyName: '',
                    Slogan: '',
                    HeaderIsActive: false,
                    NameIsActive: false,
                    SloganIsActive: false,
                    LogoPhoto: '',
                    Photo_TMP: [],
                    Mode: 'save'
                });
            }
        }, function (reason) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire('Failed' + reason);
        });
    };



});