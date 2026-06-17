app.controller('UserDetailController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'User Detail';

	$scope.GetAllUserDetail = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		//	$scope.MyProfile = [];

		$http({
			method: 'GET',
			url: base_url + "Student/Creation/GetUserDetail",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data) {
				$scope.Details = res.data;
				//$scope.v=res.data;



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	$scope.NotificationPrivate = [];
	$scope.GetPrivateNotification = function (value) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		//$scope.NotificationList1st = [];
		


		$http({
			method: 'POST',
			url: base_url + "Student/Creation/PrivateNotification",
			data: value,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data && res.data) {
				$scope.NotificationPrivate = res.data; 
				//$scope.v=res.data;
			}
			//else {
			//	Swal.fire(res.data.ResponseMSG);
			//}

		}, function (reason) {
		//	Swal.fire('Failed' + reason);
		});

	}

	//setInterval(function () { $scope.GetPrivateNotification(); }, 1000);

});