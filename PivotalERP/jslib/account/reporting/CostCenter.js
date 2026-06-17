"use strict";

agGrid.initialiseAgGridWithAngular1(angular);
app.controller("CostCenter", function ($scope, $http, $filter) {
    $scope.Title = 'CostCenter';
    LoadData();
    function Numberformat(val) {
        return $filter('number')(val, 2);
    }
    function LoadData() {
        $scope.loadingstatus = 'running';
        $scope.columnDefs = [
            { headerName: "S.No", field: "CostCenterId", filter: 'agNumberColumnFilter', width: 90, pinned: 'left', dataType: 'Number', cellStyle: { 'text-align': 'center' } },
            { headerName: "Name", field: "Name", filter: "agTextColumnFilter", width: 250, pinned: 'left', dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Alias", field: "Alias", filter: "agTextColumnFilter", width: 140, dataType: 'Text',  cellStyle: { 'text-align': 'left' } },
            { headerName: "Code", field: "Code", filter: "agTextColumnFilter", width: 140, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Email", field: "Email", filter: "agTextColumnFilter", width: 180, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "CostCategory", field: "CostCategoryName", filter: "agTextColumnFilter", dataType: 'Text', width: 180, cellStyle: { 'text-align': 'center' } },
            { headerName: "Status", field: "Status", filter: "agTextColumnFilter", width: 140, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Address", field: "Address", filter: "agTextColumnFilter", width: 200, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "PhoneNo", field: "PhoneNo", filter: "agTextColumnFilter", width: 150, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "PanVatNo", field: "PanVatNo", filter: "agTextColumnFilter", width: 150, dataType: 'Text', cellStyle: { 'text-align': 'right' } },
            { headerName: "Opening", field: "Opening", filter: "agNumberColumnFilter", width: 150, dataType: 'Number', cellStyle: { 'text-align': 'right' },filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, },
            { headerName: "DrCr", field: "DrCr", filter: "agTextColumnFilter", width: 150, dataType: 'Number', cellStyle: { 'text-align': 'right' } },
        ];

        $scope.gridOptions = {
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                width: 100,
            },
            headerHeight: 31,
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
                    Name: 'TOTAL =>',
                    Opening: 0,
                   
                }
                $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
                    var fData = node.data;
                    dt.Opening += fData.Opening;                 

                });

                var filterDataColl = [];
                filterDataColl.push(dt);

                $scope.gridOptionsBottom.api.setRowData(filterDataColl);
            }

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);

        $scope.dataForBottomGrid = [
            {
                
                Name: 'Total =>',
                Opening: 0,
                
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

        $scope.ClearData = function () {

            var DataColl = [];
            $scope.gridOptionsBottom.api.setRowData(DataColl);

            $scope.gridOptions.api.setRowData(DataColl);
        };

        $scope.GetAllCostCenter = function () {
            $scope.ClearData();
            if ($scope.loadingstatus != 'stop') {
                alert('Already Running Process')
                return;
            }
            $scope.loadingstatus = 'running';
            showPleaseWait();           
            $http({
                method: 'GET',
                url: base_url + "Account/Reporting/GetAllCostCenter",
                dataType: "json"
                //data:JSON.stringify(para)
            }).then(function (res) {
                $scope.loadingstatus = 'stop';
                hidePleaseWait();

                if (res.data.IsSuccess && res.data.Data) {
                    var DataColl = mx(res.data.Data);

                    var dt = {
                        Name: 'TOTAL =>',
                        Opening: DataColl.sum(p1 => p1.Opening),                       
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

    }

    $scope.DownloadAsXls = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var dataColl = $scope.GetDataForPrint();

        //var paraData = {
        //    Period: $scope.Groupwise.DateFromDet.dateBS + " TO " + $scope.Groupwise.DateToDet.dateBS,
        //};

        $http({
            method: 'POST',
            url: base_url + "Global/PrintXlsReportData",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("entityId", EntityId);
                formData.append("jsonData", angular.toJson(data.jsonData));
               /* formData.append("paraData", angular.toJson(paraData));*/
                formData.append("RptPath", "");
                return formData;
            },
            data: { jsonData: dataColl }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();
            if (res.data.IsSuccess && res.data.Data) {
                down_file(base_url + "//" + res.data.Data.ResponseId, "GroupSummary.xlsx");
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire(errormessage);
        });
    }


});