app.controller('RegistrationController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, Excel, $translate, FileSaver) {
	$scope.Title = 'Registration';

	$scope.Title = 'Student';
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

		 
		if ($rootScope.LANG == 'in') {

			var findInd = -1;
			findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'DOB_BS' });
			if (findInd != -1)
				$scope.gridOptions.columnDefs.splice(findInd, 1);

			$scope.gridApi.grid.getColumn('DOB_AD').displayName = 'DOB';
		} else {
			$scope.gridApi.grid.getColumn('DOB_AD').displayName = 'DOB(A.D.)';
			$scope.gridApi.grid.getColumn('DOB_BS').visible = true;
			$scope.gridApi.grid.getColumn('DOB_BS').hide = false;
			$scope.gridApi.grid.getColumn('DOB_BS').colDef.visible = true;
		}
		$scope.gridApi.grid.refresh();

	};
	$rootScope.ChangeLanguage();
	
	let stream = null;
	let video = document.querySelector("#video");
	let canvas = document.querySelector('#canvas');
	$scope.takePhotoFromCamera = async function () {

		if ($scope.webCam.Start == true) {
			$scope.webCam.Start = false;
			 

			canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);
			$scope.newRegistration.PhotoData = canvas.toDataURL('image/jpeg');

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

	OnClickDefault();

	getterAndSetter();
	function getterAndSetter() {


		$scope.gridOptions = [];

		$scope.gridOptions = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,

			columnDefs: [
				{
					name: 'Action',
					enableHiding: false,
					enableFiltering: false,
					enableSorting: false,
					minWidth: 120,
					enableColumnResizing: false,
					cellClass: "overflow-visible",
					cellTemplate: '<a href="" class="p-1" title="Click For Edit" ng-click="grid.appScope.GetRegistrationById(row.entity)">' +
						'<i class="fas fa-edit text-info" aria-hidden="true"></i>' +
						'</a>' +
						'<a href="" class="p-1" title="Click For Re-Print" ng-click="grid.appScope.Print(row.entity.TranId)">' +
						'<i class="fas fa-eye text-secondary" aria-hidden="true"></i>' +
						'</a>' +
						'<a href="" class="p-1" title="Click For Delete" ng-click="grid.appScope.DelEnquiryById(row.entity)">' +
						'<i class="fas fa-trash-alt text-danger" aria-hidden="true"></i>' +
						'</a>' +
						'  <div class="dropdown dnld-file">' +
						'<button class= "btn btn-secondary btn-light btn-sm dropdown-toggle" type="button" ' +
						'id="dropdownMenuButton" data-toggle="dropdown" ' +
						'aria-haspopup="true" aria-expanded="false" >' +
						'<i class="fas fa-clipboard-list"></i>' +
						'                                                               </button >' +
						'<div class="dropdown-menu dropdown-menu-right"' +
						'aria-labelledby="dropdownMenuButton">' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.openAssignCounselor(row.entity)" ng-show="row.entity.IsAssignCounselor==false"> Assign Counselor</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.openFollowup(row.entity)" ng-hide="row.entity.Status==3 || row.entity.Status==5"> Followup</a>' +
						'<a class="dropdown-item mb-0" href="' + base_url + 'AdmissionManagement/Creation/Admission?AdmissionEnquiryId=0&RegistrationId={{row.entity.TranId}}" ng-hide="row.entity.Status==3 || row.entity.Status==7 || row.entity.Status==5 || grid.appScope.RegConfig.ActiveAdmission==false"> Proceed to Admission</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.closeFollowup(row.entity,3)" ng-show="row.entity.Status<3"> Hold Registration </a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.closeFollowup(row.entity,4)" ng-show="row.entity.Status==3"> Resume Registration </a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.closeFollowup(row.entity,5)" ng-hide="row.entity.Status>4 || row.entity.Status==3"> Reject Registration</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.closeFollowup(row.entity,8)" ng-hide="row.entity.Status==8 || row.entity.Status==3 || row.entity.Status==5 "> Approve</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.DownloadForm(row.entity.TranId)"> Download form</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.DownloadReceipt(row.entity.ReceiptTranId)" ng-show="row.entity.ReceiptTranId>0"> Download Receipt</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.SendSMSIndivisual(row.entity)"> Send SMS</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.SendEmailIndivisual(row.entity)"> Send Email</a>' +
						'</div>' +
						'</div >',
					pinned: 'left',
				},
				{ name: "SNo", displayName: "S.No.", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "StatusStr", displayName: "Status", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "AutoManualNo", displayName: "Reg. No.", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "EnqDate_BS", displayName: "Reg. Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 160, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Email", displayName: "Email", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassName", displayName: "For Class", minWidth: 140, headerCellClass: 'headerAligment' },

				/*{ name: "Department", displayName: "Department", minWidth: 110, headerCellClass: 'headerAligment' },*/
				{ name: "ClassShiftName", displayName: "Shift", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "Medium", displayName: "Medium", minWidth: 110, headerCellClass: 'headerAligment' },

				{
					name: "DOB_AD", displayName: "DOB(AD)", minWidth: 140, headerCellClass: 'headerAligment',
					cellTemplate: '<div>{{row.entity.DOB_AD |dateFormat}}</div>',
				},
				{ name: "DOB_BS", displayName: "DOB(BS)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BirthCertificateNo", displayName: "BirthCertificateNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Caste", displayName: "Caste", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Nationality", displayName: "Nationality", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Religion", displayName: "Religion", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "FatherName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "F_ContactNo", displayName: "FatherContact", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "F_Email", displayName: "F_Email", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "F_Profession", displayName: "F_Profession", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "MotherName", displayName: "MotherName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "M_ContactNo", displayName: "M_ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "M_Email", displayName: "M_Email", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "M_Profession", displayName: "M_Profession", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "GuardianName", displayName: "GuardianName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "G_Address", displayName: "G_Address", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "G_Contact", displayName: "G_Contact", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "G_Email", displayName: "G_Email", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "G_Professsion", displayName: "G_Professsion", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "G_Relation", displayName: "G_Relation", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PA_Province", displayName: "PA_Province", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PA_District", displayName: "PA_District", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PA_LocalLevel", displayName: "PA_LocalLevel", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PA_WardNo", displayName: "PA_WardNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PA_StreetName", displayName: "PA_StreetName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CA_Province", displayName: "TA_Province", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CA_District", displayName: "TA_District", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CA_LocalLevel", displayName: "TA_LocalLevel", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CA_WardNo", displayName: "TA_WardNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CA_StreetName", displayName: "TA_StreetName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PreviousSchool", displayName: "Previous School", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PreviousSchoolAddress", displayName: "Previous School Address", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PreviousClassGpa", displayName: "Previous Gpa", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "OptionalFirst", displayName: "Optional First", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "OptionalSecond", displayName: "Optional Second", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "Talent", displayName: "Talent", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "IsTransport", displayName: "IsTransport", minWidth: 100, headerCellClass: 'headerAligment' },
				//{ name: "IsHostel", displayName: "IsHostel", minWidth: 100, headerCellClass: 'headerAligment' },
				//{ name: "IsTiffin", displayName: "IsTiffin", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Source", displayName: "Source", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "ReferralCode", displayName: "Referral Code", minWidth: 100, headerCellClass: 'headerAligment' },
 
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'registrationSummary.csv',
			exporterPdfDefaultStyle: { fontSize: 9 },
			exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
			exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
				docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
				return docDefinition;
			},
			exporterPdfOrientation: 'portrait',
			exporterPdfPageSize: 'LETTER',
			exporterPdfMaxGridWidth: 500,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'regSummary.xlsx',
			exporterExcelSheetName: 'regSummary',
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
			}
		};


	};
	$scope.LoadData = function () {

		//GlobalServices.ChangeLanguage(); 
		$('.select2').select2();

		//Added by Suresh on Chaitra 10
		$scope.ProvinceColl = GetStateList();
		$scope.DistrictColl = GetDistrictList();
		$scope.VDCColl = GetVDCList();

		$scope.ProvinceColl_Qry = mx($scope.ProvinceColl);
		$scope.DistrictColl_Qry = mx($scope.DistrictColl);
		$scope.VDCColl_Qry = mx($scope.VDCColl);
       //Ends

		//Added By Suresh for country Configuration on baishakh 5
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
		//Ends

		$scope.webCam = {
			Start: false
		};

		$timeout(function () {
			$http({
				method: 'POST',
				url: base_url + "Fee/Transaction/GetFeeConfiguration",
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newFeeConfiguration = res.data.Data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});


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
		 

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.LanguageColl = GlobalServices.getLangList();
		$scope.GenderColl = GlobalServices.getGenderList();

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

		$scope.CommunicationTypeList = [];
		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/creation/GetAllCommunicationTypeList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CommunicationTypeList = res.data.Data;
			}
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
			Registration: 1,
			Followup: 1,
			TodaysFollowup: 1,
			PendingFollowup: 1,
			UpcomingFollowup: 1,
			FollowupNotRequired: 1
		};

		$scope.searchData = {
			Registration: '',
			Followup: '',
			TodaysFollowup: '',
			PendingFollowup: '',
			UpcomingFollowup: '',
			FollowupNotRequired: ''

		};

		$scope.perPage = {
			Registration: GlobalServices.getPerPageRow(),
			Followup: GlobalServices.getPerPageRow(),
			TodaysFollowup: GlobalServices.getPerPageRow(),
			PendingFollowup: GlobalServices.getPerPageRow(),
			UpcomingFollowup: GlobalServices.getPerPageRow(),
			FollowupNotRequired: GlobalServices.getPerPageRow()
		};

		$scope.newRegistration = {
			StudentId: null,
			FamilyType:1,
			RegNo: '',
			AdmitDate_TMP: new Date(),
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
			SiblingDetailColl_TMP: [],
			AttachmentColl: [],
			LedgerPanaNo: '',
			IsNewStudent: true,
			F_AnnualIncome: 0,
			M_AnnualIncome: 0,
			EnquiryNo: '',
			ClassId_First: null,
			AdmissionEnquiryId: null,
			ReceiptAsLedgerId: 1,
			CA_WardNo: 0,
			PA_WardNo: 0,
			AdmitDate_TMP: new Date(),
			FormSale: true,
			PA_ProvinceId: null,
			PA_DistrictId: null,
			PA_LocalLevelId: null,
			CA_ProvinceId: null,
			CA_DistrictId: null,
			CA_LocalLevelId: null,
			Mode: 'Save',
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date()
		};
		$scope.newRegistration.AcademicDetailsColl.push({});
		//$scope.GetAllRegistrationList(); 

		$scope.newFollowup = {
			FollowupId: null,
			Followupdate: new Date(),
			Remarks: '',
			NextFollowDate: null,
			NextFollowupTime: null,
			FollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newFollowup.FollowupDetailColl.push({});

		$scope.newTodaysFollowup = {
			TodaysFollowupId: null,
			FollowupDate: null,
			FollowupRemarks: '',
			NextFollowDate: null,
			NextFollowupTime: null,
			TodaysFollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newTodaysFollowup.TodaysFollowupDetailColl.push({});

		$scope.newPendingFollowup = {
			PendingFollowupId: null,
			FollowupDate: null,
			FollowupRemarks: '',
			NextFollowDate: null,
			NextFollowupTime: null,
			PendingFollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newPendingFollowup.PendingFollowupDetailColl.push({});
		 
		$scope.newUpcomingFollowup = {
			UpcomingFollowupId: null,
			DOB: null,
			FollowupRemarks: '',
			NextFolloupDate: null,
			NextFolloupTime: null,
			UpcomingFollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newUpcomingFollowup.UpcomingFollowupDetailColl.push({}); 


		$scope.newFollowupNotRequired = {
			FollowupNotRequiredId: null,

			Mode: 'Save'
		};

		var smsPara = {
			EntityId: 169,
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


		//Academic Year Wise Period
		$scope.AYPeriod = {};
		GlobalServices.getAcademicPeriod().then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AYPeriod = res.data.Data;

				$scope.newRegistration.FromDate_TMP = new Date($scope.AYPeriod.StartDate);
				$scope.newRegistration.ToDate_TMP = new Date();

				$scope.newRegistration.AcademicYear = $scope.AYPeriod.AcademicYearId;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$timeout(function () {
			$scope.getRptState();
		});

		$scope.RegConfig = {};
		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/creation/GetRegistrationNumberMethod",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {		 
			if (res.data.IsSuccess && res.data.Data) {
				$scope.RegConfig = res.data.Data;				 
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.GetEnquiryById();		
		$scope.GetAllFollowupList();
	};

	$scope.getRptState = function () {
		$http({
			method: 'GET',
			url: base_url + "Global/GetListState?entityId=" + EntityId + "&isReport=true",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				if ($scope.gridApi) {
					if ($scope.gridApi.saveState) {
						var state = JSON.parse(res.data.Data);
						$scope.gridApi.saveState.restore($scope, state);
					}
				}
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	$scope.SaveRptState = function () {
		var state = $scope.gridApi.saveState.save();
		var entityId = EntityId;
		GlobalServices.saveListState(entityId, state);
	}

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

	$scope.GetFeeItemList = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var queryCl = mx($scope.ClassList).firstOrDefault(p1 => p1.ClassId == $scope.newRegistration.ClassId);

		if (queryCl)
			$scope.newRegistration.ClassName = queryCl.Name;
		else
			$scope.newRegistration.ClassName = "";

		var para = {
			ForId: 5,
			ClassId: $scope.newRegistration.ClassId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetFeeItemFor",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess == true) {
				$scope.newRegistration.FeeItemColl = res.data.Data;

				var findData = mx($scope.newRegistration.FeeItemColl);
				$scope.newRegistration.Qty = findData.sum(p1 => p1.Qty);
				$scope.newRegistration.Rate = 0;
				$scope.newRegistration.DiscountAmt = findData.sum(p1 => p1.DiscountAmt);
				$scope.newRegistration.PayableAmt = findData.sum(p1 => p1.PayableAmt);

				//Added By Suresh on 24 Baihakh
				$timeout(function () {
					$scope.GetAutoRollNo();
				});
				//Ends

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.ChangeRate = function (det, col) {
		if (col == 1 || col == 2) {
			det.PayableAmt = (det.Qty * det.Rate) - det.DiscountAmt;
		} else if (col == 3) {
			var disAmt = 0;
			var amt = det.Qty * det.Rate;
			if (det.DiscountPer > 0) {
				disAmt = amt * det.DiscountPer / 100;
			}

			det.DiscountAmt = disAmt;
			det.PayableAmt = amt - disAmt;
		} else if (col == 4) {
			var disPer = 0;
			var amt = det.Qty * det.Rate;
			if (det.DiscountAmt > 0) {
				disPer = (det.DiscountAmt / amt) * 100;
			}
			det.DiscountPer = disPer;
			det.PayableAmt = amt - det.DiscountAmt;
		}
		var findData = mx($scope.newRegistration.FeeItemColl);
		$scope.newRegistration.Qty = findData.sum(p1 => p1.Qty);
		$scope.newRegistration.Rate = 0;
		$scope.newRegistration.DiscountAmt = findData.sum(p1 => p1.DiscountAmt);
		$scope.newRegistration.PayableAmt = findData.sum(p1 => p1.PayableAmt);

	};
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
					$scope.newRegistration.FormSale = true;
					$scope.newRegistration.ReferralCode = enq.ReferralCode;
					$scope.newRegistration.IsNewStudent = true;
					$scope.newRegistration.SourceId = enq.SourceId;
					$scope.newRegistration.FollowupRemarks = enq.FollowupRemarks;
					$scope.newRegistration.IfGurandianIs = enq.IfGuradianIs;
					$scope.newRegistration.EnquiryNo = enq.AutoManualNo;
					$scope.newRegistration.RegNo = enq.AutoNumber.toString();
					$scope.newRegistration.AdmissionEnquiryId = AdmissionEnquiryId;
					$scope.newRegistration.FirstName = enq.FirstName;
					$scope.newRegistration.MiddleName = enq.MiddleName;
					$scope.newRegistration.LastName = enq.LastName;
					$scope.newRegistration.Gender = enq.Gender;
					$scope.newRegistration.CasteId = enq.CasteId;
					$scope.newRegistration.PA_FullAddress = enq.Address;
					$scope.newRegistration.ContactNo = enq.ContactNo;
					$scope.newRegistration.Email = enq.Email;
					$scope.newRegistration.IsPhysicalDisability = enq.IsPhysicalDisability;
					$scope.newRegistration.PhysicalDisability = enq.PhysicalDisability;

					if (enq.DOB)
						$scope.newRegistration.DOB_TMP = new Date(enq.DOB);

					$scope.newRegistration.Religion = enq.Religion;
					$scope.newRegistration.Nationality = enq.Nationality;
					$scope.newRegistration.ClassId = enq.ClassId;
					$scope.newRegistration.MediumId = enq.MediumId;
					$scope.newRegistration.ClassShiftId = enq.ClassShiftId;
					$scope.newRegistration.PhotoPath = enq.PhotoPath;
					$scope.newRegistration.FatherName = enq.FatherName;
					$scope.newRegistration.F_Profession = enq.F_Profession;
					$scope.newRegistration.F_ContactNo = enq.F_ContactNo;
					$scope.newRegistration.F_Email = enq.F_Email;
					$scope.newRegistration.MotherName = enq.MotherName;
					$scope.newRegistration.M_Profession = enq.M_Profession;
					$scope.newRegistration.M_Contact = enq.M_ContactNo;
					$scope.newRegistration.M_Email = enq.M_Email;
					$scope.newRegistration.GuardianName = enq.GuardianName;
					$scope.newRegistration.G_Relation = enq.G_Relation;
					$scope.newRegistration.G_Profesion = enq.G_Professsion;
					$scope.newRegistration.G_ContactNo = enq.G_Contact;
					$scope.newRegistration.G_Email = enq.G_Email;
					$scope.newRegistration.G_Address = enq.G_Address;

					if (!enq.AcademicDetailsColl || enq.AcademicDetailsColl.length == 0) {
						enq.AcademicDetailsColl = [];
						enq.AcademicDetailsColl.push({});
					}

					$scope.newRegistration.AcademicDetailsColl = enq.AcademicDetailsColl;
					$scope.newRegistration.AttachmentColl = enq.AttachmentColl;
					$scope.newRegistration.AcademicYear = enq.AcademicYearId;

					$scope.newRegistration.F_AnnualIncome = enq.F_AnnualIncome;
					$scope.newRegistration.M_AnnualIncome = enq.M_AnnualIncome;
					$scope.newRegistration.StudentTypeId = enq.StudentTypeId;


					$scope.GetAutoNumber();

					document.getElementById('admission-section').style.display = "none";
					document.getElementById('admission-Registration-form').style.display = "block";

					$scope.GetFeeItemList();

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	};
	$scope.ClearRegistration = function () {
		$scope.newRegistration = {
			StudentId: null,
			FamilyType:1,
			FirstName: '',
			MiddleName: '',
			LastName: '',
			Gender: 1,
			Caste: '',
			DOB: '',
			Nationality: '',
			Religion: '',
			Address: '',
			ContactNo: '',
			Email: '',
			IsPhysicalDisability: false,
			ClassId: null,
			MediumId: null,
			FacultyId: null,
			ShiftId: null,
			TransportFacility: '',
			HostelRequired: '',
			OtherFacility: '',
			FatherName: '',
			FatherProfession: '',
			FatherContact: '',
			FatherEmail: '',
			MotherName: '',
			MotherProfession: '',
			MotherContact: '',
			MotherEmail: '',
			GuardianName: '',
			Relation: '',
			IfGuradianIs: 3,
			Photo: null,
			PhotoPath: null,
			AcademicDetailsColl: [],
			TypeOfDocument: '',
			AttachmentColl: [],
			Description: '',			
			RegistrationDate: null,
			Source: '',
			FollowUpDate: '',
			FollowUpTime: '',
			Remarks: '',
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			IsOtherfaciltity: false,
			RegistrationDate_TMP: new Date(),
			FollowUpDueDate: null,
			FollowUpTime: null,
			FeeItemColl: [],
			IsFollowupRequired: false,
			ReceiptAsLedgerId: 1,
			CA_WardNo: 0,
			PA_WardNo: 0,
			IsNewStudent: true,
			AdmitDate_TMP: new Date(),
			FormSale: true,
			PA_ProvinceId: null,
			PA_DistrictId: null,
			PA_LocalLevelId: null,
			CA_ProvinceId: null,
			CA_DistrictId: null,
			CA_LocalLevelId: null,
			Mode: 'Save'
		};

		$('#imgFatherPhoto').attr('src', '');
		$('#imgMotherPhoto').attr('src', '');
		$('#imgGuardian').attr('src', '');
		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

		$scope.newRegistration.AcademicDetailsColl.push({});
		$scope.GetAutoNumber();

		$scope.newRegistration.AcademicYear = $scope.AYPeriod.AcademicYearId;
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
	$scope.ClearStudentPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newRegistration.PhotoData = null;
				$scope.newRegistration.Photo_TMP = [];
				$scope.newRegistration.PhotoPath = '';
			});

			$scope.webCam.Start = false;
			stream = null;
		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};
	$scope.ClearStudentOnly = function () {
		$scope.ClearStudentPhoto();
		$scope.ClearSignaturePhoto();

		$timeout(function () {
			$scope.newRegistration = {
				StudentId: null,
				RegNo: '',
				AdmitDate_TMP: new Date(),
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
				SiblingDetailColl_TMP: [],
				LedgerPanaNo: '',
				IsNewStudent: true,
				F_AnnualIncome: 0,
				M_AnnualIncome: 0,
				EnquiryNo: '',
				ClassId_First: null,
				AdmissionEnquiryId: null,
				CA_WardNo: 0,
				PA_WardNo: 0,
				Mode: 'Save'
			};

			$scope.newRegistration.AcademicDetailsColl.push({});
			$scope.newRegistration.SiblingDetailColl_TMP.push({});

		});

		AdmissionEnquiryId = null;
	}


	$scope.ClearFollowup = function () {
		$scope.newFollowup = {
			FollowupId: null,
			FollowupDate: null,
			FollowUpRemarks: '',
			NextFollowDate: null,
			NextFollowupTime: null,
			FollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newFollowup.FollowupDetailColl.push({});
	}

	$scope.ClearTodaysFollowup = function () {
		$scope.newTodaysFollowup = {
			TodaysFollowupId: null,
			TodaysFollowupDate: null,
			TodaysFollowupRemarks: '',
			NextFollowDate: null,
			NextTodaysFollowupTime: null,
			TodaysFollowupDetailColl: [],
		};
		$scope.newTodaysFollowup.TodaysFollowupDetailColl.push({});
	}


	$scope.ClearPendingFollowup = function () {
		$scope.newPendingFollowup = {
			PendingFollowupId: null,
			FollowupDate: null,
			FollowupRemarks: '',
			NextFollowDate: null,
			NextFollowupTime: null,
			PendingFollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newPendingFollowup.PendingFollowupDetailColl.push({});
	}


	$scope.ClearUpcomingFollowup = function () {
		$scope.newUpcomingFollowup = {
			UpcomingFollowupId: null,
			DOB: null,
			FollowupRemarks: '',
			NextFolloupDate: null,
			NextFolloupTime: null,
			UpcomingFollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newUpcomingFollowup.UpcomingFollowupDetailColl.push({});
	}


	$scope.ClearFollowupNotRequired = function () {
		$scope.newFollowupNotRequired = {
			FollowupNotRequiredId: null,
			Mode: 'Save'
		};
	}


	function OnClickDefault() {
		document.getElementById('admission-Registration-form').style.display = "none";
		document.getElementById('quick-Registration-form').style.display = "none";

		document.getElementById('add-admission-Registration').onclick = function () {
			document.getElementById('admission-section').style.display = "none";
			document.getElementById('admission-Registration-form').style.display = "block";
			$scope.ClearRegistration();
			
		}
		document.getElementById('admission-list-back-btn').onclick = function () {
			document.getElementById('admission-Registration-form').style.display = "none";
			document.getElementById('admission-section').style.display = "block";
		}

		//document.getElementById('quick-Registration').onclick = function () {
		//	document.getElementById('admission-section').style.display = "none";
		//	document.getElementById('quick-Registration-form').style.display = "block";
		//	$scope.ClearRegistration();
		//}
		//document.getElementById('quickback-btn').onclick = function () {
		//	document.getElementById('quick-Registration-form').style.display = "none";
		//	document.getElementById('admission-section').style.display = "block";
		//}
	};

	//************************* Class *********************************
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
				 
				$('#flMoreFiles').val('');
			}
		}
	};

	$scope.GetAutoNumber = function () {

		$timeout(function () {

			$http({
				method: 'POST',
				url: base_url + "AdmissionManagement/Creation/GetRegistrationAutoNumber",
				dataType: "json",
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				var st = res.data.Data;
				if (st.IsSuccess == true) {
					$scope.newRegistration.RegNo = st.ResponseId;
					$scope.newRegistration.AutoManualNo = st.ResponseId;
					$scope.newRegistration.AutoNumber = st.RId;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});


	};


	$scope.AddAcademicDetails = function (ind) {
		if ($scope.newRegistration.AcademicDetailsColl) {
			if ($scope.newRegistration.AcademicDetailsColl.length > ind + 1) {
				$scope.newRegistration.AcademicDetailsColl.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newRegistration.AcademicDetailsColl.push({
					ClassName: ''
				})
			}
		}
	};
	$scope.delAcademicDetails = function (ind) {
		if ($scope.newRegistration.AcademicDetailsColl) {
			if ($scope.newRegistration.AcademicDetailsColl.length > 1) {
				$scope.newRegistration.AcademicDetailsColl.splice(ind, 1);
			}
		}
	};
 
	$scope.LoadGuradianDet = function () {

		$('#imgGuardian').attr('src', '');

		if ($scope.newRegistration.IfGurandianIs == 1) {
			$scope.newRegistration.GuardianName = $scope.newRegistration.FatherName;
			$scope.newRegistration.G_Relation = 'Father';
			$scope.newRegistration.G_Profesion = $scope.newRegistration.F_Profession;
			$scope.newRegistration.G_ContactNo = $scope.newRegistration.F_ContactNo;
			$scope.newRegistration.G_Email = $scope.newRegistration.F_Email;
			$scope.newRegistration.GuardianPhotoData = $scope.newRegistration.FatherPhotoData;
			$scope.newRegistration.GuardianPhotoPath = $scope.newRegistration.FatherPhotoPath;
			//newRegistration.FatherPhotoData : (newRegistration.FatherPhotoPath
			//newRegistration.MotherPhotoData : (newRegistration.MotherPhotoPath

		} else if ($scope.newRegistration.IfGurandianIs == 2) {
			$scope.newRegistration.GuardianName = $scope.newRegistration.MotherName;
			$scope.newRegistration.G_Relation = 'Mother';
			$scope.newRegistration.G_Profesion = $scope.newRegistration.M_Profession;
			$scope.newRegistration.G_ContactNo = $scope.newRegistration.M_Contact;
			$scope.newRegistration.G_Email = $scope.newRegistration.M_Email;
			$scope.newRegistration.GuardianPhotoData = $scope.newRegistration.MotherPhotoData;
			$scope.newRegistration.GuardianPhotoPath = $scope.newRegistration.MotherPhotoPath;

		} else if ($scope.newRegistration.IfGurandianIs == 3) {
			$scope.newRegistration.GuardianName = '';
			$scope.newRegistration.G_Relation = '';
			$scope.newRegistration.G_Profesion = '';
			$scope.newRegistration.G_ContactNo = '';
			$scope.newRegistration.G_Email = '';
			$scope.newRegistration.GuardianPhotoData = null;
			$scope.newRegistration.GuardianPhotoPath = null;
		}
	};

	$scope.SamePAddress = function () {

		if ($scope.newRegistration.IsSameAsPermanentAddress == true) {
			$scope.newRegistration.CA_FullAddress = $scope.newRegistration.PA_FullAddress;
			$scope.newRegistration.CA_ProvinceId = $scope.newRegistration.PA_ProvinceId;
			$scope.newRegistration.CA_DistrictId = $scope.newRegistration.PA_DistrictId;
			$scope.newRegistration.CA_LocalLevelId = $scope.newRegistration.PA_LocalLevelId;
			$scope.newRegistration.CA_WardNo = $scope.newRegistration.PA_WardNo;
			$scope.newRegistration.StreetName = $scope.newRegistration.PA_Village;

			//Added on baishakh 5
			$scope.newRegistration.CA_Province = $scope.newRegistration.PA_Province;
			$scope.newRegistration.CA_District = $scope.newRegistration.PA_District;
			$scope.newRegistration.CA_LocalLevel = $scope.newRegistration.PA_LocalLevel;

		} else {
			$scope.newRegistration.CA_FullAddress = '';
			$scope.newRegistration.CA_ProvinceId = '';
			$scope.newRegistration.CA_DistrictId = '';
			$scope.newRegistration.CA_LocalLevelId = '';
			$scope.newRegistration.CA_WardNo = '';
			$scope.newRegistration.StreetName = '';
			$scope.newRegistration.CA_Village = '';
			//Added on baishakh 5
			$scope.newRegistration.CA_Province = '';
			$scope.newRegistration.CA_District = '';
			$scope.newRegistration.CA_LocalLevel = '';
		}
	}
	$scope.IsValidRegistration = function () {
		if ($scope.newRegistration.FirstName.isEmpty()) {
			Swal.fire('Please ! Enter First Name');
			return false;
		}

		if ($scope.CompanyConfig.Country == 'Nepal') {
			if (!$scope.newRegistration.LastName || $scope.newRegistration.LastName.trim() === '') {
				Swal.fire('Please ! Enter Last Name');
				return false;
			}
		}
		let contact = ($scope.newRegistration.ContactNo || '').toString().trim();

		if (contact === '' || contact.length !== 10 || !/^\d+$/.test(contact)) {
			Swal.fire('Please enter valid 10 digit Contact No.');
			return false;
		}

		if (!$scope.newRegistration.PA_FullAddress || $scope.newRegistration.PA_FullAddress.trim() === '') {
			Swal.fire('Please ! Enter Permanent Full Address');
			return false;
		}
		if (!$scope.newRegistration.FatherName || $scope.newRegistration.FatherName.trim() === '') {
			Swal.fire('Please ! Enter FatherName');
			return false;
		}

		//if ($scope.newRegistration.MotherName.isEmpty()) {
		//	Swal.fire('Please ! Enter Mother Name');
		//	return false;
		//}

		return true;
	};

	$scope.ClearSignaturePhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newRegistration.SignatureData = null;
				$scope.newRegistration.Signature_TMP = [];
			});

		});

		$('#imgSignature').attr('src', '');
		$('#imgSignature1').attr('src', '');
	}; 


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


	$scope.CallSaveUpdateRegistration = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newRegistration.AttachmentColl;
		//$scope.newRegistration.AttachmentColl = [];

		var photo = $scope.newRegistration.Photo_TMP;
		var signature = $scope.newRegistration.Signature_TMP;

		var fatherphoto = $scope.newRegistration.FatherPhoto_TMP;
		var motherphoto = $scope.newRegistration.MotherPhoto_TMP;
		var guardianphoto = $scope.newRegistration.GuardianPhoto_TMP;


		if ($scope.newRegistration.PhotoData && (!photo || photo.length == 0)) {
			photo = [];

			photo.push(dataURItoFile($scope.newRegistration.PhotoData));
		}

		if ($scope.newRegistration.AdmitDateDet) {
			$scope.newRegistration.AdmitDate = $filter('date')(new Date($scope.newRegistration.AdmitDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newRegistration.AdmitDate = new Date();

		if ($scope.newRegistration.DOBDet) {
			$scope.newRegistration.DOB_AD = $filter('date')(new Date($scope.newRegistration.DOBDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newRegistration.DOB_AD = null

		//if ($scope.newRegistration.DOB_ADDet) {
		//	$scope.newRegistration.DOB_AD = $filter('date')(new Date($scope.newRegistration.DOB_ADDet.dateAD), 'yyyy-MM-dd');
		//}

		if ($scope.newRegistration.EnquiryDate_Det && $scope.newRegistration.EnquiryDate_Det.dateAD) {
			$scope.newRegistration.EnquiryDate = $filter('date')(new Date($scope.newRegistration.EnquiryDate_Det.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newRegistration.EnquiryDate = new Date();


		if ($scope.newRegistration.FollowupDateDet && $scope.newRegistration.FollowupDateDet.dateAD) {
			$scope.newRegistration.FollowupDate = $filter('date')(new Date($scope.newRegistration.FollowupDateDet.dateAD), 'yyyy-MM-dd');
		}

		if ($scope.newRegistration.FollowUpTime_TMP)
			$scope.newRegistration.FollowUpTime = $scope.newRegistration.FollowUpTime_TMP.toLocaleString();
		else
			$scope.newRegistration.FollowUpTime = null;

		//if ($scope.newRegistration.ClassId > 0) {
		//	var fstClass = mx($scope.ClassList).firstOrDefault(p1 => p1.ClassId == $scope.newEnquiry.ClassId);
		//	if (fstClass)
		//		$scope.newEnquiry.ClassName = fstClass.Name;
		//}

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

		var province1 = '', district1 = '', area1 = '';
		var province2 = '', district2 = '', area2 = '';

		if ($scope.CompanyConfig.Country == 'Nepal') {
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
		} else {
			province1 = $scope.newRegistration.PA_Province || '';
			district1 = $scope.newRegistration.PA_District || '';
			area1 = $scope.newRegistration.PA_LocalLevel || '';

			province2 = $scope.newRegistration.CA_Province || '';
			district2 = $scope.newRegistration.CA_District || '';
			area2 = $scope.newRegistration.CA_LocalLevel || '';
		}

		// Assign to model
		$scope.newRegistration.PA_Province = province1;
		$scope.newRegistration.PA_District = district1;
		$scope.newRegistration.PA_LocalLevel = area1;
		$scope.newRegistration.CA_Province = province2;
		$scope.newRegistration.CA_District = district2;
		$scope.newRegistration.CA_LocalLevel = area2;

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/SaveRegistration",
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

				return formData;
			},
			data: { jsonData: $scope.newRegistration, files: filesColl, stPhoto: photo, stSignature: signature, fphoto: fatherphoto, mphoto: motherphoto, gphto: guardianphoto }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStudent();
				$timeout(function () {
					$scope.Print(res.data.Data.RId);
				}, 2000);
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

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

	$scope.GetRegSummaryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newRegistration.ToDateDet && $scope.newRegistration.FromDateDet) {
			var para = {
				dateFrom: $filter('date')(new Date($scope.newRegistration.FromDateDet.dateAD), 'yyyy-MM-dd'),
				dateTo: $filter('date')(new Date($scope.newRegistration.ToDateDet.dateAD), 'yyyy-MM-dd')
			}
			$http({
				method: 'POST',
				url: base_url + "AdmissionManagement/Creation/GetRegSummary",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess) {
					$scope.gridOptions.data = res.data.Data;
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
		$scope.ClearStudentOnly();

		$timeout(function () {
			$scope.newRegistration.Gender = 0;
		});

		$timeout(function () {
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
						$scope.newRegistration = res.data.Data;
						$scope.newRegistration.AutoManualNo = res.data.Data.RegNo;
						$scope.SelectedClass = mx($scope.ClassList).firstOrDefault(p1 => p1.ClassId == $scope.newRegistration.ClassId);

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

						if (!$scope.newRegistration.AcademicDetailsColl || $scope.newRegistration.AcademicDetailsColl.length == 0)
							$scope.AddAcademicDetails(0);

						if ($scope.newRegistration.AdmitDate)
							$scope.newRegistration.AdmitDate_TMP = new Date($scope.newRegistration.AdmitDate);

						if ($scope.newRegistration.DOB_AD)
							$scope.newRegistration.DOB_TMP = new Date($scope.newRegistration.DOB_AD);

						if ($scope.newRegistration.FollowupDate)
							$scope.newRegistration.FollowupDate_TMP = new Date($scope.newRegistration.FollowupDate);

						if ($scope.newRegistration.FollowUpTime)
							$scope.newRegistration.FollowUpTime_TMP = new Date($scope.newRegistration.FollowUpTime);


					//	var csQuery = mx($scope.ClassSectionList.SectionList);

						//Addded By Suresh on Chaita 11 2081
						var findProvince = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.PA_Province);

						if (findProvince)
							$scope.newRegistration.PA_ProvinceId = findProvince.id;
						else
							$scope.newRegistration.PA_ProvinceId = null;

						var findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.PA_District);
						if (findDistrict)
							$scope.newRegistration.PA_DistrictId = findDistrict.id;
						else
							$scope.newRegistration.PA_DistrictId = null;

						var findArea = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.PA_LocalLevel);
						if (findArea)
							$scope.newRegistration.PA_LocalLevelId = findArea.id;
						else
							$scope.newRegistration.PA_LocalLevelId = null;

						//Current Address
						var findProvince = $scope.ProvinceColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.CA_Province);

						if (findProvince)
							$scope.newRegistration.CA_ProvinceId = findProvince.id;
						else
							$scope.newRegistration.CA_ProvinceId = null;

						var findDistrict = $scope.DistrictColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.CA_District);
						if (findDistrict)
							$scope.newRegistration.CA_DistrictId = findDistrict.id;
						else
							$scope.newRegistration.CA_DistrictId = null;

						var findArea = $scope.VDCColl_Qry.firstOrDefault(p1 => p1.text == res.data.Data.CA_LocalLevel);
						if (findArea)
							$scope.newRegistration.CA_LocalLevelId = findArea.id;
						else
							$scope.newRegistration.CA_LocalLevelId = null;

						document.getElementById('admission-section').style.display = "none";
						document.getElementById('admission-Registration-form').style.display = "block";
						$scope.newRegistration.Mode = 'Modify';
						 
					});


				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});

	};

	$scope.DelRegistrationById = function (refData) {

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
					url: base_url + "AdmissionManagement/Creation/DelRegistration",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllRegistrationList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};
	 

	$scope.GetAllFollowupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FollowupList = [];
		$scope.TodaysFollowupList = [];
		$scope.PendingFollowupList = [];
		$scope.UpcomingFollowupList = [];
		$scope.FollowupNotRequiredList = [];

		var para = {
			FollowupType: 0
		};

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/GetReqFollowup",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FollowupList = res.data.Data;

				angular.forEach(res.data.Data, function (d) {

					if (d.Status == 5) {
						$scope.FollowupNotRequiredList.push(d);
					} else {
						if (d.FollowupType == 1)
							$scope.TodaysFollowupList.push(d);
						else if (d.FollowupType == 2)
							$scope.PendingFollowupList.push(d);
						else if (d.FollowupType == 3)
							$scope.UpcomingFollowupList.push(d);
					}

				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.newFollowup = {};
	$scope.openFollowup = function (beData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newFollowup = {
			StudentId: beData.TranId,
			AutoNumber: beData.AutoManualNo,
			Name: beData.Name,
			RegdNo: beData.AutoManualNo,
			FatherName: beData.FatherName,
			F_ContactNo: beData.F_ContactNo,
			ClassSection: beData.ClassName,
			NextFollowupRequired: false,
			RefTranId: beData.RefTranId,
		};

		var para = {
			TranId: beData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/GetRegFollowupList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFollowup.HistoryColl = res.data.Data;
				$('#followup').modal('show');

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.curFollowup = {};
	$scope.closeFollowup = function (beData, statusId) {
		$scope.curFollowup = beData;

		$scope.loadingstatus = "running";
		showPleaseWait();

		var statusStr = '';
		if (statusId == 3) {
			statusStr = 'Hold'
		} else if (statusId == 4)
			statusStr = 'Resumed';
		else if (statusId == 5)
			statusStr = 'Rejected';
		else if (statusId == 8)
			statusStr = 'Approved';

		$scope.newFollowup = {
			TranId: beData.TranId,
			AutoNumber: beData.AutoManualNo,
			Name: beData.Name,
			RegdNo: beData.AutoManualNo,
			FatherName: beData.FatherName,
			F_ContactNo: beData.F_ContactNo,
			ClassSection: beData.ClassName,
			NextFollowupRequired: false,
			RefTranId: beData.RefTranId,
			Status: statusId,
			StatusStr: statusStr,
			StatusRemarks: (statusId == 8 ? 'Approved ' : ''),
		};

		var para = {
			TranId: beData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/GetRegFollowupList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFollowup.HistoryColl = res.data.Data;
				$('#followupClosed').modal('show');

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	}

	$scope.SaveUpdateFollowup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();


		if ($scope.newFollowup.PaymentDueDateDet && $scope.newFollowup.PaymentDueDateDet.dateAD) {
			$scope.newFollowup.PaymentDueDate = $filter('date')(new Date($scope.newFollowup.PaymentDueDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newFollowup.PaymentDueDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		if ($scope.newFollowup.NextFollowupDateDet && $scope.newFollowup.NextFollowupDateDet.dateAD) {
			$scope.newFollowup.NextFollowupDate = $filter('date')(new Date($scope.newFollowup.NextFollowupDateDet.dateAD), 'yyyy-MM-dd');
		}

		if ($scope.newFollowup.NextFollowupTime_TMP)
			$scope.newFollowup.NextFollowupTime = $scope.newFollowup.NextFollowupTime_TMP.toLocaleString();
		else
			$scope.newFollowup.NextFollowupTime_TMP = null;

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/SaveRegistrationFollowup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newFollowup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$('#followup').modal('hide');
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.SaveFollowupClosed = function () {

		if ($scope.newFollowup.StatusRemarks.isEmpty()) {
			Swal.fire('Please ! Enter Remarks');
			return;
		}

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TranId: $scope.newFollowup.TranId,
			Status: $scope.newFollowup.Status,
			Remarks: $scope.newFollowup.StatusRemarks
		};

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/SaveRegStatus",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.curFollowup.Status = $scope.newFollowup.Status;
				$scope.curFollowup.StatusRemarks = $scope.newFollowup.StatusRemarks;
				$('#followupClosed').modal('hide');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.openAssignCounselor = function (beData) {

		$scope.newFollowup = beData;
		$('#assigncouncellor').modal('show');

	}

	$scope.SaveAssignCounselor = function () {

		if (!$scope.newFollowup.EmployeeIdColl || $scope.newFollowup.EmployeeIdColl.length == 0) {
			Swal.fire('Please ! Select Employee');
			return;
		}

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TranId: $scope.newFollowup.TranId,
			AssignDate: ($scope.newFollowup.AssignDateDet ? $filter('date')(new Date($scope.newFollowup.AssignDateDet.dateAD), 'yyyy-MM-dd') : null),
			EmployeeIdColl: $scope.newFollowup.EmployeeIdColl
		};

		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/Creation/SaveRegAssignCounselor",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFollowup.IsAssignCounselor = true;
				Swal.fire(res.data.ResponseMSG);
				$('#assigncouncellor').modal('hide');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.DownloadForm = function (tranId) {
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
												var printURL = base_url + "newpdfviewer.ashx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=0&StudentId=" + StudentId + "&vouchertype=0";
												var req = {
													url: printURL,
													method: 'GET',
													responseType: 'arraybuffer'
												};
												$http(req).then(function (resp) {
													var serverData = new Blob([resp.data], { type: resp.headers()['content-type'] });
													hidePleaseWait();
													$scope.loadingstatus = "stop";
													FileSaver.saveAs(serverData, 'enquiry-form.pdf');
												});
											}

										} else {
											resolve('You need to select:)')
										}
									})
								}
							})
						}

						if (rptTranId > 0 && print == false) {
							var printURL = base_url + "newpdfviewer.ashx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=0&StudentId=" + StudentId + "&vouchertype=0";
							var req = {
								url: printURL,
								method: 'GET',
								responseType: 'arraybuffer'
							};
							$http(req).then(function (resp) {
								var serverData = new Blob([resp.data], { type: resp.headers()['content-type'] });
								hidePleaseWait();
								$scope.loadingstatus = "stop";
								FileSaver.saveAs(serverData, 'enquiry-form.pdf');
							});
						}

					} else
						Swal.fire('No Templates found for print');
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}
	};

	$scope.DownloadReceipt = function (tranId) {
		if ((tranId || tranId > 0)) {

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + ReceiptEntityId + "&voucherId=0&isTran=true",
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
												var printURL = base_url + "newpdfviewer.ashx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + ReceiptEntityId + "&voucherid=0&tranId=" + tranId + "&vouchertype=0";
												var req = {
													url: printURL,
													method: 'GET',
													responseType: 'arraybuffer'
												};
												$http(req).then(function (resp) {
													var serverData = new Blob([resp.data], { type: resp.headers()['content-type'] });
													hidePleaseWait();
													$scope.loadingstatus = "stop";
													FileSaver.saveAs(serverData, 'receipt-form.pdf');
												});
											}

										} else {
											resolve('You need to select:)')
										}
									})
								}
							})
						}

						if (rptTranId > 0 && print == false) {
							var printURL = base_url + "newpdfviewer.ashx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + ReceiptEntityId + "&voucherid=0&tranId=" + tranId + "&vouchertype=0";
							var req = {
								url: printURL,
								method: 'GET',
								responseType: 'arraybuffer'
							};
							$http(req).then(function (resp) {
								var serverData = new Blob([resp.data], { type: resp.headers()['content-type'] });
								hidePleaseWait();
								$scope.loadingstatus = "stop";
								FileSaver.saveAs(serverData, 'receipt-form.pdf');
							});
						}

					} else
						Swal.fire('No Templates found for print');
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}
	};

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

		$('#sendemail').modal('show');


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
		var tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
		for (let ent in tmpCheckedData) {
			if (tmpCheckedData[ent].isSelected == true) {
				var dt = tmpCheckedData[ent];
				$scope.CurSMSSend.DataColl.push(dt.entity);
			}
		}

		$('#sendsms').modal('show');
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
						EntityId: EntityId,
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
		var tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
		for (let ent in tmpCheckedData) {
			if (tmpCheckedData[ent].isSelected == true) {
				var dt = tmpCheckedData[ent];
				$scope.CurEmailSend.DataColl.push(dt.entity);
			}
		}

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
					paraColl.push({ Key: 'StudentId', Value: objEntity.TranId });

					var newEmail = {
						EntityId: EntityId,
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

	$scope.CurSMSEmailVerify = {};
	$scope.SMSEmailVerification = function (entityId, emailId, mobileNo, modalId) {
		$scope.CurSMSEmailVerify = {};
		$scope.CurSMSEmailVerify.EmailId = emailId;
		$scope.CurSMSEmailVerify.MobileNo = mobileNo;
		GlobalServices.GenerateOTP(entityId, emailId, mobileNo, modalId);
	}
	$scope.RensendOTP = function (entityId, emailId, mobileNo) {
		GlobalServices.GenerateOTP(entityId, emailId, mobileNo, null);
	}
	$scope.IsValidOTP = function (entityId, otp, modalId) {
		GlobalServices.IsValidOTP(entityId, otp, modalId);
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

	$scope.GetEmpCouncelling = function () {
		$scope.newFollowup.History = '';

		if ($scope.newFollowup.EmployeeIdColl) {

			var para = {
				EmpId: $scope.newFollowup.EmployeeIdColl[0]
			};
			$http({
				method: 'POST',
				url: base_url + "AdmissionManagement/Creation/GetRegCouncellingStatuses",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {

					var h = "";
					angular.forEach(res.data.Data, function (sd) {

						if (h.length > 0)
							h = h + " , ";

						h = h + sd.Status + "(" + sd.NoOfCouncelling + ")";
					});

					$scope.newFollowup.History = h;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}


	//Added By Suresh on 24 Baishakh
	$scope.GetAutoRollNo = function () {

		$scope.SelectedClass = {};
		$timeout(function () {

			//$scope.newStudent.SemesterId = null;
			//$scope.newStudent.ClassYearId = null;

			var para = {
				ClassId: $scope.newRegistration.ClassId,
				SectionId: $scope.newRegistration.SectionId,
				BatchId: $scope.newRegistration.BatchId,
				SemesterId: $scope.newRegistration.SemesterId,
				ClassYearId: $scope.newRegistration.ClassYearId
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
					$scope.newRegistration.RollNo = st.RId;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});
	};

	$scope.dobFocusOn = null;
	$scope.OnDOBFocus = function (dateStyle) {
		$scope.dobFocusOn = dateStyle;
	}

	$scope.updateDOB = function (dateStyle) {
		$timeout(function () {
			if (dateStyle == 1) {
				if ($scope.newRegistration.DOBADDet) {
					$scope.newRegistration.DOB_TMP = new Date($scope.newRegistration.DOBADDet.dateAD);
				}
				else {
					$scope.newRegistration.DOB_TMP = null;
				}
			}
			else if (dateStyle == 2) {
				if ($scope.newRegistration.DOBDet) {
					$scope.newRegistration.DOBAD_TMP = new Date($scope.newRegistration.DOBDet.dateAD);
				}
				else {
					$scope.newRegistration.DOBAD_TMP = null;
				}
			}
		});
	};

	$scope.validateDates = function (changedField) {
		if (!$scope.newRegistration.FromDateDet || !$scope.newRegistration.ToDateDet ||
			!$scope.newRegistration.FromDateDet.dateAD || !$scope.newRegistration.ToDateDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newRegistration.FromDateDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newRegistration.ToDateDet.dateAD), 'yyyy-MM-dd')
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be greater than To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newRegistration.FromDate_TMP = new Date();
						$scope.newRegistration.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be less than From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newRegistration.ToDate_TMP = new Date();
						$scope.newRegistration.ToDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};

});
