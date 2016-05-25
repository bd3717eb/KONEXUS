using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using IntegraData;

namespace IntegraBussines
{
    public class ControlReportBalance
    {


        public bool GeneraBalanceGeneral(int numempresa, int numPersona, string año, int periodo) 
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", numempresa));
                context.Parametros.Add(new SqlParameter("@Usuario", numPersona));
                context.Parametros.Add(new SqlParameter("@Año", año));
                context.Parametros.Add(new SqlParameter("@Periodo", periodo));

                context.ExecuteProcedure("sp_Balance_General", true).Copy();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public DataTable ObtieneBalanceGeneral(int numEmpresa, int numUsuario)
        {
            DataTable dt = new DataTable();
            try {

                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Usuario", numUsuario));

                dt = context.ExecuteProcedure("sp_ObtieneBalanceGeneral", true).Copy();
                return dt;
            }
            catch(Exception ex) 
            {
                return dt;
            }
        }

        public DataTable GeneraBalanzaDeComprobacionReporte(int tipoBalanza, int tipoNivel, int numEmpresa, int numUsuario) 
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Balanza", tipoBalanza));
            context.Parametros.Add(new SqlParameter("@TipoNivel", tipoNivel));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", numUsuario));

            dt = context.ExecuteProcedure("sp_GeneraBalanzaComprobacionPrueba_V3", true).Copy();
            return dt;

        }

        public DataTable ObtieneNivelBalanza() 
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            dt = context.ExecuteProcedure("sp_ObtieneNivelesBalanza", false).Copy();
            return dt;        
        }

        public DataTable ObtieneEstatusBalanza(int numEmpresa, string año, string periodo) 
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Nuemero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Año", año));
            context.Parametros.Add(new SqlParameter("@Periodo", periodo));

            dt = context.ExecuteProcedure("sp_ObtieneEstatusBalanza", true).Copy();
            return dt;

        }

        public DataTable ObtieneNumeroBalanzaCerrada(int numEmpresa, string año, string periodo)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Nuemero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Año", año));
            context.Parametros.Add(new SqlParameter("@Periodo", periodo));

            dt = context.ExecuteProcedure("sp_ObtieneNumeroBalazaCerrada", true).Copy();
            return dt;

        }

        public bool GeneraBalanzaComprobacion(int numEmpresa, int numUsuario, int año, int periodo, int nivel, int reporte, 
                                            int estatus, int tipo) 
        {
            try{

            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", numUsuario));
            context.Parametros.Add(new SqlParameter("@Año", año));
            context.Parametros.Add(new SqlParameter("@Periodo",periodo));
            context.Parametros.Add(new SqlParameter("@Nivel",nivel));
            context.Parametros.Add(new SqlParameter("@bReporte",reporte));
            context.Parametros.Add(new SqlParameter("@bEstatus",estatus));
            context.Parametros.Add(new SqlParameter("@Tipo", tipo));

            context.ExecuteProcedure("sp_Genera_Balanza_Comprobacion", true).Copy(); //tenia version v2
            return true;

            }catch
            {
                return false;
            }
        }

        public DataTable ObtieneEstadoResultado(int numEmpresa, int numUsuario) 
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", numUsuario));

            dt = context.ExecuteProcedure("sp_ObtieneEstadoResultadoensual", true).Copy();
            return dt;

        }

        public DataTable ObtieneInfoPoliza(int numEmpresa, DateTime? fechaInicio, DateTime? fechaFin, int MesIni, string AñoIni,  int MesFin, string AñoFin, string tipoPoliza) 
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@FechaInicio", fechaInicio.HasValue ? (object)fechaInicio.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechaFin", fechaFin.HasValue ? (object)fechaFin.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@MESIni", MesIni));
            context.Parametros.Add(new SqlParameter("@AÑOIni", AñoIni));
            context.Parametros.Add(new SqlParameter("@MESFin", MesFin));
            context.Parametros.Add(new SqlParameter("@AÑOFin", AñoFin));
            context.Parametros.Add(new SqlParameter("@Tipo_Poliza", tipoPoliza));

            dt = context.ExecuteProcedure("sp_ObtieneInfoPoliza", true).Copy();
            return dt;
        }

        public DataTable ObtieneReporteContablePoliza(int numEmpresa, int periodo, string año) 
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa",numEmpresa));
            context.Parametros.Add(new SqlParameter("@Periodo",periodo));
            context.Parametros.Add(new SqlParameter("@Año", año));

            dt= context.ExecuteProcedure("sp_ObtienContabilidadPolizas",true).Copy();
            return dt;
        }
    }
}
