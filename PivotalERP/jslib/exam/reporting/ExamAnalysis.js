app.controller(
  "ExamAnalysisController",
  function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.LoadData = function () {
      $scope.GetExamAnalysisData();
    };

    function getCtx(canvasId) {
      var el = document.getElementById(canvasId);
      return el ? el.getContext("2d") : null;
    }

    //Only for ReExam Marks Analysis
    $scope.FindHighestYMax = function (a, b) {
      const result = Array.from(
        { length: Math.max(a.length, b.length) },
        (_, i) => (a[i] || 0) + (b[i] || 0),
      );

      const maxYScale = Math.ceil((Math.max(...result) + 10) / 10) * 10;
      return maxYScale;
    };

    $scope.initializeCharts = function () {
      //Creation of all the bar chart
      const BarchartThemes = {
        ExaminationPerformanceAnalysis: {
          Average: "#3f63d8",
          Pass: "#77c34f",
          maxYScale:
            Math.ceil(
              (Math.max(
                ...$scope.AnalysisData.ExamPerAnalysisColl.flatMap((x) => [
                  x.EPAAverage,
                  x.EPAPass,
                ]),
              ) +
                10) /
                10,
            ) * 10,
        },
        ReExamMarksAnalysis: {
          Regular: "#5b7cc3",
          Re_exam: "#35b8d8",
          borderRadius: 6,
          maxYScale: $scope.FindHighestYMax(
            $scope.AnalysisData.ReExamMarkAnalysisColl.flatMap(
              (x) => x.REMAReExam,
            ),
            $scope.AnalysisData.ReExamMarkAnalysisColl.flatMap(
              (x) => x.REMARegular,
            ),
          ),
        },
        ExamTypewiseResultDispatch: {
          borderRadius: 12,
        },
        ClasswiseCASSummary: {
          maxYScale: Math.ceil(
            Math.max(
              ...$scope.AnalysisData.ClassWiseCASSumColl.map((x) => x.CWCASTot),
            ) + 10,
          ),
        },
      };

      const datasetsConfigExamPer = [
        { key: "Average", label: "Average", type: "bar" },
        { key: "Pass", label: "Pass", type: "bar" },
      ];
      const datasetsConfigremark = [
        { key: "Regular", label: "Regular", type: "bar" },
        { key: "Re_exam", label: "Re-exam", type: "bar" },
      ];

      $scope.createBarChart(
        "examPerformanceChart",
        $scope.AnalysisData.ExamPerAnalysisColl.map((x) => x.SubjectName),
        {
          Average: $scope.AnalysisData.ExamPerAnalysisColl.map(
            (x) => x.EPAAverage,
          ),
          Pass: $scope.AnalysisData.ExamPerAnalysisColl.map((x) => x.EPAPass),
        },
        BarchartThemes.ExaminationPerformanceAnalysis,
        datasetsConfigExamPer,
      );

      //Class-wise CAS Summary Bar Chart
      $scope.createBarChart(
        "casBar",
        $scope.AnalysisData.ClassWiseCASSumColl.map((x) => x.ClassName),
        $scope.AnalysisData.ClassWiseCASSumColl.map((x) => x.CWCASTot),
        BarchartThemes.ClasswiseCASSummary,
        [],
        {
          categoryPercentage: 0.9,
          barPercentage: 0.9,
          legendDisplay: false,
        },
      );
      //Re-Exam Marks Analysis Chart (Stacked)
      $scope.createBarChart(
        "reExamChart",
        $scope.AnalysisData.ReExamMarkAnalysisColl.map((x) => x.SubjectName),
        {
          Regular: $scope.AnalysisData.ReExamMarkAnalysisColl.map(
            (x) => x.REMARegular,
          ),
          Re_exam: $scope.AnalysisData.ReExamMarkAnalysisColl.map(
            (x) => x.REMAReExam,
          ),
        },
        BarchartThemes.ReExamMarksAnalysis,
        datasetsConfigremark,
        {
          stacked: true,
          topOnlyRadius: true,
        },
      );

      //Mark Distribution by Exam Chart
      const highestArr = $scope.AnalysisData.MarkDistByExamColl.map(
        (x) => x.MDEHighestMark,
      ); // array
      const lowestArr = $scope.AnalysisData.MarkDistByExamColl.map(
        (x) => x.MDELowestMark,
      );
      const avgArr = highestArr.map((h, i) => {
        const l = lowestArr[i];
        return l != null && h != null ? Number(((h + l) / 2).toFixed(2)) : h;
      });

      const markDistributionColors = [
        "#ffb84d",
        "#7ac943",
        "#3b82f6",
        "#ef4444",
        "#22C55E",
        "#3B82F6",
        "#EF4444",
        "#FBBF24",
      ];
      $scope.createDoughnutChart(
        "markDistributionChart",
        $scope.AnalysisData.MarkDistByExamColl.map((x) => x.ExamName),
        avgArr,
        markDistributionColors,
        {
          cutout: 40,
          borderWidth: 4,
          borderRadius: 12,
          highestArr: highestArr,
          lowestArr: lowestArr,
          tooltipMode: "stats",
        },
      );

      //CAS Performance Donut Chart
      const casDonutColors = ["#047857", "#7ac943"];
      $scope.createDoughnutChart(
        "casDonut",
        ["Setup", "Entry"],
        [
          $scope.AnalysisData.CASPerformanceAnalysisDet.CASPASetup,
          $scope.AnalysisData.CASPerformanceAnalysisDet.CASPAEntry,
        ],
        casDonutColors,
        {
          cutout: 65,
          tooltipMode: "simple",
        },
      );

      //Attendance Analysis Chart
      $scope.createLineChart(
        "attendanceChart",
        $scope.AnalysisData.AttendAnalysisColl.map((x) => x.SubjectName),
        {
          data: $scope.AnalysisData.AttendAnalysisColl.map((x) => x.AATotal),
        },
        {
          borderColor: "#1f6fbf",
          pointBackgroundColor: "#1f6fbf",
          maxYScale:
            Math.ceil(
              (Math.max(
                ...$scope.AnalysisData.AttendAnalysisColl.map((x) => x.AATotal),
              ) +
                10) /
                10,
            ) * 10,
        },
        {
          yGridDisplay: true,
          xGridDisplay: true,
        },
      );

      //Class-wise Performance Chart
      $scope.createLineChart(
        "classPerformanceChart",
        $scope.AnalysisData.CWPerAnalysisColl.map((x) => x.ClassName),
        {
          Average: $scope.AnalysisData.CWPerAnalysisColl.map((x) => x.CWPAAvg),
          Pass: $scope.AnalysisData.CWPerAnalysisColl.map((x) => x.CWPAPass),
        },
        {
          Average: {
            borderColor: "#006b4f",
            pointBorderColor: "#006b4f",
          },
          Pass: {
            borderColor: "#7cc242",
            pointBorderColor: "#7cc242",
          },
          maxYScale:
            Math.ceil(
              (Math.max(
                ...$scope.AnalysisData.CWPerAnalysisColl.flatMap((x) => [
                  x.CWPAAvg,
                  x.CWPAPass,
                ]),
              ) +
                10) /
                10,
            ) * 10,
        },
        {
          yGridDisplay: true,
          xGridDisplay: true,
        },
      );

      //Bulk Attendance (polar) Chart
      const bulkAttendanceColors = ["#ffb74d", "#6fcf97", "#8a63d2", "#ff8a80"];
      $scope.createPolarAreaChart(
        "bulkAttendanceChart",
        $scope.AnalysisData.BulkAttendColl.map((x) => x.ExamName),
        $scope.AnalysisData.BulkAttendColl.map((x) => x.BulkAttendTot),
        bulkAttendanceColors,
      );

      //Exam Type Result Dispatch Chart
      $scope.createHorizontalChart(
        "examTypeChart",
        $scope.AnalysisData.ETResultDispatchColl.map((x) => x.SubjectName),
        $scope.AnalysisData.ETResultDispatchColl.map((x) => x.ETRDTotal),
        [
          "#5f8f2f",
          "#7bd23c",
          "#b4db28",
          "#66cda1",
          "#79c64b",
          "#21c1a3",
          "#0fa78a",
          "#057a54",
          "#7fbf3f",
        ],
      );
    };
    $scope.initTopPerformerSlider = function () {
      $timeout(function () {
        initSliders();
      }, 0);
    };
    $scope._charts = $scope._charts || {};
    $scope.createBarChart = function (
      canvasId,
      labels,
      data,
      theme,
      datasetsConfig,
      opts,
    ) {
      if ($scope._charts[canvasId]) {
        $scope._charts[canvasId].destroy();
      }

      var ctx = getCtx(canvasId);
      if (!ctx) return null;

      theme = theme || {};
      opts = opts || {};

      var isSimple =
        Array.isArray(data) || !datasetsConfig || datasetsConfig.length === 0;
      var isStacked = !!opts.stacked || !!theme.stacked;

      var datasets;

      if (isSimple) {
        datasets = [
          {
            data: Array.isArray(data)
              ? data
              : data && data.data
                ? data.data
                : [],
            backgroundColor: theme.backgroundColor || "#3f5bd8",
            borderColor:
              theme.borderColor || theme.backgroundColor || "#3f5bd8",
            borderRadius: theme.borderRadius || 0,
            borderSkipped: false,
            barThickness:
              opts.categoryPercentage != null || opts.barPercentage != null
                ? undefined
                : theme.barthickness || 26,
            categoryPercentage: opts.categoryPercentage ?? 0.8,
            barPercentage: opts.barPercentage ?? 0.7,
          },
        ];
      } else {
        var barCfgs = datasetsConfig.filter(function (c) {
          return (c.type || "bar") === "bar";
        });
        var topBarKey =
          isStacked && opts.topOnlyRadius && barCfgs.length
            ? barCfgs[barCfgs.length - 1].key
            : null;

        datasets = datasetsConfig.map(function (cfg) {
          var type = cfg.type || "bar";
          var isTopBar = type === "bar" && cfg.key === topBarKey;
          var r = theme.borderRadius || 0;

          return {
            label: cfg.label,
            type: type,
            data: data[cfg.key],
            backgroundColor: theme[cfg.key],
            borderColor: theme[cfg.key],
            borderRadius:
              isStacked && opts.topOnlyRadius
                ? isTopBar
                  ? { topLeft: r || 8, topRight: r || 8 }
                  : 0
                : r || 0,
            borderSkipped: type === "bar" ? false : undefined,
            categoryPercentage: opts.categoryPercentage ?? 0.8,
            barPercentage: opts.barPercentage ?? 0.7,
            barThickness: type === "bar" ? theme.barthickness || 26 : undefined,
            order: cfg.order,
            fill: false,
            stack:
              isStacked && type === "bar" ? cfg.stack || "stack1" : undefined,
          };
        });
      }

      $scope._charts[canvasId] = new Chart(ctx, {
        type: "bar",
        data: {
          labels: labels,
          datasets: datasets,
        },
        options: {
          responsive: true,
          maintainAspectRatio: false,
          plugins: {
            tooltip: {
              mode: theme.mode || "nearest",
              intersect: false,
            },
            legend: {
              display:
                opts.legendDisplay !== undefined
                  ? opts.legendDisplay
                  : !isSimple,
              position: opts.legendPosition || "bottom",
              reverse: !!opts.legendReverse,
              labels: {
                usePointStyle: true,
                padding: opts.legendPadding != null ? opts.legendPadding : 20,
                boxWidth: opts.legendBoxWidth != null ? opts.legendBoxWidth : 8,
              },
            },
          },
          scales: {
            x: {
              stacked: isStacked,
              grid: {
                display:
                  opts.xGridDisplay != null ? opts.xGridDisplay : !isSimple,
              },
            },
            y: {
              stacked: isStacked,
              min: 0,
              max: theme.maxYScale || (isStacked ? 50 : 110),
              ticks: {
                beginAtZero: true,
                stepSize: theme.stepSize || 20,
              },
              grid: {
                display:
                  opts.yGridDisplay != null ? opts.yGridDisplay : !isSimple,
              },
            },
          },
          layout: opts.layoutPadding
            ? { padding: opts.layoutPadding }
            : { padding: { bottom: 2 } },
        },
      });

      return $scope._charts[canvasId];
    };

    $scope.createLineChart = function (canvasId, labels, data, theme, opts) {
      if ($scope._charts[canvasId]) {
        $scope._charts[canvasId].destroy();
      }
      var ctx = getCtx(canvasId);
      if (!ctx) return null;

      theme = theme || {};
      opts = opts || {};

      var isSingle = data && Array.isArray(data.data);
      var datasets;

      if (isSingle) {
        datasets = [
          {
            label: theme.label || "Data",
            data: data.data,
            borderColor: theme.borderColor,
            backgroundColor: theme.backgroundColor,
            pointBackgroundColor:
              theme.pointBackgroundColor || theme.borderColor,
            pointBorderColor: theme.pointBorderColor || theme.borderColor,
            pointRadius: theme.pointRadius || 4,
            fill: theme.fill !== undefined ? theme.fill : false,
            tension: theme.lineTension || 0.45,
          },
        ];
      } else {
        datasets = Object.keys(data || {}).map(function (key) {
          return {
            label: key,
            data: data[key],

            borderColor: theme[key] && theme[key].borderColor,
            backgroundColor: theme[key] && theme[key].backgroundColor,

            pointBackgroundColor: theme[key] && theme[key].borderColor,
            pointBorderColor: theme[key] && theme[key].borderColor,
            pointBorderWidth: 0,

            pointRadius: (theme[key] && theme[key].pointRadius) || 4,
            pointHoverRadius: (theme[key] && theme[key].pointHoverRadius) || 5,
            borderWidth: (theme[key] && theme[key].borderWidth) || 3,
            tension: (theme[key] && theme[key].lineTension) || 0.35,
            fill: false,
          };
        });
      }

      $scope._charts[canvasId] = new Chart(ctx, {
        type: "line",
        data: {
          labels: labels,
          datasets: datasets,
        },
        options: {
          responsive: true,
          maintainAspectRatio: false,
          plugins: {
            legend: {
              display:
                opts.legendDisplay !== undefined
                  ? opts.legendDisplay
                  : !isSingle,
              position: opts.legendPosition || theme.legendPosition || "bottom",
              labels: {
                usePointStyle: true,
                boxWidth: opts.legendBoxWidth != null ? opts.legendBoxWidth : 8,
              },
            },
            tooltip: {
              mode: "index",
              intersect: false,
            },
          },
          scales: {
            x: {
              type: "category",
              grid: {
                display: opts.xGridDisplay != null ? opts.xGridDisplay : false,
                /*color: opts.xGridColor || 'rgba(0,0,0,0.03)',*/
                drawBorder: opts.xDrawBorder != null ? opts.xDrawBorder : false,
              },
              ticks: {
                callback: function (value) {
                  return labels && labels[value] != null
                    ? labels[value]
                    : value;
                },
              },
            },
            y: {
              min: 0,
              max: theme.maxYScale || 100,
              ticks: {
                beginAtZero: true,
                stepSize: opts.stepSize || 20,
              },
              grid: {
                display: opts.yGridDisplay != null ? opts.yGridDisplay : false,
                //color: opts.yGridColor || (isSingle ? '#eeeeee' : 'rgba(0,0,0,0.05)'),
                drawBorder: opts.yDrawBorder != null ? opts.yDrawBorder : false,
              },
            },
          },
          elements: {
            point: {
              borderWidth: 0,
            },
          },
        },
      });

      return $scope._charts[canvasId];
    };

    $scope.createDoughnutChart = function (
      canvasId,
      labels,
      data,
      colors,
      options,
    ) {
      options = options || {};

      if ($scope._charts[canvasId]) $scope._charts[canvasId].destroy();

      var ctx = getCtx(canvasId);
      if (!ctx) return null;

      var cutoutPct = options.cutout != null ? options.cutout : 50;

      function pick(arr, i, fallback) {
        return Array.isArray(arr) && arr[i] != null ? arr[i] : fallback;
      }

      var hasHL =
        Array.isArray(options.highestArr) || Array.isArray(options.lowestArr);

      var tooltip = hasHL
        ? {
            enabled: true,
            callbacks: {
              label: function (context) {
                var i = context.dataIndex;
                var highest = pick(options.highestArr, i, "-");
                var lowest = pick(options.lowestArr, i, "-");

                return [
                  context.label,
                  "Highest: " + highest,
                  "Lowest: " + lowest,
                ];
              },
            },
          }
        : {
            enabled: true,
            callbacks: {
              label: function (context) {
                var v = Number(context.raw);
                return (
                  context.label +
                  ": " +
                  (isNaN(v) ? (context.raw ?? "-") : v.toLocaleString())
                );
              },
            },
          };

      $scope._charts[canvasId] = new Chart(ctx, {
        type: "doughnut",
        data: {
          labels: labels,
          datasets: [
            {
              data: data,
              backgroundColor: colors,
              borderWidth: options.borderWidth || 0,
              borderColor: options.borderColor || "#ffffff",
              borderRadius: options.borderRadius || 0,
            },
          ],
        },
        options: {
          responsive: true,
          maintainAspectRatio: false,
          cutout: cutoutPct + "%",
          plugins: {
            legend: {
              position: options.legendPosition || "right",
              labels: { usePointStyle: true, boxWidth: options.boxWidth || 8 },
            },
            tooltip: tooltip,
          },
        },
      });

      return $scope._charts[canvasId];
    };

    $scope.createPolarAreaChart = function (canvasId, labels, data, colors) {
      if ($scope._charts[canvasId]) {
        $scope._charts[canvasId].destroy();
      }
      var ctx = getCtx(canvasId);
      if (!ctx) return null;

      $scope._charts[canvasId] = new Chart(ctx, {
        type: "polarArea",
        data: {
          labels: labels,
          datasets: [
            {
              data: data,
              backgroundColor: colors,
            },
          ],
        },
        options: {
          responsive: true,
          maintainAspectRatio: false,
          scales: {
            r: {
              display: false,
            },
          },
          layout: { padding: { right: 16 } },
          plugins: {
            legend: {
              display: true,
              position: "right",
              labels: {
                usePointStyle: true,
                boxWidth: 10,
                padding: 12,
                generateLabels: function (chart) {
                  var ds = chart.data.datasets[0];
                  return chart.data.labels.map(function (label, i) {
                    return {
                      text:
                        label.padEnd(14, " ") +
                        ": " +
                        ds.data[i].toLocaleString(),
                      fillStyle: ds.backgroundColor[i],
                      strokeStyle: ds.backgroundColor[i],
                      hidden: false,
                      index: i,
                    };
                  });
                },
              },
            },
          },
        },
      });

      return $scope._charts[canvasId];
    };

    $scope.createHorizontalChart = function (canvasId, labels, values, colors) {
      if ($scope._charts[canvasId]) {
        $scope._charts[canvasId].destroy();
      }

      var ctx = getCtx(canvasId);
      if (!ctx) return null;

      function roundRect(ctx, x, y, w, h, r) {
        if (w <= 0 || h <= 0) return;

        if (w < 2 * r) r = w / 2;
        if (h < 2 * r) r = h / 2;

        ctx.beginPath();
        ctx.moveTo(x + r, y);
        ctx.arcTo(x + w, y, x + w, y + h, r);
        ctx.arcTo(x + w, y + h, x, y + h, r);
        ctx.arcTo(x, y + h, x, y, r);
        ctx.arcTo(x, y, x + w, y, r);
        ctx.closePath();
      }

      var roundedTrackPlugin = {
        id: "roundedTrackPlugin",
        afterDatasetsDraw: function (chart) {
          if (chart.config.type !== "bar") return;
          if (chart.options.indexAxis !== "y") return;
          if (chart.canvas && chart.canvas.id !== canvasId) return;

          var ctx2 = chart.ctx;
          var meta = chart.getDatasetMeta(0);
          var xScale = chart.scales.x;

          var area = chart.chartArea;

          meta.data.forEach(function (bar, i) {
            var value = chart.data.datasets[0].data[i];
            if (value === null || value === undefined) return;

            var props = bar.getProps(["y", "height"], true);
            var barHeight = props.height;
            var y = props.y - barHeight / 2;

            var radius = Math.min(barHeight / 2, 14);
            var inset = radius + 2;

            var left = area.left + inset;
            var right = area.right - inset;

            var fullWidth = right - left;
            var color = chart.data.datasets[0].backgroundColor[i];

            // background track
            ctx2.save();
            ctx2.globalAlpha = 0.25;
            ctx2.fillStyle = color;
            roundRect(ctx2, left, y, fullWidth, barHeight, radius);
            ctx2.fill();
            ctx2.restore();

            // filled bar
            var xValue = xScale.getPixelForValue(value);
            var width = xValue - left;
            width = Math.max(0, Math.min(width, fullWidth));
            var r2 = Math.min(radius, width / 2);

            ctx2.save();
            ctx2.globalAlpha = 1;
            ctx2.fillStyle = color;
            roundRect(ctx2, left, y, width, barHeight, r2);
            ctx2.fill();
            ctx2.restore();
          });
        },
      };

      $scope._charts[canvasId] = new Chart(ctx, {
        type: "bar",
        data: {
          labels: labels,
          datasets: [
            {
              data: values,
              backgroundColor: colors,
              borderRadius: {
                topLeft: 14,
                bottomLeft: 14,
                topRight: 14,
                bottomRight: 14,
              },
              borderSkipped: false,
            },
          ],
        },
        options: {
          indexAxis: "y",
          animation: false,
          responsive: true,
          maintainAspectRatio: false,
          layout: { padding: { left: 10, right: 10 } },
          plugins: {
            legend: {
              display: true,
              position: "bottom",
              onClick: function (e, legendItem, legend) {
                var chart = legend.chart;
                var index = legendItem.index;
                var ds = chart.data.datasets[0];

                if (!chart.$hiddenBackup) {
                  chart.$hiddenBackup = ds.data.slice();
                }

                ds.data[index] =
                  ds.data[index] === null ? chart.$hiddenBackup[index] : null;

                chart.update();
              },
              labels: {
                usePointStyle: true,
                boxWidth: 8,
                generateLabels: function (chart) {
                  var cols = chart.data.datasets[0].backgroundColor;
                  var ds = chart.data.datasets[0];

                  if (!chart.$hiddenBackup) {
                    chart.$hiddenBackup = ds.data.slice();
                  }

                  return chart.data.labels.map(function (t, i) {
                    return {
                      text: t,
                      fillStyle: cols[i],
                      strokeStyle: cols[i],
                      hidden: ds.data[i] === null,
                      index: i,
                      pointStyle: "circle",
                    };
                  });
                },
              },
            },

            tooltip: {
              enabled: true,
              callbacks: {
                title: function (context) {
                  return context[0].label;
                },
                label: function (context) {
                  return context.parsed.x;
                },
              },
            },
          },

          scales: {
            x: {
              ticks: { beginAtZero: true, max: 100, display: false },
              grid: { display: false, drawBorder: false },
            },
            y: {
              ticks: { display: false },
              grid: { display: false, drawBorder: false },
            },
          },
        },
        plugins: [roundedTrackPlugin],
      });

      return $scope._charts[canvasId];
    };

    $timeout(function () {
      $scope.LoadData();
    }, 0);

    $scope.GetExamAnalysisData = function () {
      $scope.loadingstatus = "running";
      showPleaseWait();
      $scope.DashboardData = {};
      $http({
        method: "POST",
        url: base_url + "Exam/Report/GetExamAnalysisData",
        dataType: "json",
      }).then(
        function (res) {
          hidePleaseWait();
          $scope.loadingstatus = "stop";
          if (res.data.IsSuccess && res.data.Data) {
            $scope.AnalysisData = res.data.Data;
            $timeout(function () {
              $scope.initializeCharts();
            });
          } else {
            Swal.fire(res.data.ResponseMSG);
          }
        },
        function (reason) {
          Swal.fire("Failed" + reason);
        },
      );
    };
  },
);
