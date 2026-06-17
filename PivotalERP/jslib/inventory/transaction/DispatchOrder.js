app.controller('DispatchOrderController', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'Sales Invoice';
    var glSrv = GlobalServices;
    LoadData();

    $scope.sideBarData = [];

    $scope.lastTranId = 0;
    function LoadData() {
        $('.select2bs4').select2();
        $('.select2').select2();

        $scope.searchData = {
            RefProduct: ''
        };

        $scope.RefVoucherTypeColl = [{ id: 1, text: 'Sales Order' }];

        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.currentPages = {
            FixedProduct: 1,
            PendingOrder: 1,
        };

        $scope.searchData = {
            FixedProduct: '',
            PendingOrder: '',
        };

        $scope.perPage = {
            FixedProduct: GlobalServices.getPerPageRow(),
            PendingOrder: GlobalServices.getPerPageRow(),
        };


        $scope.AgentColl = [];
        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetAllSalesMan",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.AgentColl = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.comDet = {};
        GlobalServices.getCompanyDet().then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.comDet = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.confirmMSG = glSrv.getConfirmMSG();
        $scope.paginationOptions = {
            pageNumber: 1,
            pageSize: glSrv.getPerPageRow(),
            sort: null,
            SearchType: 'text',
            SearchCol: '',
            SearchVal: '',
            SearchColDet: null,
            pagearray: [],
            pageOptions: [5, 10, 20, 30, 40, 50]
        };
        $scope.VoucherSearchOptions = [{ text: 'PartyName', value: 'ADS.Buyes', searchType: 'text' }, { text: 'PanVat', value: 'ADS.SalesTaxNo', searchType: 'text' }, { text: 'PartyLedger', value: 'Led.Name', searchType: 'text' }, { text: 'RefNo', value: 'TS.[No]', searchType: 'text' }, { text: 'Invoice No.', value: 'TS.AutoManualNo', searchType: 'text' }, { text: 'Voucher', value: 'V.VoucherName', searchType: 'text' }, { text: 'CostClass', value: 'CC.Name', searchType: 'text' }, { text: 'VoucherDate', value: 'TS.VoucherDate', searchType: 'date' }, { text: 'Amount', value: 'TS.TotalAmount', searchType: 'number' }];

        $scope.mandetoryFields = {};
        $scope.PaymentTypeColl = glSrv.getPaymentTypeColl();
        $scope.FreightTypeColl = [];
        glSrv.getFreightTypes().then(function (res1) {
            $scope.FreightTypeColl = res1.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
        $scope.PaymentTermList = [];
        $scope.PaymentTermList.push('CASH');
        $scope.PaymentTermList.push('BANK');
        $scope.PaymentTermList.push('CREDIT');
        $scope.VoucherTypeColl = [];
        $scope.CostClassColl = [];
        $scope.NarrationList = [];
        $scope.SelectedVoucher = null;
        $scope.SelectedCostClass = null;
        $scope.SalesFeatures = {};
        $scope.Config = {};
        $scope.RefItemAllocationColl = [];
        $scope.GodownColl = [];

        $scope.HideShow = {
            Godown: true,
            VoucherType: true,
            CostClass: true,
            AutoVoucherNo: true,
            PartyCostCenter: true,
            TranCostCenter: true,
            Agent: true,
            Currency: true,
            RefNo: true,
            SalesLedger: true,
            BilledQty: true,
            Discount: true,
            DiscountAmt: true,
            DiscountPer: true,
            CurrentBalance: true,
            FreeQty: true,
            Scheme: true,
            SchemeAmt: true,
            SchemePer: true,
            ProductDescript: true,
            ProductPoint: true,
            ProductLedger: true,
            ShowProductWiseLedger: true,
            AlternetUnit: true,
            AlternetUnit1: true,
            AlternetUnit2: true,
            AlternetUnitMultiple: true,
            EntryDate: true,
            Batch: true,
            EXPDate: true,
            MFGDate: true,
            EachNarration: true,
            ExciseDuty: true,
            Vat: true,
            Amount: false,
            Rate: false,
            MRP: true,
            SalesRate: true,
            TradeRate: true,
            NotEffectQty: true,
            ActiveBarCode: true,
        }

        $scope.beData =
        {
            VoucherId: null,
            CostClassId: null,
            TranId: 0,
            AutoManualNo: '',
            AutoVoucherNo: 0,
            PartyLedgerId: null,
            PartyLedger: null,
            partySideBarData: null,
            SalesMan: null,
            salesmanSideBarData: null,
            CurRate: 1,
            ItemDetailsColl: [],
            AditionalCostColl: [],
            AttechFiles: [],
            SubTotal: 0,
            Total: 0,
            Narration: '',
            VoucherDate: new Date(),
            VoucherDate_TMP: new Date(),
            EntryDate_TMP: new Date(),
            DispatchOrderDetail: {},
            SaveClear: false,
            RefVoucherType: 1,
            PaymentTermColl: [],
        };
        $scope.beData.ItemDetailsColl.push(
            {
                RowType: 'P',
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
                ALUnitId2: null,
                SchemeAmt: 0,
                SchemeAmt: 0,
                QtyDecimal: 2,
                RateDecimal: 2,
                AmountDecimal: 2
            });
        $('.hideSideBar').on('focus', function (e) {
            $('#sidebarzz').removeClass();
            $('#sidebarzz').addClass('order-last float-right active');
        })

        $scope.UnitColl = [];
        $scope.AllUnitColl = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetAllUnit",
            dataType: "json"
            //data:JSON.stringify(para)
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.AllUnitColl = res.data.Data;
                $scope.UnitColl = mx(res.data.Data);
            }
        }, function (reason) {
            alert('Failed' + reason);
        });

        $scope.CurrencyColl = [];
        $http({
            method: 'GET',
            url: base_url + "Account/Creation/GetAllCurrency",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.CurrencyColl = res.data.Data;
            }
        }, function (reason) {
            alert('Failed' + reason);
        });

        $scope.SalesLedgerColl = [];
        glSrv.getSalesLedger().then(function (res1) {
            $scope.SalesLedgerColl = res1.data.Data;
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


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
                url: base_url + "Inventory/Creation/GetUserWiseGodown",
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.GodownColl = res.data.Data;
                    if ($scope.GodownColl.length == 1) {
                        $scope.beData.GodownId = $scope.GodownColl[0].GodownId;
                        $scope.HideShow.Godown = true;
                    } else {
                        $scope.HideShow.Godown = false;
                        $scope.beData.GodownId = null;
                    }
                } else {
                    Swal.fire(res.data.ResponseMSG);
                }

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

            $http({
                method: 'GET',
                url: base_url + "Account/Creation/GetVoucherMandetoryFields?voucherType=" + VoucherType,
                dataType: "json"
            }).then(function (res) {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.mandetoryFields = res.data.Data;
                } else
                    Swal.fire(res.data.ResponseMSG);
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
                url: base_url + "Setup/Security/GetSalesFeatures",
                dataType: "json"
            }).then(function (res1) {
                if (res1.data.IsSuccess && res1.data.Data) {
                    $scope.SalesFeatures = res1.data.Data;

                    $timeout(function () {
                        $scope.$apply(function () {
                            if ($scope.SalesFeatures.ProductWiseSalesLedger == true)
                                $scope.HideShow.SalesLedger = true;
                            else
                                $scope.HideShow.SalesLedger = false;
                        });
                    });
                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });

            $http({
                method: 'GET',
                url: base_url + "Setup/Security/GetInventoryConfig",
                dataType: "json"
            }).then(function (res1) {
                if (res1.data.IsSuccess && res1.data.Data) {
                    $scope.Config = res1.data.Data;

                    $timeout(function () {
                        $scope.$apply(function () {
                            //if ($scope.Config.AllowBilledQty == true)
                            //    $scope.HideShow.BilledQty = false;
                            //else
                            //    $scope.HideShow.BilledQty = true;

                            //if ($scope.Config.AllowDiscountAmount == true)
                            //    $scope.HideShow.DiscountAmt = false;
                            //else
                            //    $scope.HideShow.DiscountAmt = true;

                            //if ($scope.Config.AllowDiscountPer == true)
                            //    $scope.HideShow.DiscountPer = false;
                            //else
                            //    $scope.HideShow.DiscountPer = true;

                            //if ($scope.Config.AllowDiscountPer == false && $scope.Config.AllowDiscountAmount == false)
                            //    $scope.HideShow.Discount = true;
                            //else
                            //    $scope.HideShow.Discount = false;

                            //if ($scope.Config.ShowCurrentBalance == true)
                            //    $scope.HideShow.CurrentBalance = false;
                            //else
                            //    $scope.HideShow.CurrentBalance = true;

                            //if ($scope.Config.AllowFreeQty == true)
                            //    $scope.HideShow.FreeQty = false;
                            //else
                            //    $scope.HideShow.FreeQty = true;

                            //if ($scope.Config.AllowSchameAmount == true)
                            //    $scope.HideShow.SchemeAmt = false;
                            //else
                            //    $scope.HideShow.SchemeAmt = true;

                            //if ($scope.Config.AllowSchamePer == true)
                            //    $scope.HideShow.SchemePer = false;
                            //else
                            //    $scope.HideShow.SchemePer = true;

                            //if ($scope.Config.AllowSchamePer == false && $scope.Config.AllowSchameAmount == false)
                            //    $scope.HideShow.Scheme = true;
                            //else
                            //    $scope.HideShow.Scheme = false;


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


            if (RecVoucherType) {
                $http({
                    method: 'GET',
                    url: base_url + "Account/Creation/GetVoucherList?voucherType=" + RecVoucherType,
                    dataType: "json"
                }).then(function (res) {
                    if (res.data.IsSuccess && res.data.Data) {
                        $scope.RecVoucherTypeColl = res.data.Data;
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }
        }

        $scope.RefVoucherNoColl = [];
        $('#cboRefVoucherNo').select2();
        $('#cboRefVoucherNo').on("change", function (e) {
            var selectedData = $('#cboRefVoucherNo').select2('data');
            if (selectedData && selectedData.length > 0) {
                $scope.beData.RefTranIdColl = [];
                $scope.beData.RefAllotationIdColl = [];
                angular.forEach(selectedData, function (sd) {
                    $scope.beData.RefTranIdColl.push(parseInt(sd.id));
                });
                var refTranIdColl = mx($scope.beData.RefTranIdColl);
                var lstSelectedItem = null;
                angular.forEach($scope.RefItemAllocationColl, function (ri) {
                    if (refTranIdColl.contains(ri.TranId)) {
                        ri.IsSelected = true;
                        lstSelectedItem = ri;
                        $scope.beData.RefAllotationIdColl.push(ri.ItemAllocationId);
                    } else
                        ri.IsSelected = false;
                });

                if (lstSelectedItem)
                    $scope.getRefVoucherPartyDetails(lstSelectedItem);

            }
        });

        $timeout(function () {
            GlobalServices.getCurrentDateTime().then(function (res) {
                var curDate = res.data.Data;
                if (curDate) {
                    $scope.beData.VoucherDate_TMP = new Date(curDate);
                }
            }, function (errormessage) {
                alert('Unable to Delete data. pls try again.' + errormessage.responseText);
            });
        });


        $scope.PaymentTermColl = [];
        $scope.PaymentTermColl_Qry = [];
        GlobalServices.getPaymentTerms().then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.PaymentTermColl = res.data.Data;
                $scope.PaymentTermColl_Qry = mx(res.data.Data);

                angular.forEach($scope.PaymentTermColl, function (pt) {
                    if (pt.IsCash == true && pt.LedgerId > 0) {
                        $scope.beData.PaymentTermColl.push({
                            Name: pt.Name,
                            PaymentTermsId: pt.PaymentTermsId,
                            LedgerId: pt.LedgerId,
                            Amount: 0,
                            Remarks: ''
                        });
                    }
                });
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }

    $scope.AddRowInTable = function (ind) {
        if ($scope.beData.ItemDetailsColl) {
            var blankItemRowCount = 0, blankLedRowCount = 0;
            angular.forEach($scope.beData.ItemDetailsColl, function (idet) {
                if (!idet.productDetail && idet.RowType == 'P')
                    blankItemRowCount++;
                else if (!idet.costLedgerDetail && idet.RowType == 'L')
                    blankLedRowCount++;
            });

            var selectRowObj = $scope.beData.ItemDetailsColl[ind];
            if (selectRowObj.RowType == 'P' && (!selectRowObj.ProductId || selectRowObj.ProductId == 0) && blankItemRowCount <= 1 && blankLedRowCount < 1) {
                $scope.AddRowInLedgerDetails(ind);
            } else if (selectRowObj.RowType == 'L' && selectRowObj.costLedgerDetail) {
                $scope.AddRowInLedgerDetails(ind);
            } else if (blankItemRowCount < 1 && selectRowObj.RowType == 'P')
                $scope.AddRowInItemDetails(ind);

        }
    };

    $scope.AddRowInItemDetails = function (ind) {

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
                };
                udfColl.push(ud);
            });
        }

        if ($scope.beData.ItemDetailsColl) {
            if ($scope.beData.ItemDetailsColl.length > ind + 1) {
                $scope.beData.ItemDetailsColl.splice(ind + 1, 0, {
                    RowType: 'P',
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
                    ALUnitId2: null,
                    SchemeAmt: 0,
                    SchemeRate: 0,
                    ExciseAbleQty: 0,
                    ExciseAbtAmt: 0,
                    VatAbleAmt: 0,
                    VatRate: 0,
                    VatAmount: 0,
                    ExDutyRate: 0,
                    ExDutyAmount: 0,
                    QtyDecimal: 2,
                    RateDecimal: 2,
                    AmountDecimal: 2,
                    GodownId: $scope.beData.GodownId,
                    UDFFeildsColl: udfColl,
                })
            } else {
                $scope.beData.ItemDetailsColl.push({
                    RowType: 'P',
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
                    ALUnitId2: null,
                    SchemeAmt: 0,
                    SchemeRate: 0,
                    ExciseAbleQty: 0,
                    ExciseAbtAmt: 0,
                    VatAbleAmt: 0,
                    VatRate: 0,
                    VatAmount: 0,
                    ExDutyRate: 0,
                    ExDutyAmount: 0,
                    QtyDecimal: 2,
                    RateDecimal: 2,
                    AmountDecimal: 2,
                    GodownId: $scope.beData.GodownId,
                    UDFFeildsColl: udfColl,
                })
            }
        }

    }

    $scope.delRowFromItemDetails = function (ind) {
        if ($scope.beData.ItemDetailsColl) {
            if ($scope.beData.ItemDetailsColl.length > 1) {
                $scope.beData.ItemDetailsColl.splice(ind, 1);
            }
        }


        $scope.RecalculateAdditioncalCost();
        $scope.CalculateTotalAndSubTotal();
    }

    $scope.AddRowInLedgerDetails = function (ind) {

        if ($scope.SelectedVoucher.ActiveAdditionalCost == false)
            return;

        if ($scope.beData.ItemDetailsColl) {
            if ($scope.beData.ItemDetailsColl.length > ind + 1) {
                $scope.beData.ItemDetailsColl.splice(ind + 1, 0, {
                    RowType: 'L',
                    LedgerId: 0,
                    ledgerDetail: null,
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
                    CanEditRate: true,
                    ALValue1: 0,
                    ALValue2: 0,
                    ALUnitId1: null,
                    ALUnitId2: null,
                    SchemeAmt: 0,
                    SchemeAmt: 0,
                    QtyDecimal: 2,
                    RateDecimal: 2,
                    AmountDecimal: 2
                })
            } else {
                $scope.beData.ItemDetailsColl.push({
                    RowType: 'L',
                    LedgerId: 0,
                    ledgerDetail: null,
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
                    CanEditRate: true,
                    ALValue1: 0,
                    ALValue2: 0,
                    ALUnitId1: null,
                    ALUnitId2: null,
                    SchemeAmt: 0,
                    SchemeAmt: 0,
                    QtyDecimal: 2,
                    RateDecimal: 2,
                    AmountDecimal: 2
                })
            }
        }

    }

    $scope.ChangeCurrency = function () {
        if ($scope.beData.CurrencyDet) {
            $scope.beData.CurRate = $scope.beData.CurrencyDet.SellingRate;
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

        //  Swal.fire('On Product Load');
        // $scope.loadingstatus = 'running';
    };

    $scope.RefreshRowData = function (itemDet, ind) {
        //if (itemDet.RowType == 'P') {

        //    $scope.loadingstatus = 'running';
        //    showPleaseWait();
        //    $http({
        //        method: 'GET',
        //        url: base_url + "Global/GetProductDetail?ProductId=" + itemDet.ProductId + " & VoucherType=" + $scope.SelectedVoucher.VoucherType + "&VoucherId=" + $scope.SelectedVoucher.VoucherId ,
        //        dataType: "json"
        //    }).then(function (resLD) {

        //        $scope.loadingstatus = 'stop';
        //        hidePleaseWait();
        //        if (resLD.data.IsSuccess && resLD.data.Data) {
        //            itemDet.productDetail = resLD.data.Data;
        //            $scope.ProductSelectionChange(itemDet, ind);
        //        }
        //    }, function (reason) {
        //        alert('Failed' + reason);
        //    });

        //}
        //else if (itemDet.RowType == 'L') {
        //    $scope.loadingstatus = 'running';
        //    showPleaseWait();
        //    $http({
        //        method: 'GET',
        //        url: base_url + "Global/GetLedgerDetail?LedgerId=" + itemDet.LedgerId + " & VoucherType=" + $scope.SelectedVoucher.VoucherType + "&ShowClosing=false",
        //        dataType: "json"
        //    }).then(function (resLD) {

        //        $scope.loadingstatus = 'stop';
        //        hidePleaseWait();
        //        if (resLD.data.IsSuccess && resLD.data.Data) {
        //            //itemDet.costLedgerDetail = resLD.data.Data
        //            var llId = resLD.data.Data.LedgerId;
        //            angular.forEach($scope.beData.ItemDetailsColl, function (idLed) {
        //                if (idLed.RowType == 'L') {
        //                    if (llId == idLed.LedgerId) {
        //                        idLed.costLedgerDetail = resLD.data.Data;
        //                    }
        //                }
        //            });

        //            $scope.RecalculateAdditioncalCost();
        //        }
        //    }, function (reason) {
        //        alert('Failed' + reason);
        //    });
        //}
    }

    $scope.CurItemAllocation = null;
    $scope.ProductSelectionChange = function (itemDet, ind) {
        $scope.sideBarData = itemDet.sideBarData;
        $scope.CurItemAllocation = itemDet;

        var isModify = $scope.beData.TranId > 0 ? true : false;

        if (itemDet.ProductId > 0 && (itemDet.productDetail == null || itemDet.productDetail === undefined)) {

            $scope.loadingstatus = 'running';
            showPleaseWait();
            $http({
                method: 'GET',
                url: base_url + "Global/GetProductDetail?ProductId=" + itemDet.ProductId + " & VoucherType=" + $scope.SelectedVoucher.VoucherType + "&VoucherId=" + $scope.SelectedVoucher.VoucherId,
                dataType: "json"
            }).then(function (resLD) {

                $scope.loadingstatus = 'stop';
                hidePleaseWait();
                if (resLD.data.IsSuccess && resLD.data.Data) {
                    itemDet.productDetail = resLD.data.Data;
                    $scope.ProductSelectionChange(itemDet, ind);
                }
            }, function (reason) {
                alert('Failed' + reason);
            });

        }

        if (itemDet.ProductId == null || itemDet.ProductId == 0) {
            itemDet.ActualQty = 0;
            itemDet.BilledQty = 0;
            itemDet.Rate = 0;
            itemDet.ClosingQty = '';
            itemDet.UnitId = null;
            itemDet.UnitName = '';
            itemDet.DiscountAmt = 0;
            itemDet.DiscountPer = 0;
            itemDet.SchameAmt = 0;
            itemDet.SchamePer = 0;
            itemDet.ProductLedgerId = null;
            itemDet.LossRate = 0;
            itemDet.Makeing = 0;
            itemDet.Stone = 0;
            itemDet.NetWeight = 0;
            itemDet.LossWeight = 0;
            itemDet.BatchBalQty = 0;
            itemDet.TranUnitId = null;
            $scope.ChangeItemRowValue(itemDet, 'product');
        } else if (itemDet.productDetail) {

            itemDet.TranUnitId = null;
            itemDet.CanEditRate = itemDet.productDetail.CanEditRate;
            var refStockItem = false;
            if (itemDet.SalesOrderItemAllocationId > 0 || itemDet.OrderItemAllocationId > 0 || itemDet.DispatchOrderItemAllocationId > 0 || itemDet.QuotationItemAllocationId > 0) {
                refStockItem = true;
            }


            if (isModify == false && refStockItem == false) {
                itemDet.Rate = itemDet.productDetail.SalesRate;
                itemDet.ProductLedgerId = itemDet.productDetail.SalesLedgerId;
                itemDet.LedgerId = itemDet.productDetail.SalesLedgerId;
            }

            itemDet.ClosingQty = $filter('formatNumber')(itemDet.productDetail.ClosingQty) + ' ' + itemDet.productDetail.BaseUnit;
            itemDet.UnitId = itemDet.productDetail.BaseUnitId;
            itemDet.UnitName = itemDet.productDetail.BaseUnit;

            itemDet.RateOf = itemDet.productDetail.RateOf;
            itemDet.LossRate = itemDet.productDetail.LossRate;
            itemDet.Makeing = 0;
            itemDet.Stone = 0;
            itemDet.BatchBalQty = 0;
            //itemDet.ActualQty = 0;
            //itemDet.BilledQty = 0;
            //itemDet.DiscountAmt = 0;
            //itemDet.DiscountPer = 0;

            if ($scope.SelectedVoucher.IsAbbInvoice == true && itemDet.productDetail.IsTaxable == true) {
                itemDet.Rate = itemDet.Rate + (itemDet.Rate * 13 / 100);
            }

            if ($scope.SelectedVoucher.Product && $scope.SelectedVoucher.Product.VoucherWiseDecimalPlaces == true) {
                itemDet.QtyDecimal = $scope.SelectedVoucher.Product.QtyNoOfDecimalPlaces;
                itemDet.RateDecimal = $scope.SelectedVoucher.Product.RateNoOfDecimalPlaces;
                itemDet.AmountDecimal = $scope.SelectedVoucher.Product.AmountNoOfDecimalPlaces;
            } else {
                var findUnit = $scope.UnitColl.firstOrDefault(p1 => p1.UnitId == itemDet.productDetail.BaseUnitId);
                if (findUnit) {
                    itemDet.QtyDecimal = findUnit.NoOfDecimalPlaces;
                    itemDet.RateDecimal = findUnit.RateNoOfDecimalPlaces;
                    itemDet.AmountDecimal = findUnit.AmountNoOfDecimalPlaces;
                }
            }

            var clQty = ($filter('number')(itemDet.productDetail.ClosingQty, itemDet.QtyDecimal)).parseDBL();
            itemDet.productDetail.ClosingQty = clQty;
            itemDet.ClosingQty = clQty + ' ' + itemDet.productDetail.BaseUnit;

            if (isEmptyObj(itemDet.QtyDecimal))
                itemDet.QtyDecimal = 0;

            if (isEmptyObj(itemDet.RateDecimal))
                itemDet.RateDecimal = 2;

            if (isEmptyObj(itemDet.AmountDecimal))
                itemDet.AmountDecimal = 2;

            if (itemDet.productDetail.IsFixedProduct == true) {
                if (!itemDet.FixedProductColl || itemDet.FixedProductColl.length == 0) {
                    $http({
                        method: 'GET',
                        url: base_url + "Inventory/Transaction/getDueFixedProductList?productId=" + itemDet.productDetail.ProductId,
                        dataType: "json"
                    }).then(function (res) {
                        if (res.data.IsSuccess && res.data.Data) {
                            itemDet.productDetail.FixedProductColl = res.data.Data;
                            itemDet.FixedProductColl = res.data.Data;

                            if (isModify) {
                                itemDet.productDetail.FixedProductColl.push({
                                    EngineNo: itemDet.EngineNo,
                                    ChassissNo: itemDet.ChassissNo,
                                    RegdNo: itemDet.RegdNo,
                                    Model: itemDet.Model,
                                    Type: itemDet.Type,
                                    Color: itemDet.Color,
                                    KeyNo: itemDet.KeyNo,
                                    CodeNo: itemDet.CodeNo,
                                    MFGYear: itemDet.MFGYear,
                                });
                            }

                            angular.forEach(itemDet.FixedProductColl, function (fp) {
                                fp.Amount = itemDet.Rate;
                            });

                            if ($scope.SelectedVoucher.MultipleFixedProduct == true)
                                $('#mdlFixedProduct').modal('show');
                        }
                    }, function (reason) {
                        Swal.fire('Failed' + reason);
                    });
                } else {
                    if ($scope.SelectedVoucher.MultipleFixedProduct == true)
                        $('#mdlFixedProduct').modal('show');
                }
            }


            $scope.ChangeItemRowValue(itemDet, 'product');

            $timeout(function () {
                if (itemDet.productDetail.BatchWiseColl && itemDet.productDetail.BatchWiseColl.length > 0) {
                    itemDet.Batch = itemDet.productDetail.BatchWiseColl[0].BatchNo;
                    $scope.ChangeBatch(itemDet);
                }
            });


            var itemC = mx($scope.beData.ItemDetailsColl).where(p1 => p1.RowType == 'P').count();
            if (ind == (itemC - 1))
                $scope.AddRowInTable(ind);

        }

    }

    $scope.ChangeBatch = function (itemDet) {
        if (itemDet.Batch && itemDet.Batch.length > 0 && itemDet.productDetail) {

            var findB = mx(itemDet.productDetail.BatchWiseColl).firstOrDefault(p1 => p1.BatchNo == itemDet.Batch);
            if (findB) {
                itemDet.EXPDate = findB.EXPDate;
                itemDet.MFGDate = findB.MFGDate;
                itemDet.BatchBalQty = findB.BalQty;
                itemDet.EngineNo = findB.EngineNo;

                itemDet.SalesRate = findB.SalesRate;
                itemDet.TradeRate = findB.TradeRate;
                itemDet.MRP = findB.MRP;

                if (findB.SalesRate > 0)
                    itemDet.Rate = findB.SalesRate;
            }

        } else {
            itemDet.EXPDate = null;
            itemDet.MFGDate = null;
            itemDet.BatchBalQty = 0;
            itemDet.EngineNo = '';

            itemDet.SalesRate = 0;
            itemDet.TradeRate = 0;
            itemDet.MRP = 0;
        }

        $scope.RecalculateAdditioncalCost();
        $scope.CalculateTotalAndSubTotal();
    }
    $scope.AditionalCostSelectionChange = function (itemDet, ind) {

        $timeout(function () {
            $scope.sideBarData = itemDet.costSideBarData;

            if (itemDet.LedgerId == null || itemDet.LedgerId == 0) {
                itemDet.Rate = 0;
                itemDet.Amount = 0;
                itemDet.AccessableValue = 0;
            }
            else if (itemDet.LedgerId > 0 && (itemDet.costLedgerDetail == null || itemDet.costLedgerDetail === undefined)) {
                $scope.RefreshRowData(itemDet, ind);
            }
            else if (itemDet.costLedgerDetail) {
                itemDet.Rate = (itemDet.costLedgerDetail.Rate ? itemDet.costLedgerDetail.Rate : 0);
                itemDet.AccessableValue = 0;
                itemDet.Amount = 0;

            }

            var i = 0
            angular.forEach($scope.beData.ItemDetailsColl, function (idet) {
                if (idet.RowType == 'L') {
                    $scope.ChangeAdditionalRate(idet, 'rate', i);
                }
                i++;
            });
        });


    }
    $scope.ChangePaymentTemrs = function () {
        if ($scope.beData.DispatchOrderDetail.TermsOfPayment == 'CASH') {
            if ($scope.RecVoucherTypeColl && $scope.RecVoucherTypeColl.length > 0)
                $scope.beData.DispatchOrderDetail.ReceiptVoucherId = $scope.RecVoucherTypeColl[0].VoucherId;
            else
                $scope.beData.DispatchOrderDetail.ReceiptVoucherId = null;
        };
    }
    $scope.lastPartyLedgerId = null;
    $scope.PartySelectionChange = function (partyDet) {

        var isModify = $scope.beData.TranId > 0 ? true : false;

        $scope.sideBarData = partyDet.partySideBarData;

        if (partyDet.PartyLedgerId && partyDet.PartyLedgerId > 0) {
            if (partyDet.PartyLedger) {

                if ($scope.lastPartyLedgerId != partyDet.PartyLedgerId && isModify == false) {

                    $scope.ClearItemDetails();
                    $scope.beData.RefAllotationIdColl = [];
                    $scope.RefItemAllocationColl = [];
                    $scope.RefAditionalCostColl = [];
                    partyDet.DispatchOrderDetail = {};


                    partyDet.DispatchOrderDetail.TermsOfPayment = partyDet.PartyLedger.PaymentType;

                    if (partyDet.PartyLedger.BillToColl && partyDet.PartyLedger.BillToColl.length > 0)
                        partyDet.DispatchOrderDetail.BillToId = partyDet.PartyLedger.BillToColl[0].Id;

                    if (partyDet.PartyLedger.ShipToColl && partyDet.PartyLedger.ShipToColl.length > 0)
                        partyDet.DispatchOrderDetail.ShipToId = partyDet.PartyLedger.ShipToColl[0].Id;

                    $scope.ChangeBillAddress();
                    $scope.ChangeShipAddress();

                    partyDet.DispatchOrderDetail.CreditDays = partyDet.PartyLedger.CreditLimitDays;
                    partyDet.DispatchOrderDetail.Buyes = partyDet.PartyLedger.Name;
                    partyDet.DispatchOrderDetail.Address = partyDet.PartyLedger.Address;
                    partyDet.DispatchOrderDetail.SalesTaxNo = partyDet.PartyLedger.PanVat;
                    partyDet.DispatchOrderDetail.PhoneNo = partyDet.PartyLedger.MobileNo1;

                    $scope.lastPartyLedgerId = partyDet.PartyLedgerId;
                }
            }

            if ($scope.SelectedVoucher.ActivePartyDetails == true) {
                $scope.RefVoucherChange($scope.beData.RefVoucherType);
                $('#frmDispatchOrderDetailsModel').modal('show');
            }


        } else {

            $scope.lastPartyLedgerId = null;
            $scope.search = "";
            $scope.RefVoucherNoColl = [];
            $scope.RefItemAllocationColl = [];

            partyDet.DispatchOrderDetail = {};
            //partyDet.ItemDetailsColl = [];
            //$scope.AddRowInItemDetails(0);
            $scope.RecalculateAdditioncalCost();
            $scope.CalculateTotalAndSubTotal();
            $('#frmDispatchOrderDetailsModel').modal('hide');
        }



    };
    $scope.ChangeBillAddress = function () {

        if ($scope.beData.PartyLedger.BillToColl) {
            var find = mx($scope.beData.PartyLedger.BillToColl).firstOrDefault(p1 => p1.Id == $scope.beData.DispatchOrderDetail.BillToId);
            if (find) {
                $scope.beData.DispatchOrderDetail.BillToAddress = find.ConcatAddress;
            }
        }
    }
    $scope.ChangeShipAddress = function () {
        if ($scope.beData.PartyLedger.ShipToColl) {
            var find = mx($scope.beData.PartyLedger.ShipToColl).firstOrDefault(p1 => p1.Id == $scope.beData.DispatchOrderDetail.ShipToId);
            if (find) {
                $scope.beData.DispatchOrderDetail.ShipToAddress = find.ConcatAddress;
            }
        }
    }

    $scope.CheckedAll = false;
    $scope.RefAditionalCostColl = [];
    $scope.CheckedAllRefItem = function () {
        var lstSelected = null;
        angular.forEach($scope.RefItemAllocationColl, function (ri) {
            ri.IsSelected = $scope.CheckedAll;

            if (ri.IsSelected == true)
                lstSelected = ri;
        });

        if (lstSelected)
            $scope.getRefVoucherPartyDetails(lstSelected);
    }

    $scope.IsRefPartyDet = false;
    $scope.getRefVoucherPartyDetails = function (refItem) {

        if ($scope.beData.RefVoucherType && refItem && refItem.IsSelected == true) {

            var vType = 13;

            if ($scope.beData.RefVoucherType == 1)
                vType = 13;
            else if ($scope.beData.RefVoucherType == 2)
                vType = 11;

            var para = {
                TranId: refItem.TranId,
                voucherTypes: vType
            };

            $http({
                method: 'POST',
                url: base_url + "Inventory/Transaction/GetADForRefTran",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res1) {
                if (res1.data.IsSuccess && res1.data.Data) {
                    var tranData = res1.data.Data;
                    var tranDet = tranData.SalesInvoiceDetail;

                    $timeout(function () {

                        $scope.IsRefPartyDet = true;

                        $scope.beData.PartyCostCenter = tranData.PartyCostCenter;
                        $scope.beData.TranCostCenter = tranData.TranCostCenter;
                        $scope.beData.AgentId = tranData.AgentId;

                        $scope.beData.DispatchOrderDetail.TermsOfPayment = tranDet.TermsOfPayment;
                        $scope.beData.DispatchOrderDetail.OtherRefereces = tranDet.OtherRefereces;
                        $scope.beData.DispatchOrderDetail.TermsOfDelivery = tranDet.TermsOfDelivery;
                        $scope.beData.DispatchOrderDetail.DeliveryThrough = tranDet.DeliveryThrough;
                        $scope.beData.DispatchOrderDetail.DeliveryDocNo = tranDet.DeliveryDocNo;
                        $scope.beData.DispatchOrderDetail.Destination = tranDet.Destination;
                        $scope.beData.DispatchOrderDetail.SalesQuotationTermsOfPayment = tranDet.SalesQuotationTermsOfPayment;
                        $scope.beData.DispatchOrderDetail.SalesQuotationOtherRefereces = tranDet.SalesQuotationOtherRefereces;
                        $scope.beData.DispatchOrderDetail.SalesQuotationTermsOfDelivery = tranDet.SalesQuotationTermsOfDelivery;
                        $scope.beData.DispatchOrderDetail.Buyes = tranDet.Buyes;
                        $scope.beData.DispatchOrderDetail.Address = tranDet.Address;
                        $scope.beData.DispatchOrderDetail.SalesTaxNo = tranDet.SalesTaxNo;
                        $scope.beData.DispatchOrderDetail.CSTNumber = tranDet.CSTNumber;
                        $scope.beData.DispatchOrderDetail.Notes = tranDet.Notes;
                        $scope.beData.DispatchOrderDetail.PhoneNo = tranDet.PhoneNo;
                        $scope.beData.DispatchOrderDetail.Description = tranDet.Description;
                        $scope.beData.DispatchOrderDetail.OwnerName = tranDet.OwnerName;
                        $scope.beData.DispatchOrderDetail.OwnerContactNo = tranDet.OwnerContactNo;
                        $scope.beData.DispatchOrderDetail.DriverAddress = tranDet.DriverAddress;
                        $scope.beData.DispatchOrderDetail.DriverName = tranDet.DriverName;
                        $scope.beData.DispatchOrderDetail.DriverContactNo = tranDet.DriverContactNo;
                        $scope.beData.DispatchOrderDetail.LicenseNo = tranDet.LicenseNo;
                        $scope.beData.DispatchOrderDetail.Goods = tranDet.Goods;
                        $scope.beData.DispatchOrderDetail.Quantity = tranDet.Quantity;
                        $scope.beData.DispatchOrderDetail.FreightRate = tranDet.FreightRate;
                        $scope.beData.DispatchOrderDetail.AdvancePayment = tranDet.AdvancePayment;
                        $scope.beData.DispatchOrderDetail.TotalWT = tranDet.TotalWT;
                        $scope.beData.DispatchOrderDetail.ContactNo = tranDet.ContactNo;
                        $scope.beData.DispatchOrderDetail.RegdNo = tranDet.RegdNo;
                        $scope.beData.DispatchOrderDetail.EngineNo = tranDet.EngineNo;
                        $scope.beData.DispatchOrderDetail.ChassisNo = tranDet.ChassisNo;
                        $scope.beData.DispatchOrderDetail.Model = tranDet.Model;
                        $scope.beData.DispatchOrderDetail.VinNo = tranDet.VinNo;
                        $scope.beData.DispatchOrderDetail.CreditDays = tranDet.CreditDays;
                        $scope.beData.DispatchOrderDetail.ExportCountry = tranDet.ExportCountry;
                        $scope.beData.DispatchOrderDetail.PPNo = tranDet.PPNo;
                        $scope.beData.DispatchOrderDetail.PPDate = tranDet.PPDate;
                        $scope.beData.DispatchOrderDetail.OtherRefereces1 = tranDet.OtherRefereces1;
                        $scope.beData.DispatchOrderDetail.PaymentTermsId = tranDet.PaymentTermsId;
                        $scope.beData.DispatchOrderDetail.FreightTypeId = tranDet.FreightTypeId;
                        $scope.beData.Narration = tranData.Narration;
                        $scope.beData.No = tranData.RefNo;
                        $scope.beData.RefNo = tranData.RefNo;
                        $scope.beData.DispatchOrderDetail.BillToId = tranDet.BillToId;
                        $scope.beData.DispatchOrderDetail.ShipToId = tranDet.ShipToId;
                        $scope.beData.DispatchOrderDetail.BillToAddress = tranDet.BillToAddress;
                        $scope.beData.DispatchOrderDetail.ShipToAddress = tranDet.ShipToAddress;

                        $scope.RefAditionalCostColl = tranData.AditionalCostColl;
                    });



                }
            }, function (reason) {
                Swal.fire('Failed' + reason);
            });


        }


    };

    $scope.LoadRefProduct = function () {

        //if ($scope.RefItemAllocationColl && $scope.RefItemAllocationColl.length > 0) {
        //    $('#frmDispatchOrderDetailsModel').modal('hide');
        //    return;
        //}

        $scope.beData.RefAllotationIdColl = [];
        var filterData = [];
        angular.forEach($scope.RefItemAllocationColl, function (node) {
            if (node.IsSelected == true) {
                $scope.beData.RefAllotationIdColl.push(node.ItemAllocationId);
                filterData.push(node);
            }
        });

        var refVType = $scope.beData.RefVoucherType;

        if (filterData.length > 0) {
            $scope.beData.ItemDetailsColl = [];
            var tmpItemAllocationColl = [];
            angular.forEach(filterData, function (fd) {
                var refItem = {
                    RowType: 'P',
                    ProductId: fd.ProductId,
                    productDetail: null,
                    ActualQty: fd.ActualQty,
                    BilledQty: fd.BilledQty,
                    FreeQty: (fd.ActualQty - fd.BilledQty),
                    Rate: fd.Rate,
                    DiscountPer: fd.DiscountPer,
                    DiscountAmt: 0,
                    SchameAmt: 0,
                    SchamePer: 0,
                    Amount: fd.Amount,
                    Description: fd.Description,
                    QtyPoint: 0,
                    UnitId: fd.UnitId,
                    CanEditRate: false,
                    ALValue1: fd.AUQty1,
                    ALValue2: fd.AUQty2,
                    ALUnitId1: fd.AUId1,
                    ALUnitId2: fd.AUId2,
                    SchemeAmt: 0,
                    SchemeRate: 0,
                    ExciseAbleQty: 0,
                    ExciseAbtAmt: 0,
                    VatAbleAmt: 0,
                    VatRate: 0,
                    VatAmount: 0,
                    ExDutyRate: 0,
                    ExDutyAmount: 0,
                    QtyDecimal: 2,
                    RateDecimal: 2,
                    AmountDecimal: 2,
                    GodownId: (fd.GodownId && fd.GodownId > 0 ? fd.GodownId : $scope.beData.GodownId),
                    Narration: fd.Narration,
                    RefQty: fd.ActualQty,
                    OrderItemAllocationId: refVType == 1 ? fd.ItemAllocationId : null,
                    RefVType: refVType,
                    ProductLedgerId: fd.ProductLedgerId,
                    LedgerId: fd.ProductLedgerId
                };
                tmpItemAllocationColl.push(refItem);
            });

            $timeout(function () {

                $scope.loadingstatus = 'running';
                showPleaseWait();

                var newSales = {
                    ItemAllocationColl: tmpItemAllocationColl
                };

                var fnName = 'GetSalesOrderDetailsByItemAllocationId';

                if (refVType == 1)
                    fnName = 'GetSalesOrderDetailsByItemAllocationId';
                else if (refVType == 2)
                    fnName = 'GetSalesQuotaionDetailsByItemAllocationId';


                //refVType == 2 ? 'GetSalesOrderDetailsByItemAllocationId' : 'GetDispatchOrderDetailsByItemAllocationId';
                $http({
                    method: 'POST',
                    url: base_url + "Inventory/Transaction/" + fnName,
                    dataType: "json",
                    data: JSON.stringify(newSales)
                }).then(function (res1) {

                    $scope.loadingstatus = 'stop';
                    hidePleaseWait();

                    if (res1.data.IsSuccess == true) {

                        angular.forEach(res1.data.Data.ItemAllocationColl, function (ias) {
                            ias.RowType = 'P';
                            ias.Narration = ias.Narration;
                            ias.ReadOnlyQty = $scope.SelectedVoucher.Product.RefQtyAs == 2 ? true : false;

                            if (ias.ItemDetailsColl && ias.ItemDetailsColl.length > 0) {
                                angular.forEach(ias.ItemDetailsColl, function (iad) {
                                    iad.OrderItemAllocationId = ias.OrderItemAllocationId;
                                    iad.RowType = 'P';
                                    iad.Narration = ias.Narration;
                                    iad.ReadOnlyQty = $scope.SelectedVoucher.Product.RefQtyAs == 2 ? true : false;
                                    iad.ProductLedgerId = ias.LedgerId;
                                    iad.LedgerId = ias.LedgerId;
                                    $scope.beData.ItemDetailsColl.push(iad);
                                });
                            } else
                                $scope.beData.ItemDetailsColl.push(ias);

                            //$scope.beData.ItemDetailsColl.push(ias);
                        });

                        angular.forEach($scope.RefAditionalCostColl, function (ads) {
                            $scope.beData.ItemDetailsColl.push({
                                RowType: 'L',
                                LedgerId: ads.LedgerId,
                                ledgerDetail: null,
                                ActualQty: 0,
                                BilledQty: 0,
                                FreeQty: 0,
                                Rate: ads.Rate,
                                DiscountPer: 0,
                                DiscountAmt: 0,
                                SchameAmt: 0,
                                SchamePer: 0,
                                Amount: ads.Amount,
                                Description: '',
                                QtyPoint: 0,
                                UnitId: null,
                                AutoCharge: true,
                                CanEditRate: $scope.SelectedVoucher.Product.RefQtyAs == 2 ? false : true,
                                ALValue1: 0,
                                ALValue2: 0,
                                ALUnitId1: null,
                                ALUnitId2: null,
                                SchemeAmt: 0,
                                SchemeAmt: 0,
                                QtyDecimal: 2,
                                RateDecimal: 2,
                                AmountDecimal: 2,
                                Narration: ads.Narration
                            });
                        })
                        //$timeout(function () {
                        //   // angular.forEach(res1.Data.)
                        //});

                        $scope.RecalculateAdditioncalCost();
                        $scope.CalculateTotalAndSubTotal();
                    }


                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });

            });
        }

        $('#frmDispatchOrderDetailsModel').modal('hide');
    };
    $scope.RefVoucherChange = function (refVType) {

        $scope.CheckedAll = false;
        //$scope.RefAditionalCostColl = [];

        $scope.RefVoucherNoColl = [];
        $scope.RefItemAllocationColl = [];

        var funName = "getPendinSalesOrder"

        if (refVType == 1)
            funName = "getPendinSalesOrder";
        else if (refVType == 2)
            funName = "getPendinSalesQuotation";

        var agentId = 0;
        if ($scope.beData.AgentId)
            agentId = $scope.beData.AgentId;

        var vDate = null;

        if ($scope.beData.VoucherDateDet) {
            vDate = $filter('date')(new Date($scope.beData.VoucherDateDet.dateAD), 'yyyy-MM-dd');
        } else
            vDate = $filter('date')(new Date(), 'yyyy-MM-dd');

        var para = "ledgerId=" + $scope.beData.PartyLedgerId + "&agentId=" + agentId + "&voucherDate=" + vDate;
        $http({
            method: 'GET',
            url: base_url + "Inventory/Transaction/" + funName + "?" + para,
            dataType: "json"
        }).then(function (res1) {
            if (res1.data.IsSuccess && res1.data.Data) {
                $scope.RefItemAllocationColl = res1.data.Data;

                var refTranIdColl = mx($scope.beData.RefTranIdColl);
                var refAllocationIdCol = mx($scope.beData.RefAllotationIdColl);

                angular.forEach($scope.RefItemAllocationColl, function (ri) {
                    if (refTranIdColl.contains(ri.TranId)) {
                        ri.IsSelected = true;
                    } else {
                        ri.IsSelected = false;

                        if (refAllocationIdCol.contains(ri.ItemAllocationId)) {
                            ri.IsSelected = true;
                        } else
                            ri.IsSelected = false;
                    }

                });

                var grp = mx($scope.RefItemAllocationColl)
                    .groupBy(t => ({ id: t.TranId, text: t.AutoManualNo }))   // group `key`
                    .select(t => t.key)
                    .toArray();

                angular.forEach(grp, function (v) {
                    $scope.RefVoucherNoColl.push({
                        id: v.id,
                        text: v.text.toString().trim()
                    });
                });

                //$('#frmSalesReturnDetailsModel').modal('show');

            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };

    //AditionalCostOnTheBasisOf {
    //    Quantity=0,
    //    Amount=1
    //} 

    $scope.CalculateTotalAndSubTotal = function () {

        if ($scope.SelectedVoucher) {
            var subTotal = 0;
            var totalQty = 0;
            angular.forEach($scope.beData.ItemDetailsColl, function (item) {
                subTotal += item.Amount;
                totalQty += item.ActualQty;
            });

            $scope.beData.SubTotal = ($filter('number')(subTotal, $scope.SelectedVoucher.NoOfDecimalPlaces)).parseDBL();
            $scope.beData.TotalAmount = ($filter('number')(subTotal, $scope.SelectedVoucher.NoOfDecimalPlaces)).parseDBL();
        }

    };

    $scope.ChangeTranQtyItemRowValue = function (itemDet, col) {

        if (!itemDet.TranUnitId || itemDet.TranUnitId == 0 || !itemDet.productDetail)
            return;

        if (itemDet.productDetail.BaseUnitId == itemDet.TranUnitId) {
            itemDet.ActualQty = itemDet.TranUnitQty;
            $scope.ChangeItemRowValue(itemDet, 'rate');
            return;
        }

        if (itemDet.productDetail.AlternetUnitColl) {
            var alterUnit_Qry = mx(itemDet.productDetail.AlternetUnitColl);

            var alternetUnit1 = null;

            alternetUnit1 = alterUnit_Qry.firstOrDefault(p1 => p1.UnitId == itemDet.TranUnitId);
            var baseQty = 0;
            var findUnit = $scope.UnitColl.firstOrDefault(p1 => p1.UnitId == itemDet.productDetail.BaseUnitId);
            if (alternetUnit1 && findUnit) {
                baseQty = parseFloat(parseFloat(itemDet.TranUnitQty * alternetUnit1.BaseUnitValue).toFixed(findUnit.NoOfDecimalPlaces));
                itemDet.ActualQty = baseQty;
                itemDet.ALValue1 = itemDet.TranUnitQty;
                itemDet.ALUnitId1 = itemDet.TranUnitId;
            }
        }

        $scope.ChangeItemRowValue(itemDet, 'rate');
    }

    $scope.ChangeItemRowValue = function (itemDet, col) {

        if (col == 'rate' || col == 'amt') {
            if (mx(itemDet.Rate).contains('=') == true || mx(itemDet.Amount).contains('=') == true) {
                return;
            }
        }

        if (col == 'rate') {
            if (itemDet.FixedProductColl && itemDet.FixedProductColl.length > 0) {
                for (var ind = 0; ind < itemDet.FixedProductColl.length; ind++) {
                    var fp = $scope.CurItemAllocation.FixedProductColl[ind];
                    fp.Rate = itemDet.Rate;
                    fp.Amount = itemDet.Rate;
                }
            }
        }

        var amt = 0, qty = 0, rate = 0, disAmt = 0, disPer = 0, schAmt = 0, schPer = 0;

        var aQty = 0;

        var rateOf = 1;
        if (itemDet.productDetail) {
            rateOf = itemDet.productDetail ? itemDet.productDetail.RateOf : 1;
            if (rateOf == 0)
                rateOf = 1;
        }

        var nw = 0, lw = 0, mk = 0, st = 0;
        if ($scope.comDet.Maintain == 2) {

            if (itemDet.productDetail) {
                if (col == "netWeight") {
                    if (itemDet.NetWeight && itemDet.NetWeight > 0)
                        itemDet.LossWeight = itemDet.NetWeight * itemDet.LossRate / 100;
                }
                else if (col == "lossRate") {
                    if (itemDet.NetWeight && itemDet.NetWeight > 0)
                        itemDet.LossWeight = itemDet.NetWeight * itemDet.LossRate / 100;
                }
                else if (col == "fineRate") {
                    if (itemDet.NetWeight && itemDet.NetWeight > 0)
                        itemDet.FineWeight = itemDet.NetWeight * itemDet.FineRate / 100;
                }
                else if (col == "processingRate") {
                    if (itemDet.NetWeight && itemDet.NetWeight > 0)
                        itemDet.ProcessingWeight = itemDet.NetWeight * itemDet.ProcessingRate / 100;
                }
            }

            if ($scope.SelectedVoucher.Jewellery.Loss == true)
                itemDet.ActualQty = itemDet.NetWeight + itemDet.LossWeight;
            else
                itemDet.ActualQty = itemDet.FineWeight + itemDet.ProcessingWeight;
        }

        if (col == 'aQty') {
            itemDet.BilledQty = itemDet.ActualQty;


        } else if (col == 'bQty') {
            if (itemDet.ActualQty == 0 || itemDet.ActualQty < itemDet.BilledQty)
                itemDet.ActualQty = itemDet.BilledQty;
        }

        if (itemDet.productDetail) {
            if (itemDet.Batch && itemDet.Batch.length > 0) {
                var bBal = itemDet.BatchBalQty || itemDet.BatchBalQty > 0 ? itemDet.BatchBalQty : 0;
                if (itemDet.ActualQty > bBal) {

                    if (itemDet.productDetail.IgnoreNegativeBalance == false) {
                        itemDet.ActualQty = 0;
                        itemDet.BilledQty = 0;
                        Swal.fire('Please ! Enter Qty Less Then Equal ' + bBal);
                    }
                }
            }
            else {
                //if ((itemDet.productDetail.ClosingQty + (itemDet.RefQty > 0 ? itemDet.RefQty : 0)) < itemDet.ActualQty) {
                if ((itemDet.productDetail.ClosingQty + (itemDet.RefQty > 0 ? itemDet.RefQty : 0)) < itemDet.ActualQty) {
                    if (itemDet.productDetail.IgnoreNegativeBalance == false) {
                        itemDet.ActualQty = 0;
                        itemDet.BilledQty = 0;
                        Swal.fire('Please ! Enter Qty Less Then Equal ' + itemDet.productDetail.ClosingQty);
                    }

                }
            }
        }

        if (itemDet.ActualQty)
            aQty = itemDet.ActualQty;

        if ($scope.HideShow.BilledQty == true) {
            if (itemDet.ActualQty)
                qty = itemDet.ActualQty;
        } else {
            if (itemDet.BilledQty)
                qty = itemDet.BilledQty;
        }


        if (isEmptyObj(itemDet.Rate) == false)
            rate = itemDet.Rate;

        rate = ($filter('number')(rate / rateOf, itemDet.RateDecimal)).parseDBL();


        if (itemDet.productDetail) {
            if (itemDet.productDetail.ClosingQty < qty)
                itemDet.IsNegativeQty = true;
            else if (itemDet.RefQty && itemDet.RefQty < qty)
                itemDet.IsNegativeQty = true;
            else
                itemDet.IsNegativeQty = false;
        }

        if (itemDet.Amount && col == "amt" && itemDet.Amount > 0) {
            if ($scope.SelectedVoucher.Product.ChangeAmtReflectToRateQty == 2) {
                rate = itemDet.Amount / itemDet.BilledQty;
                itemDet.Rate = rate;
            } else {
                itemDet.BilledQty = itemDet.Amount / itemDet.Rate;
                itemDet.ActualQty = itemDet.BilledQty;

                qty = itemDet.ActualQty;
            }
            amt = itemDet.Amount;
        } else
            amt = qty * rate;

        if (itemDet.DiscountAmt)
            disAmt = itemDet.DiscountAmt;

        if (itemDet.DiscountPer)
            disPer = itemDet.DiscountPer;

        var discountAbleAmt = 0;
        var qtyWiseDis = false;
        if (itemDet.productDetail) {
            if (itemDet.productDetail.DiscountOn != null) {

                if (itemDet.productDetail.DiscountOn == 1) // On Qty
                {
                    qtyWiseDis = true;
                    discountAbleAmt = qty;
                } else {
                    discountAbleAmt = amt;
                }
            } else {
                if ($scope.SelectedVoucher.Product.DiscountOn == 1) {
                    qtyWiseDis = true;
                    discountAbleAmt = qty;
                }
                else
                    discountAbleAmt = amt;
            }
        }
        else {
            discountAbleAmt = amt;
        }


        if (col == "disAmt") {

            if (disAmt > 0) {
                disPer = (disAmt / discountAbleAmt) * (qtyWiseDis == true ? 1 : 100);
            } else
                disPer = 0;

        }
        else if (col == "disPer" || col == "product") {

            if (disPer > 0) {
                disAmt = discountAbleAmt * disPer / (qtyWiseDis == true ? 1 : 100);
            } else
                disAmt = 0;
        } else if (disPer > 0) {
            disAmt = discountAbleAmt * disPer / (qtyWiseDis == true ? 1 : 100);
        } else if (disAmt > 0) {
            disPer = (disAmt / discountAbleAmt) * (qtyWiseDis == true ? 1 : 100);
        }



        itemDet.Amount = ($filter('number')((amt - disAmt), itemDet.AmountDecimal)).parseDBL();

        if (col == "disAmt")
            itemDet.DiscountPer = disPer;
        else if (col == "disPer" || col == "product")
            itemDet.DiscountAmt = disAmt;
        else {
            itemDet.DiscountPer = disPer;
            itemDet.DiscountAmt = disAmt;
        }

        itemDet.TotalAmount = itemDet.Amount + itemDet.Makeing + itemDet.Stone;

        if ($scope.HideShow.BilledQty == true) {
            itemDet.BilledQty = aQty;
        }

        if (itemDet.productDetail) {
            if (itemDet.productDetail.AlternetUnitColl) {
                if (col == 'aQty' || col == 'bQty' || col == 'product') {
                    var alterUnit_Qry = mx(itemDet.productDetail.AlternetUnitColl);

                    var alternetUnit1 = null, alternetUnit2 = null;

                    if (itemDet.productDetail.AlternetUnitColl.length > 0) {

                        alternetUnit1 = alterUnit_Qry.firstOrDefault(p1 => p1.SNo == 1);
                        if (alternetUnit1) {
                            itemDet.ALValue1 = parseFloat(parseFloat((alternetUnit1.AlternetUnitValue * aQty) / alternetUnit1.BaseUnitValue).toFixed(alternetUnit1.NoOfDecimalPlaces));
                            itemDet.ALUnitId1 = alternetUnit1.UnitId;
                            itemDet.UnitName1 = alternetUnit1.UnitName;
                        }
                    }

                    if (itemDet.productDetail.AlternetUnitColl.length > 1) {
                        alternetUnit2 = alterUnit_Qry.firstOrDefault(p1 => p1.SNo == 2);
                        if (alternetUnit2) {
                            itemDet.ALValue2 = parseFloat(parseFloat((alternetUnit2.AlternetUnitValue * aQty) / alternetUnit2.BaseUnitValue).toFixed(alternetUnit2.NoOfDecimalPlaces));
                            itemDet.ALUnitId2 = alternetUnit2.UnitId;
                            itemDet.UnitName2 = alternetUnit2.UnitName;
                        }
                    }
                }
            }

            var exciseAbleQty = 0;
            var excisAbleAmt = itemDet.ActualQty * itemDet.Rate;

            if (itemDet.productDetail.ExDutyUnitId && itemDet.productDetail.ExDutyUnitId > 0) {
                if (itemDet.UnitId == itemDet.productDetail.ExDutyUnitId)
                    exciseAbleQty = itemDet.ActualQty;
                else if (itemDet.ALUnitId1 && itemDet.ALUnitId1 == itemDet.productDetail.ExDutyUnitId)
                    exciseAbleQty = itemDet.ALValue1;
                else if (itemDet.ALUnitId2 && itemDet.ALUnitId2 == itemDet.productDetail.ExDutyUnitId)
                    exciseAbleQty = itemDet.ALValue1;
            }
            else
                exciseAbleQty += itemDet.ActualQty;

            itemDet.ExciseAbleQty = exciseAbleQty;
            itemDet.ExciseAbtAmt = excisAbleAmt;
            itemDet.VatAbleAmt = 0;

            if (itemDet.productDetail.IsTaxable == true) {
                itemDet.VatAbleAmt = itemDet.Amount;
                itemDet.TaxableAmt = itemDet.Amount;

                if ($scope.SelectedVoucher.Product.ProductWiseVat == false) {
                    itemDet.VatRate = 0;
                    itemDet.VatAmount = 0;
                }
                else if ($scope.SelectedVoucher.Product.ProductWiseVat == true) {
                    itemDet.VatRate = itemDet.productDetail.VatRate;
                    itemDet.VatAmount = itemDet.Amount * itemDet.productDetail.VatRate / 100;
                }
            }

            if ($scope.SelectedVoucher.Product.ProductWiseExciseDuty == false) {
                itemDet.ExDutyRate = 0;
                itemDet.ExDutyAmount = 0;
            }
            else if ($scope.SelectedVoucher.Product.ProductWiseExciseDuty == true) {

                if (itemDet.productDetail.ExciseOn == 1) {
                    itemDet.ExDutyRate = itemDet.productDetail.EXDutyRate;
                    itemDet.ExDutyAmount = exciseAbleQty * itemDet.productDetail.EXDutyRate;
                } else {
                    itemDet.ExDutyRate = itemDet.productDetail.EXDutyRate;
                    itemDet.ExDutyAmount = excisAbleAmt * itemDet.productDetail.EXDutyRate / 100;
                }

            }

        }

        itemDet.TotalAmount = itemDet.Amount + itemDet.Makeing + itemDet.Stone;
        $scope.RecalculateAdditioncalCost();
        $scope.CalculateTotalAndSubTotal();
    }

    $scope.ChangeItemAlternetQty = function (itemDet, col) {

        var aQty = 0;

        if (col == 'aQty') {
            itemDet.BilledQty = itemDet.ActualQty;
        } else if (col == 'bQty') {
            if (itemDet.ActualQty == 0 || itemDet.ActualQty < itemDet.BilledQty)
                itemDet.ActualQty = itemDet.BilledQty;
        }

        if (itemDet.ActualQty)
            aQty = itemDet.ActualQty;


        if (itemDet.productDetail) {
            if (itemDet.productDetail.AlternetUnitColl) {
                var alterUnit_Qry = mx(itemDet.productDetail.AlternetUnitColl);

                var alternetUnit1 = null, alternetUnit2 = null;

                alternetUnit1 = alterUnit_Qry.firstOrDefault(p1 => p1.SNo == 1);
                alternetUnit2 = alterUnit_Qry.firstOrDefault(p1 => p1.SNo == 2);
                var baseQty = 0;
                var findUnit = $scope.UnitColl.firstOrDefault(p1 => p1.UnitId == itemDet.productDetail.BaseUnitId);
                if (alternetUnit1 && col == 'unit1') {

                    if (findUnit) {
                        baseQty = parseFloat(parseFloat(itemDet.ALValue1 / alternetUnit1.AlternetUnitValue).toFixed(findUnit.NoOfDecimalPlaces));

                        if ($scope.SelectedVoucher.Product.NotEffectInBaseUnit == false)
                            itemDet.ActualQty = baseQty;

                        if (alternetUnit2)
                            itemDet.ALValue2 = parseFloat(parseFloat((alternetUnit2.AlternetUnitValue * baseQty) / alternetUnit2.BaseUnitValue).toFixed(alternetUnit2.NoOfDecimalPlaces));
                    }

                } else if (alternetUnit2 && col == "unit2") {

                    if (findUnit) {
                        baseQty = parseFloat(parseFloat(itemDet.ALValue2 * alternetUnit2.AlternetUnitValue).toFixed(findUnit.NoOfDecimalPlaces));

                        if ($scope.SelectedVoucher.Product.NotEffectInBaseUnit == false)
                            itemDet.ActualQty = baseQty;

                        if (alternetUnit1)
                            itemDet.ALValue1 = parseFloat(parseFloat((alternetUnit1.AlternetUnitValue * baseQty) / alternetUnit1.BaseUnitValue).toFixed(alternetUnit1.NoOfDecimalPlaces));
                    }

                }
            }
        }

        $scope.ChangeItemRowValue(itemDet, 'rate');

    }

    $scope.ChangeAdditionalRate = function (itemDet, col, ind) {

        $scope.RecalculateAdditioncalCost();
        $scope.CalculateTotalAndSubTotal();
    }

    //TypeOfDutyTaxs {
    //    OTHERS=0,
    //    VAT=1,
    //    TSC=2,
    //    EXCISE=3,
    //    CST=4,
    //    TDS=5,
    //    SCHEME=6,
    //    FREIGHT=7,
    //    INSURANCE=8,
    //    ROUNDOFF=9,
    //    DISCOUNT=10
    //}
    $scope.RecalculateAdditioncalCost = function () {
        var newInd = 0;

        var productAmt = 0;
        var schemeAmt = 0;
        var productVatAmt = 0;
        var productVatAbleAmt = 0;
        var productExciduteAmt = 0;
        var productExciduteAbleAmt = 0;
        var productExciduteAbleQty = 0;
        var totalQty1 = 0;
        angular.forEach($scope.beData.ItemDetailsColl, function (idet) {

            if (idet.RowType == 'P') {
                totalQty1 += idet.ActualQty;

                productAmt += idet.Amount;

                schemeAmt += idet.SchameAmt;

                productVatAmt += (idet.VatAmount ? idet.VatAmount : 0);
                productVatAbleAmt += (idet.VatAbleAmt ? idet.VatAbleAmt : 0);

                productExciduteAmt += (idet.ExDutyAmount ? idet.ExDutyAmount : 0);
                productExciduteAbleAmt += (idet.ExciseAbtAmt ? idet.ExciseAbtAmt : 0);
                productExciduteAbleQty += (idet.ExciseAbleQty ? idet.ExciseAbleQty : 0);
            }
            else if (idet.RowType == 'L') {

                var ledAllotionAmt = 0;
                for (var i = 0; i < newInd; i++) {
                    var det = $scope.beData.ItemDetailsColl[i];
                    if (det.RowType == 'L') {
                        ledAllotionAmt += det.Amount;
                    }
                }

                var totalAmt1 = productAmt + ledAllotionAmt;
                var amt1 = 0;

                if (!idet.costLedgerDetail && idet.LedgerId > 0) {
                    $scope.loadingstatus = 'running';
                    showPleaseWait();
                    $http({
                        method: 'GET',
                        url: base_url + "Global/GetLedgerDetail?LedgerId=" + idet.LedgerId + " & VoucherType=" + $scope.SelectedVoucher.VoucherType + "&ShowClosing=false",
                        dataType: "json"
                    }).then(function (resLD) {

                        $scope.loadingstatus = 'stop';
                        hidePleaseWait();
                        if (resLD.data.IsSuccess && resLD.data.Data) {
                            //idet.costLedgerDetail = resLD.data.Data

                            var llId = resLD.data.Data.LedgerId;
                            angular.forEach($scope.beData.ItemDetailsColl, function (idLed) {
                                if (idLed.RowType == 'L') {
                                    if (llId == idLed.LedgerId) {
                                        idLed.costLedgerDetail = resLD.data.Data;
                                    }
                                }
                            });

                            $scope.RecalculateAdditioncalCost();
                        }
                    }, function (reason) {
                        alert('Failed' + reason);
                    });
                }


                if (idet.costLedgerDetail) {
                    if (idet.costLedgerDetail.LedgerType == 9) // Auto Rounde off
                    {
                        amt1 = Number((Math.round(totalAmt1) - totalAmt1).toFixed(3));
                    }
                    else if (idet.costLedgerDetail.LedgerType == 6)  // Scheme
                    {
                        amt1 = schemeAmt;
                    }
                    else if (idet.costLedgerDetail.LedgerType == 3)  // Excise Duty
                    {
                        amt1 = productExciduteAmt;
                    }
                    else
                        amt1 = idet.Amount;
                } else
                    amt1 = idet.Amount;


                if (idet.Rate != 0) {

                    if (idet.costLedgerDetail) {
                        if (idet.costLedgerDetail.AditionCostOnBasisOf == 0) {
                            if (idet.costLedgerDetail.LedgerType == 3) {
                                if ($scope.SelectedVoucher.Product.ProductWiseExciseDuty == true) {
                                    idet.Rate = 0;
                                    amt1 = productExciduteAmt;
                                    idet.AccessableValue = productExciduteAbleAmt;
                                } else {
                                    amt1 = productExciduteAbleQty * idet.Rate;
                                    idet.AccessableValue = productExciduteAbleAmt;
                                }
                            }
                            else {
                                amt1 = totalQty1 * idet.Rate / 100;
                                idet.AccessableValue = totalQty1;
                            }

                        }
                        else {
                            if (idet.costLedgerDetail.LedgerType == 3) // Excise Duty
                            {
                                if ($scope.SelectedVoucher.Product.ProductWiseExciseDuty == true) {
                                    idet.Rate = 0;
                                    amt1 = productExciduteAmt;
                                    idet.AccessableValue = productExciduteAbleAmt;
                                } else {
                                    amt1 = productExciduteAbleAmt * idet.Rate / 100;
                                    idet.AccessableValue = productExciduteAbleAmt;
                                }
                            }
                            else if (idet.costLedgerDetail.LedgerType == 1) // Vat
                            {
                                if ($scope.SelectedVoucher.Product.ProductWiseVat == true) {
                                    amt1 = productVatAmt + (ledAllotionAmt * idet.Rate / 100);
                                    idet.AccessableValue = productVatAbleAmt + ledAllotionAmt;
                                }
                                else {
                                    amt1 = (productVatAbleAmt + ledAllotionAmt) * idet.Rate / 100;
                                    idet.AccessableValue = (productVatAbleAmt + ledAllotionAmt);
                                }

                            }
                            else {
                                if (idet.costLedgerDetail.AppropriateTo == 1) {
                                    amt1 = productAmt * idet.Rate / 100;
                                    idet.AccessableValue = productAmt;
                                }
                                else if (idet.costLedgerDetail.AppropriateTo == 2) {
                                    amt1 = ledAllotionAmt * idet.Rate / 100;
                                    idet.AccessableValue = ledAllotionAmt;
                                }
                                else {
                                    amt1 = totalAmt1 * idet.Rate / 100;
                                    idet.AccessableValue = totalAmt1;
                                }
                            }
                        }
                    }
                    else {
                        amt1 = totalAmt1 * idet.Rate / 100;
                        idet.AccessableValue = totalAmt1;
                    }

                }

                //idet.Amount = amt1;
                idet.Amount = ($filter('number')(amt1, $scope.SelectedVoucher.NoOfDecimalPlaces)).parseDBL();

                if (idet.costLedgerDetail) {
                    if ((idet.costLedgerDetail.LedgerType == 10 || idet.costLedgerDetail.LedgerType == 6) && amt1 > 0) {
                        idet.Rate = idet.Rate * -1;
                        idet.Amount = idet.Amount * -1;
                    } else if ((idet.costLedgerDetail.LedgerType == 1 || idet.costLedgerDetail.LedgerType == 3 || idet.costLedgerDetail.LedgerType == 7 || idet.costLedgerDetail.LedgerType == 8) && amt1 < 0) {
                        idet.Rate = idet.Rate * -1;
                        idet.Amount = idet.Amount * -1;
                    }
                }

            }
            newInd++;
        });

        var totalAmt = 0;
        angular.forEach($scope.beData.ItemDetailsColl, function (idet) {
            totalAmt += idet.Amount;
        });
        $scope.beData.TotalAmount = totalAmt;
    };


    $scope.SaveDispatchOrder = function () {

        if ($scope.loadingstatus == "running")
            return;

        if ($scope.IsValidData() == true) {

            $scope.RecalculateAdditioncalCost();
            $scope.CalculateTotalAndSubTotal();

            $timeout(function () {
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
                                url: base_url + "Inventory/Transaction/SaveUpdateDispatchOrder",
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

                                if (res.data.IsSuccess == true) {
                                    $scope.beData.SaveClear = true;
                                    $scope.lastTranId = res.data.Data.RId;
                                    $scope.lastVoucherId = $scope.SelectedVoucher.VoucherId;

                                    if ($scope.SelectedVoucher.PrintVoucherAfterSaving == true) {
                                        $scope.Print();
                                    }
                                    $scope.Clear();
                                }
                                else {
                                    Swal.fire(res.data.ResponseMSG);
                                }

                            }, function (errormessage) {
                                hidePleaseWait();
                                $scope.loadingstatus = "stop";

                            });
                        });
                    }
                });
            }, 100);

        }


    }

    $scope.GetTransactionById = function (tran) {
        if (tran.TranId && tran.TranId > 0) {

            $scope.loadingstatus = "running";
            showPleaseWait();

            var para = {
                tranId: tran.TranId
            };

            $scope.ClearData();

            $timeout(function () {
                $http({
                    method: 'POST',
                    url: base_url + "Inventory/Transaction/GetDispatchOrderById",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    $timeout(function () {
                        if (res.data.IsSuccess && res.data.Data) {
                            var tran = res.data.Data;
                            $scope.SetData(tran);
                            hidePleaseWait();
                            $scope.loadingstatus = "stop";
                            $('#searVoucherRightBtn').modal('hide');
                        } else
                            Swal.fire(res.data.ResponseMSG);
                    }, 300);
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }, 100);

        }
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


        if ($scope.HideShow.TranCostCenter == false) {
            if ($scope.beData.TranCostCenter) {

                if ($scope.beData.TranCostCenter == null || $scope.beData.TranCostCenter == 0) {
                    result = false;
                    Swal.fire('Please ! Select Valid Cost Center');
                }

            } else {
                result = false;
                Swal.fire('Please ! Select Valid Cost Center');
            }
        }

        if ($scope.beData.PartyLedgerId) {
            if ($scope.beData.PartyLedgerId == null || $scope.beData.PartyLedgerId == 0) {
                result = false;
                Swal.fire('Please ! Select Valid Party Name');
            }
        } else {
            result = false;
            Swal.fire('Please ! Select Valid Party Name');
        }


        //if ($scope.HideShow.Godown == false) {
        //    if ($scope.beData.GodownId) {

        //        if ($scope.beData.GodownId == null || $scope.beData.GodownId == 0) {
        //            result = false;
        //            Swal.fire('Please ! Select Valid Godown Name');
        //        }

        //    } else {
        //        result = false;
        //        Swal.fire('Please ! Select Valid Godown Name');
        //    }
        //}

        if ($scope.SelectedVoucher.ShowWarringForBackDate == true) {
            if (!$scope.beData.VoucherDateDet) {
                var today = new Date();
                var vDate = $scope.beData.VoucherDateDet.dateAD;

                if (vDate < today) {
                    Swal.fire('Please ! Invoice Date is less then today');
                }
            }
        }

        if ($scope.SelectedVoucher.AllowCashPurchaseSale == true && $scope.beData.DispatchOrderDetail.TermsOfPayment == 'CASH') {
            if (!$scope.beData.PaymentTermColl || $scope.beData.PaymentTermColl.length == 0) {
                Swal.fire('Please ! Enter Receipt Amount');
                return false;
            }

            var pc = mx($scope.beData.PaymentTermColl);

            if (pc.sum(p1 => p1.Amount) == 0) {
                Swal.fire('Please ! Enter Receipt Amount');
                return false;
            }
        }

        var rowAt = 1;
        var lRowAt = 1;
        for (var i = 0; i < $scope.beData.ItemDetailsColl.length; i++) {
            var itemDet = $scope.beData.ItemDetailsColl[i];
            if (itemDet && itemDet.RowType == 'P') {
                if (itemDet.ProductId > 0 && (itemDet.productDetail == null || itemDet.productDetail === undefined)) {
                    result = false;
                    $timeout(function () {
                        $scope.RefreshRowData(itemDet, i);
                    });

                    //Swal.fire("Product Detail was not load at row " + rowAt);

                    //break;
                }

                rowAt++;
            }
            else if (itemDet && itemDet.RowType == 'L') {
                if (itemDet.LedgerId > 0 && (itemDet.costLedgerDetail == null || itemDet.costLedgerDetail === undefined)) {

                    result = false;
                    $timeout(function () {
                        $scope.RefreshRowData(itemDet, i);
                    });

                    //Swal.fire("Ledger Detail was not load at row " + lRowAt);

                    //break;
                }
                lRowAt++;
            }
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

        var tmpSales = {
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
            PartyLedgerId: $scope.beData.PartyLedgerId,
            SalesLedgerId: ($scope.beData.SalesLedgerId ? $scope.beData.SalesLedgerId : 0),
            TotalAmount: $scope.beData.TotalAmount,
            AgentId: $scope.beData.AgentId ? $scope.beData.AgentId : 0,
            PartyCostCenter: $scope.beData.PartyCostCenter ? $scope.beData.PartyCostCenter : 0,
            TranCostCenter: $scope.beData.TranCostCenter ? $scope.beData.TranCostCenter : 0,
            EntryDate: eDate,
            BranchId: ($scope.beData.BranchId ? $scope.beData.BranchId : 0),
            IsAbbInvoice: false,
            ItemAllocationColl: [],
            // AditionalCostColl: $scope.beData.AditionalCostColl,
            AditionalCostColl: [],
            DispatchOrderDetail: $scope.beData.DispatchOrderDetail,
            GodownId: $scope.beData.GodownId,
            DocumentColl: $scope.beData.DocumentColl,
            PaymentTermColl: $scope.beData.PaymentTermColl,
            ReceiptTranId: $scope.beData.ReceiptTranId,
        };



        if (tmpSales.DispatchOrderDetail) {
            if (tmpSales.DispatchOrderDetail.FreightRate == undefined || tmpSales.DispatchOrderDetail.FreightRate == null || tmpSales.DispatchOrderDetail.FreightRate === undefined)
                tmpSales.DispatchOrderDetail.FreightRate = 0;

            if (tmpSales.DispatchOrderDetail.AdvancePayment == undefined || tmpSales.DispatchOrderDetail.AdvancePayment == null || tmpSales.DispatchOrderDetail.AdvancePayment === undefined)
                tmpSales.DispatchOrderDetail.AdvancePayment = 0;

            if (tmpSales.DispatchOrderDetail.CreditDays == undefined || tmpSales.DispatchOrderDetail.CreditDays == null || tmpSales.DispatchOrderDetail.CreditDays === undefined)
                tmpSales.DispatchOrderDetail.CreditDays = 0;
        }

        if (tmpSales.PaymentTermColl) {
            var vid = $scope.beData.DispatchOrderDetail.ReceiptVoucherId;
            if (!vid) {
                if ($scope.RecVoucherTypeColl && $scope.RecVoucherTypeColl.length > 0)
                    vid = $scope.RecVoucherTypeColl[0].VoucherId;
            }
            angular.forEach(tmpSales.PaymentTermColl, function (pt) {
                pt.VoucherId = vid;
            });
        };

        var voucherUDFFields = [];
        angular.forEach($scope.beData.UDFFeildsColl, function (udf) {
            if (udf.FieldType == 2) {
                var ud = {
                    SNo: (udf.FieldNo ? udf.FieldNo : udf.SNo),
                    Name: udf.Name,
                    Value: udf.UDFValueDet ? $filter('date')(udf.UDFValueDet.dateAD, 'yyyy-MM-dd') : ''
                };
                voucherUDFFields.push(ud);
            } else {
                var ud = {
                    SNo: (udf.FieldNo ? udf.FieldNo : udf.SNo),
                    Name: udf.Name,
                    Value: udf.UDFValue
                };
                voucherUDFFields.push(ud);
            }
        });
        if (voucherUDFFields.length > 0)
            tmpSales.Attributes = JSON.stringify(voucherUDFFields);

        angular.forEach($scope.beData.ItemDetailsColl, function (itemDet) {
            if (itemDet.ProductId && itemDet.ProductId > 0 && itemDet.RowType == 'P') {

                var udfValues = '';
                var udfFields = [];
                angular.forEach(itemDet.UDFFeildsColl, function (udf) {
                    if (udf.FieldType == 2) {
                        var ud = {
                            SNo: (udf.FieldNo ? udf.FieldNo : udf.SNo),
                            Name: udf.Name,
                            Value: udf.UDFValueDet ? $filter('date')(udf.UDFValueDet.dateAD, 'yyyy-MM-dd') : ''
                        };
                        udfFields.push(ud);
                    } else {
                        var ud = {
                            SNo: (udf.FieldNo ? udf.FieldNo : udf.SNo),
                            Name: udf.Name,
                            Value: udf.UDFValue
                        };
                        udfFields.push(ud);
                    }
                });
                if (udfFields.length > 0)
                    udfValues = JSON.stringify(udfFields);

                var itemAllocation = {
                    Attributes: udfValues,
                    ProductId: itemDet.ProductId,
                    ActualQty: itemDet.ActualQty + (itemDet.FreeQty ? itemDet.FreeQty : 0),
                    BilledQty: itemDet.BilledQty,
                    UnitId: (itemDet.TranUnitId > 0 ? itemDet.TranUnitId : itemDet.UnitId),
                    Rate: itemDet.Rate,
                    Amount: itemDet.Amount,
                    DiscountAmt: itemDet.DiscountAmt,
                    DiscountPer: itemDet.DiscountPer,
                    SchameAmt: itemDet.SchameAmt,
                    SchamePer: itemDet.SchamePer,
                    ALUnitId1: itemDet.ALUnitId1 ? itemDet.ALUnitId1 : 0,
                    ALUnitId2: itemDet.ALUnitId2 ? itemDet.ALUnitId2 : 0,
                    ALUnitId3: itemDet.ALUnitId3 ? itemDet.ALUnitId3 : 0,
                    ALValue1: itemDet.ALValue1 ? itemDet.ALValue1 : 0,
                    ALValue2: itemDet.ALValue2 ? itemDet.ALValue2 : 0,
                    ALValue3: itemDet.ALValue3 ? itemDet.ALValue3 : 0,
                    Narration: itemDet.Narration,
                    DispatchOrderItemAllocationId: itemDet.DispatchOrderItemAllocationId ? itemDet.DispatchOrderItemAllocationId : 0,
                    OrderItemAllocationId: itemDet.OrderItemAllocationId ? itemDet.OrderItemAllocationId : 0,
                    DispatchOrderItemAllocationId: itemDet.DispatchOrderItemAllocationId ? itemDet.DispatchOrderItemAllocationId : 0,
                    ReceivedNoteItemAllocationId: itemDet.ReceivedNoteItemAllocationId ? itemDet.ReceivedNoteItemAllocationId : 0,
                    QuotationItemAllocationId: itemDet.QuotationItemAllocationId ? itemDet.QuotationItemAllocationId : 0,
                    BundleId: 0,
                    BundleQty: 0,
                    Description: itemDet.Description ? itemDet.Description : '',
                    LedgerId: itemDet.ProductLedgerId ? itemDet.ProductLedgerId : 0,
                    ItemDetailsColl: [],
                    GodownId: (itemDet.GodownId > 0 ? itemDet.GodownId : (tmpSales.GodownId > 0 ? tmpSales.GodownId : 0)),
                    VatRate: itemDet.VatRate,
                    VatAmount: itemDet.VatAmount,
                    VatAbleAmt: itemDet.VatAbleAmt,
                    ExDutyRate: itemDet.ExDutyRate,
                    ExDutyAmount: itemDet.ExDutyAmount,
                    Description: itemDet.Description,
                    RegdNo: itemDet.RegdNo ? itemDet.RegdNo : '',
                    EngineNo: itemDet.EngineNo ? itemDet.EngineNo : '',
                    ChassisNo: itemDet.ChassisNo ? itemDet.ChassisNo : '',
                    Model: itemDet.Model ? itemDet.Model : '',
                    CodeNo: itemDet.CodeNo ? itemDet.CodeNo : '',
                    Color: itemDet.Color ? itemDet.Color : '',
                    KeyNo: itemDet.KeyNo ? itemDet.KeyNo : '',
                    MFGYear: itemDet.MFGYear ? itemDet.MFGYear : 0,
                    Type: itemDet.Type ? itemDet.Type : '',
                    TranUnitId: itemDet.TranUnitId,
                    TranUnitQty: itemDet.TranUnitQty,
                    RefQty: itemDet.RefQty > 0 ? itemDet.RefQty : 0,

                };

                if (itemDet.FixedProductColl && itemDet.FixedProductColl.length > 0 && $scope.SelectedVoucher.MultipleFixedProduct == true) {

                    var rate = itemAllocation.Rate;
                    angular.forEach(itemDet.FixedProductColl, function (fp) {
                        if (fp.IsSelected == true) {
                            itemAllocation.ItemDetailsColl.push(
                                {
                                    ProductId: itemAllocation.ProductId,
                                    ActualQty: 1,
                                    BilledQty: 1,
                                    UnitId: itemAllocation.UnitId,
                                    Rate: (fp.Amount > 0 ? fp.Amount : rate),
                                    Amount: (fp.Amount > 0 ? fp.Amount : rate),
                                    Batch: itemDet.Batch,
                                    EXPDate: (itemDet.EXPDate && itemDet.EXPDateDet ? $filter('date')(new Date(itemDet.EXPDateDet), 'yyyy-MM-dd') : null),
                                    MFGDate: (itemDet.MFGDate && itemDet.MFGDateDet ? $filter('date')(new Date(itemDet.MFGDateDet), 'yyyy-MM-dd') : null),
                                    GodownId: itemAllocation.GodownId,
                                    DiscountAmt: 0,
                                    DiscountPer: 0,
                                    SchameAmt: 0,
                                    SchamePer: 0,
                                    ALUnitId1: 0,
                                    ALUnitId2: 0,
                                    ALUnitId3: 0,
                                    ALValue1: 0,
                                    ALValue2: 0,
                                    ALValue3: 0,
                                    Narration: itemAllocation.Narration,
                                    VatRate: itemAllocation.VatRate,
                                    VatAmount: itemAllocation.VatAmount,
                                    VatAbleAmt: itemAllocation.VatAbleAmt,
                                    ExDutyRate: itemAllocation.ExDutyRate,
                                    ExDutyAmount: itemAllocation.ExDutyAmount,
                                    BundleId: 0,
                                    BundleQty: 0,
                                    RegdNo: fp.RegdNo,
                                    EngineNo: fp.EngineNo,
                                    ChassisNo: fp.ChassisNo,
                                    Model: fp.Model,
                                    CodeNo: fp.CodeNo,
                                    Color: fp.Color,
                                    KeyNo: fp.KeyNo,
                                    MFGYear: fp.MFGYear,
                                    Type: fp.Type,
                                    Description: itemAllocation.Description,
                                    AbbRate: itemAllocation.AbbRate,
                                    AbbAmount: itemAllocation.AbbAmount,
                                    IsTaxable: itemAllocation.IsTaxable,
                                    RateOf: itemAllocation.RateOf,
                                    NetWeight: itemAllocation.NetWeight,
                                    LossWeight: itemAllocation.LossWeight,
                                    LossRate: itemAllocation.LossRate,

                                    FineWeight: itemAllocation.FineWeight,
                                    FineRate: itemAllocation.FineRate,
                                    ProcessingWeight: itemAllocation.ProcessingWeight,
                                    ProcessingRate: itemAllocation.ProcessingRate,

                                    Makeing: itemAllocation.Makeing,
                                    Stone: itemAllocation.Stone,
                                    MRP: itemAllocation.MRP,
                                    TradeRate: itemAllocation.TradeRate,
                                    PurchaseRate: itemAllocation.PurchaseRate,
                                    SSFAmount: itemAllocation.SSFAmount,
                                    TranUnitId: itemAllocation.TranUnitId,
                                    TranUnitQty: itemAllocation.TranUnitQty,
                                    NotEffectQty: itemAllocation.NotEffectQty,
                                    RefQty: itemAllocation.RefQty,
                                });
                        }

                    });

                } else {
                    itemAllocation.ItemDetailsColl.push(
                        {
                            ProductId: itemAllocation.ProductId,
                            ActualQty: itemAllocation.ActualQty,
                            BilledQty: itemAllocation.BilledQty,
                            UnitId: itemAllocation.UnitId,
                            Rate: itemAllocation.Rate,
                            Amount: itemAllocation.Amount,
                            Batch: itemDet.Batch,
                            EXPDate: (itemDet.EXPDate && itemDet.EXPDateDet ? $filter('date')(new Date(itemDet.EXPDateDet), 'yyyy-MM-dd') : null),
                            MFGDate: (itemDet.MFGDate && itemDet.MFGDateDet ? $filter('date')(new Date(itemDet.MFGDateDet), 'yyyy-MM-dd') : null),
                            GodownId: itemAllocation.GodownId,
                            DiscountAmt: itemAllocation.DiscountAmt,
                            DiscountPer: itemAllocation.DiscountPer,
                            SchameAmt: itemAllocation.SchameAmt,
                            SchamePer: itemAllocation.SchamePer,
                            ALUnitId1: itemAllocation.ALUnitId1,
                            ALUnitId2: itemAllocation.ALUnitId2,
                            ALUnitId3: itemAllocation.ALUnitId3,
                            ALValue1: itemAllocation.ALValue1,
                            ALValue2: itemAllocation.ALValue2,
                            ALValue3: itemAllocation.ALValue3,
                            Narration: itemAllocation.Narration,
                            VatRate: itemAllocation.VatRate,
                            VatAmount: itemAllocation.VatAmount,
                            VatAbleAmt: itemAllocation.VatAbleAmt,
                            ExDutyRate: itemAllocation.ExDutyRate,
                            ExDutyAmount: itemAllocation.ExDutyAmount,
                            BundleId: 0,
                            BundleQty: 0,
                            RegdNo: itemAllocation.RegdNo,
                            EngineNo: itemAllocation.EngineNo,
                            ChassisNo: itemAllocation.ChassisNo,
                            Model: itemAllocation.Model,
                            CodeNo: itemAllocation.CodeNo,
                            Color: itemAllocation.Color,
                            KeyNo: itemAllocation.KeyNo,
                            MFGYear: itemAllocation.MFGYear,
                            Type: itemAllocation.Type,
                            Description: itemAllocation.Description,
                            AbbRate: itemAllocation.AbbRate,
                            AbbAmount: itemAllocation.AbbAmount,
                            IsTaxable: itemAllocation.IsTaxable,
                            RateOf: itemAllocation.RateOf,
                            NetWeight: itemAllocation.NetWeight,
                            LossWeight: itemAllocation.LossWeight,
                            LossRate: itemAllocation.LossRate,

                            FineWeight: itemAllocation.FineWeight,
                            FineRate: itemAllocation.FineRate,
                            ProcessingWeight: itemAllocation.ProcessingWeight,
                            ProcessingRate: itemAllocation.ProcessingRate,

                            Makeing: itemAllocation.Makeing,
                            Stone: itemAllocation.Stone,
                            MRP: itemAllocation.MRP,
                            TradeRate: itemAllocation.TradeRate,
                            PurchaseRate: itemAllocation.PurchaseRate,
                            SSFAmount: itemAllocation.SSFAmount,
                            TranUnitId: itemAllocation.TranUnitId,
                            TranUnitQty: itemAllocation.TranUnitQty,
                            NotEffectQty: itemAllocation.NotEffectQty,
                            RefQty: itemAllocation.RefQty,
                        });
                }

                tmpSales.ItemAllocationColl.push(itemAllocation);
            }
            else if (itemDet.LedgerId && itemDet.LedgerId > 0 && itemDet.RowType == 'L') {
                tmpSales.AditionalCostColl.push({
                    LedgerId: itemDet.LedgerId,
                    AccessableValue: (itemDet.AccessableValue ? itemDet.AccessableValue : 0),
                    Rate: (itemDet.Rate ? itemDet.Rate : 0),
                    Amount: (itemDet.Amount ? itemDet.Amount : 0),
                });
            }
        });


        return tmpSales;

    };

    $scope.SetData = function (tran) {

        $scope.lastPartyLedgerId = tran.PartyLedgerId;

        if ($scope.GodownColl.length > 0 && (!$scope.beData.GodownId || $scope.beData.GodownId == 0)) {
            $scope.beData.GodownId = $scope.GodownColl[0].GodownId;
        }
        tran.GodownId = $scope.beData.GodownId;

        var ptColl = mx(tran.PaymentTermColl);
        angular.forEach($scope.beData.PaymentTermColl, function (pt) {
            var fn = ptColl.firstOrDefault(p1 => p1.PaymentTermsId == pt.PaymentTermsId);
            if (fn) {
                pt.Amount = fn.Amount;
                pt.Remarks = fn.Remarks;
            }

        });
        $scope.beData.ReceiptTranId = tran.ReceiptTranId;
        $scope.beData.TranId = tran.TranId;
        $scope.beData.VoucherId = tran.VoucherId;
        $scope.beData.CostClassId = tran.CostClassId;
        $scope.beData.AutoVoucherNo = tran.AutoVoucherNo;
        $scope.beData.CurRate = tran.CurRate;
        $scope.beData.CurrencyId = tran.CurrencyId;
        $scope.beData.ManualVoucherNO = tran.ManualVoucherNO;
        $scope.beData.Narration = tran.Narration;
        $scope.beData.VoucherDate = new Date(tran.VoucherDate);
        $scope.beData.VoucherDate_TMP = new Date(tran.VoucherDate);
        $scope.beData.RefNo = tran.RefNo;
        $scope.beData.AutoManualNo = tran.AutoManualNo;
        $scope.beData.PartyLedgerId = tran.PartyLedgerId;
        $scope.beData.SalesLedgerId = (tran.SalesLedgerId ? tran.SalesLedgerId : 0);
        $scope.beData.TotalAmount = tran.TotalAmount;
        $scope.beData.AgentId = tran.AgentId ? tran.AgentId : 0;
        $scope.beData.PartyCostCenter = tran.PartyCostCenter ? tran.PartyCostCenter : 0;
        $scope.beData.TranCostCenter = tran.TranCostCenter ? tran.TranCostCenter : 0;
        $scope.beData.EntryDate = new Date(tran.EntryDate);
        $scope.beData.BranchId = (tran.BranchId ? tran.BranchId : 0);
        $scope.beData.IsAbbInvoice = tran.IsAbbInvoice;
        $scope.beData.ItemAllocationColl = tran.ItemAllocationColl;
        $scope.beData.AditionalCostColl = [];
        $scope.beData.DispatchOrderDetail = tran.DispatchOrderDetail;
        $scope.beData.DocumentColl = tran.DocumentColl;
        //  $scope.beData.GodownId = tran.GodownId
        $scope.beData.ItemDetailsColl = [];

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
                };
                udfColl.push(ud);
            });
        }

        angular.forEach(tran.ItemAllocationColl, function (itemA) {
            angular.forEach(itemA.ItemDetailsColl, function (itemAD) {

                itemAD.LedgerId = itemA.LedgerId;
                itemAD.ProductLedgerId = itemA.LedgerId;
                itemAD.RowType = 'P';
                itemAD.RefQty = itemAD.ActualQty;
                itemAD.Description = itemA.Description;

                itemAD.UDFFeildsColl = udfColl;
                if (itemA.Attributes && itemA.Attributes.length > 0) {

                    var udfFieldsColl = mx(JSON.parse(itemA.Attributes));
                    angular.forEach(itemAD.UDFFeildsColl, function (udd) {
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

                if ($scope.SelectedVoucher.Product && $scope.SelectedVoucher.Product.VoucherWiseDecimalPlaces == true) {
                    itemAD.QtyDecimal = $scope.SelectedVoucher.Product.QtyNoOfDecimalPlaces;
                    itemAD.RateDecimal = $scope.SelectedVoucher.Product.RateNoOfDecimalPlaces;
                    itemAD.AmountDecimal = $scope.SelectedVoucher.Product.AmountNoOfDecimalPlaces;
                } else {
                    var findUnit = $scope.UnitColl.firstOrDefault(p1 => p1.UnitId == itemAD.UnitId);
                    if (findUnit) {
                        itemAD.QtyDecimal = findUnit.NoOfDecimalPlaces;
                        itemAD.RateDecimal = findUnit.RateNoOfDecimalPlaces;
                        itemAD.AmountDecimal = findUnit.AmountNoOfDecimalPlaces;
                    }
                }

                itemAD.TranUnitId = itemAD.UnitId;
                itemAD.TranUnitQty = itemAD.ActualQty;

                itemAD.Narration = itemA.Narration;
                itemAD.SalesProjectionItemAllocationId = itemA.SalesProjectionItemAllocationId;
                itemAD.IndentItemAllocationId = itemA.IndentItemAllocationId;
                itemAD.QuotationItemAllocationId = itemA.QuotationItemAllocationId;
                itemAD.OrderItemAllocationId = itemA.OrderItemAllocationId;
                itemAD.ReceivedNoteItemAllocationId = itemA.ReceivedNoteItemAllocationId;
                itemAD.DispatchOrderItemAllocationId = itemA.DispatchOrderItemAllocationId;
                itemAD.InvoiceItemAllocationId = itemA.InvoiceItemAllocationId;
                itemAD.ReturnItemAllocationId = itemA.ReturnItemAllocationId;
                itemAD.DispatchOrderItemAllocationId = itemA.DispatchOrderItemAllocationId;
                itemAD.DispatchOrderItemAllocationId = itemA.DispatchOrderItemAllocationId;
                $scope.beData.ItemDetailsColl.push(itemAD);
            });
        });

        $scope.AddRowInItemDetails($scope.beData.ItemDetailsColl.length);

        angular.forEach(tran.AditionalCostColl, function (adc) {
            adc.RowType = 'L';
            adc.ActualQty = 0;
            adc.BilledQty = 0;
            adc.FreeQty = 0;
            adc.Rate = adc.Rate;
            adc.DiscountPer = 0;
            adc.DiscountAmt = 0;
            adc.SchameAmt = 0;
            adc.SchamePer = 0;
            adc.QtyPoint = 0;
            adc.UnitId = null;
            adc.CanEditRate = true;
            adc.ALValue1 = 0;
            adc.ALValue2 = 0;
            adc.ALUnitId1 = null;
            adc.ALUnitId2 = null;
            adc.SchemeAmt = 0;
            adc.SchemeAmt = 0;
            adc.QtyDecimal = 2;
            adc.RateDecimal = 2;
            adc.AmountDecimal = 2;

            //if (adc.costLedgerDetail == null || adc.costLedgerDetail == undefined) {
            //    $scope.loadingstatus = 'running';
            //    showPleaseWait();
            //    $http({
            //        method: 'GET',
            //        url: base_url + "Global/GetLedgerDetail?LedgerId=" + adc.LedgerId + " & VoucherType=" + $scope.SelectedVoucher.VoucherType,
            //        dataType: "json"
            //    }).then(function (resLD) {
            //        $scope.loadingstatus = 'stop';
            //        hidePleaseWait();
            //        if (resLD.data.IsSuccess && resLD.data.Data) {
            //            adc.costLedgerDetail = resLD.data.Data
            //            $scope.beData.ItemDetailsColl.push(adc);
            //            $scope.RecalculateAdditioncalCost();
            //            $scope.CalculateTotalAndSubTotal();
            //        }
            //    }, function (reason) {
            //        alert('Failed' + reason);
            //    });
            //} 
            $scope.beData.ItemDetailsColl.push(adc);
        });

        $scope.RecalculateAdditioncalCost();
        $scope.CalculateTotalAndSubTotal();

    };

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
        $scope.ClearItemDetails();
        $scope.beData.SalesInvoiceDetail = {};
        $scope.beData.PartyLedgerId = null;
        $scope.beData.AditionalCostColl = [];

        if ($scope.beData.VoucherId > 0)
            $scope.SelectedVoucher = mx($scope.VoucherTypeColl).firstOrDefault(p1 => p1.VoucherId == $scope.beData.VoucherId);

        if ($scope.beData.CostClassId > 0)
            $scope.SelectedCostClass = mx($scope.CostClassColl).firstOrDefault(p1 => p1.CostClassId == $scope.beData.CostClassId);

        if ($scope.SelectedVoucher) {
            $http({
                method: 'GET',
                url: base_url + "Account/Creation/GetVoucherModeById?voucherId=" + $scope.SelectedVoucher.VoucherId,
                dataType: "json"
            }).then(function (res) {
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
                                        };
                                        $scope.beData.UDFFeildsColl.push(ud);
                                    });
                                }


                                if ($scope.SelectedVoucher.NumberingMethod == 1)
                                    $scope.HideShow.AutoVoucherNo = false;
                                else
                                    $scope.HideShow.AutoVoucherNo = true;

                                if ($scope.SelectedVoucher.UsePartyCostCenter == true)
                                    $scope.HideShow.PartyCostCenter = false;
                                else
                                    $scope.HideShow.PartyCostCenter = true;

                                if ($scope.SelectedVoucher.UseTranCostCenter == true)
                                    $scope.HideShow.TranCostCenter = false;
                                else
                                    $scope.HideShow.TranCostCenter = true;

                                if ($scope.SelectedVoucher.UseRefNo == true)
                                    $scope.HideShow.RefNo = false;
                                else
                                    $scope.HideShow.RefNo = true;


                                if ($scope.SelectedVoucher.CanChangeLedgerAndAgent == true)
                                    $scope.HideShow.Agent = false;
                                else
                                    $scope.HideShow.Agent = true;

                                if ($scope.SelectedVoucher.AllowMultipleCurrency == true)
                                    $scope.HideShow.Currency = false;
                                else
                                    $scope.HideShow.Currency = true;




                                if ($scope.SelectedVoucher.Product.ActiveActualAndBillQty == true)
                                    $scope.HideShow.BilledQty = false;
                                else
                                    $scope.HideShow.BilledQty = true;

                                if ($scope.SelectedVoucher.Product.AllowDiscount == true) {

                                    $scope.HideShow.Discount = false;
                                    if ($scope.SelectedVoucher.Product.ShowDiscountAmt)
                                        $scope.HideShow.DiscountAmt = false;
                                    else
                                        $scope.HideShow.DiscountAmt = true;

                                    if ($scope.SelectedVoucher.Product.ShowDiscountAmt)
                                        $scope.HideShow.DiscountPer = false;
                                    else
                                        $scope.HideShow.DiscountPer = true;
                                }
                                else {
                                    $scope.HideShow.Discount = true;
                                    $scope.HideShow.DiscountPer = true;
                                    $scope.HideShow.DiscountAmt = true;
                                }


                                if ($scope.SelectedVoucher.Product.ShowCurrentStock == true)
                                    $scope.HideShow.CurrentBalance = false;
                                else
                                    $scope.HideShow.CurrentBalance = true;

                                if ($scope.SelectedVoucher.Product.AllowFreeQty == true)
                                    $scope.HideShow.FreeQty = false;
                                else
                                    $scope.HideShow.FreeQty = true;

                                if ($scope.SelectedVoucher.Product.AllowScheme == true) {
                                    $scope.HideShow.Scheme = false;

                                    if ($scope.SelectedVoucher.Product.ShowSchemeAmt == true)
                                        $scope.HideShow.SchemeAmt = false;
                                    else
                                        $scope.HideShow.SchemeAmt = true;

                                    if ($scope.SelectedVoucher.Product.ShowSchemePer == true)
                                        $scope.HideShow.SchemePer = false;
                                    else
                                        $scope.HideShow.SchemePer = true;

                                } else {
                                    $scope.HideShow.Scheme = true;
                                    $scope.HideShow.SchemeAmt = true;
                                    $scope.HideShow.SchemePer = true;
                                }

                                if ($scope.SelectedVoucher.Product.ShowProductDescription == true)
                                    $scope.HideShow.ProductDescription = false;
                                else
                                    $scope.HideShow.ProductDescription = true;

                                if ($scope.SelectedVoucher.Product.ShowProductQuantityPoint == true)
                                    $scope.HideShow.ProductPoint = false;
                                else
                                    $scope.HideShow.ProductPoint = true;

                                if ($scope.SelectedVoucher.Product.ProductWiseLedger == true) {
                                    $scope.HideShow.SalesLedger = true;

                                    if ($scope.SelectedVoucher.Product.ShowProductWiseLedger == true)
                                        $scope.HideShow.ProductLedger = false;
                                    else
                                        $scope.HideShow.ProductLedger = true;
                                }
                                else {
                                    $scope.HideShow.SalesLedger = true;
                                    $scope.HideShow.ProductLedger = true;
                                }


                                if ($scope.SelectedVoucher.Product.ShowAlternateUnit == true) {
                                    $scope.HideShow.AlternetUnit = false;

                                    if ($scope.SelectedVoucher.Product.ActiveAlternateUnitColumn1 == true)
                                        $scope.HideShow.AlternetUnit1 = false;
                                    else
                                        $scope.HideShow.AlternetUnit1 = true;

                                    if ($scope.SelectedVoucher.Product.ActiveAlternateUnitColumn2 == true)
                                        $scope.HideShow.AlternetUnit2 = false;
                                    else
                                        $scope.HideShow.AlternetUnit2 = true;

                                    if ($scope.SelectedVoucher.Product.ActiveAlternateUnitMultiple == true) {
                                        $scope.HideShow.AlternetUnitMultiple = false;
                                        $scope.HideShow.AlternetUnit1 = true;
                                        $scope.HideShow.AlternetUnit2 = true;
                                    }
                                    else
                                        $scope.HideShow.AlternetUnitMultiple = true;

                                }
                                else {
                                    $scope.HideShow.AlternetUnit = true;
                                    $scope.HideShow.AlternetUnit1 = true;
                                    $scope.HideShow.AlternetUnit2 = true;
                                }


                                if ($scope.SelectedVoucher.UseEffectiveDate == true)
                                    $scope.HideShow.EntryDate = false;
                                else
                                    $scope.HideShow.EntryDate = true;

                                if ($scope.SelectedVoucher.Product.BatchNo == true)
                                    $scope.HideShow.Batch = false;
                                else
                                    $scope.HideShow.Batch = true;

                                if ($scope.SelectedVoucher.Product.UseEXP == true)
                                    $scope.HideShow.EXPDate = false;
                                else
                                    $scope.HideShow.EXPDate = true;

                                if ($scope.SelectedVoucher.Product.UseMFG == true)
                                    $scope.HideShow.MFGDate = false;
                                else
                                    $scope.HideShow.MFGDate = true;


                                if ($scope.SelectedVoucher.EachNarrationEntry == true)
                                    $scope.HideShow.EachNarration = false;
                                else
                                    $scope.HideShow.EachNarration = true;

                                if ($scope.SelectedVoucher.Product.ProductWiseExciseDuty == true)
                                    $scope.HideShow.ExciseDuty = false;
                                else
                                    $scope.HideShow.ExciseDuty = true;

                                if ($scope.SelectedVoucher.Product.ProductWiseVat == true)
                                    $scope.HideShow.Vat = false;
                                else
                                    $scope.HideShow.Vat = true;

                                if (!$scope.SelectedVoucher.VoucherDateLabel || $scope.SelectedVoucher.VoucherDateLabel.length == 0)
                                    $scope.SelectedVoucher.VoucherDateLabel = "Invoice Date";

                                if (!$scope.SelectedVoucher.EntryDateLabel || $scope.SelectedVoucher.EntryDateLabel.length == 0)
                                    $scope.SelectedVoucher.EntryDateLabel = "Entry Date";

                                if (!$scope.SelectedVoucher.RefNoName || $scope.SelectedVoucher.RefNoName.length == 0)
                                    $scope.SelectedVoucher.RefNoName = 'Ref. No.';

                                if ($scope.SelectedVoucher.Product.ShowRate == true)
                                    $scope.HideShow.Rate = false;
                                else
                                    $scope.HideShow.Rate = true;

                                if ($scope.SelectedVoucher.Product.ShowAmount == true)
                                    $scope.HideShow.Amount = false;
                                else
                                    $scope.HideShow.Amount = true;



                                $scope.beData.ItemDetailsColl = $scope.beData.ItemDetailsColl.filter(function (el) { return el.AutoCharge != true; });

                                if ($scope.SelectedVoucher.VoucherProductUDFColl && $scope.SelectedVoucher.VoucherProductUDFColl.length > 0) {
                                    angular.forEach($scope.beData.ItemDetailsColl, function (det) {
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
                                            };

                                            det.UDFFeildsColl.push(ud);
                                        });
                                    });
                                }

                                if ($scope.SelectedVoucher.AditionalChargeColl) {
                                    var itemInd = $scope.beData.ItemDetailsColl.length;
                                    for (var lInd = 0; lInd < $scope.SelectedVoucher.AditionalChargeColl.length; lInd++) {
                                        var ac = $scope.SelectedVoucher.AditionalChargeColl[lInd];
                                        $scope.AddRowInLedgerDetails(itemInd);

                                        var mul = 1;
                                        if (ac.Sign != undefined)
                                            mul = (ac.Sign == true ? 1 : -1);

                                        var ledAllocation = $scope.beData.ItemDetailsColl[itemInd];
                                        ledAllocation.CanEditRate = ac.CanEdit;
                                        ledAllocation.LedgerId = ac.LedgerId;
                                        ledAllocation.Rate = ac.Rate * mul;
                                        ledAllocation.Amount = ac.Amount * mul;
                                        ledAllocation.AutoCharge = true;
                                        itemInd++;
                                    }

                                }

                                if ($scope.SelectedVoucher.GodownColl && $scope.SelectedVoucher.GodownColl.length > 0) {
                                    var tmpGodownColl = [];
                                    var godown_Qry = mx($scope.SelectedVoucher.GodownColl);
                                    angular.forEach($scope.GodownColl, function (gd) {
                                        if (godown_Qry.contains(gd.GodownId)) {
                                            tmpGodownColl.push(gd);
                                        }
                                    });

                                    if (tmpGodownColl.length > 0) {
                                        $scope.SelectedVoucher.VoucherWiseGodownColl = tmpGodownColl;
                                    } else {
                                        $scope.SelectedVoucher.VoucherWiseGodownColl = $scope.GodownColl;
                                    }

                                    if (tmpGodownColl.length == 1) {
                                        $scope.beData.GodownId = tmpGodownColl[0].GodownId;
                                        $scope.HideShow.Godown = true;
                                    }
                                    else if (tmpGodownColl.length > 1) {
                                        $scope.HideShow.Godown = false;
                                        $scope.beData.GodownId = tmpGodownColl[0].GodownId;
                                    }
                                    else {
                                        $scope.HideShow.Godown = false;
                                        $scope.beData.GodownId = null;
                                    }

                                    if ($scope.beData.GodownId && $scope.beData.GodownId > 0) {
                                        if (angular.forEach($scope.beData.ItemDetailsColl, function (idet) {
                                            if (idet.RowType == 'P') {
                                                if (!idet.GodownId || idet.GodownId == 0)
                                                    idet.GodownId = $scope.beData.GodownId;
                                            }
                                        }));
                                    }
                                } else
                                    $scope.SelectedVoucher.VoucherWiseGodownColl = $scope.GodownColl;

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
    $("#txtBarcode").keyup(function (event) {
        if (event.keyCode === 13) {
            // $scope.barcodeScanned($scope.beData.ProductBarCode);
        }
    });
    $("#txtBarcode").keydown(function (event) {
        if (event.keyCode === 13 && event.ctrlKey == true) {
            $scope.barcodeScanned($scope.beData.ProductBarCode);
        }
    });
    $scope.barcodeScanned = function (barcode) {

        if (!barcode || barcode.length == 0)
            return;

        $scope.loadingstatus = 'running';
        showPleaseWait();
        var vid = $scope.SelectedVoucher.VoucherId;
        var queryParameters =
        {
            Top: 1,
            ColName: "P.Code",
            Operator: "like",
            OrderByCol: "P.Code",
            ColValue: barcode,
            VoucherId: vid
        };
        $http({
            method: 'GET',
            url: base_url + "Global/GetAllProduct?" + param(queryParameters),
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data && res.data.Data.length > 0) {

                $scope.beData.ProductBarCode = '';

                var findItem = res.data.Data[0];
                var alreadyExists = false;
                var indP = -1;
                var totalPLine = 0;
                angular.forEach($scope.beData.ItemDetailsColl, function (idet) {
                    indP++;

                    if (idet.RowType == 'P' && idet.ProductId > 0) {
                        if (idet.ProductId == findItem.ProductId) {
                            idet.ActualQty = idet.ActualQty + 1;
                            idet.BilledQty = idet.BilledQty + 1;
                            alreadyExists = true;
                            $scope.ChangeItemRowValue(idet, 'aQty');
                        }
                    } else if (idet.RowType == 'P') {
                        $scope.beData.ItemDetailsColl.splice(indP, 1);
                    }
                });

                angular.forEach($scope.beData.ItemDetailsColl, function (idet) {
                    if (idet.RowType == 'P')
                        totalPLine++;
                });

                if (alreadyExists == false) {

                    $timeout(function () {
                        var refItem = {
                            RowType: 'P',
                            ProductId: findItem.ProductId,
                            productDetail: null,
                            ActualQty: 1,
                            BilledQty: 1,
                            FreeQty: 0,
                            Rate: 0,
                            DiscountPer: 0,
                            DiscountAmt: 0,
                            SchameAmt: 0,
                            SchamePer: 0,
                            Amount: 0,
                            Description: '',
                            QtyPoint: 0,
                            UnitId: findItem.UnitId,
                            CanEditRate: false,
                            ALValue1: 0,
                            ALValue2: 0,
                            ALUnitId1: null,
                            ALUnitId2: null,
                            SchemeAmt: 0,
                            SchemeRate: 0,
                            ExciseAbleQty: 0,
                            ExciseAbtAmt: 0,
                            VatAbleAmt: 0,
                            VatRate: 0,
                            VatAmount: 0,
                            ExDutyRate: 0,
                            ExDutyAmount: 0,
                            QtyDecimal: 2,
                            RateDecimal: 2,
                            AmountDecimal: 2,
                            GodownId: $scope.beData.GodownId,
                            Narration: '',
                            RefQty: 0,
                            InvoiceItemAllocationId: null
                        };

                        $scope.beData.ItemDetailsColl.insert(totalPLine, refItem);
                    });
                }
            } else
                Swal.fire('Product Not Found');

        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

    };
    $scope.ClearData = function () {
        $scope.loadingstatus = "stop";
        $scope.IsRefPartyDet = false;
        $scope.lastPartyLedgerId = null;
        var sV = $scope.SelectedVoucher;
        var sC = $scope.SelectedCostClass;

        $scope.CheckedAll = false;
        $scope.RefAditionalCostColl = [];

        $scope.SelectedVoucher = null;
        $scope.SelectedCostClass = null;
        $scope.RefItemAllocationColl = [];

        $scope.beData =
        {
            VoucherId: null,
            CostClassId: null,
            TranId: 0,
            AutoManualNo: '',
            AutoVoucherNo: 0,
            PartyLedgerId: null,
            PartyLedger: null,
            partySideBarData: null,
            SalesLedgerId: null,
            salesledgerDetail: null,
            TranCostCenter: null,
            PartyCostCenter: null,
            salescostcenterDetail: null,
            SalesMan: null,
            salesmanSideBarData: null,
            CurRate: 1,
            ItemDetailsColl: [],
            AditionalCostColl: [],
            AttechFiles: [],
            SubTotal: 0,
            Total: 0,
            Narration: '',
            VoucherDate: new Date(),
            VoucherDate_TMP: new Date(),
            EntryDate_TMP: new Date(),
            DispatchOrderDetail: {},
            RefVoucherType: 1,
            PaymentTermColl: [],
        };

        angular.forEach($scope.PaymentTermColl, function (pt) {
            if (pt.IsCash == true && pt.LedgerId > 0) {
                $scope.beData.PaymentTermColl.push({
                    Name: pt.Name,
                    PaymentTermsId: pt.PaymentTermsId,
                    LedgerId: pt.LedgerId,
                    Amount: 0,
                    Remarks: ''
                });
            }
        });

        $scope.beData.ItemDetailsColl.push(
            {
                RowType: 'P',
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
                ALUnitId2: null,
                SchemeAmt: 0,
                SchemeAmt: 0,
                QtyDecimal: 2,
                RateDecimal: 2,
                AmountDecimal: 2
            });

        if ($scope.GodownColl.length == 1) {
            $scope.beData.GodownId = $scope.GodownColl[0].GodownId;
            $scope.HideShow.Godown = true;
        } else {
            $scope.HideShow.Godown = false;
            $scope.beData.GodownId = null;
        }


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

        $scope.getVoucherNo();

        $timeout(function () {
            GlobalServices.getCurrentDateTime().then(function (res) {
                var curDate = res.data.Data;
                if (curDate) {
                    $scope.beData.VoucherDate_TMP = new Date(curDate);
                }
            }, function (errormessage) {
                alert('Unable to Delete data. pls try again.' + errormessage.responseText);
            });
        });

    }

    $scope.ClearItemDetails = function () {

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
                };
                udfColl.push(ud);
            });
        }
        $scope.beData.ItemDetailsColl = [];
        $scope.beData.ItemDetailsColl.push(
            {
                RowType: 'P',
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
                ALUnitId2: null,
                SchemeAmt: 0,
                SchemeAmt: 0,
                QtyDecimal: 2,
                RateDecimal: 2,
                AmountDecimal: 2,
                UDFFeildsColl: udfColl,
            });

        if ($scope.SelectedVoucher.AditionalChargeColl && $scope.SelectedVoucher.IsAbbInvoice == false) {
            var itemInd = $scope.beData.ItemDetailsColl.length;
            for (var lInd = 0; lInd < $scope.SelectedVoucher.AditionalChargeColl.length; lInd++) {
                var ac = $scope.SelectedVoucher.AditionalChargeColl[lInd];
                $scope.AddRowInLedgerDetails(itemInd);

                var mul = 1;
                if (ac.Sign != undefined)
                    mul = (ac.Sign == true ? 1 : -1);

                var ledAllocation = $scope.beData.ItemDetailsColl[itemInd];
                ledAllocation.CanEditRate = ac.CanEdit;
                ledAllocation.LedgerId = ac.LedgerId;
                ledAllocation.Rate = ac.Rate * mul;
                ledAllocation.Amount = ac.Amount * mul;
                ledAllocation.AutoCharge = true;
                $scope.loadingstatus = 'running';
                showPleaseWait();
                $http({
                    method: 'GET',
                    url: base_url + "Global/GetLedgerDetail?LedgerId=" + ac.LedgerId + " & VoucherType=" + $scope.SelectedVoucher.VoucherType + "&ShowClosing=false",
                    dataType: "json"
                }).then(function (resLD) {


                    $scope.loadingstatus = 'stop';
                    hidePleaseWait();

                    if (resLD.data.IsSuccess && resLD.data.Data) {
                        //ledAllocation.costLedgerDetail = resLD.data.Data
                        var llId = resLD.data.Data.LedgerId;
                        angular.forEach($scope.beData.ItemDetailsColl, function (idLed) {
                            if (idLed.RowType == 'L') {
                                if (llId == idLed.LedgerId) {
                                    idLed.costLedgerDetail = resLD.data.Data;
                                }
                            }
                        });

                    }
                }, function (reason) {
                    alert('Failed' + reason);
                });

                itemInd++;
            }

        }
    }

    $scope.Clear = function () {
        if (!$scope.beData.SaveClear || $scope.beData.SaveClear == false) {

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

        } else {
            $scope.ClearData();
        }

        //if (isValidForClear == true) {

        //}
    };
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
                                                document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=" + vId + "&tranid=" + TranId + "&vouchertype=" + VoucherType;
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
                            document.getElementById("frmRpt").src = base_url + "web/ReportViewer.aspx?rpttranid=" + rptTranId + "&istransaction=true&entityid=" + EntityId + "&voucherid=" + vId + "&tranid=" + TranId + "&vouchertype=" + VoucherType;
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
    $scope.PrintVoucher = function (tranId, vid) {
        $scope.lastTranId = tranId;
        $scope.lastVoucherId = vid;
        $scope.Print();
    }
    $scope.RemoveAttachment = function (fId, ind) {

        if (fId == 1) {
            $scope.beData.DocumentColl.splice(ind, 1);
        }
        else if (fId == 2) {
            $scope.beData.AttechFiles.splice(ind, 1);
        }

    }

    $scope.LoadProperties = function () {

        $scope.PropertiesColl = [];
        $scope.ColumnColl = [];

        var para = {
            path: $scope.ItemFilePath,
            table: $scope.SelectedSheet
        };

        $http.get(base_url + "Setup/Security/LoadAllColumnsFromSheetIA?path=" + para.path + "&table=" + para.table).then(
            function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";

                if (res.data.IsSuccess) {
                    angular.forEach(res.data.PropertiesColl, function (pr) {

                        if (pr != "ResponseMSG" && pr != "IsSuccess" && pr != "CUserId") {

                            var properDet =
                            {
                                PropertyName: pr.datatype,
                                Name: '',
                                DefaultValue: '',
                                Id: -1
                            };

                            $scope.PropertiesColl.push(properDet);
                        }
                    })

                    $scope.ColumnColl = res.data.ColumnColl;

                } else {
                    alert(res.data.ResponseMSG);
                }

                $scope.loadingstatus = 'stop';
            }
            , function (reason) {
                $scope.loadingstatus = 'stop';
                alert('Failed: ' + reason);
            }
        );

    };

    $scope.ImportItemDataExcel = function () {

        if (!$scope.ItemFilePath || !$scope.SelectedSheet)
            return;

        if (!$scope.PropertiesColl || $scope.PropertiesColl.length == 0)
            return;

        var para = {
            path: $scope.ItemFilePath,
            table: $scope.SelectedSheet
        };

        $scope.loadingstatus = "running";
        showPleaseWait();

        $http({
            method: "post",
            url: base_url + "Setup/Security/FinalImportItemData?path=" + para.path + "&table=" + para.table,
            data: JSON.stringify($scope.PropertiesColl),
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";

            if (res.data.ResponseId && res.data.ResponseId.length > 0) {
                Swal.fire("Product Missing " + res.data.ResponseId);
            }


            if (res.data.IsSuccess) {
                $('#AddNewExcel').modal('hide');

                $scope.beData.ItemDetailsColl = [];
                angular.forEach(res.data.DataColl, function (fd) {

                    var refItem = {
                        IsImport: true,
                        RowType: 'P',
                        ProductId: fd.ProductId,
                        productDetail: null,
                        ActualQty: fd.ActualQty,
                        BilledQty: fd.BilledQty,
                        FreeQty: (fd.ActualQty - fd.BilledQty),
                        Rate: fd.Rate,
                        DiscountPer: fd.DiscountPer,
                        DiscountAmt: 0,
                        SchameAmt: 0,
                        SchamePer: 0,
                        Amount: fd.Amount,
                        Description: fd.Description,
                        QtyPoint: 0,
                        UnitId: fd.UnitId,
                        CanEditRate: false,
                        ALValue1: fd.AUQty1,
                        ALValue2: fd.AUQty2,
                        ALUnitId1: null,
                        ALUnitId2: null,
                        SchemeAmt: 0,
                        SchemeRate: 0,
                        ExciseAbleQty: 0,
                        ExciseAbtAmt: 0,
                        VatAbleAmt: 0,
                        VatRate: 0,
                        VatAmount: 0,
                        ExDutyRate: 0,
                        ExDutyAmount: 0,
                        QtyDecimal: 2,
                        RateDecimal: 2,
                        AmountDecimal: 2,
                        GodownId: (fd.GodownId && fd.GodownId > 0 ? fd.GodownId : $scope.beData.GodownId),
                        Narration: fd.Narration,
                        RefQty: fd.ActualQty,
                        ReceivedNoteItemAllocationId: null,
                        OrderItemAllocationId: null,
                        RegdNo: fd.RegdNo ? fd.RegdNo : '',
                        EngineNo: fd.EngineNo ? fd.EngineNo : '',
                        ChassisNo: fd.ChassisNo ? fd.ChassisNo : '',
                        Model: fd.Model ? fd.Model : '',
                        CodeNo: fd.CodeNo ? fd.CodeNo : '',
                        Color: fd.Color ? fd.Color : '',
                        KeyNo: fd.KeyNo ? fd.KeyNo : '',
                        MFGYear: fd.MFGYear ? fd.MFGYear : 0,
                        Type: fd.Type ? fd.Type : '',
                        ProductLedgerId: fd.ProductLedgerId,
                        LedgerId: fd.ProductLedgerId,
                    };
                    $scope.beData.ItemDetailsColl.push(refItem);
                });

                if ($scope.SelectedVoucher.AditionalChargeColl) {
                    var itemInd = $scope.beData.ItemDetailsColl.length;
                    for (var lInd = 0; lInd < $scope.SelectedVoucher.AditionalChargeColl.length; lInd++) {
                        var ac = $scope.SelectedVoucher.AditionalChargeColl[lInd];
                        $scope.AddRowInLedgerDetails(itemInd);

                        var mul = 1;
                        if (ac.Sign != undefined)
                            mul = (ac.Sign == true ? 1 : -1);

                        var ledAllocation = $scope.beData.ItemDetailsColl[itemInd];
                        ledAllocation.CanEditRate = ac.CanEdit;
                        ledAllocation.LedgerId = ac.LedgerId;
                        ledAllocation.Rate = ac.Rate * mul;
                        ledAllocation.Amount = ac.Amount * mul;
                        ledAllocation.AutoCharge = true;
                        $scope.loadingstatus = 'running';
                        showPleaseWait();
                        $http({
                            method: 'GET',
                            url: base_url + "Global/GetLedgerDetail?LedgerId=" + ac.LedgerId + " & VoucherType=" + $scope.SelectedVoucher.VoucherType + "&ShowClosing=false",
                            dataType: "json"
                        }).then(function (resLD) {


                            $scope.loadingstatus = 'stop';
                            hidePleaseWait();

                            if (resLD.data.IsSuccess && resLD.data.Data) {
                                //ledAllocation.costLedgerDetail = resLD.data.Data

                                var llId = resLD.data.Data.LedgerId;
                                angular.forEach($scope.beData.ItemDetailsColl, function (idLed) {
                                    if (idLed.RowType == 'L') {
                                        if (llId == idLed.LedgerId) {
                                            idLed.costLedgerDetail = resLD.data.Data;
                                        }
                                    }
                                });

                            }
                        }, function (reason) {
                            alert('Failed' + reason);
                        });

                        itemInd++;
                    }
                }

            } else {
                Swal.fire(res.data.ResponseMSG);
            }


        }, function (errormessage) {
            $scope.loadingstatus = 'stop';
            alert('Unable to Store data. pls try again.' + errormessage.responseText);
        });

    };

    $scope.getTableHeight = function () {
        var h = 260;
        if ($scope.HideShow.CostClass == false || $scope.HideShow.VoucherType == false)
            h += 55;

        if ($scope.HideShow.TranCostCenter == false || $scope.HideShow.PartyCostCenter == false)
            h += 55;

        if ($scope.HideShow.Currency == false)
            h += 55;

        return h.toString() + "px";
    }



    $("#txtSerialBarcode").keyup(function (event) {
        if (event.keyCode === 13) {
            // $scope.barcodeScanned($scope.beData.ProductBarCode);
        }
    });
    $("#txtSerialBarcode").keydown(function (event) {
        if (event.keyCode === 13 && event.ctrlKey == true) {
            $scope.enginenoScanned($scope.beData.EngineNo);
        }
    });
    $scope.enginenoScanned = function (barcode) {

        if (!barcode || barcode.length == 0)
            return;

        $scope.loadingstatus = 'running';
        showPleaseWait();

        angular.forEach($scope.CurItemAllocation.FixedProductColl, function (fp) {
            $timeout(function () {

                $scope.$apply(function () {
                    if (fp.EngineNo.trim() == barcode)
                        fp.IsSelected = true;
                });
            });

        });

        hidePleaseWait();
        $scope.loadingstatus = "stop";

    };

    $scope.PasteData = function (colName, ind) {
        var clipText = event.clipboardData.getData('text/plain');

        if (clipText) {

            if (colName == 'engineno') {
                var startInd = ind;
                $scope.loadingstatus = 'running';
                showPleaseWait();

                clipText.split("\n").forEach(function (line) {
                    if (line && line.length > 0) {

                        for (var ind = 0; ind < $scope.CurItemAllocation.FixedProductColl.length; ind++) {
                            var fp = $scope.CurItemAllocation.FixedProductColl[ind];
                            if (fp.EngineNo.trim() == line.trim()) {
                                fp.IsSelected = true;
                                break;
                            }
                        }
                        startInd++;
                    }
                });


                hidePleaseWait();
                $scope.loadingstatus = "stop";
            }

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