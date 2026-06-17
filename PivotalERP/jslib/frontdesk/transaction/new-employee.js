app.controller('NewEmployeeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'New Employee';

	OnClickDefault();
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

		//$scope.NewEmployeeTypeList = [];
		//GlobalServices.getNewEmployeeTypeList().then(function (res) {
		//	$scope.NewEmployeeTypeList = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		//$scope.BoardList = [];
		//GlobalServices.getBoardList().then(function (res) {
		//	$scope.BoardList = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

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
			NewEmployeeType: 1,
			Medium: 1
		};

		$scope.searchData = {
			HouseName: '',
			HouseDress: '',
			NewEmployeeType: '',
			Medium: ''
		};

		$scope.perPage = {
			HouseName: GlobalServices.getPerPageRow(),
			HouseDress: GlobalServices.getPerPageRow(),
			NewEmployeeType: GlobalServices.getPerPageRow(),
			Medium: GlobalServices.getPerPageRow()
		};

		$scope.newNewEmployee = {
			NewEmployeeId: null,
			ApplicationId: null,
			ApplicationDate_TMP: new Date(),
			FirstName: '',
			MiddleName: '',
			LastName: '',
			Gender: 1,
			DOB_TMP: new Date(),
			ContactNo: '',

			IsPhysicalDisability: false,
			Photo: null,
			PhotoPath: null,
			Email: '',
			FullAddress: '',
			SourceId: null,
			SalaryExpectation: '',
			SubjectId: null,
			JobTitle: '',
			Level:'',			
			Level:'',			
			AcademicDetailsColl: [],
			WorkExperienceColl: [],
			ReferenceColl: [],
			AttachmentColl: [],
			Mode: 'Save'
		};

		$scope.newNewEmployee.AcademicDetailsColl.push({});
		$scope.newNewEmployee.WorkExperienceColl.push({});
		$scope.newNewEmployee.ReferenceColl.push({});



		//$scope.GetAllHouseNameList();


	}



	function OnClickDefault() {
		document.getElementById('enquiry-form').style.display = "none";
		document.getElementById('no-option').style.display = "none";


		document.getElementById('open-form-btn').onclick = function () {
			document.getElementById('table-listing').style.display = "none";
			document.getElementById('enquiry-form').style.display = "block";

		}
		document.getElementById('back-to-list').onclick = function () {
			document.getElementById('table-listing').style.display = "block";

			document.getElementById('enquiry-form').style.display = "none";
		}
	};




	$scope.LoadGuradianDet = function () {

		if ($scope.newNewEmployee.IfGurandianIs == 1) {
			$scope.newNewEmployee.G_Name = $scope.newNewEmployee.FatherName;
			$scope.newNewEmployee.G_Relation = 'Father';
			$scope.newNewEmployee.G_Profession = $scope.newNewEmployee.F_Profession;
			$scope.newNewEmployee.G_ContactNo = $scope.newNewEmployee.F_ContactNo;
			$scope.newNewEmployee.G_Email = $scope.newNewEmployee.F_Email;


		} else if ($scope.newNewEmployee.IfGurandianIs == 2) {
			$scope.newNewEmployee.G_Name = $scope.newNewEmployee.MotherName;
			$scope.newNewEmployee.G_Relation = 'Mother';
			$scope.newNewEmployee.G_Profession = $scope.newNewEmployee.M_Profession;
			$scope.newNewEmployee.G_ContactNo = $scope.newNewEmployee.M_Contact;
			$scope.newNewEmployee.G_Email = $scope.newNewEmployee.M_Email;

		} else if ($scope.newNewEmployee.IfGurandianIs == 3) {
			$scope.newNewEmployee.G_Name = '';
			$scope.newNewEmployee.G_Relation = '';
			$scope.newNewEmployee.G_Profession = '';
			$scope.newNewEmployee.G_ContactNo = '';
			$scope.newNewEmployee.G_Email = '';
		}
	};

	$scope.SamePAddress = function () {

		if ($scope.newNewEmployee.IsSameAsPermanentAddress == true) {
			$scope.newNewEmployee.CA_FullAddress = $scope.newNewEmployee.PA_FullAddress;
			$scope.newNewEmployee.CA_Province = $scope.newNewEmployee.PA_Province;
			$scope.newNewEmployee.CA_District = $scope.newNewEmployee.PA_District;
			$scope.newNewEmployee.CA_LocalLevel = $scope.newNewEmployee.PA_LocalLevel;
			$scope.newNewEmployee.CA_WardNo = $scope.newNewEmployee.PA_WardNo;
			$scope.newNewEmployee.CA_StreetName = $scope.newNewEmployee.PA_StreetName;
		} else {
			$scope.newNewEmployee.CA_FullAddress = '';
			$scope.newNewEmployee.CA_Province = '';
			$scope.newNewEmployee.CA_District = '';
			$scope.newNewEmployee.CA_LocalLevel = '';
			$scope.newNewEmployee.CA_WardNo = '';
			$scope.newNewEmployee.CA_StreetName = '';
		}
	}

	$scope.AddAcademicDetails = function (ind) {
		if ($scope.newNewEmployee.AcademicDetailsColl) {
			if ($scope.newNewEmployee.AcademicDetailsColl.length > ind + 1) {
				$scope.newNewEmployee.AcademicDetailsColl.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newNewEmployee.AcademicDetailsColl.push({
					ClassName: ''
				})
			}
		}
	};
	$scope.delAcademicDetails = function (ind) {
		if ($scope.newNewEmployee.AcademicDetailsColl) {
			if ($scope.newNewEmployee.AcademicDetailsColl.length > 1) {
				$scope.newNewEmployee.AcademicDetailsColl.splice(ind, 1);
			}
		}
	};

/*WorkExperience*/
	$scope.AddWorkExperience = function (ind) {
		if ($scope.newNewEmployee.WorkExperienceColl) {
			if ($scope.newNewEmployee.WorkExperienceColl.length > ind + 1) {
				$scope.newNewEmployee.WorkExperienceColl.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newNewEmployee.WorkExperienceColl.push({
					ClassName: ''
				})
			}
		}
	};
	$scope.delWorkExperience = function (ind) {
		if ($scope.newNewEmployee.WorkExperienceColl) {
			if ($scope.newNewEmployee.WorkExperienceColl.length > 1) {
				$scope.newNewEmployee.WorkExperienceColl.splice(ind, 1);
			}
		}
	};



/*Reference*/
	$scope.AddReference = function (ind) {
		if ($scope.newNewEmployee.ReferenceColl) {
			if ($scope.newNewEmployee.ReferenceColl.length > ind + 1) {
				$scope.newNewEmployee.ReferenceColl.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newNewEmployee.ReferenceColl.push({
					ClassName: ''
				})
			}
		}
	};
	$scope.delReference = function (ind) {
		if ($scope.newNewEmployee.ReferenceColl) {
			if ($scope.newNewEmployee.ReferenceColl.length > 1) {
				$scope.newNewEmployee.ReferenceColl.splice(ind, 1);
			}
		}
	};


	/*Reference Ends*/

	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newNewEmployee.AttachmentColl) {
			if ($scope.newNewEmployee.AttachmentColl.length > 0) {
				$scope.newNewEmployee.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newNewEmployee.AttachmentColl.push({
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
	$scope.ClearNewEmployeePhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newNewEmployee.PhotoData = null;
				$scope.newNewEmployee.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};

	$scope.ClearSignaturePhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newNewEmployee.SignatureData = null;
				$scope.newNewEmployee.Signature_TMP = [];
			});

		});

		$('#imgSignature').attr('src', '');
		$('#imgSignature1').attr('src', '');
	};
	$scope.ClearNewEmployee = function () {

		$scope.ClearNewEmployeePhoto();
		$scope.ClearSignaturePhoto();
		$scope.newNewEmployee = {
			NewEmployeeId: null,
			ApplicationId: null,
			ApplicationDate_TMP: new Date(),
			FirstName: '',
			MiddleName: '',
			LastName: '',
			Gender: 1,
			DOB_TMP: new Date(),
			ContactNo: '',
			IsPhysicalDisability: false,
			Photo: null,
			PhotoPath: null,
			Email: '',
			FullAddress: '',
			SourceId: null,
			SalaryExpectation: '',
			SubjectId: null,
			JobTitle: '',
			Level: '',
			AcademicDetailsColl: [],
			WorkExperienceColl: [],
			ReferenceColl: [],
			AttachmentColl: [],
			Mode: 'Save'
		};

		$scope.newNewEmployee.AcademicDetailsColl.push({});
		$scope.newNewEmployee.WorkExperienceColl.push({});
		$scope.newNewEmployee.ReferenceColl.push({});
	}

	$scope.IsValidNewEmployee = function () {
		if ($scope.newNewEmployee.FirstName.isEmpty()) {
			Swal.fire('Please ! Enter First Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateNewEmployee = function () {
		if ($scope.IsValidNewEmployee() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newNewEmployee.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateNewEmployee();
					}
				});
			} else
				$scope.CallSaveUpdateNewEmployee();

		}
	};

	$scope.CallSaveUpdateNewEmployee = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newNewEmployee.AttachmentColl;
		//$scope.newNewEmployee.AttachmentColl = [];

		var photo = $scope.newNewEmployee.Photo_TMP;
		var signature = $scope.newNewEmployee.Signature_TMP;

		if ($scope.newNewEmployee.AdmitDateDet) {
			$scope.newNewEmployee.AdmitDate = $scope.newNewEmployee.AdmitDateDet.dateAD;
		} else
			$scope.newNewEmployee.AdmitDate = new Date();

		if ($scope.newNewEmployee.DOB_ADDet) {
			$scope.newNewEmployee.DOB = $scope.newNewEmployee.DOB_ADDet.dateAD;
		}

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveNewEmployee",
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
			data: { jsonData: $scope.newNewEmployee, files: filesColl, stPhoto: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearNewEmployee();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetNewEmployeeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			HouseNameId: refData.HouseNameId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetNewEmployeeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newNewEmployee = res.data.Data;
				$scope.newNewEmployee.Mode = 'Modify';

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

});