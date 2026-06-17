

"use strict";

agGrid.initialiseAgGridWithAngular1(angular);



app.controller("ProductRateListCtrl", function ($scope, $http,$filter) {
    $scope.Title = 'ProductRateList';

    LoadData();

    function Numberformat(val) {
        return $filter('number')(val, 2);
    }

    function LoadData() {
        $scope.loadingstatus = 'running';

        var columnDefs = [
            { headerName: "S.No", field: "SNo", filter: 'agNumberColumnFilter', width: 70, pinned: 'left'},
            { headerName: "Name", field: "Name", filter: "agTextColumnFilter", width: 100, pinned: 'left' },
            { headerName: "Alias", field: "Alias", filter: 'agTextColumnFilter', width: 140},
            { headerName: "Code", field: "Code", filter: 'agNumberColumnFilter', width: 140},
            { headerName: "ProductGroup", field: "ProductGroup", filter: 'agNumberColumnFilter', width: 110},
            { headerName: "Category", field: "ProductCategory", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "PartNo", field: "PartNo", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "Remarks", field: "Remarks", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "BaseUnit", field: "BaseUnit", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "OpeningUnit", field: "AlternativeUnit", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "OpeningRate", field: "OpeningRate", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "P-Rate", field: "PRate1", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "S-Rate", field: "SRate1", filter: 'agNumberColumnFilter', width: 110 },
            { headerName: "ClosingBalance", field: "ClosingBalance", filter: 'agNumberColumnFilter', width: 130 }
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

    $scope.GetAllProductRateList = function () {
        if ($scope.loadingstatus != 'stop') {
            alert('Already Running Process')
            return;
        }

        $scope.loadingstatus = 'running';
        showPleaseWait();

        $scope.DataColl = []; //declare an empty array
        $http({
            method: 'GET',
            url: base_url + "Inventory/Reporting/GetAllProductRateList",
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