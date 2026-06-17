app.controller('CommitteHeirarchyController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Committe Heirarchy';
	OnClickDefault();
	$scope.LoadData = function () {
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			CommitteHierarchy: 1

		};

		$scope.searchData = {
			CommitteHierarchy: ''

		};

		$scope.perPage = {
			CommitteHierarchy: GlobalServices.getPerPageRow()
		};

		
		$scope.newCommitteHierarchy = {
			CommitteHierarchyId: null,
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
					$scope.newCommitteHierarchy.SocialMediaColl.push({
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

		$scope.GetAllCommitteHierarchyList();
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

		// Committe Heirarchy
		document.getElementById('add-member').onclick = function () {

			$scope.ClearCommitteHierarchy();
			document.getElementById('sh-table-listing').style.display = "none";
			document.getElementById('sh-form').style.display = "block";
		}
		document.getElementById('back-to-Committe-list').onclick = function () {
			$scope.ClearCommitteHierarchy();
			document.getElementById('sh-table-listing').style.display = "block";
			document.getElementById('sh-form').style.display = "none";
		}

	}	

	$scope.ClearCommitteHierarchy = function () {
		$scope.ClearCommitteHierarchyPhoto();
		$('input[type=file]').val('');
		$scope.newCommitteHierarchy = {
			CommitteHierarchyId: null,
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
			$scope.newCommitteHierarchy.SocialMediaColl.push({
				OrderNo: sm.OrderNo,
				Name: sm.Name,
				IconPath: sm.IconPath,
				SocialMediaId: sm.SocialMediaId,
				UrlPath: sm.UrlPath,
				IsActive: true,
			});
		});
	}

	
	$scope.ClearCommitteHierarchyPhoto = function () {
		$scope.newCommitteHierarchy.PhotoData = null;
		$scope.newCommitteHierarchy.Photo_TMP = [];
		$scope.newCommitteHierarchy.ImagePath = '';

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};


	//************************* Committe Hierarchy *********************************

	$scope.IsValidCommitteHierarchy = function () {
		if ($scope.newCommitteHierarchy.FullName.isEmpty()) {
			Swal.fire('Please ! Enter Full Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateCommitteHierarchy = function () {
		if ($scope.IsValidCommitteHierarchy() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCommitteHierarchy.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCommitteHierarchy();
					}
				});
			} else
				$scope.CallSaveUpdateCommitteHierarchy();

		}
	};

	$scope.CallSaveUpdateCommitteHierarchy = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var filesColl = $scope.newCommitteHierarchy.Photo_TMP;

		if ($scope.newCommitteHierarchy.SocialMediaColl) {
			$scope.newCommitteHierarchy.SocialMediaColl.forEach(function (mm) {
				mm.URLPath = mm.UrlPath;
			});
		}

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveCommitteHierarchy",
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
			data: { jsonData: $scope.newCommitteHierarchy, files: filesColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCommitteHierarchy();
				$scope.GetAllCommitteHierarchyList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllCommitteHierarchyList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CommitteHierarchyList = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllCommitteHierarchyList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CommitteHierarchyList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetCommitteHierarchyById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			CommitteHierarchyId: refData.CommitteHierarchyId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetCommitteHierarchyById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCommitteHierarchy = res.data.Data;
				$scope.newCommitteHierarchy.Mode = 'Modify'; 

				var query = mx($scope.newCommitteHierarchy.SocialMediaColl);
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

				$scope.newCommitteHierarchy.SocialMediaColl = newList;

				document.getElementById('sh-table-listing').style.display = "none";
				document.getElementById('sh-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCommitteHierarchyById = function (refData) {
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
					CommitteHierarchyId: refData.CommitteHierarchyId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelCommitteHierarchy",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCommitteHierarchyList();
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