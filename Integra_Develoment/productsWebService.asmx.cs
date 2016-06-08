using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IntegraBussines;

namespace Integra_Develoment
{
    /// <summary>
    /// Summary description for productsWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class productsWebService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetProducts(string prefixText, int count)
        {

            DataTable dt = new DataTable();

            dt = ControlsBLL.GetProducts(Convert.ToInt32(Session["Company"]), prefixText + "%", INTEGRA.Global.Product_type);


            List<string> txtItems = new List<string>();
            String dbValues;
            int element = 0;

            foreach (DataRow row in dt.Rows)
            {
                dbValues = row["Descripcion"].ToString();
                dbValues = dbValues.ToLower();
                txtItems.Add(dbValues.ToUpper());

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
