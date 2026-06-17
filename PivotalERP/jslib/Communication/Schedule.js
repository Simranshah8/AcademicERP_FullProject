
app.controller('ScheduledController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Scheduled';

    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.CommunicationToList = [{ id: 1, text: 'Student' }, { id: 2, text: 'Employee' }];
        $scope.FilterStudentList = [{ id: 1, text: 'Class-Section' }, { id: 2, text: 'Student Type' }, { id: 3, text: 'Student Group' }, { id: 4, text: 'Student House' }, { id: 5, text: 'Transport' }, { id: 6, text: 'Gender' }, { id: 7, text: 'Individual' }];
        $scope.FilterEmployeeList = [{ id: 1, text: 'Employee' }, { id: 2, text: 'EmployeeGroup' }];
        $scope.currentPages = {
            Scheduled: 1,
        };

        $scope.searchData = {
            Scheduled: '',
        };

        $scope.perPage = {
            Scheduled: GlobalServices.getPerPageRow(),
        };
        $scope.newStudent = {
            FilterID: 1
        };
        $scope.newEmployee = {
            EmployeeFilterID: 1
        };

        $scope.newScheduled = {
            ScheduledId: null,
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
            ScheduledDetailsColl: [],
            AttachmentColl: [],
            Mode: 'Send'
        };
        $scope.newScheduled.ScheduledDetailsColl.push({});
        /*$scope.GetAllScheduledList();*/
    };


    $scope.ClearScheduled = function () {
        $scope.ClearPhoto();
        $scope.newScheduled = {
            ScheduledId: null,
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
            ScheduledDetailsColl: [],
            AttachmentColl: [],
            Mode: 'Save'
        };
        $scope.newScheduled.ScheduledDetailsColl.push({});
    };

    $scope.ClearPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newScheduled.PhotoData = null;
                $scope.newScheduled.Photo_TMP = [];
                $scope.newScheduled.PhotoPath = '';
            });

        });

        $('#imgPhoto1').attr('src', '');

    };



    function OnClickDefault() {
        //Email sent and scheduled button here
        document.getElementById('RejectTbl').style.display = "none";
        document.getElementById('VoiceRejectTbl').style.display = "none";

        document.getElementById('ApprovalBtn').onclick = function () {
            document.getElementById('RejectTbl').style.display = "none";
            document.getElementById('ApprovalTbl').style.display = "block";
        }
        document.getElementById('RejectBtn').onclick = function () {
            document.getElementById('ApprovalTbl').style.display = "none";
            document.getElementById('RejectTbl').style.display = "block";
        }




        document.getElementById('VoiceApprovalBtn').onclick = function () {
            document.getElementById('VoiceRejectTbl').style.display = "none";
            document.getElementById('VoiceApprovalTbl').style.display = "block";
        }
        document.getElementById('VoiceRejectBtn').onclick = function () {
            document.getElementById('VoiceApprovalTbl').style.display = "none";
            document.getElementById('VoiceRejectTbl').style.display = "block";
        }





        //SMS sent and scheduled button here
        document.getElementById('SMSRejectTbl').style.display = "none";

        document.getElementById('SMSApprovalBtn').onclick = function () {
            document.getElementById('SMSRejectTbl').style.display = "none";
            document.getElementById('SMSApprovalTbl').style.display = "block";
        }
        document.getElementById('SMSRejectBtn').onclick = function () {
            document.getElementById('SMSApprovalTbl').style.display = "none";
            document.getElementById('SMSRejectTbl').style.display = "block";
        }
        //Notification Tab
        document.getElementById('NotificationRejectTbl').style.display = "none";

        document.getElementById('NotificationApprovalBtn').onclick = function () {
            document.getElementById('NotificationRejectTbl').style.display = "none";
            document.getElementById('NotificationApprovalTbl').style.display = "block";
        }
        document.getElementById('NotificationRejectBtn').onclick = function () {
            document.getElementById('NotificationApprovalTbl').style.display = "none";
            document.getElementById('NotificationRejectTbl').style.display = "block";
        }
        //Whatsapp tab
        document.getElementById('WhatRejectTbl').style.display = "none";

        document.getElementById('WhatApprovalBtn').onclick = function () {
            document.getElementById('WhatRejectTbl').style.display = "none";
            document.getElementById('WhatApprovalTbl').style.display = "block";
        }
        document.getElementById('WhatRejectBtn').onclick = function () {
            document.getElementById('WhatApprovalTbl').style.display = "none";
            document.getElementById('WhatRejectTbl').style.display = "block";
        }

        //Forward, reply button start here

        document.getElementById('detailScheduled').style.display = "none";
        document.getElementById('replytoemailform').style.display = "none";
        document.getElementById('forwardtoemailform').style.display = "none";

        document.getElementById('smsdetailScheduled').style.display = "none";
        /*   document.getElementById('smsthread').style.display = "none";*/
        document.getElementById('notificationdetailScheduled').style.display = "none";
        document.getElementById('whatsappdetailScheduled').style.display = "none";

        document.getElementById('opendetail').onclick = function () {
            document.getElementById('Scheduledsection').style.display = "none";
            document.getElementById('detailScheduled').style.display = "block";
        }

        document.getElementById('back-to-Scheduled').onclick = function () {
            document.getElementById('detailScheduled').style.display = "none";
            document.getElementById('Scheduledsection').style.display = "block";
        }


        document.getElementById('replymail').onclick = function () {
            document.getElementById('detailScheduled').style.display = "none";
            document.getElementById('replytoemailform').style.display = "block";
        }

        document.getElementById('backtomaildetail').onclick = function () {
            document.getElementById('replytoemailform').style.display = "none";
            document.getElementById('detailScheduled').style.display = "block";
        }


        document.getElementById('forwardmail').onclick = function () {
            document.getElementById('detailScheduled').style.display = "none";
            document.getElementById('forwardtoemailform').style.display = "block";
        }

        document.getElementById('backtomaildetailfromforward').onclick = function () {
            document.getElementById('forwardtoemailform').style.display = "none";
            document.getElementById('detailScheduled').style.display = "block";
        }
        //sms tab show hide starts
        document.getElementById('opensmsdetail').onclick = function () {
            document.getElementById('smssection').style.display = "none";
            document.getElementById('smsdetailScheduled').style.display = "block";
        }
        document.getElementById('back-to-smsScheduled').onclick = function () {
            document.getElementById('smssection').style.display = "block";
            document.getElementById('smsdetailScheduled').style.display = "none";
        }

       
        //notification tab show hide starts
        document.getElementById('opennotificationdetail').onclick = function () {
            document.getElementById('notificationsection').style.display = "none";
            document.getElementById('notificationdetailScheduled').style.display = "block";
        }
        document.getElementById('back-to-notificationScheduled').onclick = function () {
            document.getElementById('notificationsection').style.display = "block";
            document.getElementById('notificationdetailScheduled').style.display = "none";
        }
        //whatsapp tab show hide starts
        document.getElementById('openwhatsappdetail').onclick = function () {
            document.getElementById('whatsappsection').style.display = "none";
            document.getElementById('whatsappdetailScheduled').style.display = "block";
        }
        document.getElementById('back-to-whatsappScheduled').onclick = function () {
            document.getElementById('whatsappsection').style.display = "block";
            document.getElementById('whatsappdetailScheduled').style.display = "none";
        }

          //Schedule 
        document.getElementById('VoiceMessagedetailinbox').style.display = "none";
        document.getElementById('openvoicedetail').onclick = function () {
            document.getElementById('VoiceMessageinboxsection').style.display = "none";
            document.getElementById('VoiceMessagedetailinbox').style.display = "block";
        }
        document.getElementById('back-to-VoiceMessageinbox').onclick = function () {
            document.getElementById('VoiceMessageinboxsection').style.display = "block";
            document.getElementById('VoiceMessagedetailinbox').style.display = "none";
        }

    };

    //************************* Class *********************************
    $scope.IsValidScheduled = function () {
        if ($scope.newScheduled.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }
        return true;
    };

    $scope.SaveUpdateScheduled = function () {
        if ($scope.IsValidScheduled() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newScheduled.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateScheduled();
                    }
                });
            } else
                $scope.CallSaveUpdateScheduled();
        }
    };

    $scope.CallSaveUpdateScheduled = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var UserPhoto = $scope.newScheduled.Photo_TMP;
        var filesColl = $scope.newScheduled.AttachmentColl;

        //if ($scope.newScheduled.ScheduledDateDet) {
        //    $scope.newScheduled.ScheduledDate = $scope.newScheduled.ScheduledDateDet.dateAD;
        //} else
        //    $scope.newScheduled.ScheduledDate = null;

        $scope.newScheduled.DocumentDetColl = [];
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
            data: { jsonData: $scope.newScheduled, files: filesColl, UsPhoto: UserPhoto }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearScheduled();
                $scope.GetAllScheduledList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    $scope.GetAllScheduledList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.ScheduledList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllExaminer",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ScheduledList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.GetScheduledById = function (refData) {
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
                $scope.newScheduled = res.data.Data;
                $scope.newScheduled.Mode = 'Modify';
                document.getElementById('Scheduled-section').style.display = "none";
                document.getElementById('Scheduled-form').style.display = "block";
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelScheduledById = function (refData) {
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
                        $scope.GetAllScheduledList();
                    }
                    Swal.fire(res.data.ResponseMSG);

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });


    };

});

