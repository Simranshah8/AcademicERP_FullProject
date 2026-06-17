

app.controller('AllowEntityController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Allow Shift';

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.searchData = {
			Branch: '',
			AllowEntity: '',
			Module: '',
			AcademicYear: '',
			Class: '',
			Section:'',
			Shift: '',
			Medium:''
		};
		  
		$scope.UserList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUserList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserList = res.data.Data;

			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.UserGroupList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUserGroupList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserGroupList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.beDataBranch = {
			ForUser: 1,
			ForUserId: null,
			ForGroupId: null,
			CheckedAll:false,
		};
		$scope.AllowBranchList = [];

		$scope.beDataModule = {
			ForUser: 1,
			ForUserId: null,
			ForGroupId: null,
			CheckedAll: false,
		};
		$scope.AllowModuleList = [];

		$scope.newAllowEntity = {
			ForUser: 1,
			ForUserId: null,
			ForGroupId: null,
		};


		$scope.beDataAcademicYear = {
			ForUser: 1,
			ForUserId: null,
			ForGroupId: null,
			CheckedAll: false,
		};
		$scope.AllowAcademicYearList = [];
		 
		$scope.beDataClass = {
			ForUser: 1,
			ForUserId: null,
			ForGroupId: null,
			CheckedAll: false,
		};
		$scope.AllowClassList = [];

		$scope.beDataSection = {
			ForUser: 1,
			ForUserId: null,
			ForGroupId: null,
			CheckedAll: false,
		};
		$scope.AllowSectionList = [];

		$scope.beDataShift = {
			ForUser: 1,
			ForUserId: null,
			ForGroupId: null,
			CheckedAll: false,
		};
		$scope.AllowShiftList = [];

		$scope.beDataMedium = {
			ForUser: 1,
			ForUserId: null,
			ForGroupId: null,
			CheckedAll: false,
		};
		$scope.AllowMediumList = [];
	}

	//************************* Allow Branch *********************************
	$scope.CheckUnCheckAllBranch = function () {
		var val = $scope.beDataBranch.CheckedAll;
		angular.forEach($scope.AllowBranchList, function (cl) {
			cl.IsAllow = val;
		});
	}
	$scope.GetAllowBranchList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowBranchList = [];
		var para = {
			ForUserId: ($scope.beDataBranch.ForUser == 1 ? $scope.beDataBranch.ForUserId : null),
			ForGroupId: ($scope.beDataBranch.ForUser == 2 ? $scope.beDataBranch.ForGroupId :null),
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllowBranchById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowBranchList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.CallSaveUpdateAllowBranch = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = $scope.AllowBranchList;
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveAllowBranch",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//************************* End Branch *********************************


	//************************* Allow Module *********************************

	$scope.CheckUnCheckAllModule = function () {
		var val = $scope.beDataModule.CheckedAll;
		angular.forEach($scope.AllowModuleList, function (cl) {
			cl.IsAllow = val;
		});
	}
	$scope.GetAllowModuleList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowModuleList = [];
		var para = {
			ForUserId: ($scope.beDataModule.ForUser == 1 ? $scope.beDataModule.ForUserId : null),
			ForGroupId: ($scope.beDataModule.ForUser == 2 ? $scope.beDataModule.ForGroupId : null),
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllowModuleById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowModuleList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.CallSaveUpdateAllowModule = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = $scope.AllowModuleList;
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveAllowModule",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//************************* ENd Module ****************************8

	//************************* Allow Academic Year *********************************

	$scope.CheckUnCheckAllAcademicYear = function () {
		var val = $scope.beDataAcademicYear.CheckedAll;
		angular.forEach($scope.AllowAcademicYearList, function (cl) {
			cl.IsAllow = val;
		});
	}
	$scope.GetAllowAcademicYearList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowAcademicYearList = [];
		var para = {
			ForUserId: ($scope.beDataAcademicYear.ForUser == 1 ? $scope.beDataAcademicYear.ForUserId : null),
			ForGroupId: ($scope.beDataAcademicYear.ForUser == 2 ? $scope.beDataAcademicYear.ForGroupId : null),
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllowAcademicYearById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowAcademicYearList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.CallSaveUpdateAllowAcademicYear = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = $scope.AllowAcademicYearList;
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveAllowAcademicYear",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//************************* ENd Academic ****************************




	//************************* Allow Class *********************************

	$scope.CheckUnCheckAllClass = function () {
		var val = $scope.beDataClass.CheckedAll;
		angular.forEach($scope.AllowClassList, function (cl) {
			cl.IsAllow = val;
		});
	}
	$scope.GetAllowClassList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowClassList = [];
		var para = {
			ForUserId: ($scope.beDataClass.ForUser == 1 ? $scope.beDataClass.ForUserId : null),
			ForGroupId: ($scope.beDataClass.ForUser == 2 ? $scope.beDataClass.ForGroupId : null),
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllowClassById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowClassList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.CallSaveUpdateAllowClass = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = $scope.AllowClassList;
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveAllowClass",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//************************* ENd Module ****************************8


	//************************* Allow Section *********************************

	$scope.CheckUnCheckAllSection= function () {
		var val = $scope.beDataSection.CheckedAll;
		angular.forEach($scope.AllowSectionList, function (cl) {
			cl.IsAllow = val;
		});
	}
	$scope.GetAllowSectionList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowSectionList = [];
		var para = {
			ForUserId: ($scope.beDataSection.ForUser == 1 ? $scope.beDataSection.ForUserId : null),
			ForGroupId: ($scope.beDataSection.ForUser == 2 ? $scope.beDataSection.ForGroupId : null),
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllowSectionById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowSectionList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.CallSaveUpdateAllowSection = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = $scope.AllowSectionList;
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveAllowSection",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//************************* ENd Module ****************************8

	//************************* Allow Class Shift *********************************

	$scope.CheckUnCheckAllShift = function () {
		var val = $scope.beDataShift.CheckedAll;
		angular.forEach($scope.AllowShiftList, function (cl) {
			cl.IsAllow = val;
		});
	}
	$scope.GetAllowShiftList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowShiftList = [];
		var para = {
			ForUserId: ($scope.beDataShift.ForUser == 1 ? $scope.beDataShift.ForUserId : null),
			ForGroupId: ($scope.beDataShift.ForUser == 2 ? $scope.beDataShift.ForGroupId : null),
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllowClassShiftById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowShiftList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.CallSaveUpdateAllowShift = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = $scope.AllowShiftList;
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveAllowClassShift",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//************************* ENd Academic ****************************


	//************************* Allow Medium *********************************

	$scope.CheckUnCheckAllMedium = function () {
		var val = $scope.beDataMedium.CheckedAll;
		angular.forEach($scope.AllowMediumList, function (cl) {
			cl.IsAllow = val;
		});
	}
	$scope.GetAllowMediumList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllowMediumList = [];
		var para = {
			ForUserId: ($scope.beDataMedium.ForUser == 1 ? $scope.beDataMedium.ForUserId : null),
			ForGroupId: ($scope.beDataMedium.ForUser == 2 ? $scope.beDataMedium.ForGroupId : null),
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllowMediumById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowMediumList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.CallSaveUpdateAllowMedium = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = $scope.AllowMediumList;
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveAllowMedium",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//************************* ENd Academic ****************************

	//************************* Allow Entity *********************************

	$scope.CheckAllFull = function () {
		var tmpData = $filter('filter')($scope.AllowEntityList, $scope.searchData.AllowEntity);

		angular.forEach(tmpData, function (ent) {
			ent.Full = $scope.newAllowEntity.Full;
		});
	}
	$scope.CheckAllView = function () {
		var tmpData = $filter('filter')($scope.AllowEntityList, $scope.searchData.AllowEntity);

		angular.forEach(tmpData, function (ent) {
			ent.View = $scope.newAllowEntity.View;
		});
	}
	$scope.CheckAllAdd = function () {
		var tmpData = $filter('filter')($scope.AllowEntityList, $scope.searchData.AllowEntity);

		angular.forEach(tmpData, function (ent) {
			ent.Add = $scope.newAllowEntity.Add;
		});
	}
	$scope.CheckAllModify = function () {
		var tmpData = $filter('filter')($scope.AllowEntityList, $scope.searchData.AllowEntity);

		angular.forEach(tmpData, function (ent) {
			ent.Modify = $scope.newAllowEntity.Modify;
		});
	}
	$scope.CheckAllDelete = function () {
		var tmpData = $filter('filter')($scope.AllowEntityList, $scope.searchData.AllowEntity);

		angular.forEach(tmpData, function (ent) {
			ent.Delete = $scope.newAllowEntity.Delete;
		});
	}
	$scope.CheckAllPrint = function () {
		var tmpData = $filter('filter')($scope.AllowEntityList, $scope.searchData.AllowEntity);

		angular.forEach(tmpData, function (ent) {
			ent.Print = $scope.newAllowEntity.Print;
		});
	}
	$scope.CheckAllExport = function () {
		var tmpData = $filter('filter')($scope.AllowEntityList, $scope.searchData.AllowEntity);

		angular.forEach(tmpData, function (ent) {
			ent.Export = $scope.newAllowEntity.Export;
		});
	}
	$scope.IsValidAllowEntity = function () {

		if (!$scope.AllowEntityList || $scope.AllowEntityList.length == 0) {
				Swal.fire('Not Data Found For Save');
				return false;
        }

		return true;
	}

	$scope.GetAllowEntityList = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			UserId: ($scope.newAllowEntity.ForUser==2 ? null : $scope.newAllowEntity.ForUserId ),
			GroupId: ($scope.newAllowEntity.ForUser == 1 ? null :$scope.newAllowEntity.ForGroupId )
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetEntityAccessById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AllowEntityList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.SaveUpdateAllowEntity = function () {
		if ($scope.IsValidAllowEntity() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAllowEntity.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAllowEntity();
					}
				});
			} else
				$scope.CallSaveUpdateAllowEntity();

		}
	};

	$scope.CallSaveUpdateAllowEntity = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveEntityAccess",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.AllowEntityList }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

	}

	//************************* End Allow Entity *********************************

});