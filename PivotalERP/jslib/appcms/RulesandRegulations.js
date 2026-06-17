app.controller('RulesRegulationController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Rules and Regulations';

	OnClickDefault();
	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			RulesRegulation: 1

		};

		$scope.searchData = {
			RulesRegulation: ''

		};

		$scope.perPage = {
			RulesRegulation: GlobalServices.getPerPageRow()
		};
				

		$scope.newRulesRegulation = {
			RulesRegulationId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};

		
		$scope.GetAllRulesRegulationList();
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
		document.getElementById('rules-form').style.display = "none";

		// Rules Form
		document.getElementById('add-rules').onclick = function () {
			$scope.ClearRulesRegulation();
			document.getElementById('rules-listing').style.display = "none";
			document.getElementById('rules-form').style.display = "block";

		}
		document.getElementById('back-to-rules-list').onclick = function () {
			$scope.ClearRulesRegulation();
			document.getElementById('rules-listing').style.display = "block";
			document.getElementById('rules-form').style.display = "none";
		}		
	};


	$scope.ClearRulesRegulation = function () {
		$scope.newRulesRegulation = {
			RulesRegulationId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};
		$scope.ClearRulesRegulationPhotoRules();
	}

	$scope.ClearRulesRegulationPhotoRules = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newRulesRegulation.PhotoDataRules = null;
				$scope.newRulesRegulation.PhotoRules_TMP = [];
				$scope.newRulesRegulation.ImagePath = '';
			});

		});
		$('#imgRules').attr('src', '');
		$('#imgRules1').attr('src', '');
	};

	/*Rules And Regulations Tab Js*/
	$scope.IsValidRulesRegulation = function () {

		if ($scope.newRulesRegulation.Title.isEmpty()) {
			Swal.fire('Please ! Enter Heading');
			return false;
		}

		if ($scope.newRulesRegulation.Description.isEmpty()) {
			Swal.fire('Please ! Enter Description');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateRulesRegulation = function () {
		if ($scope.IsValidRulesRegulation() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newRulesRegulation.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateRulesRegulation();
					}
				});
			} else
				$scope.CallSaveUpdateRulesRegulation();

		}
	};

	$scope.CallSaveUpdateRulesRegulation = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newRulesRegulation.PhotoRules_TMP;

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveRulesRegulation",
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

			data: { jsonData: $scope.newRulesRegulation, files: filesColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearRulesRegulation();
				$scope.GetAllRulesRegulationList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetRulesRegulationById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			RulesRegulationId: refData.RulesRegulationId
		};
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetRulesRegulationById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newRulesRegulation = res.data.Data;
				$scope.newRulesRegulation.Mode = 'Modify';

				document.getElementById('rules-listing').style.display = "none";
				document.getElementById('rules-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.GetAllRulesRegulationList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.RulesRegulationList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllRulesRegulationList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.RulesRegulationList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.DelRulesRegulationById = function (refData) {
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
					RulesRegulationId: refData.RulesRegulationId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelRulesRegulation",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllRulesRegulationList();
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