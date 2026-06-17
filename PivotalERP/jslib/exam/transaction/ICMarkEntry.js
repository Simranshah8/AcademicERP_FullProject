app.controller('MarkEntryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'MarkEntry';
	var gSrv = GlobalServices;
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			ICMarkSubject: 1,
			MarkSubmittedStatus: 1,
			MarkSubmittedStatusPending: 1

		};

		$scope.searchData = {
			ICMarkSubject: '',
			MarkSubmittedStatus: '',
			MarkSubmittedStatusPending: ''

		};

		$scope.perPage = {
			ICMarkSubject: GlobalServices.getPerPageRow(),
			MarkSubmittedStatus: gSrv.getPerPageRow(),
			MarkSubmittedStatusPending: gSrv.getPerPageRow()

		};

		$scope.AssessmentList = [];
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllAssessmentType",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				// Filter only active items
				$scope.AssessmentList = res.data.Data.filter(function (item) {
					return item.IsActive === true;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.EvaliationToolList = [];
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllEvaluationArea",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EvaliationToolList = res.data.Data.filter(function (item) {
					return item.IsActive === true;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		gSrv.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data.Data);
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

		$scope.newICMarkSubject = {
			TranId: null,
			ClassId: null,
			SubjectId: null,
			LessonId: null,
			TopicName: "",
			StudentId: null,
			IndicatorId: null,
			Evaluation: 0,
			Marks: null,
			Remarks: "",
			AssessmentDate_TMP:new Date(),
			Mode: 'Save'
		};

		$scope.newStudentWise = {
			TranId: null,
			ClassId: null,
			SubjectId: null,
			LessonId: null,
			TopicName: "",
			StudentId: null,
			AssessmentDate_TMP:new Date(),
			Mode: 'Save'
		};
		//$scope.GetAllMarkEntryList();
	}

	$scope.$watch('newICMarkSubject.SelectedClass', function (newVal, oldVal) {
		if (newVal && newVal !== oldVal) {
			$scope.newICMarkSubject.ClassYearId = null;
			$scope.newICMarkSubject.SemesterId = null;
		}
	});
	$scope.$watch('newStudentWise.SelectedClass', function (newVal, oldVal) {
		if (newVal && newVal !== oldVal) {
			$scope.newStudentWise.ClassYearId = null;
			$scope.newStudentWise.SemesterId = null;
		}
	});
	$scope.$watch('newStatus.SelectedClass', function (newVal, oldVal) {
		if (newVal && newVal !== oldVal) {
			$scope.newStatus.ClassYearId = null;
			$scope.newStatus.SemesterId = null;
		}
	});

	$scope.ClearICMarkSubject = function () {
		$scope.newICMarkSubject = {
			TranId: null,
			ClassId: null,
			SubjectId: null,
			LessonId: null,
			TopicName: "",
			StudentId: null,
			IndicatorId: null,
			Evaluation: 0,
			Marks: null,
			Remarks: "",
			AssessmentDate_TMP: new Date(),
			Mode: 'Save'
		};
	}


	//************************* MarkEntry *********************************
	$scope.isCopyDisabled = function () {
		return $scope.IStudentsDetailsSubjectsWiseList.some(function (item) {
			return item.Marks !== null && item.Marks !== undefined && item.Marks !== '';
		});
	};

	$scope.isCopyDisabledStudentWise = function () {
		return $scope.TopicForStudentWiseICList.some(function (item) {
			return item.Marks !== null && item.Marks !== undefined && item.Marks !== '';
		});
	};

	$scope.IsValidSubjectWise = function () {
		//if ($scope.newICMarkSubject.IndicatorName.isEmpty()) {
		//	Swal.fire('Please ! Enter Indicator Name');
		//	return false;
		//}
		return true;
	}

	$scope.SaveUpdateICMarkSubject = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataToSave = [];

		var assessmentDate = $filter('date')(new Date($scope.newICMarkSubject.AssessmentDateDet.dateAD), 'yyyy-MM-dd');

		$scope.IStudentsDetailsSubjectsWiseList.forEach(function (emp) {
			emp.Indicators.forEach(function (ph) {
				dataToSave.push({
					StudentId: emp.StudentId,
					IndicatorName: ph.IndicatorName,
					IndicatorSNo: ph.IndicatorSno,
					Evaluation: ph.Evaluation,
					Marks: emp.Marks,
					Remarks: emp.Remarks,
					ClassId: $scope.newICMarkSubject.SelectedClass.ClassId,
					SectionId: $scope.newICMarkSubject.SelectedClass.SectionId,
					SubjectId: $scope.newICMarkSubject.SubjectId,
					LessonSno: $scope.newICMarkSubject.SelectLesson.LessonSno,
					TopicName: $scope.newICMarkSubject.TopicName,
					//Added by Suresh on 6 Baishakh 2082
					EvaluationAreaId: ph.EvaluationAreaId,
					IndicatorId: ph.IndicatorId,
					AssessmentTypeId: $scope.newICMarkSubject.AssessmentTypeId,
					AssessmentDate: assessmentDate,
					BatchId: $scope.newICMarkSubject.BatchId || null,
					ClassYearId: $scope.newICMarkSubject.ClassYearId || null,
					SemesterId: $scope.newICMarkSubject.SemesterId || null,
				});
			})
		});

		if (dataToSave.length === 0) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire("Select the evaluation to save the data.");
			return;
		}


		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveUpdateICMarkSubject",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("dataToSave", angular.toJson(data.dataToSave));
				/*formData.append("jsonData", angular.toJson(data.jsonData));*/
				return formData;
			},
			data: { dataToSave: dataToSave }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				/*$scope.ClearICMarkSubject();*/
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	$scope.hasValidIndicators = function (indicators) {
		return indicators && indicators.some(function (indicator) {
			return indicator.IndicatorName && indicator.IndicatorName.trim() !== '';
		});
	};

	$scope.GetIStudentsDetailsSubjectsWise = function () {
		if ($scope.newICMarkSubject.SelectedClass && $scope.newICMarkSubject.SubjectId > 0 && $scope.newICMarkSubject.SelectLesson.LessonId > 0 && $scope.newICMarkSubject.TopicName != null) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newICMarkSubject.SelectedClass.ClassId,
				SectionId: $scope.newICMarkSubject.SelectedClass.SectionId,
				FilterSection: $scope.newICMarkSubject.SelectedClass.FilterSection,
				SubjectId: $scope.newICMarkSubject.SubjectId,
				LessonId: $scope.newICMarkSubject.SelectLesson.LessonId,
				TopicName: $scope.newICMarkSubject.TopicName,
				AssessmentTypeId: $scope.newICMarkSubject.AssessmentTypeId,
				CFAssessmentTypeId: $scope.newICMarkSubject.CFAssessmentTypeId,
				AssessmentDate: $filter('date')(new Date($scope.newICMarkSubject.AssessmentDateDet.dateAD), 'yyyy-MM-dd'),
				BatchId: $scope.newICMarkSubject.BatchId || null,
				ClassYearId: $scope.newICMarkSubject.ClassYearId || null,
				SemesterId: $scope.newICMarkSubject.SemesterId || null,
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetIStudentsDetailsSubjectsWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.IStudentsDetailsSubjectsWiseList = res.data.Data;
					angular.forEach($scope.IStudentsDetailsSubjectsWiseList, function (item) {
						$scope.MarksRemakrs(item);
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}).catch(function (error) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

				console.error('Request failed:', error);
				Swal.fire('Failed: ' + error.message);
			});
		}
	};

	$scope.MarksRemakrs = function (Ic) {
		var totalMarks = 0;
		var remarks = "";
		angular.forEach(Ic.Indicators, function (indicator) {
			if (indicator.Evaluation) {
				totalMarks = indicator.Marks;
				remarks = indicator.Remarks;
			}
		});
		Ic.Marks = totalMarks;
		Ic.Remarks = remarks;
	};

	//$scope.GetClassWiseSubjectListAdd = function (classId) {
	//	$scope.SubjectListAdd = [];
	//	var para = {
	//		ClassId: classId
	//	};

	//	if (classId > 0) {
	//		$http({
	//			method: 'POST',
	//			url: base_url + "Academic/Creation/GetSubjectListForLessonPlan",
	//			dataType: "json",
	//			data: JSON.stringify(para)
	//		}).then(function (res) {
	//			hidePleaseWait();
	//			$scope.loadingstatus = "stop";
	//			if (res.data.IsSuccess && res.data.Data) {
	//				$scope.SubjectListAdd = res.data.Data;
	//			} else {
	//				Swal.fire(res.data.ResponseMSG);
	//			}
	//		}, function (reason) {
	//			Swal.fire('Failed' + reason);
	//		});
	//	}

	//}


	$scope.GetSubjectLessonWise = function () {
		if ($scope.newICMarkSubject.SelectedClass.ClassId && $scope.newICMarkSubject.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newICMarkSubject.SelectedClass.ClassId,
				SubjectId: $scope.newICMarkSubject.SubjectId
			};
			$scope.SubjectLessonWiseList = [];

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetSubjectLessonWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.SubjectLessonWiseList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.GetLessonTopicDetailsWise = function () {
		if ($scope.newICMarkSubject.SelectLesson.LessonId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				LessonId: $scope.newICMarkSubject.SelectLesson.LessonId
			};
			$scope.LessonTopicDetailsWiseList = [];

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetLessonTopicDetailsWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.LessonTopicDetailsWiseList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.updateMarks = function (student) {
		var count = 0;
		angular.forEach(student.Indicators, function (indicator) {
			if (indicator.Evaluation) {
				count++;
			}
		});
		student.Marks = count;
	};

	// Initialize Marks for all students
	angular.forEach($scope.IStudentsDetailsSubjectsWiseList, function (student) {
		student.Marks = 0;
	});

	//NEw Code Added
	$scope.GetClassWiseSubMap= function () {
		$scope.newICMarkSubject.SubjectList = [];
		/*	$scope.newSubjectWise.StudentColl = [];*/
		if ($scope.newICMarkSubject.SelectedClass) {
			var para = {
				ClassId: $scope.newICMarkSubject.SelectedClass.ClassId,
				SectionIdColl: ($scope.newICMarkSubject.SelectedClass.SectionId ? $scope.newICMarkSubject.SelectedClass.SectionId.toString() : ''),
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
							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								subDet.PaperType = sm.PaperType;
								subDet.CRTH = 0;
								subDet.CRPR = 0;
								subDet.FMTH = 0;
								subDet.FMPR = 0;
								subDet.PMTH = 0;
								subDet.PMPR = 0;
								subDet.IsInclude = true;
								$scope.newICMarkSubject.SubjectList.push(subDet);
							}
						});

					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	//StudentWise Tab Code Starts
	$scope.GetClassWiseSubMapStW = function () {
		$scope.newStudentWise.SubjectList = [];
		$scope.newStudentWise.StudentList = [];
		if ($scope.newStudentWise.SelectedClass) {
			var para = {
				ClassId: $scope.newStudentWise.SelectedClass.ClassId,
				SectionIdColl: ($scope.newStudentWise.SelectedClass.SectionId ? $scope.newStudentWise.SelectedClass.SectionId.toString() : ''),
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
							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								subDet.PaperType = sm.PaperType;
								subDet.CRTH = 0;
								subDet.CRPR = 0;
								subDet.FMTH = 0;
								subDet.FMPR = 0;
								subDet.PMTH = 0;
								subDet.PMPR = 0;
								subDet.IsInclude = true;
								$scope.newStudentWise.SubjectList.push(subDet);
							}
						});

					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	};

	$scope.GetSubjectLessonAndStudentList = function () {
		if ($scope.newStudentWise.SelectedClass.ClassId && $scope.newStudentWise.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var paraLesson = {
				ClassId: $scope.newStudentWise.SelectedClass.ClassId,
				SubjectId: $scope.newStudentWise.SubjectId
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetSubjectLessonWise",
				dataType: "json",
				data: JSON.stringify(paraLesson)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

				if (res.data.IsSuccess && res.data.Data) {
					$scope.SubjectLessonWiseList1 = res.data.Data;

					var paraStudent = {
						ClassId: $scope.newStudentWise.SelectedClass.ClassId,
						SectionId: $scope.newStudentWise.SelectedClass.SectionId,
						ExamTypeId: $scope.newStudentWise.ExamTypeId,
						BatchId: $scope.newStudentWise.BatchId || null,
						ClassYearId: $scope.newStudentWise.ClassYearId || null,
						SemesterId: $scope.newStudentWise.SemesterId || null,
					};

					$scope.newStudentWise.StudentList = [];
					$http({
						method: 'POST',
						url: base_url + "Academic/Transaction/GetStudentForTran",
						dataType: "json",
						data: JSON.stringify(paraStudent)
					}).then(function (res) {
						if (res.data.IsSuccess) {
							$scope.newStudentWise.StudentList = res.data.Data;
						} else {
							Swal.fire(res.data.ResponseMSG);
						}
					}, function (reason) {
						Swal.fire('Failed to get student list: ' + reason);
					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire('Failed to get subject lesson wise list: ' + reason);
			});
		}
	}

	$scope.GetTopicForStudentWiseIC = function () {
		if ($scope.newStudentWise.SelectedClass && $scope.newStudentWise.SubjectId > 0 && $scope.newStudentWise.SelectLesson.LessonId > 0 && $scope.newStudentWise.StudentId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				ClassId: $scope.newStudentWise.SelectedClass.ClassId,
				SectionId: $scope.newStudentWise.SelectedClass.SectionId,
				FilterSection: $scope.newStudentWise.SelectedClass.FilterSection,
				SubjectId: $scope.newStudentWise.SubjectId,
				LessonId: $scope.newStudentWise.SelectLesson.LessonId,
				StudentId: $scope.newStudentWise.StudentId,
				//new field
				AssessmentTypeId: $scope.newStudentWise.AssessmentTypeId,
				CFAssessmentTypeId: $scope.newStudentWise.CFAssessmentTypeId,
				AssessmentDate: $filter('date')(new Date($scope.newStudentWise.AssessmentDateDet.dateAD), 'yyyy-MM-dd'),
				BatchId: $scope.newStudentWise.BatchId || null,
				ClassYearId: $scope.newStudentWise.ClassYearId || null,
				SemesterId: $scope.newStudentWise.SemesterId || null,
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetTopicForStudentWiseIC",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

				if (res.data.IsSuccess && res.data.Data) {
					$scope.TopicForStudentWiseICList = res.data.Data;
					angular.forEach($scope.TopicForStudentWiseICList, function (item) {
						$scope.MarksRemakrs(item);
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}).catch(function (error) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				console.error('Request failed:', error);
				Swal.fire('Failed: ' + error.message);
			});
		}
	};

	$scope.SaveUpdateStudentWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataToSave = [];

		var assessmentDate = $filter('date')(new Date($scope.newStudentWise.AssessmentDateDet.dateAD), 'yyyy-MM-dd');

		$scope.TopicForStudentWiseICList.forEach(function (emp) {
			emp.Indicators.forEach(function (ph) {
				dataToSave.push({
					StudentId: $scope.newStudentWise.StudentId,
					IndicatorName: ph.IndicatorName,
					IndicatorSNo: ph.IndicatorSno,
					Evaluation: ph.Evaluation,
					Marks: emp.Marks,
					Remarks: emp.Remarks,
					ClassId: $scope.newStudentWise.SelectedClass.ClassId,
					SectionId: $scope.newStudentWise.SelectedClass.SectionId,
					SubjectId: $scope.newStudentWise.SubjectId,
					LessonSno: $scope.newStudentWise.SelectLesson.LessonSno,
					TopicName: emp.TopicName,
					//Added by Suresh on 6 Baishakh 2082
					EvaluationAreaId: ph.EvaluationAreaId,
					IndicatorId: ph.IndicatorId,
					AssessmentTypeId: $scope.newStudentWise.AssessmentTypeId,
					AssessmentDate: assessmentDate,
					BatchId: $scope.newStudentWise.BatchId || null,
					ClassYearId: $scope.newStudentWise.ClassYearId || null,
					SemesterId: $scope.newStudentWise.SemesterId || null,
				});
			})
		});

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveUpdateICMarkSubject",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("dataToSave", angular.toJson(data.dataToSave));
				/*formData.append("jsonData", angular.toJson(data.jsonData));*/
				return formData;
			},
			data: { dataToSave: dataToSave }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				/*$scope.ClearICMarkSubject();*/
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	//Studentwise Code Ends

	//IC MarkEntry Status Code Starts
	$scope.GetClassWiseSubMapForStatus = function () {
		$scope.newStatus.SubjectList = [];
		$scope.newStatus.StudentList = [];
		if ($scope.newStatus.SelectedClass) {
			var para = {
				ClassId: $scope.newStatus.SelectedClass.ClassId,
				SectionIdColl: ($scope.newStatus.SelectedClass.SectionId ? $scope.newStatus.SelectedClass.SectionId.toString() : ''),
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
							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								subDet.PaperType = sm.PaperType;
								subDet.CRTH = 0;
								subDet.CRPR = 0;
								subDet.FMTH = 0;
								subDet.FMPR = 0;
								subDet.PMTH = 0;
								subDet.PMPR = 0;
								subDet.IsInclude = true;
								$scope.newStatus.SubjectList.push(subDet);
							}
						});

					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.GetSubjectLessonWiseStatus = function () {
		if ($scope.newStatus.SelectedClass.ClassId && $scope.newStatus.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newStatus.SelectedClass.ClassId,
				SubjectId: $scope.newStatus.SubjectId
			};
			$scope.SubjectLessonWiseListSt = [];

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetSubjectLessonWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.SubjectLessonWiseListSt = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.GetAllMarkSubmittedStatusList = function () {
		if ($scope.newStatus && $scope.newStatus.SelectLesson.LessonId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			$scope.PendingMarkList = [];
			$scope.SubmitMarkList = [];

			var para = {
				ClassId: $scope.newStatus.SelectedClass.ClassId,
				SectionId: $scope.newStatus.SelectedClass.SectionId,
				SubjectId: $scope.newStatus.SubjectId,
				LessonId: $scope.newStatus.SelectLesson.LessonId,
				BatchId: $scope.newStatus.BatchId || null,
				ClassYearId: $scope.newStatus.ClassYearId || null,
				SemesterId: $scope.newStatus.SemesterId || null,
			};
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetICMArkEntryStatus",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					angular.forEach(res.data.Data, function (d) {
						if (d.IsPending == true)
							$scope.PendingMarkList.push(d);
						else
							$scope.SubmitMarkList.push(d);
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}
	//IC MArk Entry Status Code Ends

	$scope.DeleteICMarkEntry = function () {
		if (!$scope.newICMarkSubject.SubjectId) {
			Swal.fire("Please select a subject.");
			return;
		}
		if (!$scope.newICMarkSubject.SelectLesson.LessonId) {
			Swal.fire("Please select a lesson.");
			return;
		}
		if (!$scope.newICMarkSubject.TopicName) {
			Swal.fire("Please enter the topic name.");
			return;
		}
		if (!$scope.newICMarkSubject.AssessmentTypeId) {
			Swal.fire("Please select an assessment type.");
			return;
		}
		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {

			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					ClassId: $scope.newICMarkSubject.SelectedClass.ClassId,
					SectionId: $scope.newICMarkSubject.SelectedClass.SectionId,
					FilterSection: $scope.newICMarkSubject.SelectedClass.FilterSection,
					SubjectId: $scope.newICMarkSubject.SubjectId,
					LessonId: $scope.newICMarkSubject.SelectLesson.LessonId,
					TopicName: $scope.newICMarkSubject.TopicName,
					AssessmentTypeId: $scope.newICMarkSubject.AssessmentTypeId,
					AssessmentDate: $filter('date')(new Date($scope.newICMarkSubject.AssessmentDateDet.dateAD), 'yyyy-MM-dd'),
					BatchId: $scope.newICMarkSubject.BatchId || null,
					ClassYearId: $scope.newICMarkSubject.ClassYearId || null,
					SemesterId: $scope.newICMarkSubject.SemesterId || null,
				};
				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DeleteICMarkEntry",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						Swal.fire("Data Deleted successfully.");
						$scope.GetIStudentsDetailsSubjectsWise();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

	$scope.DeleteICMarkEntryStudentWise = function () {
		if (!$scope.newStudentWise.SubjectId) {
			Swal.fire("Please select a subject.");
			return;
		}
		if (!$scope.newStudentWise.SelectLesson.LessonId) {
			Swal.fire("Please select a lesson.");
			return;
		}
		if (!$scope.newStudentWise.StudentId) {
			Swal.fire("Please select student.");
			return;
		}
		if (!$scope.newStudentWise.AssessmentTypeId) {
			Swal.fire("Please select an assessment type.");
			return;
		}
		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {

			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					ClassId: $scope.newStudentWise.SelectedClass.ClassId,
					SectionId: $scope.newStudentWise.SelectedClass.SectionId,
					FilterSection: $scope.newStudentWise.SelectedClass.FilterSection,
					SubjectId: $scope.newStudentWise.SubjectId,
					LessonId: $scope.newStudentWise.SelectLesson.LessonId,
					StudentId: $scope.newStudentWise.StudentId,
					AssessmentTypeId: $scope.newStudentWise.AssessmentTypeId,
					AssessmentDate: $filter('date')(new Date($scope.newStudentWise.AssessmentDateDet.dateAD), 'yyyy-MM-dd'),
					BatchId: $scope.newStudentWise.BatchId || null,
					ClassYearId: $scope.newStudentWise.ClassYearId || null,
					SemesterId: $scope.newStudentWise.SemesterId || null,
				};
				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DeleteICMarkEntryStudentWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						Swal.fire("Data Deleted successfully.");
						$scope.GetTopicForStudentWiseIC();
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



});