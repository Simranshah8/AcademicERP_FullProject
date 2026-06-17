

app.controller('AllowPartywiseProductController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow Partywise Product';


	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();


		$scope.newAllowPartywiseProduct = {
			AllowPartywiseProductId: null,

			Mode: 'Save',
			AllowPartywiseProductDetailsColl: []
		};
		$scope.newAllowPartywiseProduct.AllowPartywiseProductDetailsColl.push({});

		//$scope.GetAllAllowPartywiseProductList();

	}



	$scope.ClearAllowPartywiseProduct = function () {
		$scope.newAllowPartywiseProduct = {
			AllowPartywiseProductId: null,
			Mode: 'Save',
			AllowPartywiseProductDetailsColl: []
		};
		$scope.newAllowPartywiseProduct.AllowPartywiseProductDetailsColl.push({});
	}


	//************************* AllowPartywiseProduct *********************************

	//$scope.IsValidAllowPartywiseProduct = function () {
	//	if ($scope.newAllowPartywiseProduct.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter AllowPartywiseProduct Name');
	//		return false;
	//	}


	//	return true;
	//}

	$scope.SaveUpdateAllowPartywiseProduct = function () {
		if ($scope.IsValidAllowPartywiseProduct() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAllowPartywiseProduct.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAllowPartywiseProduct();
					}
				});
			} else
				$scope.CallSaveUpdateAllowPartywiseProduct();

		}
	};

	$scope.CallSaveUpdateAllowPartywiseProduct = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/SaveAllowPartywiseProduct",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newAllowPartywiseProduct }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAllowPartywiseProduct();
				$scope.GetAllAllowPartywiseProductList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllAllowPartywiseProductList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowPartywiseProductList = [];

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetAllAllowPartywiseProductList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowPartywiseProductList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAllowPartywiseProductById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AllowPartywiseProductId: refData.AllowPartywiseProductId
		};

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetAllowPartywiseProductById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAllowPartywiseProduct = res.data.Data;
				$scope.newAllowPartywiseProduct.Mode = 'Modify';



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAllowPartywiseProductById = function (refData) {

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
					AllowPartywiseProductId: refData.AllowPartywiseProductId
				};

				$http({
					method: 'POST',
					url: base_url + "Inventory/Creation/DelAllowPartywiseProduct",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAllowPartywiseProductList();
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