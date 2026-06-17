app.controller('FeeItemController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Fee Item';
	OnClickDefault();
	$scope.LoadData = function () {

		$scope.HeadForList = GlobalServices.getFeeHeadForList();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.MonthList = [];
		$scope.MonthDetailsColl = [];
		GlobalServices.getMonthListFromDB().then(function (res1)
		{
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});

			angular.forEach($scope.MonthList, function (mn) {
				$scope.MonthDetailsColl.push({
					id: mn.id,
					text: mn.text,
					IsChecked: false
				});
			});
			
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.currentPages = {
			FeeItem: 1,
			FeeItemGroup: 1,

		};

		$scope.searchData = {
			FeeItem: '',
			FeeItemGroup: '',

		};

		$scope.perPage = {
			FeeItem: GlobalServices.getPerPageRow(),
			FeeItemGroup: GlobalServices.getPerPageRow(),

		};

		$scope.newFeeItem = {
			FeeItemId: null,
			Name: '',
			HeadFor: 1,
			ApplyTax: false,
			NotApplicableForHostel: false,
			NotApplicableForTransport: false,
			IsExtraFee: false,
			Mode: 'Save'
		};


		$scope.newFeeItemGroup = {
			FeeItemGroupId: null,
			Name: '',
			FeeItemIdColl: [],
			FeeItemList:[],
			Mode: 'Save'
		};

		$scope.GetAllFeeItemList();
		$scope.GetAllFeeItemGroupList();


	}

	$scope.GetAutoOrderNo = function (be) {
		var para = {
			For: be
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetClassSetupAutoOrderNo",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				var vDet = res.data.Data;
				if (be == 19) { //Fee Item
					$scope.newFeeItem.OrderNo = vDet.RId;
				}
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	function OnClickDefault() {
		document.getElementById('fee-item-form').style.display = "none";
		document.getElementById('fee-item-group-form').style.display = "none";

		// Fee Item
		document.getElementById('add-fee-item').onclick = function () {
		document.getElementById('fee-tem-section').style.display = "none";
			document.getElementById('fee-item-form').style.display = "block";
			$scope.ClearFeeItem();
			$scope.GetAutoOrderNo(19);
		}
		document.getElementById('feeitem-back-btn').onclick = function () {
		document.getElementById('fee-item-form').style.display = "none";
			document.getElementById('fee-tem-section').style.display = "block";
			$scope.ClearFeeItem();
		}

		// Fee Item Group
		document.getElementById('add-fee-item-group').onclick = function ()
		{
			$scope.ClearFeeItemGroup();
		document.getElementById('fee-item-group-section').style.display = "none";
			document.getElementById('fee-item-group-form').style.display = "block";
			
		}
		document.getElementById('fee-item-group-back-btn').onclick = function () {
		document.getElementById('fee-item-group-form').style.display = "none";
			document.getElementById('fee-item-group-section').style.display = "block";
			$scope.ClearFeeItemGroup();
		}

	}
	$scope.CheckAllMonth = function () {
		angular.forEach($scope.MonthDetailsColl, function (md) {
			md.IsChecked = $scope.newFeeItem.AllMonth;
		});
	};

	$scope.ClearFeeItem = function () {
		$scope.newFeeItem = {
			FeeItemId: null,			
			Name: '',
			HeadFor: 1,
			ApplyTax: false,
			IsExtraFee: false,
			Mode: 'Save'
		};

		angular.forEach($scope.MonthDetailsColl, function (md) {
			md.IsChecked = false;
		});

	}
	$scope.ClearFeeItemGroup = function () {
		$scope.newFeeItemGroup = {
			FeeItemGroupId: null,
			Name: '',
			FeeItemIdColl: [],
			FeeItemList: [],
			CheckAll:false,
			Mode: 'Save'
		};

		$timeout(function () {
			angular.forEach($scope.FeeItemList, function (fi)
			{
				fi.IsChecked = false;
				$scope.newFeeItemGroup.FeeItemList.push(fi);
			})
		});
		
	}	
	$scope.CheckAllGroup = function () {
		angular.forEach($scope.newFeeItemGroup.FeeItemList, function (fi) {
			fi.IsChecked = $scope.newFeeItemGroup.CheckAll;
		});
	};
	//Fee Item 
	$scope.IsValidFeeItem = function () {
		if (!$scope.newFeeItem.Name || $scope.newFeeItem.Name.isEmpty()) {
			Swal.fire('Please ! Enter Fee Name');
			return false;
		}
		if ($scope.newFeeItem.OneTimeApplicable) {
			const selectedMonths = $scope.MonthDetailsColl.filter(md => md.IsChecked);
			if (selectedMonths.length !== 1) {
				Swal.fire('For One Time Applicable Fee Item please select only one month.');
				return false;
			}
		}

		return true;
	}

	$scope.SaveUpdateFeeItem = function () {
		if ($scope.IsValidFeeItem() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newFeeItem.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateFeeItem();
					}
				});
			} else
				$scope.CallSaveUpdateFeeItem();

		}
	};

	$scope.CallSaveUpdateFeeItem = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newFeeItem.MonthIdColl = [];
		angular.forEach($scope.MonthDetailsColl, function (md) {
			if (md.IsChecked == true)
				$scope.newFeeItem.MonthIdColl.push(md.id);
		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveFeeItem",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newFeeItem }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearFeeItem();
				$scope.GetAllFeeItemList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllFeeItemList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FeeItemList = [];

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAllFeeItemList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FeeItemList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetFeeItemById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			FeeItemId: refData.FeeItemId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetFeeItemById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFeeItem = res.data.Data;

				if ($scope.newFeeItem.MonthIdColl && $scope.newFeeItem.MonthIdColl.length > 0) {

					var mQuery = mx($scope.newFeeItem.MonthIdColl);
					angular.forEach($scope.MonthDetailsColl, function (md)
					{
						if (mQuery.contains(md.id))
							md.IsChecked = true;
						else
							md.IsChecked = false;
					});

				} else {
					angular.forEach($scope.MonthDetailsColl, function (md) {
						md.IsChecked = false;
					});
                }

				$scope.newFeeItem.Mode = 'Modify';

				document.getElementById('fee-tem-section').style.display = "none";
				document.getElementById('fee-item-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelFeeItemById = function (refData) {

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
					FeeItemId: refData.FeeItemId
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelFeeItem",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFeeItemList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	// Fee Item Group
	$scope.IsValidFeeItemGroup = function () {
		if ($scope.newFeeItemGroup.Name.isEmpty()) {
			Swal.fire('Please ! Enter Fee Item Group Name');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateFeeItemGroup = function () {
		if ($scope.IsValidFeeItemGroup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newFeeItemGroup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateFeeItemGroup();
					}
				});
			} else
				$scope.CallSaveUpdateFeeItemGroup();

		}
	};

	$scope.CallSaveUpdateFeeItemGroup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newFeeItemGroup.FeeItemIdColl = [];
		angular.forEach($scope.newFeeItemGroup.FeeItemList, function (fi) {
			if (fi.IsChecked == true)
				$scope.newFeeItemGroup.FeeItemIdColl.push(fi.FeeItemId);
		});

		//if ($scope.newFeeItemGroup.FeeItemIdColl.length > 0)
		//	$scope.newFeeItemGroup.FeeItemIdColl = $scope.newFeeItemGroup.FeeItemIdColl.toString();
		//else
		//	$scope.newFeeItemGroup.FeeItemIdColl = '';

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveFeeItemGroup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newFeeItemGroup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearFeeItemGroup();
				$scope.GetAllFeeItemGroupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllFeeItemGroupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FeeItemGroupList = [];

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAllFeeItemGroupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FeeItemGroupList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetFeeItemGroupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			FeeItemGroupId: refData.FeeItemGroupId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetFeeItemGroupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFeeItemGroup = res.data.Data;
				$scope.newFeeItemGroup.FeeItemList = [];
				var fiColl = mx(res.data.Data.FeeItemIdColl);
				$timeout(function () {
					angular.forEach($scope.FeeItemList, function (fi) {
						if (fiColl.contains(fi.FeeItemId)) {
							fi.IsChecked = true;
						} else
							fi.IsChecked = false;

						$scope.newFeeItemGroup.FeeItemList.push(fi);
					})
				});

				$scope.newFeeItemGroup.Mode = 'Modify';

				document.getElementById('fee-item-group-section').style.display = "none";
				document.getElementById('fee-item-group-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelFeeItemGroupById = function (refData) {

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
					FeeItemGroupId: refData.FeeItemGroupId
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelFeeItemGroup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFeeItemGroupList();
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