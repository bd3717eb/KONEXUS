using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using IntegraData;

namespace IntegraBussines
{
    public class ControlVentasVisor
    {

        public DataTable ObtieneDatosFiltros(int opcion, int numEmpresa, DateTime? fechaDe, DateTime? fechaHasta, int? plaza, int? sucursal, int? cliente, int? precio
                                 , int? documento, int? serie, int? DocumentoDel, int? DocumentoAl, int? PedidoDel, int? PedidoAl, int? FormPago
                                 , int? EstatusFac, int? EstatusVen, int? EntregadoMN, int? Usuario, int? Importe)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Opcion", opcion));
            context.Parametros.Add(new SqlParameter("@NumeroCompany", numEmpresa));
            context.Parametros.Add(new SqlParameter("@FechaDe", fechaDe.HasValue ? (object)fechaDe.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechaHasta", fechaHasta.HasValue ? (object)fechaHasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Plaza", plaza.HasValue ? (object)plaza.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Sucursal", sucursal.HasValue ? (object)sucursal.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Cliente", cliente.HasValue ? (object)cliente.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Precio", precio.HasValue ? (object)precio.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Documento", documento.HasValue ? (object)documento.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Serie", serie.HasValue ? (object)serie.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@DocumentoDel", DocumentoDel.HasValue ? (object)DocumentoDel.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@DocumentoAl", DocumentoAl.HasValue ? (object)DocumentoAl.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@PedidoDel", PedidoDel.HasValue ? (object)PedidoDel.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@PedidoAl", PedidoAl.HasValue ? (object)PedidoAl.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FormPago", FormPago.HasValue ? (object)FormPago.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@EstatusFac", EstatusFac.HasValue ? (object)EstatusFac.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@EstatusVen", EstatusVen.HasValue ? (object)EstatusVen.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@EntregadoMN", EntregadoMN.HasValue ? (object)EntregadoMN.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", Usuario.HasValue ? (object)Usuario.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Importe", Importe.HasValue ? (object)Importe.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_venObtieneDatosFiltrosVisorVentas]", true).Copy();
            return dt;

        }


        public DataTable GetDataVentas(int numEmpresa, int numPersona, DateTime? fechaDe, DateTime? fechaHasta, int? plaza, int? sucursal, int? cliente, int? precio
                                , int? documento, int? serie, int? DocumentoDel, int? DocumentoAl, int? PedidoDel, int? PedidoAl, int? FormPago
                                , int? EstatusFac, int? EstatusVen, int? EntregadoMN, int? Usuario, int? Importe)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@NumeroCompany", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Persona", numPersona));
            context.Parametros.Add(new SqlParameter("@FechaDe", fechaDe.HasValue ? (object)fechaDe.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechaHasta", fechaHasta.HasValue ? (object)fechaHasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Plaza", plaza.HasValue ? (object)plaza.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Sucursal", sucursal.HasValue ? (object)sucursal.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Cliente", cliente.HasValue ? (object)cliente.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Precio", precio.HasValue ? (object)precio.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Documento", documento.HasValue ? (object)documento.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Serie", serie.HasValue ? (object)serie.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@DocumentoDel", DocumentoDel.HasValue ? (object)DocumentoDel.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@DocumentoAl", DocumentoAl.HasValue ? (object)DocumentoAl.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@PedidoDel", PedidoDel.HasValue ? (object)PedidoDel.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@PedidoAl", PedidoAl.HasValue ? (object)PedidoAl.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FormPago", FormPago.HasValue ? (object)FormPago.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@EstatusFac", EstatusFac.HasValue ? (object)EstatusFac.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@EstatusVen", EstatusVen.HasValue ? (object)EstatusVen.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@EntregadoMN", EntregadoMN.HasValue ? (object)EntregadoMN.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", Usuario.HasValue ? (object)Usuario.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Importe", Importe.HasValue ? (object)Importe.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_venObtieDatosVisorVentas]", true).Copy();
            return dt;
        }


        public DataTable GetDataCalculosVenta(int numeroEmpresa, int numeroFactura, int Numero)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@NumeroCompany", numeroEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Factura", numeroFactura));
            context.Parametros.Add(new SqlParameter("@Numero", Numero));

            dt = context.ExecuteProcedure("[sp_venObtieneCalculosVenta]", true).Copy();
            return dt;
        }


        public DataTable GetIvaEmpresa(int numeroEmpresa, int Numero)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@NumeroCompany", numeroEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero", Numero));

            dt = context.ExecuteProcedure("[sp_venObtieneIvaEmpresa]", true).Copy();
            return dt;
        }


        public DataTable GetTotalMargenNuevo(int numEmpresa, int NumUsuario, int plaza, int sucursal, int cliente, int precio
                                 , int documento, int serie, int DocumentoDel, int DocumentoAl, int PedidoDel, int PedidoAl, DateTime? fechaDe, DateTime? fechaHasta,
                                  int? FormPago, int EstatusFac, int EstatusVen, int Moneda, decimal EntregadoMN, int Usuario)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", NumUsuario));
            context.Parametros.Add(new SqlParameter("@cboZona", plaza));
            context.Parametros.Add(new SqlParameter("@cboSucursal", sucursal));
            context.Parametros.Add(new SqlParameter("@Cliente", cliente));
            context.Parametros.Add(new SqlParameter("@cboLPrecio", precio));
            context.Parametros.Add(new SqlParameter("@cboDocto", documento));
            context.Parametros.Add(new SqlParameter("@cboSerie", serie));
            context.Parametros.Add(new SqlParameter("@cboNumDocto_Ini", DocumentoDel));
            context.Parametros.Add(new SqlParameter("@cboNumDocto_Fin", DocumentoAl));
            context.Parametros.Add(new SqlParameter("@cboPedido_Ini", PedidoDel));
            context.Parametros.Add(new SqlParameter("@cboPedido_Fin", PedidoAl));
            context.Parametros.Add(new SqlParameter("@dtpFecha_Ini", fechaDe.HasValue ? (object)fechaDe.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@dtpFecha_Fin", fechaHasta.HasValue ? (object)fechaHasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@cboFPago", FormPago.HasValue ? (object)FormPago.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@cboEstatus", EstatusFac));
            context.Parametros.Add(new SqlParameter("@cboEstatus_Cot", EstatusVen));
            context.Parametros.Add(new SqlParameter("@cboMoneda", Moneda));
            context.Parametros.Add(new SqlParameter("@txtImporte_MN", EntregadoMN));
            context.Parametros.Add(new SqlParameter("@txtUsuario", Usuario));


            dt = context.ExecuteProcedure("[sp_Obten_Calculo_Margen_VisorV2]", true).Copy();
            return dt;

        }

        public DataTable GetDataVentasVisorTransporte(int tipo, int numeroEmpresa, int? sucursal, int? cliente,
                                  int? documento, int? serie, int? numDoctoIni, int? numDoctoFin, DateTime? fechFacIni,
                                   DateTime? fechFacFin, int? usuario, DateTime? fechEntreIni, DateTime? fechEntreFin)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo", tipo));
            context.Parametros.Add(new SqlParameter("@NumeroEmpresa", numeroEmpresa));
            context.Parametros.Add(new SqlParameter("@Sucursal", sucursal.HasValue ? (object)sucursal.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Cliente", cliente.HasValue ? (object)cliente.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Documento", documento.HasValue ? (object)documento.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Serie", serie.HasValue ? (object)serie.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@NumDoctoIni", numDoctoIni.HasValue ? (object)numDoctoIni.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@NumDoctoFin", numDoctoFin.HasValue ? (object)numDoctoFin.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechFacIni", fechFacIni.HasValue ? (object)fechFacIni.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechFacFin", fechFacFin.HasValue ? (object)fechFacFin.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", usuario.HasValue ? (object)usuario.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechEntreIni", fechEntreIni.HasValue ? (object)fechEntreIni.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechEntreFin", fechEntreFin.HasValue ? (object)fechEntreFin.Value : DBNull.Value));

            dt = context.ExecuteProcedure("[sp_venObtieDatosVisorVentasTransporte]", true).Copy();
            return dt;
        }

        public DataTable GetDataVisorTransporte(int numEmpresa, int numUsuariaApp, int? sucursal, int? Cliente, int? Documento,
                                int? Serie, int? NumDoctoIni, int? NumDoctoFin, DateTime? fechFacIni, DateTime? fechFacFin, int? usuario,
                                string estatus, DateTime? fechEntregaIni, DateTime? fechEntregaFin)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@NumeroEmpresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@UsuarioSec", numUsuariaApp));
            context.Parametros.Add(new SqlParameter("@Sucursal", sucursal.HasValue ? (object)sucursal.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Cliente", Cliente.HasValue ? (object)Cliente.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Documento", Documento.HasValue ? (object)Documento.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Serie", Serie.HasValue ? (object)Serie.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@NumDoctoIni", NumDoctoIni.HasValue ? (object)NumDoctoIni.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@NumDoctoFin", NumDoctoFin.HasValue ? (object)NumDoctoFin.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechFacIni", fechFacIni.HasValue ? (object)fechFacIni.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechFacFin", fechFacFin.HasValue ? (object)fechFacFin.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", usuario.HasValue ? (object)usuario.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Estatus", estatus));
            context.Parametros.Add(new SqlParameter("@FechEntreIni", fechEntregaIni.HasValue ? (object)fechEntregaIni.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechEntreFin", fechEntregaFin.HasValue ? (object)fechEntregaFin.Value : DBNull.Value));

            dt = context.ExecuteProcedure("[sp_venObtieDatosVisorVentasTransporteGrid]", true).Copy();
            return dt;
        }

        public DataTable GetDetalleReporteVisorVenta(int Numero_Empresa, int Numero_Factura)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@NumeroEmpresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@NumeroFactura", Numero_Factura));

            dt = context.ExecuteProcedure("sp_venObtieneDetallesReportVisorVentas", true).Copy();


            return dt;
        }

        public DataTable GetDetalleEmpresaReport(int Numero_Empresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@NumeroEmpresa", Numero_Empresa));

            dt = context.ExecuteProcedure("sp_venObtieneDatosEmpresa", true).Copy();

            return dt;
        }

        public DataTable GetDetalleSucursalReport(int Numero_Empresa, int Numero_Usuario, int Tipo_Sucursal)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@NumeroEmpresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@NumeroUsuario", Numero_Usuario));
            context.Parametros.Add(new SqlParameter("@TipoSucursal", Tipo_Sucursal));

            dt = context.ExecuteProcedure("sp_venObtieneDatosSucursal", true).Copy();

            return dt;
        }

        //Metodo agregado para prueba de generacion de reporte 

        public DataTable GetEncabezadoReporteOperacion(int Numero_Empresa, int Usuario, int Sucursal)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", Usuario));
            context.Parametros.Add(new SqlParameter("@Numero_Sucursal", Sucursal));

            dt = context.ExecuteProcedure("sp_Caja_Obtiene_Encabezado_Empresa", true).Copy();
            return dt;
        }

        public DataTable GetDetalleReporteOperacion(int Numero_Empresa, int Sucursal, DateTime Fecha)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Sucursal", Sucursal));
            context.Parametros.Add(new SqlParameter("@Fecha", Fecha));

            dt = context.ExecuteProcedure("sp_Caja_Obtiene_Valores_Custodia", true).Copy();
            return dt;
        }

        public DataTable GetFormasPagoDiasPosteriores(string formPago, DateTime FechaRPT, int Sucursal, int numEmpresa)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@FormPago", formPago));
            context.Parametros.Add(new SqlParameter("@FechaRpt", FechaRPT));
            context.Parametros.Add(new SqlParameter("@Sucursal", Sucursal));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));

            dt = context.ExecuteProcedure("sp_Caja_Obtiene_ValoresAnteriores", true).Copy();
            return dt;
        }

        public DataTable ObtieneDatosDevolucion(int numEmpresa, DateTime fechaCan)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Fecha", fechaCan));

            dt = context.ExecuteProcedure("sp_ObtieneDevoluciones", true).Copy();
            return dt;
        }

    }

}
