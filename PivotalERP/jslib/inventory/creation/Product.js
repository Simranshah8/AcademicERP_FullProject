app.controller('Product', function ($scope, $filter, $http, $timeout, GlobalServices) {
    $scope.Title = 'Product ';
     
    var glSrv = GlobalServices;
    $scope.LoadData = function () {
        $scope.confirmMSG = GlobalServices.getConfirmMSG();

        $('.select2').select2({
            allowClear: true,
           // openOnEnter: true
        });

        $scope.ExciseOnColl = [{ id: 1, text: 'Qty' }, { id: 2, text: 'Amount' }];
        $scope.VoucherSearchOptions = [{ text: 'Name', value: 'P.Name', searchType: 'text' }, { text: 'Code', value: 'P.Code', searchType: 'text' }];
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

        $scope.newProduct = {

            SNo: 0,
            Name: '',
            Description: '',
            Alias: '',
            Code: '',
            BaseUnitId: 2,
            ProductCategoriesId: 1,
            ProductTypeId: 1,
            PurchaseLedgerId: 0,
            SalesLedgerId: 0,
            ProductDivision: 0,
            ProductDivisionId: 0,
            ProductBrandId: 0,
            ProductCompanyId: 0,
            ProductColorId: 0,
            ProductFlavourId: 0,
            ProductShapeId: 0,
            CompanyName: 0,
            PurchaseCCRate: 0,
            SalesCCRate: 0,
            WarrantyMonth: 0,
            TSCRate: 0,
            VatRate: 0,
            OpeningQty: 0,
            PurchaseRate: 0,
            OpeningForBranchId: 0,
            EXDutyRate: 0,
            ExDutyUnitId: 0,
            TreatAllSalesAsNewManufacture: false,
            TreatAllPurchaseAsConsumed: false,
            TreatllRejectionInwardAsScraped: false,
            PartNo: '',
            PartNoAlisas: '',
            Remarks: '',
            IsFixedProduct: false,
            IgnoreNegativeBalance: false,
            CanEditRate: true,
            CanEditRatePurchase: true,
            ActiveAlternativeUnit: false,
            SetStandardRate: true,
            SetGodownWiseOpening: false,
            MantainBatchWise: false,
            UseMfgDate: false,
            UseExpDate: false,
            IncludingVat: false,
            Rate: 0,
            IsTaxable: false,
            PhotoPath: '',
            MaintainBatchWise: false,
            TermCondition: '',
            Mode: 'Save',
            AlterNetUnitColl: [],
            CostRateColl: [],
            SellingRateColl: [],
            TradeRateColl: [],
            MRPRateColl: [],
            OpeningColl: [],
            QtyDecimal: 3,
            RateDecimal: 3,
            AmountDecimal: 2,
            RateOf: 0,
            LossRate: 0,
            DealerCommissionRate: 0,
            SankuchanLoss: 0,
            ActivitiesLoss: 0,
            SankuchanCostCenterId: null,
            ActivitiesCostCenterId: null,
            CostCenterDetailsS: null,
            CostCenterDetailsA: null,
            PreferedSupplierColl: [],
            IsActive: true,
            ExciseOn: 2,
            DiscountOn: null,
            ProductGroupId:1
        }

        $scope.comDet = {};
        GlobalServices.getCompanyDet().then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.comDet = res.data.Data;

                if ($scope.comDet.Maintain == 3 || $scope.comDet.Maintain == 4) {
                    $scope.newProduct.MaintainBatchWise = true;
                    $scope.newProduct.UseExpDate = true;
                    $scope.newProduct.IsTaxable = false;
                    $scope.newProduct.VatRate = 0;
                }
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.FixedProductConfig = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetFixedProductConfig",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.FixedProductConfig = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.UDFFeildsColl = [];
        var para11 = {
            EntityId: ProductEntity
        };
        $http({
            method: 'POST',
            url: base_url + "Setup/Security/getUDFByEntitId",
            dataType: "json",
            data: JSON.stringify(para11)
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.UDFFeildsColl = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.BranchList = [];
        $http({
            method: 'GET',
            url: base_url + "Setup/Security/GetAllBranchList",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BranchList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.ProductShapeList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetProductShape",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ProductShapeList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed !' + reason);

        });


        $scope.RackList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetAllRack",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.RackList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed !' + reason);

        });

        $scope.ProductBrandList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetAllProductBrand",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ProductBrandList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed ! ' + reason);
        });

        $scope.ProductTypeList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetAllProductType",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ProductTypeList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed ! ' + reason);
        });
        $scope.ProductDivisionList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetAllProductDivision",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ProductDivisionList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed ! ' + reason);
        });

        $scope.ProductCompanyList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetAllProductCompany",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ProductCompanyList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.BaseUnitList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetAllUnit",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {

                if(res.data.Data.length>0)
                    res.data.Data.splice(0, 1);

                $scope.BaseUnitList = res.data.Data;
                $scope.UnitList = res.data.Data;
                 
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.ProductGroupList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetProductGroup",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ProductGroupList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.ProductCategoriesList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetProductCategories",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ProductCategoriesList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
        $scope.GodownList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetAllGodown",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.GodownList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.ProductCostingList = [];
        $http({
            method: 'GET',
            url: base_url + "Global/GetProductCostingMethod",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ProductCostingList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
        $scope.MarketValuationList = [];
        $http({
            method: 'GET',
            url: base_url + "Global/GetProductMarketValuation",
            dataType: "json",
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.MarketValuationList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });

        $scope.ProductColorList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetProductColor",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ProductColorList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed ! ' + reason);
        });

        $scope.ProductFlavourList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetProductFlavour",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ProductFlavourList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed ! ' + reason);
        });

        $scope.ProductShapeList = [];
        $http({
            method: 'GET',
            url: base_url + "Inventory/Creation/GetProductShape",
            dataType: "json"
        }).then(function (res) {
            if (res.data.IsSuccess && res.data.Data) {
                $scope.ProductShapeList = res.data.Data;
            }
        }, function (reason) {
            Swal.fire('Failed ! ' + reason);
        });

      
        $scope.AddBlankRow();
    };

    $scope.GenerateCode = function () {

        if ($scope.newProduct.ProductId > 0 && $scope.newProduct.Code.length > 0)
            return;

        $scope.newProduct.Code = '';
        var para = {
            name: $scope.newProduct.Name,
            productGroupId: $scope.newProduct.ProductGroupId
        };
        $http({
            method: 'POST',
            url: base_url + "Inventory/Creation/GetProductCode",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $timeout(function () {
                if (res.data.IsSuccess && res.data.Data) {
                    $scope.newProduct.Code = res.data.Data.ResponseId;
                }
            });
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    };

    $scope.ClearProduct = function () {

        angular.forEach($scope.UDFFeildsColl, function (uf) {
            uf.Value = '';
            uf.AlterNetValue = '';
        });


        $timeout(function () {

            $('#imgProductPhoto').attr('src', '');

            $scope.newProduct = {
                SNo: 0,
                Name: '',
                Description: '',
                Alias: '',
                Code: '',
                BaseUnitId: 2,
                ProductCategoriesId: 1,
                ProductTypeId: 1,
                PurchaseLedgerId: 0,
                SalesLedgerId: 0,
                ProductDivision: 0,
                ProductDivisionId: 0,
                ProductBrandId: 0,
                ProductCompanyId: 0,
                ProductColorId: 0,
                ProductFlavourId: 0,
                ProductShapeId: 0,
                CompanyName: 0,
                PurchaseCCRate: 0,
                SalesCCRate: 0,
                WarrantyMonth: 0,
                TSCRate: 0,
                VatRate: 0,
                OpeningQty: 0,
                PurchaseRate: 0,
                OpeningForBranchId: 0,
                EXDutyRate: 0,
                ExDutyUnitId: 0,
                TreatAllSalesAsNewManufacture: false,
                TreatAllPurchaseAsConsumed: false,
                TreatllRejectionInwardAsScraped: false,
                PartNo: '',
                PartNoAlisas: '',
                Remarks: '',
                IsFixedProduct: false,
                IgnoreNegativeBalance: false,
                CanEditRate: true,
                CanEditRatePurchase: true,
                ActiveAlternativeUnit: false,
                SetStandardRate: true,
                SetGodownWiseOpening: false,
                MantainBatchWise: false,
                UseMfgDate: false,
                UseExpDate: false,
                IncludingVat: false,
                Rate: 0,
                IsTaxable: false,
                PhotoPath: '',
                PhotoData:null,
                MaintainBatchWise:false,
                Mode: 'Save',
                Photo_TMP: null,
                TermCondition: '',
                AlterNetUnitColl:[],
                CostRateColl:[],
                SellingRateColl:[],
                TradeRateColl:[],
                MRPRateColl:[],
                OpeningColl: [],
                QtyDecimal: 3,
                RateDecimal: 3,
                AmountDecimal: 2,
                RateOf: 0,
                LossRate: 0,
                DealerCommissionRate: 0,
                SankuchanLoss: 0,
                ActivitiesLoss: 0,
                SankuchanCostCenterId: null,
                ActivitiesCostCenterId: null,
                CostCenterDetailsS: null,
                CostCenterDetailsA: null,
                IsActive: true,
                ExciseOn: 2,
                DiscountOn: null,
                ProductGroupId: 1
            };

            if ($scope.comDet) {
                if ($scope.comDet.Maintain == 3 || $scope.comDet.Maintain == 4) {
                    $scope.newProduct.MaintainBatchWise = true;
                    $scope.newProduct.UseExpDate = true;
                    $scope.newProduct.IsTaxable = false;
                    $scope.newProduct.VatRate = 0;
                }
            }
            
            $scope.newProduct.TermCondition = '';

            $scope.AddBlankRow();

        });
    }
    $scope.ChangeParticularCostCenter = function (pr) {

    }

    $scope.IsValidProduct = function () {
        if ($scope.newProduct.Name.isEmpty()) {
            Swal.fire("Please ! Enter Product Name");
            return false;
        }
        else
            return true;
    }

    $scope.SaveUpdateProduct = function () {
        if ($scope.IsValidProduct() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newProduct.Mode;
                Swal.fire({
                    title: 'Do you want to' + saveModify + 'the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateProduct();
                    }

                });
            }
            else
                $scope.CallSaveUpdateProduct();
        }
    };

    $scope.CallSaveUpdateProduct = function () {
        $scope.loadingstatus = 'running';
        showPleaseWait();

        var productPhoto = $scope.newProduct.Photo_TMP;

        if (!$scope.newProduct.BDId)
            $scope.newProduct.BDId = 0;

        var AlterNetUnitColl = $scope.newProduct.AlterNetUnitColl;
        var CostRateColl = $scope.newProduct.CostRateColl;
        var SellingRateColl = $scope.newProduct.SellingRateColl;
        var OpeningColl = $scope.newProduct.OpeningColl;
        var TradeRateColl = $scope.newProduct.TradeRateColl;
        var MRPRateColl = $scope.newProduct.MRPRateColl;
        var preSuppColl = $scope.newProduct.PreferedSupplierColl;

        $scope.newProduct.PreferedSupplierIdColl = [];
        angular.forEach(preSuppColl, function (ss) {
            if (ss.PartyLedgerId > 0)
                $scope.newProduct.PreferedSupplierIdColl.push(ss.PartyLedgerId);
        });
        $scope.newProduct.AlterNetUnitColl = [];
        angular.forEach(AlterNetUnitColl, function (au)
        {
            if (au && au.AlterNetUnitId) {
                if (au.BaseUnitValue > 0 && au.AlterNetUnitValue > 0 && au.AlterNetUnitId > 0)
                    $scope.newProduct.AlterNetUnitColl.push(au);
            } 
        });

        $scope.newProduct.CostRateColl = [];
        angular.forEach(CostRateColl, function (cr) {
            if (cr.ApplicableFromDet && cr.Rate > 0) {
                cr.ApplicableFrom = new $filter('date')(new Date(cr.ApplicableFromDet.dateAD), 'yyyy-MM-dd');

                if (!cr.RateOf)
                    cr.RateOf = 1;

                $scope.newProduct.CostRateColl.push(cr);
            }
        });
        $scope.newProduct.SellingRateColl = [];
        angular.forEach(SellingRateColl, function (cr) {
            if (cr.ApplicableFromDet && cr.Rate > 0) {
                cr.ApplicableFrom = new $filter('date')(new Date(cr.ApplicableFromDet.dateAD), 'yyyy-MM-dd');

                if (!cr.RateOf)
                    cr.RateOf = 1;

                $scope.newProduct.SellingRateColl.push(cr);
            }
        });


        $scope.newProduct.TradeRateColl = [];
        angular.forEach(TradeRateColl, function (cr) {
            if (cr.ApplicableFromDet && cr.Rate > 0) {
                cr.ApplicableFrom = new $filter('date')(new Date(cr.ApplicableFromDet.dateAD), 'yyyy-MM-dd');

                if (!cr.RateOf)
                    cr.RateOf =1;

                $scope.newProduct.TradeRateColl.push(cr);
            }
        });

        $scope.newProduct.MRPRateColl = [];
        angular.forEach(MRPRateColl, function (cr) {
            if (cr.ApplicableFromDet && cr.Rate > 0) {
                cr.ApplicableFrom = new $filter('date')(new Date(cr.ApplicableFromDet.dateAD), 'yyyy-MM-dd');

                if (!cr.RateOf)
                    cr.RateOf = 1;

                $scope.newProduct.MRPRateColl.push(cr);
            }
        });

        $scope.newProduct.OpeningColl = [];
        angular.forEach(OpeningColl, function (op) {
            if (op.GodownId > 0 && (op.Quantity > 0 || op.Amount > 0)) {
                op.UnitId = $scope.newProduct.BaseUnitId;
 
                op.EXPDate = (op.EXPDateDet ? $filter('date')(new Date(op.EXPDateDet), 'yyyy-MM-dd') : null);
                op.MFGDate = (op.MFGDateDet ? $filter('date')(new Date(op.MFGDateDet), 'yyyy-MM-dd') : null);

                if (op.Rate == null || op.Rate == undefined)
                    op.Rate = 0;

                if (op.Amount == null || op.Amount == undefined)
                    op.Amount = 0;

                $scope.newProduct.OpeningColl.push(op);
            };
        });

        if (!$scope.newProduct.PurchaseLedgerId)
            $scope.newProduct.PurchaseLedgerId = 0;

        if (!$scope.newProduct.SalesLedgerId)
            $scope.newProduct.SalesLedgerId = 0;


        $scope.newProduct.UserDefineFieldsColl = [];
        angular.forEach($scope.UDFFeildsColl, function (uf) {
            var uVal = {
                UDFId: uf.Id,
                Value: uf.Value,
                AlterNetValue: uf.Type == 2 ? uf.Value_TMP : uf.Value
            };

            if (uf.Type == 2 && uf.ValueDet)
                uVal.Value = $filter('date')(new Date(uf.ValueDet.dateAD), 'yyyy-MM-dd');

            $scope.newProduct.UserDefineFieldsColl.push(uVal);
        });

        $http({
            method: 'POST',
            url: base_url + "Inventory/Creation/SaveProduct",
            headers: { 'content-Type': undefined },

            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                if (data.productImg && data.productImg.length > 0)
                    formData.append("photo", data.productImg[0]);

                return formData;
            },
            data: { jsonData: $scope.newProduct, productImg: productPhoto, }
        }).then(function (res) {

            $scope.loadingstatus = "stop";
            hidePleaseWait();

            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearProduct();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.ChangeBaseUnit = function () {

        $scope.newProduct.BaseUnitName = '';
        if ($scope.newProduct.BaseUnitId && $scope.newProduct.BaseUnitId > 0) {
            var findUnit = mx($scope.UnitList).firstOrDefault(p1 => p1.UnitId == $scope.newProduct.BaseUnitId);
            if (findUnit)
            {
                $scope.newProduct.BaseUnitName = findUnit.Name;
                $scope.newProduct.QtyDecimal = findUnit.NoOfDecimalPlaces;
                $scope.newProduct.RateDecimal = findUnit.RateNoOfDecimalPlaces;
                $scope.newProduct.AmountDecimal = findUnit.AmountNoOfDecimalPlaces;
            }
        }
    }

    $scope.AddAlterNetUnit = function (det, ind) {
        if (det.BaseUnitValue > 0) {
            if ($scope.newProduct.AlterNetUnitColl) {
                if ($scope.newProduct.AlterNetUnitColl.length > ind + 1) {
                    $scope.newProduct.AlterNetUnitColl.splice(ind + 1, 0, {
                    })
                } else {
                    $scope.newProduct.AlterNetUnitColl.push({
                    });
                }
            }
        }

    };
    $scope.delAlterNetUnit = function (ind) {
        if ($scope.newProduct.AlterNetUnitColl) {
            if ($scope.newProduct.AlterNetUnitColl.length > 1) {
                $scope.newProduct.AlterNetUnitColl.splice(ind, 1);
            }
        }
    };

    $scope.AddCostRate = function (det, ind) {
        if (det.ApplicableFromDet && det.Rate > 0) {
            if ($scope.newProduct.CostRateColl) {
                if ($scope.newProduct.CostRateColl.length > ind + 1) {
                    $scope.newProduct.CostRateColl.splice(ind + 1, 0, {
                        RateOf: 1
                    })
                } else {
                    $scope.newProduct.CostRateColl.push({
                        RateOf: 1
                    });
                }
            }
        }

    };
    $scope.delCostRate = function (ind) {
        if ($scope.newProduct.CostRateColl) {
            if ($scope.newProduct.CostRateColl.length > 1) {
                $scope.newProduct.CostRateColl.splice(ind, 1);
            }
        }
    };



    $scope.AddSellingRate = function (det, ind) {
        if (det.ApplicableFromDet && det.Rate > 0) {
            if ($scope.newProduct.SellingRateColl) {
                if ($scope.newProduct.SellingRateColl.length > ind + 1) {
                    $scope.newProduct.SellingRateColl.splice(ind + 1, 0, {
                        RateOf: 1
                    })
                } else {
                    $scope.newProduct.SellingRateColl.push({
                        RateOf:1
                    });
                }
            }
        }

    };
    $scope.delSellingRate = function (ind) {
        if ($scope.newProduct.SellingRateColl) {
            if ($scope.newProduct.SellingRateColl.length > 1) {
                $scope.newProduct.SellingRateColl.splice(ind, 1);
            }
        }
    };

    $scope.AddTradeRate = function (det, ind) {
        if (det.ApplicableFromDet && det.Rate > 0) {
            if ($scope.newProduct.TradeRateColl) {
                if ($scope.newProduct.TradeRateColl.length > ind + 1) {
                    $scope.newProduct.TradeRateColl.splice(ind + 1, 0, {
                        RateOf: 1
                    })
                } else {
                    $scope.newProduct.TradeRateColl.push({
                        RateOf: 1
                    });
                }
            }
        }

    };
    $scope.delTradeRate = function (ind) {
        if ($scope.newProduct.TradeRateColl) {
            if ($scope.newProduct.TradeRateColl.length > 1) {
                $scope.newProduct.TradeRateColl.splice(ind, 1);
            }
        }
    };

    $scope.AddMRPRate = function (det, ind) {
        if (det.ApplicableFromDet && det.Rate > 0) {
            if ($scope.newProduct.MRPRateColl) {
                if ($scope.newProduct.MRPRateColl.length > ind + 1) {
                    $scope.newProduct.MRPRateColl.splice(ind + 1, 0, {
                        RateOf: 1
                    })
                } else {
                    $scope.newProduct.MRPRateColl.push({
                        RateOf: 1
                    });
                }
            }
        }

    };
    $scope.delMRPRate = function (ind) {
        if ($scope.newProduct.MRPRateColl) {
            if ($scope.newProduct.MRPRateColl.length > 1) {
                $scope.newProduct.MRPRateColl.splice(ind, 1);
            }
        }
    };

    $scope.AddOpening = function (det, ind) {
        if (det.GodownId > 0 && det.Amount > 0) {
            if ($scope.newProduct.OpeningColl) {
                if ($scope.newProduct.OpeningColl.length > ind + 1) {
                    $scope.newProduct.OpeningColl.splice(ind + 1, 0, {
                    })
                } else {
                    $scope.newProduct.OpeningColl.push({
                    });
                }
            }
        }

    };
    $scope.delOpening = function (ind) {
        if ($scope.newProduct.OpeningColl) {
            if ($scope.newProduct.OpeningColl.length > 1) {
                $scope.newProduct.OpeningColl.splice(ind, 1);
            }
        }
    };


    $scope.AddPreferedSupplier = function (det, ind) {
        if (det.PartyLedgerId > 0) {
            if ($scope.newProduct.PreferedSupplierColl) {
                if ($scope.newProduct.PreferedSupplierColl.length > ind + 1) {
                    $scope.newProduct.PreferedSupplierColl.splice(ind + 1, 0, {
                    })
                } else {
                    $scope.newProduct.PreferedSupplierColl.push({
                    });
                }
            }
        }

    };
    $scope.delPreferedSupplier = function (ind) {
        if ($scope.newProduct.PreferedSupplierColl) {
            if ($scope.newProduct.PreferedSupplierColl.length > 1) {
                $scope.newProduct.PreferedSupplierColl.splice(ind, 1);
            }
        }
    };


    $scope.ChangeOpeningQty = function (col, op) {
        if (col == 1 || col == 2) {
            op.Amount = op.Quantity * op.Rate;
        }
    }

    $scope.AddBlankRow = function () {
         

        if (!$scope.newProduct.PreferedSupplierColl || $scope.newProduct.PreferedSupplierColl.length == 0) {
            $scope.newProduct.PreferedSupplierColl = [];
            $scope.newProduct.PreferedSupplierColl.push({});
        }

        if (!$scope.newProduct.AlterNetUnitColl || $scope.newProduct.AlterNetUnitColl.length == 0) {
            $scope.newProduct.AlterNetUnitColl = [];
            $scope.newProduct.AlterNetUnitColl.push({});
        }
            

        if (!$scope.newProduct.CostRateColl || $scope.newProduct.CostRateColl.length == 0) {
            $scope.newProduct.CostRateColl = [];
            $scope.newProduct.CostRateColl.push({
                RateOf: 1
            });
        }
            

        if (!$scope.newProduct.SellingRateColl || $scope.newProduct.SellingRateColl.length == 0) {
            $scope.newProduct.SellingRateColl = [];
            $scope.newProduct.SellingRateColl.push({
                RateOf: 1
            });
        }
            

        if (!$scope.newProduct.OpeningColl || $scope.newProduct.OpeningColl.length == 0) {
            $scope.newProduct.OpeningColl = [];
            $scope.newProduct.OpeningColl.push({ 
            });
        }
            

        if (!$scope.newProduct.TradeRateColl || $scope.newProduct.TradeRateColl.length == 0) {
            $scope.newProduct.TradeRateColl = [];
            $scope.newProduct.TradeRateColl.push({
                RateOf: 1
            });
        }
            

        if (!$scope.newProduct.MRPRateColl || $scope.newProduct.MRPRateColl.length == 0) {
            $scope.newProduct.MRPRateColl = [];
            $scope.newProduct.MRPRateColl.push({
                RateOf: 1
            });
        }
            

    }

    $scope.GetProductById = function (refData) {

        $scope.loadingstatus = "running";
        showPleaseWait();

        $scope.ClearProduct();

        $timeout(function () {
            var para = {
                ProductId: refData.ProductId
            };

            $http({
                method: 'POST',
                url: base_url + "Inventory/Creation/GetProductById",
                dataType: "json",
                data: JSON.stringify(para)
            }).then(function (res) {
                hidePleaseWait();
                $scope.loadingstatus = "stop";
                if (res.data.IsSuccess && res.data.Data) {
                    var resData = res.data.Data;
                    $scope.newProduct = resData;
                    $scope.newProduct.Mode = 'Modify';

                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.newProduct.SankuchanCostCenterId = resData.SankuchanCostCenterId;
                        })
                    });


                    angular.forEach($scope.newProduct.TradeRateColl, function (tr) {
                        if (tr.ApplicableFrom)
                            tr.ApplicableFrom_TMP = new Date(tr.ApplicableFrom);
                    });

                    angular.forEach($scope.newProduct.MRPRateColl, function (tr) {
                        if (tr.ApplicableFrom)
                            tr.ApplicableFrom_TMP = new Date(tr.ApplicableFrom);
                    });

                    angular.forEach($scope.newProduct.CostRateColl, function (tr) {
                        if (tr.ApplicableFrom)
                            tr.ApplicableFrom_TMP = new Date(tr.ApplicableFrom);
                    });

                    angular.forEach($scope.newProduct.SellingRateColl, function (tr) {
                        if (tr.ApplicableFrom)
                            tr.ApplicableFrom_TMP = new Date(tr.ApplicableFrom);
                    });

                    angular.forEach($scope.newProduct.OpeningColl, function (tr) {
                        if (tr.MFGDate)
                            tr.MFGDate_TMP = new Date(tr.MFGDate);

                        if (tr.EXPDate)
                            tr.EXPDate_TMP = new Date(tr.EXPDate);
                    });
                    $scope.newProduct.PreferedSupplierColl = [];
                    angular.forEach($scope.newProduct.PreferedSupplierIdColl, function (ss) {
                        $scope.newProduct.PreferedSupplierColl.push({
                            PartyLedgerId: ss
                        });
                    });


                    $scope.AddBlankRow();

                    $scope.newProduct.QtyDecimal = 3;
                    $scope.newProduct.RateDecimal = 3;
                    $scope.newProduct.AmountDecimal = 2;

                    if ($scope.newProduct.BaseUnitId && $scope.newProduct.BaseUnitId > 0) {
                        var findUnit = mx($scope.UnitList).firstOrDefault(p1 => p1.UnitId == $scope.newProduct.BaseUnitId);
                        if (findUnit) {
                            $scope.newProduct.BaseUnitName = findUnit.Name;
                            $scope.newProduct.QtyDecimal = findUnit.NoOfDecimalPlaces;
                            $scope.newProduct.RateDecimal = findUnit.RateNoOfDecimalPlaces;
                            $scope.newProduct.AmountDecimal = findUnit.AmountNoOfDecimalPlaces;
                        }
                    }

                    var udfQry = mx($scope.newProduct.UserDefineFieldsColl);
                    angular.forEach($scope.UDFFeildsColl, function (uf) {
                        var findU = udfQry.firstOrDefault(p1 => p1.UDFId == uf.Id);
                        if (findU) {

                            if (uf.Type == 2) {
                                if (uf.Value) {
                                    uf.Value_TMP = new Date(uf.Value);
                                }

                            } else {
                                uf.Value = findU.Value;
                                uf.AlterNetValue = findU.Value;
                            }

                        }
                    });

                    $('#searVoucherRightBtn').modal('hide');

                } else {
                    Swal.fire(res.data.ResponseMSG);
                }

            }, function (reason) {
                Swal.fire('Failed' + reason);
            });
        });
        
     
    };

    $scope.DelProductById = function (refData) {

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
                    ProductId: refData.ProductId
                };

                $http({
                    method: 'POST',
                    url: base_url + "Inventory/Creation/DelProduct",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingstatus = "stop";
                    if (res.data.IsSuccess) {
                        $scope.SearchData();
                    } else {
                        Swal.fire(res.data.ResponseMSG);
                    }

                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
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
            url: base_url + "Inventory/Creation/GetProductLst",
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

        $timeout(function () {
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
                url: base_url + "Inventory/Creation/GetProductLst",
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
        });
        

    }


    $scope.updateVatRate = function () {
        if ($scope.newProduct.IsTaxable) {
            $scope.newProduct.VatRate = 13; // set default VAT if taxable
        } else {
            $scope.newProduct.VatRate = 0; // set 0 if not taxable
        }
    };
});