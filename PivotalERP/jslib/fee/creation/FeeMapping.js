
$(document).ready(function () {

	$(document).on('keyup', '.serialCW', function (e) {
		if (e.which == 13) {
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
						column++;
					}

					$td = $row.children().eq(column);
					var $input = $td.find('.serialCW');
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


	$(document).on('keyup', '.serialSTW', function (e) {
		if (e.which == 13) {
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
						column++;
					}

					$td = $row.children().eq(column);
					var $input = $td.find('.serialSTW');
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


	$(document).on('keyup', '.serialMW', function (e) {
		if (e.which == 13) {
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
						column++;
					}

					$td = $row.children().eq(column);
					var $input = $td.find('.serialMW');
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

	$(document).on('keyup', '.serialF', function (e) {
		if (e.which == 13) {
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
						column++;
					}

					$td = $row.children().eq(column);
					var $input = $td.find('.serialF');
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
app.controller('FeeMappingController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Fee Mapping';
	
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.searchData = {
			FeeMappingClassWise: '',
			NotApplicable: '',
			FixedAmount: '',
			StudentTypeWise: '',
			MediumWise: '',
			ForWise:'',
			Extra:'',
			BatchWise:''
		};

		$scope.ForWiseColl = [{ id: 4, text: 'Enquiry' }, { id: 5, text: 'Registration' },{ id: 1, text: 'Admission' }, { id: 2, text: 'Left Student' }, { id: 3, text: 'Passout Student' }];

		$scope.newExtra = {};

		$scope.BatchWise = {
			BatchId: null,
			FacultyId:null
		};

		//$scope.MonthList = GlobalServices.getMonthList();

	

		$scope.BillingConfig = {}
		$http({
			method: 'POST',
			url: base_url + "Fee/Transaction/GetBillConfiguration",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BillingConfig = res.data.Data;
			}  
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.MonthList = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		

		$scope.AllClassList = [];
		GlobalServices.getClassList().then(function (AllClassres) {
			$scope.AllClassList = mx(AllClassres.data.Data);

			$scope.ClassList = [];
			GlobalServices.getClassSectionList().then(function (res) {
				$scope.ClassList = res.data.Data;
				//$scope.AllClassList = mx(res.data.Data);
				$scope.FeeItemList = [];
				GlobalServices.getFeeItemList().then(function (res1) {
					$scope.FeeItemList = res1.data.Data;
					$scope.FeeItemListForMapping = [];
					$scope.FeeItemListForExtra = [];
					$scope.FeeItemListForMess = [];
					$scope.FeeItemListForNotApplicable = [];
					//Classwise ma HeadFor=1 and IsExtraFee=0.condition update garnu theyo. So esko lagi seperate list ma push gardai xu.Classwise,Mediumwise,Batchwise and StudentTypewise ma yo euta use vayeraxa
					$scope.FeeItemListForMappingCW = [];
					//Ends
					angular.forEach($scope.FeeItemList, function (fi) {
						if (fi.IsExtraFee == false)
							$scope.FeeItemListForMapping.push(fi);

						//Classwise ko lagi push vayeko xa(Chaitra 10 2082 ma classwise ma pani HeadFor=1 feri remove vako theyo)
					/*	if (fi.IsExtraFee == false && fi.HeadFor == 1)*/
							if (fi.IsExtraFee == false)
							$scope.FeeItemListForMappingCW.push(fi);

						//Ends

						if (fi.HeadFor == 8)
							$scope.FeeItemListForExtra.push(fi);
						else if (fi.HeadFor == 4)
							$scope.FeeItemListForMess.push(fi);
						else
							$scope.FeeItemListForNotApplicable.push(fi);
					});

					$timeout(function () {
						$scope.GetFeeMappingClassWise();
					});

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		
		//$scope.ClassList = [];
		//GlobalServices.getClassList().then(function (res) {
		//	$scope.ClassList = res.data.Data;
		//	$scope.AllClassList = mx(res.data.Data);
		//	$scope.FeeItemList = [];
		//	GlobalServices.getFeeItemList().then(function (res1) {
		//		$scope.FeeItemList = res1.data.Data;
		//		$scope.FeeItemListForMapping = [];
		//		$scope.FeeItemListForExtra = [];
		//		$scope.FeeItemListForMess = [];
		//		$scope.FeeItemListForNotApplicable = [];
		//		angular.forEach($scope.FeeItemList, function (fi)
		//		{
		//			if (fi.IsExtraFee == false)
		//				$scope.FeeItemListForMapping.push(fi);

		//			if (fi.HeadFor == 8)
		//				$scope.FeeItemListForExtra.push(fi);
		//			else if (fi.HeadFor == 4)
		//				$scope.FeeItemListForMess.push(fi);
		//			else
		//				$scope.FeeItemListForNotApplicable.push(fi);
		//		});

		//		$timeout(function () {
		//			$scope.GetFeeMappingClassWise();
		//		});

		//	}, function (reason) {
		//		Swal.fire('Failed' + reason);
		//	});

		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		$scope.ClassSectionList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSectionList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.StudentTypeList = [];
		GlobalServices.getStudentTypeList().then(function (res) {
			$scope.StudentTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.MediumList = [];
		GlobalServices.getMediumList().then(function (res) {
			$scope.MediumList = res.data.Data;
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

	}

	$scope.CheckedAllExtra = function (fi) {
		
		angular.forEach($scope.newExtra.StudentColl, function (det) {

			angular.forEach(det.DiscountDetailsColl, function (md)
			{
				if (md.FeeItemId == fi.FeeItemId) {
					md.Include = fi.CheckAll;
                }				
			});
		});

	};
	$scope.GetStudentLstForExtra = function () {
		$scope.newExtra.StudentColl = [];
		if ($scope.newExtra.SelectedClass) {
			var para = {
				ClassId: $scope.newExtra.SelectedClass.ClassId,
				SectionId: $scope.newExtra.SelectedClass.SectionId || null,
				All: ($scope.BillingConfig.ShowLeftStudentInFeeMapping ? true : false),
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
					$scope.newExtra.StudentColl = res.data.Data;

					var para1 = {
						ClassId: para.ClassId,
						SectionId: para.SectionId
					};

					$http({
						method: 'POST',
						url: base_url + "Fee/Creation/GetAllExtraFeeItemMappingSetup",
						dataType: "json",
						data: JSON.stringify(para1)
					}).then(function (res1) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						if (res1.data.IsSuccess && res1.data.Data) {
							var dataColl = mx(res1.data.Data);

							angular.forEach($scope.newExtra.StudentColl, function (st) {
								st.DiscountDetailsColl = [];

								angular.forEach($scope.FeeItemListForExtra, function (dt) {

									var find = dataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId && p1.FeeItemId == dt.FeeItemId);

									st.DiscountDetailsColl.push({
										FeeItemId: dt.FeeItemId,
										Include: find ? true : false
									});
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

	$scope.SaveUpdateExtraMapping = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var cid = $scope.newExtra.SelectedClass.ClassId;
		var sid = $scope.newExtra.SelectedClass.SectionId;
		var dataColl = [];
		angular.forEach($scope.newExtra.StudentColl, function (det) {

			angular.forEach(det.DiscountDetailsColl, function (md) {
				if (md.Include == true) {
					dataColl.push({
						ClassId: cid,
						SectionId: sid,
						StudentId: det.StudentId,
						FeeItemId: md.FeeItemId,
						Remarks: det.Remarks
					});
				}
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveExtraFeeItemMappingSetup",
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
	$scope.DelExtraMapping = function () {
		var cid = $scope.newExtra.SelectedClass.ClassId;
		var sid = $scope.newExtra.SelectedClass.SectionId || null;
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
					SectionId: sid
				};

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelExtraFeeItemMappingSetup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetStudentLstForExtra();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};
	$scope.CheckedAllMess = function (fi) {

		angular.forEach($scope.newMess.StudentColl, function (det) {

			angular.forEach(det.DiscountDetailsColl, function (md) {
				if (md.FeeItemId == fi.FeeItemId) {
					md.Include = fi.CheckAll;
				}
			});
		});

	};

	$scope.GetStudentLstForMess = function () {
		$scope.newMess.StudentColl = [];
		if ($scope.newMess.SelectedClass) {
			var para = {
				ClassId: $scope.newMess.SelectedClass.ClassId,
				SectionId: $scope.newMess.SelectedClass.SectionId || null,
				All: ($scope.BillingConfig.ShowLeftStudentInFeeMapping ? true : false),
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
					$scope.newMess.StudentColl = res.data.Data;

					var para1 = {
						ClassId: $scope.newMess.SelectedClass.ClassId,
						SectionId: $scope.newMess.SelectedClass.SectionId || null,
					};

					$http({
						method: 'POST',
						url: base_url + "Fee/Creation/GetAllMessFeeItemMappingSetup",
						dataType: "json",
						data: JSON.stringify(para1)
					}).then(function (res1) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						if (res1.data.IsSuccess && res1.data.Data) {
							var dataColl = mx(res1.data.Data);

							angular.forEach($scope.newMess.StudentColl, function (st) {
								st.DiscountDetailsColl = [];

								angular.forEach($scope.FeeItemListForMess, function (dt) {

									var find = dataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId && p1.FeeItemId == dt.FeeItemId);

									st.DiscountDetailsColl.push({
										FeeItemId: dt.FeeItemId,
										Include: find ? true : false
									});
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

	$scope.SaveUpdateMessMapping = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var cid = $scope.newMess.SelectedClass.ClassId;
		var sid = $scope.newMess.SelectedClass.SectionId;
		var dataColl = [];
		angular.forEach($scope.newMess.StudentColl, function (det) {

			angular.forEach(det.DiscountDetailsColl, function (md) {
				if (md.Include == true) {
					dataColl.push({
						ClassId: cid,
						SectionId: sid,
						StudentId: det.StudentId,
						FeeItemId: md.FeeItemId,
						Remarks: det.Remarks
					});
				}
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveMessFeeItemMappingSetup",
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

	$scope.DelMessMapping = function () {

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
					ClassId: $scope.newMess.SelectedClass.ClassId,
					SectionId: $scope.newMess.SelectedClass.SectionId || null,
				};
				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelMessFeeItemMappingSetup",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetStudentLstForMess();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};


	$scope.GetFeeMappingBatchWise = function () {

		if ($scope.BatchWise.BatchId > 0 && $scope.BatchWise.FacultyId > 0)
		{

		} else {
			return;
		}
		 
		$scope.loadingstatus = "running";
		showPleaseWait();

		$scope.FeeMappingBatchsWise = [];
		$scope.BatchFeeItemList = [];

		var para = {
			BatchId: $scope.BatchWise.BatchId,
			FacultyId: $scope.BatchWise.FacultyId
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetFeeMappingBatchWise",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dataColl = mx(res.data.Data);

				var query = dataColl.groupBy(t => ({ ClassId: t.ClassId,SemesterId: t.SemesterId,ClassYearId:t.ClassYearId }));
				 
				angular.forEach(query, function (q) {
					var fst = q.elements[0];
					var beData = {
						BatchId: para.BatchId,
						FacultyId: para.FacultyId,
						ClassId: q.key.ClassId,
						SemesterId: q.key.SemesterId,
						ClassYearId: q.key.ClassYearId,
						ClassName: fst.ClassName,
						Semester: fst.Semester,
						ClassYear: fst.ClassYear,
						FeeItemColl:[],
					};
					  

					
					 
					var subQuery = mx(q.elements).orderBy(t => ({ SNo: t.FeeItemSNo, Name: t.FeeItemName, OneTimeApplicable: t.OneTimeApplicable }));
					angular.forEach(subQuery, function (sq) {
						var feeDet = {
							BatchId: para.BatchId,
							FacultyId: para.FacultyId,
							ClassId: q.key.ClassId,
							SemesterId: q.key.SemesterId,
							ClassYearId: q.key.ClassYearId,
							FeeItemId: sq.FeeItemId,
							Rate: sq.Rate,
							MonthColl:[],
							MonthDetailsColl: [],
							OneTimeApplicable: sq.OneTimeApplicable
						};

						angular.forEach($scope.MonthList, function (mn) {
							feeDet.MonthDetailsColl.push({
								id: mn.id,
								text: mn.text,
								IsChecked: false,
								MonthId: mn.id,
								DueDate:null
							});
						});

						if (sq.MonthColl && sq.MonthColl.length > 0) {
							var mcoll = mx(sq.MonthColl);
							angular.forEach(feeDet.MonthDetailsColl, function (md) {

								var findM = mcoll.firstOrDefault(p1 => p1.MonthId == md.id);
								if (findM)
									md.Include = true;
								else
									md.Include = false;
							});
                        }
						

						beData.FeeItemColl.push(feeDet);
					});

					if ($scope.BatchFeeItemList.length == 0) {
						angular.forEach(subQuery, function (sq) {							 
							$scope.BatchFeeItemList.push({
								FeeItemId: sq.FeeItemId,
								Rate: sq.Rate,
								Name: sq.FeeItemName,
								OneTimeApplicable: sq.OneTimeApplicable
							});
						}); 
                    }
					$scope.FeeMappingBatchsWise.push(beData);
				});
	 

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.SaveUpdateFeeMappingBatchWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataColl = [];
		angular.forEach($scope.FeeMappingBatchsWise, function (fm) {
			angular.forEach(fm.FeeItemColl, function (fl) {

				if (!fl.Rate || fl.Rate == null)
					fl.Rate = 0;

				fl.MonthColl = [];

				angular.forEach(fl.MonthDetailsColl, function (mnd) {
					if (mnd.Include == true) {
						fl.MonthColl.push({
							MonthId: mnd.MonthId,
							DueDate:mnd.DueDate
						});
                    }
				});

				dataColl.push(fl);
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveFeeMappingBatchWise",
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

	$scope.DelFeeMappingBatchWise = function () {


		if ($scope.BatchWise.BatchId > 0 && $scope.BatchWise.FacultyId > 0) {

		} else {
			return;
		}
		  
		var para = {
			BatchId: $scope.BatchWise.BatchId,
			FacultyId: $scope.BatchWise.FacultyId
		};

		Swal.fire({
			title: 'Do you want to delete the current data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelFeeMappingBatchWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetFeeMappingBatchWise();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.CurMonthDetailsColl_Class = [];
	$scope.CurClassCell = {};
	$scope.ShowMonthSelectionForClass = function (dt) {
		$scope.CurClassCell = dt;
		$scope.chkAll_Class = false;
		$scope.CurMonthDetailsColl_Class = [];
		GlobalServices.getAcademicMonthList(null, dt.ClassId).then(function (resAM) {
			var MonthList = [];
			angular.forEach(resAM.data.Data, function (m) {
				MonthList.push({ id: m.NM, text: m.MonthName });
			});

			if (!dt.MonthDetailsColl || dt.MonthDetailsColl.length == 0) {
				dt.MonthDetailsColl = [];

				angular.forEach(MonthList, function (mn) {
					dt.MonthDetailsColl.push({
						id: mn.id,
						text: mn.text,
						IsChecked: false,
						MonthId: mn.id,
						DueDate: null
					});
				});
			}
			 
			if (dt.MonthColl && dt.MonthColl.length > 0) {
				var mcoll = mx(dt.MonthColl);
				angular.forEach(dt.MonthDetailsColl, function (md) {

					var findM = mcoll.firstOrDefault(p1 => p1.MonthId == md.id);
					if (findM)
						md.Include = true;
					else
						md.Include = false;
				});
			}

			$scope.CurMonthDetailsColl_Class = dt.MonthDetailsColl;
			$('#modal-custom-month-classwise').modal('show');
		});
		
	};
	$scope.CheckedAll_Class = function () {
		angular.forEach($scope.CurMonthDetailsColl_Class, function (md) {
			md.Include = $scope.chkAll_Class;
		});
	};

	//$scope.ClassWiseMonthOk = function () {
	//	var fl = $scope.CurClassCell;
	//	fl.MonthColl = [];

	//	angular.forEach(fl.MonthDetailsColl, function (mnd) {
	//		if (mnd.Include == true) {
	//			fl.MonthColl.push({
	//				MonthId: mnd.MonthId,
	//				DueDate: mnd.DueDate
	//			});
	//		}
	//	});
	//	$('#modal-custom-month-classwise').modal('hide');
    //   }

	$scope.ClassWiseMonthOk = function () {
		var fl = $scope.CurClassCell;
		fl.MonthColl = [];
		var selectedMonth = 0;
		angular.forEach(fl.MonthDetailsColl, function (mnd) {
			if (mnd.Include == true) {
				selectedMonth++;
				fl.MonthColl.push({
					MonthId: mnd.MonthId,
					DueDate: mnd.DueDate
				});
			}
		});
		if (fl.OneTimeApplicable && selectedMonth > 1) {
			Swal.fire('This fee item is One Time Applicable. Please select only one month.');
			return;
		}
		$('#modal-custom-month-classwise').modal('hide');
	}
	 
	$scope.CurMonthDetailsColl_Batch = [];
	$scope.ShowMonthSelectionForBatch = function (dt) {
		$scope.CurBatchCell = dt;
		$scope.CurMonthDetailsColl_Batch = dt.MonthDetailsColl;
		$('#modal-custom-month-batch').modal('show');
	};
	$scope.CheckedAll_Batch = function () {
		angular.forEach($scope.CurMonthDetailsColl_Batch, function (md) {
			md.Include = $scope.chkAll_Batch;
		});
	};

	$scope.GetFeeMappingClassWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.FeeMappingClassWise = [];

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAllFeeMappingClassWise",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data)
			{
				var dataColl = mx(res.data.Data);

				angular.forEach($scope.ClassList.ClassList, function (cl) {

					var findCl = $scope.AllClassList.firstOrDefault(p1 => p1.ClassId == cl.ClassId);

					var beData = {
						ClassId: cl.ClassId,
						ClassName: cl.Name,
						MonthWise: (findCl ? findCl.ActiveFeeMapppingMonth : false),
						FeeItemColl:[]
					};

				/*	angular.forEach($scope.FeeItemListForMapping, function (fi) {*/
					angular.forEach($scope.FeeItemListForMappingCW, function (fi) {
						var find = dataColl.firstOrDefault(p1 => p1.ClassId == cl.ClassId && p1.FeeItemId == fi.FeeItemId);
						var item = {
							ClassId: cl.ClassId,
							ClassName: cl.Name,
							FeeItemId: fi.FeeItemId,
							FeeItemName: fi.Name,
							Rate: find ? find.Rate : 0,
							MonthColl: find ? find.MonthColl : [],
							MonthDetailsColl: [],
							MonthWise: (findCl ? findCl.ActiveFeeMapppingMonth : false),
							OneTimeApplicable: fi.OneTimeApplicable
						};
						 
						beData.FeeItemColl.push(item);
					});

					$scope.FeeMappingClassWise.push(beData);

				});


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}
		
	$scope.SaveUpdateFeeMappingClassWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataColl = [];
		angular.forEach($scope.FeeMappingClassWise, function (fm) {
			angular.forEach(fm.FeeItemColl, function (fl) {

				if (!fl.Rate || fl.Rate == null)
					fl.Rate = 0;

				//fl.MonthColl = [];

				//angular.forEach(fl.MonthDetailsColl, function (mnd) {
				//	if (mnd.Include == true) {
				//		fl.MonthColl.push({
				//			MonthId: mnd.MonthId,
				//			DueDate: mnd.DueDate
				//		});
				//	}
				//});

				dataColl.push(fl);
			});
		});
			
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveFeeMappingClassWise",
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
				$scope.ClearFeeItem();
				$scope.GetAllFeeItemList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.DelFeeMappingClassWise = function () {
	
		Swal.fire({
			title: 'Do you want to delete the current data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelFeeMappingClassWise",
					dataType: "json",
					//data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetFeeMappingClassWise();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.GetFeeMappingMediumWise = function () {


		$scope.FeeMappingMediumWise = [];

		if (!$scope.selectedMedium || $scope.selectedMedium == '0')
			return;

		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			MediumId: $scope.selectedMedium.MediumId
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAllFeeMappingMediumWise",
			dataType: "json",
			data: para
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dataColl = mx(res.data.Data);

				angular.forEach($scope.ClassList.ClassList, function (cl) {
					var beData = {
						ClassId: cl.ClassId,
						ClassName: cl.Name,
						FeeItemColl: []
					};

					/*angular.forEach($scope.FeeItemListForMapping, function (fi) {*/
					angular.forEach($scope.FeeItemListForMappingCW, function (fi) {
						var find = dataColl.firstOrDefault(p1 => p1.ClassId == cl.ClassId && p1.FeeItemId == fi.FeeItemId);
						var item = {
							MediumId: para.MediumId,
							ClassId: cl.ClassId,
							ClassName: cl.Name,
							FeeItemId: fi.FeeItemId,
							FeeItemName: fi.Name,
							Rate: find ? find.Rate : 0
						};
						beData.FeeItemColl.push(item);
					});

					$scope.FeeMappingMediumWise.push(beData);

				});


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.SaveUpdateFeeMappingMediumWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataColl = [];
		angular.forEach($scope.FeeMappingMediumWise, function (fm) {
			angular.forEach(fm.FeeItemColl, function (fl) {

				if (!fl.Rate || fl.Rate == null)
					fl.Rate = 0;

				dataColl.push(fl);
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveFeeMappingMediumWise",
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

	$scope.DelFeeMappingMediumWise = function () {

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
					MediumId: $scope.selectedMedium.MediumId
				};
				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelFeeMappingMediumWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetFeeMappingMediumWise();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.GetFeeMappingForWise = function () {
		 
		$scope.FeeMappingForWise = [];

		if (!$scope.selectedFor || $scope.selectedFor == '0')
			return;

		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			MediumId: $scope.selectedFor.id
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAllFeeMappingForWise",
			dataType: "json",
			data: para
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dataColl = mx(res.data.Data);

				angular.forEach($scope.ClassList.ClassList, function (cl) {
					var beData = {
						ClassId: cl.ClassId,
						ClassName: cl.Name,
						FeeItemColl: []
					};

					angular.forEach($scope.FeeItemListForMapping, function (fi) {

						var find = dataColl.firstOrDefault(p1 => p1.ClassId == cl.ClassId && p1.FeeItemId == fi.FeeItemId);
						var item = {
							MediumId: para.MediumId,
							ClassId: cl.ClassId,
							ClassName: cl.Name,
							FeeItemId: fi.FeeItemId,
							FeeItemName: fi.Name,
							Rate: find ? find.Rate : 0
						};
						beData.FeeItemColl.push(item);
					});

					$scope.FeeMappingForWise.push(beData);

				});


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.SaveUpdateFeeMappingForWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataColl = [];
		angular.forEach($scope.FeeMappingForWise, function (fm) {
			angular.forEach(fm.FeeItemColl, function (fl) {

				if (!fl.Rate || fl.Rate == null)
					fl.Rate = 0;

				dataColl.push(fl);
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveFeeMappingForWise",
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

	$scope.DelFeeMappingForWise = function () {

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
					MediumId: $scope.selectedFor.id
				};
				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelFeeMappingForWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetFeeMappingForWise();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};






	$scope.GetFeeMappingStudentTypeWise = function () {
		
		
		$scope.FeeMappingStudentTypeWise = [];

		if (!$scope.selectedStudentType || $scope.selectedStudentType=='0')
			return;

		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			StudentTypeId:$scope.selectedStudentType.StudentTypeId
		};
		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/GetAllFeeMappingStudentTypeWise",
			dataType: "json",
			data:para
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data)
			{
				var dataColl = mx(res.data.Data);

				angular.forEach($scope.ClassList.ClassList, function (cl) {
					var beData = {
						ClassId: cl.ClassId,
						ClassName: cl.Name,
						FeeItemColl: []
					};

				/*	angular.forEach($scope.FeeItemListForMapping, function (fi) {*/
					angular.forEach($scope.FeeItemListForMappingCW, function (fi) {
						var find = dataColl.firstOrDefault(p1 => p1.ClassId == cl.ClassId && p1.FeeItemId == fi.FeeItemId);
						var item = {
							StudentTypeId:para.StudentTypeId,
							ClassId: cl.ClassId,
							ClassName: cl.Name,
							FeeItemId: fi.FeeItemId,
							FeeItemName: fi.Name,
							Rate: find ? find.Rate : 0
						};
						beData.FeeItemColl.push(item);
					});

					$scope.FeeMappingStudentTypeWise.push(beData);

				});


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.SaveUpdateFeeMappingStudentTypeWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataColl = [];
		angular.forEach($scope.FeeMappingStudentTypeWise, function (fm) {
			angular.forEach(fm.FeeItemColl, function (fl) {
				dataColl.push(fl);
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveFeeMappingStudentTypeWise",
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

	$scope.DelFeeMappingStudentTypeWise = function () {

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
					StudentTypeId: $scope.selectedStudentType.StudentTypeId
				};
				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelFeeMappingStudentTypeWise",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetFeeMappingStudentTypeWise();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.CheckedAllNotApplicable = function () {
		var val = $scope.newFeeNotApplicable.CheckAll;
		angular.forEach($scope.newFeeNotApplicable.StudentColl, function (fm) {
			fm.Include = val;
		});
	};

	$scope.GetStudentLstForFeeNotApplicable = function (semYear)
	{
		$scope.newFeeNotApplicable.StudentColl = [];

		if ($scope.newFeeNotApplicable.SelectedClass && semYear==true) {
			var findClass = $scope.AllClassList.firstOrDefault(p1 => p1.ClassId == $scope.newFeeNotApplicable.SelectedClass.ClassId);
			if (findClass) {

				$scope.newFeeNotApplicable.SelectedClass.ClassType = findClass.ClassType;

				var semQry = mx(findClass.ClassSemesterIdColl);
				var cyQry = mx(findClass.ClassYearIdColl);

				$scope.newFeeNotApplicable.SelectedClassClassYearList = [];
				$scope.newFeeNotApplicable.SelectedClassSemesterList = [];

				angular.forEach($scope.SemesterList, function (sem) {
					if (semQry.contains(sem.id)) {
						$scope.newFeeNotApplicable.SelectedClassSemesterList.push({
							id: sem.id,
							text: sem.text,
							SemesterId: sem.id,
							Name: sem.Name
						});
					}
				});

				angular.forEach($scope.ClassYearList, function (sem) {
					if (cyQry.contains(sem.id)) {
						$scope.newFeeNotApplicable.SelectedClassClassYearList.push({
							id: sem.id,
							text: sem.text,
							ClassYearId: sem.id,
							Name: sem.Name
						});
					}
				});
			}
		}


		if ($scope.newFeeNotApplicable.SelectedClass) {
			var para = {
				ClassId: $scope.newFeeNotApplicable.SelectedClass.ClassId,
				SectionId: $scope.newFeeNotApplicable.SelectedClass.SectionId,
				All: ($scope.BillingConfig.ShowLeftStudentInFeeMapping ? true : false),
				SemesterId: $scope.newFeeNotApplicable.SemesterId,
				ClassYearId:$scope.newFeeNotApplicable.ClassYearId
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
					$scope.newFeeNotApplicable.StudentColl = res.data.Data;

					if ($scope.newFeeNotApplicable.SelectedFeeItem)
					{
						var para1 = {
							ClassId: para.ClassId,
							SectionId: para.SectionId,
							SemesterId: para.SemesterId,
							ClassYearId:para.ClassYearId,
							FeeItemId: $scope.newFeeNotApplicable.SelectedFeeItem.FeeItemId
						};
						$http({
							method: 'POST',
							url: base_url + "Fee/Creation/GetAllNotApplicableFeeItem",
							dataType: "json",
							data: JSON.stringify(para1)
						}).then(function (res1) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							if (res1.data.IsSuccess && res1.data.Data)
							{
								var dataColl = [];
								angular.forEach(res1.data.Data, function (st) {
									dataColl.push(st.StudentId);
								});
								var dataColl = mx(dataColl);
								angular.forEach($scope.newFeeNotApplicable.StudentColl, function (st)
								{									
									if (dataColl.contains(st.StudentId)) {
										st.Include = true;
									} else
										st.Include = false;
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

	$scope.ClearNotApplicableFeeItem = function () {
		$scope.newFeeNotApplicable.StudentColl = [];
		$scope.newFeeNotApplicable.SelectedClass = null;
		$scope.newFeeNotApplicable.SelectedFeeItem = null;
    }
	$scope.SaveUpdateNotApplicableFeeItem = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataColl = [];
		var cId = $scope.newFeeNotApplicable.SelectedClass.ClassId;
		var sId = $scope.newFeeNotApplicable.SelectedClass.SectionId;
		var fId = $scope.newFeeNotApplicable.SelectedFeeItem.FeeItemId;

		angular.forEach($scope.newFeeNotApplicable.StudentColl, function (fm)
		{
			if (fm.Include == true) {
				var beData = {
					ClassId: cId,
					SectionId: sId,
					FeeItemId: fId,
					StudentId: fm.StudentId,
					SemesterId: fm.SemesterId,
					ClassYearId:fm.ClassYearId
				};
				dataColl.push(beData);
            }
		});

		if (dataColl.length == 0) {
			var beData = {
				ClassId: cId,
				SectionId: sId,
				SemesterId: $scope.newFeeNotApplicable.SemesterId,
				ClassYearId: $scope.newFeeNotApplicable.ClassYearId,
				FeeItemId: fId,
				StudentId: 0,				
			};
			dataColl.push(beData);
        }

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveNotApplicableFeeItem",
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

	$scope.DelNotApplicableFeeItem = function () {

		var cId = $scope.newFeeNotApplicable.SelectedClass.ClassId;
		var sId = $scope.newFeeNotApplicable.SelectedClass.SectionId;
		var fId = $scope.newFeeNotApplicable.SelectedFeeItem.FeeItemId;
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
					ClassId: cId,
					SectionId: sId,
					SemesterId: $scope.newFeeNotApplicable.SemesterId,
					ClassYearId: $scope.newFeeNotApplicable.ClassYearId,
					FeeItemId: fId	
				};
				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelNotApplicableFeeItem",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetStudentLstForFeeNotApplicable(true);
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
		$('#modal-custom-month-fee').modal('show');
	};

	$scope.CheckedAllST = function (st) {
		angular.forEach(st.MonthDetailsColl, function (md) {
			md.Include = st.chkAll;
		});
	};
	$scope.CheckedAll = function () {
		angular.forEach($scope.CurMonthDetailsColl, function (md) {
			md.Include = $scope.chkAll;
		});
	};
	$scope.GetStudentLstForFixedAmount = function () {
		$scope.newFixedStudent.StudentColl = [];
		if ($scope.newFixedStudent.SelectedClass) {
			var para = {
				ClassId: $scope.newFixedStudent.SelectedClass.ClassId,
				SectionId: $scope.newFixedStudent.SelectedClass.SectionId,
				All: ($scope.BillingConfig.ShowLeftStudentInFeeMapping ? true : false),
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
					$scope.newFixedStudent.StudentColl = res.data.Data;

					var para1 = {
						ClassId: $scope.newFixedStudent.SelectedClass.ClassId,
						SectionId: $scope.newFixedStudent.SelectedClass.SectionId
					};

					$http({
						method: 'POST',
						url: base_url + "Fee/Creation/GetAllFixedAmountStudent",
						dataType: "json",
						data: JSON.stringify(para1)
					}).then(function (res1) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						if (res1.data.IsSuccess && res1.data.Data) {
							var dataColl = mx(res1.data.Data);

							//var MonthDetailsColl = [];
							//angular.forEach($scope.MonthList, function (mn) {
							//	MonthDetailsColl.push({
							//		id: mn.id,
							//		text: mn.text,
							//		IsChecked: false
							//	});
							//});


							angular.forEach($scope.newFixedStudent.StudentColl, function (st) {

								var find = dataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
								st.Amount = find ? find.Amount : 0;
								st.Remarks = find ? find.Remarks : '';
								st.MonthIdColl = find ? find.MonthIdColl : [];
								st.ClassId = para.ClassId;
								st.SectionId = para.SectionId;
								st.MonthDetailsColl = [];

								angular.forEach($scope.MonthList, function (mn) {
									st.MonthDetailsColl.push({
										id: mn.id,
										text: mn.text,
										IsChecked: false
									});
								});

								var mcoll = mx(st.MonthIdColl);

								angular.forEach(st.MonthDetailsColl, function (md) {

									if (mcoll.contains(md.id))
										md.Include = true;
									else
										md.Include = false;
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

	$scope.ClearFixedAmountStudent = function () {
		$scope.newFixedStudent.StudentColl = [];
		$scope.newFixedStudent.SelectedClass = null;
	};

	$scope.SaveUpdateFixedAmountStudent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataColl = [];
		var cId = $scope.newFixedStudent.SelectedClass.ClassId;
		var sId = $scope.newFixedStudent.SelectedClass.SectionId;
		
		angular.forEach($scope.newFixedStudent.StudentColl, function (fm) {
			if (fm.Amount && fm.Amount > 0)
			{
				var beData = {
					ClassId: cId,
					SectionId: sId,					
					StudentId: fm.StudentId,
					Amount: (fm.Amount ? fm.Amount : 0 ),
					Remarks: fm.Remarks,
					MonthIdColl:[]
				};
				angular.forEach(fm.MonthDetailsColl, function (md) {
					if (md.Include == true)
						beData.MonthIdColl.push(md.id);
				});
				dataColl.push(beData);
			}
		});

		$http({
			method: 'POST',
			url: base_url + "Fee/Creation/SaveFixedAmountStudent",
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

	$scope.DelFixedAmountStudent = function () {

		var cId = $scope.newFixedStudent.SelectedClass.ClassId;
		var sId = $scope.newFixedStudent.SelectedClass.SectionId;
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
					ClassId: cId,
					SectionId: sId
				};
				$http({
					method: 'POST',
					url: base_url + "Fee/Creation/DelFixedAmountStudent",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);
					if (res.data.IsSuccess == true) {
						$scope.GetStudentLstForFixedAmount();
					}
				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//Added by Sureshfor sortig
	$scope.sortNotApplicable = function (keyname) {
		$scope.sortKeyNotApplicable = keyname;
		$scope.reverse1 = !$scope.reverse1;
	}

	$scope.sortExtraCurricular = function (keyname) {
		$scope.sortKeyExtraCurricular = keyname;
		$scope.reverse2 = !$scope.reverse2;
	}
	$scope.sortDayBoarders = function (keyname) {
		$scope.sortKeyDayBoarders = keyname;
		$scope.reverse3 = !$scope.reverse3;
	}
	$scope.sortFixedStudent = function (keyname) {
		$scope.sortKeyFixedStudent = keyname;
		$scope.reverse4 = !$scope.reverse4;
	}

	$scope.BatchWiseMonthOk = function () {

		var fl = $scope.CurBatchCell;
		var selectedMonth = 0;

		angular.forEach(fl.MonthDetailsColl, function (mnd) {
			if (mnd.Include == true) {
				selectedMonth++;
			}
		});

		if (fl.OneTimeApplicable && selectedMonth > 1) {
			Swal.fire('This fee item is One Time Applicable. Please select only one month.');
			return;
		}

		$('#modal-custom-month-batch').modal('hide');
	};
});