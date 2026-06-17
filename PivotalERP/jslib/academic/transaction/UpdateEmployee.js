app.controller('EmployeeController', function ($scope, $http, $timeout, $rootScope, $filter, $translate, GlobalServices) {
	$scope.Title = 'Employee';
	$rootScope.ChangeLanguage();
	OnClickDefault();

	function OnClickDefault() {
		document.getElementById('left-employee-form').style.display = "none";

		// sections display and hide
		document.getElementById('add-employee-left').onclick = function () {
			document.getElementById('employee-left-section').style.display = "none";
			document.getElementById('left-employee-form').style.display = "block";
			$scope.ClearLeftEmployee();
		}
		document.getElementById('employee-left-back-btn').onclick = function () {
			document.getElementById('left-employee-form').style.display = "none";
			document.getElementById('employee-left-section').style.display = "block";
			$scope.ClearLeftEmployee();
		}
	}
	$scope.LoadData = function () {
		//GlobalServices.ChangeLanguage();	
		$('.select2').select2();
		var gSrv = GlobalServices;
		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();
		$scope.GenderColl = gSrv.getGenderList();
		$scope.MaritalStatusList = gSrv.getMaritaStatusList();
		$scope.NationalityList = GlobalServices.getNationalityList();
		$scope.currentPages = {
			LeftEmployee: 1,
			UpdateEmployee:1,
		};

		$scope.searchData = {
			LeftEmployee: '',
			UpdateEmployee:''
		};

		$scope.perPage = {
			LeftEmployee: gSrv.getPerPageRow(),
			UpdateEmployee: gSrv.getPerPageRow()
		};

		$scope.TaxRuleAsList = [{ id: 1, text: 'NORMAL' }, { id: 2, text: 'SSF' }];
		$scope.MonthList = gSrv.getMonthList();

		$scope.AcademicYearColl = [];
		$scope.AcademicYearColl = gSrv.getYearList();

		$scope.DepartmentList = [];
		gSrv.getDepartmentList().then(function (res) {
			$scope.DepartmentList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.DesignationList = [];
		gSrv.getDesignationList().then(function (res) {
			$scope.DesignationList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.LevelList = [];
		gSrv.getLevelList().then(function (res) {
			$scope.LevelList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		$scope.CategoryList = [];
		gSrv.getCategoryList().then(function (res) {
			$scope.CategoryList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllLeftEmployeeList();
		$scope.newLeftEmployee = {
			EmployeeSearchBy: 'E.Name',
			ShowLeftEmployee: false,
			EmployeeId: null,
			LeftRemarks: '',
			LeftDate_TMP: new Date()
		};

		$scope.LeftTypeList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllLeftTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LeftTypeList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.ClearLeftEmployee = function () {
		$scope.newLeftEmployee = {
			EmployeeSearchBy: 'E.Name',
			EmployeeId: null,
			LeftRemarks: ''
		};
	};

	$scope.DelLeftEmployee = function (refData) {

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
					EmployeeId: refData.EmployeeId,
					TranId: 0
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DelLeftEmployee",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					$scope.GetAllLeftEmployeeList();
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});

	};

	$scope.LeftEmpSelected = function () {

		//alert('ss');
	};
	$scope.GetAllLeftEmployeeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.LeftEmployeeList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetLeftEmployeeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LeftEmployeeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	$scope.CallSaveLeftEmployee = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var filesColl = $scope.newLeftEmployee.AttachmentFilesColl;
		if ($scope.newLeftEmployee.LeftDateDet) {
			$scope.newLeftEmployee.LeftDate_AD = $scope.newLeftEmployee.LeftDateDet.dateAD;
		} else
			$scope.newLeftEmployee.LeftDate_AD = null;
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveLeftEmployee",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						if (data.files[i].File)
							formData.append("file" + i, data.files[i].File);
						else
							formData.append("file" + i, data.files[i]);
					}
				}

				return formData;
			},
			data: { jsonData: $scope.newLeftEmployee, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.GetAllLeftEmployeeList();
				$scope.ClearLeftEmployee();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	//Js For Employee Update Starts
	$scope.GetEmployeeForUpdate = function () {
		$scope.UpdateEmployeeList = [];	
			var para = {				
				DepartmentId: $scope.newUpdateEmloyee.DepartmentId,
				DesignationId: $scope.newUpdateEmloyee.DesignationId,
				CategoryId: $scope.newUpdateEmloyee.CategoryId
			};

			$scope.loadingstatus = "running";
			//showPleaseWait();
			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetEmployeeForUpdate",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.UpdateEmployeeList = res.data.Data.DataColl;

					$timeout(function () {
						angular.forEach($scope.UpdateEmployeeList, function (st) {
							if (st.DateOfJoining)
								st.DateOfJoining_TMP = new Date(st.DateOfJoining);
						});
					});


					//$timeout(function () {
					//	$scope.$broadcast('refreshFixedColumns');
					//});
					//$timeout(function () {

					//	$('#main-table').fixedHeaderTable({						
					//		fixedColumns: $scope.newUpdateStudent.FreezCol
					//	});
					//});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		
	}

	$scope.sortEmployee = function (keyname) {
		$scope.sortKeyEmployee = keyname;   //set the sortKey to the param passed
		$scope.reverse2 = !$scope.reverse2; //if true make it false and vice versa
	}

	$scope.UpdateEmployee = function () {
		Swal.fire({
			title: 'Do you want to update selected Employees record?',
			showCancelButton: true,
			confirmButtonText: 'Update',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();


				angular.forEach($scope.UpdateEmployeeList, function (ut) {
					if (ut.DateOfJoiningDet)
						ut.DateOfJoining = $filter('date')(new Date(ut.DateOfJoiningDet.dateAD), 'yyyy-MM-dd');
					else if (ut.DateOfJoining_TMP)
						ut.DateOfJoining = $filter('date')(new Date(ut.DateOfJoining_TMP), 'yyyy-MM-dd');
					else if (ut.DateOfJoining)
						ut.DateOfJoining = $filter('date')(new Date(ut.DateOfJoining), 'yyyy-MM-dd');
					else
						ut.DateOfJoining = null;
				});

				var para = {
					employeeColl: $scope.UpdateEmployeeList
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/UpdateEmployee",
					dataType: "json",
					data: angular.toJson(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	
	$scope.SaveUpdateEmployeeWise = function (employee) {
		if (!employee) {
			Swal.fire('No Employee data provided for update.');
			return;
		}
		Swal.fire({
			title: 'Do you want to update ' + employee.FirstName + ' ' + employee.MiddleName + ' ' + employee.LastName + '\'s record ?',
			showCancelButton: true,
			confirmButtonText: 'Update',
		}).then((result) => {
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				// Format the Employee Joining Date
				if (employee.DateOfJoiningDet?.dateAD) {
					employee.DateOfJoining = $filter('date')(new Date(employee.DateOfJoiningDet.dateAD), 'yyyy-MM-dd');
				} else if (employee.DOBAD_TMP) {
					employee.DateOfJoining = $filter('date')(new Date(employee.DateOfJoining_TMP), 'yyyy-MM-dd');
				} else if (employee.DateOfJoining) {
					employee.DateOfJoining = $filter('date')(new Date(employee.DateOfJoining), 'yyyy-MM-dd');
				} else {
					employee.DateOfJoining = null;
				}
				
				var para = {
					employeeColl: [employee] // Wrapping the single employee in an array
				};
				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/UpdateEmployee",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

//----------------
	$scope.ShowPersonalImg = function (beData) {
		$scope.SelectedEmployee = beData;
		$('#PersonalImg').modal('show');
	};
	$scope.toggleFields = function () {
		if ($scope.newLeftEmployee.EmployeeId) {
			$scope.isFieldsDisabled = true;
		} else {
			$scope.isFieldsDisabled = false;
		}
	};

	
});