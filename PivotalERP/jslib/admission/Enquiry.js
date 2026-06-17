app.controller('EnquiryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Enquiry';

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
					name: "Action1", displayName: "Action", enableHiding: false,
					enableFiltering: false,
					enableSorting: false,
					minWidth: 70,
					enableColumnResizing: false,},
				{ name: "SNo", displayName: "S.No.", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "EnquiryNo", displayName: "Enquiry No.", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "EnqDate_BS", displayName: "Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 160, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Email", displayName: "Email", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassName", displayName: "For Class", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "Department", displayName: "Department", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "Shift", displayName: "Shift", minWidth: 110, headerCellClass: 'headerAligment' },
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
				{ name: "PreviousSchool", displayName: "PreviousSchool", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PreviousSchoolAddress", displayName: "PreviousSchoolAddress", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PreviousClassGpa", displayName: "PreviousClassGpa", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "OptionalFirst", displayName: "OptionalFirst", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "OptionalSecond", displayName: "OptionalSecond", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Talent", displayName: "Talent", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "IsTransport", displayName: "IsTransport", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "IsHostel", displayName: "IsHostel", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "IsTiffin", displayName: "IsTiffin", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Source", displayName: "Source", minWidth: 100, headerCellClass: 'headerAligment' },

				{
					name: 'Action',
					enableHiding: false,
					enableFiltering: false,
					enableSorting: false,
					minWidth: 140,
					enableColumnResizing: false,
					cellTemplate: '<a href="" class="p-1" title="Click For Edit" ng-click="grid.appScope.GetEnquiryById(row.entity)">' +
						'<i class="fas fa-edit text-info" aria-hidden="true"></i>' +
						'</a>' +
						'<a href="" class="p-1" title="Click For Re-Print" ng-click="grid.appScope.Print(row.entity.EnquiryId)">' +
						'<i class="fas fa-eye text-secondary" aria-hidden="true"></i>' +
						'</a>' +
						'<a href="" class="p-1" title="Click For Delete" ng-click="grid.appScope.DelEnquiryById(row.entity)">' +
						'<i class="fas fa-trash-alt text-danger" aria-hidden="true"></i>' +
						'</a>'
				},
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

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.LanguageColl = GlobalServices.getLangList();

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
			IsFollowupRequired: true,
			Mode: 'Save'
		};
		$scope.newEnquiry.AcademicDetailsColl.push({});
		//$scope.GetAllEnquiryList();




		$scope.newFollowup = {
			FollowupId: null,
			Followupdate: null,
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


		//$scope.GetAllFollowupList();
	};


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
			IsFollowupRequired: true,
			Mode: 'Save'
		};

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

		$scope.newEnquiry.AcademicDetailsColl.push({});
		$scope.GetAutoNumber();
	};


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
		document.getElementById('admission-enquiry-form').style.display = "none";
		document.getElementById('quick-enquiry-form').style.display = "none";

		document.getElementById('add-admission-enquiry').onclick = function () {
			document.getElementById('admission-section').style.display = "none";
			document.getElementById('admission-enquiry-form').style.display = "block";
			$scope.ClearEnquiry();
		}
		document.getElementById('admission-list-back-btn').onclick = function () {
			document.getElementById('admission-enquiry-form').style.display = "none";
			document.getElementById('admission-section').style.display = "block";
		}

		document.getElementById('quick-enquiry').onclick = function () {
			document.getElementById('admission-section').style.display = "none";
			document.getElementById('quick-enquiry-form').style.display = "block";
			$scope.ClearEnquiry();
		}
		document.getElementById('quickback-btn').onclick = function () {
			document.getElementById('quick-enquiry-form').style.display = "none";
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
					$scope.newEnquiry.AutoNumber = st.RId;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});


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


	$scope.IsValidEnquiry = function () {
		if ($scope.newEnquiry.EnquiryBy.isEmpty()) {
			Swal.fire('Please ! Enter who Enquiry');
			return false;
		}

		if ($scope.newEnquiry.ActionTaken.isEmpty()) {
			Swal.fire('Please ! Enter Action Taken');
			return false;
		}
		if ($scope.newEnquiry.Remarks.isEmpty()) {
			Swal.fire('Please ! Enter Remarks');
			return false;
		}



		return true;
	};


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
			}
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

	$scope.CallSaveUpdateEnquiry = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newEnquiry.EnquiryDateDet) {
			$scope.newEnquiry.EnquiryDate = $scope.newEnquiry.EnquiryDateDet.dateAD;
		} else
			$scope.newEnquiry.EnquiryDate = null;


		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveEnquiry",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newEnquiry }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearEnquiry();
				$scope.GetAllEnquiryList();
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

	$scope.GetEnquiryById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			EnquiryId: refData.EnquiryId
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

				document.getElementById('class-section').style.display = "none";
				document.getElementById('class-form').style.display = "block";

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
					EnquiryId: refData.EnquiryId
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
						$scope.GetAllEnquiryList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	
});
