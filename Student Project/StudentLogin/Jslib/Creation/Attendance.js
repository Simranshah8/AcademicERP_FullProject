app.controller('AttendanceController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Attendance';

	
	$scope.LoadData = function () { 
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList(); 
		$scope.currentPages = {
			LeaveWise: 1,

		}; 
		$scope.searchData = {
			LeaveWise: '',

		}; 
		$scope.perPage = {
			LeaveWise: GlobalServices.getPerPageRow(),

		}; 
		$scope.newLeaveWise = {
			LeaveWiseId: null,
			
		}; 
	} 
	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	}; 
});