
$(document).ready(function () {

	$(document).on('keyup', '.serialCAS', function (e) {

		if (e.which == 13) {
			var checkBoxChecked = true;
			if (checkBoxChecked == true) {
				var $this = $(this);
				var $td = $this.closest('td');
				var $row = $td.closest('tr');
				var $rows = $row.parent();
				var column = $td.index();

				while ($td.length) {

					$row = $row.next('tr');

					if ($row.length == 0) {

						$row = $rows.children().first();
						// $row = $rows.children().get(2);

						column++;
					}

					$td = $row.children().eq(column);
					var $input = $td.find('.serialCAS');
					if ($input.length) {
						$input.focus();
						break;
					}
				}
			} else {

				var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
				if (key == 13) {
					e.preventDefault();
					var inputs = $(this).closest('form').find(':input:visible');
					inputs.eq(inputs.index(this) + 1).focus();
				}
			}

		}
	});


});


app.controller('ClassTestController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {
	$scope.Title = 'Class Test';
	var gSrv = GlobalServices;
	OnClickDefault();
	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			ClassTestMarkEntry: 1,
			ClassSummary: 1


		};

		$scope.searchData = {
			ClassTestMarkEntry: '',
			ClassSummary: '',
			ClassSummaryDetail: ''

		};

		$scope.perPage = {
			ClassTestMarkEntry: GlobalServices.getPerPageRow(),
			ClassSummary: GlobalServices.getPerPageRow()
		};


		//$scope.ClassList = [];
		//gSrv.getClassSectionList().then(function (res) {
		//	$scope.ClassList = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		$scope.SubjectList1 = {};
		gSrv.getSubjectList().then(function (res) {
			$scope.SubjectList1 = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassSectionList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

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
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["Faculty"], false);
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
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["Level"], false);
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
				$scope.gridOptions.columnApi.setColumnsVisible(["Semester"], false);
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["Semester"], false);
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
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["Batch"], false);
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
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["ClassYear"], false);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$rootScope.ChangeLanguage();

		$scope.newFilter = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date()

		};

		$scope.sortKeys = {
			MarkEntry: '',
		};

		$scope.reverses = {
			MarkEntry: false,
		}


		$scope.newClassMarkEntry = {
			TranId: null,
			AcademicYearId: null,
			ClassId: null,
			SectionId: null,
			EmployeeId: null,
			SubjectId: null,
			LessonSNo: null,
			StudentId: null,
			BatchId: null,
			SemesterId: null,
			ClassYearId: null,
			TestDate: null,
			FullMarks: null,
			PassMarks: null,
			ObtMarks: null,
			Remarks: '',
			TestDate_TMP:new Date(),
			Mode: 'Save'
		};

		$scope.newSummary = {
			SummaryId: null,

			Mode: 'Save'
		};


	}
	$scope.ClearClassMarkEntry = function () {
		$scope.newClassMarkEntry = {
			TranId: null,
			AcademicYearId: null,
			ClassId: null,
			SectionId: null,
			EmployeeId: null,
			SubjectId: null,
			LessonSNo: null,
			StudentId: null,
			BatchId: null,
			SemesterId: null,
			ClassYearId: null,
			TestDate: null,
			FullMarks: null,
			PassMarks: null,
			ObtMarks: null,
			Remarks: '',
			TestDate_TMP: new Date(),
			Mode: 'Save'
		};
		$scope.StudentsDetailsClassWiseList = [];
	}
	$scope.ClearSummary = function () {
		$scope.newSummary = {
			SummaryId: null,

			Mode: 'Save'
		};
	}

	function OnClickDefault() {
		//FOr test summary
		document.getElementById('ClassSummaryForm').style.display = "none";

		document.getElementById('ViewDetails').onclick = function () {
			document.getElementById('ClassSummarySection').style.display = "none";
			document.getElementById('ClassSummaryForm').style.display = "block";
		}

		document.getElementById('back-to-list-ClassSummary').onclick = function () {
			document.getElementById('ClassSummaryForm').style.display = "none";
			document.getElementById('ClassSummarySection').style.display = "block";
		}
	};

	//************************* MarkEntry *********************************
	$scope.sortMarkEntry = function (keyname) {
		$scope.sortKeys.MarkEntry = keyname;
		$scope.reverses.MarkEntry = !$scope.reverses.MarkEntry;
	}



	$scope.GetClassWiseSubMap = function () {

		$scope.newClassMarkEntry.SubjectList = [];
		/*	$scope.newClassWise.MarkSetupDetailsColl = [];*/
		$scope.StudentsDetailsClassWiseList = [];
		if ($scope.newClassMarkEntry.SelectedClass) {
			var para = {
				ClassId: $scope.newClassMarkEntry.SelectedClass.ClassId,
				SectionIdColl: ($scope.newClassMarkEntry.SelectedClass.SectionId ? $scope.newClassMarkEntry.SelectedClass.SectionId.toString() : '')
			};

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
						Swal.fire('Subject Mapping Not Found');
					}
					else if (SubjectMappingColl.length > 0) {
						angular.forEach(SubjectMappingColl, function (sm) {
							var subDet = $scope.SubjectList1.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								subDet.PaperType = sm.PaperType;
								$scope.newClassMarkEntry.SubjectList.push(subDet);
							}
						});

						$scope.GetEmpListForClassTeacher();
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.GetEmpListForClassTeacher = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newClassMarkEntry.EmployeeList = [];

		if ($scope.newClassMarkEntry.SelectedClass) {

			var para = {
				ClassId: $scope.newClassMarkEntry.SelectedClass.ClassId,
				SectionId: ($scope.newClassMarkEntry.SelectedClass.SectionId ? $scope.newClassMarkEntry.SelectedClass.SectionId : null),
				SemesterId: $scope.newClassMarkEntry.SemesterId,
				ClassYearId: $scope.newClassMarkEntry.ClassYearId,
				BatchId: $scope.newClassMarkEntry.BatchId,
				SubjectId: $scope.newClassMarkEntry.SubjectId
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetEmpListForClassTeacher",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newClassMarkEntry.EmployeeList = res.data.Data;

					if ($scope.newClassMarkEntry.EmployeeList && $scope.newClassMarkEntry.EmployeeList.length > 0) {
						$scope.newClassMarkEntry.EmployeeId = $scope.newClassMarkEntry.EmployeeList[0].EmployeeId;
					}

					$scope.GetCSubjectWiseLesson();
				} else {
					Swal.fire(res.data.ResponseMSG);
				}


			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.GetStudentsDetailsClassWise = function () {
		if ($scope.newClassMarkEntry.SelectedClass && $scope.newClassMarkEntry.SubjectId > 0 && $scope.newClassMarkEntry.TestDateDet && $scope.newClassMarkEntry.TestDateDet.dateAD) {
			var para = {
				ClassId: $scope.newClassMarkEntry.SelectedClass.ClassId,
				SectionId: $scope.newClassMarkEntry.SelectedClass.SectionId,
				SubjectId: $scope.newClassMarkEntry.SubjectId,
				EmployeeId: $scope.newClassMarkEntry.EmployeeId,
				FilterSection: $scope.newClassMarkEntry.SelectedClass.FilterSection,
				LessonSNo: $scope.newClassMarkEntry.LessonSNo || null,
				TestDate: $filter('date')(new Date($scope.newClassMarkEntry.TestDateDet.dateAD), 'yyyy-MM-dd')
			};
			/*$scope.StudentsDetailsClassWiseList = [];*/

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetStudentsDetailsClassWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.StudentsDetailsClassWiseList = res.data.Data;
					$scope.newClassMarkEntry.FullMarks = res.data.Data[0].FullMarks;
					$scope.newClassMarkEntry.PassMarks = res.data.Data[0].PassMarks;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.ChangeMarkEntry = function (dc) {
		if (dc.ObtainMarks < 0) {
			dc.ObtainMarks = 0;
		}
		if (IsNumber(dc.ObtainMarks)) {
			if (dc.ObtainMarks > $scope.newClassMarkEntry.FullMarks) {
				Swal.fire("Please ! Enter Mark less than equal " + $scope.newClassMarkEntry.FullMarks);
				dc.ObtainMarks = 0;
			}
		}
	};

	$scope.onAbsentChange = function (student) {
		if (student.IsAbsent) {
			student.ObtainMarks = 0; // Set the marks to 0 if absent
		}
	};


	$scope.SaveClassTestMarkEntry = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataToSave = [];

		if ($scope.newClassMarkEntry.TestDateDet) {
			$scope.newClassMarkEntry.TestDate = $filter('date')(new Date($scope.newClassMarkEntry.TestDateDet.dateAD), 'yyyy-MM-dd');
		}

		if (!$scope.newClassMarkEntry.FullMarks) {
			Swal.fire("Please! Enter Full Marks");
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			return;
		}
		if ($scope.newClassMarkEntry.PassMarks < 0) {
			Swal.fire("Please! Enter Pass Marks");
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			return;
		}

		var classId = $scope.newClassMarkEntry.SelectedClass.ClassId;
		var teacherId = $scope.newClassMarkEntry.EmployeeId;
		var subjectId = $scope.newClassMarkEntry.SubjectId;
		var lessonSno = $scope.newClassMarkEntry.LessonSNo;
		var sectionId = ($scope.newClassMarkEntry.SelectedClass.SectionId ? $scope.newClassMarkEntry.SelectedClass.SectionId : null);
		var fullMarks = $scope.newClassMarkEntry.FullMarks;
		var passMarks = $scope.newClassMarkEntry.PassMarks;
		var testdate = $scope.newClassMarkEntry.TestDate;

		var batchId = $scope.newClassMarkEntry.BatchId;
		var classYearId = $scope.newClassMarkEntry.ClassYearId;
		var semesterId = $scope.newClassMarkEntry.SemesterId;

		for (var i = 0; i < $scope.StudentsDetailsClassWiseList.length; i++) {
			var S = $scope.StudentsDetailsClassWiseList[i];


			var tranid = S.TranId;
			var studentId = S.StudentId;
			var obtMarks = S.ObtainMarks;
			var remarks = S.RemarksType;
			var isAbsent = S.IsAbsent

			var dataItem = {
				TranId: tranid,
				ClassId: classId,
				SectionId: sectionId,
				EmployeeId: teacherId,
				SubjectId: subjectId,
				LessonSNo: lessonSno,
				StudentId: studentId,
				FullMarks: fullMarks,
				PassMarks: passMarks,
				ObtMarks: obtMarks,
				Remarks: remarks,
				IsAbsent: isAbsent,
				TestDate: testdate,
				BatchId: batchId,
				ClassYearId: classYearId,
				SemesterId: semesterId
			};
			dataToSave.push(dataItem);
		}
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveUpdateClassTestMarkEntry",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: dataToSave }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				/*$scope.ClearClassMarkEntry();*/

				//$scope.GetAllMarkEntryList();
				//$scope.newClassMarkEntry.EmployeeId = null;
				//for (var i = 0; i < $scope.StudentsDetailsClassWiseList.length; i++) {
				//	$scope.StudentsDetailsClassWiseList[i].ObtainMarks = null;
				//	$scope.StudentsDetailsClassWiseList[i].RemarksType = null;
				//}
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	//function for filter Lesson on the basis of Subject

	$scope.GetCSubjectWiseLesson = function () {
		if ($scope.newClassMarkEntry.SelectedClass.ClassId && $scope.newClassMarkEntry.SubjectId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newClassMarkEntry.SelectedClass.ClassId,
				SubjectId: $scope.newClassMarkEntry.SubjectId
			};
			$scope.CSubjectWiseLessonList = [];

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetSubjectLessonWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.CSubjectWiseLessonList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.GetClassSummaryClassWiseSubject = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ClassId: $scope.newSummary.SelectedClass.ClassId
		};
		$scope.SubjectList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetClassSummarySubjectClassWise",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SubjectList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//for view the class summary 

	$scope.GetViewClassSummaryDetails = function () {

		if (!$scope.newSummary.SelectedClass.ClassId || !$scope.newSummary.SubjectId) {
			Swal.fire('Class ID and Subject ID are required.');
			return; // Exit the function early if data is missing
		}
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ClassId: $scope.newSummary.SelectedClass.ClassId,
			SectionId: $scope.newSummary.SelectedClass.SectionId || null,
			SubjectId: $scope.newSummary.SubjectId,
			DateFrom: ($scope.newFilter.FromDateDet ? $filter('date')(new Date($scope.newFilter.FromDateDet.dateAD), 'yyyy-MM-dd') : null),
			DateTo: ($scope.newFilter.ToDateDet ? $filter('date')(new Date($scope.newFilter.ToDateDet.dateAD), 'yyyy-MM-dd') : null),
			BatchId: $scope.newSummary.BatchId,
			ClassYearId: $scope.newSummary.ClassYearId,
			SemesterId: $scope.newSummary.SemesterId,
		};
		$scope.ViewClassSummaryDetailsList = [];
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetViewClassSummaryDetails",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ViewClassSummaryDetailsList = res.data.Data;
				$scope.PassMarks = res.data.Data[0].PassMarks;

				//$scope.PassMarksByStudentId = {};
				//$scope.ViewClassSummaryDetailsList.forEach(function (item) {
				//	$scope.PassMarksByStudentId[item.StudentId] = item.PassMarks;
				//});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//js for view the ClassSummaryDetails data

	$scope.GetClassSummaryDetailsById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			ClassId: refData.ClassId,
			EmployeeId: refData.EmployeeId,
			SubjectId: refData.SubjectId,
			LessonSno: refData.LessonSno,
			TestDate: refData.TestDate
		};
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetClassSummaryDetailsById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SummaryList = res.data.Data;
				$scope.SummaryList.forEach(function (item) {
					item.PassMarks = refData.PassMarks;
				});
				if ($scope.SummaryList.length > 0) {
					$scope.newSummary.SubjectName = $scope.SummaryList[0].SubjectName;
					$scope.newSummary.ClassName = $scope.SummaryList[0].ClassName;
					$scope.newSummary.SectionName = $scope.SummaryList[0].SectionName;
					$scope.newSummary.TestDate = refData.TestDate;
					$scope.newSummary.TestDateBS = refData.TestDateBS;
					$scope.newSummary.EmployeeName = refData.EmployeeName;
					$scope.newSummary.LessonName = refData.LessonName;
				}
				$scope.newSummary.TotalStudent = 0;
				$scope.newSummary.PassStudent = 0;
				$scope.newSummary.FailStudent = 0;
				$scope.newSummary.AbsentStudent = 0;
				angular.forEach($scope.SummaryList, function (item) {
					$scope.newSummary.TotalStudent++;
					if (item.IsAbsent) {
						$scope.newSummary.AbsentStudent++;
					} else if (Number(item.ObtMarks) >= Number(item.PassMarks)) {
						$scope.newSummary.PassStudent++;
					} else {
						$scope.newSummary.FailStudent++;
					}
				});
				$scope.newSummary.Mode = 'Modify';
				document.getElementById('ClassSummarySection').style.display = "none";
				document.getElementById('ClassSummaryForm').style.display = "block";
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire('Failed : ' + reason);
		});
	};

	$scope.triggerPrint = function () {
		// Hide the 'none-print' section when printing
		document.getElementById('none-print').style.display = "none";
		document.getElementById('none-print1').style.display = "none";
		// Trigger the print dialog
		window.print();
		// After printing, show the 'none-print' section again using the 'afterprint' event
		window.onafterprint = function () {
			document.getElementById('none-print').style.display = "block";
			document.getElementById('none-print1').style.display = "block";
		};
		setTimeout(function () {
			document.getElementById('none-print').style.display = "block";
			document.getElementById('none-print1').style.display = "block";
		}, 1000);
	};

	$scope.CheckPMFM = function (marks) {
		var fullMarks = $scope.newClassMarkEntry.FullMarks;
		var passMarks = $scope.newClassMarkEntry.PassMarks;
		if (passMarks > fullMarks) {
			Swal.fire("Please! Enter Pass Marks Less then Full Marks (" + fullMarks + ")");
			$scope.newClassMarkEntry.PassMarks = fullMarks * (40 / 100);
		}
	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

	$scope.validateDates = function (changedField) {
		if (!$scope.newFilter.FromDateDet || !$scope.newFilter.ToDateDet ||
			!$scope.newFilter.FromDateDet.dateAD || !$scope.newFilter.ToDateDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newFilter.FromDateDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newFilter.ToDateDet.dateAD), 'yyyy-MM-dd')
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newFilter.FromDate_TMP = new Date();
						$scope.newFilter.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newFilter.ToDate_TMP = new Date();
						$scope.newFilter.ToDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};
});