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
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetPhisicPersons(string prefixText, int count)
        {

            DataTable dt = new DataTable();

            dt = PersonBLL.ObtenPersonaPorNumero(Convert.ToInt32(Session["Company"]), prefixText + "%", "F");

            List<string> txtItems = new List<string>();
            String dbValues;
            int element = 0;

            foreach (DataRow row in dt.Rows)
            {
                //dbValues = row["Nombre_Completo"].ToString();                
                //txtItems.Add(dbValues.ToUpper());
                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetMoralPersons(string prefixText, int count)
        {
            DataTable dt = new DataTable();

            dt = PersonBLL.ObtenPersonaPorNumero(Convert.ToInt32(Session["Company"]), prefixText + "%", "M");


            List<string> txtItems = new List<string>();
            String dbValues;
            int element = 0;

            foreach (DataRow row in dt.Rows)
            {
                //dbValues = row["Nombre_Completo"].ToString();
                //txtItems.Add(dbValues.ToUpper());
                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] TraePersonasMorales(string prefixText, int count)
        {
            DataTable dt = new DataTable();

            dt = ConsultaAutocomplete.traePersonas(Convert.ToInt32(Session["Company"]), prefixText + "%", "M");


            List<string> txtItems = new List<string>();
            String dbValues;
            int element = 0;

            foreach (DataRow row in dt.Rows)
            {
                //dbValues = row["Nombre_Completo"].ToString();
                //txtItems.Add(dbValues.ToUpper());
                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] TraePersonasFisicas(string prefixText, int count)
        {

            DataTable dt = new DataTable();

            dt = ConsultaAutocomplete.traePersonas(Convert.ToInt32(Session["Company"]), prefixText + "%", "F");

            List<string> txtItems = new List<string>();
            String dbValues;
            int element = 0;

            foreach (DataRow row in dt.Rows)
            {
                //dbValues = row["Nombre_Completo"].ToString();                
                //txtItems.Add(dbValues.ToUpper());
                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }



        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] TraePersonasFisicasContactos(string prefixText, int count)
        {

            int iCliente = -1;
            //iCliente = Convert.ToInt32(Context);
            DataTable dt = new DataTable();

            dt = ConsultaAutocomplete.traeContactosCliente(Convert.ToInt32(Session["Company"]), iCliente, prefixText + "%");

            List<string> txtItems = new List<string>();
            String dbValues;
            int element = 0;

            foreach (DataRow row in dt.Rows)
            {
                //dbValues = row["Nombre_Completo"].ToString();                
                //txtItems.Add(dbValues.ToUpper());
                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }
    }
}
