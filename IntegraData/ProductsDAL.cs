using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace IntegraData
{
    public static class ProductsDAL
    {
        public static DataTable GetProducts(int icompany, string sdescription, int imarketing, int itype)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("NumeroEmpresa", icompany),
                new SqlParameter("Descripcion", sdescription),                
                new SqlParameter("Comercializacion", imarketing),
                new SqlParameter("TipoProducto", itype),
            };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_Obtener_Productos", CommandType.StoredProcedure, parameters);
        }

        public static DataTable GetConcepts(int icompany, int imarketingproduct, int iproduct, int icustomer, int ito)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("NumeroEmpresa", icompany),
                new SqlParameter("Comercializacion", imarketingproduct),
                new SqlParameter("Producto", iproduct),
                new SqlParameter("Cliente", 4),
                new SqlParameter("Hasta", ito),
            };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_Obtener_Conceptos", CommandType.StoredProcedure, parameters);
        }
    }
}
