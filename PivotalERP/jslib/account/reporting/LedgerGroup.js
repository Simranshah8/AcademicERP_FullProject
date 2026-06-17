

"use strict";

agGrid.initialiseAgGridWithAngular1(angular);



app.controller("LedgerGroup", function ($scope, $http, $filter) {
    $scope.Title = 'LedgerGroup';

    LoadData();

    function Numberformat(val) {
        return $filter('number')(val, 2);
    }

    function LoadData() {
        $scope.loadingstatus = 'running';
        $scope.columnDefs = [
            { headerName: "S.No", field: "LedgerGroupId", filter: 'agNumberColumnFilter', width: 90, pinned: 'left', dataType: 'Number', cellStyle: { 'text-align': 'center' } },
            { headerName: "Name", field: "Name", filter: "agTextColumnFilter", width: 200, pinned: 'left', dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "Code", field: "Code", filter: "agTextColumnFilter", width: 200, cellStyle: { 'text-align': 'left' } },
            { headerName: "BaseGroup", field: "ParentGroupName", filter: "agTextColumnFilter", width: 200, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "NatureOfGroup", field: "NatureOfGroup", filter: "agTextColumnFilter", width: 200, dataType: 'Text', cellStyle: { 'text-align': 'left' } },
            { headerName: "IsActive", field: "IsActive", filter: "agNumberColumnFilter", width: 200, pinned: 'center', dataType: 'Text', cellStyle: { 'text-align': 'left' } }
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
            enableFilter: true

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);
        $scope.loadingstatus = "stop";
    }


    $scope.GetAllLedgerGroup = function () {
        if ($scope.loadingstatus != 'stop') {
            alert('Already Running Process')
            return;
        }

        $scope.loadingstatus = 'running';
        showPleaseWait();

        $scope.DataColl = []; //declare an empty array
        $http({
            method: 'GET',
            url: base_url + "Account/Reporting/GetAllLedgerGroup",
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
            /*    formData.append("paraData", angular.toJson(paraData));*/
                formData.append("RptPath", "");
                return formData;
            },
            data: { jsonData: dataColl }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();
            if (res.data.IsSuccess && res.data.Data) {
                down_file(base_url + "//" + res.data.Data.ResponseId, "LedgerGroup.xlsx");
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire(errormessage);
        });
    }



});