
app.controller('DebtorTypeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Debtors Type';

	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			DebtorType: 1,
			CreditorsType:1

		};

		$scope.searchData = {
			DebtorType: '',
			CreditorsType: ''

		};

		$scope.perPage = {
			
			DebtorType: GlobalServices.getPerPageRow(),
			CreditorsType: GlobalServices.getPerPageRow()

		};
		
		
		$scope.newDebtorType = {
			DebtorTypeId: 0,
			Name: '',
			Alias: '',
			Code:'',
			Mode: 'Save'
		};

		$scope.newCreditorsType = {
			CreditorsTypeId: null,
			Name: '',
			Alias: '',
			Code: '',
			Mode: 'Save'
		};

		$scope.GetAllDebtorTypeList();

	}

	$scope.ClearDebtorType = function () {
		$scope.newDebtorType = {
			DebtorTypeId: 0,
			Name: '',
			Alias: '',
			Code: '',
			Mode: 'Save'
		};
		
	};

	$scope.ClearCreditorsType = function () {
		$scope.newCreditorsType = {
			CreditorsTypeId: null,
			Name: '',
			Alias: '',
			Code: '',
			Mode: 'Save'
		};

	};
	function OnClickDefault() {
		document.getElementById('debtor-form').style.display = "none";
		
		// sections display and hide
		document.getElementById('add-debtor').onclick = function () {
			document.getElementById('debtor-section').style.display = "none";
			document.getElementById('debtor-form').style.display = "block";
			$scope.ClearDebtorType();
		}
		document.getElementById('back-to-list').onclick = function () {
			document.getElementById('debtor-form').style.display = "none";
			document.getElementById('debtor-section').style.display = "block";
			$scope.ClearDebtorType();
		}

	


	}


	//************************* Debtors Type *********************************

	$scope.IsValidDebtorType = function () {
		if ($scope.newDebtorType.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateDebtorType = function () {
		if ($scope.IsValidDebtorType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDebtorType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDebtorType();
					}
				});
			} else
				$scope.CallSaveUpdateDebtorType();

		}
	};

	$scope.CallSaveUpdateDebtorType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/SaveDebtorType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newDebtorType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearDebtorType();
				$scope.GetAllDebtorTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});


		
	};

	$scope.GetAllDebtorTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.DebtorTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetAllDebtorTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DebtorTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetDebtorTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			DebtorTypeId: refData.DebtorTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetDebtorTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDebtorType = res.data.Data;
				$scope.newDebtorType.Mode = 'Modify';

				document.getElementById('debtor-section').style.display = "none";
				document.getElementById('debtor-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelDebtorTypeById = function (refData) {

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
					DebtorTypeId: refData.DebtorTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Account/Creation/DelDebtorType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllDebtorTypeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	//************************* Creditors Type *********************************

	$scope.IsValidCreditorsType = function () {
		if ($scope.newCreditorsType.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}



		return true;
	}


	$scope.SaveUpdateCreditorsType = function () {
		if ($scope.IsValidCreditorsType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCreditorsType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCreditorsType();
					}
				});
			} else
				$scope.CallSaveUpdateCreditorsType();

		}
	};

	$scope.CallSaveUpdateCreditorsType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/SaveCreditorsType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newCreditorsType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCreditorsType();
				$scope.GetAllCreditorsTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});


		var photo = $scope.newCreditorsType.Photo_TMP;

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/SaveCreditorsType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.stPhoto && data.stPhoto.length > 0)
					formData.append("photo", data.stPhoto[0]);


				return formData;
			},
			data: { jsonData: $scope.newCreditorsType, stPhoto: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCreditorsType();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

	};

	$scope.GetAllCreditorsTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CreditorsTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetAllCreditorsTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CreditorsTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetCreditorsTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			CreditorsTypeId: refData.CreditorsTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetCreditorsTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCreditorsType = res.data.Data;
				$scope.newCreditorsType.Mode = 'Modify';

				//document.getElementById('class-section').style.display = "none";
				//document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCreditorsTypeById = function (refData) {

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
					CreditorsTypeId: refData.CreditorsTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Account/Creation/DelCreditorsType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCreditorsTypeList();
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