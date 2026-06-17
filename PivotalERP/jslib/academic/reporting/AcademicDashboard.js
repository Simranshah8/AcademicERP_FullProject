app.controller('ADashboardController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Academic Dashboard';

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

        $scope.ClassShiftList = [];
        $http({
            method: 'POST',
            url: base_url + "Academic/Creation/GetAllClassShift",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                // Filter only active shifts
                $scope.ClassShiftList = res.data.Data.filter(x => x.IsActive === true);

                var now = new Date();
                var currentHour = now.getHours();
                var defaultClassShiftId = null;

                if ($scope.ClassShiftList.length === 1) {
                    defaultClassShiftId = $scope.ClassShiftList[0].ClassShiftId;
                } else if ($scope.ClassShiftList.length > 1) {
                    var matchedShift = null;

                    // First: check if current time is between any shift's start and end
                    for (var i = 0; i < $scope.ClassShiftList.length; i++) {
                        var shift = $scope.ClassShiftList[i];
                        var start = new Date(shift.StartTime);
                        var end = new Date(shift.EndTime);

                        if (currentHour >= start.getHours() && currentHour < end.getHours()) {
                            matchedShift = shift;
                            break;
                        }
                    }
                    if (matchedShift) {
                        defaultClassShiftId = matchedShift.ClassShiftId;
                    } else {
                        if (currentHour < 12) {
                            // AM: Find shift with closest upcoming StartTime
                            let minDiff = Infinity;
                            $scope.ClassShiftList.forEach(function (shift) {
                                let start = new Date(shift.StartTime);
                                let diff = start.getHours() - currentHour;
                                if (diff >= 0 && diff < minDiff) {
                                    minDiff = diff;
                                    defaultClassShiftId = shift.ClassShiftId;
                                }
                            });
                        } else {
                            // PM: Find shift with latest EndTime before now
                            let maxHour = -1;
                            $scope.ClassShiftList.forEach(function (shift) {
                                let end = new Date(shift.EndTime);
                                if (end.getHours() <= currentHour && end.getHours() > maxHour) {
                                    maxHour = end.getHours();
                                    defaultClassShiftId = shift.ClassShiftId;
                                }
                            });
                        }
                        // Still nothing? Fallback to earliest StartTime logic
                        if (!defaultClassShiftId) {
                            var fallbackShift = $scope.ClassShiftList.reduce((earliest, current) => {
                                var earliestStart = new Date(earliest.StartTime);
                                var currentStart = new Date(current.StartTime);

                                if (currentStart < earliestStart) return current;
                                if (currentStart.getTime() === earliestStart.getTime()) {
                                    var earliestEnd = new Date(earliest.EndTime);
                                    var currentEnd = new Date(current.EndTime);
                                    return currentEnd < earliestEnd ? current : earliest;
                                }
                                return earliest;
                            });
                            defaultClassShiftId = fallbackShift.ClassShiftId;
                        }
                    }
                }
                $scope.newFilter.ClassShiftId = defaultClassShiftId;
                $scope.GetAcademicDashboard($scope.newFilter.ClassShiftId);
            }
        }, function (reason) {
            Swal.fire('Failed: ' + reason);
        });



        $scope.newFilter = {
            ClassShiftId: null
        };


        //$scope.GetAcademicDashboard();

    }

  


    $scope.GetAcademicDashboard = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.beData = {};
        $scope.ClassHeaderList = [];
        $scope.ClassScheduleMatrix = []; 
        $scope.ClassTeacherList = [];
        $scope.HODList = [];
        var para = {
            ClassShiftId: $scope.newFilter.ClassShiftId,
            BranchId: null,
        };
        $http({
            method: 'POST',
            url: base_url + "Academic/Report/GetAcademicDashboard",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.beData = res.data.Data;

                $scope.DataColl = res.data.Data.ClassScheduleColl || [];
                $scope.HODColl = mx(res.data.Data.HODColl);

                $scope.studentAges = [];
                angular.forEach($scope.beData.StudentBirthdaysColl, function (student) {
                    if (student.DOB_AD) {
                        let dob = new Date(student.DOB_AD); // Convert DOB_AD to Date object
                        let today = new Date();
                        let ageYears = today.getFullYear() - dob.getFullYear();
                        let ageMonths = today.getMonth() - dob.getMonth();
                        let ageDays = today.getDate() - dob.getDate();

                        // Adjust for negative months or days
                        if (ageDays < 0) {
                            ageMonths--;
                            let previousMonth = new Date(today.getFullYear(), today.getMonth(), 0);
                            ageDays += previousMonth.getDate();
                        }
                        if (ageMonths < 0) {
                            ageYears--;
                            ageMonths += 12;
                        }
                        // Add the calculated age to each student object in StudentBirthdaysColl
                        student.Age = `${ageYears} Years`;
                        // Optionally, push the age details into the studentAges array
                        $scope.studentAges.push({
                            Age: student.Age
                        });
                    }
                    if (!student.StudentBdayPhoto || student.StudentBdayPhoto=='') {
                        student.StudentBdayPhoto = "/wwwroot/dynamic/images/avatar-img.png"
                    }
                });

                $scope.employeeAge = [];
                angular.forEach($scope.beData.EmployeeBirthdaysColl, function (employee) {
                    if (employee.DOB_AD) {
                        let dob = new Date(employee.DOB_AD); // Convert DOB_AD to Date object
                        let today = new Date();
                        let ageYears = today.getFullYear() - dob.getFullYear();
                        let ageMonths = today.getMonth() - dob.getMonth();
                        let ageDays = today.getDate() - dob.getDate();

                        // Adjust for negative months or days
                        if (ageDays < 0) {
                            ageMonths--;
                            let previousMonth = new Date(today.getFullYear(), today.getMonth(), 0);
                            ageDays += previousMonth.getDate();
                        }
                        if (ageMonths < 0) {
                            ageYears--;
                            ageMonths += 12;
                        }
                        // Add the calculated age to each student object in StudentBirthdaysColl
                        employee.Age = `${ageYears} Years`;
                        // Optionally, push the age details into the studentAges array
                        $scope.employeeAge.push({
                            Age: employee.Age
                        });
                    }
                    if (!employee.EmpBdayPhoto || employee.EmpBdayPhoto == '') {
                        employee.EmpBdayPhoto = "/wwwroot/dynamic/images/avatar-img.png"
                    }
                });

                //Class Schedule
                // Step 1: Group data by StartTime and prepare ClassHeaderList
                const groupedData = $scope.DataColl.reduce((acc, curr) => {
                    const formatTime = (timeStr) => {
                        const date = new Date(`1970-01-01T${timeStr}`);
                        let hours = date.getHours();
                        const minutes = date.getMinutes().toString().padStart(2, '0');
                        const ampm = hours >= 12 ? 'PM' : 'AM';
                        hours = hours % 12;
                        hours = hours ? hours : 12; // the hour '0' should be '12'
                        return `${hours.toString().padStart(2, '0')}:${minutes} ${ampm}`;
                    };
                    const startTime = formatTime(curr.StartTime);
                    const endTime = formatTime(curr.EndTime);
                    const timeSlot = `${startTime}-${endTime}`;
                    if (!acc[timeSlot]) {
                        acc[timeSlot] = [];
                        if (!$scope.ClassHeaderList.includes(timeSlot)) {
                            $scope.ClassHeaderList.push(timeSlot);
                        }
                    }
                    acc[timeSlot].push(curr);
                    return acc;
                }, {});

                // Step 2: Prepare ClassScheduleMatrix (row-wise structure for classes)
                const classNames = [...new Set($scope.DataColl.map((item) => item.ClassName))];
                $scope.ClassScheduleMatrix = classNames.map((className) => {
                    const row = { ClassName: className, TimeSlots: [] };
                    $scope.ClassHeaderList.forEach((header) => {
                        const matchingClass = groupedData[header]?.find(
                            (item) => item.ClassName === className
                        );
                        row.TimeSlots.push(matchingClass || null); // Add null if no match
                    });
                    return row;
                });


                var query = $scope.HODColl.groupBy(t => ({ HODEmployeeId: t.HODEmployeeId }));
    			angular.forEach(query, function (q) {
                    var fst = q.elements[0];
                    var classNames = q.elements.map(function (item) { return item.SectionName_HOD? item.ClassName_HOD + '-' + item.SectionName_HOD  : item.ClassName_HOD; }).join(', ');
                    //var classNames = q.elements.map(function (item) { return item.ClassName_HOD + '-' + item.SectionName_HOD; }).join(', ');
    				var beData = {
                        HODId: fst.HODId,
                        HODEmployeeId: fst.HODEmployeeId,
                        HODCode: fst.HODCode,
                        HODName: fst.HODName,
                        Designation_HOD: fst.Designation_HOD,
                        PhotoPath_HOD: fst.PhotoPath_HOD,
                        Gender_HOD: fst.Gender_HOD,
                        ContactNo_HOD: fst.ContactNo_HOD,
                        Address_HOD: fst.Address_HOD,
                        DepartmentId: fst.DepartmentId,
                        DepartmentHOD: fst.DepartmentHOD,
                        ClassShift: fst.ClassShift,
                        ClassName_HOD: classNames
    				};
    				$scope.HODList.push(beData);
    			});                

                //Pie chart
                $timeout(function () {
                    $scope.studentOverview();
                    $scope.employeeOverview();
                    $scope.subjectOverview();
                    $scope.studentAccount();
                    $scope.employeeAccount();
                    $scope.certificatesSummary();
                    $scope.PTMDetails();
                    $scope.studentPhoto();
                    $scope.employeePhoto();
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


    //$scope.GetAcademicDashboard = function () {
    //       $scope.loadingstatus = "running";
    //       showPleaseWait();
    //       $scope.beData = {};
    //	$scope.ClassScheduleList = [];
    //	var para = {
    //		ClassShiftId: $scope.newFilter.ClassShiftId,
    //		BranchId: null,
    //       };
    //       $http({
    //		method: 'POST',
    //		url: base_url + "Academic/Report/GetAcademicDashboard",
    //           dataType: "json",
    //           data: JSON.stringify(para)
    //       }).then(function (res) {
    //           hidePleaseWait();
    //           $scope.loadingstatus = "stop";
    //           if (res.data.IsSuccess && res.data.Data) {
    //			$scope.beData = res.data.Data;

    //			$scope.DataColl = mx(res.data.Data.ClassScheduleColl);

    //			var query = $scope.DataColl.groupBy(t => ({ ClassId: t.ClassId }));
    //			angular.forEach(query, function (q) {
    //				var fst = q.elements[0];
    //				var beData = {
    //					ClassId: fst.ClassId,
    //					ClassName: fst.ClassName,
    //					ShiftStartTime: fst.ShiftStartTime,
    //					ShiftEndTime: fst.ShiftEndTime,
    //					StartTime: fst.StartTime,
    //					EndTime: fst.EndTime,
    //					SubjectName: fst.SubjectName,
    //					TeacherName: fst.TeacherName
    //				};

    //				$scope.ClassScheduleList.push(beData);
    //			});


    //           } else {
    //               Swal.fire(res.data.ResponseMSG);
    //           }
    //       }, function (reason) {
    //           hidePleaseWait();
    //           $scope.loadingstatus = "stop";
    //           Swal.fire('Failed: ' + reason.statusText);
    //       });
    //   };
    $scope.studentOverview = function () {
        // Get the canvas element for students
        var canvas = document.getElementById("pieChart");
        if (!canvas) {
            console.error("Canvas element with id 'pieChart' not found.");
            return;
        }
        var ctx = canvas.getContext('2d');

        // Data for the student pie chart
        var data = {
            labels: ["Boys", "Girls"],
            datasets: [{
                data: [$scope.beData?.TotalMaleStudent || 0, $scope.beData?.TotalFemaleStudent || 0],
                backgroundColor: ['#C96D61', '#8BC56B'],
                borderColor: ['#ffffff', '#ffffff'],
                borderWidth: 1
            }]
        };

        // Options for the student pie chart
        var options = {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            const label = tooltipItem.label || '';
                            const value = tooltipItem.raw || 0;
                            return `${label}: ${value}`;
                        }
                    }
                }
            }
        };

        // Destroy the existing student chart instance if it exists
        if (window.studentPieChart) {
            window.studentPieChart.destroy();
        }

        // Create a new chart for students
        window.studentPieChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: options
        });
    };

    $scope.employeeOverview = function () {
        // Get the canvas element for employees
        var canvas = document.getElementById("pieChart2");
        if (!canvas) {
            console.error("Canvas element with id 'pieChart2' not found.");
            return;
        }
        var ctx = canvas.getContext('2d');

        // Data for the employee pie chart
        var data = {
            labels: ["Men", "Women"],
            datasets: [{
                data: [$scope.beData?.TotalMale || 0, $scope.beData?.TotalFemale || 0],
                backgroundColor: ['#C96D61', '#8BC56B'],
                borderColor: ['#ffffff', '#ffffff'],
                borderWidth: 1
            }]
        };

        // Options for the employee pie chart
        var options = {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            const label = tooltipItem.label || '';
                            const value = tooltipItem.raw || 0;
                            return `${label}: ${value}`;
                        }
                    }
                }
            }
        };

        // Destroy the existing employee chart instance if it exists
        if (window.employeePieChart) {
            window.employeePieChart.destroy();
        }

        // Create a new chart for employees
        window.employeePieChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: options
        });
    };
    $scope.subjectOverview = function () {
        // Get the canvas element for employees
        var canvas = document.getElementById("pieChart3");
        if (!canvas) {
            console.error("Canvas element with id 'pieChart3' not found.");
            return;
        }
        var ctx = canvas.getContext('2d');

        // Data for the employee pie chart
        var data = {
            labels: ["Men", "Women"],
            datasets: [{
                data: [$scope.beData?.TotalECA || 0, $scope.beData?.TotalNonECA || 0],
                backgroundColor: ['#C96D61', '#8BC56B'],
                borderColor: ['#ffffff', '#ffffff'],
                borderWidth: 1
            }]
        };

        // Options for the employee pie chart
        var options = {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            const label = tooltipItem.label || '';
                            const value = tooltipItem.raw || 0;
                            return `${label}: ${value}`;
                        }
                    }
                }
            }
        };

        // Destroy the existing employee chart instance if it exists
        if (window.subjectPieChart) {
            window.subjectPieChart.destroy();
        }

        // Create a new chart for employees
        window.subjectPieChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: options
        });
    };

    $scope.studentAccount = function () {
        // Get the canvas element for employees
        var canvas = document.getElementById("pieChart4");
        if (!canvas) {
            console.error("Canvas element with id 'pieChart3' not found.");
            return;
        }
        var ctx = canvas.getContext('2d');

        // Data for the employee pie chart
        var data = {
            labels: ["Created", "Remaining"],
            datasets: [{
                data: [$scope.beData?.Created || 0, $scope.beData?.Remaining || 0],
                backgroundColor: ['#8BC56B', '#C96D61'],
                borderColor: ['#ffffff', '#ffffff'],
                borderWidth: 1
            }]
        };

        // Options for the employee pie chart
        var options = {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            const label = tooltipItem.label || '';
                            const value = tooltipItem.raw || 0;
                            return `${label}: ${value}`;
                        }
                    }
                }
            }
        };

        // Destroy the existing employee chart instance if it exists
        if (window.studentAccPieChart) {
            window.studentAccPieChart.destroy();
        }

        // Create a new chart for employees
        window.studentAccPieChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: options
        });
    };

    $scope.employeeAccount = function () {
        // Get the canvas element for employees
        var canvas = document.getElementById("pieChart5");
        if (!canvas) {
            console.error("Canvas element with id 'pieChart4' not found.");
            return;
        }
        var ctx = canvas.getContext('2d');

        // Data for the employee pie chart
        var data = {
            labels: ["Created", "Remaining"],
            datasets: [{
                data: [$scope.beData?.CreatedEmp || 0, $scope.beData?.RemainingEmp || 0],
                backgroundColor: ['#8BC56B', '#C96D61'],
                borderColor: ['#ffffff', '#ffffff'],
                borderWidth: 1
            }]
        };

        // Options for the employee pie chart
        var options = {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            const label = tooltipItem.label || '';
                            const value = tooltipItem.raw || 0;
                            return `${label}: ${value}`;
                        }
                    }
                }
            }
        };

        // Destroy the existing employee chart instance if it exists
        if (window.employeeAccPieChart) {
            window.employeeAccPieChart.destroy();
        }

        // Create a new chart for employees
        window.employeeAccPieChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: options
        });
    };

    $scope.certificatesSummary = function () {
        // Get the canvas element for certificates
        var canvas = document.getElementById("pieChart6");
        var ctx = canvas.getContext('2d');

        // Data for the certificates pie chart
        var data = {
            labels: ["Transfer Certificate", "Character Certificate", "Extra Certificate"],
            datasets: [{
                data: [
                    $scope.beData?.TransferCertificate || 0,
                    $scope.beData?.CharacterCertificate || 0,
                    $scope.beData?.ExtraCertificate || 0
                ],
                backgroundColor: ['#C96D61', '#8BC56B', '#3CC3DF'],
                borderColor: ['#ffffff', '#ffffff', '#ffffff'],
                borderWidth: 1
            }]
        };

        // Options for the certificates pie chart
        var options = {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            const label = tooltipItem.label || '';
                            const value = tooltipItem.raw || 0;
                            return `${label}: ${value}`;
                        }
                    }
                }
            }
        };

        // Destroy the existing certificates chart instance if it exists
        if (window.certificatesPieChart) {
            window.certificatesPieChart.destroy();
        }

        // Create a new chart for certificates
        window.certificatesPieChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: options
        });
    };

    $scope.PTMDetails = function () {
        // Get the canvas element for certificates
        var canvas = document.getElementById("pieChart7");
        var ctx = canvas.getContext('2d');

        // Data for the certificates pie chart
        var data = {
            labels: ["Yearly", "Monthly"],
            datasets: [{
                data: [
                    $scope.beData?.TotalYearlyPTM || 0,
                    $scope.beData?.TotalMonthlyPTM || 0
                ],
                backgroundColor: ['#C96D61', '#8BC56B'],
                borderColor: ['#ffffff', '#ffffff'],
                borderWidth: 1
            }]
        };

        // Options for the certificates pie chart
        var options = {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            const label = tooltipItem.label || '';
                            const value = tooltipItem.raw || 0;
                            return `${label}: ${value}`;
                        }
                    }
                }
            }
        };

        // Destroy the existing certificates chart instance if it exists
        if (window.PTMDetailsPieChart) {
            window.PTMDetailsPieChart.destroy();
        }

        // Create a new chart for certificates
        window.PTMDetailsPieChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: options
        });
    };


    
    $scope.studentPhoto = function () {
        // Get the canvas element for certificates
        var canvas = document.getElementById("pieChart8");
        var ctx = canvas.getContext('2d');

        // Data for the certificates pie chart
        var data = {
            labels: ["Photo Updated ", "Remaining"],
            datasets: [{
                data: [
                    $scope.beData?.PhotoUpdated || 0,
                    $scope.beData?.Remaining || 0
                ],
                backgroundColor: [ '#8BC56B', '#C96D61'],
                borderColor: ['#ffffff', '#ffffff'],
                borderWidth: 1
            }]
        };

        // Options for the certificates pie chart
        var options = {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            const label = tooltipItem.label || '';
                            const value = tooltipItem.raw || 0;
                            return `${label}: ${value}`;
                        }
                    }
                }
            }
        };

        // Destroy the existing certificates chart instance if it exists
        if (window.studentPhotoPieChart) {
            window.studentPhotoPieChart.destroy();
        }

        // Create a new chart for certificates
        window.studentPhotoPieChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: options
        });
    };
    
    $scope.employeePhoto = function () {
        // Get the canvas element for certificates
        var canvas = document.getElementById("pieChart9");
        var ctx = canvas.getContext('2d');

        // Data for the certificates pie chart
        var data = {
            labels: ["Photo Updated ", "Remaining"],
            datasets: [{
                data: [
                    $scope.beData?.PhotoUpdatedEmp || 0,
                    $scope.beData?.EmpRemaining || 0
                ],
                backgroundColor: [ '#8BC56B', '#C96D61'],
                borderColor: ['#ffffff', '#ffffff'],
                borderWidth: 1
            }]
        };

        // Options for the certificates pie chart
        var options = {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            const label = tooltipItem.label || '';
                            const value = tooltipItem.raw || 0;
                            return `${label}: ${value}`;
                        }
                    }
                }
            }
        };

        // Destroy the existing certificates chart instance if it exists
        if (window.employeePhotoPieChart) {
            window.employeePhotoPieChart.destroy();
        }

        // Create a new chart for certificates
        window.studentPhotoPieChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: options
        });
    };



    $scope.ShowDocPdf = function (item) {
        $scope.viewImg1 = {
            ContentPath: '',
            FileType: null
        };

        if (item.DocPath && item.DocPath.length > 0) {
            $scope.viewImg1.ContentPath = item.DocPath;
            $scope.viewImg1.FileType = 'pdf';  // Assuming DocPath is for PDFs
            document.getElementById('pdfViewer1').src = item.DocPath;
            $('#DocView').modal('show');
        } else if (item.PhotoPath && item.PhotoPath.length > 0) {
            $scope.viewImg1.ContentPath = item.PhotoPath;
            $scope.viewImg1.FileType = 'image';  // Assuming PhotoPath is for images
            $('#DocView').modal('show');
        } else if (item.File) {
            var blob = new Blob([item.File], { type: item.File?.type });
            $scope.viewImg1.ContentPath = URL.createObjectURL(blob);
            $scope.viewImg1.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

            if ($scope.viewImg1.FileType === 'pdf') {
                document.getElementById('pdfViewer1').src = $scope.viewImg1.ContentPath;
            }

            $('#DocView').modal('show');
        } else {
            Swal.fire('No Image Found');
        }
    };

    $scope.ShowDocPdfEMP = function (item) {
        $scope.viewImg = {
            ContentPath: '',
            FileType: null
        };

        if (item.DocPath && item.DocPath.length > 0) {
            $scope.viewImg.ContentPath = item.DocPath;
            $scope.viewImg.FileType = 'pdf';  // Assuming DocPath is for PDFs
            document.getElementById('pdfViewer').src = item.DocPath;
            $('#DocViewEMP').modal('show');
        } else if (item.PhotoPath && item.PhotoPath.length > 0) {
            $scope.viewImg.ContentPath = item.PhotoPath;
            $scope.viewImg.FileType = 'image';  // Assuming PhotoPath is for images
            $('#DocViewEMP').modal('show');
        } else if (item.File) {
            var blob = new Blob([item.File], { type: item.File?.type });
            $scope.viewImg.ContentPath = URL.createObjectURL(blob);
            $scope.viewImg.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';

            if ($scope.viewImg.FileType === 'pdf') {
                document.getElementById('pdfViewer').src = $scope.viewImg.ContentPath;
            }

            $('#DocViewEMP').modal('show');
        } else {
            Swal.fire('No Image Found');
        }
    };


    $scope.pageChangeHandler = function (num) {
        console.log('meals page changed to ' + num);
    };

});