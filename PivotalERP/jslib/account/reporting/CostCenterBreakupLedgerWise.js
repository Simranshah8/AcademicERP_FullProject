"use strict";
agGrid.initialiseAgGridWithAngular1(angular);
app.controller("CostCenterBreakupLedgerWise", function ($scope, $http, $filter, $timeout, GlobalServices, $compile) {

    LoadData();
    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'CostCenterBreakupLedgerWise.csv',
            sheetName: 'CostCenterBreakupLedgerWise'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }
    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    function LoadData() {
        $scope.CostCenterBreakupLedgerWise = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),

            CostCenterId: 0,

        };
        $scope.LedgerList = [];
        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetLedgerList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.LedgerList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
        $scope.CostCenterList = [];
        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetCostCenter",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.CostCenterList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        //Commented By Sureh on 19 Falgun
        //$scope.AgentList = [];
        //$http({
        //    method: 'GET',
        //    url: base_url + "Account/Creation/GetSalesManList",
        //    dataType: "json"
        //}).then(function (res) {
        //    if (res.data.IsSuccess && res.data.Data) {
        //        $scope.AgentList = res.data.Data;
        //    }
        //}, function (reason) {
        //    Swal.fire('Failed' + reason);
        //});

        $timeout(function () {
            $http({
                method: "GET",
                url: base_url + "Global/GetCompanyDetail",
                dataType: "json"
            }).then(function (res) {
                var comDet = res.data.Data;
                if (comDet) {
                    $scope.CostCenterBreakupLedgerWise.DateFrom_TMP = new Date(comDet.StartDate);
                }
            }, function (errormessage) {
                alert('Unable to Delete data. pls try again.' + errormessage.responseText);
            });
        });

        $scope.ReportName = '';

        $scope.noofdecimal = 2;

        $scope.loadingstatus = "stop";


        $scope.columnDefs = [

            { headerName: "Ledger", width: 250, field: "LedgerName", filter: 'agTextColumnFilter', pinned: 'left', dataType: 'Text', cellStyle: { 'text-align': 'text' } },
            { headerName: "OpeningDr", width: 150, field: "OpeningDr", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "OpeningCr", width: 150, field: "OpeningCr", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Opening", width: 120, field: "Opening", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "DrAmt", width: 120, field: "DrAmt", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "CrAmt", width: 120, field: "CrAmt", dataType: 'Number', cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "ClosingCr", width: 180, dataType: 'Number', field: "ClosingCr", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "ClosingDr", width: 180, dataType: 'Number', field: "ClosingDr", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Closing", width: 120, dataType: 'Number', field: "Closing", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "Branch", width: 170, dataType: 'Text', field: "Branch", cellStyle: { 'text-align': 'right' }, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },

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
            headerHeight: 31,
            rowHeight: 30,
            columnDefs: $scope.columnDefs,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            rowSelection: 'multiple',
            suppressHorizontalScroll: true,
            alignedGrids: [],

            onFilterChanged: function () {

                var dt = {
                    LedgerName: 'TOTAL =>',
                    OpeningDr: 0,
                    OpeningCr: 0,
                    Opening: 0,
                    DrAmt: 0,
                    CrAmt: 0,
                    ClosingCr: 0,
                    ClosingDr: 0,
                    Closing: 0,

                }
                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var fData = node.data;
                    dt.OpeningDr += fData.OpeningDr;
                    dt.OpeningCr += fData.OpeningCr;
                    dt.Opening += fData.Opening;
                    dt.DrAmt += fData.DrAmt;
                    dt.CrAmt += fData.CrAmt;
                    dt.ClosingCr += fData.ClosingCr;
                    dt.ClosingDr += fData.ClosingDr;
                    dt.Closing += fData.Closing;

                });


                var filterDataColl = [];
                filterDataColl.push(dt);

                $scope.gridOptionsBottom.api.setRowData(filterDataColl);
            }

        };


        // lookup the container we want the Grid to use
        //  $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        // new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);


        $scope.dataForBottomGrid = [
            {
                IsParent: true,

                LedgerName: 'Opening Balance =>',
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

                LedgerName: 'Current Total =>',
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

                LedgerName: 'Closing Balance =>',
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


    $scope.GetCostCenterBreakupLedgerWise = function () {

        $scope.ClearData();

        if (!$scope.CostCenterBreakupLedgerWise.AgentId)
            return;

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.CostCenterBreakupLedgerWise.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.CostCenterBreakupLedgerWise.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.CostCenterBreakupLedgerWise.DateToDet)
            dateTo = new Date(($filter('date')($scope.CostCenterBreakupLedgerWise.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.DataColl = []; //declare an empty array

        var beData = {
            DateFrom: dateFrom,
            DateTo: dateTo,

            CostCenterId: $scope.CostCenterBreakupLedgerWise.CostCenterId
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Account/Reporting/GetCostCenterBreakupLedgerWise",
            data: JSON.stringify(beData),
            dataType: "json"
        }).then(function (res) {

            var openingAmt = 0, drAmt = 0, crAmt = 0, closingAmt = 0;
            openingAmt = res.data.Data.OpeningAmt;
            drAmt = res.data.Data.DrAmt;
            crAmt = res.data.Data.CrAmt;
            closingAmt = res.data.Data.ClosingAmt;

            $scope.CostCenterBreakupLedgerWise.ODr = (openingAmt > 0 ? openingAmt : 0);
            $scope.CostCenterBreakupLedgerWise.OCr = (openingAmt < 0 ? Math.abs(openingAmt) : 0);
            $scope.CostCenterBreakupLedgerWise.TDr = drAmt;
            $scope.CostCenterBreakupLedgerWise.TCr = crAmt;
            $scope.CostCenterBreakupLedgerWise.CDr = (closingAmt > 0 ? closingAmt : 0);
            $scope.CostCenterBreakupLedgerWise.CCr = (closingAmt < 0 ? Math.abs(closingAmt) : 0);

            if (openingAmt > 0)
                $scope.dataForBottomGrid[0].DrAmt = openingAmt;
            else
                $scope.dataForBottomGrid[0].CrAmt = Math.abs(openingAmt);

            $scope.dataForBottomGrid[1].DrAmt = drAmt;
            $scope.dataForBottomGrid[1].CrAmt = crAmt;

            if (closingAmt > 0)
                $scope.dataForBottomGrid[2].DrAmt = closingAmt;
            else
                $scope.dataForBottomGrid[2].CrAmt = Math.abs(closingAmt);

            $scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);

            $scope.DataColl = res.data.Data.DataColl;
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
                                                        Period: $scope.CostCenterBreakupLedgerWise.DateFromDet.dateBS + " TO " + $scope.CostCenterBreakupLedgerWise.DateToDet.dateBS,
                                                        ODr: $scope.CostCenterBreakupLedgerWise.ODr,
                                                        OCr: $scope.CostCenterBreakupLedgerWise.OCr,
                                                        TDr: $scope.CostCenterBreakupLedgerWise.TDr,
                                                        TCr: $scope.CostCenterBreakupLedgerWise.TCr,
                                                        CDr: $scope.CostCenterBreakupLedgerWise.CDr,
                                                        CCr: $scope.CostCenterBreakupLedgerWise.CCr
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
                                    Period: $scope.CostCenterBreakupLedgerWise.DateFromDet.dateBS + " TO " + $scope.CostCenterBreakupLedgerWise.DateToDet.dateBS,
                                    ODr: $scope.CostCenterBreakupLedgerWise.ODr,
                                    OCr: $scope.CostCenterBreakupLedgerWise.OCr,
                                    TDr: $scope.CostCenterBreakupLedgerWise.TDr,
                                    TCr: $scope.CostCenterBreakupLedgerWise.TCr,
                                    CDr: $scope.CostCenterBreakupLedgerWise.CDr,
                                    CCr: $scope.CostCenterBreakupLedgerWise.CCr
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

    $scope.DownloadAsXls = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();
        var dataColl = $scope.GetDataForPrint();
        var paraData = {
            Period: $scope.CostCenterBreakupLedgerWise.DateFromDet.dateBS + " TO " + $scope.CostCenterBreakupLedgerWise.DateToDet.dateBS,
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
                down_file(base_url + "//" + res.data.Data.ResponseId, "CostCenterMonthly.xlsx");
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire(errormessage);
        });
    }

});
