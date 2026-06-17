app.controller('HRDashbardController', function ($scope, $http, $timeout, $filter, GlobalServices) {

    var charts = {};
    $scope.LoadData = function () {

        $scope.ClassSection = {};
        $scope.labels = [];
        $scope.DashboardData = {};

        GlobalServices.getClassSectionList().then(function (res) {

            $scope.ClassSection = res.data.Data || {};
            $scope.labels = ($scope.ClassSection.ClassList || []).map(s => s.Name);

            return $scope.GetDashboardData();

        }).then(function (dash) {

            $timeout(function () {
                initializeCharts($scope.labels, dash);
            }, 0);

        }).catch(function (err) {
            Swal.fire('Failed ' + err);
        });
    };

    function initializeCharts(labels, DashboardData) {
        DashboardData = DashboardData || {};
        var assign = (DashboardData.AssignmentColl || {});
        var hw = (DashboardData.HomeworkColl || {});
        var overandupcommingduehw = (DashboardData.OverAndUpcommingdueHWColl || []);
        var overandupcommingdueassign = (DashboardData.OverAndUpcommingdueAssignColl || []);
        // Line: Over due Homework
        lineChart(
            'overdueHomeworkChart',
            'Over Due Homework',
            overandupcommingduehw.map(x => x.ClassName),
            overandupcommingduehw.map(x => x.PastDeadline),
            'rgba(40, 167, 69, 0.1)',
            '#28a745',
            100
        );

        // Bar: upcoming due Homework 
        barChart(
            'upcomingDueHomeworkChart',
            'Upcoming Due Homework',
            overandupcommingduehw.map(x => x.ClassName),
            overandupcommingduehw.map(x => x.UpcommingDue),
            '#c59b08',
            60
        );
        // Line: Over due Assignment
        lineChart(
            'overdueAssignmentChart',
            'Over Due Assignment',
            overandupcommingdueassign.map(x => x.ClassName),
            overandupcommingdueassign.map(x => x.PastDeadline),
            'rgba(0, 123, 255, 0.1)',
            '#007bff',
            100
        );

        // Bar: upcoming due Assignment
        barChart(
            'upcomingDueAssignmentChart',
            'Upcoming Due Assignment',
            overandupcommingdueassign.map(x => x.ClassName),
            overandupcommingdueassign.map(x => x.UpcommingDue),
            '#17a2b8',
            60
        );


        // Pie: total homework 
        pieChart(
            'totalHomeworkChart',
            'Total Homework',
            ['Pending', 'Submitted'],
            [hw.THPending || 0, hw.THSubmission || 0],
            ['#dc3545', '#28a745']
        );

        // Pie: total submissions homework 
        pieChart(
            'totalSubmissionsHomeworkChart',
            'Submissions Homework',
            ['On-Time', 'Late'],
            [hw.TSHWOnTime || 0, hw.TSHWLate || 0],
            ['#28a745', '#dc3545']
        );
        // Pie: Pending submissions homework 
        pieChart(
            'pendingSubmissionsHomeworkChart',
            'Pending Homework',
            ['Due-Today', 'Past Due'],
            [hw.PSHWDueToday || 0, hw.PSHWPastDue || 0],
            ['#28a745', '#dc3545']
        );

        // Pie: total assignment 
        pieChart(
            'totalAssignmentsChart',
            'Total Assignment',
            ['Pending', 'Submitted'],
            [assign.TAPending || 0, assign.TASubmission || 0],
            ['#007bff', '#ff9800']
        );

        // Pie: total submissions Assignment
        pieChart(
            'totalSubmissionsAssignmentChart',
            'Submissions Assignment',
            ['On-Time', 'Late'],
            [assign.TSAssignOnTime || 0, assign.TSAssignLate || 0],
            ['#007bff', '#ff9800']
        );
        // Pie: Pending submissions Assignment
        pieChart(
            'pendingSubmissionsAssignmentChart',
            'Pending Assignment',
            ['Due Today', 'Past Due'],
            [assign.PSAssignDueToday || 0, assign.PSAssignPastDue || 0],
            ['#007bff', '#ff9800']
        );


    }

    function buildChart(key, canvasId, config) {
        var el = document.getElementById(canvasId);
        if (!el) return;

        if (charts[key]) {
            charts[key].destroy();
            charts[key] = null;
        }

        charts[key] = new Chart(el, config);
    }

    function pieChart(canvasId, key, labels, values, colors) {
        buildChart(key, canvasId, {
            type: 'pie',
            data: {
                labels: labels,
                datasets: [{
                    data: values,
                    backgroundColor: colors
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: false
                    }
                }
            }
        });
    }


    function barChart(canvasId, key, labels, values, color, yMax) {
        buildChart(key, canvasId, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: key,
                    data: values,
                    backgroundColor: color,
                    borderRadius: 8,
                    borderSkipped: 'bottom'
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { display: false }
                },
                scales: {
                    x: {
                        ticks: {
                            autoSkip: false,
                            maxRotation: 0,
                            minRotation: 0
                        }
                    },
                    y: {
                        beginAtZero: true,
                        max: yMax,
                        ticks: {
                            stepSize: 10
                        }
                    }
                }
            }
        });
    }



    function lineChart(canvasId, key, labels, values, color, bordercolor, yMax) {
        buildChart(key, canvasId, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: key,
                    data: values,
                    backgroundColor: color,
                    borderColor: bordercolor,
                    fill: true,
                    tension: 0.4,
                    pointRadius: 0,
                    pointHitRadius: 20,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: bordercolor,
                    pointHoverBorderColor: '#ffffff',
                    pointHoverBorderWidth: 2
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { display: false },
                    tooltip: {
                        intersect: false,
                        mode: 'index'
                    }
                },

                interaction: {
                    intersect: false,
                    mode: 'nearest'
                },

                scales: {
                    x: {
                        ticks: {
                            autoSkip: false,
                            maxRotation: 0,
                            minRotation: 0
                        }
                    },
                    y: {
                        beginAtZero: true,
                        max: yMax,
                        ticks: {
                            stepSize: 20
                        }
                    }
                }
            }
        });
    }



    $scope.GetDashboardData = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.DashboardData = {};
        return $http({
            method: 'POST',
            url: base_url + "Homework/Reporting/GetDashboardData",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.DashboardData = res.data.Data;
                return $scope.DashboardData;

            } else {
                Swal.fire(res.data.ResponseMSG);
                return null;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

});
