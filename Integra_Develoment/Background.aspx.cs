using IntegraBussines;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Integra_Develoment
{
    public partial class Background : System.Web.UI.Page
    {

        private static readonly Dictionary<int, string> dmodules = new Dictionary<int, string>
          {
            { 1, "Ventas.aspx" },
              {5, "ReportePoliza.aspx" },
              {7, "PagarCxP_CxC_Archivo.aspx" },
              {8, "AlmacenEntradas.aspx" }
          };


        #region Web.Services

        [WebMethod]
        public static void gfSalirdelSistema()
        {

            try
            {
                int user = int.Parse(HttpContext.Current.Session["Person"].ToString());
                int company = int.Parse(HttpContext.Current.Session["Company"].ToString());
                int office = int.Parse(HttpContext.Current.Session["Office"].ToString());
                ControlsBLL.gfUserCheckOut(user, "", company, office);
                //Console.Write(HttpContext.Current.Session.Count.ToString());
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Response.Redirect("~/Default.aspx");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        [WebMethod]
        public static void CambiaValorUnidad(string valor)
        {
            try
            {
                HttpContext.Current.Session["Office"] = valor;
            }
            catch (Exception ex)
            {
            }
        }


        [WebMethod]
        public static string User_Validation(int user, string pwd, int company, int office)
        {
            string sResponse = string.Empty;
            try
            {
                sResponse = ControlsBLL.gfUserCheckExist(user, pwd, company, office).Rows[0][0].ToString();
                string[] asResponse = sResponse.Split('|');
                if (asResponse[0] == "1")
                {
                    sResponse = ICompanyController.gfCompaniaPathImg(company, asResponse[1]);
                    HttpContext.Current.Session["sImgEmpresa"] = sResponse;
                    HttpContext.Current.Session["Person"] = user;
                    HttpContext.Current.Session["Company"] = company;
                    HttpContext.Current.Session["Office"] = office;

                    SqlConnectionStringBuilder sDataBase = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["INTEGRAPRODUCCIONI"].ToString());
                    HttpContext.Current.Session["sDataBase"] = sDataBase["Database"].ToString();

                    return "200";
                }
                else
                    return "404";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "404";

            }
        }

        [WebMethod]
        public static string lfFillDropOffice(string idcompany, string idperson)
        {
            string shead = string.Empty, sbody = string.Empty;
            string ssubhead = "<ul class='dropdown-menu' role='menu' aria-labelledby='menu1'>";
            string ssubheadffoot = "</ul>";

            DataTable dtOffices = ControlsBLL.GetOffices(Convert.ToInt32(idcompany), Convert.ToInt32(idperson)).Copy();

            if (dtOffices.Rows.Count > 0)
            {
                for (int i = 0; i < dtOffices.Rows.Count; i++)
                {
                    if (i == 0)
                        shead = "<button iddropofficeselect='" + dtOffices.Rows[i][0].ToString() + "' class='btn btn-default dropdown- toggle' type='button' id='dropoffice' data-toggle='dropdown'> " + dtOffices.Rows[i][1].ToString() + " <span class='caret'></span> </button>";

                    sbody += " <li role='presentation'><a iddrop='" + dtOffices.Rows[i][0].ToString() + "' onclick='lfOfficeSetValue(this);' role='menuitem' tabindex='-1' href='#'>" + dtOffices.Rows[i][1].ToString() + "</a></li>";
                }

                HttpContext.Current.Session["Office"] = dtOffices.Rows[0][0];
                HttpContext.Current.Session["Company"] = idcompany;
                return string.Concat(shead + ssubhead + sbody + ssubheadffoot);

            }
            return string.Empty;
        }

        [WebMethod]
        public static string lfGetCompaniesxUser(string sidUser)
        {
            DataTable CompanyTable = new DataTable();
            try
            {
                using (CompanyTable = IntegraBussines.ControlsBLL.ObtieneEmpresa(Convert.ToInt32(sidUser)).Copy())
                {
                    if (CompanyTable.Rows.Count > 0)
                    {
                        HttpContext.Current.Session["sTipo_Factura_Recibo"] = CompanyTable;
                        // build html for companies on boostrap
                        // DropTool.FillDrop(companyDropDownList, CompanyTable, "Descripcion", "Numero");
                        string shead = string.Empty, sbody = string.Empty;
                        string ssubhead = "<ul class='dropdown-menu' role='menu' aria-labelledby='menu1'>";
                        string ssubheadffoot = "</ul>";

                        for (int i = 0; i < CompanyTable.Rows.Count; i++)
                        {
                            if (i == 0)
                                shead = "<button iddropcompanyselect='" + CompanyTable.Rows[i][0].ToString() + "' class='btn btn-default dropdown- toggle' type='button' id='dropcompany' data-toggle='dropdown'> " + CompanyTable.Rows[i][1].ToString() + " <span class='caret'></span> </button>";

                            sbody += " <li role='presentation'><a iddrop='" + CompanyTable.Rows[i][0].ToString() + "' onclick='lfCompanySetValue(this);'  role='menuitem' tabindex='-1' href='#'>" + CompanyTable.Rows[i][1].ToString() + "</a></li>";
                        }

                        HttpContext.Current.Session["Company"] = CompanyTable.Rows[0][0];
                        var sdropoffice = lfFillDropOffice(CompanyTable.Rows[0][0].ToString(), sidUser);
                        return string.Concat(shead, ssubhead, sbody, ssubheadffoot, "|", sdropoffice);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "";
            }
            return "";
        }

        [WebMethod]
        public static string lfsetIDModule(string idmodule)
        {
            try
            {
                HttpContext.Current.Session["iModule"] = idmodule;
                string sgetmoduledescription = dmodules[int.Parse(idmodule)];
                bool bflag = new SecurityController().lfAccess(sgetmoduledescription);

                if (bflag)
                    return "OK";
                else
                    return "NO";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "NO";
            }
        }

        [WebMethod]
        public static string lfgetCredentials()
        {
            string sResult = string.Empty;
            HttpContext.Current.Session["sauthorization_page"] = null;
            HttpContext.Current.Session["scompanyuserinformation"] = null;

            try
            {
                int icompany = Convert.ToInt32(HttpContext.Current.Session["Company"]);
                int iperson = Convert.ToInt32(HttpContext.Current.Session["Person"]);
                int ioffice = Convert.ToInt32(HttpContext.Current.Session["Office"]);

                sResult = string.Concat(ICompanyController.gfCompaniaUsuarioDatosPersonales(icompany, iperson), "|", ICompanyController.gfCompaniaMensajesHtml(icompany));

                HttpContext.Current.Session["scompanyuserinformation"] = sResult;
                HttpContext.Current.Session["iModule"] = -1;
                HttpContext.Current.Session["sauthorization_page"] = ICompanyController.gfauthorization_page(icompany, iperson);
                //HttpContext.Current.Session["sDataBase"] = "";
                sResult = string.Concat(sResult, "|OK");
                return sResult;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "-409";
            }
        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

    }


}