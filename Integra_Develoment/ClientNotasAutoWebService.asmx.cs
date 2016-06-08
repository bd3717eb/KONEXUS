using IntegraBussines;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Integra_Develoment
{
    /// <summary>
    /// Summary description for ClientNotasAutoWebService
    /// </summary>
    [WebService(Namespace = "http://www.integrasoftware.com.mx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ClientNotasAutoWebService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetClients(string prefixText, int count)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();


            dt = ClientBLL.GetClientsNoteAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%");

            List<string> txtItems = new List<string>();
            //String dbValues;
            int element = 0;//, index = 0;

            foreach (DataRow row in dt.Rows)
            {
                //dbValues = row["Nombre_Completo"].ToString();

                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);
                /*if (index > 0)
                {
                    if (dbValues.ToUpper() != txtItems[index - 1])
                    {
                        txtItems.Add(dbValues.ToUpper());
                        index++;
                    }
                }
                else
                {
                    txtItems.Add(dbValues.ToUpper());
                    index++;
                }*/

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetClientsNew(string prefixText, int count)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();


            dt = ClientBLL.GetNewClientsNoteAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%");

            List<string> txtItems = new List<string>();
            //String dbValues;
            int element = 0;//, index = 0;

            foreach (DataRow row in dt.Rows)
            {
                //dbValues = row["Nombre_Completo"].ToString();

                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);
                /*if (index > 0)
                {
                    if (dbValues.ToUpper() != txtItems[index - 1])
                    {
                        txtItems.Add(dbValues.ToUpper());
                        index++;
                    }
                }
                else
                {
                    txtItems.Add(dbValues.ToUpper());
                    index++;
                }*/

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetClientsNewInvoice(string prefixText, int count)
        {
            int icontabiliza = 1;
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();


            dt = ClientBLL.GetNewClientsInvoiceAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%", icontabiliza);

            List<string> txtItems = new List<string>();
            //String dbValues;
            int element = 0;//, index = 0;

            foreach (DataRow row in dt.Rows)
            {
                //dbValues = row["Nombre_Completo"].ToString();

                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);
                /*if (index > 0)
                {
                    if (dbValues.ToUpper() != txtItems[index - 1])
                    {
                        txtItems.Add(dbValues.ToUpper());
                        index++;
                    }
                }
                else
                {
                    txtItems.Add(dbValues.ToUpper());
                    index++;
                }*/

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] ObtieneClientesDevoluciones(string prefixText, int count)
        {

            ClBuscarDevolucion clBucDev = new ClBuscarDevolucion();
            DataTable dt = new DataTable();

            dt = clBucDev.sqlAutoCompleteCliente(Convert.ToInt32(Session["Company"]), prefixText + "%");

            List<string> txtItems = new List<string>();
            int element = 0;//, index = 0;

            foreach (DataRow row in dt.Rows)
            {
                //dbValues = row["Nombre_Completo"].ToString();

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
