app.controller('AllowLeaveTypeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow Leave Type';

	$scope.LoadData = function () {
		$('.select2').select2();
		var gSrv = GlobalServices;
		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();

		$scope.BranchList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchList",
			dataType: "json",
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.DepartmentList = {};
		GlobalServices.getDepartmentList().then(function (res) {
			$scope.DepartmentList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		
		$scope.DesignationList = {};
		GlobalServices.getDesignationList().then(function (res) {
			$scope.DesignationList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.CategoryList = {};
		GlobalServices.getCategoryList().then(function (res) {
			$scope.CategoryList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	   $scope.LeaveTypeList = [];
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetAllLeaveTypeList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LeaveTypeList = res.data.Data;
				
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		
		$scope.currentPages = {
			AllowLeaveType: 1,
		};

		$scope.searchData = {
			AllowLeaveType: '',
		};

		$scope.perPage = {
			AllowLeaveType: gSrv.getPerPageRow(),
		};

		$scope.newFilter = {};

	}


	$scope.CheckUnCheckAll = function (payH) {

		if ($scope.AllowLeaveTypeList) {
			$scope.AllowLeaveTypeList.forEach(function (dc) {
				if (dc.LeaveTypeColl) {
					dc.LeaveTypeColl.forEach(function (ph) {
						if (ph.LeaveTypeId === payH.LeaveTypeId) {
							ph.IsAllow = payH.IsAllow;
						}
					});
				}

			});
		}
	};
	

	$scope.SaveAllowLeaveType = function () {

		Swal.fire({
			title: 'Do you want to save the current data?',
			showCancelButton: true,
			confirmButtonText: 'Save',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var dtColl = [];
				$scope.AllowLeaveTypeList.forEach(function (emp) {
					emp.LeaveTypeColl.forEach(function (ph) {
						if (ph.IsAllow == true) {
							dtColl.push({
								UserId: emp.UserId,
								LeaveTypeId: ph.LeaveTypeId
							});
						}
					})
				});
				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/SaveAllowLeaveType",
					headers: { 'Content-Type': undefined },
					transformRequest: function (data) {
						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));
						return formData;
					},
					data: { jsonData: dtColl }
				}).then(function (res) {
					$scope.loadingstatus = "stop";
					hidePleaseWait();
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						/*    $scope.GetAllAssignCustomer();*/
					}
				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
				});
			}
		});


	}

	$scope.GetLeaveTypeForAllow = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowLeaveTypeList = [];
		var para = {
			BranchId: $scope.newFilter.BranchId,
			DepartmentId: $scope.newFilter.DepartmentId,
			LevelId: $scope.newFilter.LevelId,
			EmployeeGroupId: $scope.newFilter.EmployeeGroupId,
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetAllEmployeeForAllowLeaveType",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dataColl = mx(res.data.Data);

				var query = dataColl.groupBy(t => ({ EmployeeId: t.EmployeeId }));

				angular.forEach(query, function (q) {
					var fst = q.elements[0];
					var subQry = mx(q.elements);

					var beData = {
						TranId: fst.TranId,
						EmployeeId: fst.EmployeeId,
						UserId: fst.UserId,
						EmployeeCode: fst.EmployeeCode,
						EmployeeName: fst.EmployeeName,
						EnrollNo: fst.EnrollNo,
						Department: fst.Department,
						Designation: fst.Designation,
						AllowLeaveType: fst.LeaveType,
						LeaveTypeColl: [],
					};

					$scope.LeaveTypeList.forEach(function (pa) {
						var find = subQry.firstOrDefault(p1 => p1.LeaveTypeId == pa.LeaveTypeId);
						beData.LeaveTypeColl.push({
							LeaveTypeId: pa.LeaveTypeId,
							IsAllow: find ? find.IsAllow : false,
						});
					});

					$scope.AllowLeaveTypeList.push(beData);
				});



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.DelAllowLeaveType = function () {
		Swal.fire({
			title: 'Warning!',
			text: 'You are about to permanently delete this Allow Leave Type data. This action cannot be undone. Are you sure you want to proceed?',
			icon: 'warning',
			showCancelButton: true,
			confirmButtonText: 'Yes, Delete',
			cancelButtonText: 'Cancel',
			reverseButtons: true
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/DelAllowLeaveType",
					dataType: "json",
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						Swal.fire(res.data.ResponseMSG);
						$scope.GetAllCreateCanteenList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};
});