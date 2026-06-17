

app.controller('ExamSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'ExamSetup';
	var glbS = GlobalServices;

	OnClickDefault();
	$scope.getLetter = function (index) {
		return String.fromCharCode(65 + index);
	}
	$scope.LoadData = function () {

		//$scope.GetAllExamSetup();

		$scope.ExamTypeColl = [];
		glbS.getExamTypeList().then(function (res) {
			$scope.ExamTypeColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//Get class and Section List
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

		// Calling ExamTypeList
		$scope.CategoryListColl = [];
		$http.post(base_url + "Elearning/Creation/GetAllExamModalList")
			.then(function (res) {
				$scope.CategoryListColl = res.data.Data;
			}, function (reason) {
				alert("Data not getfff");
			});

		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			ExamSetup: 1,

		};

		$scope.searchData = {
			ExamSetup: '',

		};

		$scope.perPage = {
			ExamSetup: GlobalServices.getPerPageRow(),

		};

		$scope.newExamSetup = {
			ExamSetupId: null,
			ExamTypeId: null,
			ClassId: null,
			SectionId: null,
			SubjectId: null,
			Lesson: '',
			Time: '',
			Duration: '',
			FullMarks: '',
			PassMarks: '',
			Instruction: '',

			IsAlerttoStudents: true,
			QuestionModelColl: [],
			Mode: 'Save'


		};
		$scope.newExamSetup.QuestionModelColl.push({});
		$scope.GetAllExamSetup();

	};

	$scope.GetClassWiseSubMap = function () {

		$scope.newExamSetup.SubjectList = [];

		if ($scope.newExamSetup.ClassId && $scope.newExamSetup.ClassId > 0) {
			var para = {
				ClassId: $scope.newExamSetup.ClassId,
				SectionIdColl: ($scope.newExamSetup.SectionId ? $scope.newExamSetup.SectionId.toString() : '')
			};


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
								subDet.SubjectType = 1;
								subDet.OTH = 1;
								subDet.OPR = 1;
								$scope.newExamSetup.SubjectList.push(subDet);
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

	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa
	}

	$scope.ClearExamSetup = function () {
		$timeout(function () {
			$scope.newExamSetup = {
				ExamSetupId: null,
				ExamTypeId: null,
				ClassId: null,
				SectionId: null,
				SubjectId: null,
				Lesson: '',
				Time: '',
				Duration: '',
				FullMarks: '',
				PassMarks: '',
				Instruction: null,

				IsAlerttoStudents: true,
				QuestionModelColl: [],
				Mode: 'Save'
			};
			$('.select2').val(null).trigger('change');
			//$('.select2').val(null).trigger('change');

			$scope.newExamSetup.QuestionModelColl.push({});
		})

	};

	$scope.AddQuestionMode = function (ind) {
		if ($scope.newExamSetup.QuestionModelColl) {
			if ($scope.newExamSetup.QuestionModelColl.length > ind + 1) {
				$scope.newExamSetup.QuestionModelColl.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newExamSetup.QuestionModelColl.push({
					ClassName: ''
				})
			}
		}
	};
	$scope.delQuestionMode = function (ind) {
		if ($scope.newExamSetup.QuestionModelColl) {
			if ($scope.newExamSetup.QuestionModelColl.length > 1) {
				$scope.newExamSetup.QuestionModelColl.splice(ind, 1);
			}
		}
	};

	function OnClickDefault() {
		document.getElementById('exam-setup-form').style.display = "none";

		document.getElementById('add-exam-setup').onclick = function () {
			document.getElementById('table-listing').style.display = "none";
			document.getElementById('exam-setup-form').style.display = "block";
		}

		document.getElementById('back-btn').onclick = function () {
			document.getElementById('table-listing').style.display = "block";
			document.getElementById('exam-setup-form').style.display = "none";
		}

	};


	//$(document).ready(function () {
	//	$('#negativemarkCheckbox').change(function () {
	//		$('#negativemarkCheckboxdiv').toggle();
	//	});
	//});


	$scope.IsValidExamSetup = function () {
		if ($scope.newExamSetup.Lesson.isEmpty()) {
			Swal.fire('Please ! Enter Lesson');
			return false;
		}
		return true;
	};

	$scope.GetExamDet = function () {
		$scope.cboSectionId();
		if ($scope.ExamTypeColl) {
			var st = mx($scope.ExamTypeColl).firstOrDefault(p1 => p1.ExamTypeId == $scope.newExamSetup.ExamTypeId);

			if (st) {
				if (st.ResultDate)
					$scope.newExamSetup.ResultDate_TMP = new Date(st.ResultDate);

				if (st.ResultTime)
					$scope.newExamSetup.ResultTime_TMP = new Date(st.ResultTime);

				if (st.ExamDate)
					$scope.newExamSetup.ExamDate_TMP = new Date(st.ExamDate);

				if (st.StartTime)
					$scope.newExamSetup.StartTime_TMP = new Date(st.StartTime);

				if (st.Duration)
					$scope.newExamSetup.Duration = st.Duration;
			}

		}
	}
	$scope.OnChangeCategoryList = function (Det) {
		angular.forEach($scope.CategoryListColl, function (st) {
			if (Det.CategoryId == st.CategoryId) {
				if (st.ExamModalType == 1) {
					Det.ExamModalName = 'Subjective';
				}
				else if (st.ExamModalType == 2) {
					Det.ExamModalName = 'Objective';
				}

			}


		});


	}

	$scope.checkFullMark = function () {
		if ($scope.newExamSetup.FullMarks <= 0) {
			alert("Full marks Cannot be less than Zero");
			$scope.newExamSetup.FullMarks = 0;
		}

	}
	$scope.NotGreaterthenFullMark = function () {
		if ($scope.newExamSetup.FullMarks) {
			if ($scope.newExamSetup.FullMarks < $scope.newExamSetup.PassMarks) {
				alert("Pls Enter valid Pass mark")
				$scope.newExamSetup.PassMarks = 0;
			}

		} else {
			alert('Pls Enter Fullmark');
			$scope.newExamSetup.PassMarks = 0;
		}
	}


	$scope.cboSectionId = function () {

		if ($('#cboSection').val() != null) {
			var arr = ($('#cboSection').val().toString());
			$scope.newExamSetup.SectionIdColl = arr;
		} else
			$scope.newExamSetup.SectionIdColl = '';

		//$scope.newExamSetup.SectionIdColl = ($scope.newExamSetup.SectionId && $scope.newExamSetup.SectionId.length > 0 ? $scope.newExamSetup.SectionId.toString() : '');
		/*$scope.GetSubjectList();*/

	};

	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newExamSetup.AttachmentColl) {
			if ($scope.newExamSetup.AttachmentColl.length > 0) {
				$scope.newExamSetup.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newExamSetup.AttachmentColl.push({
						DocumentTypeId: docType.id,
						DocumentTypeName: docType.text,
						File: file,
						Name: file.name,
						Type: file.type,
						Size: file.size,
						Description: des,
						Path: null
					});
				})

				$scope.docType = null;
				$scope.attachFile = null;
				$scope.docDescription = '';
			}
		}
	};

	$scope.CalcTotal = function (rrr) {

		rrr.Total = rrr.NoOfQuestion * rrr.Marks;
		if (rrr.Total) {

			$scope.Total = 0
			angular.forEach($scope.newExamSetup.QuestionModelColl, function (W) {

				$scope.Total = W.Total + $scope.Total;
				if ($scope.Total > $scope.newExamSetup.FullMarks) {
					rrr.Marks = 0
					rrr.Total = null
					alert('Marks cannot be set more then full mark')
				}

			});
		}

	};

	$scope.SaveUpdateExamSetup = function () {
		if ($scope.IsValidExamSetup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamSetup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamSetup();
					}
				});
			} else
				$scope.CallSaveUpdateExamSetup();

		}
	};

	$scope.CallSaveUpdateExamSetup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newExamSetup.ExamDateDet) {

			$scope.newExamSetup.ExamDate = $filter('date')(new Date($scope.newExamSetup.ExamDateDet.dateAD), 'yyyy-MM-dd');;
			$scope.newExamSetup.ExamDate_AD = $filter('date')(new Date($scope.newExamSetup.ExamDateDet.dateAD), 'yyyy-MM-dd');;
		} else
			$scope.newExamSetup.ExamDate = null;

		if ($scope.newExamSetup.ResultDateDet) {
			$scope.newExamSetup.ResultDate = $filter('date')(new Date($scope.newExamSetup.ResultDateDet.dateAD), 'yyyy-MM-dd');
			$scope.newExamSetup.ResultDate_AD = $filter('date')(new Date($scope.newExamSetup.ResultDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newExamSetup.ResultDate = null;

		//$scope.newExamSetup.StartTime = $scope.newExamSetup.StartTime_TMP.toLocaleString();
		if ($scope.newExamSetup.StartTime_TMP)
			$scope.newExamSetup.StartTime = $filter('date')(new Date($scope.newExamSetup.StartTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		else
			$scope.newExamSetup.StartTime = null;

		if ($scope.newExamSetup.ResultTime_TMP)
			$scope.newExamSetup.ResultTime = $filter('date')(new Date($scope.newExamSetup.ResultTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		else
			$scope.newExamSetup.ResultTime = null;

		$scope.newExamSetup.SectionIdColl = $scope.newExamSetup.SectionId.toString();
		//$scope.newExamSetup.Instruction =  $('#Instruction').val();

		$http({
			method: 'POST',
			url: base_url + "Elearning/Creation/AddExamSetup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamSetup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();


			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$('#Instruction').val(null).trigger('change');
				$('#cboSectionId').val(null).trigger('change');
				$scope.ClearExamSetup();
				$scope.GetAllExamSetup();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};


	$scope.GetAllExamSetup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamSetupList = [];

		$http({
			method: 'POST',
			url: base_url + "Elearning/Creation/GetAllExamSetup",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.Data && res.data.IsSuccess == true) {
				$scope.ExamSetupList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetExamSetupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ExamSetupId: refData.ExamSetupId
		};

		$http({
			method: 'POST',
			url: base_url + "Elearning/Creation/GetExamSetupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.Data && res.data.IsSuccess == true) {
				$scope.newExamSetup = res.data.Data;
				var subId = $scope.newExamSetup.SubjectId;
				$timeout(function () {
					if (res.data.Data.SectionIdColl) {

						var arr = res.data.Data.SectionIdColl.split(',');
						var sectionIdColl = arr.map(Number);
						$scope.newExamSetup.SectionIdColl = sectionIdColl;
						$scope.newExamSetup.SectionId = sectionIdColl;

						var ethin = [];
						angular.forEach(arr, function (edet) {
							ethin.push(parseInt(edet));
						});
						$scope.newExamSetup.SectionIdColl_TMP = ethin;
						$('#cboSection').val(ethin).trigger('change');


					} else
						$('#cboSection').val(null).trigger('change');
				});

				$timeout(function () {
					/*$scope.GetSubjectList();*/
				});

				$timeout(function () {
					$scope.newExamSetup.SubjectId = subId;
				});

				$timeout(function () {

					if ($scope.CategoryListColl) {
						var catCOll = mx($scope.CategoryListColl);
						angular.forEach($scope.newExamSetup.QuestionModelColl, function (qm) {
							var fC = catCOll.firstOrDefault(p1 => p1.CategoryId == qm.CategoryId);
							if (fC) {
								if (fC.ExamModalType == 1) {
									qm.ExamModalName = "Subjective";
								} else if (fC.ExamModalType == 2) {
									qm.ExamModalName = "Objective";
								}
							}
						});
					}

				});

				if ($scope.newExamSetup.ExamDate)
					$scope.newExamSetup.ExamDate_TMP = new Date($scope.newExamSetup.ExamDate);

				if ($scope.newExamSetup.StartTime)
					$scope.newExamSetup.StartTime_TMP = new Date($scope.newExamSetup.StartTime);

				if ($scope.newExamSetup.ResultDate)
					$scope.newExamSetup.ResultDate_TMP = new Date($scope.newExamSetup.ResultDate);

				if ($scope.newExamSetup.ResultTime)
					$scope.newExamSetup.ResultTime_TMP = new Date($scope.newExamSetup.ResultTime);

				$scope.newExamSetup.Mode = 'Modify';
				document.getElementById('table-listing').style.display = "none";
				document.getElementById('exam-setup-form').style.display = "block";


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExamSetupById = function (refData) {

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
					examSetupId: refData.ExamSetupId
				};
				$http({
					method: 'POST',
					url: base_url + "OnlineExam/Creation/DelExamSetupById",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExamSetup();
						Swal.fire(res.data.ResponseMSG);
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
		showPleaseWait();
		$scope.SubjectColl = [];

		if ($('#cboSection').val() != null) {
			var arr = ($('#cboSection').val().toString());
			$scope.newExamSetup.SectionIdColl = arr;
		}

		if ($scope.newExamSetup.SectionIdColl && $scope.newExamSetup.ClassId) {
			var para = {
				classId: $scope.newExamSetup.ClassId,
				sectionIdColl: $scope.newExamSetup.SectionIdColl
			};
			$http({
				method: 'POST',
				url: base_url + "OnlineClass/Creation/GetSubjectList",
				dataType: "json",
				data: para
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					$scope.SubjectColl = res.data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.validateDates = function (changedField) {
		if (!$scope.newExamSetup.ExamDateDet || !$scope.newExamSetup.ResultDateDet ||
			!$scope.newExamSetup.ExamDateDet.dateAD || !$scope.newExamSetup.ResultDateDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newExamSetup.ExamDateDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newExamSetup.ResultDateDet.dateAD), 'yyyy-MM-dd')
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'ExamDate') {
				Swal.fire({
					icon: 'warning',
					text: 'Start Date cannot be After Result Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newExamSetup.ExamDate_TMP = new Date();
						$scope.newExamSetup.ExamDateDet = new Date();
					});
				});
			} else if (changedField === 'ResultDate') {
				Swal.fire({
					icon: 'warning',
					text: 'Result Date cannot be Before Start Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newExamSetup.ResultDate_TMP = new Date();
						$scope.newExamSetup.ResultDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};
});
