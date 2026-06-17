app.controller('AcademicProgramsController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Notice';

	OnClickDefault();
	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			AcademicPrograms: 1
		};

		$scope.searchData = {
			AcademicPrograms: ''
		};

		$scope.perPage = {
			AcademicPrograms: GlobalServices.getPerPageRow()

		};
			

		$scope.newAcademicPrograms = {
			AcademicProgramsId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			Content: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};


		$scope.GetAllAcademicProgramsList();
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
		document.getElementById('acad-prog-form').style.display = "none";

		
		// Academic Programs
		document.getElementById('add-acad-prog').onclick = function () {

			$scope.ClearAcademicPrograms();
			document.getElementById('acad-prog-table-listing').style.display = "none";
			document.getElementById('acad-prog-form').style.display = "block";
		}
		document.getElementById('back-to-acad-prog-list').onclick = function () {
			$scope.ClearAcademicPrograms();
			document.getElementById('acad-prog-table-listing').style.display = "block";
			document.getElementById('acad-prog-form').style.display = "none";
		}
	};

	//$scope.ClearEmpProfile = function () {
		
	//	$scope.newAcademicPrograms = {
	//		AcademicProgramsId: null,
	//		Title: '',
	//		OrderNo: 0,
	//		Description: '',
	//		Content: '',
	//		Photo: null,
	//		PhotoPath: null,
	//		Mode: 'Save'
	//	};
	//	$scope.ClearAcademicProgramsPhotoAcadImage();
	//}

	//Academic Program Tab Js Starts From Here	
	$scope.ClearAcademicProgramsPhotoAcadImage = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newAcademicPrograms.PhotoDataAcadImage = null;
				$scope.newAcademicPrograms.PhotoAcadImage_TMP = [];
				$scope.newAcademicPrograms.ImagePath = '';
			});

		});
		$('#imgAcadImage').attr('src', '');
		$('#imgAcadImage1').attr('src', '');
	};

	
	$scope.ClearAcademicPrograms = function () {
		$scope.ClearAcademicProgramsPhotoAcadImage();
		$scope.newAcademicPrograms = {
			AcademicProgramsId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			Content: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};
	}

	$scope.IsValidAcademicPrograms = function () {
		if ($scope.newAcademicPrograms.Title.isEmpty()) {
			Swal.fire('Please ! Enter Heading');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAcademicPrograms = function () {
		if ($scope.IsValidAcademicPrograms() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAcademicPrograms.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAcademicPrograms();
					}
				});
			} else
				$scope.CallSaveUpdateAcademicPrograms();

		}
	};

	$scope.CallSaveUpdateAcademicPrograms = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newAcademicPrograms.PhotoAcadImage_TMP;

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveAcademicProgram",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						if (data.files[i].File)
							formData.append("photo", data.files[i].File);
						else
							formData.append("photo", data.files[i]);
					}
				}
				return formData;
			},

			data: { jsonData: $scope.newAcademicPrograms, files: filesColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearAcademicPrograms();

				$scope.GetAllAcademicProgramsList();

			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllAcademicProgramsList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AcademicProgramsList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllAcademicProgramList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AcademicProgramsList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAcademicProgramsById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			AcademicProgramId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAcademicProgramById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAcademicPrograms = res.data.Data;
				$scope.newAcademicPrograms.Mode = 'Modify';

				document.getElementById('acad-prog-table-listing').style.display = "none";
				document.getElementById('acad-prog-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAcademicProgramsById = function (refData) {

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
					AcademicProgramId: refData.TranId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelAcademicProgram",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAcademicProgramsList();
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