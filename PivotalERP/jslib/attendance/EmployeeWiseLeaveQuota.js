app.controller('LeaveQuotaByEmployeeCollController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Leave Quota By Employee';

	$scope.LoadData = function () {
		$('.select2').select2();
		var gSrv = GlobalServices;
		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();
		$scope.currentPages = {
			LeaveQuota: 1,
		};

		$scope.searchData = {
			LeaveQuota: '',
		};

		$scope.perPage = {
			LeaveQuota: gSrv.getPerPageRow(),
		};

		$scope.TypeColl = [{ id: 1, text: 'Employee' }, { id: 2, text: 'Salesman' }]

		$scope.BranchList = [];
		$http({
			method: 'GET',
			url: base_url + "Setup/Security/GetAllBranchList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
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



		
		$scope.CategoryList = {};
		GlobalServices.getCategoryList().then(function (res) {
			$scope.CategoryList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.PeriodList = [];
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetAllFinancialPeriodList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PeriodList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.CostClassList = [];
		$http({
			method: 'GET',
			url: base_url + "Account/Creation/GetAllCostClasss",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CostClassList = res.data.Data;
			}
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

		$scope.newFilter = {
			EmployeeOrSalesman: 1,
			BranchId: null,
			DepartmentId: null,
			CategoryId: null,
			PeriodId: null
		};

	}
	$scope.ClearFilter = function () {
		$scope.newFilter = {
			BranchId: null,
			DepartmentId: null,
			CategoryId: null,
			PeriodId: null
		};
	}


	$scope.sortEmpData = function (keyname) {
		$scope.sortKeySS = keyname;   //set the sortKey to the param passed
		$scope.reverseSS = !$scope.reverseSS; //if true make it false and vice versa
	}

	$scope.SaveLeaveQuotaColl = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var LeaveQuotaCollection = [];
		var missingUserNames = [];
		var hasNegative = false; // flag to stop if negative leave found

		angular.forEach($scope.EmployeeListForLeaveQuota, function (emp) {
			// Skip adding if UserId is missing, but collect name for message
			if (!emp.UserId || emp.UserId === 0) {
				missingUserNames.push(emp.EmployeeName);
				return;
			}

			// ✅ Check negative leave per employee
			for (var i = 0; i < emp.LeaveQuotaByEmpDetailsColl.length; i++) {
				var lc = emp.LeaveQuotaByEmpDetailsColl[i];
				if (lc.NoOfLeave < 0) {
					Swal.fire("Leave quota cannot be negative for " + lc.Name + " (" + emp.EmployeeName + ")");
					hasNegative = true;
					return;
				}
			}

			if (hasNegative) return; // stop processing further

			var findP = mx($scope.PeriodList).firstOrDefault(p1 => p1.PeriodId == $scope.newFilter.PeriodId);
			var dateFrom = findP ? $filter('date')(new Date(findP.StartDate_AD), 'yyyy-MM-dd') : null;
			var dateTo = findP ? $filter('date')(new Date(findP.EndDate_AD), 'yyyy-MM-dd') : null;

			var LeaveQuota = {
				EmployeeOrSalesman: $scope.newFilter.EmployeeOrSalesman,
				PeriodId: $scope.newFilter.PeriodId,
				UserId: emp.UserId,
				DateFrom: dateFrom,
				DateTo: dateTo,
				LeaveQuotaByEmpDetailsColl: []
			};

			angular.forEach(emp.LeaveQuotaByEmpDetailsColl, function (lc) {
				LeaveQuota.LeaveQuotaByEmpDetailsColl.push({
					Name: lc.Name,
					LeaveTypeId: lc.LeaveTypeId,
					NoOfLeave: lc.NoOfLeave
				});
			});

			LeaveQuotaCollection.push(LeaveQuota);
		});

		// Stop saving if negative leave found
		if (hasNegative) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			return;
		}

		if (missingUserNames.length > 0) {
			var nameList = missingUserNames.join(", ");
			Swal.fire("Note", "Skipped saving for these employees due to missing UserId: " + nameList, "warning");
		}

		if (LeaveQuotaCollection.length === 0) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire("Error", "No valid data to save", "error");
			return;
		}

		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/SaveLeaveQuotaColl",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: LeaveQuotaCollection }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			$scope.GetEmployeeForLeaveQuota();
		}, function () {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire("Error", "Failed to save leave quota data", "error");
		});
	};




	$scope.GetEmployeeForLeaveQuota = function () {

		//if ($scope.newFilter.PeriodId > 0) {

		//} else {
		//	return;
		//}
		if (!$scope.newFilter.PeriodId || $scope.newFilter.PeriodId <= 0) {
			Swal.fire({
				icon: 'warning',
				title: 'Please select the Period.'
			});
			return;
		}

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EmployeeListForLeaveQuota = [];

		var para = {
			BranchId: $scope.newFilter.BranchId,
			DepartmentId: $scope.newFilter.DepartmentId,
			CategoryId: $scope.newFilter.CategoryId,
			PeriodId: $scope.newFilter.PeriodId,
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetAllEmpLeaveQuota",
			dataType: "json",
			data: JSON.stringify(para),
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
						EmployeeId: fst.EmployeeId,
						UserId: fst.UserId,
						TranId: fst.TranId,
						BranchId: fst.BranchId,
						CategoryId: fst.CategoryId,
						EmployeeCode: fst.EmployeeCode,
						EmployeeName: fst.EmployeeName,
						EnrollNumber: fst.EnrollNumber,
						Department: fst.Department,
						Designation: fst.Designation,
						PayHeading: fst.PayHeading,
						LeaveQuotaByEmpDetailsColl: [],
					};

					$scope.LeaveTypeList.forEach(function (pa) {
						var find = subQry.firstOrDefault(p1 => p1.LeaveTypeId == pa.LeaveTypeId);
						beData.LeaveQuotaByEmpDetailsColl.push({
							Name: pa.Name,
							LeaveTypeId: pa.LeaveTypeId,
							NoOfLeave: find ? find.NoOfLeave : 0,
							IsAllow: find ? find.IsAllow : false
						});

					});

					$scope.EmployeeListForLeaveQuota.push(beData);

					if (beData.PeriodId)
						$scope.newFilter.PeriodId = beData.PeriodId;

				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.SaveEmpWiseLeaveQuota = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		// ✅ Validation: Check negative leave before saving
		for (var i = 0; i < refData.LeaveQuotaByEmpDetailsColl.length; i++) {
			var lc = refData.LeaveQuotaByEmpDetailsColl[i];
			if (lc.NoOfLeave < 0) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire("Leave quota cannot be negative for " + lc.Name + " (" + refData.EmployeeName + ")");
				return; // stop save
			}
		}

		// save date
		var findP = mx($scope.PeriodList).firstOrDefault(p1 => p1.PeriodId == $scope.newFilter.PeriodId);
		var dateFrom = null, dateTo = null;
		if (findP) {
			dateFrom = $filter('date')(new Date(findP.StartDate_AD), 'yyyy-MM-dd');
			dateTo = $filter('date')(new Date(findP.EndDate_AD), 'yyyy-MM-dd');
		}

		// prepare data for saving
		var empLeaveQuota = {
			EmployeeOrSalesman: $scope.newFilter.EmployeeOrSalesman,
			PeriodId: $scope.newFilter.PeriodId,
			UserId: refData.UserId,
			DateFrom: dateFrom,
			DateTo: dateTo,
			LeaveQuotaByEmpDetailsColl: []
		};

		angular.forEach(refData.LeaveQuotaByEmpDetailsColl, function (lc) {
			empLeaveQuota.LeaveQuotaByEmpDetailsColl.push({
				Name: lc.Name,
				LeaveTypeId: lc.LeaveTypeId,
				NoOfLeave: lc.NoOfLeave
			});
		});

		// API call
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/SaveLeaveQuotaByEmp",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: empLeaveQuota }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess) {
				$scope.GetEmployeeForLeaveQuota();
			}
		}, function () {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire("Error", "Failed to save Leave Quota", "error");
		});
	};



	$scope.DelLeaveQuota = function () {
		if (!$scope.newFilter.PeriodId || $scope.newFilter.PeriodId <= 0) {
			Swal.fire({
				icon: 'warning',
				title: 'Warning',
				text: 'Please select the Period first.'
			});
			return;
		}
		Swal.fire({
			title: 'Warning!',
			text: 'You are about to permanently delete this Employee Wise Leave Quota for Selected Period. This action cannot be undone. Are you sure you want to proceed?',
			icon: 'warning',
			showCancelButton: true,
			confirmButtonText: 'Yes, Delete',
			cancelButtonText: 'Cancel',
			reverseButtons: true
		}).then((result) => {
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					PeriodId: $scope.newFilter.PeriodId
				};
				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/DelLeaveQuotaByEmp",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire({
						icon: res.data.IsSuccess ? 'success' : 'error',
						text: res.data.ResponseMSG
					});
					if (res.data.IsSuccess) {
						$scope.GetEmployeeForLeaveQuota();
					}
				}, function (reason) {
					hidePleaseWait();
					Swal.fire('Failed: ' + reason);
				});
			}
		});
	};

	$scope.DelLeaveQuotaById = function (refData) {
		var employeeName = refData.EmployeeName || 'this employee';
		Swal.fire({
			title: 'Warning!',
			text: 'You are about to permanently delete the Leave Quota for "' + employeeName + '". This action cannot be undone. Are you sure you want to proceed?',
			icon: 'warning',
			showCancelButton: true,
			confirmButtonText: 'Yes, Delete',
			cancelButtonText: 'Cancel',
			reverseButtons: true
		}).then((result) => {
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					TranId: refData.TranId
				};
				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/DelLeaveQuotaByEmp",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire({
						icon: res.data.IsSuccess ? 'success' : 'error',
						text: res.data.ResponseMSG
					});
					if (res.data.IsSuccess) {
						$scope.GetEmployeeForLeaveQuota();
					}
				}, function (reason) {

					hidePleaseWait();
					Swal.fire('Failed: ' + reason);

				});
			}
		});
	};


	$scope.ApplyBulkValue = function (colIndex) {
		var value = $scope.LeaveTypeList[colIndex].BulkValue || 0;
		if (value < 0) {
			Swal.fire("Leave cannot be negative!");
			$scope.LeaveTypeList[colIndex].BulkValue = 0;
			value = 0;
		}

		angular.forEach($scope.EmployeeListForLeaveQuota, function (emp) {
			if (
				emp.LeaveQuotaByEmpDetailsColl &&
				emp.LeaveQuotaByEmpDetailsColl[colIndex] &&
				emp.LeaveQuotaByEmpDetailsColl[colIndex].IsAllow !== false
			) {
				emp.LeaveQuotaByEmpDetailsColl[colIndex].NoOfLeave = value;
			}
		});
	};

	$scope.PasteData = function (ph, sind) {
		var clipText = event.clipboardData.getData('text/plain');

		if (clipText) {
			$scope.loadingstatus = 'running';
			showPleaseWait();
			var ind = 0;
			clipText.split("\n").forEach(function (line) {
				if (line && line.length > 0) {
					if (ind < $scope.EmployeeListForLeaveQuota.length) {
						var emp = $scope.EmployeeListForLeaveQuota[ind];
						for (var ii = 0; ii < emp.LeaveQuotaByEmpDetailsColl.length; ii++) {
							var findPH = emp.LeaveQuotaByEmpDetailsColl[ii];
							if (findPH.LeaveTypeId == ph.LeaveTypeId) {
								findPH.NoOfLeave = isEmptyAmt(line);
								//$scope.ChangeRate(emp, findPH);
								break;
							}
						}
					}
					ind++;
				}
			});

			hidePleaseWait();
			$scope.loadingstatus = "stop";
		}
	}
	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});