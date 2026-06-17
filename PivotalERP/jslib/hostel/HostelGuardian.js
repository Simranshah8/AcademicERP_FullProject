app.controller('GuardianController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Guardian';

	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.studentColl = [{ id: 1, text: 'Shyam' }, { id: 2, text: 'Hari' }, { id: 3, text: 'Gopal' }];
		$scope.currentPages = {
			Guardian: 1,

		};

		$scope.searchData = {
			Guardian: '',

		};

		$scope.perPage = {
			Guardian: GlobalServices.getPerPageRow(),

		};

		$scope.newFilter = {}
		$scope.HostelList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllHostelList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.HostelList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.BuildingList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllBuildingList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BuildingList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.FloorList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllFloorList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FloorList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newGuardian = {
			GuardianId: null,
			HostelId: null,
			BuildingId: null,
			FloorId: null,
			StudentId: null,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			GuardianAddDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newGuardian.GuardianAddDetailsColl.push({});


		$scope.GetAllGuardianList();

	}


	$scope.AddGuardianAddDetails = function (ind) {
		if ($scope.newGuardian.GuardianAddDetailsColl) {
			if ($scope.newGuardian.GuardianAddDetailsColl.length > ind + 1) {
				$scope.newGuardian.GuardianAddDetailsColl.splice(ind + 1, 0, {
					Relation: ''
				})
			} else {
				$scope.newGuardian.GuardianAddDetailsColl.push({
					Relation: ''
				})
			}
		}
	};
	$scope.delGuardianAddDetails = function (ind) {
		if ($scope.newGuardian.GuardianAddDetailsColl) {
			if ($scope.newGuardian.GuardianAddDetailsColl.length > 1) {
				$scope.newGuardian.GuardianAddDetailsColl.splice(ind, 1);
			}
		}
	};

	function OnClickDefault() {

		document.getElementById('Guardian-form').style.display = "none";

		//Guardian section
		document.getElementById('add-Guardian').onclick = function () {
			document.getElementById('Guardian-section').style.display = "none";
			document.getElementById('Guardian-form').style.display = "block";
			$scope.ClearGuardian();
		}

		document.getElementById('back-to-list-Guardian').onclick = function () {
			document.getElementById('Guardian-form').style.display = "none";
			document.getElementById('Guardian-section').style.display = "block";
			$scope.ClearGuardian();
		}

	}

	$scope.ClearGuardian = function () {
		$scope.newGuardian = {
			GuardianId: null,
			HostelId: null,
			BuildingId: null,
			FloorId: null,
			StudentId: null,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			GuardianAddDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newGuardian.GuardianAddDetailsColl.push({});
	}

	//*************************Guardian *********************************

	$scope.IsValidGuardian = function () {
		//if ($scope.newGuardian.GuardianName.isEmpty()) {
		//	Swal.fire('Please ! Enter Guardian Name');
		//	return false;
		//}
		return true;
	}

	$scope.SaveUpdateGuardian = function () {
		if ($scope.IsValidGuardian() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newGuardian.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateGuardian();
					}
				});
			} else
				$scope.CallSaveUpdateGuardian();

		}
	};

	$scope.CallSaveUpdateGuardian = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var tmpDataColl = [];
		var photoFiles = [];
		var docFiles = [];
		angular.forEach($scope.newGuardian.GuardianAddDetailsColl, function (det) {
			det.StudentId = $scope.newGuardian.StudentId;
			tmpDataColl.push(det);
			photoFiles.push(det.PhotoPath_TMP && det.PhotoPath_TMP.length > 0 ? det.PhotoPath_TMP[0] : null);
			docFiles.push(det.DocumentPathTMP && det.DocumentPathTMP.length > 0 ? det.DocumentPathTMP[0] : null);
		});
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveHostelGuardian",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				for (var i = 0; i < data.photos.length; i++) {
					if (data.photos[i]) {
						formData.append("gphoto" + i, data.photos[i]);
					}
					if (data.docs[i]) {
						formData.append("gdocument" + i, data.docs[i]);
					}
				}
				return formData;
			},
			data: {
				jsonData: tmpDataColl,
				photos: photoFiles,
				docs: docFiles
			}
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess) {
				$scope.GetAllGuardianList();
				$scope.ClearGuardian();
			}
		}, function () {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire("Error occurred while saving guardian.");
		});
	};

	$scope.GetAllGuardianList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.GuardianList = [];
		var para = {
			HostelId: $scope.newFilter.HostelId ,
			BuildingId: $scope.newFilter.BuildingId,
			FloorId: $scope.newFilter.FloorId,
		};
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllHostelGuardian",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dataColl = mx(res.data.Data);
				var query = dataColl.groupBy(t => ({ StudentId: t.StudentId }));
				angular.forEach(query, function (group) {
					var fst = group.elements[0];
					var guardianDetails = group.elements.map(function (g) {
						return {
							HostelGuardainId: g.HostelGuardainId,
							GuardianName: g.GuardianName,
							Relation: g.Relation,
							ContactNo: g.ContactNo,
							PhotoPath: g.PhotoPath,
							Document: g.Document
						};
					});
					var beData = {
						StudentId: fst.StudentId,
						StudentName: fst.StudentName,
						RegNo: fst.RegNo,
						ClassSection: fst.ClassSection,
						RoomDetail: fst.RoomDetail,
						GuardianDetails: guardianDetails
					};
					$scope.GuardianList.push(beData);
				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});
	};

	$scope.GetGuardianByStdId = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentGuardianList = [];
		var para = {
			StudentId: refData.StudentId || null
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
				var dataColl = mx(res.data.Data);
				var query = dataColl.groupBy(t => ({ StudentId: t.StudentId }));
				angular.forEach(query, function (group) {
					var fst = group.elements[0];
					var guardianDetails = group.elements.map(function (g) {
						return {
							HostelGuardianId: g.HostelGuardianId,
							GuardianName: g.GuardianName,
							Relation: g.Relation,
							ContactNo: g.ContactNo,
							PhotoPath: g.PhotoPath,
							Document: g.Document
						};
					});
					var beData = {
						StudentId: fst.StudentId,
						StudentName: fst.StudentName,
						RegNo: fst.RegNo,
						ClassSection: fst.ClassSection,
						RoomDetail: fst.RoomDetail,
						GuardianDetails: guardianDetails
					};
					$scope.StudentGuardianList.push(beData);
				});
				$('#guardiandetail').modal('show');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});
	};

	$scope.GetHostelStudent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.HostelStudentList = {};
		var para = {
			HostelId: $scope.newGuardian.HostelId,
			BuildingId: $scope.newGuardian.BuildingId,
			FloorId: $scope.newGuardian.FloorId
		};
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetHostelStudent",
			dataType: "json",
			data: JSON.stringify(para)
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
	};

	//TODO: Get StdGuardianById
	$scope.GetStdGuardianById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentGuardianList = [];
		var para = {
			StudentId: $scope.newGuardian.StudentId
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
				var dataColl = mx(res.data.Data);
				var query = dataColl.groupBy(t => ({ StudentId: t.StudentId }));
				angular.forEach(query, function (group) {
					var fst = group.elements[0];
					var guardianDetails = group.elements.map(function (g) {
						return {
							HostelGuardainId: g.HostelGuardainId,
							GuardianName: g.GuardianName,
							Relation: g.Relation,
							ContactNo: g.ContactNo,
							PhotoPath: g.PhotoPath,
							Document: g.Document
						};
					});
					var beData = {
						StudentId: fst.StudentId,
						StudentName: fst.StudentName,
						RegNo: fst.RegNo,
						ClassSection: fst.ClassSection,
						RoomDetail: fst.RoomDetail,
						GuardianDetails: guardianDetails
					};
					$scope.StudentGuardianList.push(beData);
					$scope.newGuardian.GuardianAddDetailsColl = angular.copy(guardianDetails);
				});
			} else {
				$scope.newGuardian.GuardianAddDetailsColl = [];
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});
	};


	$scope.DelGuardianById = function (refData) {
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
					StudentId: refData.StudentId
				};
				$http({
					method: 'POST',
					url: base_url + "Hostel/Creation/DelHostelGuardainByStdId",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllGuardianList();
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