app.controller('GatePassController', function ($scope, $http, $timeout, $filter, GlobalServices) {

	$scope.Title = 'GatePass Entry';

	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.LanguageColl = GlobalServices.getLangList();
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();


		$scope.currentPages = {
			GatePass: 1,
		};
		$scope.searchData = {
			GatePass: '',
		};
		$scope.perPage = {
			GatePass: GlobalServices.getPerPageRow(),
		};

		$scope.newGatePass = {
			GatePassId: null,
			Name: '',
			Address: '',
			Contact: '',
			Email: '',
			GatePassName: '',
			Purpose: '',
			OutTime: new Date(),
			OutTime_TMP: new Date(),
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
			WillReturnBack:true,
			Mode: 'Save'

		};
		$scope.GetAllGatePassList();


	};

	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newGatePass.AttachmentColl) {
			if ($scope.newGatePass.AttachmentColl.length > 0) {
				$scope.newGatePass.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {
		$scope.newGatePass.AttachmentColl;
		if (files && docType) {
			if (files != null && docType != null) {
				angular.forEach(files, function (file) {
					$scope.newGatePass.AttachmentColl.push({
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
	$scope.ClearGatePass = function () {
		$timeout(function () {
			$scope.newGatePass = {
				GatePassId: null,
				Name: '',
				Address: '',
				Contact: '',
				Email: '',
				GatePassName: '',
				Purpose: '',
				OutTime: new Date(),
				OutTime_TMP: new Date(),
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
				WillReturnBack:true,
				Mode: 'Save'
			};
			$scope.ClearGatePassPhoto();

		});

	};


	function OnClickDefault() {
		document.getElementById('enquiry-form').style.display = "none";

		$scope.open_form_btn = function () {
			document.getElementById('table-listing').style.display = "none";
			document.getElementById('enquiry-form').style.display = "block";
			$scope.ClearGatePass();
		}

		$scope.back_to_list = function () {
			document.getElementById('table-listing').style.display = "block";
			document.getElementById('enquiry-form').style.display = "none";
			$scope.ClearGatePass();
		}
	};
	//Clear photo
	$scope.ClearGatePassPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newGatePass.PhotoData = null;
				$scope.newGatePass.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};




	$scope.IsValidGatePass = function () {
	 

		if ($scope.newGatePass.Purpose.isEmpty()) {
			Swal.fire('Please ! Enter Visiting Purpose');
			return false;
		}



		return true;
	};

	$scope.SaveUpdateGatePass = function () {
		if ($scope.IsValidGatePass() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newGatePass.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateGatePass();
					}
				});
			} else
				$scope.CallSaveUpdateGatePass();

		}
	};

	$scope.CallSaveUpdateGatePass = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newGatePass.Photo_TMP) {
			var photo = $scope.newGatePass.Photo_TMP;
			//$scope.newGatePass.Photo = $scope.newGatePass.PhotoData; 
		}
		if ($scope.newGatePass.AttachmentColl) {
			var filesColl = $scope.newGatePass.AttachmentColl;
		}

		if ($scope.newGatePass.InTime_TMP)
			$scope.newGatePass.InTime = $scope.newGatePass.InTime_TMP.toLocaleString();

		if ($scope.newGatePass.ValidityTime_TMP)
			$scope.newGatePass.ValidityTime = $scope.newGatePass.ValidityTime_TMP.toLocaleString();

		if ($scope.newGatePass.OutTime_TMP)
			$scope.newGatePass.OutTime = $scope.newGatePass.OutTime_TMP.toLocaleString();

		if ($scope.newGatePass.MeeTo == 1) {
			$scope.newGatePass.EmployeeId = null;
			$scope.newGatePass.OthersName = '';
			 
			if ($scope.newGatePass.StudentDetails) {
				$scope.newGatePass.Name = $scope.newGatePass.StudentDetails.Name;
				$scope.newGatePass.Address = $scope.newGatePass.StudentDetails.Address;
				$scope.newGatePass.OthersName = $scope.newGatePass.StudentDetails.RegdNo + " : " + $scope.newGatePass.StudentDetails.Name + ": " + $scope.newGatePass.StudentDetails.ClassName + " " + $scope.newGatePass.StudentDetails.SectionName + " : " + $scope.newGatePass.StudentDetails.RollNo;
			}
		} else if ($scope.newGatePass.MeeTo == 2) {
			$scope.newGatePass.StudentId = null;
			$scope.newGatePass.OthersName = '';
			
			if ($scope.newGatePass.EmployeeDetails) {

				$scope.newGatePass.Name = $scope.newGatePass.EmployeeDetails.Name;
				$scope.newGatePass.Address = $scope.newGatePass.EmployeeDetails.Address;

				$scope.newGatePass.OthersName = $scope.newGatePass.EmployeeDetails.Code + " : " + $scope.newGatePass.EmployeeDetails.Name + ": " + $scope.newGatePass.EmployeeDetails.Address + " " + $scope.newGatePass.EmployeeDetails.MobileNo;
			}

		}
		else if ($scope.newGatePass.MeeTo == 3) {
			$scope.newGatePass.EmployeeId = null;
			$scope.newGatePass.StudentId = null;
			$scope.newGatePass.Name = $scope.newGatePass.OthersName;
		}

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveGatePass",
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
			data: { jsonData: $scope.newGatePass, files: filesColl, stPhoto: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.SendNotification($scope.newGatePass);				
				$scope.Print(res.data.Data.RId);				
				$scope.GetAllGatePassList();
				$scope.ClearGatePass();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.CurGatePass = {};
	$scope.ShowInTime = function (beData) {

		if (beData.WillReturnBack == true && !beData.InTime) {
			$scope.CurGatePass = beData;

			if ($scope.CurGatePass.InTime)
				$scope.CurGatePass.InTime_TMP = new Date($scope.CurGatePass.InTime);

			$('#modal-xl').modal('show');
        }
		
	}
	$scope.UpdateInTime = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();
   
		if ($scope.CurGatePass.InTime_TMP)
			$scope.CurGatePass.InTime = $scope.CurGatePass.InTime_TMP.toLocaleString();		  

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/UpdateInTimeOfGatePass",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				 

				return formData;
			},
			data: { jsonData: $scope.CurGatePass  }
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

	$scope.SendNotification = function (objEntity) {

		var para1 = {
			EntityId: entityForSMS,
			ForATS: (objEntity.MeeTo==1 ? 3 : 2),
			TemplateType: 3
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetSENT",
			dataType: "json",
			data: JSON.stringify(para1)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
			{
				var templatesColl = res.data.Data;
				if (templatesColl && templatesColl.length > 0)
				{
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
	$scope.GetAllGatePassList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.GatePassList = [];

		var para = {
			dateFrom: ($scope.newGatePass.FromDateDet ? $filter('date')(new Date($scope.newGatePass.FromDateDet.dateAD), 'yyyy-MM-dd') : null),
			dateTo: ($scope.newGatePass.ToDateDet ? $filter('date')(new Date($scope.newGatePass.ToDateDet.dateAD), 'yyyy-MM-dd') : null),
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllGatePassList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.GatePassList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetGatePassById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			GatePassId: refData.GatePassId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetGatePassById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newGatePass = res.data.Data;
				if ($scope.newGatePass.ValidityTime)
					$scope.newGatePass.ValidityTime = new Date($scope.newGatePass.ValidityTime);
				if ($scope.newGatePass.OutTime)
					$scope.newGatePass.OutTime = new Date($scope.newGatePass.OutTime);

				$scope.newGatePass.Mode = 'Modify';
				$scope.open_form_btn();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelGatePassById = function (refData) {

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
					TranId: refData.TranId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelGatepass",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {

						$scope.GetAllGatePassList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
		 
	};

	$scope.Print = function (tranId) {
		if (( tranId > 0)) {
		 
			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + EntityId + "&voucherId=0&isTran=true",
				dataType: "json"
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					var templatesColl = res.data.Data;
					if (templatesColl && templatesColl.length > 0) {
						var templatesName = [];
						var sno = 1;
						angular.forEach(templatesColl, function (tc) {
							templatesName.push(sno + '-' + tc.ReportName);
							sno++;
						});

						var print = false;

						var rptTranId = 0;
						if (templatesColl.length == 1)
							rptTranId = templatesColl[0].RptTranId;
						else {
							Swal.fire({
								title: 'Report Templates For Print',
								input: 'select',
								inputOptions: templatesName,
								inputPlaceholder: 'Select a template',
								showCancelButton: true,
								inputValidator: (value) => {
									return new Promise((resolve) => {
										if (value >= 0) {
											resolve()
											rptTranId = templatesColl[value].RptTranId;

											if (rptTranId > 0) {
												print = true;
												document.body.style.cursor = 'wait';
												document.getElementById("frmRpt").src = '';
												document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=0&tranId=" + tranId + "&vouchertype=0";
												document.body.style.cursor = 'default';
												$('#FrmPrintReport').modal('show');
											}

										} else {
											resolve('You need to select:)')
										}
									})
								}
							})
						}

						if (rptTranId > 0 && print == false) {
							document.body.style.cursor = 'wait';
							document.getElementById("frmRpt").src = '';
							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=0&tranId=" + tranId + "&vouchertype=0";
							document.body.style.cursor = 'default';
							$('#FrmPrintReport').modal('show');
						}

					} else
						Swal.fire('No Templates found for print');
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});



		}
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