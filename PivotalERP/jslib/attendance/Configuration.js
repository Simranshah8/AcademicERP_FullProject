app.controller('ConfigurationController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Configuration'

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
	

		$scope.newConfiguration = {
			BranchId: null,
			MonthlyMinAttendance: 0,
			MaxConsecutiveAbs: 0,
			AbsFinePerDay: 0,
			Mode: 'Save'
		};


		$scope.GetConfiguration();

	}

	

	$scope.ClearConfiguration = function () {
		$scope.newConfiguration = {
			BranchId: null,
			MonthlyMinAttendance: 0,
			MaxConsecutiveAbs: 0,
			AbsFinePerDay: 0,
			Mode: 'Save'
		};
	}

	//************************* Add newConfiguration *********************************
	$scope.IsValidConfiguration = function () {

		// Monthly Minimum Attendance (%)
		if ($scope.newConfiguration.MonthlyMinAttendance == null ||
			$scope.newConfiguration.MonthlyMinAttendance < 0 ||
			$scope.newConfiguration.MonthlyMinAttendance > 100) {
			Swal.fire("Monthly Minimum Attendance must be between 0 and 100.");
			return false;
		}

		// Max Consecutive Absence (Days)
		if ($scope.newConfiguration.MaxConsecutiveAbs == null ||
			$scope.newConfiguration.MaxConsecutiveAbs < 0) {
			Swal.fire("Max Consecutive Absence (Days) cannot be negative.");
			return false;
		}

		// Absence Fine (Per Day)
		if ($scope.newConfiguration.AbsFinePerDay == null ||
			$scope.newConfiguration.AbsFinePerDay < 0) {
			Swal.fire("Absence Fine (Per Day) cannot be negative.");
			return false;
		}

		return true;
	};

	$scope.SaveUpdateConfiguration = function () {
		if ($scope.IsValidConfiguration()) {
			var saveModify = $scope.newConfiguration.Mode || "Save"; // Default to "Save" if Mode is undefined
			Swal.fire({
				title: `Do you want to ${saveModify.toLowerCase()} the current configuration?`,
				icon: 'question',
				showCancelButton: true,
				confirmButtonText: saveModify,
				cancelButtonText: 'Cancel',
			}).then((result) => {
				if (result.isConfirmed) {
					$scope.CallSaveUpdateConfiguration();
				} else {
					Swal.fire('Action cancelled.', '', 'info');
				}
			});
		}
	};


	$scope.CallSaveUpdateConfiguration = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Attendance/Setup/SaveAttendanceConfig",
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
			url: base_url + "Attendance/Setup/GetAttendanceConfig",
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