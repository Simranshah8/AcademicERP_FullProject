

app.controller('TrialBalanceCostCenterController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'TrailBalance CostCenter';


	getterAndSetter();

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

				{ name: "Ledger", displayName: "Ledger", minWidth: 140, headerCellClass: 'headerAligment' },
				
				{ name: "LedgerGroup", displayName: "Ledger Group", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Branch", displayName: "Branch", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CostCenter", displayName: "Cost Center", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Brand", displayName: "Brand", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Opening", displayName: "Opening", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "OpeningDr", displayName: "Opening Dr", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "OpeningCr", displayName: "Opening Cr", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TransactionDr", displayName: "Transaction Dr.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TransactionCr", displayName: "Transaction Cr.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClosingDr", displayName: "Closing Dr.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClosingCr", displayName: "Closing Cr.", minWidth: 140, headerCellClass: 'headerAligment' },
				

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
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();


		//$scope.currentPages = {
		//	TrialBalanceCostCenter: 1,

		//};

		//$scope.searchData = {
		//	TrialBalanceCostCenter: '',

		//};

		//$scope.perPage = {
		//	TrialBalanceCostCenter: GlobalServices.getPerPageRow(),

		//};

		$scope.newTrialBalanceCostCenter = {
			TrialBalanceCostCenterId: null,

			Mode: 'Save'
		};

		//$scope.GetAllTrialBalanceCostCenterList();

	};



	$scope.ClearTrialBalanceCostCenter = function () {
		$scope.newTrialBalanceCostCenter = {
			TrialBalanceCostCenterId: null,

			Mode: 'Save'
		};

	};

	$scope.SaveUpdateTrialBalanceCostCenter = function () {
		if ($scope.IsValidTrialBalanceCostCenter() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newTrialBalanceCostCenter.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateTrialBalanceCostCenter();
					}
				});
			} else
				$scope.CallSaveUpdateTrialBalanceCostCenter();

		}
	};



	$scope.GetAllTrialBalanceCostCenterList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TrialBalanceCostCenterList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Reporting/GetAllTrialBalanceCostCenterList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TrialBalanceCostCenterList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};



});