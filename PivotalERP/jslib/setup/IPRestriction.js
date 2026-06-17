
app.controller('IPRestrictionController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow Shift';

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.newIPRestriction = {
			IPRestrictionId: null,
			EnableApplication: false,
			AllowedIPAddress: '',
			BlockedIPAddress:'',
			Mode: 'Save'
		};

		//$scope.GetAllIPRestrictionList();

	}

	$scope.ClearIPRestriction = function () {
		$scope.newIPRestriction = {
			IPRestrictionId: null,
			EnableApplication: false,
			AllowedIPAddress: '',
			BlockedIPAddress: '',
			Mode: 'Save'
		};
	}


	//************************* IPRestriction *********************************

	$scope.IsValidIPRestriction = function () {
		if ($scope.newIPRestriction.AllowedIPAddress.isEmpty()) {
			Swal.fire('Please ! Enter Allowed IP Address');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateIPRestriction = function () {
		if ($scope.IsValidIPRestriction() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newIPRestriction.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateIPRestriction();
					}
				});
			} else
				$scope.CallSaveUpdateIPRestriction();

		}
	};

	$scope.CallSaveUpdateIPRestriction = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/SaveIPRestriction",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newIPRestriction }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearIPRestriction();
				$scope.GetAllIPRestrictionList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	}

	$scope.GetAllIPRestrictionList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.IPRestrictionList = [];

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/GetAllIPRestrictionList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.IPRestrictionList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetIPRestrictionById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			IPRestrictionId: refData.IPRestrictionId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/GetIPRestrictionById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newIPRestriction = res.data.Data;
				$scope.newIPRestriction.Mode = 'Modify';

				document.getElementById('author-section').style.display = "none";
				document.getElementById('author-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelIPRestrictionById = function (refData) {

		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					IPRestrictionId: refData.IPRestrictionId
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Setup/DelIPRestriction",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllIPRestrictionList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});