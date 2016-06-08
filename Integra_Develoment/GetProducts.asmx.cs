using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;

namespace Integra_Develoment
{
    /// <summary>
    /// Summary description for GetProducts
    /// </summary>
    [WebService(Namespace = "http://www.integrasoftware.com.mx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GetProducts : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetProduct(string prefixText, int count)
        {
            DataTable ProductsTable;
            List<string> txtItems = new List<string>();

            using (ProductsTable = VentasController.GetProducts(Convert.ToInt32(Session["Company"]), prefixText.ToUpper() + "%", 1, INTEGRA.Global.Product_type))
            {
                int element = 0;

                if (ProductsTable != null)
                {
                    if (ProductsTable.Rows.Count > 0)
                    {

                        foreach (DataRow row in ProductsTable.Rows)
                        {
                            string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Descripcion"].ToString(), row["Numero"].ToString().ToUpper());
                            txtItems.Add(item);

                            if (++element == count)
                                break;
                        }
                    }
                }
            }

            return txtItems.ToArray();
        }



        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string[] GetConceptosPorProducto(string prefixText, int count)
        {
            DataTable ProductsTable;
            List<string> txtItems = new List<string>();

            using (ProductsTable = VentasController.GetProductoCodigoBarras(Convert.ToInt32(Session["Company"]), prefixText.ToUpper() + "%"))
            {
                int element = 0;

                if (ProductsTable != null)
                {
                    if (ProductsTable.Rows.Count > 0)
                    {

                        foreach (DataRow row in ProductsTable.Rows)
                        {
                            string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Descripcion"].ToString(), row["Clasificacion_Concepto"].ToString().ToUpper());
                            txtItems.Add(item);

                            if (++element == count)
                                break;
                        }
                    }
                }
            }

            return txtItems.ToArray();
        }
    }
}
