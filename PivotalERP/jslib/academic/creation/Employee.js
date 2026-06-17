app.controller('EmployeeCRController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Employee';

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			Department: 1,
			Designation: 1,
			Level: 1,
			ServiceType: 1,
			Category: 1,
			LeftType: 1
		};

		$scope.searchData = {
			Department: '',
			Designation: '',
			Level: '',
			ServiceType: '',
			Category: '',
			LeftType:''
		};

		$scope.perPage = {
			Department: GlobalServices.getPerPageRow(),
			Designation: GlobalServices.getPerPageRow(),
			Level: GlobalServices.getPerPageRow(),
			ServiceType: GlobalServices.getPerPageRow(),
			Category: GlobalServices.getPerPageRow(),
			LeftType: GlobalServices.getPerPageRow(),
		};

		$scope.newDepartment = {
			DepartmentId: null,
			Name: '',
			DisplayName: '',
			Code:'',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newDesignation = {
			DesignationId: null,
			Name: '',
			DisplayName: '',
			Code: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newLevel = {
			LevelId: null,
			Name: '',
			DisplayName: '',
			Code: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newServiceType = {
			ServiceTypeId: null,
			Name: '',
			DisplayName: '',
			Code: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newCategory = {
			CategoryId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newDet = {
			LeftTypeId: null,
			Name: '',
			OrderNum: 0,
			Code: '',
			Description: '',
			Mode: 'Save'
		};

		$scope.GetAllDepartmentList();
		//$scope.GetAllDesignationList();
		//$scope.GetAllServiceTypeList();
		//$scope.GetAllLevelList();
		//$scope.GetAllCategoryList();
		//$scope.GetAllLeftType();

	}

	function OnClickDefault() {
		document.getElementById('department-form').style.display = "none";
		document.getElementById('designation-form').style.display = "none";
		document.getElementById('level-form').style.display = "none";
		document.getElementById('service-type-form').style.display = "none";
		document.getElementById('category-form').style.display = "none";
		document.getElementById('Seperation-form').style.display = "none";

		// Department section

		document.getElementById('add-department').onclick = function () {
			document.getElementById('department-section').style.display = "none";
			document.getElementById('department-form').style.display = "block";
			$scope.ClearDepartment();
		}

		document.getElementById('back-department-list').onclick = function () {
			document.getElementById('department-section').style.display = "block";
			document.getElementById('department-form').style.display = "none";
			$scope.ClearDepartment();
		}



		// Designation section

		document.getElementById('add-designation').onclick = function () {
			document.getElementById('designation-section').style.display = "none";
			document.getElementById('designation-form').style.display = "block";
			$scope.ClearDesignation();
		}

		document.getElementById('back-designation-list').onclick = function () {
			document.getElementById('designation-section').style.display = "block";
			document.getElementById('designation-form').style.display = "none";
			$scope.ClearDesignation();
		}

		// Level section

		document.getElementById('add-level').onclick = function () {
			document.getElementById('level-section').style.display = "none";
			document.getElementById('level-form').style.display = "block";
			$scope.ClearLevel();
		}

		document.getElementById('back-level-list').onclick = function () {
			document.getElementById('level-section').style.display = "block";
			document.getElementById('level-form').style.display = "none";
			$scope.ClearLevel();
		}

		// service type
		document.getElementById('add-service-type').onclick = function () {
			document.getElementById('service-type-section').style.display = "none";
			document.getElementById('service-type-form').style.display = "block";
			$scope.ClearServiceType();
		}

		document.getElementById('back-service-type-list').onclick = function () {
			document.getElementById('service-type-section').style.display = "block";
			document.getElementById('service-type-form').style.display = "none";
			$scope.ClearServiceType();
		}

		// category
		document.getElementById('add-category').onclick = function () {
			document.getElementById('category-section').style.display = "none";
			document.getElementById('category-form').style.display = "block";
			$scope.ClearCategory();
		}

		document.getElementById('back-category-list').onclick = function () {
			document.getElementById('category-section').style.display = "block";
			document.getElementById('category-form').style.display = "none";
			$scope.ClearCategory();
		}

		//Seperation Type(Left Type)
		document.getElementById('add-Seperation').onclick = function () {
			document.getElementById('Seperation-section').style.display = "none";
			document.getElementById('Seperation-form').style.display = "block";
			$scope.ClearCategory();
		}

		document.getElementById('back-Seperation-list').onclick = function () {
			document.getElementById('Seperation-section').style.display = "block";
			document.getElementById('Seperation-form').style.display = "none";
			$scope.ClearCategory();
		}
	}

	$scope.ClearDepartment = function () {
		$timeout(function () {
			$scope.newDepartment = {
				DepartmentId: null,
				Name: '',
				DisplayName: '',
				Code: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});		
	}

	$scope.ClearDesignation = function () {
		$timeout(function () {
			$scope.newDesignation = {
				DesignationId: null,
				Name: '',
				DisplayName: '',
				Code: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});
		
	}
	$scope.ClearLevel = function () {
		$timeout(function () {
			$scope.newLevel = {
				LevelId: null,
				Name: '',
				DisplayName: '',
				Code: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});
		
	}
	$scope.ClearServiceType = function () {
		$timeout(function () {
			$scope.newServiceType = {
				ServiceTypeId: null,
				Name: '',
				DisplayName: '',
				Code: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});
		
	}

	$scope.ClearCategory = function () {
		$timeout(function () {
			$scope.newCategory = {
				CategoryId: null,
				Name: '',
				DisplayName: '',
				Code: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});
		

		
	}


	$scope.ClearLeftType = function () {
		$scope.newDet = {
			LeftTypeId: null,
			Name: '',
			OrderNum: 0,
			Code: '',
			Mode: 'Save'
		};
	}

	$scope.IsValidDepartment = function () {
		if ($scope.newDepartment.Name.isEmpty()) {
			Swal.fire('Please ! Enter Department Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateDepartment = function () {
		if ($scope.IsValidDepartment() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDepartment.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDepartment();
					}
				});
			} else
				$scope.CallSaveUpdateDepartment();

		}
	};

	$scope.CallSaveUpdateDepartment = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveDepartment",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newDepartment }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearDepartment();
				$scope.GetAllDepartmentList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllDepartmentList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.DepartmentList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllDepartmentList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DepartmentList = res.data.Data;				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetDepartmentById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			DepartmentId: refData.DepartmentId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetDepartmentById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDepartment = res.data.Data;
				$scope.newDepartment.Mode = 'Modify';

				document.getElementById('department-section').style.display = "none";
				document.getElementById('department-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelDepartmentById = function (refData) {

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
					DepartmentId: refData.DepartmentId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelDepartment",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllDepartmentList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//************************* Designation *********************************

	$scope.IsValidDesignation = function () {
		if ($scope.newDesignation.Name.isEmpty()) {
			Swal.fire('Please ! Enter Designation Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateDesignation = function () {
		if ($scope.IsValidDesignation() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDesignation.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDesignation();
					}
				});
			} else
				$scope.CallSaveUpdateDesignation();

		}
	};

	$scope.CallSaveUpdateDesignation = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveDesignation",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newDesignation }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearDesignation();
				$scope.GetAllDesignationList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllDesignationList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.DesignationList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllDesignationList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DesignationList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetDesignationById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			DesignationId: refData.DesignationId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetDesignationById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDesignation = res.data.Data;
				$scope.newDesignation.Mode = 'Modify';

				document.getElementById('designation-section').style.display = "none";
				document.getElementById('designation-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelDesignationById = function (refData) {

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
					DesignationId: refData.DesignationId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelDesignation",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllDesignationList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Level *********************************

	$scope.IsValidLevel = function () {
		if ($scope.newLevel.Name.isEmpty()) {
			Swal.fire('Please ! Enter Level Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateLevel = function () {
		if ($scope.IsValidLevel() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newLevel.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateLevel();
					}
				});
			} else
				$scope.CallSaveUpdateLevel();

		}
	};

	$scope.CallSaveUpdateLevel = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveLevel",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newLevel }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearLevel();
				$scope.GetAllLevelList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllLevelList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.LevelList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllLevelList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LevelList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetLevelById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			LevelId: refData.LevelId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetLevelById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newLevel = res.data.Data;
				$scope.newLevel.Mode = 'Modify';

				document.getElementById('level-section').style.display = "none";
				document.getElementById('level-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelLevelById = function (refData) {

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
					LevelId: refData.LevelId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelLevel",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllLevelList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* ServiceType *********************************

	$scope.IsValidServiceType = function () {
		if ($scope.newServiceType.Name.isEmpty()) {
			Swal.fire('Please ! Enter ServiceType Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateServiceType = function () {
		if ($scope.IsValidServiceType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newServiceType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateServiceType();
					}
				});
			} else
				$scope.CallSaveUpdateServiceType();

		}
	};

	$scope.CallSaveUpdateServiceType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveServiceType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newServiceType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearServiceType();
				$scope.GetAllServiceTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllServiceTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ServiceTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllServiceTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ServiceTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetServiceTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ServiceTypeId: refData.ServiceTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetServiceTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newServiceType = res.data.Data;
				$scope.newServiceType.Mode = 'Modify';

				document.getElementById('service-type-section').style.display = "none";
				document.getElementById('service-type-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelServiceTypeById = function (refData) {

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
					ServiceTypeId: refData.ServiceTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelServiceType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllServiceTypeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Category *********************************

	$scope.IsValidCategory = function () {
		if ($scope.newCategory.Name.isEmpty()) {
			Swal.fire('Please ! Enter Category Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateCategory = function () {
		if ($scope.IsValidCategory() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCategory.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCategory();
					}
				});
			} else
				$scope.CallSaveUpdateCategory();

		}
	};

	$scope.CallSaveUpdateCategory = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveCategory",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newCategory }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCategory();
				$scope.GetAllCategoryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllCategoryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CategoryList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllCategoryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CategoryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetCategoryById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			CategoryId: refData.CategoryId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetCategoryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCategory = res.data.Data;
				$scope.newCategory.Mode = 'Modify';

				document.getElementById('category-section').style.display = "none";
				document.getElementById('category-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCategoryById = function (refData) {

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
					CategoryId: refData.CategoryId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelCategory",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCategoryList();
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
		console.log('meals page changed to ' + num);		
	};
	$scope.DelEmployee = function (refData) {

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
					EmployeeId: refData.EmployeeId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelEmployee",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						 
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});

	};


	//************************* Left Type *********************************
	$scope.IsValidLeftType = function () {
		if ($scope.newDet.Name.isEmpty()) {
			Swal.fire('Please ! Enter  Name');
			return false;
		}

		return true;
	}


	$scope.SaveLeftType = function () {
		if ($scope.IsValidLeftType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateLeftType();
					}
				});
			} else
				$scope.CallSaveUpdateLeftType();
		}
	};

	$scope.CallSaveUpdateLeftType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveLeftType",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newDet }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearLeftType();
				$scope.GetAllLeftType();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}



	$scope.GetAllLeftType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.LeftTypeList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllLeftTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LeftTypeList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.getLeftTypeById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			LeftTypeId: refData.LeftTypeId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetLeftTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDet = res.data.Data;
				$scope.newDet.Mode = 'Modify';
				document.getElementById('Seperation-section').style.display = "none";
				document.getElementById('Seperation-form').style.display = "block";


			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};



	$scope.DelLeftTypeById = function (refData, ind) {
		Swal.fire({
			title: 'Do you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected BaliType :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = {
					LeftTypeId: refData.LeftTypeId
				};
				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelLeftType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetAllLeftType();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}

});