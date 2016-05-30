using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;
using System.Data.SqlClient;
using System.Web;
//using System.Web.UI.WebControls;
using System.Net;
using System.Threading;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace IntegraBussines
{
    public class ICompanyController
    {
        public static DataTable gfauthorization_page(int icompany, int iduser)
        {
            string sQuery = string.Empty;
            DataTable dtauthorizationpages = new DataTable();
            try
            {
                sQuery = " select Numero_empresa,Numero_persona,Numero_programa,Nombre_programa,Tipo_programa,Observaciones,Numero_perfil from vFacultadABC where numero_persona = " + iduser + "  and Numero_empresa = " + icompany + "  and Nombre_programa like 'WEB%' ";
                SQLConection context = new SQLConection();
                dtauthorizationpages = context.ExecuteQuery(sQuery).Copy();
                return dtauthorizationpages;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
        public static int gfIngresaSistema(string psNumero_persona, string psContrasena, ref string psRespuesta)
        {
            int iRespuesta = 0;
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            //string sQuery = string.Concat(@" SELECT Numero_persona,Contraseña,EnLinea FROM Usuario WHERE  Numero_persona = " + psNumero_persona + "  AND Contraseña = '" + psContrasena + "'");
            //SQLConection context = new SQLConection();
            //dt = context.ExecuteQuery(sQuery).Copy();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Usuario", Convert.ToInt32(psNumero_persona)));
            context.Parametros.Add(new SqlParameter("@Contraseña", psContrasena));

            dt = context.ExecuteProcedure("spObtieneEstatusUsuario", true).Copy();


            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["Enlinea"]) > 0)
                {
                    psRespuesta = "El número de usuario o la contraseña son incorrectos";
                    iRespuesta = -1;
                }
                else
                    iRespuesta = 1;
            }
            else
            {
                psRespuesta = "Usuario y/o contraseña incorrectas";
                iRespuesta = -1;
            }

            return iRespuesta;
        }


        public static DataTable gfGetOffices(int picompanynumber, int piuser)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", picompanynumber));
            context.Parametros.Add(new SqlParameter("@user", piuser));
            dt = context.ExecuteProcedure("sp_Controles_ObtieneOficinas_v1", true).Copy();
            return dt;

        }
        public static DataTable gfObtieneEmpresa(int piUsuario)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Usuario", piUsuario));
            dt = context.ExecuteProcedure("sp_Empresa_Consulta", true).Copy();
            return dt;
        }

        //public static string gfCompaniaPathImg(int pIDCompania)
        //{
        //    // falta la ubicacion de archivos acorde a empresa que realizo miguel
        //    if (pIDCompania != 0)
        //    {
        //    }
        //    return @"<img id='idIMG' src='../Images/logo.png' alt='img' height='81' width='178' />";
        //}
        public static string gfCompaniaPathImg(int pIDCompania)
        {
            // falta la ubicacion de archivos acorde a empresa que realizo miguel HttpContext.Current.Session["sImgEmpresa"].ToString()
            string ruta = HttpContext.Current.Session["sImgEmpresa"].ToString();
            string rutaRaiz = string.Empty;
            try
            {
                if (pIDCompania != 0)
                {
                    ruta = ruta.ToUpper().Replace(".JPG", ".PNG");
                    ruta = ruta.ToUpper().Replace("D:", "E:");
                    rutaRaiz = ruta;
                    ruta = ruta.ToUpper().Replace(@"E:\PROGRAM FILES\INTEGRA EMPRESARIAL\", "../INTEGRA EMPRESARIAL/");
                    ruta = ruta.Replace(@"\", "/");

                    if (System.IO.File.Exists(rutaRaiz))
                        return @"<img id='idIMG' src='" + ruta + "' alt='img' height='81' width='178' />";
                    else
                        return @"<img id='idIMG' src='../Images/logo.png' alt='img' height='81' width='178' />";
                }
                else
                {
                    return @"<img id='idIMG' src='../Images/logo.png' alt='img' height='81' width='178' />";
                }
            }
            catch
            {
                return @"<img id='idIMG' src='../Images/logo.png' alt='img' height='81' width='178' />";
            }
        }

        public static string gfCompaniaDireccionHtml(int pIDCompania, DataTable pDT = null)
        {
            SQLConection context = new SQLConection();
            string sResultado = string.Empty;

            pDT = null;
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", pIDCompania));
            pDT = context.ExecuteProcedure("sp_Venta_ObtieneDatosDeEmpresa_v1", true);

            if (pDT.Rows.Count > 0)
            {
                sResultado = string.Concat("<table>");
                foreach (DataRow row in pDT.Rows)
                {
                    sResultado += string.Concat("<tr><td>" + row[1] + "<td/><tr/>");
                    sResultado += string.Concat("<tr><td>" + row[2] + "<td/><tr/>");
                    sResultado += string.Concat("<tr><td>" + row[3] + "<td/><tr/>");
                    sResultado += string.Concat("<tr><td>" + row[4] + "<td/><tr/>");
                    sResultado += string.Concat("<tr><td>C.P. " + row[6] + " Tel " + row[8] + " <td/><tr/>");
                }
                sResultado += string.Concat("</table>");
            }
            return sResultado;
        }

        static int numeroMensajes = 0;
        public static string gfCompaniaMensajesHtml(int dtCompania, DataTable dtMensajes = null)
        {
            numeroMensajes = 0;
            SQLConection context = new SQLConection();
            string Mensajes = string.Empty;

            dtMensajes = null;
            string mensaje = string.Empty;
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", dtCompania));
            dtMensajes = context.ExecuteProcedure("sp_Mensaje_Consulta", true);
            numeroMensajes = dtMensajes.Rows.Count;
            if (dtMensajes.Rows.Count > 0)
            {

                for (int indice = 0; indice < dtMensajes.Rows.Count; indice++)
                {
                    Mensajes += string.Concat("<table id='dtMensaje_" + indice.ToString() + "'style= 'font-size:" + dtMensajes.Rows[indice][2].ToString() + "px;  color:" + dtMensajes.Rows[indice][3].ToString() + ";  font-family:" + dtMensajes.Rows[indice][1].ToString() + "; display:none;'>");
                    Mensajes += string.Concat("<tr><td>" + dtMensajes.Rows[indice][0].ToString() + "<td/><tr/>");
                    Mensajes += string.Concat("</table>");

                }

            }
            else
            {
                return gfCompaniaDireccionHtml(dtCompania);

            }


            return Mensajes;
        }

        public static int getNumeroDeMensajes()
        {
            return numeroMensajes;
        }

        public static string getTiempoTrancicion(int dtCompania, DataTable dtTiempo = null)
        {
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero", dtCompania));
            dtTiempo = context.ExecuteProcedure("sp_Mensajes_Buscar_Tiempo", true);
            string tiempo = string.Empty;

            if (dtTiempo.Rows.Count > 0)
            {
                foreach (DataRow row in dtTiempo.Rows)
                {
                    tiempo = dtTiempo.Rows[0][0].ToString();
                }
            }
            else
            {
                tiempo = "5";
            }

            return tiempo;
        }
        public static string gfCompaniaUsuarioDatosPersonales(int Numero_Empresa, int Numero, int general = 1)//tenia 1
        {
            DataTable dtTemp = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero", Numero));
            dtTemp = context.ExecuteProcedure("sp_Usuario_ObtieneDatosDelUsuario_v2", true).Copy();
            if (dtTemp.Rows.Count > 0)
                //../Images/salir.png
                return string.Concat(@"<img id='imgSalir' class='lblSalirSistemaIII'   src='../Images/salir.png' onclick='gfSalirSistema()' /> <label class='lblSalirSistema2' onclick='gfSalirSistema()'>Salir del Sistema</label> <p/> ",
                                      "<label id='lblUsers' class='lblUsers'>" + dtTemp.Rows[0]["Nombre_Completo"].ToString() + "<label/>&nbsp;<img id='idIMGUser' src='../Images/guest.png' alt='imgUser' />");

            else
                return string.Concat(@"<img id='imgSalir' class='lblSalirSistemaIII'   src='../Images/salir.png' onclick='gfSalirSistema()' /> <label class='lblSalirSistema2' onclick='gfSalirSistema()'>Salir del Sistema</label> <p/> ",
                                      "<label id='lblUsers' class='lblUsers'>Usuario<label/>&nbsp;<img id='idIMGUser' src='../Images/guest.png' alt='imgUser' />");
            // return @"<label id='lblUser'>Usuario <label/>&nbsp;<img id='idIMGUser' src='/Images/guest.png' alt='imgUser' />";
        }

        #region Merge
        public static string gfCompaniaPathImg(int pIDCompania, string ruta)
        {
            string rutaRaiz = string.Empty;
            try
            {
                if (pIDCompania != 0)
                {
                    ruta = ruta.ToUpper().Replace(".JPG", ".PNG");
                    ruta = ruta.ToUpper().Replace("D:", "E:");
                    rutaRaiz = ruta;
                    ruta = ruta.ToUpper().Replace(@"E:\PROGRAM FILES\INTEGRA EMPRESARIAL\", "../INTEGRA EMPRESARIAL/");
                    ruta = ruta.Replace(@"\", "/");

                    if (System.IO.File.Exists(rutaRaiz))
                        return @"<a href='../MainMenuV2.html'> <img id='idIMG' src='" + ruta + "' alt='img' height='81' width='178' /></a>";
                    else
                        return @"<a href='../MainMenuV2.html'><img id='idIMG' src='../Images/logo.png' alt='img' height='81' width='178' /> </a>";
                }
                else
                {
                    return @"<a href='../MainMenuV2.html'><img id='idIMG' src='../Images/logo.png' alt='img' height='81' width='178' /> </a>";
                }
            }
            catch
            {
                return @"<img id='idIMG' src='../Images/logo.png' alt='img' height='81' width='178' />";
            }
        }
        #endregion

    }
}
