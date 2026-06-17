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
	
	$scope.UpdatePassword= function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Student/User/UpdatePassword",
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
			alert(res.data.ResponseMSG);
			if (res.data.ResponseMSG == 'Password Changed Success Fully' && res.data.IsSuccess == true) {
				document.location.href = base_url + "Accounts/LogOff";
			}
			if (res.data.IsSuccess == true) {
				document.location.href = base_url + "Accounts/LogOff";
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}



});