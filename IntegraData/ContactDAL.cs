using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace IntegraData
{
    public class ContactDAL
    {
        public string ManageContact(int imoviment, string sfullname, string sfather, string smother, string sname, string sphonenumber, string scellphone, string semail, int iclientnumber, int icontactnumber, string sjob, int icompany, int isendmail)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("TipoMovimiento", imoviment),
                new SqlParameter("Nombre_Completo", sfullname),
                new SqlParameter("Paterno", sfather),
                new SqlParameter("Materno", smother),
                new SqlParameter("Nombre", sname),
                new SqlParameter("Telefono", sphonenumber),
                new SqlParameter("Celular", scellphone),         
                new SqlParameter("EMail", semail),
                new SqlParameter("Numero_CLiente", iclientnumber),
                new SqlParameter("Numero_Contacto_Persona", icontactnumber),
                new SqlParameter("Puesto", sjob),
                new SqlParameter("Empresa", icompany),
                new SqlParameter("Envio_Mail", isendmail),
            };

            return sqlDBHelper.ExecuteNonQueryPRINTValue("sp_contactos", CommandType.StoredProcedure, parameters);
        }

   
    }
}
