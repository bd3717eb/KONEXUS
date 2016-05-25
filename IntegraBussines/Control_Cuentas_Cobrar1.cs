using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using IntegraData;

//namespace IntegraBussines
namespace Integra_Develoment
{
    public class Control_Cuentas_Cobrar1
    {

        static private string snombre_completo;
        private string sempresa;
        //private string idCliente;

        public string Sempresa
        {
            get { return sempresa; }
            set { sempresa = value; }
        }

        public string sNombre_Completo { get { return snombre_completo; } set { snombre_completo = value; } }
        public string sEmpresa { get { return sempresa; } set { sempresa = value; } }


        //En este metodo se realiza la consulta de los datos de la person seleccionada y los acomoda en una tabla
        public DataTable obtenerDatosDeProveedor(int Empresa, string nom_provedor)
        //public DataTable obtenerDatosDeProveedor(int Empresa, string idCliente)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@empresa", Empresa));
            context.Parametros.Add(new SqlParameter("@nom_provedor", nom_provedor));
            //context.Parametros.Add(new SqlParameter("@numeroCliente", idCliente));

            dt = context.ExecuteProcedure("sp_CC0700_cboClienteText_V2", true);
            DataTable DataPerson = dt;

            if (dt != null && dt.Rows.Count > 0)
            {
                //idCliente = (DataPerson.Rows[0][0] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][0]);
                sNombre_Completo = (DataPerson.Rows[0][1] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][1]);
                return dt;
            }
            else
            {
                return null;
            }
        }

        //Medoto para llenar DropDownList ddl_TipoMoneda Consultando un StoreProcedure 
        public DataTable obtieneDatosTipoMoneda(int empresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@empresa", empresa));

            dt = context.ExecuteProcedure("sp_CC0700_llena_moneda", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        //Medoto para llenar DropDownList ddl_tipo_cliente Consultando un StoreProcedure  
        public DataTable obtieneDatosTipoCliente(int numEmpresa, int idCliente)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@txtCliente", idCliente));


            dt = context.ExecuteProcedure("sp_CC0700_tipo_cliente", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        //Metodo para llenar GridView Con procedimiento almacenado sp_CC0700_consultaDatosUsuario_VS
        public DataTable obtieneDatos_Llena_grv_Detallado(int numEmpresa, int numUsuario, int numCliente, int tipoCliente, int clasMoneda, string fecha, int periodo1, int periodo2, int periodo3)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", numUsuario));
            context.Parametros.Add(new SqlParameter("@numero_cliente", numCliente));
            context.Parametros.Add(new SqlParameter("@Tipo_Cliente", tipoCliente));
            context.Parametros.Add(new SqlParameter("@Clas_Moneda", clasMoneda));
            context.Parametros.Add(new SqlParameter("@Fecha", fecha));
            context.Parametros.Add(new SqlParameter("@Periodo1", periodo1));
            context.Parametros.Add(new SqlParameter("@Periodo2", periodo2));
            context.Parametros.Add(new SqlParameter("@Periodo3", periodo3));

            dt = context.ExecuteProcedure("sp_CC0700_consultaDatosUsuarioFinal", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        //Metodo para obtener los parametros por default de los periodos de cuentas_cobrar
        public DataTable obtieneDatos_defaultPeriodos(int numEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@intNumEmpresa", numEmpresa));

            dt = context.ExecuteProcedure("sp_CC0700_parametrosDefaultPeriodos_VS", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }

        //METODOS DE PRUEBA 

        ////En este metodo se realiza la consulta de tipo de moneda para el webForm cuentas_cobrar1 del provedor seleccionada y los acomoda en una tabla
        //public DataTable obtenerDatosTipoMoneda(int Empresa)
        //{
        //    SQLConection context = new SQLConection();
        //    context.Parametros.Clear();
        //    context.Parametros.Add(new SqlParameter("@empresa", Empresa));

        //    DataTable dt = new DataTable();
        //    dt = context.ExecuteProcedure("sp_CC0700_llena_moneda", true);
        //    DataTable DataPerson = dt;

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        sempresa = (DataPerson.Rows[0][1] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][1]);

        //        return dt;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

    }
}
