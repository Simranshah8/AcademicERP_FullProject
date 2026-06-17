app.controller('FollowupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Setup';
	//OnClickDefault();
	$scope.LoadData = function () {

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			Followup: 1,
			TodaysFollowup: 1,
			PendingFollowup: 1,
			UpcomingFollowup: 1,
			FollowupNotRequired: 1

		};

		$scope.searchData = {
			Followup: '',
			TodaysFollowup: '',
			PendingFollowup: '',
			UpcomingFollowup: '',
			FollowupNotRequired: ''
		};

		$scope.perPage = {
			Followup: GlobalServices.getPerPageRow(),
			TodaysFollowup: GlobalServices.getPerPageRow(),
			PendingFollowup: GlobalServices.getPerPageRow(),
			UpcomingFollowup: GlobalServices.getPerPageRow(),
			FollowupNotRequired: GlobalServices.getPerPageRow()

		};

		$scope.newFollowup = {
			FollowupId: null,
			FollowupDate: null,
			FollowUpRemarks: '',
			NextFollowDate: null,
			NextFollowupTime:null,
			FollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newFollowup.FollowupDetailColl.push({});

		$scope.newTodaysFollowup = {
			TodaysFollowupId: null,
			FollowupDate: null,
			FollowupRemarks: '',
			NextFollowDate: null,
			NextFollowupTime: null,
			TodaysFollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newTodaysFollowup.TodaysFollowupDetailColl.push({});

		$scope.newPendingFollowup = {
			PendingFollowupId: null,
			FollowupDate: null,
			FollowupRemarks: '',
			NextFollowDate: null,
			NextFollowupTime: null,
			PendingFollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newPendingFollowup.PendingFollowupDetailColl.push({});



		$scope.newUpcomingFollowup = {
			UpcomingFollowupId: null,
			DOB: null,
			FollowupRemarks: '',
			NextFolloupDate: null,
			NextFolloupTime: null,
			UpcomingFollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newUpcomingFollowup.UpcomingFollowupDetailColl.push({});



		$scope.newFollowupNotRequired = {
			FollowupNotRequiredId: null,
			
			Mode: 'Save'
		};
				
		$scope.GetAllFollowupList();
		
	};

	function OnClickDefault() {
		document.getElementById('follow-up-required').style.display = "none";
		document.getElementById('follow-up-required-today').style.display = "none";
		document.getElementById('follow-up-pending').style.display = "none";
		document.getElementById('follow-up-upcoming').style.display = "none";
		document.getElementById('required').onclick = function () {
			document.getElementById('follow-up-required').style.display = "block";

		}
		document.getElementById('not-required').onclick = function () {
			document.getElementById('follow-up-required').style.display = "none";

		}

		document.getElementById('required-today').onclick = function () {
			document.getElementById('follow-up-required-today').style.display = "block";

		}
		document.getElementById('not-required-today').onclick = function () {
			document.getElementById('follow-up-required-today').style.display = "none";

		}


		document.getElementById('required-pending').onclick = function () {
			document.getElementById('follow-up-pending').style.display = "block";

		}
		document.getElementById('not-required-pending').onclick = function () {
			document.getElementById('follow-up-pending').style.display = "none";

		}

		document.getElementById('required-upcoming').onclick = function () {
			document.getElementById('follow-up-upcoming').style.display = "block";

		}
		document.getElementById('not-required-upcoming').onclick = function () {
			document.getElementById('follow-up-upcoming').style.display = "none";

		}


	};


	$scope.newFollowup = {};
	$scope.openFollowup = function (beData) {

		$scope.loadingstatus = "running";
		showPleaseWait();
		 
		$scope.newFollowup = {
			StudentId: beData.TranId,
			AutoNumber: beData.AutoNumber,
			Name: beData.Name,
			RegdNo: beData.RegdNo,
			FatherName: beData.FatherName,
			F_ContactNo: beData.F_ContactNo,
			ClassSection: beData.ClassName,
			NextFollowupRequired: false,
			RefTranId: beData.RefTranId,
		};

		var para = {
			TranId: beData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetEnqFollowupList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFollowup.HistoryColl = res.data.Data;
				$('#followup').modal('show');

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.closeFollowup = function (beData) {

		$scope.loadingstatus = "running";
		showPleaseWait();
		 
		$scope.newFollowup = {
			StudentId: beData.TranId,
			AutoNumber: beData.AutoNumber,
			Name: beData.Name,
			RegdNo: beData.RegdNo,
			FatherName: beData.FatherName,
			F_ContactNo: beData.F_ContactNo,
			ClassSection: beData.ClassName ,
			NextFollowupRequired: false,
			RefTranId: beData.RefTranId,
		};

		var para = {
			TranId: beData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetEnqFollowupList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFollowup.HistoryColl = res.data.Data;
				$('#followupClosed').modal('show');

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	}

	$scope.SaveUpdateFollowup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();


		if ($scope.newFollowup.PaymentDueDateDet) {
			$scope.newFollowup.PaymentDueDate = $filter('date')(new Date($scope.newFollowup.PaymentDueDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newFollowup.PaymentDueDate = $filter('date')(new Date(), 'yyyy-MM-dd');

		if ($scope.newFollowup.NextFollowupDateDet) {
			$scope.newFollowup.NextFollowupDate = $filter('date')(new Date($scope.newFollowup.NextFollowupDateDet.dateAD), 'yyyy-MM-dd');
		}

		if ($scope.newFollowup.NextFollowupTime_TMP)
			$scope.newFollowup.NextFollowupTime = $scope.newFollowup.NextFollowupTime_TMP.toLocaleString();
		else
			$scope.newFollowup.NextFollowupTime_TMP = null;

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveEnquiryFollowup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newFollowup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$('#followup').modal('hide');
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.SaveFollowupClosed = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			RefTranId: $scope.newFollowup.RefTranId,
			Remarks: $scope.newFollowup.ClosedRemarks
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveEnqFollowupClosed",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$('#followupClosed').modal('hide');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		 
	}

	$scope.ClearFollowup = function () {
		$scope.newFollowup = {
			FollowupId: null,
			FollowupDate: null,
			FollowUpRemarks: '',
			NextFollowDate: null,
			NextFollowupTime: null,
			FollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newFollowup.FollowupDetailColl.push({});
	}

	$scope.ClearTodaysFollowup = function () {
		$scope.newTodaysFollowup = {
			TodaysFollowupId: null,
			TodaysFollowupDate: null,
			TodaysFollowupRemarks: '',
			NextFollowDate: null,
			NextTodaysFollowupTime: null,
			TodaysFollowupDetailColl: [],
		};
		$scope.newTodaysFollowup.TodaysFollowupDetailColl.push({});
	}


	$scope.ClearPendingFollowup = function () {
		$scope.newPendingFollowup = {
			PendingFollowupId: null,
			FollowupDate: null,
			FollowupRemarks: '',
			NextFollowDate: null,
			NextFollowupTime: null,
			PendingFollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newPendingFollowup.PendingFollowupDetailColl.push({});
	}


	$scope.ClearUpcomingFollowup = function () {
		$scope.newUpcomingFollowup = {
			UpcomingFollowupId: null,
			DOB: null,
			FollowupRemarks: '',
			NextFolloupDate: null,
			NextFolloupTime: null,
			UpcomingFollowupDetailColl: [],
			Mode: 'Save'
		};
		$scope.newUpcomingFollowup.UpcomingFollowupDetailColl.push({});
	}


	$scope.ClearFollowupNotRequired = function () {
		$scope.newFollowupNotRequired = {
			FollowupNotRequiredId: null,
			Mode: 'Save'
		};
	}








	//*************************Followup *********************************

 

	$scope.GetAllFollowupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FollowupList = [];
		$scope.TodaysFollowupList = [];
		$scope.PendingFollowupList = [];
		$scope.UpcomingFollowupList = [];
		$scope.FollowupNotRequiredList = [];

		var para = {
			FollowupType:0
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetEnqFollowup",
			dataType: "json",
			data:JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FollowupList = res.data.Data;
				
				angular.forEach(res.data.Data, function (d) {

					if (d.FollowupType == 1)
						$scope.TodaysFollowupList.push(d);
					else if (d.FollowupType == 2)
						$scope.PendingFollowupList.push(d);
					else if (d.FollowupType == 3)
						$scope.UpcomingFollowupList.push(d);
					else if (d.FollowupType == 4)
						$scope.FollowupNotRequiredList.push(d);
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetFollowupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			FollowupId: refData.FollowupId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetSetupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFollowup = res.data.Data;
				$scope.newFollowup.Mode = 'Modify';

				document.getElementById('Setup-Employee').style.display = "none";
				document.getElementById('Setup-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelFollowupById = function (refData) {
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
					FollowupId: refData.FollowupId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelFollowup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFollowupList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//*************************Todays Followup *********************************

	$scope.IsValidTodaysFollowup = function () {
		if ($scope.newTodaysFollowup.FollowupRemarks.isEmpty()) {
			Swal.fire('Please ! Enter Followup Remarks');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateTodaysFollowup = function () {
		if ($scope.IsValidTodaysFollowup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newTodaysFollowup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateTodaysFollowup();
					}
				});
			} else
				$scope.CallSaveUpdateTodaysFollowup();

		}
	};

	$scope.CallSaveUpdateTodaysFollowup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveTodaysFollowup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newTodaysFollowup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearTodaysFollowup();
				$scope.GetAllTodaysFollowupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllTodaysFollowupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TodaysFollowupList = [];

		if ($scope.newTodaysFollowup.FollowupDateDet) {
			$scope.newTodaysFollowup.FollowupDate = $scope.newTodaysFollowup.FollowupDateDet.dateAD;
		} else
			$scope.newTodaysFollowup.FollowupDate = null;

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllTodaysFollowupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TodaysFollowupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetTodaysFollowupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TodaysFollowupId: refData.TodaysFollowupId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetTodaysFollowupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newTodaysFollowup = res.data.Data;
				$scope.newTodaysFollowup.Mode = 'Modify';

				document.getElementById('TodaysFollowup-content').style.display = "none";
				document.getElementById('TodaysFollowup-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelTodaysFollowupById = function (refData) {

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
					TodaysFollowupId: refData.TodaysFollowupId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelTodaysFollowup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllTodaysFollowupList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};




	//*************************Pending Followup *********************************

	$scope.IsValidPendingFollowup = function () {
		if ($scope.newPendingFollowup.FollowupRemarks.isEmpty()) {
			Swal.fire('Please ! Enter Followup Remarks');
			return false;
		}
		return true;
	}


	$scope.SaveUpdatePendingFollowup = function () {
		if ($scope.IsValidPendingFollowup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newPendingFollowup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdatePendingFollowup();
					}
				});
			} else
				$scope.CallSaveUpdatePendingFollowup();

		}
	};

	$scope.CallSaveUpdatePendingFollowup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();


		if ($scope.newPendingFollowup.FollowupDateDet) {
			$scope.newPendingFollowup.FollowupDate = $scope.newPendingFollowup.FollowupDateDet.dateAD;
		} else
			$scope.newPendingFollowup.FollowupDate = null;


		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SavePendingFollowup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newPendingFollowup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearPendingFollowup();
				$scope.GetAllPendingFollowupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllPendingFollowupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PendingFollowupList = [];

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllPendingFollowupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PendingFollowupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetPendingFollowupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			PendingFollowupId: refData.PendingFollowupId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetPendingFollowupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newPendingFollowup = res.data.Data;
				$scope.newPendingFollowup.Mode = 'Modify';

				document.getElementById('PendingFollowup-content').style.display = "none";
				document.getElementById('PendingFollowup-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelPendingFollowupById = function (refData) {

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
					PendingFollowupId: refData.PendingFollowupId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelPendingFollowup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllPendingFollowupList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	//*************************Upcoming Followup *********************************

	$scope.IsValidUpcomingFollowup = function () {
		if ($scope.newUpcomingFollowup.FollowupRemarks.isEmpty()) {
			Swal.fire('Please ! Enter Followup Remarks');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateUpcomingFollowup = function () {
		if ($scope.IsValidUpcomingFollowup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newUpcomingFollowup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateUpcomingFollowup();
					}
				});
			} else
				$scope.CallSaveUpdateUpcomingFollowup();

		}
	};

	$scope.CallSaveUpdateUpcomingFollowup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newUpcomingFollowup.FollowupDateDet) {
			$scope.newUpcomingFollowup.FollowupDate = $scope.newUpcomingFollowup.FollowupDateDet.dateAD;
		} else
			$scope.newUpcomingFollowup.FollowupDate = null;


		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/SaveUpcomingFollowup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newUpcomingFollowup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearUpcomingFollowup();
				$scope.GetAllUpcomingFollowupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllUpcomingFollowupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.UpcomingFollowupList = [];

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetAllUpcomingFollowupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.UpcomingFollowupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetUpcomingFollowupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			UpcomingFollowupId: refData.UpcomingFollowupId
		};

		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetUpcomingFollowupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newUpcomingFollowup = res.data.Data;
				$scope.newUpcomingFollowup.Mode = 'Modify';

				document.getElementById('UpcomingFollowup-content').style.display = "none";
				document.getElementById('UpcomingFollowup-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelUpcomingFollowupById = function (refData) {

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
					UpcomingFollowupId: refData.UpcomingFollowupId
				};

				$http({
					method: 'POST',
					url: base_url + "FrontDesk/Transaction/DelUpcomingFollowup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllUpcomingFollowupList();
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