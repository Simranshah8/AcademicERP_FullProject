app.controller('MandatoryDisclosureController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Mandatory Disclosure';

	$timeout(function () {
		OnClickDefault();
	}, 0);
	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			AddMandatoryDisclosure: 1,
		};

		$scope.searchData = {
			AddMandatoryDisclosure: '',
		};

		$scope.perPage = {
			AddMandatoryDisclosure: GlobalServices.getPerPageRow(),
		};

		$scope.GetAllMandatoryDisclosure();

		$scope.MandatoryDisclosureData = {
			TranId:'',
			Title: '',
			OrderNo: 0,
			Description: '',
			Mode:'Save'
		};
	}

	function OnClickDefault() {
		document.getElementById('mandatory-disclosure-form').style.display = "none";

		document.getElementById('add-mandatory-disclosure').onclick = function () {
			document.getElementById('mandatory-disclosure-table').style.display = "none";
			document.getElementById('mandatory-disclosure-form').style.display = "block";
		}
		document.getElementById('mandatory-disclosure-back-btn').onclick = function () {
			document.getElementById('mandatory-disclosure-form').style.display = "none";
			document.getElementById('mandatory-disclosure-table').style.display = "block";
		}
	}


	$scope.ClearDetails = function () {
		$scope.MandatoryDisclosureData = {
			Title: '',
			OrderNo: 0,
			Description: '',
			Mode:'Save'
		}
	}

	$scope.IsValidAddMandatoryDisclosure = function () {
		return true;
	}

	$scope.SaveUpdateAddDetails = function () {
		if ($scope.IsValidAddMandatoryDisclosure() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.MandatoryDisclosureData.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDetails();
					}
				});
			} else
				$scope.CallSaveUpdateDetails();
		}
	};

	$scope.CallSaveUpdateDetails = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveMandatoryDisclosure",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.MandatoryDisclosureData}
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearDetails();
				$scope.GetAllMandatoryDisclosure();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllMandatoryDisclosure = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.MandatoryDisclosureDataList = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetMandatoryDisclosures",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.MandatoryDisclosureDataList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetMandatoryDisclosureById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetMandatoryDisclosureById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.MandatoryDisclosureData = res.data.Data;

				$scope.MandatoryDisclosureData.Mode = 'Modify';

				document.getElementById('mandatory-disclosure-table').style.display = "none";
				document.getElementById('mandatory-disclosure-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DeleteMandatoryDisclosure = function (refData) {
		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					TranId: refData.TranId
				};
				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DeleteMandatoryDisclosure",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.ClearDetails();
						$scope.GetAllMandatoryDisclosure();
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