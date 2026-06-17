app.controller('StudentEligibilityController', function ($scope, $http, $timeout, $filter, GlobalServices, $rootScope, $translate) {
	$scope.Title = 'Student Eligibility';

	var glbS = GlobalServices;

	$rootScope.ConfigFunction = function () {
		var Labels = {
			RegdNo: '',
			Cast: '',
		};
		$translate('REGDNO_LNG').then(function (data) {
			Labels.RegdNo = data;
		});
		$translate('CAST_LNG').then(function (data) {
			Labels.Cast = data;
		});
	};
	$rootScope.ChangeLanguage();


	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.LanguageColl = GlobalServices.getLangList();
		$scope.GenderColl = GlobalServices.getGenderList();

		$scope.BloodGroupList = GlobalServices.getBloodGroupList();
		$scope.ReligionList = GlobalServices.getReligionList();
		$scope.CountryList = GlobalServices.getCountryList();
		$scope.DisablityList = GlobalServices.getDisablityList();

		$scope.ExaminationModeColl = [{ id: 1, text: 'Online' }, { id: 2, text: 'Offline' }];

		$scope.StudentUserAsPerList = [
			{ id: 1, text: 'AutoNumber' },
			{ id: 2, text: 'Registration No.' },
			{ id: 3, text: 'FirstName' },
			{ id: 4, text: 'AutoNumber+FirstName' },
			{ id: 5, text: 'FirstName+AutoNumber' },
			{ id: 6, text: 'Registration No.+FirstName' },
			{ id: 7, text: 'FirstName+Registration No.' },
			{ id: 8, text: 'ContactNo' },
			{ id: 9, text: 'FatherContactNo' }
		];

		$scope.currentPages = {
			Eligibility: 1,
			ExamDetails: 1,
			UserCredential: 1,
			StudentUsers:1,
		};

		$scope.searchData = {
			Eligibility: '',
			ExamDetails: '',
			UserCredential: '',
			StudentUsers: '',
			ExamList:'',
		};

		$scope.perPage = {
			Eligibility: GlobalServices.getPerPageRow(),
			ExamDetails: GlobalServices.getPerPageRow(),
			UserCredential: GlobalServices.getPerPageRow(),
			StudentUsers: GlobalServices.getPerPageRow(),
			ExamList: GlobalServices.getPerPageRow(),
		};

		$scope.newStudentUsers = {
			AsPer: 1,
			canUpdate: false,
			Prefix:'',
			Suffix: ''
		};

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newEligibility = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date()
		};

		var smsPara = {
			EntityId: entityStudentUserSMS,
			ForATS: 3,
			TemplateType: 1
		};
		$scope.SMSTemplatesColl = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(smsPara)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SMSTemplatesColl = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		smsPara.TemplateType = 2;
		$scope.EmailTemplatesColl = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(smsPara)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmailTemplatesColl = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.CurSMSSend = {
			Temlate: {},
			Description: '',
			Primary: true,
			Father: false,
			Mother: false,
			Guardian: false,
			DataColl: []
		};

		$scope.CurEmailSend = {
			Temlate: {},
			Description: '',
			Subject: '',
			Title: '',
			Primary: true,
			Father: false,
			Mother: false,
			Guardian: false,
			CC: '',
			DataColl: []
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


		$scope.ShiftList = [];
		GlobalServices.getClassShiftList().then(function (res) {
			$scope.ShiftList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.CasteList = [];
		GlobalServices.getCasteList().then(function (res) {
			$scope.CasteList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.StudentTypeList = [];
		GlobalServices.getStudentTypeList().then(function (res) {
			$scope.StudentTypeList = res.data.Data;
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

		$scope.DepartmentList = [];
		GlobalServices.getDepartmentList().then(function (res) {
			$scope.DepartmentList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ExamTypeColl = [];
		GlobalServices.getExamTypeList().then(function (res) {
			$scope.ExamTypeColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		GlobalServices.getSubjectList().then(function (res) {
			$scope.SubjectList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		$scope.DocumentTypeList = [];
		GlobalServices.getDocumentTypeList().then(function (res) {
			$scope.DocumentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.StatusColl = [{ id: 1, text: 'Eligible' }, { id: 2, text: 'InEligible' }];

		//Academic Year Wise Period
		$scope.AYPeriod = {};
		GlobalServices.getAcademicPeriod().then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AYPeriod = res.data.Data;

				$scope.newEligibility.FromDate_TMP = new Date($scope.AYPeriod.StartDate);
				$scope.newEligibility.ToDate_TMP = new Date();

				$scope.GetRegSummaryList();
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AllEmployeeColl = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllEmpShortList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.AllEmployeeColl = res.data.Data;

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllExamList();
		
	}

	$scope.GetAllExamList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamList = []; 
		$http({
			method: 'POST',
			url: base_url + "elearning/creation/GetOnlineExamListForPS",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.Data) {
				$scope.ExamList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	function OnClickDefault() {
		document.getElementById('detail').style.display = "none";
		document.getElementById('entranceexamdetail').style.display = "none";
		document.getElementById('backbtnone').onclick = function () {
			document.getElementById('entranceexamdetail').style.display = "none";
			document.getElementById('entrancetablesection').style.display = "block";
		}

		//document.getElementById('examdetail').onclick = function () {
		//	document.getElementById('tablepart').style.display = "none";
		//	document.getElementById('form-part').style.display = "block";
		//}
		document.getElementById('back-btn').onclick = function () {
			document.getElementById('detail').style.display = "none";
			document.getElementById('tablepart').style.display = "block";
		}

	

		//document.getElementById('addentrancedetail').onclick = function () {
		//	document.getElementById('entrancetablesection').style.display = "none";
		//	document.getElementById('entranceexamdetail').style.display = "block";
		//}
		
		//document.getElementById('back-detail-btn').onclick = function () {

		//	document.getElementById('detail').style.display = "block";
		//	document.getElementById('check').style.display = "none";
		//}
		
	}

	$scope.GetAllStudentUsersList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentUsersList = [];

		var para = {
			ClassId: $scope.newStudentUsers.ClassId
		};

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/creation/GetStudentUserList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentUsersList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetStudentUsersById = function (refData) {

		refData.NewPwd = refData.Pwd;
		refData.CanEdit = true;
	};
	$scope.UpdateStudentPwd = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			UserId: refData.UserId,
			OldPwd: refData.Pwd,
			NewPwd: refData.NewPwd,
			UserName: refData.UserName
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/UpdatePwd",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess) {
				$timeout(function () {
					refData.Pwd = refData.NewPwd;
					refData.CanEdit = false;
				});
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.StudentForlogout = function (st) {
		var stColl = [];
		stColl.push(st);

		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/SendNotificationToForceLogOut",
			dataType: "json",
			data: JSON.stringify(stColl)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.SendNotificationToForceLogOut = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/SendNotificationToForceLogOut",
			dataType: "json",
			data: JSON.stringify($scope.StudentUsersList)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GenerateStudentUser = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AsPer: $scope.newStudentUsers.AsPer,
			canUpdate: $scope.newStudentUsers.canUpdate,
			Prefix: $scope.newStudentUsers.Prefix,
			Suffix: $scope.newStudentUsers.Suffix
		};

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/GenerateStudentUser",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.SMSTemplateChanged = function (st) {
		$scope.CurSMSSend.Temlate = st;
		$scope.CurSMSSend.Description = st.Description;
	}
	$scope.EmailTemplateChanged = function (st) {
		$scope.CurEmailSend.Temlate = st;
		$scope.CurEmailSend.Title = st.Title;
		$scope.CurEmailSend.Subject = st.Title;
		$scope.CurEmailSend.CC = st.CC;
		$scope.CurEmailSend.Description = st.Description;
	}
	$scope.SendSMSIndivisual = function (beData) {
		$scope.CurSMSSend = {
			Temlate: {},
			Description: '',
			Primary: true,
			Father: false,
			Mother: false,
			Guardian: false,
			DataColl: []
		};
		$scope.CurSMSSend.DataColl.push(beData);

		$('#sendsms').modal('show');
	};
	$scope.SendEmailIndivisual = function (beData) {


		myDropzone.removeAllFiles();

		$scope.CurEmailSend = {
			Temlate: {},
			Description: '',
			Subject: '',
			Title: '',
			Primary: true,
			Father: false,
			Mother: false,
			Guardian: false,
			CC: '',
			DataColl: [],
		};
		$scope.CurEmailSend.DataColl.push(beData);

		if ($scope.CurEmailSend.DataColl.length == 0) {
			Swal.fire('Please ! Select Data From List To Send Email')
			return;
		} else {
			$('#sendemail').modal('show');
		}

	}
	$scope.ShowSMSDialog = function () {
		$scope.CurSMSSend = {
			Temlate: {},
			Description: '',
			Primary: true,
			Father: false,
			Mother: false,
			Guardian: false,
			DataColl: []
		};

		angular.forEach($scope.EligibleStudentColl, function (es) {
			if(es.IsSelected==true)
				$scope.CurSMSSend.DataColl.push(es);
		});
		

		if ($scope.CurSMSSend.DataColl.length == 0) {
			Swal.fire('Please ! Select Data From List To Send SMS')
			return;
		} else {
			$('#sendsms').modal('show');
		}

	};

	$scope.SendSMS = function () {
		if ($scope.CurSMSSend && $scope.CurSMSSend.DataColl && $scope.CurSMSSend.DataColl.length > 0) {

			var smsColl = [];
			angular.forEach($scope.CurSMSSend.DataColl, function (objEntity) {
				var contactNoColl = [];
				if ($scope.CurSMSSend.Primary == true) {

					if (objEntity.ContactNo && objEntity.ContactNo.length > 0)
						contactNoColl.push(objEntity.ContactNo);
				}
				if ($scope.CurSMSSend.Father == true) {
					if (objEntity.F_ContactNo && objEntity.F_ContactNo.length > 0)
						contactNoColl.push(objEntity.F_ContactNo);
				}

				if ($scope.CurSMSSend.Mother == true) {
					if (objEntity.M_ContactNo && objEntity.M_ContactNo.length > 0)
						contactNoColl.push(objEntity.M_ContactNo);
				}

				if ($scope.CurSMSSend.Guardian == true) {
					if (objEntity.G_Contact && objEntity.G_Contact.length > 0)
						contactNoColl.push(objEntity.G_Contact);
				}

				if (contactNoColl.length > 0) {
					var msg = $scope.CurSMSSend.Description;
					for (let x in objEntity) {
						var variable = '$$' + x.toLowerCase() + '$$';
						if (msg.indexOf(variable) >= 0) {
							var val = objEntity[x];
							msg = msg.replace(variable, val);
						}

						if (msg.indexOf('$$') == -1)
							break;
					}

					var newSMS = {
						EntityId: entityStudentUserSMS,
						StudentId: objEntity.TranId,
						UserId: 0,
						Title: $scope.CurSMSSend.Temlate.Title,
						Message: msg,
						ContactNo: contactNoColl.toString(),
						StudentName: objEntity.Name
					};
					smsColl.push(newSMS);
				}

			});

			if (smsColl.length > 0) {

				$scope.loadingstatus = "running";
				showPleaseWait();
				$http({
					method: 'POST',
					url: base_url + "Global/SendSMSToStudent",
					dataType: "json",
					data: JSON.stringify(smsColl)
				}).then(function (sRes) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(sRes.data.ResponseMSG);
					$('#sendsms').modal('hide');
				});
			}

		} else {
			Swal.fire('No Data found for sms');
		}
	};

	$scope.ShowEmailDialog = function () {

		myDropzone.removeAllFiles();

		$scope.CurEmailSend = {
			Temlate: {},
			Description: '',
			Subject: '',
			Title: '',
			Primary: true,
			Father: false,
			Mother: false,
			Guardian: false,
			CC: '',
			DataColl: [],
		};
		angular.forEach($scope.EligibleStudentColl, function (es) {
			if (es.IsSelected == true)
				$scope.CurEmailSend.DataColl.push(es);
		});

		$('#sendemail').modal('show');
	};

	$scope.SendEmail = function () {
		if ($scope.CurEmailSend && $scope.CurEmailSend.DataColl && $scope.CurEmailSend.DataColl.length > 0) {

			var filesColl = myDropzone.files;

			var ccColl = [];
			if ($scope.CurEmailSend.EmployeeColl && $scope.CurEmailSend.EmployeeColl.length > 0) {
				angular.forEach($scope.CurEmailSend.EmployeeColl, function (emp) {
					if (emp.EmailId && emp.EmailId.length > 0)
						ccColl.push(emp.EmailId);
				});
			}

			var emailDataColl = [];
			angular.forEach($scope.CurEmailSend.DataColl, function (objEntity) {
				var emailColl = [];
				if ($scope.CurEmailSend.Primary == true) {

					if (objEntity.Email && objEntity.Email.length > 0)
						emailColl.push(objEntity.Email);
				}
				if ($scope.CurEmailSend.Father == true) {
					if (objEntity.F_Email && objEntity.F_Email.length > 0)
						emailColl.push(objEntity.F_Email);
				}

				if ($scope.CurEmailSend.Mother == true) {
					if (objEntity.M_Email && objEntity.M_Email.length > 0)
						emailColl.push(objEntity.M_Email);
				}

				if ($scope.CurEmailSend.Guardian == true) {
					if (objEntity.G_Email && objEntity.G_Email.length > 0)
						emailColl.push(objEntity.G_Email);
				}

				if (emailColl.length > 0) {
					var msg = $scope.CurEmailSend.Description;
					for (let x in objEntity) {
						var variable = '$$' + x.toLowerCase() + '$$';
						if (msg.indexOf(variable) >= 0) {
							var val = objEntity[x];
							msg = msg.replace(variable, val);
						}

						if (msg.indexOf('$$') == -1)
							break;
					}

					var paraColl = [];
					paraColl.push({ Key: 'StudentId', Value: objEntity.StudentId });

					var newEmail = {
						EntityId: entityStudentUserSMS,
						StudentId: objEntity.TranId,
						UserId: 0,
						Title: $scope.CurEmailSend.Temlate.Title,
						Subject: $scope.CurEmailSend.Subject,
						Message: msg,
						CC: ccColl.toString(),
						To: emailColl.toString(),
						StudentName: objEntity.Name,
						ParaColl: paraColl,
						FileName: 'enquiry-form'
					};
					emailDataColl.push(newEmail);
				}

			});

			if (emailDataColl.length > 0) {


				$scope.loadingstatus = "running";
				showPleaseWait();

				$http({
					method: 'POST',
					url: base_url + "Global/SendEmailToStudent",
					headers: { 'Content-Type': undefined },

					transformRequest: function (data) {

						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));

						if (data.files) {
							for (var i = 0; i < data.files.length; i++) {

								if (data.files[i].File)
									formData.append("file" + i, data.files[i].File);
								else
									formData.append("file" + i, data.files[i]);
							}
						}

						return formData;
					},
					data: { jsonData: emailDataColl, files: filesColl }
				}).then(function (res) {

					$scope.loadingstatus = "stop";
					hidePleaseWait();

					Swal.fire(res.data.ResponseMSG);

				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					$('#sendemail').modal('hide');
				});

			}

		} else {
			Swal.fire('No Data found for sms');
		}
	};

	$scope.chkCheckAll = false;
	$scope.CheckUnCheckAll = function () {
		angular.forEach($scope.EligibleStudentColl, function (es) {
			es.IsSelected = $scope.chkCheckAll;
		});
    }
	$scope.GetRegSummaryList = function () {
		if (!$scope.newEligibility.FromDate_TMP || !$scope.newEligibility.FromDateDet || !$scope.newEligibility.FromDateDet.dateAD) {
			Swal.fire('Please select From Date');
			return;
		}

		// Validate To Date
		if (!$scope.newEligibility.ToDate_TMP || !$scope.newEligibility.ToDateDet || !$scope.newEligibility.ToDateDet.dateAD) {
			Swal.fire('Please select To Date');
			return;
		}

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EligibleStudentColl = [];
		if ($scope.newEligibility.ToDateDet && $scope.newEligibility.FromDateDet) {
			var para = {
				dateFrom: $filter('date')(new Date($scope.newEligibility.FromDateDet.dateAD), 'yyyy-MM-dd'),
				dateTo: $filter('date')(new Date($scope.newEligibility.ToDateDet.dateAD), 'yyyy-MM-dd')
			}
			$http({
				method: 'POST',
				url: base_url + "AdmissionManagement/Creation/getRegSummaryForEligible",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess) {
					$scope.EligibleStudentColl= res.data.Data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}
	$scope.GetRegistrationById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			StudentId: refData.TranId
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


				$timeout(function () {
					$scope.newDet = res.data.Data;

					$scope.SelectedClass = mx($scope.ClassList).firstOrDefault(p1 => p1.ClassId == $scope.newDet.ClassId);

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
					  
					if ($scope.newDet.AdmitDate)
						$scope.newDet.AdmitDate_TMP = new Date($scope.newDet.AdmitDate);

					if ($scope.newDet.DOB_AD)
						$scope.newDet.DOB_TMP = new Date($scope.newDet.DOB_AD);

					if ($scope.newDet.Eligibility) {
						if ($scope.newDet.Eligibility.ExamDate)
							$scope.newDet.Eligibility.ExamDate_TMP = new Date($scope.newDet.Eligibility.ExamDate);
                    }

					document.getElementById('entrancetablesection').style.display = "none";
					document.getElementById('entranceexamdetail').style.display = "block";
					

				});


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newDet.EligibilityAttachmentColl) {
			if ($scope.newDet.EligibilityAttachmentColl.length > 0) {
				$scope.newDet.EligibilityAttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newDet.EligibilityAttachmentColl.push({
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

	$scope.CallSaveUpdateRegEligible = function () {

		if (!$scope.newDet.Eligibility)
			return;

		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newDet.EligibilityAttachmentColl;
		$scope.newDet.Eligibility.AttachmentColl = $scope.newDet.EligibilityAttachmentColl;

		if ($scope.newDet.Eligibility.ExamDateDet) {
			$scope.newDet.Eligibility.ExamDate = $filter('date')(new Date($scope.newDet.Eligibility.ExamDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newDet.Eligibility.ExamDate = null;
		   
		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/SaveRegEligible",
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
			data: { jsonData: $scope.newDet.Eligibility, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.newDet = {};
				document.getElementById('entranceexamdetail').style.display = "none";
				document.getElementById('entrancetablesection').style.display = "block";
            }

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};


	$scope.CurExamSetup = null;
	$scope.CurStudent = null;
	$scope.GetStudentList = function (examDet) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CurExamSetup = examDet;
		$scope.CurStudent = null;
		$scope.PresentStudentList = [];
		$scope.AbsentStudentList = [];

		var para = {
			examSetupId: examDet.ExamSetupId,
			classId: examDet.ClassId,
			sectionId: examDet.SectionId
		};

		$http({
			method: 'POST',
			url: base_url + "elearning/creation/GetStudentForEvaluate",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.Data) {
				var tmpDataColl = res.data.Data;
				angular.forEach(tmpDataColl, function (dc) {
					if (dc.QuestionAttampt && dc.QuestionAttampt > 0)
						$scope.PresentStudentList.push(dc);
					else
						$scope.AbsentStudentList.push(dc);
				});

				$scope.CurExamSetup.TotalStudent = tmpDataColl.length;
				$scope.CurExamSetup.TotalPresent = $scope.PresentStudentList.length;
				$scope.CurExamSetup.TotalAbsent = $scope.AbsentStudentList.length;

				document.getElementById('tablepart').style.display = "none";
				document.getElementById('detail').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	$scope.ChangeObtMark = function () {
		var fm = $scope.newDet.Eligibility.FullMark;
		var pm = $scope.newDet.Eligibility.PassMark;
		var om = $scope.newDet.Eligibility.ObtainMark;
		var per = 0;

		if (fm < pm) {
			Swal.fire('Please ! Enter Passmark less than fullmark');
			$scope.newDet.Eligibility.PassMark = 0;
			return;
		}

		if (om > 0) {
			per = (om / fm) * 100;
			$scope.newDet.Eligibility.Percentage = per;
        }
	}

	$scope.validateDate = function (obj, startField, endField, startLabel, endLabel) {
		var res = GlobalServices.validateDate(obj, startField, endField, startLabel, endLabel);
		if (res.IsSuccess == false) {
			Swal.fire({
				icon: 'warning',
				text: res.Message,
				confirmButtonText: 'OK'
			}).then(function () {
				obj.FromDate_TMP = new Date();
				obj.ToDate_TMP = new Date();
				$scope.$applyAsync();
			});
		}
	};

});