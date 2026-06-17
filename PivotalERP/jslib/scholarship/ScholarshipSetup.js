app.controller('SetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Setup';
	OnClickDefault();
	$scope.LoadData = function () {

		var glbS = GlobalServices;
		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			Board: 1,
			School: 1,
			ReservationGroup: 1,
			ReservationType:1,
			Authority:1
		};

		$scope.searchData = {
			Board: '',
			School: '',
			ReservationGroup: '',
			ReservationType: '',
			Authority:''
		};

		$scope.perPage = {
			Board: GlobalServices.getPerPageRow(),
			School: GlobalServices.getPerPageRow(),
			ReservationGroup: GlobalServices.getPerPageRow(),
			ReservationType: GlobalServices.getPerPageRow(),
			Authority: GlobalServices.getPerPageRow(),

		};

		

		

		$scope.newBoard = {
			BoardId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newSchool = {
			SchoolId: null,
			Name: '',
			Address: '',
			Email: '',
			ContactNo: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newRGrp = {
			TranId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newRType = {
			TranId: null,
			ReservationGroupId:null,
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};
		$scope.newAuthority = {
			AuthorityId: null,
			Email: '',
			ContactNo: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res) {
			$scope.SubjectList = res.data.Data;
			angular.forEach($scope.SubjectList, function (sub) {
				sub.AllowSubject = false;
			});
			$scope.newSchool.SubjectList = angular.copy($scope.SubjectList);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllClassList();
		$scope.GetAllBoardList();
		$scope.GetAllSchoolList();
		$scope.GetAllReservationGroupList();
		$scope.GetAllReservationTypeList();
		$scope.GetAllAuthorityList();
	/*	$scope.GetAllSubjectList();*/
	}
	$scope.GetAllClassList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
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
				$scope.newSchool.ClassList = angular.copy(res.data.Data);
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	function OnClickDefault() {
		document.getElementById('source-form').style.display = "none";
		document.getElementById('setup-complain-form').style.display = "none";
		document.getElementById('rgrp-form').style.display = "none";
		document.getElementById('rtype-form').style.display = "none";
		document.getElementById('authority-form').style.display = "none";

		document.getElementById('add-source').onclick = function () {
			document.getElementById('source-section').style.display = "none";
			document.getElementById('source-form').style.display = "block";
			$scope.ClearBoard();
		}
		document.getElementById('sourceback-btn').onclick = function () {
			document.getElementById('source-form').style.display = "none";
			document.getElementById('source-section').style.display = "block";
			$scope.ClearBoard();
		}

		document.getElementById('add-setup-complain').onclick = function () {
			document.getElementById('setup-complain-section').style.display = "none";
			document.getElementById('setup-complain-form').style.display = "block";
			$scope.ClearSchool();
		}
		document.getElementById('setup-complainback-btn').onclick = function () {
			document.getElementById('setup-complain-form').style.display = "none";
			document.getElementById('setup-complain-section').style.display = "block";
			$scope.ClearSchool();
		}
		//Reservation Group
		document.getElementById('add-rgrp').onclick = function () {
			document.getElementById('rgrp-section').style.display = "none";
			document.getElementById('rgrp-form').style.display = "block";
			$scope.ClearSchool();
		}
		document.getElementById('rgrp-back-btn').onclick = function () {
			document.getElementById('rgrp-form').style.display = "none";
			document.getElementById('rgrp-section').style.display = "block";
			$scope.ClearSchool();
		}

		//Reservation Type
		document.getElementById('add-rtype').onclick = function () {
			document.getElementById('rtype-section').style.display = "none";
			document.getElementById('rtype-form').style.display = "block";
			$scope.ClearSchool();
		}
		document.getElementById('rtype-back-btn').onclick = function () {
			document.getElementById('rtype-form').style.display = "none";
			document.getElementById('rtype-section').style.display = "block";
			$scope.ClearSchool();
		}


		//Authority
		document.getElementById('add-authority').onclick = function () {
			document.getElementById('authority-section').style.display = "none";
			document.getElementById('authority-form').style.display = "block";
			$scope.ClearSchool();
		}
		document.getElementById('authorityback-btn').onclick = function () {
			document.getElementById('authority-form').style.display = "none";
			document.getElementById('authority-section').style.display = "block";
			$scope.ClearSchool();
		}
	}

	$scope.ClearBoard = function () {
		$scope.newBoard = {
			BoardId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};
	}
	$scope.ClearSchool = function () {
		$scope.newSchool = {
			SchoolId: null,
			Name: '',
			Address: '',
			Email: '',
			ContactNo: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};
		$scope.newSchool.ClassList = angular.copy($scope.ClassList);
		$scope.newSchool.SubjectList = angular.copy($scope.SubjectList);
		//$scope.clearAllowSubject();
	}

	$scope.clearAllowSubject = function () {
		$scope.newSchool.SubjectList.forEach(function (item) {
			item.AllowSubject = false;
		});
	};


	$scope.ClearReservationGroup = function () {
		$scope.newRGrp = {
			TranId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};
	}

	$scope.ClearReservationType = function () {
		$scope.newRType = {
			TranId: null,
			ReservationGroupId: null,
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};
	}

	$scope.ClearAuthority = function () {
		$scope.newAuthority = {
			AuthorityId: null,
			Email: '',
			ContactNo: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};
	}

	//************************* Equivalent Board *********************************

	$scope.IsValidBoard = function () {
		if ($scope.newBoard.Name.isEmpty()) {
			Swal.fire('Please ! Enter Board Name');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateBoard = function () {
		if ($scope.IsValidBoard() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBoard.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBoard();
					}
				});
			} else
				$scope.CallSaveUpdateBoard();

		}
	};

	$scope.CallSaveUpdateBoard = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Scholarship/SaveEquivalentBoard",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newBoard }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBoard();
				$scope.GetAllBoardList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllBoardList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BoardList = [];

		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllEquivalentBoard",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BoardList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	$scope.GetBoardById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			BoardId: refData.BoardId
		};

		$http({
			method: 'POST',
			url: base_url + "Scholarship/getEquivalentBoardById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBoard = res.data.Data;
				$scope.newBoard.Mode = 'Modify';

				document.getElementById('source-section').style.display = "none";
				document.getElementById('source-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	

	$scope.DelBoardById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = { BoardId: refData.BoardId };
				$http({
					method: 'POST',
					url: base_url + "Scholarship/DeleteEquivalentBoard",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";

					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.BoardList.splice(ind, 1);
						$scope.GetAllBoardList();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		});
	}
	//************************* Applied School *********************************

	$scope.IsValidSchool = function () {
		if ($scope.newSchool.Name.isEmpty()) {
			Swal.fire('Please ! Enter School Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateSchool = function () {
		if ($scope.IsValidSchool() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSchool.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSchool();
					}
				});
			} else
				$scope.CallSaveUpdateSchool();

		}
	};

	$scope.CallSaveUpdateSchool = function () {

		$scope.newSchool.ForClassIdColl = [];
		$scope.newSchool.SchoolSubjectListColl = $scope.newSchool.SubjectList;

		angular.forEach($scope.newSchool.ClassList, function (cl) {
			if (cl.IsSelected == true) {
				$scope.newSchool.ForClassIdColl.push(cl.ClassId);
            }
		});
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Scholarship/SaveAppliedSchool",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSchool }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearSchool();				
				$scope.GetAllSchoolList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllSchoolList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SchoolList = [];

		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllAppliedSchool",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SchoolList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetSchoolById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			SchoolId: refData.SchoolId
		};
		$http({
			method: 'POST',
			url: base_url + "Scholarship/getAppliedSchoolById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSchool = res.data.Data;
				$scope.newSchool.SubjectList = angular.copy($scope.SubjectList);
				//$scope.clearAllowSubject();
				$scope.newSchool.ClassList = angular.copy($scope.ClassList);
				var classIdColl = mx(res.data.Data.ForClassIdColl);
				angular.forEach($scope.newSchool.ClassList, function (cl) {
					if (classIdColl.contains(cl.ClassId)==true) {
						cl.IsSelected = true;
					} else {
						cl.IsSelected = false;
                    }
				});
				var SubjectList = mx(res.data.Data.SchoolSubjectListColl);

				$scope.newSchool.SubjectList.forEach(function (sub) {
					var findS = SubjectList.firstOrDefault(p1 => p1.SubjectId == sub.SubjectId);
					if (findS)
						sub.AllowSubject = true;
					else
						sub.AllowSubject = false;
				});

				$scope.newSchool.Mode = 'Modify';

				document.getElementById('setup-complain-section').style.display = "none";
				document.getElementById('setup-complain-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelSchoolById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = {
					SchoolId: refData.SchoolId
				};
				$http({
					method: 'POST',
					url: base_url + "Scholarship/DeleteAppliedSchool",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.SchoolList.splice(ind, 1);
						$scope.GetAllSchoolList();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	}
		
	//************************* Reservation Group *********************************

	$scope.IsValidReservationGroup = function () {
		if ($scope.newRGrp.Name.isEmpty()) {
			Swal.fire('Please ! Enter Reservation Group Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateReservationGroup = function () {
		if ($scope.IsValidReservationGroup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newRGrp.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateReservationGroup();
					}
				});
			} else
				$scope.CallSaveUpdateReservationGroup();

		}
	};

	$scope.CallSaveUpdateReservationGroup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Scholarship/SaveReservationGroup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newRGrp }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearReservationGroup();
				$scope.GetAllReservationGroupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllReservationGroupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ReservationGroupList = [];

		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllReservationGroup",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ReservationGroupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetReservationGroupById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Scholarship/getReservationGroupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newRGrp = res.data.Data;
				$scope.newRGrp.Mode = 'Modify';

				document.getElementById('rgrp-section').style.display = "none";
				document.getElementById('rgrp-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelReservationGroupById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = {
					TranId: refData.TranId
				};
				$http({
					method: 'POST',
					url: base_url + "Scholarship/DeleteReservationGroup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.ReservationGroupList.splice(ind, 1);
						$scope.GetAllReservationGroupList();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	}


	//************************* Reservation Type *********************************

	$scope.IsValidReservationType = function () {
		if ($scope.newRType.Name.isEmpty()) {
			Swal.fire('Please ! Enter Reservation Type Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateReservationType = function () {
		if ($scope.IsValidReservationType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newRType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateReservationType();
					}
				});
			} else
				$scope.CallSaveUpdateReservationType();

		}
	};

	$scope.CallSaveUpdateReservationType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Scholarship/SaveReservationType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newRType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearReservationType();
				$scope.GetAllReservationTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllReservationTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ReservationTypeList = [];

		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllReservationType",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ReservationTypeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetReservationTypeById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			TranId: refData.TranId
		};
		$http({
			method: 'POST',
			url: base_url + "Scholarship/getReservationTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newRType = res.data.Data;
				$scope.newRType.Mode = 'Modify';

				document.getElementById('rtype-section').style.display = "none";
				document.getElementById('rtype-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelReservationTypeById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = {
					TranId: refData.TranId
				};
				$http({
					method: 'POST',
					url: base_url + "Scholarship/DeleteReservationType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.ReservationTypeList.splice(ind, 1);
						$scope.GetAllReservationTypeList();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	}


	//************************* Authority *********************************

	$scope.IsValidAuthority = function () {
		if ($scope.newAuthority.Name.isEmpty()) {
			Swal.fire('Please ! Enter Reservation Type Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAuthority = function () {
		if ($scope.IsValidAuthority() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAuthority.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAuthority();
					}
				});
			} else
				$scope.CallSaveUpdateAuthority();

		}
	};

	$scope.CallSaveUpdateAuthority = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Scholarship/SaveAuthority",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newAuthority }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAuthority();
				$scope.GetAllAuthorityList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllAuthorityList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AuthorityList = [];
		$http({
			method: 'GET',
			url: base_url + "Scholarship/GetAllAuthority",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AuthorityList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAuthorityById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			AuthorityId: refData.AuthorityId
		};
		$http({
			method: 'POST',
			url: base_url + "Scholarship/getAuthorityById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAuthority = res.data.Data;
				$scope.newAuthority.Mode = 'Modify';

				document.getElementById('authority-section').style.display = "none";
				document.getElementById('authority-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAuthorityById = function (refData, ind) {
		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Name + '?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
			//message: 'Are you sure to delete selected Branch :-' + beData.Name,
		}).then((result) => {
			if (result.isConfirmed) {
				var para = {
					AuthorityId: refData.AuthorityId
				};
				$http({
					method: 'POST',
					url: base_url + "Scholarship/DeleteAuthority",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingStatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.AuthorityList.splice(ind, 1);
						$scope.GetAllAuthorityList();
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