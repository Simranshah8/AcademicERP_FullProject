app.controller('AcademicController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Contact';

    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();

        $scope.perPage = {
            MarkEntry: GlobalServices.getPerPageRow(),
        };
        $scope.currentPages = {
            MarkEntry: 1
        };
        $scope.searchData = {
            MarkEntry: ""
        };

        $scope.ResultList = [{ id: 1, text: "Pass" }, { id: 2, text: "Fail" }];

        $scope.SubjectList = [];
        GlobalServices.getSubjectList().then(function (res) {
            $scope.SubjectList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.ClassSection = {};
        GlobalServices.getClassSectionList().then(function (res) {
            $scope.ClassSection = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.EntranceSetup = [];
        $http({
            method: 'POST',
            url: base_url + "AdmissionManagement/Creation/GetEntranceSetup",
            dataType: "json"
        }).then(function (res1) {
            if (res1.data.IsSuccess && res1.data.Data) {
                $scope.EntranceSetup = res1.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.newFilter = {
            ClassId: null,
        }

        $scope.newDet = {
            TranId: null,
            EnquiryNo: '',
            Name: '',
            AppliedClass: '',
            ExamName: '',
            FullMarks: null,
            PassMarks: null,
            ObtMarks: 0,
            Result: null,
            Remarks: '',
            ExamDatetime: null,
            SubjectIncluded: '',
            Mode: 'Save'
        };
        //$scope.GetEntranceResult();
        $scope.GetEntranceExamResult();

        $scope.newMarkSetup = {
            ClassId: null,
            SumFullPass: false,
            FullMark: 0,
            PassMark: 0,
            TotallFullMark: 0,
            TotallPassMark: 0
        };

    }



    //$scope.ClearMarkEntry = function () {
    //    $scope.newDet = {
    //        ObtMarks: 0,
    //        Result: null,
    //        Remarks: '',
    //    };
    //}
    $scope.ClearMarkEntry = function () {
        if (!$scope.MarkEntryList || !$scope.MarkEntryList.length) return;
        angular.forEach($scope.MarkEntryList, function (cs) {
            if (cs.ObtainedMarks) {
                angular.forEach(cs.ObtainedMarks, function (value, key) {
                    cs.ObtainedMarks[key] = null;
                });
            }
            cs.TotalObtMarks = null;
            cs.Result = null;
            cs.Remarks = '';
        });
    };

    $scope.ClearMarkSetup = function () {
        $scope.newMarkSetup = {
            ClassId: null,
            SumFullPass: false,
            FullMark: 0,
            PassMark: 0,
            TotallFullMark: 0,
            TotallPassMark: 0
        };
    }

    $scope.checkEntranceSetup = function () {
        if ($scope.EntranceSetup.ForClassWise === false) {
            Swal.fire("Entrance Exam Setup needs to be saved class-wise-subject");
            $scope.newMarkSetup.ClassId = null;
            $scope.newMarkSetup.SumFullPass = false;
            $scope.newMarkSetup.TotallFullMarks = 0;
            $scope.newMarkSetup.TotallPassMarks = 0;
            $scope.newMarkSetup.ClassIdColl = "";
            return true;  
        }       

        return false;
    };
    $scope.GetMarkSetup = function () {
        if ($scope.checkEntranceSetup()) {
            return; 
        }
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.MarkSetupList = [];
        var classId = null;

        if ($scope.newMarkSetup.ClassId && $scope.newMarkSetup.ClassId !== 0) {
            classId = $scope.newMarkSetup.ClassId;
        }
        if ($scope.newFilter.ClassId && $scope.newFilter.ClassId !== 0) {
            classId = $scope.newFilter.ClassId === 0 ? null : $scope.newFilter.ClassId ?? null;
        }

        var para = {
            ClassId: classId,
        };

        $http({
            method: 'POST',
            url: base_url + "AdmissionManagement/Creation/GetMarkSetup",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.MarkSetupList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }
    $scope.onSumFullPassChange = function () {
        if ($scope.newMarkSetup.SumFullPass) {
            angular.forEach($scope.MarkSetupList, function (cs) {
                cs.FullMark = 0;
                cs.PassMark = 0;
            });
            if ($scope.MarkEntryList.length > 0) {
                $scope.newMarkSetup.TotallFullMarks = Number($scope.MarkEntryList[0].FullMarks) || 0;
                $scope.newMarkSetup.TotallPassMarks = Number($scope.MarkEntryList[0].PassMarks) || 0;
            }
        } else {
            $scope.updateMarks();
        }
    };
    $scope.updateMarks = function () {
        if ($scope.checkEntranceSetup()) {
            return;
        }
        if ($scope.MarkEntryList.length > 0) {
            let sumFull = 0;
            let sumPass = 0;
            angular.forEach($scope.MarkSetupList, function (cs) {
                sumFull += Number(cs.FullMark) || 0;
                sumPass += Number(cs.PassMark) || 0;
            });
            if ($scope.newMarkSetup.SumFullPass) {
                $scope.newMarkSetup.TotallFullMarks = Number($scope.MarkEntryList[0].FullMarks) || 0;
                $scope.newMarkSetup.TotallPassMarks = Number($scope.MarkEntryList[0].PassMarks) || 0;
                if (sumFull > $scope.newMarkSetup.TotallFullMarks) {
                    Swal.fire(`Sum of Full Marks (${sumFull}) cannot exceed Total Full Marks (${$scope.newMarkSetup.TotallFullMarks}). Resetting Full Marks to 0.`);
                    angular.forEach($scope.MarkSetupList, function (cs) {
                        cs.FullMark = 0;
                    });
                }
                if (sumPass > $scope.newMarkSetup.TotallPassMarks) {
                    Swal.fire(`Sum of Pass Marks (${sumPass}) cannot exceed Total Pass Marks (${$scope.newMarkSetup.TotallPassMarks}). Resetting Pass Marks to 0.`);
                    angular.forEach($scope.MarkSetupList, function (cs) {
                        cs.PassMark = 0;
                    });
                }
            } else {
                // Sum marks and assign to total
                $scope.newMarkSetup.TotallFullMarks = sumFull;
                $scope.newMarkSetup.TotallPassMarks = sumPass;
            }
        }
    };
    $scope.SaveMarkSetup = function () {
        if ($scope.checkEntranceSetup()) {
            return;
        }
        //------------mandatory validations --------------
        if (!$scope.newMarkSetup.ClassId || $scope.newMarkSetup.ClassId == 0) {
            Swal.fire("Please select Class");
            return true;
        }

        if (!$scope.newMarkSetup.TotallFullMarks || $scope.newMarkSetup.TotallFullMarks <= 0) {
            Swal.fire("Please Enter Valid Full Mark");
            return true;
        }

        if (!$scope.newMarkSetup.TotallPassMarks || $scope.newMarkSetup.TotallPassMarks <= 0) {
            Swal.fire("Please Enter Valid Pass Mark");
            return true;
        }

        if ($scope.newMarkSetup.TotallPassMarks > $scope.newMarkSetup.TotallFullMarks) {
            Swal.fire("Pass Mark cannot be greater than Full Mark");
            return true;
        }
        $scope.loadingstatus = "running";
        showPleaseWait();
        var classId = $scope.newMarkSetup.ClassId;
        var subjectMap = {};
        angular.forEach($scope.MarkSetupList, function (cs) {
            if (!subjectMap[cs.SubjectId]) {
                subjectMap[cs.SubjectId] = {
                    ClassId: classId,
                    SubjectId: cs.SubjectId,
                    FullMark: cs.FullMark || 0,
                    PassMark: cs.PassMark || 0
                };
            } else {
                subjectMap[cs.SubjectId].FullMark += cs.FullMark || 0;
                subjectMap[cs.SubjectId].PassMark += cs.PassMark || 0;
            }
        });
        var dataToSend = Object.values(subjectMap); 
        $http({
            method: 'POST',
            url: base_url + "AdmissionManagement/Creation/SaveMarkSetup",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: {
                jsonData: dataToSend
            }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            if (res.data.IsSuccess) {
                Swal.fire(res.data.ResponseMSG);
                $scope.ClearMarkSetup();
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function () {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire("Server error occurred while saving entrance marks.");
        });
    };

    $scope.CallMarkSetupEntry = function () {
        var classId = null;
        if ($scope.newMarkSetup.ClassId && $scope.newMarkSetup.ClassId !== 0) {
            classId = $scope.newMarkSetup.ClassId;
        }
        if ($scope.newFilter.ClassId && $scope.newFilter.ClassId !== 0) {
            classId = $scope.newFilter.ClassId;
        }
        var para = {
            ClassId: classId,
        };
        $scope.GetMarkSetup(para);
        $scope.GetEntranceResult(para);
    }

    $scope.CallMarkEntry = function () {
        if ($scope.EntranceSetup.ForClassWise === false) {
            Swal.fire("Entrance Exam Setup needs to be saved class-wise-subject");
            $scope.newFilter.ClassId = null;
            return true;
        }
        return false;
    }

    $scope.GetEntranceResult = function () {
        if ($scope.CallMarkEntry()) {
            return;
        }
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.MarkEntryList = [];

        var classId = null;
        if ($scope.newMarkSetup.ClassId && $scope.newMarkSetup.ClassId !== 0) {
            classId = $scope.newMarkSetup.ClassId;
        }
        if ($scope.newFilter.ClassId && $scope.newFilter.ClassId !== 0) {
            classId = $scope.newFilter.ClassId;
        }

        var para = {
            ClassId: classId
        };

        $http({
            method: 'POST',
            url: base_url + "AdmissionManagement/Creation/GetEntranceResult",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.IsSuccess && res.data.Data) {
                var rawData = res.data.Data;
                var groupedData = {};
                angular.forEach(rawData, function (item) {
                    var enquiryNo = item.EnquiryId || item.RegId;

                    if (!groupedData[enquiryNo]) {
                        groupedData[enquiryNo] = {
                            EnquiryId: item.EnquiryId,
                            RegId: item.RegId,
                            SymbolNo: item.SymbolNo,
                            Name: item.Name,
                            ClassName: item.ClassName,
                            ExamName: item.ExamName,
                            FullMarks: item.FullMarks,
                            PassMarks: item.PassMarks,
                            ExamDate: item.ExamDatetime,
                            Result: item.Result,
                            Remarks: item.Remarks,
                            ObtainedMarks: {},
                            TotalObtMarks: 0
                        };
                    }
                    // Store subject-wise mark
                    if (item.Subject) {
                        groupedData[enquiryNo].ObtainedMarks[item.Subject] =
                            parseFloat(item.ObtMarks) || 0;
                    }

                    // Update total obtained marks
                    groupedData[enquiryNo].TotalObtMarks += parseFloat(item.ObtMarks) || 0;
                 });
                $scope.MarkEntryList = Object.values(groupedData);
                $scope.gridOptions2.data = $scope.MarkEntryList;
            } else {
                Swal.fire(res.data.ResponseMSG || "No data found.");
            }
        }, function (reason) {
            Swal.fire('Failed: ' + reason);
        });
    };

    $scope.CalculateTotalAndResult = function (cs) {
        let total = 0;
        let isFailed = false;

        angular.forEach($scope.MarkSetupList, function (ms) {
            let subjectId = ms.SubjectId;
            let obtained = parseFloat(cs.ObtainedMarks[subjectId]) || 0;
            let full = parseFloat(ms.FullMark);
            let pass = parseFloat(ms.PassMark);

            if (obtained > full) {
                Swal.fire("Obtained Marks cannot exceed Full Marks!");
                cs.ObtainedMarks[subjectId] = 0;
                obtained = 0;
            }

            if (obtained < pass) {
                isFailed = true;
            }

            total += obtained;
        });

        cs.TotalObtMarks = total;
        cs.Result = isFailed ? 2 : 1; // 1 = Pass, 2 = Fail
    };


    $scope.SaveEntranceMarkEntry = function () {        
        $scope.loadingstatus = "running";
        showPleaseWait();

        var dataToSend = [];
        var hasError = false;
        angular.forEach($scope.MarkEntryList, function (cs, rowIndex ) {
            angular.forEach($scope.MarkSetupList, function (ms) {
                var obtainedMark = cs.ObtainedMarks ? cs.ObtainedMarks[ms.SubjectId] : null;
                if (obtainedMark < 0) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    hasError = true;

                    Swal.fire({
                        icon: 'warning',
                        title: 'Invalid Marks',
                        html:
                            'Negative marks found at S.No. ' + (rowIndex + 1) + '<br>' +
                            'Name: <b>' + cs.Name + '</b><br>' +
                            'Subject: <b>' + ms.SubjectName + '</b>'
                    });
                    return false;
                }
                var data = {
                    EnquiryNo: cs.EnquiryId || cs.RegId,
                    Name: cs.Name,
                    AppliedClass: cs.ClassName,
                    ExamName: cs.ExamName,
                    FullMarks: ms.FullMark,
                    PassMarks: ms.PassMark,
                    ObtMarks: obtainedMark,
                    Result: cs.Result,
                    Remarks: cs.Remarks,
                    ExamDatetime: cs.ExamDate,
                    SubjectIncluded: ms.SubjectId
                };
                dataToSend.push(data);
            });
        });
        if (hasError) return;

        $http({
            method: 'POST',
            url: base_url + "AdmissionManagement/Creation/SaveEntranceMarkEntry",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("userId", $scope.UserId); // Make sure $scope.UserId is set
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: {
                jsonData: dataToSend
            }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();

            if (res.data.IsSuccess) {
                Swal.fire(res.data.ResponseMSG || "Entrance Marks saved successfully!");
                $scope.GetEntranceExamResult();
                $scope.GetEntranceResult();
            } else {
                Swal.fire(res.data.ResponseMSG || "Failed to save entrance marks.");
            }
        }, function () {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire("Server error occurred while saving entrance marks.");
        });
    };


    $scope.gridOptions2 = {
        enableColumnMenus: false,
        showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,
        rowHeight: 31,
        columnDefs: [
            { name: "SNo", displayName: "S.No.", minWidth: 90, headerCellClass: 'headerAligment', cellTemplate: '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>' },
            {
                name: "Enquiry/Reg No.",
                width: 150, field: "EnquiryId",
                cellTemplate: '<div class="ui-grid-cell-contents">' +
                    '{{ row.entity.EnquiryId || "" }} {{ row.entity.RegId || "" }}' +
                    '</div>'
            },
            { name: "Name", field: "Name", width: 180 },
            { name: "Gender", field: "Gender", width: 100 },
            { name: "Applied Class", field: "ClassName", width: 140 },
            { name: "Exam Name", field: "ExamName", width: 150 },
            { name: "Result Date Time", field: "ResultDateMiti", width: 170 },
            //{ name: "FM", field: "FullMarks", width: 80, filter: 'agNumberColumnFilter'},
            //{ name: "PM", field: "PassMarks", width: 80, filter: 'agNumberColumnFilter' },
            { name: "Obt. Marks", field: "TotalObtMarks", width: 120, filter: 'agNumberColumnFilter' },
            //{ name: "Obt. Per (%)", field: "TotalObtMarks", width: 120 },
            { name: "Result", field: "ResultText", width: 100 },
            { name: "Remarks", field: "Remarks", width: 180 },
            { name: "DOB", field: "DOB_BS", width: 130 },
            { name: "Contact No", field: "ContactNo", width: 150 },
            { name: "Email", field: "Email", width: 200 },
            { name: "Address", field: "Address", width: 200 }
        ],
        data: $scope.EntranceExamResultList,
        exporterCsvFilename: 'EntranceExamResult.csv',
        exporterPdfDefaultStyle: { fontSize: 9 },
        exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
        exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
        exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
        exporterPdfFooter: function (currentPage, pageCount) {
            return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
        },
        exporterPdfCustomFormatter: function (docDefinition) {
            docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
            docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
            return docDefinition;
        },
        exporterPdfOrientation: 'portrait',
        exporterPdfPageSize: 'LETTER',
        exporterPdfMaxGridWidth: 500,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        exporterExcelFilename: 'enqSummary.xlsx',
        exporterExcelSheetName: 'enqSummary',
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.GetEntranceExamResult = function () {
        if ($scope.CallMarkEntry && $scope.CallMarkEntry()) {
            return;
        }

        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.EntranceExamResultList = [];

        $http({
            method: 'POST',
            url: base_url + "AdmissionManagement/Creation/GetEntranceResult",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.IsSuccess && res.data.Data) {
                var rawData = res.data.Data;
                var groupedData = {};

                angular.forEach(rawData, function (item) {
                    var enquiryNo = item.EnquiryId || item.RegId;

                    if (!groupedData[enquiryNo]) {
                        groupedData[enquiryNo] = {
                            EnquiryId: item.EnquiryId,
                            RegId: item.RegId,
                            SymbolNo: item.SymbolNo,
                            Name: item.Name,
                            Gender: item.Gender,
                            ClassName: item.ClassName,
                            ExamName: item.ExamName,
                            ResultDateMiti: item.ResultDateMiti,
                            FullMarks: item.FullMarks,
                            PassMarks: item.PassMarks,
                            ExamDate: item.ExamDatetime,
                            ObtMarks: item.ObtMarks,
                            Result: item.Result,
                            Remarks: item.Remarks,
                            Subject: item.Subject,
                            DOB_BS: item.DOB_BS,
                            ContactNo: item.ContactNo,
                            Address: item.Address,
                            Email: item.Email,
                            SubjectIdList: [],
                            ObtainedMarks: {},
                            TotalObtMarks: 0,
                            ObtainedPercentage: '',
                            ResultText: ''
                        };

                        if (item.Subject) {
                            var subjectIds = item.Subject.split(',');
                            groupedData[enquiryNo].SubjectIdList = subjectIds;

                            subjectIds.forEach(function (sid) {
                                groupedData[enquiryNo].ObtainedMarks[sid] = 0;
                            });
                        }
                    }

                    // Save obtained marks by subject
                    if (item.Subject) {
                        groupedData[enquiryNo].ObtainedMarks[item.SubjectId] = item.ObtMarks;
                        groupedData[enquiryNo].TotalObtMarks += (parseFloat(item.ObtMarks) || 0);
                    }
                });

                // Final pass to calculate percentage and result text
                angular.forEach(groupedData, function (cs) {
                    if (cs.FullMarks && cs.TotalObtMarks) {
                        cs.ObtainedPercentage = ((cs.TotalObtMarks / cs.FullMarks) * 100).toFixed(2);
                    }

                    if (cs.Result) {
                        var result = $scope.ResultList.find(function (item) {
                            return item.id == cs.Result;
                        });
                        cs.ResultText = result ? result.text : 'Unknown';
                    }
                });

                $scope.EntranceExamResultList = Object.values(groupedData);
                $scope.gridOptions2.data = $scope.EntranceExamResultList;
            } else {
                Swal.fire(res.data.ResponseMSG || "No data found.");
            }
        }, function (reason) {
            Swal.fire('Failed: ' + reason);
        });
    };



});