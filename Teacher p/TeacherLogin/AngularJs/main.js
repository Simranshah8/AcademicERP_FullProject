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

app.controller('mainController', function ($scope, $http,$timeout) {
    LoadData();

    $scope.Abt = {};
    function LoadData() {


        $scope.AppFeaturesColl = [];
        $scope.ActiveLMS = false;
        $http({
            method: 'POST',
            url: base_url + "Accounts/GetAppFeatures",
            dataType: "json",
        }).then(function (res) {
            if (res.data) {
                $scope.AppFeaturesColl = res.data;

                angular.forEach($scope.AppFeaturesColl, function (af) {
                    if (af.EntityId == 12 && af.IsActive == true)
                        $scope.ActiveLMS = true;
                })

            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

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

        $timeout(function () {
            $scope.NoticeList = [];
            $http({
                method: 'GET',
                url: base_url + "Notice/Creation/GetTopNoticeList",
                dataType: "json"
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data) {
                    $scope.NoticeList = res.data;
                }

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });
       

        $timeout(function () {
            $scope.NCount = [];
            $http({
                method: 'GET',
                url: base_url + "Notice/Creation/GeNoticeCount",
                dataType: "json"
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data) {
                    $scope.NCount = res.data;
                }

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });
        
    }

    $scope.GetAllUserDetail = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        //	$scope.MyProfile = [];

        $http({
            method: 'GET',
            url: base_url + "OnlineClass/Creation/GetUserDetail",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data) {
                $scope.Details = res.data;
                //$scope.v=res.data;



            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    }


});
app.directive('tabNext', function () {
    return {
        restrict: 'A',
        link: function (scope, elem) {

            elem.bind('keyup', function (e) {
                var code = e.keyCode || e.which;
                if (code === 13) {
                    e.preventDefault();
                    var eIDX = -1;
                    for (var i = 0; i < this.form.elements.length; i++) {
                        if (elem.eq(this.form.elements[i])) {
                            eIDX = i;
                            break;
                        }
                    }
                    if (eIDX === -1) {
                        return;
                    }
                    var j = eIDX + 1;
                    var theform = this.form;
                    while (j !== eIDX) {
                        if (j >= theform.elements.length) {
                            j = 0;
                        }
                        if ((theform.elements[j].type !== "hidden") && (theform.elements[j].type !== "file")
                            && (theform.elements[j].name !== theform.elements[eIDX].name)
                            && (!theform.elements[j].disabled)
                            && (theform.elements[j].tabIndex >= 0)) {
                            if (theform.elements[j].type === "select-one") {
                                theform.elements[j].focus();
                            } else if (theform.elements[j].type === "button") {
                                theform.elements[j].focus();
                            } else {
                                theform.elements[j].focus();
                                theform.elements[j].select();
                            }
                            return;
                            break;
                        }
                        j++;
                    }
                }
            });
        }
    }
});