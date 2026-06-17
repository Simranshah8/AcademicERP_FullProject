
app.controller('TransportMappingController', function ($scope, $http, $timeout, $filter, GlobalServices, $translate) {
	$scope.Title = 'Transport Mapping';

	//OnClickDefault();


	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa 
	}

	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		//$scope.MonthList = GlobalServices.getMonthList();
		$scope.MonthList = [];
		//GlobalServices.getMonthListFromDB().then(function (res1) {
		//	angular.forEach(res1.data.Data, function (m) {
		//		$scope.MonthList.push({ id: m.NM, text: m.MonthName });
		//	});

		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});


		$scope.currentPages = {
			Transportmapping: 1,
		};
		
		$scope.perPage = {
			Transportmapping: GlobalServices.getPerPageRow(),
		};

		$scope.TravelTypes = [{ id: 1, text: 'PICKUP' }, { id: 2, text: 'DROP-OFF' }, { id: 3, text: 'BOTH' }]
		$scope.searchData = {
			TransportMapping: '',
		};

		$scope.newTransportMapping = {
			TransportMappingId: null,
			Mode: 'Save'
		};

		//$scope.ClassSectionList = [];
		//GlobalServices.getClassSectionList().then(function (res) {
		//	$scope.ClassSectionList = res.data.Data;
		//}, function (reason) {
		//	Swal.fire('Failed' + reason);
		//});

		$scope.ClassList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
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


		$scope.TransportPointList = [];
		$scope.TransportPointQry = [];
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetAllTransportPointList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TransportPointList = res.data.Data;
				$scope.TransportPointQry = mx(res.data.Data);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.TransportRouteList = [];
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetAllTransportRouteList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TransportRouteList = mx(res.data.Data);

			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		//$scope.GetAllTransportMappingList();
	};

	$scope.CurMonthDetailsColl = [];
	$scope.ShowMonthSelection = function (dt) {

		$scope.CurMonthDetailsColl = dt.MonthDetailsColl;
		$('#modal-custom-month-fee').modal('show');
	};

	$scope.CheckedAll = function (st) {
		if (st.ForAll == true) {
			angular.forEach(st.MonthDetailsColl, function (md) {
				md.Rate = st.Rate;
				md.DiscountAmt = st.DiscountAmt;
				md.DiscountPer = st.DiscountPer;
				md.PayableAmt = st.PayableAmt;
			});
		} else {
			angular.forEach(st.MonthDetailsColl, function (md) {
				md.Rate = 0;
				md.DiscountAmt = 0;
				md.DiscountPer = 0;
				md.PayableAmt = 0;
			});
		}
	};

	$scope.ChangeTransportPoint = function (st, col) {
		var selectPoint = $scope.TransportPointQry.firstOrDefault(p1 => p1.PointId == st.PointId);
		if (selectPoint) {

			st.TransportRouteList = [];
			angular.forEach(selectPoint.RouteIdColl, function (rid) {
				var findR = $scope.TransportRouteList.firstOrDefault(p1 => p1.RouteId == rid);
				if (findR)
					st.TransportRouteList.push(findR);
			});

			if (st.TransportRouteList && st.TransportRouteList.length == 1)
				st.RouteId = st.TransportRouteList[0].RouteId;

			var rate = 0;
			if (st.TravelType) {

				if (st.TravelType == 1)
					rate = selectPoint.InRate;
				else if (st.TravelType == 2)
					rate = selectPoint.OutRate;
				else
					rate = selectPoint.BothRate;
			} else
				rate = selectPoint.BothRate;

			if (col == 1 || col == 2 || col == 3) {
				st.Rate = rate;
				st.PayableAmt = st.Rate - st.DiscountAmt;
			} else if (col == 4) {
				var disAmt = 0;
				if (st.DiscountPer > 0) {
					disAmt = (st.Rate * st.DiscountPer) / 100;
				}

				st.DiscountAmt = disAmt;
				st.PayableAmt = st.Rate - disAmt;

			} else if (col == 5) {
				var disPer = 0;
				if (st.DiscountAmt > 0) {
					disPer = (st.DiscountAmt / st.Rate) * 100;
				}

				st.DiscountPer = disPer;
				st.PayableAmt = st.Rate - st.DiscountAmt;
			}

		} else {
			st.Rate = 0;
			st.DiscountAmt = 0;
			st.DiscountPer = 0;
			st.PayableAmt = 0;

			angular.forEach(st.MonthDetailsColl, function (md) {
				md.Rate = 0;
				md.DiscountAmt = 0;
				md.DiscountPer = 0;
				md.PayableAmt = 0;
			});
		}
	}

	$scope.ChangeDetRate = function (md, col) {
		if (col == 1) {
			md.PayableAmt = md.Rate - md.DiscountAmt;
		} else if (col == 2) {
			var disAmt = 0;
			if (md.DiscountPer > 0)
				disAmt = md.Rate * md.DiscountPer / 100;

			md.DiscountAmt = disAmt;
			md.PayableAmt = md.Rate - disAmt;
		} else if (col == 3) {
			var disPer = 0;
			if (md.DiscountAmt > 0)
				disPer = md.DiscountAmt / md.Rate * 100;

			md.DiscountPer = disPer;
			md.PayableAmt = md.Rate - md.DiscountAmt;
		}
	}

	$scope.GetStudentLstForMapping = function () {
		$scope.newTransportMapping.StudentColl = [];
		if ($scope.newTransportMapping.SelectedClass) {

			var cid = 0, sid = null;

			if ($scope.newTransportMapping.SelectedClass == '0' || $scope.newTransportMapping.SelectedClass == 0) {
				cid = 0;
				sid = null;
			}
			else if ($scope.newTransportMapping.SelectedClass) {
				cid = $scope.newTransportMapping.SelectedClass.ClassId;
				sid = $scope.newTransportMapping.SelectedClass.SectionId;
            }
			GlobalServices.getAcademicMonthList(null, $scope.newTransportMapping.SelectedClass.ClassId).then(function (resAM) {
				$scope.MonthList = [];
				angular.forEach(resAM.data.Data, function (m) {
					$scope.MonthList.push({ id: m.NM, text: m.MonthYear });
				});

				
				var para = {
					ClassId: cid,
					SectionId: sid,
					BatchId: $scope.newTransportMapping.BatchId,
					ClassYearId: $scope.newTransportMapping.ClassYearId,
					SemesterId: $scope.newTransportMapping.SemesterId,
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
						$scope.newTransportMapping.StudentColl = [];
						var stColl = res.data.Data;
						angular.forEach(stColl, function (st) {
							if (st.IsLeft == false)
								$scope.newTransportMapping.StudentColl.push(st);
						});
						//$scope.newTransportMapping.StudentColl = res.data.Data;

						$http({
							method: 'POST',
							url: base_url + "Transport/Creation/GetAllTransportMapping",
							dataType: "json",
							data: JSON.stringify(para)
						}).then(function (res1) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							if (res1.data.IsSuccess && res1.data.Data) {
								var dataColl = mx(res1.data.Data);
								angular.forEach($scope.newTransportMapping.StudentColl, function (st) {

									var find = dataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
									st.PointId = find ? find.PointId : 0;
									st.RouteId = find ? find.RouteId : 0;
									st.TravelType = find ? find.TravelType : 0;
									st.Rate = find ? find.Rate : 0;
									st.DiscountPer = find ? find.DiscountPer : 0;
									st.DiscountAmt = find ? find.DiscountAmt : 0;
									st.PayableAmt = find ? find.PayableAmt : 0;
									st.MonthIdColl = find ? find.MonthIdColl : [];
									st.ClassId = para.ClassId;
									st.SectionId = para.SectionId;
									st.MonthDetailsColl = [];

									angular.forEach($scope.MonthList, function (mn) {
										st.MonthDetailsColl.push({
											id: mn.id,
											text: mn.text,
											IsChecked: false,
											Rate: 0,
											DiscountPer: 0,
											DiscountAmt: 0,
											PayableAmt: 0,
											Remarks: ''
										});
									});

									var mcoll = mx(st.MonthIdColl);

									angular.forEach(st.MonthDetailsColl, function (md) {

										var fM = mcoll.firstOrDefault(p1 => p1.MonthId == md.id);
										md.Rate = fM ? fM.Rate : 0;
										md.DiscountPer = fM ? fM.DiscountPer : 0;
										md.DiscountAmt = fM ? fM.DiscountAmt : 0;
										md.PayableAmt = fM ? fM.PayableAmt : 0;
										md.Remarks = fM ? fM.Remarks : '';
									});

									var selectPoint = $scope.TransportPointQry.firstOrDefault(p1 => p1.PointId == st.PointId);
									if (selectPoint) {
										st.TransportRouteList = [];
										angular.forEach(selectPoint.RouteIdColl, function (rid) {
											var findR = $scope.TransportRouteList.firstOrDefault(p1 => p1.RouteId == rid);
											if (findR)
												st.TransportRouteList.push(findR);
										});

										if (st.TransportRouteList && st.TransportRouteList.length == 1)
											st.RouteId = st.TransportRouteList[0].RouteId;
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

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});



		}
	}

	$scope.ClearTransportMapping = function () {
		$scope.newTransportMapping = {
			TransportMappingId: null,
			BatchId: null,
			SelectedClass: null,
			ClassYearId: null,
			SemesterId: null,
			StudentColl:[],
			Mode: 'Save'
		};

	};



	$scope.SaveUpdateTransportMapping = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var dataColl = [];
		var cId = $scope.newTransportMapping.SelectedClass.ClassId;
		var sId = $scope.newTransportMapping.SelectedClass.SectionId;

		angular.forEach($scope.newTransportMapping.StudentColl, function (fm) {
			if (fm.PointId && fm.PointId > 0) {
				var beData = {
					ClassId: cId,
					SectionId: sId,
					StudentId: fm.StudentId,
					PointId: fm.PointId,
					RouteId: fm.RouteId,
					TravelType: fm.TravelType,
					Rate: fm.Rate,
					DiscountAmt: fm.DiscountAmt,
					DiscountPer: fm.DiscountPer,
					PayableAmt: fm.PayableAmt,
					ForAll: fm.ForAll,
					MonthIdColl: []
				};

				if (!beData.DiscountAmt || beData.DiscountAmt == null)
					beData.DiscountAmt = 0;

				if (!beData.DiscountPer || beData.DiscountPer == null)
					beData.DiscountPer = 0;

				if (!beData.PayableAmt || beData.PayableAmt == null)
					beData.PayableAmt = 0;

				if (!beData.Rate || beData.Rate == null)
					beData.Rate = 0;


				angular.forEach(fm.MonthDetailsColl, function (md) {
					md.MonthId = md.id;
					if (md.Rate > 0) {

						if (!md.DiscountAmt || md.DiscountAmt == null)
							md.DiscountAmt = 0;

						if (!md.DiscountPer || md.DiscountPer == null)
							md.DiscountPer = 0;

						beData.MonthIdColl.push(md);
					}

				});
				dataColl.push(beData);
			}
		});

		if (dataColl.length == 0) {
			var beData = {
				ClassId: cId,
				SectionId: sId,
				StudentId: 0,
				PointId: 0,
				RouteId: 0,
				TravelType: 0,
				Rate: 0,
				DiscountAmt: 0,
				DiscountPer: 0,
				PayableAmt: 0,
				ForAll: true,
				MonthIdColl: []
			};
			dataColl.push(beData);
		}

		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/SaveTransportMapping",
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
	};

	//New Code added by suresh for studentwise
	$scope.SaveUpdateStudentWise = function (fm) {
		Swal.fire({
			title: 'Do you want to update ' + fm.Name + '\'s data?',
			showCancelButton: true,
			confirmButtonText: 'Update',
		}).then((result) => {
			if (result.isConfirmed) {
				$scope.loadingstatus = "running";
				showPleaseWait();

				var dataColl = [];

				var beData = {
					ClassId: 0, 
					SectionId: 0,
					StudentId: fm.StudentId,
					PointId: fm.PointId,
					RouteId: fm.RouteId,
					TravelType: fm.TravelType,
					Rate: fm.Rate,
					DiscountAmt: fm.DiscountAmt,
					DiscountPer: fm.DiscountPer,
					PayableAmt: fm.PayableAmt,
					ForAll: fm.ForAll,
					MonthIdColl: []
				};

				
				angular.forEach(fm.MonthDetailsColl, function (md) {
					md.MonthId = md.id;
					if (md.Rate > 0) {
						if (!md.DiscountAmt || md.DiscountAmt == null)
							md.DiscountAmt = 0;

						if (!md.DiscountPer || md.DiscountPer == null)
							md.DiscountPer = 0;

						beData.MonthIdColl.push(md);
					}
				});

				dataColl.push(beData);
				$http({
					method: 'POST',
					url: base_url + "Transport/Creation/SaveTransportMapping",
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
					// Hide the loading spinner and handle error
					hidePleaseWait();
					$scope.loadingstatus = "stop";
				});
			}
		});
	};



});