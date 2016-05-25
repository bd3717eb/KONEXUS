using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntegraData;
using System.Data;
using System.Data.SqlClient;

namespace IntegraBussines
{
    public abstract class PersonBLL
    {
        protected int _number;
        protected int _company;
        protected string _fullname;
        protected string _father;
        protected string _mother;
        protected string _name;
        protected string _shortname;
        protected string _rfc;
        protected string _curp;
        protected string _phonenumber;
        protected string _fax;
        protected string _cellphone;
        protected string _email;
        protected string _legal;
        protected DateTime _recorddate;
        protected int _general;

        public static DataTable gfCompaniaUsuarioDatosPersonales(int Numero_Empresa, int Numero, int general = 1)
        {
            DataTable dtTemp = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero", Numero));
            dtTemp = context.ExecuteProcedure("sp_Usuario_ObtieneDatosDelUsuario_v2", true).Copy();
            return dtTemp;

        }

        public static DataTable ObtenDatosPersona(int iNumero_Empresa, int iPersona, string sTipoPersona)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@cliente", iPersona));
            context.Parametros.Add(new SqlParameter("@persontype", sTipoPersona));


            dt = context.ExecuteProcedure("sp_Contact_ObtienePersonaFisicaPorPersonNumber_v1", true).Copy();
            return dt;
        }

        public static DataTable ObtenDomicilioPersonaPorTipoDomicilio(int iPersona, int iTipoDomicilio)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@person", iPersona));
            context.Parametros.Add(new SqlParameter("@typeAddress", iTipoDomicilio));

            dt = context.ExecuteProcedure("sp_Persona_ObtieneDomicilioFiscalPorNumeroDePersona_v1", true).Copy();
            return dt;
        }

        //public static DataTable GetfiscalAddresFromPersonNumber(int iperson, int itypeperson)
        //{
        //    Person Person = new Person();
        //    return Person.GetfiscalAddresFromPersonNumber(iperson,itypeperson);
        //}


        public static DataTable ObtenDomicilioPersona(int iPersona)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@person", iPersona));

            dt = context.ExecuteProcedure("sp_Persona_ObtieneDomicilioPorNumeroDePersona_v1", true).Copy();
            return dt;
        }
        

        public static DataTable ObtenDatosPersonaCliente(int iNumero_Empresa, int iPersona)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@personnumber", iPersona));
            context.Parametros.Add(new SqlParameter("@companynumber", iNumero_Empresa));

            dt = context.ExecuteProcedure("sp_Persona_ObtieneDatosDePersona_v3", true).Copy();
            return dt;
        }


        
        public static DataTable ObtienePersonas(int iNumero_Empresa, string sCliente)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@cliente", sCliente));

            dt = context.ExecuteProcedure("sp_WSAutoComplementoPersonas_ObtienePersonas_v1", true).Copy();
            return dt;
        }


        public static DataTable ObtenPersonaPorNumero(int iNumero_Empresa, string sPersona, string sTipoPersona)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@cliente", sPersona));
            context.Parametros.Add(new SqlParameter("@persontype", sTipoPersona));


            dt = context.ExecuteProcedure("sp_WS_ObtienePersonaFisica_v1", true).Copy();
            return dt;
        }

        public static DataTable ObtenDatosTablaPersona(int iNumero_Empresa, int iPersona)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@NumeroPersona", iPersona));
            context.Parametros.Add(new SqlParameter("@NumeroEmpresa", iNumero_Empresa));

            dt = context.ExecuteProcedure("[sp_Persona_Datos]", true).Copy();
            return dt;
        }
    }
}
