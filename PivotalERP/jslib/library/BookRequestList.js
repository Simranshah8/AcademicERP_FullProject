app.controller('BookRequestListController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'BookRequestList';
    $timeout(function () {
        $('.select2').select2();
    });
    $scope.LoadData = function () {

        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.perPage = {
            BookRequestList: GlobalServices.getPerPageRow(),
            EmployeeBookRequestList: GlobalServices.getPerPageRow(),

        };

        $scope.currentPages = {
            BookRequestList: 1,
            EmployeeBookRequestList: 1,

        };

        $scope.StatusList = [
            { id: 0, text: 'All' },
            { id: 1, text: 'Pending' },
            { id: 2, text: 'Approved' },
            { id: 3, text: 'Rejected' }
        ];

        $scope.ActionList = [
            { id: 1, text: 'Approved' },
            { id: 2, text: 'Rejected' }
        ];
        $scope.RequestList = [
            { id: 1, text: 'Student' },
            { id: 2, text: 'Employee' }
        ];
        $scope.searchData = {
            BookRequestList: '',
            EmployeeBookRequestList: ''
        };

        $scope.newDet = {
            RegdNO: '',
            Status: 1,
            DateFrom_TMP: new Date(),
            DateTo_TMP: new Date(new Date().setDate(new Date().getDate() + 7)),
            Remarks: '',
            RequestFor: 1
        };


    };

    $scope.FilteredStatusList = function (item) {
        if (!$scope.newDet.Status || $scope.newDet.Status === 0) return true; // 0 means 'All' or no filter
        return item.Status === $scope.newDet.Status;  // direct integer comparison
    };

    $scope.GetBookRequestListById = function (refData) {
        let para = {
            StudentId: ($scope.newDet.RequestFor == 1) ? refData.StudentId : null,
            EmployeeId: ($scope.newDet.RequestFor == 2) ? refData.EmployeeId : null,
            RequestFor: $scope.newDet.RequestFor
        };


        $http({
            method: 'POST',
            url: base_url + "Library/Master/GetBookRequestListById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.selectedEmployee = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (err) {
            Swal.fire("Error: " + err.statusText);
        });
    };





    $scope.ShowEmpApprovedDialog = function (emp) {
        $scope.selectedEmployee = emp;
        $scope.GetBookRequestListById({
            StudentId: null,
            EmployeeId: emp.EmployeeId,
            RequestFor: 2
        });
        $timeout(function () {
            $('#EmpAction').modal('show');
        });
    };


    $scope.ShowStdApprovedDialog = function (student) {
        $scope.selectedEmployee = student
        $scope.GetBookRequestListById({
            StudentId: student.StudentId,
            EmployeeId: null,
            RequestFor: 1
        });
        $('#StdAction').modal('show');
    };



    $scope.ShowStdRequestedBook = function () {
        $timeout(function () {
            $('#StdBookrequest').modal('show');
        });
    };
    $scope.ShowEmpRequestedBook = function () {
        $timeout(function () {
            $('#EmpBookrequest').modal('show');
        });
    };



    $scope.UpdateAction = function () {
        if (!$scope.selectedStudent) {
            return Swal.fire("Error", "No student selected", "error");
        }

        var student = $scope.BookRequestList.find(function (st) {
            return st.RegdNo === $scope.selectedStudent.RegdNo;
        });

        if (!student) {
            return Swal.fire("Error", "Student not found in BookRequestList", "error");
        }

        var statusText = '';
        if ($scope.newDet.StdAction == 1) {
            statusText = "Approved";
        } else if ($scope.newDet.StdAction == 2) {
            statusText = "Rejected";
        }

        student.Status = statusText;
        student.Remarks = $scope.newDet.Remarks || '';
        student.VerificationBy = $scope.LoggedUser || 'Unknown';
        student.VerificationDate = new Date();

        $('#StdAction').modal('hide');
        Swal.fire("Success", "Book Request Updated", "success");
    };




    $scope.GetBookRequestList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        const params = {
            StudentId: null,
            EmployeeId: null,
            RequestFor: $scope.newDet.RequestFor
        };
        $scope.BookRequestList = [];


        $http({
            method: 'POST',
            url: base_url + 'Library/Master/GetBookRequestList',
            data: JSON.stringify(params),
            dataType: 'json'
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.IsSuccess && res.data.Data) {
                if ($scope.newDet.RequestFor === 1) {
                    // For students: filter out any invalid student-related fields
                    $scope.BookRequestList = res.data.Data.filter(function (item) {
                        return item.StudentName && item.StudentName.trim() !== '' &&
                            item.RegdNo && item.RegdNo.trim() !== '';
                    });
                } else if ($scope.newDet.RequestFor === 2) {
                    // For employees: filter out any invalid employee-related fields
                    $scope.BookRequestList = res.data.Data.filter(function (item) {
                        return item.EmployeeName && item.EmployeeName.trim() !== '' &&
                            item.RegdNo && item.RegdNo.trim() !== '';
                    });
                }
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (error) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            Swal.fire('Failed to load data: ' + error.statusText);
        });
    };

    $scope.$watch('newDet.RequestFor', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            $scope.GetBookRequestList();
        }
    });




});
