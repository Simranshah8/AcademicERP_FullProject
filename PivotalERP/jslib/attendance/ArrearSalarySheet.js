app.controller('ArrearSalarySheetController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Salary Detail';

	$scope.LoadData = function () {
		$('.select2').select2();
		var gSrv = GlobalServices;
		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();
		$scope.MonthList = gSrv.getMonthList();

		$scope.DepartmentList = [];
		gSrv.getDepartmentList().then(function (res) {
			$scope.DepartmentList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

				
		$scope.AcademicYearColl = [];
		gSrv.getAcademicYearList().then(function (res) {
			$scope.AcademicYearColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AttendanceTypeList = [];
		$http({
			method: 'Get',
			url: base_url + "Attendance/Transaction/GetAllAttendanceType",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dtColl = res.data.Data;
				dtColl.forEach(function (dt) {
					if (dt.IsActive == true) {
						$scope.AttendanceTypeList.push(dt);
					}
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.PayHeadingList = [];
		$http({
			method: 'GET',
			url: base_url + "Attendance/Transaction/GetAllPayHeading",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dtColl = res.data.Data;
				dtColl.forEach(function (dt) {
					if (dt.IsActive == true) {
						$scope.PayHeadingList.push(dt);
					}
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.BranchList = [];
		$http({
			method: 'GET',
			url: base_url + "Attendance/Transaction/GetBranchListforPayhead",
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


		$scope.currentPages = {
			ArrearSalarySheet: 1,
		};

		$scope.searchData = {
			ArrearSalarySheet: '',
		};

		$scope.perPage = {
			ArrearSalarySheet: gSrv.getPerPageRow(),
		};
				
		$scope.newFilter = {
			BranchId: 0,
			DepartmentId: 0,
			CategoryId: 0,
			//YearId: null,
			//MonthId: null
		};
	}

	$scope.SaveArrearSalarySheet = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dtColl = [];
		$scope.EmployeeListForArrearSalarySheet.forEach(function (emp) {
			emp.PayHeadColl.forEach(function (ph) {
				if (ph.Amount > 0 || ph.Amount < 0) {
					dtColl.push({
						EmployeeId: emp.EmployeeId,
						PayHeadingId: ph.PayHeadingId,
						Amount: ph.Amount,
						YearId: $scope.newFilter.YearId,
						MonthId: $scope.newFilter.MonthId
					});
				}
			})
		});
		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/SaveArrearSalarySheet",
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

	$scope.GetEmployeeForArrearSalarySheet = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EmployeeListForArrearSalarySheet = [];
		$http({
			method: 'GET',
			url: base_url + "Attendance/Transaction/GetAllEmployeeForArrearSalarySheet?BranchId=" + $scope.newFilter.BranchId + "&DepartmentId=" + $scope.newFilter.DepartmentId + "&CategoryId=" + $scope.newFilter.CategoryId + "&YearId=" + $scope.newFilter.YearId + "&MonthId=" + $scope.newFilter.MonthId,
			dataType: "json"
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
						EmployeeCode: fst.EmployeeCode,
						EmployeeName: fst.EmployeeName,
						EnrollNo: fst.EnrollNo,
						Department: fst.Department,
						Designation: fst.Designation,
						PayHeading: fst.PayHeading,
						MonthId: fst.MonthId,
						YearId: fst.YearId,
						PayHeadColl: [],
						AttendanceTypeColl: []
					};

					$scope.PayHeadingList.forEach(function (pa) {
						var find = subQry.firstOrDefault(p1 => p1.PayHeadingId == pa.PayHeadingId);
						beData.PayHeadColl.push({
							PayHeadingId: pa.PayHeadingId,
							Amount: find ? find.Amount : 0,
						});
					});

					$scope.AttendanceTypeList.forEach(function (pa) {
						var find = subQry.firstOrDefault(p1 => p1.AttendanceTypeId == pa.AttendanceTypeId);
						beData.AttendanceTypeColl.push({
							AttendanceTypeId: pa.AttendanceTypeId,
							Value: find ? find.Value : 0,
							Rate: find ? find.Rate : pa.CalculationValue,
						});

					});



					$scope.EmployeeListForArrearSalarySheet.push(beData);
					if(beData.MonthId)
						$scope.newFilter.MonthId = beData.MonthId;

					if (beData.YearId)
						$scope.newFilter.YearId = beData.YearId;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.DeleteArrearSalarySheet = function () {
		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {				
				var para = {
					BranchId: $scope.newFilter.BranchId,
					DepartmentId: $scope.newFilter.DepartmentId,
					CategoryId: $scope.newFilter.CategoryId,
					YearId: $scope.newFilter.YearId,
					MonthId: $scope.newFilter.MonthId
				};
				$http({
					method: 'POST',
					url: base_url + "Attendance/Transaction/DeleteArrearSalarySheet",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess) {
						$scope.GetEmployeeForArrearSalarySheet();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

	$scope.DelArrearSalarySheetData = function (refData, ind) {
		Swal.fire({
			title: 'Do you want to delete Arrear SalarySheet of ' + refData.EmployeeName + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',			
		}).then((result) => {
			if (result.isConfirmed) {
				var para = {
					EmployeeId: refData.EmployeeId,
					YearId: $scope.newFilter.YearId,
					MonthId: $scope.newFilter.MonthId
				};
				$http({
					method: 'POST',
					url: base_url + "Attendance/Transaction/DelArrearSalarySheetData",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetEmployeeForArrearSalarySheet();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	$scope.UpdateDetailData = function (refData, ind) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dtColl = [];
		refData.PayHeadColl.forEach(function (ph) {
			if (ph.Amount > 0 || ph.Amount < 0) {
				dtColl.push({
					EmployeeId: refData.EmployeeId,
					PayHeadingId: ph.PayHeadingId,
					Amount: ph.Amount,
					YearId: $scope.newFilter.YearId,
					MonthId: $scope.newFilter.MonthId
				});
			}
		});
		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/SaveArrearSalarySheet",
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
			Swal.fire(res.data.ResponseMSG); 5
			if (res.data.IsSuccess == true) {
				$scope.GetEmployeeForArrearSalarySheet();
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