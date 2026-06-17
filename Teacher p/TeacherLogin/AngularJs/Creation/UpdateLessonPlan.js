app.controller('UpdateLessonPlanController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Update Lesson Plan';


	var glbS = GlobalServices;

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();
		//$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();

		$scope.newUpdateLessonPlan = {
			UpdateLessonPlanId: null,
			ClassId: null,
			SubjectId: null,
			Mode: 'Save'
		};

		$scope.ClassColl = [];
		$scope.SectionColl = [];
		$http.get(base_url + "StudentAttendance/Creation/GetClassSection")
			.then(function (data) {
				$scope.ClassSection = data.data.ClassList;
				$scope.SectionClassColl = data.data.SectionList;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				exDialog.openMessage({
					scope: $scope,
					title: $scope.Title,
					icon: "info",
					message: 'Failed: ' + reason
				});
			});
		 
	}



	$scope.GetClassWiseSubjectList = function (classId) {

		$scope.SubjectList = [];
		var para = {
			ClassId: classId
		};
		$scope.loadingstatus = "running";
		 
		if (classId > 0) {
			var para = {
				classId: classId,
				sectionIdColl: 0,
				sectionId: 0
			};
			$http({
				method: 'POST',
				url: base_url + "OnlineClass/Creation/GetSubjectList",
				dataType: "json",
				data: para
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				if (res.data) {
					$scope.SubjectList = res.data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.GetLessonPlanByClassSubject = function (classId, subjectId) {

		if (classId > 0 && subjectId > 0) {
			var para = {
				ClassId: classId,
				SubjectId: subjectId
			};
			$http({
				method: 'POST',
				url: base_url + "LessonPlan/Creation/GetLessonPlanByClassSubject",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {

					$timeout(function () {
						var dd = res.data;
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

		if (lesson.StatusValue == 1 || lesson.StatusValue == 2)
			$('#modal-xl').modal('show');
	};

	$scope.CurrentTopic = {};
	$scope.UpdateTopicStatus = function (lesson, topic) {
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
	$scope.UpdateTopicContentStatus = function (lesson, topic, topicContent) {
		$scope.CurrentLesson = lesson;
		$scope.CurrentTopic = topic;
		$scope.CurrentTopicContent = topicContent;

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

		if (topicContent.StatusValue == 1 || topicContent.StatusValue == 2)
			$('#modal-xl-2').modal('show');


	};

	$scope.StartLesson = function () {

		if ($scope.CurrentLesson && $scope.CurrentLesson.LessonId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			if ($scope.CurrentLesson.StartDateDet)
				$scope.CurrentLesson.StartDate_AD = $filter('date')(new Date($scope.CurrentLesson.StartDateDet.dateAD), 'yyyy-MM-dd');

			$http({
				method: 'POST',
				url: base_url + "LessonPlan/Creation/StartLesson",
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
				url: base_url + "LessonPlan/Creation/EndLesson",
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

		if ($scope.CurrentTopic && $scope.CurrentTopic.LessonId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();


			if ($scope.CurrentTopic.StartDateDet)
				$scope.CurrentTopic.StartDate_AD = $filter('date')(new Date($scope.CurrentTopic.StartDateDet.dateAD), 'yyyy-MM-dd');

			$http({
				method: 'POST',
				url: base_url + "LessonPlan/Creation/StartTopic",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					return formData;
				},
				data: { jsonData: $scope.CurrentTopic }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				hidePleaseWait();

				Swal.fire(res.data.ResponseMSG);

				if (res.data.IsSuccess == true) {
					$scope.CurrentTopic.StatusValue = 2;
					$scope.CurrentTopic.Status = 'In Progress'
					$('#modal-xl-1').modal('hide');
				}

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		}

	}

	$scope.EndTopic = function () {

		if ($scope.CurrentTopic && $scope.CurrentTopic.LessonId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			if ($scope.CurrentTopic.EndDateDet)
				$scope.CurrentTopic.EndDate_AD = $filter('date')(new Date($scope.CurrentTopic.EndDateDet.dateAD), 'yyyy-MM-dd');

			$http({
				method: 'POST',
				url: base_url + "LessonPlan/Creation/EndTopic",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					return formData;
				},
				data: { jsonData: $scope.CurrentTopic }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				hidePleaseWait();

				Swal.fire(res.data.ResponseMSG);

				if (res.data.IsSuccess == true) {
					$scope.CurrentTopic.StatusValue = 3;
					$scope.CurrentTopic.Status = 'Completed'
					$('#modal-xl-1').modal('hide');
				}

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		}

	}


	$scope.StartTopicContent = function () {

		if ($scope.CurrentTopicContent && $scope.CurrentTopicContent.LessonId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();


			if ($scope.CurrentTopicContent.StartDateDet)
				$scope.CurrentTopicContent.StartDate_AD = $filter('date')(new Date($scope.CurrentTopicContent.StartDateDet.dateAD), 'yyyy-MM-dd');

			$http({
				method: 'POST',
				url: base_url + "LessonPlan/Creation/StartTopicContent",
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
				url: base_url + "LessonPlan/Creation/EndTopicContent",
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