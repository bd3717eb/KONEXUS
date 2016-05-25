using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IntegraData;



namespace IntegraBussines
{
    public class AlmacenBLL
    {
        public static DataTable ObtieneTransaccionAutomaticaAlmacen(int iEmpresa)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", iEmpresa));
            dt = context.ExecuteProcedure("[sp_Almacen_Transaccion_Automatica]", true).Copy();

            return dt;
        }
        public static DataTable ObtieneConceptosEntradaSalidaAlmacen(int iEmpresa, int iTransaccionAlmacen)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("@transaccionAutomatica", iTransaccionAlmacen));
            dt = context.ExecuteProcedure("[sp_Almacen_ObtieneConceptosEntradaSalida]", true).Copy();

            return dt;
        }

        //salidas almacén
        public static DataTable ObtieneConceptosSalidaAlmacen(int iEmpresa, int iTransaccionAlmacen)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("@transaccionAutomatica", iTransaccionAlmacen));
            dt = context.ExecuteProcedure("[sp_Almacen_ObtieneConceptosSalida]", true).Copy();

            return dt;
        }

        public static DataTable ObtieneValidaUsuarioAlmacen(int intEmpresa, int intAlmacen, int intUsuario)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@numeroAlmacen", intAlmacen));
            context.Parametros.Add(new SqlParameter("@numeroUsuario", intUsuario));

            dt = context.ExecuteProcedure("[sp_Almacen_ObtieneAlmacenUsuario]", true).Copy();

            return dt;
        }

        public static DataTable ObtieneValidacionAlmacen(int intEmpresa, int intAlmacen)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numero_empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@numero_almacen", intAlmacen));


            dt = context.ExecuteProcedure("[sp_Almacen_ValidaAlmacen]", true).Copy();

            return dt;
        }

        public static DataTable VerificaPeriodoCosteado(int intEmpresa, int intAlmacen, DateTime dFecha)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@numeroAlmacen", intAlmacen));
            context.Parametros.Add(new SqlParameter("@fecha", dFecha));

            dt = context.ExecuteProcedure("[sp_Almacen_VerificaMetodoCosteoRangos]", true).Copy();

            return dt;
        }


        public static DataTable ObtieneValidaPermisoUsuarioAlmacen(int intEmpresa, int intAlmacen, int intUsuario)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@numeroAlmacen", intAlmacen));
            context.Parametros.Add(new SqlParameter("@numero", intUsuario));

            dt = context.ExecuteProcedure("[sp_Almacen_VerificaPermisoAlmacen]", true).Copy();

            return dt;
        }

        public static DataTable ValidaContabilidadEntradaAlmacen(int intEmpresa, int intAlmacen, int intUsuario)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@numeroAlmacen", intAlmacen));
            context.Parametros.Add(new SqlParameter("@numero", intUsuario));

            dt = context.ExecuteProcedure("[sp_Almacen_VerificaContabilidadEntradaAlmacen]", true).Copy();

            return dt;
        }


        public static DataTable ObtieneAlmacenes(int iEmpresa)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", iEmpresa));
            dt = context.ExecuteProcedure("[sp_Almacen_AlmacenRequerido]", true).Copy();

            return dt;
        }

        public static DataTable ObtieneOrdenesCompra(int intEmpresa, int intConcepto, int intUsuario, int? intCliente,
                                                     int? intAlmacen, DateTime? FechaDe, DateTime? FechaHasta, int? intNumerode, int? intNumeroHasta,int intTipoMovimiento)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
         
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Concepto", intConcepto));
            context.Parametros.Add(new SqlParameter("@Persona", intUsuario));
            context.Parametros.Add(new SqlParameter("@Cliente", intCliente.HasValue ? (object)intCliente.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechaDe", FechaDe.HasValue ? (object)FechaDe.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechaHasta", FechaHasta.HasValue ? (object)FechaHasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@NumeroDe", intNumerode.HasValue ? (object)intNumerode.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@NumeroHasta", intNumeroHasta.HasValue ? (object)intNumeroHasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@TipoMovimiento", intTipoMovimiento));

            dt = context.ExecuteProcedure("[sp_Almacenes_OrdenesV2]", true).Copy();
            
            return dt;
        }
        public static DataTable ObtieneOrdenesSalidaCompra(int intEmpresa, int intConcepto, int intUsuario, int? intCliente, int? intFactura, int? intAuxfactura,
                                                  int? intAlmacen, DateTime? FechaDe, DateTime? FechaHasta, int? intNumerode, int? intNumeroHasta, int? intAuxiliar)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Concepto", intConcepto));
            context.Parametros.Add(new SqlParameter("@Persona", intUsuario));
            context.Parametros.Add(new SqlParameter("@Cliente", intCliente.HasValue ? (object)intCliente.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Factura", intFactura.HasValue ? (object)intFactura.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@AuxiliarFactura", intAuxfactura.HasValue ? (object)intAuxfactura.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechaDe", FechaDe.HasValue ? (object)FechaDe.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechaHasta", FechaHasta.HasValue ? (object)FechaHasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@NumeroDe", intNumerode.HasValue ? (object)intNumerode.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@NumeroHasta", intNumeroHasta.HasValue ? (object)intNumeroHasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Auxiliar", intAuxiliar.HasValue ? (object)intAuxiliar.Value : DBNull.Value));

            dt = context.ExecuteProcedure("[sp_Almacenes_SalidaOrdenes]", true).Copy();


            return dt;
        }
        public static DataTable ObtieneDetalleOrdenesCompra(int intEmpresa, int intNumeroDevolucion, int intTipoOrden, int intConcepto)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Devolucion", intNumeroDevolucion));
            context.Parametros.Add(new SqlParameter("@Tipo_Orden", intTipoOrden));
            context.Parametros.Add(new SqlParameter("@Concepto", intConcepto));


            dt = context.ExecuteProcedure("[sp_Almacenes_DetalleOrdenes]", true).Copy();

            return dt;
        }


        public static DataTable ObtieneDetalleSalidasOrdenesCompra(int intEmpresa, int intNumeroOrden, int? intNumeroFactura, int? intNumeroAlmacen, int intConcepto)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Orden", intNumeroOrden));
            context.Parametros.Add(new SqlParameter("@Numero_Factura", intNumeroFactura));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intNumeroAlmacen));
            context.Parametros.Add(new SqlParameter("@Concepto", intConcepto));


            dt = context.ExecuteProcedure("[sp_Almacenes_SalidaDetalleOrdenes]", true).Copy();

            return dt;
        }

        public static DataTable ObtieneDetalleSalidasCantidadRecibida(int intEmpresa, int intTipoSalida, int? intNumeroTipoSalida, int intNumeroPartida, int? intNumeroAlmacen)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@tipoSalida", intTipoSalida));
            context.Parametros.Add(new SqlParameter("@numeroTipoSalida", intNumeroTipoSalida));
            context.Parametros.Add(new SqlParameter("@numeroPartida", intNumeroPartida));
            context.Parametros.Add(new SqlParameter("@numeroAlmacen", intNumeroAlmacen.HasValue ? (object)intNumeroAlmacen.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_Almacen_ObtieneCantidadRecibida]", true).Copy();

            return dt;
        }


        public static DataTable ObtieneNotaDeCredito(int intEmpresa, int? intRegDocto)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@numeroRegDocto", intRegDocto));

            dt = context.ExecuteProcedure("[sp_Almacen_CantidadNotaCredito]", true).Copy();

            return dt;
        }
        public static DataTable ObtieneCantidadDetalleNotaDeCredito(int intEmpresa, int intRegNota, int intNumeroCotizaDetalle)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@numeroRegNota", intRegNota));
            context.Parametros.Add(new SqlParameter("@numeroCotizaDetalle", intNumeroCotizaDetalle));



            dt = context.ExecuteProcedure("[sp_Almacen_CantidadDetalleNotaCredito]", true).Copy();

            return dt;
        }

        public static int RealizaEntradaAlmacen(int intTipoMovimiento, int intEmpresa, int intNumero, int intNumeroAlmacen, DateTime? Fecha, DateTime? Hora,
                                                      int? intTipoEntrada, int? intTipoNumeroEntrada, int? intUsuario, int? intPrioridad, int? intTipoDoctoEntrada, string sNumeroDoctoEntrada,
                                                      DateTime? FechaDoctoEntrada, string sAnexo, string sObservaciones, int? intMoneda, decimal? TipoCambio, DateTime? FechaCancelacion, string sCausa,
                                                      int? intProveedor, int? intEstatus, string sPedimento, int? intTipoCompra, int? intExterno)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            
            new SqlParameter("Tipo_Movimiento", intTipoMovimiento),
            new SqlParameter("Numero_Empresa", intEmpresa),
            new SqlParameter("Numero", intNumero),
            new SqlParameter("Numero_Almacen", intNumeroAlmacen),
            new SqlParameter("Fecha_Entrada", Fecha.HasValue ? (object)Fecha.Value : DBNull.Value),
            new SqlParameter("Hora_Entrada", Hora.HasValue ? (object)Hora.Value : DBNull.Value),
            new SqlParameter("Tipo_Entrada", intTipoEntrada.HasValue ? (object)intTipoEntrada.Value : DBNull.Value),
            new SqlParameter("Numero_Tipo_Entrada", intTipoNumeroEntrada.HasValue ? (object)intTipoNumeroEntrada.Value : DBNull.Value),
            new SqlParameter("Usuario", intUsuario.HasValue ? (object)intUsuario.Value : DBNull.Value),
            new SqlParameter("Prioridad", intPrioridad.HasValue ? (object)intPrioridad.Value : DBNull.Value),
            new SqlParameter("Tipo_Docto_Entrada", intTipoDoctoEntrada.HasValue ? (object)intTipoDoctoEntrada.Value : DBNull.Value),
            new SqlParameter("Numero_Docto_Entrada", sNumeroDoctoEntrada),
            new SqlParameter("Fecha_Docto_Entrada", FechaDoctoEntrada.HasValue ? (object)FechaDoctoEntrada.Value : DBNull.Value),
            new SqlParameter("Anexo", sAnexo),
            new SqlParameter("Observaciones", sObservaciones),
            new SqlParameter("Clas_Moneda", intMoneda.HasValue ? (object)intMoneda.Value : DBNull.Value),
            new SqlParameter("Tipo_Cambio", TipoCambio.HasValue ? (object)TipoCambio.Value : DBNull.Value),
            new SqlParameter("Fecha_Cancelacion", FechaCancelacion.HasValue ? (object)FechaCancelacion.Value : DBNull.Value),
            new SqlParameter("Causa", sCausa),
            new SqlParameter("Proveedor", intProveedor.HasValue ? (object)intProveedor.Value : DBNull.Value),
            new SqlParameter("Estatus", intEstatus.HasValue ? (object)intEstatus.Value : DBNull.Value),
            new SqlParameter("Pedimento", sPedimento),
            new SqlParameter("Tipo_Compra", intTipoCompra.HasValue ? (object)intTipoCompra.Value : DBNull.Value),
            new SqlParameter("Externo", intExterno.HasValue ? (object)intExterno.Value : DBNull.Value),
        };
            try
            {
              

                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        if (param.ParameterName == "Numero" && intNumero == 0)
                        {
                            param.Direction = ParameterDirection.Output;
                        }
                    }
                }

                return sqlDBHelper.ExecuteNonQueryOutputValue("sp_Entrada_Almacen", "Numero", CommandType.StoredProcedure, parameters);


            }
            catch (Exception)
            {

                return -1;
            }

           


        }
        public static bool RealizaEntradaAlmacenDetalle(int intTipoMovimiento, int intEmpresa, int intNumeroEntrada, int intNumeroAlmacen, int intNumeroPartida, int intTProducto, int intProducto,
                                                             int intTConcepto, int intConcepto, decimal dCantidadRecibida, int intUnidadRecibida, int intClasUbicacion, int intClasTipoInventario,
                                                             int intFamilia, string sLote, decimal? dCosto, int? intMoneda, decimal? TipoCambio, string sParte, int? intDetallado, string sDesArticuloNuevo,
                                                             DateTime? FechaCaducidad, int? intCompleto, int? intSecuencia)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo_Movimiento", intTipoMovimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Entrada", intNumeroEntrada));
            context.Parametros.Add(new SqlParameter("@Clas_Almacen", intNumeroAlmacen));
            context.Parametros.Add(new SqlParameter("@Numero_Partida", intNumeroPartida));
            context.Parametros.Add(new SqlParameter("@Clas_TProducto", intTProducto));
            context.Parametros.Add(new SqlParameter("@Clas_Producto", intProducto));
            context.Parametros.Add(new SqlParameter("@Clas_TConcepto", intTConcepto));
            context.Parametros.Add(new SqlParameter("@Concepto", intConcepto));
            context.Parametros.Add(new SqlParameter("@Cantidad_Recibida", dCantidadRecibida));
            context.Parametros.Add(new SqlParameter("@Unidad_Recibida", intUnidadRecibida));
            context.Parametros.Add(new SqlParameter("@Clas_Ubicacion", intClasUbicacion));
            context.Parametros.Add(new SqlParameter("@Clas_Tipo_Inventario", intClasTipoInventario));
            context.Parametros.Add(new SqlParameter("@Familia", intFamilia));
            context.Parametros.Add(new SqlParameter("@Lote", sLote));
            context.Parametros.Add(new SqlParameter("@Costo", dCosto));
            context.Parametros.Add(new SqlParameter("@Moneda", intMoneda));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", TipoCambio));
            context.Parametros.Add(new SqlParameter("@Parte", sParte));
            context.Parametros.Add(new SqlParameter("@Detallado", intDetallado));
            context.Parametros.Add(new SqlParameter("@Des_Articulo_Nuevo", sDesArticuloNuevo));
            context.Parametros.Add(new SqlParameter("@Fecha_Caducidad", FechaCaducidad.HasValue ? (object)FechaCaducidad.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Completo", intCompleto.HasValue ? (object)intCompleto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Secuencia", intSecuencia.HasValue ? (object)intSecuencia.Value : DBNull.Value));
            try
            {
                dt = context.ExecuteProcedure("[sp_Entrada_Almacen_Detalle]", true).Copy();
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }




        public static int RealizaSalidaAlmacen(int intTipoMovimiento, int intEmpresa, int intNumero, int intNumeroAlmacen, DateTime Fecha, DateTime Hora,
                                                 int intTipoEntrada, int intTipoNumeroEntrada, int intUsuario, int? intPrioridad, int intTipoDoctoEntrada, string sNumeroDoctoEntrada,
                                                 DateTime FechaDoctoEntrada, string sAnexo, string sObservaciones, int intMoneda, decimal TipoCambio, DateTime? FechaCancelacion, string sCausa,
                                                 int? intProveedor, int? intExterno)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
              new SqlParameter("Tipo_Movimiento", intTipoMovimiento),
              new SqlParameter("Numero_Empresa", intEmpresa),
              new SqlParameter("Numero", intNumero),
              new SqlParameter("Numero_Almacen", intNumeroAlmacen),
              new SqlParameter("Fecha_Salida", Fecha),
              new SqlParameter("Hora_Salida", Hora),
              new SqlParameter("Usuario", intUsuario),
              new SqlParameter("Tipo_Entrada", intTipoEntrada),
              new SqlParameter("Numero_Tipo_Entrada", intTipoNumeroEntrada),
              new SqlParameter("Prioridad", intPrioridad.HasValue ? (object)intPrioridad.Value : DBNull.Value),
              new SqlParameter("Tipo_Docto_Entrada", intTipoDoctoEntrada),
              new SqlParameter("Numero_Docto_Entrada", sNumeroDoctoEntrada),
              new SqlParameter("Fecha_Docto_Entrada", FechaDoctoEntrada),
              new SqlParameter("Anexo", sAnexo),
              new SqlParameter("Observaciones", sObservaciones),
              new SqlParameter("Clas_Moneda", intMoneda),
              new SqlParameter("Tipo_Cambio", TipoCambio),
              new SqlParameter("Proveedor", intProveedor.HasValue ? (object)intProveedor.Value : DBNull.Value),
              new SqlParameter("Fecha_Cancelacion", FechaCancelacion.HasValue ? (object)FechaCancelacion.Value : DBNull.Value),
              new SqlParameter("Causa", sCausa),
              new SqlParameter("Externo", intExterno.HasValue ? (object)intExterno.Value : DBNull.Value),
            };

            try
            {
                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        if (param.ParameterName == "Numero" && intNumero == 0)
                        {
                            param.Direction = ParameterDirection.Output;
                        }
                    }
                }

                return sqlDBHelper.ExecuteNonQueryOutputValue("sp_Salida_Almacen", "Numero", CommandType.StoredProcedure, parameters);

            }
            catch (Exception)
            {

                return -1;
            }




        }





        public static bool RealizaSalidaAlmacenDetalle(int intTipoMovimiento, int intEmpresa, int intNumeroEntrada, int intNumeroAlmacen, int intNumeroPartida, int intTProducto, int intProducto,
                                                          int intTConcepto, int intConcepto, decimal dCantidadRecibida, int intUnidadRecibida, int intClasUbicacion, int intClasTipoInventario,
                                                          int intFamilia, string sLote, string sNombreConcepto, decimal? dCosto, int? intMoneda, decimal? TipoCambio, int? intSecuencia)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo_Movimiento", intTipoMovimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Salida", intNumeroEntrada));
            context.Parametros.Add(new SqlParameter("@Clas_Almacen", intNumeroAlmacen));
            context.Parametros.Add(new SqlParameter("@Numero_Partida", intNumeroPartida));
            context.Parametros.Add(new SqlParameter("@Clas_TProducto", intTProducto));
            context.Parametros.Add(new SqlParameter("@Clas_Producto", intProducto));
            context.Parametros.Add(new SqlParameter("@Clas_TConcepto", intTConcepto));
            context.Parametros.Add(new SqlParameter("@Concepto", intConcepto));
            context.Parametros.Add(new SqlParameter("@Cantidad_Recibida", dCantidadRecibida));
            context.Parametros.Add(new SqlParameter("@Unidad_Recibida", intUnidadRecibida));
            context.Parametros.Add(new SqlParameter("@Clas_Ubicacion", intClasUbicacion));
            context.Parametros.Add(new SqlParameter("@Clas_Tipo_Inventario", intClasTipoInventario));
            context.Parametros.Add(new SqlParameter("@Familia", intFamilia));
            context.Parametros.Add(new SqlParameter("@Lote", sLote));
            context.Parametros.Add(new SqlParameter("@Nombre_Concepto", sNombreConcepto));
            context.Parametros.Add(new SqlParameter("@Costo", dCosto));
            context.Parametros.Add(new SqlParameter("@Moneda", intMoneda));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", TipoCambio));
            context.Parametros.Add(new SqlParameter("@Secuencia", intSecuencia.HasValue ? (object)intSecuencia.Value : DBNull.Value));

            try
            {
                dt = context.ExecuteProcedure("[sp_Salida_Almacen_Detalle]", true).Copy();
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }


        public static DataTable ObtieneAlmacenesOrdenesPendientes(int intEmpresa, int intTipoEntrada, int intNumeroEntrada)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Tipo_Entrada", intTipoEntrada));
            context.Parametros.Add(new SqlParameter("@Numero_Tipo_Entrada", intNumeroEntrada));
            dt = context.ExecuteProcedure("[sp_Almacen_OrdenesPendientes]", true).Copy();

            return dt;
        }
        public static DataTable ObtieneAlmacenesSalidasOrdenesPendientes(int intEmpresa, int intTipoEntrada, int intNumeroEntrada)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Tipo_Entrada", intTipoEntrada));
            context.Parametros.Add(new SqlParameter("@Numero_Tipo_Entrada", intNumeroEntrada));

            dt = context.ExecuteProcedure("[sp_Almacen_OrdenesSalidaPendientes]", true).Copy();

            return dt;
        }

        public static DataTable ObtieneDetalleDeEntradaAlmacen(int intEmpresa, int intNumeroEntrada, int intNumeroAlmacen)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Entrada", intNumeroEntrada));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intNumeroAlmacen));
            dt = context.ExecuteProcedure("sp_Almacen_ObtieneDetalleEntrada", true).Copy();

            return dt;
        }

        public static DataTable VerificaContabilizadaEntradaAlmacen(int intEmpresa, int intNumeroEntrada, int intNumeroAlmacen)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Entrada", intNumeroEntrada));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intNumeroAlmacen));
            dt = context.ExecuteProcedure("[sp_Almacen_VerificaContabilizada]", true).Copy();

            return dt;
        }
        public static DataTable ObtieneCantidadCosto(int intEmpresa, int intNumeroEntrada, int intNumeroAlmacen)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Entrada", intNumeroEntrada));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intNumeroAlmacen));
            dt = context.ExecuteProcedure("[sp_Almacen_ObtieneCantidadCosto]", true).Copy();

            return dt;
        }

        public static DataTable ObtieneAlmacenAuxiliar(int intEmpresa, int intNumeroAlmacen, int intConcepto)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Almacen", intNumeroAlmacen));
            context.Parametros.Add(new SqlParameter("@Concepto", intConcepto));
            dt = context.ExecuteProcedure("[sp_Almacenes_ObtieneAuxiliar]", true).Copy();

            return dt;
        }

        public static bool ActualizaContabilizada(int intEmpresa, int intNumeroEntrada, int intNumeroAlmacen)
        {
            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Entrada", intNumeroEntrada));
                context.Parametros.Add(new SqlParameter("@Numero_Almacen", intNumeroAlmacen));

                context.ExecuteProcedure("sp_AlmacenActualizaContabilizada", true).Copy();
                return true;

            }
            catch
            {
                return false;
            }
        }



        public static DataTable ObtieneExistenciayStockAlmacen(int intEmpresa, int intAlmacen, int intNumeroArticulo, int intClasProducto, int intclasTConcepto, int intclasConcepto)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@numeroAlmacen", intAlmacen));
            context.Parametros.Add(new SqlParameter("@numeroArticulo", intNumeroArticulo));
            context.Parametros.Add(new SqlParameter("@clasProducto", intClasProducto));
            context.Parametros.Add(new SqlParameter("@clasTConcepto", intclasTConcepto));
            context.Parametros.Add(new SqlParameter("@clasConcepto", intclasConcepto));

            dt = context.ExecuteProcedure("[sp_Almacen_ExistenciayStock]", true).Copy();

            return dt;
        }

        public DataTable GetLLenaDropsKardex(int opcion, int numEmpresa, int numAlmacen, int tipo, int numTipProducto, int numProducto, int numTipoConcepto)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Opcion", opcion));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", numAlmacen));
            context.Parametros.Add(new SqlParameter("@Tipo", tipo));
            context.Parametros.Add(new SqlParameter("@Numero_TProducto", numTipProducto));
            context.Parametros.Add(new SqlParameter("@Numero_Producto", numProducto));
            context.Parametros.Add(new SqlParameter("@Numero_TConcepto", numTipoConcepto));

            dt = context.ExecuteProcedure("spObtieneDatosKardex", true).Copy();
            return dt;
        }

        public bool CreaKardexOriginal(int numEmpresa, int numUsuario, DateTime? FechaInicio, DateTime? FechaFin, int claAlmacen,
                                    int? familiaCons, int? clasTipoProducto, int? clasProducto, int? clasTipoConcepto, int? clasConcepto)
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
                context.Parametros.Add(new SqlParameter("@Usuario", numUsuario));
                context.Parametros.Add(new SqlParameter("@Fecha_Ini", FechaInicio.HasValue ? (object)FechaInicio.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Fecha_Fin", FechaFin.HasValue ? (object)FechaFin.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Clas_Almacen", claAlmacen));
                context.Parametros.Add(new SqlParameter("@Familia_Cons", familiaCons.HasValue ? (object)familiaCons.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Clas_TProducto_Cons", clasTipoProducto.HasValue ? (object)clasTipoProducto.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Clas_Producto_Cons", clasProducto.HasValue ? (object)clasProducto.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Clas_TConcepto_Cons", clasTipoConcepto.HasValue ? (object)clasTipoConcepto.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Clas_Concepto_Cons", clasConcepto.HasValue ? (object)clasConcepto.Value : DBNull.Value));

                context.ExecuteProcedure("sp_Crea_Kardex_Original", true).Copy();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        public DataTable ObtieneKardexAlmacen(int tipo, int numEmpresa, int numPersona)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo", tipo));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Persona", numPersona));

            dt = context.ExecuteProcedure("spObtieneSaldosAlmacen", true);
            return dt;
        }


        //ENTRADAS DIRECTAS A ALMACEN

        public static DataTable ObtieneAlmacenesPropio(int intEmpresa, int intNumUsuario, string sNombreAlmacen)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", intNumUsuario));
            context.Parametros.Add(new SqlParameter("@Nombre_Almacen", sNombreAlmacen));
            dt = context.ExecuteProcedure("[sp_ObtieneAlmacenesPropio]", true).Copy();
  
            return dt;
        }

        public static DataTable ObtieneAlmacenesTipoDocumento(int intEmpresa)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));

            dt = context.ExecuteProcedure("[sp_AlmacenObtieneTipoDocto]", true).Copy();

            return dt;
        }

        public static DataTable ObtieneUnidadMedidaProducto(int intEmpresa, int intConcepto,string sDescripcion)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Clas_Concepto", intConcepto));
            context.Parametros.Add(new SqlParameter("@Descripcion", sDescripcion));

            dt = context.ExecuteProcedure("[sp_ObtieneDatosProductoArticulo]", true).Copy();

            return dt;
        }


        public DataTable gfProductosArticulosAlmacen(int Numero_Empresa, string sCodigo, int intNumeroAlmacen)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Codigo", sCodigo));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intNumeroAlmacen));



            dt = context.ExecuteProcedure("sp_Productos_Articulos_Union_Almacen", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }


        //ENTRADAS DIRECTAS A ALMACEN (USUARIO-ALMACEN)
        public static DataTable ObtieneAlmacenesUsuarioPropio(int intEmpresa, int intNumAlmacen,int intNumUsuario)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intNumAlmacen));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", intNumUsuario));
            dt = context.ExecuteProcedure("[sp_AlmacenValidaPropio]", true).Copy();


            return dt;
        }
        public static DataTable ObtieneAlmaceneActivoSuspendido(int intEmpresa, int intNumAlmacen)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intNumAlmacen));

            dt = context.ExecuteProcedure("[sp_AlmacenSuspension]", true).Copy();


            return dt;
        }

        public static DataTable VerificaPeriodoCerrado(int intEmpresa, DateTime dFecha)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Fecha", dFecha));

            dt = context.ExecuteProcedure("[sp_Trae_Periodo_Cerrado]", true).Copy();

            return dt;
        }


        public static bool sp_Entrada_GeneraContabilidad(int intEmpresa, int intUsuario, int intAlmacen, int intEntrada)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen));
            context.Parametros.Add(new SqlParameter("@Numero_Entrada", intEntrada));
                  
            try
            {
                dt = context.ExecuteProcedure("[sp_Entrada_GeneraContabilidad]", true).Copy();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }


        public static bool sp_Entrada_GeneraReversaContabilidad(int intEmpresa, int intUsuario, int intAlmacen, int intEntrada, DateTime FechaCancelacion)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen));
            context.Parametros.Add(new SqlParameter("@Numero_Entrada", intEntrada));
            context.Parametros.Add(new SqlParameter("@Fecha_Cancelacion", FechaCancelacion));


       
            try
            {
                dt = context.ExecuteProcedure("[sp_Entrada_GeneraReversaContabilidad]", true).Copy();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }



        public static DataTable ObtieneAlmacenEntradasDirectas(int intEmpresa,int intNumUsuario, int? intNumAlmacen, 
                                                                DateTime? fechaDesde, DateTime? fechaHasta, int? intNumDesde, int? intNumHasta)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", intNumUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intNumAlmacen.HasValue ? (object)intNumAlmacen.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Desde", fechaDesde.HasValue ? (object)fechaDesde.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Hasta", fechaHasta.HasValue ? (object)fechaHasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Desde", intNumDesde.HasValue ? (object)intNumDesde.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Hasta", intNumHasta.HasValue ? (object)intNumHasta.Value : DBNull.Value));

            dt = context.ExecuteProcedure("[sp_Entrada_TraeEntradas_grd]", true).Copy();

            return dt;
        }

        public static DataTable ObtieneAlmacenCantidadEntregada(int intEmpresa, int intNumAlmacen, int intNumEntrada,int? intNumPartida)
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intNumAlmacen));
            context.Parametros.Add(new SqlParameter("@Numero_Entrada", intNumEntrada));
            context.Parametros.Add(new SqlParameter("@Numero_Partida", intNumPartida.HasValue ? (object)intNumPartida.Value : DBNull.Value));
        
            dt = context.ExecuteProcedure("[sp_AlmacenObtieneCantidadEntregada]", true).Copy();

            return dt;
        }



        ////////////////////////CONSULTA EXISTENCIA ALMACEN/////////////////////////////////////
        
        public DataTable ObtieneAlmacenConsultaExistencia(int intEmpresa, int intUsuario)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", intUsuario));
           
            dt = context.ExecuteProcedure("sp_Almacen_Busca_Usuario", true).Copy();
            return dt;
        }


        public DataTable ObtieneDropsProductosAlmacen(int intEmpresa, int intUsuario, int? intAlmacen, int intTipo, int intNivel, int? intTProducto, int? intProducto, int? intTConcepto, string sDescripcion)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo", intTipo));
            context.Parametros.Add(new SqlParameter("@Nivel", intNivel));
            context.Parametros.Add(new SqlParameter("@TProducto", intTProducto.HasValue ? (object)intTProducto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Producto", intProducto.HasValue ? (object)intProducto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@TConcepto", intTConcepto.HasValue ? (object)intTConcepto.Value : DBNull.Value));

            if (sDescripcion == string.Empty)
            {
                context.Parametros.Add(new SqlParameter("@Descripcion", DBNull.Value));
            }
            else
            {
                context.Parametros.Add(new SqlParameter("@Descripcion", sDescripcion));
            }
           
            

            dt = context.ExecuteProcedure("sp_Almacen_Ubicacion_LlenaDrop", true).Copy();
            return dt;
        }


        public DataTable sp_Almacen_Existencia_Fisica_Net(int intEmpresa, DateTime? Fecha, int? intAlmacen, int? intTipoFamilia,int? intTProducto, 
                                                          int? intProducto, int? intTConcepto, int? intConcepto,int intUsuario)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Fecha", Fecha.HasValue ? (object)Fecha.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Inventario", DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Familia", intTipoFamilia.HasValue ? (object)intTipoFamilia.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Producto", intTProducto.HasValue ? (object)intTProducto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Producto", intProducto.HasValue ? (object)intProducto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Concepto", intTConcepto.HasValue ? (object)intTConcepto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Concepto", intConcepto.HasValue ? (object)intConcepto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Auxiliar", 1));
            context.Parametros.Add(new SqlParameter("@Cero_Existencia", DBNull.Value));
            
            dt = context.ExecuteProcedure("sp_Almacen_Existencia_Fisica_Net", true).Copy();
            return dt;
        }


        public DataTable sp_Almacen_Existencia_Fisica_Imp_Net(int intEmpresa, int intUsuario)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));


            dt = context.ExecuteProcedure("sp_Almacen_Existencia_Fisica_Imp_Net", true).Copy();
            return dt;
        }



    }
}
