app.controller('TaxCalculatorController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Tax Calculator';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			TaxCalculator: 1,

		};
		$scope.searchData = {
			TaxCalculator: '',

		};
		$scope.perPage = {
			TaxCalculator: GlobalServices.getPerPageRow(),

		};
		$scope.newTaxCalculator = {
			TaxCalculatorId: null,
			BranchId: null,
			DepartmentId: null,
			Mode: 'Save'
		};

		//$scope.GetAllTaxCalculatorList();
		//$scope.GetAllAllowExpenseCategoryList();
		//$scope.GetAllExpenseCategoryList();
	}


	$scope.SaveUpdateTaxCalculator = function () {
		if ($scope.IsValidTaxCalculator() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newTaxCalculator.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateTaxCalculator();
					}
				});
			} else
				$scope.CallSaveUpdateTaxCalculator();
		}
	};

	$scope.CallSaveUpdateTaxCalculator = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/SaveTaxCalculator",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newSetup }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearSetup();
				$scope.GetAllTaxCalculatorList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllTaxCalculatorList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TaxCalculatorList = [];

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetAllTaxCalculatorList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TaxCalculatorList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}




	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});