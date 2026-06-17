app.controller('TeacherProfileController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'MyProfile';
	var gSrv = GlobalServices;
	

	$scope.LoadData=function() {

		$scope.GenderColl = gSrv.getGenderList();
		$scope.BloodGroupList = gSrv.getBloodGroupList();
		$scope.ReligionList = gSrv.getReligionList();
		$scope.CountryList = gSrv.getCountryList();
		$scope.DisablityList = gSrv.getDisablityList();

		$scope.CasteList = [];
		$http({
			method: 'POST',
			url: base_url + "MyProfile/Creation/GetAllCaste",
			dataType: "json"
		}).then(function (res) {
			if (res.data) {
				$scope.CasteList = res.data;

				$timeout(function () {
					$scope.GetAllMyProfile();
				});
			} 
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.CanEdit = {
			PerfornalInfo: false,
			AcademicQualification: false,
			PAddress: false,
			TAddress: false,
			Citizenship: false,
			License: false,
			Passport: false,
			LifeInsurance: false,
			HealthInsurance: false,
			CIT: false,
			AccountDetail: false,
			Contact: false,
			EContact: false,
			OfficialDet: false,
			OtherDet: false,
			Supervisor: false,
			ContactInfo: false,
		};

		
	};

	$scope.EditProfile = function () {
		$scope.CanEdit.PerfornalInfo = true;
	};
	$scope.UpdateProfile = function () {


		var beData = {
			DOB_AD: null,
			AnniversaryDate: null,
			BloodGroup: $scope.MyProfile.BloodGroup,
			Religion: $scope.MyProfile.Religion,
			Nationality: $scope.MyProfile.Nationality,
			CasteId: $scope.MyProfile.CasteId,
			MaritalStatus: $scope.MyProfile.MaritalStatus,
			SpouseName: $scope.MyProfile.SpouseName,
			FatherName: $scope.MyProfile.FatherName,
			MotherName: $scope.MyProfile.MotherName,
			GrandFather: $scope.MyProfile.GrandFather
		};

		if ($scope.MyProfile.DOB_ADDet) {
			beData.DOB_AD = $filter('date')(new Date($scope.MyProfile.DOB_ADDet.dateAD), 'yyyy-MM-dd');
		}

		if ($scope.MyProfile.AnniversaryDateDet) {
			beData.AnniversaryDate = $filter('date')(new Date($scope.MyProfile.AnniversaryDateDet.dateAD), 'yyyy-MM-dd');
		}


		$http({
			method: 'POST',
			url: base_url + "MyProfile/Creation/UpdatePersonalInfo",
			dataType: "json",
			data:JSON.stringify(beData)
		}).then(function (res)
		{
			if (res.data.IsSuccess && res.data)
			{				
				$scope.CanEdit.PerfornalInfo = false;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	
	$scope.EditAcademicQualification = function () {
		$scope.CanEdit.AcademicQualification = true;
	};
	$scope.UpdateAcademicQualification = function () {
		$scope.CanEdit.AcademicQualification = false;
	}

	//Permanent Address
	$scope.EditPAddress = function () {
		$scope.CanEdit.PAddress = true;
	};
	$scope.UpdatePAddress = function () {

		var beData = {
			PA_Country: $scope.MyProfile.PA_Country,
			PA_State: $scope.MyProfile.PA_State,
			PA_Zone: $scope.MyProfile.PA_Zone,
			PA_District: $scope.MyProfile.PA_District,
			PA_City: $scope.MyProfile.PA_City,
			PA_Municipality: $scope.MyProfile.PA_Municipality,
			PA_Ward: $scope.MyProfile.PA_Ward,
			PA_Street: $scope.MyProfile.PA_Street,
			PA_HouseNo: $scope.MyProfile.PA_HouseNo,
			PA_FullAddress: $scope.MyProfile.PA_FullAddress
		};


		$http({
			method: 'POST',
			url: base_url + "MyProfile/Creation/UpdatePermanentAddress",
			dataType: "json",
			data: JSON.stringify(beData)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data) {
				$scope.CanEdit.PAddress = false;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	//Temporary Address
	$scope.EditTAddress = function () {
		$scope.CanEdit.TAddress = true;
	};

	$scope.UpdateTAddress = function () {

		var beData = {
			TA_Country: $scope.MyProfile.TA_Country,
			TA_State: $scope.MyProfile.TA_State,
			TA_Zone: $scope.MyProfile.TA_Zone,
			TA_District: $scope.MyProfile.TA_District,
			TA_City: $scope.MyProfile.TA_City,
			TA_Municipality: $scope.MyProfile.TA_Municipality,
			TA_Ward: $scope.MyProfile.TA_Ward,
			TA_Street: $scope.MyProfile.TA_Street,
			TA_HouseNo: $scope.MyProfile.TA_HouseNo,
			TA_FullAddress: $scope.MyProfile.TA_FullAddress
		};

		$http({
			method: 'POST',
			url: base_url + "MyProfile/Creation/UpdateTemporaryAddress",
			dataType: "json",
			data: JSON.stringify(beData)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data) {
				$scope.CanEdit.TAddress = false;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//Citizenship
	$scope.EditCitizenship = function () {
		$scope.CanEdit.Citizenship = true;
	};
	$scope.UpdateCitizenship = function () {

		var beData = {
			PanId: $scope.MyProfile.PanId,
			CitizenshipNo: $scope.MyProfile.CitizenshipNo,
			CitizenIssueDate: null,
			CitizenShipIssuePlace: $scope.MyProfile.CitizenShipIssuePlace			
		};

		if ($scope.MyProfile.DOB_ADDet) {
			beData.CitizenIssueDate = $filter('date')(new Date($scope.MyProfile.CitizenshipIssueDateDet.dateAD), 'yyyy-MM-dd');
		}

		$http({
			method: 'POST',
			url: base_url + "MyProfile/Creation/UpdateCitizenship",
			dataType: "json",
			data: JSON.stringify(beData)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data) {
				$scope.CanEdit.Citizenship = false;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		
	}

	//License
	$scope.EditLicense = function () {
		$scope.CanEdit.License = true;
	};
	$scope.UpdateLicense = function () {
		$scope.CanEdit.License = false;
	}

	//Passport
	$scope.EditPassport = function () {
		$scope.CanEdit.Passport = true;
	};
	$scope.UpdatePassport = function () {
		$scope.CanEdit.Passport = false;
	}

	//Life Insurance
	$scope.EditLifeInsurance = function () {
		$scope.CanEdit.LifeInsurance = true;
	};
	$scope.UpdateLifeInsurance = function () {
		$scope.CanEdit.LifeInsurance = false;
	}

	//Health Insurance
	$scope.EditHealthInsurance = function () {
		$scope.CanEdit.HealthInsurance = true;
	};
	$scope.UpdateHealthInsurance = function () {
		$scope.CanEdit.HealthInsurance = false;
	}

	//CIT
	$scope.EditCIT = function () {
		$scope.CanEdit.CIT = true;
	};
	$scope.UpdateCIT = function () {
		

		var beData = {
			SSFNo: $scope.MyProfile.SSFNo,
			CITCode: $scope.MyProfile.CITCode,
			CITAcNo: $scope.MyProfile.CITAcNo,
			CIT_Amount: $scope.MyProfile.CIT_Amount,
			CIT_Nominee: $scope.MyProfile.CIT_Nominee,
			CIT_RelationShip: $scope.MyProfile.CIT_RelationShip,
			CIT_IDType: $scope.MyProfile.CIT_IDType,
			CIT_IDNo: $scope.MyProfile.CIT_IDNo,
			CIT_EntryDate: null
		};

		if ($scope.MyProfile.CIT_EntryDateDet) {
			beData.CIT_EntryDate = $filter('date')(new Date($scope.MyProfile.CIT_EntryDateDet.dateAD), 'yyyy-MM-dd');
		}

		$http({
			method: 'POST',
			url: base_url + "MyProfile/Creation/UpdateCITDetails",
			dataType: "json",
			data: JSON.stringify(beData)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data) {
				$scope.CanEdit.CIT = false;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	}


	//Account Detail
	$scope.EditAccountDetail = function () {
		$scope.CanEdit.AccountDetail = true;
	};
	$scope.UpdateAccountDetail = function () {
		$scope.CanEdit.AccountDetail = false;
	}

	//Contact
	$scope.EditContact = function () {
		$scope.CanEdit.Contact = true;
	};
	$scope.UpdateContact = function () {
		$scope.CanEdit.Contact = false;
	}

	//Emergency Contact
	$scope.EditEContact = function () {
		$scope.CanEdit.EContact = true;
	};
	$scope.UpdateEContact = function () {
		$scope.CanEdit.EContact = false;
	}

	//Official Detail
	$scope.EditOfficialDet = function () {
		$scope.CanEdit.OfficialDet = true;
	};
	$scope.UpdateOfficialDet = function () {
		$scope.CanEdit.OfficialDet = false;
	}

	//Other Detail
	$scope.EditOtherDet = function () {
		$scope.CanEdit.OtherDet = true;
	};
	$scope.UpdateOtherDet = function () {
		$scope.CanEdit.OtherDet = false;
	}


	//Supervisor
	$scope.EditSupervisor = function () {
		$scope.CanEdit.Supervisor = true;
	};
	$scope.UpdateSupervisor = function () {
		$scope.CanEdit.Supervisor = false;
	}

	//Contact Information
	$scope.EditContactInfo = function () {
		$scope.CanEdit.ContactInfo = true;
	};
	$scope.UpdateContactInfo = function () {
		$scope.CanEdit.ContactInfo = false;
	}

	$scope.ChangeCasteName = function () {
		if ($scope.MyProfile.CasteId && $scope.MyProfile.CasteId > 0) {
			var findCaste = mx($scope.CasteList).firstOfDefault(p1 => p1.id == $scope.MyProfile.CasteId);
			if (findCaste)
				$scope.MyProfile.CasteName = findCaste.text;
		}
    }

	$scope.GetAllMyProfile = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		//	$scope.MyProfile = [];

		$http({
			method: 'POST',
			url: base_url + "MyProfile/Creation/GetAllProfile",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data) {
				$scope.MyProfile = res.data;

				if ($scope.MyProfile.PhotoPath) {
					$scope.MyProfile.PhotoPath = WEBURL + $scope.MyProfile.PhotoPath
				}

				if (!$scope.MyProfile.PhotoPath || $scope.MyProfile.PhotoPath.length == 0)
					$scope.MyProfile.PhotoPath = '/teacherloginFiles/dynamic/images/Teacher_Profile/placeholder.png';

				if ($scope.MyProfile.DOB_AD) {
					$scope.MyProfile.DOB_AD_TMP = new Date($scope.MyProfile.DOB_AD);
				}

				if ($scope.MyProfile.AnniversaryDateAD)
					$scope.MyProfile.AnniversaryDateAD_TMP = new Date($scope.MyProfile.AnniversaryDateAD);

				if ($scope.MyProfile.CitizenIssueDate)
					$scope.MyProfile.CitizenshipIssueDate_TMP = new Date($scope.MyProfile.CitizenIssueDate);

				if ($scope.MyProfile.CIT_EntryDate)
					$scope.MyProfile.CIT_EntryDate_TMP = new Date($scope.MyProfile.CIT_EntryDate);

				
				if ($scope.MyProfile.CasteId && $scope.MyProfile.CasteId > 0) {
					var findCaste = mx($scope.CasteList).firstOrDefault(p1 => p1.id == $scope.MyProfile.CasteId);
					if (findCaste)
						$scope.MyProfile.CasteName = findCaste.text;
                }

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}





});