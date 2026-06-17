
app.controller('DebtorRouteController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Debtor Route';

	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			DebtorRoute: 1,
			CreditorsRoute: 1

		};

		$scope.searchData = {
			DebtorRoute: '',
			CreditorsRoute: ''

		};

		$scope.perPage = {

			DebtorRoute: GlobalServices.getPerPageRow(),
			CreditorsRoute: GlobalServices.getPerPageRow()

		};


		$scope.newDebtorRoute = {
			DebtorRouteId: 0,
			Name: '',
			Alias: '',
			Code: '',
			Mode: 'Save'
		};

		$scope.newCreditorsRoute = {
			CreditorsRouteId: null,
			Name: '',
			Alias: '',
			Code: '',
			Mode: 'Save'
		};

		$scope.GetAllDebtorRouteList();
	}

	$scope.ClearDebtorRoute = function () {
		$scope.newDebtorRoute = {
			DebtorRouteId: 0,
			Name: '',
			Alias: '',
			Code: '',
			Mode: 'Save'
		};

	};

	$scope.ClearCreditorsRoute = function () {
		$scope.newCreditorsRoute = {
			CreditorsRouteId: null,
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
			$scope.ClearDebtorRoute();
		}
		document.getElementById('back-to-list').onclick = function () {
			document.getElementById('debtor-form').style.display = "none";
			document.getElementById('debtor-section').style.display = "block";
			$scope.ClearDebtorRoute();
		}

	


	}


	//************************* Debtors Type *********************************

	$scope.IsValidDebtorRoute = function () {
		if ($scope.newDebtorRoute.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateDebtorRoute = function () {
		if ($scope.IsValidDebtorRoute() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDebtorRoute.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDebtorRoute();
					}
				});
			} else
				$scope.CallSaveUpdateDebtorRoute();

		}
	};

	$scope.CallSaveUpdateDebtorRoute = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/SaveDebtorRoute",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newDebtorRoute }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearDebtorRoute();
				$scope.GetAllDebtorRouteList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});


		
	};

	$scope.GetAllDebtorRouteList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.DebtorRouteList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetAllDebtorRouteList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DebtorRouteList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetDebtorRouteById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			DebtorRouteId: refData.DebtorRouteId
		};

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetDebtorRouteById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDebtorRoute = res.data.Data;
				$scope.newDebtorRoute.Mode = 'Modify';
				document.getElementById('debtor-section').style.display = "none";
				document.getElementById('debtor-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelDebtorRouteById = function (refData) {

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
					DebtorRouteId: refData.DebtorRouteId
				};

				$http({
					method: 'POST',
					url: base_url + "Account/Creation/DelDebtorRoute",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllDebtorRouteList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	//************************* Creditors Route *********************************

	$scope.IsValidCreditorsRoute = function () {
		if ($scope.newCreditorsRoute.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}



		return true;
	}


	$scope.SaveUpdateCreditorsRoute = function () {
		if ($scope.IsValidCreditorsRoute() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCreditorsRoute.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCreditorsRoute();
					}
				});
			} else
				$scope.CallSaveUpdateCreditorsRoute();

		}
	};

	$scope.CallSaveUpdateCreditorsRoute = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/SaveCreditorsRoute",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newCreditorsRoute }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCreditorsRoute();
				$scope.GetAllCreditorsRouteList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});


		var photo = $scope.newCreditorsRoute.Photo_TMP;

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/SaveCreditorsRoute",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.stPhoto && data.stPhoto.length > 0)
					formData.append("photo", data.stPhoto[0]);


				return formData;
			},
			data: { jsonData: $scope.newCreditorsRoute, stPhoto: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCreditorsRoute();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

	};

	$scope.GetAllCreditorsRouteList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CreditorsRouteList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetAllCreditorsRouteList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CreditorsRouteList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetCreditorsRouteById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			CreditorsRouteId: refData.CreditorsRouteId
		};

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetCreditorsRouteById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCreditorsRoute = res.data.Data;
				$scope.newCreditorsRoute.Mode = 'Modify';

				//document.getElementById('class-section').style.display = "none";
				//document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCreditorsRouteById = function (refData) {

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
					CreditorsRouteId: refData.CreditorsRouteId
				};

				$http({
					method: 'POST',
					url: base_url + "Account/Creation/DelCreditorsRoute",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCreditorsRouteList();
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