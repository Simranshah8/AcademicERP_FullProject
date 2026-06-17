app.controller('MeritAchieversController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Merit Achievers';
    OnClickDefault();

    $scope.LoadData = function () {
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();

        $scope.currentPages = {
            MeritAchievers: 1,
        };

        $scope.searchData = {
            MeritAchievers: '',
        };

        $scope.perPage = {
            MeritAchievers: GlobalServices.getPerPageRow(),
        };

        //$scope.GetAllMeritAchievers();

        $scope.newMeritAchieversDet = {
            TranId: '',
            Name: '',
            OrderNo: 0,
            Faculty: '',
            MeritAchievementsColl: [],
            Description: '',
            Mode: 'Save'
        };
        $scope.newMeritAchieversDet.MeritAchievementsColl.push({});
        $scope.GetAllMeritAchievers();
    }

    function OnClickDefault() {
        document.getElementById('merit-achievers-form').style.display = "none";

        document.getElementById('add-merit-achievers').onclick = function () {
            document.getElementById('merit-achievers-table').style.display = "none";
            document.getElementById('merit-achievers-form').style.display = "block";
        }

        document.getElementById('merit-achievers-back-btn').onclick = function () {
            document.getElementById('merit-achievers-form').style.display = "none";
            document.getElementById('merit-achievers-table').style.display = "block";
        }
    }


    $scope.ClearDetails = function () {
        $scope.ClearPhoto();
        $scope.newMeritAchieversDet = {
            TranId: '',
            Name: '',
            OrderNo: 0,
            Faculty: '',
            MeritAchievementsColl: [],
            Description: '',
            Mode: 'Save'
        };
        $scope.newMeritAchieversDet.MeritAchievementsColl.push({});
    }


    $scope.ClearPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newMeritAchieversDet.PhotoData = null;
                $scope.newMeritAchieversDet.Photo_TMP = [];
            });
        });
        $('#imgAcadImage1').attr('src', '');
    };


    $scope.AddAchievements = function (ind) {
        if ($scope.newMeritAchieversDet.MeritAchievementsColl) {
            if ($scope.newMeritAchieversDet.MeritAchievementsColl.length > ind + 1) {
                $scope.newMeritAchieversDet.MeritAchievementsColl.splice(ind + 1, 0, {
                    Achievement: ''
                })
            } else {
                $scope.newMeritAchieversDet.MeritAchievementsColl.push({
                    Achievement: ''
                })
            }
        }
    };
    $scope.DelAchievements = function (ind) {
        if ($scope.newMeritAchieversDet.MeritAchievementsColl) {
            if ($scope.newMeritAchieversDet.MeritAchievementsColl.length > 1) {
                $scope.newMeritAchieversDet.MeritAchievementsColl.splice(ind, 1);
            }
        }
    };

    $scope.IsValidAddMeritAchievers = function () {
        if ($scope.newMeritAchieversDet.Name.isEmpty()) {
            Swal.fire('Please ! Enter  Name');
            return false;
        }


        return true;
    }


    $scope.SaveUpdateAddDetails = function () {
        if ($scope.IsValidAddMeritAchievers() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newMeritAchieversDet.Mode;
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
        var Sphoto = $scope.newMeritAchieversDet.Photo_TMP;
        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/SaveMeritAchievers",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                if (data.meritAchieversPhoto && data.meritAchieversPhoto.length > 0)
                    formData.append("Sphoto", data.meritAchieversPhoto[0]);
                return formData;
            },
            data: { jsonData: $scope.newMeritAchieversDet, meritAchieversPhoto: Sphoto }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearDetails();
                $scope.GetAllMeritAchievers();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetAllMeritAchievers = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.MeritAchieversList = [];
        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/GetAllMeritAchieversList",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.MeritAchieversList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GetMeritAchieversById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            TranId: refData.TranId
        };
        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/GetMeritAchieversById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newMeritAchieversDet = res.data.Data;

                if (!$scope.newMeritAchieversDet.MeritAchievementsColl || $scope.newMeritAchieversDet.MeritAchievementsColl.length == 0) {
                    $scope.newMeritAchieversDet.MeritAchievementsColl = [];
                    $scope.newMeritAchieversDet.MeritAchievementsColl.push({});
                }


                $scope.newMeritAchieversDet.Mode = 'Modify';

                document.getElementById('merit-achievers-table').style.display = "none";
                document.getElementById('merit-achievers-form').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DeleteMeritAchievers = function (refData) {
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
                    url: base_url + "AppCMS/Creation/DelMeritAchievers",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.ClearDetails();
                        $scope.GetAllMeritAchievers();
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