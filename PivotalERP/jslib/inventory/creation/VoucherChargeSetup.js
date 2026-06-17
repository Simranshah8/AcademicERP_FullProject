

app.controller('VoucherChargeSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Voucher Charge Setup';


	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();


		$scope.newVoucherChargeSetup = {
			VoucherChargeSetupId: null,

			Mode: 'Save',
			VoucherChargeSetupDetailsColl: []
		};
		$scope.newVoucherChargeSetup.VoucherChargeSetupDetailsColl.push({});

		//$scope.GetAllVoucherChargeSetupList();

	}



	$scope.ClearVoucherChargeSetup = function () {
		$scope.newVoucherChargeSetup = {
			VoucherChargeSetupId: null,
			Mode: 'Save',
			VoucherChargeSetupDetailsColl: []
		};
		$scope.newVoucherChargeSetup.VoucherChargeSetupDetailsColl.push({});
	}


	//************************* VoucherChargeSetup *********************************

	//$scope.IsValidVoucherChargeSetup = function () {
	//	if ($scope.newVoucherChargeSetup.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter VoucherChargeSetup Name');
	//		return false;
	//	}


	//	return true;
	//}

	$scope.SaveUpdateVoucherChargeSetup = function () {
		if ($scope.IsValidVoucherChargeSetup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newVoucherChargeSetup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateVoucherChargeSetup();
					}
				});
			} else
				$scope.CallSaveUpdateVoucherChargeSetup();

		}
	};

	$scope.CallSaveUpdateVoucherChargeSetup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/SaveVoucherChargeSetup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newVoucherChargeSetup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearVoucherChargeSetup();
				$scope.GetAllVoucherChargeSetupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllVoucherChargeSetupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.VoucherChargeSetupList = [];

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetAllVoucherChargeSetupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.VoucherChargeSetupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetVoucherChargeSetupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			VoucherChargeSetupId: refData.VoucherChargeSetupId
		};

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetVoucherChargeSetupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newVoucherChargeSetup = res.data.Data;
				$scope.newVoucherChargeSetup.Mode = 'Modify';



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelVoucherChargeSetupById = function (refData) {

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
					VoucherChargeSetupId: refData.VoucherChargeSetupId
				};

				$http({
					method: 'POST',
					url: base_url + "Inventory/Creation/DelVoucherChargeSetup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllVoucherChargeSetupList();
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