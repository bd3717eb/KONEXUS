using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace IntegraData
{
    public class CreditNotesControlsDAL
    {
        
          

        public DataTable GetCurrencies(int iuser,int icompany, int istatus, int icontabiliza)// estatus <> 8  , contabilizada = 1
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("clientnumber", iuser),
                new SqlParameter("companynumber", icompany),
                new SqlParameter("status", istatus),
                new SqlParameter("contabiliza", icontabiliza),
                           
                       
            };
            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_ControlesNotasDeCredito_ObtieneMonedas_v1", CommandType.StoredProcedure, parameters);

        }



        public DataTable GetDocuments(int iclient, int icurrency, int icompany, int iuser)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("companynumber", icompany),
                new SqlParameter("clientnumber", iclient),
                new SqlParameter("usernumber", iuser),
                new SqlParameter("currency", icurrency),
                                
            };
            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_ControlesNotasDeCredito_ObtieneDocumentosVUnionFacturas_v1", CommandType.StoredProcedure, parameters);

        }
        
        



        public DataTable GetSeries(int iclient, int icompany, int iuser, int icurrency, int idocument)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("companynumber", icompany),
                new SqlParameter("clientnumber", iclient),
                new SqlParameter("usernumber", iuser),
                new SqlParameter("currency", icurrency),
                new SqlParameter("document", idocument),
                                
            };
            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_ControlesNotasDeCredito_ObtieneSeriesVUnionFactura_v1", CommandType.StoredProcedure, parameters);

        }




        public DataTable GetOldCreditNotes(int iclient, int icompany, int icurrency, int idocument, int iserie, DateTime from, DateTime to, int istatus, int icontabiliza)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("companynumber", icompany),
                new SqlParameter("status", istatus),
                new SqlParameter("contabiliza", icontabiliza),
                new SqlParameter("clientnumber", iclient),                
                new SqlParameter("currency", icurrency),
                new SqlParameter("document", idocument),
                new SqlParameter("serie", iserie),
                                
            };
            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_ControlesNotasDeCredito_ObtieneNotasDeCreditoVUnionFactura_v1", CommandType.StoredProcedure, parameters);

        }
       
    }
}
