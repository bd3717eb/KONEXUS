using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Integra_Develoment;
using System.Web;
using Ionic.Zip;
using System.Net.Mime;


namespace IntegraBussines
{
    public class SmtpMail
    {
        public string From { get; set; }        
        public string Message { get; set; }
        public string Subject { get; set; }
        
        private string host;
        private int port;
        private string username;
        private string password;
        private List<string> To = new List<string>();
        private List<string> CC = new List<string>();    

        public SmtpMail(string _host, int _port, string _username, string _password, string[] _recipients)
        {
            host = _host;
            port = _port;
            username = _username;
            password = _password;

            AsignMailRecipients(_recipients);
        }

        public SmtpMail(string _host, int _port, params string[] _recipients)
        {
            host = _host;
            port = _port;          

            AsignMailRecipients(_recipients);
        }
       
        public bool SecutiryHostSendAttachment(string Path, string[] FileName  )
        {
            MailMessage mail = new MailMessage();
            
            SmtpClient smtp = new SmtpClient(host, port);
            

            
                mail.From = new MailAddress(From);
                AddMailRecipients(mail);
                AddAttachmentFiles(mail, Path, FileName);
                mail.Priority = MailPriority.High;
                mail.IsBodyHtml = true;
                smtp.Credentials = new System.Net.NetworkCredential(username, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = false;// tiene que traerlo de la base
           
            
            try
            {
                smtp.Send(mail);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool SecutiryHostSendAttachment(string[] FilePath)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient(host, port);
            //***********
            mail.Subject = "Aviso Factura Electronica";
            //***********
           
            mail.From = new MailAddress(From);
            AddMailRecipients(mail);
            AddAttachmentFiles(mail, null, FilePath);
            mail.Priority = MailPriority.High;
            mail.IsBodyHtml = true;

            smtp.Credentials = new System.Net.NetworkCredential(username, password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = false; // tiene que traerlo de la base

            try
            {
                smtp.Send(mail);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool SecutiryHostSendAttachment(string Path, string sPersona, string sEmpresa, string sBase, string[] FileName = null, List<string> LRuta = null)
        {
            MailMessage mail = new MailMessage();

            SmtpClient smtp = new SmtpClient(host, port);

            //Si el arreglo viene caci, significa que enviara un zip, si viene lleno solo un archivo pdf
            if (FileName != null && FileName[0] != null)
            {
                mail.From = new MailAddress(From);
                AddMailRecipients(mail);
                AddAttachmentFiles(mail, Path, FileName);
                mail.Priority = MailPriority.High;
                mail.IsBodyHtml = true;
                smtp.Credentials = new System.Net.NetworkCredential(username, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = false;// tiene que traerlo de la base
            }
            //Valida si la lista viene con contenido
            else if (LRuta != null && LRuta.Count > 0)
            {
                //string sfolderName = @"C:\DOCUMENTOS\";
                string sfolderName = "\\\\VMS00001392-2\\DocumentosElectronicos\\Temp\\DocumentosVenta";
                string sfecha = Convert.ToString(DateTime.Now.Month + "-" + DateTime.Now.Year);
                string suser = sPersona;
                //crea directorio donde se guardaran los archivos zip
                Path = System.IO.Path.Combine(sfolderName, sBase, sEmpresa, sfecha, suser);
                //Creacion del archivo zip
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFiles(LRuta, false, "");
                    zip.Save("" + Path + "\\" + "DOCUMENTOS_DE_VENTA.zip");
                    zip.Dispose();

                }
                
                //al directorio de agrego el nombre del zip
                Path = Path + "\\" + "DOCUMENTOS_DE_VENTA.zip";
                mail.From = new MailAddress(From);
                AddMailRecipients(mail);
                //llamo al metodo donde se adjuntan los archivos, y mando el objeto mail junto con el directorio
                AddAttachmentFiles(mail, Path, null);
                mail.Priority = MailPriority.High;
                mail.IsBodyHtml = true;
                smtp.Credentials = new System.Net.NetworkCredential(username, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = false;// tiene que traerlo de la base

            }
            try
            {
                //Manda correo
                smtp.Send(mail);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void AddMailRecipients(MailMessage mail)
        {
            foreach (string recipients in To)
            {
                mail.To.Add(new MailAddress(recipients));
            }
        }

        private void AsignMailRecipients(string[] recipients)
        {
            int i;

            for (i = 0; i < recipients.Length; i++)
            {
                if (recipients[i] != null)
                    To.Add(recipients[i]);
                else
                    break;
            }
        }

        private void AddAttachmentFiles(MailMessage mail, string path = null, string[] attachments = null)
        {
            int i;
            //valido si el arrgelo viene vacio o lleno
            if (attachments != null)
            {
                for (i = 0; i < attachments.Length; i++)
                {
                    if (attachments[i] != null)
                    {

                        Attachment att = new Attachment(path + attachments[i]);
                        mail.Attachments.Add(att);
                    }
                    else
                        break;
                }
            }
            //Si viene vacio
            else if(attachments == null)
            {
                //Agrego la direcion del zip al objeto Data
                Attachment Data = new Attachment(path);
                //Agrego el objeto Data a los datos del mail
                mail.Attachments.Add(Data);
            }
        }
    }
}
