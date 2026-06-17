app.controller('SyllabusStatusController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Syllabus Status';
	var glbS = GlobalServices;

	OnClickDefault();
	 

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
			Subjectwise: 1,

		};

		$scope.newTeacherwise = {
			SelectEmployee: $scope.EmployeeSearchOptions[0].value
		};


		$scope.searchData = {
			Classwise: '',
			Teacherwise: '',
			Subjectwise: '',
		};

		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;		

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

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

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

	function OnClickDefault() {
		document.getElementById('classwisestatus-form').style.display = "none";
		document.getElementById('teacherwisestatus-form').style.display = "none";
		document.getElementById('subjectstatus-form').style.display = "none";
		//Classwise
		//document.getElementById('classwisedetail').onclick = function () {
		//	document.getElementById('classwiseList').style.display = "none";
		//	document.getElementById('classwisestatus-form').style.display = "block";
		//}

		document.getElementById('backclasswiselist').onclick = function () {
			document.getElementById('classwiseList').style.display = "block";
			document.getElementById('classwisestatus-form').style.display = "none";
		}

		//Teacherwise
		//document.getElementById('tacherwisedetail').onclick = function () {
		//	document.getElementById('teacherwiselist').style.display = "none";
		//	document.getElementById('teacherwisestatus-form').style.display = "block";
		//}

		document.getElementById('backteacherwiselist').onclick = function () {
			document.getElementById('teacherwiselist').style.display = "block";
			document.getElementById('teacherwisestatus-form').style.display = "none";
		}
		//Subjectwise
		//document.getElementById('subjectdetail').onclick = function () {
		//	document.getElementById('subjectlist').style.display = "none";
		//	document.getElementById('subjectstatus-form').style.display = "block";
		//}

		document.getElementById('backsubjectwiselist').onclick = function () {
			document.getElementById('subjectlist').style.display = "block";
			document.getElementById('subjectstatus-form').style.display = "none";
		}
	}


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

	$scope.GetClassWiseLessonPlan = function (rType) {
		var para = {};

		if (rType == 1) {
			$scope.DataList1 = [];
			para = {
				BatchId: $scope.newClasswise.BatchId || null,
				ClassYearId: $scope.newClasswise.ClassYearId || null,
				SemesterId: $scope.newClasswise.SemesterId || null,
				ClassId: ($scope.newClasswise.SelectedClass ? $scope.newClasswise.SelectedClass.ClassId : null),
				SectionId: ($scope.newClasswise.SelectedClass ? $scope.newClasswise.SelectedClass.SectionId : null),
				EmployeeId: null,
				SubjectId: null,
				ReportType:1
			};
		} else if (rType == 2) {
			$scope.DataList2 = [];
			para = {
				BatchId: null,
				ClassYearId: null,
				SemesterId:null,
				ClassId: null,
				SectionId: null,
				EmployeeId: $scope.newClasswise.EmployeeId,
				SubjectId: null,
				ReportType: 2
			};
		}
		else if (rType == 3) {
			$scope.DataList3 = [];
			para = {
				BatchId: null,
				ClassYearId: null,
				SemesterId: null,
				ClassId: null,
				SectionId: null,
				EmployeeId: null,
				SubjectId: $scope.newClasswise.SubjectId,
				ReportType: 3
			};
		}
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetLessonPlanByClass",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				if (rType == 1)
					$scope.DataList1 = res.data.Data;
				else if (rType == 2)
					$scope.DataList2 = res.data.Data;
				else if (rType == 3)
					$scope.DataList3 = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
    }

	$scope.CurCW = null;
	$scope.ClassWiseDetails = function (lessionPlan) {

			$scope.CurCW = lessionPlan;
			document.getElementById('classwiseList').style.display = "none";
		document.getElementById('classwisestatus-form').style.display = "block";
	}

	$scope.CurCW1 = null;
	$scope.ClassWiseDetails1 = function (lessionPlan) {

		$scope.CurCW1 = lessionPlan;
			document.getElementById('teacherwiselist').style.display = "none";
			document.getElementById('teacherwisestatus-form').style.display = "block";
	}

	$scope.CurCW2 = null;
	$scope.ClassWiseDetails2 = function (lessionPlan) {

		$scope.CurCW2 = lessionPlan;
		document.getElementById('subjectlist').style.display = "none";
		document.getElementById('subjectstatus-form').style.display = "block";
	}
});