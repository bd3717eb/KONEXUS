using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace IntegraData
{
    public class InvoiceDAL
    {
        

        public string RetornaError() {
            return sqlDBHelper.sqlRetornaError();
        }
       
        public bool CancelInvoice(int icompany, int itypeinvoice, int iinvoicenumber, DateTime date, int iperson, string scausa, int iNumeroTransaccion, string sauxcliente, decimal dtotalinvoice, string stituloinvoice, DateTime dateinvoice)
        {
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Numero_Empresa", icompany),
                new SqlParameter("Tipo_Factura", itypeinvoice),
                new SqlParameter("Numero_Reg_Factura", iinvoicenumber),
                new SqlParameter("Fecha_Cancelacion", date),
                new SqlParameter("Usuario", iperson),
                new SqlParameter("Causa", scausa),
                new SqlParameter("Numero_Transaccion", iNumeroTransaccion),
                new SqlParameter("Auxiliar", sauxcliente),
                new SqlParameter("Cantidad", dtotalinvoice),
                new SqlParameter("Titulo", stituloinvoice),
                new SqlParameter("Fecha_Emision", dateinvoice),
                                         
            };

            return sqlDBHelper.ExecuteNonQuery("sp_Factura_Cancelacion ", CommandType.StoredProcedure, parameters);
        }

        


        public bool PayInvoice(int itipo, int icompany, int iinvoice, int? inumero, int? inumeroconcepto, int? imoneda, decimal? itipocambio, decimal? dingreso,
                               decimal? degreso, string sreferencia, int? iusuario)
        {
            
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Tipo", itipo),
                new SqlParameter("Numero_Empresa", icompany),
                new SqlParameter("Numero_Factura", iinvoice),
                new SqlParameter("Numero", inumero.HasValue ? (object)inumero.Value : DBNull.Value),
                new SqlParameter("Numero_Concepto", inumeroconcepto.HasValue ? (object)inumeroconcepto.Value : DBNull.Value),
                new SqlParameter("Clas_Moneda", imoneda.HasValue ? (object)imoneda.Value : DBNull.Value),
                new SqlParameter("TC", itipocambio.HasValue ? (object)itipocambio.Value : DBNull.Value),
                new SqlParameter("Ingreso", dingreso.HasValue ? (object)dingreso.Value : DBNull.Value),
                new SqlParameter("Egreso", degreso.HasValue ? (object)degreso.Value : DBNull.Value),
                new SqlParameter("Referencia", sreferencia),
                new SqlParameter("Usuario", iusuario.HasValue ? (object)iusuario.Value : DBNull.Value),
               
            };

            return sqlDBHelper.ExecuteNonQuery("sp_cuentas_cobrar_detalle", CommandType.StoredProcedure, parameters);
        }
        
        public bool PayInvoiceProcess(int icompany, int iinvoice)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
            
                new SqlParameter("Numero_Empresa", icompany),
                new SqlParameter("Numero_Factura", iinvoice),
                       
            };

            return sqlDBHelper.ExecuteNonQuery("sp_cuentas_cobrar_detalle_proceso", CommandType.StoredProcedure, parameters);
        }


        public DataTable CalculaImporteCxC(int icompany, int iinvoicenumber)
        {
            string Function = "SELECT dbo.fn_Calcula_Importe_Cuenta_Cobrar(@Numero_Empresa, @Numero_Factura)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Numero_Empresa", icompany),
                new SqlParameter("Numero_Factura", iinvoicenumber),
              
            };

            return sqlDBHelper.GetFunction(Function, parameters);
        }



        

        public bool UpdateFactura(int icompany, int iinvoicenumber, DateTime fechaCancel, int iuser)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("numeroempresa", icompany),
                new SqlParameter("numerofactura", iinvoicenumber),
                new SqlParameter("numeropersona", iuser),
                new SqlParameter("fecha", fechaCancel),
            };

            return sqlDBHelper.ExecuteNonQuery("sp_Factura_Actualiza", CommandType.StoredProcedure, parameters);
        }

        public DataTable ObtenerDescuentoFactura(int icompany, int iinvoicenumber)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
              new SqlParameter("empresa", icompany),
              new SqlParameter("numeroregistrofactura", iinvoicenumber),
            };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_FacturaObtenDescuento", CommandType.StoredProcedure, parameters);
        }


        public DataTable ObtenerSubtotalFactura(int icompany, int iinvoicenumber)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
              new SqlParameter("empresa", icompany),
              new SqlParameter("numeroregistrofactura", iinvoicenumber),
            };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_FacturaObtenSubtotal", CommandType.StoredProcedure, parameters);
        }
        public DataTable GetValidateTransactionContable(int icompany, int iNumberTransaction, int iNumberProgram, DateTime fechacancel)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
              new SqlParameter("companynumber", icompany),
              new SqlParameter("transactionnumber", iNumberTransaction),
              new SqlParameter("programnumber", iNumberProgram),
              new SqlParameter("fecha", fechacancel),
            };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_Factura_ObtieneTransaccionContable", CommandType.StoredProcedure, parameters);
        }

        #region Facturacion
        public DataTable GetFnFacturaConcepto(int icompany, int? iSucursal)
        {
            string Function = "SELECT dbo.fn_Factura_Concepto (@Numero_Empresa,@Clas_Sucursal)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Numero_Empresa", icompany),
                new SqlParameter("Clas_Sucursal", iSucursal.HasValue ? (object)iSucursal.Value : DBNull.Value),   
               
            };

            return sqlDBHelper.GetFunction(Function, parameters);
        }

        public DataTable GetfnIDFactura(int icompany, int inumber)
        {
            string Function = "SELECT dbo.fn_ID_Factura (@Numero_Empresa,@Numero_Factura_Libre)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Numero_Empresa", icompany),
                new SqlParameter("Numero_Factura_Libre", inumber),
               
            };

            return sqlDBHelper.GetFunction(Function, parameters);
        }
        #endregion





              
    }

}
