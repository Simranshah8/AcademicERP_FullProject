app.controller('GrievanceRedressalTeamController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			GrievanceRedressal: 1,
		};

		$scope.searchData = {
			GrievanceRedressal: '',
		};

		$scope.perPage = {
			GrievanceRedressal: GlobalServices.getPerPageRow(),
		};

		$scope.sortKeys = {
			GrievanceRedressal: '',

		};

		$scope.reverses = {
			GrievanceRedressal: false,

		}
		//clear photo
		$scope.ClearEmpPhoto = function () {

			$timeout(function () {

				$scope.$apply(function () {

					$scope.addNewDetails.PhotoData = null;

					$scope.addNewDetails.Photo_TMP = [];
					//to show img when click on eye icon
					$scope.addNewDetails.Image = '';

				});

			});

			$('#imgEmp').attr('src', '');

			$('#imgPhoto1').attr('src', '');

		};

		$scope.addNewDetails = {
			Name: '',
			Designation: '',
			Qualification: '',
			Contact: '',
			Email: '',
			Department: '',
			OrderNo: 0,
			Mode: 'Save'
		};
		$scope.GetAllGrievanceRedressalTeam();

	}
	//to show img when click on eye icon
	$scope.ShowPersonalImg = function (item) {
		$scope.viewImg = {
			ContentPath: ''
		};
		if (item.Image && item.Image.length > 0) {
			$scope.viewImg.ContentPath = item.Image;
			$('#PersonalImg').modal('show');
		} else
			Swal.fire('No Image Found');

	};
	$scope.resetGrievanceRedressalTeam = function () {
		$scope.addNewDetails = {
			Name: '',
			Designation: '',
			Qualification: '',
			Contact: '',
			Email: '',
			Department: '',
			OrderNo: 0,
			Mode: 'Save'
		};
		//clear photo
		$scope.ClearEmpPhoto();

	}
	function OnClickDefault() {
		document.getElementById('GrievanceRedressalTeam-form').style.display = "none";

		document.getElementById('add-Details').onclick = function () {
			document.getElementById('GrievanceRedressalTeam-section').style.display = "none";
			document.getElementById('GrievanceRedressalTeam-form').style.display = "block";
		}

		document.getElementById('back-Details').onclick = function () {
			document.getElementById('GrievanceRedressalTeam-section').style.display = "block";
			document.getElementById('GrievanceRedressalTeam-form').style.display = "none";
		}
	}

	$scope.IsValidGrievanceRedressalTeam = function () {
		//if ($scope.addNewDetails.First_Name.isEmpty()) {
		//	Swal.fire('Please ! Enter First Name');
		//	return false;
		//}
		return true;
	}

	$scope.SaveUpdateGrievanceRedressalTeam = function () {
		if ($scope.IsValidGrievanceRedressalTeam() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.addNewDetails.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateGrievanceRedressalTeam();
					}
				});
			} else
				$scope.CallSaveUpdateGrievanceRedressalTeam();

		}
	};

	$scope.CallSaveUpdateGrievanceRedressalTeam = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		//To save single photo
		var photo = $scope.addNewDetails.Photo_TMP;

		$http({
			method: 'Post',
			url: base_url + "AppCms/Creation/SaveGrievanceRedressalTeam",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				//To  save single photo
				if (data.emPhoto && data.emPhoto.length > 0)
					formData.append("photo", data.emPhoto[0]);

				return formData;
			},

			data: { jsonData: $scope.addNewDetails, emPhoto: photo }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.resetGrievanceRedressalTeam();
				$scope.GetAllGrievanceRedressalTeam();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllGrievanceRedressalTeam = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.GrievanceRedressalList = [];
		$http({
			method: 'POST',
			url: base_url + "AppCms/Creation/GetAllGrievanceRedressalTeam",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.GrievanceRedressalList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetGrievanceRedressalTeamById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			GrievanceRedressalId: refData.GrievanceRedressalId
		};
		$http({
			method: 'POST',
			url: base_url + "AppCms/Creation/GetGrievanceRedressalTeamById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.addNewDetails = res.data.Data;
				$scope.addNewDetails.Mode = 'Modify';

				document.getElementById('GrievanceRedressalTeam-section').style.display = "none";
				document.getElementById('GrievanceRedressalTeam-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelGrievanceRedressalTeam = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { GrievanceRedressalId: refData.GrievanceRedressalId };
				$http({
					method: 'POST',
					url: base_url + "AppCms/Creation/DelGrievanceRedressalTeam",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GrievanceRedressalList.splice(ind, 1);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	$scope.sortGrievanceRedressal = function (keyname) {
		$scope.sortKeys.GrievanceRedressal = keyname;
		$scope.reverses.GrievanceRedressal = !$scope.reverses.GrievanceRedressal;
	}
});



