app.controller('DiscountSetupController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Discount Setup';
	OnClickDefault();

	$scope.LoadData = function () {
		$('.select2').select2();
		//$scope.MonthList = GlobalServices.getMonthList();
		$scope.FeeConfig = {};
		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/GetFeeConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FeeConfig = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.MonthList = [];
		$scope.MonthListAsAY = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
				$scope.MonthListAsAY.push({ id: m.NM, text: m.MonthName });
			});
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		//$scope.MonthListAsAY = [];
		//GlobalServices.getAcademicMonthList(null, null).then(function (resAM) {
		//	$scope.MonthListAsAY = [];
		//	angular.forEach(resAM.data.Data, function (m) {
		//		$scope.MonthListAsAY.push({ id: m.NM, text: m.MonthYear });
		//	});
		//});

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.currentPages = {
			DiscountType: 1,
			ClassWiseSetup: 1,
			FeeItemWise: 1,
			StudentWise: 1,

		};

		$scope.searchData = {
			DiscountType: '',
			ClassWiseSetup: '',
			FeeItemWise: '',
			StudentWise: '',

		};

		$scope.perPage = {
			DiscountType: GlobalServices.getPerPageRow(),
			ClassWiseSetup: GlobalServices.getPerPageRow(),
			FeeItemWise: GlobalServices.getPerPageRow(),
			StudentWise: GlobalServices.getPerPageRow(),

		};

		$scope.newDiscountType = {
			DiscountTypeId: null,
			Name: '',
			Code: '',
			DetailsColl: [],
			IsActive: true,
			Mode: 'Save'
		};
		$scope.AddDiscountTypeDetails(0);

		$scope.FeeItemList = [];
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAllFeeItemList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				var dataColl = res.data.Data;
				angular.forEach(dataColl, function (dc) {

					if (dc.ScholarshipApplicable == true)
						$scope.FeeItemList.push(dc);
				});

			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassSectionList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSectionList = res.data.Data;
			$scope.AllClassList = mx(res.data.Data.ClassList);
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


		$scope.newClassWiseSetup = {
			SelectedClass: null
		};
		$scope.newFeeItemWise = {
			SelectedClass: null
		}
		$scope.newStudentWise = {
			SelectedClass: null
		}
		$scope.GetAllDiscountTypeList();

		$scope.newNotice = {
			FilterStudentOnly: true,
			Title: '',
			Description: ''
		};
	};
	function OnClickDefault() {
		document.getElementById("discount-type-form").style.display = "none";

		document.getElementById("add-discount-type").onclick = function () {
			$scope.ClearDiscountType();
			document.getElementById("discount-type-section").style.display = "none";
			document.getElementById("discount-type-form").style.display = "block";

		};
		document.getElementById("back-discount-type").onclick = function () {
			$scope.ClearDiscountType();
			document.getElementById("discount-type-section").style.display = "block";
			document.getElementById("discount-type-form").style.display = "none";

		};

	}

	//add and del
	$scope.AddDiscountTypeDetails = function (ind) {
		if ($scope.newDiscountType.DetailsColl) {
			var MonthDet = [];
			angular.forEach($scope.MonthList, function (mn) {
				MonthDet.push({
					id: mn.id,
					text: mn.text,
					IsChecked: false
				});
			});

			if ($scope.newDiscountType.DetailsColl.length > ind + 1) {

				$scope.newDiscountType.DetailsColl.splice(ind + 1, 0, {
					FeeItemId: null,
					DiscountPer: 0,
					DiscountAmt: 0,
					MonthDetailsColl: MonthDet
				})
			} else {
				$scope.newDiscountType.DetailsColl.push({
					FeeItemId: null,
					DiscountPer: 0,
					DiscountAmt: 0,
					MonthDetailsColl: MonthDet
				})
			}
		}
	};
	$scope.delDiscountTypepDetails = function (ind) {
		if ($scope.newDiscountType.DetailsColl) {
			if ($scope.newDiscountType.DetailsColl.length > 1) {
				$scope.newDiscountType.DetailsColl.splice(ind, 1);
			}
		}
	};
	$scope.CurMonthDetailsColl = [];
	$scope.ShowMonthSelectionForDiscountType = function (dt) {

		$scope.CurMonthDetailsColl = dt.MonthDetailsColl;
		$('#modal-custom-month').modal('show');
	};

	$scope.CheckedAllMonth = function () {
		angular.forEach($scope.CurMonthDetailsColl, function (md) {
			md.IsChecked = $scope.chkAllMonth;
		});
	};

	$scope.ClearDiscountType = function () {

		$timeout(function () {
			$scope.newDiscountType = {
				DiscountTypeId: null,
				Name: '',
				Code: '',
				DetailsColl: [],
				IsActive: true,
				Mode: 'Save'
			};
			$scope.AddDiscountTypeDetails(0);
			$scope.CurMonthDetailsColl = [];
		});

	};
	$scope.IsValidDiscountType = function () {
		if (!$scope.newDiscountType.Name || $scope.newDiscountType.Name.isEmpty()) {
			Swal.fire('Please ! Enter Discount Type Name');
			return false;
		}
		if ($scope.newDiscountType.DetailsColl) {
			for (let i = 0; i < $scope.newDiscountType.DetailsColl.length; i++) {
				const detail = $scope.newDiscountType.DetailsColl[i];
				if (detail.DiscountPer <= 0) {
					GlobalServices.validateAndFocus('DTDisPer', 'Discount percentage must be greater than zero', false);
					return false;
				}
			}
		}

		return true;
	}

	$scope.SaveUpdateDiscountType = function () {
		if ($scope.IsValidDiscountType() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newDiscountType.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateDiscountType();
					}
				});
			} else
				$scope.CallSaveUpdateDiscountType();

		}
	};

	$scope.CallSaveUpdateDiscountType = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		angular.forEach($scope.newDiscountType.DetailsColl, function (det) {

			if (det.FeeItemId && det.FeeItemId != null && det.FeeItemId > 0) {
				var mIdColl = [];
				angular.forEach(det.MonthDetailsColl, function (md) {
					if (md.IsChecked == true)
						mIdColl.push(md.id);
				});

				det.MonthIdColl = mIdColl;

				if (!det.DiscountAmt || det.DiscountAmt == null)
					det.DiscountAmt = 0;

				if (!det.DiscountPer || det.DiscountPer == null)
					det.DiscountPer = 0;
			}

		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveDiscountType",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newDiscountType }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearDiscountType();
				$scope.GetAllDiscountTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}


	$scope.GetAllDiscountTypeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.DiscountTypeList = [];

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAllDiscountTypeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.DiscountTypeList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetDiscountTypeById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			DiscountTypeId: refData.DiscountTypeId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetDiscountTypeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newDiscountType = res.data.Data;

				angular.forEach($scope.newDiscountType.DetailsColl, function (det) {
					var MonthDet = [];
					angular.forEach($scope.MonthList, function (mn) {
						MonthDet.push({
							id: mn.id,
							text: mn.text,
							IsChecked: false
						});
					});
					det.MonthDetailsColl = MonthDet;

					if (det.MonthIdColl && det.MonthIdColl.length > 0) {

						var mQuery = mx(det.MonthIdColl);
						angular.forEach(det.MonthDetailsColl, function (md) {
							if (mQuery.contains(md.id))
								md.IsChecked = true;
							else
								md.IsChecked = false;
						});

					} else {
						angular.forEach($scope.MonthDetailsColl, function (md) {
							md.IsChecked = false;
						});
					}
				});

				if (!$scope.newDiscountType.DetailsColl || $scope.newDiscountType.DetailsColl.length == 0)
					$scope.AddDiscountTypeDetails(0);

				$scope.newDiscountType.Mode = 'Modify';

				document.getElementById("discount-type-section").style.display = "none";
				document.getElementById("discount-type-form").style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelDiscountTypeById = function (refData) {

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
					DiscountTypeId: refData.DiscountTypeId
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelDiscountType",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllDiscountTypeList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.GetStudentLstForClassWiseSetup = function (semYear) {
		$scope.newClassWiseSetup.StudentColl = [];

		if ($scope.newClassWiseSetup.SelectedClass && semYear == true) {
			var findClass = $scope.AllClassList.firstOrDefault(p1 => p1.ClassId == $scope.newClassWiseSetup.SelectedClass.ClassId);
			if (findClass) {

				$scope.newClassWiseSetup.SelectedClass.ClassType = findClass.ClassType;

				var semQry = mx(findClass.ClassSemesterIdColl);
				var cyQry = mx(findClass.ClassYearIdColl);

				$scope.newClassWiseSetup.SelectedClassClassYearList = [];
				$scope.newClassWiseSetup.SelectedClassSemesterList = [];

				angular.forEach($scope.SemesterList, function (sem) {
					if (semQry.contains(sem.id)) {
						$scope.newClassWiseSetup.SelectedClassSemesterList.push({
							id: sem.id,
							text: sem.text,
							SemesterId: sem.id,
							Name: sem.Name
						});
					}
				});

				angular.forEach($scope.ClassYearList, function (sem) {
					if (cyQry.contains(sem.id)) {
						$scope.newClassWiseSetup.SelectedClassClassYearList.push({
							id: sem.id,
							text: sem.text,
							ClassYearId: sem.id,
							Name: sem.Name
						});
					}
				});
			}
		}

		if ($scope.newClassWiseSetup.SelectedClass) {
			var para = {
				ClassId: $scope.newClassWiseSetup.SelectedClass.ClassId,
				SectionId: $scope.newClassWiseSetup.SelectedClass.SectionId,
				All: ($scope.FeeConfig.ShowLeftStudentInDiscountSetup ? true : false),
				SemesterId: $scope.newClassWiseSetup.SemesterId,
				ClassYearId: $scope.newClassWiseSetup.ClassYearId
			};

			var paraP = {
				ClassId: $scope.newClassWiseSetup.SelectedClass.ClassId,
				SectionId: $scope.newClassWiseSetup.SelectedClass.SectionId,
				SemesterId: $scope.newClassWiseSetup.SemesterId,
				ClassYearId: $scope.newClassWiseSetup.ClassYearId
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
					$scope.newClassWiseSetup.StudentColl = res.data.Data;

					$http({
						method: 'POST',
						url: base_url + "Fee/Creation/GetAllClassWiseDiscountSetup",
						dataType: "json",
						data: JSON.stringify(paraP)
					}).then(function (res1) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						if (res1.data.IsSuccess && res1.data.Data) {
							var dataColl = mx(res1.data.Data);

							angular.forEach($scope.newClassWiseSetup.StudentColl, function (st) {
								st.DiscountDetailsColl = [];

								st.Remarks = '';

								var fSt = dataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);

								if (fSt)
									st.Remarks = fSt ? fSt.Remarks : '';

								angular.forEach($scope.DiscountTypeList, function (dt) {
									if (dt.IsActive == true) {
										var find = dataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId && p1.DiscountTypeId == dt.DiscountTypeId);

										st.DiscountDetailsColl.push({
											DiscountTypeId: dt.DiscountTypeId,
											Include: find ? true : false
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



				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}
	}

	$scope.SaveUpdateClassWiseSetup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var cid = $scope.newClassWiseSetup.SelectedClass.ClassId;
		var sid = $scope.newClassWiseSetup.SelectedClass.SectionId;
		var semId = $scope.newClassWiseSetup.SemesterId;
		var classYearId = $scope.newClassWiseSetup.ClassYearId;
		var dataColl = [];
		angular.forEach($scope.newClassWiseSetup.StudentColl, function (det) {

			angular.forEach(det.DiscountDetailsColl, function (md) {
				if (md.Include == true) {
					dataColl.push({
						ClassId: cid,
						SectionId: sid,
						StudentId: det.StudentId,
						SemesterId: det.SemesterId,
						ClassYearId: det.ClassYearId,
						DiscountTypeId: md.DiscountTypeId,
						Remarks: det.Remarks
					});
				}
			});
		});

		if (dataColl.length > 0) {
			$http({
				method: 'POST',
				url: base_url + "Fee/Creation/SaveClassWiseDiscountSetup",
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

				if (res.data.IsSuccess == true) {
					$scope.ClearDiscountType();
					$scope.GetAllDiscountTypeList();
				}

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});
		} else {
			Swal.fire('No Data Found For Save');
		}

	}
	$scope.DelClassWiseSetup = function () {
		var cid = $scope.newClassWiseSetup.SelectedClass.ClassId;
		var sid = $scope.newClassWiseSetup.SelectedClass.SectionId;
		var semId = $scope.newClassWiseSetup.SemesterId;
		var classYearId = $scope.newClassWiseSetup.ClassYearId;
		Swal.fire({
			title: 'Do you want to delete the current data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ClassId: cid,
					SectionId: sid,
					SemesterId: semId,
					ClassYearId: classYearId
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelClassWiseDiscountSetup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetStudentLstForClassWiseSetup();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};
	$scope.CurMonthDetailsColl = [];
	$scope.ShowMonthSelectionForFixedAmt = function (dt) {

		$scope.CurMonthDetailsColl = dt.MonthDetailsColl;
		$('#modal-custom-month-fee1').modal('show');
	};

	$scope.CheckedAll = function () {
		angular.forEach($scope.CurMonthDetailsColl, function (md) {
			md.Include = $scope.chkAll;
		});
	};
	$scope.CheckedAllFeeItemWiseST = function (st) {
		if (st.chkAll == true) {
			angular.forEach(st.MonthDetailsColl, function (md) {
				md.Include = st.chkAll;
				md.DiscountAmt = st.DiscountAmt;
				md.DiscountPer = st.DiscountPer;
			});
		}

	};
	$scope.GetStudentLstForFeeItemWise = function (semYear) {
		$scope.newFeeItemWise.StudentColl = [];
		$scope.CurMonthDetailsColl = [];

		if ($scope.newFeeItemWise.SelectedClass && semYear == true) {
			var findClass = $scope.AllClassList.firstOrDefault(p1 => p1.ClassId == $scope.newFeeItemWise.SelectedClass.ClassId);
			if (findClass) {

				$scope.newFeeItemWise.SelectedClass.ClassType = findClass.ClassType;

				var semQry = mx(findClass.ClassSemesterIdColl);
				var cyQry = mx(findClass.ClassYearIdColl);

				$scope.newFeeItemWise.SelectedClassClassYearList = [];
				$scope.newFeeItemWise.SelectedClassSemesterList = [];

				angular.forEach($scope.SemesterList, function (sem) {
					if (semQry.contains(sem.id)) {
						$scope.newFeeItemWise.SelectedClassSemesterList.push({
							id: sem.id,
							text: sem.text,
							SemesterId: sem.id,
							Name: sem.Name
						});
					}
				});

				angular.forEach($scope.ClassYearList, function (sem) {
					if (cyQry.contains(sem.id)) {
						$scope.newFeeItemWise.SelectedClassClassYearList.push({
							id: sem.id,
							text: sem.text,
							ClassYearId: sem.id,
							Name: sem.Name
						});
					}
				});
			}
		}

		if ($scope.newFeeItemWise.SelectedClass) {
			var para = {
				ClassId: $scope.newFeeItemWise.SelectedClass.ClassId,
				SectionId: $scope.newFeeItemWise.SelectedClass.SectionId,
				All: ($scope.FeeConfig.ShowLeftStudentInDiscountSetup ? true : false),
				SemesterId: $scope.newFeeItemWise.SemesterId,
				ClassYearId: $scope.newFeeItemWise.ClassYearId,
				BatchId: $scope.newFeeItemWise.BatchId,
			};

			var paraP = {
				ClassId: $scope.newFeeItemWise.SelectedClass.ClassId,
				SectionId: $scope.newFeeItemWise.SelectedClass.SectionId,
				SemesterId: $scope.newFeeItemWise.SemesterId,
				ClassYearId: $scope.newFeeItemWise.ClassYearId,
				BatchId: $scope.newFeeItemWise.BatchId,
			};


			$scope.loadingstatus = "running";
			showPleaseWait();

			var MonthList_FW = [];
			GlobalServices.getAcademicMonthList(null, $scope.newFeeItemWise.SelectedClass.ClassId).then(function (resAM)
			{
				angular.forEach(resAM.data.Data, function (m) {
					MonthList_FW.push({ id: m.NM, text: m.MonthYear });
				});

				$http({
					method: 'POST',
					url: base_url + "Academic/Transaction/GetClassWiseStudentForLeft",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess && res.data.Data) {
						$scope.newFeeItemWise.StudentColl = res.data.Data;

						if ($scope.newFeeItemWise.SelectedFeeItem) {
							var para1 = {
								ClassId: para.ClassId,
								SectionId: para.SectionId,
								SemesterId: para.SemesterId,
								ClassYearId: para.ClassYearId,
								FeeItemId: $scope.newFeeItemWise.SelectedFeeItem.FeeItemId,
								BatchId: $scope.newFeeItemWise.BatchId,
							}

							$http({
								method: 'POST',
								url: base_url + "Fee/Creation/GetAllFeeItemWiseDiscountSetup",
								dataType: "json",
								data: JSON.stringify(para1)
							}).then(function (res1) {
								hidePleaseWait();
								$scope.loadingstatus = "stop";
								if (res1.data.IsSuccess && res1.data.Data) {
									var dataColl = mx(res1.data.Data);

									angular.forEach($scope.newFeeItemWise.StudentColl, function (st) {

										var find = dataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
										st.Amount = find ? find.Amount : 0;
										st.Remarks = find ? find.Remarks : '';
										st.MonthIdColl = find ? find.MonthIdColl : [];
										st.ClassId = para.ClassId;
										st.SectionId = para.SectionId;
										st.DiscountPer = find ? find.DiscountPer : 0;
										st.DiscountAmt = find ? find.DiscountAmt : 0;
										st.MonthDetailsColl = [];

										angular.forEach(MonthList_FW, function (mn) {
											st.MonthDetailsColl.push({
												id: mn.id,
												text: mn.text,
												DiscountPer: 0,
												DiscountAmt: 0
											});
										});

										var mcoll = mx(st.MonthIdColl);

										angular.forEach(st.MonthDetailsColl, function (md) {

											var find = mcoll.firstOrDefault(p1 => p1.MonthId == md.id);
											md.DiscountAmt = find ? find.DiscountAmt : 0;
											md.DiscountPer = find ? find.DiscountPer : 0;
										});
									});


								} else {
									Swal.fire(res.data.ResponseMSG);
								}

							}, function (reason) {
								Swal.fire('Failed' + reason);
							});
						}


					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			});

		

		}
	}

	$scope.SaveUpdateFeeItemWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataColl = [];
		var cId = $scope.newFeeItemWise.SelectedClass.ClassId;
		var sId = $scope.newFeeItemWise.SelectedClass.SectionId;
		var semId = $scope.newFeeItemWise.SemesterId;
		var classYearId = $scope.newFeeItemWise.ClassYearId;
		angular.forEach($scope.newFeeItemWise.StudentColl, function (fm) {
			var beData = {
				ClassId: cId,
				SectionId: sId,
				FeeItemId: $scope.newFeeItemWise.SelectedFeeItem.FeeItemId,
				StudentId: fm.StudentId,
				SemesterId: fm.SemesterId,
				ClassYearId: fm.ClassYearId,
				BatchId: fm.BatchId,
				DiscountPer: (fm.DiscountPer ? fm.DiscountPer : 0),
				DiscountAmt: (fm.DiscountAmt ? fm.DiscountAmt : 0),
				Remarks: fm.Remarks,
				MonthIdColl: []
			};
			angular.forEach(fm.MonthDetailsColl, function (md) {
				if (md.DiscountAmt > 0 || md.DiscountPer > 0) {
					var det = {
						MonthId: md.id,
						DiscountPer: (md.DiscountPer ? md.DiscountPer : 0),
						DiscountAmt: (md.DiscountAmt ? md.DiscountAmt : 0)
					};
					beData.MonthIdColl.push(det);
				}
			});

			if (beData.DiscountAmt > 0 || beData.DiscountPer > 0 || beData.MonthIdColl.length > 0)
				dataColl.push(beData);

		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveFeeItemWiseDiscountSetup",
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
	$scope.DelFeeItemWise = function () {
		var cid = $scope.newFeeItemWise.SelectedClass.ClassId;
		var sid = $scope.newFeeItemWise.SelectedClass.SectionId;
		var fid = $scope.newFeeItemWise.SelectedFeeItem.FeeItemId;
		var semId = $scope.newFeeItemWise.SemesterId;
		var classYearId = $scope.newFeeItemWise.ClassYearId;
		Swal.fire({
			title: 'Do you want to delete the current data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ClassId: cid,
					SectionId: sid,
					SemesterId: semId,
					ClassYearId: classYearId,
					FeeItemId: fid
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelFeeItemWiseDiscountSetup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetStudentLstForFeeItemWise();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.GetStudentLstForFullSetup = function (semYear) {
		$scope.newFullSetup.StudentColl = [];

		if ($scope.newFullSetup.SelectedClass && semYear == true) {
			var findClass = $scope.AllClassList.firstOrDefault(p1 => p1.ClassId == $scope.newFullSetup.SelectedClass.ClassId);
			if (findClass) {

				$scope.newFullSetup.SelectedClass.ClassType = findClass.ClassType;

				var semQry = mx(findClass.ClassSemesterIdColl);
				var cyQry = mx(findClass.ClassYearIdColl);

				$scope.newFullSetup.SelectedClassClassYearList = [];
				$scope.newFullSetup.SelectedClassSemesterList = [];

				angular.forEach($scope.SemesterList, function (sem) {
					if (semQry.contains(sem.id)) {
						$scope.newFullSetup.SelectedClassSemesterList.push({
							id: sem.id,
							text: sem.text,
							SemesterId: sem.id,
							Name: sem.Name
						});
					}
				});

				angular.forEach($scope.ClassYearList, function (sem) {
					if (cyQry.contains(sem.id)) {
						$scope.newFullSetup.SelectedClassClassYearList.push({
							id: sem.id,
							text: sem.text,
							ClassYearId: sem.id,
							Name: sem.Name
						});
					}
				});
			}
		}


		if ($scope.newFullSetup.SelectedClass) {
			var para = {
				ClassId: $scope.newFullSetup.SelectedClass.ClassId,
				SectionId: $scope.newFullSetup.SelectedClass.SectionId,
				All: ($scope.FeeConfig.ShowLeftStudentInDiscountSetup ? true : false),
				SemesterId: $scope.newFullSetup.SemesterId,
				ClassYearId: $scope.newFullSetup.ClassYearId
			};

			var paraP = {
				ClassId: $scope.newFullSetup.SelectedClass.ClassId,
				SectionId: $scope.newFullSetup.SelectedClass.SectionId,
				SemesterId: $scope.newFullSetup.SemesterId,
				ClassYearId: $scope.newFullSetup.ClassYearId
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
					$scope.newFullSetup.StudentColl = res.data.Data;

					$http({
						method: 'POST',
						url: base_url + "Fee/Creation/GetAllFullDiscountSetup",
						dataType: "json",
						data: JSON.stringify(paraP)
					}).then(function (res1) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						if (res1.data.IsSuccess && res1.data.Data) {
							var dataColl = mx(res1.data.Data);

							angular.forEach($scope.newFullSetup.StudentColl, function (st) {
								var find = dataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
								st.Remarks = find ? find.Remarks : '';
								st.Include = find ? true : false;
							});


						} else {
							Swal.fire(res.data.ResponseMSG);
						}

					}, function (reason) {
						Swal.fire('Failed' + reason);
					});



				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}
	}

	$scope.SaveUpdateFullSetup = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var cid = $scope.newFullSetup.SelectedClass.ClassId;
		var sid = $scope.newFullSetup.SelectedClass.SectionId;
		var semId = $scope.newFullSetup.SemesterId;
		var classYearId = $scope.newFullSetup.ClassYearId;
		var dataColl = [];
		angular.forEach($scope.newFullSetup.StudentColl, function (det) {

			if (det.Include == true) {
				dataColl.push({
					ClassId: cid,
					SectionId: sid,
					StudentId: det.StudentId,
					SemesterId: det.SemesterId,
					ClassYearId: det.ClassYearId,
					Remarks: det.Remarks
				});
			}
		});

		if (dataColl.length == 0) {
			dataColl.push({
				ClassId: cid,
				SectionId: sid,
				StudentId: 0,
				Remarks: ''
			});
		}
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveFullDiscountSetup",
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

			if (res.data.IsSuccess == true) {
				$scope.ClearDiscountType();
				$scope.GetAllDiscountTypeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.DelFullSetup = function () {
		var cid = $scope.newFullSetup.SelectedClass.ClassId;
		var sid = $scope.newFullSetup.SelectedClass.SectionId;
		var semId = $scope.newClassWiseSetup.SemesterId;
		var classYearId = $scope.newClassWiseSetup.ClassYearId;
		Swal.fire({
			title: 'Do you want to delete the current data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					ClassId: cid,
					SectionId: sid,
					SemesterId: semId,
					ClassYearId: classYearId
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelFullDiscountSetup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetStudentLstForFullSetup();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});

	};
	getterAndSetter();

	function getterAndSetter() {


		$scope.gridOptions = [];

		$scope.gridOptions = {
			showGridFooter: true,
			showColumnFooter: false,
			useExternalPagination: false,
			useExternalSorting: false,
			enableFiltering: true,
			enableSorting: true,
			enableRowSelection: true,
			enableSelectAll: true,
			enableGridMenu: true,

			columnDefs: [

				{ name: "RegNo", displayName: "Regd.No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Name", displayName: "Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RollNo", displayName: "Roll No", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassSec", displayName: "Class/Sec", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Semester", displayName: "Semester", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "ClassYear", displayName: "Class Year", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "FatherName", displayName: "Father Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "F_ContactNo", displayName: "Father Contact", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Address", displayName: "Address", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "DiscountType", displayName: "Discount Type", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Remarks", displayName: "Remarks", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Details", displayName: "Details", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TranType", displayName: "TranType", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Cast", displayName: "Cast", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TransportPoint", displayName: "Transport Point", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "TransportRoute", displayName: "Boarders Type", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "RoomName", displayName: "House Name", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "IsLeft", displayName: "Is Left", minWidth: 140, headerCellClass: 'headerAligment' },
				{ name: "Batch", displayName: "Batch", minWidth: 140, headerCellClass: 'headerAligment' },

			],
			//   rowTemplate: rowTemplate(),
			exporterCsvFilename: 'enqSummary.csv',
			exporterPdfDefaultStyle: { fontSize: 9 },
			exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
			exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
			exporterPdfHeader: { text: "Dynamic Technosoft Pvt. Ltd. <br> Birgunj Nepal", style: 'headerStyle' },
			exporterPdfFooter: function (currentPage, pageCount) {
				return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
			},
			exporterPdfCustomFormatter: function (docDefinition) {
				docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
				docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
				return docDefinition;
			},
			exporterPdfOrientation: 'portrait',
			exporterPdfPageSize: 'LETTER',
			exporterPdfMaxGridWidth: 500,
			exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
			exporterExcelFilename: 'enqSummary.xlsx',
			exporterExcelSheetName: 'enqSummary',
			onRegisterApi: function (gridApi) {
				$scope.gridApi = gridApi;
			}
		};
	};

	$scope.GetDiscountStudent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetDiscountStudentList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.gridOptions.data = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
	$scope.PrintDiscountStudent = function () {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityDiscountStudent + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				var templatesColl = res.data.Data;
				if (templatesColl && templatesColl.length > 0) {
					var templatesName = [];
					var sno = 1;
					angular.forEach(templatesColl, function (tc) {
						templatesName.push(sno + '-' + tc.ReportName);
						sno++;
					});

					var print = false;

					var rptTranId = 0;
					if (templatesColl.length == 1)
						rptTranId = templatesColl[0].RptTranId;
					else {
						Swal.fire({
							title: 'Report Templates For Print',
							input: 'select',
							inputOptions: templatesName,
							inputPlaceholder: 'Select a template',
							showCancelButton: true,
							inputValidator: (value) => {
								return new Promise((resolve) => {
									if (value >= 0) {
										resolve()
										rptTranId = templatesColl[value].RptTranId;

										if (rptTranId > 0) {
											var dataColl = [];
											angular.forEach($scope.gridApi.core.getVisibleRows($scope.gridApi.grid), function (fr) {
												dataColl.push(fr.entity);
											});
											print = true;
											$http({
												method: 'POST',
												url: base_url + "Fee/Creation/PrintDiscountStudentList",
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
												if (res.data.IsSuccess && res.data.Data) {

													document.body.style.cursor = 'wait';
													document.getElementById("frmRpt").src = '';
													document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityDiscountStudent + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
													document.body.style.cursor = 'default';
													$('#FrmPrintReport').modal('show');

												} else
													Swal.fire('No Templates found for print');

											}, function (errormessage) {
												hidePleaseWait();
												$scope.loadingstatus = "stop";
												Swal.fire(errormessage);
											});

										}

									} else {
										resolve('You need to select:)')
									}
								})
							}
						})
					}

					if (rptTranId > 0 && print == false) {
						var dataColl = [];
						angular.forEach($scope.gridApi.core.getVisibleRows($scope.gridApi.grid), function (fr) {
							dataColl.push(fr.entity);
						});
						print = true;

						$http({
							method: 'POST',
							url: base_url + "Fee/Creation/PrintDiscountStudentList",
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
							if (res.data.IsSuccess && res.data.Data) {

								document.body.style.cursor = 'wait';
								document.getElementById("frmRpt").src = '';
								document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=false&entityid=" + entityDiscountStudent + "&voucherid=0&tranid=0&vouchertype=0&sessionid=" + res.data.Data.ResponseId;
								document.body.style.cursor = 'default';
								$('#FrmPrintReport').modal('show');

							} else
								Swal.fire('No Templates found for print');

						}, function (errormessage) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							Swal.fire(errormessage);
						});

					}

				} else
					Swal.fire('No Templates found for print');
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.SendSMSToStudent = function () {
		Swal.fire({
			title: 'Do you want to Send SMS To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				var para1 = {
					EntityId: entityDiscountStudentForSMS,
					ForATS: 3,
					TemplateType: 1
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/GetSENT",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						var templatesColl = res.data.Data;
						if (templatesColl && templatesColl.length > 0) {
							var templatesName = [];
							var sno = 1;
							angular.forEach(templatesColl, function (tc) {
								templatesName.push(sno + '-' + tc.Name);
								sno++;
							});

							var print = false;

							var rptTranId = 0;
							var selectedTemplate = null;
							if (templatesColl.length == 1) {
								rptTranId = templatesColl[0].TranId;
								selectedTemplate = templatesColl[0];
							}
							else {
								Swal.fire({
									title: 'Templates For SMS',
									input: 'select',
									inputOptions: templatesName,
									inputPlaceholder: 'Select a template',
									showCancelButton: true,
									inputValidator: (value) => {
										return new Promise((resolve) => {
											if (value >= 0) {
												resolve()
												rptTranId = templatesColl[value].TranId;
												selectedTemplate = templatesColl[value];

												if (rptTranId > 0) {
													var dataColl = [];
													angular.forEach($scope.gridApi.grid.getVisibleRows(), function (node) {
														var objEntity = node.entity;
														var tmpContactNo = '';
														if (!objEntity.F_ContactNo)
															tmpContactNo = objEntity.ContactNo;
														else
															tmpContactNo = objEntity.F_ContactNo;

														if (tmpContactNo && tmpContactNo.length > 0) {
															var msg = selectedTemplate.Description;
															for (let x in objEntity) {
																var variable = '$$' + x.toLowerCase() + '$$';
																if (msg.indexOf(variable) >= 0) {
																	var val = objEntity[x];
																	msg = msg.replace(variable, val);
																}

																if (msg.indexOf('$$') == -1)
																	break;
															}

															var newSMS = {
																EntityId: entityDiscountStudentForSMS,
																StudentId: objEntity.StudentId,
																UserId: objEntity.UserId,
																Title: selectedTemplate.Title,
																Message: msg,
																ContactNo: tmpContactNo,
																StudentName: objEntity.Name
															};

															dataColl.push(newSMS);
														}
													});

													print = true;

													$http({
														method: 'POST',
														url: base_url + "Global/SendSMSToStudent",
														dataType: "json",
														data: JSON.stringify(dataColl)
													}).then(function (sRes) {
														Swal.fire(sRes.data.ResponseMSG);
														if (sRes.data.IsSuccess && sRes.data.Data) {

														}
													});

												}

											} else {
												resolve('You need to select:)')
											}
										})
									}
								})
							}

							if (rptTranId > 0 && print == false) {
								var dataColl = [];

								angular.forEach($scope.gridApi.grid.getVisibleRows(), function (node) {
									var objEntity = node.entity;
									var tmpContactNo = '';
									if (!objEntity.F_ContactNo)
										tmpContactNo = objEntity.ContactNo;
									else
										tmpContactNo = objEntity.F_ContactNo;

									if (tmpContactNo && tmpContactNo.length > 0) {
										var msg = selectedTemplate.Description;
										for (let x in objEntity) {
											var variable = '$$' + x.toLowerCase() + '$$';
											if (msg.indexOf(variable) >= 0) {
												var val = objEntity[x];
												msg = msg.replace(variable, val);
											}

											if (msg.indexOf('$$') == -1)
												break;
										}

										var newSMS = {
											EntityId: entityDiscountStudentForSMS,
											StudentId: objEntity.StudentId,
											UserId: objEntity.UserId,
											Title: selectedTemplate.Title,
											Message: msg,
											ContactNo: tmpContactNo,
											StudentName: objEntity.Name
										};

										dataColl.push(newSMS);
									}
								});
								print = true;

								$http({
									method: 'POST',
									url: base_url + "Global/SendSMSToStudent",
									dataType: "json",
									data: JSON.stringify(dataColl)
								}).then(function (sRes) {
									Swal.fire(sRes.data.ResponseMSG);
									if (sRes.data.IsSuccess && sRes.data.Data) {

									}
								});

							}

						} else
							Swal.fire('No Templates found for SMS');
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};
	$scope.SendNoticeToStudent = function () {
		Swal.fire({
			title: 'Do you want to Send Notification To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				var para1 = {
					EntityId: entityDiscountStudentForSMS,
					ForATS: 3,
					TemplateType: 3
				};

				$http({
					method: 'POST',
					url: base_url + "Setup/Security/GetSENT",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res) {
					if (res.data.IsSuccess && res.data.Data) {
						var templatesColl = res.data.Data;
						if (templatesColl && templatesColl.length > 0) {
							var templatesName = [];
							var sno = 1;
							angular.forEach(templatesColl, function (tc) {
								templatesName.push(sno + '-' + tc.Name);
								sno++;
							});

							var print = false;

							var rptTranId = 0;
							var selectedTemplate = null;
							if (templatesColl.length == 1) {
								rptTranId = templatesColl[0].TranId;
								selectedTemplate = templatesColl[0];
							}
							else {
								Swal.fire({
									title: 'Templates For Notification',
									input: 'select',
									inputOptions: templatesName,
									inputPlaceholder: 'Select a template',
									showCancelButton: true,
									inputValidator: (value) => {
										return new Promise((resolve) => {
											if (value >= 0) {
												resolve()
												rptTranId = templatesColl[value].TranId;
												selectedTemplate = templatesColl[value];

												if (rptTranId > 0) {
													$scope.newNotice.Title = selectedTemplate.Title;
													$scope.newNotice.Description = selectedTemplate.Description;
													$('#modal-xl').modal('show');
												}

											} else {
												resolve('You need to select:)')
											}
										})
									}
								})
							}

							if (rptTranId > 0 && print == false) {
								$scope.newNotice.Title = selectedTemplate.Title;
								$scope.newNotice.Description = selectedTemplate.Description;
								$('#modal-xl').modal('show');
							}

						} else {
							$scope.newNotice.Title = '';
							$scope.newNotice.Description = '';
							$('#modal-xl').modal('show');
						}

					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			}
		});


	};

	$scope.SendManualNoticeToStudent = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var dataColl = [];
		angular.forEach($scope.gridApi.grid.getVisibleRows(), function (node) {
			var objEntity = node.entity;
			var msg = $scope.newNotice.Description;
			for (let x in objEntity) {
				var variable = '$$' + x.toLowerCase() + '$$';
				if (msg.indexOf(variable) >= 0) {
					var val = objEntity[x];
					msg = msg.replace(variable, val);
				}

				if (msg.indexOf('$$') == -1)
					break;
			}

			var newSMS = {
				EntityId: entityDiscountStudentForSMS,
				StudentId: objEntity.StudentId,
				UserId: objEntity.UserId,
				Title: $scope.newNotice.Title,
				Message: msg,
				ContactNo: objEntity.F_ContactNo,
				StudentName: objEntity.Name
			};

			dataColl.push(newSMS);
		});
		$http({
			method: 'POST',
			url: base_url + "Global/SendNotificationToStudent",
			dataType: "json",
			data: JSON.stringify(dataColl)
		}).then(function (sRes) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			Swal.fire(sRes.data.ResponseMSG);
			if (sRes.data.IsSuccess && sRes.data.Data) {

			}
		});

	};


	$scope.GetStudentLstForStudentWise = function (semYear) {
		$scope.newStudentWise.StudentColl = [];
		$scope.StudentCurMonthDetailsColl = [];

		if ($scope.newStudentWise.SelectedClass && semYear == true) {
			var findClass = $scope.AllClassList.firstOrDefault(p1 => p1.ClassId == $scope.newStudentWise.SelectedClass.ClassId);
			if (findClass) {

				$scope.newStudentWise.SelectedClass.ClassType = findClass.ClassType;

				var semQry = mx(findClass.ClassSemesterIdColl);
				var cyQry = mx(findClass.ClassYearIdColl);

				$scope.newStudentWise.SelectedClassClassYearList = [];
				$scope.newStudentWise.SelectedClassSemesterList = [];

				angular.forEach($scope.SemesterList, function (sem) {
					if (semQry.contains(sem.id)) {
						$scope.newStudentWise.SelectedClassSemesterList.push({
							id: sem.id,
							text: sem.text,
							SemesterId: sem.id,
							Name: sem.Name
						});
					}
				});

				angular.forEach($scope.ClassYearList, function (sem) {
					if (cyQry.contains(sem.id)) {
						$scope.newStudentWise.SelectedClassClassYearList.push({
							id: sem.id,
							text: sem.text,
							ClassYearId: sem.id,
							Name: sem.Name
						});
					}
				});
			}
		}

		if ($scope.newStudentWise.SelectedClass) {
			var para = {
				ClassId: $scope.newStudentWise.SelectedClass.ClassId,
				SectionId: $scope.newStudentWise.SelectedClass.SectionId,
				All: ($scope.FeeConfig.ShowLeftStudentInDiscountSetup ? true : false),
				SemesterId: $scope.newStudentWise.SemesterId,
				ClassYearId: $scope.newStudentWise.ClassYearId,
				BatchId:$scope.newStudentWise.BatchId,
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
					$scope.newStudentWise.StudentColl = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}
	}

	$scope.GetStudentWiseDiscountSetup = function () {

		if ($scope.newStudentWise.StudentId) {

			var semId = $scope.newStudentWise.SemesterId;
			var clId = $scope.newStudentWise.ClassYearId;

			var para = {
				StudentId: $scope.newStudentWise.StudentId,
				SemesterId: semId,
				ClassYearId: clId,
				BatchId:$scope.newStudentWise.BatchId,
			};

			$scope.loadingstatus = "running";
			showPleaseWait();

			var MonthList_SW = [];
			GlobalServices.getAcademicMonthList($scope.newStudentWise.StudentId, null).then(function (resAM) {				
				angular.forEach(resAM.data.Data, function (m) {
					MonthList_SW.push({ id: m.NM, text: m.MonthYear });
				});

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/GetStudentWiseDiscountSetup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess && res.data.Data) {
						$scope.newStudentWise.FeeItemColl = res.data.Data;
						 
						angular.forEach($scope.newStudentWise.FeeItemColl, function (fi) {

							fi.MonthDetailsColl = [];
							var montQry = mx(fi.MonthIdColl);
							MonthList_SW.forEach(function (m) {
								var findM = montQry.firstOrDefault(p1 => p1.MonthId == m.id);
								var mDet = {
									id: m.id,
									MonthId: m.id,
									DiscountPer: findM ? findM.DiscountPer : 0,
									DiscountAmt: findM ? findM.DiscountAmt : 0,
									text: m.text,
								};
								fi.MonthDetailsColl.push(mDet);
							});
							 
						});

						if (!$scope.newStudentWise.FeeItemColl || $scope.newStudentWise.FeeItemColl.length == 0) {
							$scope.AddStudentWiseDisDet(0);
						}

					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});


			});

		

		}
	}

	/*Add and Delete Button*/
	$scope.AddStudentWiseDisDet = function (ind) {
		if ($scope.newStudentWise.FeeItemColl) {

			var MonthDet = [];
			angular.forEach($scope.MonthList, function (mn) {
				MonthDet.push({
					id: mn.id,
					text: mn.text,
					IsChecked: false
				});
			});

			if ($scope.newStudentWise.FeeItemColl.length > ind + 1) {
				$scope.newStudentWise.FeeItemColl.splice(ind + 1, 0, {
					FeeItemId: null,
					Qty: 0,
					Rate: 0,
					DiscountPer: 0,
					DiscountAmt: 0,
					PayableAmt: 0,
					MonthDetailsColl: MonthDet
				})
			} else {
				$scope.newStudentWise.FeeItemColl.push({
					FeeItemId: null,
					Qty: 0,
					Rate: 0,
					DiscountPer: 0,
					DiscountAmt: 0,
					PayableAmt: 0,
					MonthDetailsColl: MonthDet
				})
			}
		}
	};
	$scope.delStudentWiseDisDet = function (ind) {
		if ($scope.newStudentWise.FeeItemColl) {
			if ($scope.newStudentWise.FeeItemColl.length > 1) {
				$scope.newStudentWise.FeeItemColl.splice(ind, 1);
			}
		}
	};

	$scope.StudentCurMonthDetailsColl = [];
	$scope.ShowMonthSelectionForStudentWise = function (dt) {

		$scope.StudentCurMonthDetailsColl = dt.MonthDetailsColl;
		$('#modal-custom-month-studentWise').modal('show');
	};

	$scope.SaveUpdateStudentWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataColl = [];
		var sId = $scope.newStudentWise.StudentId;
		var semId = $scope.newStudentWise.SemesterId;
		var clId = $scope.newStudentWise.ClassYearId;

		angular.forEach($scope.newStudentWise.FeeItemColl, function (fm) {

			if (fm.FeeItemId > 0) {
				var beData = {
					ClassId: 0,
					SectionId: 0,
					StudentId: sId,
					FeeItemId: fm.FeeItemId,
					DiscountPer: (fm.DiscountPer ? fm.DiscountPer : 0),
					DiscountAmt: (fm.DiscountAmt ? fm.DiscountAmt : 0),
					Remarks: fm.Remarks,
					SemesterId: semId,
					ClassYearId: clId,
					MonthIdColl: []
				};
				angular.forEach(fm.MonthDetailsColl, function (md) {
					if (md.DiscountAmt > 0 || md.DiscountPer > 0) {
						var det = {
							MonthId: md.id,
							DiscountPer: (md.DiscountPer ? md.DiscountPer : 0),
							DiscountAmt: (md.DiscountAmt ? md.DiscountAmt : 0)
						};
						beData.MonthIdColl.push(det);
					}
				});

				if (beData.DiscountAmt > 0 || beData.DiscountPer > 0 || beData.MonthIdColl.length > 0)
					dataColl.push(beData);
			}

		});

		if (dataColl.length > 0) {
			$http({
				method: 'POST',
				url: base_url + "Fee/Creation/SaveStudentWiseDiscountSetup",
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
		} else {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
		}

	}
	$scope.DelStudentWise = function () {
		var sid = $scope.newStudentWise.StudentId;
		var semId = $scope.newStudentWise.SemesterId;
		var clId = $scope.newStudentWise.ClassYearId;
		Swal.fire({
			title: 'Do you want to delete the current data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var para = {
					StudentId: sid,
					SemesterId: semId,
					ClassYearId: clId,
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelStudentWiseDiscountSetup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetStudentWiseDiscountSetup();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//Added By Suresh for column sorting
	$scope.sortClasswise = function (keyname) {
		$scope.sortKeyclasswise = keyname;
		$scope.reverse1 = !$scope.reverse1;
	}

	$scope.sortFeewise = function (keyname) {
		$scope.sortKeyfeewise = keyname;
		$scope.reverse2 = !$scope.reverse2;
	}

	$scope.sortscholarshipwise = function (keyname) {
		$scope.sortKeyscholarshipwise = keyname;
		$scope.reverse3 = !$scope.reverse3;
	}

	$scope.ChnageMaxminDis = function (beData) {
		if (beData.DiscountPer > 100)
			beData.DiscountPer = 100;
		if (beData.DiscountPer < 0)
			beData.DiscountPer = 0;
    }

});