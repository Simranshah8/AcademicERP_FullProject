

function secondsToHms(d) {
	d = Number(d);
	var h = Math.floor(d / 3600);
	var m = Math.floor(d % 3600 / 60);
	var s = Math.floor(d % 3600 % 60);

	var hDisplay = h > 0 ? h + (h == 1 ? " h, " : " h, ") : "";
	var mDisplay = m > 0 ? m + (m == 1 ? " m, " : " m, ") : "";
	var sDisplay = s > 0 ? s + (s == 1 ? " s" : " s") : "";
	return hDisplay + mDisplay + sDisplay;
}
app.controller('LiveClassController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Live Class';
	String.prototype.isEmpty = function () {
		return (this.length === 0 || !this.trim());
	};

	$scope.LoadData = function () {
		 
		$scope.colors = ['#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744','#fcb4a9', '#63d1b4', '#6baed6', '#0f5a58','#e4e744']
        $scope.RunningClassesColl = [];
		$scope.RunningClassesList(); 
		$scope.ClassScheduleColl = [];
		$http.get(base_url + "Student/Creation/GetClassSchedule")
			.then(function (data) {
				$scope.ClassScheduleColl = data.data;
				var CurrentDate = new Date();
				$scope.DayId = CurrentDate.getDay();
				$scope.ChangeWeek($scope.DayId + 1);
				var IsColl = angular.isArray($scope.ClassScheduleColl);
				if (IsColl == false) {
					location.reload();
				} 
			}, function (reason) {
					
				alert("Data not get");
			}); 


		$scope.searchData = {
			LiveClass: '',

		}; 
		$scope.newLiveClass = {
			LiveClassId: null,

		};
		$scope.newLiveClass.ShowMissedClass = false;
		 
	}

	$scope.RunningClassesList = function () {

		$timeout(function () {
			$http.get(base_url + "Student/Creation/GetRunningClassesList")
				.then(function (data) {
					$scope.RunningClassesColl = data.data;
					var IsColl = angular.isArray($scope.RunningClassesColl);
					if (IsColl == true) {
						$scope.ClassRunning = $scope.RunningClassesColl[$scope.RunningClassesColl.length - 1];

					}

					if (IsColl == false) {
						location.reload();
					}
				}, function (reason) {

					alert("Data not get");
				});
		});
		
	}
	$scope.ChangeWeek = function (DayId) {
		$scope.ClassScheduleCollList = [];
		$scope.ClassShiftList = [];
		angular.forEach($scope.ClassScheduleColl, function (st) {
			if (DayId == st.DayId && st.ForType =="Up-comping") {
				$scope.ClassScheduleCollList.push(st)
			}
		});

	}
	
	$scope.ClearDate = function () {
		$scope.newPastClass.FromDate_TMP = null;
		$scope.newPastClass.FromDateDet = null;
		$scope.newPastClass.dateTo_TMP = null;
		$scope.newPastClass.dateToDet = null;
		$scope.newLiveClass.ShowMissedClass = false;
		$scope.PassClassesList = [];
		$scope.PassMissedClassesList = [];
	}


	$scope.OnChangeDate = function () {
		$scope.V = {};
		if ($scope.newPastClass.FromDate_TMP && $scope.newPastClass.dateTo_TMP ) {

			var res = $scope.newPastClass.FromDate_TMP.split("-");
			var res2 = $scope.newPastClass.dateTo_TMP.split("-");
			$scope.dateFrom = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
			$scope.dateTo = NepaliFunctions.BS2AD({ year: res2[0], month: res2[1], day: res2[2] })

			$scope.V.dateFrom = $scope.dateFrom.year + '-' + $scope.dateFrom.month + '-' + $scope.dateFrom.day;
			$scope.V.dateTo = $scope.dateTo.year + '-' + $scope.dateTo.month + '-' + $scope.dateTo.day;

			$scope.GetAllOnlineClassesList();

		}

	};
	$scope.GetAllOnlineClassesList = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.OnlineClassesList = [];
		$scope.PassClassesList = [];
		$scope.PassMissedClassesList = [];
		$http({
			method: 'POST',
			url: base_url + "Student/Creation/GetOnlineClassesList",
			dataType: "json",
			data: $scope.V
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.PassOnlineClassesList = res.data;

				angular.forEach($scope.PassOnlineClassesList, function (st) {
					angular.forEach(st.DataColl, function (DT) {
						DT.CalcDuration = secondsToHms(DT.Duration);
						if (DT.FirstJoinAt == '') {
							$scope.PassClassesList.push(DT);
						}
						else {
							$scope.PassMissedClassesList.push(DT);
                        }
					});
				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//Auto Call Function for Running Class 
	//setInterval(function () { $scope.RunningClassesList(); }, 5000);
	//setInterval(function () { $scope.RunningClassesList(); }, 300000);
	$scope.JionClass = function (Dat) {

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.OnlineClassList = [];

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/JoinOnlineClass",
			data: Dat,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				alert(res.data.ResponseMSG)

			}
			else {
				Swal.fire(res.data.ResponseMSG);
			}
			if (res.data.IsSuccess) {
				window.open(Dat.Link, '_blank');
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});