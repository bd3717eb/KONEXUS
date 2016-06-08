using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using Integra_Develoment;
using System.Configuration;
namespace Integra_Develoment
{
    /// <summary>
    /// Summary description for ClientCuentasCobrarWebService
    /// </summary>
    [WebService(Namespace = "http://www.integrasoftware.com.mx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ClientCuentasCobrarWebService : System.Web.Services.WebService
    {
        /// <summary>
        ///Trae nombres de provedores para webForm Cuentas_Cobrar1
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] traeProvedor(string prefixText, int count)
        {
            //ConsultaAutocomplete consulta = new ConsultaAutocomplete();
            Control_Cuentas_Cobrar1 consulta = new Control_Cuentas_Cobrar1();

            DataTable dt = new DataTable();

            //dt = consulta.obtenerDatosDeProveedor(Convert.ToInt32(Session["Company"]), prefixText + "%");
            dt = consulta.obtenerDatosDeProveedor(Convert.ToInt32(Session["Company"]), prefixText + "");

            List<string> txtElementos = new List<string>();

            int elementos = 0;

            if (dt != null)
            {
                dt.Rows.Cast<System.Data.DataRow>().Take(11);

                foreach (DataRow row in dt.Rows)
                {
                    string claveElementos = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre_Completo"].ToString(), row["Numero"].ToString());
                    txtElementos.Add(claveElementos);
                    if (++elementos == count)
                        break;
                }
                return txtElementos.ToArray();
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
