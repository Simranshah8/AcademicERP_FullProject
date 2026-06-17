app.controller('HAAnalysisController', function ($scope, $http, $timeout, $filter, GlobalServices) {

    $scope.Title = 'Homework and Assignment Analysis';

    $scope.LoadData = function () {
        $scope.GetAnalysisData();
    };

    $scope.RenderCharts = function () {
        //Creation of all the bar chart
        const BarchartThemes = {
            homework: {
                submitted: '#3F63D3',
                submittedTop: '#2E4FB8',
                pending: '#7AC143',
                pendingTop: '#5FA52F',
                overdue: '#F4A641',
                bg: '#F3FAFD',
                barthickness: 26,
                borderradius: 0,
                pointradius: 5,
                mode: "nearest",
                maxYScale: 120
            },
            Assignment: {
                submitted: '#0A7CA6',
                pending: '#EB8E86',
                overdue: '#F4AE4F',
                bg: '#FFF7EC',
                barthickness: 26,
                borderradius: 0,
                pointradius: 5,
                mode: "nearest",
                maxYScale: 120
            },
            ClassAndSectionWiseSummeryHomework: {
                totalhw: '#F4BE4A',
                submitted: '#2E9E5E',
                pending: '#2F73FF',
                overdue: '#FF3B3B',
                borderradius: 14,
                barthickness: 30,
                bg: '#F3FAFD',
                pointradius: 0,
                mode: "index",
                maxYScale: 140
            },
            ClassAndSectionWiseSummeryAssignment: {
                totalassign: '#5FB7B8',
                submitted: '#2E9E5E',
                pending: '#2F73FF',
                overdue: '#FF3B3B',
                borderradius: 14,
                barthickness: 30,
                bg: ' #FFF7EC',
                pointradius: 0,
                mode: "index",
                maxYScale: 140
            }
        };
        const datasetsConfig = [
            { key: 'submitted', label: 'Submitted', type: 'bar', order: 3 },
            { key: 'pending', label: 'Pending', type: 'bar', order: 2 },
            { key: 'overdue', label: 'Overdue', type: 'line', order: 1 }
        ];
        const datasetsConfigForSummaryHW = [
            { key: 'totalhw', label: 'Total Homework(TH)', type: 'bar', order: 4 },
            { key: 'submitted', label: 'Submitted', type: 'line', order: 3 },
            { key: 'pending', label: 'Pending', type: 'line', order: 2 },
            { key: 'overdue', label: 'Overdue', type: 'line', order: 1 }
        ];
        const datasetsConfigForSummaryAssign = [
            { key: 'totalassign', label: 'Total Assignment(TA)', type: 'bar', order: 4 },
            { key: 'submitted', label: 'Submitted', type: 'line', order: 3 },
            { key: 'pending', label: 'Pending', type: 'line', order: 2 },
            { key: 'overdue', label: 'Overdue', type: 'line', order: 1 }
        ];
        $scope.createBarChart(
            'TotalHomework',
            $scope.AnalysisData.TotalHWColl.map(x => x.ClassName),
            {
                submitted: $scope.AnalysisData.TotalHWColl.map(x => x.THSubmitted),
                pending: $scope.AnalysisData.TotalHWColl.map(x => x.THPending),
                overdue: $scope.AnalysisData.TotalHWColl.map(x => x.THOverdue)
            },
            BarchartThemes.homework,
            datasetsConfig
        );

        $scope.createBarChart(
            'TotalAssignment',
            $scope.AnalysisData.TotalAssignColl.map(x => x.ClassName),
            {
                submitted: $scope.AnalysisData.TotalAssignColl.map(x => x.TASubmitted),
                pending: $scope.AnalysisData.TotalAssignColl.map(x => x.TAPending),
                overdue: $scope.AnalysisData.TotalAssignColl.map(x => x.TAOverdue)
            },
            BarchartThemes.Assignment,
            datasetsConfig
        );
        $scope.createBarChart(
            'HomeworkClassAndSectionwiseSummary',
            $scope.AnalysisData.CSWSummaryHWColl.map(
                x => `${x.ClassName} ${x.SectionName}`
            ),
            {
                totalhw: $scope.AnalysisData.CSWSummaryHWColl.map(x => x.CSWTH),
                submitted: $scope.AnalysisData.CSWSummaryHWColl.map(x => x.CSWTHSubmitted),
                pending: $scope.AnalysisData.CSWSummaryHWColl.map(x => x.CSWTHPending),
                overdue: $scope.AnalysisData.CSWSummaryHWColl.map(x => x.CSWTHOverdue)
            },
            BarchartThemes.ClassAndSectionWiseSummeryHomework,
            datasetsConfigForSummaryHW
        );

        $scope.createBarChart(
            'AssignmentClassAndSectionwiseSummary',
            $scope.AnalysisData.CSWSummaryAssignColl.map(
                x => `${x.ClassName} ${x.SectionName}`
            ),
            {
                totalassign: $scope.AnalysisData.CSWSummaryAssignColl.map(x => x.CSWTA),
                submitted: $scope.AnalysisData.CSWSummaryAssignColl.map(x => x.CSWTASubmitted),
                pending: $scope.AnalysisData.CSWSummaryAssignColl.map(x => x.CSWTAPending),
                overdue: $scope.AnalysisData.CSWSummaryAssignColl.map(x => x.CSWTAOverdue)
            },
            BarchartThemes.ClassAndSectionWiseSummeryAssignment,
            datasetsConfigForSummaryAssign
        );
        //End of the bar chart


        //Submission Completed chart
        const backgroundColor = ['#6D5EF9', '#FF7A6E', '#33C6E8', '#FDB44B', '#4A7DFF', '#61D095', '#8B5CF6', '#2DD4BF', '#0EA5E9', '#FACC15', '#34D399', '#64748B']
        $scope.CreateSubmission('HomeworkSubmissionCompleted', $scope.AnalysisData.TotalHWColl.map(x => x.ClassName), $scope.AnalysisData.TotalHWColl.map(x => x.THSubmitted), 20, backgroundColor, false, 30);

        $scope.CreateSubmission('AssignmentSubmissionCompleted', $scope.AnalysisData.TotalAssignColl.map(x => x.ClassName), $scope.AnalysisData.TotalAssignColl.map(x => x.TASubmitted), 20, backgroundColor, false, 30);
        //End of chart


        //Pending Submission 
        $scope.CreatePending('HomeworkSubmissionPending', $scope.AnalysisData.TotalHWColl.map(x => x.ClassName), $scope.AnalysisData.TotalHWColl.map(x => x.THPending), 0);
        $scope.CreatePending('AssignmentSubmissionPending', $scope.AnalysisData.TotalAssignColl.map(x => x.ClassName), $scope.AnalysisData.TotalAssignColl.map(x => x.TAPending), 90);
        //End of the chart


        //Overdue Chart
        $scope.CreateOverdue('HomeworkOverDueHomework', $scope.AnalysisData.TotalHWColl.map(x => x.ClassName), $scope.AnalysisData.TotalHWColl.map(x => x.THOverdue));
        $scope.CreateOverdue('AssignmentOverDueAssignments', $scope.AnalysisData.TotalAssignColl.map(x => x.ClassName), $scope.AnalysisData.TotalAssignColl.map(x => x.TAOverdue));
        //End of the chart



        //Submission vs Overdue chart
        const DonutChatThemes = {
            First: {
                SubmissionColor: "#00A9D7",
                OverdueColor: "#D9D9D9",
            },
            Second: {
                SubmissionColor: "#F57C00",
                OverdueColor: "#D9D9D9",
            }
        }

        $scope.createSubmissionVsOverdueDonut('SubmissionVsOverdueFirst', DonutChatThemes.First, [$scope.AnalysisData.subVsOverdueHW.HWSubByTeach, $scope.AnalysisData.subVsOverdueHW.HWOverdueByTeach])
        $scope.createSubmissionVsOverdueDonut('SubmissionVsOverdueSecond', DonutChatThemes.Second, [$scope.AnalysisData.subVsOverdueAssign.AssignSubByTeach, $scope.AnalysisData.subVsOverdueAssign.AssignOverdueByTeach])
        //End of the chart 

        //Subjectwise submission chart
        const backgroundColorForThis = [
            '#6D5EF9', '#FF7A6E', '#33C6E8', '#FDB44B', '#4A7DFF'
        ]
        $scope.CreateSubmission('SubjectWiseSubmissionHW', $scope.AnalysisData.SPRHWColl.map(x => x.SubjectName), $scope.AnalysisData.SPRHWColl.map(x => x.SPRSubmitted), 20, backgroundColorForThis, true, 30);

        $scope.CreateSubmission('SubjectWiseSubmissionAssignment', $scope.AnalysisData.SPRAssignColl.map(x => x.SubjectName), $scope.AnalysisData.SPRAssignColl.map(x => x.SPRSubmitted), 20, backgroundColorForThis, true, 30);

        //End of the chart
    }

    const ringSeparatorPlugin = {
        id: 'ringSeparator',
        afterDatasetsDraw(chart, args, opts) {
            const meta0 = chart.getDatasetMeta(0);
            if (!meta0?.data?.length) return;

            const { ctx } = chart;
            const { x, y } = meta0.data[0];

            const gap = opts?.gapPx ?? 6;     // increase for bigger inside gap
            const color = opts?.color ?? '#fff';

            ctx.save();
            ctx.strokeStyle = color;
            ctx.lineCap = 'butt';

            chart.data.datasets.forEach((ds, di) => {
                const meta = chart.getDatasetMeta(di);
                const arc = meta?.data?.[0];
                if (!arc) return;

                const { innerRadius } = arc.getProps(['innerRadius'], true);

                ctx.lineWidth = gap;
                ctx.beginPath();
                ctx.arc(x, y, innerRadius, 0, Math.PI * 2);
                ctx.stroke();
            });

            ctx.restore();
        }
    };
    const polarBaselinePlugin = {
        id: 'polarBaseline',
        beforeDatasetsDraw(chart, args, opts) {
            const meta = chart.getDatasetMeta(0);
            if (!meta?.data?.length) return;

            const { ctx } = chart;
            const { x, y } = meta.data[0];
            const inner = opts?.innerRadiusPx ?? 60; // one default

            ctx.save();
            ctx.beginPath();
            ctx.rect(0, 0, chart.width, chart.height);
            ctx.arc(x, y, inner, 0, Math.PI * 2, true);
            ctx.clip('evenodd');
        },
        afterDatasetsDraw(chart, args, opts) {
            const { ctx } = chart;
            ctx.restore();

            const meta = chart.getDatasetMeta(0);
            if (!meta?.data?.length) return;

            const { x, y } = meta.data[0];
            const inner = opts?.innerRadiusPx ?? 60; // same default

            ctx.save();
            ctx.beginPath();
            ctx.arc(x, y, inner, 0, Math.PI * 2);
            ctx.strokeStyle = opts?.ringColor ?? '#e9ecef';
            ctx.lineWidth = opts?.ringWidth ?? 2;
            ctx.stroke();
            ctx.restore();
        }
    };



    $scope.createBarChart = function (canvasId, labels, data, theme, datasetsConfig) {

        const chartBgPlugin = {
            id: 'customBg',
            beforeDraw: (chart) => {
                const ctx = chart.ctx;
                ctx.save();
                ctx.globalCompositeOperation = 'destination-over';
                ctx.fillStyle = theme.bg;
                ctx.fillRect(0, 0, chart.width, chart.height);
                ctx.restore();
            }
        };
        const datasets = datasetsConfig.map(cfg => ({
            label: cfg.label,
            type: cfg.type,
            data: data[cfg.key],
            backgroundColor: theme[cfg.key],
            borderColor: theme[cfg.key],
            borderRadius: theme.borderradius,
            barThickness: cfg.type === 'bar' ? theme.barthickness : undefined,
            tension: cfg.type === 'line' ? 0.4 : undefined,
            pointRadius: cfg.type === 'line' ? theme.pointradius : undefined,
            pointHitRadius: cfg.type === 'line' ? 15 : undefined,
            order: cfg.order,
            fill: false
        }));


        const ctx = document.getElementById(canvasId).getContext('2d');

        return new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                interaction: {
                    mode: theme.mode,
                    intersect: false,
                    axis: 'x'
                },
                plugins: {
                    tooltip: {
                        mode: theme.mode,
                        intersect: false
                    },
                    legend: {
                        position: 'bottom',
                        labels: {
                            usePointStyle: true,
                            padding: 20
                        },
                        reverse: true
                    },
                },
                scales: {
                    y: {
                        min: 0,
                        max: theme.maxYScale,
                        ticks: {
                            stepSize: 20
                        },
                    }
                }
            },
            plugins: [chartBgPlugin]
        });
    }


    $scope.CreateSubmission = function (canvasId, labels, rawValues, offset, backgroundColor, legendInPer, innerRadiusPx) {
        const filterLabel = labels.map((l, i) => ({ label: l, pct: rawValues[i] ?? 0 }))
            .filter(x => x.pct > 0);

        labels = filterLabel.map(x => x.label);
        rawValues = filterLabel.map(x => x.pct);
        if (legendInPer) {
            legend = {
                position: 'right',
                onClick(e, legendItem, legend) {
                    const chart = legend.chart;
                    const i = legendItem.index;

                    chart.toggleDataVisibility(i);
                    chart.update();
                },
                labels: {
                    usePointStyle: true,
                    pointStyle: 'circle',
                    boxWidth: 6,
                    padding: 18,
                    font: { size: 9 },


                    generateLabels(chart) {
                        const data = chart.data;
                        const dataset = data.datasets[0];

                        const raw = rawValues.map(v => Number(v || 0));
                        const total = raw.reduce((s, v) => s + v, 0);

                        return data.labels.map((label, i) => {
                            const value = raw[i] ?? 0;
                            const pct = total ? (value / total) * 100 : 0;
                            const visible = chart.getDataVisibility(i);
                            return {
                                text: [
                                    label,
                                    `${pct.toFixed(1)}%`
                                ],
                                fillStyle: dataset.backgroundColor[i],
                                strokeStyle: dataset.backgroundColor[i],
                                hidden: !visible,
                                index: i
                            };
                        });
                    }
                }
            }
        }
        else {
            legend = {
                position: 'right',
                labels: {
                    usePointStyle: true,
                    pointStyle: 'circle',
                    boxWidth: 4,
                    boxHeight: 4,
                    padding: 10,
                    font: { size: 9 }
                }
            }
        }
        const data = rawValues.map(v => v + offset);

        return new Chart(document.getElementById(canvasId), {
            type: 'polarArea',
            data: {
                labels,
                datasets: [{
                    data,
                    backgroundColor,
                    borderWidth: 0,
                    borderColor: '#ffffff'
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                startAngle: -90 * (Math.PI / 180),

                plugins: {
                    legend,
                    tooltip: {
                        callbacks: {
                            label: function (ctx) {
                                const i = ctx.dataIndex;
                                const label = ctx.label || '';
                                const original = rawValues[i] ?? 0;

                                const total = rawValues.reduce((s, v) => s + Number(v || 0), 0);
                                const pct = total ? (original / total) * 100 : 0;

                                return `${label}: ${original} (${pct.toFixed(1)}%)`;
                            }
                        }
                    },

                    polarBaseline: {
                        innerRadiusPx: innerRadiusPx,
                        ringColor: '#e9ecef',
                        ringWidth: 2
                    }
                },

                scales: {
                    r: {
                        ticks: { display: false },
                        grid: { display: false },
                        angleLines: { display: false }
                    }
                }
            },
            plugins: [polarBaselinePlugin]
        });
    }



    $scope.CreatePending = function (canvasId, labels, values, startsFrom) {
        const filterLabel = labels.map((l, i) => ({ label: l, pct: values[i] ?? 0 }))
            .filter(x => x.pct > 0);

        labels = filterLabel.map(x => x.label);
        values = filterLabel.map(x => x.pct);

        const gray = '#F2F2F2';
        const bg = '#ffffff';

        const colors = [
            '#7C6AF7', '#2DD4BF', '#FF7A6E', '#FDB44B', '#8BD17C',
            '#4A7DFF', '#A78BFA', '#34D399', '#22C55E', '#0EA5E9'
        ];

        const datasets = labels.map((lbl, i) => {
            const v = Number(values[i] ?? 0);
            const total = values.reduce((s, v) => s + Number(v || 0), 0);
            const pct = total > 0
                ? Number(((v / total) * 100).toFixed(2))
                : 0;

            return {
                label: lbl,
                data: [pct, 100 - pct],
                backgroundColor: [colors[i % colors.length], gray],
                spacing: 0
            };
        });

        return new Chart(document.getElementById(canvasId), {
            type: 'doughnut',
            data: {
                labels: ['Value', 'Remaining'],
                datasets: datasets
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: '5%',
                rotation: startsFrom,
                circumference: 360,
                plugins: {
                    legend: {
                        position: 'right',
                        labels: {
                            usePointStyle: true,
                            pointStyle: 'circle',
                            boxWidth: 6,
                            boxHeight: 6,
                            padding: 14,
                            font: { size: 10 },

                            generateLabels(chart) {
                                return chart.data.datasets.map((ds, i) => {
                                    const ringColor = Array.isArray(ds.backgroundColor)
                                        ? ds.backgroundColor[0]
                                        : ds.backgroundColor;

                                    return {
                                        text: ds.label,
                                        fillStyle: ringColor,
                                        strokeStyle: ringColor,
                                        lineWidth: 0,
                                        hidden: !chart.isDatasetVisible(i),
                                        datasetIndex: i
                                    };
                                });
                            }
                        },
                        onClick(e, item, legend) {
                            const chart = legend.chart;
                            const di = item.datasetIndex;
                            chart.setDatasetVisibility(di, !chart.isDatasetVisible(di));
                            chart.update();
                        }
                    },

                    tooltip: {
                        filter: (item) => item.dataIndex === 0,
                        callbacks: {
                            label: (ctx) => `${ctx.dataset.label}: ${ctx.raw}%`
                        }
                    },

                    ringSeparator: { gapPx: 4, color: bg }
                }
            },
            plugins: [ringSeparatorPlugin]
        });
    }




    $scope.CreateOverdue = function (canvasId, labels, values) {
        const filterLabel = labels.map((l, i) => ({ label: l, pct: values[i] ?? 0 }))
            .filter(x => x.pct > 0);

        labels = filterLabel.map(x => x.label);
        values = filterLabel.map(x => x.pct);

        return new Chart(document.getElementById(canvasId), {
            type: 'doughnut',
            data: {
                labels,
                datasets: [{
                    data: values,
                    backgroundColor: [
                        '#6D5EF9', '#3B82F6', '#EF4444', '#FDB44B', '#84CC16',
                        '#22C55E', '#2DD4BF', '#0B3B8C', '#8B5CF6', '#A855F7'
                    ],
                    borderWidth: 0,
                    spacing: 4,
                    borderRadius: 12,
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: '30%',
                plugins: {
                    legend: {
                        position: 'right',
                        labels: {
                            usePointStyle: true,
                            pointStyle: 'circle',
                            boxWidth: 4,
                            boxHeight: 4,
                            padding: 14,
                            font: { size: 10 }
                        }
                    },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                const label = context.label || '';
                                const value = Number(context.parsed) || 0;

                                const dataArr = context.dataset.data || [];
                                const total = dataArr.reduce((sum, v) => sum + (Number(v) || 0), 0);

                                const pct = total ? ((value / total) * 100) : 0;
                                return `${label}: ${value} (${pct.toFixed(1)}%)`;
                            }
                        }
                    }
                },
            },
        });
    }




    $scope.createSubmissionVsOverdueDonut = function (canvasId, theme, data) {
        var labels = ['Submission', 'Overdue']
        const el = document.getElementById(canvasId);
        if (!el) return;

        return new Chart(el, {
            type: 'doughnut',
            data: {
                labels,
                datasets: [
                    {
                        data,
                        backgroundColor: [theme.SubmissionColor, theme.OverdueColor],
                        borderWidth: 0,
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: '65%',

                plugins: {
                    legend: {
                        position: 'right',
                        fullSize: false,
                        onClick(e, legendItem, legend) {
                            const chart = legend.chart;
                            const i = legendItem.index;

                            chart.toggleDataVisibility(i);
                            chart.update();
                        },
                        labels: {
                            usePointStyle: true,
                            pointStyle: 'circle',
                            align: 'start',
                            boxWidth: 5,
                            padding: 15,
                            font: { size: 9 },
                            generateLabels(chart) {
                                const dataset = chart.data.datasets[0];
                                const data = dataset.data;
                                const labels = chart.data.labels;

                                const total = data.reduce((a, b) => a + b, 0);
                                return labels.map((label, i) => {
                                    const value = data[i];
                                    const pct = total ? Math.round((value / total) * 100) : 0;
                                    const visible = chart.getDataVisibility(i);

                                    return {
                                        text: [
                                            label,
                                            `${pct}%`
                                        ],
                                        fillStyle: dataset.backgroundColor[i],
                                        strokeStyle: dataset.backgroundColor[i],
                                        lineWidth: 0,
                                        hidden: !visible,
                                        index: i
                                    };
                                });
                            }
                        },
                    },
                    tooltip: {
                        callbacks: {
                            label: (ctx) => `${ctx.label}: ${ctx.raw}%`
                        }
                    }
                }
            },
        });
    }


    $scope.GetAnalysisData = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.DashboardData = {};
        $http({
            method: 'POST',
            url: base_url + "Homework/Reporting/GetAnalysisData",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.AnalysisData = res.data.Data;
                $timeout(function () {
                    $scope.RenderCharts();
                });
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

});
