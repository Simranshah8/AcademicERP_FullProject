app.controller('LeaveEntryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'LeaveEntry Entry';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.LanguageColl = GlobalServices.getLangList();
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		$scope.LeaveDurationList = [
			{ id: 1, text: 'Full Day' },
			{ id: 2, text: 'Half Day' },
			{ id: 3, text: 'Hourly' }
		];
		$scope.LeavePeriodList = [
			{ id: 1, text: 'First Half' },
			{ id: 2, text: 'Second Half' },
			{ id: 3, text: 'Other' }
		];

		$scope.currentPages = {
			LeaveEntry: 1,
		};
		$scope.searchData = {
			LeaveEntry: '',
		};
		$scope.perPage = {
			LeaveEntry: GlobalServices.getPerPageRow(),
		};



		$scope.LeaveTypeList = [];
		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/GetAllLeaveType",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LeaveTypeList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newFilter = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date()
		};

		$scope.newLeaveEntry = {
			LeaveEntryId: null,
			LeaveTypeId: null,
			DurationTypeId: null,
			LeavePeriodId: null,
			Remarks: '',
			AttachDocument: '',
			LeaveTo: 1,
			RemainingLeave: null,
			LeaveHours: 0,
			SubstitueEmpId: null,
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
			NoOfDays: 1,
			AttachmentColl: [],
			LeaveDuration: 1,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			Mode: 'Save'
		};
		//$scope.GetAllLeaveEntryList();
	};

	//$scope.DateChanged = function () {
	//	if ($scope.newLeaveEntry.DateFromDet && $scope.newLeaveEntry.DateToDet) {
	//		var dt1 = new Date(($filter('date')(new Date($scope.newLeaveEntry.DateFromDet.dateAD), 'yyyy-MM-dd')));
	//		var dt2 = new Date(($filter('date')(new Date($scope.newLeaveEntry.DateToDet.dateAD), 'yyyy-MM-dd')));
	//		if (dt2 < dt1) {
	//			Swal.fire('To Date cannot be before From Date');
	//			$scope.newLeaveEntry.DateTo_TMP = '';
	//			$scope.newLeaveEntry.DateToDet = null;
	//			$scope.newLeaveEntry.NoOfDays = 0;
	//			return;
	//		}
	//		$scope.newLeaveEntry.NoOfDays = datediff(dt1, dt2)+1;
	//       }
	//   }

	$scope.DateChanged = function () {
		if ($scope.newLeaveEntry.DateFromDet && $scope.newLeaveEntry.DateToDet) {
			var dt1 = new Date(($filter('date')(new Date($scope.newLeaveEntry.DateFromDet.dateAD), 'yyyy-MM-dd')));
			var dt2 = new Date(($filter('date')(new Date($scope.newLeaveEntry.DateToDet.dateAD), 'yyyy-MM-dd')));
			if (dt1 > dt2) {
				Swal.fire({
					icon: 'warning',
					title: 'Invalid Dates',
					text: 'From Date cannot be greater than To Date'
				});
				$scope.newLeaveEntry.DateTo_TMP = null;
				// Reset fields if needed
				$scope.newLeaveEntry.TotalDays = 0;
				return;
			}
			var totalDays = datediff(dt1, dt2) + 1;

			// Full Day
			if ($scope.newLeaveEntry.LeaveDuration == 1) {
				$scope.newLeaveEntry.TotalDays = totalDays;
			}

			// Half Day → Multiply by 0.5 or devide by 2
			else if ($scope.newLeaveEntry.LeaveDuration == 2) {
				//	$scope.newLeaveEntry.TotalDays = totalDays * 0.5;
				$scope.newLeaveEntry.TotalDays = Math.round((totalDays * 0.5) * 100) / 100;
				//	$scope.newLeaveEntry.TotalDays = totalDays / 2;
			}

			// Hourly (optional)
			else if ($scope.newLeaveEntry.LeaveDuration == 3) {
				/*$scope.newLeaveEntry.TotalDays = 0; // handle separately*/
				var LeaveHours = $scope.newLeaveEntry.LeaveHours || 0;
				//var shiftHours = $scope.newLeaveEntry.ShiftHoursPerDay || 1;
				$scope.newLeaveEntry.TotalDays = Math.round(((LeaveHours * totalDays) / 8) * 100) / 100;

				//	$scope.newLeaveEntry.TotalDays = (LeaveHours * totalDays) / 8;  //1 working day = 8 hrs so shiftHours = 8 hrs
			}
		}
	};


	$scope.ClearLeaveEntry = function () {
		$timeout(function () {
			$scope.newLeaveEntry = {
				LeaveEntryId: null,
				LeaveTypeId: null,
				DurationTypeId: null,
				LeavePeriodId: null,
				Remarks: '',
				AttachDocument: '',
				LeaveTo: 1,
				LeaveHours: 0,
				TotalDays: 0,
				AttachmentColl: [],
				SelectStudent: $scope.StudentSearchOptions[0].value,
				SelectEmployee: $scope.EmployeeSearchOptions[0].value,
				DateFrom_TMP: new Date(),
				DateTo_TMP: new Date(),
				NoOfDays: 1,
				LeaveDuration: 1,
				Mode: 'Save'
			};

			$('input[type=file]').val('');
		});
	};




	$scope.IsValidLeaveEntry = function () {
		if (!$scope.newLeaveEntry.LeaveTypeId || $scope.newLeaveEntry.LeaveTypeId === 0) {
			Swal.fire('Please ! Select Leave Type');
			return false;
		}

		if (!$scope.newLeaveEntry.LeaveDuration || $scope.newLeaveEntry.LeaveDuration === 0) {
			Swal.fire('Please ! Select Leave Duration');
			return false;
		}

		if ($scope.newLeaveEntry.LeaveDuration == 3) {
			if (!$scope.newLeaveEntry.StartTime_TMP) {
				Swal.fire('Please ! Select Start Time');
				return false;
			}

			if (!$scope.newLeaveEntry.EndTime_TMP) {
				Swal.fire('Please ! Select End Time');
				return false;
			}
			if ($scope.newLeaveEntry.StartTime_TMP >= $scope.newLeaveEntry.EndTime_TMP) {
				Swal.fire('Start Time cannot be greater than or equal to End Time');
				return false;
			}
			return true;
		}
		if (
			!$scope.newLeaveEntry.DateFrom_TMP ||
			!$scope.newLeaveEntry.DateFromDet ||
			!$scope.newLeaveEntry.DateFromDet.dateAD
		) {
			Swal.fire('Please ! Select From Date');
			return false;
		}

		if (
			!$scope.newLeaveEntry.DateTo_TMP ||
			!$scope.newLeaveEntry.DateToDet ||
			!$scope.newLeaveEntry.DateToDet.dateAD
		) {
			Swal.fire('Please ! Select To Date');
			return false;
		}

		var fromDate = new Date($scope.newLeaveEntry.DateFromDet.dateAD);
		var toDate = new Date($scope.newLeaveEntry.DateToDet.dateAD);

		if (fromDate > toDate) {
			Swal.fire('From Date cannot be greater than To Date');
			return false;
		}


		if ($scope.newLeaveEntry.Remarks.isEmpty()) {
			Swal.fire('Please ! Enter Reason');
			return false;
		}
		return true;
	};

	$scope.SaveUpdateLeaveEntry = function () {
		if ($scope.IsValidLeaveEntry() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newLeaveEntry.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateLeaveEntry();
					}
				});
			} else
				$scope.CallSaveUpdateLeaveEntry();
		}
	};

	$scope.CallSaveUpdateLeaveEntry = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newLeaveEntry.Photo_TMP;

		if ($scope.newLeaveEntry.DateFromDet) {
			$scope.newLeaveEntry.DateFrom = $filter('date')(new Date($scope.newLeaveEntry.DateFromDet.dateAD), 'yyyy-MM-dd');
		}
		if ($scope.newLeaveEntry.DateToDet) {
			$scope.newLeaveEntry.DateTo = $filter('date')(new Date($scope.newLeaveEntry.DateToDet.dateAD), 'yyyy-MM-dd');
		}


		if ($scope.newLeaveEntry.StartTime_TMP) {
			$scope.newLeaveEntry.StartTime = $filter('date')(new Date($scope.newLeaveEntry.StartTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		}
		else
			$scope.newLeaveEntry.StartTime = null;


		if ($scope.newLeaveEntry.EndTime_TMP) {
			$scope.newLeaveEntry.EndTime = $filter('date')(new Date($scope.newLeaveEntry.EndTime_TMP), 'yyyy-MM-dd HH:mm:ss');
		}
		else
			$scope.newLeaveEntry.EndTime = null;


		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/SaveLeaveRequest",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						if (data.files[i].File)
							formData.append("file" + i, data.files[i].File);
						else
							formData.append("file" + i, data.files[i]);
					}
				}
				return formData;
			},
			data: { jsonData: $scope.newLeaveEntry, files: filesColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearLeaveEntry();
				//$scope.GetAllLeaveEntryList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllLeaveEntryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.LeaveEntryList = [];
		var para = {
			dateFrom: ($scope.newFilter.FromDateDet ? $filter('date')(new Date($scope.newFilter.FromDateDet.dateAD), 'yyyy-MM-dd') : null),
			dateTo: ($scope.newFilter.ToDateDet ? $filter('date')(new Date($scope.newFilter.ToDateDet.dateAD), 'yyyy-MM-dd') : null),
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/GetAllLeaveRequest",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LeaveEntryList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.GetLeaveEntryById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			LeaveRequestId: refData.LeaveRequestId
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/getLeaveEntryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newLeaveEntry = res.data.Data;

				if ($scope.newLeaveEntry.DateFrom)
					$scope.newLeaveEntry.DateFrom_TMP = new Date($scope.newLeaveEntry.DateFrom);

				if ($scope.newLeaveEntry.DateTo)
					$scope.newLeaveEntry.DateTo_TMP = new Date($scope.newLeaveEntry.DateTo);

				$scope.newLeaveEntry.Mode = 'Modify';
				$scope.open_form_btn();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelLeaveRequestById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { LeaveRequestId: refData.LeaveRequestId };
				$http({
					method: 'POST',
					url: base_url + "Attendance/Transaction/DeleteLeaveRequest",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllLeaveEntryList();
					}
					Swal.fire(res.data.ResponseMSG);

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	}

	$scope.calculateLeaveHours = function () {
		$scope.newLeaveEntry.LeaveHours = $scope.newLeaveEntry.LeaveHours || 0;

		if ($scope.newLeaveEntry.StartTime_TMP && $scope.newLeaveEntry.EndTime_TMP) {
			// Parse the time values
			var startTime = moment($scope.newLeaveEntry.StartTime_TMP, 'HH:mm');
			var endTime = moment($scope.newLeaveEntry.EndTime_TMP, 'HH:mm');

			// Calculate difference in minutes
			var diffMinutes = endTime.diff(startTime, 'minutes');

			// Check if end time is before start time
			if (diffMinutes < 0) {
				Swal.fire({
					icon: 'warning',
					title: 'Invalid Time',
					text: 'End time cannot be before start time'
				});
				$scope.newLeaveEntry.LeaveHours = 0;
				$scope.newLeaveEntry.TotalDays = 0;
				$scope.newLeaveEntry.EndTime_TMP = null;
				return;
			}

			// Convert minutes to hours (decimal)
			//$scope.newLeaveEntry.LeaveHours = diffMinutes / 60;
			$scope.newLeaveEntry.LeaveHours = Math.round((diffMinutes / 60) * 100) / 100;


			//// Optional: Display formatted time
			//var hours = Math.floor(diffMinutes / 60);
			//var minutes = diffMinutes % 60;
			//$scope.newLeaveEntry.LeaveHoursDisplay = hours + 'h ' + minutes + 'm';

			// Recalculate number of days
			$scope.DateChanged();
		}
	};

});