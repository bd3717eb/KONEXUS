using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;
using System.Data.SqlClient;

namespace IntegraBussines
{
    public static class SalesReportsBLL
    {
  
        public static DataTable GetSaleDay(int iEmpresa, DateTime date, int iSucursal)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@date", date));
            context.Parametros.Add(new SqlParameter("@office", iSucursal));


            dt = context.ExecuteProcedure("sp_ReporteDeVentas_ObtieneReporteDelDia_v3", true).Copy();
            return dt;
        }
    }
}
