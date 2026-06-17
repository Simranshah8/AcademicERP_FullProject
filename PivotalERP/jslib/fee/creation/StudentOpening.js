app.controller('StudentOpeningController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Student Opening';
	OnClickDefault();
	function OnClickDefault() {
		document.getElementById('report-detail-section').style.display = "none";

		//document.getElementById('detail-btn').onclick = function () {
		//	document.getElementById('report-section').style.display = "none";
		//	document.getElementById('report-detail-section').style.display = "block";
		//}
		document.getElementById('back-btn-report').onclick = function () {
			document.getElementById('report-detail-section').style.display = "none";
			document.getElementById('report-section').style.display = "block";
		}

	}


	$scope.LoadData = function () {
		$('.select2').select2();

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();

		$scope.currentPages = {
			Report: 1,
			ClassWise:1,
		};

		$scope.perPage = {
			Report: GlobalServices.getPerPageRow(),
			ClassWise: GlobalServices.getPerPageRow(),

		};

		$scope.newReport = {
			ReportId: null,
			ReportDetailsColl: []
		};
		$scope.newReport.ReportDetailsColl.push({});

		$scope.searchData = {
			ClassWise: '',
			FeeItemWise: '',
			Report: '',
			ClassWiseOpening: '',
			StudentWiseOpening: ''
		};

		$scope.newClassWise = {
			SelectedClass: null
		};

		$scope.newFeeItemWise = {
			SelectedClass: null,
			SelectedFeeItem: null
		};
		$scope.MonthList = GlobalServices.getMonthList();

		$scope.ClassSectionList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSectionList = res.data.Data;
			$scope.AllClassList = mx(res.data.Data.ClassList);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.FeeItemList = [];
		$scope.FeeItemList_Qry = [];
		GlobalServices.getFeeItemList().then(function (res1) {
			$scope.FeeItemList = res1.data.Data;
			$scope.FeeItemList_Qry = mx(res1.data.Data);
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newBillingConfiguration = {}
		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/GetBillConfiguration",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newBillingConfiguration = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
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

		/*$scope.GetClassWiseOpening();*/
	}
	$scope.CurFeeDetailsColl = [];
	$scope.CurStudent = {};
	$scope.ShowFeeItemWise = function (dt) {
		$scope.CurStudent = dt;
		$scope.CurFeeDetailsColl = dt.FeeItemDetails;
		$('#modal-custom-month-fee1').modal('show');
	};
	$scope.StudentTotal = function () {
		var amt = 0;
		angular.forEach($scope.CurFeeDetailsColl, function (fd) {
			amt = amt + fd.OpeningAmt;
		});
		$scope.CurStudent.OpeningAmt = amt;
	};
	$scope.AddCurFeeDetails = function (ind) {
		if ($scope.CurFeeDetailsColl) {

			if ($scope.CurFeeDetailsColl.length > ind + 1) {

				$scope.CurFeeDetailsColl.splice(ind + 1, 0, {
					FeeItemId: null,
					OpeningAmt: 0,
					Remarks: ''
				})
			} else {
				$scope.CurFeeDetailsColl.push({
					FeeItemId: null,
					OpeningAmt: 0,
					Remarks: ''
				})
			}
		}
	};
	$scope.delCurFeeDetails = function (ind) {
		if ($scope.CurFeeDetailsColl) {
			if ($scope.CurFeeDetailsColl.length > 1) {
				$scope.CurFeeDetailsColl.splice(ind, 1);
				$scope.StudentTotal();
			}
		}
	};
	$scope.GetStudentLstForClassWise = function (semYear) {
		$scope.CurFeeDetailsColl = [];
		$scope.CurStudent = {};
		$scope.newClassWise.StudentColl = [];

		if ($scope.newClassWise.SelectedClass && semYear == true) {
			var findClass = $scope.AllClassList.firstOrDefault(p1 => p1.ClassId == $scope.newClassWise.SelectedClass.ClassId);
			if (findClass) {

				$scope.newClassWise.SelectedClass.ClassType = findClass.ClassType;

				var semQry = mx(findClass.ClassSemesterIdColl);
				var cyQry = mx(findClass.ClassYearIdColl);

				$scope.newClassWise.SelectedClassClassYearList = [];
				$scope.newClassWise.SelectedClassSemesterList = [];

				angular.forEach($scope.SemesterList, function (sem) {
					if (semQry.contains(sem.id)) {
						$scope.newClassWise.SelectedClassSemesterList.push({
							id: sem.id,
							text: sem.text,
							SemesterId: sem.id,
							Name: sem.Name
						});
					}
				});

				angular.forEach($scope.ClassYearList, function (sem) {
					if (cyQry.contains(sem.id)) {
						$scope.newClassWise.SelectedClassClassYearList.push({
							id: sem.id,
							text: sem.text,
							ClassYearId: sem.id,
							Name: sem.Name
						});
					}
				});
			}
		}


		if ($scope.newClassWise.SelectedClass) {
			var para = {
				ClassId: $scope.newClassWise.SelectedClass.ClassId,
				SectionId: $scope.newClassWise.SelectedClass.SectionId,
				SemesterId: $scope.newClassWise.SemesterId,
				ClassYearId: $scope.newClassWise.ClassYearId,
				BatchId: $scope.newClassWise.BatchId,
			};

			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetClassWiseStudentForOpening",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.newClassWise.StudentColl = res.data.Data;

					$http({
						method: 'POST',
						url: base_url + "Fee/Creation/GetAllStudentOpening",
						dataType: "json",
						data: JSON.stringify(para)
					}).then(function (res1) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						if (res1.data.IsSuccess && res1.data.Data) {
							var dataColl = mx(res1.data.Data);

							angular.forEach($scope.newClassWise.StudentColl, function (st) {

								var findQuery = dataColl.where(p1 => p1.StudentId == st.StudentId && p1.SemesterId == st.SemesterId && p1.ClassYearId == st.ClassYearId);

								if (st.SemesterId > 0 || st.ClassYearId > 0)
									findQuery = findQuery.where(p1 => p1.SemesterId == st.SemesterId && p1.ClassYearId == st.ClassYearId);

								var fst = findQuery ? findQuery.firstOrDefault() : null;
								st.OpeningAmt = findQuery ? findQuery.sum(p1 => p1.Amount) : 0;

								if (fst && fst.VoucherDate) {
									$timeout(function () {
										st.VoucherDate_TMP = new Date(fst.VoucherDate);
									});
								}


								st.FeeItemDetails = [];

								angular.forEach(findQuery, function (fi) {
									st.FeeItemDetails.push({
										FeeItemId: fi.FeeItemId,
										OpeningAmt: fi.Amount,
										Remarks: fi.Remarks
									});
								});

								if (st.FeeItemDetails.length == 0) {
									st.FeeItemDetails.push({
										FeeItemId: 0,
										OpeningAmt: 0,
										Remarks: ''
									});
								}

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

	$scope.ClearClassWise = function () {
		$scope.newClassWise.SelectedClass = null;
		$scope.newClassWise.StudentColl = [];
	};
	$scope.SaveUpdateClassWise = function () {

		var dataColl = [];
		var cId = $scope.newClassWise.SelectedClass.ClassId;
		var sId = $scope.newClassWise.SelectedClass.SectionId;
		var semId = $scope.newClassWise.SemesterId;
		var classYearId = $scope.newClassWise.ClassYearId;
		var bid = $scope.newClassWise.BatchId;
		var isValid = true;

		angular.forEach($scope.newClassWise.StudentColl, function (fm) {
			if (fm.OpeningAmt != 0) {
				angular.forEach(fm.FeeItemDetails, function (fi) {

					if (fi.OpeningAmt && fi.OpeningAmt != 0) {
						if (fi.FeeItemId == null || fi.FeeItemId == 0 || fi.FeeItemId == undefined) {
							Swal.fire('Fee Heading Missing of ' + fm.Name);
							isValid = false;
						}

						var findFeeItem = $scope.FeeItemList_Qry.firstOrDefault(p1 => p1.FeeItemId == fi.FeeItemId);

						var beData = {
							ClassId: cId,
							SectionId: sId,
							FeeItemId: fi.FeeItemId,
							StudentId: fm.StudentId,
							SemesterId: fm.SemesterId,
							ClassYearId: fm.ClassYearId,
							OpeningAmt: (fi.OpeningAmt ? fi.OpeningAmt : 0),
							Amount: (fi.OpeningAmt ? fi.OpeningAmt : 0),
							Remarks: fi.Remarks,
							RegNo: fm.RegdNo,
							StudentName: fm.Name,
							FeeItemName: (findFeeItem ? findFeeItem.Name : ''),
							BatchId: bid,
							VoucherDate: (fm.VoucherDateDet ? $filter('date')(fm.VoucherDateDet.dateAD, 'yyyy-MM-dd') : null)
						};
						dataColl.push(beData);
					}
				});
			}
		});

		if (isValid == true) {

			$scope.loadingstatus = "running";
			showPleaseWait();

			if (dataColl.length == 0) {
				var beData = {
					ClassId: cId,
					SectionId: sId,
					BatchId: bid,
					FeeItemId: 0,
					StudentId: 0,
					SemesterId: semId,
					ClassYearId: classYearId,
					OpeningAmt: 0,
					Amount: 0,
					Remarks: '',
					RegNo: '',
					StudentName: '',
					FeeItemName: '',
					VoucherDate: $filter('date')(new Date(), 'yyyy-MM-dd')
				};
				dataColl.push(beData);
			}
			$http({
				method: 'POST',
				url: base_url + "Fee/Creation/SaveStudentOpening",
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

	}

	$scope.GetStudentLstForFeeWise = function (semYear) {
		$scope.newFeeItemWise.StudentColl = [];

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


		if ($scope.newFeeItemWise.SelectedClass && $scope.newFeeItemWise.SelectedFeeItem && $scope.newFeeItemWise.SelectedFeeItem.FeeItemId > 0) {
			var para = {
				ClassId: $scope.newFeeItemWise.SelectedClass.ClassId,
				SectionId: $scope.newFeeItemWise.SelectedClass.SectionId,
				SemesterId: $scope.newFeeItemWise.SemesterId,
				ClassYearId: $scope.newFeeItemWise.ClassYearId,
				BatchId: $scope.newFeeItemWise.BatchId,
			};

			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Academic/Transaction/GetClassWiseStudentForOpening",
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
							FeeItemId: $scope.newFeeItemWise.SelectedFeeItem.FeeItemId,
							SemesterId: $scope.newFeeItemWise.SemesterId,
							ClassYearId: $scope.newFeeItemWise.ClassYearId,
							BatchId: $scope.newFeeItemWise.BatchId,
						};

						$http({
							method: 'POST',
							url: base_url + "Fee/Creation/GetAllStudentOpening",
							dataType: "json",
							data: JSON.stringify(para1)
						}).then(function (res1) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							if (res1.data.IsSuccess && res1.data.Data) {
								var dataColl = mx(res1.data.Data);

								angular.forEach($scope.newFeeItemWise.StudentColl, function (st) {

									var findQuery = dataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId && p1.SemesterId == st.SemesterId && p1.ClassYearId == st.ClassYearId);

									st.OpeningAmt = findQuery ? findQuery.Amount : 0;
									st.Remarks = findQuery ? findQuery.Remarks : '';

									$timeout(function () {
										st.VoucherDate_TMP = findQuery && findQuery.VoucherDate ? new Date(findQuery.VoucherDate) : null;
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

		}
	}

	$scope.ClearFeeItemWise = function () {
		$scope.newFeeItemWise.SelectedClass = null;
		$scope.newFeeItemWise.SelectedFeeItem = null;
		$scope.newFeeItemWise.StudentColl = [];
	};
	$scope.SaveUpdateFeeWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataColl = [];
		var cId = $scope.newFeeItemWise.SelectedClass.ClassId;
		var sId = $scope.newFeeItemWise.SelectedClass.SectionId;
		var fid = $scope.newFeeItemWise.SelectedFeeItem.FeeItemId;

		angular.forEach($scope.newFeeItemWise.StudentColl, function (fm) {
			var beData = {
				ClassId: cId,
				SectionId: sId,
				FeeItemId: fid,
				StudentId: fm.StudentId,
				SemesterId: fm.SemesterId,
				ClassYearId: fm.ClassYearId,
				OpeningAmt: (fm.OpeningAmt ? fm.OpeningAmt : 0),
				Amount: (fm.OpeningAmt ? fm.OpeningAmt : 0),
				Remarks: fm.Remarks,
				VoucherDate: (fm.VoucherDateDet ? $filter('date')(fm.VoucherDateDet.dateAD, 'yyyy-MM-dd') : null)
			};
			dataColl.push(beData);
		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveStudentFeeWiseOpening",
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


	// Class Wise Student Opening

	$scope.GrandTotal = 0;
	$scope.GetClassWiseOpening = function () {
		$scope.ClassWiseOpening = [];
		$scope.ClassWiseOpeningFeeItemColl = [];
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.GrandTotal = 0;
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetClassWiseOpening",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var openingColl = mx(res.data.Data);

				var fiQuery = openingColl.groupBy(t => t.FeeItemName).toArray();
				//var fiQuery = openingColl.groupBy(t => ({ ClassId: t.ClassId, SectionId: t.SectionId, Batch: t.Batch, SemesterId: t.SemesterId, ClassYearId: t.ClassYearId })).toArray();

				var fiSNo = 1;
				angular.forEach(fiQuery, function (f) {
					$scope.ClassWiseOpeningFeeItemColl.push(
						{
							id: fiSNo,
							text: f.key
						});
					fiSNo++;
				});

				var query = openingColl.groupBy(t => ({ ClassSec: t.ClassSec, Batch: t.Batch, Semester: t.Semester, ClassYear: t.ClassYear })).toArray();
				var sno = 1;

				var totalItems = [];
				angular.forEach(query, function (q) {
					var subData = mx(q.elements);
					var fst = subData.firstOrDefault();
					var beData = {
						SNo: sno,
						ClassSec: fst.ClassSec,
						Batch: fst.Batch,
						Semester: fst.Semester,
						ClassYear: fst.ClassYear,
						ClassId: fst.ClassId,
						SectionId: fst.SectionId,
						ClassName: fst.ClassName,
						SectionName: fst.SectionName,
						SemesterId: fst.SemesterId,
						ClassYearId: fst.ClassYearId,
						BatchId: fst.BatchId,
						Amount: subData.sum(p1 => p1.Amount),
						FeeItemDetailsColl: []
					};

					angular.forEach($scope.ClassWiseOpeningFeeItemColl, function (fi) {
						var find = subData.where(p1 => p1.FeeItemName == fi.text).sum(p1 => p1.Amount);
						beData.FeeItemDetailsColl.push({
							FeeItemName: fi.text,
							Amount: find
						});

						totalItems.push({
							FeeItemName: fi.text,
							Amount: find
						});
					});

					$scope.ClassWiseOpening.push(beData);
					sno++;
				});

				var totalQuery = mx(totalItems);
				var grandTotal = 0;
				angular.forEach($scope.ClassWiseOpeningFeeItemColl, function (fi) {
					var find = totalQuery.where(p1 => p1.FeeItemName == fi.text).sum(p1 => p1.Amount);
					fi.Amount = find;
					grandTotal += find;
				});
				$scope.GrandTotal = grandTotal;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.ClassGrandTotal = 0;
	$scope.GetStudentWiseOpening = function (classDet) {
		$scope.StudentWiseOpening = [];
		$scope.StudentWiseOpeningFeeItemColl = [];

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ClassGrandTotal = 0;
		$scope.newClass = {
			ClassSec: classDet.ClassSec || '',
			Batch: classDet.Batch || ''
		};
		var para = {
			ClassId: classDet.ClassId,
			SectionId: classDet.SectionId,
			SemesterId: classDet.SemesterId,
			ClassYearId: classDet.ClassYearId,
			BatchId: classDet.BatchId,
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetStudentWiseOpening",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var openingColl = mx(res.data.Data);

				var fiQuery = openingColl.groupBy(t => t.FeeItemName).toArray();
				var fiSNo = 1;
				angular.forEach(fiQuery, function (f) {
					$scope.StudentWiseOpeningFeeItemColl.push(
						{
							id: fiSNo,
							text: f.key
						});
					fiSNo++;
				});

				var totalItems = [];
				var query = openingColl.groupBy(t => ({ StudentId: t.StudentId, SemesterId: t.SemesterId, ClassYearId: t.ClassYearId })).toArray();
				var sno = 1;
				angular.forEach(query, function (q) {
					var subData = mx(q.elements);
					var fst = subData.firstOrDefault();
					var beData = {
						SNo: sno,
						StudentId: fst.StudentId,
						Name: fst.Name,
						RollNo: fst.RollNo,
						RegNo: fst.RegNo,
						ClassName: fst.ClassName,
						SectionName: fst.SectionName,
						Amount: subData.sum(p1 => p1.Amount),
						SemesterId: fst.SemesterId,
						ClassYearId: fst.ClassYearId,
						VoucherMiti: fst.VoucherMiti,
						FeeItemDetailsColl: []
					};

					angular.forEach($scope.StudentWiseOpeningFeeItemColl, function (fi) {
						var find = subData.where(p1 => p1.FeeItemName == fi.text).sum(p1 => p1.Amount);
						beData.FeeItemDetailsColl.push({
							FeeItemName: fi.text,
							Amount: find
						});
						totalItems.push({
							FeeItemName: fi.text,
							Amount: find
						});
					});



					$scope.StudentWiseOpening.push(beData);
					sno++;
				});

				var totalQuery = mx(totalItems);
				var grandTotal = 0;
				angular.forEach($scope.StudentWiseOpeningFeeItemColl, function (fi) {
					var find = totalQuery.where(p1 => p1.FeeItemName == fi.text).sum(p1 => p1.Amount);
					fi.Amount = find;
					grandTotal += find;
				});
				$scope.ClassGrandTotal = grandTotal;

				document.getElementById('report-section').style.display = "none";
				document.getElementById('report-detail-section').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.ChangeOpeningDate = function () {
		if ($scope.newClassWise.OpeningDateDet) {
			var oDate = new Date($scope.newClassWise.OpeningDateDet.dateAD);
			if (oDate) {
				$scope.newClassWise.StudentColl.forEach(function (st) {
					$timeout(function () {
						st.VoucherDate_TMP = oDate;
					});
				});
			}
		}
	}
	$scope.ChangeOpeningDateFI = function () {
		if ($scope.newFeeItemWise.OpeningDateDet) {
			var oDate = new Date($scope.newFeeItemWise.OpeningDateDet.dateAD);
			if (oDate) {
				$scope.newFeeItemWise.StudentColl.forEach(function (st) {
					$timeout(function () {
						st.VoucherDate_TMP = oDate;
					});
				});
			}
		}
	}
});