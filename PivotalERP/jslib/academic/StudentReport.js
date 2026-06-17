app.controller('StudentReportController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {
	$scope.Title = 'Student Report';
	 

	$rootScope.ConfigFunction = function () {
		var keyColl = $translate.getTranslationTable();

		var Labels = {
			RegdNo: keyColl ? keyColl['REGDNO_LNG'] : 'Reg. No.',
			Cast: keyColl ? keyColl['CAST_LNG'] : 'Cast',
			CITIZENSHIP: keyColl ? keyColl['CITIZENSHIP_LNG'] : 'Citizenship',
			LOCAL_LEVEL: keyColl ? keyColl['LOCAL_LEVEL_LNG'] : 'Local Level',
			PROVINCE: keyColl ? keyColl['PROVINCE_LNG'] : 'Province'
		};
		if ($rootScope.LANG == 'in') {

			// Student Summary
			$scope.gridApi.grid.getColumn('DOB_AD').colDef.displayName = 'DOB';
			$scope.gridApi.grid.getColumn('DOB_AD').displayName = 'DOB';
			$scope.gridApi.grid.getColumn('RegNo').colDef.displayName = Labels.RegdNo;
			$scope.gridApi.grid.getColumn('RegNo').displayName = Labels.RegdNo;

			$scope.gridApi.grid.getColumn('Caste').colDef.displayName = Labels.Cast;
			$scope.gridApi.grid.getColumn('Caste').displayName = Labels.Cast;

			$scope.gridApi.grid.getColumn('CitizenshipNo').colDef.displayName = Labels.CITIZENSHIP;
			$scope.gridApi.grid.getColumn('CitizenshipNo').displayName = Labels.CITIZENSHIP;

			$scope.gridApi.grid.getColumn('PA_LocalLevel').colDef.displayName = Labels.LOCAL_LEVEL;
			$scope.gridApi.grid.getColumn('PA_LocalLevel').displayName = Labels.LOCAL_LEVEL;

			$scope.gridApi.grid.getColumn('PA_Province').colDef.displayName = Labels.PROVINCE;
			$scope.gridApi.grid.getColumn('PA_Province').displayName = Labels.PROVINCE;


			var findInd = -1;
			findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'DOB_BS' });

			if (findInd != -1)
				$scope.gridOptions.columnDefs.splice(findInd, 1);

			$scope.gridApi.grid.getColumn('AdmitDate_AD').colDef.displayName = 'Admission Date';
			$scope.gridApi.grid.getColumn('AdmitDate_AD').displayName = 'Admission Date';

			findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'AdmitDate_BS' });
			if (findInd != -1)
				$scope.gridOptions.columnDefs.splice(findInd, 1);

			findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'EMSId' });
			if (findInd != -1)
				$scope.gridOptions.columnDefs.splice(findInd, 1);


			// New Admission List
			$scope.gridApi2.grid.getColumn('DOB_AD').colDef.displayName = 'DOB';
			$scope.gridApi2.grid.getColumn('DOB_AD').displayName = 'DOB';
			$scope.gridApi2.grid.getColumn('RegNo').colDef.displayName = Labels.RegdNo;
			$scope.gridApi2.grid.getColumn('RegNo').displayName = Labels.RegdNo;
			findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'DOB_BS' });

			if (findInd != -1)
				$scope.gridOptions2.columnDefs.splice(findInd, 1);

			$scope.gridApi2.grid.getColumn('AdmitDate_AD').colDef.displayName = 'Admission Date';
			$scope.gridApi2.grid.getColumn('AdmitDate_AD').displayName = 'Admission Date';

			findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'AdmitDate_BS' });
			if (findInd != -1)
				$scope.gridOptions2.columnDefs.splice(findInd, 1);

			findInd = $scope.gridOptions2.columnDefs.findIndex(function (obj) { return obj.name == 'EMSId' });
			if (findInd != -1)
				$scope.gridOptions2.columnDefs.splice(findInd, 1);


			// Left Student List
			$scope.gridApi3.grid.getColumn('DOB_AD').colDef.displayName = 'DOB';
			$scope.gridApi3.grid.getColumn('DOB_AD').displayName = 'DOB';
			$scope.gridApi3.grid.getColumn('RegNo').colDef.displayName = Labels.RegdNo;
			$scope.gridApi3.grid.getColumn('RegNo').displayName = Labels.RegdNo;
			findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'DOB_BS' });

			if (findInd != -1)
				$scope.gridOptions3.columnDefs.splice(findInd, 1);

			$scope.gridApi3.grid.getColumn('AdmitDate_AD').colDef.displayName = 'Admission Date';
			$scope.gridApi3.grid.getColumn('AdmitDate_AD').displayName = 'Admission Date';

			findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'AdmitDate_BS' });
			if (findInd != -1)
				$scope.gridOptions3.columnDefs.splice(findInd, 1);

			findInd = $scope.gridOptions3.columnDefs.findIndex(function (obj) { return obj.name == 'EMSId' });
			if (findInd != -1)
				$scope.gridOptions3.columnDefs.splice(findInd, 1);


			// Birthday Student List
			$scope.gridApi4.grid.getColumn('DOB_AD').colDef.displayName = 'DOB';
			$scope.gridApi4.grid.getColumn('DOB_AD').displayName = 'DOB';
			$scope.gridApi4.grid.getColumn('RegdNo').colDef.displayName = Labels.RegdNo;
			$scope.gridApi4.grid.getColumn('RegdNo').displayName = Labels.RegdNo;
			findInd = $scope.gridOptions4.columnDefs.findIndex(function (obj) { return obj.name == 'DOB_BS' });
			if (findInd != -1)
				$scope.gridOptions4.columnDefs.splice(findInd, 1);


			// Sibling Student List

			$scope.gridApi5.grid.getColumn('RegNo').colDef.displayName = Labels.RegdNo;
			$scope.gridApi5.grid.getColumn('RegNo').displayName = Labels.RegdNo;
			$scope.gridApi5.grid.getColumn('P_RegNo').colDef.displayName = 'P_' + Labels.RegdNo;
			$scope.gridApi5.grid.getColumn('P_RegNo').displayName = 'P_' + Labels.RegdNo;


		}


		$scope.BranchColl = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchListForEntry",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchColl = res.data.Data;
			}
		}, function (reason) {
			alert('Failed' + reason);
		});

		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;


			if ($scope.AcademicConfig.ActiveFaculty == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Faculty' });
				if (findInd != -1)
					$scope.gridOptions.columnDefs.splice(findInd, 1);
			}

			if ($scope.AcademicConfig.ActiveLevel == false) {

				findInd = $scope.gridOptions.columnDefs.findIndex(function (obj) { return obj.name == 'Level' });
				if (findInd != -1)
					$scope.gridOptions.columnDefs.splice(findInd, 1);

			}

			if ($scope.AcademicConfig.ActiveSemester == false) {


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

			if ($scope.gridApi) {
				$timeout(function () {
					$scope.gridApi.grid.refresh();
				});
            }
					

			if ($scope.gridApi2) {
				$timeout(function () {
					$scope.gridApi2.grid.refresh();
				});
			}
			
			
			if ($scope.gridApi3) {
				$timeout(function () {
					$scope.gridApi3.grid.refresh();
				});
			}
			
			
			if ($scope.gridApi4) {
				$timeout(function () {
					$scope.gridApi4.grid.refresh();
				});

			}
		
			
			if ($scope.gridApi5) {
				$timeout(function () {
					$scope.gridApi5.grid.refresh();
				});
			}
		
			


		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
	$rootScope.ChangeLanguage();

	$scope.getterAndSetter = function () {

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
				{ name: "AutoNumber", displayName: "Id", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "RegNo", displayName: "Regd.No.", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "BoardRegNo", displayName: "BoardRegdNo", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "Gender", displayName: "Gender", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No.", minWidth: 100, headerCellClass: 'headerAligment', type: 'number' },
				{ name: "ClassSection", displayName: "Class/Sec.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Level", displayName: "Level", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Faculty", displayName: "Faculty", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Batch", displayName: "Batch", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "Year", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "DOB_BS", displayName: "DOB(B.S.)", minWidth: 100, headerCellClass: 'headerAligment' },
				{
					name: "DOB_AD", displayName: "DOB(A.D.)", minWidth: 100, headerCellClass: 'headerAligment', type: 'date',
					cellTemplate: '<div>{{row.entity.DOB_AD |dateFormat}}</div>',
					sortFn: function (aDate, bDate) {
						//debugger;
						var a = new Date(aDate);
						var b = new Date(bDate);
						if (a < b) {
							return -1;
						}
						else if (a > b) {
							return 1;
						}
						else {
							return 0;
						}
					}
				},
				{ name: "ContactNo", displayName: "Contact No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Email", displayName: "Email Id", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father's Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "F_ContactNo", displayName: "Father's ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "MotherName", displayName: "Mother's Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "M_ContactNo", displayName: "Mother's ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "GuardianName", displayName: "GuardianName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "G_ContacNo", displayName: "G_ContacNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "P.Address", minWidth: 210, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "CurrentAddress", displayName: "Current Address", minWidth: 210, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "AdmitDate_BS", displayName: "AdmissionDate(B.S.)", minWidth: 140, headerCellClass: 'headerAligment' },
				{
					name: "AdmitDate_AD", displayName: "AdmissionDate(A.D.)", minWidth: 140, headerCellClass: 'headerAligment', type: 'date',
					cellTemplate: '<div>{{row.entity.AdmitDate_AD |dateFormat}}</div>',
					sortFn: function (aDate, bDate) {
						//debugger;
						var a = new Date(aDate);
						var b = new Date(bDate);
						if (a < b) {
							return -1;
						}
						else if (a > b) {
							return 1;
						}
						else {
							return 0;
						}
					}
				},
				{ name: "CardNo", displayName: "Card No", minWidth: 140, headerCellClass: 'headerAligment', type: 'number' },
				{ name: "EnrollNo", displayName: "Enroll No.", minWidth: 140, headerCellClass: 'headerAligment', type: 'number' },
				{ name: "Caste", displayName: "Caste", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "HouseName", displayName: "House Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Medium", displayName: "Medium", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "StudentType", displayName: "Student Type", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "AgeRange", displayName: "Age Range", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LedgerPanaNo", displayName: "Ledger PanaNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "EMSId", displayName: "IEMIS Code", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "IsNew", displayName: "Is New", minWidth: 120, headerCellClass: 'headerAligment' },
				{ name: "FatherOccupation", displayName: "Father Occupation", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "GuardianOccupation", displayName: "Guardian Occupation", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "MotherOccupation", displayName: "Mother Occupation", minWidth: 150, headerCellClass: 'headerAligment' },

				{ name: "CitizenshipNo", displayName: "Citizenship No", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "MotherTongue", displayName: "Mother Tongue", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "Height", displayName: "Height", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "Weight", displayName: "Weight", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "PhysicalDisability", displayName: "Physical Disability", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "Aim", displayName: "Aim", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "BirthCertificateNo", displayName: "Birth Certificate No", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "Remarks", displayName: "Remarks", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "PA_Province", displayName: "Province/State", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "PA_District", displayName: "District", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "PA_LocalLevel", displayName: "LocalLevel", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "PA_Village", displayName: "Village", minWidth: 150, headerCellClass: 'headerAligment' },

				{ name: "F_Email", displayName: "F_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "M_Email", displayName: "M_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "G_Email", displayName: "G_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "ClassName", displayName: "Class", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "SectionName", displayName: "Section", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "BloodGroup", displayName: "Blood Group", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "Branch", displayName: "School", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "AgeStr", displayName: "Age Det", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "NameNP", displayName: "नाम", minWidth: 150, headerCellClass: 'headerAligment' },
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
				{ name: "RegNo", displayName: "Regd.No.", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "BoardRegNo", displayName: "BoardRegdNo", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "Gender", displayName: "Gender", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No.", minWidth: 100, headerCellClass: 'headerAligment', type: 'number' },
				{ name: "ClassSection", displayName: "Class/Sec.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DOB_BS", displayName: "DOB", minWidth: 100, headerCellClass: 'headerAligment' },
				{
					name: "DOB_AD", displayName: "DOB(A.D.)", minWidth: 100, headerCellClass: 'headerAligment', type: 'date',
					cellTemplate: '<div>{{row.entity.DOB_AD |dateFormat}}</div>',
					sortFn: function (aDate, bDate) {
						//debugger;
						var a = new Date(aDate);
						var b = new Date(bDate);
						if (a < b) {
							return -1;
						}
						else if (a > b) {
							return 1;
						}
						else {
							return 0;
						}
					}
				},
				{ name: "ContactNo", displayName: "Contact No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Email", displayName: "Email Id", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father's Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "F_ContactNo", displayName: "Father's ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "MotherName", displayName: "Mother's Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "M_ContactNo", displayName: "Mother's ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "GuardianName", displayName: "GuardianName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "G_ContacNo", displayName: "G_ContacNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "P.Address", minWidth: 210, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "CurrentAddress", displayName: "Current Address", minWidth: 210, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "AdmitDate_BS", displayName: "AdmissionDate", minWidth: 140, headerCellClass: 'headerAligment' },
				{
					name: "AdmitDate_AD", displayName: "AdmissionDate(A.D.)", minWidth: 140, headerCellClass: 'headerAligment', type: 'date',
					cellTemplate: '<div>{{row.entity.AdmitDate_AD |dateFormat}}</div>',
					sortFn: function (aDate, bDate) {
						//debugger;
						var a = new Date(aDate);
						var b = new Date(bDate);
						if (a < b) {
							return -1;
						}
						else if (a > b) {
							return 1;
						}
						else {
							return 0;
						}
					}
				},
				{ name: "CardNo", displayName: "CardNo", minWidth: 140, headerCellClass: 'headerAligment', type: 'number' },
				{ name: "AgeRange", displayName: "Age Range", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "F_Email", displayName: "F_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "M_Email", displayName: "M_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "G_Email", displayName: "G_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "EMSId", displayName: "IEMIS Code", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "AgeStr", displayName: "Age Det", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "NameNP", displayName: "नाम", minWidth: 150, headerCellClass: 'headerAligment' },
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
				{ name: "RegNo", displayName: "Regd.No.", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "BoardRegNo", displayName: "BoardRegdNo", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "Gender", displayName: "Gender", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No.", minWidth: 100, headerCellClass: 'headerAligment', type: 'number' },
				{ name: "ClassSection", displayName: "Class/Sec.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DOB_BS", displayName: "DOB", minWidth: 100, headerCellClass: 'headerAligment' },
				{
					name: "DOB_AD", displayName: "DOB(A.D.)", minWidth: 100, headerCellClass: 'headerAligment', type: 'date',
					cellTemplate: '<div>{{row.entity.DOB_AD |dateFormat}}</div>',
					sortFn: function (aDate, bDate) {
						//debugger;
						var a = new Date(aDate);
						var b = new Date(bDate);
						if (a < b) {
							return -1;
						}
						else if (a > b) {
							return 1;
						}
						else {
							return 0;
						}
					}
				},
				{ name: "ContactNo", displayName: "Contact No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Email", displayName: "Email Id", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father's Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "F_ContactNo", displayName: "Father's ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "MotherName", displayName: "Mother's Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "M_ContactNo", displayName: "Mother's ContactNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "GuardianName", displayName: "GuardianName", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "G_ContacNo", displayName: "G_ContacNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "P.Address", minWidth: 210, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "CurrentAddress", displayName: "Current Address", minWidth: 210, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "AdmitDate_BS", displayName: "AdmissionDate", minWidth: 140, headerCellClass: 'headerAligment' },
				{
					name: "AdmitDate_AD", displayName: "AdmissionDate(A.D.)", minWidth: 140, headerCellClass: 'headerAligment', type: 'date',
					cellTemplate: '<div>{{row.entity.AdmitDate_AD |dateFormat}}</div>',
					sortFn: function (aDate, bDate) {
						//debugger;
						var a = new Date(aDate);
						var b = new Date(bDate);
						if (a < b) {
							return -1;
						}
						else if (a > b) {
							return 1;
						}
						else {
							return 0;
						}
					}
				},
				{ name: "LeftDate_BS", displayName: "LeftDate", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "LeftRemarks", displayName: "Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CardNo", displayName: "CardNo", minWidth: 140, headerCellClass: 'headerAligment', type: 'number' },
				{ name: "AgeRange", displayName: "Age Range", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "F_Email", displayName: "F_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "M_Email", displayName: "M_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "G_Email", displayName: "G_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "EMSId", displayName: "IEMIS Code", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "AgeStr", displayName: "Age Det", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "NameNP", displayName: "नाम", minWidth: 150, headerCellClass: 'headerAligment' },
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

		$scope.gridOptions4 = {
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
				{ name: "RegdNo", displayName: "Regd.No.", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No.", minWidth: 100, headerCellClass: 'headerAligment', type: 'number' },
				{ name: "ClassSection", displayName: "Class/Sec.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DOB_BS", displayName: "DOB(B.S.)", minWidth: 100, headerCellClass: 'headerAligment' },
				{
					name: "DOB_AD", displayName: "DOB(A.D.)", minWidth: 100, headerCellClass: 'headerAligment', type: 'date',
					cellTemplate: '<div>{{row.entity.DOB_AD |dateFormat}}</div>',
					sortFn: function (aDate, bDate) {
						//debugger;
						var a = new Date(aDate);
						var b = new Date(bDate);
						if (a < b) {
							return -1;
						}
						else if (a > b) {
							return 1;
						}
						else {
							return 0;
						}
					}
				},
				{ name: "Age", displayName: "Age", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "Contact No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Email", displayName: "Email", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father's Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "P.Address", minWidth: 210, headerCellClass: $scope.highlightFilteredHeader, headerCellClass: 'headerAligment' },
				{ name: "F_Email", displayName: "F_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "M_Email", displayName: "M_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "G_Email", displayName: "G_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "EMSId", displayName: "IEMIS Code", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "AgeStr", displayName: "Age Det", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "NameNP", displayName: "नाम", minWidth: 150, headerCellClass: 'headerAligment' },
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'studentbirthday.csv',
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
			exporterExcelFilename: 'studentbirthday.xlsx',
			exporterExcelSheetName: 'studentbirthday',
			onRegisterApi: function (gridApi) {
				$scope.gridApi4 = gridApi;
			}
		};


		$scope.gridOptions5 = {
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
				{ name: "P_ClassName", displayName: "P_ClassName", minWidth: 90, headerCellClass: 'headerAligment' },
				{ name: "P_SectionName", displayName: "P_SectionName", minWidth: 160, headerCellClass: 'headerAligment' },
				{ name: "P_RollNo", displayName: "P_RollNo", minWidth: 100, headerCellClass: 'headerAligment', type: 'number' },
				{ name: "P_RegNo", displayName: "P_RegNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "P_Name", displayName: "P_Name", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "FatherName)", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "ContactNo", displayName: "ContactNo", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassName", displayName: "ClassName", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "SectionName", displayName: "SectionName", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "RollNo", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "RegNo", displayName: "RegNo", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "Relation", displayName: "Relation", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Remarks", displayName: "Remarks", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "F_Email", displayName: "F_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "M_Email", displayName: "M_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "G_Email", displayName: "G_Email", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "EMSId", displayName: "IEMIS Code", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "AgeStr", displayName: "Age Det", minWidth: 150, headerCellClass: 'headerAligment' },
				{ name: "NameNP", displayName: "नाम", minWidth: 150, headerCellClass: 'headerAligment' },
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'studentbirthday.csv',
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
			exporterExcelFilename: 'studentbirthday.xlsx',
			exporterExcelSheetName: 'studentbirthday',
			onRegisterApi: function (gridApi) {
				$scope.gridApi5 = gridApi;
			}
		};
	};
	$scope.AddAgeList = function (ind) {
		if ($scope.AgeList) {
			if ($scope.AgeList.length > ind + 1) {
				$scope.AgeList.splice(ind + 1, 0, {
					Age: 0
				})
			} else {
				$scope.AgeList.push({
					Age: 0
				})
			}
		}

	};
	$scope.DelAgeList = function (ind) {
		if ($scope.AgeList) {
			if ($scope.AgeList.length > 1) {
				$scope.AgeList.splice(ind, 1);
			}
		}
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

	//New js made for sms
	$scope.OpenSMSModal = function () {

		// Get selected students
		let tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
		let selectedRows = tmpCheckedData.filter(row => row.isSelected === true);

		if (selectedRows.length === 0) {
			Swal.fire('Please select at least one student to send SMS');
			return;
		}

		// Save count to display in modal
		$scope.selectedSMSStudentsCount = selectedRows.length;

		// Reset model
		$scope.newSMS = { Description: '' };
		$scope.selectedSMSTemplate = null;
		$scope.smsTemplatesColl = [];

		$scope.sendTo = {
			StudentContact: false,
			FatherContact: true,
			MotherContact: false,
			GuardianContact: false
		};
		// Fetch SMS templates
		var para1 = {
			EntityId: entityStudentSummaryForSMS,
			ForATS: 3,
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
					$scope.sendTo.StudentContact = recipients.includes('$$contactno$$');
					$scope.sendTo.FatherContact = recipients.includes('$$f_contactno$$');
					$scope.sendTo.MotherContact = recipients.includes('$$m_contactno$$');
					$scope.sendTo.GuardianContact = recipients.includes('$$g_contacno$$');
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
			$scope.newSMS.Title = $scope.selectedSMSTemplate.Title;
			// Auto-check sendTo options based on placeholders in template
			var recipients = $scope.selectedSMSTemplate.Recipients || '';

			$scope.sendTo.StudentContact = recipients.includes('$$contactno$$');
			$scope.sendTo.FatherContact = recipients.includes('$$f_contactno$$');
			$scope.sendTo.MotherContact = recipients.includes('$$m_contactno$$');
			$scope.sendTo.GuardianContact = recipients.includes('$$g_contacno$$');
		} else {
			$scope.newSMS.Description = '';
			scope.newSMS.Title = '';
			// Reset checkboxes
			$scope.sendTo.StudentContact = false;
			$scope.sendTo.FatherContact = false;
			$scope.sendTo.MotherContact = false;
			$scope.sendTo.GuardianContact = false;
		}
	};

	$scope.SendSMSToStudent = function () {

		
		let tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
		let selectedRows = tmpCheckedData.filter(row => row.isSelected === true);

		if (selectedRows.length === 0) {
			Swal.fire('Please select at least one student');
			return;
		}

		// Ensure at least one contact type is selected
		if (!($scope.sendTo.StudentContact || $scope.sendTo.FatherContact || $scope.sendTo.MotherContact || $scope.sendTo.GuardianContact)) {
			Swal.fire('Please select at least one contact type to send SMS');
			return;
		}

		Swal.fire({
			title: 'Confirm Send',
			text: 'Do you want to send this SMS to selected students?',
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
				if ($scope.sendTo.StudentContact && objEntity.ContactNo) contacts.push(objEntity.ContactNo);
				if ($scope.sendTo.FatherContact && objEntity.F_ContactNo) contacts.push(objEntity.F_ContactNo);
				if ($scope.sendTo.MotherContact && objEntity.M_ContactNo) contacts.push(objEntity.M_ContactNo);
				if ($scope.sendTo.GuardianContact && objEntity.G_ContactNo) contacts.push(objEntity.G_ContacNo);

				angular.forEach(contacts, function (tmpContactNo) {
				/*	var msg = $scope.selectedSMSTemplate.Description;*/
					var msg = $scope.newSMS.Description;

					// Replace placeholders with actual student data
					for (let x in objEntity) {
						var variable = '$$' + x.toLowerCase() + '$$';
						if (msg.indexOf(variable) >= 0) {
							msg = msg.replace(variable, objEntity[x] || '');
						}
						if (msg.indexOf('$$') === -1) break;
					}

					dataColl.push({
						EntityId: entityStudentSummary,
						StudentId: objEntity.StudentId,
						UserId: objEntity.UserId,
						/*Title: $scope.selectedSMSTemplate.Title,*/
						Title: $scope.newSMS && $scope.newSMS.Title ? $scope.newSMS.Title : '',
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


	//Ends

	//$scope.SendSMSToStudent = function () {
	//	Swal.fire({
	//		title: 'Do you want to Send SMS To the filter data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		/* Read more about isConfirmed, isDenied below */
	//		if (result.isConfirmed) {
	//			var para1 = {
	//				EntityId: entityStudentSummaryForSMS,
	//				ForATS: 3,
	//				TemplateType: 1
	//			};

	//			var checkedItemOnly = false;
	//			let tmpCheckedData = [];
	//			tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
	//			for (let ent in tmpCheckedData) {
	//				if (tmpCheckedData[ent].isSelected == true) {
	//					checkedItemOnly = true;
	//					break;
	//				}
	//			}

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
	//												for (let nInd in tmpCheckedData) {
	//													if (tmpCheckedData.length == (parseInt(nInd)) || Number.isNaN(parseInt(nInd)) == true)
	//														break;

	//													let node = tmpCheckedData[nInd];
	//													if (checkedItemOnly == true && node.isSelected == false)
	//														continue;

	//													var objEntity = node.entity;
	//													var tmpContactNo = '';

	//													var tempContactNo = selectedTemplate.Recipients;
	//													if (tempContactNo && tempContactNo.length > 0) {
	//														if (tempContactNo.indexOf('$$') >= 0) {
	//															tempContactNo = tempContactNo.replace('$$contactno$$', objEntity.ContactNo);
	//														}

	//														if (tempContactNo.indexOf('$$') >= 0) {
	//															tempContactNo = tempContactNo.replace('$$f_contactno$$', objEntity.F_ContactNo);
	//														}
	//													}

	//													if (tempContactNo && tempContactNo.length > 0) {
	//														tmpContactNo = tempContactNo;
	//													} else {
	//														if (!objEntity.F_ContactNo)
	//															tmpContactNo = objEntity.ContactNo;
	//														else
	//															tmpContactNo = objEntity.F_ContactNo;
	//													}


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
	//															EntityId: entityStudentSummary,
	//															StudentId: objEntity.StudentId,
	//															UserId: objEntity.UserId,
	//															Title: selectedTemplate.Title,
	//															Message: msg,
	//															ContactNo: tmpContactNo,
	//															StudentName: objEntity.Name
	//														};

	//														dataColl.push(newSMS);
	//													}
	//												};

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

	//							for (let nInd in tmpCheckedData) {
	//								if (tmpCheckedData.length == (parseInt(nInd)) || Number.isNaN(parseInt(nInd)) == true)
	//									break;

	//								let node = tmpCheckedData[nInd];

	//								if (checkedItemOnly == true && node.isSelected == false)
	//									continue;


	//								var objEntity = node.entity;
	//								var tmpContactNo = '';

	//								var tempContactNo = selectedTemplate.Recipients;
	//								if (tempContactNo && tempContactNo.length > 0) {
	//									if (tempContactNo.indexOf('$$') >= 0) {
	//										tempContactNo = tempContactNo.replace('$$contactno$$', objEntity.ContactNo);
	//									}

	//									if (tempContactNo.indexOf('$$') >= 0) {
	//										tempContactNo = tempContactNo.replace('$$f_contactno$$', objEntity.F_ContactNo);
	//									}
	//								}

	//								if (tempContactNo && tempContactNo.length > 0) {
	//									tmpContactNo = tempContactNo;
	//								} else {
	//									if (!objEntity.F_ContactNo)
	//										tmpContactNo = objEntity.ContactNo;
	//									else
	//										tmpContactNo = objEntity.F_ContactNo;
	//								}


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
	//										EntityId: entityStudentSummary,
	//										StudentId: objEntity.StudentId,
	//										UserId: objEntity.UserId,
	//										Title: selectedTemplate.Title,
	//										Message: msg,
	//										ContactNo: tmpContactNo,
	//										StudentName: objEntity.Name
	//									};

	//									dataColl.push(newSMS);
	//								}
	//							};
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

	//					}
	//					else {
	//						Swal.fire('No Templates found for SMS');
 //                       }
							
	//				}
	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	});


	//};
	//$scope.SendNoticeToStudent = function () {
	//	Swal.fire({
	//		title: 'Do you want to Send Notification To the filter data?',
	//		showCancelButton: true,
	//		confirmButtonText: 'Send',
	//	}).then((result) => {
	//		if (result.isConfirmed) {

	//			var para1 = {
	//				EntityId: entityStudentSummaryForSMS,
	//				ForATS: 3,
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
	$scope.ResetNotification = function () {
		$scope.newNotice = {
			Title: '',
			Description: '',
			AttachmentColl: ''
		};
		$scope.selectedTemplate = null;
		var fileInput = document.querySelector('#modal-xl input[type="file"]');
		if (fileInput) {
			fileInput.value = null;
		}
	};


	$scope.SendNoticeToStudent = function () {

		let tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
		let selectedRows = tmpCheckedData.filter(row => row.isSelected === true);

		if (selectedRows.length === 0) {
			Swal.fire('Please select at least one student to send notification');
			return;
		}
		$scope.selectedStudentsCount = selectedRows.length;

		$scope.newNotice = {};
		$scope.templatesColl = [];
		$scope.selectedTemplate = null;

		var para1 = {
			EntityId: entityStudentSummaryForSMS,
			ForATS: 3,
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


	$scope.SendManualNoticeToStudent = function () {

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

						$scope.loadingstatus = "running";
						showPleaseWait();


						var checkedItemOnly = false;
						let tmpCheckedData = [];
						tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
						var dataColl = [];
						for (let ent in tmpCheckedData) {
							if (tmpCheckedData[ent].isSelected == true) {
								//checkedItemOnly = true;
								//break;

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
									EntityId: entityStudentSummary,
									StudentId: objEntity.StudentId,
									UserId: objEntity.UserId,
									Title: $scope.newNotice.Title,
									Message: msg,
									ContactNo: objEntity.F_ContactNo,
									StudentName: objEntity.Name,
									ContentPath: contentPath
								};

								dataColl.push(newSMS);

							}
						}


						//var dataColl = [];
						//angular.forEach($scope.gridApi.grid.getVisibleRows(), function (node) {
						//	var objEntity = node.entity;
						//	var msg = $scope.newNotice.Description;
						//	for (let x in objEntity) {
						//		var variable = '$$' + x.toLowerCase() + '$$';
						//		if (msg.indexOf(variable) >= 0) {
						//			var val = objEntity[x];
						//			msg = msg.replace(variable, val);
						//		}

						//		if (msg.indexOf('$$') == -1)
						//			break;
						//	}

						//	var newSMS = {
						//		//EntityId: entityStudentSummaryForSMS,
						//		EntityId: entityStudentSummary,
						//		StudentId: objEntity.StudentId,
						//		UserId: objEntity.UserId,
						//		Title: $scope.newNotice.Title,
						//		Message: msg,
						//		ContactNo: objEntity.F_ContactNo,
						//		StudentName: objEntity.Name,
						//		ContentPath: contentPath
						//	};

						//	dataColl.push(newSMS);
						//});
						Swal.fire({
							title: 'Confirm Send',
							text: 'Do you want to send this notification to selected students?',
							icon: 'question',
							showCancelButton: true,
							confirmButtonText: 'Send',
							cancelButtonText: 'Cancel'
						}).then((result) => {
							if (!result.isConfirmed) return;

							$scope.loadingstatus = "running";
							showPleaseWait();

							// Send notification
							$http({
								method: 'POST',
								url: base_url + "Global/SendNotificationToStudent",
								dataType: "json",
								data: JSON.stringify(dataColl)
							}).then(function (sRes) {

								hidePleaseWait();
								$scope.loadingstatus = "stop";

								Swal.fire(sRes.data.ResponseMSG);

								if (sRes.data.IsSuccess === true) {
									$('#modal-xl').modal('hide');
								}

							}, function (err) {
								hidePleaseWait();
								$scope.loadingstatus = "stop";
								Swal.fire('Failed to send notification');
							});

						});

					});

				}

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire('Attachment upload failed');
			});

		});

	};

	$scope.SendSMSToNewStudent = function () {
		Swal.fire({
			title: 'Do you want to Send SMS To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				var para1 = {
					EntityId: entityStudentSummaryForSMS,
					ForATS: 3,
					TemplateType: 1
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
									title: 'Templates For SMS',
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
													angular.forEach($scope.gridApi2.grid.getVisibleRows(), function (node) {
														var objEntity = node.entity;
														var tmpContactNo = '';
														if (!objEntity.F_ContactNo)
															tmpContactNo = objEntity.ContactNo;
														else
															tmpContactNo = objEntity.F_ContactNo;

														if (tmpContactNo && tmpContactNo.length > 0) {
															var msg = selectedTemplate.Description;
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
																EntityId: entityStudentSummary,
																StudentId: objEntity.StudentId,
																UserId: objEntity.UserId,
																Title: selectedTemplate.Title,
																Message: msg,
																ContactNo: tmpContactNo,
																StudentName: objEntity.Name
															};

															dataColl.push(newSMS);
														}
													});

													print = true;

													$http({
														method: 'POST',
														url: base_url + "Global/SendSMSToStudent",
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

								angular.forEach($scope.gridApi2.grid.getVisibleRows(), function (node) {
									var objEntity = node.entity;
									var tmpContactNo = '';
									if (!objEntity.F_ContactNo)
										tmpContactNo = objEntity.ContactNo;
									else
										tmpContactNo = objEntity.F_ContactNo;

									if (tmpContactNo && tmpContactNo.length > 0) {
										var msg = selectedTemplate.Description;
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
											EntityId: entityStudentSummary,
											StudentId: objEntity.StudentId,
											UserId: objEntity.UserId,
											Title: selectedTemplate.Title,
											Message: msg,
											ContactNo: tmpContactNo,
											StudentName: objEntity.Name
										};

										dataColl.push(newSMS);
									}
								});
								print = true;

								$http({
									method: 'POST',
									url: base_url + "Global/SendSMSToStudent",
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
	$scope.SendNoticeToNewStudent = function () {
		Swal.fire({
			title: 'Do you want to Send Notification To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				var para1 = {
					EntityId: entityStudentSummaryForSMS,
					ForATS: 3,
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
													angular.forEach($scope.gridApi2.grid.getVisibleRows(), function (node) {
														var objEntity = node.entity;
														var msg = selectedTemplate.Description;
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
															EntityId: entityStudentSummary,
															StudentId: objEntity.StudentId,
															UserId: objEntity.UserId,
															Title: selectedTemplate.Title,
															Message: msg,
															ContactNo: objEntity.F_ContactNo,
															StudentName: objEntity.Name
														};

														dataColl.push(newSMS);
													});
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

								angular.forEach($scope.gridApi2.grid.getVisibleRows(), function (node) {
									var objEntity = node.entity;
									var msg = selectedTemplate.Description;
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
										EntityId: entityStudentSummary,
										StudentId: objEntity.StudentId,
										UserId: objEntity.UserId,
										Title: selectedTemplate.Title,
										Message: msg,
										ContactNo: objEntity.F_ContactNo,
										StudentName: objEntity.Name
									};

									dataColl.push(newSMS);
								});
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

	$scope.SendSMSToLeftStudent = function () {
		Swal.fire({
			title: 'Do you want to Send SMS To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				var para1 = {
					EntityId: entityStudentSummaryForSMS,
					ForATS: 3,
					TemplateType: 1
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
									title: 'Templates For SMS',
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
													angular.forEach($scope.gridApi3.grid.getVisibleRows(), function (node) {
														var objEntity = node.entity;
														var tmpContactNo = '';
														if (!objEntity.F_ContactNo)
															tmpContactNo = objEntity.ContactNo;
														else
															tmpContactNo = objEntity.F_ContactNo;

														if (tmpContactNo && tmpContactNo.length > 0) {
															var msg = selectedTemplate.Description;
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
																EntityId: entityStudentSummary,
																StudentId: objEntity.StudentId,
																UserId: objEntity.UserId,
																Title: selectedTemplate.Title,
																Message: msg,
																ContactNo: tmpContactNo,
																StudentName: objEntity.Name
															};

															dataColl.push(newSMS);
														}
													});

													print = true;

													$http({
														method: 'POST',
														url: base_url + "Global/SendSMSToStudent",
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

								angular.forEach($scope.gridApi3.grid.getVisibleRows(), function (node) {
									var objEntity = node.entity;
									var tmpContactNo = '';
									if (!objEntity.F_ContactNo)
										tmpContactNo = objEntity.ContactNo;
									else
										tmpContactNo = objEntity.F_ContactNo;

									if (tmpContactNo && tmpContactNo.length > 0) {
										var msg = selectedTemplate.Description;
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
											EntityId: entityStudentSummary,
											StudentId: objEntity.StudentId,
											UserId: objEntity.UserId,
											Title: selectedTemplate.Title,
											Message: msg,
											ContactNo: tmpContactNo,
											StudentName: objEntity.Name
										};

										dataColl.push(newSMS);
									}
								});
								print = true;

								$http({
									method: 'POST',
									url: base_url + "Global/SendSMSToStudent",
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
	$scope.SendNoticeToLeftStudent = function () {
		Swal.fire({
			title: 'Do you want to Send Notification To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				var para1 = {
					EntityId: entityStudentSummaryForSMS,
					ForATS: 3,
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
													angular.forEach($scope.gridApi3.grid.getVisibleRows(), function (node) {
														var objEntity = node.entity;
														var msg = selectedTemplate.Description;
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
															EntityId: entityStudentSummary,
															StudentId: objEntity.StudentId,
															UserId: objEntity.UserId,
															Title: selectedTemplate.Title,
															Message: msg,
															ContactNo: objEntity.F_ContactNo,
															StudentName: objEntity.Name
														};

														dataColl.push(newSMS);
													});
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

								angular.forEach($scope.gridApi3.grid.getVisibleRows(), function (node) {
									var objEntity = node.entity;
									var msg = selectedTemplate.Description;
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
										EntityId: entityStudentSummary,
										StudentId: objEntity.StudentId,
										UserId: objEntity.UserId,
										Title: selectedTemplate.Title,
										Message: msg,
										ContactNo: objEntity.F_ContactNo,
										StudentName: objEntity.Name
									};

									dataColl.push(newSMS);
								});
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

	//$scope.SendNoticeToStudent = function () {

	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	var uIdColl = '';


	//	if ($scope.newNotice.FilterStudentOnly == true) {
	//		var vRows = $scope.gridApi.core.getVisibleRows();
	//		angular.forEach(vRows, function (rData) {
	//			var d = rData.entity;

	//			if (uIdColl.length > 0)
	//				uIdColl = uIdColl + ',';

	//			uIdColl = uIdColl + d.UserId;
	//		});
	//	} else {
	//		angular.forEach($scope.gridOptions.data, function (d) {
	//			if (uIdColl.length > 0)
	//				uIdColl = uIdColl + ',';

	//			uIdColl = uIdColl + d.UserId;
	//		});
	//       }

	//	if ($scope.newNotice.FilterStudentOnly == true && uIdColl.length == 0) {
	//		alert('First Load Student');
	//		return;
	//       }


	//	var filesColl = $scope.newNotice.AttachmentColl;

	//	var tmpData = {
	//		studentIdColl: uIdColl,
	//		title: $scope.newNotice.Title,
	//		description: $scope.newNotice.Description,
	//		parents:true
	//	};

	//	$http({
	//		method: 'POST',
	//		url: base_url + "Academic/Report/SendNoticeToStudent",
	//		headers: { 'Content-Type': undefined },

	//		transformRequest: function (data) {

	//			var formData = new FormData();
	//			formData.append("jsonData", angular.toJson(data.jsonData));

	//			if (data.files) {
	//				for (var i = 0; i < data.files.length; i++)
	//				{
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
		$('input[type=file]').val('');
		$('#modal-xl').modal('hide');
		$('#modal-sms').modal('hide');
	}
	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.entity = {
			StudentSummary: entityStudentSummary,
			NewStudentSummary: entityNewStudentSummary,
			LeftStudentSummary: entityLeftStudentSummary,
			SiblingStudentSummary: entitySiblingStudentSummary
		};

		$scope.confirmMSG = GlobalServices.getConfirmMSG();

		$scope.ClassSection = {};
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.CasteList = [];
		GlobalServices.getCasteList().then(function (res) {
			$scope.CasteList = res.data.Data;
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

		$scope.MediumList = [];
		GlobalServices.getMediumList().then(function (res) {
			$scope.MediumList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.HouseNameList = [];
		GlobalServices.getHouseNameList().then(function (res) {
			$scope.HouseNameList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.StudentTypeList = [];
		GlobalServices.getStudentTypeList().then(function (res) {
			$scope.StudentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newStudentSummaryList = {
			ClassId: null,
			SectionId: null,
			HouseNameId: null,
			StudentTypeId: null,
			CasteId: null
		};

		$scope.newAdmissionList = {
			ClassId: null,
			SectionId: null
		};

		$scope.newLeftStudentList = {
			ClassId: null,
			SectionId: null
		};

		$scope.getterAndSetter();

		$scope.newNotice = {
			FilterStudentOnly: true,
			Title: '',
			Description: ''
		};

		$scope.newBirthday = {
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date()
		};


		$http({
			method: 'GET',
			url: base_url + "Global/GetListState?entityId=" + $scope.entity.StudentSummary + "&isReport=true",
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

	//DONE: save State for new Admission List
		$http({
			method: 'GET',
			url: base_url + "Global/GetListState?entityId=" + $scope.entity.NewStudentSummary + "&isReport=true",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				if ($scope.gridApi) {
					if ($scope.gridApi2.saveState) {
						var state = JSON.parse(res.data.Data);

						$scope.gridApi2.saveState.restore($scope, state);
					}
				}
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	//TODO: save State for Left Student List
		$http({
			method: 'GET',
			url: base_url + "Global/GetListState?entityId=" + $scope.entity.LeftStudentSummary + "&isReport=true",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				if ($scope.gridApi) {
					if ($scope.gridApi3.saveState) {
						var state = JSON.parse(res.data.Data);

						$scope.gridApi3.saveState.restore($scope, state);
					}
				}
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	//TODO: save State for Sibling List
		$http({
			method: 'GET',
			url: base_url + "Global/GetListState?entityId=" + $scope.entity.SiblingStudentSummary + "&isReport=true",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				if ($scope.gridApi) {
					if ($scope.gridApi5.saveState) {
						var state = JSON.parse(res.data.Data);

						$scope.gridApi5.saveState.restore($scope, state);
					}
				}
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AgeList = [];
		$scope.AgeList.push({
			Age: 0
		});

		$timeout(function () {
			$scope.LoadDOB();
		});



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

		/*smsPara.TemplateType = 2;*/
		var emailPara = {
			EntityId: $scope.entity.StudentSummary,
			ForATS: 3,
			TemplateType: 2
		};
		$scope.EmailTemplatesColl = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(emailPara)
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

		$scope.IsInit = true;
		$scope.AllEmployeeColl = [];
		$timeout(function () {
			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetAllEmpShortList",
				dataType: "json"
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.AllEmployeeColl = res.data.Data;
					$scope.IsInit = false;
				}


			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		},100);
		
	}

	$scope.GetStudentSummaryList = function () {

		if ($scope.IsInit == true) return;

		$scope.loadingstatus = "running";
		showPleaseWait();

		var agePara = [];
		angular.forEach($scope.AgeList, function (al) {
			agePara.push(al.Age);
		});

		var para = {
			ClassIdColl: $scope.newStudentSummaryList.ClassId,
			SectionIdColl: $scope.newStudentSummaryList.SectionId,
			StudentTypeIdColl: $scope.newStudentSummaryList.StudentTypeId,
			HouseNameIdColl: $scope.newStudentSummaryList.HouseNameId,
			CasteIdColl: $scope.newStudentSummaryList.CasteId,
			flag: 1,
			AgeRange: agePara,
			MediumIdColl: $scope.newStudentSummaryList.MediumId,
			BatchId: $scope.newStudentSummaryList.BatchId,
			SemesterId: $scope.newStudentSummaryList.SemesterId,
			ClassYearId: $scope.newStudentSummaryList.ClassYearId,
			BranchId:$scope.newStudentSummaryList.BranchId,
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetStudentSummary",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions.data = res.data.Data;

				$('#modal-agerange').modal('hide');

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
											var dataColl = [];// $scope.gridOptions.data;
											angular.forEach($scope.gridApi.grid.getVisibleRows(), function (node) {
												var objEntity = node.entity;
												dataColl.push(objEntity);
											});

											print = true;
											$http({
												method: 'POST',
												url: base_url + "Academic/Report/PrintStudentSummary",
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
						var dataColl = [];// $scope.gridOptions.data;
						angular.forEach($scope.gridApi.grid.getVisibleRows(), function (node) {
							var objEntity = node.entity;
							dataColl.push(objEntity);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Academic/Report/PrintStudentSummary",
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

	$scope.GetNewAdmissionList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ClassIdColl: $scope.newAdmissionList.ClassId,
			SectionIdColl: $scope.newAdmissionList.SectionId,
			flag: 2,
			BranchId:$scope.newAdmissionList.BranchId,
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetStudentSummary",
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

	$scope.PrintNewStudent = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityNewStudentSummary + "&voucherId=0&isTran=false",
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
											angular.forEach($scope.gridApi2.grid.getVisibleRows(), function (node) {
												var objEntity = node.entity;
												dataColl.push(objEntity);
											});

											print = true;
											$http({
												method: 'POST',
												url: base_url + "Academic/Report/PrintStudentSummary",
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
													document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityNewStudentSummary + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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
						angular.forEach($scope.gridApi2.grid.getVisibleRows(), function (node) {
							var objEntity = node.entity;
							dataColl.push(objEntity);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Academic/Report/PrintStudentSummary",
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
								document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityNewStudentSummary + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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

	$scope.PrintLeftStudent = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityLeftStudentSummary + "&voucherId=0&isTran=false",
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
											angular.forEach($scope.gridApi3.grid.getVisibleRows(), function (node) {
												var objEntity = node.entity;
												dataColl.push(objEntity);
											});

											print = true;
											$http({
												method: 'POST',
												url: base_url + "Academic/Report/PrintStudentSummary",
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
													document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityLeftStudentSummary + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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
						angular.forEach($scope.gridApi3.grid.getVisibleRows(), function (node) {
							var objEntity = node.entity;
							dataColl.push(objEntity);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Academic/Report/PrintStudentSummary",
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
								document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityLeftStudentSummary + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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

	$scope.GetLeftStudentList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ClassIdColl: $scope.newLeftStudentList.ClassId,
			SectionIdColl: $scope.newLeftStudentList.SectionId,
			flag: 3,
			BranchId:$scope.newLeftStudentList.BranchId,
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetStudentSummary",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions3.data = res.data.Data;

			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.LoadDOB = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			dateFrom: ($scope.newBirthday.DateFromDet ? $scope.newBirthday.DateFromDet.dateAD : null),
			dateTo: ($scope.newBirthday.DateToDet ? $scope.newBirthday.DateToDet.dateAD : null),
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetStudentBirthDay",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions4.data = res.data.Data;
			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.SendEmailDOB = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Report/ST_BirthDayEmail",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.SendSMSToStudentBirthday = function () {
		Swal.fire({
			title: 'Do you want to Send SMS To the Birthday Students?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				var para1 = {
					EntityId: entityStudentBirthdayForSMS,
					ForATS: 3,
					TemplateType: 1
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
									title: 'Templates For SMS',
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
													angular.forEach($scope.gridApi4.grid.getVisibleRows(), function (node) {
														var objEntity = node.entity;
														var tmpContactNo = '';
														tmpContactNo = objEntity.ContactNo;

														if (tmpContactNo && tmpContactNo.length > 0) {
															var msg = selectedTemplate.Description;
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
																EntityId: entityStudentSummary,
																StudentId: objEntity.StudentId,
																UserId: objEntity.UserId,
																Title: selectedTemplate.Title,
																Message: msg,
																ContactNo: tmpContactNo,
																StudentName: objEntity.Name
															};

															dataColl.push(newSMS);
														}
													});

													print = true;

													$http({
														method: 'POST',
														url: base_url + "Global/SendSMSToStudent",
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

								angular.forEach($scope.gridApi4.grid.getVisibleRows(), function (node) {
									var objEntity = node.entity;
									var tmpContactNo = '';
									tmpContactNo = objEntity.ContactNo;

									if (tmpContactNo && tmpContactNo.length > 0) {
										var msg = selectedTemplate.Description;
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
											EntityId: entityStudentSummary,
											StudentId: objEntity.StudentId,
											UserId: objEntity.UserId,
											Title: selectedTemplate.Title,
											Message: msg,
											ContactNo: tmpContactNo,
											StudentName: objEntity.Name
										};

										dataColl.push(newSMS);
									}
								});
								print = true;

								$http({
									method: 'POST',
									url: base_url + "Global/SendSMSToStudent",
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
	$scope.SendNoticeToStudentBirthday = function () {
		Swal.fire({
			title: 'Do you want to Send Notification To the Student birthday data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				var para1 = {
					EntityId: entityStudentBirthdayForSMS,
					ForATS: 3,
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
													$scope.newNotice.Title = selectedTemplate.Title;
													$scope.newNotice.Description = selectedTemplate.Description;
													$('#modal-birthday-xl').modal('show');
												}

											} else {
												resolve('You need to select:)')
											}
										})
									}
								})
							}

							if (rptTranId > 0 && print == false) {
								$scope.newNotice.Title = selectedTemplate.Title;
								$scope.newNotice.Description = selectedTemplate.Description;
								$('#modal-birthday-xl').modal('show');
							}

						} else {
							$scope.newNotice.Title = '';
							$scope.newNotice.Description = '';
							$('#modal-birthday-xl').modal('show');
						}

					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}
		});

	};
	$scope.SendManualNoticeToStudentBirthday = function () {

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
						angular.forEach($scope.gridApi4.grid.getVisibleRows(), function (node) {
							var objEntity = node.entity;
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
								EntityId: entityStudentSummary,
								StudentId: objEntity.StudentId,
								UserId: objEntity.UserId,
								Title: $scope.newNotice.Title,
								Message: msg,
								ContactNo: objEntity.ContactNo,
								StudentName: objEntity.Name,
								ContentPath: contentPath
							};

							dataColl.push(newSMS);
						});
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
								$('#modal-birthday-xl').modal('hide');
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


	$scope.PrintStudentBirthday = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityStudentBirthday + "&voucherId=0&isTran=false",
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
					var selectedTemplate = null;
					if (templatesColl.length == 1) {
						rptTranId = templatesColl[0].RptTranId;
						selectedTemplate = templatesColl[0];
					}
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
										selectedTemplate = templatesColl[value];
										rptTranId = templatesColl[value].RptTranId;

										if (rptTranId > 0) {
											if (selectedTemplate.IsRDLC == true) {

												var rptPara = {
													DateFrom: $filter('date')(new Date(($scope.newBirthday.DateFromDet ? $scope.newBirthday.DateFromDet.dateAD : new Date())), 'yyyy-MM-dd'),
													DateTo: $filter('date')(new Date(($scope.newBirthday.DateToDet ? $scope.newBirthday.DateToDet.dateAD : new Date())), 'yyyy-MM-dd'),
													rptTranId: rptTranId
												};
												var paraQuery = param(rptPara);

												document.body.style.cursor = 'wait';
												document.getElementById("frmRpt").src = '';
												document.getElementById("frmRpt").src = base_url + "Academic/Report/RdlStudentBirthday?" + paraQuery;
												document.body.style.cursor = 'default';
												$('#FrmPrintReport').modal('show');
											} else {
												var dataColl = [];// $scope.gridOptions.data;
												angular.forEach($scope.gridApi4.grid.getVisibleRows(), function (node) {
													var objEntity = node.entity;
													dataColl.push(objEntity);
												});

												print = true;
												$http({
													method: 'POST',
													url: base_url + "Academic/Report/PrintStudentBirthday",
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
														document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentBirthday + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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
										}

									} else {
										resolve('You need to select:)')
									}
								})
							}
						})
					}

					if (rptTranId > 0 && print == false) {
						if (selectedTemplate.IsRDLC == true) {

							var rptPara = {
								DateFrom: $filter('date')(new Date(($scope.newBirthday.DateFromDet ? $scope.newBirthday.DateFromDet.dateAD : new Date())), 'yyyy-MM-dd'),
								DateTo: $filter('date')(new Date(($scope.newBirthday.DateToDet ? $scope.newBirthday.DateToDet.dateAD : new Date())), 'yyyy-MM-dd'),
								rptTranId: rptTranId
							};
							var paraQuery = param(rptPara);

							document.body.style.cursor = 'wait';
							document.getElementById("frmRpt").src = '';
							document.getElementById("frmRpt").src = base_url + "Academic/Report/RdlStudentBirthday?" + paraQuery;
							document.body.style.cursor = 'default';
							$('#FrmPrintReport').modal('show');

						} else {
							var dataColl = [];// $scope.gridOptions.data;
							angular.forEach($scope.gridApi4.grid.getVisibleRows(), function (node) {
								var objEntity = node.entity;
								dataColl.push(objEntity);
							});
							print = true;

							$http({
								method: 'POST',
								url: base_url + "Academic/Report/PrintStudentBirthday",
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
									document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityStudentBirthday + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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


					}

				} else
					Swal.fire('No Templates found for print');
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	$scope.LoadSibling = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();


		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetSiblingList",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.gridOptions5.data = res.data.Data;
			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

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



		// Get selected students
		 
		let selectedRows = tmpCheckedData.filter(row => row.isSelected === true);

		if (selectedRows.length === 0) {
			Swal.fire('Please select at least one student to send Email');
			return;
		}
		  
		// Fetch Email templates
		var para1 = {
			EntityId: entityStudentSummaryForSMS,
			ForATS: 3,
			TemplateType: 2
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(para1)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmailTemplatesColl = res.data.Data;

				// Auto-fill if only one template
				if ($scope.EmailTemplatesColl.length === 1) {
					$scope.selectedEmailTemplate = $scope.EmailTemplatesColl[0];					 
				}
			} else {
				Swal.fire('No templates found for SMS');
			}

			$('#sendemail').modal('show');

		}, function (err) {
			Swal.fire('Failed to load templates');
		});


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

			var bccColl = [];
			if ($scope.CurEmailSend.BCCEmployeeColl && $scope.CurEmailSend.BCCEmployeeColl.length > 0) {
				angular.forEach($scope.CurEmailSend.BCCEmployeeColl, function (empbcc) {
					if (empbcc.EmailId && empbcc.EmailId.length > 0)
						bccColl.push(empbcc.EmailId);
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
						EntityId: $scope.entity.StudentSummary,
						StudentId: objEntity.StudentId,
						UserId: 0,
						Title: $scope.CurEmailSend.Temlate.Title,
						Subject: $scope.CurEmailSend.Subject,
						Message: msg,
						CC: ccColl.toString(),
						BCC: bccColl.toString(),
						To: emailColl.toString(),
						StudentName: objEntity.Name,
						ParaColl: paraColl,
						FileName: 'student-form'
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
					$('#sendemail').modal('hide');

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


	$('#sendemail').on('hidden.bs.modal', function () {

		var scope = angular.element(this).scope();

		scope.$apply(function () {
			scope.CurEmailSend = {
				EmployeeColl: [],
				BCCEmployeeColl: []
			};
		});

		// Clear all select2 inside modal
		$(this).find('.select2').val(null).trigger('change');

	});
	//Done: save State for New Admission List
	$scope.saveNewAdmissionListState = function () {
		var state = $scope.gridApi2.saveState.save();

		$http({
			method: 'POST',
			url: base_url + "Global/SaveListState",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", JSON.stringify(data.jsonData));
				formData.append("entityId", $scope.entity.NewStudentSummary);
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
	//DONE: save State for Left Student List
	$scope.saveLeftStudentSummaryState = function () {
		var state = $scope.gridApi3.saveState.save();

		$http({
			method: 'POST',
			url: base_url + "Global/SaveListState",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", JSON.stringify(data.jsonData));
				formData.append("entityId", $scope.entity.LeftStudentSummary);
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
	//TODO: save State for Sibling List
	$scope.saveSiblingState = function () {
		var state = $scope.gridApi5.saveState.save();

		$http({
			method: 'POST',
			url: base_url + "Global/SaveListState",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", JSON.stringify(data.jsonData));
				formData.append("entityId", $scope.entity.SiblingStudentSummary);
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


});