
app.controller('NotificationLogController', function ($scope, $http) {
	$scope.Title = 'Student Attendance Classwise';

	
	$scope.LoadData = function () {
		$scope.V = {};
		$scope.newNotificationLog = {};
	}

	$scope.OnChangeDate = function () {
		if ($scope.newNotificationLog.dateFrom && $scope.newNotificationLog.dateTo) {
			var res = $scope.newNotificationLog.dateFrom.split("-");
			var resTo = $scope.newNotificationLog.dateTo.split("-");
			$scope.dateFrom = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
			$scope.dateTo = NepaliFunctions.BS2AD({ year: resTo[0], month: resTo[1], day: resTo[2] })
			$scope.V.dateFrom = $scope.dateFrom.year + '-' + $scope.dateFrom.month + '-' + $scope.dateFrom.day;
			$scope.V.dateTo = $scope.dateTo.year + '-' + $scope.dateTo.month + '-' + $scope.dateTo.day;

			$scope.GetAllNotificationLog($scope.V);

        }

	};


	$scope.GetAllNotificationLog = function (value) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.NotificationList = [];


		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetAllNotificationLogList",
			data: value,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data && res.data) {
				$scope.NotificationList = res.data;
				//$scope.v=res.data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
});