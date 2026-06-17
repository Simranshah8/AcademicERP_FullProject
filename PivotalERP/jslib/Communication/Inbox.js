
app.controller('InboxController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Inbox';

    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.CommunicationToList = [{ id: 1, text: 'Student' }, { id: 2, text: 'Employee' }];
        $scope.FilterStudentList = [{ id: 1, text: 'Class-Section' }, { id: 2, text: 'Student Type' }, { id: 3, text: 'Student Group' }, { id: 4, text: 'Student House' }, { id: 5, text: 'Transport' }, { id: 6, text: 'Gender' }, { id: 7, text: 'Individual' }];
        $scope.FilterEmployeeList = [{ id: 1, text: 'Employee' }, { id: 2, text: 'EmployeeGroup' }];
        $scope.currentPages = {
            Inbox: 1,
        };

        $scope.searchData = {
            Inbox: '',
        };

        $scope.perPage = {
            Inbox: GlobalServices.getPerPageRow(),
        };
        $scope.newStudent = {
            FilterID: 1
        };
        $scope.newEmployee = {
            EmployeeFilterID: 1
        };

        $scope.newInbox = {
            InboxId: null,
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
            InboxDetailsColl: [],
            AttachmentColl: [],
            Mode: 'Send'
        };
        $scope.newInbox.InboxDetailsColl.push({});
        /*$scope.GetAllInboxList();*/
    };


    $scope.ClearInbox = function () {
        $scope.ClearPhoto();
        $scope.newInbox = {
            InboxId: null,
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
            InboxDetailsColl: [],
            AttachmentColl: [],
            Mode: 'Send'
        };
        $scope.newInbox.InboxDetailsColl.push({});
    };

    $scope.ClearPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newInbox.PhotoData = null;
                $scope.newInbox.Photo_TMP = [];
                $scope.newInbox.PhotoPath = '';
            });

        });

        $('#imgPhoto1').attr('src', '');

    };

    $scope.myButton3 = 'btn-success';
    $scope.changeBgColor3 = function () {
        $scope.myButton3 = "btn-lightgreen ";
    };



    function OnClickDefault() {
        document.getElementById('detailinbox').style.display = "none";
        document.getElementById('replytoemailform').style.display = "none";
        document.getElementById('forwardtoemailform').style.display = "none";

        document.getElementById('smsdetailinbox').style.display = "none";
        document.getElementById('smsthread').style.display = "none";
        document.getElementById('notificationdetailinbox').style.display = "none";
        document.getElementById('whatsappdetailinbox').style.display = "none";
        document.getElementById('VoiceMessagedetailinbox').style.display = "none";
        document.getElementById('progressbarshow').style.display = "none";

        document.getElementById('opendetail').onclick = function () {
            document.getElementById('inboxsection').style.display = "none";
            document.getElementById('detailinbox').style.display = "block";
        }

        document.getElementById('back-to-inbox').onclick = function () {
            document.getElementById('detailinbox').style.display = "none";
            document.getElementById('inboxsection').style.display = "block";
        }


        document.getElementById('replymail').onclick = function () {
            document.getElementById('detailinbox').style.display = "none";
            document.getElementById('replytoemailform').style.display = "block";
        }

        document.getElementById('backtomaildetail').onclick = function () {
            document.getElementById('replytoemailform').style.display = "none";
            document.getElementById('detailinbox').style.display = "block";
        }


        document.getElementById('forwardmail').onclick = function () {
            document.getElementById('detailinbox').style.display = "none";
            document.getElementById('forwardtoemailform').style.display = "block";
        }

        document.getElementById('backtomaildetailfromforward').onclick = function () {
            document.getElementById('forwardtoemailform').style.display = "none";
            document.getElementById('detailinbox').style.display = "block";
        }
        //sms tab show hide starts
        document.getElementById('opensmsdetail').onclick = function () {
            document.getElementById('smssection').style.display = "none";
            document.getElementById('smsdetailinbox').style.display = "block";
        }
        document.getElementById('back-to-smsinbox').onclick = function () {
            document.getElementById('smssection').style.display = "block";
            document.getElementById('smsdetailinbox').style.display = "none";
        }

        //From thread
        document.getElementById('viewthread').onclick = function () {
            document.getElementById('smssection').style.display = "none";
            document.getElementById('smsthread').style.display = "block";
        }
        document.getElementById('back-to-smsfromthread').onclick = function () {
            document.getElementById('smssection').style.display = "block";
            document.getElementById('smsthread').style.display = "none";
        }

        //notification tab show hide starts
        document.getElementById('opennotificationdetail').onclick = function () {
            document.getElementById('notificationsection').style.display = "none";
            document.getElementById('notificationdetailinbox').style.display = "block";
        }
        document.getElementById('back-to-notificationinbox').onclick = function () {
            document.getElementById('notificationsection').style.display = "block";
            document.getElementById('notificationdetailinbox').style.display = "none";
        }
        //whatsapp tab show hide starts
        document.getElementById('openwhatsappdetail').onclick = function () {
            document.getElementById('whatsappsection').style.display = "none";
            document.getElementById('whatsappdetailinbox').style.display = "block";
        }
        document.getElementById('back-to-whatsappinbox').onclick = function () {
            document.getElementById('whatsappsection').style.display = "block";
            document.getElementById('whatsappdetailinbox').style.display = "none";
        }
        //voiceMessage tab here
        document.getElementById('VoiceMessagedetailinbox').style.display = "none";
        document.getElementById('forwardtoVoiceMessageform').style.display = "none";


        document.getElementById('VoiceMessageopendetail').onclick = function () {
            document.getElementById('VoiceMessageinboxsection').style.display = "none";
            document.getElementById('VoiceMessagedetailinbox').style.display = "block";
        }

        document.getElementById('back-to-VoiceMessageinbox').onclick = function () {
            document.getElementById('VoiceMessagedetailinbox').style.display = "none";
            document.getElementById('VoiceMessageinboxsection').style.display = "block";
        }



        document.getElementById('forwardVoiceMessage').onclick = function () {
            document.getElementById('VoiceMessagedetailinbox').style.display = "none";
            document.getElementById('forwardtoVoiceMessageform').style.display = "block";
        }

        document.getElementById('backtoVoiceMessagedetailfromforward').onclick = function () {
            document.getElementById('forwardtoVoiceMessageform').style.display = "none";
            document.getElementById('VoiceMessagedetailinbox').style.display = "block";
        }
        //Schedule date and time
        document.getElementById('schedulevoicediv').style.display = "none";
        document.getElementById('schedulevoicebth').onclick = function () {
            document.getElementById('schedulevoicediv').style.display = "block";
        }

      
        document.getElementById('openprogress').onclick = function () {
            document.getElementById('progressbarshow').style.display = "block";
        }


    };

    //************************* Class *********************************
    $scope.IsValidInbox = function () {
        if ($scope.newInbox.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }
        return true;
    };

    $scope.SaveUpdateInbox = function () {
        if ($scope.IsValidInbox() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newInbox.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateInbox();
                    }
                });
            } else
                $scope.CallSaveUpdateInbox();
        }
    };

    $scope.CallSaveUpdateInbox = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var UserPhoto = $scope.newInbox.Photo_TMP;
        var filesColl = $scope.newInbox.AttachmentColl;

        //if ($scope.newInbox.InboxDateDet) {
        //    $scope.newInbox.InboxDate = $scope.newInbox.InboxDateDet.dateAD;
        //} else
        //    $scope.newInbox.InboxDate = null;

        $scope.newInbox.DocumentDetColl = [];
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
            data: { jsonData: $scope.newInbox, files: filesColl, UsPhoto: UserPhoto }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearInbox();
                $scope.GetAllInboxList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    $scope.GetAllInboxList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.InboxList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllExaminer",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.InboxList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.GetInboxById = function (refData) {
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
                $scope.newInbox = res.data.Data;
                $scope.newInbox.Mode = 'Modify';
                document.getElementById('Inbox-section').style.display = "none";
                document.getElementById('Inbox-form').style.display = "block";
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelInboxById = function (refData) {
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
                        $scope.GetAllInboxList();
                    }
                    Swal.fire(res.data.ResponseMSG);

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });


    };

});

