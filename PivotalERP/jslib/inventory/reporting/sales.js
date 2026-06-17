"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller('pendingSOSummaryCtrl', function ($scope, $http, $filter, companyDet) {
    LoadData();


    function Numberformat(val) {
        return $filter('number')(val, 2);
    }

    $scope.DataColl = [];
    $scope.DateFrom = null;
    $scope.DateTo = null;

    $scope.unitColl = [];
    function LoadData() {

        

        var columnDefs = [
            { headerName: "S.No.", field: "SNo", filter: 'agNumberColumnFilter', width: 70, pinned: 'left', cellStyle: { 'text-align': 'center' } },
            { headerName: "Name", field: "Name", filter: "agTextColumnFilter", width: 210, pinned: 'left' },
            { headerName: "Group", field: "ProductGroup", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Brand", field: "ProductBrand", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Type", field: "ProductType", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Pending Qty.", field: "Qty", filter: 'agNumberColumnFilter', width: 150, cellStyle: { 'text-align': 'right' }, valueFormatter: function (params) { return Numberformat(params.value); } },
            { headerName: "Unit", field: "BaseUnit", filter: 'agTextColumnFilter', width: 140 }
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

    $scope.GetData = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();
        $http({
            method: 'POST',
            url: base_url + "Inventory/Reporting/GetPendingSOSummary?DateFrom=" + $scope.DateFrom + "&DateTo=" + $scope.DateTo,
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