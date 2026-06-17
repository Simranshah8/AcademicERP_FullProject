
app.controller('ExamScheduleController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Exam Schedule';
	var glbS = GlobalServices;
	OnClickDefault();

	//Ui grid table added by suresh on bhadra 25 starts
	/*getterAndSetter();*/

	function OnClickDefault() {

		document.getElementById('examshift-form').style.display = "none";
		// Exam shift js part
		document.getElementById('add-exam-shift').onclick = function () {
			document.getElementById('examshift-section').style.display = "none";
			document.getElementById('examshift-form').style.display = "block";
			$scope.ClearExamShift();
		}
		document.getElementById('examshift-back-btn').onclick = function () {
			document.getElementById('examshift-form').style.display = "none";
			document.getElementById('examshift-section').style.display = "block";
			$scope.ClearExamShift();
		}

	}

	//function getterAndSetter() {


	//	$scope.gridOptions = [];

	//	$scope.gridOptions = {
	//		showGridFooter: true,
	//		showColumnFooter: false,
	//		useExternalPagination: false,
	//		useExternalSorting: false,
	//		enableFiltering: true,
	//		enableSorting: true,
	//		enableRowSelection: true,
	//		enableSelectAll: true,
	//		enableGridMenu: true,

	//		columnDefs: [
	//			{ name: "SymbolNo", displayName: "Symbol No", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "RegNo", displayName: "Regd.No", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "ClassName", displayName: "Class", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "SectionName", displayName: "Section", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "BoardName", displayName: "BoardName", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "BoardRegNo", displayName: "BoardRegNo", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "TotalSubject", displayName: "TotalSubject", minWidth: 100, headerCellClass: 'headerAligment' },
	//			{ name: "SubjectDetails", displayName: "SubjectDetails", minWidth: 240, headerCellClass: 'headerAligment' },
	//			{ name: "SubjectCodeDetails", displayName: "SubjectCodeDetails", minWidth: 240, headerCellClass: 'headerAligment' },
	//			{ name: "SubjectDetailsWExamDate", displayName: "SubjectDetailsWExamDate", minWidth: 240, headerCellClass: 'headerAligment' },
	//			{ name: "SubjectDetailsWExamDateTime", displayName: "SubjectDetailsWExamDateTime", minWidth: 240, headerCellClass: 'headerAligment' },
	//			{ name: "Room", displayName: "Room", minWidth: 100, headerCellClass: 'headerAligment' },
	//			{ name: "RowName", displayName: "RowName", minWidth: 100, headerCellClass: 'headerAligment' },
	//			{ name: "BenchOrdinalNo", displayName: "Bench No", minWidth: 100, headerCellClass: 'headerAligment' },
	//			{ name: "SeatCol", displayName: "SeatCol", minWidth: 100, headerCellClass: 'headerAligment' },
	//			{ name: "ExamShiftName", displayName: "ExamShift", minWidth: 100, headerCellClass: 'headerAligment' },

	//		],
	//		//   rowTemplate: rowTemplate(),
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


	//	$scope.gridOptions1 = [];

	//	$scope.gridOptions1 = {
	//		showGridFooter: true,
	//		showColumnFooter: false,
	//		useExternalPagination: false,
	//		useExternalSorting: false,
	//		enableFiltering: true,
	//		enableSorting: true,
	//		enableRowSelection: true,
	//		enableSelectAll: true,
	//		enableGridMenu: true,

	//		columnDefs: [
	//			{ name: "SymbolNo", displayName: "Symbol No", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "RegNo", displayName: "Regd.No", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "ClassName", displayName: "Class", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "SectionName", displayName: "Section", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "BoardName", displayName: "BoardName", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "BoardRegNo", displayName: "BoardRegNo", minWidth: 140, headerCellClass: 'headerAligment' },
	//			{ name: "TotalSubject", displayName: "TotalSubject", minWidth: 100, headerCellClass: 'headerAligment' },
	//			{ name: "SubjectDetails", displayName: "SubjectDetails", minWidth: 240, headerCellClass: 'headerAligment' },
	//			{ name: "SubjectCodeDetails", displayName: "SubjectCodeDetails", minWidth: 240, headerCellClass: 'headerAligment' },
	//			{ name: "SubjectDetailsWExamDate", displayName: "SubjectDetailsWExamDate", minWidth: 240, headerCellClass: 'headerAligment' },
	//			{ name: "SubjectDetailsWExamDateTime", displayName: "SubjectDetailsWExamDateTime", minWidth: 240, headerCellClass: 'headerAligment' },


	//		],
	//		//   rowTemplate: rowTemplate(),
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
	//		exporterExcelFilename: 'enqSummary1.xlsx',
	//		exporterExcelSheetName: 'enqSummary1',
	//		onRegisterApi: function (gridApi) {
	//			$scope.gridApi1 = gridApi;
	//		}
	//	};

	//};

	$scope.UpdateStartEndTimeSubjectwise = function () {
		$timeout(function () {
			angular.forEach($scope.newExamSchedule.ExamScheduleDetailsColl, function (esd) {
				if (!esd.StartTime_TMP)
					esd.StartTime_TMP = $scope.newExamSchedule.StartTime_TMP;

				if (!esd.EndTime_TMP)
					esd.EndTime_TMP = $scope.newExamSchedule.EndTime_TMP;
			});
		});
	};

	//add and del
	$scope.AddExamScheduleDetails = function (ind) {
		if ($scope.newExamSchedule.ExamScheduleDetailsColl) {
			var detDate = null;

			if ($scope.newExamSchedule.ExamScheduleDetailsColl.length > ind)
				detDate = $scope.newExamSchedule.ExamScheduleDetailsColl[ind].ExamDateDet;


			if ($scope.newExamSchedule.ExamScheduleDetailsColl.length > ind + 1) {

				$scope.newExamSchedule.ExamScheduleDetailsColl.splice(ind + 1, 0, {
					SubjectId: null,
					PaperType: 3,
					ExamDate: (detDate && detDate.dateAD ? new Date(detDate.dateAD).addDays(1) : null),
					ExamDate_TMP: (detDate && detDate.dateAD ? new Date(detDate.dateAD).addDays(1) : null),
					StartTime: $scope.newExamSchedule.StartTime_TMP,
					EndTime: $scope.newExamSchedule.EndTime_TMP,
					StartTime_TMP: $scope.newExamSchedule.StartTime_TMP,
					EndTime_TMP: $scope.newExamSchedule.EndTime_TMP,
					Remarks: ''
				})
			} else {
				$scope.newExamSchedule.ExamScheduleDetailsColl.push({
					SubjectId: null,
					PaperType: 3,
					ExamDate: (detDate && detDate.dateAD ? new Date(detDate.dateAD).addDays(1) : null),
					ExamDate_TMP: (detDate && detDate.dateAD ? new Date(detDate.dateAD).addDays(1) : null),
					StartTime: $scope.newExamSchedule.StartTime_TMP,
					EndTime: $scope.newExamSchedule.EndTime_TMP,
					StartTime_TMP: $scope.newExamSchedule.StartTime_TMP,
					EndTime_TMP: $scope.newExamSchedule.EndTime_TMP,
					Remarks: ''
				})
			}
		}
	};
	$scope.delExamScheduleDetails = function (ind) {
		if ($scope.newExamSchedule.ExamScheduleDetailsColl) {
			if ($scope.newExamSchedule.ExamScheduleDetailsColl.length > 1) {
				$scope.newExamSchedule.ExamScheduleDetailsColl.splice(ind, 1);
			}
		}
	};


	$scope.ViewExamScheduleStatusDetails = function (ind) {
		if ($scope.newExamScheduleStatus.ExamScheduleStatusDetailsColl) {
			if ($scope.newExamScheduleStatus.ExamScheduleStatusDetailsColl.length > ind + 1) {
				$scope.newExamScheduleStatus.ExamScheduleStatusDetailsColl.splice(ind + 1, 0, {
					Sno: '',
					ClassSection: '',
					ExamDate: '',
					ExamTime: '',

				})
			} else {
				$scope.newAddExamScheduleStatus.AddExamScheduleStatusDetailsColl.push({
					Sno: '',
					ClassSection: '',
					ExamDate: '',
					ExamTime: '',
				})
			}
		}
	};

	$scope.CreateExamScheduleStatusDetails = function (ind) {
		if ($scope.newExamScheduleStatusPending.ExamScheduleStatusPendingDetailsColl) {
			if ($scope.newExamScheduleStatusPending.ExamScheduleStatusPendingDetailsColl.length > ind + 1) {
				$scope.newExamScheduleStatusPending.ExamScheduleStatusPendingDetailsColl.splice(ind + 1, 0, {
					Sno: '',
					ClassSection: '',
				})
			} else {
				$scope.newAddExamScheduleStatusPending.AddExamScheduleStatusPendingDetailsColl.push({
					Sno: '',
					ClassSection: '',
				})
			}
		}
	}

	$scope.LoadData = function () {
		$scope.entity = {
			AdmitCard: 358
		};

		$('.select2').select2();
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
				$scope.SelectedClassSemesterList = [];
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
				$scope.SelectedClassClassYearList = [];
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();
		$scope.StudentSearchOptions = glbS.getStudentSearchOptions();

		$scope.ClassSection = [];

		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SectionList = [];
		glbS.getSectionList().then(function (res) {
			$scope.SectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		$scope.AllSubjectList = [];
		glbS.getSubjectList().then(function (res) {
			$scope.AllSubjectList = res.data.Data;
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.currentPages = {
			ExamSchedule: 1,
			ExamScheduleStatus: 1,
			PrintAdmitCard: 1,
			ExamShift: 1,

		};

		$scope.searchData = {
			ExamSchedule: '',
			ExamScheduleStatus: '',
			PrintAdmitCard: '',
			ExamShift: '',
			CompletedStatus: '',
			PendingStatus: ''

		};

		$scope.perPage = {
			ExamSchedule: glbS.getPerPageRow(),
			ExamScheduleStatus: glbS.getPerPageRow(),
			PrintAdmitCard: glbS.getPerPageRow(),
			ExamShift: glbS.getPerPageRow(),

		};

		$scope.ExamTypeList = [];
		glbS.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		$scope.ReExamTypeList = [];
		glbS.getReExamTypeList().then(function (res) {
			$scope.ReExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.newExamSchedule = {
			ExamScheduleId: null,
			ExamScheduleDetailsColl: [],

			Mode: 'Save'
		};

		$scope.newExamScheduleStatus = {
			ExamScheduleStatusId: null,
			ExamScheduleStatusDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newExamScheduleStatus.ExamScheduleStatusDetailsColl.push({});


		$scope.newExamScheduleStatusPending = {
			ExamScheduleStatusPendingId: null,
			ExamScheduleStatusPendingDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newExamScheduleStatusPending.ExamScheduleStatusPendingDetailsColl.push({});

		$scope.newPrintAdmitCard = {
			PrintAdmitCardId: null,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			DuesAmt: 0
		}

		$scope.printExamSchedule =
		{
			ExamTypeId: 0,
			RptTranId: 0,
			SectionWise: false,
			InDetails: false,
			TemplatesColl: []
		};

		$scope.PaperTypeList = glbS.getPaperTypeList();
		//$scope.GetAllExamTypeList();
		//$scope.GetAllExamTypeGroupList();
		//$scope.GetAllPrintAdmitCardList();
		$scope.LoadReportTemplates();

		$scope.AllStudentColl = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllStudentForTran",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.AllStudentColl = res.data.Data;

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newExamShift = {
			ExamShiftId: null,
			Name: '',
			StartTime: '',
			EndTime: '',
			Mode: 'Save'
		};

		$scope.GetAllExamShiftList();
	}
	$scope.LoadReportTemplates = function () {

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.AdmitCard + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newPrintAdmitCard.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityAllClassExamSchedule + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.printExamSchedule.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.PrintExamSchedule = function () {
		if ($scope.printExamSchedule.ExamTypeId > 0 && $scope.printExamSchedule.RptTranId > 0) {

			var EntityId = entityAllClassExamSchedule;

			var rptPara = {
				ExamTypeId: $scope.printExamSchedule.ExamTypeId,
				SectionWise: $scope.printExamSchedule.SectionWise,
				rptTranId: $scope.printExamSchedule.RptTranId,
				ExamName: mx($scope.ExamTypeList).firstOrDefault(p1 => p1.ExamTypeId == $scope.printExamSchedule.ExamTypeId).Name,
				InDetails: $scope.printExamSchedule.InDetails
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptExamSchedule").src = '';
			document.getElementById("frmRptExamSchedule").style.width = '100%';
			document.getElementById("frmRptExamSchedule").style.height = '1300px';
			document.getElementById("frmRptExamSchedule").style.visibility = 'visible';
			document.getElementById("frmRptExamSchedule").src = base_url + "Exam/Transaction/RdlAllClassExamSchedule?" + paraQuery;
		}

	};

	$scope.LoadClassWiseSemesterYear1 = function (classId, data) {

		$scope.SelectedClassClassYearList1 = [];
		$scope.SelectedClassSemesterList1 = [];
		$scope.SelectedClass1 = mx($scope.ClassSection.ClassList).firstOrDefault(p1 => p1.ClassId == classId);

		if ($scope.SelectedClass1) {
			var semQry = mx($scope.SelectedClass1.ClassSemesterIdColl);
			var cyQry = mx($scope.SelectedClass1.ClassYearIdColl);

			angular.forEach($scope.SemesterList, function (sem) {
				if (semQry.contains(sem.id)) {
					$scope.SelectedClassSemesterList1.push({
						id: sem.id,
						text: sem.text,
						SemesterId: sem.id,
						Name: sem.Name
					});
				}
			});

			angular.forEach($scope.ClassYearList, function (sem) {
				if (cyQry.contains(sem.id)) {
					$scope.SelectedClassClassYearList1.push({
						id: sem.id,
						text: sem.text,
						ClassYearId: sem.id,
						Name: sem.Name
					});
				}
			});
		}

	};

	$scope.PrintNormalAdmitCard = function (fromInd) {

		if ($scope.newPrintAdmitCard.SelectedClass) {
			$scope.GetClasswiseMinDues($scope.newPrintAdmitCard.SelectedClass.ClassId);
		}

		// Load Class Wise Year and Semester On Class Selection Changed
		if (fromInd == 2 && $scope.newPrintAdmitCard.SelectedClass) {
			$scope.newPrintAdmitCard.SemesterId = null;
			$scope.newPrintAdmitCard.ClassYearId = null;
			$scope.LoadClassWiseSemesterYear1($scope.newPrintAdmitCard.SelectedClass.ClassId, $scope.newPrintAdmitCard);
		}

		if ($scope.newPrintAdmitCard.SelectedClass && $scope.newPrintAdmitCard.ExamTypeId && $scope.newPrintAdmitCard.RptTranId) {
			var EntityId = $scope.entity.AdmitCard;

			if ($scope.newPrintAdmitCard.RptTranId > 0) {
				var examN = mx($scope.ExamTypeList).firstOrDefault(p1 => p1.ExamTypeId == $scope.newPrintAdmitCard.ExamTypeId);
				var rptPara = {
					rpttranid: $scope.newPrintAdmitCard.RptTranId,
					istransaction: false,
					entityid: EntityId,
					ClassId: $scope.newPrintAdmitCard.SelectedClass.ClassId,
					SectionId: $scope.newPrintAdmitCard.SelectedClass.SectionId,
					ExamTypeId: $scope.newPrintAdmitCard.ExamTypeId,
					SemesterId: ($scope.newPrintAdmitCard.SemesterId > 0 ? $scope.newPrintAdmitCard.SemesterId : 0),
					ClassYearId: ($scope.newPrintAdmitCard.ClassYearId > 0 ? $scope.newPrintAdmitCard.ClassYearId : 0),
					BatchId: ($scope.newPrintAdmitCard.BatchId > 0 ? $scope.newPrintAdmitCard.BatchId : 0),
					StudentId: 0,
					DuesAmt: $scope.newPrintAdmitCard.DuesAmt,
					StudentIdColl: ''
				};
				var paraQuery = param(rptPara);
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';
				document.getElementById("frmRptTabulation").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
				document.body.style.cursor = 'default';
			} else {
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';
				document.body.style.cursor = 'default';
			}
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptTabulation").src = '';
			document.body.style.cursor = 'default';
		}
	};

	$scope.PrintStudentAdmitCard = function () {
		if ($scope.newPrintAdmitCard.ExamTypeId && $scope.newPrintAdmitCard.RptTranId) {
			var EntityId = $scope.entity.AdmitCard;

			if ($scope.newPrintAdmitCard.RptTranId > 0) {
				var examN = mx($scope.ExamTypeList).firstOrDefault(p1 => p1.ExamTypeId == $scope.newPrintAdmitCard.ExamTypeId);
				var rptPara = {
					rpttranid: $scope.newPrintAdmitCard.RptTranId,
					istransaction: false,
					entityid: EntityId,
					ClassId: 0,
					SectionId: 0,
					ExamTypeId: $scope.newPrintAdmitCard.ExamTypeId,
					StudentId: 0,// $scope.newPrintAdmitCard.StudentId,
					DuesAmt: $scope.newPrintAdmitCard.DuesAmt,
					SemesterId: 0,
					ClassYearId: 0,
					BatchId: 0,
					StudentIdColl: $scope.newPrintAdmitCard.StudentIdColl
				};
				var paraQuery = param(rptPara);
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';
				document.getElementById("frmRptTabulation").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
				document.body.style.cursor = 'default';
			} else {
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';
				document.body.style.cursor = 'default';
			}
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptTabulation").src = '';
			document.body.style.cursor = 'default';
		}

	};

	$scope.ClearExamSchedule = function () {
		$scope.newExamSchedule = {
			ExamScheduleId: null,
			ExamScheduleDetailsColl: [],
			Mode: 'Save'
		};
	}
	$scope.ClearExamScheduleStatus = function () {
		$scope.newExamScheduleStatus = {
			ExamScheduleStatusId: null,

			ExamScheduleStatusDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newExamScheduleStatus.ExamScheduleStatusDetailsColl.push({});

	}

	//************************* Exam Schedule *********************************

	$scope.IsValidExamSchedule = function () {
		//if ($scope.newExamType.Name.isEmpty()) {
		//	Swal.fire('Please ! Enter Name');
		//	return false;
		//}

		return true;
	}

	$scope.LoadClassWiseSemesterYear = function (classId, data) {
		$scope.SelectedClassClassYearList = [];
		$scope.SelectedClassSemesterList = [];
		$scope.SelectedClass = mx($scope.ClassSection.ClassList).firstOrDefault(p1 => p1.ClassId == classId);

		if ($scope.SelectedClass) {
			var semQry = mx($scope.SelectedClass.ClassSemesterIdColl);
			var cyQry = mx($scope.SelectedClass.ClassYearIdColl);

			angular.forEach($scope.SemesterList, function (sem) {
				if (semQry.contains(sem.id)) {
					$scope.SelectedClassSemesterList.push({
						id: sem.id,
						text: sem.text,
						SemesterId: sem.id,
						Name: sem.Name
					});
				}
			});

			angular.forEach($scope.ClassYearList, function (sem) {
				if (cyQry.contains(sem.id)) {
					$scope.SelectedClassClassYearList.push({
						id: sem.id,
						text: sem.text,
						ClassYearId: sem.id,
						Name: sem.Name
					});
				}
			});
		}

	};

	$scope.GetClassWiseSubMap = function (fromInd) {
		$scope.newExamSchedule.SubjectList = [];
		$scope.newExamSchedule.ExamScheduleDetailsColl = [];

		if ($scope.newExamSchedule.ClassId && $scope.newExamSchedule.ClassId > 0) {
			var para = {
				ClassId: $scope.newExamSchedule.ClassId,
				SectionIdColl: ($scope.newExamSchedule.SectionId ? $scope.newExamSchedule.SectionId.toString() : ''),
				SemesterId: $scope.newExamSchedule.SemesterId,
				ClassYearId: $scope.newExamSchedule.ClassYearId,
				BatchId: $scope.newExamSchedule.BatchId
			};

			// Load Class Wise Year and Semester On Class Selection Changed
			if (fromInd == 2) {
				$scope.newExamSchedule.SemesterId = null;
				$scope.newExamSchedule.ClassYearId = null;
				$scope.LoadClassWiseSemesterYear($scope.newExamSchedule.ClassId, $scope.newExamSchedule);
			}

			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetSubjectMappingClassWise",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var SubjectMappingColl = res.data.Data;

					if (SubjectMappingColl.length == 0) {

						$scope.AcademicConfig.ActiveBatch == true
						$scope.AcademicConfig.ActiveClassYear == true
						$scope.AcademicConfig.ActiveSemester == true

						if ($scope.AcademicConfig.ActiveBatch == false && $scope.AcademicConfig.ActiveClassYear == false && $scope.AcademicConfig.ActiveSemester == false) {
							Swal.fire('Subject Mapping Not Found');
						}
						else {

							if (para.ClassId > 0) {
								if ($scope.AcademicConfig.ActiveBatch == true && para.BatchId > 0) {
									if (para.SemesterId > 0 || para.ClassYearId > 0) {
										Swal.fire('Subject Mapping Not Found');
									}
								}
							}
						}
					}
					else if (SubjectMappingColl.length > 0) {
						angular.forEach(SubjectMappingColl, function (sm) {
							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								$scope.newExamSchedule.SubjectList.push(subDet);
							}
						});

						if ($scope.newExamSchedule.ExamTypeId && $scope.newExamSchedule.ExamTypeId > 0) {

							var para1 = {
								ClassId: para.ClassId,
								SectionIdColl: para.SectionIdColl,
								ExamTypeId: $scope.newExamSchedule.ExamTypeId,
								SemesterId: para.SemesterId,
								ClassYearId: para.ClassYearId,
								BatchId: para.BatchId
							};
							$http({
								method: 'POST',
								url: base_url + "Exam/Transaction/GetExamScheduleById",
								dataSchedule: "json",
								data: JSON.stringify(para1)
							}).then(function (res1) {

								if (res1.data.IsSuccess && res1.data.Data) {
									var schedule = res1.data.Data;

									if (schedule && schedule.StartDate && schedule.StartDate != null) {

										$scope.newExamSchedule.StartDate = new Date(schedule.StartDate);
										$scope.newExamSchedule.EndDate = new Date(schedule.EndDate);
										$scope.newExamSchedule.StartTime = new Date(schedule.StartTime);
										$scope.newExamSchedule.EndTime = new Date(schedule.EndTime);

										$scope.newExamSchedule.StartDate_TMP = new Date(schedule.StartDate);
										$scope.newExamSchedule.EndDate_TMP = new Date(schedule.EndDate);
										$scope.newExamSchedule.StartTime_TMP = new Date(schedule.StartTime);
										$scope.newExamSchedule.EndTime_TMP = new Date(schedule.EndTime);

										angular.forEach(schedule.ExamScheduleDetailsColl, function (sch) {
											$scope.newExamSchedule.ExamScheduleDetailsColl.push(
												{
													SubjectId: sch.SubjectId,
													PaperTypeId: sch.PaperTypeId,
													ExamDate: new Date(sch.ExamDate),
													StartTime: new Date(sch.StartTime),
													EndTime: new Date(sch.EndTime),
													ExamDate_TMP: new Date(sch.ExamDate),
													StartTime_TMP: new Date(sch.StartTime),
													EndTime_TMP: new Date(sch.EndTime),
													ExamShiftId: sch.ExamShiftId,
													Remarks: sch.Remarks
												}
											);
										});

									} else
										$scope.AddExamScheduleDetails(0);

								} else {
									Swal.fire(res.data.ResponseMSG);
								}
							}, function (reason) {
								Swal.fire('Failed' + reason);
							});
						}
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	};

	$scope.ChangeExamShift = function (shift) {

		var sch = mx($scope.ExamShiftList).firstOrDefault(p1 => p1.ExamShiftId == shift.ExamShiftId);
		if (sch) {
			shift.StartTime = new Date("2000-01-01 " + sch.StartTime);
			shift.EndTime = new Date("2000-01-01 " + sch.EndTime);
			shift.StartTime_TMP = new Date("2000-01-01 " + sch.StartTime);
			shift.EndTime_TMP = new Date("2000-01-01 " + sch.EndTime);
		}
	};
	$scope.SaveUpdateExamSchedule = function () {
		if ($scope.IsValidExamSchedule() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamSchedule.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamSchedule();
					}
				});
			} else
				$scope.CallSaveUpdateExamSchedule();

		}
	};

	$scope.CallSaveUpdateExamSchedule = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newExamSchedule.StartDateDet) {
			$scope.newExamSchedule.StartDate = $filter('date')(new Date($scope.newExamSchedule.StartDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newExamSchedule.StartDate = null;

		if ($scope.newExamSchedule.EndDateDet) {
			$scope.newExamSchedule.EndDate = $filter('date')(new Date($scope.newExamSchedule.EndDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newExamSchedule.EndDate = null;

		if ($scope.newExamSchedule.StartTime_TMP)
			$scope.newExamSchedule.StartTime = $scope.newExamSchedule.StartTime_TMP.toLocaleString();
		else
			$scope.newExamSchedule.StartTime = null;

		if ($scope.newExamSchedule.EndTime_TMP)
			$scope.newExamSchedule.EndTime = $scope.newExamSchedule.EndTime_TMP.toLocaleString();
		else
			$scope.newExamSchedule.EndTime = null;

		if ($scope.newExamSchedule.SectionId)
			$scope.newExamSchedule.SectionIdColl = $scope.newExamSchedule.SectionId;
		else
			$scope.newExamSchedule.SectionIdColl = [];

		$scope.newExamSchedule.FromSectionIdColl = ($scope.newExamSchedule.SectionId && $scope.newExamSchedule.SectionId.length > 0 ? $scope.newExamSchedule.SectionId.toString() : '');
		$scope.newExamSchedule.ToClassIdColl = ($scope.newExamSchedule.ToClassId_TMP && $scope.newExamSchedule.ToClassId_TMP.length > 0 ? $scope.newExamSchedule.ToClassId_TMP.toString() : '');
		$scope.newExamSchedule.ToSectionIdColl = ($scope.newExamSchedule.ToSectionId_TMP && $scope.newExamSchedule.ToSectionId_TMP.length > 0 ? $scope.newExamSchedule.ToSectionId_TMP.toString() : '');

		angular.forEach($scope.newExamSchedule.ExamScheduleDetailsColl, function (det) {

			if (det.ExamDateDet)
				det.ExamDate = $filter('date')(new Date(det.ExamDateDet.dateAD), 'yyyy-MM-dd');
			else
				det.ExamDate = null;

			if (det.StartTime_TMP)
				det.StartTime = det.StartTime_TMP.toLocaleString();
			else
				det.StartTime = null;

			if (det.EndTime_TMP)
				det.EndTime = det.EndTime_TMP.toLocaleString();
			else
				det.EndTime = null;
		});

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamSchedule",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamSchedule }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true)
				$scope.ClearExamSchedule();
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.DelExamScheduleById = function () {

		if ($scope.newExamSchedule.ClassId && $scope.newExamSchedule.ExamTypeId) {
			Swal.fire({
				title: 'Do you want to delete the selected data?',
				showCancelButton: true,
				confirmButtonText: 'Delete',
			}).then((result) => {
				/* Read more about isConfirmed, isDenied below */
				if (result.isConfirmed) {
					$scope.loadingstatus = "running";
					showPleaseWait();

					var para = {
						ClassId: $scope.newExamSchedule.ClassId,
						SectionIdColl: ($scope.newExamSchedule.SectionId ? $scope.newExamSchedule.SectionId.toString() : ''),
						ExamTypeId: $scope.newExamSchedule.ExamTypeId
					};

					$http({
						method: 'POST',
						url: base_url + "Exam/Transaction/DelExamSchedule",
						dataSchedule: "json",
						data: JSON.stringify(para)
					}).then(function (res) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						Swal.fire(res.data.ResponseMSG);

						if (res.data.IsSuccess == true)
							$scope.ClearExamSchedule();

					}, function (reason) {
						Swal.fire('Failed' + reason);
					});
				}
			});
		}

	};

	//************************* Exam Schedule Status *********************************

	$scope.IsValidExamScheduleStatus = function () {
		if ($scope.newExamScheduleStatus.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateExamScheduleStatus = function () {
		if ($scope.IsValidExamScheduleStatus() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamScheduleStatus.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamScheduleStatus();
					}
				});
			} else
				$scope.CallSaveUpdateExamScheduleStatus();

		}
	};


	$scope.CallSaveUpdateExamScheduleStatus = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamScheduleStatus",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamScheduleStatus }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExamScheduleStatus();
				$scope.GetAllExamScheduleStatusList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllExamScheduleStatusList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamScheduleStatusList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamScheduleStatusList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamScheduleStatusList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExamScheduleStatusById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ExamScheduleStatusId: refData.ExamScheduleStatusId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamScheduleStatusById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExamScheduleStatus = res.data.Data;
				$scope.newExamScheduleStatus.Mode = 'Modify';

				//document.getElementById('exam-type-group-section').style.display = "none";
				//document.getElementById('exam-type-group-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExamScheduleStatusById = function (refData) {

		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ExamScheduleStatusId: refData.ExamScheduleStatusId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelExamScheduleStatus",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExamScheduleStatusList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//*************************Print admit card *********************************

	$scope.IsValidPrintAdmitCard = function () {
		if ($scope.newPrintAdmitCard.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdatePrintAdmitCard = function () {
		if ($scope.IsValidPrintAdmitCard() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newPrintAdmitCard.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdatePrintAdmitCard();
					}
				});
			} else
				$scope.CallSaveUpdatePrintAdmitCard();

		}
	};

	$scope.CallSaveUpdatePrintAdmitCard = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SavePrintAdmitCard",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newPrintAdmitCard }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearPrintAdmitCard();
				$scope.GetAllPrintAdmitCardList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetExamScheduleStatus = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PendingExamScheduleList = [];
		$scope.CompletedExamScheduleList = [];

		if ($scope.newExamScheduleStatus.ExamTypeId > 0) {

			var para = {
				ExamTypeId: $scope.newExamScheduleStatus.ExamTypeId
			};
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetExamScheduleStatus",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					angular.forEach(res.data.Data, function (rd) {

						if (rd.IsPending == true)
							$scope.PendingExamScheduleList.push(rd);
						else
							$scope.CompletedExamScheduleList.push(rd);
					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}


	}

	$scope.GetAllPrintAdmitCardList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PrintAdmitCardList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllPrintAdmitCardList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PrintAdmitCardList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetPrintAdmitCardById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			PrintAdmitCardId: refData.PrintAdmitCardId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetPrintAdmitCardById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newPrintAdmitCard = res.data.Data;
				$scope.newPrintAdmitCard.Mode = 'Modify';

				//document.getElementById('parent-exam-type-section').style.display = "none";
				//document.getElementById('parent-exam-type-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelPrintAdmitCardById = function (refData) {

		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					PrintAdmitCardId: refData.PrintAdmitCardId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelPrintAdmitCard",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllPrintAdmitCardList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};


	//$scope.GetStudentListForExam = function ()
	//{
	//	if (($scope.newStudentList.SelectedClass || $scope.newStudentList.SelectedClass==0) && $scope.newStudentList.ExamTypeId) {
	//		$scope.loadingstatus = "running";
	//		showPleaseWait();

	//		var para = {
	//			ClassId: ($scope.newStudentList.SelectedClass == 0 ? 0 : $scope.newStudentList.SelectedClass.ClassId),
	//			SectionId: $scope.newStudentList.SelectedClass == 0 ? 0 :  $scope.newStudentList.SelectedClass.SectionId,
	//			ExamTypeId: $scope.newStudentList.ExamTypeId,
	//			SubjectId: $scope.newStudentList.SubjectId,
	//			FilterSection: $scope.newStudentList.SelectedClass == 0 ? false :  $scope.newStudentList.SelectedClass.FilterSection
	//           }
	//		$http({
	//			method: 'POST',
	//			url: base_url + "Exam/Report/GetStudentListForExam",
	//			dataType: "json",
	//			data:JSON.stringify(para)
	//		}).then(function (res) {
	//			hidePleaseWait();
	//			$scope.loadingstatus = "stop";
	//			if (res.data.IsSuccess && res.data.Data)
	//			{
	//				$scope.gridOptions.data = res.data.Data;
	//			} else {
	//				Swal.fire(res.data.ResponseMSG);
	//			}

	//		}, function (reason) {
	//			Swal.fire('Failed' + reason);
	//		});
	//       }		
	//}

	//$scope.PrintStudentListForExam = function () {
	//	$http({
	//		method: 'GET',
	//		url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityStudentForExam + "&voucherId=0&isTran=false",
	//		dataType: "json"
	//	}).then(function (res) {
	//		if (res.data.IsSuccess && res.data.Data) {
	//			var templatesColl = res.data.Data;
	//			if (templatesColl && templatesColl.length > 0) {
	//				var templatesName = [];
	//				var sno = 1;
	//				angular.forEach(templatesColl, function (tc) {
	//					templatesName.push(sno + '-' + tc.ReportName);
	//					sno++;
	//				});

	//				var print = false;

	//				var rptTranId = 0;
	//				if (templatesColl.length == 1)
	//					rptTranId = templatesColl[0].RptTranId;
	//				else {
	//					Swal.fire({
	//						title: 'Report Templates For Print',
	//						input: 'select',
	//						inputOptions: templatesName,
	//						inputPlaceholder: 'Select a template',
	//						showCancelButton: true,
	//						inputValidator: (value) => {
	//							return new Promise((resolve) => {
	//								if (value >= 0) {
	//									resolve()
	//									rptTranId = templatesColl[value].RptTranId;

	//									if (rptTranId > 0) {
	//										var dataColl = [];
	//										angular.forEach($scope.gridApi.core.getVisibleRows($scope.gridApi.grid), function (fr) {
	//											dataColl.push(fr.entity);
	//										});
	//										print = true;
	//										$http({
	//											method: 'POST',
	//											url: base_url + "Exam/Report/PrintStudentListForExam",
	//											headers: { 'Content-Type': undefined },

	//											transformRequest: function (data) {

	//												var formData = new FormData();
	//												formData.append("jsonData", angular.toJson(data.jsonData));

	//												return formData;
	//											},
	//											data: { jsonData: dataColl }
	//										}).then(function (res) {

	//											$scope.loadingstatus = "stop";
	//											hidePleaseWait();
	//											if (res.data.IsSuccess && res.data.Data) {


	//												var rptPara = {
	//													ClassSectionName: $scope.newStudentList.SelectedClass.text,
	//													ExamName: mx($scope.ExamTypeList).where(p1 => p1.id == $scope.newStudentList.ExamTypeId).firstOrDefault().Name
	//												};
	//												var paraQuery = param(rptPara);

	//												document.body.style.cursor = 'wait';
	//												document.getElementById("frmRpt").src = '';
	//												document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentForExam + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId + "&" + paraQuery;
	//												document.body.style.cursor = 'default';
	//												$('#FrmPrintReport').modal('show');

	//											} else
	//												Swal.fire('No Templates found for print');

	//										}, function (errormessage) {
	//											hidePleaseWait();
	//											$scope.loadingstatus = "stop";
	//											Swal.fire(errormessage);
	//										});

	//									}

	//								} else {
	//									resolve('You need to select:)')
	//								}
	//							})
	//						}
	//					})
	//				}

	//				if (rptTranId > 0 && print == false) {
	//					var dataColl = [];
	//					angular.forEach($scope.gridApi.core.getVisibleRows($scope.gridApi.grid), function (fr) {
	//						dataColl.push(fr.entity);
	//					});
	//					print = true;

	//					$http({
	//						method: 'POST',
	//						url: base_url + "Exam/Report/PrintStudentListForExam",
	//						headers: { 'Content-Type': undefined },

	//						transformRequest: function (data) {

	//							var formData = new FormData();
	//							formData.append("jsonData", angular.toJson(data.jsonData));

	//							return formData;
	//						},
	//						data: { jsonData: dataColl }
	//					}).then(function (res) {

	//						$scope.loadingstatus = "stop";
	//						hidePleaseWait();
	//						if (res.data.IsSuccess && res.data.Data) {


	//							var rptPara = {
	//								ClassSectionName: $scope.newStudentList.SelectedClass.text,
	//								ExamName: mx($scope.ExamTypeList).where(p1 => p1.id == $scope.newStudentList.ExamTypeId).firstOrDefault().Name
	//							};
	//							var paraQuery = param(rptPara);

	//							document.body.style.cursor = 'wait';
	//							document.getElementById("frmRpt").src = '';
	//							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentForExam + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId + "&" + paraQuery;
	//							document.body.style.cursor = 'default';
	//							$('#FrmPrintReport').modal('show');

	//						} else
	//							Swal.fire('No Templates found for print');

	//					}, function (errormessage) {
	//						hidePleaseWait();
	//						$scope.loadingstatus = "stop";
	//						Swal.fire(errormessage);
	//					});

	//				}

	//			} else
	//				Swal.fire('No Templates found for print');
	//		}
	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//};


	//$scope.GetStudentListForReExam = function () {
	//	if (($scope.newReStudentList.SelectedClass || $scope.newReStudentList.SelectedClass == 0) && ($scope.newReStudentList.ExamTypeId || $scope.newReStudentList.ExamTypeId > 0) && ($scope.newReStudentList.ReExamTypeId || $scope.newReStudentList.ReExamTypeId > 0)) {
	//		$scope.loadingstatus = "running";
	//		showPleaseWait();

	//		var para = {
	//			ClassId: ($scope.newReStudentList.SelectedClass == 0 ? 0 : $scope.newReStudentList.SelectedClass.ClassId),
	//			SectionId: $scope.newReStudentList.SelectedClass == 0 ? 0 : $scope.newReStudentList.SelectedClass.SectionId,
	//			ExamTypeId: $scope.newReStudentList.ExamTypeId,
	//			ReExamTypeId: $scope.newReStudentList.ReExamTypeId,
	//			SubjectId: $scope.newReStudentList.SubjectId,
	//		}
	//		$http({
	//			method: 'POST',
	//			url: base_url + "Exam/Report/GetStudentListForReExam",
	//			dataType: "json",
	//			data: JSON.stringify(para)
	//		}).then(function (res) {
	//			hidePleaseWait();
	//			$scope.loadingstatus = "stop";
	//			if (res.data.IsSuccess && res.data.Data) {
	//				$scope.gridOptions1.data = res.data.Data;
	//			} else {
	//				Swal.fire(res.data.ResponseMSG);
	//			}

	//		}, function (reason) {
	//			Swal.fire('Failed' + reason);
	//		});
	//	}
	//}

	//$scope.PrintStudentListForReExam = function () {
	//	$http({
	//		method: 'GET',
	//		url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityStudentForExam + "&voucherId=0&isTran=false",
	//		dataType: "json"
	//	}).then(function (res) {
	//		if (res.data.IsSuccess && res.data.Data) {
	//			var templatesColl = res.data.Data;
	//			if (templatesColl && templatesColl.length > 0) {
	//				var templatesName = [];
	//				var sno = 1;
	//				angular.forEach(templatesColl, function (tc) {
	//					templatesName.push(sno + '-' + tc.ReportName);
	//					sno++;
	//				});

	//				var print = false;

	//				var rptTranId = 0;
	//				if (templatesColl.length == 1)
	//					rptTranId = templatesColl[0].RptTranId;
	//				else {
	//					Swal.fire({
	//						title: 'Report Templates For Print',
	//						input: 'select',
	//						inputOptions: templatesName,
	//						inputPlaceholder: 'Select a template',
	//						showCancelButton: true,
	//						inputValidator: (value) => {
	//							return new Promise((resolve) => {
	//								if (value >= 0) {
	//									resolve()
	//									rptTranId = templatesColl[value].RptTranId;

	//									if (rptTranId > 0) {
	//										var dataColl = [];
	//										angular.forEach($scope.gridApi1.core.getVisibleRows($scope.gridApi.grid), function (fr) {
	//											dataColl.push(fr.entity);
	//										});
	//										print = true;
	//										$http({
	//											method: 'POST',
	//											url: base_url + "Exam/Report/PrintStudentListForExam",
	//											headers: { 'Content-Type': undefined },

	//											transformRequest: function (data) {

	//												var formData = new FormData();
	//												formData.append("jsonData", angular.toJson(data.jsonData));

	//												return formData;
	//											},
	//											data: { jsonData: dataColl }
	//										}).then(function (res) {

	//											$scope.loadingstatus = "stop";
	//											hidePleaseWait();
	//											if (res.data.IsSuccess && res.data.Data) {


	//												var rptPara = {
	//													ClassSectionName: $scope.newReStudentList.SelectedClass.text,
	//													ExamName: mx($scope.ExamTypeList).where(p1 => p1.id == $scope.newReStudentList.ExamTypeId).firstOrDefault().Name,
	//													ReExamName: mx($scope.ReExamTypeList).where(p1 => p1.id == $scope.newReStudentList.ReExamTypeId).firstOrDefault().Name
	//												};
	//												var paraQuery = param(rptPara);

	//												document.body.style.cursor = 'wait';
	//												document.getElementById("frmRpt").src = '';
	//												document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentForExam + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId + "&" + paraQuery;
	//												document.body.style.cursor = 'default';
	//												$('#FrmPrintReport').modal('show');

	//											} else
	//												Swal.fire('No Templates found for print');

	//										}, function (errormessage) {
	//											hidePleaseWait();
	//											$scope.loadingstatus = "stop";
	//											Swal.fire(errormessage);
	//										});

	//									}

	//								} else {
	//									resolve('You need to select:)')
	//								}
	//							})
	//						}
	//					})
	//				}

	//				if (rptTranId > 0 && print == false) {
	//					var dataColl = [];
	//					angular.forEach($scope.gridApi.core.getVisibleRows($scope.gridApi.grid), function (fr) {
	//						dataColl.push(fr.entity);
	//					});
	//					print = true;

	//					$http({
	//						method: 'POST',
	//						url: base_url + "Exam/Report/PrintStudentListForExam",
	//						headers: { 'Content-Type': undefined },

	//						transformRequest: function (data) {

	//							var formData = new FormData();
	//							formData.append("jsonData", angular.toJson(data.jsonData));

	//							return formData;
	//						},
	//						data: { jsonData: dataColl }
	//					}).then(function (res) {

	//						$scope.loadingstatus = "stop";
	//						hidePleaseWait();
	//						if (res.data.IsSuccess && res.data.Data) {


	//							var rptPara = {
	//								ClassSectionName: $scope.newReStudentList.SelectedClass.text,
	//								ExamName: mx($scope.ExamTypeList).where(p1 => p1.id == $scope.newReStudentList.ExamTypeId).firstOrDefault().Name,
	//								ReExamName: mx($scope.ReExamTypeList).where(p1 => p1.id == $scope.newReStudentList.ReExamTypeId).firstOrDefault().Name
	//							};
	//							var paraQuery = param(rptPara);

	//							document.body.style.cursor = 'wait';
	//							document.getElementById("frmRpt").src = '';
	//							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentForExam + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId + "&" + paraQuery;
	//							document.body.style.cursor = 'default';
	//							$('#FrmPrintReport').modal('show');

	//						} else
	//							Swal.fire('No Templates found for print');

	//					}, function (errormessage) {
	//						hidePleaseWait();
	//						$scope.loadingstatus = "stop";
	//						Swal.fire(errormessage);
	//					});

	//				}

	//			} else
	//				Swal.fire('No Templates found for print');
	//		}
	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//};


	//************************* Exam Shift *********************************

	$scope.IsValidExamShift = function () {
		if ($scope.newExamShift.Name.isEmpty()) {
			Swal.fire('Please ! Enter Exam Shift Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateExamShift = function () {
		if ($scope.IsValidExamShift() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamShift.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamShift();
					}
				});
			} else
				$scope.CallSaveUpdateExamShift();

		}
	};

	$scope.CallSaveUpdateExamShift = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();


		if ($scope.newExamShift.StartTime_TMP)
			$scope.newExamShift.StartTime = moment($scope.newExamShift.StartTime_TMP).format("HH:mm");
		else
			$scope.newExamShift.EndTime = null;

		if ($scope.newExamShift.EndTime_TMP)
			$scope.newExamShift.EndTime = moment($scope.newExamShift.EndTime_TMP).format("HH:mm");
		else
			$scope.newExamShift.EndTime = null;

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamShift",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamShift }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExamShift();
				$scope.GetAllExamShiftList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllExamShiftList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamShiftList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamShiftList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamShiftList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExamShiftById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ExamShiftId: refData.ExamShiftId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamShiftById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExamShift = res.data.Data;
				$scope.newExamShift.Mode = 'Modify';

				if ($scope.newExamShift.StartTime)
					$scope.newExamShift.StartTime_TMP = new Date(moment().format("yyyy-MM-DD") + " " + $scope.newExamShift.StartTime);

				if ($scope.newExamShift.EndTime)
					$scope.newExamShift.EndTime_TMP = new Date(moment().format("yyyy-MM-DD") + " " + $scope.newExamShift.EndTime);

				document.getElementById('examshift-section').style.display = "none";
				document.getElementById('examshift-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExamShiftById = function (refData) {

		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ExamShiftId: refData.ExamShiftId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelExamShift",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExamShiftList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//Added for getting the classwise min dues
	$scope.GetClasswiseMinDues = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			ClassId: $scope.newPrintAdmitCard.SelectedClass.ClassId
		};
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetClassWiseMinDues",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newPrintAdmitCard.DuesAmt = res.data.Data.MinDues;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.validateDates = function (changedField) {
		if (!$scope.newExamSchedule.StartDateDet || !$scope.newExamSchedule.EndDateDet ||
			!$scope.newExamSchedule.StartDateDet.dateAD || !$scope.newExamSchedule.EndDateDet.dateAD) {
			return true;
		}
		var StartDate = $filter('date')(new Date($scope.newExamSchedule.StartDateDet.dateAD), 'yyyy-MM-dd')
		var EndDate = $filter('date')(new Date($scope.newExamSchedule.EndDateDet.dateAD), 'yyyy-MM-dd')
		if (!StartDate || !EndDate) return true;
		if (StartDate > EndDate) {
			if (changedField === 'StartDate') {
				Swal.fire({
					icon: 'warning',
					text: 'Start Date cannot be After End Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newExamSchedule.StartDate_TMP = new Date();
						$scope.newExamSchedule.StartDateDet = new Date();
					});
				});
			} else if (changedField === 'EndDate') {
				Swal.fire({
					icon: 'warning',
					text: 'End Date cannot be Before Start Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newExamSchedule.EndDate_TMP = new Date();
						$scope.newExamSchedule.EndDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};
});