

app.controller('SalesRateTypeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Purchase Rate Type';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();


		$scope.newSalesRateType = {
			SalesRateTypeId: null,

			Mode: 'Save',
			SalesRateTypeDetailsColl: []
		};
		$scope.newSalesRateType.SalesRateTypeDetailsColl.push({});

		//$scope.GetAllSalesRateTypeList();

	}



	$scope.ClearSalesRateType = function () {
		$scope.newSalesRateType = {
			SalesRateTypeId: null,
			Mode: 'Save',
			SalesRateTypeDetailsColl: []
		};
		$scope.newSalesRateType.SalesRateTypeDetailsColl.push({});
	}


	//************************* SalesRateType *********************************

	//$scope.IsValidSalesRateType = function () {
	//	if ($scope.newSalesRateType.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter SalesRateType Name');
	//		return false;
	//	}


	//	return true;
	//}

	$scope.SaveUpdateSalesRateType = function () {
		if ($scope.IsValidSalesRateType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSalesRateType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSalesRateType();
					}
				});
			} else
				$scope.CallSaveUpdateSalesRateType();

		}
	};

	$scope.CallSaveUpdateSalesRateType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();


		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/SaveSalesRateType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSalesRateType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearSalesRateType();
				$scope.GetAllSalesRateTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllSalesRateTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SalesRateTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetAllSalesRateTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SalesRateTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetSalesRateTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			SalesRateTypeId: refData.SalesRateTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetSalesRateTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSalesRateType = res.data.Data;
				$scope.newSalesRateType.Mode = 'Modify';



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelSalesRateTypeById = function (refData) {

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
					SalesRateTypeId: refData.SalesRateTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Inventory/Creation/DelSalesRateType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllSalesRateTypeList();
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