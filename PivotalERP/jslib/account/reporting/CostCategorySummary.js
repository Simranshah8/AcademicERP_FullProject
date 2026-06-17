
app.controller('CostCategorySummController', function ($scope, $http, $timeout, $filter, GlobalServices, uiGridConstants) {
	$scope.Title = 'CostCategory Summary';
	getterAndSetter();

	$scope.onBtExportCSV = function () {
		var params = {
			filefield: 'costCategorySummary.csv',
			sheetfield: 'costCategorySummary'
		};

		$scope.gridOptions.api.exportDataAsCsv(params);
	}
	$scope.onFilterTextBoxChanged = function () {
		$scope.gridOptions.api.setQuickFilter($scope.search);
	}

	$scope.onColumnWiseFilter = function (colDef) {
		var valColl = [];
		valColl.push($scope.FilterByColumn);
		const instance = $scope.gridOptions.api.getFilterInstance(colDef.field);
		instance.setModel({ values: valColl });
		$scope.gridOptions.api.onFilterChanged();
	}

	function getterAndSetter() {


		$scope.gridOptions = [];
		$scope.ColumnColl = [
			 
			{ field: "CostCenterName", headerName: "Particulars", width: 250, filter: 'agTextColumnFilter' },
			{
				field: "Opening", headerName: "Opening", width: 180, filter: 'agNumberColumnFilter',
				valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }
			},
			{
				field: "TranDr", headerName: "Transaction Dr", width: 180, filter: 'agNumberColumnFilter',
				valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }
			},
			{
				field: "TranCr", headerName: "Transaction Cr", width: 180, filter: 'agNumberColumnFilter',
				valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }
			},

			{
				field: "Closing", headerName: "Closing", width: 180, filter: 'agNumberColumnFilter',
				valueFormatter: function (params) { return NumberformatAC(params.value); }, cellStyle: { 'text-align': 'right' }
			},
			 
			 
		];

		$scope.gridOptions = {
			//angularCompileRows: true,
			// a default column definition with properties that get applied to every column
			defaultColDef: {
				filter: true,
				resizable: true,
				sortable: true,

				// set every column width
				width: 90
			},
			headerHeight: 35,
			rowHeight:33,
			columnDefs: $scope.ColumnColl,
			enableColResize: true,
			rowData: null,
			filter: true,
			enableFilter: true,
			rowSelection: 'multiple',
			overlayLoadingTemplate: "Please Click the Load Button to display the data.",
			suppressHorizontalScroll: true,
			alignedGrids: [],
			onFilterChanged: function (e) {
				//console.log('onFilterChanged', e);
				var opening = 0, transactionDr = 0, transactionCr = 0, closingDr = 0, closingCr = 0, openingDr = 0, openingCr = 0;
				$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
					var tb = node.data;
					CostCenterName = 'TOTAL =>';
					opening += tb.Opening;
					transactionDr += tb.TranDr;
					transactionCr += tb.TranCr;
					closingDr += tb.Closing;
					 
				});
				
				
				$scope.dataForBottomGrid[0].Opening = opening;
				$scope.dataForBottomGrid[0].TranDr = transactionDr;
				$scope.dataForBottomGrid[0].TranCr = transactionCr;
				$scope.dataForBottomGrid[0].Closing = closingDr;
			 
				$scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);
				//console.log('gridApi.getFilterModel() =>', e.api.getFilterModel());
			},

		};


		$scope.dataForBottomGrid = [
			{
				Code: '',
				LedgerName: '',
				CostCenterName: 'Total =>',
				Opening: 0,
				TranDr: 0,
				TranCr: 0,
				Closing: 0,				
			},
		];

		$scope.gridOptionsBottom = {
			defaultColDef: {
				resizable: true,
				width: 90
			},
			rowHeight: 35,
			columnDefs: $scope.ColumnColl,
			// we are hard coding the data here, it's just for demo purposes
			rowData: $scope.dataForBottomGrid,
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
	};

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.filterValue = '';

		$scope.newLedgerwise = {
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
			branchId: 0,
			branchIdColl: ''
		};

		$scope.CostCategoryList = [];
		$http({
			method: 'GET',
			url: base_url + "Account/Creation/GetAllCostCategoryLisst",
			dataType: "json",
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CostCategoryList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$timeout(function () {
			$http({
				method: "GET",
				url: base_url + "Global/GetCompanyDetail",
				dataType: "json"
			}).then(function (res) {
				var comDet = res.data.Data;
				if (comDet) {
					$scope.newLedgerwise.DateFrom_TMP = new Date(comDet.StartDate);
					$scope.newLedgerwise.DateTo_TMP = new Date();
				}
			}, function (errormessage) {
				alert('Unable to Delete data. pls try again.' + errormessage.responseText);
			});
		});

		$scope.BranchList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.ClearLedgerwise = function () {
		$scope.newLedgerwise = {
			LedgerwiseId: null,

			Mode: 'Save'
		};

	};

	$scope.LoadReport = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.gridOptions.data = [];

		var dateFrom = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));
		var dateTo = new Date(($filter('date')(new Date(), 'yyyy-MM-dd')));

		if ($scope.newLedgerwise.DateFromDet)
			dateFrom = new Date(($filter('date')($scope.newLedgerwise.DateFromDet.dateAD, 'yyyy-MM-dd')));

		if ($scope.newLedgerwise.DateToDet)
			dateTo = new Date(($filter('date')($scope.newLedgerwise.DateToDet.dateAD, 'yyyy-MM-dd')));


		var para = {
			dateFrom: dateFrom,
			dateTo: dateTo,
			branchId: $scope.newLedgerwise.branchId,
			branchIdColl: $scope.newLedgerwise.branchIdColl
		};

		$http({
			method: 'POST',
			url: base_url + "Account/Reporting/GetCostCategorySummary",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: para }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			if (res.data.IsSuccess && res.data.Data) {
				var DataColl = mx(res.data.Data);
				$scope.dataForBottomGrid[0].Opening = DataColl.sum(p1 => p1.Opening);
				$scope.dataForBottomGrid[0].TranDr = DataColl.sum(p1 => p1.TranDr);
				$scope.dataForBottomGrid[0].TranCr = DataColl.sum(p1 => p1.TranCr);
				$scope.dataForBottomGrid[0].Closing = DataColl.sum(p1 => p1.Closing);
				
				$scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);
				$scope.gridOptions.api.setRowData(res.data.Data);
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

	};

	$scope.Print = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + EntityId + "&voucherId=0&isTran=false",
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
												url: base_url + "Account/Reporting/PrintTrailBalance",
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

													document.body.style.cursor = 'wait';
													document.getElementById("frmRpt").src = '';
													document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + EntityId + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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
							url: base_url + "Account/Reporting/PrintTrailBalance",
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

								document.body.style.cursor = 'wait';
								document.getElementById("frmRpt").src = '';
								document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + EntityId + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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

	$scope.DownloadAsXls = function () {

		$scope.loadingstatus = 'running';
		showPleaseWait();

		var dataColl = $scope.GetDataForPrint();

		var paraData = {
			Period: $scope.newLedgerwise.DateFromDet.dateBS + " TO " + $scope.newLedgerwise.DateToDet.dateBS,
		};

		$http({
			method: 'POST',
			url: base_url + "Global/PrintXlsReportData",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("entityId", EntityId);
				formData.append("jsonData", angular.toJson(data.jsonData));
				formData.append("paraData", angular.toJson(paraData));
				formData.append("RptPath", "");
				return formData;
			},
			data: { jsonData: dataColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			if (res.data.IsSuccess && res.data.Data) {
				down_file(base_url + "//" + res.data.Data.ResponseId, "CostCategorySummary.xlsx");
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(errormessage);
		});
	}

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