
$(document).ready(function () {

	$(document).on('keyup', '.serial', function (e) {
		if (e.which == 13) {
			var checkBoxChecked = $('#chkColumnFocus').prop('checked');
			if (checkBoxChecked == true) {
				var $this = $(this);
				var $td = $this.closest('td');
				var $row = $td.closest('tr');
				var $rows = $row.parent();
				var column = $td.index();


				while ($td.length) {

					$row = $row.next('tr');

					if ($row.length == 0) {

						$row = $rows.children().first();
						// $row = $rows.children().get(2);

						column++;
					}

					$td = $row.children().eq(column);
					var $input = $td.find('.serial');
					if ($input.length) {
						$input.focus();
						break;
					}
				}
			} else {

				var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
				if (key == 13) {
					e.preventDefault();
					var inputs = $(this).closest('form').find(':input:visible');
					inputs.eq(inputs.index(this) + 1).focus();
				}
			}

		}
	});

	$(document).on('keyup', '.serialSub', function (e) {
		if (e.which == 13) {
			var checkBoxChecked = $('#chkColumnFocusSub').prop('checked');
			if (checkBoxChecked == true) {
				var $this = $(this);
				var $td = $this.closest('td');
				var $row = $td.closest('tr');
				var $rows = $row.parent();
				var column = $td.index();


				while ($td.length) {

					$row = $row.next('tr');

					if ($row.length == 0) {

						$row = $rows.children().first();
						// $row = $rows.children().get(2);

						column++;
					}

					$td = $row.children().eq(column);
					var $input = $td.find('.serialSub');
					if ($input.length) {
						$input.focus();
						break;
					}
				}
			} else {

				var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
				if (key == 13) {
					e.preventDefault();
					var inputs = $(this).closest('form').find(':input:visible');
					inputs.eq(inputs.index(this) + 1).focus();
				}
			}

		}
	});
});

app.controller('ReMarkEntryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'MarkEntry';

	var gSrv = GlobalServices;
	$scope.LoadData = function () {
		$('.select2').select2();


		$scope.ExamTypeList = [];
		gSrv.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ReExamTypeList = [];
		gSrv.getReExamTypeList().then(function (res) {
			$scope.ReExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		gSrv.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();
		$scope.StudentSearchOptions = gSrv.getStudentSearchOptions();

		$scope.currentPages = {
			ClassWise: 1,
			SubjectWise: 1,
			StudentWise: 1,
			MarkSubmittedStatus: 1,
			MarkSubmittedStatusPending: 1
		};

		$scope.searchData = {
			ClassWise: '',
			SubjectWise: '',
			StudentWise: '',
			MarkSubmittedStatus: '',
			MarkSubmittedStatusPending: ''
		};

		$scope.perPage = {
			ClassWise: gSrv.getPerPageRow(),
			SubjectWise: gSrv.getPerPageRow(),
			StudentWise: gSrv.getPerPageRow(),
			MarkSubmittedStatus: gSrv.getPerPageRow(),
			MarkSubmittedStatusPending: gSrv.getPerPageRow()
		};

		$scope.newClassWise = {
			ClassWiseId: null,

			Mode: 'Save'
		};

		$scope.newSubjectWise = {
			SubjectWiseId: null,
			SubjectWiseDetailsColl: [],
			Mode: 'Save'
		};

		$scope.newSubjectWise.SubjectWiseDetailsColl.push({});

		$scope.newStudentWise = {
			StudentWiseId: null,
			StudentWiseDetailsColl: [],
			SelectStudent: $scope.StudentSearchOptions[0].value,
			Mode: 'Save'
		};
		$scope.newStudentWise.StudentWiseDetailsColl.push({});

		$scope.newMarkSubmittedStatus = {
			MarkSubmittedStatusId: null,

			Mode: 'Save'
		};

		$scope.nt = {
			Title: '',
			Description: ''
		};

		//$scope.GetAllClassList();
		//$scope.GetAllSectionList();
		//$scope.GetAllStudentWiseList();
		//$scope.GetAllMarkSubmittedStatusList();

	}

	$scope.GetClassWiseSubMap = function () {

		$scope.newClassWise.SubjectList = [];
		$scope.newClassWise.MarkSetupDetailsColl = [];
		$scope.newClassWise.StudentColl = [];
		if ($scope.newClassWise.SelectedClass) {
			var para = {
				ClassId: $scope.newClassWise.SelectedClass.ClassId,
				SectionIdColl: ($scope.newClassWise.SelectedClass.SectionId ? $scope.newClassWise.SelectedClass.SectionId.toString() : '')
			};

			$scope.loadingstatus = "running";
			showPleaseWait();

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
								$scope.newClassWise.SubjectList.push(subDet);
							}
						});

						if ($scope.newClassWise.ExamTypeId && $scope.newClassWise.ExamTypeId > 0 && $scope.newClassWise.ReExamTypeId && $scope.newClassWise.ReExamTypeId > 0) {

							var para1 = {
								ClassId: para.ClassId,
								SectionId: $scope.newClassWise.SelectedClass.SectionId,
								ExamTypeId: $scope.newClassWise.ExamTypeId,
								ReExamTypeId: $scope.newClassWise.ReExamTypeId
							};
							$http({
								method: 'POST',
								url: base_url + "Exam/Transaction/GetStudentForClassWiseReME",
								dataSchedule: "json",
								data: JSON.stringify(para1)
							}).then(function (res1) {
								if (res1.data.IsSuccess && res1.data.Data) {
									var exMarkEntry = res1.data.Data;
									$scope.newClassWise.StudentColl = exMarkEntry;

									angular.forEach($scope.newClassWise.StudentColl, function (st) {
										st.ObtMarkColl = [];
										var mcQuery = mx(st.MarkColl);
										var obtainMark = 0;
										angular.forEach($scope.newClassWise.SubjectList, function (sl) {
											var fSub = mcQuery.firstOrDefault(p1 => p1.SubjectId == sl.SubjectId);
											var om = {
												StudentId: st.StudentId,
												ExamTypeId: $scope.newClassWise.ExamTypeId,
												ReExamTypeId: $scope.newClassWise.ReExamTypeId,
												SubjectId: (fSub ? fSub.SubjectId : 0),
												PaperType: sl.PaperType,
												FMTH: (fSub ? fSub.FMTH : 0),
												FMPR: (fSub ? fSub.FMPR : 0),
												PMTH: (fSub ? fSub.PMTH : 0),
												PMPR: (fSub ? fSub.PMPR : 0),
												ObtainMarkTH: (fSub ? fSub.ObtainMarkTH : ''),
												ObtainMarkPR: (fSub ? fSub.ObtainMarkPR : ''),
												Remarks: (fSub ? fSub.Remarks : '')
											};
											st.ObtMarkColl.push(om);

											if (!isNaN(parseFloat(om.ObtainMarkTH)))
												obtainMark += parseFloat(om.ObtainMarkTH);

											if (!isNaN(parseFloat(om.ObtainMarkPR)))
												obtainMark += parseFloat(om.ObtainMarkPR);

											if (!isNaN(parseFloat(om.GraceMarkTH)))
												obtainMark += parseFloat(om.GraceMarkTH);

											if (!isNaN(parseFloat(om.GraceMarkPR)))
												obtainMark += parseFloat(om.GraceMarkPR);

										});

										st.TotalObtainMark = obtainMark;
									});

									$scope.$broadcast('refreshFixedColumns');
									//gridViewScroll.enhance();

								} else {
									Swal.fire(res.data.ResponseMSG);
								}

							}, function (reason) {
								Swal.fire('Failed' + reason);
							});
						}
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.sortClassWise = function (keyname) {
		$scope.sortKeyClassWise = keyname;   //set the sortKey to the param passed
		$scope.reverseClassWise = !$scope.reverseClassWise; //if true make it false and vice versa
	}
	$scope.sortSubjectWise = function (keyname) {
		$scope.sortKeySubjectWise = keyname;   //set the sortKey to the param passed
		$scope.reverseSubjectWise = !$scope.reverseSubjectWise; //if true make it false and vice versa
	}
	$scope.sortPending = function (keyname) {
		$scope.sortKeyPending = keyname;   //set the sortKey to the param passed
		$scope.reversePending = !$scope.reversePending; //if true make it false and vice versa
	}
	$scope.sortSubmit = function (keyname) {
		$scope.sortKeySubmit = keyname;   //set the sortKey to the param passed
		$scope.reverseSubmit = !$scope.reverseSubmit; //if true make it false and vice versa
	}

	$scope.ClearClassWise = function () {
		$scope.newClassWise = {
			ClassWiseId: null,

			Mode: 'Save'
		};
	}
	$scope.ClearSubjectWise = function () {
		$scope.newSubjectWise = {
			SubjectWiseId: null,
			SubjectWiseDetailsColl: [],
			Mode: 'Save'
		};

		$scope.newSubjectWise.SubjectWiseDetailsColl.push({});
	}
	$scope.ClearStudentWise = function () {
		$scope.newStudentWise = {
			StudentWiseId: null,
			StudentWiseDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newStudentWise.StudentWiseDetailsColl.push({});
	}
	$scope.ClearMarkSubmittedStatus = function () {
		$scope.newMarkSubmittedStatus = {
			MarkSubmittedStatusId: null,

			Mode: 'Save'
		};
	}

	//************************* ClassWise *********************************
	function isNumeric(n) {
		return !isNaN(parseFloat(n)) && isFinite(n);
	}
	function len(val) {
		return val.length;
	}
	$scope.ChangeMarkEntry = function (beData, type, newBeData) {
		//type  : 1=th  2=pr 3=gth 4=gpr


		if (type == 1) {
			if (beData.PaperType == 1 || beData.PaperType == 3) {
				if (isNumeric(beData.ObtainMarkTH)) {
					beData.IsAbsentTH = false;
					if (beData.ObtainMarkTH > beData.FMTH) {
						alert("Please Enter Obtain Mark less than equal " + beData.FMTH);
						beData.ObtainMarkTH = 0;
					}

					if (beData.ObtainMarkTH < beData.PMTH) {
						beData.IsFailTH = true;
					} else
						beData.IsFailTH = false;

				} else {
					if (beData.ObtainMarkTH == "a" || beData.ObtainMarkTH == "ab" || beData.ObtainMarkTH == "A" || beData.ObtainMarkTH == "AB" || beData.ObtainMarkTH == "Ab") {
						beData.IsAbsentTH = true;

					} else if (len(beData.ObtainMarkTH) > 0) {
						alert('Invalid obtain mark');
						beData.ObtainMarkTH = 0;
					}

				}

			} else if (beData.PaperType == 4) {
				beData.Grade = beData.ObtainMarkTH;
			}
		} else if (type == 2) {
			if (beData.PaperType == 2 || beData.PaperType == 3) {
				if (isNumeric(beData.ObtainMarkPR)) {
					beData.IsAbsentPR = false;
					if (beData.ObtainMarkPR > beData.FMPR) {
						alert("Please Enter Obtain Mark less than equal " + beData.FMPR);
						beData.ObtainMarkPR = 0;
					}

					if (beData.ObtainMarkPR < beData.PMPR) {
						beData.IsFailPR = true;
					} else
						beData.IsFailPR = false;
				} else {
					if (beData.ObtainMarkPR == "a" || beData.ObtainMarkPR == "ab" || beData.ObtainMarkPR == "A" || beData.ObtainMarkPR == "AB" || beData.ObtainMarkPR == "Ab") {
						beData.IsAbsentPR = true;

					} else if (len(beData.ObtainMarkPR) > 0) {
						alert('Invalid obtain mark');
						beData.ObtainMarkPR = 0;
					}
				}

			} else if (beData.PaperType == 4) {
				beData.Grade = beData.ObtainMarkPR;
			}
		}

		var obtainMark = 0;
		angular.forEach(newBeData.ObtMarkColl, function (marks) {
			if (!isNaN(parseFloat(marks.ObtainMarkTH)))
				obtainMark += parseFloat(marks.ObtainMarkTH);

			if (!isNaN(parseFloat(marks.ObtainMarkPR)))
				obtainMark += parseFloat(marks.ObtainMarkPR);

			if (!isNaN(parseFloat(marks.GraceMarkTH)))
				obtainMark += parseFloat(marks.GraceMarkTH);

			if (!isNaN(parseFloat(marks.GraceMarkPR)))
				obtainMark += parseFloat(marks.GraceMarkPR);
		});
		newBeData.TotalObtainMark = obtainMark;

	};
	$scope.IsValidClassWise = function () {
		//if ($scope.newClassWise.Name.isEmpty()) {
		//	Swal.fire('Please ! Enter ClassWise Name');
		//	return false;
		//}



		return true;
	}

	$scope.SaveUpdateClassWise = function () {
		if ($scope.IsValidClassWise() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newClassWise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateClassWise();
					}
				});
			} else
				$scope.CallSaveUpdateClassWise();

		}
	};

	$scope.CallSaveUpdateClassWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var tmpDataColl = [];
		angular.forEach($scope.newClassWise.StudentColl, function (st) {
			angular.forEach(st.ObtMarkColl, function (mc) {
				mc.Remarks = st.Remarks;
				tmpDataColl.push(mc);
			});
		});
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveClassWiseReMarkEntry",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: tmpDataColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true)
				$scope.ClearClassWise();

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

 

	//************************* Subject Wise   *********************************

	$scope.ChangeMarkEntrySW = function (beData, type) {
		//type  : 1=th  2=pr 3=gth 4=gpr

		if (type == 1) {
			if (beData.PaperType == 1 || beData.PaperType == 3) {
				if (isNumeric(beData.ObtainMarkTH)) {
					beData.IsAbsentTH = false;
					if (beData.ObtainMarkTH > beData.FMTH) {
						alert("Please Enter Obtain Mark less than equal " + beData.FMTH);
						beData.ObtainMarkTH = 0;
					}

					if (beData.ObtainMarkTH < beData.PMTH) {
						beData.IsFailTH = true;
					} else
						beData.IsFailTH = false;

				} else {
					if (beData.ObtainMarkTH == "a" || beData.ObtainMarkTH == "ab" || beData.ObtainMarkTH == "A" || beData.ObtainMarkTH == "AB" || beData.ObtainMarkTH == "Ab") {
						beData.IsAbsentTH = true;

					} else if (len(beData.ObtainMarkTH) > 0) {
						alert('Invalid obtain mark');
						beData.ObtainMarkTH = 0;
					}

				}

			} else if (beData.PaperType == 4) {
				beData.Grade = beData.ObtainMarkTH;
			}
		} else if (type == 2) {
			if (beData.PaperType == 2 || beData.PaperType == 3) {
				if (isNumeric(beData.ObtainMarkPR)) {
					beData.IsAbsentPR = false;
					if (beData.ObtainMarkPR > beData.FMPR) {
						alert("Please Enter Obtain Mark less than equal " + beData.FMPR);
						beData.ObtainMarkPR = 0;
					}

					if (beData.ObtainMarkPR < beData.PMPR) {
						beData.IsFailPR = true;
					} else
						beData.IsFailPR = false;
				} else {
					if (beData.ObtainMarkPR == "a" || beData.ObtainMarkPR == "ab" || beData.ObtainMarkPR == "A" || beData.ObtainMarkPR == "AB" || beData.ObtainMarkPR == "Ab") {
						beData.IsAbsentPR = true;

					} else if (len(beData.ObtainMarkPR) > 0) {
						alert('Invalid obtain mark');
						beData.ObtainMarkPR = 0;
					}
				}

			} else if (beData.PaperType == 4) {
				beData.Grade = beData.ObtainMarkPR;
			}
		}


	};

	$scope.IsValidSubjectWise = function () {
		//if ($scope.newSubjectWise.ClassId.isEmpty()) {
		//	Swal.fire('Please ! Enter Class');
		//	return false;
		//}

		return true;
	}

	$scope.GetClassWiseSubMapSME = function () {

		$scope.newSubjectWise.SubjectList = [];
		$scope.newSubjectWise.StudentColl = [];
		if ($scope.newSubjectWise.SelectedClass) {
			var para = {
				ClassId: $scope.newSubjectWise.SelectedClass.ClassId,
				SectionIdColl: ($scope.newSubjectWise.SelectedClass.SectionId ? $scope.newSubjectWise.SelectedClass.SectionId.toString() : '')
			};

			$scope.loadingstatus = "running";
			showPleaseWait();

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
								$scope.newSubjectWise.SubjectList.push(subDet);
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

	$scope.GetMarkEntrySubjectWise = function () {

		$scope.newSubjectWise.StudentColl = [];
		if ($scope.newSubjectWise.SelectedClass && $scope.newSubjectWise.ExamTypeId && $scope.newSubjectWise.SubjectId &&  $scope.newSubjectWise.ReExamTypeId) {
			var para = {
				ClassId: $scope.newSubjectWise.SelectedClass.ClassId,
				SectionId: ($scope.newSubjectWise.SelectedClass.SectionId ? $scope.newSubjectWise.SelectedClass.SectionId : ''),
				ExamTypeId: $scope.newSubjectWise.ExamTypeId,
				ReExamTypeId: $scope.newSubjectWise.ReExamTypeId,
				SubjectId: $scope.newSubjectWise.SubjectId
			};

			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetStudentForSubjectReME",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {

				$scope.loadingstatus = "stop";
				hidePleaseWait();

				if (res1.data.IsSuccess && res1.data.Data) {
					var exMarkEntry = res1.data.Data;
					$scope.newSubjectWise.StudentColl = exMarkEntry;

					$scope.$broadcast('refreshFixedColumns');
					//gridViewScroll.enhance();

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.SaveUpdateSubjectWise = function () {
		if ($scope.IsValidSubjectWise() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSubjectWise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSubjectWise();
					}
				});
			} else
				$scope.CallSaveUpdateSubjectWise();

		}
	};

	$scope.CallSaveUpdateSubjectWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			ClassId: $scope.newSubjectWise.SelectedClass.ClassId,
			SectionId: ($scope.newSubjectWise.SelectedClass.SectionId ? $scope.newSubjectWise.SelectedClass.SectionId : ''),
			ExamTypeId: $scope.newSubjectWise.ExamTypeId,
			ReExamTypeId: $scope.newSubjectWise.ReExamTypeId,
			SubjectId: $scope.newSubjectWise.SubjectId
		};

		var tmpDataColl = [];
		angular.forEach($scope.newSubjectWise.StudentColl, function (st) {
			var ms = {
				StudentId: st.StudentId,
				ExamTypeId: para.ExamTypeId,
				ReExamTypeId: para.ReExamTypeId,
				SubjectId: para.SubjectId,
				PaperType: st.PaperType,
				ObtainMarkTH: st.ObtainMarkTH,
				ObtainMarkPR: st.ObtainMarkPR
			};
			tmpDataColl.push(ms);
		});
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveClassWiseReMarkEntry",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: tmpDataColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	 

	//************************* Student Wise *********************************


	$scope.GetStudentListForME = function () {

		$scope.newStudentWise.StudentList = [];
		var para = {
			ClassId: $scope.newStudentWise.SelectedClass.ClassId,
			SectionId: $scope.newStudentWise.SelectedClass.SectionId,
			ExamTypeId: $scope.newStudentWise.ExamTypeId,
			ReExamTypeId: $scope.newStudentWise.ReExamTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetStudentForTran",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.newStudentWise.StudentList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.CalculateStudentTOM = function (beData, type) {

		if (beData && type) {
			if (type == 1) {
				if (beData.PaperType == 1 || beData.PaperType == 3) {
					if (isNumeric(beData.ObtainMarkTH)) {
						beData.IsAbsentTH = false;
						if (beData.ObtainMarkTH > beData.FMTH) {
							alert("Please Enter Obtain Mark less than equal " + beData.FMTH);
							beData.ObtainMarkTH = 0;
						}

						if (beData.ObtainMarkTH < beData.PMTH) {
							beData.IsFailTH = true;
						} else
							beData.IsFailTH = false;

					} else {
						if (beData.ObtainMarkTH == "a" || beData.ObtainMarkTH == "ab" || beData.ObtainMarkTH == "A" || beData.ObtainMarkTH == "AB" || beData.ObtainMarkTH == "Ab") {
							beData.IsAbsentTH = true;

						} else if (len(beData.ObtainMarkTH) > 0) {
							alert('Invalid obtain mark');
							beData.ObtainMarkTH = 0;
						}

					}

				} else if (beData.PaperType == 4) {
					beData.Grade = beData.ObtainMarkTH;
				}
			} else if (type == 2) {
				if (beData.PaperType == 2 || beData.PaperType == 3) {
					if (isNumeric(beData.ObtainMarkPR)) {
						beData.IsAbsentPR = false;
						if (beData.ObtainMarkPR > beData.FMPR) {
							alert("Please Enter Obtain Mark less than equal " + beData.FMPR);
							beData.ObtainMarkPR = 0;
						}

						if (beData.ObtainMarkPR < beData.PMPR) {
							beData.IsFailPR = true;
						} else
							beData.IsFailPR = false;
					} else {
						if (beData.ObtainMarkPR == "a" || beData.ObtainMarkPR == "ab" || beData.ObtainMarkPR == "A" || beData.ObtainMarkPR == "AB" || beData.ObtainMarkPR == "Ab") {
							beData.IsAbsentPR = true;

						} else if (len(beData.ObtainMarkPR) > 0) {
							alert('Invalid obtain mark');
							beData.ObtainMarkPR = 0;
						}
					}

				} else if (beData.PaperType == 4) {
					beData.Grade = beData.ObtainMarkPR;
				}
			}
		}


		var obtainMark = 0;
		angular.forEach($scope.newStudentWise.Student.MarkColl, function (marks) {
			if (!isNaN(parseFloat(marks.ObtainMarkTH)))
				obtainMark += parseFloat(marks.ObtainMarkTH);

			if (!isNaN(parseFloat(marks.ObtainMarkPR)))
				obtainMark += parseFloat(marks.ObtainMarkPR);

			if (!isNaN(parseFloat(marks.GraceMarkTH)))
				obtainMark += parseFloat(marks.GraceMarkTH);

			if (!isNaN(parseFloat(marks.GraceMarkPR)))
				obtainMark += parseFloat(marks.GraceMarkPR);
		});
		$scope.newStudentWise.Student.ObtainPer = (obtainMark / ($scope.newStudentWise.Student.FMTH + $scope.newStudentWise.Student.FMPR)) * 100;
		$scope.newStudentWise.Student.TotalObtainMark = obtainMark;
	}

	$scope.GetSubjectForSWME = function () {
		if ($scope.newStudentWise.StudentId && $scope.newStudentWise.ExamTypeId && $scope.newStudentWise.ExamTypeId > 0 &&  $scope.newStudentWise.ReExamTypeId && $scope.newStudentWise.ReExamTypeId > 0) {
			var para1 = {
				StudentId: $scope.newStudentWise.StudentId,
				ExamTypeId: $scope.newStudentWise.ExamTypeId,
				ReExamTypeId: $scope.newStudentWise.ReExamTypeId
			};
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetStudentForStudentWiseReME",
				dataSchedule: "json",
				data: JSON.stringify(para1)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					var exMarkEntry = res.data.Data;
					if (exMarkEntry.length > 0) {
						$timeout(function () {
							$scope.newStudentWise.Student = exMarkEntry[0];
							$scope.newStudentWise.Student.StudentId = para1.StudentId;
							$scope.newStudentWise.Student.ExamTypeId = para1.ExamTypeId;
							$scope.newStudentWise.Student.ReExamTypeId = para1.ReExamTypeId;
							$scope.CalculateStudentTOM(null, null);

						});
						$scope.$broadcast('refreshFixedColumns');
					}

					//gridViewScroll.enhance();

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}
	$scope.IsValidStudentWise = function () {

		return true;
	}

	$scope.SaveUpdateStudentWise = function () {
		if ($scope.IsValidStudentWise() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudentWise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudentWise();
					}
				});
			} else
				$scope.CallSaveUpdateStudentWise();

		}
	};

	$scope.CallSaveUpdateStudentWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			ClassId: $scope.newStudentWise.SelectedClass.ClassId,
			SectionId: ($scope.newStudentWise.SelectedClass.SectionId ? $scope.newStudentWise.SelectedClass.SectionId : ''),
			ExamTypeId: $scope.newStudentWise.ExamTypeId,
			ReExamTypeId: $scope.newStudentWise.ReExamTypeId,
			Student: $scope.newStudentWise.Student
		};

		var tmpDataColl = [];
		angular.forEach($scope.newStudentWise.Student.MarkColl, function (st) {
			var ms = {
				StudentId: para.Student.StudentId,
				ExamTypeId: para.Student.ExamTypeId,
				ReExamTypeId: para.Student.ReExamTypeId,
				SubjectId: st.SubjectId,
				PaperType: st.PaperType,
				ObtainMarkTH: st.ObtainMarkTH,
				ObtainMarkPR: st.ObtainMarkPR
			};
			tmpDataColl.push(ms);
		});
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveClassWiseMarkEntry",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: tmpDataColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	$scope.GetAllStudentWiseList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentWiseList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllStudentWiseList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentWiseList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetStudentWiseById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			StudentWiseId: refData.StudentWiseId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetStudentWiseById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newStudentWise = res.data.Data;
				$scope.newStudentWise.Mode = 'Modify';

				//document.getElementById('batch-section').style.display = "none";
				//document.getElementById('batch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelStudentWiseById = function (refData) {

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
					StudentWiseId: refData.StudentWiseId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelStudentWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStudentWiseList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Mark Submitted Status  *********************************

	$scope.GetAllMarkSubmittedStatusList = function () {

		if ($scope.newMarkSubmittedStatus && $scope.newMarkSubmittedStatus.ExamTypeId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			$scope.PendingMarkList = [];
			$scope.SubmitMarkList = [];

			var para = {
				ExamTypeId: $scope.newMarkSubmittedStatus.ExamTypeId
			};
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetMarkSubmit",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					angular.forEach(res.data.Data, function (d) {
						if (d.IsPending == true)
							$scope.PendingMarkList.push(d);
						else
							$scope.SubmitMarkList.push(d);
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.SendNotificationToTeacher = function () {

		if ($scope.nt.Title && $scope.nt.Description) {
			if ($scope.nt.Title.length > 0 && $scope.nt.Description.length > 0) {
				var tmpDataColl = [];
				angular.forEach($scope.PendingMarkList, function (d) {

					if (d.UserId > 0) {
						var newSMS = {
							EntityId: entityMarkEntry,
							StudentId: 0,
							UserId: d.UserId,
							Title: $scope.nt.Title,
							Message: $scope.nt.Description,
							ContactNo: d.TeacherContactNo,
							StudentName: d.SubjectTeacherName
						};
						tmpDataColl.push(newSMS);
					}
				});

				$http({
					method: 'POST',
					url: base_url + "Global/SendNotificationToStudent",
					dataType: "json",
					data: JSON.stringify(tmpDataColl)
				}).then(function (sRes) {
					Swal.fire(sRes.data.ResponseMSG);
				});

			}
		}

	};

});