app.controller('ConfigurationController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Configuration';



	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			Student: 1,
			Employee: 1,
			ClassWiseAcademicMonth: 1,
			UpgradeStudentClass: 1
		};

		$scope.searchData = {
			Student: '',
			Employee: '',
			ClassWiseAcademicMonth: '',
			UpgradeStudentStudent: ''
		};

		$scope.perPage = {
			Student: GlobalServices.getPerPageRow(),
			Employee: GlobalServices.getPerPageRow(),
			ClassWiseWiseAcademicMonth: GlobalServices.getPerPageRow(),
			UpgradeStudentClass: GlobalServices.getPerPageRow()
		};

		$scope.newStudent = {
			StudentId: null,
			RegdNumberingMethodId: null,
			RegdPrefix: '',
			RegdSuffix: '',
			AllowRollNoAutoGenerate: true,
			ShowLeftStudentInTCCC: true,
			FilterStudentForBorders:false,
			Mode: 'Save'
		};

		$scope.newEmployee = {
			EmployeeId: null,
			RegdNumberingMethodId: null,
			EmployeeCodePrefix: '',
			EmployeeCodeSuffix: '',
			Mode: 'Save'
		};

		$scope.newClassWiseAcademicMonth = {
			ClassWiseAcademicMonthId: null,
			ClassId: null,
			FromMonthId: null,
			ToMonthId:null,
			Mode: 'Save'
		};

		$scope.newUpgradeStudentClass = {
			UpgradeStudentClassId: null,
			FromClassId: null,
			ToClassId: null,
			SNoId: null,
			CanUpgrade: null,
			Mode: 'Save'
		};

		//$scope.GetAllStudentList();
		//$scope.GetAllEmployeeList();
		//$scope.GetAllClassWiseAcademicMonthList();
		//$scope.GetAllUpgradeStudentClassList();

	}

	$scope.ClearStudent = function () {
		$scope.newStudent = {
			StudentId: null,
			RegdNumberingMethodId: null,
			RegdPrefix: '',
			RegdSuffix: '',
			AllowRollNoAutoGenerate: true,
			ShowLeftStudentInTCCC: true,
			FilterStudentForBorders:false,
			Mode: 'Save'
		};
	}
	$scope.ClearEmployee = function () {
		$scope.newEmployee = {
			EmployeeId: null,
			RegdNumberingMethodId: null,
			EmployeeCodePrefix: '',
			EmployeeCodeSuffix: '',
			Mode: 'Save'
		};
	}
	$scope.ClearClassWiseAcademicMonth = function () {
		$scope.newClassWiseAcademicMonth = {
			ClassWiseAcademicMonthId: null,
			ClassId: null,
			FromMonthId: null,
			ToMonthId: null,
			Mode: 'Save'
		};
	}
	$scope.ClearUpgradeStudentClass = function () {
		$scope.newUpgradeStudentClass = {
			UpgradeStudentClassId: null,
			FromClassId: null,
			ToClassId: null,
			SNoId: null,
			CanUpgrade: null,
			Mode: 'Save'
		};
	}

	//************************* Student *********************************

	$scope.IsValidStudent = function () {
		if ($scope.newStudent.RegdPrefix.isEmpty()) {
			Swal.fire('Please ! Enter Regd. Prefix');
			return false;
		}

		if ($scope.newStudent.RegdSuffix.isEmpty()) {
			Swal.fire('Please ! Enter Regd. Suffix');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateStudent = function () {
		if ($scope.IsValidStudent() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudent.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudent();
					}
				});
			} else
				$scope.CallSaveUpdateStudent();

		}
	};

	$scope.CallSaveUpdateStudent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveStudent",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newStudent }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStudent();
				$scope.GetAllStudentList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllStudentList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllStudentList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetStudentById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			StudentId: refData.StudentId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetStudentById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newStudent = res.data.Data;
				$scope.newStudent.Mode = 'Modify';

				document.getElementById('Student-Employee').style.display = "none";
				document.getElementById('Student-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelStudentById = function (refData) {

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
					StudentId: refData.StudentId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelStudent",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStudentList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Employee *********************************

	$scope.IsValidEmployee = function () {
		if ($scope.newEmployee.EmployeeCodePrefix.isEmpty()) {
			Swal.fire('Please ! Enter Employee Code Prefix');
			return false;
		}
		if ($scope.newEmployee.EmployeeCodeSuffix.isEmpty()) {
			Swal.fire('Please ! Enter Employee Code Suffix');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateEmployee = function () {
		if ($scope.IsValidEmployee() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newEmployee.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateEmployee();
					}
				});
			} else
				$scope.CallSaveUpdateEmployee();

		}
	};

	$scope.CallSaveUpdateEmployee = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveEmployee",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newEmployee }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearEmployee();
				$scope.GetAllEmployeeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllEmployeeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EmployeeList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllEmployeeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmployeeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetEmployeeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			EmployeeId: refData.EmployeeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetEmployeeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEmployee = res.data.Data;
				$scope.newEmployee.Mode = 'Modify';

				document.getElementById('Employee-content').style.display = "none";
				document.getElementById('Employee-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelEmployeeById = function (refData) {

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
					EmployeeId: refData.EmployeeId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelEmployee",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllEmployeeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Class Wise Academic Month *********************************

	$scope.IsValidClassWiseAcademicMonth = function () {
		if ($scope.newClassWiseAcademicMonth.ClassId.isEmpty()) {
			Swal.fire('Please ! Enter Class');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateClassWiseAcademicMonth = function () {
		if ($scope.IsValidClassWiseAcademicMonth() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newClassWiseAcademicMonth.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateClassWiseAcademicMonth();
					}
				});
			} else
				$scope.CallSaveUpdateClassWiseAcademicMonth();

		}
	};

	$scope.CallSaveUpdateClassWiseAcademicMonth = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveClassWiseAcademicMonth",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newClassWiseAcademicMonth }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearClassWiseAcademicMonth();
				$scope.GetAllClassWiseAcademicMonthList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllClassWiseAcademicMonthList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ClassWiseAcademicMonthList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassWiseAcademicMonthList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassWiseAcademicMonthList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetClassWiseAcademicMonthById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ClassWiseAcademicMonthId: refData.ClassWiseAcademicMonthId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetClassWiseAcademicMonthById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newClassWiseAcademicMonth = res.data.Data;
				$scope.newClassWiseAcademicMonth.Mode = 'Modify';

				document.getElementById('batch-Employee').style.display = "none";
				document.getElementById('batch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelClassWiseAcademicMonthById = function (refData) {

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
					ClassWiseAcademicMonthId: refData.ClassWiseAcademicMonthId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelClassWiseAcademicMonth",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllClassWiseAcademicMonthList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Upgrade Student Class *********************************

	$scope.IsValidUpgradeStudentClass = function () {
		if ($scope.newUpgradeStudentClass.Name.isEmpty()) {
			Swal.fire('Please ! Enter UpgradeStudentClass Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateUpgradeStudentClass = function () {
		if ($scope.IsValidUpgradeStudentClass() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newUpgradeStudentClass.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateUpgradeStudentClass();
					}
				});
			} else
				$scope.CallSaveUpdateUpgradeStudentClass();

		}
	};

	$scope.CallSaveUpdateUpgradeStudentClass = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveUpgradeStudentClass",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newUpgradeStudentClass }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearUpgradeStudentClass();
				$scope.GetAllUpgradeStudentClassList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllUpgradeStudentClassList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.UpgradeStudentClassList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllUpgradeStudentClassList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UpgradeStudentClassList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetUpgradeStudentClassById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			UpgradeStudentClassId: refData.UpgradeStudentClassId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetUpgradeStudentClassById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newUpgradeStudentClass = res.data.Data;
				$scope.newUpgradeStudentClass.Mode = 'Modify';

				document.getElementById('UpgradeStudentClass-Employee').style.display = "none";
				document.getElementById('UpgradeStudentClass-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelUpgradeStudentClassById = function (refData) {

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
					UpgradeStudentClassId: refData.UpgradeStudentClassId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelUpgradeStudentClass",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllUpgradeStudentClassList();
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