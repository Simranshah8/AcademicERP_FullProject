
app.controller('UserGroupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'UserGroup';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			UserGroup: 1,

		};

		$scope.searchData = {
			UserGroup: '',

		};

		$scope.perPage = {
			UserGroup: GlobalServices.getPerPageRow(),

		};

		$scope.newUserGroup = {
			GroupId: 0,
			GroupName: '',
			Alias: '',
			Description: '',
			Inbuilt:false,
			Mode: 'Save'
		};


		$scope.GetAllUserGroupList();

	}

	function OnClickDefault() {

		document.getElementById('user-group-form').style.display = "none";

		//UserGroup section
		document.getElementById('add-user-group').onclick = function () {
			document.getElementById('user-group-section').style.display = "none";
			document.getElementById('user-group-form').style.display = "block";
			$scope.ClearUserGroup();
		}

		document.getElementById('back-to-list-user-group').onclick = function () {
			document.getElementById('user-group-form').style.display = "none";
			document.getElementById('user-group-section').style.display = "block";
			$scope.ClearUserGroup();
		}

	}

	$scope.ClearUserGroup = function () {
		$scope.newUserGroup = {
			GroupId: 0,
			GroupName: '',
			Alias: '',
			Description: '',
			Inbuilt: false,
			Mode: 'Save'
		};

	}

	//*************************UserGroup *********************************

	$scope.IsValidUserGroup = function () {
		if ($scope.newUserGroup.GroupName.isEmpty()) {
			Swal.fire('Please ! Enter UserGroup Name');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateUserGroup = function () {
		if ($scope.IsValidUserGroup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newUserGroup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateUserGroup();
					}
				});
			} else
				$scope.CallSaveUpdateUserGroup();

		}
	};

	$scope.CallSaveUpdateUserGroup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveUserGroup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newUserGroup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearUserGroup();
				$scope.GetAllUserGroupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllUserGroupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.UserGroupList = [];

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUserGroupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserGroupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetUserGroupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			GroupId: refData.GroupId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetUserGroupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newUserGroup = res.data.Data;
				$scope.newUserGroup.Mode = 'Modify';

				document.getElementById('user-group-section').style.display = "none";
				document.getElementById('user-group-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelUserGroupById = function (refData) {

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
					GroupId: refData.GroupId
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/DelUserGroup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllUserGroupList();
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
