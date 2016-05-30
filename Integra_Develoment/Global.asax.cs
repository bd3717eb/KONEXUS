using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using IntegraData;

namespace Integra_Develoment
{
    public class Global : System.Web.HttpApplication
    {
        private static string company_number;
        private static string srutadoc;
        private static string rutaelectronico;
        private static string archivozip;
        private static string person_number;
        private static string database;
        private static bool _Product;
        private static int product_type;
        private static int office;
        private static int article_type;
        private DataTable GlobalTable;
        public static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["INTEGRAPRODUCCIONI"].ConnectionString;

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["SesionActiva"] = 1;
            if (Session.IsNewSession && Session["Person"] == null || Session["Company"] == null || Session["Office"] == null)
                Response.Redirect("~/Default.aspx");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            try
            {
                Application.Lock();
                SQLConection context = new SQLConection();
                context.ExecuteQuery("UPDATE usuario SET EnLinea = 0 WHERE Numero_persona = " + Convert.ToInt32(Session["Person"]));
                Session["SesionActiva"] = null;
                Application.UnLock();
            }
            catch
            {

            }
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
public static class Globales
{
    public static double dMoneda { get; set; }
    public static Dictionary<int, string> gfFacturacionDefinicionTimbrado()
    {
        Dictionary<int, string> FacturacionDefinicionTimbrado = new Dictionary<int, string>();
        FacturacionDefinicionTimbrado.Add(-1, " - ");
        FacturacionDefinicionTimbrado.Add(0, "Cancelación realizada con éxito");
        FacturacionDefinicionTimbrado.Add(301, "La petición no contiene un XML válido conforme al estándar W3C");
        FacturacionDefinicionTimbrado.Add(302, "El sello del CFDi es inválido");
        FacturacionDefinicionTimbrado.Add(303, "El certificado no corresponde al RFC de emisor");
        FacturacionDefinicionTimbrado.Add(304, " El certificado fue revocado o cancelado por SAT");
        FacturacionDefinicionTimbrado.Add(305, "El certificado no es vigente a la fecha de emisión del CFDi");
        FacturacionDefinicionTimbrado.Add(307, "El CFDi ya tiene un nodo de timbre");
        FacturacionDefinicionTimbrado.Add(401, "Se ha vencido el tiempo máximo de 72 horas para timbrar el CFDi");
        FacturacionDefinicionTimbrado.Add(402, "El emisor no se encuentra en la LCO del SAT con certificado {}");
        FacturacionDefinicionTimbrado.Add(403, "Versión de CFDi no es vigente a la fecha de emisión de CFDi");
        FacturacionDefinicionTimbrado.Add(580, "NO se realizó la cancelación");
        FacturacionDefinicionTimbrado.Add(600, "Token inválido");
        FacturacionDefinicionTimbrado.Add(601, "La contraseña no corresponde a la llave privada");
        FacturacionDefinicionTimbrado.Add(602, " El certificado no corresponde a la llave privada");
        FacturacionDefinicionTimbrado.Add(700, "Identificador no válido");
        FacturacionDefinicionTimbrado.Add(701, "No existe comprobante con UUID {}");
        FacturacionDefinicionTimbrado.Add(702, " No existe comprobante con Id {}");
        FacturacionDefinicionTimbrado.Add(800, "La petición no contiene datos");
        FacturacionDefinicionTimbrado.Add(801, "El CFDi no contiene atributo versión en nodo Comprobante");
        FacturacionDefinicionTimbrado.Add(802, "Esta versión de CFDi no es vigente");
        FacturacionDefinicionTimbrado.Add(803, "Versión de CFDi inválida {}");
        FacturacionDefinicionTimbrado.Add(804, "Error en estructura del XML");
        FacturacionDefinicionTimbrado.Add(805, "El certificado codificado no es correcto");
        FacturacionDefinicionTimbrado.Add(806, " El certificado codificado no coincide con el reportado");
        FacturacionDefinicionTimbrado.Add(807, "La fecha-hora del CFDi es superior a la fecha-hora actual");
        FacturacionDefinicionTimbrado.Add(808, "El Id no debe ser mayor a 25 caracteres");
        FacturacionDefinicionTimbrado.Add(810, "Se agotaron los timbres contratados");
        FacturacionDefinicionTimbrado.Add(812, "Este CFDi ya ha sido recibido y timbrado con UUID {}");
        FacturacionDefinicionTimbrado.Add(813, "El Id es requerido, alfanumérico máximo 25 caracteres");
        FacturacionDefinicionTimbrado.Add(815, "El RFC {} es inválido");
        FacturacionDefinicionTimbrado.Add(816, "Es requerido Id de Emisor y UUID del CFDi");
        FacturacionDefinicionTimbrado.Add(817, "El emisor no tiene CSD activo y/o vigente registrado en Servisim");
        FacturacionDefinicionTimbrado.Add(818, "El CFDi no ha sido enviado aún al SAT");
        FacturacionDefinicionTimbrado.Add(820, "El CFDi está en proceso de cancelación");
        FacturacionDefinicionTimbrado.Add(821, "El CFDi es cancelable hasta 72 horas después de timbrado");
        FacturacionDefinicionTimbrado.Add(900, "Error interno");
        return FacturacionDefinicionTimbrado;
    }
    public static Dictionary<int, string> gfMeses()
    {
        Dictionary<int, string> dicMes = new Dictionary<int, string>();
        dicMes.Add(1, "Enero");
        dicMes.Add(2, "Febrero");
        dicMes.Add(3, "Marzo");
        dicMes.Add(4, "Abril");
        dicMes.Add(5, "Mayo");
        dicMes.Add(6, "Junio");
        dicMes.Add(7, "Julio");
        dicMes.Add(8, "Agosto");
        dicMes.Add(9, "Septiembre");
        dicMes.Add(10, "Octubre");
        dicMes.Add(11, "Noviembre");
        dicMes.Add(12, "Diciembre");
        return dicMes;
    }
    public static Dictionary<int, string> gfAnios()
    {
        Dictionary<int, string> dicAnio = new Dictionary<int, string>();
        int iRecorrer = DateTime.Now.Year - 12;

        //for (int i = iRecorrer; i < (DateTime.Now.Year + 1); i++)
        //    dicAnio.Add(i, i.ToString());

        for (int iB = (DateTime.Now.Year); iB > iRecorrer; iB--)
        {
            dicAnio.Add(iB, iB.ToString());
        }
        return dicAnio;
    }
    //Eblaher
    #region Variables
    public const int Default = 0;
    public const int Primero = 1;
    public const int OK = 1;
    public const int Incidencia = -1;
    public const int Indefinido = 10;
    public const string MsgSinRegistros = " * No hay registros ";
    public const string MsgIncidencia = " * Ocurrió una incidencia ";
    public const string MsgSeleccione = " * Seleccione un elemento ";
    public const string MsgSobreEscribir = " * ¿ Desea remplazar ? ";
    public const string MsgIncidenciaPresupuestoOp = "Incidencia_presupuesto_operacion";
    public const string MsgIncidenciaPresupuestoInv = "Incidencia_presupuesto_inversion";
    public const string MsgIncidenciaEjercisioInv = "Incidencia_ejercicio_inversion";
    public const string MsgOK = " * Se realizo la operación exitosamente ";
    public const string MsgAutenticacion = " Error de Autenticación , favor de contactar al administrador.";
    public const string NombreClienteDefault = "-1 - MOSTRADOR";

    public static string ICSucursal = "";

    public static string VersionEjercicio { get; set; }
    public static string VersionPresupuesto { get; set; }
    public static string Usuario { get; set; }
    public static string Anio { get; set; }

    public static int FatherDefault = 1;
    public static int FamilyDefault = 9;

    public static double dRETISR { get; set; }
    public static double dRETIVA { get; set; }
    public static double dTOTALTOTAL { get; set; }


    #endregion
}
namespace INTEGRA
{
    public class Global : System.Web.HttpApplication
    {
        private static string company_number;
        private static string srutadoc;
        private static string rutaelectronico;
        private static string archivozip;
        private static string person_number;
        private static string database;
        private static bool _Product;
        private static int product_type;
        private static int office;
        private static int article_type;
        private DataTable GlobalTable;
        public static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["INTEGRAPRODUCCIONI"].ConnectionString;

        public static ReportDocument GlobalDoc
        {
            get;
            set;

        }


        public static int download
        {
            get;
            set;

        }

        public static string DataBase
        {
            get
            {
                return database;
            }
            set
            {
                database = value;
            }
        }


        public static string Company
        {
            get
            {
                return company_number;
            }
            set
            {
                company_number = value;
            }
        }


        public static string Rutadoc
        {
            get
            {
                return srutadoc;
            }
            set
            {
                srutadoc = value;
            }
        }

        public static string Rutaelectronico
        {
            get
            {
                return rutaelectronico;
            }
            set
            {
                rutaelectronico = value;
            }
        }



        public static string ArchivoZip
        {
            get
            {
                return archivozip;
            }
            set
            {
                archivozip = value;
            }
        }


        public static string Person
        {
            get
            {
                return person_number;
            }
            set
            {
                person_number = value;
            }
        }

        public static bool Product
        {
            get
            {
                return _Product;
            }
            set
            {
                _Product = value;
            }
        }

        public static int Product_type
        {
            get
            {
                return product_type;
            }
            set
            {
                product_type = value;
            }
        }

        public static int Office
        {
            get
            {
                return office;
            }
            set
            {
                office = value;
            }
        }

        public static int Article_type
        {
            get
            {
                return article_type;
            }
            set
            {
                article_type = value;
            }
        }

        public struct searchcriteria
        {
            public int Tipo;
            public int Sucursal;
            public int Moneda;
            public int Cliente;
            public DateTime FechaInicial;
            public DateTime FechaFinal;
        }

        public struct PayData
        {
            public DateTime Fecha;
            public decimal importe;
            public int Moneda;
            public int concepto;
            public int banco;
            public int cuenta;
            public string referencia;
            public decimal tipocambio;
            public string desc_cuenta;
        }
    }

    public class SQL
    {
        public static SqlConnection get_sqlConnection(string sqlConnectionString)
        {
            return new SqlConnection(sqlConnectionString);
        }

        public static void open_connection(SqlConnection sqlConnection)
        {
            sqlConnection.Open();
        }

        public static SqlCommand get_sqlCommand(string sqlQuery, SqlConnection sqlConnection)
        {
            return new SqlCommand(sqlQuery, sqlConnection);
        }

        public static SqlDataReader get_dataReader(SqlCommand sqlCommand)
        {
            return sqlCommand.ExecuteReader();
        }

        public static void close_connection(SqlConnection sqlConnection)
        {
            sqlConnection.Close();
        }

        public static void insert_parameters(SqlCommand command, params string[] list)
        {
            int i = 0;

            while (i < list.Length)
                command.Parameters.AddWithValue(list[i++], list[i++]);
        }
    }
}