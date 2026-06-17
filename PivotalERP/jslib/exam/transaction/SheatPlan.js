app.controller('SheatPlanController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'Sheat Plan';
	var gSrv = GlobalServices;
	OnClickDefault();
	
	$scope.LoadData = function () {

		$('.select2').select2();

		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.perPageColl = gSrv.getPerPageList();
		//$scope.MonthList = gSrv.getMonthList();

		//--1=Class and RollNo wise, 2 = Class and SymbolNo wise, 3 = SymbolNo wise, 4 = Random, 5 = ClassWise Random
		$scope.SeatPlanAsList = [{ id: 1, text: 'Class and RollNo wise' }, { id: 2, text: 'Class and SymbolNo wise' }, { id: 3, text: 'SymbolNo wise' }, { id: 4, text: 'Random' }, { id: 5, text: 'Class Wise Random' }, { id: 6, text: 'Class and RollNo wise mixed' }, { id: 7, text: 'Class and RegdNo wise' }, { id: 8, text: 'Class and Rank wise' }, { id: 9, text: 'Section and Rank wise' },
			{ id: 10, text: 'Class and Rank wise 2-Rows' }, { id: 11, text: 'Class and Rank wise 3-Column Alternet' },
		];
		$scope.FieldNameAsList = [{ id: 1, text: 'Reg.No.' }, { id: 2, text: 'Symbol No.' }, { id: 3, text: 'Details' }];
		$scope.ExamTypeList = [];
		gSrv.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.currentPages = {
			RoomDetails: 1,
			ExamShift: 1,
			ExamWise: 1,
			ExamSeatPlan:1
		};

		$scope.searchData = {
			RoomDetails: '',
			ExamShift: '',
			ExamWise: '',
			ExamSeatPlan:''
			
		};

		$scope.perPage = {
			RoomDetails: gSrv.getPerPageRow(),
			ExamShift: gSrv.getPerPageRow(),
			ExamWise: gSrv.getPerPageRow(),
			ExamSeatPlan: gSrv.getPerPageRow(),
		
		};

		$scope.newRoomDetails = {
			RoomId: null,
			Name: '',
			RoomNo: 0,
			TotalBanch: 0,
			NoOfBanchRow: 0,
			ExamTypeId: null,
			ExamShiftId:null,
			DetailColl:[],
			Mode: 'Save'
		};

	

		$scope.newExamWise = {
			ExamShiftId: 0,
			ExamTypeId: 0,
			ClassIdColl: '',
			RoomIdColl: '',
			SeatPlanAs: 1,
			FieldNameAs:1,
			TemplatesColl:[],
			Mode: 'Generate'
		};

		$scope.newPrintSeatPlan =
		{
			ExamShiftId: null,
			ExamTypeId: null,
			RptTranId: null,
			FieldNameAs: 1,
		};

		/*$scope.ExamTypeList = [];
		gSrv.getExamTypeList().then(function (res) {
			$scope.ExamTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});*/


		$scope.GetAllRoomDetailsList();
		$scope.GetAllExamShiftList();
		$scope.ClassList = [];
		gSrv.getClassList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityExamSeatPlan + "&voucherId=0&isTran=false",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data)
				$scope.newExamWise.TemplatesColl = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GetAllExamSeatPlan();
	}

	function OnClickDefault() {

		//New  code added by suresh on 27 Poush
		document.getElementById('sheet-plan-form').style.display = "none";
		document.getElementById('add-form').style.display = "none";
		document.getElementById('sheet-plan-form').style.display = "none";
		 

		///Room Details
		document.getElementById('add-room-details').onclick = function () {
			document.getElementById('add-room-section').style.display = "none";
			document.getElementById('sheet-plan-form').style.display = "block";
			$scope.ClearRoomDetails();
		}
		document.getElementById('addroomback-btn').onclick = function () {
			document.getElementById('sheet-plan-form').style.display = "none";
			document.getElementById('add-room-section').style.display = "block";
			$scope.ClearRoomDetails();
		}

		 
		// 	//New  code added by suresh on 27 Poush
		document.getElementById('add-btn').onclick = function () {
			document.getElementById('table-section').style.display = "none";
			document.getElementById('add-form').style.display = "block";
		}
		document.getElementById('back-btn').onclick = function () {
			document.getElementById('add-form').style.display = "none";
			document.getElementById('table-section').style.display = "block";
		}
	}

		$scope.ClearRoomDetails = function () {
			$scope.newRoomDetails = {
				RoomDetailsId: null,
				Name: '',
				RoomNo: 0,
				NoOfBench: 0,
				NoOfColumns: 0,
				PerBenchStudents: 0,
				ExamTypeId: null,
				ExamShiftId: null,
				Mode: 'Save'
		};
	}
		$scope.ClearExamShift = function () {
			$scope.newExamShift = {
				ExamShiftId: null,
				Name: '',
				StartTime: '',
				EndTime: '',
				Mode: 'Save'
		};
	}
		$scope.ClearExamWise = function () {
			$scope.newExamWise = {
				ExamWiseId: null,
				Exam: '',
				ExamShift: '',
				Class: '',
				ClassId: '',
				Room: '',

				ReportTemplate: '',
				Mode: 'Generate'
		};
	}
	

	//************************* Room Details *********************************
	$scope.generateBanckRow = function () {
		$scope.newRoomDetails.DetailColl = [];
		for (var i = 1; i <= $scope.newRoomDetails.NoOfBanchRow; i++) {
			$scope.newRoomDetails.DetailColl.push({
				Banch_Row_SNo: i,
				Banch_Row_Name: '',
				NoOfBanch: 0,
				NoOfSeatsInRow:0
			});
		};
    }

	$scope.IsValidRoomDetails = function () {
		if ($scope.newRoomDetails.Name.isEmpty()) {
			Swal.fire('Please ! Enter Room Name');
			return false;
		}
		 
		return true;
	}

	$scope.SaveUpdateRoomDetails = function () {
		if ($scope.IsValidRoomDetails() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newRoomDetails.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateRoomDetails();
					}
				});
			} else
				$scope.CallSaveUpdateRoomDetails();

		}
	};

	$scope.CallSaveUpdateRoomDetails = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamRoom",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newRoomDetails }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearRoomDetails();
				$scope.GetAllRoomDetailsList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllRoomDetailsList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.RoomDetailsList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamRoomList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data)
			{
				var dataQuery = mx(res.data.Data).groupBy(p1 => ({ ExamType: p1.ExamTypeName, ExamShift: p1.ExamShiftName }));
				angular.forEach(dataQuery, function (q) {
					var newData = {
						ExamType: q.key.ExamType,
						ExamShift: q.key.ExamShift,
						DataColl: q.elements
					};
					$scope.RoomDetailsList.push(newData);
				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetRoomDetailsById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			RoomId: refData.RoomId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamRoomById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$timeout(function () {
					$scope.newRoomDetails = res.data.Data;
					$scope.newRoomDetails.Mode = 'Modify';

					document.getElementById('add-room-section').style.display = "none";
					document.getElementById('sheet-plan-form').style.display = "block";
				});				

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelRoomDetailsById = function (refData) {

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
					RoomId: refData.RoomId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelExamRoom",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllRoomDetailsList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Exam Shift *********************************

	$scope.IsValidExamShift = function () {
		if ($scope.newExamShift.Name.isEmpty()) {
			Swal.fire('Please ! Enter Exam Shift Name');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateExamShift = function () {
		if ($scope.IsValidExamShift() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamShift.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamShift();
					}
				});
			} else
				$scope.CallSaveUpdateExamShift();

		}
	};

	$scope.CallSaveUpdateExamShift = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();


		if ($scope.newExamShift.StartTime_TMP)
			$scope.newExamShift.StartTime = moment($scope.newExamShift.StartTime_TMP).format("HH:mm");
		else
			$scope.newExamShift.EndTime = null;

		if ($scope.newExamShift.EndTime_TMP)
			$scope.newExamShift.EndTime = moment($scope.newExamShift.EndTime_TMP).format("HH:mm");
		else
			$scope.newExamShift.EndTime = null;

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveExamShift",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamShift }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExamShift();
				$scope.GetAllExamShiftList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllExamShiftList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamShiftList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamShiftList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamShiftList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetExamShiftById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ExamShiftId: refData.ExamShiftId
		};

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetExamShiftById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newExamShift = res.data.Data;
				$scope.newExamShift.Mode = 'Modify';

				if ($scope.newExamShift.StartTime)
					$scope.newExamShift.StartTime_TMP = new Date(moment().format("yyyy-MM-DD") + " " + $scope.newExamShift.StartTime);

				if ($scope.newExamShift.EndTime)
					$scope.newExamShift.EndTime_TMP = new Date(moment().format("yyyy-MM-DD") + " " + $scope.newExamShift.EndTime);

				document.getElementById('examshift-section').style.display = "none";
				document.getElementById('examshift-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.DelExamShiftById = function (refData) {

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
					ExamShiftId: refData.ExamShiftId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelExamShift",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllExamShiftList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//************************* Exam Wise *********************************

	$scope.IsValidExamWise = function () {
		if ($scope.newExamWise.Exam.isEmpty()) {
			Swal.fire('Please ! Select Exam');
			return false;
		}

		return true;
	}

	$scope.SaveUpdateExamWise = function () {
		if ($scope.IsValidExamWise() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newExamWise.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateExamWise();
					}
				});
			} else
				$scope.CallSaveUpdateExamWise();

		}
	};

	$scope.CallSaveUpdateExamWise = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/SaveExamWise",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newExamWise }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearExamWise();
				$scope.GetAllExamWiseList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	}

	$scope.GetAllExamWiseList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.ExamWiseList = [];

		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllExamWiseList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.ExamWiseList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	$scope.GetRoomListForSeatPlan = function () {
		$scope.VacantRoomList = [];

		if ($scope.newExamWise.ExamShiftId > 0 && $scope.newExamWise.ExamTypeId > 0) {
			var para =
			{
				ExamShiftId: $scope.newExamWise.ExamShiftId,
				ExamTypeId: $scope.newExamWise.ExamTypeId
			};
			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetExamRoomList",
				dataType: "json",
				data:JSON.stringify(para)
			}).then(function (res)
			{
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.VacantRoomList = res.data.Data;

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire('Failed' + reason);
			});
        } 
	}

	$scope.GetRoomSeatDetails = function () {
		$scope.newExamWise.RoomBanchRows = [];
		$scope.newExamWise.StudentList = [];
		if ($scope.newExamWise.ExamShiftId > 0 && $scope.newExamWise.ExamTypeId > 0 && $scope.newExamWise.RoomId > 0) {

			$scope.loadingstatus = "running";
			showPleaseWait();

			var para1 = {
				RoomId: $scope.newExamWise.RoomId
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetExamRoomById",
				dataType: "json",
				data: JSON.stringify(para1)
			}).then(function (resR) {
				if (resR.data.IsSuccess && resR.data.Data) {
					var selectedRoom = resR.data.Data;

					var para =
					{
						ExamShiftId: $scope.newExamWise.ExamShiftId,
						ExamTypeId: $scope.newExamWise.ExamTypeId,
						RoomId: $scope.newExamWise.RoomId,
						SeatPlanAs: $scope.newExamWise.SeatPlanAs
					};
					 
					$http({
						method: 'POST',
						url: base_url + "Exam/Transaction/GeRoomSeatDetails",
						dataType: "json",
						data: JSON.stringify(para)
					}).then(function (res) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						if (res.data.IsSuccess && res.data.Data) {
							var VacantSeatList = mx(res.data.Data);
							
							if (VacantSeatList.count() > 0) {

								$timeout(function () {
									$http({
										method: 'POST',
										url: base_url + "Exam/Transaction/GetClassSectionForSeatPlan",
										dataType: "json",
										data: JSON.stringify(para)
									}).then(function (res1) {
										if (res1.data.IsSuccess && res1.data.Data) {
											$scope.newExamWise.ClassSectionList = res1.data.Data.ClassList;
											$scope.newExamWise.StudentList = mx(res1.data.Data.StudentList);

											$timeout(function () {
												for (var r = 0; r < selectedRoom.NoOfBanchRow; r++) {
													var findRow = VacantSeatList.where(p1 => p1.Banch_Row_SNo == (r + 1));
													if (findRow && findRow.count() > 0) {

														var banchRowDet = selectedRoom.DetailColl[r];
														var rowDetColl = [];

														for (var rc = 0; rc < banchRowDet.NoOfBanch; rc++) {

															var rowDet = {
																RowId: r,
																ColumnColl: []
															};

															for (var c = 0; c < banchRowDet.NoOfSeatsInRow; c++) {

																var findRC = findRow.firstOrDefault(p1 => p1.Seat_Col == (c + 1) && p1.BanchNo == (rc + 1));
																if (findRC) {

																	if (findRC.StudentDet && findRC.StudentDet.length > 0) {
																		rowDet.ColumnColl.push({
																			BanchNo: (rc + 1),
																			Seat_Col: (c + 1),
																			CellName: findRC.StudentDet
																		});
																	} else {
																		var colDet = {
																			ExamTypeId: para.ExamTypeId,
																			ExamShiftId: para.ExamShiftId,
																			RoomId: findRC.RoomId,
																			Banch_Row_SNo: findRC.Banch_Row_SNo,
																			BanchNo: findRC.BanchNo,
																			Seat_SNo: findRC.SNo,
																			Seat_Col: findRC.Seat_Col,
																			CellName: findRC.BanchNo + ',' + findRC.Seat_Col,
																			Student: null
																		};
																		rowDet.ColumnColl.push(colDet);
                                                                    }
																	
																}
																else {
																	rowDet.ColumnColl.push({
																		BanchNo: (rc + 1),
																		Seat_Col: (c + 1),
																		CellName:'-'
																	});
                                                                }
																	
															}

															if (rowDet.ColumnColl.length > 0)
																rowDetColl.push(rowDet);
														}

														$scope.newExamWise.RoomBanchRows.push(
															{
																ColumnName: banchRowDet.Banch_Row_Name,
																NoOfSeatsInRow: banchRowDet.NoOfSeatsInRow,
																RowColumnColl: rowDetColl,
																ColumnColl: rowDetColl[0].ColumnColl,
																RowId:r+1
															}
														);
													}
												}
											});

										}

									}, function (reason) {
										Swal.fire('Failed' + reason);
									});
								});
								
								
							}

						} else {
							Swal.fire(res.data.ResponseMSG);
						}

					}, function (reason) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						Swal.fire('Failed' + reason);
					});

				} 
			}, function (reason) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire('Failed' + reason);
			});	
		}
	}

	$scope.SaveSeatPlan = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var tmpDataColl = [];
		angular.forEach($scope.newExamWise.RoomBanchRows, function (br) {
			angular.forEach(br.RowColumnColl, function (rc) {
				angular.forEach(rc.ColumnColl, function (cc) {
					if (cc.StudentId > 0 && cc.ExamTypeId>0 && cc.ExamShiftId>0 && cc.RoomId>0) {
						tmpDataColl.push(cc);
					} 
				});
			});
		});

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/SaveSeatPlan",
			dataType: "json",
			data: JSON.stringify(tmpDataColl)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess == true) {
				$scope.newExamWise.RoomId = null;
				$scope.newExamWise.RoomBanchRows = [];
				$scope.newExamWise.StudentList = [];

				$timeout(function () {
					$scope.GetRoomListForSeatPlan();
				});
            }
			Swal.fire(res.data.ResponseMSG);

		}, function (reason) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			Swal.fire('Failed' + reason);
		});

	};
	$scope.SeatAssign = function (selectedClass, row, col,classRow) {

		var studentIdColl = [];
		angular.forEach($scope.newExamWise.RoomBanchRows, function (br) {
			angular.forEach(br.RowColumnColl, function (rc) {
				angular.forEach(rc.ColumnColl, function (cc) {
					if (cc.StudentId > 0 && (row == br.RowId && col == cc.Seat_Col)) {
					}
					else if(cc.StudentId || cc.StudentId>0) {
						studentIdColl.push(cc.StudentId);
                    }
				});
			});
		});
		studentIdColl = mx(studentIdColl);

		var findStudentQry = null;
		if ($scope.newExamWise.SeatPlanAs == 8 || $scope.newExamWise.SeatPlanAs == 10 || $scope.newExamWise.SeatPlanAs == 11)
			findStudentQry = $scope.newExamWise.StudentList.where(p1 => p1.ClassId == selectedClass.ClassId);
		else {
			if (selectedClass.SectionId > 0) {
				findStudentQry = $scope.newExamWise.StudentList.where(p1 => p1.ClassId == selectedClass.ClassId && p1.SectionId == selectedClass.SectionId);
			} else {
				findStudentQry = $scope.newExamWise.StudentList.where(p1 => p1.ClassId == selectedClass.ClassId);
            }

        }
			

		if (findStudentQry && findStudentQry.count() > 0) {

			var findStudentColl = [];
			angular.forEach(findStudentQry, function (st) {
				if (studentIdColl.contains(st.StudentId) == false)
					findStudentColl.push(st);
			});

			$timeout(function () {
				var totalStudent = findStudentColl.length;
				var stInd = 0;
				var findRow = mx($scope.newExamWise.RoomBanchRows).firstOrDefault(p1 => p1.RowId == row);
				if (findRow)
				{
					if ($scope.newExamWise.SeatPlanAs == 10) {

						angular.forEach(findRow.RowColumnColl, function (rc) {

							angular.forEach(rc.ColumnColl, function (cc) {
								
								if (cc.ExamTypeId > 0 && cc.RoomId > 0 && cc.ExamShiftId > 0 && col == cc.Seat_Col && ((classRow == 1 && cc.BanchNo % 2 == 1) || (classRow == 2 && cc.BanchNo % 2 == 0) ))
								{									 
									var findST = findStudentColl[stInd];
									if (findST) {
										cc.StudentId = findST.StudentId;
										cc.StudentDet = findST.Name + ' ' + findST.SectionName + ' ' + findST.RankInClass;
										stInd++;
									}
								}
							});
						});

					} else if ($scope.newExamWise.SeatPlanAs == 11) {

						angular.forEach(findRow.RowColumnColl, function (rc) {
							angular.forEach(rc.ColumnColl, function (cc) {
								if (cc.ExamTypeId > 0 && cc.RoomId > 0 && cc.ExamShiftId > 0 && col == cc.Seat_Col && ((col == 1 && cc.BanchNo % 2 == 1) || (col == 2 && cc.BanchNo % 2 == 0) || (col == 3 && cc.BanchNo % 2 == 1) )) {
									var findST = findStudentColl[stInd];
									if (findST) {
										cc.StudentId = findST.StudentId;
										cc.StudentDet = findST.Name + ' ' + findST.SectionName + ' ' + findST.RankInClass;
										stInd++;
									}
								}
							});
						});

					}
					else {
						angular.forEach(findRow.RowColumnColl, function (rc) {
							angular.forEach(rc.ColumnColl, function (cc) {
								if (cc.ExamTypeId > 0 && cc.RoomId > 0 && cc.ExamShiftId > 0 && col == cc.Seat_Col) {
									var findST = findStudentColl[stInd];
									if (findST) {
										cc.StudentId = findST.StudentId;

										if (!$scope.newExamWise.SeatPlanAs || $scope.newExamWise.SeatPlanAs == 1 || $scope.newExamWise.SeatPlanAs == 4 || $scope.newExamWise.SeatPlanAs == 5 || $scope.newExamWise.SeatPlanAs == 6)
											cc.StudentDet = findST.Name + ' ' + findST.RollNo;
										else if ($scope.newExamWise.SeatPlanAs == 2 || $scope.newExamWise.SeatPlanAs == 3)
											cc.StudentDet = findST.Name + ' ' + findST.SymbolNo;
										else if ($scope.newExamWise.SeatPlanAs == 7)
											cc.StudentDet = findST.Name + ' ' + findST.RegNo;
										else if ($scope.newExamWise.SeatPlanAs == 8 || $scope.newExamWise.SeatPlanAs == 10 || $scope.newExamWise.SeatPlanAs == 11)
											cc.StudentDet = findST.Name + ' ' + findST.SectionName + ' ' + findST.RankInClass;
										else if ($scope.newExamWise.SeatPlanAs == 9)
											cc.StudentDet = findST.Name + ' ' + findST.RankInSection;
										stInd++;
									}
								}
							});
						});
                    }
					
				}
			});

		}
	};

	$scope.GetAllExamSeatPlan = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.AllSeatPlanList = [];

		$http({
			method: 'POST',
			url: base_url + "Exam/Transaction/GetAllExamSeatPlan",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				var dataColl = mx(res.data.Data);
				  
				var query = dataColl.groupBy(t => ({ ExamTypeName: t.ExamTypeName,ExamShiftName:t.ExamShiftName }));
				var sno = 1;
				angular.forEach(query, function (q) {
					pel = q.elements[0];
					var beData = {
						SNo: sno,
						ExamTypeId: pel.ExamTypeId,
						ExamTypeName: q.key.ExamTypeName,
						ExamShiftName: q.key.ExamShiftName,
						ExamShiftId:pel.ExamShiftId,
						DataColl: []
					};

					var subQry1 = mx(q.elements).groupBy(t => t.ExamShiftId && t.RoomId).toArray();
					angular.forEach(subQry1, function (sq) {
						var pel = sq.elements[0];
						beData.DataColl.push(
							{
								ExamTypeId: pel.ExamTypeId,
								ExamShiftId: pel.ExamShiftId,
								RoomId: pel.RoomId,
								ExamTypeName: pel.ExamTypeName,
								ExamShiftName: pel.ExamShiftName,
								RoomName: pel.RoomName,
								UserName: pel.UserName,
								TotalBanch: pel.TotalBanch,
								LogDateTime_AD: pel.LogDateTime_AD,
								LogDateTime_BS: pel.LogDateTime_BS,
								TotalSeat: pel.TotalSeat,
								NoOfSeatAlloted: mx(sq.elements).sum(p1 => p1.NoOfSeatAlloted),
								ClassColl: sq.elements
							});
					});


					//var subQuery = mx(q.elements).groupBy(t => t.ClassSectionName).toArray();
					//angular.forEach(subQuery, function (sq) {
					//	beData.DataColl.push(
					//		{
					//			ExamTypeId: pel.ExamTypeId,
					//			ExamShiftId: pel.ExamShiftId,
					//			RoomId: pel.RoomId,
					//			ExamTypeName: pel.ExamTypeName,
					//			ExamShiftName: pel.ExamShiftName,
					//			RoomName: pel.RoomName,
					//			UserName: pel.UserName,
					//			TotalBanch: pel.TotalBanch,
					//			LogDateTime_AD: pel.LogDateTime_AD,
					//			LogDateTime_BS:pel.LogDateTime_BS,
					//			TotalSeat: mx(sq.elements).sum(p1 => p1.TotalSeat),
					//			NoOfSeatAlloted: mx(sq.elements).sum(p1 => p1.NoOfSeatAlloted),
					//			ClassColl:sq.elements
					//		});
					//});
					sno++;
					$scope.AllSeatPlanList.push(beData);
				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GenerateSeatPlan = function () {

		if ($scope.newExamWise.ExamShiftId > 0 && $scope.newExamWise.ExamTypeId > 0 && $scope.newExamWise.ClassIdColl && $scope.newExamWise.RoomIdColl) {
			var para = {
				ExamTypeId: $scope.newExamWise.ExamTypeId,
				ExamShiftId: $scope.newExamWise.ExamShiftId,				
				ClassIdColl: $scope.newExamWise.ClassIdColl.toString(),
				RoomIdColl: $scope.newExamWise.RoomIdColl.toString(),
				SeatPlanAs: $scope.newExamWise.SeatPlanAs
			};

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GenerateSeatPlan",
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
		
	};

	$scope.DelSeatPlan = function (beDet) {

		Swal.fire({
			title: 'Do you want to delete the selected data?',
			showCancelButton: true,
			confirmButtonText: 'Delete',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				var para = {
					ExamShiftId: beDet.ExamShiftId,
					ExamTypeId: beDet.ExamTypeId,
					RoomId: beDet.RoomId
				};

				$http({
					method: 'POST',
					url: base_url + "Exam/Transaction/DelSeatPlan",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {

					hidePleaseWait();
					$scope.loadingstatus = "stop";

					Swal.fire(res.data.ResponseMSG);

					if (res.data.IsSuccess == true)
						$scope.GetAllExamSeatPlan();

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


		
    }

	$scope.GetRoomListForPrintSeatPlan = function () {
		$scope.VacantRoomListSP = [];

		if ($scope.newPrintSeatPlan.ExamShiftId > 0 && $scope.newPrintSeatPlan.ExamTypeId > 0) {
			var para =
			{
				ExamShiftId: $scope.newPrintSeatPlan.ExamShiftId,
				ExamTypeId: $scope.newPrintSeatPlan.ExamTypeId
			};
			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Exam/Transaction/GetExamRoomList",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					$scope.VacantRoomListSP = res.data.Data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				Swal.fire('Failed' + reason);
			});
		}
	}

	$scope.PrintExamSeatPlan = function () {
		if ($scope.newPrintSeatPlan.ExamShiftId>0 && $scope.newPrintSeatPlan.ExamTypeId>0 && $scope.newPrintSeatPlan.RptTranId>0) {

			var EntityId = entityExamSeatPlan;

			var rptPara = {
				ExamTypeId: $scope.newPrintSeatPlan.ExamTypeId,
				ExamShiftId: $scope.newPrintSeatPlan.ExamShiftId,
				rptTranId: $scope.newPrintSeatPlan.RptTranId,
				FieldNameAs: $scope.newPrintSeatPlan.FieldNameAs,
				RoomIdColl: ($scope.newPrintSeatPlan.RoomIdColl ? $scope.newPrintSeatPlan.RoomIdColl.toString() : '')
			};
			if (rptPara.RoomIdColl == '0')
				rptPara.RoomIdColl = '';

			var paraQuery = param(rptPara);

			$scope.loadingstatus = 'running';
			document.getElementById("frmRptTabulation").src = '';
			document.getElementById("frmRptTabulation").style.width = '100%';
			document.getElementById("frmRptTabulation").style.height = '1300px';
			document.getElementById("frmRptTabulation").style.visibility = 'visible';
			document.getElementById("frmRptTabulation").src = base_url + "Exam/Transaction/RdlExamSeatPlan?" + paraQuery;

		}

	};

	$scope.Print = function (tmpDet) {
		$http({
			method: 'GET',
			url: base_url + "ReportEngine/GetReportTemplates?entityId=" + entityExamSeatPlan + "&voucherId=0&isTran=false",
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
											print = true;

											var rptPara = {
												ExamTypeId: tmpDet.ExamTypeId,
												ExamShiftId: tmpDet.ExamShiftId,
												rptTranId: rptTranId,
												FieldNameAs: tmpDet.FieldNameAs,
												RoomIdColl: tmpDet.RoomId
											};
											var paraQuery = param(rptPara);

											$scope.loadingstatus = 'running';
											document.getElementById("frmRpt").src = '';
											document.getElementById("frmRpt").style.width = '100%';
											document.getElementById("frmRpt").style.height = '1300px';
											document.getElementById("frmRpt").style.visibility = 'visible';
											document.getElementById("frmRpt").src = base_url + "Exam/Transaction/RdlExamSeatPlan?" + paraQuery;

											$('#FrmPrintReport').modal('show');
										}

									} else {
										resolve('You need to select:)')
									}
								})
							}
						})
					}

					if (rptTranId > 0 && print == false) {
						var rptPara = {
							ExamTypeId: tmpDet.ExamTypeId,
							ExamShiftId: tmpDet.ExamShiftId,
							rptTranId: rptTranId,
							FieldNameAs: tmpDet.FieldNameAs,
							RoomIdColl: tmpDet.RoomId
						};
						var paraQuery = param(rptPara);

						$scope.loadingstatus = 'running';
						document.getElementById("frmRpt").src = '';
						document.getElementById("frmRpt").style.width = '100%';
						document.getElementById("frmRpt").style.height = '1300px';
						document.getElementById("frmRpt").style.visibility = 'visible';
						document.getElementById("frmRpt").src = base_url + "Exam/Transaction/RdlExamSeatPlan?" + paraQuery;

						$('#FrmPrintReport').modal('show');
					}

				} else
					Swal.fire('No Templates found for print');
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

	$scope.ClearNotice = function () {

		$scope.newNotice = {
			FilterStudentOnly: true,
			Title: '',
			Description: ''
		};

		$scope.newSMS = {
			Description: ''
		};
		$('input[type=file]').val('');
		$('#modal-xl').modal('hide');
		 
	}

	$scope.SelectExamSeatPlan = null;
	$scope.SendNotificationExamSeatPlan = function (refData)
	{
		$scope.SelectExamSeatPlan = refData;

		$scope.ClearNotice();

		Swal.fire({
			title: 'Do you want to Send Notification To the student?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {

				var para1 = {
					EntityId: entityStudentForSMS,
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

		var contentPath = '';
		$timeout(function () {

			$http({
				method: 'POST',
				url: base_url + "Global/UploadAttachments",
				headers: { 'Content-Type': undefined },

				transformRequest: function (data) {

					var formData = new FormData();
					if (data.files) {
						for (var i = 0; i < data.files.length; i++) {
							formData.append("file" + i, data.files[i]);
						}
					}

					return formData;
				},
				data: { files: $scope.newNotice.AttachmentColl }
			}).then(function (res) {

				$scope.loadingstatus = "stop";
				hidePleaseWait();

				if (res.data.IsSuccess == true)
				{
					if (res.data.Data.length > 0) {
						contentPath = res.data.Data[0];
					}

					$timeout(function () {

						var rptPara = {
							ExamShiftId: $scope.SelectExamSeatPlan.ExamShiftId,
							ExamTypeId: $scope.SelectExamSeatPlan.ExamTypeId,
						};
						$http({
							method: 'POST',
							url: base_url + "Exam/Transaction/GetStudentExamSeatPlanForSMS",
							dataType: "json",
							data: JSON.stringify(rptPara)
						}).then(function (resST) {
							hidePleaseWait();
							$scope.loadingstatus = "stop";
							if (resST.data.IsSuccess && resST.data.Data) {
								var studentColl = resST.data.Data;
								 
								var dataColl = [];
								angular.forEach(studentColl, function (objEntity) {
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
										//EntityId: entityStudentForSMS,
										EntityId: entityStudentForSMS,
										StudentId: objEntity.StudentId,
										UserId: objEntity.UserId,
										Title: $scope.newNotice.Title,
										Message: msg,
										ContactNo: objEntity.ContactNo,
										StudentName: objEntity.Name,
										ContentPath: contentPath
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
									if (sRes.data.IsSuccess == true) {
										$('#modal-xl').modal('hide');
									}
								});

							}

						}, function (reason) {
							Swal.fire('Failed' + reason);
						});
						 
					});

				}

			}, function (errormessage) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";

			});

		});
		 
	};

	$scope.SendSMSToStudent = function (refData) {

		$scope.SelectExamSeatPlan = refData;
		Swal.fire({
			title: 'Do you want to Send SMS To the filter data?',
			showCancelButton: true,
			confirmButtonText: 'Send',
		}).then((result) => {
			/* Read more about isConfirmed, isDenied below */
			if (result.isConfirmed) {
				var para1 = {
					EntityId: entityStudentForSMS,
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

												if (rptTranId > 0)
												{
													showPleaseWait();
													var rptPara = {
														ExamShiftId: $scope.SelectExamSeatPlan.ExamShiftId,
														ExamTypeId: $scope.SelectExamSeatPlan.ExamTypeId,
													};
													$http({
														method: 'POST',
														url: base_url + "Exam/Transaction/GetStudentExamSeatPlanForSMS",
														dataType: "json",
														data: JSON.stringify(rptPara)
													}).then(function (resST) {
														hidePleaseWait();
														$scope.loadingstatus = "stop";
														if (resST.data.IsSuccess && resST.data.Data) {
															var studentColl = resST.data.Data;

															var dataColl = [];
															angular.forEach(studentColl, function (objEntity) {
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
																	//EntityId: entityStudentForSMS,
																	EntityId: entityStudentForSMS,
																	StudentId: objEntity.StudentId,
																	UserId: objEntity.UserId,
																	Title: selectedTemplate.Title,
																	Message: msg,
																	ContactNo: objEntity.ContactNo,
																	StudentName: objEntity.Name,
																	ContentPath: contentPath
																};

																dataColl.push(newSMS);
															});
															showPleaseWait();
															$http({
																method: 'POST',
																url: base_url + "Global/SendSMSToStudent",
																dataType: "json",
																data: JSON.stringify(dataColl)
															}).then(function (sRes) {
																hidePleaseWait();
																Swal.fire(sRes.data.ResponseMSG);
																if (sRes.data.IsSuccess && sRes.data.Data) {

																}
															});
														}

													}, function (reason) {
														Swal.fire('Failed' + reason);
													});

													 
													print = true;

													
												}

											} else {
												resolve('You need to select:)')
											}
										})
									}
								})
							}

							if (rptTranId > 0 && print == false) {
								showPleaseWait();
								var rptPara = {
									ExamShiftId: $scope.SelectExamSeatPlan.ExamShiftId,
									ExamTypeId: $scope.SelectExamSeatPlan.ExamTypeId,
								};
								$http({
									method: 'POST',
									url: base_url + "Exam/Transaction/GetStudentExamSeatPlanForSMS",
									dataType: "json",
									data: JSON.stringify(rptPara)
								}).then(function (resST) {
									hidePleaseWait();
									$scope.loadingstatus = "stop";
									if (resST.data.IsSuccess && resST.data.Data) {
										var studentColl = resST.data.Data;

										var dataColl = [];
										angular.forEach(studentColl, function (objEntity) {
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
												//EntityId: entityStudentForSMS,
												EntityId: entityStudentForSMS,
												StudentId: objEntity.StudentId,
												UserId: objEntity.UserId,
												Title: selectedTemplate.Title,
												Message: msg,
												ContactNo: objEntity.ContactNo,
												StudentName: objEntity.Name,
												ContentPath: contentPath
											};

											dataColl.push(newSMS);
										});
										showPleaseWait();
										$http({
											method: 'POST',
											url: base_url + "Global/SendSMSToStudent",
											dataType: "json",
											data: JSON.stringify(dataColl)
										}).then(function (sRes) {
											hidePleaseWait();
											Swal.fire(sRes.data.ResponseMSG);
											if (sRes.data.IsSuccess && sRes.data.Data) {

											}
										});
									}

								}, function (reason) {
									Swal.fire('Failed' + reason);
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

});