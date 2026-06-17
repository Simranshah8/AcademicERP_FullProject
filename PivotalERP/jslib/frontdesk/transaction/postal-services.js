app.controller('PostalServicesController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Setup';
	OnClickDefault();
	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			Received: 1,
			Dispatch: 1,

		};

		$scope.searchData = {
			Received: '',
			Dispatch: '',

		};

		$scope.perPage = {
			Received: GlobalServices.getPerPageRow(),
			Dispatch: GlobalServices.getPerPageRow(),

		};

		$scope.newReceivedSearch = {
			DateFrom_TMP: new Date(),
			DateTo_TMP:new Date(),
		}
		$scope.newDispatchSearch = {
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date(),
		}

		$scope.newReceived = {
			PostalServicesId: null,
			FormTitle: '',
			ReferenceNumber: '',
			Address: '',
			Totitle: '',
			Date: '',
			Remarks: '',
			Photo: null,
			PhotoPath: null,
			Fromdate: new Date(),
			Todate: new Date(),
			FromDateDet:null,
			TypeOfDocument: '',
			AttachmentColl: [],
			Date_TMP:new Date(),
			//AttachmentColl: [],
			Mode: 'Save'
		};


		$scope.newDispatch = {
			PostalDispatchId: null,
			Totitle: '',
			ReferenceNumber: '',
			Address: '',
			FromTitle: '',
			Dates: '',
			Miti:'',
			Remarks: '',
			dateFrom: '',
			dateTo: '',
			Date_TMP: new Date(),
			AttachmentColl: [],
			Mode: 'Save'
		};

		$scope.GetAllReceivedList();
		$scope.GetAllDispatchList();
	
		$scope.DocumentTypeList = [];
		GlobalServices.getDocumentTypeList().then(function (res) {
			$scope.DocumentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	}

	function OnClickDefault() {
		document.getElementById('postal-dispatch-form').style.display = "none";
		document.getElementById('postal-received-form').style.display = "none";

		// Postal Dispatch
		document.getElementById('add-postal-dispatch').onclick = function () {
			document.getElementById('postdispatch-section').style.display = "none";
			document.getElementById('postal-dispatch-form').style.display = "block";
		}
		document.getElementById('postaldispatchback-btn').onclick = function () {
			document.getElementById('postal-dispatch-form').style.display = "none";
			document.getElementById('postdispatch-section').style.display = "block";
		}

		// Postal Received
		document.getElementById('add-postal-received').onclick = function () {
			document.getElementById('postreceived-section').style.display = "none";
			document.getElementById('postal-received-form').style.display = "block";
		}
		document.getElementById('postalback-btn').onclick = function () {
			document.getElementById('postal-received-form').style.display = "none";
			document.getElementById('postreceived-section').style.display = "block";
		}
	}

	$scope.ClearReceived = function () {

		$('input[type=file]').val('');
		$scope.newReceived = {
			PostalServicesId: null,
			FormTitle: '',
			ReferenceNumber: '',
			Address: '',
			Totitle: '',
			Date: '',
			Remarks: '',
			Date_TMP:new Date(),
			//AttachmentColl: [],
			Mode: 'Save'
		};
	}
	$scope.ClearDispatch = function () {
		$('input[type=file]').val('');
		$scope.newDispatch = {
			PostalDispatchId: null,
			Totitle: '',
			ReferenceNumber: '',
			Address: '',
			FromTitle: '',
			Date: '',
			Remarks: '',
			AttachmentColl: [],
			Mode: 'Save'
		};
	}


	//************************* Postal Received *********************************

	$scope.IsValidReceived = function () {
		if ($scope.newReceived.FromTitle.isEmpty()) {
			Swal.fire('Please ! Enter From Title');
			return false;
		}

		if ($scope.newReceived.Totitle.isEmpty()) {
			Swal.fire('Please !To Title');
			return false;
		}

		if ($scope.newReceived.Remarks.isEmpty()) {
			Swal.fire('Please !Enter Remarks');
			return false;
		}

		return true;
	}


	$scope.delAttachmentFilesReceived = function (ind) {
		if ($scope.newReceived.AttachmentColl) {
			if ($scope.newReceived.AttachmentColl.length > 0) {
				$scope.newReceived.AttachmentColl.splice(ind, 1);
			}
		}
	}
	//$scope.AddMoreFilesReceived = function (files, docType, des) {

	//	if (files && docType) {
	//		if (files != null && docType != null) {

	//			angular.forEach(files, function (file) {
	//				$scope.newReceived.AttachmentColl.push({
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
	//		}
	//	}
	//};

	$scope.AddMoreFilesReceived = function (files, docType, des) {
		if (files && docType) {
			// Allowed MIME types
			var allowedTypes = [
				'image/png',
				'image/gif',
				'image/jpeg',
				'image/jpg',
				'application/pdf',
				'application/msword'
			];

			angular.forEach(files, function (file) {
				// Check file type
				if (!allowedTypes.includes(file.type)) {
					Swal.fire({
						icon: 'warning',
						title: 'Invalid File Type',
						text: 'Only PNG, GIF, JPG, JPEG, PDF, and DOC files are allowed.'
					});
					return; // skip this file
				}

				// Check file size <= 1 MB
				if (file.size > 1048576) {
					Swal.fire({
						icon: 'warning',
						title: 'File Too Large',
						text: 'Each file must be 1 MB or smaller.'
					});
					return; // skip this file
				}

				// Add file to collection
				$scope.newReceived.AttachmentColl.push({
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
			$('#flMoreFiles').val(''); // clear file input
		}
	};



	$scope.SaveUpdateReceived = function () {
		if ($scope.IsValidReceived() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newReceived.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateReceived();
					}
				});
			} else
				$scope.CallSaveUpdateReceived();

		}
	};
 
	$scope.CallSaveUpdateReceived = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		if ($scope.newReceived.DateDet) {
			$scope.newReceived.Date = $filter('date')(new Date($scope.newReceived.DateDet.dateAD), 'yyyy-MM-dd');
		}else if($scope.newReceived.Date_TMP)
			$scope.newReceived.Date = $filter('date')(new Date($scope.newReceived.Date_TMP), 'yyyy-MM-dd');
		else
			$scope.newReceived.Date = null;

		var filesColl = $scope.newReceived.AttachmentColl;
	

		var photo = $scope.newReceived.Photo;



		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SavePivotalServices",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}

				if (data.stPhoto && data.stPhoto.length > 0)
					formData.append("photo", data.stPhoto[0]);

				return formData;
			},

			data: { jsonData: $scope.newReceived, stPhoto: photo, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearReceived();
				$scope.GetAllReceivedList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};
	 
	$scope.GetAllReceivedList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.ReceivedList = [];

		var para = {
			fromDate: ($filter('date')(new Date(($scope.newReceivedSearch.DateFromDet ? $scope.newReceivedSearch.DateFromDet.dateAD :new Date()) ), 'yyyy-MM-dd')),
			toDate: ($filter('date')(new Date(($scope.newReceivedSearch.DateToDet ?  $scope.newReceivedSearch.DateToDet.dateAD : new Date()) ), 'yyyy-MM-dd')),
		};
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllReceivedList",
			dataType: "json",
			data:JSON.stringify(para)		
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ReceivedList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetReceivedById = function (refData) {

		$scope.ClearReceived();

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			PostalServicesId: refData.PostalServicesId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetPostalServiceById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newReceived = res.data.Data;
				$scope.newReceived.Mode = 'Modify';

				$scope.newReceived.Date_TMP = new Date(res.data.Data.Date);

				document.getElementById('postreceived-section').style.display = "none";
				document.getElementById('postal-received-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelReceivedById = function (refData) {
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
					PostalServicesId: refData.PostalServicesId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelReceived",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllReceivedList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//*************************Postal Dispatch *********************************

	$scope.IsValidDispatch = function () {
		if ($scope.newDispatch.Totitle.isEmpty()) {
			Swal.fire('Please ! Enter To Title');
			return false;
		}

	
		if ($scope.newDispatch.Address.isEmpty()) {
			Swal.fire('Please ! Enter Address');
			return false;
		}

		if ($scope.newDispatch.FromTitle.isEmpty()) {
			Swal.fire('Please ! Enter From Title');
			return false;
		}

		if ($scope.newDispatch.Remarks.isEmpty()) {
			Swal.fire('Please ! Enter Remarks');
			return false;
		}

		return true;
	}




	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newDispatch.AttachmentColl) {
			if ($scope.newDispatch.AttachmentColl.length > 0) {
				$scope.newDispatch.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newDispatch.AttachmentColl.push({
						DocumentTypeId: docType.id,
						DocumentTypeName: docType.text,
						File: file,
						Name: file.name,
						Type: file.type,
						Size: file.size,
						Description: des,
						Path: null
					});
				})

				$scope.docType = null;
				$scope.attachFile = null;
				$scope.docDescription = '';
			}
		}
	};
	//$scope.AddMoreFiles1 = function (files, docType, des) {

	//	if (files && docType) {
	//		if (files != null && docType != null) {

	//			angular.forEach(files, function (file) {
	//				$scope.newDispatch.AttachmentColl.push({
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
	//			$scope.docType1 = null;
	//			$scope.attachFile1 = null;
	//			$scope.docDescription1 = '';
	//		}
	//	}
	//};
	$scope.AddMoreFiles1 = function (files, docType, des) {
		if (files && docType) {
			// Allowed MIME types
			var allowedTypes = [
				'image/png',
				'image/gif',
				'image/jpeg',
				'image/jpg',
				'application/pdf',
				'application/msword'
			];

			angular.forEach(files, function (file) {
				// Check file type
				if (!allowedTypes.includes(file.type)) {
					Swal.fire({
						icon: 'warning',
						title: 'Invalid File Type',
						text: 'Only PNG, GIF, JPG, JPEG, PDF, and DOC files are allowed.'
					});
					return; // skip this file
				}

				// Check file size <= 1 MB
				if (file.size > 1048576) {
					Swal.fire({
						icon: 'warning',
						title: 'File Too Large',
						text: 'Each file must be 1 MB or smaller.'
					});
					return; // skip this file
				}

				// Add file to collection
				$scope.newDispatch.AttachmentColl.push({
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
			$scope.docType1 = null;
			$scope.attachFile1 = null;
			$scope.docDescription1 = '';
			$('#flMoreFiles1').val(''); // clear file input
		}
	};



	$scope.SaveUpdateDispatch = function () {
		if ($scope.IsValidDispatch() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDispatch.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDispatch();
					}
				});
			} else
				$scope.CallSaveUpdateDispatch();

		}
	};

	$scope.CallSaveUpdateDispatch = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newDispatch.DateDet) {
			$scope.newDispatch.Date = $filter('date')(new Date($scope.newDispatch.DateDet.dateAD), 'yyyy-MM-dd');
		} else if ($scope.newDispatch.Date_TMP)
			$scope.newDispatch.Date = $filter('date')(new Date($scope.newDispatch.Date_TMP), 'yyyy-MM-dd');
		else
			$scope.newDispatch.Date = null;

		var filesColl = $scope.newDispatch.AttachmentColl;


		var photo = $scope.newDispatch.Photo;



		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveDispatchServices",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.files) {
					for (var i = 0; i < data.files.length; i++) {
						formData.append("file" + i, data.files[i].File);
					}
				}

				if (data.stPhoto && data.stPhoto.length > 0)
					formData.append("photo", data.stPhoto[0]);

				return formData;
			},

			data: { jsonData: $scope.newDispatch, stPhoto: photo, files: filesColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearDispatch();
				$scope.GetAllDispatchList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

		 
	}

	$scope.GetAllDispatchList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
	/*	var para = {
			
			dateFrom: $scope.newDispatch.FromDateDet.dateAD,
			dateTo: $scope.newDispatch.ToDateDet.dateAD
		}*/
		$scope.DispatchList = [];

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllDispatchedList",
			dataType: "json",
			//data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DispatchList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetDispatchById = function (refData) {

		$scope.ClearDispatch();
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			PostalDispatchId: refData.PostalDispatchId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetPostalDispatchById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDispatch = res.data.Data;
				$scope.newDispatch.Mode = 'Modify';

				$scope.newDispatch.Date_TMP = new Date($scope.newDispatch.Date);
				document.getElementById('postdispatch-section').style.display = "none";
				document.getElementById('postal-dispatch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelDispatchById = function (refData) {

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
					PostalDispatchId: refData.PostalDispatchId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelPostalDispatch",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllDispatchList();
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
	$scope.ShowDoc = function (item) {
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

	$scope.validateDatesRcv = function (changedField) {
		if (!$scope.newReceivedSearch.DateFromDet || !$scope.newReceivedSearch.DateToDet ||
			!$scope.newReceivedSearch.DateFromDet.dateAD || !$scope.newReceivedSearch.DateToDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newReceivedSearch.DateFromDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newReceivedSearch.DateToDet.dateAD), 'yyyy-MM-dd')
		
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be greater than To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newReceivedSearch.DateFrom_TMP = new Date();
						$scope.newReceivedSearch.DateFromDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be less than From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newReceivedSearch.DateTo_TMP = new Date();
						$scope.newReceivedSearch.DateToDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};

	$scope.validateDatesDispatch = function (changedField) {
		if (!$scope.newDispatch.FromDateDet || !$scope.newDispatch.ToDateDet ||
			!$scope.newDispatch.FromDateDet.dateAD || !$scope.newDispatch.ToDateDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newDispatch.FromDateDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newDispatch.ToDateDet.dateAD), 'yyyy-MM-dd')
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be greater than To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newDispatch.dateFrom = new Date();
						$scope.newDispatch.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be less than From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newDispatch.dateTo = new Date();
						$scope.newDispatch.ToDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};
});