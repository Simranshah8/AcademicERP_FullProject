app.controller('StudentStatementController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {
	$scope.Title = 'Student Statement';

	/*OnClickDefault();*/

	getterAndSetter();

	//$rootScope.ConfigFunction = function () {
		
	//};
	$rootScope.ChangeLanguage();

	$rootScope.ConfigFunction = function () {
		$scope.LoadData();
		var findInd = -1;
		$scope.gridApi.grid.refresh();
		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;		
			if ($scope.AcademicConfig.ActiveSemester == false) {
				findInd = $scope.gridOptions1.columnDefs.findIndex(function (obj) { return obj.name == 'Semester' });				
				if (findInd != -1) {
					$scope.gridOptions1.columnDefs.splice(findInd, 1);
				}				
			} else {
				$scope.SelectedClassSemesterList = [];
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}			

			if ($scope.AcademicConfig.ActiveClassYear == false) {
				findInd = $scope.gridOptions1.columnDefs.findIndex(function (obj) { return obj.name == 'ClassYear' });				
				if (findInd != -1) {
					$scope.gridOptions1.columnDefs.splice(findInd, 1);
				}				
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

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		/*$scope.perPageColl = GlobalServices.getPerPageList();*/
		//$scope.MonthList = GlobalServices.getMonthList();
		$scope.MonthList = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();


		$scope.currentPages = {
			StudentLedger: 1,
			StudentVoucher: 1,
			ClassLedger: 1
		};

		$scope.searchData = {
			StudentLedger: '',
			StudentVoucher: '',
			ClassLedger: ''
		};

		$scope.perPage = {
			StudentLedger: GlobalServices.getPerPageRow(),
			StudentVoucher: GlobalServices.getPerPageRow(),
			ClassLedger: GlobalServices.getPerPageRow()

		};

		$scope.newStudentLedger = {
			StudentLedgerId: null,
			ShowLeftStudent:false,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			TemplatesColl:[],
			Mode: 'Save'
		};


		$scope.newStudentVoucher = {
			StudentId: null,
			ShowLeftStudent: false,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			Mode: 'Save'
		};
		

		$scope.newClassLedger = {
			ClassLedgerId: null,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			Mode: 'Save'
		};

		$scope.ClassList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityStudentLedger + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newStudentLedger.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.entity = {
			ManualBilling: 435,
			FeeReceipt: 428,
			FeeReturn: 497
		};

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityStudentVoucher + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newStudentVoucher.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.entity = {
		//	ManualBilling: 435,
		//	FeeReceipt: 428,
		//	FeeReturn: 497
		//};
	}

	//************************* Student Ledger *********************************


	$scope.PrintPDFStudentVoucher = function () {

		if ($scope.newStudentVoucher.RptTranId == -1) {
			$("#profile").printThis(
				{
					importCSS: true,
					importStyle: true,
					loadCSS: "/Content/printrules.css"
				}
			);
		} else {
			$scope.PrintStudentVoucher();
        }
		
	};

	function getterAndSetter() {
		$scope.discounts = [];
		$scope.gridOptions1 = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			//enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,

			columnDefs: [
				{
					name: 'Action',
					enableHiding: false,
					enableFiltering: false,
					enableSorting: false,
					minWidth: 80,
					enableColumnResizing: false,
					cellTemplate: `<a href="" ng-if="row.entity.$$treeLevel == 0 && !(row.entity.TranType==1 || row.entity.TranType==2)" class="p-1" title="Click For Re-Print" ng-click="grid.appScope.PrintVoucher(row.entity)">
										<i class="fas fa-eye text-secondary"></i>
									</a>`
				},
				{ name: "VoucherDate_BS", displayName: "Date", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "VoucherType", displayName: "Voucher Type", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "VoucherNo", displayName: "Voucher No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Particulars", displayName: "Particulars", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Amount", displayName: "Amount", cellClass: 'numericAlignment', cellFilter: 'number', minWidth: 140, headerCellClass: 'headerAligment' },
				{
					name: "DisAmt", displayName: "Discount", cellClass: 'numericAlignment', cellFilter: 'number', minWidth: 140, headerCellClass: 'headerAligment'
				},
				{
					name: "Debit", displayName: "Debit Amount", cellClass: 'numericAlignment', cellFilter: 'number', minWidth: 140, headerCellClass: 'headerAligment',
					 
				},
				{
					name: "Credit", displayName: "Credit Amount", cellClass: 'numericAlignment', cellFilter: 'number', minWidth: 140, headerCellClass: 'headerAligment',
				
				},
				{
					name: "CurClosing", displayName: "Balance Amount", cellClass: 'numericAlignment', cellFilter: 'number', minWidth: 140, headerCellClass: 'headerAligment',

				},
				{ name: "Narration", displayName: "Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "AVoucherNo", displayName: "Ref.Voucher No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FeeSource", displayName: "Source", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "ClassYear", minWidth: 140, headerCellClass: 'headerAligment' },
			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'StudentVoucher.csv',
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

	$scope.PrintVoucher = function (tmpDet) {
		if (tmpDet != null && (tmpDet.TranType == 3 || tmpDet.TranType == 4 || tmpDet.TranType == 5 || tmpDet.TranType == 6 || tmpDet.TranType == 7))
		{
			var printEntityId = $scope.entity.FeeReceipt;

			if (tmpDet.TranType == 4 | tmpDet.TranType == 5 || tmpDet.TranType == 6)
				printEntityId = $scope.entity.ManualBilling;
			else if (tmpDet.TranType == 3)
				printEntityId = $scope.entity.FeeReceipt;
			else if (tmpDet.TranType == 7)
				printEntityId = $scope.entity.FeeReturn;

			var TranId = tmpDet.TranId;

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + printEntityId + "&voucherId=0&isTran=true",
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
												document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + printEntityId + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";
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
							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + printEntityId + "&voucherid=0&tranid=" + TranId + "&vouchertype=0";
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

	$scope.StudentVoucher = {};
	$scope.getStudentVoucher = function ()
	{
		var frColl = [];
		$scope.gridOptions1.data = frColl;
		$scope.StudentVoucher = null;

		if ($scope.newStudentVoucher.StudentId ) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newStudentVoucher.StudentId,
				SemesterId: ($scope.newStudentVoucher.DisplayAllTran==true ? null : ($scope.newStudentVoucher.StudentDetails ? $scope.newStudentVoucher.StudentDetails.SemesterId : null)),
				ClassYearId: ($scope.newStudentVoucher.DisplayAllTran == true ? null : ($scope.newStudentVoucher.StudentDetails ? $scope.newStudentVoucher.StudentDetails.ClassYearId : null)),
			};
			$http({
				method: 'POST',
				url: base_url + "Fee/Report/GetStudentVoucher",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res)
			{
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data)
				{
					$scope.StudentVoucher = res.data.Data;

					if (!$scope.StudentVoucher.PhotoPath || $scope.StudentVoucher.PhotoPath.length == 0)
						$scope.StudentVoucher.PhotoPath = '/wwwroot/dynamic/images/avatar-img.jpg';

					frColl = [];					
					angular.forEach($scope.StudentVoucher.VoucherColl, function (fr) {						
						fr.$$treeLevel = 0;
						frColl.push(fr);
						angular.forEach(fr.DetailsColl, function (det) {
							det.$$treeLevel = 1;
							det.Particulars = ' - ' + det.FeeItem;							
							frColl.push(det);
						});						
					});
					$scope.gridOptions1.data = frColl;     

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}


	$scope.StudentLedger = {};
	$scope.getStudentLedger = function () {
		$scope.StudentLedger = null;
		if ($scope.newStudentLedger.StudentId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newStudentLedger.StudentId,
				SemesterId: ($scope.newStudentLedger.StudentDetails ? $scope.newStudentLedger.StudentDetails.SemesterId : null),
				ClassYearId: ($scope.newStudentLedger.StudentDetails ? $scope.newStudentLedger.StudentDetails.ClassYearId : null),
			};
			$http({
				method: 'POST',
				url: base_url + "Fee/Report/GetStudentLedger",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.StudentLedger = res.data.Data;

					$scope.StudentLedger.LedgerColl = [];

					var dataColl = mx($scope.StudentLedger.LedgerDetailsColl);
					var query = dataColl.groupBy(t => t.Particular).toArray();
					var queryFeeHeading = dataColl.groupBy(t => t.FeeHeading).toArray();

					$timeout(function () {
						$scope.$apply(function () {
							$scope.StudentLedger.FeeHeadingColl = [];
							angular.forEach(queryFeeHeading, function (f)
							{
								if (f.key && f.key.length > 0 && f.key!='Opening') {
									$scope.StudentLedger.FeeHeadingColl.push({
										id: f.key,
										text: f.key
									});
                                }								
							});
						});
					});

					$timeout(function ()
					{
						var sno = 1;
						var totalDues = 0;
						var opening = $scope.StudentLedger.OpeningAmt;

						angular.forEach(query, function (q) {
							var elColl = mx(q.elements);
							totalDues = totalDues + elColl.sum(p1 => p1.PDues + p1.Debit - p1.Credit);
							var beData = {
								SNo: sno,
								MonthName: q.key,
								Opening:opening,
								//Amount: opening+ elColl.sum(p1 => p1.PDues + p1.Debit),
								Amount: opening + elColl.sum(p1 => p1.Debit),
								DisAmt: elColl.sum(p1 => p1.Discount),
								PaidAmt: elColl.sum(p1 => p1.Paid),
								Balance: totalDues,
								RefNo: elColl.max(p1 => p1.RefNo),
								VoucherNo: '',
								VoucherDate: '',
								Narration:'',
								DataColl: []
							};

							angular.forEach(elColl, function (el) {

								if (beData.VoucherNo.length > 0) {
									beData.VoucherNo = beData.VoucherNo + ",";
									beData.VoucherDate = beData.VoucherDate + ",";
                                }
									
								beData.VoucherNo = beData.VoucherNo + el.VoucherNo;
								beData.VoucherDate = beData.VoucherDate + el.VoucherDate;
								beData.Narration = beData.Narration + el.Narration;
							});
							
							var sno1 = 1;
							angular.forEach($scope.StudentLedger.FeeHeadingColl, function (fi)
							{																
								beData.DataColl.push({
									SNo:sno1,
									FeeHeading: fi.text,
									DebitAmt: elColl.where(p1 => p1.FeeHeading == fi.text).sum(p1 => p1.Debit)
								});
								sno1++;
							})
							sno++;

							$scope.StudentLedger.LedgerColl.push(beData);

							if(sno>1)
								opening = totalDues;
						});

					});
					
					
					if (!$scope.StudentLedger.PhotoPath || $scope.StudentLedger.PhotoPath.length == 0)
						$scope.StudentLedger.PhotoPath = '/wwwroot/dynamic/images/avatar-img.jpg';


				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}
	$scope.PrintPDFStudentLedger = function () {
		$("#divLedger").printThis(
			{
				importCSS: true,
				importStyle: true,
				loadCSS: "/Content/printrules.css"
			}
		);
	};

	$scope.PrintStudentLedger = function () {
		if ($scope.newStudentLedger.StudentId && $scope.newStudentLedger.RptTranId > 0) {

			var EntityId = entityStudentLedger;

			var rptPara = {
				studentId: $scope.newStudentLedger.StudentId,
				rptTranId: $scope.newStudentLedger.RptTranId
			};
			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRpt").src = '';
			document.getElementById("frmRpt").style.width = '100%';
			document.getElementById("frmRpt").style.height = '1300px';
			document.getElementById("frmRpt").style.visibility = 'visible';
			document.getElementById("frmRpt").src = base_url + "Fee/Report/RptStudentLedger?" + paraQuery;
			$('#FrmPrintReport').modal('show');
		} else if ($scope.newStudentLedger.StudentId > 0 && $scope.newStudentLedger.RptTranId == -1)
		{
			$scope.PrintPDFStudentLedger();
		}
	};
	

	//************************* Student Voucher *********************************

	$scope.GetDataForPrint = function () {
		var dataColl = [];
		angular.forEach($scope.StudentVoucher.VoucherColl, function (v) {
			v.IsParent = true;
			dataColl.push(v);
			angular.forEach(v.DetailsColl, function (det) {
				det.Particulars = ' - ' + det.FeeItem;
				det.IsParent = false;
				dataColl.push(det);
			});			 
		});

		return dataColl;
	}

	$scope.PrintStudentVoucher = function () {

		var dataColl = $scope.GetDataForPrint();
	
		print = true;
		$http({
			method: 'POST',
			url: base_url + "Global/PrintReportData",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("entityId", entityStudentVoucher);
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: dataColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			if (res.data.IsSuccess && res.data.Data) {

				var v = $scope.StudentVoucher;	  
				var rptPara = {
					rpttranid: $scope.newStudentVoucher.RptTranId,
					istransaction: false,
					entityid: entityStudentVoucher,
					voucherid: 0,
					tranid: 0,
					vouchertype: 0,
					sessionid: res.data.Data.ResponseId,
					StudentId: v.StudentId,
					Name: v.Name,
					RollNo: v.RollNo,
					RegNo: v.RegNo,
					ClassName: v.ClassName,
					SectionName: v.SectionName,
					FatherName: v.FatherName,
					MotherName: v.MotherName,
					ContactNo: v.ContactNo,
					Address: v.Address,
					PhotoPath: v.PhotoPath,
					BillUpToMonth: v.BillUpToMonth,
					OpeningAmt: v.OpeningAmt,
					FeeAmt: v.FeeAmt,
					DiscountAmt: v.DiscountAmt,
					PaidAmt: v.PaidAmt,
					BalanceAmt: v.BalanceAmt,
					Level: v.Level,
					Faculty: v.Faculty,
					Semester: v.Semester,
					ClassYear: v.ClassYear,
					Batch: v.Batch,
					AcademicYear:v.AcademicYear,
				};
				var paraQuery = param(rptPara);

				document.body.style.cursor = 'wait';
				document.getElementById("frmRpt").src = '';
				document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
				document.body.style.cursor = 'default';
				$('#FrmPrintReport').modal('show');
				 

			} else
				Swal.fire('No Templates found for print');

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(errormessage);
		});
 
	};


	

});