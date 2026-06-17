using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace WebSMS
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            LiteralOtherLinks.Text = "";

            try
            {

                var qryStr = Request.QueryString;
                //var allKeys = qryStr.AllKeys;
                //if (allKeys.Contains("tranid") && allKeys.Contains("istransaction"))
                //{
                //    var tranId = Convert.ToInt32(qryStr.Get("tranid"));
                //    var istran= Convert.ToBoolean(qryStr.Get("istransaction"));

                //    if (tranId>0 && istran==true)
                //    {
                //        var entity =(Dynamic.BusinessEntity.Global.FormsEntity)Convert.ToInt32(qryStr.Get("entityid"));
                //        StringBuilder sb = new StringBuilder();
                //        var newQryStr = qryStr.ToString();
                //         sb.Append("<a href=\"ShowReport.aspx?"+ newQryStr+"&rs:Format=xlsx\" target=_self  onclick=\"document.execCommand('SaveAs',true,'file.xls');\">EXCEL</a> | ");

                //        LiteralOtherLinks.Text = sb.ToString();
                //    }
                //}

                StringBuilder sb = new StringBuilder();
                var newQryStr = qryStr.ToString();
                sb.Append("<a href=\"ShowReport.aspx?" + newQryStr + "&rs:Format=xls\" target=_self  onclick=\"document.execCommand('SaveAs',true,'file.xls');\">EXCEL</a> | ");

                LiteralOtherLinks.Text = sb.ToString();


            }
            catch(Exception ee)
            {

            }
            
        }
    }
}