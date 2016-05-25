using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IntegraData;

namespace IntegraBussines
{
    public class ProviderBLL : PersonBLL
    {
        private int _providerclass;
        private int _status;
        private string _giro;
        private int _payperiod;
        private int _cpayperiod;
        private int _pay;
        private int _zone;
        private int _pricelist;
        private int _paydata;
        private int _cpaydata;

        public int CustomerClass { get { return _providerclass; } set { _providerclass = value; } }
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

   

        public static DataTable  ObtenDatosProveedor(int iNumero_Empresa, int iPersona)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@personnumber", iPersona));
            context.Parametros.Add(new SqlParameter("@companynumber", iNumero_Empresa));

            dt = context.ExecuteProcedure("sp_Persona_ObtieneDatosProveedor_v1", true).Copy();
            return dt;
        }

        public DataTable sqlAutoCompleteProveedoresOrdenDeCompra(int Empresa, string prefijo)
        {
            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", "" + Empresa + ""));
                context.Parametros.Add(new SqlParameter("@Nombre_Completo", "" + prefijo + ""));

                DataTable dt = new DataTable();
                dt = context.ExecuteProcedure("sp_Proveedores_OrdenCompras", true);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtieneProveedoresAutocomplete(int icompany, string sclient)// nuevo sql
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", icompany));
            context.Parametros.Add(new SqlParameter("@persona", sclient));

            dt = context.ExecuteProcedure("sp_WSAutoComplementoProveedores", true).Copy();
            return dt;
        }


 

    }
}
