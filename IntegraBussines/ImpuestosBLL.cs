using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IntegraData;


namespace IntegraBussines
{
    public class ImpuestosBLL
    {
        public static decimal ObtienePreciosConImpuestosSinIVA(int intEmpresa, int intClasProducto, int intClasConcepto,int intFamilia,decimal dPrecio)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            decimal dPrecioResultado = 0;

            string sQuery = "SELECT dbo.fn_Obten_Producto_Impuestos_SIVA(" + intEmpresa + "," + intClasProducto + "," + intClasConcepto + "," + intFamilia + "," + dPrecio + ")";
            try
            {

                dt = context.ExecuteQuery(sQuery).Copy();
                dPrecioResultado = Convert.ToDecimal(dt.Rows[0][0]);
                return dPrecioResultado;

            }
            catch
            {
                return 0;
            }

        }

     
    }
}
