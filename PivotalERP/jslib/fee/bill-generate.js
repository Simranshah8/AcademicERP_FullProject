app.controller('BillGenerateController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {

	$scope.Title = 'Bill Generate';
	OnClickDefault();

	$rootScope.ConfigFunction = function () {
		$scope.LoadData();
	};
	$rootScope.ChangeLanguage();

	$scope.LoadData = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newClasswiseFine = {
			MonthId: 0
		};

		$timeout(function () {
			$scope.StudentTypeList = [];
			GlobalServices.getStudentTypeList().then(function (res) {
				$scope.StudentTypeList = res.data.Data;
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

			//$scope.ClassList = [];
			//GlobalServices.getClassList().then(function (res) {
			//	$scope.ClassList = res.data.Data;
			//	$scope.AllClassList = mx(res.data.Data);
			//}, function (reason) {
			//	Swal.fire('Failed' + reason);
			//});

			$scope.FeeItemList = [];
			GlobalServices.getFeeItemList().then(function (res1) {
				$scope.FeeItemList = res1.data.Data;
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

		});

		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();

		$scope.MonthList = [];
		$scope.MonthList_Display = [];
		GlobalServices.getAcademicMonthList(null, null).then(function (res1) {
			$scope.MonthList = [];
			$scope.MonthList_Display = [];
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
				$scope.MonthList_Display.push({ id: m.NM, text: m.MonthYear });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		//Yo classList lai mathi comment garera tala rakheko by Suresh on jan 6
		$scope.ClassList = [];
		$scope.ClassList1 = [];
		GlobalServices.getClassList().then(function (res) {
			$scope.ClassList = res.data.Data;
			$scope.AllClassList = mx(res.data.Data);


			$scope.ClassList1 = res.data.Data;

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



		$scope.newbillconfig = {};

		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/GetBillConfiguration",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newbillconfig = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();

		$scope.currentPages = {
			Classwise: 1,
			Studentwise: 1,

		};

		$scope.searchData = {
			Classwise: '',
			Studentwise: '',
			StudentDet: ''
		};

		$scope.perPage = {
			Classwise: GlobalServices.getPerPageRow(),
			Studentwise: GlobalServices.getPerPageRow(),

		};

		$scope.newClasswise = {
			ClasswiseId: null,
			ClassId: null,
			MonthId: null,
			BillDate: null,
			BillDate_TMP: new Date(),
			ManualBillingDetailsColl: [],
			Mode: 'Save'
		};

		$scope.newStudentwise = {
			StudentwiseId: null,
			ClassId: null,
			ForMonthId: null,
			ToMonthId: null,
			BillDate: null,
			BillDate_TMP: new Date(),
			SelectStudent: $scope.StudentSearchOptions[0].value,
			ManualBillingDetailsColl: [],
			Mode: 'Save'
		};

		$scope.MonthList = [];
		$scope.MonthList_SW = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			$scope.MonthList = [];
			$scope.MonthList_SW = [];

			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
				$scope.MonthList_SW.push({ id: m.NM, text: m.MonthName });
				$scope.MonthList_Display.push({ id: m.NM, text: m.MonthName });
			});

			$timeout(function () {
				$scope.AddManualBillingDetail(0);

				$scope.GetAllClasswiseList();

				//Added By Suresh on Jan 6 by suresh
				$scope.GetAllClasswiseList2();
				//Ends
				$scope.GetAllStudentwiseList();

				$scope.loadingstatus = "stop";
				hidePleaseWait();

			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});



	}

	function OnClickDefault() {
		document.getElementById('classwise-form').style.display = "none";
		document.getElementById('studentwise-form').style.display = "none";

		document.getElementById('add-classwise').onclick = function () {
			document.getElementById('classwise-section').style.display = "none";
			document.getElementById('classwise-form').style.display = "block";
			$scope.ClearClasswise();
		}
		document.getElementById('classwiseback-btn').onclick = function () {
			document.getElementById('classwise-form').style.display = "none";
			document.getElementById('classwise-section').style.display = "block";

			$scope.GetAllClasswiseList();
			//Added By Suresh on 6 Jan
			$scope.GetAllClasswiseList2();
			//Ends
			$scope.ClearClasswise();
		}



		document.getElementById('add-studentwise').onclick = function () {
			document.getElementById('studentwise-section').style.display = "none";
			document.getElementById('studentwise-form').style.display = "block";
			$scope.ClearStudentwise();
		}
		document.getElementById('studentwiseback-btn').onclick = function () {
			document.getElementById('studentwise-form').style.display = "none";
			document.getElementById('studentwise-section').style.display = "block";
			$scope.ClearStudentwise();
		}

	}

	$scope.ClearClasswise = function () {
		$scope.newClasswise = {
			ClasswiseId: null,
			ClassId: null,
			ForMonthId: null,
			BillDate_TMP: new Date(),
			ManualBillingDetailsColl: [],
			Mode: 'Save'
		};
		$scope.AddManualBillingDetail(0);
	}
	$scope.ClearStudentwise = function () {
		$scope.newStudentwise = {
			StudentwiseId: null,
			ClassId: null,
			ForMonthId: null,
			ToMonthId: null,
			BillDate: null,
			BillDate_TMP: new Date(),
			SelectStudent: $scope.StudentSearchOptions[0].value,
			ManualBillingDetailsColl: [],
			Mode: 'Save'
		};
		$scope.AddManualBillingDetailSTW(0);
	}

	//************************* Classwise *********************************
	$scope.ChangeStudentWiseMonth = function () {
		if ($scope.newStudentwise.StudentId) {
			GlobalServices.getAcademicMonthList($scope.newStudentwise.StudentId, null).then(function (resAM) {
				$scope.MonthList_SW = [];
				angular.forEach(resAM.data.Data, function (m) {
					$scope.MonthList_SW.push({ id: m.NM, text: m.MonthYear });
				});
			});
		}
	}

	$scope.getClassWiseSemester = function () {

		if ($scope.newClasswise.SelectedClass) {

			GlobalServices.getAcademicMonthList(null, $scope.newClasswise.SelectedClass.ClassId).then(function (resAM) {
				$scope.MonthList = [];
				angular.forEach(resAM.data.Data, function (m) {
					$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
				});

				var findClass = $scope.AllClassList.firstOrDefault(p1 => p1.ClassId == $scope.newClasswise.SelectedClass.ClassId);
				if (findClass) {

					$scope.newClasswise.SelectedClass.ClassType = findClass.ClassType;

					var semQry = mx(findClass.ClassSemesterIdColl);
					var cyQry = mx(findClass.ClassYearIdColl);

					$scope.newClasswise.SelectedClassClassYearList = [];
					$scope.newClasswise.SelectedClassSemesterList = [];

					angular.forEach($scope.SemesterList, function (sem) {
						if (semQry.contains(sem.id)) {
							$scope.newClasswise.SelectedClassSemesterList.push({
								id: sem.id,
								text: sem.text,
								SemesterId: sem.id,
								Name: sem.Name
							});
						}
					});

					angular.forEach($scope.ClassYearList, function (sem) {
						if (cyQry.contains(sem.id)) {
							$scope.newClasswise.SelectedClassClassYearList.push({
								id: sem.id,
								text: sem.text,
								ClassYearId: sem.id,
								Name: sem.Name
							});
						}
					});
				}

			});


		}
	}
	/*Add and Delete Button*/
	$scope.AddManualBillingDetail = function (ind) {
		if ($scope.newClasswise.ManualBillingDetailsColl) {
			if ($scope.newClasswise.ManualBillingDetailsColl.length > ind + 1) {
				$scope.newClasswise.ManualBillingDetailsColl.splice(ind + 1, 0, {
					FeeItemId: null,
					Qty: 0,
					Rate: 0,
					DiscountPer: 0,
					DiscountAmt: 0,
					PayableAmt: 0
				})
			} else {
				$scope.newClasswise.ManualBillingDetailsColl.push({
					FeeItemId: null,
					Qty: 0,
					Rate: 0,
					DiscountPer: 0,
					DiscountAmt: 0,
					PayableAmt: 0
				})
			}
		}
	};
	$scope.delManualBillingDetail = function (ind) {
		if ($scope.newClasswise.ManualBillingDetailsColl) {
			if ($scope.newClasswise.ManualBillingDetailsColl.length > 1) {
				$scope.newClasswise.ManualBillingDetailsColl.splice(ind, 1);
			}
		}
	};

	$scope.IsValidClasswise = function () {
		//if ($scope.newClasswise.ForMonthId.isEmpty()) {
		//	Swal.fire('Please ! Choose For Month');
		//	return false;
		//}



		return true;
	}

	$scope.SaveUpdateClasswise = function () {
		if ($scope.IsValidClasswise() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newClasswise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateClasswise();
					}
				});
			} else
				$scope.CallSaveUpdateClasswise();

		}
	};

	$scope.CallSaveUpdateClasswise = function () {

		if (!$scope.newClasswise.SelectedClass)
			return;

		$scope.newClasswise.ClassId = $scope.newClasswise.SelectedClass.ClassId;
		$scope.newClasswise.SectionId = $scope.newClasswise.SelectedClass.SectionId;

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newClasswise.DetailsColl = [];

		angular.forEach($scope.newClasswise.ManualBillingDetailsColl, function (det) {

			if (det.FeeItemId && det.FeeItemId > 0) {
				$scope.newClasswise.DetailsColl.push(det);
			}
		});
		if ($scope.newClasswise.BillDateDet && $scope.newClasswise.BillDateDet.dateAD) {
			$scope.newClasswise.BillDate = $filter('date')(new Date($scope.newClasswise.BillDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newClasswise.BillDate = $filter('date')(new Date(new Date()), 'yyyy-MM-dd');



		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/BillGenerateClassWise",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newClasswise }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllClasswiseList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ClasswiseList = [];

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetClassWiseBillGenerateList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dataColl = mx(res.data.Data);

				var monthColl = mx($scope.MonthList_Display);

				var query = dataColl.groupBy(t => t.MonthId).toArray();
				var sno = 1;
				angular.forEach(query, function (q) {

					var findM = monthColl.firstOrDefault(p1 => p1.id == q.key);
					var beData = {
						SNo: sno,
						MonthId: q.key,
						//MonthName:(findM ? findM.text : ''),
						MonthName: q.elements[0].MonthName,
						DataColl: [],
						//Added by Suresh
						TotalBGDiscount: 0,
						TotalBGAmt: 0
					};

					var sno1 = 1;
					angular.forEach(q.elements, function (el) {
						el.SNo = sno1;

						// Accumulate the totals for each bill
						beData.TotalBGDiscount += parseFloat(el.DiscountAmt) || 0;
						beData.TotalBGAmt += parseFloat(el.TotalAmt) || 0;


						beData.DataColl.push(el);
						sno1++;
					})
					sno++;

					$scope.ClasswiseList.push(beData);
				})
				//$scope.ClasswiseList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}




	$scope.CurClassWiseList = [];
	$scope.ShowClasswiseFeeList = function (cl) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CurClassWiseList = [];
		var para = {
			MonthId: cl.MonthId,
			ClassId: cl.ClassId,
			SemesterId: cl.SemesterId,
			ClassYearId: cl.ClassYearId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetClassWiseBillGenerateFeeList",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CurClassWiseList = res.data.Data;

				$scope.SumAmt = 0;
				$scope.DiscountSum = 0;
				$scope.TotalPayableAmt = 0;

				angular.forEach($scope.CurClassWiseList, function (item) {
					$scope.SumAmt += parseFloat(item.Amount) || 0;
					$scope.DiscountSum += parseFloat(item.DisAmt) || 0;
					$scope.TotalPayableAmt += parseFloat(item.PayableAmt) || 0;
				});

				$('#modal-xl').modal('show');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.CurStudentBGDList = [];
	$scope.ShowStudentBGDList = function (cl) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CurStudentBGDList = [];
		$scope.BillGenerateFeeItemColl = [];
		var para = {
			MonthId: cl.MonthId,
			ClassId: cl.ClassId,
			SemesterId: cl.SemesterId,
			ClassYearId: cl.ClassYearId,
			BatchId: cl.BatchId,
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetStudentBGD",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				var finalColl = [];
				var dataColl = mx(res.data.Data);
				var fiQuery = dataColl.groupBy(t => t.FeeItemName).toArray();
				var fiSNo = 1;
				angular.forEach(fiQuery, function (f) {
					$scope.BillGenerateFeeItemColl.push(
						{
							id: fiSNo,
							text: f.key
						});
				});
				var query = dataColl.groupBy(t => t.StudentId).toArray();
				angular.forEach(query, function (q) {
					var subData = mx(q.elements);
					var fst = subData.firstOrDefault();
					var beData = {
						StudentId: fst.StudentId,
						RegdNo: fst.RegdNo,
						Name: fst.Name,
						RollNo: fst.RollNo,
						ClassName: fst.ClassName + ' ' + fst.SectionName + fst.Batch + ' ' + fst.ClassYear + ' ' + fst.Semester,
						Amount: subData.sum(p1 => p1.Amount) - fst.DiscountAmt,
						GrandTotal: subData.sum(p1 => p1.Amount) - fst.DiscountAmt + fst.PDues,
						DiscountAmt: fst.DiscountAmt,
						TaxAmt: fst.TaxAmt,
						FineAmt: fst.FineAmt,
						BillNo: fst.BillNo,
						PDues: fst.PDues,
						Batch: fst.Batch,
						ClassYear: fst.ClassYear,
						Semester: fst.Semester,
						FeeItemDetailsColl: [],
						InvoiceNo: fst.InvoiceNo
					};

					angular.forEach($scope.BillGenerateFeeItemColl, function (fi) {
						var find = subData.where(p1 => p1.FeeItemName == fi.text).sum(p1 => p1.Amount);
						beData.FeeItemDetailsColl.push({
							FeeItemName: fi.text,
							Amount: find
						});
					});

					finalColl.push(beData);

				});

				var secGroup = [];
				var secQuery = mx(finalColl).groupBy(t => t.ClassName);
				angular.forEach(secQuery, function (ss) {

					var mxElements = mx(ss.elements);
					var sg = {
						SectionName: ss.key,
						DataColl: ss.elements,
						Amount: mxElements.sum(p1 => p1.Amount),
						GrandTotal: mxElements.sum(p1 => p1.GrandTotal),
						DiscountAmt: mxElements.sum(p1 => p1.DiscountAmt),
						TaxAmt: mxElements.sum(p1 => p1.TaxAmt),
						FineAmt: mxElements.sum(p1 => p1.FineAmt),
						FeeItemDetailsColl: []
					};

					angular.forEach($scope.BillGenerateFeeItemColl, function (fi) {
						var fg = {
							FeeItemName: fi.text,
							Amount: 0
						};
						angular.forEach(ss.elements, function (el) {
							var find = mx(el.FeeItemDetailsColl).where(p1 => p1.FeeItemName == fi.text).sum(p1 => p1.Amount);
							if (find)
								fg.Amount = fg.Amount + find;
						});

						sg.FeeItemDetailsColl.push(fg);
					});

					secGroup.push(sg);
				})
				$scope.CurStudentBGDList = secGroup;
				//Added function for total
				$scope.calculateTotals();
				$('#modal-info').modal('show');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	// Calculate totals function
	$scope.calculateTotals = function () {
		$scope.FeeItemTotals = {};
		$scope.TotalDiscount = 0;
		$scope.TotalAmount = 0;
		$scope.TotalPDues = 0;
		$scope.TotalGrandTotal = 0;
		angular.forEach($scope.BillGenerateFeeItemColl, function (fi) {
			$scope.FeeItemTotals[fi.text] = 0;
		});

		// Calculate all totals
		angular.forEach($scope.CurStudentBGDList, function (section) {
			$scope.TotalDiscount += section.DiscountAmt;
			$scope.TotalAmount += section.Amount;
			$scope.TotalGrandTotal += section.GrandTotal;

			// Fee item totals
			angular.forEach(section.FeeItemDetailsColl, function (feeItem) {
				$scope.FeeItemTotals[feeItem.FeeItemName] += feeItem.Amount;
			});

			// Previous dues (need to sum from individual students)
			angular.forEach(section.DataColl, function (student) {
				$scope.TotalPDues += student.PDues;
			});
		});
	}


	$scope.CurMissingStudentList = [];
	$scope.CurMissingDet = {};
	$scope.ShowMissingStudentList = function (cl) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CurMissingStudentList = [];
		$scope.CurMissingDet = cl;
		var para = {
			ClassId: cl.ClassId,
			ForMonth: cl.MonthId,
			SemesterId: cl.SemesterId,
			ClassYearId: cl.ClassYearId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetBillMissingStudent",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$scope.CurMissingStudentList = res.data.Data;

				if ($scope.CurMissingStudentList.length > 0)
					$('#modal-left').modal('show');
				else {
					Swal.fire("No Any Student Missing for bill generate");
				}
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.BillGenerateMissingStudent = function (cl) {
		$scope.loadingstatus = "running";
		showPleaseWait();


		var studentIdColl = "";
		angular.forEach($scope.CurMissingStudentList, function (st) {

			if (studentIdColl.length > 0)
				studentIdColl = studentIdColl + ',';

			studentIdColl = studentIdColl + st.StudentId;
		});
		var para = {
			ForMonth: $scope.CurMissingDet.MonthId,
			StudentIdColl: studentIdColl
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/BillGenerateMissingStudent",
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

	$scope.DelClassWiseBillGenerateById = function (refData) {

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
					MonthId: refData.MonthId,
					ClassId: refData.ClassId,
					SemesterId: refData.SemesterId,
					ClassYearId: refData.ClassYearId
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelBillGenerateClassWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllClasswiseList();
						//Added By Suresh on Jan6
						$scope.GetAllClasswiseList2();
						//Ends
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Studentwise *********************************

	$scope.AddManualBillingDetailSTW = function (ind) {
		if ($scope.newStudentwise.ManualBillingDetailsColl) {
			if ($scope.newStudentwise.ManualBillingDetailsColl.length > ind + 1) {
				$scope.newStudentwise.ManualBillingDetailsColl.splice(ind + 1, 0, {
					FeeItemId: null,
					Qty: 0,
					Rate: 0,
					DiscountPer: 0,
					DiscountAmt: 0,
					PayableAmt: 0
				})
			} else {
				$scope.newStudentwise.ManualBillingDetailsColl.push({
					FeeItemId: null,
					Qty: 0,
					Rate: 0,
					DiscountPer: 0,
					DiscountAmt: 0,
					PayableAmt: 0
				})
			}
		}
	};
	$scope.delManualBillingDetailSTW = function (ind) {
		if ($scope.newStudentwise.ManualBillingDetailsColl) {
			if ($scope.newStudentwise.ManualBillingDetailsColl.length > 1) {
				$scope.newStudentwise.ManualBillingDetailsColl.splice(ind, 1);
			}
		}
	};

	$scope.IsValidStudentwise = function () {
		if (!$scope.newStudentwise.StudentId || $scope.newStudentwise.StudentId == null || $scope.newStudentwise.StudentId <= 0) {
			Swal.fire('Please ! Choose Student');
			return false;
		}

		if (!$scope.newStudentwise.FromMonthId || $scope.newStudentwise.FromMonthId == null || $scope.newStudentwise.FromMonthId <= 0) {
			Swal.fire('Please ! Choose From Month');
			return false;
		}

		if (!$scope.newStudentwise.ToMonthId || $scope.newStudentwise.ToMonthId == null || $scope.newStudentwise.ToMonthId <= 0) {
			Swal.fire('Please ! Choose To Month');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateStudentwise = function () {
		if ($scope.IsValidStudentwise() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newStudentwise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateStudentwise();
					}
				});
			} else
				$scope.CallSaveUpdateStudentwise();

		}
	};

	$scope.CallSaveUpdateStudentwise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		//Added
		$scope.newStudentwise.DetailsColl = [];
		angular.forEach($scope.newStudentwise.ManualBillingDetailsColl, function (det) {
			if (det.FeeItemId && det.FeeItemId > 0) {
				$scope.newStudentwise.DetailsColl.push(det);
			}
		});
		//Ends

		if ($scope.newStudentwise.BillDateDet && $scope.newStudentwise.BillDateDet.dateAD) {
			$scope.newStudentwise.BillDate = $filter('date')(new Date($scope.newStudentwise.BillDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.newStudentwise.BillDate = $filter('date')(new Date(new Date()), 'yyyy-MM-dd');

		$scope.newStudentwise.ClassId = $scope.newStudentwise.StudentDetails.ClassId;
		$scope.newStudentwise.SemesterId = $scope.newStudentwise.StudentDetails.SemesterId;
		$scope.newStudentwise.ClassYearId = $scope.newStudentwise.StudentDetails.ClassYearId;
		$scope.newStudentwise.BatchId = $scope.newStudentwise.StudentDetails.BatchId;

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/BillGenerateStudentWise",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newStudentwise }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			//if (res.data.IsSuccess == true) {
			//	$scope.ClearStudentwise();
			//	$scope.GetAllStudentwiseList();
			//}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}
	$scope.DeleteStudentwise = function () {

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
					StudentId: $scope.newStudentwise.StudentId,
					FromMonthId: $scope.newStudentwise.FromMonthId,
					ToMonthId: $scope.newStudentwise.ToMonthId,
					SemesterId: $scope.newStudentwise.StudentDetails.SemesterId,
					ClassYearId: $scope.newStudentwise.StudentDetails.ClassYearId,
					BatchId: $scope.newStudentwise.StudentDetails.BatchId,
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelBillGenerateStudentWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess) {
						$scope.GetAllStudentwiseList();
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};
	$scope.GetAllStudentwiseList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentwiseList = [];

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetStudentWiseBillGenerateFeeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.StudentwiseList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetAllPendingBillGList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.PendingBillGenerateList = [];

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetPendingBillGenerateFeeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dataColl = mx(res.data.Data);

				var monthColl = mx($scope.MonthList_Display);

				var query = dataColl.groupBy(t => t.MonthId).toArray();
				var sno = 1;
				angular.forEach(query, function (q) {
					var beData = {
						SNo: sno,
						MonthId: q.key,
						MonthName: monthColl.firstOrDefault(p1 => p1.id == q.key).text,
						DataColl: []
					};

					var sno1 = 1;
					angular.forEach(q.elements, function (el) {
						el.SNo = sno1;
						beData.DataColl.push(el);
						sno1++;
					})
					sno++;

					$scope.PendingBillGenerateList.push(beData);
				})

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	$scope.BillGenerateFromPending = function (det) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ClassId: det.ClassId,
			MonthId: det.MonthId,
			BillDate: new Date()
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/BillGenerateClassWise",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: para }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}
	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa
	}

	$scope.CallSaveUpdateFine = function () {

		if ($scope.newClasswiseFine.MonthId > 0) {

		} else
			return;

		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/FineGenerateClassWise",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newClasswiseFine }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);


		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}




	//Added By Suresh on Jan 6 starts
	$scope.GetAllClasswiseList2 = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ClasswiseList2 = [];

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetClassWiseBillGenerateList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dataColl2 = mx(res.data.Data);

				var classColl2 = mx($scope.ClassList1);
				var monthListColl = mx($scope.MonthList_Display);

				// Grouping by multiple fields: ClassId, Batch, SemesterId, ClassYearId
				var query = dataColl2.groupBy(t => ({
					ClassId: t.ClassId,
					Batch: t.Batch,
					Semester: t.Semester,
					ClassYear: t.ClassYear,
					SemesterId: t.SemesterId,
					ClassYearId: t.ClassYearId
				})).toArray();

				var sno = 1;
				angular.forEach(query, function (q) {

					var findM = classColl2.firstOrDefault(p1 => p1.id == q.key.ClassId);
					var beData = {
						SNo: sno,
						ClassId: q.key.ClassId,
						Batch: q.key.Batch,
						Semester: q.key.Semester,
						ClassYear: q.key.ClassYear,
						SemesterId: q.key.SemesterId,
						ClassYearId: q.key.ClassYearId,
						ClassName: (findM ? findM.text : ''),
						DataColl2: [],
						//Added by Suresh
						TotalBGDiscount: 0,
						TotalBGAmt: 0
					};

					var sno1 = 1;
					angular.forEach(q.elements, function (el) {
						el.SNo = sno1;
						var findMonth = monthListColl.firstOrDefault(m => m.id == el.MonthId);
						//el.MonthName = findMonth ? findMonth.text : 'Unknown';
						// Accumulate the totals for each bill
						beData.TotalBGDiscount += parseFloat(el.DiscountAmt) || 0;
						beData.TotalBGAmt += parseFloat(el.TotalAmt) || 0;

						beData.DataColl2.push(el);
						sno1++;
					})
					sno++;

					$scope.ClasswiseList2.push(beData);
				})

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}


	$scope.CurStudentWiseList = [];
	$scope.ShowStudentwiseFeeList = function (cl) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.CurStudentWiseList = [];
		var para = {
			FromMonthId: cl.FromMonth,
			ToMonthId: cl.ToMonth,
			StudentId: cl.StudentId
		};

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetStudentBillGenerateFeeList",
			headers: {
				'Content-Type': 'application/json'
			},
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.CurStudentWiseList = res.data.Data;
				// Calculate totals
				$scope.AllSumAmt = 0;
				$scope.AllDiscountSum = 0;
				$scope.AllTotalPayableAmt = 0;

				angular.forEach($scope.CurStudentWiseList, function (item) {
					$scope.AllSumAmt += +item.Amount || 0;
					$scope.AllDiscountSum += +item.DisAmt || 0;
					$scope.AllTotalPayableAmt += +item.PayableAmt || 0;
				});
				//Ends
				$('#modal-xl-std').modal('show');
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}



	//Ends
});