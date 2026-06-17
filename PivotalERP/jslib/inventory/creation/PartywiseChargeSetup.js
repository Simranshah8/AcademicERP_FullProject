

app.controller('PartywiseChargeSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Partywise Charge Setup';


	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();


		$scope.newPartywiseChargeSetup = {
			PartywiseChargeSetupId: null,

			Mode: 'Save',
			PartywiseChargeSetupDetailsColl: []
		};
		$scope.newPartywiseChargeSetup.PartywiseChargeSetupDetailsColl.push({});

		//$scope.GetAllPartywiseChargeSetupList();

	}



	$scope.ClearPartywiseChargeSetup = function () {
		$scope.newPartywiseChargeSetup = {
			PartywiseChargeSetupId: null,
			Mode: 'Save',
			PartywiseChargeSetupDetailsColl: []
		};
		$scope.newPartywiseChargeSetup.PartywiseChargeSetupDetailsColl.push({});
	}


	//************************* PartywiseChargeSetup *********************************

	//$scope.IsValidPartywiseChargeSetup = function () {
	//	if ($scope.newPartywiseChargeSetup.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter PartywiseChargeSetup Name');
	//		return false;
	//	}


	//	return true;
	//}

	$scope.SaveUpdatePartywiseChargeSetup = function () {
		if ($scope.IsValidPartywiseChargeSetup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newPartywiseChargeSetup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdatePartywiseChargeSetup();
					}
				});
			} else
				$scope.CallSaveUpdatePartywiseChargeSetup();

		}
	};

	$scope.CallSaveUpdatePartywiseChargeSetup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/SavePartywiseChargeSetup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newPartywiseChargeSetup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearPartywiseChargeSetup();
				$scope.GetAllPartywiseChargeSetupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllPartywiseChargeSetupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PartywiseChargeSetupList = [];

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetAllPartywiseChargeSetupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PartywiseChargeSetupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetPartywiseChargeSetupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			PartywiseChargeSetupId: refData.PartywiseChargeSetupId
		};

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetPartywiseChargeSetupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newPartywiseChargeSetup = res.data.Data;
				$scope.newPartywiseChargeSetup.Mode = 'Modify';



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelPartywiseChargeSetupById = function (refData) {

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
					PartywiseChargeSetupId: refData.PartywiseChargeSetupId
				};

				$http({
					method: 'POST',
					url: base_url + "Inventory/Creation/DelPartywiseChargeSetup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllPartywiseChargeSetupList();
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