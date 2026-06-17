app.controller('TestimonialController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Testimonial Message';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			Testimonial: 1
		};

		$scope.searchData = {
			Testimonial: ''
		};

		$scope.perPage = {
			Testimonial: GlobalServices.getPerPageRow()

		};



		$scope.newTestimonial = {
			TestimonialId: null,
			MessageFromId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			TestimonialSocialMediaColl: [],
			Mode: 'Save'
		};

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

				angular.forEach($scope.SocialMediaList, function (sm) {
					$scope.newTestimonial.TestimonialSocialMediaColl.push({
						OrderNo: sm.OrderNo,
						Name: sm.Name,
						IconPath: sm.IconPath,
						SocialMediaId: sm.SocialMediaId,
						UrlPath: sm.UrlPath,
						IsActive: true,
					});
				});
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllTestimonialList();
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
	function OnClickDefault() {
		document.getElementById('message-form').style.display = "none";


		// Testimonial Message
		document.getElementById('add-message-form').onclick = function () {
			$scope.ClearTestimonial();
			document.getElementById('message-table-listing').style.display = "none";
			document.getElementById('message-form').style.display = "block";
		}
		document.getElementById('back-to-message-list').onclick = function () {
			$scope.ClearTestimonial();
			document.getElementById('message-table-listing').style.display = "block";
			document.getElementById('message-form').style.display = "none";
		}


	}



	$scope.ClearTestimonial = function () {
		$scope.ClearTestimonialPhotoTestimonial();
		$('input[type=file]').val('');
		$scope.newTestimonial = {
			TestimonialId: null,
			MessageFromId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			TestimonialSocialMediaColl: [],
			Mode: 'Save'
		};
		angular.forEach($scope.SocialMediaList, function (sm) {
			$scope.newTestimonial.TestimonialSocialMediaColl.push({
				OrderNo: sm.OrderNo,
				Name: sm.Name,
				IconPath: sm.IconPath,
				SocialMediaId: sm.SocialMediaId,
				UrlPath: sm.UrlPath,
				IsActive: true,
			});
		});

	}



	$scope.ClearTestimonialPhotoTestimonial = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newTestimonial.PhotoDataTestimonial = null;
				$scope.newTestimonial.PhotoTestimonial_TMP = [];
				$scope.newTestimonial.ImagePath = '';
			});

		});
		$('#imgTestimonial').attr('src', '');
		$('#imgTestimonial1').attr('src', '');
	};





	//************************* Testimonial Message *********************************
	$scope.IsValidTestimonial = function () {
		if ($scope.newTestimonial.Title.isEmpty()) {
			Swal.fire('Please ! Enter Heading');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateTestimonial = function () {
		if ($scope.IsValidTestimonial() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newTestimonial.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateTestimonial();
					}
				});
			} else
				$scope.CallSaveUpdateTestimonial();

		}
	};

	$scope.CallSaveUpdateTestimonial = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newTestimonial.PhotoTestimonial_TMP;

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveTestimonial",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						if (data.files[i].File)
							formData.append("file" + i, data.files[i].File);
						else
							formData.append("file" + i, data.files[i]);
					}
				}

				return formData;
			},
			data: { jsonData: $scope.newTestimonial, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearTestimonial();
				$scope.GetAllTestimonialList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllTestimonialList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TestimonialList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllTestimonialList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TestimonialList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetTestimonialById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TestimonialId: refData.TestimonialId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetTestimonialById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newTestimonial = res.data.Data;
				$scope.newTestimonial.Mode = 'Modify';

				var query = mx($scope.newTestimonial.TestimonialSocialMediaColl);
				var newList = [];
				angular.forEach($scope.SocialMediaList, function (sm) {
					var find = query.firstOrDefault(p1 => p1.SocialMediaId == sm.SocialMediaId);
					newList.push({
						OrderNo: sm.OrderNo,
						Name: sm.Name,
						IconPath: sm.IconPath,
						SocialMediaId: sm.SocialMediaId,
						UrlPath: (find ? find.UrlPath : sm.URLPath),
						IsActive: (find ? find.IsActive : true),
					});
				});

				$scope.newTestimonial.TestimonialSocialMediaColl = newList;

				document.getElementById('message-table-listing').style.display = "none";
				document.getElementById('message-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelTestimonialById = function (refData) {

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
					TestimonialId: refData.TestimonialId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelTestimonial",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllTestimonialList();
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