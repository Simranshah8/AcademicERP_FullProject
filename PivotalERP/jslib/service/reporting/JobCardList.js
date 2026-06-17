
"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller("rptJobCardList", function ($scope, $http) {
    $scope.Title = 'JobCard List';

    LoadData();

    $scope.DataColl = [];

    function LoadData() {

        $scope.para = {
            dateFrom: new Date(),
            dateTo:new Date()
        };

        var columnDefs = [
            { headerName: "Date (A.D.)", field: "Date (A.D.)", filter: 'agNumberColumnFilter', width: 100, pinned: 'left', cellStyle: { 'text-align': 'center' } },
            { headerName: "Date (B.S.)", field: "Date (B.S.)", filter: "agTextColumnFilter", width: 100, pinned: 'left' },
            { headerName: "JobNo", field: "Particulars", filter: 'agTextColumnFilter', width: 140 },
            { headerName: "EngineNo", field: "", filter: 'agTextColumnFilter', width: 100 },
            { headerName: "ChSrlNo", field: "", filter: 'agTextColumnFilter', width: 100 },
            { headerName: "Vin.No", field: "", filter: 'agTextColumnFilter', width: 100 },
            { headerName: "Regd.No", field: "VoucherName", filter: 'agNumberColumnFilter', width: 140, cellStyle: { 'text-align': 'right' } },
            { headerName: "TeamLeader", field: "VoucherNo", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' } },
            { headerName: "ServiceEngineer", field: "Ref. No.", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' } },
            { headerName: "Running HR", field: "Debit Amt.", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' } },
            { headerName: "Running KM", field: "Credit Amt.", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' } },
            { headerName: "VehicleType", field: "User", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' } },
            { headerName: "Model", field: "Status", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' } },
            { headerName: "Color", field: "Post By", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' } },
            { headerName: "JobTobeAttended", field: "Approve", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' } },
            { headerName: "Complain", field: "Approved By", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' } },
            { headerName: "Remarks", field: "Approved Remarks", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' } },
            { headerName: "IsClosed", field: "Reject", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' } },
            { headerName: "CustomerName", field: "Rejected By", filter: 'agNumberColumnFilter', width: 110, cellStyle: { 'text-align': 'right' } },
            { headerName: "CustomerAddress", field: "Rejected Remarks", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "CustomerContactNo", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "JobCardFor", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "JobCardType", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "ServiceType", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "AsignDateTime", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "ArrivalDateTime", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "ClosedDateTime", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "ClosedRemarks", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "Site Location", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "MTTC", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "MTTR", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "ART", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "MU", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "Warranty", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "AMC", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "Estimated Time", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "Estimated Cost", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "Date Of Sale", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "Total Amount", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "Labour Charge", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "Parts Amount", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } },
            { headerName: "OutSitePartsAmount", field: "", filter: 'agNumberColumnFilter', width: 130, cellStyle: { 'text-align': 'right' } }
        ];

        $scope.gridOptions =
        {
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
    }

    $scope.getJobCardList = function () {

        $scope.loadingstatus = 'running';
        $scope.DataColl = [];
        showPleaseWait();
        $http({
            method: 'POST',
            url: base_url + "Service/Reporting/GetJobCardList",
            dataType: "json",
            data:JSON.stringify($scope.para)
        }).then(function (res)
        {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.DataColl = res.data.Data;
            } else
                alert(res.data.ResponseMSG);

            $scope.loadingstatus = 'stop';
            hidePleaseWait();
        }, function (reason)
        {
            alert('Failed' + reason);
            $scope.loadingstatus = 'stop';

        });
    }

});