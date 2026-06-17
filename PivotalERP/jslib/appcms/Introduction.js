app.controller('IntroductionController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Introduction';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			VisionStatement: 1,
			FounderMessage: 1,
			StaffHierarchy: 1

		};

		$scope.searchData = {
			VisionStatement: '',
			FounderMessage: '',
			StaffHierarchy: ''

		};

		$scope.perPage = {
			VisionStatement: GlobalServices.getPerPageRow(),
			FounderMessage: GlobalServices.getPerPageRow(),
			StaffHierarchy: GlobalServices.getPerPageRow()

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

		$scope.newFounderMessage = {
			FounderMessageId: null,
			MessageFromId: null,
			Title: '',
			OrderNo: 0,
			Description: '',

			Mode: 'Save'
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
			Mode: 'Save'
		};
		$scope.GetAllVisionStatementList();
		$scope.GetAllFounderMessageList();
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


		document.getElementById('rules-form').style.display = "none";
		document.getElementById('message-form').style.display = "none";
		document.getElementById('sh-form').style.display = "none";


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

	$scope.ClearFounderMessage = function ()
	{
		$scope.ClearFounderMessagePhotoFounder();
		$('input[type=file]').val('');
		$scope.newFounderMessage = {
			FounderMessageId: null,
			MessageFromId: null,
			Title: '',
			OrderNo: 0,
			Description: '',

			Mode: 'Save'
		};
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


	$scope.ClearStaffHierarchyPhoto = function () {
		$scope.newStaffHierarchy.PhotoData = null;
		$scope.newStaffHierarchy.Photo_TMP = [];
		$scope.newStaffHierarchy.ImagePath ='';

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};


	//************************* VisionStatement *********************************

	$scope.IsValidVisionStatement = function () {
		if ($scope.newVisionStatement.Title.isEmpty()) {
			Swal.fire('Please ! Enter Title');
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
			data: { jsonData: $scope.newVisionStatement, files: filesColl  }
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

	//************************* Founder Message *********************************
	$scope.IsValidFounderMessage = function () {
		if ($scope.newFounderMessage.Title.isEmpty()) {
			Swal.fire('Please ! Enter Title');
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


	//************************* Staff Hierarchy *********************************

	$scope.IsValidStaffHierarchy = function () {
		if ($scope.newStaffHierarchy.FullName.isEmpty()) {
			Swal.fire('Please ! Enter Title');
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