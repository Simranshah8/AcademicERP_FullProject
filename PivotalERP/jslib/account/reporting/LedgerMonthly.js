

"use strict";

agGrid.initialiseAgGridWithAngular1(angular);



app.controller("LedgerMonthly", function ($scope, $http) {
    $scope.Title = 'LedgerMonthly';

    LoadData();

    function Numberformat(val) {
        return $filter('number')(val, 2);
    }

    $scope.DataColl = [];

    function LoadData() {

        var columnDefs = [
            { headerName: "S.No", field: "S.No", filter: 'agNumberColumnFilter', width: 100, pinned: 'left', cellStyle: { 'text-align': 'center' } },
            { headerName: "Particular", field: "Particular", filter: "agTextColumnFilter", width: 100, pinned: 'left' },
            { headerName: "OpeningAmt", field: "OpeningAmt", filter: 'agTextColumnFilter', width: 140 },
            {
                headerName: "Transaction", cellStyle: { 'text-align': 'center' },
                children: [
                    { headerName: "DR", field: "DR", filter: 'agNumberColumnFilter', width: 110, valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
                    { headerName: "CR", field: "CR", filter: 'agNumberColumnFilter', cellStyle: { 'text-align': 'center' } }
                ]
            },    


            { headerName: "ClosingAmt", field: "ClosingAmt", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },

            { headerName: "CurrentClosing", field: "CurrentClosing", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right', valueFormatter: function (params) { return Numberformat(params.value); } } }
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