app.controller('ComplaintAndFeedbackController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Complaint And Feedback'; 
	String.prototype.isEmpty = function () {
		return (this.length === 0 || !this.trim());
	}; 
	$scope.LoadData = function () { 
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList(); 
		$scope.currentPages = {
			Complaint: 1,
			Feedback:1

		}; 
		$scope.searchData = {
			Complaint: '',
			Feedback: ''

		}; 
		$scope.perPage = {
			Complaint: GlobalServices.getPerPageRow(),
			Feedback: GlobalServices.getPerPageRow(),

		}; 
	}  
	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});