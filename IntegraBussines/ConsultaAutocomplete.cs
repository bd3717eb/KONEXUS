using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using IntegraBussines;
using IntegraData;

namespace Integra_Develoment
{
    public class ConsultaAutocomplete
    {

        private int variabledinamica {get;set;}
        private int[] avariabledinamica {get;set;}
        
        #region Clientes

        public DataTable obtenerClientesDeAutocomplete(int empresa, string cliente)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@companynumber", empresa));
            context.Parametros.Add(new SqlParameter("@cliente", cliente));

            dt = context.ExecuteProcedure("sp_WSAutoComplementoCliente_ObtieneCliente_v1", true);

            if (dt != null)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }


        }

        public  static DataTable traePersonas(int Empresa, string Cliente, string Persona_Juridica)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@companynumber", Empresa));
            context.Parametros.Add(new SqlParameter("@cliente", Cliente));
            context.Parametros.Add(new SqlParameter("@persontype", Persona_Juridica));

            dt = context.ExecuteProcedure("sp_WS_ObtienePersonaFisica_v1", true);

            if (dt != null)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }
        public static DataTable traeContactosCliente(int iEmpresa, int iCliente,string sContacto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@customernumber", iCliente));
            context.Parametros.Add(new SqlParameter("@contacto", sContacto));
          


            dt = context.ExecuteProcedure("sp_WS_ClienteContactos", true);

            if (dt != null)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }
        public DataTable gfCuentasContables(int Numero_Empresa, string Nombre_Completo)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            int iOpcion = 1;
            context.Parametros.Clear();
            try
            {
                int.Parse(Nombre_Completo);
                iOpcion = 1;
            }
            catch
            {
                iOpcion = 0;

                if (Nombre_Completo.Contains('-'))
                {
                    iOpcion = 1;
                }
               
            }
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Nombre_Completo", Nombre_Completo));
            context.Parametros.Add(new SqlParameter("@Opcion", iOpcion));
            dt = context.ExecuteProcedure("sp_WSAutoComplementoCuentasContables", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }



        public DataTable gfBandasDescuento(int Numero_Empresa, string Nombre_Completo)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
         
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Banda", Nombre_Completo));

            dt = context.ExecuteProcedure("sp_Catalogo_Bandas_Autocomplit", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        public DataTable gfNodosXML(int Numero_Empresa, string Nombre_Completo)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Nodo", Nombre_Completo));

            dt = context.ExecuteProcedure("sp_Catalogo_Nodos_Autocomplit", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }


        #endregion


    }
}