app.controller('VisitorController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Book Entry';

	LoadData();
	OnClickDefault();
	function LoadData() {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();
		

		$scope.currentPages = {
			Visitor: 1,
		};
		$scope.searchData = {
			Visitor: '',
		};
		$scope.perPage = {
			Visitor: GlobalServices.getPerPageRow(),
		};
		  
		$scope.newVisitor = {
			VisitorId: null,
			Name: '',
			Address: '',
			Contact: '',
			Email: '',
			VisitorName: '',
			Purpose: '',
			InTime_TMP: '',
			ValidityTime: null,
			OutTime: null,
			Remarks: '',
			TypeOfDocumentId: null,
			AttachDocument: '',
			Description: '',
			MeeTo: 1,
			AttachmentColl: [],
			SelectStudent: $scope.StudentSearchOptions[0].value,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),		 
			Mode: 'Save'
		};
		//$scope.GetAllVisitorList();


	};

	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newVisitor.AttachmentColl) {
			if ($scope.newVisitor.AttachmentColl.length > 0) {
				$scope.newVisitor.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {
		$scope.newVisitor.AttachmentColl;
		if (files && docType) {
			if (files != null && docType != null) {
				angular.forEach(files, function (file) {
					$scope.newVisitor.AttachmentColl.push({
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
				$('#flMoreFiles').val('');
			}
		}
	};
	$scope.ClearVisitor = function () {

		$timeout(function () {
			$scope.newVisitor = {
				VisitorId: null,
				Name: '',
				Address: '',
				Contact: '',
				Email: '',
				VisitorName: '',
				Purpose: '',
				InTime_TMP: '',
				ValidityTime: null,
				OutTime: null,
				Remarks: '',
				TypeOfDocumentId: null,
				AttachDocument: '',
				Description: '',
				MeeTo: 1,
				AttachmentColl: [],
				SelectStudent: $scope.StudentSearchOptions[0].value,
				SelectEmployee: $scope.EmployeeSearchOptions[0].value,
				FromDate_TMP: new Date(),
				ToDate_TMP: new Date(),
				Mode: 'Save'
			};
		});
		
		
		
	};


	function OnClickDefault() {
		document.getElementById('enquiry-form').style.display = "none";

		$scope.open_form_btn = function () {
			document.getElementById('table-listing').style.display = "none";
			document.getElementById('enquiry-form').style.display = "block";
			$scope.ClearVisitor();
		}

		$scope.back_to_list = function () {
			document.getElementById('table-listing').style.display = "block";
			document.getElementById('enquiry-form').style.display = "none";
			$scope.ClearVisitor();
		}
	};
	//Clear photo
	$scope.ClearVisitorPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newVisitor.PhotoData = null;
				$scope.newVisitor.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};




	$scope.IsValidVisitor = function () {
		if ($scope.newVisitor.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}

		if ($scope.newVisitor.Address.isEmpty()) {
			Swal.fire('Please ! Enter Address');
			return false;
		}
		if ($scope.newVisitor.Contact.isEmpty()) {
			Swal.fire('Please ! Enter Contact');
			return false;
		}

		if ($scope.newVisitor.Purpose.isEmpty()) {
			Swal.fire('Please ! Enter Visiting Purpose');
			return false;
		}
		if (
			$scope.newVisitor.InTime_TMP === null ||
			$scope.newVisitor.InTime_TMP === undefined ||
			$scope.newVisitor.InTime_TMP === ''
		) {
			Swal.fire('Please ! Enter In Time');
			return false;
		}


		return true;
	};

	$scope.SaveUpdateVisitor = function () {
		if ($scope.IsValidVisitor() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newVisitor.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateVisitor();
					}
				});
			} else
				$scope.CallSaveUpdateVisitor();

		}
	};

	$scope.CallSaveUpdateVisitor = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newVisitor.Photo_TMP) {
			var photo = $scope.newVisitor.Photo_TMP;
			//$scope.newVisitor.Photo = $scope.newVisitor.PhotoData; 
		}
		if ($scope.newVisitor.AttachmentColl) {
			var filesColl = $scope.newVisitor.AttachmentColl;
		}

		if ($scope.newVisitor.InTime_TMP)
			$scope.newVisitor.InTime = $scope.newVisitor.InTime_TMP.toLocaleString();

		if ($scope.newVisitor.ValidityTime_TMP)
			$scope.newVisitor.ValidityTime = $scope.newVisitor.ValidityTime_TMP.toLocaleString();

		if ($scope.newVisitor.OutTime_TMP)
			$scope.newVisitor.OutTime = $scope.newVisitor.OutTime_TMP.toLocaleString();

		if ($scope.newVisitor.MeeTo == 1) {
			$scope.newVisitor.EmployeeId = null;
			$scope.newVisitor.OthersName = '';

			if ($scope.newVisitor.StudentDetails) {
				$scope.newVisitor.OthersName = $scope.newVisitor.StudentDetails.RegdNo + " : " + $scope.newVisitor.StudentDetails.Name + ": " + $scope.newVisitor.StudentDetails.ClassName+" "+$scope.newVisitor.StudentDetails.SectionName+ " : "+$scope.newVisitor.StudentDetails.RollNo;
            }
		} else if ($scope.newVisitor.MeeTo == 2) {
			$scope.newVisitor.StudentId = null;
			$scope.newVisitor.OthersName = '';

			if ($scope.newVisitor.EmployeeDetails) {
				$scope.newVisitor.OthersName = $scope.newVisitor.EmployeeDetails.Code + " : " + $scope.newVisitor.EmployeeDetails.Name + ": " + $scope.newVisitor.EmployeeDetails.Address + " " + $scope.newVisitor.EmployeeDetails.MobileNo;
			}

		}
		else if ($scope.newVisitor.MeeTo == 3) {
			$scope.newVisitor.EmployeeId = null;
			$scope.newVisitor.StudentId = null;
		}

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveVisitorBook",
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
			data: { jsonData: $scope.newVisitor, files: filesColl, stPhoto: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.SendNotification($scope.newVisitor);
				$scope.GetAllVisitorList();
				$scope.ClearVisitor();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.SendNotification = function (objEntity) {

		var para1 = {
			EntityId: entityForSMS,
			ForATS: (objEntity.MeeTo == 1 ? 3 : 2),
			TemplateType: 3
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(para1)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				var templatesColl = res.data.Data;
				if (templatesColl && templatesColl.length > 0) {
					var selectedTemplate = templatesColl[0];

					$timeout(function () {

						var dataColl = [];
						var msg = selectedTemplate.Description;
						for (let x in objEntity) {
							var variable = '$$' + x.toLowerCase() + '$$';
							if (msg.indexOf(variable) >= 0) {
								var val = objEntity[x];
								msg = msg.replace(variable, val);
							}

							if (msg.indexOf('$$') == -1)
								break;
						}

						if (objEntity.MeeTo == 1) {
							var newSMS = {
								EntityId: entityForSMS,
								StudentId: objEntity.StudentDetails.StudentId,
								UserId: objEntity.StudentDetails.UserId,
								Title: selectedTemplate.Title,
								Message: msg,
								ContactNo: objEntity.StudentDetails.ContactNo,
								StudentName: objEntity.Name,
								ContentPath: ''
							};
							dataColl.push(newSMS);
						} else if (objEntity.MeeTo == 2) {
							var newSMS = {
								EntityId: entityForSMS,
								StudentId: objEntity.EmployeeDetails.EmployeeId,
								UserId: objEntity.EmployeeDetails.UserId,
								Title: selectedTemplate.Title,
								Message: msg,
								ContactNo: (objEntity.ContactNo ? objEntity.ContactNo : ''),
								StudentName: objEntity.Name,
								ContentPath: ''
							};
							dataColl.push(newSMS);
						}

						if (dataColl.length > 0) {
							$http({
								method: 'POST',
								url: base_url + "Global/SendNotificationToStudent",
								dataType: "json",
								data: JSON.stringify(dataColl)
							}).then(function (sRes) {
								hidePleaseWait();
								$scope.loadingstatus = "stop";

							});
						}

					});

				}
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



	};

	$scope.GetAllVisitorList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.VisitorList = [];

		var para = {
			dateFrom: ($scope.newVisitor.FromDateDet ? $filter('date')(new Date($scope.newVisitor.FromDateDet.dateAD), 'yyyy-MM-dd') : null),
			dateTo: ($scope.newVisitor.ToDateDet ? $filter('date')(new Date($scope.newVisitor.ToDateDet.dateAD), 'yyyy-MM-dd') : null),
		};
		  
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllVisitorList",
			dataType: "json",
			data:JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.VisitorList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.CurGatePass = {};
	$scope.ShowInTime = function (beData) {
		if (beData.InTime) {
			$scope.CurVisitor = beData;
			if ($scope.CurVisitor.InTime)
				$scope.CurVisitor.InTime_TMP = new Date($scope.CurVisitor.InTime);

			if ($scope.CurVisitor.OutTime)
				$scope.CurVisitor.OutTime_TMP = new Date($scope.CurVisitor.OutTime);
			$('#modal-xl').modal('show');
		}
	}

	$scope.UpdateInTime = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.CurVisitor.InTime_TMP)
			$scope.CurVisitor.InTime = $scope.CurVisitor.InTime_TMP.toLocaleString();

		if ($scope.CurVisitor.OutTime_TMP)
			$scope.CurVisitor.OutTime = $scope.CurVisitor.OutTime_TMP.toLocaleString();

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/UpdateInTimeOfVisitorBook",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.CurVisitor }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			$('#modal-xl').modal('hide');

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetVisitorById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			VisitorId: refData.VisitorId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetVisitorById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newVisitor = res.data.Data;
				if ($scope.newVisitor.ValidityTime)
					$scope.newVisitor.ValidityTime = new Date($scope.newVisitor.ValidityTime);
				if ($scope.newVisitor.OutTime)
					$scope.newVisitor.OutTime = new Date($scope.newVisitor.OutTime);

				$scope.newVisitor.Mode = 'Modify';
				$scope.open_form_btn();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelVisitorById = function (refData) {

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
					VisitorId: refData.VisitorId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelVisitor",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {

						$scope.GetAllVisitorList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.validateDates = function (changedField) {
		if (!$scope.newVisitor.FromDateDet || !$scope.newVisitor.ToDateDet ||
			!$scope.newVisitor.FromDateDet.dateAD || !$scope.newVisitor.ToDateDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newVisitor.FromDateDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newVisitor.ToDateDet.dateAD), 'yyyy-MM-dd')
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be greater than To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newVisitor.FromDate_TMP = new Date();
						$scope.newVisitor.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be less than From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newVisitor.ToDate_TMP = new Date();
						$scope.newVisitor.ToDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};
});