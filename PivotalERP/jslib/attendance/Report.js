app.controller('ReportController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Report';

	

	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.ClassList = [];
		GlobalServices.getClassList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SectionList = [];
		GlobalServices.getSectionList().then(function (res) {
			$scope.SectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.currentPages = {
			StudentAttendanceLog: 1,
			OnlineClassAttendance: 1,
			LastAttendanceLog: 1,
			StudentDailyAttendance: 1,
			StudentAttendanceSummary:1
		};

		$scope.searchData = {
			StudentAttendanceLog: '',
			OnlineClassAttendance: '',
			LastAttendanceLog: '',
			StudentDailyAttendance: '',
			StudentAttendanceSummary: ''
		};

		$scope.perPage = {
			StudentAttendanceLog: GlobalServices.getPerPageRow(),
			OnlineClassAttendance: GlobalServices.getPerPageRow(),
			LastAttendanceLog: GlobalServices.getPerPageRow(),
			StudentDailyAttendance: GlobalServices.getPerPageRow(),
			StudentAttendanceSummary: GlobalServices.getPerPageRow()
		};

		$scope.newStudentAttendanceLog = {
			StudentAttendanceLogId: null,
			
		};

		$scope.newOnlineClassAttendance = {
			OnlineClassAttendanceId: null,
			
		};

		$scope.newLastAttendanceLog = {
			LastAttendanceLogId: null,
			
		};

		$scope.newStudentDailyAttendance = {
			StudentDailyAttendanceId: null,
			
		};

		$scope.newStudentAttendanceSummary = {
			StudentAttendanceSummaryId: null,

		};

		//$scope.GetAllStudentAttendanceLogList();
		//$scope.GetAllOnlineClassAttendanceList();
		//$scope.GetAllLastAttendanceLogList();
		//$scope.GetAllStudentDailyAttendanceList();
		//$scope.GetAllStudentAttendanceSummaryList();

	}

	

	$scope.ClearStudentAttendanceLog = function () {
		$scope.newStudentAttendanceLog = {
			StudentAttendanceLogId: null,

		};
	}
	$scope.ClearOnlineClassAttendance = function () {
		$scope.newOnlineClassAttendance = {
			OnlineClassAttendanceId: null,

		};
	}
	$scope.ClearLastAttendanceLog = function () {
		$scope.newLastAttendanceLog = {
			LastAttendanceLogId: null,

		};
	}

	$scope.ClearStudentDailyAttendance = function () {
		$scope.newStudentDailyAttendance = {
			StudentDailyAttendanceId: null,

		};
	}


	$scope.ClearStudentAttendanceSummary = function () {
		$scope.newStudentAttendanceSummary = {
			StudentAttendanceSummaryId: null,

		};
	}

	

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});