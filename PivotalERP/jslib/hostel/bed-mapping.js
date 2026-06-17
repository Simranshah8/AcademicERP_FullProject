
app.controller('BedMappingController', function ($scope, $http, $timeout, $filter, GlobalServices, $translate) {
	$scope.Title = 'Bed Mapping';

	$scope.sort = function (keyname) {
		$scope.sortKey = keyname;   //set the sortKey to the param passed
		$scope.reverse = !$scope.reverse; //if true make it false and vice versa 
	}
	//OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.confirmMSG = GlobalServices.getConfirmMSG();

		$scope.perPageColl = GlobalServices.getPerPageList();

		//$scope.MonthList = GlobalServices.getMonthList();
		$scope.MonthList = [];
		GlobalServices.getMonthListFromDB().then(function (res1) {
			angular.forEach(res1.data.Data, function (m) {
				$scope.MonthList.push({ id: m.NM, text: m.MonthName });
			});

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassSectionList = [];
		GlobalServices.getClassSectionList().then(function (res) {
			$scope.ClassSectionList = res.data.Data;
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




		$scope.searchData = {
			Student: '',
			Employee:''
		};

		$scope.currentPages = {
			Student: 1,
			Employee: 1
		};
		$scope.perPage = {
			Student: GlobalServices.getPerPageRow(),
			Employee: GlobalServices.getPerPageRow(), 
		};


		$scope.newStudent = {
			StudentId: null,
			Mode: 'Save'
		};

		$scope.newEmployee = {
			EmployeeId: null,
			Mode: 'Save'
		};

		$scope.RoomList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllRoomListForMapping",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.RoomList = res.data.Data;
			} 
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//$scope.GetAllTransportMappingList();
	};


	$scope.ClearStudent = function () {
		$scope.newStudent = {
			StudentId: null,
			Mode: 'Save'
		};

	};

	$scope.ClearEmployee = function () {
		$scope.newEmployee = {
			EmployeeId: null,
			Mode: 'Save'
		};

	};

	//*************************Student*********************************	

	$scope.CurMonthDetailsColl = [];
	$scope.ShowMonthSelection = function (dt) {

		$scope.CurMonthDetailsColl = dt.MonthDetailsColl;
		$('#modal-custom-month-fee').modal('show');
	};

	$scope.CheckedAll = function (st) {
		if (!st.OriginalRate) {
			st.OriginalRate = st.Rate;
		}
		if (st.ForAll) {
			st.Rate = 0;
		} else {
			st.Rate = st.OriginalRate;
		}
		angular.forEach(st.MonthDetailsColl, function (md) {
			md.Rate = st.Rate;
			md.DiscountAmt = st.DiscountAmt;
			md.DiscountPer = st.DiscountPer;
			md.PayableAmt = st.PayableAmt;
		});
	};
	$scope.ChangeRoom = function (st, col) {

		var selectRoom = mx($scope.RoomList).firstOrDefault(p1 => p1.BedId == st.BedId);
		if (selectRoom) {
			//selectRoom.IsVacant = false;
			var rate = selectRoom.RoomFee;
			
			if (col == 1) {
				st.Rate = rate;
				st.PayableAmt = st.Rate - st.DiscountAmt;
			}
			else if (col == 2)
			{				 
				//st.PayableAmt = st.Rate - st.DiscountAmt;

				var disAmt = 0;
				if (st.DiscountPer > 0) {
					disAmt = (st.Rate * st.DiscountPer) / 100;
				}

				st.DiscountAmt = disAmt;
				st.PayableAmt = st.Rate - disAmt;

            }
			else if (col == 3) {
				var disAmt = 0;
				if (st.DiscountPer > 0) {
					disAmt = (st.Rate * st.DiscountPer) / 100;
				}

				st.DiscountAmt = disAmt;
				st.PayableAmt = st.Rate - disAmt;

			} else if (col == 4) {
				var disPer = 0;
				if (st.DiscountAmt > 0) {
					disPer = (st.DiscountAmt / st.Rate) * 100;
				}

				st.DiscountPer = disPer;
				st.PayableAmt = st.Rate - st.DiscountAmt;
			}

		} else {

			//st.IsVacant = true;
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
		$scope.newStudent.StudentColl = [];
		if ($scope.newStudent.SelectedClass || $scope.newStudent.SelectedClass==0)
		{
			var para = {};

			if ($scope.newStudent.SelectedClass != 0) {
				para = {
					ClassId: $scope.newStudent.SelectedClass.ClassId,
					SectionId: $scope.newStudent.SelectedClass.SectionId,
					SemesterId: $scope.newStudent.SemesterId,
					ClassYearId: $scope.newStudent.ClassYearId,
					typeId: 2,
					BatchId: $scope.newStudent.BatchId
				};
			} else {
				para = {
					ClassId: 0,
					SectionId: 0,
					SemesterId: 0,
					ClassYearId: 0,
					typeId: 2,					
					BatchId:0

				};
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
					$scope.newStudent.StudentColl = res.data.Data;

					if ($scope.newStudent.SelectedClass != 0) {
						para = {
							ClassId: $scope.newStudent.SelectedClass.ClassId,
							SectionId: $scope.newStudent.SelectedClass.SectionId
						};
					} else {
						para = {
							ClassId: 0,
							SectionId: 0
						};
					}

					$http({
						method: 'POST',
						url: base_url + "Hostel/Creation/GetAllBedMapping",
						dataType: "json",
						data: JSON.stringify(para)
					}).then(function (res1) {
						hidePleaseWait();
						$scope.loadingstatus = "stop";
						if (res1.data.IsSuccess && res1.data.Data) {
							var dataColl = mx(res1.data.Data);
							angular.forEach($scope.newStudent.StudentColl, function (st) {

								var find = dataColl.firstOrDefault(p1 => p1.StudentId == st.StudentId);
								st.BedId = find ? find.BedId : 0;
								st.AllotDate = find && find.AllotDate ? new Date(find.AllotDate) : null;
								st.AllotDate_TMP = find && find.AllotDate ? new Date(find.AllotDate) : null;
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
	$scope.SaveUpdateStudent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var dataColl = [];
		var cId = $scope.newStudent.SelectedClass.ClassId;
		var sId = $scope.newStudent.SelectedClass.SectionId;

		angular.forEach($scope.newStudent.StudentColl, function (fm) {
			if (fm.BedId && fm.BedId > 0) {
				var beData = {
					ClassId: cId,
					SectionId: sId,
					StudentId: fm.StudentId,
					BedId: fm.BedId,					
					AllotDate: null,
					Rate: fm.Rate,
					DiscountAmt: fm.DiscountAmt,
					DiscountPer: fm.DiscountPer,
					PayableAmt: fm.PayableAmt,
					ForAll: fm.ForAll,
					MonthIdColl: []
				};

				if (fm.AllotDate_ADDet) {
					beData.AllotDate = $filter('date')(new Date(fm.AllotDate_ADDet.dateAD), 'yyyy-MM-dd');
				} else
					beData.AllotDate = null;

				angular.forEach(fm.MonthDetailsColl, function (md) {
					md.MonthId = md.id;
					if (md.Rate > 0) {
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
			url: base_url + "Hostel/Creation/SaveBedMapping",
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
				$scope.ClearStudent();
				
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.SaveUpdateStudentWise = function (fm) {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var dataColl = []; 

		var beData = {
			ClassId: 0,
			SectionId: 0,
			StudentId: fm.StudentId,
			BedId: fm.BedId,
			AllotDate: null,
			Rate: fm.Rate,
			DiscountAmt: fm.DiscountAmt,
			DiscountPer: fm.DiscountPer,
			PayableAmt: fm.PayableAmt,
			ForAll: fm.ForAll,
			MonthIdColl: []
		};

		if (fm.AllotDate_ADDet) {
			beData.AllotDate = $filter('date')(new Date(fm.AllotDate_ADDet.dateAD), 'yyyy-MM-dd');
		} else
			beData.AllotDate = null;

		angular.forEach(fm.MonthDetailsColl, function (md) {
			md.MonthId = md.id;
			if (md.Rate > 0) {
				beData.MonthIdColl.push(md);
			}

		});
		dataColl.push(beData);

	
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveBedMapping",
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


	//*************************Employee*********************************	

	$scope.SaveUpdateEmployee = function () {
		if ($scope.IsValidEmployee() == true) {
			if ($scope.confirmMSG.Accept == true) {
				var saveModify = $scope.newEmployee.Mode;
				Swal.fire({
					title: 'Do you want to ' + saveModify + ' the current data?',
					showCancelButton: true,
					confirmButtonText: saveModify,
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						$scope.CallSaveUpdateEmployee();
					}
				});
			} else
				$scope.CallSaveUpdateEmployee();

		}
	};

	$scope.CallSaveUpdateEmployee = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/SaveEmployee",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.newEmployee }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();

			Swal.fire(res.data.ResponseMSG);

			if (res.data.IsSuccess == true) {
				$scope.ClearEmployee();
				$scope.GetAllEmployeeList();
			}

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});
	};

	$scope.GetAllEmployeeList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EmployeeList = [];
		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetAllEmployeeList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmployeeList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.GetEmployeeById = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			EmployeeId: refData.EmployeeId
		};

		$http({
			method: 'POST',
			url: base_url + "Hostel/Creation/GetEmployeeById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newEmployee = res.data.Data;
				$scope.newEmployee.Mode = 'Modify';


			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.isBedTaken = function (bedId, currentStudent) {
		for (var i = 0; i < $scope.newStudent.StudentColl.length; i++) {
			var st = $scope.newStudent.StudentColl[i];
			if (st === currentStudent) continue;
			if (st.BedId == bedId) {
				return true;
			}
		}
		return false;
	};

});