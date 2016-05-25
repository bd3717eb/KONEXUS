using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;



namespace IntegraBussines
{
    public class CfdiBLL
    {
        public DataTable GetDatosEmisor(int company)
        {
            IntegraData.CfdiDAL DataEmisor = new IntegraData.CfdiDAL();
            return DataEmisor.GetDatosEmisor(company);
        }
        public DataTable GetDatosReceptor(int numeroregistro, int company, int TipoDoc)
        {
            IntegraData.CfdiDAL DataReceptor = new IntegraData.CfdiDAL();
            return DataReceptor.GetDatosReceptor(numeroregistro, company, TipoDoc);
        }

        public DataTable GetDatosArchivo(int NumeroRegistro, int Tipo, int company)
        {
            IntegraData.CfdiDAL DataArchivo = new IntegraData.CfdiDAL();
            return DataArchivo.GetDatosArchivo(NumeroRegistro, Tipo, company);
        }

        public DataTable GetPaisEmisor(string spais_emisor, int icompany, int ifamily)
        {
            IntegraData.CfdiDAL DataPais = new IntegraData.CfdiDAL();
            return DataPais.GetPaisEmisor(spais_emisor, icompany, ifamily);
        }

        public DataTable GetDatosEncabezado(int iNumeroRegistro, int iTipo, int icompany, int icontabilizada, int idocumenttype, int iinternetdocument)
        {
            IntegraData.CfdiDAL DataEncabezado = new IntegraData.CfdiDAL();
            return DataEncabezado.GetDatosEncabezado(iNumeroRegistro, iTipo, icompany, icontabilizada, idocumenttype, iinternetdocument);
        }

        public DataTable GetDatosSucursal(int Sucursal, int company)
        {
            IntegraData.CfdiDAL DataSucursal = new IntegraData.CfdiDAL();
            return DataSucursal.GetDatosSucursal(Sucursal, company);
        }
        public DataTable GetDatosSucursalCFD(int numero, int company, int TipoDoc)
        {
            IntegraData.CfdiDAL DataSucursal = new IntegraData.CfdiDAL();
            return DataSucursal.GetDatosSucursalCFD(numero, company, TipoDoc);
        }

        public DataTable GetPartidasTDS(int iNumeroRegistro, int iTipo, int icompany, int icontabilizada, int idocumenttype, int idocumentinternet)
        {
            IntegraData.CfdiDAL DataTDS = new IntegraData.CfdiDAL();
            return DataTDS.GetPartidasTDS(iNumeroRegistro, iTipo, icompany, icontabilizada, idocumenttype, idocumentinternet);
        }

        public DataTable GetPartidasCpae(int iNumeroRegistro, int iTipo, int icompany, int icontabilizada, int idocumenttype, int idocumentinternet)
        {
            IntegraData.CfdiDAL DataCpae = new IntegraData.CfdiDAL();
            return DataCpae.GetPartidasCpae(iNumeroRegistro, iTipo, icompany, icontabilizada, idocumenttype, idocumentinternet);
        }

        public DataTable GetPartidasCarton(int iNumeroRegistro, int iTipo, int icompany, int icontabilizada, int idocumenttype, int idocumentinternet)
        {
            IntegraData.CfdiDAL DataCarton = new IntegraData.CfdiDAL();
            return DataCarton.GetPartidasCarton(iNumeroRegistro, iTipo, icompany, icontabilizada, idocumenttype, idocumentinternet);
        }

        public DataTable GetPartidasCirrus(int NumeroRegistro, int Tipo, int company, int icontabilizada, int idocumenttype, int idocumentinternet)
        {
            IntegraData.CfdiDAL DataCirrus = new IntegraData.CfdiDAL();
            return DataCirrus.GetPartidasCirrus(NumeroRegistro, Tipo, company, icontabilizada, idocumenttype, idocumentinternet);
        }

        public DataTable GetPartidasGeneral(int NumeroRegistro, int Tipo, int company, int icontabilizada, int idocumenttype, int idocumentinternet)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetPartidasGeneral(NumeroRegistro, Tipo, company, icontabilizada, idocumenttype, idocumentinternet);
        }

        public DataTable GetAdenda(int NumeroRegistro, int company)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetAdenda(NumeroRegistro, company);
        }

        public DataTable GetNumberQuote(int NumeroRegistro, int company)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetNumberQuote(NumeroRegistro, company);
        }


        public DataTable GetRerefence(int NumeroRegistro, int company)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetReference(NumeroRegistro, company);
        }


        public DataTable GetCotizacionPartidaAdenda(int NumeroRegistro, int company, int partida)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetCotizacionPartidaAdenda(NumeroRegistro, company, partida);
        }

        public DataTable GetCompanyShortName(int company)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetCompanyShortName(company);
        }


        public DataTable GetCustomReport(int company)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetCustomReport(company);
        }


        public DataTable GetDataSerieInvoice(int NumeroRegistro, int company, int TipoDoc)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetDataSerieInvoice(NumeroRegistro, company, TipoDoc);
        }

        public DataTable GetConfigurationFiles(int NumeroRegistro, int company, int TipoDoc)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetConfigurationFiles(NumeroRegistro, company, TipoDoc);
        }

        public DataTable GetDatosDocto(int NumeroRegistro, int company, int TipoDoc)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetDatosDocto(NumeroRegistro, company, TipoDoc);
        }
        public DataTable GetDataInvoice(int NumeroRegistro, int company, int TipoDoc)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetDataInvoice(NumeroRegistro, company, TipoDoc);
        }

        public DataTable GetDateInvoice(int NumeroRegistro, int company, int TipoDoc)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetDateInvoice(NumeroRegistro, company, TipoDoc);
        }
        public DataTable GetDataAhorro(int NumeroRegistro, int company)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetDataAhorro(NumeroRegistro, company);
        }


        public DataTable GetNamePerson(int elaboro)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetNamePerson(elaboro);
        }
        public DataTable GetNameCountry(string spais, int icompany, int ifamily)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetNameCountry(spais, icompany, ifamily);
        }

        public DataTable GetAduanasPedimentos(int NumeroRegistro, int company)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetAduanasPedimentos(NumeroRegistro, company);
        }

        public DataTable GetObservaciones(int NumeroRegistro, int company)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetObservaciones(NumeroRegistro, company);
        }

        public DataTable GetRelaciones(int NumeroRegistro)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetRelaciones(NumeroRegistro);
        }

        public DataTable GetPartidaTipo(int NumeroRegistro, int company, int TipoDoc)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetPartidaTipo(NumeroRegistro, company, TipoDoc);
        }

        public DataTable GetDetalleFactura(int NumeroRegistro, int company, string pantalla)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetDetalleFactura(NumeroRegistro, company, pantalla);
        }


        public DataTable GetDetalleNotaCredito(int NumeroRegistro, int company, int iTipoDatos)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetDetalleNotaCredito(NumeroRegistro, company, iTipoDatos);
        }


        public DataTable GetDetalleNotaCreditoIntegra(int NumeroRegistro, int company, int iTipoDatos)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetDetalleNotaCreditoIntegra(NumeroRegistro, company, iTipoDatos);
        }

        public DataTable GetDetalleNotaCreditoDev(int NumeroRegistro, int company)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetDetalleNotaCreditoDev(NumeroRegistro, company);
        }
        public DataTable GetFolioCampo(int NumeroRegistro, int company, int nivel)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetFolioCampo(NumeroRegistro, company, nivel);
        }

        public DataTable GetCondicionesPago(int NumeroRegistro, int company)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetCondicionesPago(NumeroRegistro, company);
        }

        public DataTable GetCondicionPagoCredito(int company, int cliente, int moneda)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetCondicionPagoCredito(company, cliente, moneda);
        }
        public DataTable GetDatosPago(int NumeroRegistro, int company)
        {
            IntegraData.CfdiDAL DataGeneral = new IntegraData.CfdiDAL();
            return DataGeneral.GetDatosPago(NumeroRegistro, company);
        }


        public DataTable GetDatosMoneda(int company, int NumeroRegistro)
        {
            IntegraData.CfdiDAL DataMoneda = new IntegraData.CfdiDAL();
            return DataMoneda.GetDatosMoneda(company, NumeroRegistro);
        }
    }
}
