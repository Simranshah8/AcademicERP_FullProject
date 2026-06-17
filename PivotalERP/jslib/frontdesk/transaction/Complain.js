
app.controller('ComplainController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Complain';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.LanguageColl = GlobalServices.getLangList();
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		$scope.Statuslist = [{ id: 1, text: 'Action Taken' }, { id: 2, text: 'Action Not Taken' }];
		$scope.currentPages = {
			Complain: 1,
		};

		$scope.searchData = {
			Complain: '',
		};

		$scope.perPage = {
			Complain: GlobalServices.getPerPageRow(),
		};

		$scope.ComplainTypeList = [];	
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllComplainTypeList",
			dataType: "json"			
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ComplainTypeList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	    $scope.SourceList = [];
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllSourceList",
			dataType: "json"
		    }).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SourceList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.DocumentTypeList = [];
		GlobalServices.getDocumentTypeList().then(function (res) {
			$scope.DocumentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.newFilter = {			
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date()

		};

		$scope.newComplain = {
			ComplainId: null,
			ComplainDate_TMP: new Date(),
			ComplainTypeId: null,
			SourceId: null,
			ComplainBy: '',
			PhoneNo: null,
			AssignToId: null,
			ActionTaken: '',
			Remarks: '',
			ComplainBy: 1,
			AttachmentColl: [],
			SelectStudent: $scope.StudentSearchOptions[0].value,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			Description: '',
			Mode: 'Save'
		};			
	};

	$scope.ClearComplain = function () {
		$scope.newComplain = {
			ComplainId: null,
			ComplainDate_TMP: new Date(),
			ComplainTypeId: null,
			SourceId: null,
			ComplainBy: '',
			PhoneNo: null,
			AssignToId: null,
			ActionTaken: '',
			Remarks: '',
			ComplainBy: 1,
			AttachmentColl: [],
			SelectStudent: $scope.StudentSearchOptions[0].value,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			Description: '',
			Mode: 'Save'
		};
	};

	function OnClickDefault() {
		document.getElementById('complain-form').style.display = "none";	

		document.getElementById('add-complain-btn').onclick = function () {
			document.getElementById('complain-listing').style.display = "none";
			document.getElementById('complain-form').style.display = "block";
			$scope.ClearClass();
		}

		document.getElementById('back-complain-list').onclick = function () {
			document.getElementById('complain-listing').style.display = "block";
			document.getElementById('complain-form').style.display = "none";
			$scope.ClearClass();
		}
	};

	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newComplain.AttachmentColl) {
			if ($scope.newComplain.AttachmentColl.length > 0) {
				$scope.newComplain.AttachmentColl.splice(ind, 1);
			}
		}
	}

	//$scope.AddMoreFiles = function (files, docType, des) {
	//	if (files && docType) {
	//		if (files != null && docType != null) {
	//			angular.forEach(files, function (file) {
	//				$scope.newComplain.AttachmentColl.push({
	//					DocumentTypeId: docType.id,
	//					DocumentTypeName: docType.text,
	//					File: file,
	//					Name: file.name,
	//					Type: file.type,
	//					Size: file.size,
	//					Description: des,
	//					Path: null
	//				});
	//			})
	//			$scope.docType = null;
	//			$scope.attachFile = null;
	//			$scope.docDescription = '';
	//			$('#flMoreFiles').val('');
	//		}
	//	}
	//};

	$scope.AddMoreFiles = function (files, docType, des) {
		if (files && docType) {
			var allowedTypes = [
				'image/png',
				'image/gif',
				'image/jpeg',
				'image/jpg',
				'application/pdf',
				'application/msword'
			];

			angular.forEach(files, function (file) {
				if (!allowedTypes.includes(file.type)) {
					Swal.fire({
						icon: 'warning',
						title: 'Invalid File Type',
						text: 'Only PNG, GIF, JPG, JPEG, PDF, and DOC files are allowed.'
					});
					return;
				}

				// Validate file size <= 1 MB
				if (file.size > 1048576) {
					Swal.fire({
						icon: 'warning',
						title: 'File Too Large',
						text: 'Each file must be 1 MB or smaller.'
					});
					return;
				}

				// Add valid file
				$scope.newComplain.AttachmentColl.push({
					DocumentTypeId: docType.id,
					DocumentTypeName: docType.text,
					File: file,
					Name: file.name,
					Type: file.type,
					Size: file.size,
					Description: des,
					Path: null
				});
			});

			// Reset inputs
			$scope.docType = null;
			$scope.attachFile = null;
			$scope.docDescription = '';
			$('#flMoreFiles').val('');
		}
	};




	$scope.ShowPersonalImg = function (item) {
		$scope.viewImg = {
			ContentPath: '',
			File: null
		};
		if (item.DocPath && item.DocPath.length > 0) {
			$scope.viewImg.ContentPath = item.DocPath;
			$('#PersonalImg').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg.ContentPath = item.PhotoPath;
			$('#PersonalImg').modal('show');
		} else if (item.File) {
			$scope.viewImg.File = item.File;
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg.ContentPath = URL.createObjectURL(blob);

			$('#PersonalImg').modal('show');
		}

		else
			Swal.fire('No Image Found');

	};
 //************************* Complain *********************************

	$scope.IsValidComplain = function () {
		//if ($scope.newComplain.ComplainBy.isEmpty()) {
		//	Swal.fire('Please ! Enter ComplainBy');
		//	return false;
		//}
		return true;
	}

	$scope.SaveUpdateComplain = function () {
		if ($scope.IsValidComplain() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newComplain.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateComplain();
					}
				});
			} else
				$scope.CallSaveUpdateComplain();
		}
	};

	$scope.CallSaveUpdateComplain = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var UserPhoto = $scope.newComplain.Photo_TMP;
		var filesColl = $scope.newComplain.AttachmentColl;

		/*$scope.newComplain.AssignTo = $scope.newComplain.EmployeeDetails.Name;*/

		if ($scope.newComplain.ComplainBy == 1) {
			$scope.newComplain.EmployeeId = null;
		/*	$scope.newComplain.OthersName = '';*/

			if ($scope.newComplain.StudentDetails) {
				$scope.newComplain.Name = $scope.newComplain.StudentDetails.Name;
				$scope.newComplain.OthersName = $scope.newComplain.StudentDetails.RegdNo + " : " + $scope.newComplain.StudentDetails.Name + ": " + $scope.newComplain.StudentDetails.ClassName + " " + $scope.newComplain.StudentDetails.SectionName + " : " + $scope.newComplain.StudentDetails.RollNo;
			}
		} else if ($scope.newComplain.ComplainBy == 2) {
			$scope.newComplain.StudentId = null;
		/*	$scope.newComplain.OthersName = '';*/

			if ($scope.newComplain.EmployeeDetails) {
				$scope.newComplain.Name = $scope.newComplain.EmployeeDetails.Name;
				$scope.newComplain.OthersName = $scope.newComplain.EmployeeDetails.Code + " : " + $scope.newComplain.EmployeeDetails.Name + ": " + $scope.newComplain.EmployeeDetails.Address + " " + $scope.newComplain.EmployeeDetails.MobileNo;
			}

		}
		else if ($scope.newComplain.ComplainBy == 3) {
			$scope.newComplain.EmployeeId = null;
			$scope.newComplain.StudentId = null;
			$scope.newComplain.OthersName = $scope.newComplain.OthersName;
		}


		if ($scope.newComplain.ComplainDateDet) {
			$scope.newComplain.ComplainDate = $filter('date')(new Date($scope.newComplain.ComplainDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newComplain.ComplainDate = new Date();

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveUpdateComplain",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				if (data.UsPhoto && data.UsPhoto.length > 0)
					formData.append("UserPhoto", data.UsPhoto[0]);

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}
				return formData;
			},
			data: { jsonData: $scope.newComplain, files: filesColl, UsPhoto: UserPhoto }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearComplain();
				/*$scope.GetAllComplainList();*/
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllComplainList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ComplainList = [];
		var para = {
			dateFrom: ($scope.newFilter.FromDateDet ? $filter('date')(new Date($scope.newFilter.FromDateDet.dateAD), 'yyyy-MM-dd') : null),
			dateTo: ($scope.newFilter.ToDateDet ? $filter('date')(new Date($scope.newFilter.ToDateDet.dateAD), 'yyyy-MM-dd') : null),
			SourceId: $scope.newFilter.SourceId,
			ComplainTypeId: $scope.newFilter.ComplainTypeId,
			StatusId: $scope.newFilter.StatusId
		};
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllComplain",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ComplainList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetComplainById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			ComplainId: refData.ComplainId
		};
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/getComplainById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newComplain = res.data.Data;
				$scope.newComplain.Mode = 'Save';


				if ($scope.newComplain.ComplainDate)
					$scope.newComplain.ComplainDate_TMP = new Date($scope.newComplain.ComplainDate);

				$scope.newComplain.SelectStudent = $scope.StudentSearchOptions.length > 0 ? $scope.StudentSearchOptions[0].value : null;
				$scope.newComplain.SelectEmployee = $scope.EmployeeSearchOptions.length > 0 ? $scope.EmployeeSearchOptions[0].value : null;


				document.getElementById('complain-listing').style.display = "none";
				document.getElementById('complain-form').style.display = "block";
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelComplainById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { ComplainId: refData.ComplainId };
				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DeleteComplain",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetAllComplainList();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	}	

	$scope.newDet = {};
	$scope.openReplyModal = function (cl) {
		$scope.newDet.ComplainId = cl.ComplainId;
		$('#replymodal').modal('show');
	}

	$scope.SaveComplainReply = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveComplainReply",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newDet }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.GetAllComplainList();
				/*$scope.ClearAssign();*/
				/*$scope.RefreshTicketHis();*/
				$('#replymodal').modal('hide');
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.validateDate = function (obj, startField, endField, startLabel, endLabel) {
		var res = GlobalServices.validateDate(obj, startField, endField, startLabel, endLabel);
		if (res.IsSuccess == false) {
			Swal.fire({
				icon: 'warning',
				text: res.Message,
				confirmButtonText: 'OK'
			}).then(function () {
				obj.FromDate_TMP = new Date();
				obj.ToDate_TMP = new Date();
				$scope.$applyAsync();
			});
		}
	};

});
