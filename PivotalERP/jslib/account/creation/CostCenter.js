app.controller("CostCenter", function ($scope, $http, GlobalServices, $timeout) {
    $scope.Title = 'Cost Center';

    var glSrv = GlobalServices;

    $scope.loadingstatus = "stop";

    LoadData();

    function LoadData() {
        $('.select2').select2();

        $scope.VoucherSearchOptions = [{ text: 'Name', value: 'C.Name', searchType: 'text' }, { text: 'Alias', value: 'C.Alias', searchType: 'text' }, { text: 'Code', value: 'C.Code', searchType: 'text' }];
        $scope.paginationOptions = {
            pageNumber: 1,
            pageSize: glSrv.getPerPageRow(),
            sort: null,
            SearchType: 'text',
            SearchCol: '',
            SearchVal: '',
            SearchColDet: $scope.VoucherSearchOptions[0],
            pagearray: [],
            pageOptions: [5, 10, 20, 30, 40, 50]
        };


        $scope.loadingstatus = "stop";

        $scope.VDCColl = GetVDCList();
        $scope.confirmMSG = glSrv.getConfirmMSG();
        $scope.DrCrList = glSrv.getDrCr();

        $scope.perPageColl = GlobalServices.getPerPageList();
 
        $scope.perPage = {
            CostCenter: GlobalServices.getPerPageRow(),

        };
        $scope.currentPages = {
            CostCenter: 1

        };
        $scope.beData =
        {
            CostCenterId: 0,
            Name: '',
            Alias: '',
            Code: '',
            Address: '',
            PanVatNo: '',
            PhoneNo: '',
            Email: '',
            Status:true,
            ActiveInterestCalculation: false,
            CostCategoryName: '',
            CostCategoryId: 0,
            LedgerId: null,
            Opening: 0,
            DrCr: 1,
            InterestRate: 0,
            InterestPer: 0,
            InterestOn: 1,
            AfterDaysInterestActive: 0,
            OpeningForBranchId: 1,
            AditionalCostOnTheBasis:1,
        };
     
        $scope.CostCategoryList = [];
        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetAllCostCategoryLisst",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.CostCategoryList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.AditionalCostTypeList = [];
        $http({
            method: 'POST',
            url: base_url + "Account/Creation/GetAditionalCostTypes",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.AditionalCostTypeList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.DimensionColl = [];
        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetAllDimension",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.DimensionColl = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };
    $scope.GenerateCode = function () {

        if ($scope.beData.CostCenterId > 0 && $scope.beData.Code && $scope.beData.Code.length > 0)
            return;

        $scope.beData.Code = '';
        var para = {
            name: $scope.beData.Name,
            CostCategoryId: $scope.beData.CostCategoryId
        };
        $http({
            method: 'POST',
            url: base_url + "Account/Creation/GetCostCenterCode",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $timeout(function () {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.beData.Code = res.data.Data.ResponseId;
                }
            });
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };
    $scope.ClearFields = function () {
        $scope.loadingstatus = "stop";
        $scope.beData =
        {
            CostCenterId: 0,
            Name: '',
            Alias: '',
            Code: '',
            Address: '',
            PanVatNo: '',
            PhoneNo: '',
            Email: '',
            Status: true,
            ActiveInterestCalculation: false,
            CostCategoryName: '',
            CostCategoryId: 0,
            LedgerId: null,
            Opening: 0,
            DrCr: 1,
            InterestRate: 0,
            InterestPer: 0,
            InterestOn: 1,
            AfterDaysInterestActive: 0,
            OpeningForBranchId: 1,
            AditionalCostOnTheBasis: 1,

        };
        $('#txtName').focus();
    }

    $scope.IsValidCostCenter = function () {
        if ($scope.beData.Name.isEmpty()) {
            Swal.fire("Please ! Enter Cost Center Name");
            return false;
        }
        else
            return true;
    }

    $scope.AddNewCostCenter = function () {
        if ($scope.IsValidCostCenter() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.beData.Mode;
                Swal.fire({
                    title: 'Do you want to' + saveModify + 'the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateCostCenter();
                    }

                });
            }
            else
                $scope.CallSaveUpdateCostCenter();
        }
    };

    $scope.CallSaveUpdateCostCenter = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();
        $scope.beData.DebitCredit = $scope.beData.DrCr;

        $http({
            method: 'POST',
            url: base_url + "Account/Creation/SaveUpdateCostCenter",
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
                $scope.SearchData();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }
    $scope.getCostCenterById = function (beData) {

            $scope.loadingstatus = "running";
            showPleaseWait();

            var para = {
                CostCenterId: beData.CostCenterId
            };

            $http({
                method: 'POST',
                url: base_url + "Account/Creation/getCostCenterById",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {
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

    $scope.deleteCostCenter = function (refData, ind) {

            Swal.fire({
                title: 'Are you sure you want to delete ' + refData.Name + '?',
                showCancelButton: true,
                confirmButtonText: 'Delete',
            }).then((result) => {
                if (result.isConfirmed) {
                    $scope.loadingstatus = "running";
                    showPleaseWait();
                    var para = { CostCenterId: refData.CostCenterId };
                    $http({
                        method: 'POST',
                        url: base_url + "Account/Creation/deleteCostCenter",
                        dataType: "json",
                        data: JSON.stringify(para)
                    }).then(function (res) {
                        hidePleaseWait();
                        $scope.loadingstatus = "stop";

                        Swal.fire(res.data.ResponseMSG);
                        if (res.data.IsSuccess == true) {
                            $scope.SearchData();
                        }
                    }, function (reason) {
                        Swal.fire('Failed' + reason);

                    });
                } 

            });
        }


    $scope.SearchDataColl = [];
    $scope.SearchData = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();
        $scope.paginationOptions.TotalRows = 0;

        var sCol = $scope.paginationOptions.SearchColDet;

        var para = {
            filter: {
                DateFrom: null,
                DateTo: null,
                PageNumber: $scope.paginationOptions.pageNumber,
                RowsOfPage: $scope.paginationOptions.pageSize,
                SearchCol: (sCol ? sCol.value : ''),
                SearchVal: $scope.paginationOptions.SearchVal,
                SearchType: (sCol ? sCol.searchType : 'text')
            }
        };

        $http({
            method: 'POST',
            url: base_url + "Account/Creation/GetCostCenterLst",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $scope.SearchDataColl = res.data.Data;
                $scope.paginationOptions.TotalRows = res.data.TotalCount;
                

            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });


    };

    $scope.ReSearchData = function (pageInd) {
        if (pageInd && pageInd >= 0)
            $scope.paginationOptions.pageNumber = pageInd;
        else if (pageInd == -1)
            $scope.paginationOptions.pageNumber = 1;

        $scope.loadingstatus = 'running';
        showPleaseWait();
        $scope.paginationOptions.TotalRows = 0;
        var sCol = $scope.paginationOptions.SearchColDet;

        var para = {
            filter: {
                DateFrom: null,
                DateTo: null,
                PageNumber: $scope.paginationOptions.pageNumber,
                RowsOfPage: $scope.paginationOptions.pageSize,
                SearchCol: (sCol ? sCol.value : ''),
                SearchVal: $scope.paginationOptions.SearchVal,
                SearchType: (sCol ? sCol.searchType : 'text')
            }
        };

        $http({
            method: 'POST',
            url: base_url + "Account/Creation/GetCostCenterLst",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $scope.SearchDataColl = res.data.Data;
                $scope.paginationOptions.TotalRows = res.data.TotalCount;

            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });

    }
});