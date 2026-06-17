"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller("SalesInvoiceDetails", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'SalesInvoiceDetails.csv',
            sheetName: 'SalesInvoiceDetails'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    function LoadData() {
        $('.select2').select2({
            allowClear: true,
            openOnEnter: true
        });

        //Search Drop DownList
        $scope.VoucherSearchOptions = [{ text: 'VoucherDate', value: 'VoucherDate', dataType: 'date' },
        { text: 'Date(B.S)', value: 'VoucherDateStr', dataType: 'date' },
        { text: 'Voucher No', value: 'Voucher No', dataType: 'Number' },
        { text: 'Voucher', value: 'Voucher', dataType: 'Text' },
        { text: 'Party Name', value: 'PartyName', dataType: 'text' },
        { text: 'Party Group', value: 'PartyGroup', dataType: 'text' },
        { text: 'Party Cost Center', value: 'PartyCostCenter', dataType: 'text' },
        { text: 'Ref.No.', value: 'RefNo', dataType: 'Number' },
        { text: 'Ref.V.No.', value: 'OtherReferences', dataType: 'Number' },
        { text: 'Address', value: 'Address', dataType: 'text' },
        { text: 'PanVatNo', value: 'PanVatNo', dataType: 'Number' },
        { text: 'Sales Ledger', value: 'SalesLedger', dataType: 'text' },
        { text: 'Sales Cost Center', value: 'SalesLedgerGroup', dataType: 'text' },
        { text: 'Total Sales', value: 'TotalSales', dataType: 'Number' },
        { text: 'Discount Amt', value: 'InAmt', dataType: 'Number' },
        { text: 'Net Sales Qty', value: 'Discount', dataType: 'Number' },
        { text: 'TaxableValue', value: 'TaxableValue', dataType: 'number' },
        { text: 'Vat', value: 'Vat', dataType: 'Number' },
        { text: 'Additional Amt', value: 'AdditionalCharge', dataType: 'Number' },
        { text: 'TotalValue', value: 'TotalValue', dataType: 'Number' },
        { text: 'Entry Date', value: 'EntryDateStr', dataType: 'date' },
        { text: 'Current Total', value: 'CurrentClosing', dataType: 'number' },
        { text: 'SalesMan', value: 'SalesMan', dataType: 'text' },
        { text: 'Qty.', value: 'Qty', dataType: 'number' },
        { text: 'LineValue', value: 'LineValue', dataType: 'number' },
        { text: 'No. of Line', value: 'NoOfline', dataType: 'number' },
        { text: 'FReight', value: 'Freight', dataType: 'number' },
        { text: 'Excise Duty', value: 'ExDuty', dataType: 'number' },
        { text: 'Insurance', value: 'Insurance', dataType: 'number' },
        { text: 'Scheme', value: 'Schame', dataType: 'number' },
        { text: 'Contact No.', value: 'ContactNo', dataType: 'text' },
        ];

        //Filter Dialog Box Details 
        $scope.BranchTypeColl = [];
        $scope.VoucherTypeColl = [];
        $scope.LedgerGroupTypeColl = [];
        $scope.ExpressionColl = GlobalServices.getExpression();
        $scope.ConditionColl = GlobalServices.getLogicCondition();
        $scope.FilterColumnColl = [{ text: 'Discount', value: 'Discount', dataType: 'Number' },
        { text: 'ExDuty', value: 'ExDuty', dataType: 'Number' },
        { text: 'Frieght', value: 'Freight', dataType: 'Number' },
        { text: 'Import Tax', value: 'ImportTax', dataType: 'Number' },
        { text: 'Import Value', value: 'ImportValue', dataType: 'Number' },
        { text: 'Vat', value: 'Vat', dataType: 'Number' },
        { text: 'TotalValue', value: 'TotalValue', dataType: 'Number' },
        { text: 'TotalSales', value: 'TotalSales', dataType: 'Number' },
        { text: 'TaxableValue', value: 'TaxableValue', dataType: 'Number' },
        { text: 'Schame', value: 'Schame', dataType: 'Number' },
        { text: 'AdditionalCharge', value: 'AdditionalCharge', dataType: 'Number' },
        { text: 'Address', value: 'Address', dataType: 'text' },
        { text: 'Buyer', value: 'Buyer', dataType: 'text' },
        { text: 'InvoiceNo', value: 'InvoiceNo', dataType: 'number' },
        { text: 'PanVAtNo', value: 'PanVatNo', dataType: 'number' },
        { text: 'PartyName', value: 'PartyName', dataType: 'text' },
        { text: 'VoucherName', value: 'VoucherName', dataType: 'text' },
        { text: 'Branch', value: 'Branch', dataType: 'text' },
        ];

        $scope.SalesInvoiceDetails = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            VoucherId: 0,
            IsPost: true,
            BranchId: 0
        };
        $scope.OpeningAmt = 0;
        $scope.CurrentAmt = 0;
        $scope.TotalAmt = 0;
        $scope.ReportName = '';
        $scope.noofdecimal = 2;

        $scope.loadingstatus = "stop";

        $scope.columnDefs = [

            { headerName: "Date", width: 120, field: "VoucherDate", dataType: 'DateTime', cellStyle: { 'text-align': 'center' }, valueFormatter: function (params) { return DateFormatAD(params.value); }, pinned: 'left' },
            { headerName: "Miti", width: 110, field: "VoucherDateBS", dataType: 'DateTime', cellStyle: { 'text-align': 'center' }, pinned: 'left' },
            { headerName: "VoucherNo", width: 130, field: "InvoiceNo", dataType: 'Number', cellStyle: { 'text-align': 'right' }, pinned: 'left' },
            { headerName: "Voucher", width: 180, field: "VoucherName", dataType: 'Text', cellStyle: { 'text-align': 'left' }, pinned: 'left' },
            { headerName: "TranType", width: 130, field: "CashBank", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "For", width: 150, field: "ForParty", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "PartyCode", width: 120, field: "PartyCode", dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "PartyName", width: 250, field: "PartyLedger", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Party CostCenter", width: 160, field: "PartyCostCenter", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Ref No.", width: 120, field: "RefNo", dataType: 'Text', cellStyle: { 'text-align': 'center' } },
            { headerName: "Address", width: 160, field: "PartyAddress", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "PanVatNo", width: 120, field: "PanVatNo", dataType: 'Number', cellStyle: { 'text-align': 'right' } },
            { headerName: "Sales Ledger", width: 180, field: "SalesLedger", dataType: 'Number', cellStyle: { 'text-align': 'right' } },
            { headerName: "Invoice Amount", width: 140, field: "InvoiceAmount", dataType: 'Number', cellStyle: { 'text-align': 'right' } },
            { headerName: "Product Code", width: 150, field: "ProductCode", dataType: 'Number', cellStyle: { 'text-align': 'right' } },
            { headerName: "Product Name", width: 210, field: "ProductName", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Product Alias", width: 140, field: "ProductAlias", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Product Group", width: 150, field: "ProductGroup", cellStyle: { 'text-align': 'left' } },
            { headerName: "Product Category", width: 180, field: "ProductCategory", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Product Type", width: 140, field: "ProductType", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Product Company", width: 180, field: "ProductCompany", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Division", width: 120, field: "ProductDivision", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Qty", width: 140, field: "Qty", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Rate", width: 150, field: "Rate", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Amount", width: 150, field: "Amount", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Dis. Amt.", width: 180, field: "Discount", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Scheme", width: 150, field: "SchameAmount", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            //{ headerName: "Vat", width: 150, field: "Vat", dataType: 'Number',  cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); },  },
            //{ headerName: "Agent Code", width: 150, dataType: 'Text', field: "AgentCode", cellStyle: { 'text-align': 'right' } },
            { headerName: "DSE", width: 150, field: "DI_Name", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            //{ headerName: "Agent Area", width: 150, field: "Area", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Party Area", width: 150, field: "PartyArea", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Sales CostCenter", width: 180, dataType: 'Text', field: "TranCostCenter", cellStyle: { 'text-align': 'left' } },
            { headerName: "Party CostCategory", width: 180, dataType: 'Text', field: "PartyCostCategory", cellStyle: { 'text-align': 'left' } },
            { headerName: "Sales CostCategory", width: 180, dataType: 'Text', field: "TranCostCategory", cellStyle: { 'text-align': 'left' } },
            { headerName: "Item Description", width: 180, dataType: 'Text', field: "ProductDescription", cellStyle: { 'text-align': 'left' } },

            { headerName: "SO", width: 150, field: "SO_Name", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "ASM", width: 150, field: "ASM_Name", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "RSM", width: 150, field: "RSM_Name", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "NSM", width: 150, field: "NSM_Name", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "SD", width: 150, field: "SD_Name", dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "MD", width: 150, field: "MD_Name", dataType: 'Text', cellStyle: { 'text-align': 'left' } },

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
                    VoucherName: 'TOTAL =>',
                    Qty: 0,
                    Amount: 0,
                    Vat: 0,
                    Discount: 0,
                    Schame: 0,
                    InvoiceAmount: 0,
                }
                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var fData = node.data;
                    dt.Qty += fData.Qty;
                    dt.Amount += fData.Amount;
                    //dt.Vat += fData.Vat;
                    dt.Discount += fData.Discount;
                    dt.Schame += fData.Schame;
                    dt.InvoiceAmount += fData.InvoiceAmount;
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
                VoucherName: 'Total =>',
                Amount: 0,
                Rate: '',
                InvoiceAmount: 0,
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

    $scope.GetSalesInvoiceDetails = function () {

        $scope.ClearData();

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.SalesInvoiceDetails.DateFromDet)
            dateFrom = $filter('date')($scope.SalesInvoiceDetails.DateFromDet.dateAD, 'yyyy-MM-dd');

        if ($scope.SalesInvoiceDetails.DateToDet)
            dateTo = $filter('date')($scope.SalesInvoiceDetails.DateToDet.dateAD, 'yyyy-MM-dd');

        var beData = {
            dateFrom: dateFrom,
            dateTo: dateTo,
            //VoucherType: $scope.SalesInvoiceDetails.VoucherId,
            //isPost: $scope.SalesInvoiceDetails.IsPost,
            //branchId: $scope.SalesInvoiceDetails.BranchId
        };

        $scope.loadingstatus = 'running';
        showPleaseWait();

        $http({
            method: "POST",
            url: base_url + "Inventory/Reporting/GetSalesInvoiceDetails",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                var DataColl = mx(res.data.Data);

                var dt = {
                    VoucherName: 'TOTAL =>',
                    Qty: DataColl.sum(p1 => p1.Qty),
                    Amount: DataColl.sum(p1 => p1.Amount),
                    //   Vat: DataColl.sum(p1 => p1.Vat),
                    Discount: DataColl.sum(p1 => p1.Discount),
                    SchameAmount: DataColl.sum(p1 => p1.SchameAmount),
                    InvoiceAmount: DataColl.sum(p1 => p1.InvoiceAmount),
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
                                                        Period: $scope.SalesInvoiceDetails.DateFromDet.dateBS + " TO " + $scope.SalesInvoiceDetails.DateToDet.dateBS,
                                                        ProductGroup: '',
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
                                    Period: $scope.SalesInvoiceDetails.DateFromDet.dateBS + " TO " + $scope.SalesInvoiceDetails.DateToDet.dateBS,
                                    ProductGroup: '',
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
            var dayBook = node.data;
            filterData.push(dayBook);
        });

        return filterData;

    };

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }


});
