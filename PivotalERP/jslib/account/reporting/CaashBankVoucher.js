

"use strict";

agGrid.initialiseAgGridWithAngular1(angular);



app.controller("CashBankVoucher", function ($scope, $http) {
    $scope.Title = 'CashBankVoucher';

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
            { headerName: "CostCenter", field: "CostCenter", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "ChequeNo", field: "ChequeNo", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "ChequeDate", field: "ChequeDate", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "ChequeRemarks", field: "ChequeRemarks", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "Amount", field: "Amount", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "VoucherType", field: "VoucherType", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "VoucherNo", field: "VoucherNo", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "Ref.No", field: "Ref.No", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "Receipt", field: "Receipt", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "Payment", field: "Payment", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "Balance", field: "Balance", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "User", field: "User", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "IsLocked", field: "IsLocked", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "Branch", field: "Branch", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },

            { headerName: "LogDateTime", field: "LogDateTime", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right', valueFormatter: function (params) { return Numberformat(params.value); } } }
        ];




        $scope.gridOptions = {

            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                width: 100,

            },
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


});