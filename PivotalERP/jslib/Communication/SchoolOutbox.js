
app.controller('SchoolOutboxController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Outbox';

    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.CommunicationToList = [{ id: 1, text: 'Student' }, { id: 2, text: 'Employee' }];
        $scope.FilterStudentList = [{ id: 1, text: 'Class-Section' }, { id: 2, text: 'Student Type' }, { id: 3, text: 'Student Group' }, { id: 4, text: 'Student House' }, { id: 5, text: 'Transport' }, { id: 6, text: 'Gender' }, { id: 7, text: 'Individual' }];
        $scope.FilterEmployeeList = [{ id: 1, text: 'Employee' }, { id: 2, text: 'EmployeeGroup' }];
        $scope.currentPages = {
            Outbox: 1,
        };

        $scope.searchData = {
            Outbox: '',
        };

        $scope.perPage = {
            Outbox: GlobalServices.getPerPageRow(),
        };
        $scope.newStudent = {
            FilterID: 1
        };
        $scope.newEmployee = {
            EmployeeFilterID: 1
        };

        $scope.newOutbox = {
            OutboxId: null,
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
            OutboxDetailsColl: [],
            AttachmentColl: [],
            Mode: 'Send'
        };
        $scope.newOutbox.OutboxDetailsColl.push({});
        /*$scope.GetAllOutboxList();*/
    };


    $scope.ClearOutbox = function () {
        $scope.ClearPhoto();
        $scope.newOutbox = {
            OutboxId: null,
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
            OutboxDetailsColl: [],
            AttachmentColl: [],
            Mode: 'Save'
        };
        $scope.newOutbox.OutboxDetailsColl.push({});
    };

    $scope.ClearPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newOutbox.PhotoData = null;
                $scope.newOutbox.Photo_TMP = [];
                $scope.newOutbox.PhotoPath = '';
            });

        });

        $('#imgPhoto1').attr('src', '');

    };



    function OnClickDefault() {
        document.getElementById('detailOutbox').style.display = "none";
        document.getElementById('replytoemailform').style.display = "none";
        document.getElementById('forwardtoemailform').style.display = "none";
        document.getElementById('VoiceMessagedetailinbox').style.display = "none";

        document.getElementById('smsdetailOutbox').style.display = "none";
        /*   document.getElementById('smsthread').style.display = "none";*/
        document.getElementById('notificationdetailOutbox').style.display = "none";
        document.getElementById('whatsappdetailOutbox').style.display = "none";

        document.getElementById('opendetail').onclick = function () {
            document.getElementById('Outboxsection').style.display = "none";
            document.getElementById('detailOutbox').style.display = "block";
        }

        document.getElementById('back-to-Outbox').onclick = function () {
            document.getElementById('detailOutbox').style.display = "none";
            document.getElementById('Outboxsection').style.display = "block";
        }


        document.getElementById('replymail').onclick = function () {
            document.getElementById('detailOutbox').style.display = "none";
            document.getElementById('replytoemailform').style.display = "block";
        }

        document.getElementById('backtomaildetail').onclick = function () {
            document.getElementById('replytoemailform').style.display = "none";
            document.getElementById('detailOutbox').style.display = "block";
        }


        document.getElementById('forwardmail').onclick = function () {
            document.getElementById('detailOutbox').style.display = "none";
            document.getElementById('forwardtoemailform').style.display = "block";
        }

        document.getElementById('backtomaildetailfromforward').onclick = function () {
            document.getElementById('forwardtoemailform').style.display = "none";
            document.getElementById('detailOutbox').style.display = "block";
        }
        //sms tab show hide starts
        document.getElementById('opensmsdetail').onclick = function () {
            document.getElementById('smssection').style.display = "none";
            document.getElementById('smsdetailOutbox').style.display = "block";
        }
        document.getElementById('back-to-smsOutbox').onclick = function () {
            document.getElementById('smssection').style.display = "block";
            document.getElementById('smsdetailOutbox').style.display = "none";
        }

        ////From thread
        //document.getElementById('viewthread').onclick = function () {
        //    document.getElementById('smssection').style.display = "none";
        //    document.getElementById('smsthread').style.display = "block";
        //}
        //document.getElementById('back-to-smsfromthread').onclick = function () {
        //    document.getElementById('smssection').style.display = "block";
        //    document.getElementById('smsthread').style.display = "none";
        //}

        //notification tab show hide starts
        document.getElementById('opennotificationdetail').onclick = function () {
            document.getElementById('notificationsection').style.display = "none";
            document.getElementById('notificationdetailOutbox').style.display = "block";
        }
        document.getElementById('back-to-notificationOutbox').onclick = function () {
            document.getElementById('notificationsection').style.display = "block";
            document.getElementById('notificationdetailOutbox').style.display = "none";
        }
        //whatsapp tab show hide starts
        document.getElementById('openwhatsappdetail').onclick = function () {
            document.getElementById('whatsappsection').style.display = "none";
            document.getElementById('whatsappdetailOutbox').style.display = "block";
        }
        document.getElementById('back-to-whatsappOutbox').onclick = function () {
            document.getElementById('whatsappsection').style.display = "block";
            document.getElementById('whatsappdetailOutbox').style.display = "none";
        }
        //Voice tab show hide starts
        document.getElementById('VoiceMessageopendetail').onclick = function () {
            document.getElementById('VoiceMessageinboxsection').style.display = "none";
            document.getElementById('VoiceMessagedetailinbox').style.display = "block";
        }
        document.getElementById('back-to-VoiceMessageinbox').onclick = function () {
            document.getElementById('VoiceMessageinboxsection').style.display = "block";
            document.getElementById('VoiceMessagedetailinbox').style.display = "none";
        }
    };

    //************************* Class *********************************
    $scope.IsValidOutbox = function () {
        if ($scope.newOutbox.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }
        return true;
    };

    $scope.SaveUpdateOutbox = function () {
        if ($scope.IsValidOutbox() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newOutbox.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateOutbox();
                    }
                });
            } else
                $scope.CallSaveUpdateOutbox();
        }
    };

    $scope.CallSaveUpdateOutbox = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var UserPhoto = $scope.newOutbox.Photo_TMP;
        var filesColl = $scope.newOutbox.AttachmentColl;

        //if ($scope.newOutbox.OutboxDateDet) {
        //    $scope.newOutbox.OutboxDate = $scope.newOutbox.OutboxDateDet.dateAD;
        //} else
        //    $scope.newOutbox.OutboxDate = null;

        $scope.newOutbox.DocumentDetColl = [];
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
            data: { jsonData: $scope.newOutbox, files: filesColl, UsPhoto: UserPhoto }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearOutbox();
                $scope.GetAllOutboxList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    $scope.GetAllOutboxList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.OutboxList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllExaminer",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.OutboxList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.GetOutboxById = function (refData) {
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
                $scope.newOutbox = res.data.Data;
                $scope.newOutbox.Mode = 'Modify';
                document.getElementById('Outbox-section').style.display = "none";
                document.getElementById('Outbox-form').style.display = "block";
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelOutboxById = function (refData) {
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
                        $scope.GetAllOutboxList();
                    }
                    Swal.fire(res.data.ResponseMSG);

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });


    };

});

