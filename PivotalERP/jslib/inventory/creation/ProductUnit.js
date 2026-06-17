app.controller('ProductUnitController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Product Unit';


	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			ProductUnit: 1

		};

		$scope.searchData = {
			ProductUnit: ''

		};

		$scope.perPage = {
			ProductUnit: GlobalServices.getPerPageRow(),

		};


		$scope.newProductUnit = {
			ProductUnitId: 0,			
			Name: '',
			NoOfDecimalPlaces: 0,
			Alias: '',			
			Mode: 'Save'
		};

		$scope.GetAllProductUnitList();

	}

	

	$scope.ClearProductUnit = function () {
		$scope.newProductUnit = {
			ProductUnitId: 0,
			Name: '',
			NoOfDecimalPlaces: 0,
			Alias: '',
			

			Mode: 'Save'
		};
	}
	function OnClickDefault() {
		document.getElementById('productunit-form').style.display = "none";

		document.getElementById('add-productunit-btn').onclick = function () {
			document.getElementById('productunit-table').style.display = "none";
			document.getElementById('productunit-form').style.display = "block";
			$scope.ClearProductUnit();
		}

		document.getElementById('backlist').onclick = function () {
			document.getElementById('productunit-table').style.display = "block";
			document.getElementById('productunit-form').style.display = "none";
			$scope.ClearProductUnit();
		}
	};

	//************************* ProductUnit *********************************

	$scope.IsValidProductUnit = function () {
		if ($scope.newProductUnit.Name.isEmpty()) {
			Swal.fire('Please ! Enter  Unit Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateProductUnit = function () {
		if ($scope.IsValidProductUnit() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newProductUnit.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateProductUnit();
					}
				});
			} else
				$scope.CallSaveUpdateProductUnit();

		}
	};

	$scope.CallSaveUpdateProductUnit = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/SaveUnit",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newProductUnit }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearProductUnit();
				$scope.GetAllProductUnitList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllProductUnitList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ProductUnitList = [];

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetAllUnitList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ProductUnitList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetProductUnitById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			UnitId: refData.UnitId
		};

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetUnitById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newProductUnit = res.data.Data;
				$scope.newProductUnit.Mode = 'Modify';

				document.getElementById('productunit-table').style.display = "none";
				document.getElementById('productunit-form').style.display = "block";


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelProductUnitById = function (refData) {

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
					UnitId: refData.UnitId
				};

				$http({
					method: 'POST',
					url: base_url + "Inventory/Creation/DelUnit",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllProductUnitList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});