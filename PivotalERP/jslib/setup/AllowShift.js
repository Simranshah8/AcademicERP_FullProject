
app.controller('AllowShiftController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow Shift';

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.newAllowShift = {
			AllowShiftId: null,
			UserId: null,
			CheckAll: false,

			Mode: 'Update Data'
		};

		//$scope.GetAllAllowShiftList();

	}

	$scope.ClearAllowShift = function () {
		$scope.newAllowShift = {
			AllowShiftId: null,
			UserId: null,
			CheckAll: false,

			Mode: 'Update Data'
		};
	}


	//************************* AllowShift *********************************

	$scope.IsValidAllowShift = function () {
		if ($scope.newAllowShift.UserId.isEmpty()) {
			Swal.fire('Please ! Select User');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAllowShift = function () {
		if ($scope.IsValidAllowShift() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAllowShift.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAllowShift();
					}
				});
			} else
				$scope.CallSaveUpdateAllowShift();

		}
	};

	$scope.CallSaveUpdateAllowShift = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/SaveAllowShift",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newAllowShift }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAllowShift();
				$scope.GetAllAllowShiftList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	}

	$scope.GetAllAllowShiftList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowShiftList = [];

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/GetAllAllowShiftList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowShiftList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetAllowShiftById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AllowShiftId: refData.AllowShiftId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/GetAllowShiftById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAllowShift = res.data.Data;
				$scope.newAllowShift.Mode = 'Modify';

				document.getElementById('author-section').style.display = "none";
				document.getElementById('author-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAllowShiftById = function (refData) {

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
					AllowShiftId: refData.AllowShiftId
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Setup/DelAllowShift",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAllowShiftList();
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