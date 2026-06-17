app.controller('SalaryDetailController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Salary Detail';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			SalaryDetail: 1,

		};
		$scope.searchData = {
			SalaryDetail: '',

		};
		$scope.perPage = {
			SalaryDetail: GlobalServices.getPerPageRow(),

		};
		$scope.newSalaryDetail = {
			SalaryDetailId: null,
			BranchId: null,
			DepartmentId: null,
			Mode: 'Save'
		};

		//$scope.GetAllSalaryDetailList();
		//$scope.GetAllAllowExpenseCategoryList();
		//$scope.GetAllExpenseCategoryList();
	}


	$scope.SaveUpdateSalaryDetail = function () {
		if ($scope.IsValidSalaryDetail() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSalaryDetail.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSalaryDetail();
					}
				});
			} else
				$scope.CallSaveUpdateSalaryDetail();
		}
	};

	$scope.CallSaveUpdateSalaryDetail = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/SaveSalaryDetail",
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
				$scope.GetAllSalaryDetailList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllSalaryDetailList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SalaryDetailList = [];

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetAllSalaryDetailList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SalaryDetailList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetSalaryDetailById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			SalaryDetailId: refData.SalaryDetailId
		};
		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetSalaryDetailById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSalaryDetail = res.data.Data;
				$scope.newSalaryDetail.Mode = 'Modify';
				document.getElementById('SalaryDetaillist').style.display = "none";
				document.getElementById('SalaryDetail-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelSalaryDetailById = function (refData) {
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
					SalaryDetailId: refData.SalaryDetailId
				};

				$http({
					method: 'POST',
					url: base_url + "HRM/Creation/DelSalaryDetail",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllSalaryDetailList();
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