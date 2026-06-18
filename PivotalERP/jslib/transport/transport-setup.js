app.controller('TransportSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Transport Setup';
	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.FuleTypes = ["Diesel", "Petrol"];

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			VehicleDetails: 1,
			TransportRoute: 1,
			TransportPoint: 1
		};

		$scope.searchData = {
			VehicleDetails: '',
			TransportRoute: '',
			TransportPoint: ''
		};

		$scope.perPage = {
			VehicleDetails: GlobalServices.getPerPageRow(),
			TransportRoute: GlobalServices.getPerPageRow(),
			TransportPoint: GlobalServices.getPerPageRow(),
		};

		$scope.newVehicleDetails = {
			VehicleDetailsId: null,
			VehicleName: '',
			VehicleNumber: 0,
			SeatCapacity: 0,
			EngineNo: '',
			ChasisNo: '',
			FuelEngineTypeId: null,
			NaamsariMiti_TMP: new Date(),
			RenewalMiti_TMP: new Date(),
			ManufacturingYearId: null,
			Photo: null,
			PhotoPath: null,
			JachpassNo: '',
			ValidFrom_TMP: new Date(),
			ValidTo_TMP: new Date(),
			JachpassRemarks: '',
			InsuranceNo: '',
			InsuranceValidFrom_TMP: new Date(),
			InsuranceValidTo_TMP: new Date(),
			InsuranceRemarks: '',
			InchargeId: null,
			DriverId: null,
			ConductorId: null,
			GpsDevice: '',
			AttachmentColl: [],
			EmployeeSearchBy: 'E.Name',
			Mode: 'Save'
		};

		$scope.newTransportRoute = {
			RouteId: null,
			Name: '',
			VehicleId: null,
			FuelConsumeId: null,
			FuelConsume: '',
			FuelConsumeInLitre: '',
			ArrivalTime_TMP: null,
			DepartureTime_TMP: null,
			Lat: null,
			Lan: null,
			EndLat: null,
			EndLan: null,
			D_Lat: null,
			D_Lan: null,
			D_EndLat: null,
			D_EndLan: null,
			Mode: 'Save'
		};

		$scope.newTransportPoint = {
			PointId: null,
			Name: '',
			RouteId: null,
			PickupRate: 0,
			DropRate: 0,
			BothRate: 0,
			Latitude: 0,
			Longitude: 0,
			OrderNo:0,
			Description: '',
			Mode: 'Save'
		};

		$scope.DocumentTypeList = [];
		GlobalServices.getDocumentTypeList().then(function (res) {
			$scope.DocumentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllVehicleDetailsList();
/*		$scope.GetAllTransportRouteList();
		$scope.GetAllTransportPointList();
*/
		$scope.isRouteLoaded = false;
		$scope.isPointLoaded = false;
		$scope.LoadRoutesTab = function () {
			if (!$scope.isRouteLoaded) {
				$scope.GetAllTransportRouteList();
				$scope.isRouteLoaded = true;
            }
		}

		$scope.LoadPointsTab = function () {
			if (!$scope.isPointLoaded) {
				$scope.GetAllTransportPointList();
				$scope.isPointLoaded = true;
            }
        }
		try {
			var script = document.createElement('script');
			script.type = 'text/javascript';
			script.src = 'https://maps.googleapis.com/maps/api/js?key=' + API_KEY+'&callback=initMap';
			document.body.appendChild(script);
			//setTimeout(function () {
			//	$scope.initMap();
			//}, 500);

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
		document.getElementById('transport-route-form').style.display = "none";
		document.getElementById('transport-point-form').style.display = "none";	

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
		// Transport Route
		document.getElementById('add-transport-route').onclick = function () {
		document.getElementById('transport-route-section').style.display = "none";
			document.getElementById('transport-route-form').style.display = "block";
			$scope.ClearTransportRoute();
		}

		document.getElementById('back-transport-route').onclick = function () {
		document.getElementById('transport-route-section').style.display = "block";
			document.getElementById('transport-route-form').style.display = "none";
			$scope.ClearTransportRoute();
		}
		// Transport Point
		document.getElementById('add-transport-point').onclick = function () {
		document.getElementById('transport-point-section').style.display = "none";
			document.getElementById('transport-point-form').style.display = "block";
			$scope.ClearTransportPoint();
		}

		document.getElementById('back-transport-point').onclick = function () {
		document.getElementById('transport-point-section').style.display = "block";
			document.getElementById('transport-point-form').style.display = "none";
			$scope.ClearTransportPoint();
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
			NaamsariMiti_TMP: new Date(),
			RenewalMiti_TMP: new Date(),
			ManufacturingYearId: null,
			Photo: null,
			PhotoPath: null,
			JachpassNo: '',
			ValidFrom_TMP: new Date(),
			ValidTo_TMP: new Date(),
			JachpassRemarks: '',
			InsuranceNo: '',
			InsuranceValidFrom_TMP: new Date(),
			InsuranceValidTo_TMP: new Date(),
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

	$scope.ClearTransportRoute = function () {
		$scope.newTransportRoute = {
			RouteId: null,
			Name: '',
			VehicleId: null,
			FuelConsumeId: null,
			FuelConsume: '',
			FuelConsumeInLitre: '',
			ArrivalTime_TMP: null,
			DepartureTime_TMP: null,
			//Lat: null,
			//Lan: null,
			//EndLat: null,
			//EndLan: null,
			//D_Lat: null,
			//D_Lan: null,
			//D_EndLat: null,
			//D_EndLan: null,
			Mode: 'Save'
		};
	}

	$scope.ClearTransportPoint = function () {

		$timeout(function () {
			$scope.newTransportPoint = {
				PointId: null,
				Name: '',
				RouteId: null,
				PickupRate: 0,
				DropRate: 0,
				BothRate: 0,
				Latitude: 0,
				Longitude: 0,
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});

		$timeout(function () {
			var ethin = [];			
			$('#cboPointRoutes').val(ethin).trigger('change');
		});
		
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
			GlobalServices.validateAndFocus('VDVehicleName', 'Please ! Enter Vehicle Name', false);
			return false;
		}
		if (!$scope.newVehicleDetails.VehicleNumber) {
			GlobalServices.validateAndFocus('VDVehicleNumber', 'Please ! Enter Vehicle No', false);
			return false;
		}
		if (!$scope.newVehicleDetails.FuelEngineType) {
			GlobalServices.validateAndFocus('VDFuelEngineType', 'Please ! Select Fuel Engine Type', true);
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
			data: { jsonData: $scope.newVehicleDetails, files: filesColl, stPhoto: photo  }
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
	//************************* Transport Route *********************************

	$scope.IsValidTransportRoute = function () {
		if ($scope.newTransportRoute.Name.isEmpty()) {
			GlobalServices.validateAndFocus('TRRouteName', 'Please ! Enter Route Name', false);
			return false;
		}
		if (!$scope.newTransportRoute.VehicleId) {
			GlobalServices.validateAndFocus('TRVehicle', 'Please ! Select Vehicle', true);
			return false;
		}
		//if ($scope.newTransportRoute.FuelConsume.isEmpty()) {
		//	GlobalServices.validateAndFocus('TRFuelConsume', 'Please ! Enter In Liter', false);
		//	return false;
		//}
		//if (!$scope.newTransportRoute.Lat) {
		//	GlobalServices.validateAndFocus('TRLat', 'Please ! Enter Pickup Start Point(Lat)', false);
		//	return false;
		//}
		//if (!$scope.newTransportRoute.Lan) {
		//	GlobalServices.validateAndFocus('TRLan', 'Please ! Enter Pickup Start Point(Lan)', false);
		//	return false;
		//}
		//if (!$scope.newTransportRoute.EndLat) {
		//	GlobalServices.validateAndFocus('TREndLat', 'Please ! Enter Pickup End Point(Lat)', false);
		//	return false;
		//}
		//if (!$scope.newTransportRoute.EndLan) {
		//	GlobalServices.validateAndFocus('TREndLan', 'Please ! Enter Pickup End Point(Lan)', false);
		//	return false;
		//}
		//if (!$scope.newTransportRoute.D_Lat) {
		//	GlobalServices.validateAndFocus('TRD_Lat', 'Please ! Enter Dropoff Start Point(Lat)', false);
		//	return false;
		//}
		//if (!$scope.newTransportRoute.D_Lan) {
		//	GlobalServices.validateAndFocus('TRD_Lan', 'Please ! Enter Dropoff Start Point(Lan)', false);
		//	return false;
		//}
		//if (!$scope.newTransportRoute.D_EndLat) {
		//	GlobalServices.validateAndFocus('TRD_EndLat', 'Please ! Enter Dropoff End Point(Lat)', false);
		//	return false;
		//}
		//if (!$scope.newTransportRoute.D_EndLan) {
		//	GlobalServices.validateAndFocus('TRD_EndLan', 'Please ! Enter Dropoff End Point(Lan)', false);
		//	return false;
		//}
		//if ($scope.newTransportRoute.RouteId) {
		//	if (!$scope.newTransportRoute.ArrivalTime_TMP) {
		//		GlobalServices.validateAndFocus('TRArrivalTime', 'Please ! Enter Pickup Start Time', false);
		//		return false;
		//	}
		//	if (!$scope.newTransportRoute.DepartureTime_TMP) {
		//		GlobalServices.validateAndFocus('TRDepartureTime', 'Please ! Enter Pickup End time', false);
		//		return false;
		//	}
		//	if (!$scope.newTransportRoute.D_ArrivalTime_TMP) {
		//		GlobalServices.validateAndFocus('TRDArrivalTime', 'Please ! Enter Dropoff Start Time', false);
		//		return false;
		//	}
		//	if (!$scope.newTransportRoute.D_DepartureTime_TMP) {
		//		GlobalServices.validateAndFocus('TRDDepartureTime', 'Please ! Enter Dropoff End time', false);
		//		return false;
		//	}
		//}

		return true;
	}

	
	$scope.SaveUpdateTransportRoute = function () {
		if ($scope.IsValidTransportRoute() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newTransportRoute.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateTransportRoute();
					}
				});
			} else
				$scope.CallSaveUpdateTransportRoute();

		}
	};

	$scope.CallSaveUpdateTransportRoute = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newTransportRoute.ArrivalTime_TMP)
			$scope.newTransportRoute.ArrivalTime = $scope.newTransportRoute.ArrivalTime_TMP.toLocaleString();
		else
			$scope.newTransportRoute.ArrivalTime = null;

		if ($scope.newTransportRoute.DepartureTime_TMP)
			$scope.newTransportRoute.DepartureTime = $scope.newTransportRoute.DepartureTime_TMP.toLocaleString();
		else
			$scope.newTransportRoute.DepartureTime = null;


		if ($scope.newTransportRoute.D_ArrivalTime_TMP)
			$scope.newTransportRoute.D_ArrivalTime = $scope.newTransportRoute.D_ArrivalTime_TMP.toLocaleString();
		else
			$scope.newTransportRoute.D_ArrivalTime = null;

		if ($scope.newTransportRoute.D_DepartureTime_TMP)
			$scope.newTransportRoute.D_DepartureTime = $scope.newTransportRoute.D_DepartureTime_TMP.toLocaleString();
		else
			$scope.newTransportRoute.D_DepartureTime = null;

		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/SaveTransportRoute",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newTransportRoute }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearTransportRoute();
				$scope.GetAllTransportRouteList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllTransportRouteList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TransportRouteList = [];
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetAllTransportRouteList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TransportRouteList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetTransportRouteById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TransportRouteId: refData.RouteId
		};
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetTransportRouteById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newTransportRoute = res.data.Data;
				$scope.newTransportRoute.Mode = 'Modify';

				if ($scope.newTransportRoute.ArrivalTime)
					$scope.newTransportRoute.ArrivalTime_TMP = new Date($scope.newTransportRoute.ArrivalTime);
				else
					$scope.newTransportRoute.ArrivalTime_TMP = null;

				if ($scope.newTransportRoute.DepartureTime)
					$scope.newTransportRoute.DepartureTime_TMP = new Date($scope.newTransportRoute.DepartureTime);
				else
					$scope.newTransportRoute.DepartureTime_TMP = null;


				if ($scope.newTransportRoute.D_ArrivalTime)
					$scope.newTransportRoute.D_ArrivalTime_TMP = new Date($scope.newTransportRoute.D_ArrivalTime);
				else
					$scope.newTransportRoute.D_ArrivalTime_TMP = null;

				if ($scope.newTransportRoute.D_DepartureTime)
					$scope.newTransportRoute.D_DepartureTime_TMP = new Date($scope.newTransportRoute.D_DepartureTime);
				else
					$scope.newTransportRoute.D_DepartureTime_TMP = null;

				document.getElementById('transport-route-section').style.display = "none";
				document.getElementById('transport-route-form').style.display = "block";

				

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelTransportRouteById = function (refData) {
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
					TransportRouteId: refData.RouteId
				};
				$http({
					method: 'POST',
					url: base_url + "Transport/Creation/DelTransportRoute",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllTransportRouteList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

	//*************************TransportPoint *********************************
	$scope.IsValidTransportPoint = function () {
		if ($scope.newTransportPoint.Name.isEmpty()) {
			Swal.fire('Please ! Enter Point Name');
			return false;
		}
		return true;
	}

	$scope.IsValidTransportPoint = function () {
		if ($scope.newTransportPoint.Name.isEmpty()) {
			GlobalServices.validateAndFocus('TPPointName', 'Please ! Enter Point Name', false);
			return false;
		}
		if (!$scope.newTransportPoint.RouteIdColl || $scope.newTransportPoint.RouteIdColl.length === 0) {
			GlobalServices.validateAndFocus('cboPointRoutes', 'Please ! Select Route', true);
			return false;
		}
		return true;
	}

	$scope.SaveUpdateTransportPoint = function () {
		if ($scope.IsValidTransportPoint() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newTransportPoint.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateTransportPoint();
					}
				});
			} else
				$scope.CallSaveUpdateTransportPoint();
		}
	};

	$scope.CallSaveUpdateTransportPoint = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/SaveTransportPoint",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newTransportPoint }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearTransportPoint();
				$scope.GetAllTransportPointList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllTransportPointList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TransportPointList = [];
		//$scope.GetAllTransportRouteList();
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetAllTransportPointList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TransportPointList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetTransportPointById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TransportPointId: refData.PointId
		};
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetTransportPointById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newTransportPoint = res.data.Data;
				$scope.newTransportPoint.Mode = 'Modify';
				document.getElementById('transport-point-section').style.display = "none";
				document.getElementById('transport-point-form').style.display = "block";

				$timeout(function () {
					var ethin = [];
					angular.forEach($scope.newTransportPoint.RouteIdColl, function (edet) {
						ethin.push(edet);
					})
					$('#cboPointRoutes').val(ethin).trigger('change');
				});

				if (map) {
					if ($scope.newTransportPoint.Lat != 0) {
						var marker = new google.maps.Marker({
							position: { lat: $scope.newTransportPoint.Lat, lng: $scope.newTransportPoint.Lan },
							map: map,
							title: 'Pickup Point ',
						});
					}					 
				}

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelTransportPointById = function (refData) {
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
					TransportPointId: refData.PointId
				};
				$http({
					method: 'POST',
					url: base_url + "Transport/Creation/DelTransportPoint",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllTransportPointList();
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


	$scope.ShowMap = function (mapType) {
		
		$scope.newTransportRoute.MapType = mapType;
		$scope.newTransportPoint.MapType = mapType;

		if (!map) {
			$scope.initMap();
        }
		angular.forEach($scope.markerColl, function (mk) {
			mk.setMap(null);
		});

		$scope.CurLatLng = {
			Lat: '',
			Lng: ''
		};

		if (map) {
			var marker = null;
			if ($scope.newTransportRoute.Lat != 0 && $scope.newTransportRoute.MapType == 1) {

				$scope.CurLatLng.Lat = $scope.newTransportRoute.Lat;
				$scope.CurLatLng.Lng = $scope.newTransportRoute.Lan;

				marker = new google.maps.Marker({
					position: { lat: $scope.newTransportRoute.Lat, lng: $scope.newTransportRoute.Lan },
					map: map,
					title: 'Pickup Start Point ',							
				});
			 }

			if ($scope.newTransportRoute.EndLat != 0 && $scope.newTransportRoute.MapType == 2) {

				$scope.CurLatLng.Lat = $scope.newTransportRoute.EndLat;
				$scope.CurLatLng.Lng = $scope.newTransportRoute.EndLan;

				marker = new google.maps.Marker({
					position: { lat: $scope.newTransportRoute.EndLat, lng: $scope.newTransportRoute.EndLan },
					map: map,
					title: 'Pickup End Point ',
				});
			}

			if ($scope.newTransportRoute.D_Lat != 0 && $scope.newTransportRoute.MapType == 4) {

				$scope.CurLatLng.Lat = $scope.newTransportRoute.D_Lat;
				$scope.CurLatLng.Lng = $scope.newTransportRoute.D_Lan;

				marker = new google.maps.Marker({
					position: { lat: $scope.newTransportRoute.D_Lat, lng: $scope.newTransportRoute.D_Lan },
					map: map,
					title: 'Drop Start Point ',
				});
			}

			if ($scope.newTransportRoute.D_EndLat != 0 && $scope.newTransportRoute.MapType == 5) {

				$scope.CurLatLng.Lat = $scope.newTransportRoute.D_EndLat;
				$scope.CurLatLng.Lng = $scope.newTransportRoute.D_EndLan;

				marker = new google.maps.Marker({
					position: { lat: $scope.newTransportRoute.D_EndLat, lng: $scope.newTransportRoute.D_EndLan },
					map: map,
					title: 'Drop End Point ',
				});
			}

			if ($scope.newTransportPoint.Lat != 0 && $scope.newTransportPoint.MapType == 3) {

				$scope.CurLatLng.Lat = $scope.newTransportPoint.Lat;
				$scope.CurLatLng.Lng = $scope.newTransportPoint.Lan;

				marker = new google.maps.Marker({
					position: { lat: $scope.newTransportPoint.Lat, lng: $scope.newTransportPoint.Lan },
					map: map,
					title: 'Pickup Point ',
				});
			}

			if (marker != null)
				$scope.markerColl.push(marker);
		}

		$('#modal-map').modal('show');
	}
	var map;

	$scope.getMapTitle = function (mapType) {
		if (mapType == 1)
			return "Pickup Start Point";
		else if (mapType == 2)
			return "Pickup End Point";
		else if (mapType == 3)
			return "Pickup Point";
		else if (mapType == 4)
			return "Dropoff Start Point";
		else if (mapType == 5)
			return "Dropoff End Point";
		else
			return "Current Location";
	}
	$scope.markerColl = [];
	$scope.initMap=function()
	{
		try {

			var latitude = 27.7172453; // YOUR LATITUDE VALUE
			var longitude = 85.3239605; // YOUR LONGITUDE VALUE

			if (navigator.geolocation) {
				navigator.geolocation.getCurrentPosition(
					function (position) {
						latitude = position.coords.latitude;
						longitude = position.coords.longitude;

						var myLatLng = { lat: latitude, lng: longitude };

						map = new google.maps.Map(document.getElementById('map'), {
							center: myLatLng,
							zoom: 14,
							disableDoubleClickZoom: true, // disable the default map zoom on double click
						});

						var marker = new google.maps.Marker({
							position: myLatLng,
							map: map,
							title: $scope.getMapTitle($scope.newTransportRoute.MapType)

							// setting latitude & longitude as title of the marker
							// title is shown when you hover over the marker
							//title: latitude + ', ' + longitude
						});

						// Update lat/long value of div when the marker is clicked
						marker.addListener('click', function (event) {
							//document.getElementById('latclicked').innerHTML = event.latLng.lat();
							//document.getElementById('longclicked').innerHTML = event.latLng.lng();
							$timeout(function () {

								$scope.CurLatLng.Lat = event.latLng.lat();
								$scope.CurLatLng.Lng = event.latLng.lng();

								if ($scope.newTransportRoute.MapType == 1) {
									$scope.newTransportRoute.Lat = event.latLng.lat();
									$scope.newTransportRoute.Lan = event.latLng.lng();
								} else if ($scope.newTransportRoute.MapType == 2) {
									$scope.newTransportRoute.EndLat = event.latLng.lat();
									$scope.newTransportRoute.EndLan = event.latLng.lng();
								} else if ($scope.newTransportRoute.MapType == 3 || $scope.newTransportPoint.MapType == 3) {
									$scope.newTransportPoint.Lat = event.latLng.lat();
									$scope.newTransportPoint.Lan = event.latLng.lng();
								} else if ($scope.newTransportRoute.MapType == 4) {
									$scope.newTransportRoute.D_Lat = event.latLng.lat();
									$scope.newTransportRoute.D_Lan = event.latLng.lng();
								}
								else if ($scope.newTransportRoute.MapType == 5) {
									$scope.newTransportRoute.D_EndLat = event.latLng.lat();
									$scope.newTransportRoute.D_EndLan = event.latLng.lng();
								}

							})
						});
						$scope.markerColl.push(marker);

						// Create new marker on double click event on the map
						google.maps.event.addListener(map, 'dblclick', function (event) {

							angular.forEach($scope.markerColl, function (mk) {
								mk.setMap(null);
							});

							var marker = new google.maps.Marker({
								position: event.latLng,
								map: map,
								//title: event.latLng.lat() + ', ' + event.latLng.lng()
								title: $scope.getMapTitle($scope.newTransportRoute.MapType)
							});

							// Update lat/long value of div when the marker is clicked
							marker.addListener('click', function () {


								$timeout(function () {

									$scope.CurLatLng.Lat = event.latLng.lat();
									$scope.CurLatLng.Lng = event.latLng.lng();

									if ($scope.newTransportRoute.MapType == 1) {
										$scope.newTransportRoute.Lat = event.latLng.lat();
										$scope.newTransportRoute.Lan = event.latLng.lng();
									} else if ($scope.newTransportRoute.MapType == 2) {
										$scope.newTransportRoute.EndLat = event.latLng.lat();
										$scope.newTransportRoute.EndLan = event.latLng.lng();
									} else if ($scope.newTransportRoute.MapType == 3 || $scope.newTransportPoint.MapType == 3) {
										$scope.newTransportPoint.Lat = event.latLng.lat();
										$scope.newTransportPoint.Lan = event.latLng.lng();
									} else if ($scope.newTransportRoute.MapType == 4) {
										$scope.newTransportRoute.D_Lat = event.latLng.lat();
										$scope.newTransportRoute.D_Lan = event.latLng.lng();
									}
									else if ($scope.newTransportRoute.MapType == 5) {
										$scope.newTransportRoute.D_EndLat = event.latLng.lat();
										$scope.newTransportRoute.D_EndLan = event.latLng.lng();
									}
								})
							});
							$scope.markerColl.push(marker);
						});

					},
					function (position) {
						//alert('Error111');
					});
			}
			else {
				Swal.fire('It seems like Geolocation, which is required for this page, is not enabled in your browser.');
			}

		} catch { }
		 
	}

	$scope.EditTransportPoint = function (cl) {
		cl.IsEdit = true;
		// Backup original values
		cl.OldInRate = cl.InRate;
		cl.OldOutRate = cl.OutRate;
		cl.OldBothRate = cl.BothRate;
	};

	$scope.CancelEditTransportPoint = function (cl) {
		cl.InRate = cl.OldInRate;
		cl.OutRate = cl.OldOutRate;
		cl.BothRate = cl.OldBothRate;
		cl.IsEdit = false;
	};

	$scope.UpdateTransportPoint = function (refData) {
		Swal.fire({
			title: 'Update Transport Point Rate?',
			text: 'This action will update the transport rates and automatically apply the changes to all associated Transport Mapping records. Do you want to continue?',
			icon: 'warning',
			showCancelButton: true,
			confirmButtonText: 'Update',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					TransportPointId: refData.PointId,
					InRate: refData.InRate,
					OutRate: refData.OutRate,
					BothRate: refData.BothRate
				};
				$http({
					method: 'POST',
					url: base_url + "Transport/Creation/UpdateTransportPoint",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllTransportPointList();
						Swal.fire({ icon: 'success', title: 'Updated Successfully', text: res.data.ResponseMSG });
					} else {
						Swal.fire({ icon: 'error', title: 'Update Failed', text: res.data.ResponseMSG });
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

	$scope.CurTransportRate = function () {
		Swal.fire({
			title: 'Apply Transport Point Rate?',
			text: 'This action will update the transport rates and automatically apply the changes to all associated Transport Mapping records. Do you want to continue?',
			icon: 'warning',
			showCancelButton: true,
			confirmButtonText: 'Apply',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				$http({
					method: 'POST',
					url: base_url + "Transport/Creation/CurTransportRate",
					dataType: "json"
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllTransportPointList();
						Swal.fire({ icon: 'success', title: 'Applied Successfully', text: res.data.ResponseMSG });
					} else {
						Swal.fire({ icon: 'error', title: 'Apply Failed', text: res.data.ResponseMSG });
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

	//*********************From and To Date Validation*************************
	$scope.validateDate = function (obj, startField, endField, startLabel, endLabel) {
		var res = GlobalServices.validateDate(obj, startField, endField, startLabel, endLabel);
		if (res.IsSuccess == false) {
			Swal.fire({
				icon: 'warning',
				text: res.Message,
				confirmButtonText: 'OK'
			}).then(function () {
				obj.NaamsariMiti_TMP = new Date();
				obj.RenewalMiti_TMP = new Date();
				obj.ValidFrom_TMP= new Date(),
				obj.ValidTo_TMP= new Date(),
				obj.InsuranceValidFrom_TMP= new Date(),
				obj.InsuranceValidTo_TMP= new Date(),
				$scope.$applyAsync();
			});
		}
	};
});