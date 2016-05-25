using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace IntegraData
{
    public class toolsDAL
    {

        public DataTable import_to_string(int icompany, Decimal dtotal, string sMoneda,int ibandera)
        {
          
            string sFunction = "SELECT dbo.fn_ImporteLetras(@Numero_Empresa, @Cantidad, @sMoneda, @iBandera)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Numero_Empresa", icompany),
                new SqlParameter("Cantidad", dtotal),
                new SqlParameter("sMoneda", sMoneda),
                new SqlParameter("iBandera", ibandera),
            };

            return sqlDBHelper.GetFunction(sFunction, parameters);
    
        }




    }
}
