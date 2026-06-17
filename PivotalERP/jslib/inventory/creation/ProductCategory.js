app.controller('ProductCategoryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Product Category';


	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			ProductCategory: 1

		};

		$scope.searchData = {
			ProductCategory: ''

		};

		$scope.perPage = {
			ProductCategory: GlobalServices.getPerPageRow(),

		};


		$scope.newProductCategory = {
			ProductCategoryId: 0,
			Sn: null,
			Name: '',
			Code: '',
			Alias: '',
			ParentCategoriesId: 0,
			Mode: 'Save'
		};

		$scope.GetAllProductCategoryList();

	}

	function OnClickDefault() {
		document.getElementById('product-form').style.display = "none";

		// sections display and hide
		document.getElementById('add-product').onclick = function () {
			document.getElementById('product-section').style.display = "none";
			document.getElementById('product-form').style.display = "block";
			$scope.ClearProductCategory();
		}
		document.getElementById('back-to-list').onclick = function () {
			document.getElementById('product-form').style.display = "none";
			document.getElementById('product-section').style.display = "block";
			$scope.ClearProductCategory();
		}

	}

	$scope.ClearProductCategory = function () {

		$timeout(function () {
			$scope.newProductCategory = {
				ProductCategoryId: 0,
				Sn: null,
				Name: '',
				Code: '',
				Alias: '',
				ParentCategoriesId: 0,
				Mode: 'Save'
			};
		});
		
	}


	//************************* ProductCategory *********************************

	$scope.IsValidProductCategory = function () {
		if ($scope.newProductCategory.Name.isEmpty()) {
			Swal.fire('Please ! Enter ProductCategory Name');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateProductCategory = function () {
		if ($scope.IsValidProductCategory() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newProductCategory.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateProductCategory();
					}
				});
			} else
				$scope.CallSaveUpdateProductCategory();

		}
	};

	$scope.CallSaveUpdateProductCategory = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/SaveProductCategory",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newProductCategory }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearProductCategory();
				$scope.GetAllProductCategoryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllProductCategoryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ProductCategoryList = [];

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetAllProductCategoryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ProductCategoryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetProductCategoryById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ProductCategoryId: refData.ProductCategoryId
		};

		$http({
			method: 'POST',
			url: base_url + "Inventory/Creation/GetProductCategoryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newProductCategory = res.data.Data;
				$scope.newProductCategory.Mode = 'Modify';



			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelProductCategoryById = function (refData) {

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
					ProductCategoryId: refData.ProductCategoryId
				};

				$http({
					method: 'POST',
					url: base_url + "Inventory/Creation/DelProductCategory",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllProductCategoryList();
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