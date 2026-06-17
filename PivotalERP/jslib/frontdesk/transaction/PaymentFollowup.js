"use strict";

agGrid.initialiseAgGridWithAngular1(angular);

app.controller('PaymentFollowupController', function ($scope, $http, $timeout, $filter, $translate, $rootScope, GlobalServices) {
	$scope.Title = 'Payment Followup';
	getterAndSetter();

	function getterAndSetter() {

		var columnDefs = [
			{ headerName: "Adm.No", field: "RegdNo", width: 110, pinned: 'left' },
			{ headerName: "Name", field: "Name", width: 170, pinned: 'left' },
			{ headerName: "Roll No", field: "RollNo", width: 100, pinned: 'left' },
			{ headerName: "Class", width: 100, field: "ClassName", filter: 'agTextColumnFilter', },
			{ headerName: "Section", width: 100, field: "SectionName", filter: 'agTextColumnFilter', },
			{ headerName: "Batch", width: 100, field: "Batch", filter: 'agTextColumnFilter', },
			//{ headerName: "Faculty", width: 100, field: "Faculty", filter: 'agTextColumnFilter', },
			//{ headerName: "Level", width: 100, field: "Level", filter: 'agTextColumnFilter', },
			{ headerName: "Semester", width: 100, field: "Semester", filter: 'agTextColumnFilter', },
			{ headerName: "ClassYear", width: 100, field: "ClassYear", filter: 'agTextColumnFilter', },
			{ headerName: "Fee Heading", width: 100, field: "FeeItemName", filter: 'agTextColumnFilter', },
			{ headerName: "Previous Dues", width: 120, field: "Opening", filter: 'agNumberColumnFilter', },
			{ headerName: "Current Fee", width: 150, field: "DrTotal", filter: 'agNumberColumnFilter', },
			{ headerName: "Paid Amount", width: 120, field: "TotalCredit", filter: 'agNumberColumnFilter', },
			{ headerName: "Discount Amt", width: 130, field: "CrDiscountAmt", filter: 'agNumberColumnFilter', },
			{ headerName: "Balance Amt", width: 140, field: "TotalDues", filter: 'agNumberColumnFilter', },
			{ headerName: "Father’s Name", width: 150, field: "FatherName", filter: 'agTextColumnFilter', },
			{ headerName: "F_ContactNo", width: 120, field: "F_ContactNo", filter: 'agTextColumnFilter', },
			{ headerName: "Address", width: 160, field: "Address", filter: 'agTextColumnFilter', },
			{ headerName: "IsLeft", width: 120, field: "IsLeft", filter: 'agTextColumnFilter', },
			{ headerName: "PointName", width: 140, field: "PointName", filter: 'agTextColumnFilter', },
			{ headerName: "RouteName", width: 140, field: "RouteName", filter: 'agTextColumnFilter', },
			{ headerName: "BoarderName", width: 140, field: "BoardersName", filter: 'agTextColumnFilter', },
			{ headerName: "CardNo", width: 100, field: "CardNo", filter: 'agNumberColumnFilter', },
			{ headerName: "EnrollNo", width: 100, field: "EnrollNo", filter: 'agNumberColumnFilter', },
			{ headerName: "LedgerPanaNo", width: 110, field: "LedgerPanaNo", filter: 'agTextColumnFilter', },

			{ headerName: "LastRec.Miti", width: 140, field: "LastReceiptMiti", filter: 'agTextColumnFilter', },
			{ headerName: "LastRec.No", width: 150, field: "LastReceiptNo", filter: 'agTextColumnFilter', },
			{ headerName: "LastRec.Amt", width: 150, field: "LastReceiptAmt", filter: 'agNumberColumnFilter', },
			{
				headerName: "Action",
				field: "icon",
				width: 115, pinned: 'right',
				//id: this.PartyMstId,
				cellRenderer: function (params) {
					return '<button ng-click="openFollowup(this)" class="btn btn-sm btn-info mt-1 py-0"> Follow-up</button>'
				}
			}
		];
		   
		$scope.gridOptions = {
			angularCompileRows: true,
			rowHeight: 31,
		  	headerHeight: 31,
			// a default column definition with properties that get applied to every column
			defaultColDef: {
				filter: true,
				resizable: true,
				sortable: true,

				// set every column width
				width: 90
			},
			columnDefs: columnDefs,
			overlayLoadingTemplate: "Please Click the Load Button to display the data.",
			enableColResize: true,
			rowData: null,
			filter: true,
			enableFilter: true,
			enableSorting: true,
			rowSelection: 'multiple',
			suppressHorizontalScroll: false,
			alignedGrids: [],
			onFilterChanged: function (e) {
				//console.log('onFilterChanged', e);
				var pDue = 0, cDue = 0, paidAmt = 0, disAmt = 0, balAmt = 0;
				$scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
					var tb = node.data;
					pDue += tb.Opening;
					cDue += tb.DrTotal;
					paidAmt += tb.TotalCredit;
					disAmt += tb.CrDiscountAmt;
					balAmt += tb.TotalDues;
				});

				$scope.dataForBottomGrid[0].Opening = pDue;
				$scope.dataForBottomGrid[0].DrTotal = cDue;
				$scope.dataForBottomGrid[0].TotalCredit = paidAmt;
				$scope.dataForBottomGrid[0].CrDiscountAmt = disAmt;
				$scope.dataForBottomGrid[0].TotalDues = balAmt;
				$scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);
				//console.log('gridApi.getFilterModel() =>', e.api.getFilterModel());
			},
		};
		    
		//-------------------------------------------------------------------------------------------------------------------------------------------------
		var columnDefs1 = [

			{ headerName: "Regd.No", field: "RegdNo", width: 110, pinned: 'left' },
			{ headerName: "Name", field: "Name", width: 170, pinned: 'left' },
			{ headerName: "Roll No", field: "RollNo", width: 100, pinned: 'left' },
			{ headerName: "Class", width: 100, field: "ClassName", filter: 'agTextColumnFilter', },
			{ headerName: "Section", width: 100, field: "SectionName", filter: 'agTextColumnFilter', },
			//{ headerName: "Batch", width: 100, field: "Batch", filter: 'agTextColumnFilter', },
			//{ headerName: "Faculty", width: 100, field: "Faculty", filter: 'agTextColumnFilter', },
			//{ headerName: "Level", width: 100, field: "Level", filter: 'agTextColumnFilter', },
			//{ headerName: "Semester", width: 100, field: "Semester", filter: 'agTextColumnFilter', },
			//{ headerName: "ClassYear", width: 100, field: "ClassYear", filter: 'agTextColumnFilter', },
			{ headerName: "Fee Heading", width: 100, field: "FeeItemName", filter: 'agTextColumnFilter', },
			{ headerName: "Previous Dues", width: 120, field: "Opening", filter: 'agNumberColumnFilter', },
			{ headerName: "Current Fee", width: 150, field: "DrTotal", filter: 'agNumberColumnFilter', },
			{ headerName: "Paid Amount", width: 120, field: "TotalCredit", filter: 'agNumberColumnFilter', },
			{ headerName: "Discount Amt", width: 130, field: "CrDiscountAmt", filter: 'agNumberColumnFilter', },
			{ headerName: "Balance Amt", width: 140, field: "TotalDues", filter: 'agNumberColumnFilter', },
			{ headerName: "FatherName", width: 150, field: "FatherName", filter: 'agTextColumnFilter', },
			{ headerName: "FollowupStatus", width: 160, field: "FollowupStatus", filter: 'agTextColumnFilter', },
			{ headerName: "FollowupRemarks", width: 160, field: "FollowupRemarks", filter: 'agTextColumnFilter', },
			{ headerName: "NextFollowupMiti", width: 160, field: "NextFollowupMiti", filter: 'agTextColumnFilter', },
			{ headerName: "NextFollowupBy", width: 160, field: "NextFollowupBy", filter: 'agTextColumnFilter', },		 
			{
				headerName: "Action",
				field: "icon",
				width: 175, pinned: 'right',
				//id: this.PartyMstId,
				cellRenderer: function (params) {
					return '<button ng-click="openFollowup(this)" class="btn btn-sm btn-info mt-1 py-0"> Followup</button><button ng-click="closeFollowup(this)" class="btn btn-sm btn-info mt-1 mr-1 ml-1 py-0"> Close</button>'
				}
			}
		];

		// let the grid know which columns and what data to use
		$scope.gridOptions1 = {
			angularCompileRows: true,
			columnDefs: columnDefs1,
			overlayLoadingTemplate: "Please Click the Load Button to display the data.",
			rowHeight: 31,
			headerHeight: 31,

			defaultColDef: {
				resizable: true,
				sortable: true,
				filter: true,
				resizable: true,
				cellStyle: { 'line-height': '31px' },
				rowSelection: 'multiple'
			},
		};

		  
		//-------------------------------------------------------------------------------------------------------------------------------------------------
	 
		// let the grid know which columns and what data to use
		$scope.gridOptions2 = {
			angularCompileRows: true,
			columnDefs: columnDefs1,
			overlayLoadingTemplate: "Please Click the Load Button to display the data.",
			rowHeight: 31,
			headerHeight: 31,

			defaultColDef: {
				resizable: true,
				sortable: true,
				filter: true,
				resizable: true,
				cellStyle: { 'line-height': '31px' },
				rowSelection: 'multiple'
			},
		};
		 
		// let the grid know which columns and what data to use
		$scope.gridOptions3 = {
			angularCompileRows: true,
			overlayLoadingTemplate: "Please Click the Load Button to display the data.",
			columnDefs: columnDefs1,
			rowHeight: 31,
			headerHeight: 31,

			defaultColDef: {
				resizable: true,
				sortable: true,
				filter: true,
				resizable: true,
				cellStyle: { 'line-height': '31px' },
				rowSelection: 'multiple'
			},
		};
		   

		//-------------------------------------------------------------------------------------------------------------------------------------------------
		var columnDefs4 = [
			{ headerName: "Regd.No", field: "RegNo", width: 110, pinned: 'left' },
			{ headerName: "Name", field: "Name", width: 170, pinned: 'left', filter: 'agTextColumnFilter', },
			{ headerName: "Roll No", field: "RollNo", width: 100, pinned: 'left' },
			{ headerName: "Class", width: 100, field: "ClassName", filter: 'agTextColumnFilter', },
			{ headerName: "Section", width: 100, field: "SectionName", filter: 'agTextColumnFilter', },
			{ headerName: "ForMonth", width: 100, field: "ForMonth", filter: 'agTextColumnFilter', },
			{ headerName: "Due Date", width: 120, field: "PaymentDueMiti", filter: 'agTextColumnFilter', },
			{ headerName: "Remarks", width: 120, field: "Remarks", filter: 'agTextColumnFilter', },
			{ headerName: "Followup Date", width: 170, field: "FollowupMiti", filter: 'agTextColumnFilter', },
			{ headerName: "FollowupStatus", width: 160, field: "FollowupStatus", filter: 'agTextColumnFilter', },
			{ headerName: "Followup By", width: 140, field: "FollowupBy", filter: 'agTextColumnFilter', },
			{ headerName: "Next Followup", width: 160, field: "NextFollowupMiti", filter: 'agNumberColumnFilter', },
			{ headerName: "ClosedRemarks", width: 170, field: "ClosedRemarks", filter: 'agNumberColumnFilter', },
			{ headerName: "ClosedBy", width: 120, field: "ClosedBy", filter: 'agNumberColumnFilter', },
			{ headerName: "Dues Amt", width: 130, field: "DuesAmt", filter: 'agNumberColumnFilter', },
			{ headerName: "FatherName", width: 180, field: "FatherName", filter: 'agTextColumnFilter', },
			{ headerName: "ContactNo", width: 130, field: "F_ContactNo", filter: 'agTextColumnFilter', },
			{ headerName: "Email", width: 200, field: "Email", filter: 'agTextColumnFilter', }, 
		];

		// let the grid know which columns and what data to use
		$scope.gridOptions4 = {
			columnDefs: columnDefs4,
			overlayLoadingTemplate: "Please Click the Load Button to display the data.",
			rowHeight: 31,
			headerHeight: 31,

			defaultColDef: {
				resizable: true,
				sortable: true,
				filter: true,
				resizable: true,
				cellStyle: { 'line-height': '31px' },
				rowSelection: 'multiple'
			},
		};
		 
	}

	$scope.onFilterTextBoxChanged = function () {
		$scope.gridOptions.api.setQuickFilter($scope.search);
	}


	$scope.onFilterTodayChanged = function () {
		$scope.gridOptions1.api.setQuickFilter($scope.searchToday);
	}

	$scope.onFilterPendingChanged = function () {
		$scope.gridOptions2.api.setQuickFilter($scope.searchPending);
	}

	$scope.onFilterUpcomingChanged = function () {
		$scope.gridOptions3.api.setQuickFilter($scope.searchUpcoming);
	}

	$scope.onFilterCompleteChanged = function () {
		$scope.gridOptions4.api.setQuickFilter($scope.searchComplete);
	}

	
	$scope.openFollowup = function (rowData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var beData = rowData.data;
		$scope.newFollowup = {
			StudentId: beData.StudentId,
			Name: beData.Name,
			RegdNo: beData.RegdNo,
			FatherName: beData.FatherName,
			F_ContactNo: beData.F_ContactNo,
			TotalDues: beData.TotalDues,
			ClassSection: beData.ClassName + (beData.SectionName ? ' ' + beData.SectionName : ''),
			NextFollowupRequired: false,
			UptoMonthId: $scope.feeSummary.ToMonthId,
			FeeItemId: $scope.feeSummary.FeeItemId,
			RefTranId: beData.RefTranId,
			DuesAmt:beData.TotalDues,
		};

		var para = {
			StudentId:beData.StudentId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetPaymentFollowupList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data)
			{
				$scope.newFollowup.HistoryColl = res.data.Data;
				if (!$scope.newFollowup.PaymentDueDate) {
					$scope.newFollowup.PaymentDueDate_TMP = new Date();
				}
				if (!$scope.newFollowup.NextFollowupDateTime) {
					$scope.newFollowup.NextFollowupDate_TMP = new Date();
				}
				$('#followup').modal('show');

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		
	}

	$scope.closeFollowup = function (rowData) {

		$scope.loadingstatus = "running";
		showPleaseWait();
		  
		var beData = rowData.data;
		$scope.newFollowup = {
			StudentId: beData.StudentId,
			Name: beData.Name,
			RegdNo: beData.RegdNo,
			FatherName: beData.FatherName,
			F_ContactNo: beData.F_ContactNo,
			TotalDues: beData.TotalDues,
			ClassSection: beData.ClassName + (beData.SectionName ? ' ' + beData.SectionName : ''),
			NextFollowupRequired: false,
			UptoMonthId: $scope.feeSummary.ToMonthId,
			FeeItemId: $scope.feeSummary.FeeItemId,
			RefTranId: beData.RefTranId,
			DuesAmt: beData.TotalDues,
		};

		var para = {
			StudentId: beData.StudentId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetPaymentFollowupList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFollowup.HistoryColl = res.data.Data;
				if (!$scope.newFollowup.PaymentDueDate) {
					$scope.newFollowup.PaymentDueDate_TMP= new Date();
                }
				if (!$scope.newFollowup.NextFollowupDateTime) {
					$scope.newFollowup.NextFollowupDate_TMP= new Date();
                }
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
		 

		if ($scope.newFollowup.PaymentDueDateDet) {
			$scope.newFollowup.PaymentDueDate = $filter('date')(new Date($scope.newFollowup.PaymentDueDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newFollowup.PaymentDueDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		if ($scope.newFollowup.NextFollowupDateDet) {
			$scope.newFollowup.NextFollowupDate = $filter('date')(new Date($scope.newFollowup.NextFollowupDateDet.dateAD), 'yyyy-MM-dd');
		}
		 
		if ($scope.newFollowup.NextFollowupTime_TMP)
			$scope.newFollowup.NextFollowupTime = $scope.newFollowup.NextFollowupTime_TMP.toLocaleString();
		else
			$scope.newFollowup.NextFollowupTime_TMP = null;
		 
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SavePaymentFollowup",
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
		if (
			!$scope.newFollowup.ClosedRemarks ||
			$scope.newFollowup.ClosedRemarks.trim() === ''
		) {
			Swal.fire({
				icon: 'warning',
				title: 'Please Enter Close Remarks.'
			});
			return; // stop execution
		}
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			RefTranId: $scope.newFollowup.RefTranId,
			Remarks: $scope.newFollowup.ClosedRemarks
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveFollowupClosed",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) { 
				$('#followupClosed').modal('hide');
				Swal.fire(res.data.ResponseMSG);
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		//$http({
		//	method: 'POST',
		//	url: base_url + "FrontDesk/Transaction/SaveFollowupClosed",
		//	headers: { 'Content-Type': undefined },

		//	transformRequest: function (data) {

		//		var formData = new FormData();
		//		formData.append("jsonData", angular.toJson(data.jsonData));

		//		return formData;
		//	},
		//	data: { jsonData: $scope.newFollowup }
		//}).then(function (res) {

		//	$scope.loadingstatus = "stop";
		//	hidePleaseWait();

		//	Swal.fire(res.data.ResponseMSG);

		//	if (res.data.IsSuccess == true) {
		//		$('#followup').modal('hide');
		//	}

		//}, function (errormessage) {
		//	hidePleaseWait();
		//	$scope.loadingstatus = "stop";

		//});
	}

	$rootScope.ConfigFunction = function () {
		$scope.LoadData();
		$('.select2').select2();

		$scope.AllClassList = [];
		GlobalServices.getClassList().then(function (res) {
			$scope.AllClassList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.AcademicConfig = {};
		//GlobalServices.getAcademicConfig().then(function (res1) {
		//	$scope.AcademicConfig = res1.data.Data;

		//	if ($scope.AcademicConfig.ActiveFaculty == true) {

		//		$scope.FacultyList = [];
		//		GlobalServices.getFacultyList().then(function (res) {
		//			$scope.FacultyList = res.data.Data;
		//		}, function (reason) {
		//			Swal.fire('Failed' + reason);
		//		});

		//	}
		//	//else
		//	//	$scope.gridOptions.columnApi.setColumnsVisible(["Faculty"], false);



		//	if ($scope.AcademicConfig.ActiveLevel == true) {

		//		$scope.LevelList = [];
		//		GlobalServices.getClassLevelList().then(function (res) {
		//			$scope.LevelList = res.data.Data;
		//		}, function (reason) {
		//			Swal.fire('Failed' + reason);
		//		});

		//	}
		//	//else
		//	//	$scope.gridOptions.columnApi.setColumnsVisible(["Level"], false);

		//	if ($scope.AcademicConfig.ActiveSemester == true) {

		//		$scope.SelectedClassSemesterList = [];
		//		$scope.SemesterList = [];
		//		GlobalServices.getSemesterList().then(function (res) {
		//			$scope.SemesterList = res.data.Data;
		//		}, function (reason) {
		//			Swal.fire('Failed' + reason);
		//		});

		//	}
		//	//else
		//	//	$scope.gridOptions.columnApi.setColumnsVisible(["Semester"], false);

		//	if ($scope.AcademicConfig.ActiveBatch == true) {

		//		$scope.BatchList = [];
		//		GlobalServices.getBatchList().then(function (res) {
		//			$scope.BatchList = res.data.Data;
		//		}, function (reason) {
		//			Swal.fire('Failed' + reason);
		//		});

		//	}
		//	//else
		//	//	$scope.gridOptions.columnApi.setColumnsVisible(["Batch"], false);

		//	if ($scope.AcademicConfig.ActiveClassYear == true) {

		//		$scope.ClassYearList = [];
		//		$scope.SelectedClassClassYearList = [];
		//		GlobalServices.getClassYearList().then(function (res) {
		//			$scope.ClassYearList = res.data.Data;
		//		}, function (reason) {
		//			Swal.fire('Failed' + reason);
		//		});

		//	}
		//	//else
		//	//	$scope.gridOptions.columnApi.setColumnsVisible(["ClassYear"], false);

		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

	};
	$rootScope.ChangeLanguage();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.searchData = {
			PaymentFollowup: '',
			History:''
		};

		$scope.FollowStatusList = [
			{ id: 1, text: 'Success' },
			{ id: 2, text: 'Failed' }
		];

		$scope.newFollowup = {
			PaymentDueDate_TMP: new Date(),
			NextFollowupDate_TMP: new Date(),
		};
		$scope.newComplete = {
			DateFrom_TMP: new Date()
		};

		$scope.newPaymentFollowup = {
			PaymentFollowupId: null,
			DateFrom_TMP: '',
			DateTo_TMP: '',
			Mode: 'Save'
		};

		$scope.feeSummary = {
			FromMonthId: 0,
			ToMonthId: 0,
			ForStudent: 0,
			FeeItemIdColl: '',
		};

		$scope.MonthList = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.FeeItemList = [];
		GlobalServices.getFeeItemList().then(function (res1) {
			$scope.FeeItemList = res1.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.AcademicConfig = {};
		//pahela res1 theyo...eslai res banako at 25 chaitra 2081
		GlobalServices.getAcademicConfig().then(function (res) {
			$scope.AcademicConfig = res.data.Data;

			if ($scope.AcademicConfig.ActiveFaculty == true) {

				$scope.FacultyList = [];
				GlobalServices.getFacultyList().then(function (res) {
					$scope.FacultyList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				$scope.gridOptions.columnApi.setColumnsVisible(["Faculty"], false);
			}




			if ($scope.AcademicConfig.ActiveLevel == true) {

				$scope.LevelList = [];
				GlobalServices.getClassLevelList().then(function (res) {
					$scope.LevelList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				$scope.gridOptions.columnApi.setColumnsVisible(["Level"], false);
			}


			if ($scope.AcademicConfig.ActiveSemester == true) {

				$scope.SelectedClassSemesterList = [];
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				$scope.gridOptions.columnApi.setColumnsVisible(["Semester"], false);
			}

			if ($scope.AcademicConfig.ActiveBatch == true) {

				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				$scope.gridOptions.columnApi.setColumnsVisible(["Batch"], false);
			}

			if ($scope.AcademicConfig.ActiveClassYear == true) {

				$scope.ClassYearList = [];
				$scope.SelectedClassClassYearList = [];
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["ClassYear"], false);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



	}

	$scope.ClearFollowup = function () {
		$scope.newFollowup.PaymentDueDate_TMP = null;
		$scope.newFollowup.StatusId = null;
		$scope.newFollowup.Remarks = '';
		$scope.newFollowup.NextFollowupRequired = false;
		$scope.newFollowup.NextFollowupDate_TMP = null;
		$scope.newFollowup.NextFollowupTime_TMP = null;
		$scope.newFollowup.Mode = 'Save';
	};

	$scope.getFeeSummary = function () {

		$scope.SelectedClassSemesterList = [];
		$scope.SelectedClassClassYearList = [];

		if ($scope.feeSummary.SelectedClass) {
			var findClass = $scope.AllClassList.firstOrDefault(p1 => p1.ClassId == $scope.feeSummary.SelectedClass.ClassId);
			if (findClass) {

				$scope.feeSummary.SelectedClass.ClassType = findClass.ClassType;

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
		}

		if ($scope.feeSummary.ToMonthId) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				fromMonthId: 1,
				toMonthId: $scope.feeSummary.ToMonthId,
				forStudent: $scope.feeSummary.ForStudent,
				feeItemIdColl: $scope.feeSummary.FeeItemIdColl,
				classId: $scope.feeSummary.SelectedClass ? $scope.feeSummary.SelectedClass.ClassId : 0,
				sectionId: $scope.feeSummary.SelectedClass ? $scope.feeSummary.SelectedClass.SectionId : 0,
				batchId: $scope.feeSummary.BatchId,
				semesterId: $scope.feeSummary.SemesterId,
				classYearId: $scope.feeSummary.ClassYearId,
				ForPaymentFollowup: true,
				FollowupType: 0
			};
			$http({
				method: 'POST',
				url: base_url + "Fee/Report/GetFeeSummary",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {

					//var DataColl = mx(res.data.Data);
					//$scope.dataForBottomGrid[0].Opening = DataColl.sum(p1 => p1.Opening);
					//$scope.dataForBottomGrid[0].DrTotal = DataColl.sum(p1 => p1.DrTotal);
					//$scope.dataForBottomGrid[0].TotalCredit = DataColl.sum(p1 => p1.TotalCredit);
					//$scope.dataForBottomGrid[0].CrDiscountAmt = DataColl.sum(p1 => p1.CrDiscountAmt);
					//$scope.dataForBottomGrid[0].TotalDues = DataColl.sum(p1 => p1.TotalDues);

					//$scope.gridOptionsBottom.api.setRowData($scope.dataForBottomGrid);

					$scope.gridOptions.api.setRowData(res.data.Data);
					
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.getTodayFeeSummary = function () {

	 
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			fromMonthId: 1,
			toMonthId: 15,
			forStudent: 0,
			feeItemIdColl: '',
			classId: 0,
			sectionId:  0,
			batchId: 0,
			semesterId: 0,
			classYearId: 0,
			ForPaymentFollowup: true,
			FollowupType: 1
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Report/GetFeeSummary",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$scope.gridOptions1.api.setRowData(res.data.Data);

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.getPendingFeeSummary = function () {


		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			fromMonthId: 1,
			toMonthId: 15,
			forStudent: 0,
			feeItemIdColl: '',
			classId: 0,
			sectionId: 0,
			batchId: 0,
			semesterId: 0,
			classYearId: 0,
			ForPaymentFollowup: true,
			FollowupType: 2
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Report/GetFeeSummary",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$scope.gridOptions2.api.setRowData(res.data.Data);

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.getUpcomingFeeSummary = function () {
		 
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			fromMonthId: 1,
			toMonthId: 15,
			forStudent: 0,
			feeItemIdColl: '',
			classId: 0,
			sectionId: 0,
			batchId: 0,
			semesterId: 0,
			classYearId: 0,
			ForPaymentFollowup: true,
			FollowupType: 3
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Report/GetFeeSummary",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$scope.gridOptions3.api.setRowData(res.data.Data);

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.getCompleteFeeSummary = function () {

		if ($scope.newComplete.DateFromDet) {

			$scope.loadingstatus = "running";
			showPleaseWait();

			var forDate =  $filter('date')(new Date($scope.newComplete.DateFromDet.dateAD), 'yyyy-MM-dd');
			var para = {
				dateFrom: forDate,
				dateTo: forDate,
			};
			$http({
				method: 'POST',
				url: base_url + "FrontDesk/Transaction/GetDateWisePaymentFollowupList",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {

					$scope.gridOptions4.api.setRowData(res.data.Data);

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        }	
	}
});