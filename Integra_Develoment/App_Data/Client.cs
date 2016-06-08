using System;
using System.Data;
using System.Data.SqlClient;
using IntegraBussines;
using IntegraData;

namespace Integra_Develoment
{
    public class Client
    {
        #region Miembros

        private int icontacto_cliente;
        private int icredito_cliente;
        private int inumero_persona;
        private int iempresa;
        private int Empresa;
        private int Numero_Persona;
        private int iclasificacion_cliente;
        private DateTime fecha_alta;
        private int iestatus;
        private string sgiro;
        private int iperiodo_pago;
        private int iclas_periodo_pago;
        private int? iclas_dato_pago;
        private int? idato_pago;
        private int igeneral;
        private int iclas_sucursal;
        private int? iforma_pago;
        private int iclas_zona;
        private int? ilista_precio;
        private int ifamily;

        public Contact contacto;
        public Credit condiciones_credito;

        #endregion

        #region Constructores

        public Client(int iNumeroCliente, int iNumeroEmpresa, string sconnectionString)
        {
            int NumeroContacto;

            iContacto_Cliente = 0;
            icredito_cliente = 0;
            iNumero_Persona = iNumeroCliente;
            iEmpresa = iNumeroEmpresa;
            iPeriodo_Pago = 1;
            iGeneral = 0;
            iForma_Pago = 0;
            iClas_Dato_Pago = 0;
            iDato_Pago = 0;
            ifamily = 9;
            iEstatus = 6;
            iClas_Zona = 168;
            iClas_Periodo_Pago = 5;
            DataTable TableNumberContac = new DataTable();
            TableNumberContac = check_client_contact(iNumeroEmpresa, iNumero_Persona);



            if (TableNumberContac != null)
            {
                if (TableNumberContac.Rows.Count > 0)
                {
                    if ((NumeroContacto = Convert.ToInt32(TableNumberContac.Rows[0][0])) > 0)
                    {
                        iContacto_Cliente = 1;
                        contacto = new Contact(iNumero_Persona, NumeroContacto, iNumeroEmpresa);
                        contacto.domicilio_contacto = new Addres(contacto.Numero_Contacto_Persona);
                    }
                }
                else
                {
                    contacto = new Contact();
                    contacto.domicilio_contacto = new Addres();
                }
            }

            DataTable TableCredit = new DataTable();

            TableCredit = GetCreditCondition(iNumero_Persona, iNumeroEmpresa, ifamily);



            if (TableCredit != null)
            {

                if (TableCredit.Rows.Count > 0)
                {
                    icredito_cliente = 1;
                    condiciones_credito = new Credit(iNumero_Persona, iNumeroEmpresa);
                }
                else
                    condiciones_credito = new Credit();
            }
        }

        public Client(int NumeroEmpresa, int sucursal)
        {
            iContacto_Cliente = 0;
            icredito_cliente = 0;
            iNumero_Persona = 0;
            iEmpresa = NumeroEmpresa;
            iPeriodo_Pago = 1;
            iGeneral = 0;
            iForma_Pago = 0;
            iClas_Dato_Pago = 0;
            iDato_Pago = 0;
            iEstatus = 6;
            iClas_Zona = 168;
            iClas_Periodo_Pago = 5;
            iClas_Sucursal = sucursal;
            contacto = new Contact();
            condiciones_credito = new Credit();
            ifamily = 9;
        }

        #endregion

        #region Asignaciones

        public int iContacto_Cliente { get { return icontacto_cliente; } set { icontacto_cliente = value; } }
        public int iCredito_Cliente { get { return icredito_cliente; } set { icredito_cliente = value; } }
        public int iEmpresa { get { return iempresa; } set { iempresa = value; } }
        public int iClasificacion_Cliente { get { return iclasificacion_cliente; } set { iclasificacion_cliente = value; } }
        public DateTime Fecha_Alta { get { return fecha_alta; } set { fecha_alta = value; } }
        public int iEstatus { get { return iestatus; } set { iestatus = value; } }
        public string sGiro { get { return sgiro; } set { sgiro = value; } }
        public int iPeriodo_Pago { get { return iperiodo_pago; } set { iperiodo_pago = value; } }
        public int iClas_Periodo_Pago { get { return iclas_periodo_pago; } set { iclas_periodo_pago = value; } }
        public int iGeneral { get { return igeneral; } set { igeneral = value; } }
        public int iNumero_Persona { get { return inumero_persona; } set { inumero_persona = value; } }
        public int iClas_Sucursal { get { return iclas_sucursal; } set { iclas_sucursal = value; } }
        public int? iForma_Pago { get { return iforma_pago; } set { iforma_pago = value; } }
        public int iClas_Zona { get { return iclas_zona; } set { iclas_zona = value; } }
        public int? iLista_Precio { get { return ilista_precio; } set { ilista_precio = value; } }
        public int? iClas_Dato_Pago { get { return iclas_dato_pago; } set { iclas_dato_pago = value; } }
        public int? iDato_Pago { get { return idato_pago; } set { idato_pago = value; } }
        public int iFamily { get { return ifamily; } set { ifamily = value; } }

        #endregion

        #region Procedimientos_Almacenados

        public bool ClienteAlta(string connectionString, string Nombre_Completo, string Paterno, string Materno, string Nombre, string Nombre_Corto, string Telefono, string Correo_Electronico, string PersonalidadJuridica, string RFC, string CURP, int Clasificacion_Cliente, int? Lista_Precio)
        {
            SqlConnection Conn;
            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_ClienteAlta", Conn);

                command.CommandType = CommandType.StoredProcedure;

                Nombre_Completo = Nombre + " " + Paterno + " " + Materno;

                command.Parameters.AddWithValue("@Nombre_Completo", Nombre_Completo);
                command.Parameters.AddWithValue("@Paterno", Paterno);
                command.Parameters.AddWithValue("@Materno", Materno);
                command.Parameters.AddWithValue("@Nombre", Nombre);
                command.Parameters.AddWithValue("@NombreCorto", Nombre_Corto);
                command.Parameters.AddWithValue("@TelefonoCliente", Telefono);
                command.Parameters.AddWithValue("@Fax", Telefono);
                command.Parameters.AddWithValue("@Email", Correo_Electronico);
                command.Parameters.AddWithValue("@Personalidad_Juridica", PersonalidadJuridica);
                command.Parameters.AddWithValue("@Empresa", iEmpresa);
                command.Parameters.AddWithValue("@Clasificacion_Cliente", Clasificacion_Cliente);
                command.Parameters.AddWithValue("@Fecha", Fecha_Alta);
                command.Parameters.AddWithValue("@Estatus", iEstatus);
                command.Parameters.AddWithValue("@RFC", RFC);
                command.Parameters.AddWithValue("@sGiro", sGiro);
                command.Parameters.AddWithValue("@sCurp", CURP);
                command.Parameters.AddWithValue("@Periodo_Pago", iPeriodo_Pago);
                command.Parameters.AddWithValue("@Clas_Periodo_Pago", iClas_Periodo_Pago);
                command.Parameters.AddWithValue("@General", iGeneral);
                command.Parameters.AddWithValue("@Numero_Persona", iNumero_Persona);
                command.Parameters.AddWithValue("@Clas_Sucursal", INTEGRA.Global.Office);
                command.Parameters.AddWithValue("@Forma_Pago", iForma_Pago);
                command.Parameters.AddWithValue("@Clas_Zona", iClas_Zona);
                command.Parameters.AddWithValue("@Lista_Precio", Lista_Precio);
                command.Parameters.AddWithValue("@Clas_Dato_Pago", iClas_Dato_Pago);
                command.Parameters.AddWithValue("@Dato_Pago", iDato_Pago.HasValue ? (object)iDato_Pago.Value : DBNull.Value);

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
                //string Query_clientnumber = "select MAX(Numero_Persona) as Numero from  cliente where Numero_empresa = @companynumber";

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
                //        iNumero_Persona = Convert.ToInt32(data_clientnumber["Numero"]);                                            
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
                    DataTable dtPersona = new DataTable();

                    context.Parametros.Clear();
                    context.Parametros.Add(new SqlParameter("@companynumber", Convert.ToInt32(iEmpresa)));

                    dtPersona = context.ExecuteProcedure("sp_ObtieneNumeroPersona", true).Copy();

                    if (dtPersona.Rows.Count > 0)
                    {
                        iNumero_Persona = Convert.ToInt32(dtPersona.Rows[0][0]);
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

        public bool InsertaPersonaCliente(string connectionString, string Nombre_Completo, string Paterno, string Materno, string Nombre, string Nombre_Corto, string Telefono, string Correo_Electronico, string PersonalidadJuridica, string RFC, string CURP, int Clasificacion_Cliente, int? Lista_Precio)
        {
            SqlConnection Conn;
            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_InsertaPersonaCliente", Conn);

                command.CommandType = CommandType.StoredProcedure;

                //Cliente

                command.Parameters.AddWithValue("@Empresa", iEmpresa);
                command.Parameters.AddWithValue("@ID_Persona", iNumero_Persona);
                command.Parameters.AddWithValue("@Clasificacion_Cliente", Clasificacion_Cliente);
                command.Parameters.AddWithValue("@Fecha", Fecha_Alta);
                command.Parameters.AddWithValue("@Estatus", iEstatus);

                //Persona
                command.Parameters.AddWithValue("@Nombre_Completo", Nombre_Completo);
                command.Parameters.AddWithValue("@Paterno", Paterno);
                command.Parameters.AddWithValue("@Materno", Materno);
                command.Parameters.AddWithValue("@Nombre", Nombre);
                command.Parameters.AddWithValue("@NombreCorto", Nombre_Corto);
                command.Parameters.AddWithValue("@TelefonoCliente", Telefono);
                command.Parameters.AddWithValue("@Fax", Telefono);
                command.Parameters.AddWithValue("@Email", Correo_Electronico);
                command.Parameters.AddWithValue("@RFC", RFC);
                command.Parameters.AddWithValue("@Curp", CURP);
                command.Parameters.AddWithValue("@Giro", sGiro);

                command.Parameters.AddWithValue("@Periodo_Pago", iPeriodo_Pago);
                command.Parameters.AddWithValue("@Clas_Periodo_Pago", iClas_Periodo_Pago);
                command.Parameters.AddWithValue("@General", iGeneral);
                command.Parameters.AddWithValue("@Clas_Sucursal", INTEGRA.Global.Office);
                command.Parameters.AddWithValue("@Forma_Pago", iForma_Pago);
                command.Parameters.AddWithValue("@Clas_Zona", iClas_Zona);
                command.Parameters.AddWithValue("@Lista_Precio", Lista_Precio);
                command.Parameters.AddWithValue("@Clas_Dato_Pago", iClas_Dato_Pago);
                command.Parameters.AddWithValue("@Dato_Pago", iDato_Pago.HasValue ? (object)iDato_Pago.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Personalidad_Juridica", PersonalidadJuridica);

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
                //string Query_clientnumber = "select MAX(Numero_Persona) as Numero from  cliente where Numero_empresa = @companynumber";

                //SqlCommand command_clientnumber;
                //SqlDataReader data_clientnumber = null;

                //try
                //{
                //    INTEGRA.SQL.open_connection(Conn);

                //    command_clientnumber = INTEGRA.SQL.get_sqlCommand(Query_clientnumber, Conn);
                //    INTEGRA.SQL.insert_parameters(command_clientnumber, "@companynumber", Empresa.ToString());

                //    data_clientnumber = INTEGRA.SQL.get_dataReader(command_clientnumber);

                //    if (data_clientnumber != null)
                //    {
                //        data_clientnumber.Read();
                //        Numero_Persona = Convert.ToInt32(data_clientnumber["Numero"]);
                //    }

                //}
                //catch (Exception sqlEx)
                //{
                //    reportar la excepcion al archivo log correspondiente 
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
                    DataTable dtPersona = new DataTable();

                    context.Parametros.Clear();
                    context.Parametros.Add(new SqlParameter("@companynumber", Convert.ToInt32(iEmpresa)));

                    dtPersona = context.ExecuteProcedure("sp_ObtieneNumeroPersona", true).Copy();

                    if (dtPersona.Rows.Count > 0)
                    {
                        Numero_Persona = Convert.ToInt32(dtPersona.Rows[0][0]);
                    }

                }
                catch (Exception sqlEx)
                {
                    return false;
                }

            }
            else
                return false;

            return true;
        }

        public bool ModificaPersonaCliente(string connectionString, string Nombre_Completo, string Paterno, string Materno, string Nombre, string Nombre_Corto, string Telefono, string Correo_Electronico, string PersonalidadJuridica, string RFC, string CURP, int Clasificacion_Cliente, int? Lista_Precio)
        {
            SqlConnection Conn;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_ModificaPersonaCliente", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Empresa", iEmpresa);
                command.Parameters.AddWithValue("@Numero_Persona", iNumero_Persona);

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

                command.Parameters.AddWithValue("@Clasificacion_Cliente", Clasificacion_Cliente);
                command.Parameters.AddWithValue("@RFC", RFC);
                command.Parameters.AddWithValue("@sGiro", sGiro);
                command.Parameters.AddWithValue("@sCurp", CURP);
                command.Parameters.AddWithValue("@Periodo_Pago", iPeriodo_Pago);
                command.Parameters.AddWithValue("@Clas_Periodo_Pago", iClas_Periodo_Pago);
                command.Parameters.AddWithValue("@General", iGeneral);
                command.Parameters.AddWithValue("@Estatus", iEstatus);
                command.Parameters.AddWithValue("@Forma_Pago", iForma_Pago);
                command.Parameters.AddWithValue("@Clas_Zona", iClas_Zona);
                command.Parameters.AddWithValue("@Lista_Precio", Lista_Precio);
                command.Parameters.AddWithValue("@Clas_Dato_Pago", iClas_Dato_Pago);
                command.Parameters.AddWithValue("@Dato_Pago", iDato_Pago);
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

        public bool delete_client(string connectionString)
        {
            SqlConnection Conn;
            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.SP_EliminaClientes", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Cliente", iNumero_Persona);
                command.Parameters.AddWithValue("@Empresa", iEmpresa);

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

        #endregion

        #region Metodos

        public bool get_client_data_from_clientnumber(int icompany, int iperson)
        {
            DataTable DataClient = new DataTable();
            CustomerBLL DataCustomer = new CustomerBLL();

            try
            {
                DataClient = DataCustomer.GetCustomerTable(icompany, iperson);

                iClasificacion_Cliente = (DataClient.Rows[0][2] == DBNull.Value) ? 0 : Convert.ToInt32(DataClient.Rows[0][2]);
                Fecha_Alta = (Convert.ToDateTime(DataClient.Rows[0][3]));
                iEstatus = (DataClient.Rows[0][4] == DBNull.Value) ? 0 : Convert.ToInt32(DataClient.Rows[0][4]);
                sGiro = (DataClient.Rows[0][5] == DBNull.Value) ? null : Convert.ToString(DataClient.Rows[0][5]);
                iPeriodo_Pago = (DataClient.Rows[0][8] == DBNull.Value) ? 0 : Convert.ToInt32(DataClient.Rows[0][8]);
                iClas_Periodo_Pago = (DataClient.Rows[0][9] == DBNull.Value) ? 0 : Convert.ToInt32(DataClient.Rows[0][9]);
                iClas_Sucursal = (DataClient.Rows[0][12] == DBNull.Value) ? 0 : Convert.ToInt32(DataClient.Rows[0][12]);
                iForma_Pago = (DataClient.Rows[0][15] == DBNull.Value) ? 0 : Convert.ToInt32(DataClient.Rows[0][15]);
                iClas_Zona = (DataClient.Rows[0][16] == DBNull.Value) ? 0 : Convert.ToInt32(DataClient.Rows[0][16]);
                iLista_Precio = (DataClient.Rows[0][17] == DBNull.Value) ? 0 : Convert.ToInt32(DataClient.Rows[0][17]);
                iClas_Dato_Pago = (DataClient.Rows[0][19] == DBNull.Value) ? 0 : Convert.ToInt32(DataClient.Rows[0][19]);
                iDato_Pago = (DataClient.Rows[0][20] == DBNull.Value) ? 0 : Convert.ToInt32(DataClient.Rows[0][20]);
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


        public DataTable check_client_contact(int icompany, int iclient_number)//contactos del cliente / 
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

        public DataTable check_client_quote(int iclient_number, int icompany) //cotizaciones del cliente /
        {
            DataTable TableQuote = new DataTable();
            CustomerBLL ClientData = new CustomerBLL();

            try
            {
                TableQuote = ClientData.GetQuoteClient(icompany, iclient_number);

            }
            catch
            {
                return null;
            }
            finally
            {

            }

            return TableQuote;
        }

        public DataTable GetCreditCondition(int iclient_number, int icompanynumber, int ifamily)// creditos del cliente
        {

            DataTable TableCredit = new DataTable();
            CustomerBLL CreditData = new CustomerBLL();

            try
            {
                TableCredit = CreditData.GetCreditClient(icompanynumber, iclient_number, ifamily);
            }
            catch (Exception sqlEx)
            {
                //logeo excepcion

                return null;
            }
            finally
            {

            }

            return TableCredit;
        }


        public DataTable GetCurrencyforCreditcondition(int icompany, int iclient_number) //  TRAE MONEDA DE TABLA CLIENTE_CREDITO
        {

            DataTable TableCurrency = new DataTable();
            CustomerBLL CurrencyData = new CustomerBLL();

            try
            {
                TableCurrency = CurrencyData.GetCurrencyCreditClient(icompany, iclient_number);
            }
            catch (Exception sqlEx)
            {
                //logeo excepcion

                return null;
            }
            finally
            {

            }

            return TableCurrency;
        }


        public bool ModificaDatosPagoCliente(string connectionString)
        {
            SqlConnection Conn;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_cliente_num_pago", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Numero_Empresa", iEmpresa);
                command.Parameters.AddWithValue("@Numero_Cliente", iNumero_Persona);
                command.Parameters.AddWithValue("@Clas_Dato_Pago", iClas_Dato_Pago);
                command.Parameters.AddWithValue("@Dato_Pago", iDato_Pago.HasValue ? (object)iDato_Pago.Value : DBNull.Value);


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
        #endregion

        #region Destructor

        ~Client()
        {
        }


        #endregion
    }
}