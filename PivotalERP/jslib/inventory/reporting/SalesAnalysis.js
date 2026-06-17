
"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller('SalesAnalysisController', function ($scope, $http, $timeout, $filter, GlobalServices, $compile) {
    $scope.Title = 'Sales Analysis';

    getterAndSetter();

    function getterAndSetter() {
        var columnDefs = [
            { headerName: "ProductName", width: 140, field: "ProductName" },
            { headerName: "PartNo", width: 120, field: "PartNo" },
            { headerName: "Code", width: 120, field: "Code" },
            { headerName: "GroupName", width: 120, field: "ProductGroupName" },
            { headerName: "Categories Name", width: 120, field: "ProductCategories" },          
            { headerName: "Sales Qty", width: 120, field: "OutQty" },
            { headerName: "Sales Rate", width: 120, field: "OutRate" },
            { headerName: "Sales Amt", width: 120, field: "OutAmt" },
            { headerName: "Return Qty", width: 120, field: "InQty" },
            { headerName: "Return Rate", width: 120, field: "InRate" },
            { headerName: "Return Amt", width: 120, field: "InAmt" },
            { headerName: "Net Sales Qty", width: 140, field: "BalanceQty" },
            { headerName: "Unit", width: 120, field: "Unit" },
            { headerName: "Net Sales Rate", width: 140, field: "BalanceRate" },
            { headerName: "Net Sales Amt", width: 140, field: "BalanceAmt" },

        ];

        // let the grid know which columns and what data to use
        $scope.gridOptionsproductwise = {
            defaultColDef: {
                resizable: true,
                sortable: true,
                filter: true,
                resizable: true,
                cellStyle: { 'line-height': '31px' },
                rowSelection: 'multiple'
            },
            columnDefs: columnDefs,
            rowHeight: 31,
            headerHeight: 31,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            enableSorting: true,
            rowSelection: 'multiple',

        };


        var columnDefs1 = [

            { headerName: "GroupName", width: 120, field: "ProductGroupName" },
            { headerName: "Categories Name", width: 120, field: "ProductCategories" },
            { headerName: "Sales Qty", width: 120, field: "OutQty" },
            { headerName: "Sales Rate", width: 120, field: "OutRate" },
            { headerName: "Sales Amt", width: 120, field: "OutAmt" },
            { headerName: "Return Qty", width: 120, field: "InQty" },
            { headerName: "Return Rate", width: 120, field: "InRate" },
            { headerName: "Return Amt", width: 120, field: "InAmt" },
            { headerName: "Net Sales Qty", width: 140, field: "BalanceQty" },
            { headerName: "Unit", width: 120, field: "Unit" },
            { headerName: "Net Sales Rate", width: 140, field: "BalanceRate" },
            { headerName: "Net Sales Amt", width: 140, field: "BalanceAmt" },

        ];

        // let the grid know which columns and what data to use
        $scope.gridOptionsproductgroupwise = {
            defaultColDef: {
                resizable: true,
                sortable: true,
                filter: true,
                resizable: true,
                cellStyle: { 'line-height': '31px' },
                rowSelection: 'multiple'
            },
            columnDefs: columnDefs1,
            rowHeight: 31,
            headerHeight: 31,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            enableSorting: true,
            rowSelection: 'multiple',
        };



        var columnDefs2 = [
            { headerName: "Particulars", width: 120, field: "ProductName" },
            { headerName: "Sales Qty", width: 120, field: "OutQty" },
            { headerName: "Sales Rate", width: 120, field: "OutRate" },
            { headerName: "Sales Amt", width: 120, field: "OutAmt" },
            { headerName: "Return Qty", width: 120, field: "InQty" },
            { headerName: "Return Rate", width: 120, field: "InRate" },
            { headerName: "Return Amt", width: 120, field: "InAmt" },
            { headerName: "Net Sales Qty", width: 120, field: "NetSalesQty" },
            { headerName: "BalanceRate", width: 120, field: "BalanceRate" },
            { headerName: "Net Sales Amt", width: 120, field: "NetSalesAmt" },
        ];

        // let the grid know which columns and what data to use
        $scope.gridOptionspartywise = {
            defaultColDef: {
                resizable: true,
                sortable: true,
                filter: true,
                resizable: true,
                cellStyle: { 'line-height': '31px' },
                rowSelection: 'multiple'
            },
            columnDefs: columnDefs2,
            rowHeight: 31,
            headerHeight: 31,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            enableSorting: true,
            rowSelection: 'multiple',
        };

    }


    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.searchData = {
            Productwise: '',
            ProductGroupwise: '',
            Partywise: '',
        };

        $scope.newProductwise = {
            ProductwiseId: null,
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            Mode: 'Save'
        };

        $scope.newProductGroupwise = {
            ProductGroupwiseId: null,
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            Mode: 'Save'
        };

        $scope.newPartywise = {
            PartywiseId: null,
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            forRpt: '',
            Mode: 'Save'
        };
    }

    $scope.GetProductWise = function () {

        var DataColl = []; //declare an empty array
        $scope.gridOptionsproductwise.api.setRowData(DataColl);

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.newProductwise.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.newProductwise.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.newProductwise.DateToDet)
            dateTo = new Date(($filter('date')($scope.newProductwise.DateToDet.dateAD, 'yyyy-MM-dd')));

        var para = {
            dateFrom: dateFrom,
            dateTo: dateTo
        };

        $scope.loadingstatus = "running";
        showPleaseWait();

        $http({
            method: "post",
            url: base_url + "Inventory/Reporting/GetSalesAnalysisProductWise",
            data: JSON.stringify(para),
            dataType: "json"
        }).then(function (res) {

            DataColl = res.data.Data;
            $scope.gridOptionsproductwise.api.setRowData(DataColl);

            $scope.loadingstatus = 'done';
            hidePleaseWait();

        }, function (errormessage) {

            $scope.loadingstatus = 'stop';

            alert('Unable to Store data. pls try again.' + errormessage.responseText);
        });

    };
    $scope.GetProductWiseDataForPrint = function () {

        var RptParamentersColl = [];

        RptParamentersColl.push({
            Name: "Period",
            Value: $('#dtDateFrom').val() + ' To ' + $('#dtDateTo').val()
        });


        var filterData = [];

        $scope.gridOptionsproductwise.api.forEachNodeAfterFilterAndSort(function (node) {
            var beData = node.data;
            filterData.push(beData);
        });

        return filterData;
    };

    $scope.PrintProductWise = function () {
        $http({
            method: 'GET',
            url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityProductWise + "&voucherId=0&isTran=false",
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
                                            var dataColl = $scope.GetProductWiseDataForPrint();
                                            print = true;
                                            $http({
                                                method: 'POST',
                                                url: base_url + "Inventory/Reporting/PrintSalesAnalysisProductWise",
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

                                                    var rptPara = {
                                                        rpttranid: rptTranId,
                                                        istransaction: false,
                                                        entityid: entityProductWise,
                                                        voucherid: 0,
                                                        tranid: 0,
                                                        vouchertype: 0,
                                                        sessionid: res.data.Data.ResponseId,
                                                        Period: $scope.newProductwise.DateFromDet.dateBS + " TO " + $scope.newProductwise.DateToDet.dateBS,
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
                        var dataColl = $scope.GetProductWiseDataForPrint();
                        print = true;

                        $http({
                            method: 'POST',
                            url: base_url + "Inventory/Reporting/PrintSalesAnalysisProductWise",
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

                                var rptPara = {
                                    rpttranid: rptTranId,
                                    istransaction: false,
                                    entityid: entityProductWise,
                                    voucherid: 0,
                                    tranid: 0,
                                    vouchertype: 0,
                                    sessionid: res.data.Data.ResponseId,
                                    Period: $scope.newProductwise.DateFromDet.dateBS + " TO " + $scope.newProductwise.DateToDet.dateBS,
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

    $scope.onFilterTextBoxChangedP1 = function () {
        $scope.gridOptionsproductwise.api.setQuickFilter($scope.search1);
    }

    $scope.ExportProductWiseAsCSV = function () {
        var params = {
            fileName: 'productwise.csv',
            sheetName: 'productwise'
        };
        $scope.gridOptionsproductwise.api.exportDataAsCsv(params);
    }


    $scope.GetProductGroupWise = function () {

        var DataColl = []; //declare an empty array
        $scope.gridOptionsproductgroupwise.api.setRowData(DataColl);

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.newProductGroupwise.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.newProductGroupwise.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.newProductGroupwise.DateToDet)
            dateTo = new Date(($filter('date')($scope.newProductGroupwise.DateToDet.dateAD, 'yyyy-MM-dd')));

        var para = {
            dateFrom: dateFrom,
            dateTo: dateTo
        };

        $scope.loadingstatus = "running";
        showPleaseWait();

        $http({
            method: "post",
            url: base_url + "Inventory/Reporting/GetSalesAnalysisProducGrouptWise",
            data: JSON.stringify(para),
            dataType: "json"
        }).then(function (res) {

            DataColl = res.data.Data;
            $scope.gridOptionsproductgroupwise.api.setRowData(DataColl);

            $scope.loadingstatus = 'done';
            hidePleaseWait();

        }, function (errormessage) {

            $scope.loadingstatus = 'stop';

            alert('Unable to Store data. pls try again.' + errormessage.responseText);
        });

    };
    $scope.GetProductGroupWiseDataForPrint = function () {

        var RptParamentersColl = [];

        RptParamentersColl.push({
            Name: "Period",
            Value: $('#dtDateFrom').val() + ' To ' + $('#dtDateTo').val()
        });


        var filterData = [];

        $scope.gridOptionsproductgroupwise.api.forEachNodeAfterFilterAndSort(function (node) {
            var beData = node.data;
            filterData.push(beData);
        });

        return filterData;
    };

    $scope.PrintProductGroupWise = function () {

        $http({
            method: 'GET',
            url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityProductGroupWise + "&voucherId=0&isTran=false",
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
                                            var dataColl = $scope.GetProductGroupWiseDataForPrint();
                                            print = true;
                                            $http({
                                                method: 'POST',
                                                url: base_url + "Inventory/Reporting/PrintSalesAnalysisProductWise",
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

                                                    var rptPara = {
                                                        rpttranid: rptTranId,
                                                        istransaction: false,
                                                        entityid: entityProductGroupWise,
                                                        voucherid: 0,
                                                        tranid: 0,
                                                        vouchertype: 0,
                                                        sessionid: res.data.Data.ResponseId,
                                                        Period: $scope.newProductGroupwise.DateFromDet.dateBS + " TO " + $scope.newProductGroupwise.DateToDet.dateBS,
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
                        var dataColl = $scope.GetProductGroupWiseDataForPrint();
                        print = true;

                        $http({
                            method: 'POST',
                            url: base_url + "Inventory/Reporting/PrintSalesAnalysisProductWise",
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

                                var rptPara = {
                                    rpttranid: rptTranId,
                                    istransaction: false,
                                    entityid: entityProductGroupWise,
                                    voucherid: 0,
                                    tranid: 0,
                                    vouchertype: 0,
                                    sessionid: res.data.Data.ResponseId,
                                    Period: $scope.newProductGroupwise.DateFromDet.dateBS + " TO " + $scope.newProductGroupwise.DateToDet.dateBS,
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

    $scope.onFilterTextBoxChangedP2 = function () {
        $scope.gridOptionsproductgroupwise.api.setQuickFilter($scope.search2);
    }

    $scope.ExportProductGroupWiseAsCSV = function () {
        var params = {
            fileName: 'productwise.csv',
            sheetName: 'productwise'
        };
        $scope.gridOptionsproductgroupwise.api.exportDataAsCsv(params);
    }



    $scope.GetPartyWiseWise = function () {

        var DataColl = []; //declare an empty array
        $scope.gridOptionspartywise.api.setRowData(DataColl);

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.newPartywise.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.newPartywise.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.newPartywise.DateToDet)
            dateTo = new Date(($filter('date')($scope.newPartywise.DateToDet.dateAD, 'yyyy-MM-dd')));

        var para = {
            dateFrom: dateFrom,
            dateTo: dateTo,
            forRpt: $scope.newPartywise.forRpt
        };

        $scope.loadingstatus = "running";
        showPleaseWait();

        $http({
            method: "post",
            url: base_url + "Inventory/Reporting/GetSalesAnalysisPartyWise",
            data: JSON.stringify(para),
            dataType: "json"
        }).then(function (res) {

            DataColl = res.data.Data;
            $scope.gridOptionspartywise.api.setRowData(DataColl);

            $scope.loadingstatus = 'done';
            hidePleaseWait();

        }, function (errormessage) {

            $scope.loadingstatus = 'stop';

            alert('Unable to Store data. pls try again.' + errormessage.responseText);
        });

    };
    $scope.GetPartyWiseDataForPrint = function () {

        var RptParamentersColl = [];

        RptParamentersColl.push({
            Name: "Period",
            Value: $('#dtDateFrom').val() + ' To ' + $('#dtDateTo').val()
        });


        var filterData = [];

        $scope.gridOptionspartywise.api.forEachNodeAfterFilterAndSort(function (node) {
            var beData = node.data;
            filterData.push(beData);
        });

        return filterData;
    };

    $scope.PrintPartyWise = function () {
        $http({
            method: 'GET',
            url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityPartyWiseWise + "&voucherId=0&isTran=false",
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
                                            var dataColl = $scope.GetPartyWiseDataForPrint();
                                            print = true;
                                            $http({
                                                method: 'POST',
                                                url: base_url + "Inventory/Reporting/PrintSalesAnalysisProductWise",
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

                                                    var rptPara = {
                                                        rpttranid: rptTranId,
                                                        istransaction: false,
                                                        entityid: entityPartyWiseWise,
                                                        voucherid: 0,
                                                        tranid: 0,
                                                        vouchertype: 0,
                                                        sessionid: res.data.Data.ResponseId,
                                                        Period: $scope.newPartywise.DateFromDet.dateBS + " TO " + $scope.newPartywise.DateToDet.dateBS,
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
                        var dataColl = $scope.GetPartyWiseDataForPrint();
                        print = true;

                        $http({
                            method: 'POST',
                            url: base_url + "Inventory/Reporting/PrintSalesAnalysisProductWise",
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

                                var rptPara = {
                                    rpttranid: rptTranId,
                                    istransaction: false,
                                    entityid: entityPartyWiseWise,
                                    voucherid: 0,
                                    tranid: 0,
                                    vouchertype: 0,
                                    sessionid: res.data.Data.ResponseId,
                                    Period: $scope.newPartywise.DateFromDet.dateBS + " TO " + $scope.newPartywise.DateToDet.dateBS,
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

    $scope.onFilterTextBoxChangedP2 = function () {
        $scope.gridOptionspartywise.api.setQuickFilter($scope.search2);
    }

    $scope.ExportPartyWiseAsCSV = function () {
        var params = {
            fileName: 'productwise.csv',
            sheetName: 'productwise'
        };
        $scope.gridOptionspartywise.api.exportDataAsCsv(params);
    }
});