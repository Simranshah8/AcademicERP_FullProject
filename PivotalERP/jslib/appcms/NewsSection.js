app.controller('NewsSectionController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'NewsSection';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			NewsSection: 1,

		};

		$scope.searchData = {
			NewsSection: '',

		};

		$scope.perPage = {
			NewsSection: GlobalServices.getPerPageRow(),

		};

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
		$scope.newNewsSection = {
			NewsSectionId: 0,
			Headline: '',
			CategoryId: null,
			PublishedDate_TMP: new Date(),
			Description: '',
			Tags: '',
			Photo: '',
			Mode: 'Save'
		};

		$scope.GetAllNewsSectionList();

	}

	function OnClickDefault() {

		document.getElementById('NewsSection-form').style.display = "none";

		//NewsSection section
		document.getElementById('add-NewsSection').onclick = function () {
			document.getElementById('NewsSection-section').style.display = "none";
			document.getElementById('NewsSection-form').style.display = "block";
			$timeout(function () {
				$scope.ClearNewsSection();
			});

		}

		document.getElementById('back-to-list-NewsSection').onclick = function () {
			document.getElementById('NewsSection-form').style.display = "none";
			document.getElementById('NewsSection-section').style.display = "block";
			$timeout(function () {
				$scope.ClearNewsSection();
			});
		}

	}


	$scope.ClearPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newNewsSection.PhotoData = null;
				$scope.newNewsSection.Photo_TMP = [];
				$scope.newNewsSection.Photo = '';
			});

		});
		$('#imgLogo').attr('src', '');
		$('#imgLogo1').attr('src', '');
	};


	$scope.ClearNewsSection = function () {
		$scope.ClearPhoto();
		$scope.newNewsSection= {
			NewsSectionId: 0,
			Headline: '',
			CategoryId: null,
			PublishedDate_TMP: new Date(),
			Description: '',
			Tags: '',
			Photo: '',
			Mode: 'Save'
		};

	}

	//*************************NewsSection *********************************

	$scope.IsValidNewsSection = function () {
		if ($scope.newNewsSection.Headline.isEmpty()) {
			Swal.fire('Please ! Enter Headline');
			return false;
		}
		if ($scope.newNewsSection.CategoryId == null ||0) {
			Swal.fire('Please ! Enter Category');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateNewsSection = function () {
		if ($scope.IsValidNewsSection() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newNewsSection.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateNewsSection();
					}
				});
			} else
				$scope.CallSaveUpdateNewsSection();

		}
	};

	$scope.CallSaveUpdateNewsSection = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var photo = $scope.newNewsSection.Photo_TMP;

		if ($scope.newNewsSection.PublishedDateDet) {
			$scope.newNewsSection.PublishedDate = $filter('date')(new Date($scope.newNewsSection.PublishedDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newNewsSection.PublishedDate = null;

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveNewsSection",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.photo && data.photo.length > 0)
					formData.append("photo", data.photo[0]);

				return formData;
			},
			data: { jsonData: $scope.newNewsSection, photo: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearNewsSection();
				$scope.GetAllNewsSectionList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllNewsSectionList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.NewsSectionList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllNewsSection",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.NewsSectionList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetNewsSectionById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			NewsSectionId: refData.NewsSectionId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetNewsSectionById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newNewsSection = res.data.Data;
				$scope.newNewsSection.Mode = 'Modify';

				if ($scope.newNewsSection.PublishedDate)
					$scope.newNewsSection.PublishedDate_TMP = new Date($scope.newNewsSection.PublishedDate);

				document.getElementById('NewsSection-section').style.display = "none";
				document.getElementById('NewsSection-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelNewsSectionById = function (refData) {

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
					NewsSectionId: refData.NewsSectionId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelNewsSection",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllNewsSectionList();
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