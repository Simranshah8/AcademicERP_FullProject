

app.controller('tabulationRptController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Tabulation';

	var gSrv = GlobalServices;

	$scope.LoadData = function () {

		$scope.entity = {
			Tabulation: 359,
			GroupTabulation: 361,
			ParentTabulation: 0
		};
		$('.select2').select2();

		$scope.BranchColl = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchListForEntry",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchColl = res.data.Data;
			}
		}, function (reason) {
			alert('Failed' + reason);
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
				$scope.SemesterList_Qry = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
					$scope.SemesterList_Qry = mx(res.data.Data);
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveBatch == true) {

				$scope.BatchList = [];
				$scope.BatchList_Qry = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
					$scope.BatchList_Qry = mx(res.data.Data);
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveClassYear == true) {

				$scope.ClassYearList = [];
				$scope.ClassYearList_Qry = [];
				$scope.SelectedClassClassYearList = [];
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
					$scope.ClassYearList_Qry = mx(res.data.Data);
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ExamTypeList = [];
		gSrv.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ReExamTypeList = [];
		gSrv.getReExamTypeList().then(function (res) {
			$scope.ReExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ExamTypeGroupList = [];
		gSrv.getExamTypeGroupList().then(function (res) {
			$scope.ExamTypeGroupList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newNormalTabulation = {
			SelectedClass: null,
			ExamTypeId: null,
			FilterSection: true,
			TemplatesColl: [],
			RptTranId: 0
		};

		$scope.newNormalTabulationRE = {
			SelectedClass: null,
			ExamTypeId: null,
			ReExamTypeId: null,
			FilterSection: true,
			TemplatesColl: [],
			RptTranId: 0
		};

		$scope.newGroupTabulation = {
			SelectedClass: null,
			ExamTypeId: null,
			FilterSection: true,
			TemplatesColl: [],
			RptTranId: 0,
			CurExamTypeId:0
		};

		$scope.LoadReportTemplates();
	}

	$scope.GetBranchId = function (bid) {
		if ($scope.BranchColl.length == 1)
			return $scope.BranchColl[0].BranchId
		else
			return bid;
	}

	$scope.LoadReportTemplates = function () {

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.Tabulation + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newNormalTabulation.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityReExamTabulation+ "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newNormalTabulationRE.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.GroupTabulation + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newGroupTabulation.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.LoadClassWiseSemesterYear = function (classId, data) {

		$scope.SelectedClassClassYearList = [];
		$scope.SelectedClassSemesterList = [];
		$scope.SelectedClass = mx($scope.ClassList.ClassList).firstOrDefault(p1 => p1.ClassId == classId);

		if ($scope.SelectedClass) {
			var semQry = mx($scope.SelectedClass.ClassSemesterIdColl);
			var cyQry = mx($scope.SelectedClass.ClassYearIdColl);

			angular.forEach($scope.SemesterList, function (sem) {
				if (semQry.contains(sem.id)) {
					$scope.SelectedClassSemesterList.push({
						id: sem.id,
						text: sem.text,
						SemesterId: sem.id,
						Name: sem.Name
					});
				}
			});

			angular.forEach($scope.ClassYearList, function (sem) {
				if (cyQry.contains(sem.id)) {
					$scope.SelectedClassClassYearList.push({
						id: sem.id,
						text: sem.text,
						ClassYearId: sem.id,
						Name: sem.Name
					});
				}
			});
		}

	};
	$scope.PrintNormalTabulation = function () {

		if ($scope.newNormalTabulation.SelectedClass) {
			$scope.LoadClassWiseSemesterYear($scope.newNormalTabulation.SelectedClass[0].ClassId, $scope.newNormalTabulation);
        }

		if ($scope.newNormalTabulation.SelectedClass && $scope.newNormalTabulation.ExamTypeId && $scope.newNormalTabulation.RptTranId && $scope.newNormalTabulation.SelectedClass.length>0) {

			var EntityId = $scope.entity.Tabulation;

			var tmpCS = $scope.newNormalTabulation.SelectedClass[0];

			var tmpIdColl = [];
			var cIdColl = '';
			angular.forEach($scope.newNormalTabulation.SelectedClass, function (cl) {

				if (mx(tmpIdColl).contains(cl.ClassId) == false)
					tmpIdColl.push(cl.ClassId);
			});

			cIdColl = tmpIdColl.toString();

			if ($scope.newNormalTabulation.SelectedClass.length == 1)
				cIdColl = '';

			var examN = mx($scope.ExamTypeList).firstOrDefault(p1 => p1.ExamTypeId == $scope.newNormalTabulation.ExamTypeId);
			var fs = $scope.newNormalTabulation.FilterSection;

			var rptPara = {
				//ClassId: $scope.newNormalTabulation.SelectedClass.ClassId,
				//SectionId: $scope.newNormalTabulation.SelectedClass.SectionId,
				ClassId: tmpCS.ClassId,
				SectionId: tmpCS.SectionId,
				ExamTypeId: $scope.newNormalTabulation.ExamTypeId,
				ClassName: (fs==false ? tmpCS.ClassName : tmpCS.text),
				FilterSection: $scope.newNormalTabulation.FilterSection,
				ExamName: (examN ? examN.DisplayName : ''),
				rptTranId: $scope.newNormalTabulation.RptTranId,
				classIdColl: (cIdColl == '0' ? '' : cIdColl),
				BatchId: ($scope.newNormalTabulation.BatchId ? $scope.newNormalTabulation.BatchId : 0),
				SemesterId: ($scope.newNormalTabulation.SemesterId ? $scope.newNormalTabulation.SemesterId : 0),
				ClassYearId: ($scope.newNormalTabulation.ClassYearId ? $scope.newNormalTabulation.ClassYearId : 0),
				FromPublished: ($scope.newNormalTabulation.FromPublished ? $scope.newNormalTabulation.FromPublished : false),
				BranchId: ($scope.newNormalTabulation.BranchId ? $scope.newNormalTabulation.BranchId : 0),
				Batch: ($scope.newNormalTabulation.BatchId ? $scope.BatchList_Qry.firstOrDefault(p1=>p1.BatchId==$scope.newNormalTabulation.BatchId).Name : ''),
				Semester: ($scope.newNormalTabulation.SemesterId ? $scope.SemesterList_Qry.firstOrDefault(p1 => p1.SemesterId == $scope.newNormalTabulation.SemesterId).Name : ''),
				ClassYear: ($scope.newNormalTabulation.ClassYearId ? $scope.ClassYearList_Qry.firstOrDefault(p1 => p1.ClassYearId == $scope.newNormalTabulation.ClassYearId).Name : ''),
			};			
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptTabulation").src = '';
			document.getElementById("frmRptTabulation").style.width = '100%';
			document.getElementById("frmRptTabulation").style.height = '1300px';
			document.getElementById("frmRptTabulation").style.visibility = 'visible';		
			document.getElementById("frmRptTabulation").src = base_url + "Exam/Report/RldTabulation?"+paraQuery;

		}		

	};

	$scope.PrintNormalTabulationRE = function () {
		if ($scope.newNormalTabulationRE.SelectedClass && $scope.newNormalTabulationRE.ExamTypeId && $scope.newNormalTabulationRE.ReExamTypeId && $scope.newNormalTabulationRE.RptTranId && $scope.newNormalTabulationRE.SelectedClass.length > 0)
		{

			var EntityId = entityReExamTabulation;


			var tmpCS = $scope.newNormalTabulationRE.SelectedClass[0];

			var tmpIdColl = [];
			var cIdColl = '';
			angular.forEach($scope.newNormalTabulationRE.SelectedClass, function (cl) {

				if (mx(tmpIdColl).contains(cl.ClassId) == false)
					tmpIdColl.push(cl.ClassId);
			});

			cIdColl = tmpIdColl.toString();

			if ($scope.newNormalTabulationRE.SelectedClass.length == 1)
				cIdColl = '';

			var examN = mx($scope.ExamTypeList).firstOrDefault(p1 => p1.ExamTypeId == $scope.newNormalTabulationRE.ExamTypeId);
			var reExamN = mx($scope.ReExamTypeList).firstOrDefault(p1 => p1.ReExamTypeId == $scope.newNormalTabulationRE.ReExamTypeId);

			var fs = $scope.newNormalTabulationRE.FilterSection;
			

			var rptPara = {
				//ClassId: $scope.newNormalTabulation.SelectedClass.ClassId,
				//SectionId: $scope.newNormalTabulation.SelectedClass.SectionId,
				ClassId: tmpCS.ClassId,
				SectionId: tmpCS.SectionId,
				ExamTypeId: $scope.newNormalTabulationRE.ExamTypeId,
				ReExamTypeId: $scope.newNormalTabulationRE.ReExamTypeId,
				ClassName: (fs == false ? tmpCS.ClassName : tmpCS.text),
				FilterSection: $scope.newNormalTabulation.FilterSection,
				ExamName: (examN ? examN.DisplayName : ''),
				ReExamName: (examN ? reExamN.DisplayName : ''),
				rptTranId: $scope.newNormalTabulationRE.RptTranId,
				classIdColl: (cIdColl=='0' ? '' : cIdColl), 
				BatchId: ($scope.newNormalTabulationRE.BatchId ? $scope.newNormalTabulationRE.BatchId : 0),
				SemesterId: ($scope.newNormalTabulationRE.SemesterId ? $scope.newNormalTabulationRE.SemesterId : 0),
				ClassYearId: ($scope.newNormalTabulationRE.ClassYearId ? $scope.newNormalTabulationRE.ClassYearId : 0),
				FromPublished: ($scope.newNormalTabulationRE.FromPublished ? $scope.newNormalTabulationRE.FromPublished : false),
				BranchId: ($scope.newNormalTabulationRE.BranchId ? $scope.newNormalTabulationRE.BranchId : 0),

				Batch: ($scope.newNormalTabulationRE.BatchId ? $scope.BatchList_Qry.firstOrDefault(p1 => p1.BatchId == $scope.newNormalTabulationRE.BatchId).Name : ''),
				Semester: ($scope.newNormalTabulationRE.SemesterId ? $scope.SemesterList_Qry.firstOrDefault(p1 => p1.SemesterId == $scope.newNormalTabulationRE.SemesterId).Name : ''),
				ClassYear: ($scope.newNormalTabulationRE.ClassYearId ? $scope.ClassYearList_Qry.firstOrDefault(p1 => p1.ClassYearId == $scope.newNormalTabulationRE.ClassYearId).Name : ''),

			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptTabulationRE").src = '';
			document.getElementById("frmRptTabulationRE").style.width = '100%';
			document.getElementById("frmRptTabulationRE").style.height = '1300px';
			document.getElementById("frmRptTabulationRE").style.visibility = 'visible';
			document.getElementById("frmRptTabulationRE").src = base_url + "Exam/Report/RdlReTabulation?" + paraQuery;

		}

	};

	$scope.PrintGroupTabulation = function () {
		if ($scope.newGroupTabulation.SelectedClass && $scope.newGroupTabulation.ExamTypeGroupId && $scope.newGroupTabulation.RptTranId) {

			var EntityId = $scope.entity.GroupTabulation;

			var examN = mx($scope.ExamTypeGroupList).firstOrDefault(p1 => p1.ExamTypeGroupId == $scope.newGroupTabulation.ExamTypeGroupId);

			var fs = $scope.newGroupTabulation.FilterSection;			
			var tmpCS = $scope.newGroupTabulation.SelectedClass;

			var rptPara = {
				ClassId: $scope.newGroupTabulation.SelectedClass.ClassId,
				SectionId: $scope.newGroupTabulation.SelectedClass.SectionId,
				ExamTypeId: $scope.newGroupTabulation.ExamTypeGroupId,
				ClassName: (fs == false ? tmpCS.ClassName : tmpCS.text),
				FilterSection: $scope.newGroupTabulation.FilterSection,				
				ExamName: (examN ? examN.DisplayName : ''),
				rptTranId: $scope.newGroupTabulation.RptTranId,
				CurExamTypeId: ($scope.newGroupTabulation.CurExamTypeId ? $scope.newGroupTabulation.CurExamTypeId : 0),				
				BatchId: ($scope.newGroupTabulation.BatchId ? $scope.newGroupTabulation.BatchId : 0),
				SemesterId: ($scope.newGroupTabulation.SemesterId ? $scope.newGroupTabulation.SemesterId : 0),
				ClassYearId: ($scope.newGroupTabulation.ClassYearId ? $scope.newGroupTabulation.ClassYearId : 0),
				FromPublished: ($scope.newGroupTabulation.FromPublished ? $scope.newGroupTabulation.FromPublished : false),
				BranchId: ($scope.newGroupTabulation.BranchId ? $scope.newGroupTabulation.BranchId : 0),

				Batch: ($scope.newGroupTabulation.BatchId ? $scope.BatchList_Qry.firstOrDefault(p1 => p1.BatchId == $scope.newGroupTabulation.BatchId).Name : ''),
				Semester: ($scope.newGroupTabulation.SemesterId ? $scope.SemesterList_Qry.firstOrDefault(p1 => p1.SemesterId == $scope.newGroupTabulation.SemesterId).Name : ''),
				ClassYear: ($scope.newGroupTabulation.ClassYearId ? $scope.ClassYearList_Qry.firstOrDefault(p1 => p1.ClassYearId == $scope.newGroupTabulation.ClassYearId).Name : ''),
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptGrpTabulation").src = '';
			document.getElementById("frmRptGrpTabulation").style.width = '100%';
			document.getElementById("frmRptGrpTabulation").style.height = '1300px';
			document.getElementById("frmRptGrpTabulation").style.visibility = 'visible';
			document.getElementById("frmRptGrpTabulation").src = base_url + "Exam/Report/RdlGroupTabulation?" + paraQuery;

		}

	};
});