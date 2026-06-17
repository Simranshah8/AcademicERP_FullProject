app.controller('AddAttendanceController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {
	$scope.Title = 'Add Attendance';

	$rootScope.ConfigFunction = function () {

	};
	$rootScope.ChangeLanguage();

	var firtTimeLoad = true;
	$scope.LoadData = function () {
		$('.select2').select2();

		var glbS = GlobalServices;
		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();


		//Added y Suresh on Magh 17 Starts
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

				/*	$scope.SelectedClassSemesterList = [];*/
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
				/*$scope.SelectedClassClassYearList = [];*/
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		//Ends


		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.currentPages = {

			StudentWise: 1,
			SubjectWise: 1,
			EmployeeWise: 1
		};

		$scope.searchData = {

			StudentWise: '',
			SubjectWise: '',
			EmployeeWise: '',
			StudentType: ''
		};

		$scope.perPage = {

			StudentWise: glbS.getPerPageRow(),
			SubjectWise: glbS.getPerPageRow(),
			EmployeeWise: glbS.getPerPageRow()
		};

		$scope.newStudentWise = {
			ClassSection: null,
			StudentColl: [],
			Date_TMP: new Date(),
			Present: false,
			Notify: true
			//Mode: 'Save'
		};


		$scope.newSubjectWise = {
			ForDate_TMP: new Date(),
			Present: false,
			SubjectId: null,
			SubjectList: [],
			Notify: true
		};


		$scope.newEmployeeWise = {
			EmployeeWiseId: null,
			Date_TMP: new Date(),
			Present: false,
			EmployeeWiseDetailsColl: [],
			//Mode: 'Save'
		};

		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.DepartmentList = {};
		glbS.getDepartmentList().then(function (res) {
			$scope.DepartmentList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		//Added By Suresh For Sorting;
		$scope.sortKeys = {
			StudentWise: '',
			SubjectWise: '',
			EmployeeWise: '',
		};

		$scope.reverses = {
			StudentWise: false,
			SubjectWise: false,
			EmployeeWise: false,
		}
		//added by simran for bio attendence
		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
		$scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();

		$scope.AttendenceForColl = [
			{ id: 1, text: 'Student' },
			{ id: 2, text: 'Employee' }
		];

		$scope.newDet = {
			TranId: null,
			AttendenceMode: true,
			InOutTime: null,
			AttendenceDate: null,
			Remarks: '',
			selectedFor: 1,
			SelectStudent: $scope.StudentSearchOptions[0]?.value,
			SelectEmployee: $scope.EmployeeSearchOptions[0]?.value,
			StudentId: null,
			EmployeeId: null,
			Mode: 'Save',
			BioAttendence: []
		};

		$scope.StudentTypeList = [];
		GlobalServices.getStudentTypeList().then(function (res) {
			$scope.StudentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newStudentType = {
			ForDate_TMP: new Date(),
			Class: 1,
			ClassId: 0,
			SectionId: 0
		}
	}



	$scope.ClearStudentWise = function () {
		$scope.newStudentWise = {
			ClassSection: null,
			StudentColl: [],
			Date_TMP: new Date()
			//Mode: 'Save'
		};
		$scope.newStudentWise.StudentWiseDetailsColl.push({});
	}
	$scope.ClearSubjectWise = function () {
		$scope.newSubjectWise = {
			SubjectWiseId: null,
			SubjectWiseDetailsColl: [],

		};
		$scope.newSubjectWise.SubjectWiseDetailsColl.push({});
	}
	$scope.ClearEmployeeWise = function () {
		$scope.newEmployeeWise = {
			EmployeeWiseId: null,

			EmployeeWiseDetailsColl: [],
			//Mode: 'Save'
		};
		$scope.newEmployeeWise.EmployeeWiseDetailsColl.push({});
	}

	//New Code Added By Suresh For Sorting the table data
	$scope.sortStudentWise = function (keyname) {
		$scope.sortKeys.StudentWise = keyname;   //set the sortKey to the param passed
		$scope.reverses.StudentWise = !$scope.reverses.StudentWise; //if true make it false and vice versa
	}

	$scope.sortSubjectWise = function (keyname) {
		$scope.sortKeys.SubjectWise = keyname;
		$scope.reverses.SubjectWise = !$scope.reverses.SubjectWise;
	}


	$scope.sortEmployeeWise = function (keyname) {
		$scope.sortKeys.EmployeeWise = keyname;
		$scope.reverses.EmployeeWise = !$scope.reverses.EmployeeWise;
	}

	//Ends
	//************************* Student Wise *********************************

	$scope.PresentAll = function () {

		var pAll = $scope.newStudentWise.Present;

		if ($scope.newStudentWise.StudentList) {

			angular.forEach($scope.newStudentWise.StudentList, function (st) {
				st.Attendance = (pAll == true ? 1 : 2);
			});
		}
	};

	$scope.GetClassWiseStudentList = function () {

		$scope.newStudentWise.StudentList = [];

		if ($scope.newStudentWise.ClassSection && $scope.newStudentWise.DateDet && $scope.newStudentWise.DateDet.dateAD) {
			$scope.GetClassWiseShift(); //Added by Simran
             var para = {
				ClassId: $scope.newStudentWise.ClassSection.ClassId,
				SectionIdColl: $scope.newStudentWise.ClassSection.SectionId,
				//Added by Suresh on Magh 17 Starts
				BatchId: $scope.newStudentWise.BatchId,
				ClassYearId: $scope.newStudentWise.ClassYearId,
				SemesterId: $scope.newStudentWise.SemesterId
				//Ends
			};

			if (para.ClassId && para.ClassId > 0) {
				GlobalServices.GetClassWiseStudentList(para.ClassId, para.SectionIdColl.toString(), para.BatchId, para.ClassYearId, para.SemesterId).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						$scope.newStudentWise.StudentList = res.data.Data;

						var para1 = {
							ClassId: para.ClassId,
							SectionId: para.SectionIdColl,
							forDate: $filter('date')(new Date($scope.newStudentWise.DateDet.dateAD), 'yyyy-MM-dd'),
							InOutMode: 3,

							//Added by Suresh on Magh 17 Starts
							BatchId: para.BatchId,
							ClassYearId: para.ClassYearId,
							SemesterId: para.SemesterId
							//Ends
						};

						$http({
							method: 'POST',
							url: base_url + "Attendance/Creation/GetStudentDailyAttendance",
							dataType: "json",
							data: JSON.stringify(para1)
						}).then(function (res1) {

							if (res1.data.IsSuccess && res1.data.Data) {
								var attendanceColl = mx(res1.data.Data);

								angular.forEach($scope.newStudentWise.StudentList, function (st) {

									var att = attendanceColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
									st.ForDate = para1.forDate;
									st.Attendance = (att ? att.Attendance : null);
									st.LateMin = (att ? att.LateMin : 0);
									st.Remarks = (att ? att.Remarks : '');
									st.InOutMode = para1.InOutMode;
									st.ClassId = para1.ClassId;
									st.SectionId = para1.SectionId;

								});

							} else {
								Swal.fire(res.data.ResponseMSG);
							}

						}, function (reason) {
							Swal.fire('Failed' + reason);
						});

					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		}



	};
	$scope.IsValidStudentWise = function () {
		if (!$scope.newStudentWise.DateDet) {
			Swal.fire("Please Select For Date.",);
			return false;
		}
		if (!$scope.newStudentWise.ClassSection) {
			Swal.fire("Please Select Class.");
			return false;
		}
		return true;
	}

	$scope.SaveUpdateStudentWise = function () {
		if ($scope.IsValidStudentWise() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudentWise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudentWise();
					}
				});
			} else
				$scope.CallSaveUpdateStudentWise();

		}
	};

	$scope.CallSaveUpdateStudentWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var formattedDate = null;
		if ($scope.newStudentWise.DateDet?.dateAD) {
			formattedDate = $filter('date')(
				new Date($scope.newStudentWise.DateDet.dateAD),
				'yyyy-MM-dd'
			);
		}
		var displayDate = $scope.newStudentWise.DateDet.dateBS;
		var studentList = angular.copy($scope.newStudentWise.StudentList);

		// Assign common properties
		angular.forEach(studentList, function (s) {
			s.ForDate = formattedDate;
			s.InOutMode = 3;
		});

		var presentCount = 0;
		var absentCount = 0;
		var leaveCount = 0;

		angular.forEach(studentList, function (s) {
			if (s.Attendance == 1) { // Present
				presentCount++;
			} else if (s.Attendance == 2) { // Absent
				absentCount++;
			} else if (s.Attendance == 3) { // Late (also considered Present)
				presentCount++;
			} else if (s.Attendance == 4) { // Leave
				leaveCount++;
			}
		});
		var totalStudents = $scope.newStudentWise.StudentList.length;
		var attendanceRate = totalStudents ? ((presentCount / totalStudents) * 100).toFixed(1) : 0;
		var classSectionText = "";
		if ($scope.newStudentWise.ClassSection) {
			classSectionText = $scope.newStudentWise.ClassSection.ClassName + " " + $scope.newStudentWise.ClassSection.SectionName;
		}

		Swal.fire({
			title: "<span style='font-size:18px;'>Please verify the following data before saving</span>",
			html: `
        <div style="text-align:center; line-height:1.5; font-family:Arial, sans-serif; font-size:13px;">
            <div style="margin-bottom:8px;">
                <b>Date:</b> <span style="color:#007bff;">${displayDate || "-"}</span><br/>
                <b>Class & Section:</b> <span style="color:#007bff;">${classSectionText || "-"}</span>
            </div>
            <div style="display:flex; justify-content:center; gap:10px; margin-top:8px;">
                <div style="background:#28a745; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                    <b>Present</b><br/>${presentCount}
                </div>
                <div style="background:#dc3545; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                    <b>Absent</b><br/>${absentCount}
                </div>
                <div style="background:#ffc107; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                    <b>Leave</b><br/>${leaveCount}
                </div>
            </div></br>
                  <b>Total Students:</b> <span style="color:#007bff;">${totalStudents}</span><br/>
                    <b>Attendance Rate:</b> <span style="color:#007bff;">${attendanceRate}%</span>

        </div>
    `,
			icon: "question",
			showCancelButton: true,
			confirmButtonText: "Yes, Save",
			cancelButtonText: "Cancel"
		}).then(function (result) {
			if (result.isConfirmed) {
				// Proceed with save
				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/SaveStudentDailyAttendance",
					headers: { 'Content-Type': undefined },
					transformRequest: function (data) {
						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));
						return formData;
					},
					data: {
						jsonData: studentList
					}
				}).then(function (res) {
					$scope.loadingstatus = "stop";
					hidePleaseWait();
					Swal.fire(res.data.ResponseMSG);
				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire("Error", "Failed to save student daily attendance", "error");
				});
			} else {
				// Cancelled
				hidePleaseWait();
				$scope.loadingstatus = "stop";
			}
		});
	};
	//Added by Simran
	$scope.GetClassWiseShift = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			AcademicYearId: null,
			ClassId: $scope.newStudentWise.ClassSection.ClassId,
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetClassWiseShift",
			dataType: "json",
			data: JSON.stringify(para)
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
	};
	//End
	//************************* Subject wise *********************************

	$scope.IsValidSubjectWise = function () {
		if (!$scope.newSubjectWise.ForDateDet || !$scope.newSubjectWise.ForDateDet.dateAD) {
			Swal.fire("Please select For Date.");
			return false;
		}
		if (!$scope.newSubjectWise.ClassSection) {
			Swal.fire("Please Select Class");
			return false;
		}
		if (!$scope.newSubjectWise.SubjectId) {
			Swal.fire("Please Select Subject.");
			return false;
		}
		if (!$scope.newSubjectWise.PeriodId || $scope.newSubjectWise.PeriodId == 0) {
			Swal.fire("Please Select Period.");
			return false;
		}
		return true;
	}

	$scope.GetClassWiseSubMap = function () {
		$scope.newSubjectWise.SubjectList = [];
		$scope.newSubjectWise.StudentList = [];

		if ($scope.newSubjectWise.ClassSection && $scope.newSubjectWise.ForDateDet) {
			$scope.GetClassWiseShiftforSub();  //Added by Simran

			var para = {
				ClassId: $scope.newSubjectWise.ClassSection.ClassId,
				SectionIdColl: $scope.newSubjectWise.ClassSection.SectionId,
				SemesterId: $scope.newSubjectWise.SemesterId,
				ClassYearId: $scope.newSubjectWise.ClassYearId,
				BatchId: $scope.newSubjectWise.BatchId,
			};

			if (para.ClassId && para.ClassId > 0) {
				$scope.loadingstatus = "running";
				showPleaseWait();

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

						// Directly check conditions in one step
						if ((para.BatchId && (para.SemesterId || para.ClassYearId) && SubjectMappingColl.length === 0) ||
							(!para.BatchId && !para.SemesterId && !para.ClassYearId && SubjectMappingColl.length === 0)) {
							if (para.SemesterId > 0 || para.ClassYearId > 0)
								Swal.fire('No subjects found. Please verify the selected batch, semester, or class year.');
							else
								Swal.fire('Subject Mapping Not Found');

						} else {
							angular.forEach(SubjectMappingColl, function (sm) {
								var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
								if (subDet) {
									$scope.newSubjectWise.SubjectList.push(subDet);
								}
							});
						}
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed: ' + reason);
				});
			}
		}
	};

	//Added by Simran
	$scope.GetClassWiseShiftforSub = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			AcademicYearId: null,
			ClassId: $scope.newSubjectWise.ClassSection.ClassId,
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetClassWiseShift",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassShiftListSub = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};
	//End

	//$scope.GetClassWiseSubMap = function () {

	//	$scope.newSubjectWise.SubjectList = [];
	//	$scope.newSubjectWise.StudentList = [];

	//	if ($scope.newSubjectWise.ClassSection && $scope.newSubjectWise.ForDateDet) {
	//		var para = {
	//			ClassId: $scope.newSubjectWise.ClassSection.ClassId,
	//			SectionIdColl: $scope.newSubjectWise.ClassSection.SectionId,
	//			//Added by Suresh on 17 magh starts
	//			SemesterId: $scope.newSubjectWise.SemesterId,
	//			ClassYearId: $scope.newSubjectWise.ClassYearId,
	//			BatchId: $scope.newSubjectWise.BatchId,
	//		};


	//		if (para.ClassId && para.ClassId > 0) {

	//			$scope.loadingstatus = "running";
	//			showPleaseWait();

	//			$http({
	//				method: 'POST',
	//				url: base_url + "Academic/Creation/GetSubjectMappingClassWise",
	//				dataSchedule: "json",
	//				data: JSON.stringify(para)
	//			}).then(function (res) {
	//				hidePleaseWait();
	//				$scope.loadingstatus = "stop";
	//				if (res.data.IsSuccess && res.data.Data) {
	//					var SubjectMappingColl = res.data.Data;

	//					if (SubjectMappingColl.length == 0) {
	//						Swal.fire('Subject Mapping Not Found');
	//					}
	//					else if (SubjectMappingColl.length > 0) {
	//						angular.forEach(SubjectMappingColl, function (sm) {
	//							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
	//							if (subDet) {
	//								$scope.newSubjectWise.SubjectList.push(subDet);
	//							}
	//						});

	//					}
	//				} else {
	//					Swal.fire(res.data.ResponseMSG);
	//				}

	//			}, function (reason) {
	//				Swal.fire('Failed' + reason);
	//			});
	//		}
	//	}

	//};

	$scope.PresentAllSubjectWise = function () {

		var pAll = $scope.newSubjectWise.Present;

		if ($scope.newSubjectWise.StudentList) {

			angular.forEach($scope.newSubjectWise.StudentList, function (st) {
				st.Attendance = (pAll == true ? 1 : 2);
			});
		}
	};

	$scope.GetClassWiseStudentListforSubject = function () {

		$scope.newSubjectWise.StudentList = [];

		if ($scope.newSubjectWise.ClassSection && $scope.newSubjectWise.ForDateDet && $scope.newSubjectWise.ForDateDet.dateAD) {
			var para = {
				ClassId: $scope.newSubjectWise.ClassSection.ClassId,
				SectionIdColl: $scope.newSubjectWise.ClassSection.SectionId,
				//Added By Suresh on 17 Magh
				BatchId: $scope.newSubjectWise.BatchId,
				SemesterId: $scope.newSubjectWise.SemesterId,
				ClassYearId: $scope.newSubjectWise.ClassYearId,

				PeriodId: $scope.newSubjectWise.PeriodId
			};

			if (para.ClassId && para.ClassId > 0) {
				GlobalServices.GetClassWiseStudentList(para.ClassId, para.SectionIdColl.toString(), para.BatchId, para.ClassYearId, para.SemesterId, para.PeriodId).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						$scope.newSubjectWise.StudentList = res.data.Data;

						var para1 = {
							ClassId: para.ClassId,
							SectionId: para.SectionIdColl,
							SubjectId: $scope.newSubjectWise.SubjectId,
							forDate: ($filter('date')(new Date($scope.newSubjectWise.ForDateDet.dateAD), 'yyyy-MM-dd')),
							InOutMode: 3,
							//Added By Suresh On Magh 17 
							BatchId: para.BatchId,
							SemesterId: para.SemesterId,
							ClassYearId: para.ClassYearId,
							//Ends
							PeriodId: para.PeriodId
						};

						$http({
							method: 'POST',
							url: base_url + "Attendance/Creation/GetStudentSubjectWiseAttendance",
							dataType: "json",
							data: JSON.stringify(para1)
						}).then(function (res1) {

							if (res1.data.IsSuccess && res1.data.Data) {
								var attendanceColl = mx(res1.data.Data);

								angular.forEach($scope.newSubjectWise.StudentList, function (st) {

									var att = attendanceColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
									st.ForDate = para1.forDate;
									st.Attendance = (att ? att.Attendance : null);
									st.LateMin = (att ? att.LateMin : 0);
									st.Remarks = (att ? att.Remarks : '');
									st.InOutMode = para1.InOutMode;
									st.ClassId = para1.ClassId;
									st.SectionId = para1.SectionId;
									st.SubjectId = para1.SubjectId;
									st.PeriodId = para1.PeriodId;
								});

							} else {
								/*Swal.fire(res.data.ResponseMSG);*/

							}

						}, function (reason) {
							Swal.fire('Failed' + reason);
						});

					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}

		}



	};

	$scope.SaveUpdateSubjectWise = function () {
		if ($scope.IsValidSubjectWise() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSubjectWise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSubjectWise();
					}
				});
			} else
				$scope.CallSaveUpdateSubjectWise();

		}
	};

	//$scope.CallSaveUpdateSubjectWise = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	$http({
	//		method: 'POST',
	//		url: base_url + "Attendance/Creation/SaveStudentSubjectWiseAttendance",
	//		headers: { 'Content-Type': undefined },

	//		transformRequest: function (data) {

	//			var formData = new FormData();
	//			formData.append("jsonData", angular.toJson(data.jsonData));

	//			return formData;
	//		},
	//		data: { jsonData: $scope.newSubjectWise.StudentList }
	//	}).then(function (res) {

	//		$scope.loadingstatus = "stop";
	//		hidePleaseWait();

	//		Swal.fire(res.data.ResponseMSG);


	//	}, function (errormessage) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";

	//	});
	//}

	$scope.CallSaveUpdateSubjectWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		// Count Present (including Late), Absent, Leave
		var presentCount = 0, absentCount = 0, leaveCount = 0;
		angular.forEach($scope.newSubjectWise.StudentList, function (s) {
			if (s.Attendance == 1 || s.Attendance == 3) presentCount++;
			else if (s.Attendance == 2) absentCount++;
			else if (s.Attendance == 4) leaveCount++;
			s.PeriodId = $scope.newSubjectWise.PeriodId;
		});

		var attendanceRate = totalStudents ? ((presentCount / totalStudents) * 100).toFixed(1) : 0;

		// Prepare display values
		var displayDate = $scope.newSubjectWise.ForDateDet.dateBS; // Nepali date
		var classSectionText = $scope.newSubjectWise.ClassSection
			? $scope.newSubjectWise.ClassSection.ClassName + " " + ($scope.newSubjectWise.ClassSection.SectionName || "")
			: "-";
		var subjectText = $scope.newSubjectWise.SubjectList?.find(s => s.id === $scope.newSubjectWise.SubjectId)?.text || "-";
		var periodText = $scope.newSubjectWise.PeriodId || "-";
		var totalStudents = $scope.newSubjectWise.StudentList.length;
		var attendanceRate = totalStudents ? ((presentCount / totalStudents) * 100).toFixed(1) : 0;
		// Show confirmation dialog
		Swal.fire({
			title: "<span style='font-size:18px;'>Please verify the following data before saving</span>",
			html: `
            <div style="text-align:center; line-height:1.5; font-family:Arial, sans-serif; font-size:13px;">
                <div style="margin-bottom:8px;">
                    <b>Date:</b> <span style="color:#007bff;">${displayDate}</span><br/>
                    <b>Class & Section:</b> <span style="color:#007bff;">${classSectionText}</span><br/>
                    <b>Subject:</b> <span style="color:#007bff;">${subjectText}</span><br/>
                    <b>Period:</b> <span style="color:#007bff;">${periodText}</span>

                </div>
                <div style="display:flex; justify-content:center; gap:10px; margin-top:8px;">
                    <div style="background:#28a745; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                        <b>Present</b><br/>${presentCount}
                    </div>
                    <div style="background:#dc3545; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                        <b>Absent</b><br/>${absentCount}
                    </div>
                    <div style="background:#ffc107; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                        <b>Leave</b><br/>${leaveCount}
                    </div>
                </div></br>
                  <b>Total Students:</b> <span style="color:#007bff;">${totalStudents}</span><br/>
                    <b>Attendance Rate:</b> <span style="color:#007bff;">${attendanceRate}%</span>
            </div>
        `,
			icon: "question",
			showCancelButton: true,
			confirmButtonText: "<span style='font-size:14px;'>Yes, Save</span>",
			cancelButtonText: "<span style='font-size:14px;'>Cancel</span>",
			customClass: { popup: 'swal2-border-radius', icon: 'swal2-icon-small' }
		}).then(function (result) {
			if (result.isConfirmed) {
				// Proceed with save
				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/SaveStudentSubjectWiseAttendance",
					headers: { 'Content-Type': undefined },
					transformRequest: function (data) {
						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));
						return formData;
					},
					data: { jsonData: $scope.newSubjectWise.StudentList }
				}).then(function (res) {
					$scope.loadingstatus = "stop";
					hidePleaseWait();
					Swal.fire(res.data.ResponseMSG);
				}, function () {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire("Error", "Failed to save subject-wise attendance", "error");
				});
			} else {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
			}
		});
	};



	//Delete Subjectwise 
	$scope.DelSubjectWiseAttendance = function () {
		Swal.fire({
			title: 'Do you want to delete the data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					ClassId: $scope.newSubjectWise.ClassSection ? $scope.newSubjectWise.ClassSection.ClassId : null,
					SectionId: $scope.newSubjectWise.ClassSection ? $scope.newSubjectWise.ClassSection.SectionId : null,
					ForDate: $filter('date')($scope.newSubjectWise.ForDateDet.dateAD, 'yyyy-MM-dd'),
					AttendaneTypeId: 3,
					BatchId: $scope.newSubjectWise.BatchId,
					ClassYearId: $scope.newSubjectWise.ClassYearId,
					SemesterId: $scope.newSubjectWise.SemesterId,
					PeriodId: $scope.newSubjectWise.PeriodId
				};

				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/DeleteSubjectWiseAttendance",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						Swal.fire(res.data.ResponseMSG);
						$scope.GetClassWiseStudentListforSubject();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	}

	//************************* Employee Wise *********************************

	$scope.PresentAllEmployee = function () {

		var pAll = $scope.newEmployeeWise.Present;

		if ($scope.newEmployeeWise.EmployeeWiseDetailsColl) {

			angular.forEach($scope.newEmployeeWise.EmployeeWiseDetailsColl, function (st) {
				st.Attendance = (pAll == true ? 1 : 2);
			});
		}
	};

	$scope.GetEmpSummaryList = function () {

		if (firtTimeLoad) {
			firtTimeLoad = false;
			return;
		}

		if (isEmptyObj($scope.newEmployeeWise.DepartmentId))
			$scope.newEmployeeWise.DepartmentId = 0;

		if ($scope.newEmployeeWise.DepartmentId >= 0 && $scope.newEmployeeWise.DateDet) {

			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				DepartmentIdColl: $scope.newEmployeeWise.DepartmentId.toString()
			}
			$http({
				method: 'POST',
				url: base_url + "Academic/Report/GetEmpSummary",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess) {
					$scope.newEmployeeWise.EmployeeWiseDetailsColl = res.data.Data;

					var para1 = {
						DepartmentId: para.DepartmentIdColl,
						forDate: ($filter('date')(new Date($scope.newEmployeeWise.DateDet.dateAD), 'yyyy-MM-dd')),
						InOutMode: 3
					};

					$http({
						method: 'POST',
						url: base_url + "Attendance/Creation/GetEmployeeDailyAttendance",
						dataType: "json",
						data: JSON.stringify(para1)
					}).then(function (res1) {

						if (res1.data.IsSuccess && res1.data.Data) {
							var attendanceColl = mx(res1.data.Data);

							angular.forEach($scope.newEmployeeWise.EmployeeWiseDetailsColl, function (st) {

								var att = attendanceColl.firstOrDefault(p1 => p1.EmployeeId == st.EmployeeId);
								st.ForDate = para1.forDate;
								st.Attendance = (att ? att.Attendance : null);
								st.LateMin = (att ? att.LateMin : 0);
								st.Remarks = (att ? att.Remarks : '');
								st.InOutMode = para1.InOutMode;
								st.DepartmentId = para1.DepartmentId;

							});

						} else {
							Swal.fire(res.data.ResponseMSG);
						}

					}, function (reason) {
						Swal.fire('Failed' + reason);
					});


				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}

	}

	$scope.IsValidEmployeeWise = function () {
		if (!$scope.newEmployeeWise.DateDet || !$scope.newEmployeeWise.DateDet.dateAD) {
			Swal.fire("Please Select For Date.");
			return false;
		}

		if ($scope.newEmployeeWise.DepartmentId == null || $scope.newEmployeeWise.DepartmentId === undefined) {
			Swal.fire("Please Select Department.");
			return false;
		}


		return true;
	}

	$scope.SaveUpdateEmployeeWise = function () {
		if ($scope.IsValidEmployeeWise() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newEmployeeWise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateEmployeeWise();
					}
				});
			} else
				$scope.CallSaveUpdateEmployeeWise();

		}
	};

	//$scope.CallSaveUpdateEmployeeWise = function () {
	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	$http({
	//		method: 'POST',
	//		url: base_url + "Attendance/Creation/SaveEmployeeDailyAttendance",
	//		headers: { 'Content-Type': undefined },

	//		transformRequest: function (data) {

	//			var formData = new FormData();
	//			formData.append("jsonData", angular.toJson(data.jsonData));

	//			return formData;
	//		},
	//		data: { jsonData: $scope.newEmployeeWise.EmployeeWiseDetailsColl }
	//	}).then(function (res) {

	//		$scope.loadingstatus = "stop";
	//		hidePleaseWait();

	//		Swal.fire(res.data.ResponseMSG);


	//	}, function (errormessage) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";

	//	});
	//}

	$scope.CallSaveUpdateEmployeeWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		// --- Count summary for popup ---
		var presentCount = 0, absentCount = 0, lateCount = 0, leaveCount = 0;
		angular.forEach($scope.newEmployeeWise.EmployeeWiseDetailsColl, function (e) {
			if (e.Attendance == 1) presentCount++;
			else if (e.Attendance == 2) absentCount++;
			else if (e.Attendance == 3) lateCount++;
			else if (e.Attendance == 4) leaveCount++;
		});

		var totalEmployees = $scope.newEmployeeWise.EmployeeWiseDetailsColl.length;
		var attendanceRate = totalEmployees ? (((presentCount + lateCount) / totalEmployees) * 100).toFixed(1) : 0;

		var displayDate = $scope.newEmployeeWise.DateDet?.dateBS || "-";
		var departmentText = $scope.DepartmentList?.find(d => d.id === $scope.newEmployeeWise.DepartmentId)?.text || "All Departments";

		// --- Confirmation Dialog ---
		Swal.fire({
			title: "<span style='font-size:18px;'>Please verify the following data before saving</span>",
			html: `
            <div style="text-align:center; line-height:1.5; font-family:Arial, sans-serif; font-size:13px;">
                <div style="margin-bottom:8px;">
                    <b>Date:</b> <span style="color:#007bff;">${displayDate}</span><br/>
                    <b>Department:</b> <span style="color:#007bff;">${departmentText}</span><br/>
                </div>
                <div style="display:flex; justify-content:center; gap:10px; margin-top:8px;">
                    <div style="background:#28a745; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                        <b>Present</b><br/>${presentCount}
                    </div>
                    <div style="background:#17a2b8; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                        <b>Late</b><br/>${lateCount}
                    </div>
                    <div style="background:#dc3545; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                        <b>Absent</b><br/>${absentCount}
                    </div>
                    <div style="background:#ffc107; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                        <b>Leave</b><br/>${leaveCount}
                    </div>
                </div></br>
                <b>Total Employees:</b> <span style="color:#007bff;">${totalEmployees}</span><br/>
                <b>Attendance Rate:</b> <span style="color:#007bff;">${attendanceRate}%</span>
            </div>
        `,
			icon: "question",
			showCancelButton: true,
			confirmButtonText: "<span style='font-size:14px;'>Yes, Save</span>",
			cancelButtonText: "<span style='font-size:14px;'>Cancel</span>",
			customClass: { popup: 'swal2-border-radius', icon: 'swal2-icon-small' }
		}).then(function (result) {
			if (result.isConfirmed) {
				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/SaveEmployeeDailyAttendance",
					headers: { 'Content-Type': undefined },
					transformRequest: function (data) {
						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));
						return formData;
					},
					data: { jsonData: $scope.newEmployeeWise.EmployeeWiseDetailsColl }
				}).then(function (res) {
					$scope.loadingstatus = "stop";
					hidePleaseWait();
					Swal.fire(res.data.ResponseMSG);
				}, function () {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire("Error", "Failed to save Employee attendance", "error");
				});
			} else {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
			}
		});
	};


	$scope.DelStudentManualAttendance = function () {
		Swal.fire({
			title: 'Do you want to delete the data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					ClassId: $scope.newStudentWise.ClassSection ? $scope.newStudentWise.ClassSection.ClassId : null,
					SectionId: $scope.newStudentWise.ClassSection ? $scope.newStudentWise.ClassSection.SectionId : null,
					ForDate: $filter('date')($scope.newStudentWise.DateDet.dateAD, 'yyyy-MM-dd'),
					AttendaneTypeId: null,
					BatchId: $scope.newStudentWise.BatchId,
					ClassYearId: $scope.newStudentWise.ClassYearId,
					SemesterId: $scope.newStudentWise.SemesterId
				};

				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/DeleteStudentManualDailyAttendance",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						Swal.fire(res.data.ResponseMSG);
						$scope.GetClassWiseStudentList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	}

	//Delete newEmployeeWise
	$scope.DelEmployeeWiseAttendance = function () {
		Swal.fire({
			title: 'Do you want to delete the data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					ForDate: $filter('date')($scope.newEmployeeWise.DateDet.dateAD, 'yyyy-MM-dd'),
					DepartmentId: $scope.newEmployeeWise.DepartmentId,
					AttendaneTypeId: 3
				};

				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/DeleteEmployeeWiseAttendance",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						Swal.fire(res.data.ResponseMSG);
						$scope.GetEmpSummaryList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	}
	//************************* Bio Attendence *********************************
	$scope.ResetBioAttendence = function () {
		$timeout(function () {
			$scope.newDet = {
				TranId: null,
				AttendenceMode: true,
				InOutTime: null,
				AttendenceDate: null,
				Remarks: '',
				selectedFor: 1,
				SelectStudent: $scope.StudentSearchOptions[0]?.value,
				SelectEmployee: $scope.EmployeeSearchOptions[0]?.value,
				StudentId: null,
				EmployeeId: null,
				StudentDetails: {},
				EmployeeDetails: {},
				Mode: 'Save',
				BioAttendence: []
			};
		})
	}


	$scope.ClearTbl = function () {
		$scope.newDet.BoiAttendance = [];
	};

	$scope.SaveUpdateBioAttendence = function () {
	/*if ($scope.IsValidBioAttendence() == true)*/ {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDet.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBioAttendence();
					}
				});
			} else
				$scope.CallSaveUpdateBioAttendence();

		}
	};

	$scope.CallSaveUpdateBioAttendence = function () {
		$scope.loadingstatus = "running";

		// Validation for "For"
		if (!$scope.newDet.selectedFor || ($scope.newDet.selectedFor != 1 && $scope.newDet.selectedFor != 2)) {
			Swal.fire("Please select For (Student or Employee).");
			$scope.loadingstatus = "stop";
			return;
		}

		// Validation for student/employee selection
		if ($scope.newDet.selectedFor == 1 && !$scope.newDet.StudentId) {
			Swal.fire("Please select a Student.");
			$scope.loadingstatus = "stop";
			return;
		}
		if ($scope.newDet.selectedFor == 2 && !$scope.newDet.EmployeeId) {
			Swal.fire("Please select an Employee.");
			$scope.loadingstatus = "stop";
			return;
		}

		// Validation for empty table
		if (!$scope.newDet.BioAttendence || $scope.newDet.BioAttendence.length === 0) {
			Swal.fire("Please add at least one attendance record.");
			$scope.loadingstatus = "stop";
			return;
		}

		var tmpData = [];
		var isValid = true;

		angular.forEach($scope.newDet.BioAttendence, function (S, idx) {
			if (S.AttendenceMode === undefined || S.AttendenceMode === null) {
				Swal.fire("Please select In/Out for row " + (idx + 1));
				isValid = false;
				return;
			}
			if (!S.InOutTime_TMP) {
				Swal.fire("Please enter In/Out Time for row " + (idx + 1));
				isValid = false;
				return;
			}
			if (!S.AttendenceDateDet || !S.AttendenceDateDet.dateAD) {
				Swal.fire("Please select Date for row " + (idx + 1));
				isValid = false;
				return;
			}

			var tmp = {
				StudentId: ($scope.newDet.selectedFor == 1) ? $scope.newDet.StudentId : null,
				EmployeeId: ($scope.newDet.selectedFor == 2) ? $scope.newDet.EmployeeId : null,
				AttendenceMode: S.AttendenceMode,
				AttendenceDate: $filter('date')(new Date(S.AttendenceDateDet.dateAD), 'yyyy-MM-dd'),
				InOutTime: S.InOutTime_TMP.toLocaleString(),
				Remarks: S.Remarks || ""
			};
			tmpData.push(tmp);
		});

		if (!isValid) {
			$scope.loadingstatus = "stop";
			return;
		}

		showPleaseWait();

		$http({
			method: 'Post',
			url: base_url + "Attendance/Creation/SaveBioAttendence",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: tmpData }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ResetBioAttendence();
				$scope.GetAllBioAttendence();
			}
		}, function () {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	};


	$scope.GetBioAttendenceStudentdById = function () {
		if ($scope.newDet.StudentId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: $scope.newDet.StudentId || null,
				EmployeeId: null
			};
			$http({
				method: 'POST',
				url: base_url + "Attendance/Creation/GetAllBioAttendence",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newDet.BioAttendence = res.data.Data;
					//date and time
					angular.forEach($scope.newDet.BioAttendence, function (S) {
						if (S.AttendenceDate) {
							S.AttendenceDate_TMP = new Date(S.AttendenceDate);
						}
						if (S.InOutTime) {
							S.InOutTime_TMP = new Date(S.InOutTime);
						}
					});

					if (!$scope.newDet.BioAttendence || $scope.newDet.BioAttendence.length == 0) {
						$scope.newDet.BioAttendence = [];
						$scope.newDet.BioAttendence.push({});
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

				hidePleaseWait();
				$scope.loadingstatus = "stop";

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	};

	$scope.GetBioAttendenceEmployeeById = function () {
		if ($scope.newDet.EmployeeId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				StudentId: null,
				EmployeeId: $scope.newDet.EmployeeId || null
			};
			$http({
				method: 'POST',
				url: base_url + "Attendance/Creation/GetAllBioAttendence",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newDet.BioAttendence = res.data.Data;
					//date and time
					angular.forEach($scope.newDet.BioAttendence, function (S) {
						if (S.AttendenceDate) {
							S.AttendenceDate_TMP = new Date(S.AttendenceDate);
						}
						if (S.InOutTime) {
							S.InOutTime_TMP = new Date(S.InOutTime);
						}
					});
					if (!$scope.newDet.BioAttendence || $scope.newDet.BioAttendence.length == 0) {
						$scope.newDet.BioAttendence = [];
						$scope.newDet.BioAttendence.push({});
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

				hidePleaseWait();
				$scope.loadingstatus = "stop";

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	};

	$scope.AddPHDDetails = function (ind) {
		if ($scope.newDet.BioAttendence) {
			if ($scope.newDet.BioAttendence.length > ind + 1) {
				$scope.newDet.BioAttendence.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newDet.BioAttendence.push({
					ClassName: ''
				})
			}
		}
	};


	//Get for period for attendance

	$scope.GetPeriodForAttendance = function () {
		$scope.PeriodList = [];
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			BatchId: $scope.newSubjectWise.BatchId || null,
			ClassId: $scope.newSubjectWise.ClassSection.ClassId,
			SectionId: $scope.newSubjectWise.ClassSection.SectionId || null,
			SemesterId: $scope.newSubjectWise.SemesterId || null,
			ClassYearId: $scope.newSubjectWise.ClassYearId || null,
			SubjectId: $scope.newSubjectWise.SubjectId
		};
		$http({
			method: 'POST',
			url: base_url + "Attendance/Creation/GetAllPeriodForAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.PeriodList = res.data.Data;

				if ($scope.PeriodList.length === 1) {
					$scope.newSubjectWise.PeriodId = $scope.PeriodList[0].Period;
					$scope.GetClassWiseStudentListforSubject();
				}
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	//-------------------------Student Type Wise Attendance starts--------------------------------------------------------------

	$scope.GetTypeWiseStudentList = function () {
		$scope.newStudentType.StudentList = [];

		if ($scope.newStudentType.ClassSection && $scope.newStudentType.ForDateDet && $scope.newStudentType.ForDateDet.dateAD) {
			var para = {
				StudentTypeId: $scope.newStudentType.StudentTypeId,
				ClassId: $scope.newStudentType.ClassSection.ClassId,
				SectionId: $scope.newStudentType.ClassSection.SectionId,
				BatchId: $scope.newStudentType.BatchId,
				ClassYearId: $scope.newStudentType.ClassYearId,
				SemesterId: $scope.newStudentType.SemesterId,
				ForDate: $filter('date')(new Date($scope.newStudentType.ForDateDet.dateAD), 'yyyy-MM-dd'),
				InOutMode: 3
			};

			if (para.ClassId && para.ClassId > 0) {

				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/GetStudentTypeWiseAttendance",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {

						$scope.newStudentType.StudentList = res.data.Data;
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed: ' + reason);
				});
			}
		}
	};



	$scope.PresentAllStdType = function () {
		var pAll = $scope.newStudentType.Present;
		if ($scope.newStudentType.StudentList) {
			angular.forEach($scope.newStudentType.StudentList, function (st) {
				st.Attendance = (pAll == true ? 1 : 2);
			});
		}
	};


	$scope.IsValidStudentTypeWise = function () {
		//if ($scope.newStudentWise.ClassId.isEmpty()) {
		//	Swal.fire('Please ! Select Class');
		//	return false;
		//}

		return true;
	}

	$scope.SaveUpdateStudentTypeWise = function () {

		if (!$scope.newStudentType.ForDateDet) {
			Swal.fire("Please select For Date.");
			return;
		}
		
		if ($scope.newStudentType.StudentTypeId == null || $scope.newStudentType.StudentTypeId === undefined) {
			Swal.fire("Please select Student Type");
			return;
		}

		if (!$scope.newStudentType.ClassSection) {
			Swal.fire("Please select Class");
			return;
		}

		$scope.loadingstatus = "running";
		showPleaseWait();

		// Count Present (including Late), Absent, Leave
		var presentCount = 0, absentCount = 0, leaveCount = 0;
		angular.forEach($scope.newStudentType.StudentList, function (s) {
			if (s.Attendance == 1 || s.Attendance == 3) presentCount++;
			else if (s.Attendance == 2) absentCount++;
			else if (s.Attendance == 4) leaveCount++;
		});

		var totalStudents = $scope.newStudentType.StudentList.length;
		var attendanceRate = totalStudents ? ((presentCount / totalStudents) * 100).toFixed(1) : 0;

		// Format AD date as yyyy-MM-dd for DB
		var formattedDate = null;
		if ($scope.newStudentType.ForDateDet?.dateAD) {
			formattedDate = $filter('date')(
				new Date($scope.newStudentType.ForDateDet.dateAD),
				'yyyy-MM-dd'
			);
		}

		// Prepare display values (for confirmation popup)
		var displayDate = $scope.newStudentType.ForDateDet.dateBS; // Show Nepali date
		var classSectionText = $scope.newStudentType.ClassSection
			? $scope.newStudentType.ClassSection.ClassName + " " + ($scope.newStudentType.ClassSection.SectionName || "")
			: "-";
		var studenttypeText = $scope.StudentTypeList?.find(s => s.StudentTypeId === $scope.newStudentType.StudentTypeId)?.Name || "-";

		// Confirmation dialog
		Swal.fire({
			title: "<span style='font-size:18px;'>Please verify the following data before saving</span>",
			html: `
            <div style="text-align:center; line-height:1.5; font-family:Arial, sans-serif; font-size:13px;">
                <div style="margin-bottom:8px;">
                    <b>Date:</b> <span style="color:#007bff;">${displayDate}</span><br/>
                    <b>Class & Section:</b> <span style="color:#007bff;">${classSectionText}</span><br/>
                    <b>Student Type:</b> <span style="color:#007bff;">${studenttypeText}</span><br/>
                </div>
                <div style="display:flex; justify-content:center; gap:10px; margin-top:8px;">
                    <div style="background:#28a745; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                        <b>Present</b><br/>${presentCount}
                    </div>
                    <div style="background:#dc3545; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                        <b>Absent</b><br/>${absentCount}
                    </div>
                    <div style="background:#ffc107; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                        <b>Leave</b><br/>${leaveCount}
                    </div>
                </div></br>
                <b>Total Students:</b> <span style="color:#007bff;">${totalStudents}</span><br/>
                <b>Attendance Rate:</b> <span style="color:#007bff;">${attendanceRate}%</span>
            </div>
        `,
			icon: "question",
			showCancelButton: true,
			confirmButtonText: "<span style='font-size:14px;'>Yes, Save</span>",
			cancelButtonText: "<span style='font-size:14px;'>Cancel</span>",
			customClass: { popup: 'swal2-border-radius', icon: 'swal2-icon-small' }
		}).then(function (result) {
			if (result.isConfirmed) {
				var dtColl = [];
				$scope.newStudentType.StudentList.forEach(function (s) {
					dtColl.push({
						ForDate: formattedDate,
						StudentTypeId: $scope.newStudentType.StudentTypeId,
						StudentId: s.StudentId,
						InOutMode: s.InOutMode || 3,
						Attendance: s.Attendance,
						LateMin: s.LateMin,
						Remarks: s.Remarks
					});
				});
				$http({
					method: 'POST',
					url: base_url + "Attendance/Creation/SaveUpdateStudentTypeWise",
					headers: { 'Content-Type': undefined },
					transformRequest: function (data) {
						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));
						return formData;
					},
					data: { jsonData: dtColl }
				}).then(function (res) {
					$scope.loadingstatus = "stop";
					hidePleaseWait();
					Swal.fire(res.data.ResponseMSG);
				}, function () {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire("Error", "Failed to save Student type-wise attendance", "error");
				});

			} else {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
			}
		});
	};


	$scope.onAttendanceChange = function (sw) {

		// If not Late (3), reset LateMin
		if (sw.Attendance != 3) {
			sw.LateMin = 0;
		}

	};
});