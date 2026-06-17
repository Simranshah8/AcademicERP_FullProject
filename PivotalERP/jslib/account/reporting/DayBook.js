
agGrid.initialiseAgGridWithAngular1(angular);

app.controller("dayBookCntrl", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'daybook.csv',
            sheetName: 'daybook'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    $scope.ExpandData = function () {
        $scope.gridOptions.api.expandAll();
    }
    $scope.CollapseData = function () {
        $scope.gridOptions.api.collapseAll();
    }
    function LoadData() {

        $('.select2').select2({
            allowClear: true,
            openOnEnter: true
        });

        $scope.comDet = {};
        GlobalServices.getCompanyDet().then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.comDet = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        //agGrid.initialiseAgGridWithAngular1(angular);
        $scope.VoucherTypeList = [];
        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetVoucherTypes",
            //url: base_url + "Account/Creation/GetUserWiseVoucherTypes",
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

        $scope.PaymentTermColl = [];
        $scope.PaymentTermColl_Qry = [];
        GlobalServices.getPaymentTerms().then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.PaymentTermColl = res.data.Data;
                $scope.PaymentTermColl_Qry = mx(res.data.Data);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.DayBookTypes = [{ id: 1, text: 'Post' }, { id: 2, text: 'Pending' }, { id: 3, text: 'Cancel' }];
        $scope.dayBook = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            VoucherId: 0,
            IsPost: true,
            BranchId: 0,
            IsSummary: true,
            For: 1,
            ExpandCollapse: false,
        };

        $scope.searchData = {
            UserColl: '',
            DayBook: ''
        };

        $scope.loadingstatus = "stop";

        $scope.columnDefs = [
            {
                headerName: "Date", width: 170, field: "VoucherDate", dataType: 'DateTime', cellRenderer: 'agGroupCellRenderer',
                valueFormatter: function (params) { return DateFormatAD(params.value); },
                showRowGroup: true,
                cellRendererParams: {
                    suppressCount: false, // turn off the row count                   
                }, pinned: 'left'
            },
            {
                headerName: "Miti", width: 120, dataType: 'DateTime',
                cellRenderer:
                    function (params) {
                        return DateFormatBS(params.data.NY, params.data.NM, params.data.ND) + '</a ></center>';
                    }, pinned: 'left'

            },
            {
                headerName: "Particular's", width: 220, dataType: 'Text',
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
                }, pinned: 'left', filter: "agTextColumnFilter",
            },
            {
                headerName: "Amount", width: 150, filter: "agNumberColumnFilter", dataType: 'Number',
                valueGetter: function (params) {
                    var beData = params.data;
                    return beData.Amount;
                },
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            { headerName: "VoucherType", width: 150, field: "VoucherName", filter: true, dataType: 'Text', filter: "agTextColumnFilter", },
            { headerName: "Voucher No.", width: 150, field: "AutoManualNo", filter: true, dataType: 'Text', filter: "agTextColumnFilter", },
            { headerName: "Ref.No.", width: 120, field: "RefNo", filter: true, dataType: 'Text', filter: "agTextColumnFilter", },
            {
                headerName: "Debit", width: 150, dataType: 'Number', filter: "agNumberColumnFilter",
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

                }, filter: "agNumberColumnFilter",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            {
                headerName: "Credit", width: 150, dataType: 'Number', filter: "agNumberColumnFilter",
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

                }, filter: "agNumberColumnFilter",
                valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, footerTemplate: '<div>totaal: #= sum #</div>'
            },
            { headerName: "CostClass", width: 120, dataType: 'Text', field: "CostClassName", filter: true, filter: "agTextColumnFilter", },
            { headerName: "User", width: 120, dataType: 'Text', field: "CreatedByName", filter: "agTextColumnFilter", },


            { headerName: "Party Name", width: 140, field: "Buyes", dataType: 'Text', filter: true, filter: "agTextColumnFilter", },
            { headerName: "PAN/VAT No.", width: 110, field: "PanVatNo", dataType: 'Text', filter: true, filter: "agTextColumnFilter", },
            { headerName: "Address", width: 140, field: "Address", dataType: 'Text', filter: true, filter: "agTextColumnFilter", },
            { headerName: "PaymentTerms", width: 150, field: "PaymentTerms", dataType: 'Text', filter: true, filter: "agTextColumnFilter", },
            { headerName: "DSE", width: 150, field: "Agent", filter: true, dataType: 'Text', filter: "agTextColumnFilter", },
            { headerName: "Ref. VoucherNo", width: 130, field: "RefAutoManualNo", filter: true, dataType: 'Text', filter: "agTextColumnFilter", },
            //{
            //    headerName: "Action", width: 165, cellRenderer:
            //        function (params) {

            //            var voucherName = params.data.VoucherName;

            //            if (voucherName) {

            //                if (params.data.VoucherType < 5) {
            //                    return '<a class="btn btn-default btn-xs" data-toggle="tooltip" data-placement="top" title="Show Document" ng-click="ShowDocument(this.data)"><i class="fas fa-file text-info"></i></a> <a class="btn btn-default btn-xs" data-toggle="tooltip" data-placement="top" title="Print" ng-click="PrintVoucher(' + params.data.TranId + ',' + params.data.VoucherType + ',' + params.data.VoucherId + ')"><i class="fas fa-print text-info"></i></a>' +
            //                        '<a class="btn btn-default btn-xs"data-toggle="tooltip" data-placement="top" title="Post" ng-click="PostModal(this)"><i class="fas fa-sticky-note"></i></a>' +
            //                        '<a class="btn btn-default btn-xs"data-toggle="tooltip" data-placement="top" title="Cancel" ng-click="CancelModal(this)"><i class="fa fa-times"></i></a>' +
            //                        '<a class="btn btn-default btn-xs"data-toggle="tooltip" data-placement="top" title="Generate Receipt" ng-click="GenerateReceipt(this)"><i class="fa fa-receipt"></i></a>' +
            //                        '<a class="btn btn-default btn-xs"data-toggle="tooltip" data-placement="top" title="Delete" ng-click="deleteVoucher(' + params.data.TranId + ',' + params.data.VoucherType + ',' + params.data.VoucherId + ',\'' + voucherName + '\'' + ', \'' + params.data.AutoManualNo + '\')"><i class="fas fa-trash-alt text-danger"></i></a>';

            //                } else {
            //                    return '<a class="btn btn-default btn-xs"data-toggle="tooltip" data-placement="top" title="Show Document" ng-click="ShowDocument(this.data)"><i class="fas fa-file text-info"></i></a> <a class="btn btn-default btn-xs" data-toggle="tooltip" data-placement="top" title="Print" ng-click="PrintVoucher(' + params.data.TranId + ',' + params.data.VoucherType + ',' + params.data.VoucherId + ')"><i class="fas fa-print text-info"></i></a>' +
            //                        '<a class="btn btn-default btn-xs"data-toggle="tooltip" data-placement="top" title="Post" ng-click="PostModal(this)"><i class="fas fa-sticky-note"></i></a>' +
            //                        '<a class="btn btn-default btn-xs"data-toggle="tooltip" data-placement="top" title="Cancel" ng-click="CancelModal(this)"><i class="fa fa-times"></i></a>' +
            //                        '<a class="btn btn-default btn-xs"data-toggle="tooltip" data-placement="top" title="Generate Receipt" ng-click="GenerateReceipt(this)"><i class="fa fa-receipt"></i></a>' +
            //                        '<a class="btn btn-default btn-xs"data-toggle="tooltip" data-placement="top" title="Delete" ng-click="deleteVoucher(' + params.data.TranId + ',' + params.data.VoucherType + ',' + params.data.VoucherId + ',\'' + voucherName + '\'' + ', \'' + params.data.AutoManualNo + '\')"><i class="fas fa-trash-alt text-danger"></i></a>';
            //                }

            //            } else {
            //                return '';
            //            }
            //        }
            //},

            {
                headerName: "Action",
                width: 100,
                cellRenderer: function (params) {
                    return '<div class="btn-group" style="position: fixed; ">' +
                        '<button type="button" class="btn btn-default px-1 dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                        '<span class="caret"></span>' +
                        '</button>' +
                        '<ul class="dropdown-menu dropdown-menu-right p-2" style="position: absolute; left: 0;">' +
                        '<li><a data-toggle="tooltip" data-placement="top" title="Show Document" ng-click="ShowDocument(this.data)"><i class="fas fa-file text-info"></i> Show Document</a>  </li>' +
                        '<li><a data-toggle="tooltip" data-placement="top" title="Print" ng-click="PrintVoucher(' + params.data.TranId + ',' + params.data.VoucherType + ',' + params.data.VoucherId + ')"><i class="fas fa-print text-info"></i> Print</a></li>' +
                        '<li><a data-toggle="tooltip" data-placement="top" title="Post" ng-click="PostModal(this)"><i class="fas fa-sticky-note"></i> Post</a> </li>' +


                        '<li><a data-toggle="tooltip" data-placement="top" title="Cancel" ng-click="CancelModal(this)"><i class="fa fa-times text-danger"></i> Cancel</a> </li>' +
                        '<li><a data-toggle="tooltip" data-placement="top" title="Generate Receipt" ng-click="GenerateReceipt(this)"><i class="fa fa-receipt"></i> Generate Receipt</a> </li>' +
                        '<li><a data-toggle="tooltip" data-placement="top" title="Delete Voucher" ng-click="deleteVoucher(this)"><i class="fas fa-trash-alt text-danger"></i> Delete</a></li>' +

                        '</ul>' +
                        '</div>';
                },
                pinned: 'right'
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
            headerHeight: 35,
            rowHeight: 33,
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

                var dt = {
                    DispalyValue: "TOTAL =>",
                    DrAmount: 0,
                    CrAmount: 0
                };

                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var dc = node.data;
                    if (dc.IsParent == true) {
                        dt.DrAmount += dc.DrAmount;
                        dt.CrAmount += dc.CrAmount;
                    }
                });
                var filterDataColl = [];
                filterDataColl.push(dt);

                $scope.gridOptionsBottom.api.setRowData(filterDataColl);

            },
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


        $scope.dataForBottomGrid = [
            {
                DispalyValue: 'Total =>',
            }];

        $scope.gridOptionsBottom = {
            defaultColDef: {
                resizable: true,
                width: 90
            },
            columnDefs: $scope.columnDefs.filter(function (colDef) {
                return colDef.headerName !== "Action";  // Exclude the "Action" column for the bottom grid
            }),
           /* columnDefs: $scope.columnDefs,*/
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

    $scope.toggleExpandCollapse = function () {
        if ($scope.dayBook.ExpandCollapse == true) {
            $scope.gridOptions.api.expandAll();
        } else {
            $scope.gridOptions.api.collapseAll();
        }
    };

    $scope.PostAllVoucher = function (e) {

        if ($scope.dayBook.For != 2)
            return;


        Swal.fire({
            title: 'Do you want to post all voucher ?',
            showCancelButton: true,
            confirmButtonText: 'Post',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                var tranColl = [];

                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {

                    if (node.data.VoucherType && node.data.IsParent == true) {
                        var nd = node.data;
                        tranColl.push({
                            TranId: nd.TranId,
                            VoucherType: nd.VoucherType,
                            VoucherId: nd.VoucherId,
                            VoucherDate: nd.VoucherDate
                        });
                    }
                });

                $http({
                    method: 'POST',
                    url: base_url + "Global/PostAccInvTransaction",
                    dataType: "json",
                    data: JSON.stringify(tranColl)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.GetDayBook();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });

    }

    $scope.SelectedVoucherP = null;
    $scope.PostModal = function (e) {

        if ($scope.dayBook.For != 2)
            return;

        var obj = e.data;

        if (!obj)
            return;

        $scope.SelectedVoucherP = obj;

        var para = {
            voucherId: obj.VoucherId
        };

        $http({
            method: 'POST',
            url: base_url + "Account/Creation/GetVMForDayBook",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                var vm = res.data.Data;

                if (vm.NeedPostRemarks == true) {
                    $scope.SelectedVoucherP.NeedPostRemarks = vm.NeedPostRemarks;
                    $('#modal-post').modal('show');

                } else {
                    $scope.SelectedVoucherP.NeedPostRemarks = false;
                    $scope.PostVoucher();
                }

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });



    }
    $scope.PostVoucher = function () {

        if ($scope.dayBook.For != 2 || !$scope.SelectedVoucherP)
            return;

        var obj = $scope.SelectedVoucherP;

        Swal.fire({
            title: 'Do you want to post the selected voucher(' + obj.VoucherName + ') :- ' + obj.AutoManualNo + ' ? ',
            showCancelButton: true,
            confirmButtonText: 'Post',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                var tranColl = [];
                //tranColl.push(obj);

                if ($scope.SelectedVoucherP.NeedPostRemarks == true && isEmptyObj($scope.SelectedVoucherP.VerifyRemarks) == true) {
                    Swal.fire('Remarks missing');
                    return;
                }

                tranColl.push({
                    TranId: obj.TranId,
                    VoucherType: obj.VoucherType,
                    VoucherId: obj.VoucherId,
                    VoucherDate: obj.VoucherDate,
                    VerifyRemarks: obj.VerifyRemarks
                });


                $http({
                    method: 'POST',
                    url: base_url + "Global/PostAccInvTransaction",
                    dataType: "json",
                    data: JSON.stringify(tranColl)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {

                        $('#modal-post').modal('hide');
                        $scope.GetDayBook();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });

    }

    $scope.SelectedVoucher = null;
    $scope.CancelModal = function (e) {

        if ($scope.dayBook.For == 3)
            return;

        var obj = e.data;

        if (!obj)
            return;

        $scope.SelectedVoucher = obj;

        $('#modal-cancel').modal('show');

    }
    $scope.CancelVoucher = function () {
        $('#modal-cancel').modal('hide');

        var obj = $scope.SelectedVoucher;

        Swal.fire({
            title: 'Do you want to cancel the selected voucher(' + obj.VoucherName + ') :- ' + obj.AutoManualNo + ' ? ',
            showCancelButton: true,
            confirmButtonText: 'Yes',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                var tranColl1 = [];
                tranColl1.push({
                    TranId: obj.TranId,
                    VoucherId: obj.VoucherId,
                    VoucherType: obj.VoucherType,
                    CostClassId: obj.CostClassId,
                    IsCancel: obj.IsCancel,
                    VoucherType: obj.VoucherType,
                    CancelRemarks: obj.CancelRemarks,
                    EntryDate: obj.EntryDate,
                    VoucherDate: obj.VoucherDate,
                    IsPost: obj.IsPost,
                    AutoVoucherNo: obj.AutoVoucherNo,
                    ManualVoucherNO: obj.ManualVoucherNO,
                    AutoManualNo: obj.AutoManualNo,
                    RefNo: obj.RefNo,
                    TranType: obj.TranType,
                    CreatedBy: obj.CreatedBy,
                    CreatedByName: obj.CreatedByName,
                    BranchId: obj.BranchId,
                    IsLocked: obj.IsLocked,
                    IsOpening: obj.IsOpening,
                    IsVerify: obj.IsVerify,
                    VerifyRemarks: obj.VerifyRemarks,
                    IsReject: obj.IsReject,
                    RejectRemarks: obj.RejectRemarks,
                    VoucherDateTime: obj.VoucherDateTime,
                    ReferanceTranId: obj.ReferanceTranId,
                    IsInventory: obj.IsInventory,
                    CancelDateTime: obj.CancelDateTime,
                });

                var para = {
                    tranColl: tranColl1,
                    reason: obj.CancelRemarks
                }

                $http({
                    method: 'POST',
                    url: base_url + "Global/CancelAccInvTransaction",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.GetDayBook();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });

    };
    $scope.deleteVoucher = function (e) {


        var obj = e.data;

        var tranId = obj.TranId, voucherType = obj.VoucherType, voucherId = obj.VoucherId, voucherName = obj.VoucherName, voucherNo = obj.AutoManualNo;

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
                        $scope.GetDayBook();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });

    }

    $scope.UserWiseColl = [];
    $scope.GetDayBook = function () {

        $scope.UserWiseColl = [];
        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.dayBook.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.dayBook.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.dayBook.DateToDet)
            dateTo = new Date(($filter('date')($scope.dayBook.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.DataColl = []; //declare an empty array
        $scope.gridOptions.api.setRowData($scope.DataColl);

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var beData = {
            DateFrom: dateFrom,
            DateTo: dateTo,
            VoucherType: ($scope.dayBook.VoucherId ? $scope.dayBook.VoucherId : 0),
            isPost: $scope.dayBook.IsPost,
            branchId: $scope.dayBook.BranchId,
            For: $scope.dayBook.For,
            PaymentTermsId: $scope.dayBook.PaymentTermsId
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Account/Reporting/GetDayBook",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            var DataColl = res.data.Data;

            var dt = {
                DispalyValue: "TOTAL =>",
                DrAmount: 0,
                CrAmount: 0
            };
            angular.forEach(DataColl, function (dc) {
                dt.DrAmount += dc.DrAmount;
                dt.CrAmount += dc.CrAmount;
            });
            var filterDataColl = [];
            filterDataColl.push(dt);

            $scope.gridOptionsBottom.api.setRowData(filterDataColl);

            $scope.gridOptions.api.setRowData(res.data.Data);

            if ($scope.comDet.Maintain == 3) {
                var tmpUserTran = [];
                angular.forEach(res.data.Data, function (trn) {
                    if (trn.VoucherType == 14) {
                        var iscash = false;
                        var findPT = $scope.PaymentTermColl_Qry.firstOrDefault(p1 => p1.Name == trn.PaymentTerms);
                        if (findPT)
                            iscash = findPT.IsCash;

                        var trnData = {
                            User: trn.CreatedByName,
                            SalesCash: (iscash ? trn.DrAmount : 0),
                            SalesCredit: (iscash ? 0 : trn.DrAmount),
                            ReturnCash: 0,
                            ReturnCredit: 0,
                            Receipt: 0,
                            Payment: 0,
                        };
                        tmpUserTran.push(trnData);

                    } else if (trn.VoucherType == 16) {

                        var iscash = false;
                        var findPT = $scope.PaymentTermColl_Qry.firstOrDefault(p1 => p1.Name == trn.PaymentTerms);
                        if (findPT)
                            iscash = findPT.IsCash;

                        var trnData = {
                            User: trn.CreatedByName,
                            SalesCash: 0,
                            SalesCredit: 0,
                            ReturnCash: (iscash ? trn.CrAmount : 0),
                            ReturnCredit: (iscash ? 0 : trn.CrAmount),
                            Receipt: 0,
                            Payment: 0,
                        };
                        tmpUserTran.push(trnData);

                    } else if (trn.VoucherType == 1) {

                        var trnData = {
                            User: trn.CreatedByName,
                            SalesCash: 0,
                            SalesCredit: 0,
                            ReturnCash: 0,
                            ReturnCredit: 0,
                            Receipt: trn.TransactionAmt,
                            Payment: 0,
                        };
                        tmpUserTran.push(trnData);

                    } else if (trn.VoucherType == 2) {
                        var trnData = {
                            User: trn.CreatedByName,
                            SalesCash: 0,
                            SalesCredit: 0,
                            ReturnCash: 0,
                            ReturnCredit: 0,
                            Receipt: 0,
                            Payment: trn.TransactionAmt,
                        };
                        tmpUserTran.push(trnData);
                    }
                });

                var groupTran = mx(tmpUserTran).groupBy(t => t.User);
                angular.forEach(groupTran, function (gt) {
                    var newTran = {
                        User: gt.key,
                        SalesCash: 0,
                        SalesCredit: 0,
                        ReturnCash: 0,
                        ReturnCredit: 0,
                        Receipt: 0,
                        Payment: 0,
                        NetSales: 0,
                        NetCash: 0,
                        NetCashBal: 0
                    };
                    angular.forEach(gt.elements, function (el) {
                        newTran.SalesCash = newTran.SalesCash + el.SalesCash;
                        newTran.SalesCredit = newTran.SalesCredit + el.SalesCredit;
                        newTran.ReturnCash = newTran.ReturnCash + el.ReturnCash;
                        newTran.ReturnCredit = newTran.ReturnCredit + el.ReturnCredit;
                        newTran.Receipt = newTran.Receipt + el.Receipt;
                        newTran.Payment = newTran.Payment + el.Payment;
                    });
                    newTran.NetSales = newTran.SalesCash - newTran.ReturnCash;
                    newTran.NetCash = newTran.Receipt - newTran.Payment;
                    newTran.NetCashBal = newTran.NetSales + newTran.NetCash;
                    $scope.UserWiseColl.push(newTran);
                });
            }

            $scope.loadingstatus = "stop";
            hidePleaseWait();

        }, function (errormessage) {

            $scope.loadingstatus = 'stop';

            alert('Unable to Store data. pls try again.' + errormessage.responseText);
        });

    };



    $scope.PostSelectedVoucher = function () {

        var pendingDataColl = []; //declare an empty array

        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {

            if (node.data.VoucherType)
                pendingDataColl.push(node.data);
        });

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Account/Reporting/PostPendingVoucher",
            data: JSON.stringify(pendingDataColl),
            dataType: "json"
        }).then(function (res) {

            $scope.loadingstatus = 'done';
            alert(res.data.ResponseMSG);

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

                    if ($scope.dayBook.IsSummary == false) {
                        var ledData = {};
                        ledData.Particulars = "  " + dayBook.Particulars;

                        if (!dayBook.AditionalCostColl)
                            dayBook.AditionalCostColl = [];

                        if (dayBook.DrAmount != 0 && dayBook.AditionalCostColl.length > 0)
                            ledData.CrAmount = dayBook.DrAmount - mx(dayBook.AditionalCostColl).sum(p1 => p1.Amount);
                        else if (dayBook.AditionalCostColl.length > 0)
                            ledData.DrAmount = dayBook.CrAmount - mx(dayBook.AditionalCostColl).sum(p1 => p1.Amount);

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

                        if (!dayBook.ItemAllocationColl)
                            dayBook.ItemAllocationColl = [];

                        angular.forEach(dayBook.ItemAllocationColl, function (item) {

                            var itemData = {};
                            itemData.Particulars = "    " + item.ProductName + " ( " + item.BilledQty + item.UnitName + " @ " + item.Rate + " = " + item.Amount + " )";
                            filterData.push(itemData);

                        });

                    }

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

                            filterData.push(beData);
                        }
                        else {

                            if ($scope.dayBook.IsSummary == false) {
                                var chieldData = {};
                                chieldData.Particulars = "  " + ledAll.LedgerName;
                                chieldData.Narration = ledAll.Narration;
                                chieldData.DrAmount = ledAll.DrAmount;
                                chieldData.CrAmount = ledAll.CrAmount;
                                filterData.push(chieldData);
                            }

                        }
                    });
                }
            }

        });

        return filterData;

    };

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    $scope.PrintVoucher = function (tranId, voucherType, voucherId) {

        if (voucherType == 14) {
            Swal.fire('Please ! Print Invoice From Voucher Entry');
            return;
        }

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

    $scope.GenerateReceipt = function (e) {

        var obj = e.data;

        if (!obj)
            return;

        Swal.fire({
            title: 'Do you want to generate receipt the selected voucher(' + obj.VoucherName + ') :- ' + obj.AutoManualNo + ' ? ',
            showCancelButton: true,
            confirmButtonText: 'Yes',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                var tranColl1 = [];

                var para = {
                    TranId: obj.TranId,
                    VoucherType: obj.VoucherType
                }

                $http({
                    method: 'POST',
                    url: base_url + "Account/Reporting/GenerateRec",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.GetDayBook();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });

    };


    $scope.DownloadAsXls = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var dataColl = $scope.GetDataForPrint();

        var paraData = {
            Period: $scope.dayBook.DateFromDet.dateBS + " TO " + $scope.dayBook.DateToDet.dateBS,
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
                down_file(base_url + "//" + res.data.Data.ResponseId, "DayBook.xlsx");
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
