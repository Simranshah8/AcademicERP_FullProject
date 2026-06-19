app.controller('StudentHealthController', function ($scope, $http, $timeout, $filter, GlobalServices) {

    $scope.gridOptions1 = {
        enableHorizontalScrollbar: 1,
        enableVerticalScrollbar: 1,
        enableFiltering: true,
        enableSorting: true,
        enableGridMenu: true,
        showGridFooter: true,
        showColumnFooter: false,
        exporterCsvFilename: 'LostBookReport.csv',
        exporterPdfOrientation: 'landscape',
        exporterPdfPageSize: 'A3',
        exporterPdfMaxGridWidth: 2500,
        exporterPdfDefaultStyle: { fontSize: 8 },
        exporterPdfTableHeaderStyle: { fontSize: 9, bold: true },
        exporterPdfHeader: { text: "Lost Book Report\nInstitution Name", style: 'headerStyle' },
        exporterPdfFooter: function (currentPage, pageCount) {
            return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
        },
        exporterPdfCustomFormatter: function (docDef) {
            docDef.styles.headerStyle = { fontSize: 14, bold: true };
            docDef.styles.footerStyle = { fontSize: 8 };
            return docDef;
        },

        columnDefs: [
            { name: 'SNo', displayName: 'S.No.', width: 90, enableHiding: false },
            {
                name: 'ObservedOn',
                displayName: 'Observed On',
                width: 150,
                enableHiding: false,
                cellTemplate: '<div class="ui-grid-cell-contents">{{row.entity.ObservedOn}} {{row.entity.ObservedTime | timeFormat}}</div>'
            },

            { name: 'AdmissionNo', displayName: 'Admission No', width: 130, enableHiding: false },
            { name: 'Name', displayName: 'Name', width: 160, enableHiding: false},
            { name: 'RollNo', displayName: 'Roll No', width: 100, enableHiding: false },
            { name: 'Class', displayName: 'Class', width: 150, enableHiding: false },
            { name: 'Section', displayName: 'Section', width: 150, enableHiding: false  },
            { name: 'Batch', displayName: 'Batch', width: 150, enableHiding: true  },
            { name: 'Semester', displayName: 'Semester', width: 150, enableHiding: true  },
            { name: 'ClassYear', displayName: 'Class Year', width: 150, enableHiding: true  },
            { name: 'ObservedAt', displayName: 'Observed At', width: 150, enableHiding: false  },
            { name: 'HealthIssue', displayName: 'Health Issue', width: 150, enableHiding: false },
            { name: 'IsAdmitted', displayName: 'Is Admitted?', width: 150, enableHiding: false },
            { name: 'AdmittedAt', displayName: 'Admitted At?', width: 150, enableHiding: false },
            { name: 'AdmittedDate', displayName: 'Admitted Date', width: 150, enableHiding: false },
            { name: 'MedicineGiven', displayName: 'Medicine Given?', width: 150, enableHiding: false },
            { name: 'Age', displayName: 'Age', width: 90, enableHiding: false},
            { name: 'PrescribedBy', displayName: 'Prescribed By', width: 150, enableHiding: false }
            ],

        data: [] 
    };

    $scope.gridOptions2 = {
        enableHorizontalScrollbar: 1,
        enableVerticalScrollbar: 1,
        enableFiltering: true,
        enableSorting: true,
        enableGridMenu: true,
        showGridFooter: true,
        showColumnFooter: false,
        exporterCsvFilename: 'LostBookReport.csv',
        exporterPdfOrientation: 'landscape',
        exporterPdfPageSize: 'A3',
        exporterPdfMaxGridWidth: 2500,
        exporterPdfDefaultStyle: { fontSize: 8 },
        exporterPdfTableHeaderStyle: { fontSize: 9, bold: true },
        exporterPdfHeader: { text: "Lost Book Report\nInstitution Name", style: 'headerStyle' },
        exporterPdfFooter: function (currentPage, pageCount) {
            return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
        },
        exporterPdfCustomFormatter: function (docDef) {
            docDef.styles.headerStyle = { fontSize: 14, bold: true };
            docDef.styles.footerStyle = { fontSize: 8 };
            return docDef;
        },

        columnDefs: [
            { name: 'SNo', displayName: 'S.No.', width: 90, enableHiding: false },
            { name: 'ObservedOn', displayName: 'Observed On', width: 150, enableHiding: false },
            { name: 'AdmissionNo', displayName: 'Admission No', width: 130, enableHiding: false },
            { name: 'Name', displayName: 'Name', width: 160, enableHiding: false},
            { name: 'RollNo', displayName: 'Roll No', width: 100, enableHiding: false },
            { name: 'Class', displayName: 'Class', width: 150, enableHiding: false },
            { name: 'Section', displayName: 'Section', width: 150, enableHiding: false  },
            { name: 'Batch', displayName: 'Batch', width: 150, enableHiding: true  },
            { name: 'Semester', displayName: 'Semester', width: 150, enableHiding: true  },
            { name: 'ClassYear', displayName: 'Class Year', width: 150, enableHiding: true  },
            { name: 'HealthIssue', displayName: 'Health Issue', width: 150, enableHiding: false },
            { name: 'Details', displayName: 'Details', width: 150, enableHiding: false },
            { name: 'MedicineGiven', displayName: 'Medicine Given?', width: 150, enableHiding: false },
            { name: 'Age', displayName: 'Age', width: 90, enableHiding: false}
            ],

        data: [] 
    };

	$scope.LoadData = function () {
        $('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			StudentReport: 1,
		};

		$scope.searchData = {
			StudentReport: '',
		};

		$scope.perPage = {
			StudentReport: GlobalServices.getPerPageRow(),
		};
		

        $scope.newDet = {
            DateFrom_TMP: new Date(new Date().setDate(new Date().getDate() - 7)),
            DateTo_TMP: new Date(),
            ReportType: 1,

        };
	
        $scope.ReportTypeColl = [
            {id: 1, text: "Health Issue"},
            {id: 2, text: "Past Medical History"}
        ];


        $scope.AcademicConfig = {};
        GlobalServices.getAcademicConfig().then(function (res1) {
            $scope.AcademicConfig = res1.data.Data;
            

            if ($scope.AcademicConfig.ActiveFaculty == false) {

                findInd = $scope.gridOptions1.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' });
                if (findInd != -1)
                    $scope.gridOptions1.columnDefs.splice(findInd, 1);

                findInd1 = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' });
                if (findInd1 != -1)
                    $scope.gridOptions2.columnDefs.splice(findInd1, 1);
            }

            if ($scope.AcademicConfig.ActiveLevel == false) {

                findInd = $scope.gridOptions1.columnDefs.findIndex(function (obj) { return obj.name == 'Level' });
                if (findInd != -1)
                    $scope.gridOptions1.columnDefs.splice(findInd, 1);

                findInd1 = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Level' });
                if (findInd1 != -1)
                    $scope.gridOptions2.columnDefs.splice(findInd1, 1);

            }

            if ($scope.AcademicConfig.ActiveSemester == false) {


                findInd = $scope.gridOptions1.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' });
                if (findInd != -1)
                    $scope.gridOptions1.columnDefs.splice(findInd, 1);

                findInd1 = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' });
                if (findInd1 != -1)
                    $scope.gridOptions2.columnDefs.splice(findInd1, 1);

            } 

            if ($scope.AcademicConfig.ActiveBatch == false) {

                findInd = $scope.gridOptions1.columnDefs.findIndex(function (obj) { return obj.name == 'Batch' });
                if (findInd != -1)
                    $scope.gridOptions1.columnDefs.splice(findInd, 1);

                findInd1 = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Batch' });
                if (findInd1 != -1)
                    $scope.gridOptions2.columnDefs.splice(findInd1, 1);

            } 

            if ($scope.AcademicConfig.ActiveClassYear == false) {

                findInd = $scope.gridOptions1.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' });
                if (findInd != -1)
                    $scope.gridOptions1.columnDefs.splice(findInd, 1);

                findInd1 = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' });
                if (findInd1 != -1)
                    $scope.gridOptions2.columnDefs.splice(findInd1, 1);

            }          

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


    }

    $scope.ClearDetails = function () {
        $scope.newDet = {
            DateFrom_TMP: new Date(new Date().setDate(new Date().getDate() - 7)),
            DateTo_TMP: new Date(),
            ReportType: 1,
        };

    };


    $scope.GetStudentHealthReport = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var para = {
            dateFrom: ($scope.newDet.DateFromDet ? $scope.newDet.DateFromDet.dateAD : null),
            dateTo: ($scope.newDet.DateToDet ? $scope.newDet.DateToDet.dateAD : null),
        };
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Report/GetStudentHealthReport",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.IsSuccess && res.data.Data) {

               
                $scope.gridOptions1.data = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG || "No records found.");
                $scope.gridOptions1.data = [];
            }
        }, function (reason) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire("Failed to load data. Please try again.");
            $scope.gridOptions1.data = [];
        });
    };

    $scope.GetStudentHealthPastHistory = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var para = {
            dateFrom: ($scope.newDet.DateFromDet ? $scope.newDet.DateFromDet.dateAD : null),
            dateTo: ($scope.newDet.DateToDet ? $scope.newDet.DateToDet.dateAD : null),
        };
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Report/GetStudentHealthPastHistory",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.IsSuccess && res.data.Data) {

                $scope.gridOptions2.data = res.data.Data;
              
            } else {
                Swal.fire(res.data.ResponseMSG || "No records found.");
                $scope.gridOptions1.data = [];
            }
        }, function (reason) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire("Failed to load data. Please try again.");
            $scope.gridOptions2.data = [];
        });
    };


    $scope.validateDate = function (obj, startField, endField, startLabel, endLabel) {
        var res = GlobalServices.validateDate(obj, startField, endField, startLabel, endLabel);
        if (res.IsSuccess == false) {
            Swal.fire({
                icon: 'warning',
                text: res.Message,
                confirmButtonText: 'OK'
            }).then(function () {
                obj.DateFrom_TMP = new Date();
                obj.DateTo_TMP = new Date();
                $scope.$applyAsync();
            });
        }
    };

});
