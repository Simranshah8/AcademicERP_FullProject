app.controller('AttendanceDashBoardController', function ($scope, $http, $timeout) {
    $scope.Title = 'Attendance Dashboard';

    $scope.LoadData = function () {
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
        $scope.GetAttendanceDashboard();
    };

    $scope.GetAttendanceDashboard = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.beData = {};

        $http({
            method: 'POST',
            url: base_url + "Attendance/Report/GetAllAttendance",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.IsSuccess && res.data.Data) {
                $scope.beData = res.data.Data;

                $timeout(function () {
                    $scope.TodayStudentAttendance();
                    $scope.TodayEmployeeAttendance();
                    $scope.WeeklyStudentAttendanceOverview();
                    $scope.MonthlyStudentAttendanceOverview();
                    $scope.WeeklyEmployeeAttendanceOverview();
                    $scope.MonthlyEmployeeAttendanceOverview();
                    $scope.LeaveRequestsOverview();
                    $scope.MaxConsecutiveAbsence();
                    $scope.AbsenteeismTrends();
                    $scope.LateAttendanceTracker();
                    $scope.MinimumAttendanceCompliance();
                    $scope.AbsenceFineOverview();
                    $scope.StudentAttendanceAlerts();
                    $scope.EmployeeAttendanceAlerts();
                }, 500);


            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire('Failed: ' + reason.statusText);
        });
    };

    $scope.TodayStudentAttendance = function () {
        if (!$scope.beData || !$scope.beData.StudentAttendancecoll || !$scope.beData.StudentAttendancecoll.length) {
            console.warn("beData or StudentAttendancecoll not ready yet for chart.");
            return;
        }

        const canvas = document.getElementById('student-overview-chart');
        if (!canvas) {
            console.warn("Canvas with ID 'student-overview-chart' not found.");
            return;
        }

        const ctx = canvas.getContext('2d');

        // Initialize arrays for labels and data
        const labels = [];
        const present = [];
        const absent = [];

        // Loop through the StudentAttendancecoll data to populate labels and data
        $scope.beData.StudentAttendancecoll.forEach(function (student) {
            labels.push(student.ClassName || "Class");
            present.push(student.TotalSTDPresent || 0);
            absent.push(student.TotalSTDAbsent || 0);
        });

        const data = {
            labels: labels,
            datasets: [
                {
                    label: "Present",
                    data: present,
                    backgroundColor: "#003f87",
                    borderColor: "#003f87",
                    borderWidth: 1
                },
                {
                    label: "Absent",
                    data: absent,
                    backgroundColor: "#8BC34A",
                    borderColor: "#8BC34A",
                    borderWidth: 1
                }
            ]
        };

        const options = {
            responsive: true,
            legend: {
                display: true,
                position: 'bottom',
                labels: {
                    usePointStyle: false,
                    padding: 20
                }
            },
            tooltips: { mode: 'index', intersect: false },
            hover: { mode: 'nearest', intersect: true },
            scales: {
                xAxes: [{
                    stacked: true,
                    gridLines: { display: true }
                }],
                yAxes: [{
                    stacked: true,
                    ticks: {
                        beginAtZero: true,
                        stepSize: 10,
                        suggestedMax: Math.max(...present, ...absent) + 10
                    }
                }]
            }
        };

        new Chart(ctx, {
            type: 'bar',
            data: data,
            options: options
        });
    };

    $scope.TodayEmployeeAttendance = function () {
        $timeout(function () {
            const canvas = document.getElementById("attendance-overview-chart");
            if (!canvas) return;

            const ctx = canvas.getContext('2d');

            // Retrieve the department name and attendance data
            const departments = [];
            const present = [];
            const absent = [];

            // Loop through the Employeeattendancecoll data to populate the arrays
            $scope.beData.Employeeattendancecoll.forEach(function (employee) {
                departments.push(employee.DepartmentName || "Department");
                present.push(employee.TotalEMPPresent || 0);
                absent.push(employee.TotalEMPAbsent || 0);
            });

            const data = {
                labels: departments,  // Labels for X-axis (Department names)
                datasets: [
                    {
                        label: "Present",
                        data: present,  // Data for present employees
                        borderColor: "#E74C3C",
                        backgroundColor: "rgba(231, 76, 60, 0.2)",
                        pointStyle: 'circle',
                        pointRadius: 6,
                        pointHoverRadius: 8,
                        pointBackgroundColor: "#FFFFFF",
                        tension: 0.4,
                        fill: true
                    },
                    {
                        label: "Absent",
                        data: absent,  // Data for absent employees
                        borderColor: "#F39C12",
                        backgroundColor: "rgba(243, 156, 18, 0.2)",
                        pointStyle: 'circle',
                        pointRadius: 6,
                        pointHoverRadius: 8,
                        pointBackgroundColor: "#FFFFFF",
                        tension: 0.4,
                        fill: true
                    }
                ]
            };

            const options = {
                responsive: true,
                tooltips: {
                    mode: 'index',
                    intersect: false,
                    callbacks: {
                        label: function (tooltipItem, chartData) {
                            const datasetLabel = chartData.datasets[tooltipItem.datasetIndex].label || '';
                            const value = tooltipItem.yLabel;
                            return datasetLabel + ": " + value;
                        }
                    }
                },
                hover: { mode: 'nearest', intersect: true },
                scales: {
                    xAxes: [{
                        display: true,
                        gridLines: { display: false }
                    }],
                    yAxes: [{
                        display: true,
                        ticks: {
                            beginAtZero: true,
                            stepSize: 10,
                            suggestedMax: Math.max(...present, ...absent) + 10
                        }
                    }]
                },
                legend: {
                    display: true,
                    position: 'bottom',
                    labels: {
                        usePointStyle: true,
                        padding: 20
                    }
                }
            };

            new Chart(ctx, {
                type: 'line',
                data: data,
                options: options
            });
        }, 500);
    };


    $scope.WeeklyStudentAttendanceOverview = function () {
        const canvas = document.getElementById('pieChart');
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        const dataPoint = $scope.beData.WeeklyStudentcoll?.[0] || {};

        // Extract data from model
        const boysPresent = dataPoint.WeeklyPresentSTDBoys || 0;
        const girlsPresent = dataPoint.WeeklyPresentSTDGirls || 0;
        const boysAbsent = dataPoint.WeeklyAbsentSTDBoys || 0;
        const girlsAbsent = dataPoint.WeeklyAbsentSTDGirls || 0;

        // Set total values in scope for display in HTML
        $scope.TotalSTDPresent = boysPresent + girlsPresent;
        $scope.TotalSTDAbsent = boysAbsent + girlsAbsent;
        $scope.WeeklyPresentBoys = boysPresent;
        $scope.WeeklyPresentGirls = girlsPresent;
        $scope.WeeklyAbsentBoys = boysAbsent;
        $scope.WeeklyAbsentGirls = girlsAbsent;

        // Render the pie chart (for present only)
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: ["Boys", "Girls"],
                datasets: [{
                    data: [boysPresent, girlsPresent],
                    backgroundColor: ['#C96D61', '#8BC56B'],
                    borderColor: ['#ffffff', '#ffffff'],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                legend: {
                    display: false,
                    position: 'bottom'
                }
            }
        });
    };

    $scope.MonthlyStudentAttendanceOverview = function () {
        const canvas = document.getElementById('pieChart2');
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        const dataPoint = $scope.beData.MonthlyStudentcoll?.[0] || {};

        // Extract data from model
        const boysPresent = dataPoint.MonthlyPresentSTDBoys || 0;
        const girlsPresent = dataPoint.MonthlyPresentSTDGirls || 0;
        const boysAbsent = dataPoint.MonthlyAbsentSTDBoys || 0;
        const girlsAbsent = dataPoint.MonthlyAbsentSTDGirls || 0;
        const totalStudents = dataPoint.MonthlyTotalStudents || 0;

        // Store values in scope for HTML binding
        $scope.MonthlyTotalStudents = totalStudents;
        $scope.MonthlyTotalPresent = boysPresent + girlsPresent;
        $scope.MonthlyPresentBoys = boysPresent;
        $scope.MonthlyPresentGirls = girlsPresent;
        $scope.MonthlyTotalAbsent = boysAbsent + girlsAbsent;
        $scope.MonthlyAbsentBoys = boysAbsent;
        $scope.MonthlyAbsentGirls = girlsAbsent;

        // Draw pie chart (Present)
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: ["Boys", "Girls"],
                datasets: [{
                    data: [boysPresent, girlsPresent],
                    backgroundColor: ['#C96D61', '#8BC56B'],
                    borderColor: ['#ffffff', '#ffffff'],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                legend: {
                    display: false,
                    position: 'bottom'
                }
            }
        });
    };

    $scope.WeeklyEmployeeAttendanceOverview = function () {
        const canvas = document.getElementById('pieChart3');
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        const dataPoint = $scope.beData.weeklyEmployeecoll?.[0] || {};

        // Extract data from model
        const menPresent = dataPoint.WeeklyPresentEmpBoys || 0;
        const womenPresent = dataPoint.WeeklyPresentEmpGirls || 0;
        const menAbsent = dataPoint.WeeklyAbsentEmpBoys || 0;
        const womenAbsent = dataPoint.WeeklyAbsentEmpGirls || 0;

        // Set totals in scope if you want to bind to HTML
        $scope.TotalEMPPresent = menPresent + womenPresent;
        $scope.TotalEMPAbsent = menAbsent + womenAbsent;
        $scope.WeeklyPresentEmpBoys = menPresent;
        $scope.WeeklyPresentEmpGirls = womenPresent;
        $scope.WeeklyAbsentEmpBoys = menAbsent;
        $scope.WeeklyAbsentEmpGirls = womenAbsent;

        // Render pie chart (present only)
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: ["Men", "Women"],
                datasets: [{
                    data: [menPresent, womenPresent],
                    backgroundColor: ['#C96D61', '#8BC56B'],
                    borderColor: ['#ffffff', '#ffffff'],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                legend: {
                    display: false,
                    position: 'bottom'
                }
            }
        });
    };

    $scope.MonthlyEmployeeAttendanceOverview = function () {
        const canvas = document.getElementById('pieChart4');
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        const dataPoint = $scope.beData.monthlyEmployeecoll?.[0] || {};  // Ensure this is populated correctly

        // Ensure the values are defined and set to zero if they are missing
        const boysPresent = dataPoint.MonthlyPresentEMPBoys || 0;
        const girlsPresent = dataPoint.MonthlyPresentEMPGirls || 0;
        const boysAbsent = dataPoint.MonthlyAbsentEMPBoys || 0;
        const girlsAbsent = dataPoint.MonthlyAbsentEMPGirls || 0;

        // Store values in scope to bind in HTML
        $scope.TotalEMPPresentMonthly = boysPresent + girlsPresent;
        $scope.TotalEMPAbsentMonthly = boysAbsent + girlsAbsent;
        $scope.MonthlyPresentEmpBoys = boysPresent;
        $scope.MonthlyPresentEmpGirls = girlsPresent;
        $scope.MonthlyAbsentEmpBoys = boysAbsent;
        $scope.MonthlyAbsentEmpGirls = girlsAbsent;

        // Render the pie chart for present data
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: ["Men", "Women"],
                datasets: [{
                    data: [boysPresent, girlsPresent],
                    backgroundColor: ['#C96D61', '#8BC56B'],
                    borderColor: ['#ffffff', '#ffffff'],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                legend: { display: false, position: 'bottom' }
            }
        });
    };


    $scope.LeaveRequestsOverview = function () {
        const canvas = document.getElementById('pieChart5');
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        const dataPoint = $scope.beData.Leaverequestcoll?.[0] || {};

        // Extract values from data
        const boysLeave = dataPoint.StudentLeaveBoys || 0;
        const girlsLeave = dataPoint.StudentLeaveGirls || 0;
        const empBoysLeave = dataPoint.EmployeeLeaveBoys || 0;
        const empGirlsLeave = dataPoint.EmployeeLeaveGirls || 0;

        // New additional values
        $scope.TotalStudents = dataPoint.totalstudents || 0;
        $scope.TotalEmployees = dataPoint.totalEmployees || 0;

        $scope.TotalSTDLeave = dataPoint.totalSTDleave || 0;
        $scope.TotalEMPLeave = dataPoint.totalEMPleave || 0;

        $scope.StudentLeaveBoys = boysLeave;
        $scope.StudentLeaveGirls = girlsLeave;
        $scope.EmployeeLeaveBoys = empBoysLeave;
        $scope.EmployeeLeaveGirls = empGirlsLeave;

        $scope.PendingLeave = dataPoint.PendingLeave || 0;
        $scope.TodayLeave = dataPoint.Today || 0;
        $scope.WeeklyLeave = dataPoint.Weekly || 0;
        $scope.MonthlyLeave = dataPoint.monthly || 0;

        // Chart showing only Student leave (for visual simplicity)
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: ["Boys", "Girls"],
                datasets: [{
                    data: [boysLeave, girlsLeave],
                    backgroundColor: ['#C96D61', '#8BC56B'],
                    borderColor: ['#ffffff', '#ffffff'],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                legend: {
                    display: false,
                    position: 'bottom'
                }
            }
        });
    };

    $scope.MaxConsecutiveAbsence = function () {
        // This assumes your response data has Maxabsencecoll: List<MaxConsecutiveAbsence>
        const absenceData = $scope.beData.Maxabsencecoll || [];

        // Map this to another scope var (used in ng-repeat)
        $scope.MaxAbsenceList = absenceData.map(abs => {
            return {
                StudentName: abs.StudentNameMCA || 'N/A',
                PhotoPath: abs.PhotoPathMCA ? abs.PhotoPathMCA.replace(/\\/g, '/') : '/wwwroot/dynamic/images/avatar-img.png',
                ClassName: abs.ClassMCA || 'N/A',
                AbsentDays: abs.AbsentDays || 0
            };
        });


    };
    $scope.AbsenteeismTrends = function () {
        const canvas = document.getElementById('Absenteeism-Trends-chart');
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        const trendData = $scope.beData.Absenttrendcoll || []; // Ensure this is populated correctly

        // Group data for the chart
        const labels = trendData.map(item => item.DataIndex || "Unknown"); // Default to "Unknown" if DataIndex is missing
        const absentData = trendData.map(item => item.absentdata || 0); // Default to 0 if absentdata is missing

        new Chart(ctx, {
            type: 'line', // Using line chart type
            data: {
                labels: labels, // X-axis will display DateIndex (days)
                datasets: [
                    {
                        label: 'Absent Data',
                        data: absentData, // Y-axis values are the absent data
                        borderColor: '#4CAF50', // Line color for absent data
                        backgroundColor: 'rgba(76, 175, 80, 0.1)', // Transparent fill color for the line
                        borderWidth: 3,
                        fill: false, // Do not fill the area under the line
                        tension: 0.4, // Smooth line
                        pointStyle: 'circle',
                        pointRadius: 4,
                        pointHoverRadius: 6
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                tooltips: {
                    mode: 'index',
                    intersect: false,
                    callbacks: {
                        label: function (tooltipItem, data) {
                            const datasetLabel = data.datasets[tooltipItem.datasetIndex].label;
                            const value = tooltipItem.yLabel;
                            return `${datasetLabel}: ${value}`; // Display the absent data value in the tooltip
                        }
                    }
                },
                hover: {
                    mode: 'nearest',
                    intersect: true
                },
                legend: {
                    display: true,
                    position: 'bottom',
                    labels: {
                        usePointStyle: true,
                        padding: 20
                    }
                },
                scales: {
                    xAxes: [{
                        gridLines: { display: false },
                        ticks: {
                            autoSkip: false, // Make sure all labels (DateIndex) are shown
                            maxRotation: 45, // Rotate labels if needed to avoid overlap
                            minRotation: 45
                        }
                    }],
                    yAxes: [{
                        ticks: {
                            beginAtZero: true, // Start the Y-axis from 0
                            stepSize: 1, // Set the step size for Y-axis
                            max: Math.max(...absentData) + 1 // Set the Y-axis max dynamically based on data
                        }
                    }]
                }
            }
        });
    };



    $scope.LateAttendanceTracker = function () {
        const canvas = document.getElementById('pieChart6');
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        const dataPoint = $scope.beData.lateattendancecoll?.[0] || {};

        const studentLeave = dataPoint.totalleaveSTD || 0;
        const employeeLeave = dataPoint.totalleaveEMP || 0;

        // Optional: store totals in $scope if you want to display them elsewhere in HTML
        $scope.TotalLateLeave = dataPoint.Totalleave || (studentLeave + employeeLeave);
        $scope.StudentLateLeave = studentLeave;
        $scope.EmployeeLateLeave = employeeLeave;

        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: ["Students", "Employees"],
                datasets: [{
                    data: [studentLeave, employeeLeave],
                    backgroundColor: ['#C96D61', '#8BC56B'],
                    borderColor: ['#ffffff', '#ffffff'],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                legend: {
                    display: false,
                    position: 'bottom'
                }
            }
        });
    };

    $scope.MinimumAttendanceCompliance = function () {
        const canvas = document.getElementById('pieChart7');
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        const dataPoint = $scope.beData.compilancecoll?.[0] || {};

        // Extracting values for chart and UI
        const studentCompliance = dataPoint.Students || 0;
        const employeeCompliance = dataPoint.Employee || 0;
        const totalCompliance = dataPoint.TotalComplliance || (studentCompliance + employeeCompliance);

        // Bind to scope for HTML
        $scope.TotalCompliance = totalCompliance;
        $scope.StudentCompliance = studentCompliance;
        $scope.EmployeeCompliance = employeeCompliance;

        // Pie Chart
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: ["Students", "Employees"],
                datasets: [{
                    data: [studentCompliance, employeeCompliance],
                    backgroundColor: ['#C96D61', '#8BC56B'],
                    borderColor: ['#ffffff', '#ffffff'],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                legend: { display: false, position: 'bottom' }
            }
        });
    };

    $scope.AbsenceFineOverview = function () {
        const canvas = document.getElementById('pieChart8');
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        const dataPoint = $scope.beData.Finecoll?.[0] || {};

        const studentAbsence = dataPoint.StudentAbsence || 0;
        const employeeAbsence = dataPoint.EmployeeAbsence || 0;
        const totalFine = dataPoint.TotalFine || (studentAbsence + employeeAbsence);

        // Bind to scope for HTML
        $scope.AbsenceFineData = {
            TotalFine: totalFine,
            StudentAbsence: studentAbsence,
            EmployeeAbsence: employeeAbsence
        };

        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: ["Students", "Employees"],
                datasets: [{
                    data: [studentAbsence, employeeAbsence],
                    backgroundColor: ['#C96D61', '#8BC56B'],
                    borderColor: ['#ffffff', '#ffffff'],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                legend: { display: false, position: 'bottom' }
            }
        });
    };

    $scope.StudentAttendanceAlerts = function () {
        const data = $scope.beData.StudentAlertcoll || [];

        // Format real student data
        const formatted = data.map(alert => ({
            StudentName: alert.StudentNameAlerts || 'N/A',
            PhotoPath: alert.photopathAlerts ? alert.photopathAlerts.replace(/\\/g, '/') : '/wwwroot/dynamic/images/avatar-img.png',
            ClassName: alert.ClassAlerts || 'N/A',
            AbsentDays: alert.AbsentdaysAlerts || 0
        }));

        // Push dummy entries if less than 5
        while (formatted.length < 5) {
            formatted.push({
                StudentName: 'No Data',
                PhotoPath: '/wwwroot/dynamic/images/avatar-img.png',
                ClassName: 'N/A',
                AbsentDays: '-'
            });
        }

        $scope.StudentAlertsList = formatted;
    };

    $scope.EmployeeAttendanceAlerts = function () {
        const data = $scope.beData.EmployeeAlertscoll || [];


        const formatted = data.map(emp => ({
            EmployeeName: emp.EmployeeName || 'N/A',
            PhotoPath: emp.PhotopathEAA ? emp.PhotopathEAA.replace(/\\/g, '/') : '/wwwroot/dynamic/images/avatar-img.png',
            Department: emp.Department || 'N/A',
            AbsentDays: emp.AbsentDays || 0
        }));


        while (formatted.length < 5) {
            formatted.push({
                EmployeeName: 'No Data',
                PhotoPath: '/wwwroot/dynamic/images/avatar-img.png',
                Department: 'N/A',
                AbsentDays: '-'
            });
        }

        $scope.EmployeeAlertsList = formatted;
    };


 
});
