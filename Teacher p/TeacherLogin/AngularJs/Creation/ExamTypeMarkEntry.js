
$(document).ready(function () {

	$(document).on('keyup', '.serialECAS', function (e) {
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
					var $input = $td.find('.serialECAS');
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

app.controller('ExamTypeMarkEntryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'ExamType MarkEntry';
	var gSrv = GlobalServices;

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			MarksEntry: 1,
			MarksEntryStatus: 1,
		};

		$scope.searchData = {
			MarksEntry: '',
			MarksEntryStatus: '',
		};

		$scope.perPage = {
			MarksEntry: GlobalServices.getPerPageRow(),
			MarksEntryStatus: GlobalServices.getPerPageRow(),
		};

		$scope.newMarksEntry = {
			MarksEntryId: null,
			ClassSecId: null,
			SubjectId: null,
			TeacherId: null,
			TestName: '',
			TestDate_TMP: '',
			FulMarks: '',
			PassMarks: '',
			Mode: 'Save'
		};
		$scope.newMarksEntryStatus = {
			MarksEntryStatusId: null,
			ClassSecId: null,
			DateFrom_TMP: '',
			DateTo_TMP: '',
			Mode: 'Save'
		};

		$scope.ExamTypeList = [];
		gSrv.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.CASTypeList = [];
		gSrv.getCASTypeList().then(function (res) {
			$scope.CASTypeList = res.data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.SubjectList = {};
		gSrv.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.GetAllMarksEntryList();
		//$scope.GetAllMarksEntryStatusList();
	}



	$scope.ClearMarksEntry = function () {
		$scope.newMarksEntry = {
			ClassSecId: null,
			SubjectId: null,
			TeacherId: null,
			TestName: '',
			TestDate: '',
			FulMarks: '',
			PassMarks: '',
			Mode: 'Save'
		};
	}

	$scope.ClearMarksEntryStatus = function () {
		$scope.newMarksEntryStatus = {
			ClassSecId: null,
			DateFrom_TMP: '',
			DateTo_TMP: '',
			Mode: 'Save'
		};
	}

	//************************* MarksEntry *********************************

	$scope.GetClassWiseSubMap = function () {

		$scope.newClassWise.SubjectList = [];
		$scope.newClassWise.MarkSetupDetailsColl = [];
		$scope.newClassWise.StudentColl = [];
		if ($scope.newClassWise.SelectedClass) {
			var para = {
				ClassId: $scope.newClassWise.SelectedClass.ClassId,
				SectionId: $scope.newClassWise.SelectedClass.SectionId 
			};

			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Global/GetAllSubjectList",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					$scope.newClassWise.SubjectList = res.data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.GetStudentListForME = function () {
		if ($scope.newClassWise.SelectedClass && $scope.newClassWise.SubjectId > 0 && $scope.newClassWise.ExamTypeId > 0) {

			$scope.newClassWise.CASTypeColl = [];
			$scope.newClassWise.StudentColl = [];

			var para1 = {
				ClassId: $scope.newClassWise.SelectedClass.ClassId,
				SectionId: $scope.newClassWise.SelectedClass.SectionId,
				SubjectId: $scope.newClassWise.SubjectId,
				ExamTypeId: $scope.newClassWise.ExamTypeId,
				FilterSection: $scope.newClassWise.SelectedClass.FilterSection
			};
			$http({
				method: 'POST',
				url: base_url + "Examination/Creation/GetStudentForClassWiseCASExamME",
				dataSchedule: "json",
				data: JSON.stringify(para1)
			}).then(function (res1) {
				if (res1.data) {
					var stColl = mx(res1.data);

					var casTypeQuery = stColl.groupBy(t => t.CASTypeName);
					var tmpCASTypeColl = [];
					var fullMark = 0;
					angular.forEach(casTypeQuery, function (ct) {
						tmpCASTypeColl.push(ct.key);

						var fst = ct.elements[0];
						fullMark += fst.Mark;
						$scope.newClassWise.CASTypeColl.push({
							CASTypeId: fst.CASTypeId,
							CASTypeName: fst.CASTypeName,
							Mark: fst.Mark
						});
					});

					$scope.newClassWise.TotalMark = fullMark;
					var query = stColl.groupBy(t => t.StudentId);

					var sno = 1;
					angular.forEach(query, function (q) {

						var fst = q.elements[0];
						var eQuery = mx(q.elements);
						var beData = {
							SNo: sno,
							UserId: fst.UserId,
							StudentId: fst.StudentId,
							RegdNo: fst.RegdNo,
							Name: fst.Name,
							RollNo: fst.RollNo,
							TotalMark: eQuery.sum(p1 => p1.ObtainMark),
							Mark: eQuery.sum(p1 => p1.Mark),
							Remarks: fst.Remarks,
							DataColl: []
						};

						angular.forEach(tmpCASTypeColl, function (ct) {

							var find = eQuery.firstOrDefault(p1 => p1.CASTypeName == ct);

							beData.DataColl.push({
								CASTypeName: ct,
								ObtainMark: find.ObtainMark,
								Mark: find.Mark,
								Under: find.Under,
								Scheme: find.Scheme,
								ExamTypeId: para1.ExamTypeId,
								StudentId: fst.StudentId,
								CASTypeId: find.CASTypeId,
								SubjectId: para1.SubjectId,
								Remarks: find.Remarks
							});
						});
						$scope.newClassWise.StudentColl.push(beData);
						sno++;
					})

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.ChangeMarkEntry = function (st) {

		st.TotalMark = 0;
		angular.forEach(st.DataColl, function (dc) {

			if (IsNumber(dc.ObtainMark)) {
				if (dc.ObtainMark > dc.Mark) {
					alert("Please ! Enter Mark less than equal " + dc.Mark);
					dc.ObtainMark = 0;
				}

				st.TotalMark += parseFloat(dc.ObtainMark);
			}
			else {
				if (dc.ObtainMark == "a" || dc.ObtainMark == "ab" || dc.ObtainMark == "A" || dc.ObtainMark == "AB" || dc.ObtainMark == "Ab") {
					dc.IsAbsent = true;

				} else if (len(dc.ObtainMark) > 0) {
					alert('Invalid obtain mark');
					dc.ObtainMark = 0;
				}
			}
		});

	};

	$scope.IsValidMarksEntry = function () {

		var isValid = true;

		angular.forEach($scope.newClassWise.StudentColl, function (st) {
			angular.forEach(st.DataColl, function (dc) {
				if (IsNumber(dc.ObtainMark)) {
					if (dc.ObtainMark > dc.Mark) {
						Swal.fire("Please ! Enter Obtainmark less then equal to " + st.Mark);
						isValid = false;
					}
				}
			});
		});


		return true;
	}
	$scope.SaveUpdateMarksEntry = function () {
		if ($scope.IsValidMarksEntry() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newMarksEntry.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateMarksEntry();
					}
				});
			} else
				$scope.CallSaveUpdateMarksEntry();
		}
	};

	$scope.CallSaveUpdateMarksEntry = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var dataColl = [];
		angular.forEach($scope.newClassWise.StudentColl, function (st) {
			angular.forEach(st.DataColl, function (dc) {
				dc.Remarks = st.Remarks;

				if (dc.ObtainMark != undefined) {

					var isAbsent = dc.ObtainMark.toString().toLowerCase();
					if (isAbsent == 'ab') {
						dc.IsAbsent = true;
						dc.ObtainMark = 0;
					} else {

						dc.IsAbsent = false;

						if (dc.ObtainMark > dc.Mark) {
							Swal.fire("Please ! Enter Obtainmark less then equal to " + dc.Mark);
							return false;
						}
					}

				}

				dataColl.push(dc);
			});
		});


		$http({
			method: 'POST',
			url: base_url + "Examination/Creation/SaveCASExamWiseME",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: dataColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearMarksEntry();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllMarksEntryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.MarksEntryList = [];
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllMarksEntryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.MarksEntryList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	//************************* MarksEntryStatus *********************************

	$scope.SaveUpdateMarksEntryStatus = function () {
		if ($scope.IsValidMarksEntryStatus() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newMarksEntryStatus.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateMarksEntryStatus();
					}
				});
			} else
				$scope.CallSaveUpdateMarksEntryStatus();
		}
	};

	$scope.CallSaveUpdateMarksEntryStatus = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveMarksEntryStatus",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newMarksEntryStatus }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearMarksEntryStatus();
				$scope.GetAllMarksEntryStatusList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllMarksEntryStatusList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.MarksEntryStatusList = [];
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllMarksEntryStatusList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.MarksEntryStatusList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}



	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});