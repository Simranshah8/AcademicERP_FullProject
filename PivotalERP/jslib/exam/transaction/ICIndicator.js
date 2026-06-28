app.controller('IndicatorController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Indicator';
	var glbS = GlobalServices;
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			Indicator: 1
		};

		$scope.searchData = {
			Indicator: ''
		};

		$scope.perPage = {
			Indicator: GlobalServices.getPerPageRow(),
		};

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
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

		$scope.newIndicator = {
			IndicatorId: null,
			ClassId: null,
			SubjectId: null,
			LessonId: null,
			TopicName: null,
			IndicatorDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newIndicator.IndicatorDetailsColl.push({});
		/*$scope.GetAllIndicatorList();*/
	}

	$scope.ClearIndicator = function () {
		$scope.newIndicator = {
			IndicatorId: null,
			ClassId: null,
			SubjectId: null,
			LessonId: null,
			TopicName: null,
			IndicatorDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newIndicator.IndicatorDetailsColl.push({});
	}

	$scope.AddIndicatorDetails = function (ind) {
		if ($scope.newIndicator.IndicatorDetailsColl) {
			if ($scope.newIndicator.IndicatorDetailsColl.length > ind + 1) {
				$scope.newIndicator.IndicatorDetailsColl.splice(ind + 1, 0, {
					ClassName: ''
				})
			} else {
				$scope.newIndicator.IndicatorDetailsColl.push({
					ClassName: ''
				})
			}
		}
	};

	$scope.delIndicatorDetails = function (ind) {
		if ($scope.newIndicator.IndicatorDetailsColl) {
			if ($scope.newIndicator.IndicatorDetailsColl.length > 1) {
				$scope.newIndicator.IndicatorDetailsColl.splice(ind, 1);
			}
		}
	};

	$scope.$watch('newIndicator.SelectedClass', function (newVal, oldVal) {
		if (newVal && newVal !== oldVal) {
			$scope.newIndicator.ClassYearId = null;
			$scope.newIndicator.SemesterId = null;
		}
	});
	$scope.$watch('newDet.SelectedClass', function (newVal, oldVal) {
		if (newVal && newVal !== oldVal) {
			$scope.newDet.ClassYearId = null;
			$scope.newDet.SemesterId = null;
		}
	});
	$scope.$watch('newSummary.SelectedClass', function (newVal, oldVal) {
		if (newVal && newVal !== oldVal) {
			$scope.newSummary.ClassYearId = null;
			$scope.newSummary.SemesterId = null;
		}
	});
	//************************* Indicator *********************************
    $scope.GetClassWiseSubjectList = function (resVal) {
        $scope.SubjectListAdd = [];
        var para = {
            ClassId: resVal.SelectedClass.ClassId,
            BatchId: resVal.BatchId || null,
            ClassYearId: resVal.ClassYearId || null,
            SemesterId: resVal.SemesterId || null,
        };
        $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetSubjectListForLessonPlan",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.SubjectListAdd = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GetClassWiseSubjectListRpt = function (resVal, be) {
        var para = {
            ClassId: resVal.SelectedClass.ClassId,
            BatchId: resVal.BatchId || null,
            ClassYearId: resVal.ClassYearId || null,
            SemesterId: resVal.SemesterId || null,
        };
        $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetSubjectListForLessonPlan",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                if (be == 1) {
                    $scope.SubjectListForRpt = [];
                    $scope.SubjectListForRpt = res.data.Data;
                } else {
                    $scope.SubjectListRpt = [];
                    $scope.SubjectListRpt = res.data.Data;
                }
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

	$scope.GetTopicWiseIndicator = function () {
		$scope.newIndicator.IndicatorDetailsColl = [];
		if ($scope.newIndicator.SelectLesson.LessonSno != null && !$scope.newIndicator.TopicName == "") {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				LessonId: $scope.newIndicator.SelectLesson.LessonId,
				TopicName: $scope.newIndicator.TopicName,
			};
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetTopicWiseIndicator",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newIndicator.IndicatorDetailsColl = res.data.Data;

					if (!$scope.newIndicator.IndicatorDetailsColl || $scope.newIndicator.IndicatorDetailsColl.length == 0) {
						$scope.newIndicator.IndicatorDetailsColl = [];
						$scope.newIndicator.IndicatorDetailsColl.push({});
					}

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	 
	$scope.IsValidIndicator = function () {
		//if ($scope.newIndicator.IndicatorName.isEmpty()) {
		//	Swal.fire('Please ! Enter Indicator Name');
		//	return false;
		//}
		return true;
	}

	$scope.SaveUpdateIndicator = function () {

		if ($scope.newIndicator.IndicatorDetailsColl.length > 0 && $scope.newIndicator.IndicatorDetailsColl[0].TranId) {
			$scope.newIndicator.TranId = $scope.newIndicator.IndicatorDetailsColl[0].TranId;
		}

		if ($scope.IsValidIndicator() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newIndicator.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateIndicator();
					}
				});
			} else
				$scope.CallSaveUpdateIndicator();

		}
	};

	$scope.CallSaveUpdateIndicator = function () {
		$scope.newIndicator.IndicatorDetailsColl.forEach((item, index) => {
			item.SNo = index + 1;
		});
		$scope.newIndicator.LessonSno = $scope.newIndicator.SelectLesson.LessonSno;
		$scope.newIndicator.ClassId = $scope.newIndicator.SelectedClass.ClassId;

		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveUpdateIndicator",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newIndicator }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearIndicator();
	/*		*//*	$scope.GetAllIndicatorList();*/
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllIndicatorList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.IndicatorList = [];
		if ($scope.newDet.SelectedClass.ClassId != null && !$scope.newDet.SubjectId != null) {
			var para = {
				ClassId: $scope.newDet.SelectedClass.ClassId,
				SubjectId: $scope.newDet.SubjectId,
				LessonId: $scope.newDet.SelectLesson.LessonId,
				TopicName: $scope.newDet.TopicName,
				BatchId: $scope.newDet.BatchId || null,
				ClassYearId: $scope.newDet.ClassYearId || null,
				SemesterId: $scope.newDet.SemesterId || null,
			};
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetAllIndicator",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.IndicatorList = [];
					var query = mx(res.data.Data).groupBy(t => ({ LessonName: t.LessonName }));
					var sno = 1;
					angular.forEach(query, function (q) {
						var pare = {
							SNo: sno,
							LessonName: q.key.LessonName,
							ChieldColl: []
						};
						var sno1 = 1;
						var elQry = mx(q.elements).groupBy(t => ({ TopicName: t.TopicName }));
						var chLen = 0;
						angular.forEach(elQry, function (el) {
							var pm = {
								SNo: sno1,
								TopicName: el.key.TopicName,
								ChieldColl: el.elements,
								/*IndicatorLen: el.elements.length*/
							};
							/*chLen += el.elements.length;*/

							pare.ChieldColl.push(pm);
							sno1++;
						});
						pare.IndicatorLen = chLen;
						$scope.IndicatorList.push(pare);
						sno++;
					});


				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		} else {
			Swal.fire('Please ! Select Class followed by Subject');
			hidePleaseWait();
			$scope.loadingstatus = "stop";
        }

	}
		
	$scope.GetSubjectLessonWise = function () {
		if ($scope.newIndicator.SelectedClass.ClassId && $scope.newIndicator.SubjectId>0) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newIndicator.SelectedClass.ClassId,
				SubjectId: $scope.newIndicator.SubjectId
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

	$scope.GetLessonTopicDetailsWise = function () {
		if ($scope.newIndicator.SelectLesson) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				LessonId: $scope.newIndicator.SelectLesson.LessonId
			};
			$scope.LessonTopicDetailsWiseList = [];

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetLessonTopicDetailsWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.LessonTopicDetailsWiseList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}
	
	$scope.GetRSubjectLessonWise = function () {
		if ($scope.newDet.SelectedClass.ClassId && $scope.newDet.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newDet.SelectedClass.ClassId,
				SubjectId: $scope.newDet.SubjectId
			};
			$scope.RSubjectLessonWiseList = [];
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetSubjectLessonWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.RSubjectLessonWiseList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.GetRLessonTopicDetailsWise = function () {
		if ($scope.newDet.SelectLesson.LessonId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				LessonId: $scope.newDet.SelectLesson.LessonId
			};
			$scope.RLessonTopicDetailsWiseList = [];

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetLessonTopicDetailsWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.RLessonTopicDetailsWiseList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.GetIndicatorSummary = function () {
		if ($scope.newSummary.SelectedClass.ClassId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newSummary.SelectedClass.ClassId,
				SubjectId: $scope.newSummary.SubjectId,
				BatchId: $scope.newSummary.BatchId || null,
				ClassYearId: $scope.newSummary.ClassYearId || null,
				SemesterId: $scope.newSummary.SemesterId || null,
			};
			$scope.IndicatorTotalSummaryList = {};
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetIndicatorSummary",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.IndicatorTotalSummaryList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});