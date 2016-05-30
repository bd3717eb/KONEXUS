using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using IntegraBussines;

namespace Integra_Develoment
{
    /// <summary>
    /// Summary description for ClientAutocompleteWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ClientAutocompleteWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetClients(string prefixText, int count)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();

            dt = ClientBLL.GetClientsAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%");


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
        public string[] GetAcreedores(string prefixText, int count)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();

            dt = ClientBLL.GetAcreedoresAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%");

            List<string> txtItems = new List<string>();
            int element = 0;

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
        public string[] GetDeudores(string prefixText, int count)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();

            dt = ClientBLL.GetDeudoresAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%");

            List<string> txtItems = new List<string>();
            int element = 0;

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
        public string[] GetProveedores(string prefixText, int count)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();

            dt = ClientBLL.GetProveedoresAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%");

            List<string> txtItems = new List<string>();
            int element = 0;

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
        public string[] GetUsuario(string prefixText, int count)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();

            dt = ClientBLL.GetUsuarioAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%");


            List<string> txtItems = new List<string>();
            int element = 0;

            foreach (DataRow row in dt.Rows)
            {
                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero_Usuario"].ToString());
                txtItems.Add(ItemKey);

                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetCXPProveedores(string prefixText, int count)
        {
            CustomerBLL ClientBLL = new CustomerBLL();
            DataTable dt = new DataTable();

            dt = ClientBLL.GetCXPProveedoresAutocomplete(Convert.ToInt32(Session["Company"]), Convert.ToInt32(Session["radioButtonOpcion"]), prefixText + "%");

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
        public string[] gfClientesObtenDetalles(string prefixText, int count)
        {
            DataTable dt = new DataTable();
            List<string> txtItems = new List<string>();
            try
            {
                if (prefixText.Trim().Length > 0)
                {
                    dt = new FacturaLibreController().gfClienteDetalle(int.Parse(Convert.ToString(Session["Company"])), prefixText);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                            txtItems.Add(ItemKey);
                        }
                    }
                }
                return txtItems.ToArray();
            }
            catch (Exception ex)
            {
                //Label lblMsg = new Label();
                //LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), "ClientAutocompleteWebService.asmx", ref lblMsg);
                return txtItems.ToArray();
            }

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] ObtieneClientesDevoluciones(string prefixText, int count)
        {
            DataTable dt = new DataTable();
            List<string> txtItems = new List<string>();
            try
            {
                if (prefixText.Trim().Length > 0)
                {
                    dt = new ControlProveedorAcreedor().ObtenDatosProvedor(1, Convert.ToInt32(Session["Company"]), prefixText, Session["TipoPersona"].ToString(), Convert.ToInt32(Session["Tipo_Proveedor"].ToString()));

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre_Completo"].ToString(), row["Numero"].ToString());
                            txtItems.Add(ItemKey);
                        }
                    }
                }
                return txtItems.ToArray();
            }
            catch
            {
                //Label lblMsg = new Label();
                //LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), "ClientAutocompleteWebService.asmx", ref lblMsg);
                return txtItems.ToArray();
            }

        }
    }
}
