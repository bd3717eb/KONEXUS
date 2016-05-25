using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IntegraBussines
{
    public class toolsBLL
    {

        public DataTable import_to_string(int company, Decimal total, string Moneda,int bandera)
        {
            IntegraData.toolsDAL toolsDAL = new IntegraData.toolsDAL();

            try
            {
                return toolsDAL.import_to_string(company, total, Moneda,bandera);
            }
            catch
            {
                throw;
            }
            finally
            {
                toolsDAL = null;
            }
        }

       
    }
}
