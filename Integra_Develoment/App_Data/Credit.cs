using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using IntegraData;

namespace Integra_Develoment
{
    public class Credit
    {
        private int numero;
        private int numero_empresa;
        private int numero_cliente;
        private int numero_proveedor;
        private int moneda;
        private int numero_contrato;
        private int periodo_credito;
        private int clas_periodo;
        private decimal limite;
        private int dias;
        private decimal sobregiro;
        private DateTime vigenciade;
        private DateTime vigenciahasta;
        private int usuario;
        private int periodo_gracia;
        private int contado;

        public Credit()
        {
            Numero_Empresa = Convert.ToInt32(HttpContext.Current.Session["Company"]);
            Usuario = Convert.ToInt32(HttpContext.Current.Session["Person"]);
            Vigenciade = DateTime.MinValue;
            Vigenciade = DateTime.MinValue;
            Moneda = 109;
            Periodo_Credito = 5;
            clas_periodo = 5;
        }

        public Credit(int Cliente, int empresa)
        {
            Numero_Empresa = empresa;
            Numero_Cliente = Cliente;
            Numero_Proveedor = Cliente;
            Moneda = 109;
            Periodo_Credito = 5;
            Limite = 0;
            Dias = 1;
            Usuario = Convert.ToInt32(HttpContext.Current.Session["Person"]);
            Periodo_Gracia = 0;
            Contado = 0;
            clas_periodo = 5;
            Vigenciade = DateTime.MinValue;
            Vigenciade = DateTime.MinValue;
        }

        public bool insert_credit(int Tipo_Movimiento, string connectionString)
        {
            SqlConnection Conn;
            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);
                SqlCommand command = new SqlCommand("dbo.sp_Cliente_Credito", Conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoMovimiento", Tipo_Movimiento);
                command.Parameters.AddWithValue("@Numero_Empresa", Numero_Empresa);
                command.Parameters.AddWithValue("@Numero_Cliente", Numero_Cliente);
                command.Parameters.AddWithValue("@Moneda", Moneda);
                command.Parameters.AddWithValue("@Limite", Limite);
                command.Parameters.AddWithValue("@Dias", Dias);
                command.Parameters.AddWithValue("@Sobre_Giro", Sobregiro == -1 ? DBNull.Value : (object)sobregiro);
                command.Parameters.AddWithValue("@Vigencia_De", Vigenciade == DateTime.MinValue ? DBNull.Value : (object)vigenciade);
                command.Parameters.AddWithValue("@Vigencia_Hasta", Vigenciahasta == DateTime.MinValue ? DBNull.Value : (object)vigenciahasta);
                command.Parameters.AddWithValue("@Usuario", Usuario);
                command.Parameters.AddWithValue("@Periodo_Gracia", Periodo_Gracia == -1 ? DBNull.Value : (object)periodo_gracia);
                command.Parameters.AddWithValue("@Contado", Contado);
                result = command.ExecuteNonQuery();
            }
            catch (Exception sqlEx)
            {
                //logear la excepcion

                return false;
            }
            finally
            {
                if (Conn != null)
                    Conn.Close();
            }

            return true;
        }

        public bool insert_creditProvider(int Tipo_Movimiento, string connectionString)// *
        {
            SqlConnection Conn;
            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_Proveedor_Contrato", Conn);

                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("@TipoMovimiento", Tipo_Movimiento);
                command.Parameters.AddWithValue("@Numero_Empresa", Numero_Empresa);
                command.Parameters.AddWithValue("@Numero", 0);
                command.Parameters.AddWithValue("@Numero_Proveedor", Numero_Proveedor);
                command.Parameters.AddWithValue("@Numero_Contrato", Numero_Contrato);
                if (Vigenciade == DateTime.MinValue)
                    command.Parameters.AddWithValue("@Inicio_Vigencia", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Inicio_Vigencia", Vigenciade);
                if (Vigenciade == DateTime.MinValue)
                    command.Parameters.AddWithValue("@Fin_Vigencia", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Fin_Vigencia", Vigenciahasta);

                command.Parameters.AddWithValue("@Periodicidad_pago", Periodo_Gracia);
                command.Parameters.AddWithValue("@Clas_Periodo", clas_periodo);
                command.Parameters.AddWithValue("@Dia_Pago", Periodo_Gracia);
                command.Parameters.AddWithValue("@Moneda", Moneda);

                command.Parameters.AddWithValue("@Tiempo_Credito", DBNull.Value);
                command.Parameters.AddWithValue("@Periodo_Credito", periodo_credito);

                command.Parameters.AddWithValue("@Monto_Credito", Limite);

                command.Parameters.AddWithValue("@Clas_Periodo2", DBNull.Value);

                result = command.ExecuteNonQuery();
            }
            catch (Exception sqlEx)
            {
                //logear la excepcion

                return false;
            }
            finally
            {
                if (Conn != null)
                    Conn.Close();
            }

            if (result > 0)
            {
                //string Query_clientnumber = "select MAX(Numero) as Numero from  proveedor_contrato  where Numero_empresa = @companynumber";

                //SqlCommand command_clientnumber;
                //SqlDataReader data_clientnumber = null;

                //try
                //{
                //    INTEGRA.SQL.open_connection(Conn);

                //    command_clientnumber = INTEGRA.SQL.get_sqlCommand(Query_clientnumber, Conn);
                //    INTEGRA.SQL.insert_parameters(command_clientnumber, "@companynumber", numero_empresa.ToString());

                //    data_clientnumber = INTEGRA.SQL.get_dataReader(command_clientnumber);

                //    if (data_clientnumber != null)
                //    {
                //        data_clientnumber.Read();
                //        Numero = Convert.ToInt32(data_clientnumber["Numero"]);
                //    }

                //}
                //catch (Exception sqlEx)
                //{
                //    //reportar la excepcion al archivo log correspondiente 
                //    return false;
                //}
                //finally
                //{
                //    if (Conn != null)
                //        Conn.Close();
                //}

                try
                {
                    SQLConection context = new SQLConection();
                    DataTable dtConProvedor = new DataTable();

                    context.Parametros.Clear();
                    context.Parametros.Add(new SqlParameter("@companynumber", Convert.ToInt32(numero_empresa)));

                    dtConProvedor = context.ExecuteProcedure("spObtieneProvedorContacto", true).Copy();

                    if (dtConProvedor.Rows.Count > 0)
                    {
                        Numero = Convert.ToInt32(dtConProvedor.Rows[0][0]);
                    }

                }
                catch (Exception sqlEx)
                {
                    //reportar la excepcion al archivo log correspondiente  
                    return false;
                }
            }
            else
                return false;

            return true;

        }

        public bool change_creditProvider(int Tipo_Movimiento, string connectionString)
        {
            SqlConnection Conn;
            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_Proveedor_Contrato", Conn);

                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("@TipoMovimiento", Tipo_Movimiento);
                command.Parameters.AddWithValue("@Numero_Empresa", Numero_Empresa);
                command.Parameters.AddWithValue("@Numero", numero);
                command.Parameters.AddWithValue("@Numero_Proveedor", Numero_Proveedor);
                command.Parameters.AddWithValue("@Numero_Contrato", Numero_Contrato);
                if (Vigenciade == DateTime.MinValue)
                    command.Parameters.AddWithValue("@Inicio_Vigencia", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Inicio_Vigencia", Vigenciade);
                if (Vigenciade == DateTime.MinValue)
                    command.Parameters.AddWithValue("@Fin_Vigencia", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Fin_Vigencia", Vigenciahasta);

                command.Parameters.AddWithValue("@Periodicidad_pago", Periodo_Gracia);
                command.Parameters.AddWithValue("@Clas_Periodo", clas_periodo);
                command.Parameters.AddWithValue("@Dia_Pago", Dias);
                command.Parameters.AddWithValue("@Moneda", Moneda);

                command.Parameters.AddWithValue("@Tiempo_Credito", DBNull.Value);
                command.Parameters.AddWithValue("@Periodo_Credito", periodo_credito);

                command.Parameters.AddWithValue("@Monto_Credito", Limite);

                command.Parameters.AddWithValue("@Clas_Periodo2", DBNull.Value);

                result = command.ExecuteNonQuery();
            }
            catch (Exception sqlEx)
            {
                //logear la excepcion

                return false;
            }
            finally
            {
                if (Conn != null)
                    Conn.Close();
            }

            return true;

        }
        public int Numero_Empresa { get { return numero_empresa; } set { numero_empresa = value; } }
        public int Numero_Cliente { get { return numero_cliente; } set { numero_cliente = value; } }
        public int Numero { get { return numero; } set { numero = value; } }
        public int Numero_Proveedor { get { return numero_proveedor; } set { numero_proveedor = value; } }
        public int Moneda { get { return moneda; } set { moneda = value; } }
        public int Numero_Contrato { get { return numero_contrato; } set { numero_contrato = value; } }
        public int Clas_Periodo { get { return clas_periodo; } set { clas_periodo = value; } }
        public int Periodo_Credito { get { return periodo_credito; } set { periodo_credito = value; } }
        public decimal Limite { get { return limite; } set { limite = value; } }
        public int Dias { get { return dias; } set { dias = value; } }
        public decimal Sobregiro { get { return sobregiro; } set { sobregiro = value; } }
        public DateTime Vigenciade { get { return vigenciade; } set { vigenciade = value; } }
        public DateTime Vigenciahasta { get { return vigenciahasta; } set { vigenciahasta = value; } }
        public int Usuario { get { return usuario; } set { usuario = value; } }
        public int Periodo_Gracia { get { return periodo_gracia; } set { periodo_gracia = value; } }
        public int Contado { get { return contado; } set { contado = value; } }
    }
}