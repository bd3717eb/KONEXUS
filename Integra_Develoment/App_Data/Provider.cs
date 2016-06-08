#region Espacios_de_Nombres

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Integra_Develoment.Ventas;
using IntegraData;

#endregion

namespace Integra_Develoment
{
    public class Provider
    {
        #region Miembros

        private int contacto_proveedor;
        private int credito_proveedor;
        private int numero_persona;
        private int empresa;
        private int clasificacion_proveedor;
        private DateTime fecha_alta;
        private int estatus;
        private string giro;
        private int periodo_pago;
        private int clas_periodo_pago;
        private int? clas_formapago;
        private int clas_moneda;
        private int general;
        private int clas_sucursal;
        private int? forma_pago;
        private int tipo;
        private int tipo_proveedor;
        private int ifamily;
        private int tipo_ne;

        public Contact contacto;
        public Credit condiciones_credito;

        #endregion
        Person persona;
        #region Constructores

        public Provider(int NumeroCliente, int NumeroEmpresa, string connectionString)
        {
            int NumeroContacto;
            DataTable contactTable;

            Contacto_Proveedor = 0;
            Credito_Proveedor = 0;
            Numero_Persona = NumeroCliente;
            Empresa = NumeroEmpresa;
            Periodo_Pago = 1;
            Forma_Pago = 0;
            General = 0;
            clasificacion_proveedor = 21;
            Estatus = 9;
            Clas_formapago = 39;
            Clas_moneda = 109;
            Clas_Periodo_Pago = 5;
            tipo = 1;
            tipo_proveedor = 0;
            ifamily = 9;
            NumeroContacto = 0;
            contacto = new Contact();

            contactTable = contacto.get_contactprovider_data(NumeroCliente, NumeroEmpresa);

            if (contactTable != null)
            {

                if (contactTable.Rows.Count > 0)
                {

                    NumeroContacto = Convert.ToInt32(contactTable.Rows[0][0]);
                }

            }

            if (NumeroContacto > 0)
            {
                Contacto_Proveedor = 1;
                contacto = new Contact(Numero_Persona, NumeroContacto, NumeroEmpresa);
                contacto.domicilio_contacto = new Addres(contacto.Numero_Contacto_Persona);
            }
            else
            {
                contacto = new Contact();
                contacto.domicilio_contacto = new Addres();
            }
            DataTable TableCredit = new DataTable();
            Client client = new Client(Numero_Persona, Convert.ToInt32(INTEGRA.Global.Company), connectionString);

            TableCredit = client.GetCreditCondition(Numero_Persona, Convert.ToInt32(INTEGRA.Global.Company), ifamily);

            if (TableCredit.Rows.Count > 0)
            {
                Credito_Proveedor = 1;
                condiciones_credito = new Credit(Numero_Persona, NumeroEmpresa);
            }
            else
                condiciones_credito = new Credit();
        }

        public Provider(int NumeroEmpresa)
        {
            Contacto_Proveedor = 0;
            Credito_Proveedor = 0;
            Numero_Persona = 0;
            Empresa = NumeroEmpresa;
            Periodo_Pago = 1;
            Forma_Pago = 0;
            General = 0;
            clasificacion_proveedor = 21;
            Estatus = 9;
            Clas_formapago = 39;
            Clas_moneda = 109;
            Clas_Periodo_Pago = 5;
            tipo = 1;
            tipo_proveedor = 0;

        }

        #endregion

        #region Asignaciones

        public int Contacto_Proveedor { get { return contacto_proveedor; } set { contacto_proveedor = value; } }
        public int Credito_Proveedor { get { return credito_proveedor; } set { credito_proveedor = value; } }
        public int Empresa { get { return empresa; } set { empresa = value; } }
        public int Clasificacion_Proveedor { get { return clasificacion_proveedor; } set { clasificacion_proveedor = value; } }
        public DateTime Fecha_Alta { get { return fecha_alta; } set { fecha_alta = value; } }
        public int Estatus { get { return estatus; } set { estatus = value; } }
        public string Giro { get { return giro; } set { giro = value; } }
        public int Periodo_Pago { get { return periodo_pago; } set { periodo_pago = value; } }
        public int Clas_Periodo_Pago { get { return clas_periodo_pago; } set { clas_periodo_pago = value; } }
        public int General { get { return general; } set { general = value; } }
        public int Numero_Persona { get { return numero_persona; } set { numero_persona = value; } }
        public int Clas_Sucursal { get { return clas_sucursal; } set { clas_sucursal = value; } }
        public int? Forma_Pago { get { return forma_pago; } set { forma_pago = value; } }
        public int? Clas_formapago { get { return clas_formapago; } set { clas_formapago = value; } }
        public int Clas_moneda { get { return clas_moneda; } set { clas_moneda = value; } }
        public int Tipo { get { return tipo; } set { tipo = value; } }
        public int Tipo_Proveedor { get { return tipo_proveedor; } set { tipo_proveedor = value; } }
        public int Tipo_NE { get { return tipo_ne; } set { tipo_ne = value; } }

        #endregion

        #region Procedimientos_Almacenados



        public bool sp_proveedor(string connectionString, int tipo, string Nombre_Completo, int numero)
        {
            SqlConnection Conn;
            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_proveedor", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TipoMovimiento ", tipo);
                command.Parameters.AddWithValue("@iNumEmpresa", Empresa);
                command.Parameters.AddWithValue("@Numero_Persona", numero);
                command.Parameters.AddWithValue("@iCProveedor", clasificacion_proveedor);
                command.Parameters.AddWithValue("@iEstatus", Estatus);
                command.Parameters.AddWithValue("@Obs", DBNull.Value);
                command.Parameters.AddWithValue("@Nombre_Completo", Nombre_Completo);
                command.Parameters.AddWithValue("@Tipo", tipo_proveedor);
                command.Parameters.AddWithValue("@Forma_Pago", forma_pago);
                command.Parameters.AddWithValue("@Clas_Forma_Pago", clas_formapago);
                command.Parameters.AddWithValue("@Clas_Moneda", clas_moneda);
                command.Parameters.AddWithValue("@Tipo_NE", tipo_ne);


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

                SQLConection context = new SQLConection();
                DataTable dtPersonaProv = new DataTable();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", Convert.ToInt32(Empresa)));

                dtPersonaProv = context.ExecuteProcedure("spObtienePersonaProvedor", true).Copy();

                if (dtPersonaProv.Rows.Count > 0)
                {
                    Numero_Persona = Convert.ToInt32(dtPersonaProv.Rows[0][0]);
                }
            }
            else
                return false;

            return true;
        }


        public bool ModificaPersonaProveedor(string connectionString, string Nombre_Completo, string Paterno, string Materno, string Nombre, string Nombre_Corto,
                                             string Telefono, string Correo_Electronico, string PersonalidadJuridica, string RFC, string CURP, int Clasificacion_Proveedor)
        {
            SqlConnection Conn;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_ModificaPersonaProveedor", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Empresa", Convert.ToInt32(Empresa));
                command.Parameters.AddWithValue("@Numero_Persona", numero_persona);

                //Persona

                command.Parameters.AddWithValue("@Nombre_Completo", Nombre_Completo);
                command.Parameters.AddWithValue("@Paterno", Paterno);
                command.Parameters.AddWithValue("@Materno", Materno);
                command.Parameters.AddWithValue("@Nombre", Nombre);
                command.Parameters.AddWithValue("@NombreCorto", Nombre_Corto);
                command.Parameters.AddWithValue("@TelefonoCliente", Telefono);
                command.Parameters.AddWithValue("@Fax", Telefono);
                command.Parameters.AddWithValue("@Email", Correo_Electronico);

                //Cliente

                command.Parameters.AddWithValue("@Clasificacion_proveedor", Clasificacion_Proveedor);
                command.Parameters.AddWithValue("@RFC", RFC);
                command.Parameters.AddWithValue("@sCurp", CURP);
                command.Parameters.AddWithValue("@General", general);
                command.Parameters.AddWithValue("@Estatus", Estatus);
                command.Parameters.AddWithValue("@Forma_Pago", forma_pago);
                command.Parameters.AddWithValue("@Clas_Forma_Pago", clas_formapago);
                command.Parameters.AddWithValue("@Personalidad_Juridica", PersonalidadJuridica);

                command.ExecuteNonQuery();
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


        public bool sp_proveedorElimina(string connectionString, int tipo, string Nombre_Completo, int tipo_proveedor, int numero)
        {
            SqlConnection Conn;
            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_proveedor", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TipoMovimiento ", tipo);
                command.Parameters.AddWithValue("@iNumEmpresa", Empresa);
                command.Parameters.AddWithValue("@Numero_Persona", numero);
                command.Parameters.AddWithValue("@iCProveedor", clasificacion_proveedor);
                command.Parameters.AddWithValue("@iEstatus", Estatus);
                command.Parameters.AddWithValue("@Obs", DBNull.Value);
                command.Parameters.AddWithValue("@Nombre_Completo", Nombre_Completo);
                command.Parameters.AddWithValue("@Tipo", tipo_proveedor);
                command.Parameters.AddWithValue("@Forma_Pago", forma_pago);
                command.Parameters.AddWithValue("@Clas_Forma_Pago", clas_formapago);
                command.Parameters.AddWithValue("@Clas_Moneda", clas_moneda);


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
                return true;
            else
                return false;
        }

        #endregion

        #region Metodos

        //public bool get_provider_data_from_clientnumber(string connectionString)//hacer sp que traiga todos los datos del proveedor
        //{
        //    SqlConnection Conn;
        //    SqlCommand command_client;
        //    SqlDataReader data_client;

        //    string Query_client = "SELECT * from Proveedor where Numero_Persona = @personnumber";

        //    Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

        //    try
        //    {
        //        INTEGRA.SQL.open_connection(Conn);

        //        command_client = INTEGRA.SQL.get_sqlCommand(Query_client, Conn);
        //        INTEGRA.SQL.insert_parameters(command_client, "@personnumber", Numero_Persona.ToString());

        //        data_client = INTEGRA.SQL.get_dataReader(command_client);
        //        data_client.Read();

        //        Clasificacion_Proveedor = (data_client["Clasificacion_proveedor"] == DBNull.Value) ? 0 : Convert.ToInt32(data_client["Clasificacion_proveedor"]);
        //        Fecha_Alta = Convert.ToDateTime(data_client["Fecha_Alta"]);
        //        Estatus = (data_client["Estatus"] == DBNull.Value) ? 0 : Convert.ToInt32(data_client["Estatus"]);
        //        Forma_Pago = (data_client["Forma_Pago"] == DBNull.Value) ? 0 : Convert.ToInt32(data_client["Forma_Pago"]);
        //        Tipo_Proveedor = (data_client["Tipo"] == DBNull.Value) ? 0 : Convert.ToInt32(data_client["Tipo"]);
        //        Clas_formapago = (data_client["Clas_Forma_Pago"] == DBNull.Value) ? 0 : Convert.ToInt32(data_client["Clas_Forma_Pago"]);
        //        Clas_moneda = (data_client["Clas_Moneda"] == DBNull.Value) ? 0 : Convert.ToInt32(data_client["Clas_Moneda"]);
        //    }
        //    catch (Exception sqlEx)
        //    {
        //        //logear la excepcion

        //        return false;
        //    }
        //    finally
        //    {
        //        if (Conn != null)
        //            Conn.Close();
        //    }

        //    return true;
        //}


        public bool get_provider_data_from_clientnumber(string connectionString)//hacer sp que traiga todos los datos del proveedor
        {
            SQLConection context = new SQLConection();
            DataTable dtProvedor = new DataTable();

            try
            {
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@personnumber", true));

                dtProvedor = context.ExecuteProcedure("spObtieneDatosProvedor", true).Copy();

                if (dtProvedor.Rows.Count > 0)
                {
                    Clasificacion_Proveedor = (dtProvedor.Rows[0]["Clasificacion_proveedor"] == DBNull.Value) ? 0 : Convert.ToInt32(dtProvedor.Rows[0]["Clasificacion_proveedor"]);
                    Fecha_Alta = Convert.ToDateTime(dtProvedor.Rows[0]["Fecha_Alta"]);
                    Estatus = (dtProvedor.Rows[0]["Estatus"] == DBNull.Value) ? 0 : Convert.ToInt32(dtProvedor.Rows[0]["Estatus"]);
                    Forma_Pago = (dtProvedor.Rows[0]["Forma_Pago"] == DBNull.Value) ? 0 : Convert.ToInt32(dtProvedor.Rows[0]["Forma_Pago"]);
                    Tipo_Proveedor = (dtProvedor.Rows[0]["Tipo"] == DBNull.Value) ? 0 : Convert.ToInt32(dtProvedor.Rows[0]["Tipo"]);
                    Clas_formapago = (dtProvedor.Rows[0]["Clas_Forma_Pago"] == DBNull.Value) ? 0 : Convert.ToInt32(dtProvedor.Rows[0]["Clas_Forma_Pago"]);
                    Clas_moneda = (dtProvedor.Rows[0]["Clas_Moneda"] == DBNull.Value) ? 0 : Convert.ToInt32(dtProvedor.Rows[0]["Clas_Moneda"]);

                }
            }
            catch (Exception sqlEx)
            {
                //logear la excepcion

                return false;
            }

            return true;
        }



        #endregion

        #region Destructor

        ~Provider()
        {
        }

        #endregion
    }
}