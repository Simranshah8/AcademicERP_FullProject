
app.controller('SubjectMappingController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Subject Mapping';

	
	//add and del
	$scope.AddClassWiseDetails = function (ind)
	{
		if ($scope.newClassWise.ClassId > 0) {
			if ($scope.newClassWise.ClassWiseDetailsColl) {
				if ($scope.newClassWise.ClassWiseDetailsColl.length > ind + 1) {
					$scope.newClassWise.ClassWiseDetailsColl.splice(ind + 1, 0, {
						ClassId: $scope.newClassWise.ClassId,
						SectionIdColl: $scope.newClassWise.SectionId,
						NoOfOptionalSub: $scope.newClassWise.NoOfOptionalSub,
						SemesterId: $scope.newClassWise.SemesterId,
						ClassYearId: $scope.newClassWise.ClassYearId,
						BatchId: $scope.newClassWise.BatchId,
						SubjectId: null,
						PaperType: 3,
						CodeTH: '',
						CodePR: '',
						CRTH: 0,
						CRPR:0,
						IsOptional: false,
						IsExtra: false

					})
				} else {
					$scope.newClassWise.ClassWiseDetailsColl.push({
						ClassId: $scope.newClassWise.ClassId,
						SectionIdColl: $scope.newClassWise.SectionId,
						NoOfOptionalSub: $scope.newClassWise.NoOfOptionalSub,
						SemesterId: $scope.newClassWise.SemesterId,
						ClassYearId: $scope.newClassWise.ClassYearId,
						BatchId: $scope.newClassWise.BatchId,
						SubjectId: null,
						PaperType: 3,
						CodeTH: '',
						CodePR: '',
						CRTH: 0,
						CRPR: 0,
						IsOptional: false,
						IsExtra: false
					})
				}
			}
        }
		
	};
	$scope.delClassWiseDetails = function (ind) {
		if ($scope.newClassWise.ClassWiseDetailsColl) {
			if ($scope.newClassWise.ClassWiseDetailsColl.length > 1) {
				$scope.newClassWise.ClassWiseDetailsColl.splice(ind, 1);
			}
		}
	};

	$scope.LoadData = function () {

		$('.select2').select2();

		var glbS = GlobalServices;
		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();


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

		$scope.currentPages = {
			ClassWise: 1,
			StudentWise: 1

		};

		$scope.searchData = {
			ClassWise: '',
			StudentWise: ''
			
		};

		$scope.perPage = {
			ClassWise: glbS.getPerPageRow(),
			StudentWise: 25,
		};

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

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SectionList = [];
		glbS.getSectionList().then(function (res) {
			$scope.SectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res) {
			$scope.SubjectList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.PaperTypeList = glbS.getPaperTypeList();

		$scope.newClassWise = {
			ClassWiseId: null,
			ClassWiseDetailsColl: [],
			NoOfOptionalSub:0,
			Mode: 'Save'
		};

		$scope.newStudentWise = {
			StudentWiseId: null,
			ClassId: null,
			SectionId:null,
			OptionalSubjectList: [],
			StudentList: [],
			MatchOptSubject:true,
			Mode: 'Save'
		};

		$scope.sortKeys = {
			ClassWise: '',
			SubjectWise:'',
		};

		$scope.reverses = {
			ClassWise: false,
			SubjectWise: false,
		}

		$scope.newAcademic = {};
		$scope.showSectionDropdown = false;
		$http({
			method: 'POST',
			url: base_url + "Academic/Setup/GetAcademicConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newAcademic = res.data.Data;
				$scope.showSectionDropdown = $scope.newAcademic.SectionWiseSubjectMapping === true;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	
	}

	$scope.sortClassWise = function (keyname)
	{
		$scope.sortKeys.ClassWise = keyname;   //set the sortKey to the param passed
		$scope.reverses.ClassWise = !$scope.reverses.ClassWise; //if true make it false and vice versa
	}
	$scope.sortSubjectWise = function (keyname) {
		$scope.sortKeys.SubjectWise = keyname;   //set the sortKey to the param passed
		$scope.reverses.SubjectWise = !$scope.reverses.SubjectWise; //if true make it false and vice versa
	}


	$scope.LoadClassWiseSemesterYear = function (classId,data) {

		$scope.SelectedClassClassYearList = [];
		$scope.SelectedClassSemesterList = [];
		$scope.SelectedClass = mx($scope.ClassSection.ClassList).firstOrDefault(p1 => p1.ClassId == classId);

		if ($scope.SelectedClass) {
			var semQry = mx($scope.SelectedClass.ClassSemesterIdColl);
			var cyQry = mx($scope.SelectedClass.ClassYearIdColl);

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

	$scope.LoadClassWiseSemesterYear1 = function (classId, data) {

		$scope.SelectedClassClassYearList1 = [];
		$scope.SelectedClassSemesterList1 = [];
		$scope.SelectedClass1 = mx($scope.ClassSection.ClassList).firstOrDefault(p1 => p1.ClassId == classId);

		if ($scope.SelectedClass1) {
			var semQry = mx($scope.SelectedClass1.ClassSemesterIdColl);
			var cyQry = mx($scope.SelectedClass1.ClassYearIdColl);

			angular.forEach($scope.SemesterList, function (sem) {
				if (semQry.contains(sem.id)) {
					$scope.SelectedClassSemesterList1.push({
						id: sem.id,
						text: sem.text,
						SemesterId: sem.id,
						Name: sem.Name
					});
				}
			});

			angular.forEach($scope.ClassYearList, function (sem) {
				if (cyQry.contains(sem.id)) {
					$scope.SelectedClassClassYearList1.push({
						id: sem.id,
						text: sem.text,
						ClassYearId: sem.id,
						Name: sem.Name
					});
				}
			});
		}

	};

	$scope.ClearClassWise = function () {
		$scope.newClassWise = {
			ClassWiseId: null,
			ClassWiseDetailsColl: [],
			NoOfOptionalSub: 0,
			Mode: 'Save'
		};		
	}
	$scope.ClearStudentWise = function () {
		$scope.newStudentWise = {
			StudentWiseId: null,
			ClassId: null,
			SectionId: null,
			OptionalSubjectList: [],
			StudentList: [],
			Mode: 'Save'
		};
		
	}


	//************************* Class Wise *********************************

	

	$scope.ChangeSubject = function (subMap) {

		var sub = mx($scope.SubjectList).firstOrDefault(p1 => p1.SubjectId == subMap.SubjectId);

		if (sub) {
			subMap.CodeTH = sub.CodeTH;
			subMap.CodePR = sub.CodePR;
			subMap.CRTH = sub.CRTH;
			subMap.CRPR = sub.CRPR;
        }
		
	};

	$scope.validateDuplicateSubjects = function () {
		var subjectMap = {};
		var duplicateRows = [];

		angular.forEach($scope.newClassWise.ClassWiseDetailsColl, function (row, index) {
			if (row.SubjectId) {
				if (subjectMap[row.SubjectId] !== undefined) {
					duplicateRows.push(index + 1); // S.No (UI friendly)
				} else {
					subjectMap[row.SubjectId] = index;
				}
			}
		});

		if (duplicateRows.length > 0) {
			Swal.fire(
				'Duplicate Subject Found',
				'Same subject selected multiple times at row(s): <b>' + duplicateRows.join(', ') + '</b>',
				'warning'
			);
			return false;
		}

		return true;
	};

	$scope.IsValidClassWise = function () {
		if (!$scope.newClassWise.ClassId || $scope.newClassWise.ClassId === 0) {
			Swal.fire('Please ! Select Class first');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateClassWise = function () {
		if ($scope.IsValidClassWise() == true) {
		if ($scope.confirmMSG.Accept == false) {
			var saveModify = $scope.newClassWise.Mode;
			Swal.fire({
				title: 'Do you want to ' + saveModify + ' the current data? This action will permanently remove any previously created student-wise subject mapping data.',
				showCancelButton: true,
				confirmButtonText: saveModify,
			}).then((result) => {
				/* Read more about isConfirmed, isDenied below */
				if (result.isConfirmed) {
					$scope.CallSaveUpdateClassWise();
				}
			});
		} else {
			$scope.CallSaveUpdateClassWise();
        }
		}
	};

	$scope.CallSaveUpdateClassWise = function () {
		if (!$scope.validateDuplicateSubjects()) {
			return;
		}
		$scope.loadingstatus = "running";

		if ($scope.newClassWise.TranId && $scope.newClassWise.TranId > 0)
		{
			Swal.fire({
				title: 'Do you want to  modify SubjectMapping Class Wise . You Will loss StudentWise Subject Mapping Data?',
				showCancelButton: true,
				confirmButtonText: saveModify,
			}).then((result) => {
				/* Read more about isConfirmed, isDenied below */
				if (result.isConfirmed) {

				} else {
					return;
                }
			});
        }

		showPleaseWait();

		var sno = 1;
		var para = {
			ClassId: $scope.newClassWise.ClassId,
			SectionIdColl: ($scope.newClassWise.SectionId && $scope.newClassWise.SectionId.length > 0 ? $scope.newClassWise.SectionId : []),
			SemesterId: $scope.newClassWise.SemesterId,
			ClassYearId: $scope.newClassWise.ClassYearId,
			BatchId: $scope.newClassWise.BatchId,
			FromSectionIdColl: ($scope.newClassWise.SectionId && $scope.newClassWise.SectionId.length > 0 ? $scope.newClassWise.SectionId.toString() :''),
			ToClassIdColl: ($scope.newClassWise.ToClassId_TMP && $scope.newClassWise.ToClassId_TMP.length > 0 ? $scope.newClassWise.ToClassId_TMP.toString() : ''),
			ToSectionIdColl: ($scope.newClassWise.ToSectionId_TMP && $scope.newClassWise.ToSectionId_TMP.length > 0 ? $scope.newClassWise.ToSectionId_TMP.toString() : ''),
		};
		angular.forEach($scope.newClassWise.ClassWiseDetailsColl, function (dc) {
			dc.SNo = sno;
			sno = sno + 1;
			dc.ClassId = para.ClassId;
			dc.SectionIdColl = para.SectionIdColl;
			dc.SemesterId = para.SemesterId;
			dc.ClassYearId = para.ClassYearId;
			dc.BatchId = para.BatchId;
			dc.NoOfOptionalSub = $scope.newClassWise.NoOfOptionalSub;
			dc.FromSectionIdColl = para.FromSectionIdColl;
			dc.ToClassIdColl = para.ToClassIdColl;
			dc.ToSectionIdColl = para.ToSectionIdColl;
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveSubjectMappingClassWise",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newClassWise.ClassWiseDetailsColl }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}
	

	$scope.GetAllClassWiseList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ClassWiseList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllClassWiseList",
			dataSchedule: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ClassWiseList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetClassWiseSubMap = function (fromInd)
	{

		//$scope.loadingstatus = "running";
		//showPleaseWait();

		$scope.newClassWise.ClassWiseDetailsColl = [];

		if ($scope.newClassWise.ClassId && $scope.newClassWise.ClassId > 0) {

			// Load Class Wise Year and Semester On Class Selection Changed
			if (fromInd == 2) {
				$scope.newClassWise.SemesterId = null;
				$scope.newClassWise.ClassYearId = null;				 
				$scope.LoadClassWiseSemesterYear($scope.newClassWise.ClassId, $scope.newClassWise);
            }
				

			var para = {
				ClassId: $scope.newClassWise.ClassId,
				SectionIdColl: ($scope.newClassWise.SectionId && $scope.newClassWise.SectionId.length > 0 ? $scope.newClassWise.SectionId.toString() : ''),
				SemesterId: $scope.newClassWise.SemesterId,
				ClassYearId: $scope.newClassWise.ClassYearId,
				BatchId: $scope.newClassWise.BatchId,
				BranchId:$scope.newClassWise.BranchId,
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
					$scope.newClassWise.ClassWiseDetailsColl = res.data.Data;
					$scope.newClassWise.Mode = 'Modify';
					
					if ($scope.newClassWise.ClassWiseDetailsColl.length == 0)
						$scope.AddClassWiseDetails(0);
					else
						$scope.newClassWise.NoOfOptionalSub = $scope.newClassWise.ClassWiseDetailsColl[0].NoOfOptionalSub;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        }
		
	};

	$scope.DelClassWiseById = function (refData) {

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
					ClassWiseId: refData.ClassWiseId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelClassWise",
					dataSchedule: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllClassWiseList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Student Wise *********************************

	$scope.IsValidStudentWise = function () {
		if (!$scope.newStudentWise.ClassId || $scope.newStudentWise.ClassId === 0) {
			Swal.fire('Please ! Select Class first');
			return false;
		}
		return true;
	}

	$scope.SaveUpdateStudentWise = function () {
		if ($scope.IsValidStudentWise() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudentWise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudentWise();
					}
				});
			} else
				$scope.CallSaveUpdateStudentWise();

		}
	};


	$scope.CallSaveUpdateStudentWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var data = [];
		angular.forEach($scope.newStudentWise.StudentList, function (st) {

			
			var newSt = {
				ClassId: st.ClassId,
				SectionId: st.SectionId,
				SemesterId: st.SemesterId,
				ClassYearId: st.ClassYearId,
				BatchId:st.BatchId,
				StudentId: st.StudentId,
				NoOfOptionalSubject: st.NoOfOptionalSubject,
				MatchOptSubject: $scope.newStudentWise.MatchOptSubject,
				TranIdColl:[]
			};

			angular.forEach(st.TranIdColl, function (t) {
				if (t.Selected == true)
					newSt.TranIdColl.push(t.TranId);
			});

			data.push(newSt);
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveSubjectMappingStudentWise",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: data }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearStudentWise();				
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllStudentWiseList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentWiseList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllStudentWiseList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentWiseList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	$scope.CheckUnCheckAll = function (sub) {

		angular.forEach($scope.newStudentWise.StudentList, function (st) {
			angular.forEach(st.TranIdColl, function (t) {
				if (t.TranId == sub.TranId)
					t.Selected = sub.Selected;
			});
        })

	};
	$scope.GetStudentWiseSubMap = function (fromInd) {

		
		$scope.newStudentWise.OptionalSubjectList = [];
		$scope.newStudentWise.StudentList = [];

		if ($scope.newStudentWise.ClassId && $scope.newStudentWise.ClassId > 0) {

			var para = {
				ClassId: $scope.newStudentWise.ClassId,
				SectionId: $scope.newStudentWise.SectionId,
				SemesterId: $scope.newStudentWise.SemesterId,
				ClassYearId: $scope.newStudentWise.ClassYearId,
				BatchId: $scope.newStudentWise.BatchId,
				BranchId:$scope.newStudentWise.BranchId,
			};

			if (fromInd == 2) {
				$scope.newStudentWise.SemesterId = null;
				$scope.newStudentWise.ClassYearId = null;			
				$scope.LoadClassWiseSemesterYear1(para.ClassId, $scope.newStudentWise);
            }

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetSubjectMappingStudentWise",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var optSubjects = res.data.Data.OptionalSubjectList;
					$scope.newStudentWise.OptionalSubjectList = optSubjects;
					$scope.newStudentWise.StudentList = res.data.Data.StudentList;

					angular.forEach($scope.newStudentWise.StudentList, function (student) {

						if (optSubjects && optSubjects.length > 0)
							student.NoOfOptionalSubject = optSubjects[0].NoOfOptionalSubject;

						if (!student.TranIdColl || student.TranIdColl.length == 0)
							student.TranIdColl = [];

						var tranColl = mx(student.TranIdColl);

						student.TranIdColl = [];

						angular.forEach(optSubjects, function (sub)
						{
							if (tranColl.contains(sub.TranId) == true) {
								student.TranIdColl.push({
									TranId: sub.TranId,
									Selected: true
								});
							} else
							{
								student.TranIdColl.push({
									TranId: sub.TranId,
									Selected: false
								});
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


	$scope.DelStudentWiseById = function (refData) {

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
					StudentWiseId: refData.StudentWiseId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelStudentWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllStudentWiseList();
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

});