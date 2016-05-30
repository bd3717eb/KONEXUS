using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using IntegraBussines;


namespace Integra_Develoment
{
    public partial class mpIntegraCompany : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string Module = string.Empty;
                    string[] asTemp = Page.AppRelativeVirtualPath.Split('/');
                    Module = asTemp[1];
                    // ==========snippet==============================================================
                    // Este  consulta las credenciales para la aplicación
                    //bool bflah = new SecurityController().lfAccess(asTemp[asTemp.Length - 1]);

                    bool bflah = true;
                    if (bflah)
                    {
                        if (HttpContext.Current.Session["sImgEmpresa"] != null)
                            lblImg.Text = HttpContext.Current.Session["sImgEmpresa"] as string;
                        else
                            lblImg.Text = ICompanyController.gfCompaniaPathImg(Convert.ToInt32(HttpContext.Current.Session["Company"]));

                        HdnTiempo.Value = ICompanyController.getTiempoTrancicion(Convert.ToInt32(HttpContext.Current.Session["Company"]));

                        if (HttpContext.Current.Session["scompanyuserinformation"] != null)
                        {
                            asTemp = (HttpContext.Current.Session["scompanyuserinformation"] as String).Split('|');
                            lblUser.Text = asTemp[0];
                            lblCompanyTittle.Text = asTemp[1];
                        }
                        else
                        {
                            lblUser.Text = ICompanyController.gfCompaniaUsuarioDatosPersonales(Convert.ToInt32(HttpContext.Current.Session["Company"]), Convert.ToInt32(HttpContext.Current.Session["Person"]));
                            lblCompanyTittle.Text = ICompanyController.gfCompaniaMensajesHtml(Convert.ToInt32(HttpContext.Current.Session["Company"]));
                        }


                        HdnTablasMensajes.Value = Convert.ToString(ICompanyController.getNumeroDeMensajes());
                        lblMenuHead.Text = lfMenuHeaderDO(Module);
                        CreaHTMLUnidadNegocio();

                    }
                    else
                    {
                        Response.Redirect("../MainAccesoDenegado.html");
                    }
                }
                catch (Exception ex)
                {
                    Label lblMsg = new Label();
                    LOG.gfLog("local", ex.Message, "", "mpIntegra.aspx", ref lblMsg);
                }
            }
        }

        protected void LogoutClick(object sender, EventArgs e)
        {
            LOG.gfLogAlta(HttpContext.Current.Session["Person"].ToString(), 0, "OFFLINE");
            HttpContext.Current.Session.Clear();
            Response.Redirect("~/login.aspx");
        }

        private DataTable obtieneUnidad()
        {

            DataTable dt = new DataTable();
            dt = IntegraBussines.ControlsBLL.GetOffices(Convert.ToInt32(Session["Company"]), Convert.ToInt32(Session["Person"]));

            return dt;
        }
        private void CreaHTMLUnidadNegocio()
        {
            DataTable dtUnidadNegocio = new DataTable();
            string sHTML = string.Empty;

            sHTML = "<select id='CPHIntegraCompany_dropUnidades' class='DropDown'>";

            dtUnidadNegocio = obtieneUnidad();
            if (dtUnidadNegocio.Rows.Count > 0)
            {
                foreach (DataRow row in dtUnidadNegocio.Rows)
                {
                    if (row[0].ToString() == HttpContext.Current.Session["Office"].ToString())
                        sHTML += "<option selected='selected' onclick=lfUnidadNegocio(" + row[0].ToString() + "); value=" + row[0].ToString() + ">" + row[1].ToString() + "</option>";
                    else
                        sHTML += "<option onclick=lfUnidadNegocio(" + row[0].ToString() + "); value=" + row[0].ToString() + ">" + row[1].ToString() + "</option>";
                }

            }

            sHTML += " </select> ";

            lblUnidadNegocio.Text = sHTML;
        }

        private string lfMenuHeaderDO(string sNameModule)
        {
            try
            {
                switch (sNameModule.ToUpper())
                {
                    case "VENTAS":
                        return string.Concat("<li><a href ='#'><i class='icon-home'></i>VENTAS</a></li>");
                    case "Contabilidad":
                        return string.Concat("<li><a href ='../Contabilidad/ReportePoliza.aspx'><i class='icon-home'></i>CONTABILIDAD</a></li>",
                                             "<li><a href ='../ContabilidadElectronica/ContabilidadElectronica.aspx'><i class='icon-home'></i>CONTABILIDAD ELECTRÓNICA</a></li>");
                    case "7":
                        return string.Concat("<li><a href ='../CuentasPorCobrar/RegistroCuentasporCobrar.aspx'><i class='icon-home'></i>CUENTAS POR COBRAR</a></li>",
                                             "<li><a href ='../Gastos/Gastos.aspx'><i class='icon-home'></i>GASTOS</a></li>",
                                             "<li><a href ='../Tesoreria/PagarCxP_CxC_Archivo.aspx'><i class='icon-home'></i>BANCOS</a></li>");
                    case "8":
                        return string.Concat("<li><a href ='#'><i class='icon-home'></i>ALMACÉN </a></li>");

                    default:
                        break;
                }

                //switch (HttpContext.Current.Session["iModule"].ToString())
                //{
                //    case "1":
                //        return string.Concat("<li><a href ='#'><i class='icon-home'></i>VENTAS</a></li>");
                //    case "5":
                //        return string.Concat("<li><a href ='../Contabilidad/ReportePoliza.aspx'><i class='icon-home'></i>CONTABILIDAD</a></li>",
                //                             "<li><a href ='../ContabilidadElectronica/ContabilidadElectronica.aspx'><i class='icon-home'></i>CONTABILIDAD ELECTRÓNICA</a></li>");
                //    case "7":
                //        return string.Concat("<li><a href ='../CuentasPorCobrar/RegistroCuentasporCobrar.aspx'><i class='icon-home'></i>CUENTAS POR COBRAR</a></li>",
                //                             "<li><a href ='../Gastos/Gastos.aspx'><i class='icon-home'></i>GASTOS</a></li>",
                //                             "<li><a href ='../Tesoreria/PagarCxP_CxC_Archivo.aspx'><i class='icon-home'></i>BANCOS</a></li>");
                //    case "8":
                //        return string.Concat("<li><a href ='#'><i class='icon-home'></i>ALMACÉN </a></li>");

                //    default:
                //        break;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }
    }
}