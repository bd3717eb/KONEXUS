using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegraBussines;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using INTEGRAReports;
using System.IO;
using System.Web.Services;
using System.Web.Configuration;
namespace Integra_Develoment.Ventas
{
    public partial class FacturaLibreMorales : System.Web.UI.Page
    {
        #region Variables

        Person ipersona;
        //Servisim.CFDi.Test.CFDIProgram CFDI = new Servisim.CFDi.Test.CFDIProgram();

        static int iSaleNumber, iPuerto, iNumero_um;
        static bool bmoral = true;
        static decimal dExchange, dImporteCambio, dImportePago, dCambio, dTipoCambio;//
        static string spersonalidad, sNumeroTicket;
        private string _TablaPartidas = "products";
        private string empresa;
        bool bCreacion;
        private string sServidor, sMail, sContraseña;
        public string ruta, rutaCopia, reportname, spathString, sdatofaltante;
        DataTable TablaCorreoCliente, tempPayForms, DetalleTable;
        private DataTable TablaPartidas
        {
            get
            {
                if (ViewState[_TablaPartidas] == null)
                    return new DataTable();
                return (DataTable)ViewState[_TablaPartidas];
            }
            set
            {
                ViewState[_TablaPartidas] = value;
            }
        }

        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                ipersona = new Person(Convert.ToInt32(Request.QueryString["Client"]), Convert.ToInt32(Session["Company"]),
                                        INTEGRA.Global.connectionString);
                empresa = Convert.ToString(Session["Company"]);

                colapsiblepanel();

                if (!this.IsPostBack)
                {
                    // this pack is for only FACTURACION 
                    //string[] sTipoFactura = Session["sPaginaAsignada"].ToString().Split('/');

                    PrepareSale();
                    customertxt.Focus();
                }

                LnkGenerar.Attributes.Add("onclick", "gfProceso();");
                addchangelnk.Attributes.Add("onclick", "gfProceso();");
                LbtnPay.Attributes.Add("onclick", "gfProceso();");
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref errorLabel);
            }

        }



        protected void invoiceGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = invoiceGridView.SelectedIndex;
            FacturaLibreController Invoice = new FacturaLibreController();

            TablaPartidas = new DataTable();
            TablaPartidas.Columns.Add("Numero", typeof(int));
            TablaPartidas.Columns.Add("Cantidad", typeof(decimal));
            TablaPartidas.Columns.Add("TipoProducto", typeof(int));
            TablaPartidas.Columns.Add("Producto", typeof(int));
            TablaPartidas.Columns.Add("TipoConcepto", typeof(int));
            TablaPartidas.Columns.Add("Concepto", typeof(int));
            TablaPartidas.Columns.Add("Clas_UM", typeof(int));
            TablaPartidas.Columns.Add("Familia", typeof(int));
            TablaPartidas.Columns.Add("Descripcion", typeof(string));
            TablaPartidas.Columns.Add("Precio", typeof(decimal));
            TablaPartidas.Columns.Add("Unidad", typeof(string));
            TablaPartidas.Columns.Add("Total", typeof(decimal));
            TablaPartidas.Columns.Add("NumeroProducto", typeof(string));
            TablaPartidas.Columns.Add("Caracteristicas", typeof(string));
            TablaPartidas.Columns.Add("PorcentajeIva", typeof(string));//nuevo impuestos
            TablaPartidas.Columns.Add("IVAParametrizado", typeof(string));//nuevo impuestos

            foreach (GridViewRow row1 in invoiceGridView.Rows)
            {
                DataRow dr = TablaPartidas.NewRow();

                Label lbl = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("Numberlbl");
                Label lbltipoproducto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lbltipo_producto");
                dr["TipoProducto"] = Convert.ToInt32(lbltipoproducto.Text);
                Label lblproducto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblproducto");
                dr["Producto"] = Convert.ToInt32(lblproducto.Text);
                Label lbltipoconcepto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lbltipo_concepto");
                dr["TipoConcepto"] = Convert.ToInt32(lbltipoconcepto.Text);
                Label lblconcepto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblconcepto");
                dr["Concepto"] = Convert.ToInt32(lblconcepto.Text);
                Label lblclas_um = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblclas_um");
                dr["Clas_UM"] = Convert.ToInt32(lblclas_um.Text);
                Label lblfamilia = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblFamilia");
                dr["Familia"] = Convert.ToInt32(lblfamilia.Text);
                TextBox cantidadtxt = (TextBox)invoiceGridView.Rows[row1.RowIndex].FindControl("cantidadtxt");

                if (cantidadtxt.Text != "")
                {
                    dr["Cantidad"] = Convert.ToDecimal(cantidadtxt.Text);
                }
                else
                {
                    dr["Cantidad"] = DBNull.Value;
                }
                TextBox productotxt = (TextBox)invoiceGridView.Rows[row1.RowIndex].FindControl("productotxt");
                dr["Descripcion"] = Convert.ToString(productotxt.Text);
                productotxt.Attributes.Add("onclick", "doClearProductsTextBox();");

                TextBox precio = (TextBox)invoiceGridView.Rows[row1.RowIndex].FindControl("preciotxt");

                if (precio.Text != "")
                {
                    dr["Precio"] = precio.Text;
                }
                else
                {
                    dr["Precio"] = DBNull.Value;
                }

                if (cantidadtxt.Text != "" && precio.Text != "")
                {
                    dr["Total"] = Convert.ToDecimal(cantidadtxt.Text) * Convert.ToDecimal(precio.Text);
                }
                else
                {
                    dr["Total"] = DBNull.Value;
                }


                Label lblNumeroProducto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblNumeroProducto");
                dr["NumeroProducto"] = lblNumeroProducto.Text;

                Label lblCaracteristicas = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblCaracteristicas");
                dr["Caracteristicas"] = lblCaracteristicas.Text;
                Label lblPorcentajeIva = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblPorcentajeIva");
                dr["PorcentajeIva"] = lblPorcentajeIva.Text;
                Label lblIVAParametrizado = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblIVAParametrizado");
                dr["IVAParametrizado"] = lblIVAParametrizado.Text;

                TablaPartidas.Rows.Add(dr);
            }


            TablaPartidas.Rows.RemoveAt(index);
            GridViewTool.Bind(TablaPartidas, invoiceGridView);

            foreach (GridViewRow row in invoiceGridView.Rows)
            {
                Label txt = (Label)row.FindControl("lblNumeroProducto");

                TextBox productotxt = (TextBox)row.FindControl("productotxt");

                if (productotxt.Text != "")
                {
                    DropDownList dropunidad = (DropDownList)row.FindControl("ddlunidad");
                    dropunidad.Visible = true;
                    DataTable TablaUnidades = new DataTable();
                    DropTool.ClearDrop(dropunidad);
                    TablaUnidades = Invoice.ObtenUnidadesDeMedida(Convert.ToInt32(Session["Company"]));
                    DropTool.FillDrop(dropunidad, TablaUnidades, "Descripcion", "Numero");
                    dropunidad.SelectedValue = TablaPartidas.Rows[row.RowIndex][6].ToString();
                }
            }

            invoiceGridView.SelectedIndex = -1;

            calcularTotal();
        }

        protected void invoiceGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Show")
                {
                    hdnRow.Value = Convert.ToString(e.CommandArgument);
                    int Index = Convert.ToInt32(hdnRow.Value);

                    Label lblCaracteristicas = (Label)invoiceGridView.Rows[Index].FindControl("lblCaracteristicas");
                    if (lblCaracteristicas.Text != "")
                    {
                        txtCaracteristicas.Text = lblCaracteristicas.Text.Trim();
                    }

                    else
                    {
                        txtCaracteristicas.Text = "";
                    }
                    LabelTool.HideLabel(errorLabel);
                    mdlcaractetisticas.Show();

                }
            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(errorLabel, "" + ex.Message + "", System.Drawing.Color.DarkRed);
            }
        }

        protected void lnkCancelaCar_Click(object sender, EventArgs e)
        {
            mdlcaractetisticas.Hide();

        }
        protected void lnkLimpiarCar_Click(object sender, EventArgs e)
        {
            int Index = Convert.ToInt32(hdnRow.Value);
            Label lblCaracteristicas = (Label)invoiceGridView.Rows[Index].FindControl("lblCaracteristicas");
            lblCaracteristicas.Text = "";
            txtCaracteristicas.Text = "";

        }
        protected void AgregaCaracteristica_Click(object sender, EventArgs e)
        {

            int Index = Convert.ToInt32(hdnRow.Value);
            Label lblCaracteristicas = (Label)invoiceGridView.Rows[Index].FindControl("lblCaracteristicas");
            lblCaracteristicas.Text = txtCaracteristicas.Text.Trim();
            mdlcaractetisticas.Hide();

        }

        protected void ddlunidadChanged(object sender, EventArgs e)
        {

            int valChk = 0;
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            valChk = row.RowIndex;
            DropDownList dropunidad = (DropDownList)invoiceGridView.Rows[valChk].FindControl("ddlunidad");
            Label lblclas_um = (Label)invoiceGridView.Rows[valChk].FindControl("lblclas_um");
            lblclas_um.Text = (dropunidad.SelectedValue);
            iNumero_um = Convert.ToInt32(dropunidad.SelectedValue);
        }

        protected void monedaChanged(object sender, EventArgs e)
        {

        }

        protected void doctodrpChanged(object sender, EventArgs e)
        {
            InvoiceBLL Invoice = new InvoiceBLL();
            FillSerie();
            if (Convert.ToString(ddldocto.SelectedValue) != "" && Convert.ToString(ddlserie.SelectedValue) != "")
            {
                int iFolio = Invoice.GetFolio(Convert.ToInt32(empresa), Convert.ToInt32(ddldocto.SelectedValue), Convert.ToInt32(ddlserie.SelectedValue));
                txtfolio.Text = iFolio.ToString();
                if (ddldocto.SelectedValue == "-59")
                {
                    txtfolio.Enabled = false;
                }
                else
                {
                    txtfolio.Enabled = true;
                }
            }
            else
            {
                txtfolio.Text = "";
            }
        }

        protected void seriedrpChanged(object sender, EventArgs e)
        {
            InvoiceBLL Invoice = new InvoiceBLL();
            if (Convert.ToString(ddldocto.SelectedValue) != "" && Convert.ToString(ddlserie.SelectedValue) != "")
            {
                int iFolio = Invoice.GetFolio(Convert.ToInt32(empresa), Convert.ToInt32(ddldocto.SelectedValue), Convert.ToInt32(ddlserie.SelectedValue));
                txtfolio.Text = iFolio.ToString();
                if (ddldocto.SelectedValue == "-59")
                {
                    txtfolio.Enabled = false;
                }
                else
                {
                    txtfolio.Enabled = true;
                }
            }
        }

        protected void nuevaPartida_Click(object sender, EventArgs e)
        {

            FacturaLibreController Invoice = new FacturaLibreController();
            TablaPartidas = new DataTable();

            TablaPartidas.Columns.Add("Numero", typeof(int));
            TablaPartidas.Columns.Add("Cantidad", typeof(decimal));
            TablaPartidas.Columns.Add("TipoProducto", typeof(int));
            TablaPartidas.Columns.Add("Producto", typeof(int));
            TablaPartidas.Columns.Add("TipoConcepto", typeof(int));
            TablaPartidas.Columns.Add("Concepto", typeof(int));
            TablaPartidas.Columns.Add("Clas_UM", typeof(int));
            TablaPartidas.Columns.Add("Familia", typeof(int));
            TablaPartidas.Columns.Add("Descripcion", typeof(string));
            TablaPartidas.Columns.Add("Precio", typeof(decimal));
            TablaPartidas.Columns.Add("Unidad", typeof(string));
            TablaPartidas.Columns.Add("Total", typeof(decimal));
            TablaPartidas.Columns.Add("NumeroProducto", typeof(string));
            TablaPartidas.Columns.Add("Caracteristicas", typeof(string));
            TablaPartidas.Columns.Add("PorcentajeIva", typeof(string));//nuevo impuestos
            TablaPartidas.Columns.Add("IVAParametrizado");



            foreach (GridViewRow row1 in invoiceGridView.Rows)
            {
                DataRow dr = TablaPartidas.NewRow();

                Label lbl = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("Numberlbl");
                TextBox cantidadtxt = (TextBox)invoiceGridView.Rows[row1.RowIndex].FindControl("cantidadtxt");
                if (cantidadtxt.Text != "")
                {
                    dr["Cantidad"] = Convert.ToDecimal(cantidadtxt.Text);
                }
                else
                {
                    dr["Cantidad"] = DBNull.Value;
                }
                Label lbltipoproducto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lbltipo_producto");
                dr["TipoProducto"] = Convert.ToInt32(lbltipoproducto.Text);
                Label lblproducto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblproducto");
                dr["Producto"] = Convert.ToInt32(lblproducto.Text);
                Label lbltipoconcepto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lbltipo_concepto");
                dr["TipoConcepto"] = Convert.ToInt32(lbltipoconcepto.Text);
                Label lblconcepto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblconcepto");
                dr["Concepto"] = Convert.ToInt32(lblconcepto.Text);
                Label lblclas_um = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblclas_um");
                dr["Clas_UM"] = Convert.ToInt32(lblclas_um.Text);
                Label lblfamilia = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblFamilia");
                dr["Familia"] = Convert.ToInt32(lblfamilia.Text);
                TextBox productotxt = (TextBox)invoiceGridView.Rows[row1.RowIndex].FindControl("productotxt");
                dr["Descripcion"] = Convert.ToString(productotxt.Text);
                productotxt.Attributes.Add("onclick", "doClearProductsTextBox();");
                TextBox precio = (TextBox)invoiceGridView.Rows[row1.RowIndex].FindControl("preciotxt");

                if (precio.Text != "")
                {
                    dr["Precio"] = precio.Text;
                }
                else
                {
                    dr["Precio"] = DBNull.Value;
                }

                DropDownList dropunidad = (DropDownList)invoiceGridView.Rows[row1.RowIndex].FindControl("ddlunidad");


                if (dropunidad.Visible == true && dropunidad.SelectedItem != null)
                {
                    dr["Unidad"] = dropunidad.SelectedItem.ToString();
                }
                else
                {
                    dr["Unidad"] = 0;
                }

                if (cantidadtxt.Text != "" && precio.Text != "")
                {
                    dr["Total"] = Convert.ToDecimal(cantidadtxt.Text) * Convert.ToDecimal(precio.Text);
                }
                else
                {
                    dr["Total"] = DBNull.Value;
                }

                Label lblNumeroProducto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblNumeroProducto");
                dr["NumeroProducto"] = lblNumeroProducto.Text;
                Label lblCaracteristicas = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblCaracteristicas");
                dr["Caracteristicas"] = lblCaracteristicas.Text;

                Label lblPorcentajeIva = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblPorcentajeIva");
                dr["PorcentajeIva"] = lblPorcentajeIva.Text;

                Label lblIVAParametrizado = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblIVAParametrizado");
                dr["IVAParametrizado"] = lblIVAParametrizado.Text;

                TablaPartidas.Rows.Add(dr);

            }

            keycontainer.Value = "";

            DataRow drnew = TablaPartidas.NewRow();

            drnew["Numero"] = 0;
            drnew["Cantidad"] = DBNull.Value;
            drnew["TipoProducto"] = 207;
            drnew["Producto"] = 208;
            drnew["TipoConcepto"] = 209;
            drnew["Concepto"] = -105;
            drnew["Descripcion"] = "";
            drnew["Precio"] = DBNull.Value;
            drnew["Unidad"] = "";
            drnew["Clas_UM"] = 0;//checar
            drnew["Familia"] = 1;
            drnew["Total"] = DBNull.Value;
            drnew["NumeroProducto"] = "";
            drnew["Caracteristicas"] = "";
            drnew["PorcentajeIva"] = (Convert.ToDecimal(ddlIva.SelectedItem.ToString()) / 100).ToString();
            drnew["IVAParametrizado"] = "NO";
            TablaPartidas.Rows.Add(drnew);


            GridViewTool.Bind(TablaPartidas, invoiceGridView);
            if (TablaPartidas.Rows.Count > 0)
            {
                LabelTool.HideLabel(errorLabel);
            }


            foreach (GridViewRow row in invoiceGridView.Rows)
            {
                Label txt = (Label)row.FindControl("lblNumeroProducto");

                TextBox productotxt = (TextBox)row.FindControl("productotxt");

                if (productotxt.Text != "")
                {

                    DropDownList dropunidad = (DropDownList)row.FindControl("ddlunidad");
                    dropunidad.Visible = true;

                    DataTable TablaUnidades = new DataTable();
                    DropTool.ClearDrop(dropunidad);
                    TablaUnidades = Invoice.ObtenUnidadesDeMedida(Convert.ToInt32(Session["Company"]));
                    DropTool.FillDrop(dropunidad, TablaUnidades, "Descripcion", "Numero");
                    dropunidad.SelectedValue = TablaPartidas.Rows[row.RowIndex][6].ToString();
                }


            }
            calcularTotal();

        }

        protected void cantidadtxt_TextChanged(object sender, EventArgs e)
        {
            try
            {

                FacturaLibreController Invoice = new FacturaLibreController();
                int valChk = 0;
                GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

                valChk = row.RowIndex;
                TextBox cantidadtxt = (TextBox)invoiceGridView.Rows[valChk].FindControl("cantidadtxt");
                TextBox productotxt = (TextBox)invoiceGridView.Rows[valChk].FindControl("productotxt");
                if (cantidadtxt.Text != "")
                {
                    actualizarTotal(sender, e);
                }

                DropDownList dropunidad = (DropDownList)invoiceGridView.Rows[valChk].FindControl("ddlunidad");

                dropunidad.Visible = true;
                DataTable TablaUnidades = new DataTable();
                DropTool.ClearDrop(dropunidad);

                TablaUnidades = Invoice.ObtenUnidadesDeMedida(Convert.ToInt32(Session["Company"]));
                DropTool.FillDrop(dropunidad, TablaUnidades, "Descripcion", "Numero");
                Label lblclas_um = (Label)invoiceGridView.Rows[valChk].FindControl("lblclas_um");
                lblclas_um.Text = (dropunidad.SelectedValue);
                iNumero_um = Convert.ToInt32(dropunidad.SelectedValue);
                SetFocus(productotxt.ClientID);

            }
            catch (Exception ex)
            {

                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref errorLabel);
            }


        }

        protected void preciotxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FacturaLibreController Invoice = new FacturaLibreController();
                int valChk = 0;
                GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

                valChk = row.RowIndex;

                TextBox preciotxt1 = (TextBox)invoiceGridView.Rows[valChk].FindControl("preciotxt");

                if (preciotxt1.Text != "")
                {
                    decimal precioingreso;
                    precioingreso = Convert.ToDecimal(preciotxt1.Text);
                    preciotxt1.Text = precioingreso.ToString("f2");
                    actualizarTotal(sender, e);
                }

                DropDownList dropunidad = (DropDownList)invoiceGridView.Rows[valChk].FindControl("ddlunidad");
                if (dropunidad.SelectedValue == "")
                {
                    dropunidad.Visible = true;
                    DataTable TablaUnidades = new DataTable();
                    DropTool.ClearDrop(dropunidad);

                    TablaUnidades = Invoice.ObtenUnidadesDeMedida(Convert.ToInt32(Session["Company"]));
                    DropTool.FillDrop(dropunidad, TablaUnidades, "Descripcion", "Numero");
                    Label lblclas_um = (Label)invoiceGridView.Rows[valChk].FindControl("lblclas_um");
                    lblclas_um.Text = (dropunidad.SelectedValue);
                    iNumero_um = Convert.ToInt32(dropunidad.SelectedValue);//
                }

            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref errorLabel);

            }


        }

        protected void actualizarTotal(object sender, EventArgs e)
        {
            IntegraBussines.CustomerBLL CustomerProducts = new IntegraBussines.CustomerBLL();
            int valChk = 0;
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            valChk = row.RowIndex;

            Label lblTotal = (Label)invoiceGridView.Rows[valChk].FindControl("lblTotal");
            TextBox preciotxt1 = (TextBox)invoiceGridView.Rows[valChk].FindControl("preciotxt");
            TextBox cantidadtxt = (TextBox)invoiceGridView.Rows[valChk].FindControl("cantidadtxt");
            if (preciotxt1.Text != "" && cantidadtxt.Text != "")
            {
                lblTotal.Text = ((Convert.ToDecimal(preciotxt1.Text) * Convert.ToDecimal(cantidadtxt.Text))).ToString("f2");
            }
            calcularTotal();
        }

        protected void CambiaPorcentajeIVA()
        {

            foreach (GridViewRow row in invoiceGridView.Rows)
            {

                Label lblIVAParametrizado = (Label)invoiceGridView.Rows[row.RowIndex].FindControl("lblIVAParametrizado");
                Label lblPorcentajeIva = (Label)invoiceGridView.Rows[row.RowIndex].FindControl("lblPorcentajeIva");

                if (lblIVAParametrizado.Text == "NO")
                    lblPorcentajeIva.Text = (Convert.ToDecimal(ddlIva.SelectedItem.ToString()) / 100).ToString();

            }

        }
        protected void productsTextBox_TextChanged(object sender, EventArgs e)
        {
            IntegraBussines.CustomerBLL CustomerProducts = new IntegraBussines.CustomerBLL();
            FacturaLibreController Invoice = new FacturaLibreController();
            int valChk = 0;
            decimal dIva;
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            valChk = row.RowIndex;

            if (keycontainer.Value == "")
            {

                Label lbltipoproducto = (Label)invoiceGridView.Rows[valChk].FindControl("lbltipo_producto");
                lbltipoproducto.Text = Convert.ToString(207);
                Label lblproducto = (Label)invoiceGridView.Rows[valChk].FindControl("lblproducto");
                lblproducto.Text = Convert.ToString(208);
                Label lbltipoconcepto = (Label)invoiceGridView.Rows[valChk].FindControl("lbltipo_concepto");
                lbltipoconcepto.Text = Convert.ToString(209);
                Label lblconcepto = (Label)invoiceGridView.Rows[valChk].FindControl("lblconcepto");
                lblconcepto.Text = Convert.ToString(-105);

                Label lblfamilia = (Label)invoiceGridView.Rows[valChk].FindControl("lblFamilia");
                lblfamilia.Text = Convert.ToString(1);

                TextBox preciotxt = (TextBox)invoiceGridView.Rows[valChk].FindControl("preciotxt");
                preciotxt.Text = "";

                Label lblNumeroProducto = (Label)invoiceGridView.Rows[valChk].FindControl("lblNumeroProducto");
                lblNumeroProducto.Text = "";

                DropDownList dropunidad = (DropDownList)invoiceGridView.Rows[valChk].FindControl("ddlunidad");
                dropunidad.Visible = true;
                DataTable TablaUnidades = new DataTable();
                DropTool.ClearDrop(dropunidad);

                TablaUnidades = Invoice.ObtenUnidadesDeMedida(Convert.ToInt32(Session["Company"]));
                DropTool.FillDrop(dropunidad, TablaUnidades, "Descripcion", "Numero");

                Label lblclas_um = (Label)invoiceGridView.Rows[valChk].FindControl("lblclas_um");
                lblclas_um.Text = (dropunidad.SelectedValue);

                iNumero_um = Convert.ToInt32(dropunidad.SelectedValue);//

                Label lblPorcentajeIva = (Label)invoiceGridView.Rows[valChk].FindControl("lblPorcentajeIva");// nuevo impuestos
                lblPorcentajeIva.Text = (Convert.ToDecimal(ddlIva.SelectedItem.ToString()) / 100).ToString();
                Label lblIVAParametrizado = (Label)invoiceGridView.Rows[valChk].FindControl("lblIVAParametrizado");// nuevo impuestos
                lblIVAParametrizado.Text = "NO";
            }
            else
            {
                TextBox productotxt = (TextBox)invoiceGridView.Rows[valChk].FindControl("productotxt");
                DataTable dt = new DataTable();
                dt = Invoice.GetProductosAutocomplete(Convert.ToInt32(Session["Company"]), productotxt.Text);
                dt = ObtienePrecioConImpuestosSinIVA(dt);
                dt = ObtieneIVAPorProducto(dt);

                foreach (DataRow DRow in dt.Rows)
                {
                    if (DRow["numero"].ToString() == keycontainer.Value)
                    {
                        Label lbltipoproducto = (Label)invoiceGridView.Rows[valChk].FindControl("lbltipo_producto");
                        lbltipoproducto.Text = DRow["clas_Tproducto"].ToString();
                        Label lblproducto = (Label)invoiceGridView.Rows[valChk].FindControl("lblproducto");
                        lblproducto.Text = DRow["clas_Producto"].ToString();
                        Label lbltipoconcepto = (Label)invoiceGridView.Rows[valChk].FindControl("lbltipo_concepto");
                        lbltipoconcepto.Text = DRow["clas_Tconcepto"].ToString();
                        Label lblconcepto = (Label)invoiceGridView.Rows[valChk].FindControl("lblconcepto");
                        lblconcepto.Text = DRow["clas_Concepto"].ToString();
                        Label lblclas_um = (Label)invoiceGridView.Rows[valChk].FindControl("lblclas_um");
                        lblclas_um.Text = DRow["clas_UM"].ToString();
                        iNumero_um = Convert.ToInt32(DRow["clas_UM"]);
                        Label lblfamilia = (Label)invoiceGridView.Rows[valChk].FindControl("lblFamilia");
                        lblfamilia.Text = DRow["Familia"].ToString();

                        DropDownList dropunidad = (DropDownList)invoiceGridView.Rows[valChk].FindControl("ddlunidad");

                        dropunidad.Visible = true;

                        DataTable TablaUnidades = new DataTable();
                        DropTool.ClearDrop(dropunidad);

                        TablaUnidades = Invoice.ObtenUnidadesDeMedida(Convert.ToInt32(Session["Company"]));
                        DropTool.FillDrop(dropunidad, TablaUnidades, "Descripcion", "Numero");

                        dropunidad.SelectedValue = iNumero_um.ToString();


                        TextBox preciotxt = (TextBox)invoiceGridView.Rows[valChk].FindControl("preciotxt");
                        decimal preciobase = Convert.ToDecimal(DRow["Precio"]);

                        //dExchange = Invoice.GetfnTipoCambio(Convert.ToInt32(empresa), Convert.ToInt32(ddlmoneda.SelectedValue), DateTime.Today);
                        dExchange = ControlsBLL.GetExchange(Convert.ToInt32(Session["Company"]), 109, Convert.ToInt32(ddlmoneda.SelectedValue), DateTime.Today);
                        if (Convert.ToInt32(ddlmoneda.SelectedValue) == 110)
                        {
                            preciobase = (preciobase / dExchange);
                        }
                        else
                        {
                            preciobase = (preciobase * 1);
                        }

                        preciotxt.Text = preciobase.ToString("f2");

                        Label lblNumeroProducto = (Label)invoiceGridView.Rows[valChk].FindControl("lblNumeroProducto");
                        lblNumeroProducto.Text = keycontainer.Value;

                        Label lblPorcentajeIva = (Label)invoiceGridView.Rows[valChk].FindControl("lblPorcentajeIva");// nuevo impuestos
                        lblPorcentajeIva.Text = (Convert.ToDecimal(DRow["PorcentajeIva"]) / 100).ToString();// nuevo impuestos

                        Label lblIVAParametrizado = (Label)invoiceGridView.Rows[valChk].FindControl("lblIVAParametrizado");// nuevo impuestos
                        lblIVAParametrizado.Text = DRow["IVAParametrizado"].ToString();
                    }
                }

            }

            Label lblTotal = (Label)invoiceGridView.Rows[valChk].FindControl("lblTotal");
            TextBox preciotxt1 = (TextBox)invoiceGridView.Rows[valChk].FindControl("preciotxt");
            TextBox cantidadtxt = (TextBox)invoiceGridView.Rows[valChk].FindControl("cantidadtxt");

            if (preciotxt1.Text != "" && cantidadtxt.Text != "")
            {

                lblTotal.Text = ((Convert.ToDecimal(preciotxt1.Text) * Convert.ToDecimal(cantidadtxt.Text))).ToString();
            }

            keycontainer.Value = "";
            calcularTotal();

            TextBox txtPrecio = (TextBox)invoiceGridView.Rows[valChk].FindControl("preciotxt");
            SetFocus(txtPrecio.ClientID);

        }
        private DataTable ObtienePrecioConImpuestosSinIVA(DataTable dtPartidas)
        {
            int index = 0;


            foreach (DataRow row in dtPartidas.Rows)
            {
                dtPartidas.Rows[index]["Precio"] = ImpuestosBLL.ObtienePreciosConImpuestosSinIVA(Convert.ToInt32(Session["Company"]),
                                                          Convert.ToInt32(dtPartidas.Rows[index]["Clas_Producto"]), Convert.ToInt32(dtPartidas.Rows[index]["Clas_Concepto"]),
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
                                                                  Convert.ToInt32(dtPartidas.Rows[index]["Clas_Producto"]),
                                                                  Convert.ToInt32(dtPartidas.Rows[index]["Clas_Concepto"]));

                row["PorcentajeIVA"] = Convert.ToDecimal(dtIVA.Rows[0][4]);

                if (dtIVA.Rows[0][0].ToString() == "")
                {
                    row["IVAParametrizado"] = "NO";
                    row["PorcentajeIVA"] = Convert.ToDecimal(ddlIva.SelectedItem.ToString());
                }
                else
                {
                    row["IVAParametrizado"] = "SI";
                }


                index++;
            }

            return dtPartidas;
        }

        protected void Nueva_Click(object sender, EventArgs e)
        {
            PrepareSale();
            bloqueoControles(true);

        }

        protected void Refresh_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "openpdf();", true);

        }
        public void GeneraFacturaEnPopUp(string sRuta)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Window1", "<script> window.open('" + sRuta + "',''," +
            "'top=350,left=400,height=400,width=500,status=yes,toolbar=no,menubar=no,location=no resizable=yes');</script>", false);
        }


        protected void Generar_Click(object sender, EventArgs e)
        {
            try
            {
                iIDFactura.Value = "0";

                if (customernamelbl.Text != "")
                {
                    if (validaDatosDetalleFactura())
                    {
                        if (invoiceGridView.Rows.Count > 0)
                        {
                            bool valido = ValidarTextBoxes(invoiceGridView);
                            if (!valido)
                            {
                                LabelTool.ShowLabel(errorLabel, "Ingrese los datos faltantes", System.Drawing.Color.DarkRed);
                            }
                            else
                            {
                                LabelTool.HideLabel(errorLabel);
                                int? iSucursal = null;

                                InvoiceBLL Invoice = new InvoiceBLL();
                                FacturaLibreController FacturaLibre = new FacturaLibreController();

                                dExchange = ControlsBLL.GetExchange(Convert.ToInt32(Session["Company"]), 109, Convert.ToInt32(ddlmoneda.SelectedValue), DateTime.Today);
                                if (Session["Office"].ToString().Length != 0)
                                {
                                    iSucursal = Convert.ToInt32(Session["Office"]);
                                }
                                //Obtiene factura concepto
                                int iFacturaConcepto = Invoice.GetFnFacturaConcepto(Convert.ToInt32(empresa), iSucursal);
                                if (iFacturaConcepto > 0)
                                {
                                    if (ActualizaDatoPagoCliente())
                                    {
                                        //Valida que no exista Folio  
                                        int ValidaFolio = Invoice.CheckFolio(Convert.ToInt32(Session["Company"]), 0, Convert.ToInt32(ddldocto.SelectedValue), Convert.ToInt32(ddlserie.SelectedValue), Convert.ToInt32(txtfolio.Text));

                                        if (ValidaFolio == 0)
                                        {
                                            if (ddldocto.SelectedValue == "-59")//documento electronico
                                            {
                                                // sirve para traer el 'importe'total en letra 
                                                HttpContext.Current.Session["dropMonedaText"] = ddlmoneda.SelectedItem.Text;
                                                int iFolio = Convert.ToInt32(txtfolio.Text);
                                                if (validacionpara_e_docto(iFolio))
                                                {

                                                    int iNumeroFactLibre = SalvarFactura(iFacturaConcepto);
                                                    if (iNumeroFactLibre > 0)
                                                    {
                                                        iIDFactura.Value = Convert.ToString(Invoice.GetfnIDFactura(Convert.ToInt32(Session["Company"]), iNumeroFactLibre)); //Obtiene ID  factura 


                                                        if (ValidacionDatosElectronico())
                                                        {
                                                            FacturacionCancelacionController.gfFactura_Contabiliza(1, Convert.ToInt32(Session["Company"]), Convert.ToInt32(iIDFactura.Value));

                                                            FacturaServicio.Servicio sf = new FacturaServicio.Servicio();

                                                            string sresultado = sf.gfs_generaComprobanteFisica(Convert.ToInt32(iIDFactura.Value), 0, Session["sDataBase"].ToString(), Convert.ToInt32(Session["Company"].ToString()), Convert.ToInt32(iClientNumber.Value), "");


                                                            string[] asresultado = sresultado.Split('|');
                                                            string stempA = asresultado[0];
                                                            string sTempB = asresultado[1];
                                                            string sTempC = asresultado[2];
                                                            if (stempA.Split(':')[1].ToUpper() == "OK")
                                                                new FacturacionCancelacionController().gfFactura_Tienda(Convert.ToInt32(Session["Company"].ToString()), Convert.ToInt32(iIDFactura.Value), sTempB.Split(':')[1]);
                                                            else
                                                            {
                                                                LabelTool.ShowLabel(errorLabel, "* Incidencia " + sTempC.Split(':')[1], System.Drawing.Color.DarkRed);
                                                                throw new Exception("* Incidencia " + sTempC.Split(':')[1]);
                                                            }

                                                            string sTempFile = @WebConfigurationManager.AppSettings["sfolderRaiz"].ToString() + sTempC.Split(':')[2];
                                                            string sRes = CFDIProgram.gfComprimirFichero(sTempFile);
                                                            Session["ArchivoZip"] = sRes;
                                                            sTempFile = sTempFile.Replace(@"\", "/");
                                                            // sTempFile = sTempFile.ToUpper().Replace(@"//VMS00001392-2//PROGRAM FILES/INTEGRA EMPRESARIAL/", "http://189.206.75.72/GMCFacturas/");
                                                            sTempFile = sTempFile.ToUpper().Replace(@WebConfigurationManager.AppSettings["sfolderNameDestino2"].ToString(), @WebConfigurationManager.AppSettings["GMCFacturas"].ToString());
                                                            errorLabel.Text = sTempFile;
                                                            Session["Rutaelectronico"] = sTempFile;

                                                            LabelTool.ShowLabel(errorLabel, "Factura electrónica generada exitosamente", System.Drawing.Color.DarkRed);
                                                            salelbl.Text = "Venta exitosa, ¿desea realizar el pago?";

                                                            bloqueoControles(false);
                                                            salemsgmdl.Show();

                                                        }

                                                    }

                                                }

                                            }
                                            // SI ES NOTA DE CREDITO
                                            else
                                            {

                                                int iNumeroFactLibre = SalvarFactura(iFacturaConcepto);
                                                if (iNumeroFactLibre > 0)
                                                {
                                                    //Obtiene ID  factura
                                                    iIDFactura.Value = Convert.ToString(Invoice.GetfnIDFactura(Convert.ToInt32(Session["Company"]), iNumeroFactLibre));

                                                    DataTable dtContabilidad = VentasController.realizaContabilidad(1, Convert.ToInt32(Session["Company"].ToString()), Convert.ToInt32(iIDFactura.Value)); //cambia estatus a contabilizada

                                                    sNumeroTicket = FacturaLibre.ObtenNumeroTicket(0, INTEGRA.Global.DataBase, empresa, Convert.ToString(Session["Office"]),
                                                                  Convert.ToString(ddldocto.SelectedValue), Convert.ToString(ddlserie.SelectedValue), Convert.ToString(txtfolio.Text), " "); //Obtiene cadena ticket


                                                    /////////////////manda a timbrar
                                                    FacturaServicio.Servicio sf = new FacturaServicio.Servicio();
                                                    string sresultado = sf.gfs_generaComprobanteFisica(Convert.ToInt32(iIDFactura.Value), 1, INTEGRA.Global.DataBase,
                                                                                                       Convert.ToInt32(Session["Company"]), Convert.ToInt32(iClientNumber.Value), "CONTACTOS");



                                                    string[] asresultado = sresultado.Split('|');
                                                    string stempA = asresultado[0];
                                                    string sTempB = asresultado[1];
                                                    string sTempC = asresultado[2];
                                                    if (stempA.Split(':')[1].ToUpper() == "OK")
                                                    {
                                                        LabelTool.ShowLabel(errorLabel, sresultado, System.Drawing.Color.DarkRed);
                                                        new FacturacionCancelacionController().gfFactura_Tienda(Convert.ToInt32(Session["Company"].ToString()), Convert.ToInt32(iIDFactura.Value), "");
                                                    }
                                                    else
                                                    {

                                                        throw new Exception("* Incidencia " + sTempC.Split(':')[1]);
                                                    }
                                                    ///////////////////////

                                                    bloqueoControles(false);
                                                    salelbl.Text = "Venta exitosa, ¿desea realizar el pago?";
                                                    errorLabel.Text = "";
                                                    bloqueoControles(false);
                                                    salemsgmdl.Show();

                                                }
                                            }
                                        }
                                        else
                                        {
                                            LabelTool.ShowLabel(errorLabel, "El número de folio ya esta en uso, intente nuevamente", System.Drawing.Color.DarkRed);

                                        }
                                    }

                                }
                                else
                                {
                                    LabelTool.ShowLabel(errorLabel, "No cuenta con concepto contable", System.Drawing.Color.DarkRed);
                                }
                            }
                        }
                        else
                        {
                            LabelTool.ShowLabel(errorLabel, "Ingrese al menos un concepto", System.Drawing.Color.DarkRed);
                        }

                    }

                    else
                    {
                        LabelTool.ShowLabel(errorLabel, "Falta el campo" + sdatofaltante, System.Drawing.Color.DarkRed);
                    }



                }
                else
                {
                    LabelTool.ShowLabel(errorLabel, "Ingrese un cliente", System.Drawing.Color.DarkRed);
                }
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref errorLabel);

            }
            finally
            {

            }

        }

        protected void PrintDocument(object sender, EventArgs e)
        {
            if (ddldocto.SelectedValue == "-59")
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
                ImprimeNotaVenta();

            }
        }

        protected void SalvarDetalle(int iNumeroFactLibre)
        {
            foreach (GridViewRow row in invoiceGridView.Rows)
                SaveDetail(1, row.RowIndex, iNumeroFactLibre);
        }

        protected void AddOption(object sender, EventArgs e)
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

            foreach (GridViewRow row1 in grvCollet.Rows)
            {
                DataRow dr = tablanewoption.NewRow();

                Label lbl = (Label)grvCollet.Rows[row1.RowIndex].FindControl("lblNumber");
                dr["Numero"] = Convert.ToInt32(lbl.Text);

                LinkButton lkbtnadd = (LinkButton)grvCollet.Rows[row1.RowIndex].FindControl("lblAdd");
                dr["Mas"] = Convert.ToString(lkbtnadd.Text);

                dr["Descripcion"] = Server.HtmlDecode(row1.Cells[2].Text);

                TextBox drp = (TextBox)grvCollet.Rows[row1.RowIndex].FindControl("txtIngreso");
                dr["Ingreso"] = drp.Text.Replace('$', ' ').Trim();

                TextBox refe = (TextBox)grvCollet.Rows[row1.RowIndex].FindControl("txtReferencia");
                dr["Referencia"] = refe.Text;

                Label lbltc = (Label)grvCollet.Rows[row1.RowIndex].FindControl("lblTipocambio");
                dr["TC"] = lbltc.Text;

                Label lblcurrency = (Label)grvCollet.Rows[row1.RowIndex].FindControl("lblCurrency");
                dr["Clas_Moneda"] = Convert.ToInt32(lblcurrency.Text);

                Label lblchange = (Label)grvCollet.Rows[row1.RowIndex].FindControl("lblChange");
                dr["Cambio"] = Convert.ToInt32(lblchange.Text);

                tablanewoption.Rows.Add(dr);

            }

            LinkButton lkbtnnew = (LinkButton)grvCollet.Rows[grvCollet.Rows[valChk].RowIndex].FindControl("lblAdd");

            if (lkbtnnew.Text == "+")
            {

                DataRow drnew = tablanewoption.NewRow();
                Label lblnew = (Label)grvCollet.Rows[grvCollet.Rows[valChk].RowIndex].FindControl("lblNumber");
                drnew["Numero"] = Convert.ToInt32(lblnew.Text);

                drnew["Descripcion"] = Server.HtmlDecode(grvCollet.Rows[valChk].Cells[2].Text);

                TextBox drp = (TextBox)grvCollet.Rows[grvCollet.Rows[valChk].RowIndex].FindControl("txtIngreso");
                drnew["Ingreso"] = "0.00";

                Label lbltc = (Label)grvCollet.Rows[grvCollet.Rows[valChk].RowIndex].FindControl("lblTipocambio");
                drnew["TC"] = Convert.ToDecimal(lbltc.Text);

                Label lblcurrency = (Label)grvCollet.Rows[grvCollet.Rows[valChk].RowIndex].FindControl("lblCurrency");
                drnew["Clas_Moneda"] = Convert.ToInt32(lblcurrency.Text);

                Label lblchange = (Label)grvCollet.Rows[grvCollet.Rows[valChk].RowIndex].FindControl("lblChange");
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
            GridViewTool.Bind(sortedDT, grvCollet);

        }

        protected void ImporteTextChanged(object sender, EventArgs e)
        {

            InvoiceBLL Invoice = new InvoiceBLL();
            TextBox txt = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt.NamingContainer;
            TextBox importetxt = new TextBox();
            importetxt = (TextBox)row.FindControl("txtIngreso");
            int iColletIndex = row.RowIndex;
            decimal dImporteResta = 0, dcnt = 0;
            dImportePago = 0;
            int ichange = 0;
            Label change = (Label)grvCollet.Rows[row.RowIndex].FindControl("lblChange");
            ichange = Convert.ToInt32(change.Text);
            Label TipoCambio = (Label)grvCollet.Rows[row.RowIndex].FindControl("lblTipocambio");
            dExchange = Convert.ToDecimal(TipoCambio.Text);

            if (ValImportText(importetxt, ichange, dImporteResta, dcnt))
            {
                DataTable TablaDesglose = create_table_desglose();
                foreach (GridViewRow row1 in grvCollet.Rows)
                {
                    TextBox importtxt = new TextBox();
                    importtxt = (TextBox)row1.FindControl("txtIngreso");
                    Label lblchange = (Label)grvCollet.Rows[row1.RowIndex].FindControl("lblTipocambio");
                    decimal exchange = Convert.ToDecimal(lblchange.Text);

                    if (importtxt != null && importtxt.Text != "")
                    {
                        decimal diimport = Convert.ToDecimal(importtxt.Text.Replace('$', ' ')) * exchange;

                        if (Convert.ToInt32(ddlmoneda.SelectedValue) == 110)
                        {
                            diimport = diimport / dTipoCambio;
                        }

                        dImportePago += diimport;


                        if (diimport > 0)
                        {
                            DataRow rows = TablaDesglose.NewRow();

                            rows["Descripcion"] = grvCollet.DataKeys[row1.RowIndex].Values["Descripcion"];
                            rows["Ingreso"] = importtxt.Text;

                            TablaDesglose.Rows.Add(rows);
                        }
                    }
                }

                txtPagoImporte.Text = Convert.ToString(dImportePago.ToString("c2"));
                lblTotalpagotkt.Text = Convert.ToString(dImportePago.ToString("c2"));
                dCambio = Convert.ToDecimal(txtPagoImporte.Text.Replace('$', ' ')) - Convert.ToDecimal(txtPayTotal.Text.Replace('$', ' '));

                if (dCambio > 0)
                {
                    txtCambiopago.Text = "$" + dCambio.ToString();
                    lblChangetkt.Text = "$" + dCambio.ToString();
                }

                else
                {
                    txtCambiopago.Text = "$0.00";
                    lblChangetkt.Text = "$0.00";
                }
                dImporteResta = Convert.ToDecimal(txtPayTotal.Text.Replace('$', ' ')) - Convert.ToDecimal(txtPagoImporte.Text.Replace('$', ' '));

                if (dImporteResta > 0)
                {
                    txtRestan.Text = "$" + dImporteResta.ToString();
                }
                else
                {
                    txtRestan.Text = "$0.00";

                }

                if (dImporteResta <= 0)
                {

                    foreach (GridViewRow row2 in grvCollet.Rows)
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

                    foreach (GridViewRow row2 in grvCollet.Rows)
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

        protected void ImporteChangeTextChanged(object sender, EventArgs e)
        {

            TextBox txt = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt.NamingContainer;
            TextBox importetxt = new TextBox();
            importetxt = (TextBox)row.FindControl("txtIngreso");
            int iColletIndex = row.RowIndex;

            decimal dcnt = 0;
            Label lblchange = (Label)grvCambio.Rows[row.RowIndex].FindControl("lblTypechange");
            decimal exchange = Convert.ToDecimal(lblchange.Text);

            if (ValidacionImporteCambio(importetxt, exchange, dcnt))
            {

                foreach (GridViewRow row1 in grvCambio.Rows)
                {

                    if (importetxt != null && importetxt.Text != "")
                    {
                        decimal diimport = Convert.ToDecimal(importetxt.Text.Replace('$', ' ')) * exchange;

                        if (Convert.ToInt32(ddlmoneda.SelectedValue) == 110)
                        {
                            diimport = diimport / dTipoCambio;
                        }

                        dImporteCambio += diimport;

                    }

                }

            }

        }

        protected void NoContinuaClick(object sender, EventArgs e)
        {
            mdlTransaccion.Hide();
        }

        protected void NoContinuaCambioClick(object sender, EventArgs e)
        {
            mdlTransaccionCambio.Hide();
        }

        protected void CambioSinTransaccionClick(object sender, EventArgs e)
        {
            if (dImporteCambio == dCambio)
            {
                if (GuardaDetallePago())
                {
                    if (bGuardaCambio())
                    {
                        GuardaEncabezadoPago();
                        mdlTransaccionCambio.Hide();
                        mdlCambio.Hide();
                        mdlPago.Hide();
                        PrintSaleReportPago();
                    }

                }
            }

        }

        protected void PagoSinTransaccionClick(object sender, EventArgs e)
        {
            validaExisteCambio();
            mdlTransaccion.Hide();
        }

        protected void PayClick(object sender, EventArgs e)
        {
            bValidaDatosPago();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);

        }

        protected void addchangeClick(object sender, EventArgs e)
        {
            if (dImporteCambio > 0)
            {
                if (!bvalidacambio())
                {

                }

            }
            else
            {
                LabelTool.ShowLabel(lblMensajeCambio, "Ingrese un monto", System.Drawing.Color.DarkRed);
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "gfProcesoEsconde();", true);
        }

        protected void lnkCancelaCambio_Click(object sender, EventArgs e)
        {

            mdlCambio.Hide();

        }

        protected void btnPersonaMoral_Click(object sender, EventArgs e)
        {
            Session["sPaginaAsignada"] = "Ventas/FacturaLibreMorales.aspx";
            Response.Redirect("FacturaLibreMorales.aspx");
        }
        protected void btnPersonaFisica_Click(object sender, EventArgs e)
        {
            Session["sPaginaAsignada"] = "Ventas/FacturaLibreFisicas.aspx";
            Response.Redirect("FacturaLibreFisicas.aspx");
        }
        #endregion

        #region Metodos/Propiedades

        public DataTable generatablapartidas()
        {

            DataTable TablaPartidasFinales = new DataTable();

            TablaPartidasFinales.Columns.Add("Numero", typeof(int));
            TablaPartidasFinales.Columns.Add("Cantidad", typeof(decimal));
            TablaPartidasFinales.Columns.Add("TipoProducto", typeof(int));
            TablaPartidasFinales.Columns.Add("Producto", typeof(int));
            TablaPartidasFinales.Columns.Add("TipoConcepto", typeof(int));
            TablaPartidasFinales.Columns.Add("Concepto", typeof(int));
            TablaPartidasFinales.Columns.Add("Clas_UM", typeof(int));
            TablaPartidasFinales.Columns.Add("Familia", typeof(int));
            TablaPartidasFinales.Columns.Add("Descripcion_Producto", typeof(string));
            TablaPartidasFinales.Columns.Add("Precio", typeof(decimal));
            TablaPartidasFinales.Columns.Add("Descripcion_Unidad", typeof(string));
            TablaPartidasFinales.Columns.Add("Total", typeof(decimal));
            TablaPartidasFinales.Columns.Add("NumeroProducto", typeof(string));

            foreach (GridViewRow row1 in invoiceGridView.Rows)
            {
                DataRow dr = TablaPartidasFinales.NewRow();

                Label lbl = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("Numberlbl");
                TextBox cantidadtxt = (TextBox)invoiceGridView.Rows[row1.RowIndex].FindControl("cantidadtxt");
                dr["Cantidad"] = Convert.ToDecimal(cantidadtxt.Text);
                Label lbltipoproducto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lbltipo_producto");
                dr["TipoProducto"] = Convert.ToInt32(lbltipoproducto.Text);
                Label lblproducto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblproducto");
                dr["Producto"] = Convert.ToInt32(lblproducto.Text);
                Label lbltipoconcepto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lbltipo_concepto");
                dr["TipoConcepto"] = Convert.ToInt32(lbltipoconcepto.Text);
                Label lblconcepto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblconcepto");
                dr["Concepto"] = Convert.ToInt32(lblconcepto.Text);
                Label lblclas_um = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblclas_um");
                dr["Clas_UM"] = Convert.ToInt32(lblclas_um.Text);
                Label lblfamilia = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblFamilia");
                dr["Familia"] = Convert.ToInt32(lblfamilia.Text);
                TextBox productotxt = (TextBox)invoiceGridView.Rows[row1.RowIndex].FindControl("productotxt");
                Label lblCaracteristicas = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblCaracteristicas");
                dr["Descripcion_Producto"] = Convert.ToString(productotxt.Text) + "\n" + Convert.ToString(lblCaracteristicas.Text);
                productotxt.Attributes.Add("onclick", "doClearProductsTextBox();");
                TextBox precio = (TextBox)invoiceGridView.Rows[row1.RowIndex].FindControl("preciotxt");
                dr["Precio"] = precio.Text;
                DropDownList dropunidad = (DropDownList)invoiceGridView.Rows[row1.RowIndex].FindControl("ddlunidad");
                dr["Descripcion_Unidad"] = dropunidad.SelectedItem.ToString();
                dr["Total"] = Convert.ToDecimal(cantidadtxt.Text) * Convert.ToDecimal(precio.Text);
                Label lblNumeroProducto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblNumeroProducto");
                dr["NumeroProducto"] = lblNumeroProducto.Text;

                TablaPartidasFinales.Rows.Add(dr);

            }
            return TablaPartidasFinales;
        }

        private void calcularTotal()
        {
            double subtotal = 0;
            double total = 0;
            double iva = 0, ivapartida = 0;
            string porcentageIa = "0." + ddlIva.SelectedItem.ToString();
            double porcentageIva = Convert.ToDouble(porcentageIa);

            lblSubtotal.Text = "";
            lblIva.Text = "";
            lblTotal.Text = "";

            foreach (GridViewRow row in invoiceGridView.Rows)
            {
                Label tot = (Label)row.FindControl("lblTotal");
                Label lblPorcentajeIva = (Label)invoiceGridView.Rows[row.RowIndex].FindControl("lblPorcentajeIva");
                ivapartida = Convert.ToDouble(lblPorcentajeIva.Text);

                if (tot.Text != "")
                {
                    subtotal = subtotal + Convert.ToDouble(tot.Text);

                    if (ivapartida > 0)
                        iva += (Convert.ToDouble(tot.Text) * (ivapartida));

                }

            }

            total = subtotal + iva;
            lblSubtotal.Text = subtotal.ToString("f2");
            lblIva.Text = iva.ToString("f2");
            lblTotal.Text = total.ToString("f2");

        }

        protected void IVAChanged(object sender, EventArgs e)
        {
            try
            {
                CambiaPorcentajeIVA();
                calcularTotal();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

            }
        }

        public bool ValidarTextBoxes(Control parent)
        {

            bool validoTotal = true;
            bool valido = true;

            foreach (GridViewRow row1 in invoiceGridView.Rows)
            {
                valido = false;
                Label lbl = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("Numberlbl");
                TextBox cantidadtxt = (TextBox)invoiceGridView.Rows[row1.RowIndex].FindControl("cantidadtxt");
                Label lbltipoproducto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lbltipo_producto");
                Label lblproducto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblproducto");
                Label lbltipoconcepto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lbltipo_concepto");
                Label lblconcepto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblconcepto");
                Label lblclas_um = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblclas_um");
                Label lblfamilia = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblFamilia");
                TextBox productotxt = (TextBox)invoiceGridView.Rows[row1.RowIndex].FindControl("productotxt");
                TextBox precio = (TextBox)invoiceGridView.Rows[row1.RowIndex].FindControl("preciotxt");
                DropDownList dropunidad = (DropDownList)invoiceGridView.Rows[row1.RowIndex].FindControl("ddlunidad");
                Label lblNumeroProducto = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblNumeroProducto");
                Label lblCaracteristicas = (Label)invoiceGridView.Rows[row1.RowIndex].FindControl("lblCaracteristicas");

                if (cantidadtxt != null && cantidadtxt.Text != "" && Convert.ToDecimal(cantidadtxt.Text) > 0)
                {
                    cantidadtxt.BorderColor = System.Drawing.Color.Gray;
                    valido = true;

                }
                else
                {
                    cantidadtxt.BorderColor = System.Drawing.Color.DarkRed;
                    valido = false;
                }

                validoTotal = validoTotal && valido;

                if (productotxt != null && productotxt.Text != "")
                {

                    productotxt.BorderColor = System.Drawing.Color.Gray;
                    valido = true;

                }
                else if (lblCaracteristicas.Text != null && lblCaracteristicas.Text == "")
                {
                    productotxt.BorderColor = System.Drawing.Color.DarkRed;
                    valido = false;
                }

                validoTotal = validoTotal && valido;

                if (precio.Text != null && precio.Text != "")
                {
                    precio.BorderColor = System.Drawing.Color.Gray;
                    valido = true;
                }
                else
                {
                    precio.BorderColor = System.Drawing.Color.DarkRed;
                    valido = false;
                }

                validoTotal = validoTotal && valido;

                if (dropunidad.SelectedValue != null && dropunidad.SelectedValue != "")
                {
                    dropunidad.BorderColor = System.Drawing.Color.Gray;
                    valido = true;
                }
                else
                {
                    dropunidad.BorderColor = System.Drawing.Color.DarkRed;
                    valido = false;
                }


                validoTotal = validoTotal && valido;


            }
            return validoTotal;
        }


        public bool validaDatosDetalleFactura()
        {


            if (Convert.ToString(ddlforpago.SelectedValue) == "")
            {

                sdatofaltante = " Forma de pago";
                return false;
            }
            if (Convert.ToString(ddlmoneda.SelectedValue) == "")
            {
                sdatofaltante = " Moneda";
                return false;
            }
            if (Convert.ToString(ddlIva.SelectedValue) == "")
            {
                sdatofaltante = " Iva";
                return false;
            }
            if (Convert.ToString(ddlmetodo.SelectedValue) == "")
            {
                sdatofaltante = " Método de pago";
                return false;
            }
            if (Convert.ToString(ddldocto.SelectedValue) == "")
            {
                sdatofaltante = " Documento";
                return false;
            }

            if (Convert.ToString(ddlserie.SelectedValue) == "")
            {

                sdatofaltante = " Serie";
                return false;
            }
            if (txtfolio.Text == "")
            {
                sdatofaltante = " Folio";
                return false;
            }

            return true;
        }


        private bool validacionpara_e_docto(int iFolio)
        {
            if (ValidacionDatos(iFolio))
            {

                TablaCorreoCliente = DatosCorreoCliente();

                if (TablaCorreoCliente != null && TablaCorreoCliente.Rows.Count > 0)
                {
                    if (Convert.ToString(TablaCorreoCliente.Rows[0][0]) == "")
                    {
                        LabelTool.ShowLabel(errorLabel, "Error, no se ha encontrado correo electrónico del cliente", System.Drawing.Color.DarkRed);
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

                LabelTool.HideLabel(errorLabel);
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
                    LabelTool.ShowLabel(errorLabel, "Error, no se encontró el archivo de llave privada", System.Drawing.Color.DarkRed);

                }
                if (Convert.ToString(TablaDoctos.Rows[0][0]) != "")
                {
                    sRutaKey = Convert.ToString(TablaDoctos.Rows[0][0]);
                }
                else
                {
                    LabelTool.ShowLabel(errorLabel, "Error, no se encontró el archivo de certificado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][1]) != "")
                {
                    sContraseñaKey = Convert.ToString(TablaDoctos.Rows[0][1]);
                }
                else
                {
                    LabelTool.ShowLabel(errorLabel, "Error, no se encontró la contraseña del archivo de llave privada", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][2]) == "")
                {
                    LabelTool.ShowLabel(errorLabel, "Error, no se encontró el archivo de certificado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][3]) == "")
                {
                    LabelTool.ShowLabel(errorLabel, "Error,no se capturó el año de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToInt32(TablaDoctos.Rows[0][3]) > 9999)
                {
                    LabelTool.ShowLabel(errorLabel, "Error,en la captura del año de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctos.Rows[0][4]) == "")
                {
                    LabelTool.ShowLabel(errorLabel, "Error,no se capturó el número de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }
                if ((TablaDoctos.Rows[0][5]) != null && Convert.ToString(TablaDoctos.Rows[0][5].ToString()) != "")
                {
                    if (iFolio > Convert.ToInt32(TablaDoctos.Rows[0][5]))
                    {
                        LabelTool.ShowLabel(errorLabel, "Error,el número de documento es mayor al folio final capturado en la configuración de documentos electrónicos", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                return true;
            }

            else
            {
                LabelTool.ShowLabel(errorLabel, "Error, no se ha capturado la configuración basica de los folios para la operación de documentos electronicos", System.Drawing.Color.DarkRed);
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
                        LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del emisor no puede ser menor a 12 caracteres", System.Drawing.Color.DarkRed);
                    }
                    if (rfc.Length > 13)
                    {
                        LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del emisor no puede ser mayor a 13 caracteres", System.Drawing.Color.DarkRed);
                    }

                }
                else
                {
                    LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }

                if (Convert.ToString(TablaEmpresa.Rows[0][1]) == null)
                {
                    LabelTool.ShowLabel(errorLabel, "Error,el nombre del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][2]) == null)
                {
                    LabelTool.ShowLabel(errorLabel, "Error,el domicilio del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][3]) == null)
                {
                    LabelTool.ShowLabel(errorLabel, "Error,la colonia de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][4]) == null)
                {
                    LabelTool.ShowLabel(errorLabel, "Error,la delegación/municipio de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][5]) == null)
                {
                    LabelTool.ShowLabel(errorLabel, "Error,el estado de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }
                if (Convert.ToString(TablaEmpresa.Rows[0][6]) == null)
                {
                    string cp = Convert.ToString(TablaEmpresa.Rows[0][6]);

                    if (cp.Length < 5)
                    {
                        LabelTool.ShowLabel(errorLabel, "Error,el código postal del emisor no puede ser menor a 5 caracteres", System.Drawing.Color.DarkRed);
                    }
                    if (cp.Length > 5)
                    {
                        LabelTool.ShowLabel(errorLabel, "Error,el código postal del emisor no puede ser mayor a 5 caracteres", System.Drawing.Color.DarkRed);
                    }

                }
                if (Convert.ToString(TablaEmpresa.Rows[0][7]) == null)
                {
                    LabelTool.ShowLabel(errorLabel, "Error,el régimen fiscal del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                }


                return true;
            }

            else
            {
                LabelTool.ShowLabel(errorLabel, "Error, no se ha capturado la configuración basica de la empresa para la operación de documentos electrónicos", System.Drawing.Color.DarkRed);
                return false;
            }

        }

        private bool ValidacionDatosCliente()
        {
            CustomerBLL DatoPersona = new CustomerBLL();
            int idClient = Convert.ToInt32(iClientNumber.Value);
            DataTable TablaCliente;
            string rfc;
            int tipo = 0;
            rfc = "";

            if ((TablaCliente = DatoPersona.GetPersonData(Convert.ToInt32(Session["Company"]), idClient)) != null)
            {
                if (Convert.ToString(TablaCliente.Rows[0][14]) != null && Convert.ToString(TablaCliente.Rows[0][14]) != "")
                {
                    spersonalidad = Convert.ToString(TablaCliente.Rows[0][14]);
                }
                else
                {
                    LabelTool.ShowLabel(errorLabel, "Error,el cliente no tiene un tipo de persona juridica", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaCliente.Rows[0][7]) != null && Convert.ToString(TablaCliente.Rows[0][7]) != "")
                {
                    rfc = Convert.ToString(TablaCliente.Rows[0][7]);

                    if (rfc.ToUpper() != "XEXX010101000" || rfc.ToUpper() != "XAXX010101000")
                    {
                        if (rfc.Length < 12)
                        {
                            LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del cliente no puede ser menor a 12 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                        if (rfc.Length > 13)
                        {
                            LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del cliente no puede ser mayor a 13 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }
                else
                {
                    LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                }

                if (spersonalidad == "F")
                {
                    tipo = 1;

                    if (rfc.ToUpper() != "XEXX010101000" || rfc.ToUpper() != "XAXX010101000")
                    {

                        if (rfc.Length != 13)
                        {
                            LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del cliente no puede ser diferente a 13 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }

                }
                else
                {
                    tipo = 0;

                    if (rfc.ToUpper() == "XEXX010101000" || rfc.ToUpper() == "XAXX010101000")
                        Console.Write("RFC GABACHO");
                    else
                    {
                        if (rfc.Length != 12)
                        {
                            LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del cliente no puede diferente a 12 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }

                if (Convert.ToString(TablaCliente.Rows[0][2]) == null)
                {
                    LabelTool.ShowLabel(errorLabel, "Error,el nombre del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                }



                DataTable DataAddress = GetAddress();

                if (DataAddress != null)
                {
                    if (DataAddress.Rows[0][0] == null)
                    {
                        if (Convert.ToString(DataAddress.Rows[0][0]) == "")

                            LabelTool.ShowLabel(errorLabel, "El domicilio del cliente  no se ha capturado", System.Drawing.Color.DarkRed);
                    }

                    if (DataAddress.Rows[0][1] == null)
                    {
                        if (Convert.ToString(DataAddress.Rows[0][1]) == "")
                            LabelTool.ShowLabel(errorLabel, "La colonia de la dirección  del cliente  no se ha capturado", System.Drawing.Color.DarkRed);
                    }

                    if (DataAddress.Rows[0][2] == null)
                    {
                        if (Convert.ToString(DataAddress.Rows[0][2]) == "")

                            LabelTool.ShowLabel(errorLabel, "El código postal de la dirección  del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                    }
                    else
                    {
                        string cp = Convert.ToString(DataAddress.Rows[0][2]);

                        if (cp.Length != 5)
                        {
                            LabelTool.ShowLabel(errorLabel, "El código postal de la dirección  del cliente deberá ser igual a 5 caracteres", System.Drawing.Color.DarkRed);
                        }
                    }

                    if (DataAddress.Rows[0][3] == null)
                    {
                        if (Convert.ToString(DataAddress.Rows[0][3]) == "")
                            LabelTool.ShowLabel(errorLabel, "La delegación/municipio de la dirección  del cliente  no se ha capturado", System.Drawing.Color.DarkRed);
                    }

                    if (DataAddress.Rows[0][4] == null)
                    {
                        if (Convert.ToString(DataAddress.Rows[0][4]) == "")
                            LabelTool.ShowLabel(errorLabel, "La estado de la dirección  del cliente  no se ha capturado", System.Drawing.Color.DarkRed);
                    }
                    if (DataAddress.Rows[0][5] == null || DataAddress.Rows[0][6] == null)
                    {
                        LabelTool.ShowLabel(errorLabel, "La dirección del cliente no cuenta con un país", System.Drawing.Color.DarkRed);
                    }

                }

                return true;
            }

            else
            {
                LabelTool.ShowLabel(errorLabel, "Error, no se ha capturado la configuración basica del cliente para la operación de documentos electrónicos", System.Drawing.Color.DarkRed);
                return false;
            }

        }



        private bool ValidacionDatosMailCompany()
        {
            DataTable TablaMail;
            SaleBLL Sale = new SaleBLL();

            if ((TablaMail = Sale.GetStoresValidationsAndMailCompany(Convert.ToInt32(Session["Company"]))) != null)
            {
                if (Convert.ToString(TablaMail.Rows[0][4]) == null)
                {
                    LabelTool.ShowLabel(errorLabel, "Error, falta dato cuenta de correo del usuario", System.Drawing.Color.DarkRed);
                    return false;
                }
                if (Convert.ToString(TablaMail.Rows[0][6]) == null)
                {
                    LabelTool.ShowLabel(errorLabel, "Error, falta dato cuenta de correo del usuario", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (TablaMail != null)
                {
                    if (Convert.ToInt32(TablaMail.Rows[0][5]) == 1)
                        if (Convert.ToString(TablaMail.Rows[0][7]) == null)
                        {
                            LabelTool.ShowLabel(errorLabel, "Error, falta dato en la contraseña del correo del usuario", System.Drawing.Color.DarkRed);
                            return false;
                        }
                }

                return true;
            }

            else
            {
                LabelTool.ShowLabel(errorLabel, "Error, el usuario no tiene configurado su correo", System.Drawing.Color.DarkRed);
                return false;
            }

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
                    LabelTool.ShowLabel(errorLabel, "Error, no se encontró el archivo de llave privada", System.Drawing.Color.DarkRed);

                }
                if (Convert.ToString(TablaDoctosE.Rows[0][0]) != "")
                {
                    sRutaKey = Convert.ToString(TablaDoctosE.Rows[0][0]);
                }
                else
                {
                    LabelTool.ShowLabel(errorLabel, "Error, no se encontró el archivo de certificado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][1]) != "")
                {
                    sContraseñaKey = Convert.ToString(TablaDoctosE.Rows[0][1]);
                }
                else
                {
                    LabelTool.ShowLabel(errorLabel, "Error, no se encontró la contraseña del archivo de llave privada", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][2]) == "")
                {
                    LabelTool.ShowLabel(errorLabel, "Error, no se encontró el archivo de certificado", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][3]) == "")
                {
                    LabelTool.ShowLabel(errorLabel, "Error,no se capturó el año de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToInt32(TablaDoctosE.Rows[0][3]) > 9999)
                {
                    LabelTool.ShowLabel(errorLabel, "Error,en la captura del año de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }

                if (Convert.ToString(TablaDoctosE.Rows[0][4]) == "")
                {
                    LabelTool.ShowLabel(errorLabel, "Error,no se capturó el número de aprobación", System.Drawing.Color.DarkRed);
                    return false;
                }


                return true;
            }

            else
            {
                LabelTool.ShowLabel(errorLabel, "Error, no se ha capturado la configuración basica de los folios para la operación de documentos electrónicos", System.Drawing.Color.DarkRed);
                return false;
            }

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
                            LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del emisor no puede ser menor a 12 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                        if (rfc.Length > 13)
                        {
                            LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del emisor no puede ser mayor a 13 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }

                else
                {
                    LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }


                if (TablaEmisor.Rows[0][1] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][1]) == "")
                    {

                        LabelTool.ShowLabel(errorLabel, "El nombre del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaEmisor.Rows[0][2] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][2]) == "")
                    {
                        LabelTool.ShowLabel(errorLabel, "El domicilio del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaEmisor.Rows[0][3] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][3]) == "")
                    {
                        LabelTool.ShowLabel(errorLabel, "La colonia de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaEmisor.Rows[0][4] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][4]) == "")
                    {
                        LabelTool.ShowLabel(errorLabel, "La delegación/municipio de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaEmisor.Rows[0][5] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][5]) == "")
                    {
                        LabelTool.ShowLabel(errorLabel, "El estado de la dirección del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaEmisor.Rows[0][6] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][6]) == "")

                        LabelTool.ShowLabel(errorLabel, "El código postal de la dirección  del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                }
                else
                {
                    string cp = Convert.ToString(TablaEmisor.Rows[0][6]);

                    if (cp.Length != 5)
                    {
                        LabelTool.ShowLabel(errorLabel, "El código postal de la dirección  del cliente deberá ser igual a 5 caracteres", System.Drawing.Color.DarkRed);
                    }
                }

                if (TablaEmisor.Rows[0][8] != null)
                {
                    if (Convert.ToString(TablaEmisor.Rows[0][8]) == "")
                    {
                        LabelTool.ShowLabel(errorLabel, "No se encuentra capturado el régimen fiscal de la empresa", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                return true;
            }

            else
            {
                LabelTool.ShowLabel(errorLabel, "Error, no se ha capturado la configuración basica de los folios para la operación de documentos electrónicos", System.Drawing.Color.DarkRed);
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
                            LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del cliente no puede ser menor a 12 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                        if (rfc.Length > 13)
                        {
                            LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del cliente no puede ser mayor a 13 caracteres", System.Drawing.Color.DarkRed);
                            return false;
                        }
                    }
                }

                else
                {
                    LabelTool.ShowLabel(errorLabel, "Error,el R.F.C del emisor no se ha capturado", System.Drawing.Color.DarkRed);
                    return false;
                }


                if (TablaReceptor.Rows[0][1] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][1]) == "")
                    {
                        LabelTool.ShowLabel(errorLabel, "El nombre del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaReceptor.Rows[0][2] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][2]) == "")
                    {
                        LabelTool.ShowLabel(errorLabel, "El domicilio del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaReceptor.Rows[0][3] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][3]) == "")
                    {
                        LabelTool.ShowLabel(errorLabel, "La colonia de la dirección del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaReceptor.Rows[0][4] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][4]) == "")

                        LabelTool.ShowLabel(errorLabel, "El código postal de la dirección  del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                }
                else
                {
                    string cp = Convert.ToString(TablaReceptor.Rows[0][4]);

                    if (cp.Length != 5)
                    {
                        LabelTool.ShowLabel(errorLabel, "El código postal de la dirección  del cliente deberá ser igual a 5 caracteres", System.Drawing.Color.DarkRed);
                    }
                }

                if (TablaReceptor.Rows[0][5] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][5]) == "")
                    {
                        LabelTool.ShowLabel(errorLabel, "La delegación/municipio de la dirección del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaReceptor.Rows[0][6] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][6]) == "")
                    {
                        LabelTool.ShowLabel(errorLabel, "El estado de la dirección del cliente no se ha capturado", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }


                if (TablaReceptor.Rows[0][8] != null)
                {
                    if (Convert.ToString(TablaReceptor.Rows[0][8]) == "")
                    {
                        LabelTool.ShowLabel(errorLabel, "La dirección del cliente no cuenta con un país", System.Drawing.Color.DarkRed);
                        return false;
                    }
                }

                if (TablaReceptor.Rows[0][7] != null)
                {
                    int porcentaje;
                    int ifamily = 3;
                    SaleBLL Invoice = new SaleBLL();
                    int iva = Convert.ToInt32(TablaReceptor.Rows[0][7]);
                    DataTable Tporcentaje = Invoice.GetIvaReceptor(Convert.ToInt32(Session["Company"]), iva, ifamily);
                    if (Tporcentaje != null)
                    {
                        porcentaje = Convert.ToInt32(Tporcentaje.Rows[0][0]);
                    }
                    else
                    {

                        LabelTool.ShowLabel(errorLabel, "No encontro el porcentaje del I.V.A", System.Drawing.Color.DarkRed);
                        return false;
                    }

                }

                else
                {
                    LabelTool.ShowLabel(errorLabel, "No se ha capturado el I.V.A", System.Drawing.Color.DarkRed);
                    return false;
                }


                return true;
            }

            else
            {
                LabelTool.ShowLabel(errorLabel, "Error, no se ha capturado la configuración basica del cliente para la operacion de documentos electronicos !!!", System.Drawing.Color.DarkRed);
                return false;
            }

        }

        private DataTable DatosDoctoE()
        {
            SaleBLL DatosDocumentoE = new SaleBLL();

            DatosDocumentoE.Company = Convert.ToInt32(Session["Company"]);
            DatosDocumentoE.Number = Convert.ToInt32(iIDFactura.Value);
            DatosDocumentoE.Type = 0;

            return DatosDocumentoE.GetDatosDoctoE();
        }

        private DataTable DatosDoctoEmisor()
        {
            SaleBLL DatosDocumentoEmisor = new SaleBLL();

            DatosDocumentoEmisor.Company = Convert.ToInt32(Session["Company"]);

            return DatosDocumentoEmisor.GetDatosDoctoEmisor();
        }


        private DataTable DatosDoctoReceptor()
        {
            SaleBLL DatosDocumentoReceptor = new SaleBLL();

            DatosDocumentoReceptor.Company = Convert.ToInt32(Session["Company"]);
            DatosDocumentoReceptor.Number = Convert.ToInt32(iIDFactura.Value);
            DatosDocumentoReceptor.Type = 0;

            return DatosDocumentoReceptor.GetDatosDoctoReceptor();
        }
        private DataTable DatosDocto()
        {
            SaleBLL DatosDocumento = new SaleBLL();

            DatosDocumento.Company = Convert.ToInt32(Session["Company"]);
            DatosDocumento.Documento = Convert.ToInt32(ddldocto.SelectedValue);
            DatosDocumento.Serie = Convert.ToInt32(ddlserie.SelectedValue);

            return DatosDocumento.GetDatosDocto();
        }

        private DataTable DatosEmpresa()
        {
            SaleBLL DatosEmp = new SaleBLL();

            DatosEmp.Company = Convert.ToInt32(Session["Company"]);

            return DatosEmp.GetDatosEmpresa();
        }

        private DataTable GetInvoices()
        {
            int idClient = Convert.ToInt32(iClientNumber.Value);
            SaleBLL TableInvoices = new SaleBLL();

            TableInvoices.Company = Convert.ToInt32(Session["Company"]);
            TableInvoices.Customer = idClient;
            TableInvoices.Documento = -59;
            TableInvoices.Type = 1;

            return TableInvoices.GetInvoices();
        }

        private DataTable GetAddress()
        {
            int idClient = Convert.ToInt32(iClientNumber.Value);
            SaleBLL TableAddress = new SaleBLL();

            TableAddress.Company = Convert.ToInt32(Session["Company"]);
            TableAddress.Customer = idClient;
            TableAddress.Type = 13;

            return TableAddress.GetAddress();
        }



        private void FillDoctoControls()
        {
            InvoiceBLL Invoice = new InvoiceBLL();
            FillDocument();
            FillSerie();
            if (ddldocto.SelectedValue != "" && ddlserie.SelectedValue != "")
            {
                int iFolio = Invoice.GetFolio(Convert.ToInt32(Session["Company"]), Convert.ToInt32(ddldocto.SelectedValue), Convert.ToInt32(ddlserie.SelectedValue));
                txtfolio.Text = iFolio.ToString();

                if (ddldocto.SelectedValue == "-59")
                {
                    txtfolio.Enabled = false;
                }
                else
                {
                    txtfolio.Enabled = true;
                }
            }

            FillDrop("moneda");
            FillDrop("forpago");
            FillDrop("metodopago");
            FillDrop("IVA");
        }

        private void FillDocument()
        {
            InvoiceBLL Invoice = new InvoiceBLL();
            int idClient = Convert.ToInt32(iClientNumber.Value);
            int sucursal = 0, relacion, registro;

            DataTable TempTable = new DataTable();
            DataTable TempPerson = new DataTable();
            DataTable TempOffice = new DataTable();
            DataTable TempClassDocto = new DataTable();
            DataTable TableDocuments = new DataTable();

            if (Session["Office"] != null)
                sucursal = Convert.ToInt32(Session["Office"]);


            DropTool.ClearDrop(ddldocto);

            if (sucursal != 0)
            {
                TempPerson = Invoice.GetRelationship(Convert.ToInt32(Session["Company"]), idClient);
                relacion = Convert.ToInt32(TempPerson.Rows[0][0]);

                if (relacion > 0)
                {
                    TempClassDocto = Invoice.GetClassDocto(Convert.ToInt32(Session["Company"]), Convert.ToInt32(Session["Person"]));
                    registro = Convert.ToInt32(TempClassDocto.Rows[0][0]);
                    TableDocuments = ControlsBLL.GetTypeDocument(Convert.ToInt32(Session["Company"]), Convert.ToInt32(Session["Person"]));
                    DropTool.FillDrop(ddldocto, TableDocuments, "Descripcion", "Numero");


                }
                else
                {
                    TempClassDocto = Invoice.GetCountOfficeDocto(Convert.ToInt32(Session["Company"]), sucursal);
                    registro = Convert.ToInt32(TempClassDocto.Rows[0][0]);
                    TableDocuments = ControlsBLL.GetDocument(Convert.ToInt32(Session["Company"]), sucursal);
                    DropTool.FillDrop(ddldocto, TableDocuments, "Descripcion", "Numero");

                }
            }
            else
            {
                TempPerson = Invoice.GetRelationship(Convert.ToInt32(Session["Company"]), Convert.ToInt32(Session["Person"]));
                relacion = Convert.ToInt32(TempPerson.Rows[0][0]);


                if (relacion > 0)
                {
                    TempClassDocto = Invoice.GetClassDocto(Convert.ToInt32(Session["Company"]), Convert.ToInt32(Session["Person"]));
                    registro = Convert.ToInt32(TempClassDocto.Rows[0][0]);
                    DropTool.FillDrop(ddldocto, TempClassDocto, "Descripcion", "Numero");

                }
                else
                {
                    TableDocuments = ControlsBLL.GetTypeDocument(Convert.ToInt32(Session["Company"]), Convert.ToInt32(Session["Person"]));
                    DropTool.FillDrop(ddldocto, TableDocuments, "Descripcion", "Numero");
                }

            }

            if (TableDocuments.Rows.Count > 0)
            {
                ddldocto.Enabled = true;
            }
            else
            {
                ddldocto.Enabled = false;
            }


        }

        private void FillSerie()
        {
            DropTool.ClearDrop(ddlserie);

            if (ddldocto.SelectedItem != null)
                DropTool.FillDrop(ddlserie, ControlsBLL.GetSeriesDoctos(Convert.ToInt32(Session["Company"]), Convert.ToInt32(Session["Person"]), Convert.ToInt32(ddldocto.SelectedValue), null), "Descripcion", "Numero");
        }

        private void FillDrop(string dropname)
        {
            DataTable table = new DataTable();
            DataTable temp_table = new DataTable();
            int ifather;
            int ifamily;

            switch (dropname)
            {
                case "moneda":
                    ifather = 1;
                    ifamily = 9;

                    if ((table = ControlsBLL.GetCurrencies(Convert.ToInt32(Session["Company"]), ifather, ifamily)) == null)
                    {
                        LabelTool.ShowLabel(errorLabel, "Error al conseguir las denominaciones", System.Drawing.Color.DarkRed);
                    }
                    else
                    {
                        if (table.Rows.Count > 0)
                        {
                            if (table.Rows[0][0] != DBNull.Value)
                            {
                                DropTool.FillDrop(ddlmoneda, table, "Descripcion", "Numero");
                                ddlmoneda.SelectedValue = "109";

                            }
                        }
                    }

                    break;


                case "forpago":

                    DropTool.FillDrop(ddlforpago, CreatePayTable(), "Descripcion", "Numero");

                    break;

                case "metodopago":

                    temp_table = ControlsBLL.ObtenMetodoPagoEmpresa(Convert.ToInt32(Session["Company"]));
                    DropTool.FillDrop(ddlmetodo, temp_table, "Descripcion", "Numero");

                    break;

                case "IVA":
                    ifather = 31;
                    ifamily = 3;

                    if ((table = ControlsBLL.GetIVA(Convert.ToInt32(Session["Company"]), ifather, ifamily)) == null)
                    {
                        LabelTool.ShowLabel(errorLabel, "Error al conseguir los tipos de IVA", System.Drawing.Color.DarkRed);
                    }
                    else
                    {
                        if (table.Rows.Count > 0)
                        {
                            if (table.Rows[0][0] != DBNull.Value)
                            {
                                DropTool.FillDropSO(ddlIva, table, "Descripcion", "Numero");
                                ddlIva.SelectedValue = "36";
                            }
                        }
                    }

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


        private int GetCustomerExisting()
        {
            string sname = "";

            DataTable CustomerTable = new DataTable();


            if (ipersona.sPersonalidad_Juridica == "F")
                sname = nametxt.Text.Trim() + " " + txtpaterno.Text.Trim();
            else
                sname = ipersona.sNombre;

            CustomerTable = ControlsBLL.GetCustomerExistingFacturaLibre(Convert.ToInt32(Session["Company"]), ipersona.sNombre_Completo);

            if (CustomerTable.Rows.Count > 0)
                return Convert.ToInt32(CustomerTable.Rows[0][0]);
            else
                return 0;

        }



        private bool validate_length(TextBox txt, int ilenght, string smessage)
        {
            if (txt.Text.Length != ilenght)
            {
                lblerrorvalidacion.Visible = true;
                lblerrorvalidacion.Text = smessage;
                lblerrorvalidacion.ForeColor = System.Drawing.Color.DarkRed;

                return false;
            }

            return true;
        }


        private void LimpiaDatosCliente()
        {
            customernamelbl.Text = "";
            rfclbl.Text = "";
            streetlbl.Text = "";
            neighborlbl.Text = "";

        }

        private DataTable create_options_table()
        {
            DataTable TablaPartidas = new DataTable();

            TablaPartidas.Columns.Add("Numero", typeof(int));
            TablaPartidas.Columns.Add("Cantidad", typeof(decimal));
            TablaPartidas.Columns.Add("TipoProducto", typeof(int));
            TablaPartidas.Columns.Add("Producto", typeof(int));
            TablaPartidas.Columns.Add("TipoConcepto", typeof(int));
            TablaPartidas.Columns.Add("Concepto", typeof(int));
            TablaPartidas.Columns.Add("Clas_UM", typeof(int));
            TablaPartidas.Columns.Add("Familia", typeof(int));
            TablaPartidas.Columns.Add("Descripcion", typeof(string));
            TablaPartidas.Columns.Add("Precio", typeof(decimal));
            TablaPartidas.Columns.Add("Unidad", typeof(string));
            TablaPartidas.Columns.Add("Total", typeof(decimal));
            TablaPartidas.Columns.Add("NumeroProducto", typeof(string));
            TablaPartidas.Columns.Add("Caracteristicas", typeof(string));
            TablaPartidas.Columns.Add("PorcentajeIva", typeof(string));
            TablaPartidas.Columns.Add("IVAParametrizado");

            return TablaPartidas;
        }


        protected bool ActualizaDatoPagoCliente()
        {
            try
            {

                asignaValoresPago();
                ipersona.cliente.ModificaDatosPagoCliente(INTEGRA.Global.connectionString);

            }
            catch (Exception sqlEx)
            {
                return false;
            }

            return true;

        }

        protected void asignaValoresPago()
        {
            int idClient = Convert.ToInt32(iClientNumber.Value);
            ipersona.cliente.iNumero_Persona = idClient;
            ipersona.cliente.iClas_Dato_Pago = Convert.ToInt32(ddlmetodo.SelectedValue);

            if (TxtTC.Text.Trim() != "")
            {
                ipersona.cliente.iDato_Pago = Convert.ToInt32(TxtTC.Text.Trim());
            }
            else
            {
                ipersona.cliente.iDato_Pago = null;
            }
        }


        private int get_clas_country()
        {
            DataTable temp_table = new DataTable();
            int ifather = -1;
            int ifamily = 9;
            if ((temp_table = ControlsBLL.GetCountry(ipersona.cliente.iEmpresa, ifather, ifamily)) == null)
            {
                LabelTool.ShowLabel(lblerrorvalidacion, "Error al Conseguir el País", System.Drawing.Color.DarkRed);

                return -2;
            }
            else
            {
                if (temp_table.Rows.Count > 0)
                {
                    int index = 0;

                    foreach (DataRow row in temp_table.Rows)
                    {
                        if (row["Descripcion"].ToString() == ddlpais.SelectedItem.ToString())
                        {
                            return Convert.ToInt32(temp_table.Rows[index][0]);
                        }

                        index++;
                    }
                }
            }

            return -2;
        }
        private void PrepareSale()
        {

            newpersonrdbt.SelectedIndex = 0;
            namelbl.Text = "Razón social";
            sdatofaltante = "";
            fathernameval.Visible = false;
            fathernamelbl.Visible = false;
            txtpaterno.Visible = false;
            maternoval.Visible = false;
            maternolbl.Visible = false;
            txtmaterno.Visible = false;
            dCambio = 0;
            dImporteCambio = 0;
            AsignacionTabindex();
            LimpiarTexBox();
            keycontainer.Value = "";
            iClientNumber.Value = Convert.ToString(-1);
            GetCustomerData();
            GetAddressData();

            FillDoctoControls();
            GetTable();
            colapsiblepanel();
            bloqueoControles(true);
            customertxt.Focus();
            ddlmetodo.SelectedIndex = 1;
            lnkModificaCliente.Visible = false;

            Session["cliente"] = ipersona;
        }

        private void colapsiblepanel()
        {
            int aux = 1;

            CollapsiblePanelExtenderPartidas.ExpandedSize = 150;

            if (CollapsiblePanelExtender1.ClientState == "true")
            {
                CollapsiblePanelExtenderPartidas.ExpandedSize = CollapsiblePanelExtenderPartidas.ExpandedSize + 100;
                aux = aux + 1;
            }

            if (CollapsiblePanelExtender2.ClientState == "true")
            {
                CollapsiblePanelExtenderPartidas.ExpandedSize = CollapsiblePanelExtenderPartidas.ExpandedSize + 110;
                aux = aux + 1;
            }

            ScrollProducts.Attributes.Add("class", "GridScrollInvoice" + aux);

        }

        private void LimpiarTexBox()
        {
            errorLabel.Text = "";
            TxtTC.Text = "";
            txtObservaciones.Text = "";
            lblSubtotal.Text = "";
            lblIva.Text = "";
            lblTotal.Text = "";
            txtNumReferencia.Text = "";
        }

        private void GetTable()
        {
            FacturaLibreController Invoice = new FacturaLibreController();
            TablaPartidas = create_options_table();
            DataRow drnew;

            for (int i = 0; i < 1; i++)
            {
                drnew = TablaPartidas.NewRow();

                drnew["Numero"] = 0;
                drnew["Cantidad"] = DBNull.Value;
                drnew["TipoProducto"] = 207;
                drnew["Producto"] = 208;
                drnew["TipoConcepto"] = 209;
                drnew["Concepto"] = -105;
                drnew["Clas_UM"] = 0;
                drnew["Familia"] = 1;
                drnew["Descripcion"] = "";
                drnew["Precio"] = DBNull.Value;
                drnew["Unidad"] = "";
                drnew["Total"] = DBNull.Value;
                drnew["NumeroProducto"] = "";
                drnew["Caracteristicas"] = "";
                drnew["PorcentajeIva"] = (Convert.ToDecimal(ddlIva.SelectedItem.ToString()) / 100).ToString();
                drnew["IVAParametrizado"] = "NO";

                TablaPartidas.Rows.Add(drnew);

            }
            GridViewTool.Bind(TablaPartidas, invoiceGridView);

            foreach (GridViewRow row in invoiceGridView.Rows)
            {
                DropDownList dropunidad = (DropDownList)row.FindControl("ddlunidad");
                dropunidad.Visible = true;

                DataTable TablaUnidades = new DataTable();
                DropTool.ClearDrop(dropunidad);
                TablaUnidades = Invoice.ObtenUnidadesDeMedida(Convert.ToInt32(Session["Company"]));
                DropTool.FillDrop(dropunidad, TablaUnidades, "Descripcion", "Numero");
                dropunidad.SelectedValue = TablaPartidas.Rows[row.RowIndex][6].ToString();
            }
        }

        private void GetCustomerData()
        {
            int idClient = Convert.ToInt32(iClientNumber.Value);
            CustomerBLL Customer = new CustomerBLL();
            DataTable CustomerTable;

            using (CustomerTable = Customer.GetPersonData(Convert.ToInt32(Session["Company"]), idClient))
            {
                if (CustomerTable != null && CustomerTable.Rows.Count > 0)
                {
                    SetCustomerData(CustomerTable);
                }
                else
                {
                    LimpiaDatosCliente();
                }
            }
        }


        private DataTable DatosCorreoCliente()
        {
            SaleBLL DatosCorreo = new SaleBLL();
            int idClient = Convert.ToInt32(iClientNumber.Value);
            DatosCorreo.Customer = idClient;

            return DatosCorreo.GetDatosCorreoCliente();
        }

        private void PrintSaleReport()
        {

            DataTable GeneralSaleTable;
            DetalleTable = generatablapartidas();

            using (GeneralSaleTable = CreateReportSaleTable())
            {
                FillGeneralSaleTable(ref GeneralSaleTable);
                string empresa = Convert.ToString(Session["Company"]);
                ReportDocument document = new ReportDocument();
                document = Reports.GetInvoiceReport(empresa, DetalleTable, GeneralSaleTable);
                document.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                document.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                //string sfolderName = "\\\\VMS00001392-2\\DocumentosElectronicos\\Temp\\DocumentosVenta";
                // string sfolderName = HttpContext.Current.Server.MapPath("/DocumentosVenta");
                string sfolderName = WebConfigurationManager.AppSettings["sfolderNameReportes"].ToString();
                string sfecha = Convert.ToString(DateTime.Now.Month + "-" + DateTime.Now.Year);
                string suser = Convert.ToString(Session["Person"]);

                spathString = System.IO.Path.Combine(sfolderName, INTEGRA.Global.DataBase, empresa, sfecha, suser);

                System.IO.Directory.CreateDirectory(spathString);

                objDiskOpt.DiskFileName = spathString + "/COTIZACIÓN.pdf";

                string rutadoc = "../DocumentosElectronicos/Temp/DocumentosVenta" + "/" + INTEGRA.Global.DataBase + "/" + empresa + "/" + sfecha + "/" + suser + "/" + "COTIZACIÓN.pdf";

                Session["Rutaelectronico"] = rutadoc;
                document.ExportOptions.DestinationOptions = objDiskOpt;
                document.Export();
                document.Close();
                document.Dispose();

                salelbl.Text = "Venta exitosa, ¿desea realizar el pago?";
                errorLabel.Text = "";
                bloqueoControles(false);
                salemsgmdl.Show();



            }
        }


        private void PrintSaleReportPago()
        {

            DataTable GeneralSaleTable;
            DetalleTable = generatablapartidas();

            using (GeneralSaleTable = CreateReportSaleTable())
            {
                FillGeneralSaleTable(ref GeneralSaleTable);
                string empresa = Convert.ToString(Session["Company"]);
                ReportDocument document = new ReportDocument();
                document = Reports.GetInvoiceReport(empresa, DetalleTable, GeneralSaleTable);
                document.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                document.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                // string sfolderName = "\\\\VMS00001392-2\\DocumentosElectronicos\\Temp\\DocumentosVenta";
                string sfolderName = WebConfigurationManager.AppSettings["sfolderNameReportes"].ToString();
                string sfecha = Convert.ToString(DateTime.Now.Month + "-" + DateTime.Now.Year);
                string suser = Convert.ToString(Session["Person"]);
                spathString = System.IO.Path.Combine(sfolderName, INTEGRA.Global.DataBase, empresa, sfecha, suser);

                System.IO.Directory.CreateDirectory(spathString);

                objDiskOpt.DiskFileName = spathString + "/COTIZACIÓN.pdf";

                string rutadoc = "../DocumentosElectronicos/Temp/DocumentosVenta" + "/" + INTEGRA.Global.DataBase + "/" + empresa + "/" + sfecha + "/" + suser + "/" + "COTIZACION.pdf";

                //StreamWriter sw = new StreamWriter(@"E:\DocumentosElectronicos\myRuta.txt");
                //sw.WriteLine("asigna ruta");
                //sw.WriteLine(rutadoc);
                //sw.Close();
                Session["Rutaelectronico"] = rutadoc;
                document.ExportOptions.DestinationOptions = objDiskOpt;
                document.Export();
                document.Close();
                document.Dispose();

                LabelTool.ShowLabel(lblAvisopago, "El pago se ha realizado satisfactoriamente", System.Drawing.Color.DarkRed);
                errorLabel.Text = "";
                bloqueoControles(false);
                mdlPagoAviso.Show();


            }
        }

        private void bloqueoControles(bool bloquear)
        {
            invoiceGridView.Enabled = bloquear;
            LnkGenerar.Visible = bloquear;
        }

        private void PrintSaleReportDescarga()
        {
            string sNameReporte = string.Empty;
            DataTable GeneralSaleTable;
            DataTable DetalleTable = generatablapartidas();

            using (GeneralSaleTable = CreateReportSaleTable())
            {
                FillGeneralSaleTable(ref GeneralSaleTable);
            }

            sNameReporte = Reports.gfGetReportName(ddldocto.SelectedItem.ToString(), txtfolio.Text);
            (Reports.GetInvoiceReport(Convert.ToString(Session["Company"]), DetalleTable, GeneralSaleTable)).ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, this.Response, true, sNameReporte);
        }


        private void ImprimeNotaVenta()
        {
            string sNameReporte = string.Empty;
            DataTable GeneralSaleTable;
            DataTable DetalleTable = generatablapartidas();

            using (GeneralSaleTable = CreateReportSaleTable())
            {
                FillGeneralSaleTable(ref GeneralSaleTable);

                ReportDocument document = new ReportDocument();
                document = Reports.GetInvoiceReport(Convert.ToString(Session["Company"]), DetalleTable, GeneralSaleTable);
                document.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                document.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                //string sfolderName = "\\\\VMS00001392-2\\DocumentosElectronicos\\Temp\\DocumentosVenta";
                //string sfolderName = @"C:/Prueba/Temp/DocumentosVenta";
                string sfolderName = WebConfigurationManager.AppSettings["sfolderNameReportes"].ToString();

                string sfecha = Convert.ToString(DateTime.Now.Month + "-" + DateTime.Now.Year);
                string suser = Session["Person"].ToString();
                string spathString = System.IO.Path.Combine(sfolderName, INTEGRA.Global.DataBase, Session["Company"].ToString(), sfecha, suser);

                System.IO.Directory.CreateDirectory(spathString);

                objDiskOpt.DiskFileName = spathString + "/COTIZACION.pdf";
                string rutadoc = "../DocumentosElectronicos/Temp/DocumentosVenta" + "/" + INTEGRA.Global.DataBase + "/" + Session["Company"] + "/" + sfecha + "/" + suser + "/" + "COTIZACION.pdf";

                Session["Rutaelectronico"] = rutadoc;
                document.ExportOptions.DestinationOptions = objDiskOpt;
                document.Export();
                document.Close();
                document.Dispose();

                errorLabel.Text = "";
                PrintSaleReportDescarga();
            }

        }
        private DataTable CreateReportSaleTable()
        {
            DataTable table;


            using (table = new DataTable())
            {
                table.Columns.Add("Cliente");
                table.Columns.Add("ColoniaCliente");
                table.Columns.Add("DelCliente");
                table.Columns.Add("EstadoCliente");
                table.Columns.Add("RFC");
                table.Columns.Add("SubTotal");
                table.Columns.Add("IVA");
                table.Columns.Add("Total");
                table.Columns.Add("DocTo");
                table.Columns.Add("DocSerie");
                table.Columns.Add("Denominacion");
                table.Columns.Add("Factura");
                table.Columns.Add("Importe");
                table.Columns.Add("Estatus");
                table.Columns.Add("Fecha");
                table.Columns.Add("NumeroTicket");

                //Datos empresa
                table.Columns.Add("RFCEmpresa");
                table.Columns.Add("NombreEmpresa");
                table.Columns.Add("CalleyNumero");
                table.Columns.Add("Colonia");
                table.Columns.Add("Delegacion");
                table.Columns.Add("Codigo");
                table.Columns.Add("Telefono");
                table.Columns.Add("MetodoPago");
                table.Columns.Add("Direccion");

            }

            return table;
        }

        private void FillGeneralSaleTable(ref DataTable GeneralSaleTable)
        {
            toolsBLL importeletra = new toolsBLL();
            DataTable Importletra, AddressTable;
            DataTable dtEstatus = new DataTable();
            FacturaLibreController FacturaLibre = new FacturaLibreController();
            dtEstatus = FacturaLibre.ObtieneEstatusVenta(Convert.ToInt32(Session["Company"]), Convert.ToInt32(iIDFactura.Value));
            SaleBLL Sale = new SaleBLL();
            DataRow SaleRow = GeneralSaleTable.NewRow();
            int idClient = Convert.ToInt32(iClientNumber.Value);
            SaleRow["Cliente"] = customernamelbl.Text;

            using (AddressTable = PersonBLL.ObtenDomicilioPersonaPorTipoDomicilio(idClient, 13))
            {
                if (AddressTable != null && AddressTable.Rows.Count > 0)
                {
                    SaleRow["ColoniaCliente"] = AddressTable.Rows[0][3].ToString();
                    SaleRow["DelCliente"] = AddressTable.Rows[0][4].ToString();
                    SaleRow["EstadoCliente"] = AddressTable.Rows[0][5].ToString();
                    SaleRow["Direccion"] = AddressTable.Rows[0][3].ToString() + ' ' + AddressTable.Rows[0][4].ToString() + ' ' + AddressTable.Rows[0][5].ToString();
                }
            }

            SaleRow["RFC"] = rfclbl.Text;
            SaleRow["Subtotal"] = lblSubtotal.Text;
            SaleRow["IVA"] = lblIva.Text;
            SaleRow["Total"] = lblTotal.Text;
            SaleRow["DocTo"] = ddldocto.SelectedItem.ToString();
            SaleRow["DocSerie"] = ddlserie.SelectedItem.ToString();
            SaleRow["Denominacion"] = ddlmoneda.SelectedItem.ToString();
            SaleRow["Factura"] = txtfolio.Text;
            Importletra = importeletra.import_to_string(Convert.ToInt32(Session["Company"]), Convert.ToDecimal(lblTotal.Text), ddlmoneda.SelectedItem.ToString(), 0);
            SaleRow["Importe"] = Convert.ToString(Importletra.Rows[0][0]);
            if (dtEstatus != null && dtEstatus.Rows.Count > 0)
            {
                SaleRow["Estatus"] = dtEstatus.Rows[0][0].ToString();
            }
            else
            {
                SaleRow["Estatus"] = "";
            }

            SaleRow["Fecha"] = DateTime.Now.ToString();
            SaleRow["NumeroTicket"] = sNumeroTicket;
            SaleRow["MetodoPago"] = ddlmetodo.SelectedItem.ToString();

            DataTable TablaEmpresa = new DataTable();
            TablaEmpresa = DatosEmpresa();
            //Datos Empresa
            SaleRow["RFCEmpresa"] = Convert.ToString(TablaEmpresa.Rows[0][0]);
            SaleRow["NombreEmpresa"] = Convert.ToString(TablaEmpresa.Rows[0][1]);
            SaleRow["CalleyNumero"] = Convert.ToString(TablaEmpresa.Rows[0][2]);
            SaleRow["Colonia"] = Convert.ToString(TablaEmpresa.Rows[0][3]);
            SaleRow["Delegacion"] = Convert.ToString(TablaEmpresa.Rows[0][4]);
            SaleRow["Codigo"] = Convert.ToString(TablaEmpresa.Rows[0][6]);
            SaleRow["Telefono"] = Convert.ToString(TablaEmpresa.Rows[0][8]);

            GeneralSaleTable.Rows.Add(SaleRow);
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
            int idClient = Convert.ToInt32(iClientNumber.Value);
            SaleBLL Sale = new SaleBLL();

            Sale.Company = Convert.ToInt32(Session["Company"]);
            Sale.Number = Number;
            Sale.Customer = idClient;
            Sale.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            Sale.Documento = Convert.ToInt32(ddldocto.SelectedValue);
            Sale.Serie = Convert.ToInt32(ddlserie.SelectedValue);
            Sale.NumeroDocto = Convert.ToInt32(txtfolio.Text);
            Sale.Reference = txtNumReferencia.Text.Trim();
            Sale.CurrencyInvoice = Convert.ToInt32(ddlmoneda.SelectedValue);
            Sale.TypeChange = dExchange;
            Sale.ConceptNumber = iFacturaConcepto;
            Sale.User = Convert.ToInt32(Session["Person"]);
            Sale.Credit = Convert.ToInt32(ddlforpago.SelectedValue);
            Sale.Observations = txtObservaciones.Text;
            Sale.Notacargo = 0;
            Sale.CPayData = Convert.ToInt32(ddlmetodo.SelectedValue);

            if (Session["Office"] != null)
                Sale.Clas_Sucursal = Convert.ToInt32(Session["Office"]);
            else
                Sale.Clas_Sucursal = 0;

            if (TxtTC.Text.Trim() != "")
            {
                string NumCtaPago = TxtTC.Text;
                NumCtaPago = NumCtaPago.PadLeft(4, '0');
                Sale.SPayData = TxtTC.Text.Trim();
            }
            else
            {
                Sale.SPayData = "No identificado";
            }


            if (txtCondicionPago.Text.Trim() != "")
            {
                Sale.sCondicionPago = txtCondicionPago.Text.Trim();
            }
            else
            {
                Sale.sCondicionPago = "";
            }


            return Sale.SpFacturaLibre(Moviment);
        }
        private bool SaveDetail(int Moviment, int Index, int iNumeroFactLibre)
        {
            SaleDetailBLL SaleDetail = new SaleDetailBLL();

            SaleDetail.Company = Convert.ToInt32(Session["Company"]);
            SaleDetail.SaleNumber = iNumeroFactLibre;
            SaleDetail.Number = 0;
            Label lbltipoproducto = (Label)invoiceGridView.Rows[Index].FindControl("lbltipo_producto");
            SaleDetail.TProduct = Convert.ToInt32(lbltipoproducto.Text);
            Label lblproducto = (Label)invoiceGridView.Rows[Index].FindControl("lblproducto");
            SaleDetail.Product = Convert.ToInt32(lblproducto.Text);
            Label lbltipoconcepto = (Label)invoiceGridView.Rows[Index].FindControl("lbltipo_concepto");
            SaleDetail.TConcept = Convert.ToInt32(lbltipoconcepto.Text);
            Label lblconcepto = (Label)invoiceGridView.Rows[Index].FindControl("lblconcepto");
            SaleDetail.Concept = Convert.ToInt32(lblconcepto.Text);
            TextBox productotxt = (TextBox)invoiceGridView.Rows[Index].FindControl("productotxt");
            Label lblCaracteristicas = (Label)invoiceGridView.Rows[Index].FindControl("lblCaracteristicas");

            string caracteristicas = string.Empty;
            if (Convert.ToInt32(lblconcepto.Text) == -105)
            {
                if (productotxt.Text.Trim() == "")
                {
                    caracteristicas = Convert.ToString(lblCaracteristicas.Text.Trim());
                }
                else
                {
                    caracteristicas = Convert.ToString(productotxt.Text.Trim()) + "\n" + Convert.ToString(lblCaracteristicas.Text.Trim());
                }
                if (caracteristicas.Length > 4000)
                {
                    caracteristicas = caracteristicas.Substring(0, 4000);
                }

                SaleDetail.Characteristics = caracteristicas;

            }
            else
            {

                caracteristicas = Convert.ToString(lblCaracteristicas.Text);
                if (caracteristicas.Length > 4000)
                {
                    caracteristicas = caracteristicas.Substring(0, 4000);
                }

                SaleDetail.Characteristics = caracteristicas;

            }
            Label clas_unidad = (Label)invoiceGridView.Rows[Index].FindControl("lblclas_um");
            SaleDetail.MeasuringUnit = Convert.ToInt32(clas_unidad.Text);

            TextBox cantidadtxt = (TextBox)invoiceGridView.Rows[Index].FindControl("cantidadtxt");
            SaleDetail.Quantity = Convert.ToDecimal(cantidadtxt.Text);
            TextBox precio = (TextBox)invoiceGridView.Rows[Index].FindControl("preciotxt");
            SaleDetail.Price = Convert.ToDecimal(precio.Text);
            double cant = Convert.ToDouble(cantidadtxt.Text);
            double prec = Convert.ToDouble(precio.Text);
            double subtotal = cant * prec;

            string porcentageIa = "0." + ddlIva.SelectedItem.ToString();
            double porcentageIva = Convert.ToDouble(porcentageIa);

            double iva = subtotal * porcentageIva;
            SaleDetail.IVA = iva;//iva por partida
            SaleDetail.ClasIVA = Convert.ToInt32(ddlIva.SelectedValue);
            Label lblfamilia = (Label)invoiceGridView.Rows[Index].FindControl("lblFamilia");
            SaleDetail.MeasuringUnitFam = Convert.ToInt32(lblfamilia.Text);
            SaleDetail.DeliveryDate = DateTime.Today;
            SaleDetail.NotaCargo = null;
            SaleDetail.TotalCargo = null;

            return SaleDetail.SaveDetailFacturaLibre(Moviment);
        }

        private int sp_Factura_Contabilidad_CC(int Number)
        {
            SaleBLL Sale = new SaleBLL();

            Sale.Company = Convert.ToInt32(Session["Company"]);
            Sale.Number = Number;
            Sale.User = Convert.ToInt32(Session["Person"]);

            return Sale.spFacturaContabilidadCC();
        }

        public void AbreModalPago_Click(object sender, EventArgs e)
        {
            SaleBLL sale = new SaleBLL();
            FacturaLibreController Factura = new FacturaLibreController();

            DataTable TablaEmpresa = new DataTable();
            salemsgmdl.Hide();
            LabelTool.HideLabel(lblCmessage);
            grvCollet.Enabled = true;
            LbtnPay.Enabled = true;

            if ((TablaEmpresa = DatosEmpresa()) != null)    //Obtiene datos de la empresa
            {
                if (TablaEmpresa.Rows.Count > 0)
                {

                    lblEmpresapago.Text = Convert.ToString(TablaEmpresa.Rows[0][1]);
                    lblCallepago.Text = Convert.ToString(TablaEmpresa.Rows[0][2]);
                    lblColoniapago.Text = Convert.ToString(TablaEmpresa.Rows[0][3]);
                    lblDelegacionpago.Text = Convert.ToString(TablaEmpresa.Rows[0][4]);
                    lblCppago.Text = "C.P." + " " + Convert.ToString(TablaEmpresa.Rows[0][6]) + "  " + "Tel." + Convert.ToString(TablaEmpresa.Rows[0][8]);

                }

            }

            DataTable DtNumeroCotizacion = Factura.ObtenNumeroCotizacion(Convert.ToInt32(Session["Company"]), Convert.ToInt32(iIDFactura.Value));
            int iNumeroCotizacion = Convert.ToInt32(DtNumeroCotizacion.Rows[0][0]);
            tempPayForms = sale.GetPayForms(Convert.ToInt32(Session["Company"]), iNumeroCotizacion);

            lblSbttaltkt.Text = lblSubtotal.Text;
            lblIvatktd.Text = lblIva.Text;
            lblTotaltktd.Text = lblTotal.Text;

            txtPayTotal.Text = "$" + lblTotal.Text;
            txtPagoImporte.Text = "$0.00";
            txtCambiopago.Text = "$0.00";
            txtRestan.Text = "$" + lblTotal.Text;
            lblTotalpagotkt.Text = "$0.00";
            lblChangetkt.Text = "$0.00";
            doctopaydrp.Text = Convert.ToString(ddldocto.SelectedItem.Text);
            seriepaydrp.Text = Convert.ToString(ddlserie.SelectedItem.Text);
            numdoctopaytxt.Text = Convert.ToString(txtfolio.Text);
            txtClientepago.Text = Convert.ToString(customernamelbl.Text);

            txtMonedapago.Text = Convert.ToString(ddlmoneda.SelectedItem.Text);
            GridViewTool.Bind(tempPayForms, grvCollet);
            GridViewTool.Bind(DetalleTable, productspayGvw);//Llena el grid del ticket
            GridViewTool.Bind(null, grvDesglose);


            if (tempPayForms.Rows.Count > 0)
            {
                int index = 0;

                foreach (DataRow row in tempPayForms.Rows)
                {
                    if (Convert.ToDecimal(row["Numero"]) == -2)
                    {
                        dTipoCambio = Convert.ToDecimal(tempPayForms.Rows[index][2]);
                    }

                    index++;
                }
            }


            mdlPago.Show();//Modal de Formas de pago
        }

        private bool ValImportText(TextBox importetxt, int ichange, decimal dImporteResta, decimal dcnt)
        {
            InvoiceBLL Invoice = new InvoiceBLL();

            if (importetxt.Text != "")
            {
                if (validaImporte(importetxt) == 1)
                {

                    dImporteResta = Convert.ToDecimal(txtPayTotal.Text.Replace('$', ' ')) - Convert.ToDecimal(txtPagoImporte.Text.Replace('$', ' '));
                    decimal resultado = (Convert.ToDecimal(importetxt.Text.Replace('$', ' ')) * dExchange);

                    if (Convert.ToInt32(ddlmoneda.SelectedValue) == 110)
                    {

                        resultado = resultado / dTipoCambio;
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
                            LabelTool.ShowLabel(lblCmessage, "El importe de los conceptos que no dan cambio excede el importe total de la venta", System.Drawing.Color.DarkRed);
                            return false;
                        }

                    }
                    else
                    {
                        importetxt.Text = "$" + importetxt.Text;
                        LabelTool.HideLabel(lblCmessage);

                    }


                }

                else
                {
                    LabelTool.ShowLabel(lblCmessage, "Introduzca un importe válido", System.Drawing.Color.DarkRed);
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

        private bool ValidacionImporteCambio(TextBox importetxt, decimal exchange, decimal dcnt)
        {
            InvoiceBLL Invoice = new InvoiceBLL();

            if (importetxt.Text != "")
            {
                if (validaImporte(importetxt) == 1)
                {

                    decimal resultado = (Convert.ToDecimal(importetxt.Text.Replace('$', ' ')) * exchange);

                    if (Convert.ToInt32(ddlmoneda.SelectedValue) == 110)
                    {

                        resultado = resultado / dTipoCambio;
                    }

                    if (resultado > dCambio)
                    {

                        importetxt.Text = "$0.00";
                        LabelTool.ShowLabel(lblMensajeCambio, "El cambio es incorrecto", System.Drawing.Color.DarkRed);
                        return false;
                    }
                    else
                    {
                        importetxt.Text = "$" + importetxt.Text;
                        LabelTool.HideLabel(lblCmessage);

                    }


                }

                else
                {
                    LabelTool.ShowLabel(lblCmessage, "Introduzca un Importe Válido", System.Drawing.Color.DarkRed);
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
                    LabelTool.HideLabel(lblCmessage);
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

        // Consulta si tengo que dar cambio
        public bool validaExisteCambio()
        {
            if (dCambio > 0)
            {
                DataTable DataConcept = sp_Cuentas_Cobrar_TraeConcepto_Tienda_C();
                GridViewTool.Bind(DataConcept, grvCambio);
                MontoCambiotxt.Text = txtCambiopago.Text;
                mdlCambio.Show();
            }
            else
            {
                if (GuardaDetallePago())
                {
                    GuardaEncabezadoPago();
                }
            }

            return true;
        }

        public bool GuardaDetallePago()
        {
            int itype;
            bool bresult = true;

            foreach (GridViewRow row in grvCollet.Rows)
            {
                Label lblchange = (Label)grvCollet.Rows[row.RowIndex].FindControl("lblTipocambio");
                decimal exchange = Convert.ToDecimal(lblchange.Text);
                TextBox ingresotxt = (TextBox)grvCollet.Rows[row.RowIndex].FindControl("txtIngreso");
                decimal dingreso = (Convert.ToDecimal(ingresotxt.Text.Replace('$', ' ')) * exchange);

                if (Convert.ToInt32(ddlmoneda.SelectedValue) == 110)
                {
                    dingreso = dingreso / dTipoCambio;
                }

                if (dingreso > 0)
                {
                    bresult = bresult && CreatePay(row);//inserta detalle pago
                }
            }

            return bresult;
        }

        public bool GuardaEncabezadoPago()
        {
            int itype;
            bool bresult = true;
            SaleBLL Sale = new SaleBLL();

            if (PayInvoiceProcess())//inserta encabezado del pago
            {
                if (ddldocto.SelectedValue == "-59")
                {
                    try
                    {
                        //obtener folio_fiscal y update folio_fiscal
                        Sale.GenerarFolioFiscalPagar(Convert.ToInt32(iIDFactura.Value), int.Parse(Session["Company"].ToString()));
                    }
                    catch
                    {
                        mdlPago.Hide();
                        mdlPagoAviso.Show();
                    }
                }

                mdlPago.Hide();
                mdlPagoAviso.Show();
            }

            else
            {
                itype = 4;
                PayInvoice(itype, null);
                LabelTool.ShowLabel(lblCmessage, "Error al realizar el pago, intente nuevamente", System.Drawing.Color.DarkRed);
                bresult = false;
            }
            return bresult;
        }

        private DataTable sp_Cuentas_Cobrar_TraeConcepto_Tienda_C()
        {
            SaleBLL TableConcept = new SaleBLL();

            TableConcept.Company = Convert.ToInt32(Session["Company"]);
            TableConcept.Number = Convert.ToInt32(iIDFactura.Value);

            return TableConcept.GetTraeConcepto_Tienda_C();
        }

        private bool bValidaDatosPago()
        {

            if (dImportePago > 0)//valida si hay algo por pagar
            {
                //valida el llenado de los campos correctos
                foreach (GridViewRow row in grvCollet.Rows)
                {
                    TextBox referenciatxt = (TextBox)row.FindControl("txtReferencia");
                    TextBox importetxt = (TextBox)row.FindControl("txtIngreso");

                    if (Convert.ToDecimal(importetxt.Text.Replace('$', ' ')) > 0)
                    {
                        if (referenciatxt != null && referenciatxt.Text == "")
                        {
                            LabelTool.ShowLabel(lblCmessage, "Debe de capturar la referencia del pago de cada uno de los conceptos", System.Drawing.Color.DarkRed);
                            return false;
                        }

                    }

                }

            }

            else
            {
                LabelTool.ShowLabel(lblCmessage, "No se ha capturado algún pago", System.Drawing.Color.DarkRed);
                return false;
            }


            return bValidaTransaccionPago();

        }

        protected bool bvalidacambio()
        {

            decimal dingreso = 0, dingresototal = 0;
            DataTable ValidaTable;
            string ConceptoContable;
            string sMsjConceptoSinTrans = "";
            InvoiceBLL Invoice = new InvoiceBLL();

            foreach (GridViewRow row in grvCambio.Rows)
            {
                Label Numberlbl = (Label)row.FindControl("Numberlbl");
                Label lblchange = (Label)grvCambio.Rows[row.RowIndex].FindControl("lblTypechange");
                decimal exchange = Convert.ToDecimal(lblchange.Text);
                TextBox ingresotxt = (TextBox)grvCambio.Rows[row.RowIndex].FindControl("txtIngreso");
                dingreso = Convert.ToDecimal(ingresotxt.Text.Replace('$', ' ')) * exchange;
                int a = row.RowIndex;


                if (dingreso != 0)
                {
                    if (Convert.ToInt32(ddlmoneda.SelectedValue) == 110)
                    {
                        dingreso = dingreso / dTipoCambio;
                    }

                    dingresototal += dingreso;

                    ValidaTable = Invoice.ValidaConceptoContable(Convert.ToInt32(Session["Company"]), Convert.ToInt32(iIDFactura.Value), Convert.ToInt32(Numberlbl.Text));

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
                mdlTransaccionCambio.Show();
                return false;
            }
            else
            {
                if (dingresototal == dCambio)
                {
                    if (GuardaDetallePago())
                    {
                        if (bGuardaCambio())
                        {
                            GuardaEncabezadoPago();
                            mdlCambio.Hide();
                            mdlPago.Hide();
                            PrintSaleReportPago();

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

        private bool bValidaTransaccionPago()
        {
            InvoiceBLL Invoice = new InvoiceBLL();
            DataTable ValidaTable;
            string ConceptoContable = "";
            string sMsjTransaccSinDefinir = "";
            decimal dingreso = 0;

            foreach (GridViewRow row in grvCollet.Rows)
            {
                TextBox ingresotxt = (TextBox)grvCollet.Rows[row.RowIndex].FindControl("txtIngreso");
                dingreso = Convert.ToDecimal(ingresotxt.Text.Replace('$', ' '));

                if (dingreso != 0)
                {
                    Label Numberlbl = (Label)row.FindControl("lblNumber");

                    int a = row.RowIndex;

                    ValidaTable = Invoice.ValidaConceptoContable(Convert.ToInt32(Session["Company"]), Convert.ToInt32(iIDFactura.Value), Convert.ToInt32(Numberlbl.Text));

                    if (ValidaTable != null)
                    {
                        ConceptoContable = Convert.ToString(ValidaTable.Rows[0][0]);

                        if (ConceptoContable != "")
                        {
                            sMsjTransaccSinDefinir = sMsjTransaccSinDefinir + "El concepto " + grvCollet.Rows[a].Cells[2].Text;

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

            return validaExisteCambio();
        }

        private bool CreatePay(GridViewRow row)
        {
            int itype = 1;

            if (PayInvoice(itype, row))
            {

                return true;
            }
            else
            {
                itype = 4;

                PayInvoice(itype, row);

                return false;
            }

        }

        private bool PayInvoice(int itype, GridViewRow row)
        {

            InvoiceBLL PayInvoice = new InvoiceBLL();

            PayInvoice.iType = itype;
            PayInvoice.iCompany = Convert.ToInt32(Session["Company"]);
            PayInvoice.iInvoiceNumber = Convert.ToInt32(iIDFactura.Value);
            PayInvoice.iNumberAux = null;
            PayInvoice.dHaber = null;//Egreso

            if (itype == 1)
            {

                Label lbl = (Label)grvCollet.Rows[row.RowIndex].FindControl("lblNumber");
                PayInvoice.iConceptNumber = Convert.ToInt32(lbl.Text);

                Label currlbl = (Label)grvCollet.Rows[row.RowIndex].FindControl("lblCurrency");
                PayInvoice.iCurrency = Convert.ToInt32(currlbl.Text);

                Label exlbl = (Label)grvCollet.Rows[row.RowIndex].FindControl("lblTipocambio");
                PayInvoice.dExchange = Convert.ToDecimal(exlbl.Text);

                TextBox ingresotxt = (TextBox)grvCollet.Rows[row.RowIndex].FindControl("txtIngreso");
                PayInvoice.dIngreso = Convert.ToDecimal(ingresotxt.Text.Replace('$', ' '));

                TextBox refe = (TextBox)grvCollet.Rows[row.RowIndex].FindControl("txtReferencia");
                PayInvoice.sAux = refe.Text;//Referencia

                PayInvoice.Person = Convert.ToInt32(Session["Person"]);
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

                Label lbl = (Label)grvCambio.Rows[row.RowIndex].FindControl("Numberlbl");
                PayInvoice.iConceptNumber = Convert.ToInt32(lbl.Text);

                Label currlbl = (Label)grvCambio.Rows[row.RowIndex].FindControl("lblClasMoneda");
                PayInvoice.iCurrency = Convert.ToInt32(currlbl.Text);

                Label exlbl = (Label)grvCambio.Rows[row.RowIndex].FindControl("lblTypechange");
                PayInvoice.dExchange = Convert.ToDecimal(exlbl.Text);

                TextBox changetxt = (TextBox)grvCambio.Rows[row.RowIndex].FindControl("txtIngreso");
                PayInvoice.dEgreso = Convert.ToDecimal(changetxt.Text.Replace('$', ' '));
                PayInvoice.dIngreso = null;
                PayInvoice.sAux = "";

                PayInvoice.Person = Convert.ToInt32(Session["Person"]);

            }

            return PayInvoice.PayInvoice();

        }

        private bool PayInvoiceProcess()
        {

            InvoiceBLL PayInvoicePss = new InvoiceBLL();

            PayInvoicePss.iCompany = Convert.ToInt32(Session["Company"]);
            PayInvoicePss.iInvoiceNumber = Convert.ToInt32(iIDFactura.Value);

            return PayInvoicePss.PayInvoiceProcess();

        }

        public bool bGuardaCambio()
        {
            bool bresult = true;

            foreach (GridViewRow row in grvCambio.Rows)
            {
                TextBox ingresotxt = (TextBox)grvCambio.Rows[row.RowIndex].FindControl("txtIngreso");
                decimal dingreso = Convert.ToDecimal(ingresotxt.Text.Replace('$', ' '));

                if (dingreso > 0)
                {
                    bresult = bresult && CreateChange(row);

                }
            }
            return bresult;
        }

        private bool CreateChange(GridViewRow row)
        {
            int itype = 5;

            if (PayInvoice(itype, row))
            {

                return true;
            }
            else
            {
                itype = 4;

                PayInvoice(itype, row);

                return false;
            }
        }

        #endregion

        #region AltaClientes

        protected void ShowAddClient_Click(object sender, EventArgs e)
        {
            try
            {
                hdnMovimiento.Value = "Alta";
                Cliente(0);//Cliente nuevo
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref errorLabel);
            }

        }

        protected void Cliente(int iTipo)
        {
            if (iTipo == 0)
            {
                LimpiarControlesAlta();
                llena_controlesalta();
                AddClientMdl.Show();
            }

            else
            {
                LimpiarControlesAlta();
                llena_controlesalta();
                ObtieneDatosCliente();
                AddClientMdl.Show();
            }

        }

        protected void NewPersonChanged(object sender, EventArgs e)
        {

            if (newpersonrdbt.SelectedIndex == 0)
            {
                namelbl.Text = "Razón social";
                fathernameval.Visible = false;
                fathernamelbl.Visible = false;
                txtpaterno.Visible = false;
                maternoval.Visible = false;
                maternolbl.Visible = false;
                txtmaterno.Visible = false;
                bmoral = true;
                spersonalidad = "M";

            }
            else
            {
                namelbl.Text = "Nombre";
                fathernameval.Visible = true;
                fathernamelbl.Visible = true;
                txtpaterno.Visible = true;
                maternoval.Visible = true;
                maternolbl.Visible = true;
                txtmaterno.Visible = true;
                bmoral = false;
                spersonalidad = "F";

            }
            AsignacionTabindex();

        }

        protected void AsignacionTabindex()
        {
            if (newpersonrdbt.SelectedIndex == 0)
            {

                nametxt.TabIndex = 17;
                txtrfc.TabIndex = 18;
                txtcorto.TabIndex = 19;
                txtcorreo.TabIndex = 20;
                txtCalle.TabIndex = 21;
                txtColonia.TabIndex = 22;
                txtCp.TabIndex = 23;
                txtEstado.TabIndex = 24;
                txtDelegacion.TabIndex = 25;
            }
            else
            {
                nametxt.TabIndex = 17;
                txtpaterno.TabIndex = 18;
                txtmaterno.TabIndex = 19;
                txtrfc.TabIndex = 20;
                txtcorto.TabIndex = 21;
                txtcorreo.TabIndex = 22;
                txtCalle.TabIndex = 23;
                txtColonia.TabIndex = 24;
                txtCp.TabIndex = 25;
                txtEstado.TabIndex = 26;
                txtDelegacion.TabIndex = 27;
            }
        }

        private void llenado_ddl(string namecontrol)
        {
            DataTable temp_table = new DataTable();
            Controls cnt = new Controls();
            int ifather;
            int ifamily;

            switch (namecontrol)
            {

                case "pais":
                    ifather = -1;
                    ifamily = 9;
                    temp_table = ControlsBLL.GetCountry(Convert.ToInt32(Session["Company"]), ifather, ifamily);
                    DropTool.ClearDrop(ddlpais);
                    cnt.FillDrp(ddlpais, temp_table, "Descripcion", "numero");

                    break;
                case "formapago":

                    DropTool.ClearDrop(ddlformapago);
                    ddlformapago.Items.Add("Contado");
                    ddlformapago.Items.Add("Crédito");

                    break;

                case "datospago":
                    ifather = -63;
                    ifamily = 3;
                    temp_table = ControlsBLL.GetCustomerDataPay(Convert.ToInt32(Session["Company"]), ifather, ifamily);
                    DropTool.ClearDrop(ddldatospago);
                    cnt.FillDrp(ddldatospago, temp_table, "Descripcion", "numero");

                    break;


            }

            temp_table.Clear();
            temp_table.Dispose();
        }


        protected void datospagoddlChanged(object sender, EventArgs e)
        {

            int Clas_Data_Pago = Convert.ToInt32(ddldatospago.SelectedValue);

            if (Clas_Data_Pago != 0)
            {
                ipersona.cliente.iClas_Dato_Pago = Clas_Data_Pago;
            }

            Session["cliente"] = ipersona;

        }


        protected void paisddlChanged(object sender, EventArgs e)
        {

            int iClas_Pais = Convert.ToInt32(ddlpais.SelectedValue);

            if (iClas_Pais != 0)
            {
                ipersona.domicilio.Clas_Pais = iClas_Pais;
            }

            Session["cliente"] = ipersona;

        }

        protected void formapagoddlChanged(object sender, EventArgs e)
        {

            if (ddlformapago.SelectedValue == "Crédito")
                ipersona.cliente.iForma_Pago = 1;
            else
                ipersona.cliente.iForma_Pago = 0;

            Session["cliente"] = ipersona;

        }

        protected void CancelAddClick(object sender, EventArgs e)
        {
            AddClientMdl.Hide();
        }

        protected void AddClientClick(object sender, EventArgs e)
        {
            try
            {
                if (hdnMovimiento.Value == "Alta")
                {
                    if (validate_client_controls())
                    {
                        make_person_and_client();
                    }
                }
                else
                {
                    if (validate_client_controls())
                    {
                        actualiza_persona_y_cliente();
                    }
                }

            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref errorLabel);
            }


        }

        private bool validate_client_controls()
        {
            if (Server.HtmlDecode(nametxt.Text.Trim()) == "")
            {
                LabelTool.ShowLabel(lblerrorvalidacion, "El Nombre es obligatorio", System.Drawing.Color.DarkRed);
                nametxt.Focus();
                return false;
            }

            if (newpersonrdbt.SelectedIndex == 1)
            {
                if (Server.HtmlDecode(txtpaterno.Text.Trim()) == "")
                {
                    LabelTool.ShowLabel(lblerrorvalidacion, "El apellido paterno es obligatorio", System.Drawing.Color.DarkRed);
                    txtpaterno.Focus();
                    return false;
                }
            }


            if (!RFCValidation())
            {
                LabelTool.ShowLabel(lblerrorvalidacion, "EL RFC es erróneo, introduzca un RFC válido", System.Drawing.Color.DarkRed);
                txtrfc.Focus();
                return false;
            }


            if (Server.HtmlDecode(txtcorto.Text.Trim()) == "")
            {
                LabelTool.ShowLabel(lblerrorvalidacion, "El nombre corto es obligatorio", System.Drawing.Color.DarkRed);
                txtcorto.Focus();
                return false;
            }

            if (Server.HtmlDecode(txtcorreo.Text).Trim() == "" || !eMailValidation())
            {
                LabelTool.ShowLabel(lblerrorvalidacion, "Introduzca una cuenta de correo válida", System.Drawing.Color.DarkRed);
                txtcorreo.Focus();
                return false;
            }



            if (Server.HtmlDecode(txtCalle.Text.Trim()) == "")
            {
                LabelTool.ShowLabel(lblerrorvalidacion, "Calle es Campo Obligatorio", System.Drawing.Color.DarkRed);
                txtCalle.Focus();
                return false;
            }

            if (Server.HtmlDecode(txtColonia.Text.Trim()) == "")
            {
                LabelTool.ShowLabel(lblerrorvalidacion, "Colonia es Campo Obligatorio", System.Drawing.Color.DarkRed);
                txtColonia.Focus();
                return false;
            }

            if (Server.HtmlDecode(txtCp.Text.Trim()) == "")
            {
                LabelTool.ShowLabel(lblerrorvalidacion, "Código Postal es Campo Obligatorio", System.Drawing.Color.DarkRed);
                txtCp.Focus();
                return false;
            }
            if (!validate_length(txtCp, 5, "El Código Postal debera ser de 5 dígitos "))
            {
                txtCp.Focus();
                return false;
            }

            if (Server.HtmlDecode(txtEstado.Text.Trim()) == "")
            {
                LabelTool.ShowLabel(lblerrorvalidacion, "Estado es Campo Obligatorio", System.Drawing.Color.DarkRed);
                txtEstado.Focus();
                return false;
            }

            if (Server.HtmlDecode(txtDelegacion.Text.Trim()) == "")
            {
                LabelTool.ShowLabel(lblerrorvalidacion, "Delegación es Campo Obligatorio", System.Drawing.Color.DarkRed);
                txtDelegacion.Focus();
                return false;
            }


            return true;
        }


        private void make_person_and_client()
        {
            asign_person_values();

            if (GetCustomerExistingTable())
            {

                if (!asign_client_values())
                {
                    LabelTool.ShowLabel(lblerrorvalidacion, "Error al insertar el cliente !!!", System.Drawing.Color.DarkRed);
                }

                //validacion de clientes 
                else if (!ipersona.cliente.ClienteAlta(INTEGRA.Global.connectionString, nametxt.Text.Trim(), txtpaterno.Text.Trim(), txtmaterno.Text.Trim(),
                                             nametxt.Text.Trim(), txtcorto.Text.Trim(), "",
                                             txtcorreo.Text.Trim(), spersonalidad, txtrfc.Text.Trim().ToUpper(), "", ipersona.cliente.iClasificacion_Cliente, ipersona.cliente.iLista_Precio))
                {
                    LabelTool.ShowLabel(lblerrorvalidacion, "Error al insertar el cliente !!!", System.Drawing.Color.DarkRed);

                }
                else
                {
                    ipersona.iNumero = ipersona.cliente.iNumero_Persona;
                    ipersona.domicilio.Numero_Persona = ipersona.iNumero;
                    ipersona.iTipoCliente = 1;
                    asign_addres_values();

                    if (!ipersona.domicilio.InsertAddres(INTEGRA.Global.connectionString, Convert.ToInt32(Session["Company"])))
                    {
                        LabelTool.ShowLabel(lblerrorvalidacion, "Error al Agregar el Domicilio, Intente Nuevamente", System.Drawing.Color.DarkRed);
                    }
                    else
                    {
                        int iClas_Cuenta_Mayor = 0;
                        DataTable dtConta = CustomerBLL.ContabilidadCuentaMayor(Convert.ToInt32(Session["Company"]));
                        if (dtConta.Rows.Count > 0)
                        {
                            if (dtConta.Rows[0][0] != DBNull.Value && dtConta.Rows[0][0].ToString() != "")
                            {
                                iClas_Cuenta_Mayor = Convert.ToInt32(dtConta.Rows[0][0]);
                            }
                            if (iClas_Cuenta_Mayor > 0)
                            {
                                int iClas_Cliente = 0;
                                DataTable CustomerTable = new DataTable();
                                CustomerBLL cliente = new CustomerBLL();
                                CustomerTable = cliente.GetCustomerData(Convert.ToInt32(Session["Company"]), ipersona.iNumero);

                                if (CustomerTable.Rows.Count > 0)
                                {
                                    if (CustomerTable.Rows[0][6].ToString() != "")
                                    {
                                        iClas_Cliente = Convert.ToInt32(CustomerTable.Rows[0][6]);
                                    }
                                }

                                if (iClas_Cliente == 0)
                                {
                                    int iRespuesta = CustomerBLL.AuxContableClienteFacturaLibre(Convert.ToInt32(Session["Company"]), ipersona.iNumero, ipersona.sNombre_Completo);
                                    if (iRespuesta == -1)
                                    {
                                        LabelTool.ShowLabel(errorLabel, "Error al ingresar la cuenta para la contabilidad!!!", System.Drawing.Color.DarkRed);
                                    }
                                }
                            }
                        }



                        LabelTool.ShowLabel(errorLabel, "Alta exitosa !!!", System.Drawing.Color.DarkRed);
                        AddClientMdl.Hide();

                    }
                }

            }
            else
            {
                LabelTool.ShowLabel(lblerrorvalidacion, "Esta persona ya esta dada de alta como Cliente", System.Drawing.Color.DarkRed);
            }


        }


        private void actualiza_persona_y_cliente()
        {
            asign_person_values();
            ipersona.cliente.iNumero_Persona = Convert.ToInt32(iClientNumber.Value);
            if (!asign_client_values())
            {
                LabelTool.ShowLabel(lblerrorvalidacion, "Error al actualizar el cliente !!!", System.Drawing.Color.DarkRed);
            }
            //validacion de clientes 
            else if (!ipersona.cliente.ModificaPersonaCliente(INTEGRA.Global.connectionString, ipersona.sNombre_Completo, txtpaterno.Text.Trim(), txtmaterno.Text.Trim(),
                                         nametxt.Text.Trim(), txtcorto.Text.Trim(), "",
                                         txtcorreo.Text.Trim(), spersonalidad, txtrfc.Text.Trim().ToUpper(), "", ipersona.cliente.iClasificacion_Cliente, ipersona.cliente.iLista_Precio))
            {
                LabelTool.ShowLabel(lblerrorvalidacion, "Error al actualizar el cliente !!!", System.Drawing.Color.DarkRed);
            }
            else
            {
                ipersona.iNumero = ipersona.cliente.iNumero_Persona;
                ipersona.domicilio.Numero_Persona = ipersona.iNumero;
                ipersona.iTipoCliente = 1;
                asign_addres_values();
                int iNumero_domicilio = Convert.ToInt32(lblNumeroDomicilio.Text);
                if (!ipersona.domicilio.ModifyAddres(INTEGRA.Global.connectionString, Convert.ToInt32(Session["Company"]), iNumero_domicilio))
                {
                    LabelTool.ShowLabel(lblerrorvalidacion, "Error al actualizar el Domicilio, Intente Nuevamente", System.Drawing.Color.DarkRed);
                }
                else
                {
                    LabelTool.ShowLabel(errorLabel, "Modificación exitosa !!!", System.Drawing.Color.DarkRed);
                    AddClientMdl.Hide();
                    keycontainer.Value = iClientNumber.Value;
                    CustomerChanged(null, null);

                }
            }



        }

        private bool RFCValidation()
        {
            if (txtrfc.Text.Trim().ToUpper().Equals("XEXX010101000") || txtrfc.Text.Trim().ToUpper().Equals("XAXX010101000"))
            {
                return true;
            }
            else
            {
                IntegraBussines.CustomerBLL Customer = new IntegraBussines.CustomerBLL();
                return Customer.RFCValidation(txtrfc.Text.ToUpper().Trim(), newpersonrdbt.SelectedIndex);//checar
            }

        }


        private void asign_person_values()
        {

            if (newpersonrdbt.SelectedIndex == 0)
            {
                spersonalidad = "M";
            }
            else
            {
                spersonalidad = "F";
            }

            ipersona.sNombre = nametxt.Text.Trim();
            ipersona.sPaterno = txtpaterno.Text.Trim();
            ipersona.sMaterno = txtmaterno.Text.Trim();
            ipersona.sNombre_Corto = txtcorto.Text.Trim();
            ipersona.sNombre_Completo = ipersona.sNombre + " " + ipersona.sPaterno + " " + ipersona.sMaterno;
            ipersona.sTelefono = null;
            ipersona.sCorreo_Electronico = txtcorreo.Text.Trim();
            ipersona.sPersonalidad_Juridica = spersonalidad;
            ipersona.sRFC = txtrfc.Text.Trim().ToUpper();
            ipersona.sCURP = " ";
        }

        private void LimpiarControlesAlta()
        {
            nametxt.Text = "";
            txtpaterno.Text = "";
            txtmaterno.Text = "";
            txtrfc.Text = "";
            txtcorto.Text = "";
            txtcorreo.Text = "";
            lblerrorvalidacion.Text = "";
            txtCalle.Text = "";
            txtColonia.Text = "";
            txtCp.Text = "";
            txtEstado.Text = "";
            txtDelegacion.Text = "";

        }

        private void llena_controlesalta()
        {

            llenado_ddl("pais");
            llenado_ddl("formapago");
            llenado_ddl("datospago");

        }

        protected void ObtieneDatosCliente()
        {
            CustomerBLL DatoPersona = new CustomerBLL();
            string spersonalidad = string.Empty;
            DataTable TablaCliente = new DataTable();
            DataTable TablaDomicilio = new DataTable();
            lblNumeroDomicilio.Text = "0";
            //TablaCliente = PersonBLL.GetDataFromPersonNumber(Convert.ToInt32(Session["Company"]), Convert.ToInt32(iClientNumber.Value)); cambie
            TablaCliente = DatoPersona.GetPersonData(Convert.ToInt32(Session["Company"]), Convert.ToInt32(iClientNumber.Value));
            //TablaDomicilio = PersonBLL.GetAddresFromPersonNumber(Convert.ToInt32(iClientNumber.Value)); cambie
            TablaDomicilio = PersonBLL.ObtenDomicilioPersona(Convert.ToInt32(iClientNumber.Value));

            if (TablaCliente != null && TablaCliente.Rows.Count > 0)
            {
                spersonalidad = TablaCliente.Rows[0][14].ToString();

                if (spersonalidad == "M")
                {
                    newpersonrdbt.SelectedIndex = 0;
                    nametxt.Text = TablaCliente.Rows[0][2].ToString();
                    txtrfc.Text = TablaCliente.Rows[0][7].ToString();
                    txtcorto.Text = TablaCliente.Rows[0][6].ToString();
                    txtcorreo.Text = TablaCliente.Rows[0][13].ToString();
                    ddlformapago.SelectedIndex = Convert.ToInt32(TablaCliente.Rows[0][36]);
                    string clas_pais = TablaCliente.Rows[0][13].ToString();
                    string clas_dato_pago = TablaCliente.Rows[0][40].ToString();
                    int index = get_index(clas_pais, "pais");
                    ddlpais.SelectedIndex = index;
                    //int inx = get_index(clas_dato_pago, "Clas_Dato_Pago");
                    //ddldatospago.SelectedIndex = index;
                    if (clas_dato_pago != "")
                        ddldatospago.SelectedValue = clas_dato_pago;

                }
                else
                {
                    newpersonrdbt.SelectedIndex = 1;
                    nametxt.Text = TablaCliente.Rows[0][5].ToString();
                    txtpaterno.Text = TablaCliente.Rows[0][3].ToString();
                    txtmaterno.Text = TablaCliente.Rows[0][4].ToString();
                    txtrfc.Text = TablaCliente.Rows[0][7].ToString();
                    txtcorto.Text = TablaCliente.Rows[0][6].ToString();
                    txtcorreo.Text = TablaCliente.Rows[0][13].ToString();
                    ddlformapago.SelectedIndex = Convert.ToInt32(TablaCliente.Rows[0][36]);
                    string clas_pais = TablaCliente.Rows[0][13].ToString();
                    string clas_dato_pago = TablaCliente.Rows[0][40].ToString();
                    int index = get_index(clas_pais, "pais");
                    ddlpais.SelectedIndex = index;
                    //int inx = get_index(clas_dato_pago, "Clas_Dato_Pago");
                    //ddldatospago.SelectedIndex = inx;
                    if (clas_dato_pago != "")
                        ddldatospago.SelectedValue = clas_dato_pago;
                }

            }

            if (TablaDomicilio != null && TablaDomicilio.Rows.Count > 0)
            {
                lblNumeroDomicilio.Text = TablaDomicilio.Rows[0][0].ToString();
                txtCalle.Text = TablaDomicilio.Rows[0][2].ToString();
                txtColonia.Text = TablaDomicilio.Rows[0][3].ToString();
                txtCp.Text = TablaDomicilio.Rows[0][6].ToString();
                txtEstado.Text = TablaDomicilio.Rows[0][5].ToString();
                txtDelegacion.Text = TablaDomicilio.Rows[0][4].ToString();

            }

            NewPersonChanged(null, null);
        }

        private int get_index(string sclient_type, string smove_type)
        {
            DataTable temp_table = new DataTable();
            int iindex = 0;
            int ifather;
            int ifamily;

            switch (smove_type)
            {

                case "pais":
                    if ((temp_table = ControlsBLL.GetCustomerType(Convert.ToInt32(Session["Company"]), Convert.ToInt32(Session["Person"]))).Rows.Count == 0)
                        return 0;
                    break;

                case "Clas_Dato_Pago":
                    ifather = -63;
                    ifamily = 3;
                    if ((temp_table = ControlsBLL.GetCustomerDataPay(Convert.ToInt32(Session["Company"]), ifather, ifamily)).Rows.Count == 0)
                        return 0;
                    break;

            }
            foreach (DataRow row in temp_table.Rows)
            {
                if (row["Numero"].ToString() == sclient_type)
                    break;
                iindex++;

                if (iindex >= temp_table.Rows.Count)
                    iindex = 0;
            }
            return iindex;
        }

        protected void CustomerChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    LabelTool.HideLabel(errorLabel);
                }
                CustomerBLL Customer = new CustomerBLL();
                DataTable CustomerTable;
                int idClient;

                if (keycontainer.Value == "")
                {
                    iClientNumber.Value = "0";
                }
                else
                {
                    iClientNumber.Value = keycontainer.Value;
                    idClient = Convert.ToInt32(iClientNumber.Value);
                    using (CustomerTable = Customer.GetPersonData(Convert.ToInt32(Session["Company"]), idClient).Copy())
                    {
                        if (CustomerTable != null && CustomerTable.Rows.Count > 0)
                        {
                            if (CustomerTable.Rows[0]["Clas_Dato_Pago"] != DBNull.Value)
                                ddlmetodo.SelectedValue = ddlmetodo.Items.FindByValue(CustomerTable.Rows[0]["Clas_Dato_Pago"].ToString()).Value;
                            SetCustomerData(CustomerTable);
                            GetAddressData();
                            lnkModificaCliente.Visible = true;
                        }
                        else
                        {
                            LabelTool.ShowLabel(errorLabel, "Error al conseguir los datos del cliente!!!", System.Drawing.Color.DarkRed);
                            lnkModificaCliente.Visible = false;
                        }
                    }
                }

                customertxt.Text = "";
                keycontainer.Value = "";
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

            }

        }

        private void asign_addres_values()
        {
            ipersona.domicilio.Domicilio = txtCalle.Text.Trim();
            ipersona.domicilio.Colonia = txtColonia.Text.Trim();
            ipersona.domicilio.Delegacion = txtDelegacion.Text.Trim();
            ipersona.domicilio.Estado = txtEstado.Text.Trim();
            ipersona.domicilio.Codigo_Postal = txtCp.Text.Trim();
            ipersona.domicilio.Telefono = "";
            ipersona.domicilio.Fax = "";
            ipersona.domicilio.Clas_Pais = get_clas_country();
        }

        private bool GetCustomerExistingTable()
        {
            CustomerBLL cliente = new CustomerBLL();
            int iclient_number = GetCustomerExisting();

            if (iclient_number != 0)
            {
                DataTable CustomerTable = new DataTable();

                CustomerTable = cliente.GetCustomerData(Convert.ToInt32(Session["Company"]), iclient_number);

                if (CustomerTable != null)
                {
                    if (CustomerTable.Rows.Count > 0)
                    {

                        return false;
                    }

                }
            }

            return true;

        }

        private bool asign_client_values()
        {
            try
            {
                string date = DateTime.Now.ToShortDateString();
                ipersona.cliente.Fecha_Alta = Convert.ToDateTime(date);
                ipersona.cliente.sGiro = " ";
                ipersona.cliente.iPeriodo_Pago = 1;
                if (ddlformapago.SelectedIndex != -1)
                {
                    ipersona.cliente.iForma_Pago = ddlformapago.SelectedIndex;
                }
                else
                {
                    ipersona.cliente.iForma_Pago = null;
                }

                ipersona.cliente.iLista_Precio = null;

                if (ddldatospago.SelectedIndex != -1)
                {
                    ipersona.cliente.iClas_Dato_Pago = Convert.ToInt32(ddldatospago.SelectedValue);
                }
                else
                {
                    ipersona.cliente.iClas_Dato_Pago = null;
                }

                DataTable temp_table = new DataTable();
                int ipadre = 1;
                int ifamilia = 3;
                temp_table = ControlsBLL.GetCountry(ipersona.cliente.iEmpresa, ipadre, ifamilia);
                if (temp_table.Rows[0][0] != DBNull.Value)
                {
                    ipersona.cliente.iClasificacion_Cliente = Convert.ToInt32(temp_table.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref errorLabel);
                return false;
            }


            return true;
        }

        private void SetCustomerData(DataTable CustomerTable)
        {
            customernamelbl.Text = CustomerTable.Rows[0][2].ToString();
            rfclbl.Text = CustomerTable.Rows[0][7].ToString();
        }

        private void GetAddressData()
        {
            int itypeperson = 13;
            DataTable AddressTable;
            int idClient = Convert.ToInt32(iClientNumber.Value);

            using (AddressTable = PersonBLL.ObtenDomicilioPersonaPorTipoDomicilio(idClient, itypeperson))
            {
                if (AddressTable != null && AddressTable.Rows.Count > 0)
                {
                    SetAddressData(AddressTable);
                }
            }
        }

        protected void ModificaClient_Click(object sender, EventArgs e)
        {
            try
            {
                hdnMovimiento.Value = "Actualizacion";
                Cliente(1);//Cliente modifica

            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref errorLabel);
            }

        }

        private bool eMailValidation()
        {
            IntegraBussines.CustomerBLL Customer = new IntegraBussines.CustomerBLL();
            return Customer.emailValidation(txtcorreo.Text);
        }

        private void SetAddressData(DataTable AddressTable)
        {
            streetlbl.Text = AddressTable.Rows[0][3].ToString();
            neighborlbl.Text = AddressTable.Rows[0][4].ToString() + "&nbsp;" + AddressTable.Rows[0][5].ToString();

        }

        #endregion

        protected void lnkRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ventas.aspx");
        }

    }
}