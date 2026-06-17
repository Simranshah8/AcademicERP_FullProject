

"use strict";

agGrid.initialiseAgGridWithAngular1(angular);



app.controller("PostDatedChequeCtrl", function ($scope, $http, $filter) {
    $scope.Title = 'PostDatedCheque';

    LoadData();

    function Numberformat(val) {
        return $filter('number')(val, 2);
    }

    function LoadData() {
        $scope.loadingstatus = 'running';

        $('.select2').select2({
            allowClear: true,
            openOnEnter: true
        });

        $scope.DateAsColl = [{ id: 1, text: 'Cheque Date' }, { id: 2, text: 'Voucher Date' }, { id: 3, text: 'Valid Date' }]
        $scope.ReportTypesColl = [{ id: 1, text: 'Cleared Only' }, { id: 2, text: 'Expired Only' }, { id: 3, text: 'Cancel Only' }, { id: 4, text: 'Pending' }, { id: 5, text: 'All' }];

        $scope.newPDC = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            filterDateAs: 1,
            reportType: 5
        };

        $scope.columnDefs = [

            {
                headerName: "Action", width: 120, pinned: 'left', cellRenderer:
                    function (params) {

                        var dt = params.data;
                        return '<a class="btn btn-default btn-xs" ng-click="ShowDocument(this.data)"><i class="fas fa-file text-info"></i></a>' +
                            '<a class="btn btn-default btn-xs" ng-click="ClearModal(this.data)"><i class="fas fa-sticky-note"></i></a>' +
                            '<a class="btn btn-default btn-xs" ng-click="CancelModal(this.data)"><i class="fa fa-times"></i></a>' +
                            '<a class="btn btn-default btn-xs" ng-click="BounceModal(this.data)"><i class="fas fa-trash-alt text-danger"></i></a>';
                    }
            },
            { headerName: "S.No", field: "SNo", filter: "agTextColumnFilter", pinned: 'left', width: 90, dataType: 'Number', cellStyle: { 'text-align': 'center' } },
            { headerName: "PartyName", field: "LedgerName", filter: 'agTextColumnFilter', pinned: 'left', width: 220, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Address", field: "Address", filter: 'agTextColumnFilter', width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Group", field: "LedgerGroup", filter: 'agTextColumnFilter', width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Salesman", field: "Agent", filter: 'agTextColumnFilter', width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "VoucherDate", field: "VoucherDate", filter: 'agDateColumnFilter', width: 140, dataType: 'DateTime', cellStyle: { 'text-align': 'Center' } },
            { headerName: "VoucherMiti", field: "VoucherDateBS", filter: 'agTextColumnFilter', width: 140, dataType: 'DateTime', cellStyle: { 'text-align': 'Center' } },
            { headerName: "BankName", field: "BankName", filter: 'agTextColumnFilter', width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Bank Branch", field: "BankBranch", filter: 'agTextColumnFilter', width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Cheque No.", field: "ChequeNo", filter: 'agTextColumnFilter', width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Cheque Date", field: "ChequeDate", filter: 'agTextColumnFilter', width: 140, dataType: 'DateTime', cellStyle: { 'text-align': 'Center' } },
            { headerName: "Cheque Miti", field: "ChequeDateBS", filter: 'agTextColumnFilter', width: 140, dataType: 'DateTime', cellStyle: { 'text-align': 'Center' } },
            { headerName: "Amount", field: "Amount", filter: 'agNumberColumnFilter', width: 140, dataType: 'Number', cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "Notes", field: "Notes", filter: 'agTextColumnFilter', width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "IsCleared", field: "IsCleared", filter: 'agTextColumnFilter', width: 140, dataType: 'Text', cellStyle: { 'text-align': 'Center' } },
            { headerName: "ClearedRemarks", field: "ClearedRemarks", filter: 'agTextColumnFilter', width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "IsCancel", field: "IsCancel", filter: 'agTextColumnFilter', width: 140, dataType: 'Text', cellStyle: { 'text-align': 'Center' } },
            { headerName: "CancelDate", field: "CancelDate", filter: 'agTextColumnFilter', width: 180, dataType: 'DateTime', cellStyle: { 'text-align': 'Center' } },
            { headerName: "CancelRemarks", field: "CancelRemarks", filter: 'agTextColumnFilter', width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "CanceledBy", field: "CanceledBy", filter: 'agTextColumnFilter', width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },

            { headerName: "BounceCount", field: "BounceCount", filter: 'agTextColumnFilter', width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "BounceDetails", field: "BounceDetails", filter: 'agTextColumnFilter', width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "ReceiptNo", field: "ReceiptNo", filter: 'agTextColumnFilter', width: 150, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "ValidDays", field: "ValidDays", filter: 'agTextColumnFilter', width: 130, dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "ValidMiti", field: "ValidMiti", filter: 'agTextColumnFilter', width: 140, dataType: 'DateTime', cellStyle: { 'text-align': 'left' } },
            { headerName: "DueDays", field: "DueDays", filter: 'agTextColumnFilter', width: 180, dataType: 'Number', cellStyle: { 'text-align': 'left' } },
            { headerName: "Against", field: "Against", filter: 'agTextColumnFilter', width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "R.V. No.", field: "ReceiptVoucherNo", filter: 'agTextColumnFilter', width: 130, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Branch", field: "Branch", filter: 'agTextColumnFilter', width: 150, dataType: 'Text', cellStyle: { 'text-align': 'left' } }
        ];


        $scope.gridOptions = {
            angularCompileRows: true,
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
                    LedgerName: 'TOTAL =>',
                    Amount: 0,

                }
                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var fData = node.data;
                    dt.Amount += fData.Amount;


                });


                var filterDataColl = [];
                filterDataColl.push(dt);

                $scope.gridOptionsBottom.api.setRowData(filterDataColl);
            }

        };

        $scope.dataForBottomGrid = [
            {
                SNo: '',
                LedgerName: 'Total =>',
                Amount: 0,

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

        $scope.VoucherTypeColl = [];
        $scope.CostClassColl = [];
        $scope.SelectedVoucher = null;
        $scope.SelectedCostClass = null;

        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetVoucherList?voucherType=" + recVoucherType,
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.VoucherTypeColl = res.data.Data;

                if ($scope.VoucherTypeColl && $scope.VoucherTypeColl.length == 1)
                    $scope.SelectedVoucher = $scope.VoucherTypeColl[0];

                $http({
                    method: 'GET',
                    url: base_url + "Account/Creation/GetCostClassForEntry",
                    dataType: "json"
                }).then(function (res1) {
                    if (res1.data.IsSuccess && res1.data.Data) {
                        $scope.CostClassColl = res1.data.Data;

                        if ($scope.CostClassColl && $scope.CostClassColl.length == 1)
                            $scope.SelectedCostClass = $scope.CostClassColl[0];
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });

            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


    }
    $scope.ClearData = function () {

        var DataColl = [];
        $scope.gridOptionsBottom.api.setRowData(DataColl);

        $scope.gridOptions.api.setRowData(DataColl);
    };

    $scope.GetAllPostDatedCheque = function () {

        $scope.ClearData();
        $scope.loadingstatus = 'running';
        showPleaseWait();

        var para = {
            dateFrom: $filter('date')($scope.newPDC.DateFromDet.dateAD, 'yyyy-MM-dd'),
            dateTo: $filter('date')($scope.newPDC.DateToDet.dateAD, 'yyyy-MM-dd'),
            reportType: $scope.newPDC.reportType,
            filterDateAs: $scope.newPDC.filterDateAs
        };

        $http({
            method: 'GET',
            url: base_url + "Account/Reporting/GetAllPostDatedCheque",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                var DataColl = mx(res.data.Data);

                var dt = {
                    LedgerName: 'TOTAL =>',
                    Amount: DataColl.sum(p1 => p1.Amount),

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

    }

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    $scope.onBtExport = function () {
        var params = {
            fileName: 'data.csv',
            sheetName: 'data'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

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

        var filterData = [];

        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
            var dayBook = node.data;
            filterData.push(dayBook);
        });

        return filterData;
    }

    $scope.SelectedPDC = {};
    $scope.ShowDocument = function (beData) {
        $scope.SelectedPDC = beData;
        $('#modal-showDocument').modal('show');

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
    $scope.CancelModal = function (beData) {

        if (beData.IsCancel == true) {

            Swal.fire('This PDC was already canceled');
        } else {

            $scope.SelectedPDC = beData;

            $('#modal-cancel').modal('show');
        }

    }
    $scope.CancelPDC = function () {
        $('#modal-cancel').modal('hide');

        var obj = $scope.SelectedPDC;

        Swal.fire({
            title: 'Do you want to cancel the selected PDC (' + obj.LedgerName + ') :- ' + obj.Amount + ' ? ',
            showCancelButton: true,
            confirmButtonText: 'Yes',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                $http({
                    method: 'POST',
                    url: base_url + "Account/Creation/PDCChequeCancel",
                    dataType: "json",
                    data: JSON.stringify($scope.SelectedPDC)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.GetAllPostDatedCheque();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });

    };


    $scope.ClearModal = function (beData) {

        if (beData.IsCleared == true) {

            Swal.fire('This PDC was already cleared');
        }
        else if (beData.IsCancel == true) {
            Swal.fire('This PDC was already canceled');
        }
        else {

            $scope.SelectedPDC = beData;

            $('#modal-cleared').modal('show');
        }

    }
    $scope.ClearPDC = function () {
        $('#modal-cleared').modal('hide');

        var obj = $scope.SelectedPDC;

        Swal.fire({
            title: 'Do you want to cleared the selected PDC (' + obj.LedgerName + ') :- ' + obj.Amount + ' ? ',
            showCancelButton: true,
            confirmButtonText: 'Yes',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                $scope.SelectedPDC.VoucherId = $scope.SelectedVoucher.VoucherId;
                $scope.SelectedPDC.CostClassId = $scope.SelectedCostClass.CostClassId;
                $http({
                    method: 'POST',
                    url: base_url + "Account/Creation/PDCCleared",
                    dataType: "json",
                    data: JSON.stringify($scope.SelectedPDC)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.GetAllPostDatedCheque();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });

    };



    $scope.BounceModal = function (beData) {

        if (beData.IsCleared == true) {

            Swal.fire('This PDC was already cleared');
        }
        else if (beData.IsCancel == true) {
            Swal.fire('This PDC was already canceled');
        }
        else {

            $scope.SelectedPDC = beData;

            $('#modal-bounce').modal('show');
        }

    }
    $scope.BouncePDC = function () {
        $('#modal-bounce').modal('hide');

        var obj = $scope.SelectedPDC;

        Swal.fire({
            title: 'Do you want to bounce the selected PDC (' + obj.LedgerName + ') :- ' + obj.Amount + ' ? ',
            showCancelButton: true,
            confirmButtonText: 'Yes',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();

                $scope.SelectedPDC.BounceDate = $filter('date')($scope.SelectedPDC.BounceDateDet.dateAD, 'yyyy-MM-dd');
                $http({
                    method: 'POST',
                    url: base_url + "Account/Creation/PDCChequeBounce",
                    dataType: "json",
                    data: JSON.stringify($scope.SelectedPDC)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.GetAllPostDatedCheque();
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
            Period: $scope.newPDC.DateFromDet.dateBS + " TO " + $scope.newPDC.DateToDet.dateBS,
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
                down_file(base_url + "//" + res.data.Data.ResponseId, "PostDatedCheque.xlsx");
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire(errormessage);
        });
    }



});
