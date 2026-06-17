app.controller('SyllabusStatusController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Syllabus Status';

	OnClickDefault();
	String.prototype.isEmpty = function () {
		return (this.length === 0 || !this.trim());
	};

	$scope.LoadData = function () {
		$('.select2').select2();
		//$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();
	}

	function OnClickDefault() {
		document.getElementById('classwisestatus-form').style.display = "none";
		
		//Classwise
		document.getElementById('classwisedetail').onclick = function () {
			document.getElementById('classwiseList').style.display = "none";
			document.getElementById('classwisestatus-form').style.display = "block";
		}

		document.getElementById('backclasswiselist').onclick = function () {
			document.getElementById('classwiseList').style.display = "block";
			document.getElementById('classwisestatus-form').style.display = "none";
		}

		
	}
	

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});