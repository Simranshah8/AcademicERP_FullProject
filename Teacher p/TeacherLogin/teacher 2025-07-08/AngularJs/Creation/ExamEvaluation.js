app.controller('ExamEvaluationController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Exam Evaluation';

    $scope.LoadData = function () {
        $('.select2').select2();
        

        $scope.searchData = {
            Printsearch: ''
        };


        $scope.ClassColl = [];
        $scope.SectionColl = [];
        $http.get(base_url + "StudentAttendance/Creation/GetClassSection")
            .then(function (data) {
                $scope.ClassColl = data.data.ClassList;
                $scope.SectionClassColl = data.data.SectionList;
            }, function (reason) {
                exDialog.openMessage({
                    scope: $scope,
                    title: $scope.Title,
                    icon: "info",
                    message: 'Failed: ' + reason
                });
            });


        $scope.ExamTypeColl = [];
        $http.get(base_url + "OnlineExam/Creation/GetExamTypeList")
            .then(function (data) {
                $scope.ExamTypeList = data.data;
            }, function (reason) {
                alert("Data not get");
            });
    }

    $scope.GetStudentColl = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.StudentColl = [];
        var para = {
            ClassId: $scope.newDet.SelectedClass.ClassId,
            SectionId: $scope.newDet.SelectedClass.SectionId,

        };
        $http({
            method: 'POST',
            url: base_url + "StudentRecord/Creation/GetClassWiseStudentList",
            data: $scope.para,
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data && res.data) {
                $scope.StudentColl = res.data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    }

    $scope.Print = function () {
        // Convert canvas elements to images before PDF generation
        convertCanvasToImage();

        // Select the element to print (in this case, #printcard)
        var element = document.getElementById('printcard');

        var titleElement = document.getElementById('title');
        if (titleElement) {
            titleElement.textContent = 'Evaluation'; // Set title to "Evaluation"
            titleElement.style.display = 'none'; // Hide the title
        }
        // Show the element temporarily to capture its content for printing
        element.style.display = 'block';

        // Use a timeout to ensure the title is hidden before generating the PDF
        setTimeout(() => {
            // Generate the PDF
            html2pdf()
                .set({
                    margin: 2, // Equal margins (top, left, bottom, right)
                    filename: 'evaluation.pdf',
                    image: { type: 'jpeg', quality: 1 },
                    html2canvas: { scale: 2, logging: true, letterRendering: true },
                    jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }
                })
                .from(element)
                .toPdf()
                .get('pdf')
                .then(function (pdf) {
                    // Create a blob URL from the PDF output
                    var blob = pdf.output('blob');
                    var url = URL.createObjectURL(blob);

                    // Open the PDF in a new tab
                    window.open(url, '_blank');
                })
                .finally(() => {
                    // Hide the element again after the PDF generation
                    //element.style.display = 'none';
                    //if (titleElement) {
                    //    titleElement.style.display = 'block'; // Show the title back
                    //}
                    /* document.getElementById('infocard').style.display = "none";*/
                });
        }, 100); // Delay in milliseconds
    };

    $scope.PrintForm = function () {
        // Convert canvas elements to images before printing
        convertCanvasToImage();

        // Trigger the print with printThis
        $('#printcard').printThis();
    };

    function convertCanvasToImage() {
        const printCards = document.querySelectorAll('#printcard .chart-container');
        printCards.forEach((card, index) => {
            const canvas = card.querySelector(`#barChart${index}`);
            if (canvas) {
                const img = document.createElement('img');
                img.src = canvas.toDataURL("image/png");
                img.style.height = canvas.style.height;
                img.style.width = canvas.style.width;
                canvas.replaceWith(img);
            }
        });
    }

    $scope.GetStudentEval = function () {
        $scope.PrintList = [];
        $scope.ExamColl = [];
        $scope.ExamCollName = '';
        $scope.newDet.TotalPoint = 0;
        if ($scope.newDet.SelectedClass && $scope.newDet.ExamTypeId) {
            var para = {
                ClassId: $scope.newDet.SelectedClass.ClassId,
                SectionId: $scope.newDet.SelectedClass.SectionId,
                ExamTypeId: $scope.newDet.ExamTypeId,
                ExamTypeIdColl: ($scope.newDet.SelectExamTypeId ? $scope.newDet.SelectExamTypeId.toString() : ''),
                StudentIdColl: ($scope.newDet.StudentId ? $scope.newDet.StudentId.toString() : '')
            };
            $scope.loadingstatus = "running";
            showPleaseWait();
            $http({
                method: 'POST',
                url: base_url + "Examination/Reporting/GetStudentEvaluation",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data) {

                    if ($scope.newDet.SelectExamTypeId) {
                        $scope.newDet.SelectExamTypeId.forEach(function (et) {
                            var findExam = mx($scope.ExamTypeList).firstOrDefault(p1 => p1.ExamTypeId == et);

                            if ($scope.ExamCollName && $scope.ExamCollName.length > 0) {
                                $scope.ExamCollName = $scope.ExamCollName + " , ";
                            }
                            $scope.ExamCollName = $scope.ExamCollName + findExam.DisplayName;
                            $scope.ExamColl.push({
                                ExamTypeId: findExam.ExamTypeId,
                                label: findExam.DisplayName,
                                borderWidth: 1,
                                borderRadius: 3,
                                backgroundColor: generateRandomColor(),
                                data: [],
                            })
                        });
                    }

                    res.data.forEach(function (item) {
                        var itemTotalPoints = 0;
                        if (item.AchievementColl && Array.isArray(item.AchievementColl)) {
                            item.AchievementColl.forEach(function (achievement) {
                                if (achievement.Point) {
                                    itemTotalPoints += achievement.Point;
                                }
                            });
                        }
                        item.Point = itemTotalPoints;
                    });

                    $scope.PrintList = res.data;
                    $timeout(function () {
                        var ind = 0;
                        $scope.loadingstatus = "running";
                        showPleaseWait();
                        $scope.PrintList.forEach(function (item) {
                            item.Index = ind;
                            if (item.EvaluationBarColl && Array.isArray(item.EvaluationBarColl)) {
                                $scope.EvaluationBarGraph(item);
                            }
                            ind++;
                        });

                        $scope.loadingstatus = "stop";
                        hidePleaseWait();

                    });

                    //Added By Suresh
                    $scope.showPrintCard = true;

                    //Ends
                } else {
                    Swal.fire(res.data.ResponseMSG);
                }

            }, function (reason) {
                Swal.fire('Failed ' + reason);
            });

        }

    }

    function generateRandomColor() {
        let maxVal = 0xFFFFFF; // 16777215
        let randomNumber = Math.random() * maxVal;
        randomNumber = Math.floor(randomNumber);
        randomNumber = randomNumber.toString(16);
        let randColor = randomNumber.padStart(6, 0);
        return `#${randColor.toUpperCase()}`
    }
    $scope.ExamColl = [];
    $scope.EvaluationBarGraph = function (curRow) {

        $scope.loadingstatus = "running";
        showPleaseWait();

        var lbls = [];


        var subQry = mx(curRow.EvaluationBarColl).groupBy(p1 => p1.SubjectId);
        angular.forEach(subQry, function (sub) {
            var fst = sub.elements[0];
            lbls.push(fst.SubjectName);

            sub.elements.forEach(function (el) {
                var findExam = mx($scope.ExamColl).firstOrDefault(p1 => p1.ExamTypeId == el.ExamTypeId);
                findExam.data.push(el.Obtain);
            });

        });

        var canvasName = 'barChart' + curRow.Index;
        var canvas = document.getElementById(canvasName);
        if (canvas == null) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            return;
        }

        var ctx = canvas.getContext('2d');
        //ctx.canvas.width = 1400;
        //ctx.canvas.height = 300;
        var data = {
            labels: lbls,
            datasets: $scope.ExamColl,
        };

        var options = {
            responsive: false,
            scales: {
                x: {
                    grid: {
                        display: false,
                    },
                    ticks: {
                        font: {
                            size: 16,
                            weight: 'bold'
                        },
                        color: 'black'
                    }
                },
                y: {
                    ticks: {
                        font: {
                            size: 16,
                            weight: 'bold'
                        },
                        color: 'black'
                    }
                }

            },

            plugins: {
                layout: {
                    padding: {
                        left: 20,
                        right: 0,
                        top: 0,
                        bottom: 0,
                    },
                },
                datalabels: {
                    display: true,
                    color: 'black',
                    align: 'top',
                    anchor: 'start',
                    font: {
                        size: 14,
                        weight: 'bold'
                    }
                },
                // Configure ToolTips
                tooltips: {
                    enabled: true, // Enable/Disable ToolTip By Default Its True
                    backgroundColor: "red", // Set Tooltip Background Color
                    titleFontFamily: "Comic Sans MS", // Set Tooltip Title Font Family
                    titleFontSize: 30, // Set Tooltip Font Size
                    titleFontStyle: "bold italic",
                    titleFontColor: "yellow",
                    titleAlign: "center",
                    titleSpacing: 3,
                    titleMarginBottom: 50,
                    bodyFontFamily: "Comic Sans MS",
                    bodyFontSize: 20,
                    bodyFontStyle: "italic",
                    bodyFontColor: "black",
                    bodyAlign: "center",
                    bodySpacing: 3,
                },
                // Custom Chart Title
                title: {
                    display: true,
                    position: "bottom",
                    fontSize: 25,
                    fontFamily: "Comic Sans MS",
                    fontColor: "red",
                    fontStyle: "bold italic",
                    padding: 20,
                    lineHeight: 5,
                },
                // Legends Configuration
                legend: {
                    /* display: false,*/
                    position: "top",
                    align: "center",
                    labels: {
                        fontColor: "red",
                        boxWidth: 20,
                        usePointStyle: true,
                        font: {
                            size: 30
                        }
                    },
                },
            },
            legend: {
                display: false,
            }

        };

        var myBarChart = new Chart(ctx, {
            type: 'bar',
            data: data,
            options: options
        });

        $scope.loadingstatus = "stop";
        hidePleaseWait();
    };


});