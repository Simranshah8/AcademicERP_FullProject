
app.controller('CircularController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Circular';

    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.CircularForList = [{ id: 1, text: 'Student' }, { id: 2, text: 'Teacher' }, { id: 3, text: 'All' }];
        $scope.currentPages = {
            Circular: 1,
        };

        $scope.searchData = {
            Circular: '',
        };

        $scope.perPage = {
            Circular: GlobalServices.getPerPageRow(),
        };


        $scope.newCircular = {
            CircularId: null,
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
            AttachmentColl: [],
            Mode: 'Save'
        };
      /*  $scope.GetAllCircularList();*/
    };


    $scope.ClearCircular = function () {
        $scope.ClearPhoto();
        $scope.newCircular = {
            CircularId: null,
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
            AttachmentColl: [],
            Mode: 'Save'
        };
    };



    function OnClickDefault() {
        document.getElementById('circular-form').style.display = "none";

        document.getElementById('add-circular').onclick = function () {
            document.getElementById('circular-section').style.display = "none";
            document.getElementById('circular-form').style.display = "block";
        }
        document.getElementById('back-to-list-circular').onclick = function () {
            document.getElementById('circular-form').style.display = "none";
            document.getElementById('circular-section').style.display = "block";
        }
    };

    //************************* Circular *********************************

    var BASE64_MARKER = ';base64,';
    // Does the given URL (string) look like a base64-encoded URL?
    function isDataURI(url) {
        return url.split(BASE64_MARKER).length === 2;
    };
    function dataURItoFile(dataURI) {
        if (!isDataURI(dataURI)) { return false; }

        // Format of a base64-encoded URL:
        // data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAYAAAAEOCAIAAAAPH1dAAAAK
        var mime = dataURI.split(BASE64_MARKER)[0].split(':')[1];
        var filename = 'rc-' + (new Date()).getTime() + '.' + mime.split('/')[1];
        //var filename = 'dataURI-file-' + (new Date()).getTime() + '.jpeg';
        var bytes = atob(dataURI.split(BASE64_MARKER)[1]);
        var writer = new Uint8Array(new ArrayBuffer(bytes.length));

        for (var i = 0; i < bytes.length; i++) {
            writer[i] = bytes.charCodeAt(i);
        }

        return new File([writer.buffer], filename, { type: mime });
    }




    $scope.IsValidCircular = function () {
        if ($scope.newCircular.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }
        return true;
    };

    $scope.SaveUpdateCircular = function () {
        if ($scope.IsValidCircular() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newCircular.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateCircular();
                    }
                });
            } else
                $scope.CallSaveUpdateCircular();
        }
    };

    $scope.CallSaveUpdateCircular = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var UserPhoto = $scope.newCircular.Photo_TMP;
        var filesColl = $scope.newCircular.AttachmentColl;

        //if ($scope.newCircular.CircularDateDet) {
        //    $scope.newCircular.CircularDate = $scope.newCircular.CircularDateDet.dateAD;
        //} else
        //    $scope.newCircular.CircularDate = null;

        $scope.newCircular.DocumentDetColl = [];
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
            data: { jsonData: $scope.newCircular, files: filesColl, UsPhoto: UserPhoto }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearCircular();
               /* $scope.GetAllCircularList();*/
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    $scope.GetAllCircularList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.CircularList = [];
        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllExaminer",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.CircularList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.GetCircularById = function (refData) {
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
                $scope.newCircular = res.data.Data;
                $scope.newCircular.Mode = 'Modify';
                document.getElementById('circular-section').style.display = "none";
                document.getElementById('circular-form').style.display = "block";
            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelCircularById = function (refData) {
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
                       /* $scope.GetAllCircularList();*/
                    }
                    Swal.fire(res.data.ResponseMSG);

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });


    };

});

