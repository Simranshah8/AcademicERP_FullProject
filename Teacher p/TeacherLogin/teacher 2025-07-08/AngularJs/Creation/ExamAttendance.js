String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};

$(document).ready(function () {

	$(document).on('keyup', '.serial', function (e) {
		if (e.which == 13) {
			var checkBoxChecked = true;// $('#chkColumnFocus').prop('checked');
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


});


app.controller('ExamAttendanceController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Exam Attendance';

	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa
	}

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

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

		// Calling ExamTypeList
		$scope.ExamTypeColl = [];
		$http.get(base_url + "OnlineExam/Creation/GetExamTypeList")
			.then(function (data) {
				$scope.ExamTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});

		$scope.currentPages = {
			ExamAttendance: 1,
			BulkAttendance: 1,

		};

		$scope.searchData = {
			ExamAttendance: '',
			BulkAttendance: '',

		};

		$scope.perPage = {
			ExamAttendance: GlobalServices.getPerPageRow(),
			BulkAttendance: GlobalServices.getPerPageRow(),


		};

		$scope.newExamAttendance = {
			ExamAttendanceId: null,			
			Mode: 'Save'
		};

		$scope.newBulkAttendance = {
			BulkAttendanceId: null,
			Mode: 'Save'
		};





		//$scope.GetAllAddHomeworkList();
		//$scope.GetAllExamAttendanceList();



	}

	

	$scope.ClearAddHomework = function () {
		$scope.newAddHomework = {
			ExamAttendanceId: null,
			Mode: 'Save'
		};
	}

	$scope.ClearHomeworkList = function () {
		$scope.newHomeworkList = {
			BulkAttendanceId: null,
			Mode: 'Save'
		};
	}



	//*************************Exam Attendance *********************************

	$scope.IsValidExamAttendance = function () {
		if ($scope.newExamAttendance.Lesson.isEmpty()) {
			Swal.fire('Please ! Enter Lesson');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateExamAttendance = function () {
		if ($scope.IsValidExamAttendance() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamAttendance.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamAttendance();
					}
				});
			} else
				$scope.CallSaveUpdateExamAttendance();

		}
	};

	$scope.CallSaveUpdateExamAttendance = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/SaveExamAttendance",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamAttendance }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExamAttendance();
				$scope.GetAllExamAttendanceList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllExamAttendanceList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamAttendanceList = [];

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetAllExamAttendanceList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamAttendanceList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExamAttendanceById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ExamAttendanceId: refData.ExamAttendanceId
		};

		$http({
			method: 'POST',
			url: base_url + "Homework/Transaction/GetExamAttendanceById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExamAttendance = res.data.Data;
				$scope.newExamAttendance.Mode = 'Modify';

				document.getElementById('ExamAttendance-content').style.display = "none";
				document.getElementById('ExamAttendance-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExamAttendanceById = function (refData) {

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
					ExamAttendanceId: refData.ExamAttendanceId
				};

				$http({
					method: 'POST',
					url: base_url + "Homework/Transaction/DelExamAttendance",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExamAttendanceList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//************************* Bulk Attendance *********************************

	$scope.GetStudentForSubjectBA = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.newBulkAttendance.StudentColl = [];
		if ($scope.newBulkAttendance.SelectClassSection && $scope.newBulkAttendance.ExamTypeId) {
			var para = {
				classId: $scope.newBulkAttendance.SelectClassSection.ClassId,
				sectionId: $scope.newBulkAttendance.SelectClassSection.SectionId,				
				examTypeId: $scope.newBulkAttendance.ExamTypeId
			};
			$http({
				method: 'POST',
				url: base_url + "Examination/Creation/GetStudentForExamBA",
				dataType: "json",
				data: para
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					$scope.newBulkAttendance.StudentColl = res.data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}



	}
	$scope.SaveExamBA = function () {
		var meColl = [];
		var examTypeId = $scope.newBulkAttendance.ExamTypeId;		
		var classId = $scope.newBulkAttendance.SelectClassSection.ClassId;
		var sectionId = $scope.newBulkAttendance.SelectClassSection.SectionId;

		if (sectionId == 0)
			sectionId = null;

		angular.forEach($scope.newBulkAttendance.StudentColl, function (st) {

			var mar = {
				StudentId: st.StudentId,				
				ClassId: classId,
				SectionId: sectionId,			
				ExamTypeId: examTypeId,
				WorkingDays: st.WorkingDays,
				PresentDays: st.PresentDays,
				AbsentDays: st.AbsentDays,
				DateFrom: null,
				DateTo: null,
				Remarks:st.Remarks
			};

			meColl.push(mar);
		});

		$http({
			method: 'POST',
			url: base_url + "Examination/Creation/SaveExamBA",
			dataType: "json",
			data: JSON.stringify(meColl)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess)
				$scope.ClearExamBA();

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};
	$scope.ClearExamBA = function () {
		$scope.newBulkAttendance = null;
	}
	$scope.ChangeWorkingDays = function () {

		var wd = $scope.newBulkAttendance.WorkingDays;
		angular.forEach($scope.newBulkAttendance.StudentColl, function (st) {
			st.WorkingDays = wd;
			st.AbsentDays = st.WorkingDays - st.PresentDays;
		});
	};
	$scope.ChangePADays = function (pa, st) {
		if (pa == 1) {
			st.AbsentDays = st.WorkingDays - st.PresentDays;
		} else if (pa == 2)
			st.PresentDays = st.WorkingDays - st.AbsentDays;
	}

	$scope.DelBulkAttendanceById = function (refData) {

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
					BulkAttendanceId: refData.BulkAttendanceId
				};

				$http({
					method: 'POST',
					url: base_url + "Homework/Transaction/DelBulkAttendance",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBulkAttendanceList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});