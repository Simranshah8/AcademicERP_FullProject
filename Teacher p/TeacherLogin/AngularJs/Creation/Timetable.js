
String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};

app.controller('TimetableController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Timetable';

	$scope.LoadData = function () {
		$scope.ClassShiftColl = [];
		$http.get(base_url + "OnlineClass/Creation/GetClassShiftLit")
			.then(function (data) {
				$scope.ClassShiftColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});
		$scope.ClassScheduleColl = [];
		$http.get(base_url + "OnlineClass/Creation/GetClassSchedule").then(function (data) {
			$scope.ClassScheduleColl = data.data;
			$scope.ClassShiftColl;
			var CurrentDate = new Date();
			$scope.DayId = CurrentDate.getDay();
			$scope.DayId = $scope.DayId + 1;
			$scope.GetAccodingToMonth($scope.DayId);
		}, function (reason) {
			alert("Data not get");
		});
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
		


		
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		
	

		

		// Calling ExamTypeList
		$scope.ExamTypeColl = [];
		$http.get(base_url + "OnlineExam/Creation/GetExamTypeList")
			.then(function (data) {
				$scope.ExamTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});
		$scope.TimeTable = {}
		$scope.ExamShedule = {}
		$scope.GetExamSchedule();
		$scope.GetClassSchedule();
		
		$scope.currentPages = {			
			ExamSchedule: 1,
		};

		$scope.searchData = {			
			ExamSchedule: '',
		};

		$scope.perPage = {			
			ExamSchedule: GlobalServices.getPerPageRow(),
		};

		
	}
	$scope.OnChangeClassTime = function () {
		$scope.TimeTable.classShiftId;
		$scope.TimeTable.classId;

		//$scope.GetClassSchedule();
	}

	$scope.getFavClassIcon = function (favId) {
		return {
			'nav-link': !$scope.favorite,
			'nav-link active': $scope.favorite || $scope.DayId == favId
		};
	};

	$scope.GetAccodingToMonth = function (V) {
		$scope.ClassScheduleCollList = [];
		$scope.ClassShiftList = [];
		angular.forEach($scope.ClassScheduleColl, function (st) {
			if (V == st.DayId) {
				$scope.ClassScheduleCollList.push(st)
			}
		});
		angular.forEach($scope.ClassShiftColl, function (sc) {
			//$scope.keepGoing = true; 
			angular.forEach($scope.ClassScheduleCollList, function (CSL) {
				if (CSL.ShiftId == sc.ClassShiftId/* && $scope.keepGoing*/) {
					$scope.ClassShiftList.push(sc);
					//$scope.keepGoing = false; 
				}
			})

		});
		$scope.ClassShiftList = Array.from(new Set($scope.ClassShiftList));
    }
	$scope.GetAccodingID = function (V) {
		$scope.ClassScheduleCollListID = [];
		$scope.ClassShiftListID = [];
		angular.forEach($scope.ClassScheduleCollId, function (st) {
			if (V == st.DayId) {
				$scope.ClassScheduleCollListID.push(st)
			}
		});
		angular.forEach($scope.ClassShiftColl, function (sc) {
			//$scope.keepGoing = true; 
			angular.forEach($scope.ClassScheduleCollListID, function (CSL) {
				if (CSL.ShiftId == sc.ClassShiftId/* && $scope.keepGoing*/) {
					$scope.ClassShiftListID.push(sc);
					//$scope.keepGoing = false; 
				}
			})

		});
		$scope.ClassShiftListID = Array.from(new Set($scope.ClassShiftListID));
	}

	$scope.ngChangeExamShedule = function () {
				
		$('#cboSectionId').on("change", function (e) {
			var selected_element = $(e.currentTarget);
			var select_val = selected_element.val();

			$scope.SectionId = select_val;
			$scope.ExamShedule.sectionIdColl = ($scope.SectionId && $scope.SectionId.length > 0 ? $scope.SectionId.toString() : '');
		});
		
		
		$scope.GetExamSchedule();
    }
	
	$scope.GetExamSchedule = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamScheduleList = [];

		$http({
			method: 'POST',
			url: base_url + "Timetable/Creation/GetExamSchedule",
			data: $scope.ExamShedule,
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

	
	$scope.GetClassSchedule = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ClassScheduleCollId = [];

		$http({
			method: 'POST',
			url: base_url + "Timetable/Creation/GetClassSchedule",
			data: $scope.TimeTable,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.ClassScheduleCollId = res.data;
				var CurrentDate = new Date();
				var DayId = CurrentDate.getDay();
				 DayId =  DayId + 1;

				$scope.GetAccodingID(DayId);
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});