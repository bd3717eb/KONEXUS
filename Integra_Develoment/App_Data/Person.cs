using IntegraBussines;
using IntegraData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Integra_Develoment
{
    public class Person
    {

        private int itipocliente;
        private int itipoproveedor;
        private int inumero;
        private int iempresa;
        static private string snombre_completo;
        private string spaterno;
        private string smaterno;
        private string snombre;
        private string snombre_corto;
        private string stelefono;
        private string scelular;
        private string sfax;
        private string scorreo_electronico;
        private string spersonalidad_juridica;
        private string srfc;
        private string scurp;
        private int igeneral;
        public Addres domicilio;
        public Client cliente;
        public Provider provider;

        public Person(int NumeroEmpresa, int sucursal)
        {
            iTipoCliente = 0;
            iTipoProveedor = 0;
            iNumero = 0;
            iEmpresa = NumeroEmpresa;
            iGeneral = 0;
            cliente = new Client(iEmpresa, sucursal);
            domicilio = new Addres(iNumero);
            provider = new Provider(iEmpresa);
        }

        public Person(int NumeroPersona, int NumeroEmpresa, string connectionString)
        {
            DataTable DataClient = new DataTable();
            DataTable DataProvider = new DataTable();
            iTipoCliente = 0;
            iTipoProveedor = 0;
            iNumero = NumeroPersona;
            iEmpresa = NumeroEmpresa;
            iGeneral = 0;

            DataClient = check_for_client(iEmpresa, iNumero);
            DataProvider = check_for_provider(iEmpresa, iNumero);

            if (DataClient != null)
            {
                if (DataClient.Rows.Count > 0)
                    iTipoCliente = 1;
            }

            if (DataProvider != null)
            {
                if (DataProvider.Rows.Count > 0)
                    iTipoProveedor = 1;
            }

            cliente = new Client(iNumero, iEmpresa, connectionString);
            provider = new Provider(iNumero, iEmpresa, connectionString);
            domicilio = new Addres(iNumero);
        }


        public bool sp_Persona(string connectionString, int tipo, string Nombre_Completo, string Paterno, string Materno, string Nombre,
                                string Nombre_Corto, string Telefono, string Correo_Electronico, string PersonalidadJuridica, string RFC, string CURP, string Fecha)//*
        {
            SqlConnection Conn;
            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {

                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_Persona", Conn);

                command.CommandType = CommandType.StoredProcedure;

                Nombre_Completo = Nombre + " " + Paterno + " " + Materno;

                command.Parameters.AddWithValue("@TipoMovimiento ", tipo);
                command.Parameters.AddWithValue("@Numero_Empresa", iEmpresa);
                command.Parameters.AddWithValue("@Numero", 0);
                command.Parameters.AddWithValue("@Nombre_Completo", Nombre_Completo);
                command.Parameters.AddWithValue("@Paterno", Paterno);
                command.Parameters.AddWithValue("@Materno", Materno);
                command.Parameters.AddWithValue("@Nombre", Nombre);
                command.Parameters.AddWithValue("@Nombre_Corto", Nombre_Corto);
                command.Parameters.AddWithValue("@RFC", RFC);
                command.Parameters.AddWithValue("@CURP", CURP);
                command.Parameters.AddWithValue("@Telefono", Telefono);
                command.Parameters.AddWithValue("@Fax", Telefono);
                command.Parameters.AddWithValue("@Radiolocalizador", Telefono);
                command.Parameters.AddWithValue("@Correo_Electronico", Correo_Electronico);
                command.Parameters.AddWithValue("@Personalidad_Juridica", PersonalidadJuridica);
                command.Parameters.AddWithValue("@General", iGeneral);
                command.Parameters.AddWithValue("@Numero_Confia", DBNull.Value);
                command.Parameters.AddWithValue("@Transportista", DBNull.Value);

                command.Parameters.AddWithValue("@Fecha_Alta", Fecha);

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
                //string Query_clientnumber = "select MAX(Numero) as Numero  from  persona where Numero_empresa = @companynumber";

                //SqlCommand command_clientnumber;
                //SqlDataReader data_clientnumber = null;

                //try
                //{
                //    INTEGRA.SQL.open_connection(Conn);

                //    command_clientnumber = INTEGRA.SQL.get_sqlCommand(Query_clientnumber, Conn);
                //    INTEGRA.SQL.insert_parameters(command_clientnumber, "@companynumber", iEmpresa.ToString());

                //    data_clientnumber = INTEGRA.SQL.get_dataReader(command_clientnumber);

                //    if (data_clientnumber != null)
                //    {
                //        data_clientnumber.Read();
                //        inumero = Convert.ToInt32(data_clientnumber["Numero"]);
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
                    DataTable dtCliente = new DataTable();

                    context.Parametros.Clear();
                    context.Parametros.Add(new SqlParameter("@companynumber", Convert.ToInt32(iEmpresa)));

                    dtCliente = context.ExecuteProcedure("spOptieneUltimoNumeroPersona", true).Copy();

                    if (dtCliente.Rows.Count > 0)
                    {
                        inumero = Convert.ToInt32(dtCliente.Rows[0][0]);
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

        public bool sp_Personap(string connectionString, int tipo, string Nombre_Completo, string Paterno, string Materno, string Nombre, string Nombre_Corto, string Telefono, string Correo_Electronico, string PersonalidadJuridica, string RFC, string CURP, string Fecha)//*
        {
            SqlConnection Conn;
            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {

                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_Persona", Conn);

                command.CommandType = CommandType.StoredProcedure;

                //Nombre_Completo = Nombre + " " + Paterno + " " + Materno;

                command.Parameters.AddWithValue("@TipoMovimiento ", tipo);
                command.Parameters.AddWithValue("@Numero_Empresa", iEmpresa);
                command.Parameters.AddWithValue("@Numero", 0);
                command.Parameters.AddWithValue("@Nombre_Completo", Nombre_Completo);
                command.Parameters.AddWithValue("@Paterno", Paterno);
                command.Parameters.AddWithValue("@Materno", Materno);
                command.Parameters.AddWithValue("@Nombre", Nombre);
                command.Parameters.AddWithValue("@Nombre_Corto", Nombre_Corto);
                command.Parameters.AddWithValue("@RFC", RFC);
                command.Parameters.AddWithValue("@CURP", CURP);
                command.Parameters.AddWithValue("@Telefono", Telefono);
                command.Parameters.AddWithValue("@Fax", Telefono);
                command.Parameters.AddWithValue("@Radiolocalizador", Telefono);
                command.Parameters.AddWithValue("@Correo_Electronico", Correo_Electronico);
                command.Parameters.AddWithValue("@Personalidad_Juridica", PersonalidadJuridica);
                command.Parameters.AddWithValue("@General", iGeneral);
                command.Parameters.AddWithValue("@Numero_Confia", DBNull.Value);
                command.Parameters.AddWithValue("@Transportista", DBNull.Value);

                command.Parameters.AddWithValue("@Fecha_Alta", Fecha);

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


                try
                {
                    SQLConection context = new SQLConection();
                    DataTable dtCliente = new DataTable();

                    context.Parametros.Clear();
                    context.Parametros.Add(new SqlParameter("@companynumber", Convert.ToInt32(iEmpresa)));

                    dtCliente = context.ExecuteProcedure("spOptieneUltimoNumeroPersona", true).Copy();

                    if (dtCliente.Rows.Count > 0)
                    {
                        inumero = Convert.ToInt32(dtCliente.Rows[0][0]);
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


        public bool sp_PersonaModifica(string connectionString, int tipo, string Nombre_Completo, string Paterno, string Materno, string Nombre, string Nombre_Corto, string Telefono, string Correo_Electronico, string PersonalidadJuridica, string RFC, string CURP, string Fecha)
        {
            SqlConnection Conn;
            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {

                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_Persona", Conn);

                command.CommandType = CommandType.StoredProcedure;

                Nombre_Completo = Nombre + " " + Paterno + " " + Materno;

                command.Parameters.AddWithValue("@TipoMovimiento ", tipo);
                command.Parameters.AddWithValue("@Numero_Empresa", iEmpresa);
                command.Parameters.AddWithValue("@Numero", iNumero);
                command.Parameters.AddWithValue("@Nombre_Completo", Nombre_Completo);
                command.Parameters.AddWithValue("@Paterno", Paterno);
                command.Parameters.AddWithValue("@Materno", Materno);
                command.Parameters.AddWithValue("@Nombre", Nombre);
                command.Parameters.AddWithValue("@Nombre_Corto", Nombre_Corto);
                command.Parameters.AddWithValue("@RFC", RFC);
                command.Parameters.AddWithValue("@CURP", CURP);
                command.Parameters.AddWithValue("@Telefono", Telefono);
                command.Parameters.AddWithValue("@Fax", Telefono);
                command.Parameters.AddWithValue("@Radiolocalizador", Telefono);
                command.Parameters.AddWithValue("@Correo_Electronico", Correo_Electronico);
                command.Parameters.AddWithValue("@Personalidad_Juridica", PersonalidadJuridica);
                command.Parameters.AddWithValue("@General", iGeneral);
                command.Parameters.AddWithValue("@Numero_Confia", DBNull.Value);
                command.Parameters.AddWithValue("@Transportista", DBNull.Value);

                command.Parameters.AddWithValue("@Fecha_Alta", Fecha);



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

        public bool sp_PersonaModificaContacto(string connectionString, int tipo, string fecha)
        {
            SqlConnection Conn;
            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {

                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_Persona", Conn);

                command.CommandType = CommandType.StoredProcedure;

                sNombre_Completo = sNombre + " " + sPaterno + " " + sMaterno;

                command.Parameters.AddWithValue("@TipoMovimiento ", tipo);
                command.Parameters.AddWithValue("@Numero_Empresa", iEmpresa);
                command.Parameters.AddWithValue("@Numero", iNumero);
                command.Parameters.AddWithValue("@Nombre_Completo", sNombre_Completo);
                command.Parameters.AddWithValue("@Paterno", sPaterno);
                command.Parameters.AddWithValue("@Materno", sMaterno);
                command.Parameters.AddWithValue("@Nombre", sNombre);
                command.Parameters.AddWithValue("@Nombre_Corto", sNombre_Corto);
                command.Parameters.AddWithValue("@RFC", sRFC);
                command.Parameters.AddWithValue("@CURP", sCURP);
                command.Parameters.AddWithValue("@Telefono", sTelefono);
                command.Parameters.AddWithValue("@Fax", sTelefono);
                command.Parameters.AddWithValue("@Radiolocalizador", sTelefono);
                command.Parameters.AddWithValue("@Celular", sCelular);
                command.Parameters.AddWithValue("@Correo_Electronico", sCorreo_Electronico);
                command.Parameters.AddWithValue("@Personalidad_Juridica", spersonalidad_juridica);
                command.Parameters.AddWithValue("@General", iGeneral);
                command.Parameters.AddWithValue("@Numero_Confia", DBNull.Value);
                command.Parameters.AddWithValue("@Transportista", DBNull.Value);

                command.Parameters.AddWithValue("@Fecha_Alta", fecha);



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

        public bool sp_Personacontac(string connectionString, int tipo, string Nombre_Completo, string Paterno, string Materno, string Nombre, string Telefono, string Celular, string Correo_Electronico, int TipoGL, string personalidad_juridica)
        {
            SqlConnection Conn;
            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {

                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_Persona", Conn);

                command.CommandType = CommandType.StoredProcedure;

                Nombre_Completo = Nombre + " " + Paterno + " " + Materno;

                command.Parameters.AddWithValue("@TipoMovimiento ", tipo);
                command.Parameters.AddWithValue("@Numero_Empresa", iEmpresa);
                command.Parameters.AddWithValue("@Numero", 0);
                command.Parameters.AddWithValue("@Nombre_Completo", Nombre_Completo);
                command.Parameters.AddWithValue("@Paterno", Paterno);
                command.Parameters.AddWithValue("@Materno", Materno);
                command.Parameters.AddWithValue("@Nombre", Nombre);
                command.Parameters.AddWithValue("@Nombre_Corto", DBNull.Value);
                command.Parameters.AddWithValue("@RFC", DBNull.Value);
                command.Parameters.AddWithValue("@CURP", DBNull.Value);
                command.Parameters.AddWithValue("@Telefono", Telefono);
                command.Parameters.AddWithValue("@Fax", Telefono);
                command.Parameters.AddWithValue("@Radiolocalizador", Celular);
                command.Parameters.AddWithValue("@Celular", Celular);
                command.Parameters.AddWithValue("@Correo_Electronico", Correo_Electronico);
                command.Parameters.AddWithValue("@Personalidad_Juridica", personalidad_juridica);
                command.Parameters.AddWithValue("@General", iGeneral);
                command.Parameters.AddWithValue("@Numero_Confia", DBNull.Value);
                command.Parameters.AddWithValue("@Transportista", DBNull.Value);

                command.Parameters.AddWithValue("@Fecha_Alta", DBNull.Value);



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
                //string Query_clientnumber = "select MAX(Numero) as Numero  from  persona where Numero_empresa = @companynumber";

                //SqlCommand command_clientnumber;
                //SqlDataReader data_clientnumber = null;

                //try
                //{
                //    INTEGRA.SQL.open_connection(Conn);

                //    command_clientnumber = INTEGRA.SQL.get_sqlCommand(Query_clientnumber, Conn);
                //    INTEGRA.SQL.insert_parameters(command_clientnumber, "@companynumber", iEmpresa.ToString());

                //    data_clientnumber = INTEGRA.SQL.get_dataReader(command_clientnumber);

                //    if (data_clientnumber != null)
                //    {
                //        data_clientnumber.Read();
                //        inumero = Convert.ToInt32(data_clientnumber["Numero"]);
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
                    DataTable dtCliente = new DataTable();

                    context.Parametros.Clear();
                    context.Parametros.Add(new SqlParameter("@companynumber", Convert.ToInt32(iEmpresa)));

                    dtCliente = context.ExecuteProcedure("spOptieneUltimoNumeroPersona", true).Copy();

                    if (dtCliente.Rows.Count > 0)
                    {
                        inumero = Convert.ToInt32(dtCliente.Rows[0][0]);
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



        public bool get_person_data_from_personnumber(int icompany, int iperson)
        {

            DataTable DataPerson = new DataTable();

            try
            {
                DataPerson = PersonBLL.ObtenDatosPersonaCliente(icompany, iperson);

                if (DataPerson != null && DataPerson.Rows.Count > 0)
                {

                    sNombre_Completo = (DataPerson.Rows[0][2] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][2]);
                    sPaterno = (DataPerson.Rows[0][3] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][3]);
                    sMaterno = (DataPerson.Rows[0][4] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][4]);
                    sNombre = (DataPerson.Rows[0][5] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][5]);
                    sNombre_Corto = (DataPerson.Rows[0][6] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][6]);
                    sRFC = (DataPerson.Rows[0][7] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][7]);
                    sCURP = (DataPerson.Rows[0][8] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][8]);
                    sTelefono = (DataPerson.Rows[0][9] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][9]);
                    sFax = sTelefono;
                    sCorreo_Electronico = (DataPerson.Rows[0][13] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][13]);
                    sPersonalidad_Juridica = (DataPerson.Rows[0][14] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][14]);
                }
                else
                {


                    DataPerson = PersonBLL.ObtenDatosTablaPersona(icompany, iperson);

                    sNombre_Completo = (DataPerson.Rows[0][2] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][2]);
                    sPaterno = (DataPerson.Rows[0][3] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][3]);
                    sMaterno = (DataPerson.Rows[0][4] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][4]);
                    sNombre = (DataPerson.Rows[0][5] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][5]);
                    sNombre_Corto = (DataPerson.Rows[0][6] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][6]);
                    sRFC = (DataPerson.Rows[0][7] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][7]);
                    sCURP = (DataPerson.Rows[0][8] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][8]);
                    sTelefono = (DataPerson.Rows[0][9] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][9]);
                    sFax = sTelefono;
                    sCorreo_Electronico = (DataPerson.Rows[0][13] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][13]);
                    sPersonalidad_Juridica = (DataPerson.Rows[0][14] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][14]);
                }
            }
            catch (Exception sqlEx)
            {
                //logear la excepcion

                return false;
            }
            finally
            {

            }

            return true;
        }


        public DataTable check_for_client(int icompany, int iperson)
        {
            DataTable TableClient = new DataTable();
            CustomerBLL ClientData = new CustomerBLL();

            try
            {
                TableClient = ClientData.GetCustomerData(icompany, iperson);// GetPersonData
            }
            catch
            {
                //logeo excepcion

                return null;
            }
            finally
            {

            }
            if (TableClient != null)
            {
                if (TableClient.Rows.Count > 0)
                    return TableClient;
            }

            return null;
        }


        private DataTable check_for_provider(int icompany, int iperson)
        {
            DataTable TableProvider = new DataTable();
            ProviderBLL ProviderData = new ProviderBLL();

            try
            {
                TableProvider = ProviderBLL.ObtenDatosProveedor(icompany, iperson);
            }
            catch
            {
                //logeo excepcion

                return null;
            }
            finally
            {

            }

            if (TableProvider != null)
                return TableProvider;

            return null;
        }


        private DataTable getPhisicPersonFromPersonNumber(int icompany, int iperson, string stypeperson)
        {
            DataTable TablePerson = new DataTable();

            try
            {
                TablePerson = PersonBLL.ObtenDatosPersona(icompany, iperson, stypeperson);
            }
            catch
            {
                //logeo excepcion

                return null;
            }
            finally
            {

            }

            if (TablePerson.Rows.Count > 0)
                return TablePerson;

            return null;
        }
        public int sp_AcreedorAltaRapida(string connectionString, int Numero_Persona, int iEmpresa, string Nombre_Completo, int intTipo_NE, int intTipo)
        {
            SqlConnection Conn;
            int result = 0, inumero = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {

                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_AcreedorAltaRapida", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Numero_Empresa", iEmpresa);
                command.Parameters.AddWithValue("@Numero_Persona", Numero_Persona);
                command.Parameters.AddWithValue("@Nombre_Completo", Nombre_Completo);
                command.Parameters.AddWithValue("@Tipo_NE", intTipo_NE);
                command.Parameters.AddWithValue("@Tipo", intTipo);


                result = command.ExecuteNonQuery();
            }
            catch (Exception sqlEx)
            {
                //logear la excepcion

                return 0;
            }
            finally
            {
                if (Conn != null)
                    Conn.Close();
            }

            if (result > 0)
            {
                string Query_clientnumber = "select MAX(Numero_Persona) as Numero  from  Proveedor where Numero_empresa = @companynumber";

                SqlCommand command_clientnumber;
                SqlDataReader data_clientnumber = null;

                try
                {
                    INTEGRA.SQL.open_connection(Conn);

                    command_clientnumber = INTEGRA.SQL.get_sqlCommand(Query_clientnumber, Conn);
                    INTEGRA.SQL.insert_parameters(command_clientnumber, "@companynumber", iEmpresa.ToString());

                    data_clientnumber = INTEGRA.SQL.get_dataReader(command_clientnumber);

                    if (data_clientnumber != null)
                    {
                        data_clientnumber.Read();
                        inumero = Convert.ToInt32(data_clientnumber["Numero"]);
                    }

                }
                catch (Exception sqlEx)
                {
                    //reportar la excepcion al archivo log correspondiente 
                    return 0;
                }
                finally
                {
                    if (Conn != null)
                        Conn.Close();
                }
            }
            else
                return 0;

            return inumero;
        }

        public int iTipoCliente { get { return itipocliente; } set { itipocliente = value; } }
        public int iTipoProveedor { get { return itipoproveedor; } set { itipoproveedor = value; } }
        public int iNumero { get { return inumero; } set { inumero = value; } }
        public int iEmpresa { get { return iempresa; } set { iempresa = value; } }
        public string sNombre_Completo { get { return snombre_completo; } set { snombre_completo = value; } }
        public string sPaterno { get { return spaterno; } set { spaterno = value; } }
        public string sMaterno { get { return smaterno; } set { smaterno = value; } }
        public string sNombre { get { return snombre; } set { snombre = value; } }
        public string sNombre_Corto { get { return snombre_corto; } set { snombre_corto = value; } }
        public string sTelefono { get { return stelefono; } set { stelefono = value; } }
        public string sCelular { get { return scelular; } set { scelular = value; } }
        public string sFax { get { return sfax; } set { sfax = value; } }
        public string sCorreo_Electronico { get { return scorreo_electronico; } set { scorreo_electronico = value; } }
        public string sPersonalidad_Juridica { get { return spersonalidad_juridica; } set { spersonalidad_juridica = value; } }
        public string sRFC { get { return srfc; } set { srfc = value; } }
        public string sCURP { get { return scurp; } set { scurp = value; } }
        public int iGeneral { get { return igeneral; } set { igeneral = value; } }

    }
}