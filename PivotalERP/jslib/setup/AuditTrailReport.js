app.controller('AuditTrailReportController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Audit Trail Report';
    getterAndSetter();

    $scope.onBtExportCSV = function () {
        var params = {
            fileName: 'salesMaterializedView.csv',
            sheetName: 'salesMaterializedView'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }


    function getterAndSetter() {
        $scope.columnDefs = [
            { headerName: "S.No.", field: "SNo", width: 80, filter: "agNumberColumnFilter", },
            { headerName: "Tran Id", field: "TranId", width: 120, filter: "agNumberColumnFilter", },
            { headerName: "Auto No", field: "AutoManualNo", width: 140, filter: "agTextColumnFilter", },
            { headerName: "Action", field: "Action", width: 140, filter: "agTextColumnFilter", },
            { headerName: "Entity", field: "EntityId", width: 160, filter: "agTextColumnFilter", },
            { headerName: "User", field: "UserName", width: 160, filter: "agTextColumnFilter", },
            { headerName: "System User", field: "SystemUser", width: 160, filter: "agTextColumnFilter", },
            { headerName: "System Name", field: "PCName", width: 160, filter: "agTextColumnFilter", },
            { headerName: "MAC Address", field: "MacAddress", width: 160, filter: "agTextColumnFilter", },
            { headerName: "Log Date", field: "LogDate", width: 160, filter: "agDateColumnFilter", },
            { headerName: "Remarks", field: "LogText", width: 160, filter: "agTextColumnFilter", },
        ];

        // let the grid know which columns and what data to use
        $scope.gridOptions = {

            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,

                // set every column width
                width: 90
            },
            columnDefs: $scope.columnDefs,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            rowSelection: 'multiple',
            overlayLoadingTemplate: "Please Click the Load Button to display the data.",
        };



    }


    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();

        $scope.newAuditTrailReport = {
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(),
            UserId: 0,
            EntityId: 0,
            ActionType: 0
        };

        $scope.ActionList = [{ id: 1, text: 'Save' }, { id: 1, text: 'Modify' }, { id: 1, text: 'Delete' }, { id: 1, text: 'Print' }, { id: 1, text: 'View' },];

        $scope.UserList = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllUserList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.UserList = res.data.Data;

            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };

    $scope.GetAuditLog = function () {

        var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
        var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

        if ($scope.newAuditTrailReport.DateFromDet)
            dateFrom = new Date(($filter('date')($scope.newAuditTrailReport.DateFromDet.dateAD, 'yyyy-MM-dd')));

        if ($scope.newAuditTrailReport.DateToDet)
            dateTo = new Date(($filter('date')($scope.newAuditTrailReport.DateToDet.dateAD, 'yyyy-MM-dd')));

        $scope.DataColl = []; //declare an empty array
        $scope.gridOptions.api.setRowData($scope.DataColl);

        var beData = {
            dateFrom: dateFrom,
            dateTo: dateTo,
            userId: $scope.newAuditTrailReport.UserId,
            entityId: $scope.newAuditTrailReport.EntityId,
            action: $scope.newAuditTrailReport.Action
        };

        $scope.loadingstatus = 'running';

        $http({
            method: "post",
            url: base_url + "Setup/Security/GetAuditLog",
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
                                                url: base_url + "Setup/Security/PrintAuditLog",
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
                                                    document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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
                            url: base_url + "Setup/Security/PrintAuditLog",
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
                                document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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