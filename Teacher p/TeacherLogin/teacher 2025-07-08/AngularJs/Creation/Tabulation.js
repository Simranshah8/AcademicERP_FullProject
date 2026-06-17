app.controller('tabulationController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Tabulation';

	function param(object) {
		return Object.keys(object).map((key) => {
			return key + '=' + encodeURIComponent(object[key])
		}).join('&');
	}
	$scope.LoadData = function () {
		$('.select2').select2();
	
		$scope.normalTabulation = {
			SelectClassSection: null,
			ExamTypeId: null
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


	}

	$scope.PrintNormalTabulation = function () {
		if ($scope.normalTabulation.SelectedClass && $scope.normalTabulation.ExamTypeId) {

			var EntityId = 0;// $scope.entity.Tabulation;

			var examN = mx($scope.ExamTypeColl).firstOrDefault(p1 => p1.ExamTypeId == $scope.normalTabulation.ExamTypeId);

			var rptPara = {
				ClassId: $scope.normalTabulation.SelectedClass.ClassId,
				SectionId: $scope.normalTabulation.SelectedClass.SectionId,
				ExamTypeId: $scope.normalTabulation.ExamTypeId,
				ClassName: $scope.normalTabulation.SelectedClass.text,
				FilterSection: true,
				ExamName: (examN ? examN.Name : ''),
				IsGroup: false
			};
			var paraQuery = param(rptPara);

			$(document).ready(function () {
				$('body').css('cursor', 'wait');
			});
			$scope.loadingstatus = 'running';
			document.getElementById("frmRptTabulation").src = '';
			document.getElementById("frmRptTabulation").style.width = '100%';
			document.getElementById("frmRptTabulation").style.height = '1300px';
			document.getElementById("frmRptTabulation").style.visibility = 'visible';
			document.getElementById("frmRptTabulation").src = base_url + "Examination/Reporting/RdlTabulation?" + paraQuery;
			$('#frmRptTabulation').on('load', function () {
				$('body').css('cursor', 'default');
			});
		}

	};

	$scope.PrintGroupTabulation = function () {
		if ($scope.groupTabulation.SelectedClass && $scope.groupTabulation.ExamTypeGroupId) {

			var EntityId = 0;// $scope.entity.GroupTabulation;

			var examN = mx($scope.ExamTypeGroupColl).firstOrDefault(p1 => p1.ExamTypeGroupId == $scope.groupTabulation.ExamTypeGroupId);

			var rptPara = {
				ClassId: $scope.groupTabulation.SelectedClass.ClassId,
				SectionId: $scope.groupTabulation.SelectedClass.SectionId,
				ExamTypeId: $scope.groupTabulation.ExamTypeGroupId,
				ClassName: $scope.groupTabulation.SelectedClass.text,
				FilterSection: true,
				ExamName: (examN ? examN.Name : ''),
				IsGroup:true
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptGrpTabulation").src = '';
			document.getElementById("frmRptGrpTabulation").style.width = '100%';
			document.getElementById("frmRptGrpTabulation").style.height = '1300px';
			document.getElementById("frmRptGrpTabulation").style.visibility = 'visible';
			document.getElementById("frmRptGrpTabulation").src = base_url + "Examination/Reporting/RdlTabulation?" + paraQuery;

		}

	};
});