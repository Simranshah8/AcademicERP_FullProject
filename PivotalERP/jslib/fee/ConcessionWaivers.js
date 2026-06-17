app.controller('ConcessionWaiverController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'ConcessionWaiver';
	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		/*$scope.MonthList = GlobalServices.getMonthList();*/
		$scope.ConcessionList = [{ id: 1, Name: 'Fee Item' }];
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();


		//$scope.MonthList = [];
		//GlobalServices.getMonthListFromDB().then(function (res1) {
		//	angular.forEach(res1.data.Data, function (m) {
		//		$scope.MonthList.push({ id: m.NM, text: m.MonthName });
		//	});

		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		$scope.currentPages = {
			Concession: 1,
			Waiver: 1,

		};

		$scope.searchData = {
			Concession: '',
			Waiver: '',

		};

		$scope.perPage = {
			Concession: GlobalServices.getPerPageRow(),
			Waiver: GlobalServices.getPerPageRow(),

		};

		$scope.newConcession = {
			ConcessionId: null,
			ConcessionTypeId: null,
			Remarks: '',
			FeeItemColl: [],
			SelectStudent: $scope.StudentSearchOptions[0].value,
			Mode: 'Save'
		};
		$scope.newConcession.FeeItemColl.push({});

		$scope.newWaiver = {
			WaiverId: null,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			WaiverDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newWaiver.WaiverDetailsColl.push({});

		/*$scope.GetAllConcessionList();*/
		//$scope.GetAllWaiverList();
	}

	function OnClickDefault() {
		document.getElementById('concessionForm').style.display = "none";
		document.getElementById('waiverform').style.display = "none";

		document.getElementById('add-concession-btn').onclick = function () {
			document.getElementById('concession-table').style.display = "none";
			document.getElementById('concessionForm').style.display = "block";
		}
		document.getElementById('back-concession-list').onclick = function () {
			document.getElementById('concessionForm').style.display = "none";
			document.getElementById('concession-table').style.display = "block";
		}


		document.getElementById('add-waiver-btn').onclick = function () {
			document.getElementById('waivertable').style.display = "none";
			document.getElementById('waiverform').style.display = "block";
		}
		document.getElementById('back-waiver-list').onclick = function () {
			document.getElementById('waiverform').style.display = "none";
			document.getElementById('waivertable').style.display = "block";
		}
	}
	$scope.CurMonthDetailsColl = [];
	$scope.ShowMonthSelectionForFeePeriod = function (L) {

		$scope.CurMonthDetailsColl = L.MonthDetailsColl;
		$('#selectmonth').modal('show');
	};

	$scope.ClearConcession = function () {
		$scope.newConcession = {
			ConcessionId: null,
			ConcessionTypeId: null,
			Remarks: '',
			SelectStudent: $scope.StudentSearchOptions[0].value,
			FeeItemColl: [],
			Mode: 'Save'
		};
		$scope.newConcession.FeeItemColl.push({});
	}
	$scope.ClearWaiver = function () {
		$scope.newWaiver = {
			WaiverId: null,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			WaiverDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newWaiver.WaiverDetailsColl.push({});
	}

	$scope.AddWaiverdetail = function (ind) {
		if ($scope.newWaiver.WaiverDetailsColl) {
			if ($scope.newWaiver.WaiverDetailsColl.length > ind + 1) {
				$scope.newWaiver.WaiverDetailsColl.splice(ind + 1, 0, {
					Name: ''
				})
			} else {
				$scope.newWaiver.WaiverDetailsColl.push({
					Name: ''
				})
			}
		}
	};
	$scope.delWaiverDetails = function (ind) {
		if ($scope.newWaiver.WaiverDetailsColl) {
			if ($scope.newWaiver.WaiverDetailsColl.length > 1) {
				$scope.newWaiver.WaiverDetailsColl.splice(ind, 1);
			}
		}
	};


	$scope.AddStudentWiseDisDet = function (ind) {
		if ($scope.newConcession.FeeItemColl) {
			if ($scope.newConcession.FeeItemColl.length > ind + 1) {
				$scope.newConcession.FeeItemColl.splice(ind + 1, 0, {
					Name: ''
				})
			} else {
				$scope.newConcession.FeeItemColl.push({
					Name: ''
				})
			}
		}
	};
	$scope.delStudentWiseDisDet = function (ind) {
		if ($scope.newConcession.FeeItemColl) {
			if ($scope.newConcession.FeeItemColl.length > 1) {
				$scope.newConcession.FeeItemColl.splice(ind, 1);
			}
		}
	};

	$scope.StudentCurMonthDetailsColl = [];
	$scope.ShowMonthSelectionForStudentWise = function (dt) {

		$scope.StudentCurMonthDetailsColl = dt.MonthDetailsColl;
		$('#modal-custom-month-studentWise').modal('show');
	};

	//************************* Concession *********************************

	$scope.IsValidConcession = function () {
		if ($scope.newConcession.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateConcession = function () {
		if ($scope.IsValidConcession() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newConcession.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateConcession();
					}
				});
			} else
				$scope.CallSaveUpdateConcession();

		}
	};

	$scope.CallSaveUpdateConcession = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveConcession",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newConcession }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearConcession();
				$scope.GetAllConcessionList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllConcessionList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ConcessionList = [];

		$http({
			method: 'GET',
			url: base_url + "Fee/Creation/GetAllConcession",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ConcessionList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetConcessionById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ConcessionId: refData.ConcessionId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/getConcessionById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newConcession = res.data.Data;
				$scope.newConcession.Mode = 'Modify';
				document.getElementById('Concessiontable').style.display = "none";
				document.getElementById('groupform').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelConcessionById = function (refData) {
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
					ConcessionId: refData.ConcessionId
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DeleteConcession",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllConcessionList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//*************************Waiver*********************************

	$scope.IsValidWaiver = function () {
		if ($scope.newWaiver.Name.isEmpty()) {
			Swal.fire('Please ! Enter Test Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateWaiver = function () {
		if ($scope.IsValidWaiver() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newWaiver.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateWaiver();
					}
				});
			} else
				$scope.CallSaveUpdateWaiver();

		}
	};

	$scope.CallSaveUpdateWaiver = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveWaiver",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newWaiver }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearWaiver();
				$scope.GetAllWaiverList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllWaiverList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.WaiverList = [];
		$http({
			method: 'GET',
			url: base_url + "Fee/Creation/GetAllWaiverList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.WaiverList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetWaiverById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			WaiverId: refData.WaiverId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetWaiverById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newWaiver = res.data.Data;
				$scope.newWaiver.Mode = 'Modify';

				document.getElementById('Waiver-content').style.display = "none";
				document.getElementById('Waiver-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelWaiverById = function (refData) {
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
					WaiverId: refData.WaiverId
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelWaiver",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllWaiverList();
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