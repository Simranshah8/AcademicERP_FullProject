app.controller('StudentController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, Excel, $translate) {
	$scope.Title = 'Student';
	$rootScope.ChangeLanguage();


	let stream = null;
	let video = document.querySelector("#video");
	let canvas = document.querySelector('#canvas');
	$scope.takePhotoFromCamera = async function () {

		if ($scope.webCam.Start == true) {
			$scope.webCam.Start = false;

			canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);
			$scope.newStudent.PhotoData = canvas.toDataURL('image/jpeg');

			try {
				// stop only video
				stream.getVideoTracks()[0].stop();

			} catch { }
			stream = null;

		} else {
			$scope.webCam.Start = true;

			stream = null;

			try {
				stream = await navigator.mediaDevices.getUserMedia({ video: true, audio: false });
			}
			catch (error) {
				alert(error.message);
				return;
			}

			try {
				video.srcObject = stream;
			} catch {
				video.src = URL.createObjectURL(stream);
			}

			//video.style.display = 'block';
		}
	}
	$scope.LoadData = function () {

		$scope.MonthList = [];
		//GlobalServices.ChangeLanguage(); 
		$('.select2').select2(
			{
				allowClear: true,
				openOnEnter: true,
			}
		);

		//Added by Suresh on Chaitra 10
		$scope.ProvinceColl = GetStateList();
		$scope.DistrictColl = GetDistrictList();
		$scope.VDCColl = GetVDCList();

		$scope.ProvinceColl_Qry = mx($scope.ProvinceColl);
		$scope.DistrictColl_Qry = mx($scope.DistrictColl);
		$scope.VDCColl_Qry = mx($scope.VDCColl);
		//Ends

		$scope.webCam = {
			Start: false
		};

		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;

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

				$scope.SelectedClassSemesterList = [];
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveBatch == true) {

				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveClassYear == true) {

				$scope.ClassYearList = [];
				$scope.SelectedClassClassYearList = [];
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.Labels = {
			RegdNo: ''
		};

		//$translate.getTranslationTable()['REGDNO_LNG']
		$translate('REGDNO_LNG').then(function (data) {
			$scope.Labels.RegdNo = data;
		});

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.GenderColl = GlobalServices.getGenderList();
		$scope.CountryList = GlobalServices.getCountryList();

		//if (EntityId) {
		//	$scope.UDFFeildsColl = [];
		//	$http({
		//		method: 'GET',
		//		url: base_url + "Setup/Security/GetAllUDFFields?entityId=" + EntityId,
		//		dataType: "json"
		//	}).then(function (res) {
		//		if (res.data.IsSuccess && res.data.Data) {
		//			$scope.UDFFeildsColl = res.data.Data;
		//		}
		//	}, function (reason) {
		//		Swal.fire('Failed' + reason);
		//	});
		//      }


		$scope.CasteList = [];
		GlobalServices.getCasteList().then(function (res) {
			$scope.CasteList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassList = [];
		GlobalServices.getClassList().then(function (res) {
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

		$scope.SectionForTranList = [];
		GlobalServices.getSectionForTran().then(function (res) {
			$scope.SectionForTranList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassSectionList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AcademicYearList = [];
		GlobalServices.getAcademicYearList().then(function (res) {
			$scope.AcademicYearList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.MediumList = [];
		GlobalServices.getMediumList().then(function (res) {
			$scope.MediumList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.HouseNameList = [];
		GlobalServices.getHouseNameList().then(function (res) {
			$scope.HouseNameList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.HouseDressList = [];
		GlobalServices.getHouseDressList().then(function (res) {
			$scope.HouseDressList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.StudentTypeList = [];
		GlobalServices.getStudentTypeList().then(function (res) {
			$scope.StudentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.BoardList = [];
		GlobalServices.getBoardList().then(function (res) {
			$scope.BoardList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.TransportPointList = [];
		//GlobalServices.getList().then(function (res) {
		//	$scope.List = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		$scope.BoardersTypeList = [];
		//GlobalServices.getList().then(function (res) {
		//	$scope.List = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		$scope.DocumentTypeList = [];
		GlobalServices.getDocumentTypeList().then(function (res) {
			$scope.DocumentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.BloodGroupList = GlobalServices.getBloodGroupList();
		$scope.ReligionList = GlobalServices.getReligionList();
		$scope.CountryList = GlobalServices.getCountryList();
		$scope.NationalityList = GlobalServices.getNationalityList();

		$scope.ProvinceList = GetStateList();
		$scope.DistrictList = GetDistrictList();
		$scope.VDCColl = GetVDCList();

		$scope.ZoneList = GetZoneList();

		$scope.ProvinceColl_Qry = mx($scope.ProvinceColl);
		$scope.DistrictColl_Qry = mx($scope.DistrictColl);
		$scope.VDCColl_Qry = mx($scope.VDCColl);

		$scope.DisablityList = GlobalServices.getDisablityList();

		$scope.SiblingRelationList = ["Brother", "Sister"];

		$scope.BranchColl = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchListForEntry",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchColl = res.data.Data;
			}
		}, function (reason) {
			alert('Failed' + reason);
		});


		$scope.currentPages = {
			HouseName: 1,
			HouseDress: 1,
			StudentType: 1,
			Medium: 1,
			ViewStudent: 1
		};

		$scope.searchData = {
			HouseName: '',
			HouseDress: '',
			StudentType: '',
			Medium: '',
			ViewStudent: ''
		};

		$scope.perPage = {
			HouseName: GlobalServices.getPerPageRow(),
			HouseDress: GlobalServices.getPerPageRow(),
			StudentType: GlobalServices.getPerPageRow(),
			Medium: GlobalServices.getPerPageRow(),
			ViewStudent: GlobalServices.getPerPageRow(),
		};

		$scope.newStudent = {
			StudentId: null,
			RegNo: '',
			AdmitDate_TMP: new Date(),
			FirstName: '',
			MiddleName: '',
			LastName: '',
			Gender: 1,
			ContactNo: '',
			IsPhysicalDisability: false,
			isEDJ: false,
			Photo: null,
			PhotoPath: null,
			Signature: null,
			SignaturePath: null,
			ClassId: null,
			SectionId: null,
			RollNo: 0,
			IfGurandianIs: 3,
			IsSameAsPermanentAddress: false,
			AcademicDetailsColl: [],
			SiblingDetailColl_TMP: [],
			AttachmentColl: [],
			LedgerPanaNo: '',
			IsNewStudent: true,
			F_AnnualIncome: 0,
			M_AnnualIncome: 0,
			EnquiryNo: '',
			ClassId_First: null,
			AdmissionEnquiryId: null,

			PassOutClassId: null,
			PassOutSymbolNo: '',
			PassOutGPA: null,
			PassOutGrade: '',
			PassOutRemarks: '',
			FamilyType: 0,
			EnrollNo: 0,
			CardNo: 0,
			PA_WardNo: 0,
			CA_WardNo: 0,
			PA_ProvinceId: null,
			PA_DistrictId: null,
			PA_LocalLevelId: null,
			CA_ProvinceId: null,
			CA_DistrictId: null,
			CA_LocalLevelId: null,
			Mode: 'Save'
		};

		$scope.newStudent.AcademicDetailsColl.push({});
		$scope.newStudent.SiblingDetailColl_TMP.push({});
		$timeout(function () {
			$('#cboStudent0').select2();
		});


		$scope.viewStudent = {
			ClassId: null,
			SectionId: null,
			StudentList: [],
			ShowPhoto: false,
		};
		//$scope.GetAllHouseNameList();
	/*	$scope.LoadClassSectionList();*/
		$scope.GetAutoNumber();

		$scope.RoomList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllRoomListForMapping",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.RoomList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.TransportPointList = [];
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetAllTransportPointList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TransportPointList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetEnquiryById();
		$scope.GetRegistrationById();

		$scope.StudentConfig = {};
		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetStudentConfiguration",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentConfig = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
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
				if ($scope.CompanyConfig.Country == 'Nepal') {
					$scope.newStudent.Nationality = 'Nepali';
				}
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

	$scope.updateDOB = function (dateStyle)
	{
		$timeout(function () {
			if (dateStyle == 1) {

				if ($scope.newStudent.DOBADDet) {
					$scope.newStudent.DOBBS_TMP = new Date($scope.newStudent.DOBADDet.dateAD);
				}
				else {
					$scope.newStudent.DOBBS_TMP = null;
				}
			}
			else if (dateStyle == 2) {
				if ($scope.newStudent.DOBBSDet) {
					$scope.newStudent.DOBAD_TMP = new Date($scope.newStudent.DOBBSDet.dateAD);
					$scope.newStudent.DOBAD_TMP1 = new Date($scope.newStudent.DOBBSDet.dateAD);
				}
				else {
					$scope.newStudent.DOBAD_TMP = null;
				}
			}
		});

		//if ($scope.newStudent.DOB_ADDet) {
		//	var englishDate = $filter('date')(new Date($scope.newStudent.DOB_ADDet.dateAD), 'yyyy-MM-dd');

		//	$scope.newStudent.DOB_AD = englishDate;

		//	if (!$scope.$$phase) {
		//		$scope.$apply();
		//	}
		//}
	};
	$scope.updateDOB_BS = function () {
		if ($scope.newStudent.DOBAD) {
			$scope.$applyAsync(function () {
				$scope.newStudent.DOB_TMP = new Date($scope.newStudent.DOBAD);
			});
		}
	};


	$scope.getProvinceId = function (provinceText) {
		var province = $scope.ProvinceList.find(p => p.text === provinceText);
		return province ? province.id : null;
	};
	$scope.getDistrictId = function (districtText) {
		var district = $scope.DistrictList.find(p => p.text === districtText);
		return district ? district.id : null;
	};


	$scope.GetStudentForSibling = function (sb, ind) {

		$timeout(function () {

			if (sb.SelectedClassSection) {
				var para = {
					ClassId: sb.SelectedClassSection.ClassId,
					SectionId: sb.SelectedClassSection.SectionId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/GetSB_StudentForTran",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					sb.StudentList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});


	};

/*	$scope.LoadClassSectionList = function () {
		$scope.ClassSection = {};
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}*/

	$scope.LoadGuradianDet = function () {

		$('#imgGuardian').attr('src', '');

		if ($scope.newStudent.IfGurandianIs == 1) {
			$scope.newStudent.GuardianName = $scope.newStudent.FatherName;
			$scope.newStudent.G_Relation = 'Father';
			$scope.newStudent.G_Profesion = $scope.newStudent.F_Profession;
			$scope.newStudent.G_ContactNo = $scope.newStudent.F_ContactNo;
			$scope.newStudent.G_Email = $scope.newStudent.F_Email;
			$scope.newStudent.GuardianPhotoData = $scope.newStudent.FatherPhotoData;
			$scope.newStudent.GuardianPhotoPath = $scope.newStudent.FatherPhotoPath;

		} else if ($scope.newStudent.IfGurandianIs == 2) {
			$scope.newStudent.GuardianName = $scope.newStudent.MotherName;
			$scope.newStudent.G_Relation = 'Mother';
			$scope.newStudent.G_Profesion = $scope.newStudent.M_Profession;
			$scope.newStudent.G_ContactNo = $scope.newStudent.M_Contact;
			$scope.newStudent.G_Email = $scope.newStudent.M_Email;
			$scope.newStudent.GuardianPhotoData = $scope.newStudent.MotherPhotoData;
			$scope.newStudent.GuardianPhotoPath = $scope.newStudent.MotherPhotoPath;

		} else if ($scope.newStudent.IfGurandianIs == 3) {
			$scope.newStudent.GuardianName = '';
			$scope.newStudent.G_Relation = '';
			$scope.newStudent.G_Profesion = '';
			$scope.newStudent.G_ContactNo = '';
			$scope.newStudent.G_Email = '';
			$scope.newStudent.GuardianPhotoData = null;
			$scope.newStudent.GuardianPhotoPath = null;
		}
	};

	//$scope.SamePAddress = function () {

	//	if ($scope.newStudent.IsSameAsPermanentAddress == true) {
	//		$scope.newStudent.CA_FullAddress = $scope.newStudent.PA_FullAddress;
	//		$scope.newStudent.CA_Province = $scope.newStudent.PA_Province;
	//		$scope.newStudent.CA_District = $scope.newStudent.PA_District;
	//		$scope.newStudent.CA_LocalLevel = $scope.newStudent.PA_LocalLevel;
	//		$scope.newStudent.CA_WardNo = $scope.newStudent.PA_WardNo;
	//		$scope.newStudent.StreetName = $scope.newStudent.PA_Village;
	//		$scope.newStudent.CA_HouseNo = $scope.newStudent.PA_HouseNo;

	//	} else {
	//		$scope.newStudent.CA_FullAddress = '';
	//		$scope.newStudent.CA_Province = '';
	//		$scope.newStudent.CA_District = '';
	//		$scope.newStudent.CA_LocalLevel = '';
	//		$scope.newStudent.CA_WardNo = '';
	//		$scope.newStudent.StreetName = '';
	//		$scope.newStudent.CA_Village = '';
	//		$scope.newStudent.CA_HouseNo = '';
	//	}
	//}

	$scope.SamePAddress = function () {

		if ($scope.newStudent.IsSameAsPermanentAddress == true) {
			$scope.newStudent.CA_FullAddress = $scope.newStudent.PA_FullAddress;
			$scope.newStudent.CA_ProvinceId = $scope.newStudent.PA_ProvinceId;
			$scope.newStudent.CA_DistrictId = $scope.newStudent.PA_DistrictId;
			$scope.newStudent.CA_LocalLevelId = $scope.newStudent.PA_LocalLevelId;
			$scope.newStudent.CA_WardNo = $scope.newStudent.PA_WardNo;
			$scope.newStudent.StreetName = $scope.newStudent.PA_Village;
			$scope.newStudent.CA_HouseNo = $scope.newStudent.PA_HouseNo;

		} else {
			$scope.newStudent.CA_FullAddress = '';
			$scope.newStudent.CA_ProvinceId = '';
			$scope.newStudent.CA_DistrictId = '';
			$scope.newStudent.CA_LocalLevelId = '';
			$scope.newStudent.CA_WardNo = '';
			$scope.newStudent.StreetName = '';
			$scope.newStudent.CA_Village = '';
			$scope.newStudent.CA_HouseNo = '';
		}
	}

	$scope.AddAcademicDetails = function (ind) {
		if ($scope.newStudent.AcademicDetailsColl) {
			if ($scope.newStudent.AcademicDetailsColl.length > ind + 1) {
				$scope.newStudent.AcademicDetailsColl.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newStudent.AcademicDetailsColl.push({
					ClassName: ''
				})
			}
		}
	};
	$scope.delAcademicDetails = function (ind) {
		if ($scope.newStudent.AcademicDetailsColl) {
			if ($scope.newStudent.AcademicDetailsColl.length > 1) {
				$scope.newStudent.AcademicDetailsColl.splice(ind, 1);
			}
		}
	};
	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newStudent.AttachmentColl) {
			if ($scope.newStudent.AttachmentColl.length > 0) {
				$scope.newStudent.AttachmentColl.splice(ind, 1);
			}
		}
	}

	$scope.AddMoreFiles = function (files, docType, des) {
		if (files && docType) {
			var allowedTypes = [
				'image/png',
				'image/gif',
				'image/jpeg',
				'image/jpg',
				'application/pdf',
				'application/msword'
			];

			angular.forEach(files, function (file) {
				if (!allowedTypes.includes(file.type)) {
					Swal.fire({
						icon: 'warning',
						title: 'Invalid File Type',
						text: 'Only PNG, GIF, JPG, JPEG, PDF, and DOC files are allowed.'
					});
					return; 
				}

				// Check file size <= 1 MB
				if (file.size > 1048576) {
					Swal.fire({
						icon: 'warning',
						title: 'File Too Large',
						text: 'Each file must be 1 MB or smaller.'
					});
					return; 
				}

				// Add file to collection
				$scope.newStudent.AttachmentColl.push({
					DocumentTypeId: docType.id,
					DocumentTypeName: docType.text,
					File: file,
					Name: file.name,
					Type: file.type,
					Size: file.size,
					Description: des,
					Path: null
				});
			});

			// Reset inputs
			$scope.docType = null;
			$scope.attachFile = null;
			$scope.docDescription = '';
			$('#flMoreFiles').val('');
		}
	};
	
	$scope.ClearStudentPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newStudent.PhotoData = null;
				$scope.newStudent.Photo_TMP = [];
				$scope.newStudent.PhotoPath = '';
			});

			$scope.webCam.Start = false;
			stream = null;
		});
		$('#imgFatherPhoto').attr('src', '');
		$('#imgMotherPhoto').attr('src', '');
		$('#imgGuardian').attr('src', '');
		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};

	$scope.AddSiblingDetails = function (ind) {
		if ($scope.newStudent.SiblingDetailColl_TMP) {
			if ($scope.newStudent.SiblingDetailColl_TMP.length > ind + 1) {
				$scope.newStudent.SiblingDetailColl_TMP.splice(ind + 1, 0, {
					ClassId: null
				})
			} else {
				$scope.newStudent.SiblingDetailColl_TMP.push({
					ClassId: null
				})
			}
		}
		$timeout(function () {
			var cboInd = (ind + 1);
			$('#cboStudent' + cboInd).select2();
		})

	};
	$scope.delSiblingDetails = function (ind) {
		if ($scope.newStudent.SiblingDetailColl_TMP) {
			if ($scope.newStudent.SiblingDetailColl_TMP.length > 1) {
				$scope.newStudent.SiblingDetailColl_TMP.splice(ind, 1);
			}
		}
	};

	$scope.ClearCitizenFrontPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newStudent.CitizenFrontPhotoData = null;
				$scope.newStudent.CitizenFrontPhoto_TMP = [];
			});

		});

		$('#imgCitizenFront').attr('src', '');
		$('#imgCitizenFront').attr('src', '');
	};


	$scope.ClearCitizenBackPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newStudent.CitizenBackPhotoData = null;
				$scope.newStudent.CitizenBackPhoto_TMP = [];
			});

		});

		$('#imgCitizenBack').attr('src', '');
		$('#imgCitizenBack').attr('src', '');
	};

	$scope.ClearNIDPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newStudent.NIDPhotoData = null;
				$scope.newStudent.NIDPhoto_TMP = [];
			});

		});

		$('#imgNID').attr('src', '');
		$('#imgNID').attr('src', '');
	};

	$scope.ClearSignaturePhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newStudent.SignatureData = null;
				$scope.newStudent.Signature_TMP = [];
			});

		});

		$('#imgSignature').attr('src', '');
		$('#imgSignature1').attr('src', '');
	};

	$scope.ClearStudent = function () {

		$scope.ClearStudentOnly();

		$timeout(function () {
			$('#cboStudent0').select2();
		});

		$timeout(function () {
			$scope.GetAutoNumber();
		});

		AdmissionEnquiryId = null;

	}

	$scope.ClearStudentOnly = function () {
		$scope.ClearStudentPhoto();
		$scope.ClearSignaturePhoto();
		$scope.ClearCitizenFrontPhoto();
		$scope.ClearCitizenBackPhoto();
		$scope.ClearNIDPhoto();

		$timeout(function () {
			$scope.newStudent = {
				StudentId: null,
				RegNo: '',
				AdmitDate_TMP: new Date(),
				FirstName: '',
				MiddleName: '',
				LastName: '',
				Gender: 1,
				ContactNo: '',
				IsPhysicalDisability: false,
				isEDJ: false,
				Photo: null,
				PhotoPath: null,
				Signature: null,
				SignaturePath: null,
				ClassId: null,
				SectionId: null,
				RollNo: 0,
				IfGurandianIs: 3,
				IsSameAsPermanentAddress: false,
				AcademicDetailsColl: [],
				AttachmentColl: [],
				SiblingDetailColl_TMP: [],
				LedgerPanaNo: '',
				IsNewStudent: true,
				F_AnnualIncome: 0,
				M_AnnualIncome: 0,
				EnquiryNo: '',
				ClassId_First: null,
				AdmissionEnquiryId: null,
				PaidUptoMonth: null,

				PassOutClassId: null,
				PassOutSymbolNo: '',
				PassOutGPA: null,
				PassOutGrade: '',
				PassOutRemarks: '',
				EnrollNo: 0,
				CardNo: 0,
				FamilyType: 0,
				PA_WardNo: 0,
				CA_WardNo: 0,
				PA_ProvinceId: null,
				PA_DistrictId: null,
				PA_LocalLevelId: null,
				CA_ProvinceId: null,
				CA_DistrictId: null,
				CA_LocalLevelId: null,
				Mode: 'Save'
			};

			$scope.newStudent.AcademicDetailsColl.push({});
			$scope.newStudent.SiblingDetailColl_TMP.push({});

		});

		AdmissionEnquiryId = null;
	}

	$scope.GetEnquiryById = function () {

		if (AdmissionEnquiryId && AdmissionEnquiryId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				TranId: AdmissionEnquiryId
			};

			$http({
				method: 'POST',
				url: base_url + "FrontDesk/Transaction/GetEnquiryById",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var enq = res.data.Data;
					$scope.newStudent.ReferralCode = enq.ReferralCode;
					$scope.newStudent.EnquiryNo = enq.AutoNumber.toString();
					$scope.newStudent.RegNo = enq.AutoNumber.toString();
					$scope.newStudent.AdmissionEnquiryId = AdmissionEnquiryId;
					$scope.newStudent.FirstName = enq.FirstName;
					$scope.newStudent.MiddleName = enq.MiddleName;
					$scope.newStudent.LastName = enq.LastName;
					$scope.newStudent.Gender = enq.Gender;
					$scope.newStudent.CasteId = enq.CasteId;
					$scope.newStudent.PA_FullAddress = enq.Address;
					$scope.newStudent.ContactNo = enq.ContactNo;
					/*$scope.newStudent.DOB_TMP = new Date(enq.DOB);*/
					$scope.newStudent.Email = enq.Email;
					$scope.newStudent.IsPhysicalDisability = enq.IsPhysicalDisability;
					$scope.newStudent.PhysicalDisability = enq.PhysicalDisability;

					/*if (enq.DOB)*/

						if (enq.DOB)
							$scope.newStudent.DOB_TMP = new Date(enq.DOB);

						$scope.newStudent.Religion = enq.Religion;
					$scope.newStudent.Nationality = enq.Nationality;
					$scope.newStudent.ClassId = enq.ClassId;
					$scope.newStudent.MediumId = enq.MediumId;
					$scope.newStudent.ClassShiftId = enq.ClassShiftId;
					$scope.newStudent.PhotoPath = enq.PhotoPath;
					$scope.newStudent.FatherName = enq.FatherName;
					$scope.newStudent.F_Profession = enq.F_Profession;
					$scope.newStudent.F_ContactNo = enq.F_ContactNo;
					$scope.newStudent.F_Email = enq.F_Email;
					$scope.newStudent.MotherName = enq.MotherName;
					$scope.newStudent.M_Profession = enq.M_Profession;
					$scope.newStudent.M_ContactNo = enq.M_ContactNo;
					$scope.newStudent.M_Email = enq.M_Email;
					$scope.newStudent.GuardianName = enq.GuardianName;
					$scope.newStudent.G_Relation = enq.G_Relation;
					$scope.newStudent.G_Professsion = enq.G_Professsion;
					$scope.newStudent.G_Contact = enq.G_Contact;
					$scope.newStudent.G_Email = enq.G_Email;
					$scope.newStudent.G_Address = enq.G_Address;

					if (!enq.AcademicDetailsColl || enq.AcademicDetailsColl.length == 0) {
						enq.AcademicDetailsColl = [];
						enq.AcademicDetailsColl.push({});
					}

					$scope.newStudent.AcademicDetailsColl = enq.AcademicDetailsColl;
					$scope.newStudent.AttachmentColl = enq.AttachmentColl;

					$scope.GetAutoNumber();
					$scope.GetAutoRollNo();

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	};

	$scope.GetRegistrationById = function () {

		if (RegistrationId && RegistrationId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				StudentId: RegistrationId
			};

			$http({
				method: 'POST',
				url: base_url + "AdmissionManagement/Creation/GetRegistrationById",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var enq = res.data.Data;
					$scope.newStudent = enq;
					$scope.newStudent.RegistrationId = RegistrationId;
					$scope.newStudent.EnquiryNo = enq.RegNo;
					$scope.newStudent.StudentId = null;

					if ($scope.newStudent.AdmitDate)
						$scope.newStudent.AdmitDate_TMP = new Date($scope.newStudent.AdmitDate);

					if ($scope.newStudent.DOB_AD)
						$scope.newStudent.DOB_TMP = new Date($scope.newStudent.DOB_AD);

					$scope.newStudent.SiblingDetailColl_TMP = [];

					var csQuery = mx($scope.ClassSectionList.SectionList);

					angular.forEach($scope.newStudent.SiblingDetailColl, function (sl) {
						if (sl.ForStudentId || sl.ForStudentId > 0) {
							var findCS = csQuery.firstOrDefault(p1 => p1.ClassId == sl.ClassId && p1.SectionId == sl.SectionId);

							if (!findCS)
								findCS = csQuery.firstOrDefault(p1 => p1.ClassId == sl.ClassId);

							sl.SelectedClassSection = findCS;
							$timeout(function () {
								$scope.GetStudentForSibling(sl, 0);
							});
							$timeout(function () {
								$scope.newStudent.SiblingDetailColl_TMP.push(sl);
							});

						}

					});

					$timeout(function () {
						if ($scope.newStudent.SiblingDetailColl_TMP.length == 0)
							$scope.newStudent.SiblingDetailColl_TMP.push({});
					});

					$scope.GetAutoNumber();
					$scope.GetAutoRollNo();

					$scope.newStudent.Mode = 'Save';

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	};

	$scope.IsValidStudent = function () {

		if ($scope.newStudent.RegNo.isEmpty()) {
			Swal.fire('Please ! Enter ' + $scope.Labels.RegdNo);
			return false;
		}

		if ($scope.newStudent.FirstName.isEmpty()) {
			Swal.fire('Please ! Enter First Name');
			return false;
		}

		if ($scope.CompanyConfig.Country == 'Nepal') {
			if (!$scope.newStudent.LastName || $scope.newStudent.LastName.trim() === '') {
				Swal.fire('Please ! Enter Last Name');
				return false;
			}
		}

		if (!$scope.newStudent.Gender) {
			Swal.fire('Please ! Select Gender');
			return false;
		}

		if (!$scope.newStudent.PA_FullAddress || $scope.newStudent.PA_FullAddress.trim() === '') {
			Swal.fire('Please ! Enter Permanent Full Address');
			return false;
		}

		//if (!$scope.newStudent.DOBBS_TMP && !$scope.newStudent.DOB_TMP) {
		//	Swal.fire('Please ! Select Date of Birth');
		//	return false;
		//}

		// Age must be valid
		//if ($scope.newStudent.DOBBSDet.age && $scope.newStudent.DOBBSDet.age <= 0) {
		//	Swal.fire('Invalid Date of Birth');
		//	return false;
		//}

		if ($scope.newStudent.AcademicDetailsColl && $scope.newStudent.AcademicDetailsColl.length > 0) {
			for (var i = 0; i < $scope.newStudent.AcademicDetailsColl.length; i++) {
				var acDet = $scope.newStudent.AcademicDetailsColl[i];

				// Check if row has any data EXCEPT Class & School
				var hasOtherData =
					(acDet.Level || acDet.University || acDet.Exam || acDet.PassoutYear ||
						acDet.SymbolNo || acDet.ObtainMarks || acDet.ObtainPer ||
						acDet.Division || acDet.GPA);

				if (hasOtherData) {
					if (!acDet.ClassName || acDet.ClassName.trim() === '') {
						Swal.fire('Please enter Class for row ' + (i + 1));
						return false;
					}

					if (!acDet.SchoolColledge || acDet.SchoolColledge.trim() === '') {
						Swal.fire('Please enter School/College for row ' + (i + 1));
						return false;
					}
				}
			}
		}

		

		if ($scope.SelectedClass && $scope.SelectedClass.FacultyId > 0) {
			if ($scope.AcademicConfig.ActiveBatch == true) {

				if ($scope.newStudent.BatchId > 0) { }
				else {
					Swal.fire('Please ! Select Batch');
					return false;
				}
			}


			if ($scope.AcademicConfig.ActiveSemester == true) {

				if ($scope.AcademicConfig.ActiveClassYear == true) {
					if ($scope.newStudent.SemesterId > 0 || $scope.newStudent.ClassYearId > 0) { }
					else {
						Swal.fire('Please ! Select Semester/ClassYear');
						return false;
					}
				}
				else {

					if ($scope.newStudent.SemesterId > 0) { }
					else {
						Swal.fire('Please ! Select Semester');
						return false;
					}
				}

			}

			if ($scope.AcademicConfig.ActiveClassYear == true) {

				if ($scope.AcademicConfig.ActiveSemester == true) {
					if ($scope.newStudent.SemesterId > 0 || $scope.newStudent.ClassYearId > 0) { }
					else {
						Swal.fire('Please ! Select Semester/ClassYear');
						return false;
					}
				}
				else {

					if ($scope.newStudent.ClassYearId > 0) { }
					else {
						Swal.fire('Please ! Select Class Year');
						return false;
					}
				}

			}
		}

		function validateFiles(fileCollection, label) {
			const allowedTypes = ['image/jpeg', 'image/jpg', 'image/png']; 
			if (fileCollection && fileCollection.length > 0) {
				for (var i = 0; i < fileCollection.length; i++) {
					var file = fileCollection[i];
					if (file && file.size > 1048576) { 
						Swal.fire({
							icon: 'warning',
							title: label + ' Too Large',
							text: 'Each ' + label + ' file must be 1 MB or smaller.'
						});
						return false;
					}

					// Check file type
					if (file && !allowedTypes.includes(file.type)) {
						Swal.fire({
							icon: 'warning',
							title: 'Invalid File Type',
							text: 'Only JPG, JPEG, and PNG files are allowed for ' + label + '.'
						});
						return false;
					}
				}
			}
			return true;
		}
		if (!validateFiles($scope.newStudent.Photo_TMP, 'Photo')) return false;
		if (!validateFiles($scope.newStudent.Signature_TMP, 'Signature')) return false;
		if (!validateFiles($scope.newStudent.FatherPhoto_TMP, 'Father Photo')) return false;
		if (!validateFiles($scope.newStudent.MotherPhoto_TMP, 'Mother Photo')) return false;
		if (!validateFiles($scope.newStudent.GuardianPhoto_TMP, 'Guardian Photo')) return false;
		if (!validateFiles($scope.newStudent.CitizenFrontPhoto_TMP, 'Citizen Front Photo')) return false;
		if (!validateFiles($scope.newStudent.CitizenBackPhoto_TMP, 'Citizen Back Photo')) return false;
		if (!validateFiles($scope.newStudent.NIDPhoto_TMP, 'NID Photo')) return false;

		return true;
	}

	$scope.SaveUpdateStudent = function () {
		if ($scope.IsValidStudent() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudent.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudent();
					}
				});
			} else
				$scope.CallSaveUpdateStudent();

		}
	};

	var BASE64_MARKER = ';base64,';
	// Does the given URL (string) look like a base64-encoded URL?
	function isDataURI(url) {
		return url.split(BASE64_MARKER).length === 2;
	};
	function dataURItoFile(dataURI) {
		if (!isDataURI(dataURI)) { return false; }

		// Format of a base64-encoded URL:
		// data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAYAAAAEOCAIAAAAPH1dAAAAK
		var mime = dataURI.split(BASE64_MARKER)[0].split(':')[1];
		var filename = 'rc-' + (new Date()).getTime() + '.' + mime.split('/')[1];
		//var filename = 'dataURI-file-' + (new Date()).getTime() + '.jpeg';
		var bytes = atob(dataURI.split(BASE64_MARKER)[1]);
		var writer = new Uint8Array(new ArrayBuffer(bytes.length));

		for (var i = 0; i < bytes.length; i++) {
			writer[i] = bytes.charCodeAt(i);
		}

		return new File([writer.buffer], filename, { type: mime });
	}

	$scope.CallSaveUpdateStudent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var refStudent = false;

		if ($scope.newStudent.RegistrationId && $scope.newStudent.RegistrationId > 0)
			refStudent = true;
		else if ($scope.newStudent.AdmissionEnquiryId && $scope.newStudent.AdmissionEnquiryId > 0)
			refStudent = true;

		var filesColl = $scope.newStudent.AttachmentColl;
		//$scope.newStudent.AttachmentColl = [];

		var photo = $scope.newStudent.Photo_TMP;
		var signature = $scope.newStudent.Signature_TMP;

		var fatherphoto = $scope.newStudent.FatherPhoto_TMP;
		var motherphoto = $scope.newStudent.MotherPhoto_TMP;
		var guardianphoto = $scope.newStudent.GuardianPhoto_TMP;


		//Add js to save photo by Prashant
		var citizenshipFrontphoto = $scope.newStudent.CitizenFrontPhoto_TMP;
		var citizenshipBackphoto = $scope.newStudent.CitizenBackPhoto_TMP;
		var nationalIdPhoto = $scope.newStudent.NIDPhoto_TMP;


		if ($scope.newStudent.PhotoData && (!photo || photo.length == 0)) {
			photo = [];

			photo.push(dataURItoFile($scope.newStudent.PhotoData));
		}

		if ($scope.newStudent.AdmitDateDet) {
			$scope.newStudent.AdmitDate = $filter('date')(new Date($scope.newStudent.AdmitDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newStudent.AdmitDate = new Date();

		if ($scope.newStudent.DOBADDet && $scope.newStudent.DOBADDet.dateAD) {
			$scope.newStudent.DOB_AD = $filter('date')(new Date($scope.newStudent.DOBADDet.dateAD), 'yyyy-MM-dd');
		}

		if ($scope.newStudent.DOBADDet1 && $scope.newStudent.DOBADDet1.dateAD) {
			$scope.newStudent.DOB_AD = $filter('date')(new Date($scope.newStudent.DOBADDet1.dateAD), 'yyyy-MM-dd');
		}

		$scope.newStudent.SiblingDetailColl = [];
		angular.forEach($scope.newStudent.SiblingDetailColl_TMP, function (sl) {
			if ((sl.ForStudentId || sl.ForStudentId > 0) && sl.SelectedClassSection) {
				if (sl.Relation) {
					var newSB = {
						ForStudentId: sl.ForStudentId,
						ClassId: sl.SelectedClassSection.ClassId,
						SectionId: sl.SelectedClassSection.SectionId,
						Relation: sl.Relation,
						Remarks: sl.Remarks
					}
					$scope.newStudent.SiblingDetailColl.push(newSB);
				}
			}
		});

		//added by Suresh on Chaitra 10 starts
		var selectData1 = $('#cboProvincePA').select2('data');
		if (selectData1 && selectData1.length > 0)
			province1 = selectData1[0].text.trim();

		selectData1 = $('#cboDistrictPA').select2('data');
		if (selectData1 && selectData1.length > 0)
			district1 = selectData1[0].text.trim();


		selectData1 = $('#cboAreaPA').select2('data');
		if (selectData1 && selectData1.length > 0)
			area1 = selectData1[0].text.trim();


		var selectData2 = $('#cboProvinceCA').select2('data');
		if (selectData2 && selectData2.length > 0)
			province2 = selectData2[0].text.trim();

		selectData2 = $('#cboDistrictCA').select2('data');
		if (selectData2 && selectData2.length > 0)
			district2 = selectData2[0].text.trim();


		selectData2 = $('#cboAreaCA').select2('data');
		if (selectData2 && selectData2.length > 0)
			area2 = selectData2[0].text.trim();

		$scope.newStudent.PA_Province = province1;
		$scope.newStudent.PA_District = district1;
		$scope.newStudent.PA_LocalLevel = area1;
		$scope.newStudent.CA_Province = province2;
		$scope.newStudent.CA_District = district2;
		$scope.newStudent.CA_LocalLevel = area2;
		//Ends

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveStudent",
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

				if (data.fphoto && data.fphoto.length > 0)
					formData.append("father", data.fphoto[0]);

				if (data.mphoto && data.mphoto.length > 0)
					formData.append("mother", data.mphoto[0]);

				if (data.gphto && data.gphto.length > 0)
					formData.append("guardian", data.gphto[0]);

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
				jsonData: $scope.newStudent, files: filesColl, stPhoto: photo, stSignature: signature, fphoto: fatherphoto, mphoto: motherphoto, gphto: guardianphoto,
				cfrontphto: citizenshipFrontphoto, cbackphto: citizenshipBackphoto, nidphto: nationalIdPhoto
			}
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			var billPrint = ($scope.newStudent.PaidUptoMonth && $scope.newStudent.PaidUptoMonth > 0) ? true : false;
			var mid = $scope.newStudent.PaidUptoMonth;
			if (res.data.IsSuccess == true) {
				$scope.ClearStudent();
				$scope.Print(res.data.Data.RId);

				if (billPrint) {
					$scope.PrintBill(res.data.Data.RId, mid);
				}

				if (refStudent == true)
					refreshActiveTab();

			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}
	$scope.Print = function (tranId) {
		if ((tranId || tranId > 0)) {
			var StudentId = tranId;

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
												document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=0&StudentId=" + StudentId + "&vouchertype=0";
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
							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=0&StudentId=" + StudentId + "&vouchertype=0";
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

	$scope.PrintBill = function (studentId, monthId) {

		var billEntityId = 368;
		if (studentId > 0 && monthId > 0) {
			var StudentId = tranId;

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + billEntityId + "&voucherId=0&isTran=true",
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
												var rptPara = {
													rpttranid: rptTranId,
													istransaction: false,
													entityid: billEntityId,
													StudentId: studentId,
													ClassId: 0,
													SectionId: 0,
													FromMonthId: monthId,
													ToMonthId: monthId,
													FromBillNo: 0,
													ToBillNo: 0
												};
												var paraQuery = param(rptPara);
												document.body.style.cursor = 'wait';
												document.getElementById("frmRpt").src = '';
												document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
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

							var rptPara = {
								rpttranid: rptTranId,
								istransaction: false,
								entityid: billEntityId,
								StudentId: studentId,
								ClassId: 0,
								SectionId: 0,
								FromMonthId: monthId,
								ToMonthId: monthId,
								FromBillNo: 0,
								ToBillNo: 0
							};
							var paraQuery = param(rptPara);
							document.body.style.cursor = 'wait';
							document.getElementById("frmRpt").src = '';
							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
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

	$scope.GetAutoNumber = function () {

		$timeout(function () {

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetStudentAutoNumber",
				dataType: "json",
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				var st = res.data.Data;
				if (st.IsSuccess == true) {
					$scope.newStudent.RegNo = st.ResponseId;
					$scope.newStudent.AutoNumber = st.RId;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});


	};
	$scope.GetAutoRollNo = function () {

		$scope.SelectedClass = {};
		$timeout(function () {

			//$scope.newStudent.SemesterId = null;
			//$scope.newStudent.ClassYearId = null;

			var para = {
				ClassId: $scope.newStudent.ClassId,
				SectionId: $scope.newStudent.SectionId,
				BatchId: $scope.newStudent.BatchId,
				SemesterId: $scope.newStudent.SemesterId,
				ClassYearId: $scope.newStudent.ClassYearId
			};
			$scope.SelectedClass = mx($scope.ClassList).firstOrDefault(p1 => p1.ClassId == para.ClassId);

			$scope.MonthList = [];
			GlobalServices.getAcademicMonthList(null, para.ClassId).then(function (resAM) {
				$scope.MonthList = [];
				angular.forEach(resAM.data.Data, function (m) {
					$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
				});
			});

			if ($scope.SelectedClass) {
				var semQry = mx($scope.SelectedClass.ClassSemesterIdColl);
				var cyQry = mx($scope.SelectedClass.ClassYearIdColl);

				$scope.SelectedClassClassYearList = [];
				$scope.SelectedClassSemesterList = [];

				angular.forEach($scope.SemesterList, function (sem) {
					if (semQry.contains(sem.id)) {
						$scope.SelectedClassSemesterList.push({
							id: sem.id,
							text: sem.text,
							SemesterId: sem.id,
							Name: sem.Name
						});
					}
				});

				angular.forEach($scope.ClassYearList, function (sem) {
					if (cyQry.contains(sem.id)) {
						$scope.SelectedClassClassYearList.push({
							id: sem.id,
							text: sem.text,
							ClassYearId: sem.id,
							Name: sem.Name
						});
					}
				});

			}

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetStudentAutoRollNo",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				var st = res.data.Data;
				if (st.IsSuccess == true) {
					$scope.newStudent.RollNo = st.RId;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});


	};

	$scope.GetStudentById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			StudentId: refData.StudentId
		};
		$scope.ClearStudentOnly();

		$timeout(function () {
			$scope.newStudent.Gender = 0;
		});

		$timeout(function () {
			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetStudentById",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {



					$timeout(function () {
						$scope.newStudent = res.data.Data;

						$scope.SelectedClass = mx($scope.ClassList).firstOrDefault(p1 => p1.ClassId == $scope.newStudent.ClassId);

						if ($scope.SelectedClass) {
							var semQry = mx($scope.SelectedClass.ClassSemesterIdColl);
							var cyQry = mx($scope.SelectedClass.ClassYearIdColl);

							$scope.SelectedClassClassYearList = [];
							$scope.SelectedClassSemesterList = [];

							angular.forEach($scope.SemesterList, function (sem) {
								if (semQry.contains(sem.id)) {
									$scope.SelectedClassSemesterList.push({
										id: sem.id,
										text: sem.text,
										SemesterId: sem.id,
										Name: sem.Name
									});
								}
							});

							angular.forEach($scope.ClassYearList, function (sem) {
								if (cyQry.contains(sem.id)) {
									$scope.SelectedClassClassYearList.push({
										id: sem.id,
										text: sem.text,
										ClassYearId: sem.id,
										Name: sem.Name
									});
								}
							});

						}

						if (!$scope.newStudent.AcademicDetailsColl || $scope.newStudent.AcademicDetailsColl.length == 0)
							$scope.AddAcademicDetails(0);

						if ($scope.newStudent.AdmitDate)
							$scope.newStudent.AdmitDate_TMP = new Date($scope.newStudent.AdmitDate);

						if ($scope.newStudent.DOB_AD) {
							$scope.newStudent.DOBBS_TMP = new Date($scope.newStudent.DOB_AD);
							$scope.newStudent.DOBAD_TMP = new Date($scope.newStudent.DOB_AD);
							$scope.newStudent.DOBAD_TMP1 = new Date($scope.newStudent.DOB_AD);
						}


						$scope.newStudent.SiblingDetailColl_TMP = [];

						var csQuery = mx($scope.ClassSectionList.SectionList);

						angular.forEach($scope.newStudent.SiblingDetailColl, function (sl) {
							if (sl.ForStudentId || sl.ForStudentId > 0) {
								var findCS = csQuery.firstOrDefault(p1 => p1.ClassId == sl.ClassId && p1.SectionId == sl.SectionId);

								if (!findCS)
									findCS = csQuery.firstOrDefault(p1 => p1.ClassId == sl.ClassId);

								sl.SelectedClassSection = findCS;
								$timeout(function () {
									$scope.GetStudentForSibling(sl, 0);
								});
								$timeout(function () {
									$scope.newStudent.SiblingDetailColl_TMP.push(sl);
								});

							}

						});


						//Addded By Suresh on Chaita 13 2081
						var findProvince = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.PA_Province);

						if (findProvince)
							$scope.newStudent.PA_ProvinceId = findProvince.id;
						else
							$scope.newStudent.PA_ProvinceId = null;

						var findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.PA_District);
						if (findDistrict)
							$scope.newStudent.PA_DistrictId = findDistrict.id;
						else
							$scope.newStudent.PA_DistrictId = null;

						var findArea = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.PA_LocalLevel);
						if (findArea)
							$scope.newStudent.PA_LocalLevelId = findArea.id;
						else
							$scope.newStudent.PA_LocalLevelId = null;

						//Current Address
						var findProvince = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.CA_Province);

						if (findProvince)
							$scope.newStudent.CA_ProvinceId = findProvince.id;
						else
							$scope.newStudent.CA_ProvinceId = null;

						var findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.CA_District);
						if (findDistrict)
							$scope.newStudent.CA_DistrictId = findDistrict.id;
						else
							$scope.newStudent.CA_DistrictId = null;

						var findArea = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.CA_LocalLevel);
						if (findArea)
							$scope.newStudent.CA_LocalLevelId = findArea.id;
						else
							$scope.newStudent.CA_LocalLevelId = null;

						//Ends
						$timeout(function () {
							if ($scope.newStudent.SiblingDetailColl_TMP.length == 0)
								$scope.newStudent.SiblingDetailColl_TMP.push({});
						});


						$scope.newStudent.Mode = 'Modify';
						$('.nav-tabs a:first').tab('show');
					});


				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});

	};
	$scope.DelStudentById = function (refData) {

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
					StudentId: refData.StudentId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DelStudent",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetClassWiseStudentList(1);
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.LoadClassWiseSemesterYear = function (classId, data) {

		$scope.SelectedClassClassYearList = [];
		$scope.SelectedClassSemesterList = [];
		$scope.SelectedClass1 = mx($scope.ClassList).firstOrDefault(p1 => p1.ClassId == classId);

		if ($scope.SelectedClass1) {
			var semQry = mx($scope.SelectedClass1.ClassSemesterIdColl);
			var cyQry = mx($scope.SelectedClass1.ClassYearIdColl);

			angular.forEach($scope.SemesterList, function (sem) {
				if (semQry.contains(sem.id)) {
					$scope.SelectedClassSemesterList.push({
						id: sem.id,
						text: sem.text,
						SemesterId: sem.id,
						Name: sem.Name
					});
				}
			});

			angular.forEach($scope.ClassYearList, function (sem) {
				if (cyQry.contains(sem.id)) {
					$scope.SelectedClassClassYearList.push({
						id: sem.id,
						text: sem.text,
						ClassYearId: sem.id,
						Name: sem.Name
					});
				}
			});
		}

	};

	$scope.DefaultPhoto = '/wwwroot/dynamic/images/avatar-img.jpg';
	$scope.GetClassWiseStudentList = function (fromInd) {

		if ($scope.viewStudent.ClassId == null || $scope.viewStudent.ClassId == undefined)
			return;

		// Load Class Wise Year and Semester On Class Selection Changed
		if (fromInd == 2) {
			$scope.viewStudent.SemesterId = null;
			$scope.viewStudent.ClassYearId = null;
			$scope.LoadClassWiseSemesterYear($scope.viewStudent.ClassId, $scope.viewStudent);
		}

		$scope.viewStudent.StudentList = [];

		var para = {
			ClassId: $scope.viewStudent.ClassId,
			SectionIdColl: ($scope.viewStudent.SectionId && $scope.viewStudent.SectionId.length > 0 ? $scope.viewStudent.SectionId.toString() : ''),
			FilterSection: true,
			SemesterId: $scope.viewStudent.SemesterId,
			ClassYearId: $scope.viewStudent.ClassYearId,
			BatchId: $scope.viewStudent.BatchId,
			BranchId:$scope.viewStudent.BranchId,
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetClassWiseStudentList",
			dataSchedule: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.viewStudent.StudentList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.exportToExcel = function (tableId) { // ex: '#my-table'
		var exportHref = Excel.tableToExcel(tableId, 'ClassWiseStudent');
		$timeout(function () { location.href = exportHref; }, 100); // trigger download
	}

	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa
	}

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

	$scope.CurSiblingDetailsColl = [];
	$scope.CurStudentAcademicColl = [];
	$scope.CurParentStudent = {};
	$scope.ShowSibling = function (st) {
		$scope.CurParentStudent = st;
		var para = { StudentId: st.StudentId };

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetStudentSiblingDetails",
			dataSchedule: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				// access both lists from Data
				$scope.CurSiblingDetailsColl = res.data.Data.SiblingDetails;
				$scope.CurStudentAcademicColl = res.data.Data.AcademicDetails;

				$('#modal-sibling-info').modal('show');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed ' + reason);
		});
	};

	$scope.UploadStudentPhoto = function (st) {


		var photo = st.Photo_TMP;
		var beData = {
			StudentId: st.StudentId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/UpdateStuentPhoto",
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
});



//{ type: 'json', escape: 'false' }
//{ type: 'json', escape: 'false', ignoreColumn: '[2,3]' }
//{ type: 'json', escape: 'true' }
//{ type: 'xml', escape: 'false' }
//{ type: 'sql' }
//{ type: 'csv', escape: 'false' }
//{ type: 'txt', escape: 'false' }
//{ type: 'excel', escape: 'false' }
//{ type: 'doc', escape: 'false' }
//{ type: 'powerpoint', escape: 'false' }
//{ type: 'png', escape: 'false' }
//{ type: 'pdf', pdfFontSize: '7', escape: 'false' }