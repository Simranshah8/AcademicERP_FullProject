
app.controller('PendingVouchersController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Pending Vouchers';

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
				{ name: "RefNo", displayName: "Ref. No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DebitAmt", displayName: "Debit Amt.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CreditAmt", displayName: "Credit Amt.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CostClass", displayName: "Cost Class", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "User", displayName: "User", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "VerifyMode", displayName: "Verify Mode", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "VerifyRemarks", displayName: "Verify Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RejectRemarks", displayName: "Reject Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "EntryDate", displayName: "Entry Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CreationAt", displayName: "Creation At", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PostAt", displayName: "Post At", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "Address", displayName: "Address", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ChequeDate", displayName: "Cheque Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ChequeNo", displayName: "Cheque No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ChequeRemarks", displayName: "Cheque Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClearanceDate", displayName: "Clearance Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClearanceRemarks", displayName: "Clearance Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LilitCrossAmt", displayName: "Lilit Cross Amt", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LimitCrossDays", displayName: "Limit Cross Days", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Narration", displayName: "Narration", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PanVatNo", displayName: "PanVat No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PartyName", displayName: "Party Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RefVoucherRefNo", displayName: "Post At", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RefVoucherNo", displayName: "RefVoucher RefNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TransactionAmt", displayName: "Transaction Amt", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ModifyBy", displayName: "Modify By", minWidth: 140, headerCellClass: 'headerAligment' },
				
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
		//	PendingVouchers: 1,

		//};

		//$scope.searchData = {
		//	PendingVouchers: '',

		//};

		//$scope.perPage = {
		//	PendingVouchers: GlobalServices.getPerPageRow(),

		//};

		$scope.newPendingVouchers = {
			PendingVouchersId: null,

			Mode: 'Save'
		};

		//$scope.GetAllPendingVouchersList();

	};



	$scope.ClearPendingVouchers = function () {
		$scope.newPendingVouchers = {
			PendingVouchersId: null,

			Mode: 'Save'
		};

	};

	$scope.SaveUpdatePendingVouchers = function () {
		if ($scope.IsValidPendingVouchers() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newPendingVouchers.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdatePendingVouchers();
					}
				});
			} else
				$scope.CallSaveUpdatePendingVouchers();

		}
	};



	$scope.GetAllPendingVouchersList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PendingVouchersList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Reporting/GetAllPendingVouchersList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PendingVouchersList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};



});