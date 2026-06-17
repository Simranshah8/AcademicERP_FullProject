
app.controller('AllowBranchController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow Branch';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.newAllowBranch = {
			AllowBranchId: null,
			UserId: null,
			CheckAll: false, 
			Mode: 'Update Data'
		};
		 
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

		$scope.BranchList = [];
		$scope.BranchList_Qry = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchList = res.data.Data;
				$scope.BranchList_Qry = mx(res.data.Data);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.ClearAllowBranch = function () {
		$scope.newAllowBranch = {
			AllowBranchId: null,
			UserId: null,
			CheckAll: false,

			Mode: 'Update Data'
		};
	}
  
	$scope.CheckUnCheckAll = function () {
		var val = $scope.newAllowBranch.CheckedAll;
		angular.forEach($scope.BranchList, function (cl) {
			cl.Allow = val;
		});
	}
	//************************* AllowBranch *********************************

	$scope.IsValidAllowBranch = function () {
		if (!$scope.newAllowBranch || $scope.newAllowBranch.UserId==undefined) {
			Swal.fire('Please ! Select User');
			return false;
		}
		
		return true;
	}

	$scope.SaveUpdateAllowBranch = function () {
		if ($scope.IsValidAllowBranch() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAllowBranch.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAllowBranch();
					}
				});
			} else
				$scope.CallSaveUpdateAllowBranch();

		}
	};

	$scope.CallSaveUpdateAllowBranch = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ForUserId: $scope.newAllowBranch.UserId,
			BranchIdColl:[],
		}

		angular.forEach($scope.BranchList, function (bl) {
			if (bl.Allow == true)
				para.BranchIdColl.push(bl.BranchId);
		});

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveAllowBranch",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAllowBranchList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		angular.forEach($scope.BranchList, function (bl) {
			bl.Allow = false;
		});

		var para = {
			ForUserId: $scope.newAllowBranch.UserId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllowBranchById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				 var idColl= mx(res.data.Data);
				angular.forEach($scope.BranchList, function (bl) {
					bl.Allow = false;

					if (idColl.contains(bl.BranchId) == true)
						bl.Allow = true;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
	  

});