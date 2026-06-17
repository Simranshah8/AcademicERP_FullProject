

app.controller('SubledgerController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Subledger';

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
				{ name: "SNo", displayName: "S.No.", minWidth: 100, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "OpeningAmt", displayName: "Opening Amt.", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DrCr", displayName: "DrCr", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Alias", displayName: "Alias", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Code", displayName: "Code", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "CostCategory", displayName: "CostCategory", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Status", displayName: "Status", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Email", displayName: "Email", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Addrss", displayName: "Address", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PhoneNo", displayName: "PhoneNo", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "PanVatNo", displayName: "PanVatNo", minWidth: 140, headerCellClass: 'headerAligment' },				
				{ name: "ColumnHeader", displayName: "Column Header", minWidth: 140, headerCellClass: 'headerAligment' },
				
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
		//	Subledger: 1,

		//};

		//$scope.searchData = {
		//	Subledger: '',

		//};

		//$scope.perPage = {
		//	Subledger: GlobalServices.getPerPageRow(),

		//};

		$scope.newSubledger = {
			SubledgerId: null,

			Mode: 'Save'
		};

		//$scope.GetAllSubledgerList();

	};



	$scope.ClearSubledger = function () {
		$scope.newSubledger = {
			SubledgerId: null,

			Mode: 'Save'
		};

	};

	$scope.SaveUpdateSubledger = function () {
		if ($scope.IsValidSubledger() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSubledger.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSubledger();
					}
				});
			} else
				$scope.CallSaveUpdateSubledger();

		}
	};



	$scope.GetAllSubledgerList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SubledgerList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Reporting/GetAllSubledgerList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SubledgerList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};



});