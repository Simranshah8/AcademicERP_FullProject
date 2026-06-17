//agGrid.LicenseManager.setLicenseKey("==931880529fd1f843daf745e6af1c1637");
//agGrid.initialiseAgGridWithAngular1(angular);
var app = angular.module("myApp", [/*'agGrid',*/ /*'angularUtils.directives.dirPagination'*/]);

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

//app.controller('mainController', function ($scope, $http, companyDet) {
//    LoadData();

//    $scope.companyDet = {};
//    function LoadData() {
//        $scope.UserDefineRptColl = [];
//        $http.post(base_url + "Setup/ReportWriter/GetAllReportMenu").then(
//            function (res) {
//                if (res.data.IsSuccess == true) {
//                    $scope.UserDefineRptColl = res.data.Data;
//                }
//            }, function (reason) {
//                alert('Failed: ' + reason);
//            });

//        $http({
//            method: 'GET',
//            url: base_url + "Global/GetCompanyDetail",
//            dataType: "json"
//        }).then(function (res) {
//            if (res.data.IsSuccess && res.data.Data) {
//                var comDet = res.data.Data;
//                $scope.companyDet = comDet;

//                companyDet.Name = comDet.Name;
//                companyDet.MailingName = comDet.MailingName;
//                companyDet.Address = comDet.Address;
//                companyDet.RegdNo = comDet.RegdNo;
//                companyDet.PanVatNo = comDet.PanVatNo;
//                companyDet.PhoneNo = comDet.PhoneNo;
//                companyDet.FaxNo = comDet.FaxNo;
//                companyDet.EmailId = comDet.EmailId;
//                companyDet.WebSite = comDet.WebSite;
//                companyDet.StartDateAD = comDet.StartDate;
//                companyDet.EndDateAD = comDet.EndDate;
//                companyDet.StartDateBS = comDet.StartDateBS;
//                companyDet.EndDateBS = comDet.EndDateBS;

//            } else
//                alert(res.data.ResponseMSG);

//        }, function (reason) {
//            alert('Failed' + reason);
//        });
//    }

//});
