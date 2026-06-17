app.controller('ConfigurationController', function ($scope, $http, $timeout, $filter, $rootScope, $translate, GlobalServices) {
	$scope.Title = 'Configuration';
	$rootScope.ChangeLanguage();

	var gSrv = GlobalServices;
	$scope.LoadData = function () {

		$scope.LeftStudentConfigColl = [
			{ id: 1, text: 'Disable Account Login' },
			{ id: 2, text: 'Enable Account Login' }
			/*{ id: 2, text: 'Show the Data before Left Date' }*/
		];

		$scope.StudentRefAsColl = [{ id: 1, text: 'Any' }, { id: 2, text: 'Enquiry' }, { id: 3, text: 'Registration' }, { id: 4, text: 'Enquiry Or Registration' }];

		$scope.ReArrangeByColl = [{ id: 1, text: 'Old RollNo & Section Wise' }, { id: 2, text: 'First Name & Section Wise' }, { id: 3, text: 'Full Name & Section Wise' }, { id: 4, text: 'Last Name & Section Wise' },
		{ id: 5, text: 'Old RollNo & Class Wise' }, { id: 6, text: 'First Name & Class Wise' }, { id: 7, text: 'Full Name & Class Wise' }, { id: 8, text: 'Last Name & Class Wise' },];

		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();
		$scope.MonthList = gSrv.getMonthList();

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
			Student: gSrv.getPerPageRow(),
			Employee: gSrv.getPerPageRow(),
			ClassWiseWiseAcademicMonth: gSrv.getPerPageRow(),
			UpgradeStudentClass: gSrv.getPerPageRow()
		};

		$scope.newStudent = {
			RegdNumberingMethod: 1,
			/*RegdNumberingMethod: 2,*/
			RegdPrefix: '',
			RegdSuffix: '',
			AutoGenerateRollNo: true,
			ShowLeftStudentinTC_CC: false,
			AllowReGenerateUserPwd: false,
			LeftStudentConfig: 1,
			ReArrangeRollNoBy: 3,
			Mode: 'Save'
		};

		$scope.newEmployee = {
			RegdNumberingMethod: 1,
			CodePrefix: '',
			CodeSuffix: '',
			AllowReGenerateUserPwd: false,
			Mode: 'Save'
		};

		$scope.newAcademic = {
			ActiveLevel: false,
			ActiveFaculty: false,
			ActiveSemester: false,
			ActiveBatch: false,
			ActiveClassYear: false,
			//New field added on Mangsir 11
			ActiveClassWiseMonth: false,
			SectionWiseSetup: false,
			SectionWiseSubjectMapping: false,
			SectionWiseExamSchedule: false,
			SectionWiseMarkSetup: false,
			SectionWiseLessonPlan: false,
			Mode: 'Save'
		};

		$scope.newClassWiseAcademicMonth = {
			ClassId: 0,
			FromMonth: 0,
			ToMonth: 0,
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

		$scope.EmpUserAsPerList = [
			{ id: 1, text: 'AutoNumber' },
			{ id: 2, text: 'Code' },
			{ id: 3, text: 'FirstName' }
		];

		$scope.numberingMethods = gSrv.getNumberingMethod();
		$scope.GetAllStudentList();
		$scope.GetAllEmployeeList();
		$scope.GetAllClassWiseAcademicMonthList();
		$scope.GetAllUpgradeStudentClassList();
		$scope.GetAllAcademicList();

		$scope.ClassList = [];
		gSrv.getClassList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

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
	}

	$scope.ClearStudent = function () {
		$scope.newStudent = {
			RegdNumberingMethod: 1,
			RegdPrefix: '',
			RegdSuffix: '',
			AutoGenerateRollNo: true,
			ShowLeftStudentinTC_CC: false,
			ReArrangeRollNoBy: 3,
			Mode: 'Save'
		};

	}
	$scope.ClearEmployee = function () {
		$scope.newEmployee = {
			RegdNumberingMethod: 1,
			CodePrefix: '',
			CodeSuffix: '',
			Mode: 'Save'
		};
	}
	$scope.ClearAcademic = function () {
		$scope.newAcademic = {
			ActiveLevel: false,
			ActiveFaculty: false,
			ActiveSemester: false,
			ActiveBatch: false,
			ActiveClassYear: false,
			//New field added on Mangsir 11
			ActiveClassWiseMonth: false,
			SectionWiseSetup: false,
			SectionWiseSubjectMapping: false,
			SectionWiseExamSchedule: false,
			SectionWiseMarkSetup: false,
			SectionWiseLessonPlan: false,
			Mode: 'Save'
		};

	}
	$scope.ClearClassWiseAcademicMonth = function () {
		$scope.newClassWiseAcademicMonth = {
			ClassId: 0,
			FromMonth: 0,
			ToMonth: 0,
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
		//if ($scope.newStudent.RegdPrefix.isEmpty()) {
		//	Swal.fire('Please ! Enter Regd. Prefix');
		//	return false;
		//}

		//if ($scope.newStudent.RegdSuffix.isEmpty()) {
		//	Swal.fire('Please ! Enter Regd. Suffix');
		//	return false;
		//}
		if (!Number.isInteger($scope.newStudent.StartNo) || $scope.newStudent.StartNo < 0) {
			Swal.fire('Start No cannot be negative');
			return false;
		}
		if (!Number.isInteger($scope.newStudent.NumericalPartWidth) || $scope.newStudent.NumericalPartWidth < 0) {
			Swal.fire('Number Pad Width cannot be negative');
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

		$scope.newStudent.BranchId = $scope.newStudent.ForBranchId;

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/SaveStudentConfiguration",
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


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.ReArrangeRollNoCS = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ReArrangeBy: $scope.newStudent.ReArrangeRollNoBy
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/ReArrangeRollNoCS",
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

	$scope.GetAllStudentList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var forBranchId = $scope.newStudent.ForBranchId;
		$scope.ClearStudent();

		var para = {
			BranchId: forBranchId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetStudentConfiguration",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newStudent = res.data.Data;
				$scope.newStudent.ForBranchId = forBranchId;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}



	//************************* Employee *********************************

	$scope.IsValidEmployee = function () {
		//if ($scope.newEmployee.EmployeeCodePrefix.isEmpty()) {
		//	Swal.fire('Please ! Enter Employee Code Prefix');
		//	return false;
		//}
		//if ($scope.newEmployee.EmployeeCodeSuffix.isEmpty()) {
		//	Swal.fire('Please ! Enter Employee Code Suffix');
		//	return false;
		//}
		if (!Number.isInteger($scope.newEmployee.StartNo) || $scope.newEmployee.StartNo < 0) {
			Swal.fire('Start No Cannot be Negative');
			return false;
		}
		if (!Number.isInteger($scope.newEmployee.NumericalPartWidth) || $scope.newEmployee.NumericalPartWidth < 0) {
			Swal.fire('Number Pad Width Cannot be Negative');
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
			url: base_url + "Academic/Setup/SaveEmployeeConfiguration",
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

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllEmployeeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ClearEmployee();

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetEmployeeConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEmployee = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.IsValidAcademic = function () {
		//if ($scope.newEmployee.EmployeeCodePrefix.isEmpty()) {
		//	Swal.fire('Please ! Enter Employee Code Prefix');
		//	return false;
		//}
		//if ($scope.newEmployee.EmployeeCodeSuffix.isEmpty()) {
		//	Swal.fire('Please ! Enter Employee Code Suffix');
		//	return false;
		//}

		return true;
	}

	$scope.SaveUpdateAcademic = function () {
		if ($scope.IsValidAcademic() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAcademic.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAcademic();
					}
				});
			} else
				$scope.CallSaveUpdateAcademic();

		}
	};

	$scope.CallSaveUpdateAcademic = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/SaveAcademicConfiguration",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newAcademic }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllAcademicList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ClearAcademic();

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetAcademicConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAcademic = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	//************************* Class Wise Academic Month *********************************

	$scope.IsValidClassWiseAcademicMonth = function () {
		//if ($scope.newClassWiseAcademicMonth.ClassId.isEmpty()) {
		//	Swal.fire('Please ! Enter Class');
		//	return false;
		//}



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
			url: base_url + "Academic/Setup/SaveClassWiseAcademicMonth",
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
			url: base_url + "Academic/Setup/GetClassWiseAcademicMonth",
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
					ClassId: refData.ClassId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Setup/DelClassWiseAcademicMonth",
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
		//if ($scope.newUpgradeStudentClass.Name.isEmpty()) {
		//	Swal.fire('Please ! Enter UpgradeStudentClass Name');
		//	return false;
		//}



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
			url: base_url + "Academic/Setup/SaveUpgradeStudentClass",
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
			url: base_url + "Academic/Setup/GetUpgradeStudentClass",
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
					ClassId: refData.ClassId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Setup/DelUpgradeStudentClass",
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