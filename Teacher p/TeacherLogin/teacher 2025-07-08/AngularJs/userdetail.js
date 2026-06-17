app.controller('UserDetailController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'User Detail';

	$scope.GetAllUserDetail = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		//	$scope.MyProfile = [];

		$http({
			method: 'GET',
			url: base_url + "OnlineClass/Creation/GetUserDetail",
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





});