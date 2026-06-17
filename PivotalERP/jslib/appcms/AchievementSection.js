app.controller('AchievementSectionController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'AchievementSection';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			AchievementSection: 1,

		};

		$scope.searchData = {
			AchievementSection: '',

		};

		$scope.perPage = {
			AchievementSection: GlobalServices.getPerPageRow(),

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

		$scope.newAchievementSection = {
			AchievementSectionId: 0,
			Headline: '',
			CategoryId: null,
			AchievementDate_TMP: new Date(),
			Description: '',
			Tags: '',
			Photo: '',
			Mode: 'Save'
		};

		$scope.GetAllAchievementSectionList();

	}

	function OnClickDefault() {

		document.getElementById('AchievementSection-form').style.display = "none";

		//AchievementSection section
		document.getElementById('add-AchievementSection').onclick = function () {
			document.getElementById('AchievementSection-section').style.display = "none";
			document.getElementById('AchievementSection-form').style.display = "block";
			$timeout(function () {
				$scope.ClearAchievementSection();
			});

		}

		document.getElementById('back-to-list-AchievementSection').onclick = function () {
			document.getElementById('AchievementSection-form').style.display = "none";
			document.getElementById('AchievementSection-section').style.display = "block";
			$timeout(function () {
				$scope.ClearAchievementSection();
			});
		}

	}


	$scope.ClearPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newAchievementSection.PhotoData = null;
				$scope.newAchievementSection.Photo_TMP = [];
				$scope.newAchievementSection.Photo = '';
			});

		});
		$('#imgLogo').attr('src', '');
		$('#imgLogo1').attr('src', '');
	};


	$scope.ClearAchievementSection = function () {
		$scope.ClearPhoto();
		$scope.newAchievementSection = {
			AchievementSectionId: 0,
			Headline: '',
			CategoryId: null,
			AchievementDate_TMP: new Date(),
			Description: '',
			Tags: '',
			Photo: '',
			Mode: 'Save'
		};

	}

	//*************************AchievementSection *********************************

	$scope.IsValidAchievementSection = function () {
		if ($scope.newAchievementSection.Headline.isEmpty()) {
			Swal.fire('Please ! Enter Headline');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateAchievementSection = function () {
		if ($scope.IsValidAchievementSection() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAchievementSection.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAchievementSection();
					}
				});
			} else
				$scope.CallSaveUpdateAchievementSection();

		}
	};

	$scope.CallSaveUpdateAchievementSection = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var photo = $scope.newAchievementSection.Photo_TMP;

		if ($scope.newAchievementSection.AchievementDateDet) {
			$scope.newAchievementSection.AchievementDate = $filter('date')(new Date($scope.newAchievementSection.AchievementDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newAchievementSection.AchievementDate = null;

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveAchievementSection",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.photo && data.photo.length > 0)
					formData.append("photo", data.photo[0]);

				return formData;
			},
			data: { jsonData: $scope.newAchievementSection, photo: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAchievementSection();
				$scope.GetAllAchievementSectionList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllAchievementSectionList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AchievementSectionList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllAchievementSection",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AchievementSectionList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAchievementSectionById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AchievementSectionId: refData.AchievementSectionId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAchievementSectionById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAchievementSection = res.data.Data;
				$scope.newAchievementSection.Mode = 'Modify';

				if ($scope.newAchievementSection.AchievementDate)
					$scope.newAchievementSection.AchievementDate_TMP = new Date($scope.newAchievementSection.AchievementDate);

				document.getElementById('AchievementSection-section').style.display = "none";
				document.getElementById('AchievementSection-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAchievementSectionById = function (refData) {

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
					AchievementSectionId: refData.AchievementSectionId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelAchievementSection",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAchievementSectionList();
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