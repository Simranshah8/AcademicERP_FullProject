app.service('GlobalServices', function ($http) {
    this.getConfirmMSG = function () {
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

    this.getPerPageList = function () {
        var dataColl = [{ value: 0, text: 'All' }, { value: 5, text: 5 }, { value: 10, text: 10 }, { value: 25, text: 25 }, { value: 50, text: 50 }, { value: 100, text: 100 }, { value: 500, text: 500 }];
        return dataColl;
    };

    this.getPerPageRow = function () {
        return 10;
    }
    this.getStudentSearchOptions = function () {
        var dataColl = [{ text: 'Name', value: 'ST.Name' }, { text: 'Regd.No.', value: 'ST.RegNo' }, { text: 'Roll No.', value: 'ST.RollNo' }, { text: 'Id', value: 'ST.StudentId' }];
        return dataColl;
    };
    this.getEmployeeSearchOptions = function () {
        var dataColl = [{ text: 'Name', value: 'E.Name' }, { text: 'Code', value: 'ST.RegNo' }, { text: 'Roll No.', value: 'ST.RollNo' }, { text: 'Id', value: 'ST.StudentId' }];
        return dataColl;
    };
    this.getMonthList = function () {

        var dataColl = [
            { id: 1, text: 'Baishakh' },
            { id: 2, text: 'Jestha' },
            { id: 3, text: 'Asar' },
            { id: 4, text: 'Shrawan' },
            { id: 5, text: 'Bhadau' },
            { id: 6, text: 'Aswin' },
            { id: 7, text: 'Kartik' },
            { id: 8, text: 'Mansir' },
            { id: 9, text: 'Poush' },
            { id: 10, text: 'Magh' },
            { id: 11, text: 'Falgun' },
            { id: 12, text: 'Chaitra' }
        ];

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

    this.getBloodGroupList = function () {
        return ['A+', 'A-', 'B+', 'B-', 'O+', 'O-', 'AB+', 'AB-'];
    };

    this.getReligionList = function () {
        return ["Hinduism", "Islam", "Buddhism", "Christian", "Jainism", "Sikhism", "Judaism"];
    };

    this.getCountryList = function () {
        return ["Nepali", "Indian"];
    };

    this.getDisablityList = function () {
        return ["N/A", "Physical", "Mental", "Deaf", "Blind", "Low Vision", "Deaf and Blind", "Speech Impairment", "Multiple Disability"];
    };

    this.getExamTypeList = function () {
        return $http({
            method: 'GET',
            url: base_url + "Global/GetAllExamTypeList",
            dataType: "json"
        })
    };

    this.getCasteList = function () {
        return $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllCasteList",
            dataType: "json"
        })
    };

    this.getClassList = function () {
        return $http({
            method: 'GET',
            url: base_url + "Global/GetAllClassList",
            dataType: "json"
        })
    };
    this.getCASTypeList = function () {
        return $http({
            method: 'GET',
            url: base_url + "Global/GetAllCASTypeList",
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
    this.getClassSectionList = function () {
        return $http({
            method: 'GET',
            url: base_url + "Global/GetAllClassList",
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

    this.getSubjectList = function (cId, sId) {
        var para = {
            ClassId: cId,
            SectionId:sId
        }
        return $http({
            method: 'POST',
            url: base_url + "Global/GetAllSubjectList",
            dataType: "json",
            data: JSON.stringify(para)
        });
    };

    this.getSubjectList = function () {
      
        return $http({
            method: 'POST',
            url: base_url + "Global/GetAllSubjectList",
            dataType: "json"
        });
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
});