app.controller('VisionStatementController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Introduction';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			VisionStatement: 1,
			
		};

		$scope.searchData = {
			VisionStatement: '',
			
		};

		$scope.perPage = {
			VisionStatement: GlobalServices.getPerPageRow(),
			
		};

		$scope.newVisionStatement = {
			VisionStatementId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};

		
		$scope.GetAllVisionStatementList();
		
	}
	$scope.ShowPersonalImg = function (item) {
		$scope.viewImg = {
			ContentPath: ''
		};
		if (item.ImagePath && item.ImagePath.length > 0) {
			$scope.viewImg.ContentPath = item.ImagePath;
			$('#PersonalImg').modal('show');
		} else
			Swal.fire('No Image Found');

	};
	function OnClickDefault() {


		document.getElementById('rules-form').style.display = "none";
		

		// Vision Statement 
		document.getElementById('add-vision-form').onclick = function () {

			$scope.ClearVisionStatement();
			document.getElementById('vision-table-listing').style.display = "none";
			document.getElementById('rules-form').style.display = "block";
		}
		document.getElementById('back-to-vision-list').onclick = function () {
			$scope.ClearVisionStatement();
			document.getElementById('vision-table-listing').style.display = "block";
			document.getElementById('rules-form').style.display = "none";
		}

		

	}

	$scope.ClearVisionStatement = function () {

		$scope.ClearVisionStatementPhotoVision();
		$('input[type=file]').val('');
		$scope.newVisionStatement = {
			VisionStatementId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};
	}

	

	$scope.ClearVisionStatementPhotoVision = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newVisionStatement.PhotoDataVision = null;
				$scope.newVisionStatement.PhotoVision_TMP = [];
				$scope.newVisionStatement.ImagePath = '';
			});

		});
		$('#imgVision').attr('src', '');
		$('#imgVision1').attr('src', '');
	};

	


	//************************* VisionStatement *********************************

	$scope.IsValidVisionStatement = function () {
		if ($scope.newVisionStatement.Title.isEmpty()) {
			Swal.fire('Please ! Enter Heading');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateVisionStatement = function () {
		if ($scope.IsValidVisionStatement() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newVisionStatement.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateVisionStatement();
					}
				});
			} else
				$scope.CallSaveUpdateVisionStatement();

		}
	};

	$scope.CallSaveUpdateVisionStatement = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newVisionStatement.PhotoVision_TMP;
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveVisionStatement",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						if (data.files[i].File)
							formData.append("file" + i, data.files[i].File);
						else
							formData.append("file" + i, data.files[i]);
					}
				}

				return formData;
			},
			data: { jsonData: $scope.newVisionStatement, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearVisionStatement();
				$scope.GetAllVisionStatementList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllVisionStatementList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.VisionStatementList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllVisionStatementList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.VisionStatementList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetVisionStatementById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			VisionStatementId: refData.VisionStatementId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetVisionStatementById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newVisionStatement = res.data.Data;
				$scope.newVisionStatement.Mode = 'Modify';

				document.getElementById('vision-table-listing').style.display = "none";
				document.getElementById('rules-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelVisionStatementById = function (refData) {

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
					VisionStatementId: refData.VisionStatementId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelVisionStatement",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllVisionStatementList();
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