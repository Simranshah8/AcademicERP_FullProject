app.controller('FounderMessageController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Founder Message';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			FounderMessage: 1
		};

		$scope.searchData = {
			FounderMessage: ''
		};

		$scope.perPage = {
			FounderMessage: GlobalServices.getPerPageRow()

		};



		$scope.newFounderMessage = {
			FounderMessageId: null,
			MessageFromId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			FounderSocialMediaColl:[],
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
					$scope.newFounderMessage.FounderSocialMediaColl.push({
						OrderNo: sm.OrderNo,
						Name: sm.Name,
						IconPath: sm.IconPath,
						SocialMediaId: sm.SocialMediaId,
						UrlPath: sm.UrlPath,
						IsActive:true,
					});
				});
			}  
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		 
		$scope.GetAllFounderMessageList();
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
		document.getElementById('message-form').style.display = "none";


		// Founder Message
		document.getElementById('add-message-form').onclick = function () {
			$scope.ClearFounderMessage();
			document.getElementById('message-table-listing').style.display = "none";
			document.getElementById('message-form').style.display = "block";
		}
		document.getElementById('back-to-message-list').onclick = function () {
			$scope.ClearFounderMessage();
			document.getElementById('message-table-listing').style.display = "block";
			document.getElementById('message-form').style.display = "none";
		}

		
	}



	$scope.ClearFounderMessage = function () {
		$scope.ClearFounderMessagePhotoFounder();
		$('input[type=file]').val('');
		$scope.newFounderMessage = {
			FounderMessageId: null,
			MessageFromId: null,
			Title: '',
			OrderNo: 0,
			Description: '',
			FounderSocialMediaColl: [],
			Mode: 'Save'
		};
		angular.forEach($scope.SocialMediaList, function (sm) {
			$scope.newFounderMessage.FounderSocialMediaColl.push({
				OrderNo: sm.OrderNo,
				Name: sm.Name,
				IconPath: sm.IconPath,
				SocialMediaId: sm.SocialMediaId,
				UrlPath: sm.UrlPath,
				IsActive: true,
			});
		});

	}



	$scope.ClearFounderMessagePhotoFounder = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newFounderMessage.PhotoDataFounder = null;
				$scope.newFounderMessage.PhotoFounder_TMP = [];
				$scope.newFounderMessage.ImagePath = '';
			});

		});
		$('#imgFounder').attr('src', '');
		$('#imgFounder1').attr('src', '');
	};





	//************************* Founder Message *********************************
	$scope.IsValidFounderMessage = function () {
		if ($scope.newFounderMessage.Title.isEmpty()) {
			Swal.fire('Please ! Enter Heading');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateFounderMessage = function () {
		if ($scope.IsValidFounderMessage() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newFounderMessage.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateFounderMessage();
					}
				});
			} else
				$scope.CallSaveUpdateFounderMessage();

		}
	};

	$scope.CallSaveUpdateFounderMessage = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var filesColl = $scope.newFounderMessage.PhotoFounder_TMP;

		if ($scope.newFounderMessage.FounderSocialMediaColl) {
			$scope.newFounderMessage.FounderSocialMediaColl.forEach(function (mm) {
				mm.UrlPath = mm.URLPath;
			});
        }
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveFounderMessage",
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
			data: { jsonData: $scope.newFounderMessage, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearFounderMessage();
				$scope.GetAllFounderMessageList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllFounderMessageList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FounderMessageList = [];

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllFounderMessageList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FounderMessageList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetFounderMessageById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			FounderMessageId: refData.FounderMessageId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetFounderMessageById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFounderMessage = res.data.Data;
				$scope.newFounderMessage.Mode = 'Modify';

				var query = mx($scope.newFounderMessage.FounderSocialMediaColl);
				var newList = [];
				angular.forEach($scope.SocialMediaList, function (sm)
				{
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

				$scope.newFounderMessage.FounderSocialMediaColl = newList;

				document.getElementById('message-table-listing').style.display = "none";
				document.getElementById('message-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelFounderMessageById = function (refData) {

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
					FounderMessageId: refData.FounderMessageId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelFounderMessage",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFounderMessageList();
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