$(document).ready(function () {

	$(document).on('keyup', '.serial', function (e) {
		if (e.which == 13) {
			var checkBoxChecked = true;// $('#chkColumnFocus').prop('checked');
			if (checkBoxChecked == true) {
				var $this = $(this);
				var $td = $this.closest('td');
				var $row = $td.closest('tr');
				var $rows = $row.parent();
				var column = $td.index();


				while ($td.length) {

					$row = $row.next('tr');

					if ($row.length == 0) {

						$row = $rows.children().first();
						// $row = $rows.children().get(2);

						column++;
					}

					$td = $row.children().eq(column);
					var $input = $td.find('.serial');
					if ($input.length) {
						$input.focus();
						break;
					}
				}
			} else {

				var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
				if (key == 13) {
					e.preventDefault();
					var inputs = $(this).closest('form').find(':input:visible');
					inputs.eq(inputs.index(this) + 1).focus();
				}
			}

		}
	});


});

app.controller('AttendanceController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Attendance';

	//OnClickDefault();
	var glbS = GlobalServices;
	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ExamTypeList = [];
		glbS.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllSubjectList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SubjectList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.currentPages = {
			/*ExamAttendance: 1,*/
			BulkAttendance: 1,
			ExamTypeSummary: 1,
			ExamTypeGroup: 1,
			ParentExamGroup: 1
		};

		$scope.searchData = {
			ExamAttendance: '',
			BulkAttendance: '',
			ExamTypeSummary: '',
			ExamTypeGroup: '',
			ParentExamGroup: ''
		};

		$scope.perPage = {
			//ExamAttendance: GlobalServices.getPerPageRow(),
			BulkAttendance: GlobalServices.getPerPageRow(),
			ExamTypeSummary: glbS.getPerPageRow(),
			ExamTypeGroup: glbS.getPerPageRow(),
			ParentExamGroup: glbS.getPerPageRow()
		};

		$scope.newExamAttendance = {
			ExamAttendanceId: null,
			ExamAttendanceDetailsColl: [],
			ExamDate_TMP:new Date(),
			Mode: 'Save'
		};


		$scope.newBulkAttendance = {
			SelectedClass: null,
			ExamTypeId: null,
			WorkingDays: 0,
			FromDate: null,
			ToDate: null,
			FromDate_TMP:new Date(),
			ToDate_TMP:new Date(),
			Mode: 'Save'
		};

		$scope.newBlock = {
			SelectedClass: null,
			ExamTypeId: null,
			Mode: 'Save'
		};


		$scope.newExamTypeSummary = {
			ExamTypeSummaryId: null,

			Mode: 'Save'
		};

		$scope.newExamTypeGroup = {
			ExamTypeGroupId: null,

			Mode: 'Save'
		};

		$scope.newParentExamGroup = {
			ParentExamGroupId: null,

			Mode: 'Save'
		};

		$scope.GetAllExamShiftList();
	}

	$scope.GetAllExamShiftList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamShiftList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamShiftList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamShiftList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetRoomListForSeatPlan = function () {
		$scope.VacantRoomList = [];

		if ($scope.newExamAttendance.ExamShiftId > 0 && $scope.newExamAttendance.ExamTypeId > 0) {
			var para =
			{
				ExamShiftId: $scope.newExamAttendance.ExamShiftId,
				ExamTypeId: $scope.newExamAttendance.ExamTypeId
			};
			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetExamShiftWiseRoom",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.VacantRoomList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.ClearExamAttendance = function () {
		$scope.newExamAttendance = {
			ExamAttendanceId: null,
			ExamAttendanceDetailsColl: [],
			Mode: 'Save'
		};

	}
	$scope.ClearBulkAttendance = function () {
		$scope.newBulkAttendance = {
			BulkAttendanceId: null,
			BulkAttendanceDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newBulkAttendance.BulkAttendanceDetailsColl.push({});
	}
	$scope.ClearExamTypeSummary = function () {
		$scope.newExamTypeSummary = {
			ExamTypeSummaryId: null,

			Mode: 'Save'
		};
	}
	$scope.ClearExamTypeGroup = function () {
		$scope.newExamTypeGroup = {
			ExamTypeGroupId: null,

			Mode: 'Save'
		};
	}

	$scope.ClearParentExamGroup = function () {
		$scope.newParentExamGroup = {
			ParentExamGroupId: null,

			Mode: 'Save'
		};
	}

	//************************* Exam Attendance *********************************



	$scope.IsValidExamAttendance = function () {
		//if ($scope.newExamAttendance.ClassId.isEmpty()) {
		//	Swal.fire('Please ! Select Class');
		//	return false;
		//}

		return true;
	}

	//NEw
	$scope.GetExamRoomWiseStudentList = function () {
		$scope.newExamAttendance.ExamAttendanceDetailsColl = [];
		if (!angular.isDefined($scope.newExamAttendance.ExamTypeId) || $scope.newExamAttendance.ExamTypeId <= 0) {
			Swal.fire('Please Select Exam Type');
			return;
		}
		if (!angular.isDefined($scope.newExamAttendance.ExamShiftId) || $scope.newExamAttendance.ExamShiftId <= 0) {
			Swal.fire('Please Select Exam Shift');
			return;
		}
		if (!angular.isDefined($scope.newExamAttendance.RoomId) || $scope.newExamAttendance.RoomId <= 0) {
			Swal.fire('Please Select Room');
			return;
		}
		if (!$scope.newExamAttendance.ExamDateDet || !$scope.newExamAttendance.ExamDateDet.dateAD) {
			Swal.fire('Please Select Exam Date');
			return;
		}
		var para = {
			ExamTypeId: $scope.newExamAttendance.ExamTypeId,
			ExamShiftId: $scope.newExamAttendance.ExamShiftId,
			RoomId: $scope.newExamAttendance.RoomId,
			ExamDate: $filter('date')(new Date($scope.newExamAttendance.ExamDateDet.dateAD), 'yyyy-MM-dd')
		};
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamWiseAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			if (res.data.IsSuccess && angular.isArray(res.data.Data)) {
				$scope.newExamAttendance.ExamAttendanceDetailsColl = res.data.Data;
			} else {
				Swal.fire('Notice', res.data.ResponseMSG || 'No attendance records found.', 'info');
			}
		}, function (reason) {
			hidePleaseWait();
			Swal.fire('Error', 'Failed to load exam attendance: ' + (reason.statusText || reason), 'error');
		});
	};

	$scope.PresentAllStd = function () {
		var pAll = $scope.newExamAttendance.Present;
		if ($scope.newExamAttendance.ExamAttendanceDetailsColl) {
			angular.forEach($scope.newExamAttendance.ExamAttendanceDetailsColl, function (st) {
				st.Attendance = (pAll == true ? 1 : 2);
			});
		}
	};


	$scope.SaveUpdateExamAttendance = function () {
		if (!$scope.newExamAttendance.ExamDateDet) {
			Swal.fire("Please select Exam Date.");
			return;
		}
		if ($scope.newExamAttendance.ExamTypeId == null || $scope.newExamAttendance.ExamTypeId === undefined) {
			Swal.fire("Please select Exam Type");
			return;
		}
		if ($scope.newExamAttendance.ExamShiftId == null || $scope.newExamAttendance.ExamShiftId === undefined) {
			Swal.fire("Please select Exam Shift");
			return;
		}
		if ($scope.newExamAttendance.RoomId == null || $scope.newExamAttendance.RoomId === undefined) {
			Swal.fire("Please select Room");
			return;
		}
		$scope.loadingstatus = "running";
		showPleaseWait();
		var presentCount = 0, absentCount = 0;
		angular.forEach($scope.newExamAttendance.ExamAttendanceDetailsColl, function (s) {
			if (s.Attendance == 1) presentCount++;
			else if (s.Attendance == 2) absentCount++;
		});

		var totalStudents = $scope.newExamAttendance.ExamAttendanceDetailsColl.length;
		var attendanceRate = totalStudents ? ((presentCount / totalStudents) * 100).toFixed(1) : 0;

		// Format AD date as yyyy-MM-dd for DB
		var formattedDate = null;
		if ($scope.newExamAttendance.ExamDateDet?.dateAD) {
			formattedDate = $filter('date')(
				new Date($scope.newExamAttendance.ExamDateDet.dateAD),
				'yyyy-MM-dd'
			);
		}
		var displayDate = $scope.newExamAttendance.ExamDateDet.dateBS;
		var examtypeText = $scope.ExamTypeList?.find(s => s.ExamTypeId === $scope.newExamAttendance.ExamTypeId)?.text || "-";
		var examshiftText = $scope.ExamShiftList?.find(s => s.ExamShiftId === $scope.newExamAttendance.ExamShiftId)?.Name || "-";
		var roomText = $scope.VacantRoomList?.find(s => s.RoomId === $scope.newExamAttendance.RoomId)?.Name || "-";
		Swal.fire({
			title: "<span style='font-size:18px;'>Please verify the following data before saving</span>",
			html: `
            <div style="text-align:center; line-height:1.5; font-family:Arial, sans-serif; font-size:13px;">
                <div style="margin-bottom:8px;">
                    <b>Date:</b> <span style="color:#007bff;">${displayDate}</span><br/>
                  
                    <b>Exam Type:</b> <span style="color:#007bff;">${examtypeText}</span><br/>
                    <b>Exam Shift:</b> <span style="color:#007bff;">${examshiftText}</span><br/>
                    <b>Room Name:</b> <span style="color:#007bff;">${roomText}</span><br/>
                </div>
                <div style="display:flex; justify-content:center; gap:10px; margin-top:8px;">
                    <div style="background:#28a745; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                        <b>Present</b><br/>${presentCount}
                    </div>
                    <div style="background:#dc3545; color:white; padding:6px 10px; border-radius:6px; min-width:70px; font-size:12px;">
                        <b>Absent</b><br/>${absentCount}
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
				$scope.newExamAttendance.ExamAttendanceDetailsColl.forEach(function (s) {
					dtColl.push({
						ExamDate: formattedDate,
						ExamTypeId: $scope.newExamAttendance.ExamTypeId,
						ExamShiftId: $scope.newExamAttendance.ExamShiftId,
						RoomId: $scope.newExamAttendance.RoomId,
						StudentId: s.StudentId,

						Attendance: s.Attendance,
						Remarks: s.Remarks
					});
				});
				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/SaveUpdateExamRoomAttendance",
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
					Swal.fire("Error", "Failed to save Exam attendance", "error");
				});

			} else {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
			}
		});
	};


	$scope.DelExamAttendance = function () {
		// Validate required fields
		if (!$scope.newExamAttendance.ExamTypeId) {
			Swal.fire('Please select Exam Type.');
			return;
		}
		if (!$scope.newExamAttendance.ExamShiftId) {
			Swal.fire('Please select Exam Shift.');
			return;
		}
		if (!$scope.newExamAttendance.RoomId) {
			Swal.fire('Please select Room.');
			return;
		}
		if (!$scope.newExamAttendance.ExamDate_TMP) {
			Swal.fire('Please select Exam Date.');
			return;
		}

		// Get names for confirmation message
		var examShiftName = ($scope.ExamShiftList.find(e => e.ExamShiftId === $scope.newExamAttendance.ExamShiftId) || {}).Name || '';
		var roomName = ($scope.VacantRoomList.find(r => r.RoomId === $scope.newExamAttendance.RoomId) || {}).Name || '';
		var examDate = $filter('date')($scope.newExamAttendance.ExamDate_TMP, 'yyyy-MM-dd');

		Swal.fire({
			title: '<span style="color:#d33;">Confirm Deletion</span>',
			html: `
            <p style="font-size:16px;">You are about to delete the <b>Exam Attendance</b> record with the following details:</p>
            <ul style="text-align:left; font-size:15px; line-height:1.6;">
                <li><b>Exam Type:</b> ${($scope.ExamTypeList.find(e => e.id === $scope.newExamAttendance.ExamTypeId) || {}).text || ''}</li>
                <li><b>Exam Shift:</b> ${examShiftName}</li>
                <li><b>Room:</b> ${roomName}</li>
                <li><b>Date:</b> ${examDate}</li>
            </ul>
            <p style="color:#d33; font-weight:bold;">This action cannot be undone.</p>
        `,
			icon: 'warning',
			showCancelButton: true,
			confirmButtonText: '<i class="fas fa-trash-alt"></i> Delete',
			cancelButtonText: '<i class="fas fa-times"></i> Cancel',
			focusConfirm: false,
			reverseButtons: true,
		}).then((result) => {
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ExamTypeId: $scope.newExamAttendance.ExamTypeId,
					ExamShiftId: $scope.newExamAttendance.ExamShiftId,
					RoomId: $scope.newExamAttendance.RoomId,
					ExamDate: $filter('date')($scope.newExamAttendance.ExamDateDet.dateAD, 'yyyy-MM-dd')
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DeleteExamRoomWiseAttendance",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";

					if (res.data.IsSuccess) {
						Swal.fire('Deleted!', res.data.ResponseMSG, 'success');
						// Refresh the list after delete
						$scope.GetExamRoomWiseStudentList();
					} else {
						Swal.fire('Error!', res.data.ResponseMSG, 'error');
					}

				}, function (reason) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire('Failed', 'Request failed: ' + reason, 'error');
				});
			}
		});
	};


	//************************* Bulk Attendance *********************************

	$scope.IsValidBulkAttendance = function () {
		if (!$scope.newBulkAttendance.ExamTypeId) {
			Swal.fire('Please ! Select ExamType');
			return false;
		}

		if (!$scope.newBulkAttendance.SelectedClass) {
			Swal.fire('Please ! Select Class Name');
			return false;
		}

		return true;
	}

	$scope.ChangeWorkingDays = function () {

		var wd = $scope.newBulkAttendance.WorkingDays;
		angular.forEach($scope.newBulkAttendance.StudentList, function (st) {
			st.WorkingDays = wd;
			st.AbsentDays = st.WorkingDays - st.PresentDays;
		});
	};

	$scope.ChangePADays = function (pa, st) {
		if (pa == 1) {
			st.AbsentDays = st.WorkingDays - st.PresentDays;
		} else if (pa == 2)
			st.PresentDays = st.WorkingDays - st.AbsentDays;
	}

	$scope.GetClassWiseStudentBA = function () {

		$scope.newBulkAttendance.StudentList = [];
		if ($scope.newBulkAttendance.SelectedClass) {
			var para = {
				ClassId: $scope.newBulkAttendance.SelectedClass.ClassId,
				SectionIdColl: $scope.newBulkAttendance.SelectedClass.SectionId.toString(),
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetClassWiseStudentList",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {
				if (res1.data.IsSuccess && res1.data.Data) {
					$scope.newBulkAttendance.StudentList = res1.data.Data;
					angular.forEach($scope.newBulkAttendance.StudentList, function (st) {
						st.WorkingDays = 0;
						st.PresentDays = 0;
						st.AbsentDays = 0;
						st.Remarks = '';
					});
					if ($scope.newBulkAttendance.WorkingDays > 0) {
						$scope.ChangeWorkingDays();
					}
					$timeout(function () {

						if ($scope.newBulkAttendance.ExamTypeId) {
							var para1 = {
								ClassId: $scope.newBulkAttendance.SelectedClass.ClassId,
								SectionId: $scope.newBulkAttendance.SelectedClass.SectionId,
								ExamTypeId: $scope.newBulkAttendance.ExamTypeId
							};

							$http({
								method: 'POST',
								url: base_url + "Exam/Transaction/GetBulkAttendanceById",
								dataSchedule: "json",
								data: JSON.stringify(para1)
							}).then(function (res2) {
								if (res2.data.IsSuccess && res2.data.Data) {
									var symDataColl = mx(res2.data.Data);

									if (symDataColl) {
										var fData = symDataColl.firstOrDefault();
										/*	if (fData) {*/
										//$scope.newBulkAttendance.WorkingDays = fData.WorkingDays;

										//if(fData.DateFrom)
										//	$scope.newBulkAttendance.FromDate_TMP = new Date(fData.DateFrom);

										//if(fData.DateTo)
										//	$scope.newBulkAttendance.ToDate_TMP =new Date(fData.DateTo);
										$scope.newBulkAttendance.WorkingDays = (fData && fData.WorkingDays != null) ? fData.WorkingDays : 0;
										$scope.newBulkAttendance.FromDate_TMP = (fData && fData.DateFrom) ? new Date(fData.DateFrom) : null;
										$scope.newBulkAttendance.ToDate_TMP = (fData && fData.DateTo) ? new Date(fData.DateTo) : null;


										/*}*/
										angular.forEach($scope.newBulkAttendance.StudentList, function (st) {

											var dt = symDataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
											if (dt) {
												//st.WorkingDays = dt.WorkingDays;
												//st.PresentDays = dt.PresentDays;
												//st.AbsentDays = dt.AbsentDays;
												//st.Remarks = dt.Remarks;
												st.WorkingDays = (dt.WorkingDays != null) ? dt.WorkingDays : $scope.newBulkAttendance.WorkingDays;
												st.PresentDays = dt.PresentDays || 0;
												st.AbsentDays = dt.AbsentDays || (st.WorkingDays - st.PresentDays);
												st.Remarks = dt.Remarks || '';
											}
										});

									}


								} else {
									Swal.fire(res2.data.ResponseMSG);
								}
							});
						}

					});


				} else {
					Swal.fire(res1.data.ResponseMSG);
				}
			});
		}

	};

	$scope.ImportAttendance = function () {

		if ($scope.newBulkAttendance.SelectedClass) {

			var para1 = {
				ClassId: $scope.newBulkAttendance.SelectedClass.ClassId,
				SectionId: $scope.newBulkAttendance.SelectedClass.SectionId,
				DateFrom: $filter('date')(new Date($scope.newBulkAttendance.FromDateDet.dateAD), 'yyyy-MM-dd'),
				DateTo: $filter('date')(new Date($scope.newBulkAttendance.ToDateDet.dateAD), 'yyyy-MM-dd')
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetAttendanceForExam",
				dataSchedule: "json",
				data: JSON.stringify(para1)
			}).then(function (res2) {
				if (res2.data.IsSuccess && res2.data.Data) {
					var symDataColl = mx(res2.data.Data);

					if (symDataColl) {
						var fData = symDataColl.firstOrDefault();
						if (fData) {
							$scope.newBulkAttendance.WorkingDays = fData.WorkingDays;
						}
						angular.forEach($scope.newBulkAttendance.StudentList, function (st) {

							var dt = symDataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
							if (dt) {
								st.WorkingDays = dt.WorkingDays;
								st.PresentDays = dt.PresentDays;
								st.AbsentDays = dt.AbsentDays;
								//st.Remarks = dt.Remarks;
							}
						});

					}


				} else {
					Swal.fire(res2.data.ResponseMSG);
				}
			});
		}

	};

	$scope.SaveUpdateBulkAttendance = function () {
		if ($scope.IsValidBulkAttendance() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBulkAttendance.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBulkAttendance();
					}
				});
			} else
				$scope.CallSaveUpdateBulkAttendance();

		}
	};

	$scope.CallSaveUpdateBulkAttendance = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newBulkAttendance.FromDateDet) {
			$scope.newBulkAttendance.FromDate = $filter('date')(new Date($scope.newBulkAttendance.FromDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newBulkAttendance.FromDate = null;


		if ($scope.newBulkAttendance.ToDateDet) {
			$scope.newBulkAttendance.ToDate = $filter('date')(new Date($scope.newBulkAttendance.ToDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newBulkAttendance.ToDate = null;


		var tmpData = [];
		var dateFrom = $scope.newBulkAttendance.FromDate;
		var dateTo = $scope.newBulkAttendance.ToDate;

		angular.forEach($scope.newBulkAttendance.StudentList, function (st) {

			var newData = {
				StudentId: st.StudentId,
				ExamTypeId: $scope.newBulkAttendance.ExamTypeId,
				WorkingDays: st.WorkingDays,
				PresentDays: st.PresentDays,
				AbsentDays: st.AbsentDays,
				Remarks: st.Remarks,
				DateFrom: dateFrom,
				DateTo: dateTo,
			}
			tmpData.push(newData);
		});

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveBulkAttendance",
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
				$scope.ClearBulkAttendance();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	$scope.DelBulkAttendanceById = function (refData) {

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
					BulkAttendanceId: refData.BulkAttendanceId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelBulkAttendance",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBulkAttendanceList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Exam Type Summary *********************************

	//$scope.IsValidExamTypeSummary = function () {
	//	if ($scope.newExamTypeSummary.Exam.isEmpty()) {
	//		Swal.fire('Please ! Select Exam Type');
	//		return false;
	//	}

	//	return true;
	//}

	$scope.SaveUpdateExamTypeSummary = function () {
		if ($scope.IsValidExamTypeSummary() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamTypeSummary.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamTypeSummary();
					}
				});
			} else
				$scope.CallSaveUpdateExamTypeSummary();

		}
	};

	$scope.CallSaveUpdateExamTypeSummary = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamTypeSummary",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamTypeSummary }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExamTypeSummary();
				$scope.GetAllExamTypeSummaryList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllExamTypeSummaryList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamTypeSummaryList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamTypeSummaryList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamTypeSummaryList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExamTypeSummaryById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ExamTypeSummaryId: refData.ExamTypeSummaryId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamTypeSummaryById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExamTypeSummary = res.data.Data;
				$scope.newExamTypeSummary.Mode = 'Modify';

				//document.getElementById('batch-section').style.display = "none";
				//document.getElementById('batch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExamTypeSummaryById = function (refData) {

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
					ExamTypeSummaryId: refData.ExamTypeSummaryId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelExamTypeSummary",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExamTypeSummaryList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Exam Type Group *********************************

	//$scope.IsValidExamTypeGroup = function () {
	//	if ($scope.newExamTypeGroup.Exam.isEmpty()) {
	//		Swal.fire('Please ! Select Exam Type Group');
	//		return false;
	//	}

	//	return true;
	//}

	$scope.SaveUpdateExamTypeGroup = function () {
		if ($scope.IsValidExamTypeGroup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamTypeGroup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamTypeGroup();
					}
				});
			} else
				$scope.CallSaveUpdateExamTypeGroup();

		}
	};

	$scope.CallSaveUpdateExamTypeGroup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamTypeGroup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamTypeGroup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExamTypeGroup();
				$scope.GetAllExamTypeGroupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllExamTypeGroupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamTypeGroupList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamTypeGroupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamTypeGroupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExamTypeGroupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ExamTypeGroupId: refData.ExamTypeGroupId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamTypeGroupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExamTypeGroup = res.data.Data;
				$scope.newExamTypeGroup.Mode = 'Modify';

				//document.getElementById('board-section').style.display = "none";
				//document.getElementById('board-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExamTypeGroupById = function (refData) {

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
					ExamTypeGroupId: refData.ExamTypeGroupId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelExamTypeGroup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExamTypeGroupList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//*************************Parent Exam Group *********************************

	//$scope.IsValidParentExamGroup = function () {
	//	if ($scope.newParentExamGroup.Exam.isEmpty()) {
	//		Swal.fire('Please ! Select Exam Type Group');
	//		return false;
	//	}

	//	return true;
	//}

	$scope.SaveUpdateParentExamGroup = function () {
		if ($scope.IsValidParentExamGroup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newParentExamGroup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateParentExamGroup();
					}
				});
			} else
				$scope.CallSaveUpdateParentExamGroup();

		}
	};

	$scope.CallSaveUpdateParentExamGroup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveParentExamGroup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newParentExamGroup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearParentExamGroup();
				$scope.GetAllParentExamGroupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllParentExamGroupList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ParentExamGroupList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllParentExamGroupList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ParentExamGroupList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetParentExamGroupById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ParentExamGroupId: refData.ParentExamGroupId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetParentExamGroupById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newParentExamGroup = res.data.Data;
				$scope.newParentExamGroup.Mode = 'Modify';

				//document.getElementById('board-section').style.display = "none";
				//document.getElementById('board-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelParentExamGroupById = function (refData) {

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
					ParentExamGroupId: refData.ParentExamGroupId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelParentExamGroup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllParentExamGroupList();
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


	$scope.GetClassWiseStudentMV = function () {

		$scope.newBlock.StudentList = [];
		if ($scope.newBlock.SelectedClass && $scope.newBlock.ExamTypeId) {
			var para = {
				ClassId: $scope.newBlock.SelectedClass.ClassId,
				SectionId: $scope.newBlock.SelectedClass.SectionId,
				ExamTypeId: $scope.newBlock.ExamTypeId
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetExamWiseBlockedMarkSheetById",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res1) {
				if (res1.data.IsSuccess && res1.data.Data) {
					$scope.newBlock.StudentList = res1.data.Data;

				} else {
					Swal.fire(res1.data.ResponseMSG);
				}
			});
		}

	};
	$scope.ClearExamWiseBlocked = function () {

		$timeout(function () {
			$scope.newBlock = {
				SelectedClass: null,
				ExamTypeId: null,
				Mode: 'Save'
			};
		});
	};
	$scope.SaveUpdateExamWiseBlocked = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();


		var tmpData = [];

		angular.forEach($scope.newBlock.StudentList, function (st) {

			if (st.IsBlocked == true) {
				tmpData.push(st);
			}

		});

		if (tmpData.length == 0) {

			var f = $scope.newBlock.StudentList[0];

			tmpData.push({
				ClassId: f.ClassId,
				SectionId: f.SectionId,
				ExamTypeId: f.ExamTypeId,
				StudenId: 0,
				Message: '',
				IsBlocked: false
			});
		}

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamWiseBlockedMarkSheet",
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


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}
	$scope.sort1 = function (keyname) {
		$scope.sortKey1 = keyname;   //set the sortKey to the param passed
		$scope.reverse1 = !$scope.reverse1; //if true make it false and vice versa
	}
	$scope.sort2 = function (keyname) {
		$scope.sortKey2 = keyname;   //set the sortKey to the param passed
		$scope.reverse2 = !$scope.reverse2; //if true make it false and vice versa
	}
	$scope.sort3 = function (keyname) {
		$scope.sortKey3 = keyname;   //set the sortKey to the param passed
		$scope.reverse3 = !$scope.reverse3; //if true make it false and vice versa
	}
	$scope.sort4 = function (keyname) {
		$scope.sortKey4 = keyname;   //set the sortKey to the param passed
		$scope.reverse4 = !$scope.reverse4; //if true make it false and vice versa
	}

	$scope.validateDates = function (changedField) {
		if (!$scope.newBulkAttendance.FromDateDet || !$scope.newBulkAttendance.ToDateDet ||
			!$scope.newBulkAttendance.FromDateDet.dateAD || !$scope.newBulkAttendance.ToDateDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newBulkAttendance.FromDateDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newBulkAttendance.ToDateDet.dateAD), 'yyyy-MM-dd')
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newBulkAttendance.FromDate_TMP = new Date();
						$scope.newBulkAttendance.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newBulkAttendance.ToDate_TMP = new Date();
						$scope.newBulkAttendance.ToDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};

});