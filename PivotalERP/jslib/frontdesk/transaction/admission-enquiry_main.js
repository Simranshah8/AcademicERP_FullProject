

app.controller('EnquiryController', function ($scope, $http, $timeout, $filter, $rootScope, $translate, GlobalServices, FileSaver) {
	$scope.Title = 'Admission Enquiry';
	
	OnClickDefault();
	getterAndSetter();

	$scope.PrintHtmlForm = function () {

		$('#admission-enquiry-form').printThis({
			importCSS: true,
			importStyle: true,
			formValues: true, 
			//header: "<h1>Look at all of my kitties!</h1>"
		});
		
    }

	let stream = null;
	let video = document.querySelector("#video");
	let canvas = document.querySelector('#canvas');
	let photoId = document.querySelector('#imgPhoto1');
	$scope.takePhotoFromCamera = async function () {

		if ($scope.webCam.Start == true) {
			$scope.webCam.Start = false;
			

			canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);
			$scope.newEnquiry.PhotoData = canvas.toDataURL('image/jpeg');

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

			$scope.gridApi.grid.getColumn('Caste').colDef.displayName = Labels.Cast;
			$scope.gridApi.grid.getColumn('Caste').displayName = Labels.Cast;
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
					cellTemplate: '<a href="" class="p-1" title="Click For Edit" ng-click="grid.appScope.GetEnquiryById(row.entity)">' +
						'<i class="fas fa-edit text-info" aria-hidden="true"></i>' +
						'</a>' +
						'<a href="" class="p-1" title="Click For Re-Print" ng-click="grid.appScope.Print(row.entity.EnquiryId)">' +
						'<i class="fas fa-eye text-secondary" aria-hidden="true"></i>' +
						'</a>' +
						'<a href="" class="p-1" title="Click For Delete" ng-click="grid.appScope.DelEnquiryById(row.entity)">' +
						'<i class="fas fa-trash-alt text-danger" aria-hidden="true"></i>' +
						'</a>'+
					'  <div class="dropdown dnld-file">' +
						'<button class= "btn btn-secondary btn-light btn-sm dropdown-toggle" type="button" ' +
						'id="dropdownMenuButton" data-toggle="dropdown" ' +
						'aria-haspopup="true" aria-expanded="false" >' +
						'<i class="fas fa-clipboard-list"></i>' +
						'                                                               </button >' +
						'<div class="dropdown-menu dropdown-menu-right"' +
						'aria-labelledby="dropdownMenuButton">' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.openAssignCounselor(row.entity)" ng-show="row.entity.IsAssignCounselor==false && row.entity.Status!=5"> Assign Counselor</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.openFollowup(row.entity)" ng-hide="row.entity.Status==3 || row.entity.Status==5"> Followup</a>' +
						'<a class="dropdown-item mb-0" href="' + base_url +'AdmissionManagement/Creation/Registration?EnquiryTranId={{row.entity.TranId}}" ng-hide="row.entity.Status==3 || row.entity.Status==6 || row.entity.Status==5"> Proceed to Registration</a>' +
						'<a class="dropdown-item mb-0" href="' + base_url +'AdmissionManagement/Creation/Admission?AdmissionEnquiryId={{row.entity.TranId}}&RegistrationId=0" ng-hide="row.entity.Status==3 || row.entity.Status==7 || row.entity.Status==5 || grid.appScope.EnqConfig.ActiveAdmission==false"> Proceed to Admission</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.closeFollowup(row.entity,3)" ng-show="row.entity.Status<3"> Hold Enquiry</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.closeFollowup(row.entity,4)" ng-show="row.entity.Status==3"> Resume Enquiry</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.closeFollowup(row.entity,5)" ng-hide="row.entity.Status>4 || row.entity.Status==3"> Reject Enquiry</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.DownloadForm(row.entity.TranId)"> Download form</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.DownloadReceipt(row.entity.ReceiptTranId)" ng-show="row.entity.ReceiptTranId>0"> Download Receipt</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.SendSMSIndivisual(row.entity)"> Send SMS</a>' +
						'<a class="dropdown-item mb-0" href="#" ng-click="grid.appScope.SendEmailIndivisual(row.entity)"> Send Email</a>' +
						'</div>' +
                        '</div >',
					pinned: 'left',

				},
				{ name: "SNo", displayName: "S.No.", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "AutoManualNo", displayName: "Enquiry No.", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "StatusStr", displayName: "Status", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "StatusRemarks", displayName: "Remarks", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "EnqDate_BS", displayName: "Enquiry Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Gender", displayName: "Gender", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 160, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },				
				{ name: "Email", displayName: "Email", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassName", displayName: "For Class", minWidth: 140, headerCellClass: 'headerAligment' },

				//{ name: "Department", displayName: "Department", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "Shift", displayName: "Shift", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "Medium", displayName: "Medium", minWidth: 110, headerCellClass: 'headerAligment' },

				{
					name: "DOB_AD", displayName: "DOB(AD)", minWidth: 140, headerCellClass: 'headerAligment',
					cellTemplate: '<div>{{row.entity.DOB_AD |dateFormat}}</div>',
				},
				{ name: "DOB_BS", displayName: "DOB(BS)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Age", displayName: "Age", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "BirthCertificateNo", displayName: "BirthCertificateNo", minWidth: 140, headerCellClass: 'headerAligment' },
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
				//{ name: "PA_Province", displayName: "PA_Province", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "PA_District", displayName: "PA_District", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "PA_LocalLevel", displayName: "PA_LocalLevel", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "PA_WardNo", displayName: "PA_WardNo", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "PA_StreetName", displayName: "PA_StreetName", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "CA_Province", displayName: "TA_Province", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "CA_District", displayName: "TA_District", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "CA_LocalLevel", displayName: "TA_LocalLevel", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "CA_WardNo", displayName: "TA_WardNo", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "CA_StreetName", displayName: "TA_StreetName", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "PreviousSchool", displayName: "PreviousSchool", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "PreviousSchoolAddress", displayName: "PreviousSchoolAddress", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PreviousClassGpa", displayName: "Previous GPA", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "OptionalFirst", displayName: "OptionalFirst", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "OptionalSecond", displayName: "OptionalSecond", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "Talent", displayName: "Talent", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TransportFacility", displayName: "Transport", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "HostelRequired", displayName: "Hostel", minWidth: 100, headerCellClass: 'headerAligment' },
				//{ name: "IsTiffin", displayName: "IsTiffin", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Source", displayName: "Source", minWidth: 100, headerCellClass: 'headerAligment' },
				{
					name: "FormSale_Str", displayName: "Form Sale", minWidth: 100, headerCellClass: 'headerAligment',
				},
				{ name: "StatusStr", displayName: "Status", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "StatusRemarks", displayName: "Remarks", minWidth: 200, headerCellClass: 'headerAligment' },
				{ name: "Counselor", displayName: "Counselor", minWidth: 200, headerCellClass: 'headerAligment' },

				{ name: "NextFollowupMitiTime", displayName: "Next Followup At", minWidth: 120, headerCellClass: 'headerAligment' },
				/*{ name: "CommunicationType", displayName: "Communication Type", minWidth: 120, headerCellClass: 'headerAligment' },*/
				{ name: "EnqCommunicationType", displayName: "Communication Type", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "EnqRemarks", displayName: "Enq. Remarks", minWidth: 120, headerCellClass: 'headerAligment' },

				{ name: "PhysicalDisability", displayName: "PhysicalDisability", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "IsPhysicalDisability_Str", displayName: "Is PhysicallyDisabled", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "Otherfaciltity", displayName: "Other Facilities", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "StudentType", displayName: "StudentType", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "F_AnnualIncome", displayName: "F_AnnualIncome", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "M_AnnualIncome", displayName: "M_AnnualIncome", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "PreClassName", displayName: "PreClassName", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "Exam", displayName: "Exam", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "PassedYear", displayName: "PassedYear", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "SymbolNo", displayName: "SymbolNo", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "ObtainedMarks", displayName: "ObtainedMarks", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "Division", displayName: "Division", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "ReferralCode", displayName: "Referral Code", minWidth: 100, headerCellClass: 'headerAligment' },
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'enqSummary.csv',
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
			exporterExcelFilename: 'enqSummary.xlsx',
			exporterExcelSheetName: 'enqSummary',
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
			}
		};

		 
	};
	$scope.GetEnqSummaryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newEnquiry.ToDateDet && $scope.newEnquiry.FromDateDet) {
			var para = {
				dateFrom: $filter('date')(new Date($scope.newEnquiry.FromDateDet.dateAD), 'yyyy-MM-dd'),
				dateTo: $filter('date')(new Date($scope.newEnquiry.ToDateDet.dateAD), 'yyyy-MM-dd')
			}
			$http({
				method: 'POST',
				url: base_url + "FrontDesk/Transaction/GetEnqSummary",
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
	$scope.LoadData = function () {

		$scope.webCam = {
			Start: false
		};


		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.GenderColl = GlobalServices.getGenderList();

		$scope.ComDet = {};
		GlobalServices.getCompanyDet().then(function (res)
		{
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ComDet = res.data.Data;				 
			}
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


		$scope.StudentTypeList = [];
		GlobalServices.getStudentTypeList().then(function (res) {
			$scope.StudentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.CasteList = [];
		GlobalServices.getCasteList().then(function (res) {
			$scope.CasteList = res.data.Data;
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

		$scope.AllClassList = [];
		GlobalServices.getClassList().then(function (res) {
			$scope.AllClassList = res.data.Data;
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
		

		/*$scope.TransportPointList = [];*/
		//GlobalServices.getList().then(function (res) {
		//	$scope.List = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		/*$scope.BoardersTypeList = [];*/
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
			Enquiry: 1,
			Followup: 1,
			TodaysFollowup: 1,
			PendingFollowup: 1,
			UpcomingFollowup: 1,
			FollowupNotRequired: 1

		};

		$scope.searchData = {
			Enquiry: '',
			Followup: '',
			TodaysFollowup: '',
			PendingFollowup: '',
			UpcomingFollowup: '',
			FollowupNotRequired: ''

		};

		$scope.perPage = {
			Enquiry: GlobalServices.getPerPageRow(),
			Followup: GlobalServices.getPerPageRow(),
			TodaysFollowup: GlobalServices.getPerPageRow(),
			PendingFollowup: GlobalServices.getPerPageRow(),
			UpcomingFollowup: GlobalServices.getPerPageRow(),
			FollowupNotRequired: GlobalServices.getPerPageRow()
		};

		$scope.newEnquiry = {
			EnquiryId: null,
			FirstName: '',
			MiddleName: '',
			LastName: '',
			Gender: 1,
			Caste: null,
			DOB: null,
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
			EnquiryId: '',
			EnquiryDate: '',
			Source: '',
			FollowUpDate: '',
			FollowUpTime: '',
			Remarks: '',
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			IsOtherfaciltity: false,
			EnquiryDate_TMP: new Date(),
			FollowUpDueDate: null,
			FollowUpTime: null,
			FeeItemColl: [],
			UDFFeildsColl: [],
			IsFollowupRequired: false,
			ReceiptAsLedgerId:1,
			Mode: 'Save'
		};
		$scope.newEnquiry.AcademicDetailsColl.push({});
		//$scope.GetAllEnquiryList();

	  
		$scope.GetAutoNumber();
		$scope.entity = {
			ManualBilling: entityManualBill,
			FeeReceipt: entityFeeReceipt
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


		$scope.newFollowup = {
			FollowupId: null,
			FollowupDate: new Date(),
			FollowUpRemarks: '',
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

		$scope.GetAllFollowupList();

		var smsPara = {
			EntityId: entitySMSEmail,
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
			Description:'',
			Primary: true,
			Father: false,
			Mother: false,
			Guardian: false,
			DataColl:[]
		};

		$scope.CurEmailSend = {
			Temlate: {},
			Description: '',
			Subject: '',
			Title:'',
			Primary: true,
			Father: false,
			Mother: false,
			Guardian: false,
			CC:'',
			DataColl: []
		};

		//Academic Year Wise Period
		$scope.AYPeriod = {};
		GlobalServices.getAcademicPeriod().then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AYPeriod = res.data.Data;

				$scope.newEnquiry.FromDate_TMP = new Date($scope.AYPeriod.StartDate);
				$scope.newEnquiry.ToDate_TMP = new Date();
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$timeout(function () {
			$scope.getRptState();
		});


		$scope.EnqConfig = {};
		$http({
			method: 'POST',
			url: base_url + "AdmissionManagement/creation/GetEnquiryNumberMethod",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EnqConfig = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.getRptState = function ()
	{
		$http({
			method: 'GET',
			url: base_url + "Global/GetListState?entityId=" + EntityId + "&isReport=true",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
			{
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

	$scope.ClearEnquiry = function () {
		$scope.newEnquiry = {
			EnquiryId: null,
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
			EnquiryId: '',
			EnquiryDate: '',
			Source: '',
			FollowUpDate: '',
			FollowUpTime: '',
			Remarks: '',
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			IsOtherfaciltity: false,
			EnquiryDate_TMP: new Date(),
			FollowUpDueDate: null,
			FollowUpTime: null,
			FeeItemColl: [],
			IsFollowupRequired: false,
			ReceiptAsLedgerId: 1,
			Mode: 'Save'
		};

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

		$('#flMoreFiles').val('');
		

		$scope.newEnquiry.AcademicDetailsColl.push({});
		$scope.GetAutoNumber();
	};

	function OnClickDefault() {
		document.getElementById('admission-enquiry-form').style.display = "none";
		 
		document.getElementById('add-admission-enquiry').onclick = function () {
			document.getElementById('admission-section').style.display = "none";
			document.getElementById('admission-enquiry-form').style.display = "block";
			$scope.ClearEnquiry();
		}
		document.getElementById('admission-list-back-btn').onclick = function () {
			document.getElementById('admission-enquiry-form').style.display = "none";
			document.getElementById('admission-section').style.display = "block";
		}
	};

	//************************* Class *********************************

	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newEnquiry.AttachmentColl) {
			if ($scope.newEnquiry.AttachmentColl.length > 0) {
				$scope.newEnquiry.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newEnquiry.AttachmentColl.push({
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
				url: base_url + "FrontDesk/Transaction/GetAdmissionEnqNo",
				dataType: "json",
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				var st = res.data.Data;
				if (st.IsSuccess == true) {
					$scope.newEnquiry.AutoManualNo = st.ResponseId;
					$scope.newEnquiry.AutoNumber = st.RId;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});


	};

	$scope.IsValidEnquiry = function () {
		if ($scope.newEnquiry.FirstName.isEmpty()) {
			Swal.fire('Please ! Enter First Name');
			return false;
		}

		
		//if ($scope.newEnquiry.MotherName.isEmpty()) {
		//	Swal.fire('Please ! Enter Mother Name');
		//	return false;
		//}
		//if ($scope.newEnquiry.LastName.isEmpty()) {
		//	Swal.fire('Please ! Enter Last Name');
		//	return false;
		//}
		
		

		return true;
	};

	$scope.AddAcademicDetails = function (ind) {
		if ($scope.newEnquiry.AcademicDetailsColl) {
			if ($scope.newEnquiry.AcademicDetailsColl.length > ind + 1) {
				$scope.newEnquiry.AcademicDetailsColl.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newEnquiry.AcademicDetailsColl.push({
					ClassName: ''
				})
			}
		}
	};
	$scope.delAcademicDetails = function (ind) {
		if ($scope.newEnquiry.AcademicDetailsColl) {
			if ($scope.newEnquiry.AcademicDetailsColl.length > 1) {
				$scope.newEnquiry.AcademicDetailsColl.splice(ind, 1);
			}
		}
	};
	 

	$scope.LoadGuradianDet = function () {

		if ($scope.newEnquiry.IfGuradianIs == 1) {
			$scope.newEnquiry.GuardianName = $scope.newEnquiry.FatherName;
			$scope.newEnquiry.G_Relation = 'Father';
			$scope.newEnquiry.G_Professsion = $scope.newEnquiry.F_Profession;
			$scope.newEnquiry.G_Contact = $scope.newEnquiry.F_ContactNo;
			$scope.newEnquiry.G_Email = $scope.newEnquiry.F_Email;


		} else if ($scope.newEnquiry.IfGuradianIs == 2) {
			$scope.newEnquiry.GuardianName = $scope.newEnquiry.MotherName;
			$scope.newEnquiry.G_Relation = 'Mother';
			$scope.newEnquiry.G_Professsion = $scope.newEnquiry.M_Profession;
			$scope.newEnquiry.G_Contact = $scope.newEnquiry.M_ContactNo;
			$scope.newEnquiry.G_Email = $scope.newEnquiry.M_Email;

		} else if ($scope.newEnquiry.IfGuradianIs == 3) {
			$scope.newEnquiry.GuardianName = '';
			$scope.newEnquiry.G_Relation = '';
			$scope.newEnquiry.G_Professsion = '';
			$scope.newEnquiry.G_Contact = '';
			$scope.newEnquiry.G_Email = '';
		}
	};

	$scope.SaveUpdateEnquiry = function () {
		if ($scope.IsValidEnquiry() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newEnquiry.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateEnquiry();
					}
				});
			} else
				$scope.CallSaveUpdateEnquiry();

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


	$scope.CallSaveUpdateEnquiry = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newEnquiry.AttachmentColl;
		
		var photo = $scope.newEnquiry.Photo_TMP;

		if ($scope.newEnquiry.PhotoData && (!photo || photo.length == 0)) {
			photo = [];

			photo.push(dataURItoFile($scope.newEnquiry.PhotoData));
		}

		if ($scope.newEnquiry.DOBDet) {
			$scope.newEnquiry.DOB = $filter('date')(new Date($scope.newEnquiry.DOBDet.dateAD), 'yyyy-MM-dd');
		}

		if ($scope.newEnquiry.EnquiryDate_Det && $scope.newEnquiry.EnquiryDate_Det.dateAD)
		{
			$scope.newEnquiry.EnquiryDate = $filter('date')(new Date($scope.newEnquiry.EnquiryDate_Det.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newEnquiry.EnquiryDate = new Date();


		if ($scope.newEnquiry.FollowupDateDet && $scope.newEnquiry.FollowupDateDet.dateAD) {
			$scope.newEnquiry.FollowupDate = $filter('date')(new Date($scope.newEnquiry.FollowupDateDet.dateAD), 'yyyy-MM-dd');
		}  

		if ($scope.newEnquiry.FollowUpTime_TMP)
			$scope.newEnquiry.FollowUpTime = $scope.newEnquiry.FollowUpTime_TMP.toLocaleString();
		else
			$scope.newEnquiry.FollowUpTime = null;

		//if ($scope.newEnquiry.ClassId > 0) {
		//	var fstClass = mx($scope.AllClassList).firstOrDefault(p1 => p1.ClassId == $scope.newEnquiry.ClassId);
		//	if (fstClass)
		//		$scope.newEnquiry.ClassName = fstClass.Name;
  //      }

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveEnquiry",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {

						if(data.files[i].File)
							formData.append("file" + i, data.files[i].File);
						else
							formData.append("file" + i, data.files[i]);
					}
				}

				if (data.stPhoto && data.stPhoto.length > 0)
					formData.append("photo", data.stPhoto[0]);		

				return formData;
			},

			data: { jsonData: $scope.newEnquiry, stPhoto: photo, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearEnquiry();

				if (res.data.Data && res.data.Data.RId>0)
					$scope.Print(res.data.Data.RId);

				//if (res.data.Data && res.data.Data.ResponseId) {
				//	$scope.PrintReceipt(res.data.Data.ResponseId);
    //            }
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllEnquiryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EnquiryList = [];

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllEnquiryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EnquiryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.ClearEnquiryPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newEnquiry.PhotoData = null;
				$scope.newEnquiry.Photo_TMP = [];
				$scope.newEnquiry.PhotoPath = '';
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');
		
	};

	$scope.GetEnquiryById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TranId: refData.EnquiryId
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
				$scope.newEnquiry = res.data.Data;
				$scope.newEnquiry.Mode = 'Modify';

				if (!$scope.newEnquiry.AcademicDetailsColl || $scope.newEnquiry.AcademicDetailsColl.length == 0) {
					$scope.newEnquiry.AcademicDetailsColl = [];
					$scope.newEnquiry.AcademicDetailsColl.push({});
				}

				if ($scope.newEnquiry.DOB)
					$scope.newEnquiry.DOB_TMP = new Date($scope.newEnquiry.DOB);

				if ($scope.newEnquiry.EnquiryDate)
					$scope.newEnquiry.EnquiryDate_TMP = new Date($scope.newEnquiry.EnquiryDate);

				if ($scope.newEnquiry.FollowupDate)
					$scope.newEnquiry.FollowupDate_TMP = new Date($scope.newEnquiry.FollowupDate);

				if ($scope.newEnquiry.FollowUpTime)
					$scope.newEnquiry.FollowUpTime_TMP = new Date($scope.newEnquiry.FollowUpTime);

				angular.forEach($scope.newEnquiry.AttachmentColl, function (ac) {
					ac.DocumentTypeName = ac.Name;
					ac.Path = ac.DocPath;
					
				});

				$scope.newEnquiry.AlreadyFormSale = $scope.newEnquiry.FormSale;

				document.getElementById('admission-section').style.display = "none";
				document.getElementById('admission-enquiry-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelEnquiryById = function (refData) {

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
					TranId: refData.EnquiryId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelEnquiry",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetEnqSummaryList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
		 
	};

	$scope.GetFeeItemList = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var queryCl = mx($scope.ClassList).firstOrDefault(p1 => p1.ClassId == $scope.newEnquiry.ClassId);

		if (queryCl)
			$scope.newEnquiry.ClassName = queryCl.Name;
		else
			$scope.newEnquiry.ClassName = "";

		var para = {
			ForId: 4,
			ClassId: $scope.newEnquiry.ClassId
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
				$scope.newEnquiry.FeeItemColl = res.data.Data;
				 
				var findData = mx($scope.newEnquiry.FeeItemColl);
				$scope.newEnquiry.Qty = findData.sum(p1 => p1.Qty);
				$scope.newEnquiry.Rate = 0;
				$scope.newEnquiry.DiscountAmt = findData.sum(p1 => p1.DiscountAmt);
				$scope.newEnquiry.PayableAmt = findData.sum(p1 => p1.PayableAmt);

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
		var findData = mx($scope.newEnquiry.FeeItemColl);
		$scope.newEnquiry.Qty = findData.sum(p1 => p1.Qty);
		$scope.newEnquiry.Rate = 0;
		$scope.newEnquiry.DiscountAmt = findData.sum(p1 => p1.DiscountAmt);
		$scope.newEnquiry.PayableAmt = findData.sum(p1 => p1.PayableAmt);

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

	$scope.PrintReceipt = function (tranId) {
		if ((tranId || tranId > 0)) {
			var TranId = tranId;

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.FeeReceipt + "&voucherId=0&isTran=true",
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
												document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + $scope.entity.FeeReceipt + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";
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
							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + $scope.entity.FeeReceipt + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";
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

	$scope.ShowPersonalImg = function (item) {

		$timeout(function () {
			$scope.viewImg = {
				ContentPath: '',
				File:null
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

				//$scope.viewImg.ContentPath = URL.createObjectURL(item.File);

				$('#PersonalImg').modal('show');
			}

			else
				Swal.fire('No Image Found');
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
			url: base_url + "FrontDesk/Transaction/GetEnqFollowup",
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
			RegdNo: beData.RegdNo,
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
			url: base_url + "FrontDesk/Transaction/GetEnqFollowupList",
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
	$scope.closeFollowup = function (beData,statusId) {
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

		$scope.newFollowup = {
			TranId: beData.TranId,
			AutoNumber: beData.AutoManualNo,
			Name: beData.Name,
			RegdNo: beData.RegdNo,
			FatherName: beData.FatherName,
			F_ContactNo: beData.F_ContactNo,
			ClassSection: beData.ClassName,
			NextFollowupRequired: false,
			RefTranId: beData.RefTranId,
			Status: statusId,
			StatusStr:statusStr,
			StatusRemarks:'',
		};

		var para = {
			TranId: beData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetEnqFollowupList",
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
			url: base_url + "FrontDesk/Transaction/SaveEnquiryFollowup",
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
			Status:$scope.newFollowup.Status,
			Remarks: $scope.newFollowup.StatusRemarks
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveEnqStatus",
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

		
		if ($scope.newFollowup.NextFollowupDateDet && $scope.newFollowup.NextFollowupDateDet.dateAD) {
			$scope.newFollowup.NextFollowupDate = $filter('date')(new Date($scope.newFollowup.NextFollowupDateDet.dateAD), 'yyyy-MM-dd');
		}


		var para = {
			TranId: $scope.newFollowup.TranId,
			AssignDate: ($scope.newFollowup.AssignDateDet ? $filter('date')(new Date($scope.newFollowup.AssignDateDet.dateAD), 'yyyy-MM-dd') : null  ),
			EmployeeIdColl: $scope.newFollowup.EmployeeIdColl
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveAssignCounselor",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFollowup.IsAssignCounselor = true;
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
		var tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
		for (let ent in tmpCheckedData) {
			if (tmpCheckedData[ent].isSelected == true) {
				var dt = tmpCheckedData[ent];
				$scope.CurSMSSend.DataColl.push(dt.entity);
			}
		}

		if ($scope.CurSMSSend.DataColl.length == 0) {
			Swal.fire('Please ! Select Data From List To Send SMS')
			return;
		} else {
			$('#sendsms').modal('show');
        }
		
	};

	$scope.SendSMS = function ()
	{
		if ($scope.CurSMSSend && $scope.CurSMSSend.DataColl && $scope.CurSMSSend.DataColl.length > 0) {

			var smsColl = [];
			angular.forEach($scope.CurSMSSend.DataColl, function (objEntity)
			{
				var contactNoColl = [];
				if ($scope.CurSMSSend.Primary == true) {

					if(objEntity.ContactNo && objEntity.ContactNo.length>0)
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
						FileName:'enquiry-form'
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
		GlobalServices.GenerateOTP(entityId, emailId, mobileNo,modalId);
	}
	$scope.RensendOTP = function (entityId,emailId,mobileNo) {
		GlobalServices.GenerateOTP(entityId, emailId, mobileNo, null);
	}
	$scope.IsValidOTP = function (entityId,otp,modalId) {
		GlobalServices.IsValidOTP(entityId, otp, modalId);
	}

	$scope.GetEmpCouncelling = function () {
		$scope.newFollowup.History = '';

		if ($scope.newFollowup.EmployeeIdColl) {

			var para = {
				EmpId: $scope.newFollowup.EmployeeIdColl[0]
			};
			$http({
				method: 'POST',
				url: base_url + "FrontDesk/Transaction/GetEmpCouncellingStatuses",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data)
				{
					 
					var h = "";
					angular.forEach(res.data.Data, function (sd) {

						if (h.length > 0)
							h = h + " , ";

						h = h  + sd.Status + "(" + sd.NoOfCouncelling+")";
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

	 
});