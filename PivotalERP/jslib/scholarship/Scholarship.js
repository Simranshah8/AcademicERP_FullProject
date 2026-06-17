app.controller('scholarshipController', function ($scope, $http, $timeout, $filter, $translate, $rootScope, GlobalServices) {


    $scope.curGue = {};
    $scope.getLocation = function () {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition($scope.successCallback, $scope.errorCallback);
        } else {

            Swal.fire("Geolocation is not supported by this browser.")
        }
    }

    $scope.successCallback = function (position) {
        $scope.curGue = {};
        var latitude = position.coords.latitude;
        var longitude = position.coords.longitude;
        $scope.curGue.Lat = latitude;
        $scope.curGue.Lng = longitude;

        console.log("Latitude: " + latitude + ", Longitude: " + longitude);
        // You can use latitude and longitude values as needed
    }

    $scope.errorCallback = function (error) {
        switch (error.code) {
            case error.PERMISSION_DENIED:
                Swal.fire("User denied the request for Geolocation.");
                break;
            case error.POSITION_UNAVAILABLE:
                Swal.fire("Location information is unavailable.");
                break;
            case error.TIMEOUT:
                Swal.fire("The request to get user location timed out.");
                break;
            case error.UNKNOWN_ERROR:
                Swal.fire("An unknown error occurred.");
                break;
        }
    }


    $scope.Title = 'Student Scholarship';
    $rootScope.ChangeLanguage();
    OnClickDefault();
    $scope.LoadData = function () {

        $('.select2').select2();

        $translate.refresh();

        $scope.getLocation();

        $('.select2').select2();

        $('#cboRGIdColl').select2();
        $('#cboRGIdCollNine').select2();

        var glbS = GlobalServices;
        //code added by suresh on 1 jestha starts
        $scope.SchoolTypeList = [{ id: 1, text: ' Community (Government)' }, { id: 2, text: 'Private (Institutional)' }]
        /*$scope.ScholarshipTypeList = [{ id: 1, text: 'General' }, { id: 2, text: 'General+Reservation' }]*/
        $scope.ScholarshipTypeList = [
            { id: 1, text: 'जेहेन्दार' },
            { id: 8, text: 'जेहेन्दार ‌+ आरक्षण' },
            { id: 2, text: 'जेहेन्दार ‌+ वीरगंज महानगरपालिकाको स्थायी बासिन्दा' },
            { id: 3, text: 'जेहेन्दार ‌+ वीरगंज महानगरपालिकाको स्थायी बासिन्दा ‌+ आरक्षण' }

        ];

        $scope.filterByIds = function (item) {
            return item.id === 1 || item.id === 2 || item.id === 3;
        };
        $scope.filterBygov = function (item) {
            return item.id === 1 || item.id === 4 || item.id === 5 || item.id === 6 || item.id === 7 || item.id === 8;
        };


        $scope.SEEAppearedYearList = [{ id: 1, text: '2080' }]
        $scope.BCCertyTypeList = [{ id: 1, text: 'Citizenship Certificate' }, { id: 2, text: 'Birth Certificate' }]

        //$scope.AppliedSubjectist = [{ id: 1, text: 'Science' }, { id: 2, text: 'Management' }, { id: 3, text: 'Education' }, { id: 4, text: 'Humanities' }]

        $scope.WardList = [];

        for (let i = 1; i <= 33; i++) {
            $scope.WardList.push({ id: i, text: i.toString() });
        }

        $scope.BSYearColl = [
            { id: 1, text: '2045' },
            { id: 2, text: '2046' },
            { id: 3, text: '2047' },
            { id: 4, text: '2048' },
            { id: 5, text: '2049' },
            { id: 6, text: '2050' },
            { id: 7, text: '2051' },
            { id: 8, text: '2052' },
            { id: 9, text: '2053' },
            { id: 10, text: '2054' },
            { id: 11, text: '2055' },
            { id: 12, text: '2056' },
            { id: 13, text: '2057' },
            { id: 14, text: '2058' },
            { id: 15, text: '2059' },
            { id: 16, text: '2060' },
            { id: 17, text: '2061' },
            { id: 18, text: '2062' },
            { id: 19, text: '2063' },
            { id: 20, text: '2064' },
            { id: 21, text: '2065' },
            { id: 22, text: '2066' },
            { id: 23, text: '2067' },
            { id: 24, text: '2068' },
            { id: 25, text: '2069' },
            { id: 26, text: '2070' },
            { id: 27, text: '2071' },
            { id: 28, text: '2072' },
            { id: 29, text: '2073' },
            { id: 30, text: '2074' },
            { id: 31, text: '2075' },
            { id: 32, text: '2076' },
            { id: 33, text: '2077' },
            { id: 34, text: '2078' },
            { id: 35, text: '2079' },
            { id: 36, text: '2080' }
        ];

        $scope.ADYearColl = [
            { id: 1, text: '1995' },
            { id: 2, text: '1996' },
            { id: 3, text: '1997' },
            { id: 4, text: '1998' },
            { id: 5, text: '1999' },
            { id: 6, text: '2000' },
            { id: 7, text: '2001' },
            { id: 8, text: '2002' },
            { id: 9, text: '2003' },
            { id: 10, text: '2004' },
            { id: 11, text: '2005' },
            { id: 12, text: '2006' },
            { id: 13, text: '2007' },
            { id: 14, text: '2008' },
            { id: 15, text: '2009' },
            { id: 16, text: '2010' },
            { id: 17, text: '2011' },
            { id: 18, text: '2012' },
            { id: 19, text: '2013' },
            { id: 20, text: '2014' },
            { id: 21, text: '2015' }
        ];
        $scope.AlphabetColl = [
            { id: 1, text: 'A' },
            { id: 2, text: 'B' },
            { id: 3, text: 'C' },
            { id: 4, text: 'D' },
            { id: 5, text: 'E' },
            { id: 6, text: 'F' },
            { id: 7, text: 'G' },
            { id: 8, text: 'H' },
            { id: 9, text: 'I' },
            { id: 10, text: 'J' },
            { id: 11, text: 'K' },
            { id: 12, text: 'L' },
            { id: 13, text: 'M' },
            { id: 14, text: 'N' },
            { id: 15, text: 'O' },
            { id: 16, text: 'P' },
            { id: 17, text: 'Q' },
            { id: 18, text: 'R' },
            { id: 19, text: 'S' },
            { id: 20, text: 'T' },
            { id: 21, text: 'U' },
            { id: 21, text: 'V' },
            { id: 21, text: 'W' },
            { id: 21, text: 'X' },
            { id: 21, text: 'Y' },
            { id: 21, text: 'Z' }
        ];

        $scope.DayColl = [];

        for (let i = 1; i <= 32; i++) {
            $scope.DayColl.push({ id: i, text: i.toString() });
        }

        $scope.BSMonthColl = [
            { id: 1, text: 'Baishakh' },
            { id: 2, text: 'Jestha' },
            { id: 3, text: 'Ashadh' },
            { id: 4, text: 'Shrawan' },
            { id: 5, text: 'Bhadra' },
            { id: 6, text: 'Ashwin' },
            { id: 7, text: 'Kartik' },
            { id: 8, text: 'Mangsir' },
            { id: 9, text: 'Poush' },
            { id: 10, text: 'Magh' },
            { id: 11, text: 'Falgun' },
            { id: 12, text: 'Chaitra' }
        ];
        $scope.ADMonthColl = [
            { id: 1, text: 'January' },
            { id: 2, text: 'February' },
            { id: 3, text: 'March' },
            { id: 4, text: 'April' },
            { id: 5, text: 'May' },
            { id: 6, text: 'June' },
            { id: 7, text: 'July' },
            { id: 8, text: 'August' },
            { id: 9, text: 'September' },
            { id: 10, text: 'October' },
            { id: 11, text: 'November' },
            { id: 12, text: 'December' }
        ];

        $scope.SubjectList = {};
        glbS.getSubjectList().then(function (res) {
            $scope.SubjectList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.newDet = {
            SEEYearId: 1,
            IsEligible: false,
            SelectLan: 'np'
        };

        $scope.ProvinceColl = GetStateList();
        $scope.DistrictColl = GetDistrictList();
        $scope.VDCColl = GetVDCList();

        $scope.ProvinceColl_Qry = mx($scope.ProvinceColl);
        $scope.DistrictColl_Qry = mx($scope.DistrictColl);
        $scope.VDCColl_Qry = mx($scope.VDCColl);


        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        // $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.GenderColl = GlobalServices.getGenderList();

        $scope.newScholarship = {
            TranId: null,
            FirstName: '',
            MiddleName: '',
            LastName: '',
            Gender: 1,
            SEESymbolNo: '',
            Alphabet: '',
            GPA: '',
            Email: '',
            MobileNo: '',
            F_FirstName: '',
            F_MiddleName: '',
            F_LastName: '',
            M_FirstName: '',
            M_MiddleName: '',
            M_LastName: '',
            GF_FirstName: '',
            GF_MiddleName: '',
            GF_LastName: '',
            P_ProvinceId: null,
            P_DistrictId: null,
            P_LocalLevelId: null,
            P_WardNo: null,
            P_ToleStreet: '',
            Temp_ProvinceId: '',
            Temp_DistrictId: '',
            Temp_LocalLevelId: '',
            Temp_WardNo: '',
            Temp_ToleStreet: '',
            BC_CertificateTypeId: null,
            BC_CertificateNo: '',
            BC_IssuedDistrictId: null,
            BC_IssuedLocalLevelId: null,
            BC_IssuedWardNo: null,
            BC_IssuedToleStreet: '',
            BC_DocumentNameId: null,
            BC_FilePath: '',
            SymbolNo_Alphabet: '',
            ObtainedGPA: '',
            EquivalentBoardId: 2,
            Character_Transfer_Certi: '',
            GradeSheetFilePath: '',
            SchoolName: '',
            SchoolEMISCode: '',
            SchoolTypeId: null,
            SchoolDistrictId: null,
            SchoolLocalLevelId: null,
            SchoolWardNo: '',
            SchoolToleStreet: '',
            AppliedSubjectId: null,
            SchoolPriorityListColl: [],
            ReservationTypeId: null,
            PovCerti_RefNo: '',
            PovCerti_IssuedDistrictId: null,
            PovCerti_IssuedLocalLevelId: null,
            PovCerti_WardNo: '',
            PovCerti_ToleStreet: '',
            IssuerName: '',
            IssuerDesignation: '',
            Poverty_CertiFilePath: '',
            GovSchoolCerti_RefNo: '',
            GovSchoolCertiPath: '',
            ReservationGroupId: null,
            ConcernedAuthorityId: null,
            GrpCerti_IssuedDistrictId: null,
            GrpCerti_IssuedLocalLevelId: null,
            GrpCertiIssue_WardNo: '',
            GrpCertiIssue_ToleStreet: '',
            GroupWiseCerti_RefNo: '',
            GroupWiseCerti_Path: '',
            Mode: 'Save'
        };
        $scope.newScholarship.SchoolPriorityListColl.push({});

        $scope.newScholarshipNine = {
            TranId: null,
            FirstName: '',
            MiddleName: '',
            LastName: '',
            Gender: 1,
            SEESymbolNo: '',
            Alphabet: '',
            GPA: '',
            Email: '',
            MobileNo: '',
            F_FirstName: '',
            F_MiddleName: '',
            F_LastName: '',
            M_FirstName: '',
            M_MiddleName: '',
            M_LastName: '',
            GF_FirstName: '',
            GF_MiddleName: '',
            GF_LastName: '',
            P_ProvinceId: null,
            P_DistrictId: null,
            P_LocalLevelId: null,
            P_WardNo: null,
            P_ToleStreet: '',
            Temp_ProvinceId: '',
            Temp_DistrictId: '',
            Temp_LocalLevelId: '',
            Temp_WardNo: '',
            Temp_ToleStreet: '',
            BC_CertificateTypeId: null,
            BC_CertificateNo: '',
            BC_IssuedDistrictId: null,
            BC_IssuedLocalLevelId: null,
            BC_IssuedWardNo: null,
            BC_IssuedToleStreet: '',
            BC_DocumentNameId: null,
            BC_FilePath: '',
            SymbolNo_Alphabet: '',
            ObtainedGPA: '',
            EquivalentBoardId: 2,
            Character_Transfer_Certi: '',
            GradeSheetFilePath: '',
            SchoolName: '',
            SchoolEMISCode: '',
            SchoolTypeId: null,
            SchoolDistrictId: null,
            SchoolLocalLevelId: null,
            SchoolWardNo: '',
            SchoolToleStreet: '',
            AppliedSubjectId: null,
            SchoolPriorityListColl: [],
            ReservationTypeId: null,
            PovCerti_RefNo: '',
            PovCerti_IssuedDistrictId: null,
            PovCerti_IssuedLocalLevelId: null,
            PovCerti_WardNo: '',
            PovCerti_ToleStreet: '',
            IssuerName: '',
            IssuerDesignation: '',
            Poverty_CertiFilePath: '',
            GovSchoolCerti_RefNo: '',
            GovSchoolCertiPath: '',
            ReservationGroupId: null,
            ConcernedAuthorityId: null,
            GrpCerti_IssuedDistrictId: null,
            GrpCerti_IssuedLocalLevelId: null,
            GrpCertiIssue_WardNo: '',
            GrpCertiIssue_ToleStreet: '',
            GroupWiseCerti_RefNo: '',
            GroupWiseCerti_Path: '',
            Mode: 'Save'
        };
        $scope.newScholarshipNine.SchoolPriorityListColl.push({});



        $scope.CasteList = [];
        GlobalServices.getCasteList().then(function (res) {
            $scope.CasteList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.MediumList = [];
        GlobalServices.getMediumList().then(function (res) {
            $scope.MediumList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });



        $scope.StudentTypeList = [];
        GlobalServices.getStudentTypeList().then(function (res) {
            $scope.StudentTypeList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.DocumentTypeList = [];
        GlobalServices.getDocumentTypeList().then(function (res) {
            $scope.DocumentTypeList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.ReligionList = GlobalServices.getReligionList();
        $scope.CountryList = GlobalServices.getCountryList();
        $scope.DisablityList = GlobalServices.getDisablityList();


        $scope.AllBoardList = [];
        $http({
            method: 'GET',
            url: base_url + "Scholarship/GetAllEquivalentBoard",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.AllBoardList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });



        $scope.SchoolList = [];

        $http({
            method: 'GET',
            url: base_url + "Scholarship/GetAllAppliedSchool",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.SchoolList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });



        $scope.ReservationGroupList = [];
        $scope.ReservationGroupListAll = [];
        $http({
            method: 'GET',
            url: base_url + "Scholarship/GetAllReservationGroup",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ReservationGroupList = res.data.Data;
                $scope.ReservationGroupListAll = res.data.Data;
                $scope.ChangeGender();
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.ReservationTypeList = [];
        $http({
            method: 'GET',
            url: base_url + "Scholarship/GetAllReservationType",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ReservationTypeList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.AuthorityList = [];
        $http({
            method: 'GET',
            url: base_url + "Scholarship/GetAllAuthority",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.AuthorityList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.generateCaptcha();
        //$scope.generateCaptchaNine();

    }
    $scope.ChangeGender = function () {
        $scope.ReservationGroupList = [];
        $scope.ReservationGroupListAll.forEach(function (rs) {            
                $scope.ReservationGroupList.push(rs);
        });

        $('#cboRGIdColl').select2();
        $('#cboRGIdCollNine').select2();
        //if ($scope.newScholarship.Gender != 2) {
        //    $scope.ReservationGroupListAll.forEach(function (rs) {
        //        if (rs.TranId != 1)
        //            $scope.ReservationGroupList.push(rs);
        //    });
        //} else {
        //    $scope.ReservationGroupListAll.forEach(function (rs) {
        //        $scope.ReservationGroupList.push(rs);
        //    });
        //}
    }
    $scope.SamePAddress = function () {
        if ($scope.newScholarship.IsSameAsPermanentAddress == true) {

            $scope.newScholarship.Temp_ProvinceId = null;
            $scope.newScholarship.Temp_ProvinceId = null;
            $timeout(function () {
                $('#cboProvinceTmp').select2("val", $scope.newScholarship.P_ProvinceId);
                $('#cboWardNoTmp').select2("val", $scope.newScholarship.P_WardNo);

                $scope.newScholarship.Temp_ProvinceId = $scope.newScholarship.P_ProvinceId;
                $scope.newScholarship.Temp_ProvinceId = $scope.newScholarship.P_ProvinceId;

                $scope.newScholarship.Temp_WardNo = $scope.newScholarship.P_WardNo;
                $scope.newScholarship.Temp_WardNo = $scope.newScholarship.P_WardNo;

                $scope.newScholarship.Temp_DistrictId = $scope.newScholarship.P_DistrictId;
                $scope.newScholarship.Temp_LocalLevelId = $scope.newScholarship.P_LocalLevelId;
                $scope.newScholarship.Temp_WardNo = $scope.newScholarship.P_WardNo;
                $scope.newScholarship.Temp_ToleStreet = $scope.newScholarship.P_ToleStreet;

            });


        } else {
            $scope.newScholarship.Temp_ProvinceId = null;
            $scope.newScholarship.Temp_DistrictId = null;
            $scope.newScholarship.Temp_LocalLevelId = null;
            $scope.newScholarship.Temp_WardNo = '';
            $scope.newScholarship.Temp_ToleStreet = '';
        }

        $timeout(function () {
            $scope.ProvinceChange();
            $scope.DistrictChange();
            $scope.VDCChange();
        });

    }

    $scope.SamePAddressNine = function () {
        if ($scope.newScholarshipNine.IsSameAsPermanentAddress == true) {

            $scope.newScholarshipNine.Temp_ProvinceId = null;
            $scope.newScholarshipNine.Temp_ProvinceId = null;

            $scope.newScholarshipNine.Temp_ProvinceId = $scope.newScholarshipNine.P_ProvinceId;
            $scope.newScholarshipNine.Temp_ProvinceId = $scope.newScholarshipNine.P_ProvinceId;

            $scope.newScholarshipNine.Temp_DistrictId = $scope.newScholarshipNine.P_DistrictId;
            $scope.newScholarshipNine.Temp_LocalLevelId = $scope.newScholarshipNine.P_LocalLevelId;
            $scope.newScholarshipNine.Temp_WardNo = $scope.newScholarshipNine.P_WardNo;
            $scope.newScholarshipNine.Temp_ToleStreet = $scope.newScholarshipNine.P_ToleStreet;


        } else {
            $scope.newScholarshipNine.Temp_ProvinceId = null;
            $scope.newScholarshipNine.Temp_DistrictId = null;
            $scope.newScholarshipNine.Temp_LocalLevelId = null;
            $scope.newScholarshipNine.Temp_WardNo = '';
            $scope.newScholarshipNine.Temp_ToleStreet = '';
        }

        $timeout(function () {
            $scope.ProvinceChangeNine();
            $scope.DistrictChangeNine();
            $scope.VDCChangeNine();
        });

    }


    $scope.ClearScholarship = function () {

        $scope.PrintForm = false;
        $scope.needPassword = false;
        $scope.ClearPhoto();
        $scope.ClearSignature();

        $scope.ClearPhotoNine();
        $scope.ClearSignatureNine();

        $scope.newScholarship = {
            TranId: null,
            FirstName: '',
            MiddleName: '',
            LastName: '',
            Gender: null,
            SEESymbolNo: '',
            Alphabet: '',
            GPA: '',
            Email: '',
            MobileNo: '',
            F_FirstName: '',
            F_MiddleName: '',
            F_LastName: '',
            M_FirstName: '',
            M_MiddleName: '',
            M_LastName: '',
            GF_FirstName: '',
            GF_MiddleName: '',
            GF_LastName: '',
            P_ProvinceId: null,
            P_DistrictId: null,
            P_LocalLevelId: null,
            P_WardNo: null,
            P_ToleStreet: '',
            Temp_ProvinceId: '',
            Temp_DistrictId: '',
            Temp_LocalLevelId: '',
            Temp_WardNo: '',
            Temp_ToleStreet: '',
            BC_CertificateTypeId: null,
            BC_CertificateNo: '',
            BC_IssuedDistrictId: null,
            BC_IssuedLocalLevelId: null,
            BC_IssuedWardNo: null,
            BC_IssuedToleStreet: '',
            BC_DocumentNameId: null,
            BC_FilePath: '',
            SymbolNo_Alphabet: '',
            ObtainedGPA: '',
            EquivalentBoardId: null,
            Character_Transfer_Certi: '',
            GradeSheetFilePath: '',
            SchoolName: '',
            SchoolEMISCode: '',
            SchoolTypeId: null,
            SchoolDistrictId: null,
            SchoolLocalLevelId: null,
            SchoolWardNo: '',
            SchoolToleStreet: '',
            AppliedSubjectId: null,
            SchoolPriorityListColl: [],
            ReservationTypeId: null,
            PovCerti_RefNo: '',
            PovCerti_IssuedDistrictId: null,
            PovCerti_IssuedLocalLevelId: null,
            PovCerti_WardNo: '',
            PovCerti_ToleStreet: '',
            IssuerName: '',
            IssuerDesignation: '',
            Poverty_CertiFilePath: '',
            GovSchoolCerti_RefNo: '',
            GovSchoolCertiPath: '',
            ReservationGroupId: null,
            ConcernedAuthorityId: null,
            GrpCerti_IssuedDistrictId: null,
            GrpCerti_IssuedLocalLevelId: null,
            GrpCertiIssue_WardNo: '',
            GrpCertiIssue_ToleStreet: '',
            GroupWiseCerti_RefNo: '',
            GroupWiseCerti_Path: '',
            Mode: 'Save'
        };
        $scope.newScholarship.SchoolPriorityListColl.push({});

        angular.forEach(
            angular.element("input[type='file']"),
            function (inputElem) {
                angular.element(inputElem).val(null);
            });


        $scope.newScholarshipNine = {
            TranId: null,
            FirstName: '',
            MiddleName: '',
            LastName: '',
            Gender: null,
            SEESymbolNo: '',
            Alphabet: '',
            GPA: '',
            Email: '',
            MobileNo: '',
            F_FirstName: '',
            F_MiddleName: '',
            F_LastName: '',
            M_FirstName: '',
            M_MiddleName: '',
            M_LastName: '',
            GF_FirstName: '',
            GF_MiddleName: '',
            GF_LastName: '',
            P_ProvinceId: null,
            P_DistrictId: null,
            P_LocalLevelId: null,
            P_WardNo: null,
            P_ToleStreet: '',
            Temp_ProvinceId: '',
            Temp_DistrictId: '',
            Temp_LocalLevelId: '',
            Temp_WardNo: '',
            Temp_ToleStreet: '',
            BC_CertificateTypeId: null,
            BC_CertificateNo: '',
            BC_IssuedDistrictId: null,
            BC_IssuedLocalLevelId: null,
            BC_IssuedWardNo: null,
            BC_IssuedToleStreet: '',
            BC_DocumentNameId: null,
            BC_FilePath: '',
            SymbolNo_Alphabet: '',
            ObtainedGPA: '',
            EquivalentBoardId: null,
            Character_Transfer_Certi: '',
            GradeSheetFilePath: '',
            SchoolName: '',
            SchoolEMISCode: '',
            SchoolTypeId: null,
            SchoolDistrictId: null,
            SchoolLocalLevelId: null,
            SchoolWardNo: '',
            SchoolToleStreet: '',
            AppliedSubjectId: null,
            SchoolPriorityListColl: [],
            ReservationTypeId: null,
            PovCerti_RefNo: '',
            PovCerti_IssuedDistrictId: null,
            PovCerti_IssuedLocalLevelId: null,
            PovCerti_WardNo: '',
            PovCerti_ToleStreet: '',
            IssuerName: '',
            IssuerDesignation: '',
            Poverty_CertiFilePath: '',
            GovSchoolCerti_RefNo: '',
            GovSchoolCertiPath: '',
            ReservationGroupId: null,
            ConcernedAuthorityId: null,
            GrpCerti_IssuedDistrictId: null,
            GrpCerti_IssuedLocalLevelId: null,
            GrpCertiIssue_WardNo: '',
            GrpCertiIssue_ToleStreet: '',
            GroupWiseCerti_RefNo: '',
            GroupWiseCerti_Path: '',
            Mode: 'Save'
        };
        $scope.newScholarshipNine.SchoolPriorityListColl.push({});

    }

 

    $scope.generateCaptcha = function () {
        const canvas = document.getElementById('captcha');
        const context = canvas.getContext('2d');
        $scope.captchaText = Math.random().toString(36).substring(2, 8); // Generate random text
        context.clearRect(0, 0, canvas.width, canvas.height); // Clear the canvas
        context.font = '30px Arial';
        context.fillStyle = '#000';
        context.fillText($scope.captchaText, 50, 50); // Draw the text in canvas


        const canvas1 = document.getElementById('captchaNine');
        const context1 = canvas1.getContext('2d');
        context1.clearRect(0, 0, canvas1.width, canvas1.height); // Clear the canvas
        context1.font = '30px Arial';
        context1.fillStyle = '#000';
        context1.fillText($scope.captchaText, 50, 50); // Draw the text in canvas
    };


    $scope.generateCaptchaNine = function () {
        const canvas = document.getElementById('captchaNine');
        const context = canvas.getContext('2d');
        $scope.captchaText = Math.random().toString(36).substring(2, 8); // Generate random text
        context.clearRect(0, 0, canvas.width, canvas.height); // Clear the canvas
        context.font = '30px Arial';
        context.fillStyle = '#000';
        context.fillText($scope.captchaText, 50, 50); // Draw the text in canvas
    };



    $scope.ChangeLng = function () {
        $translate.use($scope.newDet.SelectLan);
    };


    function OnClickDefault() {
        document.getElementById('form-page').style.display = "none";

        document.getElementById('reservation-form').style.display = "none";
        document.getElementById('academic-form').style.display = "none";
        document.getElementById('personal-form').style.display = "block";
        document.getElementById('second').style.display = "none";
        document.getElementById('third').style.display = "none";
        document.getElementById('fourth').style.display = "none";

        document.getElementById('PreviewForm').style.display = "none";


        document.getElementById('ClassElevenForm').onclick = function () {
            document.getElementById('HeaderPart').style.display = "none";
            document.getElementById('home-1').style.display = "block";
            //$timeout(function () {
            //    $scope.ClearBranch();
            //});

        }

        document.getElementById('ClassNine').onclick = function () {
            document.getElementById('HeaderPart').style.display = "none";
            document.getElementById('profile-1').style.display = "block";
            //$timeout(function () {
            //    $scope.ClearBranch();
            //});

        }

        //for class 9 show/hide
        document.getElementById('form-pageNine').style.display = "none";
        document.getElementById('form-pageNine').style.display = "none";

        document.getElementById('reservation-formNine').style.display = "none";
        document.getElementById('academic-formNine').style.display = "none";
        document.getElementById('personal-formNine').style.display = "block";
        document.getElementById('secondNine').style.display = "none";
        document.getElementById('thirdNine').style.display = "none";
        document.getElementById('fourthNine').style.display = "none";
        document.getElementById('PreviewFormNine').style.display = "none";


        //document.getElementById('add-scholarship').onclick = function () {

        //	if (!$scope.curGue.Lat || !$scope.curGue.Lng) {
        //		Swal.fire('Please ! Allow Geo Location 1st')
        //		return;
        //          }

        //	if ($scope.isValidateSEEYEar() == true
        //		&& $scope.isValidateEligibility() == true
        //	) {
        //		document.getElementById('form-page').style.display = "block";
        //		document.getElementById('firstpage').style.display = "none";
        //		document.getElementById('fourth').style.display = "none";
        //	}
        //}


        document.getElementById('add-academic').onclick = function () {
            if ($scope.isValidateFirstName() == true &&
                $scope.isValidateLastName() == true &&
                $scope.isValidateGender() == true &&
                $scope.isValidateSymbolNo() == true &&
                $scope.isValidateAlphabet() == true &&
                $scope.isValidateGPA() == true &&
                $scope.isValidateEmail() == true

                &&
                $scope.isValidateMobileNo() == true &&
                $scope.isValidateFatherFName() == true &&
                $scope.isValidateFatherLName() == true &&
                $scope.isValidateMotherFName() == true &&
                $scope.isValidateMotherLName() == true &&
                $scope.isValidateGrandfatherFName() == true &&
                $scope.isValidateGrandfatherLName() == true

                &&
                $scope.isValidatePProvince() == true &&
                $scope.isValidatePDistrict() == true &&
                $scope.isValidatePLocalLevel() == true &&
                $scope.isValidatePWardNo() == true

                &&
                $scope.isValidateTempProvince() == true &&
                $scope.isValidateTempDistrict() == true &&
                $scope.isValidateTempLocalLevel() == true &&
                $scope.isValidateTempWardNo() == true &&
                $scope.isValidateCertificateType() == true &&
                $scope.isValidateBSLocalLevel() == true

                &&
                $scope.isValidateCertificateNumber() == true &&
                $scope.isValidateCertificateDate() == true &&
                $scope.isValidateCertificateIssueDistrict() == true &&
                $scope.isValidateCertificateName() == true


                &&
                $scope.isValidateProfilePhoto() == true &&
                $scope.isValidateSignature() == true

                //Updated by suresh on 7 Ashar				
                &&
                $scope.isValidateCtznFrontUpload() == true &&
                $scope.isValidateCtznBackUpload() == true &&
                $scope.isValidateBirthCertiUpload() == true
                //Ends
            ) {
                document.getElementById('personal-form').style.display = "none";
                document.getElementById('reservation-form').style.display = "none";
                document.getElementById('academic-form').style.display = "block";
                document.getElementById('second').style.display = "block";
                document.getElementById('third').style.display = "none";
                document.getElementById('first').style.display = "none";
                document.getElementById('fourth').style.display = "none";
            }
        }

        //js for class 9 academic
        document.getElementById('add-academicNine').onclick = function () {
            if ($scope.isValidateFirstNameNine() == true &&
                $scope.isValidateLastNameNine() == true &&
                $scope.isValidateGenderNine() == true &&
                $scope.isValidateSymbolNoNine() == true &&
                $scope.isValidateAlphabetNine() == true &&
                $scope.isValidateGPANine() == true &&
                $scope.isValidateEmailNine() == true

                &&
                $scope.isValidateMobileNoNine() == true &&
                $scope.isValidateFatherFNameNine() == true &&
                $scope.isValidateFatherLNameNine() == true &&
                $scope.isValidateMotherFNameNine() == true &&
                $scope.isValidateMotherLNameNine() == true &&
                $scope.isValidateGrandfatherFNameNine() == true &&
                $scope.isValidateGrandfatherLNameNine() == true

                &&
                $scope.isValidatePProvinceNine() == true &&
                $scope.isValidatePDistrictNine() == true &&
                $scope.isValidatePLocalLevelNine() == true &&
                $scope.isValidatePWardNoNine() == true

                &&
                $scope.isValidateTempProvinceNine() == true &&
                $scope.isValidateTempDistrictNine() == true &&
                $scope.isValidateTempLocalLevelNine() == true &&
                $scope.isValidateTempWardNoNine() == true &&
                $scope.isValidateCertificateTypeNine() == true &&
                $scope.isValidateBSLocalLevelNine() == true

                &&
                $scope.isValidateCertificateNumberNine() == true &&
                $scope.isValidateCertificateDateNine() == true &&
                $scope.isValidateCertificateIssueDistrictNine() == true &&
                $scope.isValidateCertificateNameNine() == true


                &&
                //$scope.isValidateProfilePhotoNine() == true &&
                //$scope.isValidateSignatureNine() == true

                //Updated by suresh on 7 Ashar

                $scope.isValidateCtznFrontUploadNine() == true &&
                $scope.isValidateCtznBackUploadNine() == true &&
                $scope.isValidateBirthCertiUploadNine() == true
                //Ends
            ) {
                document.getElementById('personal-formNine').style.display = "none";
                document.getElementById('reservation-formNine').style.display = "none";
                document.getElementById('academic-formNine').style.display = "block";
                document.getElementById('second').style.display = "block";
                document.getElementById('third').style.display = "none";
                document.getElementById('first').style.display = "none";
                document.getElementById('fourth').style.display = "none";
            }
        }


        document.getElementById('add-reservation').onclick = function () {

            if ($scope.isValidateEquivalentBoard() == true
                //Updated by suresh on 7 Ashar
                &&
                $scope.isValidateCharacterTransferCerti() == true
                //Ends
                &&
                $scope.isValidateSchoolName() == true &&
                $scope.isValidateCertificateissueDate() == true &&
                $scope.isValidateSchoolType() == true &&
                $scope.isValidateSchoolDistrict() == true &&
                $scope.isValidateSchoolLocalLevel() == true
                //&& $scope.isValidateSchoolWardNo() == true
                &&
                $scope.isValidateAppliedSubject() == true &&
                $scope.isValidateSchoolPriority() == true
            ) {
                var cc = 0;
                $scope.newScholarship.SchoolPriorityListColl.forEach(function (ss) {
                    if (ss.SchoolId > 0)
                        cc++;
                });
                if (cc != $scope.SchoolList.length) {
                    var msg = 'अहिले तपाईंले ' + cc + ' वटा विद्यालय मात्र छनोट गर्नु भएको छ । तपाईंले यसमा अझै विद्यालयहरु थप गरी छनोट गर्न सक्नु हुनेछ । के तपाईं अझै विद्यालयहरु छनोट गर्न चाहनु हुन्छ ?   चाहनु हुन्छ भने Yes click गर्नुहोला, नभए No click गरी अघि बढ्नुहोला ।';
                    Swal.fire({
                        title: msg,
                        showCancelButton: true,
                        confirmButtonText: 'Yes',
                        cancelButtonText: "No",
                    }).then((result) => {
                        /* Read more about isConfirmed, isDenied below */
                        if (result.isConfirmed) {

                        } else {
                            document.getElementById('personal-form').style.display = "none";
                            document.getElementById('reservation-form').style.display = "block";
                            document.getElementById('academic-form').style.display = "none";

                            document.getElementById('second').style.display = "none";
                            document.getElementById('third').style.display = "block";
                            document.getElementById('first').style.display = "none";
                            document.getElementById('fourth').style.display = "none";
                        }
                    });
                } else {
                    document.getElementById('personal-form').style.display = "none";
                    document.getElementById('reservation-form').style.display = "block";
                    document.getElementById('academic-form').style.display = "none";

                    document.getElementById('second').style.display = "none";
                    document.getElementById('third').style.display = "block";
                    document.getElementById('first').style.display = "none";
                    document.getElementById('fourth').style.display = "none";
                }



            }
        }

        //js for class 9 add reservation

        document.getElementById('add-reservationNine').onclick = function () {

            if ($scope.isValidateEquivalentBoardNine() == true
                //Updated by suresh on 7 Ashar
                &&
                $scope.isValidateCharacterTransferCertiNine() == true
                //Ends
                &&
                $scope.isValidateSchoolNameNine() == true &&
                $scope.isValidateCertificateissueDateNine() == true &&
                $scope.isValidateSchoolTypeNine() == true &&
                $scope.isValidateSchoolDistrictNine() == true
                //&& $scope.isValidateSchoolLocalLevel() == true
                //&& $scope.isValidateSchoolWardNo() == true
                /*	&& $scope.isValidateAppliedSubject() == true*/
                /*	&& $scope.isValidateSchoolPriority() == true*/
            ) {
                var cc = 0;
                $scope.newScholarshipNine.SchoolPriorityListColl.forEach(function (ss) {
                    if (ss.SchoolId > 0)
                        cc++;
                });
                if (cc != $scope.SchoolList.length) {
                    var msg = 'अहिले तपाईंले ' + cc + ' वटा विद्यालय मात्र छनोट गर्नु भएको छ । तपाईंले यसमा अझै विद्यालयहरु थप गरी छनोट गर्न सक्नु हुनेछ । के तपाईं अझै विद्यालयहरु छनोट गर्न चाहनु हुन्छ ?   चाहनु हुन्छ भने Yes click गर्नुहोला, नभए No click गरी अघि बढ्नुहोला ।';
                    Swal.fire({
                        title: msg,
                        showCancelButton: true,
                        confirmButtonText: 'Yes',
                        cancelButtonText: "No",
                    }).then((result) => {
                        /* Read more about isConfirmed, isDenied below */
                        if (result.isConfirmed) {

                        } else {
                            document.getElementById('personal-formNine').style.display = "none";
                            document.getElementById('reservation-formNine').style.display = "block";
                            document.getElementById('academic-formNine').style.display = "none";

                            document.getElementById('secondNine').style.display = "none";
                            document.getElementById('thirdNine').style.display = "block";
                            document.getElementById('firstNine').style.display = "none";
                            document.getElementById('fourthNine').style.display = "none";
                        }
                    });
                } else {
                    document.getElementById('personal-formNine').style.display = "none";
                    document.getElementById('reservation-formNine').style.display = "block";
                    document.getElementById('academic-formNine').style.display = "none";

                    document.getElementById('secondNine').style.display = "none";
                    document.getElementById('thirdNine').style.display = "block";
                    document.getElementById('firstNine').style.display = "none";
                    document.getElementById('fourthNine').style.display = "none";
                }



            }
        }

        //js for class 9 academic
        document.getElementById('add-academicNine').onclick = function () {
            if ($scope.isValidateFirstNameNine() == true &&
                $scope.isValidateLastNameNine() == true &&
                $scope.isValidateGenderNine() == true &&
                $scope.isValidateSymbolNoNine() == true &&
                $scope.isValidateAlphabetNine() == true &&
                $scope.isValidateGPANine() == true &&
                $scope.isValidateEmailNine() == true

                &&
                $scope.isValidateMobileNoNine() == true &&
                $scope.isValidateFatherFNameNine() == true &&
                $scope.isValidateFatherLNameNine() == true &&
                $scope.isValidateMotherFNameNine() == true &&
                $scope.isValidateMotherLNameNine() == true &&
                $scope.isValidateGrandfatherFNameNine() == true &&
                $scope.isValidateGrandfatherLNameNine() == true

                &&
                $scope.isValidatePProvinceNine() == true &&
                $scope.isValidatePDistrictNine() == true &&
                $scope.isValidatePLocalLevelNine() == true &&
                $scope.isValidatePWardNoNine() == true

                &&
                $scope.isValidateTempProvinceNine() == true &&
                $scope.isValidateTempDistrictNine() == true &&
                $scope.isValidateTempLocalLevelNine() == true &&
                $scope.isValidateTempWardNoNine() == true &&
                $scope.isValidateCertificateTypeNine() == true &&
                $scope.isValidateBSLocalLevelNine() == true

                &&
                $scope.isValidateCertificateNumberNine() == true &&
                $scope.isValidateCertificateDateNine() == true &&
                $scope.isValidateCertificateIssueDistrictNine() == true &&
                $scope.isValidateCertificateNameNine() == true


                &&
                //$scope.isValidateProfilePhotoNine() == true &&
                //$scope.isValidateSignatureNine() == true

                //Updated by suresh on 7 Ashar

                $scope.isValidateCtznFrontUploadNine() == true &&
                $scope.isValidateCtznBackUploadNine() == true &&
                $scope.isValidateBirthCertiUploadNine() == true
                //Ends
            ) {
                document.getElementById('personal-formNine').style.display = "none";
                document.getElementById('reservation-formNine').style.display = "none";
                document.getElementById('academic-formNine').style.display = "block";
                document.getElementById('second').style.display = "block";
                document.getElementById('third').style.display = "none";
                document.getElementById('first').style.display = "none";
                document.getElementById('fourth').style.display = "none";
            }
        }



        document.getElementById('back-personal').onclick = function () {
            document.getElementById('personal-form').style.display = "block";
            document.getElementById('reservation-form').style.display = "none";
            document.getElementById('academic-form').style.display = "none";
            document.getElementById('second').style.display = "none";
            document.getElementById('third').style.display = "none";
            document.getElementById('first').style.display = "block";
            document.getElementById('fourth').style.display = "none";
        }


        //js for class back to person nine

        document.getElementById('back-personalNine').onclick = function () {
            document.getElementById('personal-formNine').style.display = "block";
            document.getElementById('reservation-formNine').style.display = "none";
            document.getElementById('academic-formNine').style.display = "none";
            document.getElementById('secondNine').style.display = "none";
            document.getElementById('thirdNine').style.display = "none";
            document.getElementById('firstNine').style.display = "block";
            document.getElementById('fourthNine').style.display = "none";
        }



        document.getElementById('back-academic').onclick = function () {
            document.getElementById('personal-form').style.display = "none";
            document.getElementById('reservation-form').style.display = "none";
            document.getElementById('academic-form').style.display = "block";
            document.getElementById('second').style.display = "block";
            document.getElementById('third').style.display = "none";
            document.getElementById('first').style.display = "none";
            document.getElementById('fourth').style.display = "none";
        }


        //js for class 9 academic back

        document.getElementById('back-academicNine').onclick = function () {
            document.getElementById('personal-formNine').style.display = "none";
            document.getElementById('reservation-formNine').style.display = "none";
            document.getElementById('academic-formNine').style.display = "block";
            document.getElementById('secondNine').style.display = "block";
            document.getElementById('thirdNine').style.display = "none";
            document.getElementById('firstNine').style.display = "none";
            document.getElementById('fourthNine').style.display = "none";
        }


        //Code Added for the preview Section
        document.getElementById('back-from-previous').onclick = function () {
            document.getElementById('personal-form').style.display = "block";
            document.getElementById('reservation-form').style.display = "none";
            document.getElementById('academic-form').style.display = "none";
            document.getElementById('second').style.display = "none";
            document.getElementById('third').style.display = "none";
            document.getElementById('first').style.display = "none";
            document.getElementById('fourth').style.display = "block";
            document.getElementById('PreviewForm').style.display = "none";

        }


        document.getElementById('back-from-previousNine').onclick = function () {
            document.getElementById('personal-formNine').style.display = "block";
            document.getElementById('reservation-formNine').style.display = "none";
            document.getElementById('academic-formNine').style.display = "none";
            document.getElementById('secondNine').style.display = "none";
            document.getElementById('thirdNine').style.display = "none";
            document.getElementById('firstNine').style.display = "none";
            document.getElementById('fourthNine').style.display = "block";
            document.getElementById('PreviewFormNine').style.display = "none";

        }



        document.getElementById('PreviewBtn').onclick = function () {
            var isvalid = $scope.IsValidScholarship();
            if (isvalid == true) {
                document.getElementById('reservation-form').style.display = "none";
                document.getElementById('PreviewForm').style.display = "block";
                document.getElementById('second').style.display = "none";
                document.getElementById('third').style.display = "none";
                document.getElementById('first').style.display = "none";
                document.getElementById('fourth').style.display = "block";
            }
        }

        document.getElementById('PreviewBtnNine').onclick = function () {
            var isvalid = $scope.IsValidScholarshipNine();
            if (isvalid == true) {
                document.getElementById('reservation-formNine').style.display = "none";
                document.getElementById('PreviewFormNine').style.display = "block";
                document.getElementById('secondNine').style.display = "none";
                document.getElementById('thirdNine').style.display = "none";
                document.getElementById('firstNine').style.display = "none";
                document.getElementById('fourthNine').style.display = "block";
            }
        }


    };

    //Clear Image and Signature
    $scope.ClearPhoto = function () {

        $scope.newScholarship.PhotoData = null;
        $scope.newScholarship.Photo_TMP = null;
        $scope.newScholarship.PhotoPath = null;

        $timeout(function () {
            $scope.$apply(function () {
                $scope.newScholarship.PhotoData = null;
                $scope.newScholarship.Photo_TMP = null;
                $scope.newScholarship.PhotoPath = null;
            });
        });
        $('#imgPhoto1').attr('src', '');
    };

    $scope.ClearSignature = function () {

        $scope.newScholarship.PhotoData = null;
        $scope.newScholarship.Photo_TMP = null;
        $scope.newScholarship.SignaturePath = null;
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newScholarship.PhotoData = null;
                $scope.newScholarship.Photo_TMP = null;
                $scope.newScholarship.SignaturePath = null;
            });
        });
        $('#imgSignature').attr('src', '');
    };


    $scope.ClearPhotoNine = function () {

        $scope.newScholarshipNine.PhotoData = null;
        $scope.newScholarshipNine.Photo_TMP = null;
        $scope.newScholarshipNine.PhotoPath = null;

        $timeout(function () {
            $scope.$apply(function () {
                $scope.newScholarshipNine.PhotoData = null;
                $scope.newScholarshipNine.Photo_TMP = null;
                $scope.newScholarshipNine.PhotoPath = null;
            });
        });
        $('#imgPhotoNine1').attr('src', '');
    };

    $scope.ClearSignatureNine = function () {

        $scope.newScholarshipNine.PhotoData = null;
        $scope.newScholarshipNine.Photo_TMP = null;
        $scope.newScholarshipNine.SignaturePath = null;
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newScholarshipNine.PhotoData = null;
                $scope.newScholarshipNine.Photo_TMP = null;
                $scope.newScholarshipNine.SignaturePath = null;
            });
        });
        $('#imgSignatureNine').attr('src', '');
    };


    $scope.updateDocumentNameId = function () {
        $scope.newScholarship.BC_DocumentNameId = $scope.newScholarship.BC_CertificateTypeId;
    };

    $scope.updateDocumentNameNineId = function () {
        $scope.newScholarshipNine.BC_DocumentNameId = $scope.newScholarshipNine.BC_CertificateTypeId;
    };
    //Ends

    $scope.AddPriorityDetails = function (ind) {

        if ($scope.newScholarship.SchoolPriorityListColl) {
            for (var i = 0; i < $scope.newScholarship.SchoolPriorityListColl.length; i++) {
                var rs = $scope.newScholarship.SchoolPriorityListColl[i];
                if (!rs.SchoolId) {
                    Swal.fire('Please select School before adding a new row.');
                    return;
                }
            }

            var currentEntry = $scope.newScholarship.SchoolPriorityListColl[ind];
            // Check if the current entry has both SchoolId and PriorityId selected
            if (currentEntry.SchoolId) {
                if ($scope.newScholarship.SchoolPriorityListColl.length > ind + 1) {
                    $scope.newScholarship.SchoolPriorityListColl.splice(ind + 1, 0, {
                        ClassName: '',
                        SchoolId: null,

                    });
                } else {
                    $scope.newScholarship.SchoolPriorityListColl.push({
                        ClassName: '',
                        SchoolId: null,

                    });
                }
            } else {
                Swal.fire('Please select School before adding a new row.');
            }
        }
        $scope.CheckAlreadySelected(ind + 1);
    };

    $scope.delPriorityDetails = function (ind) {
        if ($scope.newScholarship.SchoolPriorityListColl) {
            if ($scope.newScholarship.SchoolPriorityListColl.length > 1) {
                $scope.newScholarship.SchoolPriorityListColl.splice(ind, 1);
            }
        }
        $scope.CheckAlreadySelected(-1);
    };

    $scope.AddPriorityDetailsNine = function (ind) {

        if ($scope.newScholarshipNine.SchoolPriorityListColl) {
            for (var i = 0; i < $scope.newScholarshipNine.SchoolPriorityListColl.length; i++) {
                var rs = $scope.newScholarshipNine.SchoolPriorityListColl[i];
                if (!rs.SchoolId) {
                    Swal.fire('Please select School before adding a new row.');
                    return;
                }
            }

            var currentEntry = $scope.newScholarshipNine.SchoolPriorityListColl[ind];
            // Check if the current entry has both SchoolId and PriorityId selected
            if (currentEntry.SchoolId) {
                if ($scope.newScholarshipNine.SchoolPriorityListColl.length > ind + 1) {
                    $scope.newScholarshipNine.SchoolPriorityListColl.splice(ind + 1, 0, {
                        ClassName: '',
                        SchoolId: null,

                    });
                } else {
                    $scope.newScholarshipNine.SchoolPriorityListColl.push({
                        ClassName: '',
                        SchoolId: null,

                    });
                }
            } else {
                Swal.fire('Please select School before adding a new row.');
            }
        }
        $scope.CheckAlreadySelectedNine(ind + 1);
    };

    $scope.delPriorityDetailsNine = function (ind) {
        if ($scope.newScholarshipNine.SchoolPriorityListColl) {
            if ($scope.newScholarshipNine.SchoolPriorityListColl.length > 1) {
                $scope.newScholarshipNine.SchoolPriorityListColl.splice(ind, 1);
            }
        }
        $scope.CheckAlreadySelectedNine(-1);
    };



    $scope.AddReservationGroup = function () {
        if (!$scope.newScholarship.ReservationGroupList) {
            $scope.newScholarship.ReservationGroupList = [];
        }

        var oldDataColl = mx(angular.copy($scope.newScholarship.ReservationGroupList));

        // Clear existing list and add newly selected groups
        $scope.newScholarship.ReservationGroupList = [];

        var rname = '';
        // Iterate through selected ReservationGroupId
        angular.forEach($scope.newScholarship.ReservationGroupId, function (selectedId) {
            var selectedGroup = $scope.ReservationGroupList.find(function (group) {
                return group.TranId === selectedId;
            });

            if (selectedGroup && selectedGroup.TranId != 0) {

                if (rname.length > 0)
                    rname = rname + ',';

                rname = rname + selectedGroup.Name;

                var newGRP = {
                    Name: selectedGroup.Name,
                    ReservationGroupName: selectedGroup.Name,
                    ReservationGroupId: selectedGroup.TranId,
                    ConcernedAuthorityId: null,
                    GrpCerti_IssuedDistrictId: null,
                    GrpCerti_IssuedLocalLevelId: null,
                    GrpCertiIssue_WardNo: '',
                    GrpCertiIssue_ToleStreet: '',
                    GroupWiseCerti_IssuedDate_TMP: null,
                    GroupWiseCerti_RefNo: '',
                    GroupWiseCerti_Path_TMP: '',
                    GroupWiseCerti_PathData: null,
                    GrpCerti_IssuedDistrict: '',
                    GrpCerti_IssuedLocalLevel: '',
                    AuthorityList: [],
                };

                var findOld = oldDataColl.firstOrDefault(p1 => p1.ReservationGroupId == selectedGroup.TranId);
                if (findOld) {
                    newGRP = findOld;
                    newGRP.AuthorityList = [];
                }


                /*
                    1	जिल्ला प्रशासन कार्यालय
                    2	जनजाति उत्थान राष्ट्रिय प्रतिष्ठान
                    3	राष्ट्रिय दलित आयोग
                    4	स्थानीय तहबाट प्रदान गरिएको अपाङ्गता परिचयपत्र
                    5	सम्बन्धित स्थानीय तहबाट हाल स्थायी बसोबास गरिरहेको भनी सिफारिसपत्र
                    6	आदीवासी जनजाती आयोग
                    7	मधेसी आयोग
                    8	गृह मन्त्रालय
                    9	स्थानीय तहबाट प्रदान गरिएको विपन्नता परिचयपत्र
                 */

                if (selectedGroup.TranId == 1) //2	आदिवासी / जनजाति
                {
                    angular.forEach($scope.AuthorityList, function (al) {
                        if (al.AuthorityId == 1 || al.AuthorityId == 9) {
                            newGRP.AuthorityList.push(al);
                        }
                    });
                } else if (selectedGroup.TranId == 2) //3	मधेशी
                {
                    angular.forEach($scope.AuthorityList, function (al) {
                        if (al.AuthorityId == 1 || al.AuthorityId == 2 || al.AuthorityId == 6) {
                            newGRP.AuthorityList.push(al);
                        }
                    });
                } else if (selectedGroup.TranId == 3) //4	खस आर्य
                {
                    angular.forEach($scope.AuthorityList, function (al) {
                        if (al.AuthorityId == 1 || al.AuthorityId == 3) {
                            newGRP.AuthorityList.push(al);
                        }
                    });

                    
                } 
                else if (selectedGroup.TranId == 4) //5	 
                {
                    angular.forEach($scope.AuthorityList, function (al) {
                        if (al.AuthorityId == 4) {
                            newGRP.AuthorityList.push(al);
                        }
                    });
                    
                } else if (selectedGroup.TranId == 5 || selectedGroup.TranId == 6 || selectedGroup.TranId == 7 || selectedGroup.TranId == 8)  //6
                {
                    angular.forEach($scope.AuthorityList, function (al) {
                        if (al.AuthorityId == 1 || al.AuthorityId == 8) {
                            newGRP.AuthorityList.push(al);
                        }
                    });

                }
 
                $scope.newScholarship.ReservationGroupList.push(newGRP);
            }
        });

        $scope.newScholarship.ReservationGroupName = rname;
    };

    $scope.delReservationGroup = function (ind) {
        if ($scope.newScholarship.ReservationGroupList) {
            if ($scope.newScholarship.ReservationGroupList.length > 1) {
                $scope.newScholarship.ReservationGroupList.splice(ind, 1);
            }
        }
    };

    $scope.AddReservationGroupNine = function () {
        if (!$scope.newScholarshipNine.ReservationGroupList) {
            $scope.newScholarshipNine.ReservationGroupList = [];
        }

        var oldDataColl = mx(angular.copy($scope.newScholarshipNine.ReservationGroupList));

        // Clear existing list and add newly selected groups
        $scope.newScholarshipNine.ReservationGroupList = [];

        var rname = '';
        // Iterate through selected ReservationGroupId
        angular.forEach($scope.newScholarshipNine.ReservationGroupId, function (selectedId) {
            var selectedGroup = $scope.ReservationGroupList.find(function (group) {
                return group.TranId === selectedId;
            });

            if (selectedGroup && selectedGroup.TranId != 0) {

                if (rname.length > 0)
                    rname = rname + ',';

                rname = rname + selectedGroup.Name;

                var newGRP = {
                    Name: selectedGroup.Name,
                    ReservationGroupName: selectedGroup.Name,
                    ReservationGroupId: selectedGroup.TranId,
                    ConcernedAuthorityId: null,
                    GrpCerti_IssuedDistrictId: null,
                    GrpCerti_IssuedLocalLevelId: null,
                    GrpCertiIssue_WardNo: '',
                    GrpCertiIssue_ToleStreet: '',
                    GroupWiseCerti_IssuedDate_TMP: null,
                    GroupWiseCerti_RefNo: '',
                    GroupWiseCerti_Path_TMP: '',
                    GroupWiseCerti_PathData: null,
                    GrpCerti_IssuedDistrict: '',
                    GrpCerti_IssuedLocalLevel: '',
                    AuthorityList: [],
                };

                var findOld = oldDataColl.firstOrDefault(p1 => p1.ReservationGroupId == selectedGroup.TranId);
                if (findOld) {
                    newGRP = findOld;
                    newGRP.AuthorityList = [];
                }


                /*
                1	जिल्ला प्रशासन कार्यालय
                2	जनजाति उत्थान राष्ट्रिय प्रतिष्ठान
                3	राष्ट्रिय दलित आयोग
                4	स्थानीय तहबाट प्रदान गरिएको अपाङ्गता परिचयपत्र
                5	सम्बन्धित स्थानीय तहबाट हाल स्थायी बसोबास गरिरहेको भनी सिफारिसपत्र
                6	आदीवासी जनजाती आयोग
                7	मधेसी आयोग
                8	गृह मन्त्रालय
                9	स्थानीय तहबाट प्रदान गरिएको विपन्नता परिचयपत्र
             */

                if (selectedGroup.TranId == 1) //2	आदिवासी / जनजाति
                {
                    angular.forEach($scope.AuthorityList, function (al) {
                        if (al.AuthorityId == 1 || al.AuthorityId == 9) {
                            newGRP.AuthorityList.push(al);
                        }
                    });
                } else if (selectedGroup.TranId == 2) //3	मधेशी
                {
                    angular.forEach($scope.AuthorityList, function (al) {
                        if (al.AuthorityId == 1 || al.AuthorityId == 2 || al.AuthorityId == 6) {
                            newGRP.AuthorityList.push(al);
                        }
                    });
                } else if (selectedGroup.TranId == 3) //4	खस आर्य
                {
                    angular.forEach($scope.AuthorityList, function (al) {
                        if (al.AuthorityId == 1 || al.AuthorityId == 3) {
                            newGRP.AuthorityList.push(al);
                        }
                    });


                }
                else if (selectedGroup.TranId == 4) //5	 
                {
                    angular.forEach($scope.AuthorityList, function (al) {
                        if (al.AuthorityId == 4) {
                            newGRP.AuthorityList.push(al);
                        }
                    }); 
                } else if (selectedGroup.TranId == 5 || selectedGroup.TranId == 6 || selectedGroup.TranId == 7 || selectedGroup.TranId == 8)  //6
                {
                    angular.forEach($scope.AuthorityList, function (al) {
                        if (al.AuthorityId == 1 || al.AuthorityId == 8) {
                            newGRP.AuthorityList.push(al);
                        }
                    });

                }

                $scope.newScholarshipNine.ReservationGroupList.push(newGRP);
            }
        });

        $scope.newScholarshipNine.ReservationGroupName = rname;
    };


    $scope.delReservationGroupNine = function (ind) {
        if ($scope.newScholarshipNine.ReservationGroupList) {
            if ($scope.newScholarshipNine.ReservationGroupList.length > 1) {
                $scope.newScholarshipNine.ReservationGroupList.splice(ind, 1);
            }
        }
    };


    $scope.isValidateSEEYEar = function () {
        if (!$scope.newDet.SEEYearId) {
            Swal.fire('Please Select SEE Appeared Year');
            return false;
        }
        return true;
    };

    $scope.isValidateEligibility = function () {
        if (!$scope.newDet.IsEligible) {
            Swal.fire('Have you read all the details. If you are eligible accept the terms and conditions to apply for scholarship');
            return false;
        }
        return true;
    };

    $scope.isValidateFirstName = function () {
        if ($scope.newScholarship.FirstName.isEmpty()) {
            Swal.fire('Please ! Enter First Name');
            return false;
        }
        return true;
    }


    $scope.isValidateSEEYEarNine = function () {
        if (!$scope.newDet.SEEYearId) {
            Swal.fire('Please Select SEE Appeared Year');
            return false;
        }
        return true;
    };

    $scope.isValidateEligibilityNine = function () {
        if (!$scope.newDet.IsEligible) {
            Swal.fire('Have you read all the details. If you are eligible accept the terms and conditions to apply for scholarship');
            return false;
        }
        return true;
    };

    $scope.isValidateLastName = function () {
        if ($scope.newScholarship.LastName.isEmpty()) {
            Swal.fire('Please ! Enter Last Name');
            return false;
        }
        return true;
    }

    $scope.isValidateGender = function () {
        if (!$scope.newScholarship.Gender) {
            Swal.fire('Please Select Gender');
            return false;
        }
        return true;
    };

    $scope.isValidateSymbolNo = function () {
        if ($scope.newScholarship.SEESymbolNo.isEmpty()) {
            Swal.fire('Please ! Enter SEE SymbolNo');
            return false;
        }
        return true;
    }
    $scope.isValidateAlphabet = function () {
        if ($scope.newScholarship.Alphabet.isEmpty()) {
            Swal.fire('Please ! Enter Alphabet');
            return false;
        }
        return true;
    }

    //Validate Email
    $scope.isValidateEmail = function () {
        if (!$scope.newScholarship.Email) {
            Swal.fire('Please ! Enter Email Address');
            return false;
        }

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        var ve = emailRegex.test($scope.newScholarship.Email);

        if (ve == false) {
            Swal.fire('Please ! Enter Valid Email Address');
            return false;
        }

        return true;
    }


    $scope.isValidateFirstNameNine = function () {
        if ($scope.newScholarshipNine.FirstName.isEmpty()) {
            Swal.fire('Please ! Enter First Name');
            return false;
        }
        return true;
    }

    $scope.isValidateLastNameNine = function () {
        if ($scope.newScholarshipNine.LastName.isEmpty()) {
            Swal.fire('Please ! Enter Last Name');
            return false;
        }
        return true;
    }

    $scope.isValidateGenderNine = function () {
        if (!$scope.newScholarshipNine.Gender) {
            Swal.fire('Please Select Gender');
            return false;
        }
        return true;
    };

    $scope.isValidateSymbolNoNine = function () {
        if ($scope.newScholarshipNine.SEESymbolNo.isEmpty()) {
            Swal.fire('Please ! Enter SEE SymbolNo');
            return false;
        }
        return true;
    }
    $scope.isValidateAlphabetNine = function () {
        //if ($scope.newScholarshipNine.Alphabet.isEmpty()) {
        //    Swal.fire('Please ! Enter Alphabet');
        //    return false;
        //}
        return true;
    }

    //Validate Email
    $scope.isValidateEmailNine = function () {
        if (!$scope.newScholarshipNine.Email) {
            Swal.fire('Please ! Enter Email Address');
            return false;
        }

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        var ve = emailRegex.test($scope.newScholarshipNine.Email);

        if (ve == false) {
            Swal.fire('Please ! Enter Valid Email Address');
            return false;
        }

        return true;
    }




    $scope.isValidateGPA = function () {
        //var gpa = $scope.newScholarship.GPA;
        //if (!gpa || gpa.trim() === "") {
        //	Swal.fire('Please enter GPA');
        //	return false;
        //}
        //var gpaRegex = /^\d+(\.\d{1,2})?$/; // Regular expression for GPA with up to two decimal places
        //if (!gpaRegex.test(gpa)) {
        //	Swal.fire('Please enter a valid GPA with up to two decimal places');
        //	return false;
        //}
        //var gpaFloat = parseFloat(gpa);
        //if (isNaN(gpaFloat) || gpaFloat < 1.6 || gpaFloat > 4) {
        //	Swal.fire('Please enter a valid GPA between 1.6 and 4');
        //	return false;
        //}

        return true;
    };

    $scope.isValidateGPANine = function () {
        //var gpa = $scope.newScholarshipNine.GPA;
        //if (!gpa || gpa.trim() === "") {
        //	Swal.fire('Please enter GPA');
        //	return false;
        //}
        //var gpaRegex = /^\d+(\.\d{1,2})?$/; // Regular expression for GPA with up to two decimal places
        //if (!gpaRegex.test(gpa)) {
        //	Swal.fire('Please enter a valid GPA with up to two decimal places');
        //	return false;
        //}
        //var gpaFloat = parseFloat(gpa);
        //if (isNaN(gpaFloat) || gpaFloat < 1.6 || gpaFloat > 4) {
        //	Swal.fire('Please enter a valid GPA between 1.6 and 4');
        //	return false;
        //}

        return true;
    };



    $scope.isValidateMobileNo = function () {
        var mobileNo = $scope.newScholarship.MobileNo;

        //if (!mobileNo || mobileNo.trim() === "") {
        //	Swal.fire('Please enter MobileNo');
        //	return false;
        //}
        var mobileNoPattern = /^\d{10}$/;
        if (!mobileNoPattern.test(mobileNo)) {
            Swal.fire('Please enter a valid 10-digit MobileNo');
            return false;
        }
        return true;
    }

    $scope.isValidateFatherFName = function () {
        if ($scope.newScholarship.F_FirstName.isEmpty()) {
            Swal.fire('Please ! Enter Father Firstname');
            return false;
        }
        return true;
    }

    $scope.isValidateFatherLName = function () {
        if ($scope.newScholarship.F_LastName.isEmpty()) {
            Swal.fire('Please ! Enter Father Lastname');
            return false;
        }
        return true;
    }

    $scope.isValidateMotherFName = function () {
        if ($scope.newScholarship.M_FirstName.isEmpty()) {
            Swal.fire('Please ! Enter Mother Firstname');
            return false;
        }
        return true;
    }
    $scope.isValidateMotherLName = function () {
        if ($scope.newScholarship.M_LastName.isEmpty()) {
            Swal.fire('Please ! Enter Mother Lastname');
            return false;
        }
        return true;
    }
    $scope.isValidateGrandfatherFName = function () {
        if ($scope.newScholarship.GF_FirstName.isEmpty()) {
            Swal.fire('Please ! Enter Grandfather Firstname');
            return false;
        }
        return true;
    }
    $scope.isValidateGrandfatherLName = function () {
        if ($scope.newScholarship.GF_LastName.isEmpty()) {
            Swal.fire('Please ! Enter Grandfather Lastname');
            return false;
        }
        return true;
    }


    $scope.isValidatePProvince = function () {
        if (!$scope.newScholarship.P_ProvinceId) {
            Swal.fire('Please ! Select Permanent Address Province');
            return false;
        }
        return true;
    }

    $scope.isValidatePDistrict = function () {
        if (!$scope.newScholarship.P_DistrictId) {
            Swal.fire('Please ! Select Permanent Address District');
            return false;
        }
        return true;
    }

    $scope.isValidatePLocalLevel = function () {
        if (!$scope.newScholarship.P_LocalLevelId) {
            Swal.fire('Please ! Select Permanent Address Local Level');
            return false;
        }
        return true;
    }

    $scope.isValidatePWardNo = function () {
        if (!$scope.newScholarship.P_WardNo) {
            Swal.fire('Please ! Enter Permanent Address Ward No');
            return false;
        }
        return true;
    }



    $scope.isValidateTempProvince = function () {
        if (!$scope.newScholarship.Temp_ProvinceId) {
            Swal.fire('Please ! Select Temporary Address Province');
            return false;
        }
        return true;
    }

    $scope.isValidateTempDistrict = function () {
        if (!$scope.newScholarship.Temp_DistrictId) {
            Swal.fire('Please ! Select Temporary Address District');
            return false;
        }
        return true;
    }

    $scope.isValidateTempLocalLevel = function () {
        if (!$scope.newScholarship.Temp_LocalLevelId) {
            Swal.fire('Please ! Select Temporary Address Local Level');
            return false;
        }
        return true;
    }

    $scope.isValidateTempWardNo = function () {
        if (!$scope.newScholarship.Temp_WardNo) {
            Swal.fire('Please ! Enter Temporary Address Ward No');
            return false;
        }
        return true;
    }

    $scope.isValidateCertificateType = function () {
        if (!$scope.newScholarship.BC_CertificateTypeId) {
            Swal.fire('Please ! Select Certificate Type');
            return false;
        }
        return true;
    }
    $scope.isValidateCertificateNumber = function () {
        if ($scope.newScholarship.BC_CertificateNo.isEmpty()) {
            Swal.fire('Please ! Enter Certificate No');
            return false;
        }
        return true;
    }

    $scope.isValidateCertificateDate = function () {
        if (!$scope.newScholarship.BC_IssuedDate_TMP) {
            Swal.fire('Please ! Enter Certificate Issue Date');
            return false;
        }
        return true;
    }

    $scope.isValidateCertificateIssueDistrict = function () {
        if (!$scope.newScholarship.BC_IssuedDistrictId) {
            Swal.fire('Please ! Select Certificate Issue District');
            return false;
        }
        return true;
    }


    $scope.isValidateCertificateName = function () {
        if (!$scope.newScholarship.BC_DocumentNameId) {
            Swal.fire('Please ! Select Certificate Name to upload');
            return false;
        }
        return true;
    }


    $scope.isValidateMobileNoNine = function () {
        var mobileNo = $scope.newScholarshipNine.MobileNo;

        //if (!mobileNo || mobileNo.trim() === "") {
        //	Swal.fire('Please enter MobileNo');
        //	return false;
        //}
        var mobileNoPattern = /^\d{10}$/;
        if (!mobileNoPattern.test(mobileNo)) {
            Swal.fire('Please enter a valid 10-digit MobileNo');
            return false;
        }
        return true;
    }

    $scope.isValidateFatherFNameNine = function () {
        if ($scope.newScholarshipNine.F_FirstName.isEmpty()) {
            Swal.fire('Please ! Enter Father Firstname');
            return false;
        }
        return true;
    }

    $scope.isValidateFatherLNameNine = function () {
        if ($scope.newScholarshipNine.F_LastName.isEmpty()) {
            Swal.fire('Please ! Enter Father Lastname');
            return false;
        }
        return true;
    }

    $scope.isValidateMotherFNameNine = function () {
        if ($scope.newScholarshipNine.M_FirstName.isEmpty()) {
            Swal.fire('Please ! Enter Mother Firstname');
            return false;
        }
        return true;
    }
    $scope.isValidateMotherLNameNine = function () {
        if ($scope.newScholarshipNine.M_LastName.isEmpty()) {
            Swal.fire('Please ! Enter Mother Lastname');
            return false;
        }
        return true;
    }
    $scope.isValidateGrandfatherFNameNine = function () {
        if ($scope.newScholarshipNine.GF_FirstName.isEmpty()) {
            Swal.fire('Please ! Enter Grandfather Firstname');
            return false;
        }
        return true;
    }
    $scope.isValidateGrandfatherLNameNine = function () {
        if ($scope.newScholarshipNine.GF_LastName.isEmpty()) {
            Swal.fire('Please ! Enter Grandfather Lastname');
            return false;
        }
        return true;
    }


    $scope.isValidatePProvinceNine = function () {
        if (!$scope.newScholarshipNine.P_ProvinceId) {
            Swal.fire('Please ! Select Permanent Address Province');
            return false;
        }
        return true;
    }

    $scope.isValidatePDistrictNine = function () {
        if (!$scope.newScholarshipNine.P_DistrictId) {
            Swal.fire('Please ! Select Permanent Address District');
            return false;
        }
        return true;
    }

    $scope.isValidatePLocalLevelNine = function () {
        if (!$scope.newScholarshipNine.P_LocalLevelId) {
            Swal.fire('Please ! Select Permanent Address Local Level');
            return false;
        }
        return true;
    }

    $scope.isValidatePWardNoNine = function () {
        if (!$scope.newScholarshipNine.P_WardNo) {
            Swal.fire('Please ! Enter Permanent Address Ward No');
            return false;
        }
        return true;
    }



    $scope.isValidateTempProvinceNine = function () {
        if (!$scope.newScholarshipNine.Temp_ProvinceId) {
            Swal.fire('Please ! Select Temporary Address Province');
            return false;
        }
        return true;
    }

    $scope.isValidateTempDistrictNine = function () {
        if (!$scope.newScholarshipNine.Temp_DistrictId) {
            Swal.fire('Please ! Select Temporary Address District');
            return false;
        }
        return true;
    }

    $scope.isValidateTempLocalLevelNine = function () {
        if (!$scope.newScholarshipNine.Temp_LocalLevelId) {
            Swal.fire('Please ! Select Temporary Address Local Level');
            return false;
        }
        return true;
    }

    $scope.isValidateTempWardNoNine = function () {
        if (!$scope.newScholarshipNine.Temp_WardNo) {
            Swal.fire('Please ! Enter Temporary Address Ward No');
            return false;
        }
        return true;
    }

    $scope.isValidateCertificateTypeNine = function () {
        if (!$scope.newScholarshipNine.BC_CertificateTypeId) {
            Swal.fire('Please ! Select Certificate Type');
            return false;
        }
        return true;
    }
    $scope.isValidateCertificateNumberNine = function () {
        if ($scope.newScholarshipNine.BC_CertificateNo.isEmpty()) {
            Swal.fire('Please ! Enter Certificate No');
            return false;
        }
        return true;
    }

    $scope.isValidateCertificateDateNine = function () {
        if (!$scope.newScholarshipNine.BC_IssuedDate_TMP) {
            Swal.fire('Please ! Enter Certificate Issue Date');
            return false;
        }
        return true;
    }

    $scope.isValidateCertificateIssueDistrictNine = function () {
        if (!$scope.newScholarshipNine.BC_IssuedDistrictId) {
            Swal.fire('Please ! Select Certificate Issue District');
            return false;
        }
        return true;
    }


    $scope.isValidateCertificateNameNine = function () {
        if (!$scope.newScholarshipNine.BC_DocumentNameId) {
            Swal.fire('Please ! Select Certificate Name to upload');
            return false;
        }
        return true;
    }

    $scope.isValidateCtznFrontUploadNine = function () {
        if ($scope.newScholarshipNine.BC_DocumentNameId == 1 && !$scope.newScholarshipNine.CtznshipFront_FilePath_TMP && !$scope.newScholarshipNine.CtznshipFront_FilePath) {
            Swal.fire('Please ! Upoad Citizenship Front Side ');
            return false;
        }
        return true;
    }

    $scope.isValidateCtznBackUploadNine = function () {
        if ($scope.newScholarshipNine.BC_DocumentNameId == 1 && !$scope.newScholarshipNine.CtznshipBack_FilePath_TMP && !$scope.newScholarshipNine.CtznshipBack_FilePath) {
            Swal.fire('Please ! Upoad Citizenship Back Side ');
            return false;
        }
        return true;
    }


    $scope.isValidateBirthCertiUploadNine = function () {
        if ($scope.newScholarshipNine.BC_DocumentNameId == 2 && !$scope.newScholarshipNine.BC_FilePath_TMP && !$scope.newScholarshipNine.BC_FilePath) {
            Swal.fire('Please ! Upoad Birth Certificate ');
            return false;
        }
        return true;
    }


    //Added By Suresh on 7th Ashar	
    $scope.isValidateCtznFrontUpload = function () {
        if ($scope.newScholarship.BC_DocumentNameId == 1 && !$scope.newScholarship.CtznshipFront_FilePath_TMP && !$scope.newScholarship.CtznshipFront_FilePath) {
            Swal.fire('Please ! Upoad Citizenship Front Side ');
            return false;
        }
        return true;
    }

    $scope.isValidateCtznBackUpload = function () {
        if ($scope.newScholarship.BC_DocumentNameId == 1 && !$scope.newScholarship.CtznshipBack_FilePath_TMP && !$scope.newScholarship.CtznshipBack_FilePath) {
            Swal.fire('Please ! Upoad Citizenship Back Side ');
            return false;
        }
        return true;
    }


    $scope.isValidateBirthCertiUpload = function () {
        if ($scope.newScholarship.BC_DocumentNameId == 2 && !$scope.newScholarship.BC_FilePath_TMP && !$scope.newScholarship.BC_FilePath) {
            Swal.fire('Please ! Upoad Birth Certificate ');
            return false;
        }
        return true;
    }

    //Ends

    //Photo Validation
    $scope.isValidateProfilePhoto = function () {
        if (!$scope.newScholarship.Photo_TMP && !$scope.newScholarship.PhotoPath) {
            Swal.fire('Please ! Upoad Photo ');
            return false;
        }
        return true;
    }

    $scope.isValidateSignature = function () {
        if (!$scope.newScholarship.Signature_TMP && !$scope.newScholarship.SignaturePath) {
            Swal.fire('Please ! Upoad Signature ');
            return false;
        }
        return true;
    }

    //Ends

    //Second Tab: Academic Tab Validation
    $scope.isValidateEquivalentBoard = function () {
        if (!$scope.newScholarship.EquivalentBoardId) {
            Swal.fire('Please ! Select Equivalent Board');
            return false;
        }
        return true;
    }

    $scope.isValidateSchoolName = function () {
        if ($scope.newScholarship.SchoolName.isEmpty()) {
            Swal.fire('Please ! Enter School Name');
            return false;
        }
        return true;
    }


    $scope.isValidateSchoolNameNine = function () {
        if ($scope.newScholarshipNine.SchoolName.isEmpty()) {
            Swal.fire('Please ! Enter School Name');
            return false;
        }
        return true;
    }



    $scope.isValidateCertificateissueDate = function () {
        if (!$scope.newScholarship.Certi_IssuedDate_TMP) {
            Swal.fire('Please ! Enter Certificate Issue Date of School');
            return false;
        }
        return true;
    }

    $scope.isValidateCertificateissueDateNine = function () {
        if (!$scope.newScholarshipNine.Certi_IssuedDate_TMP) {
            Swal.fire('Please ! Enter Certificate Issue Date of School');
            return false;
        }
        return true;
    }

    $scope.isValidateSchoolType = function () {
        if (!$scope.newScholarship.SchoolTypeId) {
            Swal.fire('Please ! Select School Type');
            return false;
        }
        return true;
    }
    $scope.isValidateSchoolTypeNine = function () {
        if (!$scope.newScholarshipNine.SchoolTypeId) {
            Swal.fire('Please ! Select School Type');
            return false;
        }
        return true;
    }

    $scope.isValidateSchoolDistrict = function () {
        if (!$scope.newScholarship.SchoolDistrictId) {
            Swal.fire('Please ! Select School District');
            return false;
        }
        return true;
    }


    $scope.isValidateSchoolDistrictNine = function () {
        if (!$scope.newScholarshipNine.SchoolDistrictId) {
            Swal.fire('Please ! Select School District');
            return false;
        }
        return true;
    }


    $scope.isValidateSchoolLocalLevel = function () {
        if (!$scope.newScholarship.SchoolLocalLevelId) {
            Swal.fire('Please ! Select School Local Level');
            return false;
        }
        return true;
    }



    $scope.isValidateAppliedSubject = function () {
        if (!$scope.newScholarship.AppliedSubjectId) {
            Swal.fire('Please ! Enter Applied Subject');
            return false;
        }
        return true;
    }

    $scope.isValidateSchoolPriority = function () {
        if (!$scope.newScholarship.SchoolPriorityListColl || $scope.newScholarship.SchoolPriorityListColl.length == 0) {
            Swal.fire('Please ! Select Priority School Name');
            return false;
        }
        var c = 0;
        $scope.newScholarship.SchoolPriorityListColl.forEach(function (sc) {
            if (sc.SchoolId > 0)
                c++;
        });

        if (c == 0) {
            Swal.fire('Please ! Select Priority School Name');
            return false;
        }

        return true;
    }

    //Class 9 academic validation
    $scope.isValidateEquivalentBoardNine = function () {
        if (!$scope.newScholarshipNine.EquivalentBoardId) {
            Swal.fire('Please ! Select Equivalent Board');
            return false;
        }
        return true;
    }
    //isValidateBSLocalLevelNine
    $scope.isValidateBSLocalLevelNine = function () {
        if ($scope.newScholarshipNine.BC_CertificateTypeId == 2) {
            if (!$scope.newScholarshipNine.BC_IssuedLocalLevelId) {
                Swal.fire('Please ! Select Local Level of Birth Certificate');
                return false;
            }
            return true;
        }

        return true;

    }


    //third tab Validation Starts
    $scope.IsValidScholarshipNine = function () {
        if (!$scope.newScholarshipNine.ScholarshipTypeId) {
            Swal.fire('Please ! Select Scholarship Type');
            return false;
        }

        if ($scope.newScholarshipNine.SchoolTypeId == 1 && !$scope.newScholarshipNine.GovSchoolCerti_IssuedDate_TMP) {
            Swal.fire('Please ! Select Government Certificate Issue Date');
            return false;
        }

        //if ($scope.newScholarshipNine.SchoolTypeId == 1 && $scope.newScholarshipNine.GovSchoolCerti_RefNo.isEmpty()) {
        //	Swal.fire('Please ! Enter Goverment Certificate Ref. No');
        //	return false;
        //}

        if ($scope.newScholarshipNine.SchoolTypeId == 1 && !$scope.newScholarshipNine.GovSchoolCertiPath_TMP && !$scope.newScholarshipNine.GovSchoolCertiPath) {
            Swal.fire('Please ! Upload Goverment Certificate ');
            return false;
        }

        if ((($scope.newScholarshipNine.SchoolTypeId == 1 && $scope.newScholarshipNine.ScholarshipTypeId == 5) ||
            ($scope.newScholarshipNine.SchoolTypeId == 1 && $scope.newScholarshipNine.ScholarshipTypeId == 7) ||
            ($scope.newScholarshipNine.SchoolTypeId == 1 && $scope.newScholarshipNine.ScholarshipTypeId == 8)) &&
            !$scope.newScholarshipNine.ReservationGroupId) {
            Swal.fire('Please ! Select Reservation Group');
            return false;
        }
        //Govern School Certi Ends
        if ($scope.newScholarshipNine.SchoolTypeId == 1) {
            if ($scope.newScholarshipNine.ScholarshipTypeId == 6 || $scope.newScholarshipNine.ScholarshipTypeId == 7) {
                //if (!$scope.newScholarshipNine.LandfilDistrictId) {
                //    Swal.fire('Please ! Select Landfill Site District');
                //    return false;
                //}
                //if (!$scope.newScholarshipNine.LandfillLocalLevelId) {
                //    Swal.fire('Please ! Select Landfill Site LocalLevel');
                //    return false;
                //}
                //if (!$scope.newScholarshipNine.LandfillWardNo) {
                //    Swal.fire('Please ! Select Landfill Site WardNo');
                //    return false;
                //}
                //if (!$scope.newScholarshipNine.LandFill_IssuedDate_TMP) {
                //    Swal.fire('Please ! Select Landfill Site Certificate Issued Date');
                //    return false;
                //}
                //if (!$scope.newScholarshipNine.LandFill_RefNo) {
                //    Swal.fire('Please ! Select Landfill Site Certificate Reference No');
                //    return false;
                //}
                //if (!$scope.newScholarshipNine.LandFillDocPath_TMP) {
                //    Swal.fire('Please ! Upload Landfill Site Certificate');
                //    return false;
                //}
            }
        }


        //When the school is private and the scholarship is either Landfill site or General + Landfill Site
        if ($scope.newScholarshipNine.SchoolTypeId == 2) {
            if ($scope.newScholarshipNine.ScholarshipTypeId == 3 || $scope.newScholarshipNine.ScholarshipTypeId == 6) {
                //if (!$scope.newScholarshipNine.LandfilDistrictId) {
                //    Swal.fire('Please ! Select Landfill Site District');
                //    return false;
                //}
                //if (!$scope.newScholarshipNine.LandfillLocalLevelId) {
                //    Swal.fire('Please ! Select Landfill Site LocalLevel');
                //    return false;
                //}
                //if (!$scope.newScholarshipNine.LandfillWardNo) {
                //    Swal.fire('Please ! Select Landfill Site WardNo');
                //    return false;
                //}
                //if (!$scope.newScholarshipNine.LandFill_IssuedDate_TMP) {
                //    Swal.fire('Please ! Select Landfill Site Certificate Issued Date');
                //    return false;
                //}
                //if (!$scope.newScholarshipNine.LandFill_RefNo) {
                //    Swal.fire('Please ! Select Landfill Site Certificate Reference No');
                //    return false;
                //}
                //if (!$scope.newScholarshipNine.LandFillDocPath_TMP) {
                //    Swal.fire('Please ! Upload Landfill Site Certificate');
                //    return false;
                //}
            }
        }


        //When the School is private and scholarship is Landfillsite


  

        if ($scope.newScholarshipNine.ReservationGroupList) {
            for (var i = 0; i < $scope.newScholarshipNine.ReservationGroupList.length; i++) {
                var rs = $scope.newScholarshipNine.ReservationGroupList[i];
                if (!rs.ReservationGroupId) {
                    Swal.fire("Please ! Select Reservation Group Name");
                    return false;
                }

                if (!rs.ConcernedAuthorityId) {
                    Swal.fire("Please ! Select Reservation Concerned Authority");
                    return false;
                }

                if (isEmptyObj(rs.GroupWiseCerti_Path) == true && isEmptyObj(rs.GroupWiseCerti_Path_TMP) == true && !rs.GroupWiseCerti_PathData) {
                    Swal.fire("Please ! Upload Related Document of Reservation ");
                    return false;
                }

                if (rs.ConcernedAuthorityId == 1 || rs.ConcernedAuthorityId == 6) {
                    if (!rs.GrpCerti_IssuedDistrictId) {
                        Swal.fire("Please ! Select District");
                        return false;
                    }
                }

                if (rs.ConcernedAuthorityId == 6) {
                    if (!rs.GrpCerti_IssuedLocalLevelId) {
                        Swal.fire("Please ! Select Local Level");
                        return false;
                    }
                }

                if (rs.ConcernedAuthorityId != 5) {
                    if (!rs.GroupWiseCerti_IssuedDateDet) {
                        Swal.fire("Please ! Enter Issue Date");
                        return false;
                    }

                    if (isEmptyObj(rs.GroupWiseCerti_RefNo) == true) {
                        Swal.fire("Please ! Enter Ref. No.");
                        return false;
                    }
                }
            }

        }

        return true;
    }


    $scope.IsValidAgreeCaptchaNine = function () {
        if (!$scope.newScholarshipNine.IsAgree) {
            Swal.fire('Are you sure you want to submit the form? If you have thoroughly reviewed it, please accept the terms and conditions. ');
            return false;
        }

        if (!$scope.newScholarshipNine.ReCaptcha) {
            Swal.fire('Please ! Enter Re-Captch Text ');
            return false;
        }

        if (!$scope.captchaText) {
            Swal.fire('Invalid Captch Text ');
            return false;
        }

        if ($scope.newScholarshipNine.ReCaptcha.toUpperCase() !== $scope.captchaText.toUpperCase()) {
            Swal.fire('Invalid CAPTCHA');
            return false;
        }

        return true;
    }



    //Added By suresh on 7 ashar strts
    $scope.isValidateCharacterTransferCerti = function () {
        if (!$scope.newScholarship.Character_Transfer_Certi_TMP && !$scope.newScholarship.Character_Transfer_CertiPath) {
            Swal.fire('Please ! Upload Character Transfer Certificate');
            return false;
        }
        return true;
    }


    $scope.isValidateCharacterTransferCertiNine = function () {
        if (!$scope.newScholarshipNine.Character_Transfer_Certi_TMP && !$scope.newScholarshipNine.Character_Transfer_CertiPath) {
            Swal.fire('Please ! Upload Character Transfer Certificate');
            return false;
        }
        return true;
    }

    //ENds

    //isValidateBSLocalLevel
    $scope.isValidateBSLocalLevel = function () {
        if ($scope.newScholarship.BC_CertificateTypeId == 2) {
            if (!$scope.newScholarship.BC_IssuedLocalLevelId) {
                Swal.fire('Please ! Select Local Level of Birth Certificate');
                return false;
            }
            return true;
        }

        return true;

    }
    //ENds




    //third tab Validation Starts
    $scope.IsValidScholarship = function () {
        if (!$scope.newScholarship.ScholarshipTypeId) {
            Swal.fire('Please ! Select Scholarship Type');
            return false;
        }

        if ($scope.newScholarship.SchoolTypeId == 1 && !$scope.newScholarship.GovSchoolCerti_IssuedDate_TMP) {
            Swal.fire('Please ! Select Government Certificate Issue Date');
            return false;
        }

        //if ($scope.newScholarship.SchoolTypeId == 1 && $scope.newScholarship.GovSchoolCerti_RefNo.isEmpty()) {
        //	Swal.fire('Please ! Enter Goverment Certificate Ref. No');
        //	return false;
        //}

        if ($scope.newScholarship.SchoolTypeId == 1 && !$scope.newScholarship.GovSchoolCertiPath_TMP && !$scope.newScholarship.GovSchoolCertiPath) {
            Swal.fire('Please ! Upload Goverment Certificate ');
            return false;
        }

        if ((($scope.newScholarship.SchoolTypeId == 1 && $scope.newScholarship.ScholarshipTypeId == 5) ||
            ($scope.newScholarship.SchoolTypeId == 1 && $scope.newScholarship.ScholarshipTypeId == 7) ||
            ($scope.newScholarship.SchoolTypeId == 1 && $scope.newScholarship.ScholarshipTypeId == 8)) &&
            !$scope.newScholarship.ReservationGroupId) {
            Swal.fire('Please ! Select Reservation Group');
            return false;
        }
        //Govern School Certi Ends
        if ($scope.newScholarship.SchoolTypeId == 1) {
            if ($scope.newScholarship.ScholarshipTypeId == 6 || $scope.newScholarship.ScholarshipTypeId == 7) {
                //if (!$scope.newScholarship.LandfilDistrictId) {
                //    Swal.fire('Please ! Select Landfill Site District');
                //    return false;
                //}
                //if (!$scope.newScholarship.LandfillLocalLevelId) {
                //    Swal.fire('Please ! Select Landfill Site LocalLevel');
                //    return false;
                //}
                //if (!$scope.newScholarship.LandfillWardNo) {
                //    Swal.fire('Please ! Select Landfill Site WardNo');
                //    return false;
                //}
                //if (!$scope.newScholarship.LandFill_IssuedDate_TMP) {
                //    Swal.fire('Please ! Select Landfill Site Certificate Issued Date');
                //    return false;
                //}
                //if (!$scope.newScholarship.LandFill_RefNo) {
                //    Swal.fire('Please ! Select Landfill Site Certificate Reference No');
                //    return false;
                //}
                //if (!$scope.newScholarship.LandFillDocPath_TMP) {
                //    Swal.fire('Please ! Upload Landfill Site Certificate');
                //    return false;
                //}
            }
        }



        if ($scope.newScholarship.SchoolTypeId == 2) {
            if ($scope.newScholarship.ScholarshipTypeId == 3 || $scope.newScholarship.ScholarshipTypeId == 6) {
                //if (!$scope.newScholarship.LandfilDistrictId) {
                //    Swal.fire('Please ! Select Landfill Site District');
                //    return false;
                //}
                //if (!$scope.newScholarship.LandfillLocalLevelId) {
                //    Swal.fire('Please ! Select Landfill Site LocalLevel');
                //    return false;
                //}
                //if (!$scope.newScholarship.LandfillWardNo) {
                //    Swal.fire('Please ! Select Landfill Site WardNo');
                //    return false;
                //}
                //if (!$scope.newScholarship.LandFill_IssuedDate_TMP) {
                //    Swal.fire('Please ! Select Landfill Site Certificate Issued Date');
                //    return false;
                //}
                //if (!$scope.newScholarship.LandFill_RefNo) {
                //    Swal.fire('Please ! Select Landfill Site Certificate Reference No');
                //    return false;
                //}
                //if (!$scope.newScholarship.LandFillDocPath_TMP) {
                //    Swal.fire('Please ! Upload Landfill Site Certificate');
                //    return false;
                //}
            }
        }


        

        if ($scope.newScholarship.ReservationGroupList) {
            for (var i = 0; i < $scope.newScholarship.ReservationGroupList.length; i++) {
                var rs = $scope.newScholarship.ReservationGroupList[i];
                if (!rs.ReservationGroupId) {
                    Swal.fire("Please ! Select Reservation Group Name");
                    return false;
                }

                if (!rs.ConcernedAuthorityId) {
                    Swal.fire("Please ! Select Reservation Concerned Authority");
                    return false;
                }

                if (isEmptyObj(rs.GroupWiseCerti_Path) == true && isEmptyObj(rs.GroupWiseCerti_Path_TMP) == true && !rs.GroupWiseCerti_PathData) {
                    Swal.fire("Please ! Upload Related Document of Reservation ");
                    return false;
                }

                if (rs.ConcernedAuthorityId == 1 || rs.ConcernedAuthorityId == 6) {
                    if (!rs.GrpCerti_IssuedDistrictId) {
                        Swal.fire("Please ! Select District");
                        return false;
                    }
                }

                if (rs.ConcernedAuthorityId == 6) {
                    if (!rs.GrpCerti_IssuedLocalLevelId) {
                        Swal.fire("Please ! Select Local Level");
                        return false;
                    }
                }

                if (rs.ConcernedAuthorityId != 5) {
                    if (!rs.GroupWiseCerti_IssuedDateDet) {
                        Swal.fire("Please ! Enter Issue Date");
                        return false;
                    }

                    if (isEmptyObj(rs.GroupWiseCerti_RefNo) == true) {
                        Swal.fire("Please ! Enter Ref. No.");
                        return false;
                    }
                }
            }

        }

        return true;
    }


    $scope.IsValidAgreeCaptcha = function () {
        if (!$scope.newScholarship.IsAgree) {
            Swal.fire('Are you sure you want to submit the form? If you have thoroughly reviewed it, please accept the terms and conditions. ');
            return false;
        }

        if (!$scope.newScholarship.ReCaptcha) {
            Swal.fire('Please ! Enter Re-Captch Text ');
            return false;
        }

        if (!$scope.captchaText) {
            Swal.fire('Invalid Captch Text ');
            return false;
        }

        if ($scope.newScholarship.ReCaptcha.toUpperCase() !== $scope.captchaText.toUpperCase()) 
        {
            Swal.fire('Invalid CAPTCHA');
            return false;
        }

        return true;
    }

    $scope.SaveUpdateScholarship = function () {
        if ($scope.IsValidAgreeCaptcha() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newScholarship.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateScholarship();
                    }
                });
            } else
                $scope.CallSaveUpdateScholarship();
        }
    };

    $scope.CallSaveUpdateScholarship = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var photo = $scope.newScholarship.Photo_TMP;
        var signature = $scope.newScholarship.Signature_TMP;

        var UserPhoto = $scope.newScholarship.BC_FilePath_TMP;
        var UserPhoto1 = $scope.newScholarship.CtznshipFront_FilePath_TMP;
        var UserPhoto2 = $scope.newScholarship.CtznshipBack_FilePath_TMP;
        var UserPhoto3 = $scope.newScholarship.Character_Transfer_Certi_TMP;
        var UserPhoto4 = $scope.newScholarship.Poverty_CertiFilePath_TMP;
        var UserPhoto5 = $scope.newScholarship.GovSchoolCertiPath_TMP;

        var UserPhoto6 = $scope.newScholarship.MigDocPath_TMP;
        var UserPhoto7 = $scope.newScholarship.LandFillDocPath_TMP;
        var UserPhoto8 = $scope.newScholarship.Anusuchi3DocPath_TMP;

        var relatedSchoolFile = $scope.newScholarship.RelatedSchoolFilePath_TMP;
        var gradeSheetFile = $scope.newScholarship.GradeSheet_TMP;
        if ($scope.newScholarship.DOBDet) {

            $scope.newScholarship.DOB = $filter('date')(new Date($scope.newScholarship.DOBDet.dateAD), 'yyyy-MM-dd');
            $scope.newScholarship.DOBMiti = $scope.newScholarship.DOBDet.dateBS;
        } else
            $scope.newScholarship.DOB = null;

        if ($scope.newScholarship.BC_IssuedDateDet) {
            $scope.newScholarship.BC_IssuedDate = $filter('date')(new Date($scope.newScholarship.BC_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarship.BC_IssuedDate = null;

        if ($scope.newScholarship.PovCerti_IssuedDateDet) {
            $scope.newScholarship.PovCerti_IssuedDate = $filter('date')(new Date($scope.newScholarship.PovCerti_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarship.PovCerti_IssuedDate = null;

        if ($scope.newScholarship.GovSchoolCerti_IssuedDateDet) {
            $scope.newScholarship.GovSchoolCerti_IssuedDate = $filter('date')(new Date($scope.newScholarship.GovSchoolCerti_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarship.GovSchoolCerti_IssuedDate = null;

        if ($scope.newScholarship.GroupWiseCerti_IssuedDateDet) {
            $scope.newScholarship.GroupWiseCerti_IssuedDate = $filter('date')(new Date($scope.newScholarship.GroupWiseCerti_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarship.GroupWiseCerti_IssuedDate = null;

        if ($scope.newScholarship.Certi_IssuedDateDet) {
            $scope.newScholarship.Certi_IssuedDate = $filter('date')(new Date($scope.newScholarship.Certi_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarship.Certi_IssuedDate = null;


        if ($scope.newScholarship.MigDoc_IssuedDateDet) {
            $scope.newScholarship.MigDoc_IssuedDate = $filter('date')(new Date($scope.newScholarship.MigDoc_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarship.MigDoc_IssuedDate = null;


        if ($scope.newScholarship.LandFill_IssuedDateDet) {
            $scope.newScholarship.LandFill_IssuedDate = $filter('date')(new Date($scope.newScholarship.LandFill_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarship.LandFill_IssuedDate = null;

        if ($scope.newScholarship.Anusuchi3Doc_IssuedDateDet) {
            $scope.newScholarship.Anusuchi3Doc_IssuedDate = $filter('date')(new Date($scope.newScholarship.Anusuchi3Doc_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else {
            $scope.newScholarship.Anusuchi3Doc_IssuedDate = null;
        }


        if ($scope.newScholarship.RelatedSchoolIssueDateDet) {
            $scope.newScholarship.RelatedSchoolIssueDate = $filter('date')(new Date($scope.newScholarship.RelatedSchoolIssueDateDet.dateAD), 'yyyy-MM-dd');
        } else {
            $scope.newScholarship.RelatedSchoolIssueDate = null;
        }

        //Province district for Permanent Address
        var selectData1 = $('#cboProvincePer').select2('data');
        if (selectData1 && selectData1.length > 0)
            province1 = selectData1[0].text.trim();

        selectData1 = $('#cboDistrictPer').select2('data');
        if (selectData1 && selectData1.length > 0)
            district1 = selectData1[0].text.trim();


        selectData1 = $('#cboAreaPer').select2('data');
        if (selectData1 && selectData1.length > 0)
            area1 = selectData1[0].text.trim();

        //$scope.newScholarship.P_Province = province1;
        //$scope.newScholarship.P_District = district1;
        //$scope.newScholarship.P_LocalLevel = area1;

        //Province district for Temporary Address
        var selectData2 = $('#cboProvinceTmp').select2('data');
        if (selectData2 && selectData2.length > 0)
            province2 = selectData2[0].text.trim();

        selectData2 = $('#cboDistrictTmp').select2('data');
        if (selectData2 && selectData2.length > 0)
            district2 = selectData2[0].text.trim();


        selectData2 = $('#cboAreaTmp').select2('data');
        if (selectData2 && selectData2.length > 0)
            area2 = selectData2[0].text.trim();

        //$scope.newScholarship.Temp_Province = province2;
        //$scope.newScholarship.Temp_District = district2;
        //$scope.newScholarship.Temp_LocalLevel = area2;

        //School district and Local Level				
        var selectData3 = $('#cboDistrictS').select2('data');
        if (selectData3 && selectData3.length > 0)
            district3 = selectData3[0].text.trim();

        selectData3 = $('#cboAreaS').select2('data');
        if (selectData3 && selectData3.length > 0)
            area3 = selectData3[0].text.trim();

        //$scope.newScholarship.SchoolDistrict = district3;
        //$scope.newScholarship.SchoolLocalLevel = area3;




        //Birth Certificate Issued District and LocalLevel
        var selectData6 = $('#cboDistrictBC').select2('data');
        if (selectData6 && selectData6.length > 0)
            district6 = selectData6[0].text.trim();

        selectData6 = $('#cboAreaBC').select2('data');
        if (selectData6 && selectData6.length > 0)
            area6 = selectData6[0].text.trim();

        //$scope.newScholarship.BC_IssuedDistrict = district6;
        //$scope.newScholarship.BC_IssuedLocalLevel = area6;

        //$scope.newScholarship.LandfilDistrict = $scope.newScholarship.LandfilDistrictName;
        //$scope.newScholarship.LandfillLocalLevel = $scope.newScholarship.LandfillLocalLevelName;

        $scope.newScholarship.Lat = $scope.curGue.Lat;
        $scope.newScholarship.Lng = $scope.curGue.Lng;
        if ($scope.newScholarship.ReservationGroupList) {
            $scope.newScholarship.ReservationGroupList.forEach((S) => {
                const findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == S.GrpCerti_IssuedDistrictId);
                const findLocalLevel = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == S.GrpCerti_IssuedLocalLevelId);

                S.GrpCerti_IssuedDistrict = findDistrict ? findDistrict.text : null;
                S.GrpCerti_IssuedLocalLevel = findLocalLevel ? findLocalLevel.text : null;

                if (S.GroupWiseCerti_IssuedDateDet)
                    S.GroupWiseCerti_IssuedDate = $filter('date')(new Date(S.GroupWiseCerti_IssuedDateDet.dateAD), 'yyyy-MM-dd');
            });
        }
        $http({
            method: 'POST',
            url: base_url + "Scholarship/SaveScholarship",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                if (data.stPhoto && data.stPhoto.length > 0)
                    formData.append("photo", data.stPhoto[0]);

                if (data.stSignature && data.stSignature.length > 0)
                    formData.append("signature", data.stSignature[0]);

                /*Birth Certificate */
                if (data.UsPhoto && data.UsPhoto.length > 0)
                    formData.append("UserPhoto", data.UsPhoto[0]);

                /*Citizenship Front */
                if (data.UsPhoto1 && data.UsPhoto1.length > 0)
                    formData.append("UserPhoto1", data.UsPhoto1[0]);

                /*Citizenship Back */
                if (data.UsPhoto2 && data.UsPhoto2.length > 0)
                    formData.append("UserPhoto2", data.UsPhoto2[0]);

                /*Character / Transfer Certificate / Recommendation Letter Upload*/
                if (data.UsPhoto3 && data.UsPhoto3.length > 0)
                    formData.append("UserPhoto3", data.UsPhoto3[0]);

                /*Poverty Certificate by Municipality*/
                if (data.UsPhoto4 && data.UsPhoto4.length > 0)
                    formData.append("UserPhoto4", data.UsPhoto4[0]);

                /*Government School Certificate*/
                if (data.UsPhoto5 && data.UsPhoto5.length > 0)
                    formData.append("UserPhoto5", data.UsPhoto5[0]);

                /*MigDocPath Certificate*/
                if (data.UsPhoto6 && data.UsPhoto6.length > 0)
                    formData.append("UserPhoto6", data.UsPhoto6[0]);

                /*LandFillDocPath Certificate*/
                if (data.UsPhoto7 && data.UsPhoto7.length > 0)
                    formData.append("UserPhoto7", data.UsPhoto7[0]);

                if (relatedSchoolFile && relatedSchoolFile.length > 0)
                    formData.append("RelatedSchoolFile", relatedSchoolFile[0]);

                if (data.UsPhoto8 && data.UsPhoto8.length > 0)
                    formData.append("UserPhoto8", data.UsPhoto8[0]);

                if (gradeSheetFile && gradeSheetFile.length > 0)
                    formData.append("GradeSheetFile", gradeSheetFile[0]);

                angular.forEach($scope.newScholarship.ReservationGroupList, function (rg) {
                    if (rg.ConcernedAuthorityId && rg.ConcernedAuthorityId > 0) {
                        try {
                            if (rg.GroupWiseCerti_Path_TMP && rg.GroupWiseCerti_Path_TMP.length > 0) {
                                var fn = 'CA' + rg.ReservationGroupId;
                                formData.append(fn, rg.GroupWiseCerti_Path_TMP[0]);
                            }
                        } catch (ee) {
                            if (rg.GroupWiseCerti_Path_TMP) {
                                var fn = 'CA' + rg.ReservationGroupId;
                                formData.append(fn, rg.GroupWiseCerti_Path_TMP);
                            }
                        }

                    }
                });


                return formData;
            },
            data: { jsonData: $scope.newScholarship, stPhoto: photo, stSignature: signature, UsPhoto: UserPhoto, UsPhoto1: UserPhoto1, UsPhoto2: UserPhoto2, UsPhoto3: UserPhoto3, UsPhoto4: UserPhoto4, UsPhoto5: UserPhoto5, UsPhoto6: UserPhoto6, UsPhoto7: UserPhoto7, UsPhoto8: UserPhoto8 }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            //	hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.Print();
                //$scope.Fun3();
            }
        }, function (errormessage) {
            $scope.loadingstatus = "stop";
            Swal.fire(errormessage);
        });
    }


    $scope.GetSchoolSubjectWise = function () {
        if ($scope.newScholarship.AppliedSubjectId) {

            $scope.newScholarship.AppliedSubjectName = mx($scope.SubjectList).firstOrDefault(p1 => p1.SubjectId == $scope.newScholarship.AppliedSubjectId).Name;
            $scope.newScholarship.SubjectName = $scope.newScholarship.AppliedSubjectName;

            $scope.loadingstatus = "running";
            showPleaseWait();
            var para = {
                SubjectId: $scope.newScholarship.AppliedSubjectId,
                ClassId:$scope.newScholarship.ClassId,
            };
            $scope.SchoolList = [];

            $http({
                method: 'POST',
                url: base_url + "Scholarship/GetSchoolSubjectWise",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.SchoolList = res.data.Data;
                    $scope.SchoolListFiltered = angular.copy($scope.SchoolList);
                    $scope.newScholarship.SchoolPriorityListColl = [];
                    $scope.newScholarshipNine.SchoolPriorityListColl = [];

                    if ($scope.newScholarship.SchoolPriorityListColl_M && $scope.newScholarship.SchoolPriorityListColl_M.length > 0) {

                        var sind = 0;
                        $scope.newScholarship.SchoolPriorityListColl_M.forEach(function (sp) {
                            $scope.newScholarship.SchoolPriorityListColl.push({
                                AppliedSchoolId: sp.SchoolId,
                                SchoolId: sp.SchoolId
                            });
                            $scope.CheckAlreadySelected(sind);
                            sind++;
                        });

                    } else {
                        $scope.newScholarship.SchoolPriorityListColl.push({
                            ClassName: '',
                            SchoolId: null,
                        });
                        $scope.CheckAlreadySelected(0);
                    }


                    if ($scope.newScholarshipNine.SchoolPriorityListColl_M && $scope.newScholarshipNine.SchoolPriorityListColl_M.length > 0) {

                        var sind = 0;
                        $scope.newScholarshipNine.SchoolPriorityListColl_M.forEach(function (sp) {
                            $scope.newScholarshipNine.SchoolPriorityListColl.push({
                                AppliedSchoolId: sp.SchoolId,
                                SchoolId: sp.SchoolId
                            });
                            $scope.CheckAlreadySelectedNine(sind);
                            sind++;
                        });

                    } else {
                        $scope.newScholarshipNine.SchoolPriorityListColl.push({
                            ClassName: '',
                            SchoolId: null,
                        });
                        $scope.CheckAlreadySelectedNine(0);
                    }


                } else {
                    Swal.fire(res.data.ResponseMSG);
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        }
    }

    $scope.GetSchoolSubjectWiseForNine = function () {
        $scope.newScholarship.AppliedSubjectName = '';
        $scope.newScholarship.SubjectName = '';

        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            SubjectId: 1,
            ClassId: $scope.newScholarshipNine.ClassId,
        };
        $scope.SchoolList = [];

        $http({
            method: 'POST',
            url: base_url + "Scholarship/GetSchoolSubjectWise",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.SchoolList = res.data.Data;
                $scope.SchoolListFiltered = angular.copy($scope.SchoolList);
                $scope.newScholarship.SchoolPriorityListColl = [];
                $scope.newScholarshipNine.SchoolPriorityListColl = [];

                if ($scope.newScholarshipNine.SchoolPriorityListColl_M && $scope.newScholarshipNine.SchoolPriorityListColl_M.length > 0) {

                    var sind = 0;
                    $scope.newScholarshipNine.SchoolPriorityListColl_M.forEach(function (sp) {
                        $scope.newScholarshipNine.SchoolPriorityListColl.push({
                            AppliedSchoolId: sp.SchoolId,
                            SchoolId: sp.SchoolId
                        });
                        $scope.CheckAlreadySelectedNine(sind);
                        sind++;
                    });

                } else {
                    $scope.newScholarshipNine.SchoolPriorityListColl.push({
                        ClassName: '',
                        SchoolId: null,
                    });
                    $scope.CheckAlreadySelectedNine(0);
                }


            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    //getGradesheet for class 9
    $scope.GetGradeSheetNine = function () {


        if (!$scope.newScholarshipNine.DOBDet || !$scope.newScholarshipNine.DOBDet.dateAD || isEmptyObj($scope.newScholarshipNine.DOBDet.dateBS) == true) {
            Swal.fire('Please ! Select Valid D.O.B.');
            return;
        } else {
            var dobAD = $filter('date')(new Date($scope.newScholarshipNine.DOBDet.dateAD), 'yyyy-MM-dd');
            $scope.newScholarshipNine.DOBAD_TMP = new Date(dobAD);
        }

        //if (!$scope.curGue.Lat || !$scope.curGue.Lng) {
        //    Swal.fire('Please allow the geolocation first.')
        //    return;
        //}

      
        if ($scope.newDet.IsEligible == false) {
            Swal.fire('Have you read all the details. If you are eligible accept the terms and conditions to apply for scholarship');
            return;
        }

        if ($scope.isValidateSEEYEarNine() == true &&
            $scope.isValidateEligibilityNine() == true
        ) {

        }

        if (!$scope.newScholarshipNine.DOBDet)
            return;

        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            SEESymbolNo: $scope.newScholarshipNine.SEESymbolNo,
            Alphabet: $scope.newScholarshipNine.Alphabet,
            DOB_AD: ($scope.newScholarshipNine.DOBDet ? $filter('date')(new Date($scope.newScholarshipNine.DOBDet.dateAD), 'yyyy-MM-dd') : null),
            DOB_BS: ($scope.newScholarshipNine.DOBDet ? $scope.newScholarshipNine.DOBDet.dateBS : null),
            GPA: $scope.newScholarshipNine.GPA,
            Pwd: $scope.newScholarshipNine.Pwd,
            ClassId: $scope.newScholarshipNine.ClassId,
        };
        $scope.GradeSheetList = [];

        $http({
            method: 'POST',
            url: base_url + "Scholarship/GetAllGradeSheet",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                document.getElementById('form-pageNine').style.display = "block";
                document.getElementById('Secondpage').style.display = "none";
                document.getElementById('fourthNine').style.display = "none";

                if (res.data.Data.length > 0) {
                     
                    $scope.GradeSheetList = res.data.Data;
                    var fst = $scope.GradeSheetList[0];
                    $scope.newScholarshipNine.GPA = fst.GPA;
                    $scope.newScholarshipNine.DOB_TMP = new Date(fst.DOB_AD);
                    $scope.ChangeDOBNine();

                    if ($scope.GradeSheetList.length > 0) {
                        $scope.bdData = {
                            GPA: fst.GPA,
                            Avg_GPA: fst.Avg_GPA,
                            RollNo: fst.RollNo,
                            StudentName: fst.StudentName,
                            SchoolName: fst.SchoolName,
                            DOB_AD: fst.DOB_AD
                        };

                        if (fst.Scholarship && fst.Scholarship.IsSuccess == true) {
                            $scope.newScholarshipNine = fst.Scholarship;
                            $scope.newScholarshipNine.DOB_TMP = new Date(fst.Scholarship.DOB_AD);
                            $scope.ChangeDOBNine();
                            $scope.newScholarshipNine.DOBAD_TMP = new Date(fst.Scholarship.DOB_AD);

                            if (fst.DVerify && fst.DVerify.VerifyId > 0) {
                                $scope.newScholarshipNine.DV = fst.DVerify;
                            }

                            if ($scope.newScholarshipNine.BC_IssuedDate) {
                                $scope.newScholarshipNine.BC_IssuedDate_TMP = new Date($scope.newScholarshipNine.BC_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.PovCerti_IssuedDate) {
                                $scope.newScholarshipNine.PovCerti_IssuedDate_TMP = new Date($scope.newScholarshipNine.PovCerti_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.GovSchoolCerti_IssuedDate) {
                                $scope.newScholarshipNine.GovSchoolCerti_IssuedDate_TMP = new Date($scope.newScholarshipNine.GovSchoolCerti_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.GroupWiseCerti_IssuedDate) {
                                $scope.newScholarshipNine.GroupWiseCerti_IssuedDate_TMP = new Date($scope.newScholarshipNine.GroupWiseCerti_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.Certi_IssuedDate) {
                                $scope.newScholarshipNine.Certi_IssuedDate_TMP = new Date($scope.newScholarshipNine.Certi_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.MigDoc_IssuedDate) {
                                $scope.newScholarshipNine.MigDoc_IssuedDate_TMP = new Date($scope.newScholarshipNine.MigDoc_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.LandFill_IssuedDate) {
                                $scope.newScholarshipNine.LandFill_IssuedDate_TMP = new Date($scope.newScholarshipNine.LandFill_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.Anusuchi3Doc_IssuedDate) {
                                $scope.newScholarshipNine.Anusuchi3Doc_IssuedDate_TMP = new Date($scope.newScholarshipNine.Anusuchi3Doc_IssuedDate)
                            }

                            //Added By Suresh
                            $scope.newScholarshipNine.Character_Transfer_CertiPath = $scope.newScholarshipNine.Character_Transfer_Certi;
                            if ($scope.newScholarshipNine.RelatedSchoolIssueDate) {
                                $scope.newScholarshipNine.RelatedSchoolIssueDate_TMP = new Date($scope.newScholarshipNine.RelatedSchoolIssueDate)
                            }

                            $scope.newScholarshipNine.Character_Transfer_CertiPath = $scope.newScholarshipNine.Character_Transfer_Certi;
                            $scope.newScholarshipNine.SchoolPriorityListColl_M = angular.copy($scope.newScholarshipNine.SchoolPriorityListColl);

                            var rsIdCollNine = [];
                            if ($scope.newScholarshipNine.ReservationGroupList && $scope.newScholarshipNine.ReservationGroupList.length > 0) {
                                $scope.newScholarshipNine.ReservationGroupList.forEach(function (rs) {

                                    if (rs.GroupWiseCerti_IssuedDate)
                                        rs.GroupWiseCerti_IssuedDate_TMP = new Date(rs.GroupWiseCerti_IssuedDate);

                                    rs.GroupWiseCerti_Path_TMP = rs.GroupWiseCerti_Path;

                                    $scope.ChangeGrpCertiDistrict(rs);
                                    rsIdCollNine.push(rs.ReservationGroupId);
                                });
                            }

                            $scope.newScholarshipNine.ReservationGroupId = rsIdCollNine;
                            $scope.GetSchoolSubjectWiseForNine();
                            //Ends


                            $scope.GetSchoolSubjectWiseForNine();

                            $scope.ProvinceChangeNine();
                            $scope.DistrictChangeNine();
                            $scope.VDCChangeNine();
                            $scope.ChangeBoard();
                        }
                    }
                }  


            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.CheckAlreadyExists = function () {

        $scope.needPassword = false;

        if ($scope.newScholarship.ClassId == 1) {
            if ($scope.newScholarship.SEESymbolNo && $scope.newScholarship.Alphabet) {
                var checkPara = {
                    SEESymbolNo: $scope.newScholarship.SEESymbolNo,
                    Alphabet: $scope.newScholarship.Alphabet,
                    ClassId: $scope.newScholarship.ClassId,
                };
                $http({
                    method: 'POST',
                    url: base_url + "Scholarship/CheckScholarshipApply",
                    dataType: "json",
                    data: JSON.stringify(checkPara)
                }).then(function (res) {
                    if (res.data.IsSuccess == false) {
                        $scope.needPassword = true;
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
            
        }
        else if ($scope.newScholarship.ClassId == 2) {
            if ($scope.newScholarshipNine.SEESymbolNo) {
                var checkPara = {
                    SEESymbolNo: $scope.newScholarshipNine.SEESymbolNo,
                    Alphabet: '',
                    ClassId: $scope.newScholarshipNine.ClassId,
                };
                $http({
                    method: 'POST',
                    url: base_url + "Scholarship/CheckScholarshipApply",
                    dataType: "json",
                    data: JSON.stringify(checkPara)
                }).then(function (res) {
                    if (res.data.IsSuccess == false) {
                        $scope.needPassword = true;
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        }
          

    }
     

    $scope.needPassword = false;
    $scope.GetGradeSheet = function () {

        //		$scope.needPassword = false;

        if (!$scope.newScholarship.DOBDet || !$scope.newScholarship.DOBDet.dateAD || isEmptyObj($scope.newScholarship.DOBDet.dateBS) == true) {
            Swal.fire('Please ! Select Valid D.O.B.');
            return;
        } else {
            var dobAD = $filter('date')(new Date($scope.newScholarship.DOBDet.dateAD), 'yyyy-MM-dd');
            $scope.newScholarship.DOBAD_TMP = new Date(dobAD);
        }

        //if (!$scope.curGue.Lat || !$scope.curGue.Lng) {
        //    Swal.fire('Please allow the geolocation first.')
        //    return;
        //}

        if ($scope.newDet.IsEligible == false) {
            Swal.fire('Have you read all the details. If you are eligible accept the terms and conditions to apply for scholarship');
            return;
        }

        if ($scope.isValidateSEEYEar() == true &&
            $scope.isValidateEligibility() == true
        ) {

        }

        if (!$scope.newScholarship.DOBDet)
            return;

        var checkPara = {
            SEESymbolNo: $scope.newScholarship.SEESymbolNo,
            Alphabet: $scope.newScholarship.Alphabet,
            ClassId: $scope.newScholarship.ClassId,
        };
        $http({
            method: 'POST',
            url: base_url + "Scholarship/CheckScholarshipApply",
            dataType: "json",
            data: JSON.stringify(checkPara)
        }).then(function (res) {
            if (res.data.IsSuccess) {
                $scope.newScholarship.Pwd = null;
                $scope.LoadGradeSheet();
            } else {
                $scope.needPassword = true;
                if (!$scope.newScholarship.Pwd || isEmptyObj($scope.newScholarship.Pwd) == true) {
                    Swal.fire('Please ! Enter Password');
                } else {
                    $scope.LoadGradeSheet();
                }

            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    }
    $scope.LoadGradeSheet = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            SEESymbolNo: $scope.newScholarship.SEESymbolNo,
            Alphabet: $scope.newScholarship.Alphabet,
            DOB_AD: ($scope.newScholarship.DOBDet ? $filter('date')(new Date($scope.newScholarship.DOBDet.dateAD), 'yyyy-MM-dd') : null),
            DOB_BS: $scope.newScholarship.DOB_TMP,
            GPA: $scope.newScholarship.GPA,
            Pwd: $scope.newScholarship.Pwd,
            ClassId: $scope.newScholarship.ClassId,
        };
        $scope.GradeSheetList = [];

        $http({
            method: 'POST',
            url: base_url + "Scholarship/GetAllGradeSheet",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {

                document.getElementById('form-page').style.display = "block";
                document.getElementById('firstpage').style.display = "none";
                document.getElementById('fourth').style.display = "none";

                if ($scope.needPassword == true) {
                    if (res.data.Data.length == 0) {
                        Swal.fire('Invalid Password');
                        return;
                    }

                    var fst = res.data.Data[0];
                    if (fst.Scholarship && fst.Scholarship.IsSuccess == true) { } else {
                        Swal.fire('Invalid Password');
                        return;
                    }
                }

                if (res.data.Data.length > 0) {
                  

                    $scope.GradeSheetList = res.data.Data;
                    var fst = $scope.GradeSheetList[0];
                    $scope.newScholarship.GPA = fst.GPA;
                    $scope.newScholarship.DOB_TMP = new Date(fst.DOB_AD);
                    $scope.newScholarship.DOBAD_TMP = new Date(fst.DOB_AD);
                    //$timeout(function () {
                    //	$scope.ChangeDOB();
                    //               })

                    if ($scope.GradeSheetList.length > 0) {
                        $scope.bdData = {
                            GPA: fst.GPA,
                            Avg_GPA: fst.Avg_GPA,
                            RollNo: fst.RollNo,
                            StudentName: fst.StudentName,
                            SchoolName: fst.SchoolName,
                            DOB_AD: fst.DOB_AD
                        };

                        if (fst.Scholarship && fst.Scholarship.IsSuccess == true) {
                            $scope.newScholarship = fst.Scholarship;
                            $scope.newScholarship.DOB_TMP = new Date(fst.Scholarship.DOB_AD);
                            $scope.newScholarship.DOBAD_TMP = new Date(fst.Scholarship.DOB_AD);

                            if (fst.DVerify && fst.DVerify.VerifyId > 0) {
                                $scope.newScholarship.DV = fst.DVerify;
                            }
                            //$timeout(function () {
                            //	$scope.ChangeDOB();
                            //})
                            if ($scope.newScholarship.BC_IssuedDate) {
                                $scope.newScholarship.BC_IssuedDate_TMP = new Date($scope.newScholarship.BC_IssuedDate);
                            }

                            if ($scope.newScholarship.PovCerti_IssuedDate) {
                                $scope.newScholarship.PovCerti_IssuedDate_TMP = new Date($scope.newScholarship.PovCerti_IssuedDate);
                            }

                            if ($scope.newScholarship.GovSchoolCerti_IssuedDate) {
                                $scope.newScholarship.GovSchoolCerti_IssuedDate_TMP = new Date($scope.newScholarship.GovSchoolCerti_IssuedDate);
                            }

                            if ($scope.newScholarship.GroupWiseCerti_IssuedDate) {
                                $scope.newScholarship.GroupWiseCerti_IssuedDate_TMP = new Date($scope.newScholarship.GroupWiseCerti_IssuedDate);
                            }

                            if ($scope.newScholarship.Certi_IssuedDate) {
                                $scope.newScholarship.Certi_IssuedDate_TMP = new Date($scope.newScholarship.Certi_IssuedDate);
                            }

                            if ($scope.newScholarship.MigDoc_IssuedDate) {
                                $scope.newScholarship.MigDoc_IssuedDate_TMP = new Date($scope.newScholarship.MigDoc_IssuedDate);
                            }

                            if ($scope.newScholarship.LandFill_IssuedDate) {
                                $scope.newScholarship.LandFill_IssuedDate_TMP = new Date($scope.newScholarship.LandFill_IssuedDate);
                            }

                            if ($scope.newScholarship.Anusuchi3Doc_IssuedDate) {
                                $scope.newScholarship.Anusuchi3Doc_IssuedDate_TMP = new Date($scope.newScholarship.Anusuchi3Doc_IssuedDate)
                            }

                            //Added By Suresh on 6 Shrawan
                            if ($scope.newScholarship.RelatedSchoolIssueDate) {
                                $scope.newScholarship.RelatedSchoolIssueDate_TMP = new Date($scope.newScholarship.RelatedSchoolIssueDate);
                            }
                            //Ends

                            $scope.newScholarship.Character_Transfer_CertiPath = $scope.newScholarship.Character_Transfer_Certi;

                            $scope.newScholarship.SchoolPriorityListColl_M = angular.copy($scope.newScholarship.SchoolPriorityListColl);

                            $scope.GetSchoolSubjectWise();

                            var rsIdColl = [];
                            if ($scope.newScholarship.ReservationGroupList && $scope.newScholarship.ReservationGroupList.length > 0) {
                                $scope.newScholarship.ReservationGroupList.forEach(function (rs) {

                                    if (rs.GroupWiseCerti_IssuedDate)
                                        rs.GroupWiseCerti_IssuedDate_TMP = new Date(rs.GroupWiseCerti_IssuedDate);

                                    rs.GroupWiseCerti_Path_TMP = rs.GroupWiseCerti_Path;

                                    $scope.ChangeGrpCertiDistrict(rs);

                                    rsIdColl.push(rs.ReservationGroupId);
                                });
                            }

                            $scope.newScholarship.ReservationGroupId = rsIdColl;


                            $timeout(function () {
                                //$('#cboRGIdColl').val(rsIdColl).change();
                                $scope.AddReservationGroup();
                            });

                            $scope.ProvinceChange();
                            $scope.DistrictChange();
                            $scope.VDCChange();
                            $scope.ChangeBoard();
                        }
                    }
                } else {
                   // Swal.fire('No record found');
                }


            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }


    $scope.LoadGradeSheetNine = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            SEESymbolNo: $scope.newScholarshipNine.SEESymbolNo,
            Alphabet: $scope.newScholarshipNine.Alphabet,
            DOB_AD: ($scope.newScholarshipNine.DOBDet ? $filter('date')(new Date($scope.newScholarshipNine.DOBDet.dateAD), 'yyyy-MM-dd') : null),
            DOB_BS: $scope.newScholarshipNine.DOB_TMP,
            GPA: $scope.newScholarshipNine.GPA,
            Pwd: $scope.newScholarshipNine.Pwd,
            ClassId: $scope.newScholarshipNine.ClassId,
        };
        $scope.GradeSheetList = [];

        $http({
            method: 'POST',
            url: base_url + "Scholarship/GetAllGradeSheet",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                if ($scope.needPassword == true) {
                    if (res.data.Data.length == 0) {
                        Swal.fire('Invalid Password');
                        return;
                    }

                    var fst = res.data.Data[0];
                    if (fst.Scholarship && fst.Scholarship.IsSuccess == true) { } else {
                        Swal.fire('Invalid Password');
                        return;
                    }
                }

                if (res.data.Data.length > 0) {
                    document.getElementById('form-page').style.display = "block";
                    document.getElementById('firstpage').style.display = "none";
                    document.getElementById('fourth').style.display = "none";

                    $scope.GradeSheetList = res.data.Data;
                    var fst = $scope.GradeSheetList[0];
                    $scope.newScholarshipNine.GPA = fst.GPA;
                    $scope.newScholarshipNine.DOB_TMP = new Date(fst.DOB_AD);
                    $scope.newScholarshipNine.DOBAD_TMP = new Date(fst.DOB_AD);
                    //$timeout(function () {
                    //	$scope.ChangeDOB();
                    //               })

                    if ($scope.GradeSheetList.length > 0) {
                        $scope.bdData = {
                            GPA: fst.GPA,
                            Avg_GPA: fst.Avg_GPA,
                            RollNo: fst.RollNo,
                            StudentName: fst.StudentName,
                            SchoolName: fst.SchoolName,
                            DOB_AD: fst.DOB_AD
                        };

                        if (fst.Scholarship && fst.Scholarship.IsSuccess == true) {
                            $scope.newScholarshipNine = fst.Scholarship;
                            $scope.newScholarshipNine.DOB_TMP = new Date(fst.Scholarship.DOB_AD);
                            $scope.newScholarshipNine.DOBAD_TMP = new Date(fst.Scholarship.DOB_AD);
                            //$timeout(function () {
                            //	$scope.ChangeDOB();
                            //})

                            if (fst.DVerify && fst.DVerify.VerifyId > 0) {
                                $scope.newScholarshipNine.DV = fst.DVerify;
                            }

                            if ($scope.newScholarshipNine.BC_IssuedDate) {
                                $scope.newScholarshipNine.BC_IssuedDate_TMP = new Date($scope.newScholarshipNine.BC_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.PovCerti_IssuedDate) {
                                $scope.newScholarshipNine.PovCerti_IssuedDate_TMP = new Date($scope.newScholarshipNine.PovCerti_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.GovSchoolCerti_IssuedDate) {
                                $scope.newScholarshipNine.GovSchoolCerti_IssuedDate_TMP = new Date($scope.newScholarshipNine.GovSchoolCerti_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.GroupWiseCerti_IssuedDate) {
                                $scope.newScholarshipNine.GroupWiseCerti_IssuedDate_TMP = new Date($scope.newScholarshipNine.GroupWiseCerti_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.Certi_IssuedDate) {
                                $scope.newScholarshipNine.Certi_IssuedDate_TMP = new Date($scope.newScholarshipNine.Certi_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.MigDoc_IssuedDate) {
                                $scope.newScholarshipNine.MigDoc_IssuedDate_TMP = new Date($scope.newScholarshipNine.MigDoc_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.LandFill_IssuedDate) {
                                $scope.newScholarshipNine.LandFill_IssuedDate_TMP = new Date($scope.newScholarshipNine.LandFill_IssuedDate);
                            }

                            if ($scope.newScholarshipNine.Anusuchi3Doc_IssuedDate) {
                                $scope.newScholarshipNine.Anusuchi3Doc_IssuedDate_TMP = new Date($scope.newScholarshipNine.Anusuchi3Doc_IssuedDate)
                            }

                            //Added By Suresh on 6 Shrawan
                            if ($scope.newScholarshipNine.RelatedSchoolIssueDate) {
                                $scope.newScholarshipNine.RelatedSchoolIssueDate_TMP = new Date($scope.newScholarshipNine.RelatedSchoolIssueDate);
                            }
                            //Ends

                            $scope.newScholarshipNine.Character_Transfer_CertiPath = $scope.newScholarshipNine.Character_Transfer_Certi;

                            $scope.newScholarshipNine.SchoolPriorityListColl_M = angular.copy($scope.newScholarshipNine.SchoolPriorityListColl);

                            $scope.GetSchoolSubjectWiseForNine();

                            var rsIdColl = [];
                            if ($scope.newScholarshipNine.ReservationGroupList && $scope.newScholarshipNine.ReservationGroupList.length > 0) {
                                $scope.newScholarshipNine.ReservationGroupList.forEach(function (rs) {

                                    if (rs.GroupWiseCerti_IssuedDate)
                                        rs.GroupWiseCerti_IssuedDate_TMP = new Date(rs.GroupWiseCerti_IssuedDate);

                                    rs.GroupWiseCerti_Path_TMP = rs.GroupWiseCerti_Path;

                                    $scope.ChangeGrpCertiDistrict(rs);
                                    rsIdColl.push(rs.ReservationGroupId);
                                });
                            }

                            $scope.newScholarshipNine.ReservationGroupId = rsIdColl;


                            $timeout(function () {
                                //$('#cboRGIdColl').val(rsIdColl).change();
                                $scope.AddReservationGroupNine();
                            });

                            $scope.ProvinceChangeNine();
                            $scope.DistrictChangeNine();
                            $scope.VDCChangeNine();

                        }
                    }
                } else {
                    Swal.fire('No record found');
                }


            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GPAChange = function () {
        var gpa = isEmptyNum($scope.newScholarship.GPA);

        if (gpa > 4) {
            Swal.fire("GPA must be less than or equal 4");
            $scope.newScholarship.GPA = 0;
        }

    }
    $scope.GPAChangeNine = function () {
        var gpa = isEmptyNum($scope.newScholarshipNine.GPA);

        if (gpa > 4) {
            Swal.fire("GPA must be less than or equal 4");
            $scope.newScholarshipNine.GPA = 0;
        }

    }
    $scope.GPALostFocus = function () {
        var gpa = isEmptyNum($scope.newScholarship.GPA);
        if (gpa == 0)
            return;

        if (gpa > 4) {
            Swal.fire("GPA must be less than or equal 4");
            $scope.newScholarship.GPA = 0;
        } else if (gpa < 1.6) {
            Swal.fire("You Are Not Eligiable For Scholar Ship");
            $scope.newScholarship.GPA = 0;
        }
    }
    $scope.GPALostFocusNine = function () {
        var gpa = isEmptyNum($scope.newScholarshipNine.GPA);
        if (gpa == 0)
            return;

        if (gpa > 4) {
            Swal.fire("GPA must be less than or equal 4");
            $scope.newScholarshipNine.GPA = 0;
        } else if (gpa < 1.6) {
            Swal.fire("You Are Not Eligiable For Scholar Ship");
            $scope.newScholarshipNine.GPA = 0;
        }
    }
    $scope.ChangeDOB = function () {
        if (!$scope.newScholarship.DOBDet) {
            //Swal.fire('Please ! Select Valid D.O.B.');
        } else {
            var dobAD = $filter('date')(new Date($scope.newScholarship.DOBDet.dateAD), 'yyyy-MM-dd');
            $scope.newScholarship.DOBAD_TMP = new Date(dobAD);
        }

    }

    $scope.ChangeDOBNine = function () {
        if (!$scope.newScholarshipNine.DOBDet) {
            //Swal.fire('Please ! Select Valid D.O.B.');
        } else {
            var dobAD = $filter('date')(new Date($scope.newScholarshipNine.DOBDet.dateAD), 'yyyy-MM-dd');
            $scope.newScholarshipNine.DOBAD_TMP = new Date(dobAD);
        }

    }

    $scope.ChangeBoard = function () {
        if ($scope.BoardList && $scope.BoardList.length > 0 && $scope.newScholarship.EquivalentBoardId>0) {
            var findB = mx($scope.BoardList).firstOrDefault(p1 => p1.BoardId == $scope.newScholarship.EquivalentBoardId);
            if(findB)
                $scope.newScholarship.EquivalentBoardName = findB.Name;
        }

        if ($scope.BoardListNine && $scope.BoardListNine.length > 0 && $scope.newScholarshipNine.EquivalentBoardId>0) {
            var findB = mx($scope.BoardListNine).firstOrDefault(p1 => p1.BoardId == $scope.newScholarshipNine.EquivalentBoardId);
            if(findB)
                $scope.newScholarshipNine.EquivalentBoardName = findB.Name;
        }
    }
    

    $scope.ProvinceChange = function () {

        if ($scope.newScholarship.P_ProvinceId && $scope.newScholarship.P_ProvinceId > 0) {
            $scope.newScholarship.P_ProvinceName = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarship.P_ProvinceId).text;
            $scope.newScholarship.P_Province = $scope.newScholarship.P_ProvinceName;
        } else {
            $scope.newScholarship.P_ProvinceName = '';
            $scope.newScholarship.P_Province = '';
        }
            

        if ($scope.newScholarship.Temp_ProvinceId && $scope.newScholarship.Temp_ProvinceId > 0) {
            $scope.newScholarship.Temp_ProvinceName = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarship.Temp_ProvinceId).text;
            $scope.newScholarship.Temp_Province = $scope.newScholarship.Temp_ProvinceName;
        } else {
            $scope.newScholarship.Temp_ProvinceName = '';
            $scope.newScholarship.Temp_Province = '';
        }
            

    }
    $scope.DistrictChange = function () {

        if ($scope.newScholarship.P_DistrictId && $scope.newScholarship.P_DistrictId > 0) {
            $scope.newScholarship.P_DistrictName = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarship.P_DistrictId).text;
            $scope.newScholarship.P_District = $scope.newScholarship.P_DistrictName;
        } else {
            $scope.newScholarship.P_DistrictName = '';
            $scope.newScholarship.P_District = '';
        }
            

        if ($scope.newScholarship.Temp_DistrictId && $scope.newScholarship.Temp_DistrictId > 0) {
            $scope.newScholarship.Temp_DistrictName = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarship.Temp_DistrictId).text;
            $scope.newScholarship.Temp_District = $scope.newScholarship.Temp_DistrictName;
        } else {
            $scope.newScholarship.Temp_DistrictName = '';
            $scope.newScholarship.Temp_District = '';
        }


        if ($scope.newScholarship.BC_IssuedDistrictId && $scope.newScholarship.BC_IssuedDistrictId > 0) {
            $scope.newScholarship.BC_IssuedDistrictName = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarship.BC_IssuedDistrictId).text;
            $scope.newScholarship.BC_IssuedDistrict = $scope.newScholarship.BC_IssuedDistrictName;
        } else {
            $scope.newScholarship.BC_IssuedDistrictName = '';
            $scope.newScholarship.BC_IssuedDistrict = '';
        }

        if ($scope.newScholarship.SchoolDistrictId && $scope.newScholarship.SchoolDistrictId > 0) {
            $scope.newScholarship.SchoolDistrictName = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarship.SchoolDistrictId).text;
            $scope.newScholarship.SchoolDistrict = $scope.newScholarship.SchoolDistrictName;
        } else {
            $scope.newScholarship.SchoolDistrictName = '';
            $scope.newScholarship.SchoolDistrict = '';
        }
            

        if ($scope.newScholarship.LandfilDistrictId && $scope.newScholarship.LandfilDistrictId > 0) {
            $scope.newScholarship.LandfilDistrictName = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarship.LandfilDistrictId).text;
            $scope.newScholarship.LandfilDistrict = $scope.newScholarship.LandfilDistrictName;
        } else {
            $scope.newScholarship.LandfilDistrictName = '';
            $scope.newScholarship.LandfilDistrict = '';
        }
            

    }
    $scope.VDCChange = function () {

        if ($scope.newScholarship.P_LocalLevelId && $scope.newScholarship.P_LocalLevelId > 0) {
            $scope.newScholarship.P_LocalLevelName = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarship.P_LocalLevelId).text;
            $scope.newScholarship.P_LocalLevel = $scope.newScholarship.P_LocalLevelName;
        } else {
            $scope.newScholarship.P_LocalLevelName = '';
            $scope.newScholarship.P_LocalLevel = '';
        }
            

        if ($scope.newScholarship.Temp_LocalLevelId && $scope.newScholarship.Temp_LocalLevelId > 0) {
            $scope.newScholarship.Temp_LocalLevelName = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarship.Temp_LocalLevelId).text;
            $scope.newScholarship.Temp_LocalLevel = $scope.newScholarship.Temp_LocalLevelName;
        } else {
            $scope.newScholarship.Temp_LocalLevelName = '';
            $scope.newScholarship.Temp_LocalLevel = '';
        }
            

        if ($scope.newScholarship.BC_IssuedLocalLevelId && $scope.newScholarship.BC_IssuedLocalLevelId > 0) {
            $scope.newScholarship.BC_IssuedLocalLevelName = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarship.BC_IssuedLocalLevelId).text;
            $scope.newScholarship.BC_IssuedLocalLevel = $scope.newScholarship.BC_IssuedLocalLevelName;
        } else {
            $scope.newScholarship.BC_IssuedLocalLevelName = '';
            $scope.newScholarship.BC_IssuedLocalLevel = '';
        }

        if ($scope.newScholarship.SchoolLocalLevelId && $scope.newScholarship.SchoolLocalLevelId > 0) {
            $scope.newScholarship.SchoolLocalLevelName = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarship.SchoolLocalLevelId).text;
            $scope.newScholarship.SchoolLocalLevel = $scope.newScholarship.SchoolLocalLevelName;
        } else {
            $scope.newScholarship.SchoolLocalLevelName = '';
            $scope.newScholarship.SchoolLocalLevel = '';
        }

        if ($scope.newScholarship.LandfillLocalLevelId && $scope.newScholarship.LandfillLocalLevelId > 0) {
            $scope.newScholarship.LandfillLocalLevelName = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarship.LandfillLocalLevelId).text;
            $scope.newScholarship.LandfillLocalLevel = $scope.newScholarship.LandfillLocalLevelName;
        } else {
            $scope.newScholarship.LandfillLocalLevelName = '';
            $scope.newScholarship.LandfillLocalLevel = '';
        }

    }

    $scope.ProvinceChangeNine = function () {

        if ($scope.newScholarshipNine.P_ProvinceId && $scope.newScholarshipNine.P_ProvinceId > 0) {
            $scope.newScholarshipNine.P_ProvinceName = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarshipNine.P_ProvinceId).text;
            $scope.newScholarshipNine.P_Province = $scope.newScholarshipNine.P_ProvinceName;
        } else {
            $scope.newScholarshipNine.P_ProvinceName = '';
            $scope.newScholarshipNine.P_Province = '';
        }


        if ($scope.newScholarshipNine.Temp_ProvinceId && $scope.newScholarshipNine.Temp_ProvinceId > 0) {
            $scope.newScholarshipNine.Temp_ProvinceName = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarshipNine.Temp_ProvinceId).text;
            $scope.newScholarshipNine.Temp_Province = $scope.newScholarshipNine.Temp_ProvinceName;
        } else {
            $scope.newScholarshipNine.Temp_ProvinceName = '';
            $scope.newScholarshipNine.Temp_Province = '';
        }


    }
    $scope.DistrictChangeNine = function () {

        if ($scope.newScholarshipNine.P_DistrictId && $scope.newScholarshipNine.P_DistrictId > 0) {
            $scope.newScholarshipNine.P_DistrictName = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarshipNine.P_DistrictId).text;
            $scope.newScholarshipNine.P_District = $scope.newScholarshipNine.P_DistrictName;
        } else {
            $scope.newScholarshipNine.P_DistrictName = '';
            $scope.newScholarshipNine.P_District = '';
        }


        if ($scope.newScholarshipNine.Temp_DistrictId && $scope.newScholarshipNine.Temp_DistrictId > 0) {
            $scope.newScholarshipNine.Temp_DistrictName = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarshipNine.Temp_DistrictId).text;
            $scope.newScholarshipNine.Temp_District = $scope.newScholarshipNine.Temp_DistrictName;
        } else {
            $scope.newScholarshipNine.Temp_DistrictName = '';
            $scope.newScholarshipNine.Temp_District = '';
        }


        if ($scope.newScholarshipNine.BC_IssuedDistrictId && $scope.newScholarshipNine.BC_IssuedDistrictId > 0) {
            $scope.newScholarshipNine.BC_IssuedDistrictName = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarshipNine.BC_IssuedDistrictId).text;
            $scope.newScholarshipNine.BC_IssuedDistrict = $scope.newScholarshipNine.BC_IssuedDistrictName;
        } else {
            $scope.newScholarshipNine.BC_IssuedDistrictName = '';
            $scope.newScholarshipNine.BC_IssuedDistrict = '';
        }

        if ($scope.newScholarshipNine.SchoolDistrictId && $scope.newScholarshipNine.SchoolDistrictId > 0) {
            $scope.newScholarshipNine.SchoolDistrictName = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarshipNine.SchoolDistrictId).text;
            $scope.newScholarshipNine.SchoolDistrict = $scope.newScholarshipNine.SchoolDistrictName;
        } else {
            $scope.newScholarshipNine.SchoolDistrictName = '';
            $scope.newScholarshipNine.SchoolDistrict = '';
        }


        if ($scope.newScholarshipNine.LandfilDistrictId && $scope.newScholarshipNine.LandfilDistrictId > 0) {
            $scope.newScholarshipNine.LandfilDistrictName = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarshipNine.LandfilDistrictId).text;
            $scope.newScholarshipNine.LandfilDistrict = $scope.newScholarshipNine.LandfilDistrictName;
        } else {
            $scope.newScholarshipNine.LandfilDistrictName = '';
            $scope.newScholarshipNine.LandfilDistrict = '';
        }


    }
    $scope.VDCChangeNine = function () {

        if ($scope.newScholarshipNine.P_LocalLevelId && $scope.newScholarshipNine.P_LocalLevelId > 0) {
            $scope.newScholarshipNine.P_LocalLevelName = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarshipNine.P_LocalLevelId).text;
            $scope.newScholarshipNine.P_LocalLevel = $scope.newScholarshipNine.P_LocalLevelName;
        } else {
            $scope.newScholarshipNine.P_LocalLevelName = '';
            $scope.newScholarshipNine.P_LocalLevel = '';
        }


        if ($scope.newScholarshipNine.Temp_LocalLevelId && $scope.newScholarshipNine.Temp_LocalLevelId > 0) {
            $scope.newScholarshipNine.Temp_LocalLevelName = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarshipNine.Temp_LocalLevelId).text;
            $scope.newScholarshipNine.Temp_LocalLevel = $scope.newScholarshipNine.Temp_LocalLevelName;
        } else {
            $scope.newScholarshipNine.Temp_LocalLevelName = '';
            $scope.newScholarshipNine.Temp_LocalLevel = '';
        }


        if ($scope.newScholarshipNine.BC_IssuedLocalLevelId && $scope.newScholarshipNine.BC_IssuedLocalLevelId > 0) {
            $scope.newScholarshipNine.BC_IssuedLocalLevelName = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarshipNine.BC_IssuedLocalLevelId).text;
            $scope.newScholarshipNine.BC_IssuedLocalLevel = $scope.newScholarshipNine.BC_IssuedLocalLevelName;
        } else {
            $scope.newScholarshipNine.BC_IssuedLocalLevelName = '';
            $scope.newScholarshipNine.BC_IssuedLocalLevel = '';
        }

        if ($scope.newScholarshipNine.SchoolLocalLevelId && $scope.newScholarshipNine.SchoolLocalLevelId > 0) {
            $scope.newScholarshipNine.SchoolLocalLevelName = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarshipNine.SchoolLocalLevelId).text;
            $scope.newScholarshipNine.SchoolLocalLevel = $scope.newScholarshipNine.SchoolLocalLevelName;
        } else {
            $scope.newScholarshipNine.SchoolLocalLevelName = '';
            $scope.newScholarshipNine.SchoolLocalLevel = '';
        }

        if ($scope.newScholarshipNine.LandfillLocalLevelId && $scope.newScholarshipNine.LandfillLocalLevelId > 0) {
            $scope.newScholarshipNine.LandfillLocalLevelName = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == $scope.newScholarshipNine.LandfillLocalLevelId).text;
            $scope.newScholarshipNine.LandfillLocalLevel = $scope.newScholarshipNine.LandfillLocalLevelName;
        } else {
            $scope.newScholarshipNine.LandfillLocalLevelName = '';
            $scope.newScholarshipNine.LandfillLocalLevel = '';
        }

    }

    $scope.CheckAlreadySelected = function (ind) {

        var indItem = null;
        var alreadySelected = mx($scope.newScholarship.SchoolPriorityListColl)

        if (ind >= 0) {
            if ($scope.newScholarship.SchoolPriorityListColl.length > ind) {
                indItem = $scope.newScholarship.SchoolPriorityListColl[ind];
            }
        }

        var SchoolListFiltered = [];
        $scope.SchoolList.forEach(function (item) {
            var findS = alreadySelected.firstOrDefault(p1 => p1.SchoolId == item.SchoolId);
            if (!findS)
                SchoolListFiltered.push(item);
            else {
                if (indItem) {
                    if (indItem.SchoolId == item.SchoolId) {
                        SchoolListFiltered.push(item);
                    }
                }
            }

        });

        if (ind >= 0) {
            $scope.newScholarship.SchoolPriorityListColl[ind].SchoolList = SchoolListFiltered;
        }

    }

    $scope.CheckAlreadySelectedNine = function (ind) {

        var indItem = null;
        var alreadySelected = mx($scope.newScholarshipNine.SchoolPriorityListColl)

        if (ind >= 0) {
            if ($scope.newScholarshipNine.SchoolPriorityListColl.length > ind) {
                indItem = $scope.newScholarshipNine.SchoolPriorityListColl[ind];
            }
        }

        var SchoolListFiltered = [];
        $scope.SchoolList.forEach(function (item) {
            var findS = alreadySelected.firstOrDefault(p1 => p1.SchoolId == item.SchoolId);
            if (!findS)
                SchoolListFiltered.push(item);
            else {
                if (indItem) {
                    if (indItem.SchoolId == item.SchoolId) {
                        SchoolListFiltered.push(item);
                    }
                }
            }

        });

        if (ind >= 0) {
            $scope.newScholarshipNine.SchoolPriorityListColl[ind].SchoolList = SchoolListFiltered;
        }

    }
    $scope.ChangeScholarshipType = function () {
        if ($scope.newScholarship.ScholarshipTypeId > 0) {
            $scope.newScholarship.ScholarshipTypeName = mx($scope.ScholarshipTypeList).firstOrDefault(p1 => p1.id == $scope.newScholarship.ScholarshipTypeId).text;
        } else
            $scope.newScholarship.ScholarshipTypeName = '';
    }

    $scope.ChangeScholarshipTypeNine = function () {
        if ($scope.newScholarshipNine.ScholarshipTypeId > 0) {
            $scope.newScholarshipNine.ScholarshipTypeName = mx($scope.ScholarshipTypeList).firstOrDefault(p1 => p1.id == $scope.newScholarshipNine.ScholarshipTypeId).text;
        } else
            $scope.newScholarshipNine.ScholarshipTypeName = '';
    }
    $scope.ChangeAuthority = function (curR) {

        if (curR) {
            var findQ = mx(curR.AuthorityList).firstOrDefault(p1 => p1.AuthorityId == curR.ConcernedAuthorityId);
            if (findQ) {
                curR.ConcernedAuthorityName = findQ.Name;
            }
        }
    }


    $scope.ChangeGrpCertiDistrict = function (curR) {
        if (curR) {
            var findQ = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == curR.GrpCerti_IssuedDistrictId);
            if (findQ) {
                curR.GrpCerti_IssuedDistrictName = findQ.text;
            }
        }
    }
    $scope.ChangeGrpCertiVDC = function (curR) {
        if (curR) {
            var findQ = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == curR.GrpCerti_IssuedLocalLevelId);
            if (findQ) {
                curR.GrpCerti_IssuedLocalLevelName = findQ.text;
            }
        }
    }

    $scope.Fun1 = function () {
        //alert('a');
    }
    $scope.Fun2 = function () {
        //alert('b');
    }
    $scope.Fun3 = function () {
        $timeout(function () {
            $scope.ClearScholarship();

            document.getElementById('form-page').style.display = "none";
            document.getElementById('firstpage').style.display = "block";

            document.getElementById('personal-form').style.display = "block";
            document.getElementById('reservation-form').style.display = "none";
            document.getElementById('academic-form').style.display = "none";
            document.getElementById('first').style.display = "block";
            document.getElementById('second').style.display = "none";
            document.getElementById('third').style.display = "none";
            document.getElementById('PreviewForm').style.display = "none";

            document.getElementById('PreviewFormNine').style.display = "none";
        });
    }
    $scope.Print = function () {

        $scope.PrintForm = true;
        //PreviewForm
        $('#printform').printThis({
            beforePrintEvent: $scope.Fun1,

            beforePrint: $scope.Fun2,

            afterPrint: $scope.Fun3
        });
    }
    $scope.PrintNine = function () {

        $scope.PrintForm = true;
        //PreviewForm
        $('#printformNine').printThis({
            beforePrintEvent: $scope.Fun1,

            beforePrint: $scope.Fun2,

            afterPrint: $scope.Fun3
        });
    }
    $scope.OnClassClick = function (cid) {
        $scope.BoardList = [];
        $scope.BoardListNine = [];
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newScholarship.ClassId = cid;
                $scope.newScholarshipNine.ClassId = cid;

                angular.forEach($scope.AllBoardList, function (bl) {
                    if (!bl.ClassId || bl.ClassId == cid) {
                        $scope.BoardList.push(bl);
                        $scope.BoardListNine.push(bl);
                    }
                });
                
                if ($scope.BoardList && $scope.BoardList.length > 0) {
                    $scope.newScholarship.EquivalentBoardId = $scope.BoardList[0].BoardId;
                    $scope.newScholarship.EquivalentBoardName = $scope.BoardList[0].Name;
                }

                if ($scope.BoardListNine && $scope.BoardListNine.length > 0) {
                    $scope.newScholarshipNine.EquivalentBoardId = $scope.BoardListNine[0].BoardId;
                    $scope.newScholarshipNine.EquivalentBoardName = $scope.BoardListNine[0].Name;
                }

                if(cid==2)
                    $scope.GetSchoolSubjectWiseForNine();
            });
        });
        
    }


    $scope.SaveUpdateScholarshipNine = function () {
        if ($scope.IsValidAgreeCaptchaNine() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newScholarship.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateScholarshipNine();
                    }
                });
            } else
                $scope.CallSaveUpdateScholarshipNine();
        }
    };

    $scope.CallSaveUpdateScholarshipNine = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var photo = $scope.newScholarshipNine.Photo_TMP;
        var signature = $scope.newScholarshipNine.Signature_TMP;

        var UserPhoto = $scope.newScholarshipNine.BC_FilePath_TMP;
        var UserPhoto1 = $scope.newScholarshipNine.CtznshipFront_FilePath_TMP;
        var UserPhoto2 = $scope.newScholarshipNine.CtznshipBack_FilePath_TMP;
        var UserPhoto3 = $scope.newScholarshipNine.Character_Transfer_Certi_TMP;
        var UserPhoto4 = $scope.newScholarshipNine.Poverty_CertiFilePath_TMP;
        var UserPhoto5 = $scope.newScholarshipNine.GovSchoolCertiPath_TMP;

        var UserPhoto6 = $scope.newScholarshipNine.MigDocPath_TMP;
        var UserPhoto7 = $scope.newScholarshipNine.LandFillDocPath_TMP;
        var UserPhoto8 = $scope.newScholarshipNine.Anusuchi3DocPath_TMP;

        var relatedSchoolFile = $scope.newScholarshipNine.RelatedSchoolFilePath_TMP;
        var gradeSheetFile = $scope.newScholarshipNine.GradeSheet_TMP;

        if ($scope.newScholarshipNine.DOBDet) {

            $scope.newScholarshipNine.DOB = $filter('date')(new Date($scope.newScholarshipNine.DOBDet.dateAD), 'yyyy-MM-dd');
            $scope.newScholarshipNine.DOBMiti = $scope.newScholarshipNine.DOBDet.dateBS;
        } else
            $scope.newScholarshipNine.DOB = null;

        if ($scope.newScholarshipNine.BC_IssuedDateDet) {
            $scope.newScholarshipNine.BC_IssuedDate = $filter('date')(new Date($scope.newScholarshipNine.BC_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarshipNine.BC_IssuedDate = null;

        if ($scope.newScholarshipNine.PovCerti_IssuedDateDet) {
            $scope.newScholarshipNine.PovCerti_IssuedDate = $filter('date')(new Date($scope.newScholarshipNine.PovCerti_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarshipNine.PovCerti_IssuedDate = null;

        if ($scope.newScholarshipNine.GovSchoolCerti_IssuedDateDet) {
            $scope.newScholarshipNine.GovSchoolCerti_IssuedDate = $filter('date')(new Date($scope.newScholarshipNine.GovSchoolCerti_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarshipNine.GovSchoolCerti_IssuedDate = null;

        if ($scope.newScholarshipNine.GroupWiseCerti_IssuedDateDet) {
            $scope.newScholarshipNine.GroupWiseCerti_IssuedDate = $filter('date')(new Date($scope.newScholarshipNine.GroupWiseCerti_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarshipNine.GroupWiseCerti_IssuedDate = null;

        if ($scope.newScholarshipNine.Certi_IssuedDateDet) {
            $scope.newScholarshipNine.Certi_IssuedDate = $filter('date')(new Date($scope.newScholarshipNine.Certi_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarshipNine.Certi_IssuedDate = null;


        if ($scope.newScholarshipNine.MigDoc_IssuedDateDet) {
            $scope.newScholarshipNine.MigDoc_IssuedDate = $filter('date')(new Date($scope.newScholarshipNine.MigDoc_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarshipNine.MigDoc_IssuedDate = null;


        if ($scope.newScholarshipNine.LandFill_IssuedDateDet) {
            $scope.newScholarshipNine.LandFill_IssuedDate = $filter('date')(new Date($scope.newScholarshipNine.LandFill_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newScholarshipNine.LandFill_IssuedDate = null;

        if ($scope.newScholarshipNine.Anusuchi3Doc_IssuedDateDet) {
            $scope.newScholarshipNine.Anusuchi3Doc_IssuedDate = $filter('date')(new Date($scope.newScholarshipNine.Anusuchi3Doc_IssuedDateDet.dateAD), 'yyyy-MM-dd');
        } else {
            $scope.newScholarshipNine.Anusuchi3Doc_IssuedDate = null;
        }


        if ($scope.newScholarshipNine.RelatedSchoolIssueDateDet) {
            $scope.newScholarshipNine.RelatedSchoolIssueDate = $filter('date')(new Date($scope.newScholarshipNine.RelatedSchoolIssueDateDet.dateAD), 'yyyy-MM-dd');
        } else {
            $scope.newScholarshipNine.RelatedSchoolIssueDate = null;
        }

           

        $scope.newScholarshipNine.Lat = $scope.curGue.Lat;
        $scope.newScholarshipNine.Lng = $scope.curGue.Lng;
        if ($scope.newScholarshipNine.ReservationGroupList) {
            $scope.newScholarshipNine.ReservationGroupList.forEach((S) => {
                const findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == S.GrpCerti_IssuedDistrictId);
                const findLocalLevel = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == S.GrpCerti_IssuedLocalLevelId);

                S.GrpCerti_IssuedDistrict = findDistrict ? findDistrict.text : null;
                S.GrpCerti_IssuedLocalLevel = findLocalLevel ? findLocalLevel.text : null;

                if (S.GroupWiseCerti_IssuedDateDet)
                    S.GroupWiseCerti_IssuedDate = $filter('date')(new Date(S.GroupWiseCerti_IssuedDateDet.dateAD), 'yyyy-MM-dd');
            });
        }
        $http({
            method: 'POST',
            url: base_url + "Scholarship/SaveScholarship",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                if (data.stPhoto && data.stPhoto.length > 0)
                    formData.append("photo", data.stPhoto[0]);

                if (data.stSignature && data.stSignature.length > 0)
                    formData.append("signature", data.stSignature[0]);

                /*Birth Certificate */
                if (data.UsPhoto && data.UsPhoto.length > 0)
                    formData.append("UserPhoto", data.UsPhoto[0]);

                /*Citizenship Front */
                if (data.UsPhoto1 && data.UsPhoto1.length > 0)
                    formData.append("UserPhoto1", data.UsPhoto1[0]);

                /*Citizenship Back */
                if (data.UsPhoto2 && data.UsPhoto2.length > 0)
                    formData.append("UserPhoto2", data.UsPhoto2[0]);

                /*Character / Transfer Certificate / Recommendation Letter Upload*/
                if (data.UsPhoto3 && data.UsPhoto3.length > 0)
                    formData.append("UserPhoto3", data.UsPhoto3[0]);

                /*Poverty Certificate by Municipality*/
                if (data.UsPhoto4 && data.UsPhoto4.length > 0)
                    formData.append("UserPhoto4", data.UsPhoto4[0]);

                /*Government School Certificate*/
                if (data.UsPhoto5 && data.UsPhoto5.length > 0)
                    formData.append("UserPhoto5", data.UsPhoto5[0]);

                /*MigDocPath Certificate*/
                if (data.UsPhoto6 && data.UsPhoto6.length > 0)
                    formData.append("UserPhoto6", data.UsPhoto6[0]);

                /*LandFillDocPath Certificate*/
                if (data.UsPhoto7 && data.UsPhoto7.length > 0)
                    formData.append("UserPhoto7", data.UsPhoto7[0]);

                if (relatedSchoolFile && relatedSchoolFile.length > 0)
                    formData.append("RelatedSchoolFile", relatedSchoolFile[0]);

                if (data.UsPhoto8 && data.UsPhoto8.length > 0)
                    formData.append("UserPhoto8", data.UsPhoto8[0]);

                if (gradeSheetFile && gradeSheetFile.length > 0)
                    formData.append("GradeSheetFile", gradeSheetFile[0]);

                angular.forEach($scope.newScholarshipNine.ReservationGroupList, function (rg) {
                    if (rg.ConcernedAuthorityId && rg.ConcernedAuthorityId > 0) {
                        try {
                            if (rg.GroupWiseCerti_Path_TMP && rg.GroupWiseCerti_Path_TMP.length > 0) {
                                var fn = 'CA' + rg.ReservationGroupId;
                                formData.append(fn, rg.GroupWiseCerti_Path_TMP[0]);
                            }
                        } catch (ee) {
                            if (rg.GroupWiseCerti_Path_TMP) {
                                var fn = 'CA' + rg.ReservationGroupId;
                                formData.append(fn, rg.GroupWiseCerti_Path_TMP);
                            }
                        }

                    }
                });
                 
                return formData;
            },
            data: { jsonData: $scope.newScholarshipNine, stPhoto: photo, stSignature: signature, UsPhoto: UserPhoto, UsPhoto1: UserPhoto1, UsPhoto2: UserPhoto2, UsPhoto3: UserPhoto3, UsPhoto4: UserPhoto4, UsPhoto5: UserPhoto5, UsPhoto6: UserPhoto6, UsPhoto7: UserPhoto7, UsPhoto8: UserPhoto8 }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            //	hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.PrintNine();
                //$scope.Fun3();
            }
        }, function (errormessage) {
            $scope.loadingstatus = "stop";
            Swal.fire(errormessage);
        });
    }
});