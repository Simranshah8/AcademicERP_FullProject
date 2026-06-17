app.controller('HealthCampaignController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'HealthCampaign';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.LanguageColl = GlobalServices.getLangList();

		$scope.currentPages = {
			HealthCampaign: 1,

		};

		$scope.searchData = {
			HealthCampaign: '',

		};

		$scope.perPage = {
			HealthCampaign: GlobalServices.getPerPageRow(),

		}; 
		$scope.HealthCampaign = {		 
			TranId: 0,
			CampaignName: '',
			ForDate_TMP: new Date(),
			OrganizedBy: '',
			Description: '',
			HealthCampaignColl: [],
			Mode: 'Save'
		};
		$scope.HealthCampaign.HealthCampaignColl.push({});

		//$scope.GetAllHealthCampaign();

	};


	$scope.ClearHealthCampaign = function () {
		$scope.HealthCampaign = {
			TranId: 0,
			CampaignName: '',
			ForDate_TMP: new Date(),
			OrganizedBy: '',
			Description: '',
			HealthCampaignColl: [],
			Mode: 'Save'
		};
		$scope.HealthCampaign.HealthCampaignColl.push({});
	};

	$scope.AddHealthCampaign = function (ind) {
		if ($scope.HealthCampaign.HealthCampaignColl) {
			if ($scope.HealthCampaign.HealthCampaignColl.length > ind + 1) {
				$scope.HealthCampaign.HealthCampaignColl.splice(ind + 1, 0, {
					Organization: ''
				})
			} else {
				$scope.HealthCampaign.HealthCampaignColl.push({
					Organization: ''
				})
			}
		}
	};

	$scope.delHealthCampaign = function (ind) {
		if ($scope.HealthCampaign.HealthCampaignColl) {
			if ($scope.HealthCampaign.HealthCampaignColl.length > 1) {
				$scope.HealthCampaign.HealthCampaignColl.splice(ind, 1);
			}
		}
	};



	function OnClickDefault() {
		document.getElementById('healthcampaignform').style.display = "none";

		document.getElementById('add-healthcampaign').onclick = function () {
			document.getElementById('campaigntable').style.display = "none";
			document.getElementById('healthcampaignform').style.display = "block";
			$scope.ClearHealthCampaign();
		}
		document.getElementById('backtolist').onclick = function () {
			document.getElementById('campaigntable').style.display = "block";
			document.getElementById('healthcampaignform').style.display = "none";
			$scope.ClearHealthCampaign();
		}

	};

	//************************* Class *********************************

	$scope.IsValidHealthCampaign = function () {
		if ($scope.HealthCampaign.CampaignName.isEmpty()) {
			Swal.fire('Please ! Enter Campaign Name');
			return false;
		}
		return true;
	};

	$scope.AddHealth = function () {
		if ($scope.IsValidHealthCampaign() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.HealthCampaign.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateHealthCampaign();
					}
				});
			} else
				$scope.CallSaveUpdateHealthCampaign();

		}
	};

	$scope.CallSaveUpdateHealthCampaign = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.HealthCampaign.ForDateDet) {
			$scope.HealthCampaign.ForDate = $filter('date')(new Date($scope.HealthCampaign.ForDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.HealthCampaign.ForDate = $filter('date')(new Date(), 'yyyy-MM-dd');
		 
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveHealthCampaign",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.HealthCampaign }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearHealthCampaign();
			    $scope.GetAllHealthCampaign();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllHealthCampaign = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CampaignColl = [];
		$http({
			method: 'GET',
			url: base_url + "Academic/Transaction/GetAllHealthCampaign",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CampaignColl = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.deleteHealthCampaign = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.CampaignName + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/DeleteHealthCampaign",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.CampaignColl.splice(ind, 1);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

	$scope.getHealthCampaignById = function (beData) {
		$scope.loadingstatus = "running";
		var para = {
			TranId: beData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/getHealthCampaignById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$timeout(function () {
					$scope.HealthCampaign = res.data.Data;
					$scope.HealthCampaign.Mode = 'Modify';

					$scope.HealthCampaign.ForDate_TMP = new Date($scope.HealthCampaign.ForDate);
					document.getElementById('campaigntable').style.display = "none";
					document.getElementById('healthcampaignform').style.display = "block";
				});


			} else
				Swal.fire(res.data.ResponseMSG);


		}, function (reason) {
			alert('Failed' + reason);
		});
	}

});

