app.controller('loginController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Login';
	$scope.IsOTPEnabled = false;
	$scope.applyClicked = false;
	$scope.newEnquiry = {};

	// Function to handle OTP field show/hide
	$scope.handleOTPField = function (showField) {
		$timeout(function () {
			var otpField = document.getElementById('txtOTP');
			var otpInputGroup = document.querySelector('.input-group.mb-3:nth-child(3)'); // The OTP field's parent div

			if (otpField && otpInputGroup) {
				if (showField) {
					// Show the OTP field
					otpInputGroup.style.display = 'flex';
					otpField.required = true;
					otpField.placeholder = "Authentication Code";
					otpField.value = ""; // Clear any previous value
				} else {
					// Hide the OTP field
					otpInputGroup.style.display = 'none';
					otpField.required = false;
					otpField.value = ""; // Clear any value
				}
			}
		}, 0);
	};

	// Initialize when controller loads
	//$scope.initLoginPage = function () {		
	//	$scope.handleOTPField(false);
	//	var usernameField = document.getElementById('txtUserName');
	//	if (usernameField) {
	//		var newUsernameField = usernameField.cloneNode(true);
	//		usernameField.parentNode.replaceChild(newUsernameField, usernameField);
	//		usernameField = document.getElementById('txtUserName');

	//		usernameField.onblur = function () {
	//			$scope.CheckOTP();
	//		};

	//		usernameField.addEventListener('keypress', function (e) {
	//			if (e.key === 'Enter') {
	//				$scope.CheckOTP();
	//			}
	//		});

	//		if (usernameField.value) {
	//			$timeout(function () {
	//				$scope.CheckOTP();
	//			}, 500);
	//		}
	//	}
	//};

	$scope.initLoginPage = function () {
		$scope.handleOTPField(false);
		var usernameField = document.getElementById('txtUserName');
		if (usernameField) {

			usernameField.onblur = function () {
				$scope.$apply(function () {
					$scope.CheckOTP();
				});
			};

			usernameField.addEventListener('keypress', function (e) {
				if (e.key === 'Enter') {
					$scope.$apply(function () {
						$scope.CheckOTP();
					});
				}
			});

			if (usernameField.value) {
				$timeout(function () {
					$scope.CheckOTP();
				}, 500);
			}
		}
	};

	$scope.LoadData = function () {
		$scope.IsOTPEnabled = false;
		$scope.applyClicked = false;
		$scope.newEnquiry = {};
		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/GetAllEnquiryFormConfig",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEnquiry = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.onApplyClick = function () {
		$scope.applyClicked = true;

		if ($scope.newEnquiry.FormStatus == 1) {
			window.open('/Home/AdmissionEnquiry', '_blank');
		}

		// Optional: auto-hide after 5 seconds
		if ($scope.newEnquiry.FormStatus == 2) {
			setTimeout(() => {
				$scope.$apply(() => $scope.applyClicked = false);
			}, 25000);
		}
	};

	$scope.CheckOTP = function () {
		showPleaseWait();
		var username = document.getElementById('txtUserName').value;

		// If username is empty, hide OTP field and return
		if (!username) {
			$scope.handleOTPField(false);
			hidePleaseWait();
			return;
		}

		var para = {
			Username: username
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/CheckOTP",
			headers: { 'Content-Type': 'application/json' },
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();

			if (res.data.IsSuccess) {
				$scope.IsOTPEnabled = res.data.IsSuccess;
				$scope.handleOTPField(true);
			} else {
				$scope.IsOTPEnabled = false;
				$scope.handleOTPField(false);
				if (res.data.ResponseMSG) {
					Swal.fire(res.data.ResponseMSG);
				}
			}

		}, function (error) {
			hidePleaseWait();
			$scope.IsOTPEnabled = false;
			$scope.handleOTPField(false);
			Swal.fire('Failed to verify OTP setting');
		});
	};

	

	// Global function to check OTP (for HTML onblur)
	window.checkUserOTP = function () {
		// Get AngularJS scope and call CheckOTP
		var scope = angular.element(document.getElementById('txtUserName')).scope();
		if (scope && scope.CheckOTP) {
			scope.$apply(function () {
				scope.CheckOTP();
			});
		}
	};

	// Initialize login page when controller loads
	$scope.$on('$viewContentLoaded', function () {
		$timeout(function () {
			$scope.initLoginPage();
		}, 100);
	});

	// Also run initialization after a short delay to ensure DOM is ready
	$timeout(function () {
		$scope.initLoginPage();
	}, 300);
});