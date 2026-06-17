"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller("salesMaterializedViewCntrl", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'salesMaterializedView.csv',
            sheetName: 'salesMaterializedView'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    function Numberformat(val) {

        if (!val || val == 0)
            return '';
        return $filter('number')(val, 2);
    }

    function NumberformatAC(val) {
        if (val > 0)
            return $filter('number')(val, 2) + ' DR';
        else if (val < 0)
            return $filter('number')(val, 2) + ' CR';
        else
            return '';

    }
    function DateFormatAD(date) {

        if (date)
            return $filter('date')(date, 'yyyy-MM-dd');
        return '';
    };
    function padLeft(nr, n, str) {

        if (nr && n)
            return Array(n - String(nr).length + 1).join(str || '0') + nr;
        return '';
    };
    function DateFormatBS(ny, nm, nd) {
        if (ny && nm && nd)
            return ny + '-' + padLeft(nm, 2) + '-' + padLeft(nd, 2);
        return '';
    };
    function LoadData() {

        $scope.BranchList = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllBranchList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BranchList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.salesMaterializedView = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            isReturn:false,
        };
     
        $scope.ReportName = '';
        $scope.noofdecimal = 2;

        $scope.loadingstatus = "stop";

        var columnDefs = [
            { headerName: "FiscalYear", width: 120, field: "FYear" },
            { headerName: "Bill No.", width: 80, field: "BillNo" },
            { headerName: "Customer Name", width: 180, field: "PartyName" },
            { headerName: "Customer PAN", width: 120, field: "PanVatNo" },
            { headerName: "Bill Date", width: 120, field: "VoucherDateBS" },
            {
                headerName: "Amount", width: 150, filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.TotalAmount;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            {
                headerName: "Discount", width: 150, filter: "agNumberColumnFilter",
                field: "Discount",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            {
                headerName: "Taxable Amt.", width: 150, filter: "agNumberColumnFilter",
                field: "TaxAbleAmount",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            {
                headerName: "Vat Amt.", width: 150, filter: "agNumberColumnFilter",
                field: "Vat",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            {
                headerName: "Total Amt.", width: 150, filter: "agNumberColumnFilter",
                field: "TotalAmount",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },                       
            {
                headerName: "Sync with IRD", width: 120, field: "SyncWithIRD",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.SyncWithIRD == true)
                        return 'Yes';
                    else
                        return 'No'                    
                },
            },
            {
                headerName: "IsPrinted", width: 120, field: "IsPrinted",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.IsPrinted == true)
                        return 'Yes';
                    else
                        return 'No'
                },
            },           
            {
                headerName: "IsActive", width: 120, field: "IsActive",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.IsActive == true)
                        return 'Yes';
                    else
                        return 'No'
                },
            },
            { headerName: "Print DateTime", width: 120, field: "PrintDateTime" },
            { headerName: "Entered By", width: 120, field: "EnterBy" },
            { headerName: "Print By", width: 120, field: "PrintBy" },
            {
                headerName: "Is RealTime", width: 120, field: "IsRealTime",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.IsRealTime == true)
                        return 'Yes';
                    else
                        return 'No'
                },
            },
            { headerName: "Payment_Method", width: 150, field: "PaymentMethod", cellStyle: { 'text-align': 'left' } },
            { headerName: "Vat Refund Amt.", width: 140, field: "VatRefundAmtIfAny", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "TransactionId", width: 140, field: "TransactionId", cellStyle: { 'text-align': 'left' } },

            { headerName: "Branch", width: 120, field: "Branch", cellStyle: { 'text-align': 'left' } },

        ];


        $scope.gridOptions = {
            // a default column definition with properties that get applied to every column
            angularCompileRows: true,
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,

                // set every column width
                width: 90
            },
            columnDefs: columnDefs,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            rowSelection: 'multiple',          
        };


    }
   
    $scope.GetsalesMaterializedView = function () {

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.salesMaterializedView.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.salesMaterializedView.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.salesMaterializedView.DateToDet)
            dateTo = new Date(($filter('date')($scope.salesMaterializedView.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.DataColl = []; //declare an empty array
        $scope.gridOptions.api.setRowData($scope.DataColl);

        var beData = {
            DateFrom: dateFrom,
            DateTo: dateTo,
            isReturn: false
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Account/Reporting/GetSalesMaterializedView",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            $scope.DataColl = res.data.Data;
            $scope.gridOptions.api.setRowData($scope.DataColl);

            $scope.loadingstatus = 'done';

        }, function (errormessage) {

            $scope.loadingstatus = 'stop';

            alert('Unable to Store data. pls try again.' + errormessage.responseText);
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
                                            var dataColl = [];                                            
                                            $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                                                dataColl.push(node.data);
                                            });
                                            print = true;
                                            $http({
                                                method: 'POST',
                                                url: base_url + "Account/Reporting/PrintSalesMaterializedView",
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
                        var dataColl = [];
                        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                            dataColl.push(node.data);
                        });
                        print = true;

                        $http({
                            method: 'POST',
                            url: base_url + "Account/Reporting/PrintSalesMaterializedView",
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

  
    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    
});

app.controller("salesInvoiceDetailsCntrl", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'salesInvoiceDetails.csv',
            sheetName: 'salesInvoiceDetails'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    function Numberformat(val) {

        if (!val || val == 0)
            return '';
        return $filter('number')(val, 2);
    }

    function NumberformatAC(val) {
        if (val > 0)
            return $filter('number')(val, 2) + ' DR';
        else if (val < 0)
            return $filter('number')(val, 2) + ' CR';
        else
            return '';

    }
    function DateFormatAD(date) {

        if (date)
            return $filter('date')(date, 'yyyy-MM-dd');
        return '';
    };
    function padLeft(nr, n, str) {

        if (nr && n)
            return Array(n - String(nr).length + 1).join(str || '0') + nr;
        return '';
    };
    function DateFormatBS(ny, nm, nd) {
        if (ny && nm && nd)
            return ny + '-' + padLeft(nm, 2) + '-' + padLeft(nd, 2);
        return '';
    };
    function LoadData() {

        $scope.BranchList = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllBranchList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BranchList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.salesInvoiceDetails = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
        };

        $scope.ReportName = '';
        $scope.noofdecimal = 2;

        $scope.loadingstatus = "stop";

        var columnDefs = [
            { headerName: "Date(A.D.)", width: 120, field: "VoucherDate" },
            { headerName: "Date(B.S.)", width: 80, field: "VoucherDateBS" },
            { headerName: "Invoice No.", width: 80, field: "InvoiceNo" },
            { headerName: "Voucher Name", width: 80, field: "VoucherName" },            
            { headerName: "Customer Name", width: 80, field: "PartyLedger" },
            { headerName: "Customer PAN", width: 120, field: "SalesTaxNo" },
            { headerName: "Customer Address", width: 120, field: "PartyAddress" },
            { headerName: "Party Group", width: 120, field: "PartyLedgerGroup" },
            { headerName: "Sales Ledger", width: 120, field: "SalesLedger" },
            {
                headerName: "Invoice Amt.", width: 150, filter: "agNumberColumnFilter", field: "InvoiceAmount",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            { headerName: "Product", width: 120, field: "ProductName" },
            { headerName: "ProductAlias", width: 120, field: "ProductAlias" },
            { headerName: "ProductCode", width: 120, field: "ProductCode" },
            { headerName: "ProductGroup", width: 120, field: "ProductGroup" },

            {
                headerName: "Qty", width: 150, filter: "agNumberColumnFilter",
                field: "Qty",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            { headerName: "Unit", width: 120, field: "Unit" },
            {
                headerName: "Rate", width: 150, filter: "agNumberColumnFilter",
                field: "Rate",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            {
                headerName: "Amount", width: 150, filter: "agNumberColumnFilter",
                field: "Amount",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            
            { headerName: "RefNo", width: 120, field: "RefNo" },
         

        ];


        $scope.gridOptions = {
            // a default column definition with properties that get applied to every column
            angularCompileRows: true,
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,

                // set every column width
                width: 90
            },
            columnDefs: columnDefs,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            rowSelection: 'multiple',
        };


    }

    $scope.GetSalesInvoiceDetails = function () {

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.salesInvoiceDetails.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.salesInvoiceDetails.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.salesInvoiceDetails.DateToDet)
            dateTo = new Date(($filter('date')($scope.salesInvoiceDetails.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.DataColl = []; //declare an empty array
        $scope.gridOptions.api.setRowData($scope.DataColl);

        var beData = {
            DateFrom: dateFrom,
            DateTo: dateTo,
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Account/Reporting/GetSalesInvoiceDetail",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            $scope.DataColl = res.data.Data;
            $scope.gridOptions.api.setRowData($scope.DataColl);

            $scope.loadingstatus = 'done';

        }, function (errormessage) {

            $scope.loadingstatus = 'stop';

            alert('Unable to Store data. pls try again.' + errormessage.responseText);
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
                                            var dataColl = [];
                                            $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                                                dataColl.push(node.data);
                                            });
                                            print = true;
                                            $http({
                                                method: 'POST',
                                                url: base_url + "Account/Reporting/PrintSalesInvoiceDetail",
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
                        var dataColl = [];
                        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                            dataColl.push(node.data);
                        });
                        print = true;

                        $http({
                            method: 'POST',
                            url: base_url + "Account/Reporting/PrintSalesInvoiceDetail",
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


    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }


});

app.controller("salesVatRegisterCntrl", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'salesVatRegister.csv',
            sheetName: 'salesVatRegister'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    function Numberformat(val) {

        if (!val || val == 0)
            return '';
        return $filter('number')(val, 2);
    }

    function NumberformatAC(val) {
        if (val > 0)
            return $filter('number')(val, 2) + ' DR';
        else if (val < 0)
            return $filter('number')(val, 2) + ' CR';
        else
            return '';

    }
    function DateFormatAD(date) {

        if (date)
            return $filter('date')(date, 'yyyy-MM-dd');
        return '';
    };
    function padLeft(nr, n, str) {

        if (nr && n)
            return Array(n - String(nr).length + 1).join(str || '0') + nr;
        return '';
    };
    function DateFormatBS(ny, nm, nd) {
        if (ny && nm && nd)
            return ny + '-' + padLeft(nm, 2) + '-' + padLeft(nd, 2);
        return '';
    };
    function LoadData() {

        $scope.BranchList = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllBranchList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BranchList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.salesVatRegister = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),           
        };

        $scope.ReportName = '';
        $scope.noofdecimal = 2;

        $scope.loadingstatus = "stop";

        var columnDefs = [            
            {
                headerName: "Miti", width: 130, field: "VoucherDate_BS", cellRenderer: 'agGroupCellRenderer',
                valueFormatter: function (params) { return DateFormatBS(params.value); },
                showRowGroup: true, cellStyle: { 'text-align': 'center' },
                cellRendererParams: {
                    suppressCount: false, // turn off the row count                   
                }, pinned: 'left'
            },
            { headerName: "InvoiceNo", width: 140, field: "VoucherNo", cellStyle: { 'text-align': 'center' }, pinned: 'left' },
            { headerName: "Customer Name", width: 220, field: "PartyName", cellStyle: { 'text-align': 'left' } },
            { headerName: "Customer PAN", width: 160, field: "PanVat", cellStyle: { 'text-align': 'left' } },
            { headerName: "Product Name", width: 180, field: "ProductName", cellStyle: { 'text-align': 'left' } },
            { headerName: "Non Taxable Amt.", width: 170, field: "NonTaxableSales", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Taxable Amt.", width: 160, field: "TaxableSales", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Vat", width: 140, field: "ProductVatAmount", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Export Sales", width: 160, field: "ExportSales", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Export Country", width: 160, field: "ExportCountry", cellStyle: { 'text-align': 'left' } },
            { headerName: "Branch", width: 170, field: "Branch", cellStyle: { 'text-align': 'left' } },
            { headerName: "Excise Duty", width: 160, field: "ExciseDuty", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },

            { headerName: "Item Amt.", width: 160, field: "ProductAmount", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "PP Date", width: 140, field: "PPDate_BS", cellStyle: { 'text-align': 'right' } },
            { headerName: "PP No.", width: 140, field: "PPNo", cellStyle: { 'text-align': 'right' } },
            { headerName: "Qty.", width: 140, field: "ActualQty", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "UOM", width: 120, field: "Unit", cellStyle: { 'text-align': 'right' } },
            
        ];


        $scope.gridOptions = {
            // a default column definition with properties that get applied to every column
            angularCompileRows: true,
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,

                // set every column width
                width: 90
            },
            columnDefs: columnDefs,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            rowSelection: 'multiple',
        };


    }

    $scope.GetSalesVatRegister = function () {

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.salesVatRegister.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.salesVatRegister.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.salesVatRegister.DateToDet)
            dateTo = new Date(($filter('date')($scope.salesVatRegister.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.DataColl = []; //declare an empty array
        $scope.gridOptions.api.setRowData($scope.DataColl);

        var beData = {
            DateFrom: dateFrom,
            DateTo: dateTo,          
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Account/Reporting/GetSalesVatRegister",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            $scope.DataColl = res.data.Data;
            $scope.gridOptions.api.setRowData($scope.DataColl);

            $scope.loadingstatus = 'done';

        }, function (errormessage) {

            $scope.loadingstatus = 'stop';

            alert('Unable to Store data. pls try again.' + errormessage.responseText);
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
                                            var dataColl = [];
                                            $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                                                dataColl.push(node.data);
                                            });
                                            print = true;
                                            $http({
                                                method: 'POST',
                                                url: base_url + "Account/Reporting/PrintSalesVatRegister",
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
                        var dataColl = [];
                        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                            dataColl.push(node.data);
                        });
                        print = true;

                        $http({
                            method: 'POST',
                            url: base_url + "Account/Reporting/PrintSalesVatRegister",
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


    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }


});

app.controller("purchaseVatRegisterCntrl", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'purchaseVatRegister.csv',
            sheetName: 'purchaseVatRegister'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    function Numberformat(val) {

        if (!val || val == 0)
            return '';
        return $filter('number')(val, 2);
    }

    function NumberformatAC(val) {
        if (val > 0)
            return $filter('number')(val, 2) + ' DR';
        else if (val < 0)
            return $filter('number')(val, 2) + ' CR';
        else
            return '';

    }
    function DateFormatAD(date) {

        if (date)
            return $filter('date')(date, 'yyyy-MM-dd');
        return '';
    };
    function padLeft(nr, n, str) {

        if (nr && n)
            return Array(n - String(nr).length + 1).join(str || '0') + nr;
        return '';
    };
    function DateFormatBS(ny, nm, nd) {
        if (ny && nm && nd)
            return ny + '-' + padLeft(nm, 2) + '-' + padLeft(nd, 2);
        return '';
    };
    function LoadData() {

        $scope.BranchList = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllBranchList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BranchList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.purchaseVatRegister = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
        };

        $scope.ReportName = '';
        $scope.noofdecimal = 2;

        $scope.loadingstatus = "stop";

        var columnDefs = [
            {
                headerName: "Miti", width: 130, field: "VoucherDate_BS", cellRenderer: 'agGroupCellRenderer',
                valueFormatter: function (params) { return DateFormatBS(params.value); },
                showRowGroup: true, cellStyle: { 'text-align': 'center' },
                cellRendererParams: {
                    suppressCount: false, // turn off the row count                   
                }, pinned: 'left'
            },
            { headerName: "InvoiceNo", width: 140, field: "VoucherNo", cellStyle: { 'text-align': 'center' }, pinned: 'left' },
            { headerName: "Ref.No.", width: 140, field: "RefNo", cellStyle: { 'text-align': 'center' } },
            { headerName: "PP No.", width: 140, field: "PPNo", cellStyle: { 'text-align': 'right' } },
            { headerName: "PP Date", width: 140, field: "PPDate_BS", cellStyle: { 'text-align': 'right' } },
            { headerName: "Vendor Name", width: 220, field: "PartyName", cellStyle: { 'text-align': 'left' } },
            { headerName: "Vendor PAN", width: 160, field: "PanVat", cellStyle: { 'text-align': 'left' } },
            { headerName: "Product Name", width: 180, field: "ProductName", cellStyle: { 'text-align': 'left' } },
            { headerName: "Qty.", width: 140, field: "ActualQty", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "UOM", width: 120, field: "Unit", cellStyle: { 'text-align': 'right' } },
            { headerName: "Item Amt.", width: 160, field: "ProductAmount", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Non Taxable Amt.", width: 170, field: "NonTaxablePurchase", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Taxable Amt.", width: 160, field: "TaxablePurchase", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Vat", width: 140, field: "ProductVatAmount", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Import Purchase", width: 160, field: "ImportPurchase", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Import Vat", width: 140, field: "ImportVat", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Capital Purchase", width: 140, field: "CapitalPurchase", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Capital Vat", width: 140, field: "CapitalVat", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Branch", width: 170, field: "Branch", cellStyle: { 'text-align': 'left' } },
            { headerName: "Excise Duty", width: 160, field: "ExciseDuty", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },

        ];


        $scope.gridOptions = {
            // a default column definition with properties that get applied to every column
            angularCompileRows: true,
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,

                // set every column width
                width: 90
            },
            columnDefs: columnDefs,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            rowSelection: 'multiple',
        };


    }

    $scope.GetPurchaseVatRegister = function () {

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.purchaseVatRegister.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.purchaseVatRegister.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.purchaseVatRegister.DateToDet)
            dateTo = new Date(($filter('date')($scope.purchaseVatRegister.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.DataColl = []; //declare an empty array
        $scope.gridOptions.api.setRowData($scope.DataColl);

        var beData = {
            DateFrom: dateFrom,
            DateTo: dateTo,
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Account/Reporting/GetPurchaseVatRegister",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            $scope.DataColl = res.data.Data;
            $scope.gridOptions.api.setRowData($scope.DataColl);

            $scope.loadingstatus = 'done';

        }, function (errormessage) {

            $scope.loadingstatus = 'stop';

            alert('Unable to Store data. pls try again.' + errormessage.responseText);
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
                                            var dataColl = [];
                                            $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                                                dataColl.push(node.data);
                                            });
                                            print = true;
                                            $http({
                                                method: 'POST',
                                                url: base_url + "Account/Reporting/PrintPurchaseVatRegister",
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
                        var dataColl = [];
                        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                            dataColl.push(node.data);
                        });
                        print = true;

                        $http({
                            method: 'POST',
                            url: base_url + "Account/Reporting/PrintPurchaseVatRegister",
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


    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

});

app.controller("dayBookCancelCntrl", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'daybook.csv',
            sheetName: 'daybook'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    function Numberformat(val) {

        if (!val || val == 0)
            return '';
        return $filter('number')(val, 2);
    }

    function NumberformatAC(val) {
        if (val > 0)
            return $filter('number')(val, 2) + ' DR';
        else if (val < 0)
            return $filter('number')(val, 2) + ' CR';
        else
            return '';

    }
    function DateFormatAD(date) {

        if (date)
            return $filter('date')(date, 'yyyy-MM-dd');
        return '';
    };
    function padLeft(nr, n, str) {

        if (nr && n)
            return Array(n - String(nr).length + 1).join(str || '0') + nr;
        return '';
    };
    function DateFormatBS(ny, nm, nd) {
        if (ny && nm && nd)
            return ny + '-' + padLeft(nm, 2) + '-' + padLeft(nd, 2);
        return '';
    };
    function LoadData() {

        //agGrid.initialiseAgGridWithAngular1(angular);
        $scope.VoucherTypeList = [];
        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetVoucherTypes",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.VoucherTypeList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.BranchList = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllBranchList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BranchList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.dayBook = {
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

        var columnDefs = [
            {
                headerName: "Date(A.D.)", width: 140, field: "VoucherDate", cellRenderer: 'agGroupCellRenderer',
                valueFormatter: function (params) { return DateFormatAD(params.value); }
            },
            {
                headerName: "Date(B.S.)", width: 100,
                cellRenderer:
                    function (params) {
                        return DateFormatBS(params.data.NY, params.data.NM, params.data.ND) + '</a ></center>';
                    }

            },
            {
                headerName: "Particular's", width: 100,
                valueGetter: function (params) {
                    var beData = params.data;

                    if (beData.IsInventory) {
                        return beData.PartyLedger;
                    }
                    else if (beData.LedgerAllocationColl && beData.LedgerAllocationColl.length > 0)
                        return beData.LedgerAllocationColl[0].LedgerName;
                    else if (beData.LedgerName)
                        return beData.LedgerName;
                    else if (beData.DispalyValue)
                        return beData.DispalyValue;
                    else
                        return params.data;
                }
            },
            { headerName: "VoucherType", width: 120, field: "VoucherName" },
            { headerName: "Voucher No.", width: 120, field: "AutoManualNo" },
            { headerName: "Ref.No.", width: 120, field: "RefNo" },
            {
                headerName: "Debit", width: 150, filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    var beData = params.data;

                    if (beData.IsInventory) {
                        return beData.DrAmount;
                    } else if (beData.LedgerAllocationColl && beData.LedgerAllocationColl.length == 0)
                        return 0;
                    else if (beData.LedgerAllocationColl && beData.LedgerAllocationColl.length > 0)
                        return beData.LedgerAllocationColl[0].DrAmount;
                    else if (beData.DrAmount)
                        return beData.DrAmount;
                    else
                        return 0;

                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            {
                headerName: "Credit", width: 150, filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    var beData = params.data;

                    if (beData.IsInventory) {
                        return beData.CrAmount;
                    } else if (beData.LedgerAllocationColl && beData.LedgerAllocationColl.length == 0)
                        return 0;
                    else if (beData.LedgerAllocationColl && beData.LedgerAllocationColl.length > 0)
                        return beData.LedgerAllocationColl[0].CrAmount;
                    else if (beData.CrAmount)
                        return beData.CrAmount;
                    else
                        return 0;

                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            { headerName: "CostClass", width: 120, field: "CostClassName" },
            { headerName: "User", width: 120, field: "CreatedByName" },
            {
                headerName: "Action", width: 150, cellRenderer:
                    function (params) {

                        var voucherName = params.data.VoucherType;

                        if (voucherName) {
                            return '<a class="btn btn-default btn-xs" href="' + base_url + 'Account/Transaction/' + voucherName + '?TranId={{' + params.data.TranId + '}}"><i class="fas fa-edit text-info"></i></a>' +
                                '<a class="btn btn-default btn-xs" ng-click="PrintVoucher(' + params.data.TranId + ',' + params.data.VoucherType + ',' + params.data.VoucherId + ')"><i class="fas fa-print text-info"></i></a>' +
                                '<a class="btn btn-default btn-xs" ng-click="deleteVoucher(' + params.data.TranId + ',\'' + voucherName + '\')"><i class="fas fa-trash-alt text-danger"></i></a>';
                        } else {
                            return '';
                        }
                    }
            }
        ];


        $scope.gridOptions = {
            // a default column definition with properties that get applied to every column
            angularCompileRows: true,
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,

                // set every column width
                width: 90
            },
            columnDefs: columnDefs,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            rowSelection: 'multiple',
            getNodeChildDetails: function (beData) {
                var dataColl = [];
                if (!beData.IsInventory) {
                    var first = true;

                    if (beData.LedgerAllocationColl) {
                        if (beData.LedgerAllocationColl.length > 0) {
                            angular.forEach(beData.LedgerAllocationColl, function (data) {

                                if (first == true) {
                                    first = false;
                                } else
                                    dataColl.push(data);
                            });
                        }
                    }
                    if (beData.Narration && beData.Narration.length > 0)
                        dataColl.push("(" + beData.Narration + ")");
                }
                else if (beData.IsInventory) {

                    //Dynamic.BusinessEntity.Account.VoucherTypes.StockTransfor=19
                    if (beData.VoucherType != 19) {
                        if (beData.Particulars && beData.Particulars.trim().Length > 0)
                            dataColl.Add(beData.Particulars);
                    }

                    if (beData.AditionalCostColl && beData.AditionalCostColl.length > 0) {
                        angular.forEach(beData.AditionalCostColl, function (ad) {
                            dataColl.push(ad);
                        });
                    }

                    if (beData.ItemAllocationColl && beData.ItemAllocationColl.length > 0) {
                        angular.forEach(beData.ItemAllocationColl, function (ias) {
                            dataColl.push(ias);
                        });
                    }

                    if (beData.Narration && beData.Narration.length > 0)
                        dataColl.push("(" + beData.Narration + ")");

                } else
                    return null;

                if (dataColl.length > 0) {
                    return {
                        group: true,
                        children: dataColl,
                        expanded: beData.open
                    };
                } else
                    return null;


            },

        };


    }
    $scope.deleteVoucher = function (TranId, VoucherName) {

        var ans = confirm("Are you sure you want to delete this Record?");

        if (ans) {
            $http({
                method: "post",
                url: base_url + "Account/Transaction/Delete" + VoucherName + "?TranId=" + TranId,
                //data: JSON.stringify(beData),
                dataType: "json"
            }).then(function (res) {
                alert(res.data.ResponseMSG);

            }, function (errormessage) {
                alert('Unable to Delete data. pls try again.' + errormessage.responseText);
            });
        }

    }
    $scope.GetDayBook = function () {

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.dayBook.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.dayBook.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.dayBook.DateToDet)
            dateTo = new Date(($filter('date')($scope.dayBook.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.DataColl = []; //declare an empty array
        $scope.gridOptions.api.setRowData($scope.DataColl);

        var beData = {
            DateFrom: dateFrom,
            DateTo: dateTo,
            VoucherType: $scope.dayBook.VoucherId,
            isPost: $scope.dayBook.IsPost,
            branchId: $scope.dayBook.BranchId
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Account/Reporting/GetCancelVoucherList",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            $scope.DataColl = res.data.Data;
            $scope.gridOptions.api.setRowData($scope.DataColl);

            $scope.loadingstatus = 'done';

        }, function (errormessage) {

            $scope.loadingstatus = 'stop';

            alert('Unable to Store data. pls try again.' + errormessage.responseText);
        });

    };
 
    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    $scope.PrintVoucher = function (tranId, voucherType, voucherId) {
        var para = {
            VoucherType: voucherType
        }
        $http({
            method: 'POST',
            url: base_url + "Global/GetEntityByVoucherType",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (rs) {
            if (rs.data.Data) {
                var entityId = rs.data.Data.RId;
                $timeout(function () {

                    if (tranId && tranId > 0) {

                        $http({
                            method: 'GET',
                            url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityId + "&voucherId=" + voucherId + "&isTran=true",
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

                                    var printed = false;
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
                                                        printed = true;
                                                        if (rptTranId > 0) {
                                                            document.body.style.cursor = 'wait';
                                                            document.getElementById("frmRpt").src = '';
                                                            document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + entityId + "&voucherid=" + voucherId + "&tranid=" + tranId + "&vouchertype=" + voucherType;
                                                            document.body.style.cursor = 'default';
                                                            $('#FrmPrintReport').modal('show');
                                                        }

                                                    } else {
                                                        resolve('You need to select:)')
                                                    }
                                                })
                                            }
                                        })
                                    }

                                    if (rptTranId > 0 && printed == false) {
                                        document.body.style.cursor = 'wait';
                                        document.getElementById("frmRpt").src = '';
                                        document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + entityId + "&voucherid=" + voucherId + "&tranid=" + tranId + "&vouchertype=" + voucherType;
                                        document.body.style.cursor = 'default';
                                        $('#FrmPrintReport').modal('show');
                                    }

                                } else
                                    Swal.fire('No Templates found for print');
                            }
                        }, function (reason) {
                            Swal.fire('Failed' + reason);
                        });
                    }

                });
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
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
                                                url: base_url + "Account/Reporting/PrintDayBook",
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

                                                    var findV = mx($scope.VoucherTypeList).firstOrDefault(p1 => p1.id == $scope.dayBook.VoucherId);

                                                    var rptPara = {
                                                        rpttranid: rptTranId,
                                                        istransaction: false,
                                                        entityid: EntityId,
                                                        voucherid: 0,
                                                        tranid: 0,
                                                        vouchertype: 0,
                                                        sessionid: res.data.Data.ResponseId,
                                                        Period: $scope.dayBook.DateFromDet.dateBS + " TO " + $scope.dayBook.DateToDet.dateBS,
                                                        Voucher: findV ? findV.text : ''
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
                            url: base_url + "Account/Reporting/PrintDayBook",
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

                                var findV = mx($scope.VoucherTypeList).firstOrDefault(p1 => p1.id == $scope.dayBook.VoucherId);

                                var rptPara = {
                                    rpttranid: rptTranId,
                                    istransaction: false,
                                    entityid: EntityId,
                                    voucherid: 0,
                                    tranid: 0,
                                    vouchertype: 0,
                                    sessionid: res.data.Data.ResponseId,
                                    Period: $scope.dayBook.DateFromDet.dateBS + " TO " + $scope.dayBook.DateToDet.dateBS,
                                    Voucher: findV ? findV.text : ''
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

        var RptParamentersColl = [];

        RptParamentersColl.push({
            Name: "Period",
            Value: $('#dtDateFrom').val() + ' To ' + $('#dtDateTo').val()
        });


        var filterData = [];

        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
            var dayBook = node.data;
            if (dayBook.IsParent == true) {
                var beData = {};

                beData.VoucherName = dayBook.VoucherName;
                beData.VoucherType = dayBook.VoucherType;
                beData.AutoManualNo = dayBook.AutoManualNo;
                beData.AutoVoucherNo = dayBook.AutoVoucherNo;
                beData.CanUpdateDocument = dayBook.CanUpdateDocument;
                beData.CostClassName = dayBook.CostClassName;
                beData.IsInventory = dayBook.IsInventory;
                beData.IsParent = true;
                beData.Narration = dayBook.Narration;
                beData.ND = dayBook.ND;
                beData.NM = dayBook.NM;
                beData.NY = dayBook.NY;
                beData.RefNo = dayBook.RefNo;
                beData.VoucherDate = dayBook.VoucherDate;
                beData.VoucherDateStr = DateFormatAD(dayBook.VoucherDate);
                beData.VoucherDateStrNP = DateFormatBS(beData.NY, beData.NM, beData.ND);
                beData.CreatedByName = dayBook.CreatedByName;

                if (beData.IsInventory == true) {
                    beData.Particulars = dayBook.PartyLedger;
                    beData.DrAmount = dayBook.DrAmount;
                    beData.CrAmount = dayBook.CrAmount;
                    filterData.push(beData);

                    var ledData = {};
                    ledData.Particulars = "  " + dayBook.Particulars;

                    if (!dayBook.AditionalCostColl)
                        dayBook.AditionalCostColl = [];

                    if (dayBook.DrAmount != 0 && dayBook.AditionalCostColl.length > 0)
                        ledData.CrAmount = dayBook.DrAmount - mx(dayBook.AditionalCostColl).sum(p1 => p1.Amount);
                    else if (dayBook.AditionalCostColl.length > 0)
                        ledData.DrAmount = dayBook.CrAmount - mx(dayBook.AditionalCostColl).sum(p1 => p1.Amount);

                    ledData.VoucherType = dayBook.VoucherType;
                    filterData.push(ledData);

                    angular.forEach(dayBook.AditionalCostColl, function (add) {

                        var addData = {};
                        addData.Particulars = "  " + add.LedgerName;
                        if (dayBook.DrAmount != 0) {
                            addData.CrAmount = add.Amount;
                        }
                        else {
                            addData.DrAmount = add.Amount;
                        }
                        addData.VoucherType = dayBook.VoucherType;
                        filterData.push(addData);
                    });

                    if (!dayBook.ItemAllocationColl)
                        dayBook.ItemAllocationColl = [];

                    angular.forEach(dayBook.ItemAllocationColl, function (item) {

                        var itemData = {};
                        itemData.Particulars = "    " + item.ProductName + " ( " + item.BilledQty + item.UnitName + " @ " + item.Rate + " = " + item.Amount + " )";
                        itemData.VoucherType = dayBook.VoucherType;
                        filterData.push(itemData);

                    });

                } else {
                    var firstTime = true;

                    if (!dayBook.LedgerAllocationColl)
                        dayBook.LedgerAllocationColl = [];

                    angular.forEach(dayBook.LedgerAllocationColl, function (ledAll) {
                        if (firstTime) {
                            beData.Particulars = ledAll.LedgerName;
                            beData.DrAmount = ledAll.DrAmount;
                            beData.CrAmount = ledAll.CrAmount;
                            firstTime = false;
                            beData.VoucherType = dayBook.VoucherType;
                            filterData.push(beData);
                        }
                        else {
                            var chieldData = {};
                            chieldData.Particulars = "  " + ledAll.LedgerName;
                            chieldData.Narration = ledAll.Narration;
                            chieldData.DrAmount = ledAll.DrAmount;
                            chieldData.CrAmount = ledAll.CrAmount;
                            chieldData.VoucherType = dayBook.VoucherType;
                            filterData.push(chieldData);
                        }
                    });
                }
            }

        });

        return filterData;

    };
});

app.controller("accountConfirmationCntrl", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'accountConfirmation.csv',
            sheetName: 'accountConfirmation'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    function Numberformat(val) {

        if (!val || val == 0)
            return '';
        return $filter('number')(val, 2);
    }

    function NumberformatAC(val) {
        if (val > 0)
            return $filter('number')(val, 2) + ' DR';
        else if (val < 0)
            return $filter('number')(val, 2) + ' CR';
        else
            return '';

    }
    function DateFormatAD(date) {

        if (date)
            return $filter('date')(date, 'yyyy-MM-dd');
        return '';
    };
    function padLeft(nr, n, str) {

        if (nr && n)
            return Array(n - String(nr).length + 1).join(str || '0') + nr;
        return '';
    };
    function DateFormatBS(ny, nm, nd) {
        if (ny && nm && nd)
            return ny + '-' + padLeft(nm, 2) + '-' + padLeft(nd, 2);
        return '';
    };
    function LoadData() {

        $scope.ForColl = [{ id: 0, text: 'Debtor' }, { id: 1, text: 'Creditor' }];
        $scope.GroupByColl = [{ id: 1, text: 'PAN' }, { id: 2, text: 'Party' }, { id: 3, text: 'Normal' }];

        $scope.BranchList = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllBranchList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BranchList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.accountConfirmation = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            IsCreditor: false,
            For: 0,
            GroupBy: 1
        };

        $timeout(function () {
            $http({
                method: "GET",
                url: base_url + "Global/GetCompanyDetail",
                dataType: "json"
            }).then(function (res) {
                var comDet = res.data.Data;
                if (comDet) {
                    $scope.accountConfirmation.DateFrom_TMP = new Date(comDet.StartDate);
                }
            }, function (errormessage) {
                alert('Unable to Delete data. pls try again.' + errormessage.responseText);
            });
        });

        $scope.ReportName = '';
        $scope.noofdecimal = 2;

        $scope.loadingstatus = "stop";

        $scope.columnDefs = [
            { headerName: "CustomerName", width: 220, field: "Name", dataType: 'Text', cellStyle: { 'text-align': 'left' }, pinned: 'left' },
            { headerName: "PanVatNo", colId: 'colR1', width: 120, dataType: 'Text', field: "PanVatNo", cellStyle: { 'text-align': 'center' }, pinned: 'left' },
            { headerName: "Taxable Sales", colId: 'colR2', width: 180, dataType: 'Text', field: "SalesVatAV", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Taxable SalesReturn", colId: 'colR3', width: 180, dataType: 'Text', field: "PurchaseVatAV", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Sales Vat", colId: 'colR4', width: 150, field: "SalesVat", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "SalesReturn Vat", colId: 'colR5', width: 180, dataType: 'Number', field: "PurchaseVat", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Actual Sales", colId: 'colR6', width: 150, dataType: 'Number', field: "ActualSales", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Actual Vat", width: 150, dataType: 'Number', field: "ActualVat", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Non Taxable Sales", colId: 'colR7', width: 180, dataType: 'Number', field: "NonTaxAbleSales", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Non Taxable SalesReturn", colId: 'colR8', width: 220, dataType: 'Number', field: "NonTaxAblePurchase", cellStyle: { 'text-align': 'center' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Opening", width: 150, dataType: 'Number', field: "Opening", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "CrAmount", width: 150, dataType: 'Number', field: "CrAmount", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "DrAmount", width: 150, dataType: 'Number', field: "DrAmount", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Closing", width: 150, dataType: 'Number', field: "Closing", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
           
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
                    Name: 'TOTAL =>',
                    Opening: 0,
                    DrAmount: 0,
                    CrAmount: 0,
                    Closing: 0,
                    SalesInvoiceAmount: 0,
                    SalesDiscount: 0,
                    SalesExDuty: 0,
                    SalesVat: 0,
                    SalesSchame: 0,
                    SalesFreight: 0,
                    PurchaseInvoiceAmount: 0,
                    PurchaseDiscount: 0,
                    PurchaseExDuty: 0,
                    PurchaseVat: 0,
                    PurchaseSchame: 0,
                    PurchaseFreight: 0,
                    ActualSales: 0,
                    ActualVat: 0,
                    Insurance: 0,
                    TaxAbleSales: 0,
                    NonTaxAbleSales: 0,
                    RoundOffAmt: 0,
                    SalesVatAV: 0,
                    SalesExciseAV: 0,
                    PurchaseVatAV: 0,
                    PurchaseExciseAV: 0,
                    NonTaxAblePurchase: 0,
                }
                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var fData = node.data;
                    dt.Opening += fData.Opening;
                    dt.DrAmount += fData.DrAmount;
                    dt.CrAmount += fData.CrAmount;
                    dt.Closing += fData.Closing;
                    dt.SalesInvoiceAmount += fData.SalesInvoiceAmount;
                    dt.SalesDiscount += fData.SalesDiscount;
                    dt.SalesExDuty += fData.SalesExDuty;
                    dt.SalesVat += fData.SalesVat;
                    dt.SalesSchame += fData.SalesSchame;
                    dt.SalesFreight += fData.SalesFreight;
                    dt.PurchaseInvoiceAmount += fData.PurchaseInvoiceAmount;
                    dt.PurchaseDiscount += fData.PurchaseDiscount;
                    dt.PurchaseExDuty += fData.PurchaseExDuty;
                    dt.PurchaseVat += fData.PurchaseVat;
                    dt.PurchaseSchame += fData.PurchaseSchame;
                    dt.PurchaseFreight += fData.PurchaseFreight;
                    dt.ActualSales += fData.ActualSales;
                    dt.ActualVat += fData.ActualVat;
                    dt.Insurance += fData.Insurance;
                    dt.TaxAbleSales += fData.TaxAbleSales;
                    dt.NonTaxAbleSales += fData.NonTaxAbleSales;
                    dt.RoundOffAmt += fData.RoundOffAmt;
                    dt.SalesVatAV += fData.SalesVatAV;
                    dt.SalesExciseAV += fData.SalesExciseAV;
                    dt.PurchaseVatAV += fData.PurchaseVatAV;
                    dt.PurchaseExciseAV += fData.PurchaseExciseAV;
                    dt.NonTaxAblePurchase += fData.NonTaxAblePurchase;

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
                Name: 'Total =>',
                DrAmount: 0,
                Opening: ''
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
        $scope.loadingstatus = "stop";


    }

    $scope.ChangeFor = function () {
        if ($scope.accountConfirmation.For == 0) {
            $scope.gridOptions.api.getColumnDef('colR2').headerName = 'Taxable Sales';
            $scope.gridOptions.columnApi.getColumn('colR2').colDef.headerName = 'Taxable Sales';

            $scope.gridOptions.api.getColumnDef('colR3').headerName = 'Taxable SalesReturn';
            $scope.gridOptions.columnApi.getColumn('colR3').colDef.headerName = 'Taxable SalesReturn';

            $scope.gridOptions.api.getColumnDef('colR4').headerName = 'Sales Vat';
            $scope.gridOptions.columnApi.getColumn('colR4').colDef.headerName = 'Sales Vat';

            $scope.gridOptions.api.getColumnDef('colR5').headerName = 'SalesReturn Vat';
            $scope.gridOptions.columnApi.getColumn('colR5').colDef.headerName = 'SalesReturn Vat';

            $scope.gridOptions.api.getColumnDef('colR6').headerName = 'Actual Sales';
            $scope.gridOptions.columnApi.getColumn('colR6').colDef.headerName = 'Actual Sales';

            $scope.gridOptions.api.getColumnDef('colR7').headerName = 'Non Taxable Sales';
            $scope.gridOptions.columnApi.getColumn('colR7').colDef.headerName = 'Non Taxable Sales';

            $scope.gridOptions.api.getColumnDef('colR8').headerName = 'Non Taxable SalesReturn';
            $scope.gridOptions.columnApi.getColumn('colR8').colDef.headerName = 'Non Taxable SalesReturn';
        } else {
            $scope.gridOptions.api.getColumnDef('colR2').headerName = 'Taxable Purchase';
            $scope.gridOptions.columnApi.getColumn('colR2').colDef.headerName = 'Taxable Purchase';

            $scope.gridOptions.api.getColumnDef('colR3').headerName = 'Taxable PurchaseReturn';
            $scope.gridOptions.columnApi.getColumn('colR3').colDef.headerName = 'Taxable PurchaseReturn';

            $scope.gridOptions.api.getColumnDef('colR4').headerName = 'Purchase Vat';
            $scope.gridOptions.columnApi.getColumn('colR4').colDef.headerName = 'Purchase Vat';

            $scope.gridOptions.api.getColumnDef('colR5').headerName = 'PurchaseReturn Vat';
            $scope.gridOptions.columnApi.getColumn('colR5').colDef.headerName = 'PurchaseReturn Vat';

            $scope.gridOptions.api.getColumnDef('colR6').headerName = 'Actual Purchase';
            $scope.gridOptions.columnApi.getColumn('colR6').colDef.headerName = 'Actual Purchase';

            $scope.gridOptions.api.getColumnDef('colR7').headerName = 'Non Taxable Purchase';
            $scope.gridOptions.columnApi.getColumn('colR7').colDef.headerName = 'Non Taxable Purchase';

            $scope.gridOptions.api.getColumnDef('colR8').headerName = 'Non Taxable PurchaseReturn';
            $scope.gridOptions.columnApi.getColumn('colR8').colDef.headerName = 'Non Taxable PurchaseReturn';
        }

        $scope.gridOptions.api.refreshHeader();

        $scope.GetAccountConfirmationLetter();
    }

    $scope.GetAccountConfirmationLetter = function () {

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.accountConfirmation.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.accountConfirmation.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.accountConfirmation.DateToDet)
            dateTo = new Date(($filter('date')($scope.accountConfirmation.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.DataColl = []; //declare an empty array
        $scope.gridOptions.api.setRowData($scope.DataColl);

        var beData = {
            dateFrom: dateFrom,
            dateTo: dateTo,
            IsCreditor: ($scope.accountConfirmation.For == 0 ? false : true),
        };

        $scope.loadingstatus = 'running';
        showPleaseWait();

        $http({
            method: "post",
            url: base_url + "Account/Reporting/GetAccountConfirmationLetter",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();


            var DataColl = mx(res.data.Data);
            var dt = {
                Name: 'TOTAL =>',
                Opening: DataColl.sum(p1 => p1.Opening),
                DrAmount: DataColl.sum(p1 => p1.DrAmount),
                CrAmount: DataColl.sum(p1 => p1.CrAmount),
                Closing: DataColl.sum(p1 => p1.Closing),
                SalesInvoiceAmount: DataColl.sum(p1 => p1.SalesInvoiceAmount),
                SalesDiscount: DataColl.sum(p1 => p1.SalesDiscount),
                SalesExDuty: DataColl.sum(p1 => p1.SalesExDuty),
                SalesVat: DataColl.sum(p1 => p1.SalesVat),
                SalesSchame: DataColl.sum(p1 => p1.SalesSchame),
                SalesFreight: DataColl.sum(p1 => p1.SalesFreight),
                PurchaseInvoiceAmount: DataColl.sum(p1 => p1.PurchaseInvoiceAmount),
                PurchaseDiscount: DataColl.sum(p1 => p1.PurchaseDiscount),
                PurchaseExDuty: DataColl.sum(p1 => p1.PurchaseExDuty),
                PurchaseVat: DataColl.sum(p1 => p1.PurchaseVat),
                PurchaseSchame: DataColl.sum(p1 => p1.PurchaseSchame),
                PurchaseFreight: DataColl.sum(p1 => p1.PurchaseFreight),
                ActualSales: DataColl.sum(p1 => p1.ActualSales),
                ActualVat: DataColl.sum(p1 => p1.ActualVat),
                Insurance: DataColl.sum(p1 => p1.Insurance),
                TaxAbleSales: DataColl.sum(p1 => p1.TaxAbleSales),
                NonTaxAbleSales: DataColl.sum(p1 => p1.NonTaxAbleSales),
                RoundOffAmt: DataColl.sum(p1 => p1.RoundOffAmt),
                SalesVatAV: DataColl.sum(p1 => p1.SalesVatAV),
                SalesExciseAV: DataColl.sum(p1 => p1.SalesExciseAV),
                PurchaseVatAV: DataColl.sum(p1 => p1.PurchaseVatAV),
                PurchaseExciseAV: DataColl.sum(p1 => p1.PurchaseExciseAV),
                NonTaxAblePurchase: DataColl.sum(p1 => p1.NonTaxAblePurchase),
            }

            var filterDataColl = [];
            filterDataColl.push(dt);

            $scope.gridOptionsBottom.api.setRowData(filterDataColl);

            $scope.gridOptions.api.setRowData(res.data.Data);

            $scope.loadingstatus = 'done';

        }, function (errormessage) {

            $scope.loadingstatus = 'stop';

            alert('Unable to Store data. pls try again.' + errormessage.responseText);
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

        var finalFilterData = [];
        var filterData = [];
        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
            var dayBook = node.data;
            filterData.push(dayBook);
        });

        if ($scope.accountConfirmation.GroupBy == 1) {

            var query = mx(filterData).groupBy(t => t.PanVatNo);
            angular.forEach(query, function (q) {
                var mxEL = mx(q.elements);
                var beData = {
                    Address: q.elements[0].Address,
                    Agent: q.elements[0].Agent,
                    Opening: mxEL.sum(p1 => p1.Opening),
                    CrAmount: mxEL.sum(p1 => p1.CrAmount),
                    DrAmount: mxEL.sum(p1 => p1.DrAmount),
                    Closing: mxEL.sum(p1 => p1.Closing),
                    SalesInvoiceAmount: mxEL.sum(p1 => p1.SalesInvoiceAmount),
                    SalesDiscount: mxEL.sum(p1 => p1.SalesDiscount),
                    SalesExDuty: mxEL.sum(p1 => p1.SalesExDuty),
                    SalesVat: mxEL.sum(p1 => p1.SalesVat),
                    SalesSchame: mxEL.sum(p1 => p1.SalesSchame),
                    SalesFreight: mxEL.sum(p1 => p1.SalesFreight),

                    PurchaseInvoiceAmount: mxEL.sum(p1 => p1.PurchaseInvoiceAmount),
                    PurchaseDiscount: mxEL.sum(p1 => p1.PurchaseDiscount),
                    PurchaseExDuty: mxEL.sum(p1 => p1.PurchaseExDuty),
                    PurchaseVat: mxEL.sum(p1 => p1.PurchaseVat),
                    PurchaseSchame: mxEL.sum(p1 => p1.PurchaseSchame),
                    PurchaseFreight: mxEL.sum(p1 => p1.PurchaseFreight),

                    ActualSales: mxEL.sum(p1 => p1.ActualSales),
                    ActualVat: mxEL.sum(p1 => p1.ActualVat),
                    TaxAbleSales: mxEL.sum(p1 => p1.TaxAbleSales),
                    NonTaxAbleSales: mxEL.sum(p1 => p1.NonTaxAbleSales),
                    RoundOffAmt: mxEL.sum(p1 => p1.RoundOffAmt),
                    SalesVatAV: mxEL.sum(p1 => p1.SalesVatAV),
                    SalesExciseAV: mxEL.sum(p1 => p1.SalesExciseAV),
                    PurchaseVatAV: mxEL.sum(p1 => p1.PurchaseVatAV),
                    PurchaseExciseAV: mxEL.sum(p1 => p1.PurchaseExciseAV),
                    NonTaxAblePurchase: mxEL.sum(p1 => p1.NonTaxAblePurchase),

                    GroupName: q.elements[0].GroupName,
                    LedgerId: q.elements[0].LedgerId,
                    Name: q.elements[0].Name,

                    PanVatNo: q.elements[0].PanVatNo,

                };
                finalFilterData.push(beData);
            })

        } else if ($scope.accountConfirmation.GroupBy == 2) {
            var query = mx(filterData).groupBy(t => t.Name);
            angular.forEach(query, function (q) {
                var mxEL = mx(q.elements);
                var beData = {
                    Address: q.elements[0].Address,
                    Agent: q.elements[0].Agent,
                    Closing: mxEL.sum(p1 => p1.Closing),
                    CrAmount: mxEL.sum(p1 => p1.CrAmount),
                    DrAmount: mxEL.sum(p1 => p1.DrAmount),
                    GroupName: q.elements[0].GroupName,
                    LedgerId: q.elements[0].LedgerId,
                    Name: q.elements[0].Name,
                    Opening: mxEL.sum(p1 => p1.Opening),
                    PanVatNo: q.elements[0].PanVatNo,
                    PurchaseDiscount: mxEL.sum(p1 => p1.PurchaseDiscount),
                    PurchaseExDuty: mxEL.sum(p1 => p1.PurchaseExDuty),
                    PurchaseFreight: mxEL.sum(p1 => p1.PurchaseFreight),
                    PurchaseInvoiceAmount: mxEL.sum(p1 => p1.PurchaseInvoiceAmount),
                    PurchaseSchame: mxEL.sum(p1 => p1.PurchaseSchame),
                    PurchaseVat: mxEL.sum(p1 => p1.PurchaseVat),
                    SalesDiscount: mxEL.sum(p1 => p1.SalesDiscount),
                    SalesExDuty: mxEL.sum(p1 => p1.SalesExDuty),
                    SalesFreight: mxEL.sum(p1 => p1.SalesFreight),
                    SalesInvoiceAmount: mxEL.sum(p1 => p1.SalesInvoiceAmount),
                    SalesSchame: mxEL.sum(p1 => p1.SalesSchame),
                    SalesVat: mxEL.sum(p1 => p1.SalesVat),
                    ActualSales: mxEL.sum(p1 => p1.ActualSales),
                    ActualVat: mxEL.sum(p1 => p1.ActualVat),
                    TaxAbleSales: mxEL.sum(p1 => p1.TaxAbleSales),
                    NonTaxAbleSales: mxEL.sum(p1 => p1.NonTaxAbleSales),
                    RoundOffAmt: mxEL.sum(p1 => p1.RoundOffAmt),
                    SalesVatAV: mxEL.sum(p1 => p1.SalesVatAV),
                    SalesExciseAV: mxEL.sum(p1 => p1.SalesExciseAV),
                    PurchaseVatAV: mxEL.sum(p1 => p1.PurchaseVatAV),
                    PurchaseExciseAV: mxEL.sum(p1 => p1.PurchaseExciseAV),
                    NonTaxAblePurchase: mxEL.sum(p1 => p1.NonTaxAblePurchase),
                };
                finalFilterData.push(beData);
            })
        }
        else {
            finalFilterData = filterData;
        }


        return finalFilterData;

    };

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

});


app.controller("userLogCntrl", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'userLog.csv',
            sheetName: 'userLog'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

 
    function DateFormatAD(date) {

        if (date)
            return $filter('date')(date, 'yyyy-MM-dd');
        return '';
    };
    function padLeft(nr, n, str) {

        if (nr && n)
            return Array(n - String(nr).length + 1).join(str || '0') + nr;
        return '';
    };
    function DateFormatBS(ny, nm, nd) {
        if (ny && nm && nd)
            return ny + '-' + padLeft(nm, 2) + '-' + padLeft(nd, 2);
        return '';
    };
    function LoadData() {

        $scope.BranchList = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllBranchList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BranchList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.userLog = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            userId: 0,
            entityId: 0,
            action:0
        };

        $scope.ReportName = '';
        $scope.noofdecimal = 2;

        $scope.loadingstatus = "stop";

        var columnDefs = [
            { headerName: "FiscalYear", width: 120, field: "FYear" },
            { headerName: "Bill No.", width: 80, field: "BillNo" },
            { headerName: "Customer Name", width: 180, field: "PartyName" },
            { headerName: "Customer PAN", width: 120, field: "PanVatNo" },
            { headerName: "Bill Date", width: 120, field: "VoucherDateBS" },
            {
                headerName: "Amount", width: 150, filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.Amount;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            {
                headerName: "Discount", width: 150, filter: "agNumberColumnFilter",
                field: "Discount",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            {
                headerName: "Taxable Amt.", width: 150, filter: "agNumberColumnFilter",
                field: "TaxAbleAmount",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            {
                headerName: "Vat Amt.", width: 150, filter: "agNumberColumnFilter",
                field: "Vat",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            {
                headerName: "Total Amt.", width: 150, filter: "agNumberColumnFilter",
                field: "TotalAmount",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            {
                headerName: "Sync with IRD", width: 120, field: "SyncWithIRD",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.SyncWithIRD == true)
                        return 'Yes';
                    else
                        return 'No'
                },
            },
            {
                headerName: "IsPrinted", width: 120, field: "IsPrinted",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.IsPrinted == true)
                        return 'Yes';
                    else
                        return 'No'
                },
            },
            {
                headerName: "IsActive", width: 120, field: "IsActive",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.IsActive == true)
                        return 'Yes';
                    else
                        return 'No'
                },
            },
            { headerName: "Print DateTime", width: 120, field: "PrintDateTime" },
            { headerName: "Entered By", width: 120, field: "EnterBy" },
            { headerName: "Print By", width: 120, field: "PrintBy" },
            {
                headerName: "Is RealTime", width: 120, field: "IsRealTime",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.IsRealTime == true)
                        return 'Yes';
                    else
                        return 'No'
                },
            },

        ];


        $scope.gridOptions = {
            // a default column definition with properties that get applied to every column
            angularCompileRows: true,
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,

                // set every column width
                width: 90
            },
            columnDefs: columnDefs,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            rowSelection: 'multiple',
        };


    }

    $scope.GetUserLog = function () {

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.userLog.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.userLog.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.userLog.DateToDet)
            dateTo = new Date(($filter('date')($scope.userLog.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.DataColl = []; //declare an empty array
        $scope.gridOptions.api.setRowData($scope.DataColl);

        var beData = {
            DateFrom: dateFrom,
            DateTo: dateTo,
            userId: 0,
            entityId: 0,
            action: 0
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Account/Reporting/GetuserLog",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            $scope.DataColl = res.data.Data;
            $scope.gridOptions.api.setRowData($scope.DataColl);

            $scope.loadingstatus = 'done';

        }, function (errormessage) {

            $scope.loadingstatus = 'stop';

            alert('Unable to Store data. pls try again.' + errormessage.responseText);
        });

    };

    $scope.Print = function () {
        $http({
            method: 'GET',
            url: base_url + "ReportEngine/GetReportTemplates?entityId=" + EntityId + "&voucherId=0&isTran=true",
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
                                                url: base_url + "Account/Reporting/PrintuserLog",
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
                        var dataColl = [];
                        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                            dataColl.push(node.data);
                        });
                        print = true;

                        $http({
                            method: 'POST',
                            url: base_url + "Account/Reporting/PrintuserLog",
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


    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }


});

