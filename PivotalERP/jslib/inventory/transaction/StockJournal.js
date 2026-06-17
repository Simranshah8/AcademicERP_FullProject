app.controller('StockJournal', function ($scope, $http, $timeout, $filter, GlobalServices) {
    $scope.Title = 'StockJournal';
    var glSrv = GlobalServices;
    LoadData();

    $scope.sideBarData = [];

    $scope.lastTranId = 0;
    function LoadData() {

        //$('.select2bs4').select2();
        //$('.select2').select2();

        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.currentPages = {
            SourceItemDetRow: 1,
            TargetItemDetRow: 1,
        };
        $scope.perPage = {
            SourceItemDetRow: 50,
            TargetItemDetRow: 50,
        };

        $scope.GenConfig = {};
        glSrv.getGenConfig().then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.GenConfig = res.data.Data;
            }
        }, function (reason) {
            alert('Failed' + reason);
        });

        $scope.comDet = {};
        GlobalServices.getCompanyDet().then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.comDet = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });


        $scope.VoucherSearchOptions = [{ text: 'RefNo', value: 'TS.[No]', searchType: 'text' }, { text: 'Invoice No.', value: 'TS.AutoManualNo', searchType: 'text' }, { text: 'Voucher', value: 'V.VoucherName', searchType: 'text' }, { text: 'CostClass', value: 'CC.Name', searchType: 'text' }, { text: 'VoucherDate', value: 'TS.VoucherDate', searchType: 'date' }, { text: 'Amount', value: 'TS.TotalAmount', searchType: 'number' }];

        $scope.confirmMSG = glSrv.getConfirmMSG();
        $scope.paginationOptions = {
            pageNumber: 1,
            pageSize: glSrv.getPerPageRow(),
            sort: null,
            SearchType: 'text',
            SearchCol: '',
            SearchVal: '',
            /*  SearchColDet: null,*/
            pagearray: [],
            pageOptions: [5, 10, 20, 30, 40, 50],
            SearchColDet: $scope.VoucherSearchOptions[4]
        };

        $scope.mandetoryFields = {};
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
            EntryDate: true,
            Batch: true,
            EachNarration: true,
            ExciseDuty: true,
            Vat: true,
            Amount: false,
            Rate: false

        }

        $scope.beData =
        {
            VoucherId: 0,
            TranId: 0,
            AutoManualNo: '',
            AutoVoucherNo: 0,
            VoucherDate: new Date(),
            partySideBarData: null,
            SalesMan: null,
            salesmanSideBarData: null,
            CurRate: 1,
            SourceItemDetailsColl: [],
            TargetItemDetailsColl: [],
            SourceItemColl: [],
            TargetItemColl: [],
            AttechFiles: [],
            SubTotal: 0,
            Total: 0,
            SourceGodownId: 0,
            TargetGodownId: 0,
            Narration: '',
            VoucherDate_TMP: new Date(),
            EntryDate_TMP: new Date(),
            Attributes: '',
            UDFFeildsColl: [],
        };

        $scope.beData.SourceItemDetailsColl.push(
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
                UDFFeildsColl: []
            });

        $scope.beData.TargetItemDetailsColl.push(
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
                UDFFeildsColl: []
            });


        $('.hideSideBar').on('focus', function (e) {
            $('#sidebarzz').removeClass();
            $('#sidebarzz').addClass('order-last float-right active');
        })

        $scope.UnitColl = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetAllUnit",
            dataType: "json"
            //data:JSON.stringify(para)
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
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

        }

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

    $scope.AddRowInSourceItemDetails = function (ind, boolAuto) {

        if (boolAuto == true) {
            var len = $scope.beData.SourceItemDetailsColl.length;
            if ((ind + 1) != len)
                return;

            var selectItem = $scope.beData.SourceItemDetailsColl[ind];
            if (!selectItem.ProductId || selectItem.ProductId == null || selectItem.ProductId == 0)
                return;

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

        if ($scope.beData.SourceItemDetailsColl) {
            if ($scope.beData.SourceItemDetailsColl.length > ind + 1) {
                $scope.beData.SourceItemDetailsColl.splice(ind + 1, 0, {
                    RowType: 'P',
                    ProductId: 0,
                    productDetail: null,
                    ActualQty: 0,
                    BilledQty: 0,
                    FreeQty: 0,
                    Rate: 0,
                    SourceGodownId: 0,
                    TargetGodownId: 0,
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
                    GodownId: $scope.beData.GodownId,
                    RateOf: 1,
                    LossRate: 0,
                    NetWeight: 0,
                    LossWeight: 0,
                    Makeing: 0,
                    Stone: 0,
                    FineRate: 0,
                    FineWeight: 0,
                    ProcessingRate: 0,
                    ProcessingWeight: 0,
                    UDFFeildsColl: udfColl,
                })
            } else {
                $scope.beData.SourceItemDetailsColl.push({
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
                    SourceGodownId: 0,
                    TargetGodownId: 0,
                    Description: '',
                    QtyPoint: 0,
                    UnitId: null,
                    CanEditRate: false,
                    ALValue1: 0,
                    ALValue2: 0,
                    ALUnitId1: null,
                    ALUnitId2: null,
                    GodownId: $scope.beData.GodownId,
                    RateOf: 1,
                    LossRate: 0,
                    NetWeight: 0,
                    LossWeight: 0,
                    Makeing: 0,
                    Stone: 0,
                    FineRate: 0,
                    FineWeight: 0,
                    ProcessingRate: 0,
                    ProcessingWeight: 0,
                    UDFFeildsColl: udfColl,
                })
            }
        }

    }
    $scope.delRowFromSourceItemDetails = function (ind) {
        if ($scope.beData.SourceItemDetailsColl) {
            if ($scope.beData.SourceItemDetailsColl.length > 1) {
                $scope.beData.SourceItemDetailsColl.splice(ind, 1);
            }
        }

        $scope.CalculateTotalAndSubTotal();
    }

    $scope.ChangeEngineNo = function (itemDet) {
        if (itemDet.EngineNo && itemDet.EngineNo.length > 0 && itemDet.productDetail) {

            var findB = mx(itemDet.productDetail.FixedProductColl).firstOrDefault(p1 => p1.EngineNo == itemDet.EngineNo);
            if (findB) {
                itemDet.ChassisNo = findB.ChassisNo;
                itemDet.RegdNo = findB.RegdNo;
                itemDet.Model = findB.Model;
                itemDet.Color = findB.Color;
                itemDet.KeyNo = findB.KeyNo;
                itemDet.CodeNo = findB.CodeNo;
                itemDet.MFGYear = findB.MFGYear;
                itemDet.Type = findB.Type;
                itemDet.ActualQty = 1;
                itemDet.BilledQty = 1;
                if (findB.Rate || findB.Rate > 0)
                    itemDet.Rate = findB.Rate;
            }

        } else {
            itemDet.ChassisNo = '';
            itemDet.RegdNo = '';
            itemDet.Model = '';
            itemDet.Color = '';
            itemDet.KeyNo = '';
            itemDet.CodeNo = '';
            itemDet.MFGYear = 0;
            itemDet.Type = '';
            itemDet.ActualQty = 0;
            itemDet.BilledQty = 0;
        }

        $scope.RecalculateAdditioncalCost();
        $scope.CalculateTotalAndSubTotal();
    }
    $scope.ProductSelectionChange = function (itemDet) {
        $scope.sideBarData = itemDet.sideBarData;

        var isModify = $scope.beData.TranId > 0 ? true : false;

        if (itemDet.ProductId == null) {
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
            $scope.ChangeItemRowValue(itemDet, 'product');
        } else if (itemDet.productDetail) {
            itemDet.CanEditRate = itemDet.productDetail.CanEditRate;
            //itemDet.Rate = itemDet.productDetail.SalesRate;
            if (isModify == false) {
                itemDet.Rate = itemDet.productDetail.SalesRate;
                itemDet.ProductLedgerId = itemDet.productDetail.SalesLedgerId;
                itemDet.LedgerId = itemDet.productDetail.SalesLedgerId;
            }

            itemDet.ClosingQty = $filter('formatNumber')(itemDet.productDetail.ClosingQty) + ' ' + itemDet.productDetail.BaseUnit;
            itemDet.UnitId = itemDet.productDetail.BaseUnitId;
            itemDet.UnitName = itemDet.productDetail.BaseUnit;
            // itemDet.ProductLedgerId = itemDet.productDetail.SalesLedgerId;
            itemDet.RateOf = itemDet.productDetail.RateOf;
            itemDet.LossRate = itemDet.productDetail.LossRate;
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

            if (isEmptyObj(itemDet.QtyDecimal))
                itemDet.QtyDecimal = 0;

            if (isEmptyObj(itemDet.RateDecimal))
                itemDet.RateDecimal = 2;

            if (isEmptyObj(itemDet.AmountDecimal))
                itemDet.AmountDecimal = 2;

            if (itemDet.productDetail.IsFixedProduct == true) {
                $http({
                    method: 'GET',
                    url: base_url + "Inventory/Transaction/getDueFixedProductList?productId=" + itemDet.productDetail.ProductId,
                    dataType: "json"
                }).then(function (res) {
                    if (res.data.IsSuccess && res.data.Data) {
                        itemDet.productDetail.FixedProductColl = res.data.Data;

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

                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }

            $scope.ChangeItemRowValue(itemDet, 'product');

            $timeout(function () {
                if (itemDet.productDetail.BatchWiseColl && itemDet.productDetail.BatchWiseColl.length > 0) {
                    $scope.ChangeBatch(itemDet, itemDet.productDetail.BatchWiseColl[0], 0);
                }
            });

        }

    }

    $scope.ChangeBatch = function (itemDet, batDet, ind) {
        if (itemDet && batDet) {

            itemDet.BSNo = batDet.BSNo;
            itemDet.Batch = batDet.BatchNo;
            itemDet.EXPDate = batDet.EXPDate;
            itemDet.MFGDate = batDet.MFGDate;
            itemDet.BatchBalQty = batDet.BalQty;
            itemDet.EngineNo = batDet.EngineNo;

            itemDet.SalesRate = batDet.SalesRate;
            itemDet.TradeRate = batDet.TradeRate;
            itemDet.MRP = batDet.MRP;

            if (batDet.SalesRate > 0)
                itemDet.Rate = batDet.SalesRate;

            if (batDet.Attributes && batDet.Attributes.length > 0) {
                var uValColl = JSON.parse(batDet.Attributes);
                angular.forEach(itemDet.UDFFeildsColl, function (iuc) {
                    var uVal = uValColl[iuc.NameId];
                    iuc.UDFValue = (uVal ? uVal : iuc.UDFValue);
                });
            }

            $timeout(function () {
                var cboName = 'cboBatch' + ind;
                $('#' + cboName).select2({
                    allowClear: true,
                    openOnEnter: true,
                    placeholder: '*select batch*',
                    width: '100%',
                });
            }, 100);


        } else {
            itemDet.EXPDate = null;
            itemDet.MFGDate = null;
            itemDet.BatchBalQty = 0;
            itemDet.EngineNo = '';
            itemDet.BSNo = 0;

            itemDet.SalesRate = 0;
            itemDet.TradeRate = 0;
            itemDet.MRP = 0;
        }

        $scope.CalculateTotalAndSubTotal();
    }

    $scope.ChangeItemRowValue = function (itemDet, col) {
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
                var bBal = (itemDet.BatchBalQty || itemDet.BatchBalQty > 0 ? itemDet.BatchBalQty : 0) + (itemDet.RefQty > 0 ? itemDet.RefQty : 0);
                if (itemDet.ActualQty > bBal) {

                    if (itemDet.productDetail.IgnoreNegativeBalance == false) {
                        itemDet.ActualQty = 0;
                        itemDet.BilledQty = 0;
                        Swal.fire('Please ! Enter Qty Less Then Equal ' + bBal);
                    }
                }
            }
            else {
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
        } else if (disPer > 0) {
            disAmt = amt * disPer / 100;
        } else if (disAmt > 0) {
            disPer = (disAmt / amt) * 100;
        }

        if (itemDet.ActualQty == 0 && itemDet.Rate == 0 && amt == 0) {

        } else {
            itemDet.Amount = ($filter('number')((amt - disAmt), itemDet.AmountDecimal)).parseDBL();
        }


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

            if (itemDet.ExDutyUnitId && itemDet.ExDutyUnitId > 0) {
                if (itemDet.UnitId == itemDet.ExDutyUnitId)
                    exciseAbleQty = itemDet.ActualQty;
                else if (itemDet.ALUnitId1 && itemDet.ALUnitId1 == itemDet.ExDutyUnitId)
                    exciseAbleQty = itemDet.ALValue1;
                else if (itemDet.ALUnitId2 && itemDet.ALUnitId2 == itemDet.ExDutyUnitId)
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
            else if ($scope.SelectedVoucher.Product.ProductWiseVat == true) {
                itemDet.ExDutyRate = itemDet.productDetail.EXDutyRate;
                itemDet.ExDutyAmount = exciseAbleQty * itemDet.productDetail.EXDutyRate / 100;
            }

        }


        $scope.CalculateTotalAndSubTotal();
    }

    $scope.AddRowInTargetItemDetails = function (ind, boolAuto) {

        if (boolAuto == true) {
            var len = $scope.beData.TargetItemDetailsColl.length;
            if ((ind + 1) != len)
                return;

            var selectItem = $scope.beData.TargetItemDetailsColl[ind];
            if (!selectItem.ProductId || selectItem.ProductId == null || selectItem.ProductId == 0)
                return;

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

        if ($scope.beData.TargetItemDetailsColl) {
            if ($scope.beData.TargetItemDetailsColl.length > ind + 1) {
                $scope.beData.TargetItemDetailsColl.splice(ind + 1, 0, {
                    RowType: 'P',
                    ProductId: 0,
                    productDetail: null,
                    ActualQty: 0,
                    BilledQty: 0,
                    FreeQty: 0,
                    Rate: 0,
                    SourceGodownId: 0,
                    TargetGodownId: 0,
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
                    GodownId: $scope.beData.GodownId,
                    RateOf: 1,
                    LossRate: 0,
                    NetWeight: 0,
                    LossWeight: 0,
                    Makeing: 0,
                    Stone: 0,
                    FineRate: 0,
                    FineWeight: 0,
                    ProcessingRate: 0,
                    ProcessingWeight: 0,
                    UDFFeildsColl: udfColl,
                })
            } else {
                $scope.beData.TargetItemDetailsColl.push({
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
                    SourceGodownId: 0,
                    TargetGodownId: 0,
                    Description: '',
                    QtyPoint: 0,
                    UnitId: null,
                    CanEditRate: false,
                    ALValue1: 0,
                    ALValue2: 0,
                    ALUnitId1: null,
                    ALUnitId2: null,
                    GodownId: $scope.beData.GodownId,
                    RateOf: 1,
                    LossRate: 0,
                    NetWeight: 0,
                    LossWeight: 0,
                    Makeing: 0,
                    Stone: 0,
                    FineRate: 0,
                    FineWeight: 0,
                    ProcessingRate: 0,
                    ProcessingWeight: 0,
                    UDFFeildsColl: udfColl,
                })
            }
        }

    }
    $scope.delRowFromTargetItemDetails = function (ind) {
        if ($scope.beData.TargetItemDetailsColl) {
            if ($scope.beData.TargetItemDetailsColl.length > 1) {
                $scope.beData.TargetItemDetailsColl.splice(ind, 1);
            }
        }

        $scope.CalculateTotalAndSubTotal();
    }
    $scope.TargetProductSelectionChange = function (itemDet) {
        $scope.sideBarData = itemDet.sideBarData;

        var isModify = $scope.beData.TranId > 0 ? true : false;

        if (itemDet.ProductId == null) {
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
            $scope.ChangeTargetItemRowValue(itemDet, 'product');
        } else if (itemDet.productDetail) {

            if (isModify == false) {
                itemDet.Rate = itemDet.productDetail.PurchaseRate;
                itemDet.ProductLedgerId = itemDet.productDetail.PurchaseLedgerId;
                itemDet.LedgerId = itemDet.productDetail.PurchaseLedgerId;
            }

            itemDet.CanEditRate = itemDet.productDetail.CanEditRate;
            // itemDet.Rate = itemDet.productDetail.PurchaseRate;
            itemDet.ClosingQty = $filter('formatNumber')(itemDet.productDetail.ClosingQty) + ' ' + itemDet.productDetail.BaseUnit;
            itemDet.UnitId = itemDet.productDetail.BaseUnitId;
            itemDet.UnitName = itemDet.productDetail.BaseUnit;
            //itemDet.ProductLedgerId = itemDet.productDetail.SalesLedgerId;
            itemDet.RateOf = itemDet.productDetail.RateOf;
            itemDet.LossRate = itemDet.productDetail.LossRate;
            //itemDet.ActualQty = 0;
            //itemDet.BilledQty = 0;
            //itemDet.DiscountAmt = 0;
            //itemDet.DiscountPer = 0;



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

            if (isEmptyObj(itemDet.QtyDecimal))
                itemDet.QtyDecimal = 0;

            if (isEmptyObj(itemDet.RateDecimal))
                itemDet.RateDecimal = 2;

            if (isEmptyObj(itemDet.AmountDecimal))
                itemDet.AmountDecimal = 2;

            $scope.ChangeTargetItemRowValue(itemDet, 'product');
        }

    }


    $scope.ChangeTargetItemRowValue = function (itemDet, col) {

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
                        itemDet.LossWeight = itemDet.NetWeight * itemDet.productDetail.LossRate / 100;
                }
            }

            itemDet.ActualQty = itemDet.NetWeight + itemDet.LossWeight;
        }

        if (col == 'aQty') {
            itemDet.BilledQty = itemDet.ActualQty;
        } else if (col == 'bQty') {
            if (itemDet.ActualQty == 0 || itemDet.ActualQty < itemDet.BilledQty)
                itemDet.ActualQty = itemDet.BilledQty;
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
        } else if (disPer > 0) {
            disAmt = amt * disPer / 100;
        } else if (disAmt > 0) {
            disPer = (disAmt / amt) * 100;
        }


        if (itemDet.ActualQty == 0 && itemDet.Rate == 0 && amt == 0) {

        } else {
            itemDet.Amount = ($filter('number')((amt - disAmt), itemDet.AmountDecimal)).parseDBL();
        }


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
                if (col == 'aQty' || col == 'bQty') {
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

            if (itemDet.ExDutyUnitId && itemDet.ExDutyUnitId > 0) {
                if (itemDet.UnitId == itemDet.ExDutyUnitId)
                    exciseAbleQty = itemDet.ActualQty;
                else if (itemDet.ALUnitId1 && itemDet.ALUnitId1 == itemDet.ExDutyUnitId)
                    exciseAbleQty = itemDet.ALValue1;
                else if (itemDet.ALUnitId2 && itemDet.ALUnitId2 == itemDet.ExDutyUnitId)
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
            else if ($scope.SelectedVoucher.Product.ProductWiseVat == true) {
                itemDet.ExDutyRate = itemDet.productDetail.EXDutyRate;
                itemDet.ExDutyAmount = exciseAbleQty * itemDet.productDetail.EXDutyRate / 100;
            }
        }
        $scope.CalculateTotalAndSubTotal();
    }


    $scope.CalculateTotalAndSubTotal = function () {

        if ($scope.SelectedVoucher) {
            var subTotal = 0;
            var totalQty = 0;
            angular.forEach($scope.beData.SourceItemDetailsColl, function (item) {
                subTotal += item.Amount ? item.Amount : 0;
                totalQty += item.ActualQty ? item.ActualQty : 0;
            });

            $scope.beData.SourceSubTotal = ($filter('number')(subTotal, $scope.SelectedVoucher.NoOfDecimalPlaces)).parseDBL();
            $scope.beData.SourceTotalAmount = ($filter('number')(subTotal, $scope.SelectedVoucher.NoOfDecimalPlaces)).parseDBL();

            subTotal = 0;
            totalQty = 0;
            angular.forEach($scope.beData.TargetItemDetailsColl, function (item) {
                subTotal += item.Amount ? item.Amount : 0;
                totalQty += item.ActualQty ? item.ActualQty : 0;
            });

            $scope.beData.TargetSubTotal = ($filter('number')(subTotal, $scope.SelectedVoucher.NoOfDecimalPlaces)).parseDBL();
            $scope.beData.TargetTotalAmount = ($filter('number')(subTotal, $scope.SelectedVoucher.NoOfDecimalPlaces)).parseDBL();
        }

    };

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

    $scope.SaveStockJournal = function () {

        if ($scope.IsValidData() == true) {

            var saveModify = $scope.beData.TranId > 0 ? 'Modify' : 'Save';
            Swal.fire({
                title: 'Do you want to ' + saveModify + ' the current data?',
                showCancelButton: true,
                confirmButtonText: saveModify,
            }).then((result) => {
                /* Read more about isConfirmed, isDenied below */
                if (result.isConfirmed) {



                    var reCheck = false;
                    do {
                        $scope.loadingstatus = "running";
                        showPleaseWait();

                        $scope.CalculateTotalAndSubTotal();

                        var isDetPending = false;
                        angular.forEach($scope.beData.ItemDetailsColl, function (itemDet) {
                            if (itemDet.RowType == 'P') {
                                if (itemDet.ProductId > 0 && !itemDet.productDetail) {
                                    isDetPending = true;
                                }
                            } else if (itemDet.RowType == 'L') {
                                if (itemDet.LedgerId > 0 && !itemDet.costLedgerDetail) {
                                    isDetPending = true;
                                }
                            }
                        });

                        if (isDetPending == false) {
                            $scope.loadingstatus = "stop";
                            hidePleaseWait();
                            reCheck = true;
                            break;
                        }

                    } while (reCheck == false);

                    $scope.loadingstatus = "running";
                    showPleaseWait();
                    $scope.CalculateTotalAndSubTotal();

                    var filesColl = $scope.beData.AttechFiles;
                    $scope.beData.AttechFiles = [];

                    $http({
                        method: 'POST',
                        url: base_url + "Inventory/Transaction/SaveUpdateStockJournal",
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
                            $scope.ClearData();
                        }
                        else {
                            Swal.fire(res.data.ResponseMSG);
                        }

                    }, function (errormessage) {
                        hidePleaseWait();
                        $scope.loadingstatus = "stop";

                    });
                }
            });
        }

    }

    $scope.IsValidData = function () {
        var result = true;


        return result;
    }

    $scope.AddBlankRow = function () {

        if ($scope.beData.SourceItemDetailsColl && $scope.beData.SourceItemDetailsColl.length == 0) {
            $scope.beData.SourceItemDetailsColl.push(
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

        }
        if ($scope.beData.TargetItemDetailsColl && $scope.beData.TargetItemDetailsColl.length == 0) {
            $scope.beData.TargetItemDetailsColl.push(
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
        }
    };
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
            SourceItemColl: [],
            TargetItemColl: [],
            GodownId: $scope.beData.GodownId,
            DocumentColl: $scope.beData.DocumentColl,
            Attributes: '',
        };


        var voucherUDFFields = [];
        angular.forEach($scope.beData.UDFFeildsColl, function (udf) {
            var ud = {
                SNo: (udf.FieldNo ? udf.FieldNo : udf.SNo),
                Name: udf.Name,
                Value: udf.UDFValue
            };
            voucherUDFFields.push(ud);
        });
        if (voucherUDFFields.length > 0)
            tmpSales.Attributes = JSON.stringify(voucherUDFFields);

        angular.forEach($scope.beData.SourceItemDetailsColl, function (itemDet) {
            if (itemDet.ProductId && itemDet.ProductId > 0) {

                var udfValues = '';
                var udfFields = [];
                angular.forEach(itemDet.UDFFeildsColl, function (udf) {
                    var ud = {
                        SNo: (udf.FieldNo ? udf.FieldNo : udf.SNo),
                        Name: udf.Name,
                        Value: udf.UDFValue
                    };
                    udfFields.push(ud);
                });
                if (udfFields.length > 0)
                    udfValues = JSON.stringify(udfFields);

                var itemAllocation = {
                    Attributes: udfValues,
                    ProductId: itemDet.ProductId,
                    ActualQty: isEmptyNum(itemDet.ActualQty),
                    BilledQty: isEmptyNum(itemDet.BilledQty),
                    UnitId: itemDet.UnitId,
                    Rate: isEmptyNum(itemDet.Rate),
                    Amount: isEmptyNum(itemDet.Amount),
                    DiscountAmt: isEmptyNum(itemDet.DiscountAmt),
                    DiscountPer: isEmptyNum(itemDet.DiscountPer),
                    SchameAmt: isEmptyNum(itemDet.SchameAmt),
                    SchamePer: isEmptyNum(itemDet.SchamePer),
                    ALUnitId1: itemDet.ALUnitId1 ? itemDet.ALUnitId1 : 0,
                    ALUnitId2: itemDet.ALUnitId2 ? itemDet.ALUnitId2 : 0,
                    ALUnitId3: itemDet.ALUnitId3 ? itemDet.ALUnitId3 : 0,
                    ALValue1: itemDet.ALValue1 ? itemDet.ALValue1 : 0,
                    ALValue2: itemDet.ALValue2 ? itemDet.ALValue2 : 0,
                    ALValue3: itemDet.ALValue3 ? itemDet.ALValue3 : 0,
                    Narration: itemDet.Narration,
                    Batch: itemDet.Batch,
                    EXPDate: (itemDet.EXPDate && itemDet.EXPDateDet ? $filter('date')(new Date(itemDet.EXPDateDet), 'yyyy-MM-dd') : null),
                    MFGDate: (itemDet.MFGDate && itemDet.MFGDateDet ? $filter('date')(new Date(itemDet.MFGDateDet), 'yyyy-MM-dd') : null),
                    BundleId: 0,
                    BundleQty: 0,
                    RegdNo: itemDet.RegdNo ? itemDet.RegdNo : '',
                    EngineNo: itemDet.EngineNo ? itemDet.EngineNo : '',
                    ChassisNo: itemDet.ChassisNo ? itemDet.ChassisNo : '',
                    Model: itemDet.Model ? itemDet.Model : '',
                    CodeNo: itemDet.CodeNo ? itemDet.CodeNo : '',
                    Color: itemDet.Color ? itemDet.Color : '',
                    KeyNo: itemDet.KeyNo ? itemDet.KeyNo : '',
                    MFGYear: itemDet.MFGYear ? itemDet.MFGYear : 0,
                    Type: itemDet.Type ? itemDet.Type : '',
                    Description: itemDet.Description ? itemDet.Description : '',
                    LedgerId: itemDet.ProductLedgerId ? itemDet.ProductLedgerId : 0,
                    GodownId: (itemDet.GodownId > 0 ? itemDet.GodownId : (tmpSales.GodownId > 0 ? tmpSales.GodownId : 0)),
                    VatRate: isEmptyNum(itemDet.VatRate),
                    VatAmount: isEmptyNum(itemDet.VatAmount),
                    VatAbleAmt: isEmptyNum(itemDet.VatAbleAmt),
                    ExDutyRate: isEmptyNum(itemDet.ExDutyRate),
                    ExDutyAmount: isEmptyNum(itemDet.ExDutyAmount),
                    Description: itemDet.Description,
                    AbbRate: isEmptyNum(itemDet.Rate),
                    AbbAmount: isEmptyNum(itemDet.Amount),
                    IsTaxable: (itemDet.productDetail ? itemDet.productDetail.IsTaxable : true),
                    RateOf: (itemDet.RateOf ? itemDet.RateOf : 1),
                    NetWeight: (itemDet.NetWeight ? itemDet.NetWeight : 0),
                    LossWeight: (itemDet.LossWeight ? itemDet.LossWeight : 0),
                    LossRate: (itemDet.LossRate ? itemDet.LossRate : 0),
                    FineWeight: (itemDet.FineWeight > 0 ? itemDet.FineWeight : 0),
                    FineRate: (itemDet.FineRate > 0 ? itemDet.FineRate : 0),
                    ProcessingWeight: (itemDet.ProcessingWeight > 0 ? itemDet.ProcessingWeight : 0),
                    ProcessingRate: (itemDet.ProcessingRate > 0 ? itemDet.ProcessingRate : 0),
                    Makeing: (itemDet.Makeing ? itemDet.Makeing : 0),
                    Stone: (itemDet.Stone ? itemDet.Stone : 0),
                };

                tmpSales.SourceItemColl.push(itemAllocation);
            }
        });

        angular.forEach($scope.beData.TargetItemDetailsColl, function (itemDet) {
            if (itemDet.ProductId && itemDet.ProductId > 0) {

                var udfValues = '';
                var udfFields = [];
                angular.forEach(itemDet.UDFFeildsColl, function (udf) {
                    var ud = {
                        SNo: (udf.FieldNo ? udf.FieldNo : udf.SNo),
                        Name: udf.Name,
                        Value: udf.UDFValue
                    };
                    udfFields.push(ud);
                });
                if (udfFields.length > 0)
                    udfValues = JSON.stringify(udfFields);

                var itemAllocation = {
                    Attributes: udfValues,
                    ProductId: itemDet.ProductId,
                    ActualQty: isEmptyNum(itemDet.ActualQty),
                    BilledQty: isEmptyNum(itemDet.BilledQty),
                    UnitId: itemDet.UnitId,
                    Rate: isEmptyNum(itemDet.Rate),
                    Amount: isEmptyNum(itemDet.Amount),
                    DiscountAmt: isEmptyNum(itemDet.DiscountAmt),
                    DiscountPer: isEmptyNum(itemDet.DiscountPer),
                    SchameAmt: isEmptyNum(itemDet.SchameAmt),
                    SchamePer: isEmptyNum(itemDet.SchamePer),
                    ALUnitId1: itemDet.ALUnitId1 ? itemDet.ALUnitId1 : 0,
                    ALUnitId2: itemDet.ALUnitId2 ? itemDet.ALUnitId2 : 0,
                    ALUnitId3: itemDet.ALUnitId3 ? itemDet.ALUnitId3 : 0,
                    ALValue1: itemDet.ALValue1 ? itemDet.ALValue1 : 0,
                    ALValue2: itemDet.ALValue2 ? itemDet.ALValue2 : 0,
                    ALValue3: itemDet.ALValue3 ? itemDet.ALValue3 : 0,
                    Narration: itemDet.Narration,
                    Batch: itemDet.Batch,
                    EXPDate: (itemDet.EXPDate && itemDet.EXPDateDet ? $filter('date')(new Date(itemDet.EXPDateDet), 'yyyy-MM-dd') : null),
                    MFGDate: (itemDet.MFGDate && itemDet.MFGDateDet ? $filter('date')(new Date(itemDet.MFGDateDet), 'yyyy-MM-dd') : null),
                    BundleId: 0,
                    BundleQty: 0,
                    RegdNo: itemDet.RegdNo ? itemDet.RegdNo : '',
                    EngineNo: itemDet.EngineNo ? itemDet.EngineNo : '',
                    ChassisNo: itemDet.ChassisNo ? itemDet.ChassisNo : '',
                    Model: itemDet.Model ? itemDet.Model : '',
                    CodeNo: itemDet.CodeNo ? itemDet.CodeNo : '',
                    Color: itemDet.Color ? itemDet.Color : '',
                    KeyNo: itemDet.KeyNo ? itemDet.KeyNo : '',
                    MFGYear: itemDet.MFGYear ? itemDet.MFGYear : 0,
                    Type: itemDet.Type ? itemDet.Type : '',
                    Description: itemDet.Description ? itemDet.Description : '',
                    LedgerId: itemDet.ProductLedgerId ? itemDet.ProductLedgerId : 0,
                    GodownId: (itemDet.GodownId > 0 ? itemDet.GodownId : (tmpSales.GodownId > 0 ? tmpSales.GodownId : 0)),
                    VatRate: isEmptyNum(itemDet.VatRate),
                    VatAmount: isEmptyNum(itemDet.VatAmount),
                    VatAbleAmt: isEmptyNum(itemDet.VatAbleAmt),
                    ExDutyRate: isEmptyNum(itemDet.ExDutyRate),
                    ExDutyAmount: isEmptyNum(itemDet.ExDutyAmount),
                    Description: itemDet.Description,
                    AbbRate: isEmptyNum(itemDet.Rate),
                    AbbAmount: isEmptyNum(itemDet.Amount),
                    IsTaxable: (itemDet.productDetail ? itemDet.productDetail.IsTaxable : true),
                    RateOf: (itemDet.RateOf ? itemDet.RateOf : 1),
                    NetWeight: (itemDet.NetWeight ? itemDet.NetWeight : 0),
                    LossWeight: (itemDet.LossWeight ? itemDet.LossWeight : 0),
                    LossRate: (itemDet.LossRate ? itemDet.LossRate : 0),
                    FineWeight: (itemDet.FineWeight > 0 ? itemDet.FineWeight : 0),
                    FineRate: (itemDet.FineRate > 0 ? itemDet.FineRate : 0),
                    ProcessingWeight: (itemDet.ProcessingWeight > 0 ? itemDet.ProcessingWeight : 0),
                    ProcessingRate: (itemDet.ProcessingRate > 0 ? itemDet.ProcessingRate : 0),
                    Makeing: (itemDet.Makeing ? itemDet.Makeing : 0),
                    Stone: (itemDet.Stone ? itemDet.Stone : 0),
                };

                tmpSales.TargetItemColl.push(itemAllocation);
            }
        });

        return tmpSales;
    };

    $scope.SetData = function (tran) {

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

        $scope.beData.TotalAmount = tran.TotalAmount;
        $scope.beData.EntryDate = new Date(tran.EntryDate);
        $scope.beData.BranchId = (tran.BranchId ? tran.BranchId : 0);
        $scope.beData.DocumentColl = tran.DocumentColl;

        $scope.beData.SourceItemDetailsColl = [];
        $scope.beData.TargetItemDetailsColl = [];
        angular.forEach(tran.SourceItemColl, function (itemA) {
            itemA.RowType = 'P';

            if ($scope.SelectedVoucher.Product && $scope.SelectedVoucher.Product.VoucherWiseDecimalPlaces == true) {
                itemA.QtyDecimal = $scope.SelectedVoucher.Product.QtyNoOfDecimalPlaces;
                itemA.RateDecimal = $scope.SelectedVoucher.Product.RateNoOfDecimalPlaces;
                itemA.AmountDecimal = $scope.SelectedVoucher.Product.AmountNoOfDecimalPlaces;
            } else {
                var findUnit = $scope.UnitColl.firstOrDefault(p1 => p1.UnitId == itemA.UnitId);
                if (findUnit) {
                    itemA.QtyDecimal = findUnit.NoOfDecimalPlaces;
                    itemA.RateDecimal = findUnit.RateNoOfDecimalPlaces;
                    itemA.AmountDecimal = findUnit.AmountNoOfDecimalPlaces;
                }
            }

            $scope.beData.SourceItemDetailsColl.push(itemA);
        });

        angular.forEach(tran.TargetItemColl, function (itemA) {
            itemA.RowType = 'P';

            if ($scope.SelectedVoucher.Product && $scope.SelectedVoucher.Product.VoucherWiseDecimalPlaces == true) {
                itemA.QtyDecimal = $scope.SelectedVoucher.Product.QtyNoOfDecimalPlaces;
                itemA.RateDecimal = $scope.SelectedVoucher.Product.RateNoOfDecimalPlaces;
                itemA.AmountDecimal = $scope.SelectedVoucher.Product.AmountNoOfDecimalPlaces;
            } else {
                var findUnit = $scope.UnitColl.firstOrDefault(p1 => p1.UnitId == itemA.UnitId);
                if (findUnit) {
                    itemA.QtyDecimal = findUnit.NoOfDecimalPlaces;
                    itemA.RateDecimal = findUnit.RateNoOfDecimalPlaces;
                    itemA.AmountDecimal = findUnit.AmountNoOfDecimalPlaces;
                }
            }

            $scope.beData.TargetItemDetailsColl.push(itemA);
        });

        $scope.AddBlankRow();

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

                                //if ($scope.SelectedVoucher.Product.ProductWiseLedger == true) {
                                //    $scope.HideShow.ProductLedger = false;

                                //    if ($scope.SelectedVoucher.Product.ShowProductWiseLedger == true)
                                //        $scope.HideShow.ShowProductWiseLedger = false;
                                //    else
                                //        $scope.HideShow.ShowProductWiseLedger = true;
                                //}
                                //else {
                                //    $scope.HideShow.ProductLedger = true;
                                //    $scope.HideShow.ShowProductWiseLedger = true;
                                //}


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

                                $scope.SelectedVoucher.VoucherWiseGodownColl = $scope.GodownColl;

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
                                        if (angular.forEach($scope.beData.SourceItemDetailsColl, function (idet) {
                                            if (!idet.GodownId || idet.GodownId == 0)
                                                idet.GodownId = $scope.beData.GodownId;
                                        }));
                                    }
                                    if ($scope.beData.GodownId && $scope.beData.GodownId > 0) {
                                        if (angular.forEach($scope.beData.TargetItemDetailsColl, function (idet) {
                                            if (!idet.GodownId || idet.GodownId == 0)
                                                idet.GodownId = $scope.beData.GodownId;
                                        }));
                                    }
                                } else
                                    $scope.SelectedVoucher.VoucherWiseGodownColl = $scope.GodownColl;



                                if ($scope.SelectedVoucher.VoucherProductUDFColl && $scope.SelectedVoucher.VoucherProductUDFColl.length > 0) {
                                    angular.forEach($scope.beData.SourceItemDetailsColl, function (det) {
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


                                    angular.forEach($scope.beData.TargetItemDetailsColl, function (det) {
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
        $timeout(function () {

            var sV = $scope.SelectedVoucher;
            var sC = $scope.SelectedCostClass;

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
                AttechFiles: [],
                SubTotal: 0,
                Total: 0,
                Narration: '',
                VoucherDate: new Date(),
                VoucherDate_TMP: new Date(),
                EntryDate_TMP: new Date(),
                SourceItemColl: [],
                TargetItemColl: [],
                SourceItemDetailsColl: [],
                TargetItemDetailsColl: [],

            };

            $scope.AddBlankRow();

            if ($scope.GodownColl.length > 0) {
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

    $scope.GetTransactionById = function (tran) {
        $timeout(function () {

            $scope.ClearData();

            $timeout(function () {
                if (tran.TranId && tran.TranId > 0) {

                    $scope.loadingstatus = "running";
                    showPleaseWait();

                    var para = {
                        tranId: tran.TranId
                    };
                    $http({
                        method: 'POST',
                        url: base_url + "Inventory/Transaction/GetStockJournalById",
                        dataType: "json",
                        data: JSON.stringify(para)
                    }).then(function (res) {
                        $timeout(function () {
                            if (res.data.IsSuccess && res.data.Data) {
                                var tran = res.data.Data;
                                $scope.SetData(tran);
                                $('#searVoucherRightBtn').modal('hide');
                                $scope.loadingstatus = "stop";
                                hidePleaseWait();
                            } else
                                Swal.fire(res.data.ResponseMSG);
                        });
                    }, function (reason) {
                        Swal.fire('Failed' + reason);
                    });
                }
            });

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

    $scope.pageChangeHandler = function (num) {
        console.log('page changed to ' + num);


        $scope.loadingstatus = "running";
        showPleaseWait();

        $timeout(function () {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
        });
    };


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

    $scope.ChangeItemAlternetQty = function (itemDet, col) {

        if (col == 'unit1') {
            if ($scope.SelectedVoucher.Product.ActiveRateUOM == true && itemDet.productDetail.AlternetUnitColl) {
                var alColl = mx(itemDet.productDetail.AlternetUnitColl);
                itemDet.RateUOMColl = [];
                itemDet.RateUOMColl.push({
                    UnitId: itemDet.UnitId,
                    Name: itemDet.UnitName
                });
                if (itemDet.ALUnitId1 > 0 && ($scope.HideShow.AlternetUnit1 == false || $scope.HideShow.AlternetUnitMultiple == false)) {
                    if (itemDet.ALUnitId1 != itemDet.UnitId) {
                        var findU = alColl.firstOrDefault(p1 => p1.UnitId == itemDet.ALUnitId1);
                        if (findU) {
                            itemDet.RateUOMColl.push({
                                UnitId: findU.UnitId,
                                Name: findU.UnitName
                            });
                        }
                    }
                }
                if (itemDet.ALUnitId2 > 0 && $scope.HideShow.AlternetUnit2 == false) {
                    if (itemDet.ALUnitId2 != itemDet.UnitId && itemDet.ALUnitId2 != itemDet.ALUnitId1) {
                        var findU = alColl.firstOrDefault(p1 => p1.UnitId == itemDet.ALUnitId2);
                        if (findU) {
                            itemDet.RateUOMColl.push({
                                UnitId: findU.UnitId,
                                Name: findU.UnitName
                            });
                        }
                    }
                }
                if (itemDet.RateUnitId > 0) {

                } else {
                    itemDet.RateUnitId = itemDet.UnitId;
                }
            }
        }

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

                alternetUnit1 = alterUnit_Qry.firstOrDefault(p1 => p1.UnitId == itemDet.ALUnitId1);
                alternetUnit2 = alterUnit_Qry.firstOrDefault(p1 => p1.UnitId == itemDet.ALUnitId2);

                if (!alternetUnit1)
                    alternetUnit1 = alterUnit_Qry.firstOrDefault(p1 => p1.SNo == 1);

                if (!alternetUnit2)
                    alternetUnit2 = alterUnit_Qry.firstOrDefault(p1 => p1.SNo == 2);

                var baseQty = 0;
                var findUnit = $scope.UnitColl.firstOrDefault(p1 => p1.UnitId == itemDet.productDetail.BaseUnitId);
                if (alternetUnit1 && col == 'unit1') {

                    if (findUnit) {
                        //baseQty = parseFloat(parseFloat((alternetUnit1.BaseUnitValue * itemDet.ALValue1) / alternetUnit1.AlternetUnitValue).toFixed(alternetUnit1.NoOfDecimalPlaces));
                        baseQty = parseFloat(parseFloat((alternetUnit1.BaseUnitValue * itemDet.ALValue1) / alternetUnit1.AlternetUnitValue));//.toFixed(itemDet.QtyDecimal));
                        if ($scope.SelectedVoucher.Product.NotEffectInBaseUnit == false)
                            itemDet.ActualQty = baseQty;

                        if (alternetUnit2)
                            itemDet.ALValue2 = parseFloat(parseFloat((alternetUnit2.AlternetUnitValue * baseQty) / alternetUnit2.BaseUnitValue));//.toFixed(alternetUnit2.NoOfDecimalPlaces));
                    }

                } else if (alternetUnit2 && col == "unit2") {

                    if (findUnit) {
                        //  baseQty = parseFloat(parseFloat(itemDet.ALValue2 * alternetUnit2.AlternetUnitValue).toFixed(findUnit.NoOfDecimalPlaces));
                        baseQty = parseFloat(parseFloat((alternetUnit2.BaseUnitValue * itemDet.ALValue2) / alternetUnit2.AlternetUnitValue));//.toFixed(itemDet.QtyDecimal));

                        if ($scope.SelectedVoucher.Product.NotEffectInBaseUnit == false)
                            itemDet.ActualQty = baseQty;

                        if (alternetUnit1)
                            itemDet.ALValue1 = parseFloat(parseFloat((alternetUnit1.AlternetUnitValue * baseQty) / alternetUnit1.BaseUnitValue));//.toFixed(alternetUnit1.NoOfDecimalPlaces));
                    }

                }
            }
        }

        $scope.ChangeItemRowValue(itemDet, 'rate');

    }

    $scope.validateForDate = function (obj, key, keyTMP) {
        if (!obj[key] || !obj[key].dateAD) {
            return true;
        }

        var forDate = $filter('date')(obj[key].dateAD, 'yyyy-MM-dd');
        var today = $filter('date')(new Date(), 'yyyy-MM-dd');

        if (!forDate) return true;

        if (forDate > today) {
            Swal.fire({
                icon: 'warning',
                text: 'Date cannot be a future date.',
                confirmButtonText: 'OK'
            }).then(function () {
                $scope.$apply(function () {
                    obj[keyTMP] = new Date();
                    obj[key] = new Date();
                });
            });
            return false;
        }

        if (key === 'VoucherDateDet')
            $scope.getVoucherNoOnly();
        return true;
    }

});