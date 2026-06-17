app.controller('CanteenMasterController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Canteen Master';
	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();		

		$scope.currentPages = {
			CreateCanteen: 1,
			FoodCategory: 1,
			CreateFood: 1,
			TimeSlotMaster: 1
		};

		$scope.searchData = {
			CreateCanteen: '',
			FoodCategory: '',
			CreateFood: '',
			TimeSlotMaster:''
		};

		$scope.perPage = {
			CreateCanteen: GlobalServices.getPerPageRow(),
			FoodCategory: GlobalServices.getPerPageRow(),
			CreateFood: GlobalServices.getPerPageRow(),
			TimeSlotMaster: GlobalServices.getPerPageRow(),
		};

		$scope.newCreateCanteen = {
			CreateCanteenId: null,
			Name: '',
			Description: '',
			Mode: 'Save'
		};

		$scope.newFoodCategory = {
			FoodCategoryId: null,
			Name: '',
			Description: '',
			Mode: 'Save'
		};

		$scope.newCreateFood = {
			CreateFoodId: null,
			FoodCategoryId: null,
			FoodName: '',
			Price: '',
			Volume: '',
			Photo: null,
			PhotoPath: null,
			Description: '',
			Mode: 'Save'
		};

		$scope.newTimeSlotMaster = {
			TimeSlotMasterId: null,
			TimeSlotName: '',
			Description: '',
			Mode: 'Save'
		};

		//$scope.GetAllCreateCanteenList();
		//$scope.GetAllFoodCategoryList();
		//$scope.GetAllCreateFoodList();
		//$scope.GetAllCreateFoodList();
	}

	function OnClickDefault() {
		document.getElementById('create-canteen-form').style.display = "none";
		document.getElementById('food-category-form').style.display = "none";
		document.getElementById('create-food-form').style.display = "none";
		document.getElementById('time-slot-form').style.display = "none";

		document.getElementById('add-canteen').onclick = function () {
		document.getElementById('canteen-name-section').style.display = "none";
		document.getElementById('create-canteen-form').style.display = "block";
		}

		document.getElementById('back-canteen-list').onclick = function () {
		document.getElementById('canteen-name-section').style.display = "block";
		document.getElementById('create-canteen-form').style.display = "none";
		}


		document.getElementById('add-food-category').onclick = function () {
		document.getElementById('food-category-section').style.display = "none";
		document.getElementById('food-category-form').style.display = "block";
		}

		document.getElementById('back-food-category-list').onclick = function () {
		document.getElementById('food-category-section').style.display = "block";
		document.getElementById('food-category-form').style.display = "none";
		}


		document.getElementById('add-food').onclick = function () {
		document.getElementById('food-section').style.display = "none";
		document.getElementById('create-food-form').style.display = "block";
		}

		document.getElementById('back-food-list').onclick = function () {
		document.getElementById('food-section').style.display = "block";
		document.getElementById('create-food-form').style.display = "none";
		}


		document.getElementById('add-time-slot').onclick = function () {
		document.getElementById('time-slot-section').style.display = "none";
		document.getElementById('time-slot-form').style.display = "block";
		}

		document.getElementById('back-time-slot-list').onclick = function () {
		document.getElementById('time-slot-section').style.display = "block";
		document.getElementById('time-slot-form').style.display = "none";
		}
	};

	$scope.ClearCreateCanteen = function () {
		$scope.newCreateCanteen = {
			CreateCanteenId: null,
			Name: '',
			Description: '',
			Mode: 'Save'
		};
	}

	$scope.ClearFoodCategory = function () {
		$scope.newFoodCategory = {
			FoodCategoryId: null,
			Name: '',
			Description: '',
			Mode: 'Save'
		};
	}


	$scope.ClearCreateFoodPhoto = function () {
		$timeout(function () {
			$scope.$apply(function () {
				$scope.newCreateFood.PhotoData = null;
				$scope.newCreateFood.Photo_TMP = [];
			});

		});

		$('#imgPhoto').attr('src', '');
		$('#imgPhoto1').attr('src', '');

	};

	$scope.ClearCreateFood= function () {
		$scope.newCreateFood = {
			CreateFoodId: null,
			FoodCategoryId: null,
			FoodName: '',
			Price: '',
			Volume: '',
			Image: '',
			Description: '',
			Mode: 'Save'
		};
	}

	$scope.ClearTimeSlotMaster = function () {
		$scope.newTimeSlotMaster = {
			TimeSlotMasterId: null,
			TimeSlotName: '',
			Description: '',			
			Mode: 'Save'
		};
	}

	//*************************CreateCanteen *********************************

	$scope.IsValidCreateCanteen = function () {
	if ($scope.newCreateCanteen.Name.isEmpty()) {
		Swal.fire('Please ! Enter Name');
		return false;
		}
	return true;
	}

	$scope.SaveUpdateCreateCanteen = function () {
		if ($scope.IsValidCreateCanteen() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCreateCanteen.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCreateCanteen();
					}
				});
			} else
				$scope.CallSaveUpdateCreateCanteen();
		}
	};

	$scope.CallSaveUpdateCreateCanteen = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveSetup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSetup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearSetup();
				$scope.GetAllCreateCanteenList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllCreateCanteenList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CreateCanteenList = [];

		$http({
			method: 'POST',
			url: base_url + "CanteenManagement/Creation/GetAllCreateCanteenList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CreateCanteenList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetCreateCanteenById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			CreateCanteenId: refData.CreateCanteenId
		};
		$http({
			method: 'POST',
			url: base_url + "CanteenManagement/Creation/GetSetupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCreateCanteen = res.data.Data;
				$scope.newCreateCanteen.Mode = 'Modify';

				document.getElementById('canteen-name-section').style.display = "none";
				document.getElementById('create-canteen-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCreateCanteenById = function (refData) {
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
					CreateCanteenId: refData.CreateCanteenId
				};

				$http({
					method: 'POST',
					url: base_url + "CanteenManagement/Creation/DelCreateCanteen",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCreateCanteenList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//*************************FoodCategory *********************************

	$scope.IsValidFoodCategory = function () {
		if ($scope.newFoodCategory.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}		
		return true;
	}

	
	$scope.SaveUpdateFoodCategory = function () {
		if ($scope.IsValidFoodCategory() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newFoodCategory.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateFoodCategory();
					}
				});
			} else
				$scope.CallSaveUpdateFoodCategory();
		}
	};

	$scope.CallSaveUpdateFoodCategory = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "CanteenManagement/Creation/SaveFoodCategory",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newFoodCategory }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearFoodCategory();
				$scope.GetAllFoodCategoryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllFoodCategoryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FoodCategoryList = [];

		$http({
			method: 'POST',
			url: base_url + "CanteenManagement/Creation/GetAllFoodCategoryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FoodCategoryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetFoodCategoryById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			FoodCategoryId: refData.FoodCategoryId
		};

		$http({
			method: 'POST',
			url: base_url + "CanteenManagement/Creation/GetFoodCategoryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFoodCategory = res.data.Data;
				$scope.newFoodCategory.Mode = 'Modify';

				document.getElementById('food-category-section').style.display = "none";
				document.getElementById('food-category-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelFoodCategoryById = function (refData) {
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
					FoodCategoryId: refData.FoodCategoryId
				};

				$http({
					method: 'POST',
					url: base_url + "CanteenManagement/Creation/DelFoodCategory",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFoodCategoryList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};




	//*************************CreateFood *********************************

	$scope.IsValidCreateFood = function () {
		if ($scope.newCreateFood.FoodName.isEmpty()) {
			Swal.fire('Please ! Enter Food Name');
			return false;
		}		

		return true;
	}

	
	


	$scope.SaveUpdateCreateFood = function () {
		if ($scope.IsValidCreateFood() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCreateFood.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCreateFood();
					}
				});
			} else
				$scope.CallSaveUpdateCreateFood();

		}
	};

	$scope.CallSaveUpdateCreateFood = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "CanteenManagement/Creation/SaveCreateFood",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newCreateFood }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCreateFood();
				$scope.GetAllCreateFoodList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllCreateFoodList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CreateFoodList = [];

		$http({
			method: 'POST',
			url: base_url + "CanteenManagement/Creation/GetAllCreateFoodList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CreateFoodList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetCreateFoodById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			CreateFoodId: refData.CreateFoodId
		};

		$http({
			method: 'POST',
			url: base_url + "CanteenManagement/Creation/GetCreateFoodById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCreateFood = res.data.Data;
				$scope.newCreateFood.Mode = 'Modify';

				document.getElementById('food-section').style.display = "none";
				document.getElementById('create-food-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCreateFoodById = function (refData) {

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
					CreateFoodId: refData.CreateFoodId
				};

				$http({
					method: 'POST',
					url: base_url + "CanteenManagement/Creation/DelCreateFood",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCreateFoodList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	//*************************TimeSlotMaster *********************************

	$scope.IsValidTimeSlotMaster = function () {
		if ($scope.newTimeSlotMaster.TimeSlotName.isEmpty()) {
			Swal.fire('Please ! Enter Time Slot Name');
			return false;
		}
		return true;
	}
	

	$scope.SaveUpdateTimeSlotMaster = function () {
		if ($scope.IsValidTimeSlotMaster() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newTimeSlotMaster.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateTimeSlotMaster();
					}
				});
			} else
				$scope.CallSaveUpdateTimeSlotMaster();

		}
	};

	$scope.CallSaveUpdateTimeSlotMaster = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "CanteenManagement/Creation/SaveTimeSlotMaster",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newTimeSlotMaster }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearTimeSlotMaster();
				$scope.GetAllTimeSlotMasterList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllTimeSlotMasterList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TimeSlotMasterList = [];

		$http({
			method: 'POST',
			url: base_url + "CanteenManagement/Creation/GetAllTimeSlotMasterList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TimeSlotMasterList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetTimeSlotMasterById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TimeSlotMasterId: refData.TimeSlotMasterId
		};
		$http({
			method: 'POST',
			url: base_url + "CanteenManagement/Creation/GetTimeSlotMasterById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newTimeSlotMaster = res.data.Data;
				$scope.newTimeSlotMaster.Mode = 'Modify';

				document.getElementById('time-slot-section').style.display = "none";
				document.getElementById('time-slot-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelTimeSlotMasterById = function (refData) {

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
					TimeSlotMasterId: refData.TimeSlotMasterId
				};

				$http({
					method: 'POST',
					url: base_url + "CanteenManagement/Creation/DelTimeSlotMaster",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllTimeSlotMasterList();
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