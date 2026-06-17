app.controller('PartyWiseController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Party Wise';

	
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();


		//$scope.currentPages = {
		//	PartyWise: 1

		//};

		//$scope.searchData = {
		//	PartyWise: ''

		//};

		//$scope.perPage = {
		//	PartyWise: GlobalServices.getPerPageRow(),

		//};


		$scope.newPartyWise = {
			PartyWiseId: null,

			Mode: 'Save',
			PartyWiseDetailsColl: []
		};
		$scope.newPartyWise.PartyWiseDetailsColl.push({});

		//$scope.GetAllPartyWiseList();

	}

	

	$scope.ClearPartyWise = function () {
		$scope.newPartyWise = {
			PartyWiseId: null,

			Mode: 'Save',
			PartyWiseDetailsColl: []
		};
		$scope.newPartyWise.PartyWiseDetailsColl.push({});
	}


	//************************* PartyWise *********************************

	//$scope.IsValidPartyWise = function () {
	//	if ($scope.newPartyWise.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter PartyWise Name');
	//		return false;
	//	}

	//	return true;
	//}

	$scope.SaveUpdatePartyWise = function () {
		if ($scope.IsValidPartyWise() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newPartyWise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdatePartyWise();
					}
				});
			} else
				$scope.CallSaveUpdatePartyWise();

		}
	};

	$scope.CallSaveUpdatePartyWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newPartyWise.ApplicableFromDet) {
			$scope.newPartyWise.ApplicableFrom = $scope.newPartyWise.ApplicableFromDet.dateAD;
		} else
			$scope.newPartyWise.ApplicableFrom = null;

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/SavePartyWise",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newPartyWise }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearPartyWise();
				$scope.GetAllPartyWiseList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllPartyWiseList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PartyWiseList = [];

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetAllPartyWiseList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PartyWiseList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetPartyWiseById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			PartyWiseId: refData.PartyWiseId
		};

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetPartyWiseById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newPartyWise = res.data.Data;
				$scope.newPartyWise.Mode = 'Modify';



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelPartyWiseById = function (refData) {

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
					PartyWiseId: refData.PartyWiseId
				};

				$http({
					method: 'POST',
					url: base_url + "Inventory/Creation/DelPartyWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllPartyWiseList();
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