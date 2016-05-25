using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntegraData;
using System.Data.SqlClient;
using System.Data;
using System.Web;


public class controlCC0700
{
    int empresa;

    public int IdEmpresa
    {
        get { return empresa; }
        set { empresa = value; }
    }

    public controlCC0700( int empresa)
    {
        this.empresa = empresa;
    }


    //public DataTable sqlLLenaEmpresa()
    //{
    //    empresa = 1;
    //    SQLConection context = new SQLConection();
    //    context.Parametros.Clear();
    //    context.Parametros.Add(new SqlParameter("@empresa", empresa));

    //    DataTable dt = context.ExecuteProcedure("sp_CC0700_llena_moneda", true);
    //    return dt;
    //}

    public DataTable sqlLLenaEmpresa(controlCC0700 obj)
    {
        SqlConnection con = new SqlConnection("Data Source=DMAXVPN1;Initial Catalog=INTEGRA_ELECDEMO;User ID=sa;Password=gabrmedi");
        con.Open();
        SqlCommand cmd = new SqlCommand("sp_CC0700_llena_moneda", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@empresa", "" + obj.empresa + ""));
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();

        return dt;
    }

    


}

