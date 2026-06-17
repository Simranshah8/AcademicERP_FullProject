app.controller('tabulationController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Tabulation';

	function param(object) {
		return Object.keys(object).map((key) => {
			return key + '=' + encodeURIComponent(object[key])
		}).join('&');
	}
	$scope.LoadData = function () {
		$scope.entity = {
			Tabulation: 359,
			GroupTabulation: 361,
			ParentTabulation: 0,
			ReExamTabulation:411
		};

		$('.select2').select2();
	
		$scope.normalTabulation = {
			SelectClassSection: null,
            ExamTypeId: null,
            FilterSection: true,
            FromPublished: false,
            TemplatesColl: []
        };

		$scope.groupTabulation = {
			SelectClassSection: null,
			ExamTypeId: null,
			FilterSection: true,
			FromPublished: false,
			TemplatesColl:[]
		};
		//Get class and Section List
		$scope.ClassColl = [];
		$scope.SectionColl = [];
		$http.get(base_url + "StudentAttendance/Creation/GetClassSection")
			.then(function (data) {
				$scope.ClassColl = data.data.ClassList;
				$scope.SectionClassColl = data.data.SectionList;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				alert("Couldn't find data");
			});

		// Calling ExamTypeList
		$scope.ExamTypeColl = [];
		$http.get(base_url + "OnlineExam/Creation/GetExamTypeList")
			.then(function (data) {
				$scope.ExamTypeColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});

		$scope.ExamTypeGroupColl = [];
		$http.get(base_url + "OnlineExam/Creation/GetExamTypeGroupList")
			.then(function (data) {
				$scope.ExamTypeGroupColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});
		$scope.LoadReportTemplates();

	}
	//$scope.GetBranchId = function (bid) {
	//	if ($scope.BranchColl.length == 1)
	//		return $scope.BranchColl[0].BranchId
	//	else
	//		return bid;
	//}

	$scope.LoadReportTemplates = function () {

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.Tabulation + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data)
				$scope.normalTabulation.TemplatesColl = res.data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.ReExamTabulation + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data)
				$scope.normalTabulationRE.TemplatesColl = res.data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.GroupTabulation + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data)
				$scope.groupTabulation.TemplatesColl = res.data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.PrintNormalTabulation = function () {

		//if ($scope.normalTabulation.SelectedClass) {
		//	$scope.LoadClassWiseSemesterYear($scope.normalTabulation.SelectedClass[0].ClassId, $scope.normalTabulation);
		//}

		if ($scope.normalTabulation.SelectedClass && $scope.normalTabulation.ExamTypeId && $scope.normalTabulation.RptTranId ) {

			var EntityId = $scope.entity.Tabulation;

			var tmpCS = $scope.normalTabulation.SelectedClass[0];

			var tmpIdColl = [];
			var cIdColl = '';
			angular.forEach($scope.normalTabulation.SelectedClass, function (cl) {

				if (mx(tmpIdColl).contains(cl.ClassId) == false)
					tmpIdColl.push(cl.ClassId);
			});

			cIdColl = tmpIdColl.toString();

			if ($scope.normalTabulation.SelectedClass.length == 1)
				cIdColl = '';

			var classN = mx($scope.SectionClassColl).firstOrDefault(p1 => p1.ClassId == $scope.normalTabulation.SelectedClass.ClassId);
			var examN = mx($scope.ExamTypeColl).firstOrDefault(p1 => p1.ExamTypeId == $scope.normalTabulation.ExamTypeId);

			var rptPara = {
				ClassId: $scope.normalTabulation.SelectedClass.ClassId,
				SectionId: $scope.normalTabulation.SelectedClass.SectionId,
				ExamTypeId: $scope.normalTabulation.ExamTypeId,
				ClassName: classN.text,
				FilterSection: $scope.normalTabulation.FilterSection,
				ExamName: (examN ? examN.DisplayName : ''),
				rptTranId: $scope.normalTabulation.RptTranId,
				classIdColl: (cIdColl == '0' ? '' : cIdColl),
				BatchId: ($scope.normalTabulation.BatchId ? $scope.normalTabulation.BatchId : 0),
				SemesterId: ($scope.normalTabulation.SemesterId ? $scope.normalTabulation.SemesterId : 0),
				ClassYearId: ($scope.normalTabulation.ClassYearId ? $scope.normalTabulation.ClassYearId : 0),
				FromPublished: ($scope.normalTabulation.FromPublished ? $scope.normalTabulation.FromPublished : false),
				BranchId: ($scope.normalTabulation.BranchId ? $scope.normalTabulation.BranchId : 1),
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptTabulation").src = '';
			document.getElementById("frmRptTabulation").style.width = '100%';
			document.getElementById("frmRptTabulation").style.height = '1300px';
			document.getElementById("frmRptTabulation").style.visibility = 'visible';
			document.getElementById("frmRptTabulation").src = base_url + "Examination/Reporting/RldTabulation?" + paraQuery;
			$('#frmRptTabulation').on('load', function () {
                $('body').css('cursor', 'default');
			});
		}

	};

	$scope.PrintGroupTabulation = function () {

		//if ($scope.groupTabulation.SelectedClass) {
		//	$scope.LoadClassWiseSemesterYear($scope.groupTabulation.SelectedClass[0].ClassId, $scope.groupTabulation);
		//}

		if ($scope.groupTabulation.SelectedClass && $scope.groupTabulation.ExamTypeGroupId && $scope.groupTabulation.RptTranId) {

			var EntityId = $scope.entity.GroupTabulation;

			var classN = mx($scope.SectionClassColl).firstOrDefault(p1 => p1.ClassId == $scope.groupTabulation.SelectedClass.ClassId);
			var examN = mx($scope.ExamTypeGroupColl).firstOrDefault(p1 => p1.ExamTypeGroupId == $scope.groupTabulation.ExamTypeGroupId);

			var rptPara = {
				ClassId: $scope.groupTabulation.SelectedClass.ClassId,
				SectionId: $scope.groupTabulation.SelectedClass.SectionId,
				ExamTypeId: $scope.groupTabulation.ExamTypeGroupId,
				ClassName: classN.text,
				FilterSection: $scope.groupTabulation.FilterSection,
				ExamName: (examN ? examN.DisplayName : ''),
				rptTranId: $scope.groupTabulation.RptTranId,
				CurExamTypeId: ($scope.groupTabulation.CurExamTypeId ? $scope.groupTabulation.CurExamTypeId : 0),
				BatchId: ($scope.groupTabulation.BatchId ? $scope.groupTabulation.BatchId : 0),
				SemesterId: ($scope.groupTabulation.SemesterId ? $scope.groupTabulation.SemesterId : 0),
				ClassYearId: ($scope.groupTabulation.ClassYearId ? $scope.groupTabulation.ClassYearId : 0),
				FromPublished: ($scope.groupTabulation.FromPublished ? $scope.groupTabulation.FromPublished : false),
				BranchId: ($scope.groupTabulation.BranchId ? $scope.groupTabulation.BranchId : 1),
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptGrpTabulation").src = '';
			document.getElementById("frmRptGrpTabulation").style.width = '100%';
			document.getElementById("frmRptGrpTabulation").style.height = '1300px';
			document.getElementById("frmRptGrpTabulation").style.visibility = 'visible';
			document.getElementById("frmRptGrpTabulation").src = base_url + "Examination/Reporting/RdlGroupTabulation?" + paraQuery;
			$('#frmRptGrpTabulation').on('load', function () {
				$('body').css('cursor', 'default');
			});
		}

	};



	//$scope.PrintGroupTabulation = function () {
	//	if ($scope.groupTabulation.SelectedClass && $scope.groupTabulation.ExamTypeGroupId) {

	//		var EntityId = 0;// $scope.entity.GroupTabulation;

	//		var examN = mx($scope.ExamTypeGroupColl).firstOrDefault(p1 => p1.ExamTypeGroupId == $scope.groupTabulation.ExamTypeGroupId);

	//		var rptPara = {
	//			ClassId: $scope.groupTabulation.SelectedClass.ClassId,
	//			SectionId: $scope.groupTabulation.SelectedClass.SectionId,
	//			ExamTypeId: $scope.groupTabulation.ExamTypeGroupId,
	//			ClassName: $scope.groupTabulation.SelectedClass.text,
	//			FilterSection: true,
	//			ExamName: (examN ? examN.Name : ''),
	//			IsGroup:true
	//		};
	//		var paraQuery = param(rptPara);

	//		$scope.loadingstatus = 'running';
	//		document.getElementById("frmRptGrpTabulation").src = '';
	//		document.getElementById("frmRptGrpTabulation").style.width = '100%';
	//		document.getElementById("frmRptGrpTabulation").style.height = '1300px';
	//		document.getElementById("frmRptGrpTabulation").style.visibility = 'visible';
	//		document.getElementById("frmRptGrpTabulation").src = base_url + "Examination/Reporting/RdlTabulation?" + paraQuery;

	//	}

	//};
});