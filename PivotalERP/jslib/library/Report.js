app.controller('ReportController', function ($scope, $http, $timeout, $filter, GlobalServices, $rootScope, $translate) {
	$scope.Title = 'Report';

	$rootScope.ConfigFunction = function () {
		var keyColl = $translate.getTranslationTable();

		var Labels = {
			RegdNo: keyColl['REGDNO_LNG']
		};
		if ($rootScope.LANG == 'in') {

			$scope.gridApi2.grid.getColumn('RegdNo').colDef.displayName = Labels.RegdNo;
			$scope.gridApi2.grid.getColumn('RegdNo').displayName = Labels.RegdNo;

			$scope.gridApi3.grid.getColumn('RegdNo').colDef.displayName = Labels.RegdNo;
			$scope.gridApi3.grid.getColumn('RegdNo').displayName = Labels.RegdNo;

		}


		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;

			if ($scope.AcademicConfig.ActiveFaculty == false) {

				findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' });
				if (findInd != -1)
					$scope.gridOptions2.columnDefs.splice(findInd, 1);

				findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' });
				if (findInd != -1)
					$scope.gridOptions3.columnDefs.splice(findInd, 1);


				findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'B_Faculty' });
				if (findInd != -1)
					$scope.gridOptions2.columnDefs.splice(findInd, 1);

				findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'B_Faculty' });
				if (findInd != -1)
					$scope.gridOptions3.columnDefs.splice(findInd, 1);
			}

			if ($scope.AcademicConfig.ActiveLevel == false) {

				findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Level' });
				if (findInd != -1)
					$scope.gridOptions2.columnDefs.splice(findInd, 1);

				findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'Level' });
				if (findInd != -1)
					$scope.gridOptions3.columnDefs.splice(findInd, 1);


				findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'B_Level' });
				if (findInd != -1)
					$scope.gridOptions2.columnDefs.splice(findInd, 1);

				findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'B_Level' });
				if (findInd != -1)
					$scope.gridOptions3.columnDefs.splice(findInd, 1);
			}

			if ($scope.AcademicConfig.ActiveSemester == false) {

				findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' });
				if (findInd != -1)
					$scope.gridOptions2.columnDefs.splice(findInd, 1);

				findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' });
				if (findInd != -1)
					$scope.gridOptions3.columnDefs.splice(findInd, 1);

				findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'B_Semester' });
				if (findInd != -1)
					$scope.gridOptions2.columnDefs.splice(findInd, 1);

				findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'B_Semester' });
				if (findInd != -1)
					$scope.gridOptions3.columnDefs.splice(findInd, 1);
			}

			if ($scope.AcademicConfig.ActiveBatch == false) {
				findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Batch' });
				if (findInd != -1)
					$scope.gridOptions2.columnDefs.splice(findInd, 1);

				findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'Batch' });
				if (findInd != -1)
					$scope.gridOptions3.columnDefs.splice(findInd, 1);

			}

			if ($scope.AcademicConfig.ActiveClassYear == false) {

				findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' });
				if (findInd != -1)
					$scope.gridOptions2.columnDefs.splice(findInd, 1);

				findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' });
				if (findInd != -1)
					$scope.gridOptions3.columnDefs.splice(findInd, 1);


				findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'B_ClassYear' });
				if (findInd != -1)
					$scope.gridOptions2.columnDefs.splice(findInd, 1);

				findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'B_ClassYear' });
				if (findInd != -1)
					$scope.gridOptions3.columnDefs.splice(findInd, 1);
			}


		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.gridApi2.grid.refresh();
		$scope.gridApi3.grid.refresh();

	};
	$rootScope.ChangeLanguage();


	$scope.getterAndSetter = function () {


		$scope.gridOptions1 = [];
		$scope.gridOptions2 = [];
		$scope.gridOptions3 = [];
		$scope.gridOptions4 = [];


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
			exporterMenuCsv: true,
			exporterMenuPdf: true,
			exporterMenuExcel: true,
			columnDefs: [

				{ name: "SNo", displayName: "S.No.", minWidth: 80, headerCellClass: 'headerAligment' },
				{ name: "AccessionNo", displayName: "Accession No.", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "BookTitle", displayName: "Book Title", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Subject", displayName: "Subject", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Authors", displayName: "Author", minWidth: 280, headerCellClass: 'headerAligment' },
				{ name: "Publication", displayName: "Publication", minWidth: 160, headerCellClass: 'headerAligment' },

				{ name: "Edition", displayName: "Edition", minWidth: 130, headerCellClass: 'headerAligment' },
				{ name: "Year", displayName: "Year", minWidth: 90, headerCellClass: 'headerAligment' },

				{ name: "MaterialType", displayName: "MaterialType", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "ISBNNo", displayName: "ISBNNo", minWidth: 160, headerCellClass: 'headerAligment' },

				{ name: "Volume", displayName: "Volume", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Pages", displayName: "Pages", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Language", displayName: "Language", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DonorName", displayName: "Donor", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Department", displayName: "Department", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Medium", displayName: "Medium", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "ClassName", displayName: "Class", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "Faculty", displayName: "Faculty", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "Level", displayName: "Level", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "ClassYear", minWidth: 160, headerCellClass: 'headerAligment' },

				{ name: "Rack", displayName: "Rack No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Location", displayName: "Location", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BookNo", displayName: "BookNo", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "BarCode", displayName: "BarCode", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "CallNo", displayName: "CallNo", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "BookPrice", displayName: "Price", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "Vendor", displayName: "Vendor", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "PurchaseDate", displayName: "Purchase Date", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "BillNo", displayName: "BillNo", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "Status", displayName: "Book Status", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "BookCategory", displayName: "Category", minWidth: 120, headerCellClass: 'headerAligment' },
				{
					name: 'Action',
					enableHiding: false,
					enableFiltering: false,
					enableSorting: false,
					minWidth:110,
					enableColumnResizing: false,
					pinnedRight: true,
					cellClass: 'text-center',
					cellTemplate: 
						'<a href="/Library/Master/BookEntry?bookEntryId={{row.entity.BookEntryId}}" class="p-1" title="Select">' +
						'<i class="fas fa-edit text-info" aria-hidden="true"></i>' +
						'</a>' + '<a href="" class="p-1" title="Select" ng-click="grid.appScope.DelBookEntry(row.entity)">' +
						'<i class="fas fa-trash-alt text-danger" aria-hidden="true"></i>' +
						'</a>'
				}
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'BookList.csv',
			exporterPdfDefaultStyle: { fontSize: 7 },
			//exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: {text: "Dynamic Technosoft Pvt. Ltd.\n Birgunj Nepal",style: 'headerStyle'},
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 18, bold: true, alignment: 'center' };
				docDefinition.styles.footerStyle = { fontSize: 9, alignment: 'center' };
				return docDefinition;
			},
			exporterPdfOrientation: 'landscape',
			exporterPdfPageSize: 'A3',
			exporterPdfMaxGridWidth: 1400,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'BookList.xlsx',
			exporterExcelSheetName: 'BookList',
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
				// Override CSV Export
				var originalCsvExport = gridApi.exporter.csvExport;
				gridApi.exporter.csvExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions1.data || $scope.gridOptions1.data.length === 0) {
						Swal.fire("No Data Found to export CSV!");
						return;
					}
					originalCsvExport(rowTypes, colTypes);
				};
				// Override PDF Export
				var originalPdfExport = gridApi.exporter.pdfExport;
				gridApi.exporter.pdfExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions1.data || $scope.gridOptions1.data.length === 0) {
						Swal.fire("No Data Found to export PDF!");
						return;
					}
					originalPdfExport(rowTypes, colTypes);
				};
				// Override Excel Export
				var originalExcelExport = gridApi.exporter.excelExport;
				gridApi.exporter.excelExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions1.data || $scope.gridOptions1.data.length === 0) {
						Swal.fire("No Data Found to export Excel!");
						return;
					}
					originalExcelExport(rowTypes, colTypes);
				};
			}
		};

		$scope.gridOptions2 = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,
			exporterMenuCsv: true,
			exporterMenuPdf: true,
			exporterMenuExcel: true,
			columnDefs: [

				{ name: "SNo", displayName: "S.No.", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "IssueDate_BS", displayName: "Issued Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "IssueBy", displayName: "Issue By.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "IssueTo", displayName: "Issued To", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "RegdNo", displayName: "Regd.No.", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "ClassName", displayName: "Class", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "SectionName", displayName: "Section", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "AccessionNo", displayName: "Accession No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BookTitle", displayName: "Book Title", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Subject", displayName: "Subject", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Publication", displayName: "Publication", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CreditDays", displayName: "Credit Days", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "TotalDays", displayName: "Total Days", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "OutStandingDays", displayName: "OutStanding Days", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "IssueRemarks", displayName: "Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BookNo", displayName: "BookNo", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "CallNo", displayName: "CallNo", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "BarCode", displayName: "BarCode", minWidth: 100, headerCellClass: 'headerAligment' },

				{ name: "Batch", displayName: "Batch", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "Faculty", displayName: "Faculty", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "Level", displayName: "Level", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "ClassYear", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "BookPrice", displayName: "Price", minWidth: 120, headerCellClass: 'headerAligment' },

				{ name: "Vendor", displayName: "Vendor", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "PurchaseDate_BS", displayName: "Purchase Date", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "BillNo", displayName: "BillNo", minWidth: 120, headerCellClass: 'headerAligment' },
				
				{ name: "B_Faculty", displayName: "B_Faculty", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "B_Level", displayName: "B_Level", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "B_Semester", displayName: "B_Semester", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "B_ClassYear", displayName: "B_ClassYear", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "BookCategory", displayName: "Book Category", minWidth: 120, headerCellClass: 'headerAligment' },
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'Book_Issued.csv',
			exporterPdfDefaultStyle: { fontSize: 7 },
			//exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. \n Birgunj Nepal", style: 'headerStyle' },
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 18, bold: true, alignment: 'center' };
				docDefinition.styles.footerStyle = { fontSize: 9, alignment: 'center' };
				return docDefinition;
			},
			exporterPdfOrientation: 'landscape',
			exporterPdfPageSize: 'A3',
			exporterPdfMaxGridWidth: 1400,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'Book_Issued.xlsx',
			exporterExcelSheetName: 'Book Issued',
			onRegisterApi: function (gridApi) {
				$scope.gridApi2 = gridApi;
				// Override CSV Export
				var originalCsvExport = gridApi.exporter.csvExport;
				gridApi.exporter.csvExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions2.data || $scope.gridOptions2.data.length === 0) {
						Swal.fire("No Data Found to export CSV!");
						return;
					}
					originalCsvExport(rowTypes, colTypes);
				};
				// Override PDF Export
				var originalPdfExport = gridApi.exporter.pdfExport;
				gridApi.exporter.pdfExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions2.data || $scope.gridOptions2.data.length === 0) {
						Swal.fire("No Data Found to export PDF!");
						return;
					}
					originalPdfExport(rowTypes, colTypes);
				};
				// Override Excel Export
				var originalExcelExport = gridApi.exporter.excelExport;
				gridApi.exporter.excelExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions2.data || $scope.gridOptions2.data.length === 0) {
						Swal.fire("No Data Found to export Excel!");
						return;
					}
					originalExcelExport(rowTypes, colTypes);
				};
			}
		};

		/*for third tab*/

		$scope.gridOptions3 = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,
			exporterMenuCsv: true,
			exporterMenuPdf: true,
			exporterMenuExcel: true,
			columnDefs: [

				{ name: "SNo", displayName: "S.No.", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "ReceiedBy", displayName: "Receive By", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ReturnDate_BS", displayName: "Receive Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "RegdNo", displayName: "Regd.No.", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "ClassName", displayName: "Class", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "SectionName", displayName: "Section", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "AccessionNo", displayName: "Accession No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BookTitle", displayName: "Book Title", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "IssueDate_BS", displayName: "Issued Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CreditDays", displayName: "Credit Days", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "OutStandingDays", displayName: "Outstanding Days", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FineAmount", displayName: "Fine Amount", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ReturnRemarks", displayName: "Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FineAmount", displayName: "FineAmount", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BookNo", displayName: "BookNo", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "CallNo", displayName: "CallNo", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "BarCode", displayName: "BarCode", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Batch", displayName: "Batch", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "Faculty", displayName: "Faculty", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "Level", displayName: "Level", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "ClassYear", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "BookPrice", displayName: "Price", minWidth: 120, headerCellClass: 'headerAligment' },

				{ name: "Vendor", displayName: "Vendor", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "PurchaseDate_BS", displayName: "Purchase Date", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "BillNo", displayName: "BillNo", minWidth: 120, headerCellClass: 'headerAligment' },

				{ name: "B_Faculty", displayName: "B_Faculty", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "B_Level", displayName: "B_Level", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "B_Semester", displayName: "B_Semester", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "B_ClassYear", displayName: "B_ClassYear", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "BookCategory", displayName: "Book Category", minWidth: 150, headerCellClass: 'headerAligment' },
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'Book_Received.csv',
			exporterPdfDefaultStyle: { fontSize: 7 },
			//exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. \n Birgunj Nepal", style: 'headerStyle' },
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 18, bold: true, alignment: 'center' };
				docDefinition.styles.footerStyle = { fontSize: 9, alignment: 'center' };
				return docDefinition;
			},
			exporterPdfOrientation: 'landscape',
			exporterPdfPageSize: 'A3',
			exporterPdfMaxGridWidth: 1400,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'Book_Received.xlsx',
			exporterExcelSheetName: 'Book Received',
			onRegisterApi: function (gridApi) {
				$scope.gridApi3 = gridApi;
				// Override CSV Export
				var originalCsvExport = gridApi.exporter.csvExport;
				gridApi.exporter.csvExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions3.data || $scope.gridOptions3.data.length === 0) {
						Swal.fire("No Data Found to export CSV!");
						return;
					}
					originalCsvExport(rowTypes, colTypes);
				};
				// Override PDF Export
				var originalPdfExport = gridApi.exporter.pdfExport;
				gridApi.exporter.pdfExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions3.data || $scope.gridOptions3.data.length === 0) {
						Swal.fire("No Data Found to export PDF!");
						return;
					}
					originalPdfExport(rowTypes, colTypes);
				};
				// Override Excel Export
				var originalExcelExport = gridApi.exporter.excelExport;
				gridApi.exporter.excelExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions3.data || $scope.gridOptions3.data.length === 0) {
						Swal.fire("No Data Found to export Excel!");
						return;
					}
					originalExcelExport(rowTypes, colTypes);
				};
			}
		};

		$scope.gridOptions4 = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,
			exporterMenuCsv: true,
			exporterMenuPdf: true,
			exporterMenuExcel: true,
			columnDefs: [

				{ name: "SNo", displayName: "S.No.", minWidth: 80, headerCellClass: 'headerAligment' },
				{ name: "IssueBy", displayName: "Issue By", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "IssueTo", displayName: "Issue To", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "IssueDate_BS", displayName: "Issue Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ReturnDate_BS", displayName: "Return Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TotalDays", displayName: "Outstanding Days", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FineAmount", displayName: "FineAmount", minWidth: 140, headerCellClass: 'headerAligment' },

			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'BooksLedger.csv',
			exporterPdfDefaultStyle: { fontSize: 9 },
			//exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. \n Birgunj Nepal", style: 'headerStyle' },
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 18, bold: true, alignment: 'center' };
				docDefinition.styles.footerStyle = { fontSize: 9, alignment: 'center' };
				return docDefinition;
			},
			exporterPdfOrientation: 'portrait',
			exporterPdfPageSize: 'LETTER',
			exporterPdfMaxGridWidth: 500,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'BooksLedger.xlsx',
			exporterExcelSheetName: 'Books Ledger',
			onRegisterApi: function (gridApi) {
				$scope.gridApi4 = gridApi;
				// Override CSV Export
				var originalCsvExport = gridApi.exporter.csvExport;
				gridApi.exporter.csvExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions4.data || $scope.gridOptions4.data.length === 0) {
						Swal.fire("No Data Found to export CSV!");
						return;
					}
					originalCsvExport(rowTypes, colTypes);
				};
				// Override PDF Export
				var originalPdfExport = gridApi.exporter.pdfExport;
				gridApi.exporter.pdfExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions4.data || $scope.gridOptions4.data.length === 0) {
						Swal.fire("No Data Found to export PDF!");
						return;
					}
					originalPdfExport(rowTypes, colTypes);
				};
				// Override Excel Export
				var originalExcelExport = gridApi.exporter.excelExport;
				gridApi.exporter.excelExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions4.data || $scope.gridOptions4.data.length === 0) {
						Swal.fire("No Data Found to export Excel!");
						return;
					}
					originalExcelExport(rowTypes, colTypes);
				};
			}
		};

		$scope.gridOptions5 = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,
			exporterMenuCsv: true,
			exporterMenuPdf: true,
			exporterMenuExcel: true,
			columnDefs: [

				{ name: "SNo", displayName: "S.No.", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "AccessionNo", displayName: "Accession No.", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "BookTitle", displayName: "Book Title", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Subject", displayName: "Subject", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Authors", displayName: "Author", minWidth: 280, headerCellClass: 'headerAligment' },
				{ name: "Publication", displayName: "Publication", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "Rack", displayName: "Rack No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Location", displayName: "Location", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BookNo", displayName: "BookNo", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "BarCode", displayName: "BarCode", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "CallNo", displayName: "CallNo", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "IssueBy", displayName: "Issue By", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "IssueDate_BS", displayName: "Issue Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ReturnDate_BS", displayName: "Return Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TotalDays", displayName: "Outstanding Days", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FineAmount", displayName: "FineAmount", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BookPrice", displayName: "Price", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "BookCategory", displayName: "Book Category", minWidth: 120, headerCellClass: 'headerAligment' },
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'Student_Employee_Ledger.csv',
			exporterPdfDefaultStyle: { fontSize: 7 },
			//exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. \n Birgunj Nepal", style: 'headerStyle' },
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 18, bold: true, alignment: 'center' };
				docDefinition.styles.footerStyle = { fontSize: 9, alignment: 'center' };
				return docDefinition;
			},
			exporterPdfOrientation: 'landscape',
			exporterPdfPageSize: 'A3',
			exporterPdfMaxGridWidth: 1400,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'Student_Employee_Ledger.xlsx',
			exporterExcelSheetName: 'Student/Employee Ledger',
			onRegisterApi: function (gridApi) {
				$scope.gridApi5 = gridApi;
				// Override CSV Export
				var originalCsvExport = gridApi.exporter.csvExport;
				gridApi.exporter.csvExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions5.data || $scope.gridOptions5.data.length === 0) {
						Swal.fire("No Data Found to export CSV!");
						return;
					}
					originalCsvExport(rowTypes, colTypes);
				};
				// Override PDF Export
				var originalPdfExport = gridApi.exporter.pdfExport;
				gridApi.exporter.pdfExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions5.data || $scope.gridOptions5.data.length === 0) {
						Swal.fire("No Data Found to export PDF!");
						return;
					}
					originalPdfExport(rowTypes, colTypes);
				};
				// Override Excel Export
				var originalExcelExport = gridApi.exporter.excelExport;
				gridApi.exporter.excelExport = function (rowTypes, colTypes) {
					if (!$scope.gridOptions5.data || $scope.gridOptions5.data.length === 0) {
						Swal.fire("No Data Found to export Excel!");
						return;
					}
					originalExcelExport(rowTypes, colTypes);
				};
			}
		};
	};

	var gSrv = GlobalServices;
	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.IssueToList = [{ id: 1, text: 'Student' }, { id: 2, text: 'Teacher' }];
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();

		$scope.confirmMSG = gSrv.getConfirmMSG();

		$scope.MaterialTypeList = [];
		gSrv.getMaterialTypeList().then(function (res) {
			$scope.MaterialTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.bookList = {
			MaterialTypeId: 0,
			ForType: 0
		};

		$scope.issueRegister = {
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date()
		};

		$scope.receivedRegister = {
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date()
		};

		$scope.newStudent = {
			StudentId: null,
			EmployeeId: null,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			IssueTo:1
		};

		$scope.BookSearchOptions = [{ text: 'AccessionNo', value: 'BD.AccessionNo' }, { text: 'Book Title', value: 'BD.BookTitle' }, { text: 'Subject', value: 'BD.Subject' }, { text: 'BookNo', value: 'BD.BookNo' }, { text: 'CallNo', value: 'BD.CallNo' }];
		$scope.bookRegister = {
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
			BookEntryId: 0,
			SelectBook: $scope.BookSearchOptions[0].value,
		};
		$scope.getterAndSetter();
		//$scope.GetBookDetailsList();
	}


	$scope.GetBookDetailsList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetBookDetailsList",
			dataType: "json",
			data: JSON.stringify($scope.bookList)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.gridOptions1.data = res.data.Data;
				if (!res.data.Data || res.data.Data.length == 0) {
					Swal.fire("No data found");
                }
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.DelBookEntry = function (refData) {

		var s = refData.StartedAccessionNo;
		var ed = refData.EndedAccessionNo;

		Swal.fire({
			title: 'Do you want to delete the selected book details (' + s + ' To ' + ed + ' ) ?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					BookEntryId: refData.BookEntryId,
					TranId: refData.TranId
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelBookEntry",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetBookDetailsList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});

	};

	$scope.EditBookEntry = function (refData) {
		openMenuWindow("BookEntry", "/Library/Master/BookEntry?bookEntryId=" + refData.BookEntryId);
	}

	$scope.GetBookIssueRegister = function (pendingForReceived) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			dateFrom: new Date(($filter('date')($scope.issueRegister.DateFromDet.dateAD, 'yyyy-MM-dd'))),
			dateTo: new Date(($filter('date')($scope.issueRegister.DateToDet.dateAD, 'yyyy-MM-dd'))),
			PendingForReceived: pendingForReceived
		};
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetBookIssueRegister",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.gridOptions2.data = res.data.Data;
				if (!res.data.Data || res.data.Data.length == 0) {
					Swal.fire("No data found");
				}
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetBookReceivedRegister = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			dateFrom: new Date(($filter('date')($scope.receivedRegister.DateFromDet.dateAD, 'yyyy-MM-dd'))),
			dateTo: new Date(($filter('date')($scope.receivedRegister.DateToDet.dateAD, 'yyyy-MM-dd'))),
		};
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetBookReceivedRegister",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.gridOptions3.data = res.data.Data;
				if (!res.data.Data || res.data.Data.length == 0) {
					Swal.fire("No data found");
				}
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetBookRegister = function () {

		if ($scope.bookRegister.DateFromDet && $scope.bookRegister.DateToDet && $scope.bookRegister.BookEntryId) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				dateFrom: new Date(($filter('date')($scope.bookRegister.DateFromDet.dateAD, 'yyyy-MM-dd'))),
				dateTo: new Date(($filter('date')($scope.bookRegister.DateToDet.dateAD, 'yyyy-MM-dd'))),
				BookEntryId: $scope.bookRegister.BookEntryId
			};
			$http({
				method: 'POST',
				url: base_url + "Library/Master/GetBookRegister",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.gridOptions4.data = res.data.Data;
					if (!res.data.Data || res.data.Data.length == 0) {
						Swal.fire("No data found");
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.GetStudentBookRegister = function () {

		if ($scope.newStudent.StudentId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				StudentId: $scope.newStudent.StudentId,
				EmployeeId: null
			};
			$http({
				method: 'POST',
				url: base_url + "Library/Master/GetStudentEmpBookRegister",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.gridOptions5.data = res.data.Data;
					if (!res.data.Data || res.data.Data.length == 0) {
						Swal.fire("No data found");
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.GetEmpBookRegister = function () {
		if ($scope.newStudent.EmployeeId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				StudentId: null,
				EmployeeId: $scope.newStudent.EmployeeId
			};
			$http({
				method: 'POST',
				url: base_url + "Library/Master/GetStudentEmpBookRegister",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.gridOptions5.data = res.data.Data;
					if (!res.data.Data || res.data.Data.length == 0) {
						Swal.fire("No data found");
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.PrintBookList = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityBooksList + "&voucherId=0&isTran=false",
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
											$scope.gridOptions1.api.forEachNodeAfterFilterAndSort(function (node) {
												dataColl.push(node.data);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Global/PrintReportData",
												headers: { 'Content-Type': undefined },
												transformRequest: function (data) {
													var formData = new FormData();
													formData.append("entityId", entityBooksList);
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
														entityid: entityBooksList,
														voucherid: 0,
														tranid: 0,
														vouchertype: 0,
														sessionid: res.data.Data.ResponseId,
														//Period: $scope.issueRegister.DateFromDet.dateBS + ' TO ' + $scope.issueRegister.DateToDet.dateBS,
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
						$scope.gridOptions1.api.forEachNodeAfterFilterAndSort(function (node) {
							dataColl.push(node.data);
						});
						print = true;
						$http({
							method: 'POST',
							url: base_url + "Global/PrintReportData",
							headers: { 'Content-Type': undefined },
							transformRequest: function (data) {
								var formData = new FormData();
								formData.append("entityId", entityBooksList);
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
									entityid: entityBooksList,
									voucherid: 0,
									tranid: 0,
									vouchertype: 0,
									sessionid: res.data.Data.ResponseId,
									//Period: $scope.issueRegister.DateFromDet.dateBS + ' TO ' + $scope.issueRegister.DateToDet.dateBS,
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

	$scope.PrintBooksIssuedRegister = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityBooksIssuedRegister + "&voucherId=0&isTran=false",
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
											$scope.gridOptions2.api.forEachNodeAfterFilterAndSort(function (node) {
												dataColl.push(node.data);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Global/PrintReportData",
												headers: { 'Content-Type': undefined },
												transformRequest: function (data) {
													var formData = new FormData();
													formData.append("entityId", entityBooksIssuedRegister);
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
														entityid: entityBooksIssuedRegister,
														voucherid: 0,
														tranid: 0,
														vouchertype: 0,
														sessionid: res.data.Data.ResponseId,
														Period: $scope.issueRegister.DateFromDet.dateBS + ' TO ' + $scope.issueRegister.DateToDet.dateBS,
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
						$scope.gridOptions2.api.forEachNodeAfterFilterAndSort(function (node) {
							dataColl.push(node.data);
						});
						print = true;
						$http({
							method: 'POST',
							url: base_url + "Global/PrintReportData",
							headers: { 'Content-Type': undefined },
							transformRequest: function (data) {
								var formData = new FormData();
								formData.append("entityId", entityBooksIssuedRegister);
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
									entityid: entityBooksIssuedRegister,
									voucherid: 0,
									tranid: 0,
									vouchertype: 0,
									sessionid: res.data.Data.ResponseId,
									Period: $scope.issueRegister.DateFromDet.dateBS + ' TO ' + $scope.issueRegister.DateToDet.dateBS,
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

	$scope.PrintBooksReceivedRegister = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityBooksReceivedRegister + "&voucherId=0&isTran=false",
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
											$scope.gridOptions3.api.forEachNodeAfterFilterAndSort(function (node) {
												dataColl.push(node.data);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Global/PrintReportData",
												headers: { 'Content-Type': undefined },
												transformRequest: function (data) {
													var formData = new FormData();
													formData.append("entityId", entityBooksReceivedRegister);
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
														entityid: entityBooksReceivedRegister,
														voucherid: 0,
														tranid: 0,
														vouchertype: 0,
														sessionid: res.data.Data.ResponseId,
														Period: $scope.receivedRegister.DateFromDet.dateBS + ' TO ' + $scope.receivedRegister.DateToDet.dateBS,
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
						$scope.gridOptions3.api.forEachNodeAfterFilterAndSort(function (node) {
							dataColl.push(node.data);
						});
						print = true;
						$http({
							method: 'POST',
							url: base_url + "Global/PrintReportData",
							headers: { 'Content-Type': undefined },
							transformRequest: function (data) {
								var formData = new FormData();
								formData.append("entityId", entityBooksReceivedRegister);
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
									entityid: entityBooksReceivedRegister,
									voucherid: 0,
									tranid: 0,
									vouchertype: 0,
									sessionid: res.data.Data.ResponseId,
									Period: $scope.receivedRegister.DateFromDet.dateBS + ' TO ' + $scope.receivedRegister.DateToDet.dateBS,
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

	$scope.PrintBooksLedger = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityBooksLedger + "&voucherId=0&isTran=false",
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
											$scope.gridOptions4.api.forEachNodeAfterFilterAndSort(function (node) {
												dataColl.push(node.data);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Global/PrintReportData",
												headers: { 'Content-Type': undefined },
												transformRequest: function (data) {
													var formData = new FormData();
													formData.append("entityId", entityBooksLedger);
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
														entityid: entityBooksLedger,
														voucherid: 0,
														tranid: 0,
														vouchertype: 0,
														sessionid: res.data.Data.ResponseId,
														Period: $scope.bookRegister.DateFromDet.dateBS + ' TO ' + $scope.bookRegister.DateToDet.dateBS,
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
						$scope.gridOptions4.api.forEachNodeAfterFilterAndSort(function (node) {
							dataColl.push(node.data);
						});
						print = true;
						$http({
							method: 'POST',
							url: base_url + "Global/PrintReportData",
							headers: { 'Content-Type': undefined },
							transformRequest: function (data) {
								var formData = new FormData();
								formData.append("entityId", entityBooksLedger);
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
									entityid: entityBooksLedger,
									voucherid: 0,
									tranid: 0,
									vouchertype: 0,
									sessionid: res.data.Data.ResponseId,
									Period: $scope.bookRegister.DateFromDet.dateBS + ' TO ' + $scope.bookRegister.DateToDet.dateBS,
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

	$scope.PrintStudentEmployeeLedger = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityStudent_EmployeeLedger + "&voucherId=0&isTran=false",
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
											$scope.gridOptions5.api.forEachNodeAfterFilterAndSort(function (node) {
												dataColl.push(node.data);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Global/PrintReportData",
												headers: { 'Content-Type': undefined },
												transformRequest: function (data) {
													var formData = new FormData();
													formData.append("entityId", entityStudent_EmployeeLedger);
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
														entityid: entityStudent_EmployeeLedger,
														voucherid: 0,
														tranid: 0,
														vouchertype: 0,
														sessionid: res.data.Data.ResponseId,
														Period: $scope.bookRegister.DateFromDet.dateBS + ' TO ' + $scope.bookRegister.DateToDet.dateBS,
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
						$scope.gridOptions5.api.forEachNodeAfterFilterAndSort(function (node) {
							dataColl.push(node.data);
						});
						print = true;
						$http({
							method: 'POST',
							url: base_url + "Global/PrintReportData",
							headers: { 'Content-Type': undefined },
							transformRequest: function (data) {
								var formData = new FormData();
								formData.append("entityId", entityStudent_EmployeeLedger);
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
									entityid: entityStudent_EmployeeLedger,
									voucherid: 0,
									tranid: 0,
									vouchertype: 0,
									sessionid: res.data.Data.ResponseId,
									//Period: $scope.bookRegister.DateFromDet.dateBS + ' TO ' + $scope.bookRegister.DateToDet.dateBS,
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

	$scope.validateDates = function (changedField, obj) {
		if (!obj.DateFromDet || !obj.DateToDet ||
			!obj.DateFromDet.dateAD || !obj.DateToDet.dateAD) {
			return true;
		}

		var fromDate = $filter('date')(new Date(obj.DateFromDet.dateAD), 'yyyy-MM-dd');
		var toDate = $filter('date')(new Date(obj.DateToDet.dateAD), 'yyyy-MM-dd');

		if (!fromDate || !toDate) return true;

		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.DateFrom_TMP = new Date();
						obj.DateFromDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						obj.DateTo_TMP = new Date();
						obj.DateToDet = new Date();
					});
				});
			}
			return false;
		}
		return true;
	};
});