app.controller('StaffHeirarchyController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Staff Heirarchy';
	OnClickDefault();
	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			StaffHierarchy: 1

		};

		$scope.searchData = {
			StaffHierarchy: ''

		};

		$scope.perPage = {
			StaffHierarchy: GlobalServices.getPerPageRow()
		};

		
		$scope.newStaffHierarchy = {
			StaffHierarchyId: null,
			FullName: '',
			Designation: '',
			Contact: 0,
			Email: '',
			Message: '',
			Photo: null,
			PhotoPath: null,
			SocialMediaColl: [],
			OrderNo:0,
			Mode: 'Save'
		};

		$scope.SocialMediaList = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllSocialMedia",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SocialMediaList = res.data.Data;

				angular.forEach($scope.SocialMediaList, function (sm) {
					$scope.newStaffHierarchy.SocialMediaColl.push({
						OrderNo: sm.OrderNo,
						Name: sm.Name,
						IconPath: sm.IconPath,
						SocialMediaId: sm.SocialMediaId,
						UrlPath: sm.UrlPath,
						IsActive: true,
					});
				});
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllStaffHierarchyList();
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
		document.getElementById('sh-form').style.display = "none";

		// Staff Heirarchy
		document.getElementById('add-member').onclick = function () {

			$scope.ClearStaffHierarchy();
			document.getElementById('sh-table-listing').style.display = "none";
			document.getElementById('sh-form').style.display = "block";
		}
		document.getElementById('back-to-staff-list').onclick = function () {
			$scope.ClearStaffHierarchy();
			document.getElementById('sh-table-listing').style.display = "block";
			document.getElementById('sh-form').style.display = "none";
		}

	}	

	$scope.ClearStaffHierarchy = function () {
		$scope.ClearStaffHierarchyPhoto();
		$('input[type=file]').val('');
		$scope.newStaffHierarchy = {
			StaffHierarchyId: null,
			FullName: '',
			Designation: '',
			Contact: 0,
			Email: '',
			Message: '',
			Photo: null,
			PhotoPath: null,
			SocialMediaColl: [],
			OrderNo:0,
			Mode: 'Save'
		};

		angular.forEach($scope.SocialMediaList, function (sm) {
			$scope.newStaffHierarchy.SocialMediaColl.push({
				OrderNo: sm.OrderNo,
				Name: sm.Name,
				IconPath: sm.IconPath,
				SocialMediaId: sm.SocialMediaId,
				UrlPath: sm.UrlPath,
				IsActive: true,
			});
		});
	}

	
	$scope.ClearStaffHierarchyPhoto = function () {
		$scope.newStaffHierarchy.PhotoData = null;
		$scope.newStaffHierarchy.Photo_TMP = [];
		$scope.newStaffHierarchy.ImagePath = '';

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};


	//************************* Staff Hierarchy *********************************

	$scope.IsValidStaffHierarchy = function () {
		if ($scope.newStaffHierarchy.FullName.isEmpty()) {
			Swal.fire('Please ! Enter Full Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateStaffHierarchy = function () {
		if ($scope.IsValidStaffHierarchy() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStaffHierarchy.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStaffHierarchy();
					}
				});
			} else
				$scope.CallSaveUpdateStaffHierarchy();

		}
	};

	$scope.CallSaveUpdateStaffHierarchy = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var filesColl = $scope.newStaffHierarchy.Photo_TMP;

		if ($scope.newStaffHierarchy.SocialMediaColl) {
			$scope.newStaffHierarchy.SocialMediaColl.forEach(function (mm) {
				mm.URLPath = mm.UrlPath;
			});
		}

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveStaffHierarchy",
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
			data: { jsonData: $scope.newStaffHierarchy, files: filesColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStaffHierarchy();
				$scope.GetAllStaffHierarchyList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllStaffHierarchyList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StaffHierarchyList = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllStaffHierarchyList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StaffHierarchyList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetStaffHierarchyById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			StaffHierarchyId: refData.StaffHierarchyId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetStaffHierarchyById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newStaffHierarchy = res.data.Data;
				$scope.newStaffHierarchy.Mode = 'Modify'; 

				var query = mx($scope.newStaffHierarchy.SocialMediaColl);
				var newList = [];
				angular.forEach($scope.SocialMediaList, function (sm) {
					var find = query.firstOrDefault(p1 => p1.SocialMediaId == sm.SocialMediaId);
					newList.push({
						OrderNo: sm.OrderNo,
						Name: sm.Name,
						IconPath: sm.IconPath,
						SocialMediaId: sm.SocialMediaId,
						UrlPath: (find ? find.UrlPath : sm.URLPath),
						URLPath: (find ? find.UrlPath : sm.URLPath),
						IsActive: (find ? find.IsActive : true),
					});
				});

				$scope.newStaffHierarchy.SocialMediaColl = newList;

				document.getElementById('sh-table-listing').style.display = "none";
				document.getElementById('sh-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelStaffHierarchyById = function (refData) {
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
					StaffHierarchyId: refData.StaffHierarchyId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelStaffHierarchy",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStaffHierarchyList();
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