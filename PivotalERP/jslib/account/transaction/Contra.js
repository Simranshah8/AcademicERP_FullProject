app.controller('ContraController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Contra';
    var glSrv = GlobalServices;
    LoadData();

    $scope.sideBarData = [];
    
    $scope.lastTranId = 0;
    function LoadData() {
        $scope.VoucherSearchOptions = [{ text: 'VoucherNo', value: 'TS.AutoVoucherNo', searchType: 'Number' }, { text: 'RefNo', value: 'TS.[RefNo]', searchType: 'text' }, { text: 'VoucherDate', value: 'TS.VoucherDate', searchType: 'date' }, { text: 'Voucher Name', value: 'V.VoucherName', searchType: 'text' }, { text: 'CostClass', value: 'CC.Name', searchType: 'text' },];

        $scope.GenConfig = {};
        glSrv.getGenConfig().then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.GenConfig = res.data.Data;
            }
        }, function (reason) {
            alert('Failed' + reason);
        });

        $scope.paginationOptions = {
            pageNumber: 1,
            pageSize: glSrv.getPerPageRow(),
            sort: null,
            SearchType: 'text',
            SearchCol: '',
            SearchVal: '',
           /* SearchColDet: null,*/
            pagearray: [],
            pageOptions: [5, 10, 20, 30, 40, 50],
            SearchColDet: $scope.VoucherSearchOptions[0],
        };        
      $scope.confirmMSG = {
            Accept: false,
            Decline: false,
            Delete: false,
            Modify: false,
            Print: false,
            Reset: false
        };
        $scope.mandetoryFields = {};
        $scope.VoucherTypeColl = [];
        $scope.CostClassColl = [];
        $scope.NarrationList = [];
        $scope.SelectedVoucher = null;
        $scope.SelectedCostClass = null;
        $scope.Config = {};

        $scope.HideShow = {
            VoucherType: false,
            CostClass: false,
            AutoVoucherNo: false,
            Agent: true,
            Currency: true,
            RefNo: true,
        }

        $scope.beData =
        {
            VoucherId: null,
            CostClassId: null,
            TranId: 0,
            AutoManualNo: '',
            AutoVoucherNo: 0,
            CurRate: 1,
            AttechFiles: [],
            SubTotal: 0,
            Total: 0,
            Narration: '',
            VoucherDate: new Date(),
            VoucherDate_TMP: new Date(),
            LedgerAllocationColl: [],
            UDFFeildsColl: [],
            Mode: 'Save'
        };

        $scope.beData.LedgerAllocationColl.push(
            {
                LedgerId: 0,
                AgentId: 0,
                LFNO: '',
                Narration: '',
                DrAmount: 0,
                CrAmount: 0,
                Attributes: '',
                ForBranchId: null
            }
        );
        $('.hideSideBar').on('focus', function (e) {
            $('#sidebarzz').removeClass();
            $('#sidebarzz').addClass('order-last float-right active');
        })


        if (VoucherType) {

            $http({
                method: 'GET',
                url: base_url + "Setup/Security/GetConfirmationMSG",
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.confirmMSG = res.data.Data;
                } else {
                    Swal.fire(res.data.ResponseMSG);
                }

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

            $http({
                method: 'GET',
                url: base_url + "Account/Creation/GetVoucherWiseNarration?voucherType=" + VoucherType,
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.NarrationList = res.data.Data;
                } else
                    Swal.fire(res.data.ResponseMSG);
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });


            $http({
                method: 'GET',
                url: base_url + "Setup/Security/GetAccountConfig",
                dataType: "json"
            }).then(function (res1) {
                if (res1.data.IsSuccess && res1.data.Data) {
                    $scope.Config = res1.data.Data;

                    $timeout(function () {
                        $scope.$apply(function () {

                            //if ($scope.Config.AllowSchamePer == true)
                            //    $scope.HideShow.SchemePer = false;
                            //else
                            //    $scope.HideShow.SchemePer = true;


                        });
                    });
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

            $http({
                method: 'GET',
                url: base_url + "Account/Creation/GetVoucherList?voucherType=" + VoucherType,
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.VoucherTypeColl = res.data.Data;

                    $http({
                        method: 'GET',
                        url: base_url + "Account/Creation/GetCostClassForEntry",
                        dataType: "json"
                    }).then(function (res1) {
                        if (res1.data.IsSuccess && res1.data.Data) {
                            $scope.CostClassColl = res1.data.Data;

                            $timeout(function () {
                                $scope.$apply(function () {
                                    if ($scope.VoucherTypeColl.length > 0) {
                                        $scope.SelectedVoucher = $scope.VoucherTypeColl[0];
                                        $scope.beData.VoucherId = $scope.SelectedVoucher.VoucherId;
                                    }

                                    if ($scope.CostClassColl.length > 0) {
                                        $scope.SelectedCostClass = $scope.CostClassColl[0];
                                        $scope.beData.CostClassId = $scope.SelectedCostClass.CostClassId;
                                    }

                                    if ($scope.VoucherTypeColl.length <= 1)
                                        $scope.HideShow.VoucherType = true;
                                    else
                                        $scope.HideShow.VoucherType = false;

                                    if ($scope.CostClassColl.length <= 1)
                                        $scope.HideShow.CostClass = true;
                                    else
                                        $scope.HideShow.CostClass = false;

                                    $scope.getVoucherNo();

                                    $timeout(function () {

                                        if (TranId && TranId > 0) {
                                            var para = {
                                                tranId: TranId
                                            };
                                            $http({
                                                method: 'POST',
                                                url: base_url + "Account/Transaction/GetContraById",
                                                dataType: "json",
                                                data: JSON.stringify(para)
                                            }).then(function (res) {
                                                $timeout(function () {
                                                    if (res.data.IsSuccess && res.data.Data) {
                                                        var tran = res.data.Data;
                                                        $scope.SetData(tran);
                                                    }
                                                });
                                            }, function (reason) {
                                                Swal.fire('Failed' + reason);
                                            });
                                        }
                                    });

                                });
                            });


                        }
                    }, function (reason) {
                        Swal.fire('Failed' + reason);
                    });

                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        }

    }

    $scope.getVoucherNoOnly = function () {

        var isModify = ($scope.beData.TranId > 0 ? true : false);

        if ($scope.SelectedVoucher && isModify == false) {

            if ($scope.beData.VoucherId && $scope.beData.VoucherId > 0) {
                if ($scope.beData.CostClassId && $scope.beData.CostClassId > 0) {
                    var para = {
                        voucherId: $scope.beData.VoucherId,
                        costClassId: $scope.beData.CostClassId,
                        voucherDate: $scope.beData.VoucherDateDet ? ($filter('date')(new Date($scope.beData.VoucherDateDet.dateAD), 'yyyy-MM-dd')) : ($filter('date')(new Date(), 'yyyy-MM-dd'))
                    };

                    $http({
                        method: 'POST',
                        url: base_url + "Account/Creation/GetVoucherNo",
                        dataType: "json",
                        data: JSON.stringify(para)
                    }).then(function (res) {
                        if (res.data.IsSuccess && res.data.Data) {
                            var vDet = res.data.Data;
                            $scope.beData.AutoManualNo = vDet.AutoManualNo;
                            $scope.beData.AutoVoucherNo = vDet.AutoVoucherNo;

                        } else {
                            Swal.fire(res.data.ResponseMSG);
                        }
                    }, function (reason) {
                        Swal.fire('Failed' + reason);
                    });
                }
            } else {
                $scope.beData.AutoManualNo = '';
                $scope.beData.AutoVoucherNo = 0;
            }

        }
    }

    $scope.getVoucherNo = function () {

        if ($scope.beData.VoucherId > 0)
            $scope.SelectedVoucher = mx($scope.VoucherTypeColl).firstOrDefault(p1 => p1.VoucherId == $scope.beData.VoucherId);

        if ($scope.beData.CostClassId > 0)
            $scope.SelectedCostClass = mx($scope.CostClassColl).firstOrDefault(p1 => p1.CostClassId == $scope.beData.CostClassId);

        if ($scope.SelectedVoucher) {

            $scope.loadingstatus = "running";
            showPleaseWait();

            $timeout(function () {
                $scope.$apply(function () {
                    $scope.SelectedVoucher.VoucherId = $scope.SelectedVoucher.VoucherId;
                });
            });

            $http({
                method: 'GET',
                url: base_url + "Account/Creation/GetVoucherModeById?voucherId=" + $scope.SelectedVoucher.VoucherId,
                dataType: "json"
            }).then(function (res) {

                $scope.loadingstatus = "stop";
                hidePleaseWait();

                if (res.data.IsSuccess && res.data.Data) {
                    $scope.SelectedVoucher = res.data.Data;

                    $timeout(function () {
                        $scope.$apply(function () {
                            if ($scope.SelectedVoucher) {

                                $scope.beData.VoucherDate_TMP = new Date($scope.SelectedVoucher.VoucherDate);
                                $scope.SelectedVoucher.ActiveUDF = false;

                                if ($scope.SelectedVoucher.VoucherUDFColl && $scope.SelectedVoucher.VoucherUDFColl.length > 0) {
                                    $scope.beData.UDFFeildsColl = [];
                                    $scope.SelectedVoucher.ActiveUDF = true;
                                    angular.forEach($scope.SelectedVoucher.VoucherUDFColl, function (udf) {
                                        var ud = {
                                            SNo: udf.SNo,
                                            Name: udf.Label,
                                            Value: udf.DefaultValue,
                                            FieldNo: udf.SNo,
                                            DisplayName: udf.Label,
                                            FieldType: udf.FieldType,
                                            IsMandatory: udf.IsMandatory,
                                            Length: 100,
                                            SelectOptions: udf.DropDownList,
                                            FieldAfter: udf.FieldAfter,
                                            NameId: udf.Name,
                                            Source: udf.Source,
                                            Formula: udf.Formula,
                                            UDFValue: udf.DefaultValue,
                                        };
                                        $scope.beData.UDFFeildsColl.push(ud);
                                    });
                                }

                                if ($scope.SelectedVoucher.VoucherProductUDFColl && $scope.SelectedVoucher.VoucherProductUDFColl.length > 0) {
                                    angular.forEach($scope.beData.LedgerAllocationColl, function (det) {
                                        det.UDFFeildsColl = [];
                                        angular.forEach($scope.SelectedVoucher.VoucherProductUDFColl, function (udf) {

                                            var ud = {
                                                SNo: udf.SNo,
                                                Name: udf.Label,
                                                Value: udf.DefaultValue,
                                                FieldNo: udf.SNo,
                                                DisplayName: udf.Label,
                                                FieldType: udf.FieldType,
                                                IsMandatory: udf.IsMandatory,
                                                Length: 100,
                                                SelectOptions: udf.DropDownList,
                                                FieldAfter: udf.FieldAfter,
                                                NameId: udf.Name,
                                                Source: udf.Source,
                                                Formula: udf.Formula,
                                                UDFValue: udf.DefaultValue,
                                            };

                                            det.UDFFeildsColl.push(ud);
                                        });
                                    });
                                }

                                if ($scope.SelectedVoucher.NumberingMethod == 1)
                                    $scope.HideShow.AutoVoucherNo = false;
                                else
                                    $scope.HideShow.AutoVoucherNo = true;

                                if ($scope.SelectedVoucher.UseRefNo == true)
                                    $scope.HideShow.RefNo = false;
                                else
                                    $scope.HideShow.RefNo = true;

                            }


                        });
                    });

                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        }

        if ($scope.beData.VoucherId && $scope.beData.VoucherId > 0) {
            if ($scope.beData.CostClassId && $scope.beData.CostClassId > 0) {
                var para = {
                    voucherId: $scope.beData.VoucherId,
                    costClassId: $scope.beData.CostClassId,
                    voucherDate: $scope.beData.VoucherDateDet ? ($filter('date')(new Date($scope.beData.VoucherDateDet.dateAD), 'yyyy-MM-dd')) : ($filter('date')(new Date(), 'yyyy-MM-dd'))
                };

                $http({
                    method: 'POST',
                    url: base_url + "Account/Creation/GetVoucherNo",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    if (res.data.IsSuccess && res.data.Data) {
                        var vDet = res.data.Data;
                        $scope.beData.AutoManualNo = vDet.AutoManualNo;
                        $scope.beData.AutoVoucherNo = vDet.AutoVoucherNo;

                        $timeout(function () {
                            GlobalServices.getCurrentDateTime().then(function (res) {
                                var curDate = res.data.Data;
                                if (curDate) {
                                    $scope.beData.VoucherDate_TMP = new Date(curDate);

                                    if ($scope.SelectedVoucher) {
                                        if ($scope.SelectedVoucher.VoucherDateAs == 2) {
                                            GlobalServices.getLastEntryDate($scope.SelectedVoucher.VoucherId).then(function (res) {
                                                var curDate = res.data.Data;
                                                if (curDate) {
                                                    $scope.beData.VoucherDate_TMP = new Date(curDate);
                                                }
                                            }, function (errormessage) {
                                                alert('Unable to Delete data. pls try again.' + errormessage.responseText);
                                            });
                                        }
                                    }

                                }
                            }, function (errormessage) {
                                alert('Unable to Delete data. pls try again.' + errormessage.responseText);
                            });
                        });

                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        } else {
            $scope.beData.AutoManualNo = '';
            $scope.beData.AutoVoucherNo = 0;
        }

    }
    $scope.AddRowInLedgerAllocation = function (ind, boolAuto) {


        var udfColl = [];
        if ($scope.SelectedVoucher.VoucherProductUDFColl && $scope.SelectedVoucher.VoucherProductUDFColl.length > 0) {
            angular.forEach($scope.SelectedVoucher.VoucherProductUDFColl, function (udf) {
                var ud = {
                    SNo: udf.SNo,
                    Name: udf.Label,
                    Value: udf.DefaultValue,
                    FieldNo: udf.SNo,
                    DisplayName: udf.Label,
                    FieldType: udf.FieldType,
                    IsMandatory: udf.IsMandatory,
                    Length: 100,
                    SelectOptions: udf.DropDownList,
                    FieldAfter: udf.FieldAfter,
                    NameId: udf.Name,
                    Source: udf.Source,
                    Formula: udf.Formula,
                    UDFValue: udf.DefaultValue,
                };
                udfColl.push(ud);
            });
        }

        if (boolAuto == true) {
            var len = $scope.beData.LedgerAllocationColl.length;
            if ((ind + 1) != len)
                return;

            var selectItem = $scope.beData.LedgerAllocationColl[ind];
            if (!selectItem.LedgerId || selectItem.LedgerId == null || selectItem.LedgerId == 0 || selectItem.Amount == 0)
                return;

        }

        if ($scope.beData.LedgerAllocationColl) {
            if ($scope.beData.LedgerAllocationColl.length > ind + 1) {
                $scope.beData.LedgerAllocationColl.splice(ind + 1, 0, {
                    LedgerId: 0,
                    AgentId: 0,
                    LFNO: '',
                    Narration: '',
                    DrAmount: 0,
                    CrAmount: 0,
                    ForBranchId: null,
                    UDFFeildsColl: udfColl,
                })
            } else {
                $scope.beData.LedgerAllocationColl.push({
                    LedgerId: 0,
                    AgentId: 0,
                    LFNO: '',
                    Narration: '',
                    DrAmount: 0,
                    CrAmount: 0,
                    ForBranchId: null,
                    UDFFeildsColl: udfColl,
                })
            }
        }

    }

    $scope.delRowLedgerAllocation = function (ind) {
        if ($scope.beData.LedgerAllocationColl) {
            if ($scope.beData.LedgerAllocationColl.length > 1) {
                $scope.beData.LedgerAllocationColl.splice(ind, 1);
                $scope.ChangeCrAmount();
            }
        }
    }

    $scope.ShowSideBar = function (paraData) {
        $scope.sideBarData = paraData;

        if (paraData) {
            if (paraData.length > 0) {
                if (paraData[0].text == "Currency") {

                }
            }
        }
    };


    $scope.ChangeParticularLedger = function (ledDet) {


        $scope.sideBarData = ledDet.partySideBarData;

        $timeout(function () {
            $scope.CurLedgerAllocation = ledDet;
            if (ledDet) {
                ledDet.CurrentBal = 0;
                if (ledDet.LedgerId && ledDet.LedgerId > 0 && ledDet.LedgerDetails) {
                    ledDet.CurrentBal = ledDet.LedgerDetails.Closing;

                    $timeout(function () {
                        if (ledDet.LedgerDetails && ledDet.LedgerDetails.CostCentersAreApplied == true) {
                            if (!ledDet.CostCenterColl)
                                ledDet.CostCenterColl = [];

                            if (ledDet.CostCenterColl.length == 0) {
                                ledDet.CostCenterColl.push({
                                    CostCenterId: null,
                                    DrCr: 1,
                                    Amount: 0
                                });
                            }
                            $('#frmCostCentersModel').modal('show');
                        } else
                            ledDet.CostCenterColl = [];
                    });

                    $timeout(function () {
                        if (ledDet.LedgerDetails && ledDet.LedgerDetails.InventoryValuesAreAffected == true) {
                            if (!ledDet.ItemDetailsCOll)
                                ledDet.ItemDetailsCOll = [];

                            if (ledDet.ItemDetailsCOll.length == 0) {
                                ledDet.InOut = 1;
                                ledDet.ItemDetailsCOll.push({
                                    ProductId: null,
                                    Qty: 0,
                                });
                            }
                            $('#itemdetail').modal('show');
                        } else
                            ledDet.ItemDetailsCOll = [];
                    });


                    $timeout(function () {
                        if (ledDet.LedgerDetails && (ledDet.LedgerDetails.IsTDS == true || ledDet.LedgerDetails.IsVat == true)) {
                            if (!ledDet.TDSVatDetailColl)
                                ledDet.TDSVatDetailColl = [];

                            if (ledDet.TDSVatDetailColl.length == 0) {
                                ledDet.TDSVatDetailColl.push({
                                    SNO: 0,
                                    BillNo: '',
                                    Amount: 0,
                                    Rate: 0,
                                    Payment: 0
                                });
                            }
                            $('#tds').modal('show');
                        } else
                            ledDet.ItemDetailsCOll = [];
                    });

                    $timeout(function () {
                        if (ledDet.LedgerDetails && ledDet.LedgerDetails.ActiveChequeDetails == true) {
                            if (!ledDet.CheckDetails) {
                                ledDet.CheckDetails = {
                                    ChequeNo: '',
                                    AccountNo: '',
                                    Remarks: '',
                                    CheckType: 0,
                                    ChequeDate_TMP: new Date()
                                };
                            }


                            $('#chequedetail').modal('show');
                        } else
                            ledDet.ItemDetailsCOll = [];
                    });
                }
            }
        });


        ledDet.CurrentBal = 0;
        if (ledDet.PartyLedger && ledDet.LedgerId && ledDet.LedgerId > 0)
            ledDet.CurrentBal = ledDet.PartyLedger.Closing;

        console.log(ledDet);
    }

    $scope.SaveContra = function () {

        if ($scope.IsValidData() == true) {


            var saveModify = $scope.beData.TranId > 0 ? 'Modify' : 'Save';
            Swal.fire({
                title: 'Do you want to ' + saveModify + ' the current data?',
                showCancelButton: true,
                confirmButtonText: saveModify,
            }).then((result) => {
                /* Read more about isConfirmed, isDenied below */
                if (result.isConfirmed) {
                    $scope.loadingstatus = "running";
                    showPleaseWait();

                    $timeout(function () {
                        var filesColl = $scope.beData.AttechFiles;
                        $scope.beData.AttechFiles = [];

                        $http({
                            method: 'POST',
                            url: base_url + "Account/Transaction/SaveUpdateContra",
                            headers: { 'Content-Type': undefined },

                            transformRequest: function (data) {

                                var formData = new FormData();
                                formData.append("jsonData", angular.toJson(data.jsonData));

                                if (data.files) {
                                    for (var i = 0; i < data.files.length; i++) {
                                        formData.append("file" + i, data.files[i]);
                                    }
                                }

                                return formData;
                            },
                            data: { jsonData: $scope.GetData(), files: filesColl }
                        }).then(function (res) {

                            $scope.loadingstatus = "stop";
                            hidePleaseWait();

                            Swal.fire(res.data.ResponseMSG);

                            if (res.data.IsSuccess == true) {
                                $scope.lastTranId = res.data.Data.RId;
                                $scope.lastVoucherId = $scope.SelectedVoucher.VoucherId;

                                if ($scope.SelectedVoucher.PrintVoucherAfterSaving == true)
                                    $scope.Print();

                                $scope.ClearData();
                            }

                        }, function (errormessage) {
                            hidePleaseWait();
                            $scope.loadingstatus = "stop";

                        });
                    });
                 
                }
            });

        }

    }

    $scope.GetTransactionById = function (tran) {
        $timeout(function () {

            if (tran.TranId && tran.TranId > 0) {
                var para = {
                    tranId: tran.TranId
                };
                $scope.ClearData();

                $timeout(function () {
                    $http({
                        method: 'POST',
                        url: base_url + "Account/Transaction/GetContraById",
                        dataType: "json",
                        data: JSON.stringify(para)
                    }).then(function (res) {
                        $timeout(function () {
                            if (res.data.IsSuccess && res.data.Data) {
                                var tran = res.data.Data;
                                $scope.SetData(tran);
                                $('#searVoucherRightBtn').modal('hide');
                            } else
                                Swal.fire(res.data.ResponseMSG);
                        });
                    }, function (reason) {
                        Swal.fire('Failed' + reason);
                    });
                });
               
            }
        });
    }

    $scope.DelTransactionById = function (tran) {
        Swal.fire({
            title: 'Are you sure you want to delete selected transaction ' + tran.VoucherNo + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
            //message: 'Are you sure to delete selected Branch :-' + beData.Name,
        }).then((result) => {
            if (result.isConfirmed) {
                $scope.loadingstatus = "running";
                showPleaseWait();
                var para = {
                    voucherType: VoucherType,
                    voucherId: tran.VoucherId,
                    tranId: tran.TranId
                };
                $http({
                    method: 'POST',
                    url: base_url + "Global/DelAccInvTransaction",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess == true) {
                        $scope.ClearData();
                        $scope.ReSearchData(-1);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);

                });
            }

        });
    }

    $scope.IsValidData = function () {
        var result = true;

        if ($scope.beData.VoucherId) {
            if ($scope.beData.VoucherId == null || $scope.beData.VoucherId == 0) {
                result = false;
                Swal.fire('Please ! Select Valid Voucher Name');
            }
        } else {
            result = false;
            Swal.fire('Please ! Select Valid Voucher Name');
        }

        if ($scope.beData.CostClassId) {
            if ($scope.beData.CostClassId == null || $scope.beData.CostClassId == 0) {
                result = false;
                Swal.fire('Please ! Select Valid CostClass Name');
            }
        } else {
            result = false;
            Swal.fire('Please ! Select Valid CostClass Name');
        }

        if ($scope.beData.CashBankLedgerId) {
            if ($scope.beData.CashBankLedgerId == null || $scope.beData.CashBankLedgerId == 0) {
                result = false;
                Swal.fire('Please ! Select Valid Cash/Bank Name');
            }
        } else {
            result = false;
            Swal.fire('Please ! Select Valid Cash/Bank Name');
        }

        return result;
    }

    $scope.GetData = function () {

        var vDate = new Date();
        if ($scope.beData.VoucherDateDet) {
            vDate = $filter('date')(new Date($scope.beData.VoucherDateDet.dateAD), 'yyyy-MM-dd');
        }

        var eDate = new Date();
        if ($scope.beData.EntryDateDet) {
            eDate = $filter('date')(new Date($scope.beData.EntryDateDet.dateAD), 'yyyy-MM-dd');
        } else
            eDate = $filter('date')(new Date(), 'yyyy-MM-dd');

        var tmpContra = {
            TranId: $scope.beData.TranId,
            VoucherId: $scope.beData.VoucherId,
            CostClassId: $scope.beData.CostClassId,
            AutoVoucherNo: $scope.beData.AutoVoucherNo,
            CurRate: $scope.beData.CurRate,
            CurrencyId: $scope.beData.CurrencyId,
            ManualVoucherNO: $scope.beData.ManualVoucherNO,
            Narration: $scope.beData.Narration,
            VoucherDate: vDate,
            RefNo: $scope.beData.RefNo,
            AutoManualNo: $scope.beData.AutoManualNo,
            EntryDate: eDate,
            BranchId: ($scope.beData.BranchId ? $scope.beData.BranchId : 0),
            LedgerAllocationColl: [],
            DocumentColl: $scope.beData.DocumentColl,
        };

        var voucherUDFFields = [];
        var voucherKeyVal = {};
        angular.forEach($scope.beData.UDFFeildsColl, function (udf) {

            if (udf.NameId && udf.NameId.length > 0) {
                if (udf.FieldType == 2) {
                    var ud = {
                        SNo: (udf.FieldNo ? udf.FieldNo : udf.SNo),
                        Name: udf.Name,
                        Value: udf.UDFValueDet ? $filter('date')(udf.UDFValueDet.dateAD, 'yyyy-MM-dd') : '',
                        AlValue: udf.UDFValueDet ? udf.UDFValueDet.dateBS : '',
                    };
                    voucherUDFFields.push(ud);
                    voucherKeyVal[udf.NameId] = udf.UDFValueDet ? udf.UDFValueDet.dateBS : '';
                } else if (udf.FieldType == 3 && udf.Source && udf.Source.length > 0) {
                    var ud = {
                        SNo: (udf.FieldNo ? udf.FieldNo : udf.SNo),
                        Name: udf.Name,
                        Value: udf.UDFValue,
                        AlValue: udf.UDFValueDet ? udf.UDFValueDet.text : '',
                    };
                    voucherUDFFields.push(ud);
                    voucherKeyVal[udf.NameId] = udf.UDFValueDet ? udf.UDFValueDet.text : ''
                }
                else {
                    var ud = {
                        SNo: (udf.FieldNo ? udf.FieldNo : udf.SNo),
                        Name: udf.Name,
                        Value: udf.UDFValue
                    };
                    voucherUDFFields.push(ud);
                    voucherKeyVal[udf.NameId] = udf.UDFValue;
                }
            }

        });
        if (voucherUDFFields.length > 0) {
            tmpContra.Attributes = JSON.stringify(voucherUDFFields);
            tmpContra.UDFKeyVal = JSON.stringify(voucherKeyVal);
        } else {
            tmpContra.Attributes = "";
            tmpContra.UDFKeyVal = "";
        }

        var drLedAllocation = {
            DrCr: 1,
            LedgerId: $scope.beData.CashBankLedgerId,
            AgentId: 0,
            LFNO: '',
            Narration: '',
            DrAmount: mx($scope.beData.LedgerAllocationColl).sum(p1 => p1.CrAmount),
            CrAmount: 0,
            ForBranchId: null
        };
        tmpContra.LedgerAllocationColl.push(drLedAllocation);

        angular.forEach($scope.beData.LedgerAllocationColl, function (ledA) {
            if (ledA.LedgerId && ledA.LedgerId > 0) {

                var udfValues = '';
                var udfFields = [];
                var itemKeyVal = {};
                angular.forEach(ledA.UDFFeildsColl, function (udf) {

                    if (udf.NameId && udf.NameId.length > 0) {
                        var ud = {};
                        ud.SNo = (udf.FieldNo ? udf.FieldNo : udf.SNo);
                        ud.Name = udf.Name;
                        if (udf.FieldType == 2) {
                            ud.Value = udf.UDFValueDet ? $filter('date')(udf.UDFValueDet.dateAD, 'yyyy-MM-dd') : '';
                            ud.AlValue = udf.UDFValueDet ? udf.UDFValueDet.dateBS : '';
                            itemKeyVal[udf.NameId] = udf.UDFValueDet ? udf.UDFValueDet.dateBS : '';
                        }
                        else if (udf.FieldType == 3 && udf.Source && udf.Source.length > 0) {
                            ud.Value = udf.UDFValue;
                            ud.AlValue = udf.UDFValueDet ? udf.UDFValueDet.text : '';
                            itemKeyVal[udf.NameId] = udf.UDFValueDet ? udf.UDFValueDet.text : ''
                        }
                        else {
                            ud.Value = udf.UDFValue;
                            itemKeyVal[udf.NameId] = udf.UDFValue;
                        }

                        if (udf.IsManual == true)
                            ud.IsManual = true;

                        udfFields.push(ud);
                    }

                });
                if (udfFields.length > 0) {
                    udfValues = JSON.stringify(udfFields);
                    itemKeyVal = JSON.stringify(itemKeyVal);
                } else {
                    udfValues = "";
                    itemKeyVal = "";
                }


                var crLedAllocation = {
                    UDFKeyVal: itemKeyVal,
                    Attributes: udfValues,
                    DrCr: 2,
                    LedgerId: ledA.LedgerId,
                    AgentId: ledA.AgentId,
                    LFNO: ledA.LFNO,
                    Narration: ledA.Narration,
                    DrAmount: 0,
                    CrAmount: ledA.CrAmount,
                    ForBranchId: null
                };
                tmpContra.LedgerAllocationColl.push(crLedAllocation);
            }
        });


        return tmpContra;
    };

    $scope.SetData = function (tran) {

        $scope.beData.VoucherDateDet = null;
        $scope.beData.VoucherDate = new Date(tran.VoucherDate);
        $scope.beData.VoucherDate_TMP = new Date(tran.VoucherDate);
        $scope.beData.VoucherDateAD_TMP = new Date(tran.VoucherDate);
        $scope.beData.VoucherDateDet = GlobalServices.getDateDet(tran.VoucherDate);
        setTimeout(1000);

        $scope.beData.TranId = tran.TranId;

        $scope.beData.VoucherId = tran.VoucherId;
        $scope.beData.CostClassId = tran.CostClassId;
        $scope.beData.AutoVoucherNo = tran.AutoVoucherNo;
        $scope.beData.CurRate = tran.CurRate;
        $scope.beData.CurrencyId = tran.CurrencyId;
        $scope.beData.ManualVoucherNO = tran.ManualVoucherNO;
        $scope.beData.Narration = tran.Narration;
        $scope.beData.RefNo = tran.RefNo;
        $scope.beData.AutoManualNo = tran.AutoManualNo;
        $scope.beData.EntryDate = new Date(tran.VoucherDate);
        $scope.beData.BranchId = tran.BranchId;
        $scope.beData.LedgerAllocationColl = [];
        $scope.beData.DocumentColl = tran.DocumentColl;


        var voucherUdfColl = [];
        if ($scope.SelectedVoucher.VoucherUDFColl && $scope.SelectedVoucher.VoucherUDFColl.length > 0) {
            angular.forEach($scope.SelectedVoucher.VoucherUDFColl, function (udf) {
                var ud = {
                    SNo: udf.SNo,
                    Name: udf.Label,
                    Value: udf.DefaultValue,
                    FieldNo: udf.SNo,
                    DisplayName: udf.Label,
                    FieldType: udf.FieldType,
                    IsMandatory: udf.IsMandatory,
                    Length: 100,
                    SelectOptions: udf.DropDownList,
                    FieldAfter: udf.FieldAfter,
                    NameId: udf.Name,
                    Source: udf.Source,
                    Formula: udf.Formula
                };
                voucherUdfColl.push(ud);
            });
        }
        $scope.beData.UDFFeildsColl = voucherUdfColl;
        if (tran.Attributes && tran.Attributes.length > 0) {
            var udfFieldsColl = mx(JSON.parse(tran.Attributes));
            angular.forEach($scope.beData.UDFFeildsColl, function (udd) {
                var findU = udfFieldsColl.firstOrDefault(p1 => p1.SNo == udd.SNo);
                if (findU) {
                    if (udd.FieldType == 2) {
                        if (findU.Value) {
                            udd.UDFValue_TMP = new Date(findU.Value);
                        }
                    } else if (udd.FieldType == 4) {
                        if (findU.Value) {
                            udd.UDFValue = parseInt(findU.Value);
                        }
                    }
                    else
                        udd.UDFValue = findU.Value;
                }
            });
        }


        var udfColl = [];
        if ($scope.SelectedVoucher.VoucherProductUDFColl && $scope.SelectedVoucher.VoucherProductUDFColl.length > 0) {
            angular.forEach($scope.SelectedVoucher.VoucherProductUDFColl, function (udf) {

               
                var ud = {
                    SNo: udf.SNo,
                    Name: udf.Label,
                    Value: udf.DefaultValue,
                    FieldNo: udf.SNo,
                    DisplayName: udf.Label,
                    FieldType: udf.FieldType,
                    IsMandatory: udf.IsMandatory,
                    Length: 100,
                    SelectOptions: udf.DropDownList,
                    FieldAfter: udf.FieldAfter,
                    NameId: udf.Name,
                    Source: udf.Source,
                    Formula: udf.Formula
                };
                udfColl.push(ud);
            });
        }


        var drLedAllocation = mx(tran.LedgerAllocationColl).firstOrDefault(p1 => p1.DrCr == 1);
        if (drLedAllocation) {
            $scope.beData.CashBankLedgerId = drLedAllocation.LedgerId;
        }
        angular.forEach(tran.LedgerAllocationColl, function (ledAll) {

            //ledAll.UDFFeildsColl = udfColl;
            ledAll.UDFFeildsColl = [];
            angular.forEach(udfColl, function (uc) {
                ledAll.UDFFeildsColl.push({
                    SNo: uc.SNo,
                    Name: uc.Name,
                    Value: uc.Value,
                    FieldNo: uc.SNo,
                    DisplayName: uc.DisplayName,
                    FieldType: uc.FieldType,
                    IsMandatory: uc.IsMandatory,
                    Length: 100,
                    SelectOptions: uc.SelectOptions,
                    FieldAfter: uc.FieldAfter,
                    NameId: uc.NameId,
                    Source: uc.Source,
                    Formula: uc.Formula
                });
            });
 

            if (ledAll.Attributes && ledAll.Attributes.length > 0) {
                var udfFieldsColl = mx(JSON.parse(ledAll.Attributes));
                angular.forEach(ledAll.UDFFeildsColl, function (udd) {
                    var findU = udfFieldsColl.firstOrDefault(p1 => p1.SNo == udd.SNo);
                    if (findU) {
                        if (udd.FieldType == 2) {
                            if (findU.Value) {
                                udd.UDFValue_TMP = new Date(findU.Value);
                            }
                        } else if (udd.FieldType == 4) {
                            if (findU.Value) {
                                udd.UDFValue_TMP = parseInt(findU.Value);
                            }
                        }
                        else
                            udd.UDFValue = findU.Value;
                    }
                });
            }

            if (ledAll.DrCr == 2)
                $scope.beData.LedgerAllocationColl.push(ledAll);

            angular.forEach(ledAll.CostCenterColl, function (cc) {
                if (cc.DrAmount > 0)
                    cc.Amount = cc.DrAmount;
                else if (cc.CrAmount > 0)
                    cc.Amount = cc.CrAmount;
            });

        });

        $scope.ChangeCrAmount();
    };

    $scope.ChangeCrAmount = function (curLA) {
        var total = 0;
        angular.forEach($scope.beData.LedgerAllocationColl, function (led) {
            total += led.CrAmount;
        });

        $scope.beData.Amount = total;

        GlobalServices.getLAUDFFormula(curLA, $scope.beData.LedgerAllocationColl);
    };


    $scope.ClearData = function () {
        var sV = $scope.SelectedVoucher;
        var sC = $scope.SelectedCostClass;

        $scope.SelectedVoucher = null;
        $scope.SelectedCostClass = null;

        $scope.CurLedgerAllocation = {};

        $scope.beData =
        {
            VoucherId: null,
            CostClassId: null,
            TranId: 0,
            AutoManualNo: '',
            AutoVoucherNo: 0,
            CurRate: 1,
            AttechFiles: [],
            SubTotal: 0,
            Total: 0,
            Narration: '',
            VoucherDate: new Date(),
            VoucherDate_TMP: new Date(),
            LedgerAllocationColl: [],
            UDFFeildsColl: [],
            Mode: 'Save'
        };

        $scope.beData.LedgerAllocationColl.push(
            {
                LedgerId: 0,
                AgentId: 0,
                LFNO: '',
                Narration: '',
                DrAmount: 0,
                CrAmount: 0,
                ForBranchId: null
            }
        );

        if ($scope.VoucherTypeColl.length > 0) {
            $scope.SelectedVoucher = $scope.VoucherTypeColl[0];
            $scope.beData.VoucherId = $scope.SelectedVoucher.VoucherId;
        }

        if ($scope.CostClassColl.length > 0) {
            $scope.SelectedCostClass = $scope.CostClassColl[0];
            $scope.beData.CostClassId = $scope.SelectedCostClass.CostClassId;
        }

        if (sV) {
            $scope.SelectedVoucher = sV;
            $scope.beData.VoucherId = sV.VoucherId;
        }

        if (sC) {
            $scope.SelectedCostClass = sC;
            $scope.beData.CostClassId = sC.CostClassId;
        }


        $('input[type=file]').val('');

        $timeout(function () {
            $scope.getVoucherNo();
        });

        //$timeout(function () {
        //    GlobalServices.getCurrentDateTime().then(function (res) {
        //        var curDate = res.data.Data;
        //        if (curDate) {
        //            $scope.beData.VoucherDate_TMP = new Date(curDate);
        //        }
        //    }, function (errormessage) {
        //        alert('Unable to Delete data. pls try again.' + errormessage.responseText);
        //    });
        //});

    };

    $scope.Clear = function () {
        Swal.fire({
            title: 'Are you sure?',
            text: " clear current data !",
            icon: 'info',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes !'

        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                $scope.ClearData();
            }
        });
    };
    
    $scope.SearchDataColl = [];
    $scope.SearchData = function () {

        $scope.loadingstatus = 'running';
        showPleaseWait();
        $scope.paginationOptions.TotalRows = 0;

        var sCol = $scope.paginationOptions.SearchColDet;

        var para = {
            voucherId: ($scope.SelectedVoucher ? $scope.SelectedVoucher.VoucherId : null),
            costClassId: ($scope.SelectedCostClass ? $scope.SelectedCostClass.CostClassId : null),
            voucherType: VoucherType,
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
            url: base_url + "Inventory/Transaction/GetTransactionLst",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = 'stop';
            hidePleaseWait();

            if (res.data.IsSuccess && res.data.Data) {
                $scope.SearchDataColl = res.data.Data;
                $scope.paginationOptions.TotalRows = res.data.TotalCount;
                $('#searVoucherRightBtn').modal('show');

            } else
                alert(res.data.ResponseMSG);

        }, function (reason) {
            alert('Failed' + reason);
        });


    };
    $scope.PrintVoucher = function (tranId, vid) {
        $scope.lastTranId = tranId;
        $scope.lastVoucherId = vid;
        $scope.Print();
    }
    $scope.Print = function () {
        if ($scope.lastTranId || $scope.lastVoucherId > 0) {
            var TranId = $scope.lastTranId;

            var vId = $scope.lastVoucherId;

            $http({
                method: 'GET',
                url: base_url + "ReportEngine/GetReportTemplates?entityId=" + EntityId + "&voucherId=" + vId + "&isTran=true",
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    var templatesColl = res.data.Data;
                    if (templatesColl && templatesColl.length > 0) {
                        var templatesName = [];
                        var sno = 1;
                        angular.forEach(templatesColl, function (tc) {
                            templatesName.push(sno + '-' + tc.ReportName);
                            sno++;
                        });
                        var printDone = false;
                        var rptTranId = 0;
                        if (templatesColl.length == 1)
                            rptTranId = templatesColl[0].RptTranId;
                        else {
                            Swal.fire({
                                title: 'Report Templates For Print',
                                input: 'select',
                                inputOptions: templatesName,
                                inputPlaceholder: 'Select a template',
                                showCancelButton: true,
                                inputValidator: (value) => {
                                    return new Promise((resolve) => {
                                        if (value >= 0) {
                                            resolve()
                                            rptTranId = templatesColl[value].RptTranId;
                                            printDone = true;

                                            if (rptTranId > 0) {
                                                document.body.style.cursor = 'wait';
                                                document.getElementById("frmRpt").src = '';
                                                document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=" + $scope.SelectedVoucher.VoucherId + "&tranid=" + TranId + "&vouchertype=" + VoucherType;
                                                document.body.style.cursor = 'default';
                                                $('#FrmPrintReport').modal('show');
                                            }
                                        } else {
                                            resolve('You need to select:)')
                                        }
                                    })
                                }
                            })
                        }

                        if (rptTranId > 0) {
                            document.body.style.cursor = 'wait';
                            document.getElementById("frmRpt").src = '';
                            document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=" + $scope.SelectedVoucher.VoucherId + "&tranid=" + TranId + "&vouchertype=" + VoucherType;
                            document.body.style.cursor = 'default';
                            $('#FrmPrintReport').modal('show');
                        }

                    } else
                        Swal.fire('No Templates found for print');
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

        }


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
            voucherId: ($scope.SelectedVoucher ? $scope.SelectedVoucher.VoucherId : null),
            costClassId: ($scope.SelectedCostClass ? $scope.SelectedCostClass.CostClassId : null),
            voucherType: VoucherType,
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
            url: base_url + "Inventory/Transaction/GetTransactionLst",
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

    $scope.RemoveAttachment = function (fId, ind) {

        if (fId == 1) {
            $scope.beData.DocumentColl.splice(ind, 1);
        }
        else if (fId == 2) {
            $scope.beData.AttechFiles.splice(ind, 1);
        }

    }
    $scope.ValidLedAllocationColl = [];
    $scope.IsValidTran = function () {
        $scope.ValidLedAllocationColl = [];
        if ($scope.IsValidData() == true) {

            $scope.loadingstatus = "running";
            showPleaseWait();

            $http({
                method: 'POST',
                url: base_url + "Global/IsValidVoucher",
                headers: { 'Content-Type': undefined },

                transformRequest: function (data) {

                    var formData = new FormData();
                    formData.append("EntityId", EntityId);
                    formData.append("jsonData", angular.toJson(data.jsonData));
                    return formData;
                },
                data: { jsonData: $scope.GetData() }
            }).then(function (res) {

                $scope.loadingstatus = "stop";
                hidePleaseWait();
                if (res.data.IsSuccess == true) {
                    if (res.data.Data && res.data.Data.length > 0) {
                        $scope.ValidLedAllocationColl = JSON.parse(res.data.Data);
                        $('#frmMDLLedAllocation').modal('show');
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }
                } else {
                    Swal.fire(res.data.ResponseMSG);
                }
            }, function (errormessage) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
            });

        }
    }

    $scope.TranRelation = {};
    $scope.ShowTransactionRelation = function () {

        $scope.TranRelation = {};
        if ($scope.beData.TranId > 0) {

            $scope.loadingstatus = "running";
            showPleaseWait();

            var para = {
                TranId: $scope.beData.TranId,
                VoucherType: VoucherType,
            };

            $http({
                method: 'POST',
                url: base_url + "Global/GetTranRelation",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res1) {
                $scope.loadingstatus = "stop";
                hidePleaseWait();
                if (res1.data.IsSuccess && res1.data.Data) {
                    var tranData = res1.data.Data;
                    if (tranData.length > 0) {
                        $scope.TranRelation.Parent = res1.data.Data[0];
                        $scope.TranRelation.ChieldColl = [];
                        angular.forEach(tranData, function (td) {
                            if (td.LevelId > 1)
                                $scope.TranRelation.ChieldColl.push(td);
                        });

                        $('#frmMDLTranRelation').modal('show');
                    }

                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });


        }
    }
});