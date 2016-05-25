using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IntegraData;


namespace IntegraBussines
{
    public class TransaccionContableBLL
    {

        public static DataTable ObtienesDefinicionTransaccion(int intEmpresa, int intPrograma, int intTransaccion)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Programa", intPrograma));
            context.Parametros.Add(new SqlParameter("@Numero_Transaccion", intTransaccion));
            dt = context.ExecuteProcedure("[sp_VerificarDefinicionTransaccion]", true).Copy();
      

            return dt;
        }
    }
}
