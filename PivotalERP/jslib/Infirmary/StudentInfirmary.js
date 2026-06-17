app.controller('StudentInfirmaryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Test Name';
	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		/*$scope.MonthList = GlobalServices.getMonthList();*/
		$scope.ObservedAtList = [{ id: 1, text: 'School' }, { id: 2, text: 'Home' }, { id: 3, text: 'Other' }];
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.currentPages = {
			ClinicalEvaluation: 1,
			PastHistory: 1,
			HealthImmunization: 1,
			HealthIssues: 1,
			Immunization: 1,
			GeneralCheckup: 1,
		};

		$scope.searchData = {
			ClinicalEvaluation: '',
			PastHistory: '',
			HealthImmunization: '',
			CurrentMedication: '',
			HealthIssues: '',
			Immunization: '',
			GeneralCheckup: ''
		};

		$scope.perPage = {
			ClinicalEvaluation: GlobalServices.getPerPageRow(),
			PastHistory: GlobalServices.getPerPageRow(),
			HealthImmunization: GlobalServices.getPerPageRow(),
			HealthIssues: GlobalServices.getPerPageRow(),
			Immunization: GlobalServices.getPerPageRow(),
			GeneralCheckup: GlobalServices.getPerPageRow()
		};

		$scope.HealthIssuelist = [];
		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllHealthIssue",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HealthIssuelist = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.MedicalProductlist = [];
		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllMedicalProduct",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.MedicalProductlist = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ExaminerList = [];
		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllExaminer",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExaminerList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.VaccineList = [];
		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllVaccine",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.VaccineList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.TestNameList = [];
		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllTestName",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TestNameList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.DocumentTypeList = [];
		GlobalServices.getDocumentTypeList().then(function (res) {
			$scope.DocumentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.newDet = {
			SelectStudent: $scope.StudentSearchOptions[0].value,
		};

		////$scope.newClinical = {
		////	ClinicalId: null,
		////	Name: '',
		////	Description: '',
		////	OrderNo: 0,
		////	Mode: 'Save'
		////};

		////$scope.newPastHistory = {
		////	PastHistoryId: null,
		////	Name: '',
		////	ObservedDate_TMP: new Date(),
		////	Details: '',
		////	Prescription: '',
		////	MedicineTaken: true,
		////	//StudentPastMedicineDetColl: [],
		////	//StudentPastMedicalDocumentsColl: [],
		////	Mode: 'Save'
		////};
		//////$scope.newPastHistory.StudentPastMedicineDetColl.push({});

		////$scope.newHealthIssues = {
		////	PastHistoryId: null,
		////	MedicationDate: '',
		////	Description: '',
		////	IsAdmitted: false,
		////	//StudentMedicineGivenDetColl: [],
		////	//HealthIssueAttachmentColl: [],
		////	Mode: 'Save'
		////};
		//////$scope.newHealthIssues.StudentMedicineGivenDetColl.push({});


		////$scope.newImmunization = {
		////	TranId: null,
		////	HealthIssueId: null,
		////	VaccineId: null,
		////	VaccinationDate_TMP: new Date(),
		////	Remarks: '',
		////	VaccinatorId: null,
		////	//StudentImmunizationAttachmentColl: [],
		////	Mode: 'Save'
		////};

		////$scope.newLabEvaluation = {
		////	TranId: null,
		////	TestMethod: '',
		////	TestNameId: null,
		////	EvaluateDate_TMP: new Date(),
		////	Remarks: '',
		////	HealthIssueId: null,
		////	Mode: 'Save'
		////};

		////$scope.NewGCheckup = {
		////	TranId: null,
		////	TestNameId: null,
		////	Value: 0,
		////	CheckupDate_TMP: new Date(),
		////	Remarks: '',
		////	Mode: 'Save'
		////};
		////$scope.TestNameList = $scope.TestNameList.map(function (item) {
		////	return {
		////		Name: item.Name,  
		////		TestNameId: item.TestNameId,
		////		Value: 0           
		////	};
		////});

		////$scope.MedicalHistory = {
		////	PastHistoryId: null,
		////	Name: '',
		////	ObservedDate_TMP: new Date(),
		////	Details: '',
		////	Prescription: '',
		////	MedicineTaken: true,
		////	//StudentPastMedicineDetColl: [],
		////	//StudentPastMedicalDocumentsColl: [],
		////};

		////$scope.MedicalIssues = {
		////	PastHistoryId: null,
		////	MedicationDate: '',
		////	Description: '',
		////	//StudentMedicineGivenDetColl: [],
		////	//HealthIssueAttachmentColl: []
		////};
		//////$scope.HealthImmunizationList = {			
		//////	StudentImmunizationAttachmentColl: []
		//////};




		//$scope.GetAllPastHistoryList();
		//$scope.GetAllStudentHealthIssueList();
		//$scope.GetAllStudentImmunizationList();
		//$scope.GetAllStudentCLEvaluation();
		//$scope.GetAllClinicalList();
		//$scope.GetAllPastHistoryList();
	}


	function OnClickDefault() {
		document.getElementById('pastform').style.display = "none";
		document.getElementById('clinicalevaluationform').style.display = "none";
		document.getElementById('immunizationform').style.display = "none";
		document.getElementById('immunizationAtt').style.display = "none";
		document.getElementById('healthissueform').style.display = "none";
		document.getElementById('Bmiform').style.display = "none";

		document.getElementById('add-past-btn').onclick = function () {
			document.getElementById('pastlist').style.display = "none";
			document.getElementById('pastform').style.display = "block";
			$scope.ClearPastHistory();
		}
		document.getElementById('back-past-list').onclick = function () {
			document.getElementById('pastform').style.display = "none";
			document.getElementById('pastlist').style.display = "block";
		}

		document.getElementById('add-clinical-btn').onclick = function () {
			document.getElementById('evaluationlist').style.display = "none";
			document.getElementById('clinicalevaluationform').style.display = "block";
		}
		document.getElementById('back-clinical-list').onclick = function () {
			document.getElementById('clinicalevaluationform').style.display = "none";
			document.getElementById('evaluationlist').style.display = "block";
		}

		document.getElementById('add-immunization-btn').onclick = function () {
			document.getElementById('immunizationlist').style.display = "none";
			document.getElementById('immunizationform').style.display = "block";
		}
		document.getElementById('back-immunization-list').onclick = function () {
			document.getElementById('immunizationform').style.display = "none";
			document.getElementById('immunizationlist').style.display = "block";
		}
		document.getElementById('back-immunization-listS').onclick = function () {
			document.getElementById('immunizationAtt').style.display = "none";
			document.getElementById('immunizationlist').style.display = "block";
		}

		document.getElementById('add-issue-btn').onclick = function () {
			document.getElementById('healthissuelist').style.display = "none";
			document.getElementById('healthissueform').style.display = "block";
		}
		document.getElementById('back-issue-list').onclick = function () {
			document.getElementById('healthissueform').style.display = "none";
			document.getElementById('healthissuelist').style.display = "block";
			$scope.ClearHealthIssue();
		}

		document.getElementById('add-bmi-btn').onclick = function () {
			document.getElementById('bmilist').style.display = "none";
			document.getElementById('Bmiform').style.display = "block";
		}
		document.getElementById('back-bmi-list').onclick = function () {
			document.getElementById('Bmiform').style.display = "none";
			document.getElementById('bmilist').style.display = "block";
		}
	}

	$scope.ClearClinical = function () {
		$scope.newSource = {
			ClinicalId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};
	}
	$scope.ClearPastHistory = function () {
		$scope.newPastHistory = {
			PastHistoryId: null,
			Name: '',
			ObservedDate_TMP: new Date(),
			Details: '',
			Prescription: '',
			MedicineTaken: true,
			StudentPastMedicineDetColl: [],
			StudentPastMedicalDocumentsColl: [],
			Mode: 'Save'
		};
		$scope.newPastHistory.StudentPastMedicineDetColl.push({});
	}

	$scope.ClearHealthIssue = function () {
		$scope.newHealthIssues = {
			PastHistoryId: null,
			MedicationDate: '',
			Description: '',
			IsAdmitted: false,
			StudentMedicineGivenDetColl: [],
			HealthIssueAttachmentColl: [],
			Mode: 'Save'
		};
		$scope.newHealthIssues.StudentMedicineGivenDetColl.push({});
	}


	$scope.ClearHealthImmunization = function () {
		$scope.newImmunization = {
			TranId: null,
			HealthIssueId: null,
			VaccineId: null,
			VaccinationDate_TMP: new Date(),
			Remarks: '',
			VaccinatorId: null,
			StudentImmunizationAttachmentColl: [],
			Mode: 'Save'
		};
	}

	$scope.ClearGeneralCheckup = function () {
		$scope.NewGCheckup = {
			TranId: null,
			Value: 0,
			CheckupDate_TMP: new Date(),
			Remarks: '',
			
			Mode: 'Save'
		};
		$scope.TestNameList = $scope.TestNameList.map(function (item) {
			return {
				Name: item.Name,   // Keep the name
				TestNameId: item.TestNameId,
				Value: null           // Reset the value to 0 or whatever default is
			};
		});
	}
	$scope.ClearValue = function () {
		$scope.Ph = {
			TranId: null,
			Value: 0
		};
	}


	$scope.ClearSCLEvaluation = function () {
		$scope.newLabEvaluation = {
			TranId: null,
			TestMethod: '',
			TestNameId: null,
			EvaluateDate_TMP: new Date(),
			Remarks: '',
			HealthIssueId: null,
			Mode: 'Save'
		};
	}

	$scope.ChangeStudent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			StudentId: $scope.newDet.StudentId
		};
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/getStudentDetForInfirmarybyId",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				let studentData = res.data.Data;

				// Preserve SelectStudent value
				studentData.SelectStudent = $scope.newDet.SelectStudent;

				// Assign student data without losing existing properties
				$scope.newDet = Object.assign({}, $scope.newDet, studentData);

				if (!$scope.newDet.PhotoPath) {
					$scope.newDet.PhotoPath = '/wwwroot/dynamic/images/avatar-img.jpg';
				}
				if ($scope.newDet.DOB_AD) {
					let dob = new Date($scope.newDet.DOB_AD);
					let today = new Date();
					let ageYears = today.getFullYear() - dob.getFullYear();
					let ageMonths = today.getMonth() - dob.getMonth();
					let ageDays = today.getDate() - dob.getDate();

					if (ageDays < 0) {
						ageMonths--;
						let previousMonth = new Date(today.getFullYear(), today.getMonth(), 0);
						ageDays += previousMonth.getDate();
					}
					if (ageMonths < 0) {
						ageYears--;
						ageMonths += 12;
					}
					$scope.newDet.Age = `${ageYears} Years, ${ageMonths} Months, ${ageDays} Days`;
				}

				$scope.GetMedicalHistoryById($scope.newDet.StudentId);
				$scope.getMedicalIssuesById($scope.newDet.StudentId);
				$scope.GetStudentGeneralCheckup($scope.newDet.StudentId);
				$scope.GetStudentHealthImmunization($scope.newDet.StudentId);
				$scope.GetClinicalLabEvaluation($scope.newDet.StudentId);

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};



	//*************************Past Medical History *********************************
	$scope.AddMedicineDetailsPastHistory = function (ind) {
		if ($scope.newPastHistory.StudentPastMedicineDetColl) {
			if ($scope.newPastHistory.StudentPastMedicineDetColl.length > ind + 1) {
				$scope.newPastHistory.StudentPastMedicineDetColl.splice(ind + 1, 0, {
					FormerLocalLevelName: ''
				})
			} else {
				$scope.newPastHistory.StudentPastMedicineDetColl.push({
					FormerLocalLevelName: ''
				})
			}
		}
	};

	$scope.DelMedicineDetailsPastHistory = function (ind) {
		if ($scope.newPastHistory.StudentPastMedicineDetColl) {
			if ($scope.newPastHistory.StudentPastMedicineDetColl.length > 1) {
				$scope.newPastHistory.StudentPastMedicineDetColl.splice(ind, 1);
			}
		}
	};

	$scope.delAttachmentDoc = function (ind) {
		if ($scope.newPastHistory.StudentPastMedicalDocumentsColl) {
			if ($scope.newPastHistory.StudentPastMedicalDocumentsColl.length > 0) {
				$scope.newPastHistory.StudentPastMedicalDocumentsColl.splice(ind, 1);
			}
		}
	};

	$scope.AddMoreFilesReceived = function (files, docType, des) {
		if (files && docType) {
			if (files != null && docType != null) {
				angular.forEach(files, function (file) {
					$scope.newPastHistory.StudentPastMedicalDocumentsColl.push({
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
				$('#flMoreFiles').val('');

			}
		}
	};

	$scope.IsValidPastHistory = function () {
		if ($scope.newPastHistory.Prescription.isEmpty()) {
			Swal.fire('Please ! Enter PastHistory Prescription');
			return false;
		}
		return true;
	}
	$scope.SaveUpdatePastMedicalHistory = function () {
		if ($scope.IsValidPastHistory() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newPastHistory.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdatePastMedicalHistory();
					}
				});
			} else
				$scope.CallSaveUpdatePastMedicalHistory();
		}
	};


	$scope.CallSaveUpdatePastMedicalHistory = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		/*var UserPhoto = $scope.newHealthExaminer.Photo_TMP;*/
		var filesColl = $scope.newPastHistory.StudentPastMedicalDocumentsColl;

		$scope.newPastHistory.StudentId = $scope.newDet.StudentId;

		if ($scope.newPastHistory.ObservedDateDet) {
			$scope.newPastHistory.ObservedDate = $filter('date')(new Date($scope.newPastHistory.ObservedDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newPastHistory.ObservedDate = $filter('date')(new Date(), 'yyyy-MM-dd');


		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/SaveUpdateStudentPMHistory",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				//if (data.UsPhoto && data.UsPhoto.length > 0)
				//	formData.append("UserPhoto", data.UsPhoto[0]);

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}
				return formData;
			},
			data: { jsonData: $scope.newPastHistory, files: filesColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearPastHistory();
				$scope.GetAllPastHistoryList();
				$scope.GetMedicalHistoryById($scope.newPastHistory.StudentId);
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}


	$scope.GetAllPastHistoryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PastMedicalHistoryList = [];
		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllPastMedicalHistoryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PastMedicalHistoryList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetPastHistoryById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/getStudentPastMedicalHistoryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newPastHistory = res.data.Data;

				if (!$scope.newPastHistory.StudentPastMedicineDetColl || $scope.newPastHistory.StudentPastMedicineDetColl.length == 0) {
					$scope.newPastHistory.StudentPastMedicineDetColl = [];
					$scope.newPastHistory.StudentPastMedicineDetColl.push({});
				}

				if ($scope.newPastHistory.ObservedDate)
					$scope.newPastHistory.ObservedDate_TMP = new Date($scope.newPastHistory.ObservedDate);

				$scope.newPastHistory.Mode = 'Modify';

				document.getElementById('pastlist').style.display = "none";
				document.getElementById('pastform').style.display = "block";

				if ($scope.newPastHistory.StudentId)
					$scope.newDet.StudentId = $scope.newPastHistory.StudentId;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelPastHistoryById = function (refData) {
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
					TranId: refData.TranId
				};

				$http({
					method: 'POST',
					url: base_url + "Infirmary/Creation/DeleteStudentPastMedicalHistoryById",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllPastHistoryList();
						$scope.GetMedicalHistoryById($scope.newPastHistory.StudentId);

					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};


	$scope.ShowMedicalDoc = function (item) {
		$scope.viewImg = {
			ContentPath: '',
			FileType: null
		};

		if (item.DocPath && item.DocPath.length > 0) {
			$scope.viewImg.ContentPath = item.DocPath;
			$scope.viewImg.FileType = 'pdf';  // Assuming DocPath is for PDFs
			document.getElementById('pdfMedDoc').src = item.DocPath;
			$('#MedicalDoc').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg.ContentPath = item.PhotoPath;
			$scope.viewImg.FileType = 'image';  // Assuming PhotoPath is for images
			$('#MedicalDoc').modal('show');
		} else if (item.File) {
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg.ContentPath = URL.createObjectURL(blob);
			$scope.viewImg.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

			if ($scope.viewImg.FileType === 'pdf') {
				document.getElementById('pdfMedDoc').src = $scope.viewImg.ContentPath;
			}

			$('#MedicalDoc').modal('show');
		} else {
			Swal.fire('No Image Found');
		}
	};


	//Prashant Code


	$scope.GetMedicalHistoryById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			StudentId: $scope.newDet.StudentId
		};
		$scope.MedicalHistory = [];
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/getStudentMedicalHistoryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.MedicalHistory = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.ShowPersonalImg = function (item) {
		$scope.viewImg = {
			ContentPath: '',
			FileType: null
		};

		if (item.DocPath && item.DocPath.length > 0) {
			$scope.viewImg.ContentPath = item.DocPath;
			$scope.viewImg.FileType = 'pdf';  // Assuming DocPath is for PDFs
			document.getElementById('pdfViewer').src = item.DocPath;
			$('#PersonalImg').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg.ContentPath = item.PhotoPath;
			$scope.viewImg.FileType = 'image';  // Assuming PhotoPath is for images
			$('#PersonalImg').modal('show');
		} else if (item.File) {
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg.ContentPath = URL.createObjectURL(blob);
			$scope.viewImg.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

			if ($scope.viewImg.FileType === 'pdf') {
				document.getElementById('pdfViewer').src = $scope.viewImg.ContentPath;
			}

			$('#PersonalImg').modal('show');
		} else {
			Swal.fire('No Image Found');
		}
	};
	
	$scope.ShowPersonalImg3 = function (item) {
		$scope.viewImg0 = {
			ContentPath: '',
			FileType: null
		};

		if (item.DocPath && item.DocPath.length > 0) {
			$scope.viewImg0.ContentPath = item.DocPath;
			$scope.viewImg0.FileType = 'pdf';  // Assuming DocPath is for PDFs
			document.getElementById('pdfViewer0').src = item.DocPath;
			$('#PersonalImg3').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg0.ContentPath = item.PhotoPath;
			$scope.viewImg0.FileType = 'image';  // Assuming PhotoPath is for images
			$('#PersonalImg3').modal('show');
		} else if (item.File) {
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg0.ContentPath = URL.createObjectURL(blob);
			$scope.viewImg0.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

			if ($scope.viewImg0.FileType === 'pdf') {
				document.getElementById('pdfViewer0').src = $scope.viewImg0.ContentPath;
			}

			$('#PersonalImg3').modal('show');
		} else {
			Swal.fire('No Image Found');
		}
	};



	$scope.getMedicalIssuesById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			StudentId: $scope.newDet.StudentId
		};
		$scope.MedicalIssues = [];
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/getStudentMedicalIssuesById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.MedicalIssues = res.data.Data;
				//$scope.MedicalIssues = res.data.Data.StudentMedicineGivenDetColl;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.ShowAttImg = function (item) {
		$scope.viewImg1 = {
			ContentPath: '',
			FileType: null
		};

		if (item.DocPath && item.DocPath.length > 0) {
			$scope.viewImg1.ContentPath = item.DocPath;
			$scope.viewImg1.FileType = 'pdf';  // Assuming DocPath is for PDFs
			document.getElementById('pdfViewer1').src = item.DocPath;
			$('#PersonalImg1').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg1.ContentPath = item.PhotoPath;
			$scope.viewImg1.FileType = 'image';  // Assuming PhotoPath is for images
			$('#PersonalImg1').modal('show');
		} else if (item.File) {
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg1.ContentPath = URL.createObjectURL(blob);
			$scope.viewImg1.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

			if ($scope.viewImg1.FileType === 'pdf') {
				document.getElementById('pdfViewer1').src = $scope.viewImg1.ContentPath;
			}

			$('#PersonalImg1').modal('show');
		} else {
			Swal.fire('No Image Found');
		}
	};

	//Prashant Code End

	//**********************************Health Issue************************
	$scope.AddMedicineDetails = function (ind) {
		if ($scope.newHealthIssues.StudentMedicineGivenDetColl) {
			if ($scope.newHealthIssues.StudentMedicineGivenDetColl.length > ind + 1) {
				$scope.newHealthIssues.StudentMedicineGivenDetColl.splice(ind + 1, 0, {
					Name: ''
				})
			} else {
				$scope.newHealthIssues.StudentMedicineGivenDetColl.push({
					Name: ''
				})
			}
		}
	};

	$scope.delMedicineDetails = function (ind) {
		if ($scope.newHealthIssues.StudentMedicineGivenDetColl) {
			if ($scope.newHealthIssues.StudentMedicineGivenDetColl.length > 1) {
				$scope.newHealthIssues.StudentMedicineGivenDetColl.splice(ind, 1);
			}
		}
	};

	$scope.delHIAttachmentDoc = function (ind) {
		if ($scope.newHealthIssues.HealthIssueAttachmentColl) {
			if ($scope.newHealthIssues.HealthIssueAttachmentColl.length > 0) {
				$scope.newHealthIssues.HealthIssueAttachmentColl.splice(ind, 1);
			}
		}
	};

	$scope.AddMoreHIssuesFiles = function (files1, docType1, des1) {
		if (files1 && docType1) {
			if (files1 != null && docType1 != null) {
				angular.forEach(files1, function (file) {
					$scope.newHealthIssues.HealthIssueAttachmentColl.push({
						DocumentTypeId: docType1.DocumentTypeId,
						DocumentTypeName: docType1.Name,
						File: file,
						Name: file.name,
						Type: file.type,
						Size: file.size,
						Description: des1,
						Path: null
					});
				})

				$scope.docType1 = null;
				$scope.attachFile1 = null;
				$scope.docDescription1 = '';

				$('#flMoreFiles1').val('');
			}
		}
	};


	$scope.IsValidHealthISsue = function () {
		if ($scope.newHealthIssues.Prescription.isEmpty()) {
			Swal.fire('Please ! Enter  Prescription');
			return false;
		}

		return true;
	}
	$scope.SaveUpdateHealthIssues = function () {
		if ($scope.IsValidHealthISsue() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newHealthIssues.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateHealthIssues();
					}
				});
			} else
				$scope.CallSaveUpdateHealthIssues();
		}
	};

	$scope.CallSaveUpdateHealthIssues = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		/*var UserPhoto = $scope.newHealthExaminer.Photo_TMP;*/
		var filesColl = $scope.newHealthIssues.HealthIssueAttachmentColl;

		$scope.newHealthIssues.StudentId = $scope.newDet.StudentId;

		if ($scope.newHealthIssues.ObservedDateDet) {
			$scope.newHealthIssues.ObservedDate = $filter('date')(new Date($scope.newHealthIssues.ObservedDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newHealthIssues.ObservedDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		if ($scope.newHealthIssues.AdmittedDateDet) {
			$scope.newHealthIssues.AdmittedDate = $filter('date')(new Date($scope.newHealthIssues.AdmittedDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newHealthIssues.AdmittedDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		if ($scope.newHealthIssues.DischargedDateDet) {
			$scope.newHealthIssues.DischargedDate = $filter('date')(new Date($scope.newHealthIssues.DischargedDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newHealthIssues.DischargedDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		if ($scope.newHealthIssues.ObservedTime_TMP)
			$scope.newHealthIssues.ObservedTime = $scope.newHealthIssues.ObservedTime_TMP.toLocaleString();

		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/SaveUpdateStudentHealthIssue",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				//if (data.UsPhoto && data.UsPhoto.length > 0)
				//	formData.append("UserPhoto", data.UsPhoto[0]);
				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}
				return formData;
			},
			data: { jsonData: $scope.newHealthIssues, files: filesColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearHealthIssue();
				$scope.getMedicalIssuesById($scope.newHealthIssues.StudentId);
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllStudentHealthIssueList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentHealthIssueList = [];
		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllStudentHealthIssue",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentHealthIssueList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetHealthIssueById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/getStudentHealthIssueById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newHealthIssues = res.data.Data;

				if (!$scope.newHealthIssues.StudentMedicineGivenDetColl || $scope.newHealthIssues.StudentMedicineGivenDetColl.length == 0) {
					$scope.newHealthIssues.StudentMedicineGivenDetColl = [];
					$scope.newHealthIssues.StudentMedicineGivenDetColl.push({});
				}

				if ($scope.newHealthIssues.ObservedDate)
					$scope.newHealthIssues.ObservedDate_TMP = new Date($scope.newHealthIssues.ObservedDate);

				if ($scope.newHealthIssues.AdmittedDate)
					$scope.newHealthIssues.AdmittedDate_TMP = new Date($scope.newHealthIssues.AdmittedDate);

				if ($scope.newHealthIssues.DischargedDate)
					$scope.newHealthIssues.DischargedDate_TMP = new Date($scope.newHealthIssues.DischargedDate);


				if ($scope.newHealthIssues.ObservedTime)
					$scope.newHealthIssues.ObservedTime_TMP = new Date($scope.newHealthIssues.ObservedTime);

				$scope.newHealthIssues.Mode = 'Modify';

				document.getElementById('healthissuelist').style.display = "none";
				document.getElementById('healthissueform').style.display = "block";

				if ($scope.newHealthIssues.StudentId)
					$scope.newDet.StudentId = $scope.newHealthIssues.StudentId;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelHealthIssueById = function (refData) {
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
					TranId: refData.TranId
				};
				$http({
					method: 'POST',
					url: base_url + "Infirmary/Creation/DeleteStudentHealthIssueById",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStudentHealthIssueList();
						$scope.getMedicalIssuesById($scope.newHealthIssues.StudentId);

					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

	

	$scope.ShowHealthIssDoc = function (item) {
		$scope.viewImg = {
			ContentPath: '',
			FileType: null
		};

		if (item.DocPath && item.DocPath.length > 0) {
			$scope.viewImg.ContentPath = item.DocPath;
			$scope.viewImg.FileType = 'pdf';  // Assuming DocPath is for PDFs
			document.getElementById('pdfHealthIssDoc').src = item.DocPath;
			$('#HealthIssDoc').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg.ContentPath = item.PhotoPath;
			$scope.viewImg.FileType = 'image';  // Assuming PhotoPath is for images
			$('#HealthIssDoc').modal('show');
		} else if (item.File) {
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg.ContentPath = URL.createObjectURL(blob);
			$scope.viewImg.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

			if ($scope.viewImg.FileType === 'pdf') {
				document.getElementById('pdfHealthIssDoc').src = $scope.viewImg.ContentPath;
			}
			$('#HealthIssDoc').modal('show');
		} else {
			Swal.fire('No Image Found');
		}
	};

	//-----------Health Immunization--------------------------------------------------------
	$scope.delSIAttachmentDoc = function (ind) {
		if ($scope.newImmunization.StudentImmunizationAttachmentColl) {
			if ($scope.newImmunization.StudentImmunizationAttachmentColl.length > 0) {
				$scope.newImmunization.StudentImmunizationAttachmentColl.splice(ind, 1);
			}
		}
	};

	$scope.AddMoreImmunizationFiles = function (files2, docType2, des2) {
		if (files2 && docType2) {
			if (files2 != null && docType2 != null) {
				angular.forEach(files2, function (file) {
					$scope.newImmunization.StudentImmunizationAttachmentColl.push({
						DocumentTypeId: docType2.DocumentTypeId,
						DocumentTypeName: docType2.Name,
						File: file,
						Name: file.name,
						Type: file.type,
						Size: file.size,
						Description: des2,
						Path: null
					});
				})

				$scope.docType2 = null;
				$scope.attachFile2 = null;
				$scope.docDescription2 = '';

				$('#flMoreFiles2').val('');
			}
		}
	};


	$scope.IsValidStudentImmunization = function () {
		//if ($scope.newImmunization.Prescription.isEmpty()) {
		//	Swal.fire('Please ! Enter  Prescription');
		//	return false;
		//}

		return true;
	}
	$scope.SaveUpdateStudentImmunizations = function () {
		if ($scope.IsValidStudentImmunization() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newImmunization.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudentImmunizations();
					}
				});
			} else
				$scope.CallSaveUpdateStudentImmunizations();
		}
	};

	$scope.CallSaveUpdateStudentImmunizations = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var filesColl = $scope.newImmunization.StudentImmunizationAttachmentColl;

		$scope.newImmunization.StudentId = $scope.newDet.StudentId;

		if ($scope.newImmunization.VaccinationDateDet) {
			$scope.newImmunization.VaccinationDate = $filter('date')(new Date($scope.newImmunization.VaccinationDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newImmunization.VaccinationDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/SaveUpdateStudentImmunization",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}
				return formData;
			},
			data: { jsonData: $scope.newImmunization, files: filesColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearHealthImmunization();
				$scope.GetStudentHealthImmunization();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}


	$scope.GetStudentHealthImmunization = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			StudentId: $scope.newDet.StudentId
		};
		$scope.HealthImmunizationList = [];
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/GetStudentHealthImmunization",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HealthImmunizationList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.GetAtt = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$scope.HealthImmunization = [];
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/GetStudentHealthImmunizationDoc",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				if (res.data.Data.length === 0) {
					Swal.fire('No attachment found');
					return;
				}
				$scope.HealthImmunization = res.data.Data;
				document.getElementById('immunizationAtt').style.display = "block";
				document.getElementById('immunizationlist').style.display = "none";
				//$('#exampleModalToggle1').modal('show');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.ShowAttach = function (item) {
		$scope.viewImg2 = {
			ContentPath: '',
			FileType: null
		};

		if (item.DocPath && item.DocPath.length > 0) {
			$scope.viewImg2.ContentPath = item.DocPath;
			$scope.viewImg2.FileType = 'pdf';  // Assuming DocPath is for PDFs
			document.getElementById('pdfViewer2').src = item.DocPath;
			$('#exampleModalToggle2').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg2.ContentPath = item.PhotoPath;
			$scope.viewImg2.FileType = 'image';  // Assuming PhotoPath is for images
			$('#exampleModalToggle2').modal('show');
		} else if (item.File) {
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg2.ContentPath = URL.createObjectURL(blob);
			$scope.viewImg2.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

			if ($scope.viewImg2.FileType === 'pdf') {
				document.getElementById('pdfViewer2').src = $scope.viewImg2.ContentPath;
			}

			$('#exampleModalToggle2').modal('show');
		} else {
			Swal.fire('No Image Found');
		}
	};


	$scope.GetImmunizationById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/getStudentImmunizationId",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newImmunization = res.data.Data;
				if ($scope.newImmunization.VaccinationDate)
					$scope.newImmunization.VaccinationDate_TMP = new Date($scope.newImmunization.VaccinationDate);

				$scope.newImmunization.Mode = 'Modify';
				document.getElementById('immunizationlist').style.display = "none";
				document.getElementById('immunizationform').style.display = "block";

				if ($scope.newImmunization.StudentId)
					$scope.newDet.StudentId = $scope.newImmunization.StudentId;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelImmunizationById = function (refData) {
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
					TranId: refData.TranId
				};
				$http({
					method: 'POST',
					url: base_url + "Infirmary/Creation/DeleteStudentImmunizationById",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetStudentHealthImmunization();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};


	//*****************************Clinical Lab Evaluation Tab Starts*******************************************
	$scope.GetTestNameById = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TestNameId: $scope.newLabEvaluation.TestNameId
		};
		$scope.newLabEvaluation.LabValueList = [];
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/GetLabValueById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newLabEvaluation.LabValueList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.IsValidSCLEvaluation = function () {
		if ($scope.newLabEvaluation.TestMethod.isEmpty()) {
			Swal.fire('Please ! Enter Test Method');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateSCLEvaluation = function () {
		if ($scope.IsValidSCLEvaluation() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newLabEvaluation.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSCLEvaluation();
					}
				});
			} else
				$scope.CallSaveUpdateSCLEvaluation();
		}
	};

	$scope.CallSaveUpdateSCLEvaluation = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newLabEvaluation.StudentId = $scope.newDet.StudentId;

		if ($scope.newLabEvaluation.EvaluateDateDet) {
			$scope.newLabEvaluation.EvaluateDate = $filter('date')(new Date($scope.newLabEvaluation.EvaluateDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newLabEvaluation.EvaluateDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/SaveUpdateStudentCLEvaluation",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newLabEvaluation }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearSCLEvaluation();
				//$scope.GetAllStudentCLEvaluation();
				$scope.GetClinicalLabEvaluation();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}


	//$scope.GetAllStudentCLEvaluation = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	$scope.StudentLabEvaluationList = [];
	//	$http({
	//		method: 'GET',
	//		url: base_url + "Infirmary/Creation/GetAllStudentLabEvaluation",
	//		dataType: "json"
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess && res.data.Data) {
	//			$scope.StudentLabEvaluationList = res.data.Data;
	//		} else {
	//			Swal.fire(res.data.ResponseMSG);
	//		}
	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//}
	$scope.GetStudentLabEvaluationById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/getStudentLabEvaluationbyId",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newLabEvaluation = res.data.Data;

				if ($scope.newLabEvaluation.EvaluateDate)
					$scope.newLabEvaluation.EvaluateDate_TMP = new Date($scope.newLabEvaluation.EvaluateDate);

				$scope.newLabEvaluation.Mode = 'Save';
				document.getElementById('evaluationlist').style.display = "none";
				document.getElementById('clinicalevaluationform').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelStudentEvaluationById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Infirmary/Creation/DeleteStudentLabEvaluationById",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetAllStudentCLEvaluation();
						$scope.GetClinicalLabEvaluation();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	$scope.GetClinicalLabEvaluation = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			StudentId: $scope.newDet.StudentId
		};
		$scope.ClinicalLabEvaluationList = [];
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/GetStudentClinicalLabEvaluation",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClinicalLabEvaluationList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	//***********************General Checkup Starts***********************************
	$scope.SaveGeneralCheckup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dtColl = [];
		if ($scope.NewGCheckup.CheckupDateDet) {
			$scope.NewGCheckup.CheckupDate = $filter('date')(new Date($scope.NewGCheckup.CheckupDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.NewGCheckup.CheckupDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		$scope.TestNameList.forEach(function (ph) {
			if (ph.Value > 0 || ph.Value < 0) {
				dtColl.push({
					StudentId: $scope.newDet.StudentId,
					TestNameId: ph.TestNameId,
					Value: ph.Value,
					Remarks: $scope.NewGCheckup.Remarks,
					CheckupDate: $scope.NewGCheckup.CheckupDate
				});
			}
		})
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/SaveStudentGeneralCheckup",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: dtColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearGeneralCheckup();
				$scope.GetStudentGeneralCheckup();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}


	$scope.GetStudentGeneralCheckup = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			StudentId: $scope.newDet.StudentId
		};
		$scope.GeneralCheckupList = [];
		$scope.dtColl = [];
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/GetStudentGeneralCheckup",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.GeneralCheckupList = res.data.Data;
				const testName = {};
				$scope.GeneralCheckupList.forEach(item => {
					if (!testName[item.TestNameId]) {
						testName[item.TestNameId] = item.TestName;
					}
				});
				$scope.groupedData = Object.values($scope.GeneralCheckupList.reduce((acc, item) => {
					if (!acc[item.CheckupDate]) {
						acc[item.CheckupDate] = {
							TranId: item.TranId,
							CheckupDate: item.CheckupDate,
							CheckupDateBS: item.CheckupDateBS,
							Remarks: item.Remarks,
							tests: {}
						};
					}
					acc[item.CheckupDate].tests[item.TestNameId] = item.Value;
					return acc;
				}, {}));

				$scope.TestNameLists = Object.keys(testName).map(key => ({
					TestNameId: key,
					Name: testName[key]
				}));

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};



	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

	$scope.validateForDate = function (obj) {
		if (!obj.ObservedDateDet || !obj.ObservedDateDet.dateAD) {
			return true;
		}

		var forDate = $filter('date')(obj.ObservedDateDet.dateAD, 'yyyy-MM-dd');
		var today = $filter('date')(new Date(), 'yyyy-MM-dd');

		if (!forDate) return true;

		if (forDate > today) {
			Swal.fire({
				icon: 'warning',
				text: 'Date cannot be a future date.',
				confirmButtonText: 'OK'
			}).then(function () {
				$scope.$apply(function () {
					obj.ObservedDate_TMP = new Date();
					obj.ObservedDateDet = new Date();
				});
			});
			return false;
		}
		return true;
	}

});