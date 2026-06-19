
app.controller('EmployeeInfirmaryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'EmployeeInfirmary';

    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.SeverityList = [{ id: 1, text: 'High' }, { id: 2, text: 'Low' }, { id: 3, text: 'Medium' }];
        $scope.MonthList = GlobalServices.getMonthList();
        $scope.ObservedAtList = [{ id: 1, text: 'School' }, { id: 2, text: 'Home' }, { id: 3, text: 'Other' }];
        $scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
        $scope.currentPages = {
            ClinicalEvaluation: 1,
            PastHistory: 1,
            HealthImmunization: 1,
            HealthIssues: 1,
            Immunization: 1,
            GeneralCheckup: 1,
        };

        $scope.searchData = {
            ClinicalEvaluation: '',
            PastHistory: '',
            HealthImmunization: '',
            CurrentMedication: '',
            HealthIssues: '',
            Immunization: '',
            GeneralCheckup: ''
        };

        $scope.perPage = {
            ClinicalEvaluation: GlobalServices.getPerPageRow(),
            PastHistory: GlobalServices.getPerPageRow(),
            HealthImmunization: GlobalServices.getPerPageRow(),
            HealthIssues: GlobalServices.getPerPageRow(),
            Immunization: GlobalServices.getPerPageRow(),
            GeneralCheckup: GlobalServices.getPerPageRow()
        };


        $scope.DocumentTypeList = [];
        GlobalServices.getDocumentTypeList().then(function (res) {
            $scope.DocumentTypeList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.HealthIssuelist = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllHealthIssue",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.HealthIssuelist = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.ExaminerList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllExaminer",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ExaminerList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.MedicalProductlist = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllMedicalProduct",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.MedicalProductlist = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.TestNameList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllTestName",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.TestNameList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.VaccineList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllVaccine",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.VaccineList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.newDet = {
            SelectEmployee: $scope.EmployeeSearchOptions[0].value
        };

        $scope.newPastHistory = {
            PastHistoryId: null,
            Name: '',
            ObservedDate_TMP: new Date(),
            Details: '',
            Prescription: '',
            MedicineTaken: true,
            EmployeePastMedicineDetColl: [],
            EmployeePastMedicalDocumentsColl: [],
            Mode: 'Save'
        };
        $scope.newPastHistory.EmployeePastMedicineDetColl.push({});


        $scope.newHealthIssues = {
            PastHistoryId: null,
            MedicationDate: '',
            Description: '',
            AdmittedAt: false,
            MedicineGiven: false,
            EmployeeMedicineGivenDetColl: [],
            HealthIssueAttachmentColl: [],
            Mode: 'Save'
        };
        $scope.newHealthIssues.EmployeeMedicineGivenDetColl.push({});

        $scope.newImmunization = {
            TranId: null,
            HealthIssueId: null,
            VaccineId: null,
            VaccinationDate_TMP: new Date(),
            Remarks: '',
            VaccinatorId: null,
            EmployeeImmunizationAttachmentColl: [],
            Mode: 'Save'
        };

        $scope.NewGCheckup = {
            TranId: null,
            TestNameId: null,
            Value: 0,
            CheckupDate_TMP: new Date(),
            Remarks: '',
            Mode: 'Save'
        };
        $scope.TestNameList = $scope.TestNameList.map(function (item) {
            return {
                Name: item.Name,
                TestNameId: item.TestNameId,
                Value: 0
            };
        });

        $scope.newLabEvaluation = {
            TranId: null,
            TestMethod: '',
            TestNameId: null,
            EvaluateDate_TMP: new Date(),
            Remarks: '',
            HealthIssueId: null,
            Mode: 'Save'
        };



    };


    $scope.ClearPastHistory = function () {
        $scope.newPastHistory = {
            PastHistoryId: null,
            Name: '',
            ObservedDate_TMP: new Date(),
            Details: '',
            Prescription: '',
            MedicineTaken: true,
            EmployeePastMedicineDetColl: [],
            EmployeePastMedicalDocumentsColl: [],
            Mode: 'Save'
        };
        $scope.newPastHistory.EmployeePastMedicineDetColl.push({});

    };


    $scope.ClearHealthIssue = function () {
        $scope.newHealthIssues = {
            PastHistoryId: null,
            MedicationDate: '',
            Description: '',
            AdmittedAt: false,
            MedicineGiven: false,
            EmployeeMedicineGivenDetColl: [],
            HealthIssueAttachmentColl: [],
            Mode: 'Save'
        };
        $scope.newHealthIssues.EmployeeMedicineGivenDetColl.push({});
    }


    $scope.ClearHealthImmunization = function () {
        $scope.newImmunization = {
            TranId: null,
            HealthIssueId: null,
            VaccineId: null,
            VaccinationDate_TMP: new Date(),
            Remarks: '',
            VaccinatorId: null,
            EmployeeImmunizationAttachmentColl: [],
            Mode: 'Save'
        };
    }


    $scope.ClearGeneralCheckup = function () {
        $scope.NewGCheckup = {
            TranId: null,
            Value: 0,
            CheckupDate_TMP: new Date(),
            Remarks: '',

            Mode: 'Save'
        };
        $scope.TestNameList = $scope.TestNameList.map(function (item) {
            return {
                Name: item.Name,   // Keep the name
                TestNameId: item.TestNameId,
                Value: null           // Reset the value to 0 or whatever default is
            };
        });
    }

    $scope.ClearSCLEvaluation = function () {
        $scope.newLabEvaluation = {
            TranId: null,
            TestMethod: '',
            TestNameId: null,
            EvaluateDate_TMP: new Date(),
            Remarks: '',
            HealthIssueId: null,
            Mode: 'Save'
        };
    }

    function OnClickDefault() {
        document.getElementById('pastform').style.display = "none";
        document.getElementById('clinicalevaluationform').style.display = "none";
        document.getElementById('immunizationform').style.display = "none";
        document.getElementById('healthissueform').style.display = "none";
        document.getElementById('Bmiform').style.display = "none";
        document.getElementById('immunizationAtt').style.display = "none";

        document.getElementById('add-past-btn').onclick = function () {
            document.getElementById('pastlist').style.display = "none";
            document.getElementById('pastform').style.display = "block";
        }
        document.getElementById('back-past-list').onclick = function () {
            document.getElementById('pastform').style.display = "none";
            document.getElementById('pastlist').style.display = "block";
        }

        document.getElementById('add-clinical-btn').onclick = function () {
            document.getElementById('evaluationlist').style.display = "none";
            document.getElementById('clinicalevaluationform').style.display = "block";
        }
        document.getElementById('back-clinical-list').onclick = function () {
            document.getElementById('clinicalevaluationform').style.display = "none";
            document.getElementById('evaluationlist').style.display = "block";
        }

        document.getElementById('add-immunization-btn').onclick = function () {
            document.getElementById('immunizationlist').style.display = "none";
            document.getElementById('immunizationform').style.display = "block";
        }
        document.getElementById('back-immunization-list').onclick = function () {
            document.getElementById('immunizationform').style.display = "none";
            document.getElementById('immunizationlist').style.display = "block";
        }
        document.getElementById('back-immunization-listS').onclick = function () {
            document.getElementById('immunizationAtt').style.display = "none";
            document.getElementById('immunizationlist').style.display = "block";
        }

        document.getElementById('add-issue-btn').onclick = function () {
            document.getElementById('healthissuelist').style.display = "none";
            document.getElementById('healthissueform').style.display = "block";
        }
        document.getElementById('back-issue-list').onclick = function () {
            document.getElementById('healthissueform').style.display = "none";
            document.getElementById('healthissuelist').style.display = "block";
        }

        document.getElementById('add-bmi-btn').onclick = function () {
            document.getElementById('bmilist').style.display = "none";
            document.getElementById('Bmiform').style.display = "block";
        }
        document.getElementById('back-bmi-list').onclick = function () {
            document.getElementById('Bmiform').style.display = "none";
            document.getElementById('bmilist').style.display = "block";
        }

    }


    $scope.ChangeEmployee = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        if ($scope.newDet.EmployeeId && $scope.newDet.EmployeeId > 0) {
        var para = {
            EmployeeId: $scope.newDet.EmployeeId
        };
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getEmployeeDetForInfirmarybyId",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
             /*   $scope.newDet = res.data.Data;*/
                let empData = res.data.Data;
                // Preserve SelectStudent value
                empData.SelectEmployee = $scope.newDet.SelectEmployee;

                // Assign student data without losing existing properties
                $scope.newDet = Object.assign({}, $scope.newDet, empData);

                if ($scope.newDet.PhotoPath == '') {
                    $scope.newDet.PhotoPath = '/wwwroot/dynamic/images/avatar-img.jpg';
                }
                if ($scope.newDet.DobAD) {
                    let dob = new Date($scope.newDet.DobAD); // Convert DobAD to Date object
                    let today = new Date();
                    let ageYears = today.getFullYear() - dob.getFullYear();
                    let ageMonths = today.getMonth() - dob.getMonth();
                    let ageDays = today.getDate() - dob.getDate();
                    // Adjust for negative months or days
                    if (ageDays < 0) {
                        ageMonths--;
                        let previousMonth = new Date(today.getFullYear(), today.getMonth(), 0);
                        ageDays += previousMonth.getDate();
                    }
                    if (ageMonths < 0) {
                        ageYears--;
                        ageMonths += 12;
                    }
                    $scope.newDet.Ages = `${ageYears} Years, ${ageMonths} Months, ${ageDays} Days`;
                }

                $scope.GetMedicalHistoryById($scope.newDet.EmployeeId);
                $scope.GetMedicalIssuesById($scope.newDet.EmployeeId);
                $scope.GetEmployeeGeneralCheckup($scope.newDet.EmployeeId);
                $scope.GetEmployeeHealthImmunization($scope.newDet.EmployeeId);
                $scope.GetClinicalLabEvaluation($scope.newDet.EmployeeId);

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
        };
    };

    //*************************Past Medical History *********************************
    $scope.AddMedicineDetailsPastHistory = function (ind) {
        if ($scope.newPastHistory.EmployeePastMedicineDetColl) {
            if ($scope.newPastHistory.EmployeePastMedicineDetColl.length > ind + 1) {
                $scope.newPastHistory.EmployeePastMedicineDetColl.splice(ind + 1, 0, {
                    MedicineId: null
                })
            } else {
                $scope.newPastHistory.EmployeePastMedicineDetColl.push({
                    MedicineId: null
                })
            }
        }
    };

    $scope.DelMedicineDetailsPastHistory = function (ind) {
        if ($scope.newPastHistory.EmployeePastMedicineDetColl) {
            if ($scope.newPastHistory.EmployeePastMedicineDetColl.length > 1) {
                $scope.newPastHistory.EmployeePastMedicineDetColl.splice(ind, 1);
            }
        }
    };

    $scope.delAttachmentDoc = function (ind) {
        if ($scope.newPastHistory.EmployeePastMedicalDocumentsColl) {
            if ($scope.newPastHistory.EmployeePastMedicalDocumentsColl.length > 0) {
                $scope.newPastHistory.EmployeePastMedicalDocumentsColl.splice(ind, 1);
            }
        }
    };

    $scope.AddMoreFilesReceived = function (files, docType, des) {
        if (files && docType) {
            if (files != null && docType != null) {
                angular.forEach(files, function (file) {
                    $scope.newPastHistory.EmployeePastMedicalDocumentsColl.push({
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

    $scope.ShowMedicalDoc = function (item) {
        $scope.viewImg = {
            ContentPath: '',
            FileType: null
        };

        if (item.DocPath && item.DocPath.length > 0) {
            $scope.viewImg.ContentPath = item.DocPath;
            $scope.viewImg.FileType = 'pdf';  // Assuming DocPath is for PDFs
            document.getElementById('pdfMedDoc').src = item.DocPath;
            $('#MedicalDoc').modal('show');
        } else if (item.PhotoPath && item.PhotoPath.length > 0) {
            $scope.viewImg.ContentPath = item.PhotoPath;
            $scope.viewImg.FileType = 'image';  // Assuming PhotoPath is for images
            $('#MedicalDoc').modal('show');
        } else if (item.File) {
            var blob = new Blob([item.File], { type: item.File?.type });
            $scope.viewImg.ContentPath = URL.createObjectURL(blob);
            $scope.viewImg.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

            if ($scope.viewImg.FileType === 'pdf') {
                document.getElementById('pdfMedDoc').src = $scope.viewImg.ContentPath;
            }

            $('#MedicalDoc').modal('show');
        } else {
            Swal.fire('No Image Found');
        }
    };

    $scope.IsValidPastHistory = function () {
        if ($scope.newPastHistory.Prescription.isEmpty()) {
            Swal.fire('Please ! Enter PastHistory Prescription');
            return false;
        }
        return true;
    }

    $scope.SaveUpdatePastMedicalHistory = function () {
        if ($scope.IsValidPastHistory() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newPastHistory.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdatePastMedicalHistory();
                    }
                });
            } else
                $scope.CallSaveUpdatePastMedicalHistory();
        }
    };

    $scope.CallSaveUpdatePastMedicalHistory = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        /*var UserPhoto = $scope.newHealthExaminer.Photo_TMP;*/
        var filesColl = $scope.newPastHistory.EmployeePastMedicalDocumentsColl;

        $scope.newPastHistory.EmployeeId = $scope.newDet.EmployeeId;

        if ($scope.newPastHistory.ObservedDateDet) {
            $scope.newPastHistory.ObservedDate = $filter('date')(new Date($scope.newPastHistory.ObservedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newPastHistory.ObservedDate = $filter('date')(new Date(), 'yyyy-MM-dd');


        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveUpdateEmployeePMHistory",
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
            data: { jsonData: $scope.newPastHistory, files: filesColl }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearPastHistory();
                //$scope.GetAllPastHistoryList();
                $scope.GetMedicalHistoryById($scope.newDet.EmployeeId);
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetMedicalHistoryById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            EmployeeId: refData,
        };
        $scope.MedicalHistory = [];
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getEmployeeMedicalHistoryById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.MedicalHistory = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.GetPastHistoryById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            TranId: refData.TranId
        };
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getEmployeePastMedicalHistoryById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newPastHistory = res.data.Data;

                if (!$scope.newPastHistory.EmployeePastMedicineDetColl || $scope.newPastHistory.EmployeePastMedicineDetColl.length == 0) {
                    $scope.newPastHistory.EmployeePastMedicineDetColl = [];
                    $scope.newPastHistory.EmployeePastMedicineDetColl.push({});
                }

                if ($scope.newPastHistory.ObservedDate)
                    $scope.newPastHistory.ObservedDate_TMP = new Date($scope.newPastHistory.ObservedDate);

                $scope.newPastHistory.Mode = 'Modify';

                document.getElementById('pastlist').style.display = "none";
                document.getElementById('pastform').style.display = "block";

                if ($scope.newPastHistory.EmployeeId)
                    $scope.newDet.EmployeeId = $scope.newPastHistory.EmployeeId;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelPastHistoryById = function (refData) {
        Swal.fire({
            title: 'Do you want to delete the selected data?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                var para = {
                    TranId: refData.TranId
                };
                $http({
                    method: 'POST',
                    url: base_url + "Infirmary/Creation/DelEmpPastMedHty",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        //$scope.GetAllPastHistoryList();
                        $scope.GetMedicalHistoryById($scope.newDet.EmployeeId);
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });
    };


    //**********************************Health Issue************************

    $scope.AddMedicineDetails = function (ind) {
        if ($scope.newHealthIssues.EmployeeMedicineGivenDetColl) {
            if ($scope.newHealthIssues.EmployeeMedicineGivenDetColl.length > ind + 1) {
                $scope.newHealthIssues.EmployeeMedicineGivenDetColl.splice(ind + 1, 0, {
                    Name: ''
                })
            } else {
                $scope.newHealthIssues.EmployeeMedicineGivenDetColl.push({
                    Name: ''
                })
            }
        }
    };

    $scope.delMedicineDetails = function (ind) {
        if ($scope.newHealthIssues.EmployeeMedicineGivenDetColl) {
            if ($scope.newHealthIssues.EmployeeMedicineGivenDetColl.length > 1) {
                $scope.newHealthIssues.EmployeeMedicineGivenDetColl.splice(ind, 1);
            }
        }
    };

    $scope.delHIAttachmentDoc = function (ind) {
        if ($scope.newHealthIssues.HealthIssueAttachmentColl) {
            if ($scope.newHealthIssues.HealthIssueAttachmentColl.length > 0) {
                $scope.newHealthIssues.HealthIssueAttachmentColl.splice(ind, 1);
            }
        }
    };

    $scope.AddMoreHIssuesFiles = function (files1, docType1, des1) {
        if (files1 && docType1) {
            if (files1 != null && docType1 != null) {
                angular.forEach(files1, function (file) {
                    $scope.newHealthIssues.HealthIssueAttachmentColl.push({
                        DocumentTypeId: docType1.DocumentTypeId,
                        DocumentTypeName: docType1.Name,
                        File: file,
                        Name: file.name,
                        Type: file.type,
                        Size: file.size,
                        Description: des1,
                        Path: null
                    });
                })

                $scope.docType1 = null;
                $scope.attachFile1 = null;
                $scope.docDescription1 = '';

                $('#flMoreFiles1').val('');
            }
        }
    };

    $scope.IsValidHealthISsue = function () {
        if ($scope.newHealthIssues.Prescription.isEmpty()) {
            Swal.fire('Please ! Enter  Prescription');
            return false;
        }

        return true;
    }

    $scope.SaveUpdateHealthIssues = function () {
        if ($scope.IsValidHealthISsue() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newHealthIssues.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateHealthIssues();
                    }
                });
            } else
                $scope.CallSaveUpdateHealthIssues();
        }
    };

    $scope.CallSaveUpdateHealthIssues = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        /*var UserPhoto = $scope.newHealthExaminer.Photo_TMP;*/
        var filesColl = $scope.newHealthIssues.HealthIssueAttachmentColl;

        $scope.newHealthIssues.EmployeeId = $scope.newDet.EmployeeId;

        if ($scope.newHealthIssues.ObservedDateDet) {
            $scope.newHealthIssues.ObservedDate = $filter('date')(new Date($scope.newHealthIssues.ObservedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newHealthIssues.ObservedDate = $filter('date')(new Date(), 'yyyy-MM-dd');

        if ($scope.newHealthIssues.AdmittedDateDet) {
            $scope.newHealthIssues.AdmittedDate = $filter('date')(new Date($scope.newHealthIssues.AdmittedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newHealthIssues.AdmittedDate = $filter('date')(new Date(), 'yyyy-MM-dd');

        if ($scope.newHealthIssues.DischargedDateDet) {
            $scope.newHealthIssues.DischargedDate = $filter('date')(new Date($scope.newHealthIssues.DischargedDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newHealthIssues.DischargedDate = $filter('date')(new Date(), 'yyyy-MM-dd');

        if ($scope.newHealthIssues.ObservedTime_TMP)
            $scope.newHealthIssues.ObservedTime = $scope.newHealthIssues.ObservedTime_TMP.toLocaleString();

        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveUpdateEmployeeHealthIssue",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                //if (data.UsPhoto && data.UsPhoto.length > 0)
                //	formData.append("UserPhoto", data.UsPhoto[0]);
                if (data.files) {
                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("file" + i, data.files[i].File);
                    }
                }
                return formData;
            },
            data: { jsonData: $scope.newHealthIssues, files: filesColl }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearHealthIssue();
                $scope.GetMedicalIssuesById($scope.newDet.EmployeeId);
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetMedicalIssuesById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            EmployeeId: refData,
        };
        $scope.MedicalIssues = [];
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getEmployeeMedicalIssuesById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.MedicalIssues = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.ShowAttImg = function (item) {
        $scope.viewImg1 = {
            ContentPath: '',
            FileType: null
        };

        if (item.DocPath && item.DocPath.length > 0) {
            $scope.viewImg1.ContentPath = item.DocPath;
            $scope.viewImg1.FileType = 'pdf';  // Assuming DocPath is for PDFs
            document.getElementById('pdfViewer1').src = item.DocPath;
            $('#PersonalImg1').modal('show');
        } else if (item.PhotoPath && item.PhotoPath.length > 0) {
            $scope.viewImg1.ContentPath = item.PhotoPath;
            $scope.viewImg1.FileType = 'image';  // Assuming PhotoPath is for images
            $('#PersonalImg1').modal('show');
        } else if (item.File) {
            var blob = new Blob([item.File], { type: item.File?.type });
            $scope.viewImg1.ContentPath = URL.createObjectURL(blob);
            $scope.viewImg1.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

            if ($scope.viewImg1.FileType === 'pdf') {
                document.getElementById('pdfViewer1').src = $scope.viewImg1.ContentPath;
            }

            $('#PersonalImg1').modal('show');
        } else {
            Swal.fire('No Image Found');
        }
    };

    $scope.ShowHealthIssDoc = function (item) {
        $scope.viewImg = {
            ContentPath: '',
            FileType: null
        };

        if (item.DocPath && item.DocPath.length > 0) {
            $scope.viewImg.ContentPath = item.DocPath;
            $scope.viewImg.FileType = 'pdf';  // Assuming DocPath is for PDFs
            document.getElementById('pdfHealthIssDoc').src = item.DocPath;
            $('#HealthIssDoc').modal('show');
        } else if (item.PhotoPath && item.PhotoPath.length > 0) {
            $scope.viewImg.ContentPath = item.PhotoPath;
            $scope.viewImg.FileType = 'image';  // Assuming PhotoPath is for images
            $('#HealthIssDoc').modal('show');
        } else if (item.File) {
            var blob = new Blob([item.File], { type: item.File?.type });
            $scope.viewImg.ContentPath = URL.createObjectURL(blob);
            $scope.viewImg.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

            if ($scope.viewImg.FileType === 'pdf') {
                document.getElementById('pdfHealthIssDoc').src = $scope.viewImg.ContentPath;
            }
            $('#HealthIssDoc').modal('show');
        } else {
            Swal.fire('No Image Found');
        }
    };

    $scope.GetHealthIssueById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            TranId: refData.TranId
        };
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/GetEmployeeHealthIssueById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newHealthIssues = res.data.Data;

                if (!$scope.newHealthIssues.EmployeeMedicineGivenDetColl || $scope.newHealthIssues.EmployeeMedicineGivenDetColl.length == 0) {
                    $scope.newHealthIssues.EmployeeMedicineGivenDetColl = [];
                    $scope.newHealthIssues.EmployeeMedicineGivenDetColl.push({});
                }

                if ($scope.newHealthIssues.ObservedDate)
                    $scope.newHealthIssues.ObservedDate_TMP = new Date($scope.newHealthIssues.ObservedDate);

                if ($scope.newHealthIssues.AdmittedDate)
                    $scope.newHealthIssues.AdmittedDate_TMP = new Date($scope.newHealthIssues.AdmittedDate);

                if ($scope.newHealthIssues.DischargedDate)
                    $scope.newHealthIssues.DischargedDate_TMP = new Date($scope.newHealthIssues.DischargedDate);


                if ($scope.newHealthIssues.ObservedTime)
                    $scope.newHealthIssues.ObservedTime_TMP = new Date($scope.newHealthIssues.ObservedTime);

                $scope.newHealthIssues.Mode = 'Modify';

                document.getElementById('healthissuelist').style.display = "none";
                document.getElementById('healthissueform').style.display = "block";

                if ($scope.newHealthIssues.EmployeeId)
                    $scope.newDet.EmployeeId = $scope.newHealthIssues.EmployeeId;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelHealthIssueById = function (refData) {
        Swal.fire({
            title: 'Do you want to delete the selected data?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                var para = {
                    TranId: refData.TranId
                };
                $http({
                    method: 'POST',
                    url: base_url + "Infirmary/Creation/DelEmpHealthIssue",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        //$scope.GetAllPastHistoryList();
                        $scope.GetMedicalIssuesById($scope.newDet.EmployeeId);
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });
    };


    //***********************General Checkup Starts***********************************
    $scope.SaveGeneralCheckup = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var dtColl = [];
        if ($scope.NewGCheckup.CheckupDateDet) {
            $scope.NewGCheckup.CheckupDate = $filter('date')(new Date($scope.NewGCheckup.CheckupDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.NewGCheckup.CheckupDate = $filter('date')(new Date(), 'yyyy-MM-dd');

        $scope.TestNameList.forEach(function (ph) {
            if (ph.Value > 0 || ph.Value < 0) {
                dtColl.push({
                    EmployeeId: $scope.newDet.EmployeeId,
                    TestNameId: ph.TestNameId,
                    Value: ph.Value,
                    Remarks: $scope.NewGCheckup.Remarks,
                    CheckupDate: $scope.NewGCheckup.CheckupDate
                });
            }
        })
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveEmployeeGeneralCheckup",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: dtColl }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearGeneralCheckup();
                $scope.GetEmployeeGeneralCheckup($scope.newDet.EmployeeId);
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetEmployeeGeneralCheckup = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            EmployeeId: $scope.newDet.EmployeeId
        };
        $scope.GeneralCheckupList = [];
        $scope.dtColl = [];
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/GetEmployeeGeneralCheckup",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.GeneralCheckupList = res.data.Data;
                const testName = {};
                $scope.GeneralCheckupList.forEach(item => {
                    if (!testName[item.TestNameId]) {
                        testName[item.TestNameId] = item.TestName;
                    }
                });
                $scope.groupedData = Object.values($scope.GeneralCheckupList.reduce((acc, item) => {
                    if (!acc[item.CheckupDate]) {
                        acc[item.CheckupDate] = {
                            TranId: item.TranId,
                            CheckupDate: item.CheckupDate,
                            CheckupDateBS: item.CheckupDateBS,
                            Remarks: item.Remarks,
                            tests: {}
                        };
                    }
                    acc[item.CheckupDate].tests[item.TestNameId] = item.Value;
                    return acc;
                }, {}));

                $scope.TestNameLists = Object.keys(testName).map(key => ({
                    TestNameId: key,
                    Name: testName[key]
                }));

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    //***********************Health Immunization Starts***********************************

    $scope.delSIAttachmentDoc = function (ind) {
        if ($scope.newImmunization.EmployeeImmunizationAttachmentColl) {
            if ($scope.newImmunization.EmployeeImmunizationAttachmentColl.length > 0) {
                $scope.newImmunization.EmployeeImmunizationAttachmentColl.splice(ind, 1);
            }
        }
    };

    $scope.AddMoreImmunizationFiles = function (files2, docType2, des2) {
        if (files2 && docType2) {
            if (files2 != null && docType2 != null) {
                angular.forEach(files2, function (file) {
                    $scope.newImmunization.EmployeeImmunizationAttachmentColl.push({
                        DocumentTypeId: docType2.DocumentTypeId,
                        DocumentTypeName: docType2.Name,
                        File: file,
                        Name: file.name,
                        Type: file.type,
                        Size: file.size,
                        Description: des2,
                        Path: null
                    });
                })

                $scope.docType2 = null;
                $scope.attachFile2 = null;
                $scope.docDescription2 = '';

                $('#flMoreFiles2').val('');
            }
        }
    };

    $scope.IsValidEmployeeImmunization = function () {
        //if ($scope.newImmunization.Prescription.isEmpty()) {
        //	Swal.fire('Please ! Enter  Prescription');
        //	return false;
        //}

        return true;
    }

    $scope.SaveUpdateEmployeeImmunizations = function () {
        if ($scope.IsValidEmployeeImmunization() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newImmunization.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateEmployeeImmunizations();
                    }
                });
            } else
                $scope.CallSaveUpdateEmployeeImmunizations();
        }
    };

    $scope.CallSaveUpdateEmployeeImmunizations = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var filesColl = $scope.newImmunization.EmployeeImmunizationAttachmentColl;

        $scope.newImmunization.EmployeeId = $scope.newDet.EmployeeId;

        if ($scope.newImmunization.VaccinationDateDet) {
            $scope.newImmunization.VaccinationDate = $filter('date')(new Date($scope.newImmunization.VaccinationDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newImmunization.VaccinationDate = $filter('date')(new Date(), 'yyyy-MM-dd');

        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveUpdateEmployeeImmunization",
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
            data: { jsonData: $scope.newImmunization, files: filesColl }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearHealthImmunization();
                $scope.GetEmployeeHealthImmunization();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetAtt = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            TranId: refData.TranId
        };
        $scope.HealthImmunization = [];
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/GetEmployeeHealthImmunizationDoc",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.HealthImmunization = res.data.Data;
                document.getElementById('immunizationAtt').style.display = "block";
                document.getElementById('immunizationlist').style.display = "none";
                //$('#exampleModalToggle1').modal('show');
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.GetEmployeeHealthImmunization = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            EmployeeId: $scope.newDet.EmployeeId
        };
        $scope.HealthImmunizationList = [];
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/GetEmployeeHealthImmunization",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.HealthImmunizationList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.GetImmunizationById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            TranId: refData.TranId
        };
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getEmployeeImmunizationId",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newImmunization = res.data.Data;
                if ($scope.newImmunization.VaccinationDate)
                    $scope.newImmunization.VaccinationDate_TMP = new Date($scope.newImmunization.VaccinationDate);

                $scope.newImmunization.Mode = 'Modify';
                document.getElementById('immunizationlist').style.display = "none";
                document.getElementById('immunizationform').style.display = "block";

                if ($scope.newImmunization.EmployeeId)
                    $scope.newDet.EmployeeId = $scope.newImmunization.EmployeeId;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.ShowPersonalImg2 = function (item) {
        $scope.viewImg2 = {
            ContentPath: '',
            FileType: null
        };

        if (item.DocPath && item.DocPath.length > 0) {
            $scope.viewImg2.ContentPath = item.DocPath;
            $scope.viewImg2.FileType = 'pdf';  // Assuming DocPath is for PDFs
            document.getElementById('pdfViewer2').src = item.DocPath;
            $('#ModalToggleLabel2').modal('show');
        } else if (item.PhotoPath && item.PhotoPath.length > 0) {
            $scope.viewImg2.ContentPath = item.PhotoPath;
            $scope.viewImg2.FileType = 'image';  // Assuming PhotoPath is for images
            $('#ModalToggleLabel2').modal('show');
        } else if (item.File) {
            var blob = new Blob([item.File], { type: item.File?.type });
            $scope.viewImg2.ContentPath = URL.createObjectURL(blob);
            $scope.viewImg2.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

            if ($scope.viewImg2.FileType === 'pdf') {
                document.getElementById('pdfViewer2').src = $scope.viewImg2.ContentPath;
            }

            $('#ModalToggleLabel2').modal('show');
        } else {
            Swal.fire('No Image Found');
        }
    };

    $scope.ShowAttach = function (item) {
        $scope.viewImg1 = {
            ContentPath: '',
            FileType: null
        };

        if (item.DocPath && item.DocPath.length > 0) {
            $scope.viewImg1.ContentPath = item.DocPath;
            $scope.viewImg1.FileType = 'pdf';  // Assuming DocPath is for PDFs
            document.getElementById('pdfViewer3').src = item.DocPath;
            $('#exampleModalToggle2').modal('show');
        } else if (item.PhotoPath && item.PhotoPath.length > 0) {
            $scope.viewImg1.ContentPath = item.PhotoPath;
            $scope.viewImg1.FileType = 'image';  // Assuming PhotoPath is for images
            $('#exampleModalToggle2').modal('show');
        } else if (item.File) {
            var blob = new Blob([item.File], { type: item.File?.type });
            $scope.viewImg1.ContentPath = URL.createObjectURL(blob);
            $scope.viewImg1.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

            if ($scope.viewImg1.FileType === 'pdf') {
                document.getElementById('pdfViewer2').src = $scope.viewImg1.ContentPath;
            }

            $('#exampleModalToggle2').modal('show');
        } else {
            Swal.fire('No Image Found');
        }
    };

    $scope.DelImmunizationById = function (refData) {
        Swal.fire({
            title: 'Do you want to delete the selected data?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();
                var para = {
                    TranId: refData.TranId
                };
                $http({
                    method: 'POST',
                    url: base_url + "Infirmary/Creation/DelEmployeeHealthImmunzation",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.GetEmployeeHealthImmunization();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });
    };

    //*****************************Clinical Lab Evaluation Tab Starts*******************************************
    $scope.GetTestNameById = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            TestNameId: $scope.newLabEvaluation.TestNameId
        };
        $scope.newLabEvaluation.LabValueList = [];
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/GetLabValueById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newLabEvaluation.LabValueList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.IsValidSCLEvaluation = function () {
        if ($scope.newLabEvaluation.TestMethod.isEmpty()) {
            Swal.fire('Please ! Enter Test Method');
            return false;
        }
        return true;
    }

    $scope.SaveUpdateSCLEvaluation = function () {
        if ($scope.IsValidSCLEvaluation() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newLabEvaluation.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateSCLEvaluation();
                    }
                });
            } else
                $scope.CallSaveUpdateSCLEvaluation();
        }
    };

    $scope.CallSaveUpdateSCLEvaluation = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        $scope.newLabEvaluation.EmployeeId = $scope.newDet.EmployeeId;

        if ($scope.newLabEvaluation.EvaluateDateDet) {
            $scope.newLabEvaluation.EvaluateDate = $filter('date')(new Date($scope.newLabEvaluation.EvaluateDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newLabEvaluation.EvaluateDate = $filter('date')(new Date(), 'yyyy-MM-dd');

        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveEmpCliLabEvaluation",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: { jsonData: $scope.newLabEvaluation }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearSCLEvaluation();
                $scope.GetAllEmployeeCLEvaluation();
                $scope.GetClinicalLabEvaluation($scope.newDet.EmployeeId);
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetClinicalLabEvaluation = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            EmployeeId: $scope.newDet.EmployeeId
        };
        $scope.ClinicalLabEvaluationList = [];
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/GetEmpLabEval",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ClinicalLabEvaluationList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };

    $scope.GetEmployeeLabEvaluationById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            TranId: refData.TranId
        };
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getEmployeeClinicalLabEvaluationById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newLabEvaluation = res.data.Data;

                if ($scope.newLabEvaluation.EvaluateDate)
                    $scope.newLabEvaluation.EvaluateDate_TMP = new Date($scope.newLabEvaluation.EvaluateDate);

                $scope.newLabEvaluation.Mode = 'Save';
                document.getElementById('evaluationlist').style.display = "none";
                document.getElementById('clinicalevaluationform').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelEmployeeEvaluationById = function (refData, ind) {
        Swal.fire({
            title: 'Are you sure you want to delete ' + refData.Name + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            //message: 'Are you sure to delete selected Branch :-' + beData.Name,
        }).then((result) => {
            if (result.isConfirmed) {
                var para = { TranId: refData.TranId };
                $http({
                    method: 'POST',
                    url: base_url + "Infirmary/Creation/DeleteSLEvaluationById",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingStatus = "stop";

                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess == true) {
                        $scope.GetAllEmployeeCLEvaluation();
                        $scope.GetClinicalLabEvaluation();
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }

        });
    }


    $scope.pageChangeHandler = function (num) {
        console.log('page changed to ' + num);
    };

    
});

