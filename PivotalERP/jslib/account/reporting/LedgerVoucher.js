app.controller("LedgerVoucherCntrl", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();
    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'LedgerVoucher.csv',
            sheetName: 'LedgerVoucher'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }
    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    function LoadData() {
        $('.select2').select2({
            allowClear: true,
            openOnEnter: true
        });

        $scope.InterestCalculationOnColl = [{ id: 1, text: 'Debit Balance' }, { id: 2, text: 'Credit Balance' }];

        $scope.LedgerVoucher = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            VoucherId: 0,
            IsPost: true,
            BranchId: 0,
            IsSummary: true,
            LedgerDetails: {}
        };

        $scope.comDet = {};
        $timeout(function () {
            $http({
                method: "GET",
                url: base_url + "Global/GetCompanyDetail",
                dataType: "json"
            }).then(function (res) {
                $scope.comDet = res.data.Data;
                if ($scope.comDet) {
                    $scope.LedgerVoucher.DateFrom_TMP = new Date($scope.comDet.StartDate);
                }
            }, function (errormessage) {
                alert('Unable to Delete data. pls try again.' + errormessage.responseText);
            });
        });

        $scope.LedgerTypeList = [];
        $http({
            method: 'GET',
            url: base_url + "Global/GetLedgerType",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.LedgerTypeList = res.data.Data;
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

        $scope.ReportName = '';

        $scope.noofdecimal = 2;

        $scope.loadingstatus = "stop";


        $scope.columnDefs = [
            {
                headerName: "Date", width: 140, dataType: 'DateTime', cellRenderer: 'agGroupCellRenderer',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.IsParent) {
                        return beData.VoucherDate;
                    }
                    return null;
                },
                valueFormatter: function (params) { return DateFormatAD(params.value); },
                filter: 'agDateColumnFilter', pinned: 'left'
            },
            {
                headerName: "Miti", width: 110, dataType: 'DateTime', valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.IsParent) {
                        return beData.NVoucherDate;
                    } else {
                        return "";
                    }
                    //return DateFormatBS(params.data.NY, params.data.NM, params.data.ND);
                },
                filter: 'agTextColumnFilter', pinned: 'left'
            },
            {
                headerName: "Particular's", width: 230, dataType: 'Text',
                valueGetter: function (params) {
                    var beData = params.data;

                    if (beData.IsParent) {
                        return beData.Particulars;
                    }
                    else {
                        if (beData.RowType) {

                            if (beData.RowType == 'LedgerAllocation') {
                                return "  " + beData.LedgerName;
                            } else if (beData.RowType == 'ItemAllocation') {
                                return "  => " + beData.ProductName;
                            } else if (beData.RowType == 'BillDetails') {
                                return "  " + "(" + beData.VoucherDetails + " :- Rs." + beData.Amount + " / " + beData.Remarks + " ) ";;
                            }
                            else
                                return params.data;

                        } else
                            return params.data;
                    }

                },
                filter: 'agTextColumnFilter', pinned: 'left'
            },
            { headerName: "VoucherType", width: 130, field: "VoucherName", dataType: 'Text', filter: 'agTextColumnFilter', },
            { headerName: "Voucher No.", width: 130, field: "AutoManualNo", dataType: 'Text', filter: 'agTextColumnFilter', },
            { headerName: "Ref.No.", width: 120, field: "RefNo", dataType: 'Text', filter: 'agTextColumnFilter', },



            {
                headerName: "Debit", width: 150, dataType: 'Number', filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    var beData = params.data;

                    if (beData.IsParent==true) {
                        return beData.DebitAmt;
                    }
                    else
                        return 0;

                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "Credit", width: 150, dataType: 'Number', filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.IsParent==true) {
                        return beData.CreditAmt;
                    }
                    else
                        return 0;

                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "Current Closing", width: 150, dataType: 'Number', filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.IsParent==true) {
                        return beData.CurrentClosing;
                    }
                    return 0;
                },
                valueFormatter: function (params) { return NumberformatAC(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "Qty", width: 110, dataType: 'Number', filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.RowType == 'ItemAllocation') {
                        return beData.AQty + ' ' + beData.UnitName;
                    }
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "Rate", width: 130, dataType: 'Number', filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.RowType == 'ItemAllocation') {
                        return beData.Rate;
                    } else
                        return '';

                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "Amount", width: 140, dataType: 'Number', filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.RowType == 'ItemAllocation') {
                        return beData.Amount;
                    } else if (beData.RowType == 'LedgerAllocation') {
                        return beData.DebitAmt - beData.CreditAmt;
                    }
                    else
                        return '';
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            { headerName: "CostClass", width: 120, dataType: 'Text', field: "CostClassName" },
            { headerName: "User", width: 120, dataType: 'Text', field: "UserName" },
            { headerName: "Narration", width: 150, dataType: 'Text', field: "Narration" },
            {
                headerName: "Age", width: 110, dataType: 'Number', filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.IsParent == true && beData.VoucherAge) {
                        return beData.VoucherAge;
                    } else
                        return '';
                },
                cellStyle: { 'text-align': 'center' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "Dues Amt.", width: 120, dataType: 'Number', filter: "agNumberColumnFilter",
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.IsParent == true) {
                        return beData.DuesAmt;
                    } else
                        return '';
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "Action", width: 95, cellRenderer:
                    function (params) {

                        var voucherName = params.data.VoucherName;

                        if (voucherName) {

                            if (params.data.VoucherType < 5) {
                                return '<a class="btn btn-default btn-xs" ng-click="ShowDocument(this.data)"><i class="fas fa-file text-info"></i></a> <a class="btn btn-default btn-xs" ng-click="PrintVoucher(' + params.data.TranId + ',' + params.data.VoucherType + ',' + params.data.VoucherId + ')"><i class="fas fa-print text-info"></i></a>';
                            } else {
                                return '<a class="btn btn-default btn-xs" ng-click="ShowDocument(this.data)"><i class="fas fa-file text-info"></i></a> <a class="btn btn-default btn-xs" ng-click="PrintVoucher(' + params.data.TranId + ',' + params.data.VoucherType + ',' + params.data.VoucherId + ')"><i class="fas fa-print text-info"></i></a>';
                            }

                            //if (params.data.VoucherType < 5) {
                            //    return '<a class="btn btn-default btn-xs" href="' + base_url + 'Account/Transaction/' + voucherName + '?TranId={{' + params.data.TranId + '}}"><i class="fas fa-edit text-info"></i></a>' +
                            //        '<a class="btn btn-default btn-xs" ng-click="PrintVoucher(' + params.data.TranId + ',' + params.data.VoucherType + ',' + params.data.VoucherId + ')"><i class="fas fa-print text-info"></i></a>' +
                            //        '<a class="btn btn-default btn-xs" ng-click="deleteVoucher(' + params.data.TranId + ',' + params.data.VoucherType + ',' + params.data.VoucherId + ',\'' + voucherName + '\'' + ', \'' + params.data.AutoManualNo + '\')"><i class="fas fa-trash-alt text-danger"></i></a>';
                            //} else {
                            //    return '<a class="btn btn-default btn-xs" href="' + base_url + 'Inventory/Transaction/' + voucherName + '?TranId={{' + params.data.TranId + '}}"><i class="fas fa-edit text-info"></i></a>' +
                            //        '<a class="btn btn-default btn-xs" ng-click="PrintVoucher(' + params.data.TranId + ',' + params.data.VoucherType + ',' + params.data.VoucherId + ')"><i class="fas fa-print text-info"></i></a>' +
                            //        '<a class="btn btn-default btn-xs" ng-click="deleteVoucher(' + params.data.TranId + ',' + params.data.VoucherType + ',' + params.data.VoucherId + ',\'' + voucherName + '\'' + ', \'' + params.data.AutoManualNo + '\')"><i class="fas fa-trash-alt text-danger"></i></a>';
                            //}

                        } else {
                            return '';
                        }
                    }, pinned: 'right'
            },
            { headerName: "Modify By", width: 100, field: "ModifyBy" },
            { headerName: "Payment Term", width: 150, field: "TermsOfPayment_BankName" },
            { headerName: "Branch", width: 150, field: "Branch" },

            //OTHERS=0,VAT = 1,TSC = 2,EXCISE = 3,CST = 4,TDS = 5,SCHEME = 6,FREIGHT = 7,INSURANCE = 8,ROUNDOFF = 9,DISCOUNT = 10,
            //LABOURCHARGE = 11,EXTRACHARGE = 12,ASSETS = 13,CUSTOM_SERVICE_CHARGE = 14,IMPORT_DUTY = 15,ADITIONAL_IMPORT_DUTY = 16,
            //SGST = 17,CGST = 18,LoadingUnLoading = 19,OTHERS1 = 20,OTHERS2 = 21,OTHERS3 = 22,OTHERS4 = 23,OTHERS5 = 24,IGST = 25,CUSTOMER = 26,VENDOR = 27

            {
                headerName: "OTHERS Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet0',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[0];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "VAT Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet1',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[1];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "TSC Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet2',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[2];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "EXCISE Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet3',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[3];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "CST Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet4',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[4];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "TDS Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet5',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[5];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "SCHEME Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet6',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[6];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "FREIGHT Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet7',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[7];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "INSURANCE Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet8',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[8];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },

            {
                headerName: "ROUNDOFF Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet9',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[9];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "DISCOUNT Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet10',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[10];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "LABOURCHARGE Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet11',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[11];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "EXTRACHARGE Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet12',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[12];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "ASSETS Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet13',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[13];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },


            {
                headerName: "CUSTOM_SERVICE_CHARGE Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet14',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[14];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "IMPORT_DUTY Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet15',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[15];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "ADITIONAL_IMPORT_DUTY Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet16',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[16];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "SGST Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet17',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[17];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "CGST Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet18',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[18];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },

            {
                headerName: "LoadingUnLoading Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet19',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[19];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "OTHERS1 Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet20',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[20];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "OTHERS2 Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet21',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[21];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "OTHERS3 Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet22',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[22];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "OTHERS4 Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet23',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[23];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "OTHERS5 Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet24',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[24];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "IGST Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet25',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[25];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "CUSTOMER Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet26',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[26];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },
            {
                headerName: "VENDOR Amt.", width: 140, dataType: 'Number', filter: "agNumberColumnFilter", hide: true, colId: 'ledDet27',
                valueGetter: function (params) {
                    var beData = params.data;
                    if (beData.VoucherAllocationColl && beData.VoucherAllocationColl.length > 0) {
                        return beData.VoucherAllocationColl[27];
                    }
                    else
                        return 0;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>',
                filter: 'agNumberColumnFilter',
            },

        ];


        $scope.gridOptions = {
            angularCompileRows: true,
            // a default column definition with properties that get applied to every column
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,

                // set every column width
                width: 90
            },
            headerHeight: 35,
            rowHeight: 30,
            columnDefs: $scope.columnDefs,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            rowSelection: 'multiple',
            overlayLoadingTemplate: "Please Click the Load Button to display the data.",
            suppressHorizontalScroll: true,
            alignedGrids: [],
            onFilterChanged: function (e) {
                //console.log('onFilterChanged', e);
                var drAmt = 0, crAmt = 0;
                var oDr = $scope.dataForBottomGrid[0].DebitAmt;
                var oCr = $scope.dataForBottomGrid[0].CreditAmt;

                $scope.dataForBottomGrid[1].DebitAmt = 0;
                $scope.dataForBottomGrid[1].CreditAmt = 0;
                $scope.dataForBottomGrid[2].DebitAmt = 0;
                $scope.dataForBottomGrid[2].CreditAmt = 0;

                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var tb = node.data;
                    if (tb.IsParent == true) {
                        drAmt += tb.DebitAmt;
                        crAmt += tb.CreditAmt;
                    }
                });

                var closingAmt = oDr - oCr + drAmt - crAmt;
                $scope.dataForBottomGrid[1].DebitAmt = drAmt;
                $scope.dataForBottomGrid[1].CreditAmt = crAmt;

                if (closingAmt > 0)
                    $scope.dataForBottomGrid[2].DebitAmt = closingAmt;
                else
                    $scope.dataForBottomGrid[2].CreditAmt = Math.abs(closingAmt);

                $scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);

            },
            getNodeChildDetails: function (beData) {
                var dataColl = [];
                if (beData.IsParent == true) {

                    if (beData.LedgerNarration && beData.LedgerNarration.length > 0)
                        dataColl.push("(" + beData.LedgerNarration + ")");

                    if (beData.CostCenterColl && beData.CostCenterColl.length > 0) {
                        angular.forEach(beData.CostCenterColl, function (data) {
                            data.RowType = 'LedgerAllocation';
                            dataColl.push(data);
                        });
                    }

                    if (beData.ChieldColl && beData.ChieldColl.length > 0) {
                        angular.forEach(beData.ChieldColl, function (data) {
                            data.RowType = 'LedgerAllocation';
                            dataColl.push(data);
                        });
                    }

                    if (beData.InventoryDetailsColl && beData.InventoryDetailsColl.length > 0) {
                        angular.forEach(beData.InventoryDetailsColl, function (data) {
                            data.RowType = 'ItemAllocation';
                            dataColl.push(data);
                        });
                    }

                    if (beData.AccountBillDetailsColl && beData.AccountBillDetailsColl.length > 0) {
                        angular.forEach(beData.AccountBillDetailsColl, function (data) {
                            data.RowType = 'BillDetails';
                            dataColl.push(data);
                        });
                    }

                    if (beData.Narration && beData.Narration.length > 0)
                        dataColl.push("(" + beData.Narration + ")");
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

        };


        $scope.dataForBottomGrid = [
            {
                IsParent: true,
                DateAD: '',
                DateBS: '',
                Particulars: 'Opening Balance =>',
                VoucherType: '',
                VoucherNo: '',
                RefNo: '',
                DebitAmt: 0,
                CreditAmt: 0,
                CurrentClosing: 0,
                CostClass: '',
                UserName: ''
            },
            {
                IsParent: true,
                DateAD: '',
                DateBS: '',
                Particulars: 'Current Total =>',
                VoucherType: '',
                VoucherNo: '',
                RefNo: '',
                DebitAmt: 0,
                CreditAmt: 0,
                CurrentClosing: 0,
                CostClass: '',
                UserName: ''
            },
            {
                IsParent: true,
                DateAD: '',
                DateBS: '',
                Particulars: 'Closing Balance =>',
                VoucherType: '',
                VoucherNo: '',
                RefNo: '',
                DebitAmt: 0,
                CreditAmt: 0,
                CurrentClosing: 0,
                CostClass: '',
                UserName: ''
            }
        ];
        $scope.gridOptionsBottom = {
            defaultColDef: {
                resizable: true,
                width: 90
            },
            headerHeight: 30,
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


        $timeout(function () {
            if (SelectedLedgerId && SelectedLedgerId > 0) {
                $scope.LedgerVoucher.LedgerId = SelectedLedgerId;
                $scope.GetLedgerVoucher();
            }
        });

    }

    $scope.ShowAditionalLedger = function () {

        for (var i = 0; i < 28; i++) {
            var colName = 'ledDet' + i;
            $scope.gridOptions.columnApi.setColumnVisible(colName, false);
        }

        if ($scope.LedgerVoucher.LedgerTypeIdColl && $scope.LedgerVoucher.LedgerTypeIdColl.length > 0) {
            $scope.LedgerVoucher.LedgerTypeIdColl.forEach(function (colInd) {
                var colName = 'ledDet' + colInd;
                $scope.gridOptions.columnApi.setColumnVisible(colName, true);
            });
        }
        console.log($scope.LedgerVoucher.LedgerTypeIdColl);
    }

    $scope.editVoucher = function (tranId, voucherType, voucherId, voucherName, voucherNo) {

        Swal.fire({
            title: 'Do you want to edit the selected voucher(' + voucherName + ') :- ' + voucherNo + ' ? ',
            showCancelButton: true,
            confirmButtonText: 'Edit',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {

                var tabContent = "";
                if (voucherType < 5) {
                    tabContent = base_url + "Account/Transaction/" + voucherName + "?TranId=" + tranId;
                } else {
                    tabContent = base_url + "Inventory/Transaction/" + voucherName + "?TranId=" + tranId;
                }

                var tabTitle = voucherName;
                var tabs = window.parent.document.getElementById('tabs');
                var ul = tabs.children[0];
                var rand = function () {
                    return Math.random().toString(36).substr(2); // remove `0.`
                };
                var tabId = "Tab-" + rand();

                $("<li class='nav-item ui-tabs-active ui-state-active' role='presentation'><a id='al-" + tabId + "' class='nav-link active' role='tab' aria-controls='pills-second' aria-selected='false' OnClick='TabClick(\"" + tabId + "\")' href='#" + tabId + "'>" + tabTitle + "</a><a href='#' class='fas fa-times-circle'></a></li>").appendTo(ul);
                $("<div id='" + tabId + "'><iframe id='Frm_" + tabId + "' src='" + tabContent + "' width='100%'></iframe></div>").appendTo(tabs);

            }
        });

    }

    $scope.deleteVoucher = function (tranId, voucherType, voucherId, voucherName, voucherNo) {

        Swal.fire({
            title: 'Do you want to delete the selected voucher(' + voucherName + ') :- ' + voucherNo + ' ? ',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                var para = {
                    voucherType: voucherType,
                    voucherId: voucherId,
                    tranId: tranId
                };

                $http({
                    method: 'POST',
                    url: base_url + "Global/DelAccInvTransaction",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.GetLedgerVoucher();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });

    }

    $scope.ClearData = function () {

        $timeout(function () {

            $scope.dataForBottomGrid[0].DebitAmt = 0;
            $scope.dataForBottomGrid[0].CreditAmt = 0;
            $scope.dataForBottomGrid[1].DebitAmt = 0;
            $scope.dataForBottomGrid[1].CreditAmt = 0;
            $scope.dataForBottomGrid[2].DebitAmt = 0;
            $scope.dataForBottomGrid[2].CreditAmt = 0;

            var DataColl = [];
            $scope.gridOptionsBottom.api.setRowData(DataColl);
            $scope.gridOptions.api.setRowData(DataColl);

            $scope.LedgerVoucher.ODr = 0;
            $scope.LedgerVoucher.OCr = 0;
            $scope.LedgerVoucher.TDr = 0;
            $scope.LedgerVoucher.TCr = 0;
            $scope.LedgerVoucher.CDr = 0;
            $scope.LedgerVoucher.CCr = 0;

        });

    };

    $scope.GetLedgerVoucher = function () {

        $scope.ClearData();

        if (!$scope.LedgerVoucher.LedgerId && !$scope.LedgerVoucher.PatientId)
            return;

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.LedgerVoucher.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.LedgerVoucher.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.LedgerVoucher.DateToDet)
            dateTo = new Date(($filter('date')($scope.LedgerVoucher.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.DataColl = []; //declare an empty array

        var beData = {
            DateFrom: dateFrom,
            DateTo: dateTo,
            ledgerId: ($scope.LedgerVoucher.LedgerId ? $scope.LedgerVoucher.LedgerId : 0),
            PatientId: $scope.LedgerVoucher.PatientId,
            branchIdColl: $scope.LedgerVoucher.BranchId
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Account/Reporting/GetLedgerVoucher",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            var openingAmt = 0, drAmt = 0, crAmt = 0, closingAmt = 0;
            openingAmt = res.data.Data.OpeningAmt;
            drAmt = res.data.Data.DrAmt;
            crAmt = res.data.Data.CrAmt;
            closingAmt = res.data.Data.ClosingAmt;

            $scope.LedgerVoucher.ODr = (openingAmt > 0 ? openingAmt : 0);
            $scope.LedgerVoucher.OCr = (openingAmt < 0 ? Math.abs(openingAmt) : 0);
            $scope.LedgerVoucher.TDr = drAmt;
            $scope.LedgerVoucher.TCr = crAmt;
            $scope.LedgerVoucher.CDr = (closingAmt > 0 ? closingAmt : 0);
            $scope.LedgerVoucher.CCr = (closingAmt < 0 ? Math.abs(closingAmt) : 0);

            if (openingAmt > 0)
                $scope.dataForBottomGrid[0].DebitAmt = openingAmt;
            else
                $scope.dataForBottomGrid[0].CreditAmt = Math.abs(openingAmt);

            $scope.dataForBottomGrid[1].DebitAmt = drAmt;
            $scope.dataForBottomGrid[1].CreditAmt = crAmt;

            if (closingAmt > 0)
                $scope.dataForBottomGrid[2].DebitAmt = closingAmt;
            else
                $scope.dataForBottomGrid[2].CreditAmt = Math.abs(closingAmt);

            $scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);

            $scope.DataColl = res.data.Data.DataColl;
            $scope.gridOptions.api.setRowData($scope.DataColl);

            $scope.loadingstatus = "stop";
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
                                                        Period: $scope.LedgerVoucher.DateFromDet.dateBS + " TO " + $scope.LedgerVoucher.DateToDet.dateBS,
                                                        ODr: $scope.LedgerVoucher.ODr,
                                                        OCr: $scope.LedgerVoucher.OCr,
                                                        TDr: $scope.LedgerVoucher.TDr,
                                                        TCr: $scope.LedgerVoucher.TCr,
                                                        CDr: $scope.LedgerVoucher.CDr,
                                                        CCr: $scope.LedgerVoucher.CCr,
                                                        Ledger: $scope.LedgerVoucher.LedgerDetails.Name,
                                                        Address: $scope.LedgerVoucher.LedgerDetails.Address
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
                                    Period: $scope.LedgerVoucher.DateFromDet.dateBS + " TO " + $scope.LedgerVoucher.DateToDet.dateBS,
                                    ODr: $scope.LedgerVoucher.ODr,
                                    OCr: $scope.LedgerVoucher.OCr,
                                    TDr: $scope.LedgerVoucher.TDr,
                                    TCr: $scope.LedgerVoucher.TCr,
                                    CDr: $scope.LedgerVoucher.CDr,
                                    CCr: $scope.LedgerVoucher.CCr,
                                    Ledger: $scope.LedgerVoucher.LedgerDetails.Name,
                                    Address: $scope.LedgerVoucher.LedgerDetails.Address
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
        },
            {
                Name: 'Ledger',
                Value: $scope.LedgerVoucher.LedgerDetails.Name
            },
            {
                Name: 'Address',
                Value: ($scope.LedgerVoucher.LedgerDetails.Address ? $scope.LedgerVoucher.LedgerDetails.Address : '')
            },
            {
                Name: 'MobileNo',
                Value: ($scope.LedgerVoucher.LedgerDetails.MobileNo1 ? $scope.LedgerVoucher.LedgerDetails.MobileNo1 : '')
            },
            {
                Name: 'PanVatNo',
                Value: ($scope.LedgerVoucher.LedgerDetails.PanVat ? $scope.LedgerVoucher.LedgerDetails.PanVat : '')
            },
            {
                Name: 'TelNo',
                Value: ($scope.LedgerVoucher.LedgerDetails.MobileNo2 ? $scope.LedgerVoucher.LedgerDetails.MobileNo2 : '')
            },
            {
                Name: 'EmailId',
                Value: ($scope.LedgerVoucher.LedgerDetails.EmailId ? $scope.LedgerVoucher.LedgerDetails.EmailId : '')
            },
            {
                Name: 'ODr',
                Value: $scope.dataForBottomGrid[0].DebitAmt
            },
            {
                Name: 'OCr',
                Value: $scope.dataForBottomGrid[0].CreditAmt
            },
            {
                Name: 'TDr',
                Value: $scope.dataForBottomGrid[1].DebitAmt
            },
            {
                Name: 'TCr',
                Value: $scope.dataForBottomGrid[1].CreditAmt
            },
            {
                Name: 'CDr',
                Value: $scope.dataForBottomGrid[2].DebitAmt
            },
            {
                Name: 'CCr',
                Value: $scope.dataForBottomGrid[2].CreditAmt
            }
        );

        var filterData = [];

        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
            var ledVoucher = node.data;

            if (ledVoucher.IsParent == true) {
                filterData.push(ledVoucher);

                if ($scope.LedgerVoucher.IsSummary == false) {
                    angular.forEach(ledVoucher.AccountBillDetailsColl, function (bd) {
                        filterData.push({
                            Particulars: "(" + bd.VoucherDetails + " :- Rs." + Numberformat(bd.Amount) + " / " + bd.Remarks + " ) "
                        });
                    });

                    if (ledVoucher.LedgerNarration) {
                        filterData.push({
                            Particulars: "( " + ledVoucher.LedgerNarration + " )"
                        });
                    }

                    if (ledVoucher.CostCenterColl) {
                        angular.forEach(ledVoucher.CostCenterColl, function (all) {
                            var str = "";

                            if (all.DebitAmt > 0)
                                str = NumberformatAC(all.DebitAmt);
                            else
                                str = NumberformatAC(all.CreditAmt);
                            filterData.push({
                                Particulars: all.LedgerName + " " + str
                            });
                        });
                    }

                    if (ledVoucher.ChieldColl) {
                        angular.forEach(ledVoucher.ChieldColl, function (all) {

                            var str = "";
                            if (all.DebitAmt > 0)
                                str = NumberformatAC(all.DebitAmt);
                            else
                                str = NumberformatAC(all.CreditAmt);

                            filterData.push({
                                Particulars: all.LedgerName + " " + str
                            });

                            if (all.CostCenterColl) {
                                angular.forEach(all.CostCenterColl, function (all1) {
                                    if (all1.DebitAmt > 0)
                                        str = NumberformatAC(all1.DebitAmt);
                                    else
                                        str = NumberformatAC(all1.CreditAmt);

                                    filterData.push({
                                        Particulars: all1.LedgerName + " " + str
                                    });

                                });
                            }

                        });
                    }

                    if (ledVoucher.InventoryDetailsColl) {
                        angular.forEach(ledVoucher.InventoryDetailsColl, function (invData) {
                            var beData = {};
                            beData.Particulars = invData.ProductName;// +" ( " + GlobalFunction.getNumberStr(invData.BQty) + " @ " + GlobalFunction.getNumberStr(invData.Rate) + " = " + GlobalFunction.getNumberStr(invData.Amount) + " ) ";
                            beData.AQty = invData.AQty;
                            beData.BQty = invData.BQty;
                            beData.Rate = invData.Rate;
                            beData.Amount = invData.Amount;
                            beData.Unit = invData.UnitName;

                            var fixedProduct = invData.ProductName;

                            if (invData.RegdNo && !invData.RegdNo.isEmpty())
                                fixedProduct = fixedProduct + " Regd:-" + invData.RegdNo;

                            if (invData.EngineNo && !invData.EngineNo.isEmpty())
                                fixedProduct = fixedProduct + " Eng:-" + invData.EngineNo;

                            if (invData.ChassisNo && !invData.ChassisNo.isEmpty())
                                fixedProduct = fixedProduct + " Chass:-" + invData.ChassisNo;

                            if (invData.Model && !invData.Model.isEmpty())
                                fixedProduct = fixedProduct + " Model:-" + invData.Model;

                            beData.ProductName = fixedProduct;

                            filterData.push(beData);

                        });
                    }
                }

            }

        });


        return filterData;

    };

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

    $scope.GetSalesVatRegister = function () {


        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.LedgerVoucher.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.LedgerVoucher.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.LedgerVoucher.DateToDet)
            dateTo = new Date(($filter('date')($scope.LedgerVoucher.DateToDet.dateAD, 'yyyy-MM-dd')));

        var beData =
        {
            dateFrom: dateFrom,
            dateTo: dateTo,
            VoucherId: 0,
            BranchId: 0,
            PartyLedgerId: $scope.LedgerVoucher.LedgerId
        };

        $scope.loadingstatus = 'running';
        showPleaseWait();

        $http({
            method: "post",
            url: base_url + "Account/Reporting/GetSalesVatRegister",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                var SalesDataColl = res.data.Data;

                document.getElementById("frmRpt").src = '';
                reload_message_frame('frmRpt');

                $http({
                    method: 'GET',
                    url: base_url + "ReportEngine/GetReportTemplates?entityId=" + SalesVatEntityId + "&voucherId=0&isTran=false",
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
                                                    print = true;
                                                    $http({
                                                        method: 'POST',
                                                        url: base_url + "Global/PrintReportData",
                                                        headers: { 'Content-Type': undefined },

                                                        transformRequest: function (data) {

                                                            var formData = new FormData();
                                                            formData.append("entityId", SalesVatEntityId);
                                                            formData.append("jsonData", angular.toJson(data.jsonData));

                                                            return formData;
                                                        },
                                                        data: { jsonData: SalesDataColl }
                                                    }).then(function (res) {

                                                        $scope.loadingstatus = "stop";
                                                        hidePleaseWait();
                                                        if (res.data.IsSuccess && res.data.Data) {

                                                            document.body.style.cursor = 'wait';
                                                            document.getElementById("frmRpt").src = '';

                                                            var rptPara = {
                                                                rpttranid: rptTranId,
                                                                istransaction: false,
                                                                entityid: SalesVatEntityId,
                                                                voucherid: 0,
                                                                tranid: 0,
                                                                vouchertype: 0,
                                                                sessionid: res.data.Data.ResponseId,
                                                                Period: $scope.LedgerVoucher.DateFromDet.dateBS + " TO " + $scope.LedgerVoucher.DateToDet.dateBS,
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
                                print = true;

                                $http({
                                    method: 'POST',
                                    url: base_url + "Global/PrintReportData",
                                    headers: { 'Content-Type': undefined },

                                    transformRequest: function (data) {

                                        var formData = new FormData();
                                        formData.append("entityId", SalesVatEntityId);
                                        formData.append("jsonData", angular.toJson(data.jsonData));

                                        return formData;
                                    },
                                    data: { jsonData: SalesDataColl }
                                }).then(function (res) {

                                    $scope.loadingstatus = "stop";
                                    hidePleaseWait();
                                    if (res.data.IsSuccess && res.data.Data) {

                                        var rptPara = {
                                            rpttranid: rptTranId,
                                            istransaction: false,
                                            entityid: SalesVatEntityId,
                                            voucherid: 0,
                                            tranid: 0,
                                            vouchertype: 0,
                                            sessionid: res.data.Data.ResponseId,
                                            Period: $scope.LedgerVoucher.DateFromDet.dateBS + " TO " + $scope.LedgerVoucher.DateToDet.dateBS,
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

            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            $scope.loadingstatus = "stop";
            alert('Failed' + reason);
        });

    };

    $scope.GetPatientById = function () {
        $scope.LedgerVoucher.LedgerId = null;
        $scope.LedgerVoucher.PatientId = null;
        $scope.LedgerVoucher.LedgerDetails = {};

        if ($scope.LedgerVoucher.PatientNo || $scope.LedgerVoucher.PatientNo > 0) {

            $scope.loadingstatus = 'running';
            showPleaseWait();

            $http({
                method: 'GET',
                url: base_url + "Global/GetPatientDetails?patientId=" + $scope.LedgerVoucher.PatientNo + '&voucherId=0',
                dataType: "json"
            }).then(function (res1) {

                $scope.loadingstatus = 'stop';
                hidePleaseWait();

                var patient = res1.data.Data;
                if (patient.IsSuccess == true) {

                    $scope.LedgerVoucher.PatientId = patient.PatientId;
                    $scope.LedgerVoucher.LedgerDetails.Code = $scope.LedgerVoucher.PatientNo;
                    $scope.LedgerVoucher.LedgerDetails.Name = patient.PatientName;
                    $scope.LedgerVoucher.LedgerDetails.Address = patient.Address;
                    $scope.LedgerVoucher.LedgerDetails.GroupName = 'Patient';
                    $scope.LedgerVoucher.LedgerDetails.MobileNo1 = patient.MobileNo;

                    $scope.GetLedgerVoucher();


                } else {

                    Swal.fire('Invalid Patient Id');
                }


            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        }
    };

    $scope.SelectedTran = {};
    $scope.ShowDocument = function (beData) {

        if (beData.TranId && beData.VoucherType) {
            $scope.SelectedTran = beData;

            var para = {
                TranId: beData.TranId,
                VoucherType: beData.VoucherType
            };

            $http({
                method: 'POST',
                url: base_url + "Global/GetTranDocAttachment",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess) {
                    $scope.SelectedTran.DocumentColl = res.data.Data;


                    $('#modal-showDocument').modal('show');

                } else {
                    Swal.fire(res.data.ResponseMSG);
                }

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        }

    }
    $scope.ShowPersonalImg = function (docDet) {
        $scope.viewImg = {
            ContentPath: '',
            File: null,
            FileData: null
        };
        if (docDet.DocPath || docDet.File) {
            $scope.viewImg.ContentPath = docDet.DocPath;
            $scope.viewImg.File = docDet.File;
            $scope.viewImg.FileData = docDet.DocumentData;
            $('#PersonalImg').modal('show');
        } else
            Swal.fire('No Image Found');

    };

    $scope.CurParty = {};
    $scope.ShowInterest = function () {

        if ($scope.LedgerVoucher.LedgerDetails) {

            $scope.CurParty.LedgerId = $scope.LedgerVoucher.LedgerId;
            $scope.CurParty.CustomerName = $scope.LedgerVoucher.LedgerDetails.Name;
            $scope.CurParty.Address = $scope.LedgerVoucher.LedgerDetails.Address;
            $scope.CurParty.InterestRate = 0;
            $scope.CurParty.CreditDays = 0;
            $scope.CurParty.InterestOn = 1;
            $scope.CurParty.InterestColl = [];

            $scope.loadingstatus = 'running';
            showPleaseWait();
            var para = {
                ledgerId: $scope.LedgerVoucher.LedgerId
            };

            $http({
                method: "post",
                url: base_url + "Account/Creation/GetLedgerById",
                data: JSON.stringify(para),
                dataType: "json"
            }).then(function (res) {
                $scope.loadingstatus = "stop";
                hidePleaseWait();
                if (res.data.IsSuccess == true) {
                    var det = res.data.Data;

                    $scope.CurParty.InterestRate = det.InterestRate;
                    $scope.CurParty.CreditDays = det.CreditLimitDays;
                    $scope.CurParty.InterestOn = det.InterestOn;

                    $scope.ReCalculateInt();
                }
                else {
                    Swal.fire(res.data.ResponseMSG);
                }

            }, function (errormessage) {

                $scope.loadingstatus = 'stop';

                alert('Unable to Store data. pls try again.' + errormessage.responseText);
            });



        }
    }

    $scope.ReCalculateInt = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();

        var intData = null;
        if ($scope.CurParty.IntCutOffDateDet)
            intData = $filter('date')($scope.CurParty.IntCutOffDateDet.dateAD, 'yyyy-MM-dd');

        var beData = {

            ledgerId: ($scope.CurParty.LedgerId ? $scope.CurParty.LedgerId : 0),
            interestRate: $scope.CurParty.InterestRate,
            creditDays: $scope.CurParty.CreditDays,
            IntCutOffDate: intData,
            InterestOn: $scope.CurParty.InterestOn,
        };

        $http({
            method: "post",
            url: base_url + "Account/Reporting/GetLedgerInt",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();

            if (res.data.IsSuccess == true) {
                $scope.CurParty.InterestColl = res.data.Data;

                $('#frmMdlInterest').modal('show');
            }
            else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (errormessage) {

            $scope.loadingstatus = 'stop';

            alert('Unable to Store data. pls try again.' + errormessage.responseText);
        });
    }

    $scope.DownloadAsXls = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var dataColl = $scope.GetDataForPrint();

        var paraData = {
            Period: $scope.LedgerVoucher.DateFromDet.dateBS + " TO " + $scope.LedgerVoucher.DateToDet.dateBS,
            ODr: $scope.LedgerVoucher.ODr,
            OCr: $scope.LedgerVoucher.OCr,
            TDr: $scope.LedgerVoucher.TDr,
            TCr: $scope.LedgerVoucher.TCr,
            CDr: $scope.LedgerVoucher.CDr,
            CCr: $scope.LedgerVoucher.CCr,
            Ledger: $scope.LedgerVoucher.LedgerDetails.Name,
            Address: $scope.LedgerVoucher.LedgerDetails.Address
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
                down_file(base_url + "//" + res.data.Data.ResponseId, "LedgerVoucher.xlsx");
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
