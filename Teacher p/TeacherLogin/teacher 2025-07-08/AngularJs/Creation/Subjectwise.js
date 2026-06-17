
String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};

app.controller('SubjectwiseController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Student Attendance Subjectwise';

	$scope.LoadData = function () {


		$scope.V = {};

		$scope.ClassColl = [];
		$scope.SectionColl = [];
		$http.get(base_url + "StudentAttendance/Creation/GetClassSection")
			.then(function (data) {
				$scope.ClassColl = data.data.ClassList;
				$scope.SectionColl = data.data.SectionList;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				alert("Couldn't find data");
			});
		////Calling Subject List
		//$scope.SubjectColl = [];
		//$http.get(base_url + "OnlineClass/Creation/GetSubjectList")
		//	.then(function (data) {
		//		$scope.SubjectColl = data.data;
		//	}, function (reason) {
		//		alert("Data not get");
		//	});



		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();



		$scope.currentPages = {
			AddAttendance: 1,
			SubjectwiseSummary: 1,
			AllSubjectSummary: 1

		};

		$scope.searchData = {
			AddAttendance: '',
			SubjectwiseSummary: '',
			AllSubjectSummary: ''

		};

		$scope.perPage = {
			AddAttendance: GlobalServices.getPerPageRow(),
			SubjectwiseSummary: GlobalServices.getPerPageRow(),
			AllSubjectSummary: GlobalServices.getPerPageRow(),

		};

		$scope.newAddAttendance = {
			AddAttendanceId: null,
			Mode: 'Save'
		};

		$scope.newSubjectwiseSummary = {
			SubjectwiseSummaryId: null,
			Mode: 'Save'
		};

		$scope.newAllSubjectSummary = {
			AllSubjectSummaryId: null,
			Mode: 'Save'
		};





		//$scope.GetAllAddAttendanceList();
		//$scope.GetAllAttendanceSummaryList();
		//$scope.GetAllAllClassSummaryList();



	}
	//$('.btn-toggle').on("change", function (e) {

	//});
	//$scope.togChange = function (e) {
	//	let c = $('#togBtn' + e);
	//	if ($(c).is(':checked')) {
	//		$(c).attr('value', 'true');
	//		$scope.Attendance = 1
	//		alert($scope.Attendance)
	//	}
	//	else {
	//		$(c).attr('value', 'false');
	//		$scope.Attendance = 2
	//		alert($scope.Attendance)
	//	}
	//   }
	//$(".togBtn").on('change', function () {
	//	if ($(this).is(':checked')) {
	//		$(this).attr('value', 'true');
	//		$scope.Attendance=1
	//	}
	//	else {
	//		$(this).attr('value', 'false');
	//		$scope.Attendance = 2
	//	}
	//});
	$scope.OnChangeDate = function () {
		if ($scope.newAddAttendance.forDate) {
			$scope.SubjectId = $scope.newAddAttendance.SubjectId;
			var res = $scope.newAddAttendance.forDate.split("-");
			$scope.ResultDate = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
			$scope.forDate = $scope.ResultDate.year + '-' + $scope.ResultDate.month + '-' + $scope.ResultDate.day;
			//$scope.ResultDate = NepaliFunctions.BS2AD({ year: 2058, month: 2, day: 19 })

		}
	}
	$('#cboSectionId').on("change", function (e) {

		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.Value = JSON.parse(select_val)
		$scope.inOutMode = 3;
		$scope.para = {
			ClassId: $scope.Value.ClassId,
			//SectionIdColl: ($scope.Value.SectionId && $scope.Value.SectionId.length > 0 ? $scope.SectionId.toString() : '')
			SectionId: $scope.Value.SectionId || null,
		};
		$scope.GetSubjectList();
		//$scope.GetAllAddAttendance11($scope.Value, $scope.forDate, $scope.inOutMode, $scope.SubjectId);
	});

	$('#cboSubjectId').on("change", function (e) {
		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.SubjectId = select_val;
		$scope.GetAllAddAttendance11($scope.Value, $scope.forDate, $scope.inOutMode, $scope.SubjectId);

	});


	$scope.ClearAddAttendance = function () {
		$scope.newAddAttendance = {
			AddAttendanceId: null,
			Mode: 'Save'
		};
	}

	$scope.ClearSubjectwiseSummary = function () {
		$scope.newSubjectwiseSummary = {
			SubjectwiseSummaryId: null,
			Mode: 'Save'
		};
	}

	$scope.ClearAllSubjectSummary = function () {
		$scope.newAllSubjectSummary = {
			AllSubjectSummaryId: null,
			Mode: 'Save'
		};
	}



	$scope.GetAllAddAttendance11 = function (Value, forDate, inOutMode, SubjectId) {
		$scope.V.ClassId = Value.ClassId;
		$scope.V.SectionId = Value.SectionId || null;
		$scope.V.inOutMode = inOutMode;
		$scope.V.forDate = forDate;
		$scope.V.subjectId = SubjectId;
		$scope.SubWiseStdColl = [];
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AddAttendanceList = [];
		if ($scope.V.forDate && $scope.V.ClassId /*&& $scope.V.SectionId*/) {
			$http({
				method: 'POST',
				url: base_url + "StudentRecord/Creation/GetClassWiseStudentList",
				data: $scope.para,
				dataType: "json"
			}).then(function (res) {
				hidePleaseWait();
				if (res.data && res.data) {
					$scope.ClassWiseStudentList = res.data;
					//$scope.v=res.data;

					$http({
						method: 'POST',
						url: base_url + "StudentAttendance/Creation/GetAllSubjectWiseAttendance",
						data: $scope.V,
						dataType: "json"
					}).then(function (res) {
						hidePleaseWait();
						//$scope.loadingstatus = "stop";
						if (res.data && res.data) {
							$scope.AddAttendanceList = res.data;
							//$scope.v=res.data;


							if ($scope.ClassWiseStudentList && $scope.AddAttendanceList) {
								var attendanceColl = mx($scope.AddAttendanceList);

								angular.forEach($scope.ClassWiseStudentList, function (st) {
									var att = attendanceColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
									st.ForDate = $scope.V.forDate;
									st.Attendance = (att ? att.Attendance : null);
									if (st.Attendance == null) {
										st.Attendance = false    //For present
									}
									else if (st.Attendance == 1) {
										st.Attendance = false     //For present
									}
									else if (st.Attendance == 2) {
										st.Attendance = true     //For absemt
									}
									st.Leave = false;
									st.LateMin = (att ? att.LateMin : 0);
									if (att) {
										if (att.LateMin == 0) {
											st.IsLate = false;
										}
										else {
											st.IsLate = true;
										}
									}
									else {
										st.IsLate = false;
									}
									st.Remarks = (att ? att.Remarks : '');
									st.InOutMode = $scope.V.inOutMode;
									st.ClassId = $scope.V.ClassId;
									st.SectionId = $scope.V.SectionId || null;
									st.SubjectId = $scope.V.subjectId;
									$scope.SubWiseStdColl.push(st);
								});

							} else {
								Swal.fire(res.data.ResponseMSG);
							}

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


	$scope.GetAllAddAttendance = function (Value, forDate, inOutMode, SubjectId) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AddAttendanceList = [];

		$scope.V.ClassId = Value.ClassId;
		$scope.V.SectionId = Value.SectionId || null;
		$scope.V.inOutMode = inOutMode;
		$scope.V.forDate = forDate;
		$scope.V.subjectId = SubjectId;

		$http({
			method: 'POST',
			url: base_url + "StudentAttendance/Creation/GetAllSubjectWiseAttendance",
			data: $scope.V,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data && res.data) {
				$scope.AddAttendanceList = res.data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//*************************AddAttendance *********************************

	//*************************AttendanceSummary *********************************





	//*************************AllClassSummary *********************************



	$scope.Callfunction = function () {
		$scope.DataColl = [];
		angular.forEach($scope.SubWiseStdColl, function (Det) {
			if (Det.Attendance == false) {
				Det.Attendance = 1
			}
			else {
				Det.Attendance = 2
			}
			if (Det.LateMin > 0) {
				Det.Attendance = 3
			}
			if (Det.Leave == true) {
				Det.Attendance = 4
			}
			var MapData = {
				ClassId: Det.ClassId,
				SectionId: Det.SectionId || null,
				ForDate: Det.ForDate,
				SubjectId: Det.SubjectId,
				InOutMode: Det.InOutMode,
				StudentId: Det.StudentId,
				Attendance: Det.Attendance,
				LateMin: Det.LateMin,
				Remarks: Det.Remarks,

			}

			$scope.DataColl.push(MapData);

		});
		if ($scope.DataColl) {
			$scope.SaveSubjectWiseAttendance($scope.DataColl);
		}

	}




	$scope.SaveSubjectWiseAttendance = function (CollData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "StudentAttendance/Creation/SaveSubjectWiseAttendance",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: CollData }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();


			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				//$scope.ClearExamSetup();
				//$scope.GetAllExamSetupList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};






	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};
	$scope.GetSubjectList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SubjectColl = [];
		if ($scope.para) {
			var para = {
				classId: $scope.para.ClassId,
				sectionId: $scope.para.SectionId,
			};
			$http({
				method: 'POST',
				url: base_url + "OnlineClass/Creation/GetSubjectList",
				dataType: "json",
				data: para
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data) {
					$scope.SubjectColl = res.data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}





	}

});