app.controller('ExamStatusController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Exam Status';
	OnClickDefault();
	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			UpcomingExam: 1,
			PastExams: 1,
			Evaluate:1,

		};

		$scope.searchData = {
			UpcomingExam: '',
			PastExams: '',
			Evaluate:'',
		};

		$scope.perPage = {
			UpcomingExam: GlobalServices.getPerPageRow(),
			PastExams: GlobalServices.getPerPageRow(),
			Evaluate: GlobalServices.getPerPageRow(),
		};


		$scope.GetAllExamList();


	}

	function OnClickDefault() {
		document.getElementById('detail').style.display = "none";
		document.getElementById('check').style.display = "none";
		document.getElementById('exam-details').style.display = "none";
		document.getElementById('exam-questions').style.display = "none";

		//document.getElementById('detail-btn').onclick = function () {
		//	document.getElementById('table-listing').style.display = "none";
		//	document.getElementById('detail').style.display = "block";
		//}

		document.getElementById('back-btn').onclick = function () {
			document.getElementById('table-listing').style.display = "block";
			document.getElementById('detail').style.display = "none";
		}




		//document.getElementById('review-btn').onclick = function () {
		//	document.getElementById('exam-details').style.display = "block";
		//	document.getElementById('exam-questions').style.display = "none";
		//	document.getElementById('upcoming-exam-home').style.display = "none";
		//}

		//document.getElementById('next-question').onclick = function () {
		//	document.getElementById('exam-details').style.display = "none";
		//	document.getElementById('exam-questions').style.display = "block";
		//	document.getElementById('upcoming-exam-home').style.display = "none";
		//}

		document.getElementById('back-question-detail').onclick = function () {
			document.getElementById('exam-details').style.display = "block";
			document.getElementById('exam-questions').style.display = "none";
			document.getElementById('upcoming-exam-home').style.display = "none";
		}

		document.getElementById('back-main-page').onclick = function () {
			document.getElementById('exam-details').style.display = "none";
			document.getElementById('exam-questions').style.display = "none";
			document.getElementById('upcoming-exam-home').style.display = "block";
		}

		//document.getElementById('detail-btn').onclick = function () {
		//	document.getElementById('table-listing').style.display = "none";
		//	document.getElementById('detail').style.display = "block";
		//}


		//document.getElementById('check-btn').onclick = function () {
		//	document.getElementById('check').style.display = "block";
		//	document.getElementById('detail').style.display = "none";
		//}

		document.getElementById('back-detail-btn').onclick = function () {

			document.getElementById('detail').style.display = "block";
			document.getElementById('check').style.display = "none";
		}
	}


	$scope.GetAllExamList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.UpcomingExamList = [];
		$scope.PassedExamList = [];

		$http({
			method: 'POST',
			url: base_url + "elearning/creation/GetOnlineExamList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.Data && res.data.IsSuccess==true) {

				var tmpColl = res.data.Data;

				$timeout(function () {
					angular.forEach(tmpColl, function (tc) {

						//$scope.PassedExamList.push(tc);						
						//$scope.UpcomingExamList.push(tc);
						if (tc.ForType == "Passed")
							$scope.PassedExamList.push(tc);
						else
							$scope.UpcomingExamList.push(tc);
					});
				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//Calling UpComming Exam Login
	$scope.GetUpcomingExamById = function (Data) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			examSetupId: Data.ExamSetupId
		};

		$http({
			method: 'POST',
			url: base_url + "elearning/creation/GetOnlineExamDetById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.Data && res.data.IsSuccess==true) {
				$scope.newUpcomingExam = res.data.Data;
				$scope.newUpcomingExam.Instruction = Data.Instruction;
				$scope.newUpcomingExam.TotalQuestion = mx(res.data.QuestionDetailsColl).sum(p1 => p1.NoOfQuestion);
				document.getElementById('exam-details').style.display = "block";
				document.getElementById('exam-questions').style.display = "none";
				document.getElementById('upcoming-exam-home').style.display = "none";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.GetOnlineExamQuestion = function (examDetails) {

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.RunQ = {};
		var para = {
			examSetupId: examDetails.ExamSetupId
		};

		$http({
			method: 'POST',
			url: base_url + "elearning/creation/GetQuestionListById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.Data && res.data.IsSuccess==true) {

				$scope.OnlineExamQuestion = [];
				var objColl = mx(res.data.Data);
				var categoryWise = objColl.groupBy(t => t.CategoryName).toArray();
				var sno = 1;
				angular.forEach(categoryWise, function (q) {
					var beData = {
						SNo: sno,
						CategoryName: q.key,
						QuestionList: []
					};

					angular.forEach(q.elements, function (el) {
						beData.QuestionList.push(el);
					})
					sno++;

					$scope.OnlineExamQuestion.push(beData);
				});

				//$scope.OnlineExamQuestion = res.data;						
				document.getElementById('exam-details').style.display = "none";
				document.getElementById('exam-questions').style.display = "block";
				document.getElementById('upcoming-exam-home').style.display = "none";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	//*************************Upcoming Exam *********************************


	$scope.PrintQuestions = function () {
		$("#divQuestions").printThis(
			{
				importCSS: true,
				importStyle: true,
				loadCSS: "/Content/printrules.css"
			}
		);
	};



	//*************************Past Exams *********************************

	$scope.downloadFilePath = '';
	$scope.DocumentAtt_Toggle = function (fpath) {
		if (fpath && fpath !== '') {
			$scope.downloadFilePath = WEBURLPATH + fpath;
			document.body.style.cursor = 'wait';
			document.getElementById("DocumentAtt_Iframe").src = '';
			document.getElementById("DocumentAtt_Iframe").src = WEBURLPATH + fpath;
			document.body.style.cursor = 'default';
			$('#modalIMG').modal('show');
		}

	};


	$scope.CurExamSetup = null;
	$scope.CurStudent = null;
	$scope.GetStudentList = function (examDet) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CurExamSetup = examDet;
		$scope.CurStudent = null;
		$scope.PresentStudentList = [];
		$scope.AbsentStudentList = [];

		var para = {
			examSetupId: examDet.ExamSetupId,
			classId: examDet.ClassId,
			sectionId: examDet.SectionId
		};

		$http({
			method: 'POST',
			url: base_url + "elearning/creation/GetStudentForEvaluate",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.Data) {
				var tmpDataColl = res.data.Data;
				angular.forEach(tmpDataColl, function (dc) {
					if (dc.QuestionAttampt && dc.QuestionAttampt > 0)
						$scope.PresentStudentList.push(dc);
					else
						$scope.AbsentStudentList.push(dc);
				});

				$scope.CurExamSetup.TotalStudent = tmpDataColl.length;
				$scope.CurExamSetup.TotalPresent = $scope.PresentStudentList.length;
				$scope.CurExamSetup.TotalAbsent = $scope.AbsentStudentList.length;

				document.getElementById('table-listing').style.display = "none";
				document.getElementById('detail').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetStudentAnswerList = function (studentDet) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CurStudent = studentDet;
		$scope.CurStudent.ObjectiveList = [];
		$scope.CurStudent.SubjectiveList = [];
		$scope.CurStudent.CategoryWiseObjectiveList = [];
		$scope.CurStudent.TotalObjetiveQ = 0;
		$scope.CurStudent.TotalObjetiveA = 0;
		$scope.CurStudent.TotalObjetiveC = 0;
		$scope.CurStudent.TotalObjetiveW = 0;
		$scope.CurStudent.TotalSubjectiveA = 0;
		$scope.CurStudent.TotalSubjectiveL = 0;
		var para = {
			examSetupId: $scope.CurExamSetup.ExamSetupId,
			studentId: studentDet.StudentId
		};

		$http({
			method: 'POST',
			url: base_url + "elearning/creation/GetOnlineExamAnswer",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.Data) {
				var tmpDataColl = res.data.Data;

				var objColl = [];
				var totalAswer = 0, totalCorrect = 0, totalWrong = 0;
				angular.forEach(tmpDataColl, function (dc) {
					if (dc.QuestionPath && dc.QuestionPath.length > 0)
						dc.QuestionPath = WEBURLPATH.trim() + dc.QuestionPath.trim();

					if (dc.ExamModal == 1) {

						if ((dc.AnswerText && dc.AnswerText.length > 0) || (dc.StudentDocColl && dc.StudentDocColl.length > 0)) {
							$scope.CurStudent.TotalSubjectiveA = $scope.CurStudent.TotalSubjectiveA + 1;
						} else
							$scope.CurStudent.TotalSubjectiveL = $scope.CurStudent.TotalSubjectiveL + 1;

						dc.AnswerFiles = [];

						if (dc.StudentDocColl && dc.StudentDocColl.length > 0) {
							var fInd = 0;
							angular.forEach(dc.StudentDocColl, function (doc) {
								dc.AnswerFiles.push({
									AnswerFiles: (WEBURLPATH.trim() + doc.trim()).replace(/\\/g, '/'),
									Index: fInd,
									FileName: 'Answer' + fInd,
									FName: new URL((WEBURLPATH.trim() + doc.trim()).replace(/\\/g, '/')).pathname.split('/').pop()
								});
								fInd++;
							});
						}

						$scope.CurStudent.SubjectiveList.push(dc);
					} else {

						dc.ObtainMark = 0;
						if (dc.StudentAnswerNo > 0)
							$scope.CurStudent.TotalObjetiveA = $scope.CurStudent.TotalObjetiveA + 1;


						if (dc.IsCorrect == true) {
							$scope.CurStudent.TotalObjetiveC = $scope.CurStudent.TotalObjetiveC + 1;
							dc.ObtainMark = dc.Marks;
						} else if (dc.StudentAnswerNo > 0 && dc.IsCorrect == false)
							$scope.CurStudent.TotalObjetiveW = $scope.CurStudent.TotalObjetiveW + 1;



						angular.forEach(dc.DetailsColl, function (dt) {
							dt.ObtainMark = 0;
							dt.CorrectAnswer = 0;
							dt.ShowFile = 0;

							if (dt.FilePath && dt.FilePath.length > 0) {
								var fl = dt.FilePath.toString();
								if (fl.indexOf(".jpg") > 0 || fl.indexOf(".jpeg") > 0 || fl.indexOf(".png") > 0)
									dt.ShowFile = 1;
								else if (fl.indexOf(".pdf") > 0)
									dt.ShowFile = 2;
								else if (fl.indexOf(".mp3") > 0 || fl.indexOf(".mp4") > 0 || fl.indexOf(".wav") > 0)
									dt.ShowFile = 3;
							}

							if (dt.SNo == dc.StudentAnswerNo && dt.IsCorrect == true) {
								dt.CorrectAnswer = 1;
								dt.ObtainMark = dc.Marks;
							} else if (dt.SNo == dc.StudentAnswerNo && dt.IsCorrect == false) {
								dt.CorrectAnswer = 2;
							}


						});

						objColl.push(dc);
						//$scope.CurStudent.ObjectiveList.push(dc);
					}


				});

				$scope.CurStudent.TotalObjetiveQ = objColl.length;

				var categoryWise = mx(objColl).groupBy(t => t.CategoryName).toArray();
				var sno = 1;
				angular.forEach(categoryWise, function (q) {
					var beData = {
						SNo: sno,
						CategoryName: q.key,
						ObjectiveList: []
					};

					angular.forEach(q.elements, function (el) {
						beData.ObjectiveList.push(el);
					})
					sno++;

					$scope.CurStudent.CategoryWiseObjectiveList.push(beData);
				})

				document.getElementById('check').style.display = "block";
				document.getElementById('detail').style.display = "none";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

});