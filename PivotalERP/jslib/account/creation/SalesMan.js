app.controller("SalesMan", function ($scope, $http ,$filter,$timeout,GlobalServices) {
    $scope.Title = 'SalesMan';

    var glSrv = GlobalServices;
    LoadData();
     
    function LoadData() {
        $('.select2').select2();
        $scope.loadingstatus = "stop";

        $scope.confirmMSG = glSrv.getConfirmMSG();
        $scope.perPageColl = glSrv.getPerPageList();

        $scope.DrCrList = glSrv.getDrCr();

        $scope.perPage = {
            Salesman: glSrv.getPerPageRow(),

        };
        $scope.searchData = {
            Salesman: ''
        };
        $scope.currentPages = {
            Salesman: 1

        };
        
        $scope.beData =
        {
            AgentId: 0,
            Name: '',
            NameNP: '',
            Level:1,
            Alias: '',
            Code: '',
            ParentAgent: '',
            ParentAgentId: 0,
            Address: '',
            Mobile: '',
            PhoneNo: '',
            Email: '',
            Fax:'',
            CitizenshipNo: '',
            PanNo: '',
            AreaId: 0, 
            Opening: 0,
            DrCr: 1,
            UserId: 0,            
            AutoNumber: 0,
            IsActive:true,
            CommissionRate: 0,
            BranchId:0,

        };

        $scope.BranchList = [];
        $http({
            method: 'GET',
            url: base_url + "Setup/Security/GetAllBranchList",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BranchList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.LevelList = [];
        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetSalesmanLevel",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.LevelList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });



        $scope.AreaMasterList = [];
        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetAllAreaMasterList",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.AreaMasterList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
        $scope.UserList = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllUserList",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.UserList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


    };
    $scope.ClearFields = function () {
        $scope.loadingstatus = "stop";
        $scope.beData =
        {
            AgentId: 0,
            Name: '',
            NameNP: '',
            Level: 1,
            Alias: '',
            Code: '',
            ParentAgent: '',
            ParentAgentId: 0,
            Address: '',
            Mobile: '',
            PhoneNo: '',
            Email: '',
            Fax: '',
            CitizenshipNo: '',
            PanNo: '',
            AreaId: 0,
             
            Opening: 0,
            DrCr: 1,
            UserId: 0,
            AutoNumber: 0,
            IsActive: true,
            CommissionRate: 0,
            BranchId: 0,
        };

    }

    
    $scope.GetAllSalesMan = function () {

        $scope.AgentList = [];
        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetAllSalesMan",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.AgentList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    }
     
    $scope.IsValidAgentMode = function () {
        if ($scope.beData.Name.isEmpty()) {
            Swal.fire('Please ! Enter Name');
            return false;
        }
        if ($scope.beData.Address.isEmpty()) {
            Swal.fire('Please ! Enter Address');
            return false;
        }
        if ($scope.beData.Mobile.isEmpty()) {
        Swal.fire('Please ! Enter MobileNo');
        return false;
    }
    return true;
    }

    $scope.SaveAgentMode = function () {
        if ($scope.IsValidAgentMode() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.beData.Mode;
                Swal.fire({
                    title: 'Do you want to' + saveModify + 'the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateAgentMode();
                    }
                });
            } else
                $scope.CallSaveUpdateAgentMode();
        }
    };


    $scope.CallSaveUpdateAgentMode = function ()
    {
        $scope.loadingstatus = 'running';
        showPleaseWait();

        $http({
            method: 'POST',
            url: base_url + "Account/Creation/SaveSalesMan",
            headers: { 'content-Type': undefined },

            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));
                return formData;
            },
            data: { jsonData: $scope.beData }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearFields();
                $scope.GetAllSalesMan();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
         
       
    }


    $scope.getSalesmanById = function (beData) {

        $scope.loadingstatus = "running";

        var para = {
            AgentId: beData.AgentId
        };

        $http({
            method: 'POST',
            url: base_url + "Account/Creation/getSalesmanById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                var resData = res.data.Data;
                $timeout(function () {
                    $scope.beData = res.data.Data;
                    $scope.beData.Mode = 'Modify';
                     

                    $('#custom-tabs-four-profile-tab').tab('show');
                });
            } else
                Swal.fire(res.data.ResponseMSG);


        }, function (reason) {
            alert('Failed' + reason);
        });
    };

    $scope.deleteSalesman = function (refData, ind) {

        Swal.fire({
            title: 'Are you sure you want to delete selected Salesman ' + refData.Name + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            //message: 'Are you sure to delete selected Branch :-' + beData.Name,
        }).then((result) => {
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();
                var para = { AgentId: refData.AgentId };
                $http({
                    method: 'POST',
                    url: base_url + "Account/Creation/deleteSalesmanById",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess == true) {
                        $scope.AgentList.splice(ind, 1);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);

                });
            }

        });
    }
});