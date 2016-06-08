using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IntegraBussines;

namespace Integra_Develoment.Ventas
{
    public partial class BuscarDevoluciones : System.Web.UI.Page
    {
        #region Variables

        DateTime fechaDe, fechaHasta;

        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarDropEstatus();
                LlenarDropConDevo();
                lblError.Visible = false;
                txtFechaDe.Attributes.Add("onkeypress", " javascript:return ValidNum(event);");
                txtFechaHasta.Attributes.Add("onkeypress", " javascript:return ValidNum(event);");

            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                LlenarGrid();
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                  this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);

            }

        }
        protected void grvDevoluciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grvDevoluciones.EditIndex = -1;
                grvDevoluciones.PageIndex = e.NewPageIndex;
                LlenarGrid();
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                 this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }
        protected void grvDevoluciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnBorrar = (ImageButton)e.Row.FindControl("btnBorrar");
                btnBorrar.Attributes.Add("onclick", "javascript:return " + "confirm('Desea cancelar la devolución " + DataBinder.Eval(e.Row.DataItem, "Numero") + "?" + "')");
                e.Row.Cells[1].ToolTip = "Cancelar Devolución";
            }
        }
        protected void btnConfirmarClick(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                LabelTool.HideLabel(lblError);
                hdnEntradaAlmacen.Value = "0";
                hdnEstatus.Value = "0";
                hdnTipoDevolucion.Value = "0";
                hdnNumeroDevolucion.Value = "0";
                hdnNumeroFactura.Value = "0";
                lblNombreCorto.Text = "";
                lblAlmacen.Text = "";
                int index = Convert.ToInt32(e.RowIndex.ToString());
                hdnEstatus.Value = grvDevoluciones.DataKeys[index].Values["Estatus"].ToString();
                hdnNumeroDevolucion.Value = grvDevoluciones.DataKeys[index].Values["Numero"].ToString();
                lblNombreCorto.Text = grvDevoluciones.DataKeys[index].Values["Nombre_Corto"].ToString();
                lblAlmacen.Text = grvDevoluciones.DataKeys[index].Values["Clas_Almacen"].ToString();
                lblClasIva.Text = grvDevoluciones.DataKeys[index].Values["Clas_Iva"].ToString();
                lblIdCliente.Text = grvDevoluciones.DataKeys[index].Values["Numero_Cliente"].ToString();
                lblTotal.Text = grvDevoluciones.DataKeys[index].Values["Total"].ToString();
                hdnTipoDevolucion.Value = grvDevoluciones.DataKeys[index].Values["Tipo_Devolucion"].ToString();
                hdnEntradaAlmacen.Value = grvDevoluciones.DataKeys[index].Values["Numero_Entrada"].ToString();
                hdnNumeroFactura.Value = grvDevoluciones.DataKeys[index].Values["Folio"].ToString();
                LimpiarModalCancelacion();
                mdlDatosCancelacion.Show();

            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                 this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                LabelTool.HideLabel(lblError);
                scrollagregar.Visible = false;
                ddlEstatus.SelectedValue = "5";
                ddlConcDev.SelectedValue = "0";
                txtFechaDe.Text = "";
                txtFechaHasta.Text = "";
                txtCliente.Text = "";
                keycontainer.Value = "0";
                txtNumeroDe.Text = "";
                txtNumeroHasta.Text = "";
                GridViewTool.Bind(null, grvDevoluciones);
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                 this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }

        }
        protected void cierraModal_Click(object sender, EventArgs e)
        {
            txtFechaCancelacion.Text = "";
            txtCausaCancelacion.Text = "";
            mdlDatosCancelacion.Hide();
        }
        protected void cierraModalTransaccion_Click(object sender, EventArgs e)
        {
            mdlTransaccion.Hide();

        }
        protected void linkCancelacion_Click(object sender, EventArgs e)
        {
            try
            {
                LabelTool.HideLabel(lblError);

                //validar la fecha de cancelacion
                if (txtFechaCancelacion.Text.Trim() != "" && txtCausaCancelacion.Text.Trim() != "")
                {
                    if (ValidaPeriodoContable(Convert.ToInt32(txtFechaCancelacion.Text.Substring(3, 2)), Convert.ToInt32(txtFechaCancelacion.Text.Substring(6, 4))))
                    {
                        if (Convert.ToInt32(hdnEstatus.Value) != 2)// Estatus cancelada
                        {
                            if (VerificaAnticipo()) //Verifica anticipos
                            {
                                int intCancelada = CancelaDevolucionCliente();
                                if (intCancelada != 0)
                                    if (ContabilizaReversaDevolucion())
                                    {
                                        LlenarGrid();
                                        LabelTool.ShowLabel(lblError, "La devolución se canceló correctamente", System.Drawing.Color.DarkRed);
                                        mdlDatosCancelacion.Hide();
                                    }
                            }
                        }
                        else
                        {

                            LabelTool.ShowLabel(lblError, "La devolución ya se encuentra cancelada", System.Drawing.Color.DarkRed);
                            mdlDatosCancelacion.Hide();
                        }

                    }
                    else
                    {
                        LabelTool.ShowLabel(lblMensaje, "El Período contable se encuentra cerrado", System.Drawing.Color.DarkRed);

                    }
                }
                else
                {
                    LabelTool.ShowLabel(lblMensaje, "Ingrese los datos faltantes", System.Drawing.Color.DarkRed);
                }

            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                 this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }

        }
        protected void lnkDevolucionSinTransaccion_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtContabilidad = new DataTable();
                DataTable dtAlmacen = new DataTable();
                DataTable dtCtaCliente = new DataTable();
                DataTable dtNumeroTransaccion = new DataTable();
                DataTable dtFolioFiscal = new DataTable();

                int intMovimiento = 1, intNumeroPrograma = 289, intConcepto = 158;// concepto cambiar
                int intNumeroTransaccion = 0, intTransaccion = 0;
                string sCont_Clas_Cuenta = "";
                int intNumeroNota = 0;
                int intTipoNota = 0;
                int intEntradaConcepto = 0, intNotaConcepto = 0, intMovimientoBancarioConcepto = 0, intConceptoNota = 0;
                string sFolioFiscal = string.Empty;

                if (lblMsjConcepto.Text == "No se encontró el registro para la contabilidad")
                {

                    if (validaTransaccion())
                    {
                        dtAlmacen = DevolucionController.ObtenClasAlmacen(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblAlmacen.Text));
                        if (dtAlmacen != null)
                        {
                            sCont_Clas_Cuenta = sCont_Clas_Cuenta + dtAlmacen.Rows[0][0].ToString();

                        }

                        dtCtaCliente = DevolucionController.ObtenClasCliente(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdCliente.Text));
                        if (dtCtaCliente != null)
                        {
                            sCont_Clas_Cuenta = sCont_Clas_Cuenta + dtCtaCliente.Rows[0][0].ToString().PadLeft(11);
                        }

                        string sDes_Poliza = "";

                        if (Convert.ToInt32(hdnTipoDevolucion.Value) == 0)// Devolución con factura origen
                        {
                            sDes_Poliza = hdnNumeroDevolucion + " " + lblNombreCorto.Text + "F:" + hdnTipoDevolucion.Value;
                        }
                        else
                        {
                            sDes_Poliza = hdnNumeroDevolucion + " " + lblNombreCorto.Text + "S/F";
                        }

                        DataTable dtConceptosDevoluciones = DevolucionController.ObtenConceptosDevolucionCliente(Convert.ToInt32(Session["Company"]), 158);

                        if (dtConceptosDevoluciones.Rows.Count > 0)
                        {
                            intEntradaConcepto = Convert.ToInt32(dtConceptosDevoluciones.Rows[0][0]);
                            intNotaConcepto = Convert.ToInt32(dtConceptosDevoluciones.Rows[0][1]);
                            intMovimientoBancarioConcepto = Convert.ToInt32(dtConceptosDevoluciones.Rows[0][2]);
                            intConceptoNota = Convert.ToInt32(dtConceptosDevoluciones.Rows[0][3]);
                        }


                        dtFolioFiscal = DevolucionController.ObtenFolioFiscal(Convert.ToInt32(Session["Company"]), Convert.ToInt32(hdnNumeroFactura.Value), Convert.ToInt32(lblIdCliente.Text));
                        if (dtFolioFiscal != null && dtFolioFiscal.Rows.Count > 0)
                        {
                            sFolioFiscal = dtFolioFiscal.Rows[0][0].ToString();
                        }

                        dtContabilidad = DevolucionController.CreateContabilidadDevolucionReversa(intMovimiento, Convert.ToInt32(Session["Company"]), intTransaccion, sCont_Clas_Cuenta, 121,
                                                                                                  Convert.ToDecimal(lblTotal.Text), Convert.ToInt32(Session["Person"]), null, 109,
                                                                                                  Convert.ToDateTime(txtFechaCancelacion.Text), sDes_Poliza, Convert.ToInt32(hdnEntradaAlmacen.Value),
                                                                                                  Convert.ToInt32(lblAlmacen.Text), intNumeroNota, intTipoNota, Convert.ToInt32(hdnNumeroDevolucion.Value),
                                                                                                  intEntradaConcepto, intNotaConcepto, intMovimientoBancarioConcepto,
                                                                                                  Convert.ToInt32(lblIdCliente.Text), Convert.ToInt32(lblClasIva.Text), sFolioFiscal);
                        LabelTool.ShowLabel(lblError, "Cancelación satisfactoria", System.Drawing.Color.DarkRed);
                        mdlTransaccion.Hide();
                        mdlDatosCancelacion.Hide();
                        LlenarGrid();
                    }
                    else
                    {
                        mdlTransaccion.Show(); //Este concepto no tiene afectación contable definida.  modal con mensaje para continuar si/no.
                    }
                }
                else
                {
                    dtAlmacen = DevolucionController.ObtenClasAlmacen(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblAlmacen.Text));
                    if (dtAlmacen != null)
                    {
                        sCont_Clas_Cuenta = sCont_Clas_Cuenta + dtAlmacen.Rows[0][0].ToString();

                    }

                    dtCtaCliente = DevolucionController.ObtenClasCliente(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdCliente.Text));
                    if (dtCtaCliente != null)
                    {
                        sCont_Clas_Cuenta = sCont_Clas_Cuenta + dtCtaCliente.Rows[0][0].ToString().PadLeft(11);
                    }

                    string sDes_Poliza = "";

                    if (Convert.ToInt32(hdnTipoDevolucion.Value) == 0)// Devolución con factura origen
                    {
                        sDes_Poliza = hdnNumeroDevolucion + " " + lblNombreCorto.Text + "F:" + hdnTipoDevolucion.Value;
                    }
                    else
                    {
                        sDes_Poliza = hdnNumeroDevolucion + " " + lblNombreCorto.Text + "S/F";
                    }

                    DataTable dtConceptosDevoluciones = DevolucionController.ObtenConceptosDevolucionCliente(Convert.ToInt32(Session["Company"]), 158);

                    if (dtConceptosDevoluciones.Rows.Count > 0)
                    {
                        intEntradaConcepto = Convert.ToInt32(dtConceptosDevoluciones.Rows[0][0]);
                        intNotaConcepto = Convert.ToInt32(dtConceptosDevoluciones.Rows[0][1]);
                        intMovimientoBancarioConcepto = Convert.ToInt32(dtConceptosDevoluciones.Rows[0][2]);
                        intConceptoNota = Convert.ToInt32(dtConceptosDevoluciones.Rows[0][3]);
                    }

                    dtFolioFiscal = DevolucionController.ObtenFolioFiscal(Convert.ToInt32(Session["Company"]), Convert.ToInt32(hdnNumeroFactura.Value), Convert.ToInt32(lblIdCliente.Text));
                    if (dtFolioFiscal != null && dtFolioFiscal.Rows.Count > 0)
                    {
                        sFolioFiscal = dtFolioFiscal.Rows[0][0].ToString();
                    }


                    dtContabilidad = DevolucionController.CreateContabilidadDevolucionReversa(intMovimiento, Convert.ToInt32(Session["Company"]), intTransaccion, sCont_Clas_Cuenta, 121,
                                                                                              Convert.ToDecimal(lblTotal.Text), Convert.ToInt32(Session["Person"]), null, 109,
                                                                                              Convert.ToDateTime(txtFechaCancelacion.Text), sDes_Poliza, Convert.ToInt32(hdnEntradaAlmacen.Value),
                                                                                              Convert.ToInt32(lblAlmacen.Text), intNumeroNota, intTipoNota, Convert.ToInt32(hdnNumeroDevolucion.Value),
                                                                                              intEntradaConcepto, intNotaConcepto, intMovimientoBancarioConcepto, Convert.ToInt32(lblIdCliente.Text),
                                                                                              Convert.ToInt32(lblClasIva.Text), sFolioFiscal);

                }

                LabelTool.ShowLabel(lblError, "Cancelación satisfactoria", System.Drawing.Color.DarkRed);
                mdlTransaccion.Hide();
                mdlDatosCancelacion.Hide();
                LlenarGrid();

            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                  this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
                mdlTransaccion.Hide();
                mdlDatosCancelacion.Hide();

            }

        }
        protected void NoContinua_Click(object sender, EventArgs e)
        {
            mdlDatosCancelacion.Hide();
            mdlTransaccion.Hide();
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConsultaDevolucion.aspx");
        }
        private bool ValidaPeriodoContable(int iPeriodo, int iAnio)
        {
            int intPeriodo = 0;// periodo abierto

            DataTable dtPeriodoContable = DevolucionController.ValidaPeriodoContable(Convert.ToInt32(Session["Company"]), iPeriodo, iAnio);
            if (dtPeriodoContable != null && dtPeriodoContable.Rows.Count > 0)
            {
                intPeriodo = Convert.ToInt32(dtPeriodoContable.Rows[0][0]);
            }
            if (intPeriodo == 0)
            {
                return true;
            }
            else
            {
                // intPeriodo = 1 es un periodo cerrado
                return false;
            }

        }

        #endregion

        #region Metodos/Propiedades


        private void LlenarDropEstatus()
        {
            ddlEstatus.Items.Add(new ListItem("Todos", "5"));
            ddlEstatus.Items.Add(new ListItem("Iniciada", "0"));
            ddlEstatus.Items.Add(new ListItem("Aceptada", "1"));
            ddlEstatus.Items.Add(new ListItem("Cancelada", "2"));
            ddlEstatus.Items.Add(new ListItem("Parcialmente Ingresada", "3"));
            ddlEstatus.Items.Add(new ListItem("Ingresada", "4"));
        }

        public void LlenarDropConDevo()
        {
            try
            {
                ClBuscarDevolucion bc = new ClBuscarDevolucion();
                ddlConcDev.Items.Clear();
                DataTable dt = new DataTable();
                dt = bc.sqlLlenarConceptoDevoluciones(Convert.ToInt32(Session["Company"]));

                DataRow dtRow = dt.NewRow();
                dtRow["Numero"] = 0;
                dtRow["Descripcion"] = "Todos";
                dt.Rows.InsertAt(dtRow, 0);

                this.ddlConcDev.DataSource = dt;
                this.ddlConcDev.DataValueField = "Numero";
                this.ddlConcDev.DataTextField = "Descripcion";
                this.ddlConcDev.DataBind();
            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                 this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
            }
        }

        public void LlenarGrid()
        {
            DateTime? fechaDe = null;
            DateTime? fechaHasta = null;
            int intbandera = 1;
            int intCliente = 0;
            int intNumeroDe = 0;
            int intNumeroHasta = 0;
            lblError.Visible = false;

            if (txtFechaDe.Text != "" || txtFechaHasta.Text != "")
            {
                intbandera = validaFecha();

                if (intbandera == 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Introduzca una fecha válida";
                }
                else
                {
                    if (txtFechaDe.Text.Trim() != "")
                    {
                        fechaDe = Convert.ToDateTime(txtFechaDe.Text);
                    }
                    if (txtFechaHasta.Text.Trim() != "")
                    {
                        fechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
                    }
                }
            }

            if (keycontainer.Value != "")
            {
                intCliente = Convert.ToInt32(keycontainer.Value);
            }
            if (txtNumeroDe.Text != "")
            {
                intNumeroDe = Convert.ToInt32(txtNumeroDe.Text);
            }
            if (txtNumeroHasta.Text != "")
            {
                intNumeroHasta = Convert.ToInt32(txtNumeroHasta.Text);
            }
            if (intbandera == 1)
            {

                ClBuscarDevolucion us = new ClBuscarDevolucion();
                DataTable temp_table = new DataTable();

                temp_table = us.sqlSelectTodoDevoluciones(Convert.ToInt32(Session["Company"]), Convert.ToInt32(Session["Person"]),
                    intCliente, Convert.ToInt32(ddlConcDev.SelectedValue), Convert.ToInt32(ddlEstatus.SelectedValue), fechaDe, fechaHasta, intNumeroDe, intNumeroHasta);

                if (temp_table.Rows.Count > 0)
                {
                    scrollagregar.Visible = true;

                    GridViewTool.Bind(temp_table, grvDevoluciones);
                    LabelTool.HideLabel(lblError);
                }
                else
                {
                    LabelTool.ShowLabel(lblError, "No se encontraron devoluciones con los criterios seleccionados", System.Drawing.Color.DarkRed);

                    scrollagregar.Visible = false;
                }
            }
        }

        public int validaFecha()
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

                if (txtFechaDe.Text != "" && txtFechaHasta.Text != "")
                    if (Convert.ToDateTime(txtFechaDe.Text) > Convert.ToDateTime(txtFechaHasta.Text))
                    {
                        return 0;
                    }

                return 1;
            }
            catch
            {
                return 0;
            }
        }

        protected void LimpiarModalCancelacion()
        {
            lblMensaje.Visible = false;
            txtFechaCancelacion.Text = DateTime.Now.ToShortDateString();
            txtCausaCancelacion.Text = "";
        }

        private bool VerificaAnticipo()
        {
            DataTable dtAnticipo = DevolucionController.gf_Devolucion_VerificaAnticipo(Convert.ToInt32(Session["Company"]), Convert.ToInt32(hdnNumeroDevolucion.Value));

            if (dtAnticipo != null && dtAnticipo.Rows.Count > 0)
            {
                int inttipoDevolucion = Convert.ToInt32(dtAnticipo.Rows[0][0]);
                int intAplicado = Convert.ToInt32(dtAnticipo.Rows[0][1]);
                int intRelacionFactura = Convert.ToInt32(dtAnticipo.Rows[0][2]);

                if (inttipoDevolucion == 1 && intAplicado == 1)
                {
                    LabelTool.ShowLabel(lblError, "No se puede cancelar la devolución ya que el anticipo en cuentas por cobrar ya se encuentra aplicado", System.Drawing.Color.DarkRed);
                    return false;
                }
                else if (inttipoDevolucion == 1 && intRelacionFactura == 1)
                {
                    LabelTool.ShowLabel(lblError, "No se puede cancelar la devolución ya que el anticipo en cuentas por cobrar ya se encuentra aplicado", System.Drawing.Color.DarkRed);
                    return false;
                }

            }
            return true;
        }

        private int CancelaDevolucionCliente()
        {
            try
            {
                DataTable dtDevolucion = new DataTable();
                int intMovimiento = 2;//cancelación
                int intNumeroDevolucion = Convert.ToInt32(hdnNumeroDevolucion.Value);
                int iAlmacen = 0;
                if (lblAlmacen.Text != "")
                {
                    iAlmacen = Convert.ToInt32(lblAlmacen.Text);
                }
                string sFecha = string.Empty;
                sFecha = DateTime.Now.ToShortDateString();


                dtDevolucion = DevolucionController.gf_Devolucion_Cliente(intMovimiento, Convert.ToInt32(Session["Company"]), intNumeroDevolucion, sFecha, null,
                                                                          null, null, iAlmacen, null, null, null, null, null, null, null, null, null, null, "",
                                                                          Convert.ToInt32(Session["Person"]), null, null, null, null, null,
                                                                          Convert.ToDateTime(txtFechaCancelacion.Text), txtCausaCancelacion.Text.Trim(), null, null);

                if (dtDevolucion != null)
                {
                    int intNumeroDev = Convert.ToInt32(dtDevolucion.Rows[0][0]);

                    return intNumeroDev;
                }

                else
                {
                    LabelTool.ShowLabel(lblError, "Error al realizar la devolución, intente nuevamente", System.Drawing.Color.DarkRed);

                    return 0;
                }

            }
            catch (Exception ex)
            {
                LabelTool.ShowLabel(lblError, "La entrada tiene partidas entregadas, no se puede cancelar.", System.Drawing.Color.DarkRed);
                mdlDatosCancelacion.Hide();
                return 0;
            }


        }

        private bool ContabilizaReversaDevolucion()
        {
            try
            {
                DataTable dtNumeroTransaccion = new DataTable();
                DataTable dtTransaccionContable = new DataTable();
                DataTable dtContabilidad = new DataTable();
                DataTable dtAnticipo = new DataTable();
                DataTable dtCliente = new DataTable();

                DataTable dtAlmacen = new DataTable();
                DataTable dtCtaCliente = new DataTable();
                DataTable dtCtaBanco = new DataTable();
                DataTable dtFolioFiscal = new DataTable();

                int intMovimiento = 1, intNumeroPrograma = 289, intConcepto = 158;// concepto cambiar
                int intNumeroTransaccion = 0, intTransaccion = 0;
                string sCont_Clas_Cuenta = "";
                int intNumeroNota = 0;
                int intTipoNota = 0;
                int intEntradaConcepto = 0, intNotaConcepto = 0, intMovimientoBancarioConcepto = 0, intConceptoNota = 0;
                string sFolioFiscal = string.Empty;

                dtNumeroTransaccion = DevolucionController.ObtenNumeroTransaccion(Convert.ToInt32(Session["Company"]), intConcepto, intNumeroPrograma);//ok

                if (dtNumeroTransaccion.Rows.Count > 0)
                {
                    intTransaccion = Convert.ToInt32(dtNumeroTransaccion.Rows[0][0]);
                }

                if (intTransaccion == 0)
                {
                    LabelTool.ShowLabel(lblMsjConcepto, "No se encontró el registro para la contabilidad", System.Drawing.Color.DarkRed);
                    mdlTransaccion.Show();
                    return false;
                    // No se encontro el registro para la contabilidad. modal con mensaje para continuar si/no.
                }
                else
                {
                    dtNumeroTransaccion = DevolucionController.ObtenNumeroTransaccionContable(Convert.ToInt32(Session["Company"]), intTransaccion, intNumeroPrograma);
                    if (dtNumeroTransaccion != null && dtNumeroTransaccion.Rows.Count > 0)
                    {
                        intNumeroTransaccion = Convert.ToInt32(dtNumeroTransaccion.Rows[0][0]);
                    }

                    if (intNumeroTransaccion == 0)
                    {
                        lblMsjConcepto.Text = "Este concepto no tiene afectación contable definida";
                        mdlTransaccion.Show();  //Este concepto no tiene afectación contable definida.  modal con mensaje para continuar si/no.
                        return false;
                    }
                }


                dtAlmacen = DevolucionController.ObtenClasAlmacen(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblAlmacen.Text));
                if (dtAlmacen != null)
                {
                    sCont_Clas_Cuenta = sCont_Clas_Cuenta + dtAlmacen.Rows[0][0].ToString();

                }

                dtCtaCliente = DevolucionController.ObtenClasCliente(Convert.ToInt32(Session["Company"]), Convert.ToInt32(lblIdCliente.Text));
                if (dtCtaCliente != null)
                {
                    sCont_Clas_Cuenta = sCont_Clas_Cuenta + dtCtaCliente.Rows[0][0].ToString().PadLeft(11);
                }

                string sDes_Poliza = "";

                if (Convert.ToInt32(hdnTipoDevolucion.Value) == 0)// Devolución con factura origen
                {
                    sDes_Poliza = hdnNumeroDevolucion.Value + " " + lblNombreCorto.Text + "F:" + hdnTipoDevolucion.Value;
                }
                else
                {
                    sDes_Poliza = hdnNumeroDevolucion.Value + " " + lblNombreCorto.Text + "S/F";
                }

                DataTable dtConceptosDevoluciones = DevolucionController.ObtenConceptosDevolucionCliente(Convert.ToInt32(Session["Company"]), 158);

                if (dtConceptosDevoluciones.Rows.Count > 0)
                {
                    intEntradaConcepto = Convert.ToInt32(dtConceptosDevoluciones.Rows[0][0]);
                    intNotaConcepto = Convert.ToInt32(dtConceptosDevoluciones.Rows[0][1]);
                    intMovimientoBancarioConcepto = Convert.ToInt32(dtConceptosDevoluciones.Rows[0][2]);
                    intConceptoNota = Convert.ToInt32(dtConceptosDevoluciones.Rows[0][3]);
                }


                dtFolioFiscal = DevolucionController.ObtenFolioFiscal(Convert.ToInt32(Session["Company"]), Convert.ToInt32(hdnNumeroFactura.Value), Convert.ToInt32(lblIdCliente.Text));
                if (dtFolioFiscal != null && dtFolioFiscal.Rows.Count > 0)
                {
                    sFolioFiscal = dtFolioFiscal.Rows[0][0].ToString();
                }
                dtContabilidad = DevolucionController.CreateContabilidadDevolucionReversa(intMovimiento, Convert.ToInt32(Session["Company"]), intTransaccion, sCont_Clas_Cuenta, 121,
                                                                                          Convert.ToDecimal(lblTotal.Text), Convert.ToInt32(Session["Person"]), null, 109,
                                                                                          Convert.ToDateTime(txtFechaCancelacion.Text), sDes_Poliza,
                                                                                          Convert.ToInt32(hdnEntradaAlmacen.Value), Convert.ToInt32(lblAlmacen.Text), intNumeroNota, intTipoNota,
                                                                                          Convert.ToInt32(hdnNumeroDevolucion.Value), intEntradaConcepto, 0, intMovimientoBancarioConcepto,
                                                                                          Convert.ToInt32(lblIdCliente.Text), Convert.ToInt32(lblClasIva.Text), sFolioFiscal);


                LabelTool.ShowLabel(lblError, "Cancelación satisfactoria", System.Drawing.Color.DarkRed);
                mdlDatosCancelacion.Hide();
                LlenarGrid();

            }
            catch (Exception ex)
            {
                LOG.gfLog(Session["Person"].ToString(), ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                  this.Page.ToString().Substring(4, this.Page.ToString().Substring(4).Length - 5) + ".aspx", ref lblError);
                mdlDatosCancelacion.Hide();
            }


            return true;
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

        #endregion
    }
}