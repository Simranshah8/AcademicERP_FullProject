app.controller('ArrearSalarySheetController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Salary Sheet';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			ArrearSalarySheet: 1,

		};
		$scope.searchData = {
			ArrearSalarySheet: '',

		};
		$scope.perPage = {
			ArrearSalarySheet: GlobalServices.getPerPageRow(),

		};
		$scope.newArrearSalarySheet = {
			ArrearSalarySheetId: null,
			BranchId: null,
			DepartmentId: null,
			Mode: 'Save'
		};

		//$scope.GetAllArrearSalarySheetList();
		//$scope.GetAllAllowExpenseCategoryList();
		//$scope.GetAllExpenseCategoryList();
	}


	$scope.SaveUpdateArrearSalarySheet = function () {
		if ($scope.IsValidArrearSalarySheet() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newArrearSalarySheet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateArrearSalarySheet();
					}
				});
			} else
				$scope.CallSaveUpdateArrearSalarySheet();
		}
	};

	$scope.CallSaveUpdateArrearSalarySheet = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/SaveArrearSalarySheet",
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
				$scope.GetAllArrearSalarySheetList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllArrearSalarySheetList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ArrearSalarySheetList = [];

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetAllArrearSalarySheetList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ArrearSalarySheetList = res.data.Data;

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