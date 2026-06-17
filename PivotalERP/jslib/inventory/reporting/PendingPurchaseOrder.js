"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller("PendingPurchaseOrder", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'PendingPurchaseOrder.csv',
            sheetName: 'PendingPurchaseOrder'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    function LoadData() {
        $('.select2').select2({
            allowClear: true,
            openOnEnter: true
        });
        $scope.ReportTypeColl = [{ text: 'PendingOnly', value: 'PendingOnly', dataType: 'text' }, { text: 'ClearOnly', value: 'ClearOnly', dataType: 'text' }, { text: 'Both', value: 'Both', dataType: 'text' },]


        $scope.PendingPurchaseOrder = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            VoucherId: 0,
            IsPost: true,
            BranchId: 0
        };

        $scope.loadingstatus = "stop";

        $scope.columnDefs = [
            {
                headerName: "Date(A.D.)", width: 140, field: "VoucherDate", pinned: 'left', cellRenderer: 'agGroupCellRenderer',
                valueFormatter: function (params) { return DateFormatAD(params.value); },
                showRowGroup: true, cellStyle: { 'text-align': 'center' },
                cellRendererParams: {
                    suppressCount: false, // turn off the row count                   
                }
            },
            {
                headerName: "Miti", width: 140, field: "VoucherDateBS", pinned: 'left', cellRenderer: 'agGroupCellRenderer',
                valueFormatter: function (params) { return DateFormatBS(params.value); },
                showRowGroup: true, cellStyle: { 'text-align': 'center' },
                cellRendererParams: {
                    suppressCount: false, // turn off the row count                   
                }
            },
            { headerName: "Particulars", width: 180, field: "Name", pinned: 'left', dataType: 'Text', cellStyle: { 'text-align': 'left' } },

            { headerName: "Alias", width: 120, field: "Alias", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Code", width: 120, field: "Code", dataType: 'Number', cellStyle: { 'text-align': 'left' } },


            //FIled Added by bibek start here i.e M.v Dugar
            { headerName: "Order No", width: 120, field: "OrderNo", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Order Qty", width: 120, field: "OrderQty", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "P.O. Rate", width: 120, field: "PORate", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "P.O. Amt.", width: 120, field: "POAmt", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "Cancel No", width: 130, field: "CancelNo", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "Cancel Date", width: 140, field: "CancelDate", dataType: 'DateTime', cellStyle: { 'text-align': 'left' } },

            { headerName: "Cancel Qty", width: 120, field: "CancelQty", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "Invoice No", width: 120, field: "InvoiceNo", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Purchase Date", width: 140, field: "PurchaseDate", dataType: 'DateTime', cellStyle: { 'text-align': 'left' } },
            { headerName: "Purchase Qty", width: 140, field: "PurchaseQty", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "Pending Qty", width: 130, field: "PendingQty", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "P.I. Rate", width: 130, field: "PIRate", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "P.I. Amt", width: 130, field: "PIAmt", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "Cancel Amt", width: 130, field: "CancelAmt", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "Cancel Rate", width: 130, field: "CancelRate", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "GRN Amt", width: 130, field: "GRNAmt", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "GRN Rate", width: 130, field: "GRNRate", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "Order ReturnDetails", width: 200, dataType: 'Text', field: "OrderReturnDetails", cellStyle: { 'text-align': 'left' } },
            { headerName: "Purchase Details", width: 200, field: "PurchaseDetails", dataType: 'text', cellStyle: { 'text-align': 'left' } },
            { headerName: "ReceiptNote Details", width: 200, field: "ReceiptNoteDetails", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "GRN No", width: 130, field: "GRNNo", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "GRN Date", width: 140, field: "GRNDate", dataType: 'DateTime', cellStyle: { 'text-align': 'left' } },
            { headerName: "GRN Qty", width: 130, field: "GRNQty", dataType: 'Number', cellStyle: { 'text-align': 'left' } },

            { headerName: "Flavour", width: 130, field: "Flavour", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Rate Diff", width: 130, field: "RateDiff", dataType: 'Number', cellStyle: { 'text-align': 'left' } },

            { headerName: "Voucher Name", width: 200, field: "VoucherName", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Amt. Diff", width: 130, field: "AmtDiff", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "Cost Class", width: 140, field: "CostClass", dataType: 'DateTime', cellStyle: { 'text-align': 'left' } },

            { headerName: "Branch", width: 140, field: "Branch", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Party", width: 130, field: "Party", dataType: 'Text', cellStyle: { 'text-align': 'left' } },

            { headerName: "Address", width: 200, field: "Address", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "SalesMan", width: 200, field: "SalesMan", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Group", width: 140, field: "Group", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Brand", width: 140, field: "Brand", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Color", width: 130, field: "Color", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Shape", width: 150, field: "Shape", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Product Type", width: 140, field: "ProductType", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            //FIled Added by bibek end here i.e M.v Dugar

            { headerName: "OutQty", width: 130, field: "OutQty", dataType: 'Number', cellStyle: { 'text-align': 'left' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            /*      { headerName: "PendingQty", width: 150, field: "DemandQty", dataType: 'Number', cellStyle: { 'text-align': 'left' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); },  },*/
            { headerName: "Godown", width: 130, field: "FromGodown", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "IssuesDetails", width: 180, field: "IssuesDetails", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "ServiceEnginer", width: 180, field: "ServiceEnginer", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Remarks", width: 200, field: "Remarks", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "RegdNo", width: 140, field: "RegdNo", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "EngineNo", width: 150, field: "EngineNo", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "ChassisNo", width: 150, field: "ChassisNo", dataType: 'Number', cellStyle: { 'text-align': 'right' } },
            { headerName: "VinNo", width: 180, field: "VinNo", dataType: 'Number', cellStyle: { 'text-align': 'right' } },
        ];

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
                    Name: 'Total =>',
                    DemandQty: 0,
                    OutQty: 0,
                }
                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var fData = node.data;
                    dt.PendingQty += fData.DemandQty;
                    dt.OutQty += fData.OutQty;
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
                DemandQty: 0,
                OutQty: 0,

            }];

        $scope.gridOptionsBottom = {
            defaultColDef: {
                resizable: true,
                width: 90
            },
            rowHeight: 30,
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
    $scope.GetPendingPurchaseOrder = function () {
        $scope.ClearData();
        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.PendingPurchaseOrder.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.PendingPurchaseOrder.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.PendingPurchaseOrder.DateToDet)
            dateTo = new Date(($filter('date')($scope.PendingPurchaseOrder.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var beData = {
            DateFrom: dateFrom,
            DateTo: dateTo,
            VoucherType: $scope.PendingPurchaseOrder.VoucherId,
            isPost: $scope.PendingPurchaseOrder.IsPost,
            branchId: $scope.PendingPurchaseOrder.BranchId
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Inventory/Reporting/GetPendingPurchaseOrder",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                var DataColl = mx(res.data.Data);
                var dt = {
                    Name: 'TOTAL =>',
                    OutQty: DataColl.sum(p1 => p1.OutQty),
                    DemandQty: DataColl.sum(p1 => p1.DemandQty)
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

    $scope.DownloadAsXls = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();
        var dataColl = $scope.GetDataForPrint();
        var paraData = {
            Period: $scope.PendingPurchaseOrder.DateFromDet.dateBS + " TO " + $scope.PendingPurchaseOrder.DateToDet.dateBS,
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
                down_file(base_url + "//" + res.data.Data.ResponseId, "PendingPurchaseOrder.xlsx");
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire(errormessage);
        });
    }

});
