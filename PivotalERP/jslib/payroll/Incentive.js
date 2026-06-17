app.controller('IncentiveController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Incentive';
	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		$scope.currentPages = {
			Incentive: 1,
			IncentiveType: 1,
			Brand:1
		};

		$scope.searchData = {
			Incentive: '',
			IncentiveType: '',
			Brand: ''
		};

		$scope.perPage = {
			Incentive: GlobalServices.getPerPageRow(),
			IncentiveType: GlobalServices.getPerPageRow(),
			Brand: GlobalServices.getPerPageRow()
		};

		$scope.newIncentive = {
			IncentiveId: null,
			Date_TMP: '',
			BrandId: null,
			IncentiveTypeId: null,
			EmployeeDetailsColl: [],
			Remarks: '',
			Mode: 'Save'
		};
		$scope.newIncentive.EmployeeDetailsColl.push({});

		$scope.newIncentiveType = {
			IncentiveTypeId: null,
			Name: '',
			Code: '',
			SNo: '',
			PayHeadingId: null,
			Description: '',
			Mode: 'Save'
		};

		$scope.newBrand = {
			BrandId: null,
			Name: '',
			Code: '',
			SNo: '',
			Description: '',
			Mode: 'Save'
		};

		//$scope.GetAllIncentiveList();
		//$scope.GetAllIncentiveTypeList();
		//$scope.GetAllBrandList();
	}

	$scope.ClearIncentive = function () {
		$scope.newIncentive = {
			IncentiveId: null,
			Date_TMP: '',
			BrandId: null,
			IncentiveTypeId: null,
			EmployeeDetailsColl: [],
			Remarks: '',
			Mode: 'Save'
		};
		$scope.newIncentive.EmployeeDetailsColl.push({});
	}

	$scope.ClearIncentiveType = function () {
		$scope.newIncentiveType = {
			IncentiveTypeId: null,
			Name: '',
			Code: '',
			SNo: '',
			PayHeadingId: null,
			Description: '',
			Mode: 'Save'
		};
	}

	$scope.ClearBrand = function () {
		$scope.newBrand = {
			BrandId: null,
			Name: '',
			Code: '',
			SNo: '',
			Description: '',
			Mode: 'Save'
		};
	}

	function OnClickDefault() {
		document.getElementById('Incentive-form').style.display = "none";
		document.getElementById('IncentiveType-form').style.display = "none";
		document.getElementById('Brand-form').style.display = "none";

		document.getElementById('add-Incentive').onclick = function () {
			document.getElementById('Incentivelist').style.display = "none";
			document.getElementById('Incentive-form').style.display = "block";
			$scope.ClearVehicleDetails();
		}

		document.getElementById('Incentiveback-btn').onclick = function () {
			document.getElementById('Incentive-form').style.display = "none";
			document.getElementById('Incentivelist').style.display = "block";
			$scope.ClearVehicleDetails();
		}

		document.getElementById('add-IncentiveType').onclick = function () {
			document.getElementById('IncentiveType-section').style.display = "none";
			document.getElementById('IncentiveType-form').style.display = "block";
			$scope.ClearVehicleDetails();
		}

		document.getElementById('IncentiveTypeback-btn').onclick = function () {
			document.getElementById('IncentiveType-form').style.display = "none";
			document.getElementById('IncentiveType-section').style.display = "block";
			$scope.ClearVehicleDetails();
		}
		document.getElementById('add-Brand').onclick = function () {
			document.getElementById('Brand-section').style.display = "none";
			document.getElementById('Brand-form').style.display = "block";
			$scope.ClearVehicleDetails();
		}

		document.getElementById('Brandback-btn').onclick = function () {
			document.getElementById('Brand-form').style.display = "none";
			document.getElementById('Brand-section').style.display = "block";
			$scope.ClearVehicleDetails();
		}


	};


	//*************************Incentive*********************************

	$scope.IsValidIncentive = function () {
		if ($scope.newIncentive.Date_TMP.isEmpty()) {
			Swal.fire('Please ! Enter Date');
			return false;
		}
		return true;
	}


	$scope.AddIncentiveDetails = function (ind) {
		if ($scope.newIncentive.EmployeeDetailsColl) {
			if ($scope.newIncentive.EmployeeDetailsColl.length > ind + 1) {
				$scope.newIncentive.EmployeeDetailsColl.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newIncentive.EmployeeDetailsColl.push({
					ClassName: ''
				})
			}
		}
	};
	$scope.delIncentiveDetails = function (ind) {
		if ($scope.newIncentive.EmployeeDetailsColl) {
			if ($scope.newIncentive.EmployeeDetailsColl.length > 1) {
				$scope.newIncentive.EmployeeDetailsColl.splice(ind, 1);
			}
		}
	};



	$scope.SaveUpdateIncentive = function () {
		if ($scope.IsValidIncentive() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newIncentive.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateIncentive();
					}
				});
			} else
				$scope.CallSaveUpdateIncentive();
		}
	};

	$scope.CallSaveUpdateIncentive = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/SaveIncentive",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newSetup }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearSetup();
				$scope.GetAllIncentiveList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllIncentiveList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.IncentiveList = [];

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetAllIncentiveList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.IncentiveList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetIncentiveById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			IncentiveId: refData.IncentiveId
		};

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetIncentiveById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newIncentive = res.data.Data;
				$scope.newIncentive.Mode = 'Modify';
				document.getElementById('Incentivelist').style.display = "none";
				document.getElementById('Incentive-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelIncentiveById = function (refData) {
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
					IncentiveId: refData.IncentiveId
				};

				$http({
					method: 'POST',
					url: base_url + "HRM/Creation/DelIncentive",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllIncentiveList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* IncentiveType*********************************

	$scope.IsValidIncentiveType = function () {
		if ($scope.newIncentiveType.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateIncentiveType = function () {
		if ($scope.IsValidIncentiveType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newIncentiveType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateIncentiveType();
					}
				});
			} else
				$scope.CallSaveUpdateIncentiveType();

		}
	};

	$scope.CallSaveUpdateIncentiveType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/SaveIncentiveType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newIncentiveType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearIncentiveType();
				$scope.GetAllIncentiveTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllIncentiveTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.IncentiveTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetAllIncentiveTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.IncentiveTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetIncentiveTypeById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			IncentiveTypeId: refData.IncentiveTypeId
		};
		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetIncentiveTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newIncentiveType = res.data.Data;
				$scope.newIncentiveType.Mode = 'Modify';

				document.getElementById('IncentiveType-section').style.display = "none";
				document.getElementById('IncentiveType-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelIncentiveTypeById = function (refData) {
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
					IncentiveTypeId: refData.IncentiveTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "HRM/Creation/DelIncentiveType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllIncentiveTypeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};




	//*************************Brand *********************************

	$scope.IsValidBrand = function () {
		if ($scope.newBrand.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateBrand = function () {
		if ($scope.IsValidBrand() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBrand.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBrand();
					}
				});
			} else
				$scope.CallSaveUpdateBrand();

		}
	};

	$scope.CallSaveUpdateBrand = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/SaveBrand",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newBrand }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBrand();
				$scope.GetAllBrandList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllBrandList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BrandList = [];

		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetAllBrandList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BrandList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetBrandById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			BrandId: refData.BrandId
		};
		$http({
			method: 'POST',
			url: base_url + "HRM/Creation/GetBrandById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBrand = res.data.Data;
				$scope.newBrand.Mode = 'Modify';

				document.getElementById('Brand-section').style.display = "none";
				document.getElementById('Brand-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBrandById = function (refData) {
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
					BrandId: refData.BrandId
				};

				$http({
					method: 'POST',
					url: base_url + "HRM/Creation/DelBrand",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBrandList();
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