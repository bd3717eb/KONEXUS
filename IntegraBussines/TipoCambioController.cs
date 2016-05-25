using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntegraBussines
{
    public class TipoCambioController
    {
        public List<Events> gfConsultaTipoCambiosxMes(int iNumeroMes, int iAnio)
        {
            string sQuery = string.Empty;
            DataTable dt = new DataTable();
            List<Events> lista = new List<Events>();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            sQuery = string.Concat("USE [INTEGRA_KONEXUS_FAC] SELECT CATALOGODETALLEID AS ID , CATALOGODETALLEDESCRIPCION AS MES ,DESCRIPCION2  AS FIX,DATECREATION   AS FECHA FROM CATALOGODETALLE ",
                                   "WHERE CATALOGOID =18 ",
                                   "AND DATECREATION >= CONVERT(datetime, '01/", iNumeroMes, "/", iAnio, "')  ",
                                   "AND DATECREATION <= GETDATE() ",
                                   "AND CATALOGODETALLECLAVE = ", iNumeroMes, " ",
                                   "AND DESCRIPCION2 != '-1' ORDER BY DATECREATION  ASC  ");
            dt = context.ExecuteQuery(sQuery);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow itemDiaTipoCambio in dt.Rows)
                {
                    Events eo = new Events();
                    eo.id = itemDiaTipoCambio["ID"].ToString();
                    eo.title = itemDiaTipoCambio["FIX"].ToString();
                    eo.date = itemDiaTipoCambio["FECHA"].ToString();
                    eo.start = itemDiaTipoCambio["FECHA"].ToString();
                    eo.end = itemDiaTipoCambio["FECHA"].ToString();
                    lista.Add(eo);
                }
            }
            return lista;
        }

        public string gfConsultaTipoCambios(int iNumeroMes, int iAnio)
        {
            string sQuery = string.Empty;
            DataTable dt = new DataTable();
            List<Events> lista = new List<Events>();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            sQuery = string.Concat("USE [INTEGRA_KONEXUS_FAC] SELECT CATALOGODETALLEID AS ID , CATALOGODETALLEDESCRIPCION AS MES ,DESCRIPCION2  AS FIX , DATECREATION   AS FECHA , '' AS NOMBREDIA  FROM CATALOGODETALLE ",
                                   "WHERE CATALOGOID =18 ",
                                   "AND DATECREATION >= CONVERT(datetime, '01/", iNumeroMes, "/", iAnio, "')  ",
                                   "AND DATECREATION <= GETDATE() ",
                                   "AND CATALOGODETALLECLAVE = ", iNumeroMes, " ",
                                   "AND DESCRIPCION2 != '-1' ORDER BY DATECREATION  ASC  ");
            dt = context.ExecuteQuery(sQuery);

            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName == "NOMBREDIA")
                {
                    col.ReadOnly = false;
                    col.MaxLength = 1024;
                }
            }


            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("es-MX");

            foreach (DataRow item in dt.Rows)
            {
                DateTime dtTiempo = DateTime.Parse(item["FECHA"].ToString(), cultureinfo);
                string sNombreDia = String.Format("{0:dddd}", dtTiempo).ToUpper();
                item["NOMBREDIA"] = sNombreDia;
            }
            return gfCreaHTML(dt);
        }

        public string gfCreaHTML(DataTable dt)
        {
            string sHTML = string.Concat("<table><tr><th>LUNES</th><th>MARTES</th><th>MIERCOLES</th><th>JUEVES</th><th>VIERNES</th><th>SABADO</th><th>DOMINGO</th></tr>");

            string sBodyHtml = string.Empty;
            bool bandera = true;
            foreach (DataRow itemRow in dt.Rows)
            {
                if (bandera)
                {
                    switch (itemRow["NOMBREDIA"].ToString().Substring(0, 3))
                    {
                        case "LUN":
                            sBodyHtml = "<tr><tr><td>" + itemRow["FIX"].ToString() + "</td>";
                            break;
                        case "MAR":
                            sBodyHtml = "<tr><td></td><td>" + itemRow["FIX"].ToString() + "</td>";
                            break;
                        case "MIÉ":
                            sBodyHtml = "<tr><td></td><td></td><td>" + itemRow["FIX"].ToString() + "</td>";
                            break;
                        case "JUE":
                            sBodyHtml = "<tr><td></td><td></td><td></td><td>" + itemRow["FIX"].ToString() + "</td>";

                            break;
                        case "VIE":
                            sBodyHtml = "<tr><td></td><td></td><td></td><td></td><td>" + itemRow["FIX"].ToString() + "</td>";
                            break;
                        case "SÁB":
                            sBodyHtml = "<tr><td></td><td></td><td></td><td></td><td></td><td>" + itemRow["FIX"].ToString() + "</td>";
                            break;
                        case "DOM":
                            sBodyHtml = "<tr><td></td><td></td><td></td><td></td><td></td><td></td><td>" + itemRow["FIX"].ToString() + "</td>";
                            break;
                    }
                    bandera = false;
                    continue;
                }

                if (bandera == false)
                {

                    if (itemRow["NOMBREDIA"].ToString() == "DOMINGO")
                    {
                        sBodyHtml += "<td>" + itemRow["FIX"].ToString() + "</td></tr><tr>";
                        //sBodyHtml += "<td>" + itemRow["FIX"].ToString() + "</td></tr><tr>";
                    }

                    else
                        sBodyHtml += "<td>" + itemRow["FIX"].ToString() + "</td>";
                }
            }
            return string.Concat(sHTML, sBodyHtml, "</table>");

        }
    }
    public class Events
    {
        public string id { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string url { get; set; }

        public bool allDay { get; set; }
    }


}
