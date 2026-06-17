app.controller('EmployeeAttendanceController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Employee Attendance';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.LanguageColl = GlobalServices.getLangList();		

		$scope.currentPages = {
			DailyAttendance: 1,
			PeriodAttendance: 1,
		};

		$scope.searchData = {
			DailyAttendance: '',
			PeriodAttendance: '',
		};

		$scope.perPage = {
			DailyAttendance: GlobalServices.getPerPageRow(),
			PeriodAttendance: GlobalServices.getPerPageRow(),
		};

        $scope.newPeriodAttendence = {
            FromDate_TMP: new Date(),
            ToDate_TMP: new Date(),
        };
    };


	/************************Daily Attendance ***************/
	function OnClickDefault() {
		document.getElementById('detail-of-employee').style.display = "none";
		document.getElementById('back-btn').onclick = function () {
			document.getElementById('daily-attendence-section').style.display = "block";
			document.getElementById('detail-of-employee').style.display = "none";
		}
	};


	/************************Period Attendance ***************/

	$scope.CurDetails = {};
	$scope.CurDetailsColl = [];
	$scope.ShowDetails = function (det) {

		$scope.CurDetails = det;
		$scope.CurDetailsColl = [];
		var para = {
			tranIdColl: det.TranIdColl
		};

		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetEmployeeOnlineAttendanceDet",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data.Data) {
				$scope.CurDetailsColl = res.data.Data;

				document.getElementById('daily-attendence-section').style.display = "none";
				document.getElementById('detail-of-employee').style.display = "block";
			} else {
				Swal.fire(res.data);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
	$scope.GetDateWiseAttendance = function () {
		$scope.newPeriodAttendence.DateList = [];
		$scope.newPeriodAttendence.StudentList = [];
		var para = {
			fromDate: $filter('date')(new Date($scope.newPeriodAttendence.FromDateDet.dateAD), 'yyyy-MM-dd'),
			toDate: $filter('date')(new Date($scope.newPeriodAttendence.ToDateDet.dateAD), 'yyyy-MM-dd'),
		};

		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetEmployeeOnlineAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data.Data) {
				var tmpDataColl = mx(res.data.Data);

				var finalColl = [];
				var subjectQuery = tmpDataColl.groupBy(t => t.ForDate_BS).toArray();
				var fiSNo = 1;
				angular.forEach(subjectQuery, function (f) {
					$scope.newPeriodAttendence.DateList.push(
						{
							id: f.elements[0].Period,
							text: (f.key ? f.key : ''),
							shorttext: f.key.toString().substring(0, 5),
							forDate: new Date(f.elements[0].ForDate_AD)
						});
				});

				var query = tmpDataColl.groupBy(t => t.EmployeeId).toArray();
				var nSNO = 1;
				angular.forEach(query, function (q) {
					var subData = mx(q.elements);
					var fst = subData.firstOrDefault();
					var beData = {
						SNo: nSNO,
						EmployeeId: fst.EmployeeId,
						Name: fst.Name,
						Code: fst.Code,
						Department: fst.Department,
						Designation: fst.Designation,
						TranIdColl: fst.TranIdColl,
						SubjectDetailsColl: []
					};

					var totalC = 0, totalH = 0;
					angular.forEach($scope.newPeriodAttendence.DateList, function (fi) {
						var find = subData.firstOrDefault(p1 => p1.ForDate_BS == fi.text);
						beData.SubjectDetailsColl.push({
							ForDate_BS: fi.text,
							ScheduleClass: (find ? find.ScheduleClass : 0),
							NoOfClassHosted: (find ? find.NoOfClassHosted : 0),
							Attendance: (find && find.JoinDateTime ? 'P' : 'A')
						});

						if (find && find.ScheduleClass) {
							totalC += find.ScheduleClass;
						}

						if (find && find.NoOfClassHosted) {
							totalH += find.NoOfClassHosted;
						}
					});

					beData.TotalAttendance = totalH;

					var attPer = (totalH / totalC) * 100;

					beData.ScheduleClass = totalC;
					beData.NoOfClassHosted = totalH;
					beData.AttendancePer = attPer;
					beData.AbsentPer = (100 - attPer);
					finalColl.push(beData);
					nSNO++;
				});

				$scope.newPeriodAttendence.StudentList = finalColl;

			} else {
				Swal.fire(res.data);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.validateDates = function (changedField) {
		if (!$scope.newPeriodAttendence.FromDateDet || !$scope.newPeriodAttendence.ToDateDet ||
			!$scope.newPeriodAttendence.FromDateDet.dateAD || !$scope.newPeriodAttendence.ToDateDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newPeriodAttendence.FromDateDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newPeriodAttendence.ToDateDet.dateAD), 'yyyy-MM-dd')
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newPeriodAttendence.FromDate_TMP = new Date();
						$scope.newPeriodAttendence.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newPeriodAttendence.ToDate_TMP = new Date();
						$scope.newPeriodAttendence.ToDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};

	$scope.validateDate = function (obj, startField, endField, startLabel, endLabel) {
		var res = GlobalServices.validateDate(obj, startField, endField, startLabel, endLabel);
		if (res.IsSuccess == false) {
			Swal.fire({
				icon: 'warning',
				text: res.Message,
				confirmButtonText: 'OK'
			}).then(function () {
				obj.FromDate_TMP = new Date();
				obj.ToDate_TMP = new Date();
				$scope.$applyAsync();
			});
		}
	};



});