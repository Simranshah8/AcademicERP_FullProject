
app.controller('ClasswiseController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Student Attendance Classwise';




	$scope.LoadData = function () {

		//$("#togBtn").on('change', function () {
		//	if ($(this).is(':checked')) {
		//		$(this).attr('value', 'true');
		//	}
		//	else {
		//		$(this).attr('value', 'false');
		//	}
		//});


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




		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();




		$scope.currentPages = {
			AddAttendance: 1,
			AttendanceSummary: 1,
			AllClassSummary: 1

		};

		$scope.searchData = {
			AddAttendance: '',
			AttendanceSummary: '',
			AllClassSummary: ''

		};

		$scope.perPage = {
			AddAttendance: GlobalServices.getPerPageRow(),
			AttendanceSummary: GlobalServices.getPerPageRow(),
			AllClassSummary: GlobalServices.getPerPageRow(),

		};

		$scope.newAddAttendance = {
			AddAttendanceId: null,
			forDate: new Date(),
			Mode: 'Save'
		};

		$scope.newAttendanceSummary = {
			AttendanceSummaryId: null,
			Mode: 'Save'
		};

		$scope.newAllClassSummary = {
			AllClassSummaryId: null,
			Mode: 'Save'
		};





		//$scope.GetAllAddAttendanceList();
		//$scope.GetAllAttendanceSummaryList();
		//$scope.GetAllAllClassSummaryList();



	}
	$('#cboSectionId').on("change", function (e) {

		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.Value = JSON.parse(select_val)
		$scope.para = {
			ClassId: $scope.Value.ClassId,
			//SectionIdColl: ($scope.Value.SectionId && $scope.Value.SectionId.length > 0 ? $scope.SectionId.toString() : '')
			SectionId: $scope.Value.SectionId || null,
		};
		$scope.newAddAttendance.forDate;
		if ($scope.newAddAttendance.forDateDet.dateBS && $scope.Value.ClassId > 0 /*&& $scope.Value.SectionId > 0*/) {
			var res = $scope.newAddAttendance.forDateDet.dateBS.split("-");
			$scope.ResultDate = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
			$scope.forDate = $scope.ResultDate.year + '-' + $scope.ResultDate.month + '-' + $scope.ResultDate.day;
			//$scope.ResultDate = NepaliFunctions.BS2AD({ year: 2058, month: 2, day: 19 })
			//$scope.V = $scope.Value;
			//$scope.V = $scope.forDate;
			$scope.V = {
				classId: $scope.Value.ClassId,
				sectionId: $scope.Value.SectionId || null,
				inOutMode: 3,
				forDate: $scope.forDate
			};
			$scope.GetAllAddAttendance();
		}

	});
	$scope.OnChangeDate = function () {

		$scope.V = {};
		if ($scope.newAddAttendance.forDate && $scope.Value.ClassId > 0 /*&& $scope.Value.SectionId > 0*/) {
			var res = $scope.newAddAttendance.forDate.split("-");
			$scope.ResultDate = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
			$scope.forDate = $scope.ResultDate.year + '-' + $scope.ResultDate.month + '-' + $scope.ResultDate.day;
			//$scope.ResultDate = NepaliFunctions.BS2AD({ year: 2058, month: 2, day: 19 })
			//$scope.V = $scope.Value;
			//$scope.V = $scope.forDate;
			$scope.V = {
				classId: $scope.Value.ClassId,
				sectionId: $scope.Value.SectionId || null,
				inOutMode: 3,
				forDate: $scope.forDate
			};
			$scope.GetAllAddAttendance();
		} else {
			alert('Date or Class is missing')
		}

	}

	//$scope.OnChangeDate = function () {
	//	if ($scope.newAddAttendance.forDate && $scope.Value.ClassId > 0 && $scope.Value.SectionId > 0) {
	//		var res = $scope.newAddAttendance.forDate.split("-");
	//		$scope.ResultDate = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
	//		$scope.forDate = $scope.ResultDate.year + '-' + $scope.ResultDate.month + '-' + $scope.ResultDate.day;
	//		//$scope.ResultDate = NepaliFunctions.BS2AD({ year: 2058, month: 2, day: 19 })
	//		$scope.V = $scope.Value;
	//		$scope.V = $scope.forDate;
	//		$scope.V = $scope.inOutMode;
	//		$scope.GetAllAddAttendance();
	//	}

	//}


	$scope.GetAllAddAttendance = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AddAttendanceList = [];
		if ($scope.newAddAttendance.forDate && $scope.Value.ClassId /*&& $scope.Value.SectionId*/) {
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
						url: base_url + "StudentAttendance/Creation/GetAllClassWiseAttendance",
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
									st.Remarks = (att ? att.Remarks : '');
									st.InOutMode = $scope.V.InOutMode;
									if (att) {
										if (att.LateMin == 0) {
											st.IsLate = false;
										}
										else {
											st.IsLate = true;
										}
										if (att.Attendance == 4) {
											st.Leave = true;
										}
									}
									else {
										st.IsLate = false;
									}
									st.ClassId = $scope.V.classId;
									st.SectionId = $scope.V.sectionId;

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



	//$('#cboSectionId').on("change", function (e) {

	//	var selected_element = $(e.currentTarget);
	//	var select_val = selected_element.val();
	//	$scope.Value = JSON.parse(select_val)
	//	$scope.inOutMode = 3;
	//	$scope.OnChangeDate = function (forDate) {
	//		if ($scope.newAddAttendance.forDate) {
	//			var res = $scope.newAddAttendance.forDate.split("-");
	//			$scope.ResultDate = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
	//			$scope.forDate = $scope.ResultDate.year + '-' + $scope.ResultDate.month + '-' + $scope.ResultDate.day;
	//			//$scope.ResultDate = NepaliFunctions.BS2AD({ year: 2058, month: 2, day: 19 })
	//			$scope.GetAllAddAttendance($scope.Value, $scope.forDate, $scope.inOutMode);
	//		}
	//	}


	//});



	$scope.ClearAddAttendance = function () {
		$scope.newAddAttendance = {
			AddAttendanceId: null,
			Mode: 'Save'
		};
	}

	$scope.ClearAttendanceSummary = function () {
		$scope.newAttendanceSummary = {
			AttendanceSummaryId: null,
			Mode: 'Save'
		};
	}

	$scope.ClearAllClassSummary = function () {
		$scope.newAllClassSummary = {
			AllClassSummaryId: null,
			Mode: 'Save'
		};
	}



	$scope.classwisefn = function () {
		$scope.datacoll = [];

		angular.forEach($scope.ClassWiseStudentList, function (st) {
			if (st.Attendance == false) {
				st.Attendance = 1;
			}
			else {
				st.Attendance = 2;
			}
			if (st.LateMin > 0) {
				st.Attendance = 3
			}
			if (st.Leave == true) {
				st.Attendance = 4
			}
			var classwise = {
				Attendance: st.Attendance,
				IsLate: st.IsLate,
				LateMin: st.LateMin,
				Remarks: st.Remarks,
				StudentId: st.StudentId,
				InOutMode: 3,
				ForDate: st.ForDate,
				SectionId: st.SectionId || null,
				ClassId: st.ClassId
			}
			$scope.datacoll.push(classwise);
		});

		$scope.CallSaveUpdateclasswisefn($scope.datacoll);
	}


	$scope.CallSaveUpdateclasswisefn = function (datacoll) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "StudentAttendance/Creation/AddClassWiseAttendance",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", JSON.stringify(data.jsonData));

				return formData;
			},
			data: { jsonData: datacoll }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {

				$scope.GetAllAddAttendance();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	//*************************AddAttendance *********************************







	//*************************AttendanceSummary *********************************





	//*************************AllClassSummary *********************************














	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});