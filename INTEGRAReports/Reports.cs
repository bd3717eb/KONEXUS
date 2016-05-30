using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web;

namespace INTEGRAReports
{
    public class Reports
    {
        public static ReportDocument GetInvoiceReport(string company, DataTable details, DataTable general)
        {
            Invoice Report = new Invoice();
            InvoiceReport XSD = new InvoiceReport();

            XSD.Tables[1].Merge(details, true, MissingSchemaAction.Ignore);
            XSD.Tables[2].Merge(general, true, MissingSchemaAction.Ignore);

            InvoiceReport.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XSD);

            return Report;
        }


        public static ReportDocument GetMultiplePaymentReport(string company, DataTable details, DataTable general)
        {
            MultiplePayments Report = new MultiplePayments();
            MultiplePayReport XSD = new MultiplePayReport();

            XSD.Tables[0].Merge(details, true, MissingSchemaAction.Ignore);
            XSD.Tables[1].Merge(general, true, MissingSchemaAction.Ignore);

            MultiplePayReport.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XSD);

            return Report;
        }

        public static ReportDocument GetSaleDayReport(string company, DataTable SaleDayTable)
        {
            SaleDay Report = new SaleDay();
            SaleDayReport XSD = new SaleDayReport();

            XSD.Tables[1].Merge(SaleDayTable, true, MissingSchemaAction.Ignore);

            SaleDayReport.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XSD);

            return Report;
        }


        public static ReportDocument GetCreditNoteReport(string company, DataTable CreditNoteTable, DataTable ClientTable)
        {
            CreditNote Report = new CreditNote();
            CreditNoteReport XSD = new CreditNoteReport();

            XSD.Tables[0].Merge(CreditNoteTable, true, MissingSchemaAction.Ignore);
            XSD.Tables[1].Merge(ClientTable, true, MissingSchemaAction.Ignore);


            CreditNoteReport.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XSD);

            return Report;
        }



        public static ReportDocument GetSaleDocumentReport(string company, DataTable details, DataTable general, DataTable salesdocuments)
        {
            SaleDocument Report = new SaleDocument();
            InvoiceReport XSD = new InvoiceReport();

            XSD.Tables[1].Merge(details, true, MissingSchemaAction.Ignore);
            XSD.Tables[2].Merge(general, true, MissingSchemaAction.Ignore);
            XSD.Tables[3].Merge(salesdocuments, true, MissingSchemaAction.Ignore);

            InvoiceReport.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XSD);

            return Report;
        }

        public static ReportDocument GetGastosReport(string company, DataTable details, DataTable general)
        {
            GastosRpt Report = new GastosRpt();
            ShoppingReport XSD = new ShoppingReport();


            XSD.Tables[1].Merge(details, true, MissingSchemaAction.Ignore);
            XSD.Tables[2].Merge(general, true, MissingSchemaAction.Ignore);

            ShoppingReport.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XSD);

            return Report;
        }


        public static bool ExportToDisk(ReportDocument Report, string Path, string FileName)
        {
            ExportOptions exportOpts = new ExportOptions();
            DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();

            exportOpts = Report.ExportOptions;
            exportOpts.ExportFormatType = ExportFormatType.PortableDocFormat;
            exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;

            diskOpts.DiskFileName = Path + FileName;
            exportOpts.DestinationOptions = diskOpts;

            try
            {
                Report.Export();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool ExportToDisk(ReportDocument Report, string FilePath)
        {
            ExportOptions exportOpts = new ExportOptions();
            DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();

            exportOpts = Report.ExportOptions;

            exportOpts.ExportFormatType = ExportFormatType.PortableDocFormat;
            exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;

            diskOpts.DiskFileName = FilePath;
            exportOpts.DestinationOptions = diskOpts;

            try
            {
                Report.Export();
                Report.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static string GetReportName(string docto, string number)
        {
            return (docto.ToUpper() + number.ToUpper() + ".pdf").Replace(" ", "_");
        }


        #region Facturacion
        public static ReportDocument gfReportePersonasFisicas(DataSet dspersonasFisicas)
        {
            ReporteRetenciones reporte = new ReporteRetenciones();
            reporte.Section2.ReportObjects["Picture2"].Width = 2200;
            reporte.Section2.ReportObjects["Picture2"].Height = 2100;
            reporte.SetDataSource(dspersonasFisicas);
            return reporte;
        }

        public static ReportDocument gfReportePersonasMorales(DataSet dspersonasMorales)
        {
            ReportPersonasMoraless reporte = new ReportPersonasMoraless();
            reporte.Section2.ReportObjects["Picture2"].Width = 2200;
            reporte.Section2.ReportObjects["Picture2"].Height = 2100;
            reporte.SetDataSource(dspersonasMorales);
            return reporte;
        }
        #endregion


        public static ReportDocument GetReportDevolutions(string company, DataTable devolutionsTable, DataTable ClientTable)
        {
            Devolutions Report = new Devolutions();
            DevolutionsReports XSD = new DevolutionsReports();

            XSD.Tables[0].Merge(devolutionsTable, true, MissingSchemaAction.Ignore);
            XSD.Tables[1].Merge(ClientTable, true, MissingSchemaAction.Ignore);


            DevolutionsReports.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XSD);

            return Report;
        }
        public static ReportDocument GetReportDevolutions(string company, DataTable Encabezado, DataTable ClientTable, DataTable ProductsTable)
        {
            Devolutions Report = new Devolutions();
            DevolutionsRpt XSD = new DevolutionsRpt();

            //XSD.Tables[0].Merge(Encabezado, true, MissingSchemaAction.Ignore);
            XSD.Tables[1].Merge(ClientTable, true, MissingSchemaAction.Ignore);
            XSD.Tables[2].Merge(Encabezado, true, MissingSchemaAction.Ignore);
            XSD.Tables[3].Merge(ProductsTable, true, MissingSchemaAction.Ignore);


            DevolutionsRpt.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XSD);

            return Report;
        }

        public static ReportDocument gfGeneraTicketCompra(DataTable dtEmpresa, DataTable dtProducto, DataTable dtTotales)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dtEmpresa);
            ds.Tables.Add(dtProducto);
            ds.Tables.Add(dtTotales);
            ticket reporte = new ticket();
            reporte.SetDataSource(ds);
            return reporte;
        }
        //public static ReportDocument gfGeneraPoliza(DataTable dt)
        //{
        //    Poliza2 reporte = new Poliza2();
        //    reporte.SetDataSource(dt);
        //    //reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, @"C:\Dropbox\Z\archivo2.pdf");
        //    //reporte.Close();
        //    //reporte.Dispose();
        //    return reporte;
        //}

        public static ReportDocument gfGeneraPoliza(DataTable dt, string company)
        {
            vContabilidad_Polizas XSD = new vContabilidad_Polizas();
            Poliza2 reporte = new Poliza2();

            XSD.Tables[1].Merge(dt, true, MissingSchemaAction.Ignore);

            vContabilidad_Polizas.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            reporte.SetDataSource(XSD);
            return reporte;
        }


        //public static ReportDocument gfReporteBalanzaComprobacion(DataTable dt, int idNivel)
        //{
        //    ReportDocument rdReporte = new ReportDocument();

        //    //Balanza_Comprobacion_Nivel_TresV2 reporte = new Balanza_Comprobacion_Nivel_TresV2();
        //    //reporte.SetDataSource(dt);
        //    //return reporte;
        //    string sReporte = string.Empty;
        //    switch (idNivel)
        //    {
        //        case 5:
        //            sReporte = "BALANZA COMPROBACION CUENTAS MAYOR.RPT";
        //            Balanza_Comprobacion_Cuentas_MayorV2 reporte0 = new Balanza_Comprobacion_Cuentas_MayorV2();
        //            reporte0.SetDataSource(dt);
        //            return reporte0;
        //            //// Para la empresa 2 de PRESTADORA
        //            //// falta llenar nombre de la empresa 
        //            //if (int.Parse(Session["Company"].ToString()) == 2 && strNomEmpresa.ToUpper() == "PRESTADORA")
        //            //    sReporte = "BALANZA COMPROBACION2.RPT";
        //            //else
        //            //sReporte = "BALANZA COMPROBACION CUENTAS MAYOR.RPT";
        //            break;
        //        case 6:
        //            sReporte = "BALANZA COMPROBACION NIVEL UNO.RPT";
        //            Balanza_Comprobacion_Nivel_UnoV2 reporte1 = new Balanza_Comprobacion_Nivel_UnoV2();
        //            reporte1.SetDataSource(dt);
        //            rdReporte = reporte1;
        //            break;
        //        case 7:
        //            sReporte = "BALANZA COMPROBACION NIVEL DOS.RPT";
        //            Balanza_Comprobacion_Nivel_DosV2 reporte2 = new Balanza_Comprobacion_Nivel_DosV2();
        //            reporte2.SetDataSource(dt);
        //            rdReporte = reporte2;
        //            break;

        //        case 8:
        //            sReporte = "BALANZA COMPROBACION NIVEL TRES.RPT";
        //            Balanza_Comprobacion_Nivel_TresV2 reporte3 = new Balanza_Comprobacion_Nivel_TresV2();
        //            reporte3.SetDataSource(dt);
        //            rdReporte = reporte3;
        //            break;
        //        case 9:
        //            sReporte = "BALANZA COMPROBACION NIVEL CUATRO.RPT";
        //            Balanza_Comprobacion_Nivel_CuatroV2 reporte4 = new Balanza_Comprobacion_Nivel_CuatroV2();
        //            reporte4.SetDataSource(dt);
        //            rdReporte = reporte4;
        //            break;
        //        default:
        //            break;
        //    }

        //    return rdReporte; ;
        //}

        public static ReportDocument gfReporteBalanzaComprobacion(DataTable dt, int idNivel, string company)
        {
            ReportDocument rdReporte = new ReportDocument();

            //Balanza_Comprobacion_Nivel_TresV2 reporte = new Balanza_Comprobacion_Nivel_TresV2();
            //reporte.SetDataSource(dt);
            //return reporte;
            string sReporte = string.Empty;
            vBalanceTMP XSD = new vBalanceTMP();

            switch (idNivel)
            {
                case 5:
                    sReporte = "BALANZA COMPROBACION CUENTAS MAYOR.RPT";
                    Balanza_Comprobacion_Cuentas_MayorV2 reporte0 = new Balanza_Comprobacion_Cuentas_MayorV2();

                    XSD.Tables[1].Merge(dt, true, MissingSchemaAction.Ignore);

                    vBalanceTMP.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
                    ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
                    XSD.Empresa.Rows.Add(ImgRow);

                    reporte0.SetDataSource(XSD);
                    return reporte0;
                    //// Para la empresa 2 de PRESTADORA
                    //// falta llenar nombre de la empresa 
                    //if (int.Parse(Session["Company"].ToString()) == 2 && strNomEmpresa.ToUpper() == "PRESTADORA")
                    //    sReporte = "BALANZA COMPROBACION2.RPT";
                    //else
                    //sReporte = "BALANZA COMPROBACION CUENTAS MAYOR.RPT";
                    break;
                case 6:
                    sReporte = "BALANZA COMPROBACION NIVEL UNO.RPT";
                    Balanza_Comprobacion_Nivel_Uno reporte1 = new Balanza_Comprobacion_Nivel_Uno();

                    XSD.Tables[1].Merge(dt, true, MissingSchemaAction.Ignore);
                    vBalanceTMP.EmpresaRow ImgRow1 = XSD.Empresa.NewEmpresaRow();
                    ImgRow1.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
                    XSD.Empresa.Rows.Add(ImgRow1);
                    
                    reporte1.SetDataSource(XSD);
                    rdReporte = reporte1;
                    break;
                case 7:
                    sReporte = "BALANZA COMPROBACION NIVEL DOS.RPT";
                    Balanza_Comprobacion_Nivel_Dos reporte2 = new Balanza_Comprobacion_Nivel_Dos();

                    XSD.Tables[1].Merge(dt, true, MissingSchemaAction.Ignore);
                    vBalanceTMP.EmpresaRow ImgRow2 = XSD.Empresa.NewEmpresaRow();
                    ImgRow2.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
                    XSD.Empresa.Rows.Add(ImgRow2);
                    

                    reporte2.SetDataSource(XSD);
                    rdReporte = reporte2;
                    break;

                case 8:
                    sReporte = "BALANZA COMPROBACION NIVEL TRES.RPT";
                    Balanza_Comprobacion_Nivel_Tres reporte3 = new Balanza_Comprobacion_Nivel_Tres();
                   
                    XSD.Tables[1].Merge(dt, true, MissingSchemaAction.Ignore);
                    vBalanceTMP.EmpresaRow ImgRow3 = XSD.Empresa.NewEmpresaRow();
                    ImgRow3.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
                    XSD.Empresa.Rows.Add(ImgRow3);

                    reporte3.SetDataSource(XSD);
                    rdReporte = reporte3;
                    break;
                case 9:
                    sReporte = "BALANZA COMPROBACION NIVEL CUATRO.RPT";
                    Balanza_Comprobacion_Nivel_CuatroV2 reporte4 = new Balanza_Comprobacion_Nivel_CuatroV2();
                    reporte4.SetDataSource(dt);
                    rdReporte = reporte4;
                    break;
                default:
                    break;
            }

            return rdReporte;
        }
        public static ReportDocument gfReporteEdoResultados(DataTable dt, string company)
        {
            vEstado_Resultados_MensualV2 XSD = new vEstado_Resultados_MensualV2();
            Estado_Resultado_MDV2 reporte = new Estado_Resultado_MDV2();

            XSD.Tables[0].Merge(dt, true, MissingSchemaAction.Ignore);

            vEstado_Resultados_MensualV2.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            reporte.SetDataSource(XSD);
            return reporte;

        }

        //public static ReportDocument gfReporteEdoResultados(DataTable dt)
        //{
        //    Estado_Resultado_MDV2 reporte = new Estado_Resultado_MDV2();
        //    reporte.SetDataSource(dt);
        //    return reporte;
        //}

        //public static ReportDocument gfReporteBalanceGral(bool bReporte, DataTable dt)
        //{
        //    if (bReporte)
        //    {
        //        BalanceGeneral_Cuentas_sinV2 reporte = new BalanceGeneral_Cuentas_sinV2();
        //        reporte.SetDataSource(dt);
        //        return reporte;
        //    }
        //    else
        //    {
        //        BalanceGeneral_sinV2 reporte2 = new BalanceGeneral_sinV2();
        //        reporte2.SetDataSource(dt);
        //        return reporte2;
        //    }
            
        //}
        public static ReportDocument gfReporteBalanceGral(bool bReporte, DataTable dt, string company)
        {
            if (bReporte)
            {
                BalanceGeneral_Cuentas_sinV4 reporte2 = new BalanceGeneral_Cuentas_sinV4();
                vBalanceTMPV2 XSD = new vBalanceTMPV2();

                XSD.Tables[0].Merge(dt, true, MissingSchemaAction.Ignore);

                vBalanceTMPV2.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
                ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
                XSD.Empresa.Rows.Add(ImgRow);

                reporte2.SetDataSource(XSD);
                return reporte2;
            }
            else
            {
                BalanceGeneral_sinV2 reporte2 = new BalanceGeneral_sinV2();
                vBalanceTMPV2 XSD = new vBalanceTMPV2();

                XSD.Tables[0].Merge(dt, true, MissingSchemaAction.Ignore);

                vBalanceTMPV2.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
                ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
                XSD.Empresa.Rows.Add(ImgRow);

                reporte2.SetDataSource(XSD);
                return reporte2;
            }

        }


        public static ReportDocument GetVentasVisorReport(string company,DataTable detalleEmpresa, DataTable detalleSucursal, DataTable general)
        {
            Cotizacion_Visor_VentaV2 Report = new Cotizacion_Visor_VentaV2();
            VCotizacion_Visor_Venta XCV = new VCotizacion_Visor_Venta();

            XCV.Tables[1].Merge(detalleEmpresa, true, MissingSchemaAction.Ignore);
            XCV.Tables[2].Merge(detalleSucursal, true, MissingSchemaAction.Ignore);
            XCV.Tables[3].Merge(general, true, MissingSchemaAction.Ignore);

            VCotizacion_Visor_Venta.EmpresaRow ImgRow = XCV.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XCV.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XCV);

            return Report;
        }

        //Agregado para pruebas de reportes operaciones 

        public static ReportDocument GetVentasOperacionesReport(string company,DataTable datosEncabezado, DataTable dtOperaciones, DataTable dtOperacionesAnterior, DataTable dtTotalOperaciones)
        {
            OperacionesValoresCustodia Report = new OperacionesValoresCustodia();
            VOperacionesValoresCustodia XOP = new VOperacionesValoresCustodia();

            XOP.Tables[0].Merge(datosEncabezado, true, MissingSchemaAction.Ignore);
            XOP.Tables[1].Merge(dtOperaciones, true, MissingSchemaAction.Ignore);
            XOP.Tables[2].Merge(dtOperacionesAnterior, true, MissingSchemaAction.Ignore);
            XOP.Tables[3].Merge(dtTotalOperaciones, true, MissingSchemaAction.Ignore);


            VOperacionesValoresCustodia.EmpresaRow ImgRow = XOP.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XOP.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XOP);
            return Report;
        }

        public static ReportDocument ObtieneVentaDiaria(string company, DataTable SaleDayTable)
        {
            DocumentosDeVentaDia cocVen = new DocumentosDeVentaDia();
            SaleDayReport XSD = new SaleDayReport();

            XSD.Tables[1].Merge(SaleDayTable, true, MissingSchemaAction.Ignore);

            SaleDayReport.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            cocVen.SetDataSource(XSD);

            return cocVen;
        }

        //Agregado para pruebas de reportes operaciones ->






        /// <summary>
        /// Llama y llena el reporte del ticket de impresión de una venta
        /// </summary>
        /// <param name="dtEmpresa">Inforamación de la Empresa </param>
        /// <param name="dtProducto">Detalle de los productoa a la venta</param>
        /// <returns></returns>
        public static ReportDocument gfGeneraTicketCompra(DataTable dtEmpresa, DataTable dtProducto)
        {
            dsTicket ds = new dsTicket();
            ds.Tables["dtEmpresa"].Merge(dtEmpresa.Copy(), true, MissingSchemaAction.Ignore);
            ds.Tables["dtProducto"].Merge(dtProducto.Copy(), true, MissingSchemaAction.Ignore);
            ticket reporte = new ticket();
            reporte.SetDataSource(ds);
            return reporte;
        }

        public static string gfGetReportName(string docto, string number)
        {
            string sName = (docto.ToUpper() + "_" + number.ToUpper()).Replace(" ", "_");
            sName = sName.Replace(".pdf", "");
            sName = sName.Replace("pdf", "");
            sName = sName.Replace("/", "_");

            return sName;
        }

        public static ReportDocument GetPolizasImportadasDiarias(string company, DataTable detalleEmpresa, DataTable general)
        {
            PolizasImportadas Report = new PolizasImportadas();
            PolizasImportadasDia XCV = new PolizasImportadasDia();

            XCV.Tables[0].Merge(detalleEmpresa, true, MissingSchemaAction.Ignore);
            XCV.Tables[1].Merge(general, true, MissingSchemaAction.Ignore);

            Report.SetDataSource(XCV);

            return Report;
        }
        public static ReportDocument GetVentasOperacionesReport(DataTable datosEncabezado, DataTable dtOperaciones, DataTable dtOperacionesAnterior, DataTable dtTotalOperaciones)
        {
            OperacionesValoresCustodia Report = new OperacionesValoresCustodia();
            VOperacionesValoresCustodia XOP = new VOperacionesValoresCustodia();

            XOP.Tables[0].Merge(datosEncabezado, true, MissingSchemaAction.Ignore);
            XOP.Tables[1].Merge(dtOperaciones, true, MissingSchemaAction.Ignore);
            XOP.Tables[2].Merge(dtOperacionesAnterior, true, MissingSchemaAction.Ignore);
            XOP.Tables[3].Merge(dtTotalOperaciones, true, MissingSchemaAction.Ignore);

            Report.SetDataSource(XOP);
            return Report;
        }

        public static ReportDocument GetProveedor(DataTable dtProveedor)
        {
            Proveedores Report = new Proveedores();
            VProveedor XCV = new VProveedor();

            XCV.Tables[0].Merge(dtProveedor, true, MissingSchemaAction.Ignore);

            Report.SetDataSource(XCV);
            return Report;
        }
        public static ReportDocument GetReportSolicitudRembolso(string empresa, DataTable dtRembolso)
        {
            crSolicitudReembolsoVS XSD = new crSolicitudReembolsoVS();
            crSolicitudReembolsoV2 rpt = new crSolicitudReembolsoV2();

            XSD.Tables[1].Merge(dtRembolso, true, MissingSchemaAction.Ignore);

            crSolicitudReembolsoVS.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(empresa));
            XSD.Empresa.Rows.Add(ImgRow);

            rpt.SetDataSource(XSD);
            return rpt;
        }
        public static ReportDocument GetReportAlmacenKardex(string empresa, DataTable dtKardex)
        {
            ConsultaKardex rpt = new ConsultaKardex();
            ConsultaKardexVS XSD = new ConsultaKardexVS();

            XSD.Tables[1].Merge(dtKardex, true, MissingSchemaAction.Ignore);

            ConsultaKardexVS.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(empresa));
            XSD.Empresa.Rows.Add(ImgRow);

            rpt.SetDataSource(XSD);
            return rpt;
        }


        public static ReportDocument GetReportCuentasCobrar(string numEmpresa, DataTable dtEncabezado, DataTable dtDetalle)
        {
            VCuentasCobrar XSD = new VCuentasCobrar();
            CuentasCobrar rpt = new CuentasCobrar();

            XSD.Tables[1].Merge(dtEncabezado, true, MissingSchemaAction.Ignore);
            XSD.Tables[2].Merge(dtDetalle, true, MissingSchemaAction.Ignore);

            VCuentasCobrar.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(numEmpresa));
            XSD.Empresa.Rows.Add(ImgRow);

            rpt.SetDataSource(XSD);
            return rpt;
        }

        public static ReportDocument GetReportCuentasCobrarDetallado(string numEmpresa, DataTable dtEncabezado, DataTable dtDetalle)
        {
            VCuentasCobrar XSD = new VCuentasCobrar();
            CuentasCobrarDetalle rpt = new CuentasCobrarDetalle();

            XSD.Tables[1].Merge(dtEncabezado, true, MissingSchemaAction.Ignore);
            XSD.Tables[2].Merge(dtDetalle, true, MissingSchemaAction.Ignore);

            VCuentasCobrar.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(numEmpresa));
            XSD.Empresa.Rows.Add(ImgRow);

            rpt.SetDataSource(XSD);
            return rpt;
        }


        public static ReportDocument GetReportCuentasPagarDetallado(string company, DataTable details, DataTable general)
        {
            ReporteCXP Report = new ReporteCXP();
            dsCuentasPagar XSD = new dsCuentasPagar();

            XSD.Tables[1].Merge(details, true, MissingSchemaAction.Ignore);
            XSD.Tables[2].Merge(general, true, MissingSchemaAction.Ignore);

            dsCuentasPagar.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XSD);

            return Report;
        }



        public static ReportDocument GetReportCuentasPagar(string company, DataTable details, DataTable general)
        {
            ReporteCXP1 Report = new ReporteCXP1();
            dsCuentasPagar XSD = new dsCuentasPagar();

            XSD.Tables[1].Merge(details, true, MissingSchemaAction.Ignore);
            XSD.Tables[2].Merge(general, true, MissingSchemaAction.Ignore);

            dsCuentasPagar.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XSD);

            return Report;
        }

        public static ReportDocument GetReportCotizaciones(string company, DataTable dtCotizaciones)
        {
            CotizacionesReport Report = new CotizacionesReport();
            cotizacionesDataSet XSD = new cotizacionesDataSet();

            XSD.Tables[1].Merge(dtCotizaciones, true, MissingSchemaAction.Ignore);

            cotizacionesDataSet.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XSD);

            return Report;
        }


        //public static ReportDocument GetReporteAuxiliaresContables(string company, DataTable dtDatosReporte)
        //{
        //    crAuxiliarConta Report = new crAuxiliarConta();
        //    dsAuxiliarContable XSD = new dsAuxiliarContable();

        //    XSD.Tables[1].Merge(dtDatosReporte, true, MissingSchemaAction.Ignore);

        //    dsAuxiliarContable.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
        //    ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
        //    XSD.Empresa.Rows.Add(ImgRow);

        //    Report.SetDataSource(XSD);

        //    return Report;
        //}

        public static ReportDocument GetReporteAuxiliaresContables(string company, DataTable dtDatosReporte)
        {
            crAuxiliarConta Report = new crAuxiliarConta();
            dsAuxiliarContable XSD = new dsAuxiliarContable();

            XSD.Tables[1].Merge(dtDatosReporte, true, MissingSchemaAction.Ignore);

            dsAuxiliarContable.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            Report.SetDataSource(XSD);

            return Report;
        }


        public static ReportDocument ObtieneReporteEntradaAlmacen(string company, DataTable dtEntradasAlmacen)
        {
            crEntradasAlmacen crEntradasAlmacen = new crEntradasAlmacen();
            dsEntradasAlmacen XSD = new dsEntradasAlmacen();

            XSD.Tables[1].Merge(dtEntradasAlmacen, true, MissingSchemaAction.Ignore);

            dsEntradasAlmacen.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            crEntradasAlmacen.SetDataSource(XSD);

            return crEntradasAlmacen;
        }

        public static ReportDocument ObtieneReporteSalidaAlmacen(string company, DataTable dtSalidasAlmacen)
        {
            crSalidasAlmacen crSalidasAlmacen = new crSalidasAlmacen();
            dsSalidasAlmacen XSD = new dsSalidasAlmacen();

            XSD.Tables[1].Merge(dtSalidasAlmacen, true, MissingSchemaAction.Ignore);

            dsSalidasAlmacen.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            crSalidasAlmacen.SetDataSource(XSD);

            return crSalidasAlmacen;
        }

        public static ReportDocument ObtieneReporteExistenciasAlmacen(string company, DataTable dtExistenciaAlmacen)
        {
            crExistenciaAlmacen crExistenciaAlmacen = new crExistenciaAlmacen();
            dsExistenciaAlmacen XSD = new dsExistenciaAlmacen();

            XSD.Tables[1].Merge(dtExistenciaAlmacen, true, MissingSchemaAction.Ignore);

            dsExistenciaAlmacen.EmpresaRow ImgRow = XSD.Empresa.NewEmpresaRow();
            ImgRow.Logo = ImageHelper.ImageToByteArray(ImageHelper.GetCompanyLogo(company));
            XSD.Empresa.Rows.Add(ImgRow);

            crExistenciaAlmacen.SetDataSource(XSD);

            return crExistenciaAlmacen;
        }








    }
}
