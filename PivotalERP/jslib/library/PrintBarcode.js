app.controller('PrintBarcodeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Print Barcode';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			PrintBarcode: 1,

		};

		$scope.searchData = {
			PrintBarcode: '',

		};

		$scope.perPage = {
			PrintBarcode: GlobalServices.getPerPageRow(),

		};

	

		$scope.newPrintBarcode = {
			fromAccessionNo: 0,
			toAccessionNo: 0,
			checkAll:false,
			Mode: 'Save'
		};
		
		//$scope.GetAllPrintBarcodeList();		
	}

	$scope.LoadBookDetails = function () {
		var fromNo = Number($scope.newPrintBarcode.fromAccessionNo);
		var toNo = Number($scope.newPrintBarcode.toAccessionNo);
		if (fromNo > toNo) {
			Swal.fire({text: "From Accession No cannot be greater than To Accession No",icon: "warning",confirmButtonText: "OK"});
			return false;
		}
		$scope.BookList = [];
		var para =
		{
			fromAccessionNo: fromNo,
			toAccessionNo: toNo
		};

		$http({
			method: 'POST',
			url: base_url + "Library/Master/GetBookDetForBarcode",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BookList = res.data.Data;
				if ($scope.BookList.length == 0 || !$scope.BookList) {
					Swal.fire({ text: "No books found for the selected accession number range", icon: "info", confirmButtonText: "OK" });
                }
			} else
				Swal.fire({ text: "No data available for the selected accession range", icon: "info", confirmButtonText: "OK" });
		}, function (reason) {
			Swal.fire({ text: 'Failed' + reason, icon: "error", confirmButtonText: "OK" });
		});
	}

	$scope.CheckAllData = function () {
		var val = $scope.newPrintBarcode.checkAll;
		angular.forEach($scope.BookList, function (bl) {
			bl.IsChecked = val;
		});
	}

	$scope.PrintBarCodeOfBook = function () {

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=432&voucherId=0&isTran=true",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				var templatesColl = res.data.Data;
				if (templatesColl && templatesColl.length > 0) {

					var dataColl = [];
					angular.forEach($scope.BookList, function (det) {

						if (det.IsChecked == true)
							dataColl.push(det);
					});

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
											$http({
												method: 'POST',
												url: base_url + "Library/Master/PrintLibraryBarCode",
												dataType: "json",
												data: JSON.stringify(dataColl)
											}).then(function (res) {
												if (res.data.IsSuccess && res.data.Data) {

													document.body.style.cursor = 'wait';
													document.getElementById("frmRpt").src = '';
													document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId+"&istransaction=true&entityid=432&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
													document.body.style.cursor = 'default';
													$('#FrmPrintReport').modal('show');

												} else
													Swal.fire('No Templates found for print');

											}, function (reason) {
												Swal.fire('Failed' + reason);
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
						$http({
							method: 'POST',
							url: base_url + "Library/Master/PrintLibraryBarCode",
							dataType: "json",
							data: JSON.stringify(dataColl)
						}).then(function (res) {
							if (res.data.IsSuccess && res.data.Data) {

								document.body.style.cursor = 'wait';
								document.getElementById("frmRpt").src = '';
								document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=432&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
								document.body.style.cursor = 'default';
								$('#FrmPrintReport').modal('show');

							} else
								Swal.fire('No Templates found for print');

						}, function (reason) {
							Swal.fire('Failed' + reason);
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