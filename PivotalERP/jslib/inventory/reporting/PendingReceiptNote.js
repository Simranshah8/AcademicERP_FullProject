"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller("PendingReceiptNote", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'PendingReceiptNote.csv',
            sheetName: 'PendingReceiptNote'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    function LoadData() {
        $('.select2').select2({
            allowClear: true,
            openOnEnter: true
        });
        $scope.ReportTypeColl = [{ text: 'PendingOnly', value: 'PendingOnly', dataType: 'text' }, { text: 'ClearOnly', value: 'ClearOnly', dataType: 'text' }, { text: 'Both', value: 'Both', dataType: 'text' },]


        $scope.PendingReceiptNote = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            VoucherId: 0,
            IsPost: true,
            BranchId: 0
        };


        $scope.loadingstatus = "stop";

        $scope.columnDefs = [
            {
                headerName: "Date(A.D.)", width: 140, field: "VoucherDate", pinned: 'left', dataType: 'DateTime', cellRenderer: 'agGroupCellRenderer',
                valueFormatter: function (params) { return DateFormatAD(params.value); },
                showRowGroup: true, cellStyle: { 'text-align': 'center' },
                cellRendererParams: {
                    suppressCount: false, // turn off the row count                   
                }
            },
            {
                headerName: "Miti", width: 140, field: "VoucherDateBS", pinned: 'left', dataType: 'DateTime', cellRenderer: 'agGroupCellRenderer',
                valueFormatter: function (params) { return DateFormatBS(params.value); },
                showRowGroup: true, cellStyle: { 'text-align': 'center' },
                cellRendererParams: {
                    suppressCount: false, // turn off the row count                   
                }
            },
            { headerName: "Particulars", width: 120, field: "Name", pinned: 'left', dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Code", width: 120, field: "Code", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Alias", width: 120, field: "Alias", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "ReceivedNo", width: 180, field: "ReceivedCancelNo", dataType: 'Text', cellStyle: { 'text-align': 'center' } },
            { headerName: "ReceivedQty", width: 180, field: "ReceivedQty", dataType: 'Number', cellStyle: { 'text-align': 'center' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Rate", width: 120, field: "Rate", dataType: 'Number', cellStyle: { 'text-align': 'right' } },
            { headerName: "Amount", width: 120, field: "Amount", dataType: 'Number', cellStyle: { 'text-align': 'right' } },
            { headerName: "Purchase_Date", width: 180, field: "PurchaseDate", dataType: 'DateTime', cellStyle: { 'text-align': 'left' } },
            { headerName: "InvoiceNo", width: 160, field: "InvoiceNo", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Purchase Qty", width: 180, field: "PurchaseQty", dataType: 'Number', cellStyle: { 'text-align': 'center' } },
            { headerName: "Pending Qty", width: 180, field: "PendingQty", dataType: 'Number', cellStyle: { 'text-align': 'center' } },
            { headerName: "Party", width: 120, field: "Party", dataType: 'Text', cellStyle: { 'text-align': 'left' } },

            { headerName: "Purchase Details", width: 180, field: "PurchaseDetails", dataType: 'Text', cellStyle: { 'text-align': 'left' } },

            { headerName: "Received ReturnDetails", width: 200, field: "ReceivedReturnDetails", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Return No", width: 180, field: "ReturnNo", dataType: 'Text', cellStyle: { 'text-align': 'center' } },
            { headerName: "Return Date", width: 180, field: "ReturnDate", dataType: 'DateTime', cellStyle: { 'text-align': 'left' } },
            { headerName: "Return Qty", width: 180, field: "ReturnQty", dataType: 'Number', cellStyle: { 'text-align': 'center' } },
            { headerName: "Flavour", width: 120, field: "Flavour", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Voucher Name", width: 180, field: "VoucherName", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Cost Class", width: 180, field: "CostClass", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Branch", width: 120, field: "Branch", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Address", width: 200, field: "Address", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Salesman", width: 180, field: "Salesman", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "P.Group", width: 160, field: "PGroup", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Brand", width: 120, field: "Brand", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Color", width: 120, field: "Color", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Shape", width: 120, field: "Shape", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "ProductType", width: 180, field: "ProductType", dataType: 'Text', cellStyle: { 'text-align': 'left' } },

            //{ headerName: "Qty", width: 180, field: "BalanceQty", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); },  },
            //{ headerName: "Unit", width: 120, field: "Unit", dataType: 'Text', cellStyle: { 'text-align': 'left' }  },
            //{ headerName: "Category", width: 160, field: "ProductCategory", dataType: 'Text', cellStyle: { 'text-align': 'left' }  },
            //{ headerName: "PurchaseLedger", width: 180, field: "PurchaseLedger", dataType: 'Text', cellStyle: { 'text-align': 'left' }  },
            //{ headerName: "PurchaseCostCenter", width: 200, field: "CostCenter", dataType: 'Text', cellStyle: { 'text-align': 'left' }  },
            //{ headerName: "TotalSales", width: 180, field: "TotalSales", dataType: 'Text', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); },  },          
            //{ headerName: "TotalValue", width: 180, field: "TotalValue", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); },  },

        ];


        $scope.gridOptions = {

            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                width: 100,


            },
            headerHeight: 35,
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
                    Name: 'Total =>',
                    ReceivedQty: 0,
                    BalanceQty: 0,
                    TotalSales: 0,
                    TotalValue: 0



                }
                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var fData = node.data;
                    dt.ReceivedQty += fData.ReceivedQty;
                    dt.BalanceQty += fData.BalanceQty;
                    dt.TotalSales += fData.TotalSales;
                    dt.TotalValue += fData.TotalValue;
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
                Name: 'Total =>',
                ReceivedQty: 0,
                BalanceQty: 0,
                TotalSales: 0,
                TotalValue: 0
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
    $scope.ClearData = function () {

        var DataColl = [];
        $scope.gridOptionsBottom.api.setRowData(DataColl);

        $scope.gridOptions.api.setRowData(DataColl);
    };
    $scope.GetPendingReceiptNote = function () {

        $scope.ClearData();
        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.PendingReceiptNote.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.PendingReceiptNote.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.PendingReceiptNote.DateToDet)
            dateTo = new Date(($filter('date')($scope.PendingReceiptNote.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var beData = {
            DateFrom: dateFrom,
            DateTo: dateTo,
            VoucherType: $scope.PendingReceiptNote.VoucherId,
            isPost: $scope.PendingReceiptNote.IsPost,
            branchId: $scope.PendingReceiptNote.BranchId
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Inventory/Reporting/GetPendingReceiptNote",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {


            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                var DataColl = mx(res.data.Data);

                var dt = {
                    Name: 'TOTAL =>',
                    TotalValue: DataColl.sum(p1 => p1.TotalValue),
                    TotalSales: DataColl.sum(p1 => p1.TotalSales),
                    ReceivedQty: DataColl.sum(p1 => p1.ReceivedQty),
                    BalanceQty: DataColl.sum(p1 => p1.BalanceQty),
                }

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
