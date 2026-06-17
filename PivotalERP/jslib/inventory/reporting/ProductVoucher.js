"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller("ProductVoucher", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'ProductVoucher.csv',
            sheetName: 'ProductVoucher'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    function LoadData() {
        $('.select2').select2({
            allowClear: true,
            openOnEnter: true
        });


        $scope.FixedProductConfig = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetFixedProductConfig",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.FixedProductConfig = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

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

        $scope.GodownList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetUserWiseGodown",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.GodownList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
         

        //Search Drop DownList
        $scope.VoucherSearchOptions = [{ text: 'ProductName', value: 'ProductName', dataType: 'Text' },
        { text: 'PartNo', value: 'PartNo', dataType: 'Number' },
        { text: 'Code', value: 'Code', dataType: 'Number' },
        { text: 'Group Name', value: 'GroupName', dataType: 'Text' },
        { text: 'CategoriesName', value: 'CategoriesName', dataType: 'text' },
        { text: 'SalesQty', value: 'OutQty', dataType: 'Number' },
        { text: 'SalesQty(AI.Value1)', value: 'OutQty1', dataType: 'Number' },
        { text: 'SalesQty(AI.Value2)', value: 'OutQty2', dataType: 'Number' },
        { text: 'Sales Rate', value: 'OutRate', dataType: 'Number' },
        { text: 'SalesAmt', value: 'OutAmt', dataType: 'Number' },
        { text: 'Return Qty', value: 'InQty', dataType: 'Number' },
        { text: 'Return Qty(AI.Value1)', value: 'InQty1', dataType: 'Number' },
        { text: 'Return Qty(AI.Value2)', value: 'InQty2', dataType: 'Number' },
        { text: 'Return rate', value: 'InRate', dataType: 'Number' },
        { text: 'Return Amt', value: 'InAmt', dataType: 'Number' },
        { text: 'Net Sales Qty', value: 'BalanceQty', dataType: 'Number' },
        { text: 'Unit', value: 'Unit', dataType: 'text' },
        { text: 'Net Qty(AI.Value1)', value: 'NetQty1', dataType: 'Number' },
        { text: 'Net Qty(AI.Value2)', value: 'NetQty2', dataType: 'Number' },
        { text: 'Net Sales Rate', value: 'BalanceRate', dataType: 'Number' },
        { text: 'Net Sales Amt', value: 'BalanceAmt', dataType: 'Number' },
        { text: 'ProductType', value: 'ProductType', dataType: 'text' },
        { text: 'Brand', value: 'Brand', dataType: 'text' },
        { text: 'Division', value: 'Division', dataType: 'text' },
        { text: 'Color', value: 'Color', dataType: 'text' },
        { text: 'Flavour', value: 'Flavour', dataType: 'text' },
        { text: 'Shape', value: 'Shape', dataType: 'text' },
        ];

        //Filter Dialog Box Details 
        $scope.BranchTypeColl = [];
        $scope.VoucherTypeColl = [];
        $scope.LedgerGroupTypeColl = [];
        $scope.ExpressionColl = GlobalServices.getExpression();
        $scope.ConditionColl = GlobalServices.getLogicCondition();
        $scope.FilterColumnColl = [{ text: 'Opening', value: 'Opening', dataType: 'Number' },
        { text: 'Opening Dr', value: 'OpeningDr', dataType: 'Number' },
        { text: 'Opening Cr', value: 'OpeningCr', dataType: 'Number' },
        { text: 'Total Opening Dr', value: 'TotalOpeningDr', dataType: 'Number' },
        { text: 'TotalOpening Cr', value: 'TotalOpeningCr', dataType: 'Number' },
        { text: 'Transaction', value: 'Transaction', dataType: 'Number' },
        { text: 'Transaction Dr', value: 'TransactionDr', dataType: 'Number' },
        { text: 'Transaction Cr', value: 'TransactionCr', dataType: 'Number' },
        { text: 'Closing', value: 'Closing', dataType: 'Number' },
        { text: 'Closing Dr', value: 'ClosingDr', dataType: 'Number' },
        { text: 'Closing Cr', value: 'ClosingCr', dataType: 'Number' },
        { text: 'LedgerName', value: 'LedgerName', dataType: 'text' },];

        

        $scope.ProductVoucher = {            
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            ProductId: 0,
            BatchWise:false,
        };

        $timeout(function ()
        {
            GlobalServices.getCompanyDet().then(function (res)
            {
                var comDet = res.data.Data;
                if (comDet) {
                    $scope.ProductVoucher.DateFrom_TMP = new Date(comDet.StartDate);
                }
            }, function (errormessage) {
                alert('Unable to Delete data. pls try again.' + errormessage.responseText);
            });
        });

        $scope.loadingstatus = "stop";

        $scope.columnDefs = [
            {
                headerName: "Date", width: 130, dataType: 'DateTime', cellRenderer: 'agGroupCellRenderer', field: "VoucherDate",
                valueFormatter: function (params) { return DateFormatAD(params.value); },
                filter: 'agDateColumnFilter', pinned: 'left'
            },
            {
                headerName: "Miti", width: 120, dataType: 'DateTime', valueGetter: function (params) {
                    var beData = params.data;

                    if (beData.VoucherDateNP && beData.VoucherDateNP.length > 0)
                        return beData.VoucherDateNP;

                    return DateFormatBS(beData.NY, beData.NM, beData.ND);
                    //return DateFormatBS(params.data.NY, params.data.NM, params.data.ND);
                },
                filter: 'agTextColumnFilter', pinned: 'left'
            },
            { headerName: "Voucher No.", width: 130, dataType: 'Number', field: "AutoManualNo", filter: 'agTextColumnFilter', pinned: 'left', },
            { headerName: "Ref.No.", width: 120, dataType: 'Number', field: "RefNo", filter: 'agTextColumnFilter', pinned: 'left', },
            {
                headerName: "Particular's", dataType: 'Text', width: 190,  pinned: 'left',
                valueGetter: function (params) {
                    var beData = params.data;

                    if (beData.VoucherType == "StockTransfor")
                        return beData.VoucherLedger;
                    else if (beData.VoucherType == "StockJournal")
                        return beData.GodownName;
                    else if (beData.VoucherType == "Consumption") {
                        return beData.PartyLedger + " - " + beData.GodownName;
                    }
                    else
                        return params.data.PartyLedger;
                },
                filter: 'agTextColumnFilter',
            },
            { headerName: "VoucherType", dataType: 'Text', width: 150, field: "VoucherName", filter: 'agTextColumnFilter', },
            
            {
                headerName: "InQty", width: 150, dataType: 'Number', filter: "agNumberColumnFilter", field: "InQty",

                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "InRate", width: 150, dataType: 'Number', filter: "agNumberColumnFilter", field: "InRate",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "InAmt", width: 150, dataType: 'Number', filter: "agNumberColumnFilter", field: "InAmt",

                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "OutQty", width: 150, dataType: 'Number', filter: "agNumberColumnFilter", field: "OutQty",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "OutRate", width: 150, dataType: 'Number', filter: "agNumberColumnFilter", field: "OutRate",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "OutAmt", width: 150, dataType: 'Number', filter: "agNumberColumnFilter", field: "OutAmt",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "Balance Qty.", dataType: 'Number', width: 150, filter: "agNumberColumnFilter", field: "BalanceQty",

                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            { headerName: "Unit", width: 120, dataType: 'Text', field: "Unit" },
            {
                headerName: "Balance Rate", field:"BalanceRate", width: 150, filter: "agNumberColumnFilter",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "Balance Amt.", dataType: 'Number', width: 150, filter: "agNumberColumnFilter", field: "BalanceAmt",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            { headerName: "Batch No.", width: 140, colId: 'batDet1', field: "Batch", cellStyle: { 'text-align': 'left' }, hide: true, },
            { headerName: "MFG Date", width: 140, colId: 'batDet2', field: "MFGDate", cellStyle: { 'text-align': 'center' }, hide: true,  },
            { headerName: "EXP Date", width: 140, colId: 'batDet3', field: "EXPDate", cellStyle: { 'text-align': 'center' }, hide: true,  },
            { headerName: "Engine No.", width: 140, colId: 'batDet4', field: "EngineNo", cellStyle: { 'text-align': 'left' }, hide: true,  },
            { headerName: "CostClass", dataType: 'Text', width: 120, field: "CostClassName" },
            { headerName: "Narration", dataType: 'Text', width: 120, field: "EachNarration" },
            { headerName: "User", dataType: 'Text', width: 120, field: "UserName" },
        ];


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
            columnDefs: $scope.columnDefs,
            rowData: null,
            filter: true,
            suppressHorizontalScroll: true,
            alignedGrids: [],
            enableFilter: true,

            onFilterChanged: function () {
                var dt = {
                    InQty: 0,
                    InAmt: 0,
                    InRate: 0,
                    OutQty: 0,
                    OutRate: 0,
                    OutAmt: 0,
                    BalanceQty: 0,
                    BalanceRate: 0,
                    BalanceAmt:0
                }

                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var fData = node.data;
                    dt.InQty += fData.InQty;
                    dt.InAmt += fData.InAmt;
                    dt.OutQty += fData.OutQty;
                    dt.OutAmt += fData.OutAmt;
                    dt.BalanceQty += fData.BalanceQty;
                    dt.BalanceAmt += fData.BalanceAmt;
                });
                 
                $scope.dataForBottomGrid[0].InQty = dt.InQty;
                $scope.dataForBottomGrid[0].InAmt = dt.InAmt;
                $scope.dataForBottomGrid[0].InRate = dt.InRate;
                $scope.dataForBottomGrid[0].OutQty = dt.OutQty;
                $scope.dataForBottomGrid[0].OutRate = dt.OutRate;
                $scope.dataForBottomGrid[0].OutAmt = dt.OutAmt;
                $scope.dataForBottomGrid[0].BalanceQty = dt.BalanceQty;
                $scope.dataForBottomGrid[0].BalanceRate = dt.BalanceRate;
                $scope.dataForBottomGrid[0].BalanceAmt = dt.BalanceAmt;
                $scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);
            }

        };
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);

        $scope.dataForBottomGrid = [
            {
                AutoNumber: '',
                Name: 'Total =>',
                InAmount: 0,
                Rate: '',
            }];

        $scope.gridOptionsBottom = {
            defaultColDef: {
                resizable: true,
                width: 90
            },
            columnDefs: $scope.columnDefs,
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


    }

    $scope.ShowBatchDetails = function (val)
    {
        for (var i = 1; i < 5; i++) {
            var colName = 'batDet' + i.toString();
            $scope.gridOptions.columnApi.setColumnVisible(colName, val);
        }
    }

    $scope.ClearData = function () {
        $scope.dayBook = {};

        $scope.dayBook.InQty = 0;
        $scope.dayBook.InAmt = 0;
        $scope.dayBook.InRate = 0;
        $scope.dayBook.OutQty = 0;
        $scope.dayBook.OutRate = 0;
        $scope.dayBook.OutAmt = 0;
        $scope.dayBook.BalanceQty = 0;
        $scope.dayBook.BalanceRate = 0;
        $scope.dayBook.BalanceAmt = 0;

        $scope.dataForBottomGrid[0].InQty = 0;
        $scope.dataForBottomGrid[0].InAmt = 0;
        $scope.dataForBottomGrid[0].InRate = 0;
        $scope.dataForBottomGrid[0].OutQty = 0;
        $scope.dataForBottomGrid[0].OutRate = 0;
        $scope.dataForBottomGrid[0].OutAmt = 0;
        $scope.dataForBottomGrid[0].BalanceQty = 0;
        $scope.dataForBottomGrid[0].BalanceRate = 0;
        $scope.dataForBottomGrid[0].BalanceAmt = 0;
         
        $scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);

        $scope.DataColl = [];
        $scope.gridOptions.api.setRowData($scope.DataColl);
    };

    $scope.GetProductVoucher = function () {

        $scope.ClearData();

        if (!$scope.ProductVoucher.ProductId || $scope.ProductVoucher.ProductId == 0)
            return;


        var dateFrom =$filter('date')(new Date(), 'yyyy-MM-dd');
        var dateTo = $filter('date')(new Date(), 'yyyy-MM-dd');

        if ($scope.ProductVoucher.DateFromDet)
            dateFrom =$filter('date')($scope.ProductVoucher.DateFromDet.dateAD, 'yyyy-MM-dd');

        if ($scope.ProductVoucher.DateToDet)
            dateTo = $filter('date')($scope.ProductVoucher.DateToDet.dateAD, 'yyyy-MM-dd');

        $scope.loadingstatus = 'running';
        showPleaseWait();


        var beData = {            
            dateFrom: dateFrom,
            dateTo: dateTo,
            productId: $scope.ProductVoucher.ProductId,
            godownIdColl: $scope.ProductVoucher.GodownId,
            BatchColl: $scope.ProductVoucher.BatchColl,
            EngineNoColl:$scope.ProductVoucher.EngineNoColl
        }; 
        $http({
            method: "POST",
            url: base_url + "Inventory/Reporting/GetProductVoucher",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            var dt = res.data.Data;
            $scope.dataForBottomGrid[0].InQty = dt.InQty;
            $scope.dataForBottomGrid[0].InAmt = dt.InAmt;
            $scope.dataForBottomGrid[0].InRate = dt.InRate;
            $scope.dataForBottomGrid[0].OutQty = dt.OutQty;
            $scope.dataForBottomGrid[0].OutRate = dt.OutRate;
            $scope.dataForBottomGrid[0].OutAmt = dt.OutAmt;
            $scope.dataForBottomGrid[0].BalanceQty = dt.BalanceQty;
            $scope.dataForBottomGrid[0].BalanceRate = dt.BalanceRate;
            $scope.dataForBottomGrid[0].BalanceAmt = dt.BalanceAmt;

            $scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);

            $scope.DataColl = res.data.Data.DataColl;
            $scope.gridOptions.api.setRowData($scope.DataColl);

            $scope.loadingstatus = 'stop';
            hidePleaseWait();

             

        }, function (reason) {
            $scope.loadingstatus = "stop";
            alert('Failed' + reason);
        });


    };

   

    $scope.Print = function () {
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
                                                        Period: $scope.ProductVoucher.DateFromDet.dateBS + " TO " + $scope.ProductVoucher.DateToDet.dateBS, 
                                                        Product: $scope.ProductVoucher.ProductDetail.Name, 
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
                                    Period: $scope.ProductVoucher.DateFromDet.dateBS + " TO " + $scope.ProductVoucher.DateToDet.dateBS,
                                    Product: $scope.ProductVoucher.ProductDetail.Name,
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

    $scope.GetDataForPrint = function () {
         
        var filterData = [];
        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
            var fData = node.data;

            fData.VoucherDateStr = DateFormatAD(fData.VoucherDate);
            fData.VoucherDateNP = DateFormatBS(fData.NY, fData.NM, fData.ND);
            filterData.push(fData);

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

        var paraData = {
            Period: $scope.ProductVoucher.DateFromDet.dateBS + " TO " + $scope.ProductVoucher.DateToDet.dateBS,
            Product: $scope.ProductVoucher.ProductDetail.Name,
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
                down_file(base_url + "//" + res.data.Data.ResponseId, "ProductVoucher.xlsx");
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire(errormessage);
        });
    }

    $scope.validateDates = function (changedField, obj) {
        if (!obj.DateFromDet || !obj.DateToDet ||
            !obj.DateFromDet.dateAD || !obj.DateToDet.dateAD) {
            return true;
        }

        var fromDate = $filter('date')(new Date(obj.DateFromDet.dateAD), 'yyyy-MM-dd');
        var toDate = $filter('date')(new Date(obj.DateToDet.dateAD), 'yyyy-MM-dd');

        if (!fromDate || !toDate) return true;

        if (fromDate > toDate) {
            if (changedField === 'fromDate') {
                Swal.fire({
                    icon: 'warning',
                    text: 'From Date cannot be After To Date.',
                    confirmButtonText: 'OK'
                }).then(function () {
                    $scope.$apply(function () {
                        obj.DateFrom_TMP = new Date();
                        obj.DateFromDet = new Date();
                    });
                });
            } else if (changedField === 'toDate') {
                Swal.fire({
                    icon: 'warning',
                    text: 'To Date cannot be Before From Date.',
                    confirmButtonText: 'OK'
                }).then(function () {
                    $scope.$apply(function () {
                        obj.DateTo_TMP = new Date();
                        obj.DateToDet = new Date();
                    });
                });
            }
            return false;
        }
        return true;
    };

});
