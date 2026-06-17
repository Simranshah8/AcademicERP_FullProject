

"use strict";

agGrid.initialiseAgGridWithAngular1(angular);



app.controller("PartywiseProductListCtrl", function ($scope, $http, $filter) {
    $scope.Title = 'ProductRateList';

    LoadData();

    function Numberformat(val) {
        return $filter('number')(val, 2);
    }

    function LoadData() {
        $scope.loadingstatus = 'running';

        var columnDefs = [
            { headerName: "S.No", field: "SNo", filter: 'agNumberColumnFilter', width: 100, pinned: 'left', cellStyle: { 'text-align': 'center' } },
            { headerName: "Name", field: "PartyName", filter: "agTextColumnFilter", width: 100, pinned: 'left' },
            { headerName: "Address", field: "Address", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Group", field: "GroupName", filter: 'agNumberColumnFilter', width: 140 },
            { headerName: "ProductName", field: "ProductName", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "Alias", field: "ProductAlias", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "Code", field: "ProductCode", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "ProductGroup", field: "ProductGroup", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "Agent", field: "Agent", filter: 'agNumberColumnFilter', width: 130 }
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
        $scope.loadingstatus = "stop";
    }

    $scope.GetAllPartywiseProductList = function () {
        if ($scope.loadingstatus != 'stop') {
            alert('Already Running Process')
            return;
        }

        $scope.loadingstatus = 'running';
        showPleaseWait();

        $scope.DataColl = []; //declare an empty array
        $http({
            method: 'GET',
            url: base_url + "Inventory/Reporting/GetAllPartywiseProductList",
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





});