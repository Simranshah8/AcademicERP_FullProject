

app.controller('BillPrintController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate, uiGridConstants, uiGridTreeViewConstants) {
	$scope.Title = 'BillPrint';

	$rootScope.ConfigFunction = function () {
		$scope.LoadData();
		 
	};
	$rootScope.ChangeLanguage();

	/*OnClickDefault();*/
	$scope.LoadData = function () {
	 

		$scope.entity = {
			BillPrint: 368
		};

		//$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.LanguageColl = GlobalServices.getLangList();
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.MonthList = [];
		$scope.MonthList_RS = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});
			$scope.MonthList_RS = $scope.MonthList;

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.StudentTypeList = [];
		GlobalServices.getStudentTypeList().then(function (res) {
			$scope.StudentTypeList = res.data.Data;
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
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AllClassList = [];
		GlobalServices.getClassList().then(function (res) {
			$scope.AllClassList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.MediumList = [];
		//GlobalServices.getMediumList().then(function (res) {
		//	$scope.MediumList = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

	

		//$scope.StudentTypeList = [];
		//GlobalServices.getStudentTypeList().then(function (res) {
		//	$scope.StudentTypeList = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});


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

		

		$scope.currentPages = {
			BillPrint: 1,

		};

		$scope.searchData = {
			BillPrint: '',

		};

		$scope.perPage = {
			BillPrint: GlobalServices.getPerPageRow(),

		};

		$scope.newBillPrint = {
			BillPrintId: null,
			BillPrintDate: null,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			FromBillNo: 0,
			ToBillNo:0,
			Mode: 'Save'
		};

		$scope.newReminderSlip = {
			UptoMonthId: 1,
			ForStudent: 1,
			CombinedSummary: false,
		};

		$scope.LoadReportTemplates();
		//$scope.GetAllBillPrintList();

	};
	$scope.ChangeReminderSlipClass = function () {
		if ($scope.newReminderSlip.SelectedClass) {
			GlobalServices.getAcademicMonthList(null, $scope.newReminderSlip.SelectedClass.ClassId).then(function (resAM) {
				$scope.MonthList_RS = [];
				angular.forEach(resAM.data.Data, function (m) {
					$scope.MonthList_RS.push({ id: m.NM, text: m.MonthYear });
				});
			});
		}
    }
	$scope.getClassWiseSemester = function () {

		$scope.SelectedClassSemesterList = [];
		$scope.SelectedClassClassYearList = [];
		
		if ($scope.newBillPrint.SelectedClass) {

			GlobalServices.getAcademicMonthList(null, $scope.newBillPrint.SelectedClass.ClassId).then(function (resAM) {
				$scope.MonthList = [];
				angular.forEach(resAM.data.Data, function (m) {
					$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
				});

				var findClass = $scope.AllClassList.firstOrDefault(p1 => p1.ClassId == $scope.newBillPrint.SelectedClass.ClassId);
				if (findClass) {

					$scope.newBillPrint.SelectedClass.ClassType = findClass.ClassType;

					var semQry = mx(findClass.ClassSemesterIdColl);
					var cyQry = mx(findClass.ClassYearIdColl);

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

			});

			
			 
		}
	};
	$scope.LoadReportTemplates = function () {

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + $scope.entity.BillPrint + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newBillPrint.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		
	}

	$scope.ClassWiseBillPrint= function () {
		if ($scope.newBillPrint.SelectedClass && $scope.newBillPrint.FromMonthId && $scope.newBillPrint.ToMonthId) {
			var EntityId = $scope.entity.BillPrint;

			if ($scope.newBillPrint.RptTranId > 0)
			{				
				var rptPara = {
					rpttranid: $scope.newBillPrint.RptTranId,
					istransaction: false,
					entityid: EntityId,
					StudentId:0,
					ClassId: ($scope.newBillPrint.SelectedClass ? $scope.newBillPrint.SelectedClass.ClassId : 0),
					SectionId: ($scope.newBillPrint.SelectedClass ? $scope.newBillPrint.SelectedClass.SectionId : 0),
					FromMonthId: $scope.newBillPrint.FromMonthId,
					ToMonthId: $scope.newBillPrint.ToMonthId,
					FromBillNo: $scope.newBillPrint.FromBillNo,
					ToBillNo: $scope.newBillPrint.ToBillNo,
					BatchId: ($scope.newBillPrint.BatchId ? $scope.newBillPrint.BatchId : 0),
					FacultyId: ($scope.newBillPrint.FacultyId ? $scope.newBillPrint.FacultyId : 0),
					SemesterId: ($scope.newBillPrint.SemesterId ? $scope.newBillPrint.SemesterId : 0),
					ClassYearId: ($scope.newBillPrint.ClassYearId ? $scope.newBillPrint.ClassYearId : 0),
					StudentTypeId: ($scope.newBillPrint.StudentTypeId ? $scope.newBillPrint.StudentTypeId : 0),
					MediumId: ($scope.newBillPrint.MediumId ? $scope.newBillPrint.MediumId : 0),
					ShowZeroBalance: $scope.newBillPrint.ShowZeroBalance==true ? 1 : 0,
				};
				var paraQuery = param(rptPara);
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptBillPrint").src = '';
				document.getElementById("frmRptBillPrint").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
				document.body.style.cursor = 'default';
			} else {
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptBillPrint").src = '';
				document.body.style.cursor = 'default';
			}
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptBillPrint").src = '';
			document.body.style.cursor = 'default';
		}

	};

	$scope.StudentWiseBillPrint = function () {
		if ($scope.newBillPrint.StudentId && $scope.newBillPrint.FromMonthId && $scope.newBillPrint.ToMonthId) {
			var EntityId = $scope.entity.BillPrint;

			if ($scope.newBillPrint.RptTranId > 0) {
				var rptPara = {
					rpttranid: $scope.newBillPrint.RptTranId,
					istransaction: false,
					entityid: EntityId,
					StudentId: $scope.newBillPrint.StudentId,
					ClassId: 0,
					SectionId: 0,
					FromMonthId: $scope.newBillPrint.FromMonthId,
					ToMonthId: $scope.newBillPrint.ToMonthId,
					FromBillNo: 0,
					ToBillNo: 0
				};
				var paraQuery = param(rptPara);
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptBillPrint").src = '';
				document.getElementById("frmRptBillPrint").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
				document.body.style.cursor = 'default';
			} else {
				document.body.style.cursor = 'wait';
				document.getElementById("frmRptBillPrint").src = '';
				document.body.style.cursor = 'default';
			}
		} else {
			document.body.style.cursor = 'wait';
			document.getElementById("frmRptBillPrint").src = '';
			document.body.style.cursor = 'default';
		}

	};

	$scope.ClearBillPrint = function () {
		$scope.newBillPrint = {
			BillPrintId: null,
			BillPrintDate: null,
			
			Mode: 'Save'
		};
	};

	$scope.SendEmail = function () {
		if ($scope.newBillPrint.SelectedClass && $scope.newBillPrint.FromMonthId && $scope.newBillPrint.ToMonthId) {
			var EntityId = $scope.entity.BillPrint;

			if ($scope.newBillPrint.RptTranId > 0) {
				var rptPara = {
					rpttranid: $scope.newBillPrint.RptTranId,
					istransaction: false,
					entityid: EntityId,
					StudentId: 0,
					ClassId: ($scope.newBillPrint.SelectedClass ? $scope.newBillPrint.SelectedClass.ClassId : 0),
					SectionId: ($scope.newBillPrint.SelectedClass ? $scope.newBillPrint.SelectedClass.SectionId : 0),
					FromMonthId: $scope.newBillPrint.FromMonthId,
					ToMonthId: $scope.newBillPrint.ToMonthId,
					FromBillNo: $scope.newBillPrint.FromBillNo,
					ToBillNo: $scope.newBillPrint.ToBillNo,
					BatchId: ($scope.newBillPrint.BatchId ? $scope.newBillPrint.BatchId : 0),
					FacultyId: ($scope.newBillPrint.FacultyId ? $scope.newBillPrint.FacultyId : 0),
					SemesterId: ($scope.newBillPrint.SemesterId ? $scope.newBillPrint.SemesterId : 0),
					ClassYearId: ($scope.newBillPrint.ClassYearId ? $scope.newBillPrint.ClassYearId : 0),
				};
				$scope.loadingstatus = "running";
				showPleaseWait();
				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/BillSendEmail",
					headers: { 'Content-Type': undefined },

					transformRequest: function (data) {

						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));

						return formData;
					},
					data: { jsonData: rptPara }
				}).then(function (res) {

					$scope.loadingstatus = "stop";
					hidePleaseWait();

					Swal.fire(res.data.ResponseMSG);
 

				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";

				});

			}  
		}  

	};

	  
	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newBillPrint.AttachmentColl) {
			if ($scope.newBillPrint.AttachmentColl.length > 0) {
				$scope.newBillPrint.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newBillPrint.AttachmentColl.push({
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
	 
	$scope.SaveUpdateBillPrint = function () {
		if ($scope.IsValidBillPrint() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBillPrint.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBillPrint();
					}
				});
			} else
				$scope.CallSaveUpdateBillPrint();

		}
	};

	$scope.CallSaveUpdateBillPrint = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newBillPrint.BillPrintDateDet) {
			$scope.newBillPrint.BillPrintDate = $scope.newBillPrint.BillPrintDateDet.dateAD;
		} else
			$scope.newBillPrint.BillPrintDate = null;


		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveBillPrint",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newBillPrint }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBillPrint();
				$scope.GetAllBillPrintList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllBillPrintList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BillPrintList = [];

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllBillPrintList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BillPrintList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetBillPrintById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BillPrintId: refData.BillPrintId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetBillPrintById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBillPrint = res.data.Data;
				$scope.newBillPrint.Mode = 'Modify';

				document.getElementById('class-section').style.display = "none";
				document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBillPrintById = function (refData) {

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
					BillPrintId: refData.BillPrintId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelBillPrint",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBillPrintList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	getterAndSetter();

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

				{ name: "RegNo", displayName: "Regd.No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassSec", displayName: "Class/Sec", minWidth: 140, headerCellClass: 'headerAligment' },
				{
					name: "Debit", displayName: "Fee Amt.", minWidth: 140, headerCellClass: 'headerAligment',
					aggregationType: uiGridConstants.aggregationTypes.sum,
					cellClass: 'numericAlignment', headerCellClass: 'headerAligment',
					footerCellTemplate: '<div class="ui-grid-cell-contents" >{{col.getAggregationValue() | number:2 }}</div>',
					footerCellClass: 'numericAlignment'
				},
				{
					name: "Credit", displayName: "Paid Amt.", minWidth: 140, headerCellClass: 'headerAligment',
					aggregationType: uiGridConstants.aggregationTypes.sum,
					cellClass: 'numericAlignment', headerCellClass: 'headerAligment',
					footerCellTemplate: '<div class="ui-grid-cell-contents" >{{col.getAggregationValue() | number:2 }}</div>',
					footerCellClass: 'numericAlignment'
				},
				{
					name: "Balance", displayName: "Dues Amt.", minWidth: 140, headerCellClass: 'headerAligment',
					aggregationType: uiGridConstants.aggregationTypes.sum,
					cellClass: 'numericAlignment', headerCellClass: 'headerAligment',
					footerCellTemplate: '<div class="ui-grid-cell-contents" >{{col.getAggregationValue() | number:2 }}</div>',
					footerCellClass: 'numericAlignment',
					columnFilter: {
						type: 'number'
					},
					filter: {
						condition: uiGridConstants.filter.GREATER_THAN
					}
				},
				{ name: "FatherName", displayName: "Father Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "F_ContactNo", displayName: "Father Contact No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "MotherName", displayName: "Mother Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "M_ContactNo", displayName: "Mother Contact No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TransportPoint", displayName: "Transport Point", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RoomName", displayName: "Hostel", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "IsNewStudent", displayName: "IsNewStudent", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "IsLeft", displayName: "IsLeft", minWidth: 140, headerCellClass: 'headerAligment' },

				{ name: "Batch", displayName: "Batch", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Faculty", displayName: "Faculty", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "ClassYear", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Email", displayName: "Email", minWidth: 140, headerCellClass: 'headerAligment' },


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
			exporterExcelFilename: 'remidersummary.xlsx',
			exporterExcelSheetName: 'reminderSummary',
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
			}
		};

	};

	$scope.getReminderSlip = function () {

		if ($scope.newReminderSlip.UptoMonthId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				UptoMonthId: $scope.newReminderSlip.UptoMonthId,				
				forStudent: $scope.newReminderSlip.ForStudent,
				classId: $scope.newReminderSlip.SelectedClass ? $scope.newReminderSlip.SelectedClass.ClassId : 0,
				sectionId: $scope.newReminderSlip.SelectedClass ? $scope.newReminderSlip.SelectedClass.SectionId : 0,
				BatchId: $scope.newReminderSlip.BatchId,
				ClassYearId: $scope.newReminderSlip.ClassYearId,
				SemesterId: $scope.newReminderSlip.SemesterId,
			};
			$http({
				method: 'POST',
				url: base_url + "Fee/Creation/GetReminderSlip",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					//$scope.gridOptions.data = res.data.Data;

					if ($scope.newReminderSlip.CombinedSummary == true) {

						var DataColl = mx(res.data.Data);
						var newDataColl = [];
						var groupStudent = DataColl.groupBy(p1 => p1.StudentId);
						angular.forEach(groupStudent, function (st) {
							var fst = st.elements[0];
							var groupData = mx(st.elements);
							var newData = angular.copy(fst); 
				 			if (st.elements.length > 1) {
								newData.Debit = groupData.sum(p1 => p1.Debit);
								newData.Credit = groupData.sum(p1 => p1.Credit);
								newData.Balance = groupData.sum(p1 => p1.Balance);				
							}
							newDataColl.push(newData);
						});

						$scope.gridOptions.data =newDataColl;
					}
					else {
						$scope.gridOptions.data = res.data.Data;
					}


				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.PrintReminderSlip = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityRemiderSlip + "&voucherId=0&isTran=false",
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
											var dataColl = [];
											angular.forEach($scope.gridApi.core.getVisibleRows($scope.gridApi.grid), function (fr) {
												dataColl.push(fr.entity);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Fee/Creation/PrintReminderSlip",
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
													document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityRemiderSlip + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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
						var dataColl = [];
						angular.forEach($scope.gridApi.core.getVisibleRows($scope.gridApi.grid), function (fr) {
							dataColl.push(fr.entity);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Fee/Creation/PrintReminderSlip",
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
								document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityRemiderSlip + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
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

	$scope.SendSMSToStudent = function () {
		Swal.fire({
			title: 'Do you want to Send SMS To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				var para1 = {
					EntityId: entityReminderForSMS,
					ForATS: 3,
					TemplateType: 1
				};

				var checkedItemOnly = false;
				let tmpCheckedData = [];
				tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
				for (let ent in tmpCheckedData) {
					if (tmpCheckedData[ent].isSelected == true) {
						checkedItemOnly = true;
						break;
					}
				}

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
													for (let nInd in tmpCheckedData)
													{
														if (tmpCheckedData.length == (parseInt(nInd)) || Number.isNaN(parseInt(nInd)) == true)
															break;

														let node = tmpCheckedData[nInd];
														if (checkedItemOnly == true && node.isSelected == false)
															continue;

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
																EntityId: entityReminderForSMS,
																StudentId: objEntity.StudentId,
																UserId: objEntity.UserId,
																Title: selectedTemplate.Title,
																Message: msg,
																ContactNo: tmpContactNo,
																StudentName: objEntity.Name
															};

															dataColl.push(newSMS);
														}
													};

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

								for (let nInd in tmpCheckedData)
								{
									if (tmpCheckedData.length == (parseInt(nInd)) || Number.isNaN(parseInt(nInd))==true)
										break;

									let node = tmpCheckedData[nInd];
									if (checkedItemOnly == true && node.isSelected == false)
										continue;

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
											EntityId: entityReminderForSMS,
											StudentId: objEntity.StudentId,
											UserId: objEntity.UserId,
											Title: selectedTemplate.Title,
											Message: msg,
											ContactNo: tmpContactNo,
											StudentName: objEntity.Name
										};

										dataColl.push(newSMS);
									}
								};
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
	$scope.SendNoticeToStudent = function () {

		$scope.newNotice = {};

		Swal.fire({
			title: 'Do you want to Send Notification To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				var para1 = {
					EntityId: entityReminderForSMS,
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
													$('#modal-xl').modal('show');
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
								$('#modal-xl').modal('show');
							}

						} else {

							$scope.newNotice.Title = '';
							$scope.newNotice.Description = '';
							$('#modal-xl').modal('show');
						}
					}  
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}
		});
		 
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
						for (let ent in tmpCheckedData) {
							if (tmpCheckedData[ent].isSelected == true) {
								checkedItemOnly = true;
								break;
							}
						}

						var dataColl = [];			 
						for (let nInd in tmpCheckedData) {
							if (tmpCheckedData.length == (parseInt(nInd)) || Number.isNaN(parseInt(nInd)) == true)
								break;

							let node = tmpCheckedData[nInd];
							if (checkedItemOnly == true && node.isSelected == false)
								continue;

							var objEntity = node.entity;
							var tmpContactNo = '';
							if (!objEntity.F_ContactNo)
								tmpContactNo = objEntity.ContactNo;
							else
								tmpContactNo = objEntity.F_ContactNo;

							if (tmpContactNo && tmpContactNo.length > 0) {
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
									EntityId: entityReminderForSMS,
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
						};

				 
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
	 
	$scope.SendEmailReminderSlip = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var checkedItemOnly = false;
		let tmpCheckedData = [];
		tmpCheckedData = $scope.gridApi.grid.getVisibleRows();
		for (let ent in tmpCheckedData) {
			if (tmpCheckedData[ent].isSelected == true) {
				checkedItemOnly = true;
				break;
			}
		}

		var dataColl = [];
		for (let nInd in tmpCheckedData) {
			if (tmpCheckedData.length == (parseInt(nInd)) || Number.isNaN(parseInt(nInd)) == true)
				break;

			let node = tmpCheckedData[nInd];
			if (checkedItemOnly == true && node.isSelected == false)
				continue;

			var objEntity = node.entity;
			dataColl.push(objEntity);
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/ReminderSlipSendEmail",
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
			Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(errormessage);
		});
	};
});