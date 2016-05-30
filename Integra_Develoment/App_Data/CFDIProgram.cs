using Ionic.Zip; //Install-Package DotNetZip
using System.IO;

namespace Integra_Develoment
{
    public class CFDIProgram
    {
        public static string gfComprimirFichero(string sArchivo)
        {
            string sResultado = string.Empty;
            if (Path.GetExtension(@sArchivo.ToUpper()) == ".XML")
            {

                ZipFile zip = new ZipFile();
                zip.AddFile(sArchivo, "");
                zip.AddFile(sArchivo.Replace(".xml", ".pdf"), "");
                sResultado = sArchivo.Replace(".xml", ".zip");
                zip.Save(@sResultado);
            }
            if (Path.GetExtension(@sArchivo.ToUpper()) == ".PDF")
            {

                ZipFile zip = new ZipFile();
                zip.AddFile(sArchivo, "");
                zip.AddFile(sArchivo.Replace(".pdf", ".xml"), "");
                sResultado = sArchivo.Replace(".pdf", ".zip");
                zip.Save(@sResultado);
            }
            return @sResultado;
            ////StreamWriter sw = new StreamWriter(@"E:\DocumentosElectronicos\myTracert2.txt");

            ////sw.WriteLine("entra comprimir fichero");
            //ZipFile zip = new ZipFile();
            ////sw.WriteLine(RutaFichero);

            //zip.AddFile(sArchivo);
            //zip.AddFile(sArchivo.ToUpper().Replace(".XML", ".PDF"));

            //zip.Save(RutaFichero);
            //INTEGRA.Global.Rutadoc = RutaFichero;
            //sw.WriteLine("agregó zip");
            //sw.Close();
        }
    }
}