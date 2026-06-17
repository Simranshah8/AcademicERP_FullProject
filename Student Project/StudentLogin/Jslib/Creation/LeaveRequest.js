app.controller('LeaveRequestController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Leave Request';

	OnClickDefault();

	String.prototype.isEmpty = function () {
		return (this.length === 0 || !this.trim());
	};

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		

		$scope.currentPages = {
			LeaveRequest: 1,

		};

		$scope.searchData = {
			LeaveRequest: '',

		};

		$scope.perPage = {
			LeaveRequest: GlobalServices.getPerPageRow(),

		};

		$scope.newLeaveRequest = {
			LeaveRequestId: null,
			RequestDate_TMP: null,
			LeaveTypeId: null,
			FromDate_TMP: null,
			ToDate_TMP: null,
			Reason: '',
			attachFile: null,
			Mode:'Save'

		}; 
	}


	function OnClickDefault() {


		document.getElementById('leave-request-form').style.display = "none";


		document.getElementById('add-request').onclick = function () {

			document.getElementById('leave-request-section').style.display = "none";
			document.getElementById('leave-request-form').style.display = "block";
			$scope.ClearLeaveRequest();
		}

		document.getElementById('back-to-list').onclick = function () {
			document.getElementById('leave-request-section').style.display = "block";
			document.getElementById('leave-request-form').style.display = "none";
			$scope.ClearLeaveRequest();
		}

		
	}


	$scope.ClearLeaveRequest = function () {
		$scope.newLeaveRequest = {
			LeaveRequestId: null,
			RequestDate_TMP: null,
			LeaveTypeId: null,
			FromDate_TMP: null,
			ToDate_TMP: null,
			Reason: '',
			attachFile: null,
			Mode: 'Save'

		};
	} 

	$scope.IsValidLeaveRequest = function () {
		if ($scope.newLeaveRequest.Reason.isEmpty()) {
			Swal.fire('Please ! Enter Leave Reason');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateLeaveRequest = function () {
		if ($scope.IsValidLeaveRequest() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newLeaveRequest.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateLeaveRequest();
					}
				});
			} else
				$scope.CallSaveUpdateLeaveRequest();

		}
	};

	$scope.CallSaveUpdateLeaveRequest = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newLeaveRequest.RequestDateDet) {
			$scope.newLeaveRequest.RequestDate = $scope.newLeaveRequest.RequestDateDet.dateAD;
		} else
			$scope.newLeaveRequest.RequestDate = null;

		if ($scope.newLeaveRequest.FromDateDet) {
			$scope.newLeaveRequest.FromDate = $scope.newLeaveRequest.FromDateDet.dateAD;
		} else
			$scope.newLeaveRequest.FromDate = null;

		if ($scope.newLeaveRequest.ToDateDet) {
			$scope.newLeaveRequest.ToDate = $scope.newLeaveRequest.ToDateDet.dateAD;
		} else
			$scope.newLeaveRequest.ToDate = null;

		$http({
			method: 'POST',
			url: base_url + "Creation/Creation/SaveLeaveRequest",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newLeaveRequest }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearLeaveRequest();
				$scope.GetAllLeaveRequestList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllLeaveRequestList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.LeaveRequestList = [];

		$http({
			method: 'POST',
			url: base_url + "Creation/Creation/GetAllLeaveRequestList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LeaveRequestList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetLeaveRequestById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			LeaveRequestId: refData.LeaveRequestId
		};

		$http({
			method: 'POST',
			url: base_url + "Creation/Creation/GetLeaveRequestById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newLeaveRequest = res.data.Data;
				$scope.newLeaveRequest.Mode = 'Modify';

				document.getElementById('leave-request-section').style.display = "none";
				document.getElementById('leave-request-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelLeaveRequestById = function (refData) {

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
					LeaveRequestId: refData.LeaveRequestId
				};

				$http({
					method: 'POST',
					url: base_url + "Creation/Creation/DelLeaveRequest",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllLeaveRequestList();
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