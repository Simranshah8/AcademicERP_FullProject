app.controller('StudentBiometricController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {
	$scope.Title = 'Student Biometric Attendance';
	$('.select2').select2();

	getterAndSetter();

	$scope.DateFormatAD = function (date) {

		if (date) {
			date = new Date(date);
			return $filter('date')(date, 'yyyy-MM-dd');
		}

		return '';
	};


	$rootScope.ConfigFunction = function () {
		var keyColl = $translate.getTranslationTable();

		var Labels = {
			RegdNo: keyColl['REGDNO_LNG']
		};
		if ($rootScope.LANG == 'in') {

			$scope.gridApi.grid.getColumn('RegdNo').colDef.displayName = Labels.RegdNo;
			$scope.gridApi.grid.getColumn('RegdNo').displayName = Labels.RegdNo;
			var findInd = -1;

			$scope.gridApi1.grid.getColumn('RegdNo').colDef.displayName = Labels.RegdNo;
			$scope.gridApi1.grid.getColumn('RegdNo').displayName = Labels.RegdNo;
			var findInd1 = -1;

			//$scope.gridApi2.grid.getColumn('RegdNo').colDef.displayName = Labels.RegdNo;
			//$scope.gridApi2.grid.getColumn('RegdNo').displayName = Labels.RegdNo;

			//$scope.gridApi3.grid.getColumn('EffectiveDate_AD').colDef.displayName = 'Date';
			//$scope.gridApi3.grid.getColumn('EffectiveDate_AD').displayName = 'Date';

			//findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'EffectiveDate_BS' });
			//if (findInd != -1)
			//	$scope.gridOptions3.columnDefs.splice(findInd, 1); 



		}

		$scope.gridApi.grid.refresh();
		$scope.gridApi1.grid.refresh();
		//$scope.gridApi2.grid.refresh();
		//$scope.gridApi3.grid.refresh();
		//$scope.gridApi4.grid.refresh(); 

		$scope.LoadData();

		//Added By Suresh on Magh 18 for batch semester and class year starts
		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;


			if ($scope.AcademicConfig.ActiveFaculty == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' });
				findInd1 = $scope.gridOptions1.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' });
				findInd2 = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' }); //Pending Attendance
				if (findInd != -1) {
					$scope.gridOptions.columnDefs.splice(findInd, 1);
				}
				if (findInd1 != -1) {
					$scope.gridOptions1.columnDefs.splice(findInd1, 1);
				}
				//Pending Attendance
				if (findInd2 != -1) {
					$scope.gridOptions2.columnDefs.splice(findInd2, 1);
				}
			}

			if ($scope.AcademicConfig.ActiveLevel == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Level' });
				findInd1 = $scope.gridOptions1.columnDefs.findIndex(function (obj) { return obj.name == 'Level' });
				findInd2 = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Level' }); //Pending Attendance
				if (findInd != -1) {
					$scope.gridOptions.columnDefs.splice(findInd, 1);
				}
				if (findInd1 != -1) {
					$scope.gridOptions1.columnDefs.splice(findInd1, 1);
				}
				//Pending Attendance
				if (findInd2 != -1) {
					$scope.gridOptions2.columnDefs.splice(findInd2, 1);
				}
			}

			if ($scope.AcademicConfig.ActiveSemester == false) {


				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' });
				findInd1 = $scope.gridOptions1.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' });
				findInd2 = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' }); //Pending Attendance
				if (findInd != -1) {
					$scope.gridOptions.columnDefs.splice(findInd, 1);
				}
				if (findInd1 != -1) {
					$scope.gridOptions1.columnDefs.splice(findInd1, 1);
				}
				//Pending Attendance
				if (findInd2 != -1) {
					$scope.gridOptions2.columnDefs.splice(findInd2, 1);
				}

			} else {
				$scope.SelectedClassSemesterList = [];
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveBatch == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Batch' });
				findInd1 = $scope.gridOptions1.columnDefs.findIndex(function (obj) { return obj.name == 'Batch' });
				findInd2 = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Batch' }); //Pending Attendance
				if (findInd != -1) {
					$scope.gridOptions.columnDefs.splice(findInd, 1);
				}
				if (findInd1 != -1) {
					$scope.gridOptions1.columnDefs.splice(findInd1, 1);
				}
				//Pending Attendance
				if (findInd2 != -1) {
					$scope.gridOptions2.columnDefs.splice(findInd2, 1);
				}

			} else {
				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

			if ($scope.AcademicConfig.ActiveClassYear == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' });
				findInd1 = $scope.gridOptions1.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' });
				findInd2 = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' }); //Pending Attendance
				if (findInd != -1) {
					$scope.gridOptions.columnDefs.splice(findInd, 1);
				}
				if (findInd1 != -1) {
					$scope.gridOptions1.columnDefs.splice(findInd1, 1);
				}
				//Pending Attendance
				if (findInd2 != -1) {
					$scope.gridOptions2.columnDefs.splice(findInd2, 1);
				}
			}
			else {
				$scope.ClassYearList = [];
				$scope.SelectedClassClassYearList = [];
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

			//$scope.gridApi.grid.refresh();
			//$scope.gridApi2.grid.refresh();
			//$scope.gridApi3.grid.refresh();
			//$scope.gridApi4.grid.refresh();
			//$scope.gridApi5.grid.refresh();


		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		//Ends

	};
	$rootScope.ChangeLanguage();

	function getterAndSetter() {
		$scope.gridOptions = [];
		$scope.gridOptions1 = [];
		//$scope.gridOptions2 = [];
		//$scope.gridOptions3 = [];
		//$scope.gridOptions4 = [];

		//Student Manul Attendance/Daily
		$scope.gridOptions = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,

			columnDefs: [

				{ name: "SNo", displayName: "S.No.", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "RegdNo", displayName: "Regd No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
				//Added By Suresh on 18 Magh starts
				{
					name: "ClassName",
					displayName: "Class/Sec",
					minWidth: 180,
					headerCellClass: 'headerAligment',
					cellTemplate:
						'<div style="padding-left: 10px;">{{row.entity.ClassName}}{{row.entity.SectionName ? " - " + row.entity.SectionName : ""}}</div>',
				},

				{ name: "Batch", displayName: "Batch", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "Class Year", minWidth: 140, headerCellClass: 'headerAligment' },
				//Ends
				{ name: "Attendance", displayName: "Attendance", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LateMin", displayName: "Late in min.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Remarks", displayName: "Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "Contact No", minWidth: 140, headerCellClass: 'headerAligment' },

			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'enqSummary.csv',
			exporterPdfDefaultStyle: { fontSize: 9 },
			exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
			exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
				docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
				return docDefinition;
			},
			exporterPdfOrientation: 'portrait',
			exporterPdfPageSize: 'LETTER',
			exporterPdfMaxGridWidth: 500,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'enqSummary.xlsx',
			exporterExcelSheetName: 'enqSummary',
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
			}
		};

		//Daily Biometric Attendance starts
		$scope.gridOptions1 = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,
			columnDefs: [
				{ name: "SNo", displayName: "S.No.", minWidth: 90, headerCellClass: 'headerAligment', type: 'number' },
				{ name: "RegdNo", displayName: "Regd No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment', type: 'number' },
				{ name: "EnrollNo", displayName: "Enroll No", minWidth: 140, headerCellClass: 'headerAligment', type: 'number' },
				{
					name: "ClassName",
					displayName: "Class/Sec",
					minWidth: 180,
					headerCellClass: 'headerAligment',
					cellTemplate:
						'<div style="padding-left: 10px;">{{row.entity.ClassName}}{{row.entity.SectionName ? " - " + row.entity.SectionName : ""}}</div>',
				},
				//Added By Suresh on 18 Magh starts
				{ name: "Batch", displayName: "Batch", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "Class Year", minWidth: 140, headerCellClass: 'headerAligment' },
				//Ends
				{ name: "Attendance", displayName: "Attendance", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "InTime", displayName: "In Time(HH:MM)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "OutTime", displayName: "Out Time(HH:MM)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LateInStr", displayName: "Late In(HH:MM)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BeforeOutStr", displayName: "Late Out(HH:MM)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "WorkingHR", displayName: "Working Hours", minWidth: 140, headerCellClass: 'headerAligment' },
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'enqSummary.csv',
			exporterPdfDefaultStyle: { fontSize: 9 },
			exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
			exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
				docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
				return docDefinition;
			},
			exporterPdfOrientation: 'portrait',
			exporterPdfPageSize: 'LETTER',
			exporterPdfMaxGridWidth: 500,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'enqSummary.xlsx',
			exporterExcelSheetName: 'enqSummary',
			onRegisterApi: function (gridApi) {
				$scope.gridApi1 = gridApi;
			}
		};


		//Added by simran
		$scope.gridOptions2 = [];  //Pending Attendance
		$scope.gridOptions3 = [];   //Student Type wise

		//pending Attendance
		$scope.gridOptions2 = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,
			columnDefs: [
				{ name: "SNo", displayName: "S.N.", width: 60, headerCellClass: 'headerAligment', cellTemplate: '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>' },
				{ name: "ForMiti", displayName: "Date", width: 100, headerCellClass: 'headerAligment' },
				{
					name: "ClassName",
					displayName: "Class/Sec",
					width: 280,
					headerCellClass: 'headerAligment',
					cellTemplate:
						'<div style="padding-left: 10px;">{{row.entity.ClassName}}{{row.entity.Section ? " - " + row.entity.Section : ""}}</div>',
				},
				{ name: "Batch", displayName: "Batch", width: 70, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", width: 100, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "Class Year", width: 100, headerCellClass: 'headerAligment' },
				/*	{ name: "SubjectName", displayName: "Subject Name", minWidth: 100, headerCellClass: 'headerAligment' },*/
				{ name: "ClassTeacher", displayName: "Class Teacher", minWidth: 160, headerCellClass: 'headerAligment' },
				/*{ name: "SubjectTeacher", displayName: "Subject Teacher", minWidth: 100, headerCellClass: 'headerAligment' },*/
				{ name: "Coordinator", displayName: "Coordinator", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "HOD", displayName: "HOD", minWidth: 160, headerCellClass: 'headerAligment' },

			],
			exporterCsvFilename: 'PendingAttendance.csv',
			exporterPdfDefaultStyle: { fontSize: 9 },
			exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
			exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
				docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
				return docDefinition;
			},
			exporterPdfOrientation: 'portrait',
			exporterPdfPageSize: 'LETTER',
			exporterPdfMaxGridWidth: 500,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'PendingAttendance.xlsx',
			exporterExcelSheetName: 'PendingAttendance',

			onRegisterApi: function (gridApi) {
				$scope.gridApi2 = gridApi;

			}
		}
		//studeent Type Wise
		$scope.gridOptions3 = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,
			columnDefs: [
				{ name: "SNo", displayName: "S.N.", minWidth: 60, headerCellClass: 'headerAligment', cellTemplate: '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>' },
				{ name: "RegdNo", displayName: "Regd No.", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No.", minWidth: 100, headerCellClass: 'headerAligment' },
				{
					name: "ClassName",
					displayName: "Class/Sec",
					minWidth: 180,
					headerCellClass: 'headerAligment',
					cellTemplate:
						'<div style="padding-left: 10px;">{{row.entity.ClassName}}{{row.entity.SectionName ? " - " + row.entity.SectionName : ""}}</div>',
				},
				{ name: "StudentType", displayName: "Student Type", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Attendance", displayName: "Attendance", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "LateMin", displayName: "Late in min.", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Remarks", displayName: "Remarks.", minWidth: 100, headerCellClass: 'headerAligment' },

			],
			exporterCsvFilename: 'StudentType.csv',
			exporterPdfDefaultStyle: { fontSize: 9 },
			exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
			exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
				docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
				return docDefinition;
			},
			exporterPdfOrientation: 'portrait',
			exporterPdfPageSize: 'LETTER',
			exporterPdfMaxGridWidth: 500,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'PendingAtt.xlsx',
			exporterExcelSheetName: 'PendingAtt',
			onRegisterApi: function (gridApi) {
				$scope.gridApi3 = gridApi;

			}
		}
		//End

		////Attendance Summary Starts
		//$scope.gridOptions2 = {
		//	showGridFooter: true,
		//	showColumnFooter: false,
		//	useExternalPagination: false,
		//	useExternalSorting: false,
		//	enableFiltering: true,
		//	enableSorting: true,
		//	enableRowSelection: true,
		//	enableSelectAll: true,
		//	enableGridMenu: true,
		//	columnDefs: [
		//		{ name: "SNo", displayName: "S.No.", minWidth: 90, headerCellClass: 'headerAligment', type: 'number' },
		//		{ name: "RegdNo", displayName: "Regd No.", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
		//		{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment', type: 'number' },
		//		{ name: "EnrollNo", displayName: "Enroll No", minWidth: 140, headerCellClass: 'headerAligment', type: 'number' },
		//		{ name: "ClassSec", displayName: "Class/Sec", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Day1", displayName: "Day1", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Day2", displayName: "Day2", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Day3", displayName: "Day3", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "TotalDays", displayName: "Total Days", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Weekend", displayName: "Weekend", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Present", displayName: "Present", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Leave", displayName: "Leave", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Holiday", displayName: "Holiday", minWidth: 140, headerCellClass: 'headerAligment' },				
		//	],
		//	//   rowTemplate: rowTemplate(),
		//	exporterCsvFilename: 'enqSummary.csv',
		//	exporterPdfDefaultStyle: { fontSize: 9 },
		//	exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
		//	exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
		//	exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
		//	exporterPdfFooter: function (currentPage, pageCount) {
		//		return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
		//	},
		//	exporterPdfCustomFormatter: function (docDefinition) {
		//		docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
		//		docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
		//		return docDefinition;
		//	},
		//	exporterPdfOrientation: 'portrait',
		//	exporterPdfPageSize: 'LETTER',
		//	exporterPdfMaxGridWidth: 500,
		//	exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
		//	exporterExcelFilename: 'enqSummary.xlsx',
		//	exporterExcelSheetName: 'enqSummary',
		//	onRegisterApi: function (gridApi) {
		//		$scope.gridApi2 = gridApi;
		//	}
		//};

		////Student Attendance
		//$scope.gridOptions3 = {
		//	showGridFooter: true,
		//	showColumnFooter: false,
		//	useExternalPagination: false,
		//	useExternalSorting: false,
		//	enableFiltering: true,
		//	enableSorting: true,
		//	enableRowSelection: true,
		//	enableSelectAll: true,
		//	enableGridMenu: true,
		//	columnDefs: [
		//		{ name: "SNo", displayName: "S.No.", minWidth: 60, headerCellClass: 'headerAligment', type: 'number' },
		//		{
		//			name: "EffectiveDate_AD", displayName: "Date(A.D.)", minWidth: 120, headerCellClass: 'headerAligment',
		//			cellTemplate: '<div>{{grid.appScope.DateFormatAD(row.entity.EffectiveDate_AD)}}</div>',
		//		},
		//		{ name: "EffectiveDate_BS", displayName: "Date(B.S.)", minWidth: 120, headerCellClass: 'headerAligment' },
		//		{ name: "InTime", displayName: "In Time(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
		//		{ name: "OutTime", displayName: "Out Time(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
		//		{ name: "AttendanceType", displayName: "Attendance", minWidth: 120, headerCellClass: 'headerAligment' },
		//		{ name: "TotalMinStr", displayName: "Study Hour(HH:MM)", minWidth: 110, headerCellClass: 'headerAligment' },
		//	],
		//	//   rowTemplate: rowTemplate(),
		//	exporterCsvFilename: 'enqSummary.csv',
		//	exporterPdfDefaultStyle: { fontSize: 9 },
		//	exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
		//	exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
		//	exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
		//	exporterPdfFooter: function (currentPage, pageCount) {
		//		return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
		//	},
		//	exporterPdfCustomFormatter: function (docDefinition) {
		//		docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
		//		docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
		//		return docDefinition;
		//	},
		//	exporterPdfOrientation: 'portrait',
		//	exporterPdfPageSize: 'LETTER',
		//	exporterPdfMaxGridWidth: 500,
		//	exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
		//	exporterExcelFilename: 'enqSummary.xlsx',
		//	exporterExcelSheetName: 'enqSummary',
		//	onRegisterApi: function (gridApi) {
		//		$scope.gridApi3 = gridApi;
		//	}
		//};

		////Classwise Summary
		//$scope.gridOptions4 = {
		//	showGridFooter: true,
		//	showColumnFooter: false,
		//	useExternalPagination: false,
		//	useExternalSorting: false,
		//	enableFiltering: true,
		//	enableSorting: true,
		//	enableRowSelection: true,
		//	enableSelectAll: true,
		//	enableGridMenu: true,
		//	columnDefs: [
		//		{ name: "SNo", displayName: "S.No.", minWidth: 90, headerCellClass: 'headerAligment', type: 'number' },
		//		{ name: "ClassName", displayName: "Class", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "SectionName", displayName: "Section", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "NoOfStudent", displayName: "Total", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Present", displayName: "Present", minWidth: 140, headerCellClass: 'headerAligment', type: 'number' },
		//		{ name: "Absent", displayName: "Absent ", minWidth: 140, headerCellClass: 'headerAligment', type: 'number' },
		//		{ name: "Leave", displayName: "Leave", minWidth: 140, headerCellClass: 'headerAligment', type: 'number' },
		//		{ name: "Present_Per", displayName: "Present %", minWidth: 140, headerCellClass: 'headerAligment', type: 'number' },
		//		{ name: "Absent_Per", displayName: "Absent %", minWidth: 140, headerCellClass: 'headerAligment', type: 'number'},
		//		{ name: "Leave_Per", displayName: "Leave %", minWidth: 140, headerCellClass: 'headerAligment', type: 'number' },
		//	],
		//	//   rowTemplate: rowTemplate(),
		//	exporterCsvFilename: 'enqSummary.csv',
		//	exporterPdfDefaultStyle: { fontSize: 9 },
		//	exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
		//	exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
		//	exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
		//	exporterPdfFooter: function (currentPage, pageCount) {
		//		return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
		//	},
		//	exporterPdfCustomFormatter: function (docDefinition) {
		//		docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
		//		docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
		//		return docDefinition;
		//	},
		//	exporterPdfOrientation: 'portrait',
		//	exporterPdfPageSize: 'LETTER',
		//	exporterPdfMaxGridWidth: 500,
		//	exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
		//	exporterExcelFilename: 'enqSummary.xlsx',
		//	exporterExcelSheetName: 'enqSummary',
		//	onRegisterApi: function (gridApi) {
		//		$scope.gridApi4 = gridApi;
		//	}
		//};		
	};

	$scope.LoadData = function () {

		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		//Added on Mangsir 30 by Suresh
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.InOutModeColl = [{ id: 0, text: 'All' }, { id: 1, text: 'Present' }, { id: 2, text: 'Absent' }, { id: 3, text: 'Late' }, { id: 4, text: 'Leave' }]
		$scope.currentPages = {
			periodWise: 1,

		};

		$scope.AttCategoryColl = [
			{ id: 1, text: 'Classwise' },
			{ id: 2, text: 'SubjectWise' },
		]

		$scope.PendingAtt = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			ClassId: null,
			SectionId: null,
			ClassYearId: null,
			SemesterId: null
		};

		$scope.searchData = {
			periodWise: '',
			AbsentStudentOnly: ''
		};

		$scope.perPage = {
			periodWise: GlobalServices.getPerPageRow(),

		};

		$scope.StudentTypeList = [];
		GlobalServices.getStudentTypeList().then(function (res) {
			$scope.StudentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newStudentType = {
			ForDate_TMP:new Date()
        }

		//Added By Suresh on Magh 17 starts
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

			}


			if ($scope.AcademicConfig.ActiveLevel == true) {

				$scope.LevelList = [];
				GlobalServices.getClassLevelList().then(function (res) {
					$scope.LevelList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveSemester == true) {

				/*	$scope.SelectedClassSemesterList = [];*/
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveBatch == true) {

				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveClassYear == true) {

				$scope.ClassYearList = [];
				/*$scope.SelectedClassClassYearList = [];*/
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		//Ends

		$scope.ClassShiftList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassShift",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassShiftList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.absentOnly = {
			ForDate_TMP: new Date()
		}

		$scope.dailyAttendance = {
			ForDate_TMP: new Date(),
			InOutMode: 0
		}

		$scope.periodWise = {
			ForDate_TMP: new Date()
		}
		//Ends
		$scope.newDaily = {
			ForDate: null,
			ForDate_TMP: new Date(),
			ClassIdColl: ''
		};
		//$scope.newMonthly = {
		//	YearId: 2078,
		//	SelectedClass:null,
		//	MonthId: 0,
		//	ClassId: 0,
		//	SectionId:null
		//};

		//$scope.newStudent = {
		//	StudentId: 0,
		//	DateFrom_TMP: new Date(),
		//	DateTo_TMP: new Date(),
		//	SelectStudent: $scope.StudentSearchOptions[0].value,
		//};

		//Added on Mangsir 30
		$scope.ClassSectionList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		//Ends

		$scope.ClassList = {};
		GlobalServices.getClassList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.MonthList = GlobalServices.getMonthList();
		$scope.MonthList = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.YearList = GlobalServices.getYearList();

		//$scope.ClassSection = {};
		//GlobalServices.getClassSectionList().then(function (res) {
		//	$scope.ClassSection = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});


		$scope.newClassWise = {
			ForDate_TMP: new Date()
		};
		//$scope.GetStudentDailyAttendance();

		$scope.AcademicYearColl = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllAcademicYearList",
			dataType: "json"
		}).then(function (res) {
			$scope.AcademicYearColl = res.data.Data;

			$http({
				method: 'GET',
				url: base_url + "Global/GetRunningAcademicYearId",
				dataType: "json"
			}).then(function (res) {
				var aId = res.data.Data.RId;

				var findA = mx($scope.AcademicYearColl).firstOrDefault(p1 => p1.AcademicYearId == aId);
				if (findA)
					$scope.newMonthly.YearId = parseInt(findA.Name);

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.GetStudentDailyAttendance = function () {

		//if (!$scope.newDaily.ClassIdColl) {
		//	// If ClassIdColl is not selected, display the message
		//	Swal.fire('Please select the class');
		//	return;  // Stop further execution of the function
		//}
		var para = {
			//forDate: $filter('date')($scope.newDaily.ForDateDet.dateAD, 'yyyy-MM-dd'),
			forDate: $scope.newDaily.ForDateDet && $scope.newDaily.ForDateDet.dateAD ? $filter('date')($scope.newDaily.ForDateDet.dateAD, 'yyyy-MM-dd') : new Date(),
			ClassIdColl: ($scope.newDaily.ClassIdColl ? $scope.newDaily.ClassIdColl.toString() : ''),
			BatchIdColl: ($scope.newDaily.BatchIdColl ? $scope.newDaily.BatchIdColl.toString() : ''),
			ClassYearIdColl: ($scope.newDaily.ClassYearIdColl ? $scope.newDaily.ClassYearIdColl.toString() : ''),
			SemesterIdColl: ($scope.newDaily.SemesterIdColl ? $scope.newDaily.SemesterIdColl.toString() : ''),
		};
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetStudentDailyBIOAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.gridOptions1.data = res.data.Data;

			} else {
				//alert(res.data.ResponseMSG)
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	//$scope.GetStudentMonthAttendance = function () {

	//	var para = {
	//		YearId: $scope.newMonthly.YearId,
	//		MonthId: $scope.newMonthly.MonthId,
	//		ClassId: $scope.newMonthly.SelectedClass.ClassId,
	//		SectionId:$scope.newMonthly.SelectedClass.SectionId
	//	};
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	$http({
	//		method: 'POST',
	//		url: base_url + "Attendance/Creation/GetStudentMonthlyBIOAttendance",
	//		dataType: "json",
	//		data: JSON.stringify(para)
	//	}).then(function (res)
	//	{
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess)
	//		{
	//			var tmpDataColl = res.data.Data;
	//			if (tmpDataColl && tmpDataColl.length > 0) {

	//				$timeout(function () {
	//					var totalDays = tmpDataColl[0].TotalDays;
	//					$scope.generateMonthlyColumns(totalDays);

	//					$scope.gridOptions2.data = res.data.Data;
	//				});					
	//               }

	//		} else {
	//			alert(res.data.ResponseMSG)
	//		}
	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//}

	//$scope.generateMonthlyColumns = function (totalDays) {

	//	//Attendance Summary Starts
	//	var columnDefs = [
	//		{ name: "SNo", displayName: "S.No.", minWidth: 90, headerCellClass: 'headerAligment' },
	//		{ name: "RegNo", displayName: "Regd No.", minWidth: 140, headerCellClass: 'headerAligment' },
	//		{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
	//		{ name: "RollNo", displayName: "Roll No", minWidth: 120, headerCellClass: 'headerAligment' },
	//		{ name: "EnrollNo", displayName: "Enroll No", minWidth: 120, headerCellClass: 'headerAligment' },
	//		{ name: "ClassSec", displayName: "Class/Sec", minWidth: 120, headerCellClass: 'headerAligment' },			
	//	];

	//	for (var d = 1; d <= totalDays; d++) {
	//		columnDefs.push({ name: "Day" + d, displayName: d, minWidth: 60, headerCellClass: 'headerAligment' });
	//       }
	//	columnDefs.push(
	//		{ name: "TotalDays", displayName: "Total Days", minWidth: 140, headerCellClass: 'headerAligment' },
	//		{ name: "TotalWeekend", displayName: "Weekend", minWidth: 140, headerCellClass: 'headerAligment' },
	//		{ name: "PresentDays", displayName: "Present", minWidth: 140, headerCellClass: 'headerAligment' },
	//		{ name: "TotalLeave", displayName: "Leave", minWidth: 140, headerCellClass: 'headerAligment' },
	//		{ name: "TotalHoliday", displayName: "Holiday", minWidth: 140, headerCellClass: 'headerAligment' },
	//	);

	//	$scope.gridOptions2 = {
	//		showGridFooter: true,
	//		showColumnFooter: false,
	//		useExternalPagination: false,
	//		useExternalSorting: false,
	//		enableFiltering: true,
	//		enableSorting: true,
	//		enableRowSelection: true,
	//		enableSelectAll: true,
	//		enableGridMenu: true,
	//		columnDefs:columnDefs,			
	//		exporterCsvFilename: 'enqSummary.csv',
	//		exporterPdfDefaultStyle: { fontSize: 9 },
	//		exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
	//		exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
	//		exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
	//		exporterPdfFooter: function (currentPage, pageCount) {
	//			return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
	//		},
	//		exporterPdfCustomFormatter: function (docDefinition) {
	//			docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
	//			docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
	//			return docDefinition;
	//		},
	//		exporterPdfOrientation: 'portrait',
	//		exporterPdfPageSize: 'LETTER',
	//		exporterPdfMaxGridWidth: 500,
	//		exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
	//		exporterExcelFilename: 'enqSummary.xlsx',
	//		exporterExcelSheetName: 'enqSummary',
	//		onRegisterApi: function (gridApi) {
	//			$scope.gridApi = gridApi;
	//		}
	//	};

	//};

	//$scope.getStudentBIOAttendance = function () {

	//	$scope.newStudent.TotalDays = 0;
	//	$scope.newStudent.TotalPresent = 0;
	//	$scope.newStudent.TotalAbsent = 0;
	//	$scope.newStudent.TotalWeekEnd = 0;
	//	$scope.newStudent.TotalHoliday = 0;

	//	$scope.gridOptions3.data = [];

	//	var para = {
	//		StudentId:$scope.newStudent.StudentId,
	//		dateFrom: $filter('date')($scope.newStudent.DateFromDet.dateAD, 'yyyy-MM-dd'),
	//		dateTo: $filter('date')($scope.newStudent.DateToDet.dateAD, 'yyyy-MM-dd')
	//	};
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	$http({
	//		method: 'POST',
	//		url: base_url + "Attendance/Creation/GetStudentBIOAttendance",
	//		dataType: "json",
	//		data: JSON.stringify(para)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess) {

	//			var dataColl = res.data.Data;
	//			$scope.gridOptions3.data = dataColl;

	//			var query = mx(dataColl);

	//			$scope.newStudent.TotalDays = dataColl.length;
	//			$scope.newStudent.TotalPresent = query.count(p1=>p1.IsPresent==true);				
	//			$scope.newStudent.TotalWeekEnd = query.count(p1 => p1.IsWeekEnd == true);
	//			$scope.newStudent.TotalHoliday = query.count(p1 => p1.IsHoliday == true);
	//			$scope.newStudent.TotalAbsent = $scope.newStudent.TotalDays-$scope.newStudent.TotalWeekEnd-$scope.newStudent.TotalPresent-$scope.newStudent.TotalHoliday;

	//		} else {
	//			alert(res.data.ResponseMSG)
	//			//Swal.fire(res.data.ResponseMSG);
	//		}

	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//};


	//$scope.GetClassWiseDailyAttendance = function () {

	//	$scope.gridOptions4.data = [];
	//	var para = {
	//		forDate: $filter('date')($scope.newClassWise.ForDateDet.dateAD, 'yyyy-MM-dd')
	//	};
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	$http({
	//		method: 'POST',
	//		url: base_url + "Attendance/Creation/GetClassWiseBIOAttendance",
	//		dataType: "json",
	//		data: JSON.stringify(para)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess) {
	//			$scope.gridOptions4.data = res.data.Data;

	//		} else {
	//			alert(res.data.ResponseMSG)
	//			//Swal.fire(res.data.ResponseMSG);
	//		}

	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//}


	//Code Added from Student Manual Attendance on Mangsir 30 starts
	$scope.GetDailyAttSummary = function () {

		var para = {
			ClassId: $scope.dailyAttendance.SelectedClassSection ? $scope.dailyAttendance.SelectedClassSection.ClassId : null,
			SectionId: $scope.dailyAttendance.SelectedClassSection ? $scope.dailyAttendance.SelectedClassSection.SectionId : null,
			forDate: $filter('date')($scope.dailyAttendance.ForDateDet.dateAD, 'yyyy-MM-dd'),
			InOutMode: $scope.dailyAttendance.InOutMode,
			//Added By suresh on Magh 17 
			BatchId: $scope.dailyAttendance.BatchId,
			ClassYearId: $scope.dailyAttendance.ClassYearId,
			SemesterId: $scope.dailyAttendance.SemesterId
		};
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetSTMADaily",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions.data = res.data.Data;
			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}


	$scope.GetPeriodWise = function () {

		if (!$scope.periodWise.SelectedClass) {
			Swal.fire('Please ! Select Valid Class Name');
			return;
		}

		$scope.periodWise.SubjectList = [];
		$scope.periodWise.StudentList = [];
		//Done: Add paramete BatchId, ClassYearId, SemesterId
		var para = {
			ClassId: $scope.periodWise.SelectedClass.ClassId,
			SectionId: $scope.periodWise.SelectedClass.SectionId,
			BatchId: $scope.periodWise.BatchId || null,
			ClassYearId: $scope.periodWise.ClassYearId || null,
			SemesterId: $scope.periodWise.SemesterId || null,
			forDate: $filter('date')(new Date($scope.periodWise.ForDateDet.dateAD), 'yyyy-MM-dd'),
		};

		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetSTPeriodADaily",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data) {
				var tmpDataColl = mx(res.data.Data);

				var finalColl = [];
				var subjectQuery = tmpDataColl.groupBy(t => t.SubjectName).toArray();
				var fiSNo = 1;
				angular.forEach(subjectQuery, function (f) {
					$scope.periodWise.SubjectList.push(
						{
							id: f.elements[0].Period,
							text: (f.key ? f.key : '')
						});
				});

				var query = tmpDataColl.groupBy(t => t.StudentId).toArray();
				var nSNO = 1;
				angular.forEach(query, function (q) {
					var subData = mx(q.elements);
					var fst = subData.firstOrDefault();
					var beData = {
						SNo: nSNO,
						StudentId: fst.StudentId,
						RegNo: fst.RegNo,
						Name: fst.Name,
						RollNo: fst.RollNo,
						ClassName: fst.ClassName + ' ' + fst.SectionName,
						FatherName: fst.FatherName,
						SubjectDetailsColl: []
					};

					var totalP = 0;
					angular.forEach($scope.periodWise.SubjectList, function (fi) {
						var find = subData.firstOrDefault(p1 => p1.SubjectName == fi.text);
						beData.SubjectDetailsColl.push({
							SubjectName: fi.text,
							Attendance: (find ? find.Attendance : 'A')
						});

						if (find && (find.Attendance == 'P' || find.Attendance == 'La'))
							totalP++;
					});

					beData.TotalAttendance = totalP;
					finalColl.push(beData);
					nSNO++;
				});

				$scope.periodWise.StudentList = finalColl;

			} else {
				Swal.fire(res.data);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	};

	var tmpAbsentStudentList = [];
	$scope.GetAbsentStudentOnly = function () {

		$scope.AbsentStudentList = [];
		tmpAbsentStudentList = [];
		var para = {
			ClassId: $scope.absentOnly.SelectedClassSection ? $scope.absentOnly.SelectedClassSection.ClassId : null,
			SectionId: $scope.absentOnly.SelectedClassSection ? $scope.absentOnly.SelectedClassSection.SectionId : null,
			forDate: $filter('date')($scope.absentOnly.ForDateDet.dateAD, 'yyyy-MM-dd'),
			InOutMode: 2,
			ClassShiftId: $scope.absentOnly.ClassShiftId,
			//Added By suresh on Magh 17 
			BatchId: $scope.absentOnly.BatchId,
			ClassYearId: $scope.absentOnly.ClassYearId,
			SemesterId: $scope.absentOnly.SemesterId
		};
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetSTMADaily",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				tmpAbsentStudentList = res.data.Data;

				var secGroup = [];
				var secQuery = mx(tmpAbsentStudentList).groupBy(t => t.ClassSection);
				angular.forEach(secQuery, function (ss) {
					var sg = {
						ClassSectionName: ss.key,
						DataColl: ss.elements,
					};
					secGroup.push(sg);
				})
				$scope.AbsentStudentList = secGroup;

			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}



	$scope.SendNoticeToAbsStudent = function () {

		$scope.newNotice = {};

		Swal.fire({
			title: 'Do you want to Send Notification To Absent Students data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				var para1 = {
					EntityId: entityAbsentStudentForSMS,
					ForATS: 3,
					TemplateType: 3
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/GetSENT",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						var templatesColl = res.data.Data;
						if (templatesColl && templatesColl.length > 0) {
							var templatesName = [];
							var sno = 1;
							angular.forEach(templatesColl, function (tc) {
								templatesName.push(sno + '-' + tc.Name);
								sno++;
							});

							var print = false;

							var rptTranId = 0;
							var selectedTemplate = null;
							if (templatesColl.length == 1) {
								rptTranId = templatesColl[0].TranId;
								selectedTemplate = templatesColl[0];
							}
							else {
								Swal.fire({
									title: 'Templates For Notification',
									input: 'select',
									inputOptions: templatesName,
									inputPlaceholder: 'Select a template',
									showCancelButton: true,
									inputValidator: (value) => {
										return new Promise((resolve) => {
											if (value >= 0) {
												resolve()
												rptTranId = templatesColl[value].TranId;
												selectedTemplate = templatesColl[value];

												if (rptTranId > 0) {
													$scope.newNotice.Title = selectedTemplate.Title;
													$scope.newNotice.Description = selectedTemplate.Description;
													$('#modal-xl').modal('show');
												}

											} else {
												resolve('You need to select:)')
											}
										})
									}
								})
							}

							if (rptTranId > 0 && print == false) {
								$scope.newNotice.Title = selectedTemplate.Title;
								$scope.newNotice.Description = selectedTemplate.Description;
								$('#modal-xl').modal('show');
							}

						} else {
							$scope.newNotice.Title = '';
							$scope.newNotice.Description = '';
							$('#modal-xl').modal('show');
						}

					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}
		});


	};


	$scope.SendManualNoticeToStudent = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var dataColl = [];
		angular.forEach(tmpAbsentStudentList, function (node) {
			var objEntity = node;
			var msg = $scope.newNotice.Description;
			for (let x in objEntity) {
				var variable = '$$' + x.toLowerCase() + '$$';
				if (msg.indexOf(variable) >= 0) {
					var val = objEntity[x];
					msg = msg.replace(variable, val);
				}

				if (msg.indexOf('$$') == -1)
					break;
			}

			var newSMS = {
				//EntityId: entityStudentSummaryForSMS,
				EntityId: entityAbsentStudentForSMS,
				StudentId: objEntity.StudentId,
				UserId: objEntity.UserId,
				Title: $scope.newNotice.Title,
				Message: msg,
				ContactNo: objEntity.ContactNo,
				StudentName: objEntity.Name
			};

			dataColl.push(newSMS);
		});
		$http({
			method: 'POST',
			url: base_url + "Global/SendNotificationToStudent",
			dataType: "json",
			data: JSON.stringify(dataColl)
		}).then(function (sRes) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			Swal.fire(sRes.data.ResponseMSG);
			if (sRes.data.IsSuccess && sRes.data.Data) {

			}
		});

	};

	$scope.SendSMSToAbsStudent = function () {
		Swal.fire({
			title: 'Do you want to Send SMS To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				var para1 = {
					EntityId: entityAbsentStudentForSMS,
					ForATS: 3,
					TemplateType: 1
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/GetSENT",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						var templatesColl = res.data.Data;
						if (templatesColl && templatesColl.length > 0) {
							var templatesName = [];
							var sno = 1;
							angular.forEach(templatesColl, function (tc) {
								templatesName.push(sno + '-' + tc.Name);
								sno++;
							});

							var print = false;

							var rptTranId = 0;
							var selectedTemplate = null;
							if (templatesColl.length == 1) {
								rptTranId = templatesColl[0].TranId;
								selectedTemplate = templatesColl[0];
							}
							else {
								Swal.fire({
									title: 'Templates For SMS',
									input: 'select',
									inputOptions: templatesName,
									inputPlaceholder: 'Select a template',
									showCancelButton: true,
									inputValidator: (value) => {
										return new Promise((resolve) => {
											if (value >= 0) {
												resolve()
												rptTranId = templatesColl[value].TranId;
												selectedTemplate = templatesColl[value];

												if (rptTranId > 0) {
													var dataColl = [];
													angular.forEach(tmpAbsentStudentList, function (node) {
														var objEntity = node;
														var tmpContactNo = '';
														tmpContactNo = objEntity.ContactNo;

														if (tmpContactNo && tmpContactNo.length > 0) {
															var msg = selectedTemplate.Description;
															for (let x in objEntity) {
																var variable = '$$' + x.toLowerCase() + '$$';
																if (msg.indexOf(variable) >= 0) {
																	var val = objEntity[x];
																	msg = msg.replace(variable, val);
																}

																if (msg.indexOf('$$') == -1)
																	break;
															}

															var newSMS = {
																//EntityId: entityStudentSummaryForSMS,
																EntityId: entityAbsentStudentForSMS,
																StudentId: objEntity.StudentId,
																UserId: objEntity.UserId,
																Title: selectedTemplate.Title,
																Message: msg,
																ContactNo: tmpContactNo,
																StudentName: objEntity.Name
															};

															dataColl.push(newSMS);
														}
													});

													print = true;

													$http({
														method: 'POST',
														url: base_url + "Global/SendSMSToStudent",
														dataType: "json",
														data: JSON.stringify(dataColl)
													}).then(function (sRes) {
														Swal.fire(sRes.data.ResponseMSG);
														if (sRes.data.IsSuccess && sRes.data.Data) {

														}
													});

												}

											} else {
												resolve('You need to select:)')
											}
										})
									}
								})
							}

							if (rptTranId > 0 && print == false) {
								var dataColl = [];

								angular.forEach(tmpAbsentStudentList, function (node) {
									var objEntity = node;
									var tmpContactNo = '';
									tmpContactNo = objEntity.ContactNo;

									if (tmpContactNo && tmpContactNo.length > 0) {
										var msg = selectedTemplate.Description;
										for (let x in objEntity) {
											var variable = '$$' + x.toLowerCase() + '$$';
											if (msg.indexOf(variable) >= 0) {
												var val = objEntity[x];
												msg = msg.replace(variable, val);
											}

											if (msg.indexOf('$$') == -1)
												break;
										}

										var newSMS = {
											//EntityId: entityStudentSummaryForSMS,
											EntityId: entityAbsentStudentForSMS,
											StudentId: objEntity.StudentId,
											UserId: objEntity.UserId,
											Title: selectedTemplate.Title,
											Message: msg,
											ContactNo: tmpContactNo,
											StudentName: objEntity.Name
										};

										dataColl.push(newSMS);
									}
								});
								print = true;

								$http({
									method: 'POST',
									url: base_url + "Global/SendSMSToStudent",
									dataType: "json",
									data: JSON.stringify(dataColl)
								}).then(function (sRes) {
									Swal.fire(sRes.data.ResponseMSG);
									if (sRes.data.IsSuccess && sRes.data.Data) {

									}
								});

							}

						} else
							Swal.fire('No Templates found for SMS');
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	$scope.GetStudentTypeWiseAttendance = function () {

		var para = {
			StudentTypeId: $scope.newStudentType.StudentTypeId,
			ClassId: $scope.newStudentType.ClassSection.ClassId,
			SectionId: $scope.newStudentType.ClassSection.SectionId,
			BatchId: $scope.newStudentType.BatchId,
			ClassYearId: $scope.newStudentType.ClassYearId,
			SemesterId: $scope.newStudentType.SemesterId,
			ForDate: $filter('date')(new Date($scope.newStudentType.ForDateDet.dateAD), 'yyyy-MM-dd'),
			InOutMode: 3
		};
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetStudentTypeWiseAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions3.data = res.data.Data;
			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}


	//---------------------Pending Attendance------------------
	$scope.onClassChange = function (selectedClass) {
		if (!selectedClass || !selectedClass.ClassId) {
			$scope.FilteredSections = [];
			return;
		}
		$scope.FilteredSections = $scope.SectionList.filter(function (se) {
			return se.ClassId === selectedClass.ClassId;
		});
	};

	$scope.GetStudentPendingAtt = function () {
		//if (!$scope.newDaily.ClassIdColl) {
		//	// If ClassIdColl is not selected, display the message
		//	Swal.fire('Please select the class');
		//	return;  // Stop further execution of the function
		//}
		var para = {
			DateFrom: $filter('date')($scope.PendingAtt.FromDateDet.dateAD, 'yyyy-MM-dd'),
			DateTo: $filter('date')($scope.PendingAtt.ToDateDet.dateAD, 'yyyy-MM-dd'),
			ClassId: $scope.PendingAtt.ClassId || null,
			SectionId: $scope.PendingAtt.SectionId || null,
			BatchId: $scope.PendingAtt.BatchId || null,
			SemesterId: $scope.PendingAtt.SemesterId || null,
			ClassYearId: $scope.PendingAtt.ClassYearId || null,
			AttenCategory: $scope.PendingAtt.CollAttenCategory || null,
		};
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetPendingAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.gridOptions2.data = res.data.Data;

			} else {
				//alert(res.data.ResponseMSG)
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.validateDates = function (changedField) {
		if (!$scope.PendingAtt.FromDateDet || !$scope.PendingAtt.ToDateDet ||
			!$scope.PendingAtt.FromDateDet.dateAD || !$scope.PendingAtt.ToDateDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.PendingAtt.FromDateDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.PendingAtt.ToDateDet.dateAD), 'yyyy-MM-dd')
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.PendingAtt.FromDate_TMP = new Date();
						$scope.PendingAtt.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.PendingAtt.ToDate_TMP = new Date();
						$scope.PendingAtt.ToDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};

});