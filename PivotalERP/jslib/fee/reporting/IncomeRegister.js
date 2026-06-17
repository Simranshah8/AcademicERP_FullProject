

app.controller('IncomeRegisterController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {
	$scope.Title = 'Income Register';

	var gSrv = GlobalServices;
	getterAndSetter();

	$rootScope.ConfigFunction = function () {
		$scope.LoadData();

	};
	$rootScope.ChangeLanguage();

	function getterAndSetter() {


		$scope.gridOptions = [];

		$scope.gridOptions = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,

			columnDefs: [
				{ name: "Id", displayName: "Id", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "RegdNo", displayName: "Regd. No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Class", displayName: "Class", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FeeAmount", displayName: "Fee Amount", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PaidAmount", displayName: "Paid Amount", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DuesAmount", displayName: "Dues Amount", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Rate", displayName: "Rate", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "Contact No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Is left", displayName: "Is left", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Left Date", displayName: "Left Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TransportPoint", displayName: "Transport Point", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BoarderName", displayName: "Boarder Name", minWidth: 140, headerCellClass: 'headerAligment' },

			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'enqSummary.csv',
			exporterPdfDefaultStyle: { fontSize: 9 },
			exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
			exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
				docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
				return docDefinition;
			},
			exporterPdfOrientation: 'portrait',
			exporterPdfPageSize: 'LETTER',
			exporterPdfMaxGridWidth: 500,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'enqSummary.xlsx',
			exporterExcelSheetName: 'enqSummary',
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
			}
		};


		$scope.gridOptions1 = [];

		$scope.gridOptions1 = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,

			columnDefs: [
				{ name: "Id", displayName: "Id", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "RegdNo", displayName: "Regd. No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassSec", displayName: "Class/Sec", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PreviousDues", displayName: "Previous Dues", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CurrentFee", displayName: "Current Fee", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PaidAmount", displayName: "Paid Amount", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DiscountAmount", displayName: "Discount Amount", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BalanceAmount", displayName: "Balance Amount", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "FatherName", displayName: "Father Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "Contact No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Is left", displayName: "Is left", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Left Date", displayName: "Left Date", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "TransportPoint", displayName: "Transport Point", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TransportRoute", displayName: "Transport Route", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BoarderName", displayName: "Boarder Name", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "DOB_AD", displayName: "D.O.B.(A.D.)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DOB_BS", displayName: "D.O.B.(B.S.)", minWidth: 140, headerCellClass: 'headerAligment' },


			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'enqSummary.csv',
			exporterPdfDefaultStyle: { fontSize: 9 },
			exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
			exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
				docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
				return docDefinition;
			},
			exporterPdfOrientation: 'portrait',
			exporterPdfPageSize: 'LETTER',
			exporterPdfMaxGridWidth: 500,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'enqSummary.xlsx',
			exporterExcelSheetName: 'enqSummary',
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
			}
		};


	};

	$scope.LoadData = function () {

		$scope.confirmMSG = gSrv.getConfirmMSG();

		$scope.MonthList = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.FeeItemList = [];
		gSrv.getFeeItemList().then(function (res1) {
			$scope.FeeItemList = res1.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.entity = {
			IncomeStatement: 370,
		};
		$scope.LoadReportTemplates();

		$scope.newIncomeStatement = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			TemplatesColl: [],
			FeeItemIdColl: '',
			fromReceiptNo: 0,
			toReceiptNo: 0,
			RptTranId: null
		};

		$scope.newClassWise = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			TemplatesColl: [],
			FeeItemIdColl: '',
			RptTranId: null
		};

		$scope.newStudentWise = {
			ClassId: null,
			SectionId: null,
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			TemplatesColl: [],
			FeeItemIdColl: '',
			RptTranId: null
		};

		$scope.classFee = {
			FromMonthId: 1,
			ToMonthId: 12,
			ForStudent: 1
		};

		$scope.feepc = {
			FromMonthId: 1,
			ToMonthId: 12,
			ForStudent: 0,
			TemplatesColl: [],
			FeeItemIdColl: '',
			RptTranId: null
		};

		$scope.ClassList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.LoadReportTemplates = function () {

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.IncomeStatement + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newIncomeStatement.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityClassWiseIncome + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newClassWise.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityStudentWiseIncome + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newStudentWise.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityClassFee + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.classFee.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityFeePC + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.feepc.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	$scope.PrintDateWiseIncomeStatement = function () {
		if ($scope.newIncomeStatement.RptTranId) {

			var EntityId = $scope.entity.IncomeStatement;

			var dateF = new Date();
			var dateT = new Date();

			var dateF_BS = '', dateT_BS = '';

			if ($scope.newIncomeStatement.FromDateDet) {
				dateF = $filter('date')(new Date($scope.newIncomeStatement.FromDateDet.dateAD), 'yyyy-MM-dd');
				dateF_BS = $scope.newIncomeStatement.FromDateDet.dateBS;
			}
			if ($scope.newIncomeStatement.ToDateDet) {
				dateT = $filter('date')(new Date($scope.newIncomeStatement.ToDateDet.dateAD), 'yyyy-MM-dd');
				dateT_BS = $scope.newIncomeStatement.ToDateDet.dateBS;
			}

			if ($rootScope.LANG == 'in') {
				dateF_BS = dateF;
				dateT_BS = dateT;
			}
			var rptPara = {
				dateFrom: dateF,
				dateTo: dateT,
				monthId: 0,
				period: 'Period From ' + dateF_BS + ' TO ' + dateT_BS,
				fromReceiptNo: ($scope.newIncomeStatement.fromReceiptNo ? $scope.newIncomeStatement.fromReceiptNo : 0),
				toReceiptNo: ($scope.newIncomeStatement.toReceiptNo ? $scope.newIncomeStatement.toReceiptNo : 0),
				rptTranId: $scope.newIncomeStatement.RptTranId
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptIncomeStatement").src = '';
			document.getElementById("frmRptIncomeStatement").style.width = '100%';
			document.getElementById("frmRptIncomeStatement").style.height = '1300px';
			document.getElementById("frmRptIncomeStatement").style.visibility = 'visible';
			document.getElementById("frmRptIncomeStatement").src = base_url + "Fee/Report/RdlDateWiseIncomeRegister?" + paraQuery;

		}

	};
	$scope.PrintClassWiseIncome = function () {
		if ($scope.newClassWise.RptTranId) {

			var EntityId = entityClassWiseIncome;

			var dateF = new Date();
			var dateT = new Date();

			var fromStr = '', toStr = '';
			if ($scope.newClassWise.FromDateDet) {
				dateF = $filter('date')(new Date($scope.newClassWise.FromDateDet.dateAD), 'yyyy-MM-dd');
				fromStr = $scope.newClassWise.FromDateDet.dateBS;
			}

			if ($scope.newClassWise.ToDateDet) {
				dateT = $filter('date')(new Date($scope.newClassWise.ToDateDet.dateAD), 'yyyy-MM-dd');
				toStr = $scope.newClassWise.ToDateDet.dateBS;
			}

			if ($rootScope.LANG == 'in') {
				fromStr = dateF;
				toStr = dateT;
			}

			var rptPara = {
				dateFrom: dateF,
				dateTo: dateT,
				dateFromStr: fromStr,
				dateToStr: toStr,
				rptTranId: $scope.newClassWise.RptTranId,
				feeItemIdColl: ($scope.newClassWise.FeeItemIdColl ? $scope.newClassWise.FeeItemIdColl : '')
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptClassWiseIncome").src = '';
			document.getElementById("frmRptClassWiseIncome").style.width = '100%';
			document.getElementById("frmRptClassWiseIncome").style.height = '1300px';
			document.getElementById("frmRptClassWiseIncome").style.visibility = 'visible';
			document.getElementById("frmRptClassWiseIncome").src = base_url + "Fee/Report/RptFeeIncomeClassWise?" + paraQuery;

		}

	};

	$scope.PrintStudentWiseIncome = function () {
		if ($scope.newStudentWise.RptTranId) {

			var EntityId = entityClassWiseIncome;

			var dateF = new Date();
			var dateT = new Date();

			var fromStr = '', toStr = '';
			if ($scope.newStudentWise.FromDateDet) {
				dateF = $filter('date')(new Date($scope.newStudentWise.FromDateDet.dateAD), 'yyyy-MM-dd');
				fromStr = $scope.newStudentWise.FromDateDet.dateBS;
			}

			if ($scope.newStudentWise.ToDateDet) {
				dateT = $filter('date')(new Date($scope.newStudentWise.ToDateDet.dateAD), 'yyyy-MM-dd');
				toStr = $scope.newStudentWise.ToDateDet.dateBS;
			}

			if ($rootScope.LANG == 'in') {
				dateFromStr = dateF;
				dateToStr = dateT;
			}

			var rptPara = {
				classId: $scope.newStudentWise.SelectedClass.ClassId,
				sectionId: $scope.newStudentWise.SelectedClass.SectionId ? $scope.newStudentWise.SelectedClass.SectionId : 0,
				classSec: $scope.newStudentWise.SelectedClass.text,
				dateFrom: dateF,
				dateTo: dateT,
				dateFromStr: fromStr,
				dateToStr: toStr,
				rptTranId: $scope.newStudentWise.RptTranId,
				feeItemIdColl: ($scope.newStudentWise.FeeItemIdColl ? $scope.newStudentWise.FeeItemIdColl : '')
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptStudentWiseIncome").src = '';
			document.getElementById("frmRptStudentWiseIncome").style.width = '100%';
			document.getElementById("frmRptStudentWiseIncome").style.height = '1300px';
			document.getElementById("frmRptStudentWiseIncome").style.visibility = 'visible';
			document.getElementById("frmRptStudentWiseIncome").src = base_url + "Fee/Report/RptFeeIncomeStudentWise?" + paraQuery;

		}

	};

	$scope.PrintClassFeeStatement = function () {
		if ($scope.classFee.RptTranId) {

			var EntityId = entityClassFee;

			var rptPara = {
				FromMonthId: $scope.classFee.FromMonthId,
				ToMonthId: $scope.classFee.ToMonthId,
				ForStudent: $scope.classFee.ForStudent,
				rptTranId: $scope.classFee.RptTranId
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptClassFee").src = '';
			document.getElementById("frmRptClassFee").style.width = '100%';
			document.getElementById("frmRptClassFee").style.height = '1300px';
			document.getElementById("frmRptClassFee").style.visibility = 'visible';
			document.getElementById("frmRptClassFee").src = base_url + "Fee/Report/RdlClassAndFeeWiseSummary?" + paraQuery;

		}

	};

	$scope.PrintFeePC = function () {
		if ($scope.feepc.RptTranId) {

			var EntityId = entityFeePC;

			var rptPara = {
				FromMonthId: $scope.feepc.FromMonthId,
				ToMonthId: $scope.feepc.ToMonthId,
				ForStudent: $scope.feepc.ForStudent,
				rptTranId: $scope.feepc.RptTranId
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptfeepc").src = '';
			document.getElementById("frmRptfeepc").style.width = '100%';
			document.getElementById("frmRptfeepc").style.height = '1300px';
			document.getElementById("frmRptfeepc").style.visibility = 'visible';
			document.getElementById("frmRptfeepc").src = base_url + "Fee/Report/RdlFeeSummaryPC?" + paraQuery;

		}

	};

	$scope.validateDates = function (changedField, obj) {
		if (!obj.FromDateDet || !obj.ToDateDet ||
			!obj.FromDateDet.dateAD || !obj.ToDateDet.dateAD) {
			return true;
		}

		var fromDate = $filter('date')(new Date(obj.FromDateDet.dateAD), 'yyyy-MM-dd');
		var toDate = $filter('date')(new Date(obj.ToDateDet.dateAD), 'yyyy-MM-dd');

		if (!fromDate || !toDate) return true;

		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.FromDate_TMP = new Date();
						obj.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.ToDate_TMP = new Date();
						obj.ToDateDet = new Date();
					});
				});
			}
			return false;
		}
		return true;
	};
});