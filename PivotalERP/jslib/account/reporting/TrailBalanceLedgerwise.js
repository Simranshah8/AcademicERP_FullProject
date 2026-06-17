
app.controller('LedgerwiseController', function ($scope, $http, $timeout, $filter, GlobalServices, uiGridConstants) {
	$scope.Title = 'TrailBalance Ledgerwise';


	getterAndSetter();

	$scope.onBtExportCSV = function () {
		var params = {
			filefield: 'trailbalanace.csv',
			sheetfield: 'trailBalance'
		};

		$scope.gridOptions.api.exportDataAsCsv(params);
	}
	$scope.onFilterTextBoxChanged = function () {
		$scope.gridOptions.api.setQuickFilter($scope.search);
	}

	$scope.onColumnWiseFilter=function(colDef)
	{
		var valColl = [];
		valColl.push($scope.FilterByColumn);
		const instance = $scope.gridOptions.api.getFilterInstance(colDef.field);
		instance.setModel({ values: valColl });
		$scope.gridOptions.api.onFilterChanged();
	}

	function getterAndSetter() {


		$scope.gridOptions = [];
		$scope.ColumnColl = [

			{ field: "Code", headerName: "Code", width: 140, filter: 'agTextColumnFilter' },
			{ field: "LedgerName", headerName: "Particulars", width: 140, filter: 'agTextColumnFilter' },
			{ field: "LedgerGroupName", headerName: "Ledger Group", width: 140, filter: 'agTextColumnFilter' },
			{
				field: "Opening", headerName: "Opening", width: 120, filter: 'agNumberColumnFilter',
				valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }
			},
			{
				field: "TransactionDr", headerName: "Transaction Dr", width: 120, filter: 'agNumberColumnFilter',
				valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }
			},
			{
				field: "TransactionCr", headerName: "Transaction Cr", width: 120, filter: 'agNumberColumnFilter',
				valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }
			},
			
			{
				field: "ClosingDr", headerName: "Closing Dr", width: 120, filter: 'agNumberColumnFilter',
				valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }
			},
			{
				field: "ClosingCr", headerName: "Closing Cr", width: 120, filter: 'agNumberColumnFilter',
				valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }
			},
			{ field: "AreaName", headerName: "Area Name", width: 140, filter: 'agTextColumnFilter' },
			{ field: "AreaType", headerName: "Area Type", width: 140, filter: 'agTextColumnFilter' },
			{ field: "MobileNo1", headerName: "Mobile No.1", width: 140, filter: 'agTextColumnFilter' },
			{ field: "MobileNo2", headerName: "Mobile No.2", width: 140, filter: 'agTextColumnFilter' },
			{ field: "TelNo1", headerName: "Tel.No.1", width: 140, filter: 'agTextColumnFilter' },
			{ field: "TelNo2", headerName: "Tel.No.2", width: 140, filter: 'agTextColumnFilter' },
			{ field: "EmailId", headerName: "Email Id", width: 140, filter: 'agTextColumnFilter' },
			{ field: "Address", headerName: "Address", width: 140, filter: 'agTextColumnFilter' },
			{
				field: "TotalOpeningDr", headerName: "Opening Dr.", width: 120, filter: 'agNumberColumnFilter',
				valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }
			},
			{
				field: "TotalOpeningCr", headerName: "Opening Cr.", width: 120, filter: 'agNumberColumnFilter',
				valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }
			}			
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
			columnDefs: $scope.ColumnColl,
			enableColResize: true,
			rowData: null,
			filter: true,
			enableFilter: true,
			rowSelection: 'multiple',
			suppressHorizontalScroll: true,
			alignedGrids: [],
			onFilterChanged: function (e) {
				//console.log('onFilterChanged', e);
				var opening = 0, transactionDr = 0, transactionCr = 0, closingDr = 0, closingCr = 0,openingDr=0,openingCr=0;
				$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
					var tb = node.data;
					opening += tb.Opening;
					transactionDr += tb.TransactionDr;
					transactionCr += tb.TransactionCr;
					closingDr += tb.ClosingDr;
					closingCr += tb.ClosingCr;
					openingDr += tb.OpeningDr;
					openingCr += tb.OpeningCr;
				});

				$scope.dataForBottomGrid[0].Opening = opening;
				$scope.dataForBottomGrid[0].TransactionDr = transactionDr;
				$scope.dataForBottomGrid[0].TransactionCr = transactionCr;
				$scope.dataForBottomGrid[0].ClosingDr = closingDr;
				$scope.dataForBottomGrid[0].ClosingCr = closingCr;
				$scope.dataForBottomGrid[0].TotalOpeningDr = openingDr;
				$scope.dataForBottomGrid[0].TotalOpeningCr = openingCr;
				$scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);
				//console.log('gridApi.getFilterModel() =>', e.api.getFilterModel());
			},

		};


		$scope.dataForBottomGrid = [
			{
				Code: '',
				LedgerName: '',
				LedgerGroupName: 'Total =>',
				Opening: 0,
				TransactionDr: 0,
				TransactionCr: 0,
				ClosingDr: 0,
				ClosingCr: 0,
				AreaName:'',
				AreaType: '',
				MobileNo1: '',
				MobileNo2: '',
				TelNo1: '',
				TelNo2: '',
				EmailId: '',
				Address: '',
				TotalOpeningDr: 0,
				TotalOpeningCr:0
			},			
		];

		$scope.gridOptionsBottom = {
			defaultColDef: {
				resizable: true,
				width: 90
			},
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
			branchIdColl:''
		};

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
		 
		var para = {
			dateFrom: $scope.newLedgerwise.DateFromDet ? $filter('date')(new Date($scope.newLedgerwise.DateFromDet.dateAD), 'yyyy-MM-dd')  : null,
			dateTo: $scope.newLedgerwise.DateToDet ? $filter('date')(new Date($scope.newLedgerwise.DateToDet.dateAD), 'yyyy-MM-dd')  : null,
			branchId: $scope.newLedgerwise.branchId,
			branchIdColl: $scope.newLedgerwise.branchIdColl
		};

		$http({
			method: 'POST',
			url: base_url + "Account/Reporting/GetTBLedgerWise",
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
				$scope.dataForBottomGrid[0].TransactionDr = DataColl.sum(p1 => p1.TransactionDr);
				$scope.dataForBottomGrid[0].TransactionCr = DataColl.sum(p1 => p1.TransactionCr);
				$scope.dataForBottomGrid[0].ClosingDr = DataColl.sum(p1 => p1.ClosingDr);
				$scope.dataForBottomGrid[0].ClosingCr = DataColl.sum(p1 => p1.ClosingCr);
				$scope.dataForBottomGrid[0].TotalOpeningDr = DataColl.sum(p1 => p1.TotalOpeningDr);
				$scope.dataForBottomGrid[0].TotalOpeningCr = DataColl.sum(p1 => p1.TotalOpeningCr);

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

													$scope.loadingstatus = "stop";
						 								 var rptPara = {
                                                        rpttranid: rptTranId,
                                                        istransaction: false,
                                                        entityid: EntityId,
                                                        voucherid: 0,
                                                        tranid: 0,
                                                        vouchertype: 0,
                                                        sessionid: res.data.Data.ResponseId,
                                                        Period: $scope.newLedgerwise.DateFromDet.dateBS + " TO " + $scope.newLedgerwise.DateToDet.dateBS,                                                        
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

									 var rptPara = {
                                                        rpttranid: rptTranId,
                                                        istransaction: false,
                                                        entityid: EntityId,
                                                        voucherid: 0,
                                                        tranid: 0,
                                                        vouchertype: 0,
                                                        sessionid: res.data.Data.ResponseId,
                                                        Period: $scope.newLedgerwise.DateFromDet.dateBS + " TO " + $scope.newLedgerwise.DateToDet.dateBS,                                                        
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

});