app.controller('ConfigurationController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Configuration'

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.newConfiguration = {
			BranchId: null,
			AcademicYearId: null,
			HomeworkLesson: false,
			HomeworkTopic: false,
			AssignmentLesson: false,
			Mode: 'Save'
		};


		$scope.GetConfiguration();

	}

	function OnClickDefault() {
		//document.getElementById('homework-add-form').style.display = "none";

		//document.getElementById('add-homework-add-btn').onclick = function () {
		//	document.getElementById('homework-add-section').style.display = "none";
		//	document.getElementById('homework-add-form').style.display = "block";
		//}

		//document.getElementById('back-homework-btn').onclick = function () {
		//	document.getElementById('homework-add-section').style.display = "block";
		//	document.getElementById('homework-add-form').style.display = "none";
		//}

	}

	$scope.ClearConfiguration = function () {
		$scope.newConfiguration = {
			BranchId: null,
			AcademicYearId: null,
			HomeworkLesson: false,
			HomeworkTopic: false,
			AssignmentLesson: false,
			Mode: 'Save'
		};
	}

	//************************* Add newConfiguration *********************************


	$scope.IsValidConfiguration = function () {
		//if ($scope.newAddHomework.Name.isEmpty()) {
		//	Swal.fire('Please ! Enter  Name');
		//	return false;
		//}
		return true;
	}
	$scope.SaveUpdateConfiguration = function () {
		if ($scope.IsValidConfiguration() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newConfiguration.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateConfiguration();
					}
				});
			} else
				$scope.CallSaveUpdateConfiguration();
		}
	};

	$scope.CallSaveUpdateConfiguration = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();		
		$http({
			method: 'POST',
			url: base_url + "HomeWork/Transaction/SaveConfiguration",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				
				return formData;
			},
			data: { jsonData: $scope.newConfiguration }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				//$scope.ClearAddHomework();
			$scope.GetConfiguration();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetConfiguration = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.Configuration = [];		
		$http({
			method: 'POST',
			url: base_url + "HomeWork/Transaction/GetHAConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";


			if (res.data.IsSuccess && res.data.Data) {
				$scope.newConfiguration = res.data.Data;
			}
			else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

});