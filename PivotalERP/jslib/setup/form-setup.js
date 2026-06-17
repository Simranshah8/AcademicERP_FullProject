app.controller('FormSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Form Setup';
	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.UDFDataTypeList = GlobalServices.getUDFTypes();
		$scope.currentPages = {
			UserDefineField: 1,
		};
		 
		$scope.perPage = {
			UserDefineField: GlobalServices.getPerPageRow(),
		};

		$scope.newUserDefineField = {
			UserDefineFieldId: null,
			EntityId: '',
			FieldNo: 0,
			Name: '',
			AllowNull: false,
			Type: 1,
			AllowDuplicate: false,
			IsMandatory: false,
			Row: 1,
			DefaultValue: '',
			Length: 0,
			DisplayName: '',
			Mode: 'Save'
		};


		$scope.searchData = {
			FormConfiguration: '',
			DashboardConfiguration: '',
			UserDefineField: '',
		};

		

		$scope.newFormConfiguration = {
			FormConfigurationId: null,
			SelectDashboard: '',			
			Mode: 'Save'
		};

		$scope.newDashboardConfiguration = {
			DashboardConfigurationId: null,
			ToTitle: '',			
			Mode: 'Save'
		};

		$scope.DashboardEntityColl = [];
		$http({
			method: 'GET',
			url: base_url + "Setup/Security/GetDashboardEntity",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DashboardEntityColl = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

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

		$scope.UDFEntityList = [];
		$http({
			method: 'GET',
			url: base_url + "Setup/Security/GetUDFEntity",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UDFEntityList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllUserDefineFieldList();
	}

	function OnClickDefault() {
		document.getElementById('userdefile-form').style.display = "none";

		document.getElementById('add-field').onclick = function () {
			document.getElementById('form-list').style.display = "none";
			document.getElementById('userdefile-form').style.display = "block";
			$scope.ClearUserDefineField();
		}
		document.getElementById('backtoformlist').onclick = function () {
			document.getElementById('userdefile-form').style.display = "none";
			document.getElementById('form-list').style.display = "block";
		}
	};


	$scope.GetEntityFields = function (eId)
	{
		$scope.EntityFieldsColl = [];
		if (eId || eId > 0)
		{
			var para = {
				entityId: eId
			};
			$http({
				method: 'POST',
				url: base_url + "Setup/Security/GetEntityFields",
				dataType: "json",
				data:JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.EntityFieldsColl = res.data.Data;

					angular.forEach($scope.EntityFieldsColl, function (ef) {
						ef.IsAllow = false;
						ef.EntityId = eId;
						ef.FieldId = ef.Id;
						ef.ForUserId = 0;
					});

					$timeout(function () {
						if ($scope.newDashboardConfiguration.EntityId!=392)
							$scope.GetAllowFields($scope.newDashboardConfiguration.EntityId, 0)
					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        }		
	};

	$scope.GetAllowFields = function (eId,uId) {		
		if (eId || eId > 0) {
			var para = {
				ForUserId: uId,
				EntityId:eId
			};
			$http({
				method: 'POST',
				url: base_url + "Setup/Security/GetAllowFields",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					var idColl = mx(res.data.Data);
					$timeout(function () {
						angular.forEach($scope.EntityFieldsColl, function (f) {
							f.EntityId = para.EntityId;
							f.ForUserId = para.ForUserId;
							f.FieldId = f.id;

							if (idColl.contains(f.id) == true)
								f.IsAllow = true;
							else
								f.IsAllow = false;
						});
					});
					
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	};

	$scope.ClearFormConfiguration = function () {
		$scope.newFormConfiguration = {
			FormConfigurationId: null,
			SelectForm: '',			
			Mode: 'Save'
		};
	}
	$scope.ClearDashboardConfiguration = function () {
		$scope.newDashboardConfiguration = {
			DashboardConfigurationId: null,
			SelectDashboard: '',			
			Mode: 'Save'
		};
	}

	//************************* FormConfiguration *********************************
	$scope.SaveUpdateFormConfiguration = function () {
		if ($scope.IsValidFormConfiguration() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newFormConfiguration.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateFormConfiguration();
					}
				});
			} else
				$scope.CallSaveUpdateFormConfiguration();

		}
	};

	//************************* DashboardConfiguration *********************************
	$scope.IsValidDashboardConfiguration = function () {
		var isValid = true;

		return isValid;
	};

	$scope.TestClick = function (v) {
		v.IsAllow = !v.IsAllow;
	}

		$scope.SaveUpdateDashboardConfiguration = function () {
		if ($scope.IsValidDashboardConfiguration() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDashboardConfiguration.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDashboardConfiguration();
					}
				});
			} else
				$scope.CallSaveUpdateDashboardConfiguration();

		}
	};

	$scope.CallSaveUpdateDashboardConfiguration = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

	 
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveFormSetup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.EntityFieldsColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	//************************* UserDefineField *********************************

	$scope.ClearUserDefineField = function () {

		$timeout(function () {
			$scope.newUserDefineField = {
			UserDefineFieldId: null,
			EntityId: '',
			FieldNo: 0,
			Name: '',
			AllowNull: false,
			Type: 1,
			AllowDuplicate: false,
			IsMandatory: false,
			Row: 1,
			DefaultValue: '',
			Length: 0,
			DisplayName: '',
			Mode: 'Save'
			};
		});
		
	}

	$scope.IsValidUserDefineField = function () {
		if ($scope.newUserDefineField.Name.isEmpty()) {
			Swal.fire('Please ! Enter FieldName');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateUserDefineField = function () {
		if ($scope.IsValidUserDefineField() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newUserDefineField.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateUserDefineField();
					}
				});
			} else
				$scope.CallSaveUpdateUserDefineField();
		}
	};

	$scope.CallSaveUpdateUserDefineField = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/SaveUDFClass",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newUserDefineField }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearUserDefineField();
				$scope.GetAllUserDefineFieldList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllUserDefineFieldList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.UserDefineFieldList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllUDFClassList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UserDefineFieldList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.GetUserDefineFieldById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			id: refData.Id
		};

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetUDFClassById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newUserDefineField = res.data.Data;
				$scope.newUserDefineField.Mode = 'Modify';

				document.getElementById('form-list').style.display = "none";
				document.getElementById('userdefile-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelUserDefineFieldById = function (refData) {

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
					id: refData.Id
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/DelUDFClass",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllUserDefineFieldList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	$scope.isAllAllowed = false; // Track header switch state

	// Toggle all rows when clicking the header switch
	$scope.toggleAll = function () {
		$scope.isAllAllowed = !$scope.isAllAllowed; // Toggle state
		angular.forEach($scope.EntityFieldsColl, function (item) {
			item.IsAllow = $scope.isAllAllowed;
		});
	};

	// Toggle an individual row's switch
	$scope.toggleRow = function (f) {
		f.IsAllow = !f.IsAllow;

		// Check if all rows are ON or OFF and update the header switch state
		let allChecked = $scope.EntityFieldsColl.every(item => item.IsAllow);
		let noneChecked = $scope.EntityFieldsColl.every(item => !item.IsAllow);

		if (allChecked) {
			$scope.isAllAllowed = true;
		} else if (noneChecked) {
			$scope.isAllAllowed = false;
		}
	};


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});