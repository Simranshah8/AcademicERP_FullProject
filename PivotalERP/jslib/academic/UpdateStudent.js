app.controller('UpdateStudentController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, Excel, $translate) {
	$scope.Title = 'Update Student';

	//OnClickDefault();
	var gSrv = GlobalServices;


	$rootScope.ConfigFunction = function () {
		var keyColl = $translate.getTranslationTable();

		var Labels = {
			RegdNo: keyColl['REGDNO_LNG'],
			Cast: keyColl['CAST_LNG'],
			CITIZENSHIP: keyColl['CITIZENSHIP_LNG'],
			LOCAL_LEVEL: keyColl['LOCAL_LEVEL_LNG'],
			PROVINCE: keyColl['PROVINCE_LNG']
		};

	};
	$rootScope.ChangeLanguage();

	$scope.LoadData = function () {


		$('.select2').select2();

		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();
		$scope.GenderColl = GlobalServices.getGenderList();

		$scope.BloodGroupList = GlobalServices.getBloodGroupList();
		$scope.DisablityList = GlobalServices.getDisablityList();

		$scope.CasteList = [];
		GlobalServices.getCasteList().then(function (res) {
			$scope.CasteList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassListRunning = [];
		gSrv.getClassSectionList(true).then(function (res) {
			$scope.ClassListRunning = res.data.Data;			
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
			$scope.AllClassList = mx(res.data.Data.ClassList);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.SectionList = [];
		gSrv.getSectionList().then(function (res) {
			$scope.SectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.HouseNameList = [];
		gSrv.getHouseNameList().then(function (res) {
			$scope.HouseNameList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.MediumList = [];
		gSrv.getMediumList().then(function (res) {
			$scope.MediumList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.StudentTypeList = [];
		GlobalServices.getStudentTypeList().then(function (res) {
			$scope.StudentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.currentPages = {

			UpdateStudent: 1,
			BoardRegdNo: 1,
			LeftStudent: 1,
			Passout: 1
		};

		$scope.searchData = {

			UpdateStudent: '',
			BoardRegdNo: '',
			LeftStudent: '',
			Passout: ''
		};

		$scope.perPage = {

			UpdateStudent: gSrv.getPerPageRow(),
			BoardRegdNo: gSrv.getPerPageRow(),
			LeftStudent: gSrv.getPerPageRow(),
			Passout: gSrv.getPerPageRow()
		};

		$scope.newUpdateStudent = {
			UpdateStudentId: null,
			FreezCol: 1,
			Mode: 'Accept'
		};

		

		$scope.newBoardRegdNo = {
			BoardRegdNoId: null,
			BoardRegdNoDetailsColl: [],
			Mode: 'Save'

		};
		$scope.newBoardRegdNo.BoardRegdNoDetailsColl.push({});

		$scope.newLeftStudent = {
			LeftStudentId: null,

			LeftStudentDetailsColl: [],
			Mode: 'Accept'
		};
		$scope.newLeftStudent.LeftStudentDetailsColl.push({});

		//$scope.GetAllUpdateStudentList();
		//$scope.GetAllLeftStudentList();
		//$scope.GetAllBoardRegdNoList();

		$scope.SortAsList = [{ text: 'RollNo', value: 'RollNo' }, { text: 'Name', value: 'FirstName' }, { text: 'RegNo', value: 'RegNo' }, { text: 'Section', value: 'SectionId' },
		{ text: 'Section&RollNo', value: 'SectionId,RollNo' }
		];

		$scope.StatusList = [{ id: null, text: '**select status**' },{ id: 1, text: 'Passout' }, { id: 2, text: 'Dropout' }]
		$scope.PassoutoptList = [
			{ id: 1, text: 'Regular Passout'},
			{ id: 2, text: 'Late Passout'},
			{ id: 3, text: 'Supplementary Passout'},
			{ id: 4, text: 'Reassessment Passout'},
			{ id: 5, text: 'Migration Passout'}
		]

		$scope.DropoutOptList = [
			{ id: 1, text: 'Voluntary Dropout'},
			{ id: 2, text: 'Academic Dropout'},
			{ id: 3, text: 'Financial Dropout '},
			{ id: 4, text: 'Transfer Dropout'},
			{ id: 5, text: ' Disciplinary Dropout' },
			{ id: 6, text: 'Health-Related Dropout' },
			{ id: 7, text: 'Non-Attendance Dropout' }
		]




		$scope.BranchColl = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchListForEntry",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchColl = res.data.Data;
			}
		}, function (reason) {
			alert('Failed' + reason);
		});

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
				/*$scope.SelectedClassClassYearList = [];*/
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newPassout = {
			UpdatePassoutStudentList: [],
			Mode: 'Update'

		};
	}

	$scope.DataSorting = function () {

		var sortOptions = $scope.newUpdateStudent.SortAs.split(",");

		//var dataColl = $filter('orderBy')($filter('filter')($scope.UpdateStudentList, $scope.searchData.UpdateStudent), $scope.newUpdateStudent.SortAs, $scope.reverse);
		var dataColl = $filter('orderBy')($scope.UpdateStudentList, sortOptions);
		$scope.UpdateStudentList = [];
		$timeout(function () {
			$scope.$apply(function () {
				angular.forEach(dataColl, function (dc) {
					$scope.UpdateStudentList.push(dc);
				})
			});
		});

		$timeout(function () {
			$scope.$broadcast('refreshFixedColumns');
		});
	};

	$scope.ReArrangeRollNo = function () {
		var sortOptions = [];
		var dataColl = [];
		var newROll = [];
		if ($scope.newUpdateStudent.RollNoAs == 1) {
			sortOptions = ['SectionId', 'RollNo'];
			dataColl = mx($filter('orderBy')($scope.UpdateStudentList, sortOptions));

			var query = dataColl.groupBy(t => t.SectionId).toArray();
			angular.forEach(query, function (q) {
				var r = 1;
				angular.forEach(q.elements, function (el) {
					newROll.push({
						StudentId: el.StudentId,
						RollNo: r
					});
					r++;
				});
			});

		} else if ($scope.newUpdateStudent.RollNoAs == 2) {
			sortOptions = ['RollNo'];
			dataColl = $filter('orderBy')($scope.UpdateStudentList, sortOptions);

			var r = 1;
			angular.forEach(dataColl, function (el) {
				newROll.push({
					StudentId: el.StudentId,
					RollNo: r
				});
				r++;
			});
		}

		newROll = mx(newROll);
		angular.forEach($scope.UpdateStudentList, function (st) {
			var findST = newROll.firstOrDefault(p1 => p1.StudentId == st.StudentId);
			if (findST)
				st.RollNo = findST.RollNo;
		});


		//var dataColl = $filter('orderBy')($filter('filter')($scope.UpdateStudentList, $scope.searchData.UpdateStudent), $scope.newUpdateStudent.SortAs, $scope.reverse);

	};
	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa

		$timeout(function () {
			$scope.$broadcast('refreshFixedColumns');
		});
	}

	$scope.freezColumns = function () {
		$timeout(function () {
			$scope.$broadcast('refreshFixedColumns');
		});

	};

	$scope.ClearUpdateStudent = function () {
		$scope.newUpdateStudent = {
			UpdateStudentId: null,

			Mode: 'Accept'
		};
	}
	$scope.ClearBoardRegdNo = function () {
		$scope.newBoardRegdNo.StartNo = '';
		$scope.newBoardRegdNo.PadWidth = '';
		$scope.newBoardRegdNo.Prefix = '';
		$scope.newBoardRegdNo.Suffix = '';
		if ($scope.newBoardRegdNo && $scope.newBoardRegdNo.BoardRegdNoDetailsColl) {
			$scope.newBoardRegdNo.BoardRegdNoDetailsColl.forEach(function (item) {
				item.BoardRegdNo = ""; // clear the input field
			});
		}
	};

	$scope.ClearLeftStudent = function () {
		$scope.newLeftStudent = {
			LeftStudentId: null,

			LeftStudentDetailsColl: [],
			Mode: 'Accept'
		};
		$scope.newLeftStudent.LeftStudentDetailsColl.push({});
	}

	//************************* Update Student *********************************



	//$scope.IsValidUpdateStudent = function () {
	//	if ($scope.newUpdateStudent.ClassId.isEmpty()) {
	//		Swal.fire('Please ! Select Class');
	//		return false;
	//	}

	//	return true;
	//}

	$scope.GetStudentForUpdate = function () {
		$scope.UpdateStudentList = [];
		if ($scope.newUpdateStudent.SelectedClass) {
			var para = {
				ClassId: $scope.newUpdateStudent.SelectedClass.ClassId,
				SectionId: $scope.newUpdateStudent.SelectedSection ? $scope.newUpdateStudent.SelectedSection.SectionId : 0,
				BatchId: $scope.newUpdateStudent.BatchId,
				ClassYearId: $scope.newUpdateStudent.ClassYearId,
				SemesterId: $scope.newUpdateStudent.SemesterId,
				BranchId:$scope.newUpdateStudent.BranchId,
				//ClassId: $scope.newUpdateStudent.SelectedClass.ClassId,
				//SectionId: $scope.newUpdateStudent.SelectedClass.SectionId

			};

			$scope.loadingstatus = "running";
			//showPleaseWait();


			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetStudentForUpdate",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.UpdateStudentList = res.data.Data.DataColl;

					$timeout(function () {
						angular.forEach($scope.UpdateStudentList, function (st) {
							if (st.DOB_AD)
								st.DOBAD_TMP = new Date(st.DOB_AD);
						});
					});


					//$timeout(function () {
					//	$scope.$broadcast('refreshFixedColumns');
					//});
					//$timeout(function () {

					//	$('#main-table').fixedHeaderTable({						
					//		fixedColumns: $scope.newUpdateStudent.FreezCol
					//	});
					//});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}
	}

	$scope.UpdateClassWiseST = function () {

		Swal.fire({
			title: 'Do you want to update selected students record?',
			showCancelButton: true,
			confirmButtonText: 'Update',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				//Added on 28 baishakh
				var filteredStudents = $filter('filter')(
					$scope.UpdateStudentList,
					$scope.searchData.UpdateStudent
				);
				//Ends

				/*angular.forEach($scope.UpdateStudentList, function (ut) {*/
				angular.forEach(filteredStudents, function (ut) {
					if (ut.DOB_ADDet)
						ut.DOB_AD = $filter('date')(new Date(ut.DOB_ADDet.dateAD), 'yyyy-MM-dd');
					else if (ut.DOBAD_TMP)
						ut.DOB_AD = $filter('date')(new Date(ut.DOBAD_TMP), 'yyyy-MM-dd');
					else if (ut.DOBAD)
						ut.DOB_AD = $filter('date')(new Date(ut.DOBAD), 'yyyy-MM-dd');
					else
						ut.DOB_AD = null;					
				});

				var para = {
					studentColl: filteredStudents
					/*studentColl: $scope.UpdateStudentList*/
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/UpdateClassWiseST",
					dataType: "json",
					data: angular.toJson(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.SaveUpdateUpdateStudent = function () {
		if ($scope.IsValidUpdateStudent() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newUpdateStudent.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateUpdateStudent();
					}
				});
			} else
				$scope.CallSaveUpdateUpdateStudent();

		}
	};

	$scope.CallSaveUpdateUpdateStudent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveUpdateStudent",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newUpdateStudent }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearUpdateStudent();
				$scope.GetAllUpdateStudentList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}





	//************************* Board Regd No *********************************

	$scope.IsValidBoardRegdNo = function () {
		let map = {};

		$scope.newBoardRegdNo.BoardRegdNoDetailsColl.forEach(br => {
			let val = (br.BoardRegdNo || '').trim();
			if (!val) return;
			(map[val] = map[val] || []).push(br.Name);
		});

		let msg = Object.keys(map)
			.filter(k => map[k].length > 1)
			.map(k => k + ' is used by: ' + map[k].join(', '))
			.join('<br/>');

		if (msg) {
			Swal.fire({ icon: 'warning', title: 'Duplicates Regd No Found', html: msg });
			return false;
		}

		return true;
	};



	$scope.AutoGenerateRegdNo = function () {

		if ($scope.newBoardRegdNo && $scope.newBoardRegdNo.BoardRegdNoDetailsColl && $scope.newBoardRegdNo.BoardRegdNoDetailsColl.length > 0) {
			var startNo = parseInt($scope.newBoardRegdNo.StartNo);
			var pad = $scope.newBoardRegdNo.PadWidth;
			if (isNaN(startNo))
				startNo = 0;

			var p = '';
			if ($scope.newBoardRegdNo.Prefix)
				p = $scope.newBoardRegdNo.Prefix;

			var s = '';
			if ($scope.newBoardRegdNo.Suffix)
				s = $scope.newBoardRegdNo.Suffix;

			angular.forEach($scope.newBoardRegdNo.BoardRegdNoDetailsColl, function (st) {
				st.BoardRegdNo = p + startNo.toString().padStart(pad, '0') + s;
				startNo++;
			});
		}

	};

	$scope.GetStudentLstForBoardRegd = function () {
		$scope.newBoardRegdNo.BoardRegdNoDetailsColl = [];
		if ($scope.newBoardRegdNo.SelectedClass) {
			var para = {
				ClassId: $scope.newBoardRegdNo.SelectedClass.ClassId,
				SectionId: $scope.newBoardRegdNo.SelectedClass.SectionId,
				All: false,
				SemesterId: $scope.newBoardRegdNo.SemesterId,
				ClassYearId: $scope.newBoardRegdNo.ClassYearId,
				BatchId: $scope.newBoardRegdNo.BatchId,
				BranchId:$scope.newBoardRegdNo.BranchId
			};

			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetClassWiseStudentForLeft",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newBoardRegdNo.BoardRegdNoDetailsColl = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}
	}

	$scope.SaveUpdateBoardRegdNo = function () {
		if ($scope.IsValidBoardRegdNo() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newBoardRegdNo.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateBoardRegdNo();
					}
				});
			} else
				$scope.CallSaveUpdateBoardRegdNo();

		}
	};

	$scope.CallSaveUpdateBoardRegdNo = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SaveStudentBoardReg",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newBoardRegdNo.BoardRegdNoDetailsColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	//************************* Left Student *********************************

	$scope.IsValidLeftStudent = function () {
		//if ($scope.newLeftStudent.Academic.isEmpty()) {
		//	Swal.fire('Please ! Select Academic Type');
		//	return false;
		//}

		return true;
	}

	$scope.GetStudentLstForLeft = function (semYear) {
		$scope.newLeftStudent.StudentColl = [];

		if ($scope.newLeftStudent.SelectedClass && semYear == true) {
			var findClass = $scope.AllClassList.firstOrDefault(p1 => p1.ClassId == $scope.newLeftStudent.SelectedClass.ClassId);
			if (findClass) {

				$scope.newLeftStudent.SelectedClass.ClassType = findClass.ClassType;

				var semQry = mx(findClass.ClassSemesterIdColl);
				var cyQry = mx(findClass.ClassYearIdColl);

				$scope.newLeftStudent.SelectedClassClassYearList = [];
				$scope.newLeftStudent.SelectedClassSemesterList = [];

				angular.forEach($scope.SemesterList, function (sem) {
					if (semQry.contains(sem.id)) {
						$scope.newLeftStudent.SelectedClassSemesterList.push({
							id: sem.id,
							text: sem.text,
							SemesterId: sem.id,
							Name: sem.Name
						});
					}
				});

				angular.forEach($scope.ClassYearList, function (sem) {
					if (cyQry.contains(sem.id)) {
						$scope.newLeftStudent.SelectedClassClassYearList.push({
							id: sem.id,
							text: sem.text,
							ClassYearId: sem.id,
							Name: sem.Name
						});
					}
				});
			}
		}

		if ($scope.newLeftStudent.SelectedClass) {

			if ($scope.newLeftStudent.SelectedClass.ClassType == 2 && $scope.AcademicConfig.ActiveClassYear == true) {
				if ($scope.newLeftStudent.ClassYearId > 0) {

				} else {
					//Swal.fire('Please ! Select ClassYear');
					return;
				}

			}


			if ($scope.newLeftStudent.SelectedClass.ClassType == 3 && $scope.AcademicConfig.ActiveSemester == true) {
				if ($scope.newLeftStudent.SemesterId > 0) {

				} else {
					//Swal.fire('Please ! Select Semester');
					return;
				}

			}

			var para = {
				ClassId: $scope.newLeftStudent.SelectedClass.ClassId,
				SectionId: $scope.newLeftStudent.SelectedClass.SectionId,
				SemesterId: $scope.newLeftStudent.SemesterId,
				ClassYearId: $scope.newLeftStudent.ClassYearId,
				BatchId: $scope.newLeftStudent.BatchId,
				BranchId:$scope.newLeftStudent.BranchId,
			};

			$scope.loadingstatus = "running";
			showPleaseWait();


			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetClassWiseStudentForLeft",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newLeftStudent.StudentColl = res.data.Data;

					angular.forEach($scope.newLeftStudent.StudentColl, function (st) {

						st.ClassId = para.ClassId;
						st.SectionId = para.SectionId;

						if (st.IsLeft == true && st.LeftDate_AD)
							st.LeftDate_TMP = new Date(st.LeftDate_AD);
					});

					$scope.newLeftStudent.LeftCount = mx(res.data.Data).where(p1 => p1.IsLeft == true).count();
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}
	}

	$scope.CheckUnCheckIsLeft = function (det) {
		if (det.IsLeft == false) {
			det.LeftDate_TMP = null;
		}
	}
	$scope.ChangeStatus = function (det) {
		if (det.StatusId == 1 || det.StatusId == 2) {
			det.IsLeft = true;
		} else {
			det.IsLeft = false;
        }
    }

	$scope.SaveUpdateLeftStudent = function () {
		if ($scope.IsValidLeftStudent() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newLeftStudent.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateLeftStudent();
					}
				});
			} else
				$scope.CallSaveUpdateLeftStudent();

		}
	};

	
	$scope.CallSaveUpdateLeftStudent = function () {

		if (!$scope.newLeftStudent.SelectedClass || !$scope.newLeftStudent.SelectedClass.ClassId) {
			Swal.fire('Please Select the class');
			return; // stop execution
		}

		$scope.loadingstatus = "running";
		showPleaseWait();

		$timeout(function () {
			var dataColl = [];
			var cId = $scope.newLeftStudent.SelectedClass.ClassId;
			var sId = $scope.newLeftStudent.SelectedClass.SectionId;
			var semId = $scope.newLeftStudent.SemesterId;
			var clId = $scope.newLeftStudent.ClassYearId;
			var bid = $scope.newLeftStudent.BatchId;

			var isvalid = true;

			angular.forEach($scope.newLeftStudent.StudentColl, function (st) {
				if (st.LeftRemarks && st.StatusId>0 && st.LeftRemarks.isEmpty() == false && (st.LeftDateDet || st.LeftDate_TMP)) {
					var newSt = {
						SemesterId: semId,
						ClassYearId: clId,
						StudentId: st.StudentId,
						ClassId: cId,
						SectionId: sId,
						IsLeft: true,
						LeftDate_AD: $filter('date')(new Date((st.LeftDateDet ? st.LeftDateDet.dateAD : st.LeftDate_TMP)), 'yyyy-MM-dd'),
						LeftRemarks: st.LeftRemarks,
						StatusId: st.StatusId,
						BatchId:bid,
					};
					dataColl.push(newSt);
				} else if (st.StatusId >0) {

					$scope.loadingstatus = "stop";
					hidePleaseWait();

					isvalid = false;
					Swal.fire('Please ! Enter Remarks and Date');
					return;
				}

			});

			if (isvalid == true) {
				if (dataColl.length == 0) {
					var newSt = {
						StudentId: 0,
						ClassId: cId,
						SectionId: sId,
						SemesterId: semId,
						ClassYearId: clId,
						IsLeft: true,
						LeftDate_AD: $filter('date')(new Date(), 'yyyy-MM-dd'),
						LeftRemarks: 'Remarks',
						StatusId: 0,
						BatchId: bid,
					};
					dataColl.push(newSt);
				}

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/SaveStudentLeft",
					headers: { 'Content-Type': undefined },

					transformRequest: function (data) {

						var formData = new FormData();
						formData.append("jsonData", angular.toJson(data.jsonData));

						return formData;
					},
					data: { jsonData: dataColl }
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


	}
	$scope.sortLeft = function (keyname) {
		$scope.sortKeyLeft = keyname;   //set the sortKey to the param passed
		$scope.reverse2 = !$scope.reverse2; //if true make it false and vice versa
	}

	$scope.sortBoard = function (keyname) {
		$scope.sortKeyBoard = keyname;   //set the sortKey to the param passed
		$scope.reverse1 = !$scope.reverse1; //if true make it false and vice versa
	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

	$scope.CheckUnCheckAllLeft = function (chkVal) {
		angular.forEach($scope.newLeftStudent.StudentColl, function (st) {
			st.IsLeft = chkVal;
		});
	}

	$scope.ChangeAllLeftDate = function () {

		if ($scope.newLeftStudent.LeftDateDet) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			angular.forEach($scope.newLeftStudent.StudentColl, function (st) {
				$timeout(function () {
					if (!st.LeftDate_TMP)
						st.LeftDate_TMP = new Date($scope.newLeftStudent.LeftDateDet.dateAD);
				});
			});

			$scope.loadingstatus = "stop";
			hidePleaseWait();

		}

	}
	$scope.ChangeAllLeftRemarks = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$timeout(function () {
			angular.forEach($scope.newLeftStudent.StudentColl, function (st) {
				st.LeftRemarks = $scope.newLeftStudent.LeftRemarks;
			});
		})

		$scope.loadingstatus = "stop";
		hidePleaseWait();

	}

	//Mew Code added by suresh on 9 Jestha starts
	$scope.ChangeAllStatus = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$timeout(function () {
			angular.forEach($scope.newLeftStudent.StudentColl, function (st) {
				st.StatusId = $scope.newLeftStudent.StatusId;
				st.IsLeft = true;
			});

			$scope.loadingstatus = "stop";
			hidePleaseWait();
		}, 0);
	};
	



	$scope.GetStudentForPassoutUpdate = function () {
		$scope.newPassout.UpdatePassoutStudentList = [];
		if ($scope.newPassout.SelectedClass) {
			var para = {
				ClassId: $scope.newPassout.SelectedClass.ClassId,
				SectionId: $scope.newPassout.SelectedSection ? $scope.newPassout.SelectedSection.SectionId : 0,
				All: true,
				SemesterId: $scope.newPassout.SemesterId,
				ClassYearId: $scope.newPassout.ClassYearId,
				BatchId: $scope.newPassout.BatchId,
				BranchId:$scope.newPassout.BranchId,
			};

			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetStudentForPassout",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newPassout.UpdatePassoutStudentList = res.data.Data;

					angular.forEach($scope.newPassout.UpdatePassoutStudentList, function (st) {

						st.ClassId = para.ClassId;
						st.SectionId = para.SectionId;


						if (st.IsLeft == true && st.LeftDate_AD)
							st.LeftDate_TMP = new Date(st.LeftDate_AD);

						
						if (st.PassoutDate)
							st.PassoutDate_TMP = new Date(st.PassoutDate);

					});

					$scope.newPassout.LeftCount = mx(res.data.Data).where(p1 => p1.IsLeft == true).count();

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}
	}


	$scope.IsValidPassoutStudent = function () {
	if (!$scope.newPassout.SelectedClass) {
        Swal.fire('Please select a Class!');
        return false;
    }

		return true;
	}

	$scope.SaveUpdatePassoutStudent = function () {
		if ($scope.IsValidPassoutStudent()) {
			// Always show confirmation before saving
			var saveModify = $scope.newPassout.Mode;
			Swal.fire({
				title: 'Do you want to save the data?',
				showCancelButton: true,
				confirmButtonText: 'Yes, Save',
				cancelButtonText: 'Cancel'
			}).then((result) => {
				if (result.isConfirmed) {
					$scope.CallSaveUpdatePassoutStudent();
				} else if (result.isDismissed) {
					Swal.fire('Save canceled', '', 'info');
				}
			});
		}
	};


	//$scope.SaveUpdatePassoutStudent = function () {
	//	if ($scope.IsValidPassoutStudent() == true) {
	//		if ($scope.confirmMSG.Accept == true) {
	//			var saveModify = $scope.newPassout.Mode;
	//			Swal.fire({
	//				title: 'Do you want to ' + saveModify + ' the current data?',
	//				showCancelButton: true,
	//				confirmButtonText: saveModify,
	//			}).then((result) => {
	//				/* Read more about isConfirmed, isDenied below */
	//				if (result.isConfirmed) {
	//					$scope.CallSaveUpdatePassoutStudent();
	//				}
	//			});
	//		} else
	//			$scope.CallSaveUpdatePassoutStudent();
	//	}
	//};

	$scope.CallSaveUpdatePassoutStudent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		angular.forEach($scope.newPassout.UpdatePassoutStudentList, function (ut) {
			if (ut.PassoutDateDet)
				ut.PassoutDate = $filter('date')(new Date(ut.PassoutDateDet.dateAD), 'yyyy-MM-dd');
			else if (ut.PassoutDate_TMP)
				ut.PassoutDate = $filter('date')(new Date(ut.PassoutDate_TMP), 'yyyy-MM-dd');
			else if (ut.PassoutDate)
				ut.PassoutDate = $filter('date')(new Date(ut.PassoutDate), 'yyyy-MM-dd');
			else
				ut.PassoutDate = null;
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Transaction/SavePassoutStudents",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: $scope.newPassout.UpdatePassoutStudentList }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}



	//New code added by suresh for update student Studentwise on Mangsir 5 Starts
	$scope.SaveUpdateStudentWise = function (student) {
		if (!student) {
			Swal.fire('No student data provided for update.');
			return;
		}
		Swal.fire({
			title: 'Do you want to update ' + student.FirstName + ' ' + student.MiddleName + ' ' + student.LastName + '\'s record ?',
			showCancelButton: true,
			confirmButtonText: 'Update',
		}).then((result) => {
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				// Format the student's date of birth
				if (student.DOB_ADDet?.dateAD) {
					student.DOB_AD = $filter('date')(new Date(student.DOB_ADDet.dateAD), 'yyyy-MM-dd');
				} else if (student.DOBAD_TMP) {
					student.DOB_AD = $filter('date')(new Date(student.DOBAD_TMP), 'yyyy-MM-dd');
				} else if (student.DOBAD) {
					student.DOB_AD = $filter('date')(new Date(student.DOBAD), 'yyyy-MM-dd');
				} else {
					student.DOB_AD = null;
				}
				// Prepare data for the API
				var para = {
					studentColl: [student] // Wrapping the single student in an array
				};
				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/UpdateClassWiseST",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};



	

});