app.controller('SendSmsController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'SendSms';
    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.CommunicationToList = [{ id: 1, text: 'Student' }, { id: 2, text: 'Employee' }];
        $scope.currentPages = {
            SendSms: 1,
        };

        $scope.searchData = {
            SendSms: '',
        };

        $scope.perPage = {
            SendSms: GlobalServices.getPerPageRow(),
        };

        $scope.newSendSms = {
            SendSmsId: null,
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
            SendSmsDetailsColl: [],
            AttachmentColl: [],
            Mode: 'Send'
        };
        $scope.newSendSms.SendSmsDetailsColl.push({});
        /*$scope.GetAllSendSmsList();*/
    };

    $scope.ClearSendSms = function () {
        $scope.ClearPhoto();
        $scope.newSendSms = {
            SendSmsId: null,
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
            SendSmsDetailsColl: [],
            AttachmentColl: [],
            Mode: 'Save'
        };
        $scope.newSendSms.SendSmsDetailsColl.push({});
    };    


    function OnClickDefault() {
        document.getElementById('selectstudent').style.display = "none";
        //SendSms section
        document.getElementById('searchstudent').onclick = function () {
            document.getElementById('selectstudent').style.display = "block";
        }        
    };

    //************************* Class *********************************
    $scope.IsValidSendSms = function () {
        if ($scope.newSendSms.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }
        return true;
    };

    $scope.SaveUpdateSendSms = function () {
        if ($scope.IsValidSendSms() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newSendSms.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateSendSms();
                    }
                });
            } else
                $scope.CallSaveUpdateSendSms();
        }
    };

    $scope.CallSaveUpdateSendSms = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var UserPhoto = $scope.newSendSms.Photo_TMP;
        var filesColl = $scope.newSendSms.AttachmentColl;

        //if ($scope.newSendSms.SendSmsDateDet) {
        //    $scope.newSendSms.SendSmsDate = $scope.newSendSms.SendSmsDateDet.dateAD;
        //} else
        //    $scope.newSendSms.SendSmsDate = null;

        $scope.newSendSms.DocumentDetColl = [];
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
            data: { jsonData: $scope.newSendSms, files: filesColl, UsPhoto: UserPhoto }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearSendSms();
                $scope.GetAllSendSmsList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    $scope.GetAllSendSmsList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.SendSmsList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllExaminer",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.SendSmsList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.GetSendSmsById = function (refData) {
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
                $scope.newSendSms = res.data.Data;
                $scope.newSendSms.Mode = 'Modify';
                
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelSendSmsById = function (refData) {
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
                        $scope.GetAllSendSmsList();
                    }
                    Swal.fire(res.data.ResponseMSG);

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });


    };

});

