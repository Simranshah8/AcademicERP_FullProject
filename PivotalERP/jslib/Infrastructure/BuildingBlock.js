app.controller('BuildingBlockContrioller', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Building Block';

	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.OverallConditionCOll = [
			{ id: 1, text: 'Very Good/No Damage (अति उत्तम)' },
			{ id: 2, text: 'Good/No Damage (उत्तम)' },
			{ id: 3, text: 'Normal Rash/Damage Grade 1 (सामान्य मर्मत गर्नु पर्ने)' },
			{ id: 4, text: 'Cracked/ Damage Grade 2 & 3 (चिरा परेको रेट्रोफिट गर्नु पर्ने)' },
			{ id: 5, text: 'Very Dilapidated/Damage Grade 4 & 5 (एकदमै जीर्ण-भत्काउनु पर्ने)' },
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
			{ id: 4, text: 'Tile/Jhingati (झिंगटी)' },
			{ id: 5, text: 'Slate Stone' },
			{ id: 6, text: 'Khar/Bush (खर/फुस)' },
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
			{ id: 1, text: 'साविकको जि.शि.का को अनुदान' },
			{ id: 2, text: 'शिक्षा विभाग/शिक्षा तथा मानव स्रोत विकास केन्द्र/जिल्ला शिक्षा कार्यालय' },
			{ id: 3, text: 'राष्ट्रपति शैक्षिक सुधार कार्यक्रम' },
			{ id: 4, text: 'दातृ निकायको सहयोगबाट' },
			{ id: 5, text: 'चन्दादाताको सहयोगबाट' },
			{ id: 6, text: 'ठूला विद्यालय' },
			{ id: 7, text: 'विशेष विद्यालय' },
			{ id: 8, text: 'ADB (एशियाली विकास बैंक)' },
			{ id: 9, text: 'तत्कालिन शान्ति मन्त्रालय संघीय मन्त्रालयको अनुदान' },
			{ id: 10, text: 'विद्यालयको आफ्नै स्रोत' },
			{ id: 11, text: 'CLPIU/DLPIU को अनुदानबाट (दातृनिकायको सहयोग सहित)' },
			{ id: 12, text: 'प्रदेश मन्त्रालयको अनुदानबाट' },
			{ id: 13, text: 'स्थानीय तहको आन्तरिक बजेटबाट' },
			{ id: 14, text: 'सांसद विकास कोष (संघीय)' },
			{ id: 15, text: 'सांसद विकास कोष (प्रदेश)' },
			{ id: 16, text: 'भारत सरकारको सहयोगबाट' },
			{ id: 17, text: 'नमूना विद्यालय विकास कार्यक्रम' },
			{ id: 18, text: 'Others/अन्य स्रोतबाट' },
			{ id: 19, text: 'तत्कालिन गाविस/नपा/जिविसको अनुदान' },
			{ id: 20, text: 'GICA' },
			{ id: 21, text: 'GON' },
			{ id: 22, text: 'Others' },
		];


		$scope.InterventionTypeColl = [
			{ id: 1, text: 'New Construction' },
			{ id: 2, text: 'Retrofitting' },
			{ id: 3, text: 'Rehabilitation ' },
			{ id: 4, text: 'Reconstruction' },
			{ id: 5, text: 'Retro' },
			{ id: 6, text: 'Existing' }
		];

		//Company Details
		$scope.CompanyConfig = {};
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetCompanyDet",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CompanyConfig = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);

		});
		$scope.CompletionStatusColl = [
			{ id: 0, text: 'select Status' },
		];

		$scope.currentPages = {
			BuildingBlock: 1

		};

		$scope.searchData = {
			BuildingBlock: ''

		};

		$scope.perPage = {
			BuildingBlock: GlobalServices.getPerPageRow(),

		};

		$scope.newDet = {
			BuildingBlockId: null,
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
			FacilityNotFunctioning:''
		};

		/*	$scope.GetAllBuildingBlockList();*/

	}

	function OnClickDefault() {

		document.getElementById('building-form').style.display = "none";

		//PTM section
		document.getElementById('add-Bulding').onclick = function () {
			document.getElementById('Buildingtbl').style.display = "none";
			document.getElementById('building-form').style.display = "block";
			$scope.ClearPTM();
		}

		document.getElementById('back-list-btn').onclick = function () {
			document.getElementById('building-form').style.display = "none";
			document.getElementById('Buildingtbl').style.display = "block";
			$scope.ClearPTM();
		}

	}


	$scope.ClearBuildingBlock = function () {
		$scope.newBuildingBlock = {
			BuildingBlockId: null,
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

	$scope.updateDate = function () {
		if ($scope.newDet.ConstructedDateDet) {
			var englishDate = $filter('date')(new Date($scope.newDet.ConstructedDateDet.dateAD), 'yyyy-MM-dd');

			$scope.newDet.ConstructionDate = englishDate;

			if (!$scope.$$phase) {
				$scope.$apply();
			}
		}
	};
	$scope.updateDate_BS = function () {
		if ($scope.newDet.ConstructionDate) {
			$scope.$applyAsync(function () {
				$scope.newDet.ConstructionDate_TMP = new Date($scope.newDet.ConstructionDate);
			});
		}
	};

	//************************* BuildingBlock *********************************

	$scope.IsValidBuildingBlock = function () {
		if ($scope.newBuildingBlock.Name.isEmpty()) {
			Swal.fire('Please ! Enter BuildingBlock Name');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateBuildingBlock = function () {
		if ($scope.IsValidBuildingBlock() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBuildingBlock.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBuildingBlock();
					}
				});
			} else
				$scope.CallSaveUpdateBuildingBlock();

		}
	};

	$scope.CallSaveUpdateBuildingBlock = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveBuildingBlock",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newBuildingBlock }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBuildingBlock();
				$scope.GetAllBuildingBlockList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllBuildingBlockList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BuildingBlockList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllBuildingBlockList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BuildingBlockList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetBuildingBlockById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BuildingBlockId: refData.BuildingBlockId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetBuildingBlockById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBuildingBlock = res.data.Data;
				$scope.newBuildingBlock.Mode = 'Modify';

				document.getElementById('add-BuildingBlock-section').style.display = "none";
				document.getElementById('add-BuildingBlock-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBuildingBlockById = function (refData) {

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
					BuildingBlockId: refData.BuildingBlockId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelBuildingBlock",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBuildingBlockList();
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