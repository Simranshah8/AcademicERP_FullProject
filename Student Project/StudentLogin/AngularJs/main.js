//agGrid.LicenseManager.setLicenseKey("==931880529fd1f843daf745e6af1c1637");
//agGrid.initialiseAgGridWithAngular1(angular);
var app = angular.module("myApp", [/*'agGrid',*/ 'angularUtils.directives.dirPagination']);


//app.value('companyDet',
//    {
//        Name: '',
//        MailingName: '',
//        Address: '',
//        RegdNo: '',
//        PanVatNo: '',
//        PhoneNo: '',
//        FaxNo: '',
//        EmailId: '',
//        WebSite: '',
//        StartDateAD: '',
//        EndDateAD: '',
//        StartDateBS: '',
//        EndDateBS: ''
//    });

app.controller('mainController', function ($scope, $http) {
    LoadData();

    $scope.Abt = {};
    function LoadData() {

        $scope.Abt = {};
        $http({
            method: 'POST',
            url: base_url + "Accounts/GetAboutComp",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data) {
                $scope.Abt = res.data;

                if (!$scope.Abt.LogoPath || $scope.Abt.LogoPath.length == 0)
                    $scope.Abt.LogoPath = "/wwwroot/dynamic/images/aryan-shrestha.png"

            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.AppFeaturesColl = [];
        $scope.ActiveLMS = false;
        $http({
            method: 'POST',
            url: base_url + "Accounts/GetAppFeatures",
            dataType: "json",
        }).then(function (res) {
            if ( res.data) {
                $scope.AppFeaturesColl = res.data;

                angular.forEach($scope.AppFeaturesColl, function (af) {
                    if (af.EntityId == 12 && af.IsActive == true)
                        $scope.ActiveLMS = true;
                })

            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

});