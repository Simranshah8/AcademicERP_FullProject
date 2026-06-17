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
		$scope.GetAllTeacherQuota();
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

		$scope.currentPages = {
			ClassShift: 1,
			PeriodManagement: 1,
			ManageSchedule: 1,
			AssignClassTeacher: 1,
			AssignHod: 1,
			CreateGroup:1,
			TeacherWiseQuota:1
		};

		$scope.searchData = {
			ClassShift: '',
			PeriodManagement: '',
			ManageSchedule: '',
			AssignClassTeacher: '',
			AssignHod: '',
			CreateGroup:'',
			TeacherWiseQuota:''
		};

		$scope.perPage = {
			ClassShift: glbS.getPerPageRow(),
			PeriodManagement: glbS.getPerPageRow(),
			ManageSchedule: glbS.getPerPageRow(),
			PhysicalSchedule: glbS.getPerPageRow(),
			AssignClassTeacher: glbS.getPerPageRow(),
			AssignHod: glbS.getPerPageRow(),
			CreateGroup: glbS.getPerPageRow(),
			TeacherWiseQuota: glbS.getPerPageRow(),
		};

		$scope.newClassShift = {
			ClassShiftId: null,
			Name: '',
			WeeklyDayOff: 7,
			SecondWeeklyDayOff: 1,
			StartTime: null,
			EndTime: null,
			NoofBreak: 1,
			ForOnlineClass: false,
			IsActive:true,
			Mode: 'Save'
		};

		$scope.newPeriodManagement = {
			PeriodManagementId: null,
			Shift: '',
			NoOfPeriod: 0,
			B1BreakAfterPeriod: 0,
			B1TimeDuration: 0,
			B2BreakAfterPeriod: 0,
			B2TimeDuration: 0,
			B3BreakAfterPeriod: 0,
			B3TimeDuration: 0,
			B4BreakAfterPeriod: 0,
			B4TimeDuration: 0,
			PeriodManagementDetailsColl:[],
			Mode: 'Save'
		};

		$scope.newManageSchedule = {
			ManageScheduleId: null,
			ManageScheduleDetailsColl: []
		};
		$scope.newManageSchedule.ManageScheduleDetailsColl.push({});

		$scope.newPhysicalSchedule = {
			PhysicalScheduleId: null,
			PhysicalScheduleDetailsColl: []
		};
		$scope.newPhysicalSchedule.PhysicalScheduleDetailsColl.push({});

		$scope.newAssignClassTeacher = {
			ClassTeacherId: null,
			ClassId: null,
			SectionId: null,
			Teacher:'',
			Mode: 'Save'
		};

		$scope.newAssignHod = {
			AssignHodId: null,
			DepartmentId: null,
			EmployeeId:null,
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


		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AllSubjectList = [];
		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res)
		{
			$scope.AllSubjectList = res.data.Data;
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllClassShift();
	/*	$scope.GetAllPeriodManagement();	*/	
		$scope.GetAllAssignClassTeacherList();
		//$scope.GetAllAssignHodList();
		//$scope.GetAllCreateGroupList();

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

		$scope.newTeacherWiseQuota = {
			TranId: null,
			EmployeeId: null,
			WeekColl: [],
			Mode: 'Save'
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


		document.getElementById('class-shift-form').style.display = "none";
		document.getElementById('period-management-form').style.display = "none";
		document.getElementById('class-teacher-form').style.display = "none";
		document.getElementById('hod-form').style.display = "none";
		document.getElementById('group-form').style.display = "none";

		document.getElementById('quota-form').style.display = "none";

		document.getElementById('add-class-shift').onclick = function () {
			document.getElementById('class-shift-content').style.display = "none";
			document.getElementById('class-shift-form').style.display = "block";

			$scope.ClearClassShift();
		}

		document.getElementById('back-btn-class-shift').onclick = function () {
			document.getElementById('class-shift-content').style.display = "block";
			document.getElementById('class-shift-form').style.display = "none";
			$scope.ClearClassShift();
		}

		// period Management

		document.getElementById('add-period').onclick = function () {
			document.getElementById('period-management-content').style.display = "none";
			document.getElementById('period-management-form').style.display = "block";
			$scope.ClearPeriodManagement();
		}

		document.getElementById('back-btn-period').onclick = function () {
			document.getElementById('period-management-content').style.display = "block";
			document.getElementById('period-management-form').style.display = "none";
			$scope.ClearPeriodManagement();
		}

		//Teacher Wise Quota section
		document.getElementById('add-quota').onclick = function () {
			document.getElementById('quota-section').style.display = "none";
			document.getElementById('quota-form').style.display = "block";
			$scope.ClearTeacherWiseQuota();
			$scope.GetTeacherWiseQuota();
		}

		document.getElementById('back-to-list-quota').onclick = function () {
			document.getElementById('quota-form').style.display = "none";
			document.getElementById('quota-section').style.display = "block";
			$scope.ClearTeacherWiseQuota();
		}

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

		
	}

	$scope.getFilteredWeekDays = function (type) {
		return $scope.WeekDayList.filter(function (w) {
			if (type === 'fst') {
				return w.id !== $scope.newClassShift.SecondWeeklyDayOff;
			}
			if (type === 'sec') {
				return w.id !== $scope.newClassShift.WeeklyDayOff;
			}
			return true;
		});
	};

	$scope.ClearClassShift = function () {
		$timeout(function () {
			$scope.newClassShift = {
				ClassShiftId: null,
				Name: '',
				WeeklyDayOff: 7,
				SecondWeeklyDayOff: 1,
				StartTime: null,
				EndTime: null,
				NoofBreak: 1,
				ForOnlineClass: false,
				IsActive: true,
				Mode: 'Save'
			};
		});
		
	}
	$scope.ClearPeriodManagement = function () {
		$timeout(function () {
			$scope.newPeriodManagement = {
				PeriodManagementId: null,
				Shift: '',
				NoOfPeriod: 0,
				B1BreakAfterPeriod: 0,
				B1TimeDuration: 0,
				B2BreakAfterPeriod: 0,
				B2TimeDuration: 0,
				B3BreakAfterPeriod: 0,
				B3TimeDuration: 0,
				B4BreakAfterPeriod: 0,
				B4TimeDuration: 0,
				PeriodManagementDetailsColl: [],
				Mode: 'Save'
			};
		});		
	}
	$scope.ClearManageSchedule = function () {
		$scope.newManageSchedule = {
			ManageScheduleId: null,
			ManageScheduleDetailsColl:[]
		};
		$scope.newManageSchedule.ManageScheduleDetailsColl.push({});
	}
	$scope.ClearPhysicalSchedule = function () {
		$scope.newPhysicalSchedule = {
			PhysicalScheduleId: null,
			PhysicalScheduleDetailsColl: []
		};
		$scope.newPhysicalSchedule.PhysicalScheduleDetailsColl.push({});
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
			ClassShiftId:null,
			AssignHodDetailsColl: [],
			EmployeeSearchBy: 'E.Name',
			Mode: 'Save'
		};
	}
	
	$scope.ClearTeacherWiseQuota = function () {
		$scope.newTeacherWiseQuota = {
			TranId: null,
			EmployeeId: null,
			WeekColl: [],
			Mode: 'Save'
		};
	}



	//************************* Class Shift *********************************

	$scope.IsValidClassShift = function () {
		if ($scope.newClassShift.Name.isEmpty()) {
			Swal.fire('Please ! Enter ClassShift Name');
			return false;
		}

		var startTime = $scope.newClassShift.StartTime_TMP;
		var endTime = $scope.newClassShift.EndTime_TMP;

		if (!startTime) {
			Swal.fire('Please ! Select Start Time');
			return false;
		}

		if (!endTime) {
			Swal.fire('Please ! Select End Time');
			return false;
		}

		// Convert to minutes safely
		function timeToMinutes(t) {
			if (typeof t === 'string') {
				var parts = t.split(':');
				return parseInt(parts[0]) * 60 + parseInt(parts[1]);
			} else if (t instanceof Date) {
				return t.getHours() * 60 + t.getMinutes();
			} else {
				return NaN;
			}
		}

		var startMinutes = timeToMinutes(startTime);
		var endMinutes = timeToMinutes(endTime);

		if (isNaN(startMinutes) || isNaN(endMinutes)) {
			Swal.fire('Invalid Start or End Time');
			return false;
		}

		if (startMinutes >= endMinutes) {
			Swal.fire('Start Time must be before End Time');
			return false;
		}

		var breaks = $scope.newClassShift.NoofBreak;

		if (breaks == null || breaks === '') {
			Swal.fire('Please ! Enter Valid Number of Breaks');
			return false;
		}

		// Convert to integer
		breaks = parseInt(breaks);

		if (isNaN(breaks)) {
			Swal.fire('Number of Breaks must be a valid number');
			return false;
		}

		if (breaks < 0) {
			Swal.fire('Number of Breaks cannot be negative');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateClassShift = function () {
		if ($scope.IsValidClassShift() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newClassShift.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateClassShift();
					}
				});
			} else
				$scope.CallSaveUpdateClassShift();

		}
	};

	$scope.CallSaveUpdateClassShift = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newClassShift.StartTime_TMP)
			$scope.newClassShift.StartTime = $scope.newClassShift.StartTime_TMP.toLocaleString();
		else
			$scope.newClassShift.StartTime = null;

		if ($scope.newClassShift.EndTime_TMP)
			$scope.newClassShift.EndTime = $scope.newClassShift.EndTime_TMP.toLocaleString();
		else
			$scope.newClassShift.EndTime = null;

		if ($scope.newClassShift.AbsentNoticeTime_TMP)
			$scope.newClassShift.AbsentNoticeTime = $scope.newClassShift.AbsentNoticeTime_TMP.toLocaleString();
		else
			$scope.newClassShift.AbsentNoticeTime = null;


		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveClassShift",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newClassShift }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearClassShift();
				$scope.GetAllClassShift();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllClassShift = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
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

	}

	$scope.GetClassShiftById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ClassShiftId: refData.ClassShiftId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetClassShiftById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newClassShift = res.data.Data;
				$scope.newClassShift.Mode = 'Modify';

				if ($scope.newClassShift.StartTime)
					$scope.newClassShift.StartTime_TMP = new Date($scope.newClassShift.StartTime);

				if ($scope.newClassShift.EndTime)
					$scope.newClassShift.EndTime_TMP = new Date($scope.newClassShift.EndTime);

				if ($scope.newClassShift.AbsentNoticeTime)
					$scope.newClassShift.AbsentNoticeTime_TMP = new Date($scope.newClassShift.AbsentNoticeTime);

				document.getElementById('class-shift-content').style.display = "none";
				document.getElementById('class-shift-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelClassShiftById = function (refData) {

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
					ClassShiftId: refData.ClassShiftId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelClassShift",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllClassShift();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Class Schedule *********************************

	$scope.LoadClassWiseSemesterYear = function (classId, data) {

		$scope.SelectedClassClassYearList = [];
		$scope.SelectedClassSemesterList = [];
		$scope.SelectedClass = mx($scope.ClassSection.ClassList).firstOrDefault(p1 => p1.ClassId == classId);

		if ($scope.SelectedClass) {
			var semQry = mx($scope.SelectedClass.ClassSemesterIdColl);
			var cyQry = mx($scope.SelectedClass.ClassYearIdColl);

			angular.forEach($scope.SemesterList, function (sem) {
				if (semQry.contains(sem.id)) {
					$scope.SelectedClassSemesterList.push({
						id: sem.id,
						text: sem.text,
						SemesterId: sem.id,
						Name: sem.Name
					});
				}
			});

			angular.forEach($scope.ClassYearList, function (sem) {
				if (cyQry.contains(sem.id)) {
					$scope.SelectedClassClassYearList.push({
						id: sem.id,
						text: sem.text,
						ClassYearId: sem.id,
						Name: sem.Name
					});
				}
			});
		}

	};

	$scope.GetClassWiseSubMap = function (fromInd) {

		
	/*	$scope.GetAllTeacherQuota();*/

		$scope.newManageSchedule.SubjectMappingColl = [];		
		$scope.newManageSchedule.SubjectList = [];
		$scope.newManageSchedule.CurClassScheduleColl = [];

		if ($scope.newManageSchedule.ClassId && $scope.newManageSchedule.ClassId > 0) {

			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				ClassId: $scope.newManageSchedule.ClassId,
				SectionIdColl: ($scope.newManageSchedule.SectionId ? $scope.newManageSchedule.SectionId : ''),
				SemesterId: $scope.newManageSchedule.SemesterId,
				ClassYearId: $scope.newManageSchedule.ClassYearId,
				BatchId: $scope.newManageSchedule.BatchId
			};

			if (fromInd == 2) {
				$scope.newManageSchedule.SemesterId = null;
				$scope.newManageSchedule.ClassYearId = null;
				$scope.LoadClassWiseSemesterYear($scope.newManageSchedule.ClassId, $scope.newManageSchedule);
			}

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetSubjectMappingClassWise",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data)
				{
					$scope.newManageSchedule.SubjectMappingColl = res.data.Data;

					if ($scope.newManageSchedule.SubjectMappingColl.length == 0)
					{
						//Swal.fire('Subject Mapping Not Found');
					}
					else if ($scope.newManageSchedule.SubjectMappingColl.length > 0)
					{
						angular.forEach($scope.newManageSchedule.SubjectMappingColl, function (sm) {
							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								$scope.newManageSchedule.SubjectList.push(subDet);
							}
						});

						$scope.GetClassSchedule();
                    }					
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.GetClassSchedule = function () {

		//$scope.loadingstatus = "running";
		//showPleaseWait();
		

		if ($scope.newManageSchedule.ClassId && $scope.newManageSchedule.ClassId > 0 && $scope.newManageSchedule.ClassShiftId && $scope.newManageSchedule.ClassShiftId>0) {

			var weekOff = mx($scope.ClassShiftList).firstOrDefault(p1 => p1.ClassShiftId == $scope.newManageSchedule.ClassShiftId);
			var weekOffId = 0;
			var secWeekOffId = 0;

			if (weekOff) {
				weekOffId = weekOff.WeeklyDayOff;
				secWeekOffId = weekOff.SecondWeeklyDayOff;
			}

			var para = {
				ClassId: $scope.newManageSchedule.ClassId,
				SectionId: ($scope.newManageSchedule.SectionId ? $scope.newManageSchedule.SectionId : null),
				ClassShiftId: $scope.newManageSchedule.ClassShiftId,
				SemesterId: $scope.newManageSchedule.SemesterId,
				ClassYearId: $scope.newManageSchedule.ClassYearId,
				BatchId: $scope.newManageSchedule.BatchId
			};

			var para1 = {
				ClassShiftId: $scope.newManageSchedule.ClassShiftId
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetPeriodManagementByClassShiftId",
				dataType: "json",
				data: JSON.stringify(para1)
			}).then(function (res1) {
				
				if (res1.data.IsSuccess && res1.data.Data)
				{
					var periodDet = res1.data.Data;

					if (periodDet.TranId && periodDet.TranId > 0)
					{
						var periodCOll = [];

						angular.forEach(periodDet.PeriodManagementDetailsColl, function (pd) {
							periodCOll.push(pd);
						});

						$scope.newManageSchedule.PeriodColl = periodCOll;

						$http({
							method: 'POST',
							url: base_url + "Academic/Creation/GetClassScheduleByClassId",
							dataSchedule: "json",
							data: JSON.stringify(para)
						}).then(function (res) {

							if (res.data.IsSuccess && res.data.Data)
							{
								var ClassScheduleColl = mx(res.data.Data);

								var tmpDataColl = [];
								var first = true;
								angular.forEach($scope.WeekDayList, function (wd) {

									if (wd.id != weekOffId && wd.id != secWeekOffId) {
										var dayDet = {
											DayId: wd.id,
											DayName: wd.text,
											ForAllDay: false,
											ShowCheckedBox:first,
											ScheduleColl: []
										};

										angular.forEach(periodCOll, function (pp) {
											var findData = ClassScheduleColl.firstOrDefault(p1 => p1.DayId == wd.id && p1.Period == pp.Period);

											var sc = {
												DayId: wd.id,
												DayName:wd.text,
												ClassId: para.ClassId,
												SectionId: para.SectionId,
												ClassShiftId: para.ClassShiftId,
												SemesterId: para.SemesterId,
												ClassYearId: para.ClassYearId,
												BatchId:para.BatchId,
												Period: pp.Period,
												SubjectId: (findData ? findData.SubjectId : null),
												EmployeeId: (findData ? findData.EmployeeId : null),
												EmployeeSearchBy: 'E.Name',
												AlternetColl:[]
											};

											if (findData && findData.AlternetColl && findData.AlternetColl.length>0) {
												sc.AlternetColl = findData.AlternetColl;
                                            }else
												sc.AlternetColl.push({});

											dayDet.ScheduleColl.push(sc);
										});

										tmpDataColl.push(dayDet);
										first = false;
                                    }
								});

								$scope.newManageSchedule.CurClassScheduleColl = tmpDataColl;

							} else {
								Swal.fire(res.data.ResponseMSG);
							}

						}, function (reason) {
							Swal.fire('Failed' + reason);
						});

					} else {
						Swal.fire('Period Mapping Not Found');
                    }
					
					
				} else {
					Swal.fire(res1.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

			
		}

	};

	$scope.curSC = null;
	$scope.ShowOptionSubject = function (ms,sc)
	{
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
	//$scope.ChangeForAllDay = function (curS)
	//{
	//	if (curS.ForAllDay == true) {
	//		var firstData = mx(curS.ScheduleColl);

	//		if (firstData) {
				
	//			angular.forEach($scope.newManageSchedule.CurClassScheduleColl, function (sc) {
	//				if (curS.DayId<sc.DayId) {
	//					angular.forEach(sc.ScheduleColl, function (s) {
	//						var ex = firstData.firstOrDefault(p1 => p1.Period == s.Period);
	//						if (ex) {
	//							s.SubjectId = ex.SubjectId;
	//							s.EmployeeId = ex.EmployeeId;
	//							s.AlternetColl = ex.AlternetColl;
	//						}
	//					});
	//				}					
	//			});
 //           }
			
 //       }		
	//};
	$scope.SaveOtherSectionSchedule = function (sid,dataColl) {
		var tmpDataColl = [];
		$scope.loadingstatus = "running";
		angular.forEach(dataColl, function (s) {
			s.SectionId = sid;
			tmpDataColl.push(s);
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveClassSchedule",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: tmpDataColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});

	};
	$scope.CallSaveUpdateClassSchedule = function ()
	{
		var tmpDataColl = [];
		var hasValidData = false;

		angular.forEach($scope.newManageSchedule.CurClassScheduleColl, function (sc) {
			angular.forEach(sc.ScheduleColl, function (s) {

				if (!s.IsBreak) {
					// Check if at least one meaningful data exists
					if ((s.SubjectId && s.SubjectId > 0) && (s.EmployeeId && s.EmployeeId > 0)) {
						hasValidData = true;
					}
				}

				tmpDataColl.push(s);
			});
		});

		// 🚫 रोक्ने condition
		if (!hasValidData) {
			Swal.fire({
				icon: 'warning',
				title: 'Please add at least one subject and teacher before saving!'
				
			});
			return;
		}

		$scope.loadingstatus = "running";
		showPleaseWait();

		var tmpDataColl = [];

		angular.forEach($scope.newManageSchedule.CurClassScheduleColl, function (sc) {
			angular.forEach(sc.ScheduleColl, function (s) {
				tmpDataColl.push(s);
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveClassSchedule",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: tmpDataColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			var otherSectionIdColl = $scope.newManageSchedule.OtherSectionId;
			if (otherSectionIdColl && otherSectionIdColl.length>0) {
				angular.forEach(otherSectionIdColl, function (sid) {
					$timeout(function () {
						$scope.SaveOtherSectionSchedule(sid, tmpDataColl);
					});
				})
            }
			$timeout(function () {
				Swal.fire(res.data.ResponseMSG);

				if (res.data.IsSuccess == true) {

					$scope.ClearClassShift();
					$scope.GetAllClassShift();
				}
			});
			
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.DelClassSchedule = function () {

		if ($scope.newManageSchedule.ClassShiftId && $scope.newManageSchedule.ClassId) {
			Swal.fire({
				title: 'This action will delete all schedules of the selected class and shift. Do you want to continue?',
				showCancelButton: true,
				confirmButtonText: 'Yes,Delete',
			}).then((result) => {
				/* Read more about isConfirmed, isDenied below */
				if (result.isConfirmed) {
					$scope.loadingstatus = "running";
					showPleaseWait();

					var para = {
						ClassId:$scope.newManageSchedule.ClassId,
						ClassShiftId: $scope.newManageSchedule.ClassShiftId,
						SemesterId: $scope.newManageSchedule.SemesterId,
						ClassYearId: $scope.newManageSchedule.ClassYearId,
						BatchId: $scope.newManageSchedule.BatchId,
					};

					$http({
						method: 'POST',
						url: base_url + "Academic/Creation/DelClassSchedule",
						dataType: "json",
						data: JSON.stringify(para)
					}).then(function (res) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						Swal.fire(res.data.ResponseMSG);
						//Added
						$scope.GetClassWiseSubMap(6);
						//Ends
					}, function (reason) {
						Swal.fire('Failed' + reason);
					});
				}
			});
        }
		
	};


	//************************* Physical Class Schedule *********************************

	$scope.GetPhClassWiseSubMap = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newPhysicalSchedule.SubjectMappingColl = [];
		$scope.newPhysicalSchedule.SubjectList = [];
		$scope.newPhysicalSchedule.CurClassScheduleColl = [];

		if ($scope.newPhysicalSchedule.ClassId && $scope.newPhysicalSchedule.ClassId > 0) {

			var para = {
				ClassId: $scope.newPhysicalSchedule.ClassId,
				SectionIdColl: ($scope.newPhysicalSchedule.SectionId ? $scope.newPhysicalSchedule.SectionId : '')
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetSubjectMappingClassWise",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newPhysicalSchedule.SubjectMappingColl = res.data.Data;

					if ($scope.newPhysicalSchedule.SubjectMappingColl.length == 0) {
						//Swal.fire('Subject Mapping Not Found');
					}
					else if ($scope.newPhysicalSchedule.SubjectMappingColl.length > 0) {
						angular.forEach($scope.newPhysicalSchedule.SubjectMappingColl, function (sm) {
							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								$scope.newPhysicalSchedule.SubjectList.push(subDet);
							}
						});

						$scope.GetPhClassSchedule();
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.GetPhClassSchedule = function () {

		//$scope.loadingstatus = "running";
		//showPleaseWait();


		if ($scope.newPhysicalSchedule.ClassId && $scope.newPhysicalSchedule.ClassId > 0 && $scope.newPhysicalSchedule.ClassShiftId && $scope.newPhysicalSchedule.ClassShiftId > 0) {

			var para = {
				ClassId: $scope.newPhysicalSchedule.ClassId,
				SectionId: ($scope.newPhysicalSchedule.SectionId ? $scope.newPhysicalSchedule.SectionId : null),
				ClassShiftId: $scope.newPhysicalSchedule.ClassShiftId
			};

			var para1 = {
				ClassShiftId: $scope.newPhysicalSchedule.ClassShiftId
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetPeriodManagementByClassShiftId",
				dataType: "json",
				data: JSON.stringify(para1)
			}).then(function (res1) {

				if (res1.data.IsSuccess && res1.data.Data) {
					var periodDet = res1.data.Data;

					if (periodDet.TranId && periodDet.TranId > 0) {
						var periodCOll = [];

						angular.forEach(periodDet.PeriodManagementDetailsColl, function (pd) {
							periodCOll.push(pd);
						});

						$scope.newPhysicalSchedule.PeriodColl = periodCOll;

						$http({
							method: 'POST',
							url: base_url + "Academic/Creation/GetPhysicalClassScheduleByClassId",
							dataSchedule: "json",
							data: JSON.stringify(para)
						}).then(function (res) {

							if (res.data.IsSuccess && res.data.Data) {
								var ClassScheduleColl = mx(res.data.Data);

								var tmpDataColl = [];
								angular.forEach($scope.WeekDayList, function (wd) {
									var dayDet = {
										DayId: wd.id,
										DayName: wd.text,
										ScheduleColl: []
									};

									angular.forEach(periodCOll, function (pp) {
										var findData = ClassScheduleColl.firstOrDefault(p1 => p1.DayId == wd.id && p1.Period == pp.Period);

										dayDet.ScheduleColl.push({
											DayId: wd.id,
											ClassId: para.ClassId,
											SectionId: para.SectionId,
											ClassShiftId: para.ClassShiftId,
											Period: pp.Period,
											SubjectId: (findData ? findData.SubjectId : null),
											EmployeeId: (findData ? findData.EmployeeId : null),
											EmployeeSearchBy: 'E.Name'
										});
									});

									tmpDataColl.push(dayDet);
								});

								$scope.newPhysicalSchedule.CurClassScheduleColl = tmpDataColl;

							} else {
								Swal.fire(res.data.ResponseMSG);
							}

						}, function (reason) {
							Swal.fire('Failed' + reason);
						});

					} else {
						Swal.fire('Period Mapping Not Found');
					}


				} else {
					Swal.fire(res1.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});


		}

	};
	$scope.CallSaveUpdatePhClassSchedule = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var tmpDataColl = [];

		angular.forEach($scope.newPhysicalSchedule.CurClassScheduleColl, function (sc) {
			angular.forEach(sc.ScheduleColl, function (s) {
				tmpDataColl.push(s);
			});
		});


		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SavePhysicalClassSchedule",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: tmpDataColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearPhysicalClassShift();
				
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	//************************* Period Management *********************************

	$scope.changeStartEndTime = function (pm)
	{
		pm.TimeDuration = moment(pm.EndTime_TMP).diff(moment(pm.StartTime_TMP), "minutes")
		//console.log(pm);
	};
	$scope.AddPeriod = function () {
		if ($scope.newPeriodManagement.ClassShiftId && $scope.newPeriodManagement.NoOfPeriod && $scope.newPeriodManagement.ClassShiftId>0)
		{
			var selectShift = mx($scope.ClassShiftList).firstOrDefault(p1 => p1.ClassShiftId == $scope.newPeriodManagement.ClassShiftId);

			if (selectShift)
			{
				$scope.newPeriodManagement.NoofBreak = selectShift.NoofBreak;
				$scope.changeNoOfBreak(selectShift.NoofBreak);

				$scope.newPeriodManagement.PeriodManagementDetailsColl = [];

				var startTime = new Date(selectShift.StartTime);				
				var duration = $scope.newPeriodManagement.EachPeriodDuration;
				var endTime = moment(selectShift.StartTime).add(duration, 'm').toDate();
				for (var p = 1; p <= $scope.newPeriodManagement.NoOfPeriod; p++) {
					$scope.newPeriodManagement.PeriodManagementDetailsColl.push({
						Period: p,
						StartTime_TMP: startTime,
						EndTime_TMP: endTime,
						TimeDuration: duration
					});

					startTime = endTime;
					endTime = moment(startTime).add(duration, 'm').toDate();
				}

				$scope.ChangePeriodMinutes();
            }			
        }
	};

	$scope.ChangePeriodMinutes = function () {
		$scope.newPeriodManagement.NoofBreak = 0;
		if ($scope.newPeriodManagement.ClassShiftId && $scope.newPeriodManagement.NoOfPeriod && $scope.newPeriodManagement.ClassShiftId > 0) {
			var selectShift = mx($scope.ClassShiftList).firstOrDefault(p1 => p1.ClassShiftId == $scope.newPeriodManagement.ClassShiftId);
			
			if (selectShift)
			{
				$scope.newPeriodManagement.NoofBreak = selectShift.NoofBreak;
				$scope.changeNoOfBreak(selectShift.NoofBreak);
				var startTime = new Date(selectShift.StartTime);
				var duration = $scope.newPeriodManagement.EachPeriodDuration;
				var endTime = moment(selectShift.StartTime).add(duration, 'm').toDate();

				angular.forEach($scope.newPeriodManagement.PeriodManagementDetailsColl, function (det) {
					det.StartTime_TMP = startTime;
					det.EndTime_TMP = endTime;

					startTime = endTime;

					if (det.Period == $scope.newPeriodManagement.B1BreakAfterPeriod) {
						startTime = moment(startTime).add($scope.newPeriodManagement.B1TimeDuration, 'm').toDate();
					}

					if (det.Period == $scope.newPeriodManagement.B2BreakAfterPeriod) {
						startTime = moment(startTime).add($scope.newPeriodManagement.B2TimeDuration, 'm').toDate();
					}

					if (det.Period == $scope.newPeriodManagement.B3BreakAfterPeriod) {
						startTime = moment(startTime).add($scope.newPeriodManagement.B3TimeDuration, 'm').toDate();
					}

					if (det.Period == $scope.newPeriodManagement.B4BreakAfterPeriod) {
						startTime = moment(startTime).add($scope.newPeriodManagement.B4TimeDuration, 'm').toDate();
					}

					endTime = moment(startTime).add(duration, 'm').toDate();
				});				
			}
		}
	};

	$scope.changeNoOfBreak = function (noofbreak) {
		if (!noofbreak || noofbreak == 0) {

			if ($scope.newPeriodManagement.ClassShiftId)
			{
				var selectShift = mx($scope.ClassShiftList).firstOrDefault(p1 => p1.ClassShiftId == $scope.newPeriodManagement.ClassShiftId);
				if (selectShift) {
					$scope.newPeriodManagement.NoofBreak = selectShift.NoofBreak;
				}
				noofbreak = $scope.newPeriodManagement.NoofBreak;

			}else
				noofbreak = $scope.newPeriodManagement.NoofBreak;
        }
			

		if (!noofbreak || noofbreak < 1) {
			$scope.newPeriodManagement.B1BreakAfterPeriod = 0;
			$scope.newPeriodManagement.B1TimeDuration = 0;

			$scope.newPeriodManagement.B2BreakAfterPeriod = 0;
			$scope.newPeriodManagement.B2TimeDuration = 0;

			$scope.newPeriodManagement.B3BreakAfterPeriod = 0;
			$scope.newPeriodManagement.B3TimeDuration = 0;

			$scope.newPeriodManagement.B4BreakAfterPeriod = 0;
			$scope.newPeriodManagement.B4TimeDuration = 0;
		}else	if (noofbreak == 1) {

			$scope.newPeriodManagement.B2BreakAfterPeriod = 0;
			$scope.newPeriodManagement.B2TimeDuration = 0;

			$scope.newPeriodManagement.B3BreakAfterPeriod = 0;
			$scope.newPeriodManagement.B3TimeDuration = 0;

			$scope.newPeriodManagement.B4BreakAfterPeriod = 0;
			$scope.newPeriodManagement.B4TimeDuration = 0;
		} else if (noofbreak == 2) {
			$scope.newPeriodManagement.B3BreakAfterPeriod = 0;
			$scope.newPeriodManagement.B3TimeDuration = 0;

			$scope.newPeriodManagement.B4BreakAfterPeriod = 0;
			$scope.newPeriodManagement.B4TimeDuration = 0;
		} else if (noofbreak == 3) {

			$scope.newPeriodManagement.B4BreakAfterPeriod = 0;
			$scope.newPeriodManagement.B4TimeDuration = 0;
		} else if (noofbreak == 4) {


		} else {

        }

	}

	$scope.IsValidPeriodManagement = function () {
		//if ($scope.newPeriodManagement.Shift.isEmpty()) {
		//	Swal.fire('Please ! Enter Shift');
		//	return false;
		//}

		return true;
	}

	$scope.SaveUpdatePeriodManagement = function () {
		if ($scope.IsValidPeriodManagement() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newPeriodManagement.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdatePeriodManagement();
					}
				});
			} else
				$scope.CallSaveUpdatePeriodManagement();

		}
	};

	$scope.CallSaveUpdatePeriodManagement = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		angular.forEach($scope.newPeriodManagement.PeriodManagementDetailsColl, function (det) {
			if (det.StartTime_TMP)
				det.StartTime = det.StartTime_TMP.toLocaleString();
			else
				det.StartTime = null;

			if (det.EndTime_TMP)
				det.EndTime = det.EndTime_TMP.toLocaleString();
			else
				det.EndTime = null;
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SavePeriodManagement",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newPeriodManagement }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearPeriodManagement();
				$scope.GetAllPeriodManagement();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllPeriodManagement = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PeriodManagementList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllPeriodManagement",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PeriodManagementList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetPeriodManagementById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			TranId: refData.TranId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetPeriodManagementById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newPeriodManagement = res.data.Data;
				$scope.newPeriodManagement.Mode = 'Modify';

				$scope.changeNoOfBreak(0);

				angular.forEach($scope.newPeriodManagement.PeriodManagementDetailsColl, function (det) {
					if (det.StartTime)
						det.StartTime_TMP = new Date(det.StartTime);
					
					if (det.EndTime)
						det.EndTime_TMP =new Date(det.EndTime);
					
				});

				document.getElementById('period-management-content').style.display = "none";
				document.getElementById('period-management-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelPeriodManagementById = function (refData) {

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
					TranId: refData.TranId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelPeriodManagement",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllPeriodManagement();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

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

	//************************* Manage Schedule *********************************

	$scope.IsValidManageSchedule = function () {
		if ($scope.newManageSchedule.ClassId.isEmpty()) {
			Swal.fire('Please ! Select Class');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateManageSchedule = function () {
		if ($scope.IsValidManageSchedule() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newManageSchedule.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateManageSchedule();
					}
				});
			} else
				$scope.CallSaveUpdateManageSchedule();

		}
	};

	$scope.CallSaveUpdateManageSchedule = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveManageSchedule",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newManageSchedule }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearManageSchedule();
				$scope.GetAllManageScheduleList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllManageScheduleList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ManageScheduleList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllManageScheduleList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ManageScheduleList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetManageScheduleById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ManageScheduleId: refData.ManageScheduleId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetManageScheduleById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newManageSchedule = res.data.Data;
				$scope.newManageSchedule.Mode = 'Modify';

				//document.getElementById('board-section').style.display = "none";
				//document.getElementById('board-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelManageScheduleById = function (refData) {

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
					ManageScheduleId: refData.ManageScheduleId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelManageSchedule",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllManageScheduleList();
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
		//if ($scope.newAssignHod.Department.isEmpty()) {
		//	Swal.fire('Please ! Select Department');
		//	return false;
		//}

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
				cl.ClassShiftId = para.ClassShiftId;
				cl.ShiftId = para.ClassShiftId;
				cl.SubjectId = para.SubjectId;
				//cl.BatchId = para.BatchId;
				//cl.YearId = para.ClassYearId;
				//cl.SemesterId = para.SemesterId;
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
					ClassShiftId:refData.ClassId
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

		if (!$scope.newCreateGroup.SubjectId || $scope.newCreateGroup.SubjectId==0) {
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
						SectionId:cs.SectionId
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


	//*********************************TeacherWiseQuota*****************************************


	$scope.SaveTeacherWiseQuota = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var dataColl = [];

		angular.forEach($scope.TeacherWiseQuotaColl, function (yt) {
			angular.forEach(yt.WeekColl, function (ww) {
				if (ww.NoofPeriod > 0) {
					dataColl.push({
						...ww,
						CanBlock: yt.CanBlock // Include CanBlock from the parent row
					});
				}
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveTeacherWiseQuota",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: dataColl }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				    $scope.GetAllTeacherWiseQuota();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	}


	$scope.GetTeacherWiseQuota = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TeacherWiseQuotaColl = [];
		var para = {
			DepartmentId: $scope.newTeacherWiseQuota.DepartmentId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetTeacherWiseQuota",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dtColl = mx(res.data.Data);
				var groupQry = dtColl.groupBy(p1 => ({ EmployeeId: p1.EmployeeId }));
				$scope.TeacherWiseQuotaColl = [];

				angular.forEach(groupQry, function (q) {
					var chColl = mx(q.elements);
					var fst = q.elements[0];
					var newData = {
							TranId: fst.TranId,
						Name: fst.Name,
						EmployeeId: fst.EmployeeId,
						EmployeeCode: fst.EmployeeCode,
						Department: fst.Department,
						Designation: fst.Designation,
						CanBlock: fst.CanBlock,
						TotalPeriod: fst ? fst.TotalPeriod : 0,
						WeekColl: []
					};

					$scope.WeekColl.forEach(function (mn) {
						var find = chColl.firstOrDefault(p1 => p1.WeekDay == mn.id);
						newData.WeekColl.push({
							EmployeeId: q.key.EmployeeId,
							WeekDay: mn.id,
							MonthName: mn.text,
							NoofPeriod: find ? find.NoofPeriod : 0
						});
					});

					$scope.TeacherWiseQuotaColl.push(newData);
				});

				angular.forEach($scope.TeacherWiseQuotaColl, function (TWQ) {
					angular.forEach(TWQ.WeekColl, function (day) {
						var matchedWeekend = $scope.WeekendList.find(function (wk) {
							return wk.DayId === day.WeekDay; // Use `day.We`, not `day.id`
						});
						day.isShow = matchedWeekend ? false : true; // Hide if it's in WeekendList
					});
				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});
	};


	$scope.distributeEvenly = function (row) {
		var totalWeekly = row.TotalPeriod || 0; // Get the value entered in the Weekly field
		var daysCount = row.WeekColl.filter(function (ww) { return ww.isShow; }).length; // Count visible days in WeekColl
		var baseValue = Math.floor(totalWeekly / daysCount); // Base value to distribute evenly
		var remainingValue = totalWeekly % daysCount; // Remaining value to be distributed starting from the first day

		// Reset the NoofPeriod values for each day in WeekColl
		angular.forEach(row.WeekColl, function (mn, index) {
			if (mn.isShow) {
				// Start by assigning the base value
				mn.NoofPeriod = baseValue;
				// Distribute remaining value to the first few days
				if (remainingValue > 0) {
					mn.NoofPeriod += 1;
					remainingValue--;
				}
			}
		});
	};

	$scope.updateTotal = function (row) {
		row.TotalPeriod = 0;
		angular.forEach(row.WeekColl.filter(ww => ww.isShow), function (mn) {
			if (!isNaN(parseInt(mn.NoofPeriod))) {
				row.TotalPeriod += parseInt(mn.NoofPeriod);
			}
		});
	};





	$scope.GetAllTeacherWiseQuota = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TeacherWiseQuotaList = [];
		var para = {
			DepartmentId: $scope.newFilter.DepartmentId,
			DesignationId: $scope.newFilter.DesignationId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllTeacherWiseQuota",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dtColl = mx(res.data.Data);
				var groupQry = dtColl.groupBy(p1 => ({ EmployeeId: p1.EmployeeId }));
				$scope.TeacherWiseQuotaList = [];

				angular.forEach(groupQry, function (q) {
					var fst = q.elements[0];
					var newData = {
						Name: fst.Name,
						EmployeeCode: fst.EmployeeCode,
						Department: fst.Department,
						Designation: fst.Designation,
						TotalPeriod: fst.TotalPeriod,
						AssignedPeriod: fst.AssignedPeriod,
						Remaining: fst.RemainingQuota,
						/*Remaining: fst.TotalPeriod - fst.AssignedPeriod,*/
						CanBlock: fst.CanBlock,
						EmpStatus: fst.EmpStatus
					};


					$scope.TeacherWiseQuotaList.push(newData);
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});
	};


	$scope.ValidateQuota = function (Emp) {
		if (Emp == null || Emp == undefined)
			return true;

		var EmpId = Emp.EmployeeId;
		if (EmpId == null || EmpId == undefined)
			return true;

		var findQuotaColl = $scope.TeacherQuotaList.where(p1 => p1.EmployeeId == EmpId);

		if (findQuotaColl) {
			var findQuota = findQuotaColl.firstOrDefault();
			if (findQuota) {
				var quota = findQuota.RemainingQuota;
				if (quota <= 0) {
					if (findQuota.CanBlock == true) {
						Emp.EmployeeId = null;
						Swal.fire('Scheduling blocked. Weekly quota exceeded.');
						return false;
					} else {
						
						return false; 
					}
				}

				var currentAssign = 0;
				angular.forEach($scope.newManageSchedule.CurClassScheduleColl, function (sc) {
					angular.forEach(sc.ScheduleColl, function (s) {
						if (s.EmployeeId == EmpId) {
							currentAssign++;
						}
					});
				});

				if (quota < currentAssign) {
					if (findQuota.CanBlock == true) {
						Emp.EmployeeId = null;
						Swal.fire('Scheduling blocked. Weekly quota exceeded.');
						return false;
					} else {
						return false;
					}
				}
			}
		}
		return true;
	};
	 
	$scope.ChangeForAllDay = function (curS) {
		if (curS.ForAllDay == true) {
			var firstData = mx(curS.ScheduleColl);

			if (firstData) {
				angular.forEach($scope.newManageSchedule.CurClassScheduleColl, function (sc) {
					if (curS.DayId < sc.DayId) {
						angular.forEach(sc.ScheduleColl, function (s) {
							var ex = firstData.firstOrDefault(p1 => p1.Period == s.Period);
							if (ex) {
								var originalSubjectId = s.SubjectId;
								var originalEmployeeId = s.EmployeeId;

								s.SubjectId = ex.SubjectId;
								s.EmployeeId = ex.EmployeeId;
								s.AlternetColl = ex.AlternetColl;

								if (ex.EmployeeId && ex.EmployeeId > 0) {
									var isValid = $scope.ValidateQuotaForAllDay(s, sc, ex.EmployeeId);
									if (!isValid) {
										s.SubjectId = originalSubjectId;
										s.EmployeeId = originalEmployeeId;
										s.AlternetColl = null;
									}
								}
							}
						});
					}
				});
			}
		}
	};

	// New function to validate quota for All Day copying
	$scope.ValidateQuotaForAllDay = function (scheduleItem, daySchedule, employeeId) {
		if (!employeeId) return true;

		var findQuotaColl = $scope.TeacherQuotaList.where(p1 => p1.EmployeeId == employeeId);

		if (findQuotaColl) {
			var findQuota = findQuotaColl.firstOrDefault();
			if (findQuota) {
				var quota = findQuota.RemainingQuota;
				if (quota <= 0) {
					Swal.fire('Scheduling blocked. Weekly quota exceeded');
					return false;
				}

				// Count total assignments for this employee including current one
				var currentAssign = 0;
				angular.forEach($scope.newManageSchedule.CurClassScheduleColl, function (sc) {
					angular.forEach(sc.ScheduleColl, function (s) {
						if (s.EmployeeId == employeeId) {
							currentAssign++;
						}
					});
				});

				if (quota < currentAssign) {
					Swal.fire('Scheduling blocked. Weekly quota exceeded');
					return false;
				}
			}
		}
		return true;
	};


	$scope.GetAllTeacherQuota = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TeacherQuotaList = [];	
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllTeacherQuota",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				
				$scope.TeacherQuotaList =mx(res.data.Data)

				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});
	};
	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});