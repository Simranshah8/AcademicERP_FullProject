

app.controller('casTabulationRptController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Tabulation';

	var gSrv = GlobalServices;

	$scope.LoadData = function () {

		$scope.entity = {
			Tabulation: 405,			 
		};
		$('.select2').select2();

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


		$scope.ExamTypeGroupList = [];
		gSrv.getExamTypeGroupList().then(function (res) {
			$scope.ExamTypeGroupList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		gSrv.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data.Data);
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
 

		$scope.LoadReportTemplates();
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

		 
	}

	$scope.GetClassWiseSubMap = function () {

		$scope.newNormalTabulation.SubjectList = [];	 
		if ($scope.newNormalTabulation.SelectedClass && $scope.newNormalTabulation.SelectedClass.length > 0) {
			var selectedClass = $scope.newNormalTabulation.SelectedClass[0];
			
			var para = {
				ClassId: selectedClass.ClassId,
				SectionIdColl: (selectedClass.SectionId ? selectedClass.SectionId.toString() : '')
			};

			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetSubjectMappingClassWise",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var SubjectMappingColl = res.data.Data;

					if (SubjectMappingColl.length == 0) {
						Swal.fire('Subject Mapping Not Found');
					}
					else if (SubjectMappingColl.length > 0) {
						angular.forEach(SubjectMappingColl, function (sm) {
							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								subDet.PaperType = sm.PaperType;
								$scope.newNormalTabulation.SubjectList.push(subDet);
							}
						});

					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.PrintNormalTabulation = function () {
		if ($scope.newNormalTabulation.SelectedClass   && $scope.newNormalTabulation.RptTranId > 0) {

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

			var rptPara = {			 
				ClassId: tmpCS.ClassId,
				SectionId: tmpCS.SectionId,
				ExamTypeId: $scope.newNormalTabulation.ExamTypeId,
				ClassName: tmpCS.text,
				FilterSection: $scope.newNormalTabulation.FilterSection,
				ExamName: (examN ? examN.DisplayName : ''),
				rptTranId: $scope.newNormalTabulation.RptTranId,
				classIdColl: cIdColl,
				CASTypeId: 0,
				SubjectId:$scope.newNormalTabulation.SubjectId
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptTabulation").src = '';
			document.getElementById("frmRptTabulation").style.width = '100%';
			document.getElementById("frmRptTabulation").style.height = '1300px';
			document.getElementById("frmRptTabulation").style.visibility = 'visible';
			document.getElementById("frmRptTabulation").src = base_url + "Exam/Report/RdlCASTabulation?" + paraQuery;

		}

	};

 
});