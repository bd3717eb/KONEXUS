using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using IntegraData;

namespace Integra_Develoment
{
    public class VentasController
    {


        public int iEmpresa { get; set; }
        public int iNumero { get; set; }
        public int iEstatus { get; set; }
        public DateTime Fecha { get; set; }
        public int iCliente { get; set; }
        public int iUsuario { get; set; }
        public int? iVendedor { get; set; }
        public int iResponsable { get; set; }
        public string sProyecto { get; set; }
        public int iMoneda { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public int? iContacto { get; set; }
        public string sObservaciones { get; set; }
        public int iNumeroPago { get; set; }
        public int iPeriodoPago { get; set; }
        public int? iDocumento { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public int iNumeroOrden { get; set; }
        public int cIva { get; set; }
        public int? iDocumentoRelacion { get; set; }
        public int iFacturaMoneda { get; set; }
        public int iNumeroConcepto { get; set; }
        public int? iNumeroReferencia { get; set; }
        public int iNumeroDomicilio { get; set; }
        public int iCredito { get; set; }
        public int iSucursal { get; set; }
        public int iEntregaDirecta { get; set; }
        public int? iCNumero { get; set; }
        public int? iCTienda { get; set; }




        public DataTable buscarVentas(int iEmpresa, int iSucursal, int iFamilia, int iPadre, int iEstatus, DateTime? fechaDe, DateTime? fechaHasta, int iVentaDe, int iVentaHasta, int iCliente)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@office", iSucursal));
            context.Parametros.Add(new SqlParameter("@family", iFamilia));
            context.Parametros.Add(new SqlParameter("@father", iPadre));
            context.Parametros.Add(new SqlParameter("@status", iEstatus));
            context.Parametros.Add(new SqlParameter("@datefrom", fechaDe.HasValue ? (object)fechaDe.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@dateto", fechaHasta.HasValue ? (object)fechaHasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@salefrom", iVentaDe));
            context.Parametros.Add(new SqlParameter("@saleto", iVentaHasta));
            context.Parametros.Add(new SqlParameter("@client", iCliente));

            dt = context.ExecuteProcedure("sp_Venta_BusquedaDeVentas_v2", true);

            if (dt != null)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }

        }


        public int obtenerNumeroDeEstatusDeDescripcion(string sDescripcion, int iFamilia, int iPadre)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@description", sDescripcion));
            context.Parametros.Add(new SqlParameter("@family", iFamilia));
            context.Parametros.Add(new SqlParameter("@father", iPadre));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneNumeroDeEstatusDeDescripcion_v1", true);

            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return -1;

        }


        public int Cotizacion(int iMovimiento, int iEmpresa, int iNumero, int iEstatus, DateTime Fecha, int iCliente, int iUsuario, int? iVendedor,
                             int iResponsable, string sProyecto, int iMoneda, DateTime FechaDeEntrega, int? iContacto, string sObservaciones,
                             int iNumeroPago, int iPeriodoPago, int? iDocumento, DateTime FechaExpiracion, int iNumeroOrden, int cIva,
                             int? iDocumentoRelacion, int iFacturaMoneda, int iNumeroConcepto, int? iNumeroReferencia, int iNumerDomicilio,
                             int iCredito, int iSucursal, int iEntregaDirecta, int? iCNumero, int? iCTienda)
        {
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@iType", iMovimiento));
            context.Parametros.Add(new SqlParameter("@iEmpresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("@iNumero", iNumero));
            context.Parametros.Add(new SqlParameter("@iEstatus", iEstatus));
            context.Parametros.Add(new SqlParameter("@dFecha", Fecha));
            context.Parametros.Add(new SqlParameter("@iCliente", iCliente));
            context.Parametros.Add(new SqlParameter("@iUsuario", iUsuario));
            context.Parametros.Add(new SqlParameter("@iVendedor", iVendedor.HasValue ? (object)iVendedor.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@iResponsable", iResponsable));
            context.Parametros.Add(new SqlParameter("@vNombre_Proyecto", sProyecto != "" ? (object)sProyecto : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@iMoneda", iMoneda));
            context.Parametros.Add(new SqlParameter("@dFecha_Entrega", FechaDeEntrega));
            context.Parametros.Add(new SqlParameter("@iContacto", iContacto.HasValue ? (object)iContacto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@vObservaciones", sObservaciones != "" ? (object)sObservaciones : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@iNumero_Pago", iNumeroPago));
            context.Parametros.Add(new SqlParameter("@iClas_Periodo_Pago", iPeriodoPago));
            context.Parametros.Add(new SqlParameter("@iTipoDocto", iDocumento.HasValue ? (object)iDocumento.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Vencimiento", FechaExpiracion));
            context.Parametros.Add(new SqlParameter("@iNumero_Orden", iNumeroOrden));
            context.Parametros.Add(new SqlParameter("@Clas_IVA", cIva));
            context.Parametros.Add(new SqlParameter("@Relacion_Tipo_Documento", iDocumentoRelacion.HasValue ? (object)iDocumentoRelacion.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Moneda_Factura", iFacturaMoneda));
            context.Parametros.Add(new SqlParameter("@Observaciones2", DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Num_Concepto", iNumeroConcepto));
            context.Parametros.Add(new SqlParameter("@Num_Referencia", iNumeroReferencia.HasValue ? (object)iNumeroReferencia.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Domicilio", iNumerDomicilio));
            context.Parametros.Add(new SqlParameter("@Credito", iCredito));
            context.Parametros.Add(new SqlParameter("@Clas_Sucursal", iSucursal));
            context.Parametros.Add(new SqlParameter("@Entrega_Directa", iEntregaDirecta));
            context.Parametros.Add(new SqlParameter("@Numero_Cotiza", iCNumero.HasValue ? (object)iCNumero.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_Almacen_Cot", iCTienda.HasValue ? (object)iCTienda.Value : DBNull.Value));


            if (context.Parametros != null)
            {
                foreach (SqlParameter param in context.Parametros)
                {
                    if (param.ParameterName == "iNumero" && iNumero == 0)
                    {
                        param.Direction = ParameterDirection.Output;
                    }
                }
            }


            return context.ExecuteNonQuery("sp_Cotizacion", true);


        }

        public virtual DataTable obtenerClaseAlamacen(int iEmpresa, int iNumero)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@number", iNumero));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneTipoDeAlmacen_v1", true);
            if (dt != null)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }

        public static DataTable GetProducts(int icompany, string sdescription, int imarketing, int itype)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("NumeroEmpresa", icompany));
            context.Parametros.Add(new SqlParameter("Descripcion", sdescription));
            context.Parametros.Add(new SqlParameter("Comercializacion", imarketing));
            context.Parametros.Add(new SqlParameter("TipoProducto", itype));

            dt = context.ExecuteProcedure("sp_Obtener_Productos2", true).Copy();
            dt.Rows.Cast<System.Data.DataRow>().Take(15);
            return dt;
        }
        public static DataTable GetConcepts(int icompany, int imarketingproduct, int iproduct, int icliente, int ito)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("NumeroEmpresa", icompany));
            context.Parametros.Add(new SqlParameter("Comercializacion", imarketingproduct));
            context.Parametros.Add(new SqlParameter("Producto", iproduct));
            context.Parametros.Add(new SqlParameter("Cliente", icliente));
            context.Parametros.Add(new SqlParameter("Hasta", ito));

            dt = context.ExecuteProcedure("sp_Obtener_Conceptos", true).Copy();
            return dt;

        }

        public static DataTable GetConceptosPorProducto(int icompany, int imarketingproduct, int iproduct, int icliente, int ito)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("NumeroEmpresa", icompany));
            context.Parametros.Add(new SqlParameter("Comercializacion", imarketingproduct));
            context.Parametros.Add(new SqlParameter("Producto", iproduct));
            context.Parametros.Add(new SqlParameter("Cliente", icliente));
            context.Parametros.Add(new SqlParameter("Hasta", ito));

            dt = context.ExecuteProcedure("sp_Obtener_ConceptosPorProducto", true).Copy();
            return dt;

        }


        public static DataTable ObtieneDisponiblesAlmacenProductos(int icompany, int iperson, int iproduct, int iconcept)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", icompany));
            context.Parametros.Add(new SqlParameter("usernumber", iperson));
            context.Parametros.Add(new SqlParameter("product", iproduct));
            context.Parametros.Add(new SqlParameter("concept", iconcept));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneStockDeProductosDeTienda_v1", true).Copy();
            return dt;

        }



        public DataTable ObtieneVentasFacturar(int iEmpresa, int iSucursal, int iMoneda, int iIva, int iCliente, string fechaDe, string fechaHasta, int iTipoDocumento, int? iSerie)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@numeroempresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("@sucursal", iSucursal));
            context.Parametros.Add(new SqlParameter("@moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("@iva", iIva));
            context.Parametros.Add(new SqlParameter("@numerocliente", iCliente));
            if (fechaDe != "")
            {
                context.Parametros.Add(new SqlParameter("@datefrom", fechaDe));
            }
            else
            {
                context.Parametros.Add(new SqlParameter("@datefrom", DBNull.Value));
            }
            if (fechaHasta != "")
            {
                context.Parametros.Add(new SqlParameter("@dateto", fechaHasta));
            }
            else
            {
                context.Parametros.Add(new SqlParameter("@dateto", DBNull.Value));
            }


            context.Parametros.Add(new SqlParameter("@documento", iTipoDocumento));
            context.Parametros.Add(new SqlParameter("@serie", iSerie.HasValue ? (object)iSerie.Value : DBNull.Value));


            dt = context.ExecuteProcedure("sp_BusquedaDeVentasFacturar", true).Copy();


            if (dt != null)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }

        }

        public DataTable sp_Cotizacion_Factura_Ticket(int iNumero_Empresa, int iNumero_Cliente, int iUsuario, DateTime Fecha, int iMoneda,
                             int iIVA, int iFolio_Factura, int iTipo_Docto, int iSerie_Docto, Decimal dTipo_Cambio, string sObservaciones,
                             int iClas_Pantalla, int iClas_Sucursal)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Cliente", iNumero_Cliente));
            context.Parametros.Add(new SqlParameter("@Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("@Fecha", Fecha));
            context.Parametros.Add(new SqlParameter("@Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("@Clas_IVA", iIVA));
            context.Parametros.Add(new SqlParameter("@Folio_Factura", iFolio_Factura));
            context.Parametros.Add(new SqlParameter("@Tipo_Docto", iTipo_Docto));
            context.Parametros.Add(new SqlParameter("@Serie_Docto", iSerie_Docto));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", dTipo_Cambio));
            context.Parametros.Add(new SqlParameter("@Observaciones", sObservaciones));
            context.Parametros.Add(new SqlParameter("@Clas_Pantalla", iClas_Pantalla));
            context.Parametros.Add(new SqlParameter("@Clas_Sucursal", iClas_Sucursal));

            dt = context.ExecuteProcedure("sp_Cotizacion_Factura_TicketV2", true);


            if (dt != null)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }


        }



        public static int gfInsertaFactura(int iEmpresa, string sCadena, int iPersona)
        {
            int iRespuesta = 0;
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
                context.Parametros.Add(new SqlParameter("@sCadena", sCadena));
                context.Parametros.Add(new SqlParameter("@Numero_Persona", iPersona));

                context.ExecuteProcedure("sp_Facturacion_InsertaFacturacion", true).Copy();
            }
            catch
            {
                iRespuesta = -1;
            }
            return iRespuesta;

        }


        public static DataTable realizaContabilidad(int iTipo, int iEmpresa, int iNumeroFactura)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@Tipo", iTipo));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero", iNumeroFactura));

            dt = context.ExecuteProcedure("sp_Factura_Contabiliza", true);

            if (dt != null)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }

        }

        ////////////////////////////////////////VENTAS PROGRAMADAS


        public static int gfSp_cotizacion_detalle_pagos(int iTipo, int iNumeroEmpresa, int iNumeroCotizacion, int iNumeroPartida,
                                                        int iNumeroPago, decimal dAnticipo, int iNumeroPeriodo, int iClasPeriodo,
                                                        DateTime Fecha)
        {
            int iRespuesta = 0;
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Tipo", iTipo));
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumeroEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Cotiza", iNumeroCotizacion));
                context.Parametros.Add(new SqlParameter("@Numero_Partida", iNumeroPartida));
                context.Parametros.Add(new SqlParameter("@Numero_Pago", iNumeroPago));
                context.Parametros.Add(new SqlParameter("@Anticipo", dAnticipo));
                context.Parametros.Add(new SqlParameter("@Num_Periodo", iNumeroPeriodo));
                context.Parametros.Add(new SqlParameter("@Clas_Periodo", iClasPeriodo));
                context.Parametros.Add(new SqlParameter("@Fecha", Fecha));


                context.ExecuteProcedure("sp_cotizacion_detalle_pagos", true).Copy();
            }
            catch
            {
                iRespuesta = -1;
            }


            return iRespuesta;

        }

        //nuevo
        public static DataTable GetProductoCodigoBarras(int iEmpresa, string sDescripcion)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Codigo", sDescripcion));


            dt = context.ExecuteProcedure("sp_Producto_Concepto_CB", true).Copy();
            return dt;
        }





    }
}