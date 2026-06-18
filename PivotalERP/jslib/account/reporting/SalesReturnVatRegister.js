"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller("SalesReturnVatRegister", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'SalesVatRegister.csv',
            sheetName: 'SalesVatRegister'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }


    $scope.GetPeriodMonths = function (costClassId) {
        if (costClassId > 0) {
            GlobalServices.getCompanyPeriodMonth(costClassId).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.PeriodMonthColl = res.data.Data;

                    if ($scope.PeriodMonthColl) {
                        $scope.PeriodMonthColl.forEach(function (pm) {
                            if (pm.IsRunning == true) {
                                $scope.SalesVatRegister.SelectedMonth = pm;
                                $scope.SalesVatRegister.RptMonthSNo = pm.SNo;

                                $scope.SalesVatRegister.DateFrom_TMP = new Date(pm.FromDate);
                                $scope.SalesVatRegister.DateTo_TMP = new Date(pm.ToDate);
                            }
                        });
                    }
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        }
    }
    $scope.ChangePeriodMonth = function (sm) {
        if (sm) {
            $scope.SalesVatRegister.DateFrom_TMP = new Date(sm.FromDate);
            $scope.SalesVatRegister.DateTo_TMP = new Date(sm.ToDate);

        }
    }
    $scope.ChangePeriodOption = function () {
        if ($scope.SalesVatRegister.PeriodOPT == 2) {
            $scope.ChangePeriodMonth($scope.SalesVatRegister.SelectedMonth);
        }
    }
    function LoadData() {

        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.perPage = {
            SalesVatRegister: GlobalServices.getPerPageRow(),

        };
        $scope.currentPages = {
            SalesVatRegister: 1
        };
        $scope.searchData = {
            SalesVatRegister: ''
        };
        $scope.IncludeVouchers = [{ id: 1, text: 'Debit Note', IsSelected: false }, { id: 2, text: 'Credit Note', IsSelected: false }];
        $scope.ReportFormatColl = [{ id: 1, text: 'ProductWise' }, { id: 2, text: 'InvoiceWise' }];
        $scope.SalesVatRegister = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            VoucherId: null,
            BranchId: null,
            ReportType: 2,
            PeriodOPT: 2,
            RptCostClassId: null,
            RptMonthSNo: 0,
            selectTable: false,
        };

        $scope.PeriodOptColl = GlobalServices.getPeriodOptions();
        $scope.RptCostClassColl = [];
        GlobalServices.getCostClassForRpt().then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.RptCostClassColl = res.data.Data;
                if ($scope.RptCostClassColl && $scope.RptCostClassColl.length > 0) {
                    $scope.SalesVatRegister.RptCostClassId = $scope.RptCostClassColl[0].CostClassId;
                    $scope.GetPeriodMonths($scope.RptCostClassColl[0].CostClassId);
                }
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.loadingstatus = "stop";

        var columnDefs = [
            {
                headerName: "Miti", width: 130, field: "VoucherDate_BS", cellRenderer: 'agGroupCellRenderer',
                valueFormatter: function (params) { return DateFormatBS(params.value); },
                showRowGroup: true, cellStyle: { 'text-align': 'center' },
                cellRendererParams: {
                    suppressCount: false, // turn off the row count                   
                }, pinned: 'left',dataType:'string'
            },
            { headerName: "InvoiceNo", width: 140, field: "VoucherNo", cellStyle: { 'text-align': 'center' }, pinned: 'left', dataType: 'string'},
            { headerName: "Customer Name", width: 220, field: "PartyName", cellStyle: { 'text-align': 'left' }, dataType: 'string' },
            { headerName: "Customer PAN", width: 160, field: "PanVat", cellStyle: { 'text-align': 'left' }, dataType: 'string' },
            { headerName: "Total Return", width: 140, field: "TotalSales", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Product Name", width: 180, field: "ProductName", cellStyle: { 'text-align': 'left' }, colId: 'colItem1', dataType: 'string' },
            { headerName: "Qty.", width: 140, field: "ActualQty", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, colId: 'colItem2', dataType: 'Number' },
            { headerName: "UOM", width: 120, field: "Unit", cellStyle: { 'text-align': 'right' }, colId: 'colItem3', dataType: 'string' },
            { headerName: "Item Amt.", width: 160, field: "ProductAmount", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, colId: 'colItem4', dataType: 'Number' },
            { headerName: "Non Taxable Amt.", width: 170, field: "NonTaxableSales", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, dataType: 'Number' },
            { headerName: "Taxable Amt.", width: 160, field: "TaxableSales", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, dataType: 'Number' },
            { headerName: "Vat", width: 140, field: "ProductVatAmount", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, dataType: 'Number' },
            { headerName: "Invoice Amount", width: 140, field: "InvoiceAmount", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, dataType: 'Number' },
            { headerName: "Export Sales", width: 160, field: "ExportSales", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, dataType: 'Number' },
            { headerName: "Export Country", width: 160, field: "ExportCountry", cellStyle: { 'text-align': 'left' }, dataType: 'string' },
            { headerName: "Branch", width: 170, field: "Branch", cellStyle: { 'text-align': 'left' }, dataType: 'string' },
            { headerName: "Excise Duty", width: 160, field: "ExciseDuty", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, dataType: 'Number' },

            
            { headerName: "PP Date", width: 140, field: "PPDate_BS", cellStyle: { 'text-align': 'right' }, dataType: 'string' },
            { headerName: "PP No.", width: 140, field: "PPNo", cellStyle: { 'text-align': 'right' }, dataType: 'string' },
            { headerName: "Year", width: 120, field: "ForYear", dataType: 'Text', cellStyle: { 'text-align': 'left' }, dataType: 'string' },
            { headerName: "Voucher Type", width: 150, field: "VoucherType", cellStyle: { 'text-align': 'left' }, dataType: 'string'},
            { headerName: "Voucher Name", width: 150, field: "VoucherName", cellStyle: { 'text-align': 'left' }, dataType: 'string' },
            
            
        ];

        $scope.AnalysisCollDefs = [];
        angular.forEach(columnDefs, function (cln) {
            if (cln.datatype == "string" || cln.dataType == 'Text')
                $scope.AnalysisCollDefs.push({ name: cln.field, caption: cln.headerName });
            else if (cln.datatype == "int32" || cln.datatype == "int64")
                $scope.AnalysisCollDefs.push({ name: cln.field, caption: cln.headerName, aggregateFunc: 'sum', formatFunc: function (val) { return $filter('formatNumber')(val); } });
            else if (cln.datatype == "Number" || cln.datatype == "double" || cln.datatype == "float" || cln.datatype == "decimal")
                $scope.AnalysisCollDefs.push({
                    name: cln.field, caption: cln.headerName,
                    dataSettings: {
                        aggregateFunc: 'sum',
                        formatFunc: function (value) {
                            return Number(value).toFixed(0);
                        }
                    }
                });
            else if (cln.datatype == "datetime" || cln.datatype == "DateTime" || cln.datatype == "date")
                $scope.AnalysisCollDefs.push({ name: cln.field, caption: cln.headerName });
            else
                $scope.AnalysisCollDefs.push({ name: cln.field, caption: cln.headerName });
        });

        $scope.gridOptions = {

            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                width: 100,


            },
            enableSorting: true,
            multiSortKey: 'ctrl',
            enableColResize: true,
            overlayLoadingTemplate: "Please Click the Load Button to display the data.",
            overlayNoRowsTemplate: "No Records found",
            rowSelection: 'multiple',
            columnDefs: columnDefs,
            rowData: null,
            filter: true,
            suppressHorizontalScroll: true,
            alignedGrids: [],
            enableFilter: true,

            onFilterChanged: function () {

                var dt = {
                    VoucherName: 'TOTAL =>',
                    AccessableValue: 0,
                    VatAmount: 0,
                    InvoiceAmount: 0,
                    ActualQty: 0,
                    BilledQty: 0,
                    ProductAmount: 0,
                    ProductVatAmount: 0,
                    TaxableSales: 0,
                    NonTaxableSales: 0,
                    ExportSales: 0,
                    ExciseDuty: 0,
                }
                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var fData = node.data;
                    dt.AccessableValue += fData.AccessableValue;
                    dt.VatAmount += fData.VatAmount;
                    dt.InvoiceAmount += fData.InvoiceAmount;
                    dt.ActualQty += fData.ActualQty;
                    dt.BilledQty += fData.BilledQty;

                    dt.ProductAmount += fData.ProductAmount;
                    dt.ProductVatAmount += fData.ProductVatAmount;
                    dt.TaxableSales += fData.TaxableSales;
                    dt.NonTaxableSales += fData.NonTaxableSales;

                    dt.ExportSales += fData.ExportSales;
                    dt.ExciseDuty += fData.ExciseDuty;
                });


                var filterDataColl = [];
                filterDataColl.push(dt);

                $scope.gridOptionsBottom.api.setRowData(filterDataColl);
            }

        };
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);

        $scope.dataForBottomGrid = [
            {
                AutoNumber: '',
                VoucherType: 'Total =>',
                PanVat: '',
                InvoiceAmount: 0
            }];

        $scope.gridOptionsBottom = {
            defaultColDef: {
                resizable: true,
                width: 90
            },
            columnDefs: columnDefs,
            // we are hard coding the data here, it's just for demo purposes
            rowData: $scope.dataForBottomGrid,
            debug: true,
            rowClass: 'bold-row',
            // hide the header on the bottom grid
            headerHeight: 0,
            alignedGrids: []
        };

        $scope.gridOptions.alignedGrids.push($scope.gridOptionsBottom);
        $scope.gridOptionsBottom.alignedGrids.push($scope.gridOptions);

        $scope.gridDivBottom = document.querySelector('#myGridBottom');
        new agGrid.Grid($scope.gridDivBottom, $scope.gridOptionsBottom);
        $scope.loadingstatus = "stop";

        $scope.BranchList = [];
        $http({
            method: 'GET',
            url: base_url + "Setup/Security/GetAllBranchList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BranchList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });



    }


    $scope.SelectedInclude = function () {
        var val = $scope.SalesVatRegister.IncludeVouchers;
        $scope.IncludeVouchers.forEach(function (v) {
            v.IsSelected = val;
        });
    }

    $scope.ShowAnalysis = function () {

        var oldState = {
            rowFields: [],
            columnFields: [],
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
            }
        };

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
            rowFields: oldState.rowFields,
            columnFields: oldState.columnFields,
            rowSettings: oldState.rowSettings,
            columnSettings: oldState.columnSettings,
            fields: $scope.AnalysisCollDefs,
            rows: oldState.rowFields,
            columns: oldState.columnFields,
            data: oldState.dataFields
            //rows: ['Manufacturer'],//, 'Category' ],
            //columns: ['Class', 'Category'],
            //data: ['Quantity', 'Amount']                       
        };

        var elem = document.getElementById('dtAnalysis')

        $scope.pgridwidget = new orb.pgridwidget(config);
        $scope.pgridwidget.render(elem);


        $('#frmRptAnalysis').modal('show');
    }
    $scope.ClearData = function () {

        var DataColl = [];
        $scope.gridOptionsBottom.api.setRowData(DataColl);

        $scope.gridOptions.api.setRowData(DataColl);
    };

    $scope.GetSalesVatRegister = function () {

        $scope.ClearData();
        $scope.DataColl = [];
        $scope.Summary = {};
        if ($scope.SalesVatRegister.ReportType == 2) {
            $scope.gridOptions.columnApi.setColumnVisible('colItem1', false);
            $scope.gridOptions.columnApi.setColumnVisible('colItem2', false);
            $scope.gridOptions.columnApi.setColumnVisible('colItem3', false);
            $scope.gridOptions.columnApi.setColumnVisible('colItem4', false);
        } else {
            $scope.gridOptions.columnApi.setColumnVisible('colItem1', true);
            $scope.gridOptions.columnApi.setColumnVisible('colItem2', true);
            $scope.gridOptions.columnApi.setColumnVisible('colItem3', true);
            $scope.gridOptions.columnApi.setColumnVisible('colItem4', true);
        }

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.SalesVatRegister.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.SalesVatRegister.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.SalesVatRegister.DateToDet)
            dateTo = new Date(($filter('date')($scope.SalesVatRegister.DateToDet.dateAD, 'yyyy-MM-dd')));

        var voucherQry = mx($scope.IncludeVouchers);

        var beData =
        {
            dateFrom: dateFrom,
            dateTo: dateTo,
            VoucherId: ($scope.SalesVatRegister.VoucherId ? $scope.SalesVatRegister.VoucherId : 0),
            BranchId: ($scope.SalesVatRegister.BranchId ? $scope.SalesVatRegister.BranchId : 0),
            ReportType: $scope.SalesVatRegister.ReportType,
            ShowDebitNote: voucherQry.firstOrDefault(p1 => p1.id == 1).IsSelected,
            ShowCreditNote: voucherQry.firstOrDefault(p1 => p1.id == 2).IsSelected,
        };

        $scope.loadingstatus = 'running';
        showPleaseWait();

        $http({
            method: "post",
            url: base_url + "Account/Reporting/GetSalesReturnVatRegister",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                var DataColl = mx(res.data.Data);
                $scope.DataColl = res.data.Data;
                var dt = {
                    VoucherName: 'TOTAL =>',
                    AccessableValue: DataColl.sum(p1 => p1.AccessableValue),
                    VatAmount: DataColl.sum(p1 => p1.VatAmount),
                    InvoiceAmount: DataColl.sum(p1 => p1.InvoiceAmount),
                    ActualQty: DataColl.sum(p1 => p1.ActualQty),
                    BilledQty: DataColl.sum(p1 => p1.BilledQty),
                    ProductAmount: DataColl.sum(p1 => p1.ProductAmount),
                    ProductVatAmount: DataColl.sum(p1 => p1.ProductVatAmount),
                    TaxableSales: DataColl.sum(p1 => p1.TaxableSales),
                    NonTaxableSales: DataColl.sum(p1 => p1.NonTaxableSales),
                    ExportSales: DataColl.sum(p1 => p1.ExportSales),
                    ExciseDuty: DataColl.sum(p1 => p1.ExciseDuty),
                }
                $scope.Summary = dt;
                var filterDataColl = [];
                filterDataColl.push(dt);

                $scope.gridOptionsBottom.api.setRowData(filterDataColl);

                $scope.gridOptions.api.setRowData(res.data.Data);
            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            $scope.loadingstatus = "stop";
            alert('Failed' + reason);
        });

    };


    $scope.Print = function () {

        var sm = ($scope.SalesVatRegister.PeriodOPT == 2 ? $scope.SalesVatRegister.SelectedMonth : null);

        $http({
            method: 'GET',
            url: base_url + "ReportEngine/GetReportTemplates?entityId=" + EntityId + "&voucherId=0&isTran=false",
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
                    var selectedRpt = null;
                    if (templatesColl.length == 1) {
                        selectedRpt = templatesColl[0];
                        rptTranId = templatesColl[0].RptTranId;
                    }
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
                                        selectedRpt = templatesColl[value];
                                        if (rptTranId > 0) {
                                            var dataColl = $scope.GetDataForPrint();
                                            print = true;
                                            $http({
                                                method: 'POST',
                                                url: base_url + "Global/PrintReportData",
                                                headers: { 'Content-Type': undefined },

                                                transformRequest: function (data) {

                                                    var formData = new FormData();
                                                    formData.append("entityId", EntityId);
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
                                                        entityid: EntityId,
                                                        voucherid: 0,
                                                        tranid: 0,
                                                        vouchertype: 0,
                                                        sessionid: res.data.Data.ResponseId,
                                                        Period: $scope.SalesVatRegister.DateFromDet.dateBS + " TO " + $scope.SalesVatRegister.DateToDet.dateBS,
                                                        FYear: (dataColl && dataColl.length > 0 ? dataColl[0].ForYear : ''),
                                                        NY: sm ? sm.NY : 0,
                                                        NM: sm ? sm.NM : 0,
                                                        MonthName: sm ? sm.MonthName : '',
                                                        ForMonth: sm ? sm.MonthName : '',
                                                        FiscalYear: (dataColl && dataColl.length > 0 ? dataColl[0].ForYear : ''),
                                                    };
                                                    var paraQuery = param(rptPara);
                                                    document.body.style.cursor = 'wait';
                                                    if (selectedRpt.IsRDLC == true)
                                                        document.getElementById("frmRpt").src = base_url + "Home/RdlcViewer?" + paraQuery;
                                                    else
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
                        var dataColl = $scope.GetDataForPrint();
                        print = true;

                        $http({
                            method: 'POST',
                            url: base_url + "Global/PrintReportData",
                            headers: { 'Content-Type': undefined },

                            transformRequest: function (data) {

                                var formData = new FormData();
                                formData.append("entityId", EntityId);
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
                                    entityid: EntityId,
                                    voucherid: 0,
                                    tranid: 0,
                                    vouchertype: 0,
                                    sessionid: res.data.Data.ResponseId,
                                    Period: $scope.SalesVatRegister.DateFromDet.dateBS + " TO " + $scope.SalesVatRegister.DateToDet.dateBS,
                                    FYear: (dataColl && dataColl.length > 0 ? dataColl[0].ForYear : ''),
                                    NY: sm ? sm.NY : 0,
                                    NM: sm ? sm.NM : 0,
                                    MonthName: sm ? sm.MonthName : '',
                                    ForMonth: sm ? sm.MonthName : '',
                                    FiscalYear: (dataColl && dataColl.length > 0 ? dataColl[0].ForYear : ''),
                                };
                                var paraQuery = param(rptPara);
                                document.body.style.cursor = 'wait';
                                if (selectedRpt.IsRDLC == true)
                                    document.getElementById("frmRpt").src = base_url + "Home/RdlcViewer?" + paraQuery;
                                else
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

    $scope.GetDataForPrint = function () {

        var filterData = [];

        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
            var dayBook = node.data;
            filterData.push(dayBook);
        });

        return filterData;

    };

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    $scope.DownloadAsXls = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var dataColl = $scope.GetDataForPrint();
        var sm = ($scope.SalesVatRegister.PeriodOPT == 2 ? $scope.SalesVatRegister.SelectedMonth : null);

        var paraData = {
            Period: $scope.SalesVatRegister.DateFromDet.dateBS + " TO " + $scope.SalesVatRegister.DateToDet.dateBS,
            FYear: (dataColl && dataColl.length > 0 ? dataColl[0].ForYear : ''),
            NY: sm ? sm.NY : 0,
            NM: sm ? sm.NM : 0,
            MonthName: sm ? sm.MonthName : '',
            ForMonth: sm ? sm.MonthName : '',
            FiscalYear: (dataColl && dataColl.length > 0 ? dataColl[0].ForYear : ''),
        };

        $http({
            method: 'POST',
            url: base_url + "Global/PrintXlsReportData",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("entityId", EntityId);
                formData.append("jsonData", angular.toJson(data.jsonData));
                formData.append("paraData", angular.toJson(paraData));
                formData.append("RptPath", "");
                return formData;
            },
            data: { jsonData: dataColl }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();
            if (res.data.IsSuccess && res.data.Data) {
                down_file(base_url + "//" + res.data.Data.ResponseId, "SalesReturnRegister.xlsx");
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire(errormessage);
        });
    }

    $scope.validateDate = function (obj, startField, endField, startLabel, endLabel) {
        var res = GlobalServices.validateDate(obj, startField, endField, startLabel, endLabel);
        if (res.IsSuccess == false) {
            Swal.fire({
                icon: 'warning',
                text: res.Message,
                confirmButtonText: 'OK'
            }).then(function () {
                obj.DateFrom_TMP = new Date();
                obj.DateTo_TMP = new Date();
                $scope.$applyAsync();
            });
        }
    };


});
