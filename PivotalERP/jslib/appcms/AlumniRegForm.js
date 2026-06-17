app.controller('AlumniRegFormController', function ($scope, $http, $timeout, $filter, GlobalServices) {

    // Initialize the form
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        OnClickDefault();


        $scope.currentPages = {
            Alumni: 1,
        };

        $scope.searchData = {
            Alumni: '',
        };

        $scope.perPage = {
            Alumni: GlobalServices.getPerPageRow(),
        };

        $scope.newAlumniReg = {
            AlumniRegId: null,
            FullName: '',
            DOB_TMP: null,
            DOB: null,
            OriginalAddress: '',
            CurrentAddress: '',
            Contact: '',
            Email: '',
            JoinedYear: '',
            StudiedUpTo: '',
            SEE: '',
            PlusTwo: '',

            // 1. Marksheet
            MarksheetPhoto_TMP: null,
            MarksheetPhotoData: null,

            // 2. Profile photo
            Photo_TMP: null,
            PhotoData: null,

            // 3. Memory photos
            MemoryPhotos_TMP: null,
            MemoryPhotosData: null,


            // 4. Degree / career
            DegreeTitle: '',
            University: '',
            CurPosition: '',
            CurCompany: '',
            CurUniversity: '',

            // 5. Achievement doc (PDF/image)
            AchievementPhoto_TMP: null,
            AchievementPhotoData: null,

            Achievements: '',
            Bio: '',
            Remarks: '',

            AlumniRegColl: []

           
        };

        $scope.newAlumniReg.AlumniRegColl.push({});


        $scope.GetAllAlumni();
      

    };

    $scope.ClearDetails = function () {
        $scope.newAlumniReg = {
            AlumniRegId: null,
            FullName: '',
            DOB_TMP: null,
            DOB: null,
            OriginalAddress: '',
            CurrentAddress: '',
            Contact: '',
            Email: '',
            JoinedYear: '',
            StudiedUpTo: '',
            SEE: '',
            PlusTwo: '',

            // 1. Marksheet
            MarksheetPhoto_TMP: null,
            MarksheetPhotoData: null,

            // 2. Profile photo
            Photo_TMP: null,
            PhotoData: null,

            // 3. Memory photos
            MemoryPhotos_TMP: null,
            MemoryPhotosData: null,

            // 4. Degree / career
            DegreeTitle: '',
            University: '',
            CurPosition: '',
            CurCompany: '',
            CurUniversity: '',

            // 5. Achievement doc
            AchievementPhoto_TMP: null,
            AchievementPhotoData: null,

            Achievements: '',
            Bio: '',
            Remarks: '',

            AlumniRegColl: []
        };

        $scope.newAlumniReg.AlumniRegColl.push({});

    };
    function OnClickDefault() {
        document.getElementById('AlumniDetailForm').style.display = "none";


        document.getElementById('AddDetails').onclick = function () {
            document.getElementById('AlumniDetailsList').style.display = "none";
            document.getElementById('AlumniDetailForm').style.display = "block";

            $scope.AddDetails();
        }

        document.getElementById('detailsback-btn').onclick = function () {
            document.getElementById('AlumniDetailForm').style.display = "none";
            document.getElementById('AlumniDetailsList').style.display = "block";
            $scope.ClearDetails();
        }

    };

    //event handler for marksheet file
    $scope.handleMarksheetFileChange = function ($event) {
        var input = $event.target;
        if (input.files && input.files.length > 0) {
            var file = input.files[0];
            var reader = new FileReader();

            reader.onload = function (e) {
                $scope.$apply(function () {
                    $scope.newAlumniReg.MarksheetPhotoData = e.target.result;
                });
            };

            reader.readAsDataURL(file);
        }
    };


    //check for image or pdf
    $scope.isImage = function (dataUrl) {
        return dataUrl && dataUrl.startsWith('data:image');
    };

    $scope.isPDF = function (dataUrl) {
        return dataUrl && dataUrl.startsWith('data:application/pdf');
    };
    //clear marksheet field
    $scope.ClearMarksheetUpload = function () {
        $scope.newAlumniReg.MarksheetPhoto_TMP = null;
        $scope.newAlumniReg.MarksheetPhotoData = null;
        document.getElementById('choose-Back').value = '';
    };

    // event handler for profile photo
    $scope.handleProfilePhotoChange = function ($event) {
        var input = $event.target;
        if (input.files && input.files.length > 0) {
            var file = input.files[0];

            if (!file.type.startsWith('image/')) {
                alert("Please upload a valid image (.jpg)");
                return;
            }

            var reader = new FileReader();
            reader.onload = function (e) {
                $scope.$apply(function () {
                    $scope.newAlumniReg.Photo_TMP = e.target.result;
                });
            };

            reader.readAsDataURL(file);
        }
    };
    $scope.ClearProfilePhoto = function () {
        $scope.newAlumniReg.Photo_TMP = null;
        $scope.newAlumniReg.PhotoData = null;
        var fileInput = document.getElementById('profilePhoto');
        if (fileInput) {
            fileInput.value = '';
        }
    };




    $scope.memoryPhotos = [];

    $scope.handleMemoryPhotosUpload = function (event) {
        const files = event.target.files;
        if (!files || !files.length) return;

        for (let i = 0; i < files.length; i++) {
            if ($scope.memoryPhotos.length >= 2) {
                alert("You can upload a maximum of two photos.");
                break;
            }

            const file = files[i];
            const isImage = file.type.startsWith("image/");
            if (!isImage) {
                alert("Only image files (.jpg, .png) are allowed.");
                continue;
            }

            const reader = new FileReader();
            reader.onload = function (e) {
                $scope.$apply(function () {
                    $scope.memoryPhotos.push(e.target.result);
                });
            };
            reader.readAsDataURL(file);
        }

        // Reset file input so re-selection of same file works
        event.target.value = '';
    };

    // Remove single memory photo
    $scope.removeMemoryPhoto = function (index) {
        $scope.memoryPhotos.splice(index, 1);
    };

    // handler for Achievement file
    $scope.handleAchievementDocChange = function ($event) {
        var input = $event.target;
        if (input.files && input.files.length > 0) {
            var file = input.files[0];
            var reader = new FileReader();

            reader.onload = function (e) {
                $scope.$apply(function () {
                    $scope.newAlumniReg.Achievement_DocData = e.target.result;
                });
            };

            reader.readAsDataURL(file);
        }
    };

    $scope.ClearAchievementDoc = function () {
        $scope.newAlumniReg.AchievementPhoto_TMP = null;
        $scope.newAlumniReg.AchievementPhotoData = null;

        var fileInput = document.getElementById('achievementEvidence');
        if (fileInput) {
            fileInput.value = '';
        }
    };


    $scope.IsValidAlumniReg = function () {
        if ($scope.newAlumniReg.FullName.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }

        return true;
    }



    $scope.SaveUpdateAlumniReg = function () {
        if ($scope.IsValidAlumniReg() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newAlumniReg.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateAlumniReg();
                    }
                });
            } else
                $scope.CallSaveUpdateAlumniReg();
        }
    };
    $scope.CallSaveUpdateAlumniReg = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();


        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/SaveAlumniReg",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();

                if ($scope.newAlumniReg.MarksheetPhoto_TMP && $scope.newAlumniReg.MarksheetPhoto_TMP.length > 0)
                    formData.append("MarksheetPhoto", $scope.newAlumniReg.MarksheetPhoto_TMP[0]);

                if ($scope.newAlumniReg.Photo_TMP && $scope.newAlumniReg.Photo_TMP.length > 0)
                    formData.append("ProfilePhoto", $scope.newAlumniReg.Photo_TMP[0]);

                if ($scope.newAlumniReg.MemoryPhoto1_TMP && $scope.newAlumniReg.MemoryPhoto1_TMP.length > 0)
                    formData.append("MemoryPhoto1", $scope.newAlumniReg.MemoryPhoto1_TMP[0]);

                if ($scope.newAlumniReg.MemoryPhoto2_TMP && $scope.newAlumniReg.MemoryPhoto2_TMP.length > 0)
                    formData.append("MemoryPhoto2", $scope.newAlumniReg.MemoryPhoto2_TMP[0]);

                if ($scope.newAlumniReg.AchievementPhoto_TMP && $scope.newAlumniReg.AchievementPhoto_TMP.length > 0)
                    formData.append("Achievement_Doc", $scope.newAlumniReg.AchievementPhoto_TMP[0]);

                if ($scope.newAlumniReg.DOBDet && $scope.newAlumniReg.DOBDet.dateAD) {
                    $scope.newAlumniReg.DOB = $filter('date')(new Date($scope.newAlumniReg.DOBDet.dateAD), 'yyyy-MM-dd');
                } else {
                    $scope.newAlumniReg.DOB = null;
                }

                formData.append("jsonData", angular.toJson(data.jsonData));

                if (data.marksheetFile) {
                    formData.append("MarksheetFile", data.marksheetFile);
                }

                if (data.profilePhotoFile) {
                    formData.append("ProfilePhotoFile", data.profilePhotoFile);
                }

                if (data.memoryPhotoFiles && data.memoryPhotoFiles.length > 0) {
                    for (var i = 0; i < data.memoryPhotoFiles.length && i < 2; i++) {
                        formData.append("MemoryPhotoFiles", data.memoryPhotoFiles[i]);
                    }
                }

                if (data.achievementDocFile) {
                    formData.append("AchievementDocFile", data.achievementDocFile);
                }

                return formData;
            },
            data: {
                jsonData: $scope.newAlumniReg,
                marksheetFile: $scope.newAlumniReg.MarksheetPhoto_TMP,
                profilePhotoFile: $scope.newAlumniReg.Photo_TMP,
                memoryPhotoFiles: $scope.newAlumniReg.MemoryPhotoFiles_TMP,
                achievementDocFile: $scope.newAlumniReg.AchievementPhoto_TMP
            }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess === true) {
                $scope.ClearAlumniRegForm();
            }
        }, function (err) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });

    }

    $scope.GetAllAlumni = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.AlumniList = [];

        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/GetAllAlumni",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.AlumniList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };



    $scope.DelAlumnibyId = function (refData, index) {
        Swal.fire({
            title: 'Are you sure you want to delete ' + refData.FullName + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            if (result.isConfirmed) {
                var para = { AlumniRegId: refData.AlumniRegId };
                $http({
                    method: 'POST',
                    url: base_url + "AppCMS/Creation/DelAlumniById",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingStatus = "stop";

                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess == true) {

                        $scope.AlumniList.splice(ind, 1);

                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }

        });
    }

    $scope.GetAlumniById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();

        var para = {
            AlumniRegId: refData.AlumniRegId
        };

        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/GetAlumniRegById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.IsSuccess && res.data.Data) {
                $scope.newAlumniReg = res.data.Data;
                $scope.newAlumniReg.Mode = 'Modify';

                // Show the modal only after data is available
                $('#previewModal').modal('show');

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };


    $scope.previewTemplate = function (item) {
        $scope.viewImg1 = {
            ContentPath: '',
            FileType: null
        };

        if (item.DocPath && item.DocPath.length > 0) {
            $scope.viewImg1.ContentPath = item.DocPath;
            $scope.viewImg1.FileType = 'pdf';
        } else if (item.ProfilePhoto && item.ProfilePhoto.length > 0) {
            $scope.viewImg1.ContentPath = item.ProfilePhoto;
            $scope.viewImg1.FileType = 'image';
        } else if (item.File) {
            var blob = new Blob([item.File], { type: item.File?.type });
            $scope.viewImg1.ContentPath = URL.createObjectURL(blob);
            $scope.viewImg1.FileType = item.File.type.startsWith('image/') ? 'image' : 'pdf';
        } else {
            Swal.fire('No Image Found');
            return;
        }

        $('#templatePreviewModal').modal('show');
    };

    $scope.allPreviews = [];

    $scope.openAllPreviews = function (item) {
        $scope.allPreviews = [];

        const fields = [
            { label: 'Marksheet', path: item.MarksheetPath, type: 'image' },
            { label: 'Memory Photo 1', path: item.MemoryPhoto1, type: 'image' },
            { label: 'Memory Photo 2', path: item.MemoryPhoto2, type: 'image' },
            { label: 'Achievement Document', path: item.Achievement_Doc, type: 'image' },
            { label: 'Document PDF', path: item.DocPath, type: 'pdf' },
        ];

        angular.forEach(fields, function (field) {
            if (field.path && field.path.length > 0) {
                $scope.allPreviews.push({
                    label: field.label,
                    ContentPath: field.path,
                    FileType: field.type
                });
            }
        });

        if ($scope.allPreviews.length === 0) {
            Swal.fire('No documents found.');
            return;
        }

        $('#unifiedPreviewModal').modal('show');
    };

    $scope.printAlumniPreview = function () {
        $('#printArea').printThis({
            importCSS: true,
            importStyle: true,
            loadCSS: "",
            pageTitle: "Alumni Registration Preview",
            removeInline: false,
            header: null,
            footer: null
        });
    };

    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

});

