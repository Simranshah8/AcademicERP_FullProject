app.controller('examSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Configuration';

	var gSrv = GlobalServices;

	$scope.LoadData = function () {

		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();

		$scope.entity = {
			GeneralMarkSheet: 360,
			GroupMarkSheet: 362,
			AdmitCard: 358
		};

		$scope.RankAsList = [{ id: 1, text: 'PercentageWise' }, { id: 2, text: 'GPAWise' }]
		$scope.DivisionAsList = [{ id: 1, text: 'PercentageWise' }, { id: 2, text: 'Manual' }]
		$scope.CommentAsList = [{ id: 1, text: 'PercentageWise' }, { id: 2, text: 'GPAWise' }, { id: 3, text: 'RankWise' }, { id: 4, text: 'ResultWise' }, { id: 5, text: 'StudentWise' }, { id: 6, text: 'Grade Wise ' }];
		$scope.ResultList = [{ id: 1, text: 'Pass' }, { id: 2, text: 'Fail' }];
		$scope.ConditionForResultList = [{ id: 1, text: 'Fail in TH and Pass in Total' }, { id: 2, text: 'Fail in PR and Pass in Total' }];
		$scope.MarkTypeList = [{ id: 1, text: 'Marking' }, { id: 2, text: 'Grading' }, { id: 3, text: 'Both' }];
		$scope.GPAList = [{ id: 1, text: 'Average Of Subject GP' }, { id: 2, text: 'GP As FullMark' }, { id: 3, text: 'On The Basis of CreditHour TH_PR' }, { id: 4, text: 'On The Basis of CreditHour' }];
		$scope.GPList = [{ id: 1, text: 'PercentageWise' }, { id: 2, text: 'On The Basis of CreditHour' }, { id: 3, text: 'On The Basis of WGP' }];
		$scope.ClassList = [];

		$scope.AdmitCardTemplatesColl = [];
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=358&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AdmitCardTemplatesColl = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GradeList = [];
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllGradeList",
			dataType: "json"
		}).then(function (res) {
			$scope.GradeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.MonthList = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassSection = {};
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ExamTypeList = [];
		gSrv.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ExamTypeGroupList = [];
		gSrv.getExamTypeGroupList().then(function (res) {
			$scope.ExamTypeGroupList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$timeout(function () {
			gSrv.getClassList().then(function (res) {
				$scope.ClassList = res.data.Data;

				$scope.examConfig = {
					ExamAttendanceColl: [],
				};

				$scope.GetExamConfig();

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});

		$scope.GetSeatPlanExamConfig();

		$scope.newBlock = {
			SelectedClass: null,
			ExamTypeId: null,
			Mode: 'Save'
		};

		$scope.newBlockGroup = {
			SelectedClass: null,
			ExamTypeId: null,
			Mode: 'Save'
		};

		$scope.newTemplate = {
			ExamTypeId: null,
			ExamTypeGroupId: null
		};
	}

	$scope.ToggleForClassWiseRank = function () {
		if ($scope.examConfig.ForClassWiseRank === true) {
			angular.forEach($scope.examConfig.ClassWiseRankList, function (cl) {
				cl.RankAs = $scope.examConfig.StudentRankAs;
			});
		}
	};


	$scope.IsValidConfig = function () {
		//if ($scope.newStudent.RegdPrefix.isEmpty()) {
		//	Swal.fire('Please ! Enter Regd. Prefix');
		//	return false;
		//}

		//if ($scope.newStudent.RegdSuffix.isEmpty()) {
		//	Swal.fire('Please ! Enter Regd. Suffix');
		//	return false;
		//}



		return true;
	}

	$scope.SaveUpdateConfig = function () {
		if ($scope.IsValidConfig() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudent.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateConfig();
					}
				});
			} else
				$scope.CallSaveUpdateConfig();

		}
	};


	$scope.ClearexamConfig = function () {
		$scope.newexamConfig = {
			examConfigId: null,
			ExamType: null,
			ExamAttendanceColl: [],
			Mode: 'Save'
		};
		$scope.newexamConfig.ExamAttendanceColl.push({});

	};

	$scope.AddExamAttendance = function (ind) {
		if ($scope.examConfig.ExamAttendanceColl) {
			if ($scope.examConfig.ExamAttendanceColl.length > ind + 1) {
				$scope.examConfig.ExamAttendanceColl.splice(ind + 1, 0, {
					ExamType: ''
				})
			} else {
				$scope.examConfig.ExamAttendanceColl.push({
					ExamType: ''
				})
			}
		}
	};
	$scope.delExamAttendance = function (ind) {
		if ($scope.examConfig.ExamAttendanceColl) {
			if ($scope.examConfig.ExamAttendanceColl.length > 1) {
				$scope.examConfig.ExamAttendanceColl.splice(ind, 1);
			}
		}
	};
	//ends

	$scope.CallSaveUpdateConfig = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamConfiguration",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.examConfig }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetExamConfig = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.examConfig = res.data.Data;

				var cRankList = mx($scope.examConfig.ClassWiseRankList);
				$scope.examConfig.ClassWiseRankList = [];
				angular.forEach($scope.ClassList, function (cl) {

					var find = cRankList.firstOrDefault(p1 => p1.ClassId == cl.ClassId);

					$scope.examConfig.ClassWiseRankList.push({
						ClassId: cl.ClassId,
						RankAs: find ? find.RankAs : 1,
						text: cl.Name
					});
				});

				var cCommetist = mx($scope.examConfig.ClassWiseCommentList);
				$scope.examConfig.ClassWiseCommentList = [];
				angular.forEach($scope.ClassList, function (cl) {

					var find = cCommetist.firstOrDefault(p1 => p1.ClassId == cl.ClassId);

					$scope.examConfig.ClassWiseCommentList.push({
						ClassId: cl.ClassId,
						CommentAs: find ? find.CommentAs : 1,
						text: cl.Name
					});
				});


				var cPassFailList = mx($scope.examConfig.ConditionForFailPassList);
				$scope.examConfig.ConditionForFailPassList = [];
				angular.forEach($scope.ClassList, function (cl) {

					var find = cPassFailList.firstOrDefault(p1 => p1.ClassId == cl.ClassId);

					$scope.examConfig.ConditionForFailPassList.push({
						ClassId: cl.ClassId,
						Condition1: find ? find.Condition1 : 1,
						Result1: find ? find.Result1 : 2,
						Condition2: find ? find.Condition2 : 2,
						Result2: find ? find.Result2 : 1,
						text: cl.Name
					});
				});



				var cSymList = mx($scope.examConfig.ClassWiseStarSymbolList);
				$scope.examConfig.ClassWiseStarSymbolList = [];
				angular.forEach($scope.ClassList, function (cl) {

					var find = cSymList.firstOrDefault(p1 => p1.ClassId == cl.ClassId);

					$scope.examConfig.ClassWiseStarSymbolList.push({
						ClassId: cl.ClassId,
						ShowStartForFail: find ? find.ShowStartForFail : false,
						ShowStartForFailTH: find ? find.ShowStartForFailTH : false,
						ShowStartForFailPR: find ? find.ShowStartForFailPR : false,
						text: cl.Name
					});
				});


				var cAppList = mx($scope.examConfig.ExamConfigForAppList);
				$scope.examConfig.ExamConfigForAppList = [];
				angular.forEach($scope.ClassList, function (cl) {

					var find = cAppList.firstOrDefault(p1 => p1.ClassId == cl.ClassId);

					$scope.examConfig.ExamConfigForAppList.push({
						ClassId: cl.ClassId,
						MinDues: find ? find.MinDues : 0,
						UptoMonthId: find ? find.UptoMonthId : 0,
						MarkType: find ? find.MarkType : 1,
						GeneralMarkSheet_RptTranId: find ? find.GeneralMarkSheet_RptTranId : null,
						GroupMarkSheet_RptTranId: find ? find.GroupMarkSheet_RptTranId : null,
						AdmitCard_RptTranId: find ? find.AdmitCard_RptTranId : null,
						text: cl.Name
					});
				});


				var cGPAList = mx($scope.examConfig.ClassWiseGPAList);
				$scope.examConfig.ClassWiseGPAList = [];
				angular.forEach($scope.ClassList, function (cl) {

					var find = cGPAList.firstOrDefault(p1 => p1.ClassId == cl.ClassId);

					$scope.examConfig.ClassWiseGPAList.push({
						ClassId: cl.ClassId,
						GPAAs: find ? find.GPAAs : 1,
						text: cl.Name
					});
				});

				var cGPList = mx($scope.examConfig.ClassWiseGPList);
				$scope.examConfig.ClassWiseGPList = [];
				angular.forEach($scope.ClassList, function (cl) {

					var find = cGPList.firstOrDefault(p1 => p1.ClassId == cl.ClassId);

					$scope.examConfig.ClassWiseGPList.push({
						ClassId: cl.ClassId,
						GPAs: find ? find.GPAs : 1,
						text: cl.Name
					});
				});

				if (!$scope.examConfig.ExamAttendanceColl) {
					$scope.examConfig.ExamAttendanceColl = [];
				}

				if ($scope.examConfig.ExamAttendanceColl.length == 0)
					$scope.examConfig.ExamAttendanceColl.push({});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	$scope.GetClassWiseStudentMV = function () {

		$scope.newBlock.StudentList = [];
		if ($scope.newBlock.SelectedClass && $scope.newBlock.ExamTypeId) {
			var para = {
				ClassId: $scope.newBlock.SelectedClass.ClassId,
				SectionId: $scope.newBlock.SelectedClass.SectionId,
				ExamTypeId: $scope.newBlock.ExamTypeId
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetExamWiseBlockedMarkSheetById",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {
				if (res1.data.IsSuccess && res1.data.Data) {
					$scope.newBlock.StudentList = res1.data.Data;

				} else {
					Swal.fire(res1.data.ResponseMSG);
				}
			});
		}

	};
	$scope.ClearExamWiseBlocked = function () {

		$timeout(function () {
			$scope.newBlock = {
				SelectedClass: null,
				ExamTypeId: null,
				Mode: 'Save'
			};
		});
	};
	$scope.SaveUpdateExamWiseBlocked = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();


		var tmpData = [];

		angular.forEach($scope.newBlock.StudentList, function (st) {

			if (st.IsBlocked == true) {
				tmpData.push(st);
			}

		});

		if (tmpData.length == 0) {

			var f = $scope.newBlock.StudentList[0];

			tmpData.push({
				ClassId: f.ClassId,
				SectionId: f.SectionId,
				ExamTypeId: f.ExamTypeId,
				StudenId: 0,
				Message: '',
				IsBlocked: false
			});
		}

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamWiseBlockedMarkSheet",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: tmpData }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}



	$scope.GetClassWiseStudentGMV = function () {

		$scope.newBlockGroup.StudentList = [];
		if ($scope.newBlockGroup.SelectedClass && $scope.newBlockGroup.ExamTypeId) {
			var para = {
				ClassId: $scope.newBlockGroup.SelectedClass.ClassId,
				SectionId: $scope.newBlockGroup.SelectedClass.SectionId,
				ExamTypeId: $scope.newBlockGroup.ExamTypeId
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetExamGroupWiseBlockedMarkSheetById",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {
				if (res1.data.IsSuccess && res1.data.Data) {
					$scope.newBlockGroup.StudentList = res1.data.Data;

				} else {
					Swal.fire(res1.data.ResponseMSG);
				}
			});
		}

	};
	$scope.ClearExamGroupWiseBlocked = function () {

		$timeout(function () {
			$scope.newBlockGroup = {
				SelectedClass: null,
				ExamTypeId: null,
				Mode: 'Save'
			};
		});
	};
	$scope.SaveUpdateExamGroupWiseBlocked = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();


		var tmpData = [];

		angular.forEach($scope.newBlockGroup.StudentList, function (st) {

			if (st.IsBlocked == true) {
				tmpData.push(st);
			}

		});

		if (tmpData.length == 0) {

			var f = $scope.newBlockGroup.StudentList[0];

			tmpData.push({
				ClassId: f.ClassId,
				SectionId: f.SectionId,
				ExamTypeId: f.ExamTypeId,
				StudenId: 0,
				Message: '',
				IsBlocked: false
			});
		}

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamGroupWiseBlockedMarkSheet",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: tmpData }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.SaveUpdateSeatPlan = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveSeatPlanConfiguration",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.SeatPlanConfig }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetSeatPlanExamConfig = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.SeatPlanConfig = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetSeatPlanConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SeatPlanConfig = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.ChangeSeatPlanExamType = function (eid) {
		angular.forEach($scope.SeatPlanConfig, function (sp) {
			sp.ExamTypeId = eid;
		});
	}

	$scope.GetExamWiseTemplate = function (forType) {

		if (forType == 1)
			$scope.newTemplate.ExamTypeGroupId = null;
		else
			$scope.newTemplate.ExamTypeId = null;

		var para = {
			ExamTypeId: $scope.newTemplate.ExamTypeId,
			ExamTypeGroupId: $scope.newTemplate.ExamTypeGroupId
		}
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamTypeWiseTemplate",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamTypeWiseTemplateColl = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.ChangeExamWiseTemplate = function (ba, ind) {

		var forType = 1;

		if ($scope.newTemplate.ExamTypeId > 0)
			forType = 1;
		else
			forType = 2;

		var notRef = false;
		for (var i = ind + 1; i < $scope.ExamTypeWiseTemplateColl.length; i++) {

			if (!$scope.ExamTypeWiseTemplateColl[i].ReportTemplateId || $scope.ExamTypeWiseTemplateColl[i].ReportTemplateId == 0) {
				$scope.ExamTypeWiseTemplateColl[i].ReportTemplateId = ba.ReportTemplateId;
				notRef = true;
			}
		}

		if (notRef == false && ind == 0) {
			Swal.fire({
				title: 'Do you want to change report template of below classed ?',
				showCancelButton: true,
				confirmButtonText: 'yes',
			}).then((result) => {
				/* Read more about isConfirmed, isDenied below */
				if (result.isConfirmed) {

					$timeout(function () {
						for (var i = ind + 1; i < $scope.ExamTypeWiseTemplateColl.length; i++) {
							$scope.ExamTypeWiseTemplateColl[i].ReportTemplateId = ba.ReportTemplateId;
						}
					});

				}
			});
		}
	}


	$scope.CallSaveExamWiseTemplate = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamWiseTemplate",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.ExamTypeWiseTemplateColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}



	$scope.ChangeExamWiseAdmitTemplate = function (ba, ind) {

		var forType = 1;

		if ($scope.newTemplate.AdmitCardTemplateId > 0)
			forType = 1;
		else
			forType = 2;

		var notRef = false;
		for (var i = ind + 1; i < $scope.ExamTypeWiseTemplateColl.length; i++) {

			if (!$scope.ExamTypeWiseTemplateColl[i].AdmitCardTemplateId || $scope.ExamTypeWiseTemplateColl[i].AdmitCardTemplateId == 0) {
				$scope.ExamTypeWiseTemplateColl[i].AdmitCardTemplateId = ba.AdmitCardTemplateId;
				notRef = true;
			}
		}

		if (notRef == false && ind == 0) {
			Swal.fire({
				title: 'Do you want to change report template of below classed ?',
				showCancelButton: true,
				confirmButtonText: 'yes',
			}).then((result) => {
				/* Read more about isConfirmed, isDenied below */
				if (result.isConfirmed) {

					$timeout(function () {
						for (var i = ind + 1; i < $scope.ExamTypeWiseTemplateColl.length; i++) {
							$scope.ExamTypeWiseTemplateColl[i].AdmitCardTemplateId = ba.AdmitCardTemplateId;
						}
					});

				}
			});
		}
	}

	//DONE: DelExamTypeWiseTemplate
	$scope.DelExamTypeWiseTemplate = function () {
		if (
			(!$scope.newTemplate.ExamTypeId || $scope.newTemplate.ExamTypeId === 0) &&
			(!$scope.newTemplate.ExamTypeGroupId || $scope.newTemplate.ExamTypeGroupId === 0)
		) {
			Swal.fire({
				icon: 'warning',
				title: 'Selection Required',
				text: 'Please select either Exam Type or Exam Type Group before deleting.',
			});
			return;
		}
		Swal.fire({
			title: 'Do you want to delete data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ExamTypeId: $scope.newTemplate.ExamTypeId,
					ExamTypeGroupId: $scope.newTemplate.ExamTypeGroupId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelExamTypeWiseTemplate",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
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

});