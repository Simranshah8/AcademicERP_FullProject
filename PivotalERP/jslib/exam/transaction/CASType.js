app.controller('CASTypeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'CAS Type';
	var glbS = GlobalServices;

	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.UnderColl = [{ id: 1, text: 'General' }, { id: 2, text: 'Assignment' }, { id: 3, text: 'Class Test' }, { id: 4, text: 'Attendance' }, { id: 5, text: 'Exam Type' }, { id: 6, text: 'Exam Attendance' }, { id: 7, text: 'CAS MarkEntry' }];
		$scope.SchemeColl = [{ id: 1, text: 'Input Mark' }, { id: 2, text: 'Get Mark' },]
		$scope.AttendanceFromColl = [{ id: 1, text: 'Auto Attendance(Biometric)' }, { id: 2, text: 'Daily Attendance(Manual)' }, { id: 3, text: 'Exam Attendance' }];

		$scope.currentPages = {
			CASType: 1,
			MarkSetup: 1,
			MarkSetupStatus: 1,
		};

		$scope.searchData = {
			CASType: '',
			MarkSetup: '',
			MarkSetupStatus: '',
		};

		$scope.perPage = {
			CASType: GlobalServices.getPerPageRow(),
			MarkSetup: GlobalServices.getPerPageRow(),
			MarkSetupStatus: GlobalServices.getPerPageRow(),
		};

		$scope.newCASType = {
			CASTypeId: null,
			Name: '',
			OrderNo: null,
			Description: '',
			IsActive: true,
			Mode: 'Save'
		};
		$scope.newMarkSetup = {
			MarkSetupId: null,
			ClassId: null,
			SubjectId: null,
			FullMark: 0,
			ExamType: '',
			MarkDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newMarkSetup.MarkDetailsColl.push({
			Mark: 0,
			Under: 1,
			Scheme: 1
		});

		$scope.newMarkSetupStatus = {
			MarkSetupStatusId: null,
			ClassSecId: null,
			SubjectId: null,
			DateFrom_TMP: '',
			Marks: '',
			DateTo_TMP: '',
			Mode: 'Save'
		};
		$scope.GetAllCASTypeList();

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ExamTypeList = [];
		glbS.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.GetAllMarkSetupList();
	}

	function OnClickDefault() {
		document.getElementById('cas-type-form').style.display = "none";

		document.getElementById('add-cas-type').onclick = function () {
			document.getElementById('cas-type-section').style.display = "none";
			document.getElementById('cas-type-form').style.display = "block";
		}
		document.getElementById('back-cas-type').onclick = function () {
			document.getElementById('cas-type-section').style.display = "block";
			document.getElementById('cas-type-form').style.display = "none";
		}
	}

	$scope.ClearCASType = function () {
		$scope.newCASType = {
			CASTypeId: null,
			Name: '',
			OrderNo: null,
			Description: '',
			IsActive: true,
			Mode: 'Save'
		};
	}

	$scope.ClearMarkSetup = function () {
		$scope.newMarkSetup = {
			MarkSetupId: null,
			ClassId: null,
			SubjectId: null,
			FullMark: 0,
			ExamType: '',
			MarkDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newMarkSetup.MarkDetailsColl.push({});
		$scope.ExamClassSubjectList = [];
	}

	$scope.ClearMarkSetupStatus = function () {
		$scope.newMarkSetupStatus = {
			ClassSecId: null,
			SubjectId: null,
			DateFrom_TMP: '',
			DateTo_TMP: '',
			Mode: 'Save'
		};
	}
	//************************* CASType *********************************
	$scope.IsValidCASType = function () {
		if ($scope.newCASType.Name.isEmpty()) {
			Swal.fire('Please ! Enter Name');
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
			url: base_url + "Exam/Transaction/SaveCASType",
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
			url: base_url + "Exam/Transaction/GetAllCASTypeForEdit",
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
			url: base_url + "Exam/Transaction/GetCASTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCASType = res.data.Data;
				$scope.newCASType.Mode = 'Modify';

				document.getElementById('cas-type-section').style.display = "none";
				document.getElementById('cas-type-form').style.display = "block";

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
					url: base_url + "Exam/Transaction/DelCASType",
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


	//************************* MarkSetup *********************************

	$scope.ChangeUnder = function (curRow) {
		if (curRow.Under == 7) {
			curRow.Scheme = 2;
		}
	}
	$scope.GetClassWiseExamSubjectList = function () {
		$scope.ExamClassSubjectList = [];
		var para = {
			ClassId: $scope.newMarkSetup.ClassId
		}
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamClassSubject",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamClassSubjectList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	$scope.GetClassWiseSubMap = function () {

		$scope.checkAllECS = false;
		$scope.ExamClassSubjectList = [];
		$scope.newMarkSetup.SubjectList = [];
		$scope.newMarkSetup.MarkSetupDetailsColl = [];

		if ($scope.newMarkSetup.ClassId && $scope.newMarkSetup.ClassId > 0) {
			var para = {
				ClassId: $scope.newMarkSetup.ClassId,
				SectionIdColl: ''
			};

			$scope.GetClassWiseExamSubjectList();

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetSubjectMappingClassWise",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var SubjectMappingColl = res.data.Data;

					if (SubjectMappingColl.length == 0) {
						Swal.fire('Subject Mapping Not Found');
					}
					else if (SubjectMappingColl.length > 0) {
						angular.forEach(SubjectMappingColl, function (sm) {
							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								subDet.PaperType = sm.PaperType;
								$scope.newMarkSetup.SubjectList.push(subDet);
							}
						});
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.GetMarkSetup = function () {

		$scope.newMarkSetup.MarkSetupDetailsColl = [];

		if ($scope.newMarkSetup.ClassId > 0 && $scope.newMarkSetup.SubjectId > 0 && $scope.newMarkSetup.ExamTypeId > 0) {
			var para = {
				ClassId: $scope.newMarkSetup.ClassId,
				SectionId: null,
				SubjectId: $scope.newMarkSetup.SubjectId,
				ExamTypeId: $scope.newMarkSetup.ExamTypeId
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetCASMarkSetupById",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {

					$scope.newMarkSetup.MarkDetailsColl = [];

					angular.forEach(res.data.Data.MarksSetupDetailsColl, function (det) {
						$scope.newMarkSetup.MarkDetailsColl.push({
							CASTypeId: det.CASTypeId,
							Mark: det.Mark,
							FullMark: det.FullMark,
							Under: det.Under,
							Scheme: det.Scheme,
							DateFrom_TMP: (det.DateFrom ? new Date(det.DateFrom) : null),
							DateTo_TMP: (det.DateFrom ? new Date(det.DateTo) : null),
							ExamTypeId: det.ExamTypeId,
							AttendanceFrom: det.AttendanceFrom,
							Formula: det.Formula,
						});
					});

					if ($scope.newMarkSetup.MarkDetailsColl.length == 0) {
						$scope.newMarkSetup.MarkDetailsColl.push({
							Mark: 0,
							Under: 1,
							Scheme: 1,
							Formula: '',
						});
					} else {
						$scope.newMarkSetup.FullMark = $scope.newMarkSetup.MarkDetailsColl[0].FullMark;
					}

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};
	$scope.AddMarkDetails = function (ind) {
		if ($scope.newMarkSetup.MarkDetailsColl) {
			if ($scope.newMarkSetup.MarkDetailsColl.length > ind + 1) {
				$scope.newMarkSetup.MarkDetailsColl.splice(ind + 1, 0, {
					Mark: 0,
					Under: 1,
					Scheme: 1,
					Formula: '',
				})
			} else {
				$scope.newMarkSetup.MarkDetailsColl.push({
					Mark: 0,
					Under: 1,
					Scheme: 1,
					Formula: '',
				})
			}
		}
	};
	$scope.delMarkDetails = function (ind) {
		if ($scope.newMarkSetup.MarkDetailsColl) {
			if ($scope.newMarkSetup.MarkDetailsColl.length > 1) {
				$scope.newMarkSetup.MarkDetailsColl.splice(ind, 1);
			}
		}
	};

	$scope.IsValidMarkSetup = function () {

		if (!$scope.newMarkSetup.ClassId || $scope.newMarkSetup.ClassId == 0) {
			Swal.fire("Please ! Select Class Name");
			return false;
		}

		if (!$scope.newMarkSetup.SubjectId || $scope.newMarkSetup.SubjectId == 0) {
			Swal.fire("Please ! Select Subject Name");
			return false;
		}

		if (!$scope.newMarkSetup.ExamTypeId || $scope.newMarkSetup.ExamTypeId == 0) {
			Swal.fire("Please ! Select ExamType Name");
			return false;
		}


		return true;
	}

	$scope.SaveUpdateMarkSetup = function () {
		if ($scope.IsValidMarkSetup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newMarkSetup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateMarkSetup();
					}
				});
			} else
				$scope.CallSaveUpdateMarkSetup();
		}
	};

	$scope.CallSaveUpdateMarkSetup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.newMarkSetup.MarksSetupDetailsColl = [];
		angular.forEach($scope.newMarkSetup.MarkDetailsColl, function (det) {
			$scope.newMarkSetup.MarksSetupDetailsColl.push({
				CASTypeId: det.CASTypeId,
				Mark: det.Mark,
				Under: det.Under,
				Scheme: det.Scheme,
				DateFrom: (det.DateFromDet ? $filter('date')(new Date(det.DateFromDet.dateAD), 'yyyy-MM-dd') : null),
				DateTo: (det.DateToDet ? $filter('date')(new Date(det.DateToDet.dateAD), 'yyyy-MM-dd') : null),
				ExamTypeId: det.ExamTypeId,
				AttendanceFrom: det.AttendanceFrom,
				Formula: det.Formula,
			});
		});

		var ExamClassSubjectsColl = [];
		//var tmpData = $filter('orderBy')($filter('filter')($scope.ExamClassSubjectList, $scope.searchData.MarkSetup), $scope.sortKey, $scope.reverse);
		var tmpData = $filter('filter')($scope.ExamClassSubjectList, $scope.searchData.MarkSetup);
		angular.forEach(tmpData, function (es) {
			if (es.IsSelected == true) {
				ExamClassSubjectsColl.push(es);
			}
		});
		$scope.newMarkSetup.ExamClassSubjectsColl = ExamClassSubjectsColl;

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveCASMarkSetup",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newMarkSetup }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearMarkSetup();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}

	$scope.CheckAllECS = function () {
		angular.forEach($scope.ExamClassSubjectList, function (es) {
			es.IsSelected = $scope.checkAllECS;
		});
	}

	$scope.GetMarkSetupStatus = function () {

		$scope.newMarkSetupStatus.DataColl = [];

		if ($scope.newMarkSetupStatus.ClassId > 0 && $scope.newMarkSetupStatus.ExamTypeId > 0) {
			var para = {
				ClassId: $scope.newMarkSetupStatus.ClassId,
				ExamTypeId: $scope.newMarkSetupStatus.ExamTypeId
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetCASMarkSetupStatus",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newMarkSetupStatus.DataColl = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	//************************* MarkSetup Status *********************************

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

	$scope.validateDate = function (obj, startField, endField, startLabel, endLabel) {
		var res = GlobalServices.validateDate(obj, startField, endField, startLabel, endLabel);
		if (res.IsSuccess == false) {
			Swal.fire({
				icon: 'warning',
				text: res.Message,
				confirmButtonText: 'OK'
			}).then(function () {
				obj.DateFrom_TMP = new Date();
				obj.DateTo_TMP = new Date();
				$scope.$applyAsync();
			});
		}
	};
});