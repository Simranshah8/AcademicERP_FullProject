

app.controller('ProductBundleController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Product Bundle';


	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();


		$scope.newProductBundle = {
			ProductBundleId: null,

			Mode: 'Save',
			ProductBundleDetailsColl: []
		};
		$scope.newProductBundle.ProductBundleDetailsColl.push({});

		//$scope.GetAllProductBundleList();

	}



	$scope.ClearProductBundle = function () {
		$scope.newProductBundle = {
			ProductBundleId: null,
			Mode: 'Save',
			ProductBundleDetailsColl: []
		};
		$scope.newProductBundle.ProductBundleDetailsColl.push({});
	}


	//************************* ProductBundle *********************************

	//$scope.IsValidProductBundle = function () {
	//	if ($scope.newProductBundle.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter ProductBundle Name');
	//		return false;
	//	}


	//	return true;
	//}

	$scope.SaveUpdateProductBundle = function () {
		if ($scope.IsValidProductBundle() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newProductBundle.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateProductBundle();
					}
				});
			} else
				$scope.CallSaveUpdateProductBundle();

		}
	};

	$scope.CallSaveUpdateProductBundle = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/SaveProductBundle",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newProductBundle }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearProductBundle();
				$scope.GetAllProductBundleList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllProductBundleList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ProductBundleList = [];

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetAllProductBundleList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ProductBundleList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetProductBundleById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ProductBundleId: refData.ProductBundleId
		};

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetProductBundleById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newProductBundle = res.data.Data;
				$scope.newProductBundle.Mode = 'Modify';



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelProductBundleById = function (refData) {

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
					ProductBundleId: refData.ProductBundleId
				};

				$http({
					method: 'POST',
					url: base_url + "Inventory/Creation/DelProductBundle",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllProductBundleList();
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