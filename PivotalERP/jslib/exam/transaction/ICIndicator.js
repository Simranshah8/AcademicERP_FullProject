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

	//************************* Indicator *********************************
	$scope.GetClassWiseSubjectList = function (classId) {
		$scope.SubjectListAdd = [];		
		var para = {
			ClassId: classId
		};

		if (classId > 0) {
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

	}

	$scope.GetClassWiseSubjectListRpt = function (classId) {
		$scope.SubjectListRpt = [];
		var para = {
			ClassId: classId
		};
		if (classId > 0) {
			$http({
				method: 'POST',
				url: base_url + "Academic/Creation/GetSubjectListForLessonPlan",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.SubjectListRpt = res.data.Data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	$scope.GetTopicWiseIndicator = function () {
		$scope.newIndicator.IndicatorDetailsColl = [];
		if ($scope.newIndicator.LessonId != null && !$scope.newIndicator.TopicName == "") {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				LessonId: $scope.newIndicator.LessonId,
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
		if ($scope.newDet.ClassId != null && !$scope.newDet.SubjectId != null) {
			var para = {
				ClassId: $scope.newDet.ClassId,
				SubjectId: $scope.newDet.SubjectId,
				LessonId: $scope.newDet.LessonId,
				TopicName: $scope.newDet.TopicName,
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
		if ($scope.newIndicator.ClassId && $scope.newIndicator.SubjectId>0) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newIndicator.ClassId,
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
		if ($scope.newIndicator.LessonId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				LessonId: $scope.newIndicator.LessonId
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
		if ($scope.newDet.ClassId && $scope.newDet.SubjectId > 0) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newDet.ClassId,
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
		if ($scope.newDet.LessonId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				LessonId: $scope.newDet.LessonId
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
		if ($scope.newSummary.ClassId) {
			$scope.loadingstatus = "running";
			showPleaseWait();
			var para = {
				ClassId: $scope.newSummary.ClassId,
				SubjectId: $scope.newSummary.SubjectId
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