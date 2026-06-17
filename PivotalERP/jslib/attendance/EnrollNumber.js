$(document).ready(function () {

	$(document).on('keyup', '.serialST', function (e) {

		if (e.which == 13) {
			var checkBoxChecked = true;
			if (checkBoxChecked == true) {
				var $this = $(this);
				var $td = $this.closest('td');
				var $row = $td.closest('tr');
				var $rows = $row.parent();
				var column = $td.index();


				while ($td.length) {

					$row = $row.next('tr');

					if ($row.length == 0) {

						$row = $rows.children().first();
						// $row = $rows.children().get(2);

						column++;
					}

					$td = $row.children().eq(column);
					var $input = $td.find('.serialST');
					if ($input.length) {
						$input.focus();
						break;
					}
				}
			} else {

				var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
				if (key == 13) {
					e.preventDefault();
					var inputs = $(this).closest('form').find(':input:visible');
					inputs.eq(inputs.index(this) + 1).focus();
				}
			}

		}
	});


	$(document).on('keyup', '.serialEM', function (e) {

		if (e.which == 13) {
			var checkBoxChecked = true;
			if (checkBoxChecked == true) {
				var $this = $(this);
				var $td = $this.closest('td');
				var $row = $td.closest('tr');
				var $rows = $row.parent();
				var column = $td.index();


				while ($td.length) {

					$row = $row.next('tr');

					if ($row.length == 0) {

						$row = $rows.children().first();
						// $row = $rows.children().get(2);

						column++;
					}

					$td = $row.children().eq(column);
					var $input = $td.find('.serialEM');
					if ($input.length) {
						$input.focus();
						break;
					}
				}
			} else {

				var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
				if (key == 13) {
					e.preventDefault();
					var inputs = $(this).closest('form').find(':input:visible');
					inputs.eq(inputs.index(this) + 1).focus();
				}
			}

		}
	});
});

app.controller('EnrollNumberController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {
	$scope.Title = 'Enroll Number';

	$rootScope.ConfigFunction = function () {
	 
	};
	$rootScope.ChangeLanguage();
	//Added By Suresh On Magh 16 starts
	var gSrv = GlobalServices;
	//Ends
	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			Student: 1,
			Employee: 1
		
		};

		$scope.searchData = {
			Student: '',
			Employee: ''
			
		};

		$scope.perPage = {
			Student: GlobalServices.getPerPageRow(),
			Employee: GlobalServices.getPerPageRow()
		
		};

		$scope.newStudent = {
			StudentId: null,
			StudentDetailsColl: [],
			Mode: 'Update'
		};
		$scope.newStudent.StudentDetailsColl.push({});

		$scope.newEmployee = {
			EmployeeId: null,
			EmployeeDetailsColl: [],
			Mode: 'Update'
		};
		$scope.newEmployee.EmployeeDetailsColl.push({});

	/*	$scope.ClassSection = {};
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
*/
		//$scope.GetAllStudentList();
		//$scope.GetAllEmployeeList();

		//Added by Suresh on Magh 16 for batch , semester classyear enable starts

		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
			$scope.AllClassList = mx(res.data.Data.ClassList);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


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
		

	}
	$scope.GetClassWiseStudentList = function () {

		$scope.newStudent.StudentList = [];

		var para = {
			ClassId: $scope.newStudent.SelectedClass.ClassId,
			SectionIdColl: ($scope.newStudent.SectionId && $scope.newStudent.SectionId > 0 ? $scope.newStudent.SectionId.toString() : ''),
			BatchId: $scope.newStudent.BatchId,
			ClassYearId: $scope.newStudent.ClassYearId,
			SemesterId: $scope.newStudent.SemesterId
		};

		if (para.ClassId && para.ClassId > 0) {
			GlobalServices.GetClassWiseStudentList(para.ClassId, para.SectionIdColl, para.BatchId, para.ClassYearId, para.SemesterId).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newStudent.StudentList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        }
		

	};


	$scope.ClearStudent = function () {
		$scope.newStudent = {
			StudentId: null,
			StudentDetailsColl: [],
			Mode: 'Update'
		};
		$scope.newStudent.StudentDetailsColl.push({});
	}
	$scope.ClearEmployee = function () {
		$scope.newEmployee = {
			EmployeeId: null,

			EmployeeDetailsColl: [],
			Mode: 'Update'
		};
		$scope.newEmployee.EmployeeDetailsColl.push({});


	}


	//************************* Student *********************************

	$scope.IsValidStudent = function () {
		//if ($scope.newStudent.ClassId.isEmpty()) {
		//	Swal.fire('Please ! Enter Class');
		//	return false;
		//}

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
			url: base_url + "Attendance/Creation/SaveEnrollNumberStudent",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newStudent.StudentList }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	

	//************************* Employee *********************************

	$scope.IsValidEmployee = function () {
		//if ($scope.newEmployee.Name.isEmpty()) {
		//	Swal.fire('Please ! Enter Name');
		//	return false;
		//}

		return true;
	}

	$scope.GetEmployeeList = function () {

		$scope.newEmployee.EmployeeDetailsColl = [];
	
		GlobalServices.GetEmployeeList().then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEmployee.EmployeeDetailsColl = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
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
			url: base_url + "Attendance/Creation/SaveEnrollNumberEmployee",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newEmployee.EmployeeDetailsColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.sortLeft = function (keyname) {
		$scope.sortKeyLeft = keyname;   //set the sortKey to the param passed
		$scope.reverse1 = !$scope.reverse1; //if true make it false and vice versa
	}

	$scope.sortemp = function (keyname) {
		$scope.sortKeyemp = keyname;   //set the sortKey to the param passed
		$scope.reverse2 = !$scope.reverse2; //if true make it false and vice versa
	}


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});