app.controller('CConformationController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Continuous Conformation';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.NotContinueReasonList = [
			{ id: 1, text: 'Transferring to another school' },
			{ id: 2, text: 'Relocating to another city/country' },
			{ id: 3, text: 'Financial reasons' },
			{ id: 4, text: 'Health issues' },
			{ id: 5, text: 'Personal/family reasons' },
			{ id: 6, text: 'Dissatisfaction with academic performance' },
			{ id: 7, text: 'Other' },]	
		
		$scope.newDet = {
			StudentId: null,
			ContinueYes: false,
			ContinueNo: false,
			NotContinueReasonId: null,
			OtherReason: '',
			Feedback: '',
			Mode: 'Save'
		};
		
	}

	$scope.ClearContinuousConfirmation = function () {
		$scope.newDet = {
			StudentId: null,
			ContinueYes: false,
			ContinueNo: false,
			NotContinueReasonId: null,
			OtherReason: '',
			Feedback: '',
			Mode: 'Save'
		};
	}

	$scope.selectContinueOption = function (option) {
		if (option === 'yes') {
			$scope.newDet.ContinueNo = false;
			$scope.newDet.NotContinueReasonId = null;
			$scope.newDet.OtherReason = '';
		} else if (option === 'no') {
			$scope.newDet.ContinueYes = false;
		}
	};


	


	$scope.IsValidContinuousConfirmation = function () {	
		if (!$scope.newDet.ContinueYes && !$scope.newDet.ContinueNo) {
			Swal.fire('Please select whether you will continue or not.');
			return false;
		}
		if ($scope.newDet.ContinueNo) {
			if (!$scope.newDet.NotContinueReasonId || $scope.newDet.NotContinueReasonId === '') {
				Swal.fire('Please select a reason for not continuing.');
				return false;
			}
			if ($scope.newDet.NotContinueReasonId == 7 && (!$scope.newDet.OtherReason || $scope.newDet.OtherReason.trim() === '')) {
				Swal.fire('Please specify the other reason for not continuing.');
				return false;
			}
		}
		return true;
	};


	$scope.SaveUpdateContinuousConfirmation = function () {
		if ($scope.IsValidContinuousConfirmation() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateContinuousConfirmation();
					}
				});
			} else
				$scope.CallSaveUpdateContinuousConfirmation();
		}
	};

	$scope.CallSaveUpdateContinuousConfirmation = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.newDet.StudentId = 0;
		$http({
			method: 'POST',
			url: base_url + "Student/Creation/SaveContinewConfirmation",
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
				$scope.ClearContinuousConfirmation();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	
	//$scope.GetContinuousConfirmationById = function (refData) {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	var para = {
	//		TranId: refData.TranId
	//	};
	//	$http({
	//		method: 'POST',
	//		url: base_url + "Academic/Transaction/getContinuousConfirmationById",
	//		dataType: "json",
	//		data: JSON.stringify(para)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess && res.data.Data) {
	//			$scope.newDet = res.data.Data;
	//			$scope.newDet.Mode = 'Modify';

	//			$scope.newDet.SelectStudent = $scope.StudentSearchOptions[0].value;

	//		} else {
	//			Swal.fire(res.data.ResponseMSG);
	//		}
	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//};

});