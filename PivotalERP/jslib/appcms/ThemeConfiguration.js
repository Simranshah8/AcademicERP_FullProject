app.controller('ThemeConfigController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'ThemeConfig';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.ThemeList = [{ id: 1, text: 'Theme One' }, { id: 2, text: 'Theme Two' }, { id: 3, text: 'Theme Three' }, { id: 4, text: 'Theme Four' }, { id: 5, text: 'Theme Five' }]
		

		$scope.newDet = {
			TranId: null,
			ThemeId: 1,
			PrimaryColor: '',
			SecondaryColor: '',
			ThirdColor: '',
			FourthColor: '',
			FifthColor:'',
			Mode: 'Save'
		};
		$scope.GetThemeConfig();
	}


	$scope.ResetThemeConfig = function () {
		Swal.fire({
			title: 'Would you like to reset the colors?',
			text: 'This will reset all colors to their default values and update the existing record.',
			icon: 'warning',
			showCancelButton: true,
			confirmButtonText: 'Yes',
			cancelButtonText: 'No',
		}).then((result) => {
			if (result.isConfirmed) {
				// Retain existing `TranId` and other properties
				$scope.newDet.PrimaryColor = '#005d5a';
				$scope.newDet.SecondaryColor = '#72be44';
				$scope.newDet.ThirdColor = '#007955';
				$scope.newDet.FourthColor = '#264167';
				$scope.newDet.FifthColor = '#f0fbff';
				$scope.newDet.ThemeId = 1; // Ensure default ThemeId is set
				// Ensure `Mode` is 'Update' to avoid creating a new record
				$scope.newDet.Mode = 'Update';

				$scope.$apply(); // Reflect changes in the UI

				// Call save function to update the existing record
				$scope.CallSaveUpdateThemeConfig();

				Swal.fire('Colors have been reset and updated!', '', 'success');
			} else {
				Swal.fire('Reset canceled', '', 'info');
			}
		});
	};




	//*************************Theme Config *********************************

	$scope.IsValidThemeConfig = function () {
		//if ($scope.newDet.PrimaryColor.isEmpty()) {
		//	Swal.fire('Please ! Enter PrimaryColor Name');
		//	return false;
		//}

		return true;
	}

	$scope.SaveUpdateThemeConfig = function () {
		if ($scope.IsValidThemeConfig() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdatePayrollConfig();
					}
				});
			} else
				$scope.CallSaveUpdateThemeConfig();

		}
	};

	$scope.CallSaveUpdateThemeConfig = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveThemeConfig",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newDet }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				/*$scope.ClearPayrollConfig();*/
				//$scope.GetAllPayrollConfigList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	$scope.GetThemeConfig = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.newDet = {};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetThemeConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDet = res.data.Data;

				if (!$scope.newDet.ThemeId) {
					$scope.newDet.ThemeId = 1;
				}
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