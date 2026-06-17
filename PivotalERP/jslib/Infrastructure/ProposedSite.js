app.controller('ProposedSiteContrioller', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Proposed Site';

	//OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.RetainWallTypeColl = [
			{ id: 1, text: 'Stone Masonry' },
			{ id: 2, text: 'Brick Masonry' },
			{ id: 3, text: 'Concrete' },
			{ id: 4, text: 'Others' }
		];

		$scope.currentPages = {
			ProposedSite: 1

		};

		$scope.searchData = {
			ProposedSite: ''

		};

		$scope.perPage = {
			ProposedSite: GlobalServices.getPerPageRow(),

		};

		$scope.newDet = {
			ProposedSiteId: null,

			IsCut: false,
			SoilCut: '',
			IsFill: false,
			SoilFilled: '',
			IsRetaining: false,
			LengthRetainWall: '',
			RetainingWallId: null,
			IsAccessRoad: false,
			AccessRoadNo: null,
			AccessRoadLength: null,
			IsTree: false,
			ClearanceVolumeType: null,
			ClearanceVolume: null,
			IsBoundaryWall: false,
			BoundaryWall: '',
			BoundaryWallId: null,

			SiteRemarks: '',

		};

		/*	$scope.GetAllProposedSiteList();*/

	}

	//function OnClickDefault() {

	//	document.getElementById('building-form').style.display = "none";

	//	//PTM section
	//	document.getElementById('add-Bulding').onclick = function () {
	//		document.getElementById('Buildingtbl').style.display = "none";
	//		document.getElementById('building-form').style.display = "block";
	//		$scope.ClearPTM();
	//	}

	//	document.getElementById('back-list-btn').onclick = function () {
	//		document.getElementById('building-form').style.display = "none";
	//		document.getElementById('Buildingtbl').style.display = "block";
	//		$scope.ClearPTM();
	//	}

	//}

	$scope.ClearProposedSite = function () {
		$scope.newProposedSite = {
			ProposedSiteId: null,
			BlockNo: '',
			StoryNo: '',
			OverallConditionId: null,
			ClassroomNo: '',
			OtherRooms: '',
			ConstructedOn: false,
			StructureTypeId: null,
			OtherStructureType: '',
			RoofType: '',
			OtherRoofType: '',
			DamageGradeId: null,
			InfrastructureTypeId: null,
			FundingSourcesId: null,
			InterventionTypeId: null,
			ApprovedYes: '',
			ApprovedNo: '',
			CompletionCerti: 1,
			CompletionNo: '',
			CompletionStatus: '',
			Remarks: '',
			Budget: '',
			BoysToiletNo: '',
			GirlsToiletNo: '',
			ToiletFunctional: 1,
			FacilityNotFunctioning: ''
		};
	}


	$scope.AddRoomDetails = function (ind) {
		if ($scope.newRoom.RoomDetailsColl) {
			if ($scope.newRoom.RoomDetailsColl.length > ind + 1) {
				$scope.newRoom.RoomDetailsColl.splice(ind + 1, 0, {
					RoomName: ''
				})
			} else {
				$scope.newRoom.RoomDetailsColl.push({
					RoomName: ''
				})
			}
		}
	};
	$scope.delRoomDetails = function (ind) {
		if ($scope.newRoom.RoomDetailsColl) {
			if ($scope.newRoom.RoomDetailsColl.length > 1) {
				$scope.newRoom.RoomDetailsColl.splice(ind, 1);
			}
		}
	};

	//************************* ProposedSite *********************************

	$scope.IsValidProposedSite = function () {
		if ($scope.newProposedSite.Name.isEmpty()) {
			Swal.fire('Please ! Enter ProposedSite Name');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateProposedSite = function () {
		if ($scope.IsValidProposedSite() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newProposedSite.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateProposedSite();
					}
				});
			} else
				$scope.CallSaveUpdateProposedSite();

		}
	};

	$scope.CallSaveUpdateProposedSite = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveProposedSite",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newProposedSite }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearProposedSite();
				$scope.GetAllProposedSiteList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllProposedSiteList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ProposedSiteList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllProposedSiteList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ProposedSiteList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetProposedSiteById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ProposedSiteId: refData.ProposedSiteId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetProposedSiteById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newProposedSite = res.data.Data;
				$scope.newProposedSite.Mode = 'Modify';

				document.getElementById('add-ProposedSite-section').style.display = "none";
				document.getElementById('add-ProposedSite-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelProposedSiteById = function (refData) {

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
					ProposedSiteId: refData.ProposedSiteId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelProposedSite",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllProposedSiteList();
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