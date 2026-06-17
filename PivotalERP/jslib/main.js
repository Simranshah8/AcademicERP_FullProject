//agGrid.LicenseManager.setLicenseKey("==931880529fd1f843daf745e6af1c1637");
agGrid.initialiseAgGridWithAngular1(angular);
var app = angular.module("myApp", ['agGrid', 'angularUtils.directives.dirPagination', 'angular-cron-jobs', 'pascalprecht.translate', 'ngFileSaver',
    //'ngAnimate',                // support for CSS-based animations
    //'ngTouch',                  //for touch-enabled devices
    'ui.grid',                  //data grid for AngularJS
    'ui.grid.treeView',
    'ui.grid.saveState',
    'ui.grid.pagination',       //data grid Pagination
    'ui.grid.resizeColumns',    //data grid Resize column
    'ui.grid.moveColumns',      //data grid Move column
    'ui.grid.pinning',          //data grid Pin column Left/Right
    'ui.grid.selection',        //data grid Select Rows
    'ui.grid.autoResize',       //data grid Enabled auto column Size
    'ui.grid.exporter',          //data grid Export Data
    //'ui.grid.grouping',
  //  'ui.grid.columnsFilters',
]);
 
app.config(function ($translateProvider)
{
    $translateProvider
        .useStaticFilesLoader({
            prefix: base_url + 'jslib/locales/locale-',
            suffix: '.json'
        })
    .useSanitizeValueStrategy('sanitizeParameters')
    .preferredLanguage('in');
});
 

app.run(function ($rootScope, $http, $translate,$timeout)
{
    $rootScope.ConfigFunction = function () { };
    $rootScope.ChangeLanguage = function ()
    {
        $http({
            method: 'GET',
            url: base_url + "Global/GetComCountry",
            dataType: "json"
        }).then(function (res) {
            var comDet = res.data.Data;
            var keyLang = 'np';
            if (comDet.Country == 'Nepal') {
                keyLang = 'np';
            }
            else if (comDet.Country == 'India') {
                keyLang = 'in';
            }
            $rootScope.LANG = keyLang;
            $translate.use(keyLang);

            $timeout(function () {
                $rootScope.ConfigFunction();
            });
            
        });
    };
});


app.value('companyDet',
    {
        Name: '',
        MailingName: '',
        Address: '',
        RegdNo: '',
        PanVatNo: '',
        PhoneNo: '',
        FaxNo: '',
        EmailId: '',
        WebSite: '',
        StartDateAD: '',
        EndDateAD: '',
        StartDateBS: '',
        EndDateBS: ''
    });

app.controller('mainController', function ($scope, $http, $timeout,$rootScope, $translate, companyDet) {
     

    var navbar_dark_skins = [
        'navbar-primary',
        'navbar-secondary',
        'navbar-info',
        'navbar-success',
        'navbar-danger',
        'navbar-indigo',
        'navbar-purple',
        'navbar-pink',
        'navbar-navy',
        'navbar-lightblue',
        'navbar-teal',
        'navbar-cyan',
        'navbar-dark',
        'navbar-gray-dark',
        'navbar-gray'
    ];

    var navbar_light_skins = [
        'navbar-light',
        'navbar-warning',
        'navbar-white',
        'navbar-orange'
    ];

    var sidebar_colors = [
        'bg-primary',
        'bg-warning',
        'bg-info',
        'bg-danger',
        'bg-success',
        'bg-indigo',
        'bg-lightblue',
        'bg-navy',
        'bg-purple',
        'bg-fuchsia',
        'bg-pink',
        'bg-maroon',
        'bg-orange',
        'bg-lime',
        'bg-teal',
        'bg-olive'
    ];

    var accent_colors = [
        'accent-primary',
        'accent-warning',
        'accent-info',
        'accent-danger',
        'accent-success',
        'accent-indigo',
        'accent-lightblue',
        'accent-navy',
        'accent-purple',
        'accent-fuchsia',
        'accent-pink',
        'accent-maroon',
        'accent-orange',
        'accent-lime',
        'accent-teal',
        'accent-olive'
    ];

    var sidebar_skins = [
        'sidebar-dark-primary',
        'sidebar-dark-warning',
        'sidebar-dark-info',
        'sidebar-dark-danger',
        'sidebar-dark-success',
        'sidebar-dark-indigo',
        'sidebar-dark-lightblue',
        'sidebar-dark-navy',
        'sidebar-dark-purple',
        'sidebar-dark-fuchsia',
        'sidebar-dark-pink',
        'sidebar-dark-maroon',
        'sidebar-dark-orange',
        'sidebar-dark-lime',
        'sidebar-dark-teal',
        'sidebar-dark-olive',
        'sidebar-light-primary',
        'sidebar-light-warning',
        'sidebar-light-info',
        'sidebar-light-danger',
        'sidebar-light-success',
        'sidebar-light-indigo',
        'sidebar-light-lightblue',
        'sidebar-light-navy',
        'sidebar-light-purple',
        'sidebar-light-fuchsia',
        'sidebar-light-pink',
        'sidebar-light-maroon',
        'sidebar-light-orange',
        'sidebar-light-lime',
        'sidebar-light-teal',
        'sidebar-light-olive'
    ];

    LoadThemeOptions();
    LoadData();

    $scope.companyDet = {};

    $scope.openCustomMenu = function (mnu) {
        var arg = "/Setup/ReportWriter/RunReportViewer?TranId="+mnu.TranId+"";;
        addTabs(mnu.ReportName, arg);
     //   alert(mnu);
    }
    $scope.openCustomEntity = function (mnu) {
        var arg = "/Setup/ReportWriter/ViewNewEntity?TranId=" + mnu.TranId + "";;
        addTabs(mnu.LabelName, arg);
        //   alert(mnu);
    }
    function LoadData()
    {

        
        $('#cboMenuSearch').select2({
            ////allowClear: true,
            ////openOnEnter: true,
            placeholder: '**select menu**',
        });

        $scope.UserList = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetWebUser",
            dataType: "json"
        }).then(function (res) {
            $scope.UserList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.PurposeofContactList = [{ id: 1, text: 'Payment Followup' }, { id: 2, text: 'Overall Feedback' }, { id: 3, text: 'Software Operator' }, { id: 4, text: 'General Information' }, { id: 5, text: 'Common' }];

        $scope.ProvinceColl = GetStateList();
        $scope.DistrictColl = GetDistrictList();
        $scope.VDCColl = GetVDCList();

        $scope.ProvinceColl_Qry = mx($scope.ProvinceColl);
        $scope.DistrictColl_Qry = mx($scope.DistrictColl);
        $scope.VDCColl_Qry = mx($scope.VDCColl);

        $('.select2').select2();

        $scope.Splash = {};
        $http({
            method: 'GET',
            url: base_url + "Home/GetSplash",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess) {
                $scope.Splash = res.data.Data;

                if($scope.Splash.IsSuccess==true)
                    $('#AcademicModal').modal('show');
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.SupportExecutiveList = [];
        $http({
            method: 'POST',
            url: base_url + "Support/Creation/GetSuppExe",
            dataType: "json"
        }).then(function (res) {            
            if (res.data.IsSuccess && res.data.Data) {
                $scope.SupportExecutive = res.data.Data;
            } 
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.KYC = {};
        $http({
            method: 'GET',
            url: base_url + "Home/GetKYC",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess) {
                $scope.KYC = res.data.Data;

                if (!$scope.KYC.ContactDetColl || $scope.KYC.ContactDetColl.length == 0) {
                    $scope.KYC.ContactDetColl = [];
                    $scope.KYC.ContactDetColl.push({ Name: '', ContactStatus:true, });
                }


                var findProvince = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == $scope.KYC.ProvinceState);

                if (findProvince)
                    $scope.KYC.ProvinceStateId = findProvince.id;
                else
                    $scope.KYC.ProvinceStateId = null;

                var findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == $scope.KYC.District);
                if (findDistrict)
                    $scope.KYC.DistrictId = findDistrict.id;
                else
                    $scope.KYC.DistrictId = null;

                var findArea = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == $scope.KYC.LocalLevel);
                if (findArea)
                    $scope.KYC.LocalLevelId = findArea.id;
                else
                    $scope.KYC.LocalLevelId = null;
                 
                try {
                    var script = document.createElement('script');
                    script.type = 'text/javascript';
                    script.src = 'https://maps.googleapis.com/maps/api/js?key=' + API_KEY + '&callback=initMap';
                    document.body.appendChild(script);
                    setTimeout(function () {
                         
                        $scope.initMap();
                    }, 500);

                } catch { }

                if ($scope.KYC.NeedToUpdate == true)
                    $('#kycformmodal').modal('show');
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.CustomerModuleColl = [];

        $scope.Modules = {
            QuickAccess:false,
            FrontDesk: false,
            AcademicCalendar: false,
            Academic: false,
            Exam: false,
            Fee: false,
            Homework: false,
            Attendance: false,
            AppCMS: false,
            Transport: false,
            Hostel: false,
            Canteen: false,
            Library: false,
            OnlineClass: false,
            Accounting: false,
            Inventory: false,
            Setup: false,
            LogReport: false,
            LMS: false,
            Payroll: false,
            OnlineExam: false,
            Health: false,
            Assets: false,
            AdmissionManagement:false,
            ELearning:false,
            Infirmary: false,
            Dashboard: false,
            Communication: false,
            Banking: false,

            Scholarship:false,
            HEMIS:false,
            PhysicalInfrastructure:false,
            SupportCenter:false,
        };

        $scope.AcademicYearColl = [];
        $timeout(function () {
            $http({
                method: 'POST',
                url: base_url + "Academic/Creation/GetAllAcademicYearList",
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.AcademicYearColl = res.data.Data;

                    if ($scope.AcademicYearColl.length > 0) {
                        $scope.CurAcademicYearId = $scope.AcademicYearColl[0].AcademicYearId;
                    }
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });

        $scope.NewEntityColl = [];
        $http.post(base_url + "Setup/ReportWriter/GetNewEntityMenu").then(
            function (res) {
                if (res.data.IsSuccess == true) {
                    $scope.NewEntityColl = res.data.Data;
                }
            }, function (reason) {
                alert('Failed: ' + reason);
        });

        $timeout(function () {
            $http({
                method: 'POST',
                url: base_url + "Setup/Security/GetAllowModuleList",
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    var allowModules = mx(res.data.Data);

                    angular.forEach(res.data.Data, function (md) {
                        if (md.id >= 1000) {
                            $scope.CustomerModuleColl.push(md);
                        }
                    });

                    var f1 = allowModules.firstOrDefault(p1 => p1.id == 1 && p1.IsAllow == true);
                    if (f1)
                        $scope.Modules.QuickAccess = true;

                    var f2 = allowModules.firstOrDefault(p1 => p1.id == 2 && p1.IsAllow == true);
                    if (f2)
                        $scope.Modules.FrontDesk = true;

                    var f3 = allowModules.firstOrDefault(p1 => p1.id == 3 && p1.IsAllow == true);
                    if (f3)
                        $scope.Modules.AcademicCalendar = true;

                    var f4 = allowModules.firstOrDefault(p1 => p1.id == 4 && p1.IsAllow == true);
                    if (f4)
                        $scope.Modules.Academic = true;

                    var f5 = allowModules.firstOrDefault(p1 => p1.id == 5 && p1.IsAllow == true);
                    if (f5)
                        $scope.Modules.Exam = true;


                    var f6 = allowModules.firstOrDefault(p1 => p1.id == 6 && p1.IsAllow == true);
                    if (f6)
                        $scope.Modules.Fee = true;

                    var f7 = allowModules.firstOrDefault(p1 => p1.id == 7 && p1.IsAllow == true);
                    if (f7)
                        $scope.Modules.Homework = true;

                    var f8 = allowModules.firstOrDefault(p1 => p1.id == 8 && p1.IsAllow == true);
                    if (f8)
                        $scope.Modules.Attendance = true;

                    var f9 = allowModules.firstOrDefault(p1 => p1.id == 9 && p1.IsAllow == true);
                    if (f9)
                        $scope.Modules.AppCMS = true;

                    var f10 = allowModules.firstOrDefault(p1 => p1.id == 10 && p1.IsAllow == true);
                    if (f10)
                        $scope.Modules.Transport = true;



                    var f11 = allowModules.firstOrDefault(p1 => p1.id == 11 && p1.IsAllow == true);
                    if (f11)
                        $scope.Modules.Hostel = true;

                    var f12 = allowModules.firstOrDefault(p1 => p1.id == 12 && p1.IsAllow == true);
                    if (f12)
                        $scope.Modules.Canteen = true;

                    var f13 = allowModules.firstOrDefault(p1 => p1.id == 13 && p1.IsAllow == true);
                    if (f13)
                        $scope.Modules.Library = true;

                    var f14 = allowModules.firstOrDefault(p1 => p1.id == 14 && p1.IsAllow == true);
                    if (f14)
                        $scope.Modules.OnlineClass = true;

                    var f15 = allowModules.firstOrDefault(p1 => p1.id == 15 && p1.IsAllow == true);
                    if (f15)
                        $scope.Modules.Accounting = true;

                    var f16 = allowModules.firstOrDefault(p1 => p1.id == 16 && p1.IsAllow == true);
                    if (f16)
                        $scope.Modules.Inventory = true;

                    var f17 = allowModules.firstOrDefault(p1 => p1.id == 17 && p1.IsAllow == true);
                    if (f17)
                        $scope.Modules.Setup = true;

                    var f18 = allowModules.firstOrDefault(p1 => p1.id == 18 && p1.IsAllow == true);
                    if (f18)
                        $scope.Modules.LogReport = true;

                    var f19 = allowModules.firstOrDefault(p1 => p1.id == 19 && p1.IsAllow == true);
                    if (f19)
                        $scope.Modules.LMS = true;

                    var f20 = allowModules.firstOrDefault(p1 => p1.id == 20 && p1.IsAllow == true);
                    if (f20)
                        $scope.Modules.Payroll = true;


                    var f21 = allowModules.firstOrDefault(p1 => p1.id == 21 && p1.IsAllow == true);
                    if (f21)
                        $scope.Modules.OnlineExam = true;

                    var f22 = allowModules.firstOrDefault(p1 => p1.id == 22 && p1.IsAllow == true);
                    if (f22)
                        $scope.Modules.Health = true;

                    var f23 = allowModules.firstOrDefault(p1 => p1.id == 23 && p1.IsAllow == true);
                    if (f23)
                        $scope.Modules.Assets = true;

                    var f24 = allowModules.firstOrDefault(p1 => p1.id == 24 && p1.IsAllow == true);
                    if (f24)
                        $scope.Modules.AdmissionManagement = true;

                    var f24 = allowModules.firstOrDefault(p1 => p1.id == 24 && p1.IsAllow == true);
                    if (f24)
                        $scope.Modules.AdmissionManagement = true;
                     
                    var f25 = allowModules.firstOrDefault(p1 => p1.id == 25 && p1.IsAllow == true);
                    if (f25)
                        $scope.Modules.ELearning = true;
                    
                    var f26 = allowModules.firstOrDefault(p1 => p1.id == 26 && p1.IsAllow == true);
                    if (f26)
                        $scope.Modules.Infirmary = true;
 
                   var f27 = allowModules.firstOrDefault(p1 => p1.id == 27 && p1.IsAllow == true);
                    if (f27)
                        $scope.Modules.Dashboard = true;

                    var f28 = allowModules.firstOrDefault(p1 => p1.id == 28 && p1.IsAllow == true);
                    if (f28)
                        $scope.Modules.Communication = true;

                    var f29 = allowModules.firstOrDefault(p1 => p1.id == 29 && p1.IsAllow == true);
                    if (f29)
                        $scope.Modules.Banking = true;

                    var f30 = allowModules.firstOrDefault(p1 => p1.id == 30 && p1.IsAllow == true);
                    if (f30)
                        $scope.Modules.Scholarship = true;

                    var f31 = allowModules.firstOrDefault(p1 => p1.id == 31 && p1.IsAllow == true);
                    if (f31)
                        $scope.Modules.HEMIS = true;

                    var f32 = allowModules.firstOrDefault(p1 => p1.id == 32 && p1.IsAllow == true);
                    if (f32)
                        $scope.Modules.PhysicalInfrastructure = true;

                    var f33 = allowModules.firstOrDefault(p1 => p1.id == 33 && p1.IsAllow == true);
                    if (f33)
                        $scope.Modules.SupportCenter = true;

                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });

        $scope.LMSURL = '';
        $timeout(function () {
            $http({
                method: 'POST',
                url: base_url + "Global/GetMidasURL",
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess) {
                    $scope.LMSURL = res.data.Data.ResponseMSG;
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });

        $scope.DashboardTypeColl = [];
        $timeout(function () {
            $http({
                method: 'POST',
                url: base_url + "Setup/ReportWriter/GetDashboardTypeForUser",
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.DashboardTypeColl = res.data.Data;                     
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });


        $scope.CustomEntityColl = [];
        $timeout(function () {
            $http({
                method: 'GET',
                url: base_url + "Setup/Security/GetModuleMenu",
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    angular.forEach(res.data.Data, function (ec) {
                        if (ec.PageURL.length > 0)
                            $scope.CustomEntityColl.push(ec);
                    });
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });

        $scope.EntityColl = [];
        $timeout(function () {
            $http({
                method: 'POST',
                url: base_url + "Setup/Security/GetEntityView",
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    angular.forEach(res.data.Data, function (ec) {
                        if (ec.PageURL.length > 0)
                            $scope.EntityColl.push(ec);
                    });
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });

        $scope.AllowMenuColl = [];
        $timeout(function () {
            $http({
                method: 'POST',
                url: base_url + "Setup/Security/GetEntityAccessList",
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.AllowMenuColl = res.data.Data;

                    $('#cboMenuSearch').select2({
                       // allowClear: true,
                       // openOnEnter: true,
                        placeholder: '**select menu**',
                    });
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });


        $timeout(function () {
            $scope.Abt = {};
            $http({
                method: 'POST',
                url: base_url + "Home/GetAboutComp",
                dataType: "json",
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.Abt = res.data.Data;

                    if (!$scope.Abt.LogoPath || $scope.Abt.LogoPath.length == 0)
                        $scope.Abt.LogoPath = "/wwwroot/dynamic/images/logo.png"

                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });
        

        $timeout(function () {
            $scope.UserDefineRptColl = [];
            $http.post(base_url + "Setup/ReportWriter/GetAllReportMenu").then(
                function (res) {
                    if (res.data.IsSuccess == true) {
                        $scope.UserDefineRptColl = res.data.Data;
                    }
                }, function (reason) {
                    alert('Failed: ' + reason);
                });
        });
        

        $timeout(function () {
            $http({
                method: 'GET',
                url: base_url + "Global/GetCompanyDetail",
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    var comDet = res.data.Data;
                    $scope.companyDet = comDet;

                    companyDet.Name = comDet.Name;
                    companyDet.MailingName = comDet.MailingName;
                    companyDet.Address = comDet.Address;
                    companyDet.RegdNo = comDet.RegdNo;
                    companyDet.PanVatNo = comDet.PanVatNo;
                    companyDet.PhoneNo = comDet.PhoneNo;
                    companyDet.FaxNo = comDet.FaxNo;
                    companyDet.EmailId = comDet.EmailId;
                    companyDet.WebSite = comDet.WebSite;
                    companyDet.StartDateAD = comDet.StartDate;
                    companyDet.EndDateAD = comDet.EndDate;
                    companyDet.StartDateBS = comDet.StartDateBS;
                    companyDet.EndDateBS = comDet.EndDateBS;

                    var keyLang = 'np';
                    if (comDet.Country == 'Nepal') {
                        keyLang = 'np';
                    }
                    else if (comDet.Country == 'India') {
                        keyLang = 'in';
                    }                    
                    $rootScope.LANG = keyLang;
                    $translate.use(keyLang);

                } else
                    alert(res.data.ResponseMSG);

            }, function (reason) {
                alert('Failed' + reason);
            });
        });
        

        $timeout(function () {
            $scope.UserDetails = {};
            $http({
                method: 'GET',
                url: base_url + "Global/GetUserDetail",
                dataType: "json"
            }).then(function (res) {
                if (res.data.Data) {
                    $scope.UserDetails = res.data.Data;


                    if (!$scope.UserDetails.PhotoPath || $scope.UserDetails.PhotoPath.length == 0) {

                        $scope.UserDetails.PhotoPath = "/wwwroot/dynamic/images/aryan-shrestha.png"
                    }

                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });


     
        
    }

    $scope.mapClosed = function () {
        $('#exampleModalCenter').modal('hide');
    };

    $scope.markerColl = [];
    $scope.initMap=function() {
        try {

            var latitude = $scope.KYC.Lat; // YOUR LATITUDE VALUE
            var longitude = $scope.KYC.Lng; // YOUR LONGITUDE VALUE

            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(
                    function (position) {
                        latitude = ($scope.KYC.Lat ? $scope.KYC.Lat : position.coords.latitude);
                        longitude = ($scope.KYC.Lng ? $scope.KYC.Lng : position.coords.longitude);

                        if (latitude == 0) {
                            latitude = position.coords.latitude;
                            longitude = position.coords.longitude;
                        }

                        var myLatLng = { lat: latitude, lng: longitude };

                        map = new google.maps.Map(document.getElementById('map'), {
                            center: myLatLng,
                            zoom: 14,
                            disableDoubleClickZoom: true, // disable the default map zoom on double click
                        });

                        var marker = new google.maps.Marker({
                            position: myLatLng,
                            map: map,
                            title: 'Choose Location of School'

                            // setting latitude & longitude as title of the marker
                            // title is shown when you hover over the marker
                            //title: latitude + ', ' + longitude
                        });

                        // Update lat/long value of div when the marker is clicked
                        marker.addListener('click', function (event) {
                            //document.getElementById('latclicked').innerHTML = event.latLng.lat();
                            //document.getElementById('longclicked').innerHTML = event.latLng.lng();
                            $timeout(function () {

                                $scope.KYC.Lat = event.latLng.lat();
                                $scope.KYC.Lng = event.latLng.lng();                                 
                            })
                        });
                        $scope.markerColl.push(marker);

                        // Create new marker on double click event on the map
                        google.maps.event.addListener(map, 'dblclick', function (event) {

                            angular.forEach($scope.markerColl, function (mk) {
                                mk.setMap(null);
                            });

                            var marker = new google.maps.Marker({
                                position: event.latLng,
                                map: map,
                                //title: event.latLng.lat() + ', ' + event.latLng.lng()
                                title: 'Choose Location of School'
                            });

                            // Update lat/long value of div when the marker is clicked
                            marker.addListener('click', function () {


                                $timeout(function () {

                                    $scope.KYC.Lat = event.latLng.lat();
                                    $scope.KYC.Lng = event.latLng.lng();                                     
                                })
                            });
                            $scope.markerColl.push(marker);
                        });

                    },
                    function (position) {
                        //alert('Error111');
                    });
            }
            else {
                alert('It seems like Geolocation, which is required for this page, is not enabled in your browser.');
            }

        } catch { }

    }

    $scope.ClearlogoPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.KYC.LogoData = null;
                $scope.KYC.Logo_TMP = [];
            });

        });

        $('#imgLogo').attr('src', '');
        $('#imgPhoto1').attr('src', '');

    };

    $scope.CleargegistrationPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.KYC.RegistrationData = null;
                $scope.KYC.Registration_TMP = [];
            });

        });

        $('#imgAffiliatedLogo').attr('src', '');
        $('#imgPhoto2').attr('src', '');

    };

    $scope.ClearPanPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.KYC.PanData = null;
                $scope.KYC.Pan_TMP = [];
            });

        });


        $('#imgPhoto').attr('src', '');
        $('#imgPhoto3').attr('src', '');


    };

    $scope.ClearTaxPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.KYC.TaxData = null;
                $scope.KYC.Tax_TMP = [];
            });

        });


        $('#imgBanner').attr('src', '');
        $('#imgPhoto4').attr('src', '');
    };

    $scope.AddConctactDet = function (ind) {
        if ($scope.KYC.ContactDetColl) {
            if ($scope.KYC.ContactDetColl.length > ind + 1) {
                $scope.KYC.ContactDetColl.splice(ind + 1, 0, {
                    ClassName: '',
                    ContactStatus:true,
                })
            } else {
                $scope.KYC.ContactDetColl.push({
                    ClassName: '',
                    ContactStatus: true,
                })
            }
        }
    };
    $scope.delConctactDet = function (ind) {
        if ($scope.KYC.ContactDetColl) {
            if ($scope.KYC.ContactDetColl.length > 1) {
                $scope.KYC.ContactDetColl.splice(ind, 1);
            }
        }
    };

    $scope.UpdateKyc = function () {
        $scope.loadingstatus = "running";
        showPleaseWait(); 

        var logoImg = $scope.KYC.Logo_TMP;
        var regImg = $scope.KYC.Registration_TMP;
        var panImg = $scope.KYC.Pan_TMP;
        var taxImg = $scope.KYC.Tax_TMP;


        var selectData = $('#cboProvince').select2('data');
        if (selectData && selectData.length > 0)
            province = selectData[0].text.trim();

        selectData = $('#cboDistrict').select2('data');
        if (selectData && selectData.length > 0)
            district = selectData[0].text.trim();


        selectData = $('#cboArea').select2('data');
        if (selectData && selectData.length > 0)
            area = selectData[0].text.trim();

        $scope.KYC.ProvinceState = province;
        $scope.KYC.District = district;
        $scope.KYC.LocalLevel = area;
         
        $http({
            method: 'POST',
            url: base_url + "Home/UpdateKyc",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                if (data.dtLogoImg && data.dtLogoImg.length > 0)
                    formData.append("LogoImg", data.dtLogoImg[0]);

                if (data.dtRegImg && data.dtRegImg.length > 0)
                    formData.append("RegImg", data.dtRegImg[0]);

                if (data.dtPanImg && data.dtPanImg.length > 0)
                    formData.append("PanImg", data.dtPanImg[0]);

                if (data.dtTaxImg && data.dtTaxImg.length > 0)
                    formData.append("TaxImg", data.dtTaxImg[0]);

                return formData;
            },
            data: { jsonData: $scope.KYC, dtLogoImg: logoImg, dtRegImg: regImg, dtPanImg: panImg, dtTaxImg: taxImg }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {

            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.SearchMenuItem = null;
    $scope.SearchMenuClick = function () {
        if ($scope.SearchMenuItem) {
            if ($scope.SearchMenuItem.WebUrl && $scope.SearchMenuItem.WebUrl.length > 0) {
                var arg = '/'+$scope.SearchMenuItem.WebUrl;
                addTabs($scope.SearchMenuItem.EntityName, arg);
            }
        }
    }
    $scope.openCustDashboadWindow = function (ds) {

        var arg = '/Dashboard/Common/CDashboard?tranId=' + ds.DashboardTypeId;
        addTabs(ds.Name, arg);
    }
    $scope.ChangeAcademicYear = function () {

        var para = {
            AcademicYearId:$scope.CurAcademicYearId
        };

        $http({
            method: 'POST',
            url: base_url + "Academic/Creation/ChangeAcademicYearId",
            dataType: "json",
            data:JSON.stringify(para)
        }).then(function (res) {
            if (res.data.Data) {
                refreshActiveTab()
             /*   refreshAllTabs();*/
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };
    $scope.GetSMSBal = function () {
         

        $http({
            method: 'POST',
            url: base_url + "Global/GetSMSBalace",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            if (res.data.Data) {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };
    function LoadThemeOptions()
    {
         $scope.CurThemes = {
                NoNavbarBorder:false,
                BodySmallText: false,
                NavbarSmallText: false,
                SidebarNavSmallText: false,
                FooterSmallText: false,
                SidebarNavFlatStyle: false,
                SidebarNavLegacyStyle: false,
                SidebarNavCompact: false,
                SidebarNavChildIndent: false,
                SidebarNavChildHideOnCollapse: false,
                MainSidebarDisable: false,
                BrandSmallText: false,
                NavbarVariants: '',
                AccentColorVariants: '',
                DarkSidebarVariants: '',
                LightSidebarVariants: '',
                BrandLogoVariants: '',
            };
       
            var $sidebar = $('.control-sidebar')
            var $container = $('<div />', {
                class: 'p-3 control-sidebar-content'
            })

            $sidebar.append($container)

          
            $container.append(
                '<h5>Layout Setting</h5><hr class="mb-2"/>'
            )

            var $no_border_checkbox = $('<input />', {
                type: 'checkbox',
                value: 1,
                checked: $('.main-header').hasClass('border-bottom-0'),
                class: 'mr-1',
                id: 'chkNoNavbarBorder'
            }).on('click', function () {

                $scope.CurThemes.NoNavbarBorder = $(this).is(':checked');
                UpdateThemes(false);
            })
            var $no_border_container = $('<div />', { class: 'mb-1' }).append($no_border_checkbox).append('<span>No Navbar border</span>')
            $container.append($no_border_container)

            var $text_sm_body_checkbox = $('<input />', {
                type: 'checkbox',
                value: 1,
                checked: $('body').hasClass('text-sm'),
                class: 'mr-1',
                id: 'chkBodySmallText'
            }).on('click', function ()
            {
                $scope.CurThemes.BodySmallText = $(this).is(':checked');
                UpdateThemes(false);
            })
            var $text_sm_body_container = $('<div />', { class: 'mb-1' }).append($text_sm_body_checkbox).append('<span>Body small text</span>')
            $container.append($text_sm_body_container)

            var $text_sm_header_checkbox = $('<input />', {
                type: 'checkbox',
                value: 1,
                checked: $('.main-header').hasClass('text-sm'),
                class: 'mr-1',
                id: 'chkNavbarSmallText'
            }).on('click', function ()
            {
                $scope.CurThemes.NavbarSmallText = $(this).is(':checked');
                UpdateThemes(false);
                
            })
            var $text_sm_header_container = $('<div />', { class: 'mb-1' }).append($text_sm_header_checkbox).append('<span>Navbar small text</span>')
            $container.append($text_sm_header_container)

            var $text_sm_sidebar_checkbox = $('<input />', {
                type: 'checkbox',
                value: 1,
                checked: $('.nav-sidebar').hasClass('text-sm'),
                class: 'mr-1',
                id: 'chkSidebarNavSmallText'
            }).on('click', function () {

                $scope.CurThemes.SidebarNavSmallText = $(this).is(':checked');
                UpdateThemes(false);

            })
            var $text_sm_sidebar_container = $('<div />', { class: 'mb-1' }).append($text_sm_sidebar_checkbox).append('<span>Sidebar nav small text</span>')
            $container.append($text_sm_sidebar_container)

            var $text_sm_footer_checkbox = $('<input />', {
                type: 'checkbox',
                value: 1,
                checked: $('.main-footer').hasClass('text-sm'),
                class: 'mr-1',
                id: 'chkFooterSmallText'
            }).on('click', function () {

                $scope.CurThemes.FooterSmallText = $(this).is(':checked');
                UpdateThemes(false);
                
            })
            var $text_sm_footer_container = $('<div />', { class: 'mb-1' }).append($text_sm_footer_checkbox).append('<span>Footer small text</span>')
            $container.append($text_sm_footer_container)

            var $flat_sidebar_checkbox = $('<input />', {
                type: 'checkbox',
                value: 1,
                checked: $('.nav-sidebar').hasClass('nav-flat'),
                class: 'mr-1',
                id: 'chkSidebarNavFlatStyle'
            }).on('click', function () {

                $scope.CurThemes.SidebarNavFlatStyle = $(this).is(':checked');
                UpdateThemes(false);

            })
            var $flat_sidebar_container = $('<div />', { class: 'mb-1' }).append($flat_sidebar_checkbox).append('<span>Sidebar nav flat style</span>')
            $container.append($flat_sidebar_container)

            var $legacy_sidebar_checkbox = $('<input />', {
                type: 'checkbox',
                value: 1,
                checked: $('.nav-sidebar').hasClass('nav-legacy'),
                class: 'mr-1',
                id: 'chkSidebarNavLegacyStyle'
            }).on('click', function () {

                $scope.CurThemes.SidebarNavLegacyStyle = $(this).is(':checked');
                UpdateThemes(false);
                
            })
            var $legacy_sidebar_container = $('<div />', { class: 'mb-1' }).append($legacy_sidebar_checkbox).append('<span>Sidebar nav legacy style</span>')
            $container.append($legacy_sidebar_container)

            var $compact_sidebar_checkbox = $('<input />', {
                type: 'checkbox',
                value: 1,
                checked: $('.nav-sidebar').hasClass('nav-compact'),
                class: 'mr-1',
                id: 'chkSidebarNavCompact'
            }).on('click', function () {

                $scope.CurThemes.SidebarNavCompact = $(this).is(':checked');
                UpdateThemes(false);

            })
            var $compact_sidebar_container = $('<div />', { class: 'mb-1' }).append($compact_sidebar_checkbox).append('<span>Sidebar nav compact</span>')
            $container.append($compact_sidebar_container)

            var $child_indent_sidebar_checkbox = $('<input />', {
                type: 'checkbox',
                value: 1,
                checked: $('.nav-sidebar').hasClass('nav-child-indent'),
                class: 'mr-1',
                id: 'chkSidebarNavChildIndent'
            }).on('click', function () {

                $scope.CurThemes.SidebarNavChildIndent = $(this).is(':checked');
                UpdateThemes(false);
            })
            var $child_indent_sidebar_container = $('<div />', { class: 'mb-1' }).append($child_indent_sidebar_checkbox).append('<span>Sidebar nav child indent</span>')
            $container.append($child_indent_sidebar_container)

            var $child_hide_sidebar_checkbox = $('<input />', {
                type: 'checkbox',
                value: 1,
                checked: $('.nav-sidebar').hasClass('nav-collapse-hide-child'),
                class: 'mr-1',
                id: 'chkSidebarNavChildHideOnCollapse'
            }).on('click', function () {

                $scope.CurThemes.SidebarNavChildHideOnCollapse = $(this).is(':checked');
                UpdateThemes(false);
            })
            var $child_hide_sidebar_container = $('<div />', { class: 'mb-1' }).append($child_hide_sidebar_checkbox).append('<span>Sidebar nav child hide on collapse</span>')
            $container.append($child_hide_sidebar_container)

            var $no_expand_sidebar_checkbox = $('<input />', {
                type: 'checkbox',
                value: 1,
                checked: $('.main-sidebar').hasClass('sidebar-no-expand'),
                class: 'mr-1',
                id: 'chkMainSidebarDisable'
            }).on('click', function () {

                $scope.CurThemes.MainSidebarDisable = $(this).is(':checked');
                UpdateThemes(false);
                
            })
            var $no_expand_sidebar_container = $('<div />', { class: 'mb-1' }).append($no_expand_sidebar_checkbox).append('<span>Main Sidebar disable hover/focus auto expand</span>')
            $container.append($no_expand_sidebar_container)

            var $text_sm_brand_checkbox = $('<input />', {
                type: 'checkbox',
                value: 1,
                checked: $('.brand-link').hasClass('text-sm'),
                class: 'mr-1',
                id:'chkBrandSmallText'
            }).on('click', function () {

                $scope.CurThemes.BrandSmallText = $(this).is(':checked');
                UpdateThemes(false);

            })
        var $text_sm_brand_container = $('<div />', { class: 'mb-4' }).append($text_sm_brand_checkbox).append('<span>Brand small text</span>')
            $container.append($text_sm_brand_container)

        $container.append('<h6>Navbar Variants</h6>');

            var $navbar_variants = $('<div />', {
                class: 'd-flex'
            })
            var navbar_all_colors = navbar_dark_skins.concat(navbar_light_skins)
            var $navbar_variants_colors = createSkinBlock(navbar_all_colors, function () {
                var color = $(this).data('color')

                var $main_header = $('.main-header')
                $main_header.removeClass('navbar-dark').removeClass('navbar-light')
                navbar_all_colors.forEach(function (color) {
                    $main_header.removeClass(color)
                })

                if (navbar_dark_skins.indexOf(color) > -1) {
                    $main_header.addClass('navbar-dark')
                } else {
                    $main_header.addClass('navbar-light')
                }

                $main_header.addClass(color)

                $scope.CurThemes.NavbarVariants = color;
                UpdateThemes(false);
            })

            $navbar_variants.append($navbar_variants_colors)

            $container.append($navbar_variants)

           

            $container.append('<h6>Accent Color Variants</h6>')
            var $accent_variants = $('<div />', {
                class: 'd-flex'
            })
            $container.append($accent_variants)
            $container.append(createSkinBlock(accent_colors, function () {
                var color = $(this).data('color')
                var accent_class = color
                var $body = $('body')
                accent_colors.forEach(function (skin) {
                    $body.removeClass(skin)
                })

                $body.addClass(accent_class)

                $scope.CurThemes.AccentColorVariants = color;
                UpdateThemes(false);
            }))

            $container.append('<h6>Dark Sidebar Variants</h6>')
            var $sidebar_variants_dark = $('<div />', {
                class: 'd-flex'
            })
            $container.append($sidebar_variants_dark)
            $container.append(createSkinBlock(sidebar_colors, function () {
                var color = $(this).data('color')
                var sidebar_class = 'sidebar-dark-' + color.replace('bg-', '')
                var $sidebar = $('.main-sidebar')
                sidebar_skins.forEach(function (skin) {
                    $sidebar.removeClass(skin)
                })

                $sidebar.addClass(sidebar_class)

                $scope.CurThemes.DarkSidebarVariants = color;
                UpdateThemes(false);
            }))

            $container.append('<h6>Light Sidebar Variants</h6>')
            var $sidebar_variants_light = $('<div />', {
                class: 'd-flex'
            })
            $container.append($sidebar_variants_light)
            $container.append(createSkinBlock(sidebar_colors, function () {
                var color = $(this).data('color')
                var sidebar_class = 'sidebar-light-' + color.replace('bg-', '')
                var $sidebar = $('.main-sidebar')
                sidebar_skins.forEach(function (skin) {
                    $sidebar.removeClass(skin)
                })

                $sidebar.addClass(sidebar_class)

                $scope.CurThemes.LightSidebarVariants = color;
                UpdateThemes(false);
            }))

            var logo_skins = navbar_all_colors
            $container.append('<h6>Brand Logo Variants</h6>')
            var $logo_variants = $('<div />', {
                class: 'd-flex'
            })
            $container.append($logo_variants)
            var $clear_btn = $('<a />', {
                href: '#'
            }).text('clear').on('click', function (e) {
                e.preventDefault()
                var $logo = $('.brand-link')
                logo_skins.forEach(function (skin) {
                    $logo.removeClass(skin)
                })
            })
            $container.append(createSkinBlock(logo_skins, function () {
                var color = $(this).data('color')
                var $logo = $('.brand-link')
                logo_skins.forEach(function (skin) {
                    $logo.removeClass(skin)
                })
                $logo.addClass(color)
            }).append($clear_btn))

            function createSkinBlock(colors, callback) {
                var $block = $('<div />', {
                    class: 'd-flex flex-wrap mb-3'
                })

                colors.forEach(function (color) {
                    var $color = $('<div />', {
                        class: (typeof color === 'object' ? color.join(' ') : color).replace('navbar-', 'bg-').replace('accent-', 'bg-') + ' elevation-2'
                    })

                    $block.append($color)

                    $color.data('color', color)

                    $color.css({
                        width: '40px',
                        height: '20px',
                        borderRadius: '25px',
                        marginRight: 10,
                        marginBottom: 10,
                        opacity: 0.8,
                        cursor: 'pointer'
                    })

                    $color.hover(function () {
                        $(this).css({ opacity: 1 }).removeClass('elevation-2').addClass('elevation-4')
                    }, function () {
                        $(this).css({ opacity: 0.8 }).removeClass('elevation-4').addClass('elevation-2')
                    })

                    if (callback) {
                        $color.on('click', callback)
                    }
                })

                return $block
            }

        $('.product-image-thumb').on('click', function () {
            var image_element = $(this).find('img')
            $('.product-image').prop('src', $(image_element).attr('src'))
            $('.product-image-thumb.active').removeClass('active')
            $(this).addClass('active')
        });

        $timeout(function () {

            $http({
                method: 'POST',
                url: base_url + "Setup/Security/GetThemes",
                dataType: "json"
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.CurThemes = res.data.Data;
                    UpdateThemes(true);
                } 
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });


        });
    }

    function UpdateThemes(isLoad) {
        
        if ($scope.CurThemes.NoNavbarBorder == true)
        {
            $('.main-header').addClass('border-bottom-0')
        } else {
            $('.main-header').removeClass('border-bottom-0')
        }

        if ($scope.CurThemes.BodySmallText==true) {
            $('body').addClass('text-sm')
        } else {
            $('body').removeClass('text-sm')
        }

        if ($scope.CurThemes.NavbarSmallText==true) {
            $('.main-header').addClass('text-sm')
        } else {
            $('.main-header').removeClass('text-sm')
        }

        if ($scope.CurThemes.SidebarNavSmallText==true) {
            $('.nav-sidebar').addClass('text-sm')
        } else {
            $('.nav-sidebar').removeClass('text-sm')
        }

        if ($scope.CurThemes.FooterSmallText==true) {
            $('.main-footer').addClass('text-sm')
        } else {
            $('.main-footer').removeClass('text-sm')
        }

        
        if ($scope.CurThemes.SidebarNavFlatStyle==true) {
            $('.nav-sidebar').addClass('nav-flat')
        } else {
            $('.nav-sidebar').removeClass('nav-flat')
        }

        if ($scope.CurThemes.SidebarNavLegacyStyle==true) {
            $('.nav-sidebar').addClass('nav-legacy')
        } else {
            $('.nav-sidebar').removeClass('nav-legacy')
        }

        if ($scope.CurThemes.SidebarNavCompact==true) {
            $('.nav-sidebar').addClass('nav-compact')
        } else {
            $('.nav-sidebar').removeClass('nav-compact')
        }

        if ($scope.CurThemes.SidebarNavChildIndent==true) {
            $('.nav-sidebar').addClass('nav-child-indent')
        } else {
            $('.nav-sidebar').removeClass('nav-child-indent')
        }

        if ($scope.CurThemes.SidebarNavChildHideOnCollapse==true) {
            $('.nav-sidebar').addClass('nav-collapse-hide-child')
        } else {
            $('.nav-sidebar').removeClass('nav-collapse-hide-child')
        }
                        
        if ($scope.CurThemes.MainSidebarDisable==true) {
            $('.main-sidebar').addClass('sidebar-no-expand')
        } else {
            $('.main-sidebar').removeClass('sidebar-no-expand')
        }

        if ($scope.CurThemes.BrandSmallText==true) {
            $('.brand-link').addClass('text-sm')
        } else {
            $('.brand-link').removeClass('text-sm')
        }

        if ($scope.CurThemes.NavbarVariants && $scope.CurThemes.NavbarVariants.length > 0)
        {
            var navbar_all_colors = navbar_dark_skins.concat(navbar_light_skins)

            var color = $scope.CurThemes.NavbarVariants;
            var $main_header = $('.main-header')
            $main_header.removeClass('navbar-dark').removeClass('navbar-light')
            navbar_all_colors.forEach(function (color) {
                $main_header.removeClass(color)
            })

            if (navbar_dark_skins.indexOf(color) > -1) {
                $main_header.addClass('navbar-dark')
            } else {
                $main_header.addClass('navbar-light')
            }

            $main_header.addClass(color)
        }

        if ($scope.CurThemes.AccentColorVariants && $scope.CurThemes.AccentColorVariants.length > 0) {
            var color = $scope.CurThemes.AccentColorVariants;
            var accent_class = color;
            var $body = $('body');
            accent_colors.forEach(function (skin) {
                $body.removeClass(skin)
            })

            $body.addClass(accent_class);
        }

        if ($scope.CurThemes.DarkSidebarVariants && $scope.CurThemes.DarkSidebarVariants.length > 0) {

            var color = $scope.CurThemes.DarkSidebarVariants;
            var sidebar_class = 'sidebar-dark-' + color.replace('bg-', '');
            var $sidebar = $('.main-sidebar');
            sidebar_skins.forEach(function (skin) {
                $sidebar.removeClass(skin)
            })

            $sidebar.addClass(sidebar_class);
        }


        if ($scope.CurThemes.LightSidebarVariants && $scope.CurThemes.LightSidebarVariants.length > 0) {
            var color = $scope.CurThemes.LightSidebarVariants;
            var sidebar_class = 'sidebar-light-' + color.replace('bg-', '')
            var $sidebar = $('.main-sidebar')
            sidebar_skins.forEach(function (skin) {
                $sidebar.removeClass(skin)
            })

            $sidebar.addClass(sidebar_class)
        }


        if ($scope.CurThemes.BrandLogoVariants && $scope.CurThemes.BrandLogoVariants.length > 0) {

        }

         
        if (isLoad == false) {
            $http({
                method: 'POST',
                url: base_url + "Setup/Security/SaveThemes",
                headers: { 'Content-Type': undefined },

                transformRequest: function (data) {

                    var formData = new FormData();
                    formData.append("jsonData", angular.toJson(data.jsonData));

                    return formData;
                },
                data: { jsonData: $scope.CurThemes }
            }).then(function (res) {
                if (res.data.IsSuccess == true) {
                    // $scope.ClearCaste();

                }

            }, function (errormessage) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";

            });

        } else
        {
            $('#chkNoNavbarBorder').prop('checked', $scope.CurThemes.NoNavbarBorder);
            $('#chkBodySmallText').prop('checked', $scope.CurThemes.BodySmallText);
            $('#chkNavbarSmallText').prop('checked', $scope.CurThemes.NavbarSmallText);
            $('#chkSidebarNavSmallText').prop('checked', $scope.CurThemes.SidebarNavSmallText);
            $('#chkFooterSmallText').prop('checked', $scope.CurThemes.FooterSmallText);
            $('#chkSidebarNavFlatStyle').prop('checked', $scope.CurThemes.SidebarNavFlatStyle);
            $('#chkSidebarNavLegacyStyle').prop('checked', $scope.CurThemes.SidebarNavLegacyStyle);
            $('#chkSidebarNavCompact').prop('checked', $scope.CurThemes.SidebarNavCompact);
            $('#chkSidebarNavChildIndent').prop('checked', $scope.CurThemes.SidebarNavChildIndent);
            $('#chkSidebarNavChildHideOnCollapse').prop('checked', $scope.CurThemes.SidebarNavChildHideOnCollapse);
            $('#chkMainSidebarDisable').prop('checked', $scope.CurThemes.MainSidebarDisable);
            $('#chkBrandSmallText').prop('checked', $scope.CurThemes.BrandSmallText);

        }

        //Added to put the description below the menu in menu search
        $('#cboMenuSearch').select2({
            placeholder: "Search Menu",
            width: '100%',
            templateResult: function (data) {
                if (!data.id) return data.text;

                var desc = $(data.element).data('desc');

                return $(
                    '<div>' +
                    '<div>' + data.text + '</div>' +
                    '<div style="font-size:11px;color:#888;">' + desc + '</div>' +
                    '</div>'
                );
            },
            templateSelection: function (data) {
                return data.text;
            }
        });
    }
});
