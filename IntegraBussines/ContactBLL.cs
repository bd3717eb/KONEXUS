using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;
using System.Data.SqlClient;
using IntegraData;

namespace IntegraBussines
{
    public class ContactBLL : PersonBLL
    {
        private int _contactnumber;
        private string _job;
        private int _sendmail;

        public int ContactNumber { get { return _contactnumber; } set { _contactnumber = value; } }
        public string Job { get { return _job; } set { _job = value; } }
        public int SendMail { get { return _sendmail; } set { _sendmail = value; } }

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

        public int ManageContact(int moviment)
        {
            ContactDAL Contact = new ContactDAL();

            _fullname = _name + " " + _father + " " + " " + _mother;

            string Result = Contact.ManageContact(moviment, _fullname, _father, _mother, _name, _phonenumber, _cellphone, _email, _number, _contactnumber, _job, _company, _sendmail);

            if (Result != null && Convert.ToInt32(Result) > 0)
            {
                return Convert.ToInt32(Result);
            }
            else if (moviment == 2)
                return 1;

            return 0;
        }

        public void GetPersonData(int company, int person)
        {
            DataTable PersonTable;
            string styperson = "F";

            using (PersonTable = ObtenDatosPersona(company, person, styperson))
            {
                if (PersonTable != null && PersonTable.Rows.Count > 0)
                {
                    _number = Convert.ToInt32(PersonTable.Rows[0][0]);
                    _fullname = PersonTable.Rows[0][1].ToString();
                    _father = PersonTable.Rows[0][2].ToString();
                    _mother = PersonTable.Rows[0][3].ToString();
                    _name = PersonTable.Rows[0][4].ToString();
                    _shortname = PersonTable.Rows[0][5].ToString();
                    _rfc = PersonTable.Rows[0][6].ToString();
                    _curp = PersonTable.Rows[0][7].ToString();
                    _phonenumber = PersonTable.Rows[0][8].ToString();
                    _cellphone = PersonTable.Rows[0][9].ToString();
                    _email = PersonTable.Rows[0][10].ToString();
                }
            }
        }

        public void GetContactData(int company, int contact, int customer)
        {
            ContactDAL Contact = new ContactDAL();
            DataTable PersonTable;
            DataTable ContactTable;
            string styperson = "F";

            using (PersonTable = ObtenDatosPersona(company, contact, styperson))
            {
                if (PersonTable != null && PersonTable.Rows.Count > 0)
                {
                    _number = Convert.ToInt32(PersonTable.Rows[0][0]);
                    _fullname = PersonTable.Rows[0][1].ToString();
                    _father = PersonTable.Rows[0][3].ToString();
                    _mother = PersonTable.Rows[0][4].ToString();
                    _name = PersonTable.Rows[0][5].ToString();
                    _shortname = PersonTable.Rows[0][9].ToString();
                    _rfc = PersonTable.Rows[0][2].ToString();
                    _curp = PersonTable.Rows[0][10].ToString();
                    _phonenumber = PersonTable.Rows[0][6].ToString();
                    _cellphone = PersonTable.Rows[0][8].ToString();
                    _email = PersonTable.Rows[0][7].ToString();
                }
            }

            using (ContactTable = GetDataContactClient(company, customer, contact))
            {
                if (ContactTable != null && ContactTable.Rows.Count > 0)
                {
                    _contactnumber = Convert.ToInt32(ContactTable.Rows[0][0]);
                    _job = ContactTable.Rows[0][4].ToString();
                    _sendmail = Convert.ToInt32(ContactTable.Rows[0][6]);
                }
            }
        }

        public DataTable GetDataContactClient(int icompany, int iclient, int icontact)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", icompany));
            context.Parametros.Add(new SqlParameter("customernumber", iclient));
            context.Parametros.Add(new SqlParameter("numero", icontact));

            dt = context.ExecuteProcedure("sp_Cliente_ObtieneDatosContacto_v1", true).Copy();
            return dt;
        }



        public bool IsContact(int company, int contact, int customer)
        {
            ContactDAL Contact = new ContactDAL();
            DataTable ContactTable;

            ContactTable = GetContactClient(company, customer);

            if (ContactTable != null && ContactTable.Rows.Count > 0)
            {
                return true;
            }


            return false;
        }

     

        public DataTable GetContactClient(int icompany, int iclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("customernumber", iclient));
            context.Parametros.Add(new SqlParameter("companynumber", icompany));

            dt = context.ExecuteProcedure("sp_Cliente_ObtieneContactos_BIS_v1", true).Copy();
            return dt;
        }


      

        public DataTable GetContactProvider(int icompany, int iclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("clientnumber", iclient));
            context.Parametros.Add(new SqlParameter("companynumber", icompany));

            dt = context.ExecuteProcedure("sp_Contact_ObtieneDatosDeContactoDeProveedor_v1", true).Copy();
            return dt;
        }

    }
}
