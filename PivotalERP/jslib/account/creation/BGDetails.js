app.controller('BGDetailsController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'BG Details';

	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			BGDetails: 1

		};

		$scope.searchData = {
			BGDetails: ''

		};

		$scope.perPage = {
			BGDetails: GlobalServices.getPerPageRow(),

		};

		$scope.newBGDetails = {
			BGDetailsId: null,
			PartyId: null,
			BankName: '',
			BranchName: '',
			BGNo: null,
			IssueDate_TMP: null,
			ExpiredDate_TMP: null,
			Remarks: '',
			Status: true,
			Mode: 'Save'
		};

		//$scope.GetAllBGDetailsList();

	}



	$scope.ClearBGDetails = function () {
		$scope.newBGDetails = {
			BGDetailsId: null,
			PartyId: null,
			BankName: '',
			BranchName: '',
			BGNo: null,
			IssueDate_TMP: null,
			ExpiredDate_TMP: null,
			Remarks: '',
			Status: true,
			Mode: 'Save'
		};
	}

	function OnClickDefault() {
		document.getElementById('bg-details-form').style.display = "none";

		// sections display and hide
		document.getElementById('add-bg-details').onclick = function () {
			document.getElementById('bg-details-section').style.display = "none";
			document.getElementById('bg-details-form').style.display = "block";
		}
		document.getElementById('back-to-list').onclick = function () {
			document.getElementById('bg-details-form').style.display = "none";
			document.getElementById('bg-details-section').style.display = "block";
		}

	}

	//************************* BGDetails *********************************

	$scope.IsValidBGDetails = function () {
		if ($scope.newBGDetails.BankName.isEmpty()) {
			Swal.fire('Please ! Enter Bank Name');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateBGDetails = function () {
		if ($scope.IsValidBGDetails() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBGDetails.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBGDetails();
					}
				});
			} else
				$scope.CallSaveUpdateBGDetails();

		}
	};

	$scope.CallSaveUpdateBGDetails = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newBGDetails.IssueDateDet) {
			$scope.newBGDetails.IssueDate = $scope.newBGDetails.IssueDateDet.dateAD;
		} else
			$scope.newBGDetails.IssueDate = null;

		if ($scope.newBGDetails.ExpiredDateDet) {
			$scope.newBGDetails.ExpiredDate = $scope.newBGDetails.ExpiredDateDet.dateAD;
		} else
			$scope.newBGDetails.ExpiredDate = null;

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/SaveBGDetails",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newBGDetails }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBGDetails();
				$scope.GetAllBGDetailsList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllBGDetailsList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BGDetailsList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetAllBGDetailsList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BGDetailsList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetBGDetailsById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BGDetailsId: refData.BGDetailsId
		};

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetBGDetailsById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBGDetails = res.data.Data;
				$scope.newBGDetails.Mode = 'Modify';

				

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBGDetailsById = function (refData) {

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
					BGDetailsId: refData.BGDetailsId
				};

				$http({
					method: 'POST',
					url: base_url + "Account/Creation/DelBGDetails",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBGDetailsList();
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