app.controller('UtilitiesController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Utilities';

	OnClickDefault();
	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			Utilities: 1,
			Facilities:1,
		};


		$scope.searchData = {
			Utilities: '',
			Facilities: '',
		};

		$scope.perPage = {
			Utilities: GlobalServices.getPerPageRow(),
			Facilities: GlobalServices.getPerPageRow(),
		};


		$scope.newUtilities = {
			UtilitiesId: null,
			Name: '',
			OrderNo: 0,
			Description: '',
			SubUtilitiesColl:[]
		}
		$scope.newUtilities.SubUtilitiesColl.push({});


		$scope.newFacilities = {
			FacilitiesId: null,
			Name: '',
			OrderNo: 0,
			Description: '',
			SubFacilitiesColl:[]
		}
		$scope.newFacilities.SubFacilitiesColl.push({});

		$scope.GetAllUtilitiesList();
		$scope.GetAllFacilitiesList();
	}

	$scope.AddSubUtilities = function (ind) {
		if ($scope.newUtilities.SubUtilitiesColl) {
			if ($scope.newUtilities.SubUtilitiesColl.length > ind + 1) {
				$scope.newUtilities.SubUtilitiesColl.splice(ind + 1, 0, {
					Name: ''
				})
			} else {
				$scope.newUtilities.SubUtilitiesColl.push({
					Name: ''
				})
			}
		}
	};

	$scope.DeleteSubUtilities = function (ind) {
		if ($scope.newUtilities.SubUtilitiesColl) {
			if ($scope.newUtilities.SubUtilitiesColl.length > 1) {
				$scope.newUtilities.SubUtilitiesColl.splice(ind, 1);
			}
		}
	};

	$scope.AddSubFacilities = function (ind) {
		if ($scope.newFacilities.SubFacilitiesColl) {
			if ($scope.newFacilities.SubFacilitiesColl.length > ind + 1) {
				$scope.newFacilities.SubFacilitiesColl.splice(ind + 1, 0, {
					Name: ''
				})
			} else {
				$scope.newFacilities.SubFacilitiesColl.push({
					Name: ''
				})
			}
		}
	};
	$scope.DeleteSubFacilities = function (ind) {
		if ($scope.newFacilities.SubFacilitiesColl) {
			if ($scope.newFacilities.SubFacilitiesColl.length > 1) {
				$scope.newFacilities.SubFacilitiesColl.splice(ind, 1);
			}
		}
	};


	function OnClickDefault() {
		document.getElementById('add-Utilities-form').style.display = "none";
		document.getElementById('add-facilities-form').style.display = "none";

		document.getElementById('add-Utilities').onclick = function () {
			document.getElementById('Utilities-table').style.display = "none";
			document.getElementById('add-Utilities-form').style.display = "block";
			$scope.ClearUtilities();
		}
		document.getElementById('deviceback-btn').onclick = function () {
			document.getElementById('Utilities-table').style.display = "block";
			document.getElementById('add-Utilities-form').style.display = "none";
		}

		document.getElementById('add-facilities').onclick = function () {
			document.getElementById('facilities-table').style.display = "none";
			document.getElementById('add-facilities-form').style.display = "block";
			$scope.ClearFacilities();
		}
		document.getElementById('facilities-back-btn').onclick = function () {
			document.getElementById('facilities-table').style.display = "block";
			document.getElementById('add-facilities-form').style.display = "none";
		}
	}


	$scope.ClearUtilities = function () {
		$scope.newUtilities = {
			UtilitiesId: null,
			Name: '',
			OrderNo: 0,
			Description: '',
			SubUtilitiesColl: [],
			Mode: 'Save'
		};
		$scope.newUtilities.SubUtilitiesColl.push({});
	}

	$scope.ClearFacilities = function () {
		$scope.newFacilities = {
			FacilitiesId: null,
			Name: '',
			OrderNo: 0,
			Description: '',
			SubFacilitiesColl: []
		};
		$scope.newFacilities.SubFacilitiesColl.push({});
	}

//--------------------------------Utilities------------------------------------------------------
	$scope.IsValidUtilities = function () {
		if ($scope.newUtilities.Name.isEmpty()) {
			Swal.fire('Please ! Enter Utilities Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateUtilities = function () {
		if ($scope.IsValidUtilities() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newUtilities.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateUtilities();
					}
				});
			} else
				$scope.CallSaveUpdateUtilities();
		}
	};

	$scope.CallSaveUpdateUtilities = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		angular.forEach($scope.newUtilities.SubUtilitiesColl, function (sub, index) {
			sub.SNo = index + 1; // Add SNo property
		});
		$http({
			method: 'POST',
			url: base_url + "Infrastructure/Setup/SaveUpdateUtilities",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newUtilities }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearUtilities();
				$scope.GetAllUtilitiesList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllUtilitiesList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.UtilitiesList = [];
		$http({
			method: 'POST',
			url: base_url + "Infrastructure/Setup/GetAllUtilities",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UtilitiesList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetUtilitiesById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			UtilitiesId: refData.UtilitiesId
		};

		$scope.newUtilities = null;
		$http({
			method: 'POST',
			url: base_url + "Infrastructure/Setup/getUtilitiesById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$timeout(function () {
					$scope.$apply(function () {
						$scope.newUtilities = res.data.Data;
						$scope.newUtilities.Mode = 'Modify';
					

						if (!$scope.newUtilities.SubUtilitiesColl || $scope.newUtilities.SubUtilitiesColl.length == 0) {
							$scope.newUtilities.SubUtilitiesColl = [];
							$scope.newUtilities.SubUtilitiesColl.push({});
						}

					
					});
				});

				document.getElementById('Utilities-table').style.display = "none";
				document.getElementById('add-Utilities-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelUtilitiesById = function (refData) {
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
					UtilitiesId: refData.UtilitiesId
				};

				$http({
					method: 'POST',
					url: base_url + "Infrastructure/Setup/DeleteUtilitiesById",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllUtilitiesList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};



	//--------------------------------Facilities------------------------------------------------------
	$scope.IsValidFacilities = function () {
		if ($scope.newFacilities.Name.isEmpty()) {
			Swal.fire('Please ! Enter Facilities Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateFacilities = function () {
		if ($scope.IsValidFacilities() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newFacilities.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateFacilities();
					}
				});
			} else
				$scope.CallSaveUpdateFacilities();
		}
	};

	$scope.CallSaveUpdateFacilities = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		angular.forEach($scope.newFacilities.SubFacilitiesColl, function (sub, index) {
			sub.SNo = index + 1; // Add SNo property
		});

		$http({
			method: 'POST',
			url: base_url + "Infrastructure/Setup/SaveUpdateFacilities",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newFacilities }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearFacilities();
				$scope.GetAllFacilitiesList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllFacilitiesList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FacilitiesList = [];
		$http({
			method: 'POST',
			url: base_url + "Infrastructure/Setup/GetAllFacilities",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FacilitiesList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetFacilitiesById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			FacilitiesId: refData.FacilitiesId
		};

		$scope.newFacilities = null;
		$http({
			method: 'POST',
			url: base_url + "Infrastructure/Setup/getFacilitiesById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$timeout(function () {
					$scope.$apply(function () {
						$scope.newFacilities = res.data.Data;
						$scope.newFacilities.Mode = 'Modify';
						if (!$scope.newFacilities.SubFacilitiesColl || $scope.newFacilities.SubFacilitiesColl.length == 0) {
							$scope.newFacilities.SubFacilitiesColl = [];
							$scope.newFacilities.SubFacilitiesColl.push({});
						}
					});
				});

				document.getElementById('facilities-table').style.display = "none";
				document.getElementById('add-facilities-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelFacilitiesById = function (refData) {
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
					FacilitiesId: refData.FacilitiesId
				};

				$http({
					method: 'POST',
					url: base_url + "Infrastructure/Setup/DeleteFacilitiesById",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFacilitiesList();
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
	}

});