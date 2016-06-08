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
    /// Summary description for ConsultasAutocomplete
    /// </summary>
    [WebService(Namespace = "http://www.integrasoftware.com.mx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ConsultasAutocomplete : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] traePersonas(string prefixText, int count)
        {
            ConsultaAutocomplete consulta = new ConsultaAutocomplete();
            DataTable dt = new DataTable();

            dt = consulta.obtenerClientesDeAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%");

            List<string> txtElementos = new List<string>();

            int elementos = 0;

            foreach (DataRow row in dt.Rows)
            {
                string claveElementos = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtElementos.Add(claveElementos);
                if (++elementos == count)
                    break;
            }
            return txtElementos.ToArray();
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] ObtieneClientesEntradaPorDevolucion(string prefixText, int count)
        {

            ClBuscarDevolucion clBucDev = new ClBuscarDevolucion();
            DataTable dt = new DataTable();

            dt = clBucDev.sqlAutoCompleteClienteEntradaPorDevolucion(Convert.ToInt32(Session["Company"]), prefixText + "%");

            List<string> txtItems = new List<string>();
            int element = 0;//, index = 0;

            foreach (DataRow row in dt.Rows)
            {
                //dbValues = row["Nombre_Completo"].ToString();

                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);


                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] ObtieneProveedoresOrdendeCompra(string prefixText, int count)
        {

            ProviderBLL Proveedor = new ProviderBLL();
            DataTable dt = new DataTable();

            dt = Proveedor.sqlAutoCompleteProveedoresOrdenDeCompra(Convert.ToInt32(Session["Company"]), prefixText + "%");

            List<string> txtItems = new List<string>();
            int element = 0;//, index = 0;

            foreach (DataRow row in dt.Rows)
            {
                //dbValues = row["Nombre_Completo"].ToString();

                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);


                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] ObtieneTodoslosProveedores(string prefixText, int count)
        {

            ProviderBLL Proveedor = new ProviderBLL();
            DataTable dt = new DataTable();

            dt = Proveedor.ObtieneProveedoresAutocomplete(Convert.ToInt32(Session["Company"]), prefixText + "%");

            List<string> txtItems = new List<string>();

            ConsultaAutocomplete o = new ConsultaAutocomplete();
            List<ConsultaAutocomplete> lis = new List<ConsultaAutocomplete>();
            o.gfBandasDescuento(0, "");
            lis.Add(o);

            int element = 0;//, index = 0;

            foreach (DataRow row in dt.Rows)
            {
                //dbValues = row["Nombre_Completo"].ToString();

                string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Nombre"].ToString(), row["Numero"].ToString());
                txtItems.Add(ItemKey);


                if (++element == count)
                    break;
            }

            return txtItems.ToArray();
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] gfObtieneCuentasContables(string prefixText, int count)
        {
            DataTable dt = new DataTable();
            List<string> txtItems = new List<string>();
            try
            {
                if (prefixText.Trim().Length > 0)
                {
                    dt = new ConsultaAutocomplete().gfCuentasContables(int.Parse(Convert.ToString(Session["Company"])), prefixText);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Descripcion"].ToString(), row["Numero"].ToString());
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
        public string[] gfObtieneBandas(string prefixText, int count)
        {
            DataTable dt = new DataTable();
            List<string> txtItems = new List<string>();
            try
            {
                if (prefixText.Trim().Length > 0)
                {
                    dt = new ConsultaAutocomplete().gfBandasDescuento(int.Parse(Convert.ToString(Session["Company"])), prefixText);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Descripcion"].ToString(), row["ID"].ToString());
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


        //AUTOCOMPLETE ENTRADAS DIRECTAS A ALMACEN
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] gfProductoAlmacen(string prefixText, int count)
        {
            DataTable dt = new DataTable();
            List<string> txtItems = new List<string>();
            try
            {
                if (prefixText.Trim().Length > 0)
                {
                    dt = new AlmacenBLL().gfProductosArticulosAlmacen(int.Parse(Convert.ToString(Session["Company"])), prefixText, Convert.ToInt32(Session["AlmacenSeleccionado"]));

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Descripcion"].ToString(), row["Clasificacion_Concepto"].ToString());
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
        public string[] gfObtieneNodos(string prefixText, int count)
        {
            DataTable dt = new DataTable();
            List<string> txtItems = new List<string>();
            try
            {
                if (prefixText.Trim().Length > 0)
                {
                    dt = new ConsultaAutocomplete().gfNodosXML(int.Parse(Convert.ToString(Session["Company"])), prefixText);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string ItemKey = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Descripcion"].ToString(), row["ID"].ToString());
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

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
