app.controller('LibDashboard', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Book Title';

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

			columnDefs: [

				{ name: "SNo", displayName: "S.No.", minWidth: 80, headerCellClass: 'headerAligment' },
				{ name: "AccessionNo", displayName: "Accession No.", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "BookTitle", displayName: "Book Title", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Subject", displayName: "Subject", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Authors", displayName: "Author", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "Publication", displayName: "Publication", minWidth: 160, headerCellClass: 'headerAligment' },

				{ name: "Edition", displayName: "Edition", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Year", displayName: "Year", minWidth: 90, headerCellClass: 'headerAligment' },

				{ name: "MaterialType", displayName: "MaterialType", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "ISBNNo", displayName: "ISBNNo", minWidth: 130, headerCellClass: 'headerAligment' },

				{ name: "Volume", displayName: "Volume", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Pages", displayName: "Pages", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Language", displayName: "Language", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "DonorName", displayName: "Donor", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Department", displayName: "Department", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Medium", displayName: "Medium", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "ClassName", displayName: "Class", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "Faculty", displayName: "Faculty", minWidth: 130, headerCellClass: 'headerAligment' },
				{ name: "Level", displayName: "Level", minWidth: 130, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "ClassYear", minWidth: 120, headerCellClass: 'headerAligment' },

				{ name: "Rack", displayName: "Rack No.", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "Location", displayName: "Location", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BookNo", displayName: "BookNo", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "BarCode", displayName: "BarCode", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "CallNo", displayName: "CallNo", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "BookPrice", displayName: "Price", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "Vendor", displayName: "Vendor", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "PurchaseDate", displayName: "Purchase Date", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "BillNo", displayName: "BillNo", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "BookCategory", displayName: "Category", minWidth: 120, headerCellClass: 'headerAligment' },
				
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
				$scope.gridApi2 = gridApi;
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
				$scope.gridApi3 = gridApi;
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
				$scope.gridApi4 = gridApi;
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
				$scope.gridApi5 = gridApi;
			}
		};
	};


	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		

		$scope.currentPages = {
			BookTitle: 1,
			Author: 1,
			
		};

		$scope.searchData = {
			BookTitle: '',
			Author: '',
			
		};

		$scope.totalBookCount = 0;

		$scope.perPage = {
			BookTitle: GlobalServices.getPerPageRow(),
		
		};

		$scope.newBookTitle = {
			BookTitleId: null,
			Name: '',
			Description: '',

			Mode: 'Save'
		};


		$scope.ClassList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ClassYearList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassYearList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassYearList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.LevelList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassLevelList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LevelList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.BookTitleList = [];
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllBookTitleList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BookTitleList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.AuthorList = [];
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllAuthorList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AuthorList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.FacultyList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllFacultyList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FacultyList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.EditionList = [];
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllEditionList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EditionList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.SemesterList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllSemesterList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SemesterList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



		$scope.PublicationList = [];
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllPublicationList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PublicationList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



		$scope.SubjectList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllSubjectList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SubjectList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.DonorList = [];
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllDonorList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DonorList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.RackList = [];
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllRackList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.RackList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



		$scope.BookCategoryList = [];
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllBookCategory",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BookCategoryList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		$scope.modalTitle = "Total Books";
		$scope.getterAndSetter();
		$scope.GetTotalBookList(0);  // Load total books
		$scope.GetTotalBookList(1);
		$scope.GetAllBooksTaken(true);
		$scope.GetAllBooksTaken(false);
		$scope.GetAllBooksReturned();
		
	}


	$scope.GetTotalBookList = function (forType) {
		$scope.newDet = $scope.newDet || {};
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.modalTitle = forType === 1 ? "Remaining Books" : "Total Books";
		var para = {
			SubjectIdColl: ($scope.newDet.SubjectIdColl && $scope.newDet.SubjectIdColl.length > 0) ? $scope.newDet.SubjectIdColl.toString() : null,
			AuthorIdColl: ($scope.newDet.AuthorIdColl && $scope.newDet.AuthorIdColl.length > 0) ? $scope.newDet.AuthorIdColl.toString() : null,
			PublicationIdColl: ($scope.newDet.PublicationIdColl && $scope.newDet.PublicationIdColl.length > 0) ? $scope.newDet.PublicationIdColl.toString() : null,
			EditionIdColl: ($scope.newDet.EditionIdColl && $scope.newDet.EditionIdColl.length > 0) ? $scope.newDet.EditionIdColl.toString() : null,
			CategoryIdColl: ($scope.newDet.CategoryIdColl && $scope.newDet.CategoryIdColl.length > 0) ? $scope.newDet.CategoryIdColl.toString() : null,
			ClassIdColl: ($scope.newDet.ClassIdColl && $scope.newDet.ClassIdColl.length > 0) ? $scope.newDet.ClassIdColl.toString() : null,
			FacultyIdColl: ($scope.newDet.FacultyIdColl && $scope.newDet.FacultyIdColl.length > 0) ? $scope.newDet.FacultyIdColl.toString() : null,
			LevelIdColl: ($scope.newDet.LevelIdColl && $scope.newDet.LevelIdColl.length > 0) ? $scope.newDet.LevelIdColl.toString() : null,
			SemesterIdColl: ($scope.newDet.SemesterIdColl && $scope.newDet.SemesterIdColl.length > 0) ? $scope.newDet.SemesterIdColl.toString() : null,
			ClassYearIdColl: ($scope.newDet.ClassYearIdColl && $scope.newDet.ClassYearIdColl.length > 0) ? $scope.newDet.ClassYearIdColl.toString() : null,
			ForType: forType || 0
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllBookLit",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.gridOptions1.data = res.data.Data;
				if (forType === 1) {
					$scope.remainingBookList = res.data.Data;
					$scope.remainingBookCount = res.data.Data.length;
				} else {
					$scope.gridOptions1.data = res.data.Data;
					$scope.totalBookCount = res.data.Data.length;
				}

				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});
	};

	$scope.allTakenList = [];
	$scope.yetToReturnList = [];

	$scope.allTakenStudentCount = 0;
	$scope.allTakenTeacherCount = 0;
	$scope.yetToReturnStudentCount = 0;
	$scope.yetToReturnTeacherCount = 0;

	let dataLoaded = { false: false, true: false, returned: false };

	$scope.GetAllBooksTaken = function (pendingForReceived) {
		$scope.newDet = $scope.newDet || {};
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			SubjectIdColl: ($scope.newDet.SubjectIdColl && $scope.newDet.SubjectIdColl.length > 0) ? $scope.newDet.SubjectIdColl.toString() : null,
			AuthorIdColl: ($scope.newDet.AuthorIdColl && $scope.newDet.AuthorIdColl.length > 0) ? $scope.newDet.AuthorIdColl.toString() : null,
			PublicationIdColl: ($scope.newDet.PublicationIdColl && $scope.newDet.PublicationIdColl.length > 0) ? $scope.newDet.PublicationIdColl.toString() : null,
			EditionIdColl: ($scope.newDet.EditionIdColl && $scope.newDet.EditionIdColl.length > 0) ? $scope.newDet.EditionIdColl.toString() : null,
			CategoryIdColl: ($scope.newDet.CategoryIdColl && $scope.newDet.CategoryIdColl.length > 0) ? $scope.newDet.CategoryIdColl.toString() : null,
			ClassIdColl: ($scope.newDet.ClassIdColl && $scope.newDet.ClassIdColl.length > 0) ? $scope.newDet.ClassIdColl.toString() : null,
			FacultyIdColl: ($scope.newDet.FacultyIdColl && $scope.newDet.FacultyIdColl.length > 0) ? $scope.newDet.FacultyIdColl.toString() : null,
			LevelIdColl: ($scope.newDet.LevelIdColl && $scope.newDet.LevelIdColl.length > 0) ? $scope.newDet.LevelIdColl.toString() : null,
			SemesterIdColl: ($scope.newDet.SemesterIdColl && $scope.newDet.SemesterIdColl.length > 0) ? $scope.newDet.SemesterIdColl.toString() : null,
			ClassYearIdColl: ($scope.newDet.ClassYearIdColl && $scope.newDet.ClassYearIdColl.length > 0) ? $scope.newDet.ClassYearIdColl.toString() : null,
			PendingForReceived: pendingForReceived
		};
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllBooksTaken",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				const data = res.data.Data;
				const studentCount = data.filter(item => item.IssueTo?.toLowerCase() === "student").length;
				const teacherCount = data.filter(item => item.IssueTo?.toLowerCase() === "teacher").length;

				if (pendingForReceived) {
					$scope.yetToReturnList = data;
					$scope.yetToReturnCount = data.length;
					$scope.gridOptions2.data = data;
					$scope.yetToReturnStudentCount = studentCount;
					$scope.yetToReturnTeacherCount = teacherCount;
				} else {
					$scope.allTakenList = data;
					$scope.gridOptions2.data = data;
					$scope.allTakenCount = data.length;
					$scope.allTakenStudentCount = studentCount;
					$scope.allTakenTeacherCount = teacherCount;
				}

				dataLoaded[pendingForReceived] = true;

				if (dataLoaded[true] && dataLoaded[false] && dataLoaded.returned) {
					$scope.StudentSummary();
					$scope.EmployeeSummary();
				}
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});
	};

	// 🔄 Call both API on load
	$scope.init = function () {
		$scope.GetAllBooksTaken(false); // total issued
		$scope.GetAllBooksTaken(true);  // yet to return
	};
	$scope.init();

	$scope.GetAllBooksReturned = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			SubjectIdColl: ($scope.newDet.SubjectIdColl && $scope.newDet.SubjectIdColl.length > 0) ? $scope.newDet.SubjectIdColl.toString() : null,
			AuthorIdColl: ($scope.newDet.AuthorIdColl && $scope.newDet.AuthorIdColl.length > 0) ? $scope.newDet.AuthorIdColl.toString() : null,
			PublicationIdColl: ($scope.newDet.PublicationIdColl && $scope.newDet.PublicationIdColl.length > 0) ? $scope.newDet.PublicationIdColl.toString() : null,
			EditionIdColl: ($scope.newDet.EditionIdColl && $scope.newDet.EditionIdColl.length > 0) ? $scope.newDet.EditionIdColl.toString() : null,
			CategoryIdColl: ($scope.newDet.CategoryIdColl && $scope.newDet.CategoryIdColl.length > 0) ? $scope.newDet.CategoryIdColl.toString() : null,
			ClassIdColl: ($scope.newDet.ClassIdColl && $scope.newDet.ClassIdColl.length > 0) ? $scope.newDet.ClassIdColl.toString() : null,
			FacultyIdColl: ($scope.newDet.FacultyIdColl && $scope.newDet.FacultyIdColl.length > 0) ? $scope.newDet.FacultyIdColl.toString() : null,
			LevelIdColl: ($scope.newDet.LevelIdColl && $scope.newDet.LevelIdColl.length > 0) ? $scope.newDet.LevelIdColl.toString() : null,
			SemesterIdColl: ($scope.newDet.SemesterIdColl && $scope.newDet.SemesterIdColl.length > 0) ? $scope.newDet.SemesterIdColl.toString() : null,
			ClassYearIdColl: ($scope.newDet.ClassYearIdColl && $scope.newDet.ClassYearIdColl.length > 0) ? $scope.newDet.ClassYearIdColl.toString() : null,
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllBookReturned",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				$scope.gridOptions3.data = res.data.Data;
				$scope.allReturnedCount = res.data.Data.length;

				$scope.studentReturnedCount = res.data.Data.filter(x => x.IssueTo === 'Student').length;
				$scope.teacherReturnedCount = res.data.Data.filter(x => x.IssueTo === 'Teacher').length;

				$scope.BooksSummary();

				dataLoaded.returned = true;

				if (dataLoaded[true] && dataLoaded[false] && dataLoaded.returned) {
					$scope.StudentSummary();
					$scope.EmployeeSummary();
				}
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.StudentSummary = function () {
		const canvas = document.getElementById('borrowerChart');
		const ctx = canvas.getContext('2d');

		// Destroy any existing chart on the canvas
		const existingChart = Chart.getChart(canvas);
		if (existingChart) existingChart.destroy();

		new Chart(ctx, {
			type: 'bar',
			data: {
				labels: ['Total Issued', 'Returned', 'Yet To Return', 'Lost'],
				datasets: [{
					label: 'Borrower History',
					data: [
						$scope.allTakenStudentCount || 0,
						$scope.studentReturnedCount || 0,
						$scope.yetToReturnStudentCount || 0,
						0
					],
					backgroundColor: ['#2F5597', '#70AD47', '#ED7D31', '#C00000'],
					borderRadius: 5
				}]
			},
			options: {
				scales: { y: { beginAtZero: true } },
				plugins: { legend: { display: false } }
			}
		});
	};

	$scope.EmployeeSummary = function () {
		const canvas = document.getElementById('borrowerChart2');
		const ctx = canvas.getContext('2d');

		// Destroy any existing chart on the canvas
		const existingChart = Chart.getChart(canvas);
		if (existingChart) existingChart.destroy();

		new Chart(ctx, {
			type: 'bar',
			data: {
				labels: ['Total Issued', 'Returned', 'Yet To Return', 'Lost'],
				datasets: [{
					label: 'Borrower History',
					data: [
						$scope.allTakenTeacherCount || 0,
						$scope.teacherReturnedCount || 0,
						$scope.yetToReturnTeacherCount || 0,
						0
					],
					backgroundColor: ['#2F5597', '#70AD47', '#ED7D31', '#C00000'],
					borderRadius: 5
				}]
			},
			options: {
				scales: { y: { beginAtZero: true } },
				plugins: { legend: { display: false } }
			}
		});
	};

	$scope.BooksSummary = function () {
		// Get the canvas element for the book chart
		var canvas = document.getElementById("bookChart");
		var ctx = canvas.getContext('2d');

		// Data for the doughnut chart
		var data = {
			labels: ['Books Taken', 'Yet to Return', 'Returned', 'Lost'],
			datasets: [{
				data: [
					$scope.allTakenCount || 0,
					$scope.yetToReturnCount || 0,
					$scope.allReturnedCount || 0,
					$scope.lostCount || 0
				],
				backgroundColor: [
					'#4e79a7', // Books Taken
					'#f28e2b', // Yet to Return
					'#59a14f', // Returned
					'#C00000'  // Lost
				],
				borderColor: ['#ffffff', '#ffffff', '#ffffff', '#ffffff'],
				borderWidth: 1
			}]
		};

		// Options for the doughnut chart
		var options = {
			responsive: true,
			plugins: {
				legend: {
					position: 'right',
					display: true
				},
				tooltip: {
					callbacks: {
						label: function (tooltipItem) {
							const label = tooltipItem.label || '';
							const value = tooltipItem.raw || 0;
							return `${label}: ${value}`;
						}
					}
				}
			}
		};

		// Destroy the existing book chart instance if it exists
		if (window.bookChartInstance) {
			window.bookChartInstance.destroy();
		}

		// Create a new doughnut chart
		window.bookChartInstance = new Chart(ctx, {
			type: 'doughnut',
			data: data,
			options: options
		});
	};

	$scope.ChangeLibrary = function () {

		$scope.GetTotalBookList(0);  // Load total books
		$scope.GetTotalBookList(1);
		$scope.GetAllBooksTaken(true);
		$scope.GetAllBooksTaken(false);
		$scope.GetAllBooksReturned();
    }

});