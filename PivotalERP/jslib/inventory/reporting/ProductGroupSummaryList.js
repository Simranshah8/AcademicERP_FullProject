app.controller('ProductGroupSummaryAsListController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Product Group Summary';

	getterAndSetter();

	$scope.onBtExportCSV = function () {
		var params = {
			filefield: 'productGroupSummary.csv',
			sheetfield: 'productGroupSummary'
		};

		$scope.gridOptions.api.exportDataAsCsv(params);
	}
	$scope.onFilterTextBoxChanged = function () {
		$scope.gridOptions.api.setQuickFilter($scope.search);
	}

	function getterAndSetter() {

		$scope.columnDefs = [
			{
				headerName: "ProductName", field: "ProductName", cellRenderer: 'group', width: 230, filter: "agTextColumnFilter",			 
			},
			{ headerName: "Code", field: "ProductCode", width: 120, filter: "agTextColumnFilter" },
			{ headerName: "Alias", field: "ProductAlias", width: 110, filter: "agTextColumnFilter" },
			{
				headerName: "Group", field: "ProductGroupName", cellRenderer: 'group', width: 180, filter: "agTextColumnFilter",
			},
			{
				headerName: "Category", field: "ProductCategoriesName", cellRenderer: 'group', width: 180, filter: "agTextColumnFilter",
			},
			{ headerName: "Opening Qty.", field: "OpeningQty", width: 140, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, },
			{
				headerName: "Opening Rate", width: 140, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' },
				valueGetter: function (params) {
					var beData = params.data;
					var val = 0;

					if (beData.OpeningAmt != 0 && beData.OpeningQty != 0)
						val = (beData.OpeningAmt / beData.OpeningQty);

					return val;
				}

			},

			{ headerName: "Opening Amt.", field: "OpeningAmt", width: 130, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, },
			{ headerName: "In Qty.", field: "InQty", width: 130, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, },
			{
				headerName: "In Rate", width: 140, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' },
				valueGetter: function (params) {
					var beData = params.data;
					var val = 0;

					if (beData.InAmt != 0 && beData.InQty != 0)
						val = beData.InAmt / beData.InQty;

					return val;
				}

			},
			{ headerName: "In Amt.", field: "InAmt", width: 130, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, },
			{ headerName: "Out Qty.", field: "OutQty", width: 130, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, },
			{
				headerName: "Out Rate", width: 140, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' },
				valueGetter: function (params) {
					var beData = params.data;
					var val = 0;

					if (beData.OutAmt != 0 && beData.OutQty != 0)
						val = beData.OutAmt / beData.OutQty;

					return val;
				}

			},
			{ headerName: "Out Amt.", field: "OutAmt", width: 130, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, },
			{ headerName: "Balance Qty.", field: "BalanceQty", width: 130, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, },
			{ headerName: "Unit", field: "BaseUnit", width: 110 },
			{ headerName: "Balance Rate", field: "BalanceRate", width: 140, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, },
			{ headerName: "Balance Amt.", field: "BalanceAmt", width: 140, filter: "agNumberColumnFilter", valueFormatter: function (params) { return Numberformat(params.value); }, cellStyle: { 'text-align': 'right' }, },
			{ headerName: "Godown", field: "GodownName", width: 120, filter: "agTextColumnFilter" },
			{ headerName: "Rack", field: "Rack", width: 120, filter: "agTextColumnFilter" },
			{ headerName: "RackDescription", field: "RackDescription", width: 120, filter: "agTextColumnFilter" },
			{ headerName: "ProductType", field: "ProductType", width: 120, filter: "agTextColumnFilter" },			

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
		rowHeight:33,
			enableColResize: true,
			rowData: null,
			filter: true,
			enableFilter: true,
			rowSelection: 'multiple',
			 
		};

		// lookup the container we want the Grid to use
		//var eGridDiv = document.querySelector('#myGrid1');

		// create the grid passing in the div to use together with the columns & data we want to use
		//new agGrid.Grid(eGridDiv, $scope.gridOptions);
	}


	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.filterValue = '';
		$scope.confirmMSG = GlobalServices.getConfirmMSG();

		$scope.newProductGroupSummary = {
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
			GroupId: 1,
			BranchId: 0,
			ShowZeroBalance: false
		};
		$timeout(function () {
			$http({
				method: "GET",
				url: base_url + "Global/GetCompanyDetail",
				dataType: "json"
			}).then(function (res) {
				var comDet = res.data.Data;
				if (comDet) {
					$scope.newProductGroupSummary.DateFrom_TMP = new Date(comDet.StartDate);
					$scope.newProductGroupSummary.DateTo_TMP = new Date();
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

		$timeout(function () {
			$scope.ProductGroupList = [];
			$http({
				method: 'POST',
				url: base_url + "Inventory/Creation/GetAllProductGroupList",
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.ProductGroupList = res.data.Data;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});

	};

	$scope.ClearProductGroupSummary = function () {
		$scope.newProductGroupSummary = {
			ProductGroupSummaryId: null,
			Mode: 'Save'
		};
	};

	$scope.LoadReport = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.gridOptions.data = [];

		var para = {
			dateFrom: $scope.newProductGroupSummary.DateFromDet ? $scope.newProductGroupSummary.DateFromDet.dateAD : null,
			dateTo: $scope.newProductGroupSummary.DateToDet ? $scope.newProductGroupSummary.DateToDet.dateAD : null,
			branchId: $scope.newProductGroupSummary.branchId,
			branchIdColl: $scope.newProductGroupSummary.branchIdColl,
			groupId: $scope.newProductGroupSummary.GroupId,
			showZeroBalance: $scope.newProductGroupSummary.ShowZeroBalance
		};

		$http({
			method: 'POST',
			url: base_url + "Inventory/Reporting/GetProductGroupSummaryAsList",
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
												url: base_url + "Account/Reporting/PrintProductGroupSummary",
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
							url: base_url + "Account/Reporting/PrintProductGroupSummary",
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
			Period: $scope.newProductGroupSummary.DateFromDet.dateBS + " TO " + $scope.newProductGroupSummary.DateToDet.dateBS,
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
				down_file(base_url + "//" + res.data.Data.ResponseId, "newProductGroupSummary.xlsx");
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(errormessage);
		});
	}


});