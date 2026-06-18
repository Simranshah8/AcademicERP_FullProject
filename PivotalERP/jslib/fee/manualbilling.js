

app.controller('ManualBillingController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices) {
	$scope.Title = 'Manual Billing';
	$rootScope.ChangeLanguage();
	getterAndSetter();
	OnClickDefault();
	var glbS = GlobalServices;
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.LoadAutoRate = true;
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		/*$scope.LanguageColl = GlobalServices.getLangList();*/
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();

		$scope.SelectionList = [
			{ id: 1, text: 'Others' },
			{ id: 2, text: 'Enquiry' },
			{ id: 3, text: 'Registration' },
		];

		$scope.newBillingRpt = {
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
		}
		$scope.currentPages = {
			ManualBilling: 1,
			ManualBillingClass: 1

		};

		$scope.searchData = {
			ManualBilling: '',
			ManualBillingClass: ''

		};

		$scope.perPage = {
			ManualBilling: GlobalServices.getPerPageRow(),
			ManualBillingClass: GlobalServices.getPerPageRow(),
		};
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newManualBillingClass = {
			TranId: null,
			BillingType: 1,
			SNo: null,
			Date_AD: null,
			Date_AD_TMP: new Date(),
			ClassId: null,
			ManualBillingDetailsColl: [],
			ForBilling: 1,
			Mode: 'Save'
		};
		$scope.newManualBillingClass.ManualBillingDetailsColl.push({});

		//Ends

		$scope.SelectedCostClass = null;
		$scope.CostClassColl = [];
		$http({
			method: 'GET',
			url: base_url + "Account/Creation/GetCostClassForEntry",
			dataType: "json"
		}).then(function (res1) {
			if (res1.data.IsSuccess && res1.data.Data) {
				$scope.CostClassColl = res1.data.Data;

				if ($scope.CostClassColl && $scope.CostClassColl.length > 0)
					$scope.SelectedCostClass = $scope.CostClassColl[0];


			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newManualBilling = {
			SelectionId: 1
		}

		$scope.ClassList = [];
		GlobalServices.getClassListForOR().then(function (res) {
			$scope.ClassList = res.data.Data;
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

		$scope.entity = {
			ManualBilling: entityManualBill,
			FeeReceipt: entityFeeReceipt
		};


		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;

			$timeout(function () {
				if ($scope.AcademicConfig.ActiveFaculty == true) {

					$scope.FacultyList = [];
					GlobalServices.getFacultyList().then(function (res) {
						$scope.FacultyList = res.data.Data;
					}, function (reason) {
						Swal.fire('Failed' + reason);
					});

				}

				if ($scope.AcademicConfig.ActiveLevel == true) {

					$scope.LevelList = [];
					GlobalServices.getClassLevelList().then(function (res) {
						$scope.LevelList = res.data.Data;
					}, function (reason) {
						Swal.fire('Failed' + reason);
					});

				}

				if ($scope.AcademicConfig.ActiveSemester == true) {

					$scope.gridOptions.columnApi.setColumnsVisible(["colSemester"], true);
					$scope.SelectedClassSemesterList = [];
					$scope.SemesterList = [];
					GlobalServices.getSemesterList().then(function (res) {
						$scope.SemesterList = res.data.Data;
					}, function (reason) {
						Swal.fire('Failed' + reason);
					});


				} else {
					$scope.gridOptions.columnApi.setColumnsVisible(["colSemester"], false);
				}

				if ($scope.AcademicConfig.ActiveBatch == true) {

					$scope.gridOptions.columnApi.setColumnsVisible(["colBatch"], true);
					$scope.BatchList = [];
					GlobalServices.getBatchList().then(function (res) {
						$scope.BatchList = res.data.Data;
					}, function (reason) {
						Swal.fire('Failed' + reason);
					});

				}
				else {
					$scope.gridOptions.columnApi.setColumnsVisible(["colBatch"], false);
				}

				if ($scope.AcademicConfig.ActiveClassYear == true) {
					$scope.gridOptions.columnApi.setColumnsVisible(["colClassYear"], true);
					$scope.ClassYearList = [];
					$scope.SelectedClassClassYearList = [];
					GlobalServices.getClassYearList().then(function (res) {
						$scope.ClassYearList = res.data.Data;
					}, function (reason) {
						Swal.fire('Failed' + reason);
					});

				}
				else {
					$scope.gridOptions.columnApi.setColumnsVisible(["colClassYear"], false);
				}
			});


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

		//Added By Suresh on 21 chaitra
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

	};
	//Ends


	$scope.StudentSelectionChange = function () {
		$scope.MonthList = [];
		if ($scope.newManualBilling.StudentDetails) {
			GlobalServices.getAcademicMonthList($scope.newManualBilling.StudentDetails.StudentId, null).then(function (resAM) {
				angular.forEach(resAM.data.Data, function (m) {
					$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
				});
			});
		}
	}


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
			SelectionId: 1,
			IsRateAsFeeMapping: true,
			Mode: 'Save'
		};
		$scope.AddManualBillingDetail(0);
		$scope.GetAutoNumber();
	};

	//Classwise
	$scope.ClearManualBillingClass = function () {
		$scope.newManualBillingClass = {
			TranId: null,
			BillingType: 1,
			SNo: null,
			Date_AD: null,
			Date_AD_TMP: new Date(),
			ClassId: null,
			ManualBillingDetailsColl: [],
			ForBilling: 1,
			Mode: 'Save'
		};
		$scope.GetAutoNumberBillingClass();
		$scope.newManualBillingClass.ManualBillingDetailsColl.push({});
	};

	$scope.ChangeRate = function (det, col, CW) {

		var taxRate = isEmptyAmt(det.TaxRate);
		if (col == 1 || col == 2) {
			var disAmt = 0;
			var amt = det.Qty * det.Rate;
			if (det.DiscountPer > 0) {
				disAmt = amt * det.DiscountPer / 100;
			}
			det.DiscountAmt = disAmt;

			det.PayableAmt = (det.Qty * det.Rate) - disAmt;
			//det.DiscountAmt = (det.Qty * det.Rate) - disAmt;
			det.TaxAmt = det.PayableAmt * taxRate / 100;
			det.PayableAmt = ((det.Qty * det.Rate) - disAmt) + isEmptyAmt(det.TaxAmt);

		} else if (col == 3) {
			var disAmt = 0;
			var amt = det.Qty * det.Rate;
			if (det.DiscountPer > 0) {
				disAmt = amt * det.DiscountPer / 100;
			}
			if (det.DiscountPer < 0) {
				det.DiscountPer = 0;
			}
			if (det.DiscountPer > 100) {
				det.DiscountPer = 100;
			}
			det.DiscountAmt = disAmt;
			det.PayableAmt = amt - disAmt;
			det.TaxAmt = det.PayableAmt * taxRate / 100;
			det.PayableAmt = (amt - disAmt) + isEmptyAmt(det.TaxAmt);
		} else if (col == 4) {
			var disPer = 0;
			var amt = det.Qty * det.Rate;
			if (det.DiscountAmt > 0) {
				disPer = (det.DiscountAmt / amt) * 100;
			}
			det.DiscountPer = disPer ?? 0;
			det.PayableAmt = amt - det.DiscountAmt;
			det.TaxAmt = det.PayableAmt * taxRate / 100;
			det.PayableAmt = (amt - det.DiscountAmt) + isEmptyAmt(det.TaxAmt);
			if (det.DiscountAmt > det.PayableAmt + 1) {
				det.DiscountAmt = 0;
				det.DiscountPer = 0;
				det.PayableAmt = (det.Qty * det.Rate);
				Swal.fire('Discount Amount cannot be more then Receivable Amount');
			}
		}
		if (CW == 'CW') {
			var findData = mx($scope.newManualBillingClass.ManualBillingDetailsColl);
			$scope.newManualBillingClass.Qty = findData.sum(p1 => p1.Qty);
			$scope.newManualBillingClass.Rate = 0;
			$scope.newManualBillingClass.DiscountAmt = findData.sum(p1 => p1.DiscountAmt);
			$scope.newManualBillingClass.PayableAmt = findData.sum(p1 => p1.PayableAmt);
			$scope.newManualBillingClass.TaxAmt = findData.sum(p1 => p1.TaxAmt);
		}
		else {
			var findData = mx($scope.newManualBilling.ManualBillingDetailsColl);
			$scope.newManualBilling.Qty = findData.sum(p1 => p1.Qty);
			$scope.newManualBilling.Rate = 0;
			$scope.newManualBilling.DiscountAmt = findData.sum(p1 => p1.DiscountAmt);
			$scope.newManualBilling.PayableAmt = findData.sum(p1 => p1.PayableAmt);
			$scope.newManualBilling.TaxAmt = findData.sum(p1 => p1.TaxAmt);
		}

	};

	function OnClickDefault() {
		document.getElementById('manual-form').style.display = "none";
		document.getElementById('manualclass-form').style.display = "none";


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

		document.getElementById('add-manualclass').onclick = function () {
			document.getElementById('manualclass-section').style.display = "none";
			document.getElementById('manualclass-form').style.display = "block";
			$scope.ClearManualBillingClass();
		}
		document.getElementById('manualclassback-btn').onclick = function () {
			document.getElementById('manualclass-section').style.display = "block";
			document.getElementById('manualclass-form').style.display = "none";
		}
	};

	//************************* Class *********************************

	$scope.IsValidManualBilling = function () {
		if ($scope.newManualBilling.ForBilling == 1) {
			if (!$scope.newManualBilling.StudentId) {
				Swal.fire('Please ! Select Student');
				return false;
			}
		}
		// Check for invalid rate or quantity
		if ($scope.newManualBilling.ManualBillingDetailsColl) {
			for (let i = 0; i < $scope.newManualBilling.ManualBillingDetailsColl.length; i++) {
				const detail = $scope.newManualBilling.ManualBillingDetailsColl[i];
				if (detail.Qty <= 0) {
					GlobalServices.validateAndFocus('MBQty', 'Quantity must be greater than zero', false);
					return false;
				}
				if (detail.Rate <= 0) {
					GlobalServices.validateAndFocus('MBRate', 'Rate must be greater than zero', false);
					return false;
				}
			}
		}

		// Validation for Cash/Bank selection
		if ($scope.newManualBilling.ForBilling === 2 && (!$scope.newManualBilling.LedgerId || $scope.newManualBilling.LedgerId === "")) {
			GlobalServices.validateAndFocus('cboCashBank', 'Please ! Select Cash/Bank', true);
			return false;
		}
		return true;
	};




	/*Add and Delete Button*/
	$scope.AddManualBillingDetail = function (ind) {
		if ($scope.newManualBilling.ManualBillingDetailsColl) {
			if ($scope.newManualBilling.ManualBillingDetailsColl.length > ind + 1) {
				$scope.newManualBilling.ManualBillingDetailsColl.splice(ind + 1, 0, {
					FeeItemId: null,
					Qty: 1,
					Rate: 0,
					DiscountPer: 0,
					DiscountAmt: 0,
					PayableAmt: 0
				})
			} else {
				$scope.newManualBilling.ManualBillingDetailsColl.push({
					FeeItemId: null,
					Qty: 1,
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
	//Function added by suresh
	//For ClassWise
	$scope.AddManualBillingClassDetail = function (ind) {
		if ($scope.newManualBillingClass.ManualBillingDetailsColl) {
			if ($scope.newManualBillingClass.ManualBillingDetailsColl.length > ind + 1) {
				$scope.newManualBillingClass.ManualBillingDetailsColl.splice(ind + 1, 0, {
					FeeItemId: null,
					Qty: 1,
					Rate: 0,
					DiscountPer: 0,
					DiscountAmt: 0,
					PayableAmt: 0
				})
			} else {
				$scope.newManualBillingClass.ManualBillingDetailsColl.push({
					FeeItemId: null,
					Qty: 1,
					Rate: 0,
					DiscountPer: 0,
					DiscountAmt: 0,
					PayableAmt: 0
				})
			}
		}
	};
	$scope.delManualBillingClassDetail = function (ind) {
		if ($scope.newManualBillingClass.ManualBillingDetailsColl) {
			if ($scope.newManualBillingClass.ManualBillingDetailsColl.length > 1) {
				$scope.newManualBillingClass.ManualBillingDetailsColl.splice(ind, 1);
			}
		}
	};
	//Ends

	$scope.GetDatafromRegManualNo = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BillDet = {};
		var para = {
			RegNo: $scope.newManualBilling.RegNo,
			AutoManualNo: $scope.newManualBilling.AutoManualNo
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetDataFromRegAutoManualNo",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newManualBilling.StudentName = res.data.Data.Name;
				$scope.newManualBilling.Address = res.data.Data.Address;
				$scope.newManualBilling.ClassName = res.data.Data.ClassName;
				$scope.newManualBilling.ClassId = res.data.Data.ClassId;
				$scope.newManualBilling.RegistrationId = res.data.Data.RegistrationId;
				$scope.newManualBilling.AdmissionEnquiryId = res.data.Data.AdmissionEnquiryId;
				$scope.GetFeeItemList();


			} else {
				Swal.fire(res.data.ResponseMSG);
			}


		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};
	//Ends
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

			$scope.newManualBilling.SelectionId = 1;
		}

		//Added By Suresh on Chaitra 18 Starts for Ledger selection in case the conditioj is for Others
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

						if ($scope.newManualBilling.ForBilling == 2)
							$scope.newManualBilling.LedgerId = $scope.FeeConfig.FeeReceiptLedgerId;
						else if ($scope.newManualBilling.ForBilling == 1)
							$scope.newManualBilling.LedgerId = null;
					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});
		//Ends
	}


	$scope.CallSaveUpdateManualBilling = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newManualBilling.Date_ADDet) {
			$scope.newManualBilling.BillingDate = $filter('date')(new Date($scope.newManualBilling.Date_ADDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newManualBilling.BillingDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		if ($rootScope.LANG == 'np') {
			if ($scope.newManualBilling.BillingType > 1 && $scope.newManualBilling.Date_ADDet)
				$scope.newManualBilling.ForMonthId = $scope.newManualBilling.Date_ADDet.NM;
			else if ((!$scope.newManualBilling.BillingType || $scope.newManualBilling.BillingType == 0) && $scope.newManualBilling.Date_ADDet) {
				$scope.newManualBilling.ForMonthId = $scope.newManualBilling.Date_ADDet.NM;
			}
		} else {
			if ($scope.newManualBilling.BillingType > 1 && $scope.newManualBilling.Date_ADDet)
				$scope.newManualBilling.ForMonthId = new Date($scope.newManualBilling.Date_ADDet.dateAD).getMonth() + 1;
			else if ((!$scope.newManualBilling.BillingType || $scope.newManualBilling.BillingType == 0) && $scope.newManualBilling.Date_ADDet) {
				$scope.newManualBilling.ForMonthId = new Date($scope.newManualBilling.Date_ADDet.dateAD).getMonth() + 1;
			}
		}

		if ($scope.newManualBilling.ForBilling == 2) {
			$scope.newManualBilling.StudentId = null;
			$scope.newManualBilling.BillingType = 2;
			$scope.newManualBilling.IsCash = true;
		}

		var printFeeReceipt = false;
		if ($scope.newManualBilling.IsCash == true && $scope.newManualBilling.BillingType == 2 && $scope.newManualBilling.LedgerId > 0) {
			printFeeReceipt = true;
		}

		if ($scope.newManualBilling.StudentDetails) {
			$scope.newManualBilling.ClassId = $scope.newManualBilling.StudentDetails.ClassId;
			$scope.newManualBilling.SectionId = $scope.newManualBilling.StudentDetails.SectionId;
			$scope.newManualBilling.SemesterId = $scope.newManualBilling.StudentDetails.SemesterId;
			$scope.newManualBilling.ClassYearId = $scope.newManualBilling.StudentDetails.ClassYearId;

			$scope.newManualBilling.ClassName = $scope.newManualBilling.StudentDetails.ClassName;
			$scope.newManualBilling.Batch = $scope.newManualBilling.StudentDetails.Batch;
			$scope.newManualBilling.Semester = $scope.newManualBilling.StudentDetails.Semester;
			$scope.newManualBilling.ClassYear = $scope.newManualBilling.StudentDetails.ClassYear;

			$scope.newManualBilling.Address = $scope.newManualBilling.StudentDetails.Address;
			$scope.newManualBilling.StudentName = $scope.newManualBilling.StudentDetails.Name;
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

				if (printFeeReceipt == true)
					$scope.PrintReceipt(res.data.Data.RId);
				else
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

		var para = {
			CostClassId: $scope.SelectedCostClass ? $scope.SelectedCostClass.CostClassId : null
		}
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAutoNoOfManualBilling",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.newManualBilling.SNo = res.data.Data.RId;
				$scope.newManualBillingClass.SNo = res.data.Data.RId;
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
						if (res.data.Data.ResponseId && res.data.Data.ResponseId != null) {
							fDet.Rate = parseFloat(res.data.Data.ResponseId);
							fDet.PayableAmt = fDet.Rate;
						}
					}
					// Call ChangeFee after the rate is successfully set
					$timeout(function () {
						$scope.ChangeFee(fDet);
					});
				}
			}, function (reason) {
				Swal.fire('Failed: ' + reason);
			});
		} else {
			// Fallback call if conditions are not met
			$timeout(function () {
				$scope.ChangeFee(fDet);
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

	$scope.CancelVoucher = {};
	$scope.ShowCancelDialog = function (refData) {
		$scope.CancelVoucher = refData;
		$('#modal-cancel').modal('show');
	};
	$scope.CancelManualBilling = function () {

		if (!$scope.CancelVoucher.CancelRemarks || $scope.CancelVoucher.CancelRemarks.length == 0) {
			Swal.fire('Please ! Enter Cancel Remarks');
			return;
		}

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TranId: $scope.CancelVoucher.TranId,
			CancelRemarks: $scope.CancelVoucher.CancelRemarks
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/CancelMB",
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

	$scope.PrintReceipt = function (tranId) {
		if ((tranId || tranId > 0)) {
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
						}

					} else
						Swal.fire('No Templates found for print');
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});



		}
	};


	function getterAndSetter() {


		$scope.gridColumnDef = [
			{ headerName: "Bill Type", width: 120, field: "BillingType", filter: 'agTextColumnFilter', sortable: true },
			{ headerName: "Bill No.", width: 90, field: "AutoNumber", filter: 'agNumberColumnFilter', sortable: true },
			{ headerName: "Date", width: 120, field: "BillingMiti", filter: 'agTextColumnFilter', sortable: true },
			{ headerName: "Reg.No.", width: 120, field: "RegNo", filter: 'agTextColumnFilter', sortable: true },
			{ headerName: "Roll No.", width: 100, field: "RollNo", filter: 'agNumberColumnFilter', },
			{
				headerName: "Name", field: "Name", filter: 'agTextColumnFilter', width: 180,
			},

			{ headerName: "Class", width: 100, field: "ClassName", filter: 'agTextColumnFilter', },
			{ headerName: "Section", width: 100, field: "SectionName", filter: 'agTextColumnFilter', },

			{ headerName: "Batch", width: 100, field: "Batch", filter: 'agTextColumnFilter', hide: true, colId: 'colBatch', },
			{ headerName: "Semester", width: 100, field: "Semester", filter: 'agTextColumnFilter', hide: true, colId: 'colSemester', },
			{ headerName: "ClassYear", width: 100, field: "ClassYear", filter: 'agTextColumnFilter', hide: true, colId: 'colClassYear', },

			{ headerName: "Contact No", width: 100, field: "ContactNo", filter: 'agTextColumnFilter', },
			{ headerName: "Email", width: 100, field: "Email", filter: 'agTextColumnFilter', },
			{ headerName: "FeeItem", width: 100, field: "FeeItem", filter: 'agTextColumnFilter', },
			{ headerName: "Product Name", width: 100, field: "ProductName", filter: 'agTextColumnFilter', },
			{ headerName: "Product Code", width: 100, field: "ProductCode", filter: 'agTextColumnFilter', },

			{ headerName: "Qty", width: 120, field: "Qty", filter: 'agNumberColumnFilter', },
			{ headerName: "Rate", width: 150, field: "Rate", filter: 'agNumberColumnFilter', },
			{ headerName: "DiscountAmt", width: 120, field: "DiscountAmt", filter: 'agNumberColumnFilter', },
			{ headerName: "PaidAmt", width: 130, field: "PaidAmt", filter: 'agNumberColumnFilter', },
			{ headerName: "DuesAmt", width: 130, field: "DuesAmt", filter: 'agNumberColumnFilter', },
			{ headerName: "ForMonth", width: 150, field: "ForMonth", filter: 'agTextColumnFilter', },
			{ headerName: "Remarks", width: 150, field: "Remarks", filter: 'agTextColumnFilter', },
			{ headerName: "RefNo", width: 120, field: "RefNo", filter: 'agTextColumnFilter', },
			{ headerName: "AcademicYear", width: 160, field: "AcademicYear", filter: 'agTextColumnFilter', },
			{ headerName: "UserName", width: 120, field: "UserName", filter: 'agTextColumnFilter', },
			{ headerName: "LogDateTime", width: 120, field: "LogDateTime", filter: 'agTextColumnFilter', },
			{ headerName: "Is Cancel", width: 120, field: "IsCancel", filter: 'agTextColumnFilter', },

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
			columnDefs: $scope.gridColumnDef,
			enableColResize: true,
			rowData: null,
			filter: true,
			enableFilter: true,
			enableSorting: true,
			rowSelection: 'multiple',
			suppressHorizontalScroll: true,
			alignedGrids: [],
			onFilterChanged: function (e) {
				//console.log('onFilterChanged', e);
				var DiscountAmt = 0, PaidAmt = 0, DuesAmt = 0;
				$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
					var tb = node.data;
					DiscountAmt += tb.DiscountAmt;
					PaidAmt += tb.PaidAmt;
					DuesAmt += tb.DuesAmt;
				});

				$scope.dataForBottomGrid[0].DiscountAmt = DiscountAmt;
				$scope.dataForBottomGrid[0].PaidAmt = PaidAmt;
				$scope.dataForBottomGrid[0].DuesAmt = DuesAmt;
				$scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);
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
				DiscountAmt: 0,
				PaidAmt: 0,
				DuesAmt: 0,

			},
		];

		$scope.gridOptionsBottom = {
			defaultColDef: {
				resizable: true,
				width: 90
			},
			columnDefs: $scope.gridColumnDef,
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
	$scope.onFilterTextBoxChanged = function () {
		$scope.gridOptions.api.setQuickFilter($scope.search);
	}
	$scope.getBillingDetails = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			dateFrom: $filter('date')(new Date($scope.newBillingRpt.DateFromDet.dateAD), 'yyyy-MM-dd'),
			dateTo: $filter('date')(new Date($scope.newBillingRpt.DateToDet.dateAD), 'yyyy-MM-dd'),
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetBillingDetails",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {

			var DataColl = mx(res.data.Data);
			$scope.dataForBottomGrid[0].DiscountAmt = DataColl.sum(p1 => p1.DiscountAmt);
			$scope.dataForBottomGrid[0].PaidAmt = DataColl.sum(p1 => p1.PaidAmt);
			$scope.dataForBottomGrid[0].DuesAmt = DataColl.sum(p1 => p1.DuesAmt);

			$scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);

			$scope.gridOptions.api.setRowData(res.data.Data);

			hidePleaseWait();
			$scope.loadingstatus = "stop";

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.ExportFeeSummaryAsCSV = function () {
		var params = {
			fileName: 'billingDetails.csv',
			sheetName: 'billingDetails'
		};
		$scope.gridOptions.api.exportDataAsCsv(params);
	}

	$scope.PrintBillingDetails = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityFeeManualBilling + "&voucherId=0&isTran=false",
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
												url: base_url + "Global/PrintReportData",
												headers: { 'Content-Type': undefined },

												transformRequest: function (data) {

													var formData = new FormData();
													formData.append("entityId", entityFeeManualBilling);
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
														entityid: entityFeeManualBilling,
														voucherid: 0,
														tranid: 0,
														vouchertype: 0,
														sessionid: res.data.Data.ResponseId,
														Period: $scope.newBillingRpt.DateFromDet.dateBS + ' TO ' + $scope.newBillingRpt.DateToDet.dateBS,
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
							url: base_url + "Global/PrintReportData",
							headers: { 'Content-Type': undefined },

							transformRequest: function (data) {

								var formData = new FormData();
								formData.append("entityId", entityFeeManualBilling);
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
									entityid: entityFeeManualBilling,
									voucherid: 0,
									tranid: 0,
									vouchertype: 0,
									sessionid: res.data.Data.ResponseId,
									Period: $scope.newBillingRpt.DateFromDet.dateBS + ' TO ' + $scope.newBillingRpt.DateToDet.dateBS,
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

	//$scope.clearOtherField = function () {
	//	if ($scope.newManualBilling.SelectionId == 1) {
	//		$scope.newManualBilling.AutoManualNo = "";
	//	} else if ($scope.newManualBilling.SelectionId == 2) {
	//		$scope.newManualBilling.RegNo = "";
	//	}
	//};

	$scope.clearOtherField = function () {
		// Clear fields when the selection changes
		$scope.newManualBilling.StudentName = "";
		$scope.newManualBilling.ClassName = "";
		$scope.newManualBilling.Address = "";

		// Clear specific fields based on SelectionId
		$scope.newManualBilling.AutoManualNo = "";
		$scope.newManualBilling.RegNo = "";

		// Clear ManualBillingDetailsColl if "Others" is selected
		if ($scope.newManualBilling.SelectionId == 1) {
			// When "Others" is selected, clear ManualBillingDetailsColl and reset ClassName
			$scope.newManualBilling.ManualBillingDetailsColl = [];

			// Push an empty row into ManualBillingDetailsColl
			$scope.newManualBilling.ManualBillingDetailsColl.push({
				// Empty or default values for your table fields
				ItemName: "",   // Add your actual table column name here
				Qty: 0,
				Rate: 0,
				DiscountAmt: 0,
				PayableAmt: 0
			});

			$scope.newManualBilling.ClassName = "";  // Optional: if you want to reset the ClassName as well
		} else if ($scope.newManualBilling.SelectionId == 2) {
			// Specific logic for "Registration No"
			$scope.newManualBilling.RegNo = "";
		} else if ($scope.newManualBilling.SelectionId == 3) {
			// Specific logic for "Enquiry No"
			$scope.newManualBilling.AutoManualNo = "";
		}
	};


	$scope.GetFeeItemList = function () {
		if ($scope.newManualBilling.SelectionId == 1) {
			// Skip processing for "Others" selection to allow manual input in ClassName
			return;
		}
		$scope.loadingstatus = "running";

		if ($scope.newManualBilling.ClassName) {
			const selectedClassName = $scope.newManualBilling.ClassName;

			var queryCl = mx($scope.ClassList).firstOrDefault(p1 => p1.Name === selectedClassName);

			if (queryCl) {
				$scope.newManualBilling.ClassId = queryCl.ClassId;
				$scope.newManualBilling.ClassName = queryCl.Name;
			} else {
				$scope.newManualBilling.ClassId = null;
				$scope.newManualBilling.ClassName = "";
			}
		} else {
			// If ClassName is not present, clear ManualBillingDetailsColl
			$scope.newManualBilling.ManualBillingDetailsColl = [];
			$scope.loadingstatus = "stop";
			return;
		}

		var para = {
			ForId: 4,
			ClassId: $scope.newManualBilling.ClassId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetFeeItemFor",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (!$scope.newManualBilling) {
				$scope.newManualBilling = {};
			}

			if (res.data.IsSuccess == true) {
				if (!$scope.newManualBilling.ManualBillingDetailsColl) {
					$scope.newManualBilling.ManualBillingDetailsColl = [];
				}

				$scope.newManualBilling.ManualBillingDetailsColl = res.data.Data;

				var findData = mx($scope.newManualBilling.ManualBillingDetailsColl);
				$scope.newManualBilling.Qty = findData.sum(p1 => p1.Qty);
				$scope.newManualBilling.Rate = 0;
				$scope.newManualBilling.DiscountAmt = findData.sum(p1 => p1.DiscountAmt);
				$scope.newManualBilling.PayableAmt = findData.sum(p1 => p1.PayableAmt);

				angular.forEach($scope.newManualBilling.ManualBillingDetailsColl, function (feeItem) {
					$scope.ChangeFee(feeItem);
				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});
	};


	$scope.ChangeFee = function (beData) {
		if ($scope.newManualBilling.BillingType == 1) {
			beData.TaxRate = 0;
		} else {
			if (beData.FeeItemId > 0) {
				var findMT = $scope.FeeItemList.find(function (p1) {
					return p1.FeeItemId == beData.FeeItemId;
				});
				if (findMT) {
					beData.TaxRate = findMT.TaxRate || 0; // Set TaxRate or default to 0 if undefined
				} else {
					beData.TaxRate = 0; // Fallback if item not found
				}
			} else {
				beData.TaxRate = 0; // Fallback if FeeItemId is not greater than 0
			}
		}
	}


	$scope.GetAutoNumberBillingClass = function () {
		var para = {
			CostClassId: $scope.SelectedCostClass ? $scope.SelectedCostClass.CostClassId : null
		}
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAutoNoClassWiseManualBilling",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.newManualBillingClass.AutoNumber = res.data.Data.RId;
				$scope.newManualBillingClass.SNo = res.data.Data.RId;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.SaveClassWiseManualBilling = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newManualBillingClass.Date_ADDet) {
			$scope.newManualBillingClass.BillingDate = $filter('date')(new Date($scope.newManualBillingClass.Date_ADDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newManualBillingClass.BillingDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		if ($rootScope.LANG == 'np') {
			if ($scope.newManualBillingClass.BillingType > 1 && $scope.newManualBillingClass.Date_ADDet)
				$scope.newManualBillingClass.ForMonthId = $scope.newManualBillingClass.Date_ADDet.NM;
			else if ((!$scope.newManualBillingClass.BillingType || $scope.newManualBillingClass.BillingType == 0) && $scope.newManualBillingClass.Date_ADDet) {
				$scope.newManualBillingClass.ForMonthId = $scope.newManualBillingClass.Date_ADDet.NM;
			}
		} else {
			if ($scope.newManualBillingClass.BillingType > 1 && $scope.newManualBillingClass.Date_ADDet)
				$scope.newManualBillingClass.ForMonthId = new Date($scope.newManualBillingClass.Date_ADDet.dateAD).getMonth() + 1;
			else if ((!$scope.newManualBillingClass.BillingType || $scope.newManualBillingClass.BillingType == 0) && $scope.newManualBillingClass.Date_ADDet) {
				$scope.newManualBillingClass.ForMonthId = new Date($scope.newManualBillingClass.Date_ADDet.dateAD).getMonth() + 1;
			}
		}

		if ($scope.newManualBillingClass.ForBilling == 2) {
			$scope.newManualBillingClass.BillingType = 2;
			$scope.newManualBillingClass.IsCash = true;
		}

		var printFeeReceipt = false;
		if ($scope.newManualBillingClass.IsCash == true && $scope.newManualBillingClass.BillingType == 2 && $scope.newManualBillingClass.LedgerId > 0) {
			printFeeReceipt = true;
		}
		$scope.newManualBillingClass.ClassId = $scope.newManualBillingClass.SelectedClass.ClassId;
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveClassWiseManualBilling",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newManualBillingClass }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearManualBillingClass();
				$scope.GetAllManualBillingClassWiseList();
				if (printFeeReceipt == true)
					$scope.PrintReceipt(res.data.Data.RId);
				else
					$scope.Print(res.data.Data.RId);
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};


	$scope.DelClasWiseManualBillingById = function (refData) {
		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					ManualBillingId: refData.TranId
				};
				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelManualBillingClassWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllManualBillingClassWiseList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};


	$scope.GetAllManualBillingClassWiseList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ManualBillingClasswiseList = [];
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAllManualBillingClassWiseList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ManualBillingClasswiseList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.GetClassWiseManualBillingById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.MBillDet = {};
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetClassWiseManualBillingDetailsById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.MBillDet = res.data.Data;
				angular.forEach($scope.BillingTypes, function (item) {
					if (item.Id == $scope.MBillDet.BillingType) {
						$scope.MBillDet.BillingTypeName = item.Text;
					}
				});
				$('#modal-xl1').modal('show');

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

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