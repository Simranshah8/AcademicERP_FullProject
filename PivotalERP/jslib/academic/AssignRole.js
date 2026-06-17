app.controller('ClassScheduleController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Class Schedule';

	OnClickDefault();
	//$scope.filSec=$filter('filter')($scope.ClassSection.SectionList, {ClassId:2} );

	$scope.LoadData = function () {

		$('.select2').select2();
		var glbS = GlobalServices;

		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();
		$scope.WeekDayList = glbS.getWeekDayNameList();
		$scope.EmployeeSearchOptions = glbS.getEmployeeSearchOptions();

		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;

			if ($scope.AcademicConfig.ActiveFaculty == true) {

				$scope.FacultyList = [];
				GlobalServices.getFacultyList().then(function (res) {
					$scope.FacultyList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveLevel == true) {

				$scope.LevelList = [];
				GlobalServices.getClassLevelList().then(function (res) {
					$scope.LevelList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveSemester == true) {

				$scope.SelectedClassSemesterList = [];
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveBatch == true) {

				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

			if ($scope.AcademicConfig.ActiveClassYear == true) {

				$scope.ClassYearList = [];
				$scope.SelectedClassClassYearList = [];
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ClassShiftList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassShift",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassShiftList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.currentPages = {			
			AssignClassTeacher: 1,
			AssignHod: 1,
			CreateGroup: 1,
			Coordinator:1
		};

		$scope.searchData = {			
			AssignClassTeacher: '',
			AssignHod: '',
			CreateGroup: '',
			Coordinator:''
		};

		$scope.perPage = {		
			AssignClassTeacher: glbS.getPerPageRow(),
			AssignHod: glbS.getPerPageRow(),
			CreateGroup: glbS.getPerPageRow(),
			Coordinator: glbS.getPerPageRow(),
		};

		

		$scope.newAssignClassTeacher = {
			ClassTeacherId: null,
			ClassId: null,
			SectionId: null,
			Teacher: '',
			Mode: 'Save'
		};

		$scope.newAssignHod = {
			AssignHodId: null,
			DepartmentId: null,
			EmployeeId: null,
			ShiftId: null,
			AssignHodDetailsColl: [],
			EmployeeSearchBy: 'E.Name',
			Mode: 'Save'
		};
		//$scope.newAssignHod.AssignHodDetailsColl.push({});

		$scope.newCreateGroup = {
			AssignClassTeacherId: null,
			GroupName: '',
			ClassSecId: null,
			SubjectId: null,
			Teacher: '',
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			Mode: 'Save'
		};

		$scope.newCoordinator = {
			InchargeId: null,			
			ClassSecId: null,			
			Teacher: '',
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			Mode: 'Save'
		};

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AllSubjectList = [];
		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res) {
			$scope.AllSubjectList = res.data.Data;
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		
		$scope.GetAllAssignClassTeacherList();
		$scope.GetAllAssignHodList();
		$scope.GetAllCreateGroupList();
		$scope.GetAllCoordinatorList();

		$scope.DepartmentList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllDepartmentList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DepartmentList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//Teacher Wise Quota
		$scope.newTeacherWiseQuota = {
			TranId: null,
			EmployeeId: null,
			WeekDay: null,
			NoofPeriod: null,
			TotalPeriod: null,
			Mode: 'Save'
		};


		//$scope.GetAllTeacherWiseQuota();

		$scope.newFilter = {
			DepartmentId: null,
			DesignationId: null,
		};


		//For Designation
		$scope.DesignationList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllDesignationList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DesignationList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.WeekColl = [
			{ id: 1, text: 'Sunday' },
			{ id: 2, text: 'Monday' },
			{ id: 3, text: 'Tuesday' },
			{ id: 4, text: 'Wednesday' },
			{ id: 5, text: 'Thursday' },
			{ id: 6, text: 'Friday' },
			{ id: 7, text: 'Saturday' }
		];

		$scope.WeekendList = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetWeekendList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.WeekendList = res.data.Data;
				angular.forEach($scope.WeekColl, function (day) {
					var matchedWeekend = $scope.WeekendList.find(function (wk) {
						return wk.DayId === day.id;
					});
					day.isShow = matchedWeekend ? false : true; // Set isShow properly
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		});

	}

	function OnClickDefault() {
		document.getElementById('hod-form').style.display = "none";
		document.getElementById('group-form').style.display = "none";
		document.getElementById('class-teacher-form').style.display = "none";
		document.getElementById('Coordinator-form').style.display = "none";
		// // class teacher

		document.getElementById('add-class-teacher').onclick = function () {
			document.getElementById('class-teacher-content').style.display = "none";
			document.getElementById('class-teacher-form').style.display = "block";
			$scope.ClearAssignClassTeacher();
		}

		document.getElementById('back-btn-class-teacher').onclick = function () {
			document.getElementById('class-teacher-content').style.display = "block";
			document.getElementById('class-teacher-form').style.display = "none";
			$scope.ClearAssignClassTeacher();
		}
		// // HOD

		document.getElementById('add-hod').onclick = function () {
			document.getElementById('hod-content').style.display = "none";
			document.getElementById('hod-form').style.display = "block";
			$scope.ClearAssignHod();
		}

		document.getElementById('back-btn-hod').onclick = function () {
			document.getElementById('hod-content').style.display = "block";
			document.getElementById('hod-form').style.display = "none";
			$scope.GetAllAssignHodList();
			$scope.ClearAssignHod();
		}
		// // Group

		document.getElementById('add-group').onclick = function () {
			document.getElementById('group-content').style.display = "none";
			document.getElementById('group-form').style.display = "block";
		}

		document.getElementById('back-btn-group').onclick = function () {
			document.getElementById('group-content').style.display = "block";
			document.getElementById('group-form').style.display = "none";
		}

		document.getElementById('add-Coordinator').onclick = function () {
			document.getElementById('Coordinator-content').style.display = "none";
			document.getElementById('Coordinator-form').style.display = "block";
		}

		document.getElementById('back-btn-Coordinator').onclick = function () {
			document.getElementById('Coordinator-content').style.display = "block";
			document.getElementById('Coordinator-form').style.display = "none";
		}
	}


	$scope.ClearAssignClassTeacher = function () {
		$scope.newAssignClassTeacher = {
			ClassTeacherId: null,
			ClassId: null,
			SectionId: null,
			Teacher: '',
			Mode: 'Save'
		};
	}

	$scope.ClearAssignHod = function () {
		$scope.newAssignHod = {
			AssignHodId: null,
			DepartmentId: null,
			EmployeeId: null,
			ShiftId: null,
			ClassShiftId: null,
			AssignHodDetailsColl: [],
			EmployeeSearchBy: 'E.Name',
			Mode: 'Save'
		};
	}


	


	//************************* Class Schedule *********************************



	$scope.curSC = null;
	$scope.ShowOptionSubject = function (ms, sc) {
		$scope.curSC = sc;
		$('#option-sub').modal('show');
	};
	$scope.AddOptionalSub = function (ind) {
		if ($scope.curSC.AlternetColl) {
			if ($scope.curSC.AlternetColl.length > ind + 1) {
				$scope.curSC.AlternetColl.splice(ind + 1, 0, {
					SubjectId: null
				})
			} else {
				$scope.curSC.AlternetColl.push({
					SubjectId: null
				})
			}
		}
	};
	$scope.delOptionalSub = function (ind) {
		if ($scope.curSC.AlternetColl) {
			if ($scope.curSC.AlternetColl.length > 1) {
				$scope.curSC.AlternetColl.splice(ind, 1);
			}
		}
	}





	//************************* Assign Class Teacher *********************************

	$scope.LoadClassWiseSemesterYear2 = function (classId, data) {

		$scope.SelectedClassClassYearList2 = [];
		$scope.SelectedClassSemesterList2 = [];
		$scope.SelectedClass2 = mx($scope.ClassSection.ClassList).firstOrDefault(p1 => p1.ClassId == classId);

		if ($scope.SelectedClass2) {
			var semQry = mx($scope.SelectedClass2.ClassSemesterIdColl);
			var cyQry = mx($scope.SelectedClass2.ClassYearIdColl);

			angular.forEach($scope.SemesterList, function (sem) {
				if (semQry.contains(sem.id)) {
					$scope.SelectedClassSemesterList2.push({
						id: sem.id,
						text: sem.text,
						SemesterId: sem.id,
						Name: sem.Name
					});
				}
			});

			angular.forEach($scope.ClassYearList, function (sem) {
				if (cyQry.contains(sem.id)) {
					$scope.SelectedClassClassYearList2.push({
						id: sem.id,
						text: sem.text,
						ClassYearId: sem.id,
						Name: sem.Name
					});
				}
			});
		}

	};

	$scope.GetEmpListForClassTeacher = function (fromInd) {



		$scope.newAssignClassTeacher.EmployeeList = [];

		// Load Class Wise Year and Semester On Class Selection Changed
		if (fromInd == 2) {
			$scope.newAssignClassTeacher.SemesterId = null;
			$scope.newAssignClassTeacher.ClassYearId = null;
			$scope.LoadClassWiseSemesterYear2($scope.newAssignClassTeacher.ClassId, $scope.newAssignClassTeacher);
		}

		if ($scope.newAssignClassTeacher.ClassId && $scope.newAssignClassTeacher.ClassId > 0) {

			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newAssignClassTeacher.ClassId,
				SectionId: ($scope.newAssignClassTeacher.SectionId ? $scope.newAssignClassTeacher.SectionId : null),
				SemesterId: $scope.newAssignClassTeacher.SemesterId,
				ClassYearId: $scope.newAssignClassTeacher.ClassYearId,
				BatchId: $scope.newAssignClassTeacher.BatchId,
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetEmpListForClassTeacher",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newAssignClassTeacher.EmployeeList = res.data.Data;

					if ($scope.newAssignClassTeacher.EmployeeList.length == 0) {
						//Swal.fire('Subject Mapping Not Found');
					}
					else if ($scope.newAssignClassTeacher.EmployeeList.length > 0) {

					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.IsValidAssignClassTeacher = function () {
		//if ($scope.newAssignClassTeacher.ClassId.isEmpty()) {
		//	Swal.fire('Please ! Select Class');
		//	return false;
		//}

		return true;
	}

	$scope.SaveUpdateAssignClassTeacher = function () {
		if ($scope.IsValidAssignClassTeacher() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAssignClassTeacher.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAssignClassTeacher();
					}
				});
			} else
				$scope.CallSaveUpdateAssignClassTeacher();

		}
	};

	$scope.CallSaveUpdateAssignClassTeacher = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveClassTeacher",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newAssignClassTeacher }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAssignClassTeacher();
				$scope.GetAllAssignClassTeacherList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllAssignClassTeacherList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AssignClassTeacherList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassTeacherList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AssignClassTeacherList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAssignClassTeacherById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ClassTeacherId: refData.ClassTeacherId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetClassTeacherById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAssignClassTeacher = res.data.Data;
				$scope.newAssignClassTeacher.Mode = 'Modify';

				document.getElementById('class-teacher-content').style.display = "none";
				document.getElementById('class-teacher-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAssignClassTeacherById = function (refData) {

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
					ClassTeacherId: refData.ClassTeacherId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelClassTeacher",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAssignClassTeacherList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	//*********************************AssignHod *****************************************

	$scope.GetAssignHodById = function () {
		//$scope.loadingstatus = "running";
		//showPleaseWait();
		$scope.newAssignHod.ClassList = [];

		if ($scope.newAssignHod.DepartmentId && $scope.newAssignHod.EmployeeId > 0 && $scope.newAssignHod.ClassShiftId && $scope.newAssignHod.ClassShiftId > 0) {

			var para = {
				DepartmentId: $scope.newAssignHod.DepartmentId,
				EmployeeId: $scope.newAssignHod.EmployeeId,
				ClassShiftId: $scope.newAssignHod.ClassShiftId,
				SubjectId: $scope.newAssignHod.SubjectId
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetClassHODByd",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {

				if (res1.data.IsSuccess && res1.data.Data) {
					$scope.newAssignHod.ClassList = res1.data.Data;

				} else {
					Swal.fire(res1.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});


		}

	};

	$scope.IsValidAssignHod = function () {
	
		if (!$scope.newAssignHod.DepartmentId) {
			Swal.fire('Please select Department');
			return false;
		}

		if (!$scope.newAssignHod.EmployeeId) {
			Swal.fire('Please select Teacher');
			return false;
		}

		if (!$scope.newAssignHod.ClassShiftId) {
			Swal.fire('Please select Shift');
			return false;
		}

		if (!$scope.newAssignHod.SubjectId) {
			Swal.fire('Please select Subject');
			return false;
		}
		if (!$scope.newAssignHod.ClassList || $scope.newAssignHod.ClassList.length === 0) {
			Swal.fire('Class list not found');
			return false;
		}

		var anyChecked = $scope.newAssignHod.ClassList.some(function (cls) {
			return cls.IsHod === true;
		});

		if (!anyChecked) {
			Swal.fire('Please Include at least one class to assign HOD');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateAssignHod = function () {
		if ($scope.IsValidAssignHod() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAssignHod.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAssignHod();
					}
				});
			} else
				$scope.CallSaveUpdateAssignHod();

		}
	};

	$scope.CallSaveUpdateAssignHod = function () {

		if ($scope.newAssignHod.DepartmentId && $scope.newAssignHod.EmployeeId > 0 && $scope.newAssignHod.ClassShiftId && $scope.newAssignHod.ClassShiftId > 0) {


			var para = {
				DepartmentId: $scope.newAssignHod.DepartmentId,
				EmployeeId: $scope.newAssignHod.EmployeeId,
				ClassShiftId: $scope.newAssignHod.ClassShiftId,
				SubjectId: $scope.newAssignHod.SubjectId
			};

			angular.forEach($scope.newAssignHod.ClassList, function (cl) {
				cl.DepartmentId = para.DepartmentId;
				cl.EmployeeId = para.EmployeeId;
				//cl.ClassShiftId = para.ClassShiftId;
				cl.ShiftId = para.ClassShiftId;
				cl.SubjectId = para.SubjectId;
				cl.BatchId=cl.BatchId;
				cl.YearId=cl.YearId;
				cl.SemesterId=cl.SemesterId;
			});

			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/SaveClassHOD",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					return formData;
				},
				data: { jsonData: $scope.newAssignHod.ClassList }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				hidePleaseWait();

				Swal.fire(res.data.ResponseMSG);

				if (res.data.IsSuccess == true) {
					$scope.ClearAssignHod();
				}

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});

		}

	}

	$scope.GetAllAssignHodList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AssignHodList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassHOD",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AssignHodList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	$scope.DelAssignHodById = function (refData) {

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
					DepartmentId: refData.DepartmentId,
					EmployeeId: refData.EmployeeId,
					ClassShiftId: refData.ClassShiftId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelClassHOD",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAssignHodList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//*********************************CreateGroup *****************************************
	$scope.ClearCreateGroup = function () {
		$scope.newCreateGroup = {
			AssignClassTeacherId: null,
			GroupName: '',
			ClassSecId: null,
			SubjectId: null,
			Teacher: '',
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			Mode: 'Save'
		};
	};
	$scope.IsValidCreateGroup = function () {
		if ($scope.newCreateGroup.Name.isEmpty()) {
			Swal.fire('Please ! Enter Group Name');
			return false;
		}

		if (!$scope.newCreateGroup.SubjectId || $scope.newCreateGroup.SubjectId == 0) {
			Swal.fire('Please ! Select Subject Name');
			return false;
		}

		if (!$scope.newCreateGroup.EmployeeId || $scope.newCreateGroup.EmployeeId == 0) {
			Swal.fire('Please ! Select Employee Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateCreateGroup = function () {
		if ($scope.IsValidCreateGroup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCreateGroup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCreateGroup();
					}
				});
			} else
				$scope.CallSaveUpdateCreateGroup();

		}
	};

	$scope.CallSaveUpdateCreateGroup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newCreateGroup.DetailsColl = [];
		if ($scope.newCreateGroup.SelectedClass) {
			angular.forEach($scope.newCreateGroup.SelectedClass, function (cs) {
				$scope.newCreateGroup.DetailsColl.push(
					{
						ClassId: cs.ClassId,
						SectionId: cs.SectionId
					});
			});
		}

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveClassGroup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newCreateGroup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearCreateGroup();
				$scope.GetAllCreateGroupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllCreateGroupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CreateGroupList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassGroupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CreateGroupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetCreateGroupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ClassGroupId: refData.ClassGroupId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetClassGroupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCreateGroup = res.data.Data;
				$scope.newCreateGroup.Mode = 'Modify';

				document.getElementById('group-content').style.display = "none";
				document.getElementById('group-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelCreateGroupById = function (refData) {

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
					ClassGroupId: refData.ClassGroupId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelClassGroup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCreateGroupList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//*********************************Coordinator *****************************************
	$scope.ClearCoordinator = function () {
		$scope.newCoordinator = {
			AssignClassTeacherId: null,
			GroupName: '',
			ClassSecId: null,
			SubjectId: null,
			Teacher: '',
			SelectEmployee: $scope.EmployeeSearchOptions[0].value,
			Mode: 'Save'
		};
	};


	$scope.IsValidCoordinator = function () {		
		return true;
	}

	$scope.SaveUpdateCoordinator = function () {
		if ($scope.IsValidCoordinator() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newCoordinator.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					if (result.isConfirmed) {
						$scope.CallSaveUpdateCoordinator();
					}
				});
			} else
				$scope.CallSaveUpdateCoordinator();
		}
	};

	$scope.CallSaveUpdateCoordinator = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var employeeId = $scope.newCoordinator.EmployeeId;
		var dataToSave = [];
		// Loop through your actual ClassSection.SectionList
		angular.forEach($scope.newCoordinator.ClassList, function (S) {
			
				var dataItem = {
					EmployeeId: employeeId,
					ClassId: S.ClassId,
					SectionId: S.SectionId || null,
					BatchId: S.BatchId || null,
					ClassYearId: S.ClassYearId || null,
					SemesterId: S.SemesterId || null,
					IsInclude: S.IsInclude
				};
				dataToSave.push(dataItem);
			
		});

		if (dataToSave.length === 0) {
			hidePleaseWait();
			Swal.fire("Please select at least one Class/Section.");
			$scope.loadingstatus = "stop";
			return;
		}

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveUpdateCoordinator",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: dataToSave }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess === true) {
				$scope.ClearCoordinator();
				$scope.GetAllCoordinatorList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire("Error saving data.");
		});
	};


	$scope.GetCoordinatorClassById = function () {
		$scope.newCoordinator.ClassList = [];
		if ($scope.newCoordinator.EmployeeId > 0) {
			var para = {				
				EmployeeId: $scope.newCoordinator.EmployeeId,			
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetCoordinatorClass",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {

				if (res1.data.IsSuccess && res1.data.Data) {
					$scope.newCoordinator.ClassList = res1.data.Data;

				} else {
					Swal.fire(res1.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});


		}

	};

	
	$scope.GetAllCoordinatorList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CoordinatorList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassCoordinator",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CoordinatorList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.DelCoordinatorById = function (refData) {

		Swal.fire({
			title: 'Are you sure you want to delete ' + refData.Teacher + ' ?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					EmployeeId: refData.EmployeeId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelClassCoordinator",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllCoordinatorList();
						Swal.fire(res.data.ResponseMSG);
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