app.controller('StudentDetailsController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, Excel, $translate) {
    $rootScope.ChangeLanguage();
    var gSrv = GlobalServices;
    $scope.Title = 'StudentDetails';

    let stream = null;
    let video = document.querySelector("#video");
    let canvas = document.querySelector('#canvas');
    $scope.takePhotoFromCamera = async function () {

        if ($scope.webCam.Start == true) {
            $scope.webCam.Start = false;

            canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);
            $scope.newStudent.PhotoData = canvas.toDataURL('image/jpeg');

            try {
                // stop only video
                stream.getVideoTracks()[0].stop();

            } catch { }
            stream = null;

        } else {
            $scope.webCam.Start = true;

            stream = null;

            try {
                stream = await navigator.mediaDevices.getUserMedia({ video: true, audio: false });
            }
            catch (error) {
                alert(error.message);
                return;
            }

            try {
                video.srcObject = stream;
            } catch {
                video.src = URL.createObjectURL(stream);
            }

            //video.style.display = 'block';
        }
    }

    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();

        $scope.IdCardTemplateColl = [{ id: 1, text: 'Template1' }, { id: 2, text: 'Template2' }];

        $scope.currentPages = {
            StudentDetails: 1,
        };

        $scope.searchData = {
            StudentDetails: '',
        };

        $scope.perPage = {
            StudentDetails: GlobalServices.getPerPageRow(),
        };

        $scope.newVisitor = {
            TranId: null,
            Visitorname: 1
        };

        $scope.entity = {
            StudentSummary: entityStudentSummary
        };

        $scope.CurEmailSend = {};
        $scope.newNotice = {
            Title: ''
        };
        //SMS Start
        var smsPara = {
            EntityId: $scope.entity.StudentSummary,
            ForATS: 3,
            TemplateType: 1
        };
        $scope.SMSTemplatesColl = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetSENT",
            dataType: "json",
            data: JSON.stringify(smsPara)
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.SMSTemplatesColl = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        smsPara.TemplateType = 2;
        $scope.EmailTemplatesColl = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetSENT",
            dataType: "json",
            data: JSON.stringify(smsPara)
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.EmailTemplatesColl = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
        //ENd SMS


        $scope.newStudentDetails = {
            StudentDetailsId: null,
            Name: '',
            Designation: '',
            ExaminerRegdNo: null,
            MobileNo: null,
            Email: '',
            Qualification: '',
            Address: '',
            Specialization: '',
            Username: '',
            Remarks: '',
            showdetail: false,
            Mode: 'Save'
        };


        $scope.newStudentSummaryList = {
            ClassId: null,
            SectionId: null,
            HouseNameId: null,
            StudentTypeId: null,
            CasteId: null
        };

        $scope.IdCard_Para = {
            ReportTemplateId: 1,
            IssueDate_TMP: new Date(),
            ExpiryDate_TMP: new Date()
        };

        $scope.TemplateList = [{ id: 1, text: "CBSC1" }, { id: 2, text: "CBSC2" }, { id: 3, text: "CBSC3" }, { id: 4, text: "CBSC4" }, { id: 5, text: "5" }, { id: 6, text: "6" }, { id: 7, text: "7" }, { id: 8, text: "Ble Marksheet" }, { id: 9, text: "Conduct Marksheet" }]
        $timeout(function () {
                $scope.AdmitCardTemplateList = [];
                $scope.MarkSheetCardTemplatesList = [];
                $http({
                    method: 'POST',
                    url: base_url + "Academic/SetUp/GetHTMLTemplatesConfig",
                    dataType: "json"
                }).then(function (res) {
                    if (res.data.IsSuccess && res.data.Data) {
                        $scope.AdmitCardTemplateList = res.data.Data.filter(item => item.TemplateTypeId === 1 && item.IsAllowed === true);
                        $scope.MarkSheetTemplatesList = res.data.Data.filter(item => item.TemplateTypeId === 2 && item.IsAllowed === true);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            },500);

        $scope.GMarkSheet_Para = {
            ReportTemplateId: 1,
            ExamTypeId: null,
            TemplateId: null,
            StudentIdColl: ''
        }

        $scope.AMarkSheet_Para = {
            ReportTemplateId: 1,
            ExamTypeId: null,
            StudentIdColl: ''
        }
        /*  $scope.GetAllStudentDetailsList();*/

        $scope.ClassList = [];
        $scope.ClassSection = {};
        GlobalServices.getClassSectionList().then(function (res) {
            $scope.ClassSection = res.data.Data;
            $scope.ClassList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        // For MarkSheet Start        

        $scope.ExamTypeList = [];
        gSrv.getExamTypeList().then(function (res) {
            $scope.ExamTypeList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.GradeList = [];
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/GetAllGradeList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.GradeList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        //For MarkSheet End

        $scope.ReExamTypeList = [];
        gSrv.getReExamTypeList().then(function (res) {
            $scope.ReExamTypeList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.ExamTypeGroupList = [];
        gSrv.getExamTypeGroupList().then(function (res) {
            $scope.ExamTypeGroupList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        //Start For Edit add by pRashant chaitra 18

        $scope.ProvinceColl = GetStateList();
        $scope.DistrictColl = GetDistrictList();
        $scope.VDCColl = GetVDCList();

        $scope.ProvinceColl_Qry = mx($scope.ProvinceColl);
        $scope.DistrictColl_Qry = mx($scope.DistrictColl);
        $scope.VDCColl_Qry = mx($scope.VDCColl);

        $scope.webCam = {
            Start: false
        };
        $scope.AcademicConfig = {};
        GlobalServices.getAcademicConfig().then(function (res1) {
            $scope.AcademicConfig = res1.data.Data;

            if ($scope.AcademicConfig.ActiveFaculty == true) {

                $scope.FacultyList = [];
                GlobalServices.getFacultyList().then(function (res) {
                    $scope.FacultyList = res.data.Data;
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });

            }

            if ($scope.AcademicConfig.ActiveLevel == true) {

                $scope.LevelList = [];
                GlobalServices.getClassLevelList().then(function (res) {
                    $scope.LevelList = res.data.Data;
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });

            }

            if ($scope.AcademicConfig.ActiveSemester == true) {

                $scope.SelectedClassSemesterList = [];
                $scope.SemesterList = [];
                GlobalServices.getSemesterList().then(function (res) {
                    $scope.SemesterList = res.data.Data;
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });

            }

            if ($scope.AcademicConfig.ActiveBatch == true) {

                $scope.BatchList = [];
                GlobalServices.getBatchList().then(function (res) {
                    $scope.BatchList = res.data.Data;
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });

            }

            if ($scope.AcademicConfig.ActiveClassYear == true) {

                $scope.ClassYearList = [];
                $scope.SelectedClassClassYearList = [];
                GlobalServices.getClassYearList().then(function (res) {
                    $scope.ClassYearList = res.data.Data;
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });

            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.GenderColl = GlobalServices.getGenderList();
        $scope.CountryList = GlobalServices.getCountryList();
        $scope.MonthList = [];


        $scope.CasteList = [];
        GlobalServices.getCasteList().then(function (res) {
            $scope.CasteList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.ClassList = [];
        GlobalServices.getClassList().then(function (res) {
            $scope.ClassList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.SectionList = [];
        GlobalServices.getSectionList().then(function (res) {
            $scope.SectionList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.SectionForTranList = [];
        GlobalServices.getSectionForTran().then(function (res) {
            $scope.SectionForTranList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.ClassSectionList = [];
        GlobalServices.getClassSectionList().then(function (res) {
            $scope.ClassSectionList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.AcademicYearList = [];
        GlobalServices.getAcademicYearList().then(function (res) {
            $scope.AcademicYearList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.MediumList = [];
        GlobalServices.getMediumList().then(function (res) {
            $scope.MediumList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.HouseNameList = [];
        GlobalServices.getHouseNameList().then(function (res) {
            $scope.HouseNameList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.HouseDressList = [];
        GlobalServices.getHouseDressList().then(function (res) {
            $scope.HouseDressList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.StudentTypeList = [];
        GlobalServices.getStudentTypeList().then(function (res) {
            $scope.StudentTypeList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.BoardList = [];
        GlobalServices.getBoardList().then(function (res) {
            $scope.BoardList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.TransportPointList = [];
        //GlobalServices.getList().then(function (res) {
        //	$scope.List = res.data.Data;
        //}, function (reason) {
        //	Swal.fire('Failed' + reason);
        //});

        $scope.BoardersTypeList = [];
        //GlobalServices.getList().then(function (res) {
        //	$scope.List = res.data.Data;
        //}, function (reason) {
        //	Swal.fire('Failed' + reason);
        //});

        $scope.DocumentTypeList = [];
        GlobalServices.getDocumentTypeList().then(function (res) {
            $scope.DocumentTypeList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.BloodGroupList = GlobalServices.getBloodGroupList();
        $scope.ReligionList = GlobalServices.getReligionList();
        $scope.CountryList = GlobalServices.getCountryList();
        $scope.NationalityList = GlobalServices.getNationalityList();

        $scope.ProvinceList = GetStateList();
        $scope.DistrictList = GetDistrictList();
        $scope.VDCColl = GetVDCList();

        $scope.ZoneList = GetZoneList();

        $scope.ProvinceColl_Qry = mx($scope.ProvinceColl);
        $scope.DistrictColl_Qry = mx($scope.DistrictColl);
        $scope.VDCColl_Qry = mx($scope.VDCColl);

        $scope.DisablityList = GlobalServices.getDisablityList();

        $scope.SiblingRelationList = ["Brother", "Sister"];

        $scope.BranchColl = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllBranchListForEntry",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BranchColl = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.RoomList = [];
        $http({
            method: 'POST',
            url: base_url + "Hostel/Creation/GetAllRoomListForMapping",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.RoomList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.TransportPointList = [];
        $http({
            method: 'POST',
            url: base_url + "Transport/Creation/GetAllTransportPointList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.TransportPointList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.StudentConfig = {};
        $http({
            method: 'POST',
            url: base_url + "Academic/Setup/GetStudentConfiguration",
            dataType: "json",
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.StudentConfig = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        //Company Details
        $scope.CompanyConfig = {};
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetCompanyDet",
            dataType: "json",
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.CompanyConfig = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
        //logo
        $scope.Logo = [];
        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/GetAllAboutUsList",
            dataType: "json",
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.Logo = res.data.Data[0];
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        //For Student AdmitCard_Para start
        $scope.AdmitCard_Para = {
            TemplateId: null,
            ExamTypeId: null,
            StudentIdColl: ''
        };
        $scope.AdmitCatdTemplateColl = [{ id: 1, text: "Default Template" }, { id: 2, text: "Coustom Template" }]
        //For Student AdmitCard_Para End

        //For Student Certificate_Para start
        $scope.Certificate_Para = {
            CertificateTypeId: null,
            TemplateId: 1,
            ExamTypeId: null,
            AcademicYearId: null,
            StudentIdColl: ''
        };
        $scope.CertificateTypeColl = [{ id: 1, text: "TRANSFER CERTIFICATE" }, { id: 2, text: "CHARACTER CERTIFICATE" }, { id: 3, text: "EXTRA CERTIFICATE" }]
        $scope.CertificateTemplateColl = [{ id: 1, text: "Default Template" }]

        //For Student Certificate_Para End

        //For StudentRemark start
        $scope.RemarksForList = [{ id: 1, text: 'MERITS' }, { id: 2, text: 'DEMERITS' }, { id: 3, text: 'OTHERS' },]

        $scope.RemarksTypeList = [];
        $scope.RemarksTypeQry = [];
        $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllRemarksTypeList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.RemarksTypeList = res.data.Data;
                $scope.RemarksTypeQry = mx(res.data.Data);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.newRemarks = {
            StudentRemarksId: null,
            ForDate_TMP: new Date(),
            RemarksFor: null,
            RemarksTypeId: null,
            Point: 0,
            Remarks: '',
            AttachFile: '',
            Mode: 'Save'
        };


        //For StudentRemark End

        //For StudentPTM Start
        $scope.PTMAttendByList = [
            { id: 1, text: "None" },
            { id: 2, text: "Father" },
            { id: 3, text: "Mother" },
            { id: 4, text: "Guardian" }
        ];

        $scope.newPTM = {
            TranId: null,
            ClassId: null,
            SectionId: null,
            PTMDate_TMP: new Date(),
            PTMBy: null,
            Description: '',
            StudentPTMColl: [],
            SelectedClass: null,
            Mode: 'Save'
        };
        //For StudentPTM End

        //For StudentLeft Start

        $scope.StatusList = [{ id: 1, text: 'Passout' }, { id: 2, text: 'Dropout' }]
        $scope.PassoutoptList = [
            { id: 1, text: 'Regular Passout' },
            { id: 2, text: 'Late Passout' },
            { id: 3, text: 'Supplementary Passout' },
            { id: 4, text: 'Reassessment Passout' },
            { id: 5, text: 'Migration Passout' }
        ]

        $scope.DropoutOptList = [
            { id: 1, text: 'Voluntary Dropout' },
            { id: 2, text: 'Academic Dropout' },
            { id: 3, text: 'Financial Dropout ' },
            { id: 4, text: 'Transfer Dropout' },
            { id: 5, text: ' Disciplinary Dropout' },
            { id: 6, text: 'Health-Related Dropout' },
            { id: 7, text: 'Non-Attendance Dropout' }
        ]
        $scope.newLeft = {
            LeftStudentId: null,
            LeftDate_TMP: new Date(),
            StatusId: null,
            IsLeft: false,
            LeftRemarks: '',
            Notification: false,
            SMS: false,
            Mode: 'Save'
        };
        //For StudentLeft End

        $scope.newStudent = {
            StudentId: null,
            RegNo: '',
            AdmitDate_TMP: new Date(),
            FirstName: '',
            MiddleName: '',
            LastName: '',
            Gender: 1,
            ContactNo: '',
            IsPhysicalDisability: false,
            isEDJ: false,
            Photo: null,
            PhotoPath: null,
            Signature: null,
            SignaturePath: null,
            ClassId: null,
            SectionId: null,
            RollNo: 0,
            IfGurandianIs: 3,
            IsSameAsPermanentAddress: false,
            AcademicDetailsColl: [],
            AttachmentColl: [],
            SiblingDetailColl: [],
            SiblingDetailColl_TMP: [],
            LedgerPanaNo: '',
            IsNewStudent: true,
            F_AnnualIncome: 0,
            M_AnnualIncome: 0,
            EnquiryNo: '',
            ClassId_First: null,
            AdmissionEnquiryId: null,
            PaidUptoMonth: null,

            PassOutClassId: null,
            PassOutSymbolNo: '',
            PassOutGPA: null,
            PassOutGrade: '',
            PassOutRemarks: '',
            EnrollNo: 0,
            CardNo: 0,
            FamilyType: 0,
            PA_WardNo: 0,
            CA_WardNo: 0,
            PA_ProvinceId: null,
            PA_DistrictId: null,
            PA_LocalLevelId: null,
            CA_ProvinceId: null,
            CA_DistrictId: null,
            CA_LocalLevelId: null,
            Mode: 'Save'
        };

        $scope.newStudent.AcademicDetailsColl.push({});
        $scope.newStudent.SiblingDetailColl_TMP.push({});
        // End

       //For LEAVE ENTRY Start
        $scope.AcademicYearList = {};
        GlobalServices.getAcademicYearList().then(function (res) {
            $scope.AcademicYearList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
        $scope.LeaveDurationList = [
            { id: 1, text: 'Full Day' },
            { id: 2, text: 'Half Day' },
            { id: 3, text: 'Hourly' }
        ];
        $scope.LeavePeriodList = [
            { id: 1, text: 'First Half' },
            { id: 2, text: 'Second Half' },
            { id: 3, text: 'Other' }
        ];
        $scope.LeaveTypeList = [];
        $http({
            method: 'POST',
            url: base_url + "Attendance/Transaction/GetAllLeaveType",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.LeaveTypeList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.newLeaveEntry = {
            LeaveEntryId: null,
            LeaveTypeId: null,
            DurationTypeId: null,
            LeavePeriodId: null,
            Remarks: '',
            AttachDocument: '',
            LeaveTo: 1,
            AttachmentColl: [],
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            NoOfDays: 1,
            Mode: 'Save'
        };
       //For LEAVE ENTRY End

       //For Visitor Start
        $scope.newVisitor = {
            VisitorId: null,
            Name: '',
            Address: '',
            Contact: '',
            Email: '',
            VisitorRelation: 1,
            Purpose: '',
            //InTime: new Date(),
            //InTime_TMP: new Date(),
            ValidityTime: null,
            OutTime: null,
            Remarks: '',
            TypeOfDocumentId: null,
            AttachDocument: '',
            Description: '',
            MeeTo: 1,
            AttachmentColl: [],
            FromDate_TMP: new Date(),
            ToDate_TMP: new Date(),
            Mode: 'Save'
        };
       //For Visitor End

       //For Upgrade Class & Section Start

        $scope.newUpgrade = {
            StudentId: null,
            ClassId: null,
            SectionId: null,
            Mode: 'Save'
        };

       //For Upgrade Class & Section END
    };

    $scope.GetStudentSummaryList = function () {

        $scope.AllStudentList = [];
        $scope.loadingstatus = "running";
        showPleaseWait();


        var para = {
            ClassIdColl: $scope.newStudentSummaryList.ClassId,
            SectionIdColl: $scope.newStudentSummaryList.SectionId,
            StudentTypeIdColl: '',
            HouseNameIdColl: '',
            CasteIdColl: '',
            flag: 1,
            AgeRange: '',
            MediumIdColl: '',
        };

        $http({
            method: 'POST',
            url: base_url + "Academic/Report/GetStudentSummary",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {


            if (res.data.IsSuccess) {
                var studentList = res.data.Data;
                // Step 1: Get TC List
                $http({
                    method: 'POST',
                    url: base_url + "Academic/Transaction/GetAllTCList",
                    dataType: "json"
                }).then(function (tcRes) {
                    var tcList = tcRes.data.IsSuccess ? tcRes.data.Data : [];
                    angular.forEach(tcList, function (tc) {
                        if (tc.UDF && tc.UDF.length > 0) {
                            var udfColl = JSON.parse(tc.UDF);
                            var strColl = '';
                            angular.forEach(udfColl, function (ud) {
                                if (strColl.length > 0) strColl += ',';
                                strColl += '"' + ud.Name + '":"' + ud.Value + '"';
                            });
                            if (strColl.length > 0) {
                                strColl = "{" + strColl + "}";
                                tc.UDF = JSON.parse(strColl);
                            }
                        }
                    });
                    // Step 2: Get CC List
                    $http({
                        method: 'POST',
                        url: base_url + "Academic/Transaction/GetAllCCList",
                        dataType: "json"
                    }).then(function (ccRes) {
                        hidePleaseWait();
                        $scope.loadingstatus = "stop";

                        var ccList = ccRes.data.IsSuccess ? ccRes.data.Data : [];

                        angular.forEach(ccList, function (cc) {
                            if (cc.UDF && cc.UDF.length > 0) {
                                var udfColl = JSON.parse(cc.UDF);
                                var strColl = '';
                                angular.forEach(udfColl, function (ud) {
                                    if (strColl.length > 0) strColl += ',';
                                    strColl += '"' + ud.Name + '":"' + ud.Value + '"';
                                });
                                if (strColl.length > 0) {
                                    strColl = "{" + strColl + "}";
                                    cc.UDF = JSON.parse(strColl);
                                }
                            }
                        });
                        // Merge TranId (TC) and CCTranId (CC) into student list
                        angular.forEach(studentList, function (student) {
                            var tcMatch = tcList.find(x => x.StudentId === student.StudentId);
                            var ccMatch = ccList.find(x => x.StudentId === student.StudentId);
                            student.TranId = tcMatch ? tcMatch.TranId : null;
                            student.CCTranId = ccMatch ? ccMatch.TranId : null;
                        });
                        $scope.AllStudentList = studentList;
                        if (!ccRes.data.IsSuccess) {
                            Swal.fire(ccRes.data.ResponseMSG);
                        }
                    }, function (reason) {
                        hidePleaseWait();
                        $scope.loadingstatus = "stop";
                        Swal.fire('Failed loading CC List: ' + reason);
                    });
                }, function (reason) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    Swal.fire('Failed loading TC List: ' + reason);
                });
            } else {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire('Failed: ' + reason);
        });
    };

    //Start Stundent Email
    $scope.ShowEmailDialog = function () {
        myDropzone.removeAllFiles();

        $scope.CurEmailSend = {
            Temlate: {},
            Description: '',
            Subject: '',
            Title: '',
            Primary: true,
            Father: false,
            Mother: false,
            Guardian: false,
            CC: '',
            DataColl: [],
        };

        angular.forEach($scope.AllStudentList, function (student) {
            if (student.IsSelected === true) {
                $scope.CurEmailSend.DataColl.push(student);
            }
        });

        if ($scope.CurEmailSend.DataColl.length === 0) {
            Swal.fire("Please select at least one student.");
            return;
        }

        $('#Emailmodal').modal('show');
    };


    $scope.SendEmail = function () {
        if ($scope.CurEmailSend && $scope.CurEmailSend.DataColl && $scope.CurEmailSend.DataColl.length > 0) {

            var filesColl = myDropzone.files;

            var ccColl = [];
            if ($scope.CurEmailSend.EmployeeColl && $scope.CurEmailSend.EmployeeColl.length > 0) {
                angular.forEach($scope.CurEmailSend.EmployeeColl, function (emp) {
                    if (emp.EmailId && emp.EmailId.length > 0)
                        ccColl.push(emp.EmailId);
                });
            }

            var emailDataColl = [];
            angular.forEach($scope.CurEmailSend.DataColl, function (objEntity) {
                var emailColl = [];
                if ($scope.CurEmailSend.Primary == true) {

                    if (objEntity.Email && objEntity.Email.length > 0)
                        emailColl.push(objEntity.Email);
                }
                if ($scope.CurEmailSend.Father == true) {
                    if (objEntity.F_Email && objEntity.F_Email.length > 0)
                        emailColl.push(objEntity.F_Email);
                }

                if ($scope.CurEmailSend.Mother == true) {
                    if (objEntity.M_Email && objEntity.M_Email.length > 0)
                        emailColl.push(objEntity.M_Email);
                }

                if ($scope.CurEmailSend.Guardian == true) {
                    if (objEntity.G_Email && objEntity.G_Email.length > 0)
                        emailColl.push(objEntity.G_Email);
                }

                if (emailColl.length > 0) {
                    var msg = $scope.CurEmailSend.Description;
                    for (let x in objEntity) {
                        var variable = '$$' + x.toLowerCase() + '$$';
                        if (msg.indexOf(variable) >= 0) {
                            var val = objEntity[x];
                            msg = msg.replace(variable, val);
                        }

                        if (msg.indexOf('$$') == -1)
                            break;
                    }

                    var paraColl = [];
                    paraColl.push({ Key: 'StudentId', Value: objEntity.StudentId });

                    var newEmail = {
                        EntityId: $scope.entity.StudentSummary,
                        StudentId: objEntity.StudentId,
                        UserId: 0,
                        Title: $scope.CurEmailSend.Temlate.Title,
                        Subject: $scope.CurEmailSend.Subject,
                        Message: msg,
                        CC: ccColl.toString(),
                        To: emailColl.toString(),
                        StudentName: objEntity.Name,
                        ParaColl: paraColl,
                        FileName: 'student-form'
                    };
                    emailDataColl.push(newEmail);
                }

            });

            if (emailDataColl.length > 0) {

                $scope.loadingstatus = "running";
                showPleaseWait();

                $http({
                    method: 'POST',
                    url: base_url + "Global/SendEmail",
                    headers: { 'Content-Type': undefined },

                    transformRequest: function (data) {

                        var formData = new FormData();
                        formData.append("jsonData", angular.toJson(data.jsonData));

                        if (data.files) {
                            for (var i = 0; i < data.files.length; i++) {

                                if (data.files[i].File)
                                    formData.append("file" + i, data.files[i].File);
                                else
                                    formData.append("file" + i, data.files[i]);
                            }
                        }

                        return formData;
                    },
                    data: { jsonData: emailDataColl, files: filesColl }
                }).then(function (res) {

                    $scope.loadingstatus = "stop";
                    hidePleaseWait();

                    Swal.fire(res.data.ResponseMSG);

                }, function (errormessage) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    $('#sendemail').modal('hide');
                });

            }

        } else {
            Swal.fire('No Data found for sms');
        }
    };



    //End Stundent Email

    //Start Stundent SMS

    $scope.SendSMSToStudent = function () {
        Swal.fire({
            title: 'Do you want to send SMS to the selected students?',
            showCancelButton: true,
            confirmButtonText: 'Send',
        }).then((result) => {
            if (result.isConfirmed) {
                var para1 = {
                    EntityId: entityStudentSummaryForSMS,
                    ForATS: 3,
                    TemplateType: 1
                };

                // ✅ Get selected students from AllStudentList
                var selectedStudents = $scope.AllStudentList.filter(st => st.IsSelected === true);
                if (selectedStudents.length === 0) {
                    Swal.fire("Please select at least one student.");
                    return;
                }

                $http({
                    method: 'POST',
                    url: base_url + "Setup/Security/GetSENT",
                    dataType: "json",
                    data: JSON.stringify(para1)
                }).then(function (res) {
                    if (res.data.IsSuccess && res.data.Data) {
                        var templatesColl = res.data.Data;
                        if (!templatesColl || templatesColl.length === 0) {
                            Swal.fire('No Templates found for SMS');
                            return;
                        }

                        var selectedTemplate = null;

                        // Auto-select if only one template
                        if (templatesColl.length === 1) {
                            selectedTemplate = templatesColl[0];
                            sendSMS(selectedStudents, selectedTemplate);
                        } else {
                            // Let user pick from multiple templates
                            var templatesName = templatesColl.map((tc, i) => (i + 1) + '-' + tc.Name);

                            Swal.fire({
                                title: 'Templates For SMS',
                                input: 'select',
                                inputOptions: templatesName,
                                inputPlaceholder: 'Select a template',
                                showCancelButton: true,
                                inputValidator: (value) => {
                                    return new Promise((resolve) => {
                                        if (value >= 0) {
                                            selectedTemplate = templatesColl[value];
                                            sendSMS(selectedStudents, selectedTemplate);
                                            resolve();
                                        } else {
                                            resolve('Please select a template.');
                                        }
                                    });
                                }
                            });
                        }
                    }
                }, function (err) {
                    Swal.fire('Failed to get templates: ' + err);
                });
            }
        });

        // ✅ Function to construct and send SMS
        function sendSMS(studentList, template) {
            var dataColl = [];

            angular.forEach(studentList, function (student) {
                var contactNo = '';

                if (template.Recipients) {
                    contactNo = template.Recipients
                        .replace('$$contactno$$', student.ContactNo || '')
                        .replace('$$f_contactno$$', student.F_ContactNo || '');
                }

                if (!contactNo || contactNo.includes('$$')) {
                    contactNo = student.F_ContactNo || student.ContactNo || '';
                }

                if (!contactNo) return;

                var msg = template.Description;
                for (let key in student) {
                    var variable = '$$' + key.toLowerCase() + '$$';
                    if (msg.includes(variable)) {
                        msg = msg.replaceAll(variable, student[key]);
                    }
                    if (!msg.includes('$$')) break;
                }

                var newSMS = {
                    EntityId: entityStudentSummary,
                    StudentId: student.StudentId,
                    UserId: student.UserId,
                    Title: template.Title,
                    Message: msg,
                    ContactNo: contactNo,
                    StudentName: student.Name
                };

                dataColl.push(newSMS);
            });

            if (dataColl.length > 0) {
                $http({
                    method: 'POST',
                    url: base_url + "Global/SendSMSToStudent",
                    dataType: "json",
                    data: JSON.stringify(dataColl)
                }).then(function (sRes) {
                    Swal.fire(sRes.data.ResponseMSG);
                });
            } else {
                Swal.fire("No valid contact numbers found.");
            }
        }
    };

    //ENd Student SMS

    //************************* Start Student Notification *********************************

    $scope.getSelectedStudentIds = function () {
        var selectedIds = [];
        var selectedRows = $scope.gridApi.selection.getSelectedRows();
        angular.forEach(selectedRows, function (row) {
            selectedIds.push(row.StudentId);
        });
        return selectedIds;
    };

    $scope.SendNoticeToStudent = function () {

        // Step 1: Check if students are selected
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected === true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });

        if (selectedStudentIdColl.length === 0) {
            Swal.fire('No students selected!');
            return;
        }

        // Step 2: Confirm before proceeding
        Swal.fire({
            title: 'Do you want to Send Notification To the filter data?',
            showCancelButton: true,
            confirmButtonText: 'Send',
        }).then((result) => {
            if (result.isConfirmed) {

                var para1 = {
                    EntityId: entityStudentSummaryForSMS,
                    ForATS: 3,
                    TemplateType: 3
                };

                $http({
                    method: 'POST',
                    url: base_url + "Setup/Security/GetSENT",
                    dataType: "json",
                    data: JSON.stringify(para1)
                }).then(function (res) {
                    if (res.data.IsSuccess && res.data.Data) {
                        var templatesColl = res.data.Data;

                        if (templatesColl.length > 0) {
                            var templatesName = [];
                            var sno = 1;
                            angular.forEach(templatesColl, function (tc) {
                                templatesName.push(sno + '-' + tc.Name);
                                sno++;
                            });

                            var rptTranId = 0;
                            var selectedTemplate = null;

                            if (templatesColl.length == 1) {
                                rptTranId = templatesColl[0].TranId;
                                selectedTemplate = templatesColl[0];

                                $scope.newNotice.Title = selectedTemplate.Title;
                                $scope.newNotice.Description = selectedTemplate.Description;
                                $('#Notificationmodal').modal('show');
                            } else {
                                Swal.fire({
                                    title: 'Templates For Notification',
                                    input: 'select',
                                    inputOptions: templatesName,
                                    inputPlaceholder: 'Select a template',
                                    showCancelButton: true,
                                    inputValidator: (value) => {
                                        return new Promise((resolve) => {
                                            if (value >= 0) {
                                                rptTranId = templatesColl[value].TranId;
                                                selectedTemplate = templatesColl[value];

                                                $scope.$applyAsync(() => {
                                                    $scope.newNotice.Title = selectedTemplate.Title;
                                                    $scope.newNotice.Description = selectedTemplate.Description;
                                                    $('#Notificationmodal').modal('show');
                                                });

                                                resolve();
                                            } else {
                                                resolve('You need to select :)');
                                            }
                                        });
                                    }
                                });
                            }
                        } else {
                            $scope.newNotice.Title = '';
                            $scope.newNotice.Description = '';
                            $('#Notificationmodal').modal('show');
                        }

                    } else {
                        Swal.fire('No template found.');
                    }
                }, function (error) {
                    Swal.fire('Failed: ' + error);
                });

            }
        });
    };

    $scope.SendManualNoticeToStudent = function () {

        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected === true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });

        if (selectedStudentIdColl.length === 0) {
            Swal.fire("Please select at least one student.");
            return;
        }

        $scope.loadingstatus = "running";
        showPleaseWait();

        var contentPath = '';

        $timeout(function () {

            // Upload attachment if exists
            $http({
                method: 'POST',
                url: base_url + "Global/UploadAttachments",
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    var formData = new FormData();
                    if (data.files) {
                        for (var i = 0; i < data.files.length; i++) {
                            formData.append("file" + i, data.files[i]);
                        }
                    }
                    return formData;
                },
                data: { files: $scope.newNotice.AttachmentColl }
            }).then(function (res) {

                $scope.loadingstatus = "stop";
                hidePleaseWait();

                if (res.data.IsSuccess && res.data.Data.length > 0) {
                    contentPath = res.data.Data[0];
                }

                $timeout(function () {
                    $scope.loadingstatus = "running";
                    showPleaseWait();

                    var dataColl = [];

                    angular.forEach($scope.AllStudentList, function (student) {
                        if (selectedStudentIdColl.includes(student.StudentId)) {

                            var msg = $scope.newNotice.Description;

                            // Replace $$placeholders$$ with student data
                            for (let key in student) {
                                var variable = '$$' + key.toLowerCase() + '$$';
                                if (msg.includes(variable)) {
                                    msg = msg.replaceAll(variable, student[key]);
                                }
                                if (!msg.includes('$$')) break;
                            }

                            var newSMS = {
                                EntityId: entityStudentSummary,
                                StudentId: student.StudentId,
                                UserId: student.UserId,
                                Title: $scope.newNotice.Title,
                                Message: msg,
                                ContactNo: student.F_ContactNo,
                                StudentName: student.Name,
                                ContentPath: contentPath
                            };

                            dataColl.push(newSMS);
                        }
                    });

                    // Send to API
                    $http({
                        method: 'POST',
                        url: base_url + "Global/SendNotificationToStudent",
                        dataType: "json",
                        data: JSON.stringify(dataColl)
                    }).then(function (sRes) {
                        hidePleaseWait();
                        $scope.loadingstatus = "stop";

                        Swal.fire(sRes.data.ResponseMSG);
                        if (sRes.data.IsSuccess) {
                            $('#Notificationmodal').modal('hide');
                        }
                    });

                });

            }, function () {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                Swal.fire("Failed to upload attachment.");
            });

        });
    };



    //************************* Start Student Notification *********************************

    //************************* Start Student password Edit *********************************
    $scope.togglePasswordVisibility = function (st, show) {
        let passwordSpan = document.getElementById("passwordText" + st.StudentId);
        let passwordInput = document.getElementById("editinput" + st.StudentId);
        let coverEye = document.getElementById("covereye" + st.StudentId);
        let openEye = document.getElementById("openeye" + st.StudentId);

        if (show) {
            passwordSpan.innerText = st.Pwd; // Show actual password
            passwordInput.type = "text"; // Show password in input if editing
            coverEye.style.display = "none"; // Hide closed eye
            openEye.style.display = "inline-block"; // Show open eye
        } else {
            passwordSpan.innerText = "*".repeat(st.Pwd.length); // Mask password
            passwordInput.type = "password"; // Hide password in input if editing
            coverEye.style.display = "inline-block"; // Show closed eye
            openEye.style.display = "none"; // Hide open eye
        }
    };

    $scope.enablePasswordEdit = function (st) {
        let passwordSpan = document.getElementById("passwordText" + st.StudentId);
        let passwordInput = document.getElementById("editinput" + st.StudentId);
        let saveBtn = document.getElementById("savepassword" + st.StudentId);

        passwordSpan.style.display = "none"; // Hide masked password
        passwordInput.style.display = "inline-block"; // Show input field
        passwordInput.removeAttribute("disabled"); // Enable editing
        saveBtn.style.display = "inline-block"; // Show save button
    };

    $scope.savePassword = function (st) {
        let passwordInput = document.getElementById("editinput" + st.StudentId);
        let saveBtn = document.getElementById("savepassword" + st.StudentId);

        st.NewPwd = passwordInput.value; // Get new password from input

        // Call UpdateStudentPwd function
        $scope.UpdateStudentPwd(st);

        saveBtn.style.display = "none"; // Hide save button
    };

    $scope.UpdateStudentPwd = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var para = {
            UserId: refData.UserId,
            OldPwd: refData.Pwd,
            NewPwd: refData.NewPwd,
            UserName: refData.UserName
        };

        $http({
            method: 'POST',
            url: base_url + "Academic/Setup/UpdatePwd",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess) {
                $timeout(function () {
                    refData.Pwd = refData.NewPwd; // Update password
                    refData.CanEdit = false;

                    // Hide input and restore masked password
                    let passwordSpan = document.getElementById("passwordText" + refData.StudentId);
                    let passwordInput = document.getElementById("editinput" + refData.StudentId);
                    passwordSpan.innerText = "*".repeat(refData.Pwd.length);
                    passwordSpan.style.display = "inline-block";
                    passwordInput.style.display = "none";
                });
            }
        }, function (reason) {
            Swal.fire('Failed: ' + reason);
        });
    };

    //************************* End Student password Edit *********************************

    //************************* Start Student Edit *********************************

    $scope.updateDOB = function () {
        if ($scope.newStudent.DOB_ADDet) {
            var englishDate = $filter('date')(new Date($scope.newStudent.DOB_ADDet.dateAD), 'yyyy-MM-dd');

            $scope.newStudent.DOB_AD = englishDate;

            if (!$scope.$$phase) {
                $scope.$apply();
            }
        }
    };
    $scope.updateDOB_BS = function () {
        if ($scope.newStudent.DOBAD) {
            $scope.$applyAsync(function () {
                $scope.newStudent.DOB_TMP = new Date($scope.newStudent.DOBAD);
            });
        }
    };

    $scope.getProvinceId = function (provinceText) {
        var province = $scope.ProvinceList.find(p => p.text === provinceText);
        return province ? province.id : null;
    };
    $scope.getDistrictId = function (districtText) {
        var district = $scope.DistrictList.find(p => p.text === districtText);
        return district ? district.id : null;
    };

    $scope.GetStudentForSibling = function (sb, ind) {

        $timeout(function () {

            if (sb.SelectedClassSection) {
                var para = {
                    ClassId: sb.SelectedClassSection.ClassId,
                    SectionId: sb.SelectedClassSection.SectionId
                };

                $http({
                    method: 'POST',
                    url: base_url + "Academic/Transaction/GetSB_StudentForTran",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    sb.StudentList = res.data.Data;
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }

        });


    };

    $scope.LoadClassSectionList = function () {
        $scope.ClassSection = {};
        GlobalServices.getClassSectionList().then(function (res) {
            $scope.ClassSection = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GetStudentForSibling = function (sb, ind) {
        $timeout(function () {
            if (sb.SelectedClassSection) {
                var para = {
                    ClassId: sb.SelectedClassSection.ClassId,
                    SectionId: sb.SelectedClassSection.SectionId
                };
                $http({
                    method: 'POST',
                    url: base_url + "Academic/Transaction/GetSB_StudentForTran",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    sb.StudentList = res.data.Data;
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });
    };

    $scope.LoadGuradianDet = function () {

        $('#imgGuardian').attr('src', '');

        if ($scope.newStudent.IfGurandianIs == 1) {
            $scope.newStudent.GuardianName = $scope.newStudent.FatherName;
            $scope.newStudent.G_Relation = 'Father';
            $scope.newStudent.G_Profesion = $scope.newStudent.F_Profession;
            $scope.newStudent.G_ContactNo = $scope.newStudent.F_ContactNo;
            $scope.newStudent.G_Email = $scope.newStudent.F_Email;
            $scope.newStudent.GuardianPhotoData = $scope.newStudent.FatherPhotoData;
            $scope.newStudent.GuardianPhotoPath = $scope.newStudent.FatherPhotoPath;

        } else if ($scope.newStudent.IfGurandianIs == 2) {
            $scope.newStudent.GuardianName = $scope.newStudent.MotherName;
            $scope.newStudent.G_Relation = 'Mother';
            $scope.newStudent.G_Profesion = $scope.newStudent.M_Profession;
            $scope.newStudent.G_ContactNo = $scope.newStudent.M_Contact;
            $scope.newStudent.G_Email = $scope.newStudent.M_Email;
            $scope.newStudent.GuardianPhotoData = $scope.newStudent.MotherPhotoData;
            $scope.newStudent.GuardianPhotoPath = $scope.newStudent.MotherPhotoPath;

        } else if ($scope.newStudent.IfGurandianIs == 3) {
            $scope.newStudent.GuardianName = '';
            $scope.newStudent.G_Relation = '';
            $scope.newStudent.G_Profesion = '';
            $scope.newStudent.G_ContactNo = '';
            $scope.newStudent.G_Email = '';
            $scope.newStudent.GuardianPhotoData = null;
            $scope.newStudent.GuardianPhotoPath = null;
        }
    };

    $scope.SamePAddress = function () {

        if ($scope.newStudent.IsSameAsPermanentAddress == true) {
            $scope.newStudent.CA_FullAddress = $scope.newStudent.PA_FullAddress;
            $scope.newStudent.CA_ProvinceId = $scope.newStudent.PA_ProvinceId;
            $scope.newStudent.CA_DistrictId = $scope.newStudent.PA_DistrictId;
            $scope.newStudent.CA_LocalLevelId = $scope.newStudent.PA_LocalLevelId;
            $scope.newStudent.CA_WardNo = $scope.newStudent.PA_WardNo;
            $scope.newStudent.StreetName = $scope.newStudent.PA_Village;
            $scope.newStudent.CA_HouseNo = $scope.newStudent.PA_HouseNo;

        } else {
            $scope.newStudent.CA_FullAddress = '';
            $scope.newStudent.CA_ProvinceId = '';
            $scope.newStudent.CA_DistrictId = '';
            $scope.newStudent.CA_LocalLevelId = '';
            $scope.newStudent.CA_WardNo = '';
            $scope.newStudent.StreetName = '';
            $scope.newStudent.CA_Village = '';
            $scope.newStudent.CA_HouseNo = '';
        }
    }

    $scope.AddAcademicDetails = function (ind) {
        if ($scope.newStudent.AcademicDetailsColl) {
            if ($scope.newStudent.AcademicDetailsColl.length > ind + 1) {
                $scope.newStudent.AcademicDetailsColl.splice(ind + 1, 0, {
                    ClassName: ''
                })
            } else {
                $scope.newStudent.AcademicDetailsColl.push({
                    ClassName: ''
                })
            }
        }
    };
    $scope.delAcademicDetails = function (ind) {
        if ($scope.newStudent.AcademicDetailsColl) {
            if ($scope.newStudent.AcademicDetailsColl.length > 1) {
                $scope.newStudent.AcademicDetailsColl.splice(ind, 1);
            }
        }
    };
    $scope.delAttachmentFiles = function (ind) {
        if ($scope.newStudent.AttachmentColl) {
            if ($scope.newStudent.AttachmentColl.length > 0) {
                $scope.newStudent.AttachmentColl.splice(ind, 1);
            }
        }
    }
    $scope.AddMoreFiles = function (files, docType, des) {

        if (files && docType) {
            if (files != null && docType != null) {

                angular.forEach(files, function (file) {
                    $scope.newStudent.AttachmentColl.push({
                        DocumentTypeId: docType.id,
                        DocumentTypeName: docType.text,
                        File: file,
                        Name: file.name,
                        Type: file.type,
                        Size: file.size,
                        Description: des,
                        Path: null
                    });
                })

                $scope.docType = null;
                $scope.attachFile = null;
                $scope.docDescription = '';

                $('#flMoreFiles').val('');
            }
        }
    };
    $scope.ClearStudentPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newStudent.PhotoData = null;
                $scope.newStudent.Photo_TMP = [];
                $scope.newStudent.PhotoPath = '';
            });

            $scope.webCam.Start = false;
            stream = null;
        });
        $('#imgFatherPhoto').attr('src', '');
        $('#imgMotherPhoto').attr('src', '');
        $('#imgGuardian').attr('src', '');
        $('#imgPhoto').attr('src', '');
        $('#imgPhoto1').attr('src', '');

    };

    $scope.AddSiblingDetails = function (ind) {
        if ($scope.newStudent.SiblingDetailColl_TMP) {
            if ($scope.newStudent.SiblingDetailColl_TMP.length > ind + 1) {
                $scope.newStudent.SiblingDetailColl_TMP.splice(ind + 1, 0, {
                    ClassId: null
                })
            } else {
                $scope.newStudent.SiblingDetailColl_TMP.push({
                    ClassId: null
                })
            }
        }
        $timeout(function () {
            var cboInd = (ind + 1);
            $('#cboStudent' + cboInd).select2();
        })

    };
    $scope.delSiblingDetails = function (ind) {
        if ($scope.newStudent.SiblingDetailColl_TMP) {
            if ($scope.newStudent.SiblingDetailColl_TMP.length > 1) {
                $scope.newStudent.SiblingDetailColl_TMP.splice(ind, 1);
            }
        }
    };

    $scope.ClearCitizenFrontPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newStudent.CitizenFrontPhotoData = null;
                $scope.newStudent.CitizenFrontPhoto_TMP = [];
            });

        });

        $('#imgCitizenFront').attr('src', '');
        $('#imgCitizenFront').attr('src', '');
    };


    $scope.ClearCitizenBackPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newStudent.CitizenBackPhotoData = null;
                $scope.newStudent.CitizenBackPhoto_TMP = [];
            });

        });

        $('#imgCitizenBack').attr('src', '');
        $('#imgCitizenBack').attr('src', '');
    };

    $scope.ClearNIDPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newStudent.NIDPhotoData = null;
                $scope.newStudent.NIDPhoto_TMP = [];
            });

        });

        $('#imgNID').attr('src', '');
        $('#imgNID').attr('src', '');
    };

    $scope.ClearSignaturePhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newStudent.SignatureData = null;
                $scope.newStudent.Signature_TMP = [];
            });

        });

        $('#imgSignature').attr('src', '');
        $('#imgSignature1').attr('src', '');
    };

    $scope.ClearStudent = function () {
        $scope.ClearStudentPhoto();
        $scope.ClearSignaturePhoto();
        $scope.ClearCitizenFrontPhoto();
        $scope.ClearCitizenBackPhoto();
        $scope.ClearNIDPhoto();

        $timeout(function () {
            $scope.newStudent = {
                StudentId: null,
                RegNo: '',
                AdmitDate_TMP: new Date(),
                FirstName: '',
                MiddleName: '',
                LastName: '',
                Gender: 1,
                ContactNo: '',
                IsPhysicalDisability: false,
                isEDJ: false,
                Photo: null,
                PhotoPath: null,
                Signature: null,
                SignaturePath: null,
                ClassId: null,
                SectionId: null,
                RollNo: 0,
                IfGurandianIs: 3,
                IsSameAsPermanentAddress: false,
                AcademicDetailsColl: [],
                AttachmentColl: [],
                SiblingDetailColl: [],
                SiblingDetailColl_TMP: [],
                LedgerPanaNo: '',
                IsNewStudent: true,
                F_AnnualIncome: 0,
                M_AnnualIncome: 0,
                EnquiryNo: '',
                ClassId_First: null,
                AdmissionEnquiryId: null,
                PaidUptoMonth: null,

                PassOutClassId: null,
                PassOutSymbolNo: '',
                PassOutGPA: null,
                PassOutGrade: '',
                PassOutRemarks: '',
                EnrollNo: 0,
                CardNo: 0,
                FamilyType: 0,
                PA_WardNo: 0,
                CA_WardNo: 0,
                PA_ProvinceId: null,
                PA_DistrictId: null,
                PA_LocalLevelId: null,
                CA_ProvinceId: null,
                CA_DistrictId: null,
                CA_LocalLevelId: null,
                Mode: 'Save'
            };

            $scope.newStudent.AcademicDetailsColl.push({});
            $scope.newStudent.SiblingDetailColl_TMP.push({});

        });
        AdmissionEnquiryId = null;
    }

    $scope.DefaultPhoto = '/wwwroot/dynamic/images/avatar-img.jpg';

    $scope.CheckAllStudent = function () {
        var val = $scope.newStudentDetails.CheckAll;
        angular.forEach($scope.AllStudentList, function (st) {
            st.IsSelected = val;
        });
    }

    $scope.GetStudentById = function (refData) {

        $scope.loadingstatus = "running";
        showPleaseWait();

        var para = {
            StudentId: refData.StudentId
        };
        $scope.ClearStudent();

        $timeout(function () {
            $http({
                method: 'POST',
                url: base_url + "Academic/Transaction/GetStudentById",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {



                    $timeout(function () {
                        $scope.newStudent = res.data.Data;

                        GlobalServices.getAcademicMonthList($scope.newStudent.StudentId, $scope.newStudent.ClassId).then(function (resAM) {
                            $scope.MonthList = [];
                            angular.forEach(resAM.data.Data, function (m) {
                                $scope.MonthList.push({ id: m.NM, text: m.MonthYear });
                            });
                        });

                        $scope.SelectedClass = mx($scope.ClassList).firstOrDefault(p1 => p1.ClassId == $scope.newStudent.ClassId);

                        if ($scope.SelectedClass) {
                            var semQry = mx($scope.SelectedClass.ClassSemesterIdColl);
                            var cyQry = mx($scope.SelectedClass.ClassYearIdColl);

                            $scope.SelectedClassClassYearList = [];
                            $scope.SelectedClassSemesterList = [];

                            angular.forEach($scope.SemesterList, function (sem) {
                                if (semQry.contains(sem.id)) {
                                    $scope.SelectedClassSemesterList.push({
                                        id: sem.id,
                                        text: sem.text,
                                        SemesterId: sem.id,
                                        Name: sem.Name
                                    });
                                }
                            });

                            angular.forEach($scope.ClassYearList, function (sem) {
                                if (cyQry.contains(sem.id)) {
                                    $scope.SelectedClassClassYearList.push({
                                        id: sem.id,
                                        text: sem.text,
                                        ClassYearId: sem.id,
                                        Name: sem.Name
                                    });
                                }
                            });

                        }

                        if (!$scope.newStudent.AcademicDetailsColl || $scope.newStudent.AcademicDetailsColl.length == 0)
                            $scope.AddAcademicDetails(0);

                        if ($scope.newStudent.AdmitDate)
                            $scope.newStudent.AdmitDate_TMP = new Date($scope.newStudent.AdmitDate);

                        if ($scope.newStudent.DOB_AD)
                            $scope.newStudent.DOB_TMP = new Date($scope.newStudent.DOB_AD);

                        $scope.newStudent.SiblingDetailColl_TMP = [];

                        var csQuery = mx($scope.ClassSectionList.SectionList);

                        angular.forEach($scope.newStudent.SiblingDetailColl, function (sl) {
                            if (sl.ForStudentId || sl.ForStudentId > 0) {
                                var findCS = csQuery.firstOrDefault(p1 => p1.ClassId == sl.ClassId && p1.SectionId == sl.SectionId);

                                if (!findCS)
                                    findCS = csQuery.firstOrDefault(p1 => p1.ClassId == sl.ClassId);

                                sl.SelectedClassSection = findCS;
                                $timeout(function () {
                                    $scope.GetStudentForSibling(sl, 0);
                                });
                                $timeout(function () {
                                    $scope.newStudent.SiblingDetailColl_TMP.push(sl);
                                });

                            }

                        });


                        //Addded By Suresh on Chaita 13 2081
                        var findProvince = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.PA_Province);

                        if (findProvince)
                            $scope.newStudent.PA_ProvinceId = findProvince.id;
                        else
                            $scope.newStudent.PA_ProvinceId = null;

                        var findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.PA_District);
                        if (findDistrict)
                            $scope.newStudent.PA_DistrictId = findDistrict.id;
                        else
                            $scope.newStudent.PA_DistrictId = null;

                        var findArea = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.PA_LocalLevel);
                        if (findArea)
                            $scope.newStudent.PA_LocalLevelId = findArea.id;
                        else
                            $scope.newStudent.PA_LocalLevelId = null;

                        //Current Address
                        var findProvince = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.CA_Province);

                        if (findProvince)
                            $scope.newStudent.CA_ProvinceId = findProvince.id;
                        else
                            $scope.newStudent.CA_ProvinceId = null;

                        var findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.CA_District);
                        if (findDistrict)
                            $scope.newStudent.CA_DistrictId = findDistrict.id;
                        else
                            $scope.newStudent.CA_DistrictId = null;

                        var findArea = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.CA_LocalLevel);
                        if (findArea)
                            $scope.newStudent.CA_LocalLevelId = findArea.id;
                        else
                            $scope.newStudent.CA_LocalLevelId = null;

                        //Ends
                        $timeout(function () {
                            if ($scope.newStudent.SiblingDetailColl_TMP.length == 0)
                                $scope.newStudent.SiblingDetailColl_TMP.push({});
                        });


                        $scope.newStudent.Mode = 'Modify';
                        $('#Editdetail').modal('show');
                    });


                } else {
                    Swal.fire(res.data.ResponseMSG);
                }

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });

    };

    $scope.IsValidStudent = function () {

        if ($scope.newStudent.RegNo.isEmpty()) {
            Swal.fire('Please ! Enter ' + $scope.Labels.RegdNo);
            return false;
        }

        if ($scope.newStudent.FirstName.isEmpty()) {
            Swal.fire('Please ! Enter First Name');
            return false;
        }

        if ($scope.SelectedClass && $scope.SelectedClass.FacultyId > 0) {
            if ($scope.AcademicConfig.ActiveBatch == true) {

                if ($scope.newStudent.BatchId > 0) { }
                else {
                    Swal.fire('Please ! Select Batch');
                    return false;
                }
            }


            if ($scope.AcademicConfig.ActiveSemester == true) {

                if ($scope.AcademicConfig.ActiveClassYear == true) {
                    if ($scope.newStudent.SemesterId > 0 || $scope.newStudent.ClassYearId > 0) { }
                    else {
                        Swal.fire('Please ! Select Semester/ClassYear');
                        return false;
                    }
                }
                else {

                    if ($scope.newStudent.SemesterId > 0) { }
                    else {
                        Swal.fire('Please ! Select Semester');
                        return false;
                    }
                }

            }

            if ($scope.AcademicConfig.ActiveClassYear == true) {

                if ($scope.AcademicConfig.ActiveSemester == true) {
                    if ($scope.newStudent.SemesterId > 0 || $scope.newStudent.ClassYearId > 0) { }
                    else {
                        Swal.fire('Please ! Select Semester/ClassYear');
                        return false;
                    }
                }
                else {

                    if ($scope.newStudent.ClassYearId > 0) { }
                    else {
                        Swal.fire('Please ! Select Class Year');
                        return false;
                    }
                }

            }
        }



        return true;
    }

    $scope.SaveUpdateStudent = function () {
        if ($scope.IsValidStudent() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newStudent.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateStudent();
                    }
                });
            } else
                $scope.CallSaveUpdateStudent();

        }
    };

    $scope.CallSaveUpdateStudent = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var refStudent = false;

        if ($scope.newStudent.RegistrationId && $scope.newStudent.RegistrationId > 0)
            refStudent = true;
        else if ($scope.newStudent.AdmissionEnquiryId && $scope.newStudent.AdmissionEnquiryId > 0)
            refStudent = true;

        var filesColl = $scope.newStudent.AttachmentColl;
        //$scope.newStudent.AttachmentColl = [];

        var photo = $scope.newStudent.Photo_TMP;
        var signature = $scope.newStudent.Signature_TMP;

        var fatherphoto = $scope.newStudent.FatherPhoto_TMP;
        var motherphoto = $scope.newStudent.MotherPhoto_TMP;
        var guardianphoto = $scope.newStudent.GuardianPhoto_TMP;


        //Add js to save photo by Prashant
        var citizenshipFrontphoto = $scope.newStudent.CitizenFrontPhoto_TMP;
        var citizenshipBackphoto = $scope.newStudent.CitizenBackPhoto_TMP;
        var nationalIdPhoto = $scope.newStudent.NIDPhoto_TMP;


        if ($scope.newStudent.PhotoData && (!photo || photo.length == 0)) {
            photo = [];

            photo.push(dataURItoFile($scope.newStudent.PhotoData));
        }

        if ($scope.newStudent.AdmitDateDet) {
            $scope.newStudent.AdmitDate = $filter('date')(new Date($scope.newStudent.AdmitDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newStudent.AdmitDate = new Date();

        if ($scope.newStudent.DOB_ADDet) {
            $scope.newStudent.DOB_AD = $filter('date')(new Date($scope.newStudent.DOB_ADDet.dateAD), 'yyyy-MM-dd');
        }

        $scope.newStudent.SiblingDetailColl = [];
        angular.forEach($scope.newStudent.SiblingDetailColl_TMP, function (sl) {
            if ((sl.ForStudentId || sl.ForStudentId > 0) && sl.SelectedClassSection) {
                if (sl.Relation) {
                    var newSB = {
                        ForStudentId: sl.ForStudentId,
                        ClassId: sl.SelectedClassSection.ClassId,
                        SectionId: sl.SelectedClassSection.SectionId,
                        Relation: sl.Relation,
                        Remarks: sl.Remarks
                    }
                    $scope.newStudent.SiblingDetailColl.push(newSB);
                }
            }
        });



        //added by Suresh on Chaitra 10 starts
        var selectData1 = $('#cboProvincePA').select2('data');
        if (selectData1 && selectData1.length > 0)
            province1 = selectData1[0].text.trim();

        selectData1 = $('#cboDistrictPA').select2('data');
        if (selectData1 && selectData1.length > 0)
            district1 = selectData1[0].text.trim();


        selectData1 = $('#cboAreaPA').select2('data');
        if (selectData1 && selectData1.length > 0)
            area1 = selectData1[0].text.trim();


        var selectData2 = $('#cboProvinceCA').select2('data');
        if (selectData2 && selectData2.length > 0)
            province2 = selectData2[0].text.trim();

        selectData2 = $('#cboDistrictCA').select2('data');
        if (selectData2 && selectData2.length > 0)
            district2 = selectData2[0].text.trim();


        selectData2 = $('#cboAreaCA').select2('data');
        if (selectData2 && selectData2.length > 0)
            area2 = selectData2[0].text.trim();

        $scope.newStudent.PA_Province = province1;
        $scope.newStudent.PA_District = district1;
        $scope.newStudent.PA_LocalLevel = area1;
        $scope.newStudent.CA_Province = province2;
        $scope.newStudent.CA_District = district2;
        $scope.newStudent.CA_LocalLevel = area2;
        //Ends

        $http({
            method: 'POST',
            url: base_url + "Academic/Transaction/SaveStudent",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                if (data.files) {
                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("file" + i, data.files[i].File);
                    }
                }

                if (data.stPhoto && data.stPhoto.length > 0)
                    formData.append("photo", data.stPhoto[0]);

                if (data.stSignature && data.stSignature.length > 0)
                    formData.append("signature", data.stSignature[0]);

                if (data.fphoto && data.fphoto.length > 0)
                    formData.append("father", data.fphoto[0]);

                if (data.mphoto && data.mphoto.length > 0)
                    formData.append("mother", data.mphoto[0]);

                if (data.gphto && data.gphto.length > 0)
                    formData.append("guardian", data.gphto[0]);

                //Add js to save photo by Prashant
                if (data.cfrontphto && data.cfrontphto.length > 0)
                    formData.append("citizentfront", data.cfrontphto[0]);

                if (data.cbackphto && data.cbackphto.length > 0)
                    formData.append("citizentBack", data.cbackphto[0]);

                if (data.nidphto && data.nidphto.length > 0)
                    formData.append("nidphoto", data.nidphto[0]);

                return formData;
            },
            data: {
                jsonData: $scope.newStudent, files: filesColl, stPhoto: photo, stSignature: signature, fphoto: fatherphoto, mphoto: motherphoto, gphto: guardianphoto,
                cfrontphto: citizenshipFrontphoto, cbackphto: citizenshipBackphoto, nidphto: nationalIdPhoto
            }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            var billPrint = ($scope.newStudent.PaidUptoMonth && $scope.newStudent.PaidUptoMonth > 0) ? true : false;
            var mid = $scope.newStudent.PaidUptoMonth;
            if (res.data.IsSuccess == true) {
                $scope.ClearStudent();

                $('#Editdetail').modal('hide');
                $scope.Print(res.data.Data.RId);

                if (billPrint) {
                    $scope.PrintBill(res.data.Data.RId, mid);
                }

                if (refStudent == true)
                    refreshActiveTab();

            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    }

    //************************* END Student Edit *********************************

    //************************* Start Student Photo *******************************

    $scope.UploadStudentPhoto = function (st) {
        var photo = st.Photo_TMP;
        var beData = {
            StudentId: st.StudentId
        };
        $http({
            method: 'POST',
            url: base_url + "Academic/Transaction/UpdateStuentPhoto",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                if (data.stPhoto && data.stPhoto.length > 0)
                    formData.append("photo", data.stPhoto[0]);
                return formData;
            },
            data: { jsonData: beData, stPhoto: photo }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                st.PhotoPath = res.data.Data.ResponseId;
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    //************************* End Student Photo *******************************

    //************************* Start Student Profile *******************************

    $scope.PrintStudentInfo = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            StudentId: refData.StudentId
        };
        $scope.ClearStudent();
        $timeout(function () {
            $http({
                method: 'POST',
                url: base_url + "Academic/Report/PrintStudentInfo",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {
                    $timeout(function () {
                        $scope.print = res.data.Data;
                        $scope.print.Mode = 'Print';
                        $('#StudentProfile').modal('show');
                    });
                } else {
                    Swal.fire(res.data.ResponseMSG);
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });
    };

    $scope.PrintData = function () {
        $('.mgbtn').css('margin-top', '520px'); // Apply margin before printing
        $('#printcard').printThis();
    };

    //************************* END Student Profile*********************************

    //************************* Start Student Remarks*********************************

    $scope.ShowRemarksModal = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });
        if (selectedStudentIdColl.length === 0) {
            Swal.fire("Please ! Select Student From List");
            return;
        }
        $scope.ClearRemarks();
        $('#Remarksmodal').modal('show');
    }

    $scope.ClearRemarks = function () {
        $scope.newRemarks = {
            StudentRemarksId: null,
            ForDate_TMP: new Date(),
            RemarksFor: null,
            RemarksTypeId: null,
            Point: 0,
            Remarks: '',
            AttachFile: '',
            Mode: 'Save'
        };
        document.getElementById('choose-file').value = '';
    }

    $scope.SaveStudentRemarks = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });
        // Check if any students are selected
        if (selectedStudentIdColl.length === 0) {
            Swal.fire("Please ! Select Student From List");
            return;
        }
        $scope.loadingstatus = "running";
        showPleaseWait();
        // Loop through each selected student and send a request
        var saveCount = 0;
        var totalToSave = selectedStudentIdColl.length;

        selectedStudentIdColl.forEach(function (studentId) {
            var remarksData = {
                StudentId: studentId,
                ForDate: $filter('date')($scope.newRemarks.ForDateDet.dateAD, 'yyyy-MM-dd'),
                RemarksFor: $scope.newRemarks.RemarksFor,
                RemarksTypeId: $scope.newRemarks.RemarksTypeId,
                Point: $scope.newRemarks.Point,
                Remarks: $scope.newRemarks.Remarks,
                AttachFile: $scope.newRemarks.AttachFile
            };
            // Send data to server using $http
            $http({
                method: 'POST',
                url: base_url + "Academic/Creation/SaveStudentRemarks",
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    var formData = new FormData();
                    formData.append("jsonData", angular.toJson(data.jsonData));

                    if (data.files) {
                        formData.append("file0", data.files[0]);
                    }

                    return formData;
                },
                data: { jsonData: remarksData, files: remarksData.AttachFile }
            }).then(function (res) {
                saveCount++;

                if (saveCount === totalToSave) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    $scope.ClearRemarks();

                    if (res.data.IsSuccess === true) {
                        $('#Remarksmodal').modal('hide');
                        Swal.fire(res.data.ResponseMSG);
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }
                }
            }, function (error) {
                saveCount++;

                if (saveCount === totalToSave) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    Swal.fire("Failed to save remarks. Please try again.");
                }
            });
        });
    };

    //************************* END Student Remarks*********************************

    //************************* Start Student PTM*********************************

    $scope.ShowPTMModal = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });
        if (selectedStudentIdColl.length !== 1) {
            Swal.fire("Please ! Select Only One Student From List");
            return;
        }
        $scope.ClearPTM();
        $('#PTMmodal').modal('show');
    }

    $scope.ClearPTM = function () {
        $scope.newPTM = {
            TranId: null,
            ClassId: null,
            SectionId: null,
            PTMDate_TMP: new Date(),
            PTMAttendBy: 1,
            Description: '',
            StudentPTMColl: [],
            SelectedClass: null,
            Mode: 'Save'
        };
    }

    $scope.setParentGuardianNames = function () {
        $scope.newPTM.FatherName = "";
        $scope.newPTM.MotherName = "";
        $scope.newPTM.GuardianName = "";
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected === true) {
                if ($scope.newPTM.PTMAttendBy == 2) {
                    $scope.newPTM.FatherName = st.FatherName;
                } else if ($scope.newPTM.PTMAttendBy == 3) {
                    $scope.newPTM.MotherName = st.MotherName;
                } else if ($scope.newPTM.PTMAttendBy == 4) {
                    $scope.newPTM.GuardianName = st.GuardianName;
                }
            }
        });
    };


    $scope.SaveStudentPTM = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st);
            }
        });
        $scope.loadingstatus = "running";
        showPleaseWait();
        // Loop through each selected student and send a request
        var saveCount = 0;
        var totalToSave = selectedStudentIdColl.length;

        selectedStudentIdColl.forEach(function (st) {
            var ptmData = {
                StudentId: st.StudentId,
                ClassId: $scope.newStudentSummaryList.ClassId,
                SectionId: $scope.newStudentSummaryList.SectionId ?? null,
                PTMDate: $filter('date')($scope.newPTM.PTMDateDet.dateAD, 'yyyy-MM-dd'),
                EmployeeId: $scope.newPTM.EmployeeId,
                Description: $scope.newPTM.Description,
                PTMAttendBy: $scope.newPTM.PTMAttendBy,
                PTMBy: $scope.newPTM.EmployeeId,
                TeacherRemarks: $scope.newPTM.TeacherRemarks,
                ParentRemarks: $scope.newPTM.ParentRemarks,
                Recommendation: $scope.newPTM.Recommendation,
            };
            // Send data to server using $http
            $http({
                method: 'POST',
                url: base_url + "Academic/Creation/SaveParentTeacherMeeting",
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    var formData = new FormData();
                    formData.append("jsonData", angular.toJson(data.jsonData));

                    return formData;
                },
                data: { jsonData: ptmData }
            }).then(function (res) {
                saveCount++;

                if (saveCount === totalToSave) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    $scope.ClearRemarks();

                    if (res.data.IsSuccess === true) {
                        $('#PTMmodal').modal('hide');
                        Swal.fire(res.data.ResponseMSG);
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }
                }
            }, function (error) {
                saveCount++;

                if (saveCount === totalToSave) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    Swal.fire("Failed to save remarks. Please try again.");
                }
            });
        });
    };

    //************************* END Student PTM***************************************************
    //************************* Start Student ID Card Edit *********************************
    $scope.ShowIDCardModal = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });
        if (selectedStudentIdColl.length === 0) {
            Swal.fire("Please ! Select Student From List");
            return;
        }
        $('#StudentIdCard').modal('show');
        $scope.ClearIDCard();
    }

    $scope.ClearIDCard = function () {
        $scope.IdCard_Para = {
            ReportTemplateId: null,
            IssueDate_TMP: new Date(),
            ExpiryDate_TMP: new Date(),
            Mode: 'Save'
        };
    }


    $scope.IdCard_Coll = [];
    $scope.ViewIdCard = function () {
        $scope.IdCard_Coll = [];
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });

        if (!$scope.IdCard_Para.IssueDateDet) {
            Swal.fire("Please ! Select Issue Date")
            return;
        }

        if (!$scope.IdCard_Para.ExpiryDateDet) {
            Swal.fire("Please ! Select Expire Date")
            return;
        }

        if (selectedStudentIdColl && selectedStudentIdColl.length > 0) {

            $scope.loadingstatus = "running";
            showPleaseWait();

            var pc = {};
            pc['StudentIdColl'] = selectedStudentIdColl.toString();
            pc['ValidFrom'] = $filter('date')(new Date($scope.IdCard_Para.IssueDateDet.dateAD));
            pc['ValidTo'] = $filter('date')(new Date($scope.IdCard_Para.ExpiryDateDet.dateAD));

            var para = {
                procName: "GetStudentListForIdCard",
                qry: '',
                asParentChild: false,
                tblNames: '',
                colRelations: '',
                paraColl: pc,
            };
            $http({
                method: 'POST',
                url: base_url + "Global/GetCustomData",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess == true) {
                    $scope.IdCard_Coll = res.data.Data;

                    $timeout(function () {
                        const qrcodeContainer = document.getElementById("qrcode");
                        qrcodeContainer.innerHTML = ""; // Clear previous QR code if any

                        if ($scope.IdCard_Coll.QrCode) {
                            new QRCode(qrcodeContainer, {
                                text: $scope.IdCard_Coll.QrCode,
                                width: 140,
                                height: 140,
                                colorDark: "#000000",
                                colorLight: "#ffffff",
                                correctLevel: QRCode.CorrectLevel.L
                            });
                        }

                    });

                } else {
                    Swal.fire(res.data.ResponseMSG);
                }

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        } else {
            Swal.fire('Please ! Select Student From List');
        }

    };

    //************************* End Student ID Card Edit ********************************

    //************************* Start Student Admit Card *********************************
    $scope.ClearACPara = function () {
        $scope.AdmitCard_Para = {
            TemplateId: null,
            ExamTypeId: null,
            StudentIdColl: ''
        };
    }
    $scope.ShowAdmitCardModal = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });
        if (selectedStudentIdColl.length === 0) {
            Swal.fire("Please ! Select Student From List");
            return;
        }
        $scope.ClearACPara();
        $('#AdmitCard').modal('show');
    }

    $scope.AdmitCard_Coll = [];

    $scope.ViewAdmitCard = function () {
        $scope.AdmitCard_Coll = [];
        if (!$scope.AdmitCard_Para.ExamTypeId) {
            Swal.fire("Please ! Select ExamType");
            return;
        }
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected === true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });
        if (selectedStudentIdColl.length === 0) {
            Swal.fire('Please ! Select Student From List');
            return;
        }
        $scope.loadingstatus = "running";
        showPleaseWait();
        var pc = {};
        pc['StudentIdColl'] = selectedStudentIdColl.toString();
        pc['ExamTypeId'] = $scope.AdmitCard_Para.ExamTypeId;
        var para = {
            procName: "PrintAdmitCard",
            qry: '',
            asParentChild: false,
            tblNames: '',
            colRelations: '',
            paraColl: pc
        };
        $http({
            method: 'POST',
            url: base_url + "Global/GetCustomData",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess === true && res.data.Data) {
                $scope.AdmitCard_Coll = res.data.Data;

                $scope.AdmitCard_Coll.forEach(function (item) {
                    if (item.SubjectDetails) {
                        const detailsArray = item.SubjectDetails.split(',');
                        item.SubjectScheduleList = detailsArray.map(function (entry) {
                            const [subject, detail] = entry.trim().split('=');
                            const [date, time] = detail.trim().split(' ');
                            return {
                                Subject: subject.trim(),
                                Date: date.trim(),
                                Time: detail.trim().substring(date.trim().length + 1).trim()  // time part
                            };
                        });
                    }
                });
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire('Failed: ' + reason);
        });
    };

    $scope.PrintAdmitCard = function () {
        $('#printAC').printThis()
    };


    //************************* End Student Admit Card *********************************

    //************************* Start Student Certificate *********************************
    $scope.ClearCertificatePara = function () {
        $scope.Certificate_Para = {
            CertificateTypeId: null,
            TemplateId: 1,
            ExamTypeId: null,
            AcademicYearId: null,
            StudentIdColl: ''
        };
    }
    $scope.ShowCertificateModal = function () {
        var selectedStudent = null;
        var selectedCount = 0;
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected === true) {
                selectedStudent = st;
                selectedCount++;
            }
        });
        if (selectedCount === 0) {
            Swal.fire("Please select a student from the list.");
            return;
        }
        if (selectedCount > 1) {
            Swal.fire("Please select only one student.");
            return;
        }
        $scope.ClearCertificatePara();
        $('#certificatemodal').modal('show');
    };


    $scope.ViewTCCertificate = function () {
        var selectedStudent = null;
        var selectedCount = 0;

        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected === true) {
                selectedStudent = st;
                selectedCount++;
            }
        });

        if (selectedCount === 0) {
            Swal.fire("Please select a student.");
            return;
        }

        if (selectedCount > 1) {
            Swal.fire("Only one student can be selected.");
            return;
        }

        if (!selectedStudent.TranId || selectedStudent.TranId <= 0) {
            Swal.fire("Selected student does not have a valid TC TranId.");
            return;
        }

        $scope.TCertificate_Coll = [];
        $scope.loadingstatus = "running";
        showPleaseWait();

        var pc = {
            TranId: selectedStudent.TranId
        };

        var para = {
            procName: "PrintTC",
            qry: '',
            asParentChild: false,
            tblNames: '',
            colRelations: '',
            paraColl: pc
        };

        $http({
            method: 'POST',
            url: base_url + "Global/GetCustomData",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess === true && res.data.Data) {
                $scope.TCertificate_Coll = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire('Failed: ' + reason);
        });
    };
    $scope.ViewCCCertificate = function () {
        var selectedStudent = null;
        var selectedCount = 0;

        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected === true) {
                selectedStudent = st;
                selectedCount++;
            }
        });
        if (selectedCount === 0) {
            Swal.fire("Please select a student.");
            return;
        }
        if (selectedCount > 1) {
            Swal.fire("Only one student can be selected.");
            return;
        }
        if (!selectedStudent.CCTranId || selectedStudent.CCTranId <= 0) {
            Swal.fire("Selected student does not have a valid CC TranId.");
            return;
        }
        $scope.CCCertificate_Coll = [];
        $scope.loadingstatus = "running";
        showPleaseWait();
        var pc = {
            TranId: selectedStudent.CCTranId
        };
        var para = {
            procName: "PrintCC",
            qry: '',
            asParentChild: false,
            tblNames: '',
            colRelations: '',
            paraColl: pc
        };

        $http({
            method: 'POST',
            url: base_url + "Global/GetCustomData",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess === true && res.data.Data) {
                $scope.CCCertificate_Coll = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire('Failed: ' + reason);
        });
    };


    //************************* End Student Certificate *********************************

    //************************* Start Student GMark Sheet *********************************

    $scope.ShowMarksheetModal = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });
        if (selectedStudentIdColl.length === 0) {
            Swal.fire("Please ! Select Student From List");
            return;
        }
        $scope.ClearMarkSheet();
        $('#Marksheetmodal').modal('show');
    }

    $scope.ClearMarkSheet = function () {
        $scope.GMarkSheet_Para = {
            ReportTemplateId: 1,
            ExamTypeId: null,
            TemplateId: null,
            StudentIdColl: ''
        }
    }

    $scope.ChunkedGradeList = [];
    $scope.chunkGradeList = function () {
        $scope.ChunkedGradeList = [];
        for (let i = 0; i < $scope.GradeList.length; i += 2) {
            $scope.ChunkedGradeList.push([$scope.GradeList[i], $scope.GradeList[i + 1]]);
        }
    };

    $scope.GMarkSheet_Coll = [];
    $scope.ViewGMarkSheet = function () {
        $scope.chunkGradeList();
        $scope.GMarkSheet_Coll = [];
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });

        if (!$scope.GMarkSheet_Para.ExamTypeId) {
            Swal.fire("Please ! Select ExamType")
            return;
        }

        if (selectedStudentIdColl && selectedStudentIdColl.length > 0) {

            $scope.loadingstatus = "running";
            showPleaseWait();

            var pc = {};
            pc['StudentIdColl'] = selectedStudentIdColl.toString();
            pc['ExamTypeId'] = $scope.GMarkSheet_Para.ExamTypeId;

            var para = {
                procName: "PrintMarkSheet_Only",
                qry: '',
                asParentChild: true,
                tblNames: 'StudentColl,MarkColl',
                colRelations: 'StudentId,StudentId',
                paraColl: pc,
            };
            $http({
                method: 'POST',
                url: base_url + "Global/GetCustomData",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess == true) {
                    $scope.GMarkSheet_Coll = res.data.Data.StudentColl;
                } else {
                    var exam = $scope.ExamTypeList.find(function (x) {
                        return x.ExamTypeId == $scope.GMarkSheet_Para.ExamTypeId;
                    });
                    var examName = exam ? exam.DisplayName : 'Selected Exam';

                    Swal.fire("Please! Publish Marksheet For " + examName + ".");
                }

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        } else {
            Swal.fire('Please ! Select Student From List');
        }

    };

    $scope.PrintGMarkSheet = function () {
        var templateId = $scope.GMarkSheet_Para.TemplateId;
        var $printTargets = $("page[id^='printableTCC']").filter(function () {
            return $(this).attr('id') === 'printableTCC' + templateId;
        });

        if ($printTargets.length > 0) {
            $printTargets.printThis({
                importCSS: true,
                importStyle: true,
                pageTitle: "Marksheet",
                removeInline: false,
                printDelay: 500
            });
        } else {
            alert("No matching templates found for printing.");
        }
    };





    //************************* End Student GMark Sheet *********************************

    //************************* Start Student  AMark Sheet *********************************
    $scope.AMarkSheet_Coll = [];
    $scope.ViewAMarkSheet = function () {
        $scope.AMarkSheet_Coll = [];
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });

        if (!$scope.AMarkSheet_Para.ExamTypeId) {
            Swal.fire("Please ! Select ExamType")
            return;
        }

        if (selectedStudentIdColl && selectedStudentIdColl.length > 0) {

            $scope.loadingstatus = "running";
            showPleaseWait();

            var pc = {};
            pc['StudentIdColl'] = selectedStudentIdColl.toString();
            pc['ExamTypeGroupId'] = $scope.AMarkSheet_Para.ExamTypeId;

            var para = {
                procName: "PrintGroupMarkSheet_Only",
                qry: '',
                asParentChild: true,
                tblNames: 'StudentColl,MarkColl',
                colRelations: 'StudentId,StudentId',
                paraColl: pc,
            };
            $http({
                method: 'POST',
                url: base_url + "Global/GetCustomData",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess == true) {
                    $scope.AMarkSheet_Coll = res.data.Data.StudentColl;
                } else {
                    Swal.fire(res.data.ResponseMSG);
                }

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        } else {
            Swal.fire('Please ! Select Student From List');
        }

    };

    //************************* End StudentA Mark Sheet *********************************

    //************************* Start StudentLeft *********************************

    $scope.ShowStudentLeftModal = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });
        if (selectedStudentIdColl.length === 0) {
            Swal.fire("Please ! Select Student From List");
            return;
        }
        $scope.ClearStudentLeft();
        $('#leftmodal').modal('show');
    }

    $scope.ClearStudentLeft = function () {
        $scope.newLeft = {
            LeftStudentId: null,
            LeftDate_TMP: new Date(),
            StatusId: null,
            IsLeft: false,
            LeftRemarks: '',
            Notification: false,
            SMS: false,
            Mode: 'Save'
        };
    }

    $scope.SaveUpdateLeftStudent = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st);
            }
        });
        if (selectedStudentIdColl.length === 0) {
            Swal.fire("Please! Select Student From List");
            return;
        }
        $scope.loadingstatus = "running";
        showPleaseWait();
        var dataColl = [];
        angular.forEach(selectedStudentIdColl, function (st) {
            var item = {
                StudentId: st.StudentId,
                ClassId: st.ClassId,
                SectionId: st.SectionId,
                SemesterId: st.SemesterId,
                ClassYearId: st.ClassYearId,
                IsLeft: $scope.newLeft.IsLeft,
                LeftDate_AD: $filter('date')(new Date(($scope.newLeft.LeftDateDet ? $scope.newLeft.LeftDateDet.dateAD : $scope.newLeft.LeftDate_TMP)), 'yyyy-MM-dd'),
                LeftRemarks: $scope.newLeft.LeftRemarks,
                StatusId: $scope.newLeft.StatusId
            };
            dataColl.push(item);
        });

        // Send one request with an array
        $http({
            method: 'POST',
            url: base_url + "Academic/Transaction/SaveStudentLeft",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: { jsonData: dataColl }
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.IsSuccess === true) {
                $('#leftmodal').modal('hide');
                Swal.fire("Left student remarks saved successfully!");
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function () {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire("Error saving left remarks. Please try again.");
        });
    };

    //************************* End StudentLeft *********************************

    //************************* Start Student class Upgrade *********************************

    $scope.ShowClassUpgradeModal = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });
        if (selectedStudentIdColl.length === 0) {
            Swal.fire("Please ! Select Student From List");
            return;
        }
        $scope.ClearClassUpgrade();
        if (selectedStudentIdColl.length === 1) {
            var selectedStudent = $scope.AllStudentList.find(function (s) {
                return s.StudentId === selectedStudentIdColl[0];
            });
            $scope.SelectedStudentClassSection = selectedStudent.ClassSection;
        } else {
            $scope.SelectedStudentClassSection = null;
        }

        $('#Upgrademodal').modal('show');
    };


    $scope.ClearClassUpgrade = function () {
        $scope.newUpgrade = {
            StudentId: null,
            ClassId: null,
            SectionId: null,
            Mode: 'Save'
        };
    }

    $scope.UpgradeClassSection = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st);
            }
        });
        $scope.loadingstatus = "running";
        showPleaseWait();
        // Loop through each selected student and send a request
        var saveCount = 0;
        var totalToSave = selectedStudentIdColl.length;

        selectedStudentIdColl.forEach(function (st) {
            var ClassSecData = {
                StudentId: st.StudentId,
                ClassId: $scope.newUpgrade.ClassId,
                SectionId: $scope.newUpgrade.SectionId,
            };
            // Send data to server using $http
            $http({
                method: 'POST',
                url: base_url + "Academic/Transaction/UpgradeClassSection",
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    var formData = new FormData();
                    formData.append("jsonData", angular.toJson(data.jsonData));
                    return formData;
                },
                data: { jsonData: ClassSecData }
            }).then(function (res) {
                saveCount++;

                if (saveCount === totalToSave) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    $scope.ClearClassUpgrade();

                    if (res.data.IsSuccess === true) {
                        $('#Upgrademodal').modal('hide');
                        Swal.fire(res.data.ResponseMSG);
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }
                }
            }, function (error) {
                saveCount++;
                if (saveCount === totalToSave) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    Swal.fire("Failed to upgrade class & section. Please try again.");
                }
            });
        });
    };

    //************************* End Student class Upgrade *********************************

    //************************* Start Student Document *********************************
    $scope.ShowStdDocModal = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });
        if (selectedStudentIdColl.length !== 1) {
            Swal.fire("Please ! Select Only One Student From List");
            return;
        }
        $scope.ClearStudentDocument();
        var studentRef = {
            StudentId: selectedStudentIdColl[0]
        };
        $scope.GetStudentDoc(studentRef);
       
        $('#AttachDocument').modal('show');
    };

    $scope.GetStudentDoc = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            StudentId: refData.StudentId
        };
        $scope.ClearStudentDocument();
        $timeout(function () {
            $http({
                method: 'POST',
                url: base_url + "Academic/Transaction/GetStudentById",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {
                    $timeout(function () {
                        $scope.newStudentDocument = res.data.Data;
                        $scope.newStudentDocument.Mode = 'Modify';
                        $('#AttachDocument').modal('show');
                    });
                } else {
                    Swal.fire(res.data.ResponseMSG);
                }

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });

    };


    $scope.ClearStudentDocument = function () {
        $scope.newStudentDocument = {
            StudentId: null,
            AttachmentColl: [],
            Mode: 'Save'
        };
    }

    $scope.delAttachmentFile = function (ind) {
        if ($scope.newStudentDocument.AttachmentColl) {
            if ($scope.newStudentDocument.AttachmentColl.length > 0) {
                $scope.newStudentDocument.AttachmentColl.splice(ind, 1);
            }
        }
    }
    $scope.AddMoreFile = function (files, docType, des) {
        if (files && docType) {
            if (files != null && docType != null) {
                angular.forEach(files, function (file) {
                    $scope.newStudentDocument.AttachmentColl.push({
                        DocumentTypeId: docType.id,
                        DocumentTypeName: docType.text,
                        File: file,
                        Name: file.name,
                        Type: file.type,
                        Size: file.size,
                        Description: des,
                        Path: null
                    });
                })

                $scope.docType = null;
                $scope.attachFile = null;
                $scope.docDescription = '';

                $('#flMoreFiles').val('');
            }
        }
    };

    $scope.UpdateStdDocument = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var filesColl = $scope.newStudentDocument.AttachmentColl;
        $http({
            method: 'POST',
            url: base_url + "Academic/Transaction/UpdateStudentDocument",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                if (data.files) {
                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("file" + i, data.files[i].File);
                    }
                }

                return formData;
            },
            data: { jsonData: $scope.newStudentDocument, files: filesColl }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearStudentDocument();
                $('#AttachDocument').modal('hide');
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.ShowPersonalImg = function (item) {
        $scope.viewImg = {
            ContentPath: '',
            FileType: null
        };

        if (item.DocPath && item.DocPath.length > 0) {
            $scope.viewImg.ContentPath = item.DocPath;
            $scope.viewImg.FileType = 'pdf';  // Assuming DocPath is for PDFs
            document.getElementById('pdfViewer').src = item.DocPath;
            $('#PersonalImg').modal('show');
        } else if (item.PhotoPath && item.PhotoPath.length > 0) {
            $scope.viewImg.ContentPath = item.PhotoPath;
            $scope.viewImg.FileType = 'image';  // Assuming PhotoPath is for images
            $('#PersonalImg').modal('show');
        } else if (item.File) {
            var blob = new Blob([item.File], { type: item.File?.type });
            $scope.viewImg.ContentPath = URL.createObjectURL(blob);
            $scope.viewImg.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

            if ($scope.viewImg.FileType === 'pdf') {
                document.getElementById('pdfViewer').src = $scope.viewImg.ContentPath;
            }

            $('#PersonalImg').modal('show');
        } else {
            Swal.fire('No Image Found');
        }
    };

    //************************* End Student Document *********************************

    //************************* Start Student Leave Entry *********************************

    $scope.ShowStdLeaveEntryModal = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });
        if (selectedStudentIdColl.length !== 1) {
            Swal.fire("Please ! Select Only One Student From List");
            return;
        }
        $scope.ClearLeaveEntry();
        var studentRef = {
            StudentId: selectedStudentIdColl[0]
        };
        $scope.GetStudentLeaveReq(studentRef);

        $('#leaverequestmodal').modal('show');
    };

    $scope.GetStudentLeaveReq = function (beData) {
        $scope.CurLeave = beData;
        showPleaseWait();
        $scope.StdWiseRequestColl = [];
        var findP = $scope.AcademicYearList[0];
        var dateFrom = null;
        var dateTo = null;
        if (findP) {
            dateFrom = $filter('date')(new Date(findP.StartDate), 'yyyy-MM-dd');
            dateTo = $filter('date')(new Date(findP.EndDate), 'yyyy-MM-dd');
        }
        var para = {
            LeaveStatus: 0,
            dateFrom: dateFrom,
            dateTo: dateTo,
            StudentId: beData.StudentId
        };
        $http({
            method: 'POST',
            url: base_url + "Attendance/Creation/GetStudentLeaveReq",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.StdWiseRequestColl = res.data.Data.LeaveColl;

                $('#leaverequestmodal').modal('show');
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed: ' + reason);
        });
    };


    $scope.DateChanged = function () {
        if ($scope.newLeaveEntry.DateFromDet && $scope.newLeaveEntry.DateToDet) {
            var dt1 = new Date(($filter('date')(new Date($scope.newLeaveEntry.DateFromDet.dateAD), 'yyyy-MM-dd')));
            var dt2 = new Date(($filter('date')(new Date($scope.newLeaveEntry.DateToDet.dateAD), 'yyyy-MM-dd')));
            $scope.newLeaveEntry.NoOfDays = datediff(dt1, dt2) + 1;
        }
    }

    $scope.ClearLeaveEntry = function () {
        $timeout(function () {
            $scope.newLeaveEntry = {
                LeaveEntryId: null,
                LeaveTypeId: null,
                DurationTypeId: null,
                LeavePeriodId: null,
                Remarks: '',
                AttachDocument: '',
                LeaveTo: 1,
                AttachmentColl: [],
                DateFrom_TMP: new Date(),
                DateTo_TMP: new Date(),
                NoOfDays: 1,
                Mode: 'Save'
            };

            $('input[type=file]').val('');
        });
    };


    $scope.SaveLeaveEntry = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st);
            }
        });
        $scope.newLeaveEntry.StudentId = selectedStudentIdColl[0].StudentId;

        $scope.loadingstatus = "running";
        showPleaseWait();

        var filesColl = $scope.newLeaveEntry.Photo_TMP;

        if ($scope.newLeaveEntry.DateFromDet) {
            $scope.newLeaveEntry.DateFrom = $filter('date')(new Date($scope.newLeaveEntry.DateFromDet.dateAD), 'yyyy-MM-dd');
        }
        if ($scope.newLeaveEntry.DateToDet) {
            $scope.newLeaveEntry.DateTo = $filter('date')(new Date($scope.newLeaveEntry.DateToDet.dateAD), 'yyyy-MM-dd');
        }

        $http({
            method: 'POST',
            url: base_url + "Attendance/Transaction/SaveLeaveRequest",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                if (data.files) {
                    for (var i = 0; i < data.files.length; i++) {
                        if (data.files[i].File)
                            formData.append("file" + i, data.files[i].File);
                        else
                            formData.append("file" + i, data.files[i]);
                    }
                }
                return formData;
            },
            data: { jsonData: $scope.newLeaveEntry, files: filesColl }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearLeaveEntry();
                $('#leaverequestmodal').modal('hide');
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    //************************* End Student Leave Entry *********************************

    //************************* Start Student Visitor  *********************************

    $scope.ShowStdVisitorModal = function () {
        var selectedStudentIdColl = [];
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudentIdColl.push(st.StudentId);
            }
        });
        if (selectedStudentIdColl.length !== 1) {
            Swal.fire("Please ! Select Only One Student From List");
            return;
        }
        $scope.ClearVisitor();
       /* $scope.GetStudentLeaveReq(studentRef);*/

        $('#VisitorsRecord').modal('show');
    };

    $scope.ClearVisitor = function () {
        $timeout(function () {
            $scope.newVisitor = {
                VisitorId: null,
                Name: '',
                Address: '',
                Contact: '',
                Email: '',
                VisitorRelation: 1,
                Purpose: '',
                //InTime: new Date(),
                //InTime_TMP: new Date(),
                ValidityTime: null,
                OutTime: null,
                Remarks: '',
                TypeOfDocumentId: null,
                AttachDocument: '',
                Description: '',
                MeeTo: 1,
                AttachmentColl: [],
                FromDate_TMP: new Date(),
                ToDate_TMP: new Date(),
                Mode: 'Save'
            };
            $scope.SetVisitorName($scope.newVisitor.VisitorRelation);
        });
    };

    $scope.SetVisitorName = function () {
        var selectedStudent = null;
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudent = st;
            }
        });
        if ($scope.newVisitor.VisitorRelation == 1) {
            $scope.newVisitor.Name = selectedStudent.FatherName || '';
            $scope.newVisitor.Contact = selectedStudent.F_ContactNo || '';
            $scope.newVisitor.Address = selectedStudent.Address || '';
            $scope.newVisitor.Email = selectedStudent.F_Email || '';
        } else if ($scope.newVisitor.VisitorRelation == 2) {
            $scope.newVisitor.Name = selectedStudent.MotherName || '';
            $scope.newVisitor.Contact = selectedStudent.M_ContactNo || '';
            $scope.newVisitor.Address = selectedStudent.Address || '';
            $scope.newVisitor.Email = selectedStudent.M_Email || '';
        } else if ($scope.newVisitor.VisitorRelation == 3) {
            $scope.newVisitor.Name = '';
            $scope.newVisitor.Contact = '';
            $scope.newVisitor.Email = '';
        }
    };

    $scope.SaveUpdateVisitor = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var selectedStudent = null;
        angular.forEach($scope.AllStudentList, function (st) {
            if (st.IsSelected == true) {
                selectedStudent = st;
            }
        });
        if ($scope.newVisitor.Photo_TMP) {
            var photo = $scope.newVisitor.Photo_TMP;
            //$scope.newVisitor.Photo = $scope.newVisitor.PhotoData; 
        }
        if ($scope.newVisitor.AttachmentColl) {
            var filesColl = $scope.newVisitor.AttachmentColl;
        }
        if ($scope.newVisitor.InTime_TMP)
            $scope.newVisitor.InTime = $scope.newVisitor.InTime_TMP.toLocaleString();

        if ($scope.newVisitor.ValidityTime_TMP)
            $scope.newVisitor.ValidityTime = $scope.newVisitor.ValidityTime_TMP.toLocaleString();

        if ($scope.newVisitor.OutTime_TMP)
            $scope.newVisitor.OutTime = $scope.newVisitor.OutTime_TMP.toLocaleString();

        if ($scope.newVisitor.MeeTo == 1 && selectedStudent) {
            if (selectedStudent) {
                $scope.newVisitor.StudentId = selectedStudent.StudentId;
                $scope.newVisitor.OthersName = selectedStudent.RegNo + " : " + selectedStudent.Name + ": " + selectedStudent.ClassSection + " : " + selectedStudent.RollNo;
            }
        }

        $http({
            method: 'POST',
            url: base_url + "FrontDesk/Transaction/SaveVisitorBook",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                if (data.files) {
                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("file" + i, data.files[i].File);
                    }
                }

                if (data.stPhoto && data.stPhoto.length > 0)
                    formData.append("photo", data.stPhoto[0]);

                return formData;
            },
            data: { jsonData: $scope.newVisitor, files: filesColl, stPhoto: photo }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $('#VisitorsRecord').modal('hide');
                $scope.ClearVisitor();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    //************************* End Student Visitor *********************************

    $scope.printDiv = function (divName) {
        var printContents = document.getElementById(divName).innerHTML;
        var originalContents = document.body.innerHTML;

        document.body.innerHTML = printContents;

        window.print();

        document.body.innerHTML = originalContents;
    }

});
