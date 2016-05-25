using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IntegraData;

namespace IntegraBussines
{
    public static class CajaBLL
    {
        #region //Funciones Caja

        public static DataTable getMoneda(int iNumEmpresa)
        {

            DataTable dtResponsable = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));

            dtResponsable = context.ExecuteProcedure("sp_Caja_Obten_Moneda", true).Copy();

            return dtResponsable;
        }

        public static DataTable getResponsable(int iNumEmpresa)
        {

            DataTable dtResponsable = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iSelect", 1));
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));

            dtResponsable = context.ExecuteProcedure("sp_Caja_Obten_Responsable", true).Copy();

            return dtResponsable;
        }

        public static DataTable getCentroCosto(int iNumEmpresa)
        {

            DataTable dtResponsable = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));

            dtResponsable = context.ExecuteProcedure("sp_Caja_Obten_Centro_Costo", true).Copy();

            return dtResponsable;
        }

        public static DataTable getDeducible(int iNumEmpresa)
        {

            DataTable dtResponsable = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            //context.Parametros.Add(new SqlParameter("@iSelect", 1));
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));

            dtResponsable = context.ExecuteProcedure("sp_Caja_Obten_Deducible", true).Copy();

            return dtResponsable;
        }

        public static DataTable guardarCaja(int iTipo, int iNumEmpresa, int iNumero, string sCajaDescripcion,
                                            int iEstatus, int iClasMoneda, string sDesMoneda,
                                            int iPersonaResponsable, int iClasCentroCosto, decimal dImporteMax)
        {

            DataTable dtGuardar = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@TipoMovimiento", iTipo));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero", iNumero));
            context.Parametros.Add(new SqlParameter("@Caja_Descripcion", sCajaDescripcion));
            context.Parametros.Add(new SqlParameter("@Clas_Estatus", iEstatus));
            context.Parametros.Add(new SqlParameter("@Clas_Moneda", iClasMoneda));
            context.Parametros.Add(new SqlParameter("@Descripcion_Moneda", sDesMoneda));
            context.Parametros.Add(new SqlParameter("@Persona_responsable", iPersonaResponsable));
            context.Parametros.Add(new SqlParameter("@Centro_Costo", iClasCentroCosto));
            context.Parametros.Add(new SqlParameter("@Importe", dImporteMax));

            dtGuardar = context.ExecuteProcedure("sp_Caja_Chica", true).Copy();

            return dtGuardar;
        }

        public static DataTable getConceptosCaja(int iNumEmpresa, int iNumCaja)
        {

            DataTable dtGuardar = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumCaja", iNumCaja));

            dtGuardar = context.ExecuteProcedure("sp_Caja_Chica_Obten_Caja_Concepto", true).Copy();

            return dtGuardar;
        }

        public static DataTable guardarRelacionCajaDeducible(int iTipo, int iNumEmpresa, int iNumero, int iNumCaja)
        {

            DataTable dtGuardar = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iTipo", iTipo));
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumero", iNumero));
            context.Parametros.Add(new SqlParameter("@iNumCaja", iNumCaja));


            dtGuardar = context.ExecuteProcedure("sp_Caja_Chica_Caja_Concepto", true).Copy();

            return dtGuardar;
        }

        public static DataTable guardarCajaNoDeducible(int iTipo, int iNumEmpresa, int iEstatus,
                                                        int iNumero, string sDescripcion, int iClasConcepto, int iDeducible)
        {

            DataTable dtGuardar = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@TipoMovimiento", iTipo));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@Estatus", iEstatus));
            context.Parametros.Add(new SqlParameter("@Numero", iNumero));
            context.Parametros.Add(new SqlParameter("@Descripcion", sDescripcion));
            context.Parametros.Add(new SqlParameter("@Clas_Concepto", iClasConcepto));
            context.Parametros.Add(new SqlParameter("@Deducible", iDeducible));

            dtGuardar = context.ExecuteProcedure("sp_Caja_Chica_Deducible", true).Copy();

            return dtGuardar;
        }

        public static DataTable getConceptos(int iNumEmpresa, int iConsulta, int iNumGasto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            //context.Parametros.Add(new SqlParameter("@iNumeroCaja", 1));
            context.Parametros.Add(new SqlParameter("@iConsulta", iConsulta));
            context.Parametros.Add(new SqlParameter("@iNumGasto", iNumGasto));

            dt = context.ExecuteProcedure("sp_Caja_Obten_Gastos", true).Copy();
            return dt;
        }

        public static DataTable getCaja(int iConsulta, int iNumEmpresa, int iNumCaja, int iClasMoneda,
                                        int iResponsable, int iCentroCosto)
        {

            DataTable dtResponsable = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iConsulta", iConsulta));
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumCaja", iNumCaja));
            context.Parametros.Add(new SqlParameter("@iMoneda", iClasMoneda));
            context.Parametros.Add(new SqlParameter("@iResponsable", iResponsable));
            context.Parametros.Add(new SqlParameter("@iCentroCosto", iCentroCosto));

            dtResponsable = context.ExecuteProcedure("sp_Caja_Obten_Caja", true).Copy();

            return dtResponsable;
        }

        #endregion

        #region //CAJA01 Dotación sin Solicitud

        //llena todos los combos de la pantallas de Caja Dotación de Efectivo
        public static DataTable LlenaCombo(string sPantalla, int iCombo, int iNumEmpresa, int iNumCaja, int iNumPersona,
                                int iNumBanco)
        {

            DataTable dtDatos = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@sPantalla", sPantalla));
            context.Parametros.Add(new SqlParameter("@iCombo", iCombo));
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumCaja", iNumCaja));
            context.Parametros.Add(new SqlParameter("@iNumPersona", iNumPersona));
            context.Parametros.Add(new SqlParameter("@iNumBanco", iNumBanco));

            dtDatos = context.ExecuteProcedure("sp_Caja_Llena_Combo", true).Copy();

            return dtDatos;
        }

        //Consultas para la pantalla Caja Dotación de Efectivo
        public static DataTable CajaChicaConsultas(string sPantalla, int iConsulta, int iNumEmpresa, int iClasConsepto,
                                string sNombre, long lNumeroTransaccion)
        {

            DataTable dtDatos = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@sPantalla", sPantalla));
            context.Parametros.Add(new SqlParameter("@iConsulta", iConsulta));
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iClasConcepto", iClasConsepto));
            context.Parametros.Add(new SqlParameter("@sNombre", sNombre));
            context.Parametros.Add(new SqlParameter("@iNumeroTransaccion", lNumeroTransaccion));

            dtDatos = context.ExecuteProcedure("sp_Caja_Chica_Consultas", true).Copy();

            return dtDatos;
        }

        //funcion que guarda la dotacion de efectivo para la caja
        public static DataTable GuardarCajaDotacionEfectivo(int iTipoMovimiento, int iNumero_Empresa, int iNumero,
                                                        int iNumero_Caja, int iClas_Concepto, int iForma_Dotacion,
                                                        int iClas_Moneda, decimal dImporte, int iNumero_Banco,
                                                        int iClas_Cuenta, string sFolio, int iNumero_Beneficiario,
                                                        string sObservaciones, int iUsuario, long iNumero_Transaccion,
                                                        string sAuxiliar, string sFecha, int iContabiliza)
        {
            DataTable dtResultado = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@TipoMovimiento", iTipoMovimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero", iNumero));
            context.Parametros.Add(new SqlParameter("@Numero_Caja", iNumero_Caja));
            context.Parametros.Add(new SqlParameter("@Clas_Concepto", iClas_Concepto));
            context.Parametros.Add(new SqlParameter("@Forma_Dotacion", iForma_Dotacion));
            context.Parametros.Add(new SqlParameter("@Clas_Moneda", iClas_Moneda));
            context.Parametros.Add(new SqlParameter("@Importe", dImporte));
            context.Parametros.Add(new SqlParameter("@Numero_Banco", iNumero_Banco));
            context.Parametros.Add(new SqlParameter("@Clas_Cuenta", iClas_Cuenta));
            context.Parametros.Add(new SqlParameter("@Folio", sFolio));
            context.Parametros.Add(new SqlParameter("@Numero_Beneficiario", iNumero_Beneficiario));
            context.Parametros.Add(new SqlParameter("@Observaciones", sObservaciones));
            context.Parametros.Add(new SqlParameter("@Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Transaccion", iNumero_Transaccion));
            context.Parametros.Add(new SqlParameter("@Auxiliar", sAuxiliar));
            context.Parametros.Add(new SqlParameter("@Fecha", sFecha));
            context.Parametros.Add(new SqlParameter("@Contabiliza", iContabiliza));


            dtResultado = context.ExecuteProcedure("sp_Caja_Chica_Dotacion", true).Copy();

            return dtResultado;
        }

        //Consulta todos los datos de Caja Dotación de Efectivo
        public static DataTable getDotacionEfectivo(int iNumEmpresa, int iNumUsuario, int iNumero, string sFecha, int iNumCaja, int iClasConcepto,
                                int iFormaDotacion, int iMoneda, decimal dImporte, int iNumBanco, int iClasCuenta, string sFolio, int iNumBeneficiario, string sFechaF)
        {

            DataTable dtDatos = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumUsuario", iNumUsuario));
            context.Parametros.Add(new SqlParameter("@iNumero", iNumero));
            context.Parametros.Add(new SqlParameter("@sFecha", sFecha));
            context.Parametros.Add(new SqlParameter("@iNumCaja", iNumCaja));
            context.Parametros.Add(new SqlParameter("@iClasConcepto", iClasConcepto));
            context.Parametros.Add(new SqlParameter("@iFormaDotacion", iFormaDotacion));
            context.Parametros.Add(new SqlParameter("@iMoneda", iMoneda));
            context.Parametros.Add(new SqlParameter("@dImporte", dImporte));
            context.Parametros.Add(new SqlParameter("@iNumBanco", iNumBanco));
            context.Parametros.Add(new SqlParameter("@iClasCuenta", iClasCuenta));
            context.Parametros.Add(new SqlParameter("@sFolio", sFolio));
            context.Parametros.Add(new SqlParameter("@iNumBeneficiario", iNumBeneficiario));
            context.Parametros.Add(new SqlParameter("@sFechaF", sFechaF));

            dtDatos = context.ExecuteProcedure("sp_Caja_Chica_Obten_Dotacion", true).Copy();

            return dtDatos;
        }

        #endregion

        #region //CAJA02 Disposición de Efectico

        //funcion que guarda la disposicion de efectivo para la caja
        public static DataTable GuardarCajaDisposicionEfectivo(int iTipoMovimiento, int iNumero_Empresa, int iNumero,
                                                        int iNumero_Caja, string sMotivo, decimal dImporte, string sPersona_Asignado,
                                                        int iClas_Moneda, int iUsuario, string sFecha,
                                                        string sObservaciones)
        {
            DataTable dtResultado = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@TipoMovimiento", iTipoMovimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero", iNumero));
            context.Parametros.Add(new SqlParameter("@Numero_Caja", iNumero_Caja));
            context.Parametros.Add(new SqlParameter("@Motivo", sMotivo));
            context.Parametros.Add(new SqlParameter("@Importe", dImporte));
            context.Parametros.Add(new SqlParameter("@Persona_Asignado", sPersona_Asignado));
            context.Parametros.Add(new SqlParameter("@Clas_Moneda", iClas_Moneda));
            context.Parametros.Add(new SqlParameter("@Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("@Fecha_Ingreso", sFecha));
            context.Parametros.Add(new SqlParameter("@Observacion", sObservaciones));

            dtResultado = context.ExecuteProcedure("sp_Caja_Chica_Egreso", true).Copy();

            return dtResultado;
        }

        //Consulta todos los datos de Caja Disposición de Efectivo
        public static DataTable getDisposicionEfectivo(int iNumEmpresa, int iNumUsuario, int iNumero, int iNumCaja, string sFecha,
                                string sMotivo, string sNomAsignado, decimal dImporte, string sFechaF, int iConsulta,
                                decimal dImporteF)
        {

            DataTable dtDatos = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumUsuario", iNumUsuario));
            context.Parametros.Add(new SqlParameter("@iNumero", iNumero));
            context.Parametros.Add(new SqlParameter("@iNumCaja", iNumCaja));
            context.Parametros.Add(new SqlParameter("@sFecha", sFecha));
            context.Parametros.Add(new SqlParameter("@sMotivo", sMotivo));
            context.Parametros.Add(new SqlParameter("@sNomAsignado", sNomAsignado));
            context.Parametros.Add(new SqlParameter("@dImporte", dImporte));
            context.Parametros.Add(new SqlParameter("@sFechaF", sFechaF));
            context.Parametros.Add(new SqlParameter("@iConsulta", iConsulta));
            context.Parametros.Add(new SqlParameter("@dImporteF", dImporteF));

            dtDatos = context.ExecuteProcedure("sp_Caja_Chica_Obten_Egreso", true).Copy();

            return dtDatos;
        }
        #endregion

        #region //CAJA03 Pendientes de Comprobar

        public static DataTable getPendientes(int iNumEmpresa, int iNumResponsable)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumResponsable", iNumResponsable));

            dt = context.ExecuteProcedure("sp_Caja_Chica_Obten_Egreso", true).Copy();
            return dt;
        }

        #endregion

        #region //CAJA04 Comprobación de Efectivo
        //funcion que guarda la Comprobación de efectivo para la caja
        public static DataTable GuardarCajaComprobacion(int iTipoMovimiento, int iNumero_Empresa, int Numero_Egreso, int iNumero,
                                                        string sFolio, string sProveedor, int iNumProveedor, decimal dImporte2,
                                                        int iClas_Iva, decimal dIVA, int iClas_Estatus, int iNum_Concepto, int iClas_Concepto,
                                                        int iUsuario)
        {
            DataTable dtResultado = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@TipoMovimiento", iTipoMovimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Egreso", Numero_Egreso));
            context.Parametros.Add(new SqlParameter("@Numero", iNumero));
            context.Parametros.Add(new SqlParameter("@Folio", sFolio));
            context.Parametros.Add(new SqlParameter("@Proveedor", sProveedor));
            context.Parametros.Add(new SqlParameter("@Num_Proveedor", iNumProveedor));
            context.Parametros.Add(new SqlParameter("@Importe", dImporte2));
            context.Parametros.Add(new SqlParameter("@Clas_Iva", iClas_Iva));
            context.Parametros.Add(new SqlParameter("@Iva", dIVA));
            context.Parametros.Add(new SqlParameter("@Clas_Estatus", iClas_Estatus));
            context.Parametros.Add(new SqlParameter("@Num_Concepto", iNum_Concepto));
            context.Parametros.Add(new SqlParameter("@Clas_Concepto", iClas_Concepto));
            context.Parametros.Add(new SqlParameter("@Usuario", iUsuario));



            dtResultado = context.ExecuteProcedure("sp_Caja_Chica_Comprobacion", true).Copy();

            return dtResultado;
        }

        //Consulta todos los datos Comprobantes de Efectivo
        public static DataTable getComprobacionEfectivo(int iNumEmpresa, int iNumUsuario, int iNumEgreso, int iNumCaja,
                                                        string sFecha, string sFechaF, decimal dImporte, decimal dImporteF)
        {

            DataTable dtDatos = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumUsuario", iNumUsuario));
            context.Parametros.Add(new SqlParameter("@iNumEgreso", iNumEgreso));
            context.Parametros.Add(new SqlParameter("@iNumCaja", iNumCaja));
            context.Parametros.Add(new SqlParameter("@sFecha", sFecha));
            context.Parametros.Add(new SqlParameter("@sFechaF", sFechaF));
            context.Parametros.Add(new SqlParameter("@dImporte", dImporte));
            context.Parametros.Add(new SqlParameter("@dImporteF", dImporteF));

            dtDatos = context.ExecuteProcedure("sp_Caja_Chica_Obten_Comprobacion", true).Copy();

            return dtDatos;
        }

        #endregion

        #region //CAJA05 Solicitud Reembolso

        //funcion que guarda la solicitud de reembolso para la caja
        public static DataTable GuardarSolicitudReembolso(int iTipoMovimiento, int iNumero_Empresa, int iNumCaja, int iNumero,
                                                        decimal dMonto, string sObsercacion, int iUsuario)
        {
            DataTable dtResultado = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@TipoMovimiento", iTipoMovimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Caja", iNumCaja));
            context.Parametros.Add(new SqlParameter("@Numero", iNumero));
            context.Parametros.Add(new SqlParameter("@Monto", dMonto));
            context.Parametros.Add(new SqlParameter("@Observacion", sObsercacion));
            context.Parametros.Add(new SqlParameter("@Usuario", iUsuario));

            dtResultado = context.ExecuteProcedure("sp_Caja_Chica_Reembolso", true).Copy();

            return dtResultado;
        }

        //funcion que guarda el detalle de la solicitud de reembolso
        public static DataTable GuardarSolicitudReembolsoDetalle(int iTipoMovimiento, int iNumero_Empresa, int iNumSolicitud,
                                                        int iNumEgreso, int iNumComprobancion)
        {
            DataTable dtResultado = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@TipoMovimiento", iTipoMovimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Solicitud", iNumSolicitud));
            context.Parametros.Add(new SqlParameter("@Numero_Egreso", iNumEgreso));
            context.Parametros.Add(new SqlParameter("@Numero_Comprobante", iNumComprobancion));

            dtResultado = context.ExecuteProcedure("sp_Caja_Chica_Reembolso_Detalle", true).Copy();

            return dtResultado;
        }

        //Consulta todos los datos de las Solicitudes de Reembolso
        public static DataTable getSolicitudReembolso(int iNumEmpresa, int iNumUsuario, int iNumCaja, int iNumSolicitud,
                                                        string sFecha, string sFechaF, decimal dImporte, decimal dImporteF, int iConsulta)
        {

            DataTable dtDatos = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumUsuario", iNumUsuario));
            context.Parametros.Add(new SqlParameter("@iNumCaja", iNumCaja));
            context.Parametros.Add(new SqlParameter("@iNumSolicitud", iNumSolicitud));
            context.Parametros.Add(new SqlParameter("@sFecha", sFecha));
            context.Parametros.Add(new SqlParameter("@sFechaF", sFechaF));
            context.Parametros.Add(new SqlParameter("@dImporte", dImporte));
            context.Parametros.Add(new SqlParameter("@dImporteF", dImporteF));
            context.Parametros.Add(new SqlParameter("@iConsulta", iConsulta));


            dtDatos = context.ExecuteProcedure("sp_Caja_Chica_Obten_Solicitud", true).Copy();

            return dtDatos;
        }

        #endregion

        #region //CAJA06 Dotacion de Efectivo

        //Marca los comprobantes seleccionados en la comprobación de efectivo
        public static DataTable marcarComprobantes(int iNumEmpresa, long iNumSolicitud, int iNumEgreso,
                                                    int iNumComprobacion, int iClasEstatus)
        {

            DataTable dtDatos = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumSolicitud", iNumSolicitud));
            context.Parametros.Add(new SqlParameter("@iNumEgreso", iNumEgreso));
            context.Parametros.Add(new SqlParameter("@iNumComprobacion", iNumComprobacion));
            context.Parametros.Add(new SqlParameter("@iClasEstatus", iClasEstatus));

            dtDatos = context.ExecuteProcedure("sp_Caja_Chica_Marcar_Comprobante", true).Copy();

            return dtDatos;
        }

        //Consultas para la pantalla Dotación de Efectivo
        public static DataTable CajaChicaConsultasDotacion(int iConsulta, int iNumEmpresa, int iNumSolicitud,
                                int iNumDotacion, long lNumConcepto, long lClasIVA, long lCaja, string sCve_TMP,
                                int? iNumPoliza1, string sFecha, int iPeriodo, int iNumUsuario)
        {

            DataTable dtDatos = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iConsulta", iConsulta));
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumSolicitud", iNumSolicitud));
            context.Parametros.Add(new SqlParameter("@iNumDotacion", iNumDotacion));
            context.Parametros.Add(new SqlParameter("@lNumConcepto", lNumConcepto));
            context.Parametros.Add(new SqlParameter("@lNumClasIVA", lClasIVA));
            context.Parametros.Add(new SqlParameter("@iNumCaja", lCaja));
            context.Parametros.Add(new SqlParameter("@sCve_TMP", sCve_TMP));
            context.Parametros.Add(new SqlParameter("@iNumPoliza1", iNumPoliza1.HasValue ? (object)iNumPoliza1.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@sFecha", sFecha));
            context.Parametros.Add(new SqlParameter("@iPeriodo", iPeriodo));
            context.Parametros.Add(new SqlParameter("@iNumUsuario", iNumUsuario));

            dtDatos = context.ExecuteProcedure("sp_Caha_Chica_Consultas_Dotacion_Efectivo", true).Copy();

            return dtDatos;
        }

        //Guarda la Contabilidad
        public static DataTable guardarContabilidadManual(int TipoMovimiento, int Numero_Empresa, int Numero,
                                int? Numero_Poliza, int Tipo_Poliza, long Numero_Cuenta, string Fecha,
                                string Concepto, decimal Debe, decimal Haber, int Usuario, int Periodo,
                                string Numero_Manual, string Titulo, int Clas_Moneda, decimal Tipo_Cambio,
                                int Bandera, string Folio_Fiscal)
        {

            DataTable dtDatos = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@TipoMovimiento", TipoMovimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero", Numero));
            context.Parametros.Add(new SqlParameter("@Numero_Poliza", Numero_Poliza.HasValue ? (object)Numero_Poliza.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Poliza", Tipo_Poliza));
            context.Parametros.Add(new SqlParameter("@Numero_Cuenta", Numero_Cuenta));
            context.Parametros.Add(new SqlParameter("@Fecha", Fecha));
            context.Parametros.Add(new SqlParameter("@Concepto", Concepto));
            context.Parametros.Add(new SqlParameter("@Debe", Debe));
            context.Parametros.Add(new SqlParameter("@Haber", Haber));
            context.Parametros.Add(new SqlParameter("@Usuario", Usuario));
            context.Parametros.Add(new SqlParameter("@Periodo", Periodo));
            context.Parametros.Add(new SqlParameter("@Numero_Manual", Numero_Manual));
            context.Parametros.Add(new SqlParameter("@Titulo", Titulo));
            context.Parametros.Add(new SqlParameter("@Clas_Moneda", Clas_Moneda));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", Tipo_Cambio));
            context.Parametros.Add(new SqlParameter("@Bandera", Bandera));
            context.Parametros.Add(new SqlParameter("@Folio_Fiscal", Folio_Fiscal));


            dtDatos = context.ExecuteProcedure("sp_Contabilidad_Manual_1", true).Copy();

            return dtDatos;
        }

        //llena todos los combos de la pantallas de Caja Dotación de Efectivo
        public static DataTable LlenarCombosDotacion(int iConsulta, int iNumEmpresa, int iNumUsuario, int iNumBanco)
        {

            DataTable dtDatos = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iConsulta", iConsulta));
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumUSuario", iNumUsuario));
            context.Parametros.Add(new SqlParameter("@iNumBanco", iNumBanco));

            dtDatos = context.ExecuteProcedure("sp_Caja_Chica_Dotacion_LlenaCombos", true).Copy();

            return dtDatos;
        }

        //Consulta todos los datos de Caja Dotación de Efectivo
        public static DataTable getConsultaDotacionEfectivo(int iNumEmpresa, int iNumUsuario, int iNumero, string sFecha, int iNumCaja, int iClasConcepto,
                                int iFormaDotacion, int iMoneda, decimal dImporte, int iNumBanco, int iClasCuenta, string sFolio, int iNumBeneficiario, string sFechaF)
        {

            DataTable dtDatos = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iNumEmpresa", iNumEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumUsuario", iNumUsuario));
            context.Parametros.Add(new SqlParameter("@iNumero", iNumero));
            context.Parametros.Add(new SqlParameter("@sFecha", sFecha));
            context.Parametros.Add(new SqlParameter("@iNumCaja", iNumCaja));
            context.Parametros.Add(new SqlParameter("@iClasConcepto", iClasConcepto));
            context.Parametros.Add(new SqlParameter("@iFormaDotacion", iFormaDotacion));
            context.Parametros.Add(new SqlParameter("@iMoneda", iMoneda));
            context.Parametros.Add(new SqlParameter("@dImporte", dImporte));
            context.Parametros.Add(new SqlParameter("@iNumBanco", iNumBanco));
            context.Parametros.Add(new SqlParameter("@iClasCuenta", iClasCuenta));
            context.Parametros.Add(new SqlParameter("@sFolio", sFolio));
            context.Parametros.Add(new SqlParameter("@iNumBeneficiario", iNumBeneficiario));
            context.Parametros.Add(new SqlParameter("@sFechaF", sFechaF));

            dtDatos = context.ExecuteProcedure("sp_Caja_Chica_Obten_Dotacion_Efectivo", true).Copy();

            return dtDatos;
        }

        #endregion
    }
}
