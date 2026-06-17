app.controller('ThemeMarkEntryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'ThemeMarkEntry';
	var gSrv = GlobalServices;
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			ThemeWiseME: 1,
		};

		$scope.searchData = {
			ThemeWiseME: '',
		};

		$scope.perPage = {
			ThemeWiseME: GlobalServices.getPerPageRow()
		};


		$timeout(function () {

            $scope.ClassList = [];
            gSrv.getClassSectionList().then(function (res) {
                $scope.ClassList = res.data.Data;
            }, function (reason) {
                Swal.fire('Failed' + reason);
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

            $scope.SubjectList = {};
            gSrv.getSubjectList().then(function (res) {
                $scope.SubjectList = mx(res.data.Data);
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

            $scope.AssessmentList = [];
            $http({
                method: 'POST',
                url: base_url + "Exam/Transaction/GetAllAssessmentType",
                dataType: "json"
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.AssessmentList = res.data.Data.filter(function (item) {
                        return item.IsActive === true;
                    });
                } else {
                    Swal.fire(res.data.ResponseMSG);
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
			});

        }, 100);

		$scope.beData = {
			TranId: null,
			ClassId: null,
			BatchId: null,
			ClassYearId: null,
			SemesterId: null,
			SubjectId: null,
			LessonId: null,
			StudentId: null,
			AssessmentTypeId: null,
			AssessmentDate_TMP: new Date(),
			CFAssessmentTypeId: null,
			AchievementNo: 0,
			FullMarks: 0,
			Mode: 'Save'
		};

	}

	$scope.$watch('beData.SelectedClass', function (newVal, oldVal) {
		if (newVal && newVal !== oldVal) {
			$scope.beData.ClassYearId = null;
			$scope.beData.SemesterId = null;
		}
	});

	$scope.ClearBEData = function () {
		$scope.beData = {
			TranId: null,
			ClassId: null,
			BatchId: null,
			ClassYearId: null,
			SemesterId: null,
			SubjectId: null,
			LessonId: null,
			StudentId: null,
			AssessmentTypeId: null,
			AssessmentDate_TMP: new Date(),
			CFAssessmentTypeId: null,
			AchievementNo: 0,
			FullMarks: 0,
			Mode: 'Save'
		};
		$scope.ThemeStudentsDetailsList = [];
	}

	$scope.ChangeFullMarks = function () {
		$scope.beData.FullMarks = ($scope.beData.AchievementNo || 0) * 4;
	};

	$scope.CheckObtainMarks = function (beData) {
		if (beData.ObtainMarks > $scope.beData.FullMarks) {
			beData.ObtainMarks = 0;
			Swal.fire('Obtain Marks cannot be more then Full Marks');
        }
	};

	$scope.PastePayData = function (fieldName, startIndex) {
		var clipText = event.clipboardData.getData('text/plain');
		if (!clipText) {
			return;
		}
		$scope.loadingstatus = 'running';
		showPleaseWait();
		var values = clipText.split(/\r?\n/);
		var rowIndex = startIndex;
		angular.forEach(values, function (line) {
			if (line && line.trim() !== '') {
				if (rowIndex < $scope.ThemeStudentsDetailsList.length) {
					$scope.ThemeStudentsDetailsList[rowIndex][fieldName] = isEmptyAmt(line);
					rowIndex++;
				}
			}
		});
		hidePleaseWait();
		$scope.loadingstatus = "stop";
		$event.preventDefault();
		$scope.$applyAsync();
	};

	//************************ Theme Mark Entry *****************************************

	$scope.IsValidSubjectWise = function () {
		//if ($scope.beData.IndicatorName.isEmpty()) {
		//	Swal.fire('Please ! Enter Indicator Name');
		//	return false;
		//}
		return true;
	}

	$scope.SaveThemeWiseME = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var assessmentDate = $filter('date')(new Date($scope.beData.AssessmentDateDet.dateAD), 'yyyy-MM-dd');
		var dataToSave = [];
		angular.forEach($scope.ThemeStudentsDetailsList, function (student) {
			dataToSave.push({
				ClassId: $scope.beData.SelectedClass.ClassId,
				SectionId: $scope.beData.SelectedClass.SectionId,
				SubjectId: $scope.beData.SubjectId,
				LessonSno: $scope.beData.SelectLesson.LessonSno,
				TopicName: $scope.beData.TopicName,
				AssessmentTypeId: $scope.beData.AssessmentTypeId,
				AchievementNo: $scope.beData.AchievementNo,
				FullMarks: $scope.beData.FullMarks,
				AssessmentDate: assessmentDate,
				StudentId: student.StudentId,
				ObtainMarks: student.ObtainMarks ?? 0,
				Remarks: student.Remarks,
			});
		});
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveThemeMarkEntry",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.dataToSave));
				return formData;
			},
			data: { dataToSave: dataToSave }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess === true) {
				// $scope.ClearBEData();
			}
		}, function () {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
		});
	};

	$scope.GetThemeWiseMarkEntry = function () {
		if ($scope.beData.SelectedClass && $scope.beData.SubjectId > 0 && $scope.beData.SelectLesson.LessonId > 0  && $scope.beData.AssessmentTypeId && $scope.beData.AssessmentDateDet.dateAD) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.beData.SelectedClass.ClassId,
				SectionId: $scope.beData.SelectedClass.SectionId,
				SubjectId: $scope.beData.SubjectId,
				LessonId: $scope.beData.SelectLesson.LessonId,
				AssessmentTypeId: $scope.beData.AssessmentTypeId,
				CFAssessmentTypeId: $scope.beData.CFAssessmentTypeId,
				AssessmentDate: $filter('date')(new Date($scope.beData.AssessmentDateDet.dateAD), 'yyyy-MM-dd'),
				BatchId: $scope.beData.BatchId || null,
				ClassYearId: $scope.beData.ClassYearId || null,
				SemesterId: $scope.beData.SemesterId || null,
			};
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetThemeWiseMarkEntry",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.ThemeStudentsDetailsList = res.data.Data;
					var fstData = res.data.Data[0];
					$scope.beData.AchievementNo = fstData.AchievementNo || 0;
					$scope.ChangeFullMarks();
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}).catch(function (error) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire('Failed: ' + error.message);
			});
		}
	};

	$scope.GetSubjectLessonWise = function () {
		if ($scope.beData.SelectedClass.ClassId && $scope.beData.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.beData.SelectedClass.ClassId,
				SubjectId: $scope.beData.SubjectId
			};
			$scope.SubjectLessonWiseList = [];

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetSubjectLessonWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.SubjectLessonWiseList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.GetClassWiseSubMap = function () {
		$scope.beData.SubjectList = [];
		if ($scope.beData.SelectedClass) {
			var para = {
				ClassId: $scope.beData.SelectedClass.ClassId,
				SectionIdColl: ($scope.beData.SelectedClass.SectionId ? $scope.beData.SelectedClass.SectionId.toString() : ''),
			};

			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetSubjectMappingClassWise",
				dataSchedule: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var SubjectMappingColl = res.data.Data;

					if (SubjectMappingColl.length == 0) {
						Swal.fire('Subject Mapping Not Found');
					}
					else if (SubjectMappingColl.length > 0) {
						angular.forEach(SubjectMappingColl, function (sm) {
							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								subDet.PaperType = sm.PaperType;
								subDet.CRTH = 0;
								subDet.CRPR = 0;
								subDet.FMTH = 0;
								subDet.FMPR = 0;
								subDet.PMTH = 0;
								subDet.PMPR = 0;
								subDet.IsInclude = true;
								$scope.beData.SubjectList.push(subDet);
							}
						});

					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	};

	$scope.DelThemeMarkEntry = function () {
		if (!$scope.beData.SubjectId) {
			Swal.fire("Please select a subject.");
			return;
		}
		if (!$scope.beData.SelectLesson.LessonId) {
			Swal.fire("Please select a lesson.");
			return;
		}
		if (!$scope.beData.AssessmentTypeId) {
			Swal.fire("Please select an assessment type.");
			return;
		}
		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {

			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();
				var para = {
					ClassId: $scope.beData.SelectedClass.ClassId,
					SectionId: $scope.beData.SelectedClass.SectionId,
					SubjectId: $scope.beData.SubjectId,
					LessonId: $scope.beData.SelectLesson.LessonId,
					AssessmentTypeId: $scope.beData.AssessmentTypeId,
					AssessmentDate: $filter('date')(new Date($scope.beData.AssessmentDateDet.dateAD), 'yyyy-MM-dd'),
					BatchId: $scope.beData.BatchId || null,
					ClassYearId: $scope.beData.ClassYearId || null,
					SemesterId: $scope.beData.SemesterId || null,
				};
				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelThemeMarkEntry",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						Swal.fire(res.data.ResponseMSG);
						$scope.GetThemeWiseMarkEntry();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};


});