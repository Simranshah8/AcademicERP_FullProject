app.controller('AssetsController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Assets Management';

	OnClickDefault();
	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.GenderList = GlobalServices.getGenderList();
		$('.select2').select2();

		// Initialize filter model used by grouped fetch
		$scope.newFilter = {
			BuildingId: null,
			FloorId: null,
			RoomTypeId: null
		};


		$scope.currentPages = {
			Assets: 1,
		};


		$scope.searchData = {
			Assets: '',
		};

		$scope.perPage = {
			Assets: GlobalServices.getPerPageRow(),
		};



		$scope.RoomTypeColl = [
			{ id: 1, text: 'Class Room' },
			{ id: 2, text: 'Other Room' },
		];

		//$scope.RoomListColl = [
		//	{ id: 1, text: 'Room A' },
		//	{ id: 2, text: 'Room B' },
		//];

		$scope.BuildingList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllBuildingList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BuildingList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//Company Details
		$scope.newCompanyDet = {};
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetCompanyDet",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$scope.newCompanyDet = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.Logo = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllAboutUsList",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.Logo = res.data.Data[0];
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.FloorList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllFloorList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FloorList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.AssetProductsList = [];
		$http({
			method: 'POST',
			url: base_url + "Assets/Creation/GetAllAssetProduct",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AssetProductsList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newAssets = {
			AssetsId: null,
			BuildingId: null,
			FloorId: null,
			RoomTypeId: null,
			AssetsDetailColl: [],
			Mode: 'Save'
		};

		$scope.newAssets.AssetsDetailColl.push({});
		$scope.GetAllAssetsDet();
	}

	function OnClickDefault() {
		document.getElementById('add-BuildingForm-form').style.display = "none";
		document.getElementById('preview-form').style.display = "none";

		document.getElementById('add-BuildingForm').onclick = function () {
			document.getElementById('BuildingForm-section').style.display = "none";
			document.getElementById('add-BuildingForm-form').style.display = "block";
		}
		document.getElementById('BuildingFormback-btn').onclick = function () {
			document.getElementById('add-BuildingForm-form').style.display = "none";
			document.getElementById('BuildingForm-section').style.display = "block";
		}
	}

	$scope.AddDetail = function (index) {
		if ($scope.newAssets.AssetsDetailColl) {
			if ($scope.newAssets.AssetsDetailColl.length > index + 1) {
				$scope.newAssets.AssetsDetailColl.splice(index + 1, 0);
			}
			else
				$scope.newAssets.AssetsDetailColl.push({
					Particular: '',
					Quantity: '',
					Remarks: ''
				});
		}
	};

	$scope.DeleteDetail = function (index) {
		if ($scope.newAssets.AssetsDetailColl) {
			if ($scope.newAssets.AssetsDetailColl.length > 1) {
				$scope.newAssets.AssetsDetailColl.splice(index, 1);
			}
		}
	}

	$scope.ClearAssets = function () {
		$scope.newAssets = {
			AssetsId: null,
			BuildingId: null,
			FloorId: null,
			RoomTypeId: null,
			AssetsDetailColl: [],
			Mode: 'Save'
		};
		$scope.newAssets.AssetsDetailColl.push({});
	}


	$scope.GetAllAssetsList = function () {
		$scope.newAssets.AssetsDetailColl = [];
		if ($scope.newAssets.BuildingId != null && $scope.newAssets.FloorId > 0 && $scope.newAssets.RoomTypeId > 0 && $scope.newAssets.RoomId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				BuildingId: $scope.newAssets.BuildingId,
				FloorId: $scope.newAssets.FloorId,
				RoomTypeId: $scope.newAssets.RoomTypeId,
				RoomId: $scope.newAssets.RoomId

			};
			$http({
				method: 'POST',
				url: base_url + "Assets/Creation/GetAllAssets",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newAssets.AssetsDetailColl = res.data.Data;

					if (!$scope.newAssets.AssetsDetailColl || $scope.newAssets.AssetsDetailColl.length == 0) {
						$scope.newAssets.AssetsDetailColl = [];
						$scope.newAssets.AssetsDetailColl.push({});
					}

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}


	$scope.GetAllfloorwiseRoom = function () {
		$scope.newAssets.RoomListColl = [];
		if ($scope.newAssets.BuildingId != null && $scope.newAssets.FloorId > 0 && $scope.newAssets.RoomTypeId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				BuildingId: $scope.newAssets.BuildingId,
				FloorId: $scope.newAssets.FloorId,
				RoomTypeId: $scope.newAssets.RoomTypeId,
			};
			$http({
				method: 'POST',
				url: base_url + "Assets/Creation/GetAllFllorwiseRoom",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.RoomListColl = res.data.Data;



				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.IsValidAssets = function () {
		//var isInvalid = false;
		//angular.forEach($scope.newAssets.AssetsDetailColl, function (S) {
		//	if (!S.Qty || S.Qty.trim() === '') {
		//		isInvalid = true;
		//		return;
		//	}
		//});

		//if (isInvalid) {
		//	Swal.fire('Please Select Product and Qty for empty rows');
		//	return false;
		//}
		return true;
	}


	$scope.SaveUpdateAssets = function () {
		if ($scope.IsValidAssets() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAssets.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAssets();
					}
				});
			} else
				$scope.CallSaveUpdateAssets();
		}
	};

	$scope.CallSaveUpdateAssets = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var buildingId = $scope.newAssets.BuildingId;
		var floorId = $scope.newAssets.FloorId;
		var roomTypeId = $scope.newAssets.RoomTypeId;
		var roomId = $scope.newAssets.RoomId;
		var dataToSave = [];
		for (var i = 0; i < $scope.newAssets.AssetsDetailColl.length; i++) {
			var S = $scope.newAssets.AssetsDetailColl[i];
			var productId = S.ProductId;
			var qty = S.Qty;
			var remarks = S.Remarks;

			var dataItem = {
				ProductId: productId,
				Qty: qty,
				Remarks: remarks,
				BuildingId: buildingId,
				FloorId: floorId,
				RoomTypeId: roomTypeId,
				RoomId: roomId
			};
			dataToSave.push(dataItem);
		}


		$http({
			method: 'POST',
			url: base_url + "Assets/Creation/SaveUpdateAssets",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: dataToSave }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearAssets();
				$scope.GetAllAssetsDet();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}


	$scope.GetAllAssetsDet = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllAssetsList = [];
		var para = {
			BuildingId: $scope.newFilter && $scope.newFilter.BuildingId,
			FloorId: $scope.newFilter && $scope.newFilter.FloorId,
			RoomTypeId: $scope.newFilter && $scope.newFilter.RoomTypeId,
		};
		$http({
			method: 'POST',
			url: base_url + "Assets/Creation/GetAllAssets",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				const rawData = res.data.Data;

				const groupedMap = rawData.reduce((acc, item) => {
					const match = acc.find(x =>
						x.BuildingName === item.BuildingName &&
						x.FloorName === item.FloorName &&
						x.RoomTypeId === item.RoomTypeId &&
						x.RoomName === item.RoomName
					);

					if (match) {
						match.Qty += parseInt(item.Qty) || 0;
					} else {
						acc.push({
							...item,
							Qty: parseInt(item.Qty) || 0
						});
					}

					return acc;
				}, []);

				$scope.AllAssetsList = groupedMap;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};



	//$scope.GetAllAssetsDet = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	$scope.AllAssetsList = [];
	//	$http({
	//		method: 'Post',
	//		url: base_url + "Assets/Creation/GetAllAssets",
	//		dataType: "json"
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";


	//		if (res.data.IsSuccess && res.data.Data) {
	//			$scope.AllAssetsList = res.data.Data;

	//		} else {
	//			Swal.fire(res.data.ResponseMSG);
	//		}
	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//}



	$scope.newDet = {};
	$scope.GetAssetsDetaillist = function (cl) {
		$scope.newDet.BuildingName = cl.BuildingName;
		$scope.newDet.FloorName = cl.FloorName;
		$scope.newDet.RoomTypeId = cl.RoomTypeId;
		$scope.newDet.RoomName = cl.RoomName;
		$scope.AllAssetsDetList = [];
		if (cl.BuildingId != null && cl.FloorId > 0 && cl.RoomTypeId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				BuildingId: cl.BuildingId,
				FloorId: cl.FloorId,
				RoomTypeId: cl.RoomTypeId,
				RoomId: cl.RoomId
			};
			$http({
				method: 'POST',
				url: base_url + "Assets/Creation/GetAllAssets",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.AllAssetsDetList = res.data.Data;
					$('#assetsdetailmodal').modal('show');


				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}


	$scope.newPrint = {};
	$scope.PrintAssetsList = function (cl) {
		$scope.newPrint.BuildingName = cl.BuildingName;
		$scope.newPrint.FloorName = cl.FloorName;
		$scope.newPrint.RoomTypeId = cl.RoomTypeId;
		$scope.newPrint.RoomName = cl.RoomName;
		$scope.AllAssetsDetListP = [];
		if (cl.BuildingId != null && cl.FloorId > 0 && cl.RoomTypeId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				BuildingId: cl.BuildingId,
				FloorId: cl.FloorId,
				RoomTypeId: cl.RoomTypeId,
				RoomId: cl.RoomId
			};
			$http({
				method: 'POST',
				url: base_url + "Assets/Creation/GetAllAssets",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.AllAssetsDetListP = res.data.Data;
					$('#printcard').printThis();


				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});