app.controller('UpdateLessonPlanController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Update Lesson Plan';


	var glbS = GlobalServices;

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();
		//$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();

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

		$scope.newUpdateLessonPlan = {
			UpdateLessonPlanId: null,
			ClassId: null,
			SubjectId: null,
			Mode: 'Save'
		};

		//$scope.ClassSection = {};
		//glbS.getClassSectionList().then(function (res) {
		//	$scope.ClassSection = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

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

	}


	$scope.ClearUpdateTopicStatus = function () {
		$timeout(function () {
			if ($scope.CurrentTopic) {
				$scope.CurrentTopic.StartDate_TMP = new Date();
				$scope.CurrentTopic.EndDate_TMP = new Date();
				$scope.CurrentTopic.StartRemarks = '';
				$scope.CurrentTopic.EndRemarks = '';
			}
		});
	};

	$scope.ClearUpdateTopicContentStatus = function () {
		$timeout(function () {
			if ($scope.CurrentTopicContent) {
				$scope.CurrentTopicContent.StartDate_TMP = new Date();
				$scope.CurrentTopicContent.EndDate_TMP = new Date();
				$scope.CurrentTopicContent.StartRemarks = '';
				$scope.CurrentTopicContent.EndRemarks = '';
			}
		});
	};

	$scope.ClearUpdatelessonStatus = function () {
		$timeout(function () {
			if ($scope.CurrentLesson) {
				$scope.CurrentLesson.StartDate_TMP = new Date();
				$scope.CurrentLesson.EndDate_TMP = new Date();
				$scope.CurrentLesson.StartRemarks = '';
				$scope.CurrentLesson.EndRemarks = '';
			}
		});
	};

	$scope.GetClassWiseSubjectList = function () {

		$scope.SubjectList = [];
		var para = {
			ClassId: $scope.newUpdateLessonPlan.SelectedClass.ClassId,
			BatchId: $scope.newUpdateLessonPlan.BatchId || null,
			SemesterId: $scope.newUpdateLessonPlan.SemesterId || null,
			ClassYearId: $scope.newUpdateLessonPlan.ClassYearId ||null
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
					$scope.SubjectList = res.data.Data;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetLessonPlanByClassSubject = function () {
		if (!$scope.newUpdateLessonPlan.SubjectId ||
			$scope.newUpdateLessonPlan.SubjectId == 0) {
			$scope.newUpdateLessonPlan.DetailsColl = [];
			return; 
		}

		if ($scope.newUpdateLessonPlan.SelectedClass.ClassId > 0 && $scope.newUpdateLessonPlan.SubjectId > 0) {
			//var sectionIdColl = null;
			//var sectionIdColl = ($scope.newUpdateLessonPlan.SectionIdColl && $scope.newUpdateLessonPlan.SectionIdColl.length > 0)
			//	? $scope.newUpdateLessonPlan.SectionIdColl.join(',')
			//	: '';

			var para = {
				ClassId: $scope.newUpdateLessonPlan.SelectedClass.ClassId,
				SubjectId: $scope.newUpdateLessonPlan.SubjectId,
				SectionId: $scope.newUpdateLessonPlan.SectionId || null,
				/*SectionIdColl: ($scope.newUpdateLessonPlan.SectionIdColl ? $scope.newUpdateLessonPlan.SectionIdColl.toString() : ''),*/
				BatchId: $scope.newUpdateLessonPlan.BatchId || null,
				ClassYearId: $scope.newUpdateLessonPlan.ClassYearId || null,
				SemesterId: $scope.newUpdateLessonPlan.SemesterId || null
			};
			$http({
				method: 'POST',
				url: base_url + "Elearning/Creation/GetLessonPlanForUpdate",
				/*url: base_url + "Academic/Creation/GetLessonPlanByClassSubject",*/
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {

					$timeout(function () {
						var dd = res.data.Data;
						$scope.newUpdateLessonPlan.CoverFilePath = dd.CoverFilePath;
						$scope.newUpdateLessonPlan.NoOfLesson = dd.NoOfLesson;
						$scope.newUpdateLessonPlan.TranId = dd.TranId;
						$scope.newUpdateLessonPlan.DetailsColl = dd.DetailsColl;

						angular.forEach($scope.newUpdateLessonPlan.DetailsColl, function (dc) {

							if (!dc.TopicColl || dc.TopicColl.length == 0) {
								dc.TopicColl = [];
								dc.TopicColl.push({ SNo: 1, TopicName: '' });
							}
						});

						if ($scope.newUpdateLessonPlan.DetailsColl.length > 0)
							$scope.CurrentLesson = $scope.newUpdateLessonPlan.DetailsColl[0];
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.CurrentLesson = {};
	$scope.UpdateLessonStatus = function (lesson) {
		$scope.CurrentLesson = lesson;
		$scope.CurrentLesson.NewStatus = '';

		if (lesson.StatusValue == 1) {
			$scope.CurrentLesson.NewStatus = 'Start Now';
			$scope.CurrentLesson.StartDate_TMP = new Date();
		}
		else if (lesson.StatusValue == 2) {
			$scope.CurrentLesson.NewStatus = 'Completed';
			$scope.CurrentLesson.EndDate_TMP = new Date();
			
        }
			
		else
			$scope.CurrentLesson.NewStatus = lesson.Status;

		if(lesson.StatusValue==1 || lesson.StatusValue==2)
			$('#modal-xl').modal('show');
	};

	$scope.CurrentTopic = {};
	$scope.UpdateTopicStatus = function (lesson, topic)
	{
		$scope.CurrentLesson = lesson;
		$scope.CurrentTopic = topic;
		 
		$scope.CurrentTopic.NewStatus = '';

		if (topic.StatusValue == 1) {
			$scope.CurrentTopic.NewStatus = 'Start Now';
			$scope.CurrentTopic.StartDate_TMP = new Date();
		}
		else if (topic.StatusValue == 2) {
			$scope.CurrentTopic.NewStatus = 'Completed';
			$scope.CurrentTopic.EndDate_TMP = new Date();
		}
		else
			$scope.CurrentTopic.NewStatus = lesson.Status;

		if (topic.StatusValue == 1 || topic.StatusValue == 2)
			$('#modal-xl-1').modal('show');

		
	};


	$scope.CurrentTopicContent = {};
	$scope.UpdateTopicContentStatus = function (lesson, topic, topicContent, $index) {
		$scope.CurrentLesson = lesson;
		$scope.CurrentTopic = topic;
		$scope.CurrentTopicContent = topicContent;
		$scope.CurrentTopicContent.ContentSNo = topicContent.SNo;
		$scope.CurrentTopicContent.NewStatus = '';

		if (topicContent.StatusValue == 1) {
			$scope.CurrentTopicContent.NewStatus = 'Start Now';
			$scope.CurrentTopicContent.StartDate_TMP = new Date();
		}
		else if (topicContent.StatusValue == 2) {
			$scope.CurrentTopicContent.NewStatus = 'Completed';
			$scope.CurrentTopicContent.EndDate_TMP = new Date();
		}
		else
			$scope.CurrentTopicContent.NewStatus = topicContent.Status;

		$scope.CurrentTopicContent.ClassId = $scope.newUpdateLessonPlan.SelectedClass ? $scope.newUpdateLessonPlan.SelectedClass.ClassId : 0;
		$scope.CurrentTopicContent.SectionId = $scope.newUpdateLessonPlan.SectionId || null;
		$scope.CurrentTopicContent.BatchId = $scope.newUpdateLessonPlan.BatchId || null;
		$scope.CurrentTopicContent.SemesterId = $scope.newUpdateLessonPlan.SemesterId || null;
		$scope.CurrentTopicContent.ClassYearId = $scope.newUpdateLessonPlan.ClassYearId || null;
		$scope.CurrentTopicContent.SubjectId = $scope.newUpdateLessonPlan.SubjectId || null;

		if (topicContent.StatusValue == 1 || topicContent.StatusValue == 2)
			$('#modal-xl-2').modal('show');


	};

	$scope.StartLesson = function () {

		if ($scope.CurrentLesson && $scope.CurrentLesson.LessonId>0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			if ($scope.CurrentLesson.StartDateDet)
				$scope.CurrentLesson.StartDate_AD = $filter('date')(new Date($scope.CurrentLesson.StartDateDet.dateAD), 'yyyy-MM-dd');

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/StartLesson",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					return formData;
				},
				data: { jsonData: $scope.CurrentLesson }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				hidePleaseWait();

				Swal.fire(res.data.ResponseMSG);

				if (res.data.IsSuccess == true) {
					$scope.CurrentLesson.StatusValue = 2;
					$scope.CurrentLesson.Status = 'In Progress'
					$('#modal-xl').modal('hide');
                }
					

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		}

	}

	$scope.EndLesson = function () {

		if ($scope.CurrentLesson && $scope.CurrentLesson.LessonId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();


			if ($scope.CurrentLesson.EndDateDet)
				$scope.CurrentLesson.EndDate_AD = $filter('date')(new Date($scope.CurrentLesson.EndDateDet.dateAD), 'yyyy-MM-dd');

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/EndLesson",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					return formData;
				},
				data: { jsonData: $scope.CurrentLesson }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				hidePleaseWait();

				Swal.fire(res.data.ResponseMSG);

				if (res.data.IsSuccess == true) {
					$scope.CurrentLesson.StatusValue = 3;
					$scope.CurrentLesson.Status = 'Completed'
					$('#modal-xl').modal('hide');
				}

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		}

	}


	$scope.StartTopic = function () {

		if (!$scope.CurrentTopic || $scope.CurrentTopic.LessonId <= 0) return;

		$scope.loadingstatus = "running";
		showPleaseWait();

		// Build payload
		var payload = angular.extend({}, $scope.CurrentTopic, {
			LessonSNo: $scope.CurrentLesson.SNo,
			BatchId: $scope.newUpdateLessonPlan.BatchId ||null,
			ClassId: $scope.newUpdateLessonPlan.SelectedClass.ClassId,
			SectionId: $scope.newUpdateLessonPlan.SectionId || null,
			SubjectId: $scope.newUpdateLessonPlan.SubjectId || null,
			ClassYearId: $scope.newUpdateLessonPlan.ClassYearId || null,
			SemesterId: $scope.newUpdateLessonPlan.SemesterId || null,
			StatusValue: 2,                     // 2 = In Progress
			StartRemarks: $scope.CurrentTopic.StartRemarks || '',
			TranId: $scope.newUpdateLessonPlan.TranId||0
		});

		// Convert Start Date
		if ($scope.CurrentTopic.StartDateDet && $scope.CurrentTopic.StartDateDet.dateAD) {
			payload.StartDate_AD = $filter('date')(
				new Date($scope.CurrentTopic.StartDateDet.dateAD),
				'yyyy-MM-dd'
			);
		}

		// POST to server
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/StartTopic",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var fd = new FormData();
				fd.append("jsonData", angular.toJson(data));
				return fd;
			},
			data: payload

		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess) {
				$scope.CurrentTopic.StatusValue = 2;
				$scope.CurrentTopic.Status = "In Progress";
				$('#modal-xl-1').modal('hide');
			}

		}, function () {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	};


	$scope.EndTopic = function () {

		if (!$scope.CurrentTopic || $scope.CurrentTopic.LessonId <= 0) return;

		$scope.loadingstatus = "running";
		showPleaseWait();

		var payload = angular.extend({}, $scope.CurrentTopic, {
			LessonSNo: $scope.CurrentLesson.SNo,
			BatchId: $scope.newUpdateLessonPlan.BatchId || null,
			ClassId: $scope.newUpdateLessonPlan.SelectedClass.ClassId,
			SectionId: $scope.newUpdateLessonPlan.SectionId || null,
			SubjectId: $scope.newUpdateLessonPlan.SubjectId || null,
			ClassYearId: $scope.newUpdateLessonPlan.ClassYearId || null,
			SemesterId: $scope.newUpdateLessonPlan.SemesterId || null,
			StatusValue: 3,                     // 3 = Completed
			EndRemarks: $scope.CurrentTopic.EndRemarks || '',
			TranId: $scope.newUpdateLessonPlan.TranId || 0
		});

		// Convert End Date
		if ($scope.CurrentTopic.EndDate_TMP) {
			payload.EndDate_AD = $filter('date')(
				new Date($scope.CurrentTopic.EndDate_TMP),
				'yyyy-MM-dd'
			);
		}

		// POST to server
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/EndTopic",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var fd = new FormData();
				fd.append("jsonData", angular.toJson(data));
				return fd;
			},
			data: payload

		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess) {
				$scope.CurrentTopic.StatusValue = 3;
				$scope.CurrentTopic.Status = "Completed";
				$scope.GetLessonPlanByClassSubject();
				$('#modal-xl-1').modal('hide');
			}

		}, function () {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	};


	//$scope.StartTopic = function () {

	//	if ($scope.CurrentTopic && $scope.CurrentTopic.LessonId > 0) {
	//		$scope.loadingstatus = "running";
	//		showPleaseWait();


	//		if ($scope.CurrentTopic.StartDateDet)
	//			$scope.CurrentTopic.StartDate_AD = $filter('date')(new Date($scope.CurrentTopic.StartDateDet.dateAD), 'yyyy-MM-dd');

	//		$http({
	//			method: 'POST',
	//			url: base_url + "Academic/Creation/StartTopic",
	//			headers: { 'Content-Type': undefined },

	//			transformRequest: function (data) {

	//				var formData = new FormData();
	//				formData.append("jsonData", angular.toJson(data.jsonData));

	//				return formData;
	//			},
	//			data: {
	//				jsonData: angular.extend({}, $scope.CurrentTopic, {
	//					LessonSNo: $scope.CurrentLesson.SNo
	//				})
	//			}
	//			/*data: { jsonData: $scope.CurrentTopic }*/
	//		}).then(function (res) {

	//			$scope.loadingstatus = "stop";
	//			hidePleaseWait();

	//			Swal.fire(res.data.ResponseMSG);

	//			if (res.data.IsSuccess == true) {
	//				$scope.CurrentTopic.StatusValue = 2;
	//				$scope.CurrentTopic.Status = 'In Progress'
	//				$('#modal-xl-1').modal('hide');
	//			}

	//		}, function (errormessage) {
	//			hidePleaseWait();
	//			$scope.loadingstatus = "stop";

	//		});
	//	}

	//}

	//$scope.EndTopic = function () {

	//	if ($scope.CurrentTopic && $scope.CurrentTopic.LessonId > 0) {
	//		$scope.loadingstatus = "running";
	//		showPleaseWait();

	//		if ($scope.CurrentTopic.EndDateDet)
	//			$scope.CurrentTopic.EndDate_AD = $filter('date')(new Date($scope.CurrentTopic.EndDateDet.dateAD), 'yyyy-MM-dd');

	//		$http({
	//			method: 'POST',
	//			url: base_url + "Academic/Creation/EndTopic",
	//			headers: { 'Content-Type': undefined },

	//			transformRequest: function (data) {

	//				var formData = new FormData();
	//				formData.append("jsonData", angular.toJson(data.jsonData));

	//				return formData;
	//			},
	//			data: { jsonData: $scope.CurrentTopic }
	//		}).then(function (res) {

	//			$scope.loadingstatus = "stop";
	//			hidePleaseWait();

	//			Swal.fire(res.data.ResponseMSG);

	//			if (res.data.IsSuccess == true) {
	//				$scope.CurrentTopic.StatusValue = 3;
	//				$scope.CurrentTopic.Status = 'Completed'
	//				$('#modal-xl-1').modal('hide');
	//			}

	//		}, function (errormessage) {
	//			hidePleaseWait();
	//			$scope.loadingstatus = "stop";

	//		});
	//	}

	//}


	$scope.StartTopicContent = function () {

		if ($scope.CurrentTopicContent && $scope.CurrentTopicContent.LessonId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();


			if ($scope.CurrentTopicContent.StartDateDet)
				$scope.CurrentTopicContent.StartDate_AD = $filter('date')(new Date($scope.CurrentTopicContent.StartDateDet.dateAD), 'yyyy-MM-dd');

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/StartTopicContent",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					return formData;
				},
				data: { jsonData: $scope.CurrentTopicContent }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				hidePleaseWait();

				Swal.fire(res.data.ResponseMSG);

				if (res.data.IsSuccess == true) {
					$scope.CurrentTopicContent.StatusValue = 2;
					$scope.CurrentTopicContent.Status = 'In Progress'
					$('#modal-xl-2').modal('hide');
				}

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		}

	}

	$scope.EndTopicContent = function () {

		if ($scope.CurrentTopicContent && $scope.CurrentTopicContent.LessonId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			if ($scope.CurrentTopicContent.EndDateDet)
				$scope.CurrentTopicContent.EndDate_AD = $filter('date')(new Date($scope.CurrentTopicContent.EndDateDet.dateAD), 'yyyy-MM-dd');

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/EndTopicContent",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					return formData;
				},
				data: { jsonData: $scope.CurrentTopicContent }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				hidePleaseWait();

				Swal.fire(res.data.ResponseMSG);

				if (res.data.IsSuccess == true) {
					$scope.CurrentTopicContent.StatusValue = 3;
					$scope.CurrentTopicContent.Status = 'Completed'
					$('#modal-xl-2').modal('hide');
				}

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		}

	}
});