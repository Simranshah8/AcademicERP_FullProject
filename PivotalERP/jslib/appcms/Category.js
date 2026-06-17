app.controller('CategoryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Category';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			Category: 1,

		};

		$scope.searchData = {
			Category: '',

		};

		$scope.perPage = {
			Category: GlobalServices.getPerPageRow(),

		};
		$scope.newCategory = {
			CategoryId: 0,
			Name: '',
			OrderNo: null,
			Description: '',
			Mode: 'Save'
		};
		$scope.GetAllCategoryList();

	}

	function OnClickDefault() {

		document.getElementById('Category-form').style.display = "none";

		//Category section
		document.getElementById('add-Category').onclick = function () {
			document.getElementById('Category-section').style.display = "none";
			document.getElementById('Category-form').style.display = "block";
			$timeout(function () {
				$scope.ClearCategory();
			});

		}

		document.getElementById('back-to-list-Category').onclick = function () {
			document.getElementById('Category-form').style.display = "none";
			document.getElementById('Category-section').style.display = "block";
			$timeout(function () {
				$scope.ClearCategory();
			});
		}

	}

	$scope.ClearCategory = function () {
		$scope.newCategory = {
			CategoryId: 0,
			Name: '',
			OrderNo: null,
			Description: '',
			Mode: 'Save'
		};

	}

	//*************************Category *********************************

	$scope.IsValidCategory = function () {
		if ($scope.newCategory.Name.isEmpty()) {
			Swal.fire('Please ! Enter Category Name');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateCategory = function () {
		if ($scope.IsValidCategory() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCategory.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCategory();
					}
				});
			} else
				$scope.CallSaveUpdateCategory();

		}
	};

	$scope.CallSaveUpdateCategory = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveNewsCategory",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newCategory }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCategory();
				$scope.GetAllCategoryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllCategoryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CategoryList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllNewsCategory",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CategoryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetCategoryById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			CategoryId: refData.CategoryId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetNewsCategoryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCategory = res.data.Data;
				$scope.newCategory.Mode = 'Modify';

				document.getElementById('Category-section').style.display = "none";
				document.getElementById('Category-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCategoryById = function (refData) {

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
					CategoryId: refData.CategoryId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelNewCategory",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCategoryList();
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