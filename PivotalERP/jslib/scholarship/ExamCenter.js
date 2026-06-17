
app.controller('ExamCenterController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'ExamCenter';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();	
		
		$scope.currentPages = {
			ExamCenter: 1,
		};

		$scope.searchData = {
			ExamCenter: '',
		};

		$scope.perPage = {
			ExamCenter: GlobalServices.getPerPageRow(),
		};

		

		$scope.newExamCenter = {
			ExamCenterId: null,
			Name: '',
			Address: '',
			Email: '',
			ContactNo: '',
			Description: '',
			OrderNo: 0,			
			Latitude: null,
			Longitude:null,
			Mode: 'Save'
		};

		$scope.GetAllExamCenterList();
	};

	$scope.ClearExamCenter = function () {
		$scope.newExamCenter = {
			ExamCenterId: null,
			Name: '',
			Address: '',
			Email: '',
			ContactNo: '',
			Description: '',
			OrderNo: 0,
			Latitude: null,
			Longitude: null,
			Mode: 'Save'
		};
	};

	function OnClickDefault() {
		document.getElementById('complain-form').style.display = "none";

		document.getElementById('add-complain-btn').onclick = function () {
			document.getElementById('complain-listing').style.display = "none";
			document.getElementById('complain-form').style.display = "block";
			$scope.ClearClass();
		}

		document.getElementById('back-complain-list').onclick = function () {
			document.getElementById('complain-listing').style.display = "block";
			document.getElementById('complain-form').style.display = "none";
			$scope.ClearClass();
		}
	};

	//************************* ExamCenter *********************************

	$scope.IsValidExamCenter = function () {
		if ($scope.newExamCenter.Name.isEmpty()) {
			Swal.fire('Please ! Enter ExamCenter Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateExamCenter = function () {
		if ($scope.IsValidExamCenter() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamCenter.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamCenter();
					}
				});
			} else
				$scope.CallSaveUpdateExamCenter();
		}
	};

	$scope.CallSaveUpdateExamCenter = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Scholarship/SaveExamCenter",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newExamCenter }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearExamCenter();
				$scope.GetAllExamCenterList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}
	

	$scope.GetAllExamCenterList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamCenterList = [];
		
		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllExamCenter",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamCenterList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetExamCenterById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			ExamCenterId: refData.ExamCenterId
		};
		$http({
			method: 'POST',
			url: base_url + "Scholarship/getExamCenterById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExamCenter = res.data.Data;
				$scope.newExamCenter.Mode = 'Save';


				

				document.getElementById('complain-listing').style.display = "none";
				document.getElementById('complain-form').style.display = "block";
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExamCenterById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { ExamCenterId: refData.ExamCenterId };
				$http({
					method: 'POST',
					url: base_url + "Scholarship/DeleteExamCenter",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetAllExamCenterList();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	}

	
});