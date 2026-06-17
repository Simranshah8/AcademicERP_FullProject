app.controller('TimeTableController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'TimeTable';

	//OnClickDefault();

	String.prototype.isEmpty = function () {
		return (this.length === 0 || !this.trim());
	};

	$scope.LoadData = function () {
		$scope.GetExamSchedule(); 
		$scope.ExamTypeColl = [];
		$http.get(base_url + "Student/Creation/GetExamTypeList")
			.then(function (data) {
				$scope.ExamTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});
		 
		$scope.ClassScheduleColl = [];
		$http.get(base_url + "Student/Creation/GetClassSchedule")
			.then(function (data) {
				$scope.ClassScheduleColl = data.data;
				var CurrentDate = new Date();
				$scope.DayId = CurrentDate.getDay();
				$scope.DayId = $scope.DayId + 1;
				$scope.GetAccodingToMonth($scope.DayId);
				
			}, function (reason) {
				alert("Data not get");
			});
	}



	$scope.OnChnageExamType = function (V) {
		$scope.newExamSchedule.examTypeId = V.ExamTypeId;
		$scope.GetExamSchedule();
	}
	//Dynamic Active Tab function 
	$scope.getFavClassIcon = function (favId) {
		return {
			'nav-link': !$scope.favorite,
			'nav-link active': $scope.favorite || $scope.DayId  == favId
		};
	};
	$scope.GetAccodingToMonth = function (V) {
		$scope.ClassScheduleCollList = [];
		$scope.ClassShiftList = [];
		
		angular.forEach($scope.ClassScheduleColl, function (st) {
			if (V == st.DayId) {
				$scope.ClassScheduleCollList.push(st)
				
			}
			var ClassShift = st.ShiftName
			$scope.ClassShiftList.push(ClassShift);
			
		});
		
		$scope.ClassShiftList = Array.from(new Set($scope.ClassShiftList));
		
	}

	
	$scope.GetExamSchedule = function () {

		

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamScheduleList = [];

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/GetExamSchedule",
			data: $scope.newExamSchedule,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.ExamScheduleList = res.data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
	$scope.Days = function (Det) {
		var CurrentDate = new Date(Det);
		var DayId = CurrentDate.getDay();
		switch (DayId) {
			case 0:
				day = "Sunday";
				break;
			case 1:
				day = "Monday";
				break;
			case 2:
				day = "Tuesday";
				break;
			case 3:
				day = "Wednesday";
				break;
			case 4:
				day = "Thursday";
				break;
			case 5:
				day = "Friday";
				break;
			case 6:
				day = "Saturday";
		}
		return day;
	};
	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});