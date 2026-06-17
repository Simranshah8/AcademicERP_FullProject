app.controller('HealthExamController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Health Exam';
	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		/*$scope.MonthList = GlobalServices.getMonthList();*/

		$scope.currentPages = {
			ExamSetup: 1,
		};

		$scope.searchData = {
			ExamSetup: '',
			CopyExam: '',

		};

		$scope.perPage = {
			ExamSetup: GlobalServices.getPerPageRow(),
		

		};


		$scope.ClassList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.SectionList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllSectionList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SectionList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ExamTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ExamTypeListFiltered = angular.copy($scope.ExamTypeList);

		$scope.$watch('NewCopyExam.FromExamTermId', function (newVal, oldVal) {
			if (newVal !== oldVal) {
				$scope.ExamTypeListFiltered = $scope.ExamTypeList.filter(function (item) {
					return item.id !== newVal;
				});
			}
		});


		$scope.TestNameList = [];

		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllTestName",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TestNameList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.newExamSetup = {
			ExamCheckUpId: null,
			ClassTypeId: null,
			SectionTypeId: null,
			ExamTypeId: null,
			ClassCopyId: null,
			SectionCopyId: null,
			ExamCheckUpDetColl: [],
			Mode: 'Save'
		};



		$scope.newExamSetup.ExamCheckUpDetColl.push({});
		$scope.NewCopyExam = {
			ExamTypeId: null,		
			Mode: 'Save'
		};

		$scope.GetAllHealthChekUpExam();
		$scope.GetAllCopyExamTypeList();
		$scope.GetSingleHealthExamById();
	}

	function OnClickDefault() {
		document.getElementById('setup-form').style.display = "none";

		document.getElementById('add-examsetup').onclick = function () {
			document.getElementById('setuptabulation').style.display = "none";
			document.getElementById('setup-form').style.display = "block";
		}
		document.getElementById('back-btn').onclick = function () {
			document.getElementById('setup-form').style.display = "none";
			document.getElementById('setuptabulation').style.display = "block";
		}
			}

	$scope.ClearHealthChekUpExam = function () {
		$scope.newExamSetup = {
			ExamCheckUpId: null,
			ClassTypeId: null,
			SectionTypeId: null,
			ExamTypeId: null,
			ClassCopyId: null,
			SectionCopyId: null,
			ExamCheckUpDetColl: [],
			Mode: 'Save'
		};
		$scope.newExamSetup.ExamCheckUpDetColl.push({});
	}

	$scope.ClearCopyExam = function () {
		$scope.NewCopyExam = {
			FromExamTermId: null,
			ToExamTermId: null,
			Mode: 'Save'
		};
    }

	$scope.AddExamsetupDetails = function (ind) {
		if ($scope.newExamSetup.ExamCheckUpDetColl) {
			if ($scope.newExamSetup.ExamCheckUpDetColl.length > ind + 1) {
				$scope.newExamSetup.ExamCheckUpDetColl.splice(ind + 1, 0, {
					Name: ''
				})
			} else {
				$scope.newExamSetup.ExamCheckUpDetColl.push({
					Name: ''
				})
			}
		}
	};
	$scope.delExamSetupDetails = function (ind) {
		if ($scope.newExamSetup.ExamCheckUpDetColl) {
			if ($scope.newExamSetup.ExamCheckUpDetColl.length > 1) {
				$scope.newExamSetup.ExamCheckUpDetColl.splice(ind, 1);
			}
		}
	};



	//************************* TEst Group *********************************
	$scope.IsValidHealthChekUpExam = function () {
		//if ($scope.newExamSetup.Name.isEmpty()) {
		//	Swal.fire('Please ! Enter HealthChekUpExam Name');
		//	return false;
		//}
		return true;
	}

	$scope.SaveUpdateHealthChekUpExam = function () {
		if ($scope.IsValidHealthChekUpExam() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamSetup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateHealthChekUpExam();
					}
				});
			} else
				$scope.CallSaveUpdateHealthChekUpExam();

		}
	};

	$scope.CallSaveUpdateHealthChekUpExam = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/SaveUpdateHealthChekUpExam",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamSetup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearHealthChekUpExam();
				$scope.GetAllHealthChekUpExam();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllHealthChekUpExam = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HealthChekUpExamList = [];

		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllHealthChekUpExam",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";


			if (res.data.IsSuccess && res.data.Data) {
				$scope.HealthChekUpExamList = [];

				var query = mx(res.data.Data).groupBy(t => t.ExamTypeName);
				var sno = 1;
				angular.forEach(query, function (q) {
					var pare = {
						SNo: sno,
						ExamTypeName: q.key,
						ChieldColl: []
					};

					angular.forEach(q.elements, function (el) {
						pare.ChieldColl.push(el);
					})
					$scope.HealthChekUpExamList.push(pare);
					sno++;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetHealthChekUpExamById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ExamCheckUpId: refData.ExamCheckUpId
		};

		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/GetHealthChekUpExamById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExamSetup = res.data.Data;

				if (!$scope.newExamSetup.ExamCheckUpDetColl || $scope.newExamSetup.ExamCheckUpDetColl.length == 0) {
					$scope.newExamSetup.ExamCheckUpDetColl = [];
					$scope.newExamSetup.ExamCheckUpDetColl.push({});
				}
				$scope.newExamSetup.Mode = 'Modify';

				document.getElementById('setuptabulation').style.display = "none";
				document.getElementById('setup-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelHealthChekUpExamById = function (refData) {
		Swal.fire({
			title: 'Do you want to delete the selected data?' + refData.ClassTypeId + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ExamCheckUpId: refData.ExamCheckUpId
				};

				$http({
					method: 'POST',
					url: base_url + "Infirmary/Creation/DeleteHealthChekUpExam",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllHealthChekUpExam();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//************************* CopyExamType *********************************
	$scope.IsValidCopyExamType = function () {
		//if ($scope.NewCopyExam.Name.isEmpty()) {
		//	Swal.fire('Please ! Enter CopyExamTypeName');
		//	return false;
		//}
		return true;
	}

	$scope.SaveUpdateCopyExamType = function () {
		if ($scope.IsValidCopyExamType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.NewCopyExam.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCopyExamType();
					}
				});
			} else
				$scope.CallSaveUpdateCopyExamType();
		}
	};

	$scope.CallSaveUpdateCopyExamType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/SaveUpdateCopyExamType",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.NewCopyExam }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				//$scope.ClearCopyExamType();
				$scope.GetAllCopyExamTypeList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}



	$scope.GetAllCopyExamTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CopyExamTypeList = [];
		$http({
			method: 'GET',
			url: base_url + "Infirmary/Creation/GetAllCopyExamType",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CopyExamTypeList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetCopyExamTypeById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			CopyExamTypeId: refData.CopyExamTypeId
		};
		$http({
			method: 'POST',
			url: base_url + "Infirmary/Creation/getCopyExamTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.NewCopyExam = res.data.Data;
				$scope.NewCopyExam.Mode = 'Save';
				document.getElementById('Nametable').style.display = "none";
				document.getElementById('Nameform').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCopyExamTypeById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.FromExamName + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { CopyExamTypeId: refData.CopyExamTypeId };
				$http({
					method: 'POST',
					url: base_url + "Infirmary/Creation/DeleteCopyExamType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetAllCopyExamTypeList();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}



	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});