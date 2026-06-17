
app.controller('LastLoginLogController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices) {

    $scope.AcademicConfig = {};
    GlobalServices.getAcademicConfig().then(function (res1) {
        $scope.AcademicConfig = res1.data.Data;

        if ($scope.AcademicConfig.ActiveFaculty == true) {
            $scope.FacultyList = [];
            GlobalServices.getFacultyList().then(function (res) {
                $scope.FacultyList = res.data.Data;
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        } else {
            $scope.gridOptions.columnApi.setColumnsVisible(["Faculty"], false);
        }


        if ($scope.AcademicConfig.ActiveLevel == true) {
            $scope.LevelList = [];
            GlobalServices.getClassLevelList().then(function (res) {
                $scope.LevelList = res.data.Data;
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        } else {
            $scope.gridOptions.columnApi.setColumnsVisible(["Level"], false);
        }


        if ($scope.AcademicConfig.ActiveSemester == true) {
            $scope.SemesterList = [];
            GlobalServices.getSemesterList().then(function (res) {
                $scope.SemesterList = res.data.Data;
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        } else {
            $scope.gridOptions.columnApi.setColumnsVisible(["Semester"], false);
        }

        if ($scope.AcademicConfig.ActiveBatch == true) {
            $scope.BatchList = [];
            GlobalServices.getBatchList().then(function (res) {
                $scope.BatchList = res.data.Data;
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        } else {
            $scope.gridOptions.columnApi.setColumnsVisible(["Batch"], false);
        }

        if ($scope.AcademicConfig.ActiveClassYear == true) {

            $scope.ClassYearList = [];
            $scope.SelectedClassClassYearList = [];
            GlobalServices.getClassYearList().then(function (res) {
                $scope.ClassYearList = res.data.Data;
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        } else {
            $scope.gridOptions.columnApi.setColumnsVisible(["ClassYear"], false);
        }

    }, function (reason) {
        Swal.fire('Failed' + reason);
    });

    getterAndSetter();
   
   
    function getterAndSetter() {
        $scope.gridColumnDef = [
           
            { headerName: "UserName", field: "UserName", filter: 'agTextColumnFilter', width: 120, pinned: 'left' },
            { headerName: "Name", field: "Name", filter: 'agTextColumnFilter', width: 220, pinned: 'left' },
            { headerName: "Adm.No", field: "Code", filter: 'agTextColumnFilter', width: 110 },
            { headerName: "Roll.No", field: "RollNo", filter: 'agTextColumnFilter', width: 110 },
            { headerName: "Class", field: "ClassName", filter: 'agTextColumnFilter', width: 120 },
            { headerName: "Section", field: "SectionName", filter: 'agTextColumnFilter', width: 120 },
            { headerName: "Batch", field: "Batch", filter: 'agTextColumnFilter', width: 110 },
            { headerName: "Semester", field: "Semester", filter: 'agTextColumnFilter', width: 110 },
            { headerName: "ClassYear", field: "ClassYear", filter: 'agTextColumnFilter', width: 110 },
            { headerName: "Faculty", field: "Faculty", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Level", field: "Level", filter: 'agTextColumnFilter', width: 110 },
           
            { headerName: "Public IP", field: "PublicIP", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "PC Name", field: "PCName", filter: 'agTextColumnFilter', width: 180 },
            { headerName: "App Version", field: "AppVersion", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "LogDateTime", field: "LogDateTime", filter: 'agTextColumnFilter', width: 160 },
            { headerName: "Log Miti", field: "LogMitiTime", filter: 'agTextColumnFilter', width: 170 },
            { headerName: "Before Day", field: "BeforeDay", filter: 'agTextColumnFilter', width: 120, pinned: 'right' },
        ];

        $scope.gridOptions = {
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,

                // set every column width
                width: 90
            },
            headerHeight: 31,
            rowHeight: 30,
            columnDefs: $scope.gridColumnDef,
            enableColResize: true,
            rowData: null,
            filter: true,
            enableFilter: true,
            enableSorting: true,
            overlayLoadingTemplate: "Please Click the Load Bottom to display the data",
            rowSelection: 'multiple',
            suppressHorizontalScroll: false,
            alignedGrids: [],
            enableFilter: true

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions);

        var columnDefs2 = [
           
            { headerName: "UserName", field: "UserName", filter: 'agTextColumnFilter', width: 120, pinned: 'left' },
            { headerName: "Name", field: "Name", filter: 'agTextColumnFilter', width: 220, pinned: 'left' },
            { headerName: "Code", field: "Code", filter: 'agTextColumnFilter', width: 110 },
           
            { headerName: "Department", field: "Department", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Designation", field: "Designation", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "Public IP", field: "PublicIP", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "PC Name", field: "PCName", filter: 'agTextColumnFilter', width: 180 },
            { headerName: "App Version", field: "AppVersion", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "LogDateTime", field: "LogDateTime", filter: 'agTextColumnFilter', width: 160 },
            { headerName: "Log Miti", field: "LogMitiTime", filter: 'agTextColumnFilter', width: 170 },
            { headerName: "Before Day", field: "BeforeDay", filter: 'agTextColumnFilter', width: 130},

        ];

        $scope.gridOptions2 = {
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
            overlayLoadingTemplate: "Please Click the Load Button to display the data",
            overlayNoRowsTemplate: "No Records found",
            rowSelection: 'multiple',
            columnDefs: columnDefs2,
            rowData: null,
            filter: true,
            suppressHorizontalScroll: false,
            alignedGrids: [],
            enableFilter: true

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable2');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions2);

        var columnDefs3 = [
          
            { headerName: "UserName", field: "UserName", filter: 'agTextColumnFilter', width: 120 },
            { headerName: "Name", field: "Name", filter: 'agTextColumnFilter', width: 220 },
          
            { headerName: "Designation", field: "Designation", filter: 'agTextColumnFilter', width: 140 },
          
            { headerName: "Public IP", field: "PublicIP", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "PC Name", field: "PCName", filter: 'agTextColumnFilter', width: 180 },
            { headerName: "App Version", field: "AppVersion", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "LogDateTime", field: "LogDateTime", filter: 'agTextColumnFilter', width: 160 },
            { headerName: "Log Miti", field: "LogMitiTime", filter: 'agTextColumnFilter', width: 170 },
            { headerName: "Before Day", field: "BeforeDay", filter: 'agTextColumnFilter', width: 130},

        ];
        $scope.gridOptions3 = {
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
            overlayLoadingTemplate: "Please Click the Load Button to display the data",
            overlayNoRowsTemplate: "No Records found",
            rowSelection: 'multiple',
            columnDefs: columnDefs3,
            rowData: null,
            filter: true,
            suppressHorizontalScroll: false,
            alignedGrids: [],
            enableFilter: true

        };

        // lookup the container we want the Grid to use
        $scope.eGridDiv = document.querySelector('#datatable3');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.gridOptions3);     
    }

    $scope.GetData = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();

        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllLastLoginLog",
            dataType: "json",
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                // Reset arrays before assigning new data
                $scope.DataColl = [];
                $scope.DataColl2 = [];
                $scope.DataColl3 = [];

                // Iterate through the data and assign based on UserType
                res.data.Data.forEach(function (item) {
                    if (item.UserType === 'Student') {
                        $scope.DataColl.push(item);
                    } else if (item.UserType === 'Employee') {
                        $scope.DataColl2.push(item);
                    } else if (item.UserType === 'System User') {
                        $scope.DataColl3.push(item);
                    }
                });

                // Set row data in grid
                $scope.gridOptions.api.setRowData($scope.DataColl);
                $scope.gridOptions2.api.setRowData($scope.DataColl2);
                $scope.gridOptions3.api.setRowData($scope.DataColl3);
            } else {
                alert(res.data.ResponseMSG);
            }
        }, function (reason) {
            alert('Failed: ' + reason);
        });
    };

    $scope.onFilterTextBoxChanged = function () {
        $scope.gridOptions.api.setQuickFilter($scope.search);
    }


    $scope.onFilterTextBoxChanged2 = function () {
        $scope.gridOptions2.api.setQuickFilter($scope.search2);
    }


    $scope.onFilterTextBoxChanged3 = function () {
        $scope.gridOptions3.api.setQuickFilter($scope.search3);
    }

    $scope.onBtExport = function () {
        var params = {
            fileName: 'StudentUsers.csv',
            sheetName: 'data'
        };

        $scope.gridOptions.api.exportDataAsCsv(params);
    }

    $scope.onBtExport2 = function () {
        var params = {
            fileName: 'EmployeeUsers.csv',
            sheetName: 'data'
        };

        $scope.gridOptions2.api.exportDataAsCsv(params);
    }

    $scope.onBtExport3 = function () {
        var params = {
            fileName: 'SystemUsers.csv',
            sheetName: 'data'
        };

        $scope.gridOptions3.api.exportDataAsCsv(params);
    }
});