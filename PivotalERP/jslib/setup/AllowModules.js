
app.controller('AllowModulesController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow Modules';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.newAllowModules = {
			TranId: null,
			UserId: null,
			CheckAll: false,

			Mode: 'Update Data'
		};

		$scope.AllowModulesList = [];
		$scope.UserList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUserList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserList = res.data.Data;

			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.GetAllAllowModulesList();

	}

	$scope.CheckUnCheckAll = function () {
		var val = $scope.newAllowModules.CheckedAll;
		angular.forEach($scope.AllowModulesList, function (cl) {
			cl.IsAllow = val;
		});
	}

	$scope.ClearAllowModules = function () {
		$scope.newAllowModules = {
			AllowModulesId: null,
			UserId: null,
			CheckAll: false,

			Mode: 'Update Data'
		};
	}


	//************************* AllowModules *********************************

	$scope.IsValidAllowModules = function () {
		//if ($scope.newAllowModules.UserId.isEmpty()) {
		//	Swal.fire('Please ! Select User');
		//	return false;
		//}

		return true;
	}

	$scope.SaveUpdateAllowModules = function () {
		if ($scope.IsValidAllowModules() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAllowModules.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAllowModules();
					}
				});
			} else
				$scope.CallSaveUpdateAllowModules();

		}
	};

	$scope.CallSaveUpdateAllowModules = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveAllowModule",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.AllowModulesList }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAllowModules();
				$scope.GetAllAllowModulesList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	}
	$scope.GetAllowModuleList = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			UserId: $scope.newAllowModules.UserId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllowModuleById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowModulesList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

});