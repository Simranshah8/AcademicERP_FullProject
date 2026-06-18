//"use strict";

//agGrid.initialiseAgGridWithAngular1(angular);


app.controller('FeeSummaryController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate, uiGridConstants, uiGridTreeViewConstants) {
	$scope.Title = 'Fee Summary';


	getterAndSetter();

	$rootScope.ConfigFunction = function () {
		$scope.LoadData();
		$('.select2').select2();

		$scope.AllClassList = [];
		GlobalServices.getClassList().then(function (res) {
			$scope.AllClassList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AcademicConfig = {};
		//pahela res1 theyo...eslai res banako at 25 chaitra 2081
		GlobalServices.getAcademicConfig().then(function (res) {
			$scope.AcademicConfig = res.data.Data;

			if ($scope.AcademicConfig.ActiveFaculty == true) {

				$scope.FacultyList = [];
				GlobalServices.getFacultyList().then(function (res) {
					$scope.FacultyList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				$scope.gridOptions.columnApi.setColumnsVisible(["Faculty"], false);
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["Faculty"], false);
			}




			if ($scope.AcademicConfig.ActiveLevel == true) {

				$scope.LevelList = [];
				GlobalServices.getClassLevelList().then(function (res) {
					$scope.LevelList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				$scope.gridOptions.columnApi.setColumnsVisible(["Level"], false);
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["Level"], false);
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
				$scope.gridOptions.columnApi.setColumnsVisible(["Semester"], false);
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["Semester"], false);
			}

			if ($scope.AcademicConfig.ActiveBatch == true) {

				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				$scope.gridOptions.columnApi.setColumnsVisible(["Batch"], false);
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["Batch"], false);
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
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["ClassYear"], false);
				$scope.gridOptions.columnApi.setColumnsVisible(["ClassYear"], false);//DONE: Was Missing
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
	$scope.onBtExportCSV = function () {
		var params = {
			filefield: 'FeeSummaryDateWise.csv',
			sheetfield: 'FeeSummaryDateWise'
		};
		$scope.gridOptionsDW.api.exportDataAsCsv(params);
	}

	$rootScope.ChangeLanguage();

	$scope.onFilterTextBoxChanged = function () {
		$scope.gridOptions.api.setQuickFilter($scope.search);
	}
	$scope.onFilterTextBoxChangedDW = function () {
		$scope.gridOptionsDW.api.setQuickFilter($scope.searchDW);
	}

	$scope.onFilterTextBoxChangedSib = function () {
		$scope.gridOptionsSib.api.setQuickFilter($scope.searchSib);
	}


	function getterAndSetter() {

		$scope.discounts = [];

		$scope.gridColumnDef = [
			{ headerName: "Id", width: 90, field: "AutoNumber", filter: 'agNumberColumnFilter', sortable: true, pinned: 'left' },
			{ headerName: "Reg.No.", width: 120, field: "RegdNo", filter: 'agTextColumnFilter', sortable: true, pinned: 'left' },
			{ headerName: "Roll No.", width: 110, field: "RollNo", filter: 'agNumberColumnFilter', pinned: 'left' },
			/*	{ headerName: "Name", width: 180, field: "Name", filter: 'agTextColumnFilter', },*/

			{
				headerName: "Name", field: "Name", filter: 'agTextColumnFilter', width: 180,
				cellStyle: function (params) {
					if (params.data.IsLeft == true) {
						return { color: 'red' };
					}
					else if (params.data.IsTransport == true)
						return { color: 'green' }
					else if (params.data.IsHostel == true)
						return { color: 'blue' }
					else {
						return null;
					}
				}, pinned: 'left'
			},

			{ headerName: "Class", width: 120, field: "ClassName", filter: 'agTextColumnFilter', pinned: 'left' },
			{ headerName: "Section", width: 100, field: "SectionName", filter: 'agTextColumnFilter', pinned: 'left' },

			{ headerName: "Batch", width: 100, field: "Batch", filter: 'agTextColumnFilter', },
			{ headerName: "Faculty", width: 100, field: "Faculty", filter: 'agTextColumnFilter', },
			{ headerName: "Level", width: 100, field: "Level", filter: 'agTextColumnFilter', },
			{ headerName: "Semester", width: 140, field: "Semester", filter: 'agTextColumnFilter', },
			{ headerName: "ClassYear", width: 140, field: "ClassYear", filter: 'agTextColumnFilter', },

			{ headerName: "Fee Heading", width: 180, field: "FeeItemName", filter: 'agTextColumnFilter', },
			{ headerName: "Previous Dues", width: 170, field: "Opening", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "Current Fee", width: 150, field: "DrTotal", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "Paid Amount", width: 130, field: "TotalCredit", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "Discount Amount", width: 140, field: "CrDiscountAmt", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "> Dues", width: 120, field: "FutureDues", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "Balance Amount", width: 150, field: "TotalDues", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },

			{ headerName: "FatherName", width: 190, field: "FatherName", filter: 'agTextColumnFilter', },
			{ headerName: "Father Contact No", width: 190, field: "F_ContactNo", filter: 'agTextColumnFilter', },
			{ headerName: "Mother Name", width: 180, field: "MotherName", filter: 'agTextColumnFilter', },
			{ headerName: "Mother Contact No", width: 180, field: "M_ContactNo", filter: 'agTextColumnFilter', },
			{ headerName: "Address", width: 160, field: "Address", filter: 'agTextColumnFilter', },

			{ headerName: "Is New", width: 120, field: "IsNewStudent", filter: 'agTextColumnFilter', },
			{ headerName: "Is Transport", width: 120, field: "IsTransport", filter: 'agTextColumnFilter', },
			{ headerName: "Is Hostel", width: 120, field: "IsHostel", filter: 'agTextColumnFilter', },

			{ headerName: "IsLeft", width: 120, field: "IsLeft", filter: 'agTextColumnFilter', },
			{ headerName: "LeftDate", width: 120, field: "LeftMiti", filter: 'agTextColumnFilter', },
			{ headerName: "LeftReason", width: 150, field: "LeftReason", filter: 'agTextColumnFilter', },
			{ headerName: "PointName", width: 180, field: "PointName", filter: 'agTextColumnFilter', },
			{ headerName: "RouteName", width: 180, field: "RouteName", filter: 'agTextColumnFilter', },
			{ headerName: "BoarderName", width: 180, field: "BoardersName", filter: 'agTextColumnFilter', },
			{ headerName: "CardNo", width: 130, field: "CardNo", filter: 'agNumberColumnFilter', },
			{ headerName: "EnrollNo", width: 130, field: "EnrollNo", filter: 'agNumberColumnFilter', },
			{ headerName: "LedgerPanaNo", width: 150, field: "LedgerPanaNo", filter: 'agTextColumnFilter', },

			{ headerName: "LastReceiptMiti", width: 150, field: "LastReceiptMiti", filter: 'agTextColumnFilter', },
			{ headerName: "LastReceiptNo", width: 150, field: "LastReceiptNo", filter: 'agTextColumnFilter', },
			{ headerName: "LastReceiptAmt", width: 150, field: "LastReceiptAmt", filter: 'agNumberColumnFilter', },

			{ headerName: "> DR", width: 110, field: "FutureDR", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "> CR", width: 110, field: "FutureCR", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },


			{ headerName: "Email", width: 100, field: "Email", filter: 'agTextColumnFilter', },

			{ headerName: "House Name", width: 150, field: "HouseName", filter: 'agTextColumnFilter', },
			{ headerName: "House Dress", width: 150, field: "HouseDress", filter: 'agTextColumnFilter', },
			{ headerName: "Vehicle Name", width: 150, field: "VehicleName", filter: 'agTextColumnFilter', },
			{ headerName: "Vehicle Number", width: 150, field: "VehicleNumber", filter: 'agTextColumnFilter', },
			{ headerName: "Gender", width: 150, field: "Gender", filter: 'agTextColumnFilter', },
		];

		$scope.gridOptions = {

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
			columnDefs: $scope.gridColumnDef,
			enableColResize: true,
			rowData: null,
			filter: true,
			enableFilter: true,
			enableSorting: true,
			overlayLoadingTemplate: "Please Click the Load Button to display the data",
			rowSelection: 'multiple',
			suppressHorizontalScroll: true,
			alignedGrids: [],
			onFilterChanged: function (e) {
				//console.log('onFilterChanged', e);
				var pDue = 0, cDue = 0, paidAmt = 0, disAmt = 0, balAmt = 0;
				$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
					var tb = node.data;
					pDue += tb.Opening;
					cDue += tb.DrTotal;
					paidAmt += tb.TotalCredit;
					disAmt += tb.CrDiscountAmt;
					balAmt += tb.TotalDues;
				});

				$scope.dataForBottomGrid[0].Opening = pDue;
				$scope.dataForBottomGrid[0].DrTotal = cDue;
				$scope.dataForBottomGrid[0].TotalCredit = paidAmt;
				$scope.dataForBottomGrid[0].CrDiscountAmt = disAmt;
				$scope.dataForBottomGrid[0].TotalDues = balAmt;
				$scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);
				//console.log('gridApi.getFilterModel() =>', e.api.getFilterModel());
			},
		};

		$scope.gridColumnDefDW = [
			{ headerName: "Id", width: 90, field: "AutoNumber", filter: 'agNumberColumnFilter', sortable: true, pinned: 'left' },
			{ headerName: "Reg.No.", width: 120, field: "RegdNo", filter: 'agTextColumnFilter', sortable: true, pinned: 'left' },
			{ headerName: "Roll No.", width: 115, field: "RollNo", filter: 'agNumberColumnFilter', },
			/*	{ headerName: "Name", width: 180, field: "Name", filter: 'agTextColumnFilter', },*/

			{
				headerName: "Name", field: "Name", filter: 'agTextColumnFilter', width: 180,
				cellStyle: function (params) {
					if (params.data.IsLeft == true) {
						return { color: 'red' };
					}
					else if (params.data.IsTransport == true)
						return { color: 'green' }
					else if (params.data.IsHostel == true)
						return { color: 'blue' }
					else {
						return null;
					}
				}, pinned: 'left'
			},

			{ headerName: "Class", width: 100, field: "ClassName", filter: 'agTextColumnFilter', pinned: 'left' },
			{ headerName: "Section", width: 100, field: "SectionName", filter: 'agTextColumnFilter', pinned: 'left' },

			{ headerName: "Batch", width: 100, field: "Batch", filter: 'agTextColumnFilter', },
			{ headerName: "Faculty", width: 100, field: "Faculty", filter: 'agTextColumnFilter', },
			{ headerName: "Level", width: 100, field: "Level", filter: 'agTextColumnFilter', },
			{ headerName: "Semester", width: 100, field: "Semester", filter: 'agTextColumnFilter', },
			{ headerName: "ClassYear", width: 100, field: "ClassYear", filter: 'agTextColumnFilter', },

			{ headerName: "Fee Heading", width: 130, field: "FeeItemName", filter: 'agTextColumnFilter', },
			{ headerName: "Previous Dues", width: 130, field: "Opening", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "Current Fee", width: 150, field: "DrAmt", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "Paid Amount", width: 130, field: "CrAmt", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "Discount Amount", width: 140, field: "CrDiscountAmt", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "Bill Discount", width: 140, field: "DrDiscountAmt", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			//{ headerName: "> Dues", width:  110, field: "FutureDues", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "Balance Amount", width: 140, field: "Closing", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },

			{ headerName: "FatherName", width: 150, field: "FatherName", filter: 'agTextColumnFilter', },
			{ headerName: "F_ContactNo", width: 120, field: "F_ContactNo", filter: 'agTextColumnFilter', },
			{ headerName: "Address", width: 160, field: "Address", filter: 'agTextColumnFilter', },

			//{ headerName: "Is New", width: 120, field: "IsNewStudent", filter: 'agTextColumnFilter', },
			//{ headerName: "Is Transport", width: 120, field: "IsTransport", filter: 'agTextColumnFilter', },
			//{ headerName: "Is Hostel", width: 120, field: "IsHostel", filter: 'agTextColumnFilter', },

			//{ headerName: "IsLeft", width: 120, field: "IsLeft", filter: 'agTextColumnFilter', },
			//{ headerName: "LeftDate", width: 120, field: "LeftMiti", filter: 'agTextColumnFilter', },
			//{ headerName: "LeftReason", width: 150, field: "LeftReason", filter: 'agTextColumnFilter', },
			//{ headerName: "PointName", width: 140, field: "PointName", filter: 'agTextColumnFilter', },
			//{ headerName: "RouteName", width: 140, field: "RouteName", filter: 'agTextColumnFilter', },
			//{ headerName: "BoarderName", width: 140, field: "BoardersName", filter: 'agTextColumnFilter', },
			//{ headerName: "CardNo", width: 100, field: "CardNo", filter: 'agNumberColumnFilter', },
			//{ headerName: "EnrollNo", width: 100, field: "EnrollNo", filter: 'agNumberColumnFilter', },
			//{ headerName: "LedgerPanaNo", width: 110, field: "LedgerPanaNo", filter: 'agTextColumnFilter', },

			//{ headerName: "LastReceiptMiti", width: 110, field: "LastReceiptMiti", filter: 'agTextColumnFilter', },
			//{ headerName: "LastReceiptNo", width: 110, field: "LastReceiptNo", filter: 'agTextColumnFilter', },
			//{ headerName: "LastReceiptAmt", width: 110, field: "LastReceiptAmt", filter: 'agNumberColumnFilter', },

			//{ headerName: "> DR", width: 110, field: "FutureDR", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			//{ headerName: "> CR", width: 110, field: "FutureCR", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },


			//{ headerName: "Email", width: 100, field: "Email", filter: 'agTextColumnFilter', },

			//{ headerName: "House Name", width: 120, field: "HouseName", filter: 'agTextColumnFilter', },
			//{ headerName: "House Dress", width: 120, field: "HouseDress", filter: 'agTextColumnFilter', },
			//{ headerName: "Vehicle Name", width: 120, field: "VehicleName", filter: 'agTextColumnFilter', },
			//{ headerName: "Vehicle Number", width: 120, field: "VehicleNumber", filter: 'agTextColumnFilter', },

		];

		$scope.gridOptionsDW = {

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
			columnDefs: $scope.gridColumnDefDW,
			enableColResize: true,
			rowData: null,
			filter: true,
			enableSorting: true,
			overlayLoadingTemplate: "Please Click the Load Button to display the data",
			rowSelection: 'multiple',
			suppressHorizontalScroll: true,
			alignedGrids: [],
			enableFilter: true,
			onFilterChanged: function (e) {
				var dt = {
					Name: 'TOTAL =>',
					Opening: 0,
					DrAmt: 0,
					CrAmt: 0,
					Closing: 0,
					DrDiscountAmt: 0,
					CrDiscountAmt: 0,

				}
				$scope.gridOptionsDW.api.forEachNodeAfterFilterAndSort(function (node) {
					var tb = node.data;
					dt.Opening += tb.Opening;
					dt.DrAmt += tb.DrAmt;
					dt.CrAmt += tb.CrAmt;
					dt.Closing += tb.Closing;
					dt.DrDiscountAmt += tb.DrDiscountAmt;
					dt.CrDiscountAmt += tb.CrDiscountAmt;
				});
				var filterDataColl = [];
				filterDataColl.push(dt);

				$scope.gridOptionsBottomDW.api.setRowData(filterDataColl);
				//$scope.dataForBottomGridDW[0].Opening = pDue;
				//$scope.dataForBottomGridDW[0].DrTotal = cDue;
				//$scope.dataForBottomGridDW[0].TotalCredit = paidAmt;
				//$scope.dataForBottomGridDW[0].CrDiscountAmt = disAmt;
				//$scope.dataForBottomGridDW[0].TotalDues = balAmt;
				//$scope.gridOptionsBottomDW.api.setRowData($scope.dataForBottomGrid);
				//console.log('gridApi.getFilterModel() =>', e.api.getFilterModel());
			},
		};
		$scope.dataForBottomGrid = [
			{
				RegdNo: '',
				RollNo: '',
				Name: 'Total =>',
				ClassName: '',
				SectionName: '',
				Opening: 0,
				DrTotal: 0,
				TotalCredit: 0,
				CrDiscountAmt: '',
				TotalDues: 0,
				FatherName: '',
				F_ContactNo: '',
				Address: '',
				IsLeft: '',
				TransportPoint: '',
				TransportRoute: '',
				BoarderName: '',
				CardNo: '',
				EnrollNo: '',
				LedgerPanaNo: '',


			},
		];

		$scope.gridOptionsBottom = {
			defaultColDef: {
				resizable: true,
				width: 90
			},
			rowHeight: 30,
			columnDefs: $scope.gridColumnDef,
			// we are hard coding the data here, it's just for demo purposes
			rowData: $scope.dataForBottomGrid,
			debug: true,
			rowClass: 'bold-row',
			// hide the header on the bottom grid
			headerHeight: 0,
			alignedGrids: []
		};


		$scope.dataForBottomGridDW = [
			{
				RegdNo: '',
				RollNo: '',
				Name: 'Total =>',
				ClassName: '',
				SectionName: '',
				Opening: 0,
				DrTotal: 0,
				TotalCredit: 0,
				CrDiscountAmt: '',
				TotalDues: 0,
				FatherName: '',
				F_ContactNo: '',
				Address: '',
				IsLeft: '',
				TransportPoint: '',
				TransportRoute: '',
				BoarderName: '',
				CardNo: '',
				EnrollNo: '',
				LedgerPanaNo: '',


			},
		];

		$scope.gridOptionsBottomDW = {
			defaultColDef: {
				resizable: true,
				width: 90
			},
			headerHeight: 31,
			rowHeight: 30,
			columnDefs: $scope.gridColumnDefDW,
			// we are hard coding the data here, it's just for demo purposes
			rowData: $scope.dataForBottomGridDW,
			debug: true,
			rowClass: 'bold-row',
			// hide the header on the bottom grid
			headerHeight: 0,
			alignedGrids: []
		};

		$scope.gridOptions.alignedGrids.push($scope.gridOptionsBottom);
		$scope.gridOptionsBottom.alignedGrids.push($scope.gridOptions);

		$scope.gridDivBottom = document.querySelector('#myGridBottom');
		new agGrid.Grid($scope.gridDivBottom, $scope.gridOptionsBottom);

		$scope.gridOptionsDW.alignedGrids.push($scope.gridOptionsBottomDW);
		$scope.gridOptionsBottomDW.alignedGrids.push($scope.gridOptionsDW);

		$scope.gridDivBottomDW = document.querySelector('#myGridBottomDW');
		new agGrid.Grid($scope.gridDivBottomDW, $scope.gridOptionsBottomDW);

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



		//Added By Suresh on Magh 22 for Sibling Fee Update

		$scope.gridColumnDefSib = [
			{ headerName: "Id", width: 90, field: "AutoNumber", filter: 'agNumberColumnFilter', sortable: true, pinned: 'left' },
			{ headerName: "Reg.No.", width: 120, field: "RegdNo", filter: 'agTextColumnFilter', sortable: true, pinned: 'left' },
			{ headerName: "Roll No.", width: 110, field: "RollNo", filter: 'agNumberColumnFilter', pinned: 'left' },
			/*	{ headerName: "Name", width: 180, field: "Name", filter: 'agTextColumnFilter', },*/

			{
				headerName: "Name", field: "Name", filter: 'agTextColumnFilter', width: 180,
				cellStyle: function (params) {
					if (params.data.IsLeft == true) {
						return { color: 'red' };
					}
					else if (params.data.IsTransport == true)
						return { color: 'green' }
					else if (params.data.IsHostel == true)
						return { color: 'blue' }
					else {
						return null;
					}
				}, pinned: 'left'
			},

			{ headerName: "Class", width: 120, field: "ClassName", filter: 'agTextColumnFilter', pinned: 'left' },
			{ headerName: "Section", width: 100, field: "SectionName", filter: 'agTextColumnFilter', pinned: 'left' },

			//{ headerName: "Batch", width: 100, field: "Batch", filter: 'agTextColumnFilter', },
			//{ headerName: "Faculty", width: 100, field: "Faculty", filter: 'agTextColumnFilter', },
			//{ headerName: "Level", width: 100, field: "Level", filter: 'agTextColumnFilter', },
			//{ headerName: "Semester", width: 150, field: "Semester", filter: 'agTextColumnFilter', },
			//{ headerName: "ClassYear", width: 150, field: "ClassYear", filter: 'agTextColumnFilter', },

			{ headerName: "Fee Heading", width: 140, field: "FeeItemName", filter: 'agTextColumnFilter', },
			{ headerName: "Previous Dues", width: 140, field: "Opening", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "Current Fee", width: 150, field: "DrTotal", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "Paid Amount", width: 130, field: "TotalCredit", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "Discount Amount", width: 140, field: "CrDiscountAmt", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "> Dues", width: 150, field: "FutureDues", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "Balance Amount", width: 140, field: "TotalDues", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },

			{ headerName: "FatherName", width: 210, field: "FatherName", filter: 'agTextColumnFilter', },
			{ headerName: "Father Contact No", width: 180, field: "F_ContactNo", filter: 'agTextColumnFilter', },
			{ headerName: "Mother Name", width: 180, field: "MotherName", filter: 'agTextColumnFilter', },
			{ headerName: "Mother Contact No", width: 180, field: "M_ContactNo", filter: 'agTextColumnFilter', },
			{ headerName: "Address", width: 160, field: "Address", filter: 'agTextColumnFilter', },

			{ headerName: "Is New", width: 150, field: "IsNewStudent", filter: 'agTextColumnFilter', },
			{ headerName: "Is Transport", width: 150, field: "IsTransport", filter: 'agTextColumnFilter', },
			{ headerName: "Is Hostel", width: 150, field: "IsHostel", filter: 'agTextColumnFilter', },

			{ headerName: "IsLeft", width: 150, field: "IsLeft", filter: 'agTextColumnFilter', },
			{ headerName: "LeftDate", width: 150, field: "LeftMiti", filter: 'agTextColumnFilter', },
			{ headerName: "LeftReason", width: 150, field: "LeftReason", filter: 'agTextColumnFilter', },
			{ headerName: "PointName", width: 200, field: "PointName", filter: 'agTextColumnFilter', },
			{ headerName: "RouteName", width: 200, field: "RouteName", filter: 'agTextColumnFilter', },
			{ headerName: "BoarderName", width: 200, field: "BoardersName", filter: 'agTextColumnFilter', },
			{ headerName: "CardNo", width: 150, field: "CardNo", filter: 'agNumberColumnFilter', },
			{ headerName: "EnrollNo", width: 150, field: "EnrollNo", filter: 'agNumberColumnFilter', },
			{ headerName: "LedgerPanaNo", width: 150, field: "LedgerPanaNo", filter: 'agTextColumnFilter', },

			{ headerName: "LastReceiptMiti", width: 150, field: "LastReceiptMiti", filter: 'agTextColumnFilter', },
			{ headerName: "LastReceiptNo", width: 150, field: "LastReceiptNo", filter: 'agTextColumnFilter', },
			{ headerName: "LastReceiptAmt", width: 150, field: "LastReceiptAmt", filter: 'agNumberColumnFilter', },

			{ headerName: "> DR", width: 110, field: "FutureDR", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
			{ headerName: "> CR", width: 110, field: "FutureCR", filter: 'agNumberColumnFilter', valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },


			{ headerName: "Email", width: 130, field: "Email", filter: 'agTextColumnFilter', },

			{ headerName: "House Name", width: 150, field: "HouseName", filter: 'agTextColumnFilter', },
			{ headerName: "House Dress", width: 150, field: "HouseDress", filter: 'agTextColumnFilter', },
			{ headerName: "Vehicle Name", width: 150, field: "VehicleName", filter: 'agTextColumnFilter', },
			{ headerName: "Vehicle Number", width: 150, field: "VehicleNumber", filter: 'agTextColumnFilter', },

		];

		$scope.gridOptionsSib = {

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
			columnDefs: $scope.gridColumnDefSib,
			enableColResize: true,
			rowData: null,
			filter: true,
			enableFilter: true,
			enableSorting: true,
			overlayLoadingTemplate: "Please Click the Load Button to display the data",
			rowSelection: 'multiple',
			suppressHorizontalScroll: true,
			alignedGrids: [],
			onFilterChanged: function () {

				var dt = {
					Name: 'TOTAL =>',
					FutureDR: 0,
					FutureCR: 0,
					Opening: 0,
					DrTotal: 0,
					TotalCredit: 0,
					CrDiscountAmt: 0,
					FutureDues: 0,
					TotalDues: 0,
					LastReceiptAmt: 0
				}
				$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
					var fData = node.data;
					dt.FutureDR += fData.FutureDR;
					dt.FutureCR += fData.FutureCR;
					dt.Opening += fData.Opening;
					dt.DrTotal += fData.DrTotal;
					dt.TotalCredit += fData.TotalCredit;
					dt.CrDiscountAmt += fData.CrDiscountAmt;
					dt.FutureDues += fData.FutureDues;
					dt.TotalDues += fData.TotalDues;
					dt.LastReceiptAmt += fData.LastReceiptAmt;
				});
				var filterDataColl = [];
				filterDataColl.push(dt);

				$scope.gridOptionsBottomSib.api.setRowData(filterDataColl);
			},
		};

		$scope.dataForBottomGridSib = [
			{
				RegdNo: '',
				RollNo: '',
				Name: 'Total =>',
				ClassName: '',
				SectionName: '',
				Opening: 0,
				DrTotal: 0,
				TotalCredit: 0,
				CrDiscountAmt: '',
				TotalDues: 0,
				FatherName: '',
				F_ContactNo: '',
				Address: '',
				IsLeft: '',
				TransportPoint: '',
				TransportRoute: '',
				BoarderName: '',
				CardNo: '',
				EnrollNo: '',
				LedgerPanaNo: '',


			},
		];

		$scope.gridOptionsBottomSib = {
			defaultColDef: {
				resizable: true,
				width: 90
			},
			rowHeight: 30,
			columnDefs: $scope.gridColumnDefSib,
			// we are hard coding the data here, it's just for demo purposes
			rowData: $scope.dataForBottomGridSib,
			debug: true,
			rowClass: 'bold-row',
			// hide the header on the bottom grid
			headerHeight: 0,
			alignedGrids: []
		};

		$scope.gridOptionsSib.alignedGrids.push($scope.gridOptionsBottomSib);
		$scope.gridOptionsBottomSib.alignedGrids.push($scope.gridOptionsSib);

		$scope.gridDivBottomSib = document.querySelector('#myGridBottomSib');
		new agGrid.Grid($scope.gridDivBottomSib, $scope.gridOptionsBottomSib);
		//Ends

	};

	$scope.LoadData = function () {

		$scope.PeriodColl = [{ id: 1, text: 'Month Wise' }, { id: 2, text: 'Date Wise' }];

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.entity = {
			FeeSummary: entityFeeSummarySMS

		};
		//$scope.MonthList = GlobalServices.getMonthList();
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

		$scope.newDiscountTypewise = {
			DiscountTypewiseId: null,

			Mode: 'Save'
		};

		$scope.ClassList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;



		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.feeMappingStudent = {
			ClassIdColl: '',
			FeeItemIdColl: '',
			For: 0
		}

		$scope.feeSummary = {
			FromMonthId: 0,
			ToMonthId: 0,
			ForStudent: 0,
			FeeItemIdColl: '',
			PeriodAs: 1,
			CombinedSummary: false,
		}

		$scope.SibfeeSummary = {
			FromMonthId: 0,
			ToMonthId: 0,
			ForStudent: 0,
			FeeItemIdColl: '',
			PeriodAs: 1,
			CombinedSummary: false,
		}

		$scope.feeSummaryDW = {
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
		}



		$scope.classWise = {
			SelectedFromMonth: null,
			SelectedToMonth: null,
			FromMonthId: 0,
			ToMonthId: 0,
			ForStudent: 0,
			FromMonth: '',
			ToMonth: '',
			TemplatesColl: []
		};

		$scope.studentWise = {
			SelectClass: null,
			SelectedFromMonth: null,
			SelectedToMonth: null,
			FromMonthId: 0,
			ToMonthId: 0,
			ForStudent: 0,
			FromMonth: '',
			ToMonth: '',
			TemplatesColl: []
		};

		$scope.FeeItemList = [];
		GlobalServices.getFeeItemList().then(function (res1) {
			$scope.FeeItemList = res1.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityFeeSummaryClassWise + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.classWise.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityFeeSummaryStudentWise + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.studentWise.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.LoadTemplates();

		//$scope.GetAllDiscountTypewiseList();
		$scope.IsInit = true;
		$scope.AllEmployeeColl = [];
		$timeout(function () {
			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetAllEmpShortList",
				dataType: "json"
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.AllEmployeeColl = res.data.Data;
					$scope.IsInit = false;
				}


			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}, 100);
	};

	$scope.LoadTemplates = function () {

		$scope.NotificationTemplates = [];
		var para1 = {
			EntityId: entityFeeSummarySMS,
			ForATS: 3,
			TemplateType: 3
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(para1)
		}).then(function (res) {
			if (res.data.IsSuccess) {
				$scope.NotificationTemplates = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.ExportFeeSummaryAsCSV = function () {
		var params = {
			fileName: 'feeSummary.csv',
			sheetName: 'feeSummary'
		};
		$scope.gridOptions.api.exportDataAsCsv(params);
	}
	$scope.ExportFeeSummaryAsexcel = function () {
		var params = {
			fileName: 'feeSummary.csv',
			sheetName: 'feeSummary'
		};
		$scope.gridOptions.api.exportDataAsCsv(params);
	}

	//Added By Suresh on 22 Magh starts
	$scope.ExportFeeSummarySibAsCSV = function () {
		var params = {
			fileName: 'siblingFeeSummary.csv',
			sheetName: 'siblingFeeSummary'
		};
		$scope.gridOptionsSib.api.exportDataAsCsv(params);
	}
	$scope.ExportFeeSummarySibAsexcel = function () {
		var params = {
			fileName: 'SiblingFeeSummary.csv',
			sheetName: 'SiblingFeeSummary'
		};
		$scope.gridOptionsSib.api.exportDataAsCsv(params);
	}
	//Ends

	$scope.ChangeClassOfFeeSummary = function () {
		if (!$scope.feeSummary.SelectedClass || $scope.feeSummary.SelectedClass.ClassId == 0) {
			GlobalServices.getAcademicMonthList(null, null).then(function (resAM) {
				$scope.MonthList = [];
				angular.forEach(resAM.data.Data, function (m) {
					$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
				});
			});
		} else if ($scope.feeSummary.SelectedClass) {
			GlobalServices.getAcademicMonthList(null, $scope.feeSummary.SelectedClass.ClassId).then(function (resAM) {
				$scope.MonthList = [];
				angular.forEach(resAM.data.Data, function (m) {
					$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
				});
			});
		}

		$scope.getFeeSummary();
	}

	$scope.getFeeSummary = function () {

		$scope.SelectedClassSemesterList = [];
		$scope.SelectedClassClassYearList = [];

		if ($scope.feeSummary.SelectedClass) {
			var findClass = $scope.AllClassList.firstOrDefault(p1 => p1.ClassId == $scope.feeSummary.SelectedClass.ClassId);
			if (findClass) {

				$scope.feeSummary.SelectedClass.ClassType = findClass.ClassType;

				var semQry = mx(findClass.ClassSemesterIdColl);
				var cyQry = mx(findClass.ClassYearIdColl);

				$scope.SelectedClassClassYearList = [];
				$scope.SelectedClassSemesterList = [];

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
		}

		if ($scope.feeSummary.PeriodAs == 1) {
			$scope.feeSummary.DateFrom_TMP = null;
			$scope.feeSummary.DateTo_TMP = null;
			if ($scope.feeSummary.FromMonthId > 0 && $scope.feeSummary.ToMonthId > 0) {

			} else {
				//Swal.fire('Please ! Select Month Period');
				return;
			}
		} else if ($scope.feeSummary.PeriodAs == 2) {
			if (!$scope.feeSummary.DateFromDet || !$scope.feeSummary.DateToDet) {
				//Swal.fire('Please ! Select DateFrom/To');
				return;
			}
		} else {
			return;
		}

		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			fromMonthId: ($scope.feeSummary.PeriodAs == 1 ? $scope.feeSummary.FromMonthId : 0),
			toMonthId: ($scope.feeSummary.PeriodAs == 1 ? $scope.feeSummary.ToMonthId : 0),
			forStudent: $scope.feeSummary.ForStudent,
			feeItemIdColl: $scope.feeSummary.FeeItemIdColl,
			classId: $scope.feeSummary.SelectedClass ? $scope.feeSummary.SelectedClass.ClassId : 0,
			sectionId: $scope.feeSummary.SelectedClass ? $scope.feeSummary.SelectedClass.SectionId : 0,
			batchId: $scope.feeSummary.BatchId,
			semesterId: $scope.feeSummary.SemesterId,
			classYearId: $scope.feeSummary.ClassYearId,
			ForPaymentFollowup: false,
			FollowupType: 0,
			dateFrom: ($scope.feeSummary.PeriodAs == 2 ? $filter('date')(new Date($scope.feeSummary.DateFromDet.dateAD), 'yyyy-MM-dd') : null),
			dateTo: ($scope.feeSummary.PeriodAs == 2 ? $filter('date')(new Date($scope.feeSummary.DateToDet.dateAD), 'yyyy-MM-dd') : null),
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Report/GetFeeSummary",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				var Opening = 0, DrTotal = 0, TotalCredit = 0, CrDiscountAmt = 0, TotalDues = 0;
				angular.forEach(res.data.Data, function (fs) {
					Opening += fs.Opening;
					DrTotal += fs.DrTotal;
					TotalCredit += fs.TotalCredit;
					CrDiscountAmt += fs.CrDiscountAmt;
					TotalDues += fs.TotalDues;
				});
				$scope.dataForBottomGrid[0].Opening = Opening;
				$scope.dataForBottomGrid[0].DrTotal = DrTotal;
				$scope.dataForBottomGrid[0].TotalCredit = TotalCredit;
				$scope.dataForBottomGrid[0].CrDiscountAmt = CrDiscountAmt;
				$scope.dataForBottomGrid[0].TotalDues = TotalDues;
				$scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);

				if ($scope.feeSummary.CombinedSummary == true) {

					var DataColl = mx(res.data.Data);
					var newDataColl = [];
					var groupStudent = DataColl.groupBy(p1 => p1.StudentId);
					angular.forEach(groupStudent, function (st) {
						var fst = st.elements[0];
						var groupData = mx(st.elements);
						var newData = angular.copy(fst);
						if (st.elements.length > 1) {
							newData.Opening = groupData.sum(p1 => p1.Opening);
							newData.DrAmt = groupData.sum(p1 => p1.DrAmt);
							newData.DrDiscountAmt = groupData.sum(p1 => p1.DrDiscountAmt);
							newData.DrFineAmt = groupData.sum(p1 => p1.DrFineAmt);
							newData.DrTax = groupData.sum(p1 => p1.DrTax);
							newData.DrTotal = groupData.sum(p1 => p1.DrTotal);
							newData.CrAmt = groupData.sum(p1 => p1.CrAmt);
							newData.CrDiscountAmt = groupData.sum(p1 => p1.CrDiscountAmt);
							newData.CrFineAmt = groupData.sum(p1 => p1.CrFineAmt);
							newData.TotalDebit = groupData.sum(p1 => p1.TotalDebit);
							newData.TotalCredit = groupData.sum(p1 => p1.TotalCredit);
							newData.TotalDues = groupData.sum(p1 => p1.TotalDues);
							newData.FutureDR = groupData.sum(p1 => p1.FutureDR);
							newData.FutureCR = groupData.sum(p1 => p1.FutureCR);
							newData.FutureDues = groupData.sum(p1 => p1.FutureDues);
							newData.Closing = groupData.sum(p1 => p1.Closing);
						}
						newDataColl.push(newData);
					});

					$scope.gridOptions.api.setRowData(newDataColl);
				}
				else {
					$scope.gridOptions.api.setRowData(res.data.Data);
				}


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.PrintFeeSummary = function () {

		var fromMonthText = '';
		var toMonthText = '';
		if ($scope.MonthList && $scope.feeSummary) {
			var findM = mx($scope.MonthList).firstOrDefault(p1 => p1.id == $scope.feeSummary.FromMonthId);
			if (findM && findM.text)
				fromMonthText = findM.text;

			findM = mx($scope.MonthList).firstOrDefault(p1 => p1.id == $scope.feeSummary.ToMonthId);
			if (findM && findM.text)
				toMonthText = findM.text;
		}


		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityFeeSummary + "&voucherId=0&isTran=false",
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
											$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
												dataColl.push(node.data);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Fee/Report/PrintFeeSummary",
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
														entityid: entityFeeSummary,
														voucherid: 0,
														tranid: 0,
														vouchertype: 0,
														sessionid: res.data.Data.ResponseId,
														ClassSectionName: ($scope.feeSummary.SelectedClass == 0 || $scope.feeSummary.SelectedClass == null || $scope.feeSummary.SelectedClass == undefined ? "All" : $scope.feeSummary.SelectedClass.text),
														FromMonth: fromMonthText,
														ToMonth: toMonthText,
														ForStudent: "",
														FeeItemName: ($scope.feeSummary.FeeItemIdColl == 0 || $scope.feeSummary.FeeItemIdColl == null || $scope.feeSummary.FeeItemIdColl == undefined ? "All" : mx($scope.FeeItemList).firstOrDefault(p1 => p1.FeeItemId == $scope.feeSummary.FeeItemIdColl).Name)
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
						$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
							dataColl.push(node.data);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Fee/Report/PrintFeeSummary",
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
									entityid: entityFeeSummary,
									voucherid: 0,
									tranid: 0,
									vouchertype: 0,
									sessionid: res.data.Data.ResponseId,
									ClassSectionName: ($scope.feeSummary.SelectedClass == 0 || $scope.feeSummary.SelectedClass == null || $scope.feeSummary.SelectedClass == undefined ? "All" : $scope.feeSummary.SelectedClass.text),
									FromMonth: fromMonthText,
									ToMonth: toMonthText,
									ForStudent: "",
									FeeItemName: ($scope.feeSummary.FeeItemIdColl == 0 || $scope.feeSummary.FeeItemIdColl == null || $scope.feeSummary.FeeItemIdColl == undefined ? "All" : mx($scope.FeeItemList).firstOrDefault(p1 => p1.FeeItemId == $scope.feeSummary.FeeItemIdColl).Name)
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

	$scope.PrintFeeSummarySFS = function () {

		var fromMonthText = '';
		var toMonthText = '';
		if ($scope.MonthList && $scope.SibfeeSummary) {
			var findM = mx($scope.MonthList).firstOrDefault(p1 => p1.id == $scope.SibfeeSummary.FromMonthId);
			if (findM && findM.text)
				fromMonthText = findM.text;

			findM = mx($scope.MonthList).firstOrDefault(p1 => p1.id == $scope.SibfeeSummary.ToMonthId);
			if (findM && findM.text)
				toMonthText = findM.text;
		}


		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityFeeSummary + "&voucherId=0&isTran=false",
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
											$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
												dataColl.push(node.data);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Fee/Report/PrintFeeSummary",
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
														entityid: entityFeeSummary,
														voucherid: 0,
														tranid: 0,
														vouchertype: 0,
														sessionid: res.data.Data.ResponseId,
														ClassSectionName: ($scope.SibfeeSummary.SelectedClass == 0 || $scope.SibfeeSummary.SelectedClass == null || $scope.SibfeeSummary.SelectedClass == undefined ? "All" : $scope.SibfeeSummary.SelectedClass.text),
														FromMonth: fromMonthText,
														ToMonth: toMonthText,
														ForStudent: "",
														FeeItemName: ($scope.SibfeeSummary.FeeItemIdColl == 0 || $scope.SibfeeSummary.FeeItemIdColl == null || $scope.SibfeeSummary.FeeItemIdColl == undefined ? "All" : mx($scope.FeeItemList).firstOrDefault(p1 => p1.FeeItemId == $scope.SibfeeSummary.FeeItemIdColl).Name)
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
						$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
							dataColl.push(node.data);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Fee/Report/PrintFeeSummary",
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
									entityid: entityFeeSummary,
									voucherid: 0,
									tranid: 0,
									vouchertype: 0,
									sessionid: res.data.Data.ResponseId,
									ClassSectionName: ($scope.SibfeeSummary.SelectedClass == 0 || $scope.SibfeeSummary.SelectedClass == null || $scope.SibfeeSummary.SelectedClass == undefined ? "All" : $scope.SibfeeSummary.SelectedClass.text),
									FromMonth: fromMonthText,
									ToMonth: toMonthText,
									ForStudent: "",
									FeeItemName: ($scope.SibfeeSummary.FeeItemIdColl == 0 || $scope.SibfeeSummary.FeeItemIdColl == null || $scope.SibfeeSummary.FeeItemIdColl == undefined ? "All" : mx($scope.FeeItemList).firstOrDefault(p1 => p1.FeeItemId == $scope.SibfeeSummary.FeeItemIdColl).Name)
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


	$scope.getFeeSummaryDW = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			feeItemIdColl: $scope.feeSummaryDW.FeeItemIdColl,
			dateFrom: $filter('date')(new Date($scope.feeSummaryDW.DateFromDet.dateAD), 'yyyy-MM-dd'),
			dateTo: $filter('date')(new Date($scope.feeSummaryDW.DateToDet.dateAD), 'yyyy-MM-dd'),
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Report/GetFeeDateWise",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {


				var Opening = 0, DrAmt = 0, CrAmt = 0, Closing = 0, DrDiscountAmt = 0, CrDiscountAmt = 0;
				res.data.Data.forEach(function (tb) {
					Opening += tb.Opening;
					DrAmt += tb.DrAmt;
					CrAmt += tb.CrAmt;
					Closing += tb.Closing;
					DrDiscountAmt += tb.DrDiscountAmt;
					CrDiscountAmt += tb.CrDiscountAmt;
				});

				$scope.dataForBottomGridDW[0].Opening = Opening;
				$scope.dataForBottomGridDW[0].DrAmt = DrAmt;
				$scope.dataForBottomGridDW[0].CrAmt = CrAmt;
				$scope.dataForBottomGridDW[0].Closing = Closing;
				$scope.dataForBottomGridDW[0].DrDiscountAmt = DrDiscountAmt;
				$scope.dataForBottomGridDW[0].CrDiscountAmt = CrDiscountAmt;

				$scope.gridOptionsBottomDW.api.setRowData($scope.dataForBottomGridDW);

				$scope.gridOptionsDW.api.setRowData(res.data.Data);
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.PrintFeeSummaryDW = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityFeeSummary + "&voucherId=0&isTran=false",
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
											$scope.gridOptionsDW.api.forEachNodeAfterFilterAndSort(function (node) {
												dataColl.push(node.data);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Fee/Report/PrintFeeSummary",
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
														entityid: entityFeeSummary,
														voucherid: 0,
														tranid: 0,
														vouchertype: 0,
														sessionid: res.data.Data.ResponseId,
														ClassSectionName: ($scope.feeSummaryDW.SelectedClass == 0 || $scope.feeSummaryDW.SelectedClass == null || $scope.feeSummaryDW.SelectedClass == undefined ? "All" : $scope.feeSummaryDW.SelectedClass.text),
														FromMonth: '',
														ToMonth: '',
														ForStudent: "",
														FeeItemName: ($scope.feeSummaryDW.FeeItemIdColl == 0 || $scope.feeSummaryDW.FeeItemIdColl == null || $scope.feeSummaryDW.FeeItemIdColl == undefined ? "All" : mx($scope.FeeItemList).firstOrDefault(p1 => p1.FeeItemId == $scope.feeSummaryDW.FeeItemIdColl).Name),
														Period: ($scope.feeSummaryDW.DateFromDet ? $scope.feeSummaryDW.DateFromDet.dateBS : '') + " TO " + ($scope.feeSummaryDW.DateToDet ? $scope.feeSummaryDW.DateToDet.dateBS : ''),
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
						$scope.gridOptionsDW.api.forEachNodeAfterFilterAndSort(function (node) {
							dataColl.push(node.data);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Fee/Report/PrintFeeSummary",
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
									entityid: entityFeeSummary,
									voucherid: 0,
									tranid: 0,
									vouchertype: 0,
									sessionid: res.data.Data.ResponseId,
									ClassSectionName: ($scope.feeSummaryDW.SelectedClass == 0 || $scope.feeSummaryDW.SelectedClass == null || $scope.feeSummaryDW.SelectedClass == undefined ? "All" : $scope.feeSummaryDW.SelectedClass.text),
									FromMonth: '',
									ToMonth: '',
									ForStudent: "",
									FeeItemName: ($scope.feeSummaryDW.FeeItemIdColl == 0 || $scope.feeSummaryDW.FeeItemIdColl == null || $scope.feeSummaryDW.FeeItemIdColl == undefined ? "All" : mx($scope.FeeItemList).firstOrDefault(p1 => p1.FeeItemId == $scope.feeSummaryDW.FeeItemIdColl).Name),
									Period: ($scope.feeSummaryDW.DateFromDet ? $scope.feeSummaryDW.DateFromDet.dateBS : '') + " TO " + ($scope.feeSummaryDW.DateToDet ? $scope.feeSummaryDW.DateToDet.dateBS : ''),
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

	$scope.getFeeSummaryClassWise = function () {
		if ($scope.classWise.SelectedFromMonth && $scope.classWise.SelectedToMonth && $scope.classWise.RptTranId) {

			var EntityId = entityFeeSummaryClassWise;

			var rptPara = {
				fromMonthId: $scope.classWise.SelectedFromMonth.id,
				toMonthId: $scope.classWise.SelectedToMonth.id,
				fromMonth: $scope.classWise.SelectedFromMonth.text,
				toMonth: $scope.classWise.SelectedToMonth.text,
				forStudent: $scope.classWise.ForStudent,
				rptTranId: $scope.classWise.RptTranId
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptClassWise").src = '';
			document.getElementById("frmRptClassWise").style.width = '100%';
			document.getElementById("frmRptClassWise").style.height = '1300px';
			document.getElementById("frmRptClassWise").style.visibility = 'visible';
			document.getElementById("frmRptClassWise").src = base_url + "Fee/Report/RptFeeSummaryClassWise?" + paraQuery;

		}
	};

	$scope.getFeeSummaryStudentWise = function () {
		if ($scope.studentWise.SelectedClass && $scope.studentWise.SelectedFromMonth && $scope.studentWise.SelectedToMonth && $scope.studentWise.RptTranId) {

			var EntityId = entityFeeSummaryStudentWise;

			var rptPara = {
				classId: $scope.studentWise.SelectedClass.ClassId,
				sectionId: $scope.studentWise.SelectedClass.SectionId ? $scope.studentWise.SelectedClass.SectionId : 0,
				classSec: $scope.studentWise.SelectedClass.text,
				fromMonthId: $scope.studentWise.SelectedFromMonth.id,
				toMonthId: $scope.studentWise.SelectedToMonth.id,
				fromMonth: $scope.studentWise.SelectedFromMonth.text,
				toMonth: $scope.studentWise.SelectedToMonth.text,
				forStudent: $scope.studentWise.ForStudent,
				rptTranId: $scope.studentWise.RptTranId
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptStudentWise").src = '';
			document.getElementById("frmRptStudentWise").style.width = '100%';
			document.getElementById("frmRptStudentWise").style.height = '1300px';
			document.getElementById("frmRptStudentWise").style.visibility = 'visible';
			document.getElementById("frmRptStudentWise").src = base_url + "Fee/Report/RptFeeSummaryStudentWise?" + paraQuery;

		}
	};

	//$scope.SendSMS = function () {
	//	Swal.fire({
	//		title: 'Do you want to Send SMS To the filter data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {
	//			var para1 = {
	//				EntityId: entityFeeSummarySMS,
	//				ForATS: 3,
	//				TemplateType: 1
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Setup/Security/GetSENT",
	//				dataType: "json",
	//				data: JSON.stringify(para1)
	//			}).then(function (res) {
	//				if (res.data.IsSuccess && res.data.Data) {
	//					var templatesColl = res.data.Data;
	//					if (templatesColl && templatesColl.length > 0) {
	//						var templatesName = [];
	//						var sno = 1;
	//						angular.forEach(templatesColl, function (tc) {
	//							templatesName.push(sno + '-' + tc.Name);
	//							sno++;
	//						});

	//						var print = false;

	//						var rptTranId = 0;
	//						var selectedTemplate = null;
	//						if (templatesColl.length == 1) {
	//							rptTranId = templatesColl[0].TranId;
	//							selectedTemplate = templatesColl[0];
	//						}
	//						else {
	//							Swal.fire({
	//								title: 'Templates For SMS',
	//								input: 'select',
	//								inputOptions: templatesName,
	//								inputPlaceholder: 'Select a template',
	//								showCancelButton: true,
	//								inputValidator: (value) => {
	//									return new Promise((resolve) => {
	//										if (value >= 0) {
	//											resolve()
	//											rptTranId = templatesColl[value].TranId;
	//											selectedTemplate = templatesColl[value];

	//											if (rptTranId > 0) {
	//												var dataColl = [];
	//												$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
	//													var objEntity = node.data;
	//													var msg = selectedTemplate.Description;
	//													for (let x in objEntity) {
	//														var variable = '$$' + x.toLowerCase() + '$$';
	//														if (msg.indexOf(variable) >= 0) {
	//															var val = objEntity[x];
	//															msg = msg.replace(variable, val);
	//														}

	//														if (msg.indexOf('$$') == -1)
	//															break;
	//													}

	//													var newSMS = {
	//														EntityId: entityFeeSummarySMS,
	//														StudentId: objEntity.StudentId,
	//														UserId: objEntity.UserId,
	//														Title: selectedTemplate.Title,
	//														Message: msg,
	//														ContactNo: objEntity.F_ContactNo,
	//														StudentName: objEntity.Name
	//													};

	//													dataColl.push(newSMS);
	//												});

	//												print = true;

	//												$http({
	//													method: 'POST',
	//													url: base_url + "Global/SendSMSToStudent",
	//													dataType: "json",
	//													data: JSON.stringify(dataColl)
	//												}).then(function (sRes) {
	//													Swal.fire(sRes.data.ResponseMSG);
	//													if (sRes.data.IsSuccess && sRes.data.Data) {

	//													}
	//												});

	//											}

	//										} else {
	//											resolve('You need to select:)')
	//										}
	//									})
	//								}
	//							})
	//						}

	//						if (rptTranId > 0 && print == false) {
	//							var dataColl = [];

	//							$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
	//								var objEntity = node.data;
	//								var msg = selectedTemplate.Description;
	//								for (let x in objEntity) {
	//									var variable = '$$' + x.toLowerCase() + '$$';
	//									if (msg.indexOf(variable) >= 0) {
	//										var val = objEntity[x];
	//										msg = msg.replace(variable, val);
	//									}

	//									if (msg.indexOf('$$') == -1)
	//										break;
	//								}

	//								var newSMS = {
	//									EntityId: entityFeeSummarySMS,
	//									StudentId: objEntity.StudentId,
	//									UserId: objEntity.UserId,
	//									Title: selectedTemplate.Title,
	//									Message: msg,
	//									ContactNo: objEntity.F_ContactNo,
	//									StudentName: objEntity.Name
	//								};

	//								dataColl.push(newSMS);
	//							});
	//							print = true;

	//							$http({
	//								method: 'POST',
	//								url: base_url + "Global/SendSMSToStudent",
	//								dataType: "json",
	//								data: JSON.stringify(dataColl)
	//							}).then(function (sRes) {
	//								Swal.fire(sRes.data.ResponseMSG);
	//								if (sRes.data.IsSuccess && sRes.data.Data) {

	//								}
	//							});

	//						}

	//					} else
	//						Swal.fire('No Templates found for SMS');
	//				}
	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	});


	//};

	// Updated SendSMS function for filtered data
	$scope.SendSMS = function () {
		var filteredDataCount = 0;
		$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
			filteredDataCount++;
		});

		if (filteredDataCount === 0) {
			Swal.fire('No data found after filter to send SMS');
			return;
		}

		$scope.filteredSMSStudentsCount = filteredDataCount;
		$scope.newSMSFiltered = { Description: '', Title: '' };
		$scope.selectedSMSTemplateFiltered = null;
		$scope.smsTemplatesCollFiltered = [];

		$scope.sendToFiltered = {
			StudentContact: false,
			FatherContact: true,
			MotherContact: false,
			GuardianContact: false
		};

		var para1 = {
			EntityId: entityFeeSummarySMS,
			ForATS: 3,
			TemplateType: 1
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(para1)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.smsTemplatesCollFiltered = res.data.Data;

				if ($scope.smsTemplatesCollFiltered.length === 1) {
					$scope.selectedSMSTemplateFiltered = $scope.smsTemplatesCollFiltered[0];
					$scope.OnSMSTemplateChangeFiltered();

					let recipients = $scope.selectedSMSTemplateFiltered.Recipients || '';
					$scope.sendToFiltered.StudentContact = recipients.includes('$$contactno$$');
					$scope.sendToFiltered.FatherContact = recipients.includes('$$f_contactno$$');
					$scope.sendToFiltered.MotherContact = recipients.includes('$$m_contactno$$');
					$scope.sendToFiltered.GuardianContact = recipients.includes('$$g_contacno$$');
				}

				// Open modal
				$('#modal-sms-filtered').modal('show');
			} else {
				Swal.fire('No templates found for SMS');
			}
		}, function (err) {
			Swal.fire('Failed to load templates');
		});
	};

	// Template change handler for filtered SMS
	$scope.OnSMSTemplateChangeFiltered = function () {
		if ($scope.selectedSMSTemplateFiltered) {
			$scope.newSMSFiltered.Description = $scope.selectedSMSTemplateFiltered.Description || '';
			$scope.newSMSFiltered.Title = $scope.selectedSMSTemplateFiltered.Title || '';

			var recipients = $scope.selectedSMSTemplateFiltered.Recipients || '';

			$scope.sendToFiltered.StudentContact = recipients.includes('$$contactno$$');
			$scope.sendToFiltered.FatherContact = recipients.includes('$$f_contactno$$');
			$scope.sendToFiltered.MotherContact = recipients.includes('$$m_contactno$$');
			$scope.sendToFiltered.GuardianContact = recipients.includes('$$g_contacno$$');
		} else {
			$scope.newSMSFiltered.Description = '';
			$scope.newSMSFiltered.Title = '';

			$scope.sendToFiltered.StudentContact = false;
			$scope.sendToFiltered.FatherContact = true;
			$scope.sendToFiltered.MotherContact = false;
			$scope.sendToFiltered.GuardianContact = false;
		}
	};

	$scope.SendSMSToFilteredStudents = function () {
		if (!$scope.newSMSFiltered.Description || $scope.newSMSFiltered.Description.trim() === '') {
			Swal.fire('Please enter SMS description');
			return;
		}
		if (!($scope.sendToFiltered.StudentContact ||
			$scope.sendToFiltered.FatherContact ||
			$scope.sendToFiltered.MotherContact ||
			$scope.sendToFiltered.GuardianContact)) {
			Swal.fire('Please select at least one contact type to send SMS');
			return;
		}

		var dataColl = [];
		$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
			var objEntity = node.data;

			var contacts = [];
			if ($scope.sendToFiltered.StudentContact && objEntity.ContactNo)
				contacts.push(objEntity.ContactNo);
			if ($scope.sendToFiltered.FatherContact && objEntity.F_ContactNo)
				contacts.push(objEntity.F_ContactNo);
			if ($scope.sendToFiltered.MotherContact && objEntity.M_ContactNo)
				contacts.push(objEntity.M_ContactNo);
			if ($scope.sendToFiltered.GuardianContact && objEntity.G_ContactNo)
				contacts.push(objEntity.G_ContactNo);

			if (contacts.length > 0) {
				dataColl.push({
					studentId: objEntity.StudentId,
					name: objEntity.Name,
					contacts: contacts
				});
			}
		});

		if (dataColl.length === 0) {
			Swal.fire('No students with valid contacts found');
			return;
		}

		Swal.fire({
			title: 'Confirm Send',
			text: `Do you want to send this SMS to ${dataColl.length} filtered students?`,
			icon: 'question',
			showCancelButton: true,
			confirmButtonText: 'Send',
			cancelButtonText: 'Cancel'
		}).then((result) => {
			if (!result.isConfirmed) return;

			// Prepare final data for API
			var finalDataColl = [];

			$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
				var objEntity = node.data;

				var contacts = [];
				if ($scope.sendToFiltered.StudentContact && objEntity.ContactNo)
					contacts.push(objEntity.ContactNo);
				if ($scope.sendToFiltered.FatherContact && objEntity.F_ContactNo)
					contacts.push(objEntity.F_ContactNo);
				if ($scope.sendToFiltered.MotherContact && objEntity.M_ContactNo)
					contacts.push(objEntity.M_ContactNo);
				if ($scope.sendToFiltered.GuardianContact && objEntity.G_ContactNo)
					contacts.push(objEntity.G_ContactNo);

				angular.forEach(contacts, function (tmpContactNo) {
					var msg = $scope.newSMSFiltered.Description;

					//var msg = $scope.newSMS.Description;

					for (let x in objEntity) {
						var variable = '$$' + x.toLowerCase() + '$$';
						if (msg.indexOf(variable) >= 0) {
							var val = objEntity[x] || '';
							//msg = msg.replace(new RegExp(variable, 'gi'), val);
							msg = msg.replace(variable, val || '');
						}
						if (msg.indexOf('$$') === -1) break;
					}

					finalDataColl.push({
						EntityId: entityFeeSummarySMS,
						StudentId: objEntity.StudentId,
						UserId: objEntity.UserId,
						Title: $scope.newSMSFiltered.Title || '',
						Message: msg,
						ContactNo: tmpContactNo,
						StudentName: objEntity.Name
					});
				});
			});

			// Send SMS
			$http({
				method: 'POST',
				url: base_url + "Global/SendSMSToStudent",
				dataType: "json",
				data: JSON.stringify(finalDataColl)
			}).then(function (sRes) {
				Swal.fire(sRes.data.ResponseMSG || 'SMS sent successfully');
				if (sRes.data.IsSuccess) {
					$('#modal-sms-filtered').modal('hide');
					// Reset form
					$scope.ResetSMSFiltered();
				}
			}, function () {
				Swal.fire('Failed to send SMS');
			});
		});
	};

	// Reset function
	$scope.ResetSMSFiltered = function () {
		$scope.newSMSFiltered = { Description: '', Title: '' };
		$scope.selectedSMSTemplateFiltered = null;
		$scope.sendToFiltered = {
			StudentContact: false,
			FatherContact: true,
			MotherContact: false,
			GuardianContact: false
		};
	};


	$scope.SendSMSFromMobile = function () {
		var filteredDataCount = 0;
		$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
			filteredDataCount++;
		});

		if (filteredDataCount === 0) {
			Swal.fire('No data found after filter to send SMS from mobile');
			return;
		}

		$scope.filteredMobileSMSStudentsCount = filteredDataCount;

		$scope.newMobileSMS = { Description: '' };
		$scope.selectedMobileSMSTemplate = null;
		$scope.mobileSmsTemplatesColl = [];

		$scope.sendToMobile = {
			StudentContact: false,
			FatherContact: true,
			MotherContact: false,
			GuardianContact: false
		};

		var para1 = {
			EntityId: entityFeeSummarySMS,
			ForATS: 3,
			TemplateType: 1
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(para1)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.mobileSmsTemplatesColl = res.data.Data;

				if ($scope.mobileSmsTemplatesColl.length === 1) {
					$scope.selectedMobileSMSTemplate = $scope.mobileSmsTemplatesColl[0];
					$scope.OnMobileSMSTemplateChange();
				}

				$('#modal-sms-mobile').modal('show');
			} else {
				Swal.fire('No templates found for mobile SMS');
			}
		}, function () {
			Swal.fire('Failed to load templates');
		});
	};

	$scope.OnMobileSMSTemplateChange = function () {
		if ($scope.selectedMobileSMSTemplate) {
			$scope.newMobileSMS.Description = $scope.selectedMobileSMSTemplate.Description;
			$scope.newMobileSMS.Title = $scope.selectedMobileSMSTemplate.Title;

			var recipients = $scope.selectedMobileSMSTemplate.Recipients || '';
			$scope.sendToMobile.StudentContact = recipients.includes('$$contactno$$');
			$scope.sendToMobile.FatherContact = recipients.includes('$$f_contactno$$');
			$scope.sendToMobile.MotherContact = recipients.includes('$$m_contactno$$');
			$scope.sendToMobile.GuardianContact = recipients.includes('$$g_contacno$$');
		} else {
			$scope.newMobileSMS.Description = '';
			$scope.newMobileSMS.Title = '';
			$scope.sendToMobile.StudentContact = false;
			$scope.sendToMobile.FatherContact = true;
			$scope.sendToMobile.MotherContact = false;
			$scope.sendToMobile.GuardianContact = false;
		}
	};

	$scope.SendMobileSMSToFilteredStudents = function () {
		if (!$scope.newMobileSMS.Description || $scope.newMobileSMS.Description.trim() === '') {
			Swal.fire('Please enter SMS description');
			return;
		}

		if (!($scope.sendToMobile.StudentContact ||
			$scope.sendToMobile.FatherContact ||
			$scope.sendToMobile.MotherContact ||
			$scope.sendToMobile.GuardianContact)) {
			Swal.fire('Please select at least one contact type');
			return;
		}

		var dataColl = [];
		$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
			var objEntity = node.data;
			var contacts = [];
			if ($scope.sendToMobile.StudentContact && objEntity.ContactNo)
				contacts.push(objEntity.ContactNo);
			if ($scope.sendToMobile.FatherContact && objEntity.F_ContactNo)
				contacts.push(objEntity.F_ContactNo);
			if ($scope.sendToMobile.MotherContact && objEntity.M_ContactNo)
				contacts.push(objEntity.M_ContactNo);
			if ($scope.sendToMobile.GuardianContact && objEntity.G_ContactNo)
				contacts.push(objEntity.G_ContactNo);

			if (contacts.length > 0) {
				dataColl.push({
					studentId: objEntity.StudentId,
					name: objEntity.Name,
					contacts: contacts
				});
			}
		});

		if (dataColl.length === 0) {
			Swal.fire('No students with valid contacts found');
			return;
		}

		Swal.fire({
			title: 'Confirm Send',
			text: `Do you want to send this SMS from mobile to ${dataColl.length} filtered students?`,
			icon: 'question',
			showCancelButton: true,
			confirmButtonText: 'Send',
			cancelButtonText: 'Cancel'
		}).then((result) => {
			if (!result.isConfirmed) return;

			var finalDataColl = [];

			$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
				var objEntity = node.data;

				var contacts = [];
				if ($scope.sendToMobile.StudentContact && objEntity.ContactNo)
					contacts.push(objEntity.ContactNo);
				if ($scope.sendToMobile.FatherContact && objEntity.F_ContactNo)
					contacts.push(objEntity.F_ContactNo);
				if ($scope.sendToMobile.MotherContact && objEntity.M_ContactNo)
					contacts.push(objEntity.M_ContactNo);
				if ($scope.sendToMobile.GuardianContact && objEntity.G_ContactNo)
					contacts.push(objEntity.G_ContactNo);

				angular.forEach(contacts, function (tmpContactNo) {
					var msg = $scope.newMobileSMS.Description;

					for (let x in objEntity) {
						var variable = '$$' + x.toLowerCase() + '$$';
						if (msg.indexOf(variable) >= 0) {
							var val = objEntity[x] || '';
							msg = msg.replace(new RegExp(variable, 'gi'), val);
						}
						if (msg.indexOf('$$') === -1) break;
					}

					finalDataColl.push({
						EntityId: entityFeeSummarySMS,
						StudentId: objEntity.StudentId,
						UserId: objEntity.UserId,
						Title: $scope.newMobileSMS.Title || '',
						Message: msg,
						ContactNo: tmpContactNo,
						StudentName: objEntity.Name
					});
				});
			});
			$http({
				method: 'POST',
				url: base_url + "Global/SendSMSFromMobileToStudent",
				dataType: "json",
				data: JSON.stringify(finalDataColl)
			}).then(function (sRes) {
				Swal.fire(sRes.data.ResponseMSG || 'Mobile SMS sent successfully');
				if (sRes.data.IsSuccess) {
					$('#modal-sms-mobile').modal('hide');
					$scope.ResetMobileSMS();
				}
			}, function () {
				Swal.fire('Failed to send SMS from mobile');
			});
		});
	};


	$scope.ResetMobileSMS = function () {
		$scope.newMobileSMS = { Description: '', Title: '' };
		$scope.selectedMobileSMSTemplate = null;
		$scope.sendToMobile = {
			StudentContact: false,
			FatherContact: true,
			MotherContact: false,
			GuardianContact: false
		};
	};


	//$scope.SendSMSFromMobile = function () {
	//	Swal.fire({
	//		title: 'Do you want to Send SMS From Mobile To the filter data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {
	//			var para1 = {
	//				EntityId: entityFeeSummarySMS,
	//				ForATS: 3,
	//				TemplateType: 1
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Setup/Security/GetSENT",
	//				dataType: "json",
	//				data: JSON.stringify(para1)
	//			}).then(function (res) {
	//				if (res.data.IsSuccess && res.data.Data) {
	//					var templatesColl = res.data.Data;
	//					if (templatesColl && templatesColl.length > 0) {
	//						var templatesName = [];
	//						var sno = 1;
	//						angular.forEach(templatesColl, function (tc) {
	//							templatesName.push(sno + '-' + tc.Name);
	//							sno++;
	//						});

	//						var print = false;

	//						var rptTranId = 0;
	//						var selectedTemplate = null;
	//						if (templatesColl.length == 1) {
	//							rptTranId = templatesColl[0].TranId;
	//							selectedTemplate = templatesColl[0];
	//						}
	//						else {
	//							Swal.fire({
	//								title: 'Templates For SMS',
	//								input: 'select',
	//								inputOptions: templatesName,
	//								inputPlaceholder: 'Select a template',
	//								showCancelButton: true,
	//								inputValidator: (value) => {
	//									return new Promise((resolve) => {
	//										if (value >= 0) {
	//											resolve()
	//											rptTranId = templatesColl[value].TranId;
	//											selectedTemplate = templatesColl[value];

	//											if (rptTranId > 0) {
	//												var dataColl = [];
	//												$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
	//													var objEntity = node.data;
	//													var msg = selectedTemplate.Description;
	//													for (let x in objEntity) {
	//														var variable = '$$' + x.toLowerCase() + '$$';
	//														if (msg.indexOf(variable) >= 0) {
	//															var val = objEntity[x];
	//															msg = msg.replace(variable, val);
	//														}

	//														if (msg.indexOf('$$') == -1)
	//															break;
	//													}

	//													var newSMS = {
	//														EntityId: entityFeeSummarySMS,
	//														StudentId: objEntity.StudentId,
	//														UserId: objEntity.UserId,
	//														Title: selectedTemplate.Title,
	//														Message: msg,
	//														ContactNo: objEntity.F_ContactNo,
	//														StudentName: objEntity.Name
	//													};

	//													dataColl.push(newSMS);
	//												});
	//												print = true;

	//												$http({
	//													method: 'POST',
	//													url: base_url + "Global/SendSMSFromMobileToStudent",
	//													dataType: "json",
	//													data: JSON.stringify(dataColl)
	//												}).then(function (sRes) {
	//													Swal.fire(sRes.data.ResponseMSG);
	//													if (sRes.data.IsSuccess && sRes.data.Data) {

	//													}
	//												});

	//											}

	//										} else {
	//											resolve('You need to select:)')
	//										}
	//									})
	//								}
	//							})
	//						}

	//						if (rptTranId > 0 && print == false) {
	//							var dataColl = [];

	//							$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
	//								var objEntity = node.data;
	//								var msg = selectedTemplate.Description;
	//								for (let x in objEntity) {
	//									var variable = '$$' + x.toLowerCase() + '$$';
	//									if (msg.indexOf(variable) >= 0) {
	//										var val = objEntity[x];
	//										msg = msg.replace(variable, val);
	//									}

	//									if (msg.indexOf('$$') == -1)
	//										break;
	//								}

	//								var newSMS = {
	//									EntityId: entityFeeSummarySMS,
	//									StudentId: objEntity.StudentId,
	//									UserId: objEntity.UserId,
	//									Title: selectedTemplate.Title,
	//									Message: msg,
	//									ContactNo: objEntity.F_ContactNo,
	//									StudentName: objEntity.Name
	//								};

	//								dataColl.push(newSMS);
	//							});
	//							print = true;

	//							$http({
	//								method: 'POST',
	//								url: base_url + "Global/SendSMSFromMobileToStudent",
	//								dataType: "json",
	//								data: JSON.stringify(dataColl)
	//							}).then(function (sRes) {
	//								Swal.fire(sRes.data.ResponseMSG);
	//								if (sRes.data.IsSuccess && sRes.data.Data) {

	//								}
	//							});

	//						}

	//					} else
	//						Swal.fire('No Templates found for SMS');
	//				}
	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	});


	//};

	//$scope.SendNotification = function () {
	//	Swal.fire({
	//		title: 'Do you want to Send Notification To the filter data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {

	//			var para1 = {
	//				EntityId: entityFeeSummarySMS,
	//				ForATS: 3,
	//				TemplateType: 3
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Setup/Security/GetSENT",
	//				dataType: "json",
	//				data: JSON.stringify(para1)
	//			}).then(function (res) {
	//				if (res.data.IsSuccess && res.data.Data) {
	//					var templatesColl = res.data.Data;
	//					if (templatesColl && templatesColl.length > 0) {
	//						var templatesName = [];
	//						var sno = 1;
	//						angular.forEach(templatesColl, function (tc) {
	//							templatesName.push(sno + '-' + tc.Name);
	//							sno++;
	//						});

	//						var print = false;

	//						var rptTranId = 0;
	//						var selectedTemplate = null;
	//						if (templatesColl.length == 1) {
	//							rptTranId = templatesColl[0].TranId;
	//							selectedTemplate = templatesColl[0];
	//						}
	//						else {
	//							Swal.fire({
	//								title: 'Templates For Notification',
	//								input: 'select',
	//								inputOptions: templatesName,
	//								inputPlaceholder: 'Select a template',
	//								showCancelButton: true,
	//								inputValidator: (value) => {
	//									return new Promise((resolve) => {
	//										if (value >= 0) {
	//											resolve()
	//											rptTranId = templatesColl[value].TranId;
	//											selectedTemplate = templatesColl[value];

	//											if (rptTranId > 0) {
	//												var dataColl = [];
	//												$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
	//													var objEntity = node.data;
	//													var msg = selectedTemplate.Description;
	//													for (let x in objEntity) {
	//														var variable = '$$' + x.toLowerCase() + '$$';
	//														if (msg.indexOf(variable) >= 0) {
	//															var val = objEntity[x];
	//															msg = msg.replace(variable, val);
	//														}

	//														if (msg.indexOf('$$') == -1)
	//															break;
	//													}

	//													var newSMS = {
	//														EntityId: entityFeeSummarySMS,
	//														StudentId: objEntity.StudentId,
	//														UserId: objEntity.UserId,
	//														Title: selectedTemplate.Title,
	//														Message: msg,
	//														ContactNo: objEntity.F_ContactNo,
	//														StudentName: objEntity.Name
	//													};

	//													dataColl.push(newSMS);
	//												});
	//												print = true;

	//												$http({
	//													method: 'POST',
	//													url: base_url + "Global/SendNotificationToStudent",
	//													dataType: "json",
	//													data: JSON.stringify(dataColl)
	//												}).then(function (sRes) {
	//													Swal.fire(sRes.data.ResponseMSG);
	//													if (sRes.data.IsSuccess && sRes.data.Data) {

	//													}
	//												});

	//											}

	//										} else {
	//											resolve('You need to select:)')
	//										}
	//									})
	//								}
	//							})
	//						}

	//						if (rptTranId > 0 && print == false) {
	//							var dataColl = [];

	//							$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
	//								var objEntity = node.data;
	//								var msg = selectedTemplate.Description;
	//								for (let x in objEntity) {
	//									var variable = '$$' + x.toLowerCase() + '$$';
	//									if (msg.indexOf(variable) >= 0) {
	//										var val = objEntity[x];
	//										msg = msg.replace(variable, val);
	//									}

	//									if (msg.indexOf('$$') == -1)
	//										break;
	//								}

	//								var newSMS = {
	//									EntityId: entityFeeSummarySMS,
	//									StudentId: objEntity.StudentId,
	//									UserId: objEntity.UserId,
	//									Title: selectedTemplate.Title,
	//									Message: msg,
	//									ContactNo: objEntity.F_ContactNo,
	//									StudentName: objEntity.Name
	//								};

	//								dataColl.push(newSMS);
	//							});
	//							print = true;

	//							$http({
	//								method: 'POST',
	//								url: base_url + "Global/SendNotificationToStudent",
	//								dataType: "json",
	//								data: JSON.stringify(dataColl)
	//							}).then(function (sRes) {
	//								Swal.fire(sRes.data.ResponseMSG);
	//								if (sRes.data.IsSuccess && sRes.data.Data) {

	//								}
	//							});

	//						}

	//					} else
	//						Swal.fire('No Templates found for Notification');
	//				}
	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});

	//		}
	//	});
	//};

	/*New Code added for the npotificatiuon part*/
	$scope.SendNotification = function () {
		// Check if there's any data after filtering
		var filteredDataCount = 0;
		$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
			filteredDataCount++;
		});

		if (filteredDataCount === 0) {
			Swal.fire('No data found after filter to send notification');
			return;
		}

		$scope.filteredStudentsCount = filteredDataCount;

		$scope.newNotice = {};
		$scope.templatesColl = [];
		$scope.selectedTemplate = null;

		var para1 = {
			EntityId: entityFeeSummarySMS,
			ForATS: 3,
			TemplateType: 3
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(para1)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.templatesColl = res.data.Data;
				if ($scope.templatesColl.length === 1) {
					$scope.selectedTemplate = $scope.templatesColl[0];
					$scope.OnTemplateChange();
				}
			}

			// Open modal directly
			$('#modal-notification-filtered').modal('show');

		}, function () {
			Swal.fire('Failed to load templates');
		});
	};


	$scope.OnTemplateChange = function () {
		if ($scope.selectedTemplate && $scope.selectedTemplate.TranId) {
			// Template selected - populate fields
			$scope.newNotice.Title = $scope.selectedTemplate.Title || '';
			$scope.newNotice.Description = $scope.selectedTemplate.Description || '';

			// Optional: Extract and display available variables from template
			if ($scope.newNotice.Description) {
				var variables = $scope.newNotice.Description.match(/\$\$([a-zA-Z0-9_]+)\$\$/g);
				if (variables && variables.length > 0) {
					$scope.availableVariables = [...new Set(variables)]; // Remove duplicates
					console.log('Template variables found:', $scope.availableVariables);
				} else {
					$scope.availableVariables = [];
				}
			}

			// Optional: Show template info
			$scope.selectedTemplateInfo = `Template: ${$scope.selectedTemplate.Name} (${$scope.selectedTemplate.TranId})`;
		} else {
			// No template selected or empty option - clear fields
			$scope.newNotice.Title = '';
			$scope.newNotice.Description = '';
			$scope.availableVariables = [];
			$scope.selectedTemplateInfo = '';
		}
	};

	$scope.SendManualNotificationFiltered = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var contentPath = '';
		$timeout(function () {
			// Upload attachment first if any
			if ($scope.newNotice.AttachmentColl && $scope.newNotice.AttachmentColl.length > 0) {
				$http({
					method: 'POST',
					url: base_url + "Global/UploadAttachments",
					headers: { 'Content-Type': undefined },
					transformRequest: function (data) {
						var formData = new FormData();
						if (data.files) {
							for (var i = 0; i < data.files.length; i++) {
								formData.append("file" + i, data.files[i]);
							}
						}
						return formData;
					},
					data: { files: $scope.newNotice.AttachmentColl }
				}).then(function (res) {
					$scope.loadingstatus = "stop";
					hidePleaseWait();

					if (res.data.IsSuccess == true) {
						if (res.data.Data.length > 0) {
							contentPath = res.data.Data[0];
						}
						processNotification(contentPath);
					} else {
						Swal.fire('Attachment upload failed');
					}
				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire('Attachment upload failed');
				});
			} else {
				processNotification(contentPath);
			}
		});

		function processNotification(contentPath) {
			$timeout(function () {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var dataColl = [];

				// Use forEachNodeAfterFilterAndSort to get filtered data
				$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
					var objEntity = node.data;
					var msg = $scope.newNotice.Description;

					// Replace variables in message
					for (let x in objEntity) {
						var variable = '$$' + x.toLowerCase() + '$$';
						if (msg && msg.indexOf(variable) >= 0) {
							var val = objEntity[x];
							msg = msg.replace(new RegExp(variable, 'gi'), val || '');
						}
					}

					var newSMS = {
						EntityId: entityFeeSummarySMS,
						StudentId: objEntity.StudentId,
						UserId: objEntity.UserId,
						Title: $scope.newNotice.Title,
						Message: msg,
						ContactNo: objEntity.F_ContactNo,
						StudentName: objEntity.Name,
						ContentPath: contentPath
					};

					dataColl.push(newSMS);
				});

				if (dataColl.length === 0) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire('No data to send notification');
					return;
				}

				Swal.fire({
					title: 'Confirm Send',
					text: `Do you want to send this notification to ${dataColl.length} students?`,
					icon: 'question',
					showCancelButton: true,
					confirmButtonText: 'Send',
					cancelButtonText: 'Cancel'
				}).then((result) => {
					if (!result.isConfirmed) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						return;
					}

					$scope.loadingstatus = "running";
					showPleaseWait();

					// Send notification
					$http({
						method: 'POST',
						url: base_url + "Global/SendNotificationToStudent",
						dataType: "json",
						data: JSON.stringify(dataColl)
					}).then(function (sRes) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";

						Swal.fire(sRes.data.ResponseMSG || 'Notification sent successfully');

						if (sRes.data.IsSuccess === true) {
							$('#modal-notification-filtered').modal('hide');
							// Reset form
							$scope.ResetNotification();
						}
					}, function (err) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						Swal.fire('Failed to send notification');
					});
				});
			});
		}
	};
	//Ends

	//$scope.SendSMSSFS = function () {
	//	Swal.fire({
	//		title: 'Do you want to Send SMS To the filter data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {
	//			var para1 = {
	//				EntityId: entityFeeSummarySMS,
	//				ForATS: 3,
	//				TemplateType: 1
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Setup/Security/GetSENT",
	//				dataType: "json",
	//				data: JSON.stringify(para1)
	//			}).then(function (res) {
	//				if (res.data.IsSuccess && res.data.Data) {
	//					var templatesColl = res.data.Data;
	//					if (templatesColl && templatesColl.length > 0) {
	//						var templatesName = [];
	//						var sno = 1;
	//						angular.forEach(templatesColl, function (tc) {
	//							templatesName.push(sno + '-' + tc.Name);
	//							sno++;
	//						});

	//						var print = false;

	//						var rptTranId = 0;
	//						var selectedTemplate = null;
	//						if (templatesColl.length == 1) {
	//							rptTranId = templatesColl[0].TranId;
	//							selectedTemplate = templatesColl[0];
	//						}
	//						else {
	//							Swal.fire({
	//								title: 'Templates For SMS',
	//								input: 'select',
	//								inputOptions: templatesName,
	//								inputPlaceholder: 'Select a template',
	//								showCancelButton: true,
	//								inputValidator: (value) => {
	//									return new Promise((resolve) => {
	//										if (value >= 0) {
	//											resolve()
	//											rptTranId = templatesColl[value].TranId;
	//											selectedTemplate = templatesColl[value];

	//											if (rptTranId > 0) {
	//												var dataColl = [];
	//												$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
	//													var objEntity = node.data;
	//													var msg = selectedTemplate.Description;
	//													for (let x in objEntity) {
	//														var variable = '$$' + x.toLowerCase() + '$$';
	//														if (msg.indexOf(variable) >= 0) {
	//															var val = objEntity[x];
	//															msg = msg.replace(variable, val);
	//														}

	//														if (msg.indexOf('$$') == -1)
	//															break;
	//													}

	//													var newSMS = {
	//														EntityId: entityFeeSummarySMS,
	//														StudentId: objEntity.StudentId,
	//														UserId: objEntity.UserId,
	//														Title: selectedTemplate.Title,
	//														Message: msg,
	//														ContactNo: objEntity.F_ContactNo,
	//														StudentName: objEntity.Name
	//													};

	//													dataColl.push(newSMS);
	//												});

	//												print = true;

	//												$http({
	//													method: 'POST',
	//													url: base_url + "Global/SendSMSToStudent",
	//													dataType: "json",
	//													data: JSON.stringify(dataColl)
	//												}).then(function (sRes) {
	//													Swal.fire(sRes.data.ResponseMSG);
	//													if (sRes.data.IsSuccess && sRes.data.Data) {

	//													}
	//												});

	//											}

	//										} else {
	//											resolve('You need to select:)')
	//										}
	//									})
	//								}
	//							})
	//						}

	//						if (rptTranId > 0 && print == false) {
	//							var dataColl = [];

	//							$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
	//								var objEntity = node.data;
	//								var msg = selectedTemplate.Description;
	//								for (let x in objEntity) {
	//									var variable = '$$' + x.toLowerCase() + '$$';
	//									if (msg.indexOf(variable) >= 0) {
	//										var val = objEntity[x];
	//										msg = msg.replace(variable, val);
	//									}

	//									if (msg.indexOf('$$') == -1)
	//										break;
	//								}

	//								var newSMS = {
	//									EntityId: entityFeeSummarySMS,
	//									StudentId: objEntity.StudentId,
	//									UserId: objEntity.UserId,
	//									Title: selectedTemplate.Title,
	//									Message: msg,
	//									ContactNo: objEntity.F_ContactNo,
	//									StudentName: objEntity.Name
	//								};

	//								dataColl.push(newSMS);
	//							});
	//							print = true;

	//							$http({
	//								method: 'POST',
	//								url: base_url + "Global/SendSMSToStudent",
	//								dataType: "json",
	//								data: JSON.stringify(dataColl)
	//							}).then(function (sRes) {
	//								Swal.fire(sRes.data.ResponseMSG);
	//								if (sRes.data.IsSuccess && sRes.data.Data) {

	//								}
	//							});

	//						}

	//					} else
	//						Swal.fire('No Templates found for SMS');
	//				}
	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	});


	//};

	// Send SMS to filtered students from SFS grid
	$scope.SendSMSSFS = function () {
		// Count filtered data
		var filteredDataCount = 0;
		$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
			filteredDataCount++;
		});

		if (filteredDataCount === 0) {
			Swal.fire('No data found after filter to send SMS');
			return;
		}

		$scope.filteredSMSSFSStudentsCount = filteredDataCount;
		$scope.newSMSSFS = { Description: '', Title: '' };
		$scope.selectedSMSTemplateSFS = null;
		$scope.smsTemplatesCollSFS = [];

		// Default send to Father contact only (matching original behavior)
		$scope.sendToSFS = {
			StudentContact: false,
			FatherContact: true,
			MotherContact: false,
			GuardianContact: false
		};

		var para1 = {
			EntityId: entityFeeSummarySMS,
			ForATS: 3,
			TemplateType: 1
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(para1)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.smsTemplatesCollSFS = res.data.Data;

				if ($scope.smsTemplatesCollSFS.length === 1) {
					$scope.selectedSMSTemplateSFS = $scope.smsTemplatesCollSFS[0];
					$scope.OnSMSTemplateChangeSFS();

					let recipients = $scope.selectedSMSTemplateSFS.Recipients || '';
					$scope.sendToSFS.StudentContact = recipients.includes('$$contactno$$');
					$scope.sendToSFS.FatherContact = recipients.includes('$$f_contactno$$');
					$scope.sendToSFS.MotherContact = recipients.includes('$$m_contactno$$');
					$scope.sendToSFS.GuardianContact = recipients.includes('$$g_contacno$$');
				}

				// Open modal
				$('#modal-sms-sfs').modal('show');
			} else {
				Swal.fire('No templates found for SMS');
			}
		}, function (err) {
			Swal.fire('Failed to load templates');
		});
	};

	// Template change handler for SFS SMS
	$scope.OnSMSTemplateChangeSFS = function () {
		if ($scope.selectedSMSTemplateSFS) {
			$scope.newSMSSFS.Description = $scope.selectedSMSTemplateSFS.Description || '';
			$scope.newSMSSFS.Title = $scope.selectedSMSTemplateSFS.Title || '';

			var recipients = $scope.selectedSMSTemplateSFS.Recipients || '';

			$scope.sendToSFS.StudentContact = recipients.includes('$$contactno$$');
			$scope.sendToSFS.FatherContact = recipients.includes('$$f_contactno$$');
			$scope.sendToSFS.MotherContact = recipients.includes('$$m_contactno$$');
			$scope.sendToSFS.GuardianContact = recipients.includes('$$g_contacno$$');
		} else {
			$scope.newSMSSFS.Description = '';
			$scope.newSMSSFS.Title = '';

			$scope.sendToSFS.StudentContact = false;
			$scope.sendToSFS.FatherContact = true;
			$scope.sendToSFS.MotherContact = false;
			$scope.sendToSFS.GuardianContact = false;
		}
	};

	// Send SMS to filtered students from SFS grid
	$scope.SendSMSToSFSStudents = function () {
		if (!$scope.newSMSSFS.Description || $scope.newSMSSFS.Description.trim() === '') {
			Swal.fire('Please enter SMS description');
			return;
		}
		if (!($scope.sendToSFS.StudentContact ||
			$scope.sendToSFS.FatherContact ||
			$scope.sendToSFS.MotherContact ||
			$scope.sendToSFS.GuardianContact)) {
			Swal.fire('Please select at least one contact type to send SMS');
			return;
		}

		var dataColl = [];
		$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
			var objEntity = node.data;

			var contacts = [];
			if ($scope.sendToSFS.StudentContact && objEntity.ContactNo)
				contacts.push(objEntity.ContactNo);
			if ($scope.sendToSFS.FatherContact && objEntity.F_ContactNo)
				contacts.push(objEntity.F_ContactNo);
			if ($scope.sendToSFS.MotherContact && objEntity.M_ContactNo)
				contacts.push(objEntity.M_ContactNo);
			if ($scope.sendToSFS.GuardianContact && objEntity.G_ContactNo)
				contacts.push(objEntity.G_ContactNo);

			if (contacts.length > 0) {
				dataColl.push({
					studentId: objEntity.StudentId,
					name: objEntity.Name,
					contacts: contacts
				});
			}
		});

		if (dataColl.length === 0) {
			Swal.fire('No students with valid contacts found');
			return;
		}

		Swal.fire({
			title: 'Confirm Send',
			text: `Do you want to send this SMS to ${dataColl.length} filtered students?`,
			icon: 'question',
			showCancelButton: true,
			confirmButtonText: 'Send',
			cancelButtonText: 'Cancel'
		}).then((result) => {
			if (!result.isConfirmed) return;

			// Prepare final data for API
			var finalDataColl = [];

			$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
				var objEntity = node.data;

				var contacts = [];
				if ($scope.sendToSFS.StudentContact && objEntity.ContactNo)
					contacts.push(objEntity.ContactNo);
				if ($scope.sendToSFS.FatherContact && objEntity.F_ContactNo)
					contacts.push(objEntity.F_ContactNo);
				if ($scope.sendToSFS.MotherContact && objEntity.M_ContactNo)
					contacts.push(objEntity.M_ContactNo);
				if ($scope.sendToSFS.GuardianContact && objEntity.G_ContactNo)
					contacts.push(objEntity.G_ContactNo);

				angular.forEach(contacts, function (tmpContactNo) {
					var msg = $scope.newSMSSFS.Description;

					for (let x in objEntity) {
						var variable = '$$' + x.toLowerCase() + '$$';
						if (msg.indexOf(variable) >= 0) {
							var val = objEntity[x] || '';
							msg = msg.replace(new RegExp(variable, 'gi'), val);
						}
						if (msg.indexOf('$$') === -1) break;
					}

					finalDataColl.push({
						EntityId: entityFeeSummarySMS,
						StudentId: objEntity.StudentId,
						UserId: objEntity.UserId,
						Title: $scope.newSMSSFS.Title || '',
						Message: msg,
						ContactNo: tmpContactNo,
						StudentName: objEntity.Name
					});
				});
			});

			// Send SMS
			$http({
				method: 'POST',
				url: base_url + "Global/SendSMSToStudent",
				dataType: "json",
				data: JSON.stringify(finalDataColl)
			}).then(function (sRes) {
				Swal.fire(sRes.data.ResponseMSG || 'SMS sent successfully');
				if (sRes.data.IsSuccess) {
					$('#modal-sms-sfs').modal('hide');
					// Reset form
					$scope.ResetSMSSFS();
				}
			}, function () {
				Swal.fire('Failed to send SMS');
			});
		});
	};

	// Reset function for SFS SMS
	$scope.ResetSMSSFS = function () {
		$scope.newSMSSFS = { Description: '', Title: '' };
		$scope.selectedSMSTemplateSFS = null;
		$scope.sendToSFS = {
			StudentContact: false,
			FatherContact: true,
			MotherContact: false,
			GuardianContact: false
		};
	};
	//$scope.SendSMSFromMobileSFS = function () {

	//	Swal.fire({
	//		title: 'Do you want to Send SMS From Mobile To the filter data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {
	//			var para1 = {
	//				EntityId: entityFeeSummarySMS,
	//				ForATS: 3,
	//				TemplateType: 1
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Setup/Security/GetSENT",
	//				dataType: "json",
	//				data: JSON.stringify(para1)
	//			}).then(function (res) {
	//				if (res.data.IsSuccess && res.data.Data) {
	//					var templatesColl = res.data.Data;
	//					if (templatesColl && templatesColl.length > 0) {
	//						var templatesName = [];
	//						var sno = 1;
	//						angular.forEach(templatesColl, function (tc) {
	//							templatesName.push(sno + '-' + tc.Name);
	//							sno++;
	//						});

	//						var print = false;

	//						var rptTranId = 0;
	//						var selectedTemplate = null;
	//						if (templatesColl.length == 1) {
	//							rptTranId = templatesColl[0].TranId;
	//							selectedTemplate = templatesColl[0];
	//						}
	//						else {
	//							Swal.fire({
	//								title: 'Templates For SMS',
	//								input: 'select',
	//								inputOptions: templatesName,
	//								inputPlaceholder: 'Select a template',
	//								showCancelButton: true,
	//								inputValidator: (value) => {
	//									return new Promise((resolve) => {
	//										if (value >= 0) {
	//											resolve()
	//											rptTranId = templatesColl[value].TranId;
	//											selectedTemplate = templatesColl[value];

	//											if (rptTranId > 0) {
	//												var dataColl = [];
	//												$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
	//													var objEntity = node.data;
	//													var msg = selectedTemplate.Description;
	//													for (let x in objEntity) {
	//														var variable = '$$' + x.toLowerCase() + '$$';
	//														if (msg.indexOf(variable) >= 0) {
	//															var val = objEntity[x];
	//															msg = msg.replace(variable, val);
	//														}

	//														if (msg.indexOf('$$') == -1)
	//															break;
	//													}

	//													var newSMS = {
	//														EntityId: entityFeeSummarySMS,
	//														StudentId: objEntity.StudentId,
	//														UserId: objEntity.UserId,
	//														Title: selectedTemplate.Title,
	//														Message: msg,
	//														ContactNo: objEntity.F_ContactNo,
	//														StudentName: objEntity.Name
	//													};

	//													dataColl.push(newSMS);
	//												});
	//												print = true;

	//												$http({
	//													method: 'POST',
	//													url: base_url + "Global/SendSMSFromMobileToStudent",
	//													dataType: "json",
	//													data: JSON.stringify(dataColl)
	//												}).then(function (sRes) {
	//													Swal.fire(sRes.data.ResponseMSG);
	//													if (sRes.data.IsSuccess && sRes.data.Data) {

	//													}
	//												});

	//											}

	//										} else {
	//											resolve('You need to select:)')
	//										}
	//									})
	//								}
	//							})
	//						}

	//						if (rptTranId > 0 && print == false) {
	//							var dataColl = [];

	//							$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
	//								var objEntity = node.data;
	//								var msg = selectedTemplate.Description;
	//								for (let x in objEntity) {
	//									var variable = '$$' + x.toLowerCase() + '$$';
	//									if (msg.indexOf(variable) >= 0) {
	//										var val = objEntity[x];
	//										msg = msg.replace(variable, val);
	//									}

	//									if (msg.indexOf('$$') == -1)
	//										break;
	//								}

	//								var newSMS = {
	//									EntityId: entityFeeSummarySMS,
	//									StudentId: objEntity.StudentId,
	//									UserId: objEntity.UserId,
	//									Title: selectedTemplate.Title,
	//									Message: msg,
	//									ContactNo: objEntity.F_ContactNo,
	//									StudentName: objEntity.Name
	//								};

	//								dataColl.push(newSMS);
	//							});
	//							print = true;

	//							$http({
	//								method: 'POST',
	//								url: base_url + "Global/SendSMSFromMobileToStudent",
	//								dataType: "json",
	//								data: JSON.stringify(dataColl)
	//							}).then(function (sRes) {
	//								Swal.fire(sRes.data.ResponseMSG);
	//								if (sRes.data.IsSuccess && sRes.data.Data) {

	//								}
	//							});

	//						}

	//					} else
	//						Swal.fire('No Templates found for SMS');
	//				}
	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	});


	//};


	$scope.SendSMSFromMobileSFS = function () {
		var filteredDataCount = 0;
		$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
			filteredDataCount++;
		});

		if (filteredDataCount === 0) {
			Swal.fire('No data found after filter to send SMS from mobile');
			return;
		}

		$scope.filteredMobileSMSStudentsCountSFS = filteredDataCount;

		$scope.newMobileSMSSFS = { Description: '', Title: '' };
		$scope.selectedMobileSMSTemplateSFS = null;
		$scope.mobileSmsTemplatesCollSFS = [];

		$scope.sendToMobileSFS = {
			StudentContact: false,
			FatherContact: true,
			MotherContact: false,
			GuardianContact: false
		};

		var para1 = {
			EntityId: entityFeeSummarySMS,
			ForATS: 3,
			TemplateType: 1
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(para1)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.mobileSmsTemplatesCollSFS = res.data.Data;

				if ($scope.mobileSmsTemplatesCollSFS.length === 1) {
					$scope.selectedMobileSMSTemplateSFS = $scope.mobileSmsTemplatesCollSFS[0];
					$scope.OnMobileSMSTemplateChangeSFS();
				}

				$('#modal-sms-mobile-sfs').modal('show');
			} else {
				Swal.fire('No templates found for mobile SMS');
			}
		}, function () {
			Swal.fire('Failed to load templates');
		});
	};

	$scope.OnMobileSMSTemplateChangeSFS = function () {
		if ($scope.selectedMobileSMSTemplateSFS) {
			$scope.newMobileSMSSFS.Description = $scope.selectedMobileSMSTemplateSFS.Description;
			$scope.newMobileSMSSFS.Title = $scope.selectedMobileSMSTemplateSFS.Title;

			var recipients = $scope.selectedMobileSMSTemplateSFS.Recipients || '';
			$scope.sendToMobileSFS.StudentContact = recipients.includes('$$contactno$$');
			$scope.sendToMobileSFS.FatherContact = recipients.includes('$$f_contactno$$');
			$scope.sendToMobileSFS.MotherContact = recipients.includes('$$m_contactno$$');
			$scope.sendToMobileSFS.GuardianContact = recipients.includes('$$g_contacno$$');
		} else {
			$scope.newMobileSMSSFS.Description = '';
			$scope.newMobileSMSSFS.Title = '';
			$scope.sendToMobileSFS.StudentContact = false;
			$scope.sendToMobileSFS.FatherContact = true;
			$scope.sendToMobileSFS.MotherContact = false;
			$scope.sendToMobileSFS.GuardianContact = false;
		}
	};

	$scope.SendMobileSMSToFilteredStudentsSFS = function () {
		if (!$scope.newMobileSMSSFS.Description || $scope.newMobileSMSSFS.Description.trim() === '') {
			Swal.fire('Please enter SMS description');
			return;
		}

		if (!($scope.sendToMobileSFS.StudentContact ||
			$scope.sendToMobileSFS.FatherContact ||
			$scope.sendToMobileSFS.MotherContact ||
			$scope.sendToMobileSFS.GuardianContact)) {
			Swal.fire('Please select at least one contact type');
			return;
		}

		var dataColl = [];
		$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
			var objEntity = node.data;
			var contacts = [];

			// Collect selected contacts
			if ($scope.sendToMobileSFS.StudentContact && objEntity.ContactNo)
				contacts.push(objEntity.ContactNo);
			if ($scope.sendToMobileSFS.FatherContact && objEntity.F_ContactNo)
				contacts.push(objEntity.F_ContactNo);
			if ($scope.sendToMobileSFS.MotherContact && objEntity.M_ContactNo)
				contacts.push(objEntity.M_ContactNo);
			if ($scope.sendToMobileSFS.GuardianContact && objEntity.G_ContactNo)
				contacts.push(objEntity.G_ContactNo);

			if (contacts.length > 0) {
				dataColl.push({
					studentId: objEntity.StudentId,
					name: objEntity.Name,
					contacts: contacts
				});
			}
		});

		if (dataColl.length === 0) {
			Swal.fire('No students with valid contacts found');
			return;
		}

		Swal.fire({
			title: 'Confirm Send',
			text: `Do you want to send this SMS from mobile to ${dataColl.length} filtered students?`,
			icon: 'question',
			showCancelButton: true,
			confirmButtonText: 'Send',
			cancelButtonText: 'Cancel'
		}).then((result) => {
			if (!result.isConfirmed) return;

			var finalDataColl = [];

			$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
				var objEntity = node.data;

				var contacts = [];
				if ($scope.sendToMobileSFS.StudentContact && objEntity.ContactNo)
					contacts.push(objEntity.ContactNo);
				if ($scope.sendToMobileSFS.FatherContact && objEntity.F_ContactNo)
					contacts.push(objEntity.F_ContactNo);
				if ($scope.sendToMobileSFS.MotherContact && objEntity.M_ContactNo)
					contacts.push(objEntity.M_ContactNo);
				if ($scope.sendToMobileSFS.GuardianContact && objEntity.G_ContactNo)
					contacts.push(objEntity.G_ContactNo);

				angular.forEach(contacts, function (tmpContactNo) {
					var msg = $scope.newMobileSMSSFS.Description;

					// Replace variables in the message
					for (let x in objEntity) {
						var variable = '$$' + x.toLowerCase() + '$$';
						if (msg.indexOf(variable) >= 0) {
							var val = objEntity[x] || '';
							msg = msg.replace(new RegExp(variable, 'gi'), val);
						}
						if (msg.indexOf('$$') === -1) break;
					}

					finalDataColl.push({
						EntityId: entityFeeSummarySMS,
						StudentId: objEntity.StudentId,
						UserId: objEntity.UserId,
						Title: $scope.newMobileSMSSFS.Title || '',
						Message: msg,
						ContactNo: tmpContactNo,
						StudentName: objEntity.Name
					});
				});
			});

			$http({
				method: 'POST',
				url: base_url + "Global/SendSMSFromMobileToStudent",
				dataType: "json",
				data: JSON.stringify(finalDataColl)
			}).then(function (sRes) {
				Swal.fire(sRes.data.ResponseMSG || 'Mobile SMS sent successfully');
				if (sRes.data.IsSuccess) {
					$('#modal-sms-mobile-sfs').modal('hide');
					$scope.ResetMobileSMSSFS();
				}
			}, function () {
				Swal.fire('Failed to send SMS from mobile');
			});
		});
	};

	$scope.ResetMobileSMSSFS = function () {
		$scope.newMobileSMSSFS = { Description: '', Title: '' };
		$scope.selectedMobileSMSTemplateSFS = null;
		$scope.sendToMobileSFS = {
			StudentContact: false,
			FatherContact: true,
			MotherContact: false,
			GuardianContact: false
		};
	};

	$scope.SendNotificationSFS = function () {
		Swal.fire({
			title: 'Do you want to Send Notification To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				var para1 = {
					EntityId: entityFeeSummarySMS,
					ForATS: 3,
					TemplateType: 3
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/GetSENT",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						var templatesColl = res.data.Data;
						if (templatesColl && templatesColl.length > 0) {
							var templatesName = [];
							var sno = 1;
							angular.forEach(templatesColl, function (tc) {
								templatesName.push(sno + '-' + tc.Name);
								sno++;
							});

							var print = false;

							var rptTranId = 0;
							var selectedTemplate = null;
							if (templatesColl.length == 1) {
								rptTranId = templatesColl[0].TranId;
								selectedTemplate = templatesColl[0];
							}
							else {
								Swal.fire({
									title: 'Templates For Notification',
									input: 'select',
									inputOptions: templatesName,
									inputPlaceholder: 'Select a template',
									showCancelButton: true,
									inputValidator: (value) => {
										return new Promise((resolve) => {
											if (value >= 0) {
												resolve()
												rptTranId = templatesColl[value].TranId;
												selectedTemplate = templatesColl[value];

												if (rptTranId > 0) {
													var dataColl = [];
													$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
														var objEntity = node.data;
														var msg = selectedTemplate.Description;
														for (let x in objEntity) {
															var variable = '$$' + x.toLowerCase() + '$$';
															if (msg.indexOf(variable) >= 0) {
																var val = objEntity[x];
																msg = msg.replace(variable, val);
															}

															if (msg.indexOf('$$') == -1)
																break;
														}

														var newSMS = {
															EntityId: entityFeeSummarySMS,
															StudentId: objEntity.StudentId,
															UserId: objEntity.UserId,
															Title: selectedTemplate.Title,
															Message: msg,
															ContactNo: objEntity.F_ContactNo,
															StudentName: objEntity.Name
														};

														dataColl.push(newSMS);
													});
													print = true;

													$http({
														method: 'POST',
														url: base_url + "Global/SendNotificationToStudent",
														dataType: "json",
														data: JSON.stringify(dataColl)
													}).then(function (sRes) {
														Swal.fire(sRes.data.ResponseMSG);
														if (sRes.data.IsSuccess && sRes.data.Data) {

														}
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

								$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
									var objEntity = node.data;
									var msg = selectedTemplate.Description;
									for (let x in objEntity) {
										var variable = '$$' + x.toLowerCase() + '$$';
										if (msg.indexOf(variable) >= 0) {
											var val = objEntity[x];
											msg = msg.replace(variable, val);
										}

										if (msg.indexOf('$$') == -1)
											break;
									}

									var newSMS = {
										EntityId: entityFeeSummarySMS,
										StudentId: objEntity.StudentId,
										UserId: objEntity.UserId,
										Title: selectedTemplate.Title,
										Message: msg,
										ContactNo: objEntity.F_ContactNo,
										StudentName: objEntity.Name
									};

									dataColl.push(newSMS);
								});
								print = true;

								$http({
									method: 'POST',
									url: base_url + "Global/SendNotificationToStudent",
									dataType: "json",
									data: JSON.stringify(dataColl)
								}).then(function (sRes) {
									Swal.fire(sRes.data.ResponseMSG);
									if (sRes.data.IsSuccess && sRes.data.Data) {

									}
								});

							}

						} else
							Swal.fire('No Templates found for Notification');
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}
		});
	};


	//Send Notification to filtered students from SFS grid
	$scope.SendNotificationSFS = function () {
		// Check if there's any data after filtering
		var filteredDataCount = 0;
		$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
			filteredDataCount++;
		});

		if (filteredDataCount === 0) {
			Swal.fire('No data found after filter to send notification');
			return;
		}

		$scope.filteredNotificationSFSStudentsCount = filteredDataCount;

		$scope.newNotificationSFS = {};
		$scope.notificationTemplatesCollSFS = [];
		$scope.selectedNotificationTemplateSFS = null;

		var para1 = {
			EntityId: entityFeeSummarySMS,
			ForATS: 3,
			TemplateType: 3
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(para1)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.notificationTemplatesCollSFS = res.data.Data;
				if ($scope.notificationTemplatesCollSFS.length === 1) {
					$scope.selectedNotificationTemplateSFS = $scope.notificationTemplatesCollSFS[0];
					$scope.OnNotificationTemplateChangeSFS();
				}
			}

			// Open modal directly
			$('#modal-notification-sfs').modal('show');

		}, function () {
			Swal.fire('Failed to load templates');
		});
	};

	// Template change handler for SFS Notifications
	$scope.OnNotificationTemplateChangeSFS = function () {
		if ($scope.selectedNotificationTemplateSFS && $scope.selectedNotificationTemplateSFS.TranId) {
			// Template selected - populate fields
			$scope.newNotificationSFS.Title = $scope.selectedNotificationTemplateSFS.Title || '';
			$scope.newNotificationSFS.Description = $scope.selectedNotificationTemplateSFS.Description || '';

			// Extract and display available variables from template
			if ($scope.newNotificationSFS.Description) {
				var variables = $scope.newNotificationSFS.Description.match(/\$\$([a-zA-Z0-9_]+)\$\$/g);
				if (variables && variables.length > 0) {
					$scope.availableVariablesSFS = [...new Set(variables)]; // Remove duplicates
					console.log('Template variables found:', $scope.availableVariablesSFS);
				} else {
					$scope.availableVariablesSFS = [];
				}
			}

			// Show template info
			$scope.selectedTemplateInfoSFS = `Template: ${$scope.selectedNotificationTemplateSFS.Name} (${$scope.selectedNotificationTemplateSFS.TranId})`;
		} else {
			// No template selected or empty option - clear fields
			$scope.newNotificationSFS.Title = '';
			$scope.newNotificationSFS.Description = '';
			$scope.availableVariablesSFS = [];
			$scope.selectedTemplateInfoSFS = '';
		}
	};

	// Send notification to filtered SFS students
	$scope.SendManualNotificationSFSFiltered = function () {
		$scope.loadingstatusSFS = "running";
		showPleaseWait();

		var contentPath = '';
		$timeout(function () {
			// Upload attachment first if any
			if ($scope.newNotificationSFS.AttachmentColl && $scope.newNotificationSFS.AttachmentColl.length > 0) {
				$http({
					method: 'POST',
					url: base_url + "Global/UploadAttachments",
					headers: { 'Content-Type': undefined },
					transformRequest: function (data) {
						var formData = new FormData();
						if (data.files) {
							for (var i = 0; i < data.files.length; i++) {
								formData.append("file" + i, data.files[i]);
							}
						}
						return formData;
					},
					data: { files: $scope.newNotificationSFS.AttachmentColl }
				}).then(function (res) {
					$scope.loadingstatusSFS = "stop";
					hidePleaseWait();

					if (res.data.IsSuccess == true) {
						if (res.data.Data.length > 0) {
							contentPath = res.data.Data[0];
						}
						processNotificationSFS(contentPath);
					} else {
						Swal.fire('Attachment upload failed');
					}
				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatusSFS = "stop";
					Swal.fire('Attachment upload failed');
				});
			} else {
				processNotificationSFS(contentPath);
			}
		});

		function processNotificationSFS(contentPath) {
			$timeout(function () {
				$scope.loadingstatusSFS = "running";
				showPleaseWait();

				var dataColl = [];

				// Use forEachNodeAfterFilterAndSort to get filtered data
				$scope.gridOptionsSib.api.forEachNodeAfterFilterAndSort(function (node) {
					var objEntity = node.data;
					var msg = $scope.newNotificationSFS.Description;

					// Replace variables in message
					for (let x in objEntity) {
						var variable = '$$' + x.toLowerCase() + '$$';
						if (msg && msg.indexOf(variable) >= 0) {
							var val = objEntity[x];
							msg = msg.replace(new RegExp(variable, 'gi'), val || '');
						}
					}

					var newNotification = {
						EntityId: entityFeeSummarySMS,
						StudentId: objEntity.StudentId,
						UserId: objEntity.UserId,
						Title: $scope.newNotificationSFS.Title,
						Message: msg,
						ContactNo: objEntity.F_ContactNo,
						StudentName: objEntity.Name,
						ContentPath: contentPath
					};

					dataColl.push(newNotification);
				});

				if (dataColl.length === 0) {
					hidePleaseWait();
					$scope.loadingstatusSFS = "stop";
					Swal.fire('No data to send notification');
					return;
				}

				Swal.fire({
					title: 'Confirm Send',
					text: `Do you want to send this notification to ${dataColl.length} students?`,
					icon: 'question',
					showCancelButton: true,
					confirmButtonText: 'Send',
					cancelButtonText: 'Cancel'
				}).then((result) => {
					if (!result.isConfirmed) {
						hidePleaseWait();
						$scope.loadingstatusSFS = "stop";
						return;
					}

					$scope.loadingstatusSFS = "running";
					showPleaseWait();

					// Send notification
					$http({
						method: 'POST',
						url: base_url + "Global/SendNotificationToStudent",
						dataType: "json",
						data: JSON.stringify(dataColl)
					}).then(function (sRes) {
						hidePleaseWait();
						$scope.loadingstatusSFS = "stop";

						Swal.fire(sRes.data.ResponseMSG || 'Notification sent successfully');

						if (sRes.data.IsSuccess === true) {
							$('#modal-notification-sfs').modal('hide');
							// Reset form
							$scope.ResetNotificationSFS();
						}
					}, function (err) {
						hidePleaseWait();
						$scope.loadingstatusSFS = "stop";
						Swal.fire('Failed to send notification');
					});
				});
			});
		}
	};

	// Reset function for SFS Notifications
	$scope.ResetNotificationSFS = function () {
		$scope.newNotificationSFS = {};
		$scope.selectedNotificationTemplateSFS = null;
		$scope.availableVariablesSFS = [];
		$scope.selectedTemplateInfoSFS = '';
		$scope.loadingstatusSFS = '';

		// Reset file input if you have a directive to handle it
		if ($scope.resetFileInputSFS) {
			$scope.resetFileInputSFS();
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
	}

	$scope.ExportFeeMappingAsCSV = function () {
		var params = {
			fileName: 'feeSummary.csv',
			sheetName: 'feeSummary'
		};
		$scope.gridOptions44.api.exportDataAsCsv(params);
	}


	//Added By Suresh on Magh 22 Starts from here

	$scope.getSiblingFeeSummary = function () {
		$scope.dataForBottomGridSib = [];

		if ($scope.SibfeeSummary.PeriodAs == 1) {
			$scope.SibfeeSummary.DateFrom_TMP = null;
			$scope.SibfeeSummary.DateTo_TMP = null;
			if ($scope.SibfeeSummary.FromMonthId > 0 && $scope.SibfeeSummary.ToMonthId > 0) {

			} else {
				//Swal.fire('Please ! Select Month Period');
				return;
			}
		} else if ($scope.SibfeeSummary.PeriodAs == 2) {
			if (!$scope.SibfeeSummary.DateFromDet || !$scope.SibfeeSummary.DateToDet) {
				//Swal.fire('Please ! Select DateFrom/To');
				return;
			}
		} else {
			return;
		}

		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			fromMonthId: ($scope.SibfeeSummary.PeriodAs == 1 ? $scope.SibfeeSummary.FromMonthId : 0),
			toMonthId: ($scope.SibfeeSummary.PeriodAs == 1 ? $scope.SibfeeSummary.ToMonthId : 0),
			forStudent: $scope.SibfeeSummary.ForStudent,
			feeItemIdColl: $scope.SibfeeSummary.FeeItemIdColl,
			classId: $scope.SibfeeSummary.SelectedClass ? $scope.SibfeeSummary.SelectedClass.ClassId : 0,
			sectionId: $scope.SibfeeSummary.SelectedClass ? $scope.SibfeeSummary.SelectedClass.SectionId : 0,
			batchId: $scope.SibfeeSummary.BatchId,
			semesterId: $scope.SibfeeSummary.SemesterId,
			classYearId: $scope.SibfeeSummary.ClassYearId,
			ForPaymentFollowup: false,
			FollowupType: 0,
			dateFrom: ($scope.SibfeeSummary.PeriodAs == 2 ? $filter('date')(new Date($scope.SibfeeSummary.DateFromDet.dateAD), 'yyyy-MM-dd') : null),
			dateTo: ($scope.SibfeeSummary.PeriodAs == 2 ? $filter('date')(new Date($scope.SibfeeSummary.DateToDet.dateAD), 'yyyy-MM-dd') : null),
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Report/GetFeeSummary",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$scope.DataColl = mx(res.data.Data);
				var query = $scope.DataColl.groupBy(t => ({ ParentStudentId: t.ParentStudentId }));
				angular.forEach(query, function (q) {
					/*	var fst = q.elements[0];*/
					var fst = q.elements.find(item => item.StudentId === item.ParentStudentId) || q.elements[0];

					var totalFutureDR = q.elements.reduce((sum, item) => sum + (item.FutureDR || 0), 0);
					var totalFutureCR = q.elements.reduce((sum, item) => sum + (item.FutureCR || 0), 0);
					var totalDues = q.elements.reduce((sum, item) => sum + (item.TotalDues || 0), 0);
					var totalOpening = q.elements.reduce((sum, item) => sum + (item.Opening || 0), 0);
					var totalDrTotal = q.elements.reduce((sum, item) => sum + (item.DrTotal || 0), 0);
					var totalCredit = q.elements.reduce((sum, item) => sum + (item.TotalCredit || 0), 0);
					var crDiscountAmt = q.elements.reduce((sum, item) => sum + (item.CrDiscountAmt || 0), 0);
					var futureDues = q.elements.reduce((sum, item) => sum + (item.FutureDues || 0), 0);
					var lastReceiptAmt = q.elements.reduce((sum, item) => sum + (item.LastReceiptAmt || 0), 0);

					var beData = {
						AutoNumber: fst.AutoNumber,
						UserId: fst.UserId,
						RegdNo: fst.RegdNo,
						RollNo: fst.RollNo,
						ClassName: fst.ClassName,
						SectionName: fst.SectionName,
						Batch: fst.Batch,
						Faculty: fst.Faculty,
						Level: fst.Level,
						Semester: fst.Semester,
						ClassYear: fst.ClassYear,
						Name: fst.Name,
						FeeItemName: fst.FeeItemName,

						FatherName: fst.FatherName,
						F_ContactNo: fst.F_ContactNo,
						MotherName: fst.MotherName,
						M_ContactNo: fst.M_ContactNo,
						Address: fst.Address,
						IsNewStudent: fst.IsNewStudent,
						IsTransport: fst.IsTransport,
						IsHostel: fst.IsHostel,
						IsLeft: fst.IsLeft,
						LeftMiti: fst.LeftMiti,
						LeftReason: fst.LeftReason,
						PointName: fst.PointName,
						RouteName: fst.RouteName,
						BoardersName: fst.BoardersName,
						CardNo: fst.CardNo,
						EnrollNo: fst.EnrollNo,
						LedgerPanaNo: fst.LedgerPanaNo,
						LastReceiptMiti: fst.LastReceiptMiti,
						LastReceiptNo: fst.LastReceiptNo,
						Email: fst.Email,
						HouseName: fst.HouseName,
						HouseDress: fst.HouseDress,
						VehicleName: fst.VehicleName,
						VehicleNumber: fst.VehicleNumber,
						ParentStudentId: fst.ParentStudentId,

						FutureDR: totalFutureDR,
						FutureCR: totalFutureCR,
						Opening: totalOpening,
						DrTotal: totalDrTotal,
						TotalCredit: totalCredit,
						CrDiscountAmt: crDiscountAmt,
						FutureDues: futureDues,
						TotalDues: totalDues,
						LastReceiptAmt: lastReceiptAmt,
						//Added
						ContactNo: fst.ContactNo,
						G_ContactNo: fst.G_ContactNo,
						StudentId: fst.StudentId,
					};

					$scope.dataForBottomGridSib.push(beData);


				});
				$scope.gridOptionsSib.api.setRowData($scope.dataForBottomGridSib);
				calculateBottomGridTotals();

				//var DataColl = mx(res.data.Data);
				//$scope.dataForBottomGrid[0].Opening = DataColl.sum(p1 => p1.Opening);
				//$scope.dataForBottomGrid[0].DrTotal = DataColl.sum(p1 => p1.DrTotal);
				//$scope.dataForBottomGrid[0].TotalCredit = DataColl.sum(p1 => p1.TotalCredit);
				//$scope.dataForBottomGrid[0].CrDiscountAmt = DataColl.sum(p1 => p1.CrDiscountAmt);
				//$scope.dataForBottomGrid[0].TotalDues = DataColl.sum(p1 => p1.TotalDues);

				//$scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);


				//$scope.gridOptions.api.setRowData(res.data.Data);
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}


	function calculateBottomGridTotals() {
		var totalRow = {
			Name: 'TOTAL =>',
			FutureDR: 0,
			FutureCR: 0,
			Opening: 0,
			DrTotal: 0,
			TotalCredit: 0,
			CrDiscountAmt: 0,
			FutureDues: 0,
			TotalDues: 0,
			LastReceiptAmt: 0
		};

		$scope.dataForBottomGridSib.forEach(item => {
			totalRow.FutureDR += item.FutureDR || 0;
			totalRow.FutureCR += item.FutureCR || 0;
			totalRow.Opening += item.Opening || 0;
			totalRow.DrTotal += item.DrTotal || 0;
			totalRow.TotalCredit += item.TotalCredit || 0;
			totalRow.CrDiscountAmt += item.CrDiscountAmt || 0;
			totalRow.FutureDues += item.FutureDues || 0;
			totalRow.TotalDues += item.TotalDues || 0;
			totalRow.LastReceiptAmt += item.LastReceiptAmt || 0;
		});

		// Update bottom grid with totals
		$scope.gridOptionsBottomSib.api.setRowData([totalRow]);
	}


	//New functions added from here for email send
	$scope.EmailTemplateChanged = function (st) {
		$scope.CurEmailSendFiltered.Temlate = st;
		$scope.CurEmailSendFiltered.Title = st.Title;
		$scope.CurEmailSendFiltered.Subject = st.Title;
		$scope.CurEmailSendFiltered.CC = st.CC;
		$scope.CurEmailSendFiltered.Description = st.Description;
	}

	$scope.SendEmailToFiltered = function () {
		myDropzone.removeAllFiles();
		var filteredDataCount = 0;
		$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
			filteredDataCount++;
		});

		if (filteredDataCount === 0) {
			Swal.fire('No data found after filter to send email');
			return;
		}

		$scope.filteredEmailStudentsCount = filteredDataCount;

		$scope.CurEmailSendFiltered = {
			Temlate: {},
			Description: '',
			Subject: '',
			Title: '',
			Primary: true,
			Father: false,
			Mother: false,
			Guardian: false,
			CC: '',
			DataColl: [],
			EmployeeColl: [],
			BCCEmployeeColl: []
		};

		// Get filtered data
		$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
			$scope.CurEmailSendFiltered.DataColl.push(node.data);
		});

		// Fetch Email templates
		var para1 = {
			EntityId: entityFeeSummarySMS,
			ForATS: 3,
			TemplateType: 2
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(para1)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmailTemplatesCollFiltered = res.data.Data;

				// Auto-fill if only one template
				if ($scope.EmailTemplatesCollFiltered.length === 1) {
					$scope.selectedEmailTemplateFiltered = $scope.EmailTemplatesCollFiltered[0];
				}
			} else {
				Swal.fire('No templates found for email');
			}

			$('#sendemail').modal('show');

		}, function (err) {
			Swal.fire('Failed to load templates');
		});
	};

	$scope.SendEmailToFilteredStudents = function () {
		if ($scope.CurEmailSendFiltered && $scope.CurEmailSendFiltered.DataColl && $scope.CurEmailSendFiltered.DataColl.length > 0) {

			var filesColl = []; // myDropzoneFiltered.files if you have dropzone for filtered

			var ccColl = [];
			if ($scope.CurEmailSendFiltered.EmployeeColl && $scope.CurEmailSendFiltered.EmployeeColl.length > 0) {
				angular.forEach($scope.CurEmailSendFiltered.EmployeeColl, function (emp) {
					if (emp.EmailId && emp.EmailId.length > 0)
						ccColl.push(emp.EmailId);
				});
			}

			var bccColl = [];
			if ($scope.CurEmailSendFiltered.BCCEmployeeColl && $scope.CurEmailSendFiltered.BCCEmployeeColl.length > 0) {
				angular.forEach($scope.CurEmailSendFiltered.BCCEmployeeColl, function (empbcc) {
					if (empbcc.EmailId && empbcc.EmailId.length > 0)
						bccColl.push(empbcc.EmailId);
				});
			}

			var emailDataColl = [];
			angular.forEach($scope.CurEmailSendFiltered.DataColl, function (objEntity) {
				var emailColl = [];
				if ($scope.CurEmailSendFiltered.Primary == true) {
					if (objEntity.Email && objEntity.Email.length > 0)
						emailColl.push(objEntity.Email);
				}
				if ($scope.CurEmailSendFiltered.Father == true) {
					if (objEntity.F_Email && objEntity.F_Email.length > 0)
						emailColl.push(objEntity.F_Email);
				}
				if ($scope.CurEmailSendFiltered.Mother == true) {
					if (objEntity.M_Email && objEntity.M_Email.length > 0)
						emailColl.push(objEntity.M_Email);
				}
				if ($scope.CurEmailSendFiltered.Guardian == true) {
					if (objEntity.G_Email && objEntity.G_Email.length > 0)
						emailColl.push(objEntity.G_Email);
				}

				if (emailColl.length > 0) {
					var msg = $scope.CurEmailSendFiltered.Description;
					for (let x in objEntity) {
						var variable = '$$' + x.toLowerCase() + '$$';
						if (msg.indexOf(variable) >= 0) {
							var val = objEntity[x];
							msg = msg.replace(variable, val);
						}

						if (msg.indexOf('$$') == -1)
							break;
					}

					var paraColl = [];
					paraColl.push({ Key: 'StudentId', Value: objEntity.StudentId });

					var newEmail = {
						EntityId: $scope.entity.FeeSummary,
						StudentId: objEntity.StudentId,
						UserId: 0,
						Title: $scope.CurEmailSendFiltered.Temlate.Title,
						Subject: $scope.CurEmailSendFiltered.Subject,
						Message: msg,
						CC: ccColl.toString(),
						BCC: bccColl.toString(),
						To: emailColl.toString(),
						StudentName: objEntity.Name,
						ParaColl: paraColl,
						FileName: 'student-form'
					};
					emailDataColl.push(newEmail);
				}
			});

			if (emailDataColl.length > 0) {

				$scope.loadingstatus = "running";
				showPleaseWait();

				$http({
					method: 'POST',
					url: base_url + "Global/SendEmail",
					headers: { 'Content-Type': undefined },
					transformRequest: function (data) {
						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));

						if (data.files) {
							for (var i = 0; i < data.files.length; i++) {
								if (data.files[i].File)
									formData.append("file" + i, data.files[i].File);
								else
									formData.append("file" + i, data.files[i]);
							}
						}
						return formData;
					},
					data: { jsonData: emailDataColl, files: filesColl }
				}).then(function (res) {
					$scope.loadingstatus = "stop";
					hidePleaseWait();
					Swal.fire(res.data.ResponseMSG);
					$('#sendemail').modal('hide');
				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					$('#sendemail').modal('hide');
				});
			}
		} else {
			Swal.fire('No Data found for email');
		}
	};

	$('#sendemail').on('hidden.bs.modal', function () {
		var scope = angular.element(this).scope();
		scope.$apply(function () {
			scope.CurEmailSendFiltered = {
				Temlate: {},
				Description: '',
				Subject: '',
				Title: '',
				Primary: true,
				Father: false,
				Mother: false,
				Guardian: false,
				CC: '',
				DataColl: [],
				EmployeeColl: [],
				BCCEmployeeColl: []
			};
		});

		// Clear all select2 inside modal
		$(this).find('.select2').val(null).trigger('change');
	});

	$scope.validateDate = function (obj, startField, endField, startLabel, endLabel) {
		var res = GlobalServices.validateDate(obj, startField, endField, startLabel, endLabel);
		if (res.IsSuccess == false) {
			Swal.fire({
				icon: 'warning',
				text: res.Message,
				confirmButtonText: 'OK'
			}).then(function () {
				obj.DateFrom_TMP = new Date();
				obj.DateTo_TMP = new Date();
				$scope.$applyAsync();
			});
		}
	};

});