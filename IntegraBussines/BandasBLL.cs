using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IntegraData;


namespace IntegraBussines
{
    public class BandasBLL
    {


        public static int sp_Catalogo_Bandas(int intEmpresa, int intId, string sDescripcion, int intZona,
                                      decimal dImporteMinimo, int intNumero, decimal dUtilidad_Min,
                                      decimal dUtilidad_Max, decimal dDescuento, int intTipo, int intTipoDetalle)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                       
            new SqlParameter("Numero_Empresa", intEmpresa),
            new SqlParameter("ID", intId),
            new SqlParameter("Descripcion", sDescripcion),
            new SqlParameter("Clas_Zona", intZona),
            new SqlParameter("Importe_Min", dImporteMinimo),
            new SqlParameter("Numero", intNumero),
            new SqlParameter("Utilidad_Min", dUtilidad_Min),
            new SqlParameter("Utilidad_Max", dUtilidad_Max),
            new SqlParameter("Descuento", dDescuento),
            new SqlParameter("Tipo", intTipo),
            new SqlParameter("Tipo_Detalle", intTipoDetalle),
        
        };
            try
            {
                
                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        if (param.ParameterName == "ID" && intId == 0)
                        {
                            param.Direction = ParameterDirection.Output;
                        }
                    }
                }

                return sqlDBHelper.ExecuteNonQueryOutputValue("sp_Catalogo_Bandas", "ID", CommandType.StoredProcedure, parameters);
                
            }
            catch (Exception)
            {

                return -1;
            }

        }



        public  static DataTable sp_Catalogo_Bandas_Consulta(int intTipo, int intNumeroEmpresa, int intID)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo", intTipo));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intNumeroEmpresa));
            context.Parametros.Add(new SqlParameter("@ID", intID));
        
            dt = context.ExecuteProcedure("sp_Catalogo_Bandas_Consulta", true).Copy();
            return dt;
        }


        public static DataTable ObtienePreciosConImpuestos(int intFamilia, int intEmpresa, int intClasProducto, int intClasConcepto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();


            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Familia", intFamilia));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Clas_Producto", intClasProducto));
            context.Parametros.Add(new SqlParameter("@Clas_Concepto", intClasConcepto));

            dt = context.ExecuteProcedure("sp_Precio_Impuesto", true);
            return dt;



        }
    
    }
}
