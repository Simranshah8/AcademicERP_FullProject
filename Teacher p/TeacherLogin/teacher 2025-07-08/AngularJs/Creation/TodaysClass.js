app.controller('TodaysClassController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Todays Class';

	
	String.prototype.isEmpty = function () {
		return (this.length === 0 || !this.trim());
	};

	$scope.LoadData = function () {
		$('.select2').select2();
		//$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();
	}


	$scope.newTodaysClass = {
		TodaysClassId: null,
		Date_TMP:null,
		Mode: 'Save'
	};

});