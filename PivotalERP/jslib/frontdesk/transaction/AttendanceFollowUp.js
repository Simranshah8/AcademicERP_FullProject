

app.controller('AttendanceFollowUpController', function ($scope, $http, $timeout, $filter, $translate, $rootScope, GlobalServices, $compile) {
    $scope.Title = 'AttendanceFollowUp';
    getterAndSetter();
    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    function getterAndSetter() {
        $scope.gridColumnDef = [

            { headerName: "Adm.No.", field: "RegdNo", width: 110, pinned: 'left', filter: 'agTextColumnFilter' },
            { headerName: "Name", field: "Name", width: 180, pinned: 'left', filter: 'agTextColumnFilter' },
            { headerName: "Roll No.", field: "RollNo", width: 110, filter: 'agTextColumnFilter' },
            { headerName: "Class", field: "ClassName", width: 120, filter: 'agTextColumnFilter' },
            { headerName: "Section", field: "SectionName", width: 120, filter: 'agTextColumnFilter' },
            { headerName: "Batch", field: "Batch", width: 120, filter: 'agTextColumnFilter' },
            { headerName: "Class Year", field: "ClassYear", width: 130, filter: 'agTextColumnFilter' },
            { headerName: "Semester", field: "Semester", width: 120, filter: 'agTextColumnFilter' },
            { headerName: "Contact No", field: "PersonalContactNo", width: 150, filter: 'agTextColumnFilter' },
            { headerName: "Father Name", field: "FatherName", width: 150, filter: 'agTextColumnFilter' },
            { headerName: "Father Contact No", field: "ContactNo", width: 150, filter: 'agTextColumnFilter' },
            { headerName: "Mother Name", field: "MotherName", width: 150, filter: 'agTextColumnFilter' },
            { headerName: "Mother Contact No", field: "M_Contact", width: 150, filter: 'agTextColumnFilter' },
            { headerName: "Guardian Name", field: "GuardianName", width: 150, filter: 'agTextColumnFilter' },
            { headerName: "Guardian Contact No", field: "G_ContactNo", width: 150, filter: 'agTextColumnFilter' },
            { headerName: "Class Shift", field: "ClassShift", width: 130, filter: 'agTextColumnFilter' },
            {
                headerName: "Action",
                field: "icon",
                width: 105,
                pinned: 'right',
                cellRenderer: function (params) {
                    return `<button class="btn btn-sm btn-info mt-1 py-0"
                    onclick='angular.element(this).scope().openFollowupModal(${JSON.stringify(params.data)})'>
                    Followup</button>`;
                }
            }
        ];

        $scope.gridOptions = {
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,

                // set every column width
                width: 90
            },
            headerHeight: 31,
            rowHeight: 30,
            columnDefs: $scope.gridColumnDef,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            enableSorting: true,
            overlayLoadingTemplate: "Please Click the Load Button to display the data",
            rowSelection: 'multiple',
            suppressHorizontalScroll: false,
            alignedGrids: [],
            enableFilter: true

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);

        $scope.gridOptions.onFirstDataRendered = function () {
            $timeout(function () {
                $compile(angular.element($scope.eGridDiv).contents())($scope);
            });
        };



        //REport Table starts
        $scope.gridOptionsR = [];

        $scope.gridOptionsR = {
            showGridFooter: true,
            showColumnFooter: false,
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: true,
            enableSorting: true,
            enableRowSelection: true,
            enableSelectAll: true,
            enableGridMenu: true,

            columnDefs: [

                { name: "RegNo", displayName: "Adm.No", minWidth: 100, headerCellClass: 'headerAligment' },
                { name: "StudentName", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
                { name: "RollNo", displayName: "Roll No", minWidth: 100, headerCellClass: 'headerAligment' },

                { name: "ClassName", displayName: "ClassName", minWidth: 130, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
                { name: "SectionName", displayName: "SectionName", minWidth: 130, headerCellClass: 'headerAligment' },
                { name: "Batch", displayName: "Batch", minWidth: 140, headerCellClass: 'headerAligment' },
                { name: "ClassYear", displayName: "Class Year", minWidth: 140, headerCellClass: 'headerAligment' },

                { name: "Semester", displayName: "Semester", minWidth: 110, headerCellClass: 'headerAligment' },
                { name: "FollowUpDate", displayName: "Followup Date", minWidth: 120, headerCellClass: 'headerAligment' },
                { name: "FollowUpTo", displayName: "Followup To", minWidth: 150, headerCellClass: 'headerAligment' },
                { name: "ContactNo", displayName: "Contact No", minWidth: 130, headerCellClass: 'headerAligment' },
                { name: "FollowUpStatus", displayName: "Followup Status", minWidth: 140, headerCellClass: 'headerAligment' },
                { name: "FollowUpRemarks", displayName: "Followup Remarks", minWidth: 180, headerCellClass: 'headerAligment' },
            ],
            //   rowTemplate: rowTemplate(),
            exporterCsvFilename: 'enqSummary.csv',
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
    }

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

        } else {
            $scope.gridOptions.columnApi.setColumnsVisible(["Faculty"], false);
        }


        if ($scope.AcademicConfig.ActiveLevel == true) {
            $scope.LevelList = [];
            GlobalServices.getClassLevelList().then(function (res) {
                $scope.LevelList = res.data.Data;
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        } else {
            $scope.gridOptions.columnApi.setColumnsVisible(["Level"], false);
        }


        if ($scope.AcademicConfig.ActiveSemester == true) {
            $scope.SemesterList = [];
            GlobalServices.getSemesterList().then(function (res) {
                $scope.SemesterList = res.data.Data;
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        } else {
            $scope.gridOptions.columnApi.setColumnsVisible(["Semester"], false);
        }

        if ($scope.AcademicConfig.ActiveBatch == true) {
            $scope.BatchList = [];
            GlobalServices.getBatchList().then(function (res) {
                $scope.BatchList = res.data.Data;
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        } else {
            $scope.gridOptions.columnApi.setColumnsVisible(["Batch"], false);
        }

        if ($scope.AcademicConfig.ActiveClassYear == true) {

            $scope.ClassYearList = [];
            $scope.SelectedClassClassYearList = [];
            GlobalServices.getClassYearList().then(function (res) {
                $scope.ClassYearList = res.data.Data;
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        } else {
            $scope.gridOptions.columnApi.setColumnsVisible(["ClassYear"], false);
        }

    }, function (reason) {
        Swal.fire('Failed' + reason);
    });

    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.searchData = {
            AttendanceFollowup: ''
        };

        $scope.ClassShiftList = [];
        $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllClassShift",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ClassShiftList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.ClassSectionList = [];
        GlobalServices.getClassSectionList().then(function (res) {
            $scope.ClassSectionList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.absentOnly = {
            ForDate_TMP: new Date()
        }

        $scope.newFilter2 = {
            FromDate_TMP: new Date(),
            ToDate_TMP: new Date()
        }


        $scope.FollowUpToList = [
            { id: 1, text: 'Father' },
            { id: 2, text: 'Mother' },
            { id: 3, text: 'Guardian' },
            { id: 4, text: 'Student' }
        ];

        $scope.FollowStatusList = [
            { id: 1, text: 'Success' },
            { id: 2, text: 'Failed' }
        ];

        //$scope.GetAllAttendanceFollowupList();
        $scope.GetAllAttendanceForFollowupList();
        $scope.GetAttFollowUpReport();

    };

    $scope.ClearFollowup = function () {
        $scope.newAttendanceFollowup = {
            TranId: null,
            FollowUpDate_TMP: new Date(),
            FollowUpTo: null,
            ContactNo: '',
            FollowUpStatus: null,
            FollowUpRemarks: '',
            StudentId: null,
            Mode: 'save'
        }
        $scope.GetAllAttendanceFollowupList();
    };

    $scope.$watch('absentOnly.SelectedClassSection', function (newVal, oldVal) {
        if (newVal && oldVal && newVal.id !== oldVal.id) {
            $scope.absentOnly.SemesterId = 0;
            $scope.absentOnly.ClassYearId = 0;
        }
    }, true);

    $scope.GetAllAttendanceForFollowupList = function () {
        var para = {
            ClassId: $scope.absentOnly.SelectedClassSection ? $scope.absentOnly.SelectedClassSection.ClassId : null,
            SectionId: $scope.absentOnly.SelectedClassSection ? $scope.absentOnly.SelectedClassSection.SectionId : null,
            forDate: $filter('date')($scope.absentOnly.ForDateDet.dateAD, 'yyyy-MM-dd'),
            InOutMode: 2,
            ClassShiftId: $scope.absentOnly.ClassShiftId,
            BatchId: $scope.absentOnly.BatchId,
            ClassYearId: $scope.absentOnly.ClassYearId,
            SemesterId: $scope.absentOnly.SemesterId
        };
        $scope.loadingstatus = "running";
        showPleaseWait();
        $http({
            method: 'POST',
            url: base_url + "Attendance/Creation/GetSTMADaily",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.gridOptions.api.setRowData(res.data.Data);
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }


    $scope.$watch('newFilter2.SelectedClassSection', function (newVal, oldVal) {
        if (newVal && oldVal && newVal.id !== oldVal.id) {
            $scope.newFilter2.SemesterId = 0;
            $scope.newFilter2.ClassYearId = 0;
        }
    }, true);

    $scope.GetAttFollowUpReport = function () {
        var para = {
            dateFrom: ($scope.newFilter2.FromDateDet ? $filter('date')(new Date($scope.newFilter2.FromDateDet.dateAD), 'yyyy-MM-dd') : null),
            dateTo: ($scope.newFilter2.ToDateDet ? $filter('date')(new Date($scope.newFilter2.ToDateDet.dateAD), 'yyyy-MM-dd') : null),
            ClassId: $scope.newFilter2.SelectedClassSection ? $scope.newFilter2.SelectedClassSection.ClassId : null,
            SectionId: $scope.newFilter2.SelectedClassSection ? $scope.newFilter2.SelectedClassSection.SectionId : null,
            BatchId: $scope.newFilter2.BatchId,
            ClassYearId: $scope.newFilter2.ClassYearId,
            SemesterId: $scope.newFilter2.SemesterId,
            ClassShiftId: $scope.newFilter2.ClassShiftId
        };
        $scope.loadingstatus = "running";
        showPleaseWait();
        $http({
            method: 'POST',
            url: base_url + "FrontDesk/Transaction/GetAllAttendanceFollowUp",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.gridOptionsR.data = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.openFollowupModal = function (student) {
        $scope.newAttendanceFollowup = {
            StudentId: student.StudentId,
            AdmNo: student.RegdNo,
            Name: student.Name,
            RollNo: student.RollNo,
            ClassName: student.ClassName,

            // Additional fields for followup
            FatherContactNo: student.ContactNo,
            MotherContactNo: student.M_Contact,
            GuardianContactNo: student.G_ContactNo,
            StudentContactNo: student.PersonalContactNo,

            ContactNo: '', // will be populated on dropdown change
            FollowUpTo: '',
            FollowUpDate_TMP: new Date(),
            FollowUpDate: null,
            FollowUpStatus: '',
            FollowUpRemarks: ''
        };
        var para = {
            StudentId: student.StudentId
        };

        $http({
            method: 'POST',
            url: base_url + "FrontDesk/Transaction/GetAttendanceFollowup",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newAttendanceFollowup.HistoryColl = res.data.Data;
                $('#followupClosed').modal('show');

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        //$scope.$applyAsync(); 
        //$('#followupClosed').modal('show');
    };


    $scope.onFollowUpToChange = function () {
        const type = $scope.newAttendanceFollowup.FollowUpTo;

        switch (type) {
            case 1: // Father
                $scope.newAttendanceFollowup.ContactNo = $scope.newAttendanceFollowup.FatherContactNo || '';
                break;
            case 2: // Mother
                $scope.newAttendanceFollowup.ContactNo = $scope.newAttendanceFollowup.MotherContactNo || '';
                break;
            case 3: // Guardian
                $scope.newAttendanceFollowup.ContactNo = $scope.newAttendanceFollowup.GuardianContactNo || '';
                break;
            case 4: // Student
                $scope.newAttendanceFollowup.ContactNo = $scope.newAttendanceFollowup.StudentContactNo || '';
                break;
            default:
                $scope.newAttendanceFollowup.ContactNo = '';
        }
    };

    $scope.SaveAttendanceFollowup = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        if ($scope.newAttendanceFollowup.FollowUpDateDet) {
            $scope.newAttendanceFollowup.FollowUpDate = $filter('date')(new Date($scope.newAttendanceFollowup.FollowUpDateDet.dateAD), 'yyyy-MM-dd');
        } else {
            $scope.newAttendanceFollowup.FollowUpDate = null;
        }


        $http({
            method: 'POST',
            url: base_url + "FrontDesk/Transaction/SaveAttendanceFollowup",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newAttendanceFollowup }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $('#followupClosed').modal('hide');
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    }

    $scope.validateDates = function (changedField) {
        if (!$scope.newFilter2.FromDateDet || !$scope.newFilter2.ToDateDet ||
            !$scope.newFilter2.FromDateDet.dateAD || !$scope.newFilter2.ToDateDet.dateAD) {
            return true;
        }
        var fromDate = $filter('date')(new Date($scope.newFilter2.FromDateDet.dateAD), 'yyyy-MM-dd')
        var toDate = $filter('date')(new Date($scope.newFilter2.ToDateDet.dateAD), 'yyyy-MM-dd')
  
        if (!fromDate || !toDate) return true;
        if (fromDate > toDate) {
            if (changedField === 'fromDate') {
                Swal.fire({
                    icon: 'warning',
                    text: 'From Date cannot be greater than To Date.',
                    confirmButtonText: 'OK'
                }).then(function () {
                    $scope.$apply(function () {
                        $scope.newFilter2.FromDate_TMP = new Date();
                        $scope.newFilter2.FromDateDet = new Date();
                    });
                });
            } else if (changedField === 'toDate') {
                Swal.fire({
                    icon: 'warning',
                    text: 'To Date cannot be less than From Date.',
                    confirmButtonText: 'OK'
                }).then(function () {
                    $scope.$apply(function () {
                        $scope.newFilter2.ToDate_TMP = new Date();
                        $scope.newFilter2.ToDateDet = new Date();
                    });
                });
            }
            return false;
        }

        return true;
    };
});
