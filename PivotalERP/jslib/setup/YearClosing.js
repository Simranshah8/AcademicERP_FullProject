app.controller('YearClosingController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Year Closing';
	//OnClickDefault();

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

					$timeout(function () {
						$scope.LoadClassList();
					});

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

			$scope.ClassYearList = [];
			$scope.SelectedClassClassYearList = [];
			GlobalServices.getClassYearList().then(function (res) {
				$scope.ClassYearList = res.data.Data;

				$timeout(function () {
					$scope.LoadClassList();
				});

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});



		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.newYearClosing = {
			YearClosingId: null,
			ExamAs: 1,
			PromoteTo: 1,
			RollNoAs: 3,
			ForwardFeeMapping: false,
			ForwardTransportMapping: false,
			ForwardBedMapping: false,
			Mode: 'Save',
			ClassColl: [],
			ForwardOpening:true,
			//Added by suresh on magh 13
			TransferStudentOpening: true,
			SectionAs:1
		};






		$scope.ExamTypeList = [];
		gSrv.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ExamTypeGroupList = [];
		gSrv.getExamTypeGroupList().then(function (res) {
			$scope.ExamTypeGroupList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AcademicYearColl = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllAcademicYearList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AcademicYearColl = res.data.Data;

				if ($scope.AcademicYearColl.length > 0) {
					$scope.CurAcademicYearId = $scope.AcademicYearColl[0].AcademicYearId;
				}
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.CostClassColl = [];
		$http({
			method: 'POST',
			url: base_url + "Account/Creation/GetAllCostClassList",
			dataType: "json"
		}).then(function (res1) {
			if (res1.data.IsSuccess && res1.data.Data) {
				$scope.CostClassColl = res1.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.FeeItemList = [];
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAllFeeItemList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FeeItemList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};

	$scope.GenerateOTP = function () {

		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GenerateYOTP",
			dataType: "json"
		}).then(function (res) {
			Swal.fire(res.data.ResponseMSG);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	};
	$scope.LoadClassList = function () {
		$scope.ClassList = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassList = res.data.Data;
				$scope.newYearClosing.ClassColl = res.data.Data;

				var classQry = mx($scope.ClassList);
				angular.forEach($scope.newYearClosing.ClassColl, function (cc) {
					cc.FromClassId = cc.id;
					cc.ClassType = cc.ClassType;
					var SelectedClass = classQry.firstOrDefault(p1 => p1.ClassId == cc.id);

					if (SelectedClass) {
						var semQry = mx(SelectedClass.ClassSemesterIdColl);
						var cyQry = mx(SelectedClass.ClassYearIdColl);

						cc.SelectedClassClassYearList = [];
						cc.SelectedClassSemesterList = [];

						angular.forEach($scope.SemesterList, function (sem) {
							if (semQry.contains(sem.id)) {
								cc.SelectedClassSemesterList.push({
									id: sem.id,
									text: sem.text,
									SemesterId: sem.id,
									Name: sem.Name
								});
							}
						});

						angular.forEach($scope.ClassYearList, function (sem) {
							if (cyQry.contains(sem.id)) {
								cc.SelectedClassClassYearList.push({
									id: sem.id,
									text: sem.text,
									ClassYearId: sem.id,
									Name: sem.Name
								});
							}
						});

					}

				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.IsValidYearClosing = function () {
		return true;
	}

	$scope.SaveUpdateYearClosing = function () {
		if ($scope.IsValidYearClosing() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newYearClosing.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateYearClosing();
					}
				});
			} else
				$scope.CallSaveUpdateYearClosing();

		}
	};

	$scope.CallSaveUpdateYearClosing = function () {

		Swal.fire({
			title: 'Are You Sure To End Current Academic Year , After Closing data will not revert ?',
			showCancelButton: true,
			confirmButtonText: 'Yes',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				$http({
					method: 'POST',
					url: base_url + "Setup/Security/DoYearClosing",
					headers: { 'Content-Type': undefined },

					transformRequest: function (data) {

						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));

						return formData;
					},
					data: { jsonData: $scope.newYearClosing }
				}).then(function (res) {

					$scope.loadingstatus = "stop";
					hidePleaseWait();

					Swal.fire(res.data.ResponseMSG);

					if (res.data.IsSuccess == true) {
						//$scope.ClearYearClosing();
						//$scope.GetAllYearClosingList();
					}

				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";

				});
			}
		});



	};

	$scope.UpdateAcademicYear = function () {

		Swal.fire({
			title: 'Are You Sure To Update Default Academic Year ? ',
			showCancelButton: true,
			confirmButtonText: 'Yes',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				$http({
					method: 'POST',
					url: base_url + "Setup/Security/UpdateAcademicYear",
					headers: { 'Content-Type': undefined },

					transformRequest: function (data) {

						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));

						return formData;
					},
					data: { jsonData: $scope.newYearClosing }
				}).then(function (res) {

					$scope.loadingstatus = "stop";
					hidePleaseWait();

					Swal.fire(res.data.ResponseMSG);



				}, function (errormessage) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";

				});
			}
		});



	};



	$scope.GetAllYearClosingList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.YearClosingList = [];

		$http({
			method: 'POST',
			url: base_url + "Setup/Setup/GetAllYearClosingList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.YearClosingList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//Added By Suresh on 26 Falgun
	$scope.newDetail = {};
	$scope.StudentColl = [];5
	$scope.selectAllStudents = false;

	$scope.ShowStudentSelection = function (refdata) {
		if (!refdata.ClassId) return;

		$scope.newDetail = {
			ClassName: refdata.text,
			Batch: refdata.BatchId,
			ClassYear: refdata.FromClassYearId,
			Semester: refdata.FromSemesterId
		};

		$scope.StudentColl = [];
		var para = {
			ClassId: refdata.ClassId,
			SectionIdColl: refdata.SectionId && refdata.SectionId.length > 0 ? refdata.SectionId.toString() : '',
			FilterSection: true,
			SemesterId: refdata.FromSemesterId,
			ClassYearId: refdata.FromClassYearId,
			BatchId: refdata.BatchId,
			BranchId: refdata.BranchId,
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/GetClassWiseStudentList",
			data: JSON.stringify(para),
			headers: { 'Content-Type': 'application/json' }
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentColl = res.data.Data.map(function (student) {
					return {
						ClassId: student.ClassId,
						Name: student.Name,
						RegdNo: student.RegdNo,
						RollNo: student.RollNo,
						ClassName: student.ClassName,
						Batch: student.Batch,
						Semester: student.Semester,
						ClassYear: student.ClassYear,
						BoardRegdNo: student.BoardRegdNo,
						selected: $scope.selectAllStudents 
					};
				});
				$('#modal-custom-student').modal('show');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (error) {
			Swal.fire('Failed: ' + error.statusText);
		});
	};

	

	// Function to check/uncheck all checkboxes inside modal
	$scope.CheckedAll = function (selectAll) {
		$scope.StudentColl.forEach(student => {
			student.selected = selectAll;
		});
	};

	// Update 'Select All' checkbox when any student selection changes
	$scope.updateSelection = function () {
		$scope.selectAllStudents = $scope.StudentColl.every(student => student.selected);
	};

});