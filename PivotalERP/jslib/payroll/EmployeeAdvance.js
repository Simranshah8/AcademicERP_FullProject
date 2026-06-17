app.controller('EAdvanceController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Employee Loan';
	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		$scope.currentPages = {
			EmployeeAdvance: 1,
			AdvanceType: 1,
		};

		$scope.searchData = {
			EmployeeAdvance: '',
			AdvanceType: '',
		};

		$scope.perPage = {
			EmployeeAdvance: GlobalServices.getPerPageRow(),
			AdvanceType: GlobalServices.getPerPageRow(),
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

		$scope.newEmployeeAdvance = {
			TranId: 0,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			EmployeeId: null,
			AdvanceDate: new Date(),
			AdvanceTypeId: null,
			AdvanceAmount: 0,
			Installment: '',
			DeductionAmount: 0,
			EffDate: new Date(),
			Remarks: '',
			Mode: 'Save'
		};

		$scope.newAdvanceType = {
			AdvanceTypeId: null,
			Name: '',
			Code: '',
			SNo: 0,
			PayHeadingId: null,
			Description: '',
			Mode: 'Save'
		};

		$scope.GetAllEmployeeAdvanceList();
		$scope.GetAllAdvanceTypeList();
	}

	$scope.ClearEmployeeAdvance = function () {
		$scope.newEmployeeAdvance = {
			EmployeeAdvanceId: null,
			Date_TMP: '',
			AdvanceTypeId: null,
			AdvanceAmount: '',
			Installment: '',
			DeductionAmount: '',
			EffDate_TMP: '',
			Remarks: '',
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			Mode: 'Save'
		};
	}

	$scope.ClearAdvanceType = function () {
		$scope.newAdvanceType = {
			AdvanceTypeId: null,
			Name: '',
			Code: '',
			SNo: 0,
			Description: '',
			Mode: 'Save'
		};
	}

	function OnClickDefault() {
		document.getElementById('EmployeeAdvance-form').style.display = "none";
		document.getElementById('AdvanceType-form').style.display = "none";


		document.getElementById('add-EmployeeAdvance').onclick = function () {
			document.getElementById('EmployeeAdvancelist').style.display = "none";
			document.getElementById('EmployeeAdvance-form').style.display = "block";
			$scope.ClearVehicleDetails();
		}

		document.getElementById('EmployeeAdvanceback-btn').onclick = function () {
			document.getElementById('EmployeeAdvance-form').style.display = "none";
			document.getElementById('EmployeeAdvancelist').style.display = "block";
			$scope.ClearVehicleDetails();
		}

		document.getElementById('add-AdvanceType').onclick = function () {
			document.getElementById('AdvanceType-section').style.display = "none";
			document.getElementById('AdvanceType-form').style.display = "block";
			$scope.ClearVehicleDetails();
		}

		document.getElementById('AdvanceTypeback-btn').onclick = function () {
			document.getElementById('AdvanceType-form').style.display = "none";
			document.getElementById('AdvanceType-section').style.display = "block";
			$scope.ClearVehicleDetails();
		}
	};

	//*************************EmployeeAdvance*********************************
	$scope.SaveUpdateEmployeeAdvance = function () {
		if ($scope.confirmMSG.Accept == true) {
			var saveModify = $scope.newEmployeeAdvance.Mode;
			Swal.fire({
				title: 'Do you want to ' + saveModify + ' the current data?',
				showCancelButton: true,
				confirmButtonText: saveModify,
			}).then((result) => {
				if (result.isConfirmed) {
					$scope.CallSaveUpdateEmployeeAdvance();
				}
			});
		} else
			$scope.CallSaveUpdateEmployeeAdvance();
	};

	$scope.CallSaveUpdateEmployeeAdvance = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newEmployeeAdvance.AdvanceDateDet) {
			$scope.newEmployeeAdvance.AdvanceDate = $filter('date')(new Date($scope.newEmployeeAdvance.AdvanceDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployeeAdvance.AdvanceDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		if ($scope.newEmployeeAdvance.EffDateDet) {
			$scope.newEmployeeAdvance.EffDate = $filter('date')(new Date($scope.newEmployeeAdvance.EffDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployeeAdvance.EffDate = $filter('date')(new Date(), 'yyyy-MM-dd');


		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/SaveEmployeeAdvance",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newEmployeeAdvance }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearEmployeeAdvance();
				$scope.GetAllEmployeeAdvanceList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllEmployeeAdvanceList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EmployeeAdvanceList = [];
		$http({
			method: 'GET',
			url: base_url + "Attendance/Transaction/GetAllEmployeeAdvance",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmployeeAdvanceList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetEmployeeAdvanceById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/getEmployeeAdvanceById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEmployeeAdvance = res.data.Data;

				if ($scope.newEmployeeAdvance.AdvanceDate)
					$scope.newEmployeeAdvance.AdvanceDate_TMP = new Date($scope.newEmployeeAdvance.AdvanceDate);

				if ($scope.newEmployeeAdvance.EffDate)
					$scope.newEmployeeAdvance.EffDate_TMP = new Date($scope.newEmployeeAdvance.EffDate);

				$scope.newEmployeeAdvance.Mode = 'Modify';
				document.getElementById('EmployeeAdvancelist').style.display = "none";
				document.getElementById('EmployeeAdvance-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelEmployeeAdvance = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.EmployeeId + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Attendance/Transaction/DeleteEmployeeAdvance",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.EmployeeAdvanceList.splice(ind, 1);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	//************************* AdvanceType *********************************

	$scope.IsValidAdvanceType = function () {
		if ($scope.newAdvanceType.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAdvanceType = function () {
		if ($scope.IsValidAdvanceType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAdvanceType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAdvanceType();
					}
				});
			} else
				$scope.CallSaveUpdateAdvanceType();

		}
	};

	$scope.CallSaveUpdateAdvanceType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/SaveAdvanceType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newAdvanceType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAdvanceType();
				$scope.GetAllAdvanceTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllAdvanceTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AdvanceTypeList = [];

		$http({
			method: 'GET',
			url: base_url + "Attendance/Transaction/GetAllAdvanceType",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AdvanceTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAdvanceTypeById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/GetAdvanceTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAdvanceType = res.data.Data;
				$scope.newAdvanceType.Mode = 'Modify';

				document.getElementById('AdvanceType-section').style.display = "none";
				document.getElementById('AdvanceType-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAdvanceTypeById = function (refData) {
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
					TranId: refData.TranId
				};
				$http({
					method: 'POST',
					url: base_url + "Attendance/Transaction/DeleteAdvanceType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAdvanceTypeList();
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