app.controller('EmployeeAttendanceController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {
	$scope.Title = 'Employee Attendance';
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

			var findInd = -1;

			//$scope.gridApi3.grid.getColumn('DateAD').colDef.displayName = 'Date';
			//$scope.gridApi3.grid.getColumn('DateAD').displayName = 'Date';

			//findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'DateBS' });
			//if (findInd != -1)
			//	$scope.gridOptions3.columnDefs.splice(findInd, 1);

		}
		/*	$scope.gridApi3.grid.refresh();*/

		$scope.BranchList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchList",
			dataType: "json",
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.LoadData();


	};
	$rootScope.ChangeLanguage();


	function getterAndSetter() {
		$scope.gridOptions = [];
		//$scope.gridOptions2 = [];
		//$scope.gridOptions3 = [];
		$scope.gridOptions4 = [];
		$scope.gridOptions5 = [];

		//Daily Biometric Attendance
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
				{ name: "EmpCode", displayName: "Emp.Code", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "Designation", displayName: "Designation", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "EnrollNo", displayName: "Enroll No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Attendance", displayName: "Attendance", minWidth: 120, headerCellClass: 'headerAligment' },

				{ name: "InTime", displayName: "In Time(HH:MM)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "OutTime", displayName: "Out Time(HH:MM)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LateInStr", displayName: "Late In(HH:MM)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BeforeOutStr", displayName: "Early Out(HH:MM)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "WorkingHR", displayName: "Total Working Hours", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "WorkingShift", displayName: "Shift", minWidth: 120, headerCellClass: 'headerAligment' },

				{ name: "In1", displayName: "In Time1(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Out1", displayName: "Out Time1(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "In2", displayName: "In Time2(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Out2", displayName: "Out Time2(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "In3", displayName: "In Time3(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Out3", displayName: "Out Time3(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "In4", displayName: "In Time4(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Out4", displayName: "Out Time4(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "In5", displayName: "In Time5(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Out5", displayName: "Out Time5(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "WorkingHRAsInOut", displayName: "WorkingHR as IN OUT", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Out5", displayName: "Out Time5(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Category", displayName: "Category", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BranchName", displayName: "Branch", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BranchAddress", displayName: "Branch Address", minWidth: 140, headerCellClass: 'headerAligment' },

			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'empAttendance.csv',
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

		//Attendance Summary
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
		//		{ name: "SNo", displayName: "S.No.", minWidth: 90, headerCellClass: 'headerAligment' },
		//		{ name: "EmpCode", displayName: "Emp. Code", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
		//		{ name: "Designation", displayName: "Designation", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "EnrollNumber", displayName: "Enroll No", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Day1", displayName: "Day1", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Day2", displayName: "Day2", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Day3", displayName: "Day3", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "WorkingShift", displayName: "Shift", minWidth: 120, headerCellClass: 'headerAligment' },
		//		{ name: "TotalDays", displayName: "Total Days", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Weekend", displayName: "Weekend", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Present", displayName: "Present", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Leave", displayName: "Leave", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Holiday", displayName: "Holiday", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Category", displayName: "Category", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "BranchName", displayName: "Branch", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "BranchAddress", displayName: "Branch Address", minWidth: 140, headerCellClass: 'headerAligment' },
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

		//Employee Attendance
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
		//		{ name: "SNO", displayName: "S.No.", minWidth: 90, headerCellClass: 'headerAligment' },
		//		{ name: "DateAD", displayName: "Date(A.D.)", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "DateBS", displayName: "Date(B.S.)", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "InTime", displayName: "In Time", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "OutTime", displayName: "Out Time", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Attendance", displayName: "Attendance", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "WorkingHour", displayName: "Working Hour", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "WorkingShift", displayName: "Shift", minWidth: 120, headerCellClass: 'headerAligment' },
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

		//Manual Attendance
		$scope.gridOptions4 = {
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
				{ name: "EmpCode", displayName: "Emp.Code", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "Designation", displayName: "Designation", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "EnrollNo", displayName: "Enroll No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Attendance", displayName: "Attendance", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Lateinmin", displayName: "Late in min", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Remarks", displayName: "Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Category", displayName: "Category", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BranchName", displayName: "Branch", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BranchAddress", displayName: "Branch Address", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "WorkingShift", displayName: "Shift", minWidth: 120, headerCellClass: 'headerAligment' },

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
				$scope.gridApi4 = gridApi;
			}
		};

		//Absent Only Starts
		$scope.gridOptions5 = {
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
				{ name: "EmpCode", displayName: "Emp.Code", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "Designation", displayName: "Designation", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "EnrollNo", displayName: "Enroll No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Attendance", displayName: "Attendance", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "Category", displayName: "Category", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BranchName", displayName: "Branch", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BranchAddress", displayName: "Branch Address", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "WorkingShift", displayName: "Shift", minWidth: 120, headerCellClass: 'headerAligment' },
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
				$scope.gridApi5 = gridApi;
			}
		};
	};

	$scope.LoadData = function () {

		$scope.newDaily = {
			ForDate: null,
			ForDate_TMP: new Date(),
		};

		$scope.newMonthly = {
			YearId: 2078,
			MonthId: 0
		};

		$scope.newInOutDet = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
		};
		
		$scope.newPeriodwise = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
		};

		$scope.entity = {
			InOutDetails: 125
		};

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.InOutDetails + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newInOutDet.TemplatesColl = res.data.Data;
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
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		$scope.newEmp = {
			EmployeeId: 0,
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
		};

		$scope.newManual = {
			ForDate: null,
			ForDate_TMP: new Date(),
		};

		$scope.newAbsent = {
			ForDate: null,
			ForDate_TMP: new Date(),
		};
		$scope.CurrentDateDet = {};
		$http({
			method: 'POST',
			url: base_url + "Global/GetDate",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.CurrentDateDet = res.data.Data;

				$scope.newMonthly.YearId = $scope.CurrentDateDet.NY;
				$scope.newMonthly.MonthId = $scope.CurrentDateDet.NM;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


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
	$scope.GetDailyAttendance = function () {
		$scope.gridOptions.data = [];
		var para = {
			forDate: $filter('date')($scope.newDaily.ForDateDet.dateAD, 'yyyy-MM-dd'),
			branchIdColl: $scope.newDaily.BranchId
		};
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetEmpDailyAttendance",
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

	//$scope.GetMonthAttendance = function () {
	//	$scope.gridOptions2.data = [];
	//	var para = {
	//		YearId: $scope.newMonthly.YearId,
	//		MonthId: $scope.newMonthly.MonthId,
	//		branchIdColl:$scope.newMonthly.BranchId
	//	};
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	$http({
	//		method: 'POST',
	//		url: base_url + "Attendance/Creation/GetEmpMonthlyBIOAttendance",
	//		dataType: "json",
	//		data: JSON.stringify(para)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess) {
	//			var tmpDataColl = res.data.Data;
	//			if (tmpDataColl && tmpDataColl.length > 0) {

	//				$timeout(function () {
	//					var totalDays = tmpDataColl[0].TotalDays;
	//					$scope.generateMonthlyColumns(totalDays);

	//					$scope.gridOptions2.data = res.data.Data;
	//				});
	//			}

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
	//		{ name: "EmpCode", displayName: "Emp.Code", minWidth: 140, headerCellClass: 'headerAligment' },
	//		{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
	//		{ name: "Department", displayName: "Department", minWidth: 120, headerCellClass: 'headerAligment' },
	//		{ name: "EnrollNumber", displayName: "Enroll No", minWidth: 120, headerCellClass: 'headerAligment' },
	//		{ name: "Designation", displayName: "Designation", minWidth: 120, headerCellClass: 'headerAligment' },
	//		{ name: "Category", displayName: "Category", minWidth: 140, headerCellClass: 'headerAligment' },
	//		{ name: "BranchName", displayName: "Branch", minWidth: 140, headerCellClass: 'headerAligment' },
	//		{ name: "BranchAddress", displayName: "Branch Address", minWidth: 140, headerCellClass: 'headerAligment' },

	//	];

	//	for (var d = 1; d <= totalDays; d++) {
	//		columnDefs.push({ name: "Day" + d, displayName: d, minWidth: 60, headerCellClass: 'headerAligment' });
	//	}
	//	columnDefs.push(
	//		{ name: "WorkingShift", displayName: "Shift", minWidth: 120, headerCellClass: 'headerAligment' },
	//		{ name: "TotalDays", displayName: "Total Days", minWidth: 140, headerCellClass: 'headerAligment' },
	//		{ name: "TotalWeekend", displayName: "Weekend", minWidth: 140, headerCellClass: 'headerAligment' },
	//		{ name: "TotalPresent", displayName: "Present", minWidth: 140, headerCellClass: 'headerAligment' },
	//		{ name: "TotalLeave", displayName: "Leave", minWidth: 140, headerCellClass: 'headerAligment' },
	//		{ name: "TotalHoliday", displayName: "Holiday", minWidth: 140, headerCellClass: 'headerAligment' },
	//		{ name: "TotalAbsent", displayName: "TotalAbsent", minWidth: 140, headerCellClass: 'headerAligment' },
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
	//		columnDefs: columnDefs,
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
	//		exporterExcelFilename: 'empAttendance.xlsx',
	//		exporterExcelSheetName: 'empAttendance',
	//		onRegisterApi: function (gridApi) {
	//			$scope.gridApi = gridApi;
	//		}
	//	};

	//};

	//$scope.getEmpBIOAttendance = function () {

	//	if ($scope.newEmp.EmployeeId && $scope.newEmp.DateFromDet && $scope.newEmp.DateToDet) {

	//	} else
	//		return;

	//	$scope.newEmp.TotalDays = 0;
	//	$scope.newEmp.TotalPresent = 0;
	//	$scope.newEmp.TotalAbsent = 0;
	//	$scope.newEmp.TotalWeekEnd = 0;
	//	$scope.newEmp.TotalHoliday = 0;

	//	$scope.gridOptions3.data = [];

	//	var para = {
	//		employeeId: $scope.newEmp.EmployeeId,
	//		fromDate: $filter('date')($scope.newEmp.DateFromDet.dateAD, 'yyyy-MM-dd'),
	//		toDate: $filter('date')($scope.newEmp.DateToDet.dateAD, 'yyyy-MM-dd')
	//	};
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	$http({
	//		method: 'POST',
	//		url: base_url + "Attendance/Creation/GetEmpWiseAttendance",
	//		dataType: "json",
	//		data: JSON.stringify(para)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess) {

	//			var dataColl = res.data.Data;
	//			$scope.gridOptions3.data = dataColl;

	//			var query = mx(dataColl);

	//			$scope.newEmp.TotalDays = dataColl.length;
	//			$scope.newEmp.TotalPresent = query.count(p1 => p1.IsPresent == true);
	//			$scope.newEmp.TotalWeekEnd = query.count(p1 => p1.IsWeekEnd == true);
	//			$scope.newEmp.TotalHoliday = query.count(p1 => p1.IsHoliday == true);

	//			if (dataColl && dataColl.length > 0)
	//				$scope.newEmp.TotalAbsent = dataColl[0].TotalAbsent;
	//			else
	//				$scope.newEmp.TotalAbsent = $scope.newEmp.TotalDays - $scope.newEmp.TotalWeekEnd - $scope.newEmp.TotalPresent - $scope.newEmp.TotalHoliday;

	//		} else {
	//			alert(res.data.ResponseMSG)
	//			//Swal.fire(res.data.ResponseMSG);
	//		}

	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//};

	$scope.GetManualDailyAttendance = function () {
		$scope.gridOptions.data = [];
		var para = {
			forDate: $filter('date')($scope.newManual.ForDateDet.dateAD, 'yyyy-MM-dd'),
			branchIdColl: $scope.newManual.BranchId
		};
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetEmpManualDailyAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions4.data = res.data.Data;

			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetEmpAbsentAttendance = function () {
		$scope.gridOptions.data = [];
		var para = {
			forDate: $filter('date')($scope.newAbsent.ForDateDet.dateAD, 'yyyy-MM-dd'),
		};
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetEmpAbsentList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions5.data = res.data.Data;

			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.PrintInOutDet = function () {
		if ($scope.newInOutDet.FromDateDet && $scope.newInOutDet.ToDateDet) {

			var EntityId = $scope.entity.Tabulation;

			var rptPara = {
				dateFrom: $filter('date')($scope.newInOutDet.FromDateDet.dateAD, 'yyyy-MM-dd'),
				dateTo: $filter('date')($scope.newInOutDet.ToDateDet.dateAD, 'yyyy-MM-dd'),
				dateFromBS: $scope.newInOutDet.FromDateDet.dateBS,
				dateToBS: $scope.newInOutDet.ToDateDet.dateBS,
				period: $scope.newInOutDet.FromDateDet.dateBS + ' TO ' + $scope.newInOutDet.ToDateDet.dateBS,
				rptTranId: $scope.newInOutDet.RptTranId
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptTabulation").src = '';
			document.getElementById("frmRptTabulation").style.width = '100%';
			document.getElementById("frmRptTabulation").style.height = '1300px';
			document.getElementById("frmRptTabulation").style.visibility = 'visible';
			document.getElementById("frmRptTabulation").src = base_url + "Attendance/Creation/RdlEmpDateWiseInOut?" + paraQuery;

		}

	};

	$scope.validateDates = function (changedField) {
		if (!$scope.newPeriodwise.FromDateDet || !$scope.newPeriodwise.ToDateDet ||
			!$scope.newPeriodwise.FromDateDet.dateAD || !$scope.newPeriodwise.ToDateDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newPeriodwise.FromDateDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newPeriodwise.ToDateDet.dateAD), 'yyyy-MM-dd')
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newPeriodwise.FromDate_TMP = new Date();
						$scope.newPeriodwise.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newPeriodwise.ToDate_TMP = new Date();
						$scope.newPeriodwise.ToDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};
});