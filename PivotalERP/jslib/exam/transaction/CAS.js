app.controller('CASController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'CAS';

	OnClickDefault();

	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			CASType: 1,
			CASMarkEntry: 1,
			TeacherWiseSummary: 1,
			StudentWiseSummary: 1
		};

		$scope.searchData = {
			CASType: '',
			CASMarkEntry: '',
			TeacherWiseSummary: '',
			StudentWiseSummary: ''
		};

		$scope.perPage = {
			CASType: GlobalServices.getPerPageRow(),
			CASMarkEntry: GlobalServices.getPerPageRow(),
			TeacherWiseSummary: GlobalServices.getPerPageRow(),
			StudentWiseSummary: GlobalServices.getPerPageRow()
		};

		$scope.newCASType = {
			CASTypeId: null,
			Name: '',
			MinimumValue: '',
			MaximumValue: '',
			IsGrading: true,
			IsActive: false,
			Mode: 'Save'
		};

		$scope.newCASMarkEntry = {
			CASMarkEntryId: null,
			CASMarkEntryDetailsColl:[],
			Mode: 'Save'
		};
		$scope.newCASMarkEntry.CASMarkEntryDetailsColl.push({});

		$scope.newTeacherWiseSummary = {
			TeacherWiseSummaryId: null,
			
			Mode: 'Save'
		};

		$scope.newStudentWiseSummary = {
			StudentWiseSummaryId: null,
			
			Mode: 'Save'
		};

		//$scope.GetAllCASTypeList();
		//$scope.GetAllCASMarkEntryList();
		//$scope.GetAllTeacherWiseSummaryList();
		//$scope.GetAllStudentWiseSummaryList();

	}

	function OnClickDefault() {


		document.getElementById('cas-type-form').style.display = "none";

		document.getElementById('add-cas-type').onclick = function () {
			document.getElementById('cas-type-section').style.display = "none";
			document.getElementById('cas-type-form').style.display = "block";
			$scope.ClearCASType();
		}
		document.getElementById('back-cas-type').onclick = function () {
			document.getElementById('cas-type-section').style.display = "block";
			document.getElementById('cas-type-form').style.display = "none";
			$scope.ClearCASType();
		}

		
	}

	$scope.ClearCASType = function () {
		$scope.newCASType = {
			CASTypeId: null,
			Name: '',
			MinimumValue: '',
			MaximumValue: '',
			IsGrading: true,
			IsActive: false,
			Mode: 'Save'
		};
	}
	$scope.ClearCASMarkEntry = function () {
		$scope.newCASMarkEntry = {
			CASMarkEntryId: null,
			CASMarkEntryDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newCASMarkEntry.CASMarkEntryDetailsColl.push({});
	}
	$scope.ClearTeacherWiseSummary = function () {
		$scope.newTeacherWiseSummary = {
			TeacherWiseSummaryId: null,

			Mode: 'Save'
		};
	}
	$scope.ClearStudentWiseSummary = function () {
		$scope.newStudentWiseSummary = {
			StudentWiseSummaryId: null,

			Mode: 'Save'
		};
	}

	//************************* CAS Type *********************************

	$scope.IsValidCASType = function () {
		if ($scope.newCASType.Name.isEmpty()) {
			Swal.fire('Please ! Enter CAS Type Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateCASType = function () {
		if ($scope.IsValidCASType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCASType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCASType();
					}
				});
			} else
				$scope.CallSaveUpdateCASType();

		}
	};

	$scope.CallSaveUpdateCASType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveCASType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newCASType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCASType();
				$scope.GetAllCASTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllCASTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CASTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllCASTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CASTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetCASTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			CASTypeId: refData.CASTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetCASTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCASType = res.data.Data;
				$scope.newCASType.Mode = 'Modify';

				document.getElementById('class-section').style.display = "none";
				document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCASTypeById = function (refData) {

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
					CASTypeId: refData.CASTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelCASType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCASTypeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* CAS Mark Entry *********************************

	$scope.IsValidCASMarkEntry = function () {
		if ($scope.newCASMarkEntry.Name.isEmpty()) {
			Swal.fire('Please ! Enter CASMarkEntry Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateCASMarkEntry = function () {
		if ($scope.IsValidCASMarkEntry() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCASMarkEntry.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCASMarkEntry();
					}
				});
			} else
				$scope.CallSaveUpdateCASMarkEntry();

		}
	};

	$scope.CallSaveUpdateCASMarkEntry = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newMarkEntry.DateDet) {
			$scope.newMarkEntry.Date = $scope.newMarkEntry.DateDet.dateAD;
		} else
			$scope.newMarkEntry.Date = null;

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveCASMarkEntry",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newCASMarkEntry }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCASMarkEntry();
				$scope.GetAllCASMarkEntryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllCASMarkEntryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CASMarkEntryList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllCASMarkEntryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CASMarkEntryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetCASMarkEntryById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			CASMarkEntryId: refData.CASMarkEntryId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetCASMarkEntryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCASMarkEntry = res.data.Data;
				$scope.newCASMarkEntry.Mode = 'Modify';

				document.getElementById('section-content').style.display = "none";
				document.getElementById('section-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCASMarkEntryById = function (refData) {

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
					CASMarkEntryId: refData.CASMarkEntryId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelCASMarkEntry",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCASMarkEntryList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Teacher Wise Summary *********************************

	//$scope.IsValidTeacherWiseSummary = function () {
	//	if ($scope.newTeacherWiseSummary.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter TeacherWiseSummary Name');
	//		return false;
	//	}

	//	return true;
	//}

	$scope.SaveUpdateTeacherWiseSummary = function () {
		if ($scope.IsValidTeacherWiseSummary() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newTeacherWiseSummary.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateTeacherWiseSummary();
					}
				});
			} else
				$scope.CallSaveUpdateTeacherWiseSummary();

		}
	};

	$scope.CallSaveUpdateTeacherWiseSummary = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newTeacherWiseSummary.FromDateDet) {
			$scope.newTeacherWiseSummary.FromDate = $scope.newTeacherWiseSummary.FromDateDet.dateAD;
		} else
			$scope.newTeacherWiseSummary.FromDate = null;

		if ($scope.newTeacherWiseSummary.ToDateDet) {
			$scope.newTeacherWiseSummary.ToDate = $scope.newTeacherWiseSummary.ToDateDet.dateAD;
		} else
			$scope.newTeacherWiseSummary.ToDate = null;

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveTeacherWiseSummary",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newTeacherWiseSummary }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearTeacherWiseSummary();
				$scope.GetAllTeacherWiseSummaryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllTeacherWiseSummaryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TeacherWiseSummaryList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllTeacherWiseSummaryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TeacherWiseSummaryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetTeacherWiseSummaryById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TeacherWiseSummaryId: refData.TeacherWiseSummaryId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetTeacherWiseSummaryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newTeacherWiseSummary = res.data.Data;
				$scope.newTeacherWiseSummary.Mode = 'Modify';

				document.getElementById('batch-section').style.display = "none";
				document.getElementById('batch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelTeacherWiseSummaryById = function (refData) {

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
					TeacherWiseSummaryId: refData.TeacherWiseSummaryId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelTeacherWiseSummary",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllTeacherWiseSummaryList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Student Wise Summary *********************************

	//$scope.IsValidStudentWiseSummary = function () {
	//	if ($scope.newStudentWiseSummary.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter StudentWiseSummary Name');
	//		return false;
	//	}

	//	return true;
	//}

	$scope.SaveUpdateStudentWiseSummary = function () {
		if ($scope.IsValidStudentWiseSummary() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudentWiseSummary.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudentWiseSummary();
					}
				});
			} else
				$scope.CallSaveUpdateStudentWiseSummary();

		}
	};

	$scope.CallSaveUpdateStudentWiseSummary = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newStudentWiseSummary.FromDateDet) {
			$scope.newStudentWiseSummary.FromDate = $scope.newStudentWiseSummary.FromDateDet.dateAD;
		} else
			$scope.newStudentWiseSummary.FromDate = null;

		if ($scope.newStudentWiseSummary.ToDateDet) {
			$scope.newStudentWiseSummary.ToDate = $scope.newStudentWiseSummary.ToDateDet.dateAD;
		} else
			$scope.newStudentWiseSummary.ToDate = null;

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveStudentWiseSummary",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newStudentWiseSummary }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStudentWiseSummary();
				$scope.GetAllStudentWiseSummaryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllStudentWiseSummaryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentWiseSummaryList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllStudentWiseSummaryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentWiseSummaryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetStudentWiseSummaryById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			StudentWiseSummaryId: refData.StudentWiseSummaryId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetStudentWiseSummaryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newStudentWiseSummary = res.data.Data;
				$scope.newStudentWiseSummary.Mode = 'Modify';

				document.getElementById('board-section').style.display = "none";
				document.getElementById('board-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelStudentWiseSummaryById = function (refData) {

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
					StudentWiseSummaryId: refData.StudentWiseSummaryId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelStudentWiseSummary",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStudentWiseSummaryList();
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