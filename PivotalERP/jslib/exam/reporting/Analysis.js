app.controller('examAnalysisController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Analysis';

    $scope.LoadData = function () {

        $('.select2').select2(
            {
                allowClear: true,
                openOnEnter: true,
            }
        );

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
            alert('Failed' + reason);
        });

        $scope.ReportTypes = [{ id: 1, text: 'Pivot' }, { id: 2, text: 'Table' }];

        $scope.currentPages = {
            ExamEvl: 1,
        };

        $scope.searchData = {
            ExamEvl: '',
            Printsearch: ''
        };

        $scope.perPage = {
            ExamEvl: 1,
        };

        $scope.newExamType = {
            ExamTypeId: 0,
            ReportType: 1
        };

        $scope.newTeacherSubject = {
            ExamTypeId: 0,
            ReportType: 1
        };

        $scope.newExamTypeGroup = {
            ExamTypeGroupId: 0,
            ReportType: 1
        };

        $scope.ExamTypeList = [];
        GlobalServices.getExamTypeList().then(function (res) {
            $scope.ExamTypeList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.ExamTypeGroupList = [];
        GlobalServices.getExamTypeGroupList().then(function (res) {
            $scope.ExamTypeGroupList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.ColumnColl = [{ id: 1, text: 'NoOfStudent', datatype: 'int32' }, { id: 2, text: 'ClassName', datatype: 'string' }, { id: 3, text: 'SectionName', datatype: 'string' }, { id: 4, text: 'Gender', datatype: 'string' }, { id: 5, text: 'HouseName', datatype: 'string' },
        { id: 6, text: 'Division', datatype: 'string' }, { id: 7, text: 'Grade', datatype: 'string' }, { id: 8, text: 'GP_Grade', datatype: 'string' }, { id: 9, text: 'Result', datatype: 'string' }, { id: 10, text: 'RankInClass', datatype: 'int32' },
        { id: 11, text: 'RankInSection', datatype: 'int32' }, { id: 12, text: 'RegdNo', datatype: 'string' }, { id: 13, text: 'SymbolNo', datatype: 'string' }, { id: 14, text: 'Per', datatype: 'double' }, { id: 15, text: 'ObtainMark', datatype: 'double' }, { id: 16, text: 'Caste', datatype: 'string' }];

        $scope.ColumnCollTS = [{ id: 1, text: 'NoOfStudent', datatype: 'int32' }, { id: 2, text: 'ClassName', datatype: 'string' }, { id: 3, text: 'Section', datatype: 'string' }, { id: 4, text: 'Teacher', datatype: 'string' }, { id: 5, text: 'Subject', datatype: 'string' },
        { id: 6, text: 'StudentName', datatype: 'string' }, { id: 7, text: 'Grade', datatype: 'string' }, { id: 8, text: 'Result', datatype: 'string' }, { id: 9, text: 'PaperType', datatype: 'string' }, { id: 10, text: 'RollNo', datatype: 'int32' },
        { id: 10, text: 'OM', datatype: 'double' }, { id: 11, text: 'OP', datatype: 'double' }, { id: 12, text: 'OM_Str', datatype: 'string' }, { id: 13, text: 'SymbolNo', datatype: 'string' }, { id: 14, text: 'IsFail', datatype: 'string' }, { id: 15, text: 'IsFailTH', datatype: 'string' }, { id: 16, text: 'IsFailPR', datatype: 'string' }];

        $scope.columnDefs = [];
        var sno = 1;
        angular.forEach($scope.ColumnColl, function (cln) {
            if (cln.datatype == "string")
                $scope.columnDefs.push({ name: cln.text, caption: cln.text });
            else if (cln.datatype == "int32" || cln.datatype == "int64")
                $scope.columnDefs.push({ name: cln.text, caption: cln.text, aggregateFunc: 'sum', formatFunc: function (val) { return $filter('formatNumber')(val); } });
            else if (cln.datatype == "double" || cln.datatype == "float")
                $scope.columnDefs.push({
                    name: cln.text, caption: cln.text,
                    dataSettings: {
                        aggregateFunc: 'sum',
                        formatFunc: function (value) {
                            //return $filter('formatNumber')(value);
                            return Number(value).toFixed(0);
                        }
                    }
                });
            else if (cln.datatype == "datetime" || cln.datatype == "date")
                $scope.columnDefs.push({ name: cln.text, caption: cln.text });
            else
                $scope.columnDefs.push({ name: cln.text, caption: cln.text });
        });

        $scope.columnDefsTS = [];
        sno = 1;
        angular.forEach($scope.ColumnCollTS, function (cln) {
            if (cln.datatype == "string")
                $scope.columnDefsTS.push({ name: cln.text, caption: cln.text });
            else if (cln.datatype == "int32" || cln.datatype == "int64")
                $scope.columnDefsTS.push({ name: cln.text, caption: cln.text, aggregateFunc: 'sum', formatFunc: function (val) { return $filter('formatNumber')(val); } });
            else if (cln.datatype == "double" || cln.datatype == "float")
                $scope.columnDefsTS.push({
                    name: cln.text, caption: cln.text,
                    dataSettings: {
                        aggregateFunc: 'sum',
                        formatFunc: function (value) {
                            //return $filter('formatNumber')(value);
                            return Number(value).toFixed(0);
                        }
                    }
                });
            else if (cln.datatype == "datetime" || cln.datatype == "date")
                $scope.columnDefsTS.push({ name: cln.text, caption: cln.text });
            else
                $scope.columnDefsTS.push({ name: cln.text, caption: cln.text });
        });

        google.load("visualization", "1", { packages: ["corechart", "charteditor"] });

        $scope.gridColumnDef = [
            { headerName: "S.No.", width: 80, field: "SNo", filter: 'agNumberColumnFilter', sortable: true },
            { headerName: "Class", width: 120, field: "ClassName", filter: 'agTextColumnFilter', },
            { headerName: "Section", width: 100, field: "SectionName", filter: 'agTextColumnFilter', },
            { headerName: "Roll No.", width: 100, field: "RollNo", filter: 'agNumberColumnFilter', },
            { headerName: "Reg.No.", width: 100, field: "RegdNo", filter: 'agTextColumnFilter', },
            { headerName: "B.Reg.No.", width: 120, field: "BoardRegdNo", filter: 'agTextColumnFilter', },
            { headerName: "Name", width: 150, field: "Name", filter: 'agTextColumnFilter', },
            { headerName: "Father Name", width: 150, field: "FatherName", filter: 'agTextColumnFilter', },
            { headerName: "Mother Name", width: 150, field: "MotherName", filter: 'agTextColumnFilter', },
            { headerName: "Address", width: 120, field: "Address", filter: 'agTextColumnFilter', },
            { headerName: "DOB(A.D.)", width: 120, field: "DOB_AD", filter: 'agDateColumnFilter', valueFormatter: function (params) { return DateFormatAD(params.value); }, },
            { headerName: "DOB(B.S.)", width: 120, field: "DOB_BS", filter: 'agTextColumnFilter', },
            { headerName: "Contact No.", width: 120, field: "ContactNo", filter: 'agTextColumnFilter', },
            { headerName: "House", width: 120, field: "HouseName", filter: 'agTextColumnFilter', },
            { headerName: "Credit Hour", width: 130, field: "CR", filter: 'agNumberColumnFilter', },
            { headerName: "Full Mark", width: 110, field: "FM", filter: 'agNumberColumnFilter', },
            { headerName: "Pass Mark", width: 110, field: "PM", filter: 'agNumberColumnFilter', },
            { headerName: "Obtain TH", width: 110, field: "OTH", filter: 'agNumberColumnFilter', },
            { headerName: "Obtain PR", width: 110, field: "OPR", filter: 'agNumberColumnFilter', },
            { headerName: "Total Obtain", width: 120, field: "ObtainMark", filter: 'agNumberColumnFilter', },
            { headerName: "Obtain Per%", width: 120, field: "Per", filter: 'agNumberColumnFilter', },
            { headerName: "Result", width: 90, field: "Result", filter: 'agTextColumnFilter', },
            { headerName: "GPA", width: 80, field: "GPA", filter: 'agNumberColumnFilter', },
            { headerName: "Grade", width: 100, field: "Grade", filter: 'agTextColumnFilter', },
            { headerName: "Division", width: 100, field: "Division", filter: 'agTextColumnFilter', },
            { headerName: "GP Grade", width: 120, field: "GP_Grade", filter: 'agTextColumnFilter', },
            { headerName: "Rank In Class", width: 150, field: "RankInClass", filter: 'agNumberColumnFilter', },
            { headerName: "Rank In Section", width: 150, field: "RankInSection", filter: 'agNumberColumnFilter', },
            { headerName: "Rank In School", width: 150, field: "RankInSchool", filter: 'agNumberColumnFilter', },
            { headerName: "Fail Subject", width: 130, field: "TotalFail", filter: 'agNumberColumnFilter', },
            { headerName: "TH Fail", width: 100, field: "TotalFailTH", filter: 'agNumberColumnFilter', },
            { headerName: "PR Fail", width: 100, field: "TotalFailPR", filter: 'agNumberColumnFilter', },
            { headerName: "Symbol No.", width: 110, field: "SymbolNo", filter: 'agTextColumnFilter', },
            { headerName: "Working Days", width: 130, field: "WorkingDays", filter: 'agNumberColumnFilter', },
            { headerName: "Present Days", width: 130, field: "PresentDays", filter: 'agNumberColumnFilter', },
            { headerName: "Absent Days", width: 130, field: "AbsentDays", filter: 'agNumberColumnFilter', },
            { headerName: "Comment", width: 200, field: "Comment", filter: 'agNumberColumnFilter', },
            { headerName: "Gender", width: 200, field: "Gender", filter: 'agNumberColumnFilter', },
            { headerName: "Caste", width: 200, field: "Caste", filter: 'agTextColumnFilter', },
        ];

        $scope.gridColumnDefTS = [
            { headerName: "Teacher", width: 180, field: "Teacher", filter: 'agTextColumnFilter', sortable: true },
            { headerName: "Class", width: 120, field: "ClassName", filter: 'agTextColumnFilter', },
            { headerName: "Section", width: 100, field: "Section", filter: 'agTextColumnFilter', },
            { headerName: "Roll No.", width: 100, field: "RollNo", filter: 'agNumberColumnFilter', },

            { headerName: "RegNo", width: 90, field: "RegNo", filter: 'agTextColumnFilter', },
            { headerName: "Name", width: 150, field: "StudentName", filter: 'agTextColumnFilter', },
            { headerName: "BoardRegNo", width: 120, field: "BoardRegNo", filter: 'agTextColumnFilter', },
            { headerName: "SymbolNo", width: 110, field: "SymbolNo", filter: 'agTextColumnFilter', },
            { headerName: "Subject", width: 120, field: "Subject", filter: 'agTextColumnFilter', },
            { headerName: "SubCode", width: 110, field: "SubCode", filter: 'agTextColumnFilter', },
            { headerName: "CH", width: 90, field: "CH", filter: 'agTextColumnFilter', },

            { headerName: "FM", width: 90, field: "FM", filter: 'agNumberColumnFilter', },
            { headerName: "PM", width: 90, field: "PM", filter: 'agNumberColumnFilter', },

            { headerName: "ObtainMark", width: 120, field: "OM_Str", filter: 'agTextColumnFilter', },
            { headerName: "Obtain Per%", width: 130, field: "OP", filter: 'agNumberColumnFilter', },
            { headerName: "Result", width: 90, field: "Result", filter: 'agTextColumnFilter', },


            { headerName: "Grade", width: 120, field: "Grade", filter: 'agTextColumnFilter', },

            { headerName: "GP_Grade", width: 110, field: "GP_Grade", filter: 'agTextColumnFilter', },
            { headerName: "GP", width: 90, field: "GP", filter: 'agNumberColumnFilter', },
            { headerName: "Is Fail", width: 90, field: "IsFail", filter: 'agNumberColumnFilter', },
            { headerName: "Is Fail TH", width: 90, field: "IsFailTH", filter: 'agNumberColumnFilter', },
            { headerName: "Is Fail PR", width: 90, field: "IsFailPR", filter: 'agNumberColumnFilter', },

        ];

        $scope.gridOptions = {

            // a default column definition with properties that get applied to every column
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,

                // set every column width
                width: 90
            },
            columnDefs: $scope.gridColumnDef,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            enableSorting: true,
            rowSelection: 'multiple'
        };

        $scope.gridOptions1 = {

            // a default column definition with properties that get applied to every column
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,

                // set every column width
                width: 90
            },
            columnDefs: $scope.gridColumnDef,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            enableSorting: true,
            rowSelection: 'multiple'
        };

        $scope.gridOptionsTS = {

            // a default column definition with properties that get applied to every column
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,

                // set every column width
                width: 90
            },
            columnDefs: $scope.gridColumnDefTS,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            enableSorting: true,
            rowSelection: 'multiple'
        };


        //New Code added by Suresh for Exam Evaluation starts
        var glbS = GlobalServices;

        $scope.ClassSection = {};
        glbS.getClassSectionList().then(function (res) {
            $scope.ClassSection = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.AllStudentColl = [];
        $http({
            method: 'POST',
            url: base_url + "Academic/Transaction/GetAllStudentForTran",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data)
                $scope.AllStudentColl = res.data.Data;

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
        //Ends
    }

    $scope.GetBranchId = function (bid) {
        if ($scope.BranchColl.length == 1)
            return $scope.BranchColl[0].BranchId
        else
            return bid;
    }

    $scope.ChangeTeachExam = function (col) {
        if (col == 'e') {
            $scope.newTeacherSubject.ExamTypeGroupId = 0;
        }
        else if (col == 'eg') {
            $scope.newTeacherSubject.ExamTypeId = 0;
        }
    }
    $scope.ExportExamType = function () {
        var params = {
            fileName: 'examAnalysis.csv',
            sheetName: 'examAnalysis'
        };
        $scope.gridOptions.api.exportDataAsCsv(params);
    }
    $scope.GetDataForMatrix = function () {
        if (!$scope.newExamType.ExamTypeId || $scope.newExamType.ExamTypeId == 0) {
            Swal.fire('Please select an Exam.');
            return;
        }

        if (!$scope.newExamType.ReportType || $scope.newExamType.ReportType == 0) {
            Swal.fire('Please select a Report Type.');
            return;
        }

        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.DataColl = [];

        var para = {
            ExamTypeId: $scope.newExamType.ExamTypeId,
            BranchId:$scope.newExamType.BranchId
        }
        $http({
            method: 'POST',
            url: base_url + "Exam/Report/GetExamResultSummary",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess) {
                $scope.DataColl = res.data.Data;

                if ($scope.newExamType.ReportType == 2) {
                    $scope.gridOptions.api.setRowData($scope.DataColl);

                } else {
                    var config = {
                        dataSource: $scope.DataColl,
                        canMoveFields: true,
                        dataHeadersLocation: 'columns',
                        width: 1099,
                        height: 611,
                        theme: 'green',
                        toolbar: {
                            visible: true
                        },
                        grandTotal: {
                            rowsvisible: true,
                            columnsvisible: true
                        },
                        subTotal: {
                            visible: true,
                            collapsed: true,
                            collapsible: true
                        },
                        rowSettings: {
                            subTotal: {
                                visible: true,
                                collapsed: true,
                                collapsible: true
                            }
                        },
                        columnSettings: {
                            subTotal: {
                                visible: true,
                                collapsed: true,
                                collapsible: true
                            }
                        },
                        fields: $scope.columnDefs,
                        //rows: ['Manufacturer'],//, 'Category' ],
                        //columns: ['Class', 'Category'],
                        //data: ['Quantity', 'Amount']                       
                    };

                    var elem = document.getElementById('datatable')

                    $scope.pgridwidget = new orb.pgridwidget(config);
                    $scope.pgridwidget.render(elem);
                }


            } else {
                alert(res.data.ResponseMSG)
                //Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


    }

    $scope.ExportExamTypeGroup = function () {
        var params = {
            fileName: 'examGroupAnalysis.csv',
            sheetName: 'examGroupAnalysis'
        };
        $scope.gridOptions1.api.exportDataAsCsv(params);
    }
    $scope.GetDataForMatrixGroup = function () {
        if (!$scope.newExamTypeGroup.ExamTypeGroupId || $scope.newExamTypeGroup.ExamTypeGroupId == 0) {
            Swal.fire('Please select an Exam Type Group.');
            return;
        }

        if (!$scope.newExamTypeGroup.ReportType || $scope.newExamTypeGroup.ReportType == 0) {
            Swal.fire('Please select a Report Type.');
            return;
        }
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.DataCollGroup = [];

        var para = {
            ExamTypeGroupId: $scope.newExamTypeGroup.ExamTypeGroupId,
            BranchId: $scope.newExamTypeGroup.BranchId
        }
        $http({
            method: 'POST',
            url: base_url + "Exam/Report/GetExamGroupResultSummary",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess) {
                $scope.DataCollGroup = res.data.Data;
                if ($scope.newExamTypeGroup.ReportType == 2) {
                    $scope.gridOptions1.api.setRowData($scope.DataCollGroup);

                } else {
                    var config = {
                        dataSource: $scope.DataCollGroup,
                        canMoveFields: true,
                        dataHeadersLocation: 'columns',
                        width: 1099,
                        height: 611,
                        theme: 'green',
                        toolbar: {
                            visible: true
                        },
                        grandTotal: {
                            rowsvisible: true,
                            columnsvisible: true
                        },
                        subTotal: {
                            visible: true,
                            collapsed: true,
                            collapsible: true
                        },
                        rowSettings: {
                            subTotal: {
                                visible: true,
                                collapsed: true,
                                collapsible: true
                            }
                        },
                        columnSettings: {
                            subTotal: {
                                visible: true,
                                collapsed: true,
                                collapsible: true
                            }
                        },
                        fields: $scope.columnDefs,
                        //rows: ['Manufacturer'],//, 'Category' ],
                        //columns: ['Class', 'Category'],
                        //data: ['Quantity', 'Amount']                       
                    };

                    var elem = document.getElementById('datatable1')

                    $scope.pgridwidget = new orb.pgridwidget(config);
                    $scope.pgridwidget.render(elem);
                }
            } else {
                alert(res.data.ResponseMSG)
                //Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


    }

    $scope.GetDataForPivot = function () {

        $scope.loadingstatus = "running";
        showPleaseWait();


        $http({
            method: 'POST',
            url: base_url + "Academic/Report/GetAnalysis",
            dataType: "json",
            //data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess) {
                var DataColl = res.data.Data;

                var derivers = $.pivotUtilities.derivers;
                var renderers = $.extend($.pivotUtilities.renderers,
                    $.pivotUtilities.plotly_renderers,
                    $.pivotUtilities.gchart_renderers,
                    $.pivotUtilities.export_renderers);

                $("#datatable1").pivotUI(DataColl, { renderers: renderers });

            } else {
                alert(res.data.ResponseMSG)
                //Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


    }

    $scope.PrintExamType = function () {

        var findExam = mx($scope.ExamTypeList).firstOrDefault(p1 => p1.ExamTypeId == $scope.newExamType.ExamTypeId);

        $http({
            method: 'GET',
            url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityExamType + "&voucherId=0&isTran=false",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                var templatesColl = res.data.Data;
                if (templatesColl && templatesColl.length > 0) {
                    var templatesName = [];
                    var sno = 1;
                    angular.forEach(templatesColl, function (tc) {
                        templatesName.push(sno + '-' + tc.ReportName);
                        sno++;
                    });

                    var print = false;

                    var rptTranId = 0;
                    if (templatesColl.length == 1)
                        rptTranId = templatesColl[0].RptTranId;
                    else {
                        Swal.fire({
                            title: 'Report Templates For Print',
                            input: 'select',
                            inputOptions: templatesName,
                            inputPlaceholder: 'Select a template',
                            showCancelButton: true,
                            inputValidator: (value) => {
                                return new Promise((resolve) => {
                                    if (value >= 0) {
                                        resolve()
                                        rptTranId = templatesColl[value].RptTranId;

                                        if (rptTranId > 0) {
                                            var dataColl = [];
                                            $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                                                dataColl.push(node.data);
                                            });
                                            print = true;
                                            $http({
                                                method: 'POST',
                                                url: base_url + "Exam/Report/PrintExamType",
                                                headers: { 'Content-Type': undefined },

                                                transformRequest: function (data) {

                                                    var formData = new FormData();
                                                    formData.append("jsonData", angular.toJson(data.jsonData));

                                                    return formData;
                                                },
                                                data: { jsonData: dataColl }
                                            }).then(function (res) {

                                                $scope.loadingstatus = "stop";
                                                hidePleaseWait();
                                                if (res.data.IsSuccess && res.data.Data) {

                                                    var rptPara = {
                                                        rpttranid: rptTranId,
                                                        istransaction: false,
                                                        entityid: entityExamType,
                                                        voucherid: 0,
                                                        vouchertype: 0,
                                                        sessionid: res.data.Data.ResponseId,
                                                        ExamTypeId: (findExam ? findExam.ExamTypeId : 0),
                                                        ExamTypeName: (findExam ? findExam.Name : '')

                                                    };
                                                    var paraQuery = param(rptPara);

                                                    document.body.style.cursor = 'wait';
                                                    document.getElementById("frmRpt").src = '';
                                                    document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
                                                    document.body.style.cursor = 'default';
                                                    $('#FrmPrintReport').modal('show');

                                                } else
                                                    Swal.fire('No Templates found for print');

                                            }, function (errormessage) {
                                                hidePleaseWait();
                                                $scope.loadingstatus = "stop";
                                                Swal.fire(errormessage);
                                            });

                                        }

                                    } else {
                                        resolve('You need to select:)')
                                    }
                                })
                            }
                        })
                    }

                    if (rptTranId > 0 && print == false) {
                        var dataColl = [];
                        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                            dataColl.push(node.data);
                        });
                        print = true;

                        $http({
                            method: 'POST',
                            url: base_url + "Exam/Report/PrintExamType",
                            headers: { 'Content-Type': undefined },

                            transformRequest: function (data) {

                                var formData = new FormData();
                                formData.append("jsonData", angular.toJson(data.jsonData));

                                return formData;
                            },
                            data: { jsonData: dataColl }
                        }).then(function (res) {

                            $scope.loadingstatus = "stop";
                            hidePleaseWait();
                            if (res.data.IsSuccess && res.data.Data) {

                                var rptPara = {
                                    rpttranid: rptTranId,
                                    istransaction: false,
                                    entityid: entityExamType,
                                    voucherid: 0,
                                    vouchertype: 0,
                                    sessionid: res.data.Data.ResponseId,
                                    ExamTypeId: (findExam ? findExam.ExamTypeId : 0),
                                    ExamTypeName: (findExam ? findExam.Name : '')

                                };
                                var paraQuery = param(rptPara);
                                document.body.style.cursor = 'wait';
                                document.getElementById("frmRpt").src = '';
                                document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
                                document.body.style.cursor = 'default';
                                $('#FrmPrintReport').modal('show');

                            } else
                                Swal.fire('No Templates found for print');

                        }, function (errormessage) {
                            hidePleaseWait();
                            $scope.loadingstatus = "stop";
                            Swal.fire(errormessage);
                        });

                    }

                } else
                    Swal.fire('No Templates found for print');
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };
    $scope.PrintExamTypeGroup = function () {

        var findExam = mx($scope.ExamTypeGroupList).firstOrDefault(p1 => p1.ExamTypeGroupId == $scope.newExamTypeGroup.ExamTypeGroupId);
        $http({
            method: 'GET',
            url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityExamTypeGroup + "&voucherId=0&isTran=false",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                var templatesColl = res.data.Data;
                if (templatesColl && templatesColl.length > 0) {
                    var templatesName = [];
                    var sno = 1;
                    angular.forEach(templatesColl, function (tc) {
                        templatesName.push(sno + '-' + tc.ReportName);
                        sno++;
                    });

                    var print = false;

                    var rptTranId = 0;
                    if (templatesColl.length == 1)
                        rptTranId = templatesColl[0].RptTranId;
                    else {
                        Swal.fire({
                            title: 'Report Templates For Print',
                            input: 'select',
                            inputOptions: templatesName,
                            inputPlaceholder: 'Select a template',
                            showCancelButton: true,
                            inputValidator: (value) => {
                                return new Promise((resolve) => {
                                    if (value >= 0) {
                                        resolve()
                                        rptTranId = templatesColl[value].RptTranId;

                                        if (rptTranId > 0) {
                                            var dataColl = [];
                                            $scope.gridOptions1.api.forEachNodeAfterFilterAndSort(function (node) {
                                                dataColl.push(node.data);
                                            });
                                            print = true;
                                            $http({
                                                method: 'POST',
                                                url: base_url + "Exam/Report/PrintExamTypeGroup",
                                                headers: { 'Content-Type': undefined },

                                                transformRequest: function (data) {

                                                    var formData = new FormData();
                                                    formData.append("jsonData", angular.toJson(data.jsonData));

                                                    return formData;
                                                },
                                                data: { jsonData: dataColl }
                                            }).then(function (res) {

                                                $scope.loadingstatus = "stop";
                                                hidePleaseWait();
                                                if (res.data.IsSuccess && res.data.Data) {

                                                    var rptPara = {
                                                        rpttranid: rptTranId,
                                                        istransaction: false,
                                                        entityid: entityExamTypeGroup,
                                                        voucherid: 0,
                                                        vouchertype: 0,
                                                        sessionid: res.data.Data.ResponseId,
                                                        ExamTypeId: (findExam ? findExam.ExamTypeId : 0),
                                                        ExamTypeName: (findExam ? findExam.Name : '')

                                                    };
                                                    var paraQuery = param(rptPara);

                                                    document.body.style.cursor = 'wait';
                                                    document.getElementById("frmRpt").src = '';
                                                    document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
                                                    document.body.style.cursor = 'default';
                                                    $('#FrmPrintReport').modal('show');

                                                } else
                                                    Swal.fire('No Templates found for print');

                                            }, function (errormessage) {
                                                hidePleaseWait();
                                                $scope.loadingstatus = "stop";
                                                Swal.fire(errormessage);
                                            });

                                        }

                                    } else {
                                        resolve('You need to select:)')
                                    }
                                })
                            }
                        })
                    }

                    if (rptTranId > 0 && print == false) {
                        var dataColl = [];
                        $scope.gridOptions1.api.forEachNodeAfterFilterAndSort(function (node) {
                            dataColl.push(node.data);
                        });
                        print = true;

                        $http({
                            method: 'POST',
                            url: base_url + "Exam/Report/PrintExamTypeGroup",
                            headers: { 'Content-Type': undefined },

                            transformRequest: function (data) {

                                var formData = new FormData();
                                formData.append("jsonData", angular.toJson(data.jsonData));

                                return formData;
                            },
                            data: { jsonData: dataColl }
                        }).then(function (res) {

                            $scope.loadingstatus = "stop";
                            hidePleaseWait();
                            if (res.data.IsSuccess && res.data.Data) {

                                var rptPara = {
                                    rpttranid: rptTranId,
                                    istransaction: false,
                                    entityid: entityExamTypeGroup,
                                    voucherid: 0,
                                    vouchertype: 0,
                                    sessionid: res.data.Data.ResponseId,
                                    ExamTypeId: (findExam ? findExam.ExamTypeId : 0),
                                    ExamTypeName: (findExam ? findExam.Name : '')

                                };
                                var paraQuery = param(rptPara);

                                document.body.style.cursor = 'wait';
                                document.getElementById("frmRpt").src = '';
                                document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
                                document.body.style.cursor = 'default';
                                $('#FrmPrintReport').modal('show');

                            } else
                                Swal.fire('No Templates found for print');

                        }, function (errormessage) {
                            hidePleaseWait();
                            $scope.loadingstatus = "stop";
                            Swal.fire(errormessage);
                        });

                    }

                } else
                    Swal.fire('No Templates found for print');
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };


    $scope.GetEmpSubDataForMatrix = function () {

        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.STDataColl = [];

        var para = {
            ExamTypeId: $scope.newTeacherSubject.ExamTypeId,
            ExamTypeGroupId: $scope.newTeacherSubject.ExamTypeGroupId,
            BranchId:$scope.newTeacherSubject.BranchId,
        }
        $http({
            method: 'POST',
            url: base_url + "Exam/Report/GetTeaherWiseSubjectAnalysis",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess) {
                $scope.STDataColl = res.data.Data;

                if ($scope.newTeacherSubject.ReportType == 2) {
                    $scope.gridOptionsTS.api.setRowData($scope.STDataColl);

                } else {
                    var config = {
                        dataSource: $scope.STDataColl,
                        canMoveFields: true,
                        dataHeadersLocation: 'columns',
                        width: 1099,
                        height: 611,
                        theme: 'green',
                        toolbar: {
                            visible: true
                        },
                        grandTotal: {
                            rowsvisible: true,
                            columnsvisible: true
                        },
                        subTotal: {
                            visible: true,
                            collapsed: true,
                            collapsible: true
                        },
                        rowSettings: {
                            subTotal: {
                                visible: true,
                                collapsed: true,
                                collapsible: true
                            }
                        },
                        columnSettings: {
                            subTotal: {
                                visible: true,
                                collapsed: true,
                                collapsible: true
                            }
                        },
                        fields: $scope.columnDefsTS,
                        //rows: ['Manufacturer'],//, 'Category' ],
                        //columns: ['Class', 'Category'],
                        //data: ['Quantity', 'Amount']                       
                    };

                    var elem = document.getElementById('datatableTS')

                    $scope.pgridwidget = new orb.pgridwidget(config);
                    $scope.pgridwidget.render(elem);
                }


            } else {
                alert(res.data.ResponseMSG)
                //Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


    }
    $scope.TeacherSubjectExportExamType = function () {
        var params = {
            fileName: 'examAnalysis.csv',
            sheetName: 'examAnalysis'
        };
        $scope.gridOptionsTS.api.exportDataAsCsv(params);
    }

    $scope.Print = function () {
        // Convert canvas elements to images before PDF generation
        convertCanvasToImage();

        // Select the element to print (in this case, #printcard)
        var element = document.getElementById('printcard');

        var titleElement = document.getElementById('title');
        if (titleElement) {
            titleElement.textContent = 'Evaluation'; // Set title to "Evaluation"
            titleElement.style.display = 'none'; // Hide the title
        }
        // Show the element temporarily to capture its content for printing
        element.style.display = 'block';

        // Use a timeout to ensure the title is hidden before generating the PDF
        setTimeout(() => {
            // Generate the PDF
            html2pdf()
                .set({
                    margin: 2, // Equal margins (top, left, bottom, right)
                    filename: 'evaluation.pdf',
                    image: { type: 'jpeg', quality: 1 },
                    html2canvas: { scale: 2, logging: true, letterRendering: true },
                    jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }
                })
                .from(element)
                .toPdf()
                .get('pdf')
                .then(function (pdf) {
                    // Create a blob URL from the PDF output
                    var blob = pdf.output('blob');
                    var url = URL.createObjectURL(blob);

                    // Open the PDF in a new tab
                    window.open(url, '_blank');
                })
                .finally(() => {
                    // Hide the element again after the PDF generation
                    //element.style.display = 'none';
                    //if (titleElement) {
                    //    titleElement.style.display = 'block'; // Show the title back
                    //}
                    /* document.getElementById('infocard').style.display = "none";*/
                });
        }, 100); // Delay in milliseconds
    };


    $scope.PrintForm = function () {
        // Convert canvas elements to images before printing
        convertCanvasToImage();

        // Trigger the print with printThis
        $('#printcard').printThis();
    };


    function convertCanvasToImage() {
        const printCards = document.querySelectorAll('#printcard .chart-container');
        printCards.forEach((card, index) => {
            const canvas = card.querySelector(`#barChart${index}`);
            if (canvas) {
                const img = document.createElement('img');
                img.src = canvas.toDataURL("image/png");
                img.style.height = canvas.style.height;
                img.style.width = canvas.style.width;
                canvas.replaceWith(img);
            }
        });
    }



    $scope.GetStudentEval = function () {
        if (!$scope.newDet.SelectedClass || $scope.newDet.SelectedClass === 0 || $scope.newDet.SelectedClass === '0') {
            Swal.fire({
                icon: 'warning',
                title: 'Please select a Class/Section',
                confirmButtonText: 'OK'
            });
            return;
        }
        // Validate Exam Types (multiple select)
        if (!$scope.newDet.SelectExamTypeId || $scope.newDet.SelectExamTypeId.length === 0) {
            Swal.fire({
                icon: 'warning',               
                title: 'Please select at least one Exam Type',
                confirmButtonText: 'OK'
            });
            return;
        }

        // Validate Current Exam
        if (!$scope.newDet.ExamTypeId || $scope.newDet.ExamTypeId === 0 || $scope.newDet.ExamTypeId === '0') {
            Swal.fire({
                icon: 'warning',
                title: 'Please select a Current Exam',
                confirmButtonText: 'OK'
            });
            return;
        }

        $scope.PrintList = [];
        $scope.ExamColl = [];
        $scope.ExamCollName = '';
        $scope.newDet.TotalPoint = 0;
        if ($scope.newDet.SelectedClass && $scope.newDet.ExamTypeId) {
            var para = {
                ClassId: $scope.newDet.SelectedClass.ClassId,
                SectionId: $scope.newDet.SelectedClass.SectionId,
                ExamTypeId: $scope.newDet.ExamTypeId,
                ExamTypeIdColl: ($scope.newDet.SelectExamTypeId ? $scope.newDet.SelectExamTypeId.toString() : ''),
                StudentIdColl: ($scope.newDet.StudentId ? $scope.newDet.StudentId.toString() : '')
            };
            $scope.loadingstatus = "running";
            showPleaseWait();

            $http({
                method: 'POST',
                url: base_url + "Exam/Report/GetStudentEvaluation",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {

                    if ($scope.newDet.SelectExamTypeId) {
                        $scope.newDet.SelectExamTypeId.forEach(function (et) {
                            var findExam = mx($scope.ExamTypeList).firstOrDefault(p1 => p1.ExamTypeId == et);

                            if ($scope.ExamCollName && $scope.ExamCollName.length > 0) {
                                $scope.ExamCollName = $scope.ExamCollName + " , ";
                            }

                            $scope.ExamCollName = $scope.ExamCollName + findExam.DisplayName;

                            $scope.ExamColl.push({
                                ExamTypeId: findExam.ExamTypeId,
                                label: findExam.DisplayName,
                                borderWidth: 1,
                                borderRadius: 3,
                                backgroundColor: generateRandomColor(),
                                data: [],
                            })
                        });
                    }

                    res.data.Data.forEach(function (item) {
                        var itemTotalPoints = 0;
                        if (item.AchievementColl && Array.isArray(item.AchievementColl)) {
                            item.AchievementColl.forEach(function (achievement) {
                                if (achievement.Point) {
                                    itemTotalPoints += achievement.Point;
                                }
                            });
                        }
                        item.Point = itemTotalPoints;
                    });

                    $scope.PrintList = res.data.Data;
                    $timeout(function () {
                        var ind = 0;
                        $scope.loadingstatus = "running";
                        showPleaseWait();
                        $scope.PrintList.forEach(function (item) {
                            item.Index = ind;
                            if (item.EvaluationBarColl && Array.isArray(item.EvaluationBarColl)) {
                                $scope.EvaluationBarGraph(item);
                            }
                            ind++;
                        });

                        $scope.loadingstatus = "stop";
                        hidePleaseWait();

                    });

                    //Added By Suresh
                    $scope.showPrintCard = true;

                    //Ends
                } else {
                    Swal.fire(res.data.ResponseMSG);
                }

            }, function (reason) {
                Swal.fire('Failed ' + reason);
            });

        }

    }

    function generateRandomColor() {
        let maxVal = 0xFFFFFF; // 16777215
        let randomNumber = Math.random() * maxVal;
        randomNumber = Math.floor(randomNumber);
        randomNumber = randomNumber.toString(16);
        let randColor = randomNumber.padStart(6, 0);
        return `#${randColor.toUpperCase()}`
    }
    $scope.ExamColl = [];
    $scope.EvaluationBarGraph = function (curRow) {

        $scope.loadingstatus = "running";
        showPleaseWait();

        var lbls = [];
        var tmpExamColl = angular.copy($scope.ExamColl);
        var tmpExamColl_Qry = mx(tmpExamColl);
        var subQry = mx(curRow.EvaluationBarColl).groupBy(p1 => p1.SubjectId);
        angular.forEach(subQry, function (sub) {
            var fst = sub.elements[0];
            lbls.push(fst.SubjectName);

            //sub.elements.forEach(function (el) {
            //    var findExam = tmpExamColl_Qry.firstOrDefault(p1 => p1.ExamTypeId == el.ExamTypeId);
            //    findExam.data.push(el.Obtain);
            //});
            sub.elements.forEach(function (el) {
                var findExam = tmpExamColl_Qry.firstOrDefault(function (p1) {
                    return p1.ExamTypeId == el.ExamTypeId;
                });
                if (findExam) {
                    findExam.data.push(el.Obtain || 0);
                }
            });

        });

        var canvasName = 'barChart' + curRow.Index;
        var canvas = document.getElementById(canvasName);
        if (canvas == null) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            return;
        }

        var ctx = canvas.getContext('2d');
        //ctx.canvas.width = 1400;
        //ctx.canvas.height = 300;
        var data = {
            labels: lbls,
            datasets: tmpExamColl,
        };

        var options = {
            responsive: false,
            scales: {
                x: {
                    grid: {
                        display: false,
                    },
                    ticks: {
                        font: {
                            size: 16,
                            weight: 'bold'
                        },
                        color: 'black'
                    }
                },
                y: {
                    ticks: {
                        font: {
                            size: 16,
                            weight: 'bold'
                        },
                        color: 'black'
                    }
                }

            },

            plugins: {
                layout: {
                    padding: {
                        left: 20,
                        right: 0,
                        top: 0,
                        bottom: 0,
                    },
                },
                datalabels: {
                    display: true,
                    color: 'black',
                    align: 'top',
                    anchor: 'start',
                    font: {
                        size: 14,
                        weight: 'bold'
                    }
                },
                // Configure ToolTips 
                tooltips: {
                    enabled: true, // Enable/Disable ToolTip By Default Its True
                    backgroundColor: "red", // Set Tooltip Background Color
                    titleFontFamily: "Comic Sans MS", // Set Tooltip Title Font Family
                    titleFontSize: 30, // Set Tooltip Font Size
                    titleFontStyle: "bold italic",
                    titleFontColor: "yellow",
                    titleAlign: "center",
                    titleSpacing: 3,
                    titleMarginBottom: 50,
                    bodyFontFamily: "Comic Sans MS",
                    bodyFontSize: 20,
                    bodyFontStyle: "italic",
                    bodyFontColor: "black",
                    bodyAlign: "center",
                    bodySpacing: 3,
                },
                // Custom Chart Title
                title: {
                    display: true,
                    position: "bottom",
                    fontSize: 25,
                    fontFamily: "Comic Sans MS",
                    fontColor: "red",
                    fontStyle: "bold italic",
                    padding: 20,
                    lineHeight: 5,
                },
                // Legends Configuration
                legend: {
                    /* display: false,*/
                    position: "top",
                    align: "center",
                    labels: {
                        fontColor: "red",
                        boxWidth: 20,
                        usePointStyle: true,
                        font: {
                            size: 30
                        }
                    },
                },
            },
            legend: {
                display: false,
            }

        };

        var myBarChart = new Chart(ctx, {
            type: 'bar',
            data: data,
            options: options
        });

        $scope.loadingstatus = "stop";
        hidePleaseWait();
    };




});