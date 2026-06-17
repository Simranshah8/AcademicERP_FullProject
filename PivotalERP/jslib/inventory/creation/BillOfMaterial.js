
app.controller('BillOfMaterialController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Bill Of Material';



	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();


		$scope.newBillOfMaterial = {
			BillOfMaterialId: null,
			Mode:'Save',
			BillOfMaterialDetailsColl:[]
		};
		$scope.newBillOfMaterial.BillOfMaterialDetailsColl.push({});

		//$scope.GetAllBillOfMaterialList();

	}

	$scope.ClearBillOfMaterial = function () {
		$scope.newBillOfMaterial = {
			BillOfMaterialId: null,
			Mode: 'Save',
			BillOfMaterialDetailsColl: []
		};
		$scope.newBillOfMaterial.BillOfMaterialDetailsColl.push({});
	}


	//************************* BillOfMaterial *********************************

	$scope.IsValidBillOfMaterial = function () {
		if ($scope.newBillOfMaterial.Name.isEmpty()) {
			Swal.fire('Please ! Enter BillOfMaterial Name');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateBillOfMaterial = function () {
		if ($scope.IsValidBillOfMaterial() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBillOfMaterial.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBillOfMaterial();
					}
				});
			} else
				$scope.CallSaveUpdateBillOfMaterial();

		}
	};

	$scope.CallSaveUpdateBillOfMaterial = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/SaveBillOfMaterial",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newBillOfMaterial }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBillOfMaterial();
				$scope.GetAllBillOfMaterialList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllBillOfMaterialList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BillOfMaterialList = [];

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetAllBillOfMaterialList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BillOfMaterialList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetBillOfMaterialById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BillOfMaterialId: refData.BillOfMaterialId
		};

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetBillOfMaterialById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBillOfMaterial = res.data.Data;
				$scope.newBillOfMaterial.Mode = 'Modify';



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBillOfMaterialById = function (refData) {

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
					BillOfMaterialId: refData.BillOfMaterialId
				};

				$http({
					method: 'POST',
					url: base_url + "Inventory/Creation/DelBillOfMaterial",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBillOfMaterialList();
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