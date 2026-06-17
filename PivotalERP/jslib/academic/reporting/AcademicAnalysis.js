app.controller('dashboardAnalysisController', function ($scope, $http, $timeout, $filter, GlobalServices) {


	$scope.LoadData = function () {



		$scope.GetAcademicAnalysisReport();

	}
	function getRandomColor() {
		var letters = '0123456789ABCDEF';
		var color = '#';
		for (var i = 0; i < 6; i++) {
			color += letters[Math.floor(Math.random() * 16)];
		}
		return color;

	}

	$scope.GetAcademicAnalysisReport = function () {
		$scope.loadingstatus = "running";
		showPleaseWait();
		$scope.beData = {};
		var para = {
			//ClassShiftId: $scope.newFilter.ClassShiftId,
			//BranchId: null,
		};
		$http({
			method: 'POST',
			url: base_url + "Academic/Report/GetAcademicAnalysisReport",
			dataType: "json",
			data: JSON.stringify(para)
		}).then(function (res) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			if (res.data.IsSuccess && res.data.Data) {
				$scope.beData = res.data.Data;

				//Pie chart
				$timeout(function () {
					$scope.StudentRecordsOverview();
					$scope.StudentTypeDistribution();
					$scope.GenderDistribution();
					$scope.CasteDistribution();
					$scope.AgeWiseStudentAnalysis();
					$scope.StudentDisabilityOverview();
					$scope.EmployeeRecordsSummary();
					$scope.DepartmentWiseEmployee();
					$scope.TeacherOverview();
					$scope.CasteWiseEmployeeDistribution();
					$scope.AgeWiseEmployeeAnalysis();
					$scope.EmployeeDisability();
					$scope.StudentAndEmployeeDistribution();
					$scope.BirthdaySummary();
					$scope.RemarksOverview();
					$scope.ClassSetupOverview();
					$scope.CertificationSummary();
					$scope.ParentTeacherMeeting();
				}, 0);
			} else {
				Swal.fire(res.data.ResponseMSG);
			}
		}, function (reason) {
			hidePleaseWait();
			$scope.loadingstatus = "stop";
			Swal.fire('Failed: ' + reason.statusText);
		});
	};



	$scope.StudentRecordsOverview = function () {

		$scope.StudentRecords = {
			New: $scope.beData.StdRecordColl.map(x => x.TotalNewStudent),
			Old: $scope.beData.StdRecordColl.map(x => x.TotalOldStudent),
			Left: $scope.beData.StdRecordColl.map(x => x.TotalLeftStudent),
			Passout: $scope.beData.StdRecordColl.map(x => x.TotalPassoutStudent)
		};

		$timeout(function () {
			var canvas = document.getElementById("student-overview-chart");
			var ctx = canvas.getContext('2d');

			var data = {
				labels: $scope.beData.StdRecordColl.map(x => x.ClassName),
				datasets: [
					{
						label: "New",
						data: $scope.StudentRecords.New,
						fill: false, // No fill under the line
						borderColor: "#28a745",
						backgroundColor: "#28a745", // Solid color for the circle
						borderWidth: 5, // Increased thickness of the line
						pointRadius: 0, // No beads on the line
						tension: 0.4 //makes the line curved
					},
					{
						label: "Old",
						data: $scope.StudentRecords.Old,
						fill: false, // No fill under the line
						borderColor: "#007bff",
						backgroundColor: "#007bff",
						borderWidth: 5, // Increased thickness of the line
						pointRadius: 0, // No beads on the line
						tension: 0.4
					},
					{
						label: "Left",
						data: $scope.StudentRecords.Left,
						fill: false, // No fill under the line
						borderColor: "#dc3545",
						backgroundColor: "#dc3545", // Solid color for the circle
						borderWidth: 5, // Increased thickness of the line
						pointRadius: 0, // No beads on the line
						tension: 0.4
					},
					{
						label: "Passout",
						data: $scope.StudentRecords.Passout,
						fill: false, // No fill under the line
						borderColor: "#155724",
						backgroundColor: "#155724", // Solid color for the circle
						borderWidth: 5, // Increased thickness of the line
						pointRadius: 0, // No beads on the line
						tension: 0.4
					}
				]
			};

			var options = {
				responsive: true,
				legend: {
					display: true,
					position: 'bottom',
					labels: {
						usePointStyle: true,
						padding: 20,
						//boxWidth: 7,
						//fontSize: 12
					}
				},
				tooltips: { mode: 'index', intersect: false },
				hover: { mode: 'nearest', intersect: true },
				scales: {
					xAxes: [{
						display: true,
						gridLines: { display: true } // x-axis grid lines 
					}],
					yAxes: [{
						display: true,
						ticks: {
							beginAtZero: true,
							stepSize: 10,
							max: Math.max(...[].concat(
								$scope.StudentRecords.New,
								$scope.StudentRecords.Old,
								$scope.StudentRecords.Left,
								$scope.StudentRecords.Passout
							)) + 10
						}
					}],
					grid: 0
				}
			};

			new Chart(ctx, {
				type: 'line',
				data: data,
				options: options
			});
		}, 1000);
	};

	$scope.StudentTypeDistribution = function () {
		let labels = [];
		let boysData = [];
		let girlsData = [];

		$scope.beData.StdTpyDistColl.forEach(record => {
			labels.push(record.StudentTypeName);
			boysData.push(record.TotalSTBoys || 0);
			girlsData.push(record.TotalSTGirls || 0);
		});

		$timeout(function () {

			var canvas = document.getElementById("student-type-chart");
			var ctx = canvas.getContext('2d');

			var data = {
				labels: labels,
				datasets: [
					{
						type: 'line',
						label: 'Boys',
						data: boysData,
						fill: false,
						borderColor: "#8FBB79",
						backgroundColor: "#8FBB79",
						pointStyle: 'circle',
						pointRadius: 5,
						pointHoverRadius: 7,
						pointBackgroundColor: '#FFFFFF' // White beads
					},
					{
						type: 'bar',
						label: 'Girls',
						data: girlsData,
						backgroundColor: "#4367D2"
					}
				]
			};

			var options = {
				responsive: true,
				tooltips: { mode: 'index', intersect: false },
				hover: { mode: 'nearest', intersect: true },
				scales: {
					xAxes: [{ stacked: true }],
					yAxes: [{
						stacked: false,
						ticks: {
							beginAtZero: true,
							stepSize: 20,
							min: 0,
							max: Math.max(...boysData, ...girlsData) + 10
						}
					}]
				},
				legend: {
					display: true,
					position: 'bottom',
					labels: {
						usePointStyle: true, // Use circle instead of rectangle for legend markers
						padding: 20
					}
				},
				tooltips: {
					enabled: true
				}
			};

			new Chart(ctx, {
				type: 'bar',
				data: data,
				options: options
			});

		}, 1000);
	};

	$scope.GenderDistribution = function () {
		$timeout(function () {
			var canvas = document.getElementById("gender-chart");
			var ctx = canvas.getContext('2d');

			let labels = [];
			let boysData = [];
			let girlsData = [];

			$scope.beData.StdGenderDistColl.forEach(record => {
				labels.push(record.ClassNameStdGender);
				boysData.push(record.StdGenderBoys || 0);
				girlsData.push(record.StdGenderGirls || 0);
			});


			var data = {
				labels: labels,
				datasets: [
					{
						label: "Boys",
						data: boysData,
						backgroundColor: "#1e7e34"
					},
					{
						label: "Girls",
						data: girlsData,
						backgroundColor: "#28a745"
					}
				]
			};

			var options = {
				responsive: true,
				tooltips: {
					enabled: true,
					mode: 'index', // Show all values at a point
					intersect: false,
					callbacks: {
						label: function (tooltipItem, data) {
							var dataset = data.datasets[tooltipItem.datasetIndex];
							var value = dataset.data[tooltipItem.index];
							return `${dataset.label}: ${value}`;
						}
					}
				},
				hover: {
					mode: 'index',
					intersect: false
				},
				scales: {
					xAxes: [{
						stacked: true,
						barPercentage: 0.9,
						categoryPercentage: 0.5,
						ticks: {
							fontStyle: "normal",
							maxRotation: 45,
							minRotation: 45,
							autoSkip: false,
						}
					}],
					yAxes: [{
						ticks: {
							beginAtZero: true,
							stepSize: 10,
							min: 0,
							max: Math.max(10, ...boysData, ...girlsData) + 5 // Ensure a valid max value
						}
					}]
				},
				legend: {
					display: true,
					position: 'bottom',
					labels: {
						usePointStyle: true
					}
				}
			};

			new Chart(ctx, {
				type: 'bar',  // We are using a bar chart
				data: data,
				options: options
			});
		}, 1000);
	};

	$scope.CasteDistribution = function () {
		$timeout(function () {
			var canvas = document.getElementById('caste-chart');
			var ctx = canvas.getContext('2d');

			let labels = [];
			let casteData = [];
			var backgroundColors = [];

			

			$scope.beData.CasteStdColl.forEach((record, index) => {
				labels.push(record.StdCasteName);
				casteData.push(record.NoOfCasteStd || 0);
				backgroundColors.push(getRandomColor());
			});
			var data = {
				labels: labels,
				datasets: [{
					label: labels,
					data: casteData,
					backgroundColor: backgroundColors,
					borderWidth: 1
				}]
			};

			var options = {
				responsive: true,
				scales: {
					xAxes: [{
						ticks: {
							beginAtZero: true,
							stepSize: 5
						},
						gridLines: {
							display: false
						}
					}],
					yAxes: [{
						ticks: {
							beginAtZero: true,
							display: false
						},
						gridLines: {
							display: false
						}
					}]
				},
				legend: {
					display: true,
					position: 'bottom',
					labels: {
						usePointStyle: true  // Circular markers in the legend
					}
				},
				tooltips: {
					enabled: true,
					mode: 'nearest',
					intersect: false,
					callbacks: {
						label: function (tooltipItem, data) {
							var index = tooltipItem.index;
							var label = data.labels[index];
							var count = data.casteData[0].data[index];
							return `${label}: ${count} students`;
						}
					}
				}
			};

			new Chart(ctx, {
				type: 'horizontalBar',
				data: data,
				options: options
			});
		}, 1000);
	};

	$scope.AgeWiseStudentAnalysis = function () {
		$timeout(function () {
			var canvas = document.getElementById('student-age-chart');
			var ctx = canvas.getContext('2d');

			let labels = [];
			let boysData = [];
			let girlsData = [];

			$scope.beData.AgeWiseStdColl.forEach(record => {
				labels.push(record.StdAgeGp);
				boysData.push(record.StdTotalBoys || 0);
				girlsData.push(record.StdTotalGirls || 0);
			});

			var data = {
				labels: labels,
				datasets: [
					{
						label: 'Boys',
						data: boysData,
						borderColor: '#004085',
						backgroundColor: '#004085',
						fill: false,
						pointStyle: 'circle',
						pointRadius: 4,
						pointHoverRadius: 6
					},
					{
						label: 'Girls',
						data: girlsData,
						borderColor: '#007bff',
						backgroundColor: '#007bff',
						fill: false,
						pointStyle: 'circle',
						pointRadius: 4,
						pointHoverRadius: 6
					}
				]
			};


			var options = {
				responsive: true,
				maintainAspectRatio: false,
				tooltips: { mode: 'index', intersect: false },
				hover: { mode: 'nearest', intersect: true },
				legend: {
					position: 'bottom',
					labels: {
						usePointStyle: true, // Circular legends
						pointStyle: 'circle'
					}
				},
				scales: {
					xAxes: [{ display: true }],
					yAxes: [{
						display: true,
						ticks: {
							beginAtZero: true,
							stepSize: 10,
							max: Math.max(...boysData, ...girlsData) + 10
						}
					}]
				},
				title: {
					display: false
				}
			};

			new Chart(ctx, {
				type: 'line',
				data: data,
				options: options
			});
		}, 1000);
	};

	$scope.StudentDisabilityOverview = function () {
		$timeout(function () {
			var canvas = document.getElementById('student-disability-chart');
			var ctx = canvas.getContext('2d');

			var Boys = [];
			var Girls = [];
			var DisabilityLabels = [];

			$scope.beData.StdDisabiltyColl.forEach(data => {
				DisabilityLabels.push(data.StdPhysicalDisability);
				Boys.push(data.BoysWithDisability || 0);
				Girls.push(data.GirlsWithDisability || 0);

			});
			var data = {
				labels: DisabilityLabels, // Dynamic labels based on the disability types
				datasets: [
					{
						label: 'Boys',
						data: Boys,
						backgroundColor: '#6C8EBF'
					},
					{
						label: 'Girls',
						data: Girls,
						backgroundColor: '#957DCD'
					}
				]
			};

			var maxValue = Math.max(Math.max(...Boys), Math.max(...Girls));
			var options = {
				responsive: true,
				tooltips: { mode: 'index', intersect: false },
				hover: { mode: 'nearest', intersect: true },
				legend: {
					position: 'bottom',
					labels: {
						usePointStyle: true,
						pointStyle: 'circle',
					}
				},
				title: { display: false },
				scales: {
					xAxes: [{
						barPercentage: 0.9,
						categoryPercentage: 0.4,
						ticks: {
							fontStyle: 'normal',
							maxRotation: 0,
							minRotation: 0,
							autoSkip: false,
						}
					}],
					yAxes: [{
						ticks: {
							beginAtZero: true,
							stepSize: 20,
							min: 0,
							max: maxValue+5
						}
					}]
				},
				tooltips: {
					enabled: true,
					mode: 'nearest',
					intersect: true,
					callbacks: {
						label: function (tooltipItem, data) {
							var index = tooltipItem.index;

							var boys = data.datasets[0].data[index];
							var girls = data.datasets[1].data[index];

							return `Boys: ${boys}, Girls: ${girls}`;
						}
					}
				}
			};

			new Chart(ctx, {
				type: 'bar',
				data: data,
				options: options
			});
		}, 1000);
	};


	$scope.EmployeeRecordsSummary = function () {
		$timeout(function () {
			var canvas = document.getElementById('employee-record-chart');
			var ctx = canvas.getContext('2d');
			var employeeData = {
				New: $scope.beData.TotalNewJoining || 0,
				Left: $scope.beData.TotalLeftEmployee || 0,
				Teaching: $scope.beData.Teaching || 0,
				NonTeaching: $scope.beData.NotTeaching || 0
			};

			var labels = ['New', 'Left', 'Teaching', 'Non-Teaching'];
			var backgroundColors = ['#8A2BE2', '#FFA500', '#87CEEB', '#FFC0CB'];

			var data = {
				labels: labels,
				datasets: [{
					data: Object.values(employeeData),
					backgroundColor: backgroundColors
				}]
			};

			var options = {
				responsive: true,
				maintainAspectRatio: false,
				tooltips: { mode: 'index', intersect: false },
				hover: { mode: 'nearest', intersect: true },
				legend: {
					position: 'right',
					labels: {
						usePointStyle: true,
						pointStyle: 'circle'
					}
				},
				title: { display: false },
				scale: {
					ticks: {
						display: false // Hides numbers on the chart
					},
					gridLines: {
						display: false // Hides grid lines on the chart
					}
				}
			};

			new Chart(ctx, {
				type: 'polarArea',
				data: data,
				options: options
			});
		}, 1000);
	};

	$scope.DepartmentWiseEmployee = function () {
		$timeout(function () {
			var canvas = document.getElementById('employee-department-chart');
			var ctx = canvas.getContext('2d');

			var labels = [];
			var menData = [];
			var womenData = [];

			$scope.beData.DepartmentWiseEmpColl.forEach(function (department) {
				labels.push(department.WiseEmpDepartment);
				menData.push(department.DepartmentMaleEmp);
				womenData.push(department.DepartmentFemaleEmp);
			});

			var data = {
				labels: labels,
				datasets: [
					{
						label: 'Men',
						data: menData,
						borderColor: '#006400',
						backgroundColor: '#006400',
						fill: true,
						type: 'bar',
						barPercentage: 0.6
					},
					{
						label: 'Women',
						data: womenData,
						borderColor: '#90EE90',
						backgroundColor: 'rgba(144, 238, 144, 0.3)',
						fill: true,
						type: 'line',
						pointStyle: 'circle',
						pointRadius: 3
					}
				]
			};

			var options = {
				responsive: true,
				maintainAspectRatio: false,
				tooltips: { mode: 'index', intersect: false },
				hover: { mode: 'nearest', intersect: true },
				legend: {
					position: 'bottom',
					labels: {
						usePointStyle: true
					}
				},
				scales: {
					xAxes: [{
						barPercentage: 0.6
					}],
					yAxes: [{
						ticks: {
							beginAtZero: true,
							stepSize: 20,
							min: 0,
							max: Math.max(...menData.concat(womenData)) + 10 // Auto adjust max value
						}
					}]
				}
			};


			new Chart(ctx, {
				type: 'bar', // 'bar' overall type but will use 'line' and 'bar' types per dataset
				data: data,
				options: options
			});
		}, 1000);
	};

	$scope.TeacherOverview = function () {
		$timeout(function () {
			var canvas = document.getElementById('teacher-overview-chart');
			var ctx = canvas.getContext('2d');

			var labels = [];
			var teacherCounts = [];
			var backgroundColors = [];

			$scope.beData.LevelColl.forEach(function (level) {
				labels.push(level.LevelName);
				teacherCounts.push(level.NoOfLevel);
				backgroundColors.push(getRandomColor());
			});

			var data = {
				labels: labels,
				datasets: [{
					data: teacherCounts,
					backgroundColor: backgroundColors,
					borderWidth: 4
				}]
			};

			var options = {
				responsive: true,
				tooltips: {
					enabled: true,
					callbacks: {
						label: function (tooltipItem, data) {
							var index = tooltipItem.index;
							var label = data.labels[index];
							var value = data.datasets[tooltipItem.datasetIndex].data[index];
							return label + ': ' + value + ' Teachers';
						}
					}
				},
				hover: { mode: 'nearest', intersect: true },
				legend: {
					position: 'right',
					labels: {
						usePointStyle: true
					}
				},
				cutoutPercentage: 0 // Full pie chart without hole
			};

			// Create the pie chart
			new Chart(ctx, {
				type: 'pie', // Pie chart
				data: data,
				options: options
			});
		}, 1000);
	};

	$scope.CasteWiseEmployeeDistribution = function () {
		$timeout(function () {
			var canvas = document.getElementById('cast-employee-chart');
			var ctx = canvas.getContext('2d');

			var labels = [];
			var totalData = [];
			var backgroundColors = [];
			$scope.beData.CasteEmpColl.forEach(function (item) {
				labels.push(item.EmpCasteName); // Add caste name
				totalData.push(item.NoOfCasteEmp); // Add total number of employees in the caste
				backgroundColors.push(getRandomColor()); // Add random color (or use predefined)
			});
		

			var data = {
				labels: labels,
				datasets: [{
					label: labels,  // Representing the total of Boys and Girls for each caste
					data: totalData,  // Total number (Boys + Girls) for each caste
					backgroundColor: backgroundColors,
					borderWidth: 1
				}]
			};

			var options = {
				responsive: true,
				tooltips: {
					mode: 'index',
					intersect: false,
					callbacks: {
						// Customize tooltip to show Boys and Girls breakdown
						label: function (tooltipItem, data) {
							var label = data.labels[tooltipItem.index];
							var totalEmployees = data.totalData[tooltipItem.datasetIndex].data[tooltipItem.index];
							// Customize tooltip to show the total employees for each caste
							return label + ': ' + totalEmployees + ' Employees';
						}
					}
				},
				hover: {
					mode: 'nearest',
					intersect: true
				},
				scales: {
					xAxes: [{
						ticks: {
							beginAtZero: true,
							display: false
						},
						gridLines: {
							display: false
						}
					}],
					yAxes: [{
						ticks: {
							beginAtZero: true,
							display: true
						},
						gridLines: {
							display: false
						}
					}]
				},
				legend: {
					display: false
				}
			};

			new Chart(ctx, {
				type: 'horizontalBar',
				data: data,
				options: options
			});
		}, 1000);
	};

    $scope.AgeWiseEmployeeAnalysis = function () {
        $timeout(function () {
            var canvas = document.getElementById('age-employee-chart');
            var ctx = canvas.getContext('2d');

			var labels = [];
			var menData = [];
			var womenData = [];

			$scope.beData.AgeWiseEmpColl.forEach(function (item) {
				labels.push(item.EmpAgeGp); // Add age group name
				menData.push(item.EmpTotalMale); // Add total male employees for the age group
				womenData.push(item.EmpTotalFemale); // Add total female employees for the age group
			});

            var data = {
                labels: labels,
                datasets: [
                    {
                        label: 'Boys',
						data: menData,
                        borderColor: '#004085', // Dark blue for Boys
                        backgroundColor: 'rgba(0, 64, 133, 0.2)',
                        fill: true,
                        pointStyle: 'circle', // Use circular points for the data markers
                        pointRadius: 6, // Adjusted to 6 for a reasonable point size
                        pointBackgroundColor: '#FFFFFF'
                    },
                    {
                        label: 'Girls',
						data: womenData,
                        borderColor: '#007bff', // Light blue for Girls
                        backgroundColor: 'rgba(0, 123, 255, 0.2)',
                        fill: true,
                        pointStyle: 'circle', // Use circular points for the data markers
                        pointRadius: 6, // Adjusted to 6 for a reasonable point size
                        pointBackgroundColor: '#FFFFFF'
                    }
                ]
            };

            var options = {
                responsive: true,
                tooltips: { mode: 'index', intersect: false },
                hover: { mode: 'nearest', intersect: true },
                legend: {
                    position: 'bottom', // Position the legend below the chart
                    labels: {
                        usePointStyle: true // Using circles for legend markers
                    }
                },
                scales: {
                    xAxes: [{
                        display: true
                    }],
                    yAxes: [{
                        display: true,
                        ticks: {
                            beginAtZero: true,
                            stepSize: 20, // Set step size for y-axis
							max: Math.max(...menData.concat(womenData)) + 10,
                        }
                    }]
                },
                title: {
                    display: false
                }
            };

            new Chart(ctx, {
                type: 'line',
                data: data,
                options: options
            });

        }, 1000);
	};

	$scope.EmployeeDisability = function () {
		
		$timeout(function () {
			var canvas = document.getElementById('employee-disability-chart');
			var ctx = canvas.getContext('2d');
			var Male = [];
			var Female = [];
			var DisabilityLabels = [];

			$scope.beData.EmpDisabiltyColl.forEach(data => {
				DisabilityLabels.push(data.EmpPhysicalDisability);
				Male.push(data.MaleWithDisability || 0);
				Female.push(data.FemaleWithDisability || 0);

			});

			var data = {
				labels: DisabilityLabels,
				datasets: [
					{
						label: 'Men',
						data: Male,
						borderColor: '#007bff',
						backgroundColor: '#007bff',
						fill: false,
						pointStyle: 'circle',
						pointRadius: 0,
						pointBackgroundColor: '#ffffff'
					},
					{
						label: 'Women',
						data: Female,
						borderColor: '#dc3545',
						backgroundColor: '#dc3545',
						fill: false,
						pointStyle: 'circle',
						pointRadius: 0
					}
				]
			};

			var maxValue = Math.max(Math.max(...Male), Math.max(...Female));
			var options = {
				responsive: true,
				tooltips: { mode: 'index', intersect: false },
				hover: { mode: 'nearest', intersect: true },
				legend: {
					position: 'bottom', // Legend below the chart
					labels: {
						usePointStyle: true, // Use circles for legend markers
						pointStyle: 'circle' // Ensures the legend shows circular markers
					}
				},
				scales: {
					xAxes: [{
						barPercentage: 0.9,
						categoryPercentage: 0.4,
						display: true
					}],
					yAxes: [{
						display: true,
						ticks: {
							beginAtZero: true,
							stepSize: 20, // Set step size for y-axis
							max: maxValue+5 // Set the maximum value for y-axis
						}
					}]
				},
				title: {
					display: false
				}
			};

			new Chart(ctx, {
				type: 'line',
				data: data,
				options: options
			});

		}, 1000);
	};

	$scope.StudentAndEmployeeDistribution = function () {
		$timeout(function () {
			var ctx = document.getElementById("student-employee-chart").getContext('2d');
			var labels = [];
			var studentData = [];
			var employeeData = [];

			// Loop through ClassWiseStdEmpColl and populate the data arrays
			$scope.beData.ClassWiseStdEmpColl.forEach(function (item) {
				labels.push(item.StdEmpClassName); // Class names
				studentData.push(item.TotalStdClass); // Total students in each class
				employeeData.push(item.TotalEmpClass); // Total employees in each class
			});

			var data = {
				labels: labels,
				datasets: [
					{
						label: 'Students',
						data: studentData,
						backgroundColor: '#066A4B', // Student color
						stack: 'stack1'
					},
					{
						label: 'Employees',
						data: employeeData,
						backgroundColor: '#13A97A', // Employee color
						stack: 'stack1'
					}
				]
			};

			var options = {
				responsive: true,
				tooltips: { mode: 'index', intersect: false },
				hover: { mode: 'nearest', intersect: true },
				scales: {
					yAxes: [{
						ticks: {
							beginAtZero: true,
							stepSize: 20,
							max: Math.max(...studentData.concat(employeeData)) + 10,
						}
					}],
					xAxes: [{
						stacked: true
					}]
				},
				legend: {
					position: 'bottom', // Position the legend below the chart
					labels: {
						usePointStyle: true // Using circles for legend markers
					}
				}
			};

			// Creating the stacked bar chart
			new Chart(ctx, {
				type: 'bar',
				data: data,
				options: options
			});

		}, 1000);
	};

	$scope.BirthdaySummary = function () {

		$timeout(function () {
			var ctx = document.getElementById("birthday-summary-chart").getContext('2d');

			var labels = [];
			var studentData = [];
			var employeeData = [];
			var backgroundColors = [];
			var combinedData = [];

			// Loop through BirthdaySummaryColl and populate the data arrays
			$scope.beData.BirthdaySummaryColl.forEach(function (item) {
				labels.push(item.BDayMonthName); // Month name (e.g., Jan, Feb)
				studentData.push(item.TotalStdBDay); // Total students' birthdays
				employeeData.push(item.TotalEmpBDay); // Total employees' birthdays

				backgroundColors.push(getRandomColor());
				// Combined data for the chart (student + employee)
				combinedData.push(item.TotalStdBDay + item.TotalEmpBDay);
			});

			var data = {
				labels: labels,
				datasets: [{
					label: 'Birthday Summary (Student + Employee)',
					data: combinedData, // Total for each month (student + employee)
					backgroundColor: backgroundColors, // Different color for each month
					borderWidth: 1,
					borderColor: '#fff', // White border for segments
				}]
			};

			var options = {
				responsive: true,
				tooltips: { mode: 'index', intersect: false },
				hover: { mode: 'nearest', intersect: true },
				cutoutPercentage: 50, // Hole in the middle for the doughnut
				rotation: -Math.PI / 2, // Start the first segment from the top
				circumference: Math.PI * 2, // Full circle
				tooltips: {
					callbacks: {
						label: function (tooltipItem, data) {
							// Tooltip will show the month label + combined student + employee data
							var student = studentData[tooltipItem.index];
							var employee = employeeData[tooltipItem.index];
							return data.labels[tooltipItem.index] + ': ' +
								'Students: ' + student + 'Employees: ' + employee;
						}
					}
				},
				legend: {
					position: 'right', // Legend on the right
					labels: {
						usePointStyle: true, // Using circle style for legend
						pointStyle: 'circle'
					}
				}
			};

			// Create the doughnut chart
			new Chart(ctx, {
				type: 'doughnut',
				data: data,
				options: options
			});
		}, 1000);
	};

	$scope.RemarksOverview = function () {
		$timeout(function () {
			var ctx = document.getElementById("remarks-overview-chart").getContext('2d');

			var labels = [];
			var studentData = [];
			var employeeData = [];

			// Loop through RemarkStdEmpColl to populate the data arrays
			$scope.beData.RemarkStdEmpColl.forEach(function (item) {
				labels.push(item.RemarkMonthName); // Month name (e.g., Jan, Feb)
				studentData.push(item.TotalStdRemark); // Total student remarks
				employeeData.push(item.TotalEmpRemark); // Total employee remarks
			});
			var data = {
				labels: labels,
				datasets: [
					{
						label: 'Students',
						data: studentData,
						borderColor: '#90EE90',
						backgroundColor: '#90EE90',
						fill: false,
						pointStyle: 'circle',  // Circular points
						pointRadius: 0 // Adjusts point size
					},
					{
						label: 'Employees',
						data: employeeData,
						borderColor: '#006400',
						backgroundColor: '#006400',
						fill: false,
						pointStyle: 'circle',  // Circular points
						pointRadius: 0
					}
				]
			};

			var options = {
				responsive: true,
				tooltips: {
					mode: 'nearest',
					intersect: false,
					callbacks: {
						// Custom title for each tooltip
						title: function (tooltipItem, data) {
							return data.labels[tooltipItem[0].index];
						},
						// Custom label that combines both 'Student' and 'Employee' data
						label: function (tooltipItem, data) {
							var index = tooltipItem.index;
							var studentData = data.datasets[0].data[index];
							var employeeData = data.datasets[1].data[index];

							return `${data.datasets[0].label}: ${studentData}, ${data.datasets[1].label}: ${employeeData}`;
						}
					}
				},
				hover: { mode: 'nearest', intersect: true },
				scales: {
					xAxes: [{
						ticks: {
							fontStyle: "normal",
							maxRotation: 0,
							minRotation: 0,
							autoSkip: false, // Ensure all labels are shown
						}
					}],
					yAxes: [{
						ticks: {
							beginAtZero: true,
							stepSize: 10,
							min: 0,
							max: Math.max(...studentData.concat(employeeData)) + 10,
						}
					}]
				},
				legend: {
					display: true,
					position: 'bottom',
					labels: {
						usePointStyle: true
					}
				}
			};

			new Chart(ctx, {
				type: 'line',
				data: data,
				options: options
			});
		}, 1000);
	};


	$scope.ClassSetupOverview = function () {
		$scope.ClassSetupDet = {
			One: 10,
			Two: 20,
			Three: 30,
			Four: 40,
			Five: 50,
			Six: 60,
			Seven: 70,
			Eight: 80,
			Nine: 90,
			Ten: 100,
			Plus2: 110,
			BA: 120
		};

		$timeout(function () {
			var canvas = document.getElementById('class-overview-chart');
			var ctx = canvas.getContext('2d');

			var labels = ['One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine', 'Ten', '+2', 'BA'];

			var data = {
				labels: labels,
				datasets: [
					{
						data: [
							$scope.ClassSetupDet.One, $scope.ClassSetupDet.Two, $scope.ClassSetupDet.Three,
							$scope.ClassSetupDet.Four, $scope.ClassSetupDet.Five, $scope.ClassSetupDet.Six,
							$scope.ClassSetupDet.Seven, $scope.ClassSetupDet.Eight, $scope.ClassSetupDet.Nine,
							$scope.ClassSetupDet.Ten, $scope.ClassSetupDet.Plus2, $scope.ClassSetupDet.BA
						],
						backgroundColor: [
							'#007bff', '#6610f2', '#6f42c1', '#e83e8c', '#dc3545', '#fd7e14', '#ffc107',
							'#28a745', '#20c997', '#17a2b8', '#6c757d', '#343a40'
						]
					}
				]
			};

			var options = {
				responsive: true,
				tooltips: { mode: 'index', intersect: false },
				hover: { mode: 'nearest', intersect: true },
				//plugins: {
				//	// Apply the 3D effect
				//	threeD: {
				//		enabled: true,
				//		angle: 30,  // Adjust the angle of the 3D effect
				//		rotate: false,  // Option to make the chart rotate
				//		scale: 0.8      // Adjust scale for 3D effect
				//	}
				//},
				legend: {
					position: 'right',
					labels: {
						usePointStyle: true,
						pointStyle: 'circle'
					}
				},
				cutoutPercentage: 0,  // No inner cutout
				borderWidth: 0,
			};

			// Create the Chart with the given configuration and 3D plugin enabled
			new Chart(ctx, {
				type: 'pie',  // Type of chart (Pie)
				data: data,
				options: options
			});
		}, 1000);
	};

	$scope.CertificationSummary = function () {
		$timeout(function () {
			var canvas = document.getElementById("certificate-summary-chart");
			var ctx = canvas.getContext('2d');

			var labels = [];
			var tcData = [];
			var ccData = [];
			var extraData = [];

			// Loop through CretificateSummaryColl to populate the data arrays
			$scope.beData.CretificateSummaryColl.forEach(function (item) {
				labels.push(item.CertiMonthName); // Month name (e.g., Jan, Feb)
				tcData.push(item.TrasnferCertificates); // Total Transfer Certificates (TC)
				ccData.push(item.CharacterCertificates); // Total Character Certificates (CC)
				extraData.push(item.ExtraCertificates); // Total Extra Certificates
			});

			var data = {
				labels: labels,
				datasets: [
					{
						label: 'Extra',
						data: extraData,
						backgroundColor: 'transparent',
						borderColor: '#DF7C35',
						backgroundColor: '#DF7C35',
						borderWidth: 2,
						fill: false,
						type: 'line',
						tension: 0.1,
						pointRadius: 3,
						pointBackgroundColor: '#FFFFFF'
					},
					{
						label: 'TC',
						data: tcData,
						backgroundColor: '#4367D3', // Blue color for TC
						borderColor: '#4367D3', // Blue color for TC
						barPercentage: 0.8,  // Compact bars
						categoryPercentage: 0.5
					},
					{
						label: 'CC',
						data: ccData,
						backgroundColor: '#76BE4E', // Green color for CC
						borderColor: '#76BE4E', // Green color for CC
						barPercentage: 0.8,  // Compact bars
						categoryPercentage: 0.5
					}
				]
			};

			var options = {
				responsive: true,
				tooltips: { mode: 'index', intersect: false },
				hover: { mode: 'nearest', intersect: true },
				scales: {
					xAxes: [{
						ticks: {
							fontStyle: "normal",
							maxRotation: 0,
							minRotation: 0,
							autoSkip: false, // Ensure all labels are shown
						}
					}],
					yAxes: [{
						ticks: {
							beginAtZero: true,
							stepSize: 20,
							min: 0,
							max: Math.max(...tcData.concat(ccData, extraData)) + 10,

						}
					}]
				},
				legend: {
					display: true,
					position: 'bottom', // Position legend at bottom
					labels: {
						usePointStyle: true, // Use circular points
						boxWidth: 10 // Circle size for the legend
					}
				},
				tooltips: { enabled: true }
			};

			new Chart(ctx, {
				type: 'bar',
				data: data,
				options: options
			});
		}, 1000);
	};

	$scope.ParentTeacherMeeting = function () {
		$timeout(function () {
			var canvas = document.getElementById("parent-teacher-chart");
			var ctx = canvas.getContext('2d');

			var labels = [];
			var completedData = [];
			$scope.beData.PTMDetailsColl.forEach(function (item) {
				labels.push(item.MonthName);
				completedData.push(item.TotalMeetings || 0);
			});

			var data = {
				labels: labels,
				datasets: [
					{
						label: 'Completed Meetings',
						data: completedData,
						borderColor: '#006400',
						backgroundColor: '#006400',
						fill: false,
						pointRadius: 4,
						pointBackgroundColor: '#ffffff',
						tension: 0 // Straight line
					}
				]
			};
			var options = {
				responsive: true,
				tooltips: { mode: 'index', intersect: false },
				hover: { mode: 'nearest', intersect: true },
				scales: {
					xAxes: [{
						ticks: {
							fontStyle: "normal",
							maxRotation: 0,
							minRotation: 0,
							autoSkip: false, // Ensure all labels are shown
						}
					}],
					yAxes: [{
						ticks: {
							beginAtZero: true,
							stepSize: 20,
							min: 0,
							max: completedData.length ? Math.max(...completedData) + 10 : 40
						}
					}]
				},
				legend: {
					display: true,
					position: 'bottom', // Position legend at bottom
					labels: {
						usePointStyle: true, // Use circular points
						boxWidth: 10 // Circle size for the legend
					}
				},
				tooltips: { enabled: true }
			};

			new Chart(ctx, {
				type: 'line',
				data: data,
				options: options
			});
		}, 1000);
	};


});