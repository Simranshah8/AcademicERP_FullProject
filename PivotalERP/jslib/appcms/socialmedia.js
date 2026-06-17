app.controller('SocialMediaController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Social Media';
	 
	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		 
		$scope.currentPages = {
			SocialMedia: 1,

		};

		$scope.searchData = {
			SocialMedia: '',

		};

		$scope.perPage = {
			SocialMedia: GlobalServices.getPerPageRow(),

		};

		$scope.allowedSocialDomains = [
			'facebook.com',
			'instagram.com',
			'x.com',
			'twitter.com',
			'linkedin.com',
			'youtube.com',
			'whatsapp.com'
		];


		$scope.newSocialMedia = {
			SocialMediaId: null,
			Title: '',
			Description: '',
			Image: '',
			OrderNo: null,
			Mode: 'Save'
		};

		$scope.GetAllSocialMediaList();

	}
  
	$scope.ClearSocialMediaPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newSocialMedia.PhotoData = null;
				$scope.newSocialMedia.Photo_TMP = [];
				$scope.newSocialMedia.ImagePath = '';
			});

		});
		$('input[type=file]').val('');
		$('#imgPhoto1').attr('src', '');

	};


	$scope.ClearSocialMedia = function () {

		$scope.ClearSocialMediaPhoto();


		$scope.newSocialMedia = {
			SocialMediaId: null,
			Title: '',
			Description: '',
			Image: '',
			OrderNo: null,
			Mode: 'Save'
		};

	}

	$scope.IsValidSocialMedia = function () {
		//if ($scope.newSocialMedia.Title.isEmpty()) {
		//	Swal.fire('Please ! Enter Title');
		//	return false;
		//}

		if ($scope.hasInvalidUrl()) {
			Swal.fire({
				icon: 'error',
				title: 'Invalid URL',
				text: 'Please enter only valid social media URLs (Facebook, Instagram, YouTube, etc.)'
			});
			return false;
		}
		return true;
	}
	 
	$scope.SaveUpdateSocialMedia = function () {
		if ($scope.IsValidSocialMedia() == true) {

			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSocialMedia.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSocialMedia();
					}
				});
			} else
				$scope.CallSaveUpdateSocialMedia();

		}
	};

	$scope.CallSaveUpdateSocialMedia = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

	 
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveSocialMedia",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				var find = 0;
				angular.forEach(data.jsonData, function (sm) {
					if (sm.Icon_TMP) {
						formData.append("file" + find, sm.Icon_TMP[0]);
                    }
					find++;
				});
 
				return formData;
			},
			data: { jsonData: $scope.SocialMediaList}
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
 

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.AddNewUrl = function () {
		$scope.SocialMediaList.push({
			IsActive: true,
			OrderNo:0
		});
    }
	$scope.delURL = function (ind) {
		if ($scope.SocialMediaList) {

			var refData = $scope.SocialMediaList[ind];

			if (refData.SocialMediaId > 0) {
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
							SocialMediaId: refData.SocialMediaId
						};

						$http({
							method: 'POST',
							url: base_url + "AppCMS/Creation/DelSocialMedia",
							dataType: "json",
							data: JSON.stringify(para)
						}).then(function (res) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							if (res.data.IsSuccess) {
								$scope.SocialMediaList.splice(ind, 1);
							} else {
								Swal.fire(res.data.ResponseMSG);
							}

						}, function (reason) {
							Swal.fire('Failed' + reason);
						});
					}
				});

			} else {
				$scope.SocialMediaList.splice(ind, 1);
            }
			 
		}
	};
	$scope.GetAllSocialMediaList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SocialMediaList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllSocialMedia",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SocialMediaList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetSocialMediaById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			SocialMediaId: refData.SocialMediaId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetSocialMediaById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSocialMedia = res.data.Data;

				document.getElementById('table-listing').style.display = "none";
				document.getElementById('notice-form').style.display = "block";

				$scope.newSocialMedia.Mode = 'Modify';

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};
	$scope.DelSocialMediaById = function (refData) {

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
					SocialMediaId: refData.SocialMediaId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelSocialMedia",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllSocialMediaList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.validateSocialUrl = function (sm) {
		if (!sm.URLPath) {
			sm.isInvalidUrl = false;
			return;
		}
		try {
			// Force protocol if user types without https://
			let url = sm.URLPath;
			if (!/^https?:\/\//i.test(url)) {
				url = 'https://' + url;
			}

			const parsedUrl = new URL(url);
			const domain = parsedUrl.hostname.replace('www.', '');

			sm.isInvalidUrl = !$scope.allowedSocialDomains.some(d =>
				domain === d || domain.endsWith('.' + d)
			);

		} catch (e) {
			sm.isInvalidUrl = true;
		}
	};


	$scope.hasInvalidUrl = function () {
		return $scope.SocialMediaList.some(x => x.isInvalidUrl === true);
	};
});