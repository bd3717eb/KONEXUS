using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using IntegraData;

namespace IntegraBussines
{
    public class CustomerBLL : PersonBLL
    {
        private int _customerclass;
        private int _status;
        private string _giro;
        private int _payperiod;
        private int _cpayperiod;
        private int _pay;
        private int _zone;
        private int _pricelist;
        private int _paydata;
        private int _cpaydata;

        public int CustomerClass { get { return _customerclass; } set { _customerclass = value; } }
        public DateTime RecordDate { get { return _recorddate; } set { _recorddate = value; } }
        public int Status { get { return _status; } set { _status = value; } }
        public string Giro { get { return _giro; } set { _giro = value; } }
        public int PayPeriod { get { return _payperiod; } set { _payperiod = value; } }
        public int CPayPeriod { get { return _cpayperiod; } set { _cpayperiod = value; } }
        public int Pay { get { return _pay; } set { _pay = value; } }
        public int Zone { get { return _zone; } set { _zone = value; } }
        public int PriceList { get { return _pricelist; } set { _pricelist = value; } }
        public int PayData { get { return _paydata; } set { _paydata = value; } }
        public int CPayData { get { return _cpaydata; } set { _cpaydata = value; } }

        /*Accesos Publicos para la entidad Persona*/

        public int Number { get { return _number; } set { _number = value; } }
        public int Company { get { return _company; } set { _company = value; } }
        public string FullName { get { return _fullname; } set { _fullname = value; } }
        public string Father { get { return _father; } set { _father = value; } }
        public string Mother { get { return _mother; } set { _mother = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string ShortName { get { return _shortname; } set { _shortname = value; } }
        public string RFC { get { return _rfc; } set { _rfc = value; } }
        public string CURP { get { return _curp; } set { _curp = value; } }
        public string PhoneNumber { get { return _phonenumber; } set { _phonenumber = value; } }
        public string Fax { get { return _fax; } set { _fax = value; } }
        public string CellPhone { get { return _cellphone; } set { _cellphone = value; } }
        public string eMail { get { return _email; } set { _email = value; } }
        public string Legal { get { return _legal; } set { _legal = value; } }
        public DateTime RecorDate { get { return _recorddate; } set { _recorddate = value; } }
        public int General { get { return _general; } set { _general = value; } }

        public string GetCustomerName(int company, int client)
        {
            DataTable CustomerTable;

            using (CustomerTable = GetCustomerData(company, client))
            {
                if (CustomerTable != null && CustomerTable.Rows.Count > 0)
                {
                    return CustomerTable.Rows[0][2].ToString();
                }
            }

            return null;
        }




        public DataTable GetPersonData(int company, int client)//trae datos de la tabla persona y cliente
        {
            SQLConection context = new SQLConection();
            DataTable CustomerTable = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("personnumber", client));
            context.Parametros.Add(new SqlParameter("companynumber", company));

            CustomerTable = context.ExecuteProcedure("sp_Persona_ObtieneDatosDePersona_v1", true).Copy();

            return CustomerTable;
        }

        public DataTable ObtenDatosPersonaProveedor(int iEmpresa, int iProveedor)//trae datos de la tabla persona mandar hacer
        {
            SQLConection context = new SQLConection();
            DataTable CustomerTable = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("personnumber", iProveedor));
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));

            CustomerTable = context.ExecuteProcedure("sp_Persona_ObtieneDatosDePersonaProveedor1", true).Copy();

            return CustomerTable;
        }


        //CREO QUE NO SE OCUPA 
        public DataTable GetCustomerData(int company, int client)// trae datos de la tabla cliente
        {
            SQLConection context = new SQLConection();
            DataTable CustomerTable;

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("personnumber", client));
            context.Parametros.Add(new SqlParameter("companynumber", company));

            CustomerTable = context.ExecuteProcedure("sp_Cliente_ObtieneDatosDeCliente_v1", true).Copy();

            if (CustomerTable != null && CustomerTable.Rows.Count > 0)
            {
                return CustomerTable;
            }

            return null;
        }



        public DataTable GetCustomerTable(int company, int client)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("personnumber", client));
            context.Parametros.Add(new SqlParameter("companynumber", company));

            dt = context.ExecuteProcedure("sp_Persona_ObtieneDatosDelCliente_v1", true).Copy();

            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }



        //Este procedimiento no se encuentra en INTEGRA_DEMO
        public DataTable GetCustomerDataFromCustomerName(int company, string customername)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("customername", customername));
            context.Parametros.Add(new SqlParameter("companynumber", company));

           dt = context.ExecuteProcedure("sp_Cliente_ObtieneDatosDelClientePorNombre_v1", true).Copy();

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }

            return null;
        }


        public DataTable GetContacts(int company, int customer)
        {
            ContactBLL contBll = new ContactBLL();
            DataTable dt = new DataTable();

            dt = contBll.GetContactClient(company, customer);

            return dt;
        }

        //public int Person(int _moviment)
        //{
        //    IntegraData.CustomerDAL Customer = new IntegraData.CustomerDAL();
        //    return Customer.ManagePerson(_moviment, _company, _number, _fullname, _father, _mother, _name, _shortname, _rfc, _curp, _phonenumber, _fax, _cellphone, _email, _legal, _general, _recorddate);
        //}

        public int Person(int _moviment)
        {
            SQLConection context = new SQLConection();
            int person;

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("TipoMovimiento", _moviment));
            context.Parametros.Add(new SqlParameter("Numero_Empresa", _company));
            context.Parametros.Add(new SqlParameter("Numero", _number));
            context.Parametros.Add(new SqlParameter("Nombre_Completo", _fullname));
            context.Parametros.Add(new SqlParameter("Paterno", _father));
            context.Parametros.Add(new SqlParameter("Materno", _mother));
            context.Parametros.Add(new SqlParameter("Nombre", _name));
            context.Parametros.Add(new SqlParameter("Nombre_Corto", _shortname));
            context.Parametros.Add(new SqlParameter("RFC", _rfc));
            context.Parametros.Add(new SqlParameter("CURP", _curp));
            context.Parametros.Add(new SqlParameter("Telefono", _phonenumber));
            context.Parametros.Add(new SqlParameter("Fax", _fax));
            context.Parametros.Add(new SqlParameter("Correo_Electronico", _email));
            context.Parametros.Add(new SqlParameter("Personalidad_Juridica", _legal));
            context.Parametros.Add(new SqlParameter("General", _general));
            context.Parametros.Add(new SqlParameter("Fecha_Alta", _recorddate));

            person = Convert.ToInt32(context.ExecuteProcedure("sp_Persona", true).Copy());

            return person;
        }

        //DESCOMENTAR EN CASO DE REGRESAR CABIOSDE VALIDACION DE RFC
        //public bool RFCValidation(string rfc, int type)
        //{
        //    if (type == 0)
        //    {
        //        if (Regex.IsMatch(rfc, @"^([A-Z\s]{3})\d{6}([A-Z\w]{3})$"))
        //            return true;
        //        else
        //            return false;
        //    }
        //    else
        //    {
        //        if (Regex.IsMatch(rfc, @"^([A-Z\s]{4})\d{6}([A-Z\w]{3})$"))
        //            return true;
        //        else
        //            return false;
        //    }
        //}

        public bool RFCValidation(string rfc, int type)
        {
            if (type == 0)
            {
                if (Regex.IsMatch(rfc, @"^([A-Z-&\s]{3})\d{6}([A-Z\w]{3})$"))
                    return true;
                else
                    return false;
            }
            else
            {
                if (Regex.IsMatch(rfc, @"^([A-Z-&\s]{4})\d{6}([A-Z\w]{3})$"))
                    return true;
                else
                    return false;
            }
        }

        //public bool emailValidation(string email)
        //{

        //    if (Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
        //        return true;
        //    return false;
        //}

        public Boolean emailValidation(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        //  function lfvalidar_email(valor)
        //{
        //      // creamos nuestra regla con expresiones regulares.
        //      var filter = /[\w-\.]{3,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
        //      // utilizamos test para comprobar si el parametro valor cumple la regla
        //      if(filter.test(valor))
        //          return true;
        //      else
        //          return false;
        //}


        public DataTable GetQuoteClient(int icompany, int iclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("clientnumber", iclient));
            context.Parametros.Add(new SqlParameter("companynumber", icompany));

            dt = context.ExecuteProcedure("sp_Cliente_ObtieneDatosDeCotizaciones_v1", true).Copy();

            return dt;
        }


       

        public DataTable GetCreditClient(int icompany, int icliente, int ifamily)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", icompany));
            context.Parametros.Add(new SqlParameter("clientnumber", icliente));
            context.Parametros.Add(new SqlParameter("family", ifamily));

            dt = context.ExecuteProcedure("sp_Cliente_ObtieneDatosDeCreditos_v1", true).Copy();

            return dt;
        }


        public DataTable GetMultipleClientesAutocomplete(int icompany, string sclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", icompany));
            context.Parametros.Add(new SqlParameter("@Nombre_Completo", sclient));

            dt = context.ExecuteProcedure("sp_CC_Multiple_Cliente", true).Copy();

            return dt;
        }

  
        public DataTable GetCurrencyCreditClient(int icompany, int iclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@clientnumber", iclient));
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));

            dt = context.ExecuteProcedure("sp_Cliente_ObtieneMonedaPorCliente_v1", true).Copy();

            return dt;
        }

       

        public DataTable GetClientsAutocomplete(int icompany, string sclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@cliente", sclient));

            dt = context.ExecuteProcedure("sp_WSAutoComplementoCliente_ObtieneCliente_v1", true).Copy();
            return dt;
        }

        

        public DataTable GetUsuarioAutocomplete(int icompany, string sclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@Nombre_Completo", sclient));

            dt = context.ExecuteProcedure("sp_WSAutoComplementoUsuario_ObtieneUsuario", true).Copy();

            return dt;
        }


        public DataTable GetAcreedoresAutocomplete(int icompany, string sclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numero_empresa", icompany));
            context.Parametros.Add(new SqlParameter("@acreedor", sclient));

            dt = context.ExecuteProcedure("sp_WSAutoComplemento_ObtieneAcreedores", true).Copy();

            return dt;
        }

       

        public DataTable GetDeudoresAutocomplete(int icompany, string sdeudores)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numero_empresa", icompany));
            context.Parametros.Add(new SqlParameter("@deudor", sdeudores));

            dt = context.ExecuteProcedure("sp_WSAutoComplemento_ObtieneDeudores", true).Copy();

            return dt;
        }

        
        public DataTable GetProveedoresAutocomplete(int icompany, string sdeudores)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numero_empresa", icompany));
            context.Parametros.Add(new SqlParameter("@proveedor", sdeudores));

            dt = context.ExecuteProcedure("sp_WSAutoComplemento_ObtieneProveedores", true).Copy();

            return dt;
        }
  

        public DataTable GetClientsInvoiceAutocomplete(int icompany, string sclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@cliente", sclient));

            dt = context.ExecuteProcedure("sp_WSClienteFactura_ObtieneClientes_v1", true).Copy();

            return dt;
        }

    
        public DataTable GetClientesConFacturasAutocomplete(int icompany, string sclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@cliente", sclient));

            dt = context.ExecuteProcedure("sp_WSClientesConFacturas", true).Copy();

            return dt;
        }


        //Se ocupa en el servicio
        public DataTable GetClientesFacturaAutocomplete(int icompany, string sclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", icompany));
            context.Parametros.Add(new SqlParameter("@Nombre_Completo", sclient));

            dt = context.ExecuteProcedure("sp_WsObtienePersonasPorDomicilioFiscal", true).Copy();

            return dt;
        }

       

        //ESTE PROCEDIMIENTO NO SE ENCUENTRA EN INTEGRA_DEMO
        public DataTable GetClientsNoteAutocomplete(int icompany, string sclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", icompany));
            context.Parametros.Add(new SqlParameter("cliente", sclient));

            dt = context.ExecuteProcedure("sp_WSClienteNotasAuto_ObtieneClientes_v1", true).Copy();

            return dt;
        }


       
        //EL PROCEDIMIENTO NO SE ENCUENTRA EN INTEGRA_DEMO
        public DataTable GetNewClientsNoteAutocomplete(int icompany, string sclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", icompany));
            context.Parametros.Add(new SqlParameter("cliente", sclient));

            dt = context.ExecuteProcedure("sp_WSClienteNotasAuto_ObtieneNuevosClientes_v1", true).Copy(); 

            return dt;
        }

     

        //EL PROCEDIMIENTO NO SE ENCUENTRA EL PROCEDIMIENT EN INTEGRA_DEMO
        public DataTable GetNewClientsInvoiceAutocomplete(int icompany, string sclient, int icontabiliza)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", true));
            context.Parametros.Add(new SqlParameter("cliente", true));
            context.Parametros.Add(new SqlParameter("contabiliza", true));

            dt = context.ExecuteProcedure("sp_WSClienteNotasAuto_ObtieneFacturasDeNuevosClientes_v1", true).Copy();

            return dt;
        }

        public static DataTable ContabilidadCuentaMayor(int iEmpresa)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));

            dt = context.ExecuteProcedure("[sp_ObtieneContabilidadCuentaMayor]", true).Copy();
            return dt;

        }

        public static int AuxContableCliente(int iEmpresa, int iCliente, string sNombreCliente, int iTipoCliente)
        {

            int iRespuesta = 0;
            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Cliente", iCliente));
                context.Parametros.Add(new SqlParameter("@Nombre_Completo", sNombreCliente));//
                context.Parametros.Add(new SqlParameter("@Tipo_Cliente", iTipoCliente));
                context.ExecuteProcedure("[sp_Auxiliares_Contables_Cliente]", true).Copy();

            }
            catch
            {
                iRespuesta = -1;
            }
            return iRespuesta;

        }

      
        public static DataTable consultaAcreedorExistente(int iEmpresa, string sNombre, string sRfc)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroempresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("@nombrecompleto", sNombre.Trim()));
            context.Parametros.Add(new SqlParameter("@rfc", sRfc));

            dt = context.ExecuteProcedure("[sp_Controles_ObtieneExistenciaDelCliente_v3]", true).Copy();
            return dt;

        }

        public static DataTable consultaPersonaExistente(string sNombre, string sRfc)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@nombrecompleto", sNombre.Trim()));
            context.Parametros.Add(new SqlParameter("@rfc", sRfc));

            dt = context.ExecuteProcedure("[sp_Controles_ObtieneExistenciaPersona]", true).Copy();
            return dt;

        }

        public static bool ActualizaPersonaGeneral(int intNumeroPersona)
        {
            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@NumeroPersona", intNumeroPersona));

                context.ExecuteProcedure("sp_ActualizaPersonaaGeneral", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static DataTable consultaAcreedor(int iEmpresa, int iPersona)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Persona", iPersona));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));

            dt = context.ExecuteProcedure("[sp_PersonaProveedor]", true).Copy();
            return dt;

        }


        public static DataTable consultaDeudor(int iEmpresa, int iPersona)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Persona", iPersona));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));

            dt = context.ExecuteProcedure("[sp_PersonaDeudor]", true).Copy();
            return dt;

        }


        public static DataTable sp_Cuentas_Pagar_Deudor(int iTipoMovimiento, int iEmpresa, int iPersona, string sNombreCompleto, string sPaterno, string sMaterno, string sNombre, string sRFC, string sMail)
        {

            sNombreCompleto = sNombre + ' ' + sPaterno + ' ' + sMaterno;
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@TipoMovimiento", iTipoMovimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Persona", iPersona));
            context.Parametros.Add(new SqlParameter("@Nombre_Completo", sNombreCompleto));
            context.Parametros.Add(new SqlParameter("@Paterno", sPaterno));
            context.Parametros.Add(new SqlParameter("@Materno", sMaterno));
            context.Parametros.Add(new SqlParameter("@Nombre", sNombre));
            context.Parametros.Add(new SqlParameter("@RFC", sRFC));
            context.Parametros.Add(new SqlParameter("@Mail", sMail));

            dt = context.ExecuteProcedure("[sp_Cuentas_Pagar_Deudor]", true).Copy();
            return dt;

        }
        public DataTable GetCXPProveedoresAutocomplete(int iEmpresa,int opPersonalidad, string sProveedor)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("@optPersonalidad", opPersonalidad));
            context.Parametros.Add(new SqlParameter("@sProveedor", sProveedor));

            dt = context.ExecuteProcedure("sp_CXP_Facturas_Filtro_Proveedor", true).Copy();

            return dt;
        }
        public static int AuxContableClienteFacturaLibre(int iEmpresa, int iCliente, string sNombreCliente)
        {

            int iRespuesta = 0;
            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Cliente", iCliente));
                context.Parametros.Add(new SqlParameter("@Nombre_Completo", sNombreCliente));
                context.ExecuteProcedure("[sp_Auxiliares_Contables_Cliente]", true).Copy();

            }
            catch
            {
                iRespuesta = -1;
            }
            return iRespuesta;

        }

        public DataTable GetProductosAutocomplete(int Numero_Empresa, string Descripcion)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("Descripcion", Descripcion));
            dt = context.ExecuteProcedure("sp_Productos_Conceptos_Consulta", true).Copy();
            return dt;

        }
        public static DataTable consultaPersonaExistenteRFC(string sRfc)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@rfc", sRfc));

            dt = context.ExecuteProcedure("[sp_ObtienePersonaporRFC]", true).Copy();
            return dt;

        }
        
    }
}
