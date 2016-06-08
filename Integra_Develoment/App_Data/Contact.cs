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
    public class Contact
    {
        private string nombre_completo;
        private string paterno;
        private string materno;
        private string nombre;
        private string telefono;
        private string celular;
        private string correo_electronico;
        private int numero_cliente;
        private int numero_proveedor;
        private int numero_contacto_persona;
        private string puesto;
        private int empresa;
        private int envio_mail;
        private int tipo;
        private int numero_contacto;
        public Addres domicilio_contacto;

        public Contact()
        {
            domicilio_contacto = new Addres();
        }

        public Contact(int Cliente, int Contacto, int empresa)
        {
            Nombre_Completo = null;
            Paterno = null;
            Materno = null;
            Nombre = null;
            Telefono = null;
            Celular = null;
            Correo_Electronico = null;
            Numero_Cliente = Cliente;
            Numero_Proveedor = Cliente;
            Numero_Contacto_Persona = Contacto;
            Numero_Contacto = Contacto;
            Puesto = null;
            Empresa = Convert.ToInt32(HttpContext.Current.Session["Company"]);
            Envio_Mail = 0;
            tipo = 0;
            domicilio_contacto = new Addres(Cliente);
        }

        public string Nombre_Completo { get { return nombre_completo; } set { nombre_completo = value; } }
        public string Paterno { get { return paterno; } set { paterno = value; } }
        public string Materno { get { return materno; } set { materno = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Telefono { get { return telefono; } set { telefono = value; } }
        public string Celular { get { return celular; } set { celular = value; } }
        public string Correo_Electronico { get { return correo_electronico; } set { correo_electronico = value; } }
        public int Numero_Cliente { get { return numero_cliente; } set { numero_cliente = value; } }
        public int Numero_Proveedor { get { return numero_proveedor; } set { numero_proveedor = value; } }
        public int Numero_Contacto_Persona { get { return numero_contacto_persona; } set { numero_contacto_persona = value; } }
        public int Numero_Contacto { get { return numero_contacto; } set { numero_contacto = value; } }
        public string Puesto { get { return puesto; } set { puesto = value; } }
        public int Empresa { get { return empresa; } set { empresa = value; } }
        public int Tipo { get { return tipo; } set { tipo = value; } }
        public int Envio_Mail { get { return envio_mail; } set { envio_mail = value; } }

        public bool insert_contac(int Tipo_Movimiento, string connectionString)//*
        {
            SqlConnection Conn;

            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_Contactos", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TipoMovimiento", Tipo_Movimiento);
                command.Parameters.AddWithValue("@Nombre_Completo", Nombre + "  " + Paterno + "  " + Materno);
                command.Parameters.AddWithValue("@Paterno", Paterno);
                command.Parameters.AddWithValue("@Materno", Materno);
                command.Parameters.AddWithValue("@Nombre", Nombre);
                command.Parameters.AddWithValue("@Telefono", Telefono);
                command.Parameters.AddWithValue("@Celular", Celular);
                command.Parameters.AddWithValue("@Email", Correo_Electronico);
                command.Parameters.AddWithValue("@Numero_Cliente", Numero_Cliente);
                command.Parameters.AddWithValue("@Numero_Contacto_Persona", Numero_Contacto_Persona);
                command.Parameters.AddWithValue("@Puesto", Puesto);
                command.Parameters.AddWithValue("@Empresa", Empresa);
                command.Parameters.AddWithValue("@Envio_Mail", Envio_Mail);

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
                //string Query_addresnumber = "select MAX(Numero_Contacto_Persona) as Numero from contacto where Empresa = @companynumber";

                //SqlCommand command_contac;
                //SqlDataReader data_contac = null;

                //try
                //{
                //    INTEGRA.SQL.open_connection(Conn);

                //    command_contac = INTEGRA.SQL.get_sqlCommand(Query_addresnumber, Conn);
                //    INTEGRA.SQL.insert_parameters(command_contac, "@companynumber", Empresa.ToString());

                //    data_contac = INTEGRA.SQL.get_dataReader(command_contac);

                //    if (data_contac != null)
                //    {
                //        data_contac.Read();
                //        Numero_Contacto_Persona = Convert.ToInt32(data_contac["Numero"]);
                //        //domicilio_contacto.Numero_Persona = Numero_Contacto_Persona;
                //        data_contac.Close();
                //        data_contac.Dispose();
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
                    DataTable dtConPersona = new DataTable();

                    context.Parametros.Clear();
                    context.Parametros.Add(new SqlParameter("@companynumber", Convert.ToInt32(Empresa)));

                    dtConPersona = context.ExecuteProcedure("spObtieneContactoPersona", true).Copy();

                    if (dtConPersona.Rows.Count > 0)
                    {
                        Numero_Contacto_Persona = Convert.ToInt32(dtConPersona.Rows[0][0]);
                    }

                }
                catch (Exception exSql)
                {
                    //reportar la excepcion al archivo log correspondiente
                    return false;
                }
            }

            return true;
        }
        public bool insert_contacprovider(int Tipo_Movimiento, string connectionString)//*
        {
            SqlConnection Conn;

            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_Contacto_Proveedor", Conn);


                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoMovimiento", Tipo_Movimiento);
                command.Parameters.AddWithValue("@NumEmpresa", Empresa);
                command.Parameters.AddWithValue("@Numero_Proveedor", Numero_Proveedor);
                command.Parameters.AddWithValue("@Numero_Contacto", numero_contacto_persona);
                command.Parameters.AddWithValue("@sPuesto", Puesto);


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
                //string Query_addresnumber = "select MAX(Numero_Contacto) as Numero from Contacto_Proveedor where Numero_Empresa = @companynumber";

                //SqlCommand command_contac;
                //SqlDataReader data_contac = null;

                //try
                //{
                //    INTEGRA.SQL.open_connection(Conn);

                //    command_contac = INTEGRA.SQL.get_sqlCommand(Query_addresnumber, Conn);
                //    INTEGRA.SQL.insert_parameters(command_contac, "@companynumber", Empresa.ToString());

                //    data_contac = INTEGRA.SQL.get_dataReader(command_contac);

                //    if (data_contac != null)
                //    {
                //        data_contac.Read();
                //        Numero_Contacto = Convert.ToInt32(data_contac["Numero"]);
                //        //domicilio_contacto.Numero_Persona = Numero_Contacto_Persona;
                //        data_contac.Close();
                //        data_contac.Dispose();
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
                    DataTable dtContactoProv = new DataTable();

                    context.Parametros.Clear();
                    context.Parametros.Add(new SqlParameter("@companynumber", Convert.ToInt32(Empresa)));

                    dtContactoProv = context.ExecuteProcedure("[spObtieneContactoProvedor]", true).Copy();

                    if (dtContactoProv.Rows.Count > 0)
                    {
                        Numero_Contacto = Convert.ToInt32(dtContactoProv.Rows[0][0]);
                    }
                }
                catch (Exception sqlEx)
                {
                    //reportar la excepcion al archivo log correspondiente
                    return false;
                }

            }

            return true;
        }
        public bool EliminaContactos(string connectionString)
        {
            SqlConnection Conn;

            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.SP_EliminaContactos1", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Cliente", Numero_Cliente);
                command.Parameters.AddWithValue("@Numero_Contacto_Persona", Numero_Contacto_Persona);
                command.Parameters.AddWithValue("@Empresa", Empresa);

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
        //public DataTable get_contact_data(string connectionString, int NumeroCliente) //hacer sp fusionar este con el de select * from contacto


        //    string Query_contact = "SELECT Clientes.Numero_Contacto_Persona as Numero, Persona.Nombre_Completo as Nombre," +
        //                           " Persona.Telefono, Persona.Correo_Electronico, Persona.Celular, Clientes.Puesto, Clientes.Envio_Mail, Persona.Nombre, Persona.Paterno, Persona.Materno" +
        //                           " FROM Persona INNER JOIN (select Numero_Contacto_Persona, Puesto, Envio_Mail, Empresa from Contacto where" +
        //                           " Numero_Cliente = @clientnumber and Empresa = @companynumber) as Clientes on" +
        //                           " Clientes.Numero_Contacto_Persona = Persona.Numero AND Clientes.Empresa = Persona.Numero_Empresa order by Persona.Numero";



        public DataTable get_contact_data(int icompany, int iclient_number)//contactos del cliente / 
        {
            DataTable TableContact = new DataTable();
            ContactBLL ContactData = new ContactBLL();

            try
            {

                TableContact = ContactData.GetContactClient(icompany, iclient_number);

            }
            catch
            {
                return null;
            }
            finally
            {

            }

            return TableContact;
        }

        public DataTable get_contactprovider_data(int iclient_number, int icompany)//contactos del provedor / 
        {
            DataTable TableContact = new DataTable();
            ContactBLL ContactData = new ContactBLL();

            try
            {

                TableContact = ContactData.GetContactProvider(icompany, iclient_number);

            }
            catch
            {
                return null;
            }
            finally
            {

            }

            return TableContact;
        }



    }
}