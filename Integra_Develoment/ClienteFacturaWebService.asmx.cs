using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using IntegraBussines;

namespace Integra_Develoment
{
    /// <summary>
    /// Summary description for ClienteFacturaWebService
    /// </summary>
    [WebService(Namespace = "http://www.integrasoftware.com.mx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ClienteFacturaWebService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetClients(string prefixText, int count)
        {
            List<string> txtItems = new List<string>();
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();


            dt = ClientBLL.GetClientsInvoiceAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%");

            if (dt.Rows.Count <= 0)
                return txtItems.ToArray();

            dt.Rows.Cast<DataRow>().Take(11);
            //String dbValues;
            int element = 0;//, index = 0;

            foreach (DataRow row in dt.Rows)
            {
                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] ObtieneClientesConFacturas(string prefixText, int count)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();


            dt = ClientBLL.GetClientesConFacturasAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%");

            List<string> txtItems = new List<string>();
            //String dbValues;
            int element = 0;//, index = 0;

            foreach (DataRow row in dt.Rows)
            {
                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] ObtieneClientesFactura(string prefixText, int count)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();


            dt = ClientBLL.GetClientesFacturaAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%");

            List<string> txtItems = new List<string>();
            //String dbValues;
            int element = 0;//, index = 0;

            foreach (DataRow row in dt.Rows)
            {
                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);

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
