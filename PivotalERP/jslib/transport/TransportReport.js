app.controller('TransportReportController', function ($scope, $http, $timeout, $filter, GlobalServices, uiGridConstants) {
	$scope.Title = 'Transport Report';


	//Added By Suresh on Magh 15 for configuration
	$scope.AcademicConfig = {};
	GlobalServices.getAcademicConfig().then(function (res1) {
		$scope.AcademicConfig = res1.data.Data;


		if ($scope.AcademicConfig.ActiveFaculty == false) {

			findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' });
			if (findInd != -1)
				$scope.gridOptions2.columnDefs.splice(findInd, 1);

			findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' });
			if (findInd != -1)
				$scope.gridOptions.columnDefs.splice(findInd, 1);
		}

		if ($scope.AcademicConfig.ActiveLevel == false) {

			findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Level' });
			if (findInd != -1)
				$scope.gridOptions2.columnDefs.splice(findInd, 1);

			findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Level' });
			if (findInd != -1)
				$scope.gridOptions.columnDefs.splice(findInd, 1);

		}

		if ($scope.AcademicConfig.ActiveSemester == false) {


			findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' });
			if (findInd != -1)
				$scope.gridOptions2.columnDefs.splice(findInd, 1);


			findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' });
			if (findInd != -1)
				$scope.gridOptions.columnDefs.splice(findInd, 1);

		} else {
			$scope.SelectedClassSemesterList = [];
			$scope.SemesterList = [];
			GlobalServices.getSemesterList().then(function (res) {
				$scope.SemesterList = res.data.Data;
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}

		if ($scope.AcademicConfig.ActiveBatch == false) {

			findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'Batch' });
			if (findInd != -1)
				$scope.gridOptions2.columnDefs.splice(findInd, 1);

			findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Batch' });
			if (findInd != -1)
				$scope.gridOptions.columnDefs.splice(findInd, 1);

		} else {
			$scope.BatchList = [];
			GlobalServices.getBatchList().then(function (res) {
				$scope.BatchList = res.data.Data;
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

		if ($scope.AcademicConfig.ActiveClassYear == false) {

			findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' });
			if (findInd != -1)
				$scope.gridOptions2.columnDefs.splice(findInd, 1);

			findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' });
			if (findInd != -1)
				$scope.gridOptions.columnDefs.splice(findInd, 1);

		}
		else {
			$scope.ClassYearList = [];
			$scope.SelectedClassClassYearList = [];
			GlobalServices.getClassYearList().then(function (res) {
				$scope.ClassYearList = res.data.Data;
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

		$scope.gridApi.grid.refresh();



	}, function (reason) {
		Swal.fire('Failed' + reason);
	});
		//Ends


	$scope.getterAndSetter = function () {


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
				{ name: "RegNo", displayName: "Adm.No.", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "Gender", displayName: "Gender", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No.", minWidth: 100, headerCellClass: 'headerAligment' },

				{ name: "ClassSection", displayName: "Class/Sec.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Batch", displayName: "Batch", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "ClassYear", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 140, headerCellClass: 'headerAligment' },
				//{ name: "ClassDetails", displayName: "Class/Sec.", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "RouteName", displayName: "RouteName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PointName", displayName: "PointName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TravelType", displayName: "TravelType", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Rate", displayName: "Rate", minWidth: 140, headerCellClass: 'headerAligment' },
				
				{
					name: "DuesAmt", displayName: "Total Dues", aggregationType: uiGridConstants.aggregationTypes.sum, minWidth: 140, headerCellClass: 'numericAlignment',
					footerCellTemplate: '<div class="ui-grid-cell-contents" >{{col.getAggregationValue() | number:2 }}</div>',
					footerCellClass: 'numericAlignment'
				},

				{
					name: "DebitAmt", displayName: "Transport Fee", aggregationType: uiGridConstants.aggregationTypes.sum, minWidth: 140, headerCellClass: 'numericAlignment',
					footerCellTemplate: '<div class="ui-grid-cell-contents" >{{col.getAggregationValue() | number:2 }}</div>',
					footerCellClass: 'numericAlignment'
				},

				{
					name: "CreditAmt", displayName: "Transport Paid", aggregationType: uiGridConstants.aggregationTypes.sum, minWidth: 140, headerCellClass: 'numericAlignment',
					footerCellTemplate: '<div class="ui-grid-cell-contents" >{{col.getAggregationValue() | number:2 }}</div>',
					footerCellClass: 'numericAlignment'
				},

			
				{ name: "DOB_BS", displayName: "DOB", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "Contact No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father's Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "F_ContactNo", displayName: "Father's ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "MotherName", displayName: "Mother's Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "M_ContactNo", displayName: "Mother's ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "GuardianName", displayName: "GuardianName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "G_ContacNo", displayName: "G_ContacNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 210, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "AdmitDate_BS", displayName: "AdmissionDate", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LedgerPanaNo", displayName: "Ledger Pana No", minWidth: 140, headerCellClass: 'headerAligment' },
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'studentSummary.csv',
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
			exporterExcelFilename: 'studentSummary.xlsx',
			exporterExcelSheetName: 'studentSummary',
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
			}
		};


		$scope.gridOptions2 = {
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
				{ name: "RegNo", displayName: "Adm.No.", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No.", minWidth: 100, headerCellClass: 'headerAligment' },
				//{ name: "ClassDetails", displayName: "Class/Sec.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassSection", displayName: "Class/Sec.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Batch", displayName: "Batch", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "ClassYear", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "RouteName", displayName: "RouteName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PointName", displayName: "PointName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TravelType", displayName: "TravelType", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Rate", displayName: "Rate", minWidth: 140, headerCellClass: 'headerAligment' },

				{
					name: "DuesAmt", displayName: "Total Dues", aggregationType: uiGridConstants.aggregationTypes.sum, minWidth: 140, headerCellClass: 'numericAlignment',
					footerCellTemplate: '<div class="ui-grid-cell-contents" >{{col.getAggregationValue() | number:2 }}</div>',
					footerCellClass: 'numericAlignment'
				},

				{
					name: "DebitAmt", displayName: "Transport Fee", aggregationType: uiGridConstants.aggregationTypes.sum, minWidth: 140, headerCellClass: 'numericAlignment',
					footerCellTemplate: '<div class="ui-grid-cell-contents" >{{col.getAggregationValue() | number:2 }}</div>',
					footerCellClass: 'numericAlignment'
				},

				{
					name: "CreditAmt", displayName: "Transport Paid", aggregationType: uiGridConstants.aggregationTypes.sum, minWidth: 140, headerCellClass: 'numericAlignment',
					footerCellTemplate: '<div class="ui-grid-cell-contents" >{{col.getAggregationValue() | number:2 }}</div>',
					footerCellClass: 'numericAlignment'
				},


				{ name: "DOB_BS", displayName: "DOB", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "Contact No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father's Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "F_ContactNo", displayName: "Father's ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "MotherName", displayName: "Mother's Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "M_ContactNo", displayName: "Mother's ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "GuardianName", displayName: "GuardianName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "G_ContacNo", displayName: "G_ContacNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 210, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "AdmitDate_BS", displayName: "AdmissionDate", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LedgerPanaNo", displayName: "Ledger Pana No", minWidth: 140, headerCellClass: 'headerAligment' },
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'newAdmission.csv',
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
			exporterExcelFilename: 'newAdmission.xlsx',
			exporterExcelSheetName: 'newAdmission',
			onRegisterApi: function (gridApi) {
				$scope.gridApi2 = gridApi;
			}
		};


		$scope.gridOptions3 = {
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
				{ name: "RegNo", displayName: "Adm.No.", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "BoardRegNo", displayName: "BoardRegdNo", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No.", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "ClassSection", displayName: "Class/Sec.", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "RouteName", displayName: "RouteName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PointName", displayName: "PointName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TravelType", displayName: "TravelType", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Rate", displayName: "Rate", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DuesAmt", displayName: "Total Dues", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DebitAmt", displayName: "Transport Fee", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CreditAmt", displayName: "Transport Paid", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "DOB_BS", displayName: "DOB", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "Contact No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father's Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "F_ContactNo", displayName: "Father's ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "MotherName", displayName: "Mother's Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "M_ContactNo", displayName: "Mother's ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "GuardianName", displayName: "GuardianName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "G_ContacNo", displayName: "G_ContacNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 210, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "AdmitDate_BS", displayName: "AdmissionDate", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LeftDate_BS", displayName: "LeftDate", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LeftRemarks", displayName: "Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LedgerPanaNo", displayName: "Ledger Pana No", minWidth: 140, headerCellClass: 'headerAligment' },


			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'leftStudent.csv',
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
			exporterExcelFilename: 'leftStudent.xlsx',
			exporterExcelSheetName: 'leftStudent',
			onRegisterApi: function (gridApi) {
				$scope.gridApi3 = gridApi;
			}
		};


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
				formData.append("entityId", $scope.entity.StudentSummary);
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
	$scope.SendSMSToStudent = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var uIdColl = [];

		angular.forEach($scope.gridOptions.data, function (d) {
			d.SMSText = $scope.newSMS.Description;
			uIdColl.push(d);
		});

		Swal.fire({
			title: 'Are you sure to send SMS ?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				$http({
					method: 'POST',
					url: base_url + "Academic/Report/SendSMSToStudent",
					dataType: "json",
					data: JSON.stringify(uIdColl)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.ClearNotice();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});

	};
	$scope.SendNoticeToStudent = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var uIdColl = '';


		if ($scope.newNotice.FilterStudentOnly == true) {
			var vRows = $scope.gridApi.core.getVisibleRows();
			angular.forEach(vRows, function (rData) {
				var d = rData.entity;

				if (uIdColl.length > 0)
					uIdColl = uIdColl + ',';

				uIdColl = uIdColl + d.UserId;
			});
		} else {
			angular.forEach($scope.gridOptions.data, function (d) {
				if (uIdColl.length > 0)
					uIdColl = uIdColl + ',';

				uIdColl = uIdColl + d.UserId;
			});
		}

		if ($scope.newNotice.FilterStudentOnly == true && uIdColl.length == 0) {
			alert('First Load Student');
			return;
		}


		var filesColl = $scope.newNotice.AttachmentColl;

		var tmpData = {
			studentIdColl: uIdColl,
			title: $scope.newNotice.Title,
			description: $scope.newNotice.Description,
			parents: true
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Report/SendNoticeToStudent",
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
			data: { jsonData: tmpData, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearNotice();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

	};
	$scope.ClearNotice = function () {

		$scope.newNotice = {
			FilterStudentOnly: true,
			Title: '',
			Description: ''
		};

		$scope.newSMS = {
			Description: ''
		};
		$('input[type=file]').val('');
		$('#modal-xl').modal('hide');
		$('#modal-sms').modal('hide');
	}
	$scope.LoadData = function () {

		$scope.ClassIdColl = '';
		$scope.SectionIdColl = '';
		$scope.RouteIdColl = '';
		$scope.PointIdColl = '';

		$('.select2').select2();

		//$scope.MonthList = GlobalServices.getMonthList();
		$scope.MonthList = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		$scope.newDiscountTypewise = {
			DiscountTypewiseId: null,

			Mode: 'Save'
		};

		$scope.entity = {
			StudentSummary: entityStudentSummary
		};

		$scope.confirmMSG = GlobalServices.getConfirmMSG();

		$scope.ClassSection = {};
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
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


		$scope.TransportPointList = [];
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetAllTransportPointList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TransportPointList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newStudentSummaryList = {
			ClassId: null,
			SectionId: null
		};

		$scope.TransportRouteList = [];
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetAllTransportRouteList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TransportRouteList = res.data.Data;

			}  
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.getterAndSetter();

		$scope.newNotice = {
			FilterStudentOnly: true,
			Title: '',
			Description: ''
		};

		//$http({
		//	method: 'GET',
		//	url: base_url + "Global/GetListState?entityId=" + $scope.entity.StudentSummary + "&isReport=true",
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

	}

	$scope.GetStudentSummaryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ClassIdColl: ($scope.ClassIdColl ? $scope.ClassIdColl.toString() : ''),
			SectionIdColl: ($scope.SectionIdColl ? $scope.SectionIdColl.toString() : ''),
			RouteIdColl: ($scope.RouteIdColl ? $scope.RouteIdColl.toString() : ''),
			PointIdColl: ($scope.PointIdColl ? $scope.PointIdColl.toString() : ''),

			BatchIdColl: ($scope.BatchIdColl ? $scope.BatchIdColl.toString() : ''),
			SemesterIdColl: ($scope.SemesterIdColl ? $scope.SemesterIdColl.toString() : ''),
			ClassYearIdColl: ($scope.ClassYearIdColl ? $scope.ClassYearIdColl.toString() : '')
		};
		$http({
			method: 'POST',
			url: base_url + "Transport/Report/GetStudentSummary",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions.data = res.data.Data;

			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	$scope.PrintStudentSummary = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityStudentSummary + "&voucherId=0&isTran=false",
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
											//var dataColl = $scope.gridOptions.data;
											var dataColl = [];// $scope.gridOptions.data;
											angular.forEach($scope.gridApi.grid.getVisibleRows(), function (node) {
												var objEntity = node.entity;
												dataColl.push(objEntity);
											});

											print = true;
											$http({
												method: 'POST',
												url: base_url + "Global/PrintReportData",
												headers: { 'Content-Type': undefined },

												transformRequest: function (data) {

													var formData = new FormData();
													formData.append("entityId", entityStudentSummary);
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
													document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentSummary + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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
						//var dataColl = $scope.gridOptions.data;
						var dataColl = [];// $scope.gridOptions.data;
						angular.forEach($scope.gridApi.grid.getVisibleRows(), function (node) {
							var objEntity = node.entity;
							dataColl.push(objEntity);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Global/PrintReportData",
							headers: { 'Content-Type': undefined },

							transformRequest: function (data) {

								var formData = new FormData();
								formData.append("entityId", entityStudentSummary);
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
								document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentSummary + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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

	$scope.GetStudentSummaryForMonth = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ForMonthId:$scope.forMonth.MonthId
		};
		$scope.gridOptions2.data = [];

		$http({
			method: 'POST',
			url: base_url + "Transport/Report/GetStudentSummaryForMonth",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions2.data = res.data.Data;

			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}


	$scope.PrintTranslistForMonth = function ()
	{
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityStudentSummary + "&voucherId=0&isTran=false",
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
											//var dataColl = $scope.gridOptions2.data;
											var dataColl = [];// $scope.gridOptions.data;
											angular.forEach($scope.gridApi2.grid.getVisibleRows(), function (node) {
												var objEntity = node.entity;
												dataColl.push(objEntity);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Global/PrintReportData",
												headers: { 'Content-Type': undefined },

												transformRequest: function (data) {

													var formData = new FormData();
													formData.append("entityId", entityStudentSummary);
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
													document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentSummary + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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
						//var dataColl = $scope.gridOptions2.data;
						var dataColl = [];// $scope.gridOptions.data;
						angular.forEach($scope.gridApi.grid.getVisibleRows(), function (node) {
							var objEntity = node.entity;
							dataColl.push(objEntity);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Global/PrintReportData",
							headers: { 'Content-Type': undefined },

							transformRequest: function (data) {

								var formData = new FormData();
								formData.append("entityId", entityStudentSummary);
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
								document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentSummary + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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
	 
});