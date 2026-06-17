

app.controller('BranchwiseController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'TrailBalance Branchwise';


	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();


		//$scope.currentPages = {
		//	Branchwise: 1,

		//};

		//$scope.searchData = {
		//	Branchwise: '',

		//};

		//$scope.perPage = {
		//	Branchwise: GlobalServices.getPerPageRow(),

		//};

		$scope.newBranchwise = {
			BranchwiseId: null,

			Mode: 'Save'
		};

		//$scope.GetAllBranchwiseList();

	};



	$scope.ClearBranchwise = function () {
		$scope.newBranchwise = {
			BranchwiseId: null,

			Mode: 'Save'
		};

	};

	$scope.SaveUpdateBranchwise = function () {
		if ($scope.IsValidBranchwise() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBranchwise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBranchwise();
					}
				});
			} else
				$scope.CallSaveUpdateBranchwise();

		}
	};



	$scope.GetAllBranchwiseList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BranchwiseList = [];

		$http({
			method: 'POST',
			url: base_url + "Account/Reporting/GetAllBranchwiseList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchwiseList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};



});