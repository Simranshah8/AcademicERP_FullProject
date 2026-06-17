app.controller('FeeSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Fee Setup';
	/*OnClickDefault();*/

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();

		$scope.NumberingMethods = GlobalServices.getNumberingMethod();
		$scope.FeeMappingTypes = [{ id: 1, text: 'ClassWise' }, { id: 2, text: 'StudentTypeWise' }, { id: 3, text: 'MediumWise' }];
		$scope.DateStyles = GlobalServices.getDateStyle();
		$scope.DateFormats = GlobalServices.getDateFormat();

		$scope.DiscountEffectAsColl = [{ id: 1, text: 'Expense/Loss' }, { id: 2, text: 'Credit Note' }];

		$scope.BillNoAsColl = [{ id: 1, text: 'Academic Year Wise' }, { id: 2, text: 'Financial Year Wise ' }];
		//--1<From Month,2<=From Month,3 <ToMonth,4<=To Month,5 Between From To Month
		$scope.CalculateManualBillAsColl = [{ id: 1, text: '< From Month' }, { id: 2, text: '<= From Month ' }, { id: 3, text: '< To Month ' }, { id: 4, text: '<= To Month ' }, { id: 5, text: 'between from and to month' }];

		$scope.AcademicYearColl = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllAcademicYearList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AcademicYearColl = res.data.Data;

				if ($scope.AcademicYearColl.length > 0) {
					$scope.CurAcademicYearId = $scope.AcademicYearColl[0].AcademicYearId;
				}
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.MonthList = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ReceiptMonthAsColl = [{ id: 1, text: 'PaidUpToMonth' }, { id: 2, text: 'ReceiptDateMonth' }]

		$scope.FeeItemList = [];
		GlobalServices.getFeeItemList().then(function (res1) {
			$scope.FeeItemList = res1.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.searchData = {
			Defaulter: ''
		};

		$scope.newFeeConfiguration = {
			FeeConfigurationId: null,
			TransportFeeHeadingId: null,
			HostelFeeHeadingId: null,
			LibraryFeeHeadingId: null,
			CanteenFeeHeadingId: '',
			FineFeeHeadingId: null,
			TaxFeeHeadingId: null,
			FixedStudentFeeHeadingId: null,
			FeeReceiptLedgerId: null,
			DiscountLedgerId: null,
			TaxLedgerId: null,
			FineLedgerId: null,
			FixedStudentLedgerId: null,
			FineFeeHeading: '',
			TaxFeeHeading: '',
			NumberingMethod: 1,
			NumericalPartWidth: '',
			Prefix: '',
			Suffix: '',
			DateStyle: '',
			DateFormat: '',
			NoOfDecimals: 0,
			FeemappingApplicableId: null,
			Notes: '',
			ReceiptMonthAs: 1,
			ReceiptDateValidateInBillPrint: false,
			ActiveMemoBilling: false,
			ShowRate: false,
			AllowDiscount: false,
			AllowFine: false,
			ShowOpeningHeadingWise: false,
			AdvanceFeeItemId: null,
			ShowLeftStudentInDiscountSetup: false,
			Mode: 'Save',

			OpeningFeeMonth: '',
			AllowMonthWiseOnlinePayment: true,
			AllowValidateTotalDues: false,
			InclBillDisSL: false,   //Added by Simran
		};

		$scope.newBillingConfiguration = {
			BillingConfigurationId: null,
			DateStyle: 1,
			DateFormat: 3,
			NoOfDecimal: 0,
			DiscountHeading: '',
			NumberingMethodId: null,
			NumericalPartWidth: '',
			Prefix: '',
			Suffix: '',
			BillingHeadingFontId: null,
			BillingHeading: '',
			BillingNotesFontId: null,
			BillingNotes: '',
			ReminderSlipNoteFontId: null,
			ReminderSlipNote: '',
			ShowPreDuesFeeHeading: false,
			isReGenerateInvoice: false,
			IgnoreCashSalesReceiptInBillPrint: false,
			Mode: 'Save',
			ActiveTaxOnManualBilling: true,
			//Added by suresh for 4th no as default
			CalculateManualBillAs: 4
		};


		$scope.VoucherColl = [];
		$http({
			method: 'GET',
			url: base_url + "Account/Creation/GetVoucherList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.VoucherColl = res.data.Data;
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
						$scope.newFeeConfiguration = res.data.Data;

						$scope.newFeeConfiguration.FeeReceiptLedgerId = $scope.newFeeConfiguration.FeeReceiptLedgerId;

						if ($scope.newFeeConfiguration.DefaulterDuesColl) {
							angular.forEach($scope.newFeeConfiguration.DefaulterDuesColl, function (dd) {
								if (dd.BillGenerateOn)
									dd.BillGenerateOn_TMP = new Date(dd.BillGenerateOn);
							});
						}

					});


				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});

		$timeout(function () {
			$http({
				method: 'POST',
				url: base_url + "Fee/Transaction/GetBillConfiguration",
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$timeout(function () {
						$scope.newBillingConfiguration = res.data.Data;
						$scope.newBillingConfiguration.isReGenerateInvoice = false;
					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});
		$timeout(function () {
			$scope.GetCostCenterLog();
		});
	}



	//************************* Fee Configuration *********************************

	$scope.IsValidFeeConfiguration = function () {
		//if ($scope.newFeeConfiguration.FineFeeHeading.isEmpty()) {
		//	Swal.fire('Please ! Enter Fine Fee Heading');
		//	return false;
		//}
		return true;
	}

	$scope.SaveUpdateFeeConfiguration = function () {
		if ($scope.IsValidFeeConfiguration() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newFeeConfiguration.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateFeeConfiguration();
					}
				});
			} else
				$scope.CallSaveUpdateFeeConfiguration();

		}
	};

	$scope.CallSaveUpdateFeeConfiguration = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		angular.forEach($scope.newFeeConfiguration.DefaulterDuesColl, function (dd) {
			if (dd.BillGenerateOnDet)
				dd.BillGenerateOn = $filter('date')(new Date(dd.BillGenerateOnDet.dateAD), 'yyyy-MM-dd');
		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/SaveFeeConfiguration",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newFeeConfiguration }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				//$scope.ClearFeeConfiguration();
				$scope.GetAllFeeConfigurationList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}



	//************************* Billing Configuration *********************************

	$scope.IsValidBillingConfiguration = function () {
		//if ($scope.newBillingConfiguration.DiscountHeading.isEmpty()) {
		//	Swal.fire('Please ! Enter Discount Heading');
		//	return false;
		//}
		return true;
	};

	$scope.SaveUpdateBillingConfiguration = function () {
		if ($scope.IsValidBillingConfiguration() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBillingConfiguration.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBillingConfiguration();
					}
				});
			} else
				$scope.CallSaveUpdateBillingConfiguration();

		}
	};

	$scope.CallSaveUpdateBillingConfiguration = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/SaveBillConfiguration",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newBillingConfiguration }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBillingConfiguration();
				$scope.GetAllBillingConfigurationList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetCostCenterLog = function () {

		$scope.CostCenterLogStudentColl = [];
		$scope.CostCenterLogEmpColl = [];
		$timeout(function () {
			$http({
				method: 'POST',
				url: base_url + "Fee/Transaction/GetCostCenterGenLog",
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$timeout(function () {
						var tmpColl = res.data.Data;
						angular.forEach(tmpColl, function (tc) {
							if (tc.ForCostCenter == 1)
								$scope.CostCenterLogStudentColl.push(tc);
							else if (tc.ForCostCenter == 2)
								$scope.CostCenterLogEmpColl.push(tc);
						});
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});

	};

	$scope.generateStudentCostCenter = function () {
		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/GenerateCostCenter",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
			$timeout(function () {
				$scope.GetCostCenterLog();
			});
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.generateEmpCostCenter = function () {
		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/GenerateCostCenterEmp",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
			$timeout(function () {
				$scope.GetCostCenterLog();
			});
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.generateJournalFromFeeReceipt = function () {

		var para =
		{
			IsReGenerate: ($scope.newBillingConfiguration.isReGenerateJournal ? $scope.newBillingConfiguration.isReGenerateJournal : false)
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/GenerateFeeReceiptToJournal",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
			$timeout(function () {
				$scope.GetCostCenterLog();
			});
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.CreateSalesinvoiceofPending = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			IsReGenerate: $scope.newBillingConfiguration.isReGenerateInvoice,
			DateFrom: ($scope.newBillingConfiguration.DateFromDet ? $scope.newBillingConfiguration.DateFromDet.dateAD : null),
			DateTo: ($scope.newBillingConfiguration.DateToDet ? $scope.newBillingConfiguration.DateToDet.dateAD : null),
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/GenerateSalesInvoiceOfBill",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
			$timeout(function () {
				$scope.GetCostCenterLog();
			});
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.DelTransportMapping = function () {

		Swal.fire({
			title: 'Do you want to delete the transport mapping for selected month ?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ForMonthId: $scope.TransportForMonthId
				};

				$http({
					method: 'POST',
					url: base_url + "Transport/Creation/DelTransportMappingForMonth",
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


	};
	$scope.DelBedMapping = function () {

		Swal.fire({
			title: 'Do you want to delete the bed mapping for selected month ?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ForMonthId: $scope.BedMappingForMonthId
				};

				$http({
					method: 'POST',
					url: base_url + "Hostel/Creation/DelBedMappingForMonth",
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


	};

	$scope.SaveTransportMapping = function () {

		Swal.fire({
			title: 'Do you want to generate the transport mapping for selected month ?',
			showCancelButton: true,
			confirmButtonText: 'Generate',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ForMonthId: $scope.TransportForMonthId
				};

				$http({
					method: 'POST',
					url: base_url + "Transport/Creation/TransportMappingForMonth",
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


	};
	$scope.SaveBedMapping = function (refData) {

		Swal.fire({
			title: 'Do you want to generate the bed mapping for selected month ?',
			showCancelButton: true,
			confirmButtonText: 'Generate',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ForMonthId: $scope.BedMappingForMonthId
				};

				$http({
					method: 'POST',
					url: base_url + "Hostel/Creation/BedMappingForMonth",
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


	};



	$scope.SaveStudentOpeningMerge = function () {

		Swal.fire({
			title: 'Do you want to merge student opening into single feeitem ?',
			showCancelButton: true,
			confirmButtonText: 'Mege',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					FeeItemId: $scope.FeeItemIdForOpening
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Transaction/MergeStudentOpening",
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


	};

	$scope.SaveStudentLeft = function () {

		Swal.fire({
			title: 'Do you want to update missing left student ?',
			showCancelButton: true,
			confirmButtonText: 'Update',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					FromAcademicYearId: $scope.FromAcademicYearIdSL,
					ToAcademicYearId: $scope.ToAcademicYearIdSL
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Transaction/UpdateMissingLeftStudent",
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


	};

	//Added on ashwin 20 for missing fee to account
	$scope.CreateMissingSalesinvoice = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			DateFrom: ($scope.newBillingConfiguration.DateFromDet ? $scope.newBillingConfiguration.DateFromDet.dateAD : null),
			DateTo: ($scope.newBillingConfiguration.DateToDet ? $scope.newBillingConfiguration.DateToDet.dateAD : null),
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/MissingFeeToAccount",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
			$timeout(function () {
				$scope.GetCostCenterLog();
			});
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.validateDates = function (changedField) {
		if (!$scope.newBillingConfiguration.DateFromDet || !$scope.newBillingConfiguration.DateToDet ||
			!$scope.newBillingConfiguration.DateFromDet.dateAD || !$scope.newBillingConfiguration.DateToDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newBillingConfiguration.DateFromDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newBillingConfiguration.DateToDet.dateAD), 'yyyy-MM-dd')
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newBillingConfiguration.DateFrom_TMP = new Date();
						$scope.newBillingConfiguration.DateFromDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newBillingConfiguration.DateTo_TMP = new Date();
						$scope.newBillingConfiguration.DateToDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};
});