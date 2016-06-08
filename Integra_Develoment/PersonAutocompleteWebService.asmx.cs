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
    /// Summary description for PersonAutocompleteWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PersonAutocompleteWebService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetPersons(string prefixText, int count)
        {

            DataTable dt = new DataTable();

            dt = PersonBLL.ObtienePersonas(Convert.ToInt32(Session["Company"]), prefixText + "%");
            List<string> txtItems = new List<string>();
            String dbValues;
            int element = 0;

            foreach (DataRow row in dt.Rows)
            {
                dbValues = row["Nombre_Completo"].ToString();
                dbValues = dbValues.ToLower();
                txtItems.Add(dbValues.ToUpper());

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]

        public string[] GetClients(string prefixText, int count)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();


            dt = ClientBLL.GetMultipleClientesAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%");


            List<string> txtItems = new List<string>();
            //String dbValues;
            int element = 0;//, index = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
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
