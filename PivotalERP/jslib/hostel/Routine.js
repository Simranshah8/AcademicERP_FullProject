app.controller('RoutineController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Routine';

	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.DaysColl = [
			{ id: 1, text: 'Sunday' },
			{ id: 2, text: 'Monday' },
			{ id: 3, text: 'Tuesday' },
			{ id: 4, text: 'Wednesday' },
			{ id: 5, text: 'Thursday' },
			{ id: 6, text: 'Friday' },
			{ id: 7, text: 'Saturday' }
		];

		$scope.currentPages = {
			Routine: 1

		};

		$scope.searchData = {
			Routine: ''
		};

		$scope.perPage = {
			Routine: GlobalServices.getPerPageRow()
		};


		$scope.newRoutine = {
			RoutineId: null,
			Day: [],
			Description: '',
			TimetableDetColl: [],
			Mode: 'Save'
		};
		$scope.newRoutine.TimetableDetColl.push({});
		$scope.GetAllRoutineList();
	}

	$scope.toggleDays = function (changedDay, isForAllDayToggle) {
		if (isForAllDayToggle) {
			if ($scope.newRoutine.ForAllDay) {
				$scope.DaysColl.forEach(day => day.checked = true);
			} else {
				$scope.DaysColl.forEach(day => day.checked = false);
			}
		} else {
			const allChecked = $scope.DaysColl.every(day => day.checked);
			$scope.newRoutine.ForAllDay = allChecked;
		}
	};




	$scope.CalculateDuration = function () {
		angular.forEach($scope.newRoutine.TimetableDetColl, function (item) {
			if (item.StartTime_TMP && item.EndTime_TMP) {
				var start = moment(item.StartTime_TMP);
				var end = moment(item.EndTime_TMP);

				// If end is before start (e.g., overnight session), add 1 day
				if (end.isBefore(start)) {
					end.add(1, 'day');
				}

				var durationInMinutes = end.diff(start, "minutes");
				item.Duration = durationInMinutes;
			} else {
				item.Duration = "";
			}
		});
	};



	$scope.AddRoutineAddDetails = function (ind) {
		if ($scope.newRoutine.TimetableDetColl) {
			if ($scope.newRoutine.TimetableDetColl.length > ind + 1) {
				$scope.newRoutine.TimetableDetColl.splice(ind + 1, 0, {
					Remarks: ''
				})
			} else {
				$scope.newRoutine.TimetableDetColl.push({
					Remarks: ''
				})
			}
		}
	};
	$scope.delRoutineAddDetails = function (ind) {
		if ($scope.newRoutine.TimetableDetColl) {
			if ($scope.newRoutine.TimetableDetColl.length > 1) {
				$scope.newRoutine.TimetableDetColl.splice(ind, 1);
			}
		}
	};


	function OnClickDefault() {

		document.getElementById('Routine-form').style.display = "none";

		//Routine section
		document.getElementById('add-Routine').onclick = function () {
			document.getElementById('Routine-section').style.display = "none";
			document.getElementById('Routine-form').style.display = "block";
			$scope.ClearRoutine();
		}

		document.getElementById('back-to-list-Routine').onclick = function () {
			document.getElementById('Routine-form').style.display = "none";
			document.getElementById('Routine-section').style.display = "block";
			$scope.ClearRoutine();
		}

	}

	$scope.ClearRoutine = function () {
		$scope.newRoutine = {
			RoutineId: null,
			Day: [],
			Description: '',
			TimetableDetColl: [],
			Mode: 'Save'
		};
		angular.forEach($scope.DaysColl, function (day) {
			day.checked = false;
		});
		$scope.newRoutine.TimetableDetColl.push({});
	}

	//*************************Routine *********************************

	$scope.IsValidRoutine = function () {
		if ($scope.newRoutine.Description.isEmpty()) {
			Swal.fire('Please ! Enter Routine Description');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateRoutine = function () {
		if ($scope.IsValidRoutine() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newRoutine.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateRoutine();
					}
				});
			} else
				$scope.CallSaveUpdateRoutine();

		}
	};
	$scope.CallSaveUpdateRoutine = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		// Get selected day IDs
		var selectedDays = $scope.DaysColl.filter(d => d.checked).map(d => d.id);

		if (!selectedDays || selectedDays.length === 0) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire("Error", "Please select at least one day", "error");
			return;
		}

		if (!$scope.newRoutine.Description) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire("Error", "Description is required", "error");
			return;
		}

		if (!$scope.newRoutine.TimetableDetColl || $scope.newRoutine.TimetableDetColl.length === 0) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire("Error", "Please add at least one timetable slot", "error");
			return;
		}

		// Prepare routine collection
		var routineCollection = [];

		angular.forEach(selectedDays, function (dayId) {
			angular.forEach($scope.newRoutine.TimetableDetColl, function (slot) {
				// Convert input time ("HH:mm") to "HH:mm:ss"

				let startTime = slot.StartTime_TMP ? slot.StartTime_TMP.toLocaleString() : null;
				let endTime = slot.EndTime_TMP ? slot.EndTime_TMP.toLocaleString() : null;

				routineCollection.push({
					Description: $scope.newRoutine.Description,
					Day: dayId,
					StartTime: startTime,
					EndTime: endTime,
					Remarks: slot.Remarks
				});
			});
		});

		// Send to backend
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveRoutine",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: routineCollection }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG || "Routine saved successfully!");

			if (res.data.IsSuccess) {
				$scope.ClearRoutine();
				$scope.GetAllRoutineList();
			}
		}, function () {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire("Error", "Something went wrong", "error");
		});
	};




	$scope.GetAllRoutineList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.RoutineDayList = [];

		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllHostelRoutine",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				var dataColl = mx(res.data.Data);

				var query = dataColl.groupBy(t => ({ Day: t.Day }));

				angular.forEach(query, function (q) {
					var fst = q.elements[0];
					var subQry = mx(q.elements);

					var beData = {
						HostelRoutineId: fst.HostelRoutineId,
						Day: fst.Day,
						DayName: fst.DayName,
						Description: fst.Description,
						CreatedBy: fst.CreatedBy
					};

					$scope.RoutineDayList.push(beData);
				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});
	};


	$scope.GetRoutineById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.RoutineDayList = []; // Reset list

		var para = {
			Day: refData.Day || null
		};

		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllHostelRoutine",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				var dataColl = mx(res.data.Data);
				var query = dataColl.groupBy(t => ({ Day: t.Day }));

				angular.forEach(query, function (group) {
					var fst = group.elements[0];
					var routineDetails = group.elements.map(function (g) {
						return {
							HostelGuardianId: g.HostelGuardianId,
							StartTime: g.StartTime,
							EndTime: g.EndTime,
							StartTime_TMP: g.StartTime,
							EndTime_TMP: g.EndTime,
							StartTime_TMP: new Date(g.StartTime),
							EndTime_TMP: new Date(g.EndTime),
							Remarks: g.Remarks
						};
					});
					var routineData = {
						HostelRoutineId: fst.HostelRoutineId,
						Day: fst.Day,
						DayName: fst.DayName,
						Description: fst.Description,
						CreatedBy: fst.CreatedBy,
						TimetableDetColl: routineDetails
					};
					$scope.RoutineDayList.push(routineData);
					$scope.newRoutine = {
						Mode: 'Modify',
						Day: fst.Day,
						DayName: fst.DayName,
						Description: fst.Description,
						TimetableDetColl: routineDetails
					};
					angular.forEach($scope.DaysColl, function (day) {
						day.checked = (day.id == fst.Day);
					});
					document.getElementById('Routine-section').style.display = "none";
					document.getElementById('Routine-form').style.display = "block";
				});

			} else {
				Swal.fire(res.data.ResponseMSG || "No routine data found.");
			}
		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});
	};



	$scope.DelRoutineById = function (refData) {

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
					Day: refData.Day
				};

				$http({
					method: 'POST',
					url: base_url + "Hostel/Creation/DelHostelRoutine",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllRoutineList();
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