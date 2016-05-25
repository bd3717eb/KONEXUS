using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace IntegraData
{
    public class CreditNotesDAL
    {



        public DataTable GetFolio(int icompany, int idocument, int iserie)
        {
            string sFunction = "SELECT dbo.fn_Obten_Siguiente_Folio(@Numero_Empresa, @Tipo_Documento, @Serie)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Numero_Empresa", icompany),
                new SqlParameter("Tipo_Documento", idocument),
                new SqlParameter("Serie", iserie),
            };

            return sqlDBHelper.GetFunction(sFunction, parameters);
        }

        public static DataTable GetAuxStatusInvoice(int iCompany, int iinvoicenumber)
        {
            SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("companynumber", iCompany),
                        new SqlParameter("invoicenumber", iinvoicenumber), 

                    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_NotaDeCredito_ObtieneEstatusAuxiliarDeFactura_v1", CommandType.StoredProcedure, parameters);

        }

        public static DataTable ValidaAuxContablePasivo(int iCompany, int iclient)
        {

            SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("companynumber", iCompany),
                        new SqlParameter("client", iclient), 

                    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_NotaDeCredito_ValidaAuxContablePasivo_v1", CommandType.StoredProcedure, parameters);
        }


        

    }


}
