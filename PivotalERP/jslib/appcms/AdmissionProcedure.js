app.controller('AdmissionProcedureController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Admission Procedure';

	OnClickDefault();
	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			AdmissionProcedure: 1

		};

		$scope.searchData = {
			AdmissionProcedure: ''

		};

		$scope.perPage = {
			AdmissionProcedure: GlobalServices.getPerPageRow()
		};		

		$scope.newAdmissionProcedure = {
			AdmissionProcedureId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};

	
		$scope.GetAllAdmissionProcedureList();
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
		document.getElementById('admission-procedure-form').style.display = "none";
		
		// Admission Procedure
		document.getElementById('add-admission-procedure').onclick = function () {
			$scope.ClearAdmissionProcedure();
			document.getElementById('admission-pro-listing').style.display = "none";
			document.getElementById('admission-procedure-form').style.display = "block";

		}
		document.getElementById('back-to-admission-procedure-list').onclick = function () {
			$scope.ClearAdmissionProcedure();
			document.getElementById('admission-pro-listing').style.display = "block";
			document.getElementById('admission-procedure-form').style.display = "none";
		}
		
	};	

	$scope.ClearAdmissionProcedure = function () {
		$scope.ClearAdmissionProcedurePhotoAdmission();
		$scope.newAdmissionProcedure = {
			AdmissionProcedureId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};
	}

	
	$scope.ClearAdmissionProcedurePhotoAdmission = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newAdmissionProcedure.PhotoDataAdmission = null;
				$scope.newAdmissionProcedure.PhotoAdmission_TMP = [];
				$scope.newAdmissionProcedure.ImagePath = '';
			});

		});
		$('#imgAdmission').attr('src', '');
		$('#imgAdmission1').attr('src', '');
	};


	/*Admission Procedure Tab Js*/
	$scope.IsValidAdmissionProcedure = function () {

		if ($scope.newAdmissionProcedure.Title.isEmpty()) {
			Swal.fire('Please ! Enter Heading');
			return false;
		}

		if ($scope.newAdmissionProcedure.Description.isEmpty()) {
			Swal.fire('Please ! Enter Description');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAdmissionProcedure = function () {
		if ($scope.IsValidAdmissionProcedure() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAdmissionProcedure.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAdmissionProcedure();
					}
				});
			} else
				$scope.CallSaveUpdateAdmissionProcedure();

		}
	};

	$scope.CallSaveUpdateAdmissionProcedure = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newAdmissionProcedure.PhotoAdmission_TMP;


		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveAdmissionProcedure",
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

			data: { jsonData: $scope.newAdmissionProcedure, files: filesColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearAdmissionProcedure();
				$scope.GetAllAdmissionProcedureList();

			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAdmissionProcedureById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			AdmissionProcedureId: refData.AdmissionProcedureId
		};
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAdmissionProcedureById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAdmissionProcedure = res.data.Data;
				$scope.newAdmissionProcedure.Mode = 'Modify';

				document.getElementById('admission-pro-listing').style.display = "none";
				document.getElementById('admission-procedure-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAdmissionProcedureById = function (refData) {
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
					AdmissionProcedureId: refData.AdmissionProcedureId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelAdmissionProcedure",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAdmissionProcedureList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

	$scope.GetAllAdmissionProcedureList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AdmissionProcedureList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllAdmissionProcedureList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AdmissionProcedureList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};
});