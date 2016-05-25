using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IntegraData
{//ACTUAL
    public class sqlDBHelper
    {
        static string CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["INTEGRAPRODUCCIONI"].ConnectionString;

        static string PRINTOUTPUT;

        internal static DataTable ExecuteSelectCommand(string sqlQuery)
        {
            DataTable table = null;

            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            table = new DataTable();
                            da.Fill(table);
                        }
                    }
                    catch
                    {
                        return null;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

            return table;
        }

        internal static DataTable ExecuteParamerizedSelectCommand(string sqlQuery, SqlParameter[] param)
        {
            DataTable table = new DataTable();

            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddRange(param);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {

                            con.Open();
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(table);
                        }
                    }
                    catch (Exception sqlEx)
                    {
                        return null;
                    }

                    finally
                    {
                        con.Close();
                    }
                }
            }

            return table;
        }
        static string cachEroro = String.Empty;
        internal static bool ExecuteNonQuery(string CommandName, CommandType cmdType, SqlParameter[] pars)
        {
            int result = 0;
            cachEroro = "";

            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        result = cmd.ExecuteNonQuery();
                    }
                    catch (Exception sqlEx)
                    {
                        Console.Write(sqlEx.Message);
                        cachEroro = sqlEx.Message;
                        return false;

                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

            return (result > 0);
        }


        internal static string sqlRetornaError()
        {
            return cachEroro;
        }
        internal static DataTable ExecuteNonQuerySELECTStatement(string CommandName, CommandType cmdType, SqlParameter[] pars)
        {
            DataSet DataSet = new DataSet();

            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    SqlDataAdapter Adapter;

                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        cmd.ExecuteNonQuery();

                        Adapter = new SqlDataAdapter(cmd);
                        Adapter.Fill(DataSet);
                    }
                    catch (Exception sqlEx)
                    {
                        Console.WriteLine(sqlEx.Message);
                        return new DataTable();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

            return DataSet.Tables[0];
        }

        public static int ExecuteNonQueryOutputValue(string CommandName, string outputvalue, CommandType cmdType, SqlParameter[] pars)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        cmd.ExecuteNonQuery();
                        result = Convert.ToInt32(cmd.Parameters[outputvalue].Value);
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

            return result;
        }

        internal static string ExecuteNonQueryPRINTValue(string CommandName, CommandType cmdType, SqlParameter[] pars)
        {
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                con.InfoMessage += new SqlInfoMessageEventHandler(GetPRINTMessage);

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        if (cmd.ExecuteNonQuery() <= 0)
                            return null;
                    }
                    catch (Exception sqlEx)
                    {
                        return null;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

            return PRINTOUTPUT;
        }

        private static void GetPRINTMessage(object sender, SqlInfoMessageEventArgs e)
        {
            PRINTOUTPUT = e.Message;
        }

        internal static DataTable GetFunction(string Function, SqlParameter[] pars)
        {
            DataTable table = new DataTable();

            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(Function, con))
                {
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(table);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

            return table;
        }
    }
}
