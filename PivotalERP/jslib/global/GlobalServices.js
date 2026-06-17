
app.service('GlobalServices', function ($http, $filter, $timeout, $translate, $rootScope)
{ 
    this.ChangeLanguage = function () {

        $http({
            method: 'GET',
            url: base_url + "Global/GetComCountry",
            dataType: "json"
        }).then(function (res)
        {
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
        });
    };
    this.getDateStyleConfig = function () {
        return $http({
            method: 'GET',
            url: base_url + "Setup/Security/GetDateStyle",
            dataType: "json"
        })
    };

    //this.getDateStyleConfig = function () {
    //    return $http({
    //        method: 'GET',
    //        url: base_url + "Setup/Security/GetDateStyle",
    //        dataType: "json"
    //    }).then(function (response) {
    //        // Cache the value when this function is called
    //        currentDateStyle = response.data.Data;
    //        return response;
    //    });
    //};


    this.getExpression = function () {

        var dataColl = [
            { text: "equal to", value: "==" },
            { text: "not equal to", value: "!=" },
            { text: "less than", value: "<" },
            { text: "less than equal", value: "<=" },
            { text: "greater than", value: ">" },
            { text: "greater than equal", value: ">=" },
            { text: "between", value: "between" },
            { text: "contain", value: ".Contains(" },
            { text: "StartWith", value: ".StartsWith(" },
            { text: "EndWith", value: ".EndsWith(" },
        ];

        return dataColl;
    };
    this.getLogicCondition = function () {

        var dataColl = [
            { text: "And", value: "&&" },
            { text: "Or", value: "||" }
        ];

        return dataColl;
    };
    this.getRefFieldAs = function () {
        var dataColl = [{ id: 1, value: 'TS.AutoVoucherNO', text: 'Auto Number' },
        { id: 2, value: 'TS.AutoManualNo', text: 'Voucher No.' },
        { id: 3, value: 'TS.[No]', text: 'Ref. No.' },
        { id: 4, value: 'ADS.SalesTaxNo', text: 'Pan/Vat No.' },
        { id: 5, value: 'ADS.PhoneNo', text: 'Phone No.' },
        { id: 6, value: 'ADS.OwnerContactNo', text: 'Owner Contact No' },
        { id: 7, value: 'ADS.DriverContactNo', text: 'Driver Contact No' },
        { id: 8, value: 'ADS.Goods', text: 'Vehicle No.' },
        { id: 9, value: 'ADS.PPNo', text: 'P.P. No.' },
        { id: 10, value: 'ADS.JobCardNo', text: 'JobCard No.' }];
        return dataColl;
    };
    this.getAcademicConfig = function () {

        return $http({
            method: 'POST',
            url: base_url + "Academic/Setup/GetAcademicConfiguration",
            dataType: "json"
        });
    };

    this.getConfirmMSG = function ()
    {
        var confirmMSG = {
            Accept: false,
            Decline: false,
            Delete: false,
            Modify: false,
            Print: false,
            Reset: false
        };

        return confirmMSG;
    }

    this.getDeviceCompany = function () {
        var dataColl = [{ id: 1, text: 'Real Time' }, { id: 2, text: 'Degicom' }];
        return dataColl;
    };
    this.getPeriodOptions = function () {
        var dataColl = [
            { id: 1, text: 'Period' },
            { id: 2, text: 'MonthWise' }
        ];
        return dataColl;
    }
    this.getUDFTypes = function () {          
        var dataColl = [{ id: 1, text: 'String' }, { id: 2, text: 'WholeNumberOnly' }, { id: 3, text: 'DecimalNumber' }, { id: 4, text: 'Date' }, { id: 5, text: 'DateTime' }, { id: 6, text: 'YesNo' },
                        { id: 7, text: 'Memo' }, { id: 8, text: 'List' }        ];
        return dataColl;
    };
    this.getWeekDayNameList = function () {
        var dataColl = [{ id: 1, text: 'Sunday' }, { id: 2, text: 'Monday' }, { id: 3, text: 'Tuesday' }, { id: 4, text: 'Wednesday' }, { id: 5, text: 'Thursday' }, { id: 6, text: 'Friday' }, { id: 7, text: 'Saturday' }];
        return dataColl;
    };

    this.getPerPageList = function ()
    {
        var dataColl = [{ value: 0, text: 'All' },{ value: 5, text: 5 }, { value: 10, text: 10 }, { value: 25, text: 25 }, { value: 50, text: 50 }, { value: 100, text: 100 }, { value: 500, text: 500 }];
        return dataColl;
    };

    this.getPerPageRow = function () {
        return 10;
    }
    this.getStudentSearchOptions = function () {
        //var dataColl = [{ text: 'All', value: 'ST.SearchOption' },{ text: 'Name', value: 'ST.Name' }, { text: 'Regd.No.', value: 'ST.RegNo' }, { text: 'Roll No.', value: 'ST.RollNo' }, { text: 'Id', value: 'ST.AutoNumber' }, { text: 'LedgerPanaNo', value: 'ST.LedgerPanaNo' }, { text: 'ContactNo', value: 'ST.F_ContactNo' }, { text: 'FirstName', value: 'ST.FirstName' }, { text: 'LastName', value: 'ST.LastName' }, { text: 'First&LastName', value: 'ST.FirstLastName' }];
        var dataColl = [{ text: 'All', value: 'All' }, { text: 'Name', value: 'ST.Name' }, { text: 'Regd.No.', value: 'ST.RegNo' }, { text: 'Roll No.', value: 'ST.RollNo' }, { text: 'Id', value: 'ST.AutoNumber' }, { text: 'LedgerPanaNo', value: 'ST.LedgerPanaNo' }, { text: 'ContactNo', value: 'ST.F_ContactNo' }, { text: 'FirstName', value: 'ST.FirstName' }, { text: 'LastName', value: 'ST.LastName' }, { text: 'First&LastName', value: 'ST.FirstLastName' }, { text: 'FatherName', value: 'ST.FatherName' }, { text: 'IEMIS Code', value: 'ST.EMSId' }];
        return dataColl;
    };
    this.getEmployeeSearchOptions = function () {
        var dataColl = [{ text: 'Name', value: 'E.Name' }, { text: 'Code', value: 'E.EmployeeCode' }, { text: 'Id', value: 'E.EmployeeId' }];
        return dataColl;
    };
    this.getNumberingMethod = function () {
        var dataColl = [{ text: 'Auto', value: 1,id:1 }, { text: 'Manual', value: 2,id:2 }, { text: 'None', value: 3,id:3 }];
        return dataColl;
    };
    this.getCostClassForRpt = function (voucherId) {

        return $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetCostClasssRpt",
            dataType: "json"
        })
    };
    this.getCustomRptColumns = function (EntityId) {

        var para = {
            EntityId: EntityId
        }

        return $http({
            method: 'POST',
            url: base_url + "Global/GetCustomColForRptSetup",
            dataType: "json",
            data: JSON.stringify(para)
        });
    };
    this.getAuditLog = function (entityId, tranId) {

        return $http({
            method: 'GET',
            url: base_url + "Global/GetAuditLog?EntityId=" + entityId + "&TranId=" + tranId,
            dataType: "json"
        });
    };
    this.getExpensesLedger = function (voucherId) {

        return $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetExpensesLedger",
            dataType: "json"
        })
    };
    this.getMonthList = function () {
        var dataColl = [];

        if ($rootScope.LANG == 'in') {
            dataColl = [
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
        } else {
            dataColl = [
                { id: 1, text: 'Baishakh' },
                { id: 2, text: 'Jestha' },
                { id: 3, text: 'Ashadh' },
                { id: 4, text: 'Shrawan' },
                { id: 5, text: 'Bhadau' },
                { id: 6, text: 'Ashwin' },
                { id: 7, text: 'Kartik' },
                { id: 8, text: 'Mangsir' },
                { id: 9, text: 'Poush' },
                { id: 10, text: 'Magh' },
                { id: 11, text: 'Falgun' },
                { id: 12, text: 'Chaitra' },
                { id: 13, text: 'Baishakh-2079' },
                { id: 14, text: 'Jestha-2079' },
                { id: 15, text: 'Ashadh-2079' }
            ];
        }
        

        return dataColl;
    };

    this.getPayRollMonthList = function () {
        var dataColl = [];

        if ($rootScope.LANG == 'in') {
            dataColl = [
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
        } else {
            dataColl = [
                { id: 1, text: 'Baishakh' },
                { id: 2, text: 'Jestha' },
                { id: 3, text: 'Ashadh' },
                { id: 4, text: 'Shrawan' },
                { id: 5, text: 'Bhadau' },
                { id: 6, text: 'Ashwin' },
                { id: 7, text: 'Kartik' },
                { id: 8, text: 'Mangsir' },
                { id: 9, text: 'Poush' },
                { id: 10, text: 'Magh' },
                { id: 11, text: 'Falgun' },
                { id: 12, text: 'Chaitra' }
            ];
        }


        return dataColl;
    };

    this.getMonthListFromDB = function () {

        return $http({
            method: 'GET',
            url: base_url + "Global/GetMonthNameList",
            dataType: "json"
        })
    };

    this.getAcademicMonthList= function (studentId,classId) {

        var para = {
            StudentId: studentId,
            ClassId:classId
        };
        return $http({
            method: 'POST',
            url: base_url + "Global/GetAcademicMonthList",
            dataType: "json",
            data: JSON.stringify(para)
        });
    };

    this.getYearList = function () {
        var dataColl = [];
        if ($rootScope.LANG == 'in')
        {
            dataColl = [
                { id: 2021, text: '2021' },
                { id: 2022, text: '2022' },
                { id: 2023, text: '2023' },
                { id: 2024, text: '2024' },
                { id: 2025, text: '2025' },
                { id: 2026, text: '2026' },
            ];
        } else {
            dataColl = [
                { id: 2078, text: '2078' },
                { id: 2079, text: '2079' },
                { id: 2080, text: '2080' },
                { id: 2081, text: '2081' },
                { id: 2082, text: '2082' },
                { id: 2083, text: '2083' },
                { id: 2084, text: '2084' },
                { id: 2085, text: '2085' },
            ];
        }
        

        return dataColl;
    };
    //this.getYearList = function () {
    //    var dataColl = [];

    //    if (currentDateStyle == 1) {
    //        dataColl = [
    //            { id: 2021, text: '2021' },
    //            { id: 2022, text: '2022' },
    //            { id: 2023, text: '2023' },
    //            { id: 2024, text: '2024' },
    //            { id: 2025, text: '2025' },
    //            { id: 2026, text: '2026' },
    //        ];
    //    } else {
    //        dataColl = [
    //            { id: 2078, text: '2078' },
    //            { id: 2079, text: '2079' },
    //            { id: 2080, text: '2080' },
    //            { id: 2081, text: '2081' },
    //            { id: 2082, text: '2082' },
    //            { id: 2083, text: '2083' },
    //            { id: 2084, text: '2084' },
    //            { id: 2085, text: '2085' },
    //        ];
    //    }

    //    return dataColl;
    //};


    this.getDateStyle = function () {

        var dataColl = [
            { id: 1, text: 'A.D.' },
            { id: 2, text: 'B.S.' }
        ];
        return dataColl;
    };

    this.getDateFormat = function () {

        var dataColl = [
            { id: 1, text: 'DD-MM-YYYY' },
            { id: 2, text: 'MM-DD-YYYY' },
            { id: 3, text: 'YYYY-MM-DD' },
            { id: 4, text: 'YYYY-MM' },
            { id: 5, text: 'MM-YYYY' }
        ];
        return dataColl;
    };
    this.getDateDet = function (dt) {

        dt = new Date(dt);
        var bsDateDet1 = NepaliFunctions.AD2BS({ year: dt.getFullYear(), month: dt.getMonth() + 1, day: dt.getDate() });

        var bsDate1 = bsDateDet1.year + '-' + bsDateDet1.month.toString().padStart(2, '0') + '-' + bsDateDet1.day.toString().padStart(2, '0');

        var dateDetail = {
            dateAD: dt,
            dateBS: bsDate1,
            NY: bsDateDet1.year,
            NM: bsDateDet1.month,
            ND: bsDateDet1.day
        };

        return dateDetail;
    };
    this.getFormActions = function () {

        var dataColl = [
            { id: 1, text: 'Save' },
            { id: 2, text: 'Modify' },
            { id: 3, text: 'Delete' },
            { id: 4, text: 'Print' },
            { id: 5, text: 'View' }
        ];
        return dataColl;
    };
    this.getRefQtyAs = function () {

        var dataColl = [
            { id: 0, text: 'LESS_THEN_EQUAL' },
            { id: 1, text: 'GREATER_THEN_EQUAL' },
            { id: 2, text: 'NOT_EDIT_ABLE' },
            { id: 3, text: 'NOT_VALIDATE' }
        ];
        return dataColl;
    };

    this.getAdditionDeducationSign = function () {
        var dataColl = [{ id: true, text: '+' }, { id: false, text: '-' }];
        return dataColl;
    };
    this.getGenderList = function () {

        var dataColl = [
            { id: 1, text: 'Male' },
            { id: 2, text: 'Female' },
            { id: 3, text: 'Other' }
        ];

        return dataColl;
    };

    this.getLangList = function () {

        var dataColl = [
            { id: 1, text: 'Nepali' },
            { id: 2, text: 'English' },
            { id: 3, text: 'Hindi' }
        ];

        return dataColl;
    };
    this.getStatusList = function () {

        var dataColl = [
            { id: 1, text: 'ACTIVE' },
            { id: 2, text: 'DE-ACTIVE' }            
        ];

        return dataColl;
    };

    this.getLeaveTypeApplicableForList = function () {
        var dataColl = [{ id: 1, text: 'EMPLOYEE' }, { id: 2, text: 'STUDENT' }, { id: 3, text: 'BOTH' }];
        return dataColl;
    };

    this.getPaperTypeList = function () {

        var dataColl = [
            { id: 1, text: 'TH' },
            { id: 2, text: 'PR' },
            { id: 3, text: 'BOTH' },
            { id: 4, text: 'GRADING' }
        ];

        return dataColl;
    };

    this.getBloodGroupList = function () {
        return ['A+', 'A-', 'B+', 'B-', 'O+', 'O-', 'AB+', 'AB-'];
    };

    this.getReligionList = function () {
        return ["Hinduism", "Islam", "Buddhism", "Christian", "Jainism", "Sikhism", "Judaism"];
    };

    this.getNationalityList = function () {
        return ["Nepali", "Indian"];
    };
     
    this.getCountryList = function () {
        return ["Nepal", "India"];
    };

    this.getDisablityList = function () {
        return ["N/A", "Physical", "Mental", "Deaf", "Blind", "Low Vision", "Deaf and Blind", "Speech Impairment", "Multiple Disability"];
    };

    this.getMaritaStatusList = function () {
        return ["Married", "Unmarried", "Widow", "Widower", "OTHERS"];
    };

    this.getFeeHeadForList = function () {

        var dataColl = [
            { id: 1, text: 'General Fees' },
            { id: 2, text: 'Transport Fees' },
            { id: 3, text: 'Hostel Fees' },
            { id: 4, text: 'DayBoarders Fees' },
            { id: 5, text: 'Canteen Fees' },
            { id: 6, text: 'Library Fees' },
            { id: 7, text: 'Inventory Fees' },
            { id: 8, text: 'Extra Curricular Fees' }
        ];
        return dataColl;        
    };

    this.getExamTypeList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/GetAllExamTypeList",
            dataType: "json"
        })
    };
    this.getReExamTypeList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/GetAllReExamTypeList",
            dataType: "json"
        })
    };
    this.getExamTypeGroupList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/GetAllExamTypeGroupList",
            dataType: "json"
        })
    };

    this.getCasteList = function ()
    {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllCasteList",
            dataType: "json"
        })
    };
    this.getFeeItemList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Fee/Creation/GetAllFeeItemList",
            dataType: "json"
        })
    };

    this.getClassList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllClassList",
            dataType: "json"
        })
    };
    this.getClassListForOR = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllClassListForOR",
            dataType: "json"
        })
    };

    this.getSectionList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllSectionList",
            dataType: "json"
        })
    };
    this.getSectionForTran = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllSectionForTran",
            dataType: "json"
        })
    };
    this.getClassSectionList = function (forRunning) {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetClassSectionList?ForRunning="+forRunning,
            dataType: "json"
        })
    };
    this.getClassShiftList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllClassShift",
            dataType: "json"
        })
    };
    this.getAcademicYearList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllAcademicYearList",
            dataType: "json"
        })
    };


    this.getStudentTypeList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllStudentTypeList",
            dataType: "json"
        })
    };

    this.getMediumList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllMediumList",
            dataType: "json"
        })
    };

    this.getHouseNameList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllHouseNameList",
            dataType: "json"
        })
    };

    this.getHouseDressList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllHouseDressList",
            dataType: "json"
        })
    };

    this.getBoardList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllBoardList",
            dataType: "json"
        })
    };
    this.getClassLevelList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllClassLevelList",
            dataType: "json"
        })
    };
    this.getFacultyList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllFacultyList",
            dataType: "json"
        })
    };
    this.getSemesterList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllSemesterList",
            dataType: "json"
        })
    };

    this.getBatchList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllBatchList",
            dataType: "json"
        })
    };

    this.getClassYearList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllClassYearList",
            dataType: "json"
        })
    };

    this.getDocumentTypeList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllDocumentTypeList",
            dataType: "json"
        })
    };

    this.getBookTitleList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Library/Master/GetAllBookTitleList",
            dataType: "json"
        })
    };

    this.getAuthorList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Library/Master/GetAllAuthorList",
            dataType: "json"
        })
    };

    this.getEditionList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Library/Master/GetAllEditionList",
            dataType: "json"
        })
    };

    this.getPublicationList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Library/Master/GetAllPublicationList",
            dataType: "json"
        })
    };

    this.getSubjectList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllSubjectList",
            dataType: "json"
        })
    };

    this.getDonorList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Library/Master/GetAllDonorList",
            dataType: "json"
        })
    };

    this.getRackList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Library/Master/GetAllRackList",
            dataType: "json"
        })
    };

    this.getDepartmentList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllDepartmentList",
            dataType: "json"
        })
    };
    this.getLibDepartmentList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Library/Master/GetAllDepartmentList",
            dataType: "json"
        })
    };
    this.getMaterialTypeList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Library/Master/GetAllMaterialTypeList",
            dataType: "json"
        })
    };

    this.getDesignationList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllDesignationList",
            dataType: "json"
        })
    };
    this.getServiceTypeList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllServiceTypeList",
            dataType: "json"
        })
    };
    this.getLevelList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllLevelList",
            dataType: "json"
        })
    };
    this.getCategoryList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllCategoryList",
            dataType: "json"
        })
    };

    this.GetClassWiseStudentList = function (classId, sectionIdColl, BatchId, ClassYearId, SemesterId) {
        var para = {
            ClassId: classId,
            SectionIdColl: (sectionIdColl && sectionIdColl.length > 0 ? sectionIdColl.toString() : ''),            
            SemesterId: SemesterId,
            ClassYearId: ClassYearId,
            BatchId: BatchId,
            
        };
        return $http({
            method: 'POST',
            url: base_url + "Academic/Transaction/GetClassWiseStudentList",
            dataSchedule: "json",
            data: JSON.stringify(para)
        });
    };

    this.GetEmployeeList = function () {
        
        return $http({
            method: 'POST',
            url: base_url + "Academic/Report/GetEmpSummary",
            dataSchedule: "json",
         //   data: JSON.stringify(para)
        });
    };

    this.getDrCr = function () {

        var dataColl = [
            { id: 1, text: 'DR' },
            { id: 2, text: 'CR' }
        ];
        return dataColl;
    };


    this.GetWeeklyoff = function () {

        var dataColl = [
            { id: 1, text: 'Sunday' },
            { id: 2, text: 'Munday' },
            { id: 3, text: 'Tuesday' },
            { id: 4, text: 'Wednesday' },
            { id: 5, text: 'Thursday' },
            { id: 6, text: 'Friday' },
            { id: 7, text: 'Saturday' },


        ];

        return dataColl;
    };
    this.GetWeeklyoffType = function () {

        var dataColl = [
            { id: 1, text: 'None' },
            { id: 2, text: 'HalfDay' },
            { id: 3, text: 'FullDay' },

        ];

        return dataColl;
    };
    this.GetSinglePunchPolicy = function () {

        var dataColl = [
            { id: 1, text: 'None' },
            { id: 2, text: 'Absent On Single Punch' },
            { id: 3, text: 'Half Day On Single Punch' },

        ];

        return dataColl;
    };
    this.GetOtCalculation = function () {

        var dataColl = [
            { id: 1, text: 'OT = OutTime-ShiftEndtime' },
            { id: 2, text: 'OT = Workinghrs-Shifthrs' },
            { id: 3, text: 'OT =EarlyComing+LateDeparture' },
            { id: 4, text: 'OT =NOT ALLOW' }

        ];

        return dataColl;
    };
    this.GetShowAsForLateearly = function () {

        var dataColl = [
            { id: 1, text: 'None' },
            { id: 2, text: 'Cut Half Day' },
            { id: 3, text: 'Cut Full Day' },
        ];

        return dataColl;
    };
    this.GetCutDay = function () {

        var dataColl = [
            { id: 1, text: 'Half Day' },
            { id: 2, text: 'Absent' },

        ];

        return dataColl;
    };

    this.GetAttendanceMode = function () {
        var dataColl = [
            { id: 0, text: 'Both' },
            { id: 1, text: 'InOnly' },
            { id: 2, text: 'OutOnly' }
        ];
        return dataColl;
    };

    this.getFreightTypes = function () {
        return $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetAllFreightType",
            dataType: "json"
        })
    };
    this.getPaymentTypeColl = function () {

        var dataColl = [
            { id: 1, text: 'CASH' }, { id: 2, text: 'CREDIT' }
        ];
        return dataColl;
    };

    this.getGenConfig = function () {
        return $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetGeneralConfiguration",
            dataType: "json"
        })
    };
    this.getButtonDisabled = function (entityId) {

        var para = {
            EntityId: entityId
        };
        return $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetEntityButtonDisable",
            dataType: "json",
            data: JSON.stringify(para)
        });
    };
    this.getCompanyDet = function () {

        return $http({
            method: 'GET',
            url: base_url + "Global/GetCompanyDetail",
            dataType: "json"
        })
    };
    this.getCurrentDateTime = function () {

        return $http({
            method: 'GET',
            url: base_url + "Global/GetCurrentDate",
            dataType: "json"
        })
    };
    this.getLastEntryDate = function (voucherid) {

        return $http({
            method: 'GET',
            url: base_url + "Global/GetLastEntryDate?voucherid=" + voucherid,
            dataType: "json"
        })
    };
    this.getVoucherDataAs = function () {

        var dataColl = [
            { id: 1, text: 'Current Date' },
            { id: 2, text: 'Last Entry Date' }
        ];
        return dataColl;
    };
    this.getSalesLedger = function () {

        return $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetSalesLedger",
            dataType: "json"
        })
    };

    this.getPurchaseLedger = function () {

        return $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetPurchaseLedger",
            dataType: "json"
        })
    };
    this.getTransactionEntity = function () {

        return $http({
            method: 'GET',
            url: base_url + "Global/GetTransactionEntityLst",
            dataType: "json"
        })
    };
    this.getReportEntity = function () {

        return $http({
            method: 'GET',
            url: base_url + "Global/GetReportEntityLst",
            dataType: "json"
        })
    };
    this.getCompanyPeriodMonth = function (costClassId) {

        if (costClassId > 0) {

            return $http({
                method: 'GET',
                url: base_url + "Global/GetCostClassPeriod?CostClassId=" + costClassId,
                dataType: "json"
            })

        } else {
            return $http({
                method: 'GET',
                url: base_url + "Global/GetCompanyPeriodMonth",
                dataType: "json"
            })
        }

    };
    this.getCompanyPeriodMonth = function () {

        return $http({
            method: 'GET',
            url: base_url + "Global/GetCompanyPeriodMonth",
            dataType: "json"
        })
    };
    this.getAcademicPeriod = function () {
        return $http({
            method: 'GET',
            url: base_url + "Global/GetAcademicPeriod",
            dataType: "json"
        })
    };
    this.getPaymentTerms = function () {
        return $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetAllPaymentTerms",
            dataType: "json"
        })
    };
    this.GenerateOTP = function (entityId, emailId, mobileNo, modalId) {
      
        showPleaseWait();
        var para = {
            EntityId: entityId,
            MobileNo: mobileNo,
            EmailId: emailId
        };
        $http({
            method: 'POST',
            url: base_url + "Global/GenerateOTP",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (sRes) {
            hidePleaseWait();
           

            if (sRes.data.IsSuccess == false)
                Swal.fire(sRes.data.ResponseMSG);
            else {
                if (modalId) {
                    $('#' + modalId).modal('show');
                }
            }
        });
    };

    this.IsValidOTP = function (entityId, otp, modalId) {
     
        showPleaseWait();
        var para = {
            EntityId: entityId,
            OTP: otp,
        };
        $http({
            method: 'POST',
            url: base_url + "Global/IsValidOTP",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (sRes) {
            hidePleaseWait();
    
            if (sRes.data.IsSuccess == false)
                Swal.fire(sRes.data.ResponseMSG);
            else {
                if (modalId) {
                    $('#' + modalId).modal('hide');
                }
            }
        });
    };

    this.createElementForMenu = function () {
        const contextMenu = document.createElement('div');
        contextMenu.style.position = 'absolute';
        contextMenu.style.backgroundColor = 'white';
        contextMenu.style.border = '1px solid black';
        contextMenu.style.padding = '5px';
        contextMenu.style.display = 'none';
        contextMenu.style.maxHeight = '200px';
        contextMenu.style.overflowY = 'scroll';
        document.body.appendChild(contextMenu);
        return contextMenu;
    };
    this.onCellContextMenu = function (event, grid, menu) {
        // Prevent the default context menu
        event.event.preventDefault();

        // Get the column definitions
        const columns = grid.columnApi.getAllColumns();

        // Create context menu items for each column
        menu.innerHTML = columns.map(col => `
            <div>
                <label>
                    <input type="checkbox" ${col.isVisible() ? 'checked' : ''} 
                           data-col-id="${col.getColId()}">
                    ${col.colDef.headerName}
                </label>
            </div>
        `).join('');

        // Position the context menu at the mouse pointer
        menu.style.left = `${event.event.clientX}px`;
        menu.style.top = `${event.event.clientY}px`;
        menu.style.display = 'block';

        // Add event listeners to checkboxes
        menu.querySelectorAll('input').forEach(checkbox => {
            checkbox.addEventListener('change', function () {
                const colId = this.getAttribute('data-col-id');
                const visible = this.checked;

                // Show or hide the column
                grid.columnApi.setColumnVisible(colId, visible);
            });
        });
    }
    this.saveRptListState = function (EntityId, grid) {
        const columnState = grid.columnApi.getColumnState();
        var state = JSON.stringify(columnState);

        $http({
            method: 'POST',
            url: base_url + "Global/SaveListState",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", data.jsonData);
                formData.append("entityId", EntityId);
                formData.append("isReport", true);

                return formData;
            },
            data: { jsonData: state }
        }).then(function (res) {
            Swal.fire(res.data.ResponseMSG);
        }, function (errormessage) {
            hidePleaseWait();
        });
    }
    this.getListState = function (EntityId, grid) {
        $http({
            method: 'GET',
            url: base_url + "Global/GetListState?entityId=" + EntityId + "&isReport=true",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                var state = JSON.parse(res.data.Data);
                if (state && state.length > 0) {
                    $timeout(function () {
                        grid.columnApi.setColumnState(state);
                    });
                }
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    }
    this.getRptTableColColl = function () {
        var dataColl = [
            { name: 'N_C1', text: 'Number Column 1' },
            { name: 'N_C2', text: 'Number Column 2' },
            { name: 'N_C3', text: 'Number Column 3' },
            { name: 'N_C4', text: 'Number Column 4' },

            { name: 'T_C1', text: 'Text Column 1' },
            { name: 'T_C2', text: 'Text Column 2' },
            { name: 'T_C3', text: 'Text Column 3' },
            { name: 'T_C4', text: 'Text Column 4' },

            { name: 'D_C1', text: 'Date Column 1' },
            { name: 'D_C2', text: 'Date Column 2' },
            { name: 'D_C3', text: 'Date Column 3' },
            { name: 'D_C4', text: 'Date Column 4' },

            { name: 'DT_C1', text: 'DateTime Column 1' },
            { name: 'DT_C2', text: 'DateTime Column 2' },
            { name: 'DT_C3', text: 'DateTime Column 3' },
            { name: 'DT_C4', text: 'DateTime Column 4' },

            { name: 'A_C1', text: 'Amount Column 1' },
            { name: 'A_C2', text: 'Amount Column 2' },
            { name: 'A_C3', text: 'Amount Column 3' },
            { name: 'A_C4', text: 'Amount Column 4' },
        ];
        return dataColl;
    };

    this.saveListState = function (entityId,state) {
        //var state = $scope.gridApi.saveState.save();

        if (entityId > 0 && state) {
            $http({
                method: 'POST',
                url: base_url + "Global/SaveListState",
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    var formData = new FormData();
                    formData.append("jsonData", JSON.stringify(data.jsonData));
                    formData.append("entityId", entityId);
                    formData.append("isReport", true);
                    return formData;
                },
                data: { jsonData: state }
            }).then(function (res) {               
                Swal.fire(res.data.ResponseMSG);
            }, function (errormessage) {
                hidePleaseWait(); 
            });
        } 
    };

    //ClosingQty,ActualQty,BilledQty,FreeQty,Rate,DiscountPer,DiscountAmt,Amount,NetWeight,LossRate,LossWeight,FineRate,FineWeight, ProcessingRate,ProcessingWeight,ALValue1
    //ALValue2,ALValue3,Makeing,Stone,TotalAmount,ExDutyAmount,TaxableAmt,VatAmount,MRP, PurchaseRate,TradeRate

    //cq,aq,bq,fq,rate,dp,da,amt,nw,lr,lw,fr,fw,pr,pw,av1,av2,av2,make,stone,tamt,eamt,taxamt,vamt,mrp,pr,tr

    this.getItemUDFFormula = function (curRow, dataColl, tran, colName) {


        if (curRow.UDFFeildsColl && curRow.UDFFeildsColl.length > 0) {
            angular.forEach(curRow.UDFFeildsColl, function (udf) {
                if (udf.Formula && udf.Formula.length > 0 && colName != udf.NameId) {
                    var formula = udf.Formula;
                    formula = formula.replaceAll('cq', isEmptyNum(curRow.BalanceQty));
                    formula = formula.replaceAll('aq', isEmptyNum(curRow.ActualQty));
                    formula = formula.replaceAll('bq', isEmptyNum(curRow.BilledQty));
                    formula = formula.replaceAll('fq', isEmptyNum(curRow.FreeQty));
                    formula = formula.replaceAll('rate', isEmptyNum(curRow.Rate));
                    formula = formula.replaceAll('dp', isEmptyNum(curRow.DiscountPer));
                    formula = formula.replaceAll('da', isEmptyNum(curRow.DiscountAmt));
                    formula = formula.replaceAll('amt', isEmptyNum(curRow.Amount));
                    formula = formula.replaceAll('nw', isEmptyNum(curRow.NetWeight));
                    formula = formula.replaceAll('lr', isEmptyNum(curRow.LossRate));
                    formula = formula.replaceAll('lw', isEmptyNum(curRow.LossWeight));
                    formula = formula.replaceAll('fr', isEmptyNum(curRow.FineRate));
                    formula = formula.replaceAll('fw', isEmptyNum(curRow.FineWeight));
                    formula = formula.replaceAll('pr', isEmptyNum(curRow.ProcessingRate));
                    formula = formula.replaceAll('pw', isEmptyNum(curRow.ProcessingWeight));
                    formula = formula.replaceAll('av1', isEmptyNum(curRow.ALValue1));
                    formula = formula.replaceAll('av2', isEmptyNum(curRow.ALValue2));
                    formula = formula.replaceAll('av2', isEmptyNum(curRow.ALValue3));
                    formula = formula.replaceAll('make', isEmptyNum(curRow.Makeing));
                    formula = formula.replaceAll('stone', isEmptyNum(curRow.Stone));
                    formula = formula.replaceAll('tamt', isEmptyNum(curRow.TotalAmount));
                    formula = formula.replaceAll('eamt', isEmptyNum(curRow.ExDutyAmount));
                    formula = formula.replaceAll('taxamt', isEmptyNum(curRow.TaxableAmt));
                    formula = formula.replaceAll('vamt', isEmptyNum(curRow.VatAmount));
                    formula = formula.replaceAll('mrp', isEmptyNum(curRow.MRP));
                    formula = formula.replaceAll('pr', isEmptyNum(curRow.PurchaseRate));
                    formula = formula.replaceAll('tr', isEmptyNum(curRow.TradeRate));
                    formula = formula.replaceAll('sr', isEmptyNum(curRow.SalesRate));

                    angular.forEach(curRow.UDFFeildsColl, function (udf1) {
                        var uname = 'u' + udf1.SNo;
                        formula = formula.replaceAll(uname, isEmptyNum(udf1.UDFValue));
                    });

                    try {
                        if (udf.IsManual == true) {

                        } else {
                            var nVal = math.evaluate(formula);
                            udf.UDFValue = isEmptyNum(nVal);
                        }
                    } catch { }

                } else if (udf.NameId == colName) {
                    udf.IsManual = true;
                }
            });
        }

        if (curRow.productDetail) {
            if (curRow.productDetail.SalesRateFormula && curRow.productDetail.SalesRateFormula.length > 0) {
                var formula = curRow.productDetail.SalesRateFormula;
                formula = formula.replaceAll('cq', isEmptyNum(curRow.BalanceQty));
                formula = formula.replaceAll('aq', isEmptyNum(curRow.ActualQty));
                formula = formula.replaceAll('bq', isEmptyNum(curRow.BilledQty));
                formula = formula.replaceAll('fq', isEmptyNum(curRow.FreeQty));
                formula = formula.replaceAll('rate', isEmptyNum(curRow.Rate));
                formula = formula.replaceAll('dp', isEmptyNum(curRow.DiscountPer));
                formula = formula.replaceAll('da', isEmptyNum(curRow.DiscountAmt));
                formula = formula.replaceAll('amt', isEmptyNum(curRow.Amount));
                formula = formula.replaceAll('nw', isEmptyNum(curRow.NetWeight));
                formula = formula.replaceAll('lr', isEmptyNum(curRow.LossRate));
                formula = formula.replaceAll('lw', isEmptyNum(curRow.LossWeight));
                formula = formula.replaceAll('fr', isEmptyNum(curRow.FineRate));
                formula = formula.replaceAll('fw', isEmptyNum(curRow.FineWeight));
                formula = formula.replaceAll('pr', isEmptyNum(curRow.ProcessingRate));
                formula = formula.replaceAll('pw', isEmptyNum(curRow.ProcessingWeight));
                formula = formula.replaceAll('av1', isEmptyNum(curRow.ALValue1));
                formula = formula.replaceAll('av2', isEmptyNum(curRow.ALValue2));
                formula = formula.replaceAll('av2', isEmptyNum(curRow.ALValue3));
                formula = formula.replaceAll('make', isEmptyNum(curRow.Makeing));
                formula = formula.replaceAll('stone', isEmptyNum(curRow.Stone));
                formula = formula.replaceAll('tamt', isEmptyNum(curRow.TotalAmount));
                formula = formula.replaceAll('eamt', isEmptyNum(curRow.ExDutyAmount));
                formula = formula.replaceAll('tamt', isEmptyNum(curRow.TaxableAmt));
                formula = formula.replaceAll('vamt', isEmptyNum(curRow.VatAmount));
                formula = formula.replaceAll('mrp', isEmptyNum(curRow.MRP));
                formula = formula.replaceAll('pr', isEmptyNum(curRow.PurchaseRate));
                formula = formula.replaceAll('tr', isEmptyNum(curRow.TradeRate));
                formula = formula.replaceAll('sr', isEmptyNum(curRow.SalesRate));

                angular.forEach(curRow.UDFFeildsColl, function (udf1) {
                    var uname = 'u' + udf1.SNo;
                    formula = formula.replaceAll(uname, isEmptyNum(udf1.UDFValue));
                });

                try {
                    var nVal = math.evaluate(formula);
                    curRow.Rate = isEmptyNum(nVal);
                } catch { }
            }

            if (curRow.productDetail.PurchaseRateFormula && curRow.productDetail.PurchaseRateFormula.length > 0) {
                var formula = curRow.productDetail.PurchaseRateFormula;
                formula = formula.replaceAll('cq', isEmptyNum(curRow.BalanceQty));
                formula = formula.replaceAll('aq', isEmptyNum(curRow.ActualQty));
                formula = formula.replaceAll('bq', isEmptyNum(curRow.BilledQty));
                formula = formula.replaceAll('fq', isEmptyNum(curRow.FreeQty));
                formula = formula.replaceAll('rate', isEmptyNum(curRow.Rate));
                formula = formula.replaceAll('dp', isEmptyNum(curRow.DiscountPer));
                formula = formula.replaceAll('da', isEmptyNum(curRow.DiscountAmt));
                formula = formula.replaceAll('amt', isEmptyNum(curRow.Amount));
                formula = formula.replaceAll('nw', isEmptyNum(curRow.NetWeight));
                formula = formula.replaceAll('lr', isEmptyNum(curRow.LossRate));
                formula = formula.replaceAll('lw', isEmptyNum(curRow.LossWeight));
                formula = formula.replaceAll('fr', isEmptyNum(curRow.FineRate));
                formula = formula.replaceAll('fw', isEmptyNum(curRow.FineWeight));
                formula = formula.replaceAll('pr', isEmptyNum(curRow.ProcessingRate));
                formula = formula.replaceAll('pw', isEmptyNum(curRow.ProcessingWeight));
                formula = formula.replaceAll('av1', isEmptyNum(curRow.ALValue1));
                formula = formula.replaceAll('av2', isEmptyNum(curRow.ALValue2));
                formula = formula.replaceAll('av2', isEmptyNum(curRow.ALValue3));
                formula = formula.replaceAll('make', isEmptyNum(curRow.Makeing));
                formula = formula.replaceAll('stone', isEmptyNum(curRow.Stone));
                formula = formula.replaceAll('tamt', isEmptyNum(curRow.TotalAmount));
                formula = formula.replaceAll('eamt', isEmptyNum(curRow.ExDutyAmount));
                formula = formula.replaceAll('tamt', isEmptyNum(curRow.TaxableAmt));
                formula = formula.replaceAll('vamt', isEmptyNum(curRow.VatAmount));
                formula = formula.replaceAll('mrp', isEmptyNum(curRow.MRP));
                formula = formula.replaceAll('pr', isEmptyNum(curRow.PurchaseRate));
                formula = formula.replaceAll('tr', isEmptyNum(curRow.TradeRate));
                formula = formula.replaceAll('sr', isEmptyNum(curRow.SalesRate));

                angular.forEach(curRow.UDFFeildsColl, function (udf1) {
                    var uname = 'u' + udf1.SNo;
                    formula = formula.replaceAll(uname, isEmptyNum(udf1.UDFValue));
                });

                try {
                    var nVal = math.evaluate(formula);
                    curRow.Rate = isEmptyNum(nVal);
                } catch { }
            }
        }


        var t_aq = 0, t_bq = 0, t_fq = 0, t_da = 0, t_amt = 0, t_tamt = 0, t_eamt = 0, t_taxamt = 0, t_vamt = 0;
        angular.forEach(dataColl, function (dc) {
            if (dc.RowType == 'P') {
                t_aq += isEmptyNum(dc.ActualQty);
                t_bq += isEmptyNum(dc.BilledQty);
                t_fq += isEmptyNum(dc.FreeQty);
                t_da += isEmptyNum(dc.DiscountAmt);
                t_amt += isEmptyNum(dc.Amount);
                t_tamt += isEmptyNum(dc.TotalAmount);
                t_eamt += isEmptyNum(dc.ExDutyAmount);
                t_taxamt += isEmptyNum(dc.TaxableAmt);
                t_vamt += isEmptyNum(dc.VatAmount);
            }
        });

        var t_udf = {};
        angular.forEach(dataColl, function (dc) {
            if (dc.RowType == 'P') {
                if (dc.UDFFeildsColl && dc.UDFFeildsColl.length > 0) {
                    angular.forEach(dc.UDFFeildsColl, function (udf) {
                        var uname = 't_u' + udf.SNo;
                        t_udf[uname] = isEmptyNum(t_udf[uname]) + isEmptyNum(udf.UDFValue);
                    });
                }
            }
        });

        angular.forEach(dataColl, function (dc) {
            if (dc.RowType == 'L') {
                if (dc.Formula && dc.Formula.length > 0) {
                    var formula = dc.Formula;
                    formula = formula.replaceAll('t_aq', isEmptyNum(t_aq));
                    formula = formula.replaceAll('t_bq', isEmptyNum(t_bq));
                    formula = formula.replaceAll('t_fq', isEmptyNum(t_fq));
                    formula = formula.replaceAll('t_da', isEmptyNum(t_da));
                    formula = formula.replaceAll('t_amt', isEmptyNum(t_amt));
                    formula = formula.replaceAll('t_tamt', isEmptyNum(t_tamt));
                    formula = formula.replaceAll('t_eamt', isEmptyNum(t_eamt));
                    formula = formula.replaceAll('t_taxamt', isEmptyNum(t_taxamt));
                    formula = formula.replaceAll('t_vamt', isEmptyNum(t_taxamt));

                    angular.forEach(curRow.UDFFeildsColl, function (udf) {
                        var uname = 't_u' + udf.SNo;
                        formula = formula.replaceAll(uname, isEmptyNum(isEmptyNum(t_udf[uname])));
                    });

                    try {
                        if (dc.IsManual == true) {

                        } else {
                            var nVal = math.evaluate(formula);
                            dc.Amount = isEmptyNum(nVal);
                        }

                    } catch { }
                }
            }
        });

        if (tran.UDFFeildsColl && tran.UDFFeildsColl.length > 0) {
            angular.forEach(tran.UDFFeildsColl, function (udf) {
                if (udf.Formula && udf.Formula.length > 0 && udf.NameId != colName) {
                    var formula = udf.Formula;
                    formula = formula.replaceAll('t_aq', isEmptyNum(t_aq));
                    formula = formula.replaceAll('t_bq', isEmptyNum(t_bq));
                    formula = formula.replaceAll('t_fq', isEmptyNum(t_fq));
                    formula = formula.replaceAll('t_da', isEmptyNum(t_da));
                    formula = formula.replaceAll('t_amt', isEmptyNum(t_amt));
                    formula = formula.replaceAll('t_tamt', isEmptyNum(t_tamt));
                    formula = formula.replaceAll('t_eamt', isEmptyNum(t_eamt));
                    formula = formula.replaceAll('t_taxamt', isEmptyNum(t_taxamt));
                    formula = formula.replaceAll('t_vamt', isEmptyNum(t_taxamt));

                    angular.forEach(tran.UDFFeildsColl, function (udf1) {
                        var uname = 'vu' + udf1.SNo;
                        formula = formula.replaceAll(uname, isEmptyNum(udf1.UDFValue));
                    });

                    try {
                        if (udf.IsManual == true) {

                        } else {
                            var nVal = math.evaluate(formula);
                            udf.UDFValue = isEmptyNum(nVal);
                        }

                    } catch { }
                } else if (udf.NameId == colName) {
                    udf.IsManual = true;
                }
            });
        }
    };

    //CurrentBal,DrAmount,CrAmount
    //cb,dr,cr
    this.getLAUDFFormula = function (curRow, dataColl) {

        if (curRow) {
            if (curRow.UDFFeildsColl && curRow.UDFFeildsColl.length > 0) {
                angular.forEach(curRow.UDFFeildsColl, function (udf) {
                    if (udf.Formula && udf.Formula.length > 0) {
                        var formula = udf.Formula;
                        formula = formula.replaceAll('cb', isEmptyNum(curRow.CurrentBal));
                        formula = formula.replaceAll('dr', isEmptyNum(curRow.DrAmount));
                        formula = formula.replaceAll('cr', isEmptyNum(curRow.CrAmount));

                        angular.forEach(curRow.UDFFeildsColl, function (udf1) {
                            var uname = 'u' + udf1.SNo;
                            formula = formula.replaceAll(uname, isEmptyNum(udf1.UDFValue));
                        });

                        try {
                            var nVal = math.evaluate(formula);
                            udf.UDFValue = isEmptyNum(nVal);

                        } catch { }
                    }
                });
            }
        }

    };

    function getFormulaVal(formula, curRow, tran) {

        var t_aq = 0, t_bq = 0, t_fq = 0, t_da = 0, t_amt = 0, t_tamt = 0, t_eamt = 0, t_taxamt = 0, t_vamt = 0;
        angular.forEach(tran.ItemDetailsColl, function (dc) {
            if (dc.RowType == 'P') {
                t_aq += isEmptyNum(dc.ActualQty);
                t_bq += isEmptyNum(dc.BilledQty);
                t_fq += isEmptyNum(dc.FreeQty);
                t_da += isEmptyNum(dc.DiscountAmt);
                t_amt += isEmptyNum(dc.Amount);
                t_tamt += isEmptyNum(dc.TotalAmount);
                t_eamt += isEmptyNum(dc.ExDutyAmount);
                t_taxamt += isEmptyNum(dc.TaxableAmt);
                t_vamt += isEmptyNum(dc.VatAmount);
            }
        });

        var t_udf = {};
        angular.forEach(tran.ItemDetailsColl, function (dc) {
            if (dc.RowType == 'P') {
                if (dc.UDFFeildsColl && dc.UDFFeildsColl.length > 0) {
                    angular.forEach(dc.UDFFeildsColl, function (udf) {
                        var uname = 't_u' + udf.SNo;
                        t_udf[uname] = isEmptyNum(t_udf[uname]) + isEmptyNum(udf.UDFValue);
                    });
                }
            }
        });

        var fstRow = tran.ItemDetailsColl[0];
        angular.forEach(fstRow.UDFFeildsColl, function (udf) {
            var uname = 't_u' + udf.SNo;
            formula = formula.replaceAll(uname, isEmptyNum(isEmptyNum(t_udf[uname])));
        });

        formula = formula.replaceAll('t_aq', isEmptyNum(t_aq));
        formula = formula.replaceAll('t_bq', isEmptyNum(t_bq));
        formula = formula.replaceAll('t_fq', isEmptyNum(t_fq));
        formula = formula.replaceAll('t_da', isEmptyNum(t_da));
        formula = formula.replaceAll('t_amt', isEmptyNum(t_amt));
        formula = formula.replaceAll('t_tamt', isEmptyNum(t_tamt));
        formula = formula.replaceAll('t_eamt', isEmptyNum(t_eamt));
        formula = formula.replaceAll('t_taxamt', isEmptyNum(t_taxamt));
        formula = formula.replaceAll('t_vamt', isEmptyNum(t_taxamt));

        angular.forEach(tran.UDFFeildsColl, function (udf1) {
            var uname = 'vu' + udf1.SNo;
            formula = formula.replaceAll(uname, isEmptyNum(udf1.UDFValue));
        });

        angular.forEach(tran.ItemDetailsColl, function (dc) {
            if (dc.RowType == 'L') {
                if (dc.costLedgerDetail) {
                    formula = formula.replaceAll(dc.costLedgerDetail.Code, isEmptyNum(dc.Amount));
                }
            }
        });

        if (curRow) {
            formula = formula.replaceAll('cq', isEmptyNum(curRow.BalanceQty));
            formula = formula.replaceAll('aq', isEmptyNum(curRow.ActualQty));
            formula = formula.replaceAll('bq', isEmptyNum(curRow.BilledQty));
            formula = formula.replaceAll('fq', isEmptyNum(curRow.FreeQty));
            formula = formula.replaceAll('rate', isEmptyNum(curRow.Rate));
            formula = formula.replaceAll('dp', isEmptyNum(curRow.DiscountPer));
            formula = formula.replaceAll('da', isEmptyNum(curRow.DiscountAmt));
            formula = formula.replaceAll('amt', isEmptyNum(curRow.Amount));
            formula = formula.replaceAll('nw', isEmptyNum(curRow.NetWeight));
            formula = formula.replaceAll('lr', isEmptyNum(curRow.LossRate));
            formula = formula.replaceAll('lw', isEmptyNum(curRow.LossWeight));
            formula = formula.replaceAll('fr', isEmptyNum(curRow.FineRate));
            formula = formula.replaceAll('fw', isEmptyNum(curRow.FineWeight));
            formula = formula.replaceAll('pr', isEmptyNum(curRow.ProcessingRate));
            formula = formula.replaceAll('pw', isEmptyNum(curRow.ProcessingWeight));
            formula = formula.replaceAll('av1', isEmptyNum(curRow.ALValue1));
            formula = formula.replaceAll('av2', isEmptyNum(curRow.ALValue2));
            formula = formula.replaceAll('av2', isEmptyNum(curRow.ALValue3));
            formula = formula.replaceAll('make', isEmptyNum(curRow.Makeing));
            formula = formula.replaceAll('stone', isEmptyNum(curRow.Stone));
            formula = formula.replaceAll('tamt', isEmptyNum(curRow.TotalAmount));
            formula = formula.replaceAll('eamt', isEmptyNum(curRow.ExDutyAmount));
            formula = formula.replaceAll('tamt', isEmptyNum(curRow.TaxableAmt));
            formula = formula.replaceAll('vamt', isEmptyNum(curRow.VatAmount));
            formula = formula.replaceAll('mrp', isEmptyNum(curRow.MRP));
            formula = formula.replaceAll('pr', isEmptyNum(curRow.PurchaseRate));
            formula = formula.replaceAll('tr', isEmptyNum(curRow.TradeRate));
            formula = formula.replaceAll('sr', isEmptyNum(curRow.SalesRate));

            angular.forEach(curRow.UDFFeildsColl, function (udf1) {
                var uname = 'u' + udf1.SNo;
                formula = formula.replaceAll(uname, isEmptyNum(udf1.UDFValue));
            });
        }


        try {
            var nVal = math.evaluate(formula);
            return nVal;
        } catch { }

    };

    this.getCustomLedAllocation = function (tran, scopeTran) {

        var allocationColl = [];

        if (!tran.VoucherLedAllocationColl || tran.VoucherLedAllocationColl.length == 0)
            return allocationColl;

        var ledgerIdColl = [];

        var drAmt_P = 0, crAmt_P = 0;

        var findPartyLA = mx(tran.VoucherLedAllocationColl).firstOrDefault(p1 => p1.Particular == 'Party_Led');
        if (findPartyLA) {
            if (findPartyLA.DrAmount && findPartyLA.DrAmount.length > 0)
                drAmt_P = isEmptyNum(getFormulaVal(findPartyLA.DrAmount, null, scopeTran));

            if (findPartyLA.CrAmount && findPartyLA.CrAmount.length > 0)
                crAmt_P = isEmptyNum(getFormulaVal(findPartyLA.CrAmount, null, scopeTran));
        }
        ledgerIdColl.push({ id: tran.PartyLedgerId, text: 'Party_Led', amount: 0, DrAmount: drAmt_P, CrAmount: crAmt_P });
        //ledgerIdColl.push({ id: tran.TranLedgerId, text: 'Tran_Led', amount: 0, DrAmount: 0, CrAmount: 0 });

        var allocationQry = mx(tran.VoucherLedAllocationColl);

        angular.forEach(scopeTran.ItemDetailsColl, function (itemDet) {
            if (itemDet.RowType == 'P') {
                if (itemDet.productDetail) {
                    var find = mx(ledgerIdColl).firstOrDefault(p1 => p1.id == itemDet.LedgerId);
                    if (find) {
                        var drAmt = 0, crAmt = 0;
                        var findFormula = allocationQry.firstOrDefault(p1 => p1.Particular == 'Item_Led');
                        if (findFormula) {
                            if (findFormula.DrAmount && findFormula.DrAmount.length > 0)
                                drAmt = isEmptyNum(getFormulaVal(findFormula.DrAmount, itemDet, scopeTran));

                            if (findFormula.CrAmount && findFormula.CrAmount.length > 0)
                                crAmt = isEmptyNum(getFormulaVal(findFormula.CrAmount, itemDet, scopeTran));
                        }

                        find.amount = find.amount + itemDet.Amount;
                        find.DrAmount = find.DrAmount + drAmt;
                        find.CrAmount = find.CrAmount + crAmt;
                    } else {
                        var drAmt = 0, crAmt = 0;
                        var findFormula = allocationQry.firstOrDefault(p1 => p1.Particular == 'Item_Led');
                        if (findFormula) {
                            if (findFormula.DrAmount && findFormula.DrAmount.length > 0)
                                drAmt = isEmptyNum(getFormulaVal(findFormula.DrAmount, itemDet, scopeTran));

                            if (findFormula.CrAmount && findFormula.CrAmount.length > 0)
                                crAmt = isEmptyNum(getFormulaVal(findFormula.CrAmount, itemDet, scopeTran));
                        }
                        ledgerIdColl.push({ id: itemDet.LedgerId, text: 'Item_Led', amount: itemDet.Amount, DrAmount: drAmt, CrAmount: crAmt });
                    }

                    if (itemDet.UDFFeildsColl && itemDet.UDFFeildsColl.length > 0) {
                        angular.forEach(itemDet.UDFFeildsColl, function (udf) {
                            if (udf.LedgerId > 0) {
                                var uname = 'U_Led' + udf1.SNo;
                                var findU = mx(ledgerIdColl).firstOrDefault(p1 => p1.id == udf.LedgerId);
                                if (findU) {
                                    var drAmt = 0, crAmt = 0;
                                    var findFormula = allocationQry.firstOrDefault(p1 => p1.Particular == 'Item_Led');
                                    if (findFormula) {
                                        if (findFormula.DrAmount && findFormula.DrAmount.length > 0)
                                            drAmt = isEmptyNum(getFormulaVal(findFormula.DrAmount, itemDet, scopeTran));

                                        if (findFormula.CrAmount && findFormula.CrAmount.length > 0)
                                            crAmt = isEmptyNum(getFormulaVal(findFormula.CrAmount, itemDet, scopeTran));
                                    }

                                    findU.amount = findU.amount + isEmptyNum(udf.UDFValue);
                                    find.DrAmount = find.DrAmount + drAmt;
                                    find.CrAmount = find.CrAmount + crAmt;
                                } else {

                                    var drAmt = 0, crAmt = 0;
                                    var findFormula = allocationQry.firstOrDefault(p1 => p1.Particular == uname);
                                    if (findFormula) {
                                        if (findFormula.DrAmount && findFormula.DrAmount.length > 0)
                                            drAmt = isEmptyNum(getFormulaVal(findFormula.DrAmount, itemDet, scopeTran));

                                        if (findFormula.CrAmount && findFormula.CrAmount.length > 0)
                                            crAmt = isEmptyNum(getFormulaVal(findFormula.CrAmount, itemDet, scopeTran));
                                    }

                                    ledgerIdColl.push({ id: itemDet.LedgerId, text: uname, amount: isEmptyNum(udf.UDFValue), DrAmount: drAmt, CrAmount: crAmt });
                                }
                            }
                        });
                    }
                }
            } else if (itemDet.RowType == 'L') {
                if (itemDet.costLedgerDetail) {

                    var find = mx(ledgerIdColl).firstOrDefault(p1 => p1.id == itemDet.LedgerId);
                    if (find) {
                        var drAmt = 0, crAmt = 0;
                        var findFormula = allocationQry.firstOrDefault(p1 => p1.Particular == itemDet.costLedgerDetail.Code);
                        if (findFormula) {
                            if (findFormula.DrAmount && findFormula.DrAmount.length > 0)
                                drAmt = isEmptyNum(getFormulaVal(findFormula.DrAmount, itemDet, scopeTran));

                            if (findFormula.CrAmount && findFormula.CrAmount.length > 0)
                                crAmt = isEmptyNum(getFormulaVal(findFormula.CrAmount, itemDet, scopeTran));
                        }

                        find.amount = find.amount + itemDet.Amount;
                        find.DrAmount = find.DrAmount + drAmt;
                        find.CrAmount = find.CrAmount + crAmt;
                    } else {

                        var drAmt = 0, crAmt = 0;
                        var findFormula = allocationQry.firstOrDefault(p1 => p1.Particular == itemDet.costLedgerDetail.Code);
                        if (findFormula) {
                            if (findFormula.DrAmount && findFormula.DrAmount.length > 0)
                                drAmt = isEmptyNum(getFormulaVal(findFormula.DrAmount, itemDet, scopeTran));

                            if (findFormula.CrAmount && findFormula.CrAmount.length > 0)
                                crAmt = isEmptyNum(getFormulaVal(findFormula.CrAmount, itemDet, scopeTran));
                        }

                        ledgerIdColl.push({ id: itemDet.LedgerId, text: itemDet.costLedgerDetail.Code, amount: itemDet.Amount, DrAmount: drAmt, CrAmount: crAmt });
                    }
                }
            }
        });


        angular.forEach(tran.VoucherLedAllocationColl, function (vled) {

            if (vled.Particular.substring(0, 2) == 'vu') {
                var ledId = 0;
                ledId = isEmptyNum(getFormulaVal(vled.Particular, null, scopeTran));

                if (ledId > 0)
                    vled.LedgerId = ledId;
            }

        });

        var ledIdQry = mx(ledgerIdColl);
        angular.forEach(tran.VoucherLedAllocationColl, function (vled) {
            var findRow = ledIdQry.where(p1 => p1.text == vled.Particular);
            if (findRow && findRow.count() > 0) {
                var groupBy = findRow.groupBy(p1 => p1.id);
                angular.forEach(groupBy, function (gp) {
                    var elQry = mx(gp.elements);
                    var newAllocation = {
                        DrAmount: elQry.sum(p1 => p1.DrAmount),
                        CrAmount: elQry.sum(p1 => p1.CrAmount),
                        LedgerId: gp.key,
                        Narration: vled.Remarks,
                    };
                    allocationColl.push(newAllocation);
                });
            } else {

                var drAmt = 0, crAmt = 0;
                if (vled.DrAmount && vled.DrAmount.length > 0)
                    drAmt = isEmptyNum(getFormulaVal(vled.DrAmount, null, scopeTran));

                if (vled.CrAmount && vled.CrAmount.length > 0)
                    crAmt = isEmptyNum(getFormulaVal(vled.CrAmount, null, scopeTran));

                var newAllocation = {
                    DrAmount: drAmt,
                    CrAmount: crAmt,
                    LedgerId: vled.LedgerId > 0 ? vled.LedgerId : 0,
                    LedgerCode: vled.Particular,
                    Narration: vled.Remarks,
                };
                allocationColl.push(newAllocation);

            }
        });

        return allocationColl;
    };
    this.validateAndFocus = function (fieldId, message, isSelect2 = false) {
        var el = document.getElementById(fieldId);
        if (!el) {
            console.log('Element not found: ' + fieldId);
            return false;
        }
        var isSelect = el.tagName === 'SELECT';
        var val = $(el).val();
        var isEmpty = false;
        if (isSelect2) {
            isEmpty = !val || val === "0" || val.length === 0;
        }
        else if (isSelect) {
            isEmpty = !el.value || el.value === "" || el.value === "0";
        }
        else {
            isEmpty = !el.value || !el.value.trim() || el.value === "0" || Number(el.value) < 0;
        }
        if (isEmpty) {
            el.scrollIntoView({ behavior: 'smooth', block: 'center' });
            if (isSelect2) {
                $('#' + fieldId).select2('open');
                var s2 = $('#' + fieldId).next('.select2-container').find('.select2-selection');
                s2.css('border', '1px solid red');
                $('#' + fieldId).one('change', function () {
                    s2.css('border', '');
                });
            } else {
                el.focus();
                el.style.border = '1px solid red';
                el.addEventListener(isSelect ? 'change' : 'input', function () {
                    el.style.border = '';
                }, { once: true });
            }
            Swal.fire({text: message,icon: 'warning',confirmButtonText: 'OK'});
            return false;
        }
        return true;
    };

    this.validateImageFile = function (obj,inputId,modelTmpName,modelDataName,imageId,fieldName,allowedExtensions,maxSizeMB) {
        var fileInput = document.getElementById(inputId);
        if (!fileInput || !fileInput.files || !fileInput.files.length)
            return true;
        var file = fileInput.files[0];
        var extensionPattern = "\\.(" + allowedExtensions.replace(/,/g, '|') + ")$";
        var allowedExtension = new RegExp(extensionPattern, "i");
        if (!allowedExtension.test(file.name)) {

            fileInput.value = '';

            if (obj) {
                obj[modelTmpName] = null;
                obj[modelDataName] = null;
            }
            var img = document.getElementById(imageId);
            if (img) img.src = '';
            fileInput.style.border = '1px solid red';
            Swal.fire({
                text: 'Only ' + allowedExtensions.toUpperCase().replace(/,/g, ', ') +
                    ' files are allowed for ' + fieldName,
                icon: 'warning',
                confirmButtonText: 'OK'
            });

            return false;
        }
        var maxSize = maxSizeMB * 1024 * 1024;
        if (file.size > maxSize) {
            fileInput.value = '';
            if (obj) {
                obj[modelTmpName] = null;
                obj[modelDataName] = null;
            }
            var img = document.getElementById(imageId);
            if (img) img.src = '';
            Swal.fire({
                text: fieldName + ' size must not be greater than ' + maxSizeMB + ' MB.',
                icon: 'warning',
                confirmButtonText: 'OK'
            });
            return false;
        }
        fileInput.style.border = '';
        return true;
    };


    this.validateDate = function (obj, startField, endField, startLabel, endLabel) {
        var result = {
            IsSuccess: true,
            Message: ""
        };
        if (!obj[startField] || !obj[endField] ||
            !obj[startField].dateAD || !obj[endField].dateAD) {
            return true;
        }
        var startDate = $filter('date')(new Date(obj[startField].dateAD), 'yyyy-MM-dd');
        var endDate = $filter('date')(new Date(obj[endField].dateAD), 'yyyy-MM-dd');
        if (!startDate || !endDate)
            return true;
        if (startDate > endDate) {
            result.IsSuccess = false;
            result.Message = startLabel + " cannot be After " + endLabel + ".";
        }
        return result;
    };

});