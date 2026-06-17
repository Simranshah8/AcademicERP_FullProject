app.controller('AllowPayHeadingController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Expense Category';
	
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			AllowPayHeading: 1,
			
		};
		$scope.searchData = {
			AllowPayHeading: '',
			
		};
		$scope.perPage = {
			AllowPayHeading: GlobalServices.getPerPageRow(),
			
		};
		$scope.newAllowPayHeading = {
			AllowPayHeadingId: null,
			BranchId: null,
			DepartmentId: null,			
			Mode: 'Save'
		};
		
		//$scope.GetAllAllowPayHeadingList();
		//$scope.GetAllAllowExpenseCategoryList();
		//$scope.GetAllExpenseCategoryList();
	}

	
	$scope.SaveUpdateAllowPayHeading = function () {
		if ($scope.IsValidAllowPayHeading() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAllowPayHeading.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAllowPayHeading();
					}
				});
			} else
				$scope.CallSaveUpdateAllowPayHeading();
		}
	};

	$scope.CallSaveUpdateAllowPayHeading = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/SaveAllowPayHeading",
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
				$scope.GetAllAllowPayHeadingList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllAllowPayHeadingList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowPayHeadingList = [];

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetAllAllowPayHeadingList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowPayHeadingList = res.data.Data;

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