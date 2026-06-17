String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};

app.controller('QuestionSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Question Setup';


	$scope.LoadData = function () {
		// Calling ExamTypeList
		$scope.getLetter = function (index) {
			return String.fromCharCode(65 + index);
		}
		$scope.SelectedQuestion = null;
		
		$scope.ExamTypeColl = [];
		$http.get(base_url + "OnlineExam/Creation/ExamTypeforEntity")
			.then(function (data) {
				$scope.ExamTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});

		//Get class and Section List
		$scope.ClassColl = [];
		$scope.SectionColl = [];
		$http.get(base_url + "StudentAttendance/Creation/GetClassSection")
			.then(function (data) {
				$scope.ClassColl = data.data.ClassList;
				$scope.SectionClassColl = data.data.SectionList;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				alert("Couldn't find data");
			});

	


		$scope.CheckValue = 1;
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			QuestionSetup: 1,

		};

		$scope.searchData = {
			QuestionSetup: '',

		};

		$scope.perPage = {
			QuestionSetup: GlobalServices.getPerPageRow(),

		};

		$scope.newQuestionSetup = {
			QuestionSetupId: null,
			ExamId: null,
			ModalId: null,
			CategoryId: null,
			QuestionModelColl: [],

			Mode: 'Save'
		};
		$scope.newQuestionSetup.QuestionModelColl.push({});
		//$scope.GetAllQuestionSetupList();

	};


	


	$scope.ClearQuestionSetup = function () {
		$timeout(function () {
			$scope.SubjectColl = [];
			$scope.QuestionColl = [];
			$scope.CategoryList = [];
			$scope.SelectedQuestion = null;
			$scope.SubjectColl = [];
			$scope.newQuestionSetup = {
				QuestionSetupId: null,
				ExamId: null,
				ModalId: null,
				CategoryId: null,
				Question: '',
				SectionId: null,
				QuestionModelColl: [],

				Mode: 'Save'
			};
			$('#cboSectionId').val(null).trigger('change');
			$('.textarea').val(null).trigger('change');
			$scope.newQuestionSetup.QuestionModelColl.push({});
		})
		
	};


	//$(document).ready(function () {
	//	$('#negativemarkCheckbox').change(function () {
	//		$('#negativemarkCheckboxdiv').toggle();
	//	});
	//});

	$scope.AddQuestionMode = function (ind,qDet) {
		if ($scope.SelectedQuestion.DetailsColl && qDet)
		{
			//if (!qDet.AnswerTitle || qDet.AnswerTitle == 1)
			//{
			//	if (!qDet.Answer || qDet.Answer.isEmpty())
			//	{
			//		Swal.fire('Please ! Enter Answer');
			//		return;
			//	}				
			//}
			
			if ($scope.SelectedQuestion.DetailsColl.length > ind + 1) {
				$scope.SelectedQuestion.DetailsColl.splice(ind + 1, 0, {
					ClassName: '',
					Answer: '',
					AnswerTitle:1
				})
			} else {
				$scope.SelectedQuestion.DetailsColl.push({
					ClassName: '',
					Answer: '',
					AnswerTitle: 1
				})
			}
		}
	};
	$scope.delQuestionMode = function (ind) {
		if ($scope.SelectedQuestion.DetailsColl) {
			if ($scope.SelectedQuestion.DetailsColl.length > 1) {
				$scope.SelectedQuestion.DetailsColl.splice(ind, 1);
			}
		}
	};
	$scope.IsValidQuestionSetup = function () {
		//if ($scope.newQuestionSetup.Lesson.isEmpty()) {
		//	Swal.fire('Please ! Enter Lesson');
		//	return false;
		//}
		return true;
	};

	$scope.OnClassChange = function () {

		$scope.SectionColl = [];
		$scope.SectionList = [];		
		var cid = $scope.newQuestionSetup.ClassId;		 
		angular.forEach($scope.SectionClassColl, function (SVCollData) {
			if (cid == SVCollData.ClassId) {
				$scope.Section = SVCollData;
				$scope.SectionColl.push($scope.Section);
			}
		})

		$timeout(function () {
			$scope.SectionList = $scope.SectionColl;
		});

		$scope.newQuestionSetup.sectionIdColl = $scope.newQuestionSetup.sectionIdColl ? $scope.newQuestionSetup.sectionIdColl : '';
		$timeout(function () {
			$scope.GetSubjectList();
		});

		$timeout(function () {
			$scope.GetExamSetupDetails();
		});
	};
	
	$('#cboSectionId').on("change", function (e) {
		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.SectionId = select_val;
		$scope.newQuestionSetup.sectionIdColl = ($scope.SectionId && $scope.SectionId.length > 0 ? $scope.SectionId.toString() : '')

		$timeout(function () {
			$scope.GetSubjectList();
		});

		$timeout(function () {
			$scope.GetExamSetupDetails();
		});

		
	});

	$scope.OnSubjectChange = function () {
		$timeout(function () {
			$scope.GetExamSetupDetails();
		});
    }
	

	///For Category Coll
	$scope.GetExamSetupDetails = function () {
		$scope.CategoryList = [];
		if ($scope.newQuestionSetup.ExamTypeId && $scope.newQuestionSetup.ClassId && $scope.newQuestionSetup.SubjectId) {
			var Dt = {
				examTypeId: $scope.newQuestionSetup.ExamTypeId,
				classId: $scope.newQuestionSetup.ClassId,
				sectionIdColl: ($scope.newQuestionSetup.sectionIdColl ? $scope.newQuestionSetup.sectionIdColl : ''),
				subjectId: $scope.newQuestionSetup.SubjectId

			}

			$http({
				method: 'POST',
				url: base_url + "OnlineExam/Creation/GetExamSetupDetails",
				data: Dt,
				dataType: "json"
			}).then(function (res) {
				if (res.data) {
					$scope.CategoryList = res.data;

				}
				hidePleaseWait();
				$scope.loadingstatus = "stop";
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}
	}
	
	$scope.GetCategoryData = function () {
		if ($scope.SelectedCategory) {
			if ($scope.SelectedCategory.ExamModal == 1) {
				$scope.newQuestionSetup.ExamModal = 'Subjective'
			}
			else if ($scope.SelectedCategory.ExamModal == 2){
				$scope.newQuestionSetup.ExamModal = 'Objective'
			}
		}
		$scope.SelectedQuestion = null;
		//Calling Question Setup Id 
	//	
		if ($scope.SelectedCategory) {
			var category = $scope.SelectedCategory;//CategoryId//ExamSetupId
			if (category.ExamSetupId && category.CategoryId) {
				var Dt = {
					examSetupId: $scope.SelectedCategory.ExamSetupId,
					categoryId: $scope.SelectedCategory.CategoryId,
				}

				$http({
					method: 'POST',
					url: base_url + "OnlineExam/Creation/GetQuestionByExamSetupId",
					data: Dt,
					dataType: "json"
				}).then(function (res) {
					if (res.data)
					{
						var eData = mx(res.data);
						for (var i = 1; i <= category.NoOfQuestion; i++) {

							var ed = eData.firstOrDefault(p1 => p1.QNo == i);
							var question = {
								ExamSetupId: category.ExamSetupId,
								CategoryId: category.CategoryId,
								QNo: i,
								Marks: (ed ? ed.Marks : category.Marks),
								QuestionTitle: (ed ? ed.QuestionTitle : 1),
								AnswerTitle: (ed ? ed.AnswerTitle : 1),
								Question: (ed ? ed.Question : ''),
								QuestionPath: (ed ? ed.QuestionPath : null),							  
								DetailsColl: (ed ? ed.DetailsColl : [] )
							};

							if(question.DetailsColl.length==0)
								question.DetailsColl.push({});

							$scope.QuestionColl.push(question);

						}

						if ($scope.QuestionColl && $scope.QuestionColl.length > 0)
							$scope.SelectedQuestion = $scope.QuestionColl[0];
					}
					
					hidePleaseWait();
					$scope.loadingstatus = "stop";
				}, function (reason) { Swal.fire('Failed' + reason); });

			}
			$scope.newQuestionSetup.Marks = category.Marks
			$scope.QuestionColl = [];
		
			
		}

	}

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

				if ($scope.SelectedQuestion.DetailsColl && $scope.SelectedCategory.ExamModal==2) {

					if ($scope.SelectedQuestion.DetailsColl.length > 0)
					{
						var count = mx($scope.SelectedQuestion.DetailsColl).count(p1 => p1.IsRightAnswer == true);
						if (count == 0) {
							Swal.fire('Please Choose Correct Answer');
						} else {

							var valid = true;
							angular.forEach($scope.SelectedQuestion.DetailsColl, function (det) {

								if ($scope.SelectedQuestion.AnswerTitle==1) {
									if (!det.Answer || det.Answer.isEmpty())
									{
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




	$scope.CallSaveUpdateQuestionSetup = function (sQuestion,showMSG) {

		if (!sQuestion)
			return;

		$scope.loadingstatus = "running";
		showPleaseWait();
		var sQ = sQuestion;
		var qFile = $scope.SelectedQuestion.Files_TMP;
		
		var ansFiles = [];
		var question = {
			ExamSetupId: sQ.ExamSetupId,
			CategoryId: sQ.CategoryId,
			QNo: sQ.QNo,
			Marks: sQ.Marks,
			QuestionTitle: sQ.QuestionTitle,
			AnswerTitle: sQ.AnswerTitle,
			Question: sQ.Question,
			DetailsColl: [],
			QuestionPath:sQ.QuestionPath
		};
		var sno = 1;
		angular.forEach(sQ.DetailsColl, function (dc) {
			
			question.DetailsColl.push({
				SNo: sno,
				Answer: dc.Answer,
				IsRightAnswer: (dc.IsRightAnswer ? dc.IsRightAnswer : false),
				FilePath:dc.FilePath
			});


			sno++;
			if (dc.Files_TMP) {
				angular.forEach(dc.Files_TMP, function (f) {
					ansFiles.push(f);
				});
			}
		});

		$http({
			method: 'POST',
			url: base_url + "OnlineExam/Creation/AddQuestionSetup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files && data.files.length > 0)
					formData.append("question", data.files[0]);

				if (data.aFiles) {
					for (var i = 0; i < data.aFiles.length; i++) {

						if (data.aFiles[i].File)
							formData.append("answer" + (i + 1), data.aFiles[i].File);
						else
							formData.append("answer" + (i + 1), data.aFiles[i]);
					}
				}

				return formData;
			},
			data: { jsonData: question, files: qFile, aFiles: ansFiles }
		}).then(function (res) {
			if (res && showMSG==true) {
				Swal.fire(res.data.ResponseMSG);
            }
			$scope.loadingstatus = "stop";
			hidePleaseWait();


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllQuestionSetupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.QuestionSetupList = [];

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllQuestionSetupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.QuestionSetupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetQuestionSetupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			QuestionSetupId: refData.QuestionSetupId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetQuestionSetupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newQuestionSetup = res.data.Data;
				$scope.newQuestionSetup.Mode = 'Modify';

				document.getElementById('class-section').style.display = "none";
				document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelQuestionSetupById = function (refData) {

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
					QuestionSetupId: refData.QuestionSetupId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelQuestionSetup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllQuestionSetupList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};
	$scope.GetSubjectList = function () {
		$scope.loadingstatus = "running";		
		$scope.SubjectColl = [];
		if ($scope.newQuestionSetup.ClassId) {
			var para = {
				classId: $scope.newQuestionSetup.ClassId,
				sectionIdColl: $scope.newQuestionSetup.sectionIdColl
			};
			$http({
				method: 'POST',
				url: base_url + "OnlineClass/Creation/GetSubjectList",
				dataType: "json",
				data: para
			}).then(function (res) {
				
				$scope.loadingstatus = "stop";
				if (res.data) {
					$timeout(function () {
						$scope.SubjectColl = res.data;
					});
					
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}



	}

});