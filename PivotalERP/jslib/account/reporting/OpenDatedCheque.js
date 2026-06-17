

"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller("OpenDatedCheque", function ($scope, $http, $filter) {
    $scope.Title = 'OpenDatedCheque';

    LoadData();

    function LoadData() {

        $scope.columnDefs = [
            { headerName: "Code", field: "Code", filter: 'agNumberColumnFilter', width: 100, dataType: 'Text', cellStyle: { 'text-align': 'left' }, pinned: 'left', },
            { headerName: "LedgerName", field: "LedgerName", filter: "agTextColumnFilter", width: 190, dataType: 'Text', cellStyle: { 'text-align': 'left' }, pinned: 'left', },
            { headerName: "Agent", field: "AgentName", filter: "agTextColumnFilter", width: 190, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "BankName", field: "BankName", filter: "agTextColumnFilter", width: 190, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "BankBranch", field: "BankBranch", filter: "agTextColumnFilter", width: 190, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "ChequeNo", field: "ChequeNo", filter: "agTextColumnFilter", width: 190, dataType: 'Text', cellStyle: { 'text-align': 'center' } },
            { headerName: "Amount", field: "Amount", filter: "agTextColumnFilter", width: 140, dataType: 'Text', cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return $filter('formatNumber')(params.value); } },
            { headerName: "VoucherDate", field: "VoucherDate", filter: "agTextColumnFilter", width: 150, dataType: 'DateTime', cellStyle: { 'text-align': 'center' } },
            { headerName: "VoucherDateBS", field: "VoucherDateBS", filter: "agTextColumnFilter", width: 150, dataType: 'DateTime', cellStyle: { 'text-align': 'center' } },
            { headerName: "Notes", field: "Notes", filter: "agTextColumnFilter", width: 190, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "IsCancel", field: "IsCancel", filter: "agTextColumnFilter", width: 150, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "CancelDate", field: "CancelDate", filter: "agTextColumnFilter", width: 190, dataType: 'DateTime', cellStyle: { 'text-align': 'center' } },
            { headerName: "CancelRemarks", field: "CancelRemarks", filter: "agTextColumnFilter", width: 190, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "UserName", field: "UserName", filter: "agTextColumnFilter", width: 150, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "HaveDocument", field: "HaveDocument", filter: "agTextColumnFilter", width: 150, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
        ];


        $scope.gridOptions = {
            columnDefs: $scope.columnDefs,

            defaultColDef: {
                resizable: true,
                sortable: true,
                filter: true,
                resizable: true,
                cellStyle: { 'line-height': '31px' },
                rowSelection: 'multiple'
            },
            headerHeight: 35,
            rowHeight: 30,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            rowSelection: 'multiple',

        };

        $scope.loadingstatus = "stop";

    }
    $scope.ClearData = function () {

        var DataColl = [];
        // $scope.gridOptionsBottom.api.setRowData(DataColl);

        $scope.gridOptions.api.setRowData(DataColl);
    };
    $scope.GetAllOpenDatedCheque = function () {

        $scope.ClearData();
        $scope.loadingstatus = 'running';
        showPleaseWait();


        $http({
            method: 'GET',
            url: base_url + "Account/Reporting/GetAllOpenDatedCheque",
            dataType: "json"
            //data:JSON.stringify(para)
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

                // $scope.gridOptionsBottom.api.setRowData(filterDataColl);

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

    $scope.DownloadAsXls = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();

        var dataColl = $scope.GetDataForPrint();
        //var paraData = {
        //    Period: $scope.newPDC.DateFromDet.dateBS + " TO " + $scope.newPDC.DateToDet.dateBS,
        //};

        $http({
            method: 'POST',
            url: base_url + "Global/PrintXlsReportData",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("entityId", EntityId);
                formData.append("jsonData", angular.toJson(data.jsonData));
              /*  formData.append("paraData", angular.toJson(paraData));*/
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