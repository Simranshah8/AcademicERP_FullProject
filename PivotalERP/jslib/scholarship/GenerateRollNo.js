
app.controller('GenerateRollNoController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'GenerateRollNo';


	$scope.LoadData = function () {
		$('.select2').select2();
		var glbS = GlobalServices;
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		

		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res) {
			$scope.SubjectList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


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
			GenerateId: null,
			StartNo: '',
			PadWidth: '',
			Prefix: '',
			Suffix: '',			
			Mode: 'Save'
		};

		$scope.GetAllGenerateRollNoList();
	};

	$scope.ClearGenerateRollNo = function () {
		$scope.newDet = {
			GenerateId: null,
			StartNo: '',
			PadWidth: '',
			Prefix: '',
			Suffix: '',
			Mode: 'Save'
		};
	};



	//************************* GenerateRollNo *********************************
	$scope.IsValidGenerateRollNo = function () {
		if ($scope.newDet.StartNo.isEmpty()) {
			Swal.fire('Please ! Enter StartNo');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateGenerateRollNo = function () {
		if ($scope.IsValidGenerateRollNo() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateGenerateRollNo();
					}
				});
			} else
				$scope.CallSaveUpdateGenerateRollNo();
		}
	};

	$scope.CallSaveUpdateGenerateRollNo = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Scholarship/SaveGenerateRollNo",
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
				$scope.ClearGenerateRollNo();
				/*$scope.GetAllGenerateRollNoList();*/
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	$scope.GetAllGenerateRollNoList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.GenerateRollNoList = [];

		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllGenerateRollNo",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.GenerateRollNoList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.DelGenerateRollNoById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete selected RollNo',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { GenerateId: refData.GenerateId };
				$http({
					method: 'POST',
					url: base_url + "Scholarship/DeleteGenerateRollNo",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetAllGenerateRollNoList();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	}


	

});