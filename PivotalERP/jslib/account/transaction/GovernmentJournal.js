app.controller('GJournalController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'JournalController';
    var glSrv = GlobalServices;
    LoadData();

    $scope.sideBarData = [];

    $scope.lastTranId = 0;
    function LoadData() {

        $('.select2').select2({
            allowClear: true,
            openOnEnter: true
        });

        $scope.GenConfig = {};
        glSrv.getGenConfig().then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.GenConfig = res.data.Data;
            }
        }, function (reason) {
            alert('Failed' + reason);
        });

        $scope.CashPartyColl = [];
        $scope.CashPartyColl_Qry = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Transaction/getCastPartyListForTxn",
            dataType: "json"
            //data:JSON.stringify(para)
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.CashPartyColl = res.data.Data;
                $scope.CashPartyColl_Qry = mx(res.data.Data);
            }
        }, function (reason) {
            alert('Failed' + reason);
        });

        $scope.CostDepartmentColl = [];
        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetAllCostCenterDepartment",
            dataType: "json"
            //data:JSON.stringify(para)
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.CostDepartmentColl = res.data.Data;
            }
        }, function (reason) {
            alert('Failed' + reason);
        });


        $scope.BrandColl = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetAllProductBrand",
            dataType: "json"
            //data:JSON.stringify(para)
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BrandColl = res.data.Data;
            }
        }, function (reason) {
            alert('Failed' + reason);
        });

        $scope.VoucherSearchOptions = [{ text: 'VoucherNo', value: 'TS.AutoVoucherNo', searchType: 'Number' }, { text: 'RefNo', value: 'TS.[RefNo]', searchType: 'text' }, { text: 'VoucherDate', value: 'TS.VoucherDate', searchType: 'date' }, { text: 'Voucher Name', value: 'V.VoucherName', searchType: 'text' }, { text: 'CostClass', value: 'CC.Name', searchType: 'text' },];

        $scope.paginationOptions = {
            pageNumber: 1,
            pageSize: glSrv.getPerPageRow(),
            sort: null,
            SearchType: 'text',
            SearchCol: '',
            SearchVal: '',
            SearchColDet: null,
            pagearray: [],
            pageOptions: [5, 10, 20, 30, 40, 50],
            SearchColDet: $scope.VoucherSearchOptions[0]
        };
       
        $scope.DrCrList = GlobalServices.getDrCr();

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
        $scope.GodownColl = [];

        $scope.InOutColl = [{ id: 1, text: 'IN' }, { id: 0, text: 'OUT' }];

        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetUserWiseGodown",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.GodownColl = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.VatTypeColl = [];
        $http({
            method: 'GET',
            url: base_url + "Global/GetVatTypes",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.VatTypeColl = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.BranchColl = [];
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/GetAllBranchListForEntry",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BranchColl = res.data.Data;
            }
        }, function (reason) {
            alert('Failed' + reason);
        });

        $scope.ChequeTypeColl = [];
        $http({
            method: 'GET',
            url: base_url + "Global/GetChequeTypes",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ChequeTypeColl = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.HideShow = {
            VoucherType: false,
            CostClass: false,
            AutoVoucherNo: false,
            Agent: true,
            Currency: true,
            RefNo: true,
            ProfitCenter1: true,
            ProfitCenter2: true,
            ProfitCenter3: true,
            ProfitCenter4: true,
            ProfitCenter5: true,
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
            Mode: 'Save',
            //New Added on 18 Bhadra
            BudgetNo: 0,
            TranNo:0
        };

        $scope.beData.LedgerAllocationColl.push(
            {
                DrCr: 1,
                LedgerId: 0,
                AgentId: 0,
                LFNO: '',
                Narration: '',
                DrAmount: 0,
                CrAmount: 0,
                ForBranchId: null,
                Dimension1: null,
                Dimension2: null,
                Dimension3: null,
                Dimension4: null,
                Dimension5: null,
                //New Added on 18 Bhadra
                LA_HeadingNo: '',
                LA_ProgramCode: '',
                TransactionType: '',
                LA_Level: '',
                LA_Organization: '',
                LA_PaymentMethod:''
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
                                                url: base_url + "Account/Transaction/GetJournalById",
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

                                if ($scope.SelectedVoucher.ProfitCenter1 == true)
                                    $scope.HideShow.ProfitCenter1 = false;
                                else
                                    $scope.HideShow.ProfitCenter1 = true;

                                if ($scope.SelectedVoucher.ProfitCenter2 == true)
                                    $scope.HideShow.ProfitCenter2 = false;
                                else
                                    $scope.HideShow.ProfitCenter2 = true;

                                if ($scope.SelectedVoucher.ProfitCenter3 == true)
                                    $scope.HideShow.ProfitCenter3 = false;
                                else
                                    $scope.HideShow.ProfitCenter3 = true;

                                if ($scope.SelectedVoucher.ProfitCenter4 == true)
                                    $scope.HideShow.ProfitCenter4 = false;
                                else
                                    $scope.HideShow.ProfitCenter4 = true;

                                if ($scope.SelectedVoucher.ProfitCenter5 == true)
                                    $scope.HideShow.ProfitCenter5 = false;
                                else
                                    $scope.HideShow.ProfitCenter5 = true;


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
            if (!selectItem.LedgerId || selectItem.LedgerId == null || selectItem.LedgerId == 0 || (selectItem.DrAmount == 0 && selectItem.CrAmount == 0))
                return;

        }

        var allocationQuery = mx($scope.beData.LedgerAllocationColl);
        var drAmt = allocationQuery.sum(p1 => p1.DrAmount);
        var crAmt = allocationQuery.sum(p1 => p1.CrAmount);
        var clAmt = drAmt - crAmt;

        if ($scope.beData.LedgerAllocationColl) {
            if ($scope.beData.LedgerAllocationColl.length > ind + 1) {

                var selectItem = $scope.beData.LedgerAllocationColl[ind + 1];
                if (!selectItem.LedgerId || selectItem.LedgerId == null || selectItem.LedgerId == 0 || (selectItem.DrAmount == 0 && selectItem.CrAmount == 0))
                    return;

                $scope.beData.LedgerAllocationColl.splice(ind + 1, 0, {
                    DrCr: (clAmt > 0 ? 2 : 1),
                    LedgerId: 0,
                    AgentId: 0,
                    LFNO: '',
                    Narration: '',
                    DrAmount: (clAmt > 0 ? 0 : Math.abs(clAmt)),
                    CrAmount: (clAmt > 0 ? clAmt : 0),
                    ForBranchId: null,
                    Dimension1: null,
                    Dimension2: null,
                    Dimension3: null,
                    Dimension4: null,
                    Dimension5: null,
                    UDFFeildsColl: udfColl,

                    //New Field Added on Bhadra 18
                    LA_HeadingNo: '',
                    LA_ProgramCode: '',
                    TransactionType: '',
                    LA_Level: '',
                    LA_Organization: '',
                    LA_PaymentMethod: ''

                })
            }
            else if ($scope.beData.LedgerAllocationColl.length == (ind + 1)) {
                var selectItem = $scope.beData.LedgerAllocationColl[ind];
                if (!selectItem.LedgerId || selectItem.LedgerId == null || selectItem.LedgerId == 0 || (selectItem.DrAmount == 0 && selectItem.CrAmount == 0))
                    return;

                $scope.beData.LedgerAllocationColl.push({
                    DrCr: (clAmt > 0 ? 2 : 1),
                    LedgerId: 0,
                    AgentId: 0,
                    LFNO: '',
                    Narration: '',
                    DrAmount: (clAmt > 0 ? 0 : Math.abs(clAmt)),
                    CrAmount: (clAmt > 0 ? clAmt : 0),
                    ForBranchId: null,
                    Dimension1: null,
                    Dimension2: null,
                    Dimension3: null,
                    Dimension4: null,
                    Dimension5: null,
                    UDFFeildsColl: udfColl,

                    //New Field Added on Bhadra 18
                    LA_HeadingNo: '',
                    LA_ProgramCode: '',
                    TransactionType: '',
                    LA_Level: '',
                    LA_Organization: '',
                    LA_PaymentMethod: ''
                });
            }
            else {
                $scope.beData.LedgerAllocationColl.push({
                    DrCr: (clAmt > 0 ? 2 : 1),
                    LedgerId: 0,
                    AgentId: 0,
                    LFNO: '',
                    Narration: '',
                    DrAmount: (clAmt > 0 ? 0 : Math.abs(clAmt)),
                    CrAmount: (clAmt > 0 ? clAmt : 0),
                    ForBranchId: null,
                    Dimension1: null,
                    Dimension2: null,
                    Dimension3: null,
                    Dimension4: null,
                    Dimension5: null,
                    UDFFeildsColl: udfColl,

                    //New Field Added on Bhadra 18
                    LA_HeadingNo: '',
                    LA_ProgramCode: '',
                    TransactionType: '',
                    LA_Level: '',
                    LA_Organization: '',
                    LA_PaymentMethod: ''
                });
            }
        }
        $scope.ChangeDrCrAmount();

    }

    $scope.delRowLedgerAllocation = function (ind) {
        if ($scope.beData.LedgerAllocationColl) {
            if ($scope.beData.LedgerAllocationColl.length > 1) {
                $scope.beData.LedgerAllocationColl.splice(ind, 1);
                $scope.ChangeDrCrAmount();
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

    $scope.ChangeDrCr = function (ledAll) {
        if (ledAll.DrCr == 1) {
            ledAll.DrAmount = ledAll.CrAmount;
            ledAll.CrAmount = 0;
        } else if (ledAll.DrCr == 2) {
            ledAll.CrAmount = ledAll.DrAmount;
            ledAll.DrAmount = 0;
        }

        $scope.ChangeDrCrAmount();
    };


    $scope.ChangeBuyerSelection = function () {

    }

    $scope.CurLedgerAllocation = {};
    $scope.ChangeParticularLedger = function (ledDet) {
        $scope.CurLedgerAllocation = ledDet;
        $scope.sideBarData = ledDet.partySideBarData;
        $timeout(function () {

            if (ledDet) {
                ledDet.CurrentBal = 0;
                if (ledDet.LedgerId && ledDet.LedgerId > 0 && ledDet.LedgerDetails) {
                    ledDet.CurrentBal = ledDet.LedgerDetails.Closing;

                    $timeout(function () {
                        if (ledDet.LedgerDetails.CostCentersAreApplied == true) {
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
                                    Payment: 0,
                                    BillDate_TMP: new Date(),
                                });
                            }
                            $('#tds').modal('show');
                        } else
                            ledDet.TDSVatDetailColl = [];
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
                            ledDet.CheckDetails = {};
                    });
                }
            }
        });

    }
    $scope.ChangeParticularCostCenter = function (costAllocation) {
        $timeout(function () {
            costAllocation.CurrentBal = 0;

            if (costAllocation.CostCenterId && costAllocation.CostCenterId > 0 && costAllocation.CostCenterDetails) {
                costAllocation.CurrentBal = costAllocation.CostCenterDetails.Closing;
            }
        });
    };
    $scope.AddRowInCostCenterAllocation = function (ind, boolAuto) {

        if (boolAuto == true) {
            var len = $scope.CurLedgerAllocation.CostCenterColl.length;
            if ((ind + 1) != len)
                return;

            var selectItem = $scope.CurLedgerAllocation.CostCenterColl[ind];
            if (!selectItem.CostCenterId || selectItem.CostCenterId == null || selectItem.CostCenterId == 0 || selectItem.Amount == 0)
                return;

        }

        if ($scope.CurLedgerAllocation.CostCenterColl) {
            if ($scope.CurLedgerAllocation.CostCenterColl.length > ind + 1) {
                $scope.CurLedgerAllocation.CostCenterColl.splice(ind + 1, 0, {
                    CostCenterId: 0,
                    AgentId: 0,
                    LFNO: '',
                    Narration: '',
                    Amount: 0
                })
            } else {
                $scope.CurLedgerAllocation.CostCenterColl.push({
                    CostCenterId: 0,
                    AgentId: 0,
                    LFNO: '',
                    Narration: '',
                    Amount: 0
                })
            }
        }

    }

    $scope.delRowCostCenterAllocation = function (ind) {
        if ($scope.CurLedgerAllocation.CostCenterColl) {
            if ($scope.CurLedgerAllocation.CostCenterColl.length > 1) {
                $scope.CurLedgerAllocation.CostCenterColl.splice(ind, 1);
                $scope.ChangeCostCenterAmount();
                $scope.ChangeDrCrAmount();
            }
        }
    }
    $scope.ChangeCostCenterAmount = function () {
        $timeout(function () {
            if ($scope.CurLedgerAllocation.DrCr == 2)
                $scope.CurLedgerAllocation.CrAmount = mx($scope.CurLedgerAllocation.CostCenterColl).sum(p1 => p1.Amount);
            else if ($scope.CurLedgerAllocation.DrCr == 1)
                $scope.CurLedgerAllocation.DrAmount = mx($scope.CurLedgerAllocation.CostCenterColl).sum(p1 => p1.Amount);

            $scope.ChangeDrCrAmount();
        });
    };
    $scope.OkCostCenterModal = function () {
        $timeout(function () {
            if ($scope.CurLedgerAllocation.DrCr == 2)
                $scope.CurLedgerAllocation.CrAmount = mx($scope.CurLedgerAllocation.CostCenterColl).sum(p1 => p1.Amount);
            else if ($scope.CurLedgerAllocation.DrCr == 1)
                $scope.CurLedgerAllocation.DrAmount = mx($scope.CurLedgerAllocation.CostCenterColl).sum(p1 => p1.Amount);

            $('#frmCostCentersModel').modal('hide');
        });
    };

    $scope.AddRowInItemDetails = function (ind) {


        if ($scope.CurLedgerAllocation.ItemDetailsCOll) {
            if ($scope.CurLedgerAllocation.ItemDetailsCOll.length > ind + 1) {
                $scope.CurLedgerAllocation.ItemDetailsCOll.splice(ind + 1, 0, {
                    ProductId: 0,
                    productDetail: null,
                    ActualQty: 0,
                    BilledQty: 0,
                    FreeQty: 0,
                    Rate: 0,
                    DiscountPer: 0,
                    DiscountAmt: 0,
                    SchameAmt: 0,
                    SchamePer: 0,
                    Amount: 0,
                    Description: '',
                    QtyPoint: 0,
                    UnitId: null,
                    CanEditRate: false,
                    ALValue1: 0,
                    ALValue2: 0,
                    ALUnitId1: null,
                    ALUnitId2: null
                })
            } else {
                $scope.CurLedgerAllocation.ItemDetailsCOll.push({
                    ProductId: 0,
                    productDetail: null,
                    ActualQty: 0,
                    BilledQty: 0,
                    FreeQty: 0,
                    Rate: 0,
                    DiscountPer: 0,
                    DiscountAmt: 0,
                    SchameAmt: 0,
                    SchamePer: 0,
                    Amount: 0,
                    Description: '',
                    QtyPoint: 0,
                    UnitId: null,
                    CanEditRate: false,
                    ALValue1: 0,
                    ALValue2: 0,
                    ALUnitId1: null,
                    ALUnitId2: null
                })
            }
        }

    }

    $scope.delRowFromItemDetails = function (ind) {
        if ($scope.CurLedgerAllocation.ItemDetailsCOll) {
            if ($scope.CurLedgerAllocation.ItemDetailsCOll.length > 1) {
                $scope.CurLedgerAllocation.ItemDetailsCOll.splice(ind, 1);
            }
        }
    }

    $scope.CalculateTDS = function (tds, col) {
        if (col == 'access' || col == 'rate') {
            if (tds.Rate > 0 && tds.AccessableValue > 0) {
                tds.Amount = tds.AccessableValue * tds.Rate / 100;
            }
        }

        var totalAmt = mx($scope.CurLedgerAllocation.TDSVatDetailColl).sum(p1 => p1.Amount + p1.Payment);
        if ($scope.CurLedgerAllocation.DrCr == 1)
            $scope.CurLedgerAllocation.DrAmount = totalAmt;
        else
            $scope.CurLedgerAllocation.CrAmount = totalAmt;
    }
    $scope.AddRowInTDSDetails = function (ind) {


        if ($scope.CurLedgerAllocation.TDSVatDetailColl) {
            if ($scope.CurLedgerAllocation.TDSVatDetailColl.length > ind + 1) {
                $scope.CurLedgerAllocation.TDSVatDetailColl.splice(ind + 1, 0, {
                    SNO: 0,
                    Amount: 0,
                    Rate: 0,
                    Payment: 0,
                    BillDate_TMP: new Date(),
                })
            } else {
                $scope.CurLedgerAllocation.TDSVatDetailColl.push({
                    SNO: 0,
                    Amount: 0,
                    Rate: 0,
                    Payment: 0,
                    BillDate_TMP: new Date(),
                })
            }
        }

    }

    $scope.delRowFromTDSDetails = function (ind) {
        if ($scope.CurLedgerAllocation.TDSVatDetailColl) {
            if ($scope.CurLedgerAllocation.TDSVatDetailColl.length > 1) {
                $scope.CurLedgerAllocation.TDSVatDetailColl.splice(ind, 1);
            }
        }
    }
    $scope.ChangeItemRowValue = function (itemDet, col) {

        var amt = 0, qty = 0, rate = 0, disAmt = 0, disPer = 0, schAmt = 0, schPer = 0;

        var aQty = 0;
        if (itemDet.ActualQty || itemDet.ActualQty >= 0)
            aQty = itemDet.ActualQty;

        qty = aQty;

        if (itemDet.Rate || itemDet.Rate >= 0)
            rate = itemDet.Rate;

        if (itemDet.productDetail) {
            if (itemDet.productDetail.ClosingQty < qty)
                itemDet.IsNegativeQty = true;
            else if (itemDet.RefQty && itemDet.RefQty < qty)
                itemDet.IsNegativeQty = true;
            else
                itemDet.IsNegativeQty = false;
        }
        if ((itemDet.Amount || itemDet.Amount != 0) && col == "amt") {

        }

        amt = qty * rate;

        if (itemDet.DiscountAmt || itemDet.DiscountAmt >= 0)
            disAmt = itemDet.DiscountAmt;

        if (itemDet.DiscountPer || itemDet.DiscountPer >= 0)
            disPer = itemDet.DiscountPer;

        if (col == "disAmt") {

            if (disAmt > 0) {
                disPer = (disAmt / amt) * 100;
            } else
                disPer = 0;
        }
        else if (col == "disPer" || col == "product") {

            if (disPer > 0) {
                disAmt = amt * disPer / 100;
            } else
                disAmt = 0;
        }


        itemDet.Amount = amt - disAmt;

        if (col == "disAmt")
            itemDet.DiscountPer = disPer;
        else if (col == "disPer" || col == "product")
            itemDet.DiscountAmt = disAmt;

        itemDet.BilledQty = aQty;

        if (itemDet.productDetail) {
            if (itemDet.productDetail.AlternetUnitColl) {
                var alternetUnit1 = null, alternetUnit2 = null;

                if (itemDet.productDetail.AlternetUnitColl.length > 0) {
                    alternetUnit1 = itemDet.productDetail.AlternetUnitColl[0];
                    itemDet.ALValue1 = (alternetUnit1.AlterNetUnitValue * aQty) / alternetUnit1.BaseUnitValue;
                    itemDet.ALUnitId1 = alternetUnit1.AlterNetUnitId;
                }

                if (itemDet.productDetail.AlternetUnitColl.length > 1) {
                    alternetUnit2 = itemDet.productDetail.AlternetUnitColl[1];
                    itemDet.ALValue2 = (alternetUnit2.AlterNetUnitValue * aQty) / alternetUnit2.BaseUnitValue;
                    itemDet.ALUnitId2 = alternetUnit2.AlterNetUnitId;
                }
            }
        }

        var totalAmt = mx($scope.CurLedgerAllocation.ItemDetailsCOll).sum(p1 => p1.Amount);
        if ($scope.CurLedgerAllocation.DrCr == 1)
            $scope.CurLedgerAllocation.DrAmount = parseFloat(totalAmt);
        else
            $scope.CurLedgerAllocation.CrAmount = parseFloat(totalAmt);
    }

    $scope.SaveJournal = function () {

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
                            url: base_url + "Account/Transaction/SaveUpdateJournal",
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
                        url: base_url + "Account/Transaction/GetJournalById",
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

        //if ($scope.beData.CashBankLedgerId) {
        //    if ($scope.beData.CashBankLedgerId == null || $scope.beData.CashBankLedgerId == 0) {
        //        result = false;
        //        Swal.fire('Please ! Select Valid Cash/Bank Name');
        //    }
        //} else {
        //    result = false;
        //    Swal.fire('Please ! Select Valid Cash/Bank Name');
        //}

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

        var tmpJournal = {
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
            //New code added on 18 Bhadra starts
            BudgetNo: $scope.beData.BudgetNo,
            TranNo: $scope.beData.TranNo
        //Ends
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
            tmpJournal.Attributes = JSON.stringify(voucherUDFFields);
            tmpJournal.UDFKeyVal = JSON.stringify(voucherKeyVal);
        } else {
            tmpJournal.Attributes = "";
            tmpJournal.UDFKeyVal = "";
        }

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
                    DrCr: ledA.DrCr,
                    LedgerId: ledA.LedgerId,
                    AgentId: ledA.AgentId,
                    LFNO: ledA.LFNO,
                    Narration: ledA.Narration,
                    DrAmount: ledA.DrAmount,
                    CrAmount: ledA.CrAmount,
                    ForBranchId: ledA.ForBranchId,
                    Dimension1: ledA.Dimension1,
                    Dimension2: ledA.Dimension2,
                    Dimension3: ledA.Dimension3,
                    Dimension4: ledA.Dimension4,
                    Dimension5: ledA.Dimension5,
                    SubLedgerId: ledA.SubLedgerId,
                    CostCenterColl: [],
                    ItemDetailsCOll: [],
                    TDSVatDetailColl: [],
                    CheckDetails: {},
                    BillRefColl: [],

                    //Added on Bhadra 18 starts
                    LA_HeadingNo: ledA.LA_HeadingNo,
                    LA_ProgramCode: ledA.LA_ProgramCode,
                    LA_Level: ledA.LA_Level,
                    LA_Organization: ledA.LA_Organization,
                    LA_PaymentMethod: ledA.LA_PaymentMethod
                    //Ends
                };

                if (ledA.CheckDetails && ledA.CheckDetails.ChequeDateDet) {
                    crLedAllocation.CheckDetails.ChequeNo = ledA.CheckDetails.ChequeNo;
                    crLedAllocation.CheckDetails.AccountNo = ledA.CheckDetails.AccountNo;
                    crLedAllocation.CheckDetails.Remarks = ledA.CheckDetails.Remarks;
                    crLedAllocation.CheckDetails.CheckType = ledA.CheckDetails.CheckType;
                    crLedAllocation.CheckDetails.ChequeDate = $filter('date')(new Date(ledA.CheckDetails.ChequeDateDet.dateAD), 'yyyy-MM-dd');
                }

                if (ledA.CostCenterColl) {
                    angular.forEach(ledA.CostCenterColl, function (cc) {
                        if (cc.CostCenterId && cc.CostCenterId > 0 && cc.Amount != 0) {
                            var ccAllocation = {
                                CostCategoriesId: (cc.CostCenterDetails ? cc.CostCenterDetails.CostCategoryId : cc.CostCategoriesId),
                                CostCenterId: cc.CostCenterId,
                                DrAmount: (crLedAllocation.DrCr == 1 ? cc.Amount : 0),
                                CrAmount: (crLedAllocation.DrCr == 2 ? cc.Amount : 0),
                                Narration: '',
                                DrCr: crLedAllocation.DrCr,
                                DepartmentId: cc.DepartmentId,
                                ProductBrandId: cc.ProductBrandId,
                                AreaId: cc.AreaId
                            };
                            crLedAllocation.CostCenterColl.push(ccAllocation);
                        }
                    });
                }

                if (ledA.ItemDetailsCOll) {
                    angular.forEach(ledA.ItemDetailsCOll, function (cc) {
                        if (cc.ProductId > 0 && (cc.Amount != 0 || cc.ActualQty != 0)) {
                            var itemAllocation = {
                                ProductId: cc.ProductId,
                                GodownId: cc.GodownId,
                                IsInQty: ledA.InOut,
                                ActualQty: cc.ActualQty,
                                BilledQty: cc.ActualQty,
                                UnitId: cc.productDetail.BaseUnitId,
                                Amount: cc.Amount,
                                Rate: cc.Rate
                            };
                            crLedAllocation.ItemDetailsCOll.push(itemAllocation);
                        }
                    });
                }

                if (ledA.TDSVatDetailColl) {
                    angular.forEach(ledA.TDSVatDetailColl, function (cc) {
                        if (cc.BillDateDet && (cc.Amount != 0 || cc.Payment != 0)) {
                            var tdsVat = {
                                BillNo: cc.BillNo,
                                BillDate: $filter('date')(new Date(cc.BillDateDet.dateAD), 'yyyy-MM-dd'),
                                PartyLedgerId: cc.PartyLedgerId,
                                PartyName: cc.PartyName,
                                OtherHeading: cc.OtherHeading,
                                PANVat: cc.PANVat,
                                Payment: cc.Payment,
                                AccessableValue: cc.AccessableValue,
                                Rate: cc.Rate,
                                Amount: cc.Amount,
                                TdsVatType: cc.TdsVatType
                            };
                            crLedAllocation.TDSVatDetailColl.push(tdsVat);
                        }
                    });
                }

                tmpJournal.LedgerAllocationColl.push(crLedAllocation);
            }
        });


        return tmpJournal;
    };

    $scope.SetData = function (tran) {

        $scope.beData.VoucherDate_TMP = new Date(tran.VoucherDate);
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

        //New code added on 18 Bhadra starts
        $scope.beData.BudgetNo = tran.BudgetNo;
        $scope.beData.TranNo = tran.TranNo;
        //Ends

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

            if (ledAll.TDSVatDetailColl) {
                angular.forEach(ledAll.TDSVatDetailColl, function (tds) {
                    if (tds.BillDate)
                        tds.BillDate_TMP = new Date(tds.BillDate);
                });

                angular.forEach(ledAll.CostCenterColl, function (cc) {
                    if (cc.DrAmount > 0)
                        cc.Amount = cc.DrAmount;
                    else if (cc.CrAmount > 0)
                        cc.Amount = cc.CrAmount;
                });
            }

            if (ledAll.CheckDetails && ledAll.CheckDetails.ChequeDate) {
                ledAll.CheckDetails.ChequeDate_TMP = new Date(ledAll.CheckDetails.ChequeDate);
            }

            if (ledAll.ItemDetailsCOll && ledAll.ItemDetailsCOll.length > 0) {
                angular.forEach(ledAll.ItemDetailsCOll, function (idet) {
                    if (idet.IsInQty == true) {
                        idet.InOut = 1;
                    }
                    else
                        idet.InOut = 0;
                });
            }

            $scope.beData.LedgerAllocationColl.push(ledAll);
        });

        $scope.ChangeDrCrAmount();
    };

    $scope.ChangeDrCrAmount = function (curLA) {
        var totalDr = 0, totalCr = 0;
        angular.forEach($scope.beData.LedgerAllocationColl, function (led) {
            totalCr += led.CrAmount;
            totalDr += led.DrAmount;
        });

        $scope.beData.DrAmount = totalDr;
        $scope.beData.CrAmount = totalCr;

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
            Mode: 'Save',

            //New Field Added on Bhadra 18
            BudgetNo: 0,
            TranNo:0
        };

        $scope.beData.LedgerAllocationColl.push(
            {
                DrCr: 1,
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
        })


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

    $scope.ChangeVatTDSParty = function (led) {
        if (led.VTLedgerDetails) {
            led.PartyName = led.VTLedgerDetails.Name;
            led.PANVat = led.VTLedgerDetails.PanVat;
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