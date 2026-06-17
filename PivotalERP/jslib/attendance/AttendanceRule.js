app.controller('AttendanceRuleController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Attendance Rule';	

	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();
		$scope.currentPages = {
			AttendanceRule: 1,
		};

		$scope.searchData = {
			AttendanceRule: '',
		};

		$scope.perPage = {
			AttendanceRule: GlobalServices.getPerPageRow(),
		};

		$scope.newAttendanceRule = {
			AttendanceRuleId: null,			
			PermittedLateArrival: '',
			PermittedEarlyDeparture: '',
			MarkasHalfDayifworkinghourLessThan: '',
			MarkasAbsentifworkinghourLessThan: '',
			MarkasAbsentifworkinghourLessThan: '',
			LateArrival: '',
			EarlyDeparture: '',
			NoOfLateInAMonth: '',
			IgnoreOvertimeLessthan: '',			
			Mode: 'Save'
		};
		//$scope.GetAllAttendanceRuleList();
	}

	$scope.ClearAttendanceRule = function () {
		$scope.newAttendanceRule = {			
			PermittedLateArrival: '',
			PermittedEarlyDeparture: '',
			MarkasHalfDayifworkinghourLessThan: '',
			MarkasAbsentifworkinghourLessThan: '',
			MarkasAbsentifworkinghourLessThan: '',
			LateArrival: '',
			EarlyDeparture: '',
			NoOfLateInAMonth: '',
			IgnoreOvertimeLessthan: '',
			Mode: 'Save'
		};
	}
	//************************* AttendanceRule *********************************
	$scope.IsValidAttendanceRule = function () {
		if ($scope.newAttendanceRule.PermittedLateArrival.isEmpty()) {
			Swal.fire('Please ! Enter Permitted Late Arrival');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateAttendanceRule = function () {
		if ($scope.IsValidAttendanceRule() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAttendanceRule.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAttendanceRule();
					}
				});
			} else
				$scope.CallSaveUpdateAttendanceRule();
		}
	};

	$scope.CallSaveUpdateAttendanceRule = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/SaveAttendanceRule",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newAttendanceRule }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearAttendanceRule();
				$scope.GetAllAttendanceRuleList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}
	$scope.GetAllAttendanceRuleList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AttendanceRuleList = [];
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetAllAttendanceRuleList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AttendanceRuleList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetAttendanceRuleById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			AttendanceRuleId: refData.AttendanceRuleId
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetAttendanceRuleById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAttendanceRule = res.data.Data;
				$scope.newAttendanceRule.Mode = 'Modify';
				//document.getElementById('table-listing').style.display = "none";
				//document.getElementById('notice-form').style.display = "block";
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAttendanceRuleById = function (refData) {
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
					AttendanceRuleId: refData.AttendanceRuleId
				};

				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/DelAttendanceRule",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAttendanceRuleList();
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