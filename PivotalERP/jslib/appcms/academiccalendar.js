app.controller('npCalenderController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {
	$scope.Title = 'About Us';


	var gSrv = GlobalServices;

	$rootScope.ConfigFunction = function () {
		$scope.LoadData();
	};
	$rootScope.ChangeLanguage();

	$scope.LoadData = function () {

		$scope.confirmMSG = gSrv.getConfirmMSG();
		$scope.YearColl = gSrv.getYearList();


		$scope.AcademicCalendar = {
			YearId: 0,
			MonthColl: []
		};

		//Added by simran on 2nd poush
		$scope.DepartmentList = [];
		gSrv.getDepartmentList().then(function (res) {
			$scope.DepartmentList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.BranchList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchList",
			dataType: "json"
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.DesignationList = [];
		gSrv.getDesignationList().then(function (res) {
			$scope.DesignationList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ServiceTypeList = [];
		gSrv.getServiceTypeList().then(function (res) {
			$scope.ServiceTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.EmployeeList = [];
		gSrv.GetEmployeeList().then(function (res) {
			$scope.EmployeeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.GenderCol = [
			{ id: 1, text: 'Male' },
			{ id: 2, text: 'Female' },
			{ id: 3, text: 'Other' }
		];

		$scope.ReligionsColl = [
			{ id: 1, text: 'Hinduism' },
			{ id: 2, text: 'Islam' },
			{ id: 3, text: 'Buddhism' },
			{ id: 4, text: 'Christian' },
			{ id: 5, text: 'Jainism' },
			{ id: 6, text: 'Sikhism' },
			{ id: 7, text: 'Judaism' },
		]
		//Ends

		$scope.CurDate = {};
		$http({
			method: 'POST',
			url: base_url + "Global/GetDate",
			dataType: "json"
		}).then(function (res) {
			$scope.CurDate = res.data.Data;

			if ($rootScope.LANG == 'in') {
				$scope.AcademicCalendar.YearId = new Date($scope.CurDate.Date_AD).getFullYear();
			} else {
				$scope.AcademicCalendar.YearId = $scope.CurDate.NY;
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.newCompanyDetails = {};
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetCompanyDet",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.newCompanyDet = res.data.Data;
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.Logo = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllAboutUsList",
			dataType: "json",
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.Logo = res.data.Data[0];
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.EventTypeList = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllEventTypeList",
			dataType: "json"
		}).then(function (res) {
			$scope.EventTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.AcademicYearColl = [];
		$http({
			method: 'POST',
			url: base_url + "Academic/Creation/GetAllAcademicYearList",
			dataType: "json"
		}).then(function (res) {
			$scope.AcademicYearColl = res.data.Data;

			$http({
				method: 'GET',
				url: base_url + "Global/GetRunningAcademicYearId",
				dataType: "json"
			}).then(function (res) {
				var aId = res.data.Data.RId;

				//var findA = mx($scope.AcademicYearColl).firstOrDefault(p1 => p1.AcademicYearId == aId);
				//if (findA)
				//	$scope.AcademicCalendar.YearId =parseInt(findA.Name);

				$scope.getNepaliCalendar();


			}, function (reason) {
				Swal.fire('Failed' + reason);
			});


		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	    //Added by Simran
		$scope.AcademicConfig = {};
		GlobalServices.getAcademicConfig().then(function (res1) {
			$scope.AcademicConfig = res1.data.Data;

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
		//End

	}

	$scope.getNepaliCalendar = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			YearId: $scope.AcademicCalendar.YearId
		};

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetNepaliCalendar",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.AcademicCalendar.MonthColl = res.data.Data;
				
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}


	$scope.getSelectedYearText = function () {
		var selected = $scope.YearColl.find(y => y.id === $scope.AcademicCalendar.YearId);
		return selected ? selected.text : '';
	};

	$scope.currentEvent = {
		ApplicableForClass: ''
	};
	$scope.ClickOnDay = function (dayDet) {
		/*$scope.currentEvent = dayDet;*/
		$scope.currentEvent = {};
		//Ends
		$scope.currentEvent.ApplicableForClass = '';

		if (dayDet.AD_Date) {
			$scope.currentEvent.FromDate_TMP = new Date(dayDet.AD_Date);
			$scope.currentEvent.ToDate_TMP = new Date(dayDet.AD_Date);
		}
		if (dayDet.EventColl && dayDet.EventColl.length > 0) {

			var templatesName = [];
			var sno = 1;
			angular.forEach(dayDet.EventColl, function (tc) {
				templatesName.push(sno + '-' + tc.Name);
				sno++;
			});

			Swal.fire({
				title: 'Event/Holiday Add/Edit',
				input: 'select',
				inputOptions: templatesName,
				inputPlaceholder: 'Select for edit',
				showCancelButton: true,
				inputValidator: (value) => {
					return new Promise((resolve) => {
						if (value >= 0 && Number.isInteger(parseInt(value)) == true) {
							resolve()

							var para = {
								EventHolidayId: dayDet.EventColl[value].EventHolidayId
							};
							$http({
								method: 'POST',
								url: base_url + "AppCMS/Creation/GetEventHolidayById",
								dataType: "json",
								data: JSON.stringify(para)
							}).then(function (res) {
								hidePleaseWait();
								$scope.loadingstatus = "stop";
								if (res.data.IsSuccess && res.data.Data) {
									var rdata = res.data.Data;
									$scope.currentEvent.EventHolidayId = rdata.EventHolidayId;
									$scope.currentEvent.EventTypeId = rdata.EventTypeId;
									$scope.currentEvent.Name = rdata.Name;
									$scope.currentEvent.Description = rdata.Description;
									$scope.currentEvent.Name = rdata.Name;

									if (rdata.FromDate)
										$scope.currentEvent.FromDate_TMP = new Date(rdata.FromDate);

									if (rdata.ToDate)
										$scope.currentEvent.ToDate_TMP = new Date(rdata.ToDate);

									if (rdata.AtTime)
										$scope.currentEvent.AtTime_TMP = new Date(rdata.AtTime);
									//if (rdata.ApplicableForClass) {
									//	$scope.currentEvent.ApplicableForClassColl = rdata.ApplicableForClass.split(',').map(Number);
									//}
									//Added by simran on 2nd poush
									$scope.currentEvent.ReligionColl = rdata.Religion
									$scope.currentEvent.BranchColl = rdata.Branch
									$scope.currentEvent.DepartmentColl = rdata.DepartmentId
									$scope.currentEvent.DesignationColl = rdata.DesignationId
									$scope.currentEvent.ServiceTypeColl = rdata.ServiceTypeId
									$scope.currentEvent.EmployeeColl = rdata.Employee
									$scope.currentEvent.GenderColl = rdata.GenderId
									$scope.currentEvent.ApplicableForClassColl = rdata.ApplicableForClass
									$scope.currentEvent.ClassYearColl = rdata.ClassYearId
									$scope.currentEvent.BatchColl = rdata.BatchId
									$scope.currentEvent.SemesterColl = rdata.SemesterId
									//End
									$('#modal-xl').modal('show');
								} else {
									Swal.fire(res.data.ResponseMSG);
								}
							}, function (reason) {
								Swal.fire('Failed' + reason);
							});


						} else {
							resolve()
							$('#modal-xl').modal('show');
						}
					})
				}
			})




		} else {
			$('#modal-xl').modal('show');
		}


	};

	$scope.DelEvent = function (dayDet) {

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
					EventHolidayId: $scope.currentEvent.EventHolidayId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelEventHoliday",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);

					$scope.getNepaliCalendar();

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.ShowAtTime = true;
	$scope.ChangeEventType = function () {
		var et = mx($scope.EventTypeList).firstOrDefault(p1 => p1.EventTypeId == $scope.currentEvent.EventTypeId);
		if (et && et.EType == 1)
			$scope.ShowAtTime = false;
		else
			$scope.ShowAtTime = true;
	};
	$scope.SaveUpdateEvent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.currentEvent.FromDateDet) {
			$scope.currentEvent.FromDate = $filter('date')(new Date($scope.currentEvent.FromDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.currentEvent.FromDate = null;

		if ($scope.currentEvent.ToDateDet) {
			$scope.currentEvent.ToDate = $filter('date')(new Date($scope.currentEvent.ToDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.currentEvent.ToDate = null;

		if ($scope.currentEvent.ApplicableForClassColl)
			$scope.currentEvent.ApplicableForClass = $scope.currentEvent.ApplicableForClassColl.toString();
		else
			$scope.currentEvent.ApplicableForClass = '';

		if ($scope.currentEvent.AtTime_TMP)
			$scope.currentEvent.AtTime = $scope.currentEvent.AtTime_TMP.toLocaleString();
		else
			$scope.currentEvent.AtTime = null;
		//Added by simran on 2nd poush
		if ($scope.currentEvent.ReligionColl)
			$scope.currentEvent.Religion = $scope.currentEvent.ReligionColl.toString();
		else
			$scope.currentEvent.Religion = '';

		if ($scope.currentEvent.BranchColl)
			$scope.currentEvent.Branch = $scope.currentEvent.BranchColl.toString();
		else
			$scope.currentEvent.Branch = '';

		if ($scope.currentEvent.DepartmentColl)
			$scope.currentEvent.DepartmentId = $scope.currentEvent.DepartmentColl.toString();
		else
			$scope.currentEvent.DepartmentId = '';

		if ($scope.currentEvent.DesignationColl)
			$scope.currentEvent.DesignationId = $scope.currentEvent.DesignationColl.toString();
		else
			$scope.currentEvent.DesignationId = '';

		if ($scope.currentEvent.ServiceTypeColl)
			$scope.currentEvent.ServiceTypeId = $scope.currentEvent.ServiceTypeColl.toString();
		else
			$scope.currentEvent.ServiceTypeId = '';

		if ($scope.currentEvent.EmployeeColl)
			$scope.currentEvent.Employee = $scope.currentEvent.EmployeeColl.toString();
		else
			$scope.currentEvent.Employee = '';

		if ($scope.currentEvent.GenderColl)
			$scope.currentEvent.GenderId = $scope.currentEvent.GenderColl.toString();
		else
			$scope.currentEvent.GenderId = '';
		//End

		//Added by Simran on 17th Jestha
		if ($scope.currentEvent.ClassYearColl)
			$scope.currentEvent.ClassYearId = $scope.currentEvent.ClassYearColl.toString();
		else
			$scope.currentEvent.ClassYearId = '';

		if ($scope.currentEvent.BatchColl)
			$scope.currentEvent.BatchId = $scope.currentEvent.BatchColl.toString();
		else
			$scope.currentEvent.BatchId = '';

		if ($scope.currentEvent.SemesterColl)
			$scope.currentEvent.SemesterId = $scope.currentEvent.SemesterColl.toString();
		else
			$scope.currentEvent.SemesterId = '';
		//End

		//var et = mx($scope.EventTypeList).firstOrDefault(p1 => p1.EventTypeId == $scope.currentEvent.EventTypeId);
		//if (et && et.EType == 1)
		//	$scope.currentEvent.AtTime = null;

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveEventHoliday",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.currentEvent }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			$scope.getNepaliCalendar();
			$('#modal-xl').modal('hide');

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});


	}

	//$scope.Print = function () {
	//	// Get the selected academic year from the dropdown
	//	var academicYear = $scope.AcademicCalendar.YearId;

	//	// Get the text for the academic year
	//	var academicYearText = $scope.YearColl.find(year => year.id === academicYear).text;

	//	// Create the heading HTML
	//	var headingHtml = '<div class="print-heading text-center"><h2>Academic Year: ' + academicYearText + '</h2></div>';

	//	// Temporarily add the heading to the calendar content
	//	$('#calenderpart').prepend(headingHtml);

	//	// Trigger the print dialog
	//	$('#calenderpart').printThis({
	//		importCSS: true,       // Import the CSS for printing
	//		importStyle: true,     // Import the internal styles
	//		pageTitle: "Academic Calendar",  // Set a custom title for the print page
	//		printDelay: 333,       // Delay before printing (if needed)
	//		afterPrint: function () {
	//			// Remove the heading after the print process has started
	//			$('.print-heading').remove();
	//		}
	//	});
	//};

	$scope.PrintData = function () {
		$('#calenderheader').show(); // Show the header before printing
		$('#Calenderprint').printThis({
			afterPrint: function () {
				$('#calenderheader').hide(); // Hide it again after printing
			}
		});
	};


});


app.controller('npEventListController', function ($scope, $http, $timeout, $filter, $rootScope, GlobalServices, $translate) {
	$scope.Title = 'Event List';

	var gSrv = GlobalServices;

	$rootScope.ConfigFunction = function () {
		$scope.LoadData();
	};
	$rootScope.ChangeLanguage();

	$scope.LoadData = function () {

		$scope.confirmMSG = gSrv.getConfirmMSG();

		$scope.YearColl = gSrv.getYearList();

		$scope.AcademicCalendar = {
			YearId: 0,
			MonthColl: []
		};

		$scope.CurDate = {};
		$http({
			method: 'POST',
			url: base_url + "Global/GetDate",
			dataType: "json"
		}).then(function (res) {
			$scope.CurDate = res.data.Data;

			if ($rootScope.LANG == 'in') {
				$scope.AcademicCalendar.YearId = new Date($scope.CurDate.Date_AD).getFullYear();
			} else {
				$scope.AcademicCalendar.YearId = $scope.CurDate.NY;
			}

		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.ClassList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});


		$scope.EventTypeList = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetAllEventTypeList",
			dataType: "json"
		}).then(function (res) {
			$scope.EventTypeList = res.data.Data;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.getCurrentDate();

	}

	$scope.getCurrentDate = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			forDate: null
		};

		$http({
			method: 'POST',
			url: base_url + "Global/GetDate",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.Data) {
				$scope.CurrentDate = res.data.Data;

				if ($rootScope.LANG == 'in') {
					var dt = new Date($scope.CurrentDate.Date_AD);
					$scope.AcademicCalendar.YearId = dt.getFullYear();
					$scope.AcademicCalendar.MonthId = dt.getMonth() + 1;
					$scope.AcademicCalendar.DayId = dt.getDay();
				} else {
					$scope.AcademicCalendar.YearId = $scope.CurrentDate.NY;
					$scope.AcademicCalendar.MonthId = $scope.CurrentDate.NM;
					$scope.AcademicCalendar.DayId = $scope.CurrentDate.ND;
				}


				$timeout(function () {
					$scope.getNepaliCalendar();
				});

			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.getNepaliCalendar = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		var para = {
			YearId: $scope.AcademicCalendar.YearId
		};

		$scope.AllEventList = [];
		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/GetNepaliCalendar",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {

				$timeout(function () {
					$scope.AcademicCalendar.MonthColl = res.data.Data;

					$scope.nextPreviusMonth(0);

					angular.forEach($scope.AcademicCalendar.MonthColl, function (mn) {
						angular.forEach(mn.EventColl, function (ec) {
							$scope.AllEventList.push(ec);
						});
					});

				});



			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

	}

	$scope.nextPreviusMonth = function (val) {

		if (val == 0) {

		} else if (val == 1) {

			if ($scope.AcademicCalendar.MonthId == 12)
				return;
			else
				$scope.AcademicCalendar.MonthId = $scope.AcademicCalendar.MonthId + 1;

		} else if (val == -1) {
			if ($scope.AcademicCalendar.MonthId == 1)
				return;
			else
				$scope.AcademicCalendar.MonthId = $scope.AcademicCalendar.MonthId - 1;
		}

		$scope.AcademicCalendar.CurMonth = mx($scope.AcademicCalendar.MonthColl).firstOrDefault(p1 => p1.MonthId == $scope.AcademicCalendar.MonthId);

		var trRow = [];
		trRow.push({ ColColl: [] });
		trRow.push({ ColColl: [] });
		trRow.push({ ColColl: [] });
		trRow.push({ ColColl: [] });
		trRow.push({ ColColl: [] });
		trRow.push({ ColColl: [] });

		var col = 0, row = 0;

		if ($scope.AcademicCalendar.CurMonth && $scope.AcademicCalendar.CurMonth.BlankDaysColl) {
			angular.forEach($scope.AcademicCalendar.CurMonth.BlankDaysColl, function (bd) {
				if (col == 7) {
					row++;
					col = 0;
				}
				trRow[row].ColColl.push({});
				col++;
			});
		}


		if ($scope.AcademicCalendar.CurMonth && $scope.AcademicCalendar.CurMonth.DataColl) {
			angular.forEach($scope.AcademicCalendar.CurMonth.DataColl, function (dc) {

				if (col == 7) {
					row++;
					col = 0;
				}
				trRow[row].ColColl.push(dc);
				col++;
			});
		}


		$timeout(function () {
			$scope.$apply(function () {
				if ($scope.AcademicCalendar.CurMonth)
					$scope.AcademicCalendar.CurMonth.RowColl = trRow;
			});
		});


	}

	$scope.currentEvent = {
		ApplicableForClass: ''
	};
	$scope.ClickOnDay = function (dayDet) {
		$scope.currentEvent = dayDet;
		$scope.currentEvent.ApplicableForClass = '';

		if (dayDet.AD_Date) {
			$scope.currentEvent.FromDate_TMP = new Date(dayDet.AD_Date);
			$scope.currentEvent.ToDate_TMP = new Date(dayDet.AD_Date);
		}

		if (dayDet.EventColl && dayDet.EventColl.length > 0) {

			var para = {
				EventHolidayId: dayDet.EventColl[0].EventHolidayId
			};
			$http({
				method: 'POST',
				url: base_url + "AppCMS/Creation/GetEventHolidayById",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				hidePleaseWait();
				$scope.loadingstatus = "stop";
				if (res.data.IsSuccess && res.data.Data) {
					var rdata = res.data.Data;
					$scope.currentEvent.EventHolidayId = rdata.EventHolidayId;
					$scope.currentEvent.EventTypeId = rdata.EventTypeId;
					$scope.currentEvent.Name = rdata.Name;
					$scope.currentEvent.Description = rdata.Description;
					$scope.currentEvent.Name = rdata.Name;

					if (rdata.FromDate)
						$scope.currentEvent.FromDate_TMP = new Date(rdata.FromDate);

					if (rdata.ToDate)
						$scope.currentEvent.ToDate_TMP = new Date(rdata.ToDate);

					$('#modal-xl').modal('show');
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});


		} else {
			$('#modal-xl').modal('show');
		}


	};

	$scope.DelEvent = function (dayDet) {

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
					EventHolidayId: $scope.currentEvent.EventHolidayId
				};

				$http({
					method: 'POST',
					url: base_url + "AppCMS/Creation/DelEventHoliday",
					dataType: "json",
					data: JSON.stringify(para)
				}).then(function (res) {
					hidePleaseWait();
					$scope.loadingstatus = "stop";
					Swal.fire(res.data.ResponseMSG);

					$scope.getNepaliCalendar();

				}, function (reason) {
					Swal.fire('Failed' + reason);
				});
			}
		});


	};

	$scope.SaveUpdateEvent = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();

		if ($scope.currentEvent.FromDateDet) {
			$scope.currentEvent.FromDate = $filter('date')(new Date($scope.currentEvent.FromDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.currentEvent.FromDate = null;

		if ($scope.currentEvent.ToDateDet) {
			$scope.currentEvent.ToDate = $filter('date')(new Date($scope.currentEvent.ToDateDet.dateAD), 'yyyy-MM-dd');
		} else
			$scope.currentEvent.ToDate = null;

		if ($scope.currentEvent.ApplicableForClassColl)
			$scope.currentEvent.ApplicableForClass = $scope.currentEvent.ApplicableForClassColl.toString();
		else
			$scope.currentEvent.ApplicableForClass = '';

		$http({
			method: 'POST',
			url: base_url + "AppCMS/Creation/SaveEventHoliday",
			headers: { 'Content-Type': undefined },

			transformRequest: function (data) {

				var formData = new FormData();
				formData.append("jsonData", angular.toJson(data.jsonData));

				return formData;
			},
			data: { jsonData: $scope.currentEvent }
		}).then(function (res) {

			$scope.loadingstatus = "stop";
			hidePleaseWait();
			Swal.fire(res.data.ResponseMSG);
			$scope.getNepaliCalendar();
			$('#modal-xl').modal('hide');

		}, function (errormessage) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";

		});


	}

	$scope.ShowPersonalImg = function (item) {
		$scope.viewImg = {
			ContentPath: ''
		};
		if (item.ImagePath && item.ImagePath.length > 0) {
			$scope.viewImg.ContentPath = item.ImagePath;
			$('#PersonalImg').modal('show');
		} else
			Swal.fire('No Image Found');

	};


	
});