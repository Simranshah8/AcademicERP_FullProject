

app.controller('PurchaseRateTypeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Purchase Rate Type';



	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();


		$scope.newPurchaseRateType = {
			PurchaseRateTypeId: null,

			Mode: 'Save',
			PurchaseRateTypeDetailsColl: []
		};
		$scope.newPurchaseRateType.PurchaseRateTypeDetailsColl.push({});

		//$scope.GetAllPurchaseRateTypeList();

	}



	$scope.ClearPurchaseRateType = function () {
		$scope.newPurchaseRateType = {
			PurchaseRateTypeId: null,
			Mode: 'Save',
			PurchaseRateTypeDetailsColl: []
		};
		$scope.newPurchaseRateType.PurchaseRateTypeDetailsColl.push({});
	}


	//************************* PurchaseRateType *********************************

	//$scope.IsValidPurchaseRateType = function () {
	//	if ($scope.newPurchaseRateType.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter PurchaseRateType Name');
	//		return false;
	//	}


	//	return true;
	//}

	$scope.SaveUpdatePurchaseRateType = function () {
		if ($scope.IsValidPurchaseRateType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newPurchaseRateType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdatePurchaseRateType();
					}
				});
			} else
				$scope.CallSaveUpdatePurchaseRateType();

		}
	};

	$scope.CallSaveUpdatePurchaseRateType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/SavePurchaseRateType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newPurchaseRateType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearPurchaseRateType();
				$scope.GetAllPurchaseRateTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllPurchaseRateTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PurchaseRateTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetAllPurchaseRateTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PurchaseRateTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetPurchaseRateTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			PurchaseRateTypeId: refData.PurchaseRateTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetPurchaseRateTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newPurchaseRateType = res.data.Data;
				$scope.newPurchaseRateType.Mode = 'Modify';



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelPurchaseRateTypeById = function (refData) {

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
					PurchaseRateTypeId: refData.PurchaseRateTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Inventory/Creation/DelPurchaseRateType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllPurchaseRateTypeList();
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