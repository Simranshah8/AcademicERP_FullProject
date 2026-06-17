app.controller('HostelSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'HostelSetup';

	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.AttendanceTypeColl = [{ id: 1, text: 'Present' }, { id: 2, text: 'Absent' }, { id: 3, text: 'Leave' }];
		$scope.numberingMethods = GlobalServices.getNumberingMethod();

		$scope.BranchList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchList",
			dataType: "json",
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchList = res.data.Data;

				if ($scope.BranchList && $scope.BranchList.length == 1) {
					$scope.newGatePass.BranchId = $scope.BranchList[0].BranchId;

					$scope.GetGatePassConfig();
				}
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.currentPages = {
			HostelAttendanceStatus: 1,
			HostelAttendanceShift:1,
		};

		$scope.searchData = {
			HostelAttendanceStatus: '',
			HostelAttendanceShift:''
		};

		$scope.perPage = {
			HostelAttendanceStatus: GlobalServices.getPerPageRow(),
			HostelAttendanceShift: GlobalServices.getPerPageRow()
		};

		$scope.newAttendanceStatus = {
			AttendanceStatusId: null,
			Name: '',
			Code: '',
			ColorCode: '',
			OrderNo: 0,
			AttendanceTypeId:1,
			Description: '',
			isDefault: 0,
			Mode: 'Save'
		};

		$scope.newAttendanceShift = {
			AttendanceShiftId: null,
			Name: '',
			OrderNo: 0,
			Description:'',
			Mode: 'Save'
		};

		$scope.newGatePass = {
			GatePassId: null,
			BranchId: null,
			NumberingMethod: 1,
			Prefix: '',
			Suffix: '',
			StartNo: 0,
			NumericalPartWidth: 4,
			Declaration: '',
			Mode: 'Save'
		};

		$scope.GetAllAttendanceStatusList();
		$scope.GetAllAttendanceShiftList();
	}

	function OnClickDefault() {
		document.getElementById('status-form').style.display = "none";
		document.getElementById('shift-form').style.display = "none";

		//HostelSetup section
		document.getElementById('add-status').onclick = function () {
			document.getElementById('status-section').style.display = "none";
			document.getElementById('status-form').style.display = "block";
			$scope.ClearHostelAttendanceStatus();
		}

		document.getElementById('back-btn-status').onclick = function () {
			document.getElementById('status-form').style.display = "none";
			document.getElementById('status-section').style.display = "block";
			$scope.ClearHostelAttendanceStatus();
		}
		document.getElementById('add-shift').onclick = function () {
			document.getElementById('shift-content').style.display = "none";
			document.getElementById('shift-form').style.display = "block";
			$scope.ClearShift();
		}

		document.getElementById('back-btn-shift').onclick = function () {
			document.getElementById('shift-form').style.display = "none";
			document.getElementById('shift-content').style.display = "block";
			$scope.ClearShift();
		}

	}

	$scope.ClearHostelAttendanceStatus = function () {
		$scope.newAttendanceStatus = {
			AttendanceStatusId: null,
			Name: '',
			Code: '',
			ColorCode: '',
			OrderNo: 0,
			AttendanceTypeId: 1,
			Description: '',
			isDefault: 0,
			Mode: 'Save'
		};

	}

	$scope.ClearHostelAttendanceShift = function () {
		$scope.newAttendanceShift = {
			AttendanceShiftId: null,
			Name: '',
			OrderNo: 0,
			Description: '',
			Mode: 'Save'
		};

	}

	$scope.ClearGatePass = function () {
		$scope.newGatePass = {
			GatePassId: null,
			BranchId: null,
			NumberingMethod: 1,
			Prefix: '',
			Suffix: '',
			StartNo: 0,
			NumericalPartWidth: 4,
			Declaration: '',
			Mode: 'Save'
		};
	}

	//*************************Status *********************************


	$scope.SaveUpdateAttendanceStatus = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveHostelAttendanceStatus",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newAttendanceStatus }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearHostelAttendanceStatus();
				$scope.GetAllAttendanceStatusList();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllAttendanceStatusList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HostelAttendanceStatusList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllHostelAttendanceStatusList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HostelAttendanceStatusList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetHostelAttendanceStatusById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			AttendanceStatusId: refData.AttendanceStatusId
		};
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetHostelAttendanceStatusById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAttendanceStatus = res.data.Data;
				$scope.newAttendanceStatus.Mode = 'Modify';
				document.getElementById('status-section').style.display = "none";
				document.getElementById('status-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelHostelAttendanceStatusById = function (refData) {
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
					AttendanceStatusId: refData.AttendanceStatusId
				};

				$http({
					method: 'POST',
					url: base_url + "Hostel/Creation/DelHostelAttendanceStatus",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStatusList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};


	//*************************Shift *********************************


	$scope.SaveUpdateAttendanceShift = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveHostelAttendanceShift",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newAttendanceShift }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearHostelAttendanceShift();
				$scope.GetAllAttendanceShiftList();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllAttendanceShiftList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HostelAttendanceShiftList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllHostelAttendanceShiftList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HostelAttendanceShiftList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAttendanceShiftById = function (refData) {
		$scope.loadingShift = "running";
		showPleaseWait();
		var para = {
			AttendanceShiftId: refData.AttendanceShiftId
		};
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetHostelAttendanceShiftById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingShift = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAttendanceShift = res.data.Data;
				$scope.newAttendanceShift.Mode = 'Modify';
				document.getElementById('shift-content').style.display = "none";
				document.getElementById('shift-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAttendanceShiftById = function (refData) {
		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingShift = "running";
				showPleaseWait();
				var para = {
					AttendanceShiftId: refData.AttendanceShiftId
				};

				$http({
					method: 'POST',
					url: base_url + "Hostel/Creation/DelHostelAttendanceShift",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingShift = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAttendanceShiftList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};


	//*************************Gatepass Config *********************************

	$scope.GetGatePassConfig = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BranchId: $scope.newGatePass.BranchId
		};

		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetGatePass",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var r = res.data.Data;
				$scope.newGatePass.NumberingMethod = r.NumberingMethod;
				$scope.newGatePass.Prefix = r.Prefix;
				$scope.newGatePass.Suffix = r.Suffix;
				$scope.newGatePass.StartNo = r.StartNo;
				$scope.newGatePass.NumericalPartWidth = r.NumericalPartWidth;
				$scope.newGatePass.Declaration = r.Declaration;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	$scope.SaveUpdateConfiguration = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveGatePass",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newGatePass }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.GetGatePassConfig();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});