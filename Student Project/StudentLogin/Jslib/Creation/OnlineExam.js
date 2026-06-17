String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};

app.controller('OnlineExamController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Online Exam';

	getLocation();
	OnClickDefault();


	$scope.LoadData = function () {		

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/GetAllProfile",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data) {
				$scope.MyProfile = res.data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllUpcomingExamList();
		

		$scope.newUpcomingExam = {
			UpcomingExamId: null,

		};

		$scope.newPreviousExam = {
			PreviousExamId: null,


		};


		//$scope.GetAllUpcomingExamList();


	}
	function getLocation() {
		if (navigator.geolocation) {
			navigator.geolocation.getCurrentPosition(showPosition);
		} else {
			//x.innerHTML = "Geolocation is not supported by this browser.";
		}
	}

	var latitude = 0, longitude = 0;
	var geolocation = 'Web';
	function showPosition(position) {
		latitude = position.coords.latitude;
		longitude = position.coords.longitude;
		displayLocation(latitude, longitude);
	}

	function displayLocation(latitude, longitude) {
		var request = new XMLHttpRequest();

		var method = 'GET';
		var url = 'http://maps.googleapis.com/maps/api/geocode/json?latlng=' + latitude + ',' + longitude + '&sensor=true';
		var async = true;

		request.open(method, url, async);
		request.onreadystatechange = function () {
			if (request.readyState == 4 && request.status == 200) {
				try {
					var data = JSON.parse(request.responseText);
					var address = data.results[0];
					geolocation = address.formatted_address;
				} catch {

                }
				
			}
		};
		request.send();
	};


	$scope.ClearUpcomingExam = function () {
		$scope.newUpcomingExam = {
			UpcomingExamId: null,

		};
	}

	$scope.ClearPreviousExam = function () {

		$scope.newPreviousExam = {
			PreviousExamId: null,


		};

	}

	/*Clear Photo*/
	$scope.ClearUpcomingExamPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newUpcomingExam.PhotoData = null;
				$scope.newUpcomingExam.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		//$('#imgPhoto1').attr('src', '');

	};

	/*Clear Photo*/
	$scope.ClearUpcomingExamIdPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newUpcomingExam.IdPhotoData = null;
				$scope.newUpcomingExam.IdPhoto_TMP = [];
			});

		});

		$('#imgIdPhoto').attr('src', '');
		//$('#imgIdPhoto1').attr('src', '');

	};

	function OnClickDefault() {
		document.getElementById('exam-details').style.display = "none";
		document.getElementById('exam-document').style.display = "none";
		document.getElementById('exam-questions').style.display = "none";

		document.getElementById('question-summary').style.display = "none";
		document.getElementById('summary-of-answer').style.display = "none";
		document.getElementById('question-summary-previous-exam').style.display = "none";
		document.getElementById('summary-of-answer-of-previous-exam').style.display = "none";
		document.getElementById('question-summary-previous-exam').style.display = "none";

		document.getElementById('back').onclick = function () {
			document.getElementById('upcoming-exam').style.display = "block";
			document.getElementById('exam-details').style.display = "none";

		}

		//document.getElementById('next').onclick = function () {
		//	document.getElementById('exam-document').style.display = "block";
		//	document.getElementById('exam-details').style.display = "none";

		//}

		//// verification back and next btn
		//document.getElementById('back-verification').onclick = function () {
		//	document.getElementById('exam-document').style.display = "none";
		//	document.getElementById('exam-details').style.display = "block";

		//}

		//document.getElementById('next-verification').onclick = function () {
		//	document.getElementById('exam-document').style.display = "none";
		//	document.getElementById('exam-questions').style.display = "block";

		//}


		//document.getElementById('next-question').onclick = function () {
		//	document.getElementById('exam-questions').style.display = "none";


		//}

		// question-2 back and next btn
		//document.getElementById('back-question-2').onclick = function () {
		//	document.getElementById('exam-questions').style.display = "block";


		//}


		//summary question back and next btn
		//document.getElementById('back-summary-question').onclick = function () {
		//	document.getElementById('exam-questions-2').style.display = "block";
		//	document.getElementById('question-summary').style.display = "none";

		//}

		//document.getElementById('next-summary-question').onclick = function () {
		//	document.getElementById('question-summary').style.display = "none";
		//	document.getElementById('summary-of-answer').style.display = "block";

		//}

		//summary Answer back and next btn
		document.getElementById('back-summary-of-answer').onclick = function () {
			document.getElementById('question-summary').style.display = "block";
			document.getElementById('summary-of-answer').style.display = "none";

		}

		//document.getElementById('next-summary-of-answer').onclick = function () {
		//	document.getElementById('exam-questions-2').style.display = "none";
		//	document.getElementById('question-summary').style.display = "none";
		//	document.getElementById('summary-of-answer').style.display = "block";

		//}


		// on click submit btn
		//document.getElementById('submit').onclick = function () {
		//	document.getElementById('question-summary').style.display = "block";
		//	document.getElementById('exam-questions').style.display = "none";
		//	document.getElementById('exam-questions-2').style.display = "none";

		//}
	}

	$scope.SaveUpdateUpcomingExam = function () {
		if ($scope.IsValidUpcomingExam() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newUpcomingExam.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateUpcomingExam();
					}
				});
			} else
				$scope.CallSaveUpdateUpcomingExam();

		}
	};

	$scope.GetAllUpcomingExamList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.UpcomingExamList = [];
		$scope.PassedExamList = [];

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/GetOnlineExamList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {

				var tmpColl = res.data;

				$timeout(function () {
					angular.forEach(tmpColl, function (tc) {

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
			url: base_url + "Student/Creation/GetOnlineExamDetById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.newUpcomingExam = res.data;

				$scope.newUpcomingExam.TotalQuestion = mx(res.data.QuestionDetailsColl).sum(p1 => p1.NoOfQuestion);
				//$scope.newUpcomingExam.Mode = 'Modify';
				document.getElementById('upcoming-exam').style.display = "none";
				document.getElementById('exam-details').style.display = "block";
				//document.getElementById('class-section').style.display = "none";
				//document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	//End Class
	$scope.EndFiles2 = function (input) {
		$scope.EndFiles2 = [];
		if (input.files && input.files[0]) {
			angular.forEach(input.files, function (fl) {
				$scope.EndFiles2.push(fl);
			});
		}
	};

	$scope.StartExam = function () {

		Swal.fire({
			title: 'Once you start the exam, it won’t be undone!  Are you Sure?',
			showCancelButton: true,
			cancelButtonText: 'Back',
			confirmButtonText: 'Confirm',
		}).then((result) => {
			if (result.isConfirmed) {

				var StartExam = {
					ExamSetupId: $scope.newUpcomingExam.ExamSetupId,
					Location: 'OnlineExam',
					Lat: 2.563,
					Lan: 2.69,
					Notes: 'Start From Web'
				}
				$scope.loadingstatus = "running";
				showPleaseWait();
				$http({
					method: 'POST',
					url: base_url + "Student/Creation/StartClass",
					headers: { 'Content-Type': undefined },

					transformRequest: function (data) {

						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));
						if (data.files && data.files.length > 0) {
							for (var i = 0; i < data.files.length; i++) {
								formData.append("file1" + i, data.files[i]);
							}
						}
						return formData;
					},
					data: { jsonData: StartExam, files: $scope.uploadFiles }
				}).then(function (res) {

					$scope.loadingstatus = "stop";
					hidePleaseWait();

					if (res.data.IsSuccess == true) {
						StartExam.StartDateTime = res.data.StartDateTime;
						StartExam.EndDateTime = res.data.EndDateTime;
						$scope.GetOnlineExamQuestion(StartExam);
						//$scope.ClearPreviousExam();
						//$scope.GetAllPreviousExamList();


						document.getElementById('exam-details').style.display = "none";
						document.getElementById('exam-document').style.display = "none";
						document.getElementById('exam-questions').style.display = "block";
					} else
						Swal.fire(res.data.ResponseMSG);

				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";

				});
			}
		});

		
	}
	//Calling GetOnlineExamQuestion
	$scope.GetOnlineExamQuestion = function (examDetails) {

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.RunQ = {};
		var para = {
			examSetupId: examDetails.ExamSetupId
		};

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/GetOnlineExamQuestion",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				var qCOll = res.data;
				$scope.OnlineExamQuestion = [];

				if (qCOll && qCOll.length > 0) {

					$timeout(function () {
						angular.forEach(qCOll, function (oe) {
							oe.AnswerType = (oe.SubmitType && oe.SubmitType != null ? oe.SubmitType : 1);
							oe.ExamSetupId = examDetails.ExamSetupId;
							oe.AnswerSNo = (oe.StudentAnswerNo ? oe.StudentAnswerNo : null);
							oe.AnswerText = (oe.AnswerText ? oe.AnswerText : '');
							oe.QuestionRemarks = (oe.QuestionRemarks ? oe.QuestionRemarks : '');
							oe.Location = (geolocation ? geolocation : '');
							oe.Lat = (latitude ? latitude : 0);
							oe.Lan = (longitude ? longitude : 0);
							oe.AnswerOption = 1;	
							oe.AnswerFiles = [];

							if (oe.StudentDocColl && oe.StudentDocColl.length > 0) {
								oe.AnswerOption = 2;
								angular.forEach(oe.StudentDocColl, function (doc) {
									oe.AnswerFiles.push({
										AnswerFiles: WEBURLPATH +doc
									});
								});
                            }
							

							//oe.ExamModal = 1;
							oe.QuestionPath = oe.QuestionPath ? WEBURLPATH + oe.QuestionPath : '';							
							$scope.OnlineExamQuestion.push(oe);
							// AnswerOpening  1= Text Editor,2=Image,3=Audio
						});

						$scope.RunQ.Question = $scope.OnlineExamQuestion[0];
						$scope.RunQ.QNo = 1;

						$scope.CalculateAnswerType();
					});

				}
				//$scope.newUpcomingExam.Mode = 'Modify';



				distance = $scope.newUpcomingExam.Duration * 60 * 1000;
				var startDate = new Date(examDetails.StartDateTime);
				examStartTime = new Date();
				examStartTime.setHours(startDate.getHours());
				examStartTime.setMinutes(startDate.getMinutes());
				examStartTime.setSeconds(0);

				fixed = examStartTime.getTime();
				fixed += distance;
				var x = setInterval(function () {
					//Test time in milliseconds
					distance = fixed - (new Date().getTime());
					percentremain = (distance / 36000.0);
					// Time calculations for days, hours, minutes and seconds
					var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
					var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
					var seconds = Math.floor((distance % (1000 * 60)) / 1000);

					$timeout(function () {
						$scope.remaingTime.hours = hours;
						$scope.remaingTime.minutes = minutes;
						$scope.remaingTime.seconds = seconds;
					});


					//If the count down is finished, write some text
					if (distance < 0) {
						clearInterval(x);
						Swal.fire('Your Time Is Over');
						$timeout(function () {
							$scope.submitExam();
						});
						$scope.timeOver = true;
						$scope.allCompleted = true;
					}
				}, 1000);

				document.getElementById('exam-document').style.display = "none";
				document.getElementById('exam-questions').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};
	$scope.GetAnswerTypeCSS = function (ansType) {
		if (ansType == 1)
			return 'btn-warning text-white';
		else if (ansType == 2)
			return 'btn-danger';
		else if (ansType == 3)
			return 'btn-secondary'
		else if (ansType == 4)
			return 'btn-success';
	};
	$scope.RowBreak = function (qno) {

		if (qno == 7)
			return 1;
		else if (qno == 13)
			return 1;
		else if (qno == 19)
			return 1;
		else if (qno == 25)
			return 1;
		else if (qno == 31)
			return 1;
		else if (qno == 37)
			return 1;
		else if (qno == 43)
			return 1;
		else if (qno == 49)
			return 1;
		else if (qno == 55)
			return 1;
		else if (qno == 61)
			return 1;
		else if (qno == 67)
			return 1;
		else if (qno == 73)
			return 1;
		else if (qno == 79)
			return 1;
		else if (qno == 85)
			return 1;
		else if (qno == 91)
			return 1;
		else if (qno == 97)
			return 1;
		return 0;
	};
	$scope.nextQuestion = function () {

		if ($scope.RunQ.Question.AnswerSNo && $scope.RunQ.Question.AnswerSNo > 0) {
			$scope.RunQ.Question.AnswerType = 4
		} else if ($scope.RunQ.Question.AnswerFiles && $scope.RunQ.Question.AnswerFiles.length > 0)
			$scope.RunQ.Question.AnswerType = 4
		else if ($scope.RunQ.Question.ExamModal == 1 && $scope.RunQ.Question.AnswerText && $scope.RunQ.Question.AnswerText.length>0)
			$scope.RunQ.Question.AnswerType = 4

		$timeout(function () {
			$scope.SubmitOEAnswerNext();
		});

		//var ind = $scope.RunQ.QNo;
		//$timeout(function () {
		//	if (ind < $scope.OnlineExamQuestion.length) {
		//		$scope.RunQ.Question = $scope.OnlineExamQuestion[ind];
		//		$scope.RunQ.QNo = ind + 1;
		//	} else {
		//		$scope.isLastQNo = true;
		//	}
		//},50);

		//$timeout(function () {
		//	$scope.CalculateAnswerType();
		//},50);

	};
	$scope.BackQuestion = function () {
		
		if ($scope.RunQ.Question.AnswerSNo && $scope.RunQ.Question.AnswerSNo > 0) {
			$scope.RunQ.Question.AnswerType = 4
		} else if ($scope.RunQ.Question.AnswerFiles && $scope.RunQ.Question.AnswerFiles.length > 0)
			$scope.RunQ.Question.AnswerType = 4
		else if ($scope.RunQ.Question.ExamModal == 1 && $scope.RunQ.Question.AnswerText && $scope.RunQ.Question.AnswerText.length > 0)
			$scope.RunQ.Question.AnswerType = 4

		$timeout(function () {
			$scope.SubmitOEAnswerBack();
		});

		

	};
	$scope.ClickOnQuestion = function (qst) {

		$timeout(function () {
			$scope.SubmitOEAnswer();
		});

		$timeout(function () {
			$scope.RunQ.Question = qst;

			var ind = $scope.RunQ.QNo;
			if (ind < $scope.OnlineExamQuestion.length) {
				$scope.isLastQNo = false;
			} else {
				$scope.isLastQNo = true;
			}

			$scope.CalculateAnswerType();
			$scope.RunQ.QNo = $scope.RunQ.Question.QNo ;
		});
	}
	$scope.CalculateAnswerType = function () {
		//1=Pending,2=Reported=2,3=Skiped,4=Answered

		$timeout(function () {
			var allQuestion = mx($scope.OnlineExamQuestion);
			$scope.$apply(function () {
				$scope.newUpcomingExam.Pending = allQuestion.count(p1 => p1.AnswerType == 1);
				$scope.newUpcomingExam.Reported = allQuestion.count(p1 => p1.AnswerType == 2);
				$scope.newUpcomingExam.Skiped = allQuestion.count(p1 => p1.AnswerType == 3);
				$scope.newUpcomingExam.Answered = allQuestion.count(p1 => p1.AnswerType == 4);
			});
		});

	};

	$scope.SkipQuestion = function () {
		$scope.RunQ.Question.AnswerType = 3;
		$scope.CalculateAnswerType();
		$scope.nextQuestion();
	};
	$scope.ReportQuestion = function () {

		if ($scope.RunQ.Question.QuestionRemarks && $scope.RunQ.Question.QuestionRemarks.length > 0) {
			$scope.RunQ.Question.AnswerType = 2;
			$scope.CalculateAnswerType();
		}
		$('#exampleModal').modal('hide');

	};

	$scope.ChangeAnsTypeForSubjective = function (val) {
		$scope.RunQ.Question.AnswerOption = val;
	}

	$scope.submitExam = function () {
		Swal.fire({
			title: 'Is your exam complete ?',
			showCancelButton: true,
			cancelButtonText:'Review',
			confirmButtonText: 'Submit',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				//angular.forEach($scope.OnlineExamQuestion, function (rQ) {
				//	$timeout(function () {

				//		var atFiles = rQ.Files_TMP;

				//		var submitAns = {
				//			ExamSetupId: rQ.ExamSetupId,
				//			TranId: rQ.TranId,
				//			Location: rQ.Location,
				//			Lat: rQ.Lat,
				//			Lan: rQ.Lan,
				//			QuestionRemarks: rQ.QuestionRemarks,
				//			AnswerSNo: rQ.AnswerSNo,
				//			AnswerText: rQ.AnswerText,
				//			SubmitType: rQ.AnswerType,
				//			StudentDocColl: []
				//		};

				//		angular.forEach(rQ.AnswerFiles, function (Doc) {
				//			if (typeof (Doc.AnswerFiles) == "string") {
				//				if (Doc.AnswerFiles.toLowerCase().startsWith("http")) {
				//					var path = Doc.AnswerFiles.replace(WEBURLPATH, "");
				//					submitAns.StudentDocColl.push(path);
				//				} else {
				//					submitAns.StudentDocColl.push(Doc.AnswerFiles);
				//                            }
				//			} 
				//		});

				//		$http({
				//			method: 'POST',
				//			url: base_url + "Student/Creation/SubmitOEAnswer",
				//			headers: { 'Content-Type': undefined },

				//			transformRequest: function (data) {

				//				var formData = new FormData();
				//				formData.append("jsonData", angular.toJson(data.jsonData));

				//				if (data.files) {
				//					var vsno = 1;
				//					angular.forEach(data.files, function (fl) {
				//						formData.append("file" + vsno, fl);
				//						vsno++;
				//					});
				//				}

				//				return formData;
				//			},
				//			data: { jsonData: submitAns, files: atFiles }
				//		}).then(function (res)
				//		{

				//			console.log(res.data.ResponseMSG);
				//		}, function (errormessage) {
				//			console.log(errormessage);
				//		});
				//	}, 5000);
				//});

				//$timeout(function () {
				//	$scope.EndOnlineExam();
				//}, 20000);

				$timeout(function () {
					$scope.EndOnlineExam();

				}, 1000);


			} else {
				$scope.RunQ.Question = $scope.OnlineExamQuestion[0];
				$scope.RunQ.QNo = 1;
				$scope.isLastQNo = false;
            }
		});
	};
	var percentremain = 0;
	var distance = 0;
	var fixed = new Date().getTime();
	var timeOver = false;
	$scope.isLastQNo = false;
	$scope.remaingTime = {};


	/// change starts
	var BASE64_MARKER = ';base64,';

	// Does the given URL (string) look like a base64-encoded URL?
	function isDataURI(url) {
		return url.split(BASE64_MARKER).length === 2;
	};
	function dataURItoFile(dataURI) {
		if (!isDataURI(dataURI)) { return false; }

		// Format of a base64-encoded URL:
		// data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAYAAAAEOCAIAAAAPH1dAAAAK
		var mime = dataURI.split(BASE64_MARKER)[0].split(':')[1];
		var filename = 'dataURI-file-' + (new Date()).getTime() + '.' + mime.split('/')[1];
		//var filename = 'dataURI-file-' + (new Date()).getTime() + '.jpeg';
		var bytes = atob(dataURI.split(BASE64_MARKER)[1]);
		var writer = new Uint8Array(new ArrayBuffer(bytes.length));

		for (var i = 0; i < bytes.length; i++) {
			writer[i] = bytes.charCodeAt(i);
		}

		return new File([writer.buffer], filename, { type: mime });
	}
	/// change ends

	$scope.SubmitOEAnswer = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var rQ = $scope.RunQ.Question;
	
		//Change Ends
		//var atFiles = rQ.Files_TMP;
		
		var submitAns = {
			ExamSetupId: rQ.ExamSetupId,
			TranId: rQ.TranId,
			Location: rQ.Location,
			Lat: rQ.Lat,
			Lan: rQ.Lan,
			QuestionRemarks: rQ.QuestionRemarks,
			AnswerSNo: rQ.AnswerSNo,
			AnswerText: rQ.AnswerText,
			SubmitType: rQ.AnswerType,
			StudentDocColl:[]
		};

		var atFiles = [];

		$timeout(function () {
			//Change starts
			angular.forEach(rQ.AnswerFiles, function (Doc) {
				if (typeof (Doc.AnswerFiles) == "string") {
					if (Doc.AnswerFiles.toLowerCase().startsWith("http")) {
						var path = Doc.AnswerFiles.replace(WEBURLPATH, "");
						submitAns.StudentDocColl.push(path);
					} else {
						var rFile = dataURItoFile(Doc.AnswerFiles);
						atFiles.push(rFile);
					}
				} else {
					var rFile = dataURItoFile(Doc.AnswerFiles);
					atFiles.push(rFile);
				}
			});

		});

		$timeout(function () {
			$http({
				method: 'POST',
				url: base_url + "Student/Creation/SubmitOEAnswer",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));
					if (data.files) {
						for (var i = 0; i < data.files.length; i++) {
							if (data.files[i].File)
								formData.append("file" + (i + 1), data.files[i].File);
							//change start
							else if (data.files.length > 0) {
								formData.append("file" + i, data.files[i]);
							}
							//change ends
							else
								formData.append("file" + (i + 1), data.files[i]);
						}
					}

					return formData;
				},
				data: { jsonData: submitAns, files: atFiles }
			}).then(function (res) {
				
				$scope.loadingstatus = "stop";
				hidePleaseWait();

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		});
		
	}

	$scope.SubmitOEAnswerNext = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var rQ = $scope.RunQ.Question;

		//Change Ends
		//var atFiles = rQ.Files_TMP;

		var submitAns = {
			ExamSetupId: rQ.ExamSetupId,
			TranId: rQ.TranId,
			Location: rQ.Location,
			Lat: rQ.Lat,
			Lan: rQ.Lan,
			QuestionRemarks: rQ.QuestionRemarks,
			AnswerSNo: rQ.AnswerSNo,
			AnswerText: rQ.AnswerText,
			SubmitType: rQ.AnswerType,
			StudentDocColl: []
		};

		var atFiles = [];

		$timeout(function () {
			//Change starts
			angular.forEach(rQ.AnswerFiles, function (Doc) {
				if (typeof (Doc.AnswerFiles) == "string") {
					if (Doc.AnswerFiles.toLowerCase().startsWith("http")) {
						var path = Doc.AnswerFiles.replace(WEBURLPATH, "");
						submitAns.StudentDocColl.push(path);
					} else {
						var rFile = dataURItoFile(Doc.AnswerFiles);
						atFiles.push(rFile);
					}
				} else {
					var rFile = dataURItoFile(Doc.AnswerFiles);
					atFiles.push(rFile);
				}
			});

		});

		$timeout(function () {
			$http({
				method: 'POST',
				url: base_url + "Student/Creation/SubmitOEAnswer",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));
					if (data.files) {
						for (var i = 0; i < data.files.length; i++) {
							if (data.files[i].File)
								formData.append("file" + (i + 1), data.files[i].File);
							//change start
							else if (data.files.length > 0) {
								formData.append("file" + i, data.files[i]);
							}
							//change ends
							else
								formData.append("file" + (i + 1), data.files[i]);
						}
					}

					return formData;
				},
				data: { jsonData: submitAns, files: atFiles }
			}).then(function (res) {

				var ind = $scope.RunQ.QNo;
				$timeout(function () {
					if (ind < $scope.OnlineExamQuestion.length) {
						$scope.RunQ.Question = $scope.OnlineExamQuestion[ind];
						$scope.RunQ.QNo = ind + 1;
					} else {
						$scope.isLastQNo = true;
					}
				}, 50);

				$timeout(function () {
					$scope.CalculateAnswerType();
				}, 50);

				$scope.loadingstatus = "stop";
				hidePleaseWait();

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		});

	}
	$scope.SubmitOEAnswerBack = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var rQ = $scope.RunQ.Question;

		//Change Ends
		//var atFiles = rQ.Files_TMP;

		var submitAns = {
			ExamSetupId: rQ.ExamSetupId,
			TranId: rQ.TranId,
			Location: rQ.Location,
			Lat: rQ.Lat,
			Lan: rQ.Lan,
			QuestionRemarks: rQ.QuestionRemarks,
			AnswerSNo: rQ.AnswerSNo,
			AnswerText: rQ.AnswerText,
			SubmitType: rQ.AnswerType,
			StudentDocColl: []
		};

		var atFiles = [];

		$timeout(function () {
			//Change starts
			angular.forEach(rQ.AnswerFiles, function (Doc) {
				if (typeof (Doc.AnswerFiles) == "string") {
					if (Doc.AnswerFiles.toLowerCase().startsWith("http")) {
						var path = Doc.AnswerFiles.replace(WEBURLPATH, "");
						submitAns.StudentDocColl.push(path);
					} else {
						var rFile = dataURItoFile(Doc.AnswerFiles);
						atFiles.push(rFile);
					}
				} else {
					var rFile = dataURItoFile(Doc.AnswerFiles);
					atFiles.push(rFile);
				}
			});

		});

		$timeout(function () {
			$http({
				method: 'POST',
				url: base_url + "Student/Creation/SubmitOEAnswer",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));
					if (data.files) {
						for (var i = 0; i < data.files.length; i++) {
							if (data.files[i].File)
								formData.append("file" + (i + 1), data.files[i].File);
							//change start
							else if (data.files.length > 0) {
								formData.append("file" + i, data.files[i]);
							}
							//change ends
							else
								formData.append("file" + (i + 1), data.files[i]);
						}
					}

					return formData;
				},
				data: { jsonData: submitAns, files: atFiles }
			}).then(function (res) {

				var ind = $scope.RunQ.QNo;
				$scope.isLastQNo = false;
				$timeout(function () {
					if (ind > 0) {
						$scope.RunQ.Question = $scope.OnlineExamQuestion[ind - 1];
						$scope.RunQ.QNo = ind - 1;
					}
					else {

					}
				});

				$timeout(function () {
					$scope.CalculateAnswerType();
				});

				$scope.loadingstatus = "stop";
				hidePleaseWait();

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		});

	}

	$scope.EndExamSummary = {};
	$scope.EndOnlineExam = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var EndExam = {
			ExamSetupId: $scope.newUpcomingExam.ExamSetupId,
			Location: 'OnlineExam',
			Lat: 2.563,
			Lan: 2.69,
			Notes: 'Start From Web'
		}

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/EndOnlineExam",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				if (data.files && data.files.length > 0)
					formData.append("file1", data.files[0]);
				return formData;
			},
			data: { jsonData: EndExam, files: $scope.EndFiles2 }
		}).then(function (res) {

			$timeout(function () {

				var allQuestion = mx($scope.OnlineExamQuestion);
				$scope.$apply(function () {
					$scope.EndExamSummary.ExamType = $scope.newUpcomingExam.ExamTypeName;
					$scope.EndExamSummary.FullMark = $scope.newUpcomingExam.FullMark;
					$scope.EndExamSummary.PassMark = $scope.newUpcomingExam.PassMarks;
					$scope.EndExamSummary.ClassName = $scope.newUpcomingExam.ClassName;
					$scope.EndExamSummary.SubjectName = $scope.newUpcomingExam.SubjectName;
					$scope.EndExamSummary.TotalQuestion = allQuestion.count();
					$scope.EndExamSummary.Pending = allQuestion.count(p1 => p1.AnswerType == 1);
					$scope.EndExamSummary.Reported = allQuestion.count(p1 => p1.AnswerType == 2);
					$scope.EndExamSummary.Skiped = allQuestion.count(p1 => p1.AnswerType == 3);
					$scope.EndExamSummary.Answered = allQuestion.count(p1 => p1.AnswerType == 4);

					$scope.EndExamSummary.PendingPer = ($scope.EndExamSummary.Pending / $scope.EndExamSummary.TotalQuestion) * 100;
					$scope.EndExamSummary.ReportedPer = ($scope.EndExamSummary.Reported / $scope.EndExamSummary.TotalQuestion) * 100;
					$scope.EndExamSummary.SkipedPer = ($scope.EndExamSummary.Skiped / $scope.EndExamSummary.TotalQuestion) * 100;
					$scope.EndExamSummary.AnsweredPer = ($scope.EndExamSummary.Answered / $scope.EndExamSummary.TotalQuestion) * 100;

					new Chart(document.getElementById("bar-chart"), {
						type: 'bar',

						data: {
							labels: ["Answered", "Skiped", "Pending", "", "Reported"],
							datasets: [
								{
									label: "",
									backgroundColor: ["#5cb85c", "#f0ad4e", "#0275d8", "#025A58"],
									data: [$scope.EndExamSummary.Answered, $scope.EndExamSummary.Skiped, $scope.EndExamSummary.Pending, $scope.EndExamSummary.Reported]
								}
							]
						},
						options: {
							display: true,
							width: '500',
							responsive: false,
							scales: {
								yAxes: [{
									ticks: {
										min: 2,
										max: 10,
										stepSize: 2,
										callback: function (value) { return value }
									}
								}],
								xAxes: [{
									ticks: {
										min: 2,
										max: 10,
										stepSize: 2,
										callback: function (value) { return value }
									}
								}]

							},

						}
					});

				});

				$scope.RunQ = null;
				$scope.OnlineExamQuestion = [];
				$scope.UpcomingExamList = [];
				$scope.newUpcomingExam = null;
			});

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
			document.getElementById('question-summary').style.display = "block";
			document.getElementById('exam-questions').style.display = "none";

			//document.getElementById('question-summary').style.display = "block";
			//document.getElementById('exam-questions').style.display = "none";

			if (res.data.IsSuccess == true) {

			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllPreviousExamList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PreviousExamList = [];

		$http({
			method: 'POST',
			url: base_url + "Creation/Creation/GetAllPreviousExamList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PreviousExamList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetPreviousExamById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			PreviousExamId: refData.PreviousExamId
		};

		$http({
			method: 'POST',
			url: base_url + "Creation/Creation/GetPreviousExamById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newPreviousExam = res.data.Data;
				$scope.newPreviousExam.Mode = 'Modify';

				//document.getElementById('class-section').style.display = "none";
				//document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	//online Exam chart js

	

	//Previous Exam chart js

	new Chart(document.getElementById("bar-chart-previous-exam"), {
		type: 'bar',

		data: {
			labels: ["", "", "", "", ""],
			datasets: [
				{
					label: "",
					backgroundColor: ["#5cb85c", "#f0ad4e", "#0275d8", "#025A58"],
					data: [10, 8, 6, 4]
				}
			]
		},
		options: {
			display: true,
			width: '500',
			responsive: false,
			scales: {
				yAxes: [{
					ticks: {
						min: 2,
						max: 10,
						stepSize: 2,
						callback: function (value) { return value }
					}
				}],
				xAxes: [{
					ticks: {
						min: 2,
						max: 10,
						stepSize: 2,
						callback: function (value) { return value }
					}
				}]

			},

		}
	});





	////
	$scope.ToggleImg = function (item) {
		$scope.ImagePath = item;
		$('#ToggleImg').modal('show');
	};


	$scope.configure = function () {
		Webcam.set({
			width: 320,
			height: 240,
			image_format: 'jpeg',
			jpeg_quality: 90
		});
		Webcam.attach('#my_camera');
	}

	$scope.take_snapshot = function () {

		// take snapshot and get image data
		Webcam.snap(function (data_Url) {
			// display results in page
			document.getElementById('results').innerHTML =
				'<img id="imageprev" src="' + data_Url + '"/>';
		});

		Webcam.reset();
	}


	//Changes file starts
	$scope.AfterChooseFile = function () {
		if (!$scope.RunQ.Question.AnswerFiles)
			$scope.RunQ.Question.AnswerFiles = [];

		if ($scope.RunQ.Question.Files_TMP && $scope.RunQ.Question.Files_Data && $scope.RunQ.Question.Files_TMP.length == $scope.RunQ.Question.Files_Data.length) {


			var ind = 0;
			angular.forEach($scope.RunQ.Question.Files_TMP, function (fl) {
				$scope.RunQ.Question.AnswerFiles.push({
					file: fl,
					FileName: fl.name,
					AnswerFiles: $scope.RunQ.Question.Files_Data[ind]
				});
				ind++;
			});
		}
	};

	$scope.DeleteAlbumPhoto = function (id) {
		$scope.loadingstatus = "running";
		$scope.loadingstatus = "stop";
		Swal.fire({
			icon: "info",
			text: 'Are you sure to delete selected Photo ',
			buttons: true,
			dangerMode: true,
		}).then((e) => {
			if (e) {
				$timeout(function () {
					$scope.RunQ.Question.AnswerFiles.splice(id, 1);
				});

			}
			else {
				Swal.fire("Nothing Deleted", '', 'info');
			}
		})
	}



	$scope.DocumentAtt_Toggle = function (fpath) {		
		if (fpath && fpath !== '') {

			document.body.style.cursor = 'wait';
			document.getElementById("DocumentAtt_Iframe").src = '';
			document.getElementById("DocumentAtt_Iframe").src = fpath;
			document.body.style.cursor = 'default';
			$('#modalIMG').modal('show');
		}


	};

	/// change ends


});