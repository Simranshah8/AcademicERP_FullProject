app.controller('OnlineClassController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Setup';
	OnClickDefault();
	$scope.LoadData = function () {

		$scope.OCAdmin = {};
		$scope.onlineClass =
		{
			StartDate_TMP:new Date()
		};
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			RunningClasses: 1,
			CompletedClasses: 1,
			MissedClasses: 1,
			PresentStudents: 1,
			AbsentStudents:1,
		};

		$scope.searchData = {
			RunningClasses: '',
			CompletedClasses: '',
			MissedClasses: '',
			PresentStudents: '',
			AbsentStudents:'',
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

	$scope.ShowAttendance = function (rc)
	{
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
			url: base_url + "Academic/Report/GetOnlineClassAttById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var attData = res.data.Data;
				
				if (attData && attData.length > 0) {
					$scope.PClass = attData[0];
					$scope.PClass.TeacherName = rc.TeacherName;
					$scope.PClass.SubjectName = rc.SubjectName;
					$scope.PClass.StartDateTime_AD = rc.StartDateTime_AD;
					$scope.PClass.EndDateTime_AD = rc.EndDateTime_AD;
					$scope.PClass.TotalStudent = attData.length;

					angular.forEach(attData, function (ad) {
						if (ad.AttendanceType == 2 || ad.AttendanceType==4)
							$scope.AbsentStudentsList.push(ad);
						else
							$scope.PresentStudentsList.push(ad);
					});
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
			forDate:null
		};
		if ($scope.onlineClass.ForDateDet) {
			para.forDate = $filter('date')(new Date($scope.onlineClass.ForDateDet.dateAD), 'yyyy-MM-dd');
		} else
			para.forDate = new Date();

		$scope.RunningClassesList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetOnlieClassData",
			dataType: "json",
			data:JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.OCAdmin = res.data.Data;

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
				showPleaseWait();

				var para = {
					tranId: refData.TranId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Report/EndOnlineClass",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
				
					if (res.data.IsSuccess)
					{
						$scope.GetAllRunningClassesList(); 
						hidePleaseWait();
						$scope.loadingstatus = "stop";
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
			notice:''
		};
	};
	$scope.SendNotificeToMissedClass = function ()
	{
		var para = {
			title: $scope.missedNotice.title,
			notice: $scope.missedNotice.notice,
			userIdColl:''
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

			if(res.data.IsSuccess==true)
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