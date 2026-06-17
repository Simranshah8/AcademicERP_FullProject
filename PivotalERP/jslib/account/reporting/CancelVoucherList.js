
app.controller('CancelVoucherListController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Cancel Voucher List';

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

				{ name: "Date(A.D)", displayName: "Date(A.D)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Date(B.S)", displayName: "Date(B.S)", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Particulars", displayName: "Particulars", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "VoucherType", displayName: "Voucher Type", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "VoucherNo", displayName: "Voucher No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RefNo", displayName: "Ref. No.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DebitAmt", displayName: "Debit Amt.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CreditAmt", displayName: "Credit Amt.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CostClass", displayName: "Cost Class", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "User", displayName: "User", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CancelRemarks", displayName: "Cancel Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CancelDate", displayName: "Cancel Date", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CancelBy", displayName: "Cancel By", minWidth: 140, headerCellClass: 'headerAligment' },
			
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
		//$scope.perPageColl = GlobalServices.getPerPageList();


		//$scope.currentPages = {
		//	CancelVoucherList: 1,

		//};

		//$scope.searchData = {
		//	CancelVoucherList: '',

		//};

		//$scope.perPage = {
		//	CancelVoucherList: GlobalServices.getPerPageRow(),

		//};

		$scope.newCancelVoucherList = {
			CancelVoucherListId: null,

			Mode: 'Save'
		};

		//$scope.GetAllCancelVoucherListList();

	};



	$scope.ClearCancelVoucherList = function () {
		$scope.newCancelVoucherList = {
			CancelVoucherListId: null,

			Mode: 'Save'
		};

	};

	$scope.SaveUpdateCancelVoucherList = function () {
		if ($scope.IsValidCancelVoucherList() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCancelVoucherList.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCancelVoucherList();
					}
				});
			} else
				$scope.CallSaveUpdateCancelVoucherList();

		}
	};



	$scope.GetAllCancelVoucherListList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CancelVoucherListList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Reporting/GetAllCancelVoucherListList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CancelVoucherListList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};


    $scope.Print = function () {
        $http({
            method: 'GET',
            url: base_url + "ReportEngine/GetReportTemplates?entityId=" + EntityId + "&voucherId=0&isTran=false",
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
                                            var dataColl = $scope.GetDataForPrint();
                                            print = true;
                                            $http({
                                                method: 'POST',
                                                url: base_url + "Account/Reporting/PrintDayBook",
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

                                                    var findV = mx($scope.VoucherTypeList).firstOrDefault(p1 => p1.id == $scope.dayBook.VoucherId);

                                                    var rptPara = {
                                                        rpttranid: rptTranId,
                                                        istransaction: false,
                                                        entityid: EntityId,
                                                        voucherid: 0,
                                                        tranid: 0,
                                                        vouchertype: 0,
                                                        sessionid: res.data.Data.ResponseId,
                                                        Period: $scope.dayBook.DateFromDet.dateBS + " TO " + $scope.dayBook.DateToDet.dateBS,
                                                        Voucher: findV ? findV.text : ''
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

                                        }

                                    } else {
                                        resolve('You need to select:)')
                                    }
                                })
                            }
                        })
                    }

                    if (rptTranId > 0 && print == false) {
                        var dataColl = $scope.GetDataForPrint();
                        print = true;

                        $http({
                            method: 'POST',
                            url: base_url + "Account/Reporting/PrintDayBook",
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

                                var findV = mx($scope.VoucherTypeList).firstOrDefault(p1 => p1.id == $scope.dayBook.VoucherId);

                                var rptPara = {
                                    rpttranid: rptTranId,
                                    istransaction: false,
                                    entityid: EntityId,
                                    voucherid: 0,
                                    tranid: 0,
                                    vouchertype: 0,
                                    sessionid: res.data.Data.ResponseId,
                                    Period: $scope.dayBook.DateFromDet.dateBS + " TO " + $scope.dayBook.DateToDet.dateBS,
                                    Voucher: findV ? findV.text : ''
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

                    }

                } else
                    Swal.fire('No Templates found for print');
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.GetDataForPrint = function () {

        var RptParamentersColl = [];

        RptParamentersColl.push({
            Name: "Period",
            Value: $('#dtDateFrom').val() + ' To ' + $('#dtDateTo').val()
        });


        var filterData = [];

        $scope.gridOptions.api.forEachNodeAfterFilterAndSort(function (node) {
            var dayBook = node.data;
            if (dayBook.IsParent == true) {
                var beData = {};

                beData.VoucherName = dayBook.VoucherName;
                beData.VoucherType = dayBook.VoucherType;
                beData.AutoManualNo = dayBook.AutoManualNo;
                beData.AutoVoucherNo = dayBook.AutoVoucherNo;
                beData.CanUpdateDocument = dayBook.CanUpdateDocument;
                beData.CostClassName = dayBook.CostClassName;
                beData.IsInventory = dayBook.IsInventory;
                beData.IsParent = true;
                beData.Narration = dayBook.Narration;
                beData.ND = dayBook.ND;
                beData.NM = dayBook.NM;
                beData.NY = dayBook.NY;
                beData.RefNo = dayBook.RefNo;
                beData.VoucherDate = dayBook.VoucherDate;
                beData.VoucherDateStr = DateFormatAD(dayBook.VoucherDate);
                beData.VoucherDateStrNP = DateFormatBS(beData.NY, beData.NM, beData.ND);
                beData.CreatedByName = dayBook.CreatedByName;

                if (beData.IsInventory == true) {
                    beData.Particulars = dayBook.PartyLedger;
                    beData.DrAmount = dayBook.DrAmount;
                    beData.CrAmount = dayBook.CrAmount;
                    filterData.push(beData);

                    var ledData = {};
                    ledData.Particulars = "  " + dayBook.Particulars;

                    if (!dayBook.AditionalCostColl)
                        dayBook.AditionalCostColl = [];

                    if (dayBook.DrAmount != 0 && dayBook.AditionalCostColl.length > 0)
                        ledData.CrAmount = dayBook.DrAmount - mx(dayBook.AditionalCostColl).sum(p1 => p1.Amount);
                    else if (dayBook.AditionalCostColl.length > 0)
                        ledData.DrAmount = dayBook.CrAmount - mx(dayBook.AditionalCostColl).sum(p1 => p1.Amount);

                    ledData.VoucherType = dayBook.VoucherType;
                    filterData.push(ledData);

                    angular.forEach(dayBook.AditionalCostColl, function (add) {

                        var addData = {};
                        addData.Particulars = "  " + add.LedgerName;
                        if (dayBook.DrAmount != 0) {
                            addData.CrAmount = add.Amount;
                        }
                        else {
                            addData.DrAmount = add.Amount;
                        }
                        addData.VoucherType = dayBook.VoucherType;
                        filterData.push(addData);
                    });

                    if (!dayBook.ItemAllocationColl)
                        dayBook.ItemAllocationColl = [];

                    angular.forEach(dayBook.ItemAllocationColl, function (item) {

                        var itemData = {};
                        itemData.Particulars = "    " + item.ProductName + " ( " + item.BilledQty + item.UnitName + " @ " + item.Rate + " = " + item.Amount + " )";
                        itemData.VoucherType = dayBook.VoucherType;
                        filterData.push(itemData);

                    });

                } else {
                    var firstTime = true;

                    if (!dayBook.LedgerAllocationColl)
                        dayBook.LedgerAllocationColl = [];

                    angular.forEach(dayBook.LedgerAllocationColl, function (ledAll) {
                        if (firstTime) {
                            beData.Particulars = ledAll.LedgerName;
                            beData.DrAmount = ledAll.DrAmount;
                            beData.CrAmount = ledAll.CrAmount;
                            firstTime = false;
                            beData.VoucherType = dayBook.VoucherType;
                            filterData.push(beData);
                        }
                        else {
                            var chieldData = {};
                            chieldData.Particulars = "  " + ledAll.LedgerName;
                            chieldData.Narration = ledAll.Narration;
                            chieldData.DrAmount = ledAll.DrAmount;
                            chieldData.CrAmount = ledAll.CrAmount;
                            chieldData.VoucherType = dayBook.VoucherType;
                            filterData.push(chieldData);
                        }
                    });
                }
            }

        });

        return filterData;

    };

    $scope.DownloadAsXls = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();

        var dataColl = $scope.GetDataForPrint();

        var paraData = {
            Period: $scope.dayBook.DateFromDet.dateBS + " TO " + $scope.dayBook.DateToDet.dateBS,
            FYear: (dataColl && dataColl.length > 0 ? dataColl[0].ForYear : ''),
        };

        $http({
            method: 'POST',
            url: base_url + "Global/PrintXlsReportData",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("entityId", EntityId);
                formData.append("jsonData", angular.toJson(data.jsonData));
                formData.append("paraData", angular.toJson(paraData));
                formData.append("RptPath", "");
                return formData;
            },
            data: { jsonData: dataColl }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();
            if (res.data.IsSuccess && res.data.Data) {
                down_file(base_url + "//" + res.data.Data.ResponseId, "CancelVoucherList.xlsx");
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire(errormessage);
        });
    }


});