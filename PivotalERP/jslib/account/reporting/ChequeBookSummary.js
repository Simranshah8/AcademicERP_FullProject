
app.controller('ChequeBookSummaryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Cheque Book Summary';

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

				{ name: "Date(A.D)", displayName: "Date(A.D)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Date(B.S)", displayName: "Date(B.S)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Particulars", displayName: "Particulars", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "VoucherType", displayName: "Voucher Type", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "VoucherNo", displayName: "Voucher No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ChequeNo", displayName: "Cheque No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ChequeDate", displayName: "Cheque Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ChequeType", displayName: "Cheque Type", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "AccountNo", displayName: "Account No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Remarks", displayName: "Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DebitAmt", displayName: "Debit Amt.", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "CreditAmt", displayName: "Credit Amt.", minWidth: 140, headerCellClass: 'headerAligment' },



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
		//	ChequeBookSummary: 1,

		//};

		//$scope.searchData = {
		//	ChequeBookSummary: '',

		//};

		//$scope.perPage = {
		//	ChequeBookSummary: GlobalServices.getPerPageRow(),

		//};

		$scope.newChequeBookSummary = {
			ChequeBookSummaryId: null,

			Mode: 'Save'
		};

		//$scope.GetAllChequeBookSummaryList();

	};



	$scope.ClearChequeBookSummary = function () {
		$scope.newChequeBookSummary = {
			ChequeBookSummaryId: null,

			Mode: 'Save'
		};

	};

	$scope.SaveUpdateChequeBookSummary = function () {
		if ($scope.IsValidChequeBookSummary() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newChequeBookSummary.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateChequeBookSummary();
					}
				});
			} else
				$scope.CallSaveUpdateChequeBookSummary();

		}
	};



	$scope.GetAllChequeBookSummaryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ChequeBookSummaryList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Reporting/GetAllChequeBookSummaryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ChequeBookSummaryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};



});