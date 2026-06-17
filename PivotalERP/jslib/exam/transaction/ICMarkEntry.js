app.controller('MarkEntryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'MarkEntry';
	var gSrv = GlobalServices;
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();


		$scope.currentPages = {
			ICMarkSubject: 1,
			MarkSubmittedStatus: 1,
			MarkSubmittedStatusPending: 1

		};

		$scope.searchData = {
			ICMarkSubject: '',
			MarkSubmittedStatus: '',
			MarkSubmittedStatusPending: ''

		};

		$scope.perPage = {
			ICMarkSubject: GlobalServices.getPerPageRow(),
			MarkSubmittedStatus: gSrv.getPerPageRow(),
			MarkSubmittedStatusPending: gSrv.getPerPageRow()

		};

		$scope.AssessmentList = [];
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllAssessmentType",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				// Filter only active items
				$scope.AssessmentList = res.data.Data.filter(function (item) {
					return item.IsActive === true;
				});
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.EvaliationToolList = [];
		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllEvaluationArea",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EvaliationToolList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



		$scope.SubjectList = {};
		gSrv.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.newICMarkSubject = {
			TranId: null,
			ClassId: null,
			SubjectId: null,
			LessonId: null,
			TopicName: "",
			StudentId: null,
			IndicatorId: null,
			Evaluation: 0,
			Marks: null,
			Remarks: "",
			AssessmentDate_TMP: new Date(),
			Mode: 'Save'
		};

		$scope.newStudentWise = {
			AssessmentDate_TMP:new Date()
        }
		//$scope.GetAllMarkEntryList();
	}

	$scope.ClearICMarkSubject = function () {
		$scope.newICMarkSubject = {
			TranId: null,
			ClassId: null,
			SubjectId: null,
			LessonId: null,
			TopicName: "",
			StudentId: null,
			IndicatorId: null,
			Evaluation: 0,
			Marks: null,
			Remarks: "",
			Mode: 'Save'
		};
	}


	//************************* MarkEntry *********************************
	$scope.isCopyDisabled = function () {
		return $scope.IStudentsDetailsSubjectsWiseList.some(function (item) {
			return item.Marks !== null && item.Marks !== undefined && item.Marks !== '';
		});
	};

	$scope.isCopyDisabledStudentWise = function () {
		return $scope.TopicForStudentWiseICList.some(function (item) {
			return item.Marks !== null && item.Marks !== undefined && item.Marks !== '';
		});
	};

	$scope.IsValidSubjectWise = function () {
		//if ($scope.newICMarkSubject.IndicatorName.isEmpty()) {
		//	Swal.fire('Please ! Enter Indicator Name');
		//	return false;
		//}
		return true;
	}

	$scope.SaveUpdateICMarkSubject = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataToSave = [];

		const assessmentDate = $scope.newICMarkSubject.AssessmentDateDet?.dateAD || null;

		$scope.IStudentsDetailsSubjectsWiseList.forEach(function (emp) {
			emp.Indicators.forEach(function (ph) {
				if (emp.Marks > 0) {
					dataToSave.push({
						StudentId: emp.StudentId,
						IndicatorName: ph.IndicatorName,
						IndicatorSNo: ph.IndicatorSNo,
						Evaluation: ph.Evaluation,
						Marks: emp.Marks,
						Remarks: emp.Remarks,
						ClassId: $scope.newICMarkSubject.SelectedClass.ClassId,
						SectionId: $scope.newICMarkSubject.SelectedClass.SectionId,
						SubjectId: $scope.newICMarkSubject.SubjectId,
						LessonId: $scope.newICMarkSubject.LessonId,
						TopicName: $scope.newICMarkSubject.TopicName,
						//Added by Suresh on 6 Baishakh 2082
						EvaluationAreaId: ph.EvaluationAreaId,
						IndicatorId: ph.IndicatorId,
						AssessmentTypeId: $scope.newICMarkSubject.AssessmentTypeId,
						AssessmentDate: assessmentDate
					});
				}
			})
		});

		if (dataToSave.length === 0) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire("Select the evaluation to save the data.");
			return;
		}


		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveUpdateICMarkSubject",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("dataToSave", angular.toJson(data.dataToSave));
				/*formData.append("jsonData", angular.toJson(data.jsonData));*/
				return formData;
			},
			data: { dataToSave: dataToSave }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				/*$scope.ClearICMarkSubject();*/
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	$scope.hasValidIndicators = function (indicators) {
		return indicators && indicators.some(function (indicator) {
			return indicator.IndicatorName && indicator.IndicatorName.trim() !== '';
		});
	};




	$scope.GetIStudentsDetailsSubjectsWise = function () {
		if ($scope.newICMarkSubject.SelectedClass && $scope.newICMarkSubject.SubjectId > 0 && $scope.newICMarkSubject.LessonId > 0 && $scope.newICMarkSubject.TopicName != null) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newICMarkSubject.SelectedClass.ClassId,
				SectionId: $scope.newICMarkSubject.SelectedClass.SectionId,
				FilterSection: $scope.newICMarkSubject.SelectedClass.FilterSection,
				SubjectId: $scope.newICMarkSubject.SubjectId,
				LessonId: $scope.newICMarkSubject.LessonId,
				TopicName: $scope.newICMarkSubject.TopicName,
				AssessmentTypeId: $scope.newICMarkSubject.AssessmentTypeId,
				CFAssessmentTypeId: $scope.newICMarkSubject.CFAssessmentTypeId
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetIStudentsDetailsSubjectsWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var data = res.data.Data;

					console.log("Received data:", data);

					if (!Array.isArray(data)) {
						console.error("Data is not an array:", data);
						Swal.fire('Failed: Data format is incorrect.');
						return;
					}

					// Process data to group students and their indicators
					var groupedStudents = data.reduce((acc, item) => {
						let student = acc.find(s => s.RegdNo === item.RegdNo);
						if (!student) {
							// Create a new student entry
							student = {
								RegdNo: item.RegdNo,
								SectionName: item.SectionName,
								RollNumber: item.RollNumber,
								StudentName: item.StudentName,
								StudentId: item.StudentId,
								Marks: item.Marks,
								Remarks: item.Remarks,
								Indicators: []
							};
							acc.push(student);
						}

						// Add indicator to the student
						if (item.IndicatorName && item.SNo != null) {
							student.Indicators.push({
								IndicatorName: item.IndicatorName,
								IndicatorSNo: item.SNo,
								Evaluation: item.Evaluation,
								//Added on baishakh 6 2082
								EvaluationAreaId: item.EvaluationAreaId,
								IndicatorId: item.IndicatorId
							});
						}

						return acc;
					}, []);

					console.log("Processed and grouped student data:", groupedStudents);

					$scope.IStudentsDetailsSubjectsWiseList = groupedStudents;

					//if ($scope.newICMarkSubject.AssessmentTypeId > 0) {
					//	if (data.length > 0 && data[0].AssessmentDate) {
					//		let parsedDate = new Date(data[0].AssessmentDate);
					//		if (!isNaN(parsedDate)) {
					//			$scope.newICMarkSubject.AssessmentDate_TMP = parsedDate;
					//		} else {
					//			$scope.newICMarkSubject.AssessmentDate_TMP = new Date(); // fallback to today
					//		}
					//	} else {
					//		$scope.newICMarkSubject.AssessmentDate_TMP = new Date(); // fallback to today
					//	}
					//} else {
					//	// If AssessmentType is not selected, don't set date
					//	$scope.newICMarkSubject.AssessmentDate_TMP = null;
					//}


				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}).catch(function (error) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

				console.error('Request failed:', error);
				Swal.fire('Failed: ' + error.message);
			});
		}
	};


	//$scope.GetClassWiseSubjectListAdd = function (classId) {
	//	$scope.SubjectListAdd = [];
	//	var para = {
	//		ClassId: classId
	//	};

	//	if (classId > 0) {
	//		$http({
	//			method: 'POST',
	//			url: base_url + "Academic/Creation/GetSubjectListForLessonPlan",
	//			dataType: "json",
	//			data: JSON.stringify(para)
	//		}).then(function (res) {
	//			hidePleaseWait();
	//			$scope.loadingstatus = "stop";
	//			if (res.data.IsSuccess && res.data.Data) {
	//				$scope.SubjectListAdd = res.data.Data;
	//			} else {
	//				Swal.fire(res.data.ResponseMSG);
	//			}
	//		}, function (reason) {
	//			Swal.fire('Failed' + reason);
	//		});
	//	}

	//}


	$scope.GetSubjectLessonWise = function () {
		if ($scope.newICMarkSubject.SelectedClass.ClassId && $scope.newICMarkSubject.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newICMarkSubject.SelectedClass.ClassId,
				SubjectId: $scope.newICMarkSubject.SubjectId
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
		if ($scope.newICMarkSubject.LessonId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				LessonId: $scope.newICMarkSubject.LessonId
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


	$scope.updateMarks = function (student) {
		var count = 0;
		angular.forEach(student.Indicators, function (indicator) {
			if (indicator.Evaluation) {
				count++;
			}
		});
		student.Marks = count;
	};

	// Initialize Marks for all students
	angular.forEach($scope.IStudentsDetailsSubjectsWiseList, function (student) {
		student.Marks = 0;
	});


	//NEw Code Added
	$scope.GetClassWiseSubMap = function () {
		$scope.newICMarkSubject.SubjectList = [];
		/*	$scope.newSubjectWise.StudentColl = [];*/
		if ($scope.newICMarkSubject.SelectedClass) {
			var para = {
				ClassId: $scope.newICMarkSubject.SelectedClass.ClassId,
				SectionIdColl: ($scope.newICMarkSubject.SelectedClass.SectionId ? $scope.newICMarkSubject.SelectedClass.SectionId.toString() : ''),
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
								$scope.newICMarkSubject.SubjectList.push(subDet);
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

	//StudentWise Tab Code Starts
	$scope.GetClassWiseSubMapStW = function () {
		$scope.newStudentWise.SubjectList = [];
		$scope.newStudentWise.StudentList = [];
		if ($scope.newStudentWise.SelectedClass) {
			var para = {
				ClassId: $scope.newStudentWise.SelectedClass.ClassId,
				SectionIdColl: ($scope.newStudentWise.SelectedClass.SectionId ? $scope.newStudentWise.SelectedClass.SectionId.toString() : ''),
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
								$scope.newStudentWise.SubjectList.push(subDet);
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


	$scope.GetSubjectLessonAndStudentList = function () {
		if ($scope.newStudentWise.SelectedClass.ClassId && $scope.newStudentWise.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var paraLesson = {
				ClassId: $scope.newStudentWise.SelectedClass.ClassId,
				SubjectId: $scope.newStudentWise.SubjectId
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetSubjectLessonWise",
				dataType: "json",
				data: JSON.stringify(paraLesson)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

				if (res.data.IsSuccess && res.data.Data) {
					$scope.SubjectLessonWiseList1 = res.data.Data;

					var paraStudent = {
						ClassId: $scope.newStudentWise.SelectedClass.ClassId,
						SectionId: $scope.newStudentWise.SelectedClass.SectionId,
						ExamTypeId: $scope.newStudentWise.ExamTypeId
					};

					$scope.newStudentWise.StudentList = [];
					$http({
						method: 'POST',
						url: base_url + "Academic/Transaction/GetStudentForTran",
						dataType: "json",
						data: JSON.stringify(paraStudent)
					}).then(function (res) {
						if (res.data.IsSuccess) {
							$scope.newStudentWise.StudentList = res.data.Data;
						} else {
							Swal.fire(res.data.ResponseMSG);
						}
					}, function (reason) {
						Swal.fire('Failed to get student list: ' + reason);
					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire('Failed to get subject lesson wise list: ' + reason);
			});
		}
	}



	$scope.GetTopicForStudentWiseIC = function () {
		if ($scope.newStudentWise.SelectedClass && $scope.newStudentWise.SubjectId > 0 && $scope.newStudentWise.LessonId > 0 && $scope.newStudentWise.StudentId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();

			var para = {
				ClassId: $scope.newStudentWise.SelectedClass.ClassId,
				SectionId: $scope.newStudentWise.SelectedClass.SectionId,
				FilterSection: $scope.newStudentWise.SelectedClass.FilterSection,
				SubjectId: $scope.newStudentWise.SubjectId,
				LessonId: $scope.newStudentWise.LessonId,
				StudentId: $scope.newStudentWise.StudentId,
				//new field
				AssessmentTypeId: $scope.newStudentWise.AssessmentTypeId,
				CFAssessmentTypeId: $scope.newStudentWise.CFAssessmentTypeId
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetTopicForStudentWiseIC",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

				if (res.data.IsSuccess && res.data.Data) {
					var data = res.data.Data;
					console.log("Received data:", data);

					if (!Array.isArray(data)) {
						console.error("Data is not an array:", data);
						Swal.fire('Failed: Data format is incorrect.');
						return;
					}

					// Grouping by TranId (unique for each topic)
					var groupedByTopic = data.reduce((acc, item) => {
						// Find if the topic already exists in the accumulator
						let topic = acc.find(t => t.TranId === item.TranId);

						if (topic) {
							// Add the new indicator to the existing topic
							topic.Indicators.push({
								IndicatorName: item.IndicatorName,
								IndicatorSNo: item.SNo,
								Evaluation: item.Evaluation,
								EvaluationAreaId: item.EvaluationAreaId
							});
						} else {
							// If this topic hasn't been added yet, create a new entry
							acc.push({
								TranId: item.TranId,
								TopicName: item.TopicName,
								Marks: item.Marks,
								Remarks: item.Remarks,
								EvaluationType: {}, // Initialize if needed
								RemarksType: item.RemarksType,
								Indicators: [{
									IndicatorName: item.IndicatorName,
									IndicatorSNo: item.SNo,
									Evaluation: item.Evaluation,
									EvaluationAreaId: item.EvaluationAreaId
								}]
							});
						}
						return acc;
					}, []);

					// Bind the grouped topics and their indicators to the scope for rendering in the view
					$scope.TopicForStudentWiseICList = groupedByTopic;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}).catch(function (error) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				console.error('Request failed:', error);
				Swal.fire('Failed: ' + error.message);
			});
		}
	};


	$scope.SaveUpdateStudentWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataToSave = [];

		const assessmentDate = $scope.newStudentWise.AssessmentDateDet?.dateAD || null;

		$scope.TopicForStudentWiseICList.forEach(function (emp) {
			emp.Indicators.forEach(function (ph) {
				if (emp.Marks > 0) {
					dataToSave.push({
						StudentId: $scope.newStudentWise.StudentId,
						IndicatorName: ph.IndicatorName,
						IndicatorSNo: ph.IndicatorSNo,
						Evaluation: ph.Evaluation,
						Marks: emp.Marks,
						Remarks: emp.Remarks,
						ClassId: $scope.newStudentWise.SelectedClass.ClassId,
						SectionId: $scope.newStudentWise.SelectedClass.SectionId,
						SubjectId: $scope.newStudentWise.SubjectId,
						LessonId: $scope.newStudentWise.LessonId,
						TopicName: emp.TopicName,
						//Added by Suresh on 6 Baishakh 2082
						EvaluationAreaId: ph.EvaluationAreaId,
						IndicatorId: ph.IndicatorId,
						AssessmentTypeId: $scope.newStudentWise.AssessmentTypeId,
						AssessmentDate: assessmentDate
					});
				}
			})
		});

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveUpdateICMarkSubject",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("dataToSave", angular.toJson(data.dataToSave));
				/*formData.append("jsonData", angular.toJson(data.jsonData));*/
				return formData;
			},
			data: { dataToSave: dataToSave }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				/*$scope.ClearICMarkSubject();*/
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	//Studentwise Code Ends


	//IC MarkEntry Status Code Starts
	$scope.GetClassWiseSubMapForStatus = function () {
		$scope.newStatus.SubjectList = [];
		$scope.newStatus.StudentList = [];
		if ($scope.newStatus.SelectedClass) {
			var para = {
				ClassId: $scope.newStatus.SelectedClass.ClassId,
				SectionIdColl: ($scope.newStatus.SelectedClass.SectionId ? $scope.newStatus.SelectedClass.SectionId.toString() : ''),
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
								$scope.newStatus.SubjectList.push(subDet);
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

	$scope.GetSubjectLessonWiseStatus = function () {
		if ($scope.newStatus.SelectedClass.ClassId && $scope.newStatus.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newStatus.SelectedClass.ClassId,
				SubjectId: $scope.newStatus.SubjectId
			};
			$scope.SubjectLessonWiseListSt = [];

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetSubjectLessonWise",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.SubjectLessonWiseListSt = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}


	$scope.GetAllMarkSubmittedStatusList = function () {

		if ($scope.newStatus && $scope.newStatus.LessonId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			$scope.PendingMarkList = [];
			$scope.SubmitMarkList = [];

			var para = {
				ClassId: $scope.newStatus.SelectedClass.ClassId,
				SectionId: $scope.newStatus.SelectedClass.SectionId,
				SubjectId: $scope.newStatus.SubjectId,
				LessonId: $scope.newStatus.LessonId,
			};
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetICMArkEntryStatus",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					angular.forEach(res.data.Data, function (d) {
						if (d.IsPending == true)
							$scope.PendingMarkList.push(d);
						else
							$scope.SubmitMarkList.push(d);
					});
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	}
	//IC MArk Entry Status Code Ends


	$scope.DeleteICMarkEntry = function () {
		if (!$scope.newICMarkSubject.SubjectId) {
			Swal.fire("Please select a subject.");
			return;
		}
		if (!$scope.newICMarkSubject.LessonId) {
			Swal.fire("Please select a lesson.");
			return;
		}
		if (!$scope.newICMarkSubject.TopicName) {
			Swal.fire("Please enter the topic name.");
			return;
		}
		if (!$scope.newICMarkSubject.AssessmentTypeId) {
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
					ClassId: $scope.newICMarkSubject.SelectedClass.ClassId,
					SectionId: $scope.newICMarkSubject.SelectedClass.SectionId,
					FilterSection: $scope.newICMarkSubject.SelectedClass.FilterSection,
					SubjectId: $scope.newICMarkSubject.SubjectId,
					LessonId: $scope.newICMarkSubject.LessonId,
					TopicName: $scope.newICMarkSubject.TopicName,
					AssessmentTypeId: $scope.newICMarkSubject.AssessmentTypeId,
				};
				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DeleteICMarkEntry",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						Swal.fire("Data Deleted successfully.");
						$scope.GetIStudentsDetailsSubjectsWise();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});
	};


	$scope.DeleteICMarkEntryStudentWise = function () {
		if (!$scope.newStudentWise.SubjectId) {
			Swal.fire("Please select a subject.");
			return;
		}
		if (!$scope.newStudentWise.LessonId) {
			Swal.fire("Please select a lesson.");
			return;
		}
		if (!$scope.newStudentWise.StudentId) {
			Swal.fire("Please select student.");
			return;
		}
		if (!$scope.newStudentWise.AssessmentTypeId) {
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
					ClassId: $scope.newStudentWise.SelectedClass.ClassId,
					SectionId: $scope.newStudentWise.SelectedClass.SectionId,
					FilterSection: $scope.newStudentWise.SelectedClass.FilterSection,
					SubjectId: $scope.newStudentWise.SubjectId,
					LessonId: $scope.newStudentWise.LessonId,
					StudentId: $scope.newStudentWise.StudentId,
					AssessmentTypeId: $scope.newStudentWise.AssessmentTypeId,
				};
				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DeleteICMarkEntryStudentWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						Swal.fire("Data Deleted successfully.");
						$scope.GetTopicForStudentWiseIC();
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