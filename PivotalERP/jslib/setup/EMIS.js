
app.controller('emisController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow Branch';

	$scope.LoadData = function () {
	
		$scope.ImportData = {
			EntityId: 0,
			Files_TMP: null
		};

		$scope.newEMIS = {
			SchoolCode: '',
			AcademicYear: 2078,
			ShowDownload: false,
			RegdIdAs: 1,
			TeacherIdAs: 1,
			ClassNameAs:1,
			FilePath:''
		};

		$scope.RegdIdAsList = [{ id: 1, text: 'Regd.No' }, { id: 2, text: 'EMSId' }];
		$scope.TeacherIdAsList = [{ id: 1, text: 'Emp. Code' }, { id: 2, text: 'EMSId' }];
		$scope.ClassNameAsList = [{ id: 1, text: 'Name' }, { id: 2, text: 'Description' }];
	}
	
	$scope.UploadToSrv = function () {

		if ($scope.ImportData.Files_TMP && $scope.ImportData.Files_TMP.length > 0) {

			$scope.SelectedFile = null;
			$scope.FilePath = null;
			$scope.SheetColl = [];
			$scope.SelectedSheet = null;
			$scope.PropertiesColl = [];
			$scope.ColumnColl = [];

			$http({
				method: 'POST',
				url: base_url + "Setup/EMIS/SaveEMISDataExcelFile",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					formData.append("file0", data.files);

					return formData;
				},
				data: { jsonData:$scope.newEMIS, files: $scope.ImportData.Files_TMP[0] }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				var data = res.data;
				Swal.fire(data.ResponseMSG);

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});


		}

	};

	$scope.UploadToSrvForExport = function () {

		if ($scope.ImportData.Files_TMP && $scope.ImportData.Files_TMP.length > 0) {

			$scope.SelectedFile = null;
			$scope.FilePath = null;
			$scope.SheetColl = [];
			$scope.SelectedSheet = null;
			$scope.PropertiesColl = [];
			$scope.ColumnColl = [];

			$http({
				method: 'POST',
				url: base_url + "Setup/EMIS/SaveEMISExcelFileForExport",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					formData.append("file0", data.files);

					return formData;
				},
				data: { jsonData: $scope.newEMIS, files: $scope.ImportData.Files_TMP[0] }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				var data = res.data;
				if (data.IsSuccess == true) {
					$scope.newEMIS.FilePath = data.ResponseMSG;
					$scope.newEMIS.ShowDownload = true;
                }else
					Swal.fire(data.ResponseMSG);

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});


		}

	};

	$scope.ClearFields = function () {
		$scope.loadingstatus = "running";

		$scope.ImportData = {
			EntityId: 0,
			Files_TMP: null
		};

		$scope.SelectedFile = null;
		$scope.FilePath = null;
		$scope.SheetColl = [];
		$scope.SelectedSheet = null;
		$scope.SelectedEntityId = null;

		$scope.PropertiesColl = [];
		$scope.ColumnColl = [];
		$scope.loadingstatus = "stop";
	}


	//************************* AllowBranch *********************************

	$scope.IsValidAllowBranch = function () {
		if ($scope.newAllowBranch.UserId.isEmpty()) {
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

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/SaveAllowBranch",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newAllowBranch }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAllowBranch();
				$scope.GetAllAllowBranchList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});



	}

	$scope.GetAllAllowBranchList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowBranchList = [];

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/GetAllAllowBranchList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowBranchList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetAllowBranchById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AllowBranchId: refData.AllowBranchId
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/GetAllowBranchById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAllowBranch = res.data.Data;
				$scope.newAllowBranch.Mode = 'Modify';

				document.getElementById('author-section').style.display = "none";
				document.getElementById('author-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAllowBranchById = function (refData) {

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
					AllowBranchId: refData.AllowBranchId
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Setup/DelAllowBranch",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAllowBranchList();
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