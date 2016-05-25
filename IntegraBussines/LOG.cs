using System.Data;
using IntegraData;
using System.Data.SqlClient;
using System.Net;
using System.Web.UI.WebControls;
using System.Web;
public static class LOG
{
    public static string gsIncidencia = "  * Incidencia ";
    public static void gfLog(string psUser, string psDETAIL, string psFUNCTION_METHOD, string psMODULE, ref Label plblMsg, DataTable dt = null)
    {
        SQLConection context = new SQLConection();
        dt = new DataTable();
        // ================================================================================================
        // get IP parameter and Location parameter with webservice friendly
        // ================================================================================================
        string sIP = string.Empty;
        string sLocation = string.Empty;
        try
        {
            //sIP = new WebClient().DownloadString("http://api.hostip.info/get_json.php");
            //sLocation = (sIP.Length > 250) ? sIP.Substring(0, (250 - 1)) : sIP;
            //sIP = sIP.Substring(sIP.ToUpper().LastIndexOf("IP"), (sIP.Length - sIP.ToUpper().LastIndexOf("IP")));
        }
        catch
        {
            sIP = "localhost";
            sLocation = sIP;
        }
        // ================================================================================================
        // get [OS_BROWSER] parameter
        // ================================================================================================
        string sOS_BROWSER = string.Empty;
        try
        {
            HttpBrowserCapabilities brObject = HttpContext.Current.Request.Browser;
            sOS_BROWSER = "Tipo:" + brObject.Type + "|Version: " + brObject.Version;
            sOS_BROWSER = (sOS_BROWSER.Length > 50) ? sOS_BROWSER.Substring(0, 50) : sOS_BROWSER;
        }
        catch
        {
            sOS_BROWSER = "localhost";
        }

        //psDETAIL = psDETAIL.Length > 250 ? psDETAIL.Substring(0, 250) : psDETAIL;
        context.Parametros.Clear();
        context.Parametros.Add(new SqlParameter("@IP", sIP));
        context.Parametros.Add(new SqlParameter("@LOCATION", sLocation));
        context.Parametros.Add(new SqlParameter("@OS_BROWSER", sOS_BROWSER));
        context.Parametros.Add(new SqlParameter("@FUNCTION_METHOD", psFUNCTION_METHOD));
        context.Parametros.Add(new SqlParameter("@MODULE", psMODULE));
        context.Parametros.Add(new SqlParameter("@OWNER", psUser));
        context.Parametros.Add(new SqlParameter("@DETAIL", psDETAIL));
        dt = context.ExecuteProcedure("[sp_LOG_Alta]", true).Copy();
        plblMsg.Text = dt.Rows[0][0] + " - " + gsIncidencia + " -  Detalle: " + psDETAIL;
        plblMsg.Visible = true;
    }

    public static void gfLogAlta(string psUser, int EnLinea, string sEstatus = "OnLine")
    {
        SQLConection context = new SQLConection();
        // ================================================================================================
        // get IP parameter and Location parameter with webservice friendly
        // ================================================================================================
        string sIP = string.Empty;
        string sLocation = string.Empty;
        try
        {
            //sIP = new WebClient().DownloadString("http://api.hostip.info/get_json.php");
            //sLocation = (sIP.Length > 250) ? sIP.Substring(0, (250 - 1)) : sIP;
            //sIP = sIP.Substring(sIP.ToUpper().LastIndexOf("IP"), (sIP.Length - sIP.ToUpper().LastIndexOf("IP")));
        }
        catch
        {
            sIP = "localhost";
            sLocation = sIP;
        }
        // ================================================================================================
        // get [OS_BROWSER] parameter
        // ================================================================================================
        string sOS_BROWSER = string.Empty;
        try
        {
            HttpBrowserCapabilities brObject = HttpContext.Current.Request.Browser;
            sOS_BROWSER = "Tipo:" + brObject.Type + "|Version: " + brObject.Version;
            sOS_BROWSER = (sOS_BROWSER.Length > 50) ? sOS_BROWSER.Substring(0, 50) : sOS_BROWSER;
        }
        catch
        {
            sOS_BROWSER = "localhost";
        }

        //psDETAIL = psDETAIL.Length > 250 ? psDETAIL.Substring(0, 250) : psDETAIL;
        context.Parametros.Clear();
        context.Parametros.Add(new SqlParameter("@IP", sIP));
        context.Parametros.Add(new SqlParameter("@LOCATION", sLocation));
        context.Parametros.Add(new SqlParameter("@OS_BROWSER", sOS_BROWSER));
        context.Parametros.Add(new SqlParameter("@FUNCTION_METHOD", sEstatus));
        context.Parametros.Add(new SqlParameter("@MODULE", string.Concat(sEstatus, "|", psUser, "|", HttpContext.Current.Session["Company"].ToString(), "|", (HttpContext.Current.Session["Office"] == null) ? "-" : HttpContext.Current.Session["Office"].ToString())));
        context.Parametros.Add(new SqlParameter("@OWNER", psUser));
        context.Parametros.Add(new SqlParameter("@DETAIL", sEstatus));
        context.Parametros.Add(new SqlParameter("@EnLinea", EnLinea));
        context.ExecuteProcedure("sp_LOG_RegistroIngreso", true).Copy();
    }

}

