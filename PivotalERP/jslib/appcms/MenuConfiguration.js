app.controller('MenuConfigController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'About Us';

	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		 
		$scope.GetAppCMSEntity();

	}
	     


	/*About Us Tab Js*/
	$scope.IsValidAboutUs = function () {
		 
		return true;
	}

	$scope.SaveUpdateEntity = function () {
		if ($scope.IsValidAboutUs() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAboutUs.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateEntity();
					}
				});
			} else
				$scope.CallSaveUpdateEntity();

		}
	};

	$scope.CallSaveUpdateEntity = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
 

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveMenuConfiguration",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
			 
				return formData;
			},

			data: { jsonData: $scope.EntityColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAppCMSEntity = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAppCMSEntity",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data)
			{
				$scope.EntityColl = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}



	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};
});