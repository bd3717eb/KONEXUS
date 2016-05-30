using System;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegraBussines;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using INTEGRAReports;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Web.Configuration;


namespace Integra_Develoment.Ventas
{
    public partial class Ventas : System.Web.UI.Page
    {
        #region Variables

        static int iRounded, iDirectDelivery, iColletIndex, iExistProduct, iClasAlmacen,
                   iPuerto, iReferenceCost, iValueReference = 0, iProduct;
        static decimal dTempExchange, dCambio, dExchange, dTipoCambio;
        static string sSaleValidation, sPersonalidad, sProductosNoValidos;
        static bool bMoral, bFlagCharacteristics, bClose, bResultChange, bValida;
        private string _ProductsTable = "products", _TempProductsTable = "temporalprod";
        private string _CharTable = "characteristics", _TempCharTable = "temporalchar";
        private string _Stores = "stores";
        private string[] Stores;
        public int intFlag = 0;
        DataTable ProductsTableprueba;
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

        private DataTable TempProductsTable
        {
            get
            {
                if (Session[_TempProductsTable] == null)
                    return new DataTable();
                return (DataTable)Session[_TempProductsTable];
            }
            set
            {
                Session[_TempProductsTable] = value;
            }
        }

        private DataTable CharTable
        {
            get
            {
                if (ViewState[_CharTable] == null)
                    return new DataTable();
                return (DataTable)ViewState[_CharTable];
            }
            set
            {
                ViewState[_CharTable] = value;
            }
        }

        private DataTable TempCharTable
        {
            get
            {
                if (Session[_TempCharTable] == null)
                    return new DataTable();
                return (DataTable)Session[_TempCharTable];
            }
            set
            {
                Session[_TempCharTable] = value;
            }
        }

        private DataTable TablaCorreoCliente { get; set; }
        private string sServidor { get; set; }
        private string sMail { get; set; }
        private string sContraseña { get; set; }
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable a = ProductsTable;
                NotPostBack();
                txtProductos.Attributes.Add("onclick", "doClearProductsTextBox();");
                lnkAgregaProductos.Attributes.Add("onclick", "gfProceso();");
                invoicebtnl.Attributes.Add("onclick", "gfProceso();");
                LbtnPay.Attributes.Add("onclick", "gfProceso();");
                HdnValorIncorrecto.Value = "0";

            }
            catch (Exception ex)
            {
                LOG.gfLog("localdana", ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        private void NotPostBack()
        {
            if (!this.IsPostBack)
            {
                HdnEmpresa.Value = Session["Company"].ToString();
                HdnSucursal.Value = "0";
                if (Session["Office"] != null)
                {
                    HdnSucursal.Value = Session["Office"].ToString();
                }
                HdnUsuario.Value = Session["Person"].ToString();

                ConfiguracionVentasEmpresas Configuracion = new ConfiguracionVentasEmpresas(Convert.ToInt32(HdnEmpresa.Value));

                //ConfigurationSales Configuration = new ConfigurationSales(Convert.ToInt32(HdnEmpresa.Value));

                sSaleValidation = Configuracion.sValidacionVenta;
                //sSaleValidation = "CambiarEstatusPartida"; 
                //sSaleValidation = "CambiarEstatusPartidaSE";


                // sSaleValidation = "AgregarPartida";
                //sSaleValidation = "AgregarPartidaSE";
                //sSaleValidation = "CambiarEstatusGeneralSE";
                //sSaleValidation = "CambiarEstatusGeneral";
                //sSaleValidation = "NOTVAL";
                iRounded = 2;

                LlenadoDrops();

                CharTable = CreaTablaDeCaracteristicas();
                bResultChange = true;
                if (sSaleValidation != null)
                {
                    if (sSaleValidation == "NOTVAL" || sSaleValidation.IndexOf("CambiarEstatusGeneral") != -1)
                    {
                        grvPartidas.Columns[1].Visible = false;
                        grvPartidas.Columns[2].Visible = false;
                    }
                    if (sSaleValidation == "CambiarEstatusGeneral" || sSaleValidation == "CambiarEstatusGeneralSE" || sSaleValidation == "NOTVAL")
                    {
                        hiddenstores();
                    }
                }
                create_options_table();
                ProductsTableprueba = CreaTablaProductos();

                PreparaVenta();

                if (Request.QueryString["Search"] != null)
                    ObtenDatosVenta();

            }
        }


        protected void ImporteTextChanged(object sender, EventArgs e)
        {
            try
            {
                InvoiceBLL Invoice = new InvoiceBLL();
                TextBox txt = (TextBox)sender;
                GridViewRow row = (GridViewRow)txt.NamingContainer;
                TextBox importetxt = new TextBox();
                importetxt = (TextBox)row.FindControl("txtIngreso");
                int iColletIndex = row.RowIndex;
                decimal dImporteResta = 0, dcnt = 0;
                decimal dImportePago = 0;
                int ichange = 0;
                Label change = (Label)grvFormaPago.Rows[row.RowIndex].FindControl("lblChange");
                ichange = Convert.ToInt32(change.Text);

                Label lblchange = (Label)grvFormaPago.Rows[row.RowIndex].FindControl("lblTipocambio");
                decimal exchange = Convert.ToDecimal(lblchange.Text);


                if (ValImportText(importetxt, ichange, dImporteResta, dcnt, exchange))
                {
                    DataTable TablaDesglose = create_table_desglose();
                    foreach (GridViewRow row1 in grvFormaPago.Rows)
                    {
                        TextBox importtxt = new TextBox();
                        importtxt = (TextBox)row1.FindControl("txtIngreso");
                        Label lblchange1 = (Label)grvFormaPago.Rows[row1.RowIndex].FindControl("lblTipocambio");
                        decimal exchange1 = Convert.ToDecimal(lblchange1.Text);

                        if (importtxt != null && importtxt.Text != "")
                        {
                            decimal diimport = Convert.ToDecimal(importtxt.Text.Replace('$', ' ')) * exchange;

                            if (Convert.ToInt32(drpMoneda.SelectedValue) == 110)
                            {
                                diimport = diimport / exchange;
                            }

                            dImportePago += diimport;

                            if (diimport > 0)
                            {
                                DataRow rows = TablaDesglose.NewRow();

                                rows["Descripcion"] = grvFormaPago.DataKeys[row1.RowIndex].Values["Descripcion"];
                                rows["Ingreso"] = importtxt.Text;

                                TablaDesglose.Rows.Add(rows);
                            }
                        }
                    }

                    txtPagoImporte.Text = Convert.ToString(dImportePago.ToString());
                    lblTotalpagotkt.Text = Convert.ToString(dImportePago.ToString());
                    dCambio = Convert.ToDecimal(txtPagoImporte.Text.Replace('$', ' ')) - Convert.ToDecimal(txtTotalPago.Text.Replace('$', ' '));
                    HdnImportePago.Value = dImportePago.ToString();

                    if (dCambio > 0)
                    {
                        txtCambiopago.Text = dCambio.ToString();
                        lblCambiotkt.Text = dCambio.ToString();
                    }

                    else
                    {
                        txtCambiopago.Text = "0.00";
                        lblCambiotkt.Text = "0.00";
                    }
                    dImporteResta = Convert.ToDecimal(txtTotalPago.Text.Replace('$', ' ')) - Convert.ToDecimal(txtPagoImporte.Text.Replace('$', ' '));

                    if (dImporteResta > 0)
                    {
                        txtRestaPago.Text = dImporteResta.ToString();
                    }
                    else
                    {
                        txtRestaPago.Text = "0.00";
                    }

                    if (dImporteResta <= 0)
                    {
                        foreach (GridViewRow row2 in grvFormaPago.Rows)
                        {
                            TextBox ingresotxt = new TextBox();
                            ingresotxt = (TextBox)row2.FindControl("txtIngreso");

                            LinkButton optionAgregar = new LinkButton();
                            optionAgregar = (LinkButton)row2.FindControl("lblAdd");

                            if (ingresotxt != null)
                            {
                                if (ingresotxt.Text != "")
                                {
                                    if (Convert.ToDecimal(ingresotxt.Text.Replace('$', ' ')) > 0)
                                    {
                                        ingresotxt.Enabled = true;
                                        optionAgregar.Enabled = true;
                                    }
                                    else
                                    {
                                        ingresotxt.Enabled = false;
                                        optionAgregar.Enabled = false;
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        foreach (GridViewRow row2 in grvFormaPago.Rows)
                        {
                            TextBox ingresotxt = new TextBox();
                            ingresotxt = (TextBox)row2.FindControl("txtIngreso");

                            LinkButton optionAgregar = new LinkButton();
                            optionAgregar = (LinkButton)row2.FindControl("lblAdd");

                            if (ingresotxt != null)
                            {
                                ingresotxt.Enabled = true;
                                optionAgregar.Enabled = true;
                            }
                        }
                    }
                    GridViewTool.Bind(TablaDesglose, grvDesglose);
                }
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }

        }
        protected void Caracteristicas_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = new TextBox();
                LinkButton addbtn = (LinkButton)sender;

                int selected_index, index_table, quantity, partialqty = 0;

                if (TempCharTable.Rows.Count > 0)
                    TempCharTable.Columns.Add("Cantidad");

                selected_index = Convert.ToInt32(addbtn.CommandArgument);

                txt = (TextBox)GrvProductos.Rows[selected_index].FindControl("checkTextBox");

                quantity = Convert.ToInt32(txt.Text);

                index_table = 0;

                if (index_table > 0)
                {
                    if (partialqty < quantity)
                    {
                        messagelbl.Text = "La partida aun tiene articulos por asignar";

                        bFlagCharacteristics = false;

                        get_char_table(selected_index);

                        messagemdl.Show();
                    }
                    else
                    {
                        LlenaTablaCaracteristicas();
                    }
                }
                else
                {
                    messagelbl.Text = "No es posible pedir mas de lo seleccionado";

                    bFlagCharacteristics = false;

                    get_char_table(selected_index);

                    messagemdl.Show();
                }

                charmdl.Hide();
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }
        protected void ImporteCambioTextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                GridViewRow row = (GridViewRow)txt.NamingContainer;
                TextBox importetxt = new TextBox();
                importetxt = (TextBox)row.FindControl("txtIngreso");
                int iColletIndex = row.RowIndex;

                decimal dcnt = 0, dImporteCambio = 0;
                Label lblchange = (Label)grvCambio.Rows[row.RowIndex].FindControl("lblTipocambio");
                decimal exchange = Convert.ToDecimal(lblchange.Text);

                if (ValidacionImporteCambio(importetxt, exchange, dcnt))
                {
                    foreach (GridViewRow row1 in grvCambio.Rows)
                    {
                        if (importetxt != null && importetxt.Text != "")
                        {
                            decimal diimport = Convert.ToDecimal(importetxt.Text.Replace('$', ' ')) * exchange;

                            if (Convert.ToInt32(drpMoneda.SelectedValue) == 110)
                            {
                                diimport = diimport / dTipoCambio;
                            }

                            dImporteCambio += diimport;
                        }
                    }
                }
                HdnImporteCambio.Value = dImporteCambio.ToString();
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }
        private bool ValidacionImporteCambio(TextBox importetxt, decimal exchange, decimal dcnt)
        {
            InvoiceBLL Invoice = new InvoiceBLL();

            if (importetxt.Text != "")
            {
                if (validaImporte(importetxt) == 1)
                {
                    decimal resultado = (Convert.ToDecimal(importetxt.Text.Replace('$', ' ')) * exchange);

                    if (Convert.ToInt32(drpMoneda.SelectedValue) == 110)
                    {
                        resultado = resultado / dTipoCambio;
                    }

                    if (resultado > dCambio)
                    {
                        importetxt.Text = "$0.00";
                        LabelTool.ShowLabel(lblMensajeCambio, "El cambio es incorrecto!!!", System.Drawing.Color.DarkRed);
                        return false;
                    }
                    else
                    {
                        importetxt.Text = "$" + importetxt.Text;
                        LabelTool.HideLabel(lblMensajeCambio);
                    }
                }

                else
                {
                    LabelTool.ShowLabel(lblMensajeCambio, "Introduzca un Importe Válido!!!", System.Drawing.Color.DarkRed);
                    importetxt.Text = "$0.00";
                    return false;
                }
                return true;

            }
            else
            {
                importetxt.Text = "$0.00";
                return true;
            }
        }
        protected void DeleteClick(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                GridViewRow row = (GridViewRow)txt.NamingContainer;
                TextBox importetxt = new TextBox();
                importetxt = (TextBox)row.FindControl("txtIngreso");

                if (importetxt.Text != "")
                {
                    importetxt.Text = "";

                }
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }
        protected void Acept_Click(object sender, EventArgs e)
        {
            messagemdl.Hide();


        }
        protected void aceptclickok(object sender, EventArgs e)
        {
            try
            {
                ImprimeCotizacion();
                printmdl.Hide();
                confirmmdl.Show();
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void lnkRefresca_Click(object sender, EventArgs e)
        {

        }
        protected void Refresca()
        {

        }
        protected void linkRefresh_Click(object sender, EventArgs e)
        {
            PrintDocument(null, null);
        }
        protected void linkTerminaProceso_Click(object sender, EventArgs e)
        {
            TerminaProceso();

        }
        protected void AceptClick(object sender, EventArgs e)//msj al guardar cotizacion: Venta guardada satisfactoriamente
        {
            try
            {
                ImprimeCotizacion();

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "Error", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void quantitytxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow Row;
                TextBox txt = new TextBox();

                int index;

                txt = (TextBox)sender;
                Row = (GridViewRow)txt.NamingContainer;

                index = Row.RowIndex;

                SaleBLL sale = new SaleBLL();

                if ((TempCharTable = sale.GetCharacteristics(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(TempProductsTable.Rows[index][1]),
                                                         Convert.ToInt32(TempProductsTable.Rows[index][2]),
                                                         1))
                                                         == null)
                {
                }
                else
                {
                    if (TempCharTable.Rows.Count > 0)
                    {
                        charlbl.Text = TempProductsTable.Rows[index][6].ToString();
                        GridViewTool.Bind(TempCharTable, chargrv);

                        charmdl.Show();

                        charbtn.CommandArgument = index.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void charimgbtn_Click(object sender, EventArgs e)
        {
            charmdl.Hide();
        }

        protected void grvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grvClientes, "Select$" + e.Row.RowIndex);
            }
        }

        protected void cancelacionFacturas_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "FacturacionCancelacion2.aspx?Client=" + HdnCliente.Value.ToString() + "&Company=" + HdnEmpresa.Value.ToString() + "&Person=" + HdnUsuario.Value.ToString();

                Response.Redirect(url);
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void FacturaDocumentos_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "FacturacionVentas.aspx?Client=" + HdnCliente.Value.ToString();


                Response.Redirect(url);
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void storeDropDownList_SelectedIndexChanged(object sender, EventArgs e)//ya
        {
            try
            {
                DropDownList Drp = (DropDownList)sender;
                GridViewRow Row = (GridViewRow)Drp.NamingContainer;
                SaleBLL Sale = new SaleBLL();
                int ifam = 1;
                sProductosNoValidos = "";
                Stores = (string[])Session[_Stores];
                Stores[Row.RowIndex] = Drp.SelectedValue;
                Row.Cells[1].Text = Sale.GetStockStore(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(Drp.SelectedValue), Convert.ToInt32(ProductsTable.Rows[Row.RowIndex][1]), Convert.ToInt32(ProductsTable.Rows[Row.RowIndex][2]), ifam).ToString();
                ProductsTable.Rows[Row.RowIndex][7] = Drp.SelectedValue;
                Session[_Stores] = Stores;

                int office = Convert.ToInt32(Drp.SelectedValue);

                iValueReference = ObtenCostoReferencia(office);

                if (iValueReference == 2 || iValueReference == 0)
                {
                    sProductosNoValidos = sProductosNoValidos + (ProductsTable.Rows[Row.RowIndex][9]) + ", ";

                }
                if (sProductosNoValidos != "")
                {
                    LabelTool.ShowLabel(lblError, "El Producto: " + sProductosNoValidos + " no cuenta con costo de referencia en la zona del almacén seleccionado!!!", System.Drawing.Color.DarkRed);
                }
                else
                {
                    LabelTool.HideLabel(lblError);
                }
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void storeDropDownListProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList Drp = (DropDownList)sender;
                GridViewRow Row = (GridViewRow)Drp.NamingContainer;
                SaleBLL Sale = new SaleBLL();
                int ifam = 1;
                Stores = (string[])Session[_Stores];
                Stores[Row.RowIndex] = Drp.SelectedValue;
                Row.Cells[4].Text = Sale.GetStockStore(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(Drp.SelectedValue), Convert.ToInt32(TempProductsTable.Rows[Row.RowIndex][1]), Convert.ToInt32(TempProductsTable.Rows[Row.RowIndex][2]), ifam).ToString();
                Session[_Stores] = Stores;

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void RdBtnListProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (RdBtnListProductos.SelectedIndex)
            {
                case 1:
                    {

                        INTEGRA.Global.Product_type = 112;

                        break;
                    }

                default:

                    INTEGRA.Global.Product_type = 113;

                    break;
            }
            txtProductos.Focus();
        }

        protected void txtProductos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewTool.Bind(null, GrvProductos);
                GridViewTool.Bind(null, chargrv);

                if ((TempProductsTable = get_products_concept()) == null)
                {
                    if (HdnCliente.Value == "")
                    {
                        LabelTool.ShowLabel(lblError, "Ingrese un cliente", System.Drawing.Color.DarkRed);
                    }
                }
                else
                {
                    if (TempProductsTable.Rows.Count > 0)
                    {
                        LabelTool.HideLabel(lblError);
                        AsignaPreciosdeLista();

                        TempProductsTable = ObtienePrecioConImpuestosSinIVA(TempProductsTable);
                        TempProductsTable = ObtieneIVAPorProducto(TempProductsTable);

                        if (sSaleValidation.IndexOf("CambiarEstatusGeneral") == -1)
                        {
                            if (sSaleValidation != "NOTVAL" && sSaleValidation != "AgregarPartidaSE" && sSaleValidation != "CambiarEstatusPartidaSE")// regresar
                            {
                                check_products_table();
                                int index = 0;
                                GridViewTool.Bind(TempProductsTable, GrvProductos);
                                add_Totals_columns_products(ref index);
                                GridViewTool.Bind(TempProductsTable, GrvProductos);
                                fill_store_controls_productsgrid(index);//llena los drops

                                productsmdl.Show();
                            }

                            else if (sSaleValidation == "AgregarPartidaSE" || sSaleValidation == "CambiarEstatusPartidaSE")// regresar
                            {
                                int index = 0;
                                GridViewTool.Bind(TempProductsTable, GrvProductos);
                                add_Totals_columns_products(ref index);
                                GridViewTool.Bind(TempProductsTable, GrvProductos);
                                fill_store_controls_productsgrid(index);//llena los drops
                                productsmdl.Show();
                            }

                            else if (sSaleValidation == "NOTVAL")
                            {
                                int index = 0;
                                add_Totals_columns_products(ref index);
                                GridViewTool.Bind(TempProductsTable, GrvProductos);
                                visibledrops();
                                productsmdl.Show();
                            }
                        }
                        else
                        {
                            int index = 0;
                            add_Totals_columns_products(ref index);
                            GridViewTool.Bind(TempProductsTable, GrvProductos);
                            visibledrops();
                            productsmdl.Show();

                        }
                    }
                    else
                    {
                        keycontainer.Value = "";
                        LimpiaControlTxtProductos();
                        LabelTool.ShowLabel(lblError, "El producto no se encuentra en la lista", System.Drawing.Color.DarkRed);

                    }
                }
                keycontainer.Value = "";
                LimpiaControlTxtProductos();
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void Cliente_Changed(object sender, EventArgs e)
        {
            try
            {
                CustomerBLL Customer = new CustomerBLL();
                DataTable dtCliente;
                int idCliente;

                if (keycontainer.Value == "")
                {
                    idCliente = 0;
                }
                else
                {
                    idCliente = Convert.ToInt32(keycontainer.Value);
                    HdnCliente.Value = Convert.ToString(idCliente);

                    using (dtCliente = Customer.GetPersonData(Convert.ToInt32(HdnEmpresa.Value), idCliente))
                    {
                        if (dtCliente != null && dtCliente.Rows.Count > 0)
                        {
                            AsignaDatosCliente(dtCliente);
                            ObtieneDireccionCliente();
                        }
                        else
                        {
                            LabelTool.ShowLabel(lblError, "Error al conseguir los datos del cliente!!!", System.Drawing.Color.DarkRed);
                        }
                    }
                }

                txtCliente.Text = "";

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void CurrencyChanged(object sender, EventArgs e)
        {
            try
            {
                dTempExchange = dExchange;

                if ((dExchange = ControlsBLL.GetExchange(Convert.ToInt32(HdnEmpresa.Value), 109, Convert.ToInt32(drpMoneda.SelectedValue), DateTime.Now)) == -1)
                {
                    LabelTool.ShowLabel(lblError, "Error al conseguir el tipo de cambio!!!", System.Drawing.Color.DarkRed);
                    dExchange = dTempExchange;
                }

                change_quote_price();
                GetAmounts();

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void IVAChanged(object sender, EventArgs e)
        {
            try
            {
                CambiaPorcentajeIVA();
                GetAmounts();

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void chkEntregaDirectaChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
                iDirectDelivery = 1;
            else
                iDirectDelivery = 0;

        }

        protected void CloseInvoice(object sender, EventArgs e)
        {
            invoicemdl.Hide();
        }


        protected void grvPartidas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = grvPartidas.SelectedIndex;

                if (iNumeroVenta.Value == "0" || Convert.ToInt32(ProductsTable.Rows[grvPartidas.SelectedIndex][12]) == 0)
                {
                    DeleteStoresItem(index);
                    ProductsTable.Rows.RemoveAt(index);
                    GridViewTool.Bind(ProductsTable, grvPartidas);
                }
                else
                {
                    delete_gamequote(index, Convert.ToInt32(iNumeroVenta.Value));
                }

                if (sSaleValidation.IndexOf("Partida") > 0)
                {
                    if (ProductsTable.Rows.Count > 0)
                    {
                        get_stores_for_gamequote();
                    }
                    StoresControls();
                }
                GridViewTool.CleanGridViewSelection(grvPartidas);
                CheckCommandControls();
                GetAmounts();

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void ConfirmChanged(object sender, EventArgs e)
        {
            try
            {
                SaleBLL sale = new SaleBLL();
                llenaDropsDocumentos();

                if (drpDocumento.SelectedItem != null)
                {
                    RadioButtonList rdbtn = (RadioButtonList)sender;

                    confirmmdl.Hide();

                    if (rdbtn.SelectedIndex == 0)
                    {
                        mdlFormaPago.Y = Convert.ToInt32(container.Value);
                        mdlFormaPago.Show();
                    }
                    else
                    {
                        invoicemdl.Show();
                    }
                }
                else
                {
                    messagelbl.Text = "Este usuario no esta autorizado a cerrar una venta!!!";
                    messagemdl.Show();
                }
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void ConfirmClick(object sender, EventArgs e)
        {
            try
            {
                SaleBLL Sale = new SaleBLL();
                DataTable TablaEmpresa = new DataTable();
                DataTable tempPayForms = new DataTable();
                DataTable DataNumero = Sale.GetNumeroFactura(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));//
                int iNumeroFactura = Convert.ToInt32(DataNumero.Rows[0][0]);

                confirmmdl.Hide();
                LabelTool.HideLabel(cmessagelbl);
                grvFormaPago.Enabled = true;
                LbtnPay.Enabled = true;

                if ((TablaEmpresa = DatosEmpresa()) != null)    //Obtiene datos de la empresa
                {
                    if (TablaEmpresa.Rows.Count > 0)
                    {
                        companypaylbl.Text = Convert.ToString(TablaEmpresa.Rows[0][1]);
                        addresdpaylbl.Text = Convert.ToString(TablaEmpresa.Rows[0][2]);
                        coloniapaylbl.Text = Convert.ToString(TablaEmpresa.Rows[0][3]);
                        delegacionpaylbl.Text = Convert.ToString(TablaEmpresa.Rows[0][4]);
                        cppaylbl.Text = "C.P." + " " + Convert.ToString(TablaEmpresa.Rows[0][6]) + "  " + "Tel." + Convert.ToString(TablaEmpresa.Rows[0][8]);
                    }
                }

                tempPayForms = Sale.GetPayForms(Convert.ToInt32(HdnEmpresa.Value), iNumeroFactura);//NUMERO COTIZACION

                sbttaltktlbl.Text = txtSubtotal.Text;
                ivatktdlbl.Text = txtIva.Text;
                totaltktdlbl.Text = txtTotal.Text;
                txtTotalPago.Text = txtTotal.Text;
                txtPagoImporte.Text = "$0.00";
                txtCambiopago.Text = "$0.00";
                txtRestaPago.Text = txtTotal.Text;
                lblTotalpagotkt.Text = "$0.00";
                lblCambiotkt.Text = "$0.00";

                doctopaydrp.Text = Convert.ToString(drpDocumento.SelectedItem.Text);
                seriepaydrp.Text = Convert.ToString(drpSerie.SelectedItem.Text);
                numdoctopaytxt.Text = Convert.ToString(txtNumeroDocto.Text);
                clientpaytxt.Text = Convert.ToString(lblNombrecliente.Text);
                currencypaytxt.Text = Convert.ToString(drpMoneda.SelectedItem.Text);
                GridViewTool.Bind(tempPayForms, grvFormaPago);
                GridViewTool.Bind(ProductsTable, productspayGvw);//Llena el grid del ticket
                GridViewTool.Bind(null, grvDesglose);
                // =======================================
                DataRow[] customerRow = TablaEmpresa.Select("Estado IS NOT NULL");
                customerRow[0]["Estado"] = numdoctopaytxt.Text.Trim();


                HttpContext.Current.Session["dtEmpresa"] = TablaEmpresa.Copy();

                ProductsTable.Columns.Add("Iva", typeof(decimal));
                ProductsTable.Columns.Add("TotalTotal", typeof(decimal));
                ProductsTable.Columns.Add("Subtotal", typeof(decimal));
                int numerofilas = ProductsTable.Rows.Count;
                for (int i = 0; i < numerofilas; i++)
                {

                    ProductsTable.Rows[i]["Iva"] = ivatktdlbl.Text.Remove(0, 1);
                    ProductsTable.Rows[i]["TotalTotal"] = totaltktdlbl.Text.Remove(0, 1);
                    ProductsTable.Rows[i]["Subtotal"] = sbttaltktlbl.Text.Remove(0, 1);
                }

                HttpContext.Current.Session["dtProducto"] = ProductsTable.Copy();
                mdlFormaPago.Show();//Modal de Formas de pago


            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void AgregaOpcionPago_Click(object sender, EventArgs e)
        {
            try
            {
                int valChk = 0;
                GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

                valChk = row.RowIndex;
                DataTable tablanewoption = new DataTable();

                tablanewoption.Columns.Add("Numero", typeof(int));
                tablanewoption.Columns.Add("Mas", typeof(string));
                tablanewoption.Columns.Add("Descripcion", typeof(string));
                tablanewoption.Columns.Add("Ingreso", typeof(string));
                tablanewoption.Columns.Add("Referencia", typeof(string));
                tablanewoption.Columns.Add("TC", typeof(decimal));
                tablanewoption.Columns.Add("Clas_Moneda", typeof(int));
                tablanewoption.Columns.Add("Cambio", typeof(int));

                foreach (GridViewRow row1 in grvFormaPago.Rows)
                {
                    DataRow dr = tablanewoption.NewRow();

                    Label lbl = (Label)grvFormaPago.Rows[row1.RowIndex].FindControl("lblNumber");
                    dr["Numero"] = Convert.ToInt32(lbl.Text);

                    LinkButton lkbtnadd = (LinkButton)grvFormaPago.Rows[row1.RowIndex].FindControl("lblAdd");
                    dr["Mas"] = Convert.ToString(lkbtnadd.Text);

                    dr["Descripcion"] = Server.HtmlDecode(row1.Cells[2].Text);

                    TextBox drp = (TextBox)grvFormaPago.Rows[row1.RowIndex].FindControl("txtIngreso");
                    dr["Ingreso"] = drp.Text.Replace('$', ' ');

                    TextBox refe = (TextBox)grvFormaPago.Rows[row1.RowIndex].FindControl("txtReferencia");
                    dr["Referencia"] = refe.Text;

                    Label lbltc = (Label)grvFormaPago.Rows[row1.RowIndex].FindControl("lblTipocambio");
                    dr["TC"] = lbltc.Text;

                    Label lblcurrency = (Label)grvFormaPago.Rows[row1.RowIndex].FindControl("lblMoneda");
                    dr["Clas_Moneda"] = Convert.ToInt32(lblcurrency.Text);

                    Label lblchange = (Label)grvFormaPago.Rows[row1.RowIndex].FindControl("lblChange");
                    dr["Cambio"] = Convert.ToInt32(lblchange.Text);

                    tablanewoption.Rows.Add(dr);

                }

                LinkButton lkbtnnew = (LinkButton)grvFormaPago.Rows[grvFormaPago.Rows[valChk].RowIndex].FindControl("lblAdd");

                if (lkbtnnew.Text == "+")
                {

                    DataRow drnew = tablanewoption.NewRow();
                    Label lblnew = (Label)grvFormaPago.Rows[grvFormaPago.Rows[valChk].RowIndex].FindControl("lblNumber");
                    drnew["Numero"] = Convert.ToInt32(lblnew.Text);

                    drnew["Descripcion"] = Server.HtmlDecode(grvFormaPago.Rows[valChk].Cells[2].Text);

                    TextBox drp = (TextBox)grvFormaPago.Rows[grvFormaPago.Rows[valChk].RowIndex].FindControl("txtIngreso");
                    drnew["Ingreso"] = "0.00";

                    Label lbltc = (Label)grvFormaPago.Rows[grvFormaPago.Rows[valChk].RowIndex].FindControl("lblTipocambio");
                    drnew["TC"] = Convert.ToDecimal(lbltc.Text);

                    Label lblcurrency = (Label)grvFormaPago.Rows[grvFormaPago.Rows[valChk].RowIndex].FindControl("lblMoneda");
                    drnew["Clas_Moneda"] = Convert.ToInt32(lblcurrency.Text);

                    Label lblchange = (Label)grvFormaPago.Rows[grvFormaPago.Rows[valChk].RowIndex].FindControl("lblChange");
                    drnew["Cambio"] = Convert.ToInt32(lblchange.Text);

                    drnew["Mas"] = "-";

                    tablanewoption.Rows.Add(drnew);
                }
                else
                {
                    tablanewoption.Rows.RemoveAt(valChk);
                }
                DataView dv = tablanewoption.DefaultView;
                dv.Sort = "Numero asc";
                DataTable sortedDT = dv.ToTable();
                GridViewTool.Bind(sortedDT, grvFormaPago);
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void NoConfirmClick(object sender, EventArgs e)
        {
            confirmmdl.Hide();
        }

        protected void PagoSinTransaccionClick(object sender, EventArgs e)
        {
            try
            {
                int iNumeroFactura = 0;
                SaleBLL Sale = new SaleBLL();
                DataTable DataNumero = Sale.GetNumeroFactura(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));//
                if (DataNumero.Rows.Count > 0)
                {
                    iNumeroFactura = Convert.ToInt32(DataNumero.Rows[0][0]);
                }
                validaExisteCambio(iNumeroFactura);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void CloseSaleMessage(object sender, EventArgs e)
        {
            try
            {
                SaleBLL Sale = new SaleBLL();
                salemsgmdl.Hide();

                if (Convert.ToInt32(iNumeroVenta.Value) > 0)
                {
                    lnkPagarVenta.Visible = true;

                    if (Sale.GetStatusNumberFromSaleNumber(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value)) == 10)
                    {
                        lnkImprimirDocumento.Text = "Imprimir cotización";
                        grvPartidas.Enabled = true;
                        lnkCerrarVenta.Visible = true;
                        lnkGuardarCotizacion.Visible = true;

                        ManageControls(true);
                    }
                    else
                    {
                        lnkImprimirDocumento.Text = "Imprimir factura";
                        grvPartidas.Enabled = false;
                        lnkCerrarVenta.Visible = false;
                        lnkGuardarCotizacion.Visible = false;

                        ManageControls(false);
                    }
                }
                LabelTool.HideLabel(lblError);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void creaFactura_Click(object sender, EventArgs e)
        {
            bool bandera = false;

            try
            {
                InvoiceBLL Invoice = new InvoiceBLL();
                SaleBLL Sale = new SaleBLL();
                bool bCreacion, continuar;
                continuar = true;

                DataTable DataCorreoContactos = new DataTable();
                int ValidaFolio = Invoice.CheckFolio(Convert.ToInt32(Session["Company"]), 0, Convert.ToInt32(drpDocumento.SelectedValue), Convert.ToInt32(drpSerie.SelectedValue), Convert.ToInt32(txtNumeroDocto.Text));

                if (ValidaFolio == 0)
                {
                    if (drpDocumento.SelectedValue == "-59")
                    {
                        // sirve para traer el 'importe'total en letra 
                        HttpContext.Current.Session["dropMonedaText"] = drpMoneda.SelectedItem.Text;

                        if (ValidacionDatos())
                        {
                            DataTable DataInternet = DatosDoctoInternet();

                            if (DataInternet != null)
                            {
                                DataCorreoContactos = DatosCorreoE();


                                if (DataCorreoContactos != null && DataCorreoContactos.Rows.Count > 0)
                                {
                                    if (Convert.ToString(DataCorreoContactos.Rows[0][0]) == "")
                                    {
                                        LabelTool.ShowLabel(lblError, "Error, no se ha encontrado correo electronico de los contactos del cliente", System.Drawing.Color.DarkRed);
                                        continuar = false;
                                    }
                                    else
                                    {
                                        LabelTool.HideLabel(lblError);

                                        continuar = true;
                                    }
                                }
                                else
                                {
                                    LabelTool.ShowLabel(lblError, "Error, no se ha encontrado correo electrónico de los contactos del cliente", System.Drawing.Color.DarkRed);
                                    continuar = false;
                                }
                            }

                            if (continuar == true)
                            {

                                if (ValidacionDatosMailCompany())
                                {
                                    if (CierraVenta())
                                    {
                                        if (CreaFactura())
                                        {
                                            //Obtener numero de factura y Buscar si integra hace el docto electronico
                                            DataTable DataNumero = Sale.GetNumeroFactura(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));//
                                            int iNumeroFactura = Convert.ToInt32(DataNumero.Rows[0][0]);
                                            //DataTable DataDocto = Invoice.GetEDocto(Convert.ToInt32(HdnEmpresa.Value), iNumeroFactura);//
                                            DataTable dtContabilidad = VentasController.realizaContabilidad(1, Convert.ToInt32(HdnEmpresa.Value), iNumeroFactura);
                                            //
                                            //VALIDA E-MAIL
                                            if (ValidacionDatosElectronico(iNumeroFactura))
                                            {

                                                FacturaServicio.Servicio sf = new FacturaServicio.Servicio();
                                                string sresultado = sf.gfs_generaComprobanteFisica(iNumeroFactura, 0, INTEGRA.Global.DataBase,
                                                                                                   Convert.ToInt32(Session["Company"]), Convert.ToInt32(HdnCliente.Value), "CONTACTOS");


                                                string[] asresultado = sresultado.Split('|');
                                                string stempA = asresultado[0];
                                                string sTempB = asresultado[1];
                                                string sTempC = asresultado[2];
                                                if (stempA.Split(':')[1].ToUpper() == "OK")
                                                {
                                                    LabelTool.ShowLabel(lblError, sresultado, System.Drawing.Color.DarkRed);
                                                    new FacturacionCancelacionController().gfFactura_Tienda(Convert.ToInt32(Session["Company"].ToString()), Convert.ToInt32(iNumeroFactura), sTempB.Split(':')[1]);
                                                }
                                                else
                                                    throw new Exception("* Incidencia " + sTempC.Split(':')[1]);



                                                string sTempFile = @WebConfigurationManager.AppSettings["sfolderRaiz"].ToString() + sTempC.Split(':')[2];
                                                string sRes = CFDIProgram.gfComprimirFichero(sTempFile);
                                                Session["ArchivoZip"] = sRes;
                                                sTempFile = sTempFile.Replace(@"\", "/");
                                                sTempFile = sTempFile.ToUpper();

                                                sTempFile = sTempFile.Replace(@WebConfigurationManager.AppSettings["sfolderNameDestino2"].ToString(), @WebConfigurationManager.AppSettings["GMCFacturas"].ToString());
                                                sTempFile = sTempFile.Replace(@WebConfigurationManager.AppSettings["sfolderNameDestino"].ToString(), @WebConfigurationManager.AppSettings["GMCFacturas"].ToString());

                                                Session["Rutaelectronico"] = sTempFile;

                                                LabelTool.ShowLabel(lblError, "Factura electrónica generada exitosamente", System.Drawing.Color.DarkRed);
                                                lnkPagarVenta.Visible = true;
                                                confirmmdl.Show();


                                            }

                                            ManageControls(false);
                                            ManageCommandControls(false);
                                            invoicemdl.Hide();

                                        }
                                        else
                                        {
                                            invoicemdl.Hide();
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfRefresca();", true);


                                        }
                                    }
                                    else
                                    {
                                        if (sProductosNoValidos == "")
                                            LabelTool.ShowLabel(lblError, "Error al cerrar la venta, intente nuevamente", System.Drawing.Color.DarkRed);

                                        else if (sProductosNoValidos != "")
                                            LabelTool.ShowLabel(lblError, "Los Producto(s): " + sProductosNoValidos + " no cuenta con costo de referencia en la zona del almacén seleccionado, seleccione otro almacén", System.Drawing.Color.DarkRed);
                                        else
                                            LabelTool.ShowLabel(lblError, "Error al cerrar la venta, intente nuevamente", System.Drawing.Color.DarkRed);
                                    }
                                }
                            }
                        }
                    }
                    else                  ////////////////////// si es NOTA DE CREDITO /////////////////////////////////////////
                    {
                        if (CierraVenta())
                        {
                            if (CreaFactura()) ///////////genera factura
                            {

                                int iNumeroFactura = 0;
                                DataTable DataNumero = Sale.GetNumeroFactura(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));//
                                if (DataNumero != null && DataNumero.Rows.Count > 0)
                                {
                                    iNumeroFactura = Convert.ToInt32(DataNumero.Rows[0][0]);
                                }


                                if (iNumeroFactura > 0)
                                {
                                    //REALIZA LA CONTABILIDAD
                                    DataTable dtContabilidad = VentasController.realizaContabilidad(1, Convert.ToInt32(HdnEmpresa.Value), iNumeroFactura); //cambia estatus a contabilizada

                                    /////////////////manda a timbrar
                                    FacturaServicio.Servicio sf = new FacturaServicio.Servicio();
                                    string sresultado = sf.gfs_generaComprobanteFisica(iNumeroFactura, 1, INTEGRA.Global.DataBase,
                                                                                       Convert.ToInt32(Session["Company"]), Convert.ToInt32(HdnCliente.Value), "CONTACTOS");


                                    string[] asresultado = sresultado.Split('|');
                                    string stempA = asresultado[0];
                                    string sTempB = asresultado[1];
                                    string sTempC = asresultado[2];

                                    if (stempA.Split(':')[1].ToUpper() == "OK")
                                    {
                                        LabelTool.ShowLabel(lblError, sresultado, System.Drawing.Color.DarkRed);
                                        new FacturacionCancelacionController().gfFactura_Tienda(Convert.ToInt32(Session["Company"].ToString()), Convert.ToInt32(iNumeroFactura), "");
                                    }
                                    else
                                    {
                                        throw new Exception("* Incidencia " + sTempC.Split(':')[1]);
                                    }

                                }

                                ManageControls(false);
                                ManageCommandControls(false);
                                lnkPagarVenta.Visible = true;
                                invoicemdl.Hide();
                                LabelTool.ShowLabel(lblError, "Venta " + iNumeroVenta.Value + " generada exitosamente!!!", System.Drawing.Color.DarkRed);
                                confirmmdl.Show();

                            }
                        }
                        else
                        {
                            if (sProductosNoValidos == "" && sProductosNoValidos != null)
                                LabelTool.ShowLabel(lblError, "Error al cerrar la venta, intente nuevamente", System.Drawing.Color.DarkRed);
                            else if (sProductosNoValidos != "" && sProductosNoValidos != null)

                                LabelTool.ShowLabel(lblError, "Los Producto(s): " + sProductosNoValidos + " no cuenta con costo de referencia en la zona del almacén seleccionado, seleccione otro almacén", System.Drawing.Color.DarkRed);
                            else
                                LabelTool.ShowLabel(lblError, "Error al cerrar la venta, intente nuevamente", System.Drawing.Color.DarkRed);

                        }
                    }
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "El número de folio ya esta en uso, intente nuevamente", System.Drawing.Color.DarkRed);

                }

            }
            catch (Exception ex)
            {

                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                LOG.gfLog(HttpContext.Current.Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), "creaFactura_click", ref lblError);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                invoicemdl.Hide();
            }
            finally
            {

                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfRefresca();", true);
                invoicemdl.Hide();
            }
        }

        protected void CerrarVenta_Click(object sender, EventArgs e)
        {
            try
            {

                if (HdnValorIncorrecto.Value == "0" && lblError.Text != "La cantidad ingresada no es válida" && lblError.Text != "La cantidad ingresada supera el disponible del almacén")
                {

                    if (ProductsTable != null && ProductsTable.Rows.Count > 0)
                    {
                        bClose = true;
                        SaleBLL Sale = new SaleBLL();
                        invoicebtnl.Enabled = true;
                        iExistProduct = 0;

                        if (sSaleValidation.IndexOf("CambiarEstatusGeneral") == 0)
                        {
                            foreach (GridViewRow row in grvPartidas.Rows)
                            {
                                if (Sale.IsProduct(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][1])))
                                {
                                    iExistProduct++;
                                }
                            }
                            if (iExistProduct > 0)
                            {
                                DropTool.FillDrop(generalstoreddl, get_stores(), "Descripcion", "Numero");
                                generalstoremdl.Show();
                            }

                            else// Cerrar la venta
                            {
                                int ifamily;
                                int ifather;
                                ifamily = 3;
                                ifather = 9;
                                int iStatus = Sale.GetStatusNumberFromDescription("Aceptada", ifamily, ifather);// haciendo

                                DataTable DeniedProductsTable;
                                generalstoremdl.Hide();
                                GuardaVenta(Convert.ToInt32(iNumeroVenta.Value), iStatus);

                                if (sSaleValidation == "CambiarEstatusGeneral" || sSaleValidation == "AgregarPartida" || sSaleValidation == "CambiarEstatusPartida")//puse
                                {
                                    DeniedProductsTable = GetProductMissedTable();

                                    for (int i = 0; i < ProductsTable.Rows.Count; i++)
                                    {
                                        if (Convert.ToInt32(ProductsTable.Rows[i][11]) == 113)
                                        {
                                            if (GetGeneralStock(i, Convert.ToInt32(generalstoreddl.SelectedValue)) <= 0)
                                            {
                                                DataRow ProductRow = DeniedProductsTable.NewRow();
                                                ProductRow["Product"] = ProductsTable.Rows[i][9];
                                                DeniedProductsTable.Rows.Add(ProductRow);
                                                DeniedProducts(i);
                                            }
                                        }
                                    }

                                    if (DeniedProductsTable.Rows.Count > 0)
                                    {
                                        ProductosSinExistencia(DeniedProductsTable);
                                        iStatus = Sale.GetStatusNumberFromDescription("Iniciada", ifamily, ifather);
                                        GuardaVenta(Convert.ToInt32(iNumeroVenta.Value), iStatus);
                                    }
                                    else
                                    {
                                        confirmmdl.Y = Convert.ToInt32(container.Value);
                                        confirmmdl.Show();//Desea realizar pago?
                                    }
                                }
                                else
                                {
                                    confirmmdl.Y = Convert.ToInt32(container.Value);
                                    confirmmdl.Show();//Desea realizar pago?
                                }
                            }
                        }
                        else
                        {
                            llenaDropsDocumentos();
                            invoicemdl.Show();
                        }
                    }
                }
                else
                {
                    LabelTool.ShowLabel(lblError, lblError.Text, System.Drawing.Color.DarkRed);
                }
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void GeneralStoreClick(object sender, EventArgs e)
        {
            try
            {
                SaleBLL Sale = new SaleBLL();

                if (bClose == false)
                {
                    int ifamily = 3;
                    int ifather = 9;
                    int iStatus = Sale.GetStatusNumberFromDescription("Iniciada", ifamily, ifather);


                    iNumeroVenta.Value = GuardaVenta(Convert.ToInt32(iNumeroVenta.Value), iStatus).ToString();

                    if (iNumeroVenta.Value != "0")
                    {
                        lblVenta.Text = "Venta " + iNumeroVenta.Value + " guardada satisfactoriamente!!!";

                    }
                    else
                    {
                        LabelTool.ShowLabel(lblError, "Error al insertar la venta, intente nuevamente!!", System.Drawing.Color.DarkRed);
                    }
                    salemsgmdl.Show();
                }
                else// Cerrar la venta
                {
                    DataTable DeniedProductsTable;
                    generalstoremdl.Hide();

                    DeniedProductsTable = GetProductMissedTable();


                    for (int i = 0; i < ProductsTable.Rows.Count; i++)
                    {
                        if (Sale.IsProduct(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(ProductsTable.Rows[i][1])))
                        {
                            if (GetGeneralStock(i, Convert.ToInt32(generalstoreddl.SelectedValue)) <= 0)
                            {
                                DataRow ProductRow = DeniedProductsTable.NewRow();
                                ProductRow["Product"] = ProductsTable.Rows[i][9];
                                DeniedProductsTable.Rows.Add(ProductRow);

                            }
                        }
                    }

                    if (DeniedProductsTable.Rows.Count > 0)
                    {
                        ProductosSinExistencia(DeniedProductsTable);
                    }
                    else
                    {
                        llenaDropsDocumentos();
                        invoicemdl.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void CloseConfirm(object sender, EventArgs e)
        {
            confirmmdl.Hide();
        }

        protected void Pagar_Click(object sender, EventArgs e)
        {
            try
            {
                int iNumeroFactura = 0;
                SaleBLL Sale = new SaleBLL();
                DataTable DataNumero = Sale.GetNumeroFactura(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));
                if (DataNumero.Rows.Count > 0)
                {
                    iNumeroFactura = Convert.ToInt32(DataNumero.Rows[0][0]);
                }

                bValidaDatosPago(iNumeroFactura);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        private bool bValidaDatosPago(int iNumeroFactura)
        {

            if (HdnImportePago.Value != "0")//valida si hay algo por pagar
            {
                //valida el llenado de los campos correctos
                foreach (GridViewRow row in grvFormaPago.Rows)
                {
                    TextBox referenciatxt = (TextBox)row.FindControl("txtReferencia");
                    TextBox importetxt = (TextBox)row.FindControl("txtIngreso");

                    if (Convert.ToDecimal(importetxt.Text.Replace('$', ' ')) > 0)
                    {
                        if (referenciatxt != null && referenciatxt.Text == "")
                        {
                            LabelTool.ShowLabel(cmessagelbl, "Debe de capturar la referencia del pago de cada uno de los conceptos", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }
            }

            else
            {
                LabelTool.ShowLabel(cmessagelbl, "No se ha capturado algún pago", System.Drawing.Color.DarkRed);
                return false;
            }

            return bValidaTransaccionPago(iNumeroFactura);
        }

        private bool bValidaTransaccionPago(int iNumeroFactura)
        {
            InvoiceBLL Invoice = new InvoiceBLL();
            DataTable ValidaTable;
            string ConceptoContable = "";
            string sMsjTransaccSinDefinir = "";
            decimal dingreso = 0;

            foreach (GridViewRow row in grvFormaPago.Rows)
            {
                TextBox ingresotxt = (TextBox)grvFormaPago.Rows[row.RowIndex].FindControl("txtIngreso");
                dingreso = Convert.ToDecimal(ingresotxt.Text.Replace('$', ' '));

                if (dingreso != 0)
                {
                    Label Numberlbl = (Label)row.FindControl("lblNumber");

                    int a = row.RowIndex;

                    ValidaTable = Invoice.ValidaConceptoContable(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroFactura), Convert.ToInt32(Numberlbl.Text));

                    if (ValidaTable != null)
                    {
                        ConceptoContable = Convert.ToString(ValidaTable.Rows[0][0]);

                        if (ConceptoContable != "")
                        {
                            sMsjTransaccSinDefinir = sMsjTransaccSinDefinir + "El concepto " + grvFormaPago.Rows[a].Cells[2].Text;

                        }
                    }
                }
            }

            if (sMsjTransaccSinDefinir != "")
            {
                lblMsjTransaccion.Text = sMsjTransaccSinDefinir;
                mdlTransaccion.Show();
                return false;
            }
            return validaExisteCambio(iNumeroFactura);
        }

        public bool validaExisteCambio(int iNumeroFactura)// VERIFICA SI HAY CAMBIO
        {
            if (dCambio > 0)
            {
                DataTable DataConcept = sp_Cuentas_Cobrar_TraeConcepto_Tienda_C(iNumeroFactura);
                GridViewTool.Bind(DataConcept, grvCambio);
                MontoCambiotxt.Text = txtCambiopago.Text;
                mdlCambio.Show();
            }
            else
            {
                //GUARDA PAGO
                if (GuardaDetallePago(iNumeroFactura))
                {
                    GuardaEncabezadoPago(iNumeroFactura);
                }
            }
            return true;
        }

        public bool GuardaDetallePago(int iNumeroFactura)
        {

            bool bresult = true;

            foreach (GridViewRow row in grvFormaPago.Rows)
            {
                Label lblchange = (Label)grvFormaPago.Rows[row.RowIndex].FindControl("lblTipocambio");
                decimal dTipoCambio = Convert.ToDecimal(lblchange.Text);
                TextBox ingresotxt = (TextBox)grvFormaPago.Rows[row.RowIndex].FindControl("txtIngreso");
                decimal dingreso = (Convert.ToDecimal(ingresotxt.Text.Replace('$', ' ')) * dTipoCambio);

                if (Convert.ToInt32(drpMoneda.SelectedValue) == 110)
                {
                    dingreso = dingreso / dTipoCambio;
                }

                if (dingreso > 0)
                {
                    bresult = bresult && CreatePay(row, iNumeroFactura);//inserta detalle pago
                }
            }

            return bresult;
        }

        public bool GuardaEncabezadoPago(int iNumeroFactura)
        {
            SaleBLL Sale = new SaleBLL();
            int itype;
            bool bresult = true;
            lblPagoRealizado.Text = "0";

            if (PayInvoiceProcess(iNumeroFactura))//inserta encabezado del pago  
            {
                lblPagoRealizado.Text = "1";
                LabelTool.ShowLabel(lblError, "Pago exitoso!!!", System.Drawing.Color.DarkRed);

                try
                {
                    //obtener folio_fiscal y update folio_fiscal
                    Sale.GenerarFolioFiscalPagar(iNumeroFactura, int.Parse(Session["Company"].ToString()));
                }
                catch
                {
                    lnkPagarVenta.Visible = false;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEscondeRefresh2();", true);
                }

                lnkPagarVenta.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEscondeRefresh2();", true);

            }

            else
            {
                itype = 4;
                PayInvoice(itype, null, iNumeroFactura);
                LabelTool.ShowLabel(cmessagelbl, "Error al realizar el pago, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                bresult = false;
            }
            return bresult;
        }

        public void GeneraFacturaDescarga(string sRuta)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Window1", "<script> window.open('" + sRuta + "',''," +
            "'top=350,left=400,height=400,width=500,status=yes,toolbar=no,menubar=no,location=no resizable=yes');</script>", false);
        }


        private bool PayInvoiceProcess(int iNumeroFactura)
        {
            InvoiceBLL PayInvoicePss = new InvoiceBLL();

            PayInvoicePss.iCompany = Convert.ToInt32(HdnEmpresa.Value);
            PayInvoicePss.iInvoiceNumber = iNumeroFactura;

            return PayInvoicePss.PayInvoiceProcess();
        }

        private bool PayInvoice(int itype, GridViewRow row, int iNumeroFactura)
        {

            InvoiceBLL PayInvoice = new InvoiceBLL();
            PayInvoice.iType = itype;
            PayInvoice.iCompany = Convert.ToInt32(HdnEmpresa.Value);
            PayInvoice.iInvoiceNumber = iNumeroFactura;
            PayInvoice.iNumberAux = null;
            PayInvoice.dHaber = null;//Egreso

            if (itype == 1)
            {
                Label lbl = (Label)grvFormaPago.Rows[row.RowIndex].FindControl("lblNumber");
                PayInvoice.iConceptNumber = Convert.ToInt32(lbl.Text);

                Label currlbl = (Label)grvFormaPago.Rows[row.RowIndex].FindControl("lblMoneda");
                PayInvoice.iCurrency = Convert.ToInt32(currlbl.Text);

                Label exlbl = (Label)grvFormaPago.Rows[row.RowIndex].FindControl("lblTipocambio");
                PayInvoice.dExchange = Convert.ToDecimal(exlbl.Text);

                TextBox ingresotxt = (TextBox)grvFormaPago.Rows[row.RowIndex].FindControl("txtIngreso");
                PayInvoice.dIngreso = Convert.ToDecimal(ingresotxt.Text.Replace('$', ' '));
                TextBox refe = (TextBox)grvFormaPago.Rows[row.RowIndex].FindControl("txtReferencia");
                PayInvoice.sAux = refe.Text;//Referencia
                PayInvoice.Person = Convert.ToInt32(HdnUsuario.Value);
            }
            else if (itype == 4)
            {
                PayInvoice.iConceptNumber = null;
                PayInvoice.iCurrency = null;
                PayInvoice.dExchange = null;
                PayInvoice.dIngreso = null;
                PayInvoice.sAux = "";
                PayInvoice.Person = null;
            }

            else// checar si va la opcion 5
            {
                Label lbl = (Label)grvCambio.Rows[row.RowIndex].FindControl("lblNumber");
                PayInvoice.iConceptNumber = Convert.ToInt32(lbl.Text);

                Label currlbl = (Label)grvCambio.Rows[row.RowIndex].FindControl("lblMoneda");
                PayInvoice.iCurrency = Convert.ToInt32(currlbl.Text);

                Label exlbl = (Label)grvCambio.Rows[row.RowIndex].FindControl("lblTipocambio");
                PayInvoice.dExchange = Convert.ToDecimal(exlbl.Text);

                TextBox changetxt = (TextBox)grvCambio.Rows[row.RowIndex].FindControl("txtIngreso");
                PayInvoice.dEgreso = Convert.ToDecimal(changetxt.Text.Replace('$', ' '));
                PayInvoice.dIngreso = null;
                PayInvoice.sAux = "";

                PayInvoice.Person = Convert.ToInt32(Session["Person"]);
            }

            if (INTEGRA.Global.DataBase == "INTEGRA_ELECTROPURA" || INTEGRA.Global.DataBase == "INTEGRA_DEMO" || INTEGRA.Global.DataBase == "KONEXUS_LAGENERAL_CAP" || INTEGRA.Global.DataBase == "KONEXUS_LAGENERAL")
            {
                //===========================================================
                GeneraFacturaEnPopUp("GeneraFactura.aspx");
                //===========================================================
            }
            return PayInvoice.PayInvoice();
        }

        protected void NoContinuaClick(object sender, EventArgs e)
        {
            mdlTransaccion.Hide();
        }

        protected void agregaCambio_Click(object sender, EventArgs e)
        {
            try
            {
                int iNumeroFactura = 0;
                SaleBLL Sale = new SaleBLL();
                DataTable DataNumero = Sale.GetNumeroFactura(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));//
                if (DataNumero.Rows.Count > 0)
                {
                    iNumeroFactura = Convert.ToInt32(DataNumero.Rows[0][0]);
                }

                if (HdnImporteCambio.Value != "0")
                {
                    if (!bvalidacambio(iNumeroFactura))
                    {
                        //LabelTool.ShowLabel(lblMensajeCambio, "El cambio es incorrecto, favor de verificar!!!", System.Drawing.Color.DarkRed);
                    }
                }
                else
                {
                    LabelTool.ShowLabel(lblMensajeCambio, "Ingrese un monto!!!", System.Drawing.Color.DarkRed);
                }
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected bool bvalidacambio(int iNumeroFactura)
        {
            decimal dingreso = 0, dingresototal = 0, dImporteCambio = 0;
            DataTable ValidaTable;
            string ConceptoContable;
            string sMsjConceptoSinTrans = "";
            InvoiceBLL Invoice = new InvoiceBLL();

            foreach (GridViewRow row in grvCambio.Rows)
            {
                Label Numberlbl = (Label)row.FindControl("lblNumber");
                Label lblchange = (Label)grvCambio.Rows[row.RowIndex].FindControl("lblTipocambio");
                decimal exchange = Convert.ToDecimal(lblchange.Text);
                TextBox ingresotxt = (TextBox)grvCambio.Rows[row.RowIndex].FindControl("txtIngreso");
                dingreso = Convert.ToDecimal(ingresotxt.Text.Replace('$', ' ')) * exchange;
                int a = row.RowIndex;


                if (dingreso != 0)
                {
                    if (Convert.ToInt32(drpMoneda.SelectedValue) == 110)
                    {
                        dingreso = dingreso / dTipoCambio;
                    }

                    dingresototal += dingreso;

                    ValidaTable = Invoice.ValidaConceptoContable(Convert.ToInt32(HdnEmpresa.Value), iNumeroFactura, Convert.ToInt32(Numberlbl.Text));

                    if (ValidaTable != null)
                    {
                        ConceptoContable = Convert.ToString(ValidaTable.Rows[0][0]);

                        if (ConceptoContable != "")
                        {
                            sMsjConceptoSinTrans = sMsjConceptoSinTrans + "El concepto " + grvCambio.Rows[a].Cells[3].Text;
                            lblMsjTransaccionCambio.Text = sMsjConceptoSinTrans;
                        }
                    }
                }
            }

            if (sMsjConceptoSinTrans != "")
            {
                dImporteCambio = dingresototal;
                HdnImporteCambio.Value = dImporteCambio.ToString();
                mdlTransaccionCambio.Show();
                return false;
            }
            else
            {
                if (dingresototal == dCambio)
                {
                    if (GuardaDetallePago(iNumeroFactura))
                    {
                        if (bGuardaCambio(iNumeroFactura))
                        {
                            GuardaEncabezadoPago(iNumeroFactura);
                            mdlCambio.Hide();
                            mdlFormaPago.Hide();
                        }
                    }
                }
                else
                {
                    return false;
                }

                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
            }
            return true;
        }

        public bool bGuardaCambio(int iNumeroFactura)
        {
            bool bresult = true;

            foreach (GridViewRow row in grvCambio.Rows)
            {
                TextBox ingresotxt = (TextBox)grvCambio.Rows[row.RowIndex].FindControl("txtIngreso");
                decimal dingreso = Convert.ToDecimal(ingresotxt.Text.Replace('$', ' '));

                if (dingreso > 0)
                {
                    bresult = bresult && CreateChange(row, iNumeroFactura);
                }
            }
            return bresult;
        }

        protected void NoContinuaCambioClick(object sender, EventArgs e)
        {
            mdlTransaccionCambio.Hide();
        }

        protected void CambioSinTransaccionClick(object sender, EventArgs e)
        {
            try
            {
                int iNumeroFactura = 0;
                decimal dImporteCambio = 0;
                dImporteCambio = Convert.ToDecimal(HdnImporteCambio.Value);
                SaleBLL Sale = new SaleBLL();
                DataTable DataNumero = Sale.GetNumeroFactura(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));//
                if (DataNumero.Rows.Count > 0)
                {
                    iNumeroFactura = Convert.ToInt32(DataNumero.Rows[0][0]);
                }

                if (dImporteCambio == dCambio)
                {
                    if (GuardaDetallePago(iNumeroFactura))
                    {
                        if (bGuardaCambio(iNumeroFactura))
                        {
                            GuardaEncabezadoPago(iNumeroFactura);
                            mdlTransaccionCambio.Hide();
                            mdlCambio.Hide();
                            mdlFormaPago.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void GuardarCotizacion_Click(object sender, EventArgs e)
        {
            try
            {

                if (HdnValorIncorrecto.Value == "0")
                {
                    bClose = false;
                    iExistProduct = 0;
                    SaleBLL Sale = new SaleBLL();

                    int ifamily = 3;
                    int ifather = 9;
                    int iStatus = Sale.GetStatusNumberFromDescription("Iniciada", ifamily, ifather);

                    if (sSaleValidation.IndexOf("CambiarEstatusGeneral") == 0)
                    {
                        foreach (GridViewRow row in grvPartidas.Rows)
                        {
                            if (Sale.IsProduct(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][1])))
                            {
                                iExistProduct++;
                            }
                        }

                        DropTool.ClearDrop(generalstoreddl);

                        iNumeroVenta.Value = (GuardaVenta(Convert.ToInt32(iNumeroVenta.Value), iStatus).ToString());

                        if (iNumeroVenta.Value != "0")
                        {
                            lblVenta.Text = "Venta " + iNumeroVenta.Value + " guardada satisfactoriamente!!!";
                            LabelTool.HideLabel(lblError);
                            salemsgmdl.Show();
                        }
                        else
                        {
                            LabelTool.ShowLabel(lblError, "Error al insertar la venta, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                        }
                    }
                    else
                    {
                        if (sProductosNoValidos == "" || sProductosNoValidos == null)
                        {
                            iNumeroVenta.Value = (GuardaVenta(Convert.ToInt32(iNumeroVenta.Value), iStatus).ToString());
                            if (iNumeroVenta.Value != "0")
                            {
                                lblVenta.Text = "Venta " + iNumeroVenta.Value + " guardada satisfactoriamente!!!";
                                LabelTool.HideLabel(lblError);
                                salemsgmdl.Show();
                            }
                            else
                            {
                                LabelTool.ShowLabel(lblError, "Error al insertar la venta, intente nuevamente!!!", System.Drawing.Color.DarkRed);
                            }
                        }
                        else
                        {
                            LabelTool.ShowLabel(lblError, "Los Producto(s): " + sProductosNoValidos + " no cuenta con costo de referencia en la zona del almacén seleccionado, seleccione otro almacén!!!", System.Drawing.Color.DarkRed);
                        }
                    }
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Favor de verificar las cantidades!!!", System.Drawing.Color.DarkRed);
                }
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void grvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                HdnCliente.Value = grvClientes.SelectedRow.Cells[1].Text;
                GridViewTool.CleanGridViewSelection(grvClientes);
                ObtieneDatosCliente();

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void NuevaVenta_Click(object sender, EventArgs e)
        {
            try
            {
                PreparaVenta();
                ManageControls(true);
                ManageCommandControls(false);
                LabelTool.HideLabel(lblError);
                grvPartidas.Enabled = true;
                Session["Rutaelectronico"] = "";

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }


        protected void closechange_Click(object sender, ImageClickEventArgs e)
        {
            mdlCambio.Hide();
            LabelTool.HideLabel(lblMensajeCambio);
        }


        protected void closeimgbtn_Click(object sender, ImageClickEventArgs e)
        {
            messagelbl.Text = "";
            messagemdl.Hide();

            if (bFlagCharacteristics == false)
            {
                charmdl.Show();

                bFlagCharacteristics = true;
            }

            LabelTool.HideLabel(lblError);
        }

        protected void lnkPagarVentaClick(object sender, EventArgs e)
        {
            try
            {
                SaleBLL sale = new SaleBLL();

                DataTable TablaEmpresa = new DataTable();
                DataTable tempPayForms = new DataTable();

                DataTable DataNumero = sale.GetNumeroFactura(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));//
                int iNumeroFactura = Convert.ToInt32(DataNumero.Rows[0][0]);

                confirmmdl.Hide();
                LabelTool.HideLabel(cmessagelbl);
                grvFormaPago.Enabled = true;
                LbtnPay.Enabled = true;

                if ((TablaEmpresa = DatosEmpresa()) != null)    //Obtiene datos de la empresa
                {
                    if (TablaEmpresa.Rows.Count > 0)
                    {
                        companypaylbl.Text = Convert.ToString(TablaEmpresa.Rows[0][1]);
                        addresdpaylbl.Text = Convert.ToString(TablaEmpresa.Rows[0][2]);
                        coloniapaylbl.Text = Convert.ToString(TablaEmpresa.Rows[0][3]);
                        delegacionpaylbl.Text = Convert.ToString(TablaEmpresa.Rows[0][4]);
                        cppaylbl.Text = "C.P." + " " + Convert.ToString(TablaEmpresa.Rows[0][6]) + "  " + "Tel." + Convert.ToString(TablaEmpresa.Rows[0][8]);
                    }
                }

                tempPayForms = sale.GetPayForms(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));//NUMERO COTIZACION

                sbttaltktlbl.Text = txtSubtotal.Text;
                ivatktdlbl.Text = txtIva.Text;
                totaltktdlbl.Text = txtTotal.Text;
                txtTotalPago.Text = txtTotal.Text;
                txtPagoImporte.Text = "$0.00";
                txtCambiopago.Text = "$0.00";
                txtRestaPago.Text = txtTotal.Text;
                lblTotalpagotkt.Text = "$0.00";
                lblCambiotkt.Text = "$0.00";

                doctopaydrp.Text = Convert.ToString(drpDocumento.SelectedItem.Text);
                seriepaydrp.Text = Convert.ToString(drpSerie.SelectedItem.Text);
                numdoctopaytxt.Text = Convert.ToString(txtNumeroDocto.Text);

                clientpaytxt.Text = Convert.ToString(lblNombrecliente.Text);
                currencypaytxt.Text = Convert.ToString(drpMoneda.SelectedItem.Text);
                GridViewTool.Bind(tempPayForms, grvFormaPago);
                GridViewTool.Bind(ProductsTable, productspayGvw);//Llena el grid del ticket
                GridViewTool.Bind(null, grvDesglose);

                mdlFormaPago.Show();//Modal de Formas de pago

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void PrintDocument(object sender, EventArgs e)
        {
            try
            {

                if (drpDocumento.SelectedValue == "-59")
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
                    SaleBLL Sale = new SaleBLL();

                    if (Sale.GetStatusNumberFromSaleNumber(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value)) > 12)
                    {

                        ImprimeNotadeVenta();
                    }
                    else
                    {
                        ImprimeCotizacion();
                    }
                }
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        protected void GeneralStoreClose(object sender, EventArgs e)
        {
            generalstoremdl.Hide();
        }

        protected void doctodrpChanged(object sender, EventArgs e)
        {
            InvoiceBLL Invoice = new InvoiceBLL();
            FillSerie();
            int iFolio = Invoice.GetFolio(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(drpDocumento.SelectedValue), Convert.ToInt32(drpSerie.SelectedValue));
            txtNumeroDocto.Text = iFolio.ToString();
            if (drpDocumento.SelectedValue == "-59")
            {
                txtNumeroDocto.Enabled = false;
            }
            else
            {
                txtNumeroDocto.Enabled = true;
            }
        }

        protected void seriedrpChanged(object sender, EventArgs e)
        {
            InvoiceBLL Invoice = new InvoiceBLL();
            int iFolio = Invoice.GetFolio(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(drpDocumento.SelectedValue), Convert.ToInt32(drpSerie.SelectedValue));
            txtNumeroDocto.Text = iFolio.ToString();
            if (drpDocumento.SelectedValue == "-59")
            {
                txtNumeroDocto.Enabled = false;
            }
            else
            {
                txtNumeroDocto.Enabled = true;
            }
        }

        protected void lnkAgregaProductos_Click(object sender, EventArgs e)
        {
            try
            {
                messagelbl.Text = "";
                AgregaProductos();

                if (messagelbl.Text == "")
                {
                    productsmdl.Hide();
                    GetAmounts();
                    CheckCommandControls();
                    Session.Contents.Remove(_TempProductsTable);
                    visibleprintbtn();

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEscondeRefresh1();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                }

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }

        }

        protected void productsimgbtn_Click(object sender, ImageClickEventArgs e)
        {
            productsmdl.Hide();
        }


        #endregion


        #region Metodos/Propiedades

        private void PreparaVenta()
        {
            SaleBLL Sale = new SaleBLL();
            INTEGRA.Global.Product_type = 113;
            bMoral = true;
            bFlagCharacteristics = true;
            dExchange = IntegraBussines.ControlsBLL.GetExchange(Convert.ToInt32(HdnEmpresa.Value), 109, Convert.ToInt32(drpMoneda.SelectedValue), DateTime.Now);
            iDirectDelivery = 0;
            chkEntregaDirecta.Checked = false;
            iNumeroVenta.Value = "0";
            HdnCliente.Value = "-1";
            HdnImportePago.Value = "0";
            iProduct = 1;
            HdnImporteCambio.Value = "0";
            RdBtnListProductos.SelectedIndex = 0;
            drpMoneda.SelectedIndex = 0;
            drpMoneda.SelectedIndex = 0;
            drpFormadePago.SelectedIndex = 0;
            SetFocus(txtCodigoBarras);
            txtSubtotal.Text = "";
            txtIva.Text = "";
            txtTotal.Text = "";
            HdnValorIncorrecto.Value = "0";

            if (HdnCliente.Value != "")
            {
                DataTable dtCliente = new DataTable();
                CustomerBLL Customer = new CustomerBLL();

                dtCliente = Customer.GetPersonData(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnCliente.Value));
                if (dtCliente != null && dtCliente.Rows.Count > 0)
                {
                    ObtieneDatosCliente();
                    ObtieneDireccionCliente();
                }
                else if (dtCliente.Rows.Count == 0 && Convert.ToInt32(HdnCliente.Value) == -1)
                {
                    Console.Write("No se encontró mostrador");

                }
                else
                    LabelTool.ShowLabel(lblError, "Cliente inexistente", System.Drawing.Color.DarkRed);
            }
            else
            {
                LimpiaDatosCliente();
            }

            ManageCommandControls(false);
            txtCliente.Enabled = true;
            lnkPagarVenta.Visible = false;

            if (ProductsTable != null)
                ProductsTable.Dispose();

            ProductsTable = CreaTablaProductos();

            DropTool.ClearDrop(generalstoreddl);
            GridViewTool.Bind(ProductsTable, grvPartidas);
            Stores = new string[100];
            Session[_Stores] = Stores;
        }

        private void ObtenDatosVenta()
        {

            SaleBLL Sale = new SaleBLL();

            iNumeroVenta.Value = Request.QueryString["Sale"];

            HdnCliente.Value = Request.QueryString["Client"];
            int iStatus = Convert.ToInt32(Request.QueryString["Estatus"]);
            iClasAlmacen = Convert.ToInt32(Request.QueryString["Almacen"]);
            ProductsTable = Sale.GetDetailsTableFromSaleNumber(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));

            ProductsTable = ObtienePrecioConImpuestosSinIVA(ProductsTable);
            ProductsTable = ObtieneIVAPorProducto(ProductsTable);

            ObtieneDatosCliente();
            ObtieneDireccionCliente();
            GridViewTool.Bind(ProductsTable, grvPartidas);

            GetOptionValues();
            GetAmounts();
            CheckControls(iStatus);
            ManageStoreControls();

            foreach (GridViewRow row in grvPartidas.Rows)
            {
                DropDownList store = (DropDownList)row.FindControl("storeDropDownList");

                if (store != null)
                {
                    if (Convert.ToInt32(ProductsTable.Rows[row.RowIndex][11]) == 113)// ES PRODUCTO
                    {
                        store.Visible = true;
                    }
                    else
                    {
                        store.Visible = false;
                        row.Cells[1].Text = "";
                    }
                }
            }

            if (iClasAlmacen != 0)
            {
                System.Web.UI.HtmlControls.HtmlTable tbl = (System.Web.UI.HtmlControls.HtmlTable)TablaTitulos;

                tbl.Rows[0].Cells[1].Visible = false;
                tbl.Rows[0].Cells[2].Visible = false;
                tbl.Rows[0].Cells[0].Width = "35";
                tbl.Rows[0].Cells[3].Width = "160";
                tbl.Rows[0].Cells[4].Width = "40";
                grvPartidas.Columns[0].ItemStyle.Width = 60;
                grvPartidas.Columns[3].ItemStyle.Width = 267;
                grvPartidas.Columns[4].ItemStyle.Width = 80;
                grvPartidas.Columns[5].ItemStyle.Width = 105;
                grvPartidas.Columns[6].ItemStyle.Width = 82;
                visibledropsQuote();
            }
            LimpiaQueryString("Search", "Client", "Sale", "Estatus", "Currency", "IVA", "Credit", "Delivery", "Date");
        }

        private void CheckControls(int iStatus)
        {
            if (iStatus >= 12)
            {
                ManageControls(false);
                ManageCommandControls(false);
            }
            else
            {
                if (ProductsTable.Rows.Count == 0) ManageCommandControls(false);
                else ManageCommandControls(true);
                ManageControls(true);
            }
            if (iStatus != 10)
            {
                grvPartidas.Enabled = false;
                txtProductos.Enabled = false;
            }

            else
            {
                grvPartidas.Enabled = true;
                txtProductos.Enabled = true;
            }
            txtCliente.Enabled = false;
        }

        private void LimpiaQueryString(params string[] field)
        {
            NameValueCollection filtered = new NameValueCollection(Request.QueryString);

            int i = 0;

            while (i < field.Length)
                filtered.Remove(field[i++]);
        }

        protected void GetOptionValues()
        {
            drpMoneda.SelectedValue = Request.QueryString["Moneda"].ToString();
            drpIva.SelectedValue = Request.QueryString["IVA"].ToString();
            drpFormadePago.SelectedValue = Request["Credito"].ToString();

            if (Convert.ToInt32(Request.QueryString["Delivery"]) == 1) chkEntregaDirecta.Checked = true;
        }

        private void GetAmounts()
        {
            decimal dSubTotal = 0, dTotal = 0, dIVA = 0, divapartida = 0;

            if (ProductsTable.Rows.Count > 0)
            {
                dSubTotal = 0;

                foreach (DataRow row in ProductsTable.Rows)
                {

                    divapartida = Convert.ToDecimal(row["PorcentajeIVA"]);

                    dSubTotal += Math.Round(Convert.ToDecimal(row["Total"]), iRounded);
                    if (divapartida > 0)
                    {

                        dIVA += (Convert.ToDecimal((row["Total"] == null) ? 0 : row["Total"]) * (Convert.ToDecimal(row["PorcentajeIVA"] == null ? 0 : row["PorcentajeIVA"]) / 100));
                    }

                    dTotal = Math.Round((dSubTotal + dIVA), iRounded);
                }
            }
            else
            {
                dSubTotal = 0;
                dTotal = 0;
                dIVA = 0;
            }
            SetAmounts(dTotal, dSubTotal, dIVA);
        }


        protected void CambiaPorcentajeIVA()
        {

            foreach (DataRow row in ProductsTable.Rows)
            {
                if (row["IVAParametrizado"].ToString() == "NO")
                    row["PorcentajeIVA"] = Convert.ToDecimal(drpIva.SelectedItem.ToString());

            }

        }
        private void SetAmounts(decimal dTotal, decimal dSubTotal, decimal dIVA)
        {
            txtTotal.Text = dTotal.ToString("c2");
            txtSubtotal.Text = dSubTotal.ToString("c2");
            txtIva.Text = dIVA.ToString("c2");
        }

        private void LlenadoDrops()
        {
            LlenaDrop("moneda");
            LlenaDrop("IVA");
            LlenaDrop("formapago");
        }

        private void LlenaDrop(string dropname)
        {
            DataTable table = new DataTable();
            int ifather;
            int ifamily;

            switch (dropname)
            {
                case "moneda":
                    ifather = 1;
                    ifamily = 9;

                    if ((table = ControlsBLL.GetCurrencies(Convert.ToInt32(HdnEmpresa.Value), ifather, ifamily)) == null)
                    {
                        LabelTool.ShowLabel(lblError, "Error al conseguir las Denominaciones!!!", System.Drawing.Color.DarkRed);
                    }
                    else
                    {
                        if (table.Rows.Count > 0)
                        {
                            if (table.Rows[0][0] != DBNull.Value)
                            {
                                DropTool.FillDrop(drpMoneda, table, "Descripcion", "Numero");
                            }
                        }
                    }

                    break;

                case "IVA":
                    ifather = 31;
                    ifamily = 3;

                    if ((table = ControlsBLL.GetIVA(Convert.ToInt32(HdnEmpresa.Value), ifather, ifamily)) == null)
                    {
                        LabelTool.ShowLabel(lblError, "Error al conseguir los tipos de IVA!!!", System.Drawing.Color.DarkRed);
                    }
                    else
                    {
                        if (table.Rows.Count > 0)
                        {
                            if (table.Rows[0][0] != DBNull.Value)
                            {
                                DropTool.FillDrop(drpIva, table, "Descripcion", "Numero");

                                if (INTEGRA.Global.DataBase == "INTEGRA_ELECTROPURA")
                                {
                                    drpIva.SelectedValue = "136";
                                }
                                else
                                {
                                    drpIva.SelectedValue = "36";
                                }

                            }
                        }
                    }

                    break;

                case "formapago":

                    DropTool.FillDrop(drpFormadePago, CreatePayTable(), "Descripcion", "Numero");

                    break;
            }
        }

        private DataTable CreatePayTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Numero");
            table.Columns.Add("Descripcion");
            FillPayTable(table, 0, "Contado");
            FillPayTable(table, 1, "Credito");

            return table;
        }

        private void FillPayTable(DataTable table, int value, string description)
        {
            DataRow Row = table.NewRow();

            Row["Numero"] = value;
            Row["Descripcion"] = description;

            table.Rows.Add(Row);
        }


        private bool ValImportText(TextBox importetxt, int ichange, decimal dImporteResta, decimal dcnt, decimal exchange)
        {
            InvoiceBLL Invoice = new InvoiceBLL();

            if (importetxt.Text != "")
            {
                if (validaImporte(importetxt) == 1)
                {
                    dImporteResta = Convert.ToDecimal(txtTotalPago.Text.Replace('$', ' ')) - Convert.ToDecimal(txtPagoImporte.Text.Replace('$', ' '));
                    decimal resultado = (Convert.ToDecimal(importetxt.Text.Replace('$', ' ')) * exchange);

                    if (Convert.ToInt32(drpMoneda.SelectedValue) == 110)
                    {
                        resultado = resultado / exchange;
                    }

                    if (resultado > dImporteResta)
                    {
                        if (ichange == 1)
                        {
                            dcnt = Convert.ToDecimal(importetxt.Text.Replace('$', ' '));
                            importetxt.Text = dcnt.ToString("c2");
                        }
                        else
                        {
                            importetxt.Text = "$0.00";
                            LabelTool.ShowLabel(cmessagelbl, "El importe de los conceptos que no dan cambio excede el importe total de la venta!!!", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                    else
                    {
                        importetxt.Text = "$" + importetxt.Text;
                        LabelTool.HideLabel(cmessagelbl);
                    }
                }

                else
                {
                    LabelTool.ShowLabel(cmessagelbl, "Introduzca un importe válido!!!", System.Drawing.Color.DarkRed);
                    importetxt.Text = "$0.00";
                    return false;
                }
                return true;
            }
            else
            {
                importetxt.Text = "$0.00";
                return true;
            }
        }

        private int validaImporte(TextBox importe)
        {
            decimal dimporte = 0;

            try
            {
                if (importe.Text != "")
                {
                    dimporte = Convert.ToDecimal(importe.Text.Replace('$', ' '));
                    LabelTool.HideLabel(cmessagelbl);
                    return 1;

                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        private DataTable create_table_desglose()
        {
            DataTable temp_table = new DataTable();

            temp_table.Columns.Add("Descripcion");
            temp_table.Columns.Add("Ingreso");

            return temp_table;
        }

        private void CalculaImportePago()
        {
        }

        private void get_char_table(int index)
        {
            SaleBLL sale = new SaleBLL();

            if ((TempCharTable = sale.GetCharacteristics(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(TempProductsTable.Rows[index][1]),
                                                     Convert.ToInt32(TempProductsTable.Rows[index][2]),
                                                     1))
                                                     == null)
            {
            }
        }

        private DataTable CreaTablaDeCaracteristicas()
        {
            DataTable temp_table = new DataTable();

            temp_table.Columns.Add("Empresa");
            temp_table.Columns.Add("Producto");
            temp_table.Columns.Add("Concepto");
            temp_table.Columns.Add("Detalle");
            temp_table.Columns.Add("Familia_Prod");
            temp_table.Columns.Add("cantidad");
            temp_table.Columns.Add("Numero");

            return temp_table;
        }

        private void LlenaTablaCaracteristicas()
        {
            foreach (DataRow temprow in TempCharTable.Rows)
            {
                DataRow row = CharTable.NewRow();

                row["Empresa"] = temprow["Empresa"];
                row["Producto"] = temprow["Producto"];
                row["Concepto"] = temprow["Concepto"];
                row["Detalle"] = temprow["Numero_Detalle"];
                row["Familia_Prod"] = temprow["Familia_Prod"];
                row["Cantidad"] = temprow["Cantidad"];
                row["Numero"] = ProductsTable.Rows.Count;

                CharTable.Rows.Add(row);
            }
        }

        private void Show()
        {
            salemsgmdl.Show();
        }

        private void CheckCommandControls()
        {
            if (ProductsTable.Rows.Count > 0)
            {
                ManageCommandControls(true);
            }
            else
            {
                ManageCommandControls(false);
            }
        }

        private void ObtieneDatosCliente()
        {
            CustomerBLL Customer = new CustomerBLL();
            DataTable dtCliente;
            if (HdnCliente.Value != "")
            {

                using (dtCliente = Customer.GetPersonData(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnCliente.Value)))
                {
                    if (dtCliente != null && dtCliente.Rows.Count > 0)
                    {
                        AsignaDatosCliente(dtCliente);
                    }
                }
            }
        }

        private void AsignaDatosCliente(DataTable dtCliente)
        {
            lblNombrecliente.Text = dtCliente.Rows[0][2].ToString();
            lblRfc.Text = dtCliente.Rows[0][7].ToString();
        }

        private void ObtieneDireccionCliente()
        {
            int itypeperson = 13;
            DataTable AddressTable;

            if (HdnCliente.Value != "")
            {

                using (AddressTable = PersonBLL.ObtenDomicilioPersonaPorTipoDomicilio(Convert.ToInt32(HdnCliente.Value), itypeperson))
                {
                    if (AddressTable != null && AddressTable.Rows.Count > 0)
                    {
                        SetAddressData(AddressTable);
                    }
                }
            }
        }

        private void SetAddressData(DataTable AddressTable)
        {
            lblCalle.Text = AddressTable.Rows[0][3].ToString();
            lblColonia.Text = AddressTable.Rows[0][4].ToString();
            lblEstado.Text = AddressTable.Rows[0][5].ToString();
        }

        private void LimpiaDatosCliente()
        {
            lblNombrecliente.Text = "";
            lblRfc.Text = "";
            lblCalle.Text = "";
            lblColonia.Text = "";
            lblEstado.Text = "";

        }
        private void StoresControls()
        {
            SaleBLL Sale = new SaleBLL();
            DataTable StoreTable, store_table;
            int ifamstock = 1;
            int ifam = 7;


            foreach (GridViewRow row in grvPartidas.Rows)
            {
                DropDownList stores = (DropDownList)row.FindControl("storeDropDownList");

                if (ProductsTable.Rows.Count > 0)
                {
                    int iProductType = 113;

                    if (ProductsTable.Rows[row.RowIndex][11] != DBNull.Value)
                    {
                        iProductType = Convert.ToInt32(ProductsTable.Rows[row.RowIndex][11]);
                    }
                    if (Convert.ToInt32(ProductsTable.Rows[row.RowIndex][7]) != 0 && iProductType == 113)
                    {
                        using (StoreTable = Sale.GetStoreDataFromSaleNumber(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][12]), ifam))// get_stock(row.RowIndex, Sale.GetStoreDataFromSaleNumber(Convert.ToInt32(INTEGRA.Global.Company), iSaleNumber, Convert.ToInt32(ProductsTable.Rows[row.RowIndex][7]))))
                        {
                            store_table = get_stores(row.RowIndex);

                            if (StoreTable.Rows.Count > 0)
                            {
                                int StoreNumber = Convert.ToInt32(StoreTable.Rows[0][0]);
                                decimal Stock = Sale.GetStockStore(Convert.ToInt32(HdnEmpresa.Value), StoreNumber, Convert.ToInt32(ProductsTable.Rows[row.RowIndex][1]), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][2]), ifamstock);
                                string StoreDescription = StoreTable.Rows[0][1].ToString();
                                ProductsTable.Rows[row.RowIndex][7] = StoreNumber;
                            }

                            if (store_table.Rows.Count > 0)
                            {
                                if (sSaleValidation == "CambiarEstatusPartidaSE" || sSaleValidation == "AgregarPartidaSE")
                                {
                                    //agrega a cada tienda sus disponibles

                                    foreach (DataRow row1 in store_table.Rows)
                                    {
                                        row1[1] = row1[1] + " (" + Sale.GetStockStore(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(row1[0]), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][1]), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][2]), ifamstock).ToString() + ")";
                                    }
                                }

                                DropTool.FillDrop(stores, store_table, "Descripcion", "Numero");
                                row.Cells[1].Text = Sale.GetStockStore(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][7]), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][1]), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][2]), ifamstock).ToString();
                                stores.SelectedValue = ProductsTable.Rows[row.RowIndex][7].ToString();

                                if (row.Cells[1].Text == "0" && stores.Visible == false)
                                {
                                    row.Cells[1].Text = "";
                                }
                                if (row.Cells[1].Text == "0")
                                {
                                    row.Cells[1].Text = "--";
                                }
                            }

                        }
                    }
                }
            }
        }
        private void StoresControlsProducts()
        {
            SaleBLL Sale = new SaleBLL();
            DataTable StoreTable;
            int ifam = 7;
            int ifamstock = 1;

            foreach (GridViewRow row in GrvProductos.Rows)
            {
                DropDownList stores = (DropDownList)row.FindControl("storeDropDownList");

                if (TempProductsTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(TempProductsTable.Rows[row.RowIndex][7]) != 0)
                    {
                        using (StoreTable = Sale.GetStoreDataFromSaleNumber(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value), Convert.ToInt32(TempProductsTable.Rows[row.RowIndex][7]), ifam))// get_stock(row.RowIndex, Sale.GetStoreDataFromSaleNumber(Convert.ToInt32(INTEGRA.Global.Company), iSaleNumber, Convert.ToInt32(ProductsTable.Rows[row.RowIndex][7]))))
                        {
                            if (StoreTable.Rows.Count > 0)
                            {
                                int StoreNumber = Convert.ToInt32(StoreTable.Rows[0][0]);
                                decimal Stock = Sale.GetStockStore(Convert.ToInt32(HdnEmpresa.Value), StoreNumber, Convert.ToInt32(TempProductsTable.Rows[row.RowIndex][4]), Convert.ToInt32(TempProductsTable.Rows[row.RowIndex][5]), ifamstock);
                                string StoreDescription = StoreTable.Rows[0][1].ToString();

                                row.Cells[4].Text = Stock.ToString();
                            }

                        }
                    }
                    else
                    {
                        SaleBLL Sale1 = new SaleBLL();
                        Stores = (string[])Session[_Stores];
                        Stores[row.RowIndex] = stores.SelectedValue;
                        if (Stores[row.RowIndex] != "")
                        {
                            row.Cells[4].Text = Sale1.GetStockStore(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(stores.SelectedValue), Convert.ToInt32(TempProductsTable.Rows[row.RowIndex][1]), Convert.ToInt32(TempProductsTable.Rows[row.RowIndex][2]), ifamstock).ToString();
                            Session[_Stores] = Stores;
                        }

                        else
                        {

                            if (Convert.ToInt32(TempProductsTable.Rows[row.RowIndex][7]) == 0)//19/11
                            {
                                if (Sale.IsProduct(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(TempProductsTable.Rows[row.RowIndex][1])))
                                {
                                    row.Cells[4].Text = "Sin Disponibles";
                                }
                            }
                        }
                    }
                }
            }
        }
        private int ObtenCostoReferencia(int office)
        {
            int ZoneOffice = 0, InvoiceOffice = 0, RelationOffice = 0, ValueReferenceCost = 0, iproductsFamily = 1;
            int iNumeroFactura = 0;
            DataTable TableOfficeInvoice, TableZoneOffice, TableZoneRelationOffice, TableReferenceCost;
            ConfiguracionVentasEmpresas Configuration = new ConfiguracionVentasEmpresas(Convert.ToInt32(HdnEmpresa.Value));
            SaleBLL Sale = new SaleBLL();

            DataTable DataNumero = Sale.GetNumeroFactura(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));//
            if (DataNumero.Rows.Count > 0)
            {
                if (DataNumero.Rows[0][0] != DBNull.Value)
                {
                    iNumeroFactura = Convert.ToInt32(DataNumero.Rows[0][0]);
                }
            }

            iReferenceCost = Configuration.iCostoReferencia;

            if (iDirectDelivery == 1)
            {
            }
            else
            {
                if (iReferenceCost == 1)
                {
                    if (iNumeroFactura != 0)
                    {
                        TableOfficeInvoice = Sale.GetOfficeInvoice(Convert.ToInt32(HdnEmpresa.Value), iNumeroFactura);

                        if (TableOfficeInvoice.Rows.Count > 0)
                        {
                            InvoiceOffice = Convert.ToInt32(TableOfficeInvoice.Rows[0][0]);
                        }
                    }
                    else
                    {
                        InvoiceOffice = Convert.ToInt32(HdnSucursal.Value);
                    }

                    TableZoneOffice = Sale.GetZoneOffice(Convert.ToInt32(HdnEmpresa.Value), InvoiceOffice);

                    if (TableZoneOffice.Rows.Count > 0)
                    {
                        ZoneOffice = Convert.ToInt32(TableZoneOffice.Rows[0][0]);
                    }

                    TableZoneRelationOffice = Sale.GetZoneRelationOffice(Convert.ToInt32(HdnEmpresa.Value), office);

                    if (TableZoneRelationOffice.Rows.Count > 0)
                    {
                        RelationOffice = Convert.ToInt32(TableZoneRelationOffice.Rows[0][0]);
                    }
                    else
                    {
                        RelationOffice = ZoneOffice;
                    }

                    int iProductnumber = Convert.ToInt32(ProductsTable.Rows[0][1]);
                    int iConceptonumber = Convert.ToInt32(ProductsTable.Rows[0][2]);

                    TableReferenceCost = Sale.GetReferenceCost(Convert.ToInt32(HdnEmpresa.Value), RelationOffice, iProductnumber, iConceptonumber, iproductsFamily);

                    if (TableReferenceCost.Rows.Count > 0)
                    {
                        ValueReferenceCost = Convert.ToInt32(TableReferenceCost.Rows[0][0]);

                        if (ValueReferenceCost != 0)

                            return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
            return 0;
        }
        protected void get_stores_for_gamequote()
        {
            SaleBLL Sale = new SaleBLL();
            DataTable store_table = new DataTable();
            DataTable stock_table = new DataTable();
            int ifamstock = 1;
            foreach (GridViewRow row in grvPartidas.Rows)
            {
                DropDownList store = (DropDownList)row.FindControl("storeDropDownList");


                if (store != null)
                {
                    if (Convert.ToInt32(ProductsTable.Rows[row.RowIndex][12]) == 0)
                    {
                        if (Sale.IsProduct(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][1])))
                        {
                            iProduct = 1;
                            store_table = get_stores(row.RowIndex);

                            if (store_table.Rows.Count > 0)
                            {
                                if (sSaleValidation == "CambiarEstatusPartidaSE" || sSaleValidation == "AgregarPartidaSE")
                                {
                                    //agrega a cada tienda sus disponibles

                                    foreach (DataRow row1 in store_table.Rows)
                                    {
                                        row1[1] = row1[1] + " (" + Sale.GetStockStore(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(row1[0]), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][1]), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][2]), ifamstock).ToString() + ")";
                                    }
                                }

                                DropTool.FillDrop(store, store_table, "Descripcion", "Numero");

                                row.Cells[1].Text = Sale.GetStockStore(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][7]), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][1]), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][2]), ifamstock).ToString();
                                store.SelectedValue = ProductsTable.Rows[row.RowIndex][7].ToString();
                            }
                        }
                        else
                        {
                            iProduct = 0;
                            store.Visible = false;
                            row.Cells[1].Text = "";
                        }
                    }
                }

            }

            DataTable ProductsTableTemp = CreaTablaProductos();
            sProductosNoValidos = "";

            foreach (GridViewRow row in grvPartidas.Rows)
            {
                DropDownList store = (DropDownList)row.FindControl("storeDropDownList");

                if (store != null)
                {
                    if (Convert.ToInt32(ProductsTable.Rows[row.RowIndex][12]) == 0)
                    {
                        if (Sale.IsProduct(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][1])))
                        {
                            iProduct = 1;
                            store_table = get_stores(row.RowIndex);

                            if (store_table.Rows.Count > 0)
                            {
                                int office = Convert.ToInt32(store.SelectedValue);
                                if (iReferenceCost == 1)
                                {
                                    iValueReference = ObtenCostoReferencia(office);

                                    if (iValueReference == 2 || iValueReference == 0)
                                    {
                                        sProductosNoValidos = sProductosNoValidos + (ProductsTable.Rows[row.RowIndex][9]) + ", ";
                                    }
                                    else
                                    {
                                        ProductsTableTemp.ImportRow((ProductsTable.Rows[row.RowIndex]));
                                    }
                                }
                                else
                                {
                                    ProductsTableTemp.ImportRow((ProductsTable.Rows[row.RowIndex]));
                                }
                            }
                        }
                        else
                        {
                            iProduct = 0;
                            ProductsTableTemp.ImportRow((ProductsTable.Rows[row.RowIndex]));
                        }
                    }

                    else
                    {
                        ProductsTableTemp.ImportRow((ProductsTable.Rows[row.RowIndex]));
                    }
                }
            }

            if (sProductosNoValidos != "")
            {
                LabelTool.ShowLabel(lblError, "Los Producto(s): " + sProductosNoValidos + " no cuenta con costo de referencia en la zona del almacén seleccionado!!!", System.Drawing.Color.DarkRed);
            }
            else
            {
                LabelTool.HideLabel(lblError);
            }

            sProductosNoValidos = "";
            ProductsTable = ProductsTableTemp;

            foreach (GridViewRow row in grvPartidas.Rows)
            {
                DropDownList store = (DropDownList)row.FindControl("storeDropDownList");

                if (store != null)
                {
                    if (Sale.IsProduct(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][1])))
                    {
                        store.Visible = true;
                    }
                    else
                    {
                        store.Visible = false;
                        row.Cells[1].Text = "";
                    }
                }
            }
        }
        protected void get_stores_for_gamequoteproducts()//llena los drops de las sucursales
        {
            SaleBLL Sale = new SaleBLL();
            DataTable store_table = new DataTable();
            DataTable stock_table = new DataTable();
            int ifamstock = 1;

            foreach (GridViewRow row in GrvProductos.Rows)
            {
                DropDownList store = (DropDownList)row.FindControl("storeDropDownList");
                TextBox qtytxt = new TextBox();
                qtytxt = (TextBox)row.FindControl("checkTextBox");

                if (store != null)
                {
                    if (Convert.ToInt32(TempProductsTable.Rows[row.RowIndex][7]) == 0)
                    {
                        if (Sale.IsProduct(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(TempProductsTable.Rows[row.RowIndex][1])))
                        {
                            store_table = get_storesGridProduct(row.RowIndex);

                            if (store_table.Rows.Count > 0)
                            {
                                if (sSaleValidation == "CambiarEstatusPartidaSE" || sSaleValidation == "AgregarPartidaSE")
                                {
                                    //agrega a cada tienda sus disponibles

                                    foreach (DataRow row1 in store_table.Rows)
                                    {
                                        row1[1] = row1[1] + " (" + Sale.GetStockStore(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(row1[0]), Convert.ToInt32(TempProductsTable.Rows[row.RowIndex][1]), Convert.ToInt32(TempProductsTable.Rows[row.RowIndex][2]), ifamstock).ToString() + ")";

                                    }
                                }
                                DropTool.FillDrop(store, store_table, "Descripcion", "Numero");
                            }
                            else
                            {
                                store.Enabled = false;
                                qtytxt.Enabled = false;
                                row.Cells[4].Text = "Sin disponibles";
                            }
                            visibledropstrue();
                            System.Web.UI.HtmlControls.HtmlTable tblproducts = (System.Web.UI.HtmlControls.HtmlTable)TablaTituloProductos;
                            tblproducts.Rows[0].Cells[4].Visible = true;
                            tblproducts.Rows[0].Cells[5].Visible = true;
                        }
                        else
                        {
                            store.Visible = false;
                            row.Cells[4].Text = "";
                            visibledrops();
                            System.Web.UI.HtmlControls.HtmlTable tblproducts = (System.Web.UI.HtmlControls.HtmlTable)TablaTituloProductos;
                            tblproducts.Rows[0].Cells[4].Visible = false;
                            tblproducts.Rows[0].Cells[5].Visible = false;
                        }
                    }
                }
            }
        }
        private void populate_dropdown_stores(DropDownList store, DataTable store_table)
        {
            int item_number = 0;

            foreach (DataRow store_row in store_table.Rows)
            {
                store.Items.Add(store_table.Rows[item_number++][1].ToString());
            }
        }
        private DataTable get_stores(int number_row)
        {
            SalesStoresBLL SalesStores = new SalesStoresBLL();
            SaleBLL sale = new SaleBLL();

            SalesStores.Company = Convert.ToInt32(HdnEmpresa.Value);
            SalesStores.User = Convert.ToInt32(HdnUsuario.Value);
            SalesStores.Office = Convert.ToInt32(HdnSucursal.Value);

            if (sSaleValidation.IndexOf("SE") > 0)
            {
                if (SalesStores.Office > 0)
                {
                    if (SalesStores.GetNumberOfVirtualStores() > 0)
                    {
                        if (iDirectDelivery == 1)
                            return sale.GetVirtualStores(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(HdnSucursal.Value));
                        else
                            return sale.GetNormalStores(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(HdnSucursal.Value));
                    }
                    else
                    {
                        if (iDirectDelivery == 0)
                            return sale.GetAllStores(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value));
                    }
                }
                else
                    return sale.GetAllStores(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value));
            }

            return filter_stock_stores(number_row);
        }
        private DataTable get_storesGridProduct(int number_row)
        {
            SalesStoresBLL SalesStores = new SalesStoresBLL();
            SaleBLL sale = new SaleBLL();

            SalesStores.Company = Convert.ToInt32(HdnEmpresa.Value);
            SalesStores.User = Convert.ToInt32(HdnUsuario.Value);
            SalesStores.Office = Convert.ToInt32(HdnSucursal.Value);

            if (sSaleValidation.IndexOf("SE") > 0)
            {
                if (SalesStores.Office > 0)
                {
                    if (SalesStores.GetNumberOfVirtualStores() > 0)
                    {
                        if (iDirectDelivery == 1)

                            return sale.GetVirtualStores(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(HdnSucursal.Value));
                        else

                            return sale.GetNormalStores(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(HdnSucursal.Value));
                    }
                    else
                    {
                        if (iDirectDelivery == 0)
                            return sale.GetAllStores(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value));
                    }
                }
                else
                    return sale.GetAllStores(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value));
            }

            return filter_stock_storesproducts(number_row);
        }
        private DataTable get_stores()
        {
            SaleBLL sale = new SaleBLL();
            SalesStoresBLL SalesStores = new SalesStoresBLL();

            SalesStores.Company = Convert.ToInt32(HdnEmpresa.Value);
            SalesStores.User = Convert.ToInt32(HdnUsuario.Value);
            SalesStores.Office = Convert.ToInt32(HdnSucursal.Value);

            if (SalesStores.Office > 0)
            {
                if (SalesStores.GetNumberOfVirtualStores() > 0)
                {
                    if (iDirectDelivery == 1)
                        return sale.GetVirtualStores(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(HdnSucursal.Value));
                    else
                        return sale.GetNormalStores(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(HdnSucursal.Value));
                }
                else
                {
                    if (iDirectDelivery == 0)
                        return sale.GetAllStores(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value));
                }
            }

            return sale.GetAllStores(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value));
        }
        private DataTable filter_stock_stores(int number_row)
        {
            DataTable temp_table = new DataTable();
            DataTable final_table = new DataTable();

            SaleBLL sale = new SaleBLL();

            temp_table = VentasController.ObtieneDisponiblesAlmacenProductos(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(ProductsTable.Rows[number_row][1]), Convert.ToInt32(ProductsTable.Rows[number_row][2]));
            final_table = temp_table.Clone();
            final_table = temp_table.Copy();

            if (temp_table.Rows.Count > 0)
            {
                int index = 0;

                foreach (DataRow row in temp_table.Rows)
                {
                    decimal quantity = Convert.ToDecimal(ProductsTable.Rows[number_row][5]);
                    decimal stocks = Convert.ToDecimal(row["Cantidad"]);

                    if (stocks < quantity)
                    {
                        final_table.Rows.RemoveAt(index);
                        index--;
                    }

                    index++;
                }
            }

            return final_table;
        }
        private DataTable filter_stock_storesproducts(int number_row)
        {
            SaleBLL sale = new SaleBLL();

            DataTable final_table = new DataTable();

            final_table = VentasController.ObtieneDisponiblesAlmacenProductos(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(TempProductsTable.Rows[number_row][1]), Convert.ToInt32(TempProductsTable.Rows[number_row][2]));

            return final_table;
        }
        protected DataTable CreaTablaProductos()
        {
            DataTable temp_table = new DataTable();

            temp_table.Columns.Add("Tipo_Concepto");
            temp_table.Columns.Add("Producto");
            temp_table.Columns.Add("Concepto");
            temp_table.Columns.Add("Precio");
            temp_table.Columns.Add("Unidad");
            temp_table.Columns.Add("Cantidad");
            temp_table.Columns.Add("Total");
            temp_table.Columns.Add("Numero");
            temp_table.Columns.Add("Empresa");
            temp_table.Columns.Add("Descripcion_Producto");
            temp_table.Columns.Add("Descripcion_Unidad");
            temp_table.Columns.Add("Tipo");
            temp_table.Columns.Add("Partida");
            temp_table.Columns.Add("PorcentajeIVA");
            temp_table.Columns.Add("IVAParametrizado");

            return temp_table;
        }
        protected void hiddenstores()
        {
            System.Web.UI.HtmlControls.HtmlTable tbl = (System.Web.UI.HtmlControls.HtmlTable)TablaTitulos;
            tbl.Rows[0].Cells[1].Visible = false;
            tbl.Rows[0].Cells[2].Visible = false;
            tbl.Rows[0].Cells[0].Width = "35";
            tbl.Rows[0].Cells[3].Width = "160";
            tbl.Rows[0].Cells[4].Width = "40";
            grvPartidas.Columns[0].ItemStyle.Width = 60;
            grvPartidas.Columns[3].ItemStyle.Width = 267;
            grvPartidas.Columns[4].ItemStyle.Width = 80;
            grvPartidas.Columns[5].ItemStyle.Width = 105;
            grvPartidas.Columns[6].ItemStyle.Width = 82;
            visibledropsQuote();
            System.Web.UI.HtmlControls.HtmlTable tblproducts = (System.Web.UI.HtmlControls.HtmlTable)TablaTituloProductos;
            tblproducts.Rows[0].Cells[4].Visible = false;
            tblproducts.Rows[0].Cells[5].Visible = false;
            GrvProductos.Columns[0].ItemStyle.Width = 60;
        }
        protected void showstores()
        {
            System.Web.UI.HtmlControls.HtmlTable tbl = (System.Web.UI.HtmlControls.HtmlTable)TablaTitulos;
            tbl.Rows[0].Cells[1].Visible = true;
            tbl.Rows[0].Cells[2].Visible = true;
            System.Web.UI.HtmlControls.HtmlTable tblproducts = (System.Web.UI.HtmlControls.HtmlTable)TablaTituloProductos;
            tblproducts.Rows[0].Cells[4].Visible = true;
            tblproducts.Rows[0].Cells[5].Visible = true;
        }
        private void visibledrops()
        {
            foreach (GridViewRow row in GrvProductos.Rows)
            {
                DropDownList drop = new DropDownList();

                drop = (DropDownList)row.FindControl("storeDropDownList");

                if (drop != null)
                {
                    drop.Visible = false;
                    GrvProductos.Columns[4].Visible = false;
                    GrvProductos.Columns[5].Visible = false;
                }
            }
        }
        private void visibledropstrue()
        {
            foreach (GridViewRow row in GrvProductos.Rows)
            {
                DropDownList drop = new DropDownList();

                drop = (DropDownList)row.FindControl("storeDropDownList");

                if (drop != null)
                {
                    drop.Visible = true;
                    GrvProductos.Columns[4].Visible = true;
                    GrvProductos.Columns[5].Visible = true;
                }
            }
        }
        private void visibledropsQuote()
        {
            foreach (GridViewRow row in grvPartidas.Rows)
            {
                DropDownList drop = new DropDownList();

                drop = (DropDownList)row.FindControl("storeDropDownList");

                if (drop != null)
                {
                    drop.Visible = false;
                    grvPartidas.Columns[1].Visible = false;
                    grvPartidas.Columns[2].Visible = false;
                }
            }
        }
        private void AgregaProductos()
        {

            int index = 0;
            add_Totals_columns(ref index);
            LLenaProductsTable();

            if (sSaleValidation.IndexOf("CambiarEstatusGeneral") == -1)
            {
                if (sSaleValidation != "NOTVAL" && sSaleValidation != "AgregarPartidaSE" && sSaleValidation != "CambiarEstatusPartidaSE")// comente para prueba regresar
                {
                    check_products_table();
                    fill_store_controls(index);
                    GetStores();

                }

                else if (sSaleValidation == "AgregarPartidaSE" || sSaleValidation == "CambiarEstatusPartidaSE")// integra_tmp entra aqui   comente para prueba regresar
                {
                    fill_store_controls(index);
                    GetStores();
                }
                else
                {
                    GridViewTool.Bind(ProductsTable, grvPartidas);
                }
            }
            else
            {
                GridViewTool.Bind(ProductsTable, grvPartidas);
            }

        }
        private void visibleprintbtn()
        {
            int idx = 0, inew = 0;

            foreach (DataRow row in ProductsTable.Rows)
            {
                if (Convert.ToInt32(ProductsTable.Rows[idx][12]) == 0)
                {
                    inew++;
                }

                idx++;
            }

        }
        private void AsignaPreciosdeLista()
        {
            if (dExchange > 1)
            {
                foreach (DataRow row in TempProductsTable.Rows)
                {
                    if (Convert.ToInt32(drpMoneda.SelectedValue) == 109)
                        row["Precio"] = Math.Round((Convert.ToDecimal(row["Precio"]) * dExchange), iRounded).ToString();
                    else
                        row["Precio"] = Math.Round((Convert.ToDecimal(row["Precio"]) / dExchange), iRounded).ToString();
                }
            }
        }

        private void LimpiaControlTxtProductos()
        {
            txtProductos.Text = "";
        }
        private DataTable get_products_concept()
        {
            DataTable dt = null;
            if (keycontainer.Value != null && keycontainer.Value != "" && HdnCliente.Value != "")
            {
                dt = VentasController.GetConcepts(Convert.ToInt32(HdnEmpresa.Value), 1, Convert.ToInt32(keycontainer.Value), Convert.ToInt32(HdnCliente.Value), 0);
            }

            return dt;
        }

        private void change_quote_price()
        {
            int index = 0;
            decimal dCantidad = 0;

            if (ProductsTable.Rows.Count > 0)
            {
                foreach (GridViewRow row in grvPartidas.Rows)
                {
                    TextBox cantidadtxt = (TextBox)grvPartidas.Rows[row.RowIndex].FindControl("cantidadtxt");
                    dCantidad = Convert.ToInt32(cantidadtxt.Text);

                    if (Convert.ToInt32(drpMoneda.SelectedValue) == 109)
                    {
                        row.Cells[5].Text = Math.Round((Convert.ToDecimal(row.Cells[5].Text.Replace('$', ' ')) * dTempExchange), iRounded).ToString();
                    }
                    else
                    {
                        row.Cells[5].Text = Math.Round((Convert.ToDecimal(row.Cells[5].Text.Replace('$', ' ')) / dExchange), iRounded).ToString();
                    }

                    row.Cells[7].Text = Math.Round(Convert.ToDecimal(row.Cells[5].Text.Replace('$', ' ')) * Convert.ToDecimal(dCantidad), iRounded).ToString();

                    if (ProductsTable.Rows.Count > 0)
                    {
                        ProductsTable.Rows[index][3] = row.Cells[5].Text;
                        ProductsTable.Rows[index++][6] = row.Cells[7].Text;
                    }
                }
            }
        }
        private void DeleteStoresItem(int index)
        {
            Stores = (string[])Session[_Stores];
            List<string> list = new List<string>(Stores);
            list.RemoveAt(index);
            Stores = list.ToArray();
            Session[_Stores] = Stores;
        }
        protected void delete_gamequote(int index, int iNumeroVenta)
        {
            if (GuardaDetalleVenta(2, index, iNumeroVenta, 0))
            {
                ProductsTable.Rows.RemoveAt(index);
                GridViewTool.Bind(ProductsTable, grvPartidas);
            }
        }
        private bool GuardaDetalleVenta(int Moviment, int Index, int iNumeroVenta, decimal dNuevoPrecio)
        {
            SaleDetailBLL SaleDetail = new SaleDetailBLL();

            SaleDetail.Company = Convert.ToInt32(HdnEmpresa.Value);
            SaleDetail.SaleNumber = iNumeroVenta;
            SaleDetail.Number = Convert.ToInt32(ProductsTable.Rows[Index][12]);
            SaleDetail.Product = Convert.ToInt32(ProductsTable.Rows[Index][1]);
            SaleDetail.TConcept = Convert.ToInt32(ProductsTable.Rows[Index][0]);
            SaleDetail.Concept = Convert.ToInt32(ProductsTable.Rows[Index][2]);
            SaleDetail.Width = null;
            SaleDetail.High = null;
            SaleDetail.Quantity = Convert.ToDecimal(ProductsTable.Rows[Index][5]);
            SaleDetail.Price = Convert.ToDecimal(ProductsTable.Rows[Index][3]);
            SaleDetail.To = null;
            if (dNuevoPrecio == 0)
            {
                SaleDetail.Amount = Convert.ToDecimal(ProductsTable.Rows[Index][6]);
            }
            else
            {
                SaleDetail.Amount = Convert.ToDecimal(ProductsTable.Rows[Index][5]) * dNuevoPrecio;
            }
            SaleDetail.Section = null;
            SaleDetail.OrderNumber = 1;
            SaleDetail.Active = 1;
            SaleDetail.StatusOrder = 1;
            SaleDetail.MeasuringUnit = Convert.ToInt32(ProductsTable.Rows[Index][4]);
            SaleDetail.MeasuringUnitQty = null;
            SaleDetail.MeasuringUnitFam = 1;
            SaleDetail.ProductConceptFam = 1;
            SaleDetail.Characteristics = null;
            SaleDetail.DeliveryDate = DateTime.Today;
            SaleDetail.Zone = null;
            SaleDetail.ReferenceCost = null;

            return SaleDetail.SaveDetail(Moviment);
        }
        private int GuardaVenta(int iNumeroVenta, int iStatus)
        {
            bool Flag = true;


            if (iNumeroVenta > 0) Flag = false;

            if ((iNumeroVenta = Sale(1, iNumeroVenta, iStatus)) > 0)
            {
                if (Flag == true) save_characteristics(iNumeroVenta);


                if (GuardaDetallesVenta(iNumeroVenta))
                {
                    return iNumeroVenta;
                }
                else
                {
                    return 0;

                }
            }

            return 0;
        }
        private bool Delete(int Index)
        {
            try
            {
                DeleteCotArtDesc(Index);

                return true;

            }
            catch
            {
                return false;
            }
        }
        private bool DeleteCotArtDesc(int Index)
        {
            InvoiceBLL DeleteCAD = new InvoiceBLL();

            DeleteCAD.iCompany = Convert.ToInt32(HdnEmpresa.Value);
            DeleteCAD.iNumber = Convert.ToInt32(ProductsTable.Rows[Index][12]);
            DeleteCAD.iInvoiceNumber = Convert.ToInt32(iNumeroVenta.Value);//Num cotización

            return DeleteCAD.DeleteCAD();
        }
        private int Sale(int Moviment, int Number, int iStatus)
        {
            SaleBLL Sale = new SaleBLL();
            CustomerBLL Customer = new CustomerBLL();
            int itypeperson = 13;

            Sale.Company = Convert.ToInt32(HdnEmpresa.Value);
            Sale.Number = Number;
            Sale.Status = Convert.ToInt32(iStatus);
            Sale.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            Sale.Customer = Convert.ToInt32(HdnCliente.Value);
            Sale.User = Convert.ToInt32(HdnUsuario.Value);
            Sale.Seller = null;
            Sale.Responsible = Convert.ToInt32(HdnUsuario.Value);
            Sale.Project = "";
            Sale.Currency = Convert.ToInt32(drpMoneda.SelectedValue);
            Sale.DeliveryDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            Sale.Contact = null;
            Sale.Observations = "";
            Sale.PayNumber = 1;
            Sale.CPayPeriod = 5;
            Sale.Document = null;
            Sale.ExpirationDate = DateTime.Today;
            Sale.OrderNumber = 1;
            Sale.CIVA = Convert.ToInt32(drpIva.SelectedValue);
            Sale.DocumentRelation = null;
            Sale.CurrencyInvoice = Convert.ToInt32(drpMoneda.SelectedValue);
            Sale.ConceptNumber = 2;
            Sale.ReferenceNumber = null;
            DataTable TableFiscalAddres = new DataTable();
            TableFiscalAddres = PersonBLL.ObtenDomicilioPersonaPorTipoDomicilio(Convert.ToInt32(HdnCliente.Value), itypeperson);
            if (TableFiscalAddres != null)
            {
                if (TableFiscalAddres.Rows.Count > 0)
                {
                    Sale.AddressNumber = (TableFiscalAddres.Rows[0][11] == DBNull.Value) ? 0 : Convert.ToInt32(TableFiscalAddres.Rows[0][11]);
                }
            }
            else
            {
                Sale.AddressNumber = 0;//preguntar
            }
            Sale.Credit = Convert.ToInt32(drpFormadePago.SelectedValue);
            Sale.COffice = Convert.ToInt32(HdnSucursal.Value);
            Sale.DirectDelivery = iDirectDelivery;
            Sale.CNumber = null;

            if (sSaleValidation.IndexOf("CambiarEstatusGeneral") == -1)
            {
                Sale.CStore = null;
            }
            else
            {
                if (Convert.ToInt32(iNumeroVenta.Value) > 0)
                {
                    if (iClasAlmacen != 0)
                    {
                        Sale.CStore = iClasAlmacen;//(checar iClasAlmacen tendria que ser el almacen si es que trae esa venta )
                    }
                    else
                    {
                        Sale.CStore = null;

                    }
                }
                else
                {
                    if (iExistProduct > 0 && generalstoreddl.SelectedValue != "")

                        Sale.CStore = Convert.ToInt32(generalstoreddl.SelectedValue);
                    else
                        Sale.CStore = null;

                }

            }
            return Sale.SaveSale(Moviment);
        }

        private void ManageStoreControls()
        {
            if (sSaleValidation == "AgregarPartida" || sSaleValidation == "AgregarPartidaSE"
                    || sSaleValidation == "CambiarEstatusPartida" || sSaleValidation == "CambiarEstatusPartidaSE")
                StoresControls();
        }

        private void save_characteristics(int iNumeroVenta)
        {
            int index = 0;

            foreach (DataRow row in CharTable.Rows)
                sp_Cotizacion_Detalle_Caracteristicas(index++, iNumeroVenta);
        }

        private void disable_quotes()
        {
            foreach (GridViewRow row in grvPartidas.Rows)
                row.Cells[0].Enabled = false;
        }

        protected bool GuardaDetallesVenta(int iNumeroVenta)
        {
            SaleBLL Sale = new SaleBLL();
            bool detalleguardadoCorrecto = true;
            decimal dNuevoPrecio = 0;

            foreach (GridViewRow row in grvPartidas.Rows)
            {


                DropDownList store = new DropDownList();

                store = (DropDownList)row.FindControl("storeDropDownList");

                if (store != null && sSaleValidation != "CambiarEstatusGeneral" && sSaleValidation != "CambiarEstatusGeneralSE" && sSaleValidation != "NOTVAL")
                {
                    if (store.SelectedValue.ToString() != "" && Convert.ToInt32(ProductsTable.Rows[row.RowIndex][12]) == 0)
                    {

                        Sale.StoreDetailsTMPTable(2, Convert.ToInt32(HdnEmpresa.Value), iNumeroVenta, 0, Convert.ToInt32(store.SelectedValue), Convert.ToDecimal(ProductsTable.Rows[row.RowIndex][5]), Convert.ToInt32(HdnUsuario.Value));
                        Sale.StoreDetailsTMPTable(1, Convert.ToInt32(HdnEmpresa.Value), iNumeroVenta, 0, Convert.ToInt32(store.SelectedValue), Convert.ToDecimal(ProductsTable.Rows[row.RowIndex][5]), Convert.ToInt32(HdnUsuario.Value));

                        detalleguardadoCorrecto = GuardaDetalleVenta(1, row.RowIndex, iNumeroVenta, dNuevoPrecio);
                        store.Dispose();
                    }

                    else if (Convert.ToInt32(ProductsTable.Rows[row.RowIndex][12]) == 0)
                    {

                        detalleguardadoCorrecto = GuardaDetalleVenta(1, row.RowIndex, iNumeroVenta, dNuevoPrecio);
                        store.Dispose();
                    }

                    else if (Convert.ToInt32(ProductsTable.Rows[row.RowIndex][12]) != 0 && Convert.ToInt32(ProductsTable.Rows[row.RowIndex][11]) == 113)
                    {
                        Sale.StoreDetailsTMPTable(2, Convert.ToInt32(HdnEmpresa.Value), iNumeroVenta, 0, Convert.ToInt32(store.SelectedValue), Convert.ToDecimal(ProductsTable.Rows[row.RowIndex][5]), Convert.ToInt32(HdnUsuario.Value));
                        Sale.StoreDetailsTMPTable(1, Convert.ToInt32(HdnEmpresa.Value), iNumeroVenta, 0, Convert.ToInt32(store.SelectedValue), Convert.ToDecimal(ProductsTable.Rows[row.RowIndex][5]), Convert.ToInt32(HdnUsuario.Value));


                        detalleguardadoCorrecto = GuardaDetalleVenta(3, row.RowIndex, iNumeroVenta, dNuevoPrecio);
                        //  Delete(row.RowIndex);//Borra de Cotizacion_Art_Descuentos
                        store.Dispose();
                    }

                    else if (Convert.ToInt32(ProductsTable.Rows[row.RowIndex][12]) != 0 && Convert.ToInt32(ProductsTable.Rows[row.RowIndex][11]) == 112)
                    {

                        detalleguardadoCorrecto = GuardaDetalleVenta(3, row.RowIndex, iNumeroVenta, dNuevoPrecio);
                        //  Delete(row.RowIndex);//Borra de Cotizacion_Art_Descuentos
                        store.Dispose();
                    }
                }
                else if (sSaleValidation == "CambiarEstatusGeneral" || sSaleValidation == "CambiarEstatusGeneralSE")
                {
                    if (Convert.ToInt32(ProductsTable.Rows[row.RowIndex][12]) == 0)
                    {
                        //if (Sale.IsProduct(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(ProductsTable.Rows[row.RowIndex][1]))) //checando
                        //{
                        //   Sale.StoreDetailsTMPTable(1, Convert.ToInt32(HdnEmpresa.Value), iSaleNumber, 0, Convert.ToInt32(generalstoreddl.SelectedValue), Convert.ToDecimal(ProductsTable.Rows[row.RowIndex][5]), Convert.ToInt32(HdnUsuario.Value));
                        //}

                        detalleguardadoCorrecto = GuardaDetalleVenta(1, row.RowIndex, iNumeroVenta, dNuevoPrecio);

                    }
                }

                else if (sSaleValidation == "NOTVAL")
                {

                    if (Convert.ToInt32(ProductsTable.Rows[row.RowIndex][12]) == 0)
                    {
                        detalleguardadoCorrecto = GuardaDetalleVenta(1, row.RowIndex, iNumeroVenta, dNuevoPrecio);
                        Delete(row.RowIndex);//Borra de Cotizacion_Art_Descuentos
                    }
                }
            }

            ProductsTable = Sale.GetDetailsTableFromSaleNumber(Convert.ToInt32(HdnEmpresa.Value), iNumeroVenta);
            return detalleguardadoCorrecto;
        }

        protected int sp_Cotizacion_Detalle_Caracteristicas(int index, int iNumeroVenta)
        {
            SqlConnection Conn;
            SqlCommand command;

            int returnval = -1;

            Conn = INTEGRA.SQL.get_sqlConnection(INTEGRA.Global.connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                command = new SqlCommand("sp_Cotizacion_DEtalle_Caracteristicas", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Tipo", 4);
                command.Parameters.AddWithValue("@Numero_Empresa", Convert.ToInt32(HdnEmpresa.Value));
                command.Parameters.AddWithValue("@Numero_Cotiza", iNumeroVenta);
                command.Parameters.AddWithValue("@Numero_Partida", DBNull.Value);
                command.Parameters.AddWithValue("@Clas_Producto", Convert.ToInt32(CharTable.Rows[index][1]));
                command.Parameters.AddWithValue("@Clas_Concepto", Convert.ToInt32(CharTable.Rows[index][2]));
                command.Parameters.AddWithValue("@Clas_Caract", Convert.ToInt32(CharTable.Rows[index][3]));
                command.Parameters.AddWithValue("@Cantidad", Convert.ToDecimal(CharTable.Rows[index][5]));
                command.Parameters.AddWithValue("@Familia_Concepto", Convert.ToInt32(CharTable.Rows[index][4]));
                command.Parameters.AddWithValue("@Familia_Caract", 3);

                returnval = command.ExecuteNonQuery();

            }
            catch (Exception sqlEx)
            {
                return -1;
            }
            finally
            {
                if (Conn != null)
                    Conn.Close();
            }

            return returnval;
        }

        private DataTable create_options_table()
        {
            DataTable tablaprueba = new DataTable();

            tablaprueba.Columns.Add("Numero", typeof(int));
            tablaprueba.Columns.Add("Descripcion", typeof(string));
            tablaprueba.Columns.Add("Ingreso", typeof(string));
            tablaprueba.Columns.Add("Referencia", typeof(string));
            tablaprueba.Columns.Add("TC", typeof(decimal));

            return tablaprueba;
        }

        private void ClearDoctoControls()
        {
            drpDocumento.Items.Clear();
            drpSerie.Items.Clear();
        }

        private void ManageControls(bool command)
        {
            RdBtnListProductos.Enabled = command;
            txtProductos.Enabled = command;
            drpMoneda.Enabled = command;
            drpMoneda.Enabled = command;
            drpFormadePago.Enabled = command;
            chkEntregaDirecta.Enabled = command;
            txtCliente.Enabled = command;
        }

        private void ManageCommandControls(bool command)
        {
            SaleBLL Sale = new SaleBLL();

            lnkCerrarVenta.Visible = command;
            lnkGuardarCotizacion.Visible = command;

            if (Convert.ToInt32(iNumeroVenta.Value) > 0)
            {
                if (Sale.GetStatusNumberFromSaleNumber(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value)) == 10)
                {
                    lnkImprimirDocumento.Text = "Imprimir cotización";
                    grvPartidas.Enabled = true;
                    lnkPagarVenta.Visible = false;
                }
                else
                {
                    lnkImprimirDocumento.Text = "Guardar factura";
                    grvPartidas.Enabled = false;
                }
            }
        }

        private void llenaDropsDocumentos()
        {
            InvoiceBLL Invoice = new InvoiceBLL();
            FillDocument();
            FillSerie();
            int iFolio = Invoice.GetFolio(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(drpDocumento.SelectedValue), Convert.ToInt32(drpSerie.SelectedValue));
            txtNumeroDocto.Text = iFolio.ToString();

            if (drpDocumento.SelectedValue == "-59")
            {
                txtNumeroDocto.Enabled = false;
            }
            else
            {
                txtNumeroDocto.Enabled = true;
            }
        }

        private void FillDocument()
        {
            InvoiceBLL Invoice = new InvoiceBLL();

            int sucursal = 0, relacion, registro;

            DataTable TempTable = new DataTable();
            DataTable TempPerson = new DataTable();
            DataTable TempOffice = new DataTable();
            DataTable TempClassDocto = new DataTable();

            if (Convert.ToInt32(iNumeroVenta.Value) != 0)
            {
                TempTable = Invoice.GetOfficeQuote(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));

                if (TempTable.Rows.Count > 0)
                {
                    sucursal = Convert.ToInt32(TempTable.Rows[0][0]);
                }
            }
            else
            {
                sucursal = Convert.ToInt32(HdnSucursal.Value);
            }

            DropTool.ClearDrop(drpDocumento);

            if (sucursal != 0)
            {
                TempPerson = Invoice.GetRelationship(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnCliente.Value));
                relacion = Convert.ToInt32(TempPerson.Rows[0][0]);

                if (relacion > 0)
                {
                    TempClassDocto = Invoice.GetClassDocto(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value));
                    registro = Convert.ToInt32(TempClassDocto.Rows[0][0]);
                    DropTool.FillDrop(drpDocumento, ControlsBLL.GetTypeDocument(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value)), "Descripcion", "Numero");

                }
                else
                {
                    TempClassDocto = Invoice.GetCountOfficeDocto(Convert.ToInt32(HdnEmpresa.Value), sucursal);
                    registro = Convert.ToInt32(TempClassDocto.Rows[0][0]);
                    DropTool.FillDrop(drpDocumento, ControlsBLL.GetDocument(Convert.ToInt32(HdnEmpresa.Value), sucursal), "Descripcion", "Numero");
                }
            }
            else
            {
                TempPerson = Invoice.GetRelationship(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value));
                relacion = Convert.ToInt32(TempPerson.Rows[0][0]);

                if (relacion > 0)
                {
                    TempClassDocto = Invoice.GetClassDocto(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value));
                    registro = Convert.ToInt32(TempClassDocto.Rows[0][0]);
                    DropTool.FillDrop(drpDocumento, TempClassDocto, "Descripcion", "Numero");

                }
                else
                {
                    DropTool.FillDrop(drpDocumento, ControlsBLL.GetTypeDocument(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnUsuario.Value)), "Descripcion", "Numero");
                }
            }
        }

        private void FillSerie()
        {
            DropTool.ClearDrop(drpSerie);

            if (drpDocumento.SelectedItem != null)
                DropTool.FillDrop(drpSerie, DevolucionController.ObtieneSerieDeNotaVenta(Convert.ToInt32(Session["Company"]),
                                  Convert.ToInt32(HdnUsuario.Value), Convert.ToInt32(drpDocumento.SelectedValue)), "Descripcion", "Numero");
        }
        private DataTable DatosDocto()
        {
            SaleBLL DatosDocumento = new SaleBLL();

            DatosDocumento.Company = Convert.ToInt32(HdnEmpresa.Value);
            DatosDocumento.Documento = Convert.ToInt32(drpDocumento.SelectedValue);
            DatosDocumento.Serie = Convert.ToInt32(drpSerie.SelectedValue);

            return DatosDocumento.GetDatosDocto();
        }

        private DataTable DatosDoctoE(int iNumeroFactura)
        {
            SaleBLL DatosDocumentoE = new SaleBLL();

            DatosDocumentoE.Company = Convert.ToInt32(HdnEmpresa.Value);
            DatosDocumentoE.Number = iNumeroFactura;
            DatosDocumentoE.Type = 0;

            return DatosDocumentoE.GetDatosDoctoE();
        }

        private DataTable DatosDoctoEmisor()
        {
            SaleBLL DatosDocumentoEmisor = new SaleBLL();

            DatosDocumentoEmisor.Company = Convert.ToInt32(HdnEmpresa.Value);

            return DatosDocumentoEmisor.GetDatosDoctoEmisor();
        }

        private DataTable DatosDoctoReceptor(int iNumeroFactura)
        {
            SaleBLL DatosDocumentoReceptor = new SaleBLL();

            DatosDocumentoReceptor.Company = Convert.ToInt32(HdnEmpresa.Value);
            DatosDocumentoReceptor.Number = iNumeroFactura;
            DatosDocumentoReceptor.Type = 0;

            return DatosDocumentoReceptor.GetDatosDoctoReceptor();
        }

        private DataTable DatosEmpresa()
        {
            SaleBLL DatosEmp = new SaleBLL();

            DatosEmp.Company = Convert.ToInt32(HdnEmpresa.Value);

            return DatosEmp.GetDatosEmpresa();
        }

        private DataTable DatosDoctoInternet()
        {
            SaleBLL DatosDocInternet = new SaleBLL();

            DatosDocInternet.Company = Convert.ToInt32(HdnEmpresa.Value);
            DatosDocInternet.Serie = Convert.ToInt32(drpSerie.SelectedValue);

            return DatosDocInternet.GetDatosInternet();
        }

        private DataTable DatosCorreoE()
        {
            SaleBLL DatosCorreo = new SaleBLL();

            DatosCorreo.Company = Convert.ToInt32(HdnEmpresa.Value);
            DatosCorreo.Customer = Convert.ToInt32(HdnCliente.Value);
            DatosCorreo.Type = 1;

            return DatosCorreo.GetDatosCorreo();
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
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de llave privada", System.Drawing.Color.DarkRed);

                }
                if (Convert.ToString(TablaDoctos.Rows[0][0]) != "")
                {
                    sRutaKey = Convert.ToString(TablaDoctos.Rows[0][0]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][1]) != "")
                {
                    sContraseñaKey = Convert.ToString(TablaDoctos.Rows[0][1]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró la contraseña del archivo de llave privada", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][2]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][3]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturó el año de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToInt32(TablaDoctos.Rows[0][3]) > 9999)
                {
                    LabelTool.ShowLabel(lblError, "Error,en la captura del año de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][4]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturó el número de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }
                if ((TablaDoctos.Rows[0][5]) != null && Convert.ToString(TablaDoctos.Rows[0][5].ToString()) != "")
                {
                    if (Convert.ToInt32(txtNumeroDocto.Text) > Convert.ToInt32(TablaDoctos.Rows[0][5]))
                    {
                        LabelTool.ShowLabel(lblError, "Error,el número de documento es mayor al folio final capturado en la configuración de documentos electrónicos", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }
                return true;
            }

            else
            {
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica de los folios para la operación de documentos electrónicos", System.Drawing.Color.DarkRed);
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
                        LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no puede ser menor a 12 caracteres", System.Drawing.Color.DarkRed);
                    }
                    if (rfc.Length > 13)
                    {
                        LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no puede ser mayor a 13 caracteres", System.Drawing.Color.DarkRed);
                    }
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }

                if (Convert.ToString(TablaEmpresa.Rows[0][1]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,el nombre del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][2]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,el domicilio del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][3]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,la colonia de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][4]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,la delegación/municipio de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][5]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,el estado de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][6]) == null)
                {
                    string cp = Convert.ToString(TablaEmpresa.Rows[0][6]);

                    if (cp.Length < 5)
                    {
                        LabelTool.ShowLabel(lblError, "Error,el código postal del emisor no puede ser menor a 5 caracteres", System.Drawing.Color.DarkRed);
                    }
                    if (cp.Length > 5)
                    {
                        LabelTool.ShowLabel(lblError, "Error,el código postal del emisor no puede ser mayor a 5 caracteres", System.Drawing.Color.DarkRed);
                    }
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][7]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,el régimen fiscal del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }

                return true;
            }
            else
            {
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica de la empresa para la operación de documentos electrónicos", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        private bool ValidacionDatosCliente()
        {
            DataTable TablaCliente;
            string rfc;
            int tipo;
            rfc = "";

            if ((TablaCliente = PersonBLL.ObtenDatosPersonaCliente(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(HdnCliente.Value))) != null)
            {
                if (Convert.ToString(TablaCliente.Rows[0][14]) != null && Convert.ToString(TablaCliente.Rows[0][14]) != "")
                {
                    sPersonalidad = Convert.ToString(TablaCliente.Rows[0][14]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error,el cliente no tiene un tipo de persona juridica", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaCliente.Rows[0][7]) != null && Convert.ToString(TablaCliente.Rows[0][7]) != "")
                {
                    rfc = Convert.ToString(TablaCliente.Rows[0][7]);

                    if (rfc.ToUpper() != "XEXX010101000")
                    {
                        if (rfc.Length < 12)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede ser menor a 12 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                        if (rfc.Length > 13)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede ser mayor a 13 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                }

                if (sPersonalidad == "F")
                {
                    tipo = 1;

                    if (rfc.Length != 13)
                    {
                        LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede ser diferente a 13 caracteres", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }
                else
                {
                    tipo = 0;

                    if (rfc.Length != 12)
                    {
                        LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede diferente a 12 caracteres", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (Convert.ToString(TablaCliente.Rows[0][2]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error,el nombre del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                }

                if (!RFCValidation(rfc, tipo))
                {
                    LabelTool.ShowLabel(lblError, "EL RFC no coincide con la validación, desea utilizarlo?", System.Drawing.Color.DarkRed);

                    return false;
                }

                DataTable DataAddress = GetAddress();

                if (DataAddress != null && DataAddress.Rows.Count > 0)
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
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica del cliente para la operacion de documentos electronicos", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        private bool ValidacionDatosDoctoElectronico(int iNumeroFactura)
        {
            DataTable TablaDoctosE;
            string sRutaKey;
            string sContraseñaKey;
            sRutaKey = string.Empty;
            sContraseñaKey = string.Empty;

            if ((TablaDoctosE = DatosDoctoE(iNumeroFactura)) != null)
            {
                if (Convert.ToString(TablaDoctosE.Rows[0][0]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de llave privada", System.Drawing.Color.DarkRed);

                }
                if (Convert.ToString(TablaDoctosE.Rows[0][0]) != "")
                {
                    sRutaKey = Convert.ToString(TablaDoctosE.Rows[0][0]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][1]) != "")
                {
                    sContraseñaKey = Convert.ToString(TablaDoctosE.Rows[0][1]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró la contraseña del archivo de llave privada", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][2]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][3]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturó el año de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToInt32(TablaDoctosE.Rows[0][3]) > 9999)
                {
                    LabelTool.ShowLabel(lblError, "Error,en la captura del año de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][4]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturó el número de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }
                return true;
            }

            else
            {
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración de los folios para la operación de documentos electronicos", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        private bool ValidacionDatosDoctoEmisor(int iNumeroFactura)
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
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no puede ser menor a 12 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                        if (rfc.Length > 13)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no puede ser mayor a 13 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no se ha capturado", System.Drawing.Color.DarkRed);
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
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica de los folios para la operacion de documentos electronicos", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        private bool ValidacionDatosDoctoReceptor(int iNumeroFactura)
        {
            DataTable TablaReceptor;
            string rfc;

            if ((TablaReceptor = DatosDoctoReceptor(iNumeroFactura)) != null)
            {
                if (TablaReceptor.Rows[0][0] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][0]) != "")
                    {
                        rfc = Convert.ToString(TablaReceptor.Rows[0][0]);

                        if (rfc.Length < 12)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede ser menor a 12 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                        if (rfc.Length > 13)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede ser mayor a 13 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no se ha capturado", System.Drawing.Color.DarkRed);
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
                    int porcentaje;
                    int ifamily = 3;
                    SaleBLL Invoice = new SaleBLL();

                    int iva = Convert.ToInt32(TablaReceptor.Rows[0][7]);
                    DataTable Tporcentaje = Invoice.GetIvaReceptor(Convert.ToInt32(HdnEmpresa.Value), iva, ifamily);
                    if (Tporcentaje != null)
                    {
                        porcentaje = Convert.ToInt32(Tporcentaje.Rows[0][0]);
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
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica del cliente para la operacion de documentos electronicos", System.Drawing.Color.DarkRed);
                return false;
            }

        }

        private bool ValidacionDatosElectronico(int iNumeroFactura)
        {
            if (ValidacionDatosDoctoElectronico(iNumeroFactura))
            {
                if (ValidacionDatosDoctoEmisor(iNumeroFactura))
                {
                    if (ValidacionDatosDoctoReceptor(iNumeroFactura))
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
        private bool ValidacionDatosMailCompany()
        {
            DataTable TablaMail;
            SaleBLL Sale = new SaleBLL();

            if ((TablaMail = Sale.GetStoresValidationsAndMailCompany(Convert.ToInt32(HdnEmpresa.Value))) != null)
            {
                if (Convert.ToString(TablaMail.Rows[0][4]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error, falta dato cuenta de correo del usuario", System.Drawing.Color.DarkRed);
                    return false;
                }
                if (Convert.ToString(TablaMail.Rows[0][6]) == null)
                {
                    LabelTool.ShowLabel(lblError, "Error, falta dato cuenta de correo del usuario", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaMail != null)
                {
                    if (Convert.ToInt32(TablaMail.Rows[0][5]) == 1)
                        if (Convert.ToString(TablaMail.Rows[0][7]) == null)
                        {
                            LabelTool.ShowLabel(lblError, "Error, falta dato en la contraseña del correo del usuario", System.Drawing.Color.DarkRed);
                            return false;
                        }
                }
                return true;
            }

            else
            {
                LabelTool.ShowLabel(lblError, "Error, el usuario no tiene configurado su correo", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        private bool RFCValidation(string rfc, int tipo)
        {
            IntegraBussines.CustomerBLL Customer = new IntegraBussines.CustomerBLL();
            return Customer.RFCValidation(rfc, tipo);
        }



        private void ProductosSinExistencia(DataTable DeniedProductsTable)
        {
            messagelbl.Text = "No existe la cantidad solicitada en el almacén para el producto:<br><br>";

            foreach (DataRow DeniedRow in DeniedProductsTable.Rows)
                messagelbl.Text += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;==>    " + DeniedRow["Product"].ToString() + "<br>";

            messagemdl.Show();
        }

        private bool DeniedProducts(int index)
        {
            SalesStoresBLL Store = new SalesStoresBLL();
            SaleBLL Sale = new SaleBLL();
            return Store.DeniedProducts(1, Convert.ToInt32(HdnEmpresa.Value), 1, Convert.ToInt32(iNumeroVenta.Value), index, Convert.ToInt32(generalstoreddl.SelectedValue), Sale.GetFhaterNumberFromChildNumber(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(ProductsTable.Rows[index][1]), 1), Convert.ToInt32(ProductsTable.Rows[index][1]), Convert.ToInt32(ProductsTable.Rows[index][0]), Convert.ToInt32(ProductsTable.Rows[index][2]), Convert.ToInt32(ProductsTable.Rows[index][5]), Convert.ToDateTime(DateTime.Now.ToShortDateString()), 1, Convert.ToInt32(ProductsTable.Rows[index][3]));
        }

        private DataTable GetProductMissedTable()
        {
            DataTable ProductMissedTable = new DataTable();

            ProductMissedTable.Columns.Add("Product");

            return ProductMissedTable;
        }

        private int GetGeneralStock(int index, int Store)
        {
            DataTable StockTable;
            SaleBLL sale = new SaleBLL();
            int ifamily = 1;

            using (StockTable = sale.GetStock(Convert.ToInt32(HdnEmpresa.Value), Store, Convert.ToInt32(ProductsTable.Rows[index][1]), Convert.ToInt32(ProductsTable.Rows[index][2]), ifamily))
            {
                if (StockTable != null && StockTable.Rows.Count > 0)
                {
                    int stock = Convert.ToInt32(StockTable.Rows[0][0]);

                    if (stock > 0 && stock > Convert.ToInt32(ProductsTable.Rows[index][5]))
                        return stock;
                }
            }
            return 0;
        }

        protected bool change(int iNumeroFactura)
        {
            bool bresult = false;

            DataTable ChangeTable;

            if (bvalidacambio(iNumeroFactura))
            {
                foreach (GridViewRow row in grvCambio.Rows)
                {
                    TextBox ingresotxt = (TextBox)grvCambio.Rows[row.RowIndex].FindControl("txtIngreso");
                    decimal dingreso = Convert.ToDecimal(ingresotxt.Text.Replace('$', ' '));

                    if (dingreso > 0)
                    {
                        bresult = CreateChange(row, iNumeroFactura);
                        if (bresult == false)
                        {
                            return false;
                        }
                    }
                }

                InvoiceBLL Invoice = new InvoiceBLL();
                ChangeTable = Invoice.CalculaImporteCxC(Convert.ToInt32(HdnEmpresa.Value), iNumeroFactura);

                if (ChangeTable != null)
                {
                    if (ChangeTable.Rows.Count > 0)
                    {
                        if (ChangeTable.Rows[0][0] != null && Convert.ToString(ChangeTable.Rows[0][0]) != "")
                        {
                            dCambio = Convert.ToDecimal(ChangeTable.Rows[0][0]);

                            if (dCambio != Convert.ToDecimal(txtTotalPago.Text.Replace('$', ' ')))
                            {
                                LabelTool.ShowLabel(lblMensajeCambio, "El cambio capturado es incorrecto", System.Drawing.Color.DarkRed);

                            }
                        }
                    }
                }
                mdlCambio.Hide();
                return true;
            }
            else
            {
                LabelTool.ShowLabel(lblMensajeCambio, "No se ha seleccionado un concepto", System.Drawing.Color.DarkRed);

                return false;
            }
        }

        private bool CreaFactura()
        {
            InvoiceBLL Invoice = new InvoiceBLL();
            DataTable temp_table = new DataTable();

            int Result;
            string Error;
            try
            {
                Result = Invoice.CreateInvoice(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value), Convert.ToInt32(drpDocumento.SelectedValue), Convert.ToInt32(drpSerie.SelectedValue), Convert.ToInt32(txtNumeroDocto.Text), Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToInt32(HdnCliente.Value));

                if (Result == 1)
                {
                    return true;
                }
                else if (Result == 0)
                {
                    Error = Invoice.GetExepcion();
                    if (Error == "")
                    {
                        LabelTool.ShowLabel(lblError, "Error al cerrar la venta , intente nuevamente", System.Drawing.Color.DarkRed);
                    }
                    else
                    {
                        LabelTool.ShowLabel(lblError, Error, System.Drawing.Color.DarkRed);
                    }

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoRefresh();", true);
                    return false;
                }
                else if (Result == -1)
                {
                    LabelTool.ShowLabel(lblError, "Error al conseguir el domicilio fiscal del cliente , intente nuevamente", System.Drawing.Color.DarkRed);

                    return false;
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error al insertar la factura , intente nuevamente", System.Drawing.Color.DarkRed);

                    return false;
                }
            }
            catch (Exception sqlEx)
            {
                LabelTool.ShowLabel(lblError, sqlEx.Message, System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfRefresca();", true);
            }

            return false;
        }


        private bool CreatePay(GridViewRow row, int iNumeroFactura)
        {
            int itype = 1;

            if (PayInvoice(itype, row, iNumeroFactura))
            {
                return true;
            }
            else
            {
                itype = 4;

                PayInvoice(itype, row, iNumeroFactura);

                return false;
            }
        }

        private bool CreateChange(GridViewRow row, int iNumeroFactura)
        {
            int itype = 5;

            if (PayInvoice(itype, row, iNumeroFactura))
            {
                return true;
            }
            else
            {
                itype = 4;

                PayInvoice(itype, row, iNumeroFactura);

                return false;
            }
        }


        private void PrintSaleReport(ReportDocument reporte)
        {
            try
            {
                reporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, this.Response, true, Reports.GetReportName("COTIZACIÓN", iNumeroVenta.Value));

            }
            catch (Exception ex)
            {
                Label lbl = new Label();
                LOG.gfLog(HttpContext.Current.Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), "PrintSaleReport", ref lbl);

            }
            finally
            {

            }
        }

        private void PrintInvoice(ReportDocument reporte)
        {
            try
            {
                reporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, this.Response, true, Reports.GetReportName("Nota de venta", iNumeroVenta.Value));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
            }
        }

        private void ImprimeCotizacion()
        {
            Session["Rutaelectronico"] = "";
            DataTable GeneralSaleTable;
            using (GeneralSaleTable = CreateReportSaleTable())
            {
                FillGeneralSaleTable(ref GeneralSaleTable);
                ReportDocument document = new ReportDocument();
                document = Reports.GetInvoiceReport(HdnEmpresa.Value, ProductsTable, GeneralSaleTable);
                document.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                document.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();

                string sfolderName = WebConfigurationManager.AppSettings["sfolderNameReportes"].ToString();
                string sfecha = Convert.ToString(DateTime.Now.Month + "-" + DateTime.Now.Year);
                string suser = Convert.ToString(HdnUsuario.Value);
                string spathString = System.IO.Path.Combine(sfolderName, INTEGRA.Global.DataBase, HdnEmpresa.Value, sfecha, suser);
                System.IO.Directory.CreateDirectory(spathString);
                objDiskOpt.DiskFileName = @spathString + @"\COTIZACION.pdf";
                string rutadoc = "../DocumentosElectronicos/Temp/DocumentosVenta" + "/" + INTEGRA.Global.DataBase + "/" + HdnEmpresa.Value + "/" + sfecha + "/" + suser + "/" + "COTIZACION.pdf";

                document.ExportOptions.DestinationOptions = objDiskOpt;
                document.Export();

                lblError.Text = "";
                Session["Rutaelectronico"] = rutadoc;
                hdnRuta.Value = rutadoc;
                PrintSaleReport(document);

            }

        }

        private void ImprimeNotadeVenta()
        {
            InvoiceBLL Invoice = new InvoiceBLL();
            DataTable InvoiceTable;
            DataTable ReportSaleTable;
            int ifather = 1;
            Session["Rutaelectronico"] = "";
            int InvoiceNumber = Invoice.GetInvoiceNumber(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));

            using (InvoiceTable = InvoiceBLL.ObtieneDetallesFactura(Convert.ToInt32(HdnEmpresa.Value), InvoiceNumber, Convert.ToInt32(HdnCliente.Value), ifather))
            {
                using (ReportSaleTable = CreateReportSaleTable())
                {
                    FillReportSaleTable(ReportSaleTable, InvoiceTable);

                    ReportDocument document = new ReportDocument();
                    document = Reports.GetInvoiceReport(HdnEmpresa.Value, ProductsTable, ReportSaleTable);
                    document.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    document.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();

                    string sfolderName = WebConfigurationManager.AppSettings["sfolderNameReportes"].ToString();

                    string sfecha = Convert.ToString(DateTime.Now.Month + "-" + DateTime.Now.Year);
                    string suser = Convert.ToString(HdnUsuario.Value);
                    string spathString = System.IO.Path.Combine(sfolderName, INTEGRA.Global.DataBase, HdnEmpresa.Value, sfecha, suser);

                    System.IO.Directory.CreateDirectory(spathString);
                    objDiskOpt.DiskFileName = @spathString + @"\COTIZACION.pdf";

                    string rutadoc = "../DocumentosElectronicos/Temp/DocumentosVenta" + "/" + INTEGRA.Global.DataBase + "/" + HdnEmpresa.Value + "/" + sfecha + "/" + suser + "/" + "COTIZACION.pdf";

                    Session["Rutaelectronico"] = rutadoc;
                    document.ExportOptions.DestinationOptions = objDiskOpt;
                    document.Export();
                    lblError.Text = "";
                    PrintInvoice(document);

                }
            }
        }
        public void export(ReportDocument report, string name)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            report.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, this.Response, true, name);
            Response.Flush();
            Response.Close();
        }

        public void GeneraFacturaEnPopUp(string sRuta)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Window1", "<script> window.open('" + sRuta + "',''," +
            "'top=350,left=400,height=400,width=500,status=yes,toolbar=no,menubar=no,location=no resizable=yes');</script>", false);
        }

        private void FillGeneralSaleTable(ref DataTable GeneralSaleTable)
        {
            toolsBLL importeletra = new toolsBLL();
            DataTable Importletra;

            SaleBLL Sale = new SaleBLL();
            DataRow SaleRow = GeneralSaleTable.NewRow();

            SaleRow["Cliente"] = lblNombrecliente.Text;
            SaleRow["Direccion"] = lblCalle.Text + ", " + lblColonia.Text + ", " + lblEstado.Text;
            SaleRow["RFC"] = lblRfc.Text;
            SaleRow["Subtotal"] = Convert.ToDecimal(txtSubtotal.Text.Replace('$', ' '));
            SaleRow["IVA"] = Convert.ToDecimal(txtIva.Text.Replace('$', ' '));
            SaleRow["Total"] = Convert.ToDecimal(txtTotal.Text.Replace('$', ' '));
            SaleRow["DocTo"] = "COTIZACIÓN";
            SaleRow["Denominacion"] = drpMoneda.SelectedItem.ToString();
            SaleRow["Factura"] = iNumeroVenta.Value;
            Importletra = importeletra.import_to_string(Convert.ToInt32(HdnEmpresa.Value), Convert.ToDecimal(txtTotal.Text.Replace('$', ' ')), drpMoneda.SelectedItem.ToString(), 0);
            SaleRow["Importe"] = Convert.ToString(Importletra.Rows[0][0]);
            SaleRow["Estatus"] = Sale.GetStatusDescriptionFromSaleNumber(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value));
            SaleRow["Fecha"] = DateTime.Now.ToShortDateString();

            GeneralSaleTable.Rows.Add(SaleRow);
        }

        private void TransferFile(string filepath, string filename)
        {
            FileInfo file = new FileInfo(filepath + filename);

            if (file.Exists)
            {
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.TransmitFile(file.FullName);
                Response.End();
            }
        }

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

        //    if ((DataMail = ControlsBLL.GetDataMail(Convert.ToInt32(HdnEmpresa.Value))) == null)
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
        //                iPuerto = Convert.ToInt32(DataMail.Rows[0][1]);
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

        //    SmtpMail Mail = new SmtpMail(sServidor, iPuerto, sMail, sContraseña, recipients);

        //    Mail.From = sMail;
        //    Mail.Message = Message;
        //    Mail.Subject = Subject;

        //    return Mail;
        //}


        private void FillReportSaleTable(DataTable ReportSaleTable, DataTable InvoiceTable)
        {
            IntegraBussines.CustomerBLL ClientBAL = new IntegraBussines.CustomerBLL();
            DataTable ClientDataTable;
            DataTable FiscalAddressTable;
            int itypeperson = 13;

            using (ClientDataTable = ClientBAL.GetPersonData(Convert.ToInt32(HdnEmpresa.Value),
                                                             Convert.ToInt32(HdnCliente.Value)))
            {
                using (FiscalAddressTable = PersonBLL.ObtenDomicilioPersonaPorTipoDomicilio(Convert.ToInt32(HdnCliente.Value), itypeperson))
                {
                    DataRow ReportRow = ReportSaleTable.NewRow();

                    if (FiscalAddressTable.Rows.Count > 0)
                        ReportRow["Direccion"] = FiscalAddressTable.Rows[0][2].ToString() +
                                                 ", " +
                                                 FiscalAddressTable.Rows[0][3].ToString() +
                                                 ", " +
                                                 FiscalAddressTable.Rows[0][4].ToString();
                    else
                        ReportRow["Direccion"] = " ";

                    ReportRow["Cliente"] = ClientDataTable.Rows[0][2].ToString();
                    ReportRow["RFC"] = ClientDataTable.Rows[0][7];
                    ReportRow["SubTotal"] = InvoiceTable.Rows[0][1].ToString();
                    ReportRow["IVA"] = InvoiceTable.Rows[0][3].ToString();
                    ReportRow["Total"] = InvoiceTable.Rows[0][2].ToString();
                    ReportRow["DocTo"] = InvoiceTable.Rows[0][6].ToString();
                    ReportRow["Denominacion"] = drpMoneda.SelectedItem.ToString();
                    ReportRow["Factura"] = InvoiceTable.Rows[0][0].ToString();
                    ReportRow["Importe"] = InvoiceTable.Rows[0][4].ToString();
                    ReportRow["Estatus"] = InvoiceTable.Rows[0][5].ToString();
                    ReportRow["Fecha"] = InvoiceTable.Rows[0][7].ToString();
                    ReportSaleTable.Rows.Add(ReportRow);
                }
            }
        }

        private DataTable CreateReportSaleTable()
        {
            DataTable table;

            using (table = new DataTable())
            {
                table.Columns.Add("Cliente");
                table.Columns.Add("Direccion");
                table.Columns.Add("RFC");
                table.Columns.Add("SubTotal");
                table.Columns.Add("IVA");
                table.Columns.Add("Total");
                table.Columns.Add("DocTo");
                table.Columns.Add("Denominacion");
                table.Columns.Add("Factura");
                table.Columns.Add("Importe");
                table.Columns.Add("EStatus");
                table.Columns.Add("Fecha");
            }
            return table;
        }




        private bool CierraVenta()
        {
            SaleBLL Sale = new SaleBLL();

            int ifamily;
            int ifather;
            ifamily = 3;
            ifather = 9;
            int iStatus = Sale.GetStatusNumberFromDescription("Aceptada", ifamily, ifather);


            if (sSaleValidation == "AgregarPartida" || sSaleValidation == "AgregarPartidaSE")
            {
                iNumeroVenta.Value = GuardaVenta(Convert.ToInt32(iNumeroVenta.Value), iStatus).ToString();
                if (iNumeroVenta.Value == "0")
                    return false;

                disable_quotes();
            }
            else if (sSaleValidation == "CambiarEstatusPartida" || sSaleValidation == "CambiarEstatusPartidaSE")
            {
                if (sProductosNoValidos == "" || sProductosNoValidos == null)
                {
                    iNumeroVenta.Value = GuardaVenta(Convert.ToInt32(iNumeroVenta.Value), iStatus).ToString();
                    if (iNumeroVenta.Value == "0")

                        return false;

                    if (!Sale.AwayProducts(1, Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(iNumeroVenta.Value)))
                        LabelTool.ShowLabel(lblError, "Error al apartar los articulos, hagalo manualmente", System.Drawing.Color.DarkRed);

                    disable_quotes();
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Los Producto(s): " + sProductosNoValidos + " no cuenta con costo de referencia en la zona del almacén seleccionado, seleccione otro almacén", System.Drawing.Color.DarkRed);
                    return false;
                }
            }

            else if (sSaleValidation == "CambiarEstatusGeneral")
            {
                DataTable DeniedProductsTable;
                generalstoremdl.Hide();
                iNumeroVenta.Value = GuardaVenta(Convert.ToInt32(iNumeroVenta.Value), iStatus).ToString();
                if (iNumeroVenta.Value == "0")
                    return false;
                DeniedProductsTable = GetProductMissedTable();

                for (int i = 0; i < ProductsTable.Rows.Count; i++)
                {
                    if (Convert.ToInt32(ProductsTable.Rows[i][11]) == 113)
                    {
                        if (GetGeneralStock(i, Convert.ToInt32(generalstoreddl.SelectedValue)) <= 0)
                        {
                            DataRow ProductRow = DeniedProductsTable.NewRow();
                            ProductRow["Product"] = ProductsTable.Rows[i][9];
                            DeniedProductsTable.Rows.Add(ProductRow);
                            DeniedProducts(i);
                        }
                    }
                }

                if (DeniedProductsTable.Rows.Count > 0)
                {
                    ProductosSinExistencia(DeniedProductsTable);
                    iStatus = Sale.GetStatusNumberFromDescription("Iniciada", ifamily, ifather);
                    GuardaVenta(Convert.ToInt32(iNumeroVenta.Value), iStatus);
                    invoicemdl.Hide();
                    return false;
                }
            }
            else if (sSaleValidation == "CambiarEstatusGeneralSE" || sSaleValidation == "NOTVAL")
            {
                generalstoremdl.Hide();
                iNumeroVenta.Value = GuardaVenta(Convert.ToInt32(iNumeroVenta.Value), iStatus).ToString();
                if (iNumeroVenta.Value == "0")
                    return false;
            }
            return true;
        }

        private void GetStores()
        {
            int index = 0;
            int ifamstock = 1;

            if (ProductsTable != null && ProductsTable.Rows.Count > 0)
            {
                for (index = 0; index < grvPartidas.Rows.Count; index++)
                {
                    DropDownList drp = (DropDownList)grvPartidas.Rows[index].FindControl("storeDropDownList");

                    if (drp != null && drp.Visible && Convert.ToInt32(ProductsTable.Rows[index][12]) == 0)// checar sale error no es visible drop y lo toma como visible
                    {
                        SaleBLL Sale = new SaleBLL();
                        drp.SelectedValue = ProductsTable.Rows[index][7].ToString();
                        grvPartidas.Rows[index].Cells[1].Text = Sale.GetStockStore(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(drp.SelectedValue),
                                                                                    Convert.ToInt32(ProductsTable.Rows[index][1]), Convert.ToInt32(ProductsTable.Rows[index][2]),
                                                                                    ifamstock).ToString();
                    }
                }
            }
        }

        private void GetStores_productsgrid()
        {
            int index = 0;
            int ifamstock = 1;

            if (ProductsTable != null && ProductsTable.Rows.Count > 0)
            {
                DropDownList drp = (DropDownList)GrvProductos.Rows[index].FindControl("storeDropDownList");

                if (drp != null && drp.Visible && Convert.ToInt32(ProductsTable.Rows[index][7]) == 0)
                {
                    SaleBLL Sale = new SaleBLL();

                    if (((string[])Session[_Stores])[index] != "" && ((string[])Session[_Stores])[index] != null)
                    {
                        drp.SelectedValue = ((string[])Session[_Stores])[index];
                        GrvProductos.Rows[index].Cells[4].Text = Sale.GetStockStore(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(drp.SelectedValue),
                                                                                    Convert.ToInt32(ProductsTable.Rows[index][1]), Convert.ToInt32(ProductsTable.Rows[index][2]),
                                                                                    ifamstock).ToString();
                    }
                    else
                    {
                        GrvProductos.Rows[index].Cells[4].Text = "Sin disponibles";
                    }
                }
            }
        }

        private void check_products_table()//Valida que exista cantidad suficiente del producto en por lo menos un almacén 
        {
            DataTable ProductsTableTMP = new DataTable();

            ProductsTableTMP.Columns.Add("product");

            for (int i = 0; i < ProductsTable.Rows.Count; i++)
            {
                if (INTEGRA.Global.Product_type == 113)
                {
                    if ((filter_stock_stores(i)).Rows.Count <= 0)
                    {
                        DataRow row = ProductsTableTMP.NewRow();

                        row["product"] = ProductsTable.Rows[i][9];

                        ProductsTableTMP.Rows.Add(row);

                        ProductsTable.Rows.RemoveAt(i--);
                    }
                }
                else //es servicio
                {
                    //pasos cuando es servicio
                }
            }

            if (ProductsTableTMP.Rows.Count > 0)
            {
                messagelbl.Text = "No existe la cantidad solicitada en ninguno <br> de los almacenes para el producto:<br><br>";

                foreach (DataRow ProductsRowTMP in ProductsTableTMP.Rows)
                {
                    string producto = ProductsRowTMP["product"].ToString();
                    messagelbl.Text += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;==>    " + ProductsRowTMP["product"].ToString() + "<br>";
                    messagemdl.Show();
                    charmdl.Hide();
                }
            }
            else
            {
                messagelbl.Text = "";
            }
        }





        private void add_Totals_columns(ref int index)
        {

            foreach (GridViewRow row in GrvProductos.Rows)
            {
                TextBox txt = (TextBox)row.FindControl("checkTextBox");

                if (txt != null)
                {
                    if (txt.Text != "")
                    {
                        TempProductsTable.Rows[index][5] = Convert.ToDecimal(txt.Text);
                        TempProductsTable.Rows[index][6] = Convert.ToDecimal(Convert.ToDecimal(TempProductsTable.Rows[index][5]) *
                                                                         Convert.ToDecimal(TempProductsTable.Rows[index][3]));
                        DropDownList store;
                        store = (DropDownList)row.FindControl("storeDropDownList");

                        if (sSaleValidation != "CambiarEstatusGeneral" && sSaleValidation != "CambiarEstatusGeneralSE" && sSaleValidation != "NOTVAL")
                        {
                            if (INTEGRA.Global.Product_type != 112)
                            {
                                TempProductsTable.Rows[index][7] = Convert.ToInt32(store.SelectedValue);
                                TempProductsTable.Rows[index][12] = 0;
                            }
                            else
                            {
                                TempProductsTable.Rows[index][7] = 0;
                                TempProductsTable.Rows[index][12] = 0;
                            }
                        }

                        else
                        {
                            TempProductsTable.Rows[index][7] = 0;
                            TempProductsTable.Rows[index][12] = 0;
                        }

                    }
                    else
                    {

                        TempProductsTable.Rows.RemoveAt(index);
                        index--;
                    }
                }

                index++;
            }

        }


        private DataTable GetInvoices()
        {
            SaleBLL TableInvoices = new SaleBLL();

            TableInvoices.Company = Convert.ToInt32(HdnEmpresa.Value);
            TableInvoices.Customer = Convert.ToInt32(HdnCliente.Value);
            TableInvoices.Documento = -59;
            TableInvoices.Type = 1;

            return TableInvoices.GetInvoices();
        }

        private DataTable sp_Cuentas_Cobrar_TraeConcepto_Tienda_C(int iNumeroFactura)
        {
            SaleBLL TableConcept = new SaleBLL();

            TableConcept.Company = Convert.ToInt32(HdnEmpresa.Value);
            TableConcept.Number = iNumeroFactura;

            return TableConcept.GetTraeConcepto_Tienda_C();
        }

        private DataTable GetAddress()
        {
            SaleBLL TableAddress = new SaleBLL();

            TableAddress.Company = Convert.ToInt32(HdnEmpresa.Value);
            TableAddress.Customer = Convert.ToInt32(HdnCliente.Value);
            TableAddress.Type = 13;

            return TableAddress.GetAddress();
        }

        private void add_Totals_columns_products(ref int index)
        {
            if (TempProductsTable.Rows.Count > 0)
            {
                DataColumn concept_column;

                concept_column = TempProductsTable.Columns.Add("Cantidad");
                concept_column.SetOrdinal(5);
                concept_column = TempProductsTable.Columns.Add("Total");
                concept_column.SetOrdinal(6);
                concept_column = TempProductsTable.Columns.Add("Numero");
                concept_column.SetOrdinal(7);
                concept_column = TempProductsTable.Columns.Add("Partida");
                concept_column.SetOrdinal(12);

            }

            foreach (GridViewRow row in GrvProductos.Rows)
            {
                TextBox txt = (TextBox)row.FindControl("checkTextBox");

                if (txt != null)
                {
                    TempProductsTable.Rows[index][5] = 0;
                    TempProductsTable.Rows[index][6] = 0;
                    TempProductsTable.Rows[index][7] = 0;
                    TempProductsTable.Rows[index][12] = 0;
                }
                else
                {
                    TempProductsTable.Rows.RemoveAt(index);

                    index--;
                }
                index++;
            }
        }

        private void fill_store_controls(int index)
        {
            if (index > 0)
            {

                GridViewTool.Bind(ProductsTable, grvPartidas);

                if (sSaleValidation == "AgregarPartida" ||
                    sSaleValidation == "AgregarPartidaSE" ||
                    sSaleValidation == "CambiarEstatusPartida" ||
                    sSaleValidation == "CambiarEstatusPartidaSE")
                {
                    get_stores_for_gamequote();

                    StoresControls();
                }
            }
        }

        private void fill_store_controls_productsgrid(int index)
        {
            if (sSaleValidation == "AgregarPartida" ||
                sSaleValidation == "AgregarPartidaSE" ||
                sSaleValidation == "CambiarEstatusPartida" ||
                sSaleValidation == "CambiarEstatusPartidaSE")
            {
                get_stores_for_gamequoteproducts();
                StoresControlsProducts();
            }
        }

        private void fill_products_table()
        {
            foreach (DataRow row in TempProductsTable.Rows)
                ProductsTable.ImportRow(row);
        }

        private void LLenaProductsTable()
        {
            int intConcepto = 0, intAlmacen = 0, intResultado = 0;
            decimal dCantidad = 0;

            if (ProductsTable.Rows.Count > 0)
            {
                foreach (DataRow row in TempProductsTable.Rows)
                {
                    intConcepto = Convert.ToInt32(row["Concepto"]);
                    intAlmacen = Convert.ToInt32(row["Numero"]);
                    dCantidad = Convert.ToInt32(row["Cantidad"]);
                    intResultado = fill_products_table3(intConcepto, intAlmacen, dCantidad);


                    if (intResultado == 0)
                    {
                        ProductsTable.ImportRow(row);
                    }

                }
            }
            else
            {
                foreach (DataRow row in TempProductsTable.Rows)
                    ProductsTable.ImportRow(row);
            }

        }
        private int fill_products_table3(int intConcepto, int intAlmacen, decimal dCantidad)
        {
            int intConceptoTP = 0, intAlmacenTP = 0;
            decimal dCantidadTP = 0, dNuevaCantidad = 0,
                    dPrecio = 0, dNuevoTotal = 0;
            int index = 0;
            foreach (DataRow row in ProductsTable.Rows)
            {

                intConceptoTP = Convert.ToInt32(row["Concepto"]);
                intAlmacenTP = Convert.ToInt32(row["Numero"]);

                if (intConcepto == intConceptoTP && intAlmacen == intAlmacenTP)
                {
                    dCantidadTP = Convert.ToDecimal(row["Cantidad"]);
                    dNuevaCantidad = dCantidad + dCantidadTP;
                    dPrecio = Convert.ToDecimal(row["Precio"]);
                    dNuevoTotal = dNuevaCantidad * dPrecio;
                    //meter una nueva columna con los datos nuevos

                    DataRow nueva = ProductsTable.NewRow();

                    nueva["Tipo_Concepto"] = row["Tipo_Concepto"];
                    nueva["Producto"] = row["Producto"];
                    nueva["Concepto"] = row["Concepto"];
                    nueva["Precio"] = row["Precio"];
                    nueva["Unidad"] = row["Unidad"];
                    nueva["Cantidad"] = dNuevaCantidad;
                    nueva["Total"] = dNuevoTotal;
                    nueva["Numero"] = row["Numero"];
                    nueva["Empresa"] = row["Empresa"];
                    nueva["Descripcion_Producto"] = row["Descripcion_Producto"];
                    nueva["Descripcion_Unidad"] = row["Descripcion_Unidad"];
                    nueva["Tipo"] = row["Tipo"];
                    nueva["Partida"] = row["Partida"];
                    nueva["PorcentajeIVA"] = row["PorcentajeIVA"];
                    nueva["IVAParametrizado"] = row["IVAParametrizado"];
                    ProductsTable.Rows.Add(nueva);

                    ProductsTable.Rows.RemoveAt(index);

                    return 1;
                }

                index++;

            }

            return 0;

        }

        public void TerminaProceso()
        {
            ManageControls(false);
            grvPartidas.Enabled = false;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "Refresh();", true);
        }


        protected void linkTestII_Click(object sender, EventArgs e)
        {
            salemsgmdl.Hide();

        }


        #endregion


        #region Redirect
        protected void RePrintClick(object sender, EventArgs e)
        {
            Response.Redirect("ReimpresionFactura.aspx?Client=" + HdnCliente.Value.ToString());
        }

        protected void FacturaLibreClick(object sender, EventArgs e)
        {
            Response.Redirect("FacturaLibreMorales.aspx");
        }
        protected void FacturaGobiernoClick(object sender, EventArgs e)
        {
            Response.Redirect("FacturaCapturaLibreMorales.aspx");
        }

        protected void FacturaProgramadaClick(object sender, EventArgs e)
        {
            Response.Redirect("VentasProgramadas.aspx");
        }

        protected void linkLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ventas.aspx");
        }

        protected void lnkBuscaVentas_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchSales2.aspx?Client=" + HdnCliente.Value.ToString() + "&Company=" + HdnEmpresa.Value.ToString() + "&Person=" + HdnUsuario.Value.ToString());
        }
        protected void AddClients(object sender, EventArgs e)
        {
            Response.Redirect("Clientes.aspx?Moviment=Client");
        }

        protected void Mensaje_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevoMensaje.aspx");
        }


        #endregion
        #region FacturaMorales
        private bool validacionpara_e_docto(int iFolio)
        {
            if (ValidacionDatos(iFolio))
            {
                TablaCorreoCliente = DatosCorreoCliente();
                if (TablaCorreoCliente != null && TablaCorreoCliente.Rows.Count > 0)
                {
                    if (Convert.ToString(TablaCorreoCliente.Rows[0][0]) == "")
                    {
                        LabelTool.ShowLabel(lblError, "Error, no se ha encontrado correo electronico de los contactos del cliente", System.Drawing.Color.DarkRed);
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

        private bool ValidacionDatos(int iFolio)
        {
            if (ValidacionDatosDocto(iFolio))// 
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

        private bool ValidacionDatosDocto(int iFolio)
        {
            string sRutaKey = "";
            string sContraseñaKey = "";
            DataTable TablaDoctos;

            if ((TablaDoctos = DatosDocto()) != null)
            {
                if (Convert.ToString(TablaDoctos.Rows[0][0]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de llave privada", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaDoctos.Rows[0][0]) != "")
                {
                    sRutaKey = Convert.ToString(TablaDoctos.Rows[0][0]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado", System.Drawing.Color.DarkRed);
                    return false;
                }
                if (Convert.ToString(TablaDoctos.Rows[0][1]) != "")
                {
                    sContraseñaKey = Convert.ToString(TablaDoctos.Rows[0][1]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró la contraseña del archivo de llave privada", System.Drawing.Color.DarkRed);
                    return false;
                }
                if (Convert.ToString(TablaDoctos.Rows[0][2]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado", System.Drawing.Color.DarkRed);
                    return false;
                }
                if (Convert.ToString(TablaDoctos.Rows[0][3]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturó el año de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }
                if (Convert.ToInt32(TablaDoctos.Rows[0][3]) > 9999)
                {
                    LabelTool.ShowLabel(lblError, "Error,en la captura del año de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }
                if (Convert.ToString(TablaDoctos.Rows[0][4]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturó el número de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }
                if ((TablaDoctos.Rows[0][5]) != null && Convert.ToString(TablaDoctos.Rows[0][5].ToString()) != "")
                {
                    if (iFolio > Convert.ToInt32(TablaDoctos.Rows[0][5]))
                    {
                        LabelTool.ShowLabel(lblError, "Error,el número de documento es mayor al folio final capturado en la configuración de documentos electrónicos", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }
                return true;
            }
            else
            {
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica de los folios para la operacion de documentos electrónicos", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        private int SalvarFactura(int iFacturaConcepto)
        {
            int iNumeroFactLibre = Sp_Factura_Libre(1, 0, iFacturaConcepto);//SalvaEncabezado

            if (iNumeroFactLibre > 0)
            {
                SalvarDetalle(iNumeroFactLibre);
                return iNumeroFactLibre;
            }

            return iNumeroFactLibre;
        }

        private int Sp_Factura_Libre(int Moviment, int Number, int iFacturaConcepto)
        {
            int idClient = Convert.ToInt32(Session["Person"]);
            SaleBLL Sale = new SaleBLL();

            Sale.Company = Convert.ToInt32(HttpContext.Current.Session["Company"]);
            Sale.Number = Number;
            Sale.Customer = idClient;
            Sale.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            Sale.Documento = Convert.ToInt32(drpDocumento.SelectedValue);
            Sale.Serie = Convert.ToInt32(drpSerie.SelectedValue);
            Sale.NumeroDocto = Convert.ToInt32(txtfolio.Text);
            Sale.Reference = "";
            Sale.CurrencyInvoice = Convert.ToInt32(drpMoneda.SelectedValue);
            Sale.TypeChange = dExchange;
            Sale.ConceptNumber = iFacturaConcepto;
            Sale.User = Convert.ToInt32(Session["Person"]);
            Sale.Credit = 0;
            Sale.Observations = string.Empty;
            Sale.Notacargo = 0;
            Sale.CPayData = Convert.ToInt32(drpFormadePago.SelectedValue);

            if (Session["Office"] != null)
                Sale.Clas_Sucursal = Convert.ToInt32(Session["Office"]);
            else
                Sale.Clas_Sucursal = 0;

            Sale.SPayData = "No identificado";
            return Sale.SpFacturaLibre(Moviment);
        }

        protected void SalvarDetalle(int iNumeroFactLibre)
        {
            foreach (GridViewRow row in grvPartidas.Rows)
                SaveDetail(1, row.RowIndex, iNumeroFactLibre);
        }

        private int sp_Factura_Contabilidad_CC(int Number)
        {
            SaleBLL Sale = new SaleBLL();

            Sale.Company = Convert.ToInt32(HttpContext.Current.Session["Company"]);
            Sale.Number = Number;
            Sale.User = Convert.ToInt32(Session["Person"]);


            return Sale.spFacturaContabilidadCC();
        }

        private bool ValidacionDatosElectronico()
        {
            if (ValidacionDatosDoctoElectronico())
            {
                if (ValidacionDatosDoctoEmisor())
                {
                    if (ValidacionDatosDoctoReceptor())
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

        private bool ValidacionDatosDoctoElectronico()
        {
            DataTable TablaDoctosE;
            string sRutaKey = "";
            string sContraseñaKey = "";

            if ((TablaDoctosE = DatosDoctoE()) != null)
            {
                if (Convert.ToString(TablaDoctosE.Rows[0][0]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontro el archivo de llave privada", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaDoctosE.Rows[0][0]) != "")
                {
                    sRutaKey = Convert.ToString(TablaDoctosE.Rows[0][0]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][1]) != "")
                {
                    sContraseñaKey = Convert.ToString(TablaDoctosE.Rows[0][1]);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró la contraseña del archivo de llave privada", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][2]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error, no se encontró el archivo de certificado", System.Drawing.Color.DarkRed);
                    return false;
                }
                if (Convert.ToString(TablaDoctosE.Rows[0][3]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturó el año de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }
                if (Convert.ToInt32(TablaDoctosE.Rows[0][3]) > 9999)
                {
                    LabelTool.ShowLabel(lblError, "Error,en la captura del año de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }
                if (Convert.ToString(TablaDoctosE.Rows[0][4]) == "")
                {
                    LabelTool.ShowLabel(lblError, "Error,no se capturó el número de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }
                return true;
            }
            else
            {
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica de los folios para la operación de documentos electrónicos", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        //private SmtpMail InitializeMail(string Message, string Subject)
        //{
        //    DataTable DataMail;
        //    string[] recipients = new string[100];
        //    int i = 0;

        //    int index = 0;

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

        //    if ((DataMail = ControlsBLL.GetDataMail(Convert.ToInt32(HttpContext.Current.Session["Company"]))) == null)
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
        //                iPuerto = Convert.ToInt32(DataMail.Rows[0][1]);
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

        //    SmtpMail Mail = new SmtpMail(sServidor, iPuerto, sMail, sContraseña, recipients);

        //    Mail.From = sMail;
        //    Mail.Message = Message;
        //    Mail.Subject = Subject;

        //    return Mail;
        //}

        private void bloqueoControles(bool bloquear)
        {
            grvPartidas.Enabled = bloquear;

        }

        private DataTable DatosDoctoE()
        {
            SaleBLL DatosDocumentoE = new SaleBLL();

            DatosDocumentoE.Company = Convert.ToInt32(HttpContext.Current.Session["Company"]);
            DatosDocumentoE.Number = Convert.ToInt32(iIDFactura.Value);
            DatosDocumentoE.Type = 0;

            return DatosDocumentoE.GetDatosDoctoE();
        }

        private bool ValidacionDatosDoctoEmisor()
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
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no puede ser menor a 12 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                        if (rfc.Length > 13)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no puede ser mayor a 13 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaEmisor.Rows[0][1] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][1]) == "")
                    {

                        LabelTool.ShowLabel(lblError, "El nombre del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaEmisor.Rows[0][2] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][2]) == "")
                    {
                        LabelTool.ShowLabel(lblError, "El domicilio del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaEmisor.Rows[0][3] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][3]) == "")
                    {
                        LabelTool.ShowLabel(lblError, "La colonia de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaEmisor.Rows[0][4] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][4]) == "")
                    {
                        LabelTool.ShowLabel(lblError, "La delegación/municipio de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaEmisor.Rows[0][5] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][5]) == "")
                    {
                        LabelTool.ShowLabel(lblError, "El estado de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaEmisor.Rows[0][6] != null)
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

                if (TablaEmisor.Rows[0][8] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][8]) == "")
                    {
                        LabelTool.ShowLabel(lblError, "No se encuentra capturado el régimen fiscal de la empresa", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                return true;
            }

            else
            {
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica de los folios para la operacion de documentos electronicos", System.Drawing.Color.DarkRed);
                return false;
            }

        }

        private bool ValidacionDatosDoctoReceptor()
        {
            DataTable TablaReceptor;
            string rfc;

            if ((TablaReceptor = DatosDoctoReceptor()) != null)
            {
                if (TablaReceptor.Rows[0][0] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][0]) != "")
                    {
                        rfc = Convert.ToString(TablaReceptor.Rows[0][0]);

                        if (rfc.Length < 12)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede ser menor a 12 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                        if (rfc.Length > 13)
                        {
                            LabelTool.ShowLabel(lblError, "Error,el R.F.C del cliente no puede ser mayor a 13 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error,el R.F.C del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaReceptor.Rows[0][1] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][1]) == "")
                    {
                        LabelTool.ShowLabel(lblError, "El nombre del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaReceptor.Rows[0][2] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][2]) == "")
                    {
                        LabelTool.ShowLabel(lblError, "El domicilio del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaReceptor.Rows[0][3] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][3]) == "")
                    {
                        LabelTool.ShowLabel(lblError, "La colonia de la dirección del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaReceptor.Rows[0][4] != null)
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

                if (TablaReceptor.Rows[0][5] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][5]) == "")
                    {
                        LabelTool.ShowLabel(lblError, "La delegación/municipio de la dirección del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaReceptor.Rows[0][6] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][6]) == "")
                    {
                        LabelTool.ShowLabel(lblError, "El estado de la dirección del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaReceptor.Rows[0][8] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][8]) == "")
                    {
                        LabelTool.ShowLabel(lblError, "La dirección del cliente no cuenta con un país", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaReceptor.Rows[0][7] != null)
                {
                    int porcentaje;
                    int ifamily = 3;
                    SaleBLL Invoice = new SaleBLL();
                    int iva = Convert.ToInt32(TablaReceptor.Rows[0][7]);
                    DataTable Tporcentaje = Invoice.GetIvaReceptor(Convert.ToInt32(HttpContext.Current.Session["Company"]), iva, ifamily);
                    if (Tporcentaje != null)
                    {
                        porcentaje = Convert.ToInt32(Tporcentaje.Rows[0][0]);
                    }
                    else
                    {
                        LabelTool.ShowLabel(lblError, "No encontró el porcentaje del I.V.A", System.Drawing.Color.DarkRed);
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
                LabelTool.ShowLabel(lblError, "Error, no se ha capturado la configuración basica del cliente para la operacion de documentos electronicos", System.Drawing.Color.DarkRed);
                return false;
            }
        }

        private DataTable DatosDoctoReceptor()
        {
            SaleBLL DatosDocumentoReceptor = new SaleBLL();
            DatosDocumentoReceptor.Company = Convert.ToInt32(HttpContext.Current.Session["Company"]);
            DatosDocumentoReceptor.Number = Convert.ToInt32(iIDFactura.Value);
            DatosDocumentoReceptor.Type = 0;
            return DatosDocumentoReceptor.GetDatosDoctoReceptor();
        }

        private bool SaveDetail(int Moviment, int Index, int iNumeroFactLibre)
        {
            SaleDetailBLL SaleDetail = new SaleDetailBLL();

            SaleDetail.Company = Convert.ToInt32(HttpContext.Current.Session["Company"]);
            SaleDetail.SaleNumber = iNumeroFactLibre;
            SaleDetail.Number = 0;
            Label lbltipoproducto = (Label)grvPartidas.Rows[Index].FindControl("lbltipo_producto");
            SaleDetail.TProduct = Convert.ToInt32(lbltipoproducto.Text);
            Label lblproducto = (Label)grvPartidas.Rows[Index].FindControl("lblproducto");
            SaleDetail.Product = Convert.ToInt32(lblproducto.Text);
            Label lbltipoconcepto = (Label)grvPartidas.Rows[Index].FindControl("lbltipo_concepto");
            SaleDetail.TConcept = Convert.ToInt32(lbltipoconcepto.Text);
            Label lblconcepto = (Label)grvPartidas.Rows[Index].FindControl("lblconcepto");
            SaleDetail.Concept = Convert.ToInt32(lblconcepto.Text);
            TextBox productotxt = (TextBox)grvPartidas.Rows[Index].FindControl("productotxt");

            if (Convert.ToInt32(lblconcepto.Text) == -105)
            {
                SaleDetail.Characteristics = Convert.ToString(productotxt.Text);
            }
            else
            {
                SaleDetail.Characteristics = null;
            }

            Label clas_unidad = (Label)grvPartidas.Rows[Index].FindControl("lblclas_um");
            SaleDetail.MeasuringUnit = Convert.ToInt32(clas_unidad.Text);
            TextBox cantidadtxt = (TextBox)grvPartidas.Rows[Index].FindControl("cantidadtxt");
            SaleDetail.Quantity = Convert.ToInt32(cantidadtxt.Text);
            TextBox precio = (TextBox)grvPartidas.Rows[Index].FindControl("preciotxt");
            SaleDetail.Price = Convert.ToDecimal(precio.Text);
            double cant = Convert.ToDouble(cantidadtxt.Text);
            double prec = Convert.ToDouble(precio.Text);
            double subtotal = cant * prec;

            //string porcentageIa = "0." + ddlIva.SelectedItem.ToString();
            string porcentageIa = "0." + drpIva.SelectedItem.ToString();
            double porcentageIva = Convert.ToDouble(porcentageIa);

            double iva = subtotal * porcentageIva;
            //iva por partida
            SaleDetail.IVA = iva;
            SaleDetail.ClasIVA = Convert.ToInt32(drpIva.SelectedValue);
            Label lblfamilia = (Label)grvPartidas.Rows[Index].FindControl("lblFamilia");
            SaleDetail.MeasuringUnitFam = Convert.ToInt32(lblfamilia.Text);
            SaleDetail.DeliveryDate = DateTime.Today;
            SaleDetail.NotaCargo = null;
            SaleDetail.TotalCargo = null;

            return SaleDetail.SaveDetailFacturaLibre(Moviment);
        }

        private DataTable DatosCorreoCliente()
        {
            SaleBLL DatosCorreo = new SaleBLL();
            int idClient = Convert.ToInt32(HttpContext.Current.Session["Person"]);
            DatosCorreo.Customer = idClient;
            return DatosCorreo.GetDatosCorreoCliente().Copy();
        }
        #endregion

        //NUEVO PRODUCTOS POR CODIGO DE BARRAS

        protected void txtCodigoBarras_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewTool.Bind(null, GrvProductos);
                GridViewTool.Bind(null, chargrv);

                if ((TempProductsTable = ObtieneConceptoPorProducto()) == null)
                {
                    if (HdnCliente.Value == "")
                    {
                        LabelTool.ShowLabel(lblError, "Ingrese un cliente", System.Drawing.Color.DarkRed);
                    }
                }
                else
                {
                    if (TempProductsTable.Rows.Count > 0)
                    {
                        LabelTool.HideLabel(lblError);

                        AsignaPreciosdeLista();
                        TempProductsTable = ObtienePrecioConImpuestosSinIVA(TempProductsTable);
                        TempProductsTable = ObtieneIVAPorProducto(TempProductsTable);

                        if (sSaleValidation.IndexOf("CambiarEstatusGeneral") == -1)
                        {
                            if (sSaleValidation != "NOTVAL" && sSaleValidation != "AgregarPartidaSE" && sSaleValidation != "CambiarEstatusPartidaSE")// regresar
                            {

                                int index = 0;

                                agregaColumnasDeTotales(ref index);
                                int existeDisponibles = buscaAlmacen();
                                if (existeDisponibles == 1)
                                {
                                    AgregaProductosCodigoBarras();
                                }
                                else
                                {
                                    LabelTool.ShowLabel(lblError, "Sin disponibles para este producto", System.Drawing.Color.DarkRed);
                                }
                            }

                            else if (sSaleValidation == "AgregarPartidaSE" || sSaleValidation == "CambiarEstatusPartidaSE")// regresar
                            {
                                int index = 0;

                                agregaColumnasDeTotales(ref index);

                                AgregaProductosCodigoBarras();
                            }

                            else if (sSaleValidation == "NOTVAL")
                            {
                                int index = 0;
                                agregaColumnasDeTotales(ref index);

                                AgregaProductosCodigoBarras();
                            }
                        }
                        else
                        {
                            int index = 0;
                            agregaColumnasDeTotales(ref index);

                            AgregaProductosCodigoBarras();

                        }
                    }
                    else
                    {


                        if (keycontainer.Value == "" || keycontainer.Value == "0")
                        {
                            LabelTool.ShowLabel(lblError, "El producto no existe", System.Drawing.Color.DarkRed);
                            keycontainer.Value = "";
                            txtCodigoBarras.Text = "";
                        }
                        else
                        {

                            keycontainer.Value = "";
                            txtCodigoBarras.Text = "";
                            LabelTool.ShowLabel(lblError, "El producto no se encuentra en la lista", System.Drawing.Color.DarkRed);
                        }

                    }
                }
                keycontainer.Value = "";
                txtCodigoBarras.Text = "";
                CheckCommandControls();
                SetFocus(txtCodigoBarras);
                GetAmounts();
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "" + ex.Message + "", System.Drawing.Color.DarkRed);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                      this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }

        }
        private DataTable ObtienePrecioConImpuestosSinIVA(DataTable dtPartidas)
        {
            int index = 0;


            foreach (DataRow row in dtPartidas.Rows)
            {
                dtPartidas.Rows[index]["Precio"] = ImpuestosBLL.ObtienePreciosConImpuestosSinIVA(Convert.ToInt32(Session["Company"]),
                                                          Convert.ToInt32(dtPartidas.Rows[index][1]), Convert.ToInt32(dtPartidas.Rows[index][2]),
                                                          1, Convert.ToDecimal(dtPartidas.Rows[index]["Precio"]));

                index++;
            }

            return dtPartidas;


        }


        private DataTable ObtieneIVAPorProducto(DataTable dtPartidas)
        {
            int index = 0;
            DataTable dtIVA = new DataTable();
            dtPartidas.Columns.Add("PorcentajeIVA");//PORCENTAJE DE IVA
            dtPartidas.Columns.Add("IVAParametrizado");//INDICARA SI ES UN IVA PARAMETRIZADO 

            foreach (DataRow row in dtPartidas.Rows)
            {
                dtIVA = BandasBLL.ObtienePreciosConImpuestos(1, Convert.ToInt32(Session["Company"]),
                                                                  Convert.ToInt32(dtPartidas.Rows[index]["Producto"]),
                                                                  Convert.ToInt32(dtPartidas.Rows[index]["Concepto"]));

                row["PorcentajeIVA"] = Convert.ToDecimal(dtIVA.Rows[0][4]);

                if (dtIVA.Rows[0][0].ToString() == "")
                {
                    row["IVAParametrizado"] = "NO";
                    row["PorcentajeIVA"] = Convert.ToDecimal(drpIva.SelectedItem.ToString());
                }
                else
                {
                    row["IVAParametrizado"] = "SI";
                }


                index++;
            }

            return dtPartidas;
        }


        protected void cantidadtxt_TextChanged(object sender, EventArgs e)
        {
            try
            {

                TextBox txtCantidad = (TextBox)sender;
                GridViewRow Row = (GridViewRow)txtCantidad.NamingContainer;
                //Editar la tabla de productos con la nueva cantidad
                int a = Row.RowIndex;
                decimal dCantidadIngresada = 0, dDisponibles = 0;

                DropDownList store = (DropDownList)Row.FindControl("storeDropDownList");

                if (txtCantidad.Text != "")
                {
                    if (sSaleValidation != "AgregarPartidaSE" && sSaleValidation != "CambiarEstatusGeneralSE" && sSaleValidation != "CambiarEstatusPartidaSE")
                    {
                        if (Row.Cells[1].Text != "")
                        {
                            dDisponibles = Convert.ToDecimal(Row.Cells[1].Text);
                            dCantidadIngresada = Convert.ToDecimal(txtCantidad.Text);

                            if (dCantidadIngresada > dDisponibles)
                            {
                                LabelTool.ShowLabel(lblError, "La cantidad ingresada supera el disponible del almacén", System.Drawing.Color.DarkRed);
                                HdnValorIncorrecto.Value = "1";
                            }
                            else
                            {
                                LabelTool.HideLabel(lblError);
                                ProductsTable.Rows[a][5] = txtCantidad.Text;
                                ProductsTable.Rows[a][6] = Convert.ToDecimal(Convert.ToDecimal(ProductsTable.Rows[a][5]) *
                                                         Convert.ToDecimal(ProductsTable.Rows[a][3]));   //CANTIDAD * PRECIO  = TOTAL PARTIDA (SIN IVA)

                                Row.Cells[7].Text = ProductsTable.Rows[a][6].ToString();
                                GetAmounts();
                                HdnValorIncorrecto.Value = "0";
                            }

                        }

                    }
                    else
                    {

                        LabelTool.HideLabel(lblError);
                        ProductsTable.Rows[a][5] = txtCantidad.Text;
                        ProductsTable.Rows[a][6] = Convert.ToDecimal(Convert.ToDecimal(ProductsTable.Rows[a][5]) *
                                                 Convert.ToDecimal(ProductsTable.Rows[a][3]));   //CANTIDAD * PRECIO  = TOTAL PARTIDA (SIN IVA)

                        Row.Cells[7].Text = ProductsTable.Rows[a][6].ToString();
                        GetAmounts();
                        HdnValorIncorrecto.Value = "0";

                    }


                }
                else
                {

                    LabelTool.ShowLabel(lblError, "La cantidad ingresada no es válida", System.Drawing.Color.DarkRed);
                    HdnValorIncorrecto.Value = "1";
                }


            }
            catch (Exception ex)
            {

                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
                LabelTool.ShowLabel(lblError, "La cantidad ingresada no es válida", System.Drawing.Color.DarkRed);
                HdnValorIncorrecto.Value = "1";
            }
        }
        private DataTable ObtieneConceptoPorProducto()// probando
        {
            DataTable dt = new DataTable();
            if (keycontainer.Value != null && keycontainer.Value != "" && HdnCliente.Value != "")
            {
                dt = VentasController.GetConceptosPorProducto(Convert.ToInt32(HdnEmpresa.Value), 1, Convert.ToInt32(keycontainer.Value), Convert.ToInt32(HdnCliente.Value), 0);
            }
            else if (keycontainer.Value == null || keycontainer.Value == "" && HdnCliente.Value != "")
            {
                // paso 1 obtener el value del producto   
                string sDescripcion = txtCodigoBarras.Text.Trim();
                DataTable ProductsTable = VentasController.GetProductoCodigoBarras(Convert.ToInt32(Session["Company"]), sDescripcion);
                if (ProductsTable != null && ProductsTable.Rows.Count > 0)
                {
                    int iNumeroConcepto = Convert.ToInt32(ProductsTable.Rows[0][1]);
                    dt = VentasController.GetConceptosPorProducto(Convert.ToInt32(HdnEmpresa.Value), 1, iNumeroConcepto, Convert.ToInt32(HdnCliente.Value), 0);
                    keycontainer.Value = iNumeroConcepto.ToString();

                }

            }

            return dt;
        }
        private void agregaColumnasDeTotales(ref int index)
        {
            if (TempProductsTable.Rows.Count > 0)
            {
                DataColumn concept_column;

                concept_column = TempProductsTable.Columns.Add("Cantidad");
                concept_column.SetOrdinal(5);
                concept_column = TempProductsTable.Columns.Add("Total");
                concept_column.SetOrdinal(6);
                concept_column = TempProductsTable.Columns.Add("Numero");
                concept_column.SetOrdinal(7);
                concept_column = TempProductsTable.Columns.Add("Partida");
                concept_column.SetOrdinal(12);
            }

            TempProductsTable.Rows[index][5] = 0;
            TempProductsTable.Rows[index][6] = 0;
            TempProductsTable.Rows[index][7] = 0;
            TempProductsTable.Rows[index][12] = 0;



            foreach (DataRow row in TempProductsTable.Rows)
            {

                TempProductsTable.Rows[index][5] = 0;
                TempProductsTable.Rows[index][6] = 0;
                TempProductsTable.Rows[index][7] = 0;
                TempProductsTable.Rows[index][12] = 0;

                index++;
            }
        }
        private void AgregaProductosCodigoBarras()
        {
            int index = 0;

            agregaTotales(ref index);
            LLenaProductsTable();

            if (sSaleValidation.IndexOf("CambiarEstatusGeneral") == -1)
            {
                if (sSaleValidation != "NOTVAL" && sSaleValidation != "AgregarPartidaSE" && sSaleValidation != "CambiarEstatusPartidaSE")
                {

                    fill_store_controls(1);
                    GetStores();
                }

                else if (sSaleValidation == "AgregarPartidaSE" || sSaleValidation == "CambiarEstatusPartidaSE")
                {
                    fill_store_controls(1);
                    GetStores();
                }
                else
                {
                    GridViewTool.Bind(ProductsTable, grvPartidas);
                }
            }
            else
            {
                GridViewTool.Bind(ProductsTable, grvPartidas);
            }

        }

        private void agregaTotales(ref int index)
        {

            TempProductsTable.Rows[index][5] = 1;
            TempProductsTable.Rows[index][6] = Convert.ToDecimal(Convert.ToDecimal(TempProductsTable.Rows[index][5]) *
                                                             Convert.ToDecimal(TempProductsTable.Rows[index][3]));
            TempProductsTable.Rows[index][12] = 0;

        }


        protected int buscaAlmacen()//llena los drops de las sucursales
        {
            SaleBLL Sale = new SaleBLL();
            DataTable store_table = new DataTable();
            DataTable stock_table = new DataTable();
            int iconta = 0, aux = 0, sumaDisponibles = 0;

            if (Convert.ToInt32(TempProductsTable.Rows[0][7]) == 0)
            {
                if (Sale.IsProduct(Convert.ToInt32(HdnEmpresa.Value), Convert.ToInt32(TempProductsTable.Rows[0][1])))
                {
                    store_table = get_storesGridProduct(0);

                    if (store_table.Rows.Count > 0)
                    {


                        foreach (DataRow row1 in store_table.Rows)
                        {
                            if (Convert.ToInt32(store_table.Rows[iconta][2]) > 0)
                            {

                                TempProductsTable.Rows[0][7] = Convert.ToInt32(store_table.Rows[iconta][0]);
                            }

                            sumaDisponibles += Convert.ToInt32(store_table.Rows[iconta][2]);

                            iconta++;
                        }
                        if (sumaDisponibles == 0)
                        {
                            TempProductsTable.Rows[0][7] = 0;
                            return 0;
                        }

                    }
                    else
                    {
                        TempProductsTable.Rows[0][7] = 0;
                        return 0;

                    }

                }

            }
            return 1;

        }


        private int validaCantidadAlmacenes()//Valida que exista cantidad suficiente del producto en por lo menos un almacén 
        {
            DataTable ProductsTableTMP = new DataTable();

            ProductsTableTMP.Columns.Add("product");

            for (int i = 0; i < ProductsTable.Rows.Count; i++)
            {
                if (INTEGRA.Global.Product_type == 113)
                {
                    if ((filter_stock_stores(i)).Rows.Count <= 0)
                    {
                        DataRow row = ProductsTableTMP.NewRow();

                        row["product"] = ProductsTable.Rows[i][9];

                        ProductsTableTMP.Rows.Add(row);

                    }
                }
                else //es servicio
                {
                    //pasos cuando es servicio
                }
            }

            if (ProductsTableTMP.Rows.Count > 0)
            {
                messagelbl.Text = "No existe la cantidad solicitada en ninguno de los almacenes para el producto:<br><br>";

                foreach (DataRow ProductsRowTMP in ProductsTableTMP.Rows)
                {
                    string producto = ProductsRowTMP["product"].ToString();
                    messagelbl.Text += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;==>    " + ProductsRowTMP["product"].ToString() + "<br>";

                }

                return 0;
            }
            else
            {
                LabelTool.HideLabel(lblError);
                LabelTool.HideLabel(messagelbl);
                return 1;
            }
        }


    }
}