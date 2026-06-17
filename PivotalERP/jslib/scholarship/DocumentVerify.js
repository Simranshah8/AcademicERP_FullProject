
app.controller('VerifyController', function ($scope, $http, $timeout, $filter, $translate, $rootScope, GlobalServices) {
	$scope.Title = 'Verify';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.newFilter = {
			SubjectId: 0,
			StatusId: -1,
		};
		$scope.VoucherSearchOptions = [{ text: 'SymbolNo', value: 'TR.SEESymbolNo', searchType: 'text' }, { text: 'MobileNo', value: 'TR.MobileNo', searchType: 'text' }, { text: 'Email', value: 'TR.Email', searchType: 'text' }, { text: 'Name', value: 'TR.CandidateName', searchType: 'text' }, { text: 'DOB', value: 'TR.DOB_BS', searchType: 'text' }];
		$scope.paginationOptions = {
			pageNumber: 1,
			pageSize: 20,
			sort: null,
			SearchType: 'text',
			SearchCol: '',
			SearchVal: '',
			SearchColDet: $scope.VoucherSearchOptions[0],
			pagearray: [],
			pageOptions: [5, 10, 20, 30, 40, 50]
		};

		//$scope.currentPages = {
		//	Verify: 1,
		//};

		//$scope.searchData = {
		//	Verify: '',
		//};

		//$scope.perPage = {
		//	Verify: GlobalServices.getPerPageRow(),
		//};

		$scope.ClassList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassList",
			dataType: "json"
		}).then(function (res) {			 
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassList = res.data.Data;
			}  
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GenderColl = GlobalServices.getGenderList();
		$scope.SchoolTypeList = [{ id: 1, text: 'Government (Community)' }, { id: 2, text: 'Private' }, { id: 3, text: 'Others' }]
		$scope.VerificationStatusList = [{ id: 0, text: 'Not Verified' }, { id: 1, text: 'Under Verified' }, { id: 2, text: 'Verified' }, { id: 3, text: 'Rejected' }, { id: -1, text: 'All' }];

		$scope.BCCertyTypeList = [{ id: 1, text: 'Citizenship Certificate' }, { id: 2, text: 'Birth Certificate' }]

		$scope.CandidateSearchOptions = [{ id: 1, text: 'Name' }, { id: 2, text: 'SymbolNo' }, { id: 3, text: 'MobileNo' }]
		$scope.ScholarshipTypeList = [
			{ id: 1, text: 'General' },
			{ id: 8, text: 'General + Reservation' },
			{ id: 2, text: 'Permanent Resident of Kathmandu Metropolitan City' },
			{ id: 3, text: 'Landfill Site' },
			{ id: 4, text: 'General + Permanent Resident of Kathmandu Metropolitan City' },
			{ id: 5, text: 'General + Permanent Resident of Kathmandu Metropolitan City + Reservation' },
			{ id: 6, text: 'General + Landfill Site' },
			{ id: 7, text: 'General + Landfill Site + Reservation' }

		]
		$scope.WardList = [];

		for (let i = 1; i <= 33; i++) {
			$scope.WardList.push({ id: i, text: i.toString() });
		}

		$rootScope.ChangeLanguage();

		$scope.newDet = {
			SelectCandidate: 'Name'
		}

		$scope.BSYearColl = [
			{ id: 1, text: '2045' },
			{ id: 2, text: '2046' },
			{ id: 3, text: '2047' },
			{ id: 4, text: '2048' },
			{ id: 5, text: '2049' },
			{ id: 6, text: '2050' },
			{ id: 7, text: '2051' },
			{ id: 8, text: '2052' },
			{ id: 9, text: '2053' },
			{ id: 10, text: '2054' },
			{ id: 11, text: '2055' },
			{ id: 12, text: '2056' },
			{ id: 13, text: '2057' },
			{ id: 14, text: '2058' },
			{ id: 15, text: '2059' },
			{ id: 16, text: '2060' },
			{ id: 17, text: '2061' },
			{ id: 18, text: '2062' },
			{ id: 19, text: '2063' },
			{ id: 20, text: '2064' },
			{ id: 21, text: '2065' },
			{ id: 22, text: '2066' },
			{ id: 23, text: '2067' },
			{ id: 24, text: '2068' },
			{ id: 25, text: '2069' },
			{ id: 26, text: '2070' },
			{ id: 27, text: '2071' },
			{ id: 28, text: '2072' },
			{ id: 29, text: '2073' },
			{ id: 30, text: '2074' },
			{ id: 31, text: '2075' },
			{ id: 32, text: '2076' },
			{ id: 33, text: '2077' },
			{ id: 34, text: '2078' },
			{ id: 35, text: '2079' },
			{ id: 36, text: '2080' }
		];

		$scope.ADYearColl = [
			{ id: 1, text: '1995' },
			{ id: 2, text: '1996' },
			{ id: 3, text: '1997' },
			{ id: 4, text: '1998' },
			{ id: 5, text: '1999' },
			{ id: 6, text: '2000' },
			{ id: 7, text: '2001' },
			{ id: 8, text: '2002' },
			{ id: 9, text: '2003' },
			{ id: 10, text: '2004' },
			{ id: 11, text: '2005' },
			{ id: 12, text: '2006' },
			{ id: 13, text: '2007' },
			{ id: 14, text: '2008' },
			{ id: 15, text: '2009' },
			{ id: 16, text: '2010' },
			{ id: 17, text: '2011' },
			{ id: 18, text: '2012' },
			{ id: 19, text: '2013' },
			{ id: 20, text: '2014' },
			{ id: 21, text: '2015' }
		];

		$scope.DayColl = [];

		for (let i = 1; i <= 32; i++) {
			$scope.DayColl.push({ id: i, text: i.toString() });
		}

		$scope.BSMonthColl = [
			{ id: 1, text: 'Baishakh' },
			{ id: 2, text: 'Jestha' },
			{ id: 3, text: 'Ashadh' },
			{ id: 4, text: 'Shrawan' },
			{ id: 5, text: 'Bhadra' },
			{ id: 6, text: 'Ashwin' },
			{ id: 7, text: 'Kartik' },
			{ id: 8, text: 'Mangsir' },
			{ id: 9, text: 'Poush' },
			{ id: 10, text: 'Magh' },
			{ id: 11, text: 'Falgun' },
			{ id: 12, text: 'Chaitra' }
		];
		$scope.ADMonthColl = [
			{ id: 1, text: 'January' },
			{ id: 2, text: 'February' },
			{ id: 3, text: 'March' },
			{ id: 4, text: 'April' },
			{ id: 5, text: 'May' },
			{ id: 6, text: 'June' },
			{ id: 7, text: 'July' },
			{ id: 8, text: 'August' },
			{ id: 9, text: 'September' },
			{ id: 10, text: 'October' },
			{ id: 11, text: 'November' },
			{ id: 12, text: 'December' }
		];



		$scope.ProvinceColl = GetStateList();
		$scope.DistrictColl = GetDistrictList();
		$scope.VDCColl = GetVDCList();

		$scope.ProvinceColl_Qry = mx($scope.ProvinceColl);
		$scope.DistrictColl_Qry = mx($scope.DistrictColl);
		$scope.VDCColl_Qry = mx($scope.VDCColl);

		$scope.SubjectList = {};
		GlobalServices.getSubjectList().then(function (res) {
			$scope.SubjectList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.BoardList = [];
		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllEquivalentBoard",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BoardList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.SchoolList = [];
		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllAppliedSchool",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SchoolList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ReservationGroupList = [];
		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllReservationGroup",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ReservationGroupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ReservationTypeList = [];
		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllReservationType",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ReservationTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.AuthorityList = [];
		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllAuthority",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AuthorityList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newDet = {
			SelectLan: 'np'
		};



		$scope.newVerify = {
			V_TranId: null,
			V_FirstName: false,
			V_MiddleName: false,
			V_LastName: false,
			V_Gender: false,
			V_SymbolNoAlphabet: false,
			V_Alphabet: false,
			V_GPA: false,
			V_Email: false,
			V_MobileNo: false,
			V_F_FirstName: false,
			V_F_MiddleName: false,
			V_F_LastName: false,
			V_M_FirstName: false,
			V_M_MiddleName: false,
			V_M_LastName: false,
			V_GF_FirstName: false,
			V_GF_MiddleName: false,
			V_GF_LastName: false,
			V_P_Province: false,
			V_P_District: false,
			V_P_LocalLevel: false,
			V_P_WardNo: false,
			V_P_ToleStreet: false,
			V_Temp_Province: false,
			V_Temp_District: false,
			V_Temp_LocalLevel: false,
			V_Temp_WardNo: false,
			V_Temp_ToleStreet: false,
			V_BC_CertificateType: false,
			V_BC_CertificateNo: false,
			V_BC_IssuedDistrict: false,
			V_BC_IssuedLocalLevel: false,
			V_BC_IssuedWardNo: false,
			V_BC_IssuedToleStreet: false,
			V_BC_DocumentName: false,
			V_BC_FilePath: false,
			V_SymbolNo_Alphabet: false,
			V_ObtainedGPA: false,
			V_EquivalentBoardId: false,
			V_Character_Transfer_Certi: false,
			V_GradeSheetFilePath: false,
			V_SchoolName: false,
			V_SchoolEMISCode: false,
			V_SchoolTypeId: false,
			V_SchoolDistrict: false,
			V_SchoolLocalLevel: false,
			V_SchoolWardNo: false,
			V_SchoolToleStreet: false,
			V_AppliedSubject: false,
			V_CollegePriority: false,
			V_ReservationType: false,
			V_PovCerti_RefNo: false,
			V_PovCerti_IssuedDistrict: false,
			V_PovCerti_IssuedLocalLevel: false,
			V_PovCerti_WardNo: false,
			V_PovCerti_ToleStreet: false,
			V_IssuerName: false,
			V_IssuerDesignation: false,
			V_Poverty_CertiFilePath: false,
			V_GovSchoolCerti_RefNo: false,
			V_GovSchoolCertiPath: false,
			V_ReservationGroup: false,
			V_ConcernedAuthority: false,
			V_GrpCerti_IssuedDistrict: false,
			V_GrpCerti_IssuedLocalLevel: false,
			V_GrpCertiIssue_WardNo: false,
			V_GrpCertiIssue_ToleStreet: false,
			V_GroupWiseCerti_RefNo: false,
			V_GroupWiseCerti_Path: false,
			Remarks: '',
			VerifyAll: false,
			V_ScholarshipType: false,
			V_PovCerti_IssuedDate: false,
			V_GroupWiseCerti_IssuedDate: false,
			V_GovSchoolCerti_IssuedDate: false,
			V_Certi_IssuedDate: false,
			V_EquivalentBoard: false,
			V_CtznshipFront_FilePath: false,
			V_CtznshipBack_FilePath: false,
			V_Mode: 'Save',
			//Added by suresh on 2 shrawan
			V_Gradesheet_Certi: false,
			V_RelatedSchoolFilePath: false,
			V_RelatedSchoolIssueMiti: false,
			V_RelatedSchoolRefNo: false
		};

		$scope.GetAllVerifyList();
	};

	$scope.ChangeLng = function () {
		$translate.use($scope.newDet.SelectLan);
	};

	function OnClickDefault() {
		document.getElementById('verify-form').style.display = "none";

		document.getElementById('back-list').onclick = function () {
			document.getElementById('firstpage').style.display = "block";
			document.getElementById('verify-form').style.display = "none";
			$scope.ClearScholarship();
		}
	};


	$scope.ClearScholarship = function () {
		$scope.newVerify = {
			V_TranId: null,
			V_FirstName: false,
			V_MiddleName: false,
			V_LastName: false,
			V_Gender: false,
			V_SymbolNoAlphabet: false,
			V_Alphabet: false,
			V_GPA: false,
			V_Email: false,
			V_MobileNo: false,
			V_F_FirstName: false,
			V_F_MiddleName: false,
			V_F_LastName: false,
			V_M_FirstName: false,
			V_M_MiddleName: false,
			V_M_LastName: false,
			V_GF_FirstName: false,
			V_GF_MiddleName: false,
			V_GF_LastName: false,
			V_P_Province: false,
			V_P_District: false,
			V_P_LocalLevel: false,
			V_P_WardNo: false,
			V_P_ToleStreet: false,
			V_Temp_Province: false,
			V_Temp_District: false,
			V_Temp_LocalLevel: false,
			V_Temp_WardNo: false,
			V_Temp_ToleStreet: false,
			V_BC_CertificateType: false,
			V_BC_CertificateNo: false,
			V_BC_IssuedDistrict: false,
			V_BC_IssuedLocalLevel: false,
			V_BC_IssuedWardNo: false,
			V_BC_IssuedToleStreet: false,
			V_BC_DocumentName: false,
			V_BC_FilePath: false,
			V_SymbolNo_Alphabet: false,
			V_ObtainedGPA: false,
			V_EquivalentBoardId: false,
			V_Character_Transfer_Certi: false,
			V_GradeSheetFilePath: false,
			V_SchoolName: false,
			V_SchoolEMISCode: false,
			V_SchoolTypeId: false,
			V_SchoolDistrict: false,
			V_SchoolLocalLevel: false,
			V_SchoolWardNo: false,
			V_SchoolToleStreet: false,
			V_AppliedSubject: false,
			V_CollegePriority: false,
			V_ReservationType: false,
			V_PovCerti_RefNo: false,
			V_PovCerti_IssuedDistrict: false,
			V_PovCerti_IssuedLocalLevel: false,
			V_PovCerti_WardNo: false,
			V_PovCerti_ToleStreet: false,
			V_IssuerName: false,
			V_IssuerDesignation: false,
			V_Poverty_CertiFilePath: false,
			V_GovSchoolCerti_RefNo: false,
			V_GovSchoolCertiPath: false,
			V_ReservationGroup: false,
			V_ConcernedAuthority: false,
			V_GrpCerti_IssuedDistrict: false,
			V_GrpCerti_IssuedLocalLevel: false,
			V_GrpCertiIssue_WardNo: false,
			V_GrpCertiIssue_ToleStreet: false,
			V_GroupWiseCerti_RefNo: false,
			V_GroupWiseCerti_Path: false,
			Remarks: '',
			VerifyAll: false,
			V_ScholarshipType: false,
			V_PovCerti_IssuedDate: false,
			V_GroupWiseCerti_IssuedDate: false,
			V_GovSchoolCerti_IssuedDate: false,
			V_Certi_IssuedDate: false,
			V_EquivalentBoard: false,
			V_CtznshipFront_FilePath: false,
			V_CtznshipBack_FilePath: false,
			V_Mode: 'Save',
			//Added by suresh on 2 shrawan
			V_Gradesheet_Certi: false,
			V_RelatedSchoolFilePath: false,
			V_RelatedSchoolIssueMiti: false,
			V_RelatedSchoolRefNo: false
		};
	};

	$scope.GetAllVerifyList = function (pageInd, forVerify) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if (pageInd && pageInd >= 0)
			$scope.paginationOptions.pageNumber = pageInd;
		else if (pageInd == -1)
			$scope.paginationOptions.pageNumber = 1;

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
				SearchType: (sCol ? sCol.searchType : 'text')
			},
			StatusId: $scope.newFilter.StatusId,
			SubjectId: $scope.newFilter.SubjectId,
			ClassId:$scope.newFilter.ClassId,
		};


		$scope.VerifyList = [];
		$http({
			method: 'POST',
			url: base_url + "Scholarship/GetAllScholarship",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				$scope.VerifyList = res.data.Data;
				$scope.paginationOptions.TotalRows = res.data.TotalCount;

				if (forVerify == true) {
					if ($scope.VerifyList.length > 0) {
						$scope.GetVerifyById($scope.VerifyList[0], 0);
					}
				}
			}
			else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.CheckUnCheckList = function () {
		var val = $scope.beData.CheckedAll;
		angular.forEach($scope.VerifyList, function (cl) {
			cl.IsAllow = val;
		});
	}

	$scope.NextCandidate = function () {
		//var nind = $scope.CurIndex + 1;
		//if ($scope.VerifyList.length > nind) {
		//	$scope.GetVerifyById($scope.VerifyList[nind], nind);
		//} else {
		//	$scope.CurIndex = -1;
		//	$scope.paginationOptions.pageNumber = $scope.paginationOptions.pageNumber + 1;
		//	$scope.GetAllVerifyList($scope.paginationOptions.pageNumber);			
		//	//Swal.fire		 
		//      }

		$scope.CurIndex = -1;
		$scope.paginationOptions.pageNumber = 1;
		$scope.GetAllVerifyList($scope.paginationOptions.pageNumber, true);
		//Swal.fire

	}

	$scope.CurIndex = -1;
	$scope.GetVerifyById = function (refData, ind) {
		$scope.CurIndex = ind;
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Scholarship/getScholarshipById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				/*		*//*$scope.newVerify.Email = res.data.Data.Email;*/
				$scope.newScholarship = res.data.Data;

				if ($scope.newDet.YearlyBillPaymentDateDet) {
					$scope.newDet.YearlyBillPaymentDate = $filter('date')(new Date($scope.newDet.YearlyBillPaymentDateDet.dateAD), 'yyyy-MM-dd');
				}
				if ($scope.newScholarship.DOB)
					$scope.newScholarship.DOB_TMP = new Date($scope.newScholarship.DOB);

				if ($scope.newScholarship.BC_IssuedDate)
					$scope.newScholarship.BC_IssuedDate_TMP = new Date($scope.newScholarship.BC_IssuedDate);

				if ($scope.newScholarship.Certi_IssuedDate)
					$scope.newScholarship.Certi_IssuedDate_TMP = new Date($scope.newScholarship.Certi_IssuedDate);

				if ($scope.newScholarship.GovSchoolCerti_IssuedDate)
					$scope.newScholarship.GovSchoolCerti_IssuedDate_TMP = new Date($scope.newScholarship.GovSchoolCerti_IssuedDate);

				if ($scope.newScholarship.PovCerti_IssuedDate)
					$scope.newScholarship.PovCerti_IssuedDate_TMP = new Date($scope.newScholarship.PovCerti_IssuedDate);

				if ($scope.newScholarship.GroupWiseCerti_IssuedDate)
					$scope.newScholarship.GroupWiseCerti_IssuedDate_TMP = new Date($scope.newScholarship.GroupWiseCerti_IssuedDate);

				$scope.newDet = {
					SelectDoc: true
				};
				$timeout(function () {
					$scope.GetScholarshipVerifyById();
				});




			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.ViewFormbyId = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Scholarship/getScholarshipById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newScholarship = res.data.Data;
				/*$scope.newScholarship.Mode = 'Save';*/
				if ($scope.newDet.YearlyBillPaymentDateDet) {
					$scope.newDet.YearlyBillPaymentDate = $filter('date')(new Date($scope.newDet.YearlyBillPaymentDateDet.dateAD), 'yyyy-MM-dd');
				}
				if ($scope.newScholarship.DOB)
					$scope.newScholarship.DOB_TMP = new Date($scope.newScholarship.DOB);

				if ($scope.newScholarship.BC_IssuedDate)
					$scope.newScholarship.BC_IssuedDate_TMP = new Date($scope.newScholarship.BC_IssuedDate);

				if ($scope.newScholarship.Certi_IssuedDate)
					$scope.newScholarship.Certi_IssuedDate_TMP = new Date($scope.newScholarship.Certi_IssuedDate);

				if ($scope.newScholarship.GovSchoolCerti_IssuedDate)
					$scope.newScholarship.GovSchoolCerti_IssuedDate_TMP = new Date($scope.newScholarship.GovSchoolCerti_IssuedDate);

				if ($scope.newScholarship.PovCerti_IssuedDate)
					$scope.newScholarship.PovCerti_IssuedDate_TMP = new Date($scope.newScholarship.PovCerti_IssuedDate);

				if ($scope.newScholarship.GroupWiseCerti_IssuedDate)
					$scope.newScholarship.GroupWiseCerti_IssuedDate_TMP = new Date($scope.newScholarship.GroupWiseCerti_IssuedDate);

				$('#formpreview').modal('show');

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.GetScholarshipVerifyById = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: $scope.newScholarship.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "Scholarship/getScholarshipDocVerifyById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newVerify = res.data.Data;
				$scope.newVerify.Email = $scope.newScholarship.Email;

				$scope.newVerify.ScholarshipDet = $scope.newScholarship;

				document.getElementById('firstpage').style.display = "none";
				document.getElementById('verify-form').style.display = "block";

				$scope.newVerify.ReservationGroupList = $scope.newScholarship.ReservationGroupList;
				$scope.ChangeDocStatus();

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};



	//docverify save js
	$scope.IsValidScholarshipVerify = function () {
		if (!$scope.newVerify.Remarks) {
			Swal.fire('Please ! Enter Remarks');
			return false;
		}
		return true;
	}

	$scope.SaveScholarshipDocVerify = function () {
		if ($scope.IsValidScholarshipVerify() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newVerify.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateScholarshipDocVerify();
					}
				});
			} else
				$scope.CallSaveUpdateScholarshipDocVerify();
		}
	};

	$scope.CallSaveUpdateScholarshipDocVerify = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.newVerify.TranId = $scope.newScholarship.TranId;
		if ($scope.newScholarship.ReservationGroupList) {
			$scope.newVerify.ReservationGroupList = angular.copy($scope.newScholarship.ReservationGroupList);
		}

		$http({
			method: 'POST',
			url: base_url + "Scholarship/SaveScholarshipDocVerify",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newVerify }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearScholarship();
				/*$scope.GetAllScholarshipVerifyList();*/
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.ChangeDocStatus = function () {
		if ($scope.newVerify.V_Status == 1) {
			$scope.newVerify.V_Subject = 'छात्रवृत्तिको आवेदन फाराम Unverified भएको बारे । ';
		}
		else if ($scope.newVerify.V_Status == 2) {
			$scope.newVerify.V_Subject = 'छात्रवृत्तिको आवेदन फाराम Verified भएको बारे । ';
		}
		else if ($scope.newVerify.V_Status == 3) {
			$scope.newVerify.V_Subject = 'छात्रवृत्तिको आवेदन फाराम Rejected भएको बारे । ';
		}

		$scope.GetRemarks();
	}

	$scope.GetRemarks = function () {

		if ($scope.newVerify && $scope.newVerify.ScholarshipDet) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Scholarship/getDocVerifyText",
				dataType: "json",
				data: JSON.stringify($scope.newVerify)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newVerify.Remarks = res.data.Data.ResponseMSG;
					$scope.newVerify.SMSText = res.data.Data.ResponseId;
				}
				else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.ResetPwd = function () {
		if ($scope.VerifyList) {
			var idColl = [];
			$scope.VerifyList.forEach(function (vl) {
				if (vl.IsAllow == true) {
					idColl.push(vl.TranId);
				}
			});

			if (idColl.length > 0) {

				$scope.loadingstatus = "running";
				showPleaseWait();

				$http({
					method: 'POST',
					url: base_url + "Scholarship/ResetPwd",
					dataType: "json",
					data: JSON.stringify(idColl)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				Swal.fire('Please ! Select Student');
			}
		}
	}

});