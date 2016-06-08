using System;
using System.Data;
using System.Data.SqlClient;
using IntegraBussines;
using IntegraData;

namespace Integra_Develoment
{
    public class Addres
    {
        private int numero;
        private int numero_persona;
        private string domicilio;
        private string colonia;
        private string delegacion;
        private string estado;
        private string codigo_postal;
        private string telefono;
        private string fax;
        private int fiscal;
        private int clas_tipo_dom;
        private int clas_pais;
        Person Person;

        public Addres()
        {
            Clas_Tipo_Domicilio = 13;
        }

        public Addres(int NumeroPersona)
        {
            Numero = 0;
            Numero_Persona = NumeroPersona;
            Domicilio = null;
            Colonia = null;
            Delegacion = null;
            Estado = null;
            Codigo_Postal = null;
            Telefono = null;
            Fax = null;
            Fiscal = 1;
            Clas_Tipo_Domicilio = 13;
            //Clas_Pais = 113;
        }

        public int GetFiscalAddresFromPersonNumber()
        {
            int itypeperson = 13;
            try
            {
                DataTable TableFiscalAddres = PersonBLL.ObtenDomicilioPersonaPorTipoDomicilio(Numero_Persona, itypeperson);

                if (TableFiscalAddres != null)
                {
                    Numero = (TableFiscalAddres.Rows[0][0] == DBNull.Value) ? 0 : Convert.ToInt32(TableFiscalAddres.Rows[0][0]);
                    numero_persona = (TableFiscalAddres.Rows[0][1] == DBNull.Value) ? 0 : Convert.ToInt32(TableFiscalAddres.Rows[0][1]);
                    Domicilio = TableFiscalAddres.Rows[0][2].ToString();
                    Colonia = TableFiscalAddres.Rows[0][3].ToString();
                    Delegacion = TableFiscalAddres.Rows[0][4].ToString();
                    Estado = TableFiscalAddres.Rows[0][5].ToString();
                    Codigo_Postal = TableFiscalAddres.Rows[0][6].ToString();
                    telefono = TableFiscalAddres.Rows[0][7].ToString();
                    fax = TableFiscalAddres.Rows[0][8].ToString();
                    fiscal = (TableFiscalAddres.Rows[0][9] == DBNull.Value) ? 1 : Convert.ToInt32(TableFiscalAddres.Rows[0][9]);
                    //num domicilio persona
                    Clas_Tipo_Domicilio = (TableFiscalAddres.Rows[0][11] == DBNull.Value) ? 0 : Convert.ToInt32(TableFiscalAddres.Rows[0][11]);
                    //familia
                    Clas_Pais = (TableFiscalAddres.Rows[0][13] == DBNull.Value) ? -2 : Convert.ToInt32(TableFiscalAddres.Rows[0][13]);
                    //familia pais 
                }
            }
            catch (Exception sqlEx)
            {
                //logear excecion
                return -1;
            }


            return 1;
        }

        public int GetAddresFromPersonNumber()
        {

            try
            {
                DataTable TableAddres = PersonBLL.ObtenDomicilioPersona(Numero_Persona);

                if (TableAddres != null)
                {
                    Numero = (TableAddres.Rows[0][0] == DBNull.Value) ? 0 : Convert.ToInt32(TableAddres.Rows[0][0]);
                    numero_persona = (TableAddres.Rows[0][1] == DBNull.Value) ? 0 : Convert.ToInt32(TableAddres.Rows[0][1]);
                    Domicilio = TableAddres.Rows[0][2].ToString();
                    Colonia = TableAddres.Rows[0][3].ToString();
                    Delegacion = TableAddres.Rows[0][4].ToString();
                    Estado = TableAddres.Rows[0][5].ToString();
                    Codigo_Postal = TableAddres.Rows[0][6].ToString();
                    telefono = TableAddres.Rows[0][7].ToString();
                    fax = TableAddres.Rows[0][8].ToString();
                    fiscal = (TableAddres.Rows[0][9] == DBNull.Value) ? 1 : Convert.ToInt32(TableAddres.Rows[0][9]);
                    Clas_Tipo_Domicilio = (TableAddres.Rows[0][11] == DBNull.Value) ? 0 : Convert.ToInt32(TableAddres.Rows[0][11]);
                    Clas_Pais = (TableAddres.Rows[0][13] == DBNull.Value) ? -2 : Convert.ToInt32(TableAddres.Rows[0][13]);
                }
            }
            catch
            {
                //logear excecion

                return -1;
            }

            return 1;
        }

        public bool InsertAddresPerson(int Tipo_Movimiento, string connectionString, int Numero_Domicilio)
        {
            SqlConnection Conn;

            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_Domicilio", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TipoMovimiento", Tipo_Movimiento);
                command.Parameters.AddWithValue("@Numero", Numero_Domicilio);
                command.Parameters.AddWithValue("@Numero_Persona", Numero_Persona);
                command.Parameters.AddWithValue("@Domicilio", Domicilio);
                command.Parameters.AddWithValue("@Colonia", Colonia);
                command.Parameters.AddWithValue("@Delegacion_Municipio", Delegacion);
                command.Parameters.AddWithValue("@Estado", Estado);
                command.Parameters.AddWithValue("@Codigo_Postal", Codigo_Postal);
                command.Parameters.AddWithValue("@Telefono", Telefono);
                command.Parameters.AddWithValue("@Fax", Fax);
                command.Parameters.AddWithValue("@Fiscal", Fiscal);
                command.Parameters.AddWithValue("@Clas_Tipo_Dom", Clas_Tipo_Domicilio);
                command.Parameters.AddWithValue("@Clas_Pais", Clas_Pais);

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

            if (Tipo_Movimiento == 1)
            {
                if (result > 0)
                {
                    //string Query_addresnumber = "select MAX(Numero) as Numero from domicilio";

                    //SqlCommand command_addresnumber;
                    //SqlDataReader data_addresnumber = null;

                    //try
                    //{

                    //    INTEGRA.SQL.open_connection(Conn);

                    //    command_addresnumber = INTEGRA.SQL.get_sqlCommand(Query_addresnumber, Conn);

                    //    data_addresnumber = INTEGRA.SQL.get_dataReader(command_addresnumber);

                    //    if (data_addresnumber != null)
                    //    {
                    //        data_addresnumber.Read();
                    //        Numero = Convert.ToInt32(data_addresnumber["Numero"]);
                    //        data_addresnumber.Close();
                    //        data_addresnumber.Dispose();
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
                        DataTable dt = new DataTable();

                        dt = context.ExecuteProcedure("spObtieneDomicilio", false).Copy();

                        if (dt.Rows.Count > 0)
                        {
                            Numero = Convert.ToInt32(dt.Rows[0][0]);
                        }

                    }
                    catch (Exception sqlEx)
                    {
                        return false;
                    }


                }
            }

            return true;
        }

        public bool InsertAddres(string connectionString, int Company)
        {
            SqlConnection Conn;

            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_InsertaDomicilio", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Empresa", Company);
                command.Parameters.AddWithValue("@Numero_Persona", Numero_Persona);
                command.Parameters.AddWithValue("@Domicilio", Domicilio);
                command.Parameters.AddWithValue("@Colonia", Colonia);
                command.Parameters.AddWithValue("@Delegacion", Delegacion);
                command.Parameters.AddWithValue("@Estado", Estado);
                command.Parameters.AddWithValue("@CP", Codigo_Postal);
                command.Parameters.AddWithValue("@Telefono", Telefono);
                command.Parameters.AddWithValue("@Fiscal", Fiscal);
                command.Parameters.AddWithValue("@Tipo_Domicilio_Clasificacion", Clas_Tipo_Domicilio);
                command.Parameters.AddWithValue("@Pais", Clas_Pais);

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

        public bool ModifyAddres(string connectionString, int Company, int Numero_Domicilio)
        {
            SqlConnection Conn;

            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_ModificaDomicilio", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Numero_Persona", Numero_Persona);
                command.Parameters.AddWithValue("@Numero_Domicilio", Numero_Domicilio);
                command.Parameters.AddWithValue("@Domicilio", Domicilio);
                command.Parameters.AddWithValue("@Colonia", Colonia);
                command.Parameters.AddWithValue("@Delegacion", Delegacion);
                command.Parameters.AddWithValue("@Estado", Estado);
                command.Parameters.AddWithValue("@CP", Codigo_Postal);
                command.Parameters.AddWithValue("@Telefono", Telefono);
                command.Parameters.AddWithValue("@Fiscal", Fiscal);
                command.Parameters.AddWithValue("@Tipo_Domicilio_Clasificacion", Clas_Tipo_Domicilio);
                command.Parameters.AddWithValue("@Pais", Clas_Pais);

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

        public bool InsertAddresProvider(int Tipo_Movimiento, string connectionString, int Numero_Domicilio, int Fiscal, int Clas_Tipo_Domicilio)
        {
            SqlConnection Conn;

            int result = 0;

            Conn = INTEGRA.SQL.get_sqlConnection(connectionString);

            try
            {
                INTEGRA.SQL.open_connection(Conn);

                SqlCommand command = new SqlCommand("dbo.sp_Domicilio", Conn);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TipoMovimiento", Tipo_Movimiento);
                command.Parameters.AddWithValue("@Numero", Numero_Domicilio);
                command.Parameters.AddWithValue("@Numero_Persona", Numero_Persona);
                command.Parameters.AddWithValue("@Domicilio", Domicilio);
                command.Parameters.AddWithValue("@Colonia", Colonia);
                command.Parameters.AddWithValue("@Delegacion_Municipio", Delegacion);
                command.Parameters.AddWithValue("@Estado", Estado);
                command.Parameters.AddWithValue("@Codigo_Postal", Codigo_Postal);
                command.Parameters.AddWithValue("@Telefono", Telefono);
                command.Parameters.AddWithValue("@Fax", Fax);
                command.Parameters.AddWithValue("@Fiscal", Fiscal);
                command.Parameters.AddWithValue("@Clas_Tipo_Dom", Clas_Tipo_Domicilio);
                command.Parameters.AddWithValue("@Clas_Pais", Clas_Pais);

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

            if (Tipo_Movimiento == 1)
            {
                if (result > 0)
                {
                    //string Query_addresnumber = "select MAX(Numero) as Numero from domicilio";

                    //SqlCommand command_addresnumber;
                    //SqlDataReader data_addresnumber = null;

                    //try
                    //{
                    //    INTEGRA.SQL.open_connection(Conn);

                    //    command_addresnumber = INTEGRA.SQL.get_sqlCommand(Query_addresnumber, Conn);

                    //    data_addresnumber = INTEGRA.SQL.get_dataReader(command_addresnumber);

                    //    if (data_addresnumber != null)
                    //    {
                    //        data_addresnumber.Read();
                    //        Numero = Convert.ToInt32(data_addresnumber["Numero"]);
                    //        data_addresnumber.Close();
                    //        data_addresnumber.Dispose();
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
                        DataTable dt = new DataTable();

                        dt = context.ExecuteProcedure("[spObtieneDomicilio]", false);

                        if (dt.Rows.Count > 0)
                        {
                            Numero = Convert.ToInt32(dt.Rows[0][0]);
                        }
                    }
                    catch (Exception sqlEx)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int Numero { get { return numero; } set { numero = value; } }
        public int Numero_Persona { get { return numero_persona; } set { numero_persona = value; } }
        public string Domicilio { get { return domicilio; } set { domicilio = value; } }
        public string Colonia { get { return colonia; } set { colonia = value; } }
        public string Delegacion { get { return delegacion; } set { delegacion = value; } }
        public string Estado { get { return estado; } set { estado = value; } }
        public string Codigo_Postal { get { return codigo_postal; } set { codigo_postal = value; } }
        public string Telefono { get { return telefono; } set { telefono = value; } }
        public string Fax { get { return fax; } set { fax = value; } }
        public int Fiscal { get { return fiscal; } set { fiscal = value; } }
        public int Clas_Tipo_Domicilio { get { return clas_tipo_dom; } set { clas_tipo_dom = value; } }
        public int Clas_Pais { get { return clas_pais; } set { clas_pais = value; } }

    }
}