app.controller('GatepassController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Gatepass';

	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();

		$scope.currentPages = {
			Gatepass: 1,
		};

		$scope.searchData = {
			Gatepass: '',
		};

		$scope.perPage = {
			Gatepass: GlobalServices.getPerPageRow(),
		};

		$scope.HostelStudentList = {};
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetHostelStudent",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HostelStudentList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});

		$scope.newGatepass = {
			GatePassId: null,
			StudentId: null,
			StudentPhoto: '',
			Purpose: '',
			OutDate_TMP: new Date(),
			OutTime_TMP: '',
			ExpectedInDate_TMP: new Date(),
			OutTime_TMP: '',
			AccompaniedBy: 2,
			GuardianId: null,
			GurdianName: '',
			Relation: '',
			PermittedBy: null,
			Remarks: '',
			AttachmentPath_TMP: '',
			SelectStudent: $scope.StudentSearchOptions[0].value,
			Mode: 'Save'
		};


		$scope.GetAllGatepassList();

	}

	function OnClickDefault() {

		document.getElementById('Gatepass-form').style.display = "none";

		//Gatepass section
		document.getElementById('add-Gatepass').onclick = function () {
			document.getElementById('Gatepass-section').style.display = "none";
			document.getElementById('Gatepass-form').style.display = "block";
			$scope.ClearGatepass();
		}

		document.getElementById('back-to-list-Gatepass').onclick = function () {
			document.getElementById('Gatepass-form').style.display = "none";
			document.getElementById('Gatepass-section').style.display = "block";
			$scope.ClearGatepass();
		}

	}

	$scope.ClearGatepass = function () {
		$scope.newGatepass = {
			GatePassId: null,
			StudentId: null,
			StudentPhoto: '',
			Purpose: '',
			OutDate_TMP: new Date(),
			OutTime_TMP: '',
			ExpectedInDate_TMP: new Date(),
			OutTime_TMP: '',
			AccompaniedBy: 2,
			GuardianId: null,
			GurdianName: '',
			G_Relation: '',
			G_ContactNo: '',
			PermittedBy: null,
			Remarks: '',
			AttachmentPath_TMP: '',
			SelectStudent: $scope.StudentSearchOptions[0].value,
			Mode: 'Save'
		};
		document.getElementById('studentPhotoPreview').src = '';
		document.getElementById("choose-file").value = "";
		document.getElementById('choose-Att').src = '';
		$scope.GetAutoNumber();
	}

	//*************************Gatepass *********************************

	$scope.GetAutoNumber = function () {
		$timeout(function () {
			$http({
				method: 'POST',
				url: base_url + "Hostel/Creation/getAutoGatePass",
				dataType: "json",
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				var st = res.data.Data;
				if (st.IsSuccess == true) {
					//$scope.newRegistration.RegNo = st.ResponseId;
					$scope.newGatepass.GatePassNo = st.ResponseId;
					$scope.newGatepass.AutoNumber = st.RId;
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});
	};
	$scope.onAccompaniedByChange = function () {
		if ($scope.newGatepass.AccompaniedBy == '1') {
			$scope.newGatepass.GurdianName = '';
			$scope.newGatepass.G_Relation = '';
			$scope.newGatepass.G_ContactNo = '';
			$scope.newGatepass.GuardianId = null;
		} else if ($scope.newGatepass.AccompaniedBy == '2') {
			$scope.newGatepass.GurdianName = '';
			$scope.newGatepass.G_Relation = '';
			$scope.newGatepass.G_ContactNo = '';
			$scope.newGatepass.GuardianId = null;
		}
	};

	$scope.GetGuardianByStd = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.GuardianListByStd = [];
		var para = {
			HostelId: null,
			BuildingId: null,
			FloorId: null,
			StudentId: $scope.newGatepass.StudentId
		};
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetGuardianByStdId",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				$scope.GuardianListByStd = res.data.Data;
				$scope.newGatepass.StudentPhoto = $scope.GuardianListByStd[0].StudentPhoto;
			} else {
				$scope.newGuardian.GuardianAddDetailsColl = [];
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});
	};

	$scope.onGuardianChange = function () {
		var selectedGuardian = $scope.GuardianListByStd.find(function (g) {
			return g.HostelGuardianId == $scope.newGatepass.GuardianId;
		});

		if (selectedGuardian) {
			$scope.newGatepass.GuardianName = selectedGuardian.GuardianName;
			$scope.newGatepass.G_Relation = selectedGuardian.Relation;
			$scope.newGatepass.G_ContactNo = selectedGuardian.ContactNo;
			//if (selectedGuardian.HostelGuardianId == 0) {
			//	 = null;
			//}
		} else {
			$scope.newGatepass.GuardianName = '';
			$scope.newGatepass.G_Relation = '';
			$scope.newGatepass.G_ContactNo = '';
		}
	};



	$scope.IsValidGatepass = function () {
		if ($scope.newGatepass.Purpose.isEmpty()) {
			Swal.fire('Please ! Enter Gatepass Purpose');
			return false;
		}


		return true;
	}

	$scope.SaveUpdateGatepass = function () {
		if ($scope.IsValidGatepass() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newGatepass.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateGatepass();
					}
				});
			} else
				$scope.CallSaveUpdateGatepass();
		}
	};

	$scope.CallSaveUpdateGatepass = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var attachment = $scope.newGatepass.AttachmentPath_TMP;
		//TODO: Combine DateTime
		if ($scope.newGatepass.OutDateDet && $scope.newGatepass.OutTime_TMP) {
			const datePart = new Date($scope.newGatepass.OutDateDet.dateAD);
			const timePart = new Date($scope.newGatepass.OutTime_TMP);
			// Combine date and time into a single Date object
			const combinedDateTime = new Date(
				datePart.getFullYear(),
				datePart.getMonth(),
				datePart.getDate(),
				timePart.getHours(),
				timePart.getMinutes(),
				timePart.getSeconds()
			);
			// Format the combined date and time
			$scope.newGatepass.OutDateTime = $filter('date')(combinedDateTime, 'yyyy-MM-dd HH:mm:ss');
		} else {
			$scope.newGatepass.OutDateTime = null;
		}
		if ($scope.newGatepass.ExpectedInDateDet && $scope.newGatepass.ExpectedTime_TMP) {
			const datePart = new Date($scope.newGatepass.ExpectedInDateDet.dateAD);
			const timePart = new Date($scope.newGatepass.ExpectedTime_TMP);
			const combinedDateTime = new Date(
				datePart.getFullYear(),
				datePart.getMonth(),
				datePart.getDate(),
				timePart.getHours(),
				timePart.getMinutes(),
				timePart.getSeconds()
			);
			$scope.newGatepass.ExpectedDateTime = $filter('date')(combinedDateTime, 'yyyy-MM-dd HH:mm:ss');
		} else {
			$scope.newGatepass.ExpectedDateTime = null;
		}
		if ($scope.newGatepass.GuardianId == 0) {
			$scope.newGatepass.GuardianId = null;
		}
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveHostelGatePass",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				if (data.Attachment && data.Attachment.length > 0)
					formData.append("attachment", data.Attachment[0]);

				return formData;
			},
			data: { jsonData: $scope.newGatepass, Attachment: attachment }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearGatepass();
				$scope.GetAllGatepassList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.GetAllGatepassList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.GatepassList = [];

		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllHostelGatePassList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.GatepassList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetGatepassById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			GatePassId: refData.GatePassId
		};
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetHostelGatePassById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newGatepass = res.data.Data;
				$scope.newGatepass.Mode = 'Modify';
				$scope.GetGuardianByStd($scope.newGatepass.StudentId);
				if ($scope.newGatepass.OutDateTime) {
					$scope.newGatepass.OutDate_TMP = new Date($scope.newGatepass.OutDateTime);
					$scope.newGatepass.OutTime_TMP = new Date($scope.newGatepass.OutDateTime);
				}

				if ($scope.newGatepass.ExpectedDateTime) {
					$scope.newGatepass.ExpectedInDate_TMP = new Date($scope.newGatepass.ExpectedDateTime);
					$scope.newGatepass.ExpectedTime_TMP = new Date($scope.newGatepass.ExpectedDateTime);
				}


                document.getElementById('Gatepass-section').style.display = "none";
                document.getElementById('Gatepass-form').style.display = "block";
            } else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelGatepassById = function (refData) {
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
					GatePassId: refData.GatePassId
				};
				$http({
					method: 'POST',
					url: base_url + "Hostel/Creation/DelHostelGatePass",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllGatepassList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};


	$scope.ShowArrivalDateTime = function (refData) {
		if (!refData.ArrivalDateTime) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				GatePassId: refData.GatePassId
			};
			$http({
				method: 'POST',
				url: base_url + "Hostel/Creation/GetAllHostelGatePassList",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.CurGatePass = res.data.Data[0];
					if ($scope.CurGatePass.ArrivalDateTime) {
						$scope.CurGatePass.ArrivalDate_TMP = new Date($scope.CurGatePass.ArrivalDateTime);
						$scope.CurGatePass.ArrivalTime_TMP = new Date($scope.CurGatePass.ArrivalDateTime);
					}
					$('#modal-xl').modal('show');
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}


	$scope.UpdateArrivalDateTime = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		//TODO: Combine DateTime
		if ($scope.CurGatePass.ArrivalDateDet && $scope.CurGatePass.ArrivalTime_TMP) {
			const datePart = new Date($scope.CurGatePass.ArrivalDateDet.dateAD);
			const timePart = new Date($scope.CurGatePass.ArrivalTime_TMP);
			// Combine date and time into a single Date object
			const combinedDateTime = new Date(
				datePart.getFullYear(),
				datePart.getMonth(),
				datePart.getDate(),
				timePart.getHours(),
				timePart.getMinutes(),
				timePart.getSeconds()
			);
			// Format the combined date and time
			$scope.CurGatePass.ArrivalDateTime = $filter('date')(combinedDateTime, 'yyyy-MM-dd HH:mm:ss');
		} else {
			$scope.CurGatePass.ArrivalDateTime = null;
		}
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/UpdateInTimeOfGatePass",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.CurGatePass }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearGatepass();
				$scope.GetAllGatepassList();
				$('#modal-xl').modal('hide');
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}


	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});