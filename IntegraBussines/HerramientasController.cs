using System.Data;
using IntegraData;
using System.Data.SqlClient;
using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Web.Configuration;

namespace IntegraBussines
{
    public class HerramientasController
    {
        string key = "mikey";

        /// <summary>
        /// Funcion Encripta
        /// </summary>
        /// <param name="texto">recibe texto a encriptar</param>
        /// <returns>regresa texto de encriptada</returns>
        public string gfEncriptar(string texto)
        {
            //mikey
            //arreglo de bytes donde guardaremos la llave
            byte[] keyArray;
            //arreglo de bytes donde guardaremos el texto
            //que vamos a encriptar
            byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);

            //se utilizan las clases de encriptación
            //provistas por el Framework
            //Algoritmo MD5
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice
            //hashing
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            //Algoritmo 3DAS
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform = tdes.CreateEncryptor();

            //arreglo de bytes donde se guarda la
            //cadena cifrada
            byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
            tdes.Clear();

            //se regresa el resultado en forma de una cadena
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }
        /// <summary>
        /// Funcion que desencripta
        /// </summary>
        /// <param name="textoEncriptado">recibe texto encriptado</param>
        /// <returns>regresa texto de encriptada</returns>
        public string gfDesencriptar(string textoEncriptado)
        {
            byte[] keyArray;
            //convierte el texto en una secuencia de bytes
            byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);
            //byte[] data = Convert.FromBase64String(encodedString);
            //string decodedString = Encoding.UTF8.GetString(Array_a_Descifrar);

            //se llama a las clases que tienen los algoritmos
            //de encriptación se le aplica hashing
            //algoritmo MD5
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);
            tdes.Clear();
            //se regresa en forma de cadena
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        /// <summary>
        /// Función que registra usuario bloqueado , email a enviar liga de desbloqueo y libera al usuario
        /// </summary>
        /// <param name="psLIGAENCRIPTADA">Link</param>
        /// <param name="psUSUARIOATRAPADO">Numero de usuario</param>
        /// <param name="psEMPRESA">Numero de empresa</param>
        /// <param name="psEMAIL">Email a enviar</param>
        /// <param name="piOPCION">1 registra, 2 elimina liga y desbloquea usuario</param>
        public static void gfRegistraDesbloqueaUsuario(string psLIGAENCRIPTADA, string psUSUARIOATRAPADO, string psEMPRESA, string psEMAIL, int piOPCION)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@LIGAENCRIPTADA ", psLIGAENCRIPTADA));
            context.Parametros.Add(new SqlParameter("@USUARIOATRAPADO", psUSUARIOATRAPADO));
            context.Parametros.Add(new SqlParameter("@EMPRESA", psEMPRESA));
            context.Parametros.Add(new SqlParameter("@EMAIL", psEMAIL));
            context.Parametros.Add(new SqlParameter("@OPCION", piOPCION));
            dt = context.ExecuteProcedure("[sp_LOG_DesbloqueaUsuarios]", true).Copy();
        }
        /// <summary>
        /// Envia correo de facturación con la liga de desbloqueo
        /// </summary>
        /// <param name="psLigaDesbloqueo"></param>
        /// <param name="plistaContactos"></param>
        /// <param name="piNumeroEmpresa"></param>
        public static void gfEmailSend(string psLigaDesbloqueo, List<string> plistaContactos, int piNumeroEmpresa)
        {
            string sMsgEmail = string.Concat("<p style='font-family:Tahoma;font-size:12px;color:#000'>----------------------------------------------------------------------------------------------------------------------------------------------------<br/><br/>Apreciable Cliente <br> En el presente correo encontraras una liga para desbloquear tu usuario quedamos a sus órdenes. <p/>",
                                             "<a href='", psLigaDesbloqueo, "'> Desbloquear usuario  </a>");
            try
            {
                var EmailloginInfo = new NetworkCredential(WebConfigurationManager.AppSettings["EmailAccount"],
                                                           WebConfigurationManager.AppSettings["EmailPassword"]);
                var Emailmsg = new System.Net.Mail.MailMessage();
                var EmailsmtpClient = new SmtpClient(WebConfigurationManager.AppSettings["EmailSMTP"],
                                                     int.Parse(WebConfigurationManager.AppSettings["EmailPORT"].ToString()));
                Emailmsg.From = new MailAddress(WebConfigurationManager.AppSettings["EmailAccount"]);

                foreach (string contacto in plistaContactos)
                    Emailmsg.To.Add(new MailAddress(contacto));

                Emailmsg.Subject = "Notificación de Desbloqueo de usuario";
                Emailmsg.IsBodyHtml = true;
                Emailmsg.Body = sMsgEmail;
                EmailsmtpClient.EnableSsl = false;
                EmailsmtpClient.UseDefaultCredentials = false;
                EmailsmtpClient.Credentials = EmailloginInfo;
                EmailsmtpClient.Send(Emailmsg);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        /// <summary>
        /// Manda email con link de desbloqueo , desbloquea usuario con la liga enviado al correo
        /// </summary>
        /// <param name="psInfoEncriptada"></param>
        /// <param name="iOpcion"></param>
        /// <returns></returns>
        public static bool gfDesbloqueUsuario(string psInfoEncriptada, int iOpcion)
        {
            string sTextoDescriptado = string.Empty;
            string sLigaDesbloqueo = string.Concat(WebConfigurationManager.AppSettings["LigaDesbloqueo"].ToString(), psInfoEncriptada);
            string[] sIDValor;
            try
            {
                if (iOpcion == 1)
                {
                    sTextoDescriptado = new HerramientasController().gfDesencriptar(psInfoEncriptada);
                    sIDValor = sTextoDescriptado.Split('|');
                    string sEmailTemp = sIDValor[0].Replace('+', '@');
                    sEmailTemp = sIDValor[0].Replace('-', '.');
                    List<string> listaEmail = new List<string>();
                    listaEmail.Add(sEmailTemp);
                    HerramientasController.gfEmailSend(sLigaDesbloqueo, listaEmail, int.Parse(sIDValor[1]));
                }
                else
                {
                    sTextoDescriptado = new HerramientasController().gfDesencriptar(psInfoEncriptada);
                    sIDValor = sTextoDescriptado.Split('|');
                    string sEmailTemp = sIDValor[0].Replace('+', '@');
                    sEmailTemp = sIDValor[0].Replace('-', '.');
                    HerramientasController.gfRegistraDesbloqueaUsuario(string.Concat(psInfoEncriptada), sIDValor[1], sIDValor[2], sEmailTemp, 2);
                }
                return true;
            }
            catch
            {
                gfDesbloqueUsuario(psInfoEncriptada, iOpcion);
                return false;
            }
        }

    }
}
