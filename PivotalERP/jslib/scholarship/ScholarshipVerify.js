
app.controller('VerifyController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Verify';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
				
		$scope.currentPages = {
			Verify: 1,
		};

		$scope.searchData = {
			Verify: '',
		};

		$scope.perPage = {
			Verify: GlobalServices.getPerPageRow(),
		};
		$scope.GenderColl = GlobalServices.getGenderList();
		$scope.SchoolTypeList = [{ id: 1, text: 'Government (Community)' }, { id: 2, text: 'Private' }, { id: 3, text: 'Others' }]
		$scope.ScholarshipTypeList = [{ id: 1, text: 'General' }, { id: 2, text: 'Reservation' }, { id: 3, text: 'General+Reservation' }]

		$scope.BCCertyTypeList = [{ id: 1, text: 'Citizenship Certificate' }, { id: 2, text: 'Birth Certificate' }]
		$scope.PriorityList = [{ id: 1, text: 'First' }, { id: 2, text: 'Second' }, { id: 3, text: 'Third' }, { id: 4, text: 'Fourth' }, { id: 5, text: 'Fifth' }]
		$scope.AppliedSubjectist = [{ id: 1, text: 'Science' }, { id: 2, text: 'Management' }, { id: 3, text: 'Education' }, { id: 4, text: 'Humanities' }]

		$scope.WardList = [
			{ id: 1, text: '1' },
			{ id: 2, text: '2' },
			{ id: 3, text: '3' },
			{ id: 4, text: '4' },
			{ id: 5, text: '5' },
			{ id: 6, text: '6' },
			{ id: 7, text: '7' },
			{ id: 8, text: '8' },
			{ id: 9, text: '9' },
			{ id: 10, text: '10' },
			{ id: 11, text: '11' },
			{ id: 12, text: '12' },
			{ id: 13, text: '13' },
			{ id: 14, text: '14' },
			{ id: 15, text: '15' },
			{ id: 16, text: '16' },
			{ id: 17, text: '17' },
			{ id: 18, text: '18' },
			{ id: 19, text: '19' },
			{ id: 20, text: '20' },
			{ id: 21, text: '21' },
			{ id: 22, text: '22' },
			{ id: 23, text: '23' },
			{ id: 24, text: '24' },
			{ id: 25, text: '25' },
			{ id: 26, text: '26' },
			{ id: 27, text: '27' },
			{ id: 28, text: '28' },
			{ id: 29, text: '29' },
			{ id: 30, text: '30' },
			{ id: 31, text: '31' },
			{ id: 32, text: '32' },
			{ id: 33, text: '33' }
		];

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

		$scope.DayColl = [
			{ id: 1, text: '1' },
			{ id: 2, text: '2' },
			{ id: 3, text: '3' },
			{ id: 4, text: '4' },
			{ id: 5, text: '5' },
			{ id: 6, text: '6' },
			{ id: 7, text: '7' },
			{ id: 8, text: '8' },
			{ id: 9, text: '9' },
			{ id: 10, text: '10' },
			{ id: 11, text: '11' },
			{ id: 12, text: '12' },
			{ id: 13, text: '13' },
			{ id: 14, text: '14' },
			{ id: 15, text: '15' },
			{ id: 16, text: '16' },
			{ id: 17, text: '17' },
			{ id: 18, text: '18' },
			{ id: 19, text: '19' },
			{ id: 20, text: '20' },
			{ id: 21, text: '21' },
			{ id: 22, text: '22' },
			{ id: 23, text: '23' },
			{ id: 24, text: '24' },
			{ id: 25, text: '25' },
			{ id: 26, text: '26' },
			{ id: 27, text: '27' },
			{ id: 28, text: '28' },
			{ id: 29, text: '29' },
			{ id: 30, text: '30' },
			{ id: 31, text: '31' },
			{ id: 32, text: '32' }
		];
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

		$scope.newDet = {
			IsEligible: false
		};

		$scope.ProvinceColl = GetStateList();
		$scope.DistrictColl = GetDistrictList();
		$scope.VDCColl = GetVDCList();

		$scope.ProvinceColl_Qry = mx($scope.ProvinceColl);
		$scope.DistrictColl_Qry = mx($scope.DistrictColl);
		$scope.VDCColl_Qry = mx($scope.VDCColl);

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
			V_CtznshipBack_FilePath:false,
			V_Mode: 'Save'
		};

		$scope.GetAllVerifyList();
	};


	function OnClickDefault() {
		document.getElementById('verifyform').style.display = "none";		

		document.getElementById('back-list').onclick = function () {
			document.getElementById('firstpage').style.display = "block";
			document.getElementById('verifyform').style.display = "none";
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
			V_CtznshipBack_FilePath:false,
			Mode: 'Save'
		};	
		
	};


	$scope.GetAllVerifyList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.VerifyList = [];
		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllScholarship",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.VerifyList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}


	$scope.GetVerifyById = function (refData) {
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

				
				$timeout(function () {
					$scope.GetScholarshipVerifyById();
				});

				document.getElementById('firstpage').style.display = "none";
				document.getElementById('verifyform').style.display = "block";


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
			url: base_url + "Scholarship/getScholarshipVerifyById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newVerify = res.data.Data;
				

				//document.getElementById('source-section').style.display = "none";
				//document.getElementById('source-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.toggleAll = function () {
		var toggleStatus = $scope.newVerify.VerifyAll;
		for (var key in $scope.newVerify) {
			if ($scope.newVerify.hasOwnProperty(key) && key !== 'VerifyAll' && key !== 'V_TranId') {
				$scope.newVerify[key] = toggleStatus;
			}
		}
	};



	$scope.IsValidScholarshipVerify = function () {
		if (!$scope.newVerify.Remarks) {
			Swal.fire('Please ! Enter Remarks');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateScholarshipVerify = function () {
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
						$scope.CallSaveUpdateScholarshipVerify();
					}
				});
			} else
				$scope.CallSaveUpdateScholarshipVerify();
		}
	};

	$scope.CallSaveUpdateScholarshipVerify = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.newVerify.TranId = $scope.newScholarship.TranId;
		$http({
			method: 'POST',
			url: base_url + "Scholarship/SaveScholarshipVerify",
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

});