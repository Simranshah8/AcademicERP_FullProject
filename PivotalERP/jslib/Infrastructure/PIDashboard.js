
app.controller('PIDashboardController', function ($scope, $http, $timeout, $filter, $translate, $rootScope, GlobalServices) {
    OnClickDefault();
    $scope.Title = 'PI Dashboard';
    $scope.LoadData = function () {
        $('.select2').select2();

        $scope.newCompanyDetails = {};
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetCompanyDet",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newCompanyDet = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });



        $scope.Logo = [];
        $http({
            method: 'POST',
            url: base_url + "AppCMS/Creation/GetAllAboutUsList",
            dataType: "json",
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.Logo = res.data.Data[0];
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

       


        $scope.LandDetailsList = [];
        $http({
            method: 'GET',
            url: base_url + "Infrastructure/Creation/GetAllLandDetails",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.LandDetailsList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.BuildingDetailsList = [];

        $http({
            method: 'POST',
            url: base_url + "Hostel/Creation/GetAllBuildingList",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BuildingDetailsList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.VehicleSummaryList = [];
        $http({
            method: 'POST',
            url: base_url + "Transport/Creation/GetAllVehicleList",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.VehicleSummaryList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.RoomDistributionList = [];
        $http({
            method: 'POST',
            url: base_url + "Infrastructure/Reporting/GetAllRoomDistribution",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.RoomDistributionList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
       

        $scope.GetAllAssetsDet();
    }


    $scope.GetAllAssetsDet = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.AllAssetsList = [];
        $http({
            method: 'Post',
            url: base_url + "Assets/Creation/GetAllAssets",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.AllAssetsList = [];


                var query = mx(res.data.Data).groupBy(t => ({ BuildingName: t.BuildingName, FloorName: t.FloorName }));
                var sno = 1;

                angular.forEach(query, function (q) {
                    var pare = {
                        SNo: sno,
                        BuildingName: q.key.BuildingName,
                        FloorName: q.key.FloorName,
                        ChieldColl: []
                    };

                    angular.forEach(q.elements, function (el) {
                        pare.ChieldColl.push(el);
                    })
                    $scope.AllAssetsList.push(pare);
                    sno++;
                });
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

 

    function OnClickDefault() {
        document.getElementById('printcard').style.display = "none";
    }

    //$scope.PrintData = function () {
    //    $('#printcard').printThis();
    //}
    $scope.PrintData = function () {

        document.getElementById('printcard').style.display = "block";

        setTimeout(function () {
            $('#printcard').printThis();

            setTimeout(function () {
                document.getElementById('printcard').style.display = "none";
            }, 1000);
        }, 200);
    };

});