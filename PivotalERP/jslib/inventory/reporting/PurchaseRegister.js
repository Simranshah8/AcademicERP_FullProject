"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller('PurchaseRegisterController', function ($scope, $http, $timeout, $filter, GlobalServices, $compile) {
	$scope.Title = 'Purchase Register';
    getterAndSetter();

	function getterAndSetter() {
        var columnDefs = [
            {
                headerName: "Date(A.D.)", width: 160, field: "VoucherDate", cellRenderer: 'agGroupCellRenderer',
                valueFormatter: function (params) { return DateFormatAD(params.value); },
                showRowGroup: true,
                cellRendererParams: {
                    suppressCount: false, // turn off the row count                   
                }
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
                    if (params.node.rowPinned) {
                        return  '';
                    }
                    var beData = params.data;

                    if (beData.Party && beData.Party.length>0) {
                        return beData.Party;
                    }
                    else if (beData.ProductName && beData.ProductName.length > 0)
                        return "       " + beData.ProductName + " : " + beData.Alias + " : " + beData.Code + " : " + beData.PartNo;
                    else if (beData.LedgerName && beData.LedgerName.length > 0)
                        return beData.LedgerName;
                    else
                        return params.data;
                }
            },
            {
                headerName: "Purchase Ledger", width: 120,
                valueGetter: function (params) {
                    var beData = params.data;

                    if (beData.TranLedger && beData.TranLedger.length > 0) {
                        return beData.TranLedger;
                    }
                    else if (beData.ProductName && beData.ProductName.length > 0)
                        return beData.ActualQty + " : " + beData.Unit + " @ " + beData.Rate ;                    
                    else
                        return '';
                }
            },
            {
                headerName: "Amount", width: 150, filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    if (params.node.rowPinned) {
                        return params.data.TotalProductAmount || 0;
                    }
                    return params.data.TotalProductAmount || 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); },
                cellStyle: { 'text-align': 'right' }
            },
            //{
            //    headerName: "Amount", width: 150, filter: "agNumberColumnFilter",
            //    valueGetter: function (params) {
            //        if (params.node.rowPinned) {
            //            return params.data.Debit || 0;
            //        }
            //        var beData = params.data;
            //        return beData.TotalProductAmount;
            //    },
            //    valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            //},
            { headerName: "VoucherType", width: 120, field: "VoucherName" },
            { headerName: "Voucher No.", width: 120, field: "AutoManualNo" },
            { headerName: "Ref.No.", width: 120, field: "RefNo" },
            //{ headerName: "PartyCostCenter", width: 120, field: "PartyCostCenter" },
            //{ headerName: "TranCostCenter", width: 120, field: "TranCostCenter" },
            {
                headerName: "Debit", width: 150, filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    if (params.node.rowPinned) {
                        return params.data.Debit || 0;
                    }

                    var beData = params.data;

                    if (beData.VoucherType=='PurchaseInvoice') {
                        return beData.TotalAmount;
                    }  
                    else
                        return 0;

                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }
            },
            {
                headerName: "Credit", width: 150, filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    if (params.node.rowPinned) {
                        return params.data.Credit || 0;
                    }

                    var beData = params.data;
                    if (beData.VoucherType == 'PurchaseReturn') {
                        return beData.TotalAmount;
                    }
                    else
                        return 0;

                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }
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
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }
            },
            { headerName: "CostClass", width: 120, field: "CostClass" },
            
            
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
            headerHeight: 35,
            rowHeight: 33,
            columnDefs: columnDefs,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            rowSelection: 'multiple',
            overlayLoadingTemplate: "Please Click the Load Button to display the data.",
           
            getNodeChildDetails: function (beData) {
                var dataColl = [];

                if (beData.ItemDetailsColl && beData.ItemDetailsColl.length>0) {
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
                    dataColl.push('('+beData.Narration+')');
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
                    Amount: 0,
                    Debit: 0,
                    Credit: 0,
                    Vat: 0
                };

                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var d = node.data;
                    dt.Amount += d.TotalProductAmount || 0; // <-- add this
                    if (d.VoucherType === 'PurchaseInvoice') dt.Debit += d.TotalAmount || 0;
                    if (d.VoucherType === 'PurchaseReturn') dt.Credit += d.TotalAmount || 0;
                    dt.Vat += d.Vat || 0;
                });
                $scope.gridOptions.api.setPinnedBottomRowData([dt]);

            },

        };


    }
	   
	
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.searchData = {
			PurchaseRegister: '',
		};

        $scope.dayBook = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            Mode: 'Save'
        };
		$scope.newPurchaseRegister = {
			PurchaseRegisterId: null,
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
			Mode: 'Save'
		};

		//$scope.GetAllPurchaseRegisterList();
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
            dateFrom: dateFrom,
            dateTo: dateTo 
        };
        $scope.loadingstatus = "running";
        showPleaseWait();

        $http({
            method: "post",
            url: base_url + "Inventory/Reporting/GetPurchaseRegister",
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
                if (dc.VoucherType === 'PurchaseInvoice')
                    dt.Debit += dc.TotalAmount || 0;
                if (dc.VoucherType === 'PurchaseReturn')
                    dt.Credit += dc.TotalAmount || 0;
                dt.Vat += dc.Vat || 0;
            });

            $scope.gridOptions.api.setRowData(DataColl);
            $scope.gridOptions.api.setPinnedBottomRowData([dt]);

            //$scope.DataColl = res.data.Data;
            //$scope.gridOptions.api.setRowData($scope.DataColl);

            $scope.loadingstatus = 'done';
            hidePleaseWait();

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

        var RptParamentersColl = [];

        RptParamentersColl.push({
            Name: "Period",
            Value: $('#dtDateFrom').val() + ' To ' + $('#dtDateTo').val()
        });


        var filterData = [];

        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
            var dayBook = node.data;
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
            //beData.VoucherDateStr = GlobalFunction.GetDateStr(beData.VoucherDate, Dynamic.Windows.Forms.Windows.Forms.SDDatePicker.BaseDate.EnglishDate);
            beData.VoucherDateStrNP = DateFormatBS(beData.NY, beData.NM, beData.ND);
            beData.CreatedByName = dayBook.CreatedByName;

            if (beData.IsInventory == true) {
                beData.Particulars = dayBook.PartyLedger;
                beData.DrAmount = dayBook.DrAmount;
                beData.CrAmount = dayBook.CrAmount;
                filterData.push(beData);

                var ledData = {};
                ledData.Particulars = "  " + dayBook.Particulars;

                if (dayBook.DrAmount != 0)
                    ledData.CrAmount = dayBook.DrAmount - mx(dayBook.AditionalCostColl).Sum(p1 => p1.Amount);
                else
                    ledData.DrAmount = dayBook.CrAmount - mx(dayBook.AditionalCostColl).Sum(p1 => p1.Amount);

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
                    filterData.push(addData);
                });


                angular.forEach(dayBook.ItemAllocationColl, function (item) {

                    var itemData = {};
                    itemData.Particulars = "    " + item.ProductName + " ( " + item.BilledQty + item.UnitName + " @ " + item.Rate + " = " + item.Amount + " )";
                    filterData.push(itemData);

                });

            } else {
                var firstTime = true;
                angular.forEach(dayBook.LedgerAllocationColl, function (ledAll) {
                    if (firstTime) {
                        beData.Particulars = ledAll.LedgerName;
                        beData.DrAmount = ledAll.DrAmount;
                        beData.CrAmount = ledAll.CrAmount;
                        firstTime = false;

                        filterData.push(beData);
                    }
                    else {
                        var chieldData = {};
                        chieldData.Particulars = "  " + ledAll.LedgerName;
                        chieldData.Narration = ledAll.Narration;
                        chieldData.DrAmount = ledAll.DrAmount;
                        chieldData.CrAmount = ledAll.CrAmount;
                        filterData.push(chieldData);
                    }
                });
            }


        });

        return filterData;

    };

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
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