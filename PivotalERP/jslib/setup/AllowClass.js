
app.controller('AllowClassController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow Class';

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();

		$scope.newAllowClass = {
			UserId: null,
			CheckedAll:false
		};

		$scope.AllowClassList = [];
		$scope.UserList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUserList",
			dataType: "json"
		}).then(function (res) {			
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserList = res.data.Data;

			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	}

	$scope.CheckUnCheckAll = function () {
		var val = $scope.newAllowClass.CheckedAll;
		angular.forEach($scope.AllowClassList, function (cl) {
			cl.IsAllow = val;
		});
    }
	$scope.ClearAllowClass = function () {
		$scope.newAllowClass = {
			UserId: null
		};

		$scope.AllowClassList = [];
	}


	//************************* AllowClass *********************************

	$scope.IsValidAllowClass = function () {
		if ($scope.AllowClassList.length == 0)
		{
			Swal.fire('Please ! Select User');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAllowClass = function () {
		if ($scope.IsValidAllowClass() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAllowClass.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAllowClass();
					}
				});
			} else
				$scope.CallSaveUpdateAllowClass();

		}
	};

	$scope.CallSaveUpdateAllowClass = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveAllowClass",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.AllowClassList }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	}

	$scope.GetAllowClassList = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			UserId: $scope.newAllowClass.UserId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllowClassById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowClassList = res.data.Data;				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

});


app.controller('AllowAcademicYearController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow AcademicYear';

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();

		$scope.newAllowAcademicYear = {
			UserId: null,
			CheckedAll: false
		};

		$scope.AllowAcademicYearList = [];
		$scope.UserList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUserList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserList = res.data.Data;

			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	}

	$scope.CheckUnCheckAll = function () {
		var val = $scope.newAllowAcademicYear.CheckedAll;
		angular.forEach($scope.AllowAcademicYearList, function (cl) {
			cl.IsAllow = val;
		});
	}
	$scope.ClearAllowAcademicYear = function () {
		$scope.newAllowAcademicYear = {
			UserId: null
		};

		$scope.AllowAcademicYearList = [];
	}


	//************************* AllowAcademicYear *********************************

	$scope.IsValidAllowAcademicYear = function () {
		if ($scope.AllowAcademicYearList.length == 0) {
			Swal.fire('Please ! Select User');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAllowAcademicYear = function () {
		if ($scope.IsValidAllowAcademicYear() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAllowAcademicYear.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAllowAcademicYear();
					}
				});
			} else
				$scope.CallSaveUpdateAllowAcademicYear();

		}
	};

	$scope.CallSaveUpdateAllowAcademicYear = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveAllowAcademicYear",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.AllowAcademicYearList }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	}

	$scope.GetAllowAcademicYearList = function () {

		if (!$scope.newAllowAcademicYear.UserId)
			return;

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			UserId: $scope.newAllowAcademicYear.UserId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllowAcademicYearById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowAcademicYearList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

});