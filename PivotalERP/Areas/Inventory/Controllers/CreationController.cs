using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dynamic.BusinessEntity.Global;
using Newtonsoft.Json;
using PivotalERP.Models;
using AcademicLib.BE.Global;
namespace PivotalERP.Areas.Inventory.Controllers
{
    public class CreationController : PivotalERP.Controllers.BaseController
    {
        #region "Unit"

        [PermissionsAttribute(Actions.View, (int)ENTITIES.Unit, false)]
        public ActionResult Unit()
        {
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.Unit, false)]
        public JsonNetResult SaveUnit()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Unit>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (beData.UnitId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.Unit(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.Unit(User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetUnit()
        {
            Dynamic.BusinessEntity.Inventory.UnitCollections dataColl = new Dynamic.BusinessEntity.Inventory.UnitCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.Unit(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region "Product Type"

        
        public ActionResult ProductType()
        {
            return View();
        }

        [HttpPost]        
        public JsonNetResult SaveProductType()
        {


            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.ProductType>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (beData.ProductTypeId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductType(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductType(User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetProductSupplier(int ProductId)
        {
            Dynamic.BusinessEntity.Account.LedgerCollections dataColl = new Dynamic.BusinessEntity.Account.LedgerCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.ProductDB(User.HostName, User.DBName).getPreferedSuppliers(User.UserId, ProductId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetAllProductType()
        {
            Dynamic.BusinessEntity.Inventory.ProductTypeCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductTypeCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductType(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        
        public JsonNetResult getProductTypeById(int ProductTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.ProductType(User.HostName, User.DBName).getProductTypeById(User.UserId, ProductTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        
        public JsonNetResult DeleteProductType(int ProductTypeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (ProductTypeId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Inventory.ProductType(User.HostName, User.DBName).DeleteById(ProductTypeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "Product Division"

        
        public ActionResult ProductDivision()
        {
            return View();
        }
        [HttpPost]
        
        public JsonNetResult SaveProductDivision()
        {


            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.ProductDivision>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (beData.ProductDivisionId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductDivision(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductDivision(User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetAllProductDivision()
        {
            Dynamic.BusinessEntity.Inventory.ProductDivisionCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductDivisionCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductDivision(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
                public JsonNetResult getProductDivisionById(int ProductDivisionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.ProductDivision(User.HostName, User.DBName).getProductDivisionById(User.UserId, ProductDivisionId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        
        public JsonNetResult DeleteProductDivision(int ProductDivisionId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (ProductDivisionId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Inventory.ProductDivision(User.HostName, User.DBName).DeleteById(ProductDivisionId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        
        public ActionResult Rack()
        {
            return View();
        }

        
        public JsonNetResult SaveRack()
        {

            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Rack>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (beData.RackId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.Rack(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.Rack(User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        
        public JsonNetResult getRackById(int RackId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.Rack(User.HostName, User.DBName).getRackById(User.UserId, RackId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetAllRack()
        {
            Dynamic.BusinessEntity.Inventory.RackCollections dataColl = new Dynamic.BusinessEntity.Inventory.RackCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.Rack(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        
        public JsonNetResult DeleteRack(int RackId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (RackId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Product Shape name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Inventory.Rack(User.HostName, User.DBName).DeleteById(RackId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #region "Product Brand"
        
        public ActionResult ProductBrand()
        {
            return View();
        }
        [HttpPost]
        
        public JsonNetResult SaveProductBrand()
        {


            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.ProductBrand>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (beData.ProductBrandId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductBrand(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductBrand(User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetAllProductBrand()
        {
            Dynamic.BusinessEntity.Inventory.ProductBrandCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductBrandCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductBrand(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        
        public JsonNetResult getProductBrandById(int ProductBrandId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.ProductBrand(User.HostName, User.DBName).getProductBrandById(User.UserId, ProductBrandId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        
        public JsonNetResult DeleteProductBrand(int ProductBrandId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (ProductBrandId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Inventory.ProductBrand(User.HostName, User.DBName).DeleteById(ProductBrandId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "ProductGroup"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.ProductGroup, false)]
        public ActionResult ProductGroup()
        {
            return View();
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.ProductGroup, false)]
        public JsonNetResult SaveProductGroup()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.ProductGroup>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (BranchWiseMaster)
                    {
                        if (beData.BDId == 0)
                            beData.BDId = User.BranchId;
                    }

                    if (beData.ProductGroupId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductGroup(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductGroup(User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetProductGroup()
        {
            Dynamic.BusinessEntity.Inventory.ProductGroupCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductGroupCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductGroup(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetAllProductGroup()
        {
            Dynamic.BusinessEntity.Inventory.ProductGroupCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductGroupCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductGroup(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetAllProductGroupList()
        {
            Dynamic.BusinessEntity.Inventory.ProductGroupCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductGroupCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductGroup(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };

        }
        [HttpPost]
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.ProductGroup, false)]
        public JsonNetResult getProductGroupById(int ProductGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.ProductGroup(User.HostName, User.DBName).getProductGroupById(User.UserId, ProductGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.ProductGroup, false)]
        public JsonNetResult DeleteProductGroup(int ProductGroupId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (ProductGroupId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Inventory.ProductGroup(User.HostName, User.DBName).DeleteById(ProductGroupId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "Product Categories"
        
        public ActionResult ProductCategories()
        {
            return View();
        }

        
        [HttpPost]
        public JsonNetResult SaveProductCategory()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.ProductCategories>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (beData.ProductCategoriesId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductCategories(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductCategories(User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetProductCategories()
        {
            Dynamic.BusinessEntity.Inventory.ProductCategoriesCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductCategoriesCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductCategories(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetAllProductCategories()
        {
            Dynamic.BusinessEntity.Inventory.ProductCategoriesCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductCategoriesCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductCategories(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        [HttpPost]
        
        public JsonNetResult getProductCategoriesById(int ProductCategoriesId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.ProductCategories(User.HostName, User.DBName).getProductCategoriesById(User.UserId, ProductCategoriesId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        
        public JsonNetResult deleteProductCategories(int ProductCategoriesId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (ProductCategoriesId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Inventory.ProductCategories(User.HostName, User.DBName).DeleteById(ProductCategoriesId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "Unit"

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetAllUnit()
        {
            Dynamic.BusinessEntity.Inventory.UnitCollections dataColl = new Dynamic.BusinessEntity.Inventory.UnitCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.Unit(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Unit, false)]

        public JsonNetResult getUnitById(int UnitId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.Unit(User.HostName, User.DBName).getUnitById(User.UserId, UnitId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.Unit, false)]
        public JsonNetResult deleteUnit(int UnitId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (UnitId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Inventory.Unit(User.HostName, User.DBName).DeleteById(UnitId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region"ProductCompany"
        
        public ActionResult ProductCompany()
        {
            return View();
        }
        [HttpPost]
        
        public JsonNetResult SaveProductCompany()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.ProductCompany>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (beData.ProductCompanyId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductCompany(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductCompany(User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetAllProductCompany()
        {
            Dynamic.BusinessEntity.Inventory.ProductCompanyCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductCompanyCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductCompany(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        
        public JsonNetResult getProductCompanyById(int ProductCompanyId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.ProductCompany(User.HostName, User.DBName).getProductCompanyById(User.UserId, ProductCompanyId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        
        public JsonNetResult deleteProductCompany(int ProductCompanyId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (ProductCompanyId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Debtor Creditor name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Inventory.ProductCompany(User.HostName, User.DBName).DeleteById(ProductCompanyId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        #endregion

        #region "Product"
        [PermissionsAttribute(Actions.View, (int)ENTITIES.Product, false)]
        public ActionResult Product()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetProductCode(string name, int? productGroupId)
        {
            var dataColl = new Dynamic.BusinessLogic.Inventory.Product(User.HostName, User.DBName).getProductCode(User.UserId, name, productGroupId);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetFixedProductConfig()
        {
            Dynamic.BusinessEntity.Setup.ServiceSettingForInventory dataColl = new Dynamic.BusinessEntity.Setup.ServiceSettingForInventory();
            try
            {
                dataColl = new Dynamic.DataAccess.Setup.ServiceSettingForInventory(User.HostName, User.DBName).getConfigurationForProductMaster(User.UserId);

                if (dataColl == null)
                    dataColl = new Dynamic.BusinessEntity.Setup.ServiceSettingForInventory();

                return new JsonNetResult() { Data = dataColl, TotalCount = 1, IsSuccess = true, ResponseMSG = GLOBALMSG.SUCCESS };
            }
            catch (Exception ee)
            {


            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = false, ResponseMSG = "Unable To Get Config" };
        }

        [HttpPost]
        public JsonNetResult GetProductById(int ProductId)
        {
            var productBL = new Dynamic.BusinessLogic.Inventory.Product(User.HostName, User.DBName);
            Dynamic.BusinessEntity.Inventory.Product dataColl = productBL.getProductById(User.UserId, ProductId);
            productBL.getProductDetailsByproductId(ref dataColl);

            return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.IsSuccess ? 1 : 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.Product, false)]
        public JsonNetResult DelProduct(int ProductId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.Product(User.HostName, User.DBName).DeleteById(ProductId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetProductListForPetrolPump()
        {
            Dynamic.ReportEntity.Inventory.ProductForAbbreviatedCollections dataColl = new Dynamic.ReportEntity.Inventory.ProductForAbbreviatedCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.ProductDB(User.HostName, User.DBName).getProductListForAbbreviatedInvoice(User.UserId, 4);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetProductListByType(int ProductType)
        {
            Dynamic.ReportEntity.Inventory.ProductForAbbreviatedCollections dataColl = new Dynamic.ReportEntity.Inventory.ProductForAbbreviatedCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.ProductDB(User.HostName, User.DBName).getProductListForAbbreviatedInvoice(User.UserId, ProductType);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetFixedProductList()
        {
            Dynamic.BusinessEntity.Inventory.ProductCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.ProductDB(User.HostName, User.DBName).getAllFixedProductShortDetails(User.UserId);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResult GetProductLst(TableFilter filter)
        {
            Dynamic.BusinessEntity.Common.ProductCollections dataColl = new Dynamic.BusinessEntity.Common.ProductCollections();
            try
            {
                filter.UserId = User.UserId;
                dataColl = new Dynamic.DataAccess.Common.ProductDB(User.HostName, User.DBName).getProductSearchList(filter);

                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.TotalRows, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = "", TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost, ValidateInput(false)]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.Product, false)]
        public JsonNetResult SaveProduct()
        {

            string photoLocation = "/Attachments/inventory/product";
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Product>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (Request.Files.Count > 0)
                    {
                        var filesColl = Request.Files;
                        var photo = filesColl["photo"];

                        if (photo != null)
                        {
                            var photoDoc = GetAttachmentDocuments(photoLocation, photo, true);
                            beData.PhotoPath = photoDoc.DocPath;
                        }
                    }

                    if (BranchWiseMaster)
                    {
                        if (beData.BDId == 0)
                            beData.BDId = User.BranchId;
                    }

                    bool isModify = beData.ProductId > 0;

                    if (beData.ProductId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.Product(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.Product(User.HostName, User.DBName).SaveFormData(beData);


                    if (resVal.IsSuccess)
                    {
                        Dynamic.BusinessEntity.Global.AuditLog auditLog = new AuditLog();
                        auditLog.TranId = (isModify ? beData.ProductId : resVal.RId);
                        auditLog.EntityId = Dynamic.BusinessEntity.Global.FormsEntity.Product;
                        auditLog.Action = (isModify ? Actions.Modify : Actions.Save);
                        auditLog.LogText = (isModify ? "Manual " + auditLog.EntityId.ToString() + " Modify " + beData.Name : "New " + auditLog.EntityId.ToString()) + beData.Name;
                        auditLog.AutoManualNo = IsNullStr(beData.Code);
                        SaveAuditLog(auditLog);
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion

        #region "ProductShape"

        
        public ActionResult ProductShape()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        
        public JsonNetResult SaveProductShape()
        {


            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.ProductShape>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (beData.ProductShapeId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductShape(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductShape(User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetProductShape()
        {
            Dynamic.BusinessEntity.Inventory.ProductShapeCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductShapeCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductShape(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        
        public JsonNetResult getProductShapeById(int ProductShapeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.ProductShape(User.HostName, User.DBName).getProductShapeById(User.UserId, ProductShapeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        
        public JsonNetResult DeleteProductShape(int ProductShapeId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (ProductShapeId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Product Shape name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Inventory.ProductShape(User.HostName, User.DBName).DeleteById(ProductShapeId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetAllProductShape()
        {
            Dynamic.BusinessEntity.Inventory.ProductShapeCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductShapeCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductShape(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region "ProductColor"
        
        public ActionResult ProductColor()
        {
            return View();
        }
        [HttpPost]
        
        public JsonNetResult SaveProductColor()
        {


            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.ProductColor>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (beData.ProductColorId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductColor(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductColor(User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpGet]
        public JsonNetResult GetProductColor()
        {
            Dynamic.BusinessEntity.Inventory.ProductColorCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductColorCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductColor(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        
        public JsonNetResult getProductColorById(int ProductColorId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.ProductColor(User.HostName, User.DBName).getProductColorById(User.UserId, ProductColorId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        
        public JsonNetResult DeleteProductColor(int ProductColorId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (ProductColorId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Product Color name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Inventory.ProductColor(User.HostName, User.DBName).DeleteById(ProductColorId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetAllProductColor()
        {
            Dynamic.BusinessEntity.Inventory.ProductColorCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductColorCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductColor(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        #region "ProductFlavour"
        
        public ActionResult ProductFlavour()
        {
            return View();
        }
        [HttpPost]
        
        public JsonNetResult SaveProductFlavour()
        {


            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.ProductFlavour>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (beData.ProductFlavourId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductFlavour(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.ProductFlavour(User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpGet]
        public JsonNetResult GetProductFlavour()
        {
            Dynamic.BusinessEntity.Inventory.ProductFlavourCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductFlavourCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductFlavour(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        [HttpPost]
        
        public JsonNetResult getProductFlavourById(int ProductFlavourId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.ProductFlavour(User.HostName, User.DBName).getProductFlavourById(User.UserId, ProductFlavourId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        
        public JsonNetResult DeleteProductFlavour(int ProductFlavourId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (ProductFlavourId < 0)
                {
                    resVal.ResponseMSG = "can't delete default ProductFlavour name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Inventory.ProductFlavour(User.HostName, User.DBName).DeleteById(ProductFlavourId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetAllProductFlavour()
        {
            Dynamic.BusinessEntity.Inventory.ProductFlavourCollections dataColl = new Dynamic.BusinessEntity.Inventory.ProductFlavourCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.ProductFlavour(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion


        #region "Godown"

        [HttpGet]
        public JsonNetResult GetUserWiseGodown()
        {
            Dynamic.BusinessEntity.Inventory.GodownCollections dataColl = new Dynamic.BusinessEntity.Inventory.GodownCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.GodownDB(User.HostName, User.DBName).getUserWiseGodown(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [PermissionsAttribute(Actions.View, (int)ENTITIES.Godown, false)]
        public ActionResult Godown()
        {
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Save, (int)ENTITIES.Godown, false)]
        public JsonNetResult SaveGodown()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.Godown>(Request["jsonData"]);
                if (beData != null)
                {
                    beData.CUserId = User.UserId;

                    if (beData.GodownId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.Godown(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.Godown(User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [HttpPost]
        [PermissionsAttribute(Actions.Delete, (int)ENTITIES.Godown, false)]
        public JsonNetResult DeleteGodown(int GodownId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (GodownId < 0)
                {
                    resVal.ResponseMSG = "can't delete default Godown name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Inventory.Godown(User.HostName, User.DBName).DeleteById(GodownId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        [PermissionsAttribute(Actions.Modify, (int)ENTITIES.Godown, false)]
        public JsonNetResult getGodownById(int GodownId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                resVal = new Dynamic.BusinessLogic.Inventory.Godown(User.HostName, User.DBName).getGodownById(User.UserId, GodownId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonNetResult GetAllGodown()
        {
            Dynamic.BusinessEntity.Inventory.GodownCollections dataColl = new Dynamic.BusinessEntity.Inventory.GodownCollections();
            try
            {
                dataColl = new Dynamic.BusinessLogic.Inventory.Godown(User.HostName, User.DBName).getAllAsList(User.UserId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }
        #endregion

        #region "PartyWise Product Rate"

        
        public ActionResult PartyWiseProductRate()
        {
            return View();
        }

        [HttpPost]
        
        public JsonNetResult SavePartyWiseProductRate()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.PartyWiseProductRateCollections>(Request["jsonData"]);
                if (beData != null && beData.Count > 0)
                {
                    resVal = new Dynamic.DataAccess.Inventory.ProductDB(User.HostName, User.DBName).SavePartyWiseProductRate(User.UserId, beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }
        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [HttpPost]
        public JsonNetResult GetPartyWiseProductRate(int LedgerId)
        {
            Dynamic.BusinessEntity.Inventory.PartyWiseProductRateCollections dataColl = new Dynamic.BusinessEntity.Inventory.PartyWiseProductRateCollections();
            try
            {
                dataColl = new Dynamic.DataAccess.Inventory.ProductDB(User.HostName, User.DBName).getPartyWiseProductRate(User.UserId, LedgerId);
                return new JsonNetResult() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;

            }
            return new JsonNetResult() { Data = null, TotalCount = 0, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }

        #endregion

        #region " BOM "

        
        public ActionResult BOM()
        {
            return View();
        }

        [HttpPost]
        
        public JsonNetResult SaveBOM()
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                var beData = DeserializeObject<Dynamic.BusinessEntity.Inventory.BOM>(Request["jsonData"]);
                if (beData != null)
                {
                    var usr = User;
                    beData.CreateBy = usr.UserId;
                    beData.CUserId = usr.UserId;
                    beData.ModifyBy = usr.UserId;

                    if (beData.TranId > 0)
                        resVal = new Dynamic.BusinessLogic.Inventory.BOM(User.HostName, User.DBName).ModifyFormData(beData);
                    else
                        resVal = new Dynamic.BusinessLogic.Inventory.BOM(User.HostName, User.DBName).SaveFormData(beData);
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        [HttpPost]
        public JsonNetResultWithEnum GetAllBOM(int? VoucherType, DateTime? VoucherDate)
        {
            Dynamic.BusinessEntity.Inventory.AllBOMCollections dataColl = new Dynamic.BusinessEntity.Inventory.AllBOMCollections();
            try
            {

                dataColl = new Dynamic.BusinessLogic.Inventory.BOM(User.HostName, User.DBName).getAllBOM(User.UserId, VoucherType, VoucherDate);
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return new JsonNetResultWithEnum() { Data = dataColl, TotalCount = dataColl.Count, IsSuccess = dataColl.IsSuccess, ResponseMSG = dataColl.ResponseMSG };
        }


        [HttpPost]
        public JsonNetResult GetBomById(int tranId)
        {
            Dynamic.BusinessEntity.Inventory.BOM beData = new Dynamic.BusinessEntity.Inventory.BOM();
            try
            {

                beData = new Dynamic.BusinessLogic.Inventory.BOM(User.HostName, User.DBName).getBOMById(User.UserId, tranId);
            }
            catch (Exception ee)
            {
                beData.IsSuccess = false;
                beData.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = beData, TotalCount = 1, IsSuccess = beData.IsSuccess, ResponseMSG = beData.ResponseMSG };
        }

        [HttpPost]
        
        public JsonNetResult DeleteBOM(int tranId)
        {
            ResponeValues resVal = new ResponeValues();
            try
            {
                if (tranId < 0)
                {
                    resVal.ResponseMSG = "can't delete default  name";
                    resVal.IsSuccess = false;
                }
                else
                    resVal = new Dynamic.BusinessLogic.Inventory.BOM(User.HostName, User.DBName).DeleteById(User.UserId, tranId);
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new JsonNetResult() { Data = resVal, TotalCount = 0, IsSuccess = resVal.IsSuccess, ResponseMSG = resVal.ResponseMSG };
        }

        #endregion
    }
}