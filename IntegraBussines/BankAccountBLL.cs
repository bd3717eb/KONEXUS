using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IntegraData;


namespace IntegraBussines
{
    public class BankAccountBLL
    {
    
      
        public static DataTable GetBanks(int iEmpresa, int iUsuario, int iMoneda, decimal dMonto, int iTipo)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@type", iTipo));
            context.Parametros.Add(new SqlParameter("@import", dMonto));
            context.Parametros.Add(new SqlParameter("@currency", iMoneda));
            context.Parametros.Add(new SqlParameter("@personnumber", iUsuario));
           
         

            dt = context.ExecuteProcedure("sp_CuentaBancaria_ObtieneBancos_v1", true).Copy();
            // sp_ se encuentra solo en INTEGRA_TABLADECO_TMP del 21 verificar si se usa este método
            return dt;
        }


        public static DataTable GetBankAccounts(int iEmpresa, int iUsuario, int iMoneda,int iBanco, decimal dMonto, int iTipo)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@type", iTipo));
            context.Parametros.Add(new SqlParameter("@import", dMonto));
            context.Parametros.Add(new SqlParameter("@banknumber", iUsuario));
            context.Parametros.Add(new SqlParameter("@currency", iMoneda));
            context.Parametros.Add(new SqlParameter("@personnumber", iEmpresa));

            dt = context.ExecuteProcedure("sp_CuentaBancaria_ObtieneCuentasBancrias_v1", true).Copy();
            // sp_ se encuentra solo en INTEGRA_TABLADECO_TMP del 21 verificar si se usa este método
            return dt;
        }
    }
}
