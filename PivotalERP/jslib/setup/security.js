"use strict";

agGrid.initialiseAgGridWithAngular1(angular);


app.controller('webapiLogCtrl', function ($scope, $http, $filter, companyDet, GlobalServices) {
    LoadData();


    $scope.DataColl = [];
    $scope.DateFrom_TMP = new Date();
    $scope.DateTo_TMP = new Date();
    $scope.ForUser = '';
    function LoadData() {


        var columnDefs = [
            {

                headerName: "IPAddress", width: 120, field: "IPAddress",
                cellRenderer:
                    function (params) {
                        // params.$scope.PrintReceipt = PrintReceipt;
                        //  params.$scope.PrintReceipt=PrintReceipt;
                        if (params.data.IsChield)
                            return ""
                        else
                            return '<center><a   href="../..' + base_url + '/apilog/' + params.data.LogId + '.json" download target="_blank" rel="noopener"">' + params.data.IPAddress + '</a ></center>';
                    }
            },

            { headerName: "UserName", field: "UserName", filter: "agTextColumnFilter", width: 100 },
            { headerName: "PC Name", field: "MethodName", filter: "agTextColumnFilter", width: 100 },
            { headerName: "RequestContentType", field: "RequestContentType", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "RequestUri", field: "RequestUri", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "RequestMethod", field: "RequestMethod", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "RequestTimestamp", field: "RequestTimestamp", width: 150, cellStyle: { 'text-align': 'center' } },
            { headerName: "ResponseContentType", field: "ResponseContentType", filter: 'agTextColumnFilter', width: 180 },
            { headerName: "ResponseStatusCode", field: "ResponseStatusCode", filter: 'agTextColumnFilter', width: 150 },
            { headerName: "ResponseTimestamp", field: "ResponseTimestamp", width: 180, cellStyle: { 'text-align': 'center' } }

        ];

        $scope.gridOptions = {
            //angularCompileRows: true,
            // a default column definition with properties that get applied to every column
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                // set every column width
                width: 100,

            },
            enableSorting: true,
            multiSortKey: 'ctrl',
            enableColResize: true,
            overlayLoadingTemplate: "Please Click the Load Button to display the data.",
            overlayNoRowsTemplate: "No Records found",
            rowSelection: 'multiple',
            columnDefs: columnDefs,
            rowData: null,
            filter: true,
            //suppressHorizontalScroll: true,
            alignedGrids: [],
            enableFilter: true

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);


    }

    $scope.GetData = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var para = {
            dateFrom: $filter('date')(new Date($scope.DateFromDet.dateAD), 'yyyy-MM-dd'),
            dateTo: $filter('date')(new Date($scope.DateToDet.dateAD), 'yyyy-MM-dd'),
            forUser: $scope.ForUser
        };

        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetWebAPILog",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $scope.DataColl = res.data.Data;

                $scope.gridOptions.api.setRowData($scope.DataColl);


            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });



    }

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    $scope.onBtExport = function () {
        var params = {
            fileName: 'log.csv',
            sheetName: 'data'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
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

app.controller('smsapiLogCtrl', function ($scope, $http, $filter, companyDet, GlobalServices) {
    LoadData();


    $scope.DataColl = [];
    $scope.DateFrom = new Date();
    $scope.DateTo = new Date();

    function LoadData() {
        $scope.DateFrom_TMP = new Date();
        $scope.DateTo_TMP = new Date();

        var columnDefs = [
            //  { headerName: "IPAddress", field: "IPAddress", filter: "agTextColumnFilter", width: 210, pinned: 'left' },
            { headerName: "MobileNo", field: "PhoneNo", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Message", field: "Message", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Entity", field: "Entity", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "User", field: "UserName", width: 150, cellStyle: { 'text-align': 'center' } },
            { headerName: "MacAddress", field: "MacAddress", filter: 'agTextColumnFilter', width: 180 },
            { headerName: "Response", field: "ResponseLog", filter: 'agTextColumnFilter', width: 150 },
            { headerName: "LogDateTime", field: "LogDateTime", filter: 'agTextColumnFilter', width: 150 },
            { headerName: "DeliverAt", field: "SendDateTime", width: 180, cellStyle: { 'text-align': 'center' } }

        ];

        $scope.gridOptions = {
            //angularCompileRows: true,
            // a default column definition with properties that get applied to every column
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                // set every column width
                width: 100,

            },
            enableSorting: true,
            multiSortKey: 'ctrl',
            enableColResize: true,
            overlayLoadingTemplate: "Please Click the Load Button to display the data.",
            overlayNoRowsTemplate: "No Records found",
            rowSelection: 'multiple',
            columnDefs: columnDefs,
            rowData: null,
            filter: true,
            //suppressHorizontalScroll: true,
            alignedGrids: [],
            enableFilter: true

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);


    }

    $scope.GetData = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var para = {
            dateFrom: $filter('date')(new Date($scope.DateFromDet.dateAD), 'yyyy-MM-dd'),
            dateTo: $filter('date')(new Date($scope.DateToDet.dateAD), 'yyyy-MM-dd')
        };
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetSMSAPILog",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $scope.DataColl = res.data.Data;

                $scope.gridOptions.api.setRowData($scope.DataColl);


            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });

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

    }

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    $scope.onBtExport = function () {
        var params = {
            fileName: 'log.csv',
            sheetName: 'data'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
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

app.controller('smsSendLogCtrl', function ($scope, $http, $filter, companyDet, GlobalServices) {
    LoadData();


    $scope.DataColl = [];
    $scope.DateFrom = new Date();
    $scope.DateTo = new Date();

    function LoadData() {
        $scope.DateFrom_TMP = new Date();
        $scope.DateTo_TMP = new Date();

        var columnDefs = [
            //  { headerName: "IPAddress", field: "IPAddress", filter: "agTextColumnFilter", width: 210, pinned: 'left' },
            { headerName: "SNo", field: "SNo", filter: 'agTextColumnFilter', width: 120 },
            { headerName: "MobileNo", field: "ContactNo", filter: 'agTextColumnFilter', width: 120 },
            { headerName: "Message", field: "MSG", filter: 'agTextColumnFilter', width: 300 },
            { headerName: "SendAt", field: "LogMitiTime", filter: 'agTextColumnFilter', width: 140 },

        ];

        $scope.gridOptions = {
            //angularCompileRows: true,
            // a default column definition with properties that get applied to every column
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                // set every column width
                width: 100,

            },
            enableSorting: true,
            multiSortKey: 'ctrl',
            enableColResize: true,
            overlayLoadingTemplate: "Please Click the Load Button to display the data.",
            overlayNoRowsTemplate: "No Records found",
            rowSelection: 'multiple',
            columnDefs: columnDefs,
            rowData: null,
            filter: true,
            //suppressHorizontalScroll: true,
            alignedGrids: [],
            enableFilter: true

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);


    }

    $scope.GetData = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var para = {
            dateFrom: $filter('date')(new Date($scope.DateFromDet.dateAD), 'yyyy-MM-dd'),
            dateTo: $filter('date')(new Date($scope.DateToDet.dateAD), 'yyyy-MM-dd')
        };
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetSMSSendLog",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $scope.DataColl = res.data.Data;

                $scope.gridOptions.api.setRowData($scope.DataColl);


            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });



    }

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    $scope.onBtExport = function () {
        var params = {
            fileName: 'log.csv',
            sheetName: 'data'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
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

app.controller('notificationLogCtrl', function ($scope, $http, $filter, companyDet, GlobalServices) {
    LoadData();


    $scope.DataColl = [];
    $scope.DateFrom = new Date();
    $scope.DateTo = new Date();

    function LoadData() {
        $scope.DateFrom_TMP = new Date();
        $scope.DateTo_TMP = new Date();

        var columnDefs = [
            //  { headerName: "IPAddress", field: "IPAddress", filter: "agTextColumnFilter", width: 210, pinned: 'left' },
            { headerName: "CustomerCode", field: "CustomerCode", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Heading", field: "Heading", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Subject", field: "Subject", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Content", field: "Content", width: 150, cellStyle: { 'text-align': 'center' } },
            { headerName: "UserName", field: "UserName", filter: 'agTextColumnFilter', width: 180 },
            { headerName: "EntityName", field: "EntityName", filter: 'agTextColumnFilter', width: 150 },
            { headerName: "RequestLog", field: "RequestLog", filter: 'agTextColumnFilter', width: 150 },
            { headerName: "ResponseLog", field: "ResponseLog", filter: 'agTextColumnFilter', width: 150 },
            { headerName: "LogDateTime", field: "LogDateTime", filter: 'agTextColumnFilter', width: 150 },
            { headerName: "IsRead", field: "IsRead", filter: 'agTextColumnFilter', width: 120 }
        ];

        $scope.gridOptions = {
            //angularCompileRows: true,
            // a default column definition with properties that get applied to every column
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                // set every column width
                width: 100,

            },
            enableSorting: true,
            multiSortKey: 'ctrl',
            enableColResize: true,
            overlayLoadingTemplate: "Please Click the Load Button to display the data.",
            overlayNoRowsTemplate: "No Records found",
            rowSelection: 'multiple',
            columnDefs: columnDefs,
            rowData: null,
            filter: true,
            //suppressHorizontalScroll: true,
            alignedGrids: [],
            enableFilter: true

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);


    };

    $scope.GetData = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var para = {
            dateFrom: $filter('date')(new Date($scope.DateFromDet.dateAD), 'yyyy-MM-dd'),
            dateTo: $filter('date')(new Date($scope.DateToDet.dateAD), 'yyyy-MM-dd')
        };

        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetNotificationLog",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $scope.DataColl = res.data.Data;

                $scope.gridOptions.api.setRowData($scope.DataColl);


            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });



    };

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    $scope.onBtExport = function () {
        var params = {
            fileName: 'notificationlog.csv',
            sheetName: 'data'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
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

app.controller('emailLogCtrl', function ($scope, $http, $filter, companyDet, GlobalServices) {
    LoadData();


    $scope.DataColl = [];
    $scope.DateFrom = new Date();
    $scope.DateTo = new Date();

    function LoadData() {

        $scope.DateFrom_TMP = new Date();
        $scope.DateTo_TMP = new Date();

        var columnDefs = [
            //  { headerName: "IPAddress", field: "IPAddress", filter: "agTextColumnFilter", width: 210, pinned: 'left' },
            { headerName: "From", field: "From", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "To", field: "To", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Cc", field: "Cc", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Subject", field: "Subject", width: 150, cellStyle: { 'text-align': 'center' } },
            { headerName: "Message", field: "Message", filter: 'agTextColumnFilter', width: 180 },
            { headerName: "Smtp", field: "Smtp", filter: 'agTextColumnFilter', width: 150 },
            { headerName: "User", field: "UserName", filter: 'agTextColumnFilter', width: 150 },
            { headerName: "ResponseLog", field: "ResponseMSG", filter: 'agTextColumnFilter', width: 150 },
            { headerName: "LogDateTime", field: "LogDateTime", filter: 'agTextColumnFilter', width: 150 }
        ];

        $scope.gridOptions = {
            //angularCompileRows: true,
            // a default column definition with properties that get applied to every column
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                // set every column width
                width: 100,

            },
            enableSorting: true,
            multiSortKey: 'ctrl',
            enableColResize: true,
            overlayLoadingTemplate: "Please Click the Load Button to display the data.",
            overlayNoRowsTemplate: "No Records found",
            rowSelection: 'multiple',
            columnDefs: columnDefs,
            rowData: null,
            filter: true,
            //suppressHorizontalScroll: true,
            alignedGrids: [],
            enableFilter: true

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);


    };

    $scope.GetData = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var para = {
            dateFrom: $filter('date')(new Date($scope.DateFromDet.dateAD), 'yyyy-MM-dd'),
            dateTo: $filter('date')(new Date($scope.DateToDet.dateAD), 'yyyy-MM-dd')
        };

        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetEmailLog",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $scope.DataColl = res.data.Data;

                $scope.gridOptions.api.setRowData($scope.DataColl);


            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });



    };

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    $scope.onBtExport = function () {
        var params = {
            fileName: 'emaillog.csv',
            sheetName: 'data'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
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

app.controller('emailSetupCtrl', function ($scope, $http, $filter) {

    $scope.Title = 'Email Setup';

    LoadData();
    function LoadData() {

        $scope.loadingstatus = 'running';

        $scope.beData = {
            EmailId: '',
            UserName: '',
            Password: '',
            SMTP: '',
            Port: 0,
            UseSSL: false,
            SMSUser: '',
            SMSPwd: '',
            SMSUserId: '',
            Mode: 'Email Setup'
        };
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/getEmailSetup",
            dataType: "json"
            //data:JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $scope.beData = res.data.Data;

                if ($scope.beData.SMSUserId && $scope.beData.SMSUserId.length > 0)
                    $scope.beData.SMSUserId = parseInt($scope.beData.SMSUserId);
            } else
                alert(res.data.ResponseMSG);

        });

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


    }

    $scope.UpdateEmailSetup = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();
        $http({
            method: "post",
            url: base_url + "Setup/Security/UpdateEmailSetup",
            data: JSON.stringify($scope.beData),
            dataType: "json"
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            alert(res.data.ResponseMSG);

        });

    };

});

app.controller('oneSignalSetupCtrl', function ($scope, $http, $filter, companyDet) {

    $scope.Title = 'OneSignal Setup';

    LoadData();
    function LoadData() {

        $scope.loadingstatus = 'running';

        showPleaseWait();

        $scope.beData = {
            ApiId: '',
            ApiKey: '',
            Mode: 'OneSignal Setup'
        };
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/getOneSignalSetup",
            dataType: "json"
            //data:JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $scope.beData = res.data.Data;

            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });

    }

    $scope.UpdateOneSignalSetup = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();
        $http({
            method: "post",
            url: base_url + "Setup/Security/UpdateOneSignalSetup",
            data: JSON.stringify($scope.beData),
            dataType: "json"
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            alert(res.data.ResponseMSG);

        }, function (errormessage) {
            alert('Unable to Delete data. pls try again.' + errormessage.responseText);
        });

    };

});

app.controller("sentCntrl", function ($scope, $http, $filter, $timeout) {

    $scope.Title = 'Templates';

    LoadData();

    function LoadData() {
        $scope.loadingstatus = 'running';

        $('#txtBody').summernote({
            // focus: true,
            height: 120,
            codemirror: {
                theme: 'united'
            }
        });

        $scope.cronOptions = {
            //allowMultiple: true,
            //options: {
            //    allowWeek: false,
            //    allowMonth: false,
            //    allowYear: false
            //},
            quartz: true
        }

        $('.select2').select2({ allowClear: true, width: '100%' });

        $scope.ModuleColl = [];
        $http.get(base_url + "Setup/ReportWriter/GetModuleList").then(function (res) {
            $scope.ModuleColl = res.data.Data;
        }, function (reason) { alert('Failed: ' + reason); });

        $scope.ActionColl = [];
        $http.get(base_url + "Setup/ReportWriter/GetActionList").then(function (res) {
            $scope.ActionColl = res.data.Data;
        }, function (reason) { alert('Failed: ' + reason); });

        $scope.loadingstatus = 'stop';

        $scope.UserColl = []; //declare an empty array
        $http.post(base_url + "Setup/Security/GetAllUserList").then(
            function (res) {
                $scope.UserColl = res.data.Data;
                $scope.loadingstatus = "stop";
            }
            , function (reason) {
                alert('Failed: ' + reason);
            }
        );

        $scope.beData = {
            IsReport: false,
            ModuleId: 0,
            EntityId: 0,
            For: 0,
            Title: '',
            CRON: '',
            Message: '',
            Status: true,
            ToLedger: false,
            OnAction: 1,
            Mode: '',
            ForTemplate: 3
        };
    }

    $scope.getModuleWiseEntityList = function () {
        $scope.DataColl = [];
        $http.post(base_url + "Setup/Security/GetEntityByModule?modul=" + $scope.beData.ModuleId + "&IsReport=" + $scope.beData.IsReport + "&ForTemplate=" + $scope.beData.ForTemplate).then(function (res) {
            $scope.DataColl = res.data.Data;
        }, function (errormessage) {
            alert('Unable to get data for update.' + errormessage.responseText);
        });
    }

    $scope.LoadEntityWiseProperties = function (enDet, forTemp) {
        $scope.beData.EntityId = enDet.Id;
        $scope.beData.For = 1;
        $scope.beData.ForTemplate = forTemp;
        $scope.PropertesColl = [];
        $http.post(base_url + "Setup/Security/GetPropertiesList?entityId=" + enDet.Id + "&IsReport=" + $scope.beData.IsReport).then(function (res) {
            $scope.PropertesColl = res.data.Data;

            $http.post(base_url + "Setup/Security/GetSENT?EntityId=" + enDet.Id + "&IsReport=" + $scope.beData.IsReport + "&ForTemplate=" + $scope.beData.ForTemplate).then(function (res2) {
                var tmpData = res2.data.Data;
                $scope.beData.OnAction = tmpData.OnAction;
                $scope.beData.Status = tmpData.Status;
                $scope.beData.Title = tmpData.Title;
                $scope.beData.Message = tmpData.Message;
                $scope.beData.CRON = tmpData.CRON;
                $scope.beData.UserIdColl = tmpData.UserIdColl;

                $('#txtBody').summernote('code', tmpData.Message);
                if ($scope.beData.UserIdColl) {
                    $timeout(function () {
                        var ethin = [];
                        angular.forEach($scope.beData.UserIdColl, function (edet) {
                            ethin.push(edet);
                        })
                        $('#cboUser').val(ethin).trigger('change');
                    });
                }


            }, function (errormessage) {
                alert('Unable to get data for update.' + errormessage.responseText);
            });

        }, function (errormessage) {
            alert('Unable to get data for update.' + errormessage.responseText);
        });
    };
    $scope.AddVariable = function (vName) {
        $('#txtBody').summernote('insertText', '##' + vName.toString().toLowerCase() + '##');
    }
    $scope.Validate = function () {
        var isValid = true;


        return isValid;
    }


    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;   //set the sortKey to the param passed
        $scope.reverse = !$scope.reverse; //if true make it false and vice versa
    }

    $scope.AddNewTemplate = function () {

        var isValid = $scope.Validate();

        if (!isValid)
            return;

        if ($('#cboUser').val() != null) {
            var arr = ($('#cboUser').val().toString()).split(',');
            $scope.beData.UserIdColl = arr.map(Number);
        }

        $scope.beData.Message = $('#txtBody').summernote('code');

        $scope.loadingstatus = 'running';

        $http({
            method: 'POST',
            url: base_url + "Setup/Security/SaveUpdateSENT",
            data: JSON.stringify($scope.beData)
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            alert(res.data.ResponseMSG);

            if (res.data.IsSuccess) {
                $scope.ClearFields();
            }

        }, function (errormessage) {

            $scope.loadingstatus = "stop";
            alert('Unable to store(save) data. pls try again.' + errormessage.responseText);

        });


    }


    $scope.deleteQueryBuilder = function (beData) {

        var ans = confirm("Are you sure you want to delete this Record?");

        if (ans) {
            var getData = $http({
                method: "post",
                url: base_url + "Setup/ReportWriter/DeleteQueryBuilderById",
                data: JSON.stringify(beData),
                dataType: "json"
            });

            getData.then(function (res) {
                alert(res.data.Data.ResponseMSG);
                if (res.data.Data.IsSuccess) {
                    $scope.GetAllQueryBuilder();
                }

            }, function (errormessage) {
                alert('Unable to Delete data. pls try again.' + errormessage.responseText);
            });
        }

    }

    $scope.ClearFields = function () {
        $scope.beData = {
            IsReport: false,
            ModuleId: 0,
            EntityId: 0,
            For: 0,
            Title: '',
            CRON: '',
            Message: '',
            Status: true,
            ToLedger: false,
            OnAction: 1,
            Mode: ''
        };
        $('#txtBody').summernote('code', '');
    }


});



app.controller('sqlErrorLogCtrl', function ($scope, $http, $filter, companyDet, GlobalServices) {
    LoadData();


    $scope.DataColl = [];
    $scope.DateFrom_TMP = new Date();
    $scope.DateTo_TMP = new Date();

    function LoadData() {

        var columnDefs = [
            {

                headerName: "TranId", width: 120, field: "TranId",
            },
            { headerName: "Number", field: "Number", filter: 'agTextColumnFilter', width: 120 },
            { headerName: "State", field: "State", filter: 'agTextColumnFilter', width: 120 },
            { headerName: "Severity", field: "Severity", filter: 'agTextColumnFilter', width: 120 },
            { headerName: "Procedure", field: "Procedure", width: 160, },
            { headerName: "ErrorLine", field: "ErrorLine", filter: 'agTextColumnFilter', width: 120 },
            { headerName: "ErrorMessage", field: "ErrorMessage", filter: 'agTextColumnFilter', width: 220 },
            { headerName: "UserName", field: "UserName", width: 140, cellStyle: { 'text-align': 'center' } },
            { headerName: "LogMiti", field: "LogMiti", width: 140, cellStyle: { 'text-align': 'center' } }

        ];

        $scope.gridOptions = {
            //angularCompileRows: true,
            // a default column definition with properties that get applied to every column
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                // set every column width
                width: 100,

            },
            enableSorting: true,
            multiSortKey: 'ctrl',
            enableColResize: true,
            overlayLoadingTemplate: "Please Click the Load Button to display the data.",
            overlayNoRowsTemplate: "No Records found",
            rowSelection: 'multiple',
            columnDefs: columnDefs,
            rowData: null,
            filter: true,
            //suppressHorizontalScroll: true,
            alignedGrids: [],
            enableFilter: true

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);


    }

    $scope.GetData = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetSqlErrorLog?dateFrom=" + $scope.DateFromDet.dateAD.toJSON() + "&dateTo=" + $scope.DateToDet.dateAD.toJSON(),
            dataType: "json"
            //data:JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $scope.DataColl = res.data.Data;

                $scope.gridOptions.api.setRowData($scope.DataColl);


            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });



    }

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    $scope.onBtExport = function () {
        var params = {
            fileName: 'log.csv',
            sheetName: 'data'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
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


app.controller('irdapiLogCtrl', function ($scope, $http, $filter, companyDet, GlobalServices) {
    LoadData();

    $scope.DataColl = [];
    $scope.DateFrom_TMP = new Date();
    $scope.DateTo_TMP = new Date();

    function LoadData() {

        $scope.apiLog = { DateFrom_TMP: new Date(), DateTo_TMP: new Date() };

        $scope.BranchList = [];
        $http({
            method: 'GET',
            url: base_url + "Setup/Security/GetAllBranchList",
            datatype: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BranchList = res.data.Data;
            }
        }, function (reason) {
            swal.fire('failed' + reason);
        });

        var columnDefs = [
            {
                headerName: "Action", width: 110, pinned: 'left', cellRenderer:
                    function (params) {

                        if (!params.data.Api_ResponseId || params.data.Api_ResponseId.length == 0)
                            return '<a class="btn btn-default btn-xs" ng-click="Download(this.data)"><i class="fas fa-file text-info"></i></a> <a class="btn btn-default btn-xs" ng-click="DownloadNew(this.data)"><i class="fas fa-file"></i></a>  <a class="btn btn-default btn-xs" ng-click="PushTran(this.data)"><i class="fas fa-upload"></i></a> ';
                        return '';
                    }
            },
            { headerName: "InvoiceNo", field: "InvoiceNo", filter: 'agTextColumnFilter', width: 140, pinned: 'left' },
            { headerName: "Voucher", field: "Voucher", width: 180, cellStyle: { 'text-align': 'center' }, pinned: 'left' },
            { headerName: "Date", field: "InvoiceDate", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Miti", field: "InvoiceMiti", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Branch", field: "Branch", width: 180, cellStyle: { 'text-align': 'left' } },
            { headerName: "Request", field: "RequestLog", filter: 'agTextColumnFilter', width: 180 },
            { headerName: "Response", field: "ResponseLog", filter: 'agTextColumnFilter', width: 150 },
            { headerName: "Party", field: "Party", width: 180, cellStyle: { 'text-align': 'left' } },
            { headerName: "BillType", field: "BillType", width: 150, cellStyle: { 'text-align': 'center' } },
            { headerName: "InvoiceDateTime", field: "InvoiceDateTime", width: 130, cellStyle: { 'text-align': 'center' } },
            { headerName: "LogDateTime", field: "LogDateTime", width: 180, cellStyle: { 'text-align': 'center' } },
            { headerName: "LogDateTime", field: "LogDateTime", width: 150, cellStyle: { 'text-align': 'center' } },
            { headerName: "IsCancel", field: "IsCancel", width: 120, cellStyle: { 'text-align': 'center' } },

        ];

        $scope.gridOptions = {
            angularCompileRows: true,
            // a default column definition with properties that get applied to every column
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                // set every column width
                width: 100,

            },
            enableSorting: true,
            multiSortKey: 'ctrl',
            enableColResize: true,
            overlayLoadingTemplate: "Please Click the Load Button to display the data.",
            overlayNoRowsTemplate: "No Records found",
            rowSelection: 'multiple',
            columnDefs: columnDefs,
            rowData: null,
            filter: true,
            //suppressHorizontalScroll: true,
            alignedGrids: [],
            enableFilter: true

        };


    }

    $scope.GetData = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var qryPara = {
            dateFrom: $filter('date')($scope.apiLog.DateFromDet.dateAD, 'yyyy-MM-dd'),
            dateTo: $filter('date')($scope.apiLog.DateToDet.dateAD, 'yyyy-MM-dd'),
            branchId: $scope.apiLog.BranchId
        };

        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetIRDAPILog",
            dataType: "json",
            data: JSON.stringify(qryPara)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $scope.DataColl = res.data.Data;

                $scope.gridOptions.api.setRowData($scope.DataColl);

            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });
    }

    $scope.Download = function (beData) {
        var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(beData.RequestLog);
        var downloadAnchorNode = document.createElement('a');
        downloadAnchorNode.setAttribute("href", dataStr);
        downloadAnchorNode.setAttribute("download", beData.InvoiceNo + ".json");
        document.body.appendChild(downloadAnchorNode); // required for firefox
        downloadAnchorNode.click();
        downloadAnchorNode.remove();
    }
    $scope.DownloadNew = function (beData) {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var qryPara = {
            tranId: beData.TranId,
            billType: beData.BillType,
        };

        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetJSONForIRD",
            dataType: "json",
            data: JSON.stringify(qryPara)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();
            if (res.data.IsSuccess == true) {

                var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(res.data.Data.JsonStr);
                var downloadAnchorNode = document.createElement('a');
                downloadAnchorNode.setAttribute("href", dataStr);
                downloadAnchorNode.setAttribute("download", beData.InvoiceNo + ".json");
                document.body.appendChild(downloadAnchorNode); // required for firefox
                downloadAnchorNode.click();
                downloadAnchorNode.remove();


            } else
                Swal.fire(res.data.ResponseMSG);


        }, function (reason) {
            alert('Failed' + reason);
        });


    }
    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }

    $scope.onBtExport = function () {
        var params = {
            fileName: 'log.csv',
            sheetName: 'data'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    $scope.PushTran = function (beData) {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var qryPara = {
            tranId: beData.TranId,
            billType: beData.BillType,
        };

        $http({
            method: 'POST',
            url: base_url + "Setup/Security/PushSalesReturnToIRD",
            dataType: "json",
            data: JSON.stringify(qryPara)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);


        }, function (reason) {
            alert('Failed' + reason);
        });


    }

    $scope.PushDataToSAP = function () {

        if (!$scope.newSAP.VoucherTypeId || $scope.newSAP.VoucherId < 0)
            return;

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var qryPara = {
            voucherType: $scope.newSAP.VoucherTypeId,
            BranchId: $scope.newSAP.BranchId
        };

        $http({
            method: 'POST',
            url: base_url + "Setup/Security/PushTranToSAP_H",
            dataType: "json",
            data: JSON.stringify(qryPara)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();
            alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
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