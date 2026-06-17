app.controller('CalendarController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Calendar';
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();

        $scope.SelectionList = [{ id: 1, text: 'Week' }, { id: 2, text: 'Month' }, { id: 3, text: 'Year' }];
        $scope.RemainderOptionList = [{ id: 1, text: 'Days' }, { id: 2, text: 'Week' }];
       

        $scope.searchData = {
            Calendar: '',
        };

        $scope.newCalendar = {
            CalendarId: null,
            SelectionId: 1,
            Mode: 'Send'
        };


        $scope.newTask = {
            ReminderId: null,
            Reminderdiv: [],
            RemainderoptionId:1,
            Mode: 'Send'
        };
        $scope.newTask.Reminderdiv.push({});
        /*$scope.GetAllCalendarList();*/
    };


    $scope.ClearCalendar = function () {
        $scope.ClearPhoto();
        $scope.newCalendar = {
            CalendarId: null,

            Mode: 'Send'
        };

    };



    $scope.AddReminder = function (ind) {
        if ($scope.newTask.Reminderdiv) {
            if ($scope.newTask.Reminderdiv.length > ind + 1) {
                $scope.newTask.Reminderdiv.splice(ind + 1, 0, {
                    Remindcount: ''
                })
            } else {
                $scope.newTask.Reminderdiv.push({
                    Remindcount: ''
                })
            }
        }
    };
    $scope.delReminder = function (ind) {
        if ($scope.newTask.Reminderdiv) {
            if ($scope.newTask.Reminderdiv.length > 1) {
                $scope.newTask.Reminderdiv.splice(ind, 1);
            }
        }
    };



    $scope.SaveUpdateTask = function () {
        if ($scope.IsValidTask() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newTask.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateTask();
                    }
                });
            } else
                $scope.CallSaveUpdateTask();
        }
    };

    $scope.CallSaveUpdateTask = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var UserPhoto = $scope.newTask.Photo_TMP;
        var filesColl = $scope.newTask.AttachmentColl;
       
        $scope.newTask.DocumentDetColl = [];
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveExaminer",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));


                if (data.UsPhoto && data.UsPhoto.length > 0)
                    formData.append("UserPhoto", data.UsPhoto[0]);

                if (data.files) {
                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("file" + i, data.files[i].File);
                    }
                }

                return formData;
            },
            data: { jsonData: $scope.newTask, files: filesColl, UsPhoto: UserPhoto }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearTask();
                $scope.GetAllTaskList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    $scope.GetAllTaskList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.TaskList = [];
        $http({
            method: 'GET',
            url: base_url + "Communication/Creation/GetAllExaminer",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.TaskList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.GetTaskById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            ExaminerId: refData.ExaminerId
        };

        $http({
            method: 'POST',
            url: base_url + "Communication/Creation/getExaminerById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newTask = res.data.Data;
                $scope.newTask.Mode = 'Modify';
                document.getElementById('Task-section').style.display = "none";
                document.getElementById('Task-form').style.display = "block";
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelTaskById = function (refData) {
        Swal.fire({
            title: 'Do you want to delete the selected data?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();
                var para = {
                    ExaminerId: refData.ExaminerId
                };
                $http({
                    method: 'POST',
                    url: base_url + "Communication/Creation/DeleteExaminer",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess == true) {
                        $scope.GetAllTaskList();
                    }
                    Swal.fire(res.data.ResponseMSG);

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });


    };

});

