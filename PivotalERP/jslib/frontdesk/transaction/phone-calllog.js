app.controller('PhoneCallController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Phone Call Log';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.LanguageColl = GlobalServices.getLangList();
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();


		$scope.currentPages = {
			PhoneCall: 1,
		};
		$scope.searchData = {
			PhoneCall: '',
		};
		$scope.perPage = {
			PhoneCall: GlobalServices.getPerPageRow(),
		};

		$scope.newPhoneCall = {
			PhoneCallId: null,
			Name: '',
			Address: '',
			Contact: '',
			Email: '',
			PhoneCallName: '',
			Purpose: '',
			MeeTo:1,
			InOutTime_TMP: new Date(),
			ForDate_TMP: new Date(),
			CallType:1,
			ValidityTime: null,
			OutTime: null,
			Remarks: '',
			TypeOfDocumentId: null,
			AttachDocument: '',
			Description: '',			
			AttachmentColl: [],
			SelectStudent: $scope.StudentSearchOptions[0].value,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date(),
			Mode: 'Save'
		};
		$scope.GetAllPhoneCallList();


	};

	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newPhoneCall.AttachmentColl) {
			if ($scope.newPhoneCall.AttachmentColl.length > 0) {
				$scope.newPhoneCall.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {
		$scope.newPhoneCall.AttachmentColl;
		if (files && docType) {
			if (files != null && docType != null) {
				angular.forEach(files, function (file) {
					$scope.newPhoneCall.AttachmentColl.push({
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
	$scope.ClearPhoneCall = function () {
		$timeout(function () {
			$scope.newPhoneCall = {
				PhoneCallId: null,
				Name: '',
				Address: '',
				Contact: '',
				Email: '',
				PhoneCallName: '',
				Purpose: '',
				MeeTo: 1,
				InOutTime_TMP: new Date(),
				ForDate_TMP: new Date(),
				CallType: 1,
				ValidityTime: null,
				OutTime: null,
				Remarks: '',
				TypeOfDocumentId: null,
				AttachDocument: '',
				Description: '',				
				AttachmentColl: [],
				SelectStudent: $scope.StudentSearchOptions[0].value,
				SelectEmployee: $scope.EmployeeSearchOptions[0].value,
				FromDate_TMP: new Date(),
				ToDate_TMP: new Date(),
				Mode: 'Save'
			};
			$scope.ClearPhoneCallPhoto();

		});

	};


	function OnClickDefault() {
		document.getElementById('enquiry-form').style.display = "none";

		$scope.open_form_btn = function () {
			document.getElementById('table-listing').style.display = "none";
			document.getElementById('enquiry-form').style.display = "block";
			$scope.ClearPhoneCall();
		}

		$scope.back_to_list = function () {
			document.getElementById('table-listing').style.display = "block";
			document.getElementById('enquiry-form').style.display = "none";
			$scope.ClearPhoneCall();
		}
	};
	//Clear photo
	$scope.ClearPhoneCallPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newPhoneCall.PhotoData = null;
				$scope.newPhoneCall.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};



	$scope.IsValidPhoneCall = function () {

		// Meet To validation
		if (!$scope.newPhoneCall.MeeTo) {
			Swal.fire('Please ! Select Meet To');
			return false;
		}

		// Student validation
		if ($scope.newPhoneCall.MeeTo == 1) {
			if (!$scope.newPhoneCall.StudentId) {
				Swal.fire('Please ! Select Student');
				return false;
			}
		}

		// Employee validation
		if ($scope.newPhoneCall.MeeTo == 2) {
			if (!$scope.newPhoneCall.EmployeeId) {
				Swal.fire('Please ! Select Employee');
				return false;
			}
		}

		// Other name validation
		if ($scope.newPhoneCall.MeeTo == 3) {
			if (!$scope.newPhoneCall.OthersName || $scope.newPhoneCall.OthersName.trim() === '') {
				Swal.fire('Please ! Enter Name');
				return false;
			}
		}

		// Contact validation
		if (!$scope.newPhoneCall.Contact || $scope.newPhoneCall.Contact.trim() === '') {
			Swal.fire('Please ! Enter Contact');
			return false;
		}

		// Date validation
		if (!$scope.newPhoneCall.ForDateDet) {
			Swal.fire('Please ! Select Date');
			return false;
		}

		// Purpose validation
		if (!$scope.newPhoneCall.Purpose || $scope.newPhoneCall.Purpose.trim() === '') {
			Swal.fire('Please ! Enter Phone Call Purpose');
			return false;
		}

		// Time validation
		if (!$scope.newPhoneCall.InOutTime_TMP) {
			Swal.fire('Please ! Select Time');
			return false;
		}

		// Call Type validation
		if (!$scope.newPhoneCall.CallType) {
			Swal.fire('Please ! Select Call Type');
			return false;
		}

		return true;
	};


	$scope.SaveUpdatePhoneCall = function () {
		if ($scope.IsValidPhoneCall() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newPhoneCall.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdatePhoneCall();
					}
				});
			} else
				$scope.CallSaveUpdatePhoneCall();

		}
	};

	$scope.CallSaveUpdatePhoneCall = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newPhoneCall.ForDate = $filter('date')(new Date($scope.newPhoneCall.ForDateDet.dateAD), 'yyyy-MM-dd');

		if ($scope.newPhoneCall.NextFollowupDate_TMP)
			$scope.newPhoneCall.NextFollowupDate = $filter('date')(new Date($scope.newPhoneCall.NextFollowupDateDet.dateAD), 'yyyy-MM-dd');

		if ($scope.newPhoneCall.InOutTime_TMP)
			$scope.newPhoneCall.InOutTime = $scope.newPhoneCall.InOutTime_TMP.toLocaleString();
		  
		if ($scope.newPhoneCall.MeeTo == 1) {
			$scope.newPhoneCall.EmployeeId = null;
			$scope.newPhoneCall.OthersName = '';

			if ($scope.newPhoneCall.StudentDetails) {
				$scope.newPhoneCall.Name = $scope.newPhoneCall.StudentDetails.Name;
				$scope.newPhoneCall.Address = $scope.newPhoneCall.StudentDetails.Address;
				$scope.newPhoneCall.OthersName = $scope.newPhoneCall.StudentDetails.RegdNo + " : " + $scope.newPhoneCall.StudentDetails.Name + ": " + $scope.newPhoneCall.StudentDetails.ClassName + " " + $scope.newPhoneCall.StudentDetails.SectionName + " : " + $scope.newPhoneCall.StudentDetails.RollNo;
			}
		} else if ($scope.newPhoneCall.MeeTo == 2) {
			$scope.newPhoneCall.StudentId = null;
			$scope.newPhoneCall.OthersName = '';

			if ($scope.newPhoneCall.EmployeeDetails) {

				$scope.newPhoneCall.Name = $scope.newPhoneCall.EmployeeDetails.Name;
				$scope.newPhoneCall.Address = $scope.newPhoneCall.EmployeeDetails.Address;

				$scope.newPhoneCall.OthersName = $scope.newPhoneCall.EmployeeDetails.Code + " : " + $scope.newPhoneCall.EmployeeDetails.Name + ": " + $scope.newPhoneCall.EmployeeDetails.Address + " " + $scope.newPhoneCall.EmployeeDetails.MobileNo;
			}

		}
		else if ($scope.newPhoneCall.MeeTo == 3) {
			$scope.newPhoneCall.EmployeeId = null;
			$scope.newPhoneCall.StudentId = null;
			$scope.newPhoneCall.Name = $scope.newPhoneCall.OthersName;
		}

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SavePhoneCallLog",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				  
				return formData;
			},
			data: { jsonData: $scope.newPhoneCall}
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.SendNotification($scope.newPhoneCall);
				$scope.GetAllPhoneCallList();
				$scope.ClearPhoneCall();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.SendNotification = function (objEntity) {

		if (objEntity.CallType == 1)
			objEntity.CallType = 'INCOMING';
		else
			objEntity.CallType = 'OUTGOING';

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

	$scope.GetAllPhoneCallList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PhoneCallList = [];

		var para = {
			dateFrom: ($scope.newPhoneCall.FromDateDet ? $filter('date')(new Date($scope.newPhoneCall.FromDateDet.dateAD), 'yyyy-MM-dd') : null),
			dateTo: ($scope.newPhoneCall.ToDateDet ? $filter('date')(new Date($scope.newPhoneCall.ToDateDet.dateAD), 'yyyy-MM-dd') : null),
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllPhoneCallLogList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PhoneCallList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetPhoneCallById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			PhoneCallId: refData.PhoneCallId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetPhoneCallById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newPhoneCall = res.data.Data;
				if ($scope.newPhoneCall.ValidityTime)
					$scope.newPhoneCall.ValidityTime = new Date($scope.newPhoneCall.ValidityTime);
				if ($scope.newPhoneCall.OutTime)
					$scope.newPhoneCall.OutTime = new Date($scope.newPhoneCall.OutTime);

				$scope.newPhoneCall.Mode = 'Modify';
				$scope.open_form_btn();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelPhoneCallById = function (refData) {

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
					PhoneCallId: refData.PhoneCallId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelPhoneCall",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {

						$scope.GetAllPhoneCallList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


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
