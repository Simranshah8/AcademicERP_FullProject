app.controller('ExamDashbardController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.LoadData = function () {
        $scope.TopPerformers = [];
        $scope.Awards = [];
        $scope.resultSummary = {};
        $scope.GetExamDashboardData();
    };

    $scope.initTopPerformerSlider = function () {
        $timeout(function () {
            initSliders();
        }, 0);
    };



    $scope.initializeCharts = function () {
        $scope.createPieChart(
            "casChart",
            ["Setup", "Entry"],
            [$scope.DashboardData.CASMarkStatDet.CASSetup, $scope.DashboardData.CASMarkStatDet.CASEntry],
            ["#7b7cff", "#ff8a80"],
            {
                legendDisplay: true,
                legendPosition: "right",
                maintainAspectRatio: false,
                responsive: true,
                usePointStyle: true,
                pointStyle: "circle",
            }
        );

        $scope.createPieChart(
            "ResultSumChart",
            ["Published", "Pending"],
            [$scope.DashboardData.ResultSumDet.RSPublished, $scope.DashboardData.ResultSumDet.RSPending],
            ["#28a745", "#dc3545"],
            { legendDisplay: false, maintainAspectRatio: false }
        );

        $scope.createPieChart(
            "UERChart",
            ["This Week", "This Month"],
            [$scope.DashboardData.ResultSumDet.UERThisWeek, $scope.DashboardData.ResultSumDet.UERThisMonth],
            ["#28a745", "#dc3545"],
            { legendDisplay: false, maintainAspectRatio: false }
        );

        $scope.createPieChart(
            "REResultChart",
            ["This Week", "This Month"],
            [$scope.DashboardData.ResultSumDet.REThisWeek, $scope.DashboardData.ResultSumDet.REThisMonth],
            ["#28a745", "#dc3545"],
            { legendDisplay: false, maintainAspectRatio: false }
        );

        $scope.createPieChart(
            "examStatusChart",
            ["Completed", "Pending"],
            [$scope.DashboardData.ExamCompVsPendDet.ExamComVsPenC, $scope.DashboardData.ExamCompVsPendDet.ExamComVsPenP],
            ["#36c3e0", "#ffb74d"],
            {
                legendDisplay: true,
                maintainAspectRatio: false,
                responsive: true,
                legendPosition: "right",
                usePointStyle: true,
                pointStyle: "circle",
            }
        );

        $scope.createBarChart(
            "testChart",
            $scope.DashboardData.CTConductedColl.map(x => x.ClassName),
            [{
                label: "Tests",
                data: $scope.DashboardData.CTConductedColl.map(x => x.TotalCT),
                backgroundColor: "#4361ee"
            }],
            {
                responsive: true,
                legendDisplay: false,
                maintainAspectRatio: false,
                scales: {
                    x: {
                        beginAtZero: true,
                        grid: { display: false },
                        ticks: {
                            autoSkip: false
                        }

                    },
                    y: {
                        max: 10,
                        grid: { display: false },
                        ticks: {
                            autoSkip: false
                        }

                    }
                },

            }
        );

        $scope.createBarChart(
            "attendanceChart",
            $scope.DashboardData.ClassWiseAttendSumColl.map(x => x.ClassName),
            [
                {
                    label: 'Present',
                    data: $scope.DashboardData.ClassWiseAttendSumColl.map(x => x.CWASPresent),
                    backgroundColor: '#4361ee'
                },
                {
                    label: 'Absent',
                    data: $scope.DashboardData.ClassWiseAttendSumColl.map(x => x.CWASAbsent),
                    backgroundColor: '#7cc242'
                }
            ],
            {
                maintainAspectRatio: false,
                legendDisplay: true,
                legendPosition: 'bottom',
                legendLabels: {
                    usePointStyle: true,
                    pointStyle: 'circle',
                    boxWidth: 8
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        grid: { color: '#eee' }
                    },
                    x: {
                        grid: { display: false }
                    }
                }
            }
        );

        $scope.createBarChart(
            "subjectChart",
            $scope.DashboardData.SWPendReExamsColl.map(x => x.SubjectName),
            [{
                data: $scope.DashboardData.SWPendReExamsColl.map(x => x.SWReExamPend),
                backgroundColor: [
                    '#1e88e5', '#42a5f5', '#c49a00', '#f1d6a8', '#00c853',
                    '#fbc02d', '#0d47a1', '#2e7d32', '#66bb6a', '#ffee58'
                ],
                borderRadius: 4,
            }],
            {
                indexAxis: 'y',
                maintainAspectRatio: false,
                legendDisplay: false,
                scales: {
                    x: {
                        display: false,
                        beginAtZero: true,
                        grid: { display: false }
                    },
                    y: {
                        grid: { display: false }
                    }
                }
            }
        );
        $scope.MaxForTotExamScheduled = Math.max(...$scope.DashboardData.TotExamSchColl.map(x => x.TotalExamSch));
        $scope.TrachDataForTotExamScheduled = [];
        $scope.DashboardData.TotExamSchColl.forEach(function (item) {
            $scope.TrachDataForTotExamScheduled.push($scope.MaxForTotExamScheduled + 10);
        });

        $scope.createBarChart(
            "totalExamsscheduled",
            $scope.DashboardData.TotExamSchColl.map(x => x.ExamName),
            [
                {
                    label: 'Track',
                    data: $scope.TrachDataForTotExamScheduled,
                    backgroundColor: '#eeeeee',
                    borderRadius: 12,
                    borderSkipped: false,
                    barThickness: 25,
                    grouped: false,
                    order: 2
                },
                {
                    label: 'Exams',
                    data: $scope.DashboardData.TotExamSchColl.map(x => x.TotalExamSch),
                    backgroundColor: ['#7CB342', '#7CFC4E', '#B6D800', '#66D19E'],
                    borderRadius: 12,
                    borderSkipped: false,
                    barThickness: 25,
                    grouped: false,
                    order: 1
                }
            ],
            {
                responsive: true,
                maintainAspectRatio: false,
                legendDisplay: true,
                legendPosition: 'bottom',
                legendOnClick: function () { },
                indexAxis: 'y',

                legendLabels: {
                    usePointStyle: true,
                    boxWidth: 4,
                    font: { size: 9 },
                    generateLabels: function (chart) {
                        const data = chart.data;
                        return data.labels.map(function (label, i) {
                            return {
                                text: '  ' + label,
                                fillStyle: data.datasets[1].backgroundColor[i],
                                strokeStyle: data.datasets[1].backgroundColor[i],
                                hidden: false,
                                index: i
                            };
                        });
                    }
                },

                scales: {
                    x: {
                        display: false,
                        beginAtZero: true,
                        max: 100,
                        grid: { display: false }
                    },
                    y: {
                        display: false,
                        grid: { display: false }
                    }
                },
            }
        );


        $scope.createLineChart(
            "resultChart",
            $scope.DashboardData.ExamResultStatColl.map(x => x.ClassName),
            [{
                label: "Completed",
                data: $scope.DashboardData.ExamResultStatColl.map(x => x.ERSTotal),
                borderColor: "#f4b400",
                backgroundColor: "#f4b400",
                tension: 0.4,
                fill: false,
                pointBackgroundColor: "#000"
            }],
            {
                legendDisplay: false,
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true, max: 10, ticks: {
                            autoSkip: false
                        }
                    },
                    x: {
                        ticks: {
                            autoSkip: false
                        }
                    }
                },
                plugins: {
                    tooltip: {
                        callbacks: {
                            title: (items) => items[0].label,
                            label: (ctx) => {
                                const row = $scope.DashboardData.ExamResultStatColl[ctx.dataIndex];
                                return [
                                    `Total: ${row.ERSTotal}`,
                                    `Completed: ${row.ERSCompleted}`,
                                    `Pending: ${row.ERSPending}`,
                                ];
                            }
                        }
                    }
                }
            }
        );

        $scope.createLineChart(
            "classChart",
            $scope.DashboardData.CWPendReExamColl.map(x => x.ClassName),
            [{
                label: 'Pending',
                data: $scope.DashboardData.CWPendReExamColl.map(x => x.CWPendReExamNo),
                borderColor: '#65b32e',
                backgroundColor: 'rgba(101,179,46,0.2)',
                tension: 0.4,
                fill: true,
                pointRadius: 4,
                pointBackgroundColor: '#65b32e'
            }],
            {
                maintainAspectRatio: false,
                legendDisplay: false,
                scales: {
                    y: {
                        beginAtZero: true,
                        max: 100,
                        ticks: {
                            stepSize: 20
                        }
                    }
                }

            }
        );

        $scope.createDoughnutChart(
            "marksChart",
            ['Completed', 'Pending'],
            [$scope.DashboardData.MarkEntryStatDet.MarkEntryStatComp, $scope.DashboardData.MarkEntryStatDet.MarkEntryStatPend],
            ['#7cc242', '#0b7d4f'],
            {
                maintainAspectRatio: false,
                responsive: true,
                cutout: '50%',
                legendDisplay: true,
                legendPosition: 'bottom'
            }
        );
    }

    $scope.doughnutCharts = $scope.doughnutCharts || {};

    $scope.createDoughnutChart = function (canvasId, labels, values, colors, opts) {
        opts = opts || {};

        const canvas = document.getElementById(canvasId);
        if (!canvas) return;
        if ($scope.doughnutCharts[canvasId]) {
            $scope.doughnutCharts[canvasId].destroy();
        }

        const ctx = canvas.getContext('2d');

        $scope.doughnutCharts[canvasId] = new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: labels,
                datasets: [{
                    data: values,
                    backgroundColor: colors
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: opts.maintainAspectRatio ?? true,
                cutout: opts.cutout ?? '70%',
                plugins: {
                    legend: {
                        display: opts.legendDisplay ?? true,
                        position: opts.legendPosition ?? 'bottom',
                        labels: {
                            usePointStyle: true,
                            pointStyle: 'circle',
                            boxWidth: 8
                        }
                    }
                }
            }
        });
    };



    $scope.lineCharts = $scope.lineCharts || {};

    $scope.createLineChart = function (canvasId, labels, datasets, opts) {
        opts = opts || {};

        const canvas = document.getElementById(canvasId);
        if (!canvas) return;

        if ($scope.lineCharts[canvasId]) {
            $scope.lineCharts[canvasId].destroy();
        }

        const ctx = canvas.getContext('2d');

        $scope.lineCharts[canvasId] = new Chart(ctx, {
            type: 'line',
            data: { labels, datasets },
            options: {
                responsive: true,
                maintainAspectRatio: opts.maintainAspectRatio ?? true,

                plugins: Object.assign(
                    {
                        legend: {
                            display: opts.legendDisplay ?? false,
                            position: opts.legendPosition ?? 'top'
                        }
                    },
                    opts.plugins || {}
                ),

                scales: opts.scales ?? {}
            }
        });
    };



    $scope.pieCharts = $scope.pieCharts || {};

    $scope.createPieChart = function (canvasId, labels, values, colors, opts) {
        opts = opts || {};

        const canvas = document.getElementById(canvasId);
        if (!canvas) return;

        // Destroy existing chart
        if ($scope.pieCharts[canvasId]) {
            $scope.pieCharts[canvasId].destroy();
        }

        const ctx = canvas.getContext('2d');

        $scope.pieCharts[canvasId] = new Chart(ctx, {
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
                maintainAspectRatio: opts.maintainAspectRatio ?? false,
                plugins: {
                    legend: {
                        display: opts.legendDisplay ?? false,
                        position: opts.legendPosition ?? 'right',
                        labels: {
                            usePointStyle: true,
                            pointStyle: 'circle',
                            boxWidth: 8
                        }
                    }
                }
            }
        });
    };


    $scope.barCharts = $scope.barCharts || {};

    $scope.createBarChart = function (canvasId, labels, datasets, opts) {
        opts = opts || {};

        const canvas = document.getElementById(canvasId);
        if (!canvas) return;

        if ($scope.barCharts[canvasId]) {
            $scope.barCharts[canvasId].destroy();
        }

        const ctx = canvas.getContext('2d');

        $scope.barCharts[canvasId] = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: datasets
            },
            options: {
                responsive: true,
                maintainAspectRatio: opts.maintainAspectRatio ?? true,
                plugins: {
                    legend: {
                        display: opts.legendDisplay ?? false,
                        position: opts.legendPosition ?? 'top',
                        onClick: opts.legendOnClick ?? undefined,
                        labels: opts.legendLabels ?? {
                            usePointStyle: true,
                            pointStyle: 'circle',
                            boxWidth: 8
                        }
                    },
                    tooltip: {
                        filter: function (ctx) {
                            return ctx.dataset.label !== 'Track';
                        }
                    }
                },
                indexAxis: opts.indexAxis ?? 'x',
                scales: opts.scales ?? {},
            }
        });
    };



    $scope.InitializeUpcommingExamDet = function (dates, classes, subjects, teachers) {
        const headerRow = document.getElementById("examDatesRow");
        dates.forEach(d => {
            const th = document.createElement("th");
            th.className = "date-head";
            th.innerText = d;
            headerRow.appendChild(th);
        });
        const body = document.getElementById("examBody");

        classes.forEach((cls, i) => {
            const tr = document.createElement("tr");
            const classTd = document.createElement("td");
            classTd.className = "class-name";
            classTd.innerText = cls;
            tr.appendChild(classTd);
            eachSubjects = subjects[i].split(",");
            eachTeachers = teachers[i].split(",");
            dates.forEach((date, i) => {
                const td = document.createElement("td");
                td.className = "exam-cell";
                td.innerHTML = `
            <div class="exam-subject">${eachSubjects[i]}</div>
            <div class="exam-teacher">${eachTeachers[i]}</div>
            `;
                tr.appendChild(td);
            });
            body.appendChild(tr);
        });
    }
    $scope.GetExamDashboardData = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.DashboardData = {};
        $http({
            method: 'POST',
            url: base_url + "Exam/Report/GetDashboardData",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.DashboardData = res.data.Data;
                $scope.TopPerformers = $scope.DashboardData.TopPerformerColl;
                $scope.Awards = $scope.DashboardData.AwardColl;


                $scope.resultSummary = $scope.DashboardData.ResultSumDet;


                const UpcommingExamDate = $scope.DashboardData.UpcommingExamColl.map(x => x.ExamMitti);

                const UpcommingClasses = $scope.DashboardData.UpcommingExamColl.map(x => x.ClassName);
                const UpcommingSub = $scope.DashboardData.UpcommingExamColl.map(x => x.Subjects);
                const UpcommingTeach = $scope.DashboardData.UpcommingExamColl.map(x => x.Teachers);

                $scope.InitializeUpcommingExamDet(UpcommingExamDate, UpcommingClasses, UpcommingSub, UpcommingTeach);
                $timeout(function () {
                    $scope.initializeCharts();
                });
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }
});
