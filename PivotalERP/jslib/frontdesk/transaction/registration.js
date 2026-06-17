
app.controller('regController', function ($scope, $http, $timeout, $filter, $translate, $rootScope, GlobalServices) {

	$scope.Title = 'Student Registration';
	$rootScope.ChangeLanguage();

	$scope.LoadData = function () {
		//Added by Suresh on Chaitra 10
		$('.select2').select2();

		$scope.ProvinceColl = GetStateList();
		$scope.DistrictColl = GetDistrictList();
		$scope.VDCColl = GetVDCList();

		$scope.ProvinceColl_Qry = mx($scope.ProvinceColl);
		$scope.DistrictColl_Qry = mx($scope.DistrictColl);
		$scope.VDCColl_Qry = mx($scope.VDCColl);
       //Ends
		$scope.SEETypeList = [{ id: 1, text: 'Government' }, { id: 2, text: 'Private' }]
		$scope.PlusTwoTypeList = [{ id: 1, text: 'Government' }, { id: 2, text: 'Private' }]

		$scope.Abt = {};
		$http({
			method: 'POST',
			url: base_url + "Home/GetAboutComp",
			dataType: "json",
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.Abt = res.data.Data;
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



		$scope.newEnquiry = {};
		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/GetAllEnquiryFormConfig",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEnquiry = res.data.Data;



			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});




		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		// $scope.perPageColl = GlobalServices.getPerPageList();
		$scope.GenderColl = GlobalServices.getGenderList();

		$scope.CasteList = [];
		GlobalServices.getCasteList().then(function (res) {
			$scope.CasteList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassShiftColl = [];
		GlobalServices.getClassShiftList().then(function (res) {
			$scope.ClassShiftColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassList = [];
		GlobalServices.getClassListForOR().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SectionList = [];
		GlobalServices.getSectionList().then(function (res) {
			$scope.SectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.MediumList = [];
		GlobalServices.getMediumList().then(function (res) {
			$scope.MediumList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.StudentTypeList = [];
		GlobalServices.getStudentTypeList().then(function (res) {
			$scope.StudentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.DocumentTypeList = [];
		GlobalServices.getDocumentTypeList().then(function (res) {
			$scope.DocumentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//Added by Suresh for Sunway on Kartik 27
		$scope.SourceList = [];
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllSourceList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SourceList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		//Ends

		

		$scope.ReligionList = GlobalServices.getReligionList();
		$scope.CountryList = GlobalServices.getCountryList();
		$scope.DisablityList = GlobalServices.getDisablityList();


		$scope.newRegistration = {
			RegistrationId: null,
			FirstName: '',
			MiddleName: '',
			LastName: '',
			Gender: 1,
			ContactNo: '',
			BirthCertificateNo: '',
			IsPhysicalDisability: false,
			Photo: null,
			PhotoPath: null,
			ClassId: null,
			IsSameAsPermanentAddress: false,
			AttachmentColl: [],
			AnyDisease: false,
			IsTransport: false,
			IsHostel: false,
			IsTiffin: false,
			IsTution: false,
			FatherName: '',
			PA_FullAddress: '',
			Department: '',
			Shift: '',
			PA_ProvinceId: null,
			PA_DistrictId: null,
			PA_LocalLevelId: null,
			CA_ProvinceId: null,
			CA_DistrictId: null,
			CA_LocalLevelId: null,
			Mode: 'Save'
		};
		$scope.showRefField = false;
		$scope.refFieldName = '';

	}

	$scope.onSourceChange = function (sourceId) {

		$scope.showRefField = false;
		$scope.refFieldName = '';
		$scope.newRegistration.ReferralCode = null;

		if (!sourceId) return;

		var src = $scope.SourceList.find(function (x) {
			return x.SourceId === sourceId;
		});

		if (src && src.IsRefDetReq) {
			$scope.showRefField = true;
			$scope.refFieldName = src.FieldName;
		}
	};

	//$scope.SamePAddress = function () {

	//	if ($scope.newRegistration.IsSameAsPermanentAddress == true) {
	//		$scope.newRegistration.CA_FullAddress = $scope.newRegistration.PA_FullAddress;
	//		$scope.newRegistration.CA_Province = $scope.newRegistration.PA_Province;
	//		$scope.newRegistration.CA_District = $scope.newRegistration.PA_District;
	//		$scope.newRegistration.CA_LocalLevel = $scope.newRegistration.PA_LocalLevel;
	//		$scope.newRegistration.CA_WardNo = $scope.newRegistration.PA_WardNo;
	//		$scope.newRegistration.CA_StreetName = $scope.newRegistration.PA_StreetName;
	//	} else {
	//		$scope.newRegistration.CA_FullAddress = '';
	//		$scope.newRegistration.CA_Province = '';
	//		$scope.newRegistration.CA_District = '';
	//		$scope.newRegistration.CA_LocalLevel = '';
	//		$scope.newRegistration.CA_WardNo = '';
	//		$scope.newRegistration.CA_StreetName = '';
	//	}
	//}

	$scope.SamePAddress = function () {

		if ($scope.newRegistration.IsSameAsPermanentAddress == true) {
			$scope.newRegistration.CA_FullAddress = $scope.newRegistration.PA_FullAddress;
			$scope.newRegistration.CA_ProvinceId = $scope.newRegistration.PA_ProvinceId;
			$scope.newRegistration.CA_DistrictId = $scope.newRegistration.PA_DistrictId;
			$scope.newRegistration.CA_LocalLevelId = $scope.newRegistration.PA_LocalLevelId;
			$scope.newRegistration.CA_WardNo = $scope.newRegistration.PA_WardNo;
			$scope.newRegistration.CA_StreetName = $scope.newRegistration.PA_StreetName;
		} else {
			$scope.newRegistration.CA_FullAddress = '';
			$scope.newRegistration.CA_ProvinceId = '';
			$scope.newRegistration.CA_DistrictId = '';
			$scope.newRegistration.CA_LocalLevelId = '';
			$scope.newRegistration.CA_WardNo = '';
			$scope.newRegistration.CA_StreetName = '';
		}
		$scope.ProvinceChange();
		$scope.DistrictChange();
		$scope.VDCChange();
	}


	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newRegistration.AttachmentColl) {
			if ($scope.newRegistration.AttachmentColl.length > 0) {
				$scope.newRegistration.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {
				angular.forEach(files, function (file) {
					$scope.newRegistration.AttachmentColl.push({
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
	$scope.ClearRegistrationPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newRegistration.PhotoData = null;
				$scope.newRegistration.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};

	$scope.ClearRegistration = function () {

		$scope.ClearRegistrationPhoto();
		$scope.newRegistration = {
			RegistrationId: null,
			FirstName: '',
			MiddleName: '',
			LastName: '',
			Gender: 1,
			ContactNo: '',
			BirthCertificateNo: '',
			IsPhysicalDisability: false,
			Photo: null,
			PhotoPath: null,
			ClassId: null,
			IsSameAsPermanentAddress: false,
			AttachmentColl: [],
			AnyDisease: false,
			IsTransport: false,
			IsHostel: false,
			IsTiffin: false,
			IsTution: false,
			Department: '',
			Shift: '',
			Mode: 'Save'
		};

		if (!$scope.printData) {
			$scope.printData = {};
		}
		angular.forEach(
			angular.element("input[type='file']"),
			function (inputElem) {
				angular.element(inputElem).val(null);
			});
	}

	$scope.IsValidRegistration = function () {
		if ($scope.newRegistration.FirstName.isEmpty()) {
			Swal.fire('Please ! Enter First Name');
			return false;
		}

		if ($scope.newRegistration.LastName.isEmpty()) {
			Swal.fire('Please ! Enter Last Name');
			return false;
		}

		if ($scope.newRegistration.ContactNo.isEmpty()) {
			Swal.fire('Please ! Enter Contact No.');
			return false;
		}

		//if ($scope.newRegistration.FatherName.isEmpty()) {
		//	Swal.fire('Please ! Enter Father Name');
		//	return false;
		//}

		if ($scope.newRegistration.PA_FullAddress.isEmpty()) {
			Swal.fire('Please ! Enter Address Name');
			return false;
		}

		if ($scope.showRefField) {
			if (!$scope.newRegistration.ReferralCode ||
				$scope.newRegistration.ReferralCode.isEmpty()) {

				Swal.fire('Please! Enter Enquiry Source Detail');
				return false;
			}
		}


		return true;
	}

	$scope.SaveUpdateRegistration = function () {
		if ($scope.IsValidRegistration() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newRegistration.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateRegistration();
					}
				});
			} else
				$scope.CallSaveUpdateRegistration();

		}
	};

	$scope.CallSaveUpdateRegistration = function () {
		$scope.loadingstatus = "running";
		//showPleaseWait();

		var filesColl = $scope.newRegistration.AttachmentColl;
		var photo = $scope.newRegistration.Photo_TMP;

		if ($scope.newRegistration.DOBDet) {
			$scope.newRegistration.DOB = $filter('date')(new Date($scope.newRegistration.DOBDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newRegistration.DOB = null;

		//if (moment.isDate($scope.newRegistration.DOB_TMP)) {
		//	$scope.newRegistration.DOB = $filter('date')(new Date($scope.newRegistration.DOB_TMP), 'yyyy-MM-dd');
		//}

		//added by Suresh on Chaitra 10 starts
		//var selectData1 = $('#cboProvincePA').select2('data');
		//if (selectData1 && selectData1.length > 0)
		//	province1 = selectData1[0].text.trim();

		//selectData1 = $('#cboDistrictPA').select2('data');
		//if (selectData1 && selectData1.length > 0)
		//	district1 = selectData1[0].text.trim();


		//selectData1 = $('#cboAreaPA').select2('data');
		//if (selectData1 && selectData1.length > 0)
		//	area1 = selectData1[0].text.trim();


		//var selectData2 = $('#cboProvinceCA').select2('data');
		//if (selectData2 && selectData2.length > 0)
		//	province2 = selectData2[0].text.trim();

		//selectData2 = $('#cboDistrictCA').select2('data');
		//if (selectData2 && selectData2.length > 0)
		//	district2 = selectData2[0].text.trim();


		//selectData2 = $('#cboAreaCA').select2('data');
		//if (selectData2 && selectData2.length > 0)
		//	area2 = selectData2[0].text.trim();

		//$scope.newRegistration.PA_Province = province1;
		//$scope.newRegistration.PA_District = district1;
		//$scope.newRegistration.PA_LocalLevel = area1;
		//$scope.newRegistration.CA_Province = province2;
		//$scope.newRegistration.CA_District = district2;
		//$scope.newRegistration.CA_LocalLevel = area2;
		if ($scope.newRegistration.PA_ProvinceId &&
			$scope.newRegistration.PA_DistrictId &&
			$scope.newRegistration.PA_LocalLevelId) {

			var paProvince = $('#cboProvincePA').select2('data');
			var paDistrict = $('#cboDistrictPA').select2('data');
			var paArea = $('#cboAreaPA').select2('data');

			$scope.newRegistration.PA_Province = paProvince?.[0]?.text?.trim() || null;
			$scope.newRegistration.PA_District = paDistrict?.[0]?.text?.trim() || null;
			$scope.newRegistration.PA_LocalLevel = paArea?.[0]?.text?.trim() || null;

		} else {
			$scope.newRegistration.PA_Province = null;
			$scope.newRegistration.PA_District = null;
			$scope.newRegistration.PA_LocalLevel = null;
		}

		// -------- Current Address --------
		if ($scope.newRegistration.CA_ProvinceId &&
			$scope.newRegistration.CA_DistrictId &&
			$scope.newRegistration.CA_LocalLevelId) {

			var caProvince = $('#cboProvinceCA').select2('data');
			var caDistrict = $('#cboDistrictCA').select2('data');
			var caArea = $('#cboAreaCA').select2('data');

			$scope.newRegistration.CA_Province = caProvince?.[0]?.text?.trim() || null;
			$scope.newRegistration.CA_District = caDistrict?.[0]?.text?.trim() || null;
			$scope.newRegistration.CA_LocalLevel = caArea?.[0]?.text?.trim() || null;

		} else {
			$scope.newRegistration.CA_Province = null;
			$scope.newRegistration.CA_District = null;
			$scope.newRegistration.CA_LocalLevel = null;
		}
		//Ends

		$scope.printData = angular.copy($scope.newRegistration);
		$http({
			method: 'POST',
			url: base_url + "Home/SaveReg",
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
				return formData;
			},
			data: { jsonData: $scope.newRegistration, files: filesColl, stPhoto: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			//	hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				// Extracting AutoManualNo from the response message
				var match = res.data.ResponseMSG.match(/\((.*?)\)/); // Extracts value inside parentheses
				if (match && match[1]) {
					$scope.newRegistration.AutoManualNo = match[1]; // Assign extracted value
				

				}
				var today = new Date();
				var formattedDate = today.getFullYear() + "-" +
					("0" + (today.getMonth() + 1)).slice(-2) + "-" +
					("0" + today.getDate()).slice(-2);

				$scope.newRegistration.EnquiryMiti = formattedDate;

				$scope.PrintData();

				// Delay clearing the form so the print template (bound to newRegistration) is still populated
				$timeout(function () {
					$scope.ClearRegistration();
				}, 5000);
			}
		}, function (errormessage) {
			//hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetRegistrationById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			HouseNameId: refData.HouseNameId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetRegistrationById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newRegistration = res.data.Data;
				$scope.newRegistration.Mode = 'Modify';

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.PrintData = function () {
		$('#printcard').printThis()
	};

	$scope.ProvinceChange = function () {
		if ($scope.newRegistration.PA_ProvinceId && $scope.newRegistration.PA_ProvinceId > 0) {
			$scope.newRegistration.PA_ProvinceName = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.id == $scope.newRegistration.PA_ProvinceId).text;
			$scope.newRegistration.PA_Province = $scope.newRegistration.PA_ProvinceName;
		} else {
			$scope.newRegistration.PA_ProvinceName = '';
			$scope.newRegistration.PA_Province = '';
		}

		if ($scope.newRegistration.CA_ProvinceId && $scope.newRegistration.CA_ProvinceId > 0) {
			$scope.newRegistration.CA_ProvinceName = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.id == $scope.newRegistration.CA_ProvinceId).text;
			$scope.newRegistration.CA_Province = $scope.newRegistration.CA_ProvinceName;
		} else {
			$scope.newRegistration.CA_ProvinceName = '';
			$scope.newRegistration.CA_Province = '';
		}
	}



	$scope.DistrictChange = function () {
		if ($scope.newRegistration.PA_DistrictId && $scope.newRegistration.PA_DistrictId > 0) {
			$scope.newRegistration.PA_DistrictName = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == $scope.newRegistration.PA_DistrictId).text;
			$scope.newRegistration.PA_District = $scope.newRegistration.PA_DistrictName;
		} else {
			$scope.newRegistration.PA_DistrictName = '';
			$scope.newRegistration.PA_District = '';
		}

		if ($scope.newRegistration.CA_DistrictId && $scope.newRegistration.CA_DistrictId > 0) {
			$scope.newRegistration.CA_DistrictName = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.id == $scope.newRegistration.CA_DistrictId).text;
			$scope.newRegistration.CA_District = $scope.newRegistration.CA_DistrictName;
		} else {
			$scope.newRegistration.CA_DistrictName = '';
			$scope.newRegistration.CA_District = '';
		}		
	}


	$scope.VDCChange = function () {
		if ($scope.newRegistration.PA_LocalLevelId && $scope.newRegistration.PA_LocalLevelId > 0) {
			$scope.newRegistration.PA_LocalLevelName = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == $scope.newRegistration.PA_LocalLevelId).text;
			$scope.newRegistration.PA_LocalLevel = $scope.newRegistration.PA_LocalLevelName;
		} else {
			$scope.newRegistration.PA_LocalLevelName = '';
			$scope.newRegistration.PA_LocalLevel = '';
		}
		if ($scope.newRegistration.CA_LocalLevelId && $scope.newRegistration.CA_LocalLevelId > 0) {
			$scope.newRegistration.CA_LocalLevelName = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.id == $scope.newRegistration.CA_LocalLevelId).text;
			$scope.newRegistration.CA_LocalLevel = $scope.newRegistration.CA_LocalLevelName;
		} else {
			$scope.newRegistration.CA_LocalLevelName = '';
			$scope.newRegistration.CA_LocalLevel = '';
		}	

	}

	$scope.GenderChange = function () {
		if ($scope.newRegistration.Gender > 0) {
			$scope.newRegistration.GenderName = mx($scope.GenderColl).firstOrDefault(p1 => p1.id == $scope.newRegistration.Gender).text;
		} else
			$scope.newRegistration.GenderName = '';
	}

	$scope.CasteChange = function () {
		if ($scope.newRegistration.CasteId > 0) {
			$scope.newRegistration.CasteName = mx($scope.CasteList).firstOrDefault(p1 => p1.CasteId == $scope.newRegistration.CasteId).text;
		} else
			$scope.newRegistration.CasteName = '';
	}

	$scope.CourseChange = function () {
		if ($scope.newRegistration.ClassId > 0) {
			$scope.newRegistration.ClassName = mx($scope.ClassList).firstOrDefault(p1 => p1.ClassId == $scope.newRegistration.ClassId).text;
		} else
			$scope.newRegistration.ClassName = '';
	}

	$scope.ShiftChange = function () {
		if ($scope.newRegistration.ClassShiftId > 0) {
			$scope.newRegistration.ClassShiftName = mx($scope.ClassShiftColl).firstOrDefault(p1 => p1.ClassShiftId == $scope.newRegistration.ClassShiftId).text;
		} else
			$scope.newRegistration.ClassShiftName = '';
	}

	$scope.SEETypeChange = function () {
		if ($scope.newRegistration.SEETypeId > 0) {
			$scope.newRegistration.SEETypeName = mx($scope.SEETypeList).firstOrDefault(p1 => p1.id == $scope.newRegistration.SEETypeId).text;
		} else
			$scope.newRegistration.SEETypeName = '';
	}

	$scope.PlusTwoChange = function () {
		if ($scope.newRegistration.PlusTwoId > 0) {
			$scope.newRegistration.PlusTwoName = mx($scope.PlusTwoTypeList).firstOrDefault(p1 => p1.id == $scope.newRegistration.PlusTwoId).text;
		} else
			$scope.newRegistration.PlusTwoName = '';
	}

	$scope.SourceChange = function () {
		if ($scope.newRegistration.SourceId > 0) {
			$scope.newRegistration.SourceName = mx($scope.SourceList).firstOrDefault(p1 => p1.SourceId == $scope.newRegistration.SourceId).Name;
		} else
			$scope.newRegistration.SourceNamee = '';
	}
});