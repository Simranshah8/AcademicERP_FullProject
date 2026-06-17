
app.controller('MyProfileController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'MyProfile';
	var gSrv = GlobalServices;
	$scope.LoadData = function() {

		$scope.GenderColl = gSrv.getGenderList();
		$scope.BloodGroupList = gSrv.getBloodGroupList();
		$scope.ReligionList = gSrv.getReligionList();
		$scope.CountryList = gSrv.getCountryList();
		$scope.DisablityList = gSrv.getDisablityList();

		$scope.CanEdit = {
			PerfornalInfo: false,
			AcademicDetails: false,
			ParentsDetail: false,
			Guardian: false,
			PAddress: false,
			CAddress: false,
			Contact: false
		};

		$scope.CasteList = [];
		$http({
			method: 'POST',
			url: base_url + "Student/Creation/GetAllCaste",
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


		
	};

	$scope.AfterChoosePhoto = function () {

		$timeout(function () {
			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Student/Creation/UpdatePhoto",
				headers: { 'Content-Type': undefined },
				transformRequest: function (data) {
					var formData = new FormData();
					if (data.photo.length > 0)
						formData.append("photo", data.photo[0]);

					return formData;
				},
				data: { photo: $scope.Photo_TMP }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				hidePleaseWait();

				Swal.fire(res.data.ResponseMSG);

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		});
		

	}

	$scope.AfterChooseSignature = function () {

		$timeout(function () {
			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Student/Creation/UpdateSignature",
				headers: { 'Content-Type': undefined },
				transformRequest: function (data) {
					var formData = new FormData();
					if (data.photo.length > 0)
						formData.append("photo", data.photo[0]);

					return formData;
				},
				data: { photo: $scope.Signature_TMP }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				hidePleaseWait();

				Swal.fire(res.data.ResponseMSG);

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		});


	}

	$scope.ChangeCasteName = function () {
		if ($scope.MyProfile.CasteId && $scope.MyProfile.CasteId > 0) {
			var findCaste = mx($scope.CasteList).firstOfDefault(p1 => p1.id == $scope.MyProfile.CasteId);
			if (findCaste)
				$scope.MyProfile.CasteName = findCaste.text;
		}
	}
	//Personal Information
	$scope.EditProfile = function () {
		$scope.CanEdit.PerfornalInfo = true;
	};
	$scope.UpdateProfile = function () {

		var beData = {
			Gender: 1,
				DOB_AD: null,
				Religion: $scope.MyProfile.Religion,
				CasteId: $scope.MyProfile.CasteId,
				Nationality: $scope.MyProfile.Nationality,
				BloodGroup: $scope.MyProfile.BloodGroup,
				MotherTongue: $scope.MyProfile.MotherTongue,
				Height: $scope.MyProfile.Height,
				Weigth: $scope.MyProfile.Weigth,
				Aim: $scope.MyProfile.Aim,
				BirthCertificateNo: $scope.MyProfile.BirthCertificateNo,
				CitizenshipNo: $scope.MyProfile.CitizenshipNo,
			Remarks: $scope.MyProfile.Remarks,
			BoardRegNo: $scope.MyProfile.BoardRegNo,
			IsPhysicalDisability: $scope.MyProfile.IsPhysicalDisability
		};


		if ($scope.MyProfile.Gender == "Male")
			beData.Gender = 1;
		else if ($scope.MyProfile.Gender == "Female")
			beData.Gender = 2;
		else
			beData.Gender = 3;

		if ($scope.MyProfile.DOB_ADDet) {
			beData.DOB_AD = $filter('date')(new Date($scope.MyProfile.DOB_ADDet.dateAD), 'yyyy-MM-dd');
		}

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/UpdatePersonalInfo",
			dataType: "json",
			data: JSON.stringify(beData)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data) {
				$scope.CanEdit.PerfornalInfo = false;
				$("#toggle_tst").toggle();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		
	}
	//Academic Details
	$scope.EditAcademicDetails = function () {
		$scope.CanEdit.AcademicDetails = true;
	};
	$scope.UpdateAcademicDetails = function () {
		

		var beData = $scope.MyProfile.AcademicDetailsColl;

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/UpdatePreviousSchool",
			dataType: "json",
			data: JSON.stringify(beData)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data) {
				$scope.CanEdit.AcademicDetails = false;
				$("#toggle_academicDetails2").toggle();

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	}

	//Parents Detail
	$scope.EditParentsDetail = function () {
		$scope.CanEdit.ParentsDetail = true;
	};
	$scope.UpdateParentsDetail = function () {

		var beData = {
			FatherName: $scope.MyProfile.FatherName,
				F_Profession: $scope.MyProfile.F_Profession,
				F_ContactNo: $scope.MyProfile.F_ContactNo,
				F_Email: $scope.MyProfile.F_Email,
				MotherName: $scope.MyProfile.MotherName,
				M_Profession: $scope.MyProfile.M_Profession,
				M_ContactNo: $scope.MyProfile.M_ContactNo,
				M_Email: $scope.MyProfile.M_Email
		};

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/UpdateParentDetails",
			dataType: "json",
			data: JSON.stringify(beData)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data) {
				$scope.CanEdit.ParentsDetail = false;
				$("#toggle_parents").toggle();

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		
	}

	//Guardian
	$scope.EditGuardian = function () {
		$scope.CanEdit.Guardian = true;
	};
	$scope.UpdateGuardian = function () {
		
		var beData = {
			GuardianName: $scope.MyProfile.GuardianName,
			Relation: $scope.MyProfile.Relation,
			G_ContactNo: $scope.MyProfile.G_ContactNo,
			G_Address: $scope.MyProfile.G_Address,
			G_Profesion: $scope.MyProfile.G_Profesion,
			G_Email: $scope.MyProfile.G_Email
		};

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/UpdateGuardianDetails",
			dataType: "json",
			data: JSON.stringify(beData)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data) {
				$scope.CanEdit.Guardian = false;
				$("#toggle_guardian").toggle();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//Permanent Address
	$scope.EditPAddress = function () {
		$scope.CanEdit.PAddress = true;
	};
	$scope.UpdatePAddress = function () {
	
		var beData = {
			PA_FullAddress: $scope.MyProfile.PA_FullAddress,
			PA_Province: $scope.MyProfile.PA_Province,
			PA_District: $scope.MyProfile.PA_District,
			PA_LocalLevel: $scope.MyProfile.PA_LocalLevel,
			PA_WardNo: $scope.MyProfile.PA_WardNo,
			PA_Village: $scope.MyProfile.PA_Village,
			PA_LAT: $scope.MyProfile.PA_LAT,
			PA_LAN: $scope.MyProfile.PA_LAN
		};

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/UpdatePermanentAddress",
			dataType: "json",
			data: JSON.stringify(beData)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data) {
				$scope.CanEdit.PAddress = false;
				$("#toggle_paddress").toggle();

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//Current Address
	$scope.EditCAddress = function () {
		$scope.CanEdit.CAddress = true;
	};
	$scope.UpdateCAddress = function () {

		var beData = {
			CA_FullAddress: $scope.MyProfile.CA_FullAddress,
			CA_Province: $scope.MyProfile.CA_Province,
			CA_District: $scope.MyProfile.CA_District,
			CA_LocalLevel: $scope.MyProfile.CA_LocalLevel,
			CA_WardNo: $scope.MyProfile.CA_WardNo,
			CA_Village: $scope.MyProfile.CA_Village,
			CA_LAT: $scope.MyProfile.CA_LAT,
			CA_LAN: $scope.MyProfile.CA_LAN
		};

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/UpdateTemporaryAddress",
			dataType: "json",
			data: JSON.stringify(beData)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data) {
				$scope.CanEdit.CAddress = false;
				$("#toggle_caddress").toggle();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		
	}

	//Contact Information
	$scope.EditContact = function () {
		$scope.CanEdit.Contact = true;
	};
	$scope.UpdateContact = function () {
		

		var beData = {			
			ContactNo: $scope.MyProfile.ContactNo,
			Email: $scope.MyProfile.Email,			
		};

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/UpdateContactInfo",
			dataType: "json",
			data: JSON.stringify(beData)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data) {
				$scope.CanEdit.Contact = false;
				$("#toggle_contact").toggle();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}



	$scope.GetAllMyProfile = function () {

		$scope.ProfileIcon = '/StudentLoginFiles/dynamic/images/StudentIcon.png'

		$scope.loadingstatus = "running";
		showPleaseWait();
		//	$scope.MyProfile = [];

		$http({
			method: 'POST',
			url: base_url + "Student/Creation/GetAllProfile",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data) {
				$scope.MyProfile = res.data;

				if ($scope.MyProfile.PhotoPath && $scope.MyProfile.PhotoPath.length > 0)
					$scope.MyProfile.PhotoPath = WEBURL + $scope.MyProfile.PhotoPath;
				else
					$scope.MyProfile.PhotoPath = '/StudentLoginFiles/dynamic/images/stu_prof/stu_profile.svg';

				if ($scope.MyProfile.CasteId && $scope.MyProfile.CasteId > 0) {
					var findCaste = mx($scope.CasteList).firstOrDefault(p1 => p1.id == $scope.MyProfile.CasteId);
					if (findCaste)
						$scope.MyProfile.CasteName = findCaste.text;
				}

				if ($scope.MyProfile.DOB_AD)
					$scope.MyProfile.DOB_TMP = new Date($scope.MyProfile.DOB_AD);

				if (!$scope.MyProfile.AcademicDetailsColl || $scope.MyProfile.AcademicDetailsColl.length == 0) {
					$scope.MyProfile.AcademicDetailsColl = [];
					$scope.MyProfile.AcademicDetailsColl.push({
						ClassName :'',
						Exam: '',
						PassoutYear: '',
						SymbolNo: '',
						ObtainMarks: 0,
						ObtainPer: 0,
						Division: '',
						GPA: 0,
						SchoolColledge: '',
						BoardName:'',
					});
                }
				//$scope.v=res.data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}



});