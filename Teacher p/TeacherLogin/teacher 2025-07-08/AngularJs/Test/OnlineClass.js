String.prototype.isEmpty = function () {
	return (this.length === 0 || !this.trim());
};
app.controller('OnlineClassController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'OnlineClass';

	OnClickDefault();
	$scope.LoadData = function () {


		$scope.colors = ['#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)', '#33A2FF ', 'rgb(255, 153, 102)', 'rgb(153, 204, 0)']

		$scope.Data = {};
		$scope.V = {};
		$scope.V.dateFrom = new Date();
		$scope.V.dateTo = new Date();
		$scope.GetAllOnlineClassesList();
		$scope.SubjectwithoutBody = [];
		$http.post(base_url + "OnlineClass/Creation/GetSubjectColl")
			.then(function (data) {
				$scope.SubjectwithoutBody = data.data;
			}, function (reason) {
				alert("Data not get");
			});



		$scope.ClassShiftColl = [];
		$http.get(base_url + "OnlineClass/Creation/GetClassShiftLit")
			.then(function (data) {
				$scope.ClassShiftColl = data.data;
			}, function (reason) {
				alert("Data not get");
			});


		//Get class and Section List
		$scope.ClassColl = [];
		$scope.SectionColl = [];
		$http.get(base_url + "StudentAttendance/Creation/GetClassSection")
			.then(function (data) {
				$scope.ClassColl = data.data.ClassList;
				$scope.SectionClassColl = data.data.SectionList;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				alert("Couldn't find data");
			});

		//Get Online Platform List
		$scope.OnlinePlatformColl = [];
		$http.get(base_url + "OnlineClass/Creation/GetOnlinePlatform")
			.then(function (data) {
				$scope.OnlinePlatformColl = data.data;
				$scope.newOnlineClass.PlatformType = 1
				$scope.OnChangeplatformtype($scope.newOnlineClass.PlatformType);
				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				alert("Data not get");
			});

	
		

		//Get ClassSchedule data
		$scope.ClassScheduleColl = [];
		$http.get(base_url + "OnlineClass/Creation/GetClassSchedule")
			.then(function (data) {
				$scope.ClassScheduleColl = data.data;
				var CurrentDate = new Date();
				$scope.DayId = CurrentDate.getDay();
				$scope.ClassShiftColl;
				$scope.ChangeWeek($scope.DayId+1);
			}, function (reason) {
				alert("Data not get");
			});
		
		



		//Calling RunningClassesList
		$scope.RunningClassesList = function () {

			$http.get(base_url + "OnlineClass/Creation/GetRunningClassesList")
				.then(function (data) {
					$scope.RunningClassesColl = data.data;
				}, function (reason) {
					alert("Data not get");
				});


		}



		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.LanguageColl = GlobalServices.getLangList();

		$scope.currentPages = {
			PastClass: 1,
			OnlinePlatform: 1

		};

		$scope.searchData = {
			PastClass: '',
			OnlinePlatform: ''

		};

		$scope.perPage = {
			PastClass: GlobalServices.getPerPageRow(),
			OnlinePlatform: GlobalServices.getPerPageRow()

		};
		$scope.HostClass = { };
		$scope.newOnlineClass = {
			OnlineClassId: null,
			SectionId: null,
			SubjectId: null,
			ClassId: null,
			Note: '',
			Mode: 'Save'
		};

		$scope.newOnlinePlatform = {
			OnlinePlatformId: null,
			Mode: 'Save'
		};



		//$scope.GetAllOnlineClassList();

	};
	$('#cboClassId').on("change", function (e) {
		$scope.SectionColl = [];
		$scope.SectionList = [];
		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.newOnlineClass.ClassId = select_val
		angular.forEach($scope.SectionClassColl, function (SVCollData) {
			if (select_val == SVCollData.ClassId) {

				$scope.Section = SVCollData;
				$scope.SectionColl.push($scope.Section);
			}
		})
		$timeout(function () {
			$scope.SectionList = $scope.SectionColl;
		});
	});
	$('#cboClassId1').on("change", function (e) {
		$scope.SectionColl = [];
		$scope.SectionList = [];
		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.ClassId = select_val
		angular.forEach($scope.SectionClassColl, function (SVCollData) {
			if (select_val == SVCollData.ClassId) {

				$scope.Section = SVCollData;
				$scope.SectionColl.push($scope.Section);
			}


		})
		$timeout(function () {
			$scope.SectionList = $scope.SectionColl;
		});



	});
	$scope.ChangeWeek = function (DayId) {
		$scope.ClassScheduleCollList = [];
		$scope.ClassShiftList = [];
		$scope.UpCommingScheduleCollList = [];
		angular.forEach($scope.ClassScheduleColl, function (st) {
			if (DayId == st.DayId) {
				$scope.ClassScheduleCollList.push(st)
				//if (st.Up == 'Passed') {
				//	$scope.UpCommingScheduleCollList.push(st)
				//}
			}
			
		});
		angular.forEach($scope.ClassShiftColl, function (sc) {
			//$scope.keepGoing = true; 
			angular.forEach($scope.ClassScheduleCollList, function ( CSL) {
				if (CSL.ShiftId == sc.ClassShiftId/* && $scope.keepGoing*/) {
					$scope.ClassShiftList.push(sc);
					//$scope.keepGoing = false; 
				}
			})

		});
		$scope.ClassShiftList = Array.from(new Set($scope.ClassShiftList));
	}
	$scope.GetHostDet = function (df) {

		$scope.HostClass = df
		$scope.HostClass.InterFun = true;
		

    }
	$scope.checkFull = function () {

		var val = $scope.chkFull;

		if ($scope.AbsentColl) {
			angular.forEach($scope.AbsentColl, function (ec) {
				ec.Full = val;
			});
		}
	};

	$('#cboSectionId').on("change", function (e) {

		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.SectionId = select_val;

		$scope.newOnlineClass.ClassId = $scope.newOnlineClass.ClassId ;
		$scope.newOnlineClass.SectionIdColl = ($scope.SectionId && $scope.SectionId.length > 0 ? $scope.SectionId.toString() : '');
		$scope.GetSubjectList();

	});
	$('#cboSubjectId').on("change", function (e) {

		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		$scope.SubjectId = select_val;
		$scope.newOnlineClass.SubjectId = $scope.SubjectId;

	});
	$('#cboClassShift').on("change", function (e) {
		$scope.ClassArray = [];
		$scope.ClassListColl = [];
		var selected_element = $(e.currentTarget);
		var select_val = selected_element.val();
		if (select_val) {
			var Dat = JSON.parse(select_val);
			//$scope.ClassShiftId = select_val;
			$scope.newOnlineClass.ClassShiftId = Dat.ClassShiftId;
			angular.forEach($scope.ClassScheduleCollList, function (Cl) {
				if (Dat.ClassShiftId == Cl.ShiftId) {
					$scope.ClassArray.push(Cl.ClassId);
				}
			})

			$timeout(function () {
				$scope.ClassArray = Array.from(new Set($scope.ClassArray));
				angular.forEach($scope.ClassColl, function (Cl) {
					angular.forEach($scope.ClassArray, function (Val) {
						if (Cl.ClassId == Val) {
							$scope.ClassListColl.push(Cl);
						}


					})

				})


			});
        }
		
		



	});
	function OnClickDefault() {
		document.getElementById('show-attendance-part').style.display = "none";
		document.getElementById('area-after-platform-setup').style.display = "none";
		document.getElementById('attendance-completed-classes').style.display = "none";
		
		//document.getElementById('show-attendance').onclick = function () {

		//}

		document.getElementById('back-btn').onclick = function () {
			document.getElementById('listing part').style.display = "block";
			document.getElementById('show-attendance-part').style.display = "none";
		}

		document.getElementById('next-btn').onclick = function () {
			document.getElementById('area-after-platform-setup').style.display = "block";
			document.getElementById('setup-form').style.display = "none";
		}

		document.getElementById('back-to-platform').onclick = function () {
			document.getElementById('area-after-platform-setup').style.display = "none";
			document.getElementById('setup-form').style.display = "block";
		}

		document.getElementById('todays-attendance').onclick = function () {
			document.getElementById('area-after-platform-setup').style.display = "none";
			document.getElementById('attendance-completed-classes').style.display = "block";
		}
		document.getElementById('back-todays-completed-class').onclick = function () {
			document.getElementById('area-after-platform-setup').style.display = "block";
			document.getElementById('attendance-completed-classes').style.display = "none";
		}
	}

	$scope.OnSaveOnlinePlatformGetList = function () {
		$scope.OnlinePlatformColl = [];
		$http.get(base_url + "OnlineClass/Creation/GetOnlinePlatform")
			.then(function (data) {
				$scope.OnlinePlatformColl = data.data;

				//$scope.ONChangeClassSection($scope);
			}, function (reason) {
				alert("Data not get");
			});
	}
	$scope.ClearOnlineClass = function () {
		//location.reload();
		$timeout(function () {
			$('.select2').val(null).trigger('change');
			$scope.newOnlineClass.Notes = ''
			


		});
		
		//$scope.newOnlineClass = {
		//	OnlineClassId: null,
		//	SectionId: null,
		//	SubjectId: null,
		//	ClassId: null,
		//	Note: '',
		//	Mode: 'Save'
		//};
	};




	//************************* Class *********************************

	$scope.IsValidOnlineClass = function () {
		if ($scope.newOnlineClass.UserName == '') {
			Swal.fire('Please ! Enter Title');
			return false;
		}



		return true;
	};


	$scope.OnChangeplatformtype = function (G) {
		var Coll = $scope.OnlinePlatformColl;
		if (G == 1) {
			$scope.newOnlineClass.Register = ' https://zoom.us/signup'

		}
		else if (G == 2) {
			$scope.newOnlineClass.Register = ' https://apps.google.com/meet/'
		}
		else if (G == 3) {
			$scope.newOnlineClass.Register = ' https://www.microsoft.com/en-us/microsoft-teams/log-in'
		}
		angular.forEach($scope.OnlinePlatformColl, function (st) {

			if ($scope.newOnlineClass.PlatformType == st.PlatformType) {
				$scope.newOnlineClass.Link = st.Link
				$scope.newOnlineClass.Pwd = st.Pwd
				$scope.newOnlineClass.UserName = st.UserName
			}

		});



	};



	//Defination Of Function for calling upadate online platform Type
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
	//Defination Of Function for Updating Online Platform
	$scope.CallSaveUpdateOnlineClass = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		//if ($scope.newOnlineClass.OnlineClassDateDet) {
		//	$scope.newOnlineClass.OnlineClassDate = $scope.newOnlineClass.OnlineClassDateDet.dateAD;
		//} else
		//	$scope.newOnlineClass.OnlineClassDate = null;


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
				
				//$scope.GetAllOnlineClassList();
				$scope.OnSaveOnlinePlatformGetList();
				//window.open('https://zoom.us/signin', '_blank');
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};
	//Defination Of Function for calling Start Class function
	$scope.StartOnlineClass = function () {
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
						$scope.CallStartOnlineClass();
					}
				});
			} else
				$scope.CallStartOnlineClass();
		}
	};

	//Defination Of Function for Updating Online Platform
	$scope.CallStartOnlineClass = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		//if ($scope.newOnlineClass.OnlineClassDateDet) {
		//	$scope.newOnlineClass.OnlineClassDate = $scope.newOnlineClass.OnlineClassDateDet.dateAD;
		//} else
		//	$scope.newOnlineClass.OnlineClassDate = null;
		if ($scope.HostClass.InterFun) {
			$scope.newOnlineClass.ClassShiftId = $scope.HostClass.ShiftId;
			$scope.newOnlineClass.ClassId = $scope.HostClass.ClassId
			$scope.newOnlineClass.SectionId = $scope.HostClass.SectionId
			$scope.newOnlineClass.Notes = $scope.HostClass.Notes
			angular.forEach($scope.SubjectwithoutBody, function (sc) {
				if ($scope.HostClass.SubjectName == sc.Name) {
					$scope.newOnlineClass.SubjectId = sc.SubjectId
				}

			})
        }
		

		

		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/StartOnlineClass",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newOnlineClass }
		}).then(function (res) {
			hidePleaseWait();
			if (res.data.ResponseMSG == "Already Class is Running.") {
				Swal.fire({
					title: res.data.ResponseMSG+'Do you want to end the class?',
					showCancelButton: true,
					confirmButtonText: 'End',
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.Data.TranId = res.data.RId;
						$scope.EndOnlineClass($scope.Data);
					}
				});
				$scope.loadingstatus = "stop";
			}
			else {
				Swal.fire({

					title: res.data.ResponseMSG,
					//showCancelButton: true,
					confirmButtonText: 'OK',
				}).then((result) => {
					if (res.data.RId > 0) {
						window.open($scope.newOnlineClass.Link, '_blank');
					}
				});
				///Swal.fire(res.data.ResponseMSG);

				if (res.data.IsSuccess == true) {
					//$scope.ClearOnlineClass();
					//$scope.GetAllOnlineClassList();
					$scope.OnSaveOnlinePlatformGetList();

					$scope.ClearOnlineClass();
					$scope.RunningClassesList();
				}
			}



		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};





	$scope.EndOnlineClass = function (Data) {
		var Data1 = Data.TranId;
		Data.RId = Data.TranId;
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.OnlineClassList = [];

		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/EndOnlineClass",
			data: Data,
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.ResponseMSG == "Success") {
				alert('Ended successfully')
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};


	$scope.GetAllOnlineClassList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.OnlineClassList = [];

		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetAllOnlineClassList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.OnlineClassList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GetOnlineClassById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			OnlineClassId: refData.OnlineClassId
		};

		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetOnlineClassById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newOnlineClass = res.data.Data;
				$scope.newOnlineClass.Mode = 'Modify';

				document.getElementById('class-section').style.display = "none";
				document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelOnlineClassById = function (refData) {

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
					OnlineClassId: refData.OnlineClassId
				};

				$http({
					method: 'POST',
					url: base_url + "OnlineClass/Creation/DelOnlineClass",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllOnlineClassList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};
	//Clalling multiple time Running Class
	setInterval(function () { $scope.RunningClassesList(); }, 1000);

	/*pastOnline classes*/
	$scope.OnChangeDate = function () {
	
		if ($scope.newPastClass.FromDate_TMP && $scope.newPastClass.ToDate_TMP) {

			var res = $scope.newPastClass.FromDate_TMP.split("-");
			var resTo = $scope.newPastClass.ToDate_TMP.split("-");
			$scope.dateFrom = NepaliFunctions.BS2AD({ year: res[0], month: res[1], day: res[2] })
			$scope.dateTo = NepaliFunctions.BS2AD({ year: resTo[0], month: resTo[1], day: resTo[2] })
			$scope.V.dateFrom = $scope.dateFrom.year + '-' + $scope.dateFrom.month + '-' + $scope.dateFrom.day;
			$scope.V.dateTo = $scope.dateTo.year + '-' + $scope.dateTo.month + '-' + $scope.dateTo.day;

			$scope.GetAllOnlineClassesList();

		}

	};
	$scope.GetAllOnlineClassesList = function () {
		
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.OnlineClassesList = [];
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
				$scope.PassOnlineClassesList = res.data;
				
				angular.forEach($scope.PassOnlineClassesList, function (st) {
					angular.forEach(st.DataColl, function (DT) {

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


	$scope.GetOnlineClassAttById = function (Data) {
		$scope.ValselectId = Data;


			var para = {
				tranId: Data.TranId
	               };

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.OnlineClassAttByIdList = [];
		
		$http({
			method: 'POST',
			url: base_url + "OnlineClass/Creation/GetOnlineClassAttById",
			dataType: "json",
			data: para
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data) {
				$scope.AbsentColl = res.data.AbsentColl;
				$scope.LateColl = res.data.LateColl;
				$scope.LeaveColl = res.data.LeaveColl;
				$scope.PresentColl = res.data.PresentColl;


				document.getElementById('listing part').style.display = "none";
      		    document.getElementById('show-attendance-part').style.display = "block";


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



	}
	$scope.GetSubjectList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SubjectColl = [];
		if($scope.newOnlineClass.SectionIdColl && $scope.newOnlineClass.ClassId){
			var para = {
				classId: $scope.newOnlineClass.ClassId,
				sectionIdColl: $scope.newOnlineClass.SectionIdColl
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




	//$scope.GetOnlineClassAttById = function (Data) {


	//var para = {
	//	OnlineClassId: refData.OnlineClassId
	//};

	//	$scope.loadingstatus = "running";
	//	showPleaseWait();
	//	$scope.OnlineClassesList = [];
	//	$scope.PassClassesList = [];
	//	$http({
	//		method: 'POST',
	//		url: base_url + "OnlineClass/Creation/GetOnlineClassAttById",
	//		dataType: "json",
	//		data: Idval
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data) {
	//			$scope.OnlineClassAttByIdList = res.data;

	//			document.getElementById('listing part').style.display = "none";
	//			document.getElementById('show-attendance-part').style.display = "block";

	//		} else {
	//			Swal.fire(res.data.ResponseMSG);
	//		}

	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});






		
	//}
});