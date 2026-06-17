
String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};


app.controller('StudentLeaveController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'StudentLeave';

	//OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		

		$scope.currentPages = {
			StudentLeave: 1,

		};

		$scope.searchData = {
			StudentLeave: '',

		};

		$scope.perPage = {
			StudentLeave: GlobalServices.getPerPageRow(),

		};

		$scope.newStudentLeave = {
			StudentLeaveId: null,
			
			Mode: 'Save'
		};

		//$scope.GetAllStudentLeaveList();

	};


	$scope.ClearStudentLeave = function () {
		$scope.newStudentLeave = {
			StudentLeaveId: null,
			
			Mode: 'Save'
		};
	};



	
	//************************* Class *********************************

	$scope.IsValidStudentLeave = function () {
		if ($scope.newStudentLeave.StudentLeaveBy.isEmpty()) {
			Swal.fire('Please ! Enter who StudentLeave');
			return false;
		}

		if ($scope.newStudentLeave.ActionTaken.isEmpty()) {
			Swal.fire('Please ! Enter Action Taken');
			return false;
		}
		if ($scope.newStudentLeave.Remarks.isEmpty()) {
			Swal.fire('Please ! Enter Remarks');
			return false;
		}



		return true;
	};


	




	$scope.SaveUpdateStudentLeave = function () {
		if ($scope.IsValidStudentLeave() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudentLeave.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudentLeave();
					}
				});
			} else
				$scope.CallSaveUpdateStudentLeave();

		}
	};

	$scope.CallSaveUpdateStudentLeave = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newStudentLeave.StudentLeaveDateDet) {
			$scope.newStudentLeave.StudentLeaveDate = $scope.newStudentLeave.StudentLeaveDateDet.dateAD;
		} else
			$scope.newStudentLeave.StudentLeaveDate = null;


		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveStudentLeave",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newStudentLeave }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStudentLeave();
				$scope.GetAllStudentLeaveList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllStudentLeaveList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentLeaveList = [];

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllStudentLeaveList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentLeaveList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetStudentLeaveById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			StudentLeaveId: refData.StudentLeaveId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetStudentLeaveById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newStudentLeave = res.data.Data;
				$scope.newStudentLeave.Mode = 'Modify';

				document.getElementById('class-section').style.display = "none";
				document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelStudentLeaveById = function (refData) {

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
					StudentLeaveId: refData.StudentLeaveId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelStudentLeave",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStudentLeaveList();
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