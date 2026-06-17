

"use strict";

agGrid.initialiseAgGridWithAngular1(angular);



app.controller("TrailBalanceLedgerwise", function ($scope, $http, $filter) {
    $scope.Title = 'TrailBalanceLedgerwise';

    LoadData();

    function Numberformat(val) {
        return $filter('number')(val, 2);
    }

    $scope.DataColl = [];

    function LoadData() {

        var columnDefs = [
            { headerName: "S.No", field: "S.No", filter: 'agNumberColumnFilter', width: 100, pinned: 'left', cellStyle: { 'text-align': 'center' } },
            { headerName: "Code", field: "Code", filter: "agTextColumnFilter", width: 100, pinned: 'left' },
            { headerName: "LedgerGroup", field: "LedgerGroup", filter: 'agTextColumnFilter', width: 140 },
            {
                headerName: "Opening", cellStyle: { 'text-align': 'center' },
                children: [
                    { headerName: "DR", field: "DR", filter: 'agNumberColumnFilter', width: 110, valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
                    { headerName: "CR", field: "CR", filter: 'agNumberColumnFilter', cellStyle: { 'text-align': 'center' } }
                ]
            },

            { headerName: "Opening", field: "Opening", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            {
                headerName: "Transaction", cellStyle: { 'text-align': 'center' },
                children: [
                    { headerName: "DR", field: "DR", filter: 'agNumberColumnFilter', width: 110, valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
                    { headerName: "CR", field: "CR", filter: 'agNumberColumnFilter', cellStyle: { 'text-align': 'center' } }
                ]
            },

            {
                headerName: "Closing", cellStyle: { 'text-align': 'center' },
                children: [
                    { headerName: "DR", field: "DR", filter: 'agNumberColumnFilter', width: 110, valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
                    { headerName: "CR", field: "CR", filter: 'agNumberColumnFilter', cellStyle: { 'text-align': 'center' } }
                ]
            },

            { headerName: "Closing", field: "Closing", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "Area", field: "Area", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "Salesman", field: "Salesman", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "MobileNo1", field: "MobileNo1", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "MobileNo2", field: "MobileNo2", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "EmailId", field: "EmailId", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "Address", field: "Address", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right', valueFormatter: function (params) { return Numberformat(params.value); } } }
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
            overlayLoadingTemplate: "Loading..",
            overlayNoRowsTemplate: "No Records found",
            rowSelection: 'multiple',
            columnDefs: columnDefs,
            rowData: null,          
            //suppressHorizontalScroll: true,
            alignedGrids: [],
            enableFilter: true

        };
               
        $scope.eGridDiv = document.querySelector('#datatable');

        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);
    }


});