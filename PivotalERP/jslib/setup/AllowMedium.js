
app.controller('AllowMediumController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow Medium';

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.newAllowMedium = {
			AllowMediumId: null,
			UserId: null,
			CheckAll: false,

			Mode: 'Update Data'
		};

		//$scope.GetAllAllowMediumList();

	}

	$scope.ClearAllowMedium = function () {
		$scope.newAllowMedium = {
			AllowMediumId: null,
			UserId: null,
			CheckAll: false,

			Mode: 'Update Data'
		};
	}


	//************************* AllowMedium *********************************

	$scope.IsValidAllowMedium = function () {
		if ($scope.newAllowMedium.UserId.isEmpty()) {
			Swal.fire('Please ! Select User');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAllowMedium = function () {
		if ($scope.IsValidAllowMedium() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAllowMedium.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAllowMedium();
					}
				});
			} else
				$scope.CallSaveUpdateAllowMedium();

		}
	};

	$scope.CallSaveUpdateAllowMedium = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/SaveAllowMedium",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newAllowMedium }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAllowMedium();
				$scope.GetAllAllowMediumList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	}

	$scope.GetAllAllowMediumList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowMediumList = [];

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/GetAllAllowMediumList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowMediumList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetAllowMediumById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AllowMediumId: refData.AllowMediumId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/GetAllowMediumById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAllowMedium = res.data.Data;
				$scope.newAllowMedium.Mode = 'Modify';

				document.getElementById('author-section').style.display = "none";
				document.getElementById('author-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAllowMediumById = function (refData) {

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
					AllowMediumId: refData.AllowMediumId
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Setup/DelAllowMedium",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAllowMediumList();
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