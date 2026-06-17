"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller('crlimitExpiredPartyListCtrl', function ($scope, $http, $filter, companyDet) {
    LoadData();


    function Numberformat(val) {
        return $filter('number')(val, 2);
    }

    $scope.DataColl = [];
    $scope.DateFrom = null;
    $scope.DateTo = null;

    function LoadData() {


        var columnDefs = [
            { headerName: "S.No.", field: "SNo", filter: 'agNumberColumnFilter', width: 70, pinned: 'left', cellStyle: { 'text-align': 'center' } },
            { headerName: "Name", field: "Name", filter: "agTextColumnFilter", width: 210, pinned: 'left' },
            { headerName: "Group", field: "LedgerGroup", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Salesman", field: "Agent", filter: 'agTextColumnFilter', width: 140 }, 
            { headerName: "Area", field: "Area", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Closing Balance", field: "ClosingBalance", filter: 'agNumberColumnFilter', width: 150, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "DuesFromDays", field: "DuesFromDays", filter: 'agNumberColumnFilter', width: 180, cellStyle: { 'text-align': 'center' } },
            { headerName: "Limit Amt.", field: "CreditLimitAmt", filter: 'agNumberColumnFilter', width: 150, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "LimitDays", field: "CreditLimitDays", filter: 'agNumberColumnFilter', width: 180, cellStyle: { 'text-align': 'center' } },
            { headerName: "CrossDays", field: "CrossDays", filter: 'agNumberColumnFilter', width: 180, cellStyle: { 'text-align': 'center' } }
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

    $scope.GetData = function() {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        $http({
            method: 'POST',
            url: base_url + "Inventory/Reporting/GetCRLimitExpiredParty?DateFrom=" + $scope.DateFrom + "&DateTo=" + $scope.DateTo ,
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
            fileName: 'data.csv',
            sheetName: 'data'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }
});