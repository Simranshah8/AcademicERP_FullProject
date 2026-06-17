app.controller('TodaysClassController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Todays Class';
	var glbS = GlobalServices;

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();
		$scope.EmployeeSearchOptions = glbS.getEmployeeSearchOptions();
		//$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.perPage = {
			Classwise: glbS.getPerPageRow(),		 
			Teacherwise: glbS.getPerPageRow(),
			Subjectwise: glbS.getPerPageRow(),
		};

		$scope.currentPages = {
			Classwise: 1,
			Teacherwise: 1,
			Subjectwise:1,

		};


		$scope.searchData = {
			Classwise: '',
			Teacherwise: '',
			Subjectwise: '',			
		};

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
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
				$scope.SelectedClassSemesterList = [];
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
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.todayClass = {
			ForDate: new Date(),
			ForDate_TMP:new Date(),
			SelectedClass: null,			 
			BatchId: null,
			SemesterId: null,
			ClassYearId: null,
			ReportType:1
		}

		$scope.newTeacherwise = {
			ForDate: new Date(),
			ForDate_TMP: new Date(),
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			EmployeeId:null
		}

		$scope.newSubjectwise = {
			ForDate: new Date(),
			ForDate_TMP: new Date(),
			SubjectId: null
		}

		$scope.SubjectList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllSubjectList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SubjectList = res.data.Data;
			}  
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



	}

	$scope.GetTodayLessonPlan = function (rType) {		
		var para = {};
		if (rType == 1) {
			$scope.DataList1 = [];
			para = {
				forDate: $filter('date')(new Date($scope.todayClass.ForDateDet.dateAD), 'yyyy-MM-dd'),
				classId: ($scope.todayClass.SelectedClass ? $scope.todayClass.SelectedClass.ClassId : null),
				sectionId: ($scope.todayClass.SelectedClass ? $scope.todayClass.SelectedClass.SectionId : null),
				subjectId: null,
				employeeId: null,
				reportType: rType,
				BatchId: ($scope.todayClass.BatchId ? $scope.todayClass.BatchId : null),
				ClassYearId: ($scope.todayClass.ClassYearId ? $scope.todayClass.ClassYearId : null),
				SemesterId: ($scope.todayClass.SemesterId ? $scope.todayClass.SemesterId : null),
			};
		} else if (rType == 2) {
			$scope.DataList2 = [];
			para = {
				forDate: $filter('date')(new Date($scope.newTeacherwise.ForDateDet.dateAD), 'yyyy-MM-dd'),
				classId: null,
				sectionId: null,
				subjectId: null,
				employeeId: $scope.newTeacherwise.EmployeeId,
				reportType: rType
			};
        }
		else if (rType == 3) {
			$scope.DataList3 = [];
			para = {
				forDate: $filter('date')(new Date($scope.newSubjectwise.ForDateDet.dateAD), 'yyyy-MM-dd'),
				classId: null,
				sectionId: null,
				subjectId: $scope.newSubjectwise.SubjectId,
				employeeId: null,
				reportType: rType
			};
		}
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetTodayLessonPlan",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                if (rType == 1) {
                    $scope.DataList1 = [];
					$scope.DataList1 = res.data.Data;
                }
                else if (rType == 2) {
                    $scope.DataList2 = [];
                    $scope.DataList2 = res.data.Data;
                }
                else if (rType == 3) {
                    $scope.DataList3 = [];
                    $scope.DataList3 = res.data.Data;
                }
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
    }

	
	$(document).on('click', '.card-body img', function () {
		var src = $(this).attr('src');

		if (src) {
			var newWindow = window.open();
			newWindow.document.write(
				'<img src="' + src + '" style="width:100%;height:auto;" />'
			);
			newWindow.document.title = "Image Preview";
		}
	});
	$(document).on('click', '.card-body a', function (e) {
		e.preventDefault();
		var url = $(this).attr('href');

		if (url) {
			window.open(url, '_blank'); // opens in new tab
		}
	});

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});