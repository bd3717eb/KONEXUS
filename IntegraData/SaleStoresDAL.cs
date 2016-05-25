using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace IntegraData
{
    public  class SaleStoresDAL
    {
             

        public static string DeniedProducts(int imoviment, int icompany, int itype, int isalenumber, int ipartialsale, int istore, int iTproduct, int iproduct, int iTconcept, int iconcept, decimal dquantity, DateTime date, int iFconcept, decimal dprice)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Tipo_Movimiento", imoviment),
                new SqlParameter("Numero_Empresa", icompany),
                new SqlParameter("Tipo", itype),
                new SqlParameter("Numero_Tipo", isalenumber),
                new SqlParameter("Numero", ipartialsale),
                new SqlParameter("Clas_Almacen", istore),
                new SqlParameter("Clas_TProducto", iTproduct),
                new SqlParameter("Clas_Producto", iproduct),
                new SqlParameter("Clas_TConcepto", iTconcept),
                new SqlParameter("Clas_Concepto", iconcept),              
                new SqlParameter("Cantidad", dquantity),
                new SqlParameter("Fecha", date),
                new SqlParameter("Familia_Concepto", iFconcept),
                new SqlParameter("Precio", dprice),              
            };

            return sqlDBHelper.ExecuteNonQueryPRINTValue("sp_Almacen_Articulos_Negados", CommandType.StoredProcedure, parameters);
        }

    }
}
