app.controller('adminDashboardController', function ($scope, $http, $timeout, $filter, GlobalServices, $rootScope, $translate) {
	$scope.Title = 'Admin Dashboard';
	var gSrv = GlobalServices;
	$rootScope.ChangeLanguage();
	$scope.isInitialLoad = true;
	$scope.LoadData = function ()
	{
		$scope.isLoading = true;
		showPleaseWait();
		$('.select2').select2();


		//Added by Shishant for SMS log on December 3, 2024 
		$scope.SMSBalance = {
			available_credit: 0,
			total_sms_sent: 0,
			last_transaction_date: '',
			last_transaction_date_sms_sent: '',
			response_code: ''
		};
	/*	$scope.AddSMS();*/
		//

		$scope.newBirthday = {
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date()
		};
		$scope.newBirthdayE = {
			DateFrom_TMP: new Date(),
			DateTo_TMP: new Date()
		};
		$scope.LoadDOBStudent();
        $scope.LoadDOBE();
        $scope.beData = {
            StudentAttendanceFor: null,
            TotalFeeCollect: 0,
            TotalFeeCollectPer: 0
        };


		$scope.AcademicYearList = [];
		$scope.AcademicMonthList = [];
		$scope.AcademicYearList = gSrv.getYearList();
		$scope.AcademicMonthList = gSrv.getMonthList();

		$scope.newFilter = {
			EmployeeAttYearId: null,
			EmployeeAttMonthId: null,
			HomeworkClassId: null,
			HomeworkMonthId: null,
			LMSMonthId: null,
		};



		$scope.ClassList = [];
		$scope.ClassSectionList = [];
		gSrv.getClassSectionList().then(function (res) {
			$scope.ClassList = res.data.Data.ClassList;
			$scope.ClassSectionList = res.data.Data.SectionListWithClass;
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		$scope.BranchList = [];
		$http({
			method: 'POST',
			url: base_url + "Setup/Security/GetAllBranchList",
			dataType: "json",
		}).then(function (res) {
			if (res.data.IsSuccess && res.data.Data) {
				$scope.BranchList = res.data.Data;
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});

		var now = new Date();
		var hrs = now.getHours();
		var echo = "Hello";
		if (hrs >= 0) {
			echo = "Morning Sunshine!";
		}
		if (hrs >= 15) {
			echo = "Good morning";
		}
		if (hrs >= 12) {
			echo = "Good afternoon";
		}
		if (hrs >= 17) {
			echo = "Good evening";
		}
		if (hrs >= 22) {
			echo = "Go to bed!";
		}
		$scope.Greeting = echo;

		$scope.HouseColorCardList = ["blue", "purple", "green", "red","yello","dark"];

		$scope.FieldsColl = [];
		var para = {
			ForUserId: null,
			EntityId: entityId
		};

		$timeout(function () {
			$http({
				method: 'POST',
				url: base_url + "Setup/Security/GetAllowFields",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.FieldsColl = mx(res.data.Data);
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});

		$scope.PeriodMonths = [];
		$timeout(function () {
			$http({
				method: 'GET',
				url: base_url + "Global/GetCompanyPeriodMonths",
				dataType: "json",				
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.PeriodMonths = res.data.Data;
				} else {
					Swal.fire(res.data.ResponseMSG);
				}
			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		});

		$timeout(function () {

			var para = {
				branchId: null
			};
			$scope.loadingstatus = "running";
			showPleaseWait();
		
			$http({
				method: 'POST',
				url: base_url + "Dashboard/Common/GetAdminDashboard",
				dataType: "json",
				data:JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.beData = res.data.Data;
					$scope.beData.StudentAttendanceFor = 0;

					var totalFeeCollected = ($scope.beData.TotalReceivedAmt + $scope.beData.TotalDiscountAmt);
					var feeCollectedPer = $scope.beData.TotalFeeAmt == 0 ? 0 : (totalFeeCollected / $scope.beData.TotalFeeAmt) * 100;
					$scope.beData.TotalFeeCollect = parseFloat(totalFeeCollected).toFixed(2);
					$scope.beData.TotalFeeCollectPer = parseFloat(feeCollectedPer).toFixed(2);

					var recPer = $scope.beData.TotalFeeAmt == 0 ? 0 : ($scope.beData.TotalReceivedAmt / $scope.beData.TotalFeeAmt) * 100;
					$scope.beData.TotalFeeReceivedPer = parseFloat(recPer).toFixed(2);
					$scope.beData.TotalDuesPer = $scope.beData.TotalFeeAmt == 0 ? 0 : (100 - $scope.beData.TotalFeeCollectPer);
					$scope.beData.TotalDisPer = $scope.beData.TotalFeeCollectPer - $scope.beData.TotalFeeReceivedPer;

					$scope.StudentAnalysisSections();
					$scope.StudentMonthlyAttendance();
					$scope.LibraryBooks();
					$scope.StaffAnalysis();
					var myVar = setInterval(UpdateLastLoginDateTime, 60000);

					$scope.loadingstatus = "stop";
					hidePleaseWait();

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

			$timeout(function () {
				$scope.isInitialLoad = false;
			},500);
		});


		//Added By Suresh starts
		$scope.YearColl = gSrv.getYearList();
		$scope.AcademicCalendar = {
			YearId: 0,
			MonthColl: []
		};

	/*	$scope.CurDate = {};
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
		});*/

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

		$scope.MonthList = [];
		GlobalServices.getAcademicMonthList(null, null)
			.then(function (res) {
				if (res.data && res.data.Data) {
					$scope.MonthList = res.data.Data;
				}
			}, function (reason) {
				Swal.fire('Failed', reason.statusText || 'Something went wrong', 'error');
			});


		$scope.getCurrentDate();

		//Ends

		
	}


	//Added By Suresh on Falgun 22 starts
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
					$scope.AcademicCalendar.DayId = dt.getDate();
				} else {
					$scope.AcademicCalendar.YearId = $scope.CurrentDate.NY;
					$scope.AcademicCalendar.MonthId = $scope.CurrentDate.NM;
					$scope.AcademicCalendar.DayId = $scope.CurrentDate.ND;

					$scope.beData.StudentAttendanceFor = $scope.CurrentDate.NM;
					$scope.newFilter.EmployeeAttYearId = $scope.CurrentDate.NY;
					$scope.newFilter.EmployeeAttMonthId = $scope.CurrentDate.NM;
					$scope.newFilter.HomeworkMonthId = $scope.CurrentDate.NM;
					$scope.newFilter.LMSMonthId = $scope.CurrentDate.NM;
				}


				$timeout(function () {
					$scope.getNepaliCalendar();
					$scope.GetStudentMonthlyAttendance();
					$scope.GetEmployeeMonthlyAttendance();
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

	//Ends

	$scope.ReLoadData = function () {
		if ($scope.isInitialLoad) {
			return;
        }
		$timeout(function () {

			var para = {
				branchId: $scope.ForBranchId
			};
			$scope.loadingstatus = "running";
			showPleaseWait();

			$http({
				method: 'POST',
				url: base_url + "Dashboard/Common/GetAdminDashboard",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					$scope.beData = res.data.Data;
					$scope.beData.StudentAttendanceFor = 0;

					var totalFeeCollected = ($scope.beData.TotalReceivedAmt + $scope.beData.TotalDiscountAmt);
					var feeCollectedPer = $scope.beData.TotalFeeAmt == 0 ? 0 : (totalFeeCollected / $scope.beData.TotalFeeAmt) * 100;
					$scope.beData.TotalFeeCollect = parseFloat(totalFeeCollected).toFixed(2);
					$scope.beData.TotalFeeCollectPer = parseFloat(feeCollectedPer).toFixed(2);

					var recPer = $scope.beData.TotalFeeAmt == 0 ? 0 : ($scope.beData.TotalReceivedAmt / $scope.beData.TotalFeeAmt) * 100;
					$scope.beData.TotalFeeReceivedPer = parseFloat(recPer).toFixed(2);
					$scope.beData.TotalDuesPer = $scope.beData.TotalFeeAmt == 0 ? 0 : (100 - $scope.beData.TotalFeeCollectPer);
					$scope.beData.TotalDisPer = $scope.beData.TotalFeeCollectPer - $scope.beData.TotalFeeReceivedPer;

					$scope.StudentAnalysisSections();
					$scope.StudentMonthlyAttendance();
					$scope.LibraryBooks();
					$scope.StaffAnalysis();
					var myVar = setInterval(UpdateLastLoginDateTime, 60000);

					$scope.loadingstatus = "stop";
					hidePleaseWait();

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});

		});
    }
	function UpdateLastLoginDateTime() {

		var today = new Date();
		var lastDate = new Date($scope.beData.LastLoginAt_AD);
		var diffMs = (today - lastDate); // milliseconds between now & Christmas
		var diffDays = Math.floor(diffMs / 86400000); // days
		var diffHrs = Math.floor((diffMs % 86400000) / 3600000); // hours
		var diffMins = Math.round(((diffMs % 86400000) % 3600000) / 60000); // minutes

		var msg = '';
		if (diffDays > 0)
			msg = diffDays + " days";

		msg = msg + diffHrs + " hrs, " + diffMins + " min";

		$timeout(function () {
			$scope.beData.LastLoginAt_HM = msg;
		});
		

		UpdateCurrentLoginDateTime();
	};

	function UpdateCurrentLoginDateTime() {

		var today = new Date();
		var lastDate = new Date($scope.beData.CurrentLoginAt_AD);
		var diffMs = (today - lastDate); // milliseconds between now & Christmas
		var diffDays = Math.floor(diffMs / 86400000); // days
		var diffHrs = Math.floor((diffMs % 86400000) / 3600000); // hours
		var diffMins = Math.round(((diffMs % 86400000) % 3600000) / 60000); // minutes

		var msg = '';
		if (diffDays > 0)
			msg = diffDays + " days";

		msg = msg + diffHrs + " hrs, " + diffMins + " min";

		$timeout(function () {
			$scope.beData.CurrentLoginAt_HM = msg;
		});
		

	};

	$scope.getHouseColor = function () {

		var ind = Math.floor(Math.random() * 5);
		if (ind < 5)
			return $scope.HouseColorCardList[ind];
		else
			return $scope.HouseColorCardList[0];
	}

	$scope.StudentAnalysisSections = function () {
		var canvas = document.getElementById("pie-chart");
		var ctx = canvas.getContext('2d');

		Chart.defaults.global.defaultFontColor = 'black';
		Chart.defaults.global.defaultFontSize = 14;
		var theHelp = Chart.helpers;

		var data = {
			labels: ["boys ", "girls"],
			datasets: [{
				fill: true,
				backgroundColor: [
					'#8BC56B',
					'#E8A39A'
				],
				data: [$scope.beData.Student_Male, $scope.beData.Student_Female],
				borderColor: ['#8BC56B', '#E8A39A'],
				borderWidth: [2, 2]
			}]
		};

		var options = {
			title: {
				display: false,
			},
			tooltips: {
				displayColors: false,
				backgroundColor: '#fff',
				bodyFontColor: '#000'
			},
			rotation: -0.7 * Math.PI,
			legend: {
				display: false,

			}
		};

		// Chart declaration:
		var myPieChart = new Chart(ctx, {
			type: 'pie',
			data: data,
			options: options
		});
	}

    var studentMonltyChart = null;
    $scope.GetStudentMonthlyAttendance = function (md) {

        //if (!md || md == 0) {
        //	$scope.StudentMonthlyAttendance();
        //} else if (md.FromDate) {
        var para = {
            //dateFrom: new Date(md.FromDate),
            //dateTo: new Date(md.ToDate)
            MonthId: $scope.beData.StudentAttendanceFor
        };
        $http({
            method: 'POST',
            url: base_url + "Dashboard/Common/GetStudentMonthlyAttendance",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                var monthlyColl = res.data.Data;

                var daysColl = [];
                var presentColl = [];
                var absentColl = [];


                var d = 1;
                angular.forEach(monthlyColl, function (m) {
                    daysColl.push(d);
                    presentColl.push(m.TotalPresent);
                    absentColl.push(m.TotalStudent - m.TotalPresent);
                    d++;
                });

                $timeout(function () {
                    var ctx = document.getElementById("attover2").getContext('2d');
                    var tmpDataSets = [{
                        label: 'Present',
                        backgroundColor: "#5BC22E",
                        data: presentColl,
                    }, {
                        label: 'Absent',
                        backgroundColor: "rgba(91, 194, 46, 0.3)",
                        data: absentColl,
                    }];
                    if (studentMonltyChart)
                        studentMonltyChart.destroy();

                    studentMonltyChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: daysColl,
                            datasets: tmpDataSets,
                        },
                        options: {
                            tooltips: {
                                backgroundColor: '#fff',
                                bodyFontColor: '#000',
                                titleFontColor: '#000',
                                displayColors: true,
                                callbacks: {
                                    mode: 'x',
                                },
                            },
                            scales: {
                                xAxes: [{
                                    stacked: true,
                                    gridLines: {
                                        display: false,
                                    }
                                }],
                                yAxes: [{
                                    stacked: true,
                                    ticks: {
                                        beginAtZero: true,
                                    },
                                    type: 'linear',
                                }]
                            },
                            responsive: true,
                            maintainAspectRatio: false,
                            legend: { position: 'bottom' },
                        }
                    });

                });


            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
        //}		
    };

	$scope.StudentMonthlyAttendance = function () {
		var ctx = document.getElementById("attendanceChart").getContext('2d');
		var labelColl = [];
		angular.forEach($scope.PeriodMonths, function (p) {
			labelColl.push(p.MonthName);
		});
		var presentColl = [];
		var absentColl = [];
		angular.forEach($scope.beData.StudentMonthlyAttColl, function (att) {
			presentColl.push(att.AvgStudentPresent);
			absentColl.push(att.TotalStudent-att.AvgStudentPresent);
		});

		var myChart = new Chart(ctx, {
			type: 'line',
			data: {
				labels: labelColl,
				datasets: [
					{
						// lineTension: 0,
						label: 'Present',
						borderColor: '#37B57F',
						backgroundColor: 'rgba(55, 181, 127, 0.1)',
						fill: true,
						data: presentColl,
						borderWidth: 0,
						pointStyle: 'line',
					}					
				]
			},
			options: {
				tooltips: {
					displayColors: false,
					backgroundColor: '#fff',
					bodyFontColor: '#000',
					titleFontColor: '#000',
					titleFontWeight: 'normal',
					footerFontColor: '#000',
					callbacks: {
						title: function (item, everything) {
							return;
						},
						label: function (item, everything) {
							let year = item.xLabel;
							let population = item.yLabel;
							population = population.toLocaleString();

							let label = 'Present:' + population + ' std';
							return label;
						},
						footer: function (item, everything) {
							let index = item[0].index;
							
							let absent = absentColl[index];
							
							return 'Absent: ' + absent + ' std';
						}
					}
				},

				plugins: {
					maintainAspectRatio: false,
					tooltip: {
						displayColors: false,
						backgroundColor: 'red',
						mode: 'index',
						intersect: false,
						callbacks: {
						}
					},
				},
				scales: {
					xAxes: [{
						stacked: true,
						barThickness: 10,
						maxBarThickness: 10,
						borderRadius: 5,
						gridLines: {
							display: false,
							zeroLineWidth: 0.5
						}
					}],
					yAxes: [{
						stacked: true,
						ticks: {
							beginAtZero: true,
							fontStyle: '400'
						},
						type: 'linear',
						gridLines: {
							display: true,
							lineWidth: 0.3,
							zeroLineWidth: 0.3
						}
					}],
					x: {
						stacked: true,
					},
					y: {
						stacked: true
					}

				},


				responsive: true,
				maintainAspectRatio: false,
				legend: {
					position: 'bottom', labels: {
						// usePointStyle: true,
					}
				},
			}
		});
	}

	$scope.LibraryBooks = function () {
		var bookQry = mx($scope.beData.PublicationWiseBookColl);
		$scope.beData.TotalBook = bookQry.sum(p1 => p1.NoOfBook);
		$scope.beData.TotalBookIssue = bookQry.sum(p1 => p1.Issues);
		$scope.beData.TotalBookYetReturn = bookQry.sum(p1 => p1.NotYetReturn);
		$scope.beData.TotalBookRetrun = $scope.beData.TotalBookIssue - $scope.beData.TotalBookYetReturn
		$scope.beData.TotalBookLost = bookQry.sum(p1 => p1.BookLost);
		$scope.beData.TotalBookBalance = bookQry.sum(p1 => p1.Balance);

		var valColl = [];
		valColl.push($scope.beData.TotalBookIssue);
		valColl.push($scope.beData.TotalBookRetrun);
		valColl.push($scope.beData.TotalBookLost);
		valColl.push($scope.beData.TotalBookBalance);
		valColl.push($scope.beData.TotalBookYetReturn);
		valColl.push(0);

		var config = {
			type: 'doughnut',
			data: {
				datasets: [{
					data: valColl,
					backgroundColor: [
						'#5250FA',
						'#00B800',
						'#D65653',
						'#7F8C8D',
						'#F4933C',
					],
				}],
				labels: [
					"Return",
					"Issue",
					"Lost/Damage",
					"Balance",
					"Yet To Retrun"
				]
			},
			options: {
				responsive: true,
				legend: {
					display: false,
					labels: {

						padding: 20
					},
				},
				tooltips: {
					displayColors: false,
					enabled: true,
					backgroundColor: '#fff',
					bodyFontColor: '#000',
					titleFontSize: 20,
					bodyFontSize: 20
				}
			}
		};

		var ctx = document.getElementById("chart-area").getContext("2d");
		var myPie = new Chart(ctx, config);

	}

	$scope.StaffAnalysis = function () {
		var canvas = document.getElementById("pie-chart2");
		var ctx = canvas.getContext('2d');

		Chart.defaults.global.defaultFontColor = 'black';
		Chart.defaults.global.defaultFontSize = 14;
		var theHelp = Chart.helpers;

		var valColl = [];
		valColl.push($scope.beData.Emp_T_Male + $scope.beData.Emp_NT_Male);
		valColl.push($scope.beData.Emp_T_Female + $scope.beData.Emp_NT_Female);
		
		

		var data = {
			labels: ["Women ", "Men"],
			datasets: [{
				fill: true,
				backgroundColor: [
					'#8BC56B',
					'#E8A39A'
				],
				data: valColl,
				borderColor: ['#8BC56B', '#E8A39A'],
				borderWidth: [2, 2]
			}]
		};

		var options = {
			title: {
				display: false,
			},
			tooltips: {
				displayColors: false,
				backgroundColor: '#fff',
				bodyFontColor: '#000'
			},
			rotation: -0.7 * Math.PI,
			legend: {
				display: false,
			}

		};

		// Chart declaration:
		var myPieChart = new Chart(ctx, {
			type: 'pie',
			data: data,
			options: options
		});
    }

	//Updated By Suresh on 21 falgun starts
	$scope.showAccountStatus = false;

	$scope.ChangeAccountStatus = function () {
		$scope.showAccountStatus = !$scope.showAccountStatus;
	}

	$scope.showFeesCollected = false;

	$scope.ChangeFeeStatus = function () {
		$scope.showFeesCollected = !$scope.showFeesCollected;
	}
	$scope.showAccountActivity = false;

	$scope.ChangeAccountActivityStatus = function () {
		$scope.showAccountActivity = !$scope.showAccountActivity;
	}
	$scope.showOverallFeeCollection = false;

	$scope.ChangeOverallFeeCollectionStatus = function () {
		$scope.showOverallFeeCollection = !$scope.showOverallFeeCollection;
	}
	$scope.showSMSBalance = false;
	//Updated by Shishant for SMS balance on December 5,2024
	$scope.GetSMSBalance = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$http({
			method: 'POST',
			url: base_url + "Global/GetSMSBalace",
			dataType: "json"
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.SMSBalance = JSON.parse(res.data.Data.ResponseMSG);
				//NEw Code added
				$scope.showSMSBalance = !$scope.showSMSBalance;
				//Ends
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	};

	$scope.LoadDOBStudent = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.StudentBirthdayList = [];
		var para = {
			dateFrom: ($scope.newBirthday.DateFromDet ? $scope.newBirthday.DateFromDet.dateAD : null),
			dateTo: ($scope.newBirthday.DateToDet ? $scope.newBirthday.DateToDet.dateAD : null),
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetStudentBirthDay",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess) {
				$scope.StudentBirthdayList = res.data.Data;
			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}


	$scope.LoadDOBE = function () {

		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.EmpBirthdayList = [];
		var para = {
			dateFrom: ($scope.newBirthdayE.DateFromDet ? $scope.newBirthdayE.DateFromDet.dateAD : null),
			dateTo: ($scope.newBirthdayE.DateToDet ? $scope.newBirthdayE.DateToDet.dateAD : null),
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetEmployeeBirthDay",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.EmpBirthdayList = res.data.Data;
			} else {
				alert(res.data.ResponseMSG)
				//Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			Swal.fire('Failed' + reason);
		});
	}



	var employeeMonltyChart = null;
	$scope.GetEmployeeMonthlyAttendance = function () {
		if ($scope.newFilter.EmployeeAttYearId && $scope.newFilter.EmployeeAttMonthId) {
			var para = {
				YearId: $scope.newFilter.EmployeeAttYearId,
				MonthId: $scope.newFilter.EmployeeAttMonthId,
			};
			$http({
				method: 'POST',
				url: base_url + "Dashboard/Common/GetEmployeeMonthlyAttendance",
				dataType: "json",
				data: JSON.stringify(para)
			}).then(function (res) {
				if (res.data.IsSuccess && res.data.Data) {
					var monthlyColl = res.data.Data;

					var daysColl = [];
					var presentColl = [];
					var absentColl = [];
					var lateColl = [];
					var leaveColl = [];

					var presentColors = [];
					var absentColors = [];
					var lateColors = [];
					var leaveColors = [];

					var d = 1;
                    angular.forEach(monthlyColl, function (m) {
                        daysColl.push(d);
                        presentColl.push(m.TotalPresent);
                        lateColl.push(m.TotalLate);
                        leaveColl.push(m.TotalLeave);
                        absentColl.push(m.TotalEmployee - m.TotalPresent - m.TotalLate - m.TotalLeave);

                        presentColors.push('#5BC22E');          // green
                        absentColors.push('rgba(91, 194, 46,0.3)'); // light green
                        lateColors.push('#FFA500');             // orange
                        leaveColors.push('#1E90FF');            // blue


                        d++;
                    });

					$timeout(function () {
						var ctx = document.getElementById("attendanceChartLine").getContext('2d');
						var tmpDataSets = [
							{ label: 'Present', backgroundColor: presentColors, data: presentColl },
							{ label: 'Late', backgroundColor: lateColors, data: lateColl },
							{ label: 'Leave', backgroundColor: leaveColors, data: leaveColl },
							{ label: 'Absent', backgroundColor: absentColors, data: absentColl },
						];

						if (employeeMonltyChart)
							employeeMonltyChart.destroy();

						employeeMonltyChart = new Chart(ctx, {
							type: 'bar',
							data: { labels: daysColl, datasets: tmpDataSets },
							options: {
								tooltips: {
									backgroundColor: '#fff',
									bodyFontColor: '#000',
									titleFontColor: '#000',
									displayColors: true,
									callbacks: { mode: 'x' },
								},
								scales: {
									xAxes: [{ stacked: true, gridLines: { display: false } }],
									yAxes: [{ stacked: true, ticks: { beginAtZero: true }, type: 'linear' }]
								},
								responsive: true,
								maintainAspectRatio: false,
								legend: { position: 'bottom' },
							}
						});
					});

				} else {
					Swal.fire(res.data.ResponseMSG);
				}

			}, function (reason) {
				Swal.fire('Failed' + reason);
			});
		}
	};



});