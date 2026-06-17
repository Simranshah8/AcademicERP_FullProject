
app.controller('ClasswiseInfirmaryController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'ClasswiseInfirmary';


    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.SeverityList = [{ id: 1, text: 'High' }, { id: 2, text: 'Low' }, { id: 3, text: 'Medium' }];
        $scope.currentPages = {
            ClasswiseInfirmary: 1,
        };

        $scope.searchData = {
            ClasswiseInfirmary: '',
        };

        $scope.perPage = {
            ClasswiseInfirmary: GlobalServices.getPerPageRow(),
        };


     


        $scope.newClasswiseInfirmary = {
            ClasswiseInfirmaryId: null,
            Name: '',
            Severity: '',
            OrderNo: 1,
            Description: '',

            Mode: 'Save'
        };

        /*$scope.GetAllClasswiseInfirmaryList();*/
    };


    $scope.ClearClasswiseInfirmary = function () {
        $scope.newClasswiseInfirmary = {
            ClasswiseInfirmaryId: null,
            Name: '',
            Severity: '',
            OrderNo: 1,
            Description: '',

            Mode: 'Save'
        };

    };


   

    //************************* Class *********************************

    $scope.IsValidClasswiseInfirmary = function () {
        if ($scope.newClasswiseInfirmary.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }

        return true;
    };

    $scope.SaveUpdateClasswiseInfirmary = function () {
        if ($scope.IsValidClasswiseInfirmary() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newClasswiseInfirmary.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateClasswiseInfirmary();
                    }
                });
            } else
                $scope.CallSaveUpdateClasswiseInfirmary();

        }
    };

    $scope.CallSaveUpdateClasswiseInfirmary = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        if ($scope.newClasswiseInfirmary.ClasswiseInfirmaryDateDet) {
            $scope.newClasswiseInfirmary.ClasswiseInfirmaryDate = $scope.newClasswiseInfirmary.ClasswiseInfirmaryDateDet.dateAD;
        } else
            $scope.newClasswiseInfirmary.ClasswiseInfirmaryDate = null;


        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/SaveClasswiseInfirmary",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newClasswiseInfirmary }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearClasswiseInfirmary();
                /*$scope.GetAllClasswiseInfirmaryList();*/
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    $scope.GetAllClasswiseInfirmaryList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.ClasswiseInfirmaryList = [];

        $http({
            method: 'GET',
            url: base_url + "Infirmary/Creation/GetAllClasswiseInfirmaryList",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ClasswiseInfirmaryList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };

    $scope.GetClasswiseInfirmaryById = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            ClasswiseInfirmaryId: refData.ClasswiseInfirmaryId
        };

        $http({
            method: 'POST',
            url: base_url + "Infirmary/Creation/GetClasswiseInfirmaryById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newClasswiseInfirmary = res.data.Data;
                $scope.newClasswiseInfirmary.Mode = 'Modify';
                document.getElementById('ClasswiseInfirmary-section').style.display = "none";
                document.getElementById('ClasswiseInfirmary-form').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelClasswiseInfirmaryById = function (refData) {
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
                    ClasswiseInfirmaryId: refData.ClasswiseInfirmaryId
                };

                $http({
                    method: 'POST',
                    url: base_url + "Infirmary/Creation/DeleteClasswiseInfirmary",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess == true) {
                        /*  $scope.GetAllClasswiseInfirmaryList();*/

                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });


    };

});

