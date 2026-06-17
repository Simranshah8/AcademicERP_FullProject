app.controller('StudentAttendanceController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Student Attendance';

	var glbS = GlobalServices;
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();
		//$scope.LanguageColl = GlobalServices.getLangList();		

		$scope.currentPages = {
			ClasswiseAttendance: 1,
			SubjectwiseAttendance: 1,
		};

		$scope.searchData = {
			ClasswiseAttendance: '',
			SubjectwiseAttendance: '',
		};

		$scope.perPage = {
			ClasswiseAttendance: glbS.getPerPageRow(),
			SubjectwiseAttendance: glbS.getPerPageRow(),
		};

		$scope.ClassSectionList = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newSubjectAttendence = {
			SubjectList: [],
			FromDate_TMP:new Date(),
			ToDate_TMP: new Date(),
		};

		$scope.newClasswiseAttendance = {
			StudentList: []
		};
		$scope.newClassAttendence = {
			InitialDate_TMP: new Date()
		};
	};


	/************************Classwise Attendance ***************/

	$scope.GetDateWiseAttendance = function () {

		if (!$scope.newClassAttendence.SelectedClass) {
			Swal.fire('Please ! Select Valid Class Name');
			return;
		}

		$scope.newClassAttendence.SubjectList = [];
		$scope.newClassAttendence.StudentList = [];
		var para = {
			forDate: $filter('date')(new Date($scope.newClassAttendence.InitialDateDet.dateAD), 'yyyy-MM-dd'),
			classId: $scope.newClassAttendence.SelectedClass.ClassId,
			sectionId: $scope.newClassAttendence.SelectedClass.SectionId
		};

		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetDateWiseOnlineAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data) {
				var tmpDataColl = mx(res.data.Data);

				var finalColl = [];
				var subjectQuery = tmpDataColl.groupBy(t => t.SubjectName).toArray();
				var fiSNo = 1;
				angular.forEach(subjectQuery, function (f) {
					$scope.newClassAttendence.SubjectList.push(
						{
							id: f.elements[0].Period,
							text: (f.key ? f.key : '')
						});
				});

				var query = tmpDataColl.groupBy(t => t.StudentId).toArray();
				var nSNO = 1;
				angular.forEach(query, function (q) {
					var subData = mx(q.elements);
					var fst = subData.firstOrDefault();
					var beData = {
						SNo: nSNO,
						StudentId: fst.StudentId,
						RegNo: fst.RegNo,
						Name: fst.Name,
						RollNo: fst.RollNo,
						ClassName: fst.ClassName + ' ' + fst.SectionName,
						FatherName: fst.FatherName,
						SubjectDetailsColl: []
					};

					var totalP = 0;
					angular.forEach($scope.newClassAttendence.SubjectList, function (fi) {
						var find = subData.firstOrDefault(p1 => p1.SubjectName == fi.text);
						beData.SubjectDetailsColl.push({
							SubjectName: fi.text,
							Attendance: (find && find.JoinDateTime ? 'P' : 'A')
						});

						if (find && find.JoinDateTime)
							totalP++;
					});

					beData.TotalAttendance = totalP;
					finalColl.push(beData);
					nSNO++;
				});

				$scope.newClassAttendence.StudentList = finalColl;

			} else {
				Swal.fire(res.data);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	};


	$scope.GetAllClasswiseAttendanceList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ClasswiseAttendanceList = [];
		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetAllClasswiseAttendanceList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClasswiseAttendanceList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};


	/************************Subjectwise Attendance ***************/

	$scope.GetClassWiseSubMap = function () {

		$scope.newSubjectAttendence.SubjectList = [];


		if ($scope.newSubjectAttendence.SelectedClass) {

			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				ClassId: $scope.newSubjectAttendence.SelectedClass.ClassId,
				SectionIdColl: ($scope.newSubjectAttendence.SelectedClass ? $scope.newSubjectAttendence.SelectedClass.SectionId : '')
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

					$timeout(function () {
						angular.forEach(res.data.Data, function (sm) {
							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								$scope.newSubjectAttendence.SubjectList.push(subDet);
							}
						});

					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};


	$scope.GetSubjectWiseAttendance = function () {

		if (!$scope.newSubjectAttendence.SelectedClass) {
			Swal.fire('Please ! Select Valid Class Name');
			return;
		}

		if (!$scope.newSubjectAttendence.SubjectId) {
			Swal.fire('Please ! Select Valid Subject Name');
			return;
		}

		$scope.newSubjectAttendence.DateList = [];
		$scope.newSubjectAttendence.StudentList = [];
		var para = {
			fromDate: $filter('date')(new Date($scope.newSubjectAttendence.FromDateDet.dateAD), 'yyyy-MM-dd'),
			toDate: $filter('date')(new Date($scope.newSubjectAttendence.ToDateDet.dateAD), 'yyyy-MM-dd'),
			classId: $scope.newSubjectAttendence.SelectedClass.ClassId,
			sectionId: $scope.newSubjectAttendence.SelectedClass.SectionId,
			subjectId: $scope.newSubjectAttendence.SubjectId
		};

		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetSubjectWiseOnlineAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data) {
				var tmpDataColl = mx(res.data.Data);

				var finalColl = [];
				var subjectQuery = tmpDataColl.groupBy(t => t.ForDate_BS).toArray();
				var fiSNo = 1;
				angular.forEach(subjectQuery, function (f) {
					$scope.newSubjectAttendence.DateList.push(
						{
							id: f.elements[0].Period,
							text: (f.key ? f.key : ''),
							shorttext: f.key.toString().substring(0, 5),
							forDate: new Date(f.elements[0].ForDate_AD)
						});
				});

				var query = tmpDataColl.groupBy(t => t.StudentId).toArray();
				var nSNO = 1;
				angular.forEach(query, function (q) {
					var subData = mx(q.elements);
					var fst = subData.firstOrDefault();
					var beData = {
						SNo: nSNO,
						StudentId: fst.StudentId,
						RegNo: fst.RegNo,
						Name: fst.Name,
						RollNo: fst.RollNo,
						ClassName: fst.ClassName + ' ' + fst.SectionName,
						FatherName: fst.FatherName,
						SubjectDetailsColl: []
					};

					var totalP = 0;
					angular.forEach($scope.newSubjectAttendence.DateList, function (fi) {
						var find = subData.firstOrDefault(p1 => p1.ForDate_BS == fi.text);
						beData.SubjectDetailsColl.push({
							ForDate_BS: fi.text,
							Attendance: (find && find.JoinDateTime ? 'P' : 'A')
						});

						if (find && find.JoinDateTime)
							totalP++;
					});

					beData.TotalAttendance = totalP;

					var attPer = (totalP / beData.SubjectDetailsColl.length) * 100;


					beData.AttendancePer = attPer;
					beData.AbsentPer = (100 - attPer);
					finalColl.push(beData);
					nSNO++;
				});

				$scope.newSubjectAttendence.StudentList = finalColl;

			} else {
				Swal.fire(res.data);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


	};


	$scope.GetAllSubjectwiseAttendanceList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SubjectwiseAttendanceList = [];
		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetAllSubjectwiseAttendanceList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SubjectwiseAttendanceList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.validateDates = function (changedField) {
		if (!$scope.newSubjectAttendence.FromDateDet || !$scope.newSubjectAttendence.ToDateDet ||
			!$scope.newSubjectAttendence.FromDateDet.dateAD || !$scope.newSubjectAttendence.ToDateDet.dateAD) {
			return true;
		}
		var fromDate = $filter('date')(new Date($scope.newSubjectAttendence.FromDateDet.dateAD), 'yyyy-MM-dd')
		var toDate = $filter('date')(new Date($scope.newSubjectAttendence.ToDateDet.dateAD), 'yyyy-MM-dd')
		if (!fromDate || !toDate) return true;
		if (fromDate > toDate) {
			if (changedField === 'fromDate') {
				Swal.fire({
					icon: 'warning',
					text: 'From Date cannot be After To Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newSubjectAttendence.FromDate_TMP = new Date();
						$scope.newSubjectAttendence.FromDateDet = new Date();
					});
				});
			} else if (changedField === 'toDate') {
				Swal.fire({
					icon: 'warning',
					text: 'To Date cannot be Before From Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newSubjectAttendence.ToDate_TMP = new Date();
						$scope.newSubjectAttendence.ToDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};
});