app.controller('TransportAttendanceController', function ($scope, $http, $timeout, $filter, GlobalServices) {
	$scope.Title = 'TransportAttendance';
	
	OnClickDefault();
	$scope.LoadData = function () {
		$('.select2').select2();
		$scope.loadingstatus = "stop";
		$scope.confirmMSG = GlobalServices.getConfirmMSG();
		$scope.perPageColl = GlobalServices.getPerPageList();
		$scope.AttendanceForColl = [{ id: 1, text: 'Pickup' }, { id: 2, text: 'Drop Off' }, { id: 3, text: 'Both' }];

		$scope.currentPages = {
			TransportAttendance: 3,

		};

		$scope.searchData = {
			TransportAttendance: '',

		};

		$scope.perPage = {
			TransportAttendance: GlobalServices.getPerPageRow(),

		};

		//Vehicle List
		$scope.VehicleDetailsList = [];
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetAllVehicleList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.VehicleDetailsList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		//Transport Point
		$scope.TransportRouteList = [];
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetAllTransportRouteList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TransportRouteList = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.TransportPointList = [];
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetAllTransportPointList",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TransportPointList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		
		$scope.newFilter = {
			FromDate_TMP: new Date(),
			ToDate_TMP: new Date()

		};

		$scope.newTransportAttendance = {
			TranId: null,
			ForDate: null,
			ForDate_TMP: new Date(),
			VehicleId: null,
			RouteId: null,
			AttendanceForId: 3,
			StudentId: null,
			VehiclePointId: null,
			PickUp: false,
			DropOff: false,
			Remaks: '',
			PresentforPick: null,
			PresentforDrop: null,
			Mode: 'Save'
		};

		$scope.GetAllTransportAttendanceList();

	}

	function OnClickDefault() {

		document.getElementById('TransportAttendance-form').style.display = "none";
		document.getElementById('TransportAttendance-details').style.display = "none";

		//TransportAttendance section
		document.getElementById('add-TransportAttendance').onclick = function () {
			document.getElementById('TransportAttendance-section').style.display = "none";
			document.getElementById('TransportAttendance-details').style.display = "none";
			document.getElementById('TransportAttendance-form').style.display = "block";
			$scope.ClearTransportAttendance();
		}

		document.getElementById('back-to-list-TransportAttendance').onclick = function () {
			document.getElementById('TransportAttendance-form').style.display = "none";
			document.getElementById('TransportAttendance-details').style.display = "none";
			document.getElementById('TransportAttendance-section').style.display = "block";
			$scope.ClearTransportAttendance();
		}

		//document.getElementById('TransportAttendance-det').onclick = function () {
		//	document.getElementById('TransportAttendance-section').style.display = "none";
		//	document.getElementById('TransportAttendance-form').style.display = "none";
		//	document.getElementById('TransportAttendance-details').style.display = "block";
		//	$scope.ClearTransportAttendance();
		//}


		document.getElementById('back-from-details').onclick = function () {
			document.getElementById('TransportAttendance-form').style.display = "none";
			document.getElementById('TransportAttendance-details').style.display = "none";
			document.getElementById('TransportAttendance-section').style.display = "block";
			$scope.ClearTransportAttendance();
		}

	}

	$scope.ClearTransportAttendance = function () {
		$scope.newTransportAttendance = {
			TranId: null,
			ForDate: null,
			ForDate_TMP:new Date(),
			VehicleId: null,
			RouteId: null,
			AttendanceForId: 3,
			StudentId: null,
			VehiclePointId: null,
			PickUp: false,
			DropOff: false,
			Remaks: '',
			PresentforPick: null,
			PresentforDrop: null,
			Mode: 'Save'
		};
		$scope.StudentByTransportRoutList = [];

	}


	//show bit value 
	$scope.PresentAbsent = function (bv) {
		if (bv) {
			return "Present";
		} else {
			return "Absent";
		}
	};
	

	$scope.updatePickupAttendance = function () {
		angular.forEach($scope.StudentByTransportRoutList, function (student) {
			student.PickUp = $scope.newTransportAttendance.PresentforPick;
		});
	};

	$scope.updateDropoffAttendance = function () {
		angular.forEach($scope.StudentByTransportRoutList, function (student) {
			student.DropOff = $scope.newTransportAttendance.PresentforDrop;
		});
	};



	//*************************TransportAttendance *********************************

	$scope.SaveUpdateTransportAttendance = function (S) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		var dataToSave = [];
		var forDate = $filter('date')(new Date($scope.newTransportAttendance.ForDateDet.dateAD), 'yyyy-MM-dd');
		var attendance = $scope.newTransportAttendance.AttendanceForId;
		var vehicleId = $scope.newTransportAttendance.VehicleId;
		var routeId = $scope.newTransportAttendance.RouteId;
		for (var i = 0; i < $scope.StudentByTransportRoutList.length; i++) {
			var S = $scope.StudentByTransportRoutList[i];
            //var tranid = S.TranId;
			var studentId = S.StudentId;
			var pickup = S.PickUp;
			var dropOff = S.DropOff;
			var remarks = S.Remarks || "";
			var vehiclePointId = S.VehiclePointId;

			// Default attendance value
			var attendanceValue = false;

			if (attendance === 1) {
				attendanceValue = pickup;  
			} else if (attendance === 2) {
				attendanceValue = dropOff; 
			} else if (attendance === 3) {
				var dataItem1 = {
					//TranId: tranid,
					StudentId: studentId,
					VehicleId: vehicleId,
					RouteId: routeId,
					Remarks: remarks,
					ForDate: forDate,
					Attendance: pickup,  
					VehiclePointId: vehiclePointId,
					AttendanceForId: 1
				};
				var dataItem2 = {
					//TranId: tranid,
					StudentId: studentId,
					VehicleId: vehicleId,
					RouteId: routeId,
					Remarks: remarks,
					ForDate: forDate,
					Attendance: dropOff,
					VehiclePointId: vehiclePointId,
					AttendanceForId: 2 
				};
				// Add both records to the dataToSave array
				dataToSave.push(dataItem1);
				dataToSave.push(dataItem2);
				continue;  
			}

			// Default case for AttendanceForId = 1 or 2
			var dataItem = {
				//TranId: tranid,
				StudentId: studentId,
				VehicleId: vehicleId,
				RouteId: routeId,
				Remarks: remarks,
				ForDate: forDate,
				Attendance: attendanceValue,
				VehiclePointId: vehiclePointId,
				AttendanceForId: attendance
			};
			dataToSave.push(dataItem);
		}

		// Send data to the server
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/SaveTransportAtt",
			headers: { 'Content-Type': undefined },
			transformRequest: function (data) {
				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));
				return formData;
			},
			data: { jsonData: dataToSave }
		}).then(function (res) {
			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			if (res.data.IsSuccess == true) {
				$scope.ClearTransportAttendance();
				$scope.GetAllTransportAttendanceList();
			}
		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
		});
	};




	$scope.GetAllTransportAttendanceList = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TransportAttendanceList = [];
		var para = {
			DateFrom : ($scope.newFilter.FromDateDet ? $filter('date')(new Date($scope.newFilter.FromDateDet.dateAD), 'yyyy-MM-dd') : null),
			DateTo: ($scope.newFilter.ToDateDet ? $filter('date')(new Date($scope.newFilter.ToDateDet.dateAD), 'yyyy-MM-dd') : null)
		};
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetAllTransportAttendanceList",
			dataType: "json",
            data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TransportAttendanceList = res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.GetTransportAttendanceById = function (refData) {

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ForDate: refData.ForDate
		};

		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetTransportAttById",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newTransportAttendance = res.data.Data;
				$scope.newTransportAttendance.Mode = 'Modify';

				document.getElementById('TransportAttendance-section').style.display = "none";
				document.getElementById('TransportAttendance-form').style.display = "block";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.GetDetails = function (refData) {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.TransportDetailList= [];
		var para = {
			//ForDate: ($scope.newFilter.FromDateDet ? $filter('date')(new Date($scope.newFilter.FromDateDet.dateAD), 'yyyy-MM-dd') : null),
			ForDate: refData.ForDate,
			VehicleId: refData.VehicleId,
			RouteId: refData.RouteId,
		};

		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetTransportAttDetail",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.TransportDetail = res.data.Data[0];
				$scope.TransportDetailList = res.data.Data;

				document.getElementById('TransportAttendance-section').style.display = "none";
				document.getElementById('TransportAttendance-details').style.display = "block";
				document.getElementById('TransportAttendance-form').style.display = "none";

			} else {
				Swal.fire(res.data.ResponseMSG);
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};
	$scope.getUniqueStudentCount = function () {
		const uniqueStudents = new Set($scope.TransportDetailList.map(student => student.StudentId));
		return uniqueStudents.size;
	};


	$scope.DelTransportAttendanceById = function (refData) {

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
					TranId: refData.TranId
				};

				$http({
					method: 'POST',
					url: base_url + "Transport/Creation/DelTransportAttendance",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					if (res.data.IsSuccess) {
						$scope.GetAllTransportAttendanceList();
					} else {
						Swal.fire(res.data.ResponseMSG);
					}

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	//Get Student Detail Via TransportRout

	$scope.GetAllStudentByTransportRoutList = function () {
		$scope.StudentByTransportRoutList = [];
		if (!$scope.newTransportAttendance.ForDateDet || !$scope.newTransportAttendance.VehicleId || !$scope.newTransportAttendance.RouteId || !$scope.newTransportAttendance.AttendanceForId)
			return;

		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			ForDate: $filter('date')(new Date($scope.newTransportAttendance.ForDateDet.dateAD), 'yyyy-MM-dd'),
			VehicleId: $scope.newTransportAttendance.VehicleId,
			RouteId: $scope.newTransportAttendance.RouteId,
			AttendanceForId: $scope.newTransportAttendance.AttendanceForId,
		};

		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetStudentByTransportRout",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

			if (res.data.IsSuccess && res.data.Data) {
				// Group by StudentId using lodash
				var groupedData = _.groupBy(res.data.Data, 'StudentId');

				angular.forEach(groupedData, function (group, StudentId) {
					var fst = group[0];  // Get the first element in the group
					var beData = {
						//TranId: fst.TranId,
						StudentId: fst.StudentId,
						StudentName: fst.StudentName,
						RegNo: fst.RegNo,
						Batch: fst.Batch,
						Class: fst.Class,
						Section: fst.Section,
						ClassYear: fst.ClassYear,
						Semester: fst.Semester,
						SContactNo: fst.SContactNo,
						SAddress: fst.SAddress,
						PointName: fst.PointName,
						VehiclePointId: fst.VehiclePointId,
						AttendanceForId: fst.AttendanceForId,
						Remarks: fst.Remarks
					};
					angular.forEach(group, function (item) {
						if ($scope.newTransportAttendance.AttendanceForId === 1) {
							beData.PickUp = item.Attendance;
							beData.DropOff = false;
						} else if ($scope.newTransportAttendance.AttendanceForId === 2) {
							beData.DropOff = item.Attendance;
							beData.PickUp = false;
						} else if ($scope.newTransportAttendance.AttendanceForId === 3) {
							if (item.AttendanceForId === 1) {
								beData.PickUp = item.Attendance;
							} else if (item.AttendanceForId === 2) {
								beData.DropOff = item.Attendance;
							}
						}
					});

					$scope.StudentByTransportRoutList.push(beData);
				});

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			hidePleaseWait();
			Swal.fire('Failed' + reason);
		});
	};


	//VechicleRouteById value 
	$scope.getVechicleRouteById = function () {
		if ($scope.newTransportAttendance && $scope.newTransportAttendance.VehicleId > 0) {
		} else {
			return;
		}
		$scope.loadingstatus = "running";
		showPleaseWait();
		var para = {
			VehicleId: $scope.newTransportAttendance.VehicleId
		};
		$scope.VechicleRouteByIdList = [];
		$http({
			method: 'POST',
			url: base_url + "Transport/Creation/GetTransportRouteByVehicle",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.VechicleRouteByIdList= res.data.Data;

			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}

	$scope.pageChangeHandler = function (num) {
		console.log('page changed to ' + num);
	};

	$scope.validateDate = function (obj, startField, endField, startLabel, endLabel) {
		var res = GlobalServices.validateDate(obj, startField, endField, startLabel, endLabel);
		if (res.IsSuccess == false) {
			Swal.fire({
				icon: 'warning',
				text: res.Message,
				confirmButtonText: 'OK'
			}).then(function () {
				obj.FromDate_TMP = new Date();
				obj.ToDate_TMP = new Date();
				$scope.$applyAsync();
			});
		}
	};

});
