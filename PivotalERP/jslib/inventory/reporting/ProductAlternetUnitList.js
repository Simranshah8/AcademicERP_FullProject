

"use strict";

agGrid.initialiseAgGridWithAngular1(angular);



app.controller("ProductAlternetUnitListCtrl", function ($scope, $http, $filter) {
    $scope.Title = 'ProductAlternetUnit';

    LoadData();

    function Numberformat(val) {
        return $filter('number')(val, 2);
    }

    function LoadData() {
        $scope.loadingstatus = 'running';
        var columnDefs = [
            { headerName: "S.No", field: "SNo", filter: 'agNumberColumnFilter', width: 70, pinned: 'left', cellStyle: { 'text-align': 'center' } },
            { headerName: "Name", field: "Name", filter: "agTextColumnFilter", width: 100, pinned: 'left' },
            { headerName: "Alias", field: "Alias", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Code", field: "Code", filter: 'agNumberColumnFilter', width: 140 },
            { headerName: "ProductGroup", field: "ProductGroup", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "BaseValue1", field: "BaseUnitValue1", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "BaseUnit1", field: "BaseUnit1", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "AlternetValue1", field: "AlternetUnitValue1", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "AlternetUnit1", field: "AUName1", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "BaseValue2", field: "BaseUnitValue2", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "BaseUnit2", field: "BaseUnit2", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "AlternetValue2", field: "AlternetUnitValue2", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "AlternetUnit2", field: "AUName2", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "BaseValue3", field: "BaseUnitValue3", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "BaseUnit3", field: "BaseUnit3", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "AlternetValue3", field: "AlternetUnitValue3", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "AlternetUnit3", field: "AUName3", filter: 'agNumberColumnFilter', width: 110 },

            { headerName: "SalesLedgerName", field: "SalesLedgerName", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "SalesLedgerCode", field: "SalesLedgerCode", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "PurchaseLedgerName", field: "PurchaseLedgerName", filter: 'agNumberColumnFilter', width: 110 },


            { headerName: "PurchaseLedgerCode", field: "PurchaseLedgerCode", filter: 'agNumberColumnFilter', width: 130 }
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
            enableFilter: true

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);
        $scope.loadingstatus = "stop";
    }

    $scope.GetAllProductAlternetUnitList = function () {
        if ($scope.loadingstatus != 'stop') {
            alert('Already Running Process')
            return;
        }

        $scope.loadingstatus = 'running';
        showPleaseWait();

        $scope.DataColl = []; //declare an empty array
        $http({
            method: 'GET',
            url: base_url + "Inventory/Reporting/GetAllProductAlternetUnitList",
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