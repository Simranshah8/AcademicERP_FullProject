"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller('PrintlogController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices) {

    $scope.LoadData = function () {
        $('.select2').select2();

        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.filter = {
            ExamType: 1,
            ExamTypeId: null,
            ExamTypeGroupId: null
        };

        $scope.ExamTypeColl = [
            { id: 1, text: 'Exam type' },
            { id: 2, text: 'Exam type group' }
        ];

        //fetch data for ExamTypeList dropdown
        $scope.ExamTypeList = [];
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/GetAllExamTypeList",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ExamTypeList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        //fetch data for ExamTypeGroupList dropdown
        $scope.ExamTypeGroupList = [];
        $http({
            method: 'POST',
            url: base_url + "Exam/Transaction/GetAllExamTypeGroupList",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ExamTypeGroupList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        // ==== Column Definitions ====
        $scope.ExamTypeColumnsDefs = [
            { headerName: "UserName", field: "UserName", width: 100, cellStyle: { textAlign: 'center' } },
            { headerName: "Name", field: "Name", width: 226 },
            { headerName: "Reg. No.", field: "RegNo", width: 150 },
            { headerName: "Roll No.", field: "RollNo", width: 150 },
            { headerName: "Class", field: "ClassName", width: 150 },
            { headerName: "Section", field: "SectionName", width: 150 },
            { headerName: "Batch", field: "Batch", width: 150 },
            { headerName: "Semester", field: "Semester", width: 150 },
            { headerName: "Class Year", field: "ClassYear", width: 150 },
            { headerName: "Faculty", field: "Faculty", width: 150 },
            { headerName: "Level", field: "Level", width: 150 },
            { headerName: "Public Ip", field: "PublicIP", width: 150 },
            { headerName: "LogDate", field: "LogDate", width: 150 },
            { headerName: "LogMitiTme", field: "LogMitiTme", width: 170 },
        ];


        $scope.ExamTypeOptions = {
            defaultColDef: {
                filter: true,
                resizable: true,
                sortable: true,
                width: 100
            },
            //headerHeight: 31,
            //rowHeight: 30,
            columnDefs: $scope.ExamTypeColumnsDefs,
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
        $scope.eGridDiv = document.querySelector('#ExamTypeData');

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid($scope.eGridDiv, $scope.ExamTypeOptions);

        $scope.ExamTypeOptions.onFirstDataRendered = function () {
            $timeout(function () {
                $compile(angular.element($scope.eGridDiv).contents())($scope);
            });
        };
    };

    $scope.$watch('filter.ExamType', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            $scope.filter.ExamTypeId = null;
            $scope.filter.ExamTypeGroupId = null;
        }
    });

    $scope.GetExamTypeData = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();
        var para = {
            ExamTypeId: $scope.filter.ExamTypeId || 0,
            ExamTypeGroupId: $scope.filter.ExamTypeGroupId || 0
        };
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetExamTypeData",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ExamTypeOptions.api.setRowData(res.data.Data);
            } else {
                alert(res.data.ResponseMSG);
            }
        }, function (reason) {
            $scope.loadingstatus = "stop";
            alert('Failed: ' + reason);
        });
    };

    $scope.onFilterTextBoxChanged = function () {
        $scope.ExamTypeOptions.api.setQuickFilter($scope.search);
    };

    //export csv file
    $scope.onBtExport = function () {
        var params = {
            fileName: 'ExamType.csv',
            sheetName: 'data'
        };
        $scope.ExamTypeOptions.api.exportDataAsCsv(params);
    }

    $scope.onBtExportExcel = function () {
        var params = {
            fileName: 'ExamType.xlsx',
            sheetName: 'data'
        };
        $scope.ExamTypeOptions.api.exportDataAsExcel(params);
    }
});






