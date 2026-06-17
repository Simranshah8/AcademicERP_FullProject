app.controller('BranchWiseController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Branch Wise';


	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();


		//$scope.currentPages = {
		//	BranchWise: 1

		//};

		//$scope.searchData = {
		//	BranchWise: ''

		//};

		//$scope.perPage = {
		//	BranchWise: GlobalServices.getPerPageRow(),

		//};


		$scope.newBranchWise = {
			BranchWiseId: null,

			Mode: 'Save',
			BranchWiseDetailsColl: []
		};
		$scope.newBranchWise.BranchWiseDetailsColl.push({});

		//$scope.GetAllBranchWiseList();

	}



	$scope.ClearBranchWise = function () {
		$scope.newBranchWise = {
			BranchWiseId: null,

			Mode: 'Save',
			BranchWiseDetailsColl: []
		};
		$scope.newBranchWise.BranchWiseDetailsColl.push({});
	}


	//************************* BranchWise *********************************

	//$scope.IsValidBranchWise = function () {
	//	if ($scope.newBranchWise.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter BranchWise Name');
	//		return false;
	//	}

	//	return true;
	//}

	$scope.SaveUpdateBranchWise = function () {
		if ($scope.IsValidBranchWise() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBranchWise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBranchWise();
					}
				});
			} else
				$scope.CallSaveUpdateBranchWise();

		}
	};

	$scope.CallSaveUpdateBranchWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newPartyWise.ApplicableFromDet) {
			$scope.newPartyWise.ApplicableFrom = $scope.newPartyWise.ApplicableFromDet.dateAD;
		} else
			$scope.newPartyWise.ApplicableFrom = null;

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/SaveBranchWise",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newBranchWise }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBranchWise();
				$scope.GetAllBranchWiseList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllBranchWiseList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BranchWiseList = [];

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetAllBranchWiseList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchWiseList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetBranchWiseById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BranchWiseId: refData.BranchWiseId
		};

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetBranchWiseById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBranchWise = res.data.Data;
				$scope.newBranchWise.Mode = 'Modify';



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBranchWiseById = function (refData) {

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
					BranchWiseId: refData.BranchWiseId
				};

				$http({
					method: 'POST',
					url: base_url + "Inventory/Creation/DelBranchWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBranchWiseList();
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