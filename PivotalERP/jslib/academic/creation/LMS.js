app.controller('LMSController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'LMS';
	var glbS = GlobalServices;

	OnClickDefault();
	String.prototype.isEmpty = function () {
		return (this.length === 0 || !this.trim());
	};

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();

		$scope.newAddLessonContent = {
			ClassId: null,
			SubjectId: null,
			BatchId: null,
			ClassYearId: null,
			SemesterId: null,
			NoOfLesson: 0,
			DetailsColl: [],
			Mode: 'Save'
		};
		$scope.newLessonContentList = {
			ClassId: null,
			SubjectId: null,
			BatchId: null,
			ClassYearId: null,
			SemesterId: null,
			DetailsColl: [],
			Mode: 'Save'
		};

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;


			if ($scope.AcademicConfig.ActiveFaculty == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' });
				if (findInd != -1)
					$scope.gridOptions.columnDefs.splice(findInd, 1);
			}

			if ($scope.AcademicConfig.ActiveLevel == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Level' });
				if (findInd != -1)
					$scope.gridOptions.columnDefs.splice(findInd, 1);

			}

			if ($scope.AcademicConfig.ActiveSemester == false) {


				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' });
				if (findInd != -1)
					$scope.gridOptions.columnDefs.splice(findInd, 1);

			} else {
				$scope.SelectedClassSemesterList = [];
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveBatch == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Batch' });
				if (findInd != -1)
					$scope.gridOptions.columnDefs.splice(findInd, 1);

			} else {
				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

			if ($scope.AcademicConfig.ActiveClassYear == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' });
				if (findInd != -1)
					$scope.gridOptions.columnDefs.splice(findInd, 1);

			}
			else {
				$scope.ClassYearList = [];
				$scope.SelectedClassClassYearList = [];
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

			//$scope.gridApi.grid.refresh();
			//$scope.gridApi2.grid.refresh();
			//$scope.gridApi3.grid.refresh();
			//$scope.gridApi4.grid.refresh();
			//$scope.gridApi5.grid.refresh();


		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		//Ends


		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		//$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();
	}
	//Done Create ClearFucntion

	$scope.ResetQuizForm = function () {

		// Reset Quiz main data
		if ($scope.CurrentLessionTopic && $scope.CurrentLessionTopic.Quiz) {
			$scope.CurrentLessionTopic.Quiz.Topic = "";
			$scope.CurrentLessionTopic.Quiz.NoOfQuestion = null;
			$scope.CurrentLessionTopic.Quiz.Duration = null;
			$scope.CurrentLessionTopic.Quiz.Description = "";
			$scope.CurrentLessionTopic.Quiz.FullMark = null;
			$scope.CurrentLessionTopic.Quiz.PassMark = null;
			$scope.ChangeNoOfQuestion();
			// reset question collection if needed
			if ($scope.CurrentLessionTopic.Quiz.QuestionColl) {
				angular.forEach($scope.CurrentLessionTopic.Quiz.QuestionColl, function (q) {
					q.QuestionContent = "";
					q.QuestionType = 1;
					q.ImageData = null;
					q.ContentPath = null;
					q.AnswerColl = [];
					q.AnswerSNo = null;
				});
			}
		}

		// Reset selected question
		$scope.SelectedQuestion = {
			SNo: 1,
			QuestionType: 1,
			AnswerType: 1,
			QuestionContent: "",
			ImageData: null,
			ContentPath: null,
			AnswerColl: [],
			AnswerSNo: null,
			Duration: null,
			Mark: null
		};

		angular.element("input[type='file']").val(null);

	};

	$scope.ResetContent = function () {
		$scope.CurrentLessionTopic.ContentsColl = [];
		$scope.CurrentLessionTopic.TMPContentsColl = null;
		angular.element("input[type='file']").val(null);
	};

	$scope.ClearAddLessonContent = function () {
		$scope.newAddLessonContent = {
			ClassId: null,
			SubjectId: null,
			BatchId: null,
			ClassYearId: null,
			SemesterId: null,
			NoOfLesson: 0,
			DetailsColl: [],
			Mode: 'Save'
		};
    }
	$scope.ClearLessonContentList = function () {
		$scope.newLessonContentList = {
			ClassId: null,
			SubjectId: null,
			BatchId: null,
			ClassYearId: null,
			SemesterId: null,
			DetailsColl: [],
			Mode: 'Save'
		};
	}
	$scope.$watch('newAddLessonContent.SelectedClass', function (newVal, oldVal) {
		if (newVal && newVal !== oldVal) {
			// Clear only if the selected class actually changed
			$scope.newAddLessonContent.ClassYearId = null;
			$scope.newAddLessonContent.SemesterId = null;
		}
	});

	//Done: Add Parameter
	$scope.GetClassWiseSubjectList = function () {
		$scope.SubjectList = [];
		$scope.newAddLessonContent.NoOfLesson = 0;
		$scope.newAddLessonContent.TranId = 0;
        $scope.newAddLessonContent.DetailsColl = [];

        var para = {
            ClassId: $scope.newAddLessonContent.SelectedClass.ClassId,
            BatchId: $scope.newAddLessonContent.BatchId || null,
            ClassYearId: $scope.newAddLessonContent.ClassYearId || null,
            SemesterId: $scope.newAddLessonContent.SemesterId || null,
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

	$scope.GetLessonPlanByClassSubject = function (classId, subjectId) {

		$scope.newAddLessonContent.NoOfLesson = 0;
		$scope.newAddLessonContent.TranId = 0;
		$scope.newAddLessonContent.DetailsColl = [];
		$scope.CurrentLessionTopic = {};

		if (classId > 0 && subjectId > 0) {
			var para = {
				ClassId: classId,
				SubjectId: subjectId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetLessonPlanByClassSubject",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {

					$timeout(function () {
						var dd = res.data.Data;
						$scope.newAddLessonContent.NoOfLesson = dd.NoOfLesson;
						$scope.newAddLessonContent.TranId = dd.TranId;
						$scope.newAddLessonContent.DetailsColl = dd.DetailsColl;

					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.CurrentLessionTopic = {};
	$scope.ClickOnTopic = function (lession, topic) {
		$scope.selectedLesson = lession;
		$scope.selectedTopic = topic;
		$scope.selectedKey = lession.LessonId + '_' + topic.SNo;
		$scope.loadingstatus = "running";
		showPleaseWait();

		topic.ContentsColl = [];
		topic.VideoColl = [];
		topic.Quiz = {};

		$scope.CurrentLessionTopic = {};
		$scope.CurrentLessionTopic = {
			LessonId: lession.LessonId,
			//new
			ClassId: $scope.newAddLessonContent.SelectedClass?.ClassId,
			BatchId: $scope.newAddLessonContent.BatchId || null,
			SemesterId: $scope.newAddLessonContent.SemesterId || null,
			ClassYearId: $scope.newAddLessonContent.ClassYearId || null,
			SubjectId: $scope.newAddLessonContent.SubjectId,
			//Ends
			LessonSNo:lession.SNo,
			TopicSNo: topic.SNo,
			LessonName: lession.LessonName,
			TopicName: topic.TopicName,
			ContentsColl: [],
			VideoColl: [],
			Quiz: {
				Topic: '',
				Description: '',
				NoOfQuestion: 0,
				FullMark: 0,
				PassMark: 0,
				Duration: 0,
				QuestionColl:[]
			}
		};

		var para = {
			//LessonId: lession.LessonId,
			ClassId: $scope.newAddLessonContent.SelectedClass?.ClassId,
			BatchId: $scope.newAddLessonContent.BatchId || null,
			SemesterId: $scope.newAddLessonContent.SemesterId || null,
			ClassYearId: $scope.newAddLessonContent.ClassYearId || null,
			SubjectId: $scope.newAddLessonContent.SubjectId,
			//Added Ends
			LessonSNo: lession.SNo,
			TopicSNo:topic.SNo
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetLessonTopicVideo",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				angular.forEach(res.data.Data, function (dt) {
					$scope.CurrentLessionTopic.VideoColl.push({
						Title: dt.Title,
						Link: dt.Link,
						Remarks:dt.Remarks
					});
				});

				if ($scope.CurrentLessionTopic.VideoColl.length == 0) {
					$scope.CurrentLessionTopic.VideoColl.push({
						Title: '',
						Link: '',
						Remarks: ''
					});
                } 

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetLessonTopicContent",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				angular.forEach(res.data.Data, function (dt) {
					$scope.CurrentLessionTopic.ContentsColl.push({
						FilePath: dt.FilePath,
						FileName: dt.FileName,
						FileType: dt.FileType
					});
				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.LessonTopicQuizList = [];
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetLessonTopicQuizList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$scope.LessonTopicQuizList = res.data.Data;
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		//$scope.loadingstatus = "running";
		//showPleaseWait();
		//$http({
		//	method: 'POST',
		//	url: base_url + "Academic/Creation/GetLessonTopicQuiz",
		//	dataType: "json",
		//	data: JSON.stringify(para)
		//}).then(function (res) {
		//	hidePleaseWait();
		//	$scope.loadingstatus = "stop";
		//	if (res.data.IsSuccess && res.data.Data) {

		//		$scope.CurrentLessionTopic.Quiz = res.data.Data;
		//		if ($scope.CurrentLessionTopic.Quiz.QuestionColl.length > 0) {
		//			$scope.SelectedQuestion = angular.copy(
		//				$scope.CurrentLessionTopic.Quiz.QuestionColl[0]
		//			);
		//		}
		//	} else {
		//		Swal.fire(res.data.ResponseMSG);
		//	}

		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});
	}


	$scope.GetLessonTopicQuizId = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			QuizId: refData.QuizId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetLessonTopicQuiz",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CurrentLessionTopic.Quiz = res.data.Data;

				if ($scope.CurrentLessionTopic.Quiz.QuestionColl.length > 0) {
					$scope.SelectedQuestion = angular.copy(
						$scope.CurrentLessionTopic.Quiz.QuestionColl[0]
					);
				}
				

				/*$scope.newExamType.Mode = 'Modify';*/

				document.getElementById('quizlist').style.display = "none";
				document.getElementById('quizform').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.ChooseContentFile = function () {

		if (!$scope.CurrentLessionTopic.ContentsColl)
			$scope.CurrentLessionTopic.ContentsColl = [];

		$timeout(function () {

			$scope.loadingstatus = "running";
			showPleaseWait();


			angular.forEach($scope.CurrentLessionTopic.TMPContentsColl, function (cc) {

				var reader = new FileReader();
				reader.onload = function (evt)
				{
					$scope.$apply(function () {
						var newContents = {
							FileName: cc.name,
							FilePath: '',
							File:cc,
							DocData: evt.target.result
						};
						$scope.CurrentLessionTopic.ContentsColl.push(newContents);
						$scope.CurrentLessionTopic.TMPContent = null;
						$scope.CurrentLessionTopic.TMPContentsColl = [];

						$scope.loadingstatus = "stop";
						hidePleaseWait();

					});
					 
				};
				reader.readAsDataURL(cc);

				
			});
		});
	}

	$scope.delContentFile = function (ind) {
		if ($scope.CurrentLessionTopic.ContentsColl) {
			if ($scope.CurrentLessionTopic.ContentsColl.length > 0) {
				$scope.CurrentLessionTopic.ContentsColl.splice(ind, 1);
			}
		}
	};

	$scope.CallSaveUpdateLessonTopicContent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var dataColl = [];
		angular.forEach($scope.CurrentLessionTopic.ContentsColl, function (cc) {
			var newBeData = {
				//Added
				ClassId: $scope.newAddLessonContent.SelectedClass.ClassId,
				BatchId: $scope.newAddLessonContent.BatchId ||null,
				SemesterId: $scope.newAddLessonContent.SemesterId || null,
				ClassYearId: $scope.newAddLessonContent.ClassYearId || null,
				SubjectId: $scope.newAddLessonContent.SubjectId,
				//Ends
				LessonId: $scope.CurrentLessionTopic.LessonId,
				LessonSNo: $scope.CurrentLessionTopic.LessonSNo,
				TopicSNo: $scope.CurrentLessionTopic.TopicSNo,
				FileName: cc.FileName,
				FilePath:cc.FilePath
			};
			dataColl.push(newBeData);
		});
		
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveLessonTopicContent",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				} 

				return formData;
			},
			data: { jsonData: dataColl, files: $scope.CurrentLessionTopic.ContentsColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
			 

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.AddVideo = function (ind) {
		if ($scope.CurrentLessionTopic.VideoColl) {
			if ($scope.CurrentLessionTopic.VideoColl.length > ind + 1) {
				$scope.CurrentLessionTopic.VideoColl.splice(ind + 1, 0, {
					Title: '',
					Link: '',
					Remarks: ''
				})
			} else {
				$scope.CurrentLessionTopic.VideoColl.push({
					Title: '',
					Link: '',
					Remarks: ''
				})
			}
		}

	};
	$scope.delVideo = function (ind) {
		if ($scope.CurrentLessionTopic.VideoColl) {
			if ($scope.CurrentLessionTopic.VideoColl.length > 1) {
				$scope.CurrentLessionTopic.VideoColl.splice(ind, 1);
			}
		}
	};

	$scope.CallSaveUpdateLessonTopicVideo = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var dataColl = [];
		angular.forEach($scope.CurrentLessionTopic.VideoColl, function (cc) {
			var newBeData = {
				//Added
				ClassId: $scope.newAddLessonContent.SelectedClass.ClassId,
				BatchId: $scope.newAddLessonContent.BatchId || null,
				SemesterId: $scope.newAddLessonContent.SemesterId || null,
				ClassYearId: $scope.newAddLessonContent.ClassYearId || null,
				SubjectId: $scope.newAddLessonContent.SubjectId,
				//Ends
				LessonId: $scope.CurrentLessionTopic.LessonId,
				LessonSNo: $scope.CurrentLessionTopic.LessonSNo,
				TopicSNo: $scope.CurrentLessionTopic.TopicSNo,
				Title: cc.Title,
				Link: cc.Link,
				Remarks:cc.Remarks
			};
			dataColl.push(newBeData);
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveLessonTopicVideo",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}

				return formData;
			},
			data: { jsonData: dataColl, files: null }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.ChangeNoOfQuestion = function () {

		var quetionColl = mx($scope.CurrentLessionTopic.Quiz.QuestionColl);
		for (var qSNo = 1; qSNo <= $scope.CurrentLessionTopic.Quiz.NoOfQuestion; qSNo++) {

			var find = quetionColl.firstOrDefault(p1 => p1.SNo == qSNo);
			if (find) {

			} else {
				$scope.CurrentLessionTopic.Quiz.QuestionColl.push({
					SNo: qSNo,
					QuestionType: 1,
					QuestionContent: '',
					AnswerType: 1,
					ContentPath: '',
					Duration: 0,
					Mark: 0,
					AnswerSNo: 0,
					ImageData: null,
					Files_TMP: null,
					AnswerColl: [{ SNo:1,AnswerType:1,ContentPath:'',AnswerContent:'' }]
				});
            }
		}

		var totalExistsQuestion = $scope.CurrentLessionTopic.Quiz.QuestionColl.length;

		for (var qSNo = $scope.CurrentLessionTopic.Quiz.NoOfQuestion + 1; qSNo <= totalExistsQuestion; qSNo++) {
			var lastInd = $scope.CurrentLessionTopic.Quiz.QuestionColl.length - 1;
			$scope.CurrentLessionTopic.Quiz.QuestionColl.splice(lastInd, 1);
		}

	}

	$scope.SelectedQuestion = {
		QuestionContent: '',
		ContentPath:'',
		SNo: 0,
		Duration: 0,
		Mark: 0,
		AnswerSNo: 0,
		QuestionType: 1,
		AnswerType:1,
		AnswerColl: [],
		ImageData: null,
		Files_TMP:null
	};
	//$scope.ClickOnQuestionBtn = function (que) {
	//	document.getElementById("imgQuestion").src = "''";
	//	$timeout(function () {
	//		$scope.$apply(function () {
	//			var que1 = mx($scope.CurrentLessionTopic.Quiz.QuestionColl).firstOrDefault(p1 => p1.SNo == $scope.SelectedQuestion.SNo);
	//			if (que1) {
	//				que1.QuestionContent = $scope.SelectedQuestion.QuestionContent;
	//				que1.SNo = $scope.SelectedQuestion.SNo;
	//				que1.QuestionType = $scope.SelectedQuestion.QuestionType;
	//				que1.AnswerType = $scope.SelectedQuestion.AnswerType;
	//				que1.AnswerColl = $scope.SelectedQuestion.AnswerColl;
	//				que1.Duration=$scope.SelectedQuestion.Duration;
	//				que1.Mark = $scope.SelectedQuestion.Mark;
	//				que1.AnswerSNo = $scope.SelectedQuestion.AnswerSNo;
	//				que1.ImageData = $scope.SelectedQuestion.ImageData;
	//				que1.ContentPath = $scope.SelectedQuestion.ContentPath;
	//				que1.Files_TMP = $scope.SelectedQuestion.Files_TMP;
 //               }				
	//		});
	//	})

	//	$timeout(function () {
	//		$scope.$apply(function () {
	//			$scope.SelectedQuestion.QuestionContent = que.QuestionContent;
	//			$scope.SelectedQuestion.SNo = que.SNo;
	//			$scope.SelectedQuestion.QuestionType = que.QuestionType;
	//			$scope.SelectedQuestion.AnswerType = que.AnswerType;
	//			$scope.SelectedQuestion.AnswerColl = que.AnswerColl;
	//			$scope.SelectedQuestion.Duration = que.Duration;
	//			$scope.SelectedQuestion.Mark = que.Mark;
	//			$scope.SelectedQuestion.AnswerSNo = que.AnswerSNo;
	//			$scope.SelectedQuestion.ImageData = que.ImageData;
	//			$scope.SelectedQuestion.ContentPath = que.ContentPath;
	//			$scope.SelectedQuestion.Files_TMP=que.Files_TMP;
	//		});
 //       })
		
	//}

	$scope.ClickOnQuestionBtn = function (que) {
		var img = document.getElementById("imgQuestion");
		if (img) img.src = "";

		if ($scope.SelectedQuestion) {

			var index = $scope.CurrentLessionTopic.Quiz.QuestionColl
				.findIndex(x => x.SNo === $scope.SelectedQuestion.SNo);

			if (index > 0) {

				var target = $scope.CurrentLessionTopic.Quiz.QuestionColl[index];

				target.QuestionContent = $scope.SelectedQuestion.QuestionContent;
				target.QuestionType = $scope.SelectedQuestion.QuestionType;
				target.AnswerType = $scope.SelectedQuestion.AnswerType;
				target.AnswerColl = $scope.SelectedQuestion.AnswerColl;
				target.Duration = $scope.SelectedQuestion.Duration;
				target.Mark = $scope.SelectedQuestion.Mark;
				target.AnswerSNo = $scope.SelectedQuestion.AnswerSNo;
				target.ImageData = $scope.SelectedQuestion.ImageData;
				target.ContentPath = $scope.SelectedQuestion.ContentPath;

				target.Files_TMP = $scope.SelectedQuestion.Files_TMP;
			}
		}
		var selected = $scope.CurrentLessionTopic.Quiz.QuestionColl
			.find(x => x.SNo === que.SNo);

		$scope.SelectedQuestion = selected || que;

	};

	$scope.CallSaveUpdateLessonTopicQuiz = function () {
		if (!$scope.CurrentLessionTopic.Quiz.Topic || $scope.CurrentLessionTopic.Quiz.Topic.trim() === "") {
			Swal.fire( "Title is required");
			return;
		}

		if ($scope.CurrentLessionTopic.Quiz.NoOfQuestion == null || $scope.CurrentLessionTopic.Quiz.NoOfQuestion === "") {
			Swal.fire("Validation", "No of Questions is required");
			return;
		}

		if ($scope.CurrentLessionTopic.Quiz.NoOfQuestion <= 0) {
			Swal.fire("No of Questions must be greater than 0");
			return;
		}

		if ($scope.CurrentLessionTopic.Quiz.FullMark != null && $scope.CurrentLessionTopic.Quiz.PassMark != null) {
			if ($scope.CurrentLessionTopic.Quiz.PassMark > $scope.CurrentLessionTopic.Quiz.FullMark) {
				Swal.fire("Pass Mark cannot be greater than Full Mark");
				return;
			}
		}
		// Full Mark validation
		if ($scope.CurrentLessionTopic.Quiz.FullMark != null) {
			if ($scope.CurrentLessionTopic.Quiz.FullMark < 0) {
				Swal.fire( "Full Mark cannot be negative");
				return;
			}
		}

		// Pass Mark validation
		if ($scope.CurrentLessionTopic.Quiz.PassMark != null) {
			if ($scope.CurrentLessionTopic.Quiz.PassMark < 0) {
				Swal.fire( "Pass Mark cannot be negative");
				return;
			}
		}
		if ($scope.CurrentLessionTopic.Quiz.Duration == null || $scope.CurrentLessionTopic.Quiz.Duration === "") {
			Swal.fire("Duration is required");
			return;
		}

		if ($scope.CurrentLessionTopic.Quiz.Duration <= 0) {
			Swal.fire("Duration must be greater than 0");
			return;
		}
		$scope.loadingstatus = "running";
		showPleaseWait();
		if ($scope.SelectedQuestion) {
			var que1 = mx($scope.CurrentLessionTopic.Quiz.QuestionColl).firstOrDefault(p1 => p1.SNo == $scope.SelectedQuestion.SNo);
			if (que1) {
				que1.QuestionContent = $scope.SelectedQuestion.QuestionContent;
				que1.SNo = $scope.SelectedQuestion.SNo;
				que1.QuestionType = $scope.SelectedQuestion.QuestionType;
				que1.AnswerType = $scope.SelectedQuestion.AnswerType;
				que1.AnswerColl = $scope.SelectedQuestion.AnswerColl;
				que1.Duration = $scope.SelectedQuestion.Duration;
				que1.Mark = $scope.SelectedQuestion.Mark;
				que1.AnswerSNo = $scope.SelectedQuestion.AnswerSNo;
				que1.ImageData = $scope.SelectedQuestion.ImageData;
				que1.ContentPath = $scope.SelectedQuestion.ContentPath;
				que1.Files_TMP = $scope.SelectedQuestion.Files_TMP;
			}
        }
		var fileColl = [];
		var beData = {
			//Added
			ClassId: $scope.newAddLessonContent.SelectedClass.ClassId,
			BatchId: $scope.newAddLessonContent.BatchId || null,
			SemesterId: $scope.newAddLessonContent.SemesterId || null,
			ClassYearId: $scope.newAddLessonContent.ClassYearId || null,
			SubjectId: $scope.newAddLessonContent.SubjectId,
				//Ends
			LessonId: $scope.CurrentLessionTopic.LessonId,
			LessonSNo: $scope.CurrentLessionTopic.LessonSNo,
			TopicSNo:$scope.CurrentLessionTopic.TopicSNo,
			Topic: $scope.CurrentLessionTopic.Quiz.Topic,
			Description: $scope.CurrentLessionTopic.Quiz.Description,
			NoOfQuestion: $scope.CurrentLessionTopic.Quiz.NoOfQuestion,
			Duration: $scope.CurrentLessionTopic.Quiz.Duration,
			FullMark: $scope.CurrentLessionTopic.Quiz.FullMark,
			PassMark: $scope.CurrentLessionTopic.Quiz.PassMark,
			QuizId: $scope.CurrentLessionTopic.Quiz.QuizId||0,
			QuestionColl:[]
		};
		angular.forEach($scope.CurrentLessionTopic.Quiz.QuestionColl, function (que) {
			 
			var newBeData = {
				QuestionContent: que.QuestionContent,
				SNo:que.SNo,
				QuestionType: que.QuestionType,
				AnswerType: que.AnswerType,
				AnswerColl: [],
				Duration: que.Duration,
				Mark:que.Mark,
				AnswerSNo: que.AnswerSNo,
				//ImageData: que.ImageData,
				ContentPath: que.ContentPath,
				//Files_TMP: que.Files_TMP			 
			};

			if (que.Files_TMP) {
				fileColl.push({
					Name: "file-q" + que.SNo,
					File:que.Files_TMP[0]
				});
            }

			var sno = 1;
			angular.forEach(que.AnswerColl, function (ans) {
				var ansData = {
					SNo: sno,
					AnswerType: que.AnswerType,
					AnswerContent: ans.AnswerContent,
					ContentPath:ans.ContentPath,
					IsRightAnswer:que.AnswerSNo==sno ? true : false,
				};
				if (ans.Files_TMP) {
					fileColl.push({
						Name: "file-q" + que.SNo + "-a" + sno,
						File: ans.Files_TMP[0]
					});
				}
				newBeData.AnswerColl.push(ansData);
				sno++;
			});

			beData.QuestionColl.push(newBeData);
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveLessonTopicQuiz",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append(data.files[i].Name, data.files[i].File);
					}
				}

				return formData;
			},
			data: { jsonData: beData, files: fileColl}
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			$scope.ResetQuizForm();
			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess && $scope.selectedLesson && $scope.selectedTopic) {
				$scope.ClickOnTopic($scope.selectedLesson, $scope.selectedTopic);
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	function OnClickDefault() {
		document.getElementById('addVideo').style.display = "none";
		document.getElementById('content-form').style.display = "none";
		document.getElementById('contentlistquiz-form').style.display = "none";
		document.getElementById('video-form').style.display = "none";
		document.getElementById('quizform').style.display = "none";
		document.getElementById('exam-details').style.display = "none";
		document.getElementById('exam-questions').style.display = "none";
		document.getElementById('lCexam-details').style.display = "none";
		/*document.getElementById('LCexam-questions').style.display = "none";*/
		document.getElementById('fullpdf').style.display = "none";
		document.getElementById('fullimg').style.display = "none";

		document.getElementById('AddVideoBtn').onclick = function () {
			document.getElementById('videolist').style.display = "none";
			document.getElementById('addVideo').style.display = "block";

		}
		document.getElementById('backtoVideo').onclick = function () {
			document.getElementById('videolist').style.display = "block";
			document.getElementById('addVideo').style.display = "none";
		}
		//Content
		//document.getElementById('view-content').onclick = function () {
		//	document.getElementById('lessonlist').style.display = "none";
		//	document.getElementById('content-form').style.display = "block";

		//}
		document.getElementById('backcontent').onclick = function () {
			document.getElementById('lessonlist').style.display = "block";
			document.getElementById('content-form').style.display = "none";
		}
		//Assignment
		//document.getElementById('view-assignment').onclick = function () {
		//	document.getElementById('lessonlist').style.display = "none";
		//	document.getElementById('contentlistquiz-form').style.display = "block";

		//}
		//document.getElementById('backassignment').onclick = function () {
		//	document.getElementById('lessonlist').style.display = "block";
		//	document.getElementById('contentlistquiz-form').style.display = "none";
		//}
		//Video
		//document.getElementById('view-video').onclick = function () {
		//	document.getElementById('lessonlist').style.display = "none";
		//	document.getElementById('video-form').style.display = "block";

		//}
		document.getElementById('backvideo').onclick = function () {
			document.getElementById('lessonlist').style.display = "block";
			document.getElementById('video-form').style.display = "none";
		}

		//Quiz
		//Video
		document.getElementById('addquiz').onclick = function () {
			document.getElementById('quizlist').style.display = "none";
			document.getElementById('quizform').style.display = "block";

		}
		document.getElementById('backquiz').onclick = function () {
			document.getElementById('quizlist').style.display = "block";
			document.getElementById('quizform').style.display = "none";
		}
		//Exam Preview
		//document.getElementById('preview').onclick = function () {
		//	document.getElementById('quizlist').style.display = "none";
		//	document.getElementById('quizform').style.display = "none";
		//	document.getElementById('exam-details').style.display = "block";

		//}
		document.getElementById('back-main-page').onclick = function () {
			document.getElementById('quizlist').style.display = "block";
			document.getElementById('quizform').style.display = "none";
			document.getElementById('exam-details').style.display = "none";
		}
		//Exam Questions List
		document.getElementById('next-question').onclick = function () {
			document.getElementById('exam-details').style.display = "none";
			document.getElementById('exam-questions').style.display = "block";

		}
		document.getElementById('back-question-detail').onclick = function () {
			document.getElementById('exam-details').style.display = "block";
			document.getElementById('exam-questions').style.display = "none";
			
		}

		//LEsson Content List Tab Quiz Preview
		//document.getElementById('LCpreviewquiz').onclick = function () {
		//	document.getElementById('contentlistquizlist').style.display = "none";
		//	document.getElementById('lCexam-details').style.display = "block";

		//}

		//document.getElementById('back-LCEL').onclick = function () {
		//	document.getElementById('contentlistquizlist').style.display = "block";
		//	document.getElementById('lCexam-details').style.display = "none";

		//}

		//document.getElementById('next-questionLC').onclick = function () {
		//	document.getElementById('lCexam-details').style.display = "none";
		//	document.getElementById('LCexam-questions').style.display = "block";

		//}
		//document.getElementById('back-question-LCdetailLContent').onclick = function () {
		//	document.getElementById('lCexam-details').style.display = "block";
		//	document.getElementById('LCexam-questions').style.display = "none";

		//}

		//document.getElementById('viewpdf').onclick = function () {
		//	document.getElementById('fullpdf').style.display = "block";
		//	document.getElementById('fullimg').style.display = "none";
		//}

		//document.getElementById('imgview').onclick = function () {
		//	document.getElementById('fullpdf').style.display = "none";
		//	document.getElementById('fullimg').style.display = "block";
		//}
	}
	$scope.AddQuestionMode = function (ind, qDet) {
		if ($scope.SelectedQuestion.AnswerColl && qDet) {
		  
			if ($scope.SelectedQuestion.AnswerColl.length > ind + 1) {
				$scope.SelectedQuestion.AnswerColl.splice(ind + 1, 0, {					
					AnswerContent: '',
					ContentPath:'',
				})
			} else {
				$scope.SelectedQuestion.AnswerColl.push({					
					AnswerContent: '',
					ContentPath: '',
				})
			}
		}
	};
	$scope.delQuestionMode = function (ind) {
		if ($scope.SelectedQuestion.AnswerColl) {
			if ($scope.SelectedQuestion.AnswerColl.length > 1) {
				$scope.SelectedQuestion.AnswerColl.splice(ind, 1);
			}
		}
	};	 

	$scope.ChangeAnswerSelection = function (ind) {
		var i = 0;
		angular.forEach($scope.SelectedQuestion.DetailsColl, function (det) {
			if (ind != i)
				det.IsRightAnswer = false;
			i++;
		});
	};

	$scope.ChangeQuestion = function (qa) {
		$timeout(function () {
			$scope.$apply(function () {
				if ($scope.SelectedQuestion.DetailsColl && $scope.SelectedCategory.ExamModal == 2) {
					if ($scope.SelectedQuestion.DetailsColl.length > 0) {
						var count = mx($scope.SelectedQuestion.DetailsColl).count(p1 => p1.IsRightAnswer == true);
						if (count == 0) {
							Swal.fire('Please Choose Correct Answer');
						} else {
							var valid = true;
							angular.forEach($scope.SelectedQuestion.DetailsColl, function (det) {
								if ($scope.SelectedQuestion.AnswerTitle == 1) {
									if (!det.Answer || det.Answer.isEmpty()) {
										valid = false;
										Swal.fire('Please Enter Answer');
										return;
									}
								}
							});
							if (valid == true) {
								if (($scope.SelectedQuestion.Question && $scope.SelectedQuestion.Question.length > 0) || $scope.SelectedQuestion.Files_TMP)
									$scope.CallSaveUpdateQuestionSetup($scope.SelectedQuestion, false);
								$timeout(function () {
									$scope.$apply(function () {
										//$('#imgQuestion').remove();
										document.getElementById("imgQuestion").src = "''";
										$scope.SelectedQuestion = qa;
									});
								});
								$('#flQuestion').val('');
							}
						}
					}
				} else {
					if (($scope.SelectedQuestion.Question && $scope.SelectedQuestion.Question.length > 0) || $scope.SelectedQuestion.Files_TMP)
						$scope.CallSaveUpdateQuestionSetup($scope.SelectedQuestion, false);

					$timeout(function () {
						$scope.$apply(function () {
							//	$('#imgQuestion').remove();
							document.getElementById("imgQuestion").src = "''";
							$scope.SelectedQuestion = qa;
						});
					});
					$('#flQuestion').val('');
				}
			});
		});
	}


	$scope.$watch('newLessonContentList.SelectedClass', function (newVal, oldVal) {
		if (newVal && newVal !== oldVal) {
			$scope.newLessonContentList.ClassYearId = null;
			$scope.newLessonContentList.SemesterId = null;
		}
	});

	//Donr: Add Prarameter
	$scope.GetClassWiseSubjectListRpt = function () {

		$scope.SubjectListRpt = [];
		$scope.newLessonContentList.NoOfLesson = 0;
		$scope.newLessonContentList.TranId = 0;
        $scope.newLessonContentList.DetailsColl = [];

        var para = {
            ClassId: $scope.newLessonContentList.SelectedClass.ClassId,
            BatchId: $scope.newLessonContentList.BatchId || null,
			ClassYearId: $scope.newLessonContentList.ClassYearId || null,
            SemesterId: $scope.newLessonContentList.SemesterId || null,
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
                $scope.SubjectListRpt = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    }

	$scope.GetLessonPlanByClassSubjectRpt = function (classId, subjectId) {
		$scope.newLessonContentList.NoOfLesson = 0;
		$scope.newLessonContentList.TranId = 0;
		$scope.newLessonContentList.DetailsColl = [];
		if (classId > 0 && subjectId > 0) {
			var para = {
				ClassId: classId,
				SubjectId: subjectId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetLessonPlanByClassSubject",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var dd = res.data.Data;
					$scope.newLessonContentList.NoOfLesson = dd.NoOfLesson;
					$scope.newLessonContentList.TranId = dd.TranId;
					$scope.newLessonContentList.DetailsColl = dd.DetailsColl;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.SelectedLesson = {};
	$scope.SelectedTopic = {};
	$scope.DisplayContent = function (lesson,topic) {
		$scope.SelectedLesson = lesson;
		$scope.SelectedTopic = topic;
		$scope.SelectedLesson.SubjectName = mx($scope.SubjectListRpt).firstOrDefault(p1 => p1.SubjectId == $scope.newLessonContentList.SubjectId).Name;		
		var para = {
			//LessonId: lession.LessonId,
			ClassId: $scope.newLessonContentList.SelectedClass?.ClassId,
			BatchId: $scope.newLessonContentList.BatchId || null,
			SemesterId: $scope.newLessonContentList.SemesterId || null,
			ClassYearId: $scope.newLessonContentList.ClassYearId || null,
			SubjectId: $scope.newLessonContentList.SubjectId,
			//Added Ends
			LessonSNo: lesson.SNo,
			TopicSNo: topic.SNo
		};		
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetLessonTopicContent",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SelectedTopic.ContentsColl = [];
				angular.forEach(res.data.Data, function (dt) {
					$scope.SelectedTopic.ContentsColl.push({
						FilePath: dt.FilePath,
						FileName: dt.FileName,
						FileType:dt.FileType
					});
				});
				document.getElementById('lessonlist').style.display = "none";
				document.getElementById('content-form').style.display = "block";
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	$scope.DisplayVideo = function (lesson, topic) {
		$scope.SelectedLesson = lesson;
		$scope.SelectedTopic = topic;
		$scope.SelectedLesson.SubjectName = mx($scope.SubjectListRpt).firstOrDefault(p1 => p1.SubjectId == $scope.newLessonContentList.SubjectId).Name;
		var para = {
			//LessonId: lesson.LessonId,
			ClassId: $scope.newLessonContentList.SelectedClass?.ClassId,
			BatchId: $scope.newLessonContentList.BatchId || null,
			SemesterId: $scope.newLessonContentList.SemesterId || null,
			ClassYearId: $scope.newLessonContentList.ClassYearId || null,
			SubjectId: $scope.newLessonContentList.SubjectId,

			LessonSNo: lesson.SNo,
			TopicSNo: topic.SNo
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetLessonTopicVideo",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$scope.SelectedTopic.VideoColl = [];
				angular.forEach(res.data.Data, function (dt) {
					$scope.SelectedTopic.VideoColl.push({
						Title: dt.Title,
						Link: dt.Link,
						Remarks: dt.Remarks
					});
				});
				document.getElementById('lessonlist').style.display = "none";
				document.getElementById('video-form').style.display = "block";
			 
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	$scope.DisplayQuiz = function (lesson, topic) {
		$scope.SelectedLesson = lesson;
		$scope.SelectedTopic = topic;
		$scope.SelectedLesson.SubjectName = mx($scope.SubjectListRpt).firstOrDefault(p1 => p1.SubjectId == $scope.newLessonContentList.SubjectId).Name;
		$scope.LessonTopicQuizList1 = [];
		var para = {
			/*LessonId: lesson.LessonId,*/
			ClassId: $scope.newLessonContentList.SelectedClass?.ClassId,
			BatchId: $scope.newLessonContentList.BatchId || null,
			SemesterId: $scope.newLessonContentList.SemesterId || null,
			ClassYearId: $scope.newLessonContentList.ClassYearId || null,
			SubjectId: $scope.newLessonContentList.SubjectId,

			LessonSNo: lesson.SNo,
			TopicSNo: topic.SNo
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetLessonTopicQuizList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LessonTopicQuizList1 = res.data.Data;
				document.getElementById('lessonlist').style.display = "none";
				document.getElementById('contentlistquiz-form').style.display = "block";
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.CurDoc = {};
	$scope.DocumentAtt_Toggle = function (doc) {
		$scope.CurDoc = doc;
		if (doc.FilePath && doc.FilePath !== '') {

			document.body.style.cursor = 'wait';
			document.getElementById("DocumentAtt_Iframe").src = '';
			document.getElementById("DocumentAtt_Iframe").src = doc.FilePath;
			document.body.style.cursor = 'default';
			$('#modalIMG').modal('show');
		}		 
	};


	$scope.DeleteLessonTopicQuizId = function (refData) {
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
					QuizId: refData.QuizId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelLessonTopicQuizById",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						var lession = {
							LessonId: $scope.CurrentLessionTopic.LessonId,
							SNo: $scope.CurrentLessionTopic.LessonSNo,
							LessonName: $scope.CurrentLessionTopic.LessonName
						};

						var topic = {
							SNo: $scope.CurrentLessionTopic.TopicSNo,
							TopicName: $scope.CurrentLessionTopic.TopicName
						};
						Swal.fire(res.data.ResponseMSG);
						
						$scope.ClickOnTopic(lession, topic);
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.goBackToQuizList = function () {
		document.getElementById('contentlistquizlist').style.display = "block";
		document.getElementById('lCexam-details').style.display = "none";
	};

	$scope.BacktoQList = function () {
		document.getElementById('lessonlist').style.display = "block";
		document.getElementById('contentlistquiz-form').style.display = "none";
		document.getElementById('contentlistquizlist').style.display = "block";
		document.getElementById('lCexam-details').style.display = "none";
	};

	$scope.PreviewQuiz = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			QuizId: refData.QuizId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetLessonTopicQuiz",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.quizPreviewData = res.data.Data;

				//if ($scope.quizPreviewData.QuestionColl.length > 0) {
				//	$scope.SelectedQuestion = angular.copy(
				//		$scope.PreviewQuiz.QuestionColl[0]
				//	);
				//}


				/*$scope.newExamType.Mode = 'Modify';*/

				document.getElementById('contentlistquizlist').style.display = "none";
				document.getElementById('lCexam-details').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};
});