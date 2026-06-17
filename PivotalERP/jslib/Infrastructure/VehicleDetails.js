app.controller('TransportSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Transport Setup';
	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.FuleTypes = ["Diesel", "Petrol", "Electric", "CNG"];

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			VehicleDetails: 1
		};

		$scope.searchData = {
			VehicleDetails: ''
		};

		$scope.perPage = {
			VehicleDetails: GlobalServices.getPerPageRow()
		};

		$scope.newCompanyDetails = {};
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetCompanyDet",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCompanyDet = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.Logo = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllAboutUsList",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.Logo = res.data.Data[0];
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newVehicleDetails = {
			VehicleDetailsId: null,
			VehicleName: '',
			VehicleNumber: 0,
			SeatCapacity: 0,
			EngineNo: '',
			ChasisNo: '',
			FuelEngineTypeId: null,
			NaamsariMiti_TMP: null,
			RenewalMiti_TMP: null,
			ManufacturingYearId: null,
			Photo: null,
			PhotoPath: null,
			JachpassNo: '',
			ValidFrom_TMP: null,
			ValidTo_TMP: null,
			JachpassRemarks: '',
			InsuranceNo: '',
			InsuranceValidFrom_TMP: null,
			InsuranceValidTo_TMP: null,
			InsuranceRemarks: '',
			InchargeId: null,
			DriverId: null,
			ConductorId: null,
			GpsDevice: '',
			AttachmentColl: [],
			EmployeeSearchBy: 'E.Name',
			Mode: 'Save'
		};

		

		$scope.DocumentTypeList = [];
		GlobalServices.getDocumentTypeList().then(function (res) {
			$scope.DocumentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllVehicleDetailsList();

		try {
			var script = document.createElement('script');
			script.type = 'text/javascript';
			script.src = 'https://maps.googleapis.com/maps/api/js?key=' + API_KEY + '&callback=initMap';
			document.body.appendChild(script);
			setTimeout(function () {
				$scope.initMap();
			}, 500);

		} catch { }


	}
	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newVehicleDetails.AttachmentColl) {
			if ($scope.newVehicleDetails.AttachmentColl.length > 0) {
				$scope.newVehicleDetails.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {

		if (!$scope.newVehicleDetails.AttachmentColl)
			$scope.newVehicleDetails.AttachmentColl = [];

		if (files && docType) {
			if (files != null && docType != null) {

				angular.forEach(files, function (file) {
					$scope.newVehicleDetails.AttachmentColl.push({
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
	function OnClickDefault() {
		document.getElementById('driver-form').style.display = "none";
		document.getElementById('printpart').style.display = "none";
		

		document.getElementById('add-driver').onclick = function () {
			document.getElementById('driver-section').style.display = "none";
			document.getElementById('driver-form').style.display = "block";
			$scope.ClearVehicleDetails();
		}

		document.getElementById('back-driver-list').onclick = function () {
			document.getElementById('driver-form').style.display = "none";
			document.getElementById('driver-section').style.display = "block";
			$scope.ClearVehicleDetails();
		}
		
	};

	$scope.ClearVehicleDetails = function () {
		$scope.newVehicleDetails = {
			VehicleDetailsId: null,
			VehicleName: '',
			VehicleNumber: 0,
			SeatCapacity: 0,
			EngineNo: '',
			ChasisNo: '',
			FuelEngineTypeId: null,
			NaamsariMiti_TMP: null,
			RenewalMiti_TMP: null,
			ManufacturingYearId: null,
			Photo: null,
			PhotoPath: null,
			JachpassNo: '',
			ValidFrom_TMP: null,
			ValidTo_TMP: null,
			JachpassRemarks: '',
			InsuranceNo: '',
			InsuranceValidFrom_TMP: null,
			InsuranceValidTo_TMP: null,
			InsuranceRemarks: '',
			InchargeId: null,
			DriverId: null,
			ConductorId: null,
			GpsDevice: '',
			AttachmentColl: [],
			EmployeeSearchBy: 'E.Name',
			Mode: 'Save'
		};

		$scope.ClearVehicleDetailsPhoto();
	}

	

	//*************************Vehicle Detail *********************************
	$scope.ClearVehicleDetailsPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newVehicleDetails.PhotoData = null;
				$scope.newVehicleDetails.Photo_TMP = [];
			});
		});
		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');
	};

	$scope.IsValidVehicleDetails = function () {
		if ($scope.newVehicleDetails.VehicleName.isEmpty()) {
			Swal.fire('Please ! Enter Vehicle Name');
			return false;
		}
		return true;
	}

	$scope.delAttachmentFiles = function (ind) {
		if ($scope.newVehicleDetails.AttachmentColl) {
			if ($scope.newVehicleDetails.AttachmentColl.length > 0) {
				$scope.newVehicleDetails.AttachmentColl.splice(ind, 1);
			}
		}
	}
	$scope.AddMoreFiles = function (files, docType, des) {
		if (files && docType) {
			if (files != null && docType != null) {
				angular.forEach(files, function (file) {
					$scope.newVehicleDetails.AttachmentColl.push({
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


	$scope.SaveUpdateVehicleDetails = function () {
		if ($scope.IsValidVehicleDetails() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newVehicleDetails.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateVehicleDetails();
					}
				});
			} else
				$scope.CallSaveUpdateVehicleDetails();
		}
	};

	$scope.CallSaveUpdateVehicleDetails = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newVehicleDetails.ValidFromDet) {
			$scope.newVehicleDetails.JPValidityFrom = $filter('date')(new Date($scope.newVehicleDetails.ValidFromDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newVehicleDetails.JPValidityFrom = null;

		if ($scope.newVehicleDetails.ValidToDet) {
			$scope.newVehicleDetails.JPValidityTo = $filter('date')(new Date($scope.newVehicleDetails.ValidToDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newVehicleDetails.JPValidityTo = null;

		if ($scope.newVehicleDetails.InsuranceValidFromDet) {
			$scope.newVehicleDetails.IValidityFrom = $filter('date')(new Date($scope.newVehicleDetails.InsuranceValidFromDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newVehicleDetails.IValidityFrom = null;

		if ($scope.newVehicleDetails.InsuranceValidToDet) {
			$scope.newVehicleDetails.IValidityTo = $filter('date')(new Date($scope.newVehicleDetails.InsuranceValidToDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newVehicleDetails.IValidityTo = null;

		if ($scope.newVehicleDetails.NaamsariMitiDet) {
			$scope.newVehicleDetails.NamsariDate = $filter('date')(new Date($scope.newVehicleDetails.NaamsariMitiDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newVehicleDetails.NamsariDate = null;

		if ($scope.newVehicleDetails.RenewalMitiDet) {
			$scope.newVehicleDetails.RenewalDate = $filter('date')(new Date($scope.newVehicleDetails.RenewalMitiDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newVehicleDetails.RenewalDate = null;

		var filesColl = $scope.newVehicleDetails.AttachmentColl;
		var photo = $scope.newVehicleDetails.Photo_TMP;
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/SaveVehicle",
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
			data: { jsonData: $scope.newVehicleDetails, files: filesColl, stPhoto: photo }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearVehicleDetails();
				$scope.GetAllVehicleDetailsList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllVehicleDetailsList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.VehicleDetailsList = [];

		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetAllVehicleList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.VehicleDetailsList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetVehicleDetailsById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			VehicleId: refData.VehicleId
		};

		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetVehicleById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newVehicleDetails = res.data.Data;
				$scope.newVehicleDetails.Mode = 'Modify';

				if ($scope.newVehicleDetails.JPValidityFrom) {
					$scope.newVehicleDetails.ValidFrom_TMP = new Date($scope.newVehicleDetails.JPValidityFrom);
				} else
					$scope.newVehicleDetails.ValidFrom_TMP = null;

				if ($scope.newVehicleDetails.JPValidityTo) {
					$scope.newVehicleDetails.ValidTo_TMP = new Date($scope.newVehicleDetails.JPValidityTo);
				} else
					$scope.newVehicleDetails.ValidTo_TMP = null;

				if ($scope.newVehicleDetails.IValidityFrom) {
					$scope.newVehicleDetails.InsuranceValidFrom_TMP = new Date($scope.newVehicleDetails.IValidityFrom);
				} else
					$scope.newVehicleDetails.InsuranceValidFrom_TMP = null;

				if ($scope.newVehicleDetails.IValidityTo) {
					$scope.newVehicleDetails.InsuranceValidTo_TMP = new Date($scope.newVehicleDetails.IValidityTo);
				} else
					$scope.newVehicleDetails.InsuranceValidTo_TMP = null;

				if ($scope.newVehicleDetails.NamsariDate) {
					$scope.newVehicleDetails.NaamsariMiti_TMP = new Date($scope.newVehicleDetails.NamsariDate);
				} else
					$scope.newVehicleDetails.NaamsariMiti_TMP = null;

				if ($scope.newVehicleDetails.RenewalDate) {
					$scope.newVehicleDetails.RenewalMiti_TMP = new Date($scope.newVehicleDetails.RenewalDate);
				} else
					$scope.newVehicleDetails.RenewalMiti_TMP = null;

				document.getElementById('driver-section').style.display = "none";
				document.getElementById('driver-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.GetAtt = function (item) {
		$scope.viewImg2 = {
			ContentPath: '',
			FileType: null
		};

		if (item.ImagePath && item.ImagePath.length > 0) {
			$scope.viewImg2.ContentPath = item.ImagePath;
			$scope.viewImg2.FileType = 'pdf';
			document.getElementById('pdfViewer2').src = item.ImagePath;
			$('#PersonalImg3').modal('show');
		} else if (item.PhotoPath && item.PhotoPath.length > 0) {
			$scope.viewImg2.ContentPath = item.PhotoPath;
			$scope.viewImg2.FileType = 'image';
			$('#PersonalImg3').modal('show');
		} else if (item.File) {
			var blob = new Blob([item.File], { type: item.File?.type });
			$scope.viewImg2.ContentPath = URL.createObjectURL(blob);
			$scope.viewImg2.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

			if ($scope.viewImg2.FileType === 'pdf') {
				document.getElementById('pdfViewer2').src = $scope.viewImg2.ContentPath;
			}

			$('#PersonalImg3').modal('show');
		} else {
			Swal.fire('No Any Attachment Found');
		}
	};

	$scope.DelVehicleDetailsById = function (refData) {
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
					VehicleId: refData.VehicleId
				};

				$http({
					method: 'POST',
					url: base_url + "Transport/Creation/DelVehicle",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllVehicleDetailsList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};
	
	$scope.PrintVehicleById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			VehicleId: refData.VehicleId
		};

		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetVehicleById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newVehicleDetails = res.data.Data;
				$scope.newVehicleDetails.Mode = 'Modify';

				if ($scope.newVehicleDetails.JPValidityFrom) {
					$scope.newVehicleDetails.ValidFrom_TMP = new Date($scope.newVehicleDetails.JPValidityFrom);
				} else
					$scope.newVehicleDetails.ValidFrom_TMP = null;

				if ($scope.newVehicleDetails.JPValidityTo) {
					$scope.newVehicleDetails.ValidTo_TMP = new Date($scope.newVehicleDetails.JPValidityTo);
				} else
					$scope.newVehicleDetails.ValidTo_TMP = null;

				if ($scope.newVehicleDetails.IValidityFrom) {
					$scope.newVehicleDetails.InsuranceValidFrom_TMP = new Date($scope.newVehicleDetails.IValidityFrom);
				} else
					$scope.newVehicleDetails.InsuranceValidFrom_TMP = null;

				if ($scope.newVehicleDetails.IValidityTo) {
					$scope.newVehicleDetails.InsuranceValidTo_TMP = new Date($scope.newVehicleDetails.IValidityTo);
				} else
					$scope.newVehicleDetails.InsuranceValidTo_TMP = null;

				if ($scope.newVehicleDetails.NamsariDate) {
					$scope.newVehicleDetails.NaamsariMiti_TMP = new Date($scope.newVehicleDetails.NamsariDate);
				} else
					$scope.newVehicleDetails.NaamsariMiti_TMP = null;

				if ($scope.newVehicleDetails.RenewalDate) {
					$scope.newVehicleDetails.RenewalMiti_TMP = new Date($scope.newVehicleDetails.RenewalDate);
				} else
					$scope.newVehicleDetails.RenewalMiti_TMP = null;
				$scope.timestamp = new Date().getTime();
				$('#printcard').printThis();

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.ChangeIncharge = function () {
		if ($scope.newVehicleDetails.InchargeId > 0) {
			$scope.newVehicleDetails.Incharge = mx($scope.EmployeeCollection).firstOrDefault(p1 => p1.id == $scope.newVehicleDetails.InchargeId).text;
		} else
			$scope.newGeneralInfoDet.Incharge = '';
	}
});