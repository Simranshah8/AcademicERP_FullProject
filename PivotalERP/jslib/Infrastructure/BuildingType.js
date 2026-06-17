app.controller('BuildingTypeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'BuildingType';

    OnClickDefault();
    $scope.LoadData = function () {

        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.currentPages = {
            BuildingType: 1,
        };


        $scope.searchData = {
            BuildingType: '',
        };

        $scope.perPage = {
            BuildingType: GlobalServices.getPerPageRow(),
        };


        $scope.newDet = {
            BuildingTypeId: null,
            Name: '',
            OrderNo: 0,
            Description: '',
            Mode: 'save'
        };

        $scope.GetAllBuildingTypeList();

    };

    function OnClickDefault() {
        document.getElementById('add-BuildingType-form').style.display = "none";
        document.getElementById('add-BuildingType').onclick = function () {
            document.getElementById('table-section').style.display = "none";
            document.getElementById('add-BuildingType-form').style.display = "block";
        }
        document.getElementById('BuildingTypeback-btn').onclick = function () {
            document.getElementById('add-BuildingType-form').style.display = "none";
            document.getElementById('table-section').style.display = "block";
        }
    };

    $scope.ClearBuildingType = function () {
        $scope.newDet = {
            BuildingTypeId: null,
            Name: '',
            OrderNo: 0,
            Description: '',
            Mode: 'save'
        };
    }

    //************************* BuildingType*********************************
  
    $scope.SaveUpdateBuildingType = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $http({
            method: 'POST',
            url: base_url + "Infrastructure/Creation/SaveBuildingType",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newDet }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearBuildingType();
                $scope.GetAllBuildingTypeList();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    }

    $scope.GetAllBuildingTypeList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.BuildingTypeList = [];
        $http({
            method: 'GET',
            url: base_url + "Infrastructure/Creation/GetAllBuildingType",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BuildingTypeList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.GetBuildingTypeById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            BuildingTypeId: refData.BuildingTypeId
        };
        $http({
            method: 'POST',
            url: base_url + "Infrastructure/Creation/getBuildingTypeById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newDet = res.data.Data;
                $scope.newDet.Mode = 'Modify';

                document.getElementById('table-section').style.display = "none";
                document.getElementById('add-BuildingType-form').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };


    $scope.DelBuildingTypeById = function (refData, ind) {
        Swal.fire({
            title: 'Are you sure you want to delete ' + refData.Name + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            //message: 'Are you sure to delete selected Branch :-' + beData.Name,
        }).then((result) => {
            if (result.isConfirmed) {
                var para = { BuildingTypeId: refData.BuildingTypeId };
                $http({
                    method: 'POST',
                    url: base_url + "Infrastructure/Creation/DeleteBuildingType",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingStatus = "stop";

                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess == true) {
                        $scope.BuildingTypeList.splice(ind, 1);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }

        });
    }

});