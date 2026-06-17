
app.controller('MarkSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Mark Setup';


	var glbS = GlobalServices;
	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.confirmMSG = glbS.getConfirmMSG();
		$scope.perPageColl = glbS.getPerPageList();

		$scope.currentPages = {
			CompleteStatus: 1,
			PendingStatus: 1,
		};

		$scope.perPage = {			
			CompleteStatus: glbS.getPerPageRow(),
			PendingStatus: glbS.getPerPageRow()
		};

		$scope.searchData = {			
			CompleteStatus: '',
			PendingStatus: ''
		};

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


		$scope.ObtMarkAsColl = [{ id: 1, text: 'INPUT' }, { id: 2, text: 'CAS' }];

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

		$scope.newMarkSetup = {
			TranId: 0,
			IsAutoSum: true,
			MarkSetupDetailsColl: [],
			FullMark: 0,
			PassMark: 0,
			Mode: 'Save'
		};


		$scope.newMarkSetupStatus = {
			MarkSetupStatusId: null,
			MarkSetupStatusDetailsColl: [],

			Mode: 'Save'
		};
		$scope.newMarkSetupStatus.MarkSetupStatusDetailsColl.push({});


		$scope.newMarkSetupStatusPending = {
			MarkSetupStatusPendingId: null,
			MarkSetupStatusPendingDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newMarkSetupStatusPending.MarkSetupStatusPendingDetailsColl.push({});

		//$scope.GetAllExamTypeList();
		//$scope.GetAllExamTypeGroupList();
		//$scope.GetAllPrintAdmitCardList();


		$scope.newMarkSetupTransfer = {
			FromExamTypeId: null,
			ToExamTypeId: null

		};

	}

	$scope.GetBranchId = function (bid) {
		if ($scope.BranchColl.length == 1)
			return $scope.BranchColl[0].BranchId
		else
			return bid;
	}
	$scope.ClearSymbolNumberTransfer = function () {
		$scope.newMarkSetupTransfer = {
			FromExamTypeId: null,
			ToExamTypeId: null

		};
	}
	$scope.TransforSymbolNumber = function (refData) {

		if ($scope.newMarkSetupTransfer.FromExamTypeId && $scope.newMarkSetupTransfer.ToExamTypeId) {
			Swal.fire({
				title: 'Do you want to transfer marksetup  of selected examtype ?',
				showCancelButton: true,
				confirmButtonText: 'Transfer',
			}).then((result) => {
				/* Read more about isConfirmed, isDenied below */
				if (result.isConfirmed) {
					$scope.loadingstatus = "running";
					showPleaseWait();

					var para = {
						FromExamTypeId: $scope.newMarkSetupTransfer.FromExamTypeId,
						ToExamTypeId: $scope.newMarkSetupTransfer.ToExamTypeId,
						BranchId: ($scope.BranchColl.length == 1 ? $scope.BranchColl[0].BranchId : $scope.newMarkSetupTransfer.BranchId),
					};

					$http({
						method: 'POST',
						url: base_url + "Exam/Transaction/TransforMarkSetup",
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
		}
	};

	$scope.LoadClassWiseSemesterYear = function (classId, data) {

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
	$scope.GetClassWiseSubMap = function (fromInd) {

		//$scope.loadingstatus = "running";
		//showPleaseWait();

		$scope.newMarkSetup.ClassWiseDetailsColl = [];
		$scope.newMarkSetup.SubjectList = [];

		if ($scope.newMarkSetup.ClassId && $scope.newMarkSetup.ClassId > 0) {

			// Load Class Wise Year and Semester On Class Selection Changed
			if (fromInd == 2) {
				$scope.newMarkSetup.SemesterId = null;
				$scope.newMarkSetup.ClassYearId = null;
				$scope.LoadClassWiseSemesterYear($scope.newMarkSetup.ClassId, $scope.newMarkSetup);
			}


			var para = {
				ClassId: $scope.newMarkSetup.ClassId,
				SectionIdColl: ($scope.newMarkSetup.SectionId && $scope.newMarkSetup.SectionId.length > 0 ? $scope.newMarkSetup.SectionId.toString() : ''),
				SemesterId: $scope.newMarkSetup.SemesterId,
				ClassYearId: $scope.newMarkSetup.ClassYearId,
				BatchId: $scope.newMarkSetup.BatchId,
				BranchId: ($scope.BranchColl.length == 1 ? $scope.BranchColl[0].BranchId : $scope.newMarkSetup.BranchId),
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

						if ($scope.AcademicConfig.ActiveBatch == true) {
							if (para.BatchId > 0 && (para.SemesterId > 0 || para.ClassYearId > 0)) {
								Swal.fire('Subject Mapping Not Found');
							}
						} else {
							Swal.fire('Subject Mapping Not Found');
						}



					}
					else if (SubjectMappingColl.length > 0) {
						angular.forEach(SubjectMappingColl, function (sm) {
							var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
							if (subDet) {
								subDet.PaperType = sm.PaperType;
								subDet.CRTH = (sm.CRTH > 0 ? sm.CRTH : 0);
								subDet.CRPR = (sm.CRPR > 0 ? sm.CRPR : 0);
								subDet.FMTH = 0;
								subDet.FMPR = 0;
								subDet.PMTH = 0;
								subDet.PMPR = 0;
								subDet.IsInclude = true;
								subDet.SubjectType = 1;
								subDet.OTH = 1;
								subDet.OPR = 1;
								$scope.newMarkSetup.SubjectList.push(subDet);
							}
						});

						if ($scope.newMarkSetup.ExamTypeId && $scope.newMarkSetup.ExamTypeId > 0) {

							var para1 = {
								ClassId: para.ClassId,
								SectionIdColl: para.SectionIdColl,
								ExamTypeId: $scope.newMarkSetup.ExamTypeId,
								SemesterId: $scope.newMarkSetup.SemesterId,
								ClassYearId: $scope.newMarkSetup.ClassYearId,
								BatchId: $scope.newMarkSetup.BatchId,
							};
							$http({
								method: 'POST',
								url: base_url + "Exam/Transaction/GetMarkSetupById",
								dataSchedule: "json",
								data: JSON.stringify(para1)
							}).then(function (res1) {
								if (res1.data.IsSuccess && res1.data.Data) {
									var exMarkSetup = res1.data.Data;

									if (exMarkSetup) {
										$scope.newMarkSetup.FullMark = exMarkSetup.FullMark;
										$scope.newMarkSetup.PassMark = exMarkSetup.PassMark;
										$scope.newMarkSetup.IsAutoSum = exMarkSetup.IsAutoSum;

										if (exMarkSetup.MarksSetupDetailsColl && exMarkSetup.MarksSetupDetailsColl.length > 0) {
											var query = mx(exMarkSetup.MarksSetupDetailsColl);
											angular.forEach($scope.newMarkSetup.SubjectList, function (exS) {
												var fData = query.firstOrDefault(p1 => p1.SubjectId == exS.SubjectId);
												if (fData) {
													exS.CRTH = fData.CRTH;
													exS.CRPR = fData.CRPR;
													exS.FMTH = fData.FMTH;
													exS.FMPR = fData.FMPR;
													exS.PMTH = fData.PMTH;
													exS.PMPR = fData.PMPR;
													exS.IsInclude = fData.IsInclude;
													exS.SubjectType = fData.SubjectType;
													exS.OTH = fData.OTH;
													exS.OPR = fData.OPR;
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

	//$scope.GetClassWiseSubMap = function () {

	//	$scope.newMarkSetup.SubjectList = [];
	//	$scope.newMarkSetup.MarkSetupDetailsColl = [];

	//	if ($scope.newMarkSetup.ClassId && $scope.newMarkSetup.ClassId > 0) {
	//		var para = {
	//			ClassId: $scope.newMarkSetup.ClassId,
	//			SectionIdColl: ($scope.newMarkSetup.SectionId ? $scope.newMarkSetup.SectionId.toString() : '')
	//		};


	//		$http({
	//			method: 'POST',
	//			url: base_url + "Academic/Creation/GetSubjectMappingClassWise",
	//			dataSchedule: "json",
	//			data: JSON.stringify(para)
	//		}).then(function (res) {
	//			hidePleaseWait();
	//			$scope.loadingstatus = "stop";
	//			if (res.data.IsSuccess && res.data.Data) {
	//				var SubjectMappingColl = res.data.Data;

	//				if (SubjectMappingColl.length == 0) {
	//					Swal.fire('Subject Mapping Not Found');
	//				}
	//				else if (SubjectMappingColl.length > 0) {
	//					angular.forEach(SubjectMappingColl, function (sm) {
	//						var subDet = $scope.SubjectList.firstOrDefault(p1 => p1.SubjectId == sm.SubjectId);
	//						if (subDet) {
	//							subDet.PaperType = sm.PaperType;
	//							subDet.CRTH =(sm.CRTH>0 ? sm.CRTH : 0);
	//							subDet.CRPR =(sm.CRPR>0 ? sm.CRPR : 0);
	//							subDet.FMTH = 0;
	//							subDet.FMPR = 0;
	//							subDet.PMTH = 0;
	//							subDet.PMPR = 0;
	//							subDet.IsInclude = true;
	//							subDet.SubjectType = 1;
	//							subDet.OTH = 1;
	//							subDet.OPR = 1; 
	//							$scope.newMarkSetup.SubjectList.push(subDet);
	//						}
	//					});

	//					if ($scope.newMarkSetup.ExamTypeId && $scope.newMarkSetup.ExamTypeId > 0) {

	//						var para1 = {
	//							ClassId: para.ClassId,
	//							SectionIdColl: para.SectionIdColl,
	//							ExamTypeId: $scope.newMarkSetup.ExamTypeId
	//						};
	//						$http({
	//							method: 'POST',
	//							url: base_url + "Exam/Transaction/GetMarkSetupById",
	//							dataSchedule: "json",
	//							data: JSON.stringify(para1)
	//						}).then(function (res1) {
	//							if (res1.data.IsSuccess && res1.data.Data)
	//							{
	//								var exMarkSetup = res1.data.Data;

	//								if (exMarkSetup) {
	//									$scope.newMarkSetup.FullMark = exMarkSetup.FullMark;
	//									$scope.newMarkSetup.PassMark = exMarkSetup.PassMark;
	//									$scope.newMarkSetup.IsAutoSum = exMarkSetup.IsAutoSum;

	//									if (exMarkSetup.MarksSetupDetailsColl && exMarkSetup.MarksSetupDetailsColl.length > 0)
	//									{
	//										var query = mx(exMarkSetup.MarksSetupDetailsColl);
	//										angular.forEach($scope.newMarkSetup.SubjectList, function (exS) {
	//											var fData = query.firstOrDefault(p1 => p1.SubjectId == exS.SubjectId);
	//											if (fData) {													
	//												exS.CRTH = fData.CRTH;
	//												exS.CRPR = fData.CRPR;
	//												exS.FMTH = fData.FMTH;
	//												exS.FMPR = fData.FMPR;
	//												exS.PMTH = fData.PMTH;
	//												exS.PMPR = fData.PMPR;
	//												exS.IsInclude = fData.IsInclude;
	//												exS.SubjectType = fData.SubjectType;
	//												exS.OTH = fData.OTH;
	//												exS.OPR = fData.OPR;
	//                                               }
	//										});
	//                                       }
	//                                   }
	//							} else {
	//								Swal.fire(res.data.ResponseMSG);
	//							}

	//						}, function (reason) {
	//							Swal.fire('Failed' + reason);
	//						});

	//					}
	//				}
	//			} else {
	//				Swal.fire(res.data.ResponseMSG);
	//			}

	//		}, function (reason) {
	//			Swal.fire('Failed' + reason);
	//		});
	//	}

	//};

	$scope.ClearMarkSetup = function () {
		$scope.newMarkSetup = {
			TranId: 0,
			IsAutoSum: true,
			MarkSetupDetailsColl: [],
			FullMark: 0,
			PassMark: 0,
			Mode: 'Save'
		};
	}
	$scope.ClearMarkSetupStatus = function () {
		$scope.newMarkSetupStatus = {
			MarkSetupStatusId: null,

			MarkSetupStatusDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newMarkSetupStatus.MarkSetupStatusDetailsColl.push({});

	}

	$scope.ClearMarkSetupStatusPending = function () {
		$scope.newMarkSetupStatusPending = {
			MarkSetupStatusPendingId: null,

			MarkSetupStatusPendingDetailsColl: [],
			Mode: 'Save'
		};
		$scope.newMarkSetupStatusPending.MarkSetupStatusPendingDetailsColl.push({});

	}

	$scope.excludeSelectedExamType = function (item) {
		var bid = $scope.GetBranchId($scope.newMarkSetupTransfer.BranchId);

		return item.id !== $scope.newMarkSetupTransfer.FromExamTypeId && item.BranchId == bid;
	};
	//************************* Mark Setup *********************************

	$scope.IsValidMarkSetup = function () {
		if (!$scope.newMarkSetup.ClassId || $scope.newMarkSetup.ClassId == 0) {
			Swal.fire('Please Select Class');
			return false;
		}

		if (!$scope.newMarkSetup.ExamTypeId || $scope.newMarkSetup.ExamTypeId == 0) {
			Swal.fire('Please Select Exam Type');
			return false;
		}
		if ($scope.newMarkSetup.FullMark < 0) {
			Swal.fire('Full Marks cannot be Negative');
			return false;
		}

		// Check if PassMark is negative
		if ($scope.newMarkSetup.PassMark < 0) {
			Swal.fire('Pass Marks cannot be Negative');
			return false;
		}

		// Check if FullMark is greater than PassMark
		if ($scope.newMarkSetup.FullMark < $scope.newMarkSetup.PassMark) {
			Swal.fire('Full Marks must be greater than Pass Marks');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateMarkSetup = function () {
		if ($scope.IsValidMarkSetup() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newMarkSetup.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateMarkSetup();
					}
				});
			} else
				$scope.CallSaveUpdateMarkSetup();

		}
	};

	$scope.CallSaveUpdateMarkSetup = function () {

		var tmpMarkSetup = {
			TranId: 0,
			ExamTypeId: $scope.newMarkSetup.ExamTypeId,
			ClassId: $scope.newMarkSetup.ClassId,
			SectionId: null,
			SemesterId: $scope.newMarkSetup.SemesterId,
			ClassYearId: $scope.newMarkSetup.ClassYearId,
			BatchId: $scope.newMarkSetup.BatchId,
			SectionIdColl: ($scope.newMarkSetup.SectionId ? $scope.newMarkSetup.SectionId.toString() : ''),
			FullMark: ($scope.newMarkSetup.FullMark ? $scope.newMarkSetup.FullMark : 0),
			PassMark: ($scope.newMarkSetup.PassMark ? $scope.newMarkSetup.PassMark : 0),
			IsAutoSum: ($scope.newMarkSetup.IsAutoSum ? $scope.newMarkSetup.IsAutoSum : false),
			MarksSetupDetailsColl: [],
			FromSectionIdColl: ($scope.newMarkSetup.SectionId && $scope.newMarkSetup.SectionId.length > 0 ? $scope.newMarkSetup.SectionId.toString() : ''),
			ToClassIdColl: ($scope.newMarkSetup.ToClassId_TMP && $scope.newMarkSetup.ToClassId_TMP.length > 0 ? $scope.newMarkSetup.ToClassId_TMP.toString() : ''),
			ToSectionIdColl: ($scope.newMarkSetup.ToSectionId_TMP && $scope.newMarkSetup.ToSectionId_TMP.length > 0 ? $scope.newMarkSetup.ToSectionId_TMP.toString() : ''),
		};

		angular.forEach($scope.newMarkSetup.SubjectList, function (s) {
			tmpMarkSetup.MarksSetupDetailsColl.push({
				SubjectId: s.SubjectId,
				CRTH: (s.CRTH ? s.CRTH : 0),
				CRPR: (s.CRPR ? s.CRPR : 0),
				FMTH: (s.FMTH ? s.FMTH : 0),
				FMPR: (s.FMPR ? s.FMPR : 0),
				PMTH: (s.PMTH ? s.PMTH : 0),
				PMPR: (s.PMPR ? s.PMPR : 0),
				IsInclude: (s.IsInclude ? s.IsInclude : false),
				SubjecType: s.SubjectType,
				OTH: (s.OTH ? s.OTH : 1),
				OPR: (s.OPR ? s.OPR : 1),
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveMarkSetup",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: tmpMarkSetup }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true)
				$scope.ClearMarkSetup();

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});


	}


	$scope.GetMarkSetupById = function (refData) {

		var para = {
			MarkSetupId: refData.MarkSetupId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetMarkSetupById",
			dataSchedule: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newMarkSetup = res.data.Data;
				$scope.newMarkSetup.Mode = 'Modify';

				//document.getElementById('exam-type-section').style.display = "none";
				//document.getElementById('exam-type-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelMarkSetupById = function (refData) {

		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				var para = {
					MarkSetupId: refData.MarkSetupId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelMarkSetup",
					dataSchedule: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllMarkSetupList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Mark Setup Status *********************************


	$scope.GetMarkSetupStatus = function () {
		$scope.PendingMarkSetupList = [];
		$scope.CompletedMarkSetupList = [];

		if ($scope.newMarkSetupStatus.ExamTypeId > 0) {

			var para = {
				ExamTypeId: $scope.newMarkSetupStatus.ExamTypeId,
				//BranchId:$scope.newMarkSetupStatus.BranchId,
				BranchId: ($scope.BranchColl.length == 1 ? $scope.BranchColl[0].BranchId : $scope.newMarkSetupStatus.BranchId),
			};
			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetMarkSetupStatus",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					angular.forEach(res.data.Data, function (rd) {

						if (rd.IsPending == true)
							$scope.PendingMarkSetupList.push(rd);
						else
							$scope.CompletedMarkSetupList.push(rd);
					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}

	}

	//Update by suresh starts
	$scope.ChangeAllCRTH = function () {
		if ($scope.newMarkSet.CRTH !== null) {
			angular.forEach($scope.newMarkSetup.SubjectList, function (ms) {
				if (ms.PaperType != 2) { // keep respect of readonly condition
					ms.CRTH = $scope.newMarkSet.CRTH;
				}
			});
		}
	};

	$scope.ChangeAllCRPR = function () {
		if ($scope.newMarkSet.CRPR !== null) {
			angular.forEach($scope.newMarkSetup.SubjectList, function (ms) {
				if (ms.PaperType != 1) {
					ms.CRPR = $scope.newMarkSet.CRPR;
				}
			});
		}
	};

	$scope.ChangeAllFMTH = function () {
		if ($scope.newMarkSet.FMTH != null) {
			angular.forEach($scope.newMarkSetup.SubjectList, function (ms) {
				if (ms.PaperType != 2 && ms.PaperType != 4) {
					ms.FMTH = $scope.newMarkSet.FMTH;
				}
			});
		}
	};
	
	$scope.ChangeAllFMPR = function () {
		if ($scope.newMarkSet.FMPR != null) {
			angular.forEach($scope.newMarkSetup.SubjectList, function (ms) {
				if (ms.PaperType != 1 && ms.PaperType != 4) {
					ms.FMPR = $scope.newMarkSet.FMPR;
				}
			});
		}
	};

	$scope.ChangeAllPMTH = function () {
		if ($scope.newMarkSet.PMTH != null) {
			angular.forEach($scope.newMarkSetup.SubjectList, function (ms) {
				if (ms.PaperType != 2 && ms.PaperType != 4) {
					ms.PMTH = $scope.newMarkSet.PMTH;
				}
			});
		}
	};


	
	$scope.ChangeAllPMPR = function () {
		if ($scope.newMarkSet.PMPR != null) {
			angular.forEach($scope.newMarkSetup.SubjectList, function (ms) {
				if (ms.PaperType != 1 && ms.PaperType != 4) {
					ms.PMPR = $scope.newMarkSet.PMPR;
				}
			});
		}
	};

	$scope.ChangeAllIsInclude = function () {
		angular.forEach($scope.newMarkSetup.SubjectList, function (ms) {
			ms.IsInclude = $scope.newMarkSet.IsInclude; // copy from header checkbox
		});
	};

	$scope.ChangeAllOTH = function () {
		if ($scope.newMarkSet.OTH !== null) {
			angular.forEach($scope.newMarkSetup.SubjectList, function (ms) {
				ms.OTH = $scope.newMarkSet.OTH;
			});
		}
	};

	$scope.ChangeAllOPR = function () {
		if ($scope.newMarkSet.OPR !== null) {
			angular.forEach($scope.newMarkSetup.SubjectList, function (ms) {
				ms.OPR = $scope.newMarkSet.OPR;
			});
		}
	};


	//Ends

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

});