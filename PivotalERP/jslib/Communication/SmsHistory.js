
app.controller('SmsHistoryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'SmsHistory';

    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.CommunicationToList = [{ id: 1, text: 'Student' }, { id: 2, text: 'Employee' }];
        $scope.currentPages = {
            SmsHistory: 1,
        };

        $scope.searchData = {
            SmsHistory: '',
        };

        $scope.perPage = {
            SmsHistory: GlobalServices.getPerPageRow(),
        };


        $scope.newSmsHistory = {
            SmsHistoryId: null,
            Name: '',
            Designation: '',
            ExaminerRegdNo: null,
            MobileNo: null,
            Email: '',
            Qualification: '',
            Address: '',
            Specialization: '',
            Username: '',
            Remarks: '',
            SmsHistoryDetailsColl: [],
            AttachmentColl: [],
            Mode: 'Send'
        };
        $scope.newSmsHistory.SmsHistoryDetailsColl.push({});
        /*$scope.GetAllSmsHistoryList();*/
    };


    $scope.ClearSmsHistory = function () {
        $scope.ClearPhoto();
        $scope.newSmsHistory = {
            SmsHistoryId: null,
            Name: '',
            Designation: '',
            ExaminerRegdNo: null,
            MobileNo: null,
            Email: '',
            Qualification: '',
            Address: '',
            Specialization: '',
            Username: '',
            Remarks: '',
            SmsHistoryDetailsColl: [],
            AttachmentColl: [],
            Mode: 'Save'
        };
        $scope.newSmsHistory.SmsHistoryDetailsColl.push({});
    };

    $scope.ClearPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newSmsHistory.PhotoData = null;
                $scope.newSmsHistory.Photo_TMP = [];
                $scope.newSmsHistory.PhotoPath = '';
            });

        });

        $('#imgPhoto1').attr('src', '');

    };


    function OnClickDefault() {
        document.getElementById('reply-form').style.display = "none";
        document.getElementById('forward-form').style.display = "none";
        //SmsHistory section
        document.getElementById('replymail').onclick = function () {
            document.getElementById('SmsHistory-section').style.display = "none";
            document.getElementById('reply-form').style.display = "block";
        }

        document.getElementById('back-reply').onclick = function () {
            document.getElementById('reply-form').style.display = "none";
            document.getElementById('SmsHistory-section').style.display = "block";
        }

        document.getElementById('forwardmail').onclick = function () {
            document.getElementById('SmsHistory-section').style.display = "none";
            document.getElementById('forward-form').style.display = "block";
        }

        document.getElementById('back-forward').onclick = function () {
            document.getElementById('forward-form').style.display = "none";
            document.getElementById('SmsHistory-section').style.display = "block";
        }
    };

    //************************* Class *********************************
    $scope.IsValidSmsHistory = function () {
        if ($scope.newSmsHistory.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }
        return true;
    };

    $scope.SaveUpdateSmsHistory = function () {
        if ($scope.IsValidSmsHistory() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newSmsHistory.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateSmsHistory();
                    }
                });
            } else
                $scope.CallSaveUpdateSmsHistory();
        }
    };

    $scope.CallSaveUpdateSmsHistory = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var UserPhoto = $scope.newSmsHistory.Photo_TMP;
        var filesColl = $scope.newSmsHistory.AttachmentColl;

        //if ($scope.newSmsHistory.SmsHistoryDateDet) {
        //    $scope.newSmsHistory.SmsHistoryDate = $scope.newSmsHistory.SmsHistoryDateDet.dateAD;
        //} else
        //    $scope.newSmsHistory.SmsHistoryDate = null;

        $scope.newSmsHistory.DocumentDetColl = [];
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
            data: { jsonData: $scope.newSmsHistory, files: filesColl, UsPhoto: UserPhoto }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearSmsHistory();
                $scope.GetAllSmsHistoryList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    $scope.GetAllSmsHistoryList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.SmsHistoryList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllExaminer",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.SmsHistoryList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.GetSmsHistoryById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            ExaminerId: refData.ExaminerId
        };

        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/getExaminerById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newSmsHistory = res.data.Data;
                $scope.newSmsHistory.Mode = 'Modify';
                document.getElementById('SmsHistory-section').style.display = "none";
                document.getElementById('SmsHistory-form').style.display = "block";
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelSmsHistoryById = function (refData) {
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
                    url: base_url + "Infirmary/Creation/DeleteExaminer",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess == true) {
                        $scope.GetAllSmsHistoryList();
                    }
                    Swal.fire(res.data.ResponseMSG);

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });


    };

});

