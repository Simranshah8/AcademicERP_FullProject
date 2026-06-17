app.controller('NepaliMonthController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Month';
	 
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

		$scope.newSocialMedia = {
			NM: 0,
			Name: '',
			SNo: 0
		};

		$scope.GetAllNepaliMonthList();

	}
    

	$scope.IsValidSocialMedia = function () {
		//if ($scope.newSocialMedia.Title.isEmpty()) {
		//	Swal.fire('Please ! Enter Title');
		//	return false;
		//}

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
						$scope.CallSaveUpdateMonth();
					}
				});
			} else
				$scope.CallSaveUpdateMonth();

		}
	};

	$scope.CallSaveUpdateMonth = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

	 
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveNepaliMonth",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
 
				return formData;
			},
			data: { jsonData: $scope.NepaliMonthList}
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
		$scope.NepaliMonthList.push({
			NM: 0,
			Name: '',
			SNo: 0
		});
    }
	$scope.delURL = function (ind) {
		if ($scope.NepaliMonthList) {

			var refData = $scope.NepaliMonthList[ind];

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
							NM: refData.NM
						};

						$http({
							method: 'POST',
							url: base_url + "AppCMS/Creation/DelNepaliMonth",
							dataType: "json",
							data: JSON.stringify(para)
						}).then(function (res) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							if (res.data.IsSuccess) {
								$scope.NepaliMonthList.splice(ind, 1);
							} else {
								Swal.fire(res.data.ResponseMSG);
							}

						}, function (reason) {
							Swal.fire('Failed' + reason);
						});
					}
				});

			} else {
				$scope.NepaliMonthList.splice(ind, 1);
            }
			 
		}
	};
	$scope.GetAllNepaliMonthList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.NepaliMonthList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetNepaliMonth",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.NepaliMonthList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	 
	$scope.DelMonthById = function (refData) {

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
					url: base_url + "AppCMS/Creation/DelNepaliMonth",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllNepaliMonthList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



});