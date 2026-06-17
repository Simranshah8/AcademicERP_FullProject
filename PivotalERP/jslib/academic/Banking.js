app.controller('bankingController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {
	$scope.Title = 'Student Report';



	$scope.getterAndSetter=function() {
		 
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
				{ name: "AccountStatus", displayName: "Account Status", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "RegNo", displayName: "Reg. No.", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 160, headerCellClass: 'headerAligment' },			
				{ name: "RollNo", displayName: "Roll No.", minWidth: 100, headerCellClass: 'headerAligment', type: 'number' },
				{ name: "ClassName", displayName: "Class", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "SectionName", displayName: "Section", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father Name", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "Contact No", minWidth: 110, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "AccountNo", displayName: "Account No", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "BranchName", displayName: "Branch", minWidth: 100, headerCellClass: 'headerAligment' },

				{ name: "SessionId", displayName: "SessionId", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ProcessInstanceId", displayName: "ProcessInstanceId", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LastRequestAt", displayName: "Last Request At", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LastResponse", displayName: "Last Response", minWidth: 140, headerCellClass: 'headerAligment' }, 
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
				{ name: "AccountStatus", displayName: "Account Status", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "EmployeeCode", displayName: "Code", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 160, headerCellClass: 'headerAligment' },				 
				{ name: "FatherName", displayName: "Father Name", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "Contact No", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "AccountNo", displayName: "Account No", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "BranchName", displayName: "Branch", minWidth: 100, headerCellClass: 'headerAligment' },

				{ name: "SessionId", displayName: "SessionId", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ProcessInstanceId", displayName: "ProcessInstanceId", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LastRequestAt", displayName: "Last Request At", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LastResponse", displayName: "Last Response", minWidth: 140, headerCellClass: 'headerAligment' },
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

 
	};
	 
	 
	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.entity = {
			StudentSummary: entityStudentSummary
		};

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		 
		$scope.newStudentSummaryList = {
			ClassId: null,
			SectionId: null,
			HouseNameId: null,
			StudentTypeId: null,
			CasteId:null
		};

	 
		$scope.getterAndSetter();

		$scope.newNotice = {
			FilterStudentOnly: true,
			Title: '',
			Description: ''
		};

		$scope.newBirthday = {
			DateFrom_TMP: new Date(),
			DateTo_TMP:new Date()
		};

		   

		var smsPara = {
			EntityId: $scope.entity.StudentSummary,
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
 
	}

	$scope.GetStudentSummaryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
  
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetStudentForAccountOpen",
			dataType: "json",
		//	data: JSON.stringify(para)
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

	$scope.ProcessForNewAccount = function () {
		Swal.fire({
			title: 'Are You Sure To Process for new account opening for selected student',
			showCancelButton: true,
			confirmButtonText: 'yes',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				$scope.loadingstatus = "running";
				showPleaseWait();

				var studentIdColl = [];
				let tmpCheckedData = [];
				tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
				for (let ent in tmpCheckedData) {
					var entityObj = tmpCheckedData[ent];
					if (entityObj.isSelected == true) {
						if (entityObj.entity.AccountStatus == 'Pending')
						{
							var newId = {
								Id: entityObj.entity.StudentId,
								BankId: entityObj.entity.BankId,
                            }
							studentIdColl.push(newId);
						}
					}
				}

				var para1 = {
					studentIdColl:studentIdColl
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/OpenStudentAccount",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					alert(res.data.ResponseMSG)
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.GetEmpSummaryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetEmpForAccountOpen",
			dataType: "json",
			//	data: JSON.stringify(para)
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

	$scope.ProcessForNewAccountEmp = function () {
		Swal.fire({
			title: 'Are You Sure To Process for new account opening for selected employee',
			showCancelButton: true,
			confirmButtonText: 'yes',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				$scope.loadingstatus = "running";
				showPleaseWait();

				var studentIdColl = [];
				let tmpCheckedData = [];
				tmpCheckedData = $scope.gridApi2.grid.getVisibleRows();
				for (let ent in tmpCheckedData) {
					var entityObj = tmpCheckedData[ent];
					if (entityObj.isSelected == true) {
						if (entityObj.entity.AccountStatus == 'Pending') {
							//studentIdColl.push(entityObj.entity.EmployeeId);

							var newId = {
								Id: entityObj.entity.EmployeeId,
								BankId: entityObj.entity.BankId,
							}
							studentIdColl.push(newId);
						}
					}
				}

				var para1 = {
					empIdColl: studentIdColl
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/OpenEmpAccount",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					alert(res.data.ResponseMSG)
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	$scope.CheckAccountStatusEMP = function ()
	{
		Swal.fire({
			title: 'Are You Sure To Process for new account status for selected employee',
			showCancelButton: true,
			confirmButtonText: 'yes',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				$scope.loadingstatus = "running";
				showPleaseWait();

				var studentIdColl = [];
				let tmpCheckedData = [];
				tmpCheckedData = $scope.gridApi2.grid.getVisibleRows();
				for (let ent in tmpCheckedData) {
					var entityObj = tmpCheckedData[ent];
					if (entityObj.isSelected == true) {
						if (entityObj.entity.AccountStatus == 'Processing') {
							//studentIdColl.push(entityObj.entity.EmployeeId);

							var newId = {
								Id: entityObj.entity.EmployeeId,
								BankId: entityObj.entity.BankId,
								CAPId: entityObj.entity.ProcessInstanceId
							}
							studentIdColl.push(newId);
						}
					}
				}

				var para1 = {
					bankIdColl: studentIdColl
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/CheckBAStatus",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					alert(res.data.ResponseMSG)
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.CheckAccountStatusST = function () {
		Swal.fire({
			title: 'Are You Sure To Process for new account status for selected student',
			showCancelButton: true,
			confirmButtonText: 'yes',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				$scope.loadingstatus = "running";
				showPleaseWait();

				var studentIdColl = [];
				let tmpCheckedData = [];
				tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
				for (let ent in tmpCheckedData) {
					var entityObj = tmpCheckedData[ent];
					if (entityObj.isSelected == true) {
						if (entityObj.entity.AccountStatus == 'Processing') {
							//studentIdColl.push(entityObj.entity.EmployeeId);

							var newId = {
								Id: entityObj.entity.StudentId,
								BankId: entityObj.entity.BankId,
								CAPId: entityObj.entity.ProcessInstanceId
							}
							studentIdColl.push(newId);
						}
					}
				}

				var para1 = {
					bankIdColl: studentIdColl
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/CheckBAStatus",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					alert(res.data.ResponseMSG)
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

});