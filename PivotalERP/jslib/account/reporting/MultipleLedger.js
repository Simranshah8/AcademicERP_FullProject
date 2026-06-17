

"use strict";

agGrid.initialiseAgGridWithAngular1(angular);



app.controller("MultipleLedger", function ($scope, $http) {
    $scope.Title = 'MultipleLedger';

    LoadData();

    function Numberformat(val) {
        return $filter('number')(val, 2);
    }

    $scope.DataColl = [];

    function LoadData() {

        var columnDefs = [
            { headerName: "S.No", field: "S.No", filter: 'agNumberColumnFilter', width: 100, pinned: 'left', cellStyle: { 'text-align': 'center' } },
            { headerName: "Date(AD)", field: "Date(AD)", filter: "agTextColumnFilter", width: 100, pinned: 'left' },
            { headerName: "Date(BS)", field: "Date(BS)", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Particular", field: "Particular", filter: 'agNumberColumnFilter', width: 140, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "VoucherType", field: "VoucherType", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "VoucherNo", field: "VoucherNo", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "Ref.No", field: "Ref.No", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "DebitAmt", field: "DebitAmt", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
           
            { headerName: "CreditAmt", field: "CreditAmt", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right', valueFormatter: function (params) { return Numberformat(params.value); } } }
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
            columnDefs: columnDefs,
            rowData: null,
            filter: true,
            suppressHorizontalScroll: true,
            alignedGrids: [],
            enableFilter: true

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);
    }

    $scope.DownloadAsXls = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();
        var dataColl = $scope.GetDataForPrint();
        var paraData = {
            Period: $scope.MultipleLedgerDayBook.DateFromDet.dateBS + " TO " + $scope.MultipleLedgerDayBook.DateToDet.dateBS,
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
                down_file(base_url + "//" + res.data.Data.ResponseId, "MultipleLedger.xlsx");
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire(errormessage);
        });
    }


});