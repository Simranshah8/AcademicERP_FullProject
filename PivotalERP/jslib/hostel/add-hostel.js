app.controller('AddHostelController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Add Hostel';
	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();
		$scope.OverallConditionColl = [
			{ id: 1, text: 'Very Good/No Damage' },
			{ id: 2, text: 'Good/No Damage' },
			{ id: 3, text: 'Normal Rash/Damage Grade 1' },
			{ id: 4, text: 'Cracked/ Damage Grade 2 & 3' },
			{ id: 5, text: 'Very Dilapidated/Damage Grade 4 & 5' },
		];

		$scope.StructureTypeColl = [
			{ id: 1, text: 'RCC Frame Structure' },
			{ id: 2, text: 'Brick Masonry in cement mortar with RCC Slab (Load Bearing Structur)' },
			{ id: 3, text: 'Stone Masonry in cement mortar with RCC Slab (Load Bearing Structur)' },
			{ id: 4, text: 'Brick Masonry in mud mortar with RCC Slab (Load Bearing Structur)' },
			{ id: 5, text: 'Stone Masonry in mud mortar with RCC Slab (Load Bearing Structur)' },
			{ id: 6, text: 'Brick Masonry with Steel Truss' },
			{ id: 7, text: 'Brick Masonry with c/c mortar' },
			{ id: 8, text: 'Stone Masonry with c/c mortar' },
			{ id: 9, text: 'Brick Masonry with mud mortar' },
			{ id: 10, text: 'Stone Masonry with mud mortar' },
			{ id: 11, text: 'Stone Frame with Steel Truss' },
			{ id: 12, text: 'Stone Masonry with Steel Truss' },
			{ id: 13, text: 'Wooden Structure' },
			{ id: 14, text: 'Others' },
		];

		$scope.RoofTypeColl = [
			{ id: 1, text: 'RCC Slab' },
			{ id: 2, text: 'CGI Sheet/UPVC Sheet' },
			{ id: 3, text: 'Timber (Wood)' },
			{ id: 4, text: 'Tile/Jhingati' },
			{ id: 5, text: 'Slate Stone' },
			{ id: 6, text: 'Khar/Bush' },
			{ id: 7, text: 'Steel Truss with CGI Sheet' },
			{ id: 8, text: 'Timber/Bamboo Structure with CGI Sheet' },
			{ id: 9, text: 'Others' },
		];

		$scope.DamageGradeColl = [
			{ id: 1, text: 'Completely damaged' },
			{ id: 2, text: 'Major damage' },
			{ id: 3, text: 'Minor damage' },
			{ id: 4, text: 'No damage' },
			{ id: 5, text: 'Damage Grade 1' },
			{ id: 6, text: 'Damage Grade 2' },
			{ id: 7, text: 'Damage Grade 3' },
			{ id: 8, text: 'Damage Grade 4' },
			{ id: 9, text: 'Damage Grade 5' },
		];

		$scope.InfrastructureTypeColl = [
			{ id: 1, text: 'Academic Classes' },
			{ id: 2, text: 'Toilet' },
			{ id: 3, text: 'Toilet for Disable' },
			{ id: 4, text: 'Hostel' },
			{ id: 5, text: 'Lab' },
			{ id: 6, text: 'Library' },
			{ id: 7, text: 'Multipurpose Hall' },
			{ id: 8, text: 'Canteen' },
			{ id: 9, text: 'Administrative Room' },
		];

		$scope.FundingSourceColl = [
			{ "id": 1, "text": "Government of Nepal (GON) Grants" },
			{ "id": 2, "text": "Ministry of Education, Science & Technology" },
			{ "id": 3, "text": "Provincial Government Grants" },
			{ "id": 4, "text": "Local Government Budget (Municipality/Rural Municipality)" },
			{ "id": 5, "text": "Presidential Educational Reform Program" },
			{ "id": 6, "text": "Parliamentary Development Funds" },
			{ "id": 7, "text": "Asian Development Bank (ADB)" },
			{ "id": 8, "text": "World Bank" },
			{ "id": 9, "text": "UNICEF/UNESCO Support" },
			{ "id": 10, "text": "Indian Government Grants" },
			{ "id": 11, "text": "Other International NGOs & Development Agencies" },
			{ "id": 12, "text": "Donor & Community Contributions" },
			{ "id": 13, "text": "Institution Own Funds" },
			{ "id": 14, "text": "Corporate Social Responsibility (CSR) Funds" },
			{ "id": 15, "text": "Reconstruction Grants (Post-Earthquake & Disaster Recovery)" },
			{ "id": 16, "text": "Public-Private Partnerships (PPP)" }
		];

		$scope.InterventionTypeColl = [
			{ id: 1, text: 'New Construction' },
			{ id: 2, text: 'Retrofitting' },
			{ id: 3, text: 'Rehabilitation ' },
			{ id: 4, text: 'Reconstruction' },
			{ id: 5, text: 'Retro' },
			{ id: 6, text: 'Existing' }
		];


		$scope.CompletionStatusColl = [
			{ id: 0, text: 'select Status' },
		];

		$scope.newCompanyDet = {};
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetCompanyDet",
			dataType: "json",
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

		$scope.BuildingTypeList = [];
		$http({
			method: 'GET',
			url: base_url + "Infrastructure/Creation/GetAllBuildingType",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BuildingTypeList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.currentPages = {
			Hostel: 1,
			Building: 1,
			Floor: 1,
			Room: 1,
		};

		$scope.searchData = {
			Hostel: '',
			Building: '',
			Floor: '',
			Room: '',
		};

		$scope.perPage = {
			Hostel: GlobalServices.getPerPageRow(),
			Building: GlobalServices.getPerPageRow(),
			Floor: GlobalServices.getPerPageRow(),
			Room: GlobalServices.getPerPageRow(),
		};

		$scope.newHostel = {
			HostelId: null,
			Name: '',
			Code: '',
			Address: '',
			WardenId: null,
			FacilitiesDetailsColl: [],
			EmployeeSearchBy: 'E.Name',
			Mode: 'Save'
		};
		$scope.newHostel.FacilitiesDetailsColl.push({});

		$scope.newBuilding = {
			BuildingId: null,
			Name: '',
			Location: '',
			Photo: null,
			PhotoPath: null,
			Mode: 'Save'
		};

		$scope.newFloor = {
			FloorId: null,
			Name: '',
			Mode: 'Save'
		};

		$scope.newRoom = {
			RoomId: null,
			HostelId: null,
			BuildingId: null,
			FloorId: null,
			RoomName: '',
			RoomFees: '',
			NoOfBeds: '',
			Photo: null,
			PhotoPath: null,
			RoomAssetColl: [],
			RoomBedColl: [],
			UpdateInMapping:false,
			Mode: 'Save'
		};
		$scope.newRoom.RoomAssetColl.push({});

		$scope.GetAllHostelList();
		$scope.GetAllBuildingList();
		$scope.GetAllFloorList();
		$scope.GetAllRoomList();
	}

	function OnClickDefault() {
		document.getElementById('add-hostel-form').style.display = "none";
		document.getElementById('building-form').style.display = "none";
		document.getElementById('floor-form').style.display = "none";
		document.getElementById('room-form').style.display = "none";


		document.getElementById('add-hostel').onclick = function () {
			document.getElementById('hostel-list').style.display = "none";
			document.getElementById('add-hostel-form').style.display = "block";
			$scope.ClearHostel();
		}
		document.getElementById('back-hostel-btn').onclick = function () {
			document.getElementById('add-hostel-form').style.display = "none";
			document.getElementById('hostel-list').style.display = "block";
			$scope.ClearHostel();
		}


		document.getElementById('add-building').onclick = function () {
			document.getElementById('building-list').style.display = "none";
			document.getElementById('building-form').style.display = "block";
			$scope.ClearBuilding();
		}
		document.getElementById('back-btn-building').onclick = function () {
			document.getElementById('building-form').style.display = "none";
			document.getElementById('building-list').style.display = "block";
			$scope.ClearBuilding();
		}


		document.getElementById('add-floor').onclick = function () {
			document.getElementById('floor-list').style.display = "none";
			document.getElementById('floor-form').style.display = "block";
			$scope.ClearFloor();
		}
		document.getElementById('back-btn-floor').onclick = function () {
			document.getElementById('floor-form').style.display = "none";
			document.getElementById('floor-list').style.display = "block";
			$scope.ClearFloor();
		}


		document.getElementById('add-room').onclick = function () {
			document.getElementById('room-list').style.display = "none";
			document.getElementById('room-form').style.display = "block";
			$scope.ClearRoom();
		}
		document.getElementById('back-btn-room').onclick = function () {
			document.getElementById('room-form').style.display = "none";
			document.getElementById('room-list').style.display = "block";
			$scope.ClearRoom();
		}
	};

	$scope.ClearHostel = function () {
		$scope.newHostel = {
			HostelId: null,
			Name: '',
			Code: '',
			Address: '',
			WardenId: null,
			FacilitiesDetailsColl: [],
			EmployeeSearchBy: 'E.Name',
			Mode: 'Save'
		};
		$scope.newHostel.FacilitiesDetailsColl.push({});
	}

	$scope.ClearBuilding = function () {
		$scope.newBuilding = {
            BuildingId: null,
            BuildingNo: '',
            Name: '',
            Location: '',
            BuildingTypeId: null,
            OtherBuildingType: '',
            NoOfFloor: null,
            OverallCondition: '',
            NoOfClassRooms: null,
            NoOfOtherRooms: null,
            ConstructionDate_TMP: '',
            StructureType: '',
            OtherStructureType: '',
            RoofType: '',
            OtherRoofType: '',
            DamageGrade: '',
            InfrastructureType: '',
            FundingSources: '',
            InterventionType: '',
            IsApprovedDesign: 0,
            IsCompletionCertificate: 0,
            CompletionStatus: '',
            CompletionDate_TMP: '',
            Remarks: '',
            Budget: 0,
            BoysToiletNo: null,
            GirlsToiletNo: null,
            IsToiletFunctional: 0,
            FacilityNotFunctioning: '',
            BuildingFacilitiesColl: [],
            Mode: 'Save'
        };
		$scope.newBuilding.BuildingFacilitiesColl.push({});

		$scope.ClearBuildingPhoto();
	}

	$scope.ClearFloor = function () {
		$scope.newFloor = {
			FloorId: null,
			Name: '',
			Mode: 'Save'
		};
	}

	$scope.ClearRoom = function () {
		$scope.newRoom = {
			RoomId: null,
			HostelId: null,
			BuildingId: null,
			FloorId: null,
			RoomName: '',
			RoomFees: '',
			NoOfBeds: '',
			Photo: null,
			PhotoPath: null,
			RoomAssetColl: [],
			RoomBedColl: [],
			UpdateInMapping:false,
			Mode: 'Save'
		};
		$scope.newRoom.RoomAssetColl.push({});

		$scope.ClearRoomPhotoRoom();
	}


	$scope.ClearBuildingPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newBuilding.PhotoData = null;
				$scope.newBuilding.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');
	};


	$scope.ClearRoomPhotoRoom = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newRoom.PhotoDataRoom = null;
				$scope.newRoom.PhotoRoom_TMP = [];
			});

		});
		$('#imgRoom').attr('src', '');
		$('#imgRoom1').attr('src', '');
	};
	//*************************Hostel*********************************	

	$scope.IsValidHostel = function () {
		if ($scope.newHostel.Name.isEmpty()) {
			Swal.fire('Please ! Enter Hostel Name');
			return false;
		}
		return true;
	}

	
	$scope.AddFacilitiesDetails = function (ind) {
		if ($scope.newHostel.FacilitiesDetailsColl) {
			if ($scope.newHostel.FacilitiesDetailsColl.length > ind + 1) {
				$scope.newHostel.FacilitiesDetailsColl.splice(ind + 1, 0, {
					Facilities: ''
				})
			} else {
				$scope.newHostel.FacilitiesDetailsColl.push({
					Facilities: ''
				})
			}
		}
	};
	$scope.delFacilitiesDetails = function (ind) {
		if ($scope.newHostel.FacilitiesDetailsColl) {
			if ($scope.newHostel.FacilitiesDetailsColl.length > 1) {
				$scope.newHostel.FacilitiesDetailsColl.splice(ind, 1);
			}
		}
	};

	$scope.SaveUpdateHostel = function () {
		if ($scope.IsValidHostel() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newHostel.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateHostel();
					}
				});
			} else
				$scope.CallSaveUpdateHostel();
		}
	};

	$scope.CallSaveUpdateHostel = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newHostel.FacilitiesColl = [];
		angular.forEach($scope.newHostel.FacilitiesDetailsColl, function (f) {

			if (f.Name && f.Name.length > 0)
				$scope.newHostel.FacilitiesColl.push(f.Name);
		});

		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveHostel",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newHostel }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearHostel();
				$scope.GetAllHostelList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllHostelList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HostelList = [];

		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllHostelList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HostelList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetHostelById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			HostelId: refData.HostelId
		};

		$scope.newHostel = null;
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetHostelById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				
				$timeout(function () {
					$scope.$apply(function ()
					{
						$scope.newHostel = res.data.Data;
						$scope.newHostel.Mode = 'Modify';
						$scope.newHostel.WardenId = res.data.Data.WardenId;

						$scope.newHostel.FacilitiesDetailsColl = [];
						angular.forEach($scope.newHostel.FacilitiesColl, function (f) {
							$scope.newHostel.FacilitiesDetailsColl.push({
								Name: f
							});
						});

						if ($scope.newHostel.FacilitiesDetailsColl.length == 0)
							$scope.AddFacilitiesDetails(0);
					});
				});

				

				document.getElementById('hostel-list').style.display = "none";
				document.getElementById('add-hostel-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelHostelById = function (refData) {
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
					HostelId: refData.HostelId
				};

				$http({
					method: 'POST',
					url: base_url + "Hostel/Creation/DelHostel",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllHostelList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};
	//************************* Building *********************************

	$scope.IsValidBuilding = function () {
		if ($scope.newBuilding.Name.isEmpty()) {
			Swal.fire('Please ! Enter Building Name');
			return false;
		}
		return true;
	}


	$scope.SaveUpdateBuilding = function () {
		if ($scope.IsValidBuilding() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBuilding.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBuilding();
					}
				});
			} else
				$scope.CallSaveUpdateBuilding();

		}
	};
	$scope.CallSaveUpdateBuilding = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var photo = $scope.newBuilding.Photo_TMP;

		if ($scope.newBuilding.ConstructedDateDet)
			$scope.newBuilding.ConstructionDate = $filter('date')(new Date($scope.newBuilding.ConstructedDateDet.dateAD), 'yyyy-MM-dd');

		if ($scope.newBuilding.IsCompletionCertificate == true) {
			if ($scope.newBuilding.CompletionDateDet) {
				$scope.newBuilding.CompletionDate = $filter('date')(new Date($scope.newBuilding.CompletionDateDet.dateAD), 'yyyy-MM-dd');
			}
			$scope.newBuilding.CompletionStatus = '';
		} else {
			$scope.newBuilding.CompletionDate = '';
		}

		if ($scope.newBuilding.IsCompletionCertificate == true)
			$scope.newBuilding.FacilityNotFunctioning = '';


		if ($scope.newBuilding.BuildingTypeColl)
			$scope.newBuilding.OtherBuildingType = $scope.newBuilding.BuildingTypeColl.toString();
		else
			$scope.newBuilding.OtherBuildingType = '';


		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveBuilding",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.stPhoto && data.stPhoto.length > 0)
					formData.append("photo", data.stPhoto[0]);

				return formData;
			},
			data: { jsonData: $scope.newBuilding, stPhoto: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearBuilding();
				$scope.GetAllBuildingList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllBuildingDetailsList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BuildingList = [];

		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllBuildingList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				$scope.BuildingList = res.data.Data;

				// Ensure BuildingTypeList is available and process OtherBuildingType
				if ($scope.BuildingTypeList.length > 0) {
					$scope.BuildingList.forEach(building => {
						if (building.OtherBuildingType) {
							let selectedIds = building.OtherBuildingType
								.split(',')
								.map(id => parseInt(id.trim(), 10)) // Convert to numbers
								.filter(id => !isNaN(id)); // Remove invalid numbers

							// Map the BuildingTypeIds to their respective BuildingType names
							building.BuildingTypeColl = selectedIds
								.map(id => {
									const buildingType = $scope.BuildingTypeList.find(bt => bt.BuildingTypeId === id);
									return buildingType ? buildingType.Name : ''; // Use Name if found
								})
								.filter(name => name); // Remove empty values
						} else {
							building.BuildingTypeColl = [];
						}
					});

					// Trigger select2 update
					setTimeout(() => {
						$('.select2').trigger('change');
					}, 100);
				}
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire('Failed: ' + reason);
		});
	};

	$scope.GetAllBuildingList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BuildingList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllBuildingList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BuildingList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetBuildingById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			BuildingId: refData.BuildingId
		};
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetBuildingById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBuilding = res.data.Data;
				$scope.newBuilding.Mode = 'Modify';

				document.getElementById('building-list').style.display = "none";
				document.getElementById('building-form').style.display = "block";


				if ($scope.newBuilding.IsApprovedDesign == true)
					$scope.newBuilding.IsApprovedDesign = 1;
				else
					$scope.newBuilding.IsApprovedDesign = 0;

				if ($scope.newBuilding.IsCompletionCertificate == true)
					$scope.newBuilding.IsCompletionCertificate = 1;
				else
					$scope.newBuilding.IsCompletionCertificate = 0;

				if ($scope.newBuilding.IsToiletFunctional == true)
					$scope.newBuilding.IsToiletFunctional = 1;
				else
					$scope.newBuilding.IsToiletFunctional = 0;


				if ($scope.newBuilding.CompletionDate)
					$scope.newBuilding.CompletionDate_TMP = new Date($scope.newBuilding.CompletionDate);

				if ($scope.newBuilding.ConstructionDate)
					$scope.newBuilding.ConstructionDate_TMP = new Date($scope.newBuilding.ConstructionDate);


				if (!$scope.newBuilding.BuildingFacilitiesColl || $scope.newBuilding.BuildingFacilitiesColl.length == 0) {
					$scope.newBuilding.BuildingFacilitiesColl = [];
					$scope.newBuilding.BuildingFacilitiesColl.push({});

				}

				if ($scope.newBuilding.OtherBuildingType) {
					$scope.newBuilding.BuildingTypeColl = $scope.newBuilding.OtherBuildingType.split(',').map(Number);

					setTimeout(function () {
						$('.select2').trigger('change');
					}, 100);
				}

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBuildingById = function (refData) {
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
					BuildingId: refData.BuildingId
				};
				$http({
					method: 'POST',
					url: base_url + "Hostel/Creation/DelBuilding",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBuildingList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

	//*************************Floor *********************************
	$scope.IsValidFloor = function () {
		if ($scope.newFloor.Name.isEmpty()) {
			Swal.fire('Please ! Enter Floor Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateFloor = function () {
		if ($scope.IsValidFloor() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newFloor.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateFloor();
					}
				});
			} else
				$scope.CallSaveUpdateFloor();
		}
	};

	$scope.CallSaveUpdateFloor = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveFloor",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newFloor }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearFloor();
				$scope.GetAllFloorList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllFloorList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FloorList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllFloorList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FloorList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetFloorById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			FloorId: refData.FloorId
		};
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetFloorById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFloor = res.data.Data;
				$scope.newFloor.Mode = 'Modify';
				document.getElementById('floor-list').style.display = "none";
				document.getElementById('floor-form').style.display = "block";
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelFloorById = function (refData) {
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
					FloorId: refData.FloorId
				};
				$http({
					method: 'POST',
					url: base_url + "Hostel/Creation/DelFloor",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFloorList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};


	//*************************Room *********************************
	$scope.IsValidRoom = function () {
		if ($scope.newRoom.RoomName.isEmpty()) {
			Swal.fire('Please ! Enter Room Name');
			return false;
		}
		return true;
	}


	$scope.AddAssetsDetails = function (ind) {
		if ($scope.newRoom.RoomAssetColl) {
			if ($scope.newRoom.RoomAssetColl.length > ind + 1) {
				$scope.newRoom.RoomAssetColl.splice(ind + 1, 0, {
					Particulars: '',
					Quantities:0,
				})
			} else {
				$scope.newRoom.RoomAssetColl.push({
					Particulars: '',
					Quantities: 0,
				})
			}
		}
	};
	$scope.delAssetsDetails = function (ind) {
		if ($scope.newRoom.RoomAssetColl) {
			if ($scope.newRoom.RoomAssetColl.length > 1) {
				$scope.newRoom.RoomAssetColl.splice(ind, 1);
			}
		}
	};

	$scope.ChangeBedNo = function () {
		$scope.newRoom.RoomBedColl = [];
		for (var i = 0; i < $scope.newRoom.NoOfBeds; i++) {
			$scope.newRoom.RoomBedColl.push({
				BedNo: i + 1,
				BedName:''
			});
        }
	};

	$scope.SaveUpdateRoom = function () {
		if ($scope.IsValidRoom() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newRoom.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateRoom();
					}
				});
			} else
				$scope.CallSaveUpdateRoom();
		}
	};

	$scope.CallSaveUpdateRoom = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var photo = $scope.newRoom.PhotoRoom_TMP;
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveRoom",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.stPhoto && data.stPhoto.length > 0)
					formData.append("photo", data.stPhoto[0]);


				return formData;
			},
			data: { jsonData: $scope.newRoom, stPhoto: photo }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearRoom();
				$scope.GetAllRoomList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllRoomList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.RoomList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllRoomList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.RoomList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetRoomById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			RoomId: refData.RoomId
		};
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetRoomById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newRoom = res.data.Data;

				if (!$scope.newRoom.RoomAssetColl || $scope.newRoom.RoomAssetColl.length == 0) {
					$scope.newRoom.RoomAssetColl = [];
					$scope.newRoom.RoomAssetColl.push({
						Particulars: '',
						Quantities: 0,
					});
                }
				$scope.newRoom.Mode = 'Modify';
				document.getElementById('room-list').style.display = "none";
				document.getElementById('room-form').style.display = "block";
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelRoomById = function (refData) {
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
					RoomId: refData.RoomId
				};
				$http({
					method: 'POST',
					url: base_url + "Hostel/Creation/DelRoom",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllRoomList();
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