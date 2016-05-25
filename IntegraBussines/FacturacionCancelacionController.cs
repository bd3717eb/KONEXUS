using System.Data;
using IntegraData;
using System.Data.SqlClient;
using System;
namespace IntegraBussines
{
    public class FacturacionCancelacionController
    {
        public static DataTable gfGetFacturasCanceladas(int Numero_Empresa, int Numero_Cliente, string Fecha_Ini, string Fecha_Fin, int Numero_Factura = 0)
        {
            if (Numero_Cliente <= 0)
                Numero_Cliente = 0;

            if (Fecha_Ini.Trim().Length == 0)
                Fecha_Ini = string.Empty;

            if (Fecha_Fin.Trim().Length == 0)
                Fecha_Fin = string.Empty;

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("Numero_Cliente", Numero_Cliente));
            context.Parametros.Add(new SqlParameter("Fecha_Ini", Fecha_Ini));
            context.Parametros.Add(new SqlParameter("Fecha_Fin", Fecha_Fin));
            context.Parametros.Add(new SqlParameter("Numero_Factura", Numero_Factura));
            dt = context.ExecuteProcedure("SP_Factura_Consulta_Canceladas", true).Copy();
            return dt;
        }
        public static DataTable gfGetDocumentNumbers(int icompany, int idocument, int iclient, int iserie, int iOpcion = 0)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", icompany));
            context.Parametros.Add(new SqlParameter("document", idocument));
            context.Parametros.Add(new SqlParameter("serie", iserie));
            context.Parametros.Add(new SqlParameter("client", iclient));
            context.Parametros.Add(new SqlParameter("opcion", iOpcion));
            dt = context.ExecuteProcedure("sp_CancelacionDeFactura_ObtieneNumerosDeDocumentoVFactura_v1", true).Copy();
            return dt;
        }

        //public static int gfFacturaActualizaPersonaFisica(decimal RET_ISR, decimal RET_IVA, decimal TotalTotal, int iIDFacturaNext, int iClientNumber)
        //{
        //    string sQuery = string.Empty;
        //    int iRespuesta = 0;
        //    try
        //    {
        //        sQuery = string.Concat("UPDATE Factura  SET   RET_ISR	=  '" + RET_ISR + "'   ,RET_IVA	= '" + RET_IVA + "'   ,TOTALTOTAL = '" + TotalTotal + "'   WHERE Numero_Factura = '" + iIDFacturaNext + "' AND Numero_Cliente =" + iClientNumber + "");
        //        new SQLConection().ExecuteNonQuery(sQuery);
        //    }
        //    catch
        //    {
        //        iRespuesta = -1;
        //    }
        //    return iRespuesta;

        //}


        public static int gfFacturaActualizaPersonaFisica(decimal RET_ISR, decimal RET_IVA, decimal TotalTotal, int iIDFacturaNext, int iClientNumber)
        {
            int iRespuesta = 0;
            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();

                context.Parametros.Add(new SqlParameter("@Numero_Cliente", iClientNumber));
                context.Parametros.Add(new SqlParameter("@Ret_Isr", RET_ISR));
                context.Parametros.Add(new SqlParameter("@Ret_IVA", RET_IVA));
                context.Parametros.Add(new SqlParameter("@Total", TotalTotal));
                context.Parametros.Add(new SqlParameter("@Num_Factura", iIDFacturaNext));

                context.ExecuteProcedure("sp_ActualizaPersonaFisica", true).Copy();
                return iRespuesta;

            }
            catch
            {
                return -1;
            }

        }


        public static DataTable gfGetDocuments(int companynumber, int client, int opcion = 0)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", companynumber));
            context.Parametros.Add(new SqlParameter("@client", client));
            context.Parametros.Add(new SqlParameter("@opcion", opcion));
            dt = context.ExecuteProcedure("[sp_CancelacionDeFactura_ObtieneDocumentosVFactura_v1]", true).Copy();
            return dt;
        }

        public static DataTable gfGetSeries(int companynumber,
                                            int document,
                                            int client,
                                            int opcion)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", companynumber));
            context.Parametros.Add(new SqlParameter("@document", document));
            context.Parametros.Add(new SqlParameter("@client", client));
            context.Parametros.Add(new SqlParameter("@opcion", opcion));
            dt = context.ExecuteProcedure("[sp_CancelacionDeFactura_ObtieneSeriesVFactura_v1]", true).Copy();
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }


        public DataTable gfItemsParaCancelacion(int icompany, int iClient, int iCurrency, int idocument, int iSerie, DateTime? DateFrom, DateTime? DateTo, int idocFrom, int idocTo)
        {
            //InvoiceDAL objIDAL = new InvoiceDAL();
            DataTable dt = InvoiceBLL.GetInvoicesView(icompany, iClient, iCurrency, idocument, iSerie, DateFrom, DateTo, idocFrom, idocTo);
            if (dt != null)
                return dt;
            else
                return new DataTable();

        }

        public DataTable gfFacCanObtieneConceptoyEstatus(int companynumber, int number)
        {
            DataTable dt = InvoiceBLL.GetClientExchangeConceptAndEstatus(companynumber, number);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        public DataTable gfFacCanObtenDocCxC(int companynumber, int invoicenumber)
        {
            DataTable dt = new InvoiceBLL().GetDoctoCxC(companynumber, invoicenumber);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        public DataTable gfFacCanObtenRutaTransporte(int Numero_Empresa, int Numero_Factura)
        {
            DataTable dt = new InvoiceBLL().GetRutaTransporte(Numero_Empresa, Numero_Factura);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        public DataTable gfVFacCanValidacionVenta(int companynumber)
        {
            DataTable dt = new SaleBLL().GetSalesValidations(companynumber);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        public DataTable gfFacCanObtieneOrigen(int companynumber, int invoicenumber)
        {
            DataTable dt = new InvoiceBLL().ObtieneOrigenFactura(companynumber, invoicenumber);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        public DataTable gfFacCanObtieneStockdeFactura(int companynumber, int invoicenumber)
        {
            DataTable dt = new InvoiceBLL().ObtieneDisponibleDeFactura(companynumber, invoicenumber);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        public DataTable gfFacCanObtieneTicket(int companynumber, int invoicenumber)
        {
            DataTable dt = new InvoiceBLL().GetTicket(companynumber, invoicenumber);
            if (dt != null)
                return dt;
            else
                return new DataTable();


        }
        public DataTable gfGetFacturas(int icompany, int iClient, int iCurrency, int idocument, int iSerie, DateTime? DateFrom, DateTime? DateTo, int idocFrom, int idocTo)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@iClient", iClient));
            context.Parametros.Add(new SqlParameter("@iCurrency", iCurrency));
            context.Parametros.Add(new SqlParameter("@idocument", idocument));
            context.Parametros.Add(new SqlParameter("@iSerie", iSerie));
            context.Parametros.Add(new SqlParameter("@DateFrom", DateFrom.HasValue ? (object)DateFrom.Value.ToShortDateString() : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@DateTo", DateTo.HasValue ? (object)DateTo.Value.ToShortDateString() : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@idocFrom", idocFrom));
            context.Parametros.Add(new SqlParameter("@idocTo", idocTo));
            dt = context.ExecuteProcedure("sp_Factura_ObtieneVistaDeFacturas_v3", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();

        }

        public DataTable gfFactura_Tienda(int Numero_Empresa, int ID_Factura, string Folio_Fiscal)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@ID_Factura", ID_Factura));
            context.Parametros.Add(new SqlParameter("@Folio_Fiscal", Folio_Fiscal));

            dt = context.ExecuteProcedure("[sp_Factura_Tienda_CXC]", true).Copy();
            if (dt != null)
                return dt;
            else
                return new DataTable();

        }


        public DataTable sp_Factura_Contabilidad_CC(int Numero_Empresa, int intNumero, int Numero_Usuario, string Folio_Fiscal)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero", intNumero));
            context.Parametros.Add(new SqlParameter("@Usuario", Numero_Usuario));
            context.Parametros.Add(new SqlParameter("@Folio_Fiscal", Folio_Fiscal));

            dt = context.ExecuteProcedure("[sp_Factura_Contabilidad_CC]", true).Copy();
            if (dt != null)
                return dt;
            else
                return new DataTable();

        }

        public bool ActualizarCuentasPorCobrar(string iTipo, string sNumero_Factura, DateTime datecancel, int numEmpresa)
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Tipo", iTipo));
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
                context.Parametros.Add(new SqlParameter("@Fecha_Cancelacion", datecancel));
                context.Parametros.Add(new SqlParameter("@Numero_Factura", sNumero_Factura));

                context.ExecuteProcedure("sp_ActualizarCuentasPorCobrar", true).Copy();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public static bool  gfFactura_Contabiliza(int Tipo, int Numero_Empresa, int Numero)
        {
            try
            {
                //@Tipo				INT,
                //@Numero_Empresa		INT,
                //@Numero				INT
                Tipo = 1;
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Tipo", Tipo));
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
                context.Parametros.Add(new SqlParameter("@Numero", Numero));
                context.ExecuteProcedure("[sp_Factura_Contabiliza]", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
