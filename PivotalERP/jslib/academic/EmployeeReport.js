app.controller('EmployeeReportController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate, uiGridConstants) {
	$scope.Title = 'Employee Report';

	getterAndSetter();

	$rootScope.ConfigFunction = function () {
		var keyColl = $translate.getTranslationTable();


		var Labels = {
			Citizenship: keyColl['CITIZENSHIP_LNG']
		};

		if ($rootScope.LANG == 'in') {

			// Emp Summary
			$scope.gridApi.grid.getColumn('DOB_AD').colDef.displayName = 'DOB';
			$scope.gridApi.grid.getColumn('DOB_AD').displayName = 'DOB';

			$scope.gridApi.grid.getColumn('CitizenShipno').colDef.displayName = Labels.Citizenship;
			$scope.gridApi.grid.getColumn('CitizenShipno').displayName = Labels.Citizenship;

			var findInd = -1;
			findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'DOB_BS' });

			if (findInd != -1)
				$scope.gridOptions.columnDefs.splice(findInd, 1);

			//Emp Left List
			$scope.gridApiLeft.grid.getColumn('DOB_AD').colDef.displayName = 'DOB';
			$scope.gridApiLeft.grid.getColumn('DOB_AD').displayName = 'DOB';

			findInd = $scope.gridOptionsLeft.columnDefs.findIndex(function (obj) { return obj.name == 'DOB_BS' });
			if (findInd != -1)
				$scope.gridOptionsLeft.columnDefs.splice(findInd, 1);

			// Emp Birthday List
			//$scope.gridApi4.grid.getColumn('DOB_AD').colDef.displayName = 'DOB';
			//$scope.gridApi4.grid.getColumn('DOB_AD').displayName = 'DOB';

			//findInd = $scope.gridOptions4.columnDefs.findIndex(function (obj) { return obj.name == 'DOB_BS' });
			//if (findInd != -1)
			//	$scope.gridOptions4.columnDefs.splice(findInd, 1);

		}
		$scope.gridApi.grid.refresh();
	/*	$scope.gridApi4.grid.refresh();*/
		$scope.gridApiLeft.grid.refresh();


	};
	$rootScope.ChangeLanguage();


	function getterAndSetter() {


		$scope.gridOptions = [];

		$scope.gridOptions = {
			showGridFooter: true,
			showColumnFooter: true,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,

			columnDefs: [
				{ name: "EmployeeCode", displayName: "Code", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "IsTeaching", displayName: "IsTeaching", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Gender", displayName: "Gender", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 160, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Department", displayName: "Department", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Designation", displayName: "Designation", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Category", displayName: "Category", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DOB_AD", displayName: "DOB(AD)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DOB_BS", displayName: "DOB(BS)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Age", displayName: "Age", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "FatherName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Caste", displayName: "Caste", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "PA_FullAddress", displayName: "PA_FullAddress", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TA_FullAddress", displayName: "TA_FullAddress", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "FatherName", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "Nationality", displayName: "Nationality", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "OfficeContactNo", displayName: "OfficeContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PersonalContactNo", displayName: "PersonalContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CitizenShipno", displayName: "CitizenShipno", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Email", displayName: "Email", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BloodGroup", displayName: "Blood Group", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Level", displayName: "Level", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "MaritalStatus", displayName: "Marital Status", minWidth: 130, headerCellClass: 'headerAligment' },
				{ name: "PanId", displayName: "Pan No", minWidth: 110, headerCellClass: 'headerAligment' },

				{ name: "Qualification", displayName: "Qualification", minWidth: 210, headerCellClass: 'headerAligment' },
				{ name: "MitiOfJoining", displayName: "Date of Joining", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "MitiOfConfirmation", displayName: "Date Of Confirmation", minWidth: 110, headerCellClass: 'headerAligment' },				
				{ name: "PA_State", displayName: "Province", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "PA_Municipality", displayName: "Municipality", minWidth: 110, headerCellClass: 'headerAligment' },

				{ name: "FatherContactNo", displayName: "Father ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "MotherName", displayName: "Mother Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "MotherContactNo", displayName: "Mother ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BankName", displayName: "Bank Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BankBranch", displayName: "Bank Branch", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BankAccountName", displayName: "Bank AccountName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BankAccountNo", displayName: "Bank AccountNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "NameNP", displayName: "नाम", minWidth: 150, headerCellClass: 'headerAligment' },

				//{
				//    name: 'Edit',
				//    enableFiltering: false,
				//    enableSorting: false,
				//    width: '5%',
				//    enableColumnResizing: false,
				//    cellTemplate: '<span class="label label-warning label-mini">' +
				//        '<a href="" style="color:white" title="Select" ng-click="grid.appScope.GetByID(row.entity)">' +
				//        '<i class="fa fa-check-square" aria-hidden="true"></i>' +
				//        '</a>' +
				//        '</span>'
				//},                
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'empSummary.csv',
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
			exporterExcelFilename: 'empSummary.xlsx',
			exporterExcelSheetName: 'DayBook',
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
			}
		};

		//$scope.gridOptions4 = {
		//	showGridFooter: true,
		//	showColumnFooter: false,
		//	useExternalPagination: false,
		//	useExternalSorting: false,
		//	enableFiltering: true,
		//	enableSorting: true,
		//	enableRowSelection: true,
		//	enableSelectAll: true,
		//	enableGridMenu: true,
		//	columnDefs: [
		//		{ name: "Code", displayName: "Code No.", minWidth: 90, headerCellClass: 'headerAligment' },
		//		{ name: "Name", displayName: "Name", minWidth: 160, headerCellClass: 'headerAligment' },
		//		{ name: "EnrollNo", displayName: "Enroll No.", minWidth: 100, headerCellClass: 'headerAligment', type: 'number' },
		//		{ name: "Department", displayName: "Department", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Designation", displayName: "Designation", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "DOB_BS", displayName: "DOB(B.S.)", minWidth: 100, headerCellClass: 'headerAligment' },
		//		{
		//			name: "DOB_AD", displayName: "DOB(A.D.)", minWidth: 100, headerCellClass: 'headerAligment', type: 'date',
		//			sortFn: function (aDate, bDate) {
		//				//debugger;
		//				var a = new Date(aDate);
		//				var b = new Date(bDate);
		//				if (a < b) {
		//					return -1;
		//				}
		//				else if (a > b) {
		//					return 1;
		//				}
		//				else {
		//					return 0;
		//				}
		//			}
		//		},
		//		{ name: "Age", displayName: "Age", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "ContactNo", displayName: "Contact No.", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "FatherName", displayName: "Father's Name", minWidth: 140, headerCellClass: 'headerAligment' },
		//		{ name: "Address", displayName: "Address", minWidth: 210, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },

		//	],
		//	//   rowTemplate: rowTemplate(),
		//	exporterCsvFilename: 'empbirthday.csv',
		//	exporterPdfDefaultStyle: { fontSize: 9 },
		//	exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
		//	exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
		//	exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
		//	exporterPdfFooter: function (currentPage, pageCount) {
		//		return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
		//	},
		//	exporterPdfCustomFormatter: function (docDefinition) {
		//		docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
		//		docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
		//		return docDefinition;
		//	},
		//	exporterPdfOrientation: 'portrait',
		//	exporterPdfPageSize: 'LETTER',
		//	exporterPdfMaxGridWidth: 500,
		//	exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
		//	exporterExcelFilename: 'empbirthday.xlsx',
		//	exporterExcelSheetName: 'empbirthday',
		//	onRegisterApi: function (gridApi) {
		//		$scope.gridApi4 = gridApi;
		//	}
		//};

		$scope.gridOptionsLeft = {
			showGridFooter: true,
			showColumnFooter: true,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,

			columnDefs: [
				{ name: "EmployeeCode", displayName: "Code", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 180, headerCellClass: 'headerAligment' },
				{ name: "Gender", displayName: "Gender", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 160, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Department", displayName: "Department", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Designation", displayName: "Designation", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Category", displayName: "Category", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DOB_AD", displayName: "DOB(AD)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DOB_BS", displayName: "DOB(BS)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "FatherName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Caste", displayName: "Caste", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LeftMiti", displayName: "Left Date", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "Remarks", displayName: "Left Remarks", minWidth: 180, headerCellClass: 'headerAligment' },
				{ name: "NameNP", displayName: "नाम", minWidth: 150, headerCellClass: 'headerAligment' },

			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'empSummary.csv',
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
			exporterExcelFilename: 'empLeftSummary.xlsx',
			exporterExcelSheetName: 'empLeft',
			onRegisterApi: function (gridApi) {
				$scope.gridApiLeft = gridApi;
			}
		};

		//New Code added By suresh starts
		$scope.gridOptions5 = {
			showGridFooter: true,
			showColumnFooter: true,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,
			columnDefs: [
				{ name: "EmpCode", displayName: "Code", minWidth: 80, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 180, headerCellClass: 'headerAligment' },
				{ name: "Gender", displayName: "Gender", minWidth: 80, headerCellClass: 'headerAligment' },
				{ name: "BankName", displayName: "Bank Name", minWidth: 160, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "AccountName", displayName: "Account Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "AccountNo", displayName: "Account No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Branch", displayName: "Branch Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ForPayRoll", displayName: "For PayRoll", minWidth: 100, headerCellClass: 'headerAligment' },
				
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'empSummary.csv',
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
			exporterExcelFilename: 'EmpBankDetail.xlsx',
			exporterExcelSheetName: 'empBankdetail',
			onRegisterApi: function (gridApi) {
				$scope.gridApiLeft = gridApi;
			}
		};
		//Ends
	};
	$scope.saveRptListState = function () {
		var state = $scope.gridApi.saveState.save();

		$http({
			method: 'POST',
			url: base_url + "Global/SaveListState",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", JSON.stringify(data.jsonData));
				formData.append("entityId", $scope.entity.EmpSummary);
				formData.append("isReport", true);

				return formData;
			},
			data: { jsonData: state }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

	};
	//$scope.SendSMSToEmp = function () {

	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	var uIdColl = [];

	//	angular.forEach($scope.gridOptions.data, function (d) {
	//		d.SMSText = $scope.newSMS.Description;
	//		uIdColl.push(d);
	//	});

	//	Swal.fire({
	//		title: 'Are you sure to send SMS ?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {
	//			$scope.loadingstatus = "running";
	//			showPleaseWait();

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Academic/Report/SendSMSToEmployee",
	//				dataType: "json",
	//				data: JSON.stringify(uIdColl)
	//			}).then(function (res) {
	//				hidePleaseWait();
	//				$scope.loadingstatus = "stop";
	//				if (res.data.IsSuccess) {
	//					$scope.ClearNotice();
	//				} else {
	//					Swal.fire(res.data.ResponseMSG);
	//				}

	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	});

	//};
	//$scope.SendNoticeToEmp = function () {

	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	var uIdColl = '';

	//	if ($scope.newNotice.FilterStudentOnly == true) {
	//		angular.forEach($scope.gridOptions.data, function (d) {

	//			if (uIdColl.length > 0)
	//				uIdColl = uIdColl + ',';

	//			uIdColl = uIdColl + d.UserId;
	//		});
	//	}

	//	if ($scope.newNotice.FilterStudentOnly == true && uIdColl.length == 0) {
	//		alert('First Load Employee');
	//		return;
	//	}


	//	var filesColl = $scope.newNotice.AttachmentColl;

	//	var tmpData = {
	//		studentIdColl: uIdColl,
	//		title: $scope.newNotice.Title,
	//		description: $scope.newNotice.Description,
	//		parents: true
	//	};

	//	$http({
	//		method: 'POST',
	//		url: base_url + "Academic/Report/SendNoticeToStudent",
	//		headers: { 'Content-Type': undefined },

	//		transformRequest: function (data) {

	//			var formData = new FormData();
	//			formData.append("jsonData", angular.toJson(data.jsonData));

	//			if (data.files) {
	//				for (var i = 0; i < data.files.length; i++) {
	//					if (data.files[i].File)
	//						formData.append("file" + i, data.files[i].File);
	//					else
	//						formData.append("file" + i, data.files[i]);
	//				}
	//			}

	//			return formData;
	//		},
	//		data: { jsonData: tmpData, files: filesColl }
	//	}).then(function (res) {

	//		$scope.loadingstatus = "stop";
	//		hidePleaseWait();

	//		Swal.fire(res.data.ResponseMSG);

	//		if (res.data.IsSuccess == true) {
	//			$scope.ClearNotice();
	//		}

	//	}, function (errormessage) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";

	//	});

	//};
	$scope.ClearNotice = function () {

		$scope.newNotice = {
			FilterStudentOnly: true,
			Title: '',
			Description: ''
		};

		$scope.newSMS = {
			Description: ''
		};
		$('#modal-xl').modal('hide');
		$('#modal-sms').modal('hide');
	}
	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.entity = {
			EmpSummary: entityEmpSummary,
			IdCard: 373
		};
		$scope.newSMS = {
			Description: ''
		};

		$scope.DepartmentList = [];
		GlobalServices.getDepartmentList().then(function (res) {
			$scope.DepartmentList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.DesignationList = [];
		GlobalServices.getDesignationList().then(function (res) {
			$scope.DesignationList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();

		$scope.newNotice = {
			FilterStudentOnly: true,
			Title: '',
			Description: ''
		};
		$scope.currentPages = {
			EmployeeSummary: 1,
			IdentityCard: 1,
			LeftEmployee: 1

		};

		$scope.searchData = {
			EmployeeSummary: '',
			IdentityCard: '',
			LeftEmployee: ''
		};

		$scope.perPage = {
			EmployeeSummary: GlobalServices.getPerPageRow(),
			IdentityCard: GlobalServices.getPerPageRow(),
			LeftEmployee: GlobalServices.getPerPageRow(),

		};

		$scope.newEmployeeSummary = {
			EmployeeSummaryId: null,

		};

		$scope.newIdentityCard = {
			IdentityCardId: null,
			GenerateQR: false,
			DepartmentId: 0,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,

		};

		$scope.newLeftEmployee = {
			LeftEmployeeId: null,

		};

		//$http({
		//	method: 'GET',
		//	url: base_url + "Global/GetListState?entityId=" + $scope.entity.EmpSummary + "&isReport=true",
		//	dataType: "json"
		//}).then(function (res) {
		//	if (res.data.IsSuccess && res.data.Data) {
		//		if ($scope.gridApi) {
		//			if ($scope.gridApi.saveState) {
		//				var state = JSON.parse(res.data.Data);

		//				$scope.gridApi.saveState.restore($scope, state);
		//			}
		//		}
		//	}		
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		$scope.LoadReportTemplates();

		$scope.newBirthday = {
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date()
		};

		//$timeout(function () {
		//	$scope.LoadDOB();
		//});

		$scope.EmpListForSelectionColl = [];
		//For 1=All,2=Continue,3=Left
		var empPara = {
			For: 2
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllEmpForSelection",
			dataType: "json",
			data: JSON.stringify(empPara)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.EmpListForSelectionColl = res.data.Data;
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		var smsPara = {
			EntityId: entityEmpSummaryForSMS,
			ForATS: 2,
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
	}

	$scope.GetEmpSummaryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetEmpSummary",
			dataType: "json",
			//data: JSON.stringify(para)
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

	$scope.PrintEmpSummary = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityEmpSummary + "&voucherId=0&isTran=false",
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
											var dataColl = [];// $scope.gridOptions.data;
											angular.forEach($scope.gridApi.grid.getVisibleRows(), function (node) {
												var objEntity = node.entity;
												dataColl.push(objEntity);
											});

											print = true;
											$http({
												method: 'POST',
												url: base_url + "Academic/Report/PrintEmployeeSummary",
												headers: { 'Content-Type': undefined },

												transformRequest: function (data) {

													var formData = new FormData();
													formData.append("jsonData", angular.toJson(data.jsonData));

													return formData;
												},
												data: { jsonData: dataColl }
											}).then(function (res) {

												$scope.loadingstatus = "stop";
												hidePleaseWait();
												if (res.data.IsSuccess && res.data.Data) {

													document.body.style.cursor = 'wait';
													document.getElementById("frmRpt").src = '';
													document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityEmpSummary + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
													document.body.style.cursor = 'default';
													$('#FrmPrintReport').modal('show');

												} else
													Swal.fire('No Templates found for print');

											}, function (errormessage) {
												hidePleaseWait();
												$scope.loadingstatus = "stop";
												Swal.fire(errormessage);
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
						var dataColl = [];// $scope.gridOptions.data;
						angular.forEach($scope.gridApi.grid.getVisibleRows(), function (node) {
							var objEntity = node.entity;
							dataColl.push(objEntity);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Academic/Report/PrintEmployeeSummary",
							headers: { 'Content-Type': undefined },

							transformRequest: function (data) {

								var formData = new FormData();
								formData.append("jsonData", angular.toJson(data.jsonData));

								return formData;
							},
							data: { jsonData: dataColl }
						}).then(function (res) {

							$scope.loadingstatus = "stop";
							hidePleaseWait();
							if (res.data.IsSuccess && res.data.Data) {

								document.body.style.cursor = 'wait';
								document.getElementById("frmRpt").src = '';
								document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityEmpSummary + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
								document.body.style.cursor = 'default';
								$('#FrmPrintReport').modal('show');

							} else
								Swal.fire('No Templates found for print');

						}, function (errormessage) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							Swal.fire(errormessage);
						});

					}

				} else
					Swal.fire('No Templates found for print');
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.GetEmpLeftSummaryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetEmpLeftSummary",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptionsLeft.data = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.ClearEmployeeSummary = function () {
		$scope.newEmployeeSummary = {
			EmployeeSummaryId: null,

		};
	}
	$scope.ClearIdentityCard = function () {
		$scope.newIdentityCard = {
			GenerateQR: false,
			DepartmentId: 0,
			IdentityCardId: null,

		};
	}
	$scope.ClearLeftEmployee = function () {
		$scope.newLeftEmployee = {
			LeftEmployeeId: null,

		};
	}


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

	$scope.LoadReportTemplates = function () {

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.IdCard + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newIdentityCard.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	$scope.TestClick = function (v) {
		v.GenerateQR = !v.GenerateQR;
	}
	$scope.PrintNormalIdCard = function () {
		if ($scope.newIdentityCard.RptTranId) {
			var EntityId = $scope.entity.IdCard;

			var template = mx($scope.newIdentityCard.TemplatesColl).firstOrDefault(p1 => p1.RptTranId == $scope.newIdentityCard.RptTranId);

			if ($scope.newIdentityCard.RptTranId > 0) {

				var vFrom = null, vTo = null;
				if ($scope.newIdentityCard.IssueDateDet)
					vFrom = $filter('date')(new Date($scope.newIdentityCard.IssueDateDet.dateAD), 'yyyy-MM-dd');

				if ($scope.newIdentityCard.ValidUptoDet)
					vTo = $filter('date')(new Date($scope.newIdentityCard.ValidUptoDet.dateAD), 'yyyy-MM-dd');

				var rptPara = {
					rpttranid: $scope.newIdentityCard.RptTranId,
					istransaction: false,
					entityid: EntityId,
					EmpIdColl: '',
					ValidFrom: vFrom,
					ValidTo: vTo,
					GenerateQR: $scope.newIdentityCard.GenerateQR,
					DepartmentId: $scope.newIdentityCard.DepartmentId,
					DesignationId: $scope.newIdentityCard.DesignationId,
				};

				var paraQuery = param(rptPara);
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';

				if (template.IsRDLC == true)
					document.getElementById("frmRptTabulation").src = base_url + "Academic/Report/RdlEmployeeIdCard?" + paraQuery;
				else
					document.getElementById("frmRptTabulation").src = base_url + "web/ReportViewer.aspx?" + paraQuery;

				document.body.style.cursor = 'default';
			} else {
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';
				document.body.style.cursor = 'default';
			}
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptTabulation").src = '';
			document.body.style.cursor = 'default';
		}

	};

	$scope.PrintStudentIdCard = function () {
		if ($scope.newIdentityCard.RptTranId && $scope.newIdentityCard.EmployeeIdColl) {
			var EntityId = $scope.entity.IdCard;

			var template = mx($scope.newIdentityCard.TemplatesColl).firstOrDefault(p1 => p1.RptTranId == $scope.newIdentityCard.RptTranId);

			if ($scope.newIdentityCard.RptTranId > 0) {

				var vFrom = null, vTo = null;
				if ($scope.newIdentityCard.IssueDateDet)
					vFrom = $filter('date')(new Date($scope.newIdentityCard.IssueDateDet.dateAD), 'yyyy-MM-dd');

				if ($scope.newIdentityCard.ValidUptoDet)
					vTo = $filter('date')(new Date($scope.newIdentityCard.ValidUptoDet.dateAD), 'yyyy-MM-dd');

				var rptPara = {
					rpttranid: $scope.newIdentityCard.RptTranId,
					istransaction: false,
					entityid: EntityId,
					EmpIdColl: ($scope.newIdentityCard.EmployeeIdColl ? $scope.newIdentityCard.EmployeeIdColl.toString() : ''),
					ValidFrom: vFrom,
					ValidTo: vTo,
					GenerateQR: $scope.newIdentityCard.GenerateQR,
					DepartmentId: 0
				};
				var paraQuery = param(rptPara);
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';

				if (template.IsRDLC == true)
					document.getElementById("frmRptTabulation").src = base_url + "Academic/Report/RdlEmployeeIdCard?" + paraQuery;
				else
					document.getElementById("frmRptTabulation").src = base_url + "web/ReportViewer.aspx?" + paraQuery;

				document.body.style.cursor = 'default';
			} else {
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptTabulation").src = '';
				document.body.style.cursor = 'default';
			}
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptTabulation").src = '';
			document.body.style.cursor = 'default';
		}

	};


	//$scope.SendSMSToEmp = function () {
	//	Swal.fire({
	//		title: 'Do you want to Send SMS To the filter data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {
	//			var para1 = {
	//				EntityId: entityEmpSummaryForSMS,
	//				ForATS: 2,
	//				TemplateType: 1
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Setup/Security/GetSENT",
	//				dataType: "json",
	//				data: JSON.stringify(para1)
	//			}).then(function (res) {
	//				if (res.data.IsSuccess && res.data.Data) {
	//					var templatesColl = res.data.Data;
	//					if (templatesColl && templatesColl.length > 0) {
	//						var templatesName = [];
	//						var sno = 1;
	//						angular.forEach(templatesColl, function (tc) {
	//							templatesName.push(sno + '-' + tc.Name);
	//							sno++;
	//						});

	//						var print = false;

	//						var rptTranId = 0;
	//						var selectedTemplate = null;
	//						if (templatesColl.length == 1) {
	//							rptTranId = templatesColl[0].TranId;
	//							selectedTemplate = templatesColl[0];
	//						}
	//						else {
	//							Swal.fire({
	//								title: 'Templates For SMS',
	//								input: 'select',
	//								inputOptions: templatesName,
	//								inputPlaceholder: 'Select a template',
	//								showCancelButton: true,
	//								inputValidator: (value) => {
	//									return new Promise((resolve) => {
	//										if (value >= 0) {
	//											resolve()
	//											rptTranId = templatesColl[value].TranId;
	//											selectedTemplate = templatesColl[value];

	//											if (rptTranId > 0) {

	//												let tmpCheckedData = [];
	//												tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
	//												var dataColl = [];
	//												for (let ent in tmpCheckedData) {
	//													if (tmpCheckedData[ent].isSelected == true) {
	//														//checkedItemOnly = true;
	//														//break;

	//														var objEntity = tmpCheckedData[ent].entity;
	//														var tmpContactNo = objEntity.ContactNo;

	//														if (tmpContactNo && tmpContactNo.length > 0) {
	//															var msg = selectedTemplate.Description;
	//															for (let x in objEntity) {
	//																var variable = '$$' + x.toLowerCase() + '$$';
	//																if (msg.indexOf(variable) >= 0) {
	//																	var val = objEntity[x];
	//																	msg = msg.replace(variable, val);
	//																}

	//																if (msg.indexOf('$$') == -1)
	//																	break;
	//															}

	//															var newSMS = {
	//																EntityId: entityEmpSummaryForSMS,
	//																StudentId: objEntity.EmployeeId,
	//																UserId: objEntity.UserId,
	//																Title: selectedTemplate.Title,
	//																Message: msg,
	//																ContactNo: tmpContactNo,
	//																StudentName: objEntity.Name
	//															};

	//															dataColl.push(newSMS);
	//														}

	//													}
	//												}



	//												print = true;

	//												$http({
	//													method: 'POST',
	//													url: base_url + "Global/SendSMSToStudent",
	//													dataType: "json",
	//													data: JSON.stringify(dataColl)
	//												}).then(function (sRes) {
	//													Swal.fire(sRes.data.ResponseMSG);
	//													if (sRes.data.IsSuccess && sRes.data.Data) {

	//													}
	//												});

	//											}

	//										} else {
	//											resolve('You need to select:)')
	//										}
	//									})
	//								}
	//							})
	//						}

	//						if (rptTranId > 0 && print == false) {
	//							let tmpCheckedData = [];
	//							tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
	//							var dataColl = [];
	//							for (let ent in tmpCheckedData) {
	//								if (tmpCheckedData[ent].isSelected == true) {
	//									//checkedItemOnly = true;
	//									//break;

	//									var objEntity = tmpCheckedData[ent].entity;
	//									var tmpContactNo = objEntity.ContactNo;

	//									if (tmpContactNo && tmpContactNo.length > 0) {
	//										var msg = selectedTemplate.Description;
	//										for (let x in objEntity) {
	//											var variable = '$$' + x.toLowerCase() + '$$';
	//											if (msg.indexOf(variable) >= 0) {
	//												var val = objEntity[x];
	//												msg = msg.replace(variable, val);
	//											}

	//											if (msg.indexOf('$$') == -1)
	//												break;
	//										}

	//										var newSMS = {
	//											EntityId: entityEmpSummaryForSMS,
	//											StudentId: objEntity.EmployeeId,
	//											UserId: objEntity.UserId,
	//											Title: selectedTemplate.Title,
	//											Message: msg,
	//											ContactNo: tmpContactNo,
	//											StudentName: objEntity.Name
	//										};

	//										dataColl.push(newSMS);
	//									}

	//								}
	//							}

	//							print = true;

	//							$http({
	//								method: 'POST',
	//								url: base_url + "Global/SendSMSToStudent",
	//								dataType: "json",
	//								data: JSON.stringify(dataColl)
	//							}).then(function (sRes) {
	//								Swal.fire(sRes.data.ResponseMSG);
	//								if (sRes.data.IsSuccess && sRes.data.Data) {

	//								}
	//							});

	//						}

	//					} else
	//						Swal.fire('No Templates found for SMS');
	//				}
	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	});


	//};
	$scope.SendNoticeToEmp = function () {
		Swal.fire({
			title: 'Do you want to Send Notification To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				var para1 = {
					EntityId: entityEmpSummaryForSMS,
					ForATS: 2,
					TemplateType: 3
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/GetSENT",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						var templatesColl = res.data.Data;
						if (templatesColl && templatesColl.length > 0) {
							var templatesName = [];
							var sno = 1;
							angular.forEach(templatesColl, function (tc) {
								templatesName.push(sno + '-' + tc.Name);
								sno++;
							});

							var print = false;

							var rptTranId = 0;
							var selectedTemplate = null;
							if (templatesColl.length == 1) {
								rptTranId = templatesColl[0].TranId;
								selectedTemplate = templatesColl[0];
							}
							else {
								Swal.fire({
									title: 'Templates For Notification',
									input: 'select',
									inputOptions: templatesName,
									inputPlaceholder: 'Select a template',
									showCancelButton: true,
									inputValidator: (value) => {
										return new Promise((resolve) => {
											if (value >= 0) {
												resolve()
												rptTranId = templatesColl[value].TranId;
												selectedTemplate = templatesColl[value];

												if (rptTranId > 0) {
													var dataColl = [];
													let tmpCheckedData = [];
													tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
													for (let ent in tmpCheckedData) {
														if (tmpCheckedData[ent].isSelected == true) {
															var objEntity = tmpCheckedData[ent].entity;
															var msg = $scope.newNotice.Description;
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
																EntityId: entityEmpSummaryForSMS,
																StudentId: objEntity.EmployeeId,
																UserId: objEntity.UserId,
																Title: selectedTemplate.Title,
																Message: msg,
																ContactNo: objEntity.ContactNo,
																StudentName: objEntity.Name
															};

															dataColl.push(newSMS);

														}
													}

													print = true;

													$http({
														method: 'POST',
														url: base_url + "Global/SendNotificationToStudent",
														dataType: "json",
														data: JSON.stringify(dataColl)
													}).then(function (sRes) {
														Swal.fire(sRes.data.ResponseMSG);
														if (sRes.data.IsSuccess && sRes.data.Data) {

														}
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
								var dataColl = [];
								let tmpCheckedData = [];
								tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
								for (let ent in tmpCheckedData) {
									if (tmpCheckedData[ent].isSelected == true) {
										var objEntity = tmpCheckedData[ent].entity;
										var msg = $scope.newNotice.Description;
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
											EntityId: entityEmpSummaryForSMS,
											StudentId: objEntity.EmployeeId,
											UserId: objEntity.UserId,
											Title: selectedTemplate.Title,
											Message: msg,
											ContactNo: objEntity.ContactNo,
											StudentName: objEntity.Name
										};

										dataColl.push(newSMS);

									}
								}
								print = true;

								$http({
									method: 'POST',
									url: base_url + "Global/SendNotificationToStudent",
									dataType: "json",
									data: JSON.stringify(dataColl)
								}).then(function (sRes) {
									Swal.fire(sRes.data.ResponseMSG);
									if (sRes.data.IsSuccess && sRes.data.Data) {

									}
								});

							}

						} else
							Swal.fire('No Templates found for SMS');
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}
		});


	};


	//$scope.SendNoticeToEmployee = function () {
	//	Swal.fire({
	//		title: 'Do you want to Send Notification To the filter data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {

	//			var para1 = {
	//				EntityId: entityEmpSummaryForSMS,
	//				ForATS: 2,
	//				TemplateType: 3
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Setup/Security/GetSENT",
	//				dataType: "json",
	//				data: JSON.stringify(para1)
	//			}).then(function (res) {
	//				if (res.data.IsSuccess && res.data.Data) {
	//					var templatesColl = res.data.Data;
	//					if (templatesColl && templatesColl.length > 0) {
	//						var templatesName = [];
	//						var sno = 1;
	//						angular.forEach(templatesColl, function (tc) {
	//							templatesName.push(sno + '-' + tc.Name);
	//							sno++;
	//						});

	//						var print = false;

	//						var rptTranId = 0;
	//						var selectedTemplate = null;
	//						if (templatesColl.length == 1) {
	//							rptTranId = templatesColl[0].TranId;
	//							selectedTemplate = templatesColl[0];
	//						}
	//						else {
	//							Swal.fire({
	//								title: 'Templates For Notification',
	//								input: 'select',
	//								inputOptions: templatesName,
	//								inputPlaceholder: 'Select a template',
	//								showCancelButton: true,
	//								inputValidator: (value) => {
	//									return new Promise((resolve) => {
	//										if (value >= 0) {
	//											resolve()
	//											rptTranId = templatesColl[value].TranId;
	//											selectedTemplate = templatesColl[value];

	//											if (rptTranId > 0) {
	//												$scope.newNotice.Title = selectedTemplate.Title;
	//												$scope.newNotice.Description = selectedTemplate.Description;
	//												$('#modal-xl').modal('show');
	//											}

	//										} else {
	//											resolve('You need to select:)')
	//										}
	//									})
	//								}
	//							})
	//						}

	//						if (rptTranId > 0 && print == false) {
	//							$scope.newNotice.Title = selectedTemplate.Title;
	//							$scope.newNotice.Description = selectedTemplate.Description;
	//							$('#modal-xl').modal('show');
	//						}

	//					} else {
	//						$scope.newNotice.Title = '';
	//						$scope.newNotice.Description = '';
	//						$('#modal-xl').modal('show');
	//					}

	//				}
	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});

	//		}
	//	});


	//};

	$scope.SendManualNoticeToEmployee = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var contentPath = '';
		$timeout(function () {
			$http({
				method: 'POST',
				url: base_url + "Global/UploadAttachments",
				headers: { 'Content-Type': undefined },
				transformRequest: function (data) {
					var formData = new FormData();
					if (data.files) {
						for (var i = 0; i < data.files.length; i++) {
							formData.append("file" + i, data.files[i]);
						}
					}
					return formData;
				},
				data: { files: $scope.newNotice.AttachmentColl }
			}).then(function (res) {
				$scope.loadingstatus = "stop";
				hidePleaseWait();
				if (res.data.IsSuccess == true) {
					if (res.data.Data.length > 0) {
						contentPath = res.data.Data[0];
					}
					$timeout(function () {
						var dataColl = [];
						let tmpCheckedData = [];
						tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
						for (let ent in tmpCheckedData) {
							if (tmpCheckedData[ent].isSelected == true) {
								var objEntity = tmpCheckedData[ent].entity;
								var msg = $scope.newNotice.Description;
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
									//EntityId: entityStudentSummaryForSMS,
									EntityId: entityEmpSummaryForSMS,
									StudentId: objEntity.EmployeeId,
									UserId: objEntity.UserId,
									Title: $scope.newNotice.Title,
									Message: msg,
									ContactNo: objEntity.ContactNo,
									StudentName: objEntity.Name,
									ContentPath: contentPath
								};
								dataColl.push(newSMS);
							}
						}
						$http({
							method: 'POST',
							url: base_url + "Global/SendNotificationToStudent",
							dataType: "json",
							data: JSON.stringify(dataColl)
						}).then(function (sRes) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							Swal.fire(sRes.data.ResponseMSG);
							if (sRes.data.IsSuccess == true) {
								$('#modal-xl').modal('hide');
							}
						});
					});
				}
			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
			});
		});
	};

	//$scope.LoadDOB = function () {

	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	var para = {
	//		dateFrom: ($scope.newBirthday.DateFromDet ? $scope.newBirthday.DateFromDet.dateAD : null),
	//		dateTo: ($scope.newBirthday.DateToDet ? $scope.newBirthday.DateToDet.dateAD : null),
	//	};
	//	$http({
	//		method: 'POST',
	//		url: base_url + "Academic/Report/GetEmployeeBirthDay",
	//		dataType: "json",
	//		data: JSON.stringify(para)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess) {
	//			$scope.gridOptions4.data = res.data.Data;
	//		} else {
	//			alert(res.data.ResponseMSG)
	//			//Swal.fire(res.data.ResponseMSG);
	//		}
	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//}

	//$scope.SendSMSToStudentBirthday = function () {
	//	Swal.fire({
	//		title: 'Do you want to Send SMS To the Birthday Employees?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {
	//			var para1 = {
	//				EntityId: entityEmployeeBirthdayForSMS,
	//				ForATS: 2,
	//				TemplateType: 1
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Setup/Security/GetSENT",
	//				dataType: "json",
	//				data: JSON.stringify(para1)
	//			}).then(function (res) {
	//				if (res.data.IsSuccess && res.data.Data) {
	//					var templatesColl = res.data.Data;
	//					if (templatesColl && templatesColl.length > 0) {
	//						var templatesName = [];
	//						var sno = 1;
	//						angular.forEach(templatesColl, function (tc) {
	//							templatesName.push(sno + '-' + tc.Name);
	//							sno++;
	//						});

	//						var print = false;

	//						var rptTranId = 0;
	//						var selectedTemplate = null;
	//						if (templatesColl.length == 1) {
	//							rptTranId = templatesColl[0].TranId;
	//							selectedTemplate = templatesColl[0];
	//						}
	//						else {
	//							Swal.fire({
	//								title: 'Templates For SMS',
	//								input: 'select',
	//								inputOptions: templatesName,
	//								inputPlaceholder: 'Select a template',
	//								showCancelButton: true,
	//								inputValidator: (value) => {
	//									return new Promise((resolve) => {
	//										if (value >= 0) {
	//											resolve()
	//											rptTranId = templatesColl[value].TranId;
	//											selectedTemplate = templatesColl[value];

	//											if (rptTranId > 0) {
	//												var dataColl = [];
	//												angular.forEach($scope.gridApi4.grid.getVisibleRows(), function (node) {
	//													var objEntity = node.entity;
	//													var tmpContactNo = '';
	//													tmpContactNo = objEntity.ContactNo;

	//													if (tmpContactNo && tmpContactNo.length > 0) {
	//														var msg = selectedTemplate.Description;
	//														for (let x in objEntity) {
	//															var variable = '$$' + x.toLowerCase() + '$$';
	//															if (msg.indexOf(variable) >= 0) {
	//																var val = objEntity[x];
	//																msg = msg.replace(variable, val);
	//															}

	//															if (msg.indexOf('$$') == -1)
	//																break;
	//														}

	//														var newSMS = {
	//															//EntityId: entityStudentSummaryForSMS,
	//															EntityId: entityEmpSummary,
	//															StudentId: objEntity.StudentId,
	//															UserId: objEntity.UserId,
	//															Title: selectedTemplate.Title,
	//															Message: msg,
	//															ContactNo: tmpContactNo,
	//															StudentName: objEntity.Name
	//														};

	//														dataColl.push(newSMS);
	//													}
	//												});

	//												print = true;

	//												$http({
	//													method: 'POST',
	//													url: base_url + "Global/SendSMSToStudent",
	//													dataType: "json",
	//													data: JSON.stringify(dataColl)
	//												}).then(function (sRes) {
	//													Swal.fire(sRes.data.ResponseMSG);
	//													if (sRes.data.IsSuccess && sRes.data.Data) {

	//													}
	//												});

	//											}

	//										} else {
	//											resolve('You need to select:)')
	//										}
	//									})
	//								}
	//							})
	//						}

	//						if (rptTranId > 0 && print == false) {
	//							var dataColl = [];

	//							angular.forEach($scope.gridApi4.grid.getVisibleRows(), function (node) {
	//								var objEntity = node.entity;
	//								var tmpContactNo = '';
	//								tmpContactNo = objEntity.ContactNo;

	//								if (tmpContactNo && tmpContactNo.length > 0) {
	//									var msg = selectedTemplate.Description;
	//									for (let x in objEntity) {
	//										var variable = '$$' + x.toLowerCase() + '$$';
	//										if (msg.indexOf(variable) >= 0) {
	//											var val = objEntity[x];
	//											msg = msg.replace(variable, val);
	//										}

	//										if (msg.indexOf('$$') == -1)
	//											break;
	//									}

	//									var newSMS = {
	//										//EntityId: entityStudentSummaryForSMS,
	//										EntityId: entityEmpSummary,
	//										StudentId: objEntity.StudentId,
	//										UserId: objEntity.UserId,
	//										Title: selectedTemplate.Title,
	//										Message: msg,
	//										ContactNo: tmpContactNo,
	//										StudentName: objEntity.Name
	//									};

	//									dataColl.push(newSMS);
	//								}
	//							});
	//							print = true;

	//							$http({
	//								method: 'POST',
	//								url: base_url + "Global/SendSMSToStudent",
	//								dataType: "json",
	//								data: JSON.stringify(dataColl)
	//							}).then(function (sRes) {
	//								Swal.fire(sRes.data.ResponseMSG);
	//								if (sRes.data.IsSuccess && sRes.data.Data) {

	//								}
	//							});

	//						}

	//					} else
	//						Swal.fire('No Templates found for SMS');
	//				}
	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	});


	//};
	//$scope.SendNoticeToStudentBirthday = function () {
	//	Swal.fire({
	//		title: 'Do you want to Send Notification To the Employee birthday data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {

	//			var para1 = {
	//				EntityId: entityEmployeeBirthdayForSMS,
	//				ForATS: 2,
	//				TemplateType: 3
	//			};

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Setup/Security/GetSENT",
	//				dataType: "json",
	//				data: JSON.stringify(para1)
	//			}).then(function (res) {
	//				if (res.data.IsSuccess && res.data.Data) {
	//					var templatesColl = res.data.Data;
	//					if (templatesColl && templatesColl.length > 0) {
	//						var templatesName = [];
	//						var sno = 1;
	//						angular.forEach(templatesColl, function (tc) {
	//							templatesName.push(sno + '-' + tc.Name);
	//							sno++;
	//						});

	//						var print = false;

	//						var rptTranId = 0;
	//						var selectedTemplate = null;
	//						if (templatesColl.length == 1) {
	//							rptTranId = templatesColl[0].TranId;
	//							selectedTemplate = templatesColl[0];
	//						}
	//						else {
	//							Swal.fire({
	//								title: 'Templates For Notification',
	//								input: 'select',
	//								inputOptions: templatesName,
	//								inputPlaceholder: 'Select a template',
	//								showCancelButton: true,
	//								inputValidator: (value) => {
	//									return new Promise((resolve) => {
	//										if (value >= 0) {
	//											resolve()
	//											rptTranId = templatesColl[value].TranId;
	//											selectedTemplate = templatesColl[value];

	//											if (rptTranId > 0) {
	//												$scope.newNotice.Title = selectedTemplate.Title;
	//												$scope.newNotice.Description = selectedTemplate.Description;
	//												$('#modal-birthday-xl').modal('show');
	//											}

	//										} else {
	//											resolve('You need to select:)')
	//										}
	//									})
	//								}
	//							})
	//						}

	//						if (rptTranId > 0 && print == false) {
	//							$scope.newNotice.Title = selectedTemplate.Title;
	//							$scope.newNotice.Description = selectedTemplate.Description;
	//							$('#modal-birthday-xl').modal('show');
	//						}

	//					} else {
	//						$scope.newNotice.Title = '';
	//						$scope.newNotice.Description = '';
	//						$('#modal-birthday-xl').modal('show');
	//					}

	//				}
	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});

	//		}
	//	});

	//};
	//$scope.SendManualNoticeToStudentBirthday = function () {

	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	var contentPath = '';
	//	$timeout(function () {

	//		$http({
	//			method: 'POST',
	//			url: base_url + "Global/UploadAttachments",
	//			headers: { 'Content-Type': undefined },

	//			transformRequest: function (data) {

	//				var formData = new FormData();
	//				if (data.files) {
	//					for (var i = 0; i < data.files.length; i++) {
	//						formData.append("file" + i, data.files[i]);
	//					}
	//				}

	//				return formData;
	//			},
	//			data: { files: $scope.newNotice.AttachmentColl }
	//		}).then(function (res) {

	//			$scope.loadingstatus = "stop";
	//			hidePleaseWait();

	//			if (res.data.IsSuccess == true) {
	//				if (res.data.Data.length > 0) {
	//					contentPath = res.data.Data[0];
	//				}

	//				$timeout(function () {

	//					var dataColl = [];
	//					angular.forEach($scope.gridApi4.grid.getVisibleRows(), function (node) {
	//						var objEntity = node.entity;
	//						var msg = $scope.newNotice.Description;
	//						for (let x in objEntity) {
	//							var variable = '$$' + x.toLowerCase() + '$$';
	//							if (msg.indexOf(variable) >= 0) {
	//								var val = objEntity[x];
	//								msg = msg.replace(variable, val);
	//							}

	//							if (msg.indexOf('$$') == -1)
	//								break;
	//						}

	//						var newSMS = {
	//							//EntityId: entityStudentSummaryForSMS,
	//							EntityId: entityEmpSummary,
	//							StudentId: objEntity.StudentId,
	//							UserId: objEntity.UserId,
	//							Title: $scope.newNotice.Title,
	//							Message: msg,
	//							ContactNo: objEntity.ContactNo,
	//							StudentName: objEntity.Name,
	//							ContentPath: contentPath
	//						};

	//						dataColl.push(newSMS);
	//					});
	//					$http({
	//						method: 'POST',
	//						url: base_url + "Global/SendNotificationToStudent",
	//						dataType: "json",
	//						data: JSON.stringify(dataColl)
	//					}).then(function (sRes) {
	//						hidePleaseWait();
	//						$scope.loadingstatus = "stop";

	//						Swal.fire(sRes.data.ResponseMSG);
	//						if (sRes.data.IsSuccess == true) {
	//							$('#modal-birthday-xl').modal('hide');
	//						}
	//					});

	//				});

	//			}

	//		}, function (errormessage) {
	//			hidePleaseWait();
	//			$scope.loadingstatus = "stop";

	//		});

	//	});

	//};

	//$scope.PrintEmployeeBirthday = function () {


	//	$http({
	//		method: 'GET',
	//		url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityEmployeeBirthday + "&voucherId=0&isTran=false",
	//		dataType: "json"
	//	}).then(function (res) {
	//		if (res.data.IsSuccess && res.data.Data) {
	//			var templatesColl = res.data.Data;
	//			if (templatesColl && templatesColl.length > 0) {
	//				var templatesName = [];
	//				var sno = 1;
	//				angular.forEach(templatesColl, function (tc) {
	//					templatesName.push(sno + '-' + tc.ReportName);
	//					sno++;
	//				});

	//				var print = false;

	//				var rptTranId = 0;
	//				var selectedTemplate = null;
	//				if (templatesColl.length == 1) {
	//					rptTranId = templatesColl[0].RptTranId;
	//					selectedTemplate = templatesColl[0];
	//				}
	//				else {
	//					Swal.fire({
	//						title: 'Report Templates For Print',
	//						input: 'select',
	//						inputOptions: templatesName,
	//						inputPlaceholder: 'Select a template',
	//						showCancelButton: true,
	//						inputValidator: (value) => {
	//							return new Promise((resolve) => {
	//								if (value >= 0) {
	//									resolve()
	//									selectedTemplate = templatesColl[value];
	//									rptTranId = templatesColl[value].RptTranId;

	//									if (rptTranId > 0) {
	//										if (selectedTemplate.IsRDLC == true) {

	//											var rptPara = {
	//												DateFrom: $filter('date')(new Date(($scope.newBirthday.DateFromDet ? $scope.newBirthday.DateFromDet.dateAD : new Date())), 'yyyy-MM-dd'),
	//												DateTo: $filter('date')(new Date(($scope.newBirthday.DateToDet ? $scope.newBirthday.DateToDet.dateAD : new Date())), 'yyyy-MM-dd'),
	//												rptTranId: rptTranId
	//											};
	//											var paraQuery = param(rptPara);

	//											document.body.style.cursor = 'wait';
	//											document.getElementById("frmRpt").src = '';
	//											document.getElementById("frmRpt").src = base_url + "Academic/Report/RdlEmployeeBirthday?" + paraQuery;
	//											document.body.style.cursor = 'default';
	//											$('#FrmPrintReport').modal('show');
	//										} else {
	//											var dataColl = [];// $scope.gridOptions.data;
	//											angular.forEach($scope.gridApi4.grid.getVisibleRows(), function (node) {
	//												var objEntity = node.entity;
	//												dataColl.push(objEntity);
	//											});

	//											print = true;
	//											$http({
	//												method: 'POST',
	//												url: base_url + "Academic/Report/PrintEmployeeBirthday",
	//												headers: { 'Content-Type': undefined },

	//												transformRequest: function (data) {

	//													var formData = new FormData();
	//													formData.append("jsonData", angular.toJson(data.jsonData));

	//													return formData;
	//												},
	//												data: { jsonData: dataColl }
	//											}).then(function (res) {

	//												$scope.loadingstatus = "stop";
	//												hidePleaseWait();
	//												if (res.data.IsSuccess && res.data.Data) {

	//													document.body.style.cursor = 'wait';
	//													document.getElementById("frmRpt").src = '';
	//													document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityEmployeeBirthday + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
	//													document.body.style.cursor = 'default';
	//													$('#FrmPrintReport').modal('show');

	//												} else
	//													Swal.fire('No Templates found for print');

	//											}, function (errormessage) {
	//												hidePleaseWait();
	//												$scope.loadingstatus = "stop";
	//												Swal.fire(errormessage);
	//											});
	//										}
	//									}

	//								} else {
	//									resolve('You need to select:)')
	//								}
	//							})
	//						}
	//					})
	//				}

	//				if (rptTranId > 0 && print == false) {
	//					if (selectedTemplate.IsRDLC == true) {

	//						var rptPara = {
	//							DateFrom: $filter('date')(new Date(($scope.newBirthday.DateFromDet ? $scope.newBirthday.DateFromDet.dateAD : new Date())), 'yyyy-MM-dd'),
	//							DateTo: $filter('date')(new Date(($scope.newBirthday.DateToDet ? $scope.newBirthday.DateToDet.dateAD : new Date())), 'yyyy-MM-dd'),
	//							rptTranId: rptTranId
	//						};
	//						var paraQuery = param(rptPara);

	//						document.body.style.cursor = 'wait';
	//						document.getElementById("frmRpt").src = '';
	//						document.getElementById("frmRpt").src = base_url + "Academic/Report/RdlEmployeeBirthday?" + paraQuery;
	//						document.body.style.cursor = 'default';
	//						$('#FrmPrintReport').modal('show');

	//					} else {
	//						var dataColl = [];// $scope.gridOptions.data;
	//						angular.forEach($scope.gridApi4.grid.getVisibleRows(), function (node) {
	//							var objEntity = node.entity;
	//							dataColl.push(objEntity);
	//						});
	//						print = true;

	//						$http({
	//							method: 'POST',
	//							url: base_url + "Academic/Report/PrintEmployeeBirthday",
	//							headers: { 'Content-Type': undefined },

	//							transformRequest: function (data) {

	//								var formData = new FormData();
	//								formData.append("jsonData", angular.toJson(data.jsonData));

	//								return formData;
	//							},
	//							data: { jsonData: dataColl }
	//						}).then(function (res) {

	//							$scope.loadingstatus = "stop";
	//							hidePleaseWait();
	//							if (res.data.IsSuccess && res.data.Data) {

	//								document.body.style.cursor = 'wait';
	//								document.getElementById("frmRpt").src = '';
	//								document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityEmployeeBirthday + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
	//								document.body.style.cursor = 'default';
	//								$('#FrmPrintReport').modal('show');

	//							} else
	//								Swal.fire('No Templates found for print');

	//						}, function (errormessage) {
	//							hidePleaseWait();
	//							$scope.loadingstatus = "stop";
	//							Swal.fire(errormessage);
	//						});
	//					}


	//				}

	//			} else
	//				Swal.fire('No Templates found for print');
	//		}
	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//};

	$scope.EmailTemplateChanged = function (st) {
		$scope.CurEmailSend.Temlate = st;
		$scope.CurEmailSend.Title = st.Title;
		$scope.CurEmailSend.Subject = st.Title;
		$scope.CurEmailSend.CC = st.CC;
		$scope.CurEmailSend.Description = st.Description;
	}
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

				if (objEntity.Email && objEntity.Email.length > 0)
					emailColl.push(objEntity.Email);

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
					paraColl.push({ Key: 'EmployeeId', Value: objEntity.EmployeeId });

					var newEmail = {
						EntityId: entityEmpSummary,
						StudentId: objEntity.EmployeeId,
						UserId: 0,
						Title: $scope.CurEmailSend.Temlate.Title,
						Subject: $scope.CurEmailSend.Subject,
						Message: msg,
						CC: ccColl.toString(),
						To: emailColl.toString(),
						StudentName: objEntity.Name,
						ParaColl: paraColl,
						FileName: 'emp-form'
					};
					emailDataColl.push(newEmail);
				}

			});

			if (emailDataColl.length > 0) {


				$scope.loadingstatus = "running";
				showPleaseWait();

				$http({
					method: 'POST',
					url: base_url + "Global/SendEmail",
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


	//New Code Added By Suresh for EmployeeBank Detail
	$scope.GetEmpBankDetail = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetAllEmpBankDetail",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions5.data = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	//New js made for sms
	$scope.OpenSMSModal = function () {
		let tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
		let selectedRows = tmpCheckedData.filter(row => row.isSelected === true);

		if (selectedRows.length === 0) {
			Swal.fire('Please select at least one Employee to send SMS');
			return;
		}

		// Save count to display in modal
		$scope.selectedSMSEmployeeCount = selectedRows.length;

		// Reset model
		$scope.newSMS = { Description: '' };
		$scope.selectedSMSTemplate = null;
		$scope.smsTemplatesColl = [];

		$scope.sendTo = {
			PersonalContact: true,
			OfficeContact: false
			
		};
		// Fetch SMS templates
		var para1 = {
			EntityId: entityEmpSummaryForSMS,
			ForATS: 2,
			TemplateType: 1
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(para1)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.smsTemplatesColl = res.data.Data;

				// Auto-fill if only one template
				if ($scope.smsTemplatesColl.length === 1) {
					$scope.selectedSMSTemplate = $scope.smsTemplatesColl[0];
					$scope.OnSMSTemplateChange();

					let recipients = $scope.selectedSMSTemplate.Recipients || '';
					$scope.sendTo.PersonalContact = recipients.includes('$$personalcontactno$$');
					$scope.sendTo.OfficeContact = recipients.includes('$$officecontactno$$');
					
				}
			} else {
				Swal.fire('No templates found for SMS');
			}

			// Open modal
			$('#modal-sms').modal('show');

		}, function (err) {
			Swal.fire('Failed to load templates');
		});
	};

	$scope.OnSMSTemplateChange = function () {
		if ($scope.selectedSMSTemplate) {
			// Set the description from template
			$scope.newSMS.Description = $scope.selectedSMSTemplate.Description;
			// Auto-check sendTo options based on placeholders in template
			var recipients = $scope.selectedSMSTemplate.Recipients || '';

			$scope.sendTo.PersonalContact = recipients.includes('$$personalcontactno$$');
			$scope.sendTo.OfficeContact = recipients.includes('$$officecontactno$$');
			
		} else {
			$scope.newSMS.Description = '';
			// Reset checkboxes
			$scope.sendTo.PersonalContact = false;
			$scope.sendTo.OfficeContact = false;
			
		}
	};



	$scope.SendSMSToEmp = function () {
		
		let tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
		let selectedRows = tmpCheckedData.filter(row => row.isSelected === true);

		if (selectedRows.length === 0) {
			Swal.fire('Please select at least one Employee');
			return;
		}

		// Ensure at least one contact type is selected
		if (!($scope.sendTo.PersonalContact || $scope.sendTo.OfficeContact)) {
			Swal.fire('Please select at least one contact type to send SMS');
			return;
		}

		Swal.fire({
			title: 'Confirm Send',
			text: 'Do you want to send this SMS to selected Employees?',
			icon: 'question',
			showCancelButton: true,
			confirmButtonText: 'Send',
			cancelButtonText: 'Cancel'
		}).then((result) => {
			if (!result.isConfirmed) return;
			var dataColl = [];
			angular.forEach(selectedRows, function (node) {
				var objEntity = node.entity;
				// Build contact list based on selected checkboxes
				var contacts = [];
				if ($scope.sendTo.PersonalContact && objEntity.PersonalContactNo) contacts.push(objEntity.PersonalContactNo);
				if ($scope.sendTo.OfficeContact && objEntity.OfficeContactNo) contacts.push(objEntity.OfficeContactNo);
				

				angular.forEach(contacts, function (tmpContactNo) {
					var msg = $scope.selectedSMSTemplate.Description;
					// Replace placeholders with actual student data
					for (let x in objEntity) {
						var variable = '$$' + x.toLowerCase() + '$$';
						if (msg.indexOf(variable) >= 0) {
							msg = msg.replace(variable, objEntity[x] || '');
						}
						if (msg.indexOf('$$') === -1) break;
					}
					dataColl.push({
						EntityId: entityEmpSummaryForSMS,
						StudentId: objEntity.EmployeeId,
						UserId: objEntity.UserId,
						Title: $scope.selectedSMSTemplate.Title,
						Message: msg,
						ContactNo: tmpContactNo,
						StudentName: objEntity.Name
					});
				});
			});
			// Send SMS
			$http({
				method: 'POST',
				url: base_url + "Global/SendSMSToStudent",
				dataType: "json",
				data: JSON.stringify(dataColl)
			}).then(function (sRes) {
				Swal.fire(sRes.data.ResponseMSG);
				if (sRes.data.IsSuccess) {
					$('#modal-sms').modal('hide');
				}
			}, function () {
				Swal.fire('Failed to send SMS');
			});

		});

	};

	$scope.SendNoticeToEmployee = function () {

		let tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
		let selectedRows = tmpCheckedData.filter(row => row.isSelected === true);

		if (selectedRows.length === 0) {
			Swal.fire('Please select at least one employee to send notification');
			return;
		}
		$scope.selectedStudentsCount = selectedRows.length;

		$scope.newNotice = {};
		$scope.templatesColl = [];
		$scope.selectedTemplate = null;

		var para1 = {
			EntityId: entityEmpSummaryForSMS,
			ForATS: 2,
			TemplateType: 3
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(para1)
		}).then(function (res) {

			if (res.data.IsSuccess && res.data.Data) {

				$scope.templatesColl = res.data.Data;
				if ($scope.templatesColl.length === 1) {
					$scope.selectedTemplate = $scope.templatesColl[0];
					$scope.OnTemplateChange();
				}
			}

			// Open modal directly
			$('#modal-xl').modal('show');

		}, function () {
			Swal.fire('Failed to load templates');
		});
	};

	$scope.OnTemplateChange = function () {
		if ($scope.selectedTemplate) {
			$scope.newNotice.Title = $scope.selectedTemplate.Title;
			$scope.newNotice.Description = $scope.selectedTemplate.Description;
		} else {
			$scope.newNotice.Title = '';
			$scope.newNotice.Description = '';
		}
	};
});
