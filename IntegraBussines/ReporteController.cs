using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using IntegraData;
using System.Web.UI.WebControls;
namespace IntegraBussines
{
    public class ReporteController
    {
        //sSQL = "SELECT Numero, Descripcion " & _
        // "FROM Clasificacion " & _
        // "WHERE Empresa = " & intNumEmpresa & _
        // " AND Familia = 5 AND Numero_Padre = 118"
        //public static DataTable gfLlenaCboTipo(int pNumEmpresa)
        //{
        //    SQLConection context = new SQLConection();
        //    DataTable dt = new DataTable();
        //    string sQuery = string.Concat(@" SELECT Numero, Descripcion ",
        //                                   " FROM Clasificacion ",
        //                                   " WHERE Empresa = ", pNumEmpresa.ToString(), "",
        //                                   " AND Familia = 5 AND Numero_Padre = 118");
        //    dt = context.ExecuteQuery(sQuery).Copy();

        //    return dt;
        //}

        public static DataTable gfLlenaCboTipo(int pNumEmpresa)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", pNumEmpresa));

            dt = context.ExecuteProcedure("sp_ObtieneTipoTransaccion", true).Copy();

            return dt;
        }


        public static DataTable gfLlenaDatatable(string sQuery)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            dt = context.ExecuteQuery(sQuery);
            return dt;
        }

        //public static DataTable gfEstado_Mensual(int iEmpresa, int iPersona, int iAnio, int iMes)
        //{
        //    string sQuery = string.Concat("sp_Estado_Resultados_Mensual ", iEmpresa.ToString(), ",", iPersona.ToString(), ",'", iAnio.ToString(), "',", iMes.ToString(), "");
        //    DataTable dt = new DataTable();
        //    SQLConection context = new SQLConection();
        //    dt = context.ExecuteQuery(sQuery);
        //    return dt;
        //}


        public static DataTable gfEstado_Mensual(int iEmpresa, int iPersona, string iAnio, int iMes)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", iPersona));
            context.Parametros.Add(new SqlParameter("@Año", iAnio));
            context.Parametros.Add(new SqlParameter("@Periodo", iMes));

            dt = context.ExecuteProcedure("sp_Estado_Resultados_Mensual", true).Copy();
            return dt;
        }


    }
}
