app.controller('StudentManualAttendanceController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {
	$scope.Title = 'Student Manual Attendance';
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
			RegdNo: keyColl ? keyColl['REGDNO_LNG'] : 'Reg.No.'
		};
		if ($rootScope.LANG == 'in') {

			//$scope.gridApi.grid.getColumn('RegdNo').colDef.displayName = Labels.RegdNo;
			//$scope.gridApi.grid.getColumn('RegdNo').displayName = Labels.RegdNo;
			//var findInd = -1;

			$scope.gridApi3.grid.getColumn('RegdNo').colDef.displayName = Labels.RegdNo;
			$scope.gridApi3.grid.getColumn('RegdNo').displayName = Labels.RegdNo;


			$scope.gridApi4.grid.getColumn('EffectiveDate_AD').colDef.displayName = 'Date';
			$scope.gridApi4.grid.getColumn('EffectiveDate_AD').displayName = 'Date';

			findInd = $scope.gridOptions4.columnDefs.findIndex(function (obj) { return obj.name == 'EffectiveDate_BS' });
			if (findInd != -1)
				$scope.gridOptions4.columnDefs.splice(findInd, 1);

		}
		/*	$scope.gridApi.grid.refresh();*/
		$scope.gridApi2.grid.refresh();
		$scope.gridApi3.grid.refresh();
		$scope.gridApi4.grid.refresh();

		$scope.LoadData();
	};
	$rootScope.ChangeLanguage();

	function getterAndSetter() {
		/*	$scope.gridOptions = [];*/
		$scope.gridOptions2 = [];
		$scope.gridOptions3 = [];
		$scope.gridOptions4 = [];
		$scope.gridOptions5 = [];

		////Daily Attendance
		//$scope.gridOptions = {
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
		//		{ name: "RegdNo", displayName: "Regd No.", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
		//		{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "ClassSection", displayName: "Class/Sec", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Attendance", displayName: "Attendance", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "LateMin", displayName: "Late in min.", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "FatherName", displayName: "Father Name", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "ContactNo", displayName: "Contact No", minWidth: 140, headerCellClass: 'headerAligment' },

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
		//		$scope.gridApi = gridApi;
		//	}
		//};

		//ClassWise Summary Starts
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
				{ name: "DT_BS", displayName: "Date", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "ClassSection", displayName: "ClassSec", minWidth: 170, headerCellClass: 'headerAligment' },
				{ name: "NoOfStudent", displayName: "No.Of Student", minWidth: 130, headerCellClass: 'headerAligment' },
				{ name: "Present", displayName: "Present", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Absent", displayName: "Absent ", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Leave", displayName: "Leave", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "PresentPer", displayName: "Present %", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "AbsentPer", displayName: "Absent %", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "LeavePer", displayName: "Leave %", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "Holiday", displayName: "Holiday", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "IsWeekEnd", displayName: "IsWeekEnd", minWidth: 120, headerCellClass: 'headerAligment' },
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
				$scope.gridApi2 = gridApi;
			}
		};

		//Attendance Summary
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
				{ name: "SNo", displayName: "S.No.", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "RegdNo", displayName: "Regd No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassSec", displayName: "Class/Sec", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Day1", displayName: "Day1", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Day2", displayName: "Day2", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Day3", displayName: "Day3", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TotalDays", displayName: "Total Days", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Weekend", displayName: "Weekend", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Present", displayName: "Present", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Leave", displayName: "Leave", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Holiday", displayName: "Holiday", minWidth: 140, headerCellClass: 'headerAligment' },
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
				$scope.gridApi3 = gridApi;
			}
		};

		//Student Attendance
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
				{ name: "SNo", displayName: "S.No.", minWidth: 60, headerCellClass: 'headerAligment', type: 'number' },
				{
					name: "EffectiveDate_AD", displayName: "Date(A.D.)", minWidth: 120, headerCellClass: 'headerAligment',
					cellTemplate: '<div>{{grid.appScope.DateFormatAD(row.entity.EffectiveDate_AD)}}</div>',
				},
				{ name: "EffectiveDate_BS", displayName: "Date(B.S.)", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "InTime", displayName: "In Time(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "OutTime", displayName: "Out Time(HH:MM)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "AttendanceType", displayName: "Attendance", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "TotalMinStr", displayName: "Study Hour(HH:MM)", minWidth: 110, headerCellClass: 'headerAligment' },

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


		//Student Attendance Sumary
		$scope.gridOptions5 = {
			showGridFooter: true,
			showColumnFooter: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,
			exporterCsvFilename: 'StudentAttendanceSummary.csv',
			exporterPdfDefaultStyle: { fontSize: 9 },
			exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
			exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. \n Birgunj Nepal", style: 'headerStyle' },
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
			exporterExcelFilename: 'StudentAttendanceSummary.xlsx',
			exporterExcelSheetName: 'StudentAttendanceSummary',
			columnDefs: [
				{ field: "RegNo", displayName: "Reg No", minWidth: 80, headerCellClass: 'headerAligment', type: 'number' },
				{ field: "StudentName", displayName: "Name", minWidth: 180, headerCellClass: 'headerAligment' },
				{ field: "ClassName", displayName: "ClassName", minWidth: 120, headerCellClass: 'headerAligment' },
				{ field: "SectionName", displayName: "SectionName", minWidth: 100, headerCellClass: 'headerAligment' },
				{ field: "RollNo", displayName: "Roll No", minWidth: 100, headerCellClass: 'headerAligment' },
				{ field: "EMSId", displayName: "EMIS No", minWidth: 120, headerCellClass: 'headerAligment' },
				{ field: "Batch", displayName: "Batch", minWidth: 110, headerCellClass: 'headerAligment', visible: true },
				{ field: "Semester", displayName: "Semester", minWidth: 110, headerCellClass: 'headerAligment', visible: true },
				{ field: "ClassYear", displayName: "Class Year", minWidth: 110, headerCellClass: 'headerAligment', visible: true },
				{ field: "TotalDays", displayName: "Total Days", minWidth: 110, headerCellClass: 'headerAligment' },
				{ field: "SchoolDays", displayName: "School Days", minWidth: 110, headerCellClass: 'headerAligment' },
				{ field: "TotalWeekEnd", displayName: "Weeks Off", minWidth: 110, headerCellClass: 'headerAligment' },
				{ field: "TotalHoliday", displayName: "Holiday", minWidth: 110, headerCellClass: 'headerAligment' },
				{ field: "TotalPresent", displayName: "Present", minWidth: 110, headerCellClass: 'headerAligment' },
				{ field: "TotalLeave", displayName: "Leave", minWidth: 110, headerCellClass: 'headerAligment' },
				{ field: "TotalAbsent", displayName: "Absent", minWidth: 110, headerCellClass: 'headerAligment' },
				{ field: "LeftStatus", displayName: "Left Status", minWidth: 110, headerCellClass: 'headerAligment' }
			],
			onRegisterApi: function (gridApi) {
				$scope.gridApi5 = gridApi;
			}
		};

	};

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


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


		//Added By Suresh on Magh 17 starts



		$scope.AcademicConfig = {};
		//pahela res1 theyo...eslai res banako at 25 chaitra 2081
		GlobalServices.getAcademicConfig().then(function (res) {
			$scope.AcademicConfig = res.data.Data;

			if ($scope.AcademicConfig.ActiveFaculty == true) {

				$scope.FacultyList = [];
				GlobalServices.getFacultyList().then(function (res) {
					$scope.FacultyList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				var col = ($scope.gridOptions5?.columnDefs || []).find(x => x.field === "Faculty");
				if (col) col.visible = false;
			}

			if ($scope.AcademicConfig.ActiveLevel == true) {

				$scope.LevelList = [];
				GlobalServices.getClassLevelList().then(function (res) {
					$scope.LevelList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				var col = ($scope.gridOptions5?.columnDefs || []).find(x => x.field === "Level");
				if (col) col.visible = false;
			}


			if ($scope.AcademicConfig.ActiveBatch == true) {

				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				var col = ($scope.gridOptions5?.columnDefs || []).find(x => x.field === "Batch");
				if (col) col.visible = false;
			}

			if ($scope.AcademicConfig.ActiveSemester == true) {

				$scope.SelectedClassSemesterList = [];
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				var col = ($scope.gridOptions5?.columnDefs || []).find(x => x.field === "Semester");
				if (col) col.visible = false;
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
				var col = ($scope.gridOptions5?.columnDefs || []).find(x => x.field === "ClassYear");
				if (col) col.visible = false;
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		//Ends


		$scope.SubjectList = {};
		GlobalServices.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.InOutModeColl = [{ id: 0, text: 'All' }, { id: 1, text: 'Present' }, { id: 2, text: 'Absent' }, { id: 3, text: 'Late' }, { id: 4, text: 'Leave' }]
		$scope.currentPages = {
			periodWise: 1,
			Subjectwise: 1,
			SubjectwiseAttendance: 1
		};

		$scope.searchData = {
			periodWise: '',
			Subjectwise: '',
			SubjectwiseAttendance: '',
			AbsentStudentOnly: ''
		};

		$scope.perPage = {
			periodWise: GlobalServices.getPerPageRow(),
			Subjectwise: GlobalServices.getPerPageRow(),
			SubjectwiseAttendance: GlobalServices.getPerPageRow(),
		};

		$scope.ClassSectionList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.classWiseSummary = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date()
		};
		$scope.dailyAttendance = {
			ForDate_TMP: new Date(),
			InOutMode: 0,
		};
		$scope.newSubjectAttendence = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			SubjectList: []
		};

		$scope.periodWise =
		{
			ForDate_TMP: new Date()
		};
		$scope.filterAttendanceSum =
		{
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
			BatchId: null,
			ClassYearId: null,
			SemesterId: null,
		};

		$scope.newMonthly = {
			YearId: 2078,
			SelectedClass: null,
			MonthId: 0,
			ClassId: 0,
			SectionId: null
		};

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

		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.newStudent = {
			StudentId: 0,
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
			SelectStudent: $scope.StudentSearchOptions[0].value,
		};

		$scope.absentOnly = {
			ForDate_TMP: new Date()
		}

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

	//$scope.GetDailyAttSummary = function () {

	//	var para = {
	//		ClassId: $scope.dailyAttendance.SelectedClassSection ? $scope.dailyAttendance.SelectedClassSection.ClassId : null,
	//		SectionId: $scope.dailyAttendance.SelectedClassSection ? $scope.dailyAttendance.SelectedClassSection.SectionId : null,
	//		forDate: $filter('date')($scope.dailyAttendance.ForDateDet.dateAD, 'yyyy-MM-dd'),
	//		InOutMode: $scope.dailyAttendance.InOutMode
	//	};
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	$http({
	//		method: 'POST',
	//		url: base_url + "Attendance/Creation/GetSTMADaily",
	//		dataType: "json",
	//		data: JSON.stringify(para)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess) {
	//			$scope.gridOptions.data = res.data.Data;
	//		} else {
	//			alert(res.data.ResponseMSG)
	//			//Swal.fire(res.data.ResponseMSG);
	//		}

	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//}

	//$scope.GetPeriodWise = function () {

	//	if (!$scope.periodWise.SelectedClass) {
	//		Swal.fire('Please ! Select Valid Class Name');
	//		return;
	//	}

	//	$scope.periodWise.SubjectList = [];
	//	$scope.periodWise.StudentList = [];
	//	var para = {

	//		ClassId: $scope.periodWise.SelectedClass.ClassId,
	//		SectionId: $scope.periodWise.SelectedClass.SectionId,
	//		forDate: $filter('date')(new Date($scope.periodWise.ForDateDet.dateAD), 'yyyy-MM-dd'),
	//	};

	//	$http({
	//		method: 'POST',
	//		url: base_url + "Attendance/Creation/GetSTPeriodADaily",
	//		dataType: "json",
	//		data: JSON.stringify(para)
	//	}).then(function (res) {
	//		if (res.data) {
	//			var tmpDataColl = mx(res.data.Data);

	//			var finalColl = [];
	//			var subjectQuery = tmpDataColl.groupBy(t => t.SubjectName).toArray();
	//			var fiSNo = 1;
	//			angular.forEach(subjectQuery, function (f) {
	//				$scope.periodWise.SubjectList.push(
	//					{
	//						id: f.elements[0].Period,
	//						text: (f.key ? f.key : '')
	//					});
	//			});

	//			var query = tmpDataColl.groupBy(t => t.StudentId).toArray();
	//			var nSNO = 1;
	//			angular.forEach(query, function (q) {
	//				var subData = mx(q.elements);
	//				var fst = subData.firstOrDefault();
	//				var beData = {
	//					SNo: nSNO,
	//					StudentId: fst.StudentId,
	//					RegNo: fst.RegNo,
	//					Name: fst.Name,
	//					RollNo: fst.RollNo,
	//					ClassName: fst.ClassName + ' ' + fst.SectionName,
	//					FatherName: fst.FatherName,
	//					SubjectDetailsColl: []
	//				};

	//				var totalP = 0;
	//				angular.forEach($scope.periodWise.SubjectList, function (fi) {
	//					var find = subData.firstOrDefault(p1 => p1.SubjectName == fi.text);
	//					beData.SubjectDetailsColl.push({
	//						SubjectName: fi.text,
	//						Attendance: (find ? find.Attendance : 'A')
	//					});

	//					if (find && (find.Attendance == 'P' || find.Attendance == 'La'))
	//						totalP++;
	//				});

	//				beData.TotalAttendance = totalP;
	//				finalColl.push(beData);
	//				nSNO++;
	//			});

	//			$scope.periodWise.StudentList = finalColl;

	//		} else {
	//			Swal.fire(res.data);
	//		}

	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});


	//};

	$scope.GetClassWiseSubMap = function () {

		$scope.newSubjectAttendence.SubjectList = [];


		if ($scope.newSubjectAttendence.SelectedClass) {

			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				ClassId: $scope.newSubjectAttendence.SelectedClass.ClassId,
				SectionIdColl: ($scope.newSubjectAttendence.SelectedClass ? $scope.newSubjectAttendence.SelectedClass.SectionId : '')
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetSubjectMappingClassWise",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {

					$timeout(function () {
						angular.forEach(res.data.Data, function (sm) {
							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								$scope.newSubjectAttendence.SubjectList.push(subDet);
							}
						});

					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};
	$scope.GetSubjectWiseAttendance = function () {

		if (!$scope.newSubjectAttendence.SelectedClass) {
			Swal.fire('Please ! Select Valid Class Name');
			return;
		}

		if (!$scope.newSubjectAttendence.SubjectId) {
			Swal.fire('Please ! Select Valid Subject Name');
			return;
		}

		$scope.newSubjectAttendence.DateList = [];
		$scope.newSubjectAttendence.StudentList = [];
		var para = {
			fromDate: $filter('date')(new Date($scope.newSubjectAttendence.FromDateDet.dateAD), 'yyyy-MM-dd'),
			toDate: $filter('date')(new Date($scope.newSubjectAttendence.ToDateDet.dateAD), 'yyyy-MM-dd'),
			classId: $scope.newSubjectAttendence.SelectedClass.ClassId,
			sectionId: $scope.newSubjectAttendence.SelectedClass.SectionId,
			subjectId: $scope.newSubjectAttendence.SubjectId,
			BatchId: $scope.newSubjectAttendence.BatchId,
			SemesterId: $scope.newSubjectAttendence.SemesterId,
			ClassYearId: $scope.newSubjectAttendence.ClassYearId
		};

		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetSubjectWiseAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data) {
				var tmpDataColl = mx(res.data.Data);

				var finalColl = [];
				var subjectQuery = tmpDataColl.groupBy(t => t.ForDate_BS).toArray();
				var fiSNo = 1;
				angular.forEach(subjectQuery, function (f) {
					var fst = f.elements[0];
					$scope.newSubjectAttendence.DateList.push(
						{
							id: fst.Period,
							text: (f.key ? f.key : ''),
							shorttext: fst.NM + '-' + fst.ND,
							/*shorttext: fst.ND + '-' + fst.NM,*/
							forDate: new Date(fst.ForDate_AD)
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
					angular.forEach($scope.newSubjectAttendence.DateList, function (fi) {
						var find = subData.firstOrDefault(p1 => p1.ForDate_BS == fi.text);
						beData.SubjectDetailsColl.push({
							ForDate_BS: fi.text,
							Attendance: (find ? find.Attendance : 'A')
						});

						if (find && (find.Attendance == 'P' || find.Attendance == 'La'))
							totalP++;
					});

					beData.TotalAttendance = totalP;

					var attPer = (totalP / beData.SubjectDetailsColl.length) * 100;


					beData.AttendancePer = attPer;
					beData.AbsentPer = (100 - attPer);
					finalColl.push(beData);
					nSNO++;
				});

				$scope.newSubjectAttendence.StudentList = finalColl;

			} else {
				Swal.fire(res.data);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	};

	$scope.GetClassWiseSummary = function () {

		var para = {
			fromDate: $filter('date')($scope.classWiseSummary.FromDateDet.dateAD, 'yyyy-MM-dd'),
			toDate: $filter('date')($scope.classWiseSummary.ToDateDet.dateAD, 'yyyy-MM-dd'),
		};
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetClassWiseSummary",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions2.data = res.data.Data;
			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetStudentMonthAttendance = function () {

		var para = {
			YearId: $scope.newMonthly.YearId,
			MonthId: $scope.newMonthly.MonthId,
			ClassId: $scope.newMonthly.SelectedClass.ClassId,
			SectionId: $scope.newMonthly.SelectedClass.SectionId,

			BatchId: $scope.newMonthly.BatchId || null,
			ClassYearId: $scope.newMonthly.ClassYearId || null,
			SemesterId: $scope.newMonthly.SemesterId || null,
		};
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetStudentMonthlyBIOAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				var tmpDataColl = res.data.Data;
				if (tmpDataColl && tmpDataColl.length > 0) {

					$timeout(function () {
						var totalDays = tmpDataColl[0].TotalDays;
						$scope.generateMonthlyColumns(totalDays);

						$scope.gridOptions3.data = res.data.Data;
					});
				}

			} else {
				alert(res.data.ResponseMSG)
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}


	$scope.generateMonthlyColumns = function (totalDays) {

		//Attendance Summary Starts
		var columnDefs = [
			{ name: "SNo", displayName: "S.No.", minWidth: 90, headerCellClass: 'headerAligment' },
			{ name: "RegNo", displayName: "Regd No.", minWidth: 140, headerCellClass: 'headerAligment' },
			{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
			{ name: "RollNo", displayName: "Roll No", minWidth: 120, headerCellClass: 'headerAligment' },
			{ name: "EnrollNo", displayName: "Enroll No", minWidth: 120, headerCellClass: 'headerAligment' },
			{ name: "ClassSec", displayName: "Class/Sec", minWidth: 120, headerCellClass: 'headerAligment' },
		];

		for (var d = 1; d <= totalDays; d++) {
			columnDefs.push({ name: "Day" + d, displayName: d, minWidth: 60, headerCellClass: 'headerAligment' });
		}
		columnDefs.push(
			{ name: "TotalDays", displayName: "Total Days", minWidth: 140, headerCellClass: 'headerAligment' },
			{ name: "TotalWeekend", displayName: "Weekend", minWidth: 140, headerCellClass: 'headerAligment' },
			{ name: "PresentDays", displayName: "Present", minWidth: 140, headerCellClass: 'headerAligment' },
			{ name: "TotalLeave", displayName: "Leave", minWidth: 140, headerCellClass: 'headerAligment' },
			{ name: "TotalHoliday", displayName: "Holiday", minWidth: 140, headerCellClass: 'headerAligment' },
			{ name: "AbsentDays", displayName: "Absent", minWidth: 140, headerCellClass: 'headerAligment' },
		);

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
			columnDefs: columnDefs,
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

	};

	$scope.getStudentBIOAttendance = function () {

		$scope.newStudent.TotalDays = 0;
		$scope.newStudent.TotalPresent = 0;
		$scope.newStudent.TotalAbsent = 0;
		$scope.newStudent.TotalWeekEnd = 0;
		$scope.newStudent.TotalHoliday = 0;
		//Leave Added By Suresh on Mangsir 30
		$scope.newStudent.Leave = 0;
		//Ends
		$scope.gridOptions4.data = [];

		var para = {
			StudentId: $scope.newStudent.StudentId,
			dateFrom: $filter('date')($scope.newStudent.DateFromDet.dateAD, 'yyyy-MM-dd'),
			dateTo: $filter('date')($scope.newStudent.DateToDet.dateAD, 'yyyy-MM-dd')
		};
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetStudentBIOAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {

				var dataColl = res.data.Data;
				$scope.gridOptions4.data = dataColl;

				var query = mx(dataColl);

				$scope.newStudent.TotalDays = dataColl.length;
				$scope.newStudent.TotalPresent = query.count(p1 => p1.IsPresent == true);
				$scope.newStudent.TotalWeekEnd = query.count(p1 => p1.IsWeekEnd == true);
				$scope.newStudent.TotalHoliday = query.count(p1 => p1.IsHoliday == true);
				//OnLeave
				$scope.newStudent.Leave = query.count(p1 => p1.IsPresent == false && p1.AttendanceType === "L");

				//$scope.newStudent.TotalAbsent = $scope.newStudent.TotalDays - $scope.newStudent.TotalWeekEnd - $scope.newStudent.TotalPresent - $scope.newStudent.TotalHoliday;
				$scope.newStudent.TotalAbsent = query.count(p1 => p1.IsAbsent == true);

			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.GetStudentAttendanceSumary = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			DateFrom: $filter('date')($scope.filterAttendanceSum?.DateFromDet?.dateAD || null, 'yyyy-MM-dd'),
			DateTo: $filter('date')($scope.filterAttendanceSum?.DateToDet?.dateAD || null, 'yyyy-MM-dd'),
			BatchId: $scope.filterAttendanceSum?.BatchId || null,
			ClassId: $scope.filterAttendanceSum?.SelectedClass?.ClassId || null,
			SectionId: $scope.filterAttendanceSum?.SelectedClass?.SectionId || null,
			ClassYearId: $scope.filterAttendanceSum?.ClassYearId || null,
			SemesterId: $scope.filterAttendanceSum?.SemesterId || null,
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetStudentAttendanceSumary",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions5.data = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG)
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.validateDates = function (changedField, obj) {
		if (!obj.FromDateDet || !obj.ToDateDet ||
			!obj.FromDateDet.dateAD || !obj.ToDateDet.dateAD) {
			return true;
		}

		var fromDate = $filter('date')(new Date(obj.FromDateDet.dateAD), 'yyyy-MM-dd');
		var toDate = $filter('date')(new Date(obj.ToDateDet.dateAD), 'yyyy-MM-dd');

		if (!fromDate || !toDate) return true;

		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.FromDate_TMP = new Date();
						obj.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.ToDate_TMP = new Date();
						obj.ToDateDet = new Date();
					});
				});
			}
			return false;
		}
		return true;
	};

	$scope.validateDates1 = function (changedField, obj) {
		if (!obj.DateFromDet || !obj.DateToDet ||
			!obj.DateFromDet.dateAD || !obj.DateToDet.dateAD) {
			return true;
		}

		var fromDate = $filter('date')(new Date(obj.DateFromDet.dateAD), 'yyyy-MM-dd');
		var toDate = $filter('date')(new Date(obj.DateToDet.dateAD), 'yyyy-MM-dd');

		if (!fromDate || !toDate) return true;

		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.DateFrom_TMP = new Date();
						obj.DateFromDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.DateTo_TMP = new Date();
						obj.DateToDet = new Date();
					});
				});
			}
			return false;
		}
		return true;
	};
	//var tmpAbsentStudentList = [];
	//$scope.GetAbsentStudentOnly = function () {

	//	$scope.AbsentStudentList = [];
	//	tmpAbsentStudentList = [];
	//	var para = {
	//		ClassId: $scope.absentOnly.SelectedClassSection ? $scope.absentOnly.SelectedClassSection.ClassId : null,
	//		SectionId: $scope.absentOnly.SelectedClassSection ? $scope.absentOnly.SelectedClassSection.SectionId : null,
	//		forDate: $filter('date')($scope.absentOnly.ForDateDet.dateAD, 'yyyy-MM-dd'),
	//		InOutMode: 2,
	//		ClassShiftId:$scope.absentOnly.ClassShiftId,
	//	};
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	$http({
	//		method: 'POST',
	//		url: base_url + "Attendance/Creation/GetSTMADaily",
	//		dataType: "json",
	//		data: JSON.stringify(para)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess) {
	//			tmpAbsentStudentList = res.data.Data;

	//			var secGroup = [];
	//			var secQuery = mx(tmpAbsentStudentList).groupBy(t => t.ClassSection);
	//			angular.forEach(secQuery, function (ss) {
	//				var sg = {
	//					ClassSectionName: ss.key,
	//					DataColl: ss.elements,
	//				};
	//				secGroup.push(sg);
	//			})
	//			$scope.AbsentStudentList = secGroup;

	//		} else {
	//			alert(res.data.ResponseMSG)
	//			//Swal.fire(res.data.ResponseMSG);
	//		}

	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//}
	//$scope.SendNoticeToAbsStudent = function () {

	//	$scope.newNotice = {};

	//	Swal.fire({
	//		title: 'Do you want to Send Notification To Absent Students data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {

	//			var para1 = {
	//				EntityId: entityAbsentStudentForSMS,
	//				ForATS: 3,
	//				TemplateType: 3
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Setup/Security/GetSENT",
	//				dataType: "json",
	//				data: JSON.stringify(para1)
	//			}).then(function (res) {
	//				if (res.data.IsSuccess && res.data.Data) {
	//					var templatesColl = res.data.Data;
	//					if (templatesColl && templatesColl.length > 0) {
	//						var templatesName = [];
	//						var sno = 1;
	//						angular.forEach(templatesColl, function (tc) {
	//							templatesName.push(sno + '-' + tc.Name);
	//							sno++;
	//						});

	//						var print = false;

	//						var rptTranId = 0;
	//						var selectedTemplate = null;
	//						if (templatesColl.length == 1) {
	//							rptTranId = templatesColl[0].TranId;
	//							selectedTemplate = templatesColl[0];
	//						}
	//						else {
	//							Swal.fire({
	//								title: 'Templates For Notification',
	//								input: 'select',
	//								inputOptions: templatesName,
	//								inputPlaceholder: 'Select a template',
	//								showCancelButton: true,
	//								inputValidator: (value) => {
	//									return new Promise((resolve) => {
	//										if (value >= 0) {
	//											resolve()
	//											rptTranId = templatesColl[value].TranId;
	//											selectedTemplate = templatesColl[value];

	//											if (rptTranId > 0) {
	//												$scope.newNotice.Title = selectedTemplate.Title;
	//												$scope.newNotice.Description = selectedTemplate.Description;
	//												$('#modal-xl').modal('show');
	//											}

	//										} else {
	//											resolve('You need to select:)')
	//										}
	//									})
	//								}
	//							})
	//						}

	//						if (rptTranId > 0 && print == false) {
	//							$scope.newNotice.Title = selectedTemplate.Title;
	//							$scope.newNotice.Description = selectedTemplate.Description;
	//							$('#modal-xl').modal('show');
	//						}

	//					} else {
	//						$scope.newNotice.Title = '';
	//						$scope.newNotice.Description = '';
	//						$('#modal-xl').modal('show');
	//					}

	//				}
	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});

	//		}
	//	});


	//};
	//$scope.SendManualNoticeToStudent = function () {

	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	var dataColl = [];
	//	angular.forEach(tmpAbsentStudentList, function (node) {
	//		var objEntity = node;
	//		var msg = $scope.newNotice.Description;
	//		for (let x in objEntity) {
	//			var variable = '$$' + x.toLowerCase() + '$$';
	//			if (msg.indexOf(variable) >= 0) {
	//				var val = objEntity[x];
	//				msg = msg.replace(variable, val);
	//			}

	//			if (msg.indexOf('$$') == -1)
	//				break;
	//		}

	//		var newSMS = {
	//			//EntityId: entityStudentSummaryForSMS,
	//			EntityId: entityAbsentStudentForSMS,
	//			StudentId: objEntity.StudentId,
	//			UserId: objEntity.UserId,
	//			Title: $scope.newNotice.Title,
	//			Message: msg,
	//			ContactNo: objEntity.ContactNo,
	//			StudentName: objEntity.Name
	//		};

	//		dataColl.push(newSMS);
	//	});
	//	$http({
	//		method: 'POST',
	//		url: base_url + "Global/SendNotificationToStudent",
	//		dataType: "json",
	//		data: JSON.stringify(dataColl)
	//	}).then(function (sRes) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";

	//		Swal.fire(sRes.data.ResponseMSG);
	//		if (sRes.data.IsSuccess && sRes.data.Data) {

	//		}
	//	});

	//};

	//$scope.SendSMSToAbsStudent = function () {
	//	Swal.fire({
	//		title: 'Do you want to Send SMS To the filter data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {
	//			var para1 = {
	//				EntityId: entityAbsentStudentForSMS,
	//				ForATS: 3,
	//				TemplateType: 1
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Setup/Security/GetSENT",
	//				dataType: "json",
	//				data: JSON.stringify(para1)
	//			}).then(function (res) {
	//				if (res.data.IsSuccess && res.data.Data) {
	//					var templatesColl = res.data.Data;
	//					if (templatesColl && templatesColl.length > 0) {
	//						var templatesName = [];
	//						var sno = 1;
	//						angular.forEach(templatesColl, function (tc) {
	//							templatesName.push(sno + '-' + tc.Name);
	//							sno++;
	//						});

	//						var print = false;

	//						var rptTranId = 0;
	//						var selectedTemplate = null;
	//						if (templatesColl.length == 1) {
	//							rptTranId = templatesColl[0].TranId;
	//							selectedTemplate = templatesColl[0];
	//						}
	//						else {
	//							Swal.fire({
	//								title: 'Templates For SMS',
	//								input: 'select',
	//								inputOptions: templatesName,
	//								inputPlaceholder: 'Select a template',
	//								showCancelButton: true,
	//								inputValidator: (value) => {
	//									return new Promise((resolve) => {
	//										if (value >= 0) {
	//											resolve()
	//											rptTranId = templatesColl[value].TranId;
	//											selectedTemplate = templatesColl[value];

	//											if (rptTranId > 0) {
	//												var dataColl = [];
	//												angular.forEach(tmpAbsentStudentList, function (node) {
	//													var objEntity = node;
	//													var tmpContactNo = '';
	//													tmpContactNo = objEntity.ContactNo;

	//													if (tmpContactNo && tmpContactNo.length > 0) {
	//														var msg = selectedTemplate.Description;
	//														for (let x in objEntity) {
	//															var variable = '$$' + x.toLowerCase() + '$$';
	//															if (msg.indexOf(variable) >= 0) {
	//																var val = objEntity[x];
	//																msg = msg.replace(variable, val);
	//															}

	//															if (msg.indexOf('$$') == -1)
	//																break;
	//														}

	//														var newSMS = {
	//															//EntityId: entityStudentSummaryForSMS,
	//															EntityId: entityAbsentStudentForSMS,
	//															StudentId: objEntity.StudentId,
	//															UserId: objEntity.UserId,
	//															Title: selectedTemplate.Title,
	//															Message: msg,
	//															ContactNo: tmpContactNo,
	//															StudentName: objEntity.Name
	//														};

	//														dataColl.push(newSMS);
	//													}
	//												});

	//												print = true;

	//												$http({
	//													method: 'POST',
	//													url: base_url + "Global/SendSMSToStudent",
	//													dataType: "json",
	//													data: JSON.stringify(dataColl)
	//												}).then(function (sRes) {
	//													Swal.fire(sRes.data.ResponseMSG);
	//													if (sRes.data.IsSuccess && sRes.data.Data) {

	//													}
	//												});

	//											}

	//										} else {
	//											resolve('You need to select:)')
	//										}
	//									})
	//								}
	//							})
	//						}

	//						if (rptTranId > 0 && print == false) {
	//							var dataColl = [];

	//							angular.forEach(tmpAbsentStudentList, function (node) {
	//								var objEntity = node;
	//								var tmpContactNo = '';
	//								tmpContactNo = objEntity.ContactNo;

	//								if (tmpContactNo && tmpContactNo.length > 0) {
	//									var msg = selectedTemplate.Description;
	//									for (let x in objEntity) {
	//										var variable = '$$' + x.toLowerCase() + '$$';
	//										if (msg.indexOf(variable) >= 0) {
	//											var val = objEntity[x];
	//											msg = msg.replace(variable, val);
	//										}

	//										if (msg.indexOf('$$') == -1)
	//											break;
	//									}

	//									var newSMS = {
	//										//EntityId: entityStudentSummaryForSMS,
	//										EntityId: entityAbsentStudentForSMS,
	//										StudentId: objEntity.StudentId,
	//										UserId: objEntity.UserId,
	//										Title: selectedTemplate.Title,
	//										Message: msg,
	//										ContactNo: tmpContactNo,
	//										StudentName: objEntity.Name
	//									};

	//									dataColl.push(newSMS);
	//								}
	//							});
	//							print = true;

	//							$http({
	//								method: 'POST',
	//								url: base_url + "Global/SendSMSToStudent",
	//								dataType: "json",
	//								data: JSON.stringify(dataColl)
	//							}).then(function (sRes) {
	//								Swal.fire(sRes.data.ResponseMSG);
	//								if (sRes.data.IsSuccess && sRes.data.Data) {

	//								}
	//							});

	//						}

	//					} else
	//						Swal.fire('No Templates found for SMS');
	//				}
	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	});


	//};
});