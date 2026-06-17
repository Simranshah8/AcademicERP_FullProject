app.controller('ELoanController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Employee Loan';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		
		$scope.currentPages = {
			EmployeeLoan: 1,
			LoanType: 1,
		};

		$scope.PayHeadingList = [];
		$http({
			method: 'GET',
			url: base_url + "Attendance/Transaction/GetAllPayHeading",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PayHeadingList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.LoanTypeList = [];
		//$http({
		//	method: 'GET',
		//	url: base_url + "HRM/Creation/GetAllLoanType",
		//	dataType: "json"
		//}).then(function (res) {
		//	hidePleaseWait();
		//	$scope.loadingstatus = "stop";
		//	if (res.data.IsSuccess && res.data.Data) {
		//		$scope.LoanTypeList = res.data.Data;

		//	} else {
		//		Swal.fire(res.data.ResponseMSG);
		//	}

		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		$scope.searchData = {
			EmployeeLoan: '',
			LoanType: '',
		};

		$scope.perPage = {
			EmployeeLoan: GlobalServices.getPerPageRow(),
			LoanType: GlobalServices.getPerPageRow(),
		};

		$scope.newEmployeeLoan = {
			TranId: 0,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			EmployeeId: null,
			LoanDate: new Date(),
			LoanTypeId: null,
			PrincipleAmount: 0,
			InterestRate: 0,
			Period: '',
			EMIAmount: 0,
			EffDate: new Date(),
			Remarks: '',
			Mode: 'Save'
		};
		

		$scope.newLoanType = {
			LoanTypeId: null,
			Name: '',
			Code: '',
			SerialNo: 0,
			PayHeadingId: null,
			Description: '',
			Mode: 'Save'
		};

		$scope.GetAllLoanType();
		$scope.GetAllEmployeeLoan();

	}

	$scope.ClearEmployeeLoan = function () {
		$scope.newEmployeeLoan = {
			TranId: 0,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			EmployeeId: null,
			LoanDate: new Date(),
			LoanTypeId: null,
			PrincipleAmount: 0,
			InterestRate: 0,
			Period: '',
			EMIAmount: 0,
			EffDate: new Date(),
			Remarks: '',
			Mode: 'Save'
		};
	}

	$scope.ClearLoanType = function () {
		$scope.newLoanType = {
			LoanTypeId: null,
			Name: '',
			Code: '',
			SerialNo: 0,
			PayHeadingId: null,
			Description: '',
			Mode: 'Save'
		};
	}

	function OnClickDefault() {
		document.getElementById('EmployeeLoan-form').style.display = "none";
		document.getElementById('LoanType-form').style.display = "none";


		document.getElementById('add-EmployeeLoan').onclick = function () {
			document.getElementById('EmployeeLoanlist').style.display = "none";
			document.getElementById('EmployeeLoan-form').style.display = "block";
			$scope.ClearEmployeeLoan();
		}

		document.getElementById('EmployeeLoanback-btn').onclick = function () {
			document.getElementById('EmployeeLoan-form').style.display = "none";
			document.getElementById('EmployeeLoanlist').style.display = "block";
			$scope.ClearEmployeeLoan();
		}

		document.getElementById('add-LoanType').onclick = function () {
			document.getElementById('LoanType-section').style.display = "none";
			document.getElementById('LoanType-form').style.display = "block";
			$scope.ClearLoanType();
		}

		document.getElementById('LoanTypeback-btn').onclick = function () {
			document.getElementById('LoanType-form').style.display = "none";
			document.getElementById('LoanType-section').style.display = "block";
			$scope.ClearLoanType();
		}



	};


	//*************************EmployeeLoan*********************************

	$scope.SaveUpdateEmployeeLoan = function () {
		if ($scope.confirmMSG.Accept == true) {
			var saveModify = $scope.newEmployeeLoan.Mode;
			Swal.fire({
				title: 'Do you want to ' + saveModify + ' the current data?',
				showCancelButton: true,
				confirmButtonText: saveModify,
			}).then((result) => {
				if (result.isConfirmed) {
					$scope.CallSaveUpdateEmployeeLoan();
				}
			});
		} else
			$scope.CallSaveUpdateEmployeeLoan();
	};

	$scope.CallSaveUpdateEmployeeLoan = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newEmployeeLoan.LoanDateDet) {
			$scope.newEmployeeLoan.LoanDate = $filter('date')(new Date($scope.newEmployeeLoan.LoanDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployeeLoan.LoanDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		if ($scope.newEmployeeLoan.EffDateDet) {
			$scope.newEmployeeLoan.EffDate = $filter('date')(new Date($scope.newEmployeeLoan.EffDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployeeLoan.EffDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/SaveEmployeeLoan",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newEmployeeLoan }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearEmployeeLoan();
				$scope.GetAllEmployeeLoan();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllEmployeeLoan = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EmployeeLoanList = [];
		$http({
			method: 'GET',
			url: base_url + "Attendance/Transaction/GetAllEmployeeLoan",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmployeeLoanList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetEmployeeLoanById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/getEmployeeLoanById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEmployeeLoan = res.data.Data;

				if ($scope.newEmployeeLoan.LoanDate)
					$scope.newEmployeeLoan.LoanDate_TMP = new Date($scope.newEmployeeLoan.LoanDate);

				if ($scope.newEmployeeLoan.EffDate)
					$scope.newEmployeeLoan.EffDate_TMP = new Date($scope.newEmployeeLoan.EffDate);

				$scope.newEmployeeLoan.Mode = 'Modify';
				document.getElementById('EmployeeLoanlist').style.display = "none";
				document.getElementById('EmployeeLoan-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.deleteEmployeeLoan = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Period + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Attendance/Transaction/DeleteEmployeeLoan",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.EmployeeLoanList.splice(ind, 1);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	//************************* LoanType  *********************************

	$scope.IsValidLoanType = function () {
		if ($scope.newLoanType.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateLoanType = function () {
		if ($scope.IsValidLoanType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newLoanType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateLoanType();
					}
				});
			} else
				$scope.CallSaveUpdateLoanType();

		}
	};

	$scope.CallSaveUpdateLoanType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/SaveLoanType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newLoanType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearLoanType();
				$scope.GetAllLoanType();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllLoanType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.LoanTypeList = [];
		$http({
			method: 'GET',
			url: base_url + "Attendance/Transaction/GetAllLoanType",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LoanTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetLoanTypeById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/getLoanTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newLoanType = res.data.Data;
				$scope.newLoanType.Mode = 'Modify';
				document.getElementById('LoanType-section').style.display = "none";
				document.getElementById('LoanType-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelLoanType = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Attendance/Transaction/DeleteLoanType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.LoanTypeList.splice(ind, 1);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});