app.controller('MarkEntryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'MarkEntry';
	var gSrv = GlobalServices;
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			LocWiseME: 1,
			StudentWiseLOC: 1,
		};

		$scope.searchData = {
			LocWiseME: '',
			StudentWiseLOC: '',
		};

		$scope.perPage = {
			LocWiseME: GlobalServices.getPerPageRow(),
			StudentWiseLOC: GlobalServices.getPerPageRow()
		};

		$scope.MarksList = [{ id: 0, value: 0 },{ id: 1, value: 1 }, { id: 2, value: 2 }, { id: 3, value: 3 }, { id: 4, value: 4 }];

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



		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
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

		$scope.EvaluationToolList = [];
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllEvaluationArea",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EvaluationToolList = res.data.Data.filter(function (item) {
					return item.IsActive === true;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		gSrv.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.newLoc = {
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

		$scope.newStudentWise = {
			TranId: null,
			ClassId: null,
			SubjectId: null,
			LessonId: null,
			TopicName: "",
			StudentId: null,
			AssessmentDate_TMP: new Date(),
			Mode: 'Save'
		};

		//$scope.GetAllMarkEntryList();
	}

	$scope.$watch('newLoc.SelectedClass', function (newVal, oldVal) {
		if (newVal && newVal !== oldVal) {
			$scope.newLoc.ClassYearId = null;
			$scope.newLoc.SemesterId = null;
		}
	});
	$scope.$watch('newStudentWise.SelectedClass', function (newVal, oldVal) {
		if (newVal && newVal !== oldVal) {
			$scope.newStudentWise.ClassYearId = null;
			$scope.newStudentWise.SemesterId = null;
		}
	});

	$scope.ClearLocWiseME = function () {
		$scope.newLoc = {
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
		$scope.LOCStudentsDetailsSubjectsWiseList = [];
	}

	$scope.ClearStdWiseME = function () {
		$scope.newStudentWise = {
			ClassId: null,
			SubjectId: null,
			LessonId: null,
			StudentId: null,
			AssessmentDate_TMP: new Date(),
			Mode: 'Save'
		};
		$scope.StdWiseLOCMarkEntry = [];
	}


	//************************ Subject Wise LOC Mark Entry *****************************************

	$scope.IsValidSubjectWise = function () {
		//if ($scope.newLoc.IndicatorName.isEmpty()) {
		//	Swal.fire('Please ! Enter Indicator Name');
		//	return false;
		//}
		return true;
	}

	$scope.SaveUpdateLocWiseME = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var assessmentDate = $filter('date')(new Date($scope.newLoc.AssessmentDateDet.dateAD), 'yyyy-MM-dd');
		var dataToSave = [];
		angular.forEach($scope.LOCStudentsDetailsSubjectsWiseList, function (student) {
			dataToSave.push({
				StudentId: student.StudentId,
				Marks: student.Marks ?? 0,
				Remarks: student.Remarks,
				ClassId: $scope.newLoc.SelectedClass.ClassId,
				SectionId: $scope.newLoc.SelectedClass.SectionId,
				SubjectId: $scope.newLoc.SubjectId,
				LessonSno: $scope.newLoc.SelectLesson.LessonSno,
				TopicName: $scope.newLoc.TopicName,
				AssessmentTypeId: $scope.newLoc.AssessmentTypeId,
				EvaluationAreaId: student.EvaluationAreaId,
				Evaluation: true,
				AssessmentDate: assessmentDate
			});
		});
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveUpdateICMarkSubject",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("dataToSave", angular.toJson(data.dataToSave));
				return formData;
			},
			data: { dataToSave: dataToSave }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess === true) {
				// Optionally clear the form
				// $scope.ClearLocWiseME();
			}
		}, function (errormessage) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			console.error(errormessage);
		});
	};

	$scope.GetStudentsForLOCMarkEntry = function () {
		if ($scope.newLoc.SelectedClass && $scope.newLoc.SubjectId > 0 && $scope.newLoc.SelectLesson.LessonId > 0 && $scope.newLoc.TopicName != null && $scope.newLoc.AssessmentTypeId && $scope.newLoc.AssessmentDateDet.dateAD) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newLoc.SelectedClass.ClassId,
				SectionId: $scope.newLoc.SelectedClass.SectionId,
				SubjectId: $scope.newLoc.SubjectId,
				LessonId: $scope.newLoc.SelectLesson.LessonId,
				TopicName: $scope.newLoc.TopicName,
				AssessmentTypeId: $scope.newLoc.AssessmentTypeId,
				CFAssessmentTypeId: $scope.newLoc.CFAssessmentTypeId,
				AssessmentDate: $filter('date')(new Date($scope.newLoc.AssessmentDateDet.dateAD), 'yyyy-MM-dd'),
				BatchId: $scope.newLoc.BatchId || null,
				ClassYearId: $scope.newLoc.ClassYearId || null,
				SemesterId: $scope.newLoc.SemesterId || null,
			};
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetStudentsForLOCMarkEntry",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.LOCStudentsDetailsSubjectsWiseList = res.data.Data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}).catch(function (error) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire('Failed: ' + error.message);
			});
		}
	};
	
	$scope.GetSubjectLessonWise = function () {
		if ($scope.newLoc.SelectedClass.ClassId && $scope.newLoc.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newLoc.SelectedClass.ClassId,
				SubjectId: $scope.newLoc.SubjectId
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
		if ($scope.newLoc.SelectLesson.LessonId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				LessonId: $scope.newLoc.SelectLesson.LessonId
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

	$scope.GetClassWiseSubMap = function () {
		$scope.newLoc.SubjectList = [];
		if ($scope.newLoc.SelectedClass) {
			var para = {
				ClassId: $scope.newLoc.SelectedClass.ClassId,
				SectionIdColl: ($scope.newLoc.SelectedClass.SectionId ? $scope.newLoc.SelectedClass.SectionId.toString() : ''),
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
								$scope.newLoc.SubjectList.push(subDet);
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

	$scope.DeleteLOCMarkEntry = function () {
		if (!$scope.newLoc.SubjectId) {
			Swal.fire("Please select a subject.");
			return;
		}
		if (!$scope.newLoc.SelectLesson.LessonId) {
			Swal.fire("Please select a lesson.");
			return;
		}
		if (!$scope.newLoc.TopicName) {
			Swal.fire("Please enter the topic name.");
			return;
		}
		if (!$scope.newLoc.AssessmentTypeId) {
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
					ClassId: $scope.newLoc.SelectedClass.ClassId,
					SectionId: $scope.newLoc.SelectedClass.SectionId,
					FilterSection: $scope.newLoc.SelectedClass.FilterSection,
					SubjectId: $scope.newLoc.SubjectId,
					LessonId: $scope.newLoc.SelectLesson.LessonId,
					TopicName: $scope.newLoc.TopicName,
					AssessmentTypeId: $scope.newLoc.AssessmentTypeId,
					AssessmentDate: $filter('date')(new Date($scope.newLoc.AssessmentDateDet.dateAD), 'yyyy-MM-dd'),
					BatchId: $scope.newLoc.BatchId || null,
					ClassYearId: $scope.newLoc.ClassYearId || null,
					SemesterId: $scope.newLoc.SemesterId || null,
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
						$scope.GetStudentsForLOCMarkEntry();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

	//************************ Student Wise LOC Mark Entry *****************************************

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

	$scope.GetStdWiseLOCMarkEntry = function () {
		if ($scope.newStudentWise.SelectedClass && $scope.newStudentWise.SubjectId > 0 && $scope.newStudentWise.SelectLesson.LessonId > 0 && $scope.newStudentWise.StudentId > 0 && $scope.newStudentWise.AssessmentTypeId) {
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
				url: base_url + "Exam/Transaction/GetStdWiseLOCMarkEntry",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

				if (res.data.IsSuccess && res.data.Data) {
					$scope.StdWiseLOCMarkEntry = res.data.Data;
					
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

	$scope.SaveStdWiseLOCME = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var assessmentDate = $filter('date')(new Date($scope.newStudentWise.AssessmentDateDet.dateAD), 'yyyy-MM-dd');
		var dataToSave = [];
		angular.forEach($scope.StdWiseLOCMarkEntry, function (student) {
			dataToSave.push({
				StudentId: $scope.newStudentWise.StudentId,
				Marks: student.Marks ?? 0,
				Remarks: student.Remarks,
				ClassId: $scope.newStudentWise.SelectedClass.ClassId,
				SectionId: $scope.newStudentWise.SelectedClass.SectionId,
				SubjectId: $scope.newStudentWise.SubjectId,
				LessonSno: $scope.newStudentWise.SelectLesson.LessonSno,
				TopicName: student.TopicName,
				AssessmentTypeId: $scope.newStudentWise.AssessmentTypeId,
				EvaluationAreaId: student.EvaluationAreaId,
				Evaluation: true,
				AssessmentDate: assessmentDate
			});
		});
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveUpdateICMarkSubject",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("dataToSave", angular.toJson(data.dataToSave));
				return formData;
			},
			data: { dataToSave: dataToSave }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess === true) {
				// Optionally clear the form
				// $scope.ClearLocWiseME();
			}
		}, function (errormessage) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			console.error(errormessage);
		});
	};

	$scope.DelLOCMarkEntryStudentWise = function () {
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