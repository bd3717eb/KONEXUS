using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;
using System.Data.SqlClient;

namespace IntegraBussines
{
    public class GastosBLL
    {
        public int Company { get; set; }
        public int Number { get; set; }
        public int Depto { get; set; }
        public int Seccion { get; set; }
        public int Puesto { get; set; }
        public int CEstatus { get; set; }
        public int? Auxiliar { get; set; }
        public int Class { get; set; }
        public int Family { get; set; }
        public int? Client { get; set; }
        public int? Provider { get; set; }
        public int Person { get; set; }
        public int Product { get; set; }
        public int CSucursal { get; set; }
        public int CZone { get; set; }
        public int NumberRC { get; set; }
        public int NumberSC { get; set; }
        public int NumberOC { get; set; }
        public int? FormPay { get; set; }
        public decimal? CantFormPay { get; set; }
        public decimal Iva { get; set; }
        public DateTime Date { get; set; }
        public int UnidadMedida { get; set; }
        public int? CCurrency { get; set; }
        public int? CAlmacen { get; set; }
        public int CConcepto { get; set; }
        public string SConcept { get; set; }
        public string Observations { get; set; }
        public int? Addres { get; set; }
        public string NameClient { get; set; }
        public decimal Subtotal { get; set; }
        public int Descuento { get; set; }
        public decimal Total { get; set; }
        public decimal Price { get; set; }
        public decimal Price2 { get; set; }
        public string ImporteLetra { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal Cantidad { get; set; }
        public int? ReferenceNumber { get; set; }
        public int? User { get; set; }
        public DateTime FechaCancelacion { get; set; }
        public string Causa { get; set; }
        public int? Cargo { get; set; }
        public decimal? Saldo { get; set; }
        public int? NumberCaja { get; set; }
        public int Currency { get; set; }
        public int? NumberBank { get; set; }
        public int? Account { get; set; }
        public int? MovimentBank { get; set; }
        public int? Credito { get; set; }
        public int? ConceptoCancel { get; set; }
        public int? UserCancel { get; set; }
        public int TipoCompra { get; set; }
        public int Compra { get; set; }
        public int TipoConcepto { get; set; }
        public int Concepto { get; set; }




        public static DataTable gfCuentasPagarConceptos(int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));

            dt = context.ExecuteProcedure("sp_CuentasPagarConceptos", true).Copy();
            return dt;
        }
        public static DataTable gfCuentasPagarCuentaBanco(int intEmpresa, int intNumeroBanco)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numero_empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("numero_banco", intNumeroBanco));

            dt = context.ExecuteProcedure("sp_cp_cuentabanco", true).Copy();
            return dt;
        }

        public static DataTable gfCuentasPagarDatosCuentaBanco(int intOpcion, int intEmpresa, int intNumeroBanco, int intNumeroCuenta)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("opcion", intOpcion));
            context.Parametros.Add(new SqlParameter("numero_empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("numero_banco", intNumeroBanco));
            context.Parametros.Add(new SqlParameter("numero_cuenta", intNumeroCuenta));

            dt = context.ExecuteProcedure("sp_cp_cuentaMultiplesPagos", true).Copy();
            return dt;
        }

        public static DataTable gfCuentasPagarConceptosProveedor(int iEmpresa, int iProveedor)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();//
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("numeroproveedor", iProveedor));

            dt = context.ExecuteProcedure("sp_CuentasPagarConceptosProveedor", true).Copy();
            return dt;
        }
        public static DataTable gfCuentasObtieneAuxiliar(int iEmpresa, int iConcepto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Concepto", iConcepto));

            dt = context.ExecuteProcedure("Sp_GastosObtieneAuxiliar", true).Copy();
            return dt;
        }
        public static DataTable gfCuentasTipoConceptos(int iEmpresa, int iConcepto, int? iMoneda, int? iPersona, decimal? dMonto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Concepto", iConcepto));
            context.Parametros.Add(new SqlParameter("Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("Persona", iPersona));
            context.Parametros.Add(new SqlParameter("Monto", dMonto));

            dt = context.ExecuteProcedure("Sp_GastosObtieneTipoGasto", true).Copy();
            return dt;
        }



        public static DataTable gfCuentasTipoConceptosProveedor(int iEmpresa, int iProveedor, int iConcepto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("numeroproveedor", iProveedor));
            context.Parametros.Add(new SqlParameter("numeroconcepto", iConcepto));

            dt = context.ExecuteProcedure("sp_CuentasPagarConceptosProveedorTipo", true).Copy();
            return dt;
        }
        public static DataTable gfCuentasGastoCostoConceptosProveedor(int iEmpresa, int iProveedor, int iConcepto, int iTipo)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("numeroproveedor", iProveedor));
            context.Parametros.Add(new SqlParameter("numeroconcepto", iConcepto));
            context.Parametros.Add(new SqlParameter("numerotipo", iTipo));

            dt = context.ExecuteProcedure("[sp_CuentasPagarConceptosProveedorGastoCosto]", true).Copy();
            return dt;
        }
        public static DataTable gfCuentasGastoCosto(int iEmpresa, int iConcepto, string sTemp, int? iMoneda, decimal? dMonto, int iTipoGasto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Concepto", iConcepto));
            context.Parametros.Add(new SqlParameter("sTemp", sTemp));
            context.Parametros.Add(new SqlParameter("Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("Monto", dMonto));
            context.Parametros.Add(new SqlParameter("Tipo_Gasto", iTipoGasto));

            dt = context.ExecuteProcedure("Sp_GastosObtieneGastoCosto", true).Copy();
            return dt;
        }
        public static DataTable gfConceptoBancos(int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));


            dt = context.ExecuteProcedure("Sp_GastosObtieneConcepto", true).Copy();
            return dt;
        }
        public static DataTable gfBancos(int iEmpresa, int iAuxiliar, int iCaso, int iMoneda, int iPersona, decimal dMonto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Auxiliar", iAuxiliar));
            context.Parametros.Add(new SqlParameter("Caso", iCaso));
            context.Parametros.Add(new SqlParameter("Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("Persona", iPersona));
            context.Parametros.Add(new SqlParameter("Monto", dMonto));

            dt = context.ExecuteProcedure("Sp_GastosObtieneBanco", true).Copy();
            return dt;
        }
        public static DataTable gfCuenta(int iEmpresa, int iMoneda, int iNumero_Banco, int iPersona, decimal dMonto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("Numero_Banco", iNumero_Banco));
            context.Parametros.Add(new SqlParameter("Persona", iPersona));
            context.Parametros.Add(new SqlParameter("Monto", dMonto));

            dt = context.ExecuteProcedure("Sp_GastosObtieneNumeroCuenta", true).Copy();
            return dt;
        }

        public static DataTable gfObtenClasificacionIva(int iIva)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@iva", iIva));

            dt = context.ExecuteProcedure("sp_CuentasPagarClasificacionIVA", true).Copy();
            return dt;
        }
        public static DataTable gfObtenNumeroTransaccionPrograma(int iNumero_Empresa, int iConcepto, int iIva)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Concepto", iConcepto));
            context.Parametros.Add(new SqlParameter("@Clas_iva", iIva));

            dt = context.ExecuteProcedure("sp_CuentasPagarVerificartransaccionPrograma", true).Copy();
            return dt;
        }

        public static DataTable gfObtenNumeroTransaccionProgramaParaPago(int iNumero_Empresa, int iConcepto, int iIva)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Concepto", iConcepto));
            context.Parametros.Add(new SqlParameter("@Clas_iva", iIva));

            dt = context.ExecuteProcedure("sp_CuentasPagarVerificartransaccionProgramaParaPago", true).Copy();
            return dt;
        }


        public static DataTable gfObtenDefinicionNumeroTransaccionPrograma(int iNumero_Empresa, int iNumeroTransaccion)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Transaccion", iNumeroTransaccion));


            dt = context.ExecuteProcedure("sp_CuentasPagarVerificarDefinicionTransaccion", true).Copy();
            return dt;
        }

        public static DataTable gfObtenDefinicionNumeroTransaccionProgramaParaPago(int iNumero_Empresa, int iNumeroTransaccion)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Transaccion", iNumeroTransaccion));


            dt = context.ExecuteProcedure("sp_CuentasPagarVerificarDefinicionTransaccionParaPago", true).Copy();
            return dt;
        }
        public static DataTable gfsp_Cuentas_Pagar(int iTipo_Movimiento, int? iNumero, int iNumero_Empresa, int iNumero_Proveedor, int iNumero_Concepto, string sDocumento,
                                            string sNumero_Referecia, DateTime Fecha_Aplicacion, decimal dMonto, decimal? dCargo, decimal? dAbono, decimal? dSaldo,
                                            int iPersona, int iMoneda, int? iBanco, int? iCuenta, int iTipo_Persona, int? iNum_Caja, int iClas_Iva, int? iClas_Gasto,
                                            int? iClas_Tipo_Gasto, decimal? dTipoCambio, int? Clas_CCosto1, int? Clas_CCosto2, string sFolio_Fiscal, int? iNumeroBancoMovimiento, int? iPolizaManual)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@TipoMovimiento", iTipo_Movimiento));
            context.Parametros.Add(new SqlParameter("@Numero", iNumero));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Proveedor", iNumero_Proveedor));
            context.Parametros.Add(new SqlParameter("@Numero_Concepto", iNumero_Concepto));
            context.Parametros.Add(new SqlParameter("@Documento", sDocumento));
            context.Parametros.Add(new SqlParameter("@Numero_Referencia", sNumero_Referecia));
            context.Parametros.Add(new SqlParameter("@Fecha_Aplicacion", Fecha_Aplicacion));
            context.Parametros.Add(new SqlParameter("@Fecha_Vencimiento", Fecha_Aplicacion));
            context.Parametros.Add(new SqlParameter("@Monto", dMonto));
            context.Parametros.Add(new SqlParameter("@Cargo", dCargo.HasValue ? (object)dCargo.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Abono", dAbono.HasValue ? (object)dAbono.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Saldo", dSaldo.HasValue ? (object)dSaldo.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", iPersona));
            context.Parametros.Add(new SqlParameter("@Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("@Num_Banco", iBanco.HasValue ? (object)iBanco.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Num_Cuenta", iCuenta.HasValue ? (object)iCuenta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Persona", iTipo_Persona));
            context.Parametros.Add(new SqlParameter("@Num_Caja", iNum_Caja.HasValue ? (object)iNum_Caja.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_IVA", iClas_Iva));
            context.Parametros.Add(new SqlParameter("@Clas_Gasto", iClas_Gasto.HasValue ? (object)iClas_Gasto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_Tipo_Gasto", iClas_Tipo_Gasto.HasValue ? (object)iClas_Tipo_Gasto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", dTipoCambio.HasValue ? (object)dTipoCambio.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_Centro_Costo", Clas_CCosto1));
            context.Parametros.Add(new SqlParameter("@Clas_CC", Clas_CCosto2));
            context.Parametros.Add(new SqlParameter("@Prorrateo", null));
            context.Parametros.Add(new SqlParameter("@TC_Mes", null));
            context.Parametros.Add(new SqlParameter("@Referencia_Texto", " "));
            context.Parametros.Add(new SqlParameter("@Descripcion", " "));
            context.Parametros.Add(new SqlParameter("@Numero_CProyecto", null));
            context.Parametros.Add(new SqlParameter("@Concepto_Cancelacion", null));
            context.Parametros.Add(new SqlParameter("@Usuario_Cancelacion", null));
            context.Parametros.Add(new SqlParameter("@Fecha_Revision", null));
            context.Parametros.Add(new SqlParameter("@Folio_FIscal", sFolio_Fiscal));
            context.Parametros.Add(new SqlParameter("@Numero_Movimiento_Banco", iNumeroBancoMovimiento));
            context.Parametros.Add(new SqlParameter("@Poliza_Manual", iPolizaManual.HasValue ? (object)iPolizaManual.Value : DBNull.Value));

            dt = context.ExecuteProcedure("[sp_Cuentas_Pagar2]", true).Copy();
            if (dt != null)
                return dt;
            else
                return new DataTable();


        }
        public static DataTable gfObtenContClasProveedor(int iNumero_Empresa, int iPersona)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Persona", iPersona));


            dt = context.ExecuteProcedure("sp_CuentasPagarContClasProveedor", true).Copy();
            return dt;
        }
        public static DataTable gfObtenNumeroReferencia(int iNumero_Empresa, int iNumeroCuentasPagar)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Cuentas_Pagar", iNumeroCuentasPagar));


            dt = context.ExecuteProcedure("sp_CuentasPagarNumeroReferencia", true).Copy();
            return dt;
        }

        public static DataTable gfObtenNombreCortoAcreedor(int iNumero_Empresa, int iNumero_Persona)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Persona", iNumero_Persona));


            dt = context.ExecuteProcedure("sp_CuentasPagarNombreCorto", true).Copy();
            return dt;
        }
        public static DataTable gfsp_Bancos_Registro_Movimiento_Caja(int iTipo_Movimiento, int iNumero_Empresa, int? iNumero, int? iNumero_Banco, string sCuenta, DateTime Fecha,
                                                                     string sFolio, decimal dEfectivo, decimal? dDocumentos, int iClas_Concepto, int iBeneficiario,
                                                                     int iTipo, int iClas_Cuenta, int iPersona, int? iNumero_CC, int iCheque, decimal dTipoCambio)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@TipoMovimiento", iTipo_Movimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero", iNumero));
            context.Parametros.Add(new SqlParameter("@Numero_Banco", iNumero_Banco.HasValue ? (object)iNumero_Banco.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Cuenta", sCuenta));
            context.Parametros.Add(new SqlParameter("@Fecha", Fecha));
            context.Parametros.Add(new SqlParameter("@Folio", sFolio));
            context.Parametros.Add(new SqlParameter("@Efectivo", dEfectivo));
            context.Parametros.Add(new SqlParameter("@Documentos", dDocumentos.HasValue ? (object)dDocumentos.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_Concepto", iClas_Concepto));
            context.Parametros.Add(new SqlParameter("@Beneficiario", iBeneficiario));
            context.Parametros.Add(new SqlParameter("@Comentario", ""));
            context.Parametros.Add(new SqlParameter("@Tipo", iTipo));
            context.Parametros.Add(new SqlParameter("@Saldo_Inicial", dDocumentos.HasValue ? (object)dDocumentos.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Saldo_Final", dDocumentos.HasValue ? (object)dDocumentos.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_Cuenta", iClas_Cuenta));
            context.Parametros.Add(new SqlParameter("@Clas_Facultad", 322));
            context.Parametros.Add(new SqlParameter("@Usuario_Reg", iPersona));
            context.Parametros.Add(new SqlParameter("@Nombre_Beneficiario", null));
            context.Parametros.Add(new SqlParameter("@Numero_CC", iNumero_CC));
            context.Parametros.Add(new SqlParameter("@Retiro_Cheque_Transafer", iCheque));
            context.Parametros.Add(new SqlParameter("@Registro_Externo", 0));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", dTipoCambio));
            context.Parametros.Add(new SqlParameter("@Fecha_Cancelacion", null));
            context.Parametros.Add(new SqlParameter("@Causa_Cancelacion", null));


            dt = context.ExecuteProcedure("[sp_Bancos_Registro_Movimiento_Caja2]", true).Copy();
            if (dt != null)
                return dt;
            else
                return new DataTable();

        }

        public static DataTable gfVerificaFolio(int Numero_Empresa, string sFolio_Fiscal)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            string sQuery = "SELECT dbo.fn_Verifica_Folios_Fiscales_v2(" + Numero_Empresa + "," + " '" + sFolio_Fiscal + "' " + ") AS Existe";
            dt = context.ExecuteQuery(sQuery).Copy();
            return dt;
        }
        public static DataTable gfVerificaFolioCXC(int Numero_Empresa, string sFolio_Fiscal)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            string sQuery = "SELECT dbo.fn_Verifica_Folios_Fiscales_CxC_v2(" + Numero_Empresa + "," + " '" + sFolio_Fiscal + "' " + ") AS Existe";
            dt = context.ExecuteQuery(sQuery).Copy();
            return dt;
        }
        public static DataTable gfVericaRfcEmisor(int iEmpresa, string sRfcEmisor)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("@RFCemisor", sRfcEmisor));


            dt = context.ExecuteProcedure("sp_CxC_VerificaRFCDelEmisor", true).Copy();
            return dt;
        }


        public static DataTable gfNombreEmpresa(int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));


            dt = context.ExecuteProcedure("sp_CFDI_ObtieneDatosDelEmisor_v2", true).Copy();
            return dt;
        }

        public static DataTable gfObtenNumeroCheque(int iNumero_Empresa, int iNumero_Concepto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Concepto", iNumero_Concepto));


            dt = context.ExecuteProcedure("sp_CuentasPagarNumeroCheque", true).Copy();
            return dt;
        }

        public static DataTable gfObtenMoneda(int iNumero_Empresa, string sDescripcionMoneda)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            string sQuery = "SELECT dbo.fn_ObtenMoneda(" + iNumero_Empresa + "," + " '" + sDescripcionMoneda + "' " + ") AS Moneda";
            dt = context.ExecuteQuery(sQuery).Copy();
            return dt;
        }

        // ///// PAGOS MULTIPLES GASTOS /////////////

        public static DataTable gfObtenFacturasProveedor(int iNumero_Empresa, int iPersona, int? iProveedor, DateTime fechade, DateTime fechahasta, int iMoneda)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", iPersona));
            context.Parametros.Add(new SqlParameter("@Numero_Proveedor", iProveedor.HasValue ? (object)iProveedor.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_De", fechade));
            context.Parametros.Add(new SqlParameter("@Fecha_Hasta", fechahasta));
            context.Parametros.Add(new SqlParameter("@Clas_Moneda", iMoneda));

            dt = context.ExecuteProcedure("sp_cp_Obten_Facturas_Proveedor", true).Copy();
            return dt;
        }

        public static DataTable gfCuentasPagarConceptosPagoMult(int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));

            dt = context.ExecuteProcedure("sp_CuentasPagarConceptosPagoMult", true).Copy();
            return dt;
        }


        public static DataTable gfCuentasCobrarConceptosPagoMult(int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));

            dt = context.ExecuteProcedure("sp_CuentasCobrarConceptosPagoMult", true).Copy();
            return dt;
        }


        public static DataTable gfCuentasPagarConceptosPagoIndividual(int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));

            dt = context.ExecuteProcedure("sp_CuentasPagarConceptosPagoIndividual", true).Copy();
            return dt;
        }

        public static DataTable gfCuentasCobrarConceptosPagoIndividual(int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));

            dt = context.ExecuteProcedure("sp_CuentasCobrarConceptosPagoIndividual", true).Copy();
            return dt;
        }
        public static DataTable gfBancosPagoMult(int iEmpresa, int iMoneda, int iPersona, decimal dMonto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("Persona", iPersona));
            context.Parametros.Add(new SqlParameter("Monto", dMonto));

            dt = context.ExecuteProcedure("Sp_GastosObtieneBancoPagoMult", true).Copy();
            return dt;
        }
        public static DataTable gfCuentaPagoMult(int iEmpresa, int iMoneda, int iPersona, int iNumero_Banco, decimal dMonto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("Persona", iPersona));
            context.Parametros.Add(new SqlParameter("Banco", iNumero_Banco));
            context.Parametros.Add(new SqlParameter("Monto", dMonto));

            dt = context.ExecuteProcedure("Sp_GastosObtieneNumeroCuentaPagoMult", true).Copy();
            return dt;
        }

        public static DataTable gfCuentasObtieneAuxiliarCobrarConceptos(int iEmpresa, int iConcepto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Concepto", iConcepto));

            dt = context.ExecuteProcedure("Sp_GastosObtieneAuxiliarCobrarConceptos", true).Copy();
            return dt;
        }

        public static DataTable gfCuentasObtieneAuxiliarPagarConceptos(int iEmpresa, int iConcepto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Numero_Concepto", iConcepto));

            dt = context.ExecuteProcedure("sp_Proveedor_Conceptos_Auxiliar", true).Copy();
            return dt;
        }



        public static bool DeleteProcesoPago(int iNumero_Empresa, int iNumero_Usuario)
        {

            int iResultado = 0;
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", iNumero_Usuario));

            iResultado = context.ExecuteNonQuery("sp_cc_Proceso_PagoBorrado", true);
            if (iResultado >= 0)
                return true;
            else
                return false;
        }
        public static bool DeleteProcesoPagoCXP(int iNumero_Empresa, int iNumero_Usuario)
        {

            int iResultado = 0;
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", iNumero_Usuario));

            iResultado = context.ExecuteNonQuery("sp_cp_Proceso_PagoBorrado", true);
            if (iResultado >= 0)
                return true;
            else
                return false;
        }
        public static DataTable sp_Cuentas_Cobrar_Alta_CFDI(int? iNumero, int iNumero_Empresa, string sRfcCliente, string sNombreCliente, string sSerie, string sFolio, DateTime Fecha,
                                                           string sRfcEmisor, decimal dMonto, string sMoneda, decimal dTipoCambio, decimal dTasaIva, string sFolioFiscal, int iGenerarCobro,
                                                           DateTime? FechaCobro, string sFolioCobro, decimal? dMontoCobro, int? iMonedaCobro, decimal? dTipoCambioCobro, int iUsuario,
                                                           int? iSucursal)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero", iNumero));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@RFC_Cliente", sRfcCliente));
            context.Parametros.Add(new SqlParameter("@Nombre_Cliente", sNombreCliente));
            context.Parametros.Add(new SqlParameter("@Serie", sSerie));
            context.Parametros.Add(new SqlParameter("@Folio", sFolio));
            context.Parametros.Add(new SqlParameter("@Fecha", Fecha));
            context.Parametros.Add(new SqlParameter("@RFC_Emisor", sRfcEmisor));
            context.Parametros.Add(new SqlParameter("@Monto", dMonto));
            context.Parametros.Add(new SqlParameter("@Moneda", sMoneda));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", dTipoCambio));
            context.Parametros.Add(new SqlParameter("@Tasa_IVA", dTasaIva));
            context.Parametros.Add(new SqlParameter("@Folio_Fiscal", sFolioFiscal));
            context.Parametros.Add(new SqlParameter("@Generar_Cobro", iGenerarCobro));
            context.Parametros.Add(new SqlParameter("@Fecha_Cobro", FechaCobro.HasValue ? (object)FechaCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Folio_Cobro", sFolioCobro));
            context.Parametros.Add(new SqlParameter("@Monto_Cobro", dMontoCobro.HasValue ? (object)dMontoCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Moneda_Cobro", iMonedaCobro.HasValue ? (object)iMonedaCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio_Cobro", dTipoCambioCobro.HasValue ? (object)dTipoCambioCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("@Sucursal", iSucursal.HasValue ? (object)iSucursal.Value : DBNull.Value));



            dt = context.ExecuteProcedure("[sp_Cuentas_Cobrar_Alta_CFDI]", true).Copy();

            return dt;


        }

        public static DataTable sp_Cuentas_Cobrar_Alta_CFDI2(int? iNumero, int iNumero_Empresa, string sRfcCliente, string sNombreCliente, string sSerie, string sFolio, DateTime Fecha,
                                                   string sRfcEmisor, decimal dMonto, string sMoneda, decimal dTipoCambio, decimal dTasaIva, string sFolioFiscal, int iGenerarCobro,
                                                   DateTime? FechaCobro, string sFolioCobro, decimal? dMontoCobro, int? iMonedaCobro, decimal? dTipoCambioCobro, int iUsuario,
                                                   int? iSucursal, decimal? dMontoIva, decimal? dMontoSubtotal)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero", iNumero));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@RFC_Cliente", sRfcCliente));
            context.Parametros.Add(new SqlParameter("@Nombre_Cliente", sNombreCliente));
            context.Parametros.Add(new SqlParameter("@Serie", sSerie));
            context.Parametros.Add(new SqlParameter("@Folio", sFolio));
            context.Parametros.Add(new SqlParameter("@Fecha", Fecha));
            context.Parametros.Add(new SqlParameter("@RFC_Emisor", sRfcEmisor));
            context.Parametros.Add(new SqlParameter("@Monto", dMonto));
            context.Parametros.Add(new SqlParameter("@Moneda", sMoneda));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", dTipoCambio));
            context.Parametros.Add(new SqlParameter("@Tasa_IVA", dTasaIva));
            context.Parametros.Add(new SqlParameter("@Folio_Fiscal", sFolioFiscal));
            context.Parametros.Add(new SqlParameter("@Generar_Cobro", iGenerarCobro));
            context.Parametros.Add(new SqlParameter("@Fecha_Cobro", FechaCobro.HasValue ? (object)FechaCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Folio_Cobro", sFolioCobro));
            context.Parametros.Add(new SqlParameter("@Monto_Cobro", dMontoCobro.HasValue ? (object)dMontoCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Moneda_Cobro", iMonedaCobro.HasValue ? (object)iMonedaCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio_Cobro", dTipoCambioCobro.HasValue ? (object)dTipoCambioCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("@Sucursal", iSucursal.HasValue ? (object)iSucursal.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Monto_IVA", dMontoIva.HasValue ? (object)dMontoIva.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Monto_Subtotal", dMontoSubtotal.HasValue ? (object)dMontoSubtotal.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_Cuentas_Cobrar_Alta_CFDI]", true).Copy();

            return dt;


        }

        public static bool gfsp_cc_proceso_pago_scf(int iTipo_Movimiento, int iNumero_Empresa, int iNumeroUsuario, int iClas_Pantalla, string sDescripcion,
                                                         decimal dImporte, decimal dTipoCambio, int? iNumeroConcepto, int? iTipo_Conta, int? iTipo_Costo_Gasto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Tipo", iTipo_Movimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", iNumeroUsuario));
            context.Parametros.Add(new SqlParameter("@Clas_Pantalla", iClas_Pantalla));
            context.Parametros.Add(new SqlParameter("@Descripcion", sDescripcion));
            context.Parametros.Add(new SqlParameter("@Importe", dImporte));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", dTipoCambio));
            context.Parametros.Add(new SqlParameter("@Numero_Concepto", iNumeroConcepto.HasValue ? (object)iNumeroConcepto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Conta", iTipo_Conta.HasValue ? (object)iTipo_Conta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Costo_Gasto", iTipo_Costo_Gasto.HasValue ? (object)iTipo_Costo_Gasto.Value : DBNull.Value));

            dt = context.ExecuteProcedure("sp_cc_proceso_pago_scf", true).Copy();
            if (dt != null)
                return true;
            else
                return false;


        }




        public static DataTable ObtienedatosCXP(int iNumero_Empresa, DateTime? FechaIni, DateTime? FechaFin)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Fecha_Ini", FechaIni.HasValue ? (object)FechaIni.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Fin", FechaFin.HasValue ? (object)FechaFin.Value : DBNull.Value));


            dt = context.ExecuteProcedure("sp_cuentas_pagar_preregistro_consulta", true).Copy();
            return dt;
        }


        //CANCELACIÓN GASTOS CUENTAS PAGAR 

        public static DataTable sp_CP_Pagos_Consulta(int iEmpresa, int iNumeroProveedor, int iMoneda, DateTime? FechaIni, DateTime? FechaFin)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("@Num_Persona", iNumeroProveedor));
            context.Parametros.Add(new SqlParameter("@Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("@Fecha_Ini", FechaIni.HasValue ? (object)FechaIni.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Fin", FechaFin.HasValue ? (object)FechaFin.Value : DBNull.Value));


            dt = context.ExecuteProcedure("sp_CP_Pagos_Consulta", true).Copy();

            return dt;

        }

        public static DataTable gfObtenConceptosParaTransaccion(int iNumero_Empresa, int iNumeroPago)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Pago", iNumeroPago));


            dt = context.ExecuteProcedure("sp_cp_Conceptos_Valida_Transaccion", true).Copy();
            return dt;
        }

        public static DataTable CancelaPagoCP(int iNumero_Empresa, int iUsuario, int iClas_Pantalla, int iNumeroPago, int iTipoContabilidad, int iConceptoCancelacion, string sCausaCancelacion, DateTime FechaCancelacion)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("@Clas_Pantalla", iClas_Pantalla));
            context.Parametros.Add(new SqlParameter("@Numero_Pago", iNumeroPago));
            context.Parametros.Add(new SqlParameter("@Tipo_Contabilidad", iTipoContabilidad));
            context.Parametros.Add(new SqlParameter("@Concepto_Cancelacion", iConceptoCancelacion));
            context.Parametros.Add(new SqlParameter("@Causa_Cancelacion", sCausaCancelacion));
            context.Parametros.Add(new SqlParameter("@Fecha_Cancelacion", FechaCancelacion));

            dt = context.ExecuteProcedure("sp_cp_Reversa_Pago_Gral", true).Copy();
            return dt;
        }



        //02/03/2015
        public static DataTable CancelaCuentaporPagar(int iNumero_Empresa, int iUsuario, int? iConceptoCancelacion, DateTime? FechaAplicacion, string sCausaCancelacion, int? iNumeroTipoMovimiento,
                                                      int? iCliente, int? iConcepto, int? iTipoCliente, string sDescripcionConcepto, int? iNumeroCuenta,
                                                      decimal? dTipoCambio, DateTime? FechaCancelacion)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@iNumPersona", iUsuario));
            context.Parametros.Add(new SqlParameter("@lConcepto_Cancelacion", iConceptoCancelacion));
            context.Parametros.Add(new SqlParameter("@dtpVencimiento", FechaAplicacion));
            context.Parametros.Add(new SqlParameter("@Causa_Cancelacion", sCausaCancelacion));
            context.Parametros.Add(new SqlParameter("@iNumero_Tipo_Movimiento", iNumeroTipoMovimiento));
            context.Parametros.Add(new SqlParameter("@iCliente", iCliente));
            //context.Parametros.Add(new SqlParameter("@Tipo_Movimiento", sDescripcionMovimiento));
            context.Parametros.Add(new SqlParameter("@iConcepto", iConcepto));
            //context.Parametros.Add(new SqlParameter("@lFactura", iFactura));
            context.Parametros.Add(new SqlParameter("@Num_Cliente", iTipoCliente));
            context.Parametros.Add(new SqlParameter("@Des_Concepto", sDescripcionConcepto));
            context.Parametros.Add(new SqlParameter("@cboNumCuenta", iNumeroCuenta.HasValue ? (object)iNumeroCuenta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", dTipoCambio));
            context.Parametros.Add(new SqlParameter("@dtpFecha", FechaCancelacion));



            dt = context.ExecuteProcedure("sp_Eliminar_Cuentas_Pagar", true).Copy();
            return dt;
        }

        public static DataTable TraeDatosXml(int iNumero_Empresa, DateTime? FechaIni, DateTime? FechaFin)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Fecha_Ini", FechaIni.HasValue ? (object)FechaIni.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Fin", FechaFin.HasValue ? (object)FechaFin.Value : DBNull.Value));


            dt = context.ExecuteProcedure("sp_cuentas_pagar_preregistro_consulta", true).Copy();
            return dt;
        }

        //CUENTAS PAGAR CANCELACIÓN
        public static DataTable gfObtenFacturascxpProveedor(int iNumero_Empresa, int? iProveedor, string sDocumento, DateTime? fechade, DateTime? fechahasta, int? iMoneda, decimal? dmontoInicial, decimal? dmontoFinal)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Proveedor", iProveedor.HasValue ? (object)iProveedor.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Moneda", iMoneda.HasValue ? (object)iMoneda.Value : DBNull.Value));
            //context.Parametros.Add(new SqlParameter("@Docto", (string.IsNullOrEmpty(sDocumento) ? null : sDocumento)));

            if (sDocumento == null || sDocumento == "")
            {
                context.Parametros.Add(new SqlParameter("@Docto", DBNull.Value));
            }
            else
            {
                context.Parametros.Add(new SqlParameter("@Docto", sDocumento.ToString()));
            }

            context.Parametros.Add(new SqlParameter("@Fecha_Ini", fechade.HasValue ? (object)fechade.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Fin", fechahasta.HasValue ? (object)fechahasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Monto_Ini", dmontoInicial.HasValue ? (object)dmontoInicial.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Monto_Fin", dmontoFinal.HasValue ? (object)dmontoFinal.Value : DBNull.Value));

            dt = context.ExecuteProcedure("sp_CXP_Facturas_Filtro", true).Copy();
            return dt;
        }

        //Metodo para realizar la insercion de datos de la poliza ultmios Cambios

        public static bool RegistraPolizaDiaria(int numEmpresa, DateTime fecha, string UUID)
        {
            bool bandera = true;

            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
                context.Parametros.Add(new SqlParameter("@Fecha_Poliza", fecha));
                context.Parametros.Add(new SqlParameter("@UUID", UUID));

                context.ExecuteProcedure("sp_Alta_Poliza_Diaria", true).Copy();

            }
            catch
            {
                bandera = false;
            }

            return bandera;
        }

        //-------------------------------------------------------------------------------------

        //public static int ContabilidadManual(int iMovimento, int iEmpresa, int iNumero, int? iNumeroPoliza, int? iTipoPoliza,
        //                                           int iNumeroCuenta, DateTime Fecha, string sConcepto, decimal? Debe, decimal? Haber,
        //                                           int intPersona, int intPeriodo, int intMoneda, decimal dTipoCambio, string sFolioFiscal)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //      new SqlParameter("TipoMovimiento", iMovimento),
        //      new SqlParameter("Numero_Empresa", iEmpresa),
        //      new SqlParameter("Numero", iNumero),
        //      new SqlParameter("Numero_Poliza", iNumeroPoliza.HasValue ? (object)iNumeroPoliza.Value : DBNull.Value),
        //      new SqlParameter("Tipo_Poliza", iTipoPoliza.HasValue ? (object)iTipoPoliza.Value : DBNull.Value),
        //      new SqlParameter("Numero_Cuenta", iNumeroCuenta),
        //      new SqlParameter("Fecha", Fecha),
        //      new SqlParameter("Concepto", sConcepto),
        //      new SqlParameter("Debe", Debe.HasValue ? (object)Debe.Value : DBNull.Value),
        //      new SqlParameter("Haber", Haber.HasValue ? (object)Haber.Value : DBNull.Value),
        //      new SqlParameter("Usuario", intPersona),
        //      new SqlParameter("Periodo", intPeriodo),
        //      new SqlParameter("Numero_Manual", DBNull.Value),
        //      new SqlParameter("Titulo", sConcepto),
        //      new SqlParameter("Clas_Moneda", intMoneda),
        //      new SqlParameter("Tipo_Cambio", dTipoCambio),
        //      new SqlParameter("Folio_Fiscal", sFolioFiscal),

        //    };

        //    try
        //    {
        //        if (parameters != null)
        //        {
        //            foreach (SqlParameter param in parameters)
        //            {
        //                if (param.ParameterName == "Numero" && iNumero == 0)
        //                {
        //                    param.Direction = ParameterDirection.Output;
        //                }
        //            }
        //        }

        //        return sqlDBHelper.ExecuteNonQueryOutputValue("sp_Contabilidad_Manual", "Numero", CommandType.StoredProcedure, parameters);

        //    }
        //    catch (Exception)
        //    {

        //        return -1;
        //    }



        //}


        public static int ContabilidadManual(int iMovimento, int iEmpresa, int? iNumero, int? iNumeroPoliza, int? iTipoPoliza,
                                                       int? iNumeroCuenta, DateTime? Fecha, string sConcepto, decimal? Debe, decimal? Haber,
                                                       int intPersona, int? intPeriodo, int? intMoneda, decimal? dTipoCambio, string sFolioFiscal)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
              new SqlParameter("TipoMovimiento", iMovimento),
              new SqlParameter("Numero_Empresa", iEmpresa),
              new SqlParameter("Numero", iNumero.HasValue ? (object)iNumero.Value : DBNull.Value),
              new SqlParameter("Numero_Poliza", iNumeroPoliza.HasValue ? (object)iNumeroPoliza.Value : DBNull.Value),
              new SqlParameter("Tipo_Poliza", iTipoPoliza.HasValue ? (object)iTipoPoliza.Value : DBNull.Value),
              new SqlParameter("Numero_Cuenta", iNumeroCuenta.HasValue ? (object)iNumeroCuenta.Value : DBNull.Value),
              new SqlParameter("Fecha", Fecha.HasValue ? (object)Fecha.Value : DBNull.Value),
              new SqlParameter("Concepto", sConcepto),
              new SqlParameter("Debe", Debe.HasValue ? (object)Debe.Value : DBNull.Value),
              new SqlParameter("Haber", Haber.HasValue ? (object)Haber.Value : DBNull.Value),
              new SqlParameter("Usuario", intPersona),
              new SqlParameter("Periodo", intPeriodo.HasValue ? (object)intPeriodo.Value : DBNull.Value),
              new SqlParameter("Numero_Manual", DBNull.Value),
              new SqlParameter("Titulo", sConcepto),
              new SqlParameter("Clas_Moneda", intMoneda.HasValue ? (object)intMoneda.Value : DBNull.Value),
              new SqlParameter("Tipo_Cambio", dTipoCambio.HasValue ? (object)dTipoCambio.Value : DBNull.Value),
              new SqlParameter("Folio_Fiscal", sFolioFiscal),
            
            };

            try
            {
                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        if (param.ParameterName == "Numero" && iNumero == 0)
                        {
                            param.Direction = ParameterDirection.Output;
                        }
                    }
                }

                return sqlDBHelper.ExecuteNonQueryOutputValue("sp_Contabilidad_Manual", "Numero", CommandType.StoredProcedure, parameters);

            }
            catch (Exception)
            {

                return -1;
            }



        }



        public static DataTable ContabilidadManualPaso(int iEmpresa, int? iNumeroPoliza, int? iTipoPoliza,
                                                   int iNumeroCuenta, DateTime Fecha, string sConcepto, decimal? Debe, decimal? Haber,
                                                   int iPersona, int intPeriodo, int intMoneda, decimal dTipoCambio, string sFolioFiscal, int intPolizaPago, string sTitulo)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Numero_Poliza", iNumeroPoliza.HasValue ? (object)iNumeroPoliza.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Tipo_Poliza", iTipoPoliza.HasValue ? (object)iTipoPoliza.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Numero_Cuenta", iNumeroCuenta));
            context.Parametros.Add(new SqlParameter("Fecha", Fecha));
            context.Parametros.Add(new SqlParameter("Concepto", sConcepto));
            context.Parametros.Add(new SqlParameter("Debe", Debe.HasValue ? (object)Debe.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Haber", Haber.HasValue ? (object)Haber.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Usuario", iPersona));
            context.Parametros.Add(new SqlParameter("Periodo", intPeriodo));
            context.Parametros.Add(new SqlParameter("Numero_Manual", DBNull.Value));
            context.Parametros.Add(new SqlParameter("Titulo", sTitulo));
            context.Parametros.Add(new SqlParameter("Clas_Moneda", intMoneda));
            context.Parametros.Add(new SqlParameter("Tipo_Cambio", dTipoCambio));
            context.Parametros.Add(new SqlParameter("Folio_Fiscal", sFolioFiscal));
            context.Parametros.Add(new SqlParameter("Poliza_Pago", intPolizaPago));



            dt = context.ExecuteProcedure("sp_cp_Proceso_ContabilidadIndividual", true).Copy();
            return dt;
        }

        public static DataTable ObtenFacturasClientePolizaManual(int iEmpresa, int iUsuario, int iTipo, int? iCliente, DateTime Fechaini, DateTime FechaFin, int iMoneda, int? iSucursal)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Numero_Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("Tipo", iTipo));
            context.Parametros.Add(new SqlParameter("Numero_Cliente", iCliente.HasValue ? (object)iCliente.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Fecha_De", Fechaini));
            context.Parametros.Add(new SqlParameter("Fecha_Hasta", FechaFin));
            context.Parametros.Add(new SqlParameter("Clas_Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("Clas_Sucursal", iSucursal.HasValue ? (object)iSucursal.Value : DBNull.Value));


            dt = context.ExecuteProcedure("sp_cc_Obten_Facturas_ClienteFolioFiscalPolizaManual", true).Copy();
            return dt;
        }

        public static bool InsertaContabilidadManual(int iEmpresa, int iPersona, int intPagar, int? intMovimientoPago, int? intBanco, string sCuenta, DateTime? Fecha, decimal? dEfectivo,
                                                          string sDocumentos, decimal? dSaldoInicial, decimal? dSaldoFinal, int? intNumeroProveedor, string sComentario,
                                                          int? intNumCuenta, int? intCheque, int? intNumConcepto, int? intClasIva)
        {
            try
            {
                //DataTable dt = new DataTable();
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
                context.Parametros.Add(new SqlParameter("Numero_Usuario", iPersona));
                context.Parametros.Add(new SqlParameter("Pago", intPagar));
                context.Parametros.Add(new SqlParameter("NumeroMovimiento", intMovimientoPago.HasValue ? (object)intMovimientoPago.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Numero_Banco", intBanco.HasValue ? (object)intBanco.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Des_Cuenta", sCuenta));
                context.Parametros.Add(new SqlParameter("Fecha_Aplicacion", Fecha.HasValue ? (object)Fecha.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Efectivo", dEfectivo.HasValue ? (object)dEfectivo.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Documento", sDocumentos));
                context.Parametros.Add(new SqlParameter("SaldoInicial", dSaldoInicial.HasValue ? (object)dSaldoInicial.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("SaldoFinal", dSaldoFinal.HasValue ? (object)dSaldoFinal.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Numero_Proveedor", intNumeroProveedor.HasValue ? (object)intNumeroProveedor.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Comentario", sComentario));
                context.Parametros.Add(new SqlParameter("Num_Cuenta", intNumCuenta.HasValue ? (object)intNumCuenta.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Cheque", intCheque.HasValue ? (object)intCheque.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Num_Concepto", intNumConcepto.HasValue ? (object)intNumConcepto.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Clas_Iva", intClasIva.HasValue ? (object)intClasIva.Value : DBNull.Value));
               
                context.ExecuteProcedure("sp_cp_Conta_ManualPago", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool InsertaContabilidadManualPolizasIndividuales(int iEmpresa, int iPersona, int intPagar, int? intMovimientoPago, int? intBanco, string sCuenta, DateTime? Fecha, decimal? dEfectivo,
                                                        string sDocumentos, decimal? dSaldoInicial, decimal? dSaldoFinal, int? intNumeroProveedor, string sComentario,
                                                        int? intNumCuenta, int? intCheque, int? intNumConcepto, int? intClasIva, int? intAcreedor, decimal? dMonto)
        {
            try
            {
                //DataTable dt = new DataTable();
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
                context.Parametros.Add(new SqlParameter("Numero_Usuario", iPersona));
                context.Parametros.Add(new SqlParameter("Pago", intPagar));
                context.Parametros.Add(new SqlParameter("NumeroMovimiento", intMovimientoPago.HasValue ? (object)intMovimientoPago.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Numero_Banco", intBanco.HasValue ? (object)intBanco.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Des_Cuenta", sCuenta));
                context.Parametros.Add(new SqlParameter("Fecha_Aplicacion", Fecha.HasValue ? (object)Fecha.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Efectivo", dEfectivo.HasValue ? (object)dEfectivo.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Documento", sDocumentos));
                context.Parametros.Add(new SqlParameter("SaldoInicial", dSaldoInicial.HasValue ? (object)dSaldoInicial.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("SaldoFinal", dSaldoFinal.HasValue ? (object)dSaldoFinal.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Numero_Proveedor", intNumeroProveedor.HasValue ? (object)intNumeroProveedor.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Comentario", sComentario));
                context.Parametros.Add(new SqlParameter("Num_Cuenta", intNumCuenta.HasValue ? (object)intNumCuenta.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Cheque", intCheque.HasValue ? (object)intCheque.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Num_Concepto", intNumConcepto.HasValue ? (object)intNumConcepto.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Clas_Iva", intClasIva.HasValue ? (object)intClasIva.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Numero_Acreedor", intAcreedor.HasValue ? (object)intAcreedor.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("Monto", dMonto.HasValue ? (object)dMonto.Value : DBNull.Value));


                context.ExecuteProcedure("sp_cp_Conta_ManualPagoPolizasIndividuales", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }



        public static bool ActualizarCuentasPorPagar(int intEmpresa, int intNumeroReferencia, decimal dAbono)
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();

                context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Referencia", intNumeroReferencia));
                context.Parametros.Add(new SqlParameter("@Abono", dAbono));


                context.ExecuteProcedure("sp_CP_Actualiza_Saldo", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeleteTablaPaso(int iNumero_Empresa, int iNumero_Usuario)
        {

            int iResultado = 0;
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", iNumero_Usuario));

            iResultado = context.ExecuteNonQuery("sp_cp_BorraTablaPaso", true);
            if (iResultado >= 0)
                return true;
            else
                return false;
        }
        public static DataTable sp_Cuentas_Cobrar_Cliente(int intMovimiento, int? iNumero, int iNumero_Empresa, string sRfcCliente, string sNombreCliente, string sSerie, string sFolio, DateTime? Fecha,

                                                string sRfcEmisor, decimal? dMonto, string sMoneda, decimal? dTipoCambio, int? intClas_Iva, string sFolioFiscal, int? iGenerarCobro,

                                                DateTime? FechaCobro, string sFolioCobro, decimal? dMontoCobro, int? iMonedaCobro, decimal? dTipoCambioCobro, int iUsuario, int? intPoliza_Manual)
        {



            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();

            context.Parametros.Clear();



            context.Parametros.Add(new SqlParameter("@Movimiento", intMovimiento));

            context.Parametros.Add(new SqlParameter("@Numero", iNumero));

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));

            context.Parametros.Add(new SqlParameter("@RFC_Cliente", sRfcCliente));

            context.Parametros.Add(new SqlParameter("@Nombre_Cliente", sNombreCliente));

            context.Parametros.Add(new SqlParameter("@Serie", sSerie));

            context.Parametros.Add(new SqlParameter("@Folio", sFolio));

            context.Parametros.Add(new SqlParameter("@Fecha", Fecha.HasValue ? (object)Fecha.Value : DBNull.Value));

            context.Parametros.Add(new SqlParameter("@RFC_Emisor", sRfcEmisor));

            context.Parametros.Add(new SqlParameter("@Monto", dMonto.HasValue ? (object)dMonto.Value : DBNull.Value));

            context.Parametros.Add(new SqlParameter("@Moneda", sMoneda));

            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", dTipoCambio.HasValue ? (object)dTipoCambio.Value : DBNull.Value));

            context.Parametros.Add(new SqlParameter("@Clas_IVA", intClas_Iva.HasValue ? (object)intClas_Iva.Value : DBNull.Value));

            context.Parametros.Add(new SqlParameter("@Folio_Fiscal", sFolioFiscal));

            context.Parametros.Add(new SqlParameter("@Generar_Cobro", iGenerarCobro.HasValue ? (object)iGenerarCobro.Value : DBNull.Value));

            context.Parametros.Add(new SqlParameter("@Fecha_Cobro", FechaCobro.HasValue ? (object)FechaCobro.Value : DBNull.Value));

            context.Parametros.Add(new SqlParameter("@Folio_Cobro", sFolioCobro));

            context.Parametros.Add(new SqlParameter("@Monto_Cobro", dMontoCobro.HasValue ? (object)dMontoCobro.Value : DBNull.Value));

            context.Parametros.Add(new SqlParameter("@Moneda_Cobro", iMonedaCobro.HasValue ? (object)iMonedaCobro.Value : DBNull.Value));

            context.Parametros.Add(new SqlParameter("@Tipo_Cambio_Cobro", dTipoCambioCobro.HasValue ? (object)dTipoCambioCobro.Value : DBNull.Value));

            context.Parametros.Add(new SqlParameter("@Usuario", iUsuario));

            context.Parametros.Add(new SqlParameter("@Poliza_Manual", intPoliza_Manual.HasValue ? (object)intPoliza_Manual.Value : DBNull.Value));





            dt = context.ExecuteProcedure("[sp_Cuentas_Cobrar_Cliente]", true).Copy();



            return dt;





        }


        public static DataTable sp_Cuentas_Cobrar_Cliente2(int intMovimiento, int? iNumero, int iNumero_Empresa, string sRfcCliente, string sNombreCliente, string sSerie, string sFolio, DateTime? Fecha,
                                                           string sRfcEmisor, decimal? dMonto, string sMoneda, decimal? dTipoCambio, int? intClas_Iva, string sFolioFiscal, int? iGenerarCobro,
                                                           DateTime? FechaCobro, string sFolioCobro, decimal? dMontoCobro, int? iMonedaCobro, decimal? dTipoCambioCobro, int iUsuario,
                                                            int? intPoliza_Manual, decimal? dMontoIva, decimal? dMontoSubtotal)
        {



            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Movimiento", intMovimiento));
            context.Parametros.Add(new SqlParameter("@Numero", iNumero));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@RFC_Cliente", sRfcCliente));
            context.Parametros.Add(new SqlParameter("@Nombre_Cliente", sNombreCliente));
            context.Parametros.Add(new SqlParameter("@Serie", sSerie));
            context.Parametros.Add(new SqlParameter("@Folio", sFolio));
            context.Parametros.Add(new SqlParameter("@Fecha", Fecha.HasValue ? (object)Fecha.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@RFC_Emisor", sRfcEmisor));
            context.Parametros.Add(new SqlParameter("@Monto", dMonto.HasValue ? (object)dMonto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Moneda", sMoneda));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", dTipoCambio.HasValue ? (object)dTipoCambio.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_IVA", intClas_Iva.HasValue ? (object)intClas_Iva.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Folio_Fiscal", sFolioFiscal));
            context.Parametros.Add(new SqlParameter("@Generar_Cobro", iGenerarCobro.HasValue ? (object)iGenerarCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Cobro", FechaCobro.HasValue ? (object)FechaCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Folio_Cobro", sFolioCobro));
            context.Parametros.Add(new SqlParameter("@Monto_Cobro", dMontoCobro.HasValue ? (object)dMontoCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Moneda_Cobro", iMonedaCobro.HasValue ? (object)iMonedaCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio_Cobro", dTipoCambioCobro.HasValue ? (object)dTipoCambioCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("@Poliza_Manual", intPoliza_Manual.HasValue ? (object)intPoliza_Manual.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Monto_Subtotal", dMontoSubtotal.HasValue ? (object)dMontoSubtotal.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Monto_IVA", dMontoIva.HasValue ? (object)dMontoIva.Value : DBNull.Value));
            //context.Parametros.Add(new SqlParameter("@Sucursal", intSucursal.HasValue ? (object)intSucursal.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_Cuentas_Cobrar_ClienteV2]", true).Copy();

            return dt;


        }




        public static DataTable sp_Cuentas_Cobrar_Cliente3(int intMovimiento, int? iNumero, int iNumero_Empresa, string sRfcCliente, string sNombreCliente, string sSerie, string sFolio, DateTime? Fecha,
                                                          string sRfcEmisor, decimal? dMonto, string sMoneda, decimal? dTipoCambio, int? intClas_Iva, string sFolioFiscal, int? iGenerarCobro,
                                                          DateTime? FechaCobro, string sFolioCobro, decimal? dMontoCobro, int? iMonedaCobro, decimal? dTipoCambioCobro, int iUsuario,
                                                           int? intPoliza_Manual, decimal? dMontoIva, decimal? dMontoSubtotal, int? intSucursal)
        {



            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Movimiento", intMovimiento));
            context.Parametros.Add(new SqlParameter("@Numero", iNumero));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@RFC_Cliente", sRfcCliente));
            context.Parametros.Add(new SqlParameter("@Nombre_Cliente", sNombreCliente));
            context.Parametros.Add(new SqlParameter("@Serie", sSerie));
            context.Parametros.Add(new SqlParameter("@Folio", sFolio));
            context.Parametros.Add(new SqlParameter("@Fecha", Fecha.HasValue ? (object)Fecha.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@RFC_Emisor", sRfcEmisor));
            context.Parametros.Add(new SqlParameter("@Monto", dMonto.HasValue ? (object)dMonto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Moneda", sMoneda));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", dTipoCambio.HasValue ? (object)dTipoCambio.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_IVA", intClas_Iva.HasValue ? (object)intClas_Iva.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Folio_Fiscal", sFolioFiscal));
            context.Parametros.Add(new SqlParameter("@Generar_Cobro", iGenerarCobro.HasValue ? (object)iGenerarCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Cobro", FechaCobro.HasValue ? (object)FechaCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Folio_Cobro", sFolioCobro));
            context.Parametros.Add(new SqlParameter("@Monto_Cobro", dMontoCobro.HasValue ? (object)dMontoCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Moneda_Cobro", iMonedaCobro.HasValue ? (object)iMonedaCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio_Cobro", dTipoCambioCobro.HasValue ? (object)dTipoCambioCobro.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("@Poliza_Manual", intPoliza_Manual.HasValue ? (object)intPoliza_Manual.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Monto_Subtotal", dMontoSubtotal.HasValue ? (object)dMontoSubtotal.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Monto_IVA", dMontoIva.HasValue ? (object)dMontoIva.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Sucursal", intSucursal.HasValue ? (object)intSucursal.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_Cuentas_Cobrar_ClienteV2]", true).Copy();

            return dt;


        }


        public static bool ActualizarCuentasPorCobrar(int intEmpresa, int intNumeroReferencia, decimal dAbono)
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();

                context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Referencia", intNumeroReferencia));
                context.Parametros.Add(new SqlParameter("@Abono", dAbono));


                context.ExecuteProcedure("sp_CC_Actualiza_Saldo", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static DataTable gfObtenNumeroFolio(int iNumero_Empresa, int iNumero_Banco, int iNumero_Cuenta)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Banco", iNumero_Banco));
            context.Parametros.Add(new SqlParameter("@Numero_Cuenta", iNumero_Cuenta));


            dt = context.ExecuteProcedure("sp_cpBancos_Union_Folio", true).Copy();
            return dt;
        }
        //nuevo 

        public static DataTable gfConceptoContableNC(int iEmpresa, int intConceptoContable)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("@Concepto_Contable_NC", intConceptoContable));

            dt = context.ExecuteProcedure("sp_Ventas_Configuracion_ObtenCNC", true).Copy();
            return dt;
        }

        public static DataTable gfsp_Cuentas_pagar_NC(int iNumero_Empresa, int iClasIva, DateTime Fecha, int iProveedor,
                                                      string sDocto, decimal dMonto, int iUsuario, int iMoneda, decimal dTC,
                                                      string sFF, int iTipoConta)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Clas_Iva", iClasIva));
            context.Parametros.Add(new SqlParameter("@Fecha_Aplicacion", Fecha));
            context.Parametros.Add(new SqlParameter("@Proveedor", iProveedor));
            context.Parametros.Add(new SqlParameter("@Docto", sDocto));
            context.Parametros.Add(new SqlParameter("@Monto", dMonto));
            context.Parametros.Add(new SqlParameter("@Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("@Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("@TC", dTC));
            context.Parametros.Add(new SqlParameter("@FF", sFF));
            context.Parametros.Add(new SqlParameter("@Flag_Conta", iTipoConta));

            dt = context.ExecuteProcedure("sp_Cuentas_pagar_NC", true).Copy();
            return dt;

        }

        public static DataTable gf_ObtieneDatosCuentasContablesPorNumero(int iEmpresa, int intNumeroCuenta)//pasar a otro proyectos
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero", intNumeroCuenta));

            dt = context.ExecuteProcedure("sp_ObtieneDatosCuentasContablesPorNumero", true).Copy();
            return dt;
        }



        public static bool RegistraConceptosCuentasCobrar(int intTipo, int intEmpresa, int intNumUsuario, string sDescripcion, decimal dCantidad, decimal dImporte, string noUUID)
        {
            bool bandera = true;

            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Tipo", intTipo));
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Usuario", intNumUsuario));
                context.Parametros.Add(new SqlParameter("@Descripcion", sDescripcion));
                context.Parametros.Add(new SqlParameter("@Cantidad", dCantidad));
                context.Parametros.Add(new SqlParameter("@Importe", dImporte));
                context.Parametros.Add(new SqlParameter("@FF", noUUID));
                context.ExecuteProcedure("sp_Cuentas_Cobrar_Alta_CFDI_Prod", true).Copy();

            }
            catch
            {
                bandera = false;
            }

            return bandera;
        }

        ///////////////////////////////////// NODOS XML ///////////////////////////////

       

        public static int gf_sp_Catalogo_Nodos(int intEmpresa, int intID, string sDescripcionGpoNodos, string sDescNodo, int intTipo, int intTipoDetalle, int intNumero)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                       
            new SqlParameter("Numero_Empresa", intEmpresa),
            new SqlParameter("ID", intID),
            new SqlParameter("Descripcion", sDescripcionGpoNodos),
             new SqlParameter("DescripcionNodo", sDescNodo),
            new SqlParameter("Tipo", intTipo),
            new SqlParameter("Tipo_Detalle", intTipoDetalle),
            new SqlParameter("Numero", intNumero),
        
        };
            try
            {

                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        if (param.ParameterName == "ID" && intID == 0)
                        {
                            param.Direction = ParameterDirection.Output;
                        }
                    }
                }

                return sqlDBHelper.ExecuteNonQueryOutputValue("sp_Catalogo_Nodos", "ID", CommandType.StoredProcedure, parameters);

            }
            catch (Exception)
            {

                return -1;
            }

        }

        public static DataTable sp_Catalogo_Nodos_Consulta(int intTipo, int intNumeroEmpresa, int intID)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo", intTipo));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intNumeroEmpresa));
            context.Parametros.Add(new SqlParameter("@ID", intID));

            dt = context.ExecuteProcedure("sp_Catalogo_Nodos_Consulta", true).Copy();
            return dt;
        }


        public static DataTable gfDropNodos(int Numero_Empresa, string Nombre_Completo)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Nodo", Nombre_Completo));

            dt = context.ExecuteProcedure("sp_Catalogo_Nodos_Autocomplit", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        public static DataTable sp_Catalogo_Nodos_Proveedor(int intNumeroEmpresa, int intIDNodo)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intNumeroEmpresa));
            context.Parametros.Add(new SqlParameter("@ID_Nodo", intIDNodo));

            dt = context.ExecuteProcedure("sp_Nodo_Relacion_TraeProveedor", true).Copy();
            return dt;
        }


        public static DataTable sp_Nodo_Relacion_Proveedor(int intTipo, int intEmpresa, int intId, int intNumeroCliente)
        {


            try
            {

                SQLConection context = new SQLConection();
                DataTable dt = new DataTable();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Tipo", intTipo));
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
                context.Parametros.Add(new SqlParameter("@ID_Nodo", intId));
                context.Parametros.Add(new SqlParameter("@Numero_Proveedor", intNumeroCliente));

                dt = context.ExecuteProcedure("sp_Nodo_Relacion_Proveedores", true).Copy();
                return dt;

            }
            catch (Exception)
            {

                DataTable dt = new DataTable();
                return dt;
            }

        }
    }


}
