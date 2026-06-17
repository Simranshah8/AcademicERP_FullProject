
app.controller('HealthExaminerController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Health Issue/Disease';

    OnClickDefault();

    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();


        $scope.currentPages = {
            HealthExaminer: 1
        };

        $scope.searchData = {
            HealthExaminer: ''
        };

        $scope.perPage = {
            HealthExaminer: GlobalServices.getPerPageRow()
        };


        $scope.newHealthExaminer = {
            ExaminerId: null,
            Name: '',
            Designation: '',
            ExaminerRegdNo: '',
            MobileNo: '',
            Email: '',
            Qualification: '',
            Specialization: '',
            UsernameId: null,
            Address: '',
            Remarks: '',
            PhotoPath: '',
            SPhotoPath: '',
            AttachmentColl: [],
            Mode: 'Save'
        };

        $scope.DocumentTypeList = [];
        GlobalServices.getDocumentTypeList().then(function (res) {
            $scope.DocumentTypeList = res.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.GetAllExaminerList();
    };


    $scope.ClearExaminer = function () {
        $scope.ClearPhoto();
        $scope.newHealthExaminer = {
            ExaminerId: null,
            Name: '',
            Designation: '',
            ExaminerRegdNo: '',
            MobileNo: '',
            Email: '',
            Qualification: '',
            Specialization: '',
            UsernameId: null,
            Address: '',
            Remarks: '',
            PhotoPath: '',
            SPhotoPath: '',
            AttachmentColl: [],
            Mode: 'Save'
        };

    };


    $scope.ClearPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newHealthExaminer.PhotoData = null;
                $scope.newHealthExaminer.Photo_TMP = [];
                $scope.newHealthExaminer.PhotoPath = '';
            });

        });

        $('#imgPhoto1').attr('src', '');

    };


    function OnClickDefault() {
        document.getElementById('HealthExaminer-form').style.display = "none";
        //HealthExaminer section
        document.getElementById('add-HealthExaminer').onclick = function () {
            document.getElementById('HealthExaminer-section').style.display = "none";
            document.getElementById('HealthExaminer-form').style.display = "block";

        }
        document.getElementById('back-to-list-HealthExaminer').onclick = function () {
            document.getElementById('HealthExaminer-form').style.display = "none";
            document.getElementById('HealthExaminer-section').style.display = "block";
        }
    };

    //$scope.delAttachmentFilesReceived = function (ind) {
    //    if ($scope.newHealthExaminer.AttachmentColl) {
    //        if ($scope.newHealthExaminer.AttachmentColl.length > 0) {
    //            $scope.newHealthExaminer.AttachmentColl.splice(ind, 1);
    //        }
    //    }
    //}

    $scope.delAttachmentDoc = function (ind) {
        if ($scope.newHealthExaminer.AttachmentColl) {
            if ($scope.newHealthExaminer.AttachmentColl.length > 0) {
                $scope.newHealthExaminer.AttachmentColl.splice(ind, 1);
            }
        }
    };

    $scope.AddMoreFilesReceived = function (files, docType, des) {

        if (files && docType) {
            if (files != null && docType != null) {

                angular.forEach(files, function (file) {
                    $scope.newHealthExaminer.AttachmentColl.push({
                        DocumentTypeId: docType.id,
                        DocumentTypeName: docType.text,
                        File: file,
                        Name: file.name,
                        Type: file.type,
                        Size: file.size,
                        Description: des,
                        Path: null
                    });
                })

                $scope.docType = null;
                $scope.attachFile = null;
                $scope.docDescription = '';
            }
        }
    };

    $scope.ShowPersonalImg = function (item) {
        $scope.viewImg = {
            ContentPath: '',
            File: null
        };
        if (item.DocPath && item.DocPath.length > 0) {
            $scope.viewImg.ContentPath = item.DocPath;
            $('#PersonalImg').modal('show');
        } else if (item.PhotoPath && item.PhotoPath.length > 0) {
            $scope.viewImg.ContentPath = item.PhotoPath;
            $('#PersonalImg').modal('show');
        } else if (item.File) {
            $scope.viewImg.File = item.File;
            var blob = new Blob([item.File], { type: item.File?.type });
            $scope.viewImg.ContentPath = URL.createObjectURL(blob);

            $('#PersonalImg').modal('show');
        }

        else
            Swal.fire('No Image Found');

    };

    //************************* HealthExaminer *********************************
    $scope.IsValidExaminer = function () {
        if ($scope.newHealthExaminer.Name.isEmpty()) {
            Swal.fire('Please ! Enter Examiner Name');
            return false;
        }
        return true;
    }

    $scope.SaveUpdateExaminer = function () {
        if ($scope.IsValidExaminer() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newHealthExaminer.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateExaminer();
                    }
                });
            } else
                $scope.CallSaveUpdateExaminer();
        }
    };

    $scope.CallSaveUpdateExaminer = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var UserPhoto = $scope.newHealthExaminer.Photo_TMP;
        var filesColl = $scope.newHealthExaminer.AttachmentColl;
        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveUpdateExaminer",
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
            data: { jsonData: $scope.newHealthExaminer, files: filesColl, UsPhoto: UserPhoto }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearExaminer();
                $scope.GetAllExaminerList();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }



    $scope.GetAllExaminerList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.ExaminerList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllExaminer",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ExaminerList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GetExaminerById = function (refData) {
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
                $scope.newHealthExaminer = res.data.Data;
                $scope.newHealthExaminer.Mode = 'Save';
                document.getElementById('HealthExaminer-section').style.display = "none";
                document.getElementById('HealthExaminer-form').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelExaminerById = function (refData, ind) {
        Swal.fire({
            title: 'Are you sure you want to delete ' + refData.Name + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            //message: 'Are you sure to delete selected Branch :-' + beData.Name,
        }).then((result) => {
            if (result.isConfirmed) {
                var para = { ExaminerId: refData.ExaminerId };
                $http({
                    method: 'POST',
                    url: base_url + "Infirmary/Creation/DeleteExaminer",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingStatus = "stop";

                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess == true) {
                        $scope.GetAllExaminerList();
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }

        });
    }


    $scope.UserNameList = [];
    $http({
        method: 'GET',
        url: base_url + "Infirmary/Creation/GetAllUserNameList",
        dataType: "json"
    }).then(function (res) {
        hidePleaseWait();
        $scope.loadingstatus = "stop";
        if (res.data.IsSuccess && res.data.Data) {
            $scope.UserNameList = res.data.Data;

        } else {
            Swal.fire(res.data.ResponseMSG);
        }

    }, function (reason) {
        Swal.fire('Failed' + reason);
    });




});