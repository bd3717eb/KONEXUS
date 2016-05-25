using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;
using System.Data.SqlClient;
using IntegraData;


namespace IntegraBussines
{
    public class ControlContabilidadElectronica
    {

        public DataTable ObtieneDatosEmpresa(int numEmpresa)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", numEmpresa));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneDatosDeEmpresa_v1", true).Copy();
            return dt;
        }

        public DataTable LLenaCatalogo(int numEmpresa)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));

            dt = context.ExecuteProcedure("sp_Contabilidad_Catalogo_Homologacion_Consulta_XML", true).Copy();
            return dt;
        }

        public DataTable ObtieneHijosCatalogo(int numEmpresa, int numPadre)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Padre", numPadre));

            dt = context.ExecuteProcedure("sp_Contabilidad_Catalogo_Homologacion_Hijos", true).Copy();
            return dt;
        }



        public DataTable ObtieneNivelDeBalanza(int numEmpresa, string numNivel)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Nivel", numNivel));

            dt = context.ExecuteProcedure("sp_ObtieneNivelesBalanzaContabilidad", true).Copy();
            return dt;
        }

        public DataTable ObtieneEstatusBalanza(int numEmpresa, int Año, int Periodo)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Nuemero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Año", Año));
            context.Parametros.Add(new SqlParameter("@Periodo", Periodo));

            dt = context.ExecuteProcedure("sp_ObtieneEstatusBalanza", true).Copy();
            return dt;

        }

        public bool GeneraBalanzaDeComprovacion(int numEmpresa, int numUsuario, int año, int periodo, int nivel, int bReporte = 0, int bEstatus = 0, int tipo = 0) 
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
                context.Parametros.Add(new SqlParameter("@Usuario", numUsuario));
                context.Parametros.Add(new SqlParameter("@Año", año));
                context.Parametros.Add(new SqlParameter("@Periodo", periodo));
                context.Parametros.Add(new SqlParameter("@Nivel", nivel));
                context.Parametros.Add(new SqlParameter("@bReporte", bReporte));
                context.Parametros.Add(new SqlParameter("@bEstatus", bEstatus));
                context.Parametros.Add(new SqlParameter("@Tipo", tipo));


                context.ExecuteProcedure("sp_Genera_Balanza_Comprobacion", true).Copy();
                return true;

            }catch
            {
                return false;
            }
        }

        public DataTable ObtieneBalanzaXML(int numEmpresa, int numPersona) 
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", numPersona));

            dt= context.ExecuteProcedure("sp_Balanza_XML",true).Copy();
            return dt;
        }

        public DataTable ObtieneMapeoArchivo(int numEmpresa, int numBanco)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Banco", numBanco));

            dt = context.ExecuteProcedure("sp_ObtieneMapeoBanco", true).Copy();
            return dt;
        }

    }
}
