app.controller('BranchController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Branch';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			Branch: 1,

		};

		$scope.searchData = {
			Branch: '',

		};

		$scope.perPage = {
			Branch: GlobalServices.getPerPageRow(),

		};

		$scope.newBranch = {
			BranchId: 0,
			Name: '',
			Code: '',
			Address: '',
			ContactNo: '',
			ContactPerson: '',
			FaxNo: '',
			PanNo: '',
			EmailId: '',
			Mode: 'Save'
		};


		$scope.CostClassColl = [];
		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetAllCostClassList",
			dataType: "json"
		}).then(function (res1) {
			if (res1.data.IsSuccess && res1.data.Data) {
				$scope.CostClassColl = res1.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



		$scope.AcademicYearColl = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllAcademicYearList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AcademicYearColl = res.data.Data;

				if ($scope.AcademicYearColl.length > 0) {
					$scope.CurAcademicYearId = $scope.AcademicYearColl[0].AcademicYearId;
				}
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllBranchList();

	}

	function OnClickDefault() {

		document.getElementById('branch-form').style.display = "none";

		//branch section
		document.getElementById('add-branch').onclick = function () {
			document.getElementById('branch-section').style.display = "none";
			document.getElementById('branch-form').style.display = "block";
			$timeout(function () {
				$scope.ClearBranch();
			});

		}

		document.getElementById('back-to-list-branch').onclick = function () {
			document.getElementById('branch-form').style.display = "none";
			document.getElementById('branch-section').style.display = "block";
			$timeout(function () {
				$scope.ClearBranch();
			});
		}

	}

	$scope.ClearBranch = function () {
		$scope.newBranch = {
			BranchId: 0,
			Name: '',
			Code: '',
			Address: '',
			ContactNo: '',
			ContactPerson: '',
			FaxNo: '',
			PanNo: '',
			EmailId: '',
			Mode: 'Save'
		};

	}

	//*************************Branch *********************************

	$scope.IsValidBranch = function () {
		if ($scope.newBranch.Name.isEmpty()) {
			Swal.fire('Please ! Enter Branch Name');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateBranch = function () {
		if ($scope.IsValidBranch() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBranch.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBranch();
					}
				});
			} else
				$scope.CallSaveUpdateBranch();

		}
	};

	$scope.CallSaveUpdateBranch = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveBranch",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newBranch }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBranch();
				$scope.GetAllBranchList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllBranchList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BranchList = [];

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

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