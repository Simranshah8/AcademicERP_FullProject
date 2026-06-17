"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller('SalesRegisterController', function ($scope, $http, $timeout, $filter, GlobalServices, $compile) {
    $scope.Title = 'Sales Register';
    getterAndSetter();

    function getterAndSetter() {
        $scope.columnDefs = [
            {
                headerName: "Date(A.D.)", width: 120, field: "VoucherDate", cellRenderer: 'agGroupCellRenderer',
                valueFormatter: function (params) { return DateFormatAD(params.value); },
                showRowGroup: true,
                cellRendererParams: {
                    suppressCount: false,
                }
            },
            {
                headerName: "Date(B.S.)", width: 120,
                cellRenderer: function (params) {
                    return DateFormatBS(params.data.NY, params.data.NM, params.data.ND);
                }
            },
            {
                headerName: "Particular's", width: 180,
                valueGetter: function (params) {
                    var beData = params.data;

                    if (beData.Party && beData.Party.length > 0) {
                        return beData.Party;
                    }
                    else if (beData.ProductName && beData.ProductName.length > 0)
                        return "       " + beData.ProductName + " : " + beData.Alias + " : " + beData.Code + " : " + beData.PartNo;
                    else if (beData.LedgerName && beData.LedgerName.length > 0)
                        return beData.LedgerName;
                    else
                        return '';
                }
            },
            {
                headerName: "Sales Ledger", width: 150,
                valueGetter: function (params) {
                    var beData = params.data;

                    if (beData.TranLedger && beData.TranLedger.length > 0) {
                        return beData.TranLedger;
                    }
                    else if (beData.ProductName && beData.ProductName.length > 0)
                        return beData.ActualQty + " : " + beData.Unit + " @ " + beData.Rate;
                    else
                        return '';
                }
            },
            {
                headerName: "Amount", width: 150, filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    // Handle pinned row
                    if (params.node.rowPinned) {
                        return params.data.TotalProductAmount || 0;
                    }

                    var beData = params.data;
                    return beData.TotalProductAmount;
                },
                valueFormatter: function (params) { return Numberformat(params.value); },
                cellStyle: { 'text-align': 'right' }
            },

            { headerName: "VoucherType", width: 150, field: "VoucherName" },
            { headerName: "Voucher No.", width: 150, field: "AutoManualNo" },
            { headerName: "Ref.No.", width: 120, field: "RefNo" },
            {
                headerName: "Debit", width: 150, filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    if (params.node.rowPinned) {
                        return params.data.Debit || 0;
                    }

                    var beData = params.data;
                    if (beData.VoucherType == 'SalesInvoice') {
                        return beData.TotalAmount;
                    }
                    return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); },
                cellStyle: { 'text-align': 'right' }
            },
            {
                headerName: "Credit", width: 150, filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    if (params.node.rowPinned) {
                        return params.data.Credit || 0;
                    }

                    var beData = params.data;
                    if (beData.VoucherType == 'SalesReturn') {
                        return beData.TotalAmount;
                    }
                    return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); },
                cellStyle: { 'text-align': 'right' }
            },
            {
                headerName: "Vat", width: 150, filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    if (params.node.rowPinned) {
                        return params.data.Vat || 0;
                    }

                    var beData = params.data;
                    return beData.Vat;
                },
                valueFormatter: function (params) { return Numberformat(params.value); },
                cellStyle: { 'text-align': 'right' }
            },

            //{
            //    headerName: "Debit", width: 150, filter: "agNumberColumnFilter",
            //    valueGetter: function (params) {
            //        var beData = params.data;

            //        if (beData.VoucherType == 'SalesInvoice') {
            //            return beData.TotalAmount;
            //        }
            //        else
            //            return 0;
            //    },
            //    valueFormatter: function (params) { return Numberformat(params.value); },
            //    cellStyle: { 'text-align': 'right' }
            //},
            //{
            //    headerName: "Credit", width: 150, filter: "agNumberColumnFilter",
            //    valueGetter: function (params) {
            //        var beData = params.data;
            //        if (beData.VoucherType == 'SalesReturn') {
            //            return beData.TotalAmount;
            //        }
            //        else
            //            return 0;
            //    },
            //    valueFormatter: function (params) { return Numberformat(params.value); },
            //    cellStyle: { 'text-align': 'right' }
            //},
            //{
            //    headerName: "Vat", width: 150, filter: "agNumberColumnFilter",
            //    valueGetter: function (params) {
            //        var beData = params.data;
            //        return beData.Vat;
            //    },
            //    valueFormatter: function (params) { return Numberformat(params.value); },
            //    cellStyle: { 'text-align': 'right' }
            //},
            { headerName: "CostClass", width: 130, field: "CostClass" },
        ];

        $scope.gridOptions = {
            angularCompileRows: true,
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                width: 90
            },
            headerHeight: 35,
            rowHeight: 33,
            columnDefs: $scope.columnDefs,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            rowSelection: 'multiple',
            overlayLoadingTemplate: "Please Click the Load Button to display the data.",
           

            getNodeChildDetails: function (beData) {
                var dataColl = [];

                if (beData.ItemDetailsColl && beData.ItemDetailsColl.length > 0) {
                    angular.forEach(beData.ItemDetailsColl, function (data) {
                        dataColl.push(data);
                    });
                }

                if (beData.AditionalCostColl && beData.AditionalCostColl.length > 0) {
                    angular.forEach(beData.AditionalCostColl, function (data) {
                        dataColl.push(data);
                    });
                }
                if (beData.Narration && beData.Narration.length > 0) {
                    dataColl.push('(' + beData.Narration + ')');
                }

                if (dataColl.length > 0) {
                    return {
                        group: true,
                        children: dataColl,
                        expanded: beData.open
                    };
                } else
                    return null;
            },
            getRowStyle: function (params) {
                if (params.node.rowPinned) {
                    return {
                        fontWeight: 'bold'
                    };
                }
            },
            onFilterChanged: function () {
                var dt = {
                    TranLedger: "TOTAL =>",
                    TotalProductAmount: 0,
                    Debit: 0,
                    Credit: 0,
                    Vat: 0
                };

                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var d = node.data;
                    dt.TotalProductAmount += d.TotalProductAmount || 0;
                    if (d.VoucherType === 'SalesInvoice') dt.Debit += d.TotalAmount || 0;
                    if (d.VoucherType === 'SalesReturn') dt.Credit += d.TotalAmount || 0;
                    dt.Vat += d.Vat || 0;
                });

                $scope.gridOptions.api.setPinnedBottomRowData([dt]);
            }
        };
    }

    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.searchData = {
            SalesRegister: '',
        };

        $scope.dayBook = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            Mode: 'Save'
        };
        $scope.newSalesRegister = {
            SalesRegisterId: null,
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            Mode: 'Save'
        };
    };

    $scope.GetDayBook = function () {
        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.dayBook.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.dayBook.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.dayBook.DateToDet)
            dateTo = new Date(($filter('date')($scope.dayBook.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.DataColl = [];
        $scope.gridOptions.api.setRowData($scope.DataColl);

        var beData = {
            dateFrom: dateFrom,
            dateTo: dateTo
        };

        $scope.loadingstatus = "running";
        showPleaseWait();

        $http({
            method: "post",
            url: base_url + "Inventory/Reporting/GetSalesRegister",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {
            var DataColl = res.data.Data;

            var dt = {
                TranLedger: "TOTAL =>",
                TotalProductAmount: 0,
                Debit: 0,
                Credit: 0,
                Vat: 0
            };

            angular.forEach(DataColl, function (dc) {
                dt.TotalProductAmount += dc.TotalProductAmount || 0;
                if (dc.VoucherType === 'SalesInvoice')
                    dt.Debit += dc.TotalAmount || 0;
                if (dc.VoucherType === 'SalesReturn')
                    dt.Credit += dc.TotalAmount || 0;
                dt.Vat += dc.Vat || 0;
            });

            $scope.gridOptions.api.setRowData(DataColl);
            $scope.gridOptions.api.setPinnedBottomRowData([dt]);

            $scope.loadingstatus = 'done';
            hidePleaseWait();
        }, function (errormessage) {
            $scope.loadingstatus = 'stop';
            alert('Unable to Store data. pls try again.' + errormessage.responseText);
        });
    };

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'Groupwise.csv',
            sheetName: 'Groupwise'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    };

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    };

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
