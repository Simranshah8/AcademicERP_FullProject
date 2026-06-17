app.controller('EmployeeController', function ($scope, $http, $timeout, $rootScope, $filter, $translate, GlobalServices) {
	$scope.Title = 'Employee';
	$rootScope.ChangeLanguage();
	//OnClickDefault();

	//function OnClickDefault() {
	//	document.getElementById('left-employee-form').style.display = "none";

	//	// sections display and hide
	//	document.getElementById('add-employee-left').onclick = function () {
	//		document.getElementById('employee-left-section').style.display = "none";
	//		document.getElementById('left-employee-form').style.display = "block";
	//		$scope.ClearLeftEmployee();
	//	}
	//	document.getElementById('employee-left-back-btn').onclick = function () {
	//		document.getElementById('left-employee-form').style.display = "none";
	//		document.getElementById('employee-left-section').style.display = "block";
	//		$scope.ClearLeftEmployee();
	//	}
	//}

	//Added By Suresh on 12 Mangsir for webcam
	let stream = null;
	let video = document.querySelector("#video");
	let canvas = document.querySelector("#canvas");
	$scope.webCam = {
		Start: false
	};
	$scope.newEmployee = $scope.newEmployee || {};
	$scope.takePhotoFromCamera = async function () {
		if ($scope.webCam.Start === true) {
			canvas.width = video.videoWidth || 320;
			canvas.height = video.videoHeight || 240;
			var context = canvas.getContext("2d");
			context.drawImage(video, 0, 0, canvas.width, canvas.height);
			var base64Image = canvas.toDataURL("image/jpeg", 0.9);
			var byteString = atob(base64Image.split(',')[1]);
			var mimeString = base64Image.split(',')[0].split(':')[1].split(';')[0];
			var ab = new ArrayBuffer(byteString.length);
			var ia = new Uint8Array(ab);
			for (var i = 0; i < byteString.length; i++) {
				ia[i] = byteString.charCodeAt(i);
			}
			var blob = new Blob([ab], { type: mimeString });
			// Create File object
			var file = new File([blob], "CameraPhoto.jpg", {
				type: "image/jpeg"
			});
			$scope.$applyAsync(function () {
				$scope.newEmployee.PhotoData = base64Image;
				$scope.newEmployee.Photo_TMP = [file];
				$scope.webCam.Start = false;
			});
		}
		else {
			try {
				stream = await navigator.mediaDevices.getUserMedia({
					video: {
						width: 640,
						height: 480
					},
					audio: false
				});
				video.srcObject = stream;
				$scope.$applyAsync(function () {
					$scope.webCam.Start = true;
				});
			}
			catch (error) {
				Swal.fire("Camera Error: " + error.message);
			}
		}
	};
	//Ends


	$scope.LoadData = function () {

		//GlobalServices.ChangeLanguage();

		$scope.TaxRuleAsList = [{ id: 1, text: 'NORMAL' }, { id: 2, text: 'SSF' }];
		var gSrv = GlobalServices;
		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();
		$scope.GenderColl = gSrv.getGenderList();
		$scope.RemarksForList = [{ id: 1, text: 'MERITS' }, { id: 2, text: 'DEMERITS' }, { id: 3, text: 'OTHERS' },]
		$scope.DistrictList = GetDistrictList();
		$scope.MonthList = gSrv.getMonthList();

		$scope.currentPages = {
			EmpList: 1,
			LeftEmployee: 1,
			RemarkList: 1,
		};

		$scope.searchData = {
			EmpList: '',
			LeftEmployee: '',
			RemarkList: ''
		};

		$scope.perPage = {
			EmpList: gSrv.getPerPageRow(),
			LeftEmployee: gSrv.getPerPageRow(),
			RemarkList: gSrv.getPerPageRow(),
		};

		$scope.AcademicYearColl = [];
		$scope.AcademicYearColl = gSrv.getYearList();

		$scope.CasteList = [];
		gSrv.getCasteList().then(function (res) {
			$scope.CasteList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		//Added By Suresh for Webcam
		$scope.webCam = {
			Start: false
		};
		//Ends
		$('.select2').select2();
		$scope.BloodGroupList = gSrv.getBloodGroupList();
		$scope.ReligionList = gSrv.getReligionList();
		$scope.CountryList = gSrv.getCountryList();
		$scope.NationalityList = GlobalServices.getNationalityList();
		$scope.ProvinceList = GetStateList();
		$scope.DistrictList = GetDistrictList();
		$scope.ZoneList = GetZoneList();
		$scope.DisablityList = gSrv.getDisablityList();
		$scope.MaritalStatusList = gSrv.getMaritaStatusList();

		$scope.VDCColl = GetVDCList();

		$scope.MotherTonqueList = ["Bhojpuri", "English", "Gurung", "Maithali", "Nepali", "Newari", "Sanskrit", "Sherpa", "Tamang",
			"Rai", "Awadhi", "Hindi", "Kirati", "Tharu", "Doteli", "Magar", "Urdu", "Bajjika", "Limbu", "Rajbansi",
			"Majhi", "Thami", "Dhimal", "Others", "Baitadeli", "Achhami", "Bajhangi", "Sunuwar", "Kham", "Tajpuriya", "Angika", "Kumal",
			"Darai", "Bajureli", "Bote", "Ghale", "Tibetian", "Raji", "Pahari", "Dailekhi", "Dadeldhuri", "Surel", "Baram", "Bankariya", "Kusunda", "Puma",
			"Darchuleli", "Athpariya", "Rana Tharu", "Lapcha", "Yakkha", "Bhujel", "Kulung", "Bantawa"];

		$scope.TeacherLevelList = ["ECD", "Primary", "Lower", "Secondary", "Secondary", "Higher", "Secondary"];
		$scope.TeacherRankList = ["1st", "2nd", "3rd"];

		$scope.TeacherPositionList = ["ECD Facilitator", "Permanent", "Temporary", "Rahat", "PCF", "Private Sources", "Permanent Leon", "Temporary Leon"];
		$scope.TeacherTypeList = ["Teacher", "Head Teacher"];
		$scope.TeachingLanguageList = ["Bhojpuri", "English", "Gurung", "Maithali", "Nepali", "Newari", "Sanskrit", "Sherpa", "Tamang", "Tharu", "Awadhi",
			"Doteli", "Hindi", "Magar", "Urdu", "Bajjika", "Limbu", "Rajbansi", "Majhi", "Thami", "Dhimal"];


		$scope.DocumentTypeList = [];
		gSrv.getDocumentTypeList().then(function (res) {
			$scope.DocumentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

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



		$scope.BankList = [];
		$http({
			method: 'GET',
			url: base_url + "Account/Creation/GetAllBank",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BankList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.BankList_Qry = mx($scope.BankList);

		$scope.newEmployee = {
			EmployeeId: null,
			EmployeeCode: '',
			EnrollNumber: 0,
			FirstName: '',
			MiddleName: '',
			LastName: '',
			Gender: 1,
			DOB_AD: null,

			PhoneNo: '',
			OfficePhone: '',
			EmailId: '',
			MaritalStatus: '',
			SpouseName: '',
			AnniversaryDate: null,
			FatherName: '',
			MotherName: '',
			GrandFatherName: '',
			isEDJ: false,

			IsPhysicalDisability: false,
			Photo: null,
			PhotoPath: null,
			Signature: null,
			SignaturePath: null,

			PanId: '',
			CitizenshipNo: '',
			CitizenshipIssueDate: null,
			CitizenShipIssuePlace: '',
			DrivindLicenceNo: '',
			LicenceIssueDate: null,
			LicenceExpiryDate: null,
			LicenceIssuePlace: '',
			PasswordNo: '',
			PasswordIssueDate: null,
			PasswordExpiryDate: null,
			PasswordIssuePlace: '',
			CardNo: 0,
			Country: '',
			State: '',
			Zone: '',
			District: '',
			City: '',
			MunicipalityVDC: '',
			Ward: 0,
			Street: '',
			HouseNo: '',
			FullAddress: '',

			IsSameAsPermanentAddress: false,
			PersonName: '',
			Relationship: '',
			Address: '',
			Phone: '',
			Mobile: '',
			Department: '',
			Designation: '',
			Category: '',
			Level: '',
			IsTeaching: true,
			SubjectTeacher: '',
			ServiceType: '',
			DateOfJoining: null,
			DateOfConfirmation: null,
			DateOfRetirement: null,
			RemoteArea: '',
			AccessionNo: 0,
			SSFNo: '',
			CITCode: '',
			CITACNo: '',
			Amount: '',
			Nominee: '',
			Relationship: '',
			IdType: '',
			IdNo: '',
			EntryDate: new Date(),
			BankList: [],
			InsuranceCompany: '',
			PolicyName: '',
			PolicyNo: '',
			PolicyAmount: '',
			PolicyStartDate: null,
			PolicyLastDate: null,
			LI_PaymentType: '',
			StartMonth: '',
			IsDeduct: true,
			Remarks: '',
			InsuranceType: '',
			H_IsDeduct: false,
			AcademicQualificationColl: [],
			WorkExperienceColl: [],
			EmployeeResearchPublicationColl: [],
			AttachmentColl: [],

			S_FirstLevelId: null,
			S_SecondLevelId: null,
			S_ThirdLevel: null,
			Mode: 'Save'
		};
		$scope.newEmployee.BankList.push({ ForPayRoll: false });
		$scope.newEmployee.AcademicQualificationColl.push({});
		$scope.newEmployee.WorkExperienceColl.push({});
		$scope.newEmployee.EmployeeResearchPublicationColl.push({});



		$scope.DepartmentList = [];
		gSrv.getDepartmentList().then(function (res) {
			$scope.DepartmentList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.DesignationList = [];
		gSrv.getDesignationList().then(function (res) {
			$scope.DesignationList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.LevelList = [];
		gSrv.getLevelList().then(function (res) {
			$scope.LevelList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		$scope.CategoryList = [];
		gSrv.getCategoryList().then(function (res) {
			$scope.CategoryList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		$scope.ServiceTypeList = [];
		gSrv.getServiceTypeList().then(function (res) {
			$scope.ServiceTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.ClassShiftList = [];
		//$http({
		//	method: 'POST',
		//	url: base_url + "Academic/Creation/GetAllClassShift",
		//	dataType: "json"
		//}).then(function (res) {
		//	if (res.data.IsSuccess && res.data.Data) {
		//		$scope.ClassShiftList = res.data.Data;
		//	}
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		$scope.SubjectList = [];
		gSrv.getSubjectList().then(function (res) {
			$scope.SubjectList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.InsuranceTypeList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllInsuranceType",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.InsuranceTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.GetAutoNumber();
	/*	$scope.GetAllLeftEmployeeList();*/

		//$scope.newLeftEmployee = {
		//	EmployeeSearchBy: 'E.Name',
		//	EmployeeId: null,
		//	LeftRemarks: '',
		//	LeftDate_TMP: new Date()
		//};

		//$scope.newRemarkList = {
		//	RemarkListId: null,
		//	FromDate_TMP: new Date(),
		//	ToDate_TMP: new Date(),
		//	//Mode: 'Save'
		//};

		//$scope.newEmployeeRemarks = {
		//	EmployeeSearchBy: 'E.Name',
		//	EmployeeId: null,
		//};

		//$scope.RemarksTypeList = [];
		//$scope.RemarksTypeQry = [];
		//$http({
		//	method: 'POST',
		//	url: base_url + "Academic/Creation/GetAllRemarksTypeList",
		//	dataType: "json"
		//}).then(function (res) {
		//	if (res.data.IsSuccess && res.data.Data) {
		//		$scope.RemarksTypeList = res.data.Data;
		//		$scope.RemarksTypeQry = mx(res.data.Data);
		//	}

		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		//$scope.ClearEmployeeRemarks();

		$scope.UserList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUserList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//Company Details
		$scope.CompanyConfig = {};
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetCompanyDet",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CompanyConfig = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	
		 

	}


	$scope.dobFocusOn = null;
	$scope.OnDOBFocus = function (dateStyle) {
		$scope.dobFocusOn = dateStyle;
	}

	$scope.updateDOB = function (dateStyle) {
		$timeout(function () {
			if (dateStyle == 1) {

				if ($scope.newEmployee.DOBADDet) {
					$scope.newEmployee.DOBBS_TMP = new Date($scope.newEmployee.DOBADDet.dateAD);
				}
				else {
					$scope.newEmployee.DOBBS_TMP = null;
				}
			}
			else if (dateStyle == 2) {
				if ($scope.newEmployee.DOBBSDet) {
					$scope.newEmployee.DOBAD_TMP = new Date($scope.newEmployee.DOBBSDet.dateAD);
					$scope.newEmployee.DOBAD_TMP1 = new Date($scope.newEmployee.DOBBSDet.dateAD);
				}
				else {
					$scope.newEmployee.DOBAD_TMP = null;
				}
			}
		});
 
	};
	$scope.ClearLeftEmployee = function () {
		$scope.newLeftEmployee = {
			EmployeeSearchBy: 'E.Name',
			EmployeeId: null,
			LeftRemarks: ''
		};
	};
	$scope.GetAutoNumber = function () {

		$timeout(function () {

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetEmployeeAutoNumber",
				dataType: "json",
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				var st = res.data.Data;
				if (st.IsSuccess == true) {
					$scope.newEmployee.EmployeeCode = st.ResponseId;
					$scope.newEmployee.AutoNumber = st.RId;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});


	};


	$scope.getProvinceId = function (provinceText) {
		var province = $scope.ProvinceList.find(p => p.text === provinceText);
		return province ? province.id : null;
	};
	$scope.getDistrictId = function (districtText) {
		var district = $scope.DistrictList.find(p => p.text === districtText);
		return district ? district.id : null;
	};

	$scope.GetEmpSummaryList = function () {

		if ($scope.EmpList.DepartmentId > -1) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				DepartmentIdColl: $scope.EmpList.DepartmentId
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetEmpSummary",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess) {
					$scope.EmpList.DataColl = res.data.Data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}
	$scope.SamePAddress = function () {

		if ($scope.newEmployee.IsSameAsPermanentAddress == true) {
			$scope.newEmployee.TA_Counrty = $scope.newEmployee.PA_Country;
			$scope.newEmployee.TA_State = $scope.newEmployee.PA_State;
			$scope.newEmployee.TA_Zone = $scope.newEmployee.PA_Zone;
			$scope.newEmployee.TA_District = $scope.newEmployee.PA_District;
			$scope.newEmployee.TA_City = $scope.newEmployee.PA_City;
			$scope.newEmployee.TA_Municipality = $scope.newEmployee.PA_Municipality;
			$scope.newEmployee.TA_Ward = $scope.newEmployee.PA_Ward;
			$scope.newEmployee.TA_Street = $scope.newEmployee.PA_Street;
			$scope.newEmployee.TA_HouseNo = $scope.newEmployee.PA_HouseNo;
			$scope.newEmployee.TA_FullAddress = $scope.newEmployee.PA_FullAddress;
		} else {
			$scope.newEmployee.TA_Counrty = '';
			$scope.newEmployee.TA_State = '';
			$scope.newEmployee.TA_Zone = '';
			$scope.newEmployee.TA_District = '';
			$scope.newEmployee.TA_City = '';
			$scope.newEmployee.TA_Municipality = '';
			$scope.newEmployee.TA_Ward = 0;
			$scope.newEmployee.TA_Street = '';
			$scope.newEmployee.TA_HouseNo = '';
			$scope.newEmployee.TA_FullAddress = '';
		}
	}
	//Bank Account Details Coll
	$scope.AddBankAccountDetails = function (ind) {
		if ($scope.newEmployee.BankList) {
			if ($scope.newEmployee.BankList.length > ind + 1) {
				$scope.newEmployee.BankList.splice(ind + 1, 0, {
					BankName: '',
					AccountName: '',
					AccountNo: '',
					Branch: '',
					ForPayRoll: false,

				})
			} else {
				$scope.newEmployee.BankList.push({
					BankName: '',
					AccountName: '',
					AccountNo: '',
					Branch: '',
					ForPayRoll: false,
				})
			}
		}
	};
	$scope.delBankAccountDetails = function (ind) {
		if ($scope.newEmployee.BankList) {
			if ($scope.newEmployee.BankList.length > 1) {
				$scope.newEmployee.BankList.splice(ind, 1);
			}
		}
	};

	//Academic Qualification Details Coll
	$scope.AddAcademicQualificationDetails = function (ind) {
		if ($scope.newEmployee.AcademicQualificationColl) {
			if ($scope.newEmployee.AcademicQualificationColl.length > ind + 1) {
				$scope.newEmployee.AcademicQualificationColl.splice(ind + 1, 0, {
					DegreeName: '',
					BoardUniversity: '',
					PassedYear: '',
					GradePercentage: '',


				})
			} else {
				$scope.newEmployee.AcademicQualificationColl.push({
					DegreeName: '',
					BoardUniversity: '',
					PassedYear: '',
					GradePercentage: '',
				})
			}
		}
	};
	$scope.delAcademicQualificationDetails = function (ind) {
		if ($scope.newEmployee.AcademicQualificationColl) {
			if ($scope.newEmployee.AcademicQualificationColl.length > 1) {
				$scope.newEmployee.AcademicQualificationColl.splice(ind, 1);
			}
		}
	};

	//Work Experience Details Coll
	$scope.AddWorkExperienceDetails = function (ind) {
		if ($scope.newEmployee.WorkExperienceColl) {
			if ($scope.newEmployee.WorkExperienceColl.length > ind + 1) {
				$scope.newEmployee.WorkExperienceColl.splice(ind + 1, 0, {
					DegreeName: '',
					BoardUniversity: '',
					PassedYear: '',
					GradePercentage: '',


				})
			} else {
				$scope.newEmployee.WorkExperienceColl.push({
					DegreeName: '',
					BoardUniversity: '',
					PassedYear: '',
					GradePercentage: '',
				})
			}
		}
	};
	$scope.delWorkExperienceDetails = function (ind) {
		if ($scope.newEmployee.WorkExperienceColl) {
			if ($scope.newEmployee.WorkExperienceColl.length > 1) {
				$scope.newEmployee.WorkExperienceColl.splice(ind, 1);
			}
		}
	};

	//Research & Publications Coll
	$scope.AddEmpResearchPublicationDetails = function (ind) {
		if ($scope.newEmployee.EmployeeResearchPublicationColl) {
			if ($scope.newEmployee.EmployeeResearchPublicationColl.length > ind + 1) {
				$scope.newEmployee.EmployeeResearchPublicationColl.splice(ind + 1, 0, {
					ResearchTitle: '',
					JournalName: '',
                    Coauthors: ''

				})
			} else {
				$scope.newEmployee.EmployeeResearchPublicationColl.push({
					ResearchTitle: '',
					JournalName: '',
					Coauthors: ''
				})
			}
		}
	};
	$scope.delEmpResearchPublicationDetails = function (ind) {
		if ($scope.newEmployee.EmployeeResearchPublicationColl) {
			if ($scope.newEmployee.EmployeeResearchPublicationColl.length > 1) {
				$scope.newEmployee.EmployeeResearchPublicationColl.splice(ind, 1);
			}
		}
	};

	//Attach Document
	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newEmployee.AttachmentColl) {
			if ($scope.newEmployee.AttachmentColl.length > 0) {
				$scope.newEmployee.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newEmployee.AttachmentColl.push({
						DocumentTypeId: docType.id,
						DocumentTypeName: docType.text,
						File: file,
						Name: file.name,
						Type: file.type,
						Size: file.size,
						Description: des,
						Path: null
					});
				})

				$scope.docType = null;
				$scope.attachFile = null;
				$scope.docDescription = '';
			}
		}
	};


	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newEmployee.AttachmentColl) {
			if ($scope.newEmployee.AttachmentColl.length > 0) {
				$scope.newEmployee.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newEmployee.AttachmentColl.push({
						DocumentTypeId: docType.id,
						DocumentTypeName: docType.text,
						File: file,
						Name: file.name,
						Type: file.type,
						Size: file.size,
						Description: des,
						Path: null
					});
				})

				$scope.docType = null;
				$scope.attachFile = null;
				$scope.docDescription = '';
			}
		}
	};
	$scope.ClearEmployee = function () {
		$scope.ClearEmployeePhoto();
		$scope.ClearSignaturePhoto();
		$scope.ClearCitizenFrontPhoto();
		$scope.ClearCitizenBackPhoto();
		$scope.ClearNIDPhoto();
		$timeout(function () {
			$scope.newEmployee = {
				EmployeeId: null,
				EmployeeCode: '',
				EnrollNumber: 0,
				FirstName: '',
				MiddleName: '',
				LastName: '',
				Gender: 1,
				DOB_AD: null,

				PhoneNo: '',
				OfficePhone: '',
				EmailId: '',
				MaritalStatus: '',
				SpouseName: '',
				AnniversaryDate: null,
				FatherName: '',
				MotherName: '',
				GrandFatherName: '',
				isEDJ: false,

				IsPhysicalDisability: false,
				Photo: null,
				PhotoPath: null,
				Signature: null,
				SignaturePath: null,

				PanId: '',
				CitizenshipNo: '',
				CitizenshipIssueDate: null,
				CitizenShipIssuePlace: '',
				DrivindLicenceNo: '',
				LicenceIssueDate: null,
				LicenceExpiryDate: null,
				LicenceIssuePlace: '',
				PasswordNo: '',
				PasswordIssueDate: null,
				PasswordExpiryDate: null,
				PasswordIssuePlace: '',
				CardNo: 0,
				Country: '',
				State: '',
				Zone: '',
				District: '',
				City: '',
				MunicipalityVDC: '',
				Ward: 0,
				Street: '',
				HouseNo: '',
				FullAddress: '',

				IsSameAsPermanentAddress: false,
				PersonName: '',
				Relationship: '',
				Address: '',
				Phone: '',
				Mobile: '',
				Department: '',
				Designation: '',
				Category: '',
				Level: '',
				IsTeaching: true,
				SubjectTeacher: '',
				ServiceType: '',
				DateOfJoining: null,
				DateOfConfirmation: null,
				DateOfRetirement: null,
				RemoteArea: '',
				AccessionNo: 0,
				SSFNo: '',
				CITCode: '',
				CITACNo: '',
				Amount: '',
				Nominee: '',
				Relationship: '',
				IdType: '',
				IdNo: '',
				EntryDate: new Date(),
				BankList: [],
				InsuranceCompany: '',
				PolicyName: '',
				PolicyNo: '',
				PolicyAmount: '',
				PolicyStartDate: null,
				PolicyLastDate: null,
				LI_PaymentType: '',
				StartMonth: '',
				IsDeduct: true,
				Remarks: '',
				InsuranceType: '',
				H_IsDeduct: false,
				AcademicQualificationColl: [],
				WorkExperienceColl: [],
				EmployeeResearchPublicationColl: [],
				AttachmentColl: [],

				S_FirstLevelId: null,
				S_SecondLevelId: null,
				S_ThirdLevel:null,
				Mode: 'Save'
			};
			$scope.newEmployee.BankList.push({ ForPayRoll: false });
			$scope.newEmployee.AcademicQualificationColl.push({});
			$scope.newEmployee.WorkExperienceColl.push({});
			$scope.newEmployee.EmployeeResearchPublicationColl.push({});
			$scope.GetAutoNumber();
		});

	}

	//$scope.updateDOB = function () {
	//	if ($scope.newEmployee.DOB_ADDet) {
	//		var englishDate = $filter('date')(new Date($scope.newEmployee.DOB_ADDet.dateAD), 'yyyy-MM-dd');

	//		$scope.newEmployee.DOB_AD = new Date(englishDate);

	//		if (!$scope.$$phase) {
	//			$scope.$apply();
	//		}
	//	}
	//};
	$scope.updateDOB_BS = function () {
		if ($scope.newEmployee.DOB_AD) {
			$scope.$applyAsync(function () { 
				$scope.newEmployee.DOB_AD_TMP = new Date($scope.newEmployee.DOB_AD);
			});			
		}
	};

	$scope.ClearEmployeePhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newEmployee.PhotoData = null;
				$scope.newEmployee.Photo_TMP = [];
				$scope.newEmployee.PhotoPath = '';
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};

	$scope.ClearSignaturePhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newEmployee.SignatureData = null;
				$scope.newEmployee.Signature_TMP = [];
			});

		});

		$('#imgSignature').attr('src', '');
		$('#imgSignature1').attr('src', '');
	};



	$scope.ClearCitizenFrontPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newEmployee.CitizenFrontPhotoData = null;
				$scope.newEmployee.CitizenFrontPhoto_TMP = [];
			});

		});

		$('#imgCitizenFront').attr('src', '');
		$('#imgCitizenFront').attr('src', '');
	};


	$scope.ClearCitizenBackPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newEmployee.CitizenBackPhotoData = null;
				$scope.newEmployee.CitizenBackPhoto_TMP = [];
			});

		});

		$('#imgCitizenBack').attr('src', '');
		$('#imgCitizenBack').attr('src', '');
	};

	$scope.ClearNIDPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newEmployee.NIDPhotoData = null;
				$scope.newEmployee.NIDPhoto_TMP = [];
			});

		});

		$('#imgNID').attr('src', '');
		$('#imgNID').attr('src', '');
	};

	$scope.IsValidEmployee = function () {

		if ($scope.newEmployee.EmployeeCode.isEmpty()) {
			Swal.fire('Please ! Enter Employee Code');
			return false;
		}

		if ($scope.newEmployee.FirstName.isEmpty()) {
			Swal.fire('Please ! Enter Employee First Name');
			return false;
		}
		
		if ($scope.CompanyConfig.Country == 'Nepal') {
			if (!$scope.newEmployee.LastName || $scope.newEmployee.LastName.trim() === '') {
				Swal.fire('Please ! Enter Last Name');
				return false;
			}
		}

		//if ($scope.CompanyConfig.Country === 'Nepal') {
		//	// Either BS or AD is required
		//	if (
		//		!$scope.newEmployee.DOBBS_TMP &&
		//		!$scope.newEmployee.DOBAD_TMP
		//	) {
		//		Swal.fire('Please ! Enter Date of Birth (BS or AD)');
		//		return false;
		//	}			

		//} else {
		//	// Non-Nepal → AD mandatory
		//	if (!$scope.newEmployee.DOBAD_TMP) {
		//		Swal.fire('Please ! Enter Date of Birth');
		//		return false;
		//	}
		//}


		return true;
	}

	$scope.SaveUpdateEmployee = function () {
		if ($scope.IsValidEmployee() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newEmployee.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateEmployee();
					}
				});
			} else
				$scope.CallSaveUpdateEmployee();

		}
	};

	$scope.CallSaveUpdateEmployee = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newEmployee.AttachmentColl;

		var photo = $scope.newEmployee.Photo_TMP;
		var signature = $scope.newEmployee.Signature_TMP;

		//Add js to save photo by Prashant
		var citizenshipFrontphoto = $scope.newEmployee.CitizenFrontPhoto_TMP;
		var citizenshipBackphoto = $scope.newEmployee.CitizenBackPhoto_TMP;
		var nationalIdPhoto = $scope.newEmployee.NIDPhoto_TMP;

		if ($scope.newEmployee.DOBADDet1 && $scope.newEmployee.DOBADDet1.dateAD) {
			$scope.newEmployee.DOB_AD = $filter('date')(new Date($scope.newEmployee.DOBADDet1.dateAD), 'yyyy-MM-dd');
		}

		if ($scope.newEmployee.DOBADDet && $scope.newEmployee.DOBADDet.dateAD) {
			$scope.newEmployee.DOB_AD = $filter('date')(new Date($scope.newEmployee.DOBADDet.dateAD), 'yyyy-MM-dd');
		}
		 

		if ($scope.newEmployee.AnniversaryDateDet) {
			$scope.newEmployee.AnniversaryDate = $filter('date')(new Date($scope.newEmployee.AnniversaryDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.AnniversaryDate = null;

		if ($scope.newEmployee.CitizenshipIssueDateDet) {
			$scope.newEmployee.CitizenIssueDate = $filter('date')(new Date($scope.newEmployee.CitizenshipIssueDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.CitizenIssueDate = null;

		if ($scope.newEmployee.LicenceIssueDateDet) {
			$scope.newEmployee.LicenceIssueDate = $filter('date')(new Date($scope.newEmployee.LicenceIssueDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.LicenceIssueDate = null;

		if ($scope.newEmployee.LicenceExpiryDateDet) {
			$scope.newEmployee.LicenceExpiryDate = $filter('date')(new Date($scope.newEmployee.LicenceExpiryDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.LicenceExpiryDate = null;

		if ($scope.newEmployee.PasswordIssueDateDet) {
			$scope.newEmployee.PasswordIssueDate = $filter('date')(new Date($scope.newEmployee.PasswordIssueDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.PasswordIssueDate = null;

		if ($scope.newEmployee.PasswordExpiryDateDet) {
			$scope.newEmployee.PasswordExpiryDate = $filter('date')(new Date($scope.newEmployee.PasswordExpiryDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.PasswordExpiryDate = null;

		if ($scope.newEmployee.DateOfJoiningDet) {
			$scope.newEmployee.DateOfJoining = $filter('date')(new Date($scope.newEmployee.DateOfJoiningDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.DateOfJoining = null;

		if ($scope.newEmployee.DateOfConfirmationDet) {
			$scope.newEmployee.DateOfConfirmation = $filter('date')(new Date($scope.newEmployee.DateOfConfirmationDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.DateOfConfirmation = null;

		if ($scope.newEmployee.DateOfRetirementDet) {
			$scope.newEmployee.DateOfRetirement = $filter('date')(new Date($scope.newEmployee.DateOfRetirementDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.DateOfRetirement = null;

		//LI_PolicyStartDate name assignment updated by suresh on 11 Mangsir
		if ($scope.newEmployee.PolicyStartDateDet) {
			$scope.newEmployee.LI_PolicyStartDate = $filter('date')(new Date($scope.newEmployee.PolicyStartDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.LI_PolicyStartDate = null;

		if ($scope.newEmployee.PolicyLastDateDet) {
			$scope.newEmployee.LI_PolicyLastDate = $filter('date')(new Date($scope.newEmployee.PolicyLastDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.LI_PolicyLastDate = null;
		//End

		if ($scope.newEmployee.HI_PolicyStartDateDet) {
			$scope.newEmployee.HI_PolicyStartDate = $filter('date')(new Date($scope.newEmployee.HI_PolicyStartDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.HI_PolicyStartDate = null;

		if ($scope.newEmployee.HI_PolicyLastDateDet) {
			$scope.newEmployee.HI_PolicyLastDate = $filter('date')(new Date($scope.newEmployee.HI_PolicyLastDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.HI_PolicyLastDate = null;

		if ($scope.newEmployee.CIT_EntryDateDet) {
			$scope.newEmployee.CIT_EntryDate = $filter('date')(new Date($scope.newEmployee.CIT_EntryDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEmployee.CIT_EntryDate = null;
		//New Added By Suresh

		//if ($scope.newEmployee.CIT_EntryDateDet) {
		//	$scope.newEmployee.CIT_EntryDate = $filter('date')(new Date($scope.newEmployee.CIT_EntryDateDet.dateAD), 'yyyy-MM-dd');
		//} else
		//	$scope.newEmployee.CIT_EntryDate = null;


		if ($scope.newEmployee.WorkExperienceColl) {
			$scope.newEmployee.WorkExperienceColl.forEach((S) => {
				if (S.StartDateDet)
					S.StartDate = $filter('date')(new Date(S.StartDateDet.dateAD), 'yyyy-MM-dd');
				if (S.EndDateDet)
					S.EndDate = $filter('date')(new Date(S.EndDateDet.dateAD), 'yyyy-MM-dd');
			});
		}
		//Ends

		//new Add by Prashant
		//if ($scope.newEmployee.EmployeeResearchPublicationColl) {
		//	$scope.newEmployee.EmployeeResearchPublicationColl.forEach((S) => {
		//		if (S.PublicationDateDet)
		//			S.PublicationDate = $filter('date')(new Date(S.PublicationDateDet.dateAD), 'yyyy-MM-dd');
		//	});
		//}

		if ($scope.newEmployee.SystemUserId == 0)
			$scope.newEmployee.SystemUserId = null;

		if (!$scope.newEmployee.CardNo)
			$scope.newEmployee.CardNo = 0;

		$scope.newEmployee.BankList.forEach(function (bankAccount) {
			var matchingBank = $scope.BankList.find(function (bank) {
				return bank.BankId === bankAccount.BankId;
			});
			if (matchingBank) {
				bankAccount.BankName = matchingBank.Name;
			}
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveEmployee",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}

				if (data.stPhoto && data.stPhoto.length > 0)
					formData.append("photo", data.stPhoto[0]);

				if (data.stSignature && data.stSignature.length > 0)
					formData.append("signature", data.stSignature[0]);

				//Add js to save photo by Prashant
				if (data.cfrontphto && data.cfrontphto.length > 0)
					formData.append("citizentfront", data.cfrontphto[0]);

				if (data.cbackphto && data.cbackphto.length > 0)
					formData.append("citizentBack", data.cbackphto[0]);

				if (data.nidphto && data.nidphto.length > 0)
					formData.append("nidphoto", data.nidphto[0]);

				return formData;
			},
			data: {
				jsonData: $scope.newEmployee, files: filesColl, stPhoto: photo, stSignature: signature,
				cfrontphto: citizenshipFrontphoto, cbackphto: citizenshipBackphoto, nidphto: nationalIdPhoto
			}
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearEmployee();
				$scope.Print(res.data.Data.RId);
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.Print = function (tranId) {
		if ((tranId || tranId > 0)) {
			var EmployeeId = tranId;

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + EntityId + "&voucherId=0&isTran=true",
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
												document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=0&EmployeeId=" + EmployeeId + "&vouchertype=0";
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
							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=0&EmployeeId=" + EmployeeId + "&vouchertype=0";
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

	$scope.GetAllEmployeeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EmployeeList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllEmployeeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmployeeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetEmployeeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			EmployeeId: refData.EmployeeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetEmployeeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEmployee = res.data.Data;

				if ($scope.newEmployee.DOB_AD) {
					$scope.newEmployee.DOBBS_TMP = new Date($scope.newEmployee.DOB_AD);
					$scope.newEmployee.DOBAD_TMP = new Date($scope.newEmployee.DOB_AD);
					$scope.newEmployee.DOBAD_TMP1 = new Date($scope.newEmployee.DOB_AD);
				}
				 
				if ($scope.newEmployee.AnniversaryDate) {
					$scope.newEmployee.AnniversaryDate_TMP = new Date($scope.newEmployee.AnniversaryDate);
				}

				if ($scope.newEmployee.CitizenIssueDate) {
					$scope.newEmployee.CitizenshipIssueDate_TMP = new Date($scope.newEmployee.CitizenIssueDate);
				}

				if ($scope.newEmployee.LicenceIssueDate) {
					$scope.newEmployee.LicenceIssueDate_TMP = new Date($scope.newEmployee.LicenceIssueDate);
				}

				if ($scope.newEmployee.LicenceExpiryDate) {
					$scope.newEmployee.LicenceExpiryDate_TMP = new Date($scope.newEmployee.LicenceExpiryDate);
				}

				if ($scope.newEmployee.PasswordIssueDate) {
					$scope.newEmployee.PasswordIssueDate_TMP = new Date($scope.newEmployee.PasswordIssueDate);
				}

				if ($scope.newEmployee.PasswordExpiryDate) {
					$scope.newEmployee.PasswordExpiryDate_TMP = new Date($scope.newEmployee.PasswordExpiryDate);
				}

				if ($scope.newEmployee.DateOfJoining) {
					$scope.newEmployee.DateOfJoining_TMP = new Date($scope.newEmployee.DateOfJoining);
				}

				if ($scope.newEmployee.DateOfConfirmation) {
					$scope.newEmployee.DateOfConfirmation_TMP = new Date($scope.newEmployee.DateOfConfirmation);
				}

				if ($scope.newEmployee.DateOfRetirement) {
					$scope.newEmployee.DateOfRetirement_TMP = new Date($scope.newEmployee.DateOfRetirement);
				}

				//Edited by Suresh. Pahela PolicyStartDate theyo. Ahela  LI_PolicyStartDate banako xu
				if ($scope.newEmployee.LI_PolicyStartDate) {
					$scope.newEmployee.PolicyStartDate_TMP = new Date($scope.newEmployee.LI_PolicyStartDate);
				}

				if ($scope.newEmployee.LI_PolicyLastDate) {
					$scope.newEmployee.PolicyLastDate_TMP = new Date($scope.newEmployee.LI_PolicyLastDate);
				}

				if ($scope.newEmployee.HI_PolicyStartDate) {
					$scope.newEmployee.HI_PolicyStartDate_TMP = new Date($scope.newEmployee.HI_PolicyStartDate);
				}

				if ($scope.newEmployee.HI_PolicyLastDate) {
					$scope.newEmployee.HI_PolicyLastDate_TMP = new Date($scope.newEmployee.HI_PolicyLastDate);
				}
				//New Code Added By Suresh on Mangsir 12
				if ($scope.newEmployee.CIT_EntryDate) {
					$scope.newEmployee.CIT_EntryDate_TMP = new Date($scope.newEmployee.CIT_EntryDate);
				}

				if ($scope.newEmployee.WorkExperienceColl) {
					$scope.newEmployee.WorkExperienceColl.forEach((S) => {
						if (S.StartDate)
							S.StartDate_TMP = new Date(S.StartDate);
						if (S.EndDate)
							S.EndDate_TMP = new Date(S.EndDate);
					});
				}

				//if ($scope.newEmployee.BankList) {
				//	$scope.newEmployee.BankList.forEach((S) => {
				//		if (S.BankName)
				//			S.Name = S.BankName;
						
				//	});
				//}
				//Ends

				if (!$scope.newEmployee.AcademicQualificationColl || $scope.newEmployee.AcademicQualificationColl.length == 0)
					$scope.AddAcademicQualificationDetails(0);

				if (!$scope.newEmployee.WorkExperienceColl || $scope.newEmployee.WorkExperienceColl.length == 0)
					$scope.AddWorkExperienceDetails(0);

				//$timeout(function () {
				//	if ($scope.newEmployee.EmployeeResearchPublicationColl.length == 0)
				//		$scope.newEmployee.EmployeeResearchPublicationColl.push({});
				//});
				if (!$scope.newEmployee.EmployeeResearchPublicationColl || $scope.newEmployee.EmployeeResearchPublicationColl.length == 0) {
					$scope.newEmployee.EmployeeResearchPublicationColl = [];
					$scope.newEmployee.EmployeeResearchPublicationColl.push({});
				}


				if (!$scope.newEmployee.BankList || $scope.newEmployee.BankList.length == 0) {
					$scope.AddBankAccountDetails(0);
					$scope.newEmployee.BankList[0].BankName = $scope.newEmployee.BankName;
					$scope.newEmployee.BankList[0].AccountNo = $scope.newEmployee.BA_AccountNo;
					$scope.newEmployee.BankList[0].Branch = $scope.newEmployee.BA_Branch;
				}



				$scope.newEmployee.Mode = 'Modify';

				$('.nav-tabs a:first').tab('show');

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};


	$scope.DelEmployeeById = function (refData) {

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
					EmployeeId: refData.EmployeeId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DelEmployee",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true)
						$scope.GetEmpSummaryList();

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});

	};

	//$scope.DelLeftEmployee = function (refData) {

	//	Swal.fire({
	//		title: 'Do you want to delete the selected data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Delete',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {
	//			$scope.loadingstatus = "running";
	//			showPleaseWait();

	//			var para = {
	//				EmployeeId: refData.EmployeeId,
	//				TranId: 0
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Academic/Transaction/DelLeftEmployee",
	//				dataType: "json",
	//				data: JSON.stringify(para)
	//			}).then(function (res) {
	//				hidePleaseWait();
	//				$scope.loadingstatus = "stop";
	//				Swal.fire(res.data.ResponseMSG);
	//				$scope.GetAllLeftEmployeeList();
	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	});

	//};


	//$scope.LeftEmpSelected = function () {

	//	//alert('ss');
	//};
	//$scope.GetAllLeftEmployeeList = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	$scope.LeftEmployeeList = [];

	//	$http({
	//		method: 'POST',
	//		url: base_url + "Academic/Transaction/GetLeftEmployeeList",
	//		dataType: "json"
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess && res.data.Data) {
	//			$scope.LeftEmployeeList = res.data.Data;

	//		} else {
	//			Swal.fire(res.data.ResponseMSG);
	//		}

	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});

	//}
	//$scope.CallSaveLeftEmployee = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	var filesColl = $scope.newLeftEmployee.AttachmentFilesColl;


	//	if ($scope.newLeftEmployee.LeftDateDet) {
	//		$scope.newLeftEmployee.LeftDate_AD = $scope.newLeftEmployee.LeftDateDet.dateAD;
	//	} else
	//		$scope.newLeftEmployee.LeftDate_AD = null;

	//	$http({
	//		method: 'POST',
	//		url: base_url + "Academic/Transaction/SaveLeftEmployee",
	//		headers: { 'Content-Type': undefined },

	//		transformRequest: function (data) {

	//			var formData = new FormData();
	//			formData.append("jsonData", angular.toJson(data.jsonData));

	//			if (data.files) {
	//				for (var i = 0; i < data.files.length; i++) {
	//					if (data.files[i].File)
	//						formData.append("file" + i, data.files[i].File);
	//					else
	//						formData.append("file" + i, data.files[i]);
	//				}
	//			}

	//			return formData;
	//		},
	//		data: { jsonData: $scope.newLeftEmployee, files: filesColl }
	//	}).then(function (res) {

	//		$scope.loadingstatus = "stop";
	//		hidePleaseWait();

	//		Swal.fire(res.data.ResponseMSG);

	//		if (res.data.IsSuccess == true) {
	//			$scope.ClearLeftEmployee();
	//		}

	//	}, function (errormessage) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";

	//	});
	//}

	//$scope.ClearEmployeeRemarks = function () {
	//	$scope.newEmployeeRemarks = {
	//		EmployeeRemarksId: null,
	//		EmployeeRemarksDetailsColl: [],
	//		EmployeeSearchBy: 'E.Name',
	//		EmployeeId: null,
	//		//Mode: 'Save'
	//	};
	//	$scope.newEmployeeRemarks.EmployeeRemarksDetailsColl.push({});
	//}
	//$scope.ClearRemarkList = function () {
	//	$scope.newRemarkList = {
	//		RemarkListId: null,
	//		FromDate_TMP: new Date(),
	//		ToDate_TMP: new Date(),
	//		//Mode: 'Save'
	//	};
	//}

	//$scope.GetEmpRemarks = function () {


	//	$scope.PEmployeeRemarksList = [];

	//	$scope.EmployeeRemarksList = [];
	//	$scope.EmployeeRemarksList.push({});

	//	if ($scope.newEmployeeRemarks.EmployeeId) {

	//		$scope.loadingstatus = "running";
	//		showPleaseWait();

	//		var para = {
	//			EmployeeId: $scope.newEmployeeRemarks.EmployeeId
	//		};
	//		$http({
	//			method: 'POST',
	//			url: base_url + "Academic/Transaction/GetEmpRemarks",
	//			dataType: "json",
	//			data: JSON.stringify(para)
	//		}).then(function (res) {
	//			hidePleaseWait();
	//			$scope.loadingstatus = "stop";
	//			if (res.data.IsSuccess && res.data.Data) {
	//				$scope.PEmployeeRemarksList = res.data.Data;

	//			} else {
	//				Swal.fire(res.data.ResponseMSG);
	//			}

	//		}, function (reason) {
	//			Swal.fire('Failed' + reason);
	//		});
	//	}

	//}

	//$scope.AddRowInRemarks = function (ind) {
	//	if ($scope.EmployeeRemarksList) {
	//		if ($scope.EmployeeRemarksList.length > ind + 1) {
	//			$scope.EmployeeRemarksList.splice(ind + 1, 0, {});
	//		} else {
	//			$scope.EmployeeRemarksList.push({});
	//		}
	//	}

	//};
	//$scope.delRowInRemarks = function (ind, beData) {
	//	if ($scope.EmployeeRemarksList && (!beData.TranId || beData.TranId == 0)) {
	//		if ($scope.EmployeeRemarksList.length > 1) {
	//			$scope.EmployeeRemarksList.splice(ind, 1);
	//		}
	//	} else if (beData.TranId > 0) {
	//		$scope.DelEmployeeRemarksById(beData);
	//	}
	//};

	//$scope.SaveUpdateEmployeeRemarks = function (beData) {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	beData.EmployeeId = $scope.newEmployeeRemarks.EmployeeId;

	//	if (beData.ForDateDet)
	//		beData.ForDate = $filter('date')(new Date(beData.ForDateDet.dateAD), 'yyyy-MM-dd');

	//	$http({
	//		method: 'POST',
	//		url: base_url + "Academic/Transaction/SaveEmployeeRemarks",
	//		headers: { 'Content-Type': undefined },

	//		transformRequest: function (data) {

	//			var formData = new FormData();
	//			formData.append("jsonData", angular.toJson(data.jsonData));

	//			if (data.files) {
	//				formData.append("file0", data.files[0]);
	//			}

	//			return formData;
	//		},
	//		data: { jsonData: beData, files: beData.AttachFile }
	//	}).then(function (res) {

	//		$scope.loadingstatus = "stop";
	//		hidePleaseWait();

	//		Swal.fire(res.data.ResponseMSG);

	//		if (res.data.IsSuccess == true) {
	//			$scope.GetEmpRemarks();
	//		}


	//	}, function (errormessage) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";

	//	});
	//}

	//$scope.DelEmployeeRemarksById = function (refData) {

	//	Swal.fire({
	//		title: 'Do you want to delete the selected data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Delete',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {
	//			$scope.loadingstatus = "running";
	//			showPleaseWait();

	//			var para = {
	//				TranId: refData.TranId
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Academic/Transaction/DelEmployeeRemarks",
	//				dataType: "json",
	//				data: JSON.stringify(para)
	//			}).then(function (res) {
	//				hidePleaseWait();
	//				$scope.loadingstatus = "stop";
	//				if (res.data.IsSuccess) {
	//					$scope.GetEmpRemarks();
	//				} else {
	//					Swal.fire(res.data.ResponseMSG);
	//				}

	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	});


	//};

	$scope.ShowPersonalImg = function (item) {
		$scope.viewImg = {
			ContentPath: '',
			File: null
		};
		if (item.DocPath && item.DocPath.length > 0) {
			$scope.viewImg.ContentPath = item.DocPath;
			$('#PersonalImg').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg.ContentPath = item.PhotoPath;
			$('#PersonalImg').modal('show');
		} else if (item.File) {
			$scope.viewImg.File = item.File;
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg.ContentPath = URL.createObjectURL(blob);

			$('#PersonalImg').modal('show');
		}

		else
			Swal.fire('No Image Found');

	};


	//$scope.GetEmpRemarksList = function () {

	//	if ($scope.newRemarkList.FromDateDet && $scope.newRemarkList.ToDateDet) {

	//		$scope.loadingstatus = "running";
	//		showPleaseWait();
	//		$scope.AllRemarksList = [];

	//		var para = {
	//			dateFrom: $filter('date')(new Date($scope.newRemarkList.FromDateDet.dateAD), 'yyyy-MM-dd'),
	//			dateTo: $filter('date')(new Date($scope.newRemarkList.ToDateDet.dateAD), 'yyyy-MM-dd'),
	//			remarksTypeId: $scope.newRemarkList.RemarksTypeId,
	//			remarksFor: $scope.newRemarkList.RemarksFor
	//		};
	//		$http({
	//			method: 'POST',
	//			url: base_url + "Academic/Transaction/GetEmpRemarksList",
	//			dataType: "json",
	//			data: JSON.stringify(para)
	//		}).then(function (res) {
	//			hidePleaseWait();
	//			$scope.loadingstatus = "stop";
	//			if (res.data.IsSuccess && res.data.Data) {
	//				$scope.AllRemarksList = res.data.Data;

	//			} else {
	//				Swal.fire(res.data.ResponseMSG);
	//			}

	//		}, function (reason) {
	//			Swal.fire('Failed' + reason);
	//		});

	//	}

	//}

	$scope.DefaultPhoto = '/wwwroot/dynamic/images/avatar-img.jpg';

	$scope.UploadEmployeePhoto = function (st) {


		var photo = st.Photo_TMP;
		var beData = {
			EmployeeId: st.EmployeeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/UpdateEmployeePhoto",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.stPhoto && data.stPhoto.length > 0)
					formData.append("photo", data.stPhoto[0]);

				return formData;
			},
			data: { jsonData: beData, stPhoto: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				st.PhotoPath = res.data.Data.ResponseId;
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

	}

	$scope.ChangeDate = function (field, dateStyle) {
		$timeout(function () {

			if (field == 'DOB') {
				if (dateStyle == 1) //AD
				{
					if ($scope.newEmployee.DOBADDet) {
						$scope.newEmployee.DOBBS_TMP = new Date($scope.newEmployee.DOBADDet.dateAD);
					}
				}
				else if (dateStyle == 2) //BS
				{
					if ($scope.newEmployee.DOBBSDet) {
						$scope.newEmployee.DOBAD_TMP = new Date($scope.newEmployee.DOBBSDet.dateAD);
					}
				}
			}
		 

		});
	}


	$scope.ValidateEmployeePhoto = function (obj, id, modelTmpName, modelDataName, imageId, fieldName, allowedExtensions, maxSizeMB=1) {
		GlobalServices.validateImageFile(obj, id, modelTmpName, modelDataName, imageId, fieldName, allowedExtensions,maxSizeMB);
		$scope.$applyAsync();
	};


	$scope.PrintEmployeeForm = function () {
		$('#printprofileform').printThis();
	}
});