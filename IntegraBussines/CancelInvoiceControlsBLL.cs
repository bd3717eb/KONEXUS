using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;
using System.Data.SqlClient;


namespace IntegraBussines
{
    public  class CancelInvoiceControlsBLL
    {
     
        public static DataTable GetDocuments(int iCliente, int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@client", iCliente));

            dt = context.ExecuteProcedure("sp_CancelacionDeFactura_ObtieneDocumentosVFactura_v1", true).Copy();
            return dt;
        }
     
        public static DataTable GetDocumentNumbers(int icompany, int idocument, int iclient, int iserie)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@document", idocument));
            context.Parametros.Add(new SqlParameter("@serie", iserie));
            context.Parametros.Add(new SqlParameter("@client", iclient));

            dt = context.ExecuteProcedure("sp_CancelacionDeFactura_ObtieneNumerosDeDocumentoVFactura_v1", true).Copy();
            return dt;
        }



        public static DataTable GetSeries(int iEmpresa, int iCliente, int iDocumento)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@document", iDocumento));
            context.Parametros.Add(new SqlParameter("@client", iCliente));

            dt = context.ExecuteProcedure("sp_CancelacionDeFactura_ObtieneSeriesVFactura_v1", true).Copy();
            return dt;
        }

     

        public static DataTable GetProgramTransaccion(int iEmpresa, int iNumeroPrograma, int iConcepto, int iIvaFactura, int iConceptoFactura)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@iProgramNumber", iNumeroPrograma));
            context.Parametros.Add(new SqlParameter("@iConcept", iConcepto));
            context.Parametros.Add(new SqlParameter("@iIvaInvoice", iIvaFactura));
            context.Parametros.Add(new SqlParameter("@iConceptInvoice", iConceptoFactura));

            dt = context.ExecuteProcedure("sp_CancelacionDeFactura_ObtieneProgramaDeTransaccion_v1", true).Copy();
            return dt;
        }

       
    }
}
