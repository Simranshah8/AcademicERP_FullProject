
app.controller('BackdateEntryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Backdate Entry';
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.newBackdateEntry = {
			UserWise: 1,
			UserId: null,
			GroupId:null,
			ForAllVouchers: 0,
			Mode: 'Save'
		};
		$scope.searchData = {
			BackdateEntry: '',

		};
		$scope.UserList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUserList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserList = res.data.Data;

			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

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

		//$scope.GetAllBackdateEntryList();
	}

	$scope.ClearBackdateEntry = function () {
		$scope.newBackdateEntry = {
			UserWise: 1,
			UserId: null,
			GroupId: null,
			ForAllVouchers: 0,
			Mode: 'Save'
		};
	}

	$scope.ClearData = function () {
		$scope.AllowBackDateList = [];
	};
	$scope.GetAllowUserWise = function () {

		$scope.AllowBackDateList = [];
		if ($scope.newBackdateEntry.UserId) {
			var para = {
				UserId: $scope.newBackdateEntry.UserId,
				GroupId: null
			};
			$http({
				method: 'POST',
				url: base_url + "Setup/Security/GetAllowBackdateEntry",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.AllowBackDateList = res.data.Data;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        }
		
	};

	$scope.GetAllowGroupWise = function () {

		$scope.AllowBackDateList = [];

		if ($scope.newBackdateEntry.GroupId) {
			var para = {
				UserId: null,
				GroupId: $scope.newBackdateEntry.GroupId
			};
			$http({
				method: 'POST',
				url: base_url + "Setup/Security/GetAllowBackdateEntry",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.AllowBackDateList = res.data.Data;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        }
		
	};
	
	$scope.SaveUpdateBackdateEntry = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var tmpURL = '';
		if ($scope.newBackdateEntry.UserWise == 1)
			tmpURL = base_url + "Setup/Security/SaveUpdateAllowUserWiseBackDate";
		else
			tmpURL = base_url + "Setup/Security/SaveUpdateAllowUserGroupWiseBackDate";

		$http({
			method: 'POST',
			url: tmpURL,
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.AllowBackDateList }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.ChangeForAllVoucher = function () {

		angular.forEach($scope.AllowBackDateList, function (ab) {
			ab.TotalBackDaysAllow = $scope.newBackdateEntry.TotalBackDaysAllow;
		});
	};

	$scope.CheckUncheckAll = function () {
		angular.forEach($scope.AllowBackDateList, function (ab) {
			ab.AllowAnyDate = $scope.newBackdateEntry.CheckedAll;
		});
	}

	$scope.CheckAllForPostBackDaysAllow = function () {
		var tmpData = $filter('filter')($scope.AllowBackDateList, $scope.searchData.AllowBackDateEntry);

		angular.forEach(tmpData, function (ent) {
			ent.PostBackDaysAllow = $scope.newBackdateEntry.PostBackDaysAllow;
		});
	}

	$scope.CheckAllForCancelBackDaysAllow = function () {
		var tmpData = $filter('filter')($scope.AllowBackDateList, $scope.searchData.AllowBackDateEntry);

		angular.forEach(tmpData, function (ent) {
			ent.CancelBackDaysAllow = $scope.newBackdateEntry.CancelBackDaysAllow;
		});
	}
	
});