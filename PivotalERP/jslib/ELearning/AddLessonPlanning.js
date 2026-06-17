app.controller('LessonPlanController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Lesson Plan';

	var glbS = GlobalServices;

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();
		//$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();

		//$scope.ClassSection = {};
		//glbS.getClassSectionList().then(function (res) {
		//	$scope.ClassSection = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ClassColl = [];
		$scope.SectionClassColl = [];
		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
			$scope.ClassColl = res.data.Data.ClassList;
			$scope.SectionClassColl = res.data.Data.SectionList;
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

		$scope.newAcademic = {};
		$scope.showSectionDropdown = false;
		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetAcademicConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAcademic = res.data.Data;
				$scope.showSectionDropdown = $scope.newAcademic.SectionWiseLessonPlan === true;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newAddLesson = {
			TranId: null,
			ClassId: null,
			SubjectId: null,
			NoOfLesson: 0,
			DetailsColl: [],
			Mode: 'Save'
		};

		$scope.newListOfLessons = {
			ListOfLessonsId: null,
			ClassId: null,
			SubjectId: null,
			Mode: 'Save'
		};

		$scope.newAddLessonPlan = {
			AddLessonPlanId: null,
			ClassId: null,
			SubjectId: null,
			Mode: 'Save'
		};

		$scope.newListOfLessonPlan = {
			ListOfLessonPlanId: null,
			ClassId: null,
			SubjectId: null,
			Mode: 'Save'
		};



	}

	$scope.GetClassWiseSubMap = function () {
		$scope.newAddLessonPlan.SubjectList = [];
		if ($scope.newAddLessonPlan?.SelectedClass?.ClassId && $scope.newAddLessonPlan.SelectedClass.ClassId > 0) {
			var para = {
				ClassId: $scope.newAddLessonPlan.SelectedClass.ClassId,
				SectionIdColl: ($scope.newAddLessonPlan.SectionId ? $scope.newAddLessonPlan.SectionId.toString() : ''),
				SemesterId: $scope.newAddLessonPlan.SemesterId||null,
				ClassYearId: $scope.newAddLessonPlan.ClassYearId|| null,
				BatchId: $scope.newAddLessonPlan.BatchId|| null,
				BranchId:null,
			};
			$scope.newAddLessonPlan.DetailsColl = [];
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
					/*	Swal.fire('Subject Mapping Not Found');*/
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
								subDet.SubjectType = 1;
								subDet.OTH = 1;
								subDet.OPR = 1;
								$scope.newAddLessonPlan.SubjectList.push(subDet);
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

	$scope.GetClassWiseSubMapRep = function () {
		$scope.newListOfLessonPlan.SubjectList = [];
		if ($scope.newListOfLessonPlan?.SelectedClass?.ClassId && $scope.newListOfLessonPlan.SelectedClass.ClassId > 0) {
			var para = {
				ClassId: $scope.newListOfLessonPlan.SelectedClass.ClassId,
				SectionIdColl: ($scope.newListOfLessonPlan.SectionId ? $scope.newListOfLessonPlan.SectionId.toString() : ''),
				SemesterId: $scope.newListOfLessonPlan.SemesterId || null,
				ClassYearId: $scope.newListOfLessonPlan.ClassYearId || null,
				BatchId: $scope.newListOfLessonPlan.BatchId || null,
				BranchId: null,
			};
			$scope.newListOfLessonPlan.DetailsColl = [];
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
					/*	Swal.fire('Subject Mapping Not Found');*/
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
								subDet.SubjectType = 1;
								subDet.OTH = 1;
								subDet.OPR = 1;
								$scope.newListOfLessonPlan.SubjectList.push(subDet);
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

	$scope.CurrentLesson = {};
	$scope.ChangeCurrentLesson = function (lesson) {
		$scope.CurrentLesson = lesson;
	}
	
	
	$scope.$watch('newAddLesson.SelectedClass', function (newVal, oldVal) {
		if (newVal && oldVal && newVal.id !== oldVal.id) {
			// Same class selected again
			$scope.newAddLesson.SemesterId = null;
			$scope.newAddLesson.ClassYearId = null;
		}
	}, true);
	

	$scope.ClearAddLesson = function () {
		$timeout(function () {
			$scope.newAddLesson = {
				TranId: null,
				ClassId: null,
				SubjectId: null,
				NoOfLesson: 0,
				DetailsColl: [],
				Mode: 'Save'
			};
			$scope.CurrentLesson = {};
		});
	};

	$scope.$watch('newListOfLessons.SelectedClass', function (newVal, oldVal) {
		if (newVal && oldVal && newVal.id !== oldVal.id) {
			// Same class selected again
			$scope.newListOfLessons.SemesterId = null;
			$scope.newListOfLessons.ClassYearId = null;
		}
	}, true);
	

	$scope.GetLessonPlanByClassSubjectALP = function () {
		if ($scope.newAddLessonPlan.SelectedClass.ClassId > 0 && $scope.newAddLessonPlan.SubjectId > 0) {
			$scope.newAddLessonPlan.SubjectName = mx($scope.newAddLessonPlan.SubjectList).firstOrDefault(p1 => p1.SubjectId == $scope.newAddLessonPlan.SubjectId).Name;
			var sectionIdColl = null;
			var sectionIdColl = ($scope.newAddLessonPlan.SectionIdColl && $scope.newAddLessonPlan.SectionIdColl.length > 0)
				? $scope.newAddLessonPlan.SectionIdColl.join(',')
				: '';

			var para = {
				ClassId: $scope.newAddLessonPlan.SelectedClass.ClassId,
				SubjectId: $scope.newAddLessonPlan.SubjectId,
				SectionIdColl: sectionIdColl,
				/*SectionIdColl: ($scope.newAddLessonPlan.SectionIdColl ? $scope.newAddLessonPlan.SectionIdColl.toString() : ''),*/
				BatchId: $scope.newAddLessonPlan.BatchId || null,
				ClassYearId: $scope.newAddLessonPlan.ClassYearId || null,
				SemesterId: $scope.newAddLessonPlan.SemesterId || null
			};
			
			
			$http({
				method: 'POST',
				url: base_url + "ELearning/Creation/GetLPClassSubjectSecWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$timeout(function () {
						var dd = res.data.Data;
						$scope.newAddLessonPlan.NoOfLesson = dd.NoOfLesson;
						$scope.newAddLessonPlan.TranId = dd.TranId;
						$scope.newAddLessonPlan.DetailsColl = dd.DetailsColl;
						angular.forEach($scope.newAddLessonPlan.DetailsColl, function (dc) {
							if (dc.PlanStartDate_AD)
								dc.PlanStartDate_TMP = new Date(dc.PlanStartDate_AD);

							if (dc.PlanEndDate_AD)
								dc.PlanEndDate_TMP = new Date(dc.PlanEndDate_AD);

							angular.forEach(dc.TopicColl, function (tc) {
								if (tc.PlanStartDate_AD)
									tc.PlanStartDate_TMP = new Date(tc.PlanStartDate_AD);

								if (tc.PlanEndDate_AD)
									tc.PlanEndDate_TMP = new Date(tc.PlanEndDate_AD);
							});
						})
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.ClearLessonPlan = function () {
		$timeout(function () {
			$scope.newAddLessonPlan = {};
		});
	}
	

	$scope.CallSaveUpdateLessonPlanDate = function () {
		
		if (!$scope.newAddLessonPlan.SelectedClass ||
			!$scope.newAddLessonPlan.SelectedClass.ClassId) {
			Swal.fire('Please select a Class');
			return;
		}

		if (!$scope.newAddLessonPlan.SubjectId || $scope.newAddLessonPlan.SubjectId <= 0) {
			Swal.fire('Please select a Subject');
			return;
		}
		var invalid = false;

		angular.forEach($scope.newAddLessonPlan.DetailsColl, function (dc) {

			var validate = function (fromDet, toDet) {
				return fromDet && toDet &&
					new Date(fromDet.dateAD) > new Date(toDet.dateAD);
			};

			if (dc.TopicColl && dc.TopicColl.length) {
				angular.forEach(dc.TopicColl, function (tc) {
					if (validate(tc.PlanStartDateDet, tc.PlanEndDateDet)) {
						invalid = true;
					}
				});
			} else {
				if (validate(dc.PlanStartDateDet, dc.PlanEndDateDet)) {
					invalid = true;
				}
			}
		});

		if (invalid) {
			Swal.fire('From Date must be earlier than or equal to To Date');
			return;
		}
		//Ends
		if ($scope.newAddLessonPlan.SelectedClass.ClassId && $scope.newAddLessonPlan.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var sectionIdsStr = ($scope.newAddLessonPlan.SectionIdColl || []).toString();
			var saveData = [];
			angular.forEach($scope.newAddLessonPlan.DetailsColl, function (dc) {
				// If lesson has topics, add one record per topic
				if (dc.TopicColl && dc.TopicColl.length > 0) {
					angular.forEach(dc.TopicColl, function (tc) {
						saveData.push({
							AcademicYearId: $scope.newAddLessonPlan.AcademicYearId,
							BranchId: $scope.newAddLessonPlan.BranchId,
							ClassId: $scope.newAddLessonPlan.SelectedClass.ClassId,
							SectionIdColl: sectionIdsStr,
							BatchId: $scope.newAddLessonPlan.BatchId,
							SemesterId: $scope.newAddLessonPlan.SemesterId,
							ClassYearId: $scope.newAddLessonPlan.ClassYearId,
							SubjectId: $scope.newAddLessonPlan.SubjectId,
							EmployeeId: $scope.newAddLessonPlan.EmployeeId,
							LessonSNo: dc.SNo,
							TopicSNo: tc.SNo,
							PlanStartDate: tc.PlanStartDateDet ? $filter('date')(new Date(tc.PlanStartDateDet.dateAD), 'yyyy-MM-dd') : null,
							PlanEndDate: tc.PlanEndDateDet ? $filter('date')(new Date(tc.PlanEndDateDet.dateAD), 'yyyy-MM-dd') : null,
							TranId: tc.TranId|| 0
						});
					});
				} else {
					// If no topics, add only lesson row
					saveData.push({
						AcademicYearId: $scope.newAddLessonPlan.AcademicYearId,
						BranchId: $scope.newAddLessonPlan.BranchId,
						ClassId: $scope.newAddLessonPlan.SelectedClass.ClassId,
						SectionIdColl: sectionIdsStr,
						BatchId: $scope.newAddLessonPlan.BatchId,
						SemesterId: $scope.newAddLessonPlan.SemesterId,
						ClassYearId: $scope.newAddLessonPlan.ClassYearId,
						SubjectId: $scope.newAddLessonPlan.SubjectId,
						EmployeeId: $scope.newAddLessonPlan.EmployeeId,
						LessonSNo: dc.SNo,
						TopicSNo: null,
						PlanStartDate: dc.PlanStartDateDet ? $filter('date')(new Date(dc.PlanStartDateDet.dateAD), 'yyyy-MM-dd') : null,
						PlanEndDate: dc.PlanEndDateDet ? $filter('date')(new Date(dc.PlanEndDateDet.dateAD), 'yyyy-MM-dd') : null,
						TranId: tc.TranId || 0
					});
				}
			});
			$http({
				method: 'POST',
				url: base_url + "Elearning/Creation/SaveLessonPlanningColl",
				headers: { 'Content-Type': undefined },
				transformRequest: function (data) {
					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));
					return formData;
				},
				data: { jsonData: saveData }
			}).then(function (res) {
				$scope.loadingstatus = "stop";
				hidePleaseWait();
				Swal.fire(res.data.ResponseMSG);
				$scope.GetLessonPlanByClassSubjectALP();
				$scope.GetLessonPlanByClassSubjectLLP();
			}, function () {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
			});
		}
	};


	$scope.GetClassWiseSubjectListForLLP = function (classId) {
		$scope.SubjectListLLP = [];
		var para = {
			ClassId: classId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetSubjectListForLessonPlan",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$timeout(function () {
					$scope.SubjectListLLP = res.data.Data;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetLessonPlanByClassSubjectLLP = function () {
		if ($scope.newListOfLessonPlan.SelectedClass.ClassId > 0 && $scope.newListOfLessonPlan.SubjectId > 0) {
			$scope.newListOfLessonPlan.SubjectName = mx($scope.newListOfLessonPlan.SubjectList).firstOrDefault(p1 => p1.SubjectId == $scope.newListOfLessonPlan.SubjectId).Name;

			var sectionIdColl = ($scope.newListOfLessonPlan.SectionIdColl && $scope.newListOfLessonPlan.SectionIdColl.length > 0)
				? $scope.newListOfLessonPlan.SectionIdColl.join(',')
				: '';

			var para = {
				ClassId: $scope.newListOfLessonPlan.SelectedClass.ClassId,
				SubjectId: $scope.newListOfLessonPlan.SubjectId,
				  SectionIdColl: sectionIdColl,
				/*SectionIdColl: ($scope.newListOfLessonPlan.SectionIdColl ? $scope.newListOfLessonPlan.SectionIdColl.toString() : ''),*/
				BatchId: $scope.newListOfLessonPlan.BatchId || null,
				ClassYearId: $scope.newListOfLessonPlan.ClassYearId || null,
				SemesterId: $scope.newListOfLessonPlan.SemesterId || null
			};
			$http({
				method: 'POST',
				url: base_url + "ELearning/Creation/GetLPClassSubjectSecWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$timeout(function () {
						var dd = res.data.Data;
						$scope.newListOfLessonPlan.NoOfLesson = dd.NoOfLesson;
						$scope.newListOfLessonPlan.TranId = dd.TranId;
						$scope.newListOfLessonPlan.DetailsColl = dd.DetailsColl;
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}	}
	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};


	$scope.CurrentLessionTopic = {};
	$scope.ClickOnTopic = function (lession, topic) {
		if (topic.PlanStartDateDet)
			topic.PlanStartDate_AD = new Date(topic.PlanStartDateDet.dateAD);

		if (topic.PlanEndDateDet)
			topic.PlanEndDate_AD = new Date(topic.PlanEndDateDet.dateAD);

		if (topic.PlanStartDate_AD && topic.PlanEndDate_AD) {
			$scope.CurrentLessionTopic = {
				//LessonId: lession.LessonId,
				//LessonSNo: lession.SNo,
				//TopicSNo: topic.SNo,
				//LessonName: lession.LessonName,
				//TopicName: topic.TopicName,
				TranId: topic.TranId,
				ContentsColl: [],
			};

			var para = {
				TranId: topic.TranId
			};

			$http({
				method: 'POST',
				url: base_url + "ELearning/Creation/GetLessonPlanningTopicContent",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					angular.forEach(res.data.Data, function (dt) {
						$scope.CurrentLessionTopic.ContentsColl.push({
							//FilePath: dt.FilePath,
							//FileName: dt.FileName,
							ForDate_TMP: new Date(dt.ForDate),
							Contents: dt.Contents
						});
					});

					if ($scope.CurrentLessionTopic.ContentsColl.length == 0) {
						var sDate = new Date(topic.PlanStartDate_AD);
						for (var d = new Date(topic.PlanStartDate_AD); d <= new Date(topic.PlanEndDate_AD); d.setDate(d.getDate() + 1)) {
							$timeout(function () {
								$scope.CurrentLessionTopic.ContentsColl.push({
									//FilePath: '',
									//FileName: '',
									ForDate_TMP: sDate,
									Contents: ''
								});
								sDate = sDate.addDays(1);
							});
						}
					}
					$('#modal-xl').modal('show');
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.CallSaveUpdateLessonTopicTeacherContent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataColl = [];
		angular.forEach($scope.CurrentLessionTopic.ContentsColl, function (cc, index) {
			var newBeData = {
				//LessonId: $scope.CurrentLessionTopic.LessonId,
				//LessonSNo: $scope.CurrentLessionTopic.LessonSNo,
				//TopicSNo: $scope.CurrentLessionTopic.TopicSNo,
				//FileName: cc.FileName,
				//FilePath: cc.FilePath,
				ContentSNo: index + 1,
				TranId: $scope.CurrentLessionTopic.TranId,
				ForDate: $filter('date')(new Date(cc.ForDateDet.dateAD), 'yyyy-MM-dd'),
				Contents: cc.Contents
			};
			dataColl.push(newBeData);
		});

		$http({
			method: 'POST',
			url: base_url + "ELearning/Creation/SaveLessonPlanningContent",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: dataColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			$('#modal-xl').modal('hide');
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.AddContentDetails = function (ind) {
		if ($scope.CurrentLessionTopic.ContentsColl) {
			if ($scope.CurrentLessionTopic.ContentsColl.length > ind + 1) {
				$scope.CurrentLessionTopic.ContentsColl.splice(ind + 1, 0, {
					FilePath: '',
					FileName: '',
					ForDate_TMP: null,
					Contents: ''
				})
			} else {
				$scope.CurrentLessionTopic.ContentsColl.push({
					FilePath: '',
					FileName: '',
					ForDate_TMP: null,
					Contents: ''
				})
			}
		}
	};

	$scope.delContentDetails = function (ind) {
		if ($scope.CurrentLessionTopic.ContentsColl) {
			if ($scope.CurrentLessionTopic.ContentsColl.length > 1) {
				$scope.CurrentLessionTopic.ContentsColl.splice(ind, 1);
			}
		}
	};

	$scope.CurrentLessonC = {};
	$scope.ClickOnLessonC = function (lession) {
		if (lession.PlanStartDateDet)
			lession.PlanStartDate_AD = new Date(lession.PlanStartDateDet.dateAD);

		if (lession.PlanEndDateDet)
			lession.PlanEndDate_AD = new Date(lession.PlanEndDateDet.dateAD);

		if (lession.PlanStartDate_AD) {
			$scope.CurrentLessonC = {
				LessonId: lession.LessonId,
				LessonSNo: lession.SNo,
				LessonName: lession.LessonName,
				ContentsColl: [],
			};
			var para = {
				LessonId: lession.LessonId,
				LessonSNo: lession.SNo
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetLessonTeacherContent",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					angular.forEach(res.data.Data, function (dt) {
						$scope.CurrentLessonC.ContentsColl.push({
							FilePath: dt.FilePath,
							FileName: dt.FileName,
							ForDate_TMP: new Date(dt.ForDate),
							Contents: dt.Contents
						});
					});
					if ($scope.CurrentLessonC.ContentsColl.length == 0) {
						var sDate = new Date(lession.PlanStartDate_AD);
						for (var d = new Date(lession.PlanStartDate_AD); d <= new Date(lession.PlanEndDate_AD); d.setDate(d.getDate() + 1)) {
							$timeout(function () {
								$scope.CurrentLessonC.ContentsColl.push({
									FilePath: '',
									FileName: '',
									ForDate_TMP: sDate,
									Contents: ''
								});
								sDate = sDate.addDays(1);
							});
						}
					}
					$('#modal-xl-1').modal('show');
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.CallSaveUpdateLessonTeacherContent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataColl = [];
		angular.forEach($scope.CurrentLessonC.ContentsColl, function (cc) {
			var newBeData = {
				LessonId: $scope.CurrentLessonC.LessonId,
				LessonSNo: $scope.CurrentLessonC.LessonSNo,
				FileName: cc.FileName,
				FilePath: cc.FilePath,
				ForDate: $filter('date')(new Date(cc.ForDateDet.dateAD), 'yyyy-MM-dd'),
				Contents: cc.Contents
			};
			dataColl.push(newBeData);
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveLessonTeacherContent",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: dataColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.AddContentDetails1 = function (ind) {
		if ($scope.CurrentLessonC.ContentsColl) {
			if ($scope.CurrentLessonC.ContentsColl.length > ind + 1) {
				$scope.CurrentLessonC.ContentsColl.splice(ind + 1, 0, {
					FilePath: '',
					FileName: '',
					ForDate_TMP: null,
					Contents: ''
				})
			} else {
				$scope.CurrentLessonC.ContentsColl.push({
					FilePath: '',
					FileName: '',
					ForDate_TMP: null,
					Contents: ''
				})
			}
		}
	};
	$scope.delContentDetails1 = function (ind) {
		if ($scope.CurrentLessonC.ContentsColl) {
			if ($scope.CurrentLessonC.ContentsColl.length > 1) {
				$scope.CurrentLessonC.ContentsColl.splice(ind, 1);
			}
		}
	};
});