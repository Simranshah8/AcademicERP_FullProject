String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};

app.controller('ChangePasswordController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'ChangePassword';

	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.ChangPwd = {
			AddpwdId: null,
			ClassId: null,
			SectionId: null,
			SubjectId: null,
			pwdTypeId: null,
			Title: '',
			pwdDescription: '',
			Title: '',
			Mode: 'Save'
		};

		$scope.newpwdList = {
			pwdListId: null,
			Mode: 'Save'
		};
	}
	$scope.ClearAddpwd = function () {
		$scope.ChangPwd = {
			AddpwdId: null,
			ClassId: null,
			SectionId: null,
			SubjectId: null,
			pwdTypeId: null,
			Title: '',
			pwdDescription: '',
			Title: '',
			Mode: 'Save'
		};
	}
	$scope.IsValidAddpwd = function () {
		if ($scope.ChangPwd.oldPwd == '') {
			Swal.fire('Please ! Enter Title');
			return false;
		}


		if ($scope.ChangPwd.newPwd == '') {
			Swal.fire('Please ! Enter Description');
			return false;
		}

		return true;
	}
	$scope.SaveUpdatePassword = function () {
		if ($scope.IsValidAddpwd() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.ChangPwd.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAddpwd();
					}
				});
			} else
				$scope.CallSaveUpdateAddpwd();

		}
	};
	$scope.CallSaveUpdateAddpwd = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Accounts/UpdatePassword",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.ChangPwd }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				//$scope.ClearClass();
				//$scope.GetAllClassList();
				//$http({

				//	url: base_url + "Accounts/LogOff",

				//})
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}



});