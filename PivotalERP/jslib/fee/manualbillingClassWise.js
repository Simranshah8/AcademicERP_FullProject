

app.controller('ManualBillingClassWiseController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Manual Billing';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.LoadAutoRate = true;
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		/*$scope.LanguageColl = GlobalServices.getLangList();*/
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();

		$scope.currentPages = {
			ManualBilling: 1,

		};

		$scope.searchData = {
			ManualBilling: '',

		};

		$scope.perPage = {
			ManualBilling: GlobalServices.getPerPageRow(),

		};
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.MonthList = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.entity = {
			ManualBilling: entityManualBill
		};

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
						$scope.newFeeConfiguration = res.data.Data;

						$scope.BillingTypes = [];
						$http({
							method: 'GET',
							url: base_url + "Fee/Creation/GetBillingTypes",
							dataType: "json"
						}).then(function (res) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							if (res.data.IsSuccess && res.data.Data) {
								var btColl = res.data.Data;
								angular.forEach(btColl, function (bc) {
									if ($scope.newFeeConfiguration.ActiveMemoBilling == true && bc.id == 1)
										$scope.BillingTypes.push(bc)
									else if (bc.id > 1)
										$scope.BillingTypes.push(bc);
								});


								$scope.newManualBilling = {
									TranId: null,
									BillingType: ($scope.newFeeConfiguration.ActiveMemoBilling == true ? 1 : 2),
									SNo: null,
									Date_AD: null,
									Date_AD_TMP: new Date(),
									ClassId: null,
									ManualBillingDetailsColl: [],
									SelectStudent: $scope.StudentSearchOptions[0].value,
									ShowLeftStudent: false,
									IsCash: true,
									ForBilling: 1,
									Mode: 'Save'
								};
								$scope.AddManualBillingDetail(0);

								$scope.GetAllManualBillingList();
								$scope.GetAutoNumber();

							}
						}, function (reason) {
							Swal.fire('Failed' + reason);
						});

					});


				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});

		$timeout(function () {
			$scope.FeeItemList = [];
			GlobalServices.getFeeItemList().then(function (res1) {
				$scope.FeeItemList = res1.data.Data;
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});

	};


	$scope.ClearManualBilling = function () {
		$scope.newManualBilling = {
			TranId: null,
			BillingType: 1,
			SNo: null,
			Date_AD: null,
			Date_AD_TMP: new Date(),
			ClassId: null,
			ManualBillingDetailsColl: [],
			SelectStudent: $scope.StudentSearchOptions[0].value,
			ShowLeftStudent: false,
			IsCash: true,
			ForBilling: 1,
			Mode: 'Save'
		};
		$scope.AddManualBillingDetail(0);
		$scope.GetAutoNumber();
	};

	$scope.ChangeRate = function (det, col) {
		if (col == 1 || col == 2) {
			det.PayableAmt = (det.Qty * det.Rate) - det.DiscountAmt;
		} else if (col == 3) {
			var disAmt = 0;
			var amt = det.Qty * det.Rate;
			if (det.DiscountPer > 0) {
				disAmt = amt * det.DiscountPer / 100;
			}

			det.DiscountAmt = disAmt;
			det.PayableAmt = amt - disAmt;
		} else if (col == 4) {
			var disPer = 0;
			var amt = det.Qty * det.Rate;
			if (det.DiscountAmt > 0) {
				disPer = (det.DiscountAmt / amt) * 100;
			}
			det.DiscountPer = disPer;
			det.PayableAmt = amt - det.DiscountAmt;
		}
		var findData = mx($scope.newManualBilling.ManualBillingDetailsColl);
		$scope.newManualBilling.Qty = findData.sum(p1 => p1.Qty);
		$scope.newManualBilling.Rate = 0;
		$scope.newManualBilling.DiscountAmt = findData.sum(p1 => p1.DiscountAmt);
		$scope.newManualBilling.PayableAmt = findData.sum(p1 => p1.PayableAmt);

	};

	function OnClickDefault() {
		document.getElementById('manual-form').style.display = "none";

		document.getElementById('add-manual').onclick = function () {
			document.getElementById('manual-section').style.display = "none";
			document.getElementById('manual-form').style.display = "block";
		}
		document.getElementById('manualback-btn').onclick = function () {
			document.getElementById('manual-section').style.display = "block";
			document.getElementById('manual-form').style.display = "none";
			$timeout(function () {
				$scope.GetAllManualBillingList();
			});
		}
	};

	//************************* Class *********************************

	$scope.IsValidManualBilling = function () {
		//if ($scope.newManualBilling.SNo.isEmpty()) {
		//	Swal.fire('Please ! Enter SNo');
		//	return false;
		//}
		return true;
	};



	/*Add and Delete Button*/
	$scope.AddManualBillingDetail = function (ind) {
		if ($scope.newManualBilling.ManualBillingDetailsColl) {
			if ($scope.newManualBilling.ManualBillingDetailsColl.length > ind + 1) {
				$scope.newManualBilling.ManualBillingDetailsColl.splice(ind + 1, 0, {
					FeeItemId: null,
					Qty: 0,
					Rate: 0,
					DiscountPer: 0,
					DiscountAmt: 0,
					PayableAmt: 0
				})
			} else {
				$scope.newManualBilling.ManualBillingDetailsColl.push({
					FeeItemId: null,
					Qty: 0,
					Rate: 0,
					DiscountPer: 0,
					DiscountAmt: 0,
					PayableAmt: 0
				})
			}
		}
	};
	$scope.delManualBillingDetail = function (ind) {
		if ($scope.newManualBilling.ManualBillingDetailsColl) {
			if ($scope.newManualBilling.ManualBillingDetailsColl.length > 1) {
				$scope.newManualBilling.ManualBillingDetailsColl.splice(ind, 1);
			}
		}
	};



	$scope.SaveUpdateManualBilling = function () {
		if ($scope.IsValidManualBilling() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newManualBilling.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateManualBilling();
					}
				});
			} else
				$scope.CallSaveUpdateManualBilling();

		}
	};

	$scope.ChangeForBilling = function () {
		if ($scope.newManualBilling.ForBilling == 2) {
			$scope.newManualBilling.StudentId = null;
			$scope.newManualBilling.BillingType = 2;
			$scope.newManualBilling.IsCash = true;
		}
	}
	$scope.CallSaveUpdateManualBilling = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newManualBilling.Date_ADDet) {
			$scope.newManualBilling.BillingDate = $filter('date')(new Date($scope.newManualBilling.Date_ADDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newManualBilling.BillingDate = new Date();

		if ($scope.newManualBilling.BillingType > 1)
			$scope.newManualBilling.ForMonthId = $scope.newManualBilling.Date_ADDet.NM;
		else if (!$scope.newManualBilling.BillingType || $scope.newManualBilling.BillingType == 0) {
			$scope.newManualBilling.ForMonthId = $scope.newManualBilling.Date_ADDet.NM;
		}

		if ($scope.newManualBilling.ForBilling == 2) {
			$scope.newManualBilling.StudentId = null;
			$scope.newManualBilling.BillingType = 2;
			$scope.newManualBilling.IsCash = true;
		}


		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveManualBilling",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newManualBilling }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearManualBilling();
				$scope.Print(res.data.Data.RId);


			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllManualBillingList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ManualBillingList = [];

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAllManualBillingList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ManualBillingList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};


	$scope.GetManualBillingById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BillDet = {};
		var para = {
			TranId: refData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetManualBillingDetById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BillDet = res.data.Data;
				$('#modal-xl').modal('show');

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};



	$scope.GetAutoNumber = function () {

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAutoNoOfManualBilling",
			dataType: "json",

		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.newManualBilling.SNo = res.data.Data.RId;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetFeeRate = function (fDet) {

		if ($scope.newManualBilling.StudentId && $scope.newManualBilling.StudentId > 0 && $scope.LoadAutoRate == true) {
			var para = {
				StudentId: $scope.newManualBilling.StudentId,
				FeeItemId: fDet.FeeItemId
			};
			$http({
				method: 'POST',
				url: base_url + "Fee/Creation/GetFeeRate",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess) {
					if (res.data.Data) {
						if (res.data.Data.ResponseId && res.data.Data.ResponseId != null)
							fDet.Rate = parseFloat(res.data.Data.ResponseId);
					}
					//$scope.newManualBilling.SNo = res.data.Data.RId;
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}


	};
	$scope.DelManualBillingById = function (refData) {

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
					ManualBillingId: refData.TranId
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelManualBilling",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllManualBillingList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};
	$scope.CancelManualBilling = function (refData) {


		Swal.fire({
			title: 'Do you want to cancel the selected bill no:- ' + refData.AutoNumber,
			showCancelButton: true,
			confirmButtonText: 'Yes',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					TranId: refData.TranId,
					CancelRemarks: 'Cancel Manual Billing'
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/CancelMB",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});

	}

	$scope.Print = function (tranId) {
		if (tranId && tranId > 0) {
			var TranId = tranId;

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.ManualBilling + "&voucherId=0&isTran=true",
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
												document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + $scope.entity.ManualBilling + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";
												document.body.style.cursor = 'default';
												$('#FrmPrintReport').modal('show');
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
							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + $scope.entity.ManualBilling + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";
							document.body.style.cursor = 'default';
							$('#FrmPrintReport').modal('show');
						}

					} else
						Swal.fire('No Templates found for print');
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}
	};
});