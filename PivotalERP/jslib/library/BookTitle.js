app.controller('BookTitleController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Book Title';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.GenderColl = GlobalServices.getGenderList();
		$scope.CountryList = GlobalServices.getCountryList();

		$scope.currentPages = {
			BookTitle: 1,
			Author: 1,
			Edition: 1,
			Publication: 1,
			Subject: 1,
			Donor: 1,
			Rack: 1,
			Department: 1,
			Material: 1,
			BookCategory: 1
		};

		$scope.searchData = {
			BookTitle: '',
			Author: '',
			Edition: '',
			Publication: '',
			Subject: '',
			Donor: '',
			Rack: '',
			Department: '',
			Material: '',
			BookCategory: '',
		};

		$scope.perPage = {
			BookTitle: GlobalServices.getPerPageRow(),
			Author: GlobalServices.getPerPageRow(),
			Edition: GlobalServices.getPerPageRow(),
			Publication: GlobalServices.getPerPageRow(),
			Subject: GlobalServices.getPerPageRow(),
			Donor: GlobalServices.getPerPageRow(),
			Rack: GlobalServices.getPerPageRow(),
			Department: GlobalServices.getPerPageRow(),
			Material: GlobalServices.getPerPageRow(),
			BookCategory: GlobalServices.getPerPageRow()
		};

		$scope.newBookTitle = {
			BookTitleId: null,
			Name: '',
			Description: '',

			Mode: 'Save'
		};

		$scope.newAuthor = {
			AuthorId: null,
			Name: '',

			Gender: '',
			Nationality: '',
			Address: '',
			MobileNo: '',
			PhoneNo: '',
			EmailId: '',
			Description: '',
			ImagePath:'',
			Mode: 'Save'
		};

		$scope.newEdition = {
			EditionId: null,
			Name: '',
			Description: '',

			Mode: 'Save'
		};

		$scope.newPublication = {
			PublicationId: null,
			Name: '',
			Address: '',
			Country: '',
			Phoneno: '',
			Email: '',
			Description: '',
			LogoPath: '',
			ContactPersonList: [],
			AttachmentColl: [],
			Mode: 'Save'
		};
		$scope.newPublication.ContactPersonList.push({});
		
		$scope.newSubject = {
			SubjectId: null,
			Name: '',
			Code: '',
			CodeTH: '',
			CodePR:'',
			IsECA: false,

			Mode: 'Save'
		};

		$scope.newDonor = {
			DonorId: null,
			Name: '',
			Address: '',
			Description: '',
			MobileNo: '',
			PhoneNo: '',
			Email: '',
			ContactPersonList: [],
			AttachmentColl: [],
			Mode: 'Save'
		};
		$scope.newDonor.ContactPersonList.push({});
		
		$scope.newRack = {
			RackId: null,
			Name: '',
			Rackno: '',
			Location: '',
			Description: '',

			Mode: 'Save'
		};

		$scope.newDepartment = {
			DepartmentId: null,
			Name: '',
			Description: '',

			Mode: 'Save'
		};

		$scope.DocumentTypeList = [];
		GlobalServices.getDocumentTypeList().then(function (res) {
			$scope.DocumentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		

		$scope.newMaterial = {
			MaterialTypeId: null,
			Name: '',
			OrderNo: 0,
			Description: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};

		$scope.newDet = {
			BookCategoryId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'save'
		};

		$scope.GetAllBookTitleList();
		/*$scope.GetAllAuthorList();
		$scope.GetAllEditionList();
		$scope.GetAllPublicationList();
		$scope.GetAllSubjectList();
		$scope.GetAllDonorList();
		$scope.GetAllRackList();
		$scope.GetAllDepartmentList();
		$scope.GetAllMaterialList();
		$scope.GetAllnewDetList();*/
		$scope.isAuthorLoaded = false;
		$scope.isEditionLoaded = false;
		$scope.isPublicationLoaded = false;
		$scope.isSubjectLoaded = false;
		$scope.isDonorLoaded = false;
		$scope.isRackLoaded = false;
		$scope.isDepartmentLoaded = false;
		$scope.isMaterialLoaded = false;
		$scope.isBookCategoryLoaded = false;
	}

	//Customer Information Details Coll for Publication
	$scope.AddPublicationContact = function (ind) {
		if ($scope.newPublication.ContactPersonList) {
			if ($scope.newPublication.ContactPersonList.length > ind + 1) {
				$scope.newPublication.ContactPersonList.splice(ind + 1, 0, {
					Name: '',
					Designation: '',
					MobileNo: '',
					ContactNo: '',
					EmailId: ''
				})
			} else {
				$scope.newPublication.ContactPersonList.push({
					Name: '',
					Designation: '',
					MobileNo: '',
					ContactNo: '',
					EmailId: ''
				})
			}
		}
	};
	$scope.delPublicationContact = function (ind) {
		if ($scope.newPublication.ContactPersonList) {
			if ($scope.newPublication.ContactPersonList.length > 1) {
				$scope.newPublication.ContactPersonList.splice(ind, 1);
			}
		}
	};
	$scope.AddMoreFilesPublication = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newPublication.AttachmentColl.push({
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

				$scope.newPublication.docType = null;
				$scope.newPublication.attachFile = null;
				$scope.newPublication.docDescription = '';

				$('#flMoreFiles').val('');
			}
		}
	};
	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newPublication.AttachmentColl) {
			if ($scope.newPublication.AttachmentColl.length > 0) {
				$scope.newPublication.AttachmentColl.splice(ind, 1);
			}
		}
	}

	$scope.AddDonorContact = function (ind) {
		if ($scope.newDonor.ContactPersonList) {
			if ($scope.newDonor.ContactPersonList.length > ind + 1) {
				$scope.newDonor.ContactPersonList.splice(ind + 1, 0, {
					Name: '',
					Designation: '',
					MobileNo: '',
					ContactNo: '',
					EmailId: ''
				})
			} else {
				$scope.newDonor.ContactPersonList.push({
					Name: '',
					Designation: '',
					MobileNo: '',
					ContactNo: '',
					EmailId: ''
				})
			}
		}
	};
	$scope.delDonorContact = function (ind) {
		if ($scope.newDonor.ContactPersonList) {
			if ($scope.newDonor.ContactPersonList.length > 1) {
				$scope.newDonor.ContactPersonList.splice(ind, 1);
			}
		}
	};
	$scope.AddMoreFilesDonor = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newDonor.AttachmentColl.push({
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

				$scope.newDonor.docType = null;
				$scope.newDonor.attachFile = null;
				$scope.newDonor.docDescription = '';

				$('#flMoreFilesDonor').val('');
			}
		}
	};
	$scope.delAttachmentFilesDonor = function (ind) {
		if ($scope.newDonor.AttachmentColl) {
			if ($scope.newDonor.AttachmentColl.length > 0) {
				$scope.newDonor.AttachmentColl.splice(ind, 1);
			}
		}
	}

	function OnClickDefault() {


		document.getElementById('book-form').style.display = "none";
		document.getElementById('author-form').style.display = "none";
		document.getElementById('edition-form').style.display = "none";
		document.getElementById('publication-form').style.display = "none";
		document.getElementById('subject-form').style.display = "none";
		document.getElementById('donor-form').style.display = "none";
		document.getElementById('rack-form').style.display = "none";
		document.getElementById('department-form').style.display = "none";
		document.getElementById('material-form').style.display = "none";

		document.getElementById('add-BookCategory-form').style.display = "none";

		//book area
		document.getElementById('add-book').onclick = function () {
			document.getElementById('book-title-section').style.display = "none";
			document.getElementById('book-form').style.display = "block";
			$scope.ClearBookTitle();
		}


		document.getElementById('back-book-list').onclick = function () {
			document.getElementById('book-title-section').style.display = "block";
			document.getElementById('book-form').style.display = "none";
			$scope.ClearBookTitle();
		}


		// author area

		document.getElementById('add-author').onclick = function () {
			document.getElementById('author-section').style.display = "none";
			document.getElementById('author-form').style.display = "block";
			$scope.ClearAuthor();
		}

		document.getElementById('back-author-list').onclick = function () {
			document.getElementById('author-section').style.display = "block";
			document.getElementById('author-form').style.display = "none";
			$scope.ClearAuthor();
		}


		// // edition section

		document.getElementById('add-edition').onclick = function () {
			document.getElementById('edition-section').style.display = "none";
			document.getElementById('edition-form').style.display = "block";
			$scope.ClearEdition();
		}

		document.getElementById('back-edition-list').onclick = function () {
			document.getElementById('edition-section').style.display = "block";
			document.getElementById('edition-form').style.display = "none";
			$scope.ClearEdition();
		}

		// // publication section

		document.getElementById('add-publication').onclick = function () {
			document.getElementById('publication-section').style.display = "none";
			document.getElementById('publication-form').style.display = "block";
			$scope.ClearPublication();
		}

		document.getElementById('back-publication-list').onclick = function () {
			document.getElementById('publication-section').style.display = "block";
			document.getElementById('publication-form').style.display = "none";
			$scope.ClearPublication();
		}

		//subject section
		document.getElementById('add-subject').onclick = function () {
			document.getElementById('subject-section').style.display = "none";
			document.getElementById('subject-form').style.display = "block";
			$scope.ClearSubject();
		}

		document.getElementById('back-subject-list').onclick = function () {
			document.getElementById('subject-section').style.display = "block";
			document.getElementById('subject-form').style.display = "none";
			$scope.ClearSubject();
		}

		//donor section
		document.getElementById('add-donor').onclick = function () {
			document.getElementById('donor-section').style.display = "none";
			document.getElementById('donor-form').style.display = "block";
			$scope.ClearDonor();
		}

		document.getElementById('back-donor-list').onclick = function () {
			document.getElementById('donor-section').style.display = "block";
			document.getElementById('donor-form').style.display = "none";
			$scope.ClearDonor();
		}

		//Rack section
		document.getElementById('add-rack').onclick = function () {
			document.getElementById('rack-section').style.display = "none";
			document.getElementById('rack-form').style.display = "block";
			$scope.ClearRack();
		}

		document.getElementById('back-rack-list').onclick = function () {
			document.getElementById('rack-section').style.display = "block";
			document.getElementById('rack-form').style.display = "none";
			$scope.ClearRack();
		}

		//Deparment section
		document.getElementById('add-department').onclick = function () {
			document.getElementById('department-section').style.display = "none";
			document.getElementById('department-form').style.display = "block";
			$scope.ClearDepartment();
		}

		document.getElementById('back-department-list').onclick = function () {
			document.getElementById('department-section').style.display = "block";
			document.getElementById('department-form').style.display = "none";
			$scope.ClearDepartment();
		}

		//Material Type section
		document.getElementById('add-material').onclick = function () {
			document.getElementById('material-section').style.display = "none";
			document.getElementById('material-form').style.display = "block";
			$scope.ClearMaterial();
		}

		document.getElementById('back-material-list').onclick = function () {
			document.getElementById('material-section').style.display = "block";
			document.getElementById('material-form').style.display = "none";
			$scope.ClearMaterial();
		}

		//Category
		document.getElementById('add-BookCategory').onclick = function () {
			document.getElementById('table-section').style.display = "none";
			document.getElementById('add-BookCategory-form').style.display = "block";
		}
		document.getElementById('BookCategoryback-btn').onclick = function () {
			document.getElementById('add-BookCategory-form').style.display = "none";
			document.getElementById('table-section').style.display = "block";
		}

		$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
			var target = $(e.target).attr("href");
			if (target === "#profile") {
				if (!$scope.isAuthorLoaded) {
					$scope.GetAllAuthorList();
					$scope.isAuthorLoaded = true;
				}
			} else if (target === "#edition") {
				if (!$scope.isEditionLoaded) {
					$scope.GetAllEditionList();
					$scope.isEditionLoaded = true;
				}
			} else if (target === "#board") {
				if (!$scope.isPublicationLoaded) {
					$scope.GetAllPublicationList();
					$scope.isPublicationLoaded = true;
				}
			} else if (target === "#subject") {
				if (!$scope.isSubjectLoaded) {
					$scope.GetAllSubjectList();
					$scope.isSubjectLoaded = true;
				}
			} else if (target === "#donor") {
				if (!$scope.isDonorLoaded) {
					$scope.GetAllDonorList();
					$scope.isDonorLoaded = true;
				}
			} else if (target === "#rack") {
				if (!$scope.isRackLoaded) {
					$scope.GetAllRackList();
					$scope.isRackLoaded = true;
				}
			} else if (target === "#department") {
				if (!$scope.isDepartmentLoaded) {
					$scope.GetAllDepartmentList();
					$scope.isDepartmentLoaded = true;
				}
			} else if (target === "#material") {
				if (!$scope.isMaterialLoaded) {
					$scope.GetAllMaterialList();
					$scope.isMaterialLoaded = true;
				}
			} else if (target === "#Category") {
				if (!$scope.isBookCategoryLoaded) {
					$scope.GetAllnewDetList();
					$scope.isBookCategoryLoaded = true;
				}
			}
			$scope.$appyAsync();
		});
	}

	$scope.ClearMaterial = function () {

		$timeout(function () {
			$scope.newMaterial = {
				MaterialTypeId: null,
				Name: '',
				OrderNo: 0,
				Description: '',
				Photo: null,
				PhotoPath: null,
				Mode: 'Save'
			};
		});

		
	}

	//Material Type Clear photo
	$scope.ClearMaterialPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newMaterial.PhotoData = null;
				$scope.newMaterial.Photo_TMP = [];
			});

		});

		$('#imgPhoto-material').attr('src', '');
		$('#imgPhoto1-material').attr('src', '');

	};

	$scope.ClearBookTitle = function () {

		$timeout(function () {
			$scope.newBookTitle = {
				BookTitleId: null,
				Name: '',
				Description: '',

				Mode: 'Save'
			};
		});
	

	}
	$scope.ClearAuthor = function () {
		$timeout(function () {
			$scope.ClearAuthorPhoto();
			$scope.newAuthor = {
				AuthorId: null,
				Name: '',

				Gender: '',
				Nationality: '',
				Address: '',
				MobileNo: '',
				PhoneNo: '',
				EmailId: '',
				Description: '',
				ImagePath: '',
				Mode: 'Save'
			};
		});
		
	}
	$scope.ClearEdition = function () {
		$timeout(function () {
			$scope.newEdition = {
				EditionId: null,
				Name: '',
				Description: '',

				Mode: 'Save'
			};
		});
		
	}

	$scope.ClearPublication = function () {
		
		$timeout(function () {
			$scope.ClearPublicationPhoto();
			$scope.newPublication = {
				PublicationId: null,
				Name: '',
				Address: '',
				Country: '',
				Phoneno: '',
				Email: '',
				Description: '',
				LogoPath: '',
				ContactPersonList: [],
				AttachmentColl: [],
				Mode: 'Save'
			};
			$scope.newPublication.ContactPersonList.push({});
		});
		
	}

	$scope.ClearSubject = function () {
		$timeout(function () {
			$scope.newSubject = {
				SubjectId: null,
				Name: '',
				Code: '',
				CodeTH: '',
				CodePR: '',
				IsECA: false,

				Mode: 'Save'
			};
		});
	
	}

	$scope.ClearDonor = function () {

		$timeout(function () {
			$scope.ClearDonorPhoto();
			$scope.newDonor = {
				DonorId: null,
				Name: '',
				Address: '',
				Description: '',
				MobileNo: '',
				PhoneNo: '',
				Email: '',
				ContactPersonList: [],
				AttachmentColl: [],
				Mode: 'Save'
			};
			$scope.newDonor.ContactPersonList.push({});
		});
		
	}

	$scope.ClearRack = function () {
		$timeout(function () {
			$scope.newRack = {
				RackId: null,
				Name: '',
				Rackno: '',
				Location: '',
				Description: '',

				Mode: 'Save'
			};
		});
		


	}

	$scope.ClearDepartment = function () {
		$timeout(function () {
			$scope.newDepartment = {
				DepartmentId: null,
				Name: '',
				Description: '',

				Mode: 'Save'
			};
		});
		
	}


	//Author Clear photo
	$scope.ClearAuthorPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newAuthor.PhotoData = null;
				$scope.newAuthor.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};

	//Publication Clear photo
	$scope.ClearPublicationPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newPublication.PhotoData = null;
				$scope.newPublication.Photo_TMP = [];
			});

		});

		$('#imgPhoto-publication').attr('src', '');
		$('#imgPhoto1-publication').attr('src', '');

	};
	//Donor Clear photo
	$scope.ClearDonorPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newDonor.PhotoData = null;
				$scope.newDonor.Photo_TMP = [];
			});

		});

		$('#imgPhoto-donor').attr('src', '');
		$('#imgPhoto1-donor').attr('src', '');

	};

	//************************* Book Title *********************************

	$scope.IsValidBookTitle = function () {
		if ($scope.newBookTitle.Name.isEmpty()) {
			Swal.fire('Please ! Enter Book Name');
			return false;
		}

		if ($scope.newBookTitle.Description.isEmpty()) {
			Swal.fire('Please ! Enter Description');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateBookTitle = function () {
		if ($scope.IsValidBookTitle() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBookTitle.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBookTitle();
					}
				});
			} else
				$scope.CallSaveUpdateBookTitle();

		}
	};

	$scope.CallSaveUpdateBookTitle = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Library/Master/SaveBookTitle",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newBookTitle }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBookTitle();
				$scope.GetAllBookTitleList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllBookTitleList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BookTitleList = [];

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllBookTitleList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BookTitleList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetBookTitleById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BookTitleId: refData.BookTitleId
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetBookTitleById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBookTitle = res.data.Data;
				$scope.newBookTitle.Mode = 'Modify';

				document.getElementById('book-title-section').style.display = "none";
				document.getElementById('book-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBookTitleById = function (refData) {

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
					BookTitleId: refData.BookTitleId
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelBookTitle",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBookTitleList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Author *********************************

	$scope.IsValidAuthor = function () {
		if ($scope.newAuthor.Name.isEmpty()) {
			Swal.fire('Please ! Enter Author Name');
			return false;
		}		
		//if ($scope.newAuthor.Nationality.isEmpty()) {
		//	Swal.fire('Please ! Enter Nationality');
		//	return false;
		//}
		//if ($scope.newAuthor.Address.isEmpty()) {
		//	Swal.fire('Please ! Enter Address');
		//	return false;
		//}
		
		return true;
	}

	$scope.SaveUpdateAuthor = function () {
		if ($scope.IsValidAuthor() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAuthor.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAuthor();
					}
				});
			} else
				$scope.CallSaveUpdateAuthor();

		}
	};

	$scope.CallSaveUpdateAuthor = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var authorPhoto = $scope.newAuthor.Photo_TMP;
		
		$http({
			method: 'POST',
			url: base_url + "Library/Master/SaveAuthor",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.photo && data.photo.length>0)
					formData.append("photo", data.photo[0]);

				return formData;
			},
			data: { jsonData: $scope.newAuthor, photo: authorPhoto }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAuthor();
				$scope.GetAllAuthorList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllAuthorList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AuthorList = [];

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllAuthorList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AuthorList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetAuthorById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AuthorId: refData.AuthorId
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAuthorById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAuthor = res.data.Data;
				$scope.newAuthor.Mode = 'Modify';

				document.getElementById('author-section').style.display = "none";
				document.getElementById('author-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAuthorById = function (refData) {

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
					AuthorId: refData.AuthorId
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelAuthor",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAuthorList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Edition *********************************

	$scope.IsValidEdition = function () {
		if ($scope.newEdition.Name.isEmpty()) {
			Swal.fire('Please ! Enter Edition Name');
			return false;
		}

		//if ($scope.newEdition.Description.isEmpty()) {
		//	Swal.fire('Please ! Enter Edition Description');
		//	return false;
		//}

		return true;
	}

	$scope.SaveUpdateEdition = function () {
		if ($scope.IsValidEdition() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newEdition.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateEdition();
					}
				});
			} else
				$scope.CallSaveUpdateEdition();

		}
	};

	$scope.CallSaveUpdateEdition = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Library/Master/SaveEdition",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newEdition }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearEdition();
				$scope.GetAllEditionList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllEditionList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EditionList = [];

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllEditionList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EditionList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetEditionById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			EditionId: refData.EditionId
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetEditionById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEdition = res.data.Data;
				$scope.newEdition.Mode = 'Modify';

				document.getElementById('edition-section').style.display = "none";
				document.getElementById('edition-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelEditionById = function (refData) {

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
					EditionId: refData.EditionId
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelEdition",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllEditionList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Publication *********************************

	$scope.IsValidPublication = function () {
		if ($scope.newPublication.Name.isEmpty()) {
			Swal.fire('Please ! Enter Publication Name');
			return false;
		}
		//if ($scope.newPublication.Address.isEmpty()) {
		//	Swal.fire('Please ! Enter Publication Address');
		//	return false;
		//}
		
		return true;
	}

	$scope.SaveUpdatePublication = function () {
		if ($scope.IsValidPublication() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newPublication.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdatePublication();
					}
				});
			} else
				$scope.CallSaveUpdatePublication();

		}
	};

	$scope.CallSaveUpdatePublication = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newPublication.AttachmentColl;
		var publicationPhoto = $scope.newPublication.Photo_TMP;

		$http({
			method: 'POST',
			url: base_url + "Library/Master/SavePublication",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.photo && data.photo.length > 0)
					formData.append("photo", data.photo[0]);

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}


				return formData;
			},
			data: { jsonData: $scope.newPublication, files: filesColl, photo: publicationPhoto  }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearPublication();
				$scope.GetAllPublicationList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllPublicationList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PublicationList = [];

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllPublicationList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PublicationList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetPublicationById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			PublicationId: refData.PublicationId
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetPublicationById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newPublication = res.data.Data;
				$scope.newPublication.Mode = 'Modify';

				if (!$scope.newPublication.ContactPersonList || $scope.newPublication.ContactPersonList.length==0)
					$scope.newPublication.ContactPersonList.push({});

				document.getElementById('publication-section').style.display = "none";
				document.getElementById('publication-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelPublicationById = function (refData) {

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
					PublicationId: refData.PublicationId
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelPublication",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllPublicationList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//************************* Subject *********************************

	$scope.IsValidSubject = function () {
		if ($scope.newSubject.Name.isEmpty()) {
			Swal.fire('Please ! Enter Subject Name');
			return false;
		}
		

		return true;
	}

	$scope.SaveUpdateSubject = function () {
		if ($scope.IsValidSubject() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSubject.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSubject();
					}
				});
			} else
				$scope.CallSaveUpdateSubject();

		}
	};

	$scope.CallSaveUpdateSubject = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveSubject",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSubject }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearSubject();
				$scope.GetAllSubjectList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllSubjectList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SubjectList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllSubjectList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SubjectList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetSubjectById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			SubjectId: refData.SubjectId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetSubjectById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSubject = res.data.Data;
				$scope.newSubject.Mode = 'Modify';

				document.getElementById('subject-section').style.display = "none";
				document.getElementById('subject-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelSubjectById = function (refData) {

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
					SubjectId: refData.SubjectId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelSubject",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllSubjectList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//************************* Donor *********************************

	$scope.IsValidDonor = function () {
		if ($scope.newDonor.Name.isEmpty()) {
			Swal.fire('Please ! Enter Donor Name');
			return false;
		}
		if ($scope.newDonor.Address.isEmpty()) {
			Swal.fire('Please ! Enter Donor Address');
			return false;
		}
		
		return true;
	}

	$scope.SaveUpdateDonor = function () {
		if ($scope.IsValidDonor() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDonor.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDonor();
					}
				});
			} else
				$scope.CallSaveUpdateDonor();

		}
	};

	$scope.CallSaveUpdateDonor = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newDonor.AttachmentColl;
		var publicationPhoto = $scope.newDonor.Photo_TMP;

		$http({
			method: 'POST',
			url: base_url + "Library/Master/SaveDonor",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));


				if (data.photo && data.photo.length > 0)
					formData.append("photo", data.photo[0]);

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}

				return formData;
			},
			data: { jsonData: $scope.newDonor, files: filesColl, photo: publicationPhoto }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearDonor();
				$scope.GetAllDonorList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllDonorList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.DonorList = [];

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllDonorList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DonorList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetDonorById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			DonorId: refData.DonorId
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetDonorById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDonor = res.data.Data;
				$scope.newDonor.Mode = 'Modify';

				if (!$scope.newDonor.ContactPersonList || $scope.newDonor.ContactPersonList.length == 0)
					$scope.newDonor.ContactPersonList.push({});

				document.getElementById('donor-section').style.display = "none";
				document.getElementById('donor-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelDonorById = function (refData) {

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
					DonorId: refData.DonorId
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelDonor",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllDonorList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//************************* Rack *********************************

	$scope.IsValidRack = function () {
		if ($scope.newRack.Name.isEmpty()) {
			Swal.fire('Please ! Enter Rack Name');
			return false;
		}
		if ($scope.newRack.Location.isEmpty()) {
			Swal.fire('Please ! Enter Rack Location');
			return false;
		}

		
		return true;
	}

	$scope.SaveUpdateRack = function () {
		if ($scope.IsValidRack() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newRack.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateRack();
					}
				});
			} else
				$scope.CallSaveUpdateRack();

		}
	};

	$scope.CallSaveUpdateRack = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Library/Master/SaveRack",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newRack }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearRack();
				$scope.GetAllRackList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllRackList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.RackList = [];

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllRackList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.RackList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetRackById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			RackId: refData.RackId
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetRackById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newRack = res.data.Data;
				$scope.newRack.Mode = 'Modify';

				document.getElementById('rack-section').style.display = "none";
				document.getElementById('rack-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelRackById = function (refData) {

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
					RackId: refData.RackId
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelRack",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllRackList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//************************* Department *********************************

	$scope.IsValidDepartment = function () {
		if ($scope.newDepartment.Name.isEmpty()) {
			Swal.fire('Please ! Enter Department Name');
			return false;
		}

		
		return true;
	}

	$scope.SaveUpdateDepartment = function () {
		if ($scope.IsValidDepartment() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDepartment.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDepartment();
					}
				});
			} else
				$scope.CallSaveUpdateDepartment();

		}
	};

	$scope.CallSaveUpdateDepartment = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Library/Master/SaveDepartment",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newDepartment }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearDepartment();
				$scope.GetAllDepartmentList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllDepartmentList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.DepartmentList = [];

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllDepartmentList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DepartmentList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetDepartmentById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			DepartmentId: refData.DepartmentId
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetDepartmentById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDepartment = res.data.Data;
				$scope.newDepartment.Mode = 'Modify';

				document.getElementById('department-section').style.display = "none";
				document.getElementById('department-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelDepartmentById = function (refData) {

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
					DepartmentId: refData.DepartmentId
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelDepartment",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllDepartmentList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//*****************************Material Type******************************************

	$scope.IsValidMaterial = function () {
		if ($scope.newMaterial.Name.isEmpty()) {
			Swal.fire('Please ! Enter Material Name');
			return false;
		}

		//if ($scope.newMaterial.Description.isEmpty()) {
		//	Swal.fire('Please ! Enter Material Description');
		//	return false;
		//}

		return true;
	}

	$scope.SaveUpdateMaterial = function () {
		if ($scope.IsValidMaterial() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newMaterial.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateMaterial();
					}
				});
			} else
				$scope.CallSaveUpdateMaterial();

		}
	};

	$scope.CallSaveUpdateMaterial = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		

		$http({
			method: 'POST',
			url: base_url + "Library/Master/SaveMaterialType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

			
				return formData;
			},
			data: { jsonData: $scope.newMaterial   }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearMaterial();
				$scope.GetAllMaterialList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

		
	}

	$scope.GetAllMaterialList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.MaterialList = [];

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllMaterialTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.MaterialList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetMaterialById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			MaterialTypeId: refData.MaterialTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetMaterialTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newMaterial = res.data.Data;
				$scope.newMaterial.Mode = 'Modify';

				document.getElementById('material-section').style.display = "none";
				document.getElementById('material-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelMaterialById = function (refData) {

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
					MaterialTypeId: refData.MaterialTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelMaterialType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllMaterialList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

	//Category tab js code

	$scope.ClearDetails = function () {
		$timeout(function () {
			$scope.newDet = {
				BookCategoryId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'save'
			};
		});
	};

	$scope.IsValidAddBookCategory = function () {
		if ($scope.newDet.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}
		return true;
	}

	$scope.SaveBookCategory = function () {
		if ($scope.IsValidAddBookCategory() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.SaveUpdateBookCategory();
					}
				});
			} else
				$scope.SaveUpdateBookCategory();
		}
	};

	$scope.SaveUpdateBookCategory = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Library/Master/SaveBookCategory",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newDet }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearDetails();
				$scope.GetAllnewDetList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}


	$scope.GetAllnewDetList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AddBookCategoryList = [];
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetAllBookCategory",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AddBookCategoryList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetBookCategoryById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			BookCategoryId: refData.BookCategoryId
		};
		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetBookCategoryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDet = res.data.Data;

				$scope.newDet.Mode = 'Modify';

				document.getElementById('table-section').style.display = "none";
				document.getElementById('add-BookCategory-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.DelBookCategoryById = function (refData) {
		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {

			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					BookCategoryId: refData.BookCategoryId
				};
				$http({
					method: 'POST',
					url: base_url + "Library/Master/DelBookCategory",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.ClearDetails();
						$scope.GetAllnewDetList();
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