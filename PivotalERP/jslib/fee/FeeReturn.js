

app.controller('FeeReturnController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate, uiGridConstants, uiGridTreeViewConstants) {
	$scope.Title = 'Fee Return';

	/*OnClickDefault();*/


	$rootScope.ConfigFunction = function () {
		var keyColl = $translate.getTranslationTable();

		var Labels = {
			RegdNo: keyColl['REGDNO_LNG'],
			Cast: keyColl['CAST_LNG'],
		};

		$scope.gridApi.grid.getColumn('RegdNo').colDef.displayName = Labels.RegdNo;
		$scope.gridApi.grid.getColumn('RegdNo').displayName = Labels.RegdNo;


		if ($rootScope.LANG == 'in') {

			var findInd = -1;
			findInd = $scope.gridFRCollection.columnDefs.findIndex(function (obj) { return obj.name == 'DOB_BS' });
			if (findInd != -1)
				$scope.gridFRCollection.columnDefs.splice(findInd, 1);
		}

		$scope.LoadData();

		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;

			if ($scope.AcademicConfig.ActiveFaculty == false) {

				findInd = $scope.gridFRCollection.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' });
				if (findInd != -1)
					$scope.gridFRCollection.columnDefs.splice(findInd, 1);


			}

			if ($scope.AcademicConfig.ActiveLevel == false) {

				findInd = $scope.gridFRCollection.columnDefs.findIndex(function (obj) { return obj.name == 'Level' });
				if (findInd != -1)
					$scope.gridFRCollection.columnDefs.splice(findInd, 1);


			}

			if ($scope.AcademicConfig.ActiveSemester == false) {

				findInd = $scope.gridFRCollection.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' });
				if (findInd != -1)
					$scope.gridFRCollection.columnDefs.splice(findInd, 1);


			}

			if ($scope.AcademicConfig.ActiveBatch == false) {
				findInd = $scope.gridFRCollection.columnDefs.findIndex(function (obj) { return obj.name == 'Batch' });
				if (findInd != -1)
					$scope.gridFRCollection.columnDefs.splice(findInd, 1);


			}

			if ($scope.AcademicConfig.ActiveClassYear == false) {

				findInd = $scope.gridFRCollection.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' });
				if (findInd != -1)
					$scope.gridFRCollection.columnDefs.splice(findInd, 1);


			}


			$scope.gridApi.grid.refresh();

			$scope.getRptState();


		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
	$rootScope.ChangeLanguage();

	$scope.LoadData = function () {
		$scope.entity = {
			FeeReceipt: 497
		};

		$scope.SelectedCostClass = null;
		$scope.CostClassColl = [];

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.MonthList = [];
		GlobalServices.getAcademicMonthList(null, null).then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();

		$scope.currentPages = {
			FeeReceipt: 1,
			FeeReceiptCollection: 1,

			TransferFeeReceiptToAccount: 1
		};

		$scope.searchData = {
			FeeReceipt: '',
			FeeReceiptCollection: '',

			TransferFeeReceiptToAccount: ''
		};

		$scope.perPage = {
			FeeReceipt: GlobalServices.getPerPageRow(),
			FeeReceiptCollection: GlobalServices.getPerPageRow(),

			TransferFeeReceiptToAccount: GlobalServices.getPerPageRow()
		};

		$scope.UDFFeildsColl = [];
		$http({
			method: 'GET',
			url: base_url + "Setup/Security/GetAllUDFFields?entityId=" + EntityId,
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UDFFeildsColl = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$timeout(function () {
			$http({
				method: 'POST',
				url: base_url + "Fee/Transaction/GetFeeConfiguration",
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$timeout(function () {
						$scope.FeeConfig = res.data.Data;

						if ($scope.FeeConfig.FeeReceiptLedgerId > 0)
							$scope.newFeeReceipt.ReceiptAsLedgerId = $scope.FeeConfig.FeeReceiptLedgerId;
					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});

		$scope.newFeeReceipt = {
			TranId: null,
			AutoNumber: 0,
			AutoManualNo: '',
			RefNo: '',
			Date_TMP: new Date(),
			PreviousDues: 0,
			CurrentDues: 0,
			TotalDues: 0,
			DiscountAmt: 0,
			DiscountPer: 0,
			FineAmt: 0,
			ReceivableAmt: 0,
			ReceivedAmt: 0,
			AfterReceivedDues: 0,
			AdvanceAmt: 0,
			CostClassId: null,
			AcademicYearId: null,
			Narration: '',
			StudentId: null,
			ClassId: null,
			StudentName: '',
			DetailsColl: [],
			SelectStudent: $scope.StudentSearchOptions[0].value,
			ReceiptAsLedgerId: 1,
			Mode: 'Save'
		};

		$scope.newFeeReceiptCollection = {
			FeeReceiptCollectionId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
			ShowCancel: false,
			Mode: 'Save'
		};



		$scope.newTransferFeeReceiptToAccount = {
			TransferFeeReceiptToAccountId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.FeeItemList = [];
		GlobalServices.getFeeItemList().then(function (res1) {
			$scope.FeeItemList = res1.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllFeeReceiptCollectionList();


		$http({
			method: 'GET',
			url: base_url + "Account/Creation/GetCostClassForEntry",
			dataType: "json"
		}).then(function (res1) {
			if (res1.data.IsSuccess && res1.data.Data) {
				$scope.CostClassColl = res1.data.Data;

				if ($scope.CostClassColl && $scope.CostClassColl.length > 0)
					$scope.SelectedCostClass = $scope.CostClassColl[0];

				$timeout(function () {
					$scope.$apply(function () {

						$scope.getVoucherNo();
					});
				});


			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	}

	$scope.LoadDefaultMonth = function () {

		$timeout(function () {
			if ($scope.newFeeReceipt.DateDet) {

				if ($rootScope.LANG == 'in')
					$scope.newFeeReceipt.PaidUpToMonth = new Date($scope.newFeeReceipt.DateDet.dateAD).getMonth() + 1;
				else
					$scope.newFeeReceipt.PaidUpToMonth = $scope.newFeeReceipt.DateDet.NM;
				//$scope.newFeeReceipt.PaidUpToMonth = 13;
			}
		});
	}
	$scope.getVoucherNo = function () {

		$scope.newFeeReceipt.AutoNumber = 0;
		$scope.newFeeReceipt.AutoManualNo = '';

		if ($scope.SelectedCostClass) {
			var para = {
				CostClassId: $scope.SelectedCostClass.CostClassId,
				AcademicYearId: null,
			};
			$http({
				method: 'POST',
				url: base_url + "Fee/Transaction/GetAutoNoOfFeeReturn",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					var vNo = res.data.Data;

					if (vNo.IsSuccess == true) {
						$scope.newFeeReceipt.AutoNumber = vNo.AutoNumber;
						$scope.newFeeReceipt.AutoManualNo = vNo.AutoManualNo;
					}

				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.CurDues = null;
	$scope.DefaultPhoto = '/wwwroot/dynamic/images/avatar-img.jpg';
	$scope.getDuesDetails = function () {
		$scope.CurDues = null;
		$scope.newFeeReceipt.ReceivableAmt = 0;
		$scope.newFeeReceipt.ReceivedAmt = 0;
		$scope.newFeeReceipt.TenderAmount = 0;
		$scope.newFeeReceipt.AfterReceivedDues = 0;
		$scope.newFeeReceipt.Narration = '';

		if ($scope.SelectedCostClass && $scope.newFeeReceipt.StudentId && $scope.newFeeReceipt.StudentId > 0) {

			GlobalServices.getAcademicMonthList($scope.newFeeReceipt.StudentId, null).then(function (resAM) {
				$scope.MonthList = [];
				angular.forEach(resAM.data.Data, function (m) {
					$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
				});

				var para = {
					StudentId: $scope.newFeeReceipt.StudentId,
					PaidUpToMonth: $scope.newFeeReceipt.PaidUpToMonth,
					PaidUpMonthColl: '',
					SemesterId: ($scope.newFeeReceipt.StudentDetails ? $scope.newFeeReceipt.StudentDetails.SemesterId : null),
					ClassYearId: ($scope.newFeeReceipt.StudentDetails ? $scope.newFeeReceipt.StudentDetails.ClassYearId : null)
				};
				$http({
					method: 'POST',
					url: base_url + "Fee/Transaction/GetDuesForReturn",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						$scope.CurDues = res.data.Data;

						$scope.CurDues.PaymentModeColl = [];
						$scope.CurDues.PaymentModeColl.push({
							LedgerId: 0,
							Amount: 0,
							Remarks: ''
						});
						if (!$scope.CurDues.PhotoPath || $scope.CurDues.PhotoPath.length == 0)
							$scope.CurDues.PhotoPath = $scope.DefaultPhoto;

						if ($scope.CurDues.FeeItemWiseDuesColl && $scope.CurDues.FeeItemWiseDuesColl.length > 0) {
							$scope.Calculation($scope.CurDues.FeeItemWiseDuesColl[0], 3);
							$scope.CalculationOnTotal(1);
						}

						if ($scope.CurDues.ReceiptColl) {
							var totalAmt = mx($scope.CurDues.ReceiptColl).sum(p1 => p1.ReceivedAmt + p1.DiscountAmt);
							var diffAmt = $scope.CurDues.TotalCredit - totalAmt;

							if (diffAmt > 0) {
								$scope.CurDues.ReceiptColl.splice(0, 0, {
									VoucherDate_BS: '',
									AutoManualNo: 'Opening Advance',
									ReceivedAmt: diffAmt
								});
							}
						}

						$scope.CurDues.ManualFeeDetailsColl = [];
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			});
		}

	}
	$scope.Calculation = function (sRow, col) {
		var disAmt = 0, disPer = 0;
		if (col == 1) {
			if (sRow.DiscountPer > 0)
				disAmt = Number.parseFloat((sRow.TotalDues * sRow.DiscountPer / 100).toFixed(2));

			sRow.DiscountAmt = disAmt;
		} else if (col == 2) {
			if (sRow.DiscountAmt > 0)
				disPer = Number.parseFloat(((sRow.DiscountAmt / sRow.TotalDues) * 100).toFixed(3));

			disAmt = sRow.DiscountAmt;
			sRow.DiscountPer = disPer;
		} else
			disAmt = sRow.DiscountAmt;

		sRow.ReceivableAmt = sRow.TotalDues - disAmt;
		sRow.AfterReceivedDues = sRow.ReceivableAmt - sRow.ReceivedAmt;

		var query = mx($scope.CurDues.FeeItemWiseDuesColl);
		$scope.newFeeReceipt.DiscountPer = 0;
		$scope.newFeeReceipt.ReceivableAmt = Number.parseFloat(query.sum(p1 => p1.ReceivableAmt).toFixed(2));
		$scope.newFeeReceipt.DiscountAmt = Number.parseFloat(query.sum(p1 => p1.DiscountAmt).toFixed(2));
		$scope.newFeeReceipt.FineAmt = Number.parseFloat(query.sum(p1 => p1.FineAmt).toFixed(2));
		$scope.newFeeReceipt.ReceivedAmt = Number.parseFloat(query.sum(p1 => p1.ReceivedAmt).toFixed(2));

		var query1 = mx($scope.CurDues.ManualFeeDetailsColl);
		$scope.newFeeReceipt.ReceivableAmt = $scope.newFeeReceipt.ReceivableAmt + Number.parseFloat(query1.sum(p1 => p1.ReceivableAmt).toFixed(2));
		$scope.newFeeReceipt.DiscountAmt = $scope.newFeeReceipt.DiscountAmt + Number.parseFloat(query1.sum(p1 => p1.DiscountAmt).toFixed(2));
		$scope.newFeeReceipt.FineAmt = $scope.newFeeReceipt.FineAmt + Number.parseFloat(query1.sum(p1 => p1.FineAmt).toFixed(2));
		$scope.newFeeReceipt.ReceivedAmt = $scope.newFeeReceipt.ReceivedAmt + Number.parseFloat(query1.sum(p1 => p1.ReceivedAmt).toFixed(2));

		$scope.newFeeReceipt.AfterReceivedDues = $scope.newFeeReceipt.ReceivableAmt - $scope.newFeeReceipt.ReceivedAmt;
	}
	$scope.CalculationOnTotal = function (col) {


		if (col == 1) {
			angular.forEach($scope.CurDues.FeeItemWiseDuesColl, function (det) {
				det.DiscountPer = 0;
				det.DiscountAmt = 0;

				det.DiscountPer = $scope.newFeeReceipt.DiscountPer;

				if (det.DiscountPer > 0)
					det.DiscountAmt = Number.parseFloat((det.TotalDues * det.DiscountPer / 100).toFixed(2));

				det.ReceivableAmt = det.TotalDues - det.DiscountAmt;
				det.AfterReceivedDues = det.ReceivableAmt - det.ReceivedAmt;

			});

			var query = mx($scope.CurDues.FeeItemWiseDuesColl);
			$scope.newFeeReceipt.ReceivableAmt = Number.parseFloat(query.sum(p1 => p1.ReceivableAmt).toFixed(2));
			$scope.newFeeReceipt.DiscountAmt = Number.parseFloat(query.sum(p1 => p1.DiscountAmt).toFixed(2));
			$scope.newFeeReceipt.FineAmt = Number.parseFloat(query.sum(p1 => p1.FineAmt).toFixed(2));
			$scope.newFeeReceipt.ReceivedAmt = Number.parseFloat(query.sum(p1 => p1.ReceivedAmt).toFixed(2));
		}
		else if (col == 4) {
			var totalAmt = 0;
			if ($scope.FeeConfig.AllowMultiplePaymentmode == true) {

				angular.forEach($scope.CurDues.PaymentModeColl, function (fi) {
					totalAmt += fi.Amount;
				});
				$scope.newFeeReceipt.ReceivedAmt = totalAmt;

			} else {
				totalAmt = $scope.newFeeReceipt.ReceivedAmt;
			}


			angular.forEach($scope.CurDues.FeeItemWiseDuesColl, function (det) {

				if (det.ReceivableAmt > 0) {
					if (totalAmt >= det.ReceivableAmt) {
						det.ReceivedAmt = det.ReceivableAmt;
						totalAmt -= det.ReceivableAmt;
					} else if (totalAmt > 0) {
						det.ReceivedAmt = totalAmt;
						totalAmt = 0;
					} else
						det.ReceivedAmt = 0;

					totalAmt = Number.parseFloat(totalAmt.toFixed(2));
					det.ReceivableAmt = det.TotalDues - det.DiscountAmt;
					det.AfterReceivedDues = det.ReceivableAmt - det.ReceivedAmt;
				}

			});

			var query = mx($scope.CurDues.FeeItemWiseDuesColl);
			$scope.newFeeReceipt.ReceivableAmt = Number.parseFloat(query.sum(p1 => p1.ReceivableAmt).toFixed(2));
			$scope.newFeeReceipt.DiscountAmt = Number.parseFloat(query.sum(p1 => p1.DiscountAmt).toFixed(2));
			$scope.newFeeReceipt.FineAmt = Number.parseFloat(query.sum(p1 => p1.FineAmt).toFixed(2));

		}

		var query1 = mx($scope.CurDues.ManualFeeDetailsColl);
		$scope.newFeeReceipt.ReceivableAmt = $scope.newFeeReceipt.ReceivableAmt + Number.parseFloat(query1.sum(p1 => p1.ReceivableAmt).toFixed(2));
		$scope.newFeeReceipt.DiscountAmt = $scope.newFeeReceipt.DiscountAmt + Number.parseFloat(query1.sum(p1 => p1.DiscountAmt).toFixed(2));
		$scope.newFeeReceipt.FineAmt = $scope.newFeeReceipt.FineAmt + Number.parseFloat(query1.sum(p1 => p1.FineAmt).toFixed(2));
		$scope.newFeeReceipt.ReceivedAmt = $scope.newFeeReceipt.ReceivedAmt + Number.parseFloat(query1.sum(p1 => p1.ReceivedAmt).toFixed(2));

		$scope.newFeeReceipt.AfterReceivedDues = $scope.newFeeReceipt.ReceivableAmt - $scope.newFeeReceipt.ReceivedAmt;
	}

	$scope.ClearFeeReceipt = function () {
		$scope.CurDues = null;
		$scope.newFeeReceipt = {
			TranId: null,
			AutoNumber: 0,
			AutoManualNo: '',
			RefNo: '',
			Date_TMP: new Date(),
			PreviousDues: 0,
			CurrentDues: 0,
			TotalDues: 0,
			DiscountAmt: 0,
			DiscountPer: 0,
			FineAmt: 0,
			ReceivableAmt: 0,
			ReceivedAmt: 0,
			AfterReceivedDues: 0,
			AdvanceAmt: 0,
			CostClassId: null,
			AcademicYearId: null,
			Narration: '',
			StudentId: null,
			ClassId: null,
			StudentName: '',
			DetailsColl: [],
			SelectStudent: $scope.StudentSearchOptions[0].value,
			ReceiptAsLedgerId: 1,
			Mode: 'Save'
		};

		if ($scope.FeeConfig.FeeReceiptLedgerId > 0)
			$scope.newFeeReceipt.ReceiptAsLedgerId = $scope.FeeConfig.FeeReceiptLedgerId;

		$timeout(function () {
			$scope.getVoucherNo();
		});
	}
	$scope.ClearFeeReceiptCollection = function () {
		$scope.newFeeReceiptCollection = {
			FeeReceiptCollectionId: null,

			Mode: 'Save'
		};
	}

	$scope.ClearTransferFeeReceiptToAccount = function () {
		$scope.newTransferFeeReceiptToAccount = {
			TransferFeeReceiptToAccountId: null,

			Mode: 'Save'
		};
	}

	$scope.PrintFeeeceiptCollections = function () {
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
												url: base_url + "Fee/Transaction/PrintFeeReturnColl",
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
														Period: $scope.newFeeReceiptCollection.DateFromDet.dateBS + ' TO ' + $scope.newFeeReceiptCollection.DateToDet.dateBS,
														OpeningDisAmt: $scope.OpeningDisAmt,
														OpeningAmt: $scope.OpeningAmt,
														CurrentAmt: $scope.CurrentAmt,
														CurrentDisAmt: $scope.CurrentDisAmt
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
							url: base_url + "Fee/Transaction/PrintFeeReturnColl",
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
									Period: $scope.newFeeReceiptCollection.DateFromDet.dateBS + ' TO ' + $scope.newFeeReceiptCollection.DateToDet.dateBS,
									OpeningDisAmt: $scope.OpeningDisAmt,
									OpeningAmt: $scope.OpeningAmt,
									CurrentAmt: $scope.CurrentAmt,
									CurrentDisAmt: $scope.CurrentDisAmt
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

	//************************* FeeReceipt *********************************

	$scope.IsValidFeeReceipt = function () {

		if (!$scope.SelectedCostClass || $scope.SelectedCostClass.CostClassId == null || $scope.SelectedCostClass.CostClassId == 0) {
			Swal.fire('Please ! Select CostClass');
			return false;
		}

		if (!$scope.newFeeReceipt.StudentId || $scope.newFeeReceipt.StudentId == null || $scope.newFeeReceipt.StudentId == 0) {
			Swal.fire('Please ! Select Student');
			return false;
		}

		if (!$scope.CurDues || $scope.CurDues == null) {
			Swal.fire('No Dues Found');
			return false;
		}

		if (!$scope.newFeeReceipt.PaidUpToMonth || $scope.newFeeReceipt.PaidUpToMonth == null || $scope.newFeeReceipt.PaidUpToMonth == 0) {
			Swal.fire('Please ! Select PaidUpToMonth');
			return false;

		}
		return true;
	}
	$scope.AddFeeDetails = function (ind) {
		if ($scope.CurDues.ManualFeeDetailsColl) {
			if ($scope.CurDues.ManualFeeDetailsColl.length > ind + 1) {
				$scope.CurDues.ManualFeeDetailsColl.splice(ind + 1, 0, {
					FeeItemId: null,
					PreviousDues: 0,
					CurrentDues: 0,
					DiscountAmt: 0,
					DiscountPer: 0,
					ReceivableAmt: 0,
					FineAmt: 0,
					ReceivedAmt: 0,
					TotalDues: 0
				})
			} else {
				$scope.CurDues.ManualFeeDetailsColl.push({
					FeeItemId: null,
					PreviousDues: 0,
					CurrentDues: 0,
					DiscountAmt: 0,
					DiscountPer: 0,
					ReceivableAmt: 0,
					FineAmt: 0,
					ReceivedAmt: 0,
					TotalDues: 0
				})
			}
		}
	};
	$scope.delFeeDetails = function (ind) {
		if ($scope.CurDues.ManualFeeDetailsColl) {
			if ($scope.CurDues.ManualFeeDetailsColl.length > 0) {
				$scope.CurDues.ManualFeeDetailsColl.splice(ind, 1);
			}
		}
	};


	$scope.SaveUpdateFeeReceipt = function () {
		if ($scope.IsValidFeeReceipt() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newFeeReceipt.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateFeeReceipt();
					}
				});
			} else
				$scope.CallSaveUpdateFeeReceipt();

		}
	};

	$scope.CallSaveUpdateFeeReceipt = function () {



		var isValidPaymentMode = true;
		if ($scope.FeeConfig.AllowMultiplePaymentmode == true) {

			angular.forEach($scope.CurDues.PaymentModeColl, function (fi) {
				if (!fi.LedgerId || fi.LedgerId == 0) {
					Swal.fire('Please ! Select Payment Mode');
					isValidPaymentMode = false;
				}

				if (fi.Amount <= 0) {
					Swal.fire('Please ! Enter Receipt Amt');
					isValidPaymentMode = false;
				}
			});


		}

		if (isValidPaymentMode == false)
			return;

		var fr = $scope.newFeeReceipt;
		var cd = $scope.CurDues;
		var para = {
			VoucherDate: new Date(),
			Narration: fr.Narration,
			RefNo: fr.RefNo,
			StudentId: fr.StudentId,
			PaidUpToMonth: fr.PaidUpToMonth,
			PreviousDues: cd.PreviousDues,
			CurrentDues: cd.CurrentDues,
			TotalDues: cd.TotalDues,
			DiscountPer: fr.DiscountPer,
			DiscountAmt: fr.DiscountAmt,
			FineAmt: fr.FineAmt,
			ReceivableAmt: fr.ReceivableAmt,
			ReceivedAmt: fr.ReceivedAmt,
			AfterReceivedDues: (cd.TotalDues - fr.DiscountAmt) - fr.ReceivedAmt,
			CostClassId: $scope.SelectedCostClass.CostClassId,
			DetailsColl: [],
			ReceiptAsLedgerId: (fr.ReceiptAsLedgerId ? fr.ReceiptAsLedgerId : 1),
			SemesterId: fr.StudentDetails.SemesterId,
			ClassYearId: fr.StudentDetails.ClassYearId,
			ClassId: fr.StudentDetails.ClassId,
			PaymentModeColl: [],
			ClassName: fr.StudentDetails.ClassName,
			Address: fr.StudentDetails.Address,
			StudentName: fr.StudentDetails.Name,
			Batch: fr.StudentDetails.Batch,
			Semester: fr.StudentDetails.Semester,
			ClassYear: fr.StudentDetails.ClassYear,
		};

		if ($scope.FeeConfig.AllowMultiplePaymentmode == true) {
			angular.forEach($scope.CurDues.PaymentModeColl, function (pm) {
				para.ReceiptAsLedgerId = pm.LedgerId;
				para.PaymentModeColl.push({
					LedgerId: pm.LedgerId,
					Amount: pm.Amount,
					Remarks: pm.Remarks
				});
			});
		}

		angular.forEach(cd.FeeItemWiseDuesColl, function (det) {
			if (det.FeeItemId > 0) {

				if ($scope.FeeConfig.MonthWiseFeeHeading == true) {
					det.IsManual = false;

					if (det.AfterReceivedDues < 0) {
						Swal.fire('Negative Amount does not accept');
						return;
					}
					para.DetailsColl.push(det);
				} else {

					if ($scope.FeeConfig.ShowDuesFeeHeadingInReceipt == true) {
						if (det.AfterReceivedDues < 0) {
							Swal.fire('Negative Amount does not accept');
							return;
						}
						para.DetailsColl.push(det);
					} else {
						if (det.FineAmt > 0 || det.DiscountAmt > 0 || det.ReceivedAmt > 0) {
							det.IsManual = false;

							if (det.AfterReceivedDues < 0) {
								Swal.fire('Negative Amount does not accept');
								return;
							}
							para.DetailsColl.push(det);
						}
					}

				}

			}
		});

		angular.forEach(cd.ManualFeeDetailsColl, function (det) {
			if (det.FeeItemId > 0) {
				if (det.FineAmt > 0 || det.DiscountAmt > 0 || det.ReceivedAmt > 0) {
					det.IsManual = true;

					//if (det.AfterReceivedDues < 0) {
					//	Swal.fire('Negative Amount does not accept');
					//	return;
					//}

					para.DetailsColl.push(det);
				}
			}
		});

		//if ($scope.newFeeReceipt.AfterReceivedDues < 0) {
		//	Swal.fire('Negative Amount does not accept');
		//	return;
		//}

		if ($scope.newFeeReceipt.DateDet) {
			para.VoucherDate = $filter('date')(new Date($scope.newFeeReceipt.DateDet.dateAD), 'yyyy-MM-dd');
		} else
			para.VoucherDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/SaveFeeReturn",
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

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearFeeReceipt();
				$scope.Print(res.data.Data.RId, null);
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.Print = function (tranId, tmpDet) {
		if ((tranId || tranId > 0) && (tmpDet == null || tmpDet.TranType == null || tmpDet.TranType == 1)) {
			var TranId = tranId;

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.FeeReceipt + "&voucherId=0&isTran=true",
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
												print = true;
												document.body.style.cursor = 'wait';
												document.getElementById("frmRpt").src = '';
												document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + $scope.entity.FeeReceipt + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";
												document.body.style.cursor = 'default';
												$('#FrmPrintReport').modal('show');

												//var newURL = base_url + "newpdfviewer.ashx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + $scope.entity.FeeReceipt + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";										 
												//$http({
												//	url: newURL,
												//	method: "GET",
												//	headers: {
												//		"Content-type": "application/pdf"
												//	},
												//	responseType: "arraybuffer"
												//}).then(function (resPDF) {

												//	var pdfFile = new Blob([resPDF.data], {
												//		type: "application/pdf"
												//	});
												//	var pdfUrl = URL.createObjectURL(pdfFile);

												//	$scope.loadingstatus = "stop";
												//	hidePleaseWait();

												//	printJS(pdfUrl);

												//}, function (errormessage) {
												//	alert('Unable to Delete data. pls try again.' + errormessage.responseText);
												//});
											}

										} else {
											resolve('You need to select:)')
										}
									})
								}
							})
						}

						if (rptTranId > 0 && print == false) {
							document.body.style.cursor = 'wait';
							document.getElementById("frmRpt").src = '';
							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + $scope.entity.FeeReceipt + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";
							document.body.style.cursor = 'default';
							$('#FrmPrintReport').modal('show');

							//var newURL = base_url + "newpdfviewer.ashx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + $scope.entity.FeeReceipt + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";
							//$http({
							//	url: newURL,
							//	method: "GET",
							//	headers: {
							//		"Content-type": "application/pdf"
							//	},
							//	responseType: "arraybuffer"
							//}).then(function (resPDF) {

							//	var pdfFile = new Blob([resPDF.data], {
							//		type: "application/pdf"
							//	});
							//	var pdfUrl = URL.createObjectURL(pdfFile);

							//	$scope.loadingstatus = "stop";
							//	hidePleaseWait();

							//	printJS(pdfUrl);

							//}, function (errormessage) {
							//	alert('Unable to Delete data. pls try again.' + errormessage.responseText);
							//});
						}

					} else
						Swal.fire('No Templates found for print');
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});



		}
	};

	$scope.GetAllFeeReceiptList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FeeReceiptList = [];

		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/GetAllFeeReturnList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FeeReceiptList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetFeeReceiptById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			FeeReceiptId: refData.FeeReceiptId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/GetFeeReturnById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFeeReceipt = res.data.Data;
				$scope.newFeeReceipt.Mode = 'Modify';


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelFeeReceiptById = function (refData) {

		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					FeeReceiptId: refData.FeeReceiptId
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Transaction/DelFeeReturn",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFeeReceiptList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* FeeReceiptCollection *********************************

	getterAndSetter();
	function getterAndSetter() {
		$scope.discounts = [];
		$scope.gridFRCollection = {
			showGridFooter: true,
			showColumnFooter: true,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,
			showTreeExpandNoChildren: true,
			columnDefs: [
				{
					name: 'Action',
					enableHiding: false,
					enableFiltering: false,
					enableSorting: false,
					minWidth: 140,
					enableColumnResizing: false,
					cellTemplate: '<a href="" class="p-1" title="Click For Cancel" ng-click="grid.appScope.ShowCancelDialog(row.entity)">' +
						'<i class="fas fa-times-circle text-danger" aria-hidden="true"></i>' +
						'</a>' +
						'<a href="" class="p-1" title="Click For Re-Print" ng-click="grid.appScope.Print(row.entity.TranId,row.entity)">' +
						'<i class="fas fa-eye text-secondary" aria-hidden="true"></i>' +
						'</a>' +
						'<a href="" class="p-1" title="Click For Re-Email" ng-click="grid.appScope.SendEmail(row.entity.TranId,row.entity)">' +
						'<i class="fa fa-envelope text-secondary" aria-hidden="true"></i>' +
						'</a>'
				},
				{ name: "SNo", displayName: "S.No", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "VoucherDate_BS", displayName: "Date", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "PaidUpToMonthName", displayName: "PaidUpToMonth", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "RegdNo", displayName: "Regd.No", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No", cellFilter: 'number', minWidth: 100, headerCellClass: 'headerAligment' },
				{
					name: "ClassName", displayName: "Class", minWidth: 120, headerCellClass: 'headerAligment'

				},
				{
					name: "SectionName", displayName: "Section", minWidth: 90, headerCellClass: 'headerAligment'
				},
				{ name: "Level", displayName: "Level", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Faculty", displayName: "Faculty", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Batch", displayName: "Batch", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "Year", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "RefNo", displayName: "Ref.No", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "AutoManualNo", displayName: "Rec.No", minWidth: 120, headerCellClass: 'headerAligment' },
				{
					name: "ReceivedAmt", displayName: "Amount", cellClass: 'numericAlignment', cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, minWidth: 100, headerCellClass: 'headerAligment',
					footerCellTemplate: '<div class="ui-grid-cell-contents" >{{col.getAggregationValue() | number:2 }}</div>',
					footerCellClass: 'numericAlignment'
				},
				{
					name: "DiscountAmt", displayName: "Discount Amt.", cellClass: 'numericAlignment', cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, minWidth: 100, headerCellClass: 'headerAligment',
					footerCellTemplate: '<div class="ui-grid-cell-contents" >{{col.getAggregationValue() | number:2 }}</div>',
					footerCellClass: 'numericAlignment'
				},
				{
					name: "FineAmt", displayName: "Fine Amt.", cellClass: 'numericAlignment', cellFilter: 'number', aggregationType: uiGridConstants.aggregationTypes.sum, minWidth: 100, headerCellClass: 'headerAligment',
					footerCellTemplate: '<div class="ui-grid-cell-contents" >{{col.getAggregationValue() | number:2 }}</div>',
					footerCellClass: 'numericAlignment'
				},
				{ name: "FatherName", displayName: "Father Name", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "F_ContactNo", displayName: "Contact No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DOB_BS", displayName: "DOB(B.S.)", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "PaidUpToMonth", displayName: "Month Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BranchName", displayName: "Branch", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CostClass", displayName: "CostClass", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "UserName", displayName: "User", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Narration", displayName: "Narration", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ReceiptAsLedger", displayName: "Receipt As", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "JVNo", displayName: "Jv. No.", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "CancelRemarks", displayName: "Cancel Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CancelDateTime", displayName: "Cancel DateTime", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CancelBy", displayName: "Cancel By", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "AcademicYearName", displayName: "Academic Year", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "LogDateTime", displayName: "LogDateTime", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "IsNewStudent", displayName: "Is New Student", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "LedgerPanaNo", displayName: "Ledger PanaNo", minWidth: 100, headerCellClass: 'headerAligment' },
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'feeCollection.csv',
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
			exporterExcelFilename: 'feeCollection.xlsx',
			exporterExcelSheetName: 'feeCollection',
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
			}
		};



	};

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

	$scope.CancelVoucher = {};
	$scope.ShowCancelDialog = function (refData) {
		$scope.CancelVoucher = refData;
		$('#modal-cancel').modal('show');
	};
	$scope.CancelFR = function () {

		if (!$scope.CancelVoucher.CancelRemarks || $scope.CancelVoucher.CancelRemarks.length == 0) {
			Swal.fire('Please ! Enter Cancel Remarks');
			return;
		}

		Swal.fire({
			title: 'Do you want to cancel the selected fee receipt ' + $scope.CancelVoucher.AutoVoucherNo,
			showCancelButton: true,
			confirmButtonText: 'Yes',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					TranId: $scope.CancelVoucher.TranId,
					CancelRemarks: $scope.CancelVoucher.CancelRemarks
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Transaction/CancelFeeReturn",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					$('#modal-cancel').modal('hide');
					Swal.fire(res.data.ResponseMSG);

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});

	}
	$scope.GetAllFeeReceiptCollectionList = function () {
		if ($scope.newFeeReceiptCollection.DateFromDet &&
			$scope.newFeeReceiptCollection.DateToDet &&
			$scope.newFeeReceiptCollection.DateToDet.dateAD < $scope.newFeeReceiptCollection.DateFromDet.dateAD) {

			$scope.newFeeReceiptCollection.DateTo_TMP = new Date();
			$scope.newFeeReceiptCollection.DateToDet = new Date();

			Swal.fire(
				'Invalid Date',
				'To Date must be greater than or equal to From Date',
				'warning'
			);
			return;
		}

		$scope.OpeningAmt = 0;
		$scope.OpeningDisAmt = 0;
		$scope.CurrentAmt = 0;
		$scope.CurrentDisAmt = 0;
		$scope.FeeReceiptCollectionList = [];

		if ($scope.newFeeReceiptCollection.DateFromDet && $scope.newFeeReceiptCollection.DateToDet) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				dateFrom: $filter('date')(new Date($scope.newFeeReceiptCollection.DateFromDet.dateAD), 'yyyy-MM-dd'),
				dateTo: $filter('date')(new Date($scope.newFeeReceiptCollection.DateToDet.dateAD), 'yyyy-MM-dd'),
				showCancel: $scope.newFeeReceiptCollection.ShowCancel,
				fromReceipt: $scope.newFeeReceiptCollection.fromReceipt,
				toReceipt: $scope.newFeeReceiptCollection.toReceipt
			};
			$http({
				method: 'POST',
				url: base_url + "Fee/Transaction/GetFeeReturnCollection",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.OpeningAmt = res.data.Data.OpeningAmt;
					$scope.OpeningDisAmt = res.data.Data.OpeningDisAmt;
					$scope.CurrentAmt = res.data.Data.CurrentAmt;
					$scope.CurrentDisAmt = res.data.Data.CurrentDisAmt;

					var frColl = [];
					var sno = 1;
					angular.forEach(res.data.Data.DataColl, function (fr) {
						fr.SNo = sno;
						fr.$$treeLevel = 0;
						frColl.push(fr);
						angular.forEach(fr.DetailsColl, function (det) {
							det.Name = '  -' + det.Name;
							frColl.push(det);
						});
						sno++;
					});
					$scope.gridFRCollection.data = frColl;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.GetFeeReceiptCollectionById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			FeeReceiptCollectionId: refData.FeeReceiptCollectionId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/GetFeeReturnCollectionById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFeeReceiptCollection = res.data.Data;
				$scope.newFeeReceiptCollection.Mode = 'Modify';



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelFeeReceiptCollectionById = function (refData) {

		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					FeeReceiptCollectionId: refData.FeeReceiptCollectionId
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Transaction/DelFeeReturnCollection",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFeeReceiptCollectionList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};


	$scope.AddPaymentModeRow = function (ind) {
		if ($scope.CurDues.PaymentModeColl) {
			if ($scope.CurDues.PaymentModeColl.length > ind + 1) {
				$scope.CurDues.PaymentModeColl.splice(ind + 1, 0, {
					LedgerId: 0,
					Amount: 0,
					Remarks: ''
				})
			} else {
				$scope.CurDues.PaymentModeColl.push({
					LedgerId: 0,
					Amount: 0,
					Remarks: ''
				})
			}
		}


		$scope.CalculationOnTotal(4);
	};
	$scope.delPaymentModeRow = function (ind) {
		if ($scope.CurDues.PaymentModeColl) {
			if ($scope.CurDues.PaymentModeColl.length > 1) {
				$scope.CurDues.PaymentModeColl.splice(ind, 1);
			}
		}

		$scope.CalculationOnTotal(4);
	};
});