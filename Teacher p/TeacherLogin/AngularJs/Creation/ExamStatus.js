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

		};

		$scope.searchData = {
			UpcomingExam: '',
			PastExams: '',

		};

		$scope.perPage = {
			UpcomingExam: GlobalServices.getPerPageRow(),
			PastExams: GlobalServices.getPerPageRow(),

		};


		$scope.GetAllExamList();


	}

	function OnClickDefault() {
		document.getElementById('detail').style.display = "none";
		document.getElementById('check').style.display = "none";
		document.getElementById('exam-details').style.display = "none";
		document.getElementById('exam-questions').style.display = "none";

		document.getElementById('detail-btn').onclick = function () {
			document.getElementById('table-listing').style.display = "none";
			document.getElementById('detail').style.display = "block";
		}

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

		document.getElementById('detail-btn').onclick = function () {
			document.getElementById('table-listing').style.display = "none";
			document.getElementById('detail').style.display = "block";
		}


		document.getElementById('check-btn').onclick = function () {
			document.getElementById('check').style.display = "block";
			document.getElementById('detail').style.display = "none";
		}

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
			method: 'GET',
			url: base_url + "OnlineExam/Reporting/GetOnlineExamList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {

				var tmpColl = res.data;

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
			url: base_url + "OnlineExam/Reporting/GetOnlineExamDetById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.newUpcomingExam = res.data;
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
			url: base_url + "OnlineExam/Reporting/GetQuestionListById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();	
			$scope.loadingstatus = "stop";
			if (res.data) {

				$scope.OnlineExamQuestion = [];
				var objColl = mx(res.data);
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
			document.getElementById("DocumentAtt_Iframe").src = WEBURLPATH +fpath;
			document.body.style.cursor = 'default';
			$('#modalIMG').modal('show');
		}

	};


});