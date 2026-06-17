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

		$scope.newAddLessonContent = {
			ClassId: null,
			SubjectId: null,
			NoOfLesson: 0,
			DetailsColl: [],
			Mode: 'Save'
		};

		 

		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		//$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();
	}

	$scope.GetClassWiseSubjectList = function (classId) {
		$scope.CurrentLesson = {};

		$scope.SubjectList = [];
		var para = {
			ClassId: classId
		};
		$scope.loadingstatus = "running";

		$scope.SubjectColl = [];
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

		$scope.CurrentLesson = {};

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
						$scope.newAddLessonContent.CoverFilePath = dd.CoverFilePath;
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

		$scope.loadingstatus = "running";
		showPleaseWait();

		topic.ContentsColl = [];
		topic.VideoColl = [];
		topic.Quiz = {};

		$scope.CurrentLessionTopic = {};
		$scope.CurrentLessionTopic = {
			LessonId: lession.LessonId,
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
			LessonId: lession.LessonId,
			LessonSNo: lession.SNo,
			TopicSNo:topic.SNo
		};
		$http({
			method: 'POST',
			url: base_url + "LessonPlan/Creation/GetLessonTopicVideo",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {

				angular.forEach(res.data, function (dt) {
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
			url: base_url + "LessonPlan/Creation/GetLessonTopicContent",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if ( res.data) {

				angular.forEach(res.data, function (dt) {
					$scope.CurrentLessionTopic.ContentsColl.push({
						FilePath: dt.FilePath,
						FileName: dt.FileName,
						FileType: dt.FileType
					});
				});

			} else {
				Swal.fire(res.data);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "LessonPlan/Creation/GetLessonTopicQuiz",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {

				$scope.CurrentLessionTopic.Quiz = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

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
			url: base_url + "LessonPlan/Creation/SaveLessonTopicContent",
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
			url: base_url + "LessonPlan/Creation/SaveLessonTopicVideo",
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
	$scope.ClickOnQuestionBtn = function (que) {
		document.getElementById("imgQuestion").src = "''";
		$timeout(function () {
			$scope.$apply(function () {
				var que1 = mx($scope.CurrentLessionTopic.Quiz.QuestionColl).firstOrDefault(p1 => p1.SNo == $scope.SelectedQuestion.SNo);
				if (que1) {
					que1.QuestionContent = $scope.SelectedQuestion.QuestionContent;
					que1.SNo = $scope.SelectedQuestion.SNo;
					que1.QuestionType = $scope.SelectedQuestion.QuestionType;
					que1.AnswerType = $scope.SelectedQuestion.AnswerType;
					que1.AnswerColl = $scope.SelectedQuestion.AnswerColl;
					que1.Duration=$scope.SelectedQuestion.Duration;
					que1.Mark = $scope.SelectedQuestion.Mark;
					que1.AnswerSNo = $scope.SelectedQuestion.AnswerSNo;
					que1.ImageData = $scope.SelectedQuestion.ImageData;
					que1.ContentPath = $scope.SelectedQuestion.ContentPath;
					que1.Files_TMP = $scope.SelectedQuestion.Files_TMP;
                }				
			});
		})

		$timeout(function () {
			$scope.$apply(function () {
				$scope.SelectedQuestion.QuestionContent = que.QuestionContent;
				$scope.SelectedQuestion.SNo = que.SNo;
				$scope.SelectedQuestion.QuestionType = que.QuestionType;
				$scope.SelectedQuestion.AnswerType = que.AnswerType;
				$scope.SelectedQuestion.AnswerColl = que.AnswerColl;
				$scope.SelectedQuestion.Duration = que.Duration;
				$scope.SelectedQuestion.Mark = que.Mark;
				$scope.SelectedQuestion.AnswerSNo = que.AnswerSNo;
				$scope.SelectedQuestion.ImageData = que.ImageData;
				$scope.SelectedQuestion.ContentPath = que.ContentPath;
				$scope.SelectedQuestion.Files_TMP=que.Files_TMP;
			});
        })
		
	}

	$scope.CallSaveUpdateLessonTopicQuiz = function () {
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
			LessonId: $scope.CurrentLessionTopic.LessonId,
			LessonSNo: $scope.CurrentLessionTopic.LessonSNo,
			TopicSNo:$scope.CurrentLessionTopic.TopicSNo,
			Topic: $scope.CurrentLessionTopic.Quiz.Topic,
			Description: $scope.CurrentLessionTopic.Quiz.Description,
			NoOfQuestion: $scope.CurrentLessionTopic.Quiz.NoOfQuestion,
			Duration: $scope.CurrentLessionTopic.Quiz.Duration,
			FullMark: $scope.CurrentLessionTopic.Quiz.FullMark,
			PassMark: $scope.CurrentLessionTopic.Quiz.PassMark,
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
			url: base_url + "LessonPlan/Creation/SaveLessonTopicQuiz",
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

			Swal.fire(res.data.ResponseMSG);


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
		document.getElementById('LCexam-questions').style.display = "none";
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
		document.getElementById('backassignment').onclick = function () {
			document.getElementById('lessonlist').style.display = "block";
			document.getElementById('contentlistquiz-form').style.display = "none";
		}
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
		document.getElementById('LCpreviewquiz').onclick = function () {
			document.getElementById('contentlistquizlist').style.display = "none";
			document.getElementById('lCexam-details').style.display = "block";

		}
		document.getElementById('back-LCEL').onclick = function () {
			document.getElementById('contentlistquizlist').style.display = "block";
			document.getElementById('lCexam-details').style.display = "none";

		}

		document.getElementById('next-questionLC').onclick = function () {
			document.getElementById('lCexam-details').style.display = "none";
			document.getElementById('LCexam-questions').style.display = "block";

		}
		document.getElementById('back-question-LCdetailLContent').onclick = function () {
			document.getElementById('lCexam-details').style.display = "block";
			document.getElementById('LCexam-questions').style.display = "none";

		}

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
	 


	$scope.GetClassWiseSubjectListRpt = function (classId) {

		$scope.SubjectListRpt = [];
		$scope.newLessonContentList.NoOfLesson = 0;
		$scope.newLessonContentList.TranId = 0;
		$scope.newLessonContentList.DetailsColl = [];

		var para = {
			classId: classId,
			sectionIdColl: 0,
			sectionId: 0
		};

		if (classId > 0) {
			$http({
				method: 'POST',
				url: base_url + "OnlineClass/Creation/GetSubjectList",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					$scope.SubjectListRpt = res.data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

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
				url: base_url + "LessonPlan/Creation/GetLessonPlanByClassSubject",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {

					var dd = res.data;
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
			LessonId: lesson.LessonId,
			LessonSNo: lesson.SNo,
			TopicSNo: topic.SNo
		};
		
		$http({
			method: 'POST',
			url: base_url + "LessonPlan/Creation/GetLessonTopicContent",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {

				$scope.SelectedTopic.ContentsColl = [];
				angular.forEach(res.data, function (dt) {
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
			LessonId: lesson.LessonId,
			LessonSNo: lesson.SNo,
			TopicSNo: topic.SNo
		};

		$http({
			method: 'POST',
			url: base_url + "LessonPlan/Creation/GetLessonTopicVideo",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {

				$scope.SelectedTopic.VideoColl = [];
				angular.forEach(res.data, function (dt) {
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

		var para = {
			LessonId: lesson.LessonId,
			LessonSNo: lesson.SNo,
			TopicSNo: topic.SNo
		};

		$http({
			method: 'POST',
			url: base_url + "LessonPlan/Creation/GetLessonTopicQuiz",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {

				$scope.SelectedTopic.Quiz = res.data;

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

});