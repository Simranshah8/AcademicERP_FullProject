app.controller('CurrencyRateController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Currency Rate';



	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();


		//$scope.currentPages = {
		//	CurrencyRate: 1

		//};

		//$scope.searchData = {
		//	CurrencyRate: ''

		//};

		//$scope.perPage = {
		//	CurrencyRate: GlobalServices.getPerPageRow(),

		//};

		$scope.newCurrencyRate = {
			CurrencyRateId: null,
			CurrencyRateDetailsColl:[]
		};
		$scope.newCurrencyRate.CurrencyRateDetailsColl.push({});

		//$scope.GetAllCurrencyRateList();

	}



	$scope.ClearCurrencyRate = function () {
		$scope.newCurrencyRate = {
			CurrencyRateId: null,
			CurrencyRateDetailsColl: []
		};
		$scope.newCurrencyRate.CurrencyRateDetailsColl.push({});
	}


	//************************* CurrencyRate *********************************

	

	$scope.SaveUpdateCurrencyRate = function () {
		if ($scope.IsValidCurrencyRate() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCurrencyRate.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCurrencyRate();
					}
				});
			} else
				$scope.CallSaveUpdateCurrencyRate();

		}
	};

	$scope.CallSaveUpdateCurrencyRate = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newBGDetails.DateOfRateExchangeDet) {
			$scope.newBGDetails.DateOfRateExchange = $scope.newBGDetails.DateOfRateExchangeDet.dateAD;
		} else
			$scope.newBGDetails.DateOfRateExchange = null;


		$http({
			method: 'POST',
			url: base_url + "Account/Creation/SaveCurrencyRate",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newCurrencyRate }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCurrencyRate();
				$scope.GetAllCurrencyRateList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllCurrencyRateList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CurrencyRateList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetAllCurrencyRateList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CurrencyRateList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetCurrencyRateById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			CurrencyRateId: refData.CurrencyRateId
		};

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetCurrencyRateById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCurrencyRate = res.data.Data;
				$scope.newCurrencyRate.Mode = 'Modify';

				

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCurrencyRateById = function (refData) {

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
					CurrencyRateId: refData.CurrencyRateId
				};

				$http({
					method: 'POST',
					url: base_url + "Account/Creation/DelCurrencyRate",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCurrencyRateList();
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