
app.controller('ExamTypeWiseResultSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Result Setup';


	var glbS = GlobalServices;
	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();


		$scope.ClassSection = {};
		glbS.getClassSectionList().then(function (res) {
			$scope.ClassSection = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.SubjectList = {};
		glbS.getSubjectList().then(function (res) {
			$scope.SubjectList = mx(res.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ExamTypeList = [];
		glbS.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ExamTypeGroupList = [];
		glbS.getExamTypeGroupList().then(function (res) {
			$scope.ExamTypeGroupList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newExamTypeWiseResultSetup = {
			TranId: 0,
			IsSubjectWise: true,
			ExamTypeWiseResultSetupDetailsColl: [],
			TotalFailSubject: 0,
			
			Mode: 'Save'
		};


		$scope.newExamGroupWiseResultSetup = {
			TranId: 0,
			IsSubjectWise: true,
			ExamGroupWiseResultSetupDetailsColl: [],
			TotalFailSubject: 0,
			Mode: 'Save'
		};
	}

	$scope.GetClassWiseSubMap = function () {

		$scope.newExamTypeWiseResultSetup.SubjectList = [];
		$scope.newExamTypeWiseResultSetup.ExamTypeWiseResultSetupDetailsColl = [];

		if ($scope.newExamTypeWiseResultSetup.ClassId && $scope.newExamTypeWiseResultSetup.ClassId > 0) {
			var para = {
				ClassId: $scope.newExamTypeWiseResultSetup.ClassId,
				SectionIdColl: ($scope.newExamTypeWiseResultSetup.SectionId ? $scope.newExamTypeWiseResultSetup.SectionId.toString() : '')
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
					var SubjectMappingColl = res.data.Data;

					if (SubjectMappingColl.length == 0) {
						Swal.fire('Subject Mapping Not Found');
					}
					else if (SubjectMappingColl.length > 0) {
						angular.forEach(SubjectMappingColl, function (sm) {
							if (sm.PaperType < 4) {
								var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
								if (subDet) {
									subDet.PaperType = sm.PaperType;
									subDet.TH = false;
									subDet.PR = false;
									$scope.newExamTypeWiseResultSetup.SubjectList.push(subDet);
								}
                            }							
						});

						if ($scope.newExamTypeWiseResultSetup.ExamTypeId && $scope.newExamTypeWiseResultSetup.ExamTypeId > 0) {

							var para1 = {
								ClassId: para.ClassId,
								SectionIdColl: para.SectionIdColl,
								ExamTypeId: $scope.newExamTypeWiseResultSetup.ExamTypeId
							};
							$http({
								method: 'POST',
								url: base_url + "Exam/Transaction/GetExamTypeWiseResultSetupById",
								dataSchedule: "json",
								data: JSON.stringify(para1)
							}).then(function (res1) {
								if (res1.data.IsSuccess && res1.data.Data) {
									var exExamTypeWiseResultSetup = res1.data.Data;

									if (exExamTypeWiseResultSetup) {
										$scope.newExamTypeWiseResultSetup.TotalFailSubject = exExamTypeWiseResultSetup.TotalFailSubject;
										$scope.newExamTypeWiseResultSetup.IsSubjectWise = exExamTypeWiseResultSetup.IsSubjectWise;

										if (exExamTypeWiseResultSetup.ExamTypeWiseResultSetupDetailsColl && exExamTypeWiseResultSetup.ExamTypeWiseResultSetupDetailsColl.length > 0) {
											var query = mx(exExamTypeWiseResultSetup.ExamTypeWiseResultSetupDetailsColl);
											angular.forEach($scope.newExamTypeWiseResultSetup.SubjectList, function (exS) {
												var fData = query.firstOrDefault(p1 => p1.SubjectId == exS.SubjectId);
												if (fData) {
													exS.TH = fData.TH;
													exS.PR = fData.PR;
												}
											});
										}
									}
								} else {
									Swal.fire(res.data.ResponseMSG);
								}

							}, function (reason) {
								Swal.fire('Failed' + reason);
							});

						}
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.DelExamTypeWiseColl = function (refData) {
        if ($scope.newExamTypeWiseResultSetup.ClassId && $scope.newExamTypeWiseResultSetup.ExamTypeId) {

            Swal.fire({
                title: 'Do you want to delete the selected data?',
                showCancelButton: true,
                confirmButtonText: 'Delete',
            }).then((result) => {
                if (result.isConfirmed) {
                    $scope.loadingstatus = "running";
                    showPleaseWait();

                    let classId = $scope.newExamTypeWiseResultSetup.ClassId;
                    let sectionId = $scope.newExamTypeWiseResultSetup.SectionId ? $scope.newExamTypeWiseResultSetup.SectionId.toString() : null;
                    const para = {
                        ClassId: classId,
						SectionIdColl: sectionId,
						ExamTypeId: $scope.newExamTypeWiseResultSetup.ExamTypeId,
                    };
                    $http({
                        method: 'POST',
                        url: base_url + "Exam/Transaction/DelExamTypeWiseColl",
                        dataType: "json",
                        data: JSON.stringify(para)
                    }).then(function (res) {
                        hidePleaseWait();
                        $scope.loadingstatus = "stop";
                        if (res.data.IsSuccess) {
                            $scope.ClearExamTypeWiseResultSetup();
                            Swal.fire(res.data.ResponseMSG);
                        } else {
                            Swal.fire(res.data.ResponseMSG);
                        }

                    }, function (reason) {
                        Swal.fire('Failed' + reason);
                    });
                }
            });
        } else {
            Swal.fire('Validation Error', 'Please ensure all required fields are selected.', 'warning');
        }
	};




	$scope.ClearExamTypeWiseResultSetup = function () {
		$scope.newExamTypeWiseResultSetup = {
			TranId: 0,
			IsSubjectWise: true,
			ExamTypeWiseResultSetupDetailsColl: [],
			TotalFailSubject: 0,
			
			Mode: 'Save'
		};
	}
	    
	$scope.IsValidExamTypeWiseResultSetup = function () {
		 
		return true;
	}

	$scope.SaveUpdateExamTypeWiseResultSetup = function () {
		if ($scope.IsValidExamTypeWiseResultSetup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamTypeWiseResultSetup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamTypeWiseResultSetup();
					}
				});
			} else
				$scope.CallSaveUpdateExamTypeWiseResultSetup();

		}
	};

	$scope.CallSaveUpdateExamTypeWiseResultSetup = function () {

		var tmpExamTypeWiseResultSetup = {
			TranId: 0,
			ExamTypeId: $scope.newExamTypeWiseResultSetup.ExamTypeId,
			ClassId: $scope.newExamTypeWiseResultSetup.ClassId,
			SectionId: null,
			SectionIdColl: ($scope.newExamTypeWiseResultSetup.SectionId ? $scope.newExamTypeWiseResultSetup.SectionId.toString() : ''),
			TotalFailSubject: ($scope.newExamTypeWiseResultSetup.TotalFailSubject ? $scope.newExamTypeWiseResultSetup.TotalFailSubject : 0),
			
			IsSubjectWise: ($scope.newExamTypeWiseResultSetup.IsSubjectWise ? $scope.newExamTypeWiseResultSetup.IsSubjectWise : false),
			ExamTypeWiseResultSetupDetailsColl: []
		};

		angular.forEach($scope.newExamTypeWiseResultSetup.SubjectList, function (s) {
			tmpExamTypeWiseResultSetup.ExamTypeWiseResultSetupDetailsColl.push({
				SubjectId: s.SubjectId,
				TH: (s.TH ? s.TH : false),
				PR: (s.PR ? s.PR : false),				
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamTypeWiseResultSetup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: tmpExamTypeWiseResultSetup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true)
				$scope.ClearExamTypeWiseResultSetup();

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

	}
	  


	$scope.GetClassWiseSubMapGroup = function () {

		$scope.newExamGroupWiseResultSetup.SubjectList = [];
		$scope.newExamGroupWiseResultSetup.ExamGroupWiseResultSetupDetailsColl = [];

		if ($scope.newExamGroupWiseResultSetup.ClassId && $scope.newExamGroupWiseResultSetup.ClassId > 0) {
			var para = {
				ClassId: $scope.newExamGroupWiseResultSetup.ClassId,
				SectionIdColl: ($scope.newExamGroupWiseResultSetup.SectionId ? $scope.newExamGroupWiseResultSetup.SectionId.toString() : '')
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
					var SubjectMappingColl = res.data.Data;

					if (SubjectMappingColl.length == 0) {
						Swal.fire('Subject Mapping Not Found');
					}
					else if (SubjectMappingColl.length > 0) {
						angular.forEach(SubjectMappingColl, function (sm) {
							if (sm.PaperType < 4) {
								var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
								if (subDet) {
									subDet.PaperType = sm.PaperType;
									subDet.TH = false;
									subDet.PR = false;
									$scope.newExamGroupWiseResultSetup.SubjectList.push(subDet);
								}
							}
						});

						if ($scope.newExamGroupWiseResultSetup.ExamTypeId && $scope.newExamGroupWiseResultSetup.ExamTypeId > 0) {

							var para1 = {
								ClassId: para.ClassId,
								SectionIdColl: para.SectionIdColl,
								ExamTypeId: $scope.newExamGroupWiseResultSetup.ExamTypeId
							};
							$http({
								method: 'POST',
								url: base_url + "Exam/Transaction/GetExamGroupWiseResultSetupById",
								dataSchedule: "json",
								data: JSON.stringify(para1)
							}).then(function (res1) {
								if (res1.data.IsSuccess && res1.data.Data) {
									var exExamTypeWiseResultSetup = res1.data.Data;

									if (exExamTypeWiseResultSetup) {
										$scope.newExamGroupWiseResultSetup.TotalFailSubject = exExamTypeWiseResultSetup.TotalFailSubject;
										$scope.newExamGroupWiseResultSetup.IsSubjectWise = exExamTypeWiseResultSetup.IsSubjectWise;

										if (exExamTypeWiseResultSetup.ExamGroupWiseResultSetupDetailsColl && exExamTypeWiseResultSetup.ExamGroupWiseResultSetupDetailsColl.length > 0) {
											var query = mx(exExamTypeWiseResultSetup.ExamGroupWiseResultSetupDetailsColl);
											angular.forEach($scope.newExamGroupWiseResultSetup.SubjectList, function (exS) {
												var fData = query.firstOrDefault(p1 => p1.SubjectId == exS.SubjectId);
												if (fData) {
													exS.TH = fData.TH;
													exS.PR = fData.PR;
												}
											});
										}
									}
								} else {
									Swal.fire(res.data.ResponseMSG);
								}

							}, function (reason) {
								Swal.fire('Failed' + reason);
							});

						}
					}
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	};

	$scope.ClearExamGroupWiseResultSetup = function () {
		$scope.newExamGroupWiseResultSetup = {
			TranId: 0,
			IsSubjectWise: true,
			ExamGroupWiseResultSetupDetailsColl: [],
			TotalFailSubject: 0,

			Mode: 'Save'
		};
	}



	$scope.DelExamGroupWiseColl = function (refData) {
		if ($scope.newExamGroupWiseResultSetup.ClassId && $scope.newExamGroupWiseResultSetup.ExamTypeId) {
			Swal.fire({
				title: 'Do you want to delete the selected data?',
				showCancelButton: true,
				confirmButtonText: 'Delete',
			}).then((result) => {
				if (result.isConfirmed) {
					$scope.loadingstatus = "running";
					showPleaseWait();
					var para = {
						ClassId: $scope.newExamGroupWiseResultSetup.ClassId,
						SectionIdColl: $scope.newExamGroupWiseResultSetup.SectionId ? $scope.newExamGroupWiseResultSetup.SectionId.toString() : '',
						ExamTypeId: $scope.newExamGroupWiseResultSetup.ExamTypeId,
					};
					$http({
						method: 'POST',
						url: base_url + "Exam/Transaction/DelExamTypeGroupWiseResultSetup",
						dataType: "json",
						data: JSON.stringify(para)
					}).then(function (res) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						if (res.data.IsSuccess) {
							$scope.ClearExamGroupWiseResultSetup();
							Swal.fire(res.data.ResponseMSG);
						} else {
							Swal.fire(res.data.ResponseMSG);
						}

					}, function (reason) {
						Swal.fire('Failed' + reason);
					});
				}
			});
		} else {
			Swal.fire('Validation Error', 'Please ensure all required fields are selected.', 'warning');
		}
	};

	$scope.IsValidExamGroupWiseResultSetup = function () {

		return true;
	}

	$scope.SaveUpdateExamGroupWiseResultSetup = function () {
		if ($scope.IsValidExamGroupWiseResultSetup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamGroupWiseResultSetup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamGroupWiseResultSetup();
					}
				});
			} else
				$scope.CallSaveUpdateExamGroupWiseResultSetup();

		}
	};

	$scope.CallSaveUpdateExamGroupWiseResultSetup = function () {

		var tmpExamTypeWiseResultSetup = {
			TranId: 0,
			ExamTypeGroupId: $scope.newExamGroupWiseResultSetup.ExamTypeId,
			ClassId: $scope.newExamGroupWiseResultSetup.ClassId,
			SectionId: null,
			SectionIdColl: ($scope.newExamGroupWiseResultSetup.SectionId ? $scope.newExamGroupWiseResultSetup.SectionId.toString() : ''),
			TotalFailSubject: ($scope.newExamGroupWiseResultSetup.TotalFailSubject ? $scope.newExamGroupWiseResultSetup.TotalFailSubject : 0),

			IsSubjectWise: ($scope.newExamGroupWiseResultSetup.IsSubjectWise ? $scope.newExamGroupWiseResultSetup.IsSubjectWise : false),
			ExamGroupWiseResultSetupDetailsColl: []
		};

		angular.forEach($scope.newExamGroupWiseResultSetup.SubjectList, function (s) {
			tmpExamTypeWiseResultSetup.ExamGroupWiseResultSetupDetailsColl.push({
				SubjectId: s.SubjectId,
				TH: (s.TH ? s.TH : false),
				PR: (s.PR ? s.PR : false),
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamGroupWiseResultSetup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: tmpExamTypeWiseResultSetup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true)
				$scope.ClearExamTypeWiseResultSetup();

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});

	}


});