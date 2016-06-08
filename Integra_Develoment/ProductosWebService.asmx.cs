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
    /// Summary description for ProductosWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ProductosWebService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetProducts(string prefixText, int count, string contextKey)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();

            string fecha = contextKey;

            DateTime? fechaInicio = null;
            DateTime? fechaFin = null;
            if (contextKey != null)
            {
                fechaInicio = Convert.ToDateTime(contextKey.Remove(10, 10));
                fechaFin = Convert.ToDateTime(contextKey.Remove(0, 10));
            }
            //
            string empresa = Convert.ToString(Session["Company"]);
            dt = ClientBLL.GetProductosAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%").Copy();
            List<string> txtItems = new List<string>();

            int element = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString().Trim(), row["Numero"].ToString());
                    txtItems.Add(ItemKey);

                    if (++element == count)
                        break;
                }
            }
            return txtItems.ToArray();
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetProductosGeneral(string prefixText, int count)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();

            dt = ClientBLL.GetCXPProveedoresAutocomplete(Convert.ToInt32(Session["Company"]), Convert.ToInt32(Session["radioOpcion"]), prefixText + "%");

            List<string> txtItems = new List<string>();
            int element = 0;

            foreach (DataRow row in dt.Rows)
            {
                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre_Completo"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetProductoCodigoBarras(string prefixText, int count, string contextKey)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();

            string fecha = contextKey;

            DateTime? fechaInicio = null;
            DateTime? fechaFin = null;
            if (contextKey != null)
            {
                fechaInicio = Convert.ToDateTime(contextKey.Remove(10, 10));
                fechaFin = Convert.ToDateTime(contextKey.Remove(0, 10));
            }
            //
            string empresa = Convert.ToString(Session["Company"]);
            dt = ClientBLL.GetProductosAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%").Copy();
            List<string> txtItems = new List<string>();

            int element = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString().Trim(), row["Numero"].ToString());
                    txtItems.Add(ItemKey);

                    if (++element == count)
                        break;
                }
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
