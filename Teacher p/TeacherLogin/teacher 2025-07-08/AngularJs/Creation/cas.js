
String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};

app.controller('CasController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'CAS';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			MarksEntry: 1,
			Summary: 1,
			

		};

		$scope.searchData = {
			MarksEntry: '',
			Summary: '',
			
		};

		$scope.perPage = {
			MarksEntry: GlobalServices.getPerPageRow(),
			Summary: GlobalServices.getPerPageRow(),
		
		};

		$scope.newMarksEntry = {
			MarksEntryId: null,
			Mode: 'Save'
		};

		$scope.newSummary = {
			SummaryId: null,
			Mode: 'Save'
		};

		



		//$scope.GetAllMarksEntryList();
		//$scope.GetAllAttendanceSummaryList();
		//$scope.GetAllAllClassSummaryList();



	}





	$scope.ClearMarksEntry = function () {
		$scope.newMarksEntry = {
			MarksEntryId: null,
			Mode: 'Save'
		};
	}

	$scope.ClearSummary = function () {
		$scope.newSummary = {
			SummaryId: null,
			Mode: 'Save'
		};
	}





	//*************************MarksEntry *********************************

	//*************************Summary *********************************














	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});