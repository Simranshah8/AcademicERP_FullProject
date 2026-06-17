app.controller('UploadPhotoSignatureController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Upload Photos and Signature';

	/*OnClickDefault();*/

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.UploadStudentPhotoOptions = [{ id: 1, text: 'Regd.No.' }, { id: 2, text: 'Board Regd.No.' }, { id: 3, text: 'Enroll No.' }, { id: 4, text: 'Card No.' }, { id: 5, text: 'EMS Id' }
			, { id: 6, text: 'System Id' }, { id: 7, text: 'StudentId' }, { id: 8, text: 'RollNo' }
		];

		$scope.UploadEmployeePhotoOptions = [{ id: 1, text: 'EmployeeCode' }, { id: 2, text: 'Enroll No.' }, { id: 3, text: 'Card No.' }, { id: 4, text: 'EmployeeId' }, { id: 5, text: 'AutoNumber' }];


		$scope.SignatureTypeList = [{ id: 1, text: 'Principal' }, { id: 2, text: 'Vice Principal' }, { id: 3, text: 'Accountant' }, { id: 4, text: 'Academic Co-Ordinator' }, { id: 5, text: 'Exam Co-Ordinator' }, { id: 6, text: 'Warden' }, { id: 7, text: 'Librarian' }];

		$scope.ClassList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassTeacherList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassTeacherList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassTeacherList = res.data.Data;
				$scope.GetAllSignature();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.UserList = [];

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUserList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserList = res.data.Data;
				$scope.GetAllSignature();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.currentPages = {
			StudentPhotoPhoto: 1,
			EmployeePhotoPhoto: 1,
			UploadSignature: 1
		};

		$scope.searchData = {
			StudentPhotoPhoto: '',
			EmployeePhotoPhoto: '',
			UploadSignature: ''
		};

		$scope.perPage = {
			StudentPhotoPhoto: GlobalServices.getPerPageRow(),
			EmployeePhotoPhoto: GlobalServices.getPerPageRow(),
			UploadSignature: GlobalServices.getPerPageRow()
		};



		$scope.newStudentPhoto = {
			StudentPhotoId: null,
			PhotoUploadedBy: null,

			Mode: 'Save'
		};

		$scope.newEmployeePhoto = {
			EmployeePhotoId: null,
			PhotoUploadedBy: null,
			Mode: 'Save'
		};

		$scope.newUploadSignature = {
			UploadSignatureId: null,

			Mode: 'Save'
		};

		/*	$scope.GetAllSignature();*/
		//$scope.GetAllStudentPhotoList();
		//$scope.GetAllEmployeePhotoList();
		//$scope.GetAllUploadSignatureList();
		$scope.GetAllPhotoStatus();

	}

	$scope.ChangeUploadBy = function () {
		if ($scope.newStudentPhoto.PhotoUploadedBy != 8)
			$scope.newStudentPhoto.SelectedClassSec = null;
	}

	$scope.UploadStudentPhotoToSrv = function () {
		if ($scope.newStudentPhoto.Files_TMP && $scope.newStudentPhoto.Files_TMP.length > 0) {

			if ($scope.newStudentPhoto.SelectedClassSec) {
				$scope.newStudentPhoto.ClassId = $scope.newStudentPhoto.SelectedClassSec.ClassId;
				$scope.newStudentPhoto.SectionId = $scope.newStudentPhoto.SelectedClassSec.SectionId;
			}

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/UpdateStudentPhoto",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					var f = 0;
					angular.forEach(data.files, function (fl) {
						formData.append("file" + f, fl);
						f = f + 1;
					});

					return formData;
				},
				data: { jsonData: $scope.newStudentPhoto, files: $scope.newStudentPhoto.Files_TMP }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				var data = res.data;
				Swal.fire(data.ResponseMSG);

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});


		}
	};

	$scope.ClearStudentPhoto = function () {
		$scope.newStudentPhoto = {
			StudentPhotoId: null,
			PhotoUploadedBy: null,
			Mode: 'Save'
		};
	}
	$scope.ClearEmployeePhoto = function () {
		$scope.newEmployeePhoto = {
			EmployeePhotoId: null,
			ClassId: null,
			Mode: 'Save'
		};
	}
	$scope.UploadEmployeePhotoToSrv = function () {
		if ($scope.newEmployeePhoto.Files_TMP && $scope.newEmployeePhoto.Files_TMP.length > 0) {

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/UpdateEmployeePhoto",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					var f = 0;
					angular.forEach(data.files, function (fl) {
						formData.append("file" + f, fl);
						f = f + 1;
					});

					return formData;
				},
				data: { jsonData: $scope.newEmployeePhoto, files: $scope.newEmployeePhoto.Files_TMP }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				var data = res.data;
				Swal.fire(data.ResponseMSG);

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});


		}
	};
	$scope.ClearUploadSignature = function () {
		$scope.newUploadSignature = {

			Mode: 'Save'
		};
	}

	//************************* StudentPhoto *********************************

	$scope.IsValidStudentPhoto = function () {
		if ($scope.newStudentPhoto.PhotoUploadedBy.isEmpty()) {
			Swal.fire('Please !Select Who Uploaded Photo');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateStudentPhoto = function () {
		if ($scope.IsValidStudentPhoto() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudentPhoto.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudentPhoto();
					}
				});
			} else
				$scope.CallSaveUpdateStudentPhoto();

		}
	};

	$scope.CallSaveUpdateStudentPhoto = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveStudentPhoto",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newStudentPhoto }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStudentPhoto();
				$scope.GetAllStudentPhotoList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllStudentPhotoList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentPhotoList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllStudentPhotoList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentPhotoList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetStudentPhotoById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			StudentPhotoId: refData.StudentPhotoId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetStudentPhotoById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newStudentPhoto = res.data.Data;
				$scope.newStudentPhoto.Mode = 'Modify';

				document.getElementById('StudentPhoto-content').style.display = "none";
				document.getElementById('StudentPhoto-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelStudentPhotoById = function (refData) {

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
					StudentPhotoId: refData.StudentPhotoId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelStudentPhoto",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStudentPhotoList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//*************************Employee Photo*********************************

	//$scope.IsValidEmployeePhoto = function () {
	//	if ($scope.newEmployeePhoto.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter EmployeePhoto Name');
	//		return false;
	//	}



	//	return true;
	//}

	$scope.SaveUpdateEmployeePhoto = function () {
		if ($scope.IsValidEmployeePhoto() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newEmployeePhoto.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateEmployeePhoto();
					}
				});
			} else
				$scope.CallSaveUpdateEmployeePhoto();

		}
	};

	$scope.CallSaveUpdateEmployeePhoto = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveEmployeePhoto",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newEmployeePhoto }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearEmployeePhoto();
				$scope.GetAllEmployeePhotoList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllEmployeePhotoList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EmployeePhotoList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllEmployeePhotoList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmployeePhotoList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetEmployeePhotoById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			EmployeePhotoId: refData.EmployeePhotoId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetEmployeePhotoById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEmployeePhoto = res.data.Data;
				$scope.newEmployeePhoto.Mode = 'Modify';

				document.getElementById('batch-StudentPhoto').style.display = "none";
				document.getElementById('batch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelEmployeePhotoById = function (refData) {

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
					EmployeePhotoId: refData.EmployeePhotoId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelEmployeePhoto",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllEmployeePhotoList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Signature *********************************

	$scope.ClearUploadSignaturePrinciple = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newUploadSignature.Principle = null;
				$scope.newUploadSignature.Principle_TMP = [];
			});

		});



		$('#imgPrinciple').attr('src', '');
		$('#imgPrinciple1').attr('src', '');


	};

	$scope.ClearUploadSignatureVicePrinciple = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newUploadSignature.VicePrinciple = null;
				$scope.newUploadSignature.VicePrinciple_TMP = [];
			});

		});


		$('#imgVicePrinciple').attr('src', '');
		$('#imgVicePrinciple1').attr('src', '');


	};

	$scope.ClearUploadSignatureAccountant = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newUploadSignature.Accountant = null;
				$scope.newUploadSignature.Accountant_TMP = [];
			});

		});



		$('#imgAccountant').attr('src', '');
		$('#imgAccountant1').attr('src', '');
	};

	$scope.ClearUploadSignatureAcademicC = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newUploadSignature.AcademicC = null;
				$scope.newUploadSignature.AcademicC_TMP = [];
			});

		});



		$('#imgAcademicC').attr('src', '');
		$('#imgAcademicC1').attr('src', '');
	};


	$scope.ClearUploadSignatureExamC = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newUploadSignature.ExamC = null;
				$scope.newUploadSignature.ExamC_TMP = [];
			});

		});



		$('#imgExamC').attr('src', '');
		$('#imgExamC1').attr('src', '');
	};

	$scope.ClearUploadSignatureWarden = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newUploadSignature.Warden = null;
				$scope.newUploadSignature.Warden_TMP = [];
			});

		});



		$('#imgWarden').attr('src', '');
		$('#imgWarden1').attr('src', '');
	};


	$scope.ClearUploadSignatureLibrarian = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newUploadSignature.Librarian = null;
				$scope.newUploadSignature.Librarian_TMP = [];
			});

		});



		$('#imgLibrarian').attr('src', '');
		$('#imgLibrarian1').attr('src', '');
	};






	$scope.SaveUpdateUploadSignature = function () {
		if ($scope.IsValidUploadSignature() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newUploadSignature.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateUploadSignature();
					}
				});
			} else
				$scope.CallSaveUpdateUploadSignature();

		}
	};

	$scope.CallSaveUpdateUploadSignature = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var photo = $scope.newUploadSignature.Photo_TMP;
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Transaction/SaveUploadSignature",
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
			data: { jsonData: $scope.newUploadSignature, stPhoto: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStudent();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

		var photo = $scope.newUploadSignature.Principle_TMP;
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Transaction/SaveUploadSignature",
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
			data: { jsonData: $scope.newUploadSignature, stPhoto: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStudent();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveUploadSignature",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newUploadSignature }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearUploadSignature();
				$scope.GetAllUploadSignatureList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllUploadSignatureList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.UploadSignatureList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllUploadSignatureList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UploadSignatureList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetUploadSignatureById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			UploadSignatureId: refData.UploadSignatureId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetUploadSignatureById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newUploadSignature = res.data.Data;
				$scope.newUploadSignature.Mode = 'Modify';

				//document.getElementById('table-listing').style.display = "none";
				//document.getElementById('notice-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelUploadSignatureById = function (refData) {

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
					UploadSignatureId: refData.UploadSignatureId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelUploadSignature",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllUploadSignatureList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};


	$scope.GetAllSignature = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		// Initialize
		$scope.SignatureList = [];
		$scope.ClassTeacherSigList = [];
		$scope.UserSignatureList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllSignature",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				$scope.SignatureList = res.data.Data.SignatureList || [];
				$scope.ClassTeacherSigList = res.data.Data.ClassTeacherSigList || [];
				$scope.UserSignatureList = res.data.Data.UserSignatureList || [];

				// Map SignatureList to SignatureTypeList
				if ($scope.SignatureTypeList && $scope.SignatureTypeList.length) {
					$scope.SignatureTypeList.forEach(function (typeItem) {
						var match = $scope.SignatureList.find(function (sig) {
							return sig.SignatureType === typeItem.id;
						});

						if (match) {
							typeItem.SignatureId = match.SignatureId;
							typeItem.ImagePath = match.PhotoPath || ''; // Assign empty if not present
						}
					});
				}

				if ($scope.UserList && $scope.UserList.length) {
					$scope.UserList.forEach(function (typeItem) {
						var match2 = $scope.UserSignatureList.find(function (sig) {
							return sig.UserId === typeItem.UserId;
						});

						if (match2) {
							typeItem.UserSignatureId = match2.UserSignatureId;
							typeItem.PhotoPath = match2.PhotoPath || ''; // Assign empty if not present
						}
					});
				}

				// Check if ClassTeacherList is available
				if ($scope.ClassTeacherList && $scope.ClassTeacherList.length) {
					$scope.ClassTeacherList.forEach(function (typeItem) {
						var match1 = $scope.ClassTeacherSigList.find(function (sig) {
							return sig.TeacherId === typeItem.TeacherId;
						});

						if (match1) {
							typeItem.TranId = match1.TranId;
							typeItem.PhotoPath = match1.PhotoPath || ''; // Assign empty if not present
						} else {
							typeItem.PhotoPath = ''; // Clear if no match
						}
					});
				} else {
					console.warn("ClassTeacherList is empty or undefined.");
				}
			} else {
				Swal.fire(res.data.ResponseMSG || "Failed to retrieve signature data.");
			}

		}, function (reason) {
			hidePleaseWait();
			console.error("Error fetching signature data:", reason);
			Swal.fire('Failed to load signatures: ' + reason.statusText || reason);
		});
	};






	$scope.SaveUpdateSignature = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.SignatureTypeList.forEach(function (typeItem) {
			var match = $scope.SignatureList.find(function (sig) {
				return sig.SignatureType === typeItem.id;
			});
			if (match) {
				typeItem.SignatureId = match.SignatureId || 0;
				typeItem.PhotoPath = match.PhotoPath || '';
			}
		});

		// Map the SignatureTypeList to include PhotoPath and SignatureType
		var listToSave = $scope.SignatureTypeList.map(function (item) {
			return {
				SignatureId: item.SignatureId || 0,
				PhotoPath: item.PhotoPath || '',
				SignatureType: item.id,
				Image_TMP: item.Image_TMP
			};
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveUpdateSignature",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				var find = 0;
				angular.forEach(data.jsonData, function (sm) {
					if (sm.Image_TMP) {
						formData.append("file" + find, sm.Image_TMP[0]);
					}
					find++;
				});

				return formData;
			},

			data: { jsonData: listToSave }

		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	};


	$scope.SaveUpdateCTSignature = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		// Prepare a list with required fields including TranId
		var listToSave2 = $scope.ClassTeacherList.map(function (item) {
			return {
				TranId: item.TranId || 0,
				TeacherId: item.TeacherId,
				ClassName: item.ClassName,
				SectionName: item.SectionName,
				EmployeeName: item.EmployeeName,
				PhotoPath: item.PhotoPath || '',
				Photo_TMP: item.Photo_TMP
			};
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveUpdateCTSignature",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				var find = 0;
				angular.forEach(data.jsonData, function (sm) {
					if (sm.Photo_TMP && sm.Photo_TMP.length > 0) {
						formData.append("file" + find, sm.Photo_TMP[0]);
					}
					find++;
				});

				return formData;
			},

			data: { jsonData: listToSave2 }

		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire('Failed to save class teacher signatures.');
		});
	};


	$scope.SaveUpdateUserSignature = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		// Prepare a list with required fields including TranId
		var listToSave1 = $scope.UserList.map(function (item) {
			return {
				UserSignatureId: item.UserSignatureId || 0,
				UserId: item.UserId,
				Photo_TMP: item.Photo_TMP
			};
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveUpdateUS",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				var find = 0;
				angular.forEach(data.jsonData, function (sm) {
					if (sm.Photo_TMP && sm.Photo_TMP.length > 0) {
						formData.append("file" + find, sm.Photo_TMP[0]);
					}
					find++;
				});

				return formData;
			},

			data: { jsonData: listToSave1 }

		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire('Failed to save class User signatures.');
		});
	};
	//DOne: For Photo Status
	$scope.GetAllPhotoStatus = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentPhotoStatus = [];
		$scope.EmployeePhotoStatus = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllPhotoStatus",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentPhotoStatus = res.data.Data.studentPhotoStatusColl;
				$scope.EmployeePhotoStatus = res.data.Data.employeePhotoStatusColl;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});