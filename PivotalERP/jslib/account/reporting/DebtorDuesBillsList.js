
app.controller('DebtorDuesBillsListController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Bills Receivable';


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

				{ name: "Party", displayName: "Party", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PAN", displayName: "PAN", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Post", displayName: "Post", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "MobileNo", displayName: "Mobile No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "EmailId", displayName: "EmailId", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Voucher", displayName: "Voucher", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "GroupName", displayName: "Group Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ParentGroup", displayName: "ParentGroup", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Salesman", displayName: "Sales Man", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Area", displayName: "Area", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Date(A.D)", displayName: "Date(A.D)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Date(B.S.)", displayName: "Date(B.S.)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "InvoiceNo", displayName: "Invoice No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Amount", displayName: "Amount", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TotalDays", displayName: "Total Days", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Opening", displayName: "Opening", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DrAmount", displayName: "Dr. Amount", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CrAmount", displayName: "Cr.Amount", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Closing", displayName: "Closing", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Branch", displayName: "Branch", minWidth: 140, headerCellClass: 'headerAligment' },
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
		//	DebtorDuesBillsList: 1,

		//};

		//$scope.searchData = {
		//	DebtorDuesBillsList: '',

		//};

		//$scope.perPage = {
		//	DebtorDuesBillsList: GlobalServices.getPerPageRow(),

		//};

		$scope.newDebtorDuesBillsList = {
			DebtorDuesBillsListId: null,

			Mode: 'Save'
		};

		//$scope.GetAllDebtorDuesBillsListList();

	};



	$scope.ClearDebtorDuesBillsList = function () {
		$scope.newDebtorDuesBillsList = {
			DebtorDuesBillsListId: null,

			Mode: 'Save'
		};

	};

	$scope.SaveUpdateDebtorDuesBillsList = function () {
		if ($scope.IsValidDebtorDuesBillsList() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDebtorDuesBillsList.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDebtorDuesBillsList();
					}
				});
			} else
				$scope.CallSaveUpdateDebtorDuesBillsList();

		}
	};



	$scope.GetAllDebtorDuesBillsListList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.DebtorDuesBillsListList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Reporting/GetAllDebtorDuesBillsListList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DebtorDuesBillsListList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};



});