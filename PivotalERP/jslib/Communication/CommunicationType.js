
app.controller('CommunicationTypeController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'CommunicationType';

    OnClickDefault();
    $scope.LoadData = function () {
        $('.select2').select2();
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.currentPages = {
            CommunicationType: 1,
        };

        $scope.searchData = {
            CommunicationType: '',
        };

        $scope.perPage = {
            CommunicationType: GlobalServices.getPerPageRow(),
        };


        $scope.newCommunicationType = {
            CommunicationTypeId: null,
            CommunicationName: '',
            OrderNo: 1,
            Description: '',
            Mode: 'Save'
        };

        /* $scope.GetAllCommunicationTypeList();*/
    };

    $scope.ClearCommunicationType = function () {
        $scope.newCommunicationType = {
            CommunicationTypeId: null,
            CommunicationName: '',
            OrderNo: 1,
            Description: '',
            Mode: 'Save'
        };
    };

    function OnClickDefault() {

        document.getElementById('Communication-form').style.display = "none";
        document.getElementById('Voice-form').style.display = "none";
        document.getElementById('progressbardiv').style.display = "none";


        document.getElementById('add-communication').onclick = function () {
            document.getElementById('communication-section').style.display = "none";
            document.getElementById('Communication-form').style.display = "block";
        }
        document.getElementById('back-to-list-communication').onclick = function () {
            document.getElementById('Communication-form').style.display = "none";
            document.getElementById('communication-section').style.display = "block";
        }
        //voice communication
        document.getElementById('add-Voice').onclick = function () {
            document.getElementById('Voice-section').style.display = "none";
            document.getElementById('Voice-form').style.display = "block";
        }
        document.getElementById('back-to-list-Voice').onclick = function () {
            document.getElementById('Voice-form').style.display = "none";
            document.getElementById('Voice-section').style.display = "block";
        }


        document.getElementById('expendvoicetranslateprogress').onclick = function () {
            document.getElementById('progressbardiv').style.display = "block";
        }
    };

    //************************* Class *********************************

    $scope.IsValidCommunicationType = function () {
        if ($scope.newCommunicationType.CommunicationName.isEmpty()) {
            Swal.fire('Please ! Enter Communication Name');
            return false;
        }

        return true;
    };

    $scope.SaveUpdateCommunicationType = function () {
        if ($scope.IsValidCommunicationType() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newCommunicationType.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    /* Read more about isConfirmed, isDenied below */
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateCommunicationType();
                    }
                });
            } else
                $scope.CallSaveUpdateCommunicationType();
        }
    };

    $scope.CallSaveUpdateCommunicationType = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();

        if ($scope.newCommunicationType.CommunicationTypeDateDet) {
            $scope.newCommunicationType.CommunicationTypeDate = $scope.newCommunicationType.CommunicationTypeDateDet.dateAD;
        } else
            $scope.newCommunicationType.CommunicationTypeDate = null;
        $http({
            method: 'POST',
            url: base_url + "Communication/Creation/SaveCommunicationType",
            headers: { 'Content-Type': undefined },

            transformRequest: function (data) {

                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                return formData;
            },
            data: { jsonData: $scope.newCommunicationType }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);

            if (res.data.IsSuccess == true) {
                $scope.ClearCommunicationType();
                $scope.GetAllCommunicationTypeList();
            }

        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

        });
    };

    $scope.GetAllCommunicationTypeList = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        $scope.CommunicationTypeList = [];

        $http({
            method: 'GET',
            url: base_url + "Communication/Creation/GetAllCommunicationType",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.CommunicationTypeList = res.data.Data;

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };

    $scope.GetCommunicationTypeId = function (refData) {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            CommunicationTypeId: refData.CommunicationTypeId
        };

        $http({
            method: 'POST',
            url: base_url + "Communication/Creation/GetCommunicationTypeById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.newCommunicationType = res.data.Data;
                $scope.newCommunicationType.Mode = 'Modify';
                document.getElementById('communication-section').style.display = "none";
                document.getElementById('Communication-form').style.display = "block";

            } else {
                Swal.fire(res.data.ResponseMSG);
            }

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.DelCommunicationTypeId = function (refData) {
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
                    CommunicationTypeId: refData.CommunicationTypeId
                };

                $http({
                    method: 'POST',
                    url: base_url + "Communication/Creation/DeleteCommunicationType",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess == true) {
                        /*  $scope.GetAllCommunicationTypeList();*/

                    }
                    Swal.fire(res.data.ResponseMSG);

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        });

    };

});

