app.controller('ScheduleReportController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Schedule Report';

	var gSrv = GlobalServices;

	$scope.LoadData = function () {

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

		$scope.entity = {
			ClassWiseSchedule: 354,
			AllClassSchedule: 355,
			EmpClassSchedule:356
		};
		$('.select2').select2();

		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();
		//$scope.MonthList = gSrv.getMonthList();

		$scope.EmployeeSearchOptions = gSrv.getEmployeeSearchOptions();
		
		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassShiftList = [];
		gSrv.getClassShiftList().then(function (res) {
			$scope.ClassShiftList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newTeacherWise = {
			EmployeeDetails: {},
			EmpSideBarData: {},
			EmployeeId: 0,
			SelectEmployee: $scope.EmployeeSearchOptions[0].value
		};

		$scope.newClassWise = {};

		//Added For Class Schedule Status on ishakh 24 starts
		$scope.currentPages = {
			Completed: 1,
			Pending: 1,
		};


		$scope.searchData = {
			Completed: '',
			Pending: '',
		};

		$scope.perPage = {
			Completed: GlobalServices.getPerPageRow(),
			Pending: GlobalServices.getPerPageRow(),
		};

		$scope.GetClassScheduleStatus();
		//Ends
	}

	$scope.LoadClassWiseSemesterYear = function (classId, data) {

		$scope.SelectedClassClassYearList = [];
		$scope.SelectedClassSemesterList = [];
		$scope.SelectedClass1 = mx($scope.ClassList.ClassList).firstOrDefault(p1 => p1.ClassId == classId);

		if ($scope.SelectedClass1) {
			var semQry = mx($scope.SelectedClass1.ClassSemesterIdColl);
			var cyQry = mx($scope.SelectedClass1.ClassYearIdColl);

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

	$scope.PrintClassWiseSchedule = function (fromInd)
	{

		// Load Class Wise Year and Semester On Class Selection Changed
		if (fromInd == 3 && $scope.SelectedClass) {
			$scope.newClassWise.SemesterId = null;
			$scope.newClassWise.ClassYearId = null;
			$scope.LoadClassWiseSemesterYear($scope.SelectedClass.ClassId, $scope.newClassWise);
		}

		if ($scope.SelectedClass && $scope.SelectedShift) {

			var EntityId = $scope.entity.ClassWiseSchedule;

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + EntityId + "&voucherId=0&isTran=false",
				dataType: "json"
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					var templatesColl = res.data.Data;
					if (templatesColl && templatesColl.length > 0) {
						var templatesName = [];
						var sno = 1;
						angular.forEach(templatesColl, function (tc) {
							templatesName.push(sno + '-' + tc.ReportName);
							sno++;
						});

						var rptTranId = 0;
						if (templatesColl.length == 1)
							rptTranId = templatesColl[0].RptTranId;
						else {
							Swal.fire({
								title: 'Report Templates For Print',
								input: 'select',
								inputOptions: templatesName,
								inputPlaceholder: 'Select a template',
								showCancelButton: true,
								inputValidator: (value) => {
									return new Promise((resolve) => {
										if (value >= 0) {
											resolve()
											rptTranId = templatesColl[value].RptTranId;
										} else {
											resolve('You need to select:)')
										}
									})
								}
							})
						}

						if (rptTranId > 0) {

							var rptPara = {
								rpttranid: rptTranId,
								istransaction: false,
								entityid: EntityId,
								ClassShiftId: $scope.SelectedShift.id,
								ClassShiftName:$scope.SelectedShift.text,
								ClassId: $scope.SelectedClass.ClassId,
								SectionId: $scope.SelectedClass.SectionId,
								ClassSectionName: $scope.SelectedClass.text,
								SemesterId: ($scope.newClassWise.SemesterId > 0 ? $scope.newClassWise.SemesterId : 0),
								ClassYearId: ($scope.newClassWise.ClassYearId > 0 ? $scope.newClassWise.ClassYearId : 0),
								BatchId: ($scope.newClassWise.BatchId > 0 ? $scope.newClassWise.BatchId : 0),
								EmployeeId:0
							};
							//var paraQuery = rptPara.toQueryString();
							var paraQuery = param(rptPara);
							document.body.style.cursor = 'wait';
							document.getElementById("frmRpt").src = '';
							document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
							document.body.style.cursor = 'default';
						}

					} else
						Swal.fire('No Templates found for print');
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});


		}
		//else
			//Swal.fire('Please ! Select Class And Shift Name');

	};
	$scope.PrintAllClassSchedule = function () {
		if ($scope.SelectedShift_A) {

			var EntityId = $scope.entity.AllClassSchedule;

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + EntityId + "&voucherId=0&isTran=false",
				dataType: "json"
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					var templatesColl = res.data.Data;
					if (templatesColl && templatesColl.length > 0) {
						var templatesName = [];
						var sno = 1;
						angular.forEach(templatesColl, function (tc) {
							templatesName.push(sno + '-' + tc.ReportName);
							sno++;
						});

						var rptTranId = 0;
						if (templatesColl.length == 1)
							rptTranId = templatesColl[0].RptTranId;
						else {
							Swal.fire({
								title: 'Report Templates For Print',
								input: 'select',
								inputOptions: templatesName,
								inputPlaceholder: 'Select a template',
								showCancelButton: true,
								inputValidator: (value) => {
									return new Promise((resolve) => {
										if (value >= 0) {
											resolve()
											rptTranId = templatesColl[value].RptTranId;
										} else {
											resolve('You need to select:)')
										}
									})
								}
							})
						}

						if (rptTranId > 0) {

							var rptPara = {
								rpttranid: rptTranId,
								istransaction: false,
								entityid: EntityId,
								ClassShiftId: $scope.SelectedShift_A.id,
								ClassShiftName: $scope.SelectedShift_A.text								
							};							
							var paraQuery = param(rptPara);
							document.body.style.cursor = 'wait';
							document.getElementById("frmRpt1").src = '';
							document.getElementById("frmRpt1").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
							document.body.style.cursor = 'default';
						}

					} else
						Swal.fire('No Templates found for print');
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});


		}
		//else
		//Swal.fire('Please ! Select Class And Shift Name');

	};
	$scope.PrintEmpClassSchedule = function () {
		if ($scope.newTeacherWise.EmployeeDetails) {

			var EntityId = $scope.entity.EmpClassSchedule;

			$http({
				method: 'GET',
				url: base_url + "ReportEngine/GetReportTemplates?entityId=" + EntityId + "&voucherId=0&isTran=false",
				dataType: "json"
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					var templatesColl = res.data.Data;
					if (templatesColl && templatesColl.length > 0) {
						var templatesName = [];
						var sno = 1;
						angular.forEach(templatesColl, function (tc) {
							templatesName.push(sno + '-' + tc.ReportName);
							sno++;
						});

						var rptTranId = 0;
						if (templatesColl.length == 1)
							rptTranId = templatesColl[0].RptTranId;
						else {
							Swal.fire({
								title: 'Report Templates For Print',
								input: 'select',
								inputOptions: templatesName,
								inputPlaceholder: 'Select a template',
								showCancelButton: true,
								inputValidator: (value) => {
									return new Promise((resolve) => {
										if (value >= 0) {
											resolve()
											rptTranId = templatesColl[value].RptTranId;
										} else {
											resolve('You need to select:)')
										}
									})
								}
							})
						}

						if (rptTranId > 0) {

							var rptPara = {
								rpttranid: rptTranId,
								istransaction: false,
								entityid: EntityId,
								EmployeeId: $scope.newTeacherWise.EmployeeDetails.EmployeeId,
								EmpName: $scope.newTeacherWise.EmployeeDetails.Name
							};
							var paraQuery = param(rptPara);
							document.body.style.cursor = 'wait';
							document.getElementById("frmRpt2").src = '';
							document.getElementById("frmRpt2").src = base_url + "web/ReportViewer.aspx?" + paraQuery;
							document.body.style.cursor = 'default';
						}

					} else
						Swal.fire('No Templates found for print');
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});


		}
		//else
		//Swal.fire('Please ! Select Class And Shift Name');

	};
	$scope.ClearClassWise = function () {
		$scope.newClassWise = {
			ClassWiseId: null,

		};
	}
	$scope.ClearClassSchedule = function () {
		$scope.newClassSchedule = {
			ClassScheduleId: null,

		};
	}
	$scope.ClearTeacherWise = function () {
		$scope.newTeacherWise = {
			TeacherWiseId: null,

		};
	}

	//Added by Roshan for Class Schedule Stctus


	$scope.GetClassScheduleStatus = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CompletedColl = [];
		$scope.PendingColl = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetAllClassSchedule",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CompletedColl = res.data.Data.completedColl;
				$scope.PendingColl = res.data.Data.pendingColl;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});