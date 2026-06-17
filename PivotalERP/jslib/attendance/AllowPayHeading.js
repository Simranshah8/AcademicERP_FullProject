app.controller('AllowPayHeadingController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow PayHeading';

	$scope.LoadData = function () {
		$('.select2').select2();
		var gSrv = GlobalServices;
		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();

		$scope.DepartmentList = [];
		gSrv.getDepartmentList().then(function (res) {
			$scope.DepartmentList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.CategoryList = [];
		gSrv.getCategoryList().then(function (res) {
			$scope.CategoryList = res.data.Data;
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
			AllowPayHeading: 1,
		};

		$scope.searchData = {
			AllowPayHeading: '',
		};

		$scope.perPage = {
            AllowPayHeading: gSrv.getPerPageRow(),
        };
        $scope.sortKeys = {
            EmployeeWise: '',
        }

        $scope.reverses = {
            EmployeeWise: false,
        }
        $scope.newFilter = {};

	}

	$scope.sortEmpWise = function (keyname) {
		$scope.sortKeys.EmployeeWise = keyname;
		$scope.reverses.EmployeeWise = !$scope.reverses.EmployeeWise;
	}
	$scope.CheckUnCheckAll = function (payH)
	{

		if ($scope.AllowPayHeadingList) {
			$scope.AllowPayHeadingList.forEach(function (dc)
			{
				if (dc.PayHeadColl) {
					dc.PayHeadColl.forEach(function (ph) {
						if (ph.PayHeadingId === payH.PayHeadingId) {
							ph.IsAllow = payH.IsAllow;
						}
					});
                }
				
			});
		}
	};


	$scope.SaveAllowPayHeading = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dtColl = [];
		$scope.AllowPayHeadingList.forEach(function (emp) {
			emp.PayHeadColl.forEach(function (ph) {
				if (ph.IsAllow == true) {
					dtColl.push({
						EmployeeId: emp.EmployeeId,
						PayHeadingId: ph.PayHeadingId
					});
				}
			})
		});
		$http({
			method: 'POST',
			url: base_url + "Attendance/Transaction/SaveAllowPayHeading",
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

	$scope.GetPayHeadingForAllow = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowPayHeadingList = [];
		$http({
			method: 'GET',
			url: base_url + "Attendance/Transaction/GetAllEmployeeForAllowPayHeading?BranchId=" + $scope.newFilter.BranchId + "&DepartmentId=" + $scope.newFilter.DepartmentId + "&CategoryId=" + $scope.newFilter.CategoryId,
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
						AllowPayHeading:fst.PayHeading,						
						PayHeadColl: [],
					};

					$scope.PayHeadingList.forEach(function (pa) {
						var find = subQry.firstOrDefault(p1 => p1.PayHeadingId == pa.PayHeadingId);
						beData.PayHeadColl.push({
							PayHeadingId: pa.PayHeadingId,
							IsAllow:find ? find.IsAllow : false,
						});
					});

					$scope.AllowPayHeadingList.push(beData);
				});



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