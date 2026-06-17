app.controller('IdentityCardController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'IdentityCard';


	$scope.LoadData = function () {

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
				$scope.SelectedClassClassYearList = [];
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.entity = {
			SIdCard: 372,
			EIdCard: 373
		};

		$('.select2').select2();

		$scope.MonthList = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassSection = [];

		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();


		//Added for Employee Id Card by Suresh on Mangsir 11
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		$scope.DepartmentList = [];
		GlobalServices.getDepartmentList().then(function (res) {
			$scope.DepartmentList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.DesignationList = [];
		GlobalServices.getDesignationList().then(function (res) {
			$scope.DesignationList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newIdentityCard = {
			IdentityCardId: null,
			GenerateQR: false,
			DepartmentId: 0,
			DesignationId: 0,
			IssueDate_TMP: new Date(),
			ValidUpto_TMP: new Date(),
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,

		};

		$scope.EmpListForSelectionColl = [];
		//For 1=All,2=Continue,3=Left
		var empPara = {
			For: 2
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllEmpForSelection",
			dataType: "json",
			data: JSON.stringify(empPara)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.EmpListForSelectionColl = res.data.Data;
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.LoadReportTemplatesEmp();
		//Ends for Employee On load

		$scope.newPrintIdCard = {
			ClassId: null,
			SectionId: null,
			ValidFrom: null,
			ValidTo: null,
			StudentIdColl: null,
			TemplatesColl: [],
			GenerateQR: false,
			For: 0,
			RollNoFrom: 0,
			RollNoTo: 0,
			OnlyPhotoStudent: false,
			IssueDate_TMP: new Date(),
			ValidUpto_TMP: new Date(),
			SelectStudent: $scope.StudentSearchOptions[0].value
		};

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

	$scope.TestClick = function (v) {
		v.GenerateQR = !v.GenerateQR;
	}

	$scope.LoadReportTemplates = function () {

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.SIdCard + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newPrintIdCard.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$http({
		//	method: 'GET',
		//	url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.EIdCard + "&voucherId=0&isTran=false",
		//	dataType: "json"
		//}).then(function (res) {
		//	if (res.data.IsSuccess && res.data.Data)
		//		$scope.newIdentityCard.TemplatesColl = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});
	}


	$scope.LoadClassWiseSemesterYear = function (classId, data) {

		$scope.SelectedClassClassYearList = [];
		$scope.SelectedClassSemesterList = [];
		$scope.SelectedClass = mx($scope.ClassSection.ClassList).firstOrDefault(p1 => p1.ClassId == classId);

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
	$scope.PrintNormalIdCard = function (fromInd) {

		if (fromInd == 2 && $scope.newPrintIdCard.SelectedClass) {
			$scope.newPrintIdCard.SemesterId = null;
			$scope.newPrintIdCard.ClassYearId = null;
			$scope.LoadClassWiseSemesterYear($scope.newPrintIdCard.SelectedClass.ClassId, $scope.newPrintIdCard);
		}

		if ($scope.newPrintIdCard.SelectedClass && $scope.newPrintIdCard.RptTranId) {
			var EntityId = $scope.entity.SIdCard;



			var template = mx($scope.newPrintIdCard.TemplatesColl).firstOrDefault(p1 => p1.RptTranId == $scope.newPrintIdCard.RptTranId);

			if ($scope.newPrintIdCard.RptTranId > 0) {

				var vFrom = null, vTo = null;
				if ($scope.newPrintIdCard.IssueDateDet)
					vFrom = $filter('date')(new Date($scope.newPrintIdCard.IssueDateDet.dateAD), 'yyyy-MM-dd');

				if ($scope.newPrintIdCard.ValidUptoDet)
					vTo = $filter('date')(new Date($scope.newPrintIdCard.ValidUptoDet.dateAD), 'yyyy-MM-dd');

				var rptPara = {
					rpttranid: $scope.newPrintIdCard.RptTranId,
					istransaction: false,
					entityid: $scope.entity.SIdCard,
					ClassId: $scope.newPrintIdCard.SelectedClass.ClassId,
					SectionId: $scope.newPrintIdCard.SelectedClass.SectionId,
					StudentIdColl: ($scope.newPrintIdCard.StudentIdColl ? $scope.newPrintIdCard.StudentIdColl.toString() : ''),
					ValidFrom: vFrom,
					ValidTo: vTo,
					DuesAmt: 0,
					GenerateQR: $scope.newPrintIdCard.GenerateQR,
					For: $scope.newPrintIdCard.For,
					rollNoFrom: $scope.newPrintIdCard.RollNoFrom,
					rollNoTo: $scope.newPrintIdCard.RollNoTo,
					ForMonth: ($scope.newPrintIdCard.ForMonth > 0 ? $scope.newPrintIdCard.ForMonth : 0),
					SemesterId: ($scope.newPrintIdCard.SemesterId > 0 ? $scope.newPrintIdCard.SemesterId : 0),
					ClassYearId: ($scope.newPrintIdCard.ClassYearId > 0 ? $scope.newPrintIdCard.ClassYearId : 0),
					BatchId: ($scope.newPrintIdCard.BatchId > 0 ? $scope.newPrintIdCard.BatchId : 0),
					OnlyPhotoStudent: $scope.newPrintIdCard.OnlyPhotoStudent,
					ClassIdColl: ($scope.newPrintIdCard.ClassIdColl ? $scope.newPrintIdCard.ClassIdColl.toString() : '')
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
		if ($scope.newPrintIdCard.RptTranId && $scope.newPrintIdCard.StudentIdColl) {
			var EntityId = $scope.entity.SIdCard;

			if ($scope.newPrintIdCard.RptTranId > 0) {

				var template = mx($scope.newPrintIdCard.TemplatesColl).firstOrDefault(p1 => p1.RptTranId == $scope.newPrintIdCard.RptTranId);

				var vFrom = null, vTo = null;
				if ($scope.newPrintIdCard.IssueDateDet)
					vFrom = $filter('date')(new Date($scope.newPrintIdCard.IssueDateDet.dateAD), 'yyyy-MM-dd');

				if ($scope.newPrintIdCard.ValidUptoDet)
					vTo = $filter('date')(new Date($scope.newPrintIdCard.ValidUptoDet.dateAD), 'yyyy-MM-dd');

				var rptPara = {
					rpttranid: $scope.newPrintIdCard.RptTranId,
					istransaction: false,
					entityid: $scope.entity.SIdCard,
					ClassId: 0,
					SectionId: 0,
					StudentIdColl: ($scope.newPrintIdCard.StudentIdColl ? $scope.newPrintIdCard.StudentIdColl.toString() : ''),
					ValidFrom: vFrom,
					ValidTo: vTo,
					DuesAmt: 0,
					GenerateQR: $scope.newPrintIdCard.GenerateQR,
					For: $scope.newPrintIdCard.For,
					ForMonth: ($scope.newPrintIdCard.ForMonth > 0 ? $scope.newPrintIdCard.ForMonth : 0),
					SemesterId: 0,
					ClassYearId: 0,
					BatchId: 0,
					OnlyPhotoStudent: false,
					ClassIdColl: ''
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


	//Employee ID Card Starts by Suresh on Mangsir 11
	$scope.ClearIdentityCard = function () {
		$scope.newIdentityCard = {
			GenerateQR: false,
			DepartmentId: 0,
			IdentityCardId: null,

		};
	}

	$scope.TestClickEmp = function (v) {
		v.GenerateQR = !v.GenerateQR;
	}

	$scope.LoadReportTemplatesEmp = function () {

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.EIdCard + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newIdentityCard.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}



	$scope.PrintEmployeeIdCard = function () {
		if ($scope.newIdentityCard.RptTranId && $scope.newIdentityCard.EmployeeIdColl) {
			var EntityId = $scope.entity.EIdCard;

			var template = mx($scope.newIdentityCard.TemplatesColl).firstOrDefault(p1 => p1.RptTranId == $scope.newIdentityCard.RptTranId);

			if ($scope.newIdentityCard.RptTranId > 0) {

				var vFrom = null, vTo = null;
				if ($scope.newIdentityCard.IssueDateDet)
					vFrom = $filter('date')(new Date($scope.newIdentityCard.IssueDateDet.dateAD), 'yyyy-MM-dd');

				if ($scope.newIdentityCard.ValidUptoDet)
					vTo = $filter('date')(new Date($scope.newIdentityCard.ValidUptoDet.dateAD), 'yyyy-MM-dd');

				var rptPara = {
					rpttranid: $scope.newIdentityCard.RptTranId,
					istransaction: false,
					entityid: $scope.entity.EIdCard,
					EmpIdColl: ($scope.newIdentityCard.EmployeeIdColl ? $scope.newIdentityCard.EmployeeIdColl.toString() : ''),
					ValidFrom: vFrom,
					ValidTo: vTo,
					GenerateQR: $scope.newIdentityCard.GenerateQR,
					DepartmentId: 0
				};
				var paraQuery = param(rptPara);
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulationEmp").src = '';

				if (template.IsRDLC == true)
					document.getElementById("frmRptTabulationEmp").src = base_url + "Academic/Report/RdlEmployeeIdCard?" + paraQuery;
				else
					document.getElementById("frmRptTabulationEmp").src = base_url + "web/ReportViewer.aspx?" + paraQuery;

				document.body.style.cursor = 'default';
			} else {
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulationEmp").src = '';
				document.body.style.cursor = 'default';
			}
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptTabulationEmp").src = '';
			document.body.style.cursor = 'default';
		}

	};



	$scope.PrintNormalIdCardEmp = function () {
		if ($scope.newIdentityCard.RptTranId) {
			var EntityId = $scope.entity.EIdCard;

			var template = mx($scope.newIdentityCard.TemplatesColl).firstOrDefault(p1 => p1.RptTranId == $scope.newIdentityCard.RptTranId);

			if ($scope.newIdentityCard.RptTranId > 0) {

				var vFrom = null, vTo = null;
				if ($scope.newIdentityCard.IssueDateDet)
					vFrom = $filter('date')(new Date($scope.newIdentityCard.IssueDateDet.dateAD), 'yyyy-MM-dd');

				if ($scope.newIdentityCard.ValidUptoDet)
					vTo = $filter('date')(new Date($scope.newIdentityCard.ValidUptoDet.dateAD), 'yyyy-MM-dd');

				var rptPara = {
					rpttranid: $scope.newIdentityCard.RptTranId,
					istransaction: false,
					entityid: $scope.entity.EIdCard,
					EmpIdColl: '',
					ValidFrom: vFrom,
					ValidTo: vTo,
					GenerateQR: $scope.newIdentityCard.GenerateQR,
					DepartmentId: $scope.newIdentityCard.DepartmentId,
					DesignationId: $scope.newIdentityCard.DesignationId,
				};

				var paraQuery = param(rptPara);
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulationEmp").src = '';

				if (template.IsRDLC == true)
					document.getElementById("frmRptTabulationEmp").src = base_url + "Academic/Report/RdlEmployeeIdCard?" + paraQuery;
				else
					document.getElementById("frmRptTabulationEmp").src = base_url + "web/ReportViewer.aspx?" + paraQuery;

				document.body.style.cursor = 'default';
			} else {
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulationEmp").src = '';
				document.body.style.cursor = 'default';
			}
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptTabulationEmp").src = '';
			document.body.style.cursor = 'default';
		}

	};
	//Ends

	$scope.validateDate = function (obj, startField, endField, startLabel, endLabel) {
		var res = GlobalServices.validateDate(obj, startField, endField, startLabel, endLabel);
		if (res.IsSuccess == false) {
			Swal.fire({
				icon: 'warning',
				text: res.Message,
				confirmButtonText: 'OK'
			}).then(function () {
				obj.IssueDate_TMP = new Date();
				obj.ValidUpto_TMP = new Date();
				$scope.$applyAsync();
			});
		}
	};
	

});