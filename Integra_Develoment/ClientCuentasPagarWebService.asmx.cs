using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using IntegraBussines;
using Integra_Develoment.Ventas;


namespace Integra_Develoment
{
    /// <summary>
    /// Summary description for ClientCuentasPagarWebService
    /// </summary>
    [WebService(Namespace = "http://www.integrasoftware.com.mx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ClientCuentasPagarWebService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] obtProvedor(string prefixText, int count)
        {
            List<string> txtItems = new List<string>();
            ControlCuentasPagar cns = new ControlCuentasPagar();
            DataTable dt = new DataTable();

            dt = cns.obtenerProveedorDeAutocomplete(Convert.ToInt32(Session["radioButton"]), Convert.ToInt32(Session["Company"]), prefixText + "%");

            if (dt == null)
                return txtItems.ToArray();

            dt.Rows.Cast<DataRow>().Take(11);
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

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
