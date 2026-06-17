app.controller('LessonPlanController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Lesson Plan';
	OnClickDefault();
	var glbS = GlobalServices;

	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		//$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();
		//$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();

		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newAddLesson = {
			TranId: null,
			ClassId: null,
			SubjectId: null,
			NoOfLesson: 0,
			DetailsColl:[],
			Mode: 'Save'
		};

		$scope.newListOfLessons = {
			ListOfLessonsId: null,
			ClassId: null,
			SubjectId: null,
			Mode: 'Save'
		};

		$scope.newAddLessonPlan = {
			AddLessonPlanId: null,
			ClassId: null,
			SubjectId: null,
			Mode: 'Save'
		};

		$scope.newListOfLessonPlan = {
			ListOfLessonPlanId: null,
			ClassId: null,
			SubjectId: null,
			Mode: 'Save'
		};
	}

	$scope.CurrentLesson = {};
	$scope.ChangeCurrentLesson = function (lesson) {
		$scope.CurrentLesson = lesson;
    }
	$scope.AddTopic = function (ind) {
		if ($scope.CurrentLesson.TopicColl) {
			if ($scope.CurrentLesson.TopicColl.length > ind + 1) {
				$scope.CurrentLesson.TopicColl.splice(ind + 1, 0, {
					SNo: 0,
					TopicName: ''
				})
			} else {
				$scope.CurrentLesson.TopicColl.push({
					SNo: 0,
					TopicName:''
				})
			}
		}
		var sno = 1;
		angular.forEach($scope.CurrentLesson.TopicColl, function (tc) {
			tc.SNo = sno;
			sno++;
		});

	};
	$scope.DelTopic = function (ind) {
		if ($scope.CurrentLesson.TopicColl) {
			if ($scope.CurrentLesson.TopicColl.length > 1) {
				$scope.CurrentLesson.TopicColl.splice(ind, 1);
			}
		}

		var sno = 1;
		angular.forEach($scope.CurrentLesson.TopicColl, function (tc) {
			tc.SNo = sno;
			sno++;
		});
	};
	$scope.ChangeNoOfLesson = function () {
		if ($scope.newAddLesson.ClassId > 0 && $scope.newAddLesson.SubjectId > 0) {
			var nl = $scope.newAddLesson.NoOfLesson;
			if (nl > $scope.newAddLesson.DetailsColl.length) {
				var needToAdd = nl - $scope.newAddLesson.DetailsColl.length;
				for (var i = 0; i < needToAdd; i++) {					
					$scope.newAddLesson.DetailsColl.push({
						SNo: 0,
						TopicColl: [{SNo:1,TopicName:''}],
					});
				}
			} else {
				var needToAdd = $scope.newAddLesson.DetailsColl.length - nl;
				for (var i = 0; i < needToAdd; i++) {
					var ind = $scope.newAddLesson.DetailsColl.length - 1;
					$scope.newAddLesson.DetailsColl.splice(ind, 1);
				}
			}

			var sno = 1;
			angular.forEach($scope.newAddLesson.DetailsColl, function (dc) {
				dc.SNo = sno;
				sno++;
			});
		}		
	};

	$scope.addNL = function (val) {

		if ($scope.newAddLesson.ClassId > 0 && $scope.newAddLesson.SubjectId > 0) {
			if (val < 0) {
				if ($scope.newAddLesson.NoOfLesson > 0)
					$scope.newAddLesson.NoOfLesson = $scope.newAddLesson.NoOfLesson + val;

			} else {
				$scope.newAddLesson.NoOfLesson = $scope.newAddLesson.NoOfLesson + val;
			}

			$scope.ChangeNoOfLesson();
        }
		
    }

	//************************* Add Lesson *********************************
	function OnClickDefault() {
		//document.getElementById('list-editing').style.display = "none";
		//document.getElementById('topicdateip').style.display = "none";
		//document.getElementById('lessondateip').style.display = "none";
		//document.getElementById('editlist').onclick = function () {
		//	document.getElementById('listing').style.display = "none";
		//	document.getElementById('list-editing').style.display = "block";

		//}
		//document.getElementById('backList').onclick = function () {
		//	document.getElementById('listing').style.display = "block";
		//	document.getElementById('list-editing').style.display = "none";
		//}

		//document.getElementById('topiccalender').onclick = function () {
		//	document.getElementById('topicdateip').style.display = "block";

		//}
		//document.getElementById('lessonipicon').onclick = function () {
		//	document.getElementById('lessondateip').style.display = "block";

		//}

	};

	$scope.GetClassWiseSubjectList = function (classId) {

		$scope.SubjectList = [];
		var para = {
			ClassId: classId
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetSubjectListForLessonPlan",
			dataType: "json",
			data:JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$timeout(function () {
					$scope.SubjectList = res.data.Data;
				});				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.GetLessonPlanByClassSubject = function (classId,subjectId) {

		if (classId > 0 && subjectId > 0) {
			var para = {
				ClassId: classId,
				SubjectId: subjectId,
				SectionIdColl:'',
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetLessonPlanByClassSubject",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {

					$timeout(function () {
						var dd = res.data.Data;
						$scope.newAddLesson.CoverFilePath = dd.CoverFilePath;
						$scope.newAddLesson.NoOfLesson = dd.NoOfLesson;
						$scope.newAddLesson.TranId = dd.TranId;
						$scope.newAddLesson.DetailsColl = dd.DetailsColl;

						angular.forEach($scope.newAddLesson.DetailsColl, function (dc) {

							if (!dc.TopicColl || dc.TopicColl.length == 0) {
								dc.TopicColl = [];
								dc.TopicColl.push({ SNo: 1, TopicName: '' });
                            }
						});

						if ($scope.newAddLesson.DetailsColl.length > 0)
							$scope.CurrentLesson = $scope.newAddLesson.DetailsColl[0];
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        }
		
	}

	$scope.ClearAddLesson = function () {

		$timeout(function () {
			$scope.newAddLesson = {
				TranId: null,
				ClassId: null,
				SubjectId: null,
				NoOfLesson: 0,
				DetailsColl: [],
				Mode: 'Save'
			};
			$scope.CurrentLesson = {};
		});
		
	};


	$scope.CallSaveUpdateLessonPlan = function () {
	 
		if ($scope.newAddLesson.ClassId && $scope.newAddLesson.SubjectId > 0)
		{
			$scope.loadingstatus = "running";
			showPleaseWait();

			var attachmentColl = $scope.newAddLesson.Files_TMP;

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/SaveLessonPlan",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					if (data.files) {
						for (var i = 0; i < data.files.length; i++) {
							if (data.files[i].File && data.files[i].File != null)
								formData.append("file" + i, data.files[i].File);
							else
								formData.append("file" + i, data.files[i]);
						}
					}

					return formData;
				},
				data: { jsonData: $scope.newAddLesson, files: attachmentColl  }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				hidePleaseWait();

				Swal.fire(res.data.ResponseMSG);
				if (res.data.IsSuccess == true) {
					$scope.ClearAddLesson();
                }

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		}
		
	}


	$scope.GetClassWiseSubjectListForLP = function (classId) {

		$scope.SubjectListLP = [];
		var para = {
			ClassId: classId
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

				$timeout(function () {
					$scope.SubjectListLP = res.data.Data;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	$scope.GetLessonPlanByClassSubjectLP = function (classId, subjectId) {

		if (classId > 0 && subjectId > 0) {

			$scope.newListOfLessonPlan.SubjectName = mx($scope.SubjectListLP).firstOrDefault(p1 => p1.SubjectId == subjectId).Name;

			var para = {
				ClassId: classId,
				SubjectId: subjectId,
				SectionIdColl:'',
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetLessonPlanByClassSubject",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {

					$timeout(function () {
						var dd = res.data.Data;
						$scope.newListOfLessons.CoverFilePath = dd.CoverFilePath;
						$scope.newListOfLessons.NoOfLesson = dd.NoOfLesson;
						$scope.newListOfLessons.TranId = dd.TranId;
						$scope.newListOfLessons.DetailsColl = dd.DetailsColl;

						angular.forEach($scope.newListOfLessons.DetailsColl, function (dc) {

							if (!dc.TopicColl || dc.TopicColl.length == 0) {
								dc.TopicColl = [];
								dc.TopicColl.push({ SNo: 1, TopicName: '' });
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

	}



	$scope.GetClassWiseSubjectListForALP = function (classId) {

		$scope.SubjectListALP = [];
		var para = {
			ClassId: classId
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

				$timeout(function () {
					$scope.SubjectListALP = res.data.Data;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	$scope.GetLessonPlanByClassSubjectALP = function (classId, subjectId) {

		if (classId > 0 && subjectId > 0) {

			$scope.newAddLessonPlan.SubjectName = mx($scope.SubjectListALP).firstOrDefault(p1 => p1.SubjectId == subjectId).Name;

			var para = {
				ClassId: classId,
				SubjectId: subjectId,
				SectionIdColl:'',
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetLessonPlanByClassSubject",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {

					$timeout(function () {
						var dd = res.data.Data;
						$scope.newAddLessonPlan.NoOfLesson = dd.NoOfLesson;
						$scope.newAddLessonPlan.TranId = dd.TranId;
						$scope.newAddLessonPlan.DetailsColl = dd.DetailsColl;

						angular.forEach($scope.newAddLessonPlan.DetailsColl, function (dc) {
							if (dc.PlanStartDate_AD)
								dc.PlanStartDate_TMP = new Date(dc.PlanStartDate_AD);

							if (dc.PlanEndDate_AD)
								dc.PlanEndDate_TMP = new Date(dc.PlanEndDate_AD);

							angular.forEach(dc.TopicColl, function (tc) {
								if (tc.PlanStartDate_AD)
									tc.PlanStartDate_TMP = new Date(tc.PlanStartDate_AD);

								if (tc.PlanEndDate_AD)
									tc.PlanEndDate_TMP = new Date(tc.PlanEndDate_AD);
							});
                        })
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}
	$scope.ClearLessonPlan = function () {

		$timeout(function () {
			$scope.newAddLessonPlan = {};
		});
    }
	$scope.CallSaveUpdateLessonPlanDate = function () {

		if ($scope.newAddLessonPlan.ClassId && $scope.newAddLessonPlan.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			angular.forEach($scope.newAddLessonPlan.DetailsColl, function (dc) {
				if (dc.PlanStartDateDet)
					dc.PlanStartDate_AD = $filter('date')(new Date(dc.PlanStartDateDet.dateAD), 'yyyy-MM-dd');

				if (dc.PlanEndDateDet)
					dc.PlanEndDate_AD = $filter('date')(new Date(dc.PlanEndDateDet.dateAD), 'yyyy-MM-dd');

				angular.forEach(dc.TopicColl, function (tc) {
					if (tc.PlanStartDateDet)
						tc.PlanStartDate_AD = $filter('date')(new Date(tc.PlanStartDateDet.dateAD), 'yyyy-MM-dd');

					if (tc.PlanEndDateDet)
						tc.PlanEndDate_AD = $filter('date')(new Date(tc.PlanEndDateDet.dateAD), 'yyyy-MM-dd');
				});
			});

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/UpdateLessonPlanDate",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					formData.append("jsonData", angular.toJson(data.jsonData));

					return formData;
				},
				data: { jsonData: $scope.newAddLessonPlan }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				hidePleaseWait();

				Swal.fire(res.data.ResponseMSG);
				 

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		}

	}


	$scope.GetClassWiseSubjectListForLLP = function (classId) {

		$scope.SubjectListLLP = [];
		var para = {
			ClassId: classId
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

				$timeout(function () {
					$scope.SubjectListLLP = res.data.Data;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}
	$scope.GetLessonPlanByClassSubjectLLP = function (classId, subjectId) {

		if (classId > 0 && subjectId > 0) {

			$scope.newListOfLessonPlan.SubjectName = mx($scope.SubjectListLLP).firstOrDefault(p1 => p1.SubjectId == subjectId).Name;

			var para = {
				ClassId: classId,
				SubjectId: subjectId,
				SectionIdColl:'',
			};
			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetLessonPlanByClassSubject",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {

					$timeout(function () {
						var dd = res.data.Data;
						$scope.newListOfLessonPlan.NoOfLesson = dd.NoOfLesson;
						$scope.newListOfLessonPlan.TranId = dd.TranId;
						$scope.newListOfLessonPlan.DetailsColl = dd.DetailsColl;

						 
					});
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


	$scope.CurrentLessionTopic = {};
	$scope.ClickOnTopic = function (lession, topic) {

		if (topic.PlanStartDateDet)
			topic.PlanStartDate_AD = new Date(topic.PlanStartDateDet.dateAD);

		if (topic.PlanEndDateDet)
			topic.PlanEndDate_AD = new Date(topic.PlanEndDateDet.dateAD);

		if (topic.PlanStartDate_AD && topic.PlanEndDate_AD) {
			$scope.CurrentLessionTopic = {
				LessonId: lession.LessonId,
				LessonSNo: lession.SNo,
				TopicSNo: topic.SNo,
				LessonName: lession.LessonName,
				TopicName: topic.TopicName,
				ContentsColl: [],
			};

			var para = {
				LessonId: lession.LessonId,
				LessonSNo: lession.SNo,
				TopicSNo: topic.SNo
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetLessonTopicTeacherContent",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {

					angular.forEach(res.data.Data, function (dt) {
						$scope.CurrentLessionTopic.ContentsColl.push({
							FilePath: dt.FilePath,
							FileName: dt.FileName,
							ForDate_TMP: new Date(dt.ForDate),
							Contents: dt.Contents
						});
					});

					if ($scope.CurrentLessionTopic.ContentsColl.length == 0) {

						var sDate = new Date(topic.PlanStartDate_AD);

						for (var d = new Date(topic.PlanStartDate_AD); d <= new Date(topic.PlanEndDate_AD); d.setDate(d.getDate() + 1)) {

							$timeout(function () {								
								$scope.CurrentLessionTopic.ContentsColl.push({
									FilePath: '',
									FileName: '',
									ForDate_TMP: sDate,
									Contents: ''
								});
								sDate = sDate.addDays(1);
							});

							
											 
						}
					}

					$('#modal-xl').modal('show');


				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
        }
		
		 
	}

	$scope.CallSaveUpdateLessonTopicTeacherContent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var dataColl = [];
		angular.forEach($scope.CurrentLessionTopic.ContentsColl, function (cc) {
			var newBeData = {
				LessonId: $scope.CurrentLessionTopic.LessonId,
				LessonSNo: $scope.CurrentLessionTopic.LessonSNo,
				TopicSNo: $scope.CurrentLessionTopic.TopicSNo,
				FileName: cc.FileName,
				FilePath: cc.FilePath,
				ForDate: $filter('date')(new Date(cc.ForDateDet.dateAD), 'yyyy-MM-dd'),
				Contents:cc.Contents
			};
			dataColl.push(newBeData);
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveLessonTopicTeacherContent",
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
	 
	$scope.AddContentDetails = function (ind) {
		if ($scope.CurrentLessionTopic.ContentsColl) {
			if ($scope.CurrentLessionTopic.ContentsColl.length > ind + 1) {
				$scope.CurrentLessionTopic.ContentsColl.splice(ind + 1, 0, {
					FilePath: '',
					FileName: '',
					ForDate_TMP: null,
					Contents: ''
				})
			} else {
				$scope.CurrentLessionTopic.ContentsColl.push({
					FilePath: '',
					FileName: '',
					ForDate_TMP: null,
					Contents: ''
				})
			}
		}
		 
	};
	$scope.delContentDetails = function (ind) {
		if ($scope.CurrentLessionTopic.ContentsColl) {
			if ($scope.CurrentLessionTopic.ContentsColl.length > 1) {
				$scope.CurrentLessionTopic.ContentsColl.splice(ind, 1);
			}
		}
		 
	};



	$scope.CurrentLessonC = {};
	$scope.ClickOnLessonC = function (lession) {

		if (lession.PlanStartDateDet)
			lession.PlanStartDate_AD = new Date(lession.PlanStartDateDet.dateAD);

		if (lession.PlanEndDateDet)
			lession.PlanEndDate_AD = new Date(lession.PlanEndDateDet.dateAD);

		if (lession.PlanStartDate_AD) {
			$scope.CurrentLessonC = {
				LessonId: lession.LessonId,
				LessonSNo: lession.SNo,			 
				LessonName: lession.LessonName,			 
				ContentsColl: [],
			};

			var para = {
				LessonId: lession.LessonId,
				LessonSNo: lession.SNo 
			};

			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetLessonTeacherContent",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {

					angular.forEach(res.data.Data, function (dt) {
						$scope.CurrentLessonC.ContentsColl.push({
							FilePath: dt.FilePath,
							FileName: dt.FileName,
							ForDate_TMP: new Date(dt.ForDate),
							Contents: dt.Contents
						});
					});

					if ($scope.CurrentLessonC.ContentsColl.length == 0) {

						var sDate = new Date(lession.PlanStartDate_AD);

						for (var d = new Date(lession.PlanStartDate_AD); d <= new Date(lession.PlanEndDate_AD); d.setDate(d.getDate() + 1)) {

							$timeout(function () {
								$scope.CurrentLessonC.ContentsColl.push({
									FilePath: '',
									FileName: '',
									ForDate_TMP: sDate,
									Contents: ''
								});
								sDate = sDate.addDays(1);
							});



						}
					}

					$('#modal-xl-1').modal('show');


				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}


	}

	$scope.CallSaveUpdateLessonTeacherContent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var dataColl = [];
		angular.forEach($scope.CurrentLessonC.ContentsColl, function (cc) {
			var newBeData = {
				LessonId: $scope.CurrentLessonC.LessonId,
				LessonSNo: $scope.CurrentLessonC.LessonSNo, 
				FileName: cc.FileName,
				FilePath: cc.FilePath,
				ForDate: $filter('date')(new Date(cc.ForDateDet.dateAD), 'yyyy-MM-dd'),
				Contents: cc.Contents
			};
			dataColl.push(newBeData);
		});

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveLessonTeacherContent",
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

	$scope.AddContentDetails1 = function (ind) {
		if ($scope.CurrentLessonC.ContentsColl) {
			if ($scope.CurrentLessonC.ContentsColl.length > ind + 1) {
				$scope.CurrentLessonC.ContentsColl.splice(ind + 1, 0, {
					FilePath: '',
					FileName: '',
					ForDate_TMP: null,
					Contents: ''
				})
			} else {
				$scope.CurrentLessonC.ContentsColl.push({
					FilePath: '',
					FileName: '',
					ForDate_TMP: null,
					Contents: ''
				})
			}
		}

	};
	$scope.delContentDetails1 = function (ind) {
		if ($scope.CurrentLessonC.ContentsColl) {
			if ($scope.CurrentLessonC.ContentsColl.length > 1) {
				$scope.CurrentLessonC.ContentsColl.splice(ind, 1);
			}
		}

	};

});