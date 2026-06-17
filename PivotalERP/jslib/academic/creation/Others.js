app.controller('OthersController', function ($scope, $http, $timeout, $filter, $translate, $rootScope, GlobalServices) {
	$scope.Title = 'Others';
	$rootScope.ChangeLanguage();

	OnClickDefault();

	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			Caste: 1,
			RemarksType: 1,
			DocumentType: 1,
			InsuranceType:1
		};

		$scope.searchData = {
			Caste: '',
			RemarksType: '',
			DocumentType: '',
			InsuranceType: ''
		};

		$scope.perPage = {
			Caste: GlobalServices.getPerPageRow(),
			RemarksType: GlobalServices.getPerPageRow(),
			DocumentType: GlobalServices.getPerPageRow(),
			InsuranceType: GlobalServices.getPerPageRow()
		};

		$scope.newCaste = {
			CasteId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newRemarksType = {
			RemarksTypeId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newDocumentType = {
			DocumentTypeId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newInsuranceType = {
			InsuranceTypeId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.GetAllCasteList();
		//$scope.GetAllRemarksTypeList();
		//$scope.GetAllDocumentTypeList();
		//$scope.GetAllInsuranceTypeList();
		$scope.Labels = {
			Cast: 'Cast'
		};
		 
		$translate('CAST_LNG').then(function (data) {
			$scope.Labels.Cast = data;
		});
	}

	function OnClickDefault() {


		document.getElementById('caste-form').style.display = "none";
		document.getElementById('remarks-type-form').style.display = "none";
		document.getElementById('document-type-form').style.display = "none";
		document.getElementById('InsuranceType-form').style.display = "none";

		//   caste
		document.getElementById('add-caste').onclick = function () {
			document.getElementById('caste-section').style.display = "none";
			document.getElementById('caste-form').style.display = "block";
			$scope.ClearCaste();

		}
		document.getElementById('back-caste').onclick = function () {
			document.getElementById('caste-section').style.display = "block";
			document.getElementById('caste-form').style.display = "none";
			$scope.ClearCaste();
		}

		// remarks type
		document.getElementById('add-remarks').onclick = function () {
			document.getElementById('remarks-type-section').style.display = "none";
			document.getElementById('remarks-type-form').style.display = "block";
			$scope.ClearRemarksType();

		}
		document.getElementById('back-remarks-type').onclick = function () {
			document.getElementById('remarks-type-section').style.display = "block";
			document.getElementById('remarks-type-form').style.display = "none";
			$scope.ClearRemarksType();
		}

		// document type
		document.getElementById('add-document').onclick = function () {
			document.getElementById('document-type-section').style.display = "none";
			document.getElementById('document-type-form').style.display = "block";
			$scope.ClearDocumentType();

		}
		document.getElementById('back-document-type').onclick = function () {
			document.getElementById('document-type-section').style.display = "block";
			document.getElementById('document-type-form').style.display = "none";
			$scope.ClearDocumentType();
		}

		// Insurance type
		document.getElementById('add-insurance').onclick = function () {
			document.getElementById('insurance-section').style.display = "none";
			document.getElementById('InsuranceType-form').style.display = "block";
			$scope.ClearInsuranceType();

		}
		document.getElementById('back-insurance-type').onclick = function () {
			document.getElementById('insurance-section').style.display = "block";
			document.getElementById('InsuranceType-form').style.display = "none";
			$scope.ClearInsuranceType();
		}
	}

	$scope.ClearCaste = function () {

		$timeout(function () {
			$scope.newCaste = {
				CasteId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});
		
	}
	$scope.ClearRemarksType = function () {
		$timeout(function () {
			$scope.newRemarksType = {
				RemarksTypeId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});
	
	}
	$scope.ClearDocumentType = function () {
		$timeout(function () {
			$scope.newDocumentType = {
				DocumentTypeId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});
		

	}

	$scope.ClearInsuranceType = function () {
		$timeout(function () {
			$scope.newInsuranceType = {
				InsuranceTypeId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});
	}

	$scope.IsValidCaste = function () {
		if ($scope.newCaste.Name.isEmpty()) {
			Swal.fire('Please ! Enter ' + $scope.Labels.Cast+'  Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateCaste = function () {
		if ($scope.IsValidCaste() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCaste.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCaste();
					}
				});
			} else
				$scope.CallSaveUpdateCaste();

		}
	};

	$scope.CallSaveUpdateCaste = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveCaste",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newCaste }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCaste();
				$scope.GetAllCasteList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllCasteList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CasteList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllCasteList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CasteList = res.data.Data;				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetCasteById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			CasteId: refData.CasteId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetCasteById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCaste = res.data.Data;
				$scope.newCaste.Mode = 'Modify';

				document.getElementById('caste-section').style.display = "none";
				document.getElementById('caste-form').style.display = "block";


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCasteById = function (refData) {

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
					CasteId: refData.CasteId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelCaste",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCasteList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* RemarksType *********************************

	$scope.IsValidRemarksType = function () {
		if ($scope.newRemarksType.Name.isEmpty()) {
			Swal.fire('Please ! Enter RemarksType Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateRemarksType = function () {
		if ($scope.IsValidRemarksType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newRemarksType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateRemarksType();
					}
				});
			} else
				$scope.CallSaveUpdateRemarksType();

		}
	};

	$scope.CallSaveUpdateRemarksType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveRemarksType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newRemarksType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearRemarksType();
				$scope.GetAllRemarksTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllRemarksTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.RemarksTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllRemarksTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.RemarksTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetRemarksTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			RemarksTypeId: refData.RemarksTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetRemarksTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newRemarksType = res.data.Data;
				$scope.newRemarksType.Mode = 'Modify';

				document.getElementById('remarks-type-section').style.display = "none";
				document.getElementById('remarks-type-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelRemarksTypeById = function (refData) {

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
					RemarksTypeId: refData.RemarksTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelRemarksType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllRemarksTypeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* DocumentType *********************************

	$scope.IsValidDocumentType = function () {
		if ($scope.newDocumentType.Name.isEmpty()) {
			Swal.fire('Please ! Enter DocumentType Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateDocumentType = function () {
		if ($scope.IsValidDocumentType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDocumentType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDocumentType();
					}
				});
			} else
				$scope.CallSaveUpdateDocumentType();

		}
	};

	$scope.CallSaveUpdateDocumentType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveDocumentType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newDocumentType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearDocumentType();
				$scope.GetAllDocumentTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllDocumentTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.DocumentTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllDocumentTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DocumentTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetDocumentTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			DocumentTypeId: refData.DocumentTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetDocumentTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDocumentType = res.data.Data;
				$scope.newDocumentType.Mode = 'Modify';
				document.getElementById('document-type-section').style.display = "none";
				document.getElementById('document-type-form').style.display = "block";


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelDocumentTypeById = function (refData) {

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
					DocumentTypeId: refData.DocumentTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelDocumentType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllDocumentTypeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//************************* InsuranceType *********************************

	$scope.IsValidInsuranceType = function () {
		if ($scope.newInsuranceType.Name.isEmpty()) {
			Swal.fire('Please ! Enter Insurance Type Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateInsuranceType = function () {
		if ($scope.IsValidInsuranceType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newInsuranceType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateInsuranceType();
					}
				});
			} else
				$scope.CallSaveUpdateInsuranceType();

		}
	};

	$scope.CallSaveUpdateInsuranceType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveInsuranceType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newInsuranceType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearInsuranceType();
				$scope.GetAllInsuranceTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllInsuranceTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.InsuranceTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllInsuranceType",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.InsuranceTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetInsuranceTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			InsuranceId: refData.InsuranceId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetInsuranceTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newInsuranceType = res.data.Data;
				$scope.newInsuranceType.Mode = 'Modify';
				document.getElementById('insurance-section').style.display = "none";
				document.getElementById('InsuranceType-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelInsuranceTypeById = function (refData) {

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
					InsuranceId: refData.InsuranceId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelInsuranceType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllInsuranceTypeList();
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

});