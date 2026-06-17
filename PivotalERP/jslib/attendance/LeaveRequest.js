app.controller('LeaveRequestController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Leave Request';
	var gSrv = GlobalServices;

	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.LeaveStatusColl = [{ id: 0, text: 'ALL' }, { id: 1, text: 'NOT_APPROVED' }, { id: 2, text: 'APPROVED' }, { id: 3, text: 'CANCEL' }, { id: 4, text: 'REJECTED' },]

		$scope.ApprovedStatusColl = [{ id: 2, text: 'Approve' }, { id: 3, text: 'Cancel' }, { id: 4, text: 'Deny' },]
		$scope.SelectColl = [{ id: 1, text: 'DateWise' }, { id: 2, text: 'PeriodWise' }];
		$scope.newDet = {
			SelectId: 1 // default: DateWise
		};
		//Added y Suresh on Magh 18 Starts
		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;

			if ($scope.AcademicConfig.ActiveFaculty == true) {

				$scope.FacultyList = [];
				GlobalServices.getFacultyList().then(function (res) {
					$scope.FacultyList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}


			if ($scope.AcademicConfig.ActiveLevel == true) {

				$scope.LevelList = [];
				GlobalServices.getClassLevelList().then(function (res) {
					$scope.LevelList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveSemester == true) {

				/*	$scope.SelectedClassSemesterList = [];*/
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveBatch == true) {

				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveClassYear == true) {

				$scope.ClassYearList = [];
				/*$scope.SelectedClassClassYearList = [];*/
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		//Ends

		$scope.currentPages = {
			StudentLeaveRequest: 1,
			EmployeeLeaveRequest: 1,
		};

		$scope.searchData = {
			StudentLeaveRequest: '',
			EmployeeLeaveRequest: '',
		};

		$scope.perPage = {
			StudentLeaveRequest: GlobalServices.getPerPageRow(),
			EmployeeLeaveRequest: GlobalServices.getPerPageRow(),
		};

		$scope.newStudentLeaveRequest = {
			LeaveStatus: 1,
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
		};

		$scope.newEmployeeLeaveRequest = {
			LeaveStatus: 1,
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
		};

		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	$scope.GetEmpLeaveReqList = function () {
		showPleaseWait();
		$scope.EmpLeaveRequestColl = [];

		var para = {
			LeaveStatus: $scope.newEmployeeLeaveRequest.LeaveStatus,
			dateFrom: $scope.newEmployeeLeaveRequest.DateFromDet ? $filter('date')(new Date($scope.newEmployeeLeaveRequest.DateFromDet.dateAD), 'yyyy-MM-dd') : null,
			dateTo: $scope.newEmployeeLeaveRequest.DateToDet ? $filter('date')(new Date($scope.newEmployeeLeaveRequest.DateToDet.dateAD), 'yyyy-MM-dd') : null,
			EmployeeId: null,
		}
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetEmpLeaveReq",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmpLeaveRequestColl = res.data.Data.LeaveColl;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.CurLeave = {};
	$scope.ShowApprovedDialog = function (beData) {
		$scope.CurLeave = beData;

		showPleaseWait();
		$scope.EmpWiseRequestColl = [];

		var para = {
			LeaveStatus: 0,
			dateFrom: null,
			dateTo: null,
			EmployeeId: beData.EmployeeId
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetEmpLeaveReq",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmpWiseRequestColl = res.data.Data.LeaveColl;
				$scope.BalanceColl = res.data.Data.BalanceColl;


				$('#modal-xl').modal('show');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



	};

	$scope.LeaveApproved = function () {
		showPleaseWait();
		$scope.EmpWiseRequestColl = [];

		var para = {
			LeaveRequestId: $scope.CurLeave.LeaveRequestId,
			ApprovedRemarks: $scope.CurLeave.ApprovedRemarks,
			ApprovedType: $scope.CurLeave.LeaveStatus
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/LeaveApprove",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$('#modal-xl').modal('hide');

				if ($scope.CurLeave.StudentId > 0)
					$scope.GetStudentLeaveReqList();
				else
					$scope.GetEmpLeaveReqList();

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.ShowDocImg = function (item) {

		if (item.DocumentColl && item.DocumentColl.length > 0) {
			$scope.viewImg = {
				ContentPath: ''
			};
			$scope.viewImg.ContentPath = item.DocumentColl[0].DocPath;
			$('#PersonalImg').modal('show');
		}


	};
	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};


	$scope.GetStudentLeaveReqList = function () {
		showPleaseWait();
		$scope.StudentLeaveRequestColl = [];

		var para = {
			LeaveStatus: $scope.newStudentLeaveRequest.LeaveStatus,
			dateFrom: $scope.newStudentLeaveRequest.DateFromDet ? $filter('date')(new Date($scope.newStudentLeaveRequest.DateFromDet.dateAD), 'yyyy-MM-dd') : null,
			dateTo: $scope.newStudentLeaveRequest.DateToDet ? $filter('date')(new Date($scope.newStudentLeaveRequest.DateToDet.dateAD), 'yyyy-MM-dd') : null,
			StudentId: null,
			ClassId: ($scope.newStudentLeaveRequest.SelectedClass ? $scope.newStudentLeaveRequest.SelectedClass.ClassId : null),
			SectionId: ($scope.newStudentLeaveRequest.SelectedClass ? $scope.newStudentLeaveRequest.SelectedClass.SectionId : null),
			//Added by Suresh on 18 Magh for batch ClassYear and Semester starts
			BatchId: $scope.newStudentLeaveRequest.BatchId,
			SemesterId: $scope.newStudentLeaveRequest.SemesterId,
			ClassYearId: $scope.newStudentLeaveRequest.ClassYearId,
			//Ends

		}
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetStudentLeaveReq",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentLeaveRequestColl = res.data.Data.LeaveColl;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.ShowSTApprovedDialog = function (beData) {
		$scope.CurLeave = beData;

		showPleaseWait();
		$scope.EmpWiseRequestColl = [];

		var para = {
			LeaveStatus: 0,
			dateFrom: null,
			dateTo: null,
			StudentId: beData.StudentId
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetStudentLeaveReq",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmpWiseRequestColl = res.data.Data.LeaveColl;

				$('#modal-xl').modal('show');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



	};

	$scope.validateDates = function (changedField, obj) {
		if (!obj.DateFromDet || !obj.DateToDet ||
			!obj.DateFromDet.dateAD || !obj.DateToDet.dateAD) {
			return true;
		}

		var fromDate = $filter('date')(new Date(obj.DateFromDet.dateAD), 'yyyy-MM-dd');
		var toDate = $filter('date')(new Date(obj.DateToDet.dateAD), 'yyyy-MM-dd');

		if (!fromDate || !toDate) return true;

		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.DateFrom_TMP = new Date();
						obj.DateFromDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.DateTo_TMP = new Date();
						obj.DateToDet = new Date();
					});
				});
			}
			return false;
		}
		return true;
	};
});