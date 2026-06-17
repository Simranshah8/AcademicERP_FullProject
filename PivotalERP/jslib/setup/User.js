

app.controller('UserController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'User';

	OnClickDefault();

	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			User: 1,

		};

		$scope.searchData = {
			User: '',

		};

		$scope.perPage = {
			User: GlobalServices.getPerPageRow(),

		};

		$scope.newUser = {
			UserId: 0,
			GroupId: 1,
			FirstName: '',
			LastName: '',
			Designation: '',
			UserName: '',
			Password: '',
			RePassword: '',
			Address: '',
			MobileNO:'',
			BranchId: 1,
			EMailID: '',
			Active: true,
			UserNeverExpire: false,
			UserCannotChangePassword: false,
			ChangePasswordFirstTime: false,
			StartDate_TMP: null,
			EndDate_TMP: null,
			LogonHours: 1,
			UserWiseSecurity: false,
			UserWiseAutoPost: false,
			Photo: null,
			PhotoPath: null,
			AllowAppLogin:false,
			Mode: 'Save'
		};
		$scope.UserGroupList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUserGroupList",
			dataType: "json"
		}).then(function (res) {		
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserGroupList = res.data.Data;
			} 
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.BranchList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchList",
			dataType: "json"
		}).then(function (res) {			
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchList = res.data.Data;
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllUserList();

	}

	function OnClickDefault() {

		document.getElementById('user-form').style.display = "none";

		//User section
		document.getElementById('add-user').onclick = function () {
			document.getElementById('user-section').style.display = "none";
			document.getElementById('user-form').style.display = "block";
			$scope.ClearUser();
		}

		document.getElementById('back-to-list-user').onclick = function () {
			document.getElementById('user-form').style.display = "none";
			document.getElementById('user-section').style.display = "block";
			$scope.ClearUser();
		}

	}

	$scope.ClearUser = function () {

		$scope.ClearUserPhoto();
		$scope.newUser = {
			UserId: 0,
			GroupId: 1,
			FirstName: '',
			LastName: '',
			Designation: '',
			UserName: '',
			Password: '',
			RePassword: '',
			Address: '',
			MobileNO: '',
			BranchId: 1,
			EMailID: '',
			Active: true,
			UserNeverExpire: false,
			UserCannotChangePassword: false,
			ChangePasswordFirstTime: false,
			StartDate_TMP: null,
			EndDate_TMP: null,
			LogonHours: 1,
			UserWiseSecurity: false,
			UserWiseAutoPost: false,
			Photo: null,
			PhotoPath: null,
			AllowAppLogin: false,
			Mode: 'Save'
		};

	}

	// Clear photo
	$scope.ClearUserPhoto = function () {

		
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newUser.PhotoData = null;
				$scope.newUser.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};


	//*************************User *********************************

	$scope.IsValidUser = function () {

		if ($scope.newUser.FirstName.isEmpty()) {
			Swal.fire('Please ! Enter First Name');
			return false;
		}

		if ($scope.newUser.LastName.isEmpty()) {
			Swal.fire('Please ! Enter Last Name');
			return false;
		}

		if ($scope.newUser.UserName.isEmpty()) {
			Swal.fire('Please ! Enter UserName');
			return false;
		}

		if ($scope.newUser.BranchId==0) {
			Swal.fire('Please ! Select Valid Branch Name');
			return false;
		}

		if ($scope.newUser.UserId == 0) {
			if ($scope.newUser.Password.isEmpty()) {
				Swal.fire('Please ! Enter User Password');
				return false;
			}

			if ($scope.newUser.RePassword.isEmpty()) {
				Swal.fire('Please ! Enter User Re-Password');
				return false;
			}

			if ($scope.newUser.RePassword != $scope.newUser.Password) {
				Swal.fire('User Password Re-Password does not matched');
				return false;
			}
        }
		

	
		return true;
	}

	$scope.SaveUpdateUser = function () {
		if ($scope.IsValidUser() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newUser.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateUser();
					}
				});
			} else
				$scope.CallSaveUpdateUser();

		}
	};

	$scope.CallSaveUpdateUser = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newUser.StartDateDet) {
			$scope.newUser.StartDate = $filter('date')(new Date($scope.newUser.StartDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newUser.StartDate = null;

		if ($scope.newUser.EndDateDet) {
			$scope.newUser.EndDate = $filter('date')(new Date($scope.newUser.EndDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newUser.EndDate = null;

		// photo function
		var photo = $scope.newUser.Photo_TMP;

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveUser",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.stPhoto && data.stPhoto.length > 0)
					formData.append("photo", data.stPhoto[0]);


				return formData;
			},
			data: { jsonData: $scope.newUser, stPhoto: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearUser();
				$scope.GetAllUserList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

		
	}

	$scope.GetAllUserList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.UserList = [];

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUserList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetUserById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			UserId: refData.UserId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetUserById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$timeout(function () {
					$scope.newUser = res.data.Data;
					$scope.newUser.Mode = 'Modify';

					if ($scope.newUser.StartDate)
						$scope.newUser.StartDate_TMP = new Date($scope.newUser.StartDate);

					if ($scope.newUser.EndDate)
						$scope.newUser.EndDate_TMP = new Date($scope.newUser.EndDate);

					document.getElementById('user-section').style.display = "none";
					document.getElementById('user-form').style.display = "block";

				});
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelUserById = function (refData) {

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
					UserId: refData.UserId
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/DelUser",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllUserList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.ClearLogById = function (refData) {

		Swal.fire({
			title: 'Do you want to clear login lof of user '+refData.UserName+' ?',
			showCancelButton: true,
			confirmButtonText: 'Clear Log',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					UserName: refData.UserName
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/ClearLoginLog",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.GenToken = function () {

		Swal.fire({
			title: 'Are you sure to generate authentication key ? after reset existing google authenication will not work .',
			showCancelButton: true,
			confirmButtonText: 'Generate',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				 

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/GetGoogleAuthKey",
					dataType: "json", 
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.GoogleDet = {};
	$scope.GetAuthenticatorQR = function (usr) {
		$scope.GoogleDet = usr;
		$scope.bedata = usr;
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			UserName: usr.UserName
		}
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetGoogleQR",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			if (res.data.IsSuccess == true) {
				var qrDet = res.data.Data;
				$scope.GoogleDet.QRImage = qrDet.ResponseMSG;
				$('#AuthenticatorQR').modal('show');

			}
			else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (errormessage) {
			hidePleaseWait();
			//$scope.loadingstatus = "stop";
			Swal.fire(errormessage);
		});
	}
	 

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});
