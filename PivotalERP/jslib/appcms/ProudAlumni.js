app.controller('ProudAlumniController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Proud Alumnni';
    OnClickDefault();

    $scope.LoadData = function () {
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();

        $scope.currentPages = {
            ProudAlumni: 1,
        };

        $scope.searchData = {
            ProudAlumni: '',
        };

        $scope.perPage = {
            ProudAlumni: GlobalServices.getPerPageRow(),
        };

        $scope.GetAllProudAlumni();

        $scope.newProudAlumniDet = {
            TranId: '',
            Name: '',
            OrderNo: 0,
            DegreeDetails:'',
            CurrentCompany: '',
            Position: '',
            Description: '',
            Mode: 'Save'
        };
    }

    function OnClickDefault() {
        document.getElementById('proud-alumni-form').style.display = "none";

        document.getElementById('add-proud-alumni').onclick = function () {
            document.getElementById('proud-alumni-table').style.display = "none";
            document.getElementById('proud-alumni-form').style.display = "block";
        }

        document.getElementById('proud-alumni-back-btn').onclick = function () {
            document.getElementById('proud-alumni-form').style.display = "none";
            document.getElementById('proud-alumni-table').style.display = "block";
        }
    }


    $scope.ClearDetails = function () {
        $scope.ClearPhoto();
        $scope.newProudAlumniDet = {
            TranId: '',
            Name: '',
            OrderNo: 0,
            DegreeDetails: '',
            CurrentCompany: '',
            Position: '',
            Description:'',
            Mode: 'Save'
        };
    }

    $scope.ClearPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newProudAlumniDet.PhotoData = null;
                $scope.newProudAlumniDet.Photo_TMP = [];
            });
        });
        $('#imgAcadImage1').attr('src', '');
    };

    $scope.IsValidAddProudAlumni = function () {
        if ($scope.newProudAlumniDet.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }
        return true;
    }

    $scope.SaveUpdateAddDetails = function () {
        if ($scope.IsValidAddProudAlumni() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newProudAlumniDet.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateDetails();
                    }
                });
            } else
                $scope.CallSaveUpdateDetails();
        }
    };

    $scope.CallSaveUpdateDetails = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();


        var Sphoto = $scope.newProudAlumniDet.Photo_TMP;


        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/SaveProudAlumni",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                if (data.proudAlumniPhoto && data.proudAlumniPhoto.length > 0)
                    formData.append("Sphoto", data.proudAlumniPhoto[0]);
                return formData;
            },
            data: { jsonData: $scope.newProudAlumniDet, proudAlumniPhoto: Sphoto }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearDetails();
                $scope.GetAllProudAlumni();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetAllProudAlumni = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.ProudAlumniList = [];
        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/GetAllProudAlumni",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ProudAlumniList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GetProudAlumniById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            TranId: refData.TranId
        };
        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/GetProudAlumniById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newProudAlumniDet = res.data.Data;

                $scope.newProudAlumniDet.Mode = 'Modify';

                document.getElementById('proud-alumni-table').style.display = "none";
                document.getElementById('proud-alumni-form').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DeleteProudAlumni = function (refData) {
        Swal.fire({
            title: 'Do you want to delete the selected data?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();
                var para = {
                    TranId: refData.TranId
                };
                $http({
                    method: 'POST',
                    url: base_url + "AppCMS/Creation/DeleteProudAlumni",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.ClearDetails();
                        $scope.GetAllProudAlumni();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });
    };

    $scope.pageChangeHandler = function (num) {
        console.log('page changed to ' + num);
    };

});