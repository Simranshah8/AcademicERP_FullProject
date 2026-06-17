
app.controller('GeneralInformationController', function ($scope, $http, $timeout, $filter, $translate, $rootScope, GlobalServices) {
	OnClickDefault();
	$scope.Title = 'General Information';
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.GenderList = GlobalServices.getGenderList();
		$scope.ProvinceColl = GetStateList();
		$scope.DistrictColl = GetDistrictList();
		$scope.LocalLevelColl = GetVDCList();
		$scope.ProvinceColl_Qry = mx($scope.ProvinceColl);
		$scope.DistrictColl_Qry = mx($scope.DistrictColl);

		$scope.CasteList = [];
		GlobalServices.getCasteList().then(function (res) {
			$scope.CasteList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		//$scope.GeneralInfoFacilitiesColl = [];
		//$http({
		//	method: 'POST',
		//	url: base_url + "Infrastructure/Setup/GetAllFacilities",
		//	dataType: "json"
		//}).then(function (res) {
		//	hidePleaseWait();
		//	$scope.loadingstatus = "stop";
		//	if (res.data.IsSuccess && res.data.Data) {
		//		$scope.newGeneralInfoDet.GeneralInfoFacilitiesColl = res.data.Data;
		//	} else {
		//		Swal.fire(res.data.ResponseMSG);
		//	}
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});


		$scope.newGeneralInfoDet = {
			Name: '',
			YearOfEstablishment: '',
			ContactNo: '',
			Email: '',
			AffiliatedTo: '',
			ProvinceId: null,
			DistrictId: null,
			LocalLevelId: null,
			Province: '',
			District: '',
			LocalLevel: '',
			WardNo: '',
			Tole: '',
			//DistanceFromNearestCity: '',
			MajorLandmarks: '',
			//DistanceFromHeadquarters: '',
			CampusChiefName: '',
			CampusChiefContactNo: '',
			CampusChiefEmail: '',
			CampusChiefQualification: '',
			CampusChiefGender: null,
			CampusChiefCaste: null,
			IsCCTeaching: false,
			CCAppointmentDate_TMP: '',
			CampusITName: '',
			CampusITContactNo: '',
			CampusITEmail: '',
			CampusITQualification: '',
			CampusITGender: null,
			CampusITCasteId: null,
			IsCampusITTeaching: false,
			CampusITAppointmentDate_TMP: '',
			ElectricitySupplyId: null,
			RegularWaterSupplyId: null,
			InternetFacilityId: null,
			OfficialSchoolTelephone: '',
			SewereageSystemId: null,
			IsLandOwnershipCertificate: false,
			LandOwnershipTypeId: null,
			LandAreaSqm: null,
			LandAreaRopani: null,
			LandAreaBigha: null,
			IsAllWeatherRoad: false,
			SiteOrientationId: null,
			RoadWidth: null,
			WalkingDistanceMeter: null,
			WalkingDistanceMin: null,
			IsInternetFacility: false,
			IsRegularWaterSupply: false,
			IsElectricity: false,
			IsOfficialSchoolTelephone: false,
			IsVehicleAccessibility: false
		}

		$scope.ElectricitySupplySourceColl = [
			{ id: 1, text: 'Solar' },
			{ id: 2, text: 'Generator' },
			{ id: 3, text: 'Inverter' },
			{ id: 4, text: 'Transmission Line' },
			{ id: 5, text: 'Local Hydropower' }
		];

		$scope.RegularWaterSupplySourceColl = [
			{ id: 1, text: 'River' },
			{ id: 2, text: 'Tubewell' },
			{ id: 3, text: 'Public Tap' },
			{ id: 4, text: 'Well' },
			{ id: 5, text: 'Pond' },
			{ id: 6, text: 'Municipal Tap Water' },
			{ id: 7, text: 'Schools Own Source Tap' }
		];
		$scope.InternetFacilityColl = [
			{ id: 1, text: 'Mobile Internet' },
			{ id: 2, text: 'Wireless BroadBand' },
			{ id: 3, text: 'Fiber To The Home (FTTH)' },
			{ id: 4, text: 'Fiber/Cable Network' },
			{ id: 5, text: 'Wireless Network' }
		];
		$scope.SewerageSystemColl = [
			{ id: 1, text: 'Drainage' },
			{ id: 2, text: 'Pipe' },
			{ id: 3, text: 'Septic Tank' },
			{ id: 4, text: 'Direct to the River/Stream' },
			{ id: 5, text: 'To the Open Area' }
		];
		$scope.LandOwnershipTypeColl = [
			{ id: 1, text: 'Govenment' },
			{ id: 2, text: 'Institutional' },
			{ id: 3, text: 'Private' },
			{ id: 4, text: 'Ailani/Parti' },
			{ id: 5, text: 'Guthi(Trust)' },
			{ id: 6, text: 'Guthi(Private)' }
		];
		$scope.SiteOrientationColl = [
			{ id: 1, text: 'South East' },
			{ id: 2, text: 'South West' },
			{ id: 3, text: 'East' },
			{ id: 4, text: 'West' },
			{ id: 5, text: 'North' },
			{ id: 6, text: 'South' },
			{ id: 7, text: 'North East' },
			{ id: 8, text: 'North West' }
		];
		$scope.RoadTypeColl = [
			{ id: 1, text: 'Metal Road' },
			{ id: 2, text: 'Gravel Road' },
			{ id: 3, text: 'Earthen Road' },
			{ id: 4, text: 'Dirt Road' },
			{ id: 5, text: 'Footpath Way' }
		];

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


		$scope.ClassList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassList = res.data.Data.filter(function (cls) {
					return cls.IsActive === true;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllGeneralInfoList();
	}

	$scope.ClearGeneralInfo = function () {
		$scope.newGeneralInfoDet = {
			Name: '',
			YearOfEstablishment: '',
			ContactNo: '',
			Email: '',
			AffiliatedTo: '',
			ProvinceId: null,
			DistrictId: null,
			LocalLevelId: null,
			Province: '',
			District: '',
			LocalLevel: '',
			WardNo: '',
			Tole: '',
			//DistanceFromNearestCity: '',
			MajorLandmarks: '',
			//DistanceFromHeadquarters: '',
			CampusChiefName: '',
			CampusChiefContactNo: '',
			CampusChiefEmail: '',
			CampusChiefQualification: '',
			CampusChiefGender: null,
			CampusChiefCaste: null,
			IsCCTeaching: false,
			CCAppointmentDate_TMP: '',
			CampusITName: '',
			CampusITContactNo: '',
			CampusITEmail: '',
			CampusITQualification: '',
			CampusITGender: null,
			CampusITCasteId: null,
			IsCampusITTeaching: false,
			CampusITAppointmentDate_TMP: '',
			ElectricitySupplyId: null,
			RegularWaterSupplyId: null,
			InternetFacilityId: null,
			OfficialSchoolTelephone: '',
			SewereageSystemId: null,
			IsLandOwnershipCertificate: false,
			LandOwnershipTypeId: null,
			LandAreaSqm: null,
			LandAreaRopani: null,
			LandAreaBigha: null,
			IsAllWeatherRoad: false,
			SiteOrientationId: null,
			RoadWidth: null,
			WalkingDistanceMeter: null,
			WalkingDistanceMin: null,
			IsInternetFacility: false,
			IsRegularWaterSupply: false,
			IsElectricity: false,
			IsOfficialSchoolTelephone: false,
			IsVehicleAccessibility: false
		}

	}

	$scope.initMap = function () {
		console.log("initMap function is being triggered");

		// Default location
		var latitude = 27.7172453;
		var longitude = 85.3239605;

		if (navigator.geolocation) {
			navigator.geolocation.getCurrentPosition(
				function (position) {
					latitude = position.coords.latitude;
					longitude = position.coords.longitude;

					var myLatLng = { lat: latitude, lng: longitude };

					// Initialize Google Map
					var map = new google.maps.Map(document.getElementById('map'), {
						center: myLatLng,
						zoom: 14
					});

					// Create initial marker
					var marker = new google.maps.Marker({
						position: myLatLng,
						map: map
					});

					// Update Latitude and Longitude in the model
					$scope.$apply(function () {
						$scope.newDet.Latitude = latitude;
						$scope.newDet.Longitude = longitude;
					});

					// Marker click listener
					marker.addListener('click', function (event) {
						$scope.$apply(function () {
							$scope.newDet.Latitude = event.latLng.lat();
							$scope.newDet.Longitude = event.latLng.lng();
						});
					});

					// Double-click to change location
					google.maps.event.addListener(map, 'dblclick', function (event) {
						// Remove all existing markers
						angular.forEach($scope.markerColl, function (mk) {
							mk.setMap(null);
						});

						// Place new marker
						var newMarker = new google.maps.Marker({
							position: event.latLng,
							map: map
						});

						// Update model with new position
						$scope.$apply(function () {
							$scope.newDet.Latitude = event.latLng.lat();
							$scope.newDet.Longitude = event.latLng.lng();
						});

						// Save the new marker
						$scope.markerColl.push(newMarker);
					});
				},
				function () {
					alert('Geolocation is not enabled or not available.');
				}
			);
		} else {
			alert('Geolocation is not supported in your browser.');
		}
	};

	function OnClickDefault() {
		document.getElementById('preview-form').style.display = "none";

	
		
	}


	$scope.ChangeCollegeChief = function () {
		
		if ($scope.newGeneralInfoDet.CampusChiefId) {

			var para = {
				EmployeeId: $scope.newGeneralInfoDet.CampusChiefId
			};

			$scope.newChief = [];

			$http({
				method: 'POST',
				url: base_url + "Infrastructure/Creation/getEmpShortDetById",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

				if (res.data.IsSuccess && res.data.Data) {
					$scope.newChief = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire('Failed: ' + reason);
			});
		}
	};

	$scope.ChangeCollegeIT = function () {

		if ($scope.newGeneralInfoDet.CampusITId) {

			var para = {
				EmployeeId: $scope.newGeneralInfoDet.CampusITId
			};

			$scope.newIT = [];

			$http({
				method: 'POST',
				url: base_url + "Infrastructure/Creation/getEmpShortDetById",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

				if (res.data.IsSuccess && res.data.Data) {
					$scope.newIT = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire('Failed: ' + reason);
			});
		}
	};

	

	$scope.GetAllGeneralInfoList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.GeneralInfoList = [];

		$http({
			method: 'POST',
			url: base_url + "Infrastructure/Creation/GetAllGeneralInformation",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				$scope.newGeneralInfoDet = res.data.Data;
				$scope.newGeneralInfoDet.GeneralInfoFacilitiesColl = res.data.Data.GeneralInfoFacilitiesColl;
				angular.forEach($scope.newGeneralInfoDet.GeneralInfoFacilitiesColl, function (sub) {
					
					sub.SourceName = sub.SourceName || '';
					sub.IsAvailable = sub.IsAvailable || false;

					
					if (sub.IsAvailable) {
						$scope.GetFacilitiesById(sub);
					}
				});

				if ($scope.newGeneralInfoDet.CCAppointmentDate)
					$scope.newGeneralInfoDet.CCAppointmentDate_TMP = new Date($scope.newGeneralInfoDet.CCAppointmentDate);

				if ($scope.newGeneralInfoDet.CampusITAppointmentDate)
					$scope.newGeneralInfoDet.CampusITAppointmentDate_TMP = new Date($scope.newGeneralInfoDet.CampusITAppointmentDate);



				$timeout(function () {
					$scope.ChangeCollegeChief();
				});

				$timeout(function () {
					$scope.ChangeCollegeIT();
				});
				$timeout(function () {
					$scope.ChangeLandOwnershipType();
				});
				$timeout(function () {
					$scope.ChangeSiteOrientation();
				});
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	// Function to fetch facility details when checkbox changes
	$scope.GetFacilitiesById = function (facility) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			FacilitiesId: facility.FacilitiesId
		};

		$http({
			method: 'POST',
			url: base_url + "Infrastructure/Setup/getFacilitiesById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				
				var selectedSource = facility.SourceName;
				facility.SourceList = res.data.Data.SubFacilitiesColl;

			
				if (selectedSource) {
					var matchedSource = facility.SourceList.find(s => s.Name === selectedSource);
					facility.SourceName = matchedSource ? matchedSource.Name : '';
				}
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	// Handle checkbox change
	$scope.handleFacilityChange = function (facility) {
		if (facility.IsAvailable) {
			$scope.GetFacilitiesById(facility);
		} else {
			facility.SourceList = []; 
			facility.SourceName = ''; 
		}
	};



	$scope.IsValidGeneralInfo = function () {
		if ($scope.newGeneralInfoDet.Tole.isEmpty()) {
			Swal.fire('Please ! Choose Tole Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateGeneralInfo = function () {
		if ($scope.IsValidGeneralInfo() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newGeneralInfoDet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateGeneralInfo();
					}
				});
			} else
				$scope.CallSaveUpdateGeneralInfo();
		}
	};

	$scope.CallSaveUpdateGeneralInfo = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.newGeneralInfoDet.CompanyId = $scope.newCompanyDet.CompanyId;

		if ($scope.newGeneralInfoDet.CCAppointmentDateDet)
			$scope.newGeneralInfoDet.CCAppointmentDate = $filter('date')(new Date($scope.newGeneralInfoDet.CCAppointmentDateDet.dateAD), 'yyyy-MM-dd');

		if ($scope.newGeneralInfoDet.CampusITAppointmentDateDet)
			$scope.newGeneralInfoDet.CampusITAppointmentDate = $filter('date')(new Date($scope.newGeneralInfoDet.CampusITAppointmentDateDet.dateAD), 'yyyy-MM-dd');

		var selectData1 = $('#cboProvince').select2('data');
		if (selectData1 && selectData1.length > 0)
			province1 = selectData1[0].text.trim();

		selectData1 = $('#cboDistrict').select2('data');
		if (selectData1 && selectData1.length > 0)
			district1 = selectData1[0].text.trim();


		selectData1 = $('#cboArea').select2('data');
		if (selectData1 && selectData1.length > 0)
			area1 = selectData1[0].text.trim();

		$scope.newGeneralInfoDet.Province = province1;
		$scope.newGeneralInfoDet.District = district1;
		$scope.newGeneralInfoDet.LocalLevel = area1;


		angular.forEach($scope.newGeneralInfoDet.GeneralInfoFacilitiesColl, function (sub) {
			sub.SNo = sub.SNo; // Add SNo property
		});

		$http({
			method: 'POST',
			url: base_url + "Infrastructure/Creation/SaveUpdateGeneralInformation",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newGeneralInfoDet }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearSetup();
				$scope.GetAllGeneralInfoList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	$scope.ChangeLandOwnershipType = function () {
		if ($scope.newGeneralInfoDet.LandOwnershipTypeId > 0) {
			$scope.newGeneralInfoDet.LandOwnershipType = mx($scope.LandOwnershipTypeColl).firstOrDefault(p1 => p1.id == $scope.newGeneralInfoDet.LandOwnershipTypeId).text;
		} else
			$scope.newGeneralInfoDet.LandOwnershipType = '';
	}

	$scope.ChangeSiteOrientation = function () {
		if ($scope.newGeneralInfoDet.SiteOrientationId > 0) {
			$scope.newGeneralInfoDet.SiteOrientation = mx($scope.SiteOrientationColl).firstOrDefault(p1 => p1.id == $scope.newGeneralInfoDet.SiteOrientationId).text;
		} else
			$scope.newGeneralInfoDet.SiteOrientation = '';
	}

	$scope.PrintData = function () {
		$('#printcard').printThis();
	}

	

});