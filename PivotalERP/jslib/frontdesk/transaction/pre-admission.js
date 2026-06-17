app.controller('PreadmissionController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Preadmission';


	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.GenderColl = GlobalServices.getGenderList();

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
		$scope.DisablityList = GlobalServices.getDisablityList();



		$scope.currentPages = {
			HouseName: 1,
			HouseDress: 1,
			PreadmissionType: 1,
			Medium: 1
		};

		$scope.searchData = {
			HouseName: '',
			HouseDress: '',
			PreadmissionType: '',
			Medium: ''
		};

		$scope.perPage = {
			HouseName: GlobalServices.getPerPageRow(),
			HouseDress: GlobalServices.getPerPageRow(),
			PreadmissionType: GlobalServices.getPerPageRow(),
			Medium: GlobalServices.getPerPageRow()
		};

		$scope.newPreadmission = {
			PreadmissionId: null,
			RegNo: '',
			AdmitDate: new Date(),
			FirstName: '',
			MiddleName: '',
			LastName: '',
			Gender: 1,
			ContactNo: '',
			IsPhysicalDisability: false,
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
			Mode: 'Save'
		};

		$scope.newPreadmission.AcademicDetailsColl.push({});


		//$scope.GetAllHouseNameList();


	}

	$scope.LoadGuradianDet = function () {

		if ($scope.newPreadmission.IfGurandianIs == 1) {
			$scope.newPreadmission.G_Name = $scope.newPreadmission.FatherName;
			$scope.newPreadmission.G_Relation = 'Father';
			$scope.newPreadmission.G_Profession = $scope.newPreadmission.F_Profession;
			$scope.newPreadmission.G_ContactNo = $scope.newPreadmission.F_ContactNo;
			$scope.newPreadmission.G_Email = $scope.newPreadmission.F_Email;


		} else if ($scope.newPreadmission.IfGurandianIs == 2) {
			$scope.newPreadmission.G_Name = $scope.newPreadmission.MotherName;
			$scope.newPreadmission.G_Relation = 'Mother';
			$scope.newPreadmission.G_Profession = $scope.newPreadmission.M_Profession;
			$scope.newPreadmission.G_ContactNo = $scope.newPreadmission.M_Contact;
			$scope.newPreadmission.G_Email = $scope.newPreadmission.M_Email;

		} else if ($scope.newPreadmission.IfGurandianIs == 3) {
			$scope.newPreadmission.G_Name = '';
			$scope.newPreadmission.G_Relation = '';
			$scope.newPreadmission.G_Profession = '';
			$scope.newPreadmission.G_ContactNo = '';
			$scope.newPreadmission.G_Email = '';
		}
	};

	$scope.SamePAddress = function () {

		if ($scope.newPreadmission.IsSameAsPermanentAddress == true) {
			$scope.newPreadmission.CA_FullAddress = $scope.newPreadmission.PA_FullAddress;
			$scope.newPreadmission.CA_Province = $scope.newPreadmission.PA_Province;
			$scope.newPreadmission.CA_District = $scope.newPreadmission.PA_District;
			$scope.newPreadmission.CA_LocalLevel = $scope.newPreadmission.PA_LocalLevel;
			$scope.newPreadmission.CA_WardNo = $scope.newPreadmission.PA_WardNo;
			$scope.newPreadmission.CA_StreetName = $scope.newPreadmission.PA_StreetName;
		} else {
			$scope.newPreadmission.CA_FullAddress = '';
			$scope.newPreadmission.CA_Province = '';
			$scope.newPreadmission.CA_District = '';
			$scope.newPreadmission.CA_LocalLevel = '';
			$scope.newPreadmission.CA_WardNo = '';
			$scope.newPreadmission.CA_StreetName = '';
		}
	}

	$scope.AddAcademicDetails = function (ind) {
		if ($scope.newPreadmission.AcademicDetailsColl) {
			if ($scope.newPreadmission.AcademicDetailsColl.length > ind + 1) {
				$scope.newPreadmission.AcademicDetailsColl.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newPreadmission.AcademicDetailsColl.push({
					ClassName: ''
				})
			}
		}
	};
	$scope.delAcademicDetails = function (ind) {
		if ($scope.newPreadmission.AcademicDetailsColl) {
			if ($scope.newPreadmission.AcademicDetailsColl.length > 1) {
				$scope.newPreadmission.AcademicDetailsColl.splice(ind, 1);
			}
		}
	};
	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newPreadmission.AttachmentColl) {
			if ($scope.newPreadmission.AttachmentColl.length > 0) {
				$scope.newPreadmission.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newPreadmission.AttachmentColl.push({
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
	$scope.ClearPreadmissionPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newPreadmission.PhotoData = null;
				$scope.newPreadmission.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};

	$scope.ClearSignaturePhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newPreadmission.SignatureData = null;
				$scope.newPreadmission.Signature_TMP = [];
			});

		});

		$('#imgSignature').attr('src', '');
		$('#imgSignature1').attr('src', '');
	};
	$scope.ClearPreadmission = function () {
		$scope.newPreadmission = {
			PreadmissionId: null,
			RegNo: '',
			AdmitDate: new Date(),
			FirstName: '',
			MiddleName: '',
			LastName: '',
			Gender: 1,
			ContactNo: '',
			IsPhysicalDisability: false,
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
			Mode: 'Save'
		};
		$scope.newPreadmission.AcademicDetailsColl.push({});
	}
	


	$scope.IsValidPreadmission = function () {
		if ($scope.newPreadmission.Name.isEmpty()) {
			Swal.fire('Please ! Enter House Name');
			return false;
		}

		return true;
	}



	$scope.AddAcademicDetails = function (ind) {
		if ($scope.newPreadmission.AcademicDetailsColl) {
			if ($scope.newPreadmission.AcademicDetailsColl.length > ind + 1) {
				$scope.newPreadmission.AcademicDetailsColl.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newPreadmission.AcademicDetailsColl.push({
					ClassName: ''
				})
			}
		}
	};
	$scope.delAcademicDetails = function (ind) {
		if ($scope.newPreadmission.AcademicDetailsColl) {
			if ($scope.newPreadmission.AcademicDetailsColl.length > 1) {
				$scope.newPreadmission.AcademicDetailsColl.splice(ind, 1);
			}
		}
	};
	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newPreadmission.AttachmentColl) {
			if ($scope.newPreadmission.AttachmentColl.length > 0) {
				$scope.newPreadmission.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newPreadmission.AttachmentColl.push({
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


	$scope.LoadGuradianDet = function () {

		if ($scope.newPreadmission.IfGurandianIs == 1) {
			$scope.newPreadmission.G_Name = $scope.newPreadmission.FatherName;
			$scope.newPreadmission.G_Relation = 'Father';
			$scope.newPreadmission.G_Profession = $scope.newPreadmission.F_Profession;
			$scope.newPreadmission.G_ContactNo = $scope.newPreadmission.F_ContactNo;
			$scope.newPreadmission.G_Email = $scope.newPreadmission.F_Email;


		} else if ($scope.newPreadmission.IfGurandianIs == 2) {
			$scope.newPreadmission.G_Name = $scope.newPreadmission.MotherName;
			$scope.newPreadmission.G_Relation = 'Mother';
			$scope.newPreadmission.G_Profession = $scope.newPreadmission.M_Profession;
			$scope.newPreadmission.G_ContactNo = $scope.newPreadmission.M_Contact;
			$scope.newPreadmission.G_Email = $scope.newPreadmission.M_Email;

		} else if ($scope.newPreadmission.IfGurandianIs == 3) {
			$scope.newPreadmission.G_Name = '';
			$scope.newPreadmission.G_Relation = '';
			$scope.newPreadmission.G_Profession = '';
			$scope.newPreadmission.G_ContactNo = '';
			$scope.newPreadmission.G_Email = '';
		}
	};



	$scope.SaveUpdatePreadmission = function () {
		if ($scope.IsValidPreadmission() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newPreadmission.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdatePreadmission();
					}
				});
			} else
				$scope.CallSaveUpdatePreadmission();

		}
	};

	$scope.CallSaveUpdatePreadmission = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newPreadmission.AttachmentColl;
		//$scope.newPreadmission.AttachmentColl = [];

		var photo = $scope.newPreadmission.Photo_TMP;
		var signature = $scope.newPreadmission.Signature_TMP;

		if ($scope.newPreadmission.AdmitDateDet) {
			$scope.newPreadmission.AdmitDate = $scope.newPreadmission.AdmitDateDet.dateAD;
		} else
			$scope.newPreadmission.AdmitDate = new Date();

		if ($scope.newPreadmission.DOB_ADDet) {
			$scope.newPreadmission.DOB = $scope.newPreadmission.DOB_ADDet.dateAD;
		}

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SavePreadmission",
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
			data: { jsonData: $scope.newPreadmission, files: filesColl, stPhoto: photo, stSignature: signature }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearPreadmission();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}
	$scope.DelHouseNameById = function (refData) {

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
					HouseNameId: refData.HouseNameId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelHouseName",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllHouseNameList();
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