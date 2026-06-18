app.controller('HostelAttendanceController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Hostel Attendance';

	getterAndSetter();

	function getterAndSetter() {
		$scope.gridOptions = [];
		$scope.gridOptions3 = [];
		$scope.gridOptions4 = [];
		//Daily Attendance
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
				{
				name: "SNo",
				displayName: "S.No.",
				minWidth: 90,
				enableSorting: false,
				enableColumnMenu: false,
				headerCellClass: 'headerAligment',
				cellTemplate: '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
			},
				{ name: "RegNo", displayName: "Admission No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "StudentName", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "ClassSection", displayName: "Class/Sec", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RoomDetail", displayName: "Room Detail", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ShiftName", displayName: "Shift", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "StatusName", displayName: "Status", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Remarks", displayName: "Remarks", minWidth: 350, headerCellClass: 'headerAligment' },

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
				{
					name: "SNo",
					displayName: "S.No.",
					minWidth: 90,
					enableSorting: false,
					enableColumnMenu: false,
					headerCellClass: 'headerAligment',
					cellTemplate: '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
				},
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
		//Monthly Attendance
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
				{ name: "AdmissionNo", displayName: "Admission No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "StudentName", displayName: "Student Name", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "ClassSec", displayName: "Class/Sec", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RoomNo", displayName: "RoomNo", minWidth: 140, headerCellClass: 'headerAligment' },
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


	};

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

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
			Attendance: ''
		};

		$scope.perPage = {
			periodWise: GlobalServices.getPerPageRow(),
			Subjectwise: GlobalServices.getPerPageRow(),
			SubjectwiseAttendance: GlobalServices.getPerPageRow()
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

		$scope.ShiftList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllHostelAttendanceShiftList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingShift = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ShiftList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.StatusList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllHostelAttendanceStatusList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StatusList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



		$scope.newAttendance = {
			HostelAttendanceId: null,
			StudentId: null,
			HostelId: null,
			BuildingId: null,
			FloorId: null,
			ShiftId: null,
			ForDate_TMP: new Date(),
			ObservationRemarks: '',
			StatusId: null,
			Remarks: '',
			Mode: 'Save'
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


		$scope.HostelList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllHostelList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HostelList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.BuildingList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllBuildingList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BuildingList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.FloorList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllFloorList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FloorList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});





	};


	$scope.ClearAttendance = function () {
		$scope.newAttendance = {
			HostelAttendanceId: null,
			StudentId: null,
			HostelId: null,
			BuildingId: null,
			FloorId: null,
			ShiftId: null,
			ForDate_TMP: new Date(),
			ObservationRemarks: '',
			StatusId: null,
			Remarks: '',
			Mode: 'Save'
		};

	}


	//*************************Add Attendance *********************************

	$scope.GetAllHostelAttendance = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		if ($scope.newAttendance.ForDateDet)
			var forDate = $filter('date')(new Date($scope.newAttendance.ForDateDet.dateAD), 'yyyy-MM-dd');
		var para = {
			HostelId: $scope.newAttendance.HostelId,
			BuildingId: $scope.newAttendance.BuildingId,
			FloorId: $scope.newAttendance.FloorId,
			ShiftId: $scope.newAttendance.ShiftId,
			ForDate: forDate
		}
		$scope.newAttendance.StudentList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllHostelAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAttendance.StudentList = res.data.Data;
				$scope.newAttendance.ObservationRemarks = res.data.Data[0].ObservationRemarks;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.SaveHostelAttendance = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var attendanceList = [];

		angular.forEach($scope.newAttendance.StudentList, function (s) {
			attendanceList.push({
				HostelId: $scope.newAttendance.HostelId,
				BuildingId: $scope.newAttendance.BuildingId,
				FloorId: $scope.newAttendance.FloorId,
				ShiftId: $scope.newAttendance.ShiftId,
				ForDate: $filter('date')(new Date($scope.newAttendance.ForDateDet.dateAD), 'yyyy-MM-dd'),
				ObservationRemarks: $scope.newAttendance.ObservationRemarks,
				StudentId: s.StudentId,
				StatusId: s.StatusId,
				Remarks: s.Remarks || ''
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveHostelAttendance",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: attendanceList }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
		}, function () {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire("Error", "Failed to save hostel attendance.", "error");
		});
	};


	//*************************Hostel Daily Attendance *********************************

	$scope.GetStudentHostelDailyAttendance = function () {
		if ($scope.newAttendance.ForDateDet)
			var forDate = $filter('date')(new Date($scope.dailyAttendance.ForDateDet.dateAD), 'yyyy-MM-dd');
		var para = {
			HostelId: $scope.dailyAttendance.HostelId,
			ForDate: forDate,
			BuildingId: $scope.dailyAttendance.BuildingId,
			FloorId: $scope.dailyAttendance.FloorId,
			ShiftId: $scope.dailyAttendance.ShiftId,
		}
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllHostelAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions.data = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.validateDates = function (changedField, obj) {
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