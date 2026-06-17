app.controller('AboutusController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'About Us';

	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		
		$scope.newAboutUs = {
			AboutUsId: null,
			Content: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};
		
		$scope.GetAllAboutUsList();
		
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
	

	$scope.ClearAboutUs = function () {
		$scope.newAboutUs = {
			AboutUsId: null,
			Content: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};
	}

	

	$scope.ClearAboutUsSchoolPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newAboutUs.SchoolPhotoData = null;
				$scope.newAboutUs.SchoolPhoto_TMP = [];
				$scope.newAboutUs.SchoolPhoto = '';
			});

		});
		$('#imgSchoolPhoto1').attr('src', '');
		$('#imgSchoolPhoto').attr('src', '');
	};

	$scope.ClearAboutUsPhotoLogo = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newAboutUs.PhotoDataLogo = null;
				$scope.newAboutUs.PhotoLogo_TMP = [];
				$scope.newAboutUs.ImagePath = '';
			});

		});
		$('#imgLogo').attr('src', '');
		$('#imgLogo1').attr('src', '');
	};

	$scope.ClearAffiliatedLogo = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newAboutUs.AffiliatedLogoData = null;
				$scope.newAboutUs.AffiliatedLogo_TMP = [];
				$scope.newAboutUs.AffiliatedLogo = '';
			});

		});
		$('#imgAffiliatedLogo').attr('src', '');
		$('#imgAffiliatedLogo1').attr('src', '');
	};

	$scope.ClearAboutUsPhotoHeader = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newAboutUs.PhotoDataHeader = null;
				$scope.newAboutUs.PhotoHeader_TMP = [];
				$scope.newAboutUs.ImagePath = '';
			});

		});
		$('#imgHeader').attr('src', '');
		$('#imgHeader1').attr('src', '');
	};

	$scope.ClearAboutUsPhotoBanner = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newAboutUs.PhotoDataBanner = null;
				$scope.newAboutUs.PhotoBanner_TMP = [];
				$scope.newAboutUs.ImagePath = '';
			});

		});
		$('#imgBanner').attr('src', '');
		$('#imgBanner1').attr('src', '');
	};

	

	/*About Us Tab Js*/
	$scope.IsValidAboutUs = function () {
		if ($scope.newAboutUs.Content.isEmpty()) {
			Swal.fire('Please ! Enter Content');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAboutUs = function () {
		if ($scope.IsValidAboutUs() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAboutUs.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAboutUs();
					}
				});
			} else
				$scope.CallSaveUpdateAboutUs();

		}
	};

	$scope.CallSaveUpdateAboutUs = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var logo = $scope.newAboutUs.PhotoLogo_TMP;
		var img = $scope.newAboutUs.PhotoHeader_TMP;
		var banner = $scope.newAboutUs.PhotoBanner_TMP;
		var affiliated = $scope.newAboutUs.AffiliatedLogo_TMP;
		var school = $scope.newAboutUs.SchoolPhoto_TMP;

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveAboutUs",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.logo && data.logo.length > 0)
					formData.append("logo", data.logo[0]);
				
				if (data.school && data.school.length > 0)
					formData.append("school", data.school[0]);

				if (data.img && data.img.length > 0)
					formData.append("image", data.img[0]);

				if (data.banner && data.banner.length > 0)
					formData.append("banner", data.banner[0]);

				if (data.affiliated && data.affiliated.length > 0)
					formData.append("affiliated", data.affiliated[0]);

				return formData;
			},

			data: { jsonData: $scope.newAboutUs, logo: logo, school: school, img: img, banner: banner, affiliated: affiliated }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllAboutUsList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllAboutUsList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				if (res.data.Data.length > 0)
					$scope.newAboutUs = res.data.Data[0];

				$scope.newAboutUs.Mode = "Save";
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};
});