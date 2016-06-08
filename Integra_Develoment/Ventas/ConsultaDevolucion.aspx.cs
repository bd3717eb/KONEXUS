using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IntegraBussines;
using INTEGRAReports;
using System.Web.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using IntegraData;

namespace Integra_Develoment.Ventas
{

    ///////////////////////////////////////////////PÁGINA QUE CONSULTA VENTAS PARA REALIZAR UNA DEVOLUCIÓN////////////////////////////////////////////
    public partial class ConsultaDevolucion : System.Web.UI.Page
    {

        #region Variables



        int intClasAlmacen;

        decimal cantidad;
        double dprecio;
        static int intPuerto;
        public static string srfc, scalle, scolonia, sdelegacion, sconcepto;
        decimal dSumaSubtotal = 0, dTotal = 0, dIva = 0;
        DateTime fechaDe, fechaHasta;
        DataTable TablaClasAlmacen;
        private string sServidor, sMail, sContraseña;
        private string _ProductsTable = "products";
        DataTable TablaCorreoCliente;
        private DataTable ProductsTable
        {
            get
            {
                if (ViewState[_ProductsTable] == null)
                    return new DataTable();
                return (DataTable)ViewState[_ProductsTable];
            }
            set
            {
                ViewState[_ProductsTable] = value;
            }
        }
        DataTable ClientTable = new DataTable();
        static DataTable dtTemp = new DataTable();
        private List<string> listaParametros;
        private string strMsg { get; set; }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {



                    txtNumeroDe.Attributes.Add("onkeypress", " ValidarCaracteres(this, 7);javascript:return ValidNum(event);");
                    txtNumeroDe.Attributes.Add("onkeyup", " ValidarCaracteres(this, 7);");
                    txtNumeroHasta.Attributes.Add("onkeypress", " ValidarCaracteres(this, 7);javascript:return ValidNum(event);");
                    txtNumeroHasta.Attributes.Add("onkeyup", " ValidarCaracteres(this, 7);");


                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(this.lnkGuardar);

                    if (Request.QueryString["Client"] != null && Request.QueryString["Client"].ToString() != "")
                    {
                        lblIdCliente.Text = Request.QueryString["Client"];
                    }
                    else
                    {
                        lblIdCliente.Text = "0";
                    }
                    HdnEmpresa.Value = Session["Company"].ToString();
                    HdnUsuario.Value = Session["Person"].ToString();

                    if (Session["Office"] != null)
                    {
                        HdnSucursal.Value = Session["Office"].ToString();
                    }

                    if (lblIdCliente.Text != "0")
                    {
                        obtenerNombrePersona(Convert.ToInt32(lblIdCliente.Text));
                    }

                    lfConfiguracionInicial(1);
                    txtFechaDe.Text = "";
                    txtFechaHasta.Text = "";
                    lblIdCliente.Visible = false;
                    lnkGuardar.Attributes.Add("onclick", "gfProceso();");

                }

                btnBuscar.Attributes.Add("onclick", "gfProceso();");
                UpdtDocumentos.Update();


            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
             this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }
        protected void grvDocumentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable tabla_temp = new DataTable();

                tabla_temp = (DataTable)Session["buscar"];
                GridViewTool.Bind(tabla_temp, grvDocumentos);
                grvDocumentos.PageIndex = e.NewPageIndex;
                UpdtDocumentos.Update();
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
            this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }
        protected void grvDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    e.Row.Cells[3].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                    e.Row.Cells[3].Attributes["onmouseout"] = "this.style.textDecoration='none';";
                    e.Row.Cells[3].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grvDocumentos, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[3].ToolTip = "Ver partidas";
                    e.Row.Cells[4].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                    e.Row.Cells[4].Attributes["onmouseout"] = "this.style.textDecoration='none';";
                    e.Row.Cells[4].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grvDocumentos, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[4].ToolTip = "Ver partidas";
                    e.Row.Cells[5].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                    e.Row.Cells[5].Attributes["onmouseout"] = "this.style.textDecoration='none';";
                    e.Row.Cells[5].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grvDocumentos, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[5].ToolTip = "Ver partidas";
                    e.Row.Cells[6].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                    e.Row.Cells[6].Attributes["onmouseout"] = "this.style.textDecoration='none';";
                    e.Row.Cells[6].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grvDocumentos, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[6].ToolTip = "Ver partidas";
                    e.Row.Cells[7].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                    e.Row.Cells[7].Attributes["onmouseout"] = "this.style.textDecoration='none';";
                    e.Row.Cells[7].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grvDocumentos, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[7].ToolTip = "Ver partidas";
                    e.Row.Cells[8].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                    e.Row.Cells[8].Attributes["onmouseout"] = "this.style.textDecoration='none';";
                    e.Row.Cells[8].Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grvDocumentos, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[8].ToolTip = "Ver partidas";

                }
            }
            catch (Exception ex)
            {

                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
              this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }

        }
        protected void grvDocumentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                HdnNumeroCotiza.Value = "0";
                HdnNumeroFactura.Value = "0";
                HdnNumero.Value = "0";
                int index = 0;
                decimal dCantidad = 0;

                DataTable dt = null;
                DataTable dtPrecioConIEPSProducto = new DataTable();
                GridViewRow row = grvDocumentos.SelectedRow;
                HdnNumeroFactura.Value = grvDocumentos.DataKeys[row.RowIndex].Values["NUMERO_FACTURA"].ToString();
                HdnNumero.Value = grvDocumentos.DataKeys[row.RowIndex].Values["NUMERO"].ToString();
                HdnNumeroCotiza.Value = grvDocumentos.DataKeys[row.RowIndex].Values["NUMERO_COTIZA"].ToString();
                lblIdCliente.Text = grvDocumentos.DataKeys[row.RowIndex].Values["NUMERO_CLIENTE"].ToString();
                lblNombreBeneficiario.Text = grvDocumentos.DataKeys[row.RowIndex].Values["CLIENTE"].ToString();
                lblIdBeneficiario.Text = grvDocumentos.DataKeys[row.RowIndex].Values["NUMERO_CLIENTE"].ToString();
                lblClasIva.Text = grvDocumentos.DataKeys[row.RowIndex].Values["Clas_IVA"].ToString();
                lblDesIva.Text = grvDocumentos.DataKeys[row.RowIndex].Values["Des_Iva"].ToString();
                lblTipoCambio.Text = grvDocumentos.DataKeys[row.RowIndex].Values["TIPO_CAMBIO"].ToString();
                lblFecha.Text = grvDocumentos.DataKeys[row.RowIndex].Values["FECHA"].ToString();

                lblFolioFiscal.Text = grvDocumentos.DataKeys[row.RowIndex].Values["Folio_Fiscal"].ToString();

                dt = DevolucionController.ObtieneDetalles(Convert.ToInt32(Session["Company"]), Convert.ToInt32(HdnNumero.Value));
                dtTemp = dt.Copy();


                //precio con ieps

                dtTemp.Columns.Add("PrecioConIEPS", typeof(decimal));
                dtTemp.Columns.Add("ValorIva", typeof(decimal));
                foreach (DataRow row1 in dtTemp.Rows)
                {


                    if ((dtPrecioConIEPSProducto = BandasBLL.ObtienePreciosConImpuestos(1, Convert.ToInt32(Session["Company"]),
                                                                     Convert.ToInt32(dtTemp.Rows[index][4]),
                                                                     Convert.ToInt32(dtTemp.Rows[index][6])))
                                                                     == null)
                    {
                        LabelTool.ShowLabel(lblError, "Error al consultar los precios!!",
                                                System.Drawing.Color.DarkRed);
                    }
                    else
                    {
                        if (dtPrecioConIEPSProducto.Rows.Count > 0)
                        {
                            if (dtPrecioConIEPSProducto.Rows[0]["PorcentajeIEPS"] != DBNull.Value)
                            {

                                dtTemp.Rows[index]["PrecioConIEPS"] = Convert.ToDecimal(dtTemp.Rows[index]["Precio"]) * (1 + (Convert.ToDecimal(dtPrecioConIEPSProducto.Rows[0]["PorcentajeIEPS"]) / 100));

                            }
                            else
                            {
                                dtTemp.Rows[index]["PrecioConIEPS"] = Convert.ToDecimal(dtTemp.Rows[index]["Precio"]);
                            }

                            if (dtPrecioConIEPSProducto.Rows[0]["PorcentajeIVA"] != DBNull.Value)
                            {

                                dtTemp.Rows[index]["ValorIva"] = dtPrecioConIEPSProducto.Rows[0]["PorcentajeIVA"];

                            }
                            else
                            {
                                dtTemp.Rows[index]["ValorIva"] = 0;
                            }


                        }
                        else
                        {
                            dtTemp.Rows[index]["PrecioConIEPS"] = Convert.ToDecimal(dtTemp.Rows[index]["Precio"]);
                            dtTemp.Rows[index]["ValorIva"] = 0;
                        }
                    }

                    index++;
                }

                if (dtTemp.Rows.Count > 0)
                {
                    GrvDetalles.DataSource = dtTemp;
                    GrvDetalles.DataBind();

                    decimal dCantidadProductos = 0;

                    foreach (GridViewRow row1 in GrvDetalles.Rows)
                    {
                        CheckBox chk = (CheckBox)row1.FindControl("chkPartida");
                        TextBox txtCantidad = (TextBox)GrvDetalles.Rows[row1.RowIndex].FindControl("txtCantidad");

                        dCantidadProductos = Convert.ToDecimal(txtCantidad.Text);
                        if (dCantidadProductos == 0)
                        {
                            chk.Enabled = false;
                            chk.Checked = false;
                            txtCantidad.Enabled = false;
                        }

                    }



                    llenaSucursalyAlmacen();
                    llenaDropsGridDetalle();
                    llenaFormaPago();
                    visibleControles(false);

                    if (drpFormaPago.SelectedValue != "")
                    {
                        if (Convert.ToInt32(drpFormaPago.SelectedValue) == -12)
                        {
                            llenaDropsDocumentosNC(Convert.ToInt32(HdnNumeroCotiza.Value));
                            visibleControles(true);
                        }

                        if (Convert.ToInt32(drpFormaPago.SelectedValue) == -9)
                        {
                            lblBanco.Visible = false;
                            drpBanco.Visible = false;
                            lblCuenta.Visible = false;
                            drpCuenta.Visible = false;
                            lblFolioBanco.Visible = false;
                            txtFolioBanco.Visible = false;
                            lblBeneficiario.Visible = false;
                            txtBeneficiario.Visible = false;
                            txtBeneficiario.Text = "";
                        }
                        else
                        {
                            llenaDropsDatosBanco();
                            txtBeneficiario.Text = lblNombreBeneficiario.Text;

                            lblBanco.Visible = true;
                            drpBanco.Visible = true;
                            lblCuenta.Visible = true;
                            drpCuenta.Visible = true;
                            lblFolioBanco.Visible = true;
                            txtFolioBanco.Visible = true;
                            lblBeneficiario.Visible = true;
                            txtBeneficiario.Visible = true;
                            txtBeneficiario.Text = "";
                            LblNbreBeneficiario.Text = "";
                        }
                    }
                    txtObservaciones.Text = "";
                    txtTotal.Text = "";
                    LabelTool.HideLabel(lblmsjerror);
                    mdlpartidas.Show();
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Sin productos pendientes para devolución", System.Drawing.Color.DarkRed);
                }




            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
             this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }
        protected void AllChecks(object sender, EventArgs e)
        {
            try
            {
                if (((CheckBox)sender).Checked)
                {
                    foreach (GridViewRow row in GrvDetalles.Rows)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("chkPartida");

                        if (chk != null && chk.Visible && chk.Enabled)
                        {
                            chk.Checked = true;

                        }

                    }
                }
                else
                {

                    foreach (GridViewRow row in GrvDetalles.Rows)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("chkPartida");

                        if (chk != null && chk.Visible && chk.Enabled)
                        {
                            chk.Checked = false;
                        }
                    }
                }
                obtenerTotal();
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
              this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }


        }
        protected void drptipoDocumento_Changed(object sender, EventArgs e)
        {
            try
            {
                InvoiceBLL Invoice = new InvoiceBLL();
                FillSerie();
                int intFolio = Invoice.GetFolio(Convert.ToInt32(Session["Company"]), Convert.ToInt32(drptipoDocumento.SelectedValue), Convert.ToInt32(drpdocSerie.SelectedValue));
                txtNumero.Text = intFolio.ToString();
                if (drpDocumento.SelectedValue == "-59")
                {
                    txtNumero.Enabled = false;
                }
                else
                {
                    txtNumero.Enabled = true;
                }

                mdlpartidas.Show();


            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
            this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }

        }

        protected void drpConceptoConta_Changed(object sender, EventArgs e)
        {
            llenaDropsGridDetalle();

        }
        protected void drpFormaPago_Changed(object sender, EventArgs e)
        {
            try
            {
                visibleControles(false);

                if (Convert.ToInt32(drpFormaPago.SelectedValue) == -12)
                {
                    llenaDropsDocumentosNC(Convert.ToInt32(HdnNumeroCotiza.Value));
                    visibleControles(true);
                }

                if (Convert.ToInt32(drpFormaPago.SelectedValue) == -9 || Convert.ToInt32(drpFormaPago.SelectedValue) == -12)//Nota de crédito o efectivo
                {
                    lblBanco.Visible = false;
                    drpBanco.Visible = false;
                    lblCuenta.Visible = false;
                    drpCuenta.Visible = false;
                    lblFolioBanco.Visible = false;
                    txtFolioBanco.Visible = false;
                    lblBeneficiario.Visible = false;
                    txtBeneficiario.Visible = false;
                    txtBeneficiario.Text = "";
                }
                else
                {
                    llenaDropsDatosBanco();
                    lblBanco.Visible = true;
                    drpBanco.Visible = true;
                    lblCuenta.Visible = true;
                    drpCuenta.Visible = true;
                    lblFolioBanco.Visible = true;
                    txtFolioBanco.Visible = true;
                    lblBeneficiario.Visible = true;
                    txtBeneficiario.Visible = true;
                    txtBeneficiario.Text = lblNombreBeneficiario.Text;
                }

            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
              this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }


        }
        protected void drpdocSerie_Changed(object sender, EventArgs e)
        {
            try
            {
                InvoiceBLL Invoice = new InvoiceBLL();
                int intFolio = Invoice.GetFolio(Convert.ToInt32(Session["Company"]), Convert.ToInt32(drptipoDocumento.SelectedValue), Convert.ToInt32(drpdocSerie.SelectedValue));
                txtNumero.Text = intFolio.ToString();
                if (drpDocumento.SelectedValue == "-59")
                {
                    txtNumero.Enabled = false;
                }
                else
                {
                    txtNumero.Enabled = true;
                }
                mdlpartidas.Show();

            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
            this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }

        }
        protected void drpBanco_Changed(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDatoCuenta = new DataTable();
                DataTable dtDatoFolio = new DataTable();

                dtDatoCuenta = DevolucionController.LLenaCuenta(Convert.ToInt32(Session["Company"]), Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(drpBanco.SelectedValue));

                if (dtDatoCuenta != null)
                {
                    DropTool.FillDrop(drpCuenta, dtDatoCuenta, "Descripcion", "Numero");
                }

                if (drpCuenta.SelectedValue != "")
                {
                    try
                    {
                        dtDatoFolio = DevolucionController.BancoSiguienteFolio(Convert.ToInt32(drpCuenta.SelectedValue), Convert.ToInt32(drpBanco.SelectedValue), Convert.ToInt32(Session["Company"]));
                        if (dtDatoFolio != null && dtDatoFolio.Rows.Count > 0)
                        {
                            txtFolioBanco.Text = dtDatoFolio.Rows[0][0].ToString();
                            txtFolioBanco.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        txtFolioBanco.Text = "";
                        txtFolioBanco.Enabled = true;

                    }

                }

            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
             this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);

            }

        }
        protected void drpCuenta_Changed(object sender, EventArgs e)
        {
            try
            {

                DataTable dtDatoFolio = new DataTable();

                if (drpCuenta.SelectedValue != "")
                {
                    try
                    {
                        dtDatoFolio = DevolucionController.BancoSiguienteFolio(Convert.ToInt32(drpCuenta.SelectedValue), Convert.ToInt32(drpBanco.SelectedValue), Convert.ToInt32(Session["Company"]));
                        if (dtDatoFolio != null && dtDatoFolio.Rows.Count > 0)
                        {
                            txtFolioBanco.Text = dtDatoFolio.Rows[0][0].ToString();
                            txtFolioBanco.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        txtFolioBanco.Text = "";
                        txtFolioBanco.Enabled = true;

                    }

                }
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
            this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);

            }

        }
        protected void BuscarClick(object sender, EventArgs e)
        {
            try
            {
                BuscarVenta();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);

            }

        }
        protected void LimpiarClick(object sender, EventArgs e)
        {
            try
            {
                txtNombreCliente.Text = "";
                lblCliente.Text = "";
                lblIdCliente.Text = "0";
                txtFechaDe.Text = "";
                txtFechaHasta.Text = "";
                LabelTool.HideLabel(lblError);
                drpDocumento.SelectedIndex = -1;
                drpSerie.SelectedIndex = -1;
                txtNumeroDe.Text = "";
                txtNumeroHasta.Text = "";
                GridViewTool.Bind(null, grvDocumentos);
                grvDocumentos.SelectedIndex = -1;
                lblFolioFiscal.Text = "";

            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
             this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void lnkRefresca_Click(object sender, EventArgs e)
        {
        }
        protected void lnkTerminaProceso_Click(object sender, EventArgs e)
        {
            mdlpartidas.Hide();
            GridViewTool.Bind(null, grvDocumentos);
            BuscarVenta();

        }
        protected void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (txtNombreCliente.Text.Trim().Length > Globales.Default)
                {
                    int iOpcion = 0;
                    try
                    {
                        string[] idClient = txtNombreCliente.Text.Split(' ');
                        lblIdCliente.Text = idClient[0];
                        iOpcion = 0;
                        obtenerNombrePersona(Convert.ToInt32(lblIdCliente.Text));
                        txtNombreCliente.Text = "";
                    }
                    catch
                    {
                        lblIdCliente.Text = "0";
                        iOpcion = 1;
                    }
                    lfConfiguracionInicial(iOpcion);
                    BuscarVenta();
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected void txtBeneficiario_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBeneficiario.Text.Trim().Length > Globales.Default)
                {

                    try
                    {
                        string[] idClient = txtBeneficiario.Text.Trim().Split('-');
                        lblIdBeneficiario.Text = idClient[0];
                        LblNbreBeneficiario.Text = idClient[1];


                    }
                    catch
                    {
                        lblIdBeneficiario.Text = "0";
                        LblNbreBeneficiario.Text = "";

                    }

                }
                txtBeneficiario.Text = "";
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                          this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }

        }
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {

            try
            {
                obtenerTotal();
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }

        }
        protected void drpDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtNumeroDocumentos = new DataTable();
                lfPopulate_serie();

            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                          this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }
        protected void GeneraDevolucion_Click(object sender, EventArgs e)
        {
            try
            {
                decimal dSaldo = 0;

                int intTipodevolucion = 0, intRegistroBanco = 0, intNumeroTransaccion = 0,
                     intNumeroNota = 0, intEstatus = 1, intnumeroDevolucion = 0;
                DataTable dtSaldo = new DataTable();
                DataTable dtSecuencia = new DataTable();
                DataTable dtRegistroBanco = new DataTable();
                string sMsg = string.Empty;
                ProductsTable = CreateProductsTable();

                HdnProcesoOk.Value = "0";
                HdnNumeroNota.Value = "0";

                if (ValidarCantidades())
                {
                    dtSaldo = DevolucionController.gfCuentasCobrarSaldo(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdCliente.Text),
                   Convert.ToInt32(HdnNumero.Value));

                    if (dtSaldo != null && dtSaldo.Rows.Count > 0 && dtSaldo.Rows[0][0].ToString() != "")
                    {
                        dSaldo = Convert.ToDecimal(dtSaldo.Rows[0][0]);
                    }

                    obtenerTotal();

                    if (Math.Round(dSaldo, 2) < Math.Round(Convert.ToDecimal(lblTotal.Text), 2))
                    {
                        intTipodevolucion = 1;///////sin factura origen
                        // !Importante: es factura origen si la cantidad total de la devolucion no excede el saldo* sino es sin factura origen
                        // preguntar ya que todas las que estan pagadas ya no tienen saldo y no estan afectando la factura no se muestra el descuento si eran tres articulos y regreso uno
                        // no se ve reflejado en la partida
                    }
                    if (Math.Round(dSaldo, 2) == Math.Round(Convert.ToDecimal(lblTotal.Text), 2))
                    {
                        intTipodevolucion = 0;
                    }

                    if (Convert.ToInt32(drpFormaPago.SelectedValue) != -12)  //-12 = Nota de Credito
                    {
                        intEstatus = Convert.ToInt32(DevolucionController.ObtenEstatusACancelar(Convert.ToInt32(Session["Company"]),
                                                                                              Convert.ToInt32(HdnNumero.Value)).Rows[0][0]);

                    }
                    //-1 'NO SE PUEDE CANCELAR POR QUE NO SE HA PAGADO' 
                    if (intEstatus > -1)// se puede realizar devolución
                    {
                        //////Proceso por  método de compensación

                        if (Convert.ToInt32(drpFormaPago.SelectedValue) == -12)////// FormaPagoCompensacion por NOTA DE CREDITO
                        {
                            intTipodevolucion = 1;//Se acordó que la Nota de crédito se generaría sin factura origen para no afectar el saldo de la factura si esta se encontraba pagada
                            intNumeroTransaccion = iConceptoDefinido();
                            if (intNumeroTransaccion > 0)
                            {
                                /////Proceso Devolucion y entrada al almacen
                                intnumeroDevolucion = procesoDevolucionyEntradaAlmacen(intTipodevolucion);
                                if (intnumeroDevolucion > 0)
                                {
                                    devolucionPorNotaCredito(intTipodevolucion, intnumeroDevolucion, intNumeroTransaccion);
                                }
                            }
                            else
                            {
                                LabelTool.ShowLabel(lblError, "No se encontró el registro para la contabilidad", System.Drawing.Color.DarkRed);
                            }
                        }
                        else if (Convert.ToInt32(drpFormaPago.SelectedValue) == -10 || Convert.ToInt32(drpFormaPago.SelectedValue) == -11) ////////////////// FormaPagoCompensacion CHEQUE Y TRANSFERENCIA
                        {
                            intNumeroTransaccion = iConceptoDefinido();
                            if (intNumeroTransaccion > 0)
                            {
                                intnumeroDevolucion = procesoDevolucionyEntradaAlmacen(intTipodevolucion);
                                if (intnumeroDevolucion > 0)
                                {
                                    int intTipoMovimiento = 1;
                                    intRegistroBanco = RegistroBancos(intTipoMovimiento, intNumeroTransaccion);

                                    HdnNumeroNota.Value = intnumeroDevolucion.ToString();
                                    intNumeroNota = Convert.ToInt32(HdnNumeroNota.Value);
                                    HdnProcesoOk.Value = intnumeroDevolucion.ToString();

                                    dtSecuencia = DevolucionController.gf_Devolucion_Guarda_Nota_Detalle_2(intnumeroDevolucion, Convert.ToInt32(Session["Company"]));//puse
                                    int inx = 0;
                                    int intcolumna = 0;
                                    foreach (DataRow row in dtSecuencia.Rows)
                                    {
                                        int iSecuencia = Convert.ToInt32(dtSecuencia.Rows[inx][0]);
                                        decimal dCantidad = Convert.ToDecimal(dtSecuencia.Rows[inx][3]);
                                        decimal dTotal = Convert.ToDecimal(dtSecuencia.Rows[inx][5]);
                                        int clasConcepto = Convert.ToInt32(dtSecuencia.Rows[inx][2]);

                                        inx++;
                                        FillProductsTable(ProductsTable, intnumeroDevolucion, intcolumna, dCantidad, clasConcepto);
                                        intcolumna++;
                                    }

                                    mdlpartidas.Hide();
                                    LabelTool.ShowLabel(lblError, "Devolución " + intnumeroDevolucion + " generada satisfactoriamente!!!", System.Drawing.Color.DarkRed);
                                    GridViewTool.Bind(null, grvDocumentos);
                                    BuscarVenta();
                                    grvDocumentos.SelectedIndex = -1;
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoImprime();", true);
                                }
                            }
                            else
                            {
                                LabelTool.ShowLabel(lblError, "No se encontró el registro para la contabilidad", System.Drawing.Color.DarkRed);
                            }
                        }
                        else      ////////////////////////////////////////// Forma pago compensacion efectivo
                        {

                            int intTipoMovimiento = 1;
                            intNumeroTransaccion = iConceptoDefinido();
                            if (intNumeroTransaccion > 0)
                            {
                                intnumeroDevolucion = procesoDevolucionyEntradaAlmacen(intTipodevolucion);
                                if (intnumeroDevolucion > 0)
                                {
                                    if (FormaEfectivo(intTipoMovimiento))
                                    {
                                        int iResultado = RegistroContabilidad(intTipoMovimiento, intNumeroTransaccion);//Realiza póliza contable

                                        if (iResultado == 1)
                                        {
                                            HdnNumeroNota.Value = intnumeroDevolucion.ToString();
                                            HdnProcesoOk.Value = intnumeroDevolucion.ToString();

                                            dtSecuencia = DevolucionController.gf_Devolucion_Guarda_Nota_Detalle_2(intnumeroDevolucion, Convert.ToInt32(Session["Company"]));//puse
                                            int inx = 0;
                                            int intcolumna = 0;
                                            foreach (DataRow row in dtSecuencia.Rows)
                                            {
                                                int iSecuencia = Convert.ToInt32(dtSecuencia.Rows[inx][0]);
                                                decimal dCantidad = Convert.ToDecimal(dtSecuencia.Rows[inx][3]);
                                                decimal dTotal = Convert.ToDecimal(dtSecuencia.Rows[inx][5]);
                                                int clasConcepto = Convert.ToInt32(dtSecuencia.Rows[inx][2]);
                                                inx++;
                                                FillProductsTable(ProductsTable, intnumeroDevolucion, intcolumna, dCantidad, clasConcepto);
                                                intcolumna++;
                                            }

                                            mdlpartidas.Hide();
                                            LabelTool.ShowLabel(lblError, "Devolución " + intnumeroDevolucion + " generada satisfactoriamente", System.Drawing.Color.DarkRed);
                                            GridViewTool.Bind(null, grvDocumentos);
                                            BuscarVenta();
                                            grvDocumentos.SelectedIndex = -1;
                                            HdnNumeroNota.Value = intnumeroDevolucion.ToString();
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoImprime();", true);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                LabelTool.ShowLabel(lblError, "No se encontró el registro para la contabilidad", System.Drawing.Color.DarkRed);
                            }

                        }

                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoRefresh();", true);


                    }
                    else
                    {

                        LabelTool.ShowLabel(lblError, "No se puede realizar la devolución por que no se encuentra pagada la venta", System.Drawing.Color.DarkRed);

                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                    }

                }

                else
                {
                    mdlpartidas.Show();

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                }


            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, ex.Message, System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);

            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoRefresh();", true);
            }
        }

        public void devolucionPorNotaCredito(int intTipodevolucion, int intnumeroDevolucion, int intNumeroTransaccion)
        {
            int intNumeroNota = 0;
            bool detalleNotaOk = false;
            DataTable dtSecuencia = new DataTable();

            intNumeroNota = EncabezadoNotaCredito(intTipodevolucion);
            if (intNumeroNota > 0)
            {
                HdnNumeroNota.Value = intNumeroNota.ToString();
                listaParametros = new List<string>();
                listaParametros.Add(intNumeroNota.ToString());
                listaParametros.Add(intnumeroDevolucion.ToString());
                listaParametros.Add(HttpContext.Current.Session["Company"].ToString());
                DevolucionController.gfi_Devolucion_Cliente_Actualiza(2, ref listaParametros);
                dtSecuencia = DevolucionController.gf_Devolucion_Guarda_Nota_Detalle_2(intnumeroDevolucion, Convert.ToInt32(Session["Company"]));
                int inx = 0;
                int intcolumna = 0;
                int intSecuenciaCotizacion = 0;


                foreach (GridViewRow rowD in GrvDetalles.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)rowD.FindControl("chkPartida");

                    if (chk != null && chk.Checked)
                    {
                        Label SecuenciaCotizacion = (Label)GrvDetalles.Rows[rowD.RowIndex].FindControl("lblSecuencia");
                        intSecuenciaCotizacion = Convert.ToInt32(SecuenciaCotizacion.Text);
                        int iSecuencia = Convert.ToInt32(dtSecuencia.Rows[inx][0]);
                        decimal dCantidad = Convert.ToDecimal(dtSecuencia.Rows[inx][3]);
                        decimal dTotal = Convert.ToDecimal(dtSecuencia.Rows[inx][5]);
                        int clasConcepto = Convert.ToInt32(dtSecuencia.Rows[inx][2]);
                        detalleNotaOk = Nota_Detalle(intNumeroNota, dCantidad, iSecuencia, dTotal, intSecuenciaCotizacion);
                        inx++;
                        FillProductsTable(ProductsTable, intnumeroDevolucion, intcolumna, dCantidad, clasConcepto);
                        intcolumna++;
                    }
                }


                if (detalleNotaOk)
                {
                    //proceso contabilidad
                    bool bConta = Contabilidad(intTipodevolucion, intnumeroDevolucion, intNumeroNota, intNumeroTransaccion);
                    if (bConta && Convert.ToInt32(drptipoDocumento.SelectedValue) == 33) // FormaPagoCompensacion por nota credito
                    {
                        HdnProcesoOk.Value = intnumeroDevolucion.ToString();

                        mdlpartidas.Hide();
                        GridViewTool.Bind(null, grvDocumentos);
                        BuscarVenta();
                        grvDocumentos.SelectedIndex = -1;
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoImprime();", true);
                        LabelTool.ShowLabel(lblError, "Devolución " + HdnProcesoOk.Value + " generada satisfactoriamente!!!", System.Drawing.Color.DarkRed);
                    }
                    else if (bConta && Convert.ToInt32(drptipoDocumento.SelectedValue) == -59) // FormaPagoCompensacion documento electronico
                    {
                        if (ProcesoDocumentoElectronico())
                        {

                            //Proceso documento electronico
                            FacturaServicio.Servicio fs = new FacturaServicio.Servicio();


                            string resultado = fs.gfs_generaComprobanteFisica(intNumeroNota, 1, INTEGRA.Global.DataBase,
                                                                             Convert.ToInt32(Session["Company"]),
                                                                             Convert.ToInt32(lblIdCliente.Text), "CONTACTOS");

                            string[] temp = resultado.Split('|');

                            string sMensaje = temp[0];
                            string sUUID = temp[1];
                            string sUbicacion = temp[2];
                            // xml pdf xmlatimbrar y lo copia a los 3 servidores
                            /*
                             strDestino48 = strDestino.Replace("VMS00001392-2", "D");
                             sResultado = string.Concat("sMensaje:", sMensaje, "|sUUID:", uuid, "|", "sPATH:", strDestino48);
                             */
                            if (sMensaje.Split(':')[1].ToUpper() == "OK")
                            {
                                string sTempFile = @WebConfigurationManager.AppSettings["sfolderRaiz"].ToString() + sUbicacion.Split(':')[2];
                                string sRes = CFDIProgram.gfComprimirFichero(sTempFile);
                                Session["ArchivoZip"] = sRes;
                                sTempFile = sTempFile.Replace(@"\", "/");
                                sTempFile = sTempFile.ToUpper();
                                sTempFile = sTempFile.Replace(@WebConfigurationManager.AppSettings["sfolderNameDestino2"].ToString(), @WebConfigurationManager.AppSettings["GMCFacturas"].ToString());
                                sTempFile = sTempFile.Replace(@WebConfigurationManager.AppSettings["sfolderNameDestino"].ToString(), @WebConfigurationManager.AppSettings["GMCFacturas"].ToString());
                                Session["Rutaelectronico"] = sTempFile;

                                LimpiarClick(null, null);
                                mdlpartidas.Hide();
                                LabelTool.ShowLabel(lblError, "Factura electrónica generada exitosamente", System.Drawing.Color.DarkRed);

                                BuscarVenta();
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoDescarga();", true);


                            }
                            else
                                throw new Exception("* Incidencia " + sMensaje.Split(':')[1]);

                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                        }
                    }
                    else
                    {
                        LabelTool.ShowLabel(lblError, "Error al generar la contabilidad!!!", System.Drawing.Color.DarkRed);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                    }
                }
            }
        }
        public int procesoDevolucionyEntradaAlmacen(int intTipodevolucion)
        {
            int intnumeroDevolucion = 0, index = 0, intdetalleok = 0, intSecuenciaDevolucion = 1;
            intnumeroDevolucion = DevolucionCliente(intTipodevolucion);
            if (intnumeroDevolucion > 0)
            {
                foreach (GridViewRow row in GrvDetalles.Rows)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)row.FindControl("chkPartida");
                    if (chk != null && chk.Checked)
                    {
                        intdetalleok = gf_Devolucion_Cliente_Detalle(intnumeroDevolucion, index);
                    }
                    index++;
                }

                index = 0;
                /////Proceso entrada almacen
                int intnumeroEntrada = EntradaAlmacen(intnumeroDevolucion);
                if (intnumeroEntrada > 0)
                {
                    //listaParametros.Clear();
                    listaParametros.Add(intnumeroDevolucion.ToString());
                    listaParametros.Add(HttpContext.Current.Session["Company"].ToString());
                    DevolucionController.gfi_Devolucion_Cliente_Actualiza(1, ref listaParametros);
                    int intpartida = 1;
                    foreach (GridViewRow row in GrvDetalles.Rows)
                    {
                        CheckBox chk = new CheckBox();
                        chk = (CheckBox)row.FindControl("chkPartida");

                        if (chk != null && chk.Checked)
                        {
                            if (chk != null && chk.Checked)
                            {
                                EntradaAlmacen_Detalle(intnumeroEntrada, intpartida, intnumeroDevolucion, index, intSecuenciaDevolucion);
                                intSecuenciaDevolucion++;
                            }
                        }
                        intpartida++;
                        index++;
                    }

                    ContabilidadEntradaAlmacen(intnumeroEntrada, Convert.ToInt32(drpAlmacen.SelectedValue), intnumeroDevolucion);
                }
            }
            return intnumeroDevolucion;
        }



        protected void Descarga_Click(object sender, EventArgs e)
        {
            string FileName = Convert.ToString(Session["ArchivoZip"]);

            if (FileName != null && FileName != "")
            {
                FileName = FileName.Replace(".pdf", ".zip");

                Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                //Le digo el fichero a descargar, en este caso en mi proyecto le cree una carpeta llamda pdf y dentro de esta un documento pdf llamado iis 7.0.pdf debe de ir con todo y extencion y no te olvides de las barras invertida que tiene este ejemplo, si no te funcion por una virguliña ~ antes de la primera barra ej. ~/pdf/iis 7.0.pdf
                //Response.WriteFile(FileName);
                //Le ago un bolcad al fichero.
                Response.Flush();
                //Ruta del archivo donde se encuenta, es igual al de arriba solo que este el el fichero en si, lo anerior es para tomar el nombre y ponerlo la ventana de descarga que aparece asi no tienes que escribirlo tu, o el susuario.
                Response.TransmitFile(FileName);
                //Le envio todo al encabezado.
                // Response.Redirect( Session["Rutaelectronico"]);
                Response.End();
            }
        }
        protected void PrintDocument(object sender, EventArgs e)
        {
            if (drptipoDocumento.SelectedValue == "-59")
            {
                Response.Clear();
                string FileName = Convert.ToString(Session["ArchivoZip"]);

                if (FileName != null && FileName != "")
                {
                    FileName = FileName.Replace(".pdf", ".zip");
                    //El header esto es opcional.
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                    //Le digo el fichero a descargar, en este caso en mi proyecto le cree una carpeta llamda pdf y dentro de esta un documento pdf llamado iis 7.0.pdf debe de ir con todo y extencion y no te olvides de las barras invertida que tiene este ejemplo, si no te funcion por una virguliña ~ antes de la primera barra ej. ~/pdf/iis 7.0.pdf
                    //Response.WriteFile(FileName);
                    //Le ago un bolcad al fichero.
                    Response.Flush();
                    //Ruta del archivo donde se encuenta, es igual al de arriba solo que este el el fichero en si, lo anerior es para tomar el nombre y ponerlo la ventana de descarga que aparece asi no tienes que escribirlo tu, o el susuario.
                    Response.TransmitFile(FileName);
                    //Le envio todo al encabezado.
                    // Response.Redirect( Session["Rutaelectronico"]);
                    Response.End();
                }
            }
            else
            {

                //ImprimeNotaVenta();
                // PrintSaleReportDescarga();
            }
        }

        protected void linkImprime_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnProcesoOk.Value != "")
                {
                    GetCustomerData();
                    GetAddressData();
                    printPDF(Convert.ToInt32(HdnProcesoOk.Value));
                    if (HdnNumeroNota.Value != "")
                    {
                        DataTable EncabezadoPDF = obtieneDatosNotaCredito(Convert.ToInt32(HdnNumeroNota.Value));
                        ReportDocument reporte = (Reports.GetReportDevolutions(Convert.ToString(Session["Company"]), EncabezadoPDF, ClientTable, ProductsTable));
                        reporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, this.Response, true,
                                                     Reports.GetReportName("Devolucion", HdnNumeroNota.Value).Replace(" ", ""));
                        reporte.Close();
                        reporte.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
            }

        }

        public void GeneraFacturaEnPopUp(string sRuta)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Window1", "<script> window.open('" + sRuta + "',''," +
            "'top=350,left=400,height=400,width=500,status=yes,toolbar=no,menubar=no,location=no resizable=yes');</script>", false);
        }

        protected void lnkDevolucionSinTransaccion_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtContabilidad = new DataTable();
                DataTable dtAlmacen = new DataTable();
                DataTable dtCtaCliente = new DataTable();
                DataTable dtNumeroTransaccion = new DataTable();

                int intMovimiento = 1, intNumeroPrograma = 289, intConcepto = 158;// concepto cambiar
                int intNumeroTransaccion = 0, intTransaccion = 0;
                string sCont_Clas_Cuenta = "";
                int intNumeroNota = 0;
                int intTipoNota = 0;
                int intEntradaConcepto = 0, intNotaConcepto = 0, intMovimientoBancarioConcepto = 0, intConceptoNota = 0;

                DataTable dtTransaccionContable = new DataTable();
                DataTable dtAnticipo = new DataTable();
                DataTable dtCliente = new DataTable();
                DataTable dtPrefijo = new DataTable();
                DataTable dtCtaBanco = new DataTable();

                int intnumeroAnticipo = 0;
                int intNumeroConcepto = 99;

                string sPrefijo = " ";

                if (lblMsjConcepto.Text == "No se encontró el registro para la contabilidad")
                {

                    if (validaTransaccion())
                    {
                        dtPrefijo = DevolucionController.ObtenPrefijoNota(Convert.ToInt32(Session["Company"]), intNumeroNota);
                        if (dtPrefijo != null && dtPrefijo.Rows.Count > 0)
                        {
                            sPrefijo = dtPrefijo.Rows[0][0].ToString();
                        }

                        string sConcepto = " ";
                        int intTipoPoliza = 121;
                        decimal? dCargo = null;
                        decimal? dSaldo = null;
                        int? intNumeroFactura = null;


                        string sDes_Poliza = HdnProcesoOk.Value + " " + LblNombreCorto.Text;
                        sConcepto = sPrefijo + (drptipoDocumento.SelectedItem.ToString()) + intNumeroNota + LblNombreCorto.Text.Trim() + "Devolución" + "F." + HdnNumeroFactura.Value;

                        dtAnticipo = DevolucionController.CreateAnticipo(intMovimiento, 0, Convert.ToInt32(Session["Company"]),
                                                                        Convert.ToInt32(lblIdCliente.Text), intNumeroConcepto, intNumeroNota, null,
                                                                        Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDecimal(txtTotal.Text), Convert.ToDecimal(txtTotal.Text),
                                                                        dCargo, dSaldo, intNumeroFactura, Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(drpMoneda.SelectedValue),
                                                                        null, null, 33, null, null, Convert.ToInt32(lblClasIva.Text), Convert.ToDecimal(lblTipoCambio.Text), null);


                        if (dtAnticipo != null && dtAnticipo.Rows.Count > 0)
                        {
                            intnumeroAnticipo = Convert.ToInt32(dtAnticipo.Rows[0][0]);

                            //return numeroAnticipo;
                        }

                        if (intnumeroAnticipo > 0)
                        {

                            string sAuxiliar = "";
                            dtAlmacen = DevolucionController.ObtenClasAlmacen(Convert.ToInt32(Session["Company"]), Convert.ToInt32(drpAlmacen.SelectedValue));
                            if (dtAlmacen != null)
                            {
                                sAuxiliar = sAuxiliar + dtAlmacen.Rows[0][0].ToString();


                            }
                            dtCtaCliente = DevolucionController.ObtenClasCliente(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdCliente.Text));
                            if (dtCtaCliente != null)
                            {
                                sAuxiliar = sAuxiliar + dtCtaCliente.Rows[0][0].ToString().PadLeft(11);
                            }

                            sAuxiliar = sAuxiliar + drpCuenta.SelectedValue.PadLeft(11);

                            //checar numero de transaccion
                            dtContabilidad = DevolucionController.CreateContabilidad(intMovimiento, Convert.ToInt32(Session["Company"]), intTransaccion, sAuxiliar, intTipoPoliza,
                                             Convert.ToDecimal(txtTotal.Text), Convert.ToInt32(HdnUsuario.Value), null, Convert.ToInt32(drpMoneda.SelectedValue), Convert.ToDateTime(DateTime.Now.ToShortDateString()), sConcepto,
                                             Convert.ToDecimal(lblTipoCambio.Text), null, null, lblFolioFiscal.Text.ToString());

                            if (dtContabilidad != null)
                            {
                                int numeroContabilidad = Convert.ToInt32(dtContabilidad.Rows[0][0]);
                                if (numeroContabilidad > 0)
                                {
                                    LabelTool.ShowLabel(lblError, "Cancelación satisfactoria", System.Drawing.Color.DarkRed);
                                    mdlTransaccion.Hide();
                                    mdlpartidas.Hide();
                                }
                            }

                            else
                            {
                                LabelTool.ShowLabel(lblError, "Error al realizar la contabilidad, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                                mdlTransaccion.Hide();
                                mdlpartidas.Hide();
                            }
                        }
                        else
                        {
                            LabelTool.ShowLabel(lblError, "Error al realizar a crear el anticipo, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                            mdlTransaccion.Hide();
                            mdlpartidas.Hide();
                        }

                    }
                    else
                    {
                        mdlTransaccion.Show();  //Este concepto no tiene afectación contable definida.  modal con mensaje para continuar si/no.
                    }
                }
                else
                {
                    dtPrefijo = DevolucionController.ObtenPrefijoNota(Convert.ToInt32(Session["Company"]), intNumeroNota);
                    if (dtPrefijo != null && dtPrefijo.Rows.Count > 0)
                    {
                        sPrefijo = dtPrefijo.Rows[0][0].ToString();
                    }

                    string sConcepto = " ";
                    int intTipoPoliza = 121;
                    decimal? dCargo = null;
                    decimal? dSaldo = null;
                    int? intNumeroFactura = null;

                    string sDes_Poliza = HdnProcesoOk.Value + " " + LblNombreCorto.Text;
                    sConcepto = sPrefijo + (drptipoDocumento.SelectedItem.ToString()) + intNumeroNota + LblNombreCorto.Text.Trim() + "Devolución" + "F." + HdnNumeroFactura.Value;

                    dtAnticipo = DevolucionController.CreateAnticipo(intMovimiento, 0, Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdCliente.Text),
                                                                     intNumeroConcepto, intNumeroNota, null, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDecimal(txtTotal.Text),
                                                                     Convert.ToDecimal(txtTotal.Text), dCargo, dSaldo, intNumeroFactura, Convert.ToInt32(HdnUsuario.Value),
                                                                     Convert.ToInt32(drpMoneda.SelectedValue), null, null, 33, null, null, Convert.ToInt32(lblClasIva.Text),
                                                                     Convert.ToDecimal(lblTipoCambio.Text), null);


                    if (dtAnticipo != null)
                    {
                        intnumeroAnticipo = Convert.ToInt32(dtAnticipo.Rows[0][0]);

                        //return numeroAnticipo;
                    }


                    if (intnumeroAnticipo > 0)
                    {

                        string sAuxiliar = "";
                        dtAlmacen = DevolucionController.ObtenClasAlmacen(Convert.ToInt32(Session["Company"]), Convert.ToInt32(drpAlmacen.SelectedValue));
                        if (dtAlmacen != null)
                        {
                            sAuxiliar = sAuxiliar + dtAlmacen.Rows[0][0].ToString();


                        }
                        dtCtaCliente = DevolucionController.ObtenClasCliente(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdCliente.Text));
                        if (dtCtaCliente != null)
                        {
                            sAuxiliar = sAuxiliar + dtCtaCliente.Rows[0][0].ToString().PadLeft(11);
                        }

                        sAuxiliar = sAuxiliar + drpCuenta.SelectedValue.PadLeft(11);

                        //checar numero de transaccion
                        dtContabilidad = DevolucionController.CreateContabilidad(intMovimiento, Convert.ToInt32(Session["Company"]), intTransaccion,
                                                                                 sAuxiliar, intTipoPoliza, Convert.ToDecimal(txtTotal.Text),
                                                                                 Convert.ToInt32(HdnUsuario.Value), null, Convert.ToInt32(drpMoneda.SelectedValue),
                                                                                 Convert.ToDateTime(DateTime.Now.ToShortDateString()), sConcepto, Convert.ToDecimal(lblTipoCambio.Text), null, null, lblFolioFiscal.Text.ToString());

                        if (dtContabilidad != null)
                        {
                            int numeroContabilidad = Convert.ToInt32(dtContabilidad.Rows[0][0]);
                            if (numeroContabilidad > 0)
                            {
                                LabelTool.ShowLabel(lblError, "Cancelación satisfactoria", System.Drawing.Color.DarkRed);
                                mdlTransaccion.Hide();
                                mdlpartidas.Hide();
                            }

                        }

                        else
                        {
                            LabelTool.ShowLabel(lblError, "Error al realizar la contabilidad, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                            mdlTransaccion.Hide();
                            mdlpartidas.Hide();
                        }
                    }
                    else
                    {
                        LabelTool.ShowLabel(lblError, "Error al realizar  el anticipo, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                        mdlTransaccion.Hide();
                        mdlpartidas.Hide();
                    }
                }

            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
            this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
                mdlTransaccion.Hide();
                mdlpartidas.Hide();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
            }

        }
        protected void cierraModalTransaccion_Click(object sender, EventArgs e)
        {
            mdlTransaccion.Hide();

        }
        protected bool validaTransaccion()
        {
            int intNumeroTransaccion = 0, intTransaccion = 0, intNumeroPrograma = 289;
            DataTable dtNumeroTransaccion = new DataTable();
            dtNumeroTransaccion = DevolucionController.ObtenNumeroTransaccionContable(Convert.ToInt32(Session["Company"]), intTransaccion, intNumeroPrograma);
            if (dtNumeroTransaccion != null && dtNumeroTransaccion.Rows.Count > 0)
            {
                intNumeroTransaccion = Convert.ToInt32(dtNumeroTransaccion.Rows[0][0]);
            }

            if (intNumeroTransaccion == 0)
            {
                lblMsjConcepto.Text = "Este concepto no tiene afectación contable definida";
                return false;
            }
            return true;

        }
        protected void NoContinua_Click(object sender, EventArgs e)
        {
            //preguntar que mas tiene que hacer al no continuar
            mdlpartidas.Hide();
            mdlTransaccion.Hide();
        }
        protected void chkPartidaChanged(object sender, EventArgs e)
        {
            try
            {
                obtenerTotal();

            }
            catch (Exception ex)
            {


            }



        }
        protected void imgpartidas_Click(object sender, ImageClickEventArgs e)
        {
            mdlpartidas.Hide();
        }
        protected void hfPerson_ValueChanged(object sender, EventArgs e)
        {

        }
        protected void drpNumeroDe_TextChanged(object sender, EventArgs e)
        {

        }
        protected void drpFechaHasta_TextChanged(object sender, EventArgs e)
        {

        }
        protected void linkBusquedaDevoluciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("BuscarDevoluciones.aspx");
        }

        #endregion

        #region Metodos/Propiedades

        private void visibleControles(bool a)
        {
            lbltipoDocumento.Visible = a;
            drptipoDocumento.Visible = a;
            lbldocSerie.Visible = a;
            drpdocSerie.Visible = a;
            lblNumero.Visible = a;
            txtNumero.Visible = a;
            lblConcepto.Visible = a;
            drpConcepto.Visible = a;
        }

        private void llenaDropsGridDetalle()
        {
            DataTable dtMotivo = new DataTable();

            int intfamilia = 7;
            if (drpConceptoConta.SelectedValue != "")
            {
                int intpadre = Convert.ToInt32(drpConceptoConta.SelectedValue);


                dtMotivo = DevolucionController.ObtieneCatalogo(Convert.ToInt32(Session["Company"]), intpadre, intfamilia);

                foreach (GridViewRow row in GrvDetalles.Rows)
                {
                    DropDownList motivo = (DropDownList)row.FindControl("MotivoDropDownList");

                    if (motivo != null)
                    {
                        DropTool.FillDrop(motivo, dtMotivo, "Descripcion", "Numero");
                    }

                }
            }

        }
        private void llenaDropsDatosBanco()
        {
            DataTable dtDatoBanco = new DataTable();
            DataTable dtDatoCuenta = new DataTable();
            DataTable dtDatoFolio = new DataTable();

            dtDatoBanco = DevolucionController.LLenaBanco(Convert.ToInt32(Session["Company"]), Convert.ToInt32(HdnUsuario.Value));

            if (dtDatoBanco != null)
            {
                DropTool.FillDrop(drpBanco, dtDatoBanco, "Descripcion", "Numero");
            }

            if (drpBanco.SelectedValue != "")
            {

                dtDatoCuenta = DevolucionController.LLenaCuenta(Convert.ToInt32(Session["Company"]), Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(drpBanco.SelectedValue));


                if (dtDatoCuenta != null)
                {
                    DropTool.FillDrop(drpCuenta, dtDatoCuenta, "Descripcion", "Numero");
                }
            }
            if (drpCuenta.SelectedValue != "")
            {
                try
                {
                    dtDatoFolio = DevolucionController.BancoSiguienteFolio(Convert.ToInt32(drpCuenta.SelectedValue), Convert.ToInt32(drpBanco.SelectedValue), Convert.ToInt32(Session["Company"]));
                    if (dtDatoFolio != null && dtDatoFolio.Rows.Count > 0)
                    {
                        txtFolioBanco.Text = dtDatoFolio.Rows[0][0].ToString();
                        txtFolioBanco.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    txtFolioBanco.Text = "";
                    txtFolioBanco.Enabled = true;

                }



            }

        }
        private void llenaFormaPago()
        {

            DataTable dtFormaPago = new DataTable();
            int intfamilia = 7;
            int intpadre = -8;

            dtFormaPago = DevolucionController.ObtieneCatalogo(Convert.ToInt32(Session["Company"]), intpadre, intfamilia);

            DataView dv = dtFormaPago.DefaultView;
            dv.Sort = "Descripcion asc";
            DataTable sortedDT = dv.ToTable();

            if (sortedDT != null)
            {
                DropTool.FillDrop(drpFormaPago, sortedDT, "Descripcion", "Numero");
            }
        }
        private void llenaSucursalyAlmacen()
        {

            DataTable dtSucursal = new DataTable();
            DataTable dtAlmacen = new DataTable();
            DataTable dtConcepto = new DataTable();
            int intfamilia = 7;
            int intpadre = 30;

            dtSucursal = DevolucionController.ObtieneSucursales(Convert.ToInt32(Session["Company"]), Convert.ToInt32(HdnUsuario.Value));
            dtAlmacen = DevolucionController.ObtieneCatalogo(Convert.ToInt32(Session["Company"]), intpadre, intfamilia);

            dtConcepto = DevolucionController.LLenaMotivosDevolucion(Convert.ToInt32(Session["Company"]));

            if (dtSucursal != null)
            {
                DropTool.FillDrop(drpSucursal, dtSucursal, "Descripcion", "Numero");
            }
            if (dtAlmacen != null)
            {
                DropTool.FillDrop(drpAlmacen, dtAlmacen, "Descripcion", "Numero");
            }

            if (dtConcepto != null)
            {
                DropTool.FillDrop(drpConceptoConta, dtConcepto, "Descripcion", "Numero");
            }



        }
        private void llenaDropsDocumentosNC(int intnumeroCotizacion)
        {
            DataTable dtConceptoNota = new DataTable();
            int intfamilia = 4;
            int intpadre = 102;
            InvoiceBLL Invoice = new InvoiceBLL();
            FillDocumento();
            FillSerie();
            int intFolio = Invoice.GetFolio(Convert.ToInt32(Session["Company"]), Convert.ToInt32(drptipoDocumento.SelectedValue), Convert.ToInt32(drpdocSerie.SelectedValue));
            txtNumero.Text = intFolio.ToString();

            if (drpDocumento.SelectedValue == "-59")
            {
                txtNumero.Enabled = false;
            }
            else
            {
                txtNumero.Enabled = true;
            }


            dtConceptoNota = DevolucionController.ObtieneCatalogo(Convert.ToInt32(Session["Company"]), intpadre, intfamilia);
            if (dtConceptoNota != null)
            {

                DataView dv = dtConceptoNota.DefaultView;
                dv.Sort = "Descripcion asc";
                DataTable dtConceptoNotaSort = dv.ToTable();


                DropTool.FillDrop(drpConcepto, dtConceptoNotaSort, "Descripcion", "Numero");
            }

        }
        private void FillDocumento()
        {
            DropTool.ClearDrop(drptipoDocumento);

            if (drpDocumento.SelectedItem != null)
                DropTool.FillDrop(drptipoDocumento, DevolucionController.ObtieneDocumentosDeNotaVenta(Convert.ToInt32(Session["Company"]),
                                  Convert.ToInt32(HdnUsuario.Value)), "Descripcion", "Numero");
        }
        private void FillSerie()
        {
            DropTool.ClearDrop(drpdocSerie);

            if (drpDocumento.SelectedItem != null)
                DropTool.FillDrop(drpdocSerie, DevolucionController.ObtieneSerieDeNotaVenta(Convert.ToInt32(Session["Company"]),
                                  Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(drptipoDocumento.SelectedValue)), "Descripcion", "Numero");
        }
        private void printPDF(int iNumeroDevolucion)
        {
            ClientTable = CreateClientTable();
            FillClientTable(ref ClientTable, iNumeroDevolucion);

        }
        private DataTable obtieneDatosNotaCredito(int intNumeroNota)
        {
            DataTable dtNotaCredito = CreateTablaNotaCredito();
            FillTableNotaCredito(ref dtNotaCredito, intNumeroNota);
            return dtNotaCredito;

        }
        private DataTable CreateProductsTable()
        {
            DataTable table;

            using (table = new DataTable())
            {
                table.Columns.Add("Cantidad");
                table.Columns.Add("Articulo");
                table.Columns.Add("Precio");
                table.Columns.Add("Subtotal");
                table.Columns.Add("IVA");
                table.Columns.Add("Total");
                table.Columns.Add("Fecha");
            }
            return table;
        }
        private void FillProductsTable(DataTable ProductsTable, int intNumeroDevolucion, int intColumna, decimal dCantidad, int iConcepto)
        {
            //DataRow ProductsRow = ProductsTable.NewRow();
            DataTable dt = new DataTable();
            DataTable dtPrecioConIEPSProducto = new DataTable();
            int iConceptoDet = 0;
            int index = 0;
            decimal dprecio = 0, dTotal = 0;
            dt = DevolucionController.ObtieneDetalles(Convert.ToInt32(Session["Company"]), Convert.ToInt32(HdnNumero.Value));

            foreach (DataRow row in dt.Rows)
            {
                if (dt.Rows[index][2].ToString() != "")
                {
                    iConceptoDet = Convert.ToInt32(dt.Rows[index][6]);
                    if (iConcepto == iConceptoDet)
                    {
                        DataRow ProductsRow = ProductsTable.NewRow();
                        sconcepto = Convert.ToString(dt.Rows[index][9]);



                        if ((dtPrecioConIEPSProducto = BandasBLL.ObtienePreciosConImpuestos(1, Convert.ToInt32(Session["Company"]),
                                                                Convert.ToInt32(dt.Rows[index]["Producto"]),
                                                                Convert.ToInt32(dt.Rows[index]["Concepto"])))
                                                                == null)
                        {
                            LabelTool.ShowLabel(lblError, "Error al consultar los precios!!",
                                                    System.Drawing.Color.DarkRed);
                        }
                        else
                        {
                            if (dtPrecioConIEPSProducto.Rows.Count > 0)
                            {
                                if (dtPrecioConIEPSProducto.Rows[0][0] != DBNull.Value)
                                {

                                    dt.Rows[index]["Precio"] = Convert.ToDecimal(dtPrecioConIEPSProducto.Rows[0][1]);
                                }
                            }
                        }
                        dprecio = Convert.ToDecimal(dt.Rows[index]["Precio"]);
                        dTotal = dprecio * dCantidad;

                        string dCandidadDec = dCantidad.ToString("F");
                        ProductsRow["Cantidad"] = Convert.ToString(dCandidadDec);
                        ProductsRow["Articulo"] = sconcepto;
                        ProductsRow["Precio"] = dprecio.ToString();
                        ProductsRow["Subtotal"] = dSumaSubtotal.ToString();
                        ProductsRow["IVA"] = dIva.ToString();
                        ProductsRow["Total"] = dTotal.ToString();

                        ProductsTable.Rows.Add(ProductsRow);
                    }



                }

                index++;
            }




        }
        private void FillClientTable(ref DataTable ClientTable, int intNumeroDevolucion)
        {
            DataRow ClientRow = ClientTable.NewRow();
            DateTime dFecha = Convert.ToDateTime(lblFecha.Text);
            CustomerBLL Customer = new CustomerBLL();
            DataTable CustomerTable;

            CustomerTable = Customer.GetPersonData(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdCliente.Text));
            if (CustomerTable != null && CustomerTable.Rows.Count > 0)
            {
                ClientRow["Nombre_Cliente"] = CustomerTable.Rows[0][2].ToString();
            }
            else
            {
                ClientRow["Nombre_Cliente"] = "";
            }




            ClientRow["RFC"] = srfc;
            ClientRow["Direccion"] = scalle + ", " + scolonia + ", " + sdelegacion;
            ClientRow["Fecha"] = dFecha;
            ClientRow["Numero_Devolucion"] = Convert.ToString(intNumeroDevolucion);
            ClientRow["Tipo_Docto"] = "Nota de Crédito";

            ClientTable.Rows.Add(ClientRow);
        }
        private void GetCustomerData()
        {
            CustomerBLL Customer = new CustomerBLL();
            DataTable CustomerTable;

            using (CustomerTable = Customer.GetPersonData(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdCliente.Text)))
            {
                if (CustomerTable != null && CustomerTable.Rows.Count > 0)
                {
                    SetCustomerData(CustomerTable);
                }
            }
        }
        private void SetCustomerData(DataTable CustomerTable)
        {
            srfc = CustomerTable.Rows[0][7].ToString();
        }
        private void GetAddressData()
        {
            DataTable AddressTable;
            int inttypeperson = 13;

            using (AddressTable = PersonBLL.ObtenDomicilioPersonaPorTipoDomicilio(Convert.ToInt32(lblIdCliente.Text), inttypeperson))
            {
                if (AddressTable != null && AddressTable.Rows.Count > 0)
                {
                    SetAddressData(AddressTable);
                }
            }
        }
        private void SetAddressData(DataTable AddressTable)
        {
            scalle = AddressTable.Rows[0][2].ToString();
            sdelegacion = AddressTable.Rows[0][3].ToString();
            scolonia = AddressTable.Rows[0][4].ToString();
        }
        private DataTable CreateClientTable()
        {
            DataTable table;

            using (table = new DataTable())
            {
                table.Columns.Add("Nombre_Cliente");
                table.Columns.Add("RFC");
                table.Columns.Add("Direccion");
                table.Columns.Add("Fecha");
                table.Columns.Add("Numero_Devolucion");
                table.Columns.Add("Tipo_Docto");

            }

            return table;
        }
        private DataTable CreateTablaNotaCredito()
        {
            DataTable table;

            using (table = new DataTable())
            {
                table.Columns.Add("Numero_Nota");
                table.Columns.Add("Fecha");
                table.Columns.Add("Subtotal");
                table.Columns.Add("Iva");
                table.Columns.Add("Total");
                table.Columns.Add("Comentario");
            }

            return table;
        }
        private void FillTableNotaCredito(ref DataTable dtNotaCredito, int iNumeroNota)
        {
            DataRow notaRow = dtNotaCredito.NewRow();
            DateTime dFecha = Convert.ToDateTime(lblFecha.Text);

            notaRow["Numero_Nota"] = iNumeroNota.ToString();
            notaRow["Fecha"] = lblFecha.Text;
            notaRow["Subtotal"] = lblSubtotal.Text;
            notaRow["Iva"] = lblIva.Text;
            notaRow["Total"] = lblTotal.Text;
            notaRow["Comentario"] = txtObservaciones.Text.Trim();

            dtNotaCredito.Rows.Add(notaRow);
        }
        private int DevolucionCliente(int intTipodevolucion)
        {
            try
            {
                DataTable dtDevolucion = new DataTable();
                DataTable dtConcepto = new DataTable();
                int intmovimiento = 1;
                int? intNumero = null, intNumeroFactura = null;
                int intConcepto = 158;
                int intTipoDocto = -4;//solicitud
                byte Relacionfactura = 1;
                string sFecha = string.Empty;
                sFecha = DateTime.Now.ToShortDateString();


                //foreach (GridViewRow row in GrvDetalles.Rows)
                //{
                //    DropDownList ddlMotivo = (DropDownList)row.FindControl("MotivoDropDownList");

                //    CheckBox chk = new CheckBox();
                //    chk = (CheckBox)row.FindControl("chkPartida");
                //    if (chk != null && chk.Checked)
                //    {
                //        if (ddlMotivo != null)
                //        {
                //            intConcepto = Convert.ToInt32(ddlMotivo.SelectedValue);
                //            break;
                //        }
                //    }


                //}
                if (drpConceptoConta.SelectedValue != "")
                {
                    intConcepto = Convert.ToInt32(drpConceptoConta.SelectedValue);
                }

                intNumeroFactura = Convert.ToInt32(HdnNumeroFactura.Value);
                //if (intTipodevolucion == 0)
                //{
                intNumero = Convert.ToInt32(HdnNumero.Value);

                // }


                dtDevolucion = DevolucionController.gf_Devolucion_Cliente(intmovimiento, Convert.ToInt32(Session["Company"]), 0, sFecha, 0,
                                                                          Convert.ToInt32(lblIdCliente.Text), intConcepto, Convert.ToInt32(drpAlmacen.SelectedValue), 0,
                                                                          Convert.ToInt32(drpMoneda.SelectedValue), intNumero, intNumeroFactura, Convert.ToInt32(lblClasIva.Text),
                                                                          Convert.ToDecimal(lblTipoCambio.Text), Convert.ToDecimal(lblSubtotal.Text), Convert.ToDecimal(lblIva.Text),
                                                                          Convert.ToDecimal(txtTotal.Text), intTipodevolucion, txtObservaciones.Text.Trim(), Convert.ToInt32(HdnUsuario.Value),
                                                                          7, 7, 9, 3, intTipoDocto, null, "", Relacionfactura, null, Convert.ToInt32(Session["Office"]));

                if (dtDevolucion != null)
                {
                    int intnumeroDevolucion = Convert.ToInt32(dtDevolucion.Rows[0][0]);

                    return intnumeroDevolucion;
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error al realizar la devolución, intente nuevamente!!!", System.Drawing.Color.DarkRed);

                    return 0;

                }

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error al realizar la devolución, intente nuevamente!!!", System.Drawing.Color.DarkRed);

                return 0;

            }


        }
        private int gf_Devolucion_Cliente_Detalle(int inumeroDevolucion, int intIndex)
        {
            try
            {
                DataTable dtDevolucion = new DataTable();
                int intmovimiento = 1;
                int intfamilia = 1;
                int intClas_Causa = 159;
                Label Precio = (Label)GrvDetalles.Rows[intIndex].FindControl("lblPrecio");
                Label Producto = (Label)GrvDetalles.Rows[intIndex].FindControl("lblProducto");
                Label Concepto = (Label)GrvDetalles.Rows[intIndex].FindControl("lblConcepto");

                TextBox Cantidad = (TextBox)GrvDetalles.Rows[intIndex].FindControl("txtCantidad");
                Label Costo = (Label)GrvDetalles.Rows[intIndex].FindControl("lblCosto");
                Label Secuencia = (Label)GrvDetalles.Rows[intIndex].FindControl("lblSecuencia");
                DropDownList causa = (DropDownList)GrvDetalles.Rows[intIndex].FindControl("MotivoDropDownList");
                if (causa.SelectedValue != "")
                {
                    intClas_Causa = Convert.ToInt32(causa.SelectedValue);
                }


                dtDevolucion = DevolucionController.gf_Devolucion_Cliente_Detalle(intmovimiento, Convert.ToInt32(Session["Company"]),
                               inumeroDevolucion, Convert.ToInt32(Secuencia.Text), Convert.ToInt32(Producto.Text), Convert.ToInt32(Concepto.Text), intfamilia,
                               Convert.ToDecimal(Cantidad.Text), Convert.ToDecimal(Precio.Text.Replace('$', ' ')), intClas_Causa, 7, Convert.ToDecimal(Costo.Text));
                if (dtDevolucion != null)
                {
                    int intnumeroDevolucion = Convert.ToInt32(dtDevolucion.Rows[0][0]);

                    return intnumeroDevolucion;
                }
                else
                {

                    LabelTool.ShowLabel(lblError, "Error al realizar la devolución, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                    return 0;
                }

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error al realizar la devolución, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                return 0;
            }

        }
        private int EntradaAlmacen(int intnumeroDevolucion)
        {
            try
            {
                DataTable dtEntradaAlmacen = new DataTable();
                int intmovimiento = 1;
                int inttipoentrada = 15;//solicitud
                int intTipoDoctoentrada = -4;//solicitud
                int? intprioridad = null;//

                dtEntradaAlmacen = DevolucionController.gf_Entrada_Almacen_Devolucion_Cliente(intmovimiento, Convert.ToInt32(Session["Company"]), 0, Convert.ToInt32(drpAlmacen.SelectedValue),
                                  DateTime.Now.ToShortDateString(), Convert.ToDateTime(DateTime.Now.ToShortDateString()), inttipoentrada, intnumeroDevolucion, Convert.ToInt32(HdnUsuario.Value), intprioridad, intTipoDoctoentrada,
                                  HdnNumeroFactura.Value, Convert.ToDateTime(lblFecha.Text), null, txtObservaciones.Text.Trim(), Convert.ToInt32(drpMoneda.SelectedValue), Convert.ToDecimal(lblTipoCambio.Text),
                                 null, null, Convert.ToInt32(lblIdCliente.Text), 0, null, intnumeroDevolucion);

                if (dtEntradaAlmacen != null)
                {
                    int intnumeroEntrada = Convert.ToInt32(dtEntradaAlmacen.Rows[0][0]);

                    return intnumeroEntrada;
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error al realizar la entrada a almacén, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                    return 0;
                }

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error al realizar la entrada a almacén, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                return 0;

            }

        }
        private bool EntradaAlmacen_Detalle(int intnumeroEntrada, int intpartida, int intnumeroDevolucion, int intIndex, int intSecuenciaDevolucion)
        {
            try
            {
                DataTable dtDevolucion = new DataTable();
                int intmovimiento = 1;

                int intUbicacion = 0;

                Label Precio = (Label)GrvDetalles.Rows[intIndex].FindControl("lblPrecio");
                Label Producto = (Label)GrvDetalles.Rows[intIndex].FindControl("lblProducto");
                Label Concepto = (Label)GrvDetalles.Rows[intIndex].FindControl("lblConcepto");
                Label TProducto = (Label)GrvDetalles.Rows[intIndex].FindControl("lblTProducto");
                Label TConcepto = (Label)GrvDetalles.Rows[intIndex].FindControl("lblTConcepto");
                Label Unidad = (Label)GrvDetalles.Rows[intIndex].FindControl("lblUnidad");
                Label Ubicacion = (Label)GrvDetalles.Rows[intIndex].FindControl("lblUbicacion");
                if (Ubicacion.Text != "")
                {
                    intUbicacion = Convert.ToInt32(Ubicacion.Text);
                }
                Label Familia = (Label)GrvDetalles.Rows[intIndex].FindControl("lblFamilia");
                TextBox Cantidad = (TextBox)GrvDetalles.Rows[intIndex].FindControl("txtCantidad");
                Label Costo = (Label)GrvDetalles.Rows[intIndex].FindControl("lblCosto");
                Label Secuencia = (Label)GrvDetalles.Rows[intIndex].FindControl("lblSecuencia");
                DropDownList causa = (DropDownList)GrvDetalles.Rows[intIndex].FindControl("MotivoDropDownList");

                dtDevolucion = DevolucionController.gf_Entrada_Almacen_Detalle_Devolucion(intmovimiento, Convert.ToInt32(Session["Company"]), intnumeroEntrada,
                               Convert.ToInt32(drpAlmacen.SelectedValue), intpartida, Convert.ToInt32(TProducto.Text), Convert.ToInt32(Producto.Text),
                               Convert.ToInt32(TConcepto.Text), Convert.ToInt32(Concepto.Text), Convert.ToDecimal(Cantidad.Text), Convert.ToInt32(Unidad.Text),
                               intUbicacion, 58, Convert.ToInt32(Familia.Text), null, Convert.ToDecimal(Precio.Text.Replace('$', ' ')),
                               Convert.ToInt32(drpMoneda.SelectedValue), Convert.ToDecimal(lblTipoCambio.Text), "", 0, null, null, null, intSecuenciaDevolucion, intnumeroDevolucion);
                if (dtDevolucion != null)
                {

                    return true;
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error al realizar entrada a almacén , intente nuevamente", System.Drawing.Color.DarkRed);
                    return false;
                }

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error al realizar entrada a almacén , intente nuevamente!!!", System.Drawing.Color.DarkRed);
                return false;

            }

        }

        protected bool GeneraContabilidadEntrada(int intAlmacen, int intEntrada)
        {
            bool bRespuesta = false;
            bRespuesta = AlmacenBLL.sp_Entrada_GeneraContabilidad(Convert.ToInt32(Session["Company"]), Convert.ToInt32(Session["Person"]), intAlmacen, intEntrada);
            return bRespuesta;


        }
        private int EncabezadoNotaCredito(int intTipodevolucion)
        {
            try
            {
                DataTable dtNotaCredito = new DataTable();
                int intmovimiento = 1, intContabilizada = 1;
                int? intrelacionfactura = null;//preguntar
                string sImporteLetra = "";
                toolsBLL importeletra = new toolsBLL();
                DataTable Importletra;
                Importletra = importeletra.import_to_string(Convert.ToInt32(Session["Company"]), Convert.ToDecimal(txtTotal.Text.Replace('$', ' ')), drpMoneda.SelectedItem.ToString(), 0);
                sImporteLetra = Convert.ToString(Importletra.Rows[0][0]);
                int? intNumeroFactura = null;

                if (intTipodevolucion == 0)
                {
                    intNumeroFactura = Convert.ToInt32(HdnNumeroFactura.Value);
                }

                dtNotaCredito = DevolucionController.CreaNotaEncabezado(intmovimiento, Convert.ToInt32(Session["Company"]), 0, Convert.ToInt32(lblIdCliente.Text), Convert.ToDateTime(DateTime.Now.ToShortDateString()), 0,
                                Convert.ToInt32(lblClasIva.Text), Convert.ToInt32(drptipoDocumento.SelectedValue), Convert.ToInt32(drpdocSerie.SelectedValue), Convert.ToInt32(txtNumero.Text),
                                Convert.ToInt32(drpMoneda.SelectedValue), Convert.ToInt32(drpConcepto.SelectedValue), txtObservaciones.Text.Trim(), Convert.ToDecimal(lblSubtotal.Text), 0,
                                Convert.ToDecimal(lblIva.Text), Convert.ToDecimal(txtTotal.Text), sImporteLetra, Convert.ToDecimal(lblTipoCambio.Text), Convert.ToInt32(lblIdCliente.Text),
                                 14, Convert.ToInt32(drptipoDocumento.SelectedValue), Convert.ToInt32(drpdocSerie.SelectedValue), Convert.ToInt32(HdnNumero.Value), intNumeroFactura, intTipodevolucion,
                                intNumeroFactura, null, null, null, Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(drpSucursal.SelectedValue), Convert.ToInt32(HdnUsuario.Value), intrelacionfactura, intContabilizada);

                // checar los numeros que se tienen que pasar

                if (dtNotaCredito != null)
                {
                    int intnumeroNotaCredito = Convert.ToInt32(dtNotaCredito.Rows[0][0]);

                    return intnumeroNotaCredito;
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error al realizar la nota de crédito, intente nuevamente!!!", System.Drawing.Color.DarkRed);

                    return 0;
                }


            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error al realizar la nota de crédito, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                return 0;

            }

        }
        private bool Nota_Detalle(int intnumeroNota, decimal dCantidad, int intSecuencia, decimal dTotal, int intSecuenciaCotizacion)
        {
            try
            {
                DataTable dtNotaDetalle = new DataTable();
                int intmovimiento = 1;



                dtNotaDetalle = DevolucionController.CreaNotaDetalle(intmovimiento, Convert.ToInt32(Session["Company"]), intnumeroNota, 0, dTotal,
                                Convert.ToInt32(HdnNumeroCotiza.Value), intSecuenciaCotizacion, dCantidad, intSecuencia);


                if (dtNotaDetalle != null)
                {
                    return true;
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error al realizar  la nota de crédito, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                    return false;
                }

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error al realizar la nota de crédito, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                return false;

            }


        }
        private bool Contabilidad(int intTipodevolucion, int intNumeroDevolucion, int intNumeroNota, int intTransaccion)
        {
            try
            {

                DataTable dtContabilidad = new DataTable();
                DataTable dtAnticipo = new DataTable();
                DataTable dtCliente = new DataTable();
                DataTable dtPrefijo = new DataTable();
                DataTable dtAlmacen = new DataTable();
                DataTable dtCtaCliente = new DataTable();
                DataTable dtCtaBanco = new DataTable();

                int intMovimiento = 1;
                int intnumeroAnticipo = 0;
                int intNumeroConcepto = 99;



                string sPrefijo = " ";

                dtPrefijo = DevolucionController.ObtenPrefijoNota(Convert.ToInt32(Session["Company"]), intNumeroNota);
                if (dtPrefijo != null && dtPrefijo.Rows.Count > 0)
                {
                    sPrefijo = dtPrefijo.Rows[0][0].ToString();
                }

                string sConcepto = " ";
                int intTipoPoliza = 121;
                decimal? dCargo = null;
                decimal? dSaldo = null;
                int? intNumeroFactura = null;

                string sDes_Poliza = intNumeroDevolucion + " " + LblNombreCorto.Text;
                sConcepto = sPrefijo + (drptipoDocumento.SelectedItem.ToString()) + intNumeroNota + LblNombreCorto.Text.Trim() + "Devolución" + "F." + HdnNumeroFactura.Value;

                dtAnticipo = DevolucionController.CreateAnticipo(intMovimiento, 0, Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdCliente.Text), intNumeroConcepto, intNumeroNota, null,
                             Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDecimal(txtTotal.Text), Convert.ToDecimal(txtTotal.Text), dCargo, dSaldo, intNumeroFactura, Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(drpMoneda.SelectedValue),
                             null, null, 33, null, null, Convert.ToInt32(lblClasIva.Text), Convert.ToDecimal(lblTipoCambio.Text), null);


                if (dtAnticipo != null)
                {
                    intnumeroAnticipo = Convert.ToInt32(dtAnticipo.Rows[0][0]);

                }

                if (intnumeroAnticipo > 0)
                {

                    string sAuxiliar = "";
                    dtAlmacen = DevolucionController.ObtenClasAlmacen(Convert.ToInt32(Session["Company"]), Convert.ToInt32(drpAlmacen.SelectedValue));
                    if (dtAlmacen != null)
                    {
                        sAuxiliar = sAuxiliar + dtAlmacen.Rows[0][0].ToString();


                    }
                    dtCtaCliente = DevolucionController.ObtenClasCliente(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdCliente.Text));
                    if (dtCtaCliente != null)
                    {
                        sAuxiliar = sAuxiliar + dtCtaCliente.Rows[0][0].ToString().PadLeft(11);
                    }

                    sAuxiliar = sAuxiliar + drpCuenta.SelectedValue.PadLeft(11);

                    //checar numero de transaccion
                    dtContabilidad = DevolucionController.CreateContabilidad(intMovimiento, Convert.ToInt32(Session["Company"]), intTransaccion, sAuxiliar, intTipoPoliza,
                                     Convert.ToDecimal(txtTotal.Text), Convert.ToInt32(HdnUsuario.Value), null, Convert.ToInt32(drpMoneda.SelectedValue), Convert.ToDateTime(DateTime.Now.ToShortDateString()), sConcepto,
                                     Convert.ToDecimal(lblTipoCambio.Text), null, null, lblFolioFiscal.Text.ToString());

                    if (dtContabilidad != null)
                    {


                        return true;
                    }

                    else
                    {
                        LabelTool.ShowLabel(lblError, "Error al realizar la contabilidad, intente nuevamente", System.Drawing.Color.DarkRed);

                        return false;
                    }
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error al realizar el anticipo, intente nuevamente", System.Drawing.Color.DarkRed);
                    return false;
                }

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error al realizar la contabilidad, intente nuevamente", System.Drawing.Color.DarkRed);
                return false;
            }


        }


        private int RegistroBancos(int intMovimiento, int intNumeroTransaccion1)//cheque, trasferencia bancaria
        {
            try
            {
                DataTable dtRegistro = new DataTable();
                DataTable dtNumeroTransaccion = new DataTable();
                DataTable dtNumeroTransaccion1 = new DataTable();
                DataTable dtContabilidad = new DataTable();
                DataTable dtAlmacen = new DataTable();
                DataTable dtCtaCliente = new DataTable();


                int intNumero = 0, intNumeroTransaccion = 0;
                string sAuxiliar = drpCuenta.SelectedItem.ToString();
                int intClas_Concepto = -24, intClas_Facultad = 0;// preguntar el concepto, pendiente Omar
                //739
                int? intRetiroChequeTransfer = null;
                decimal dSaldoInicial = 0, dSaldoFinal = 0;
                string sConcepto = " ";
                int intTipoPoliza = 121;
                int iRegistro1 = 0;


                dtNumeroTransaccion = DevolucionController.ObtenNumeroTransaccion2(Convert.ToInt32(Session["Company"]), intClas_Concepto);
                if (dtNumeroTransaccion.Rows.Count > 0)
                {
                    intNumeroTransaccion = Convert.ToInt32(dtNumeroTransaccion.Rows[0][0]);
                }
                // falta validar si esta definida la transaccion continuar si/no 

                dtRegistro = DevolucionController.RegistroBancos(intMovimiento, Convert.ToInt32(Session["Company"]), intNumero, Convert.ToInt32(drpBanco.SelectedValue),
                                                                drpCuenta.SelectedItem.ToString(), Convert.ToDateTime(DateTime.Now.ToShortDateString()), txtFolioBanco.Text.Trim(), Convert.ToDecimal(lblTotal.Text),
                                                                null, intClas_Concepto, Convert.ToInt32(lblIdBeneficiario.Text), "", intMovimiento, dSaldoInicial, dSaldoFinal,
                                                                Convert.ToInt32(drpCuenta.SelectedValue), intClas_Facultad, Convert.ToInt32(HdnUsuario.Value), intNumeroTransaccion,
                                                                sAuxiliar, Convert.ToInt32(HdnUsuario.Value), intRetiroChequeTransfer, Convert.ToDecimal(lblTipoCambio.Text),
                                                                null, txtObservaciones.Text.Trim());



                if (dtRegistro != null)
                {

                    iRegistro1 = Convert.ToInt32(dtRegistro.Rows[0][1]);
                }

                sConcepto = drpFormaPago.SelectedItem.ToString() + iRegistro1 + LblNombreCorto.Text.Trim() + "Devolución" + "F." + HdnNumeroFactura.Value;
                string sAuxiliarconta = "";
                dtAlmacen = DevolucionController.ObtenClasAlmacen(Convert.ToInt32(Session["Company"]), Convert.ToInt32(drpAlmacen.SelectedValue));
                if (dtAlmacen != null)
                {
                    sAuxiliarconta = sAuxiliarconta + dtAlmacen.Rows[0][0].ToString();


                }
                dtCtaCliente = DevolucionController.ObtenClasCliente(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdCliente.Text));
                if (dtCtaCliente != null)
                {
                    sAuxiliarconta = sAuxiliarconta + dtCtaCliente.Rows[0][0].ToString().PadLeft(11);
                }

                sAuxiliarconta = sAuxiliarconta + drpCuenta.SelectedValue.PadLeft(11);

                dtContabilidad = DevolucionController.CreateContabilidad(intMovimiento, Convert.ToInt32(Session["Company"]), intNumeroTransaccion1, sAuxiliarconta, intTipoPoliza,
                                    Convert.ToDecimal(txtTotal.Text), Convert.ToInt32(HdnUsuario.Value), null, Convert.ToInt32(drpMoneda.SelectedValue), Convert.ToDateTime(DateTime.Now.ToShortDateString()), sConcepto,
                                    Convert.ToDecimal(lblTipoCambio.Text), null, null, lblFolioFiscal.Text.ToString());

                if (dtRegistro != null)
                {

                    int iRegistro = Convert.ToInt32(dtRegistro.Rows[0][1]);
                    return iRegistro;
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error al realizar el registro de bancos , intente nuevamente", System.Drawing.Color.DarkRed);
                    return 0;
                }

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error al realizar el registro de bancos , intente nuevamente", System.Drawing.Color.DarkRed);
                return 0;
            }

        }


        private int RegistroContabilidad(int intMovimiento, int intNumeroTransaccion)//Solo contabilidad
        {
            try
            {

                DataTable dtNumeroTransaccion = new DataTable();
                DataTable dtNumeroTransaccion1 = new DataTable();
                DataTable dtContabilidad = new DataTable();
                DataTable dtAlmacen = new DataTable();
                DataTable dtCtaCliente = new DataTable();

                string sAuxiliar = drpCuenta.SelectedItem.ToString();
                // preguntar el concepto, pendiente 
                //739

                string sConcepto = " ";
                int intTipoPoliza = 121;
                int iRegistro1 = 0;


                sConcepto = drpFormaPago.SelectedItem.ToString() + iRegistro1 + LblNombreCorto.Text.Trim() + "Devolución" + "F." + HdnNumeroFactura.Value;
                string sAuxiliarconta = "";
                dtAlmacen = DevolucionController.ObtenClasAlmacen(Convert.ToInt32(Session["Company"]), Convert.ToInt32(drpAlmacen.SelectedValue));
                if (dtAlmacen != null)
                {
                    sAuxiliarconta = sAuxiliarconta + dtAlmacen.Rows[0][0].ToString();


                }
                dtCtaCliente = DevolucionController.ObtenClasCliente(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdCliente.Text));
                if (dtCtaCliente != null)
                {
                    sAuxiliarconta = sAuxiliarconta + dtCtaCliente.Rows[0][0].ToString().PadLeft(11);
                }

                sAuxiliarconta = sAuxiliarconta + drpCuenta.SelectedValue.PadLeft(11);

                dtContabilidad = DevolucionController.CreateContabilidad(intMovimiento, Convert.ToInt32(Session["Company"]), intNumeroTransaccion, sAuxiliarconta, intTipoPoliza,
                                    Convert.ToDecimal(txtTotal.Text), Convert.ToInt32(HdnUsuario.Value), null, Convert.ToInt32(drpMoneda.SelectedValue), Convert.ToDateTime(DateTime.Now.ToShortDateString()), sConcepto,
                                    Convert.ToDecimal(lblTipoCambio.Text), null, null, lblFolioFiscal.Text.ToString());




                return 1;

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error al realizar la contabilidad, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                return 0;
            }

        }

        private bool ContabilidadEntradaAlmacen(int intNumeroEntrada, int intAlmacenEntrada, int intNumeroDevolucion)
        {
            try
            {


                DataTable dtContabilidad = new DataTable();
                DataTable dtCosto = new DataTable();
                DataTable dtAuxiliar = new DataTable();
                int intNumeroTransaccion = iConceptoDefinido();
                int intMovimiento = 1, intNumeroPrograma = 9;
                int intTipoPoliza = 121;
                int intTransaccion = 0;
                decimal dCosto = 0;

                string sAuxiliar = "", sConcepto = "";


                dtCosto = AlmacenBLL.ObtieneCantidadCosto(Convert.ToInt32(Session["Company"]), intNumeroEntrada, intAlmacenEntrada);
                if (dtCosto != null && dtCosto.Rows.Count > 0)
                {
                    if (dtCosto.Rows[0][0] != DBNull.Value && dtCosto.Rows[0][0].ToString() != "")
                        dCosto = Convert.ToDecimal(dtCosto.Rows[0][0]);
                }

                dtAuxiliar = AlmacenBLL.ObtieneAlmacenAuxiliar(Convert.ToInt32(Session["Company"]), intAlmacenEntrada, 2);
                if (dtAuxiliar != null && dtAuxiliar.Rows.Count > 0)
                {
                    sAuxiliar = dtAuxiliar.Rows[0][0].ToString();
                }

                sConcepto = intNumeroEntrada.ToString() + ' ' + drpAlmacen.SelectedItem.Text.Trim() + ',' + " D.C " + intNumeroDevolucion;

                //checar numero de transaccion
                dtContabilidad = DevolucionController.CreateContabilidad(intMovimiento, Convert.ToInt32(Session["Company"]), intNumeroTransaccion, sAuxiliar, intTipoPoliza,
                                 dCosto, Convert.ToInt32(Session["Person"]), null, Convert.ToInt32(drpMoneda.SelectedValue), DateTime.Now, sConcepto,
                                 Convert.ToDecimal(lblTipoCambio.Text), null, null, null);

                if (dtContabilidad != null)
                {
                    if (AlmacenBLL.ActualizaContabilizada(Convert.ToInt32(Session["Company"]), intNumeroEntrada, intAlmacenEntrada))
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error al realizar la contabilidad, intente nuevamente!!!", System.Drawing.Color.DarkRed);

                    return false;
                }


            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error al realizar la contabilidad, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                return false;
            }


        }

        private bool FormaEfectivo(int intMovimiento)//efectivo
        {
            if (Convert.ToDecimal(lblTotal.Text.Replace('$', ' ')) > 0)
            {
                if (bValidaTransaccion(Convert.ToInt32(HdnNumeroFactura.Value)))
                {
                    if (GuardaDetallePago(Convert.ToInt32(HdnNumeroFactura.Value)))
                    {
                        if (GuardaEncabezadoPago(Convert.ToInt32(HdnNumeroFactura.Value)))
                            return true;
                    }
                }

            }
            return false;


        }
        public int iConceptoDefinido()
        {
            int intConcepto = 0, intNumeroPrograma = 289, intNumeroTransaccion = 0, intDefinida = 0;

            DataTable dtNumeroTransaccion = new DataTable();
            DataTable dtTransaccionContableDefinida = new DataTable();

            //foreach (GridViewRow row in GrvDetalles.Rows)
            //{
            //    DropDownList ddlMotivo = (DropDownList)row.FindControl("MotivoDropDownList");

            //    CheckBox chk = new CheckBox();
            //    chk = (CheckBox)row.FindControl("chkPartida");
            //    if (chk != null && chk.Checked)
            //    {
            //        if (ddlMotivo != null)
            //        {
            //            intConcepto = Convert.ToInt32(ddlMotivo.SelectedValue);
            //            break;
            //        }
            //    }


            //}
            if (drpConceptoConta.SelectedValue != "")
            {
                intConcepto = Convert.ToInt32(drpConceptoConta.SelectedValue);
            }

            dtNumeroTransaccion = DevolucionController.ObtenNumeroTransaccion(Convert.ToInt32(Session["Company"]), intConcepto, intNumeroPrograma); // aqui cambiar

            if (dtNumeroTransaccion.Rows.Count > 0)
            {
                intNumeroTransaccion = Convert.ToInt32(dtNumeroTransaccion.Rows[0][0]);

            }

            if (intNumeroTransaccion == 0)
            {
                //LabelTool.ShowLabel(lblMsjConcepto, "No se encontró el registro para la contabilidad", System.Drawing.Color.DarkRed);
                return 0;

            }
            else
            {
                dtTransaccionContableDefinida = DevolucionController.ObtenNumeroTransaccionContable(Convert.ToInt32(Session["Company"]), intNumeroTransaccion, intNumeroPrograma);
                if (dtTransaccionContableDefinida != null && dtTransaccionContableDefinida.Rows.Count > 0)
                {
                    intDefinida = Convert.ToInt32(dtTransaccionContableDefinida.Rows[0][0]);
                }

                if (intDefinida == 0)
                {
                    // LabelTool.ShowLabel(lblMsjConcepto, "La transacción contable no se encuentra definida", System.Drawing.Color.DarkRed);

                    return 0;
                }
            }
            if (intDefinida == 0)
            {
                return 0;
            }
            else
            {
                return intNumeroTransaccion;
            }



        }
        public bool GuardaEncabezadoPago(int intNumeroFactura)
        {
            int inttype;
            bool bresult = true;
            //lblPagoRealizado.Text = "0";

            if (PayInvoiceProcess(intNumeroFactura))//inserta encabezado del pago
            {
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "CerrarPopupsPago();", true);
            }

            else
            {

                LabelTool.ShowLabel(lblError, "Error al realizar la devolución, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                bresult = false;
            }
            return bresult;
        }
        private bool PayInvoiceProcess(int intNumeroFactura)
        {

            InvoiceBLL PayInvoicePss = new InvoiceBLL();

            PayInvoicePss.iCompany = Convert.ToInt32(Session["Company"]);
            PayInvoicePss.iInvoiceNumber = intNumeroFactura;

            return PayInvoicePss.PayInvoiceProcess();

        }
        public bool GuardaDetallePago(int intNumeroFactura)
        {
            int inttype;
            bool bresult = true;
            bresult = bresult && CreatePay(intNumeroFactura);//inserta detalle pago

            return bresult;
        }
        private bool CreatePay(int intNumeroFactura)
        {
            int inttype = 1;

            if (PayInvoice(inttype, intNumeroFactura))
            {

                return true;
            }
            else
            {
                //itype = 4;

                //PayInvoice(itype, iNumeroFactura);

                return false;
            }

        }
        private bool PayInvoice(int inttype, int intNumeroFactura)
        {

            InvoiceBLL PayInvoice = new InvoiceBLL();

            PayInvoice.iType = inttype;
            PayInvoice.iCompany = Convert.ToInt32(Session["Company"]);
            PayInvoice.iInvoiceNumber = intNumeroFactura;
            PayInvoice.iNumberAux = null;
            PayInvoice.dHaber = Convert.ToDecimal(lblTotal.Text.Replace('$', ' ')); ;//Egreso


            if (inttype == 1)
            {
                int intConcepto = 0;
                if (Convert.ToInt32(drpMoneda.SelectedValue) == 109)
                {
                    intConcepto = -1;
                }
                else
                {
                    intConcepto = -2;
                }

                PayInvoice.iConceptNumber = intConcepto;
                PayInvoice.iCurrency = Convert.ToInt32(drpMoneda.SelectedValue);
                PayInvoice.dExchange = Convert.ToDecimal(lblTipoCambio.Text);
                PayInvoice.dIngreso = null;//ingreso
                PayInvoice.dEgreso = Convert.ToDecimal(lblTotal.Text.Replace('$', ' ')); ;//Egreso
                PayInvoice.sAux = "";//Referencia
                PayInvoice.Person = Convert.ToInt32(HdnUsuario.Value);
            }


            return PayInvoice.PayInvoice();

        }
        private bool bValidaTransaccion(int intNumeroFactura)
        {
            InvoiceBLL Invoice = new InvoiceBLL();
            DataTable ValidaTable;
            string ConceptoContable = "";
            string sMsjTransaccSinDefinir = "";
            decimal dingreso = 0;
            int intConcepto = 0;
            if (Convert.ToInt32(drpMoneda.SelectedValue) == 109)
            {
                intConcepto = -1;
            }
            else
            {
                intConcepto = -2;
            }

            ValidaTable = Invoice.ValidaConceptoContable(Convert.ToInt32(Session["Company"]), Convert.ToInt32(intNumeroFactura), intConcepto);

            if (ValidaTable != null)
            {
                ConceptoContable = Convert.ToString(ValidaTable.Rows[0][0]);

                if (ConceptoContable != "")
                {
                    sMsjTransaccSinDefinir = sMsjTransaccSinDefinir + "El concepto efectivo";

                }
            }


            if (sMsjTransaccSinDefinir != "")
            {
                //lblMsjTransaccion.Text = sMsjTransaccSinDefinir;
                //mdlTransaccion.Show();
                return false;
            }

            return true;
        }

        public bool ValidarCantidades()
        {
            bool valido = false;

            foreach (GridViewRow row in GrvDetalles.Rows)
            {

                CheckBox chk = new CheckBox();
                chk = (CheckBox)row.FindControl("chkPartida");
                if (chk != null && chk.Checked)
                {
                    Label cantidadorigen = (Label)GrvDetalles.Rows[row.RowIndex].FindControl("Cantoriginal");
                    Label precio = (Label)GrvDetalles.Rows[row.RowIndex].FindControl("lblPrecioConIEPS");
                    TextBox cantidadtxt = (TextBox)GrvDetalles.Rows[row.RowIndex].FindControl("txtCantidad");

                    if (cantidadtxt != null && cantidadtxt.Text != "" && Convert.ToDouble(cantidadtxt.Text) > Convert.ToDouble(cantidadorigen.Text))
                    {
                        cantidadtxt.BorderColor = System.Drawing.Color.DarkRed;
                        LabelTool.ShowLabel(lblmsjerror, "Verificar la cantidad a devolver", System.Drawing.Color.DarkRed);
                        valido = false;

                    }
                    else
                    {
                        cantidadtxt.BorderColor = System.Drawing.Color.Gray;
                        LabelTool.HideLabel(lblmsjerror);
                        valido = true;
                    }

                }
                else
                    LabelTool.ShowLabel(lblmsjerror, "Seleccione producto para devolución", System.Drawing.Color.DarkRed);

            }
            return valido;
        }
        public void obtenerTotal()
        {
            decimal dSumaSubtotal = 0, dTotal = 0, dIva = 0, dSumaIva = 0, auxValorIva = 0, dTotalPartida = 0;
            string sSumaSubtotal = string.Empty, sTotal = string.Empty, sIva = string.Empty;


            foreach (GridViewRow row in GrvDetalles.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)row.FindControl("chkPartida");
                Label precio = (Label)GrvDetalles.Rows[row.RowIndex].FindControl("lblPrecioConIEPS");
                Label lblValorIva = (Label)GrvDetalles.Rows[row.RowIndex].FindControl("lblValorIva");
                TextBox txtcantidad = (TextBox)GrvDetalles.Rows[row.RowIndex].FindControl("txtCantidad");

                if (chk != null && chk.Checked && txtcantidad.Text != "" && chk.Enabled)
                {
                    dTotalPartida = (Convert.ToDecimal(txtcantidad.Text) * Convert.ToDecimal(precio.Text.Replace('$', ' ')));
                    dSumaSubtotal += (Convert.ToDecimal(txtcantidad.Text) * Convert.ToDecimal(precio.Text.Replace('$', ' ')));

                    if (lblValorIva.Text != "")// sacar el iva por partida
                    {
                        auxValorIva = Convert.ToDecimal(lblValorIva.Text) / 100;
                        dSumaIva += dTotalPartida * auxValorIva;
                    }
                }

            }

            dIva = dSumaIva;
            dTotal = dSumaSubtotal + dSumaIva;


            sIva = mostrar_dos_decimas(dIva.ToString());
            sSumaSubtotal = mostrar_dos_decimas(dSumaSubtotal.ToString());
            sTotal = mostrar_dos_decimas(dTotal.ToString());


            lblSubtotal.Text = sSumaSubtotal;
            lblIva.Text = sIva;
            txtTotal.Text = sTotal;
            lblTotal.Text = sTotal;
            // ====================================
            // parametros 
            listaParametros = new List<string>();
            listaParametros.Add(sSumaSubtotal);
            listaParametros.Add(sIva);
            listaParametros.Add(sTotal);

        }


        public string mostrar_dos_decimas(string b)
        {
            string a = ""; int xc = 0; int xc2 = 0;
            for (int i = 0; i < b.Length; i++)
            {
                if (b.Substring(i, 1) != "." & xc == 0)
                {
                    a += b.Substring(i, 1);
                }
                else
                {
                    xc = 1;
                    if (xc2 < 3)
                    {
                        a += b.Substring(i, 1);
                    }
                    xc2++;
                }
            }
            return a;
        }

        private void lfPopulate_serie()
        {
            int intpiOpcion = 0;
            DropTool.gfClearDrop(ref drpSerie);

            if (drpDocumento.SelectedItem != null)

                DropTool.gfFillDrop(ref drpSerie, FacturacionCancelacionController.gfGetSeries(Convert.ToInt32(Session["Company"]),
                                                                           Convert.ToInt32(drpDocumento.SelectedValue),
                                                                           Convert.ToInt32(lblIdCliente.Text), intpiOpcion).Copy(), "Descripcion", "Numero");


        }
        private void obtenerNombrePersona(int intCliente)
        {

            PersonaController persona = new PersonaController();
            DataTable dt = new DataTable();

            dt = persona.obtenerDatosPorNumeroPersona(Convert.ToInt32(Session["Company"]), intCliente);

            if (dt != null && dt.Rows.Count > 0)
            {
                lblCliente.Text = dt.Rows[0][2].ToString();
                LblNombreCorto.Text = dt.Rows[0][6].ToString();//nombre corto

            }
        }
        private void lfConfiguracionInicial(int intpiOpcion = 0)
        {

            DataTable dtTipoDocumento, dtSerie;
            DataTable dtNumeroDocumentos = new DataTable();
            try
            {

                dtTipoDocumento = FacturacionCancelacionController.gfGetDocuments(int.Parse(Convert.ToString(Session["Company"])),
                                                                                   Convert.ToInt32(lblIdCliente.Text),
                                                                                  intpiOpcion).Copy();
                DropTool.gfFillDrop(ref drpDocumento, dtTipoDocumento, "Descripcion", "Numero");

                int intTipoDocumento = (drpDocumento.SelectedItem.Value.Trim() == "Seleccione") ? 0 : Convert.ToInt32(drpDocumento.SelectedItem.Value);

                dtSerie = FacturacionCancelacionController.gfGetSeries(Convert.ToInt32(Session["Company"]),
                                                                       intTipoDocumento,
                                                                       Convert.ToInt32(lblIdCliente.Text),
                                                                       intpiOpcion).Copy();
                DropTool.gfFillDrop(ref drpSerie, dtSerie, "Descripcion", "Numero");


                //if (intpiOpcion == 1)
                //    dtNumeroDocumentos = FacturacionCancelacionController.gfGetDocumentNumbers(Convert.ToInt32(Session["Company"]), 0, 0, 0, 1);
                //else
                //    dtNumeroDocumentos = CancelInvoiceControlsBLL.GetDocumentNumbers(int.Parse(Convert.ToString(Session["Company"])), int.Parse(drpDocumento.SelectedValue),
                //                                                                       Convert.ToInt32(lblIdCliente.Text), int.Parse(drpSerie.SelectedValue));

                //DropTool.gfDropFill(ref drpNumeroDe, null, dtNumeroDocumentos);
                //DropTool.gfDropFill(ref drpNumeroHasta, null, dtNumeroDocumentos);
                DropTool.gfFillDrop(ref drpMoneda, ControlsBLL.GetCurrencies(Convert.ToInt32(Session["Company"]), Globales.FatherDefault, Globales.FamilyDefault), "Descripcion", "Numero");

                GridViewTool.Bind(null, grvDocumentos);

                drpDocumento.Enabled = true;
                drpSerie.Enabled = true;
                txtNumeroDe.Enabled = true;
                txtNumeroHasta.Enabled = true;


            }
            catch (Exception ex)
            {
                //LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblMsg);
            }
        }
        private int obtenerFechas()
        {
            try
            {
                if (txtFechaDe.Text != "")
                {
                    fechaDe = Convert.ToDateTime(txtFechaDe.Text.Trim());

                    if (fechaDe.Year <= 1900 || fechaDe.Month <= 0 || fechaDe.Day <= 0)
                        return 0;
                }

                if (txtFechaHasta.Text != "")
                {
                    fechaHasta = Convert.ToDateTime(txtFechaHasta.Text.Trim());

                    if (fechaHasta.Year <= 1900 || fechaHasta.Month <= 0 || fechaHasta.Day <= 0)
                        return 0;
                }

                return 1;
            }
            catch
            {
                return 0;
            }
        }
        private void BuscarVenta()
        {

            DataTable dt = null;
            DateTime? dtA = null;
            DateTime? dtB = null;
            try
            {
                dtA = ((txtFechaDe.Text.Trim().Length == 0) ? dtA : Convert.ToDateTime(txtFechaDe.Text));
                dtB = ((txtFechaHasta.Text.Trim().Length == 0) ? dtB : Convert.ToDateTime(txtFechaHasta.Text));
                dtTemp = null;
                if (drpDocumento.Enabled)
                {
                    string de = txtNumeroDe.Text.Trim() == "" ? "0" : txtNumeroDe.Text.Trim();
                    string hasta = txtNumeroHasta.Text.Trim() == "" ? "0" : txtNumeroHasta.Text.Trim();

                    dt = new FacturacionCancelacionController().gfGetFacturas(int.Parse(Convert.ToString(Session["Company"])),
                                                                                       Convert.ToInt32(lblIdCliente.Text),
                                                                                       int.Parse(drpMoneda.SelectedItem.Value),
                                                                                       int.Parse(drpDocumento.SelectedItem.Value),
                                                                                       int.Parse(drpSerie.SelectedItem.Value),
                                                                                       dtA,
                                                                                       dtB,
                                                                                       int.Parse(de),
                                                                                       int.Parse(hasta));
                    dtTemp = dt.Copy();
                    grvDocumentos.DataSource = dtTemp;
                    grvDocumentos.DataBind();
                }
                else
                {

                    lblIdCliente.Text = "0";
                    string sTemp = string.Empty;


                    string sdta = (dtA == null) ? "null" : "'" + dtA.ToString() + "'";
                    string sdtB = (dtB == null) ? "null" : "'" + dtB.ToString() + "'";

                    string sQuery = @"execute [sp_Factura_ObtieneVistaDeFacturas_v3] " + int.Parse(Convert.ToString(Session["Company"])) + ",0,0,0,0," + sdta + "," + sdtB + ",0,0";
                    dt = new SQLConection().ExecuteQuery(sQuery);
                    dtTemp = dt.Copy();
                    grvDocumentos.DataSource = dtTemp;
                    grvDocumentos.DataBind();
                }

                Session["buscar"] = dtTemp;
                //LabelTool.HideLabel(lblError);
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "No se han encontrado documentos con esos criterios!!!", System.Drawing.Color.DarkRed);
                GridViewTool.Bind(null, grvDocumentos);
            }


        }

        #endregion

        #region Facturacion

        //Declara Variables 

        static string sPersonalidad;


        private bool ProcesoDocumentoElectronico()
        {

            try
            {
                DataTable DataCorreoContactos = new DataTable();
                // sirve para traer el 'importe'total en letra 
                HttpContext.Current.Session["dropMonedaText"] = drpMoneda.SelectedItem.Text;

                if (ValidacionDatos())
                {
                    DataTable DataInternet = DatosDoctoInternet();

                    if (DataInternet != null)
                    {
                        DataCorreoContactos = DatosCorreoE();
                        //SQLConection context = new SQLConection();
                        //DataCorreoContactos = context.ExecuteQuery(" SELECT 'cover_24@hotmail.com' AS Correo_Electronico , 'ELIA P' AS Nombre_Completo ");
                        if (DataCorreoContactos != null && DataCorreoContactos.Rows.Count > 0)
                        {
                            if (Convert.ToString(DataCorreoContactos.Rows[0][0]) == "")
                            {
                                LabelTool.ShowLabel(lblError, "Error, no se ha encontrado correo electrónico de los contactos del cliente!!!", System.Drawing.Color.DarkRed);
                                return false;
                            }

                        }

                        else
                        {
                            LabelTool.ShowLabel(lblError, "Error, no se ha encontrado correo electrónico de los contactos del cliente!!!", System.Drawing.Color.DarkRed);
                            return false;
                        }

                    }

                    if (ValidacionDatosMailCompany())
                    {
                        if (validacionpara_e_docto(Convert.ToInt32(txtNumero.Text)))
                        {
                            //VALIDA E-MAIL
                            if (ValidacionDatosElectronico(Convert.ToInt32(HdnNumeroNota.Value)))
                            {

                            }
                        }
                        else
                        {
                            return false;
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                return false;

            }

            return true;
        }


        private bool ValidacionDatos()
        {
            if (ValidacionDatosDocto())
            {
                if (ValidacionDatosCliente())
                {
                    if (ValidacionDatosEmpresa())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }

        private bool ValidacionDatosDocto()
        {
            DataTable TablaDoctos;
            string sRutaKey;
            string sContraseñaKey;
            sRutaKey = string.Empty;
            sContraseñaKey = string.Empty;

            if ((TablaDoctos = DatosDocto()) != null)
            {
                if (Convert.ToString(TablaDoctos.Rows[0][0]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de llave privada!!", System.Drawing.Color.DarkRed);

                }
                if (Convert.ToString(TablaDoctos.Rows[0][0]) != "")
                {
                    sRutaKey = Convert.ToString(TablaDoctos.Rows[0][0]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][1]) != "")
                {
                    sContraseñaKey = Convert.ToString(TablaDoctos.Rows[0][1]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró la contraseña del archivo de llave privada!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][2]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][3]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturó el año de aprobación!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToInt32(TablaDoctos.Rows[0][3]) > 9999)
                {
                    LabelTool.ShowLabel(lblError, "Error,en la captura del año de aprobación!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][4]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturó el número de aprobación!!", System.Drawing.Color.DarkRed);
                    return false;
                }
                if ((TablaDoctos.Rows[0][5]) != null && Convert.ToString(TablaDoctos.Rows[0][5].ToString()) != "")
                {
                    if (Convert.ToInt32(txtNumero.Text) > Convert.ToInt32(TablaDoctos.Rows[0][5]))
                    {
                        LabelTool.ShowLabel(lblError, "Error,el número de documento es mayor al folio final capturado en la configuración de documentos electrónicos!!", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }
                return true;
            }

            else
            {
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica de los folios para la operación de documentos electrónicos !!!", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        private bool ValidacionDatosCliente()
        {
            DataTable TablaCliente;
            string rfc;
            int inttipo;
            rfc = "";

            if ((TablaCliente = PersonBLL.ObtenDatosPersonaCliente(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdBeneficiario.Text))) != null)
            {
                if (Convert.ToString(TablaCliente.Rows[0][14]) != null && Convert.ToString(TablaCliente.Rows[0][14]) != "")
                {
                    sPersonalidad = Convert.ToString(TablaCliente.Rows[0][14]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error,el cliente no tiene un tipo de persona jurídica!!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaCliente.Rows[0][7]) != null && Convert.ToString(TablaCliente.Rows[0][7]) != "")
                {
                    rfc = Convert.ToString(TablaCliente.Rows[0][7]);

                    if (rfc.ToUpper() != "XEXX010101000" && rfc.ToUpper() != "XAXX010101000")
                    {
                        if (rfc.Length < 12)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede ser menor a 12 caracteres!!!", System.Drawing.Color.DarkRed);
                            return false;
                        }
                        if (rfc.Length > 13)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede ser mayor a 13 caracteres!!!", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no se ha capturado!!!", System.Drawing.Color.DarkRed);
                }

                if (sPersonalidad == "F")
                {
                    inttipo = 1;
                    if (rfc.ToUpper() != "XEXX010101000" && rfc.ToUpper() != "XAXX010101000")
                    {
                        if (rfc.Length != 13)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede ser diferente a 13 caracteres!!!", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }
                else
                {
                    inttipo = 0;
                    if (rfc.ToUpper() != "XEXX010101000" && rfc.ToUpper() != "XAXX010101000")
                    {

                        if (rfc.Length != 12)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede diferente a 12 caracteres!!!", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }

                if (Convert.ToString(TablaCliente.Rows[0][2]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,el nombre del cliente no se ha capturado!!!", System.Drawing.Color.DarkRed);
                }
                if (rfc.ToUpper() != "XEXX010101000" && rfc.ToUpper() != "XAXX010101000")
                {

                    if (!RFCValidation(rfc, inttipo))
                    {
                        LabelTool.ShowLabel(lblError, "EL RFC no coincide con la validación, desea utilizarlo?", System.Drawing.Color.DarkRed);

                        return false;
                    }
                }

                DataTable DataAddress = GetAddress();

                if (DataAddress != null)
                {
                    if (DataAddress.Rows[0][0] == null)
                    {
                        if (Convert.ToString(DataAddress.Rows[0][0]) == "")

                            LabelTool.ShowLabel(lblError, "El domicilio del cliente  no se ha capturado", System.Drawing.Color.DarkRed);
                    }

                    if (DataAddress.Rows[0][1] == null)
                    {
                        if (Convert.ToString(DataAddress.Rows[0][1]) == "")
                            LabelTool.ShowLabel(lblError, "La colonia de la dirección  del cliente  no se ha capturado", System.Drawing.Color.DarkRed);
                    }

                    if (DataAddress.Rows[0][2] == null)
                    {
                        if (Convert.ToString(DataAddress.Rows[0][2]) == "")

                            LabelTool.ShowLabel(lblError, "El código postal de la dirección  del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                    }
                    else
                    {
                        string cp = Convert.ToString(DataAddress.Rows[0][2]);

                        if (cp.Length != 5)
                        {
                            LabelTool.ShowLabel(lblError, "El código postal de la dirección  del cliente deberá ser igual a 5 caracteres", System.Drawing.Color.DarkRed);
                        }
                    }

                    if (DataAddress.Rows[0][3] == null)
                    {
                        if (Convert.ToString(DataAddress.Rows[0][3]) == "")
                            LabelTool.ShowLabel(lblError, "La delegación/municipio de la dirección  del cliente  no se ha capturado", System.Drawing.Color.DarkRed);
                    }

                    if (DataAddress.Rows[0][4] == null)
                    {
                        if (Convert.ToString(DataAddress.Rows[0][4]) == "")
                            LabelTool.ShowLabel(lblError, "La estado de la dirección  del cliente  no se ha capturado", System.Drawing.Color.DarkRed);
                    }
                    if (DataAddress.Rows[0][5] == null || DataAddress.Rows[0][6] == null)
                    {
                        LabelTool.ShowLabel(lblError, "La dirección del cliente no cuenta con un país", System.Drawing.Color.DarkRed);
                    }
                }

                return true;
            }
            else
            {
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica del cliente para la operación de documentos electronicos !!!", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        private bool ValidacionDatosEmpresa()
        {
            DataTable TablaEmpresa;

            if ((TablaEmpresa = DatosEmpresa()) != null)
            {
                if (Convert.ToString(TablaEmpresa.Rows[0][0]) != null && Convert.ToString(TablaEmpresa.Rows[0][0]) != "")
                {
                    string rfc = Convert.ToString(TablaEmpresa.Rows[0][0]);

                    if (rfc.Length < 12)
                    {
                        LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no puede ser menor a 12 caracteres!!!", System.Drawing.Color.DarkRed);
                    }
                    if (rfc.Length > 13)
                    {
                        LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no puede ser mayor a 13 caracteres!!!", System.Drawing.Color.DarkRed);
                    }
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no se ha capturado!!!", System.Drawing.Color.DarkRed);
                }

                if (Convert.ToString(TablaEmpresa.Rows[0][1]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,el nombre del emisor no se ha capturado!!!", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][2]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,el domicilio del emisor no se ha capturado!!!", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][3]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,la colonia de la dirección del emisor no se ha capturado!!!", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][4]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,la delegación/municipio de la dirección del emisor no se ha capturado!!!", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][5]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,el estado de la dirección del emisor no se ha capturado!!!", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][6]) == null)
                {
                    string cp = Convert.ToString(TablaEmpresa.Rows[0][6]);

                    if (cp.Length < 5)
                    {
                        LabelTool.ShowLabel(lblError, "Error,el código postal del emisor no puede ser menor a 5 caracteres!!!", System.Drawing.Color.DarkRed);
                    }
                    if (cp.Length > 5)
                    {
                        LabelTool.ShowLabel(lblError, "Error,el código postal del emisor no puede ser mayor a 5 caracteres!!!", System.Drawing.Color.DarkRed);
                    }
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][7]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,el régimen fiscal del emisor no se ha capturado!!!", System.Drawing.Color.DarkRed);
                }

                return true;
            }
            else
            {
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica de la empresa para la operación de documentos electrónicos !!!", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        private DataTable DatosDocto()
        {
            SaleBLL DatosDocumento = new SaleBLL();

            DatosDocumento.Company = Convert.ToInt32(Session["Company"]);
            DatosDocumento.Documento = Convert.ToInt32(drptipoDocumento.SelectedValue);
            DatosDocumento.Serie = Convert.ToInt32(drpdocSerie.SelectedValue);

            return DatosDocumento.GetDatosDocto();
        }

        private bool RFCValidation(string rfc, int inttipo)
        {
            IntegraBussines.CustomerBLL Customer = new IntegraBussines.CustomerBLL();
            return Customer.RFCValidation(rfc, inttipo);
        }

        private DataTable GetAddress()
        {
            SaleBLL TableAddress = new SaleBLL();

            TableAddress.Company = Convert.ToInt32(Session["Company"]);
            TableAddress.Customer = Convert.ToInt32(lblIdCliente.Text);
            TableAddress.Type = 13;

            return TableAddress.GetAddress();
        }

        private DataTable DatosEmpresa()
        {
            SaleBLL DatosEmp = new SaleBLL();

            DatosEmp.Company = Convert.ToInt32(Session["Company"]);

            return DatosEmp.GetDatosEmpresa();
        }

        private DataTable DatosDoctoInternet()
        {
            SaleBLL DatosDocInternet = new SaleBLL();

            DatosDocInternet.Company = Convert.ToInt32(Session["Company"]);
            DatosDocInternet.Serie = Convert.ToInt32(drpdocSerie.SelectedValue);

            return DatosDocInternet.GetDatosInternet();
        }

        private DataTable DatosCorreoE()
        {
            SaleBLL DatosCorreo = new SaleBLL();

            DatosCorreo.Company = Convert.ToInt32(Session["Company"]);
            DatosCorreo.Customer = Convert.ToInt32(lblIdCliente.Text);
            DatosCorreo.Type = 1;

            return DatosCorreo.GetDatosCorreo();
        }

        private bool ValidacionDatosMailCompany()
        {
            DataTable TablaMail;
            SaleBLL Sale = new SaleBLL();

            if ((TablaMail = Sale.GetStoresValidationsAndMailCompany(Convert.ToInt32(Session["Company"]))) != null)
            {
                if (Convert.ToString(TablaMail.Rows[0][4]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error, falta dato cuenta de correo del usuario!!", System.Drawing.Color.DarkRed);
                    return false;
                }
                if (Convert.ToString(TablaMail.Rows[0][6]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error, falta dato cuenta de correo del usuario!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaMail != null)
                {
                    if (Convert.ToInt32(TablaMail.Rows[0][5]) == 1)
                        if (Convert.ToString(TablaMail.Rows[0][7]) == null)
                        {
                            LabelTool.ShowLabel(lblError, "Error, falta dato en la contraseña del correo del usuario!!", System.Drawing.Color.DarkRed);
                            return false;
                        }
                }
                return true;
            }

            else
            {
                LabelTool.ShowLabel(lblError, "Error, el usuario no tiene configurado su correo!!!", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        private bool ValidacionDatosElectronico(int intNumeroFactura)
        {
            if (ValidacionDatosDoctoElectronico(intNumeroFactura))
            {
                if (ValidacionDatosDoctoEmisor(intNumeroFactura))
                {
                    //if (ValidacionDatosDoctoReceptor(intNumeroFactura))
                    //{
                    return true;
                    //}
                    //else
                    //{
                    //    return false;
                    //}
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }

        private bool ValidacionDatosDoctoElectronico(int intNumeroFactura)
        {
            DataTable TablaDoctosE;
            string sRutaKey;
            string sContraseñaKey;
            sRutaKey = string.Empty;
            sContraseñaKey = string.Empty;

            if ((TablaDoctosE = DatosDoctoE(intNumeroFactura)) != null && TablaDoctosE.Rows.Count > 0)
            {
                if (Convert.ToString(TablaDoctosE.Rows[0][0]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de llave privada!!", System.Drawing.Color.DarkRed);

                }
                if (Convert.ToString(TablaDoctosE.Rows[0][0]) != "")
                {
                    sRutaKey = Convert.ToString(TablaDoctosE.Rows[0][0]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][1]) != "")
                {
                    sContraseñaKey = Convert.ToString(TablaDoctosE.Rows[0][1]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró la contraseña del archivo de llave privada!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][2]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][3]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturo el año de aprobación!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToInt32(TablaDoctosE.Rows[0][3]) > 9999)
                {
                    LabelTool.ShowLabel(lblError, "Error,en la captura del año de aprobación!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][4]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturo el número de aprobación!!", System.Drawing.Color.DarkRed);
                    return false;
                }
                return true;
            }

            else
            {
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica de los folios para la operación de documentos electrónicos !!!", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        private bool ValidacionDatosDoctoEmisor(int intNumeroFactura)
        {
            DataTable TablaEmisor;
            string rfc;

            if ((TablaEmisor = DatosDoctoEmisor()) != null)
            {
                if (TablaEmisor.Rows[0][0] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][0]) != "")
                    {
                        rfc = Convert.ToString(TablaEmisor.Rows[0][0]);

                        if (rfc.Length < 12)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no puede ser menor a 12 caracteres!!!", System.Drawing.Color.DarkRed);
                            return false;
                        }
                        if (rfc.Length > 13)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no puede ser mayor a 13 caracteres!!!", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no se ha capturado !!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaEmisor.Rows[0][1] == null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][1]) == "")
                        LabelTool.ShowLabel(lblError, "El nombre del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaEmisor.Rows[0][2] == null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][2]) == "")
                        LabelTool.ShowLabel(lblError, "El domicilio del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaEmisor.Rows[0][3] == null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][3]) == "")
                        LabelTool.ShowLabel(lblError, "La colonia de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaEmisor.Rows[0][4] == null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][4]) == "")
                        LabelTool.ShowLabel(lblError, "La delegación/municipio de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaEmisor.Rows[0][5] == null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][5]) == "")
                        LabelTool.ShowLabel(lblError, "El estado de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaEmisor.Rows[0][6] == null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][6]) == "")

                        LabelTool.ShowLabel(lblError, "El código postal de la dirección  del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                }
                else
                {
                    string cp = Convert.ToString(TablaEmisor.Rows[0][6]);

                    if (cp.Length != 5)
                    {
                        LabelTool.ShowLabel(lblError, "El código postal de la dirección  del cliente deberá ser igual a 5 caracteres", System.Drawing.Color.DarkRed);
                    }
                }

                if (TablaEmisor.Rows[0][8] == null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][8]) == "")
                        LabelTool.ShowLabel(lblError, "No se encuentra capturado el régimen fiscal de la empresa", System.Drawing.Color.DarkRed);
                    return false;
                }
                return true;
            }

            else
            {
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica de los folios para la operación de documentos electrónicos !!!", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        private bool ValidacionDatosDoctoReceptor(int intNumeroFactura)
        {
            DataTable TablaReceptor;
            string rfc;

            if ((TablaReceptor = DatosDoctoReceptor(intNumeroFactura)) != null)
            {
                if (TablaReceptor.Rows[0][0] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][0]) != "")
                    {
                        rfc = Convert.ToString(TablaReceptor.Rows[0][0]);

                        if (rfc.Length < 12)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede ser menor a 12 caracteres!!!", System.Drawing.Color.DarkRed);
                            return false;
                        }
                        if (rfc.Length > 13)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede ser mayor a 13 caracteres!!!", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no se ha capturado !!!", System.Drawing.Color.DarkRed);
                    return false;
                }


                if (TablaReceptor.Rows[0][1] == null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][1]) == "")
                        LabelTool.ShowLabel(lblError, "El nombre del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaReceptor.Rows[0][2] == null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][2]) == "")
                        LabelTool.ShowLabel(lblError, "El domicilio del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaReceptor.Rows[0][3] == null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][3]) == "")
                        LabelTool.ShowLabel(lblError, "La colonia de la dirección del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaReceptor.Rows[0][4] == null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][4]) == "")

                        LabelTool.ShowLabel(lblError, "El código postal de la dirección  del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                }
                else
                {
                    string cp = Convert.ToString(TablaReceptor.Rows[0][4]);

                    if (cp.Length != 5)
                    {
                        LabelTool.ShowLabel(lblError, "El código postal de la dirección  del cliente deberá ser igual a 5 caracteres", System.Drawing.Color.DarkRed);
                    }
                }

                if (TablaReceptor.Rows[0][5] == null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][5]) == "")
                        LabelTool.ShowLabel(lblError, "La delegación/municipio de la dirección del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaReceptor.Rows[0][6] == null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][6]) == "")
                        LabelTool.ShowLabel(lblError, "El estado de la dirección del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaReceptor.Rows[0][8] == null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][8]) == "")
                        LabelTool.ShowLabel(lblError, "La dirección del cliente no cuenta con un país", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaReceptor.Rows[0][7] != null)
                {
                    int intporcentaje;
                    int intfamily = 3;
                    SaleBLL Invoice = new SaleBLL();

                    int intiva = Convert.ToInt32(TablaReceptor.Rows[0][7]);
                    DataTable Tporcentaje = Invoice.GetIvaReceptor(Convert.ToInt32(Session["Company"]), intiva, intfamily);
                    if (Tporcentaje != null)
                    {
                        intporcentaje = Convert.ToInt32(Tporcentaje.Rows[0][0]);
                    }
                    else
                    {
                        LabelTool.ShowLabel(lblError, "No encontro el porcentaje del I.V.A", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "No se ha capturado el I.V.A", System.Drawing.Color.DarkRed);
                    return false;
                }
                return true;
            }

            else
            {
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica del cliente para la operación de documentos electrónicos !!!", System.Drawing.Color.DarkRed);
                return false;
            }

        }

        private DataTable DatosDoctoE(int intNumeroFactura)
        {
            SaleBLL DatosDocumentoE = new SaleBLL();

            DatosDocumentoE.Company = Convert.ToInt32(Session["Company"]);
            DatosDocumentoE.Number = intNumeroFactura;
            DatosDocumentoE.Type = 1;// aqui tiene que ir cero en caso factura y 1 en caso nota de credito

            return DatosDocumentoE.GetDatosDoctoE();
        }

        private DataTable DatosDoctoEmisor()
        {
            SaleBLL DatosDocumentoEmisor = new SaleBLL();

            DatosDocumentoEmisor.Company = Convert.ToInt32(Session["Company"]);

            return DatosDocumentoEmisor.GetDatosDoctoEmisor();
        }

        private DataTable DatosDoctoReceptor(int intNumeroFactura)
        {
            SaleBLL DatosDocumentoReceptor = new SaleBLL();

            DatosDocumentoReceptor.Company = Convert.ToInt32(Session["Company"]);
            DatosDocumentoReceptor.Number = intNumeroFactura;
            DatosDocumentoReceptor.Type = 0;

            return DatosDocumentoReceptor.GetDatosDoctoReceptor();
        }

        //protected void SendMailClick(string RutaDoc, DataTable DataCorreoContactos)
        //{
        //    try
        //    {
        //        RutaDoc = RutaDoc.Replace(".pdf", ".zip");
        //        SmtpMail Mail = InitializeMail("", "Facturas " + DateTime.Now.ToString(), DataCorreoContactos);

        //        string[] attachments = new string[100];
        //        string NameReport;
        //        int i = 0;

        //        NameReport = RutaDoc;
        //        if (!System.IO.File.Exists(NameReport))
        //        {
        //            attachments[i++] = NameReport;
        //        }
        //        else
        //            attachments[i++] = NameReport;

        //        if (i == 0)
        //        {
        //            LabelTool.ShowLabel(lblError, "Error en documento a enviar", System.Drawing.Color.DarkRed);
        //        }

        //        SendMail(Mail, attachments);
        //    }
        //    catch (Exception ex)
        //    {
        //        LOG.gfLog(HttpContext.Current.Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), "creaFactura_click", ref lblError);
        //    }
        //}

        //private SmtpMail InitializeMail(string Message, string Subject, DataTable DataCorreoContactos)
        //{
        //    DataTable DataMail;
        //    string[] recipients = new string[100];
        //    int i = 0;
        //    int index = 0;
        //    string sServidor, sMail, sContraseña;
        //    sServidor = string.Empty;
        //    sMail = string.Empty;
        //    sContraseña = string.Empty;

        //    foreach (DataRow row in DataCorreoContactos.Rows)
        //    {
        //        if (DataCorreoContactos.Rows[index][0].ToString() != "")
        //        {
        //            recipients[i++] = DataCorreoContactos.Rows[index][0].ToString();
        //        }
        //        index++;
        //    }

        //    if ((DataMail = ControlsBLL.GetDataMail(Convert.ToInt32(Session["Company"]))) == null)
        //    {
        //        LabelTool.ShowLabel(lblError, "Error al conseguir los datos de la empresa!!!", System.Drawing.Color.DarkRed);
        //    }
        //    else
        //    {
        //        if (DataMail.Rows.Count > 0)
        //        {
        //            if (DataMail.Rows[0][0].ToString() != "")
        //            {
        //                sServidor = DataMail.Rows[0][0].ToString();
        //            }
        //            else
        //            {
        //            }
        //            if (DataMail.Rows[0][1].ToString() != "")
        //            {
        //                intPuerto = Convert.ToInt32(DataMail.Rows[0][1]);
        //            }
        //            else
        //            {
        //            }
        //            if (DataMail.Rows[0][2].ToString() != "")
        //            {
        //                sMail = DataMail.Rows[0][2].ToString();
        //            }
        //            else
        //            {
        //            }
        //            if (DataMail.Rows[0][3].ToString() != "")
        //            {
        //                sContraseña = DataMail.Rows[0][3].ToString();
        //            }
        //            else
        //            {
        //            }
        //        }

        //        else
        //        {
        //        }
        //    }

        //    SmtpMail Mail = new SmtpMail(sServidor, intPuerto, sMail, sContraseña, recipients);

        //    Mail.From = sMail;
        //    Mail.Message = Message;
        //    Mail.Subject = Subject;

        //    return Mail;
        //}

        //private void SendMail(SmtpMail Mail, string[] attachments)
        //{
        //    if (Mail.SecutiryHostSendAttachment(attachments))
        //    {
        //        LabelTool.ShowLabel(lblError, "Facturas enviadas correctamente", System.Drawing.Color.DarkRed);
        //    }
        //    else
        //    {
        //        LabelTool.ShowLabel(lblError, "No existe un destinatario", System.Drawing.Color.DarkRed);
        //    }
        //}

        //private SmtpMail InitializeMail(string Message, string Subject)
        //{
        //    DataTable DataMail;
        //    string[] recipients = new string[100];
        //    int i = 0;

        //    int index = 0;
        //    /*********************/
        //    //TablaCorreoCliente = FacturacionCancelacionController.gfTemp();
        //    /*********************/
        //    if (TablaCorreoCliente != null)
        //    {
        //        foreach (DataRow row in TablaCorreoCliente.Rows)
        //        {
        //            if (TablaCorreoCliente.Rows[index][0].ToString() != "")
        //            {
        //                recipients[i++] = TablaCorreoCliente.Rows[index][0].ToString();
        //            }
        //            index++;
        //        }
        //    }

        //    if ((DataMail = ControlsBLL.GetDataMail(Convert.ToInt32(Session["Company"]))) == null)
        //    {
        //        LabelTool.ShowLabel(lblError, "Error al conseguir los datos de la empresa!!!", System.Drawing.Color.DarkRed);
        //    }
        //    else
        //    {
        //        if (DataMail.Rows.Count > 0)
        //        {
        //            if (DataMail.Rows[0][0].ToString() != "")
        //            {
        //                sServidor = DataMail.Rows[0][0].ToString();
        //            }
        //            else
        //            {
        //                //sServidor = "smtp.gmail.com";
        //            }
        //            if (DataMail.Rows[0][1].ToString() != "")
        //            {
        //                intPuerto = Convert.ToInt32(DataMail.Rows[0][1]);
        //            }
        //            else
        //            {

        //            }
        //            if (DataMail.Rows[0][2].ToString() != "")
        //            {
        //                sMail = DataMail.Rows[0][2].ToString();


        //            }
        //            else
        //            {

        //            }
        //            if (DataMail.Rows[0][3].ToString() != "")
        //            {
        //                sContraseña = DataMail.Rows[0][3].ToString();
        //            }

        //        }

        //    }

        //    SmtpMail Mail = new SmtpMail(sServidor, intPuerto, sMail, sContraseña, recipients);

        //    Mail.From = sMail;
        //    Mail.Message = Message;
        //    Mail.Subject = Subject;

        //    return Mail;
        //}

        private bool validacionpara_e_docto(int intFolio)
        {
            if (ValidacionDatos(intFolio))
            {

                TablaCorreoCliente = DatosCorreoCliente();

                if (TablaCorreoCliente != null && TablaCorreoCliente.Rows.Count > 0)
                {
                    if (Convert.ToString(TablaCorreoCliente.Rows[0][0]) == "")
                    {
                        LabelTool.ShowLabel(lblError, "Error, no se ha encontrado correo electrónico de los contactos del cliente", System.Drawing.Color.DarkRed);
                        return false;
                    }

                }
                else
                {
                    return false;
                }
                if (!ValidacionDatosMailCompany())
                {
                    return false;
                }

                LabelTool.HideLabel(lblError);
                return true;

            }

            else
            {
                return false;
            }
        }

        private bool ValidacionDatos(int intFolio)
        {
            if (ValidacionDatosDocto(intFolio))// 
            {
                if (ValidacionDatosCliente())//
                {
                    if (ValidacionDatosEmpresa())//
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }

            else
            {
                return false;
            }
        }

        private bool ValidacionDatosDocto(int intFolio)
        {
            string sRutaKey = "";
            string sContraseñaKey = "";
            DataTable TablaDoctos;

            if ((TablaDoctos = DatosDocto()) != null)
            {
                if (Convert.ToString(TablaDoctos.Rows[0][0]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de llave privada!!", System.Drawing.Color.DarkRed);

                }
                if (Convert.ToString(TablaDoctos.Rows[0][0]) != "")
                {
                    sRutaKey = Convert.ToString(TablaDoctos.Rows[0][0]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][1]) != "")
                {
                    sContraseñaKey = Convert.ToString(TablaDoctos.Rows[0][1]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró la contraseña del archivo de llave privada!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][2]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][3]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturo el año de aprobación!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToInt32(TablaDoctos.Rows[0][3]) > 9999)
                {
                    LabelTool.ShowLabel(lblError, "Error,en la captura del año de aprobación!!", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][4]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturo el número de aprobación!!", System.Drawing.Color.DarkRed);
                    return false;
                }
                if ((TablaDoctos.Rows[0][5]) != null && Convert.ToString(TablaDoctos.Rows[0][5].ToString()) != "")
                {
                    if (intFolio > Convert.ToInt32(TablaDoctos.Rows[0][5]))
                    {
                        LabelTool.ShowLabel(lblError, "Error,el número de documento es mayor al folio final capturado en la configuración de documentos electrónicos!!", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                return true;
            }

            else
            {
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica de los folios para la operación de documentos electrónicos !!!", System.Drawing.Color.DarkRed);
                return false;
            }

        }

        private DataTable DatosCorreoCliente()
        {
            SaleBLL DatosCorreo = new SaleBLL();
            int intidClient = Convert.ToInt32(lblIdBeneficiario.Text);
            DatosCorreo.Customer = intidClient;

            return DatosCorreo.GetDatosCorreoCliente();
        }

        #endregion

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ventas.aspx");
        }

    }
}