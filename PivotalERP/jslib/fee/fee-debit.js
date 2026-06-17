
$(document).ready(function () {

	$(document).on('keyup', '.serial', function (e) {
		if (e.which == 13) {
			//var checkBoxChecked = $('#chkColumnFocus').prop('checked');
			var checkBoxChecked = true;
			if (checkBoxChecked == true) {
				var $this = $(this);
				var $td = $this.closest('td');
				var $row = $td.closest('tr');
				var $rows = $row.parent();
				var column = $td.index();


				while ($td.length) {

					$row = $row.next('tr');

					if ($row.length == 0) {

						$row = $rows.children().first();
						// $row = $rows.children().get(2);

						column++;
					}

					$td = $row.children().eq(column);
					var $input = $td.find('.serial');
					if ($input.length) {
						$input.focus();
						break;
					}
				}
			} else {

				var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
				if (key == 13) {
					e.preventDefault();
					var inputs = $(this).closest('form').find(':input:visible');
					inputs.eq(inputs.index(this) + 1).focus();
				}
			}

		}
	});

	$(document).on('keyup', '.serialSub', function (e) {
		if (e.which == 13) {
			var checkBoxChecked = $('#chkColumnFocusSub').prop('checked');
			if (checkBoxChecked == true) {
				var $this = $(this);
				var $td = $this.closest('td');
				var $row = $td.closest('tr');
				var $rows = $row.parent();
				var column = $td.index();


				while ($td.length) {

					$row = $row.next('tr');

					if ($row.length == 0) {

						$row = $rows.children().first();
						// $row = $rows.children().get(2);

						column++;
					}

					$td = $row.children().eq(column);
					var $input = $td.find('.serialSub');
					if ($input.length) {
						$input.focus();
						break;
					}
				}
			} else {

				var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
				if (key == 13) {
					e.preventDefault();
					var inputs = $(this).closest('form').find(':input:visible');
					inputs.eq(inputs.index(this) + 1).focus();
				}
			}

		}
	});
});

app.controller('FeeDebitController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {
	$scope.Title = 'Fee Debit';

	$rootScope.ConfigFunction = function () {
		$scope.LoadData();
	};
	$rootScope.ChangeLanguage();

	$scope.LoadData = function ()
	{
		$scope.confirmMSG = GlobalServices.getConfirmMSG();	
		//$scope.MonthList = GlobalServices.getMonthList();
		$scope.MonthList = [];
		GlobalServices.getAcademicMonthList(null, null).then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();

		$scope.FeeItemList = [];
		GlobalServices.getFeeItemList().then(function (res1) {
			$scope.FeeItemList = res1.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassSectionList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AllClassList = [];
		GlobalServices.getClassList().then(function (res) {
			$scope.AllClassList = mx(res.data.Data);
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

			} else {
				$scope.gridOptions.columnApi.setColumnsVisible(["Faculty"], false);
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["Faculty"], false);
			}




			if ($scope.AcademicConfig.ActiveLevel == true) {

				$scope.LevelList = [];
				GlobalServices.getClassLevelList().then(function (res) {
					$scope.LevelList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				$scope.gridOptions.columnApi.setColumnsVisible(["Level"], false);
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["Level"], false);
			}


			if ($scope.AcademicConfig.ActiveSemester == true) {

				$scope.SelectedClassSemesterList = [];
				$scope.SemesterList = [];
				GlobalServices.getSemesterList().then(function (res) {
					$scope.SemesterList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				$scope.gridOptions.columnApi.setColumnsVisible(["Semester"], false);
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["Semester"], false);
			}

			if ($scope.AcademicConfig.ActiveBatch == true) {

				$scope.BatchList = [];
				GlobalServices.getBatchList().then(function (res) {
					$scope.BatchList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				$scope.gridOptions.columnApi.setColumnsVisible(["Batch"], false);
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["Batch"], false);
			}

			if ($scope.AcademicConfig.ActiveClassYear == true) {

				$scope.ClassYearList = [];
				$scope.SelectedClassClassYearList = [];
				GlobalServices.getClassYearList().then(function (res) {
					$scope.ClassYearList = res.data.Data;
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			} else {
				$scope.gridOptionsDW.columnApi.setColumnsVisible(["ClassYear"], false);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.currentPages = {
			FeeDebitClasswise: 1,
			FeeDebitStudentWise: 1,
			FeeDebitFeeItemwise: 1			
		};

		$scope.searchData = {
			FeeDebitClasswise: '',
			FeeDebitStudentWise: '',
			FeeDebitFeeItemwise: ''
		};

		$scope.perPage = {
			FeeDebitClasswise: GlobalServices.getPerPageRow(),
			FeeDebitStudentWise: GlobalServices.getPerPageRow(),
			FeeDebitFeeItemwise: GlobalServices.getPerPageRow()
			
		};

		$scope.newFeeDebitClasswise = {
			FeeDebitClasswiseId: null,
			ClassId: null,
			MonthId: null,
			SortingId: null,
			ShowLeftStudent:false,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			Mode: 'Save'
		};
		

		$scope.newFeeDebitStudentWise = {
			FeeDebitStudentWiseId: null,			
			FeeDebitStudentWiseDetailColl: [],
			SelectStudent: $scope.StudentSearchOptions[0].value,
			Mode: 'Save'
		};
		$scope.newFeeDebitStudentWise.FeeDebitStudentWiseDetailColl.push({});

		$scope.newFeeDebitFeeItemwise = {
			FeeDebitFeeItemwiseId: null,
			Name: '',
			Description: '',
			OrderNo: 0,
			SelectStudent: $scope.StudentSearchOptions[0].value,
			Mode: 'Save'
		};

		$scope.SortAsList = [{ text: 'RollNo', value: 'RollNo' }, { text: 'Name', value: 'FirstName' }, { text: 'RegNo', value: 'RegNo' }];

		//$scope.GetAllClassList();
		//$scope.GetAllSectionList();
		//$scope.GetAllAcademicYearList();
		//$scope.GetAllBoardList();

	}

	
	//$scope.ClearFeeDebitClasswise = function () {
	//	$scope.newClass = {
	//		ClassId: null,
	//		Name: '',
	//		Description: '',
	//		OrderNo: 0,
	//		Mode: 'Save'
	//	};
	//}
	$scope.ClearFeeDebitStudentWise = function () {
		$scope.newSection = {
			FeeDebitStudentWiseId: null,
			FeeDebitStudentWiseDetailColl: [],
			/*Mode: 'Save'*/
		};
		$scope.newFeeDebitStudentWise.FeeDebitStudentWiseDetailColl.push({});
	}
	//$scope.ClearFeeDebitFeeItemwise = function () {
	//	$scope.newFeeDebitFeeItemwise = {
	//		AcademicYearId: null,
	//		Name: '',
	//		Description: '',
	//		OrderNo: 0,
	//		Mode: 'Save'
	//	};
	//}
	
	//************************* Fee Debit Classwise *********************************

	//$scope.IsValidFeeDebitClasswise = function () {
	//	if ($scope.newFeeDebitClasswise.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter FeeDebitClasswise Name');
	//		return false;
	//	}

	//	return true;
	//}

	$scope.DataSorting = function () {

		var dataColl = $filter('orderBy')($scope.newFeeDebitClasswise.StudentColl, $scope.newFeeDebitClasswise.SortAs);
		$scope.newFeeDebitClasswise.StudentColl = [];
		$timeout(function () {
			$scope.$apply(function () {
				angular.forEach(dataColl, function (dc) {
					$scope.newFeeDebitClasswise.StudentColl.push(dc);
				})
			});
		});

	};

	$scope.ChangeOnRateAmt = function (st,fd, col) {

		if (col == 1) {
			fd.PayableAmt = fd.Rate - fd.DiscountAmt;
		} else if (col == 2) {
			var disAmt = 0;
			if (fd.DiscountPer > 0)
				disAmt = fd.Rate * fd.DiscountPer / 100;

			fd.DiscountAmt = disAmt;
			fd.PayableAmt = fd.Rate - disAmt;
		} else if (col == 3) {
			var disPer = 0;
			if (fd.DiscountAmt > 0)
				disPer = fd.DiscountAmt / fd.Rate * 100;

			fd.DiscountPer = disPer;
			fd.PayableAmt = fd.Rate - fd.DiscountAmt;
		}

		var subData = mx(st.FeeItemColl);
		st.Rate = subData.sum(p1 => p1.Rate);
		st.DiscountAmt = subData.sum(p1 => p1.DiscountAmt);
		st.PayableAmt = subData.sum(p1 => p1.PayableAmt);
	};

	$scope.GetStudentLstForClassWise = function (col) {
		$scope.newFeeDebitClasswise.StudentColl = [];
		
		if ($scope.newFeeDebitClasswise.SelectedClass) {
			var para = {
				ClassId: $scope.newFeeDebitClasswise.SelectedClass.ClassId,
				SectionId: $scope.newFeeDebitClasswise.SelectedClass.SectionId,
				All: $scope.newFeeDebitClasswise.ShowLeftStudent,
			};

			if (col == 'class') {
				$scope.MonthList = [];

				GlobalServices.getAcademicMonthList(null, para.ClassId).then(function (resAM) {
					angular.forEach(resAM.data.Data, function (m) {
						$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
					});
				});
				return;
            }
			

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
					$scope.newFeeDebitClasswise.StudentColl = res.data.Data;

					if ($scope.newFeeDebitClasswise.MonthId && $scope.newFeeDebitClasswise.MonthId > 0) {

						var para1 = {
							ClassId: para.ClassId,
							SectionId: para.SectionId,
							MonthId: $scope.newFeeDebitClasswise.MonthId,
							BatchId: $scope.newFeeDebitClasswise.BatchId,
							SemesterId: $scope.newFeeDebitClasswise.SemesterId,
							ClassYearId: $scope.newFeeDebitClasswise.ClassYearId,
						};

						$http({
							method: 'POST',
							url: base_url + "Fee/Creation/GetAllFeeDebit",
							dataType: "json",
							data: JSON.stringify(para1)
						}).then(function (res1) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							if (res1.data.IsSuccess && res1.data.Data) {
								var dataColl = mx(res1.data.Data);


								angular.forEach($scope.newFeeDebitClasswise.StudentColl, function (st) {
									var findQuery = dataColl.where(p1 => p1.StudentId == st.StudentId);
									st.FeeItemColl = [];
									angular.forEach($scope.FeeItemList, function (fi) {

										var find = null;
										if (findQuery)
											find = findQuery.firstOrDefault(p1 => p1.FeeItemId == fi.FeeItemId);

										var item = {
											ClassId: para.ClassId,
											SectionId: para.SectionId,
											StudentId: st.StudentId,
											BatchId: st.BatchId,
											SemesterId: st.SemesterId,
											ClassYearId:st.ClassYearId,
											MonthId: para1.MonthId,
											FeeItemId: fi.FeeItemId,
											Rate: find ? find.Rate : 0,
											DiscountPer: find ? find.DiscountPer : 0,
											DiscountAmt: find ? find.DiscountAmt : 0,
											TaxAmt: find ? find.TaxAmt : 0,
											FineAmt: find ? find.FineAmt : 0,
											PayableAmt: find ? find.PayableAmt : 0,
											Remarks: find ? find.Remarks : ''
										};

										st.FeeItemColl.push(item);
									});

									var subData = mx(st.FeeItemColl);
									st.Rate = subData.sum(p1 => p1.Rate);
									st.DiscountAmt = subData.sum(p1 => p1.DiscountAmt);
									st.PayableAmt = subData.sum(p1 => p1.PayableAmt);
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
	
	$scope.SaveUpdateFeeDebitClasswise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var dataColl = [];
		angular.forEach($scope.newFeeDebitClasswise.StudentColl, function (st) {
			angular.forEach(st.FeeItemColl, function (fi) {
				dataColl.push(fi);
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveFeeDebit",
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

	$scope.TransferFeeDebitClasswise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var mid = $scope.newFeeDebitClasswise.ToMonthId;

		if (mid > 0) {
			var dataColl = [];
			angular.forEach($scope.newFeeDebitClasswise.StudentColl, function (st) {
				angular.forEach(st.FeeItemColl, function (fi) {
					fi.MonthId = mid;
					dataColl.push(fi);
				});
			});

			$http({
				method: 'POST',
				url: base_url + "Fee/Creation/SaveFeeDebit",
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


	$scope.DelFeeDebitClasswiseById = function (refData) {

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
					FeeDebitClasswiseId: refData.FeeDebitClasswiseId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelFeeDebitClasswise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFeeDebitClasswiseList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Section *********************************

	//$scope.IsValidFeeDebitStudentWise = function () {
	//	if ($scope.newFeeDebitStudentWise.Name.isEmpty()) {
	//		Swal.fire('Please ! Enter FeeDebitStudentWise Name');
	//		return false;
	//	}

	//	return true;
	//}

	$scope.ChangeOnStudentRateAmt = function (st, fd, col) {

		if (col == 1) {
			fd.PayableAmt = fd.Rate - fd.DiscountAmt;
		} else if (col == 2) {
			var disAmt = 0;
			if (fd.DiscountPer > 0)
				disAmt = fd.Rate * fd.DiscountPer / 100;

			fd.DiscountAmt = disAmt;
			fd.PayableAmt = fd.Rate - disAmt;
		} else if (col == 3) {
			var disPer = 0;
			if (fd.DiscountAmt > 0)
				disPer = fd.DiscountAmt / fd.Rate * 100;

			fd.DiscountPer = disPer;
			fd.PayableAmt = fd.Rate - fd.DiscountAmt;
		}

		var subData = mx(st.FeeItemColl);
		st.Rate = subData.sum(p1 => p1.Rate);
		st.DiscountAmt = subData.sum(p1 => p1.DiscountAmt);
		st.PayableAmt = subData.sum(p1 => p1.PayableAmt);


		var summary = mx($scope.newFeeDebitStudentWise.MonthDetailsColl);

		$scope.newFeeDebitStudentWise.Rate = summary.sum(p1 => p1.Rate);
		$scope.newFeeDebitStudentWise.DiscountAmt = summary.sum(p1 => p1.DiscountAmt);
		$scope.newFeeDebitStudentWise.PayableAmt = summary.sum(p1 => p1.PayableAmt);

		$scope.newFeeDebitStudentWise.GrandTotalColl = [];

		angular.forEach($scope.FeeItemList, function (fi) {
			var beData = {
				Rate: 0,
				DiscountAmt: 0,
				PayableAmt: 0
			};
			angular.forEach($scope.newFeeDebitStudentWise.MonthDetailsColl, function (md) {

				var findData = mx(md.FeeItemColl).where(p1 => p1.FeeItemId == fi.FeeItemId);
				beData.Rate = beData.Rate + (findData ? findData.sum(p1 => p1.Rate) : 0);
				beData.DiscountAmt = beData.DiscountAmt + (findData ? findData.sum(p1 => p1.DiscountAmt) : 0);
				beData.PayableAmt = beData.PayableAmt + (findData ? findData.sum(p1 => p1.PayableAmt) : 0);
			});

			$scope.newFeeDebitStudentWise.GrandTotalColl.push(beData);
		});
	};
	$scope.GetStudentWise = function () {

		$scope.newFeeDebitStudentWise.GrandTotalColl = [];
		$scope.newFeeDebitStudentWise.MonthDetailsColl = [];
		if ($scope.newFeeDebitStudentWise.StudentId && $scope.newFeeDebitStudentWise.StudentId > 0) {

			//GlobalServices.getAcademicMonthList($scope.newFeeDebitStudentWise.StudentId, null).then(function (resAM) {
			//	angular.forEach(resAM.data.Data, function (m) {
			//		$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
			//	});
			//});

			GlobalServices.getAcademicMonthList($scope.newFeeDebitStudentWise.StudentId, null).then(function (resAM) {
				$scope.MonthList = [];
				angular.forEach(resAM.data.Data, function (m) {
					$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
				});


				var para1 = {
					StudentId: $scope.newFeeDebitStudentWise.StudentId
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/GetAllFeeDebitStudentWise",
					dataType: "json",
					data: JSON.stringify(para1)
				}).then(function (res1) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res1.data.IsSuccess && res1.data.Data) {
						var dataColl = mx(res1.data.Data);
						$scope.newFeeDebitStudentWise.MonthDetailsColl = [];

						angular.forEach($scope.MonthList, function (mn) {
							var findQuery = dataColl.where(p1 => p1.MonthId == mn.id);

							var mon = {
								MonthId: mn.id,
								MonthName: mn.text,
								StudentId: para1.StudentId,
								FeeItemColl: []
							};
							angular.forEach($scope.FeeItemList, function (fi) {

								var find = null;
								if (findQuery)
									find = findQuery.firstOrDefault(p1 => p1.FeeItemId == fi.FeeItemId);

								var item = {
									StudentId: para1.StudentId,
									MonthId: mn.id,
									FeeItemId: fi.FeeItemId,
									Rate: find ? find.Rate : 0,
									DiscountPer: find ? find.DiscountPer : 0,
									DiscountAmt: find ? find.DiscountAmt : 0,
									TaxAmt: find ? find.TaxAmt : 0,
									FineAmt: find ? find.FineAmt : 0,
									PayableAmt: find ? find.PayableAmt : 0,
									Remarks: find ? find.Remarks : ''
								};

								mon.FeeItemColl.push(item);
							});
							var subData = mx(mon.FeeItemColl);
							mon.Rate = subData.sum(p1 => p1.Rate);
							mon.DiscountAmt = subData.sum(p1 => p1.DiscountAmt);
							mon.PayableAmt = subData.sum(p1 => p1.PayableAmt);
							$scope.newFeeDebitStudentWise.MonthDetailsColl.push(mon);

						});

						var summary = mx($scope.newFeeDebitStudentWise.MonthDetailsColl);

						$scope.newFeeDebitStudentWise.Rate = summary.sum(p1 => p1.Rate);
						$scope.newFeeDebitStudentWise.DiscountAmt = summary.sum(p1 => p1.DiscountAmt);
						$scope.newFeeDebitStudentWise.PayableAmt = summary.sum(p1 => p1.PayableAmt);

						$scope.newFeeDebitStudentWise.GrandTotalColl = [];

						angular.forEach($scope.FeeItemList, function (fi) {
							var beData = {
								Rate: 0,
								DiscountAmt: 0,
								PayableAmt: 0
							};
							angular.forEach($scope.newFeeDebitStudentWise.MonthDetailsColl, function (md) {

								var findData = mx(md.FeeItemColl).where(p1 => p1.FeeItemId == fi.FeeItemId);
								beData.Rate = beData.Rate + (findData ? findData.sum(p1 => p1.Rate) : 0);
								beData.DiscountAmt = beData.DiscountAmt + (findData ? findData.sum(p1 => p1.DiscountAmt) : 0);
								beData.PayableAmt = beData.PayableAmt + (findData ? findData.sum(p1 => p1.PayableAmt) : 0);
							});

							$scope.newFeeDebitStudentWise.GrandTotalColl.push(beData);
						});


						//var subData = mx(st.FeeItemColl);
						//st.Rate = subData.sum(p1 => p1.Rate);
						//st.DiscountAmt = subData.sum(p1 => p1.DiscountAmt);
						//st.PayableAmt = subData.sum(p1 => p1.PayableAmt);

					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});

			  
			});



		

        }
	};

	$scope.SaveUpdateFeeDebitStudentWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		 
		var dataColl = [];
		angular.forEach($scope.newFeeDebitStudentWise.MonthDetailsColl, function (st)
		{
			angular.forEach(st.FeeItemColl, function (fi) {

				fi.BatchId = $scope.newFeeDebitStudentWise.StudentDetails.BatchId;
				fi.SemesterId = $scope.newFeeDebitStudentWise.StudentDetails.SemesterId;
				fi.ClassYearId = $scope.newFeeDebitStudentWise.StudentDetails.ClassYearId;

				dataColl.push(fi);
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveFeeDebitStudentWise",
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


	$scope.DelFeeDebitStudentWiseById = function (refData) {

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
					FeeDebitStudentWiseId: refData.FeeDebitStudentWiseId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelFeeDebitStudentWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFeeDebitStudentWiseList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Academic Year *********************************

	$scope.IsValidFeeDebitFeeItemwise = function () {
		if (!$scope.newFeeDebitFeeItemwise.MonthId || $scope.newFeeDebitFeeItemwise.MonthId==0) {
			Swal.fire('Please ! Select MonthName');
			return false;
		}

		if (!$scope.newFeeDebitFeeItemwise.FeeItemId || $scope.newFeeDebitFeeItemwise.FeeItemId == 0) {
			Swal.fire('Please ! Select FeeItem Name');
			return false;
		}

		if (!$scope.newFeeDebitFeeItemwise.Amount || $scope.newFeeDebitFeeItemwise.Amount == 0) {
			Swal.fire('Please ! Enter Amount');
			return false;
		}

		
		return true;
	}

	$scope.SaveUpdateFeeDebitFeeItemwise = function () {

		if ($scope.IsValidFeeDebitFeeItemwise() == false)
			return;

		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.newFeeDebitFeeItemwise.ClassIdColl = '';
		$scope.newFeeDebitFeeItemwise.SectionIdColl = '';

		var cIdColl = [];
		var sIdColl = [];
		if ($scope.newFeeDebitFeeItemwise.SelectedClassColl) {
			$scope.newFeeDebitFeeItemwise.SelectedClassColl.forEach(function (sc) {
				if (sc.ClassId > 0) {
					cIdColl.push(sc.ClassId);
				}

				if (sc.SectionId > 0) {
					sIdColl.push(sc.SectionId);
				}
			});
		}

		if (cIdColl.length > 0)
			$scope.newFeeDebitFeeItemwise.ClassIdColl = cIdColl.toString();

		if (sIdColl.length > 0)
			$scope.newFeeDebitFeeItemwise.SectionIdColl = sIdColl.toString();

		//if ($scope.newFeeDebitFeeItemwise.ClassId)
		//	$scope.newFeeDebitFeeItemwise.ClassIdColl = $scope.newFeeDebitFeeItemwise.ClassId.toString();
		//else
		//	$scope.newFeeDebitFeeItemwise.ClassIdColl = '';

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveFeeDebitFeeItemWise",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newFeeDebitFeeItemwise }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearFeeDebitFeeItemwise();
				$scope.GetAllFeeDebitFeeItemwiseList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllFeeDebitFeeItemwiseList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FeeDebitFeeItemwiseList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllFeeDebitFeeItemwiseList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.FeeDebitFeeItemwiseList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetFeeDebitFeeItemwiseById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			FeeDebitFeeItemwiseId: refData.FeeDebitFeeItemwiseId
		};

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetFeeDebitFeeItemwiseById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newFeeDebitFeeItemwise = res.data.Data;
				$scope.newFeeDebitFeeItemwise.Mode = 'Modify';

				document.getElementById('batch-section').style.display = "none";
				document.getElementById('batch-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelFeeDebitFeeItemwiseById = function (refData) {

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
					FeeDebitFeeItemwiseId: refData.FeeDebitFeeItemwiseId
				};

				$http({
					method: 'POST',
					url: base_url + "Academic/Creation/DelFeeDebitFeeItemwise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllFeeDebitFeeItemwiseList();
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