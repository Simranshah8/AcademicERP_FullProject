String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};


app.controller('OnlineClassController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'OnlineClass';

	$scope.GetDateWiseAttendance = function ()
	{

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
			url: base_url + "OnlineClass/Creation/GetDateWiseAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data)
			{
				var tmpDataColl = mx(res.data);

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
						SNo:nSNO,
						StudentId: fst.StudentId,
						RegNo: fst.RegNo,
						Name: fst.Name,
						RollNo: fst.RollNo,
						ClassName: fst.ClassName + ' ' + fst.SectionName,
						FatherName:fst.FatherName,						
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
	$scope.GetSubjectListForAttendance = function () {
		$scope.loadingstatus = "running";
		$scope.SubjectCollForAttendance = [];
		if ($scope.newSubjectAttendence.SelectedClass)
		{			
			var para = {
				classId: $scope.newSubjectAttendence.SelectedClass.ClassId,
				sectionIdColl: $scope.newSubjectAttendence.SelectedClass.SectionId,
			};
			$http({
				method: 'POST',
				url: base_url + "OnlineClass/Creation/GetSubjectList",
				dataType: "json",
				data: para
			}).then(function (res) {
				$scope.loadingstatus = "stop";
				$timeout(function () {
					$scope.SubjectCollForAttendance = res.data;
				});				

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        }
		



	}

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
			url: base_url + "OnlineClass/Creation/GetSubjectWiseAttendance",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data) {
				var tmpDataColl = mx(res.data);

				var finalColl = [];
				var subjectQuery = tmpDataColl.groupBy(t => t.ForDate_BS).toArray();
				var fiSNo = 1;
				angular.forEach(subjectQuery, function (f) {
					$scope.newSubjectAttendence.DateList.push(
						{
							id: f.elements[0].Period,
							text: (f.key ? f.key : ''),
							shorttext:f.key.toString().substring(0, 5),
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
							Attendance:  (find && find.JoinDateTime ? 'P' : 'A')
						});

						if (find && find.JoinDateTime)
							totalP++;
					});

					beData.TotalAttendance = totalP;

					var attPer = (totalP / beData.SubjectDetailsColl.length)* 100;


					beData.AttendancePer = attPer;
					beData.AbsentPer =(100-attPer);
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
	OnClickDefault();
	$scope.LoadData = function () {
		
		
		$scope.newNotice = {};
		$scope.V = {};
		$scope.colors = ['#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)']

		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			PastClass: 1,
			PastClassPresent: 1,
			PastClassAbsent: 1,
			PastClassAbsent2nd: 1,
			OnlinePlatform: 1,
			ClassAttendence: 1,
			SubjectAttendence:1

		};

		$scope.searchData = {
			PastClass: '',
			PastClassPresent: '',
			PastClassAbsent: '',
			PastClassAbsent2nd: '',
			OnlinePlatform: '',
			ClassAttendence: '',
			SubjectAttendence:''
		};

		$scope.perPage = {
			PastClass: GlobalServices.getPerPageRow(),
			PastClassPresent: GlobalServices.getPerPageRow(),
			PastClassAbsent: GlobalServices.getPerPageRow(),
			PastClassAbsent2nd: GlobalServices.getPerPageRow(),
			OnlinePlatform: GlobalServices.getPerPageRow(),
			ClassAttendence: GlobalServices.getPerPageRow(),
			SubjectAttendence: GlobalServices.getPerPageRow()
		};

		$scope.newOnlineClass = {
			PlatformType: 1,
			Register: 'https://zoom.us/signup',
			UserName: '',
			Pwd: '',
			Link: ''
		};

		$scope.createClass = {
			ShiftId: null,
			ClassId: null,
			SectionId: null,
			SubjectId: null,
			Notes: ''
		};

		//new tab Class Attendence
		$scope.newClassAttendence = {
			ClassAttendenceId: null,
			InitialDate_TMP: null,

		};

		//Get class and Section List
		$scope.AllClassColl = [];
		$scope.SectionClassColl = [];
		$scope.ClassSectionList = [];
		$http.get(base_url + "StudentAttendance/Creation/GetClassSection")
			.then(function (data) {
				$scope.AllClassColl = data.data.ClassList;
				$scope.ClassSectionList = data.data.SectionList;
				$scope.SectionClassColl = mx(data.data.SectionList);
			}, function (reason) {
				alert("Couldn't find data");
			});

		$scope.ClassGroupList = [];
		$http.get(base_url + "StudentAttendance/Creation/GetClassGroupList")
			.then(function (res) {
				$scope.ClassGroupList = res.data;
			}, function (reason) {
				alert("Couldn't find data");
			});

		$scope.getAllOnlinePlatform();

		$scope.GetRunningClassList();
		$scope.GetColleRunningClasses();
		$scope.GetPassedClassList();
		$scope.GetPassedClassListDate();
		
	};

	$scope.getAllOnlinePlatform = function () {
		//Get Online Platform List
		$scope.OnlinePlatformColl = [];
		$http.get(base_url + "OnlineClass/Creation/GetOnlinePlatform")
			.then(function (data) {
				$scope.OnlinePlatformColl = data.data;
				$scope.OnChangeplatformtype();
			}, function (reason) {
				alert("Data not get");
			});
	};
	$scope.OnChangeplatformtype = function () {

		if ($scope.newOnlineClass.PlatformType == 1) {
			$scope.newOnlineClass.Register = 'https://zoom.us/signup'
		}
		else if ($scope.newOnlineClass.PlatformType == 2) {
			$scope.newOnlineClass.Register = 'https://apps.google.com/meet/'
		}
		else if ($scope.newOnlineClass.PlatformType == 3) {
			$scope.newOnlineClass.Register = 'https://www.microsoft.com/en-us/microsoft-teams/log-in'
		}
		var plt = mx($scope.OnlinePlatformColl).firstOrDefault(p1 => p1.PlatformType == $scope.newOnlineClass.PlatformType);
		if (plt) {
			$scope.newOnlineClass.Link = plt.Link
			$scope.newOnlineClass.Pwd = plt.Pwd
			$scope.newOnlineClass.UserName = plt.UserName
		}

	};


	$scope.Days = function (Det) {
		var CurrentDate = new Date(Det);
		var DayId = CurrentDate.getDay();
		var day;
		switch (DayId) {
			case 0:
				day = "Sunday";
				break;
			case 1:
				day = "Monday";
				break;
			case 2:
				day = "Tuesday";
				break;
			case 3:
				day = "Wednesday";
				break;
			case 4:
				day = "Thursday";
				break;
			case 5:
				day = "Friday";
				break;
			case 6:
				day = "Saturday";
		}
		return day;
	};
	$scope.IsValidOnlineClass = function () {
		if ($scope.newOnlineClass.UserName.isEmpty()) {
			Swal.fire('Please ! Enter UserName');
			return false;
		}
		if ($scope.newOnlineClass.Pwd.isEmpty()) {
			Swal.fire('Please ! Enter Password');
			return false;
		}
		if ($scope.newOnlineClass.Link.isEmpty()) {
			Swal.fire('Please ! Enter Meeting URL');
			return false;
		}
		return true;
	};
	$scope.SaveUpdateOnlineClass = function () {
		if ($scope.IsValidOnlineClass() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newOnlineClass.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateOnlineClass();
					}
				});
			} else
				$scope.CallSaveUpdateOnlineClass();

		}
	};
	$scope.CallSaveUpdateOnlineClass = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/AddOnlinePlatform",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newOnlineClass }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.getAllOnlinePlatform();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	//Clalling multiple time Running Class
	//setInterval(function ()
	//{

	//	$timeout(function () {
	//		$scope.RunningClassesList();
	//	}, 3000);

	//	$timeout(function () {
	//		$scope.GetColleRunningClasses();
	//	},4000);

	//	//$timeout(function () {
	//	//	$scope.GetPassedClassList();
	//	//}, 4000);

	//}, 5000);

	function OnClickDefault() {
		document.getElementById('show-attendance-part').style.display = "none";
		document.getElementById('area-after-platform-setup').style.display = "none";
		document.getElementById('attendance-completed-classes').style.display = "none";


		document.getElementById('back-todays-completed-class').onclick = function () {
			document.getElementById('area-after-platform-setup').style.display = "block";
			document.getElementById('attendance-completed-classes').style.display = "none";
		}

		//document.getElementById('todays-attendance').onclick = function () {

		//}

		document.getElementById('show-attendance-part').style.display = "none";
		document.getElementById('area-after-platform-setup').style.display = "none";

		document.getElementById('back-btn').onclick = function () {
			document.getElementById('listing part').style.display = "block";
			document.getElementById('show-attendance-part').style.display = "none";
		}

		document.getElementById('next-btn').onclick = function () {
			document.getElementById('area-after-platform-setup').style.display = "block";
			document.getElementById('setup-form').style.display = "none";
			$scope.getAllClassSchedule();
			$scope.GetRunningClassList();
		}

		document.getElementById('back-to-platform').onclick = function () {
			document.getElementById('area-after-platform-setup').style.display = "none";
			document.getElementById('setup-form').style.display = "block";
		}
		document.getElementById('back-to-platform1').onclick = function () {
			document.getElementById('area-after-platform-setup').style.display = "none";
			document.getElementById('setup-form').style.display = "block";
		}
	}
	$scope.get24hTime = function (str) {
		str = String(str).toLowerCase().replace(/\s/g, '');
		var has_am = str.indexOf('am') >= 0;
		var has_pm = str.indexOf('pm') >= 0;
		// first strip off the am/pm, leave it either hour or hour:minute
		str = str.replace('am', '').replace('pm', '');
		// if hour, convert to hour:00
		if (str.indexOf(':') < 0) str = str + ':00';
		// now it's hour:minute
		// we add am/pm back if striped out before 
		if (has_am) str += ' am';
		if (has_pm) str += ' pm';
		// now its either hour:minute, or hour:minute am/pm
		// put it in a date object, it will convert to 24 hours format for us 
		var d = new Date("1/1/2011 " + str);
		// make hours and minutes double digits
		var doubleDigits = function (n) {
			return (parseInt(n) < 10) ? "0" + n : String(n);
		};
		return doubleDigits(d.getHours()) + ':' + doubleDigits(d.getMinutes());
	}

	$scope.getAllClassSchedule = function () {

		$scope.ClassScheduleColl = [];
		$scope.ClassShiftColl = [];
		$scope.ClassColl = [];
		$scope.SectionColl = [];
		$scope.ClassScheduleQuery = null;
		$http.get(base_url + "OnlineClass/Creation/GetClassSchedule")
			.then(function (res) {
				$scope.ClassScheduleColl = res.data;
				$scope.ClassScheduleQuery = mx(res.data);

				$timeout(function () {
					$scope.getClassScheduleForCard();
				});

				$timeout(function () {
					var grp = $scope.ClassScheduleQuery
						.groupBy(t => ({ id: t.ShiftId, val: t.ShiftName }))   // group `key`
						.select(t => t.key)
						.toArray();

					angular.forEach(grp, function (sh) {
						$scope.ClassShiftColl.push({
							ShiftId: sh.id,
							Name: sh.val
						});
					});

					angular.forEach($scope.ClassScheduleColl, function (cs) {
						if (cs.ForType == "Running") 
							$scope.GetHostDet(cs);
										
					});

					$timeout(function () {
						$scope.OnShiftChange();
					});

					$timeout(function () {
						$scope.OnClassChange();
					});					

				});


			}, function (reason) {
				alert("Data not get");
			});
	};
	$scope.getClassScheduleForCard = function () {
		$scope.ClassScheduleCollList = [];
		var DayId = new Date().getDay();
		DayId++;
		var query = $scope.ClassScheduleQuery.where(p1 => p1.DayId == DayId);

		angular.forEach(query, function (st) {
			var classTime = new Date(moment().format("yyyy-MM-DD") + ' ' + $scope.get24hTime(st.StartTime));
			var classEndTime = new Date(moment().format("yyyy-MM-DD") + ' ' + $scope.get24hTime(st.EndTime));
			if (new Date() < classTime || classEndTime>new Date())
				$scope.ClassScheduleCollList.push(st)
		});
	};
	$scope.OnShiftChange = function () {
		$scope.ClassColl = [];
		$scope.SectionColl = [];
		$scope.SubjectColl = [];
		var grp = $scope.ClassScheduleQuery
			.where(p1 => p1.ShiftId == $scope.createClass.ShiftId)
			.groupBy(t => ({ id: t.ClassId, val: t.ClassName }))   // group `key`
			.select(t => t.key)
			.toArray();

		$timeout(function () {
			angular.forEach(grp, function (sh) {
				$scope.ClassColl.push({
					ClassId: sh.id,
					Name: sh.val
				});
			});
		});
		
	}
	$scope.OnClassChange = function () {
		$scope.SectionColl = [];
		$scope.SubjectColl = [];
		var secColl = $scope.SectionClassColl.where(p1 => p1.ClassId == $scope.createClass.ClassId);

		$timeout(function () {
			$scope.$apply(function () {
				angular.forEach(secColl, function (sh) {
					$scope.SectionColl.push(sh);
				});
			});
			
			$scope.GetSubjectList();
        })
		
	}
	$scope.GetSubjectList = function () {
		$scope.loadingstatus = "running";
		$scope.SubjectColl = [];
		var sIds = ($scope.createClass.SectionId ? $scope.createClass.SectionId.toString() : '');
		if ($scope.createClass.ClassId) {
			var para = {
				classId: $scope.createClass.ClassId,
				sectionIdColl: sIds
			};
			$http({
				method: 'POST',
				url: base_url + "OnlineClass/Creation/GetSubjectList",
				dataType: "json",
				data: para
			}).then(function (res) {
				$scope.loadingstatus = "stop";

				$timeout(function () {
					$scope.$apply(function () {
						$scope.SubjectColl = res.data;
					});
				});

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}



	}

	$scope.StartOnlineClass = function () {
		if ($scope.RunningClassesColl.length > 0) {


			Swal.fire({
				title: 'Do you want to end the class?',
				showCancelButton: true,
				confirmButtonText: 'End',
			}).then((result) => {
				/* Read more about isConfirmed, isDenied below */
				if (result.isConfirmed) {
					var endData = $scope.RunningClassesColl[0];
					$scope.EndOnlineClass(endData);
				}
			});

		}
		else {

			$scope.loadingstatus = "running";
			if ($scope.createClass && $scope.createClass.ShiftId && $scope.createClass.ClassId) {
				$scope.createClass.UserName = $scope.newOnlineClass.UserName;
				$scope.createClass.Pwd = $scope.newOnlineClass.Pwd;
				$scope.createClass.Link = $scope.newOnlineClass.Link;
				$scope.createClass.PlatformType = $scope.newOnlineClass.PlatformType;
				$scope.createClass.ClassShiftId = $scope.createClass.ShiftId;
				$scope.createClass.SectionIdColl = $scope.createClass.SectionId ? $scope.createClass.SectionId.toString() : '';

				$http({
					method: 'POST',
					url: base_url + "OnlineClass/Creation/StartOnlineClass",
					headers: { 'Content-Type': undefined },
					transformRequest: function (data) {

						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));

						return formData;
					},
					data: { jsonData: $scope.createClass }
				}).then(function (res) {
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess == true && res.data.RId > 0) {
						$scope.HostClass = null;
						Swal.fire({
							title: 'Online Class Started',
							//showCancelButton: true,
							confirmButtonText: 'OK',
						}).then((result) => {
							window.open($scope.newOnlineClass.Link, '_blank');
						});

						$scope.GetRunningClassList();
					} else {
						Swal.fire({
							title: res.data.ResponseMSG + 'Do you want to end the class?',
							showCancelButton: true,
							confirmButtonText: 'End',
						}).then((result) => {
							/* Read more about isConfirmed, isDenied below */
							if (result.isConfirmed) {
								var endData = {
									TranId: res.data.RId
								}
								$scope.EndOnlineClass(endData);
							}
						});

					}


				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";

				});

			}

		}

	};

	$scope.StartOnlineClassGroup = function () {
		if ($scope.RunningClassesColl.length > 0) {


			Swal.fire({
				title: 'Do you want to end the class?',
				showCancelButton: true,
				confirmButtonText: 'End',
			}).then((result) => {
				/* Read more about isConfirmed, isDenied below */
				if (result.isConfirmed) {
					var endData = $scope.RunningClassesColl[0];
					$scope.EndOnlineClass(endData);
				}
			});

		}
		else {

			$scope.loadingstatus = "running";
			if ($scope.createClass && $scope.createClass.SelectedClassGroup && $scope.GroupWiseHost == true)
			{
				var classGroup = $scope.createClass.SelectedClassGroup;

				$scope.createClass.ClassGroupId = classGroup.ClassGroupId;
				$scope.createClass.SubjectId = classGroup.SubjectId;

				$scope.createClass.UserName = $scope.newOnlineClass.UserName;
				$scope.createClass.Pwd = $scope.newOnlineClass.Pwd;
				$scope.createClass.Link = $scope.newOnlineClass.Link;
				$scope.createClass.PlatformType = $scope.newOnlineClass.PlatformType;
				$scope.createClass.ClassShiftId =null;
				$scope.createClass.SectionIdColl = '';
				$scope.createClass.ClassId = 0;

				$http({
					method: 'POST',
					url: base_url + "OnlineClass/Creation/StartOnlineClass",
					headers: { 'Content-Type': undefined },
					transformRequest: function (data) {

						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));

						return formData;
					},
					data: { jsonData: $scope.createClass }
				}).then(function (res) {
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess == true && res.data.RId > 0) {
						$scope.HostClass = null;
						Swal.fire({
							title: 'Online Class Started',
							//showCancelButton: true,
							confirmButtonText: 'OK',
						}).then((result) => {
							window.open($scope.newOnlineClass.Link, '_blank');
						});

						$scope.GetRunningClassList();
					} else {
						Swal.fire({
							title: res.data.ResponseMSG + 'Do you want to end the class?',
							showCancelButton: true,
							confirmButtonText: 'End',
						}).then((result) => {
							/* Read more about isConfirmed, isDenied below */
							if (result.isConfirmed) {
								var endData = {
									TranId: res.data.RId
								}
								$scope.EndOnlineClass(endData);
							}
						});

					}


				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";

				});

			}

		}

	};

	$scope.GetRunningClassList = function () {
		$scope.RunningClassesColl = [];
		$http.get(base_url + "OnlineClass/Creation/GetRunningClassesList")
			.then(function (data) {
				$scope.RunningClassesColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});
	}
	$scope.EndOnlineClass = function (refData) {

		refData.RId = refData.TranId;
		$scope.loadingstatus = "running";
		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/EndOnlineClass",
			data: refData,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.ResponseMSG == "Success") {
				alert('Ended successfully')
				$scope.GetRunningClassList();
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
	$scope.GetHostDet = function (df) {
		$scope.HostClass = null;
		var startTime = new Date(moment().format("yyyy-MM-DD") + ' ' + $scope.get24hTime(df.StartTime));
		var endTime = new Date(moment().format("yyyy-MM-DD") + ' ' + $scope.get24hTime(df.EndTime));
		var now = new Date();
		if (now >= startTime && now <= endTime) {
			$scope.HostClass = df;
			$scope.createClass.UserName = $scope.newOnlineClass.UserName;
			$scope.createClass.Pwd = $scope.newOnlineClass.Pwd;
			$scope.createClass.Link = $scope.newOnlineClass.Link;
			$scope.createClass.PlatformType = $scope.newOnlineClass.PlatformType;
			$scope.createClass.ClassShiftId = df.ShiftId;
			$scope.createClass.ShiftId = df.ShiftId;
			$scope.createClass.ClassId = df.ClassId;
			$scope.createClass.SubjectId = df.SubjectId;
			$scope.createClass.SectionIdColl = (df.SectionIdColl ? df.SectionIdColl : '');
			$scope.createClass.SectionId = (df.SectionIdColl ? df.SectionIdColl : '');

			
		} else {
			if (startTime > now) {
				Swal.fire('Please wait for schedule class time. If you want to host/start class now, please create class manually.');
			}
		}
	}

	$scope.GetColleRunningClasses = function () {
		$scope.ColleaguesClassesColl = [];
		$http.get(base_url + "OnlineClass/Creation/GetColleRunningClasses")
			.then(function (data) {
				$scope.ColleaguesClassesColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});
	}

	$scope.GetPassedClassList = function () {
		$scope.GetMissedClassList();
		$scope.loadingstatus = "running";
		$scope.PassClassesList = [];
		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetOnlineClassesList",
			dataType: "json",
			data: $scope.V
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				angular.forEach(res.data, function (st) {
					angular.forEach(st.DataColl, function (DT) {
						DT.absentNo = DT.NoOfStudent - DT.NoOfPresent;
						$scope.PassClassesList.push(DT);
					});
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.OnChangeDate = function () {

		if ($scope.newPastClass.FromDate_TMP && $scope.newPastClass.ToDate_TMP) {

			var res = $scope.newPastClass.FromDate_TMP.split("-");
			var resTo = $scope.newPastClass.ToDate_TMP.split("-");
			$scope.dateFrom = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
			$scope.dateTo = NepaliFunctions.BS2AD({ year: resTo[0], month: resTo[1], day: resTo[2] })
			$scope.V.dateFrom = $scope.dateFrom.year + '-' + $scope.dateFrom.month + '-' + $scope.dateFrom.day;
			$scope.V.dateTo = $scope.dateTo.year + '-' + $scope.dateTo.month + '-' + $scope.dateTo.day;

			$scope.GetPassedClassListDate();

		}

	};

	$scope.GetPassedClassListDate = function () {

		$scope.loadingstatus = "running";
		$scope.PassClassesListDate = [];
		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetOnlineClassesList",
			dataType: "json",
			data: $scope.V
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				angular.forEach(res.data, function (st) {
					angular.forEach(st.DataColl, function (DT) {
						DT.absentNo = DT.NoOfStudent - DT.NoOfPresent;
						DT.Present_Per = (DT.NoOfPresent / DT.NoOfStudent) * 100;
						$scope.PassClassesListDate.push(DT);
					});
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
		$scope.Days();
	}
	$scope.TodaysAttendence = function (Data) {

		$scope.SelectedData = Data
		var para = {
			tranId: Data.TranId
		};

		$scope.loadingstatus = "running";
		showPleaseWait();
		//$scope.TodaysOnlineClassAttByIdList = [];

		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetOnlineClassAttById",
			dataType: "json",
			data: para
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.TodayAbsentColl = res.data.AbsentColl;
				$scope.TodayPresentColl = res.data.PresentColl;



				document.getElementById('area-after-platform-setup').style.display = "none";
				document.getElementById('attendance-completed-classes').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	$scope.GetOnlineClassAttById = function (Data) {
		$scope.PastClass = Data
		var para = {
			tranId: Data.TranId
		};

		$scope.loadingstatus = "running";
		showPleaseWait();
		//$scope.OnlineClassAttByIdList = [];

		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetOnlineClassAttById",
			dataType: "json",
			data: para
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.PastClass.AbsentColl = res.data.AbsentColl;
				$scope.PastClass.PresentColl = res.data.PresentColl;


				document.getElementById('listing part').style.display = "none";
				document.getElementById('show-attendance-part').style.display = "block";


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



	}
	//*********************************** Notification ***************************
	$scope.NotificationToggle = function (item) {
		$scope.StudentColl = []

		angular.forEach(item, function (DT) {

			$scope.StudentColl.push(DT.StudentId);
		});
		$scope.newNotice.studentIdColl = ($scope.StudentColl && $scope.StudentColl.length > 0 ? $scope.StudentColl.toString() : '');
		$('#modal-xl').modal('show');
	};
	$scope.getCollval = function () {

		return {
			'col-md-9': !$scope.favorite,
			'col-md-12': $scope.favorite || $scope.ColleaguesClassesColl.length == 0
		};
	};
	$scope.SendNoticeToStudent = function () {
		$scope.loadingstatus = "running";

		$timeout(function () {
			setTimeout(function () {

				$http({
					method: 'POST',
					url: base_url + "StudentRecord/Creation/SendNoticeToStudent",
					headers: { 'Content-Type': undefined },

					transformRequest: function (data) {

						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));
						return formData;
					},
					data: { jsonData: $scope.newNotice }
				}).then(function (res) {

					$scope.loadingstatus = "stop";
					hidePleaseWait();
					if (res.data.IsSuccess == true) {
						Swal.fire('Sent Successfully');

					}

				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";

				});

			}, 100);
		})
	}


	//***********************Missed Class**********************
	$scope.GetMissedClassList = function () {

		$scope.loadingstatus = "running";
		$scope.MissedClassesList = [];
		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetMissedClassList",
			dataType: "json",
			data: $scope.V
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				angular.forEach(res.data.DataColl, function (DT) {
					DT.absentNo = DT.NoOfStudent - DT.NoOfPresent;
					$scope.MissedClassesList.push(DT);
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
});

app.controller('RptOnlineClassController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Online Class List';
	OnClickDefault();
	$scope.LoadData = function () {

		$scope.OCAdmin = {};
		$scope.onlineClass =
		{
			StartDate_TMP: new Date()
		};
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			RunningClasses: 1,
			CompletedClasses: 1,
			MissedClasses: 1,
			PresentStudents: 1,
			AbsentStudents: 1,
		};

		$scope.searchData = {
			RunningClasses: '',
			CompletedClasses: '',
			MissedClasses: '',
			PresentStudents: '',
			AbsentStudents: '',
		};

		$scope.perPage = {
			RunningClasses: GlobalServices.getPerPageRow(),
			CompletedClasses: GlobalServices.getPerPageRow(),
			MissedClasses: GlobalServices.getPerPageRow(),
			PresentStudents: GlobalServices.getPerPageRow(),
			AbsentStudents: GlobalServices.getPerPageRow(),
		};

		$scope.newRunningClasses = {
			RunningClassesId: null,
			Mode: 'Save'
		};


		$scope.newCompletedClasses = {
			CompletedClassesId: null,
			Mode: 'Save'
		};

		$scope.newMissedClasses = {
			MissedClassesId: null,
			Mode: 'Save'
		};

		$scope.missedNotice = {
			title: '',
			notice: ''
		};

		$scope.GetAllRunningClassesList();
	}

	$scope.ClearRunningClasses = function () {
		$scope.newRunningClasses = {
			RunningClassesId: null,
			Mode: 'Save'
		};
	}
	$scope.ClearCompletedClasses = function () {
		$scope.newCompletedClasses = {
			CompletedClassesId: null,
			Mode: 'Save'
		};
	}

	$scope.ClearMissedClasses = function () {
		$scope.newMissedClasses = {
			MissedClassesId: null,
			Mode: 'Save'
		};
	}


	function OnClickDefault() {
		document.getElementById('attendance-form').style.display = "none";

		//document.getElementById('show-attendance').onclick = function () {
		//document.getElementById('timelinesection').style.display = "none";
		//document.getElementById('attendance-form').style.display = "block";
		//}
		document.getElementById('back-to-list').onclick = function () {
			document.getElementById('attendance-form').style.display = "none";
			document.getElementById('timelinesection').style.display = "block";
		}
	};

	//************************* Running Classes *********************************

	$scope.ShowAttendance = function (rc) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			tranId: rc.TranId
		};

		$scope.PresentStudentsList = [];
		$scope.AbsentStudentsList = [];
		$scope.PClass = {};
		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetOnlineClassAttById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				var attData = res.data;
				$scope.PresentStudentsList = attData.PresentColl;
				$scope.AbsentStudentsList = attData.AbsentColl;
			//	$scope.TodayAbsentColl = res.data.AbsentColl;
				//$scope.TodayPresentColl = res.data.PresentColl;

				$scope.PClass = null;
				if (attData.PresentColl && attData.PresentColl.length > 0)
					$scope.PClass = attData.PresentColl[0];

				if ($scope.PClass==null  && attData.AbsentColl && attData.AbsentColl.length > 0)
					$scope.PClass = attData.AbsentColl[0];

				if (attData) {
					$scope.PClass.TeacherName = rc.TeacherName;
					$scope.PClass.SubjectName = rc.SubjectName;
					$scope.PClass.StartDateTime_AD = rc.StartDateTime_AD;
					$scope.PClass.EndDateTime_AD = rc.EndDateTime_AD;
					$scope.PClass.TotalStudent = attData.PresentColl.length+attData.AbsentColl.length;

					//angular.forEach(attData, function (ad) {
					//	if (ad.AttendanceType == 2 || ad.AttendanceType == 4)
					//		$scope.AbsentStudentsList.push(ad);
					//	else
					//		$scope.PresentStudentsList.push(ad);
					//});
					$scope.PClass.Present = $scope.PresentStudentsList.length;
					$scope.PClass.Absent = $scope.AbsentStudentsList.length;
					document.getElementById('timelinesection').style.display = "none";
					document.getElementById('attendance-form').style.display = "block";
				}

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



	};
	$scope.GetAllRunningClassesList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			forDate: null
		};
		if ($scope.onlineClass.ForDateDet) {
			para.forDate = $filter('date')(new Date($scope.onlineClass.ForDateDet.dateAD), 'yyyy-MM-dd');
		} else
			para.forDate = new Date();

		$scope.RunningClassesList = [];
		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetRptOnlineClass",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.OCAdmin = res.data;

				var query = mx($scope.OCAdmin.PassedColl);
				$scope.summary = {
					NoOfPresent: query.sum(p1 => p1.NoOfPresent),
					NoOfStudent: query.sum(p1 => p1.NoOfStudent),
				};

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetRunningClassesById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			RunningClassesId: refData.RunningClassesId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Report/GetAllRunningClassesList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			//if (res.data.IsSuccess && res.data.Data) {
			//	$scope.newRunningClasses = res.data.Data;
			//	$scope.newRunningClasses.Mode = 'Modify';

			//	document.getElementById('Setup-ExamAttendStatus').style.display = "none";
			//	document.getElementById('Setup-form').style.display = "block";

			//} else {
			//	Swal.fire(res.data.ResponseMSG);
			//}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.EndOnlineClass = function (refData) {

		Swal.fire({
			title: 'Are You Sure To End Running Class',
			showCancelButton: true,
			confirmButtonText: 'End Class',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
			
				$scope.loadingstatus = "running";
				$http({
					method: 'POST',
					url: base_url + "OnlineClass/Creation/EndOnlineClass",
					data: refData,
					dataType: "json"
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess && res.data.ResponseMSG == "Success")
					{
						Swal.fire('Ended successfully')
						$scope.GetAllRunningClassesList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};

	//************************* Completed Classes *********************************

	$scope.GetAllCompletedClassesList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CompletedClassesList = [];
		$http({
			method: 'POST',
			url: base_url + "Exam/Report/GetAllCompletedClassesList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			//if (res.data.IsSuccess && res.data.Data) {
			//	$scope.CompletedClassesList = res.data.Data;

			//} else {
			//	Swal.fire(res.data.ResponseMSG);
			//}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	$scope.GetCompletedClassesById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			CompletedClassesId: refData.CompletedClassesId
		};
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetCompletedClassesById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			//if (res.data.IsSuccess && res.data.Data) {
			//	$scope.newCompletedClasses = res.data.Data;
			//	$scope.newCompletedClasses.Mode = 'Modify';

			//	document.getElementById('CompletedClasses-content').style.display = "none";
			//	document.getElementById('CompletedClasses-form').style.display = "block";

			//} else {
			//	Swal.fire(res.data.ResponseMSG);
			//}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};



	//************************* Missed Classes *********************************

	$scope.ClearMissedNotice = function () {
		$scope.missedNotice = {
			title: '',
			notice: ''
		};
	};
	$scope.SendNotificeToMissedClass = function () {
		var para = {
			title: $scope.missedNotice.title,
			notice: $scope.missedNotice.notice,
			userIdColl: ''
		};

		angular.forEach($scope.OCAdmin.MissedColl, function (ms) {

			if (para.userIdColl.length > 0)
				para.userIdColl = para.userIdColl + ',';

			para.userIdColl = para.userIdColl + ms.UserId;
		});
		$http({
			method: 'POST',
			url: base_url + "Academic/Report/SendNoticeToMissedClass",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true)
				$scope.ClearMissedNotice();
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.ClearAbsentNotice = function () {
		$scope.absentNotice = {
			title: '',
			notice: ''
		};
	};
	$scope.SendNotificeToAbsStudent = function () {
		var para = {
			title: $scope.absentNotice.title,
			notice: $scope.absentNotice.notice,
			userIdColl: ''
		};

		angular.forEach($scope.AbsentStudentsList, function (ms) {

			if (para.userIdColl.length > 0)
				para.userIdColl = para.userIdColl + ',';

			para.userIdColl = para.userIdColl + ms.UserId;
		});
		$http({
			method: 'POST',
			url: base_url + "Academic/Report/SendNoticeToMissedClass",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true)
				$scope.ClearMissedNotice();
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.GetAllMissedClassesList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.MissedClassesList = [];
		$http({
			method: 'POST',
			url: base_url + "Exam/Report/GetAllMissedClassesList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			//if (res.data.IsSuccess && res.data.Data) {
			//	$scope.MissedClassesList = res.data.Data;

			//} else {
			//	Swal.fire(res.data.ResponseMSG);
			//}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	$scope.GetMissedClassesById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			MissedClassesId: refData.MissedClassesId
		};
		$http({
			method: 'POST',
			url: base_url + "FrontDesk/Transaction/GetMissedClassesById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			//if (res.data.IsSuccess && res.data.Data) {
			//	$scope.newMissedClasses = res.data.Data;
			//	$scope.newMissedClasses.Mode = 'Modify';

			//	document.getElementById('MissedClasses-content').style.display = "none";
			//	document.getElementById('MissedClasses-form').style.display = "block";

			//} else {
			//	Swal.fire(res.data.ResponseMSG);
			//}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});

app.filter("getDiff", function () {
	return function (time) {
		var StartDateTime_AD = new Date(time.StartDateTime_AD);
		var EndDateTime_AD = new Date(time.EndDateTime_AD);
		var milisecondsDiff = EndDateTime_AD - StartDateTime_AD;

		return Math.floor(milisecondsDiff / (1000 * 60 * 60)).toLocaleString(undefined, { minimumIntegerDigits: 2 }) + ":" + (Math.floor(milisecondsDiff / (1000 * 60)) % 60).toLocaleString(undefined, { minimumIntegerDigits: 2 }) + ":" + (Math.floor(milisecondsDiff / 1000) % 60).toLocaleString(undefined, { minimumIntegerDigits: 2 });

	}
});
app.filter('sumByColumn', function () {
	return function (collection, column) {
		var total = 0;

		collection.forEach(function (item) {
			total += parseInt(item[column]);
		});

		return total;
	};
});