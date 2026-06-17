$(document).on('keyup', '.serial', function (e) {
	if (e.which == 13) {
		var $this = $(this);
		var $td = $this.closest('td');
		var $row = $td.closest('tr');
		var $rows = $row.parent();
		var column = $td.index();


		while ($td.length) {
			$row = $row.next('tr');
			if ($row.length == 0) {
				$row = $rows.children().first();
				column++;
			}

			$td = $row.children().eq(column);
			var $input = $td.find('.serial');
			if ($input.length) {
				$input.focus();
				break;
			}
		}
	}
});
app.controller('SalarySheetController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Salary Sheet';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			SalarySheet: 1,

		};
		$scope.searchData = {
			SalarySheet: '',

		};
		$scope.perPage = {
			SalarySheet: GlobalServices.getPerPageRow(),

		};
		$scope.newSalarySheet = {
			SalarySheetId: null,
			BranchId: null,
			DepartmentId: null,
			Mode: 'Save'
		};

		//$scope.GetAllSalarySheetList();
		//$scope.GetAllAllowExpenseCategoryList();
		//$scope.GetAllExpenseCategoryList();
	}


	$scope.SaveUpdateSalarySheet = function () {
		if ($scope.IsValidSalarySheet() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSalarySheet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSalarySheet();
					}
				});
			} else
				$scope.CallSaveUpdateSalarySheet();
		}
	};

	$scope.CallSaveUpdateSalarySheet = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/SaveSalarySheet",
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
				$scope.GetAllSalarySheetList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllSalarySheetList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SalarySheetList = [];

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetAllSalarySheetList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SalarySheetList = res.data.Data;

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