app.controller("PDCDetails", function ($scope, $http, $timeout, GlobalServices, $filter) {
    $scope.Title = 'PDCDetails';

    LoadData();
     
    function LoadData() {
        $('.select2').select2({
            allowClear: true,
            openOnEnter: true
        });

        $scope.BankColl = [];
        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetAllBank",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BankColl = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.loadingstatus = "stop";
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();


        $scope.perPage = {
            PDCDetails: GlobalServices.getPerPageRow(),

        };
        $scope.searchData = {
            PDCDetails: ''
        };
        $scope.currentPages = {
            PDCDetails: 1

        };
 
        $scope.beData =
        {
            TranId: 0,
            AgentId: 0,
            LedgerId: 0,
            AgentName: '',
            BankBranch: '',
            BankName: '',
            ReceiptNo:'',
            LedgerName: '',
            NY: 0,
            NM: 0,
            ND: 0,
            CNY: 0,
            CNM: 0,
            CND:0,
            DocumentColl: [],
            VoucherDate_TMP: new Date(),
            ChequeDate_TMP: new Date(),
            ValidDays: 0,
            Against: '',
            CustomerRemarks:'',
           
        };
        $scope.loadingstatus = "stop";



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
        $timeout(function () {
            $scope.DocumentTypeList = [];
            $scope.DocumentTypeList_Qry = [];
            $http({
                method: 'GET',
                url: base_url + "Global/GetDocumentTypes",
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.DocumentTypeList = res.data.Data;
                    $scope.DocumentTypeList_Qry = mx(res.data.Data);
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });

    };

    $scope.ClearFields = function () {
        $scope.loadingstatus = "stop";
        $scope.beData =
        {
            TranId: 0,
            AgentId: 0,
            LedgerId: 0,
            AgentName: '',
            BankBranch: '',
            BankName: '',
            ReceiptNo: '',
            LedgerName: '',
            NY: 0,
            NM: 0,
            ND: 0,
            CNY: 0,
            CNM: 0,
            CND: 0,
            DocumentColl: [],
            VoucherDate_TMP: new Date(),
            ChequeDate_TMP: new Date(),
            ValidDays: 0,
            Against: '',
            CustomerRemarks: '',
        };

    }
  
    $scope.GetAllPDCDetails = function () {

        $scope.PDCColl = []; //declare an empty array

        if ($scope.loadingstatus != 'stop') {
            alert('Already Running Process')
            return;
        }

        $scope.loadingstatus = 'running';
        showPleaseWait();

        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetPDC",
            dataType: "json"
            //data:JSON.stringify(para)
        }).then(function (res) {

            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $timeout(function () {
                    $scope.PDCColl = res.data.Data;
                });
            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });

    }

    $scope.CurDocument = {};
    $scope.AddMoreFiles = function () {

        if ($scope.CurDocument.DocumentTypeId > 0 && $scope.CurDocument.Document_TMP) {
            var findDocType = $scope.DocumentTypeList_Qry.firstOrDefault(p1 => p1.DocumentTypeId == $scope.CurDocument.DocumentTypeId);
            var file = $scope.CurDocument.Document_TMP[0];
            if (findDocType) {
                $scope.beData.DocumentColl.push({
                    DocumentTypeId: findDocType.DocumentTypeId,
                    DocumentTypeName: findDocType.Name,
                    File: file,
                    Name: file.name,
                    Type: file.type,
                    Size: file.size,
                    Description: '',
                    DocumentData: $scope.CurDocument.DocumentData,
                    DocPath: null
                });

                $scope.CurDocument = {};

                $('#flMoreFiles').val('');
            }
        }
    };
    $scope.delAttachmentFiles = function (ind) {
        if ($scope.beData.DocumentColl) {
            if ($scope.beData.DocumentColl.length > 0) {
                $scope.beData.DocumentColl.splice(ind, 1);
            }
        }
    }

    $scope.AddPDC = function () {
        if ($scope.confirmMSG.Accept == true) {
            var saveModify = $scope.beData.Mode;
            Swal.fire({
                title: 'Do you want to' + saveModify + 'the current data?',
                showCancelButton: true,
                confirmButtonText: saveModify,
            }).then((result) => {
                if (result.isConfirmed) {
                    $scope.CallSaveUpdatePDC();
                }

            });
        }
        else
            $scope.CallSaveUpdatePDC();
    };

    $scope.CallSaveUpdatePDC = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();

        var filesColl = $scope.beData.DocumentColl;

        if ($scope.beData.VoucherDateDet) {
            var dDet = $scope.beData.VoucherDateDet;
            $scope.beData.VoucherDate = $filter('date')(new Date(dDet.dateAD), 'yyyy-MM-dd');
            $scope.beData.NY = dDet.NY;
            $scope.beData.NM = dDet.NM;
            $scope.beData.ND = dDet.ND;
        } else
            $scope.beData.VoucherDate = $filter('date')(new Date(), 'yyyy-MM-dd');

        if ($scope.beData.ChequeDateDet) {
            var dDet = $scope.beData.ChequeDateDet;
            $scope.beData.ChequeDate = $filter('date')(new Date(dDet.dateAD), 'yyyy-MM-dd');
            $scope.beData.CNY = dDet.NY;
            $scope.beData.CNM = dDet.NM;
            $scope.beData.CND = dDet.ND;
        }


        $http({
            method: 'POST',
            url: base_url + "Account/Creation/SaveUpdatePDC",
            headers: { 'content-Type': undefined },

            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                if (data.files) {
                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("file" + i, data.files[i].File);
                    }
                }

                return formData;
            },
            data: { jsonData: $scope.beData, files: filesColl }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearFields();
                $scope.GetAllPDCDetails();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.getPDCById = function (beData) {

        $scope.loadingstatus = "running";

        var para = {
            TranId: beData.TranId
        };

        $http({
            method: 'POST',
            url: base_url + "Account/Creation/GetPDCById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $timeout(function () {
                    $scope.beData = res.data.Data;
                    $scope.beData.Mode = 'Modify';

                    if ($scope.beData.VoucherDate)
                        $scope.beData.VoucherDate_TMP = new Date($scope.beData.VoucherDate);

                    if ($scope.beData.ChequeDate)
                        $scope.beData.ChequeDate_TMP = new Date($scope.beData.ChequeDate);

                    if ($scope.beData.ValidDate)
                        $scope.beData.ValidDate_TMP = new Date($scope.beData.ValidDate);

                    if ($scope.beData.BounceDate)
                        $scope.beData.BounceDate_TMP = new Date($scope.beData.BounceDate);


                    $('#custom-tabs-four-profile-tab').tab('show');
                });
            } else
                Swal.fire(res.data.ResponseMSG);


        }, function (reason) {
            alert('Failed' + reason);
        });
    };

    $scope.deletePDC = function (refData, ind) {

        Swal.fire({
            title: 'Are you sure you want to delete selected PDC ' + refData.LedgerName + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            //message: 'Are you sure to delete selected Branch :-' + beData.Name,
        }).then((result) => {
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();
                var para = { TranId: refData.TranId };
                $http({
                    method: 'POST',
                    url: base_url + "Account/Creation/deletePDC",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess == true) {
                        $scope.PDCColl.splice(ind, 1);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);

                });
            }

        });
    }

    $scope.ShowPersonalImg = function (docDet) {
        $scope.viewImg = {
            ContentPath: '',
            File: null,
            FileData: null
        };
        if (docDet.DocPath || docDet.File) {
            $scope.viewImg.ContentPath = docDet.DocPath;
            $scope.viewImg.File = docDet.File;
            $scope.viewImg.FileData = docDet.DocumentData;
            $('#PersonalImg').modal('show');
        } else
            Swal.fire('No Image Found');

    };

  
});