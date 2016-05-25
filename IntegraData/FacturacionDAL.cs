using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace IntegraData
{
    public class FacturacionDAL
    {

        
        public bool CancelaPago(int iEmpresa, int iUsuario, int iNumeroPago, DateTime FechaCancelacion, int iConceptoCancelacion)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Numero_Empresa", iEmpresa),
                new SqlParameter("Usuario",iUsuario),
                new SqlParameter("Numero", iNumeroPago),
                new SqlParameter("FechaCancelacion", FechaCancelacion),
                new SqlParameter("lConceptoCancelacion", iConceptoCancelacion),
               
            };

            return sqlDBHelper.ExecuteNonQuery("sp_cc_Pagos_Eliminacion", CommandType.StoredProcedure, parameters);
        }


    }
}
