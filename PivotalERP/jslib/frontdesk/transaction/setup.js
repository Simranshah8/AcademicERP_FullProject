app.controller('SetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Setup';
	OnClickDefault();
	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			Setup: 1,
			ComplainType: 1,
			Source:1,
			
		};

		$scope.searchData = {
			Setup: '',
			ComplainType: '',
			Source:''
		};

		$scope.perPage = {
			Setup: GlobalServices.getPerPageRow(),
			ComplainType: GlobalServices.getPerPageRow(),
			Source: GlobalServices.getPerPageRow(),
			
		};

		$scope.newSource = {
			SetupId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Status: 1,
			IsActive:true,
			Mode: 'Save'
		};

		$scope.newComplainType = {
			ComplainTypeId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			ComplainTypeFor:1,
			Mode: 'Save'
		};

		$scope.ComplainTypeFors = [{ id: 1, text: 'Front Desk' }, { id: 2, text: 'Student' }, { id: 3, text: 'Employee' }]

		$scope.GetAllSourceList();
		$scope.GetAllComplainTypeList();
		
		$scope.ClassList = [];
		GlobalServices.getClassList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	function OnClickDefault() {
		document.getElementById('source-form').style.display = "none";
		document.getElementById('setup-complain-form').style.display = "none";

		document.getElementById('add-source').onclick = function () {
			document.getElementById('source-section').style.display = "none";
			document.getElementById('source-form').style.display = "block";
			$scope.ClearSource();
		}
		document.getElementById('sourceback-btn').onclick = function () {
			document.getElementById('source-form').style.display = "none";
			document.getElementById('source-section').style.display = "block";
			$scope.ClearSource();
		}

		document.getElementById('add-setup-complain').onclick = function () {
			document.getElementById('setup-complain-section').style.display = "none";
			document.getElementById('setup-complain-form').style.display = "block";
			$scope.ClearComplainType();
		}
		document.getElementById('setup-complainback-btn').onclick = function () {
			document.getElementById('setup-complain-form').style.display = "none";
			document.getElementById('setup-complain-section').style.display = "block";
			$scope.ClearComplainType();
		}
	}

	$scope.ClearSource = function () {
		$scope.newSource = {
			SetupId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Status: 1,
			IsActive:true,
			Mode: 'Save'
		};
	}
	$scope.ClearComplainType = function () {
		$scope.newComplainType = {
			ComplainTypeId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			ComplainTypeFor: 1,
			Mode: 'Save'
		};
	}
	

	//************************* Source *********************************

	$scope.IsValidSource = function () {
		if ($scope.newSource.Name.isEmpty()) {
			Swal.fire('Please ! Enter Setup Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateSource = function () {
		if ($scope.IsValidSource() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSource.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSource();
					}
				});
			} else
				$scope.CallSaveUpdateSource();

		}
	};

	$scope.CallSaveUpdateSource = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newSource.IsActive == true)
			$scope.newSource.Status = 1;
		else
			$scope.newSource.Status = 2;

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveSource",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSource }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearSource();
				$scope.GetAllSourceList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllSourceList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SourceList = [];

		var para = {
			ForTran: false
		};
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllSourceList",
			dataType: "json",
			data:JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SourceList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetSourceById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			SourceId: refData.SourceId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetSourceById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSource = res.data.Data;
				$scope.newSource.Mode = 'Modify';

				document.getElementById('source-section').style.display = "none";
				document.getElementById('source-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelSourceById = function (refData) {
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
					SourceId: refData.SourceId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelSource",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllSourceList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.SaveForOnlineRegistration = function () {
		$scope.loadingstatus = "running";
		
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/UpdateClassForOR",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.ClassList }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	//************************* Complain Type *********************************

	$scope.IsValidComplainType = function () {
		if ($scope.newComplainType.Name.isEmpty()) {
			Swal.fire('Please ! Enter ComplainType Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateComplainType = function () {
		if ($scope.IsValidComplainType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newComplainType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateComplainType();
					}
				});
			} else
				$scope.CallSaveUpdateComplainType();

		}
	};

	$scope.CallSaveUpdateComplainType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveComplainType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newComplainType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearComplainType();
				$scope.GetAllComplainTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllComplainTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ComplainTypeList = [];

		var para = {
			ForTran: false
		};
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllComplainTypeList",
			dataType: "json",
			data:JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ComplainTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetComplainTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ComplainTypeId: refData.ComplainTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetComplainTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newComplainType = res.data.Data;
				$scope.newComplainType.Mode = 'Modify';

				document.getElementById('setup-complain-section').style.display = "none";
				document.getElementById('setup-complain-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelComplainTypeById = function (refData) {

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
					ComplainTypeId: refData.ComplainTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelComplainType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllComplainTypeList();
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