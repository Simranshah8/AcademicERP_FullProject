"use strict";

//const { data } = require("jquery");

agGrid.initialiseAgGridWithAngular1(angular);

app.controller("PurchaseCostingVoucherWise", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {
 

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'PurchaseCostingVoucherWise.csv',
            sheetName: 'PurchaseCostingVoucherWise'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    $scope.LoadData=function()
    {
        $('.select2').select2({
            allowClear: true,
            openOnEnter: true
        });
        $scope.ReportTypeColl = [{ text: 'PendingOnly', value: 'PendingOnly', dataType: 'text' }, { text: 'ClearOnly', value: 'ClearOnly', dataType: 'text' }, { text: 'Both', value: 'Both', dataType: 'text' },]

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


        $scope.PurchaseCostingVoucherWise = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            VoucherId: 0,
            IsPost: true,
            BranchId: 0,
            LedgerWise: false,
            ColumnarView:true,
        };
       
        $scope.loadingstatus = "stop";

        $scope.columnDefs = [
            {
                headerName: "Date", width: 130, pinned: 'left', dataType: 'DateTime', field: "VoucherDate", cellRenderer: 'agGroupCellRenderer',
                valueFormatter: function (params) { return DateFormatAD(params.value); },
                showRowGroup: true, cellStyle: { 'text-align': 'center' },
                cellRendererParams: {
                    suppressCount: false, // turn off the row count                   
                }
            },
            {
                headerName: "Miti", width: 130, pinned: 'left', dataType: 'DateTime',  field: "VoucherDateBS", cellRenderer: 'agGroupCellRenderer',
                valueFormatter: function (params) { return DateFormatBS(params.value); },
                showRowGroup: true, cellStyle: { 'text-align': 'center' },
                cellRendererParams: {
                    suppressCount: false, // turn off the row count                   
                }
            },
            { headerName: "RefNo", width: 150, pinned: 'left', field: "RefNo", dataType: 'Number', cellStyle: { 'text-align': 'center' } },

            { headerName: "VoucherNo", width: 180, field: "VoucherNo", dataType: 'Number', cellStyle: { 'text-align': 'center' } },
            { headerName: "PartyName", width: 250, field: "PartyName", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Address", width: 180, field: "Address", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "InvoiceAmt", width: 180, field: "InvoiceAmt", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Product", width: 180, field: "ProductName", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Qty", width: 140, field: "Qty", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); },  },
            { headerName: "Rate", width: 150, field: "Rate", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Amount", width: 180, field: "Amount", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); },  },
            { headerName: "Sub Ledger", width: 180, field: "CostLedger", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "AdditionalCost", width: 180, field: "CostAmt", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            
            { headerName: "Total", width: 180, field: "TotalAmount", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Cost Rate", width: 180, field: "CostRate", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            
           

            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol1',hide:true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol1;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol2', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol2;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol3', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol3;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol4', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol4;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol5', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol5;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol6', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol6;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol7', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol7;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol8', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol8;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol9', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol9;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol10', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol10;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol11', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol11;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol12', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol12;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol13', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol12;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol14', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol14;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol15', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol15;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol16', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol16;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol17', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol17;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol18', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol18;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol19', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol19;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: 'costCol', width: 140, dataType: 'Number', filter: "agNumberColumnFilter", colId: 'costCol20', hide: true,
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.costCol20;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },

            { headerName: "ProductGroup", width: 180, dataType: 'Text', field: "ProductGroup", cellStyle: { 'text-align': 'left' } },
            { headerName: "PartyGroup", width: 180, field: "PartyGroup", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Narration", width: 180, field: "Narration", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "JournalVoucherNo", width: 180, field: "JVVoucherNo", dataType: 'Number', cellStyle: { 'text-align': 'right' } },
            { headerName: "CurrencyName", width: 180, field: "CurrencyName", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "CurrencyRate", width: 180, field: "CurrencyRate", dataType: 'Number', cellStyle: { 'text-align': 'right' } },
            { headerName: "ProductModel", width: 150, field: "ProductModel", dataType: 'Text', cellStyle: { 'text-align': 'left' }, hide: true, },
            { headerName: "EngineNo", width: 150, field: "EngineNo", dataType: 'Text', cellStyle: { 'text-align': 'left' }, hide: true, },
            { headerName: "ChassisNo", width: 150, field: "ChassisNo", dataType: 'Text', cellStyle: { 'text-align': 'left' }, hide: true, },


        ];


        $scope.ReGenerateGrid();
       // $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
       // new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);

        $scope.dataForBottomGrid = [
            {
                PartyName: 'Total =>',
                Qty: 0,
                Amount: 0,
                InvoiceAmt:0
            }];

        $scope.gridOptionsBottom = {
            defaultColDef: {
                resizable: true,
                width: 90
            },
            rowHeight:30,
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
    $scope.ReGenerateGrid = function () {
        $scope.gridOptions = {

            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                width: 100,


            },
            headerHeight: 31,
            rowHeight: 30,
            enableSorting: true,
            multiSortKey: 'ctrl',
            enableColResize: true,
            overlayLoadingTemplate: "Loading..",
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
                    PartyName: 'Total =>',
                    Qty: 0,
                    Amount: 0,
                    InvoiceAmt: 0,
                    TotalAmount: 0,
                    CostAmt: 0,
                    costCol1: 0,
                    costCol2: 0,
                    costCol3: 0,
                    costCol4: 0,
                    costCol5: 0,
                    costCol6: 0,
                    costCol7: 0,
                    costCol8: 0,
                    costCol9: 0,
                    costCol10: 0,
                    costCol11: 0,
                    costCol12: 0,
                    costCol13: 0,
                    costCol14: 0,
                    costCol15: 0,
                    costCol16: 0,
                    costCol17: 0,
                    costCol18: 0,
                    costCol19: 0,
                    costCol20: 0,
                }
                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var fData = node.data;
                    dt.Qty += fData.Qty;
                    dt.Amount += fData.Amount;
                    dt.InvoiceAmt += fData.InvoiceAmt;
                    dt.TotalAmount += fData.TotalAmount;
                    dt.CostAmt += fData.CostAmt;
                    dt.costCol1 += isEmptyNum(fData.costCol1);
                    dt.costCol2 += isEmptyNum(fData.costCol2);
                    dt.costCol3 += isEmptyNum(fData.costCol3);
                    dt.costCol4 += isEmptyNum(fData.costCol4);
                    dt.costCol5 += isEmptyNum(fData.costCol5);
                    dt.costCol6 += isEmptyNum(fData.costCol6);
                    dt.costCol7 += isEmptyNum(fData.costCol7);
                    dt.costCol8 += isEmptyNum(fData.costCol8);
                    dt.costCol9 += isEmptyNum(fData.costCol9);
                    dt.costCol10 += isEmptyNum(fData.costCol10);
                    dt.costCol11+= isEmptyNum(fData.costCol11);
                    dt.costCol12 += isEmptyNum(fData.costCol12);
                    dt.costCol13 += isEmptyNum(fData.costCol13);
                    dt.costCol14 += isEmptyNum(fData.costCol14);
                    dt.costCol15 += isEmptyNum(fData.costCol15);
                    dt.costCol16 += isEmptyNum(fData.costCol16);
                    dt.costCol17 += isEmptyNum(fData.costCol17);
                    dt.costCol18 += isEmptyNum(fData.costCol18);
                    dt.costCol19 += isEmptyNum(fData.costCol19);
                    dt.costCol20 += isEmptyNum(fData.costCol20);
                });
                var filterDataColl = [];
                filterDataColl.push(dt);
                $scope.gridOptionsBottom.api.setRowData(filterDataColl);
            }

        };
    }
    $scope.ClearData = function () {

        for (var c = 1; c <=20; c++) {
            var colName = 'costCol' + c;
            $scope.gridOptions.columnApi.setColumnVisible(colName, false);
        }

        if ($scope.FixedProductConfig.ShowEngineNo==true) {
            $scope.gridOptions.columnApi.setColumnVisible('EngineNo', true);
            $scope.gridOptions.api.getColumnDef('EngineNo').headerName = $scope.FixedProductConfig.EngineNo;
            $scope.gridOptions.columnApi.getColumn('EngineNo').colDef.headerName = $scope.FixedProductConfig.EngineNo;
        } else
            $scope.gridOptions.columnApi.setColumnVisible('EngineNo', false);


        if ($scope.FixedProductConfig.ShowChassisNo == true) {
            $scope.gridOptions.columnApi.setColumnVisible('ChassisNo', true);
            $scope.gridOptions.api.getColumnDef('ChassisNo').headerName = $scope.FixedProductConfig.ChassisNo;
            $scope.gridOptions.columnApi.getColumn('ChassisNo').colDef.headerName = $scope.FixedProductConfig.ChassisNo;
        } else
            $scope.gridOptions.columnApi.setColumnVisible('ChassisNo', false);

        if ($scope.FixedProductConfig.ShowModel == true) {
            $scope.gridOptions.columnApi.setColumnVisible('ProductModel', true);
            $scope.gridOptions.api.getColumnDef('ProductModel').headerName = $scope.FixedProductConfig.Model;
            $scope.gridOptions.columnApi.getColumn('ProductModel').colDef.headerName = $scope.FixedProductConfig.Model;
        } else
            $scope.gridOptions.columnApi.setColumnVisible('ProductModel', false);



        var DataColl = [];
        $scope.gridOptionsBottom.api.setRowData(DataColl);

        $scope.gridOptions.api.setRowData(DataColl);
    };
    $scope.GetPurchaseCostingVoucherWise = function () {

        $scope.ClearData();
        var dateFrom =$filter('date')(new Date(), 'yyyy-MM-dd');
        var dateTo =$filter('date')(new Date(), 'yyyy-MM-dd');

        if ($scope.PurchaseCostingVoucherWise.DateFromDet)
            dateFrom =$filter('date')($scope.PurchaseCostingVoucherWise.DateFromDet.dateAD, 'yyyy-MM-dd');

        if ($scope.PurchaseCostingVoucherWise.DateToDet)
            dateTo = $filter('date')($scope.PurchaseCostingVoucherWise.DateToDet.dateAD, 'yyyy-MM-dd');

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var beData = {
            DateFrom: dateFrom,
            DateTo: dateTo,
            ledgerWise:$scope.PurchaseCostingVoucherWise.LedgerWise,
            VoucherType: $scope.PurchaseCostingVoucherWise.VoucherId,
            isPost: $scope.PurchaseCostingVoucherWise.IsPost,
            branchId: $scope.PurchaseCostingVoucherWise.BranchId
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Inventory/Reporting/GetPurchaseCostingVoucherWise",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                var DataColl = mx(res.data.Data);

               // var query = DataColl.groupBy(p1 => p1.CostLedger);

                
                var totalInvoiceAmt = 0;
                var totalQty = 0;
                var totalAmt = 0;
                var totalCostAmt = 0;
                var totalAmtSum = 0;
                
                 
                if ($scope.PurchaseCostingVoucherWise.ColumnarView == true) {
                    var costLedQry = DataColl.groupBy(p1 => p1.CostLedger);
                    var colValPro = {};
                    var colSno = 1;
                    angular.forEach(costLedQry, function (cq) {
                        var colName = 'costCol' + colSno;
                        $scope.gridOptions.columnApi.setColumnVisible(colName, true);
                        $scope.gridOptions.api.getColumnDef(colName).headerName = cq.key;
                        $scope.gridOptions.columnApi.getColumn(colName).colDef.headerName = cq.key;
                        colValPro[colName] = cq.key;
                        colSno++;
                    });

                    colSno--;

                    var tmpDCColl = [];
                    var groupTran = DataColl.groupBy(p1 => ({ TranId: p1.TranId, AllocationId: p1.ItemAllocationId,EngineNo:p1.EngineNo }));
                    angular.forEach(groupTran, function (q)
                    {
                        var fst = q.elements[0];
                        var cQry = mx(q.elements);
                        //var curQty = cQry.sum(p1 => p1.Qty);
                        //var curAmt = cQry.sum(p1 => p1.Amount);
                        var curQty = fst.Qty;
                        var curAmt = fst.Amount;
                        var curRate = 0;

                        if (curQty != 0 && curAmt != 0)
                            curRate = curAmt / curQty;

                        totalQty += curQty;
                        totalAmt += curAmt;
                        var newR = {
                            TranId: fst.TranId,
                            VoucherNo: fst.VoucherNo,
                            VoucherDate: fst.VoucherDate,
                            VoucherDateBS: fst.VoucherDateBS,
                            RefNo: fst.RefNo,
                            PartyName: fst.PartyName,
                            PartyCode: fst.PartyCode,
                            Address: fst.Address,
                            Narration: fst.Narration,
                            ProductName: fst.ProductName,
                            Alias: fst.Alias,
                            Code: fst.Code,
                            ProductGroup: fst.ProductGroup,
                            Qty: curQty,
                            Rate: curRate,
                            Amount: curAmt,
                            CostAmt: cQry.sum(p1 => p1.CostAmt),
                            InvoiceAmt: fst.InvoiceAmt,
                            VoucherName: fst.VoucherName,
                            Branch: fst.Branch,
                            PartyGroup: fst.PartyGroup,
                            VoucherId: fst.VoucherId,
                            CostClassId: fst.CostClassId,
                            JVTranId: fst.JVTranId,
                            JVVoucherNo: fst.JVVoucherNo,
                            CurrencyName: fst.CurrencyName,
                            CurrencyRate: fst.CurrencyRate,
                            TotalAmount: cQry.sum(p1 => p1.TotalAmount),
                            CostRate: 0,
                            ProductModel: fst.ProductModel,
                            EngineNo: fst.EngineNo,
                            ChassisNo: fst.ChassisNo,
                            CostCenter: '',
                            ItemAllocationId: fst.ItemAllocationId,
                        };
                        newR.CostRate = (newR.Amount + newR.CostAmt)/newR.Qty;

                        
                        cQry.forEach(function (cq) {                            
                            for (var col = 1; col <= colSno; col++) {
                                var colName = 'costCol' + col;
                                if (colValPro[colName] == cq.CostLedger) {
                                    newR[colName] = cq.CostAmt;
                                }
                            }                           
                        });
                        totalCostAmt += newR.CostAmt;
                        totalAmtSum += newR.TotalAmount;

                        tmpDCColl.push(newR);

                    })

                    var dt = {
                        PartyName: 'Total =>',
                        Qty: 0,
                        Amount: 0,
                        InvoiceAmt: 0,
                        TotalAmount: 0,
                        CostAmt: 0,
                        costCol1: 0,
                        costCol2: 0,
                        costCol3: 0,
                        costCol4: 0,
                        costCol5: 0,
                        costCol6: 0,
                        costCol7: 0,
                        costCol8: 0,
                        costCol9: 0,
                        costCol10: 0,
                        costCol11: 0,
                        costCol12: 0,
                        costCol13: 0,
                        costCol14: 0,
                        costCol15: 0,
                        costCol16: 0,
                        costCol17: 0,
                        costCol18: 0,
                        costCol19: 0,
                        costCol20: 0,
                    }
                    tmpDCColl.forEach(function (fData) {                        
                        dt.Qty += fData.Qty;
                        dt.Amount += fData.Amount;
                        dt.InvoiceAmt += fData.InvoiceAmt;
                        dt.TotalAmount += fData.TotalAmount;
                        dt.CostAmt += fData.CostAmt;
                        dt.costCol1 += isEmptyNum(fData.costCol1);
                        dt.costCol2 += isEmptyNum(fData.costCol2);
                        dt.costCol3 += isEmptyNum(fData.costCol3);
                        dt.costCol4 += isEmptyNum(fData.costCol4);
                        dt.costCol5 += isEmptyNum(fData.costCol5);
                        dt.costCol6 += isEmptyNum(fData.costCol6);
                        dt.costCol7 += isEmptyNum(fData.costCol7);
                        dt.costCol8 += isEmptyNum(fData.costCol8);
                        dt.costCol9 += isEmptyNum(fData.costCol9);
                        dt.costCol10 += isEmptyNum(fData.costCol10);
                        dt.costCol11 += isEmptyNum(fData.costCol11);
                        dt.costCol12 += isEmptyNum(fData.costCol12);
                        dt.costCol13 += isEmptyNum(fData.costCol13);
                        dt.costCol14 += isEmptyNum(fData.costCol14);
                        dt.costCol15 += isEmptyNum(fData.costCol15);
                        dt.costCol16 += isEmptyNum(fData.costCol16);
                        dt.costCol17 += isEmptyNum(fData.costCol17);
                        dt.costCol18 += isEmptyNum(fData.costCol18);
                        dt.costCol19 += isEmptyNum(fData.costCol19);
                        dt.costCol20 += isEmptyNum(fData.costCol20);
                    });
                    var filterDataColl = [];
                    filterDataColl.push(dt);
                    $scope.gridOptionsBottom.api.setRowData(filterDataColl);

                    $scope.gridOptions.api.setRowData(tmpDCColl);
                               
                    
                } else {
                    var dt = {
                        PartyName: 'TOTAL =>',
                        Qty: DataColl.sum(p1 => p1.Qty),
                        Amount: DataColl.sum(p1 => p1.Amount),
                        InvoiceAmt: totalInvoiceAmt,
                        CostAmt: DataColl.sum(p1 => p1.CostAmt),
                        TotalAmount: DataColl.sum(p1 => p1.TotalAmount),
                    }

                    var filterDataColl = [];
                    filterDataColl.push(dt);

                    $scope.gridOptionsBottom.api.setRowData(filterDataColl);

                    $scope.gridOptions.api.setRowData(res.data.Data);
                }

               
            } else
                alert(res.data.ResponseMSG);

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
                            title: 'Report  For Print',
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

                                                    document.body.style.cursor = 'wait';
                                                    document.getElementById("frmRpt").src = '';
                                                    document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + EntityId + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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

                                document.body.style.cursor = 'wait';
                                document.getElementById("frmRpt").src = '';
                                document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + EntityId + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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

    $scope.UpdateCosting = function () {

        $scope.ClearData();
        var dateFrom = $filter('date')(new Date(), 'yyyy-MM-dd');
        var dateTo = $filter('date')(new Date(), 'yyyy-MM-dd');

        if ($scope.PurchaseCostingVoucherWise.DateFromDet)
            dateFrom = $filter('date')($scope.PurchaseCostingVoucherWise.DateFromDet.dateAD, 'yyyy-MM-dd');

        if ($scope.PurchaseCostingVoucherWise.DateToDet)
            dateTo = $filter('date')($scope.PurchaseCostingVoucherWise.DateToDet.dateAD, 'yyyy-MM-dd');

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var beData = {
            datefrom: dateFrom,
            dateTo: dateTo,      
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Inventory/Reporting/UpdateCostInProductVoucher",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            $scope.loadingstatus = 'stop';
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
             

        }, function (reason) {
            $scope.loadingstatus = "stop";
            alert('Failed' + reason);
        });

    };

    $scope.GetDataForPrint = function () {

        var filterData = [];

        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
            var dayBook = node.data;
            filterData.push(dayBook);
        });

        return filterData;
    }
    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }
});
