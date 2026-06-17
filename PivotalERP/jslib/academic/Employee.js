app.controller('EmployeeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Employee';


	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.GenderColl = GlobalServices.getGenderList();

		//$scope.CasteList = [];
		//GlobalServices.getCasteList().then(function (res) {
		//	$scope.CasteList = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		
		$scope.BloodGroupList = GlobalServices.getBloodGroupList();
		$scope.ReligionList = GlobalServices.getReligionList();
		$scope.CountryList = GlobalServices.getCountryList();
		$scope.DisablityList = GlobalServices.getDisablityList();


		$scope.newEmployee = {
			EmployeeId: null,
			EmployeeCode: '',
			EnrollNumber:'',
			FirstName: '',
			MiddleName: '',
			LastName: '',
			Gender: 1,
			DOB_AD_TMP: null,
			
			PhoneNo: '',
			OfficePhone: '',
			EmailId: '',
			MaritalStatus: '',
			SpouseName: '',
			AnniversaryDate_TMP: null,
			FatherName: '',
			MotherName: '',
			GrandFatherName: '',

			IsPhysicalDisability: false,
			Photo: null,
			PhotoPath: null,
			Signature: null,
			SignaturePath: null,
			
			PanId: null,
			CitizenshipNo: 0,
			CitizenshipIssueDate_TMP: null,
			CitizenshipIssuePlace: '',
			DrivingLicenceNo: 0,
			LicenceIssueDate_TMP: null,
			LicenceExpiryDate_TMP: null,
			LicenceIssuePlace: '',
			PassportNo: 0,
			PassportIssueDate_TMP: null,
			PassportExpiryDate_TMP: null,
			PassportIssuePlace: '',
			CardNo:0,
			Country: '',
			State: '',
			Zone: '',
			District: '',
			City: '',
			MunicipalityVDC: '',
			Ward: '',
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
			IsTeaching: 2,
			SubjectTeacher: '',
			ServiceType: '',
			DateOfJoining: null,
			DateOfConfirmation:null,
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
			EntryDate: '',
			BankAccountDetailsColl: [],
			InsuranceCompany: '',
			PolicyName: '',
			PolicyNo: '',
			PolicyAmount: '',
			PolicyStartDate_TMP: null,
			PolicyLastDate_TMP: null,
			PaymentType: '',
			StartMonth: '',
			IsDeduct: true,
			Remarks: '',
			InsuranceType: '',
			H_IsDeduct: false,
			AcademicQualificationDetailsColl: [],
			WorkExperienceDetailsColl:[],
			AttachmentColl: [],


			Mode: 'Save'
		};
		$scope.newEmployee.BankAccountDetailsColl.push({});
		$scope.newEmployee.AcademicQualificationDetailsColl.push({});
		$scope.newEmployee.WorkExperienceDetailsColl.push({});
		
		$scope.newEmployee.AttachmentColl.push({});

		//$scope.GetAllEmployeeList();


	}


	$scope.SamePAddress = function () {

		if ($scope.newEmployee.IsSameAsPermanentAddress == true) {
			$scope.newEmployee.TA_Country = $scope.newEmployee.Country;
			$scope.newEmployee.TA_State = $scope.newEmployee.State;
			$scope.newEmployee.TA_Zone = $scope.newEmployee.Zone;
			$scope.newEmployee.TA_District = $scope.newEmployee.District;
			$scope.newEmployee.TA_City = $scope.newEmployee.City;
			$scope.newEmployee.TA_MunicipalityVDC = $scope.newEmployee.MunicipalityVDC;
			$scope.newEmployee.TA_Ward = $scope.newEmployee.Ward;
			$scope.newEmployee.TA_Street = $scope.newEmployee.Street;
			$scope.newEmployee.TA_HouseNo = $scope.newEmployee.HouseNo;
			$scope.newEmployee.TA_FullAddress = $scope.newEmployee.FullAddress;
		} else {
			$scope.newEmployee.TA_Country = '';
			$scope.newEmployee.TA_State = '';
			$scope.newEmployee.TA_Zone = '';
			$scope.newEmployee.TA_District = '';
			$scope.newEmployee.TA_City = '';
			$scope.newEmployee.TA_MunicipalityVDC = '';
			$scope.newEmployee.TA_Ward = '';
			$scope.newEmployee.TA_Street = '';
			$scope.newEmployee.TA_HouseNo = '';
			$scope.newEmployee.TA_FullAddress = '';
		}
	}
	//Bank Account Details Coll
	$scope.AddBankAccountDetails = function (ind) {
		if ($scope.newEmployee.BankAccountDetailsColl) {
			if ($scope.newEmployee.BankAccountDetailsColl.length > ind + 1) {
				$scope.newEmployee.BankAccountDetailsColl.splice(ind + 1, 0, {
					BankName: '',
					AccountName: '',
					AccountNo: '',
					Branch: '',
					ForPayRoll: '',

				})
			} else {
				$scope.newEmployee.BankAccountDetailsColl.push({
					BankName: '',
					AccountName: '',
					AccountNo: '',
					Branch: '',
					ForPayRoll: '',
				})
			}
		}
	};
	$scope.delBankAccountDetails = function (ind) {
		if ($scope.newEmployee.BankAccountDetailsColl) {
			if ($scope.newEmployee.BankAccountDetailsColl.length > 1) {
				$scope.newEmployee.BankAccountDetailsColl.splice(ind, 1);
			}
		}
	};

	//Academic Qualification Details Coll
	$scope.AddAcademicQualificationDetails = function (ind) {
		if ($scope.newEmployee.AcademicQualificationDetailsColl) {
			if ($scope.newEmployee.AcademicQualificationDetailsColl.length > ind + 1) {
				$scope.newEmployee.AcademicQualificationDetailsColl.splice(ind + 1, 0, {
					DegreeName: '',
					BoardUniversity: '',
					PassedYear: '',
					GradePercentage: '',
					

				})
			} else {
				$scope.newEmployee.AcademicQualificationDetailsColl.push({
					DegreeName: '',
					BoardUniversity: '',
					PassedYear: '',
					GradePercentage: '',
				})
			}
		}
	};
	$scope.delAcademicQualificationDetails = function (ind) {
		if ($scope.newEmployee.AcademicQualificationDetailsColl) {
			if ($scope.newEmployee.AcademicQualificationDetailsColl.length > 1) {
				$scope.newEmployee.AcademicQualificationDetailsColl.splice(ind, 1);
			}
		}
	};

	//Work Experience Details Coll
	$scope.AddWorkExperienceDetails = function (ind) {
		if ($scope.newEmployee.WorkExperienceDetailsColl) {
			if ($scope.newEmployee.WorkExperienceDetailsColl.length > ind + 1) {
				$scope.newEmployee.WorkExperienceDetailsColl.splice(ind + 1, 0, {
					DegreeName: '',
					BoardUniversity: '',
					PassedYear: '',
					GradePercentage: '',


				})
			} else {
				$scope.newEmployee.WorkExperienceDetailsColl.push({
					DegreeName: '',
					BoardUniversity: '',
					PassedYear: '',
					GradePercentage: '',
				})
			}
		}
	};
	$scope.delWorkExperienceDetails = function (ind) {
		if ($scope.newEmployee.WorkExperienceDetailsColl) {
			if ($scope.newEmployee.WorkExperienceDetailsColl.length > 1) {
				$scope.newEmployee.WorkExperienceDetailsColl.splice(ind, 1);
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
		$scope.newEmployee = {
			EmployeeId: null,
			EmployeeCode: '',
			EnrollNumber: '',
			FirstName: '',
			MiddleName: '',
			LastName: '',
			Gender: 1,
			DOB_AD_TMP: null,

			PhoneNo: '',
			OfficePhone: '',
			EmailId: '',
			MaritalStatus: '',
			SpouseName: '',
			AnniversaryDate_TMP: null,
			FatherName: '',
			MotherName: '',
			GrandFatherName: '',

			IsPhysicalDisability: false,
			Photo: null,
			PhotoPath: null,
			Signature: null,
			SignaturePath: null,

			PanId: null,
			CitizenshipNo: 0,
			CitizenshipIssueDate_TMP: null,
			CitizenshipIssuePlace: '',
			DrivingLicenceNo: 0,
			LicenceIssueDate_TMP: null,
			LicenceExpiryDate_TMP: null,
			LicenceIssuePlace: '',
			PassportNo: 0,
			PassportIssueDate_TMP: null,
			PassportExpiryDate_TMP: null,
			PassportIssuePlace: '',
			CardNo: 0,
			Country: '',
			State: '',
			Zone: '',
			District: '',
			City: '',
			MunicipalityVDC: '',
			Ward: '',
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
			IsTeaching: 2,
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
			EntryDate: '',
			BankAccountDetailsColl: [],
			InsuranceCompany: '',
			PolicyName: '',
			PolicyNo: '',
			PolicyAmount: '',
			PolicyStartDate_TMP: null,
			PolicyLastDate_TMP: null,
			PaymentType: '',
			StartMonth: '',
			IsDeduct: true,
			Remarks: '',
			InsuranceType: '',
			H_IsDeduct: false,
			AcademicQualificationDetailsColl: [],
			WorkExperienceDetailsColl: [],
			AttachmentColl: [],


			Mode: 'Save'
		};
		$scope.newEmployee.BankAccountDetailsColl.push({});
		$scope.newEmployee.AcademicQualificationDetailsColl.push({});
		$scope.newEmployee.WorkExperienceDetailsColl.push({});

		$scope.newEmployee.AttachmentColl.push({});
	}


	$scope.ClearEmployeePhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newEmployee.PhotoData = null;
				$scope.newEmployee.Photo_TMP = [];
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
	


	$scope.IsValidEmployee = function () {
		if ($scope.newEmployee.FirstName.isEmpty()) {
			Swal.fire('Please ! Enter Employee Name');
			return false;
		}

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

		var photo = $scope.newEmployee.Photo_TMP;
		var signature = $scope.newEmployee.Signature_TMP;

		if ($scope.newEmployee.DOB_ADDet) {
			$scope.newEmployee.DOB_AD = $scope.newEmployee.DOB_ADDet.dateAD;
		} else
			$scope.newEmployee.DOB_ADDate = null;

		if ($scope.newEmployee.AnniversaryDateDet) {
			$scope.newEmployee.AnniversaryDate = $scope.newEmployee.AnniversaryDateDet.dateAD;
		} else
			$scope.newEmployee.AnniversaryDateDate = null;

		if ($scope.newEmployee.CitizenshipIssueDateDet) {
			$scope.newEmployee.CitizenshipIssueDate = $scope.newEmployee.CitizenshipIssueDateDet.dateAD;
		} else
			$scope.newEmployee.CitizenshipIssueDate = null;

		if ($scope.newEmployee.LicenceIssueDateDet) {
			$scope.newEmployee.LicenceIssueDate = $scope.newEmployee.LicenceIssueDateDet.dateAD;
		} else
			$scope.newEmployee.LicenceIssueDate = null;

		if ($scope.newEmployee.LicenceExpiryDateDet) {
			$scope.newEmployee.LicenceExpiryDate = $scope.newEmployee.LicenceExpiryDateDet.dateAD;
		} else
			$scope.newEmployee.LicenceExpiryDate = null;

		if ($scope.newEmployee.PassportIssueDateDet) {
			$scope.newEmployee.PassportIssueDate = $scope.newEmployee.PassportIssueDateDet.dateAD;
		} else
			$scope.newEmployee.PassportIssueDate = null;

		if ($scope.newEmployee.PassportExpiryDateDet) {
			$scope.newEmployee.PassportExpiryDate = $scope.newEmployee.PassportExpiryDateDet.dateAD;
		} else
			$scope.newEmployee.PassportExpiryDate = null;


		if ($scope.newEmployee.PolicyStartDateDet) {
			$scope.newEmployee.PolicyStartDate = $scope.newEmployee.PolicyStartDateDet.dateAD;
		} else
			$scope.newEmployee.PolicyStartDate = null;

		if ($scope.newEmployee.PolicyLastDateDet) {
			$scope.newEmployee.PolicyLastDate = $scope.newEmployee.PolicyLastDateDet.dateAD;
		} else
			$scope.newEmployee.PolicyLastDate = null;

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

				return formData;
			},
			data: { jsonData: $scope.newEmployee }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearEmployee();
				$scope.GetAllEmployeeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

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
					if (res.data.IsSuccess) {
						$scope.GetAllEmployeeList();
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

});