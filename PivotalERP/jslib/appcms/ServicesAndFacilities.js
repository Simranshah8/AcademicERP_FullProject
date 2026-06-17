app.controller('ServicesAndFacilitiesController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Notice';

	OnClickDefault();
	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			ServicesFacilities: 1
		};

		$scope.searchData = {
			ServicesFacilities: ''
		};

		$scope.perPage = {
			ServicesFacilities: GlobalServices.getPerPageRow()
		};

		$scope.newServicesFacilities = {
			ServicesFacilitiesId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			Content: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};	

		$scope.GetAllServicesFacilitiesList();
		
	}
	$scope.ShowPersonalImg = function (item) {
		$scope.viewImg = {
			ContentPath: ''
		};
		if (item.ImagePath && item.ImagePath.length > 0) {
			$scope.viewImg.ContentPath = item.ImagePath;
			$('#PersonalImg').modal('show');
		} else
			Swal.fire('No Image Found');

	};
	//$scope.GetAllFeedbackList = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	$scope.FeedbackList = [];

	//	$http({
	//		method: 'POST',
	//		url: base_url + "AppCMS/Creation/GetFeedbackList",
	//		dataType: "json"
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess && res.data.Data) {
	//			$scope.FeedbackList = res.data.Data;

	//		} else {
	//			Swal.fire(res.data.ResponseMSG);
	//		}

	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});


	//}
	function OnClickDefault() {
		document.getElementById('sf-form').style.display = "none";

		document.getElementById('add-sf').onclick = function () {
			$scope.ClearServicesFacilities();
			document.getElementById('sf-table-listing').style.display = "none";
			document.getElementById('sf-form').style.display = "block";
		}
		document.getElementById('back-to-sf-list').onclick = function () {
			$scope.ClearServicesFacilities();
			document.getElementById('sf-table-listing').style.display = "block";
			document.getElementById('sf-form').style.display = "none";
		}
		
	};

	/*Services and Facilities Tab Js*/
	$scope.ClearServicesFacilitiesPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newServicesFacilities.PhotoData = null;
				$scope.newServicesFacilities.Photo_TMP = [];
				$scope.newServicesFacilities.ImagePath = '';
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};


	$scope.ClearServicesFacilities = function () {
		$scope.ClearServicesFacilitiesPhoto();
		$scope.newServicesFacilities = {
			ServicesFacilitiesId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			Content: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};
	}

	$scope.IsValidServicesFacilities = function () {
		if ($scope.newServicesFacilities.Title.isEmpty()) {
			Swal.fire('Please ! Enter Heading');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateServicesFacilities = function () {
		if ($scope.IsValidServicesFacilities() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newServicesFacilities.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateServicesFacilities();
					}
				});
			} else
				$scope.CallSaveUpdateServicesFacilities();

		}
	};

	$scope.CallSaveUpdateServicesFacilities = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newServicesFacilities.Photo_TMP;


		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveServicesAndFacilities",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						if (data.files[i].File)
							formData.append("photo", data.files[i].File);
						else
							formData.append("photo", data.files[i]);
					}
				}

				return formData;
			},

			data: { jsonData: $scope.newServicesFacilities, files: filesColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearServicesFacilities();
				$scope.GetAllServicesFacilitiesList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllServicesFacilitiesList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ServicesFacilitiesList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllServicesAndFacilitiesList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ServicesFacilitiesList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetServicesFacilitiesById = function (refData) {
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
				$scope.newServicesFacilities = res.data.Data;
				$scope.newServicesFacilities.Mode = 'Modify';

				document.getElementById('sf-table-listing').style.display = "none";
				document.getElementById('sf-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelServicesAndFacilities = function (refData) {

		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Title + '?',
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
						$scope.GetAllServicesFacilitiesList();
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