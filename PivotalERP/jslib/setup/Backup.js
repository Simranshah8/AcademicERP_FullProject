app.controller('BackupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Backup';

	$scope.LoadData = function () {
		$scope.beData = {
			PassKey:'',
			FilePath: '',
			IsSuccess:false
		};

    }
	   
	$scope.DoBackup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			passKey: $scope.beData.PassKey
		};
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/DoBackup",
			dataType: "json",
			data:JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data)
			{
				Swal.fire(res.data.ResponseMSG);
				$scope.beData.IsSuccess = res.data.IsSuccess;
				$scope.beData.FilePath = res.data.Data.FilePath;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAllBranchList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BranchList = [];

	
	}

	$scope.GetBranchById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BranchId: refData.BranchId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetBranchById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBranch = res.data.Data;
				$scope.newBranch.Mode = 'Modify';

				document.getElementById('branch-section').style.display = "none";
				document.getElementById('branch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBranchById = function (refData) {

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
					BranchId: refData.BranchId
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/DelBranch",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBranchList();
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