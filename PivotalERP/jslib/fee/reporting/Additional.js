"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller('AdditionalController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate, uiGridConstants) {
	$scope.Title = 'Additional';

	/*var gSrv = GlobalServices;*/
	getterAndSetter();

	$rootScope.ConfigFunction = function () {
		$scope.LoadData();

	};
	$rootScope.ChangeLanguage();

	$scope.getRptState = function () {
		$http({
			method: 'GET',
			url: base_url + "Global/GetListState?entityId=" + EntityId + "&isReport=true",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				if ($scope.gridApi) {
					if ($scope.gridApi.saveState) {
						var state = JSON.parse(res.data.Data);
						$scope.gridApi.saveState.restore($scope, state);
					}
				}
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	$scope.SaveRptState = function () {
		var state = $scope.gridApi.saveState.save();
		var entityId = EntityId;
		GlobalServices.saveListState(entityId, state);
	}


	function getterAndSetter() {

		$scope.gridOptions1 = [];

		$scope.gridOptions1 = {
			showGridFooter: true,
			showColumnFooter: true,
			enableFiltering: true,
			enableSorting: true,
			enableGridMenu: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableColumnResizing: true,
			enableHorizontalScrollbar: 1,
			enableVerticalScrollbar: 1,
			rowHeight: 32,
			headerRowHeight: 35,
			columnDefs: [

				{ name: "RegdNo", displayName: "Regd. No", width: 120, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No", width: 90, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", width: 180, headerCellClass: 'headerAligment' },
				{ name: "ClassSection", displayName: "Class", width: 120, headerCellClass: 'headerAligment' },
				{ name: "BillMiti", displayName: "Date", width: 100, headerCellClass: 'headerAligment' },
				{
					name: "AutoNumber", displayName: "Bill No.", width: 100, headerCellClass: 'headerAligment',
					aggregationType: uiGridConstants.aggregationTypes.count,
					footerCellTemplate: '<div class="ui-grid-cell-contents">{{col.getAggregationValue()}}</div>',
					footerCellClass: 'numericAlignment'
				},
				{ name: "MonthName", displayName: "Month", width: 120, headerCellClass: 'headerAligment' },
				{ name: "FeeItem", displayName: "Fee Item", width: 160, headerCellClass: 'headerAligment' },
				{
					name: "Qty", displayName: "Qty.", width: 80, headerCellClass: 'headerAligment', cellClass: 'numericAlignment', cellFilter: 'number',
					aggregationType: uiGridConstants.aggregationTypes.sum,
					footerCellTemplate: '<div class="ui-grid-cell-contents">{{col.getAggregationValue() | number:2}}</div>',
					footerCellClass: 'numericAlignment'
				},
				{
					name: "Rate", displayName: "Rate", width: 100, headerCellClass: 'headerAligment', cellClass: 'numericAlignment', cellFilter: 'number',
					aggregationType: uiGridConstants.aggregationTypes.avg,
					footerCellTemplate: '<div class="ui-grid-cell-contents">{{col.getAggregationValue() | number:2}}</div>',
					footerCellClass: 'numericAlignment'
				},
				{
					name: "DiscountAmt", displayName: "Discount Amt", width: 120, headerCellClass: 'headerAligment', cellClass: 'numericAlignment', cellFilter: 'number',
					aggregationType: uiGridConstants.aggregationTypes.sum,
					footerCellTemplate: '<div class="ui-grid-cell-contents">{{col.getAggregationValue() | number:2}}</div>',
					footerCellClass: 'numericAlignment'
				},
				{ name: "DiscountPer", displayName: "Discount %", width: 110, headerCellClass: 'headerAligment', cellClass: 'numericAlignment', cellFilter: 'number' },
				{
					name: "PayableAmt", displayName: "Payable Amt", width: 140, headerCellClass: 'headerAligment', cellClass: 'numericAlignment', cellFilter: 'number',
					aggregationType: uiGridConstants.aggregationTypes.sum,
					footerCellTemplate: '<div class="ui-grid-cell-contents">{{col.getAggregationValue() | number:2}}</div>',
					footerCellClass: 'numericAlignment'
				},
				{ name: "Remarks", displayName: "Remarks", width: 200, headerCellClass: 'headerAligment' },
				{ name: "BillingType", displayName: "Bill Type", width: 140, headerCellClass: 'headerAligment' }
			],

			exporterCsvFilename: 'manualbillSummary.csv',
			exporterExcelFilename: 'manualbillSummary.xlsx',
			exporterExcelSheetName: 'manualbillSummary',
			exporterPdfOrientation: 'landscape',
			exporterPdfPageSize: 'LETTER',
			exporterPdfMaxGridWidth: 800,
			exporterPdfDefaultStyle: { fontSize: 9 },
			exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
			exporterPdfTableHeaderStyle: {
				fontSize: 10,
				bold: true,
				italics: true,
				color: 'blue'
			},
			exporterPdfHeader: {
				text: "Dynamic Technosoft Pvt. Ltd.\nBirgunj Nepal",
				style: 'headerStyle'
			},
			exporterPdfFooter: function (currentPage, pageCount) {
				return {
					text: currentPage + ' of ' + pageCount,
					style: 'footerStyle'
				};
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
				docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
				return docDefinition;
			},
			exporterCsvLinkElement: angular.element(
				document.querySelectorAll(".custom-csv-link-location")
			),
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
			}
		};

		//Fee HEadingWiseStudent List Only
		//$scope.gridOptions2 = [];
		//$scope.gridOptions2 = {
		//	showGridFooter: true,
		//	showColumnFooter: false,
		//	useExternalPagination: false,
		//	useExternalSorting: false,
		//	enableFiltering: true,
		//	enableSorting: true,
		//	enableRowSelection: true,
		//	enableSelectAll: true,
		//	enableGridMenu: true,
		//	columnDefs: [
		//		{ name: "Id", displayName: "Id", minWidth: 100, headerCellClass: 'headerAligment' },
		//		{ name: "RegdNo", displayName: "Regd. No", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Class", displayName: "Class", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "FeeItem", displayName: "Fee Item", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "DiscountAmt", displayName: "Discount Amt.", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "DiscountPer", displayName: "Discount Per", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "FatherName", displayName: "Father Name", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "ContactNo", displayName: "Contact No.", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Address", displayName: "Address", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "TransportPoint", displayName: "Transport Point", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "BoarderName", displayName: "Boarders Name", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "HouseName", displayName: "House Name", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Remarks", displayName: "Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "IsLeft", displayName: "Is Left", minWidth: 140, headerCellClass: 'headerAligment' },


		//	],
		//	//   rowTemplate: rowTemplate(),
		//	exporterCsvFilename: 'enqSummary.csv',
		//	exporterPdfDefaultStyle: { fontSize: 9 },
		//	exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
		//	exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
		//	exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
		//	exporterPdfFooter: function (currentPage, pageCount) {
		//		return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
		//	},
		//	exporterPdfCustomFormatter: function (docDefinition) {
		//		docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
		//		docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
		//		return docDefinition;
		//	},
		//	exporterPdfOrientation: 'portrait',
		//	exporterPdfPageSize: 'LETTER',
		//	exporterPdfMaxGridWidth: 500,
		//	exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
		//	exporterExcelFilename: 'enqSummary.xlsx',
		//	exporterExcelSheetName: 'enqSummary',
		//	onRegisterApi: function (gridApi) {
		//		$scope.gridApi = gridApi;
		//	}
		//};



		////$scope.gridOptions3 = [];
		////$scope.gridOptions3 = {
		////	showGridFooter: true,
		////	showColumnFooter: false,
		////	useExternalPagination: false,
		////	useExternalSorting: false,
		////	enableFiltering: true,
		////	enableSorting: true,
		////	enableRowSelection: true,
		////	enableSelectAll: true,
		////	enableGridMenu: true,

		////	columnDefs: [
		////		{ name: "Id", displayName: "Id", minWidth: 100, headerCellClass: 'headerAligment' },
		////		{ name: "RegdNo", displayName: "Regd. No", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "Class", displayName: "Class", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "FeeItem", displayName: "Fee Item", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "DiscountAmt", displayName: "Discount Amt", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "DiscountPer", displayName: "Discount Per", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "FatherName", displayName: "Father Name", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "ContactNo", displayName: "Contact No", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "Address", displayName: "Address", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "TransportPoint", displayName: "Transport Point", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "BoarderName", displayName: "Boarder Name", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "HouseName", displayName: "House Name", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "Remarks", displayName: "Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "IsLeft", displayName: "Is Left", minWidth: 140, headerCellClass: 'headerAligment' },


		////	],
		////	//   rowTemplate: rowTemplate(),
		////	exporterCsvFilename: 'enqSummary.csv',
		////	exporterPdfDefaultStyle: { fontSize: 9 },
		////	exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
		////	exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
		////	exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
		////	exporterPdfFooter: function (currentPage, pageCount) {
		////		return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
		////	},
		////	exporterPdfCustomFormatter: function (docDefinition) {
		////		docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
		////		docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
		////		return docDefinition;
		////	},
		////	exporterPdfOrientation: 'portrait',
		////	exporterPdfPageSize: 'LETTER',
		////	exporterPdfMaxGridWidth: 500,
		////	exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
		////	exporterExcelFilename: 'enqSummary.xlsx',
		////	exporterExcelSheetName: 'enqSummary',
		////	onRegisterApi: function (gridApi) {
		////		$scope.gridApi = gridApi;
		////	}
		////};



		////$scope.gridOptions4 = [];
		////$scope.gridOptions4 = {
		////	showGridFooter: true,
		////	showColumnFooter: false,
		////	useExternalPagination: false,
		////	useExternalSorting: false,
		////	enableFiltering: true,
		////	enableSorting: true,
		////	enableRowSelection: true,
		////	enableSelectAll: true,
		////	enableGridMenu: true,

		////	columnDefs: [
		////		{ name: "Id", displayName: "Id", minWidth: 100, headerCellClass: 'headerAligment' },
		////		{ name: "RegdNo", displayName: "Regd. No", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "Class", displayName: "Class", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "PreviousDues", displayName: "Previous Dues", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "CurrentFees", displayName: "Current Fees", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "PaidAmount", displayName: "Paid Amount", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "DiscountAmount", displayName: "Discount Amount", minWidth: 140, headerCellClass: 'headerAligment' },
		////		{ name: "BalanceAmount", displayName: "Balance Amount", minWidth: 140, headerCellClass: 'headerAligment' },

		////	],
		////	//   rowTemplate: rowTemplate(),
		////	exporterCsvFilename: 'enqSummary.csv',
		////	exporterPdfDefaultStyle: { fontSize: 9 },
		////	exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
		////	exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
		////	exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
		////	exporterPdfFooter: function (currentPage, pageCount) {
		////		return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
		////	},
		////	exporterPdfCustomFormatter: function (docDefinition) {
		////		docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
		////		docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
		////		return docDefinition;
		////	},
		////	exporterPdfOrientation: 'portrait',
		////	exporterPdfPageSize: 'LETTER',
		////	exporterPdfMaxGridWidth: 500,
		////	exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
		////	exporterExcelFilename: 'enqSummary.xlsx',
		////	exporterExcelSheetName: 'enqSummary',
		////	onRegisterApi: function (gridApi) {
		////		$scope.gridApi = gridApi;
		////	}
		////};


		////$scope.gridOptions5 = [];
		////$scope.gridOptions5 = {
		//	showGridFooter: true,
		//	showColumnFooter: false,
		//	useExternalPagination: false,
		//	useExternalSorting: false,
		//	enableFiltering: true,
		//	enableSorting: true,
		//	enableRowSelection: true,
		//	enableSelectAll: true,
		//	enableGridMenu: true,

		//	columnDefs: [
		//		{ name: "Date", displayName: "Date(B.S.)", minWidth: 100, headerCellClass: 'headerAligment' },
		//		{ name: "Particulars", displayName: "Particulars", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "MoreAmount", displayName: "More Amount", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Amount", displayName: "Amount", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Discount", displayName: "Discount", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Tax", displayName: "Tax", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "VoucherType", displayName: "Voucher Type", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "VoucherNo", displayName: "Voucher No", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "RefNo", displayName: "Ref. No", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "DebitAmount", displayName: "Debit Amount", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "CreditAmount", displayName: "Credit Amount", minWidth: 140, headerCellClass: 'headerAligment' },

		//	],
		//	//   rowTemplate: rowTemplate(),
		//	exporterCsvFilename: 'enqSummary.csv',
		//	exporterPdfDefaultStyle: { fontSize: 9 },
		//	exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
		//	exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
		//	exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
		//	exporterPdfFooter: function (currentPage, pageCount) {
		//		return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
		//	},
		//	exporterPdfCustomFormatter: function (docDefinition) {
		//		docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
		//		docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
		//		return docDefinition;
		//	},
		//	exporterPdfOrientation: 'portrait',
		//	exporterPdfPageSize: 'LETTER',
		//	exporterPdfMaxGridWidth: 500,
		//	exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
		//	exporterExcelFilename: 'enqSummary.xlsx',
		//	exporterExcelSheetName: 'enqSummary',
		//	onRegisterApi: function (gridApi) {
		//		$scope.gridApi = gridApi;
		//	}
		//};



		//Added on MAgh 23 for Fee Mapping Student
		$scope.gridColumnDef44 = [
			{ headerName: "Id", width: 90, field: "AutoNumber", filter: 'agNumberColumnFilter', sortable: true },
			{ headerName: "Reg.No.", width: 120, field: "RegNo", filter: 'agTextColumnFilter', sortable: true },
			{ headerName: "Roll No.", width: 100, field: "RollNo", filter: 'agNumberColumnFilter', },
			/*	{ headerName: "Name", width: 180, field: "Name", filter: 'agTextColumnFilter', },*/

			{
				headerName: "Name", field: "Name", filter: 'agTextColumnFilter', width: 180,
			},

			{ headerName: "Class", width: 100, field: "ClassName", filter: 'agTextColumnFilter', },
			{ headerName: "Section", width: 100, field: "SectionName", filter: 'agTextColumnFilter', },
			{ headerName: "Fee Heading", width: 100, field: "FeeItem", filter: 'agTextColumnFilter', },
			{ headerName: "FatherName", width: 150, field: "FatherName", filter: 'agTextColumnFilter', },
			{ headerName: "F_ContactNo", width: 120, field: "ContactNo", filter: 'agTextColumnFilter', },
			{ headerName: "Address", width: 160, field: "Address", filter: 'agTextColumnFilter', },
			{ headerName: "Nature", width: 140, field: "Nature", filter: 'agTextColumnFilter', },

		];

		$scope.gridOptions44 = {

			// a default column definition with properties that get applied to every column
			defaultColDef: {
				filter: true,
				resizable: true,
				sortable: true,

				// set every column width
				width: 90
			},
			headerHeight: 31,
			rowHeight: 30,
			columnDefs: $scope.gridColumnDef44,
			enableColResize: true,
			rowData: null,
			filter: true,
			enableFilter: true,
			enableSorting: true,
			rowSelection: 'multiple',


		};


		$scope.filter = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			/*	BillingType: 0,*/
			BillingType: true,
			ReportType: 1,
			IsCancel: false
		};

		$scope.BillingTypeColl = [
			{ id: 1, text: 'SalesInvoice', IsSelected: true },
			{ id: 2, text: 'Debit Note', IsSelected: true },
			{ id: 3, text: 'Credit Note', IsSelected: true }
		];

		$scope.ReportTypeColl = [
			{ id: 1, text: 'Fee Heading Wise' },
			{ id: 2, text: 'Class Wise' }
		];
		// ==== Column Definitions ====
		$scope.summaryColumnDefs = [
			{ headerName: "S.No.", field: "SNo", width: 90, cellStyle: { textAlign: 'center' } },
			{ headerName: "Fee Order No", field: "OrderNo", width: 150 },
			{ headerName: "Fee Item Name", field: "FeeItemName", width: 226 },
			{ headerName: "Billing Amt.", field: "BillingAmt", width: 226, cellClass: 'ag-right-aligned-cell', valueFormatter: currencyFormatter },
			{ headerName: "Discount Amt.", field: "DisAmt", width: 226, cellClass: 'ag-right-aligned-cell', valueFormatter: currencyFormatter },
			{ headerName: "Payable Amt.", field: "PayableAmt", width: 226, cellClass: 'ag-right-aligned-cell', valueFormatter: currencyFormatter },
			{ headerName: "Ledger", field: "Ledger", width: 226 },
			{ headerName: "Ledger Group", field: "LedgerGroup", width: 226 },
			{ headerName: "Product", field: "Product", width: 226 },
			{ headerName: "Product Type", field: "ProductType", width: 226 },
		];

		$scope.detailsColumnDefs = [
			{ headerName: "S.No.", field: "SNo", width: 90, cellStyle: { textAlign: 'center' } },
			{ headerName: "Class", field: "ClassName", width: 100 },
			{ headerName: "Section", field: "SectionName", width: 100 },
			{ headerName: "Batch", field: "Batch", width: 100, filter: 'agTextColumnFilter' },
			{ headerName: "Semester", field: "Semester", width: 120, filter: 'agTextColumnFilter' },
			{ headerName: "Class Year", field: "ClassYear", width: 150, filter: 'agTextColumnFilter' },
			/*	{ headerName: "Fee Item Name", field: "FeeItemName",  width: 150 },*/
			{ headerName: "Billing Amt.", field: "BillingAmt", width: 130, cellClass: 'ag-right-aligned-cell', valueFormatter: currencyFormatter },
			{ headerName: "Discount Amt.", field: "DisAmt", width: 130, cellClass: 'ag-right-aligned-cell', valueFormatter: currencyFormatter },
			{ headerName: "Payable Amt.", field: "PayableAmt", width: 140, cellClass: 'ag-right-aligned-cell', valueFormatter: currencyFormatter },
			/*{ headerName: "Source", field: "Source", width: 100 },*/
		];

		// ==== Grid Options ====
		$scope.gridSummaryOptions = {
			defaultColDef: {
				filter: true,
				resizable: true,
				sortable: true,
				width: 100,
			},
			headerHeight: 35,
			rowHeight: 33,
			enableSorting: true,
			multiSortKey: 'ctrl',
			columnDefs: $scope.summaryColumnDefs,
			rowData: null,
			rowSelection: 'multiple',
			overlayLoadingTemplate: "Please Click the Load Button to display the data",
			onFilterChanged: function () {
				$scope.CalculateSummaryTotal();
			},
			getRowStyle: function (params) {
				if (params.node.rowPinned) {
					return { fontWeight: 'bold', backgroundColor: '#f9f9f9' };
				}
			}
		};

		$scope.gridDetailsOptions = {
			defaultColDef: {
				filter: true,
				resizable: true,
				sortable: true,
				width: 100,
			},
			headerHeight: 35,
			rowHeight: 33,
			enableSorting: true,
			multiSortKey: 'ctrl',
			columnDefs: $scope.detailsColumnDefs,
			rowData: null,
			rowSelection: 'multiple',
			overlayLoadingTemplate: "Please Click the Load Button to display the data",
			getRowStyle: function (params) {
				if (params.node.rowPinned) {
					return {
						fontWeight: 'bold',
					};
				}
			},
			onFilterChanged: function () {
				$scope.CalculateDetailsTotal();
			}

		};

		new agGrid.Grid(document.querySelector('#BillingSummaryData'), $scope.gridSummaryOptions);
		new agGrid.Grid(document.querySelector('#BillingDetailsData'), $scope.gridDetailsOptions);

	};

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();

		$scope.newManualBill = {
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date()
		}

		//Added By Suresh on 23 Magh
		$('.select2').select2();

		$scope.ClassList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.FeeItemList = [];
		GlobalServices.getFeeItemList().then(function (res1) {
			$scope.FeeItemList = res1.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.MonthList = [];
		$scope.MonthList_Display = [];
		GlobalServices.getAcademicMonthList(null, null).then(function (res1) {
			$scope.MonthList = [];
			$scope.MonthList_Display = [];
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
				$scope.MonthList_Display.push({ id: m.NM, text: m.MonthYear });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.MonthList = [];
		//GlobalServices.getMonthListFromDB().then(function (res1) {
		//	angular.forEach(res1.data.Data, function (m) {
		//		$scope.MonthList.push({ id: m.NM, text: m.MonthName });
		//	});

		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});


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

		$scope.entity = {
			IncomeStatement: 370,
		};

		$scope.LoadReportTemplates();



		//Ends


		$scope.getRptState();



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

			} else {
				$scope.gridDetailsOptions.columnApi.setColumnsVisible(["Faculty"], false);
			}
			if ($scope.AcademicConfig.ActiveLevel == true) {
				$scope.LevelList = [];
				GlobalServices.getClassLevelList().then(function (res) {
					$scope.LevelList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				$scope.gridDetailsOptions.columnApi.setColumnsVisible(["Level"], false);
			}
			if ($scope.AcademicConfig.ActiveSemester == true) {

				$scope.SelectedClassSemesterList = [];
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			} else {
				$scope.gridDetailsOptions.columnApi.setColumnsVisible(["Semester"], false);
			}
			if ($scope.AcademicConfig.ActiveBatch == true) {

				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			} else {
				$scope.gridDetailsOptions.columnApi.setColumnsVisible(["Batch"], false);
			}
			if ($scope.AcademicConfig.ActiveClassYear == true) {

				$scope.ClassYearList = [];
				$scope.SelectedClassClassYearList = [];
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			} else {
				$scope.gridDetailsOptions.columnApi.setColumnsVisible(["ClassYear"], false);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



		$scope.loadingstatus = "stop";

	};



	//Ends
	$scope.GetManualBillingDetails = function () {

		var para = {
			dateFrom: $filter('date')(new Date(), 'yyyy-MM-dd'),
			dateTo: $filter('date')(new Date(), 'yyyy-MM-dd'),
		};

		if ($scope.newManualBill.DateFromDet)
			para.dateFrom = $filter('date')(new Date($scope.newManualBill.DateFromDet.dateAD), 'yyyy-MM-dd');

		if ($scope.newManualBill.DateToDet)
			para.dateTo = $filter('date')(new Date($scope.newManualBill.DateToDet.dateAD), 'yyyy-MM-dd');

		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetManualBillingDetails",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.gridOptions1.data = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.PrintMBCollections = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityFeeReeceiptColl + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				var templatesColl = res.data.Data;
				if (templatesColl && templatesColl.length > 0) {
					var templatesName = [];
					var sno = 1;
					angular.forEach(templatesColl, function (tc) {
						templatesName.push(sno + '-' + tc.ReportName);
						sno++;
					});

					var print = false;
					var template = null;
					var rptTranId = 0;
					if (templatesColl.length == 1) {
						rptTranId = templatesColl[0].RptTranId;
						template = templatesColl[0];
					}
					else {
						Swal.fire({
							title: 'Report Templates For Print',
							input: 'select',
							inputOptions: templatesName,
							inputPlaceholder: 'Select a template',
							showCancelButton: true,
							inputValidator: (value) => {
								return new Promise((resolve) => {
									if (value >= 0) {
										resolve()
										rptTranId = templatesColl[value].RptTranId;

										if (rptTranId > 0) {

											template = mx(templatesColl).firstOrDefault(p1 => p1.RptTranId == rptTranId);

											var dataColl = [];
											if ($scope.newFeeReceiptCollection.PrintDetails == true) {
												//dataColl = $scope.gridFRCollection.data;												
												angular.forEach($scope.gridApi.grid.getVisibleRows(), function (ent) {
													var dt = ent.entity;
													dataColl.push(dt);
												});
											}

											else {
												//angular.forEach($scope.gridFRCollection.data, function (dt) {
												//	if (dt.IsParent == true)
												//		dataColl.push(dt);
												//});

												angular.forEach($scope.gridApi.grid.getVisibleRows(), function (ent) {
													var dt = ent.entity;
													if (dt.IsParent == true)
														dataColl.push(dt);
												});
											}


											print = true;
											$http({
												method: 'POST',
												url: base_url + "Global/PrintReportData",
												headers: { 'Content-Type': undefined },

												transformRequest: function (data) {

													var formData = new FormData();
													formData.append("jsonData", angular.toJson(data.jsonData));

													return formData;
												},
												data: { jsonData: dataColl }
											}).then(function (res) {

												$scope.loadingstatus = "stop";
												hidePleaseWait();
												if (res.data.IsSuccess && res.data.Data) {

													var rptPara = {
														Period: $scope.newManualBill.DateFromDet.dateBS + ' TO ' + $scope.newManualBill.DateToDet.dateBS,
													};
													var paraQuery = param(rptPara);


													if (template.IsRDLC == true)
														document.getElementById("frmRpt").src = base_url + "Fee/Report/RdlFeeDayBook?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityFeeReeceiptColl + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId + "&" + paraQuery;
													else
														document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityFeeReeceiptColl + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId + "&" + paraQuery;

													//document.body.style.cursor = 'wait';
													//document.getElementById("frmRpt").src = '';
													//document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityFeeReeceiptColl + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId + "&" + paraQuery;
													//document.body.style.cursor = 'default';
													$('#FrmPrintReport').modal('show');

												} else
													Swal.fire('No Templates found for print');

											}, function (errormessage) {
												hidePleaseWait();
												$scope.loadingstatus = "stop";
												Swal.fire(errormessage);
											});

										}

									} else {
										resolve('You need to select:)')
									}
								})
							}
						})
					}

					if (rptTranId > 0 && print == false) {
						var dataColl = [];
						if ($scope.newFeeReceiptCollection.PrintDetails == true) {
							//dataColl = $scope.gridFRCollection.data;		 
							angular.forEach($scope.gridApi.grid.getVisibleRows(), function (ent) {
								var dt = ent.entity;
								dataColl.push(dt);
							});
						}

						else {
							//angular.forEach($scope.gridFRCollection.data, function (dt) {
							//	if (dt.IsParent == true)
							//		dataColl.push(dt);
							//});

							angular.forEach($scope.gridApi.grid.getVisibleRows(), function (ent) {
								var dt = ent.entity;
								if (dt.IsParent == true)
									dataColl.push(dt);
							});
						}
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Global/PrintReportData",
							headers: { 'Content-Type': undefined },

							transformRequest: function (data) {

								var formData = new FormData();
								formData.append("jsonData", angular.toJson(data.jsonData));

								return formData;
							},
							data: { jsonData: dataColl }
						}).then(function (res) {

							$scope.loadingstatus = "stop";
							hidePleaseWait();
							if (res.data.IsSuccess && res.data.Data) {

								var rptPara = {
									Period: $scope.newManualBill.DateFromDet.dateBS + ' TO ' + $scope.newManualBill.DateToDet.dateBS,
								};
								var paraQuery = param(rptPara);

								document.body.style.cursor = 'wait';

								if (template.IsRDLC == true)
									document.getElementById("frmRpt").src = base_url + "Fee/Report/RdlFeeDayBook?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityFeeReeceiptColl + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId + "&" + paraQuery;
								else
									document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityFeeReeceiptColl + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId + "&" + paraQuery;


								//document.getElementById("frmRpt").src = '';
								//document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityFeeReeceiptColl + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId+"&"+paraQuery;
								//document.body.style.cursor = 'default';
								$('#FrmPrintReport').modal('show');

							} else
								Swal.fire('No Templates found for print');

						}, function (errormessage) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							Swal.fire(errormessage);
						});

					}

				} else
					Swal.fire('No Templates found for print');
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}


	//New Code added by Suresh in 23 Magh for arranging the tabs
	$scope.LoadReportTemplates = function () {
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



	$scope.getFeeMappingStudent = function () {

		$scope.loadingstatus = "running";
		var tmpColl = [];
		$scope.gridOptions44.api.setRowData(tmpColl);

		showPleaseWait();
		var para = {
			ClassIdColl: ($scope.feeMappingStudent.ClassIdColl ? $scope.feeMappingStudent.ClassIdColl.toString() : ''),
			FeeItemIdColl: ($scope.feeMappingStudent.FeeItemIdColl ? $scope.feeMappingStudent.FeeItemIdColl.toString() : ''),
			For: ($scope.feeMappingStudent.For ? $scope.feeMappingStudent.For : 0)
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Report/GetFeeMappingStudent",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$scope.gridOptions44.api.setRowData(res.data.Data);
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.PrintFeeMappingStudent = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityFeeMappingStudent + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				var templatesColl = res.data.Data;
				if (templatesColl && templatesColl.length > 0) {
					var templatesName = [];
					var sno = 1;
					angular.forEach(templatesColl, function (tc) {
						templatesName.push(sno + '-' + tc.ReportName);
						sno++;
					});

					var print = false;

					var rptTranId = 0;
					if (templatesColl.length == 1)
						rptTranId = templatesColl[0].RptTranId;
					else {
						Swal.fire({
							title: 'Report Templates For Print',
							input: 'select',
							inputOptions: templatesName,
							inputPlaceholder: 'Select a template',
							showCancelButton: true,
							inputValidator: (value) => {
								return new Promise((resolve) => {
									if (value >= 0) {
										resolve()
										rptTranId = templatesColl[value].RptTranId;

										if (rptTranId > 0) {
											var dataColl = [];
											$scope.gridOptions44.api.forEachNodeAfterFilterAndSort(function (node) {
												dataColl.push(node.data);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Fee/Report/PrintFeeMappingStudent",
												headers: { 'Content-Type': undefined },

												transformRequest: function (data) {

													var formData = new FormData();
													formData.append("jsonData", angular.toJson(data.jsonData));

													return formData;
												},
												data: { jsonData: dataColl }
											}).then(function (res) {

												$scope.loadingstatus = "stop";
												hidePleaseWait();
												if (res.data.IsSuccess && res.data.Data) {


													var rptPara = {
														rpttranid: rptTranId,
														istransaction: false,
														entityid: entityFeeMappingStudent,
														voucherid: 0,
														tranid: 0,
														vouchertype: 0,
														sessionid: res.data.Data.ResponseId,
													};
													var paraQuery = param(rptPara);

													document.body.style.cursor = 'wait';
													document.getElementById("frmRpt").src = '';
													document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
													document.body.style.cursor = 'default';
													$('#FrmPrintReport').modal('show');

												} else
													Swal.fire('No Templates found for print');

											}, function (errormessage) {
												hidePleaseWait();
												$scope.loadingstatus = "stop";
												Swal.fire(errormessage);
											});

										}

									} else {
										resolve('You need to select:)')
									}
								})
							}
						})
					}

					if (rptTranId > 0 && print == false) {
						var dataColl = [];
						$scope.gridOptions44.api.forEachNodeAfterFilterAndSort(function (node) {
							dataColl.push(node.data);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Fee/Report/PrintFeeMappingStudent",
							headers: { 'Content-Type': undefined },

							transformRequest: function (data) {

								var formData = new FormData();
								formData.append("jsonData", angular.toJson(data.jsonData));

								return formData;
							},
							data: { jsonData: dataColl }
						}).then(function (res) {

							$scope.loadingstatus = "stop";
							hidePleaseWait();
							if (res.data.IsSuccess && res.data.Data) {

								var rptPara = {
									rpttranid: rptTranId,
									istransaction: false,
									entityid: entityFeeMappingStudent,
									voucherid: 0,
									tranid: 0,
									vouchertype: 0,
									sessionid: res.data.Data.ResponseId
								};
								var paraQuery = param(rptPara);

								document.body.style.cursor = 'wait';
								document.getElementById("frmRpt").src = '';
								document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
								document.body.style.cursor = 'default';
								$('#FrmPrintReport').modal('show');

							} else
								Swal.fire('No Templates found for print');

						}, function (errormessage) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							Swal.fire(errormessage);
						});

					}

				} else
					Swal.fire('No Templates found for print');
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.onFilterTextBoxChanged44 = function () {
		$scope.gridOptions44.api.setQuickFilter($scope.search44);
		$scope.gridSummaryOptions.api.setQuickFilter($scope.searchSummary);
		$scope.gridDetailsOptions.api.setQuickFilter($scope.searchDetails);
	}

	$scope.ExportFeeMappingAsCSV = function () {
		var params = {
			fileName: 'feeSummary.csv',
			sheetName: 'feeSummary'
		};
		$scope.gridOptions44.api.exportDataAsCsv(params);
	}

	$scope.ExportBillSummaryAsCSV = function () {
		var params = {
			fileName: 'BillingSummary.csv',
			sheetName: 'BillingSummary'
		};
		$scope.gridSummaryOptions.api.exportDataAsCsv(params);
	}
	$scope.ExportBillDetailsAsCSV = function () {
		var params = {
			fileName: 'BillingSummaryDetails.csv',
			sheetName: 'BillingDetails'
		};
		$scope.gridDetailsOptions.api.exportDataAsCsv(params);
	}

	$scope.getBillingTypeLabel = function () {
		var selectedItems = $scope.BillingTypeColl
			.filter(function (item) { return item.IsSelected; })
			.map(function (item) { return item.text; });
		return selectedItems.length > 0 ? selectedItems.join(', ') : 'Select Billing Type';
	};

	$scope.GetBillingSummary = function () {
		$scope.loadingstatus = 'running';
		showPleaseWait();

		// Collect IDs of selected billing types
		var selectedBillingTypes = $scope.BillingTypeColl
			.filter(function (item) { return item.IsSelected; })
			.map(function (item) { return item.id; });

		// Convert to comma-separated string or '0' if none selected
		var billingTypeParam = selectedBillingTypes.length > 0 ? selectedBillingTypes.join(',') : '0';

		const para = {
			FromDate: $filter('date')(new Date($scope.filter.FromDateDet.dateAD), 'yyyy-MM-dd'),
			ToDate: $filter('date')(new Date($scope.filter.ToDateDet.dateAD), 'yyyy-MM-dd'),
			/*	BillingType: $scope.filter.BillingType,*/
			BillingType: billingTypeParam,
			ReportType: $scope.filter.ReportType,
			IsCancel: $scope.filter.IsCancel
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Report/GetBillingSummaryList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			$scope.loadingstatus = 'stop';
			hidePleaseWait();

			if (res.data.IsSuccess && res.data.Data) {
				$scope.gridSummaryOptions.api.setRowData(res.data.Data);

				$timeout(function () {
					$scope.CalculateSummaryTotal();
				}, 0);
			} else {
				alert(res.data.ResponseMSG);
			}
		}, function (reason) {
			$scope.loadingstatus = "stop";
			alert('Failed: ' + reason);
		});
	};

	$scope.GetBillingSummaryDetails = function () {
		$scope.loadingstatus = 'running';
		showPleaseWait();
		// Collect IDs of selected billing types
		var selectedBillingTypes = $scope.BillingTypeColl
			.filter(function (item) { return item.IsSelected; })
			.map(function (item) { return item.id; });

		// Convert to comma-separated string or '0' if none selected
		var billingTypeParam = selectedBillingTypes.length > 0 ? selectedBillingTypes.join(',') : '0';
		const para = {
			FromDate: $filter('date')(new Date($scope.filter.FromDateDet.dateAD), 'yyyy-MM-dd'),
			ToDate: $filter('date')(new Date($scope.filter.ToDateDet.dateAD), 'yyyy-MM-dd'),
			/*BillingType: $scope.filter.BillingType,*/
			BillingType: billingTypeParam,
			ReportType: 2,
			IsCancel: $scope.filter.IsCancel
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Report/GetBillingSummaryList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			$scope.loadingstatus = 'stop';
			hidePleaseWait();

			if (res.data.IsSuccess && res.data.Data) {
				$scope.gridDetailsOptions.api.setRowData(res.data.Data);

				$timeout(function () {
					$scope.CalculateDetailsTotal();
				}, 0);
			} else {
				alert(res.data.ResponseMSG);
			}
		}, function (reason) {
			$scope.loadingstatus = "stop";
			alert('Failed: ' + reason);
		});
	};

	$scope.CalculateSummaryTotal = function () {
		if (!$scope.gridSummaryOptions.api) return;

		let totalRow = {
			FeeItemName: 'TOTAL =>',
			BillingAmt: 0,
			DisAmt: 0,
			PayableAmt: 0
		};

		$scope.gridSummaryOptions.api.forEachNodeAfterFilterAndSort(function (node) {
			const data = node.data;
			if (data) {
				totalRow.BillingAmt += +data.BillingAmt || 0;
				totalRow.DisAmt += +data.DisAmt || 0;
				totalRow.PayableAmt += +data.PayableAmt || 0;
			}
		});

		$scope.gridSummaryOptions.api.setPinnedBottomRowData([totalRow]);
	};

	$scope.CalculateDetailsTotal = function () {
		if (!$scope.gridDetailsOptions.api) return;

		let totalRow = {
			FeeItemName: 'TOTAL =>',
			BillingAmt: 0,
			DisAmt: 0,
			PayableAmt: 0
		};

		$scope.gridDetailsOptions.api.forEachNodeAfterFilterAndSort(function (node) {
			let data = node.data;
			if (data) {
				totalRow.BillingAmt += +data.BillingAmt || 0;
				totalRow.DisAmt += +data.DisAmt || 0;
				totalRow.PayableAmt += +data.PayableAmt || 0;
			}
		});

		$scope.gridDetailsOptions.api.setPinnedBottomRowData([totalRow]);
	};

	function currencyFormatter(params) {
		if (params.value == null) return '';
		return (+params.value).toLocaleString('en-US', { minimumFractionDigits: 2 });
	}

	//Added by suresh for dropdown change in checkbox
	$scope.SelectedInclude = function () {
		var val = $scope.filter.BillingType;
		$scope.BillingTypeColl.forEach(function (v) {
			v.IsSelected = val;
		});
	}

	$scope.validateDate = function (obj, startField, endField, startLabel, endLabel) {
		var res = GlobalServices.validateDate(obj, startField, endField, startLabel, endLabel);
		if (res.IsSuccess == false) {
			Swal.fire({
				icon: 'warning',
				text: res.Message,
				confirmButtonText: 'OK'
			}).then(function () {
				obj.FromDate_TMP = new Date();
				obj.ToDate_TMP = new Date();
				obj.DateFrom_TMP = new Date();
				obj.DateTo_TMP = new Date();
				$scope.$applyAsync();
			});
		}
	};

});