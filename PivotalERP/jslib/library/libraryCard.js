app.controller('LibraryCardController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Library Card';

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.entity = {
			student: entityStudent,
			employee: entityEmployee

		};
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();
		$scope.searchData = {
			Student: '',
			Employee: '',
		};

		$scope.newStudent = {
			StudentId: null,
			Mode: 'Save'
		};

		$scope.newEmployee = {
			EmployeeId: null,
			Mode: 'Save'
		};

		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();


		$scope.newPrintStudentCard = {
			ClassId: null,
			SectionId: null,
			ValidFrom: null,
			ValidTo: null,
			StudentIdColl: null,
			TemplatesColl: [],
			SelectStudent: $scope.StudentSearchOptions[0].value
		};

		$scope.newPrintEmployeeCard = {
			ClassId: null,
			SectionId: null,
			ValidFrom: null,
			ValidTo: null,
			StudentIdColl: null,
			TemplatesColl: [],
			SelectStudent: $scope.StudentSearchOptions[0].value
		};

		$scope.ClassSection = [];

		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.AllStudentColl = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllStudentForTran",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.AllStudentColl = res.data.Data;

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.LoadReportTemplates();
	}

	$scope.LoadReportTemplates = function () {

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.student + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newPrintStudentCard.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.employee + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newPrintEmployeeCard.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	$scope.PrintNormalIdCard = function () {

		if (!$scope.PrintNormalIdCard)
			return;

		if ($scope.newPrintStudentCard.SelectedClass && $scope.newPrintStudentCard.RptTranId) {
			var EntityId = $scope.entity.student;

			var template = mx($scope.newPrintStudentCard.TemplatesColl).firstOrDefault(p1 => p1.RptTranId == $scope.newPrintStudentCard.RptTranId);

			if ($scope.newPrintStudentCard.RptTranId > 0) {

				var vFrom = null, vTo = null;
				if ($scope.newPrintStudentCard.IssueDateDet)
					vFrom = $filter('date')(new Date($scope.newPrintStudentCard.IssueDateDet.dateAD), 'yyyy-MM-dd');

				if ($scope.newPrintStudentCard.ValidUptoDet)
					vTo = $filter('date')(new Date($scope.newPrintStudentCard.ValidUptoDet.dateAD), 'yyyy-MM-dd');

				var rptPara = {
					rpttranid: $scope.newPrintStudentCard.RptTranId,
					istransaction: false,
					entityid: EntityId,
					ClassId: $scope.newPrintStudentCard.SelectedClass.ClassId,
					SectionId: $scope.newPrintStudentCard.SelectedClass.SectionId,
					StudentIdColl: ($scope.newPrintStudentCard.StudentIdColl ? $scope.newPrintStudentCard.StudentIdColl.toString() : ''),
					ValidFrom: vFrom,
					ValidTo: vTo,
					DuesAmt: 0
				};
				var paraQuery = param(rptPara);
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';

				if (template.IsRDLC == true)
					document.getElementById("frmRptTabulation").src = base_url + "Academic/Report/RdlIdentityCard?" + paraQuery;
				else
					document.getElementById("frmRptTabulation").src = base_url + "web/ReportViewer.aspx?" + paraQuery;

				document.body.style.cursor = 'default';
			} else {
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';
				document.body.style.cursor = 'default';
			}
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptTabulation").src = '';
			document.body.style.cursor = 'default';
			$scope.PrintStudentIdCard();
		}

	};

	$scope.PrintStudentIdCard = function () {

		if (!$scope.PrintNormalIdCard)
			return;

		if ($scope.newPrintStudentCard.RptTranId && $scope.newPrintStudentCard.StudentIdColl) {
			var EntityId = $scope.entity.student;

			if ($scope.newPrintStudentCard.RptTranId > 0) {

				var template = mx($scope.newPrintStudentCard.TemplatesColl).firstOrDefault(p1 => p1.RptTranId == $scope.newPrintStudentCard.RptTranId);

				var vFrom = null, vTo = null;
				if ($scope.newPrintStudentCard.IssueDateDet)
					vFrom = $filter('date')(new Date($scope.newPrintStudentCard.IssueDateDet.dateAD), 'yyyy-MM-dd');

				if ($scope.newPrintStudentCard.ValidUptoDet)
					vTo = $filter('date')(new Date($scope.newPrintStudentCard.ValidUptoDet.dateAD), 'yyyy-MM-dd');

				var rptPara = {
					rpttranid: $scope.newPrintStudentCard.RptTranId,
					istransaction: false,
					entityid: EntityId,
					ClassId: 0,
					SectionId: 0,
					StudentIdColl: ($scope.newPrintStudentCard.StudentIdColl ? $scope.newPrintStudentCard.StudentIdColl.toString() : ''),
					ValidFrom: vFrom,
					ValidTo: vTo,
					DuesAmt: 0
				};
				var paraQuery = param(rptPara);
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';
				if (template.IsRDLC == true)
					document.getElementById("frmRptTabulation").src = base_url + "Academic/Report/RdlIdentityCard?" + paraQuery;
				else
					document.getElementById("frmRptTabulation").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
				document.body.style.cursor = 'default';
			} else {
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';
				document.body.style.cursor = 'default';
			}
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptTabulation").src = '';
			document.body.style.cursor = 'default';
		}

	};

	$scope.validateDates = function (obj,changedField) {
		if (!obj.IssueDateDet || !obj.ValidUptoDet ||
			!obj.IssueDateDet.dateAD || !obj.ValidUptoDet.dateAD) {
			return true;
		}

		var fromDate = $filter('date')(new Date(obj.IssueDateDet.dateAD), 'yyyy-MM-dd');
		var toDate = $filter('date')(new Date(obj.ValidUptoDet.dateAD), 'yyyy-MM-dd');

		if (!fromDate || !toDate) return true;

		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'Issue Date cannot be After ValidUpto Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.IssueDate_TMP = new Date();
						obj.IssueDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'ValidUpto Date cannot be Before Issue Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.ValidUpto_TMP = new Date();
						obj.ValidUptoDet = new Date();
					});
				});
			}
			return false;
		}
		return true;
	};


});