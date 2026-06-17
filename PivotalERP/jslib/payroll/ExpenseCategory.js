app.controller('ExpenseCategoryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Expense Category';
	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			ExpenseRateSetup: 1,
			AllowExpenseCategory: 1,
			ExpenseCategory: 1,
			ExpenseGroup:1
		};
		$scope.searchData = {
			ExpenseRateSetup: '',
			AllowExpenseCategory: '',
			ExpenseCategory: '',
			ExpenseGroup:''
		};
		$scope.perPage = {
			ExpenseRateSetup: GlobalServices.getPerPageRow(),
			AllowExpenseCategory: GlobalServices.getPerPageRow(),
			ExpenseCategory: GlobalServices.getPerPageRow(),
			ExpenseGroup: GlobalServices.getPerPageRow(),
		};
		$scope.newExpenseRateSetup = {
			ExpenseRateSetupId: null,			
			BranchId: null,
			DepartmentId: null,
			CategoryId: null,
			ApplicableFrom_TMP: '',			
			Mode: 'Save'
		};
		$scope.newAllowExpenseCategory = {
			AllowExpenseCategoryId: null,
			BranchId: null,
			DepartmentId: null,
			CategoryId: null,
			Mode: 'Save'
		};
		$scope.newExpenseCategory = {
			ExpenseCategoryId: null,
			Name: '',
			GroupNameId: null,
			SNo: '',
			CanEdit:false,
			Description: '',
			Mode: 'Save'
		};
		$scope.newExpenseGroup = {
			ExpenseGroupId: null,
			Name: '',
			GroupNameId: null,
			SNo: '',
			CanEdit: false,
			Description: '',
			Mode: 'Save'
		};

		//$scope.GetAllExpenseRateSetupList();
		//$scope.GetAllAllowExpenseCategoryList();
		//$scope.GetAllExpenseCategoryList();
	}

	$scope.ClearExpenseRateSetup = function () {
		$scope.newExpenseRateSetup = {
			ExpenseRateSetupId: null,
			BranchId: null,
			DepartmentId: null,
			CategoryId: null,
			ApplicableFrom_TMP: '',
			Mode: 'Save'			
		};		
	}

	$scope.ClearAllowExpenseCategory = function () {
		$scope.newAllowExpenseCategory = {
			AllowExpenseCategoryId: null,
			BranchId: null,
			DepartmentId: null,
			CategoryId: null,
			Mode: 'Save'
		};
	}

	$scope.ClearExpenseCategory = function () {
		$scope.newExpenseCategory = {
			ExpenseCategoryId: null,
			Name: '',
			GroupNameId: null,
			SNo: '',
			CanEdit: false,
			Description: '',
			Mode: 'Save'
		};
	}

	$scope.ClearExpenseGroup = function () {
		$scope.newExpenseGroup = {
			ExpenseGroupId: null,
			Name: '',
			GroupNameId: null,
			SNo: '',
			CanEdit: false,
			Description: '',
			Mode: 'Save'
		};
	}
	function OnClickDefault() {
		document.getElementById('ECForm').style.display = "none";
		document.getElementById('EGForm').style.display = "none";		

		document.getElementById('add-expensecategory').onclick = function () {
			document.getElementById('ECSection').style.display = "none";
			document.getElementById('ECForm').style.display = "block";
			$scope.ClearVehicleDetails();
		}
		document.getElementById('ECback-btn').onclick = function () {
			document.getElementById('ECForm').style.display = "none";
			document.getElementById('ECSection').style.display = "block";
			$scope.ClearVehicleDetails();
		}
		document.getElementById('add-EG').onclick = function () {
			document.getElementById('EGSection').style.display = "none";
			document.getElementById('EGForm').style.display = "block";
			$scope.ClearVehicleDetails();
		}
		document.getElementById('EGback-btn').onclick = function () {
			document.getElementById('EGForm').style.display = "none";
			document.getElementById('EGSection').style.display = "block";
			$scope.ClearVehicleDetails();
		}
	};
	//*************************Pay Heading *********************************
	$scope.SaveUpdateExpenseRateSetup = function () {
		if ($scope.IsValidExpenseRateSetup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExpenseRateSetup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExpenseRateSetup();
					}
				});
			} else
				$scope.CallSaveUpdateExpenseRateSetup();
		}
	};

	$scope.CallSaveUpdateExpenseRateSetup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/SaveExpenseRateSetup",
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
				$scope.GetAllExpenseRateSetupList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllExpenseRateSetupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExpenseRateSetupList = [];

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetAllExpenseRateSetupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExpenseRateSetupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExpenseRateSetupById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			ExpenseRateSetupId: refData.ExpenseRateSetupId
		};
		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetExpenseRateSetupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExpenseRateSetup = res.data.Data;
				$scope.newExpenseRateSetup.Mode = 'Modify';
				document.getElementById('ExpenseRateSetuplist').style.display = "none";
				document.getElementById('ExpenseRateSetup-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExpenseRateSetupById = function (refData) {
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
					ExpenseRateSetupId: refData.ExpenseRateSetupId
				};

				$http({
					method: 'POST',
					url: base_url + "HRM/Creation/DelExpenseRateSetup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExpenseRateSetupList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* AllowExpenseCategory Route *********************************

	$scope.SaveUpdateAllowExpenseCategory = function () {
		if ($scope.IsValidAllowExpenseCategory() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAllowExpenseCategory.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAllowExpenseCategory();
					}
				});
			} else
				$scope.CallSaveUpdateAllowExpenseCategory();

		}
	};

	$scope.CallSaveUpdateAllowExpenseCategory = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/SaveAllowExpenseCategory",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newAllowExpenseCategory }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAllowExpenseCategory();
				$scope.GetAllAllowExpenseCategoryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllAllowExpenseCategoryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowExpenseCategoryList = [];

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetAllAllowExpenseCategoryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowExpenseCategoryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAllowExpenseCategoryById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			AllowExpenseCategoryId: refData.AllowExpenseCategoryId
		};
		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetAllowExpenseCategoryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAllowExpenseCategory = res.data.Data;
				$scope.newAllowExpenseCategory.Mode = 'Modify';

				document.getElementById('phg-section').style.display = "none";
				document.getElementById('phg-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAllowExpenseCategoryById = function (refData) {
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
					AllowExpenseCategoryId: refData.AllowExpenseCategoryId
				};

				$http({
					method: 'POST',
					url: base_url + "HRM/Creation/DelAllowExpenseCategory",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAllowExpenseCategoryList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};




	//*************************ExpenseCategory *********************************
	$scope.IsValidExpenseCategory = function () {
		if ($scope.newExpenseCategory.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}
		return true;
	}


	$scope.SaveUpdateExpenseCategory = function () {
		if ($scope.IsValidExpenseCategory() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExpenseCategory.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExpenseCategory();
					}
				});
			} else
				$scope.CallSaveUpdateExpenseCategory();

		}
	};

	$scope.CallSaveUpdateExpenseCategory = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/SaveExpenseCategory",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExpenseCategory }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExpenseCategory();
				$scope.GetAllExpenseCategoryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllExpenseCategoryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExpenseCategoryList = [];

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetAllExpenseCategoryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExpenseCategoryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExpenseCategoryById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			ExpenseCategoryId: refData.ExpenseCategoryId
		};

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetExpenseCategoryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExpenseCategory = res.data.Data;
				$scope.newExpenseCategory.Mode = 'Modify';

				document.getElementById('phc-section').style.display = "none";
				document.getElementById('phc-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExpenseCategoryById = function (refData) {

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
					ExpenseCategoryId: refData.ExpenseCategoryId
				};

				$http({
					method: 'POST',
					url: base_url + "HRM/Creation/DelExpenseCategory",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExpenseCategoryList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});

	};
	//------------------------------Expense Group-----------------------------------------------------------------
	$scope.IsValidExpenseGroup = function () {
		if ($scope.newExpenseGroup.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}
		return true;
	}


	$scope.SaveUpdateExpenseGroup = function () {
		if ($scope.IsValidExpenseGroup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExpenseGroup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExpenseGroup();
					}
				});
			} else
				$scope.CallSaveUpdateExpenseGroup();

		}
	};

	$scope.CallSaveUpdateExpenseGroup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/SaveExpenseGroup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExpenseGroup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExpenseGroup();
				$scope.GetAllExpenseGroupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllExpenseGroupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExpenseGroupList = [];

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetAllExpenseGroupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExpenseGroupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExpenseGroupById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			ExpenseGroupId: refData.ExpenseGroupId
		};

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetExpenseGroupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExpenseGroup = res.data.Data;
				$scope.newExpenseGroup.Mode = 'Modify';

				document.getElementById('phc-section').style.display = "none";
				document.getElementById('phc-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExpenseGroupById = function (refData) {
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
					ExpenseGroupId: refData.ExpenseGroupId
				};
				$http({
					method: 'POST',
					url: base_url + "HRM/Creation/DelExpenseGroup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExpenseGroupList();
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