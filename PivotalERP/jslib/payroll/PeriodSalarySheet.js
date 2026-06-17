app.controller('PeriodSalarySheetController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'PeriodSalarySheet';

	getterAndSetter();
	var gSrv = GlobalServices;
	$scope.onBtExportCSV = function () {
		var params = {
			filefield: 'profitloss.csv',
			sheetfield: 'profitloss'
		};
		$scope.gridOptions.api.exportDataAsCsv(params);
	}
	$scope.onFilterTextBoxChanged = function () {
		// Apply the quick filter based on the search term
		$scope.gridOptions.api.setQuickFilter($scope.search);
		// Recalculate totals for the currently filtered data
		recalculateTotals();
	};

	// Function to recalculate totals based on the filtered rows
	function recalculateTotals() {
		let totals = {};
		const payHeadings = Object.entries($scope.gridOptions.columnApi.getAllColumns().filter(col => col.getColId() !== "EmployeeCode" && col.getColId() !== "EmployeeName" && col.getColId() !== "Department" && col.getColId() !== "Category" && col.getColId() !== "Designation" && col.getColId() !== "BranchName"));
		// Initialize totals for each pay heading
		payHeadings.forEach(([id]) => {
			totals[id] = 0;
		});
		totals.Earning = 0;
		totals.Deducation = 0;
		totals.Tax = 0;
		totals.Netpayable = 0;
		// Iterate through the visible rows and sum the amounts
		const allRows = $scope.gridOptions.api.getModel().rowsToDisplay;
		allRows.forEach(rowNode => {
			const rowData = rowNode.data;
			// Update totals for each pay heading
			payHeadings.forEach(([id]) => {
				totals[id] += rowData[id] || 0;
			});
			// Update overall totals
			totals.Earning += rowData.Earning || 0;
			totals.Deducation += rowData.Deducation || 0;
			totals.Tax += rowData.Tax || 0;
			totals.Netpayable += rowData.Netpayable || 0;
		});

		// Update the summary row with the new totals
		const summaryRow = {
			EmployeeCode: "Total",
			EmployeeName: "",
			Department: "",
			Category: "",
			Designation: "",
			BranchName: "",
			Earning: totals.Earning,
			Deducation: totals.Deducation,
			Tax: totals.Tax,
			Netpayable: totals.Netpayable
		};

		// Add totals for dynamic columns (sum of amounts per pay heading)
		payHeadings.forEach(([id]) => {
			summaryRow[id] = totals[id];
		});

		// Set the updated summary row at the bottom
		$scope.gridOptions.api.setPinnedBottomRowData([summaryRow]);
	}


	function getterAndSetter() {
		$scope.columnDefs = [
			{ headerName: "Emp Code", width: 120, field: "EmployeeCode", dataType: 'Text', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'left' } },
			{ headerName: "Employee Name", width: 250, field: "EmployeeName", dataType: 'Text', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'left' } },
			{ headerName: "PanNo", width: 140, field: "PanNo", dataType: 'Text', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'left' } },
			{ headerName: "Department", width: 140, field: "Department", dataType: 'Text', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'left' } },
			{ headerName: "Category", width: 140, field: "Category", dataType: 'Text', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'left' } },
			{ headerName: "Designation", width: 140, field: "Designation", dataType: 'Text', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'left' } },
			{ headerName: "PayheadingName", width: 200, field: "PayHeading", dataType: 'Text', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'left' } },
			{ headerName: "Amount", width: 110, field: "Amount", dataType: 'Number', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'right' } },
			{ headerName: "Rate", width: 110, field: "Rate", dataType: 'Number', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'right' } },
			{ headerName: "Earning", width: 110, field: "Earning", dataType: 'Number', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'right' } },
			{ headerName: "Deducation", width: 110, field: "Deducation", dataType: 'Number', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'right' } },
			{ headerName: "Tax", width: 110, field: "Tax", dataType: 'Number', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'right' } },
			{ headerName: "Netpayable", width: 110, field: "Netpayable", dataType: 'Number', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'right' } },
			{ headerName: "PayHeadType", width: 110, field: "PayHeadType", dataType: 'Text', filter: 'agTextColumnFilter', cellStyle: { 'text-align': 'left' } },
		];
		// let the grid know which columns and what data to use
		$scope.gridOptions = {
			columnDefs: $scope.columnDefs,
			defaultColDef: {
				resizable: true,
				sortable: true,
				filter: true,
				resizable: true,
				cellStyle: { 'line-height': '31px' },
				rowSelection: 'multiple'
			},
			headerHeight: 35,
			rowHeight: 33,
			enableColResize: true,
			overlayLoadingTemplate: "Please Click the Load Button to display the data.",
			rowData: null,
			filter: true,
			enableFilter: true,
			rowSelection: 'multiple',
			getNodeChildDetails: function (beData) {
				if (beData.ChieldsCOll && beData.ChieldsCOll.length > 0) {
					return {
						group: true,
						children: beData.ChieldsCOll,
						expanded: beData.open
					};
				} else
					return null;
			}
		};
	}

	$scope.LoadData = function () {
		$scope.filterValue = '';
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		$scope.ForEmployeeColl = [{ id: 0, text: 'Continue' }, { id: 1, text: 'Left' }];
		$scope.currentPages = {
			SalaryDetail: 1,
		};
		$scope.searchData = {
			SalaryDetail: '',
		};
		$scope.perPage = {
			SalaryDetail: GlobalServices.getPerPageRow(),
		};

		$scope.AcademicYearColl = [];
		$scope.AcademicYearColl = gSrv.getYearList();

		$scope.BranchList = [];
		$http({
			method: 'GET',
			url: base_url + "Attendance/Transaction/GetBranchListforPayhead",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.DepartmentList = [];
		gSrv.getDepartmentList().then(function (res) {
			$scope.DepartmentList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.CategoryList = [];
		gSrv.getCategoryList().then(function (res) {
			$scope.CategoryList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.newPeriodSalary = {
			SalaryDetailId: null,
			BranchId: null,
			DepartmentId: null,
			Mode: 'Save'
		};
	}



	$scope.ExportPeriodSalarySheetExcel = function () {
		var params = {
			fileName: 'PeriodSalarySheet.csv',
			sheetName: 'PeriodSalarySheet'
		};
		$scope.gridOptions.api.exportDataAsCsv(params);
	}

	$scope.GetPeriodSalarySheet = function () {
		if (!$scope.newPeriodSalary.FromYearId) {
			Swal.fire('Please select "From Year".');
			return;
		}
		if (!$scope.newPeriodSalary.FromMonthId) {
			Swal.fire('Please select "From Month".');
			return;
		}
		if (!$scope.newPeriodSalary.ToYearId) {
			Swal.fire('Please select "To Year".');
			return;
		}
		if (!$scope.newPeriodSalary.ToMonthId) {
			Swal.fire('Please select "To Month".');
			return;
		}
		$scope.loadingstatus = "running";
		showPleaseWait();

		const para = {
			FromYearId: $scope.newPeriodSalary.FromYearId,
			FromMonthId: $scope.newPeriodSalary.FromMonthId,
			ToYearId: $scope.newPeriodSalary.ToYearId,
			ToMonthId: $scope.newPeriodSalary.ToMonthId,
			BranchId: $scope.newPeriodSalary.BranchId,
			DepartmentId: $scope.newPeriodSalary.DepartmentId,
			CategoryId: $scope.newPeriodSalary.CategoryId
		};

		$http.post(base_url + "Attendance/Report/GetAllPeriodSalarySheet", JSON.stringify(para))
			.then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

				if (res.data.IsSuccess && res.data.Data) {
					$scope.DataColl = res.data.Data;

					// Step 1: Collect unique PayHeadings, their IDs, and SNo for dynamic columns
					const payHeadingsMap = {};
					$scope.DataColl.forEach(function (item) {
						if (!payHeadingsMap[item.PayHeadingId]) {
							payHeadingsMap[item.PayHeadingId] = {
								name: item.PayHeading,
								sno: item.PayHeadingSNo
							};
						}
					});

					// Convert the map to an array of entries and sort by SNo
					const payHeadings = [];
					Object.entries(payHeadingsMap).forEach(function ([id, value]) {
						payHeadings.push({
							id: id,
							name: value.name,
							sno: value.sno
						});
					});

					payHeadings.sort(function (a, b) {
						return a.sno - b.sno;
					});

					// Step 2: Build dynamic column definitions
					const dynamicColumnDefs = [
						{ headerName: "Emp Code", field: "EmployeeCode", width: 120, pinned: 'left' },
						{ headerName: "Employee Name", field: "EmployeeName", width: 200, pinned: 'left' },
						{ headerName: "PanNo", field: "PanNo", width: 120 },
						{ headerName: "Department", field: "Department", width: 140 },
						{ headerName: "Category", field: "Category", width: 140 },
						{ headerName: "Designation", field: "Designation", width: 140 },
						{ headerName: "Branch Name", field: "BranchName", width: 140 }
					];

					payHeadings.forEach(function (payHeading) {
						dynamicColumnDefs.push({
							headerName: payHeading.name,
							field: payHeading.id, // Use PayHeadingId as the field name
							width: 160,
							valueFormatter: function (params) { return Numberformat(params.value); },
							cellStyle: { 'text-align': 'right' }
						});
					});

					dynamicColumnDefs.push(
						{ headerName: "Earning", field: "Earning", width: 160, valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
						{ headerName: "Deduction", field: "Deducation", width: 160, valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
						{ headerName: "Tax", field: "Tax", width: 160, valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } },
						{ headerName: "Net Payable", field: "Netpayable", width: 160, valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' } }
					);

					$scope.gridOptions.api.setColumnDefs(dynamicColumnDefs);

					// Step 3: Calculate totals and build ordered data
					let totals = {};
					payHeadings.forEach(function (payHeading) {
						totals[payHeading.id] = 0; // Initialize totals for each pay heading
					});
					totals.Earning = 0;
					totals.Deducation = 0;
					totals.Tax = 0;
					totals.Netpayable = 0;

					const orderedData = $scope.DataColl.reduce(function (acc, item) {
						let row = acc.find(function (row) { return row.EmployeeCode === item.EmployeeCode; });
						if (!row) {
							row = {
								EmployeeCode: item.EmployeeCode,
								EmployeeName: item.EmployeeName,
								Department: item.Department,
								Designation: item.Designation,
								BranchName: item.BranchName,
								Category: item.Category,
								PanNo: item.PanNo,
							};
							acc.push(row);
						}

						// Assign dynamic pay heading amounts
						row[item.PayHeadingId] = item.Amount;

						// Push values for Earning, Deduction, Tax, and NetPayable if they exist
						if ('Earning' in item) {
							row.Earning = (row.Earning || 0) + item.Earning;
							totals.Earning += item.Earning;
						}
						if ('Deducation' in item) {
							row.Deducation = (row.Deducation || 0) + item.Deducation;
							totals.Deducation += item.Deducation;
						}
						if ('Tax' in item) {
							row.Tax = (row.Tax || 0) + item.Tax;
							totals.Tax += item.Tax;
						}
						if ('Netpayable' in item) {
							row.Netpayable = (row.Netpayable || 0) + item.Netpayable;
							totals.Netpayable += item.Netpayable;
						}

						// Update totals for dynamic pay headings
						if (item.PayHeadingId) {
							totals[item.PayHeadingId] += item.Amount || 0;
						}

						return acc;
					}, []);

					// Step 4: Add totals row (pinned to the bottom)
					const summaryRow = {
						EmployeeCode: "",
						EmployeeName: "Total",
						Department: "",
						Category: "",
						Designation: "",
						BranchName: "",
						Earning: totals.Earning || 0,
						Deducation: totals.Deducation || 0,
						Tax: totals.Tax || 0,
						Netpayable: totals.Netpayable || 0
					};

					payHeadings.forEach(function (payHeading) {
						summaryRow[payHeading.id] = totals[payHeading.id];
					});

					$scope.gridOptions.api.setPinnedBottomRowData([summaryRow]);
					$scope.gridOptions.api.setRowData(orderedData);
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			})
			.catch(function (reason) {
				hidePleaseWait();
				Swal.fire('Failed: ' + (reason.message || reason));
			});
	};


	$scope.onBtExportCSV = function () {
		var params = {
			fileName: 'PeriodSalarySheet.csv',
			sheetName: 'PeriodSalarySheet'
		};

		$scope.gridOptions.api.exportDataAsCsv(params);
	}



	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});