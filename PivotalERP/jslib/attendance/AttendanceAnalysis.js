app.controller('AttendanceAnalysisController', function ($scope, $http, $timeout) {
    $scope.Title = 'AttendanceAnalysis';

    $scope.LoadData = function () {
        $scope.GetAttendanceAnalysis();
    };

    $scope.GetAttendanceAnalysis = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.beData = [];

        $http({
            method: 'POST',
            url: base_url + "Attendance/Report/GetAllAnalysis",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.beData = res.data.Data;

                $timeout(function () {
                    $scope.WeeklyAttendanceOverview();
                    $scope.MonthlyAttendanceOverview();
                    $scope.ClasswiseStudentLeave();
                    $scope.DepartmentWiseEmployeeLeave();
                    $scope.DepartmentWiseAbsenteeism();
                    $scope.ClasswiseAbsenteeism();
                    $scope.TopAbsentEmployees();
                    $scope.TopAbsentStudents();
                    $scope.EmployeeWithLongestConsecutives();
                    $scope.StudentWithLongestConsecutives();
                    $scope.ClasswiseMinimumAttendance();
                    $scope.DepartmentwiseLateAttendance();
                    $scope.AbsenceFineByClass();
                    $scope.MeetingMinimumAttendance();
                    $scope.AbsenceFineByDepartment();
                    $scope.AbsenteeismComparison();
                    $scope.StudentAbsenteeismCvsP();
                    $scope.EmployeeAbsenteeismCvsP();
                    $scope.HigherAbsenteeism();
                    $scope.ExcessiveAbsences();
                    
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

    $scope.WeeklyAttendanceOverview = function () {
        const canvas = document.getElementById('student-overview-chart');

        const ctx = canvas.getContext('2d');

        const labels = [];
        const studentsPresent = [];
        const employeesPresent = [];

        // Populate data arrays
        $scope.beData.Attendancecoll.forEach(function (attendance) {
            labels.push(attendance.Week || "Week");
            studentsPresent.push(attendance.presentSTD || 0);
            employeesPresent.push(attendance.presentEMP || 0);
        });

        const data = {
            labels: labels,
            datasets: [
                {
                    type: 'bar',
                    label: "Student",
                    data: studentsPresent,
                    backgroundColor: "#003f87",  // Dark blue
                    borderColor: "#003f87",
                    borderWidth: 1,
                    yAxisID: 'y-axis-1'
                },
                {
                    type: 'line',
                    label: "Employee",
                    data: employeesPresent,
                    borderColor: "#4CAF50",
                    backgroundColor: "rgba(76, 175, 80, 0.2)",
                    fill: true,
                    tension: 0.4,
                    pointStyle: 'circle',
                    pointRadius: 6,
                    pointHoverRadius: 8,
                    pointBackgroundColor: "#FFFFFF",
                    yAxisID: 'y-axis-1'
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
                    gridLines: {
                        display: true, color: "#f0f0f0",
                        zeroLineColor: "#cccccc"
                    }
                }],
                yAxes: [{
                    id: 'y-axis-1',
                    display: true,
                    gridLines: {
                        display: true, color: "#f0f0f0",
                        zeroLineColor: "#cccccc"
                    },
                    ticks: {
                        beginAtZero: true,
                        stepSize: 10,
                        suggestedMax: Math.max(...studentsPresent, ...employeesPresent) + 10
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
            type: 'bar',
            data: data,
            options: options
        });
    };

    $scope.MonthlyAttendanceOverview = function () {
        const canvas = document.getElementById('monthly-overview-chart');
        if (!canvas) {
            console.warn("Canvas for Monthly Overview not found.");
            return;
        }

        const ctx = canvas.getContext('2d');
        if (!$scope.beData.MonthlyAttendancecoll || !$scope.beData.MonthlyAttendancecoll.length) {
            console.warn("No monthly data found.");
            return;
        }

        const labels = [];
        const students = [];
        const employees = [];

        $scope.beData.MonthlyAttendancecoll.forEach(function (item) {
            labels.push(item.Month || "Month");
            students.push(item.MonthlyPStudent || 0);
            employees.push(item.MonthlyPEmployee || 0);
        });

        const suggestedMax = Math.max(...students, ...employees, 10);

        const data = {
            labels: labels,
            datasets: [
                {
                    label: "Student",
                    data: students,
                    borderColor: "#003f87",
                    backgroundColor: "rgba(0, 63, 135, 0.1)",
                    fill: true,
                    tension: 0.4,
                    pointStyle: 'circle',
                    pointRadius: 6,
                    pointHoverRadius: 8,
                    pointBackgroundColor: "#FFFFFF",
                    pointBorderColor: "#003f87",
                    borderWidth: 2
                },
                {
                    label: "Employee",
                    data: employees,
                    borderColor: "#1E90FF",
                    backgroundColor: "rgba(30, 144, 255, 0.1)",
                    fill: true,
                    tension: 0.4,
                    pointStyle: 'circle',
                    pointRadius: 6,
                    pointHoverRadius: 8,
                    pointBackgroundColor: "#FFFFFF",
                    pointBorderColor: "#1E90FF",
                    borderWidth: 2
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
                        const label = chartData.datasets[tooltipItem.datasetIndex].label || '';
                        const value = tooltipItem.yLabel;
                        return label + ": " + value;
                    }
                }
            },
            hover: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                xAxes: [{
                    display: true,
                    gridLines: {
                        display: true, color: "#f0f0f0",
                        zeroLineColor: "#cccccc"
                    }
                }],
                yAxes: [{
                    display: true,
                    gridLines: {
                        display: true, color: "#f0f0f0",
                        zeroLineColor: "#cccccc"
                    },
                    ticks: {
                        beginAtZero: true,
                        stepSize: 20,
                        suggestedMax: suggestedMax
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
    };

    $scope.ClasswiseStudentLeave = function () {
        const canvas = document.getElementById('classwise-leave-chart');
        if (!canvas) {
            console.warn("Canvas for classwise leave not found.");
            return;
        }

        const ctx = canvas.getContext('2d');
        if (!$scope.beData.ClasswiseLeavecoll || !$scope.beData.ClasswiseLeavecoll.length) {
            console.warn("No classwise leave data available.");
            return;
        }

        const labels = [];
        const sickLeave = [];
        const personalLeave = [];
        const otherLeave = [];

        $scope.beData.ClasswiseLeavecoll.forEach(function (item) {
            labels.push(item.Class || "Class");
            sickLeave.push(item.SickLeave || 0);
            personalLeave.push(item.PersonalLeave || 0);
            otherLeave.push(item.Others || 0);
        });

        const suggestedMax = Math.max(...sickLeave, ...personalLeave, ...otherLeave, 10);

        const data = {
            labels: labels,
            datasets: [
                {
                    label: "Sick",
                    data: sickLeave,
                    borderColor: "#E53935",
                    backgroundColor: "rgba(229, 57, 53, 0.1)",
                    fill: true,
                    tension: 0.4,
                    pointStyle: 'circle',
                    pointRadius: 6,
                    pointHoverRadius: 8,
                    pointBackgroundColor: "#FFFFFF",
                    pointBorderColor: "#E53935",
                    borderWidth: 2
                },
                {
                    label: "Personal",
                    data: personalLeave,
                    borderColor: "#388E3C",
                    backgroundColor: "rgba(56, 142, 60, 0.1)",
                    fill: true,
                    tension: 0.4,
                    pointStyle: 'circle',
                    pointRadius: 6,
                    pointHoverRadius: 8,
                    pointBackgroundColor: "#FFFFFF",
                    pointBorderColor: "#388E3C",
                    borderWidth: 2
                },
                {
                    label: "Other",
                    data: otherLeave,
                    borderColor: "#1E88E5",
                    backgroundColor: "rgba(30, 136, 229, 0.1)",
                    fill: true,
                    tension: 0.4,
                    pointStyle: 'circle',
                    pointRadius: 6,
                    pointHoverRadius: 8,
                    pointBackgroundColor: "#FFFFFF",
                    pointBorderColor: "#1E88E5",
                    borderWidth: 2
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
                        const label = chartData.datasets[tooltipItem.datasetIndex].label || '';
                        const value = tooltipItem.yLabel;
                        return label + ": " + value;
                    }
                }
            },
            hover: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                xAxes: [{
                    display: true,
                    gridLines: {
                        display: true,
                        color: "#f0f0f0"
                    }
                }],
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        stepSize: 2,
                        suggestedMax: suggestedMax
                    },
                    gridLines: {
                        display: true,
                        color: "#f0f0f0"
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
    };

    $scope.DepartmentWiseEmployeeLeave = function () {
        const canvas = document.getElementById('department-leave-chart');
        if (!canvas) {
            console.warn("Canvas not found.");
            return;
        }

        const ctx = canvas.getContext('2d');
        if (!$scope.beData.EmployeeLeaveColl || !$scope.beData.EmployeeLeaveColl.length) {
            console.warn("No department-wise employee leave data.");
            return;
        }

        const labels = [];
        const sickLeave = [];
        const casualLeave = [];
        const emergencyLeave = [];

        $scope.beData.EmployeeLeaveColl.forEach(function (item) {
            labels.push(item.DepartmentE || "Dept");
            sickLeave.push(item.SickLeaveE || 0);
            casualLeave.push(item.PersonalLeaveE || 0);
            emergencyLeave.push(item.OthersE || 0);
        });

        const data = {
            labels: labels,
            datasets: [
                {
                    label: "Sick",
                    backgroundColor: "#2e7d32", // green
                    borderColor: "#2e7d32",
                    borderWidth: 1,
                    data: sickLeave
                },
                {
                    label: "Casual",
                    backgroundColor: "#7cb342", // light green
                    borderColor: "#7cb342",
                    borderWidth: 1,
                    data: casualLeave
                },
                {
                    label: "Emergency",
                    backgroundColor: "#cddc39", // lime
                    borderColor: "#cddc39",
                    borderWidth: 1,
                    data: emergencyLeave
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
            hover: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                xAxes: [{
                    display: true,
                    gridLines: {
                        display: true,
                        color: "#f0f0f0"
                    },
                    ticks: {
                        autoSkip: false
                    },
                    barPercentage: 0.6,
                    categoryPercentage: 0.7
                }],
                yAxes: [{
                    display: true,
                    gridLines: {
                        display: true,
                        color: "#f0f0f0"
                    },
                    ticks: {
                        beginAtZero: true,
                        stepSize: 2
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
            type: 'bar',
            data: data,
            options: options
        });
    };

    $scope.DepartmentWiseAbsenteeism = function () {
        const canvas = document.getElementById('department-absenteeism-chart');
        if (!canvas) {
            console.warn("Canvas not found.");
            return;
        }

        const ctx = canvas.getContext('2d');
        if (!$scope.beData.DepartmentWiseAbsenteeismcoll || !$scope.beData.DepartmentWiseAbsenteeismcoll.length) {
            console.warn("No data found.");
            return;
        }

        const labels = [];
        const boysData = [];
        const girlsData = [];

        $scope.beData.DepartmentWiseAbsenteeismcoll.forEach(function (item) {
            labels.push(item.DepartmentA || "Dept");
            boysData.push(item.BoysE || 0);
            girlsData.push(item.GirlsE || 0);
        });

        const data = {
            labels: labels,
            datasets: [
                {
                    type: 'bar',
                    label: "Men",
                    data: boysData,
                    backgroundColor: "#2F56D6",
                    borderColor: "#2F56D6",
                    borderWidth: 1,
                    yAxisID: 'y-axis-1'
                },
                {
                    type: 'line',
                    label: "Women",
                    data: girlsData,
                    borderColor: "#7BC043",
                    backgroundColor: "rgba(123, 192, 67, 0.1)",
                    fill: true,
                    tension: 0.4,
                    pointStyle: 'circle',
                    pointRadius: 5,
                    pointHoverRadius: 7,
                    pointBackgroundColor: "#FFFFFF",
                    pointBorderColor: "#7BC043",
                    borderWidth: 2,
                    yAxisID: 'y-axis-1'
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
            hover: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                xAxes: [{
                    display: true,
                    gridLines: {
                        display: true,
                        color: "#f0f0f0"
                    },
                    ticks: {
                        autoSkip: false
                    }
                }],
                yAxes: [{
                    id: 'y-axis-1',
                    display: true,
                    gridLines: {
                        display: true,
                        color: "#f0f0f0"
                    },
                    ticks: {
                        beginAtZero: true,
                        stepSize: 1  // Or 5 or 10 depending on your data scale
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
            type: 'bar', // Root chart type (can be 'bar' for mixed)
            data: data,
            options: options
        });
    };

    $scope.ClasswiseAbsenteeism = function () {
        const canvas = document.getElementById('classwise-absenteeism-chart');
        if (!canvas) {
            console.warn("Canvas not found.");
            return;
        }

        const ctx = canvas.getContext('2d');
        if (!$scope.beData.ClassAbsenteeismcoll || !$scope.beData.ClassAbsenteeismcoll.length) {
            console.warn("No classwise absenteeism data.");
            return;
        }

        const labels = [];
        const boysData = [];
        const girlsData = [];

        $scope.beData.ClassAbsenteeismcoll.forEach(function (item) {
            labels.push(item.ClassA || "Class");
            boysData.push(item.BoysC || 0);
            girlsData.push(item.GirlsC || 0);
        });

        const data = {
            labels: labels,
            datasets: [
                {
                    label: "Boys",
                    data: boysData,
                    borderColor: "#FFA726", // Orange
                    backgroundColor: "rgba(255, 167, 38, 0.1)",
                    fill: true,
                    tension: 0.4,
                    pointStyle: 'circle',
                    pointRadius: 5,
                    pointHoverRadius: 7,
                    pointBackgroundColor: "#FFFFFF",
                    pointBorderColor: "#FFA726",
                    borderWidth: 2
                },
                {
                    label: "Girls",
                    data: girlsData,
                    borderColor: "#EF5350", // Pink
                    backgroundColor: "rgba(239, 83, 80, 0.1)",
                    fill: true,
                    tension: 0.4,
                    pointStyle: 'circle',
                    pointRadius: 5,
                    pointHoverRadius: 7,
                    pointBackgroundColor: "#FFFFFF",
                    pointBorderColor: "#EF5350",
                    borderWidth: 2
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
                        const label = chartData.datasets[tooltipItem.datasetIndex].label || '';
                        const value = tooltipItem.yLabel;
                        return label + ": " + value;
                    }
                }
            },
            hover: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                xAxes: [{
                    display: true,
                    gridLines: {
                        display: true,
                        color: "#f5f5f5"  // ✅ Light gridline
                    },
                    ticks: {
                        autoSkip: false
                    }
                }],
                yAxes: [{
                    display: true,
                    gridLines: {
                        display: true,
                        color: "#f5f5f5"
                    },
                    ticks: {
                        beginAtZero: true,
                        stepSize: 5
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
    };

    $scope.TopAbsentEmployees = function () {
        const canvas = document.getElementById('top-absent-employees-table'); // optional if chart is also here
        $scope.EmployeeData = [];

        // Assuming `beData.TopAbsentEmpcoll` is already populated by your backend call
        if (!$scope.beData.TopAbsentEmpcoll || !$scope.beData.TopAbsentEmpcoll.length) {
            console.warn("No top absent employee data found.");
            return;
        }

        $scope.EmployeeData = $scope.beData.TopAbsentEmpcoll.map(function (item) {
            return {
                name: item.NameTAE,
                gender: item.GenderTAE,
                department: item.DepartmentTAE,
                absentDays: item.AbsentDayTAE,
                image: item.PhotoTAE || '/wwwroot/dynamic/images/aryan-shrestha.png' // optional: show default photo if none
            };
        });
    };

    $scope.TopAbsentStudents = function () {
        $scope.StudentData = [];

        if (!$scope.beData.TopAbsentScoll || !$scope.beData.TopAbsentScoll.length) {
            console.warn("No top absent student data found.");
            return;
        }

        $scope.StudentData = $scope.beData.TopAbsentScoll.map(function (item) {
            return {
                name: item.NameTAS,
                gender: item.GenderTAS,
                class: item.ClassTAS,
                section: item.SectionTAS,
                absentDays: item.AbsentDayTAS,
                image: item.PhotoTAS || '/wwwroot/dynamic/images/aryan-shrestha.png' // optional: show default photo if none
            };
        });
    };

    $scope.EmployeeWithLongestConsecutives = function () {
        $scope.EmployeeAbsenteeCards = [];

        // 1. Longest Consecutive Absence
        if ($scope.beData.ELCAcoll && $scope.beData.ELCAcoll.length > 0) {
            const elca = $scope.beData.ELCAcoll[0];
            $scope.EmployeeAbsenteeCards.push({
                name: elca.NameCD || 'N/A',
                photo: elca.PhotopathCD ? elca.PhotopathCD.replace(/\\/g, '/') : 'images/default-user.png',
                department: elca.DepartmentCD || '-',
                tag: "Consecutive days"
            });
        } else {
            $scope.EmployeeAbsenteeCards.push({
                photo: "images/default-user.png",
                department: "-",
                tag: "Consecutive days"
            });
        }

        // 2. Minimum Absenteeism
        if ($scope.beData.EMAcoll && $scope.beData.EMAcoll.length > 0) {
            const ema = $scope.beData.EMAcoll[0];
            $scope.EmployeeAbsenteeCards.push({
                name: ema.NameMA || 'N/A',
                photo: ema.PhotopathMA ? ema.PhotopathMA.replace(/\\/g, '/') : 'images/default-user.png',
                department: ema.DepartmentMA || "-",
                tag: "Minimum attendance"
            });
        } else {
            $scope.EmployeeAbsenteeCards.push({
                name: "N/A",
                photo: "images/default-user.png",
                department: "-",
                tag: "Minimum attendance"
            });
        }

        // 3. Highest Absenteeism
        if ($scope.beData.EHAcoll && $scope.beData.EHAcoll.length > 0) {
            const eha = $scope.beData.EHAcoll[0];
            $scope.EmployeeAbsenteeCards.push({
                name: eha.NameHA || 'N/A',
                photo: eha.PhotopathHA ? eha.PhotopathHA.replace(/\\/g, '/') : 'images/default-user.png',
                department: eha.DepartmentHA || "-",
                tag: "High absenteeism"
            });
        } else {
            $scope.EmployeeAbsenteeCards.push({
               
                photo: "images/default-user.png"
            });
        };
    };

    $scope.StudentWithLongestConsecutives = function () {
        $scope.StudentAbsenteeCards = [];

        // 1. Longest Consecutive Absence
        if ($scope.beData.SLCAcoll && $scope.beData.SLCAcoll.length > 0) {
            const scd = $scope.beData.SLCAcoll[0];
            $scope.StudentAbsenteeCards.push({
                name: scd.NameSCD || 'N/A',
                photo: scd.PhotopathSCD ? scd.PhotopathSCD.replace(/\\/g, '/') : 'images/default-user.png',
                department: scd.ClassSCD || '-',
                tag: "Consecutive days"
            });
        } else {
            $scope.StudentAbsenteeCards.push({
                name: "N/A",
                photo: "images/default-user.png",
                department: "-",
                tag: "Consecutive days"
            });
        }

        // 2. Minimum Attendance
        if ($scope.beData.SMAcoll && $scope.beData.SMAcoll.length > 0) {
            const sma = $scope.beData.SMAcoll[0];
            $scope.StudentAbsenteeCards.push({
                name: sma.NameSMA || 'N/A',
                photo: sma.PhotopathSMA ? sma.PhotopathSMA.replace(/\\/g, '/') : 'images/default-user.png',
                department: sma.ClassSMA || "-",
                tag: "Minimum attendance"
            });
        } else {
            $scope.StudentAbsenteeCards.push({
                name: "N/A",
                photo: "images/default-user.png",
                department: "-",
                tag: "Minimum attendance"
            });
        }

        // 3. Highest Absenteeism
        if ($scope.beData.SHAcoll && $scope.beData.SHAcoll.length > 0) {
            const sha = $scope.beData.SHAcoll[0];
            $scope.StudentAbsenteeCards.push({
                name: sha.NameSHA || 'N/A',
                photo: sha.PhotopathSHA ? sha.PhotopathSHA.replace(/\\/g, '/') : 'images/default-user.png',
                department: sha.DepartmentSHA || "-",
                tag: "High absenteeism"
            });
        } else {
            $scope.StudentAbsenteeCards.push({
                name: "N/A",
                photo: "images/default-user.png",
                department: "-",
                tag: "High absenteeism"
            });
        };

    };


    $scope.ClasswiseMinimumAttendance = function () {
        const canvas = document.getElementById('classwise-minimum-attendance-chart');
        if (!canvas) {
            console.warn("Canvas for Classwise Minimum Attendance not found.");
            return;
        }

        const ctx = canvas.getContext('2d');

        if (!$scope.beData.ClassWisecoll || !$scope.beData.ClassWisecoll.length) {
            console.warn("No classwise late attendance data found.");
            return;
        }

        const labels = [];
        const boysData = [];
        const girlsData = [];

        $scope.beData.ClassWisecoll.forEach(function (item) {
            labels.push(item.ClassCLA || "Class");
            boysData.push(item.LateBoysS || 0);
            girlsData.push(item.LateGirlsS || 0);
        });

        const suggestedMax = Math.max(...boysData, ...girlsData, 5);

        const data = {
            labels: labels,
            datasets: [
                {
                    label: "Boys",
                    data: boysData,
                    borderColor: "#005e54",
                    backgroundColor: "rgba(0, 94, 84, 0.2)",
                    fill: true,
                    tension: 0.4,
                    pointStyle: 'circle',
                    pointRadius: 6,
                    pointHoverRadius: 8,
                    pointBackgroundColor: "#FFFFFF",
                    pointBorderColor: "#005e54",
                    borderWidth: 2
                },
                {
                    label: "Girls",
                    data: girlsData,
                    borderColor: "#7ec56e",
                    backgroundColor: "rgba(126, 197, 110, 0.2)",
                    fill: true,
                    tension: 0.4,
                    pointStyle: 'circle',
                    pointRadius: 6,
                    pointHoverRadius: 8,
                    pointBackgroundColor: "#FFFFFF",
                    pointBorderColor: "#7ec56e",
                    borderWidth: 2
                }
            ]
        };

        const options = {
            responsive: true,
            tooltips: {
                mode: 'index',
                intersect: false,
                backgroundColor: "#ffffff",
                titleFontColor: "#000",
                bodyFontColor: "#000",
                borderColor: "#ccc",
                borderWidth: 1,
                callbacks: {
                    label: function (tooltipItem, chartData) {
                        const label = chartData.datasets[tooltipItem.datasetIndex].label || '';
                        const value = tooltipItem.yLabel;
                        return label + ": " + value;
                    }
                }
            },
            hover: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                xAxes: [{
                    display: true,
                    gridLines: {
                        display: true,
                        color: "#f0f0f0",
                        zeroLineColor: "#cccccc"
                    }
                }],
                yAxes: [{
                    display: true,
                    gridLines: {
                        display: true,
                        color: "#f0f0f0",
                        zeroLineColor: "#cccccc"
                    },
                    ticks: {
                        beginAtZero: true,
                        stepSize: 1,
                        suggestedMax: suggestedMax
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
    };

    $scope.DepartmentwiseLateAttendance = function () {
        const canvas = document.getElementById('departmentwise-late-attendance-chart');
        if (!canvas) {
            console.warn("Canvas for Departmentwise Late Attendance not found.");
            return;
        }

        const ctx = canvas.getContext('2d');

        if (!$scope.beData.EmployeeLateAttdcoll || !$scope.beData.EmployeeLateAttdcoll.length) {
            console.warn("No departmentwise late attendance data found.");
            return;
        }

        const labels = [];
        const boysData = [];
        const girlsData = [];

        $scope.beData.EmployeeLateAttdcoll.forEach(function (item) {
            labels.push(item.Department || "Dept");
            boysData.push(item.LateBoysE || 0);
            girlsData.push(item.LateGirlsE || 0);
        });

        const data = {
            labels: labels,
            datasets: [
                {
                    label: "Men",
                    data: boysData,
                    backgroundColor: "#9370DB"
                },
                {
                    label: "Women",
                    data: girlsData,
                    backgroundColor: "#D3D3D3"
                }
            ]
        };

        const options = {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                xAxes: [{
                    stacked: true,
                    gridLines: {
                        display: false
                    },
                    ticks: {
                        beginAtZero: true,
                        stepSize: 10
                    }
                }],
                yAxes: [{
                    stacked: true,
                    gridLines: {
                        display: false
                    }
                }]
            },
            legend: {
                display: false
            },
            tooltips: {
                mode: 'index',
                intersect: false,
                callbacks: {
                    label: function (tooltipItem, chartData) {
                        const label = chartData.datasets[tooltipItem.datasetIndex].label;
                        const value = tooltipItem.xLabel;
                        return label + " : " + value;
                    }
                },
                backgroundColor: "#eee",
                titleFontColor: "#000",
                bodyFontColor: "#000",
                borderColor: "#ccc",
                borderWidth: 1
            }
        };

        new Chart(ctx, {
            type: 'horizontalBar',
            data: data,
            options: options
        });
    };

    $scope.AbsenceFineByClass = function () {
        const canvas = document.getElementById('absence-fine-by-class-chart');
        if (!canvas) {
            console.warn("Canvas for Absence Fine chart not found.");
            return;
        }

        const ctx = canvas.getContext('2d');
        const dataList = ($scope.beData.StudentFinecoll || []).filter(item => item.FineAFC > 0);

        // Show chart only if data is present
        $scope.hasStudentFineData = dataList.length > 0;

        if (!dataList.length) {
            console.warn("No absence fine data available.");
            return;
        }

        // Sort for visual layering
        dataList.sort((a, b) => b.FineAFC - a.FineAFC);

        const labels = dataList.map(item => item.ClassAFC);
        const values = dataList.map(item => item.FineAFC);
        const backgroundColors = labels.map(() => {
            const hue = Math.floor(Math.random() * 360);
            return `hsl(${hue}, 70%, 60%)`;
        });

        new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: labels,
                datasets: [{
                    data: values,
                    backgroundColor: backgroundColors,
                    borderWidth: 10,
                    hoverOffset: 10,
                    cutoutPercentage: 40
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                title: {
                    display: false
                },
                legend: {
                    position: 'right',
                    labels: {
                        usePointStyle: true,
                        padding: 15,
                        fontSize: 12,
                        boxWidth: 12
                    }
                },
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, chartData) {
                            const label = chartData.labels[tooltipItem.index] || '';
                            const value = chartData.datasets[0].data[tooltipItem.index];
                            const total = chartData.datasets[0].data.reduce((a, b) => a + b, 0);
                            const percent = ((value / total) * 100).toFixed(0);
                            return `${label}: ${percent}%`;
                        }
                    },
                    backgroundColor: "#ffffff",
                    titleFontColor: "#000",
                    bodyFontColor: "#000",
                    borderColor: "#ccc",
                    borderWidth: 1
                }
            }
        });
    };

    $scope.MeetingMinimumAttendance = function () {
        const canvas = document.getElementById("meeting-minimum-attendance-chart");
        if (!canvas) {
            console.warn("Canvas not found.");
            return;
        }

        const ctx = canvas.getContext("2d");
        const dataList = $scope.beData.MinimumAttdcoll || [];

        if (!dataList.length) {
            console.warn("No meeting minimum attendance data available.");
            return;
        }

        const labels = dataList.map(item => item.MonthNameMMA || "Month");
        const studentData = dataList.map(item => item.StudentMMA || 0);
        const employeeData = dataList.map(item => item.EmployeeMMA || 0);
        const suggestedMax = Math.max(...studentData, ...employeeData, 5);

        const chartData = {
            labels: labels,
            datasets: [
                {
                    label: "Student",
                    data: studentData,
                    borderColor: "#1E90FF",
                    backgroundColor: "rgba(30, 144, 255, 0.15)",
                    borderWidth: 2,
                    fill: true,
                    tension: 0.4,
                    pointRadius: 5,
                    pointBackgroundColor: "#fff",
                    pointBorderColor: "#1E90FF"
                },
                {
                    label: "Employee",
                    data: employeeData,
                    borderColor: "#003f87",
                    backgroundColor: "rgba(0, 63, 135, 0.15)",
                    borderWidth: 2,
                    fill: true,
                    tension: 0.4,
                    pointRadius: 5,
                    pointBackgroundColor: "#fff",
                    pointBorderColor: "#003f87"
                }
            ]
        };

        const chartOptions = {
            responsive: true,
            maintainAspectRatio: false,
            tooltips: {
                mode: 'index',
                intersect: false,
                backgroundColor: "#fff",
                titleFontColor: "#000",
                bodyFontColor: "#000",
                borderColor: "#ccc",
                borderWidth: 1,
                callbacks: {
                    label: function (tooltipItem, chartData) {
                        const label = chartData.datasets[tooltipItem.datasetIndex].label || '';
                        const value = tooltipItem.yLabel;
                        return `${label}: ${value}`;
                    }
                }
            },
            hover: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                xAxes: [{
                    gridLines: {
                        color: "#f0f0f0",
                        zeroLineColor: "#ccc"
                    }
                }],
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        stepSize: 20,
                        suggestedMax: suggestedMax
                    },
                    gridLines: {
                        color: "#f0f0f0",
                        zeroLineColor: "#ccc"
                    }
                }]
            },
            legend: {
                display: true,
                position: 'bottom',
                labels: {
                    usePointStyle: true,
                    padding: 15
                }
            }
        };

        new Chart(ctx, {
            type: 'line',
            data: chartData,
            options: chartOptions
        });
    };

    $scope.AbsenceFineByDepartment = function () {
        const canvas = document.getElementById("absence-fine-by-department-chart");
        if (!canvas) {
            console.warn("Canvas not found.");
            return;
        }

        const ctx = canvas.getContext("2d");

        // Filter out departments with 0 or null fine
        const dataList = ($scope.beData.EmployeeFinecoll || []).filter(item => item.FineAFD > 0);

        // Control ng-show/ng-if visibility
        $scope.hasEmployeeFineData = dataList.length > 0;

        if (!dataList.length) {
            console.warn("No absence fine department data available.");
            return;
        }

        const labels = dataList.map(item => item.DepartmentAFD || "Dept");
        const fineData = dataList.map(item => item.FineAFD || 0);

        // Generate random HSL colors
        const getRandomColor = () => `hsl(${Math.floor(Math.random() * 360)}, 70%, 60%)`;
        const backgroundColors = labels.map(() => getRandomColor());

        new Chart(ctx, {
            type: 'polarArea',
            data: {
                labels: labels,
                datasets: [{
                    data: fineData,
                    backgroundColor: backgroundColors,
                    borderColor: "#ffffff",
                    borderWidth: 2
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scale: {
                    ticks: {
                        beginAtZero: true,
                        backdropColor: "transparent"
                    },
                    gridLines: {
                        color: "#e0e0e0"
                    }
                },
                legend: {
                    position: 'right',
                    labels: {
                        fontSize: 8,
                        boxWidth: 6,
                        padding: 5,
                        usePointStyle: true,
                        fontStyle: 'normal',
                        fontColor: '#444'
                    }
                },
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, chartData) {
                            const label = chartData.labels[tooltipItem.index] || '';
                            const value = chartData.datasets[0].data[tooltipItem.index];
                            const total = chartData.datasets[0].data.reduce((a, b) => a + b, 0);
                            const percent = ((value / total) * 100).toFixed(0);
                            return `${label}: ${percent}%`;
                        }
                    },
                    backgroundColor: "#fff",
                    titleFontColor: "#000",
                    bodyFontColor: "#000",
                    borderColor: "#ccc",
                    borderWidth: 1
                },
                animation: {
                    animateRotate: true,
                    animateScale: true
                }
            }
        });
    };

    $scope.AbsenteeismComparison = function () {
        const canvas = document.getElementById("absenteeism-comparison-chart");
        if (!canvas) {
            console.warn("Canvas not found.");
            return;
        }

        const ctx = canvas.getContext("2d");
        const dataList = $scope.beData.Comparisoncoll || [];

        if (!dataList.length) {
            console.warn("No comparison data found.");
            return;
        }

        const data = dataList[0]; // Assuming a single row
        const labels = ['2078', '2079', '2080', '2081'];

        const currentData = [
            data.AbsentData2078C || 0,
            data.AbsentData2079C || 0,
            data.AbsentData2080C || 0,
            data.AbsentData2081C || 0
        ];

        const previousData = [
            data.AbsentData2078P || 0,
            data.AbsentData2079P || 0,
            data.AbsentData2080P || 0,
            data.AbsentData2081P || 0
        ];

        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: "Current",
                        data: currentData,
                        backgroundColor: "#3b6efb",
                        borderRadius: 4,
                        borderSkipped: false,
                        yAxisID: "y-axis-1"
                    },
                    {
                        label: "Previous",
                        data: previousData,
                        type: 'line',
                        borderColor: "#7ec56e",
                        backgroundColor: "rgba(126, 197, 110, 0.2)",
                        borderWidth: 3,
                        tension: 0.4,
                        pointRadius: 6,
                        pointBackgroundColor: "#ffffff",
                        pointBorderColor: "#7ec56e",
                        fill: false,
                        yAxisID: "y-axis-1"
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    yAxes: [{
                        id: "y-axis-1",
                        ticks: {
                            beginAtZero: true,
                            stepSize: 20
                        },
                        gridLines: {
                            color: "#f0f0f0"
                        }
                    }],
                    xAxes: [{
                        gridLines: {
                            color: "#f0f0f0"
                        }
                    }]
                },
                legend: {
                    display: true,
                    position: 'bottom',
                    labels: {
                        usePointStyle: true,
                        padding: 10
                    }
                },
                tooltips: {
                    mode: 'index',
                    intersect: false,
                    backgroundColor: "#fff",
                    titleFontColor: "#000",
                    bodyFontColor: "#000",
                    borderColor: "#ccc",
                    borderWidth: 1,
                    callbacks: {
                        label: function (tooltipItem, chartData) {
                            const label = chartData.datasets[tooltipItem.datasetIndex].label || '';
                            const value = tooltipItem.yLabel;
                            return `${label}: ${value}`;
                        }
                    }
                }
            }
        });
    };

    $scope.StudentAbsenteeismCvsP = function () {
        const canvas = document.getElementById("student-absenteeism-cvsp-chart");
        if (!canvas) {
            console.warn("Canvas not found.");
            return;
        }

        const ctx = canvas.getContext("2d");

        const rawList = $scope.beData.Studentcomparisoncoll || [];

        const dataList = rawList
            .map(item => {
                const prev = item.AbsentData2080SCvsP || 0;
                const curr = item.AbsentData2081SCvsP || 0;
                const diff = curr - prev;
                return {
                    className: item.ClassE || "Class",
                    prev,
                    curr,
                    diff
                };
            })
            .filter(item => item.diff !== 0); // Only show differences

        $scope.hasStudentCvsPData = dataList.length > 0;

        if (!dataList.length) {
            console.warn("No valid data to show.");
            return;
        }

        const labels = dataList.map(item => item.className);
        const values = dataList.map(item => item.diff);
        const previousData = dataList.map(item => item.prev);
        const currentData = dataList.map(item => item.curr);

        const getRandomColor = () =>
            `hsl(${Math.floor(Math.random() * 360)}, 70%, 60%)`;
        const backgroundColors = labels.map(() => getRandomColor());

        new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: labels,
                datasets: [{
                    data: values,
                    backgroundColor: backgroundColors,
                    borderColor: "#ffffff",
                    borderWidth: 6,
                    hoverOffset: 10
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutoutPercentage: 55,
                legend: {
                    position: 'right',
                    labels: {
                        boxWidth: 10,
                        fontSize: 12,
                        padding: 8,
                        usePointStyle: true
                    }
                },
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, chartData) {
                            const index = tooltipItem.index;
                            const label = chartData.labels[index];
                            const diff = chartData.datasets[0].data[index];
                            const prev = previousData[index];
                            const curr = currentData[index];
                            return `${label}: ${diff} (Curr: ${curr}, Prev: ${prev})`;
                        }
                    },
                    backgroundColor: "#fff",
                    titleFontColor: "#000",
                    bodyFontColor: "#000",
                    borderColor: "#ccc",
                    borderWidth: 1
                },
                animation: {
                    animateRotate: true,
                    animateScale: true
                }
            }
        });
    };

    $scope.EmployeeAbsenteeismCvsP = function () {
        const canvas = document.getElementById("employee-absenteeism-cvsp-chart");
        if (!canvas) {
            console.warn("Canvas not found.");
            return;
        }

        const ctx = canvas.getContext("2d");

        const rawData = $scope.beData.Employeecomparisoncoll || [];

        const labels = [];
        const previousData = [];
        const currentDiffData = [];
        const diffColors = [];

        rawData.forEach(item => {
            const dept = item.DepartmentE || "Dept";
            const prev = item.AbsentData2080ECvsP || 0;
            const curr = item.AbsentData2081ECvsP || 0;
            const diff = curr - prev;

            labels.push(dept);
            previousData.push(prev);
            currentDiffData.push(Math.abs(diff));
            diffColors.push(diff >= 0 ? "#66BB6A" : "#EF5350"); // green ↑, red ↓
        });

        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: "Previous",
                        data: previousData,
                        backgroundColor: "#388E3C", // dark green
                        stack: 'absentee'
                    },
                    {
                        label: "Current",
                        data: currentDiffData,
                        backgroundColor: diffColors,
                        stack: 'absentee'
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    xAxes: [{
                        stacked: true,
                        ticks: { autoSkip: false },
                        gridLines: { color: "#f0f0f0" }
                    }],
                    yAxes: [{
                        stacked: true,
                        ticks: {
                            beginAtZero: true,
                            stepSize: 10
                        },
                        gridLines: { color: "#f0f0f0" }
                    }]
                },
                legend: {
                    display: true,
                    position: 'bottom',
                    labels: {
                        usePointStyle: true,
                        padding: 12
                    }
                },
                tooltips: {
                    mode: 'index',
                    intersect: false,
                    callbacks: {
                        label: function (tooltipItem, chartData) {
                            const label = chartData.datasets[tooltipItem.datasetIndex].label;
                            const value = chartData.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
                            return `${label}: ${value}`;
                        }
                    },
                    backgroundColor: "#fff",
                    titleFontColor: "#000",
                    bodyFontColor: "#000",
                    borderColor: "#ccc",
                    borderWidth: 1
                }
            }
        });
    };

    $scope.HigherAbsenteeism = function () {
        const canvas = document.getElementById("higher-absenteeism-chart");
        if (!canvas) {
            console.warn("Canvas not found.");
            return;
        }

        const ctx = canvas.getContext("2d");

        const dataList = $scope.beData.Highabsenteeismcoll || [];

        if (!dataList.length) {
            console.warn("No higher absenteeism data.");
            return;
        }

        const labels = dataList.map(item => item.MonthHA || "Month");
        const studentData = dataList.map(item => item.AbsentSTD || 0);
        const employeeData = dataList.map(item => item.AbsentEMP || 0);

        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: "Student",
                        data: studentData,
                        backgroundColor: "#003f87",
                        borderRadius: 4,
                        barThickness: 20
                    },
                    {
                        label: "Employee",
                        data: employeeData,
                        backgroundColor: "#7ec56e",
                        borderRadius: 4,
                        barThickness: 20
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    xAxes: [{
                        stacked: false,
                        gridLines: { color: "#f0f0f0" }
                    }],
                    yAxes: [{
                        ticks: {
                            beginAtZero: true,
                            stepSize: 10
                        },
                        gridLines: { color: "#f0f0f0" }
                    }]
                },
                legend: {
                    display: true,
                    position: 'bottom',
                    labels: {
                        usePointStyle: true,
                        padding: 15
                    }
                },
                tooltips: {
                    mode: 'index',
                    intersect: false,
                    backgroundColor: "#fff",
                    titleFontColor: "#000",
                    bodyFontColor: "#000",
                    borderColor: "#ccc",
                    borderWidth: 1,
                    callbacks: {
                        title: function (tooltipItems) {
                            return tooltipItems[0].xLabel;
                        },
                        label: function (tooltipItem, chartData) {
                            const label = chartData.datasets[tooltipItem.datasetIndex].label;
                            const value = tooltipItem.yLabel;
                            return `${label}: ${value}`;
                        }
                    }
                }
            }
        });
    };

    $scope.ExcessiveAbsences = function () {
        const canvas = document.getElementById("excessive-absences-chart");
        if (!canvas) {
            console.warn("Canvas not found.");
            return;
        }

        const ctx = canvas.getContext("2d");

        const dataList = $scope.beData.Excessiveabsentcoll || [];

        if (!dataList.length) {
            console.warn("No excessive absences data found.");
            return;
        }

        const labels = dataList.map(item => item.MonthEA || "Month");
        const studentData = dataList.map(item => item.AbsentSTDEA || 0);
        const employeeData = dataList.map(item => item.AbsentEMPEA || 0);

        new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: "Student",
                        data: studentData,
                        borderColor: "#005e54",
                        backgroundColor: "rgba(0, 94, 84, 0.1)",
                        borderWidth: 2,
                        fill: false,
                        pointRadius: 4,
                        pointHoverRadius: 6,
                        pointBackgroundColor: "#fff",
                        pointBorderColor: "#005e54",
                        tension: 0.4
                    },
                    {
                        label: "Employee",
                        data: employeeData,
                        borderColor: "#8BC34A",
                        backgroundColor: "rgba(139, 195, 74, 0.1)",
                        borderWidth: 2,
                        fill: false,
                        pointRadius: 4,
                        pointHoverRadius: 6,
                        pointBackgroundColor: "#fff",
                        pointBorderColor: "#8BC34A",
                        tension: 0.4
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    xAxes: [{
                        gridLines: { color: "#f0f0f0" }
                    }],
                    yAxes: [{
                        ticks: {
                            beginAtZero: true,
                            stepSize: 10
                        },
                        gridLines: { color: "#f0f0f0" }
                    }]
                },
                legend: {
                    display: true,
                    position: 'bottom',
                    labels: {
                        usePointStyle: true,
                        padding: 10
                    }
                },
                tooltips: {
                    mode: 'index',
                    intersect: false,
                    backgroundColor: "#fff",
                    titleFontColor: "#000",
                    bodyFontColor: "#000",
                    borderColor: "#ccc",
                    borderWidth: 1,
                    callbacks: {
                        title: function (tooltipItems) {
                            return tooltipItems[0].xLabel;
                        },
                        label: function (tooltipItem, chartData) {
                            const label = chartData.datasets[tooltipItem.datasetIndex].label;
                            const value = tooltipItem.yLabel;
                            return `${label}: ${value}`;
                        }
                    }
                }
            }
        });
    };






});




