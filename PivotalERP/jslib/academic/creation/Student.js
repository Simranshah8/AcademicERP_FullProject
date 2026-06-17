app.controller('StudentCRController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Student';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			HouseName: 1,
			HouseDress: 1,
			StudentType: 1,
			Medium: 1
		};

		$scope.searchData = {
			HouseName: '',
			HouseDress: '',
			StudentType: '',
			Medium: ''
		};

		$scope.perPage = {
			HouseName: GlobalServices.getPerPageRow(),
			HouseDress: GlobalServices.getPerPageRow(),
			StudentType: GlobalServices.getPerPageRow(),
			Medium: GlobalServices.getPerPageRow()
		};

		$scope.newHouseName = {
			HouseNameId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			CoOrdinatorId: null,
			EmployeeSearchBy: 'E.Name',
			CaptainId_Boy: null,
			ViceCaptainId_Boy: null,
			CaptainId_Girl: null,
			ViceCaptainId_Girl: null,
			HouseInchargeIdColl: [],
			HouseMemberIdColl: [],
			Mode: 'Save'
		};

		$scope.newHouseDress = {
			HouseDressId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			HouseNameId: null,
			ColorCode:'',
			Mode: 'Save'
		};

		$scope.newStudentType = {
			StudentTypeId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newMedium = {
			BatchId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.GetAllHouseNameList();
		//$scope.GetAllHouseDressList();
		//$scope.GetAllStudentTypeList();
		//$scope.GetAllMediumList();

		$scope.TypeColl = [];
		$http({
			method: 'GET',
			url: base_url + "Global/GetStudentTypes",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.TypeColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AllStudentColl = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllStudentForTran",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.AllStudentColl = res.data.Data;

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AllEmployeeColl = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetAllEmpShortList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.AllEmployeeColl = res.data.Data;

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	function OnClickDefault() {

		document.getElementById('house-form').style.display = "none";
		document.getElementById('house-dress-form').style.display = "none";
		document.getElementById('student-type-form').style.display = "none";
		document.getElementById('medium-form').style.display = "none";




		document.getElementById('add-house').onclick = function () {
			document.getElementById('house-name-section').style.display = "none";
			document.getElementById('house-form').style.display = "block";
			$scope.ClearHouseName();

		}

		document.getElementById('back-house-list').onclick = function () {			
			document.getElementById('house-form').style.display = "none";
			document.getElementById('house-name-section').style.display = "block";		
			$scope.ClearHouseName();
		}


		document.getElementById('add-house-dress').onclick = function () {
			document.getElementById('house-dress-section').style.display = "none";
			document.getElementById('house-dress-form').style.display = "block";
			$scope.ClearHouseDress();
		}

		document.getElementById('back-housedress-list').onclick = function () {
			document.getElementById('house-dress-section').style.display = "block";
			document.getElementById('house-dress-form').style.display = "none";
			$scope.ClearHouseDress();
		}

		// student-type js


		document.getElementById('add-student').onclick = function () {
			document.getElementById('student-type-section').style.display = "none";
			document.getElementById('student-type-form').style.display = "block";
			$scope.ClearStudentType();
		}

		document.getElementById('back-student-list').onclick = function () {
			document.getElementById('student-type-section').style.display = "block";
			document.getElementById('student-type-form').style.display = "none";
			$scope.ClearStudentType();
		}

		// medium section js


		document.getElementById('add-medium').onclick = function () {
			document.getElementById('medium-section').style.display = "none";
			document.getElementById('medium-form').style.display = "block";
			$scope.ClearMedium();
		}

		document.getElementById('back-medium-list').onclick = function () {
			document.getElementById('medium-section').style.display = "block";
			document.getElementById('medium-form').style.display = "none";
			$scope.ClearMedium();
		}

	}

	$scope.ClearHouseName = function () {

		$('#cboCoOrdinator').select2('data', null);
		$timeout(function () {
			$scope.newHouseName = {
				HouseNameId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				CoOrdinatorId: null,
				EmployeeSearchBy: 'E.Name',
				CaptainId_Boy: null,
				ViceCaptainId_Boy: null,
				CaptainId_Girl: null,
				ViceCaptainId_Girl: null,
				HouseInchargeIdColl: [],
				HouseMemberIdColl:[],
				Mode: 'Save'
			};
		});

		var ethin = [];
		$timeout(function () {			
			$('#cboIncharge').val(ethin).trigger('change');
		});

		$timeout(function () {			
			$('#cboMember').val(ethin).trigger('change');
		});
		
	}
	$scope.ClearHouseDress = function () {

		$timeout(function () {
			$scope.newHouseDress = {
				HouseDressId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});
		
	}
	$scope.ClearStudentType = function () {

		$timeout(function () {
			$scope.newStudentType = {
				StudentTypeId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});
		
	}
	$scope.ClearMedium = function () {

		$timeout(function () {
			$scope.newMedium = {
				MediumId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});
		


		
	}

	$scope.IsValidHouseName = function () {
		if ($scope.newHouseName.Name.isEmpty()) {
			Swal.fire('Please ! Enter House Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateHouseName = function () {
		if ($scope.IsValidHouseName() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newHouseName.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateHouseName();
					}
				});
			} else
				$scope.CallSaveUpdateHouseName();

		}
	};

	$scope.CallSaveUpdateHouseName = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newHouseName.HouseInchargeIdColl_TMP)
			$scope.newHouseName.HouseInchargeIdColl = $scope.newHouseName.HouseInchargeIdColl_TMP;
		else
			$scope.newHouseName.HouseInchargeIdColl = [];


		if ($scope.newHouseName.HouseMemberIdColl_TMP)
			$scope.newHouseName.HouseMemberIdColl = $scope.newHouseName.HouseMemberIdColl_TMP;
		else
			$scope.newHouseName.HouseMemberIdColl = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveHouseName",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newHouseName }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearHouseName();
				$scope.GetAllHouseNameList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllHouseNameList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HouseNameList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllHouseNameList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HouseNameList = res.data.Data;
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetHouseNameById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			HouseNameId: refData.HouseNameId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetHouseNameById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newHouseName = res.data.Data;
				$scope.newHouseName.Mode = 'Modify';

				var ethin = [];
				$timeout(function () {
					ethin = $scope.newHouseName.HouseInchargeIdColl;
					$scope.newHouseName.HouseInchargeIdColl_TMP = $scope.newHouseName.HouseInchargeIdColl;
					$('#cboIncharge').val(ethin).trigger('change');
				});

				$timeout(function () {
					ethin = $scope.newHouseName.HouseMemberIdColl;
					$scope.newHouseName.HouseMemberIdColl_TMP = $scope.newHouseName.HouseMemberIdColl;
					$('#cboMember').val(ethin).trigger('change');
				});

				document.getElementById('house-name-section').style.display = "none";
				document.getElementById('house-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelHouseNameById = function (refData) {

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
					HouseNameId: refData.HouseNameId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelHouseName",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllHouseNameList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});

	};


	//************************* HouseDress *********************************

	$scope.IsValidHouseDress = function () {
		if ($scope.newHouseDress.Name.isEmpty()) {
			Swal.fire('Please ! Enter HouseDress Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateHouseDress = function () {
		if ($scope.IsValidHouseDress() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newHouseDress.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateHouseDress();
					}
				});
			} else
				$scope.CallSaveUpdateHouseDress();

		}
	};

	$scope.CallSaveUpdateHouseDress = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveHouseDress",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newHouseDress }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearHouseDress();
				$scope.GetAllHouseDressList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllHouseDressList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HouseDressList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllHouseDressList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HouseDressList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetHouseDressById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			HouseDressId: refData.HouseDressId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetHouseDressById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newHouseDress = res.data.Data;
				$scope.newHouseDress.Mode = 'Modify';

				document.getElementById('house-dress-section').style.display = "none";
				document.getElementById('house-dress-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelHouseDressById = function (refData) {

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
					HouseDressId: refData.HouseDressId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelHouseDress",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllHouseDressList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* StudentType *********************************

	$scope.IsValidStudentType = function () {
		if ($scope.newStudentType.Name.isEmpty()) {
			Swal.fire('Please ! Enter StudentType Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateStudentType = function () {
		if ($scope.IsValidStudentType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudentType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudentType();
					}
				});
			} else
				$scope.CallSaveUpdateStudentType();

		}
	};

	$scope.CallSaveUpdateStudentType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveStudentType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newStudentType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStudentType();
				$scope.GetAllStudentTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllStudentTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllStudentTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetStudentTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			StudentTypeId: refData.StudentTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetStudentTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newStudentType = res.data.Data;
				$scope.newStudentType.Mode = 'Modify';

				document.getElementById('student-type-section').style.display = "none";
				document.getElementById('student-type-form').style.display = "block";
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelStudentTypeById = function (refData) {

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
					StudentTypeId: refData.StudentTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelStudentType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStudentTypeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Medium *********************************

	$scope.IsValidMedium = function () {
		if ($scope.newMedium.Name.isEmpty()) {
			Swal.fire('Please ! Enter Medium Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateMedium = function () {
		if ($scope.IsValidMedium() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newMedium.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateMedium();
					}
				});
			} else
				$scope.CallSaveUpdateMedium();

		}
	};

	$scope.CallSaveUpdateMedium = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveMedium",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newMedium }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearMedium();
				$scope.GetAllMediumList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllMediumList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.MediumList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllMediumList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.MediumList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetMediumById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			MediumId: refData.MediumId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetMediumById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newMedium = res.data.Data;
				$scope.newMedium.Mode = 'Modify';

				document.getElementById('medium-section').style.display = "none";
				document.getElementById('medium-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelMediumById = function (refData) {

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
					MediumId: refData.MediumId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelMedium",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllMediumList();
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