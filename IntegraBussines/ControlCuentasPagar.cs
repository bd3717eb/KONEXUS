using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IntegraData;

namespace IntegraBussines
{
    public class ControlCuentasPagar
    {
        static private string snombre_completo;
        private string snumero;



        public string sNumero { get { return snumero; } set { snumero = value; } }
        public string sNombre_Completo { get { return snombre_completo; } set { snombre_completo = value; } }
       


        // OBTIENE NUMERO Y NOMBRE 
        public DataTable obtDatosxNumeroPersona(int optPersonalidad, int intNumEmpresa, string sCliente)
        {
            SQLConection context = new SQLConection();

            //parametros q enviare al proc
            context.Parametros.Add(new SqlParameter("@optPersonalidad", optPersonalidad));
            context.Parametros.Add(new SqlParameter("@intNumEmpresa", intNumEmpresa));
            context.Parametros.Add(new SqlParameter("@sCliente", sCliente));

            DataTable dt = new DataTable();
            // sp_Persona_ObtieneDatosDePersona_v1 - Procedimiento que se debe cambiar asp_CP0600_ClienteDropDown
            dt = context.ExecuteProcedure("sp_CP0600_ObtNomCliente", true);
            DataTable DataPerson = dt;

            if (dt != null && dt.Rows.Count > 0)
            {

                //snumero = (DataPerson.Rows[0][0] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][1]);
                sNombre_Completo = (DataPerson.Rows[0][1] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][1]);

                return dt;
            }
            else
            {
                return null;
            }

        }


        //OBTIENE TIPO PERSONA
        public DataTable ObtieneTipo(int optPersonalidad, int intNumEmpresa, int txtCliente)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@optPersonalidad", optPersonalidad));
            context.Parametros.Add(new SqlParameter("@intNumEmpresa", intNumEmpresa));
            context.Parametros.Add(new SqlParameter("@txtCliente", txtCliente));

            dt = context.ExecuteProcedure("sp_CP0600_Tipo_Persona", true);

            if (dt != null)
            //if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return new DataTable();
                //return null;
            }
        }

        //OBTIENE TIPO MONEDA
        public DataTable ObtieneMoneda(int empresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@empresa", empresa));

            dt = context.ExecuteProcedure("sp_CP0600_llenaMoneda", true);

            if (dt != null)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }

        //OBTIENE PROVEEDOR DE AUTOCOMPLETE
        public DataTable obtenerProveedorDeAutocomplete(int optPersonalidad, int intNumEmpresa, string sCliente)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@optPersonalidad", optPersonalidad));
            context.Parametros.Add(new SqlParameter("@intNumEmpresa", intNumEmpresa));
            context.Parametros.Add(new SqlParameter("@sCliente", sCliente));

            dt = context.ExecuteProcedure("sp_CP0600_ObtNomCliente", true);

            if (dt != null)
            {
                return dt;
            }
            else 
            {
                return new DataTable();
            }

        }

        //CONSULTA SIMPLE
        public DataTable sqlConsultaGrid(int intNumEmpresa, int NumUsuario, int NumProveedor, String dtpFecha, int cboTipo, int cboMoneda)
        {
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@intNumEmpresa", intNumEmpresa));
            context.Parametros.Add(new SqlParameter("@NumUsuario", NumUsuario));
            context.Parametros.Add(new SqlParameter("@NumProveedor", NumProveedor));
            context.Parametros.Add(new SqlParameter("@dtpFecha", dtpFecha));
            context.Parametros.Add(new SqlParameter("@cboTipo", cboTipo));
            context.Parametros.Add(new SqlParameter("@cboMoneda", cboMoneda));

            DataTable dt = new DataTable();
            dt = context.ExecuteProcedure("sp_CP0600_ConsultaDetallada", true);
            return dt;
        }

        //CONSULTA DETALLADA
        public DataTable sqlConsultaDetalladaGrid(int NumEmpresa, int NumUsuario, int Numero_Proveedor, int Clas_Moneda, int Tipo_Cliente, int Periodo1, int Periodo2, int Periodo3, String Fecha)
        {
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@NumEmpresa", NumEmpresa));
            context.Parametros.Add(new SqlParameter("@NumUsuario", NumUsuario));
            context.Parametros.Add(new SqlParameter("@NumeroProveedor", Numero_Proveedor));
            context.Parametros.Add(new SqlParameter("@Clas_Moneda", Clas_Moneda));
            context.Parametros.Add(new SqlParameter("@Tipo_Cliente", Tipo_Cliente));
            context.Parametros.Add(new SqlParameter("@Periodo1", Periodo1));
            context.Parametros.Add(new SqlParameter("@Periodo2", Periodo2));
            context.Parametros.Add(new SqlParameter("@Periodo3", Periodo3));
            context.Parametros.Add(new SqlParameter("@Fecha", Fecha));

            DataTable dt = new DataTable();
            dt = context.ExecuteProcedure("sp_CP0600_ConsultaDetallada", true);
            return dt;
        }


    }

}
