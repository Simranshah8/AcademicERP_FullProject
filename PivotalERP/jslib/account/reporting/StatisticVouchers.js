
app.controller('StatisticVouchersController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'StatisticVouchers';

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

				{ name: "VoucherName", displayName: "Voucher Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "VoucherType", displayName: "Voucher Type", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "NoOfVoucher", displayName: "No. Of Voucher", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "NoOfCancel", displayName: "No. Of Cancel", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "NoOfPosted", displayName: "No. Of Posted", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "NoOfUnPost", displayName: "No. Of UnPost", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TotalVoucher", displayName: "Total Voucher", minWidth: 140, headerCellClass: 'headerAligment' },
				
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
		//	StatisticVouchers: 1,

		//};

		//$scope.searchData = {
		//	StatisticVouchers: '',

		//};

		//$scope.perPage = {
		//	StatisticVouchers: GlobalServices.getPerPageRow(),

		//};

		$scope.newStatisticVouchers = {
			StatisticVouchersId: null,

			Mode: 'Save'
		};

		//$scope.GetAllStatisticVouchersList();

	};



	$scope.ClearStatisticVouchers = function () {
		$scope.newStatisticVouchers = {
			StatisticVouchersId: null,

			Mode: 'Save'
		};

	};

	$scope.SaveUpdateStatisticVouchers = function () {
		if ($scope.IsValidStatisticVouchers() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStatisticVouchers.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStatisticVouchers();
					}
				});
			} else
				$scope.CallSaveUpdateStatisticVouchers();

		}
	};



	$scope.GetAllStatisticVouchersList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StatisticVouchersList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Reporting/GetAllStatisticVouchersList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StatisticVouchersList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};



});