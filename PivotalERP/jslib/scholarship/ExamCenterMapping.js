
app.controller('ExamCenterMappingController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'ExamCenterMapping';

	
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			ExamCenterMapping: 1,
		};

		$scope.searchData = {
			ExamCenterMapping: '',
		};

		$scope.perPage = {
			ExamCenterMapping: GlobalServices.getPerPageRow(),
		};

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
		

		$scope.newDet = {
			TranId: null,
			FromRollNo: '',
			ToRollNo: '',			
			ExamCenterId: null,
			Mode: 'Save'
		};

		$scope.GetAllExamCenterMappingList();
	};

	$scope.ClearExamCenterMapping = function () {
		$scope.newDet = {
			TranId: null,
			FromRollNo: '',
			ToRollNo: '',
			ExamCenterId: null,
			Mode: 'Save'
		};
	};

	

	//************************* ExamCenterMapping *********************************
	$scope.notMapped = function (examCenter) {
		for (var i = 0; i < $scope.ExamCenterMappingList.length; i++) {
			if ($scope.ExamCenterMappingList[i].ExamCenterId === examCenter.ExamCenterId) {
				return false;
			}
		}
		return true;
	};


	$scope.IsValidExamCenterMapping = function () {
		if ($scope.newDet.FromRollNo.isEmpty()) {
			Swal.fire('Please ! Enter From Roll No');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateExamCenterMapping = function () {
		if ($scope.IsValidExamCenterMapping() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamCenterMapping();
					}
				});
			} else
				$scope.CallSaveUpdateExamCenterMapping();
		}
	};

	$scope.CallSaveUpdateExamCenterMapping = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Scholarship/SaveExamCenterMapping",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newDet }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearExamCenterMapping();
				$scope.GetAllExamCenterMappingList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	$scope.GetAllExamCenterMappingList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamCenterMappingList = [];

		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllExamCenterMapping",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamCenterMappingList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetExamCenterMappingById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			ExamCenterMappingId: refData.ExamCenterMappingId
		};
		$http({
			method: 'POST',
			url: base_url + "Scholarship/getExamCenterMappingById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDet = res.data.Data;
				$scope.newDet.Mode = 'Save';
				document.getElementById('complain-listing').style.display = "none";
				document.getElementById('complain-form').style.display = "block";
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExamCenterMappingById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.FromRollNo + ' to ' +refData.ToRollNo +'?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { TranId: refData.TranId };
				$http({
					method: 'POST',
					url: base_url + "Scholarship/DeleteExamCenterMapping",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetAllExamCenterMappingList();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	}


});