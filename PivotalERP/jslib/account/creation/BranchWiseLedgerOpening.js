app.controller('BranchWiseLedgerOpeningController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'BranchWise Ledger Opening';


	$scope.LoadData = function () {

		$('.select2').select2({
			allowClear: true,
			openOnEnter: true
		});
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.DrCrList = GlobalServices.getDrCr();

		$timeout(function () {
			$http({
				method: "GET",
				url: base_url + "Global/GetCompanyDetail",
				dataType: "json"
			}).then(function (res) {
				$scope.CompDet = res.data.Data;
			/*	$scope.newBranchWiseLedgerOpening.VoucherDate_TMP = new Date($scope.CompDet.StartDate);*/
			}, function (errormessage) {
				alert('Unable to Delete data. pls try again.' + errormessage.responseText);
			});
		});

		$timeout(function () {
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
		});

		$scope.CostClassColl = [];
		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetAllCostClassList",
			dataType: "json"
		}).then(function (res1) {
			if (res1.data.IsSuccess && res1.data.Data) {
				$scope.CostClassColl = res1.data.Data;
				var defaultFiscal = $scope.CostClassColl.find(cc => cc.IsDefault);
				if (defaultFiscal) {
					$scope.newBranchWiseLedgerOpening.CostClassId = defaultFiscal.CostClassId;
					$scope.OnFiscalYearChange();
				}
			}
		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});

		$timeout(function () {
			$scope.CurrencyList = [];
			$http({
				method: 'GET',
				url: base_url + "Account/Creation/GetAllCurrencyList",
				dataType: "json"
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.CurrencyList = res.data.Data;

					//Added by suresh on Mangsir 26 to keep Rs as default
					$scope.newBranchWiseLedgerOpening.SelectedCurrency = $scope.CurrencyList[0];
					//Ends
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});
		$timeout(function () {
			$scope.LedgerGroupList = [];
			$http({
				method: 'GET',
				url: base_url + "Account/Creation/GetAllLedgerGroupList",
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.LedgerGroupList = res.data.Data;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});

		$timeout(function () {
			$scope.newBranchWiseLedgerOpening = {
				BranchWiseLedgerOpeningId: null,
				BranchId: 1,
				LedgerGroupId: null,
				CurrencyId: 1,
				CurRate: 1,
			/*	SelectedCurrency: null,*/
				NY: 0,
				NM: 0,
				ND: 0,
				VoucherDate: null,
				VoucherDate_TMP: null,
				OpeningColl: [],
				Mode: 'Save'
			};
			$scope.newBranchWiseLedgerOpening.OpeningColl.push({
				DrCr: 1
			});
		});


		//$scope.GetAllBranchWiseLedgerOpeningList();

	}


	$scope.OnFiscalYearChange = function () {
		var selected = $scope.CostClassColl.find(cc => cc.CostClassId === $scope.newBranchWiseLedgerOpening.CostClassId);
		$scope.newBranchWiseLedgerOpening.VoucherDate_TMP = selected ? new Date(selected.StartDate) : null;
	};

	$scope.ChangeParticularLedger = function (led) {
		if (led.LedgerDetails && led.LedgerId) {
			var voucherDate = new Date();
			if ($scope.newBranchWiseLedgerOpening.VoucherDateDet)
				voucherDate = $filter('date')(new Date($scope.newBranchWiseLedgerOpening.VoucherDateDet.dateAD), 'yyyy-MM-dd');

			var para = {
				BranchId: $scope.newBranchWiseLedgerOpening.BranchId,
				LedgerId: led.LedgerId,
				voucherDate: voucherDate
			};
			$http({
				method: 'POST',
				url: base_url + "Account/Creation/GetLedgerOpeningforBranch",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$timeout(function () {
						var openingDet = res.data.Data;
						led.OpeningId = openingDet.OpeningId;
						led.Amount = openingDet.Amount;
						led.InBoth = openingDet.InBoth;
						led.CurrencyName = openingDet.CurrencyName;
						led.CurRate = openingDet.CurRate;
						led.OpeningId = openingDet.OpeningId;

						if (openingDet.DrAmt > 0)
							led.DrCr = 1;
						else if (openingDet.CrAmt > 0)
							led.DrCr = 2;
						else
							led.DrCr = openingDet.DrCr;

						$scope.ChangeDrCrAmount();
					});

				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});


		} else {
			led.OpeningId = 0;
			led.Amount = 0;
			led.DrCr = 1;

			$scope.ChangeDrCrAmount();
        }
	};
	$scope.ChangeDrCrAmount = function () {

		var totalDr = 0, totalCr = 0;
		angular.forEach($scope.newBranchWiseLedgerOpening.OpeningColl, function (op) {
			if (op.LedgerId || op.LedgerId > 0) {
				if (op.Amount && op.DrCr == 1) {
					totalDr += parseFloat(op.Amount);
				}
				else if (op.Amount && op.DrCr == 2) {
					totalCr += parseFloat(op.Amount);
				}
			}
		});

		$scope.newBranchWiseLedgerOpening.TotalDr = totalDr;
		$scope.newBranchWiseLedgerOpening.TotalCr = totalCr;

	};

	$scope.AddRowInOpening = function (ind) {

		if ($scope.newBranchWiseLedgerOpening.OpeningColl) {
			if ($scope.newBranchWiseLedgerOpening.OpeningColl.length > ind + 1) {

				var selectItem = $scope.newBranchWiseLedgerOpening.OpeningColl[ind + 1];
				if (!selectItem.LedgerId || selectItem.LedgerId == null || selectItem.LedgerId == 0 || selectItem.Amount == 0)
					return;

				$scope.newBranchWiseLedgerOpening.OpeningColl.splice(ind + 1, 0, {
					DrCr: 1,
					LedgerId: 0,
					AgentId: 0,
				});
			}
			else if ($scope.newBranchWiseLedgerOpening.OpeningColl.length == (ind + 1)) {
				var selectItem = $scope.newBranchWiseLedgerOpening.OpeningColl[ind];
				if (!selectItem.LedgerId || selectItem.LedgerId == null || selectItem.LedgerId == 0 || selectItem.Amount == 0)
					return;

				$scope.newBranchWiseLedgerOpening.OpeningColl.push({
					DrCr: 1,
					LedgerId: 0,
					AgentId: 0,
				});
			}
			else {
				$scope.newBranchWiseLedgerOpening.OpeningColl.push({
					DrCr: 1,
					LedgerId: 0,
					AgentId: 0,
				});
			}

		}
		$scope.ChangeDrCrAmount();

	}

	$scope.delRowOpening = function (ind) {
		if ($scope.newBranchWiseLedgerOpening.OpeningColl) {
			if ($scope.newBranchWiseLedgerOpening.OpeningColl.length > 1) {
				$scope.newBranchWiseLedgerOpening.OpeningColl.splice(ind, 1);
				$scope.ChangeDrCrAmount();
			}
		}
	}

	$scope.ClearBranchWiseLedgerOpening = function () {
		$timeout(function () {
			$scope.newBranchWiseLedgerOpening = {
				BranchWiseLedgerOpeningId: null,
				BranchId: 1,
				LedgerGroupId: null,
				CurrencyId: 1,
				CurRate: 1,
				/*SelectedCurrency: null,*/
				NY: 0,
				NM: 0,
				ND: 0,
				VoucherDate: null,
				VoucherDate_TMP: null,
				OpeningColl: [],
				Mode: 'Save'
			};
			$scope.newBranchWiseLedgerOpening.OpeningColl.push({
				DrCr: 1
			});
			$scope.newBranchWiseLedgerOpening.VoucherDate_TMP = new Date($scope.CompDet.StartDate);
			$scope.ChangeDrCrAmount();
		});
	}


	//************************* BranchWiseLedgerOpening *********************************

	$scope.IsValidBranchWiseLedgerOpening = function () {
		if (!$scope.newBranchWiseLedgerOpening.BranchId) {
			Swal.fire('Please ! Select Branch Name');
			return false;
		}

		if (!$scope.newBranchWiseLedgerOpening.SelectedCurrency) {
			Swal.fire('Please ! Select Currency Name');
			return false;
		}
		if (!$scope.newBranchWiseLedgerOpening.CurRate) {
			Swal.fire('Please ! Select Rate');
			return false;
		}
		if (!$scope.newBranchWiseLedgerOpening.CostClassId) {
			Swal.fire('Please ! Select Fiscal Year');
			return false;
		}
		if (!$scope.newBranchWiseLedgerOpening.VoucherDateDet) {
			Swal.fire('Please ! Select Opening Date');
			return false;
		}
		for (var i = 0; i < $scope.newBranchWiseLedgerOpening.OpeningColl.length; i++) {

			var row = $scope.newBranchWiseLedgerOpening.OpeningColl[i];

			if (!row.LedgerId || row.LedgerId == 0) {
				Swal.fire('Please ! Select Ledger in row ' + (i + 1));
				return false;
			}

			if (!row.DrCr) {
				Swal.fire('Please ! Select Dr/Cr in row ' + (i + 1));
				return false;
			}

		}

		return true;
	}

	$scope.SaveUpdateBranchWiseLedgerOpening = function () {
		if ($scope.IsValidBranchWiseLedgerOpening() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBranchWiseLedgerOpening.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBranchWiseLedgerOpening();
					}
				});
			} else
				$scope.CallSaveUpdateBranchWiseLedgerOpening();

		}
	};

	$scope.CallSaveUpdateBranchWiseLedgerOpening = function () {

		var voucherDate = $filter('date')(new Date($scope.newBranchWiseLedgerOpening.VoucherDateDet.dateAD), 'yyyy-MM-dd');

		var dataColl = [];
		angular.forEach($scope.newBranchWiseLedgerOpening.OpeningColl, function (op) {
			if (op.LedgerId || op.LedgerId > 0) {
				var beData = {
					LedgerId: op.LedgerId,
					Amount: op.DrCr == 1 ? op.Amount : -op.Amount ,
					DrCr: op.DrCr,
					BranchId: $scope.newBranchWiseLedgerOpening.BranchId,
					CurrencyId: $scope.newBranchWiseLedgerOpening.SelectedCurrency.CurrencyId,
					VoucherDate: voucherDate,
					NY: $scope.newBranchWiseLedgerOpening.VoucherDateDet.NY,
					NM: $scope.newBranchWiseLedgerOpening.VoucherDateDet.NM,
					ND: $scope.newBranchWiseLedgerOpening.VoucherDateDet.ND,
					OpeningId: op.OpeningId ? op.OpeningId : 0,
					CurrencyId: $scope.newBranchWiseLedgerOpening.SelectedCurrency.CurrencyId,
					CurRate: $scope.newBranchWiseLedgerOpening.CurRate,
					DrAmt: op.DrCr == 1 ? op.Amount : 0,
					CrAmt: op.DrCr == 2 ? op.Amount : 0
				};
				dataColl.push(beData);
			}
		});
		$http({
			method: 'POST',
			url: base_url + "Account/Creation/SaveUpdateLedgerOpeningforBranch",
			dataType: "json",
			data: JSON.stringify(dataColl)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});