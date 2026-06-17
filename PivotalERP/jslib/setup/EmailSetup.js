
app.controller('EmailSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow Shift';

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.newEmailSetup = {
			EmailSetupId: null,
			EmailId: '',
			UserName: '',
			Password: '',
			SMTP: '',
			Port: '',
			UseSSL: false,
			SMSUser: '',
			SMSPwd:'',
			Mode: 'Save'
		};

		//$scope.GetAllEmailSetupList();

	}

	$scope.ClearEmailSetup = function () {
		$scope.newEmailSetup = {
			EmailSetupId: null,
			EmailId: '',
			UserName: '',
			Password: '',
			SMTP: '',
			Port: '',
			UseSSL: false,
			SMSUser: '',
			SMSPwd: '',
			Mode: 'Save'
		};
	}


	//************************* EmailSetup *********************************

	$scope.IsValidEmailSetup = function () {
		if ($scope.newEmailSetup.UserName.isEmpty()) {
			Swal.fire('Please ! Enter UserName');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateEmailSetup = function () {
		if ($scope.IsValidEmailSetup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newEmailSetup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateEmailSetup();
					}
				});
			} else
				$scope.CallSaveUpdateEmailSetup();

		}
	};

	$scope.CallSaveUpdateEmailSetup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/SaveEmailSetup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newEmailSetup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearEmailSetup();
				$scope.GetAllEmailSetupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	}

	$scope.GetAllEmailSetupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EmailSetupList = [];

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/GetAllEmailSetupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmailSetupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetEmailSetupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			EmailSetupId: refData.EmailSetupId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/GetEmailSetupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEmailSetup = res.data.Data;
				$scope.newEmailSetup.Mode = 'Modify';

				document.getElementById('author-section').style.display = "none";
				document.getElementById('author-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelEmailSetupById = function (refData) {

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
					EmailSetupId: refData.EmailSetupId
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Setup/DelEmailSetup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllEmailSetupList();
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