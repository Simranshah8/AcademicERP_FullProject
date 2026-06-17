app.controller('AddHostelController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Add Hostel';
	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			Floor: 1
		};

		$scope.searchData = {
			Floor: ''
		};

		$scope.perPage = {
			Floor: GlobalServices.getPerPageRow()
		};

		

		

		$scope.newFloor = {
			FloorId: null,
			Name: '',
			Mode: 'Save'
		};

		
		$scope.GetAllFloorList();
	}

	function OnClickDefault() {
		document.getElementById('floor-form').style.display = "none";


		


		document.getElementById('add-floor').onclick = function () {
			document.getElementById('floor-list').style.display = "none";
			document.getElementById('floor-form').style.display = "block";
			$scope.ClearFloor();
		}
		document.getElementById('back-btn-floor').onclick = function () {
			document.getElementById('floor-form').style.display = "none";
			document.getElementById('floor-list').style.display = "block";
			$scope.ClearFloor();
		}
	};	

	$scope.ClearFloor = function () {
		$scope.newFloor = {
			FloorId: null,
			Name: '',
			Mode: 'Save'
		};
	}

	
	//*************************Floor *********************************
	$scope.IsValidFloor = function () {
		if ($scope.newFloor.Name.isEmpty()) {
			Swal.fire('Please ! Enter Floor Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateFloor = function () {
		if ($scope.IsValidFloor() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newFloor.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateFloor();
					}
				});
			} else
				$scope.CallSaveUpdateFloor();
		}
	};

	$scope.CallSaveUpdateFloor = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveFloor",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newFloor }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearFloor();
				$scope.GetAllFloorList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllFloorList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
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

	}

	$scope.GetFloorById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			FloorId: refData.FloorId
		};
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetFloorById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFloor = res.data.Data;
				$scope.newFloor.Mode = 'Modify';
				document.getElementById('floor-list').style.display = "none";
				document.getElementById('floor-form').style.display = "block";
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelFloorById = function (refData) {
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
					FloorId: refData.FloorId
				};
				$http({
					method: 'POST',
					url: base_url + "Hostel/Creation/DelFloor",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFloorList();
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