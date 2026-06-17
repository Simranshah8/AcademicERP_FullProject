
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

		$scope.currentPages = {
			MarksEntry: 1,
		};

		$scope.searchData = {
			MarksEntry: '',
		};

		$scope.perPage = {
			MarksEntry: GlobalServices.getPerPageRow(),
		};

		$scope.beData = {
			MarksEntryId: null,
			ClassSecId: null,
			SubjectId: null,
			TeacherId: null,
			TestName: '',
			TestDate_TMP: '',
			FulMarks: '',
			PassMarks: '',
			TotObjMark: null,
			MarkSetupDetailsColl: [],
			StudentColl: [],
			Mode: 'Save'
		};
		$scope.newFilter = {
			ClassId: null,
			SubjectId: null,
			ExamTypeId: null
        }

		$scope.ClassList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ExamTypeList = [];
		GlobalServices.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.ClearMarksEntry = function () {
		$scope.beData = {
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




	$scope.GetStudentList = function () {
		$scope.StudentList = [];
		if (!$scope.newFilter.SelectedClass) {
			Swal.fire("Please ! Select Class");
			return;
		}
		else {
			var para = {
				ClassId: $scope.newFilter.SelectedClass.ClassId,
				SectionIdColl: ($scope.newFilter.SelectedClass.SectionId ? $scope.newFilter.SelectedClass.SectionId : 0),
			};
			$scope.loadingstatus = "running";
			showPleaseWait();
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetStudentList",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.StudentList = res.data.Data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	};
	$scope.GetObjectiveList = function () {
		$scope.ObjectiveList = [];
		if (!$scope.newFilter.SelectedClass) {
			Swal.fire("Please ! Select Class");
			return;
		} if (!$scope.newFilter.SubjectId) {
			Swal.fire("Please ! Select Subject");
			return;
		} if (!$scope.newFilter.ExamTypeId) {
			Swal.fire("Please ! Select ExamType");
			return;
		} else {
			var para = {
				ClassId: $scope.newFilter.SelectedClass.ClassId,
				SectionIdColl: ($scope.newFilter.SelectedClass.SectionId ? $scope.newFilter.SelectedClass.SectionId : 0),
				SubjectId: $scope.newFilter.SubjectId,
				ExamTypeId: $scope.newFilter.ExamTypeId
			};
			$scope.loadingstatus = "running";
			showPleaseWait();
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetObjectiveList",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var total = 0;
					angular.forEach(res.data.Data, function (objective) {
						total = total + objective.Marks;
					});
					res.data.Data.TotObjMark = total;
					$scope.ObjectiveList = res.data.Data;
					angular.forEach($scope.StudentList, function (st) {
						st.Marks = [];
						st.TotObjMark = 0;
						angular.forEach($scope.ObjectiveList, function (ct) {
							st.Marks.push({
								ObjectiveId: ct.ObjectiveId,
								SNo: ct.SNo,
								Mark: null,
							});
						});
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	};

	$scope.ChangeMarkEntry = function (st, ct, idx) {
		var markObj = st.Marks[idx];
		var val = markObj.Mark;

		var num = parseFloat(val);
		if (!isNaN(num) && num > ct.Marks) {
			Swal.fire("Mark cannot exceed " + ct.Marks);
			markObj.Mark = null;
		}

		// Recalculate student total
		var total = 0;
		angular.forEach(st.Marks, function (m) {
			var n = parseFloat(m.Mark);
			if (!isNaN(n)) total += n;
		});
		st.TotObjMark = total;
	};


	$scope.GetClassSummaryClassWiseSubject = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			ClassId: $scope.newFilter.SelectedClass.ClassId
		};
		$scope.SubjectList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetClassSummarySubjectClassWise",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFilter.SubjectList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}


	//$scope.SaveUpdateMarksEntry = function () {
	//	var dataColl = [];

	//	angular.forEach($scope.StudentList, function (st) {
	//		dataColl.push({
	//			StudentId: st.StudentId,
	//			Remarks: st.Remarks,
	//			Marks: st.Marks
	//		});
	//	});

	//	showPleaseWait();
	//	$http({
	//		method: 'POST',
	//		url: base_url + "",
	//		headers: { 'Content-Type': undefined },
	//		transformRequest: function (data) {
	//			var formData = new FormData();
	//			formData.append("jsonData", angular.toJson(data.jsonData));
	//			return formData;
	//		},
	//		data: { jsonData: dataColl }
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		Swal.fire(res.data.ResponseMSG);
	//	}, function () {
	//		hidePleaseWait();
	//	});
	//};

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});

