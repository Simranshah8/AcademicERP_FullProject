
app.controller('Ledger', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Ledger';

	var glSrv = GlobalServices;
	$scope.LoadData = function () {

		$scope.ProvinceColl = GetStateList();
		$scope.DistrictColl = GetDistrictList();
		$scope.VDCColl = GetVDCList();

		$scope.ProvinceColl_Qry = mx($scope.ProvinceColl);
		$scope.DistrictColl_Qry = mx($scope.DistrictColl);
		$scope.VDCColl_Qry = mx($scope.VDCColl);

		$('.select2').select2({
			allowClear: true,
			openOnEnter: true
		});

		$scope.VoucherSearchOptions = [{ text: 'MobileNo', value: 'Led.CompanyContactNo', searchType: 'text' },{ text: 'Name', value: 'Led.Name', searchType: 'text' }, { text: 'Alias', value: 'Led.Alias', searchType: 'text' }, { text: 'Code', value: 'Led.Code', searchType: 'text' }, { text: 'PanVat', value: 'LS.PanVatNo', searchType: 'text' }, { text: 'PanVat', value: 'LS.PanVatNo', searchType: 'text' }];
		$scope.paginationOptions = {
			pageNumber: 1,
			pageSize: glSrv.getPerPageRow(),
			sort: null,
			SearchType: 'text',
			SearchCol: '',
			SearchVal: '',
			SearchColDet: $scope.VoucherSearchOptions[0],
			pagearray: [],
			pageOptions: [5, 10, 20, 30, 40, 50]
		};

		$scope.PaymentTypeColl = glSrv.getPaymentTypeColl();
		$scope.confirmMSG = glSrv.getConfirmMSG();
		$scope.DrCrList = glSrv.getDrCr();
		//$scope.DocumentTypeList = GlobalServices.getDocumentTypeList();

		$scope.CreditRuleTypeList = [];
		$http({
			method: 'GET',
			url: base_url + "Global/GetCreditRules",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CreditRuleTypeList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.UDFFeildsColl = [];
		var para11 = {
			EntityId: LedgerEntity
		};
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/getUDFByEntitId",
			dataType: "json",
			data: JSON.stringify(para11)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UDFFeildsColl = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.BranchList = [];
		$http({
			method: 'GET',
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
			$scope.SalesManList = [];
			$http({
				method: 'GET',
				url: base_url + "Account/Creation/GetAllSalesMan",
				dataType: "json"
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.SalesManList = res.data.Data;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});

		$timeout(function () {
			$scope.DocumentTypeList = [];
			$scope.DocumentTypeList_Qry = [];
			$http({
				method: 'GET',
				url: base_url + "Global/GetDocumentTypes",
				dataType: "json"
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.DocumentTypeList = res.data.Data;
					$scope.DocumentTypeList_Qry = mx(res.data.Data);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
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
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});


		$timeout(function () {

			$scope.AreaList = [];
			$http({
				method: 'GET',
				url: base_url + "Account/Creation/GetAllAreaMasterList",
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.AreaList = res.data.Data;
					$scope.AreaList_Qry = mx(res.data.Data);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

			$scope.RouteList = [];
			$http({
				method: 'GET',
				url: base_url + "Account/Creation/GetAllDebtorRouteList",
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.RouteList = res.data.Data;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});


		$timeout(function () {
			$scope.DebtorTypeList = [];
			$http({
				method: 'GET',
				url: base_url + "Account/Creation/GetAllDebtorTypeList",
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.DebtorTypeList = res.data.Data;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

			$scope.AditionalCostTypeList = [];
			$http({
				method: 'POST',
				url: base_url + "Account/Creation/GetAditionalCostTypes",
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.AditionalCostTypeList = res.data.Data;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

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

			$scope.DutyTaxTypeList = [];
			$http({
				method: 'POST',
				url: base_url + "Account/Creation/GetTypeOfDutyTaxs",
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.DutyTaxTypeList = res.data.Data;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

			$scope.IncomeExpensesTypeList = [];
			$http({
				method: 'POST',
				url: base_url + "Account/Creation/GetTypeOfIncomeExpenses",
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.IncomeExpensesTypeList = res.data.Data;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

			$scope.DocumentTypeList = [];
			//GlobalServices.getDocumentTypeList().then(function (res) {
			//	$scope.DocumentTypeList = res.data.Data;
			//}, function (reason) {
			//	Swal.fire('Failed' + reason);
			//});



		});


		$scope.newLedger = {
			LedgerId: 0,
			DrCr: 1,
			NameNP: '',
			Name: '',
			Alias: '',
			Code: '', 
			PanVatNo: '',
			Address: '',
			OpeningAmount: 0,
			DuesFrom: null,
			DuesFrom_TMP: null,
			CreditLimitAmount: 0,
			CreditLimitDays: 0,
			CurrencyId: 0,
			DebtorTypeId: null,
			DebtorRouteId: null,
			LedgerGroupId: 12,
			DocumentId: null,
			AreaId: 0,
			AgentId: 0,
			OpeningForBranchId: 1,
			Status: true,
			BillWiseAdjustment: true,
			InventoryValuesAreAffected: false,
			CostCentersAreApplied: false,
			ActiveInterestCalculation: false,
			ActiveChequeDetails: false,
			ActiveRemitDetails: false,
			IsTDS: false,
			IsVat: false,
			IsImportExportLedger: false,
			CreditLimitDaysType: 1,
			AccountNo: '',
			BankName: '',
			SlabWiseInterestRate: false,
			AfterDaysInterestActive: 0,
			InterestRate: 0,
			InterestPer: 0,
			InterestOn: 1,
			AditionalCostOnTheBasis: 1,
			TypeOfIncomeExp: 1,
			TypeOfDutyTax: 0,
			LedgerWiseCostCenter: false,
			IsLC: false,
			LCPartyId: null,
			DocumentColl: [],
			ContactPersons: [],
			BillToColl: [],
			ShipToColl:[],
			Rate: 0,
			BlockCreditTransaction: false,
			BlockDebitTransaction: false,
			Province: '',
			District: '',
			Palika: '',
			Tole: '',
			WardNo: 0,
			Lat: 0,
			Lon: 0,
			ProvinceId1: null,
			DistrictId1: null,
			CityId1: null,
			NotVisible: false,
			CompanyContactNo: '',
			CreditLimitAs: 1,
			StatutoryDetail: {
				PanVatNo: ''
			}

		};

		$scope.ClearContactDetails();
		$scope.ClearBillTo();
		$scope.ClearShipTo();

		//$scope.GetAllLedgerList();

	}

	$scope.GenerateCode = function () {

		if ($scope.newLedger.LedgerId > 0 && $scope.newLedger.Code && $scope.newLedger.Code.length > 0)
			return;

		$scope.newLedger.Code = '';
		var para = {
			name: $scope.newLedger.Name,
			ledgerGroupId: $scope.newLedger.LedgerGroupId
		};
		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetLedgerCode",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			$timeout(function () {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newLedger.Code = res.data.Data.ResponseId;
				}
			});
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.newContactDetails = {};
	$scope.AddNewContactPerson = function () {
		if (!$scope.newContactDetails.Name.isEmpty()) {
			$scope.newLedger.ContactPersons.push($scope.newContactDetails);
			$scope.ClearContactDetails();
		}
	};
	$scope.EditContactPerson = function (ind) {
		$scope.newContactDetails = $scope.newLedger.ContactPersons[ind];
		$scope.newLedger.ContactPersons.splice(ind, 1);
	};
	$scope.DelContactPerson = function (ind) {
		$scope.newLedger.ContactPersons.splice(ind, 1);
	};


	$scope.newBillToDetails = {};
	$scope.AddBillTo = function () {
		if (!$scope.newBillToDetails.AddressId.isEmpty()) {
			$scope.newLedger.BillToColl.push($scope.newBillToDetails);
			$scope.ClearBillTo();
		}
	};
	$scope.EditBillTo = function (ind) {
		$scope.newBillToDetails = $scope.newLedger.BillToColl[ind];
		$scope.newLedger.BillToColl.splice(ind, 1);
	};
	$scope.DelBillTo = function (ind) {
		$scope.newLedger.BillToColl.splice(ind, 1);
	};


	$scope.newShipToDetails = {};
	$scope.AddShipTo = function () {
		if (!$scope.newShipToDetails.AddressId.isEmpty()) {
			$scope.newLedger.ShipToColl.push($scope.newShipToDetails);
			$scope.ClearShipTo();
		}
	};
	$scope.EditShipTo = function (ind) {
		$scope.newShipToDetails = $scope.newLedger.ShipToColl[ind];
		$scope.newLedger.ShipToColl.splice(ind, 1);
	};
	$scope.DelShipTo = function (ind) {
		$scope.newLedger.ShipToColl.splice(ind, 1);
	};

	$scope.CurDocument = {};
	$scope.AddMoreFiles = function () {

		if ($scope.CurDocument.DocumentTypeId > 0 && $scope.CurDocument.Document_TMP) {
			var findDocType = $scope.DocumentTypeList_Qry.firstOrDefault(p1 => p1.DocumentTypeId == $scope.CurDocument.DocumentTypeId);
			var file = $scope.CurDocument.Document_TMP[0];
			if (findDocType) {
				$scope.newLedger.DocumentColl.push({
					DocumentTypeId: findDocType.DocumentTypeId,
					DocumentTypeName: findDocType.Name,
					File: file,
					Name: file.name,
					Type: file.type,
					Size: file.size,
					Description: '',
					DocumentData: $scope.CurDocument.DocumentData,
					DocPath: null
				});

				$scope.CurDocument = {};

				$('#flMoreFiles').val('');
			}
		}
	};
	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newLedger.DocumentColl) {
			if ($scope.newLedger.DocumentColl.length > 0) {
				$scope.newLedger.DocumentColl.splice(ind, 1);
			}
		}
	}
	$scope.ClearLedger = function () {

		angular.forEach($scope.UDFFeildsColl, function (uf) {
			uf.Value = '';
			uf.AlterNetValue = '';
		});

		$timeout(function () {
			$scope.newLedger = {
				LedgerId: 0,
				DrCr: 1,
				NameNP: '',
				Name: '',
				Alias: '',
				Code: '',
				LedgerGroupId: 12,
				PanVatNo: '',
				Address: '',
				OpeningAmount: 0,
				DuesFrom: null,
				DuesFrom_TMP: null,
				CreditLimitAmount: 0,
				CreditLimitDays: 0,
				CurrencyId: 0,
				DebtorTypeId: null,
				DebtorRouteId: null, 
				DocumentId: null,
				AreaId: 0,
				AgentId: 0,
				OpeningForBranchId: 1,
				Status: true,
				BillWiseAdjustment: true,
				InventoryValuesAreAffected: false,
				CostCentersAreApplied: false,
				ActiveInterestCalculation: false,
				ActiveChequeDetails: false,
				ActiveRemitDetails: false,
				IsTDS: false,
				IsVat: false,
				IsImportExportLedger: false,
				CreditLimitDaysType: 1,
				AccountNo: '',
				BankName: '',
				SlabWiseInterestRate: false,
				AfterDaysInterestActive: 0,
				InterestRate: 0,
				InterestPer: 0,
				InterestOn: 1,
				AditionalCostOnTheBasis: 1,
				TypeOfIncomeExp: 1,
				TypeOfDutyTax: 0,
				LedgerWiseCostCenter: false,
				IsLC: false,
				LCPartyId: null,
				DocumentColl: [],
				ContactPersons: [],
				BillToColl: [],
				ShipToColl: [],
				Rate: 0,
				BlockCreditTransaction: false,
				BlockDebitTransaction: false,
				Province: '',
				District: '',
				Palika: '',
				Tole: '',
				WardNo: null,
				Lat: 0,
				Lon: 0,
				ProvinceId1: null,
				DistrictId1: null,
				CityId1: null,
				NotVisible: false,
				CompanyContactNo: '',
				CreditLimitAs:1,
				StatutoryDetail: {
					PanVatNo: ''
				}
			};

			$scope.ClearContactDetails();

			//$('#tab-1st-active').removeClass('resp-tab-item hor_1 active resp-tab-active');
			//$('#tab-2st-active').removeClass('resp-tab-item hor_1 active resp-tab-active');
			//$('#tab-3st-active').removeClass('resp-tab-item hor_1 active resp-tab-active');
			//$('#tab-4st-active').removeClass('resp-tab-item hor_1 active resp-tab-active');
			//$('#tab-1st-active').addClass('resp-tab-item hor_1 active resp-tab-active');

		});

	}

	$scope.ClearContactDetails = function () {
		$scope.newContactDetails = {
			Name: '',
			Post: '',
			Address: '',
			EmailId: '',
			CitizenShipNo: '',
			MobileNo1: '',
			MobileNo2: '',
			TelNo1: '',
			TelNo2: '',
		};
	}

	$scope.ClearBillTo = function () {
		$scope.newBillToDetails = {
			AddressId: '',
			FullAddress: '',
			Province: '',
			District: '',
			Palika: '',
			Tole: '', 
		};
	}

	$scope.ClearShipTo = function () {
		$scope.newShipToDetails = {
			AddressId: '',
			FullAddress: '',
			Province: '',
			District: '',
			Palika: '',
			Tole: '',
		};
	}

	$scope.ClearDocumentAttach = function () {
		$scope.newDocumentAttach = {
			DocumentAttachId: null,
			TypeOfDocument: '',
			AttachDoc: null,

		};
	}
	 
	//ContactDetails Clear photo
	$scope.ClearContactDetailsPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newContactDetails.PhotoData = null;
				$scope.newContactDetails.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};

	//************************* Ledger *********************************

	$scope.IsValidLedger = function () {
		if ($scope.newLedger.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateLedger = function () {
		if ($scope.IsValidLedger() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newLedger.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateLedger();
					}
				});
			} else
				$scope.CallSaveUpdateLedger();

		}
	};

	$scope.CallSaveUpdateLedger = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newLedger.TypeOfDutyTax = 26;

		if ($scope.newLedger.DuesFromDet) {
			$scope.newLedger.DuesFrom = $filter('date')(new Date($scope.newLedger.DuesFromDet.dateAD), 'yyyy-MM-dd');
		}

		var selectData = $('#cboProvince').select2('data');
		if (selectData && selectData.length > 0)
			province = selectData[0].text.trim();

		selectData = $('#cboDistrict').select2('data');
		if (selectData && selectData.length > 0)
			district = selectData[0].text.trim();


		selectData = $('#cboArea').select2('data');
		if (selectData && selectData.length > 0)
			area = selectData[0].text.trim();

		$scope.newLedger.Province = province;
		$scope.newLedger.District = district;
		$scope.newLedger.Palika = area;
		$scope.newLedger.GenerateCode = true;

		if (!$scope.newLedger.AreaId)
			$scope.newLedger.AreaId = 0;

		angular.forEach($scope.newLedger.BillToColl, function (bt) {
			if (bt.ProvinceId1 > 0) {
				var findP = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.id == bt.ProvinceId1);
				if (findP)
					bt.Province = findP.text;
			}

			if (bt.DistrictId1 > 0) {
				var findP = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == bt.DistrictId1);
				if (findP)
					bt.District = findP.text;
			}

			if (bt.PalikaId1 > 0) {
				var findP = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == bt.PalikaId1);
				if (findP)
					bt.Palika = findP.text;
			}

		});

		angular.forEach($scope.newLedger.ShipToColl, function (bt) {
			if (bt.ProvinceId1 > 0) {
				var findP = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.id == bt.ProvinceId1);
				if (findP)
					bt.Province = findP.text;
			}

			if (bt.DistrictId1 > 0) {
				var findP = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == bt.DistrictId1);
				if (findP)
					bt.District = findP.text;
			}

			if (bt.PalikaId1 > 0) {
				var findP = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == bt.PalikaId1);
				if (findP)
					bt.Palika = findP.text;
			}

		});

		var filesColl = $scope.newLedger.DocumentColl;

		$scope.newLedger.UserDefineFieldsColl = [];
		angular.forEach($scope.UDFFeildsColl, function (uf) {
			var uVal = {
				UDFId: uf.Id,
				Value: uf.Value,
				AlterNetValue: uf.Type == 2 ? uf.Value_TMP : uf.Value
			};

			if (uf.Type == 2 && uf.ValueDet)
				uVal.Value = $filter('date')(new Date(uf.ValueDet.dateAD), 'yyyy-MM-dd');

			$scope.newLedger.UserDefineFieldsColl.push(uVal);
		});


		if (!$scope.newLedger.BDId)
			$scope.newLedger.BDId = 0;

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/SaveLedger",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));


				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}

				return formData;
			},
			data: { jsonData: $scope.newLedger, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearLedger();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	}

	$scope.GetAllLedgerList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.LedgerList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetAllLedgerList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LedgerList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetLedgerById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.ClearLedger();
		$timeout(function () {
			var para = {
				LedgerId: refData.LedgerId
			};

			$http({
				method: 'POST',
				url: base_url + "Account/Creation/GetLedgerById",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var resData = res.data.Data;
					$scope.newLedger = resData;
					$scope.newLedger.Mode = 'Modify';

					if ($scope.newLedger.DuesFrom)
						$scope.newLedger.DuesFrom_TMP = new Date($scope.newLedger.DuesFrom);

					var findProvince = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == resData.Province);

					if (findProvince)
						$scope.newLedger.ProvinceId1 = findProvince.id;
					else
						$scope.newLedger.ProvinceId1 = null;

					var findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == resData.District);
					if (findDistrict)
						$scope.newLedger.DistrictId1 = findDistrict.id;
					else
						$scope.newLedger.DistrictId1 = null;

					var findArea = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == resData.Palika);
					if (findArea)
						$scope.newLedger.CityId1 = findArea.id;
					else
						$scope.newLedger.CityId1 = null;


					angular.forEach($scope.newLedger.BillToColl, function (bt) {
						if (bt.Province) {
							var findP = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == bt.Province);
							if (findP)
								bt.ProvinceId1 = findP.id;
						}

						if (bt.District) {
							var findP = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == bt.District);
							if (findP)
								bt.DistrictId1 = findP.id;
						}

						if (bt.Palika) {
							var findP = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == bt.Palika);
							if (findP)
								bt.PalikaId1 = findP.id;
						}

					});

					angular.forEach($scope.newLedger.ShipToColl, function (bt) {
						if (bt.Province) {
							var findP = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == bt.Province);
							if (findP)
								bt.ProvinceId1 = findP.id;
						}

						if (bt.District) {
							var findP = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == bt.District);
							if (findP)
								bt.DistrictId1 = findP.id;
						}

						if (bt.Palika) {
							var findP = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == bt.Palika);
							if (findP)
								bt.PalikaId1 = findP.id;
						}

					});

					var udfQry = mx($scope.newLedger.UserDefineFieldsColl);
					angular.forEach($scope.UDFFeildsColl, function (uf) {
						var findU = udfQry.firstOrDefault(p1 => p1.UDFId == uf.Id);
						if (findU) {

							if (uf.Type == 2) {
								if (uf.Value) {
									uf.Value_TMP = new Date(uf.Value);
								}

							} else {
								uf.Value = findU.Value;
								uf.AlterNetValue = findU.Value;
							}

						}
					});

					$('#searVoucherRightBtn').modal('hide');

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});
	
	};

	$scope.DelLedgerById = function (refData) {

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
					LedgerId: refData.LedgerId
				};

				$http({
					method: 'POST',
					url: base_url + "Account/Creation/DelLedger",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.SearchData();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//************************* ContactDetails *********************************

	$scope.IsValidContactDetails = function () {
		if ($scope.newContactDetails.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateContactDetails = function () {
		if ($scope.IsValidContactDetails() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newContactDetails.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateContactDetails();
					}
				});
			} else
				$scope.CallSaveUpdateContactDetails();

		}
	};

	$scope.CallSaveUpdateContactDetails = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/SaveContactDetails",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newContactDetails }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearContactDetails();
				$scope.GetAllContactDetailsList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	}

	$scope.GetAllContactDetailsList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ContactDetailsList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetAllContactDetailsList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ContactDetailsList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetContactDetailsById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ContactDetailsId: refData.ContactDetailsId
		};

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetContactDetailsById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newContactDetails = res.data.Data;
				$scope.newContactDetails.Mode = 'Modify';

				//document.getElementById('author-section').style.display = "none";
				//document.getElementById('author-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelContactDetailsById = function (refData) {

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
					ContactDetailsId: refData.ContactDetailsId
				};

				$http({
					method: 'POST',
					url: base_url + "Account/Creation/DelContactDetails",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllContactDetailsList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* StatutoryDetails *********************************

	$scope.IsValidStatutoryDetails = function () {
		if ($scope.newStatutoryDetails.BankName.isEmpty()) {
			Swal.fire('Please ! Enter Bank Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateStatutoryDetails = function () {
		if ($scope.IsValidStatutoryDetails() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStatutoryDetails.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStatutoryDetails();
					}
				});
			} else
				$scope.CallSaveUpdateStatutoryDetails();

		}
	};

	$scope.CallSaveUpdateStatutoryDetails = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/SaveStatutoryDetails",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newStatutoryDetails }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStatutoryDetails();
				$scope.GetAllStatutoryDetailsList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	}

	$scope.GetAllStatutoryDetailsList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StatutoryDetailsList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetAllStatutoryDetailsList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StatutoryDetailsList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetStatutoryDetailsById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			StatutoryDetailsId: refData.StatutoryDetailsId
		};

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetStatutoryDetailsById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newStatutoryDetails = res.data.Data;
				$scope.newStatutoryDetails.Mode = 'Modify';

				//document.getElementById('author-section').style.display = "none";
				//document.getElementById('author-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelStatutoryDetailsById = function (refData) {

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
					StatutoryDetailsId: refData.StatutoryDetailsId
				};

				$http({
					method: 'POST',
					url: base_url + "Account/Creation/DelStatutoryDetails",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStatutoryDetailsList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* DocumentAttach *********************************

	$scope.IsValidDocumentAttach = function () {
		if ($scope.newDocumentAttach.TypeOfDocument.isEmpty()) {
			Swal.fire('Please ! Enter Document Type');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateDocumentAttach = function () {
		if ($scope.IsValidDocumentAttach() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDocumentAttach.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDocumentAttach();
					}
				});
			} else
				$scope.CallSaveUpdateDocumentAttach();

		}
	};

	$scope.CallSaveUpdateDocumentAttach = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/SaveDocumentAttach",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newDocumentAttach }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearDocumentAttach();
				$scope.GetAllDocumentAttachList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	}

	$scope.GetAllDocumentAttachList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.DocumentAttachList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetAllDocumentAttachList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DocumentAttachList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetDocumentAttachById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			DocumentAttachId: refData.DocumentAttachId
		};

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetDocumentAttachById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDocumentAttach = res.data.Data;
				$scope.newDocumentAttach.Mode = 'Modify';

				//document.getElementById('author-section').style.display = "none";
				//document.getElementById('author-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelDocumentAttachById = function (refData) {

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
					DocumentAttachId: refData.DocumentAttachId
				};

				$http({
					method: 'POST',
					url: base_url + "Account/Creation/DelDocumentAttach",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllDocumentAttachList();
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

	$scope.SearchDataColl = [];
	$scope.SearchData = function () {

		$scope.loadingstatus = 'running';
		showPleaseWait();
		$scope.paginationOptions.TotalRows = 0;

		var sCol = $scope.paginationOptions.SearchColDet;

		var para = {
			filter: {
				DateFrom: null,
				DateTo: null,
				PageNumber: $scope.paginationOptions.pageNumber,
				RowsOfPage: $scope.paginationOptions.pageSize,
				SearchCol: (sCol ? sCol.value : ''),
				SearchVal: $scope.paginationOptions.SearchVal,
				SearchType: (sCol ? sCol.searchType : 'text'),
				For:1
			}
		};

		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetLedgerLst",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			$scope.loadingstatus = 'stop';
			hidePleaseWait();

			if (res.data.IsSuccess && res.data.Data) {
				$scope.SearchDataColl = res.data.Data;
				$scope.paginationOptions.TotalRows = res.data.TotalCount;
				$('#searVoucherRightBtn').modal('show');

			} else
				alert(res.data.ResponseMSG);

		}, function (reason) {
			alert('Failed' + reason);
		});


	};

	$scope.ReSearchData = function (pageInd) {

		$timeout(function () {
			if (pageInd && pageInd >= 0)
				$scope.paginationOptions.pageNumber = pageInd;
			else if (pageInd == -1)
				$scope.paginationOptions.pageNumber = 1;

			$scope.loadingstatus = 'running';
			showPleaseWait();
			$scope.paginationOptions.TotalRows = 0;
			var sCol = $scope.paginationOptions.SearchColDet;

			var para = {
				filter: {
					DateFrom: null,
					DateTo: null,
					PageNumber: $scope.paginationOptions.pageNumber,
					RowsOfPage: $scope.paginationOptions.pageSize,
					SearchCol: (sCol ? sCol.value : ''),
					SearchVal: $scope.paginationOptions.SearchVal,
					SearchType: (sCol ? sCol.searchType : 'text'),
					For: 1
				}
			};

			$http({
				method: 'POST',
				url: base_url + "Account/Creation/GetLedgerLst",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				$scope.loadingstatus = 'stop';
				hidePleaseWait();

				if (res.data.IsSuccess && res.data.Data) {
					$scope.SearchDataColl = res.data.Data;
					$scope.paginationOptions.TotalRows = res.data.TotalCount;

				} else
					alert(res.data.ResponseMSG);

			}, function (reason) {
				alert('Failed' + reason);
			});
		});


	}

	$scope.ShowPersonalImg = function (docDet) {
		$scope.viewImg = {
			ContentPath: '',
			File: null,
			FileData: null
		};
		if (docDet.DocPath || docDet.File) {
			$scope.viewImg.ContentPath = docDet.DocPath;
			$scope.viewImg.File = docDet.File;
			$scope.viewImg.FileData = docDet.DocumentData;
			$('#PersonalImg').modal('show');
		} else
			Swal.fire('No Image Found');

	};

	$scope.ChangeAreaSelection = function (areaId) {
		if ($scope.AreaList_Qry) {
			var findArea = $scope.AreaList_Qry.firstOrDefault(p1 => p1.AreaId == areaId);
			if (findArea) {

				var findProvince = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == findArea.State);

				if (findProvince)
					$scope.newLedger.ProvinceId1 = findProvince.id;
				else
					$scope.newLedger.ProvinceId1 = null;

				var findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == findArea.District);
				if (findDistrict)
					$scope.newLedger.DistrictId1 = findDistrict.id;
				else
					$scope.newLedger.DistrictId1 = null;

				var findArea1 = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == findArea.City);
				if (findArea1)
					$scope.newLedger.CityId1 = findArea1.id;
				else
					$scope.newLedger.CityId1 = null;

				$scope.newLedger.Tole = findArea.Name;

            }
        }
    }


});