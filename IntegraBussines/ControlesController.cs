using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IntegraData;
//using Ionic.Zip;

namespace IntegraBussines
{
    public class ControlesController
    {
        public DataTable gfObtieneClasificacion(int Numero_Empresa, int iPadre, int iFamilia)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@companynumber", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@father", iPadre));
            context.Parametros.Add(new SqlParameter("@family", iFamilia));

            dt = context.ExecuteProcedure("sp_Controles_ObtienePaises_v1", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        public static DataTable gfObtieneEmail(int picompanynumber, int picliente)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", picompanynumber));
            context.Parametros.Add(new SqlParameter("@cliente", picliente));
            context.Parametros.Add(new SqlParameter("@sendemail", 1));
            dt = context.ExecuteProcedure("sp_Venta_ObtieneDatosDeCorreo_v1", true);

            //dt = context.ExecuteQuery("SELECT 'eblaher@outlook.com' AS Correo_Electronico	, 'Esteban Blanquel' AS Nombre_Completo");
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        public DataTable gfObtieneSucursales(int iNumero_Empresa, int iUsuario)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@companynumber", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@user", iUsuario));
         

            dt = context.ExecuteProcedure("sp_Controles_ObtieneOficinas_v1", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        public DataTable gfObtieneDocumentos(int iNumero_Empresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numero_empresa", iNumero_Empresa));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneDocumentos", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        public DataTable gfObtieneDocumentosParaFactura(int iNumero_Empresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numero_empresa", iNumero_Empresa));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneDocumentosParaFactura", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        public DataTable gfObtieneSeries(int iNumero_Empresa,int iDocumento, int iUsuario)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numero_empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@numero_usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("@tipo_documento", iDocumento));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneSeries", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }
  

    }
}
