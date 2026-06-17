app.controller('LibraryMembershipController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Library Membership';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			Student: 1,
			Employee: 1,
		};

		$scope.searchData = {
			Student: '',
			Employee: '',

		};

		$scope.perPage = {
		
			Student: GlobalServices.getPerPageRow(),
			Employee: GlobalServices.getPerPageRow(),
		};

	
		$scope.newStudent = {
			StudentId: null,			
			Mode: 'Save'
		};

		$scope.newEmployee = {
			EmployeeId: null,			
			Mode: 'Save'
		};

		$scope.StudentMembershipNoAsList = [{ id: 1, text: 'AutoNumber' }, { id: 2, text: 'Regd.No.' }];
		$scope.EmployeeMembershipNoAsList = [{ id: 1, text: 'AutoNumber' }, { id: 2, text: 'Code' }];

		$scope.ClassList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
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

	}

	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa
	}

	$scope.GetClassWiseMemberList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.StudentMemberList = [];
		var para = {
			ClassId: $scope.newStudent.SelectedClass.ClassId,
			SectionId: $scope.newStudent.SelectedClass.SectionId,
			BatchId: $scope.newStudent.BatchId,
			ClassYearId: $scope.newStudent.ClassYearId,
			SemesterId: $scope.newStudent.SemesterId
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetClassWiseMemberlist",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			$scope.StudentMemberList = res.data.Data;

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GenerateStudentMembership = function () {
		Swal.fire({
			title: 'Do you want to generate membership?',
			showCancelButton: true,
			confirmButtonText: 'Generate',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ForStudentEmp: 1,
					Prefix: $scope.newStudent.Prefix,
					Suffix: $scope.newStudent.Suffix,
					MembershipNoAs: $scope.newStudent.MembershipNoAs,
					ReGenerate: $scope.newStudent.ReGenerate
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/GenerateMembership",
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
    }


	$scope.GetEmpMemberList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.EmpMemberList = [];
		
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetEmpMemberlist",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			$scope.EmpMemberList = res.data.Data;

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GenerateEmployeeMembership = function () {
		Swal.fire({
			title: 'Do you want to generate membership?',
			showCancelButton: true,
			confirmButtonText: 'Generate',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ForStudentEmp: 2,
					Prefix: $scope.newEmployee.Prefix,
					Suffix: $scope.newEmployee.Suffix,
					MembershipNoAs: $scope.newEmployee.MembershipNoAs,
					ReGenerate: $scope.newEmployee.ReGenerate
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/GenerateMembership",
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
	}
});