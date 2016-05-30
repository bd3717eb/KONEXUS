using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Web;

namespace INTEGRAReports
{
    public class ImageHelper
    {
        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }

        public static Image ObtenerImagenNoDisponible()
        {
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "../INTEGRAReports/ImagesReports/0/logo.png", FileMode.Open);
       
            return Image.FromStream(file);
        }

        //public static Image GetCompanyLogo(string company)
        //{

        //    FileStream file;
        //    try
        //    {
        //        file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Images/ImagenesEmpresas/" + company + "/logo.png", FileMode.Open, FileAccess.Read, FileShare.Read);
        //    }
        //    catch
        //    {
        //        file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "../INTEGRAReports/ImagesReports/0/logo.png", FileMode.Open);
               
        //    }
        //    return Image.FromStream(file);
        //}

        public static Image GetCompanyLogo(string company)
        {
            FileStream file;
            string ruta = HttpContext.Current.Session["sImgEmpresa"].ToString();
            ruta = ruta.Replace("D:", "E:");
            try
            {
                //E:\Program Files\integra empresarial\logempresas
                if (System.IO.File.Exists(ruta))
                    @file = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.Read);
                else
                    @file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Images/ImagenesEmpresas/" + company + "/logo.png", FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch
            {
                @file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Images/ImagenesEmpresas/" + company + "/logo.png", FileMode.Open, FileAccess.Read, FileShare.Read);

            }
            return Image.FromStream(@file);

        }
    }
}
