app.controller('ServicesAndFacilitiesController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Services And Facilities';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			ServicesAndFacilities: 1
			
		};

		$scope.searchData = {
			ServicesAndFacilities: ''
			
		};

		$scope.perPage = {
			ServicesAndFacilities: GlobalServices.getPerPageRow()
			
		};

		$scope.newServicesAndFacilities = {
			ServicesAndFacilitiesId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			Content: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};

		
		$scope.GetAllServicesAndFacilitiesList();
		
	}

	function OnClickDefault() {


		document.getElementById('notice-form').style.display = "none";


		document.getElementById('open-form-btn').onclick = function () {
			document.getElementById('table-listing').style.display = "none";
			document.getElementById('notice-form').style.display = "block";
			$scope.ClearServicesAndFacilities();
		}
		document.getElementById('back-to-list').onclick = function () {
			document.getElementById('table-listing').style.display = "block";
			document.getElementById('notice-form').style.display = "none";
			$scope.ClearServicesAndFacilities();
		}

	}

	$scope.ClearServicesAndFacilities = function () {
		$scope.newServicesAndFacilities = {
			ServicesAndFacilitiesId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			Content: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};
	}
	
	$scope.ClearServicesAndFacilitiesPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newServicesAndFacilities.PhotoData = null;
				$scope.newServicesAndFacilities.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};
	

	//************************* Services And Facilities *********************************

	$scope.IsValidServicesAndFacilities = function () {
		if ($scope.newServicesAndFacilities.Title.isEmpty()) {
			Swal.fire('Please ! Enter Title');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateServicesAndFacilities = function () {
		if ($scope.IsValidServicesAndFacilities() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newServicesAndFacilities.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateServicesAndFacilities();
					}
				});
			} else
				$scope.CallSaveUpdateServicesAndFacilities();

		}
	};

	$scope.CallSaveUpdateServicesAndFacilities = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var photo = $scope.newServicesAndFacilities.Photo_TMP;
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveServicesAndFacilities",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				
				if (data.stPhoto && data.stPhoto.length > 0)
					formData.append("photo", data.stPhoto[0]);


				return formData;
			},
			data: { jsonData: $scope.newServicesAndFacilities,stPhoto: photo}
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearServicesAndFacilities();
				$scope.GetAllServicesAndFacilitiesList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
		
	}

	$scope.GetAllServicesAndFacilitiesList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ServicesAndFacilitiesList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllServicesAndFacilitiesList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ServicesAndFacilitiesList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetServicesAndFacilitiesById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ServicesAndFacilitiesId: refData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetServicesAndFacilitiesById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newServicesAndFacilities = res.data.Data;
				$scope.newServicesAndFacilities.Mode = 'Modify';

				document.getElementById('table-listing').style.display = "none";
				document.getElementById('notice-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelServicesAndFacilitiesById = function (refData) {

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
					ServicesAndFacilitiesId: refData.TranId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelServicesAndFacilities",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllServicesAndFacilitiesList();
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