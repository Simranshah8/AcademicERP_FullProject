app.controller('ClassController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Class';

	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.ClassTypeList = [{ id: 1, text: 'None' }, { id: 2, text: 'Year Wise' }, { id: 3, text: 'Semester Wise' }];

		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.MonthList = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.currentPages = {
			Class: 1,
			Section: 1,
			AcademicYear: 1,
			Board: 1,
			Level: 1,
			Faculty: 1,
			Semester: 1,
			Batch: 1,
			ClassYear: 1,
			ClassGroup: 1
		};

		$scope.searchData = {
			Class: '',
			Section: '',
			AcademicYear: '',
			Board: '',
			Level: '',
			Faculty: '',
			Semester: '',
			Batch: '',
			ClassYear: '',
			ClassGroup: ''
		};

		$scope.perPage = {
			Class: 25,
			Section: GlobalServices.getPerPageRow(),
			AcademicYear: GlobalServices.getPerPageRow(),
			Board: GlobalServices.getPerPageRow(),
			Level: GlobalServices.getPerPageRow(),
			Faculty: GlobalServices.getPerPageRow(),
			Semester: GlobalServices.getPerPageRow(),
			Batch: GlobalServices.getPerPageRow(),
			ClassYear: GlobalServices.getPerPageRow(),
			ClassGroup: GlobalServices.getPerPageRow(),
		};

		$scope.newClass = {
			ClassId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			IsPassOut: false,
			ClassYearList: [],
			SemesterList: [],
			AcademicMonthColl: [],
			ActiveFeeMapppingMonth: false,
			IsActive: true,
			FacultyColl: [],
			BoardColl: [],
			Mode: 'Save'
		};
		$scope.newClass.AcademicMonthColl.push({ MonthId: 0, Name: '' });

		$scope.newSection = {
			SectionId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newClassYear = {
			ClassYearId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newLevel = {
			LevelId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newFaculty = {
			FacultyId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newSemester = {
			SemesterId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newBatch = {
			BatchId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};
		$scope.newAcademicYear = {
			AcademicYearId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			IsRunning: false,
			CostClassId:null,
			Mode: 'Save'
		};

		$scope.newBoard = {
			BoardId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save'
		};

		$scope.newClassGroups = {
			ClassGroupId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save',
			ClassList: [],
			ClassGroupsDetailsColl: []
		};

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

		$scope.GetAllClassList();
		$scope.GetAllSectionList();
		$scope.GetAllAcademicYearList();
		$scope.GetAllBoardList();

		$scope.GetAllLevelList();
		$scope.GetAllFacultyList();
		$scope.GetAllSemesterList();
		$scope.GetAllBatchList();
		$scope.GetAllClassYearList();
		$scope.GetAllClassGroupsList();
	}

	$scope.GetClassSetupAutoOrderNo = function (be) {
		var para = {
			For: be
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetClassSetupAutoOrderNo",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				var vDet = res.data.Data;
				if (be == 1) {
					$scope.newClass.OrderNo = vDet.RId;
				}
				if (be == 2) {
					$scope.newSection.OrderNo = vDet.RId;
				}
				if (be == 3) {
					$scope.newLevel.OrderNo = vDet.RId;
				}
				if (be == 4) {
					$scope.newFaculty.OrderNo = vDet.RId;
				}
				if (be == 5) {
					$scope.newSemester.OrderNo = vDet.RId;
				}
				if (be == 6) {
					$scope.newBatch.OrderNo = vDet.RId;
				}
				if (be == 7) {
					$scope.newClassYear.OrderNo = vDet.RId;
				}
				if (be == 8) {
					$scope.newAcademicYear.OrderNo = vDet.RId;
				}
				if (be == 9) {
					$scope.newBoard.OrderNo = vDet.RId;
				}
				if (be == 10) {
					$scope.newClassGroups.OrderNo = vDet.RId;
				}

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	function OnClickDefault() {


		document.getElementById('class-form').style.display = "none";
		document.getElementById('section-form').style.display = "none";
		document.getElementById('academicyear-form').style.display = "none";
		document.getElementById('board-form').style.display = "none";

		document.getElementById('level-form').style.display = "none";
		document.getElementById('faculty-form').style.display = "none";
		document.getElementById('semester-form').style.display = "none";
		document.getElementById('batch-form').style.display = "none";
		document.getElementById('classyear-form').style.display = "none";
		document.getElementById('classgroup-form').style.display = "none";

		document.getElementById('add-class').onclick = function () {
			document.getElementById('class-section').style.display = "none";
			document.getElementById('class-form').style.display = "block";
			$scope.ClearClass();
			$scope.GetClassSetupAutoOrderNo(1);
		}

		document.getElementById('back-btn').onclick = function () {
			document.getElementById('class-section').style.display = "block";
			document.getElementById('class-form').style.display = "none";
			$scope.ClearClass();
		}

		//// classyear area

		document.getElementById('add-classyear').onclick = function () {
			document.getElementById('classyear-content').style.display = "none";
			document.getElementById('classyear-form').style.display = "block";
			$scope.ClearClassYear();
			$scope.GetClassSetupAutoOrderNo(7);
		}

		document.getElementById('back-btn-classyear').onclick = function () {
			document.getElementById('classyear-content').style.display = "block";
			document.getElementById('classyear-form').style.display = "none";
			$scope.ClearClassYear();
		}

		//// level area

		document.getElementById('add-level').onclick = function () {
			document.getElementById('level-content').style.display = "none";
			document.getElementById('level-form').style.display = "block";
			$scope.ClearLevel();
			$scope.GetClassSetupAutoOrderNo(3);
		}

		document.getElementById('back-btn-level').onclick = function () {
			document.getElementById('level-content').style.display = "block";
			document.getElementById('level-form').style.display = "none";
			$scope.ClearLevel();
		}

		//// Faculty area

		document.getElementById('add-faculty').onclick = function () {
			document.getElementById('faculty-content').style.display = "none";
			document.getElementById('faculty-form').style.display = "block";
			$scope.ClearFaculty();
			$scope.GetClassSetupAutoOrderNo(4);
		}

		document.getElementById('back-btn-faculty').onclick = function () {
			document.getElementById('faculty-content').style.display = "block";
			document.getElementById('faculty-form').style.display = "none";
			$scope.ClearFaculty();
		}

		//// Semester area

		document.getElementById('add-semester').onclick = function () {
			document.getElementById('semester-content').style.display = "none";
			document.getElementById('semester-form').style.display = "block";
			$scope.ClearSemester();
			$scope.GetClassSetupAutoOrderNo(5);
		}

		document.getElementById('back-btn-semester').onclick = function () {
			document.getElementById('semester-content').style.display = "block";
			document.getElementById('semester-form').style.display = "none";
			$scope.ClearSemester();
		}

		//// Batch area

		document.getElementById('add-batch').onclick = function () {
			document.getElementById('batch-content').style.display = "none";
			document.getElementById('batch-form').style.display = "block";
			$scope.ClearBatch();
			$scope.GetClassSetupAutoOrderNo(6);
		}

		document.getElementById('back-btn-batch').onclick = function () {
			document.getElementById('batch-content').style.display = "block";
			document.getElementById('batch-form').style.display = "none";
			$scope.ClearBatch();
		}

		// section area

		document.getElementById('add-section').onclick = function () {
			document.getElementById('section-content').style.display = "none";
			document.getElementById('section-form').style.display = "block";
			$scope.ClearSection();
			$scope.GetClassSetupAutoOrderNo(2);
		}

		document.getElementById('back-btn-section').onclick = function () {
			document.getElementById('section-content').style.display = "block";
			document.getElementById('section-form').style.display = "none";
			$scope.ClearSection();
		}


		// // academicyear 

		document.getElementById('add-academicyear').onclick = function () {
			document.getElementById('academicyear-section').style.display = "none";
			document.getElementById('academicyear-form').style.display = "block";
			$scope.ClearAcademicYear();
			$scope.GetClassSetupAutoOrderNo(8);
		}

		document.getElementById('back-btn-academicyear').onclick = function () {
			document.getElementById('academicyear-section').style.display = "block";
			document.getElementById('academicyear-form').style.display = "none";
			$scope.ClearAcademicYear();
		}

		// // board section

		document.getElementById('add-board').onclick = function () {
			document.getElementById('board-section').style.display = "none";
			document.getElementById('board-form').style.display = "block";
			$scope.ClearBoard();
			$scope.GetClassSetupAutoOrderNo(9);
		}

		document.getElementById('back-btn-board').onclick = function () {
			document.getElementById('board-section').style.display = "block";
			document.getElementById('board-form').style.display = "none";
			$scope.ClearBoard();
		}

		//class group section
		document.getElementById('add-classgroup').onclick = function () {
			document.getElementById('classgroup-section').style.display = "none";
			document.getElementById('classgroup-form').style.display = "block";
			$scope.ClearClassGroups();
			$scope.GetClassSetupAutoOrderNo(10);
		}

		document.getElementById('back-btn-classgroup').onclick = function () {
			document.getElementById('classgroup-section').style.display = "block";
			document.getElementById('classgroup-form').style.display = "none";
			$scope.ClearClassGroups();
		}
	}

	$scope.ClearClass = function () {
		$timeout(function () {
			$scope.newClass = {
				ClassId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				IsPassOut: false,
				ClassYearList: [],
				SemesterList: [],
				AcademicMonthColl: [],
				ActiveFeeMapppingMonth: false,
				IsActive: true,
				FacultyColl: [],
				BoardColl: [],
				Mode: 'Save'
			};
			$scope.newClass.AcademicMonthColl.push({ MonthId: 0, Name: '' });

			angular.forEach($scope.SemesterList, function (sem) {
				$scope.newClass.SemesterList.push({
					id: sem.id,
					Name: sem.Name,
					Selected:false
				});
			});

			angular.forEach($scope.ClassYearList, function (sem) {
				$scope.newClass.ClassYearList.push({
					id: sem.id,
					Name: sem.Name,
					Selected: false
				});
			});


			setTimeout(function () {
				$('.select2').val(null).trigger('change');
			}, 100);
		});
		
	}
	$scope.ClearSection = function () {
		$timeout(function () {
			$scope.newSection = {
				SectionId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});
		
	}
	$scope.ClearClassYear = function () {
		$timeout(function () {
			$scope.newClassYear = {
				ClassYearId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});

	}
	$scope.ClearLevel = function () {
		$timeout(function () {
			$scope.newLevel = {
				LevelId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});

	}

	$scope.ClearFaculty = function () {
		$timeout(function () {
			$scope.newFaculty = {
				FacultyId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});

	}

	$scope.ClearSemester = function () {
		$timeout(function () {
			$scope.newSemester = {
				SemesterId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});

	}

	$scope.ClearBatch = function () {
		$timeout(function () {
			$scope.newBatch = {
				BatchId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});

	}

	$scope.ClearAcademicYear = function () {
		$timeout(function () {
			$scope.newAcademicYear = {
				AcademicYearId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				IsRunning: false,
				CostClassId:null,
				Mode: 'Save'
			};
		});
	
	}
	$scope.ClearBoard = function () {
		$timeout(function () {
			$scope.newBoard = {
				BoardId: null,
				Name: '',
				Description: '',
				OrderNo: 0,
				Mode: 'Save'
			};
		});
		

	
	}

	$scope.ClearClassGroups = function () {
		$scope.newClassGroups = {
			ClassGroupId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			Mode: 'Save',
			ClassList: [],
			CheckAll: false,

			ClassGroupsDetailsColl: []
		};
		$timeout(function () {
			angular.forEach($scope.ClassList, function (cl) {
				cl.IsChecked = false;
				$scope.newClassGroups.ClassList.push(cl);
			})
		});

	}
	//************************* Class *********************************

	$scope.IsValidClass = function () {
		if ($scope.newClass.Name.isEmpty()) {
			Swal.fire('Please ! Enter Class Name');
			return false;
		}

		let monthSet = new Set();
		for (let i = 0; i < $scope.newClass.AcademicMonthColl.length; i++) {
			let msNo = $scope.newClass.AcademicMonthColl[i].MSNo;
			if (!msNo) continue; // skip if empty
			if (monthSet.has(msNo)) {
				Swal.fire('Duplicate "For Month" found in row ' + (i + 1));
				return false;
			}
			monthSet.add(msNo);
		}

		return true;
	}

	$scope.SaveUpdateClass = function () {
		if ($scope.IsValidClass() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newClass.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateClass();
					}
				});
			} else
				$scope.CallSaveUpdateClass();

		}
	};

	$scope.CallSaveUpdateClass = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newClass.ClassYearIdColl = [];
		$scope.newClass.ClassSemesterIdColl = [];

		if ($scope.newClass.ClassType == 3) {
			angular.forEach($scope.newClass.SemesterList, function (sem) {
				if (sem.Selected == true)
					$scope.newClass.ClassSemesterIdColl.push(sem.id);
			});
		} else if ($scope.newClass.ClassType == 2) {
			angular.forEach($scope.newClass.ClassYearList, function (sem) {
				if (sem.Selected == true)
					$scope.newClass.ClassYearIdColl.push(sem.id);
			});
        }

		if ($scope.newClass.AcademicMonthColl) {
			if ($scope.newClass.AcademicMonthColl.length > 0) {
				var msno = 1;
				$scope.newClass.AcademicMonthColl.forEach(function (cm) {
					cm.MonthId = msno;

					if (cm.StartDateDet && cm.StartDate_TMP && cm.StartDate_TMP.toString().length > 0) {
						cm.StartDate = $filter('date')(new Date(cm.StartDateDet.dateAD), 'yyyy-MM-dd');
					} else
						cm.StartDate = null;

					if (cm.EndDateDet && cm.EndDate_TMP && cm.EndDate_TMP.toString().length > 0) {
						cm.EndDate = $filter('date')(new Date(cm.EndDateDet.dateAD), 'yyyy-MM-dd');
					} else
						cm.EndDate = null;
						 
					msno++;
				});
            }
		}

		//if ($scope.newClass.FacultyColl)
		//	$scope.newClass.Faculty = $scope.newClass.FacultyColl.toString();
		//else
		//	$scope.newClass.Faculty = '';

		if ($scope.newClass.BoardColl)
			$scope.newClass.Board = $scope.newClass.BoardColl.toString();
		else
			$scope.newClass.Board = '';

		if ($scope.newClass.StartDateDet && $scope.newClass.StartDate_TMP && $scope.newClass.StartDate_TMP.toString().length > 0) {
			$scope.newClass.StartDate = $filter('date')(new Date($scope.newClass.StartDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newClass.StartDate = null;

		if ($scope.newClass.EndDateDet && $scope.newClass.EndDate_TMP && $scope.newClass.EndDate_TMP.toString().length > 0) {
			$scope.newClass.EndDate = $filter('date')(new Date($scope.newClass.EndDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newClass.EndDate = null;

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveClass",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newClass }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearClass();
				$scope.GetAllClassList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllClassList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
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
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetClassById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ClassId: refData.ClassId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetClassById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newClass = res.data.Data;
				$scope.newClass.Mode = 'Modify';
				$scope.newClass.SemesterList = [];
				$scope.newClass.ClassYearList = [];

				var semQry = mx($scope.newClass.ClassSemesterIdColl);
				var cyQry = mx($scope.newClass.ClassYearIdColl);

				angular.forEach($scope.SemesterList, function (sem) {
					$scope.newClass.SemesterList.push({
						id: sem.id,
						Name: sem.Name,
						Selected: semQry.contains(sem.id)
					});
				});

				angular.forEach($scope.ClassYearList, function (sem) {
					$scope.newClass.ClassYearList.push({
						id: sem.id,
						Name: sem.Name,
						Selected: cyQry.contains(sem.id)
					});
				});

				if (!$scope.newClass.AcademicMonthColl || $scope.newClass.AcademicMonthColl.length == 0) {
					$scope.newClass.AcademicMonthColl = [];
					$scope.newClass.AcademicMonthColl.push({ MonthId: 0, Name: '' });
				}
				var rdata = res.data.Data;
				//if (rdata.Faculty) {
				//	$scope.newClass.FacultyColl = rdata.Faculty.split(',').map(Number);

				//	setTimeout(function () {
				//		$('.select2').trigger('change');
				//	}, 100);
				//}

				if ($scope.newClass.StartDate)
					$scope.newClass.StartDate_TMP = new Date($scope.newClass.StartDate);

				if ($scope.newClass.EndDate)
					$scope.newClass.EndDate_TMP = new Date($scope.newClass.EndDate);

				$scope.newClass.AcademicMonthColl.forEach(function (am) {
					if (am.StartDate)
						am.StartDate_TMP = new Date(am.StartDate);

					if (am.EndDate)
						am.EndDate_TMP = new Date(am.EndDate);

				});
				if (rdata.Board) {
					$scope.newClass.BoardColl = rdata.Board.split(',').map(Number);

					setTimeout(function () {
						$('.select2').trigger('change');
					}, 100);
				}

				//New Code added for select2
				$timeout(function () {
					$('.select2').each(function () {
						if ($(this).hasClass('select2-hidden-accessible')) {
							$(this).select2('destroy');
						}
						$(this).select2({ width: '100%' });
					});
				}, 0);
				//Ends
				 
				document.getElementById('class-section').style.display = "none";
				document.getElementById('class-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelClassById = function (refData) {

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
					ClassId: refData.ClassId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelClass",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllClassList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Section *********************************

	$scope.IsValidSection = function () {
		if ($scope.newSection.Name.isEmpty()) {
			Swal.fire('Please ! Enter Section Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateSection = function () {
		if ($scope.IsValidSection() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSection.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSection();
					}
				});
			} else
				$scope.CallSaveUpdateSection();

		}
	};

	$scope.CallSaveUpdateSection = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveSection",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSection }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearSection();
				$scope.GetAllSectionList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllSectionList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SectionList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllSectionList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SectionList = res.data.Data;
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetSectionById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			SectionId: refData.SectionId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetSectionById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSection = res.data.Data;
				$scope.newSection.Mode = 'Modify';

				document.getElementById('section-content').style.display = "none";
				document.getElementById('section-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelSectionById = function (refData) {

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
					SectionId: refData.SectionId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelSection",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllSectionList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* ClassYear *********************************

	$scope.IsValidClassYear = function () {
		if ($scope.newClassYear.Name.isEmpty()) {
			Swal.fire('Please ! Enter ClassYear Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateClassYear = function () {
		if ($scope.IsValidClassYear() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newClassYear.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateClassYear();
					}
				});
			} else
				$scope.CallSaveUpdateClassYear();

		}
	};

	$scope.CallSaveUpdateClassYear = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveClassYear",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newClassYear }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearClassYear();
				$scope.GetAllClassYearList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllClassYearList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ClassYearList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassYearList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassYearList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetClassYearById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ClassYearId: refData.ClassYearId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetClassYearById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newClassYear = res.data.Data;
				$scope.newClassYear.Mode = 'Modify';

				document.getElementById('classyear-content').style.display = "none";
				document.getElementById('classyear-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelClassYearById = function (refData) {

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
					ClassYearId: refData.ClassYearId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelClassYear",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllClassYearList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//************************* Level *********************************

	$scope.IsValidLevel = function () {
		if ($scope.newLevel.Name.isEmpty()) {
			Swal.fire('Please ! Enter Level Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateLevel = function () {
		if ($scope.IsValidLevel() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newLevel.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateLevel();
					}
				});
			} else
				$scope.CallSaveUpdateLevel();

		}
	};

	$scope.CallSaveUpdateLevel = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveClassLevel",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newLevel }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearLevel();
				$scope.GetAllLevelList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllLevelList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.LevelList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassLevelList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.LevelList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetLevelById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			LevelId: refData.LevelId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetClassLevelById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newLevel = res.data.Data;
				$scope.newLevel.Mode = 'Modify';

				document.getElementById('level-content').style.display = "none";
				document.getElementById('level-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelLevelById = function (refData) {

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
					LevelId: refData.LevelId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelClassLevel",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllLevelList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Faculty *********************************

	$scope.IsValidFaculty = function () {
		if ($scope.newFaculty.Name.isEmpty()) {
			Swal.fire('Please ! Enter Faculty Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateFaculty = function () {
		if ($scope.IsValidFaculty() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newFaculty.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateFaculty();
					}
				});
			} else
				$scope.CallSaveUpdateFaculty();

		}
	};

	$scope.CallSaveUpdateFaculty = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveFaculty",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newFaculty }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearFaculty();
				$scope.GetAllFacultyList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllFacultyList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FacultyList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllFacultyList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FacultyList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetFacultyById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			FacultyId: refData.FacultyId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetFacultyById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFaculty = res.data.Data;
				$scope.newFaculty.Mode = 'Modify';

				document.getElementById('faculty-content').style.display = "none";
				document.getElementById('faculty-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelFacultyById = function (refData) {

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
					FacultyId: refData.FacultyId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelFaculty",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFacultyList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	//************************* Semester *********************************

	$scope.IsValidSemester = function () {
		if ($scope.newSemester.Name.isEmpty()) {
			Swal.fire('Please ! Enter Semester Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateSemester = function () {
		if ($scope.IsValidSemester() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newSemester.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateSemester();
					}
				});
			} else
				$scope.CallSaveUpdateSemester();

		}
	};

	$scope.CallSaveUpdateSemester = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveSemester",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newSemester }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearSemester();
				$scope.GetAllSemesterList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllSemesterList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.SemesterList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllSemesterList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SemesterList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetSemesterById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			SemesterId: refData.SemesterId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetSemesterById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newSemester = res.data.Data;
				$scope.newSemester.Mode = 'Modify';

				document.getElementById('semester-content').style.display = "none";
				document.getElementById('semester-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelSemesterById = function (refData) {

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
					SemesterId: refData.SemesterId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelSemester",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllSemesterList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Batch *********************************

	$scope.IsValidBatch = function () {
		if ($scope.newBatch.Name.isEmpty()) {
			Swal.fire('Please ! Enter Batch Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateBatch = function () {
		if ($scope.IsValidBatch() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBatch.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBatch();
					}
				});
			} else
				$scope.CallSaveUpdateBatch();

		}
	};

	$scope.CallSaveUpdateBatch = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveBatch",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newBatch }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBatch();
				$scope.GetAllBatchList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllBatchList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BatchList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllBatchList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BatchList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetBatchById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BatchId: refData.BatchId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetBatchById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBatch = res.data.Data;
				$scope.newBatch.Mode = 'Modify';

				document.getElementById('batch-content').style.display = "none";
				document.getElementById('batch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBatchById = function (refData) {

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
					BatchId: refData.BatchId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelBatch",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBatchList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};



	//************************* Academic Year *********************************

	$scope.IsValidAcademicYear = function () {
		if (!$scope.newAcademicYear.Name || $scope.newAcademicYear.Name.toString().isEmpty()) {
			Swal.fire('Please ! Enter AcademicYear Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateAcademicYear = function () {
		if ($scope.IsValidAcademicYear() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newAcademicYear.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateAcademicYear();
					}
				});
			} else
				$scope.CallSaveUpdateAcademicYear();

		}
	};

	$scope.CallSaveUpdateAcademicYear = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.newAcademicYear.StartDateDet) {
			$scope.newAcademicYear.StartDate = $filter('date')(new Date($scope.newAcademicYear.StartDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newAcademicYear.StartDate = null;

		if ($scope.newAcademicYear.EndDateDet) {
			$scope.newAcademicYear.EndDate = $filter('date')(new Date($scope.newAcademicYear.EndDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newAcademicYear.EndDate = null;

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveAcademicYear",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newAcademicYear }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearAcademicYear();
				$scope.GetAllAcademicYearList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllAcademicYearList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AcademicYearList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllAcademicYearList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AcademicYearList = res.data.Data;
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAcademicYearById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			AcademicYearId: refData.AcademicYearId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAcademicYearById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAcademicYear = res.data.Data;
				$scope.newAcademicYear.Mode = 'Modify';

				$scope.newAcademicYear.Name = parseInt(res.data.Data.Name);

				if($scope.newAcademicYear.StartDate)
					$scope.newAcademicYear.StartDate_TMP = new Date($scope.newAcademicYear.StartDate);

				if($scope.newAcademicYear.EndDate)
					$scope.newAcademicYear.EndDate_TMP = new Date($scope.newAcademicYear.EndDate);

				document.getElementById('academicyear-section').style.display = "none";
				document.getElementById('academicyear-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelAcademicYearById = function (refData) {

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
					AcademicYearId: refData.AcademicYearId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelAcademicYear",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllAcademicYearList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Board *********************************

	$scope.IsValidBoard = function () {
		if ($scope.newBoard.Name.isEmpty()) {
			Swal.fire('Please ! Enter Board Name');
			return false;
		}



		return true;
	}

	$scope.SaveUpdateBoard = function () {
		if ($scope.IsValidBoard() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBoard.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBoard();
					}
				});
			} else
				$scope.CallSaveUpdateBoard();

		}
	};

	$scope.CallSaveUpdateBoard = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveBoard",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newBoard }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearBoard();
				$scope.GetAllBoardList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllBoardList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.BoardList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllBoardList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BoardList = res.data.Data;
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetBoardById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			BoardId: refData.BoardId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetBoardById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBoard = res.data.Data;
				$scope.newBoard.Mode = 'Modify';

				document.getElementById('board-section').style.display = "none";
				document.getElementById('board-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelBoardById = function (refData) {

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
					BoardId: refData.BoardId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelBoard",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllBoardList();
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


	$scope.AddClassMonth = function (ind) {
		if ($scope.newClass.AcademicMonthColl) {
			if ($scope.newClass.AcademicMonthColl.length > ind + 1) {
				$scope.newClass.AcademicMonthColl.splice(ind + 1, 0, {
					MonthId:0,
					Name:'',
				})
			} else {
				$scope.newClass.AcademicMonthColl.push({
					MonthId: 0,
					Name: '',
				})
			}
		}
	};
	$scope.delClassMonth = function (ind) {
		if ($scope.newClass.AcademicMonthColl) {
			if ($scope.newClass.AcademicMonthColl.length > 1) {
				$scope.newClass.AcademicMonthColl.splice(ind, 1);
			}
		}
	};


	//************************* Class Group *********************************
	$scope.CheckAllGroup = function () {
		angular.forEach($scope.newClassGroups.ClassList, function (cl) {
			cl.IsChecked = $scope.newClassGroups.CheckAll;
		});
	};

	$scope.IsValidClassGroups = function () {
		if ($scope.newClassGroups.Name.isEmpty()) {
			Swal.fire('Please ! Enter ClassGroups Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateClassGroups = function () {
		if ($scope.IsValidClassGroups() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newClassGroups.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateClassGroups();
					}
				});
			} else
				$scope.CallSaveUpdateClassGroups();

		}
	};

	$scope.CallSaveUpdateClassGroups = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newClassGroups.ClassGroupsDetailsColl = [];

		$scope.newClassGroups.ClassGroupsDetailsColl = []; // Clear array before pushing

		angular.forEach($scope.newClassGroups.ClassList, function (cl) {
			if (cl.IsChecked === true) {
				$scope.newClassGroups.ClassGroupsDetailsColl.push(
					cl.ClassId
				);
			}
		});


		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveClassGroups",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newClassGroups }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess === true) {
				$scope.ClearClassGroups();
				$scope.GetAllClassGroupsList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	};


	$scope.GetAllClassGroupsList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ClassGroupsList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassGroups",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassGroupsList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	//$scope.GetClassGroupsById = function (refData) {

	//	$scope.loadingstatus = "running";
	//	showPleaseWait();

	//	var para = {
	//		ClassGroupId: refData.ClassGroupId
	//	};

	//	$http({
	//		method: 'POST',
	//		url: base_url + "Academic/Creation/GetClassGroupsById",
	//		dataType: "json",
	//		data: JSON.stringify(para)
	//	}).then(function (res) {
	//		hidePleaseWait();
	//		$scope.loadingstatus = "stop";
	//		if (res.data.IsSuccess && res.data.Data) {
	//			$scope.newClassGroups = res.data.Data;
	//			$scope.newClassGroups.Mode = 'Modify';
	//			$scope.newClassGroups.ClassList = [];
	//			var classColl = mx(res.data.Data.ClassGroupsDetailsColl);
	//			$timeout(function () {
	//				angular.forEach($scope.ClassList, function (cl) {
	//					if (classColl.contains(cl.ClassId)) {
	//						cl.IsChecked = true;
	//					} else
	//						cl.IsChecked = false;

	//					$scope.newClassGroups.ClassList.push(cl);
	//				})
	//			});
	//			document.getElementById('classgroup-section').style.display = "none";
	//			document.getElementById('classgroup-form').style.display = "block";

	//		} else {
	//			Swal.fire(res.data.ResponseMSG);
	//		}

	//	}, function (reason) {
	//		Swal.fire('Failed' + reason);
	//	});
	//};

	$scope.GetClassGroupsById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ClassGroupId: refData.ClassGroupId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetClassGroupsById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				$scope.newClassGroups = res.data.Data;
				$scope.newClassGroups.Mode = 'Modify';
				$scope.newClassGroups.ClassList = [];

				var classColl = mx(res.data.Data.ClassGroupsDetailsColl);
				$timeout(function () {
					let allChecked = true;
					angular.forEach($scope.ClassList, function (cl) {
						if (classColl.contains(cl.ClassId)) {
							cl.IsChecked = true;
						} else {
							cl.IsChecked = false;
							allChecked = false;
						}
						$scope.newClassGroups.ClassList.push(cl);
						$scope.newClassGroups.CheckAll = allChecked;
					})
				});

				document.getElementById('classgroup-section').style.display = "none";
				document.getElementById('classgroup-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed: ' + reason);
		});
	};

	$scope.DelClassGroupsById = function (refData) {
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
					ClassGroupId: refData.ClassGroupId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelClassGroups",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllClassGroupsList();

					}
					Swal.fire(res.data.ResponseMSG);


				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};
	
	$scope.validateDates = function (changedField) {
		if (!$scope.newAcademicYear.StartDateDet || !$scope.newAcademicYear.EndDateDet ||
			!$scope.newAcademicYear.StartDateDet.dateAD || !$scope.newAcademicYear.EndDateDet.dateAD) {
			return true;
		}
		var StartDate = $filter('date')(new Date($scope.newAcademicYear.StartDateDet.dateAD), 'yyyy-MM-dd');
		var EndDate = $filter('date')(new Date($scope.newAcademicYear.EndDateDet.dateAD), 'yyyy-MM-dd');
		if (!StartDate || !EndDate) return true;
		if (StartDate > EndDate) {
			if (changedField === 'StartDate') {
				Swal.fire({
					icon: 'warning',
					text: 'Start Date cannot be After End Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newAcademicYear.StartDate_TMP = new Date();
						$scope.newAcademicYear.StartDateDet = new Date();
					});
				});
			} else if (changedField === 'EndDate') {
				Swal.fire({
					icon: 'warning',
					text: 'End Date cannot be Before Start Date.',
					confirmButtonText: 'OK'
				}).then(function () {
					$scope.$apply(function () {
						$scope.newAcademicYear.EndDate_TMP = new Date();
						$scope.newAcademicYear.EndDateDet = new Date();
					});
				});
			}
			return false;
		}

		return true;
	};
});