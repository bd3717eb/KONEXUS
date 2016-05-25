using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using IntegraData;

namespace IntegraBussines
{
    public class DevolucionController 
    {



        public static DataTable GetDetailsTableFromSaleNumber2(int icompany, int isalenumber, int inumcotizacion)
        {
            DataTable dt = new DataTable();
            //DataTable dtRegresa = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@salenumber", isalenumber));
            context.Parametros.Add(new SqlParameter("@Numero_Cotiza", inumcotizacion));

            dt = context.ExecuteProcedure("[sp_Venta_ObtieneDetallesDeTablaPorNumeroDeVenta_v3]", true).Copy();
            //dt = context.ExecuteProcedure("[sp_Venta_ObtieneDetallesDeTablaPorNumeroDeVenta_v2]", true).Copy();
            //dtRegresa = gfUnion(isalenumber, dt);
            return dt;
        }
        public static DataTable GetDetailsTableFromSaleNumber3(int Numero_Factura, int Numero_Empresa, int Numero_Almacen)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Factura", Numero_Factura));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", Numero_Almacen));
            dt = context.ExecuteProcedure("[sp_Venta_ObtieneDetallesDeTablaPorNumeroDeVenta_v3]", true).Copy();
            return dt;
        }

        public static DataTable ObtieneDetalles(int icompany, int isalenumber)
        {
            DataTable dt = new DataTable();
            //DataTable dtRegresa = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@salenumber", isalenumber));


            dt = context.ExecuteProcedure("[sp_Devoluciones_ObtieneDetallesPorNumeroDeVenta_v2]", true).Copy();

            return dt;
        }

        public static DataTable ValidaPeriodoContable(int iEmpresa, int iPeriodo, int iAnio)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@periodo", iPeriodo));
            context.Parametros.Add(new SqlParameter("@anio", iAnio));

            dt = context.ExecuteProcedure("[sp_NotaDeCredito_ValidaPeriodoContable_v1]", true).Copy();
            return dt;
        }

        public static DataTable IsProduct(int iEmpresa, int iProducto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@product", iProducto));
            dt = context.ExecuteProcedure("[sp_Venta_EsProducto_v1]", true).Copy();
            return dt;
        }

        public static DataTable gfUnion(int Numero_Factura, DataTable dtRecibe)
        {
            DataTable dtRegresa = gfConstruyeDT().Copy();
            DataTable dtA = dtRecibe;

            foreach (DataRow row in dtA.Rows)
            {
                DataRow dr = dtRegresa.NewRow();
                dr["Tipo_Concepto"] = row["Tipo_Concepto"];
                dr["Producto"] = row["Producto"];
                dr["Concepto"] = row["Concepto"];
                dr["Precio"] = row["Precio"];
                dr["Unidad"] = row["Unidad"];
                dr["Cantidad"] = row["Cantidad"];
                dr["Total"] = row["Total"];
                dr["Numero"] = row["Numero"];
                dr["Empresa"] = row["Empresa"];
                dr["Descripcion_Producto"] = row["Descripcion_Producto"];
                dr["Descripcion_Unidad"] = row["Descripcion_Unidad"];
                dr["FPTCC"] = row["FPTCC"];
                dr["partida"] = row["partida"];
                dr["costo_producto"] = gfn_Obten_Costo_Concepto((int)row["Empresa"], (int)row["Producto"], (int)row["Tipo_Concepto"], (int)row["Tipo"], Numero_Factura).Rows[0][0].ToString();
                dtRegresa.Rows.Add(dr);
            }
            return dtRegresa;
        }
        public static DataTable gfConstruyeDT()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Tipo_Concepto", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Concepto", typeof(string));
            dt.Columns.Add("Precio", typeof(string));
            dt.Columns.Add("Unidad", typeof(string));
            dt.Columns.Add("Cantidad", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            dt.Columns.Add("Numero", typeof(string));
            dt.Columns.Add("Empresa", typeof(string));
            dt.Columns.Add("Descripcion_Producto", typeof(string));
            dt.Columns.Add("Descripcion_Unidad", typeof(string));
            dt.Columns.Add("FPTCC", typeof(string));
            dt.Columns.Add("partida", typeof(string));
            dt.Columns.Add("costo_producto", typeof(string));
            return dt;
        }
        public static DataTable gfn_Obten_Costo_Concepto(int Numero_Empresa, int Clas_Producto, int Clas_Concepto, int Familia_Concepto, int Numero_Factura)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            string sQuery = "SELECT dbo.fn_Obten_Costo_Concepto(" + Numero_Empresa + "," + Clas_Producto + "," + Clas_Concepto + "," + Familia_Concepto + "," + Numero_Factura + ") AS Costo";
            dt = context.ExecuteQuery(sQuery).Copy();
            return dt;
        }
        public static DataTable ObtieneCatalogo(int iempresa, int ipadre, int ifamilia)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@pFamilia", ifamilia));
            context.Parametros.Add(new SqlParameter("@pEmpresa", iempresa));
            context.Parametros.Add(new SqlParameter("@pNumero_Padre", ipadre));

            dt = context.ExecuteProcedure("[sp_Clasificacion_generico]", true).Copy();

            return dt;
        }
        public static DataTable LLenaBanco(int iempresa, int iusuario)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@pNumero_Empresa", iempresa));
            context.Parametros.Add(new SqlParameter("@pNum_Persona", iusuario));

            dt = context.ExecuteProcedure("[sp_Bancos_Chequeras_byEmpresaPersona]", true).Copy();

            return dt;
        }
        public static DataTable LLenaCuenta(int iempresa, int iusuario, int ibanco)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@pNumero_Empresa", iempresa));
            context.Parametros.Add(new SqlParameter("@pNum_Persona", iusuario));
            context.Parametros.Add(new SqlParameter("@pNum_Banco", ibanco));
            dt = context.ExecuteProcedure("[sp_Bancos_Chequeras_byEmpresaPersona_Detalle]", true).Copy();

            return dt;
        }
       
        public static DataTable LLenaMotivosDevolucion(int iempresa)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", iempresa));


            dt = context.ExecuteProcedure("[sp_Devolucion_ConceptoDevolucion]", true).Copy();

            return dt;
        }

        
        public static DataTable ObtieneSucursales(int iempresa, int iusuario)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@pNumero_Empresa", iempresa));
            context.Parametros.Add(new SqlParameter("@pNum_Usuario", iusuario));

            dt = context.ExecuteProcedure("[sp_Sucursal_CatalogoByEmpresaUsuario]", true).Copy();

            return dt;
        }
        public static DataTable BancoSiguienteFolio(int iClas_Cuenta, int iNumero_Banco, int iNumero_Empresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Clas_Cuenta", iClas_Cuenta));
            context.Parametros.Add(new SqlParameter("Numero_Banco", iNumero_Banco));
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iNumero_Empresa));
            dt = context.ExecuteProcedure("[sp_Banco_FolioNext]", true).Copy();
            return dt;
        }
        public static DataTable ObtieneDocumentosDeNotaVenta(int iempresa, int iusuario)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@companynumber", iempresa));
            context.Parametros.Add(new SqlParameter("@usernumber", iusuario));

            dt = context.ExecuteProcedure("[sp_NotaDeCredito_ObtieneDocumentosDeNota_v1]", true).Copy();

            return dt;
        }
        public static DataTable ObtieneSerieDeNotaVenta(int iempresa, int iusuario, int idocumento)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@companynumber", iempresa));
            context.Parametros.Add(new SqlParameter("@usernumber", iusuario));
            context.Parametros.Add(new SqlParameter("@document", idocumento));

            dt = context.ExecuteProcedure("[sp_NotaDeCredito_ObtieneTipoDeDocumentos_v1]", true).Copy();

            return dt;
        }


        //public static DataTable gfCuentasCobrarSaldo(int iNumero_Empresa, int iNumero_Cliente, int iDocumento)
        //{
        //    string sQuery = String.Empty;
        //    SQLConection context = new SQLConection();
        //    DataTable dt = new DataTable();

        //    sQuery = string.Concat(" SELECT  SALDO ",
        //                           " FROM CUENTAS_COBRAR ",
        //                           " WHERE Numero_Empresa = " + iNumero_Empresa + "",
        //                           " AND Numero_Cliente = " + iNumero_Cliente + "",
        //                           " AND Documento = " + "'" + iDocumento + "'" + "");
        //    dt = context.ExecuteQuery(sQuery);
        //    return dt;
        //}
        public static DataTable gfCuentasCobrarSaldo(int iNumero_Empresa, int iNumero_Cliente, int iDocumento)//hacer sp
        {
            
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Cliente", iNumero_Cliente));
            context.Parametros.Add(new SqlParameter("@Numero_Documento", iDocumento));

            dt = context.ExecuteProcedure("SP_CuentasCobrarObtenSaldo", true).Copy();
            return dt;
            
        }
        public static DataTable gf_Devolucion_Cliente(Nullable<int> tipo_Movimiento,
                                                   Nullable<int> numero_Empresa,
                                                   int numero,
                                                   string fecha,
                                                   int? estatus,
                                                   int? numero_Cliente,
                                                   int? clas_Concepto,
                                                   int? numero_Almacen,
                                                   int? clas_Causa,
                                                   int? clas_Moneda,
                                                   int? numero_Factura,
                                                   int? folio,
                                                   int? clas_Iva,
                                                   decimal? tipo_Cambio,
                                                   decimal? subTotal,
                                                   decimal? iva,
                                                   decimal? total,
                                                   int? tipo_Devolucion,
                                                   string observaciones,
                                                   int numero_Usuario,
                                                   int? familia_Concepto_Causa,
                                                   int? familia_Almacen,
                                                   int? familia_Moneda,
                                                   int? familia_Iva,
                                                   int? tipo_Docto_Entrada,
                                                   Nullable<System.DateTime> fecha_Cancelacion,
                                                   string causa_Cancelacion,
                                                   Nullable<byte> relacion_Factura, Nullable<int> Clas_Causa = null,
                                                   Nullable<int> intClas_Sucursal = null)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo_Movimiento", tipo_Movimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero", numero));
            context.Parametros.Add(new SqlParameter("@Fecha", fecha));
            context.Parametros.Add(new SqlParameter("@Estatus", estatus.HasValue ? (object)estatus.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Cliente", numero_Cliente.HasValue ? (object)numero_Cliente.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_Concepto", clas_Concepto.HasValue ? (object)clas_Concepto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", numero_Almacen.HasValue ? (object)numero_Almacen.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_Causa", Clas_Causa.HasValue ? (object)Clas_Causa.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_Moneda", clas_Moneda.HasValue ? (object)clas_Moneda.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Factura", numero_Factura.HasValue ? (object)numero_Factura.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Folio", folio.HasValue ? (object)folio.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_Iva", clas_Iva.HasValue ? (object)clas_Iva.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", tipo_Cambio.HasValue ? (object)tipo_Cambio.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@SubTotal", subTotal.HasValue ? (object)subTotal.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Iva", iva.HasValue ? (object)iva.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Total", total.HasValue ? (object)total.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Devolucion", tipo_Devolucion.HasValue ? (object)tipo_Devolucion.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Observaciones", observaciones));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", numero_Usuario));
            context.Parametros.Add(new SqlParameter("@Familia_Concepto_Causa", familia_Concepto_Causa.HasValue ? (object)familia_Concepto_Causa.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Familia_Almacen", familia_Almacen.HasValue ? (object)familia_Almacen.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Familia_Moneda", familia_Moneda.HasValue ? (object)familia_Moneda.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Familia_Iva", familia_Iva.HasValue ? (object)familia_Iva.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Docto_Entrada", tipo_Docto_Entrada.HasValue ? (object)tipo_Docto_Entrada.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Cancelacion", fecha_Cancelacion));
            context.Parametros.Add(new SqlParameter("@Causa_Cancelacion", causa_Cancelacion));
            context.Parametros.Add(new SqlParameter("@Relacion_Factura", 1));
            context.Parametros.Add(new SqlParameter("@Clas_Sucursal", intClas_Sucursal.HasValue ? (object)intClas_Sucursal.Value : DBNull.Value));
            dt = context.ExecuteProcedure("sp_Devolucion_Cliente_v2", true).Copy();
            return dt;
        }

        public static DataTable gf_Devolucion_Cliente_Detalle(
             int Tipo_Movimiento,
             int Numero_Empresa,
             int Numero_Devolucion,
             int Secuencia,
             int Clas_Producto,
             int Clas_Concepto,
             int Familia,
             decimal Cantidad,
             decimal Precio_Unitario,
             int Clas_Causa,
             int Familia_Concepto_Causa,
             Nullable<decimal> Costo = null)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo_Movimiento", Tipo_Movimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Devolucion", Numero_Devolucion));
            context.Parametros.Add(new SqlParameter("@Secuencia", Secuencia));
            context.Parametros.Add(new SqlParameter("@Clas_Producto", Clas_Producto));
            context.Parametros.Add(new SqlParameter("@Clas_Concepto", Clas_Concepto));
            context.Parametros.Add(new SqlParameter("@Familia", Familia));
            context.Parametros.Add(new SqlParameter("@Cantidad", Cantidad));
            context.Parametros.Add(new SqlParameter("@Precio_Unitario", Precio_Unitario));
            context.Parametros.Add(new SqlParameter("@Clas_Causa", Clas_Causa));
            context.Parametros.Add(new SqlParameter("@Familia_Concepto_Causa", Familia_Concepto_Causa));
            context.Parametros.Add(new SqlParameter("@Costo", Costo));
            dt = context.ExecuteProcedure("sp_Devolucion_Cliente_Detalle_v2", true).Copy();
            return dt;
        }
        // =====================================ENTRADA ALMACEN======================================
        public static DataTable gf_Entrada_Almacen_Devolucion_Cliente(int tipo_Movimiento,
                                               int numero_Empresa,
                                               int numero,
                                               int numero_almacen,
                                               string fecha_entrada,
                                               System.DateTime hora_entrada,
                                               int tipo_entrada,
                                               int numero_tipo_entrada,
                                               int usuario,
                                               int? prioridad,
                                               int tipo_docto_entrada,
                                               string numero_docto_entrada,
                                               DateTime fecha_docto_entrada,
                                               string anexo,
                                               string observaciones,
                                               int clas_moneda,
                                               decimal tipo_cambio,
                                               DateTime? fecha_cancelacion,
                                               string causa,
                                               int? proveedor,
                                               int? estatus,
                                               string pedimento,
                                               int? numero_devolucion)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo_Movimiento", tipo_Movimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero", numero));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", numero_almacen));
            context.Parametros.Add(new SqlParameter("@Fecha_Entrada", fecha_entrada));
            context.Parametros.Add(new SqlParameter("@Hora_Entrada", hora_entrada));
            context.Parametros.Add(new SqlParameter("@Tipo_Entrada", tipo_entrada));
            context.Parametros.Add(new SqlParameter("@Numero_Tipo_Entrada", numero_tipo_entrada));
            context.Parametros.Add(new SqlParameter("@Usuario", usuario));
            context.Parametros.Add(new SqlParameter("@Prioridad", prioridad));
            context.Parametros.Add(new SqlParameter("@Tipo_Docto_Entrada", tipo_docto_entrada));
            context.Parametros.Add(new SqlParameter("@Numero_Docto_Entrada", numero_docto_entrada));
            context.Parametros.Add(new SqlParameter("@Fecha_Docto_Entrada", fecha_docto_entrada));
            context.Parametros.Add(new SqlParameter("@Anexo", anexo));
            context.Parametros.Add(new SqlParameter("@Observaciones", observaciones));
            context.Parametros.Add(new SqlParameter("@Clas_Moneda", clas_moneda));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", tipo_cambio));
            context.Parametros.Add(new SqlParameter("@Fecha_Cancelacion", fecha_cancelacion));
            context.Parametros.Add(new SqlParameter("@Causa", causa));
            context.Parametros.Add(new SqlParameter("@Proveedor", proveedor));
            context.Parametros.Add(new SqlParameter("@Estatus", estatus));
            context.Parametros.Add(new SqlParameter("@Pedimento", pedimento));
            context.Parametros.Add(new SqlParameter("@Numero_Devolucion", numero_devolucion));

            dt = context.ExecuteProcedure("sp_Entrada_Almacen_Devolucion_Cliente_v2", true).Copy();
            return dt;
        }

        public static DataTable gf_Entrada_Almacen_Detalle_Devolucion(
             int Tipo_Movimiento,
             int Numero_Empresa,
             int Numero_Entrada,
             int Clas_Almacen,
             int Numero_Partida,
             int Clas_TProducto,
             int Clas_Producto,
             int Clas_TConcepto,
             int Concepto,
             decimal Cantidad_Recibida,
             int Unidad_Recibida,
             int Clas_Ubicacion,
             int Clas_Tipo_Inventario,
             int Familia,
             string Lote,
             decimal Precio,
             int? Moneda,
             decimal Tipo_Cambio,
             string Parte,
             int? Detallado,
             string Des_Articulo_Nuevo,
             DateTime? Fecha_Caducidad,
             int? Completo,
             int? Secuencia,
             int? numero_devolucion)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo_Movimiento", Tipo_Movimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Entrada", Numero_Entrada));
            context.Parametros.Add(new SqlParameter("@Clas_Almacen", Clas_Almacen));
            context.Parametros.Add(new SqlParameter("@Numero_Partida", Numero_Partida));
            context.Parametros.Add(new SqlParameter("@Clas_TProducto", Clas_TProducto));
            context.Parametros.Add(new SqlParameter("@Clas_Producto", Clas_Producto));
            context.Parametros.Add(new SqlParameter("@Clas_TConcepto", Clas_TConcepto));
            context.Parametros.Add(new SqlParameter("@Concepto", Concepto));
            context.Parametros.Add(new SqlParameter("@Cantidad_Recibida", Cantidad_Recibida));
            context.Parametros.Add(new SqlParameter("@Unidad_Recibida", 895));
            context.Parametros.Add(new SqlParameter("@Clas_Ubicacion", 138));
            context.Parametros.Add(new SqlParameter("@Clas_Tipo_Inventario", Clas_Tipo_Inventario));
            context.Parametros.Add(new SqlParameter("@Familia", Familia));
            context.Parametros.Add(new SqlParameter("@Lote", Lote));
            context.Parametros.Add(new SqlParameter("@Precio", Precio));
            context.Parametros.Add(new SqlParameter("@Moneda", Moneda));
            context.Parametros.Add(new SqlParameter("@Tipo_Cambio", Tipo_Cambio));
            context.Parametros.Add(new SqlParameter("@Parte", Parte));
            context.Parametros.Add(new SqlParameter("@Detallado", Detallado));
            context.Parametros.Add(new SqlParameter("@Des_Articulo_Nuevo", Des_Articulo_Nuevo));
            context.Parametros.Add(new SqlParameter("@Fecha_Caducidad", Fecha_Caducidad));
            context.Parametros.Add(new SqlParameter("@Completo", Completo));
            context.Parametros.Add(new SqlParameter("@Secuencia", Secuencia));
            context.Parametros.Add(new SqlParameter("@Numero_Devolucion", numero_devolucion));
            dt = context.ExecuteProcedure("sp_Entrada_Almacen_Detalle_Devolucion ", true).Copy();
            return dt;
        }

        // ==========================================================================================
        public static DataTable gf_Devolucion_ObtieneNumeroNota_byEmpresaNota(int Numero_Empresa, int Numero_Nota)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Nota", Numero_Nota));
            dt = context.ExecuteProcedure("sp_Devolucion_ObtieneNumeroNota_byEmpresaNota", true).Copy();
            return dt;
        }
        public static DataTable gf_Devolucion_Guarda_Nota_Detalle(int Numero, int pOpcion = 0)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero", Numero));
            context.Parametros.Add(new SqlParameter("@pOpcion", pOpcion));
            dt = context.ExecuteProcedure("sp_Devolucion_Guarda_Nota_Detalle", true).Copy();
            return dt;
        }

        public static DataTable gf_Devolucion_Guarda_Nota_Detalle_2(int Numero_Devolucion, int Numero_Empresa)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Devolucion", Numero_Devolucion));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            dt = context.ExecuteProcedure("sp_Devolucion_Guarda_Nota_Detalle_2", true).Copy();
            return dt;
        }
        public static DataTable gf_ContabilizaDevolucion(int Clas_Numero, int Numero_Empresa, int Numero_Almacen)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Clas_Numero", Clas_Numero));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", Numero_Almacen));
            dt = context.ExecuteProcedure("sp_ContabilizaDevolucion", true).Copy();
            return dt;
        }
        public static DataTable CreaNotaEncabezado(int iMovimiento, int iEmpresa, int iNumero, int iCliente, DateTime Fecha, int iTipoConceptoImporte, int iCIva, int iTipoDoctoNota,
                          int iTipoDoctoNotaRelacion, int iNumeroNota, int iMoneda, int iCConcepto, string sObservaciones,
                          decimal dSubtotal, decimal dDescuento, decimal dIva, decimal dTotal, string sImporteLetra, decimal? dTipoCambio, int iNPersonaAplicada,
                          int CEstatus, int? TipoDocto, int? TipoDoctoRelacion, int? NumeroRegDocto, int? NumeroDocto, int TipoTransaccion,
                          int? NumeroFacturaRelacion, int? NumeroCXC, DateTime? FechaCancelacion, string Causa, int? usuario, int CSucursal, int usuariocaptura, int? RelacionFactura, int iContabilizada)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Tipo_Movimiento", iMovimiento));
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Numero", iNumero));
            context.Parametros.Add(new SqlParameter("Numero_Cliente", iCliente));
            context.Parametros.Add(new SqlParameter("Fecha", Fecha));
            context.Parametros.Add(new SqlParameter("Tipo_Concepto_Importe", iTipoConceptoImporte));
            context.Parametros.Add(new SqlParameter("Clas_Iva", iCIva));
            context.Parametros.Add(new SqlParameter("Tipo_Docto_Nota", iTipoDoctoNota));
            context.Parametros.Add(new SqlParameter("Tipo_Docto_Nota_Relacion", iTipoDoctoNotaRelacion));
            context.Parametros.Add(new SqlParameter("Numero_Nota", iNumeroNota));
            context.Parametros.Add(new SqlParameter("Clas_Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("Clas_Concepto", iCConcepto));
            context.Parametros.Add(new SqlParameter("Comentario", sObservaciones));
            context.Parametros.Add(new SqlParameter("Subtotal", dSubtotal));
            context.Parametros.Add(new SqlParameter("Descuento", dDescuento));
            context.Parametros.Add(new SqlParameter("Iva", dIva));
            context.Parametros.Add(new SqlParameter("Total", dTotal));
            context.Parametros.Add(new SqlParameter("Importe_Letra", sImporteLetra));
            context.Parametros.Add(new SqlParameter("Tipo_Cambio", dTipoCambio.HasValue ? (object)dTipoCambio.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Numero_Persona_Aplicada", iNPersonaAplicada));
            context.Parametros.Add(new SqlParameter("Clas_Estatus", CEstatus));
            context.Parametros.Add(new SqlParameter("Tipo_Docto", TipoDocto.HasValue ? (object)TipoDocto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Tipo_Docto_Relacion", TipoDoctoRelacion.HasValue ? (object)TipoDoctoRelacion.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Numero_Reg_Docto", NumeroRegDocto.HasValue ? (object)NumeroRegDocto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Numero_Docto", NumeroDocto.HasValue ? (object)NumeroDocto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Tipo_Transaccion", TipoTransaccion));
            context.Parametros.Add(new SqlParameter("Numero_Factura_Relacion", NumeroFacturaRelacion.HasValue ? (object)NumeroFacturaRelacion.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Numero_CXC", NumeroCXC.HasValue ? (object)NumeroCXC.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Fecha_Cancelacion", FechaCancelacion.HasValue ? (object)FechaCancelacion.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Causa", Causa));
            context.Parametros.Add(new SqlParameter("Usuario", usuario.HasValue ? (object)usuario.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Clas_Sucursal", CSucursal));
            context.Parametros.Add(new SqlParameter("Usuario_Captura", usuariocaptura));
            context.Parametros.Add(new SqlParameter("Relacion_Factura", RelacionFactura));
            context.Parametros.Add(new SqlParameter("Contabilizada", iContabilizada));

            dt = context.ExecuteProcedure("sp_Nota_Credito_v2", true).Copy();
            return dt;

        }

        public static DataTable CreaNotaDetalle(int iMovimiento, int iEmpresa, int iNumeroNota, int iTipoConceptoImporte, decimal dTotal, int iNumberCot, int iNumberCotDet,
                                  decimal? dCantidad, int? iSecuencia)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Tipo_Movimiento", iMovimiento));
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Numero_Registro_Nota", iNumeroNota));
            context.Parametros.Add(new SqlParameter("Tipo_Importe_Concepto", iTipoConceptoImporte));
            context.Parametros.Add(new SqlParameter("Importe", dTotal));
            context.Parametros.Add(new SqlParameter("Numero_Cotiza", iNumberCot));
            context.Parametros.Add(new SqlParameter("Numero_Cotiza_Detalle", iNumberCotDet));
            context.Parametros.Add(new SqlParameter("Cantidad", dCantidad.HasValue ? (object)dCantidad.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Secuencia", iSecuencia.HasValue ? (object)iSecuencia.Value : DBNull.Value));
            dt = context.ExecuteProcedure("sp_Nota_Credito_Detalle", true).Copy();
            return dt;

        }

        public static DataTable gf_Devolucion_ValidaNota(int Numero_Empresa, int Numero)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero", Numero));
            dt = context.ExecuteProcedure("sp_Devolucion_ValidaNota", true).Copy();
            return dt;
        }
        //

        public static DataTable gf_Devolucion_VerificaAnticipo(int Numero_Empresa, int Numero)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Devolucion", Numero));
            dt = context.ExecuteProcedure("sp_Devoluciones_VerificaCXCAnticipos", true).Copy();
            return dt;
        }


        public static int gf_ActualizaDevolucionCliente(decimal dSubtotal, decimal dIva, decimal dTotal, int iNumero, int iEmpresa)//hacer sp
        {
            string sQuery = string.Empty;
            int iRespuesta = 0;
            try
            {
                sQuery = string.Concat("UPDATE Devolucion_Cliente  SET   Subtotal	=  '" + dSubtotal + "'   ,dIva	= '" + dIva + "'   ,dTotal = '" + dTotal + "'   WHERE Numero = '" + iNumero + "' AND Numero_Empresa =" + iEmpresa + "");
                new SQLConection().ExecuteNonQuery(sQuery);
            }
            catch
            {
                iRespuesta = -1;
            }
            return iRespuesta;

        }

  

        public static int gfi_Devolucion_Cliente_Actualiza(int iOpcion, ref List<string> listaParametros)//hacer sp
        {
            int iRespuesta = 0;
            SQLConection context = new SQLConection();
            switch (iOpcion)
            {
                case 1:
                    if (listaParametros.Count != 5)
                        throw new Exception("* Vericar parametros");
                    try
                    {

                        decimal subtotal, iva, total;


                        subtotal = Convert.ToDecimal(listaParametros[0]);
                        iva = Convert.ToDecimal(listaParametros[1]);
                        total = Convert.ToDecimal(listaParametros[2]);
                        subtotal = Math.Truncate(subtotal * 100) / 100;
                        iva = Math.Truncate(iva * 100) / 100;
                        total = Math.Truncate(total * 100) / 100;

                        int? intNumNota = null;
                        context.Parametros.Clear();
                        context.Parametros.Add(new SqlParameter("@Opcion", iOpcion));
                        context.Parametros.Add(new SqlParameter("@Numero_Empresa", Convert.ToInt32(listaParametros[4])));
                        context.Parametros.Add(new SqlParameter("@Numero", Convert.ToInt32(listaParametros[3])));
                        context.Parametros.Add(new SqlParameter("@Subtotal", subtotal));
                        context.Parametros.Add(new SqlParameter("@IVA", iva));
                        context.Parametros.Add(new SqlParameter("@Total", total));
                        context.Parametros.Add(new SqlParameter("@Num_NotaCredito", intNumNota.HasValue ? (object)intNumNota.Value : DBNull.Value));

                        context.ExecuteProcedure("sp_DevolucionActualizaCliente", true);
                        iRespuesta = 1;
                    }
                    catch {
                        return iRespuesta;
                    }
                    break;
                case 2:
                    if (listaParametros.Count != 3)
                        throw new Exception("* Parametros insuficientes");
                    try
                    {
                        decimal? dSubtotal = null;
                        decimal? dIva = null;
                        decimal? dTotal = null;
                        context.Parametros.Clear();
                        context.Parametros.Add(new SqlParameter("@Opcion", iOpcion));
                        context.Parametros.Add(new SqlParameter("@Numero_Empresa", Convert.ToInt32(listaParametros[2])));
                        context.Parametros.Add(new SqlParameter("@Numero", Convert.ToInt32(listaParametros[1])));
                        context.Parametros.Add(new SqlParameter("@Subtotal", dSubtotal.HasValue ? (object)dSubtotal.Value : DBNull.Value));
                        context.Parametros.Add(new SqlParameter("@IVA", dIva.HasValue ? (object)dIva.Value : DBNull.Value));
                        context.Parametros.Add(new SqlParameter("@Total", dTotal.HasValue ? (object)dTotal.Value : DBNull.Value));
                        context.Parametros.Add(new SqlParameter("@Num_NotaCredito", Convert.ToInt32(listaParametros[0])));

                        context.ExecuteProcedure("sp_DevolucionActualizaCliente", true);
                        iRespuesta = 1;
                    }
                    catch 
                    {
                        return iRespuesta;
                    }

                    break;
                default:
                    break;
            }
            listaParametros.Clear();
            return iRespuesta;

        }

        public static DataTable CreateAnticipo(int iMoviment, int iNumber, int iCompany, int iClient, int iNumberConcept, int iFolio, int? iReferenceNumber, DateTime Date,
                            decimal dTotal, decimal? dAbono, decimal? dCargo, decimal? dSaldo, int? iNumFactura, int? iuser, int iCurrency, int? iNumberBank, int? iAccount,
                            int? iTipoDocto, int? iTipoDoctoRelacion, int? iNumberCaja, int iCIva, decimal? dTipoCambio, int? iMovimentBank)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("TipoMovimiento", iMoviment));
            context.Parametros.Add(new SqlParameter("Numero", iNumber));
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iCompany));
            context.Parametros.Add(new SqlParameter("Numero_Cliente", iClient));
            context.Parametros.Add(new SqlParameter("Numero_Concepto", iNumberConcept));
            context.Parametros.Add(new SqlParameter("Documento", iFolio));
            context.Parametros.Add(new SqlParameter("Numero_Referencia", iReferenceNumber.HasValue ? (object)iReferenceNumber.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Fecha_Aplicacion", Date));
            context.Parametros.Add(new SqlParameter("Fecha_Vencimiento", Date));
            context.Parametros.Add(new SqlParameter("Monto", dTotal));
            context.Parametros.Add(new SqlParameter("Cargo", dCargo.HasValue ? (object)dCargo.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Abono", dAbono.HasValue ? (object)dAbono.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Saldo", dSaldo.HasValue ? (object)dSaldo.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Numero_Factura", iNumFactura.HasValue ? (object)iNumFactura.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Usuario", iuser.HasValue ? (object)iuser.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Moneda", iCurrency));
            context.Parametros.Add(new SqlParameter("Num_Banco", iNumberBank.HasValue ? (object)iNumberBank.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Num_Cuenta", iAccount.HasValue ? (object)iAccount.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Clas_Tipo_Documento", iTipoDocto.HasValue ? (object)iTipoDocto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Tipo_Documento_CC", iTipoDoctoRelacion.HasValue ? (object)iTipoDoctoRelacion.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Num_Caja", iNumberCaja.HasValue ? (object)iNumberCaja.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Clas_IVA", iCIva));
            context.Parametros.Add(new SqlParameter("Tipo_Cambio", dTipoCambio.HasValue ? (object)dTipoCambio.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Numero_Movimiento_Banco", iMovimentBank.HasValue ? (object)iMovimentBank.Value : DBNull.Value));
            dt = context.ExecuteProcedure("[sp_Cuentas_Cobrar_Anticipos_v2]", true).Copy();
            return dt;
        }

        public static DataTable CreateContabilidad(int iMovimiento, int iEmpresa, int iNumberoTransaccion, string sAuxiliar, int iTipoPoliza, decimal dTotal,
                                        int iUsuario, decimal? dNumeroManual, int iMoneda, DateTime Fecha, string sConcepto, decimal? dTipoCambio, decimal? dTipoCambioHistorico,
                                        decimal? dTipoCambioOriginal, string sFolioFiscal)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("TipoMovimiento", iMovimiento));
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Numero_Transaccion", iNumberoTransaccion));
            context.Parametros.Add(new SqlParameter("Auxiliar", sAuxiliar));
            context.Parametros.Add(new SqlParameter("Tipo_Poliza", iTipoPoliza));
            context.Parametros.Add(new SqlParameter("Cantidad", dTotal));
            context.Parametros.Add(new SqlParameter("Numero_Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("Numero_Manual", dNumeroManual.HasValue ? (object)dNumeroManual.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Clas_Moneda_Destino", iMoneda));
            context.Parametros.Add(new SqlParameter("Fecha_Emision", Fecha));
            context.Parametros.Add(new SqlParameter("Concepto_TMP", sConcepto));
            context.Parametros.Add(new SqlParameter("Tipo_Cambio", dTipoCambio.HasValue ? (object)dTipoCambio.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Tipo_Cambio_Historico", dTipoCambioHistorico.HasValue ? (object)dTipoCambioHistorico.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Tipo_Cambio_Original", dTipoCambioOriginal.HasValue ? (object)dTipoCambioOriginal.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Folio_Fiscal", sFolioFiscal));

            dt = context.ExecuteProcedure("sp_Contabilidad_Automatica_V2", true).Copy();
            return dt;

        }

        public static DataTable CreateContabilidadDevolucionReversa(int iMovimiento, int iEmpresa, int iNumeroTransaccion, string sAuxiliar, int iTipoPoliza, decimal dTotal,
                                         int iUsuario, decimal? dNumeroManual, int iMoneda, DateTime Fecha, string sConcepto,int iNumeroEntrada,int iNumeroAlmacen,
                                         int iNumeroNota, int iTipoNota,int iNumeroDevolucion,int iGeneraEntrada,int iGeneraNota,int iGeneraMovimiento,
                                         int iCliente,int iClas_Iva,string sFolioFiscal)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("TipoMovimiento", iMovimiento));
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Numero_Transaccion", iNumeroTransaccion));
            context.Parametros.Add(new SqlParameter("Auxiliar", sAuxiliar));
            context.Parametros.Add(new SqlParameter("Tipo_Poliza", iTipoPoliza));
            context.Parametros.Add(new SqlParameter("Cantidad", dTotal));
            context.Parametros.Add(new SqlParameter("Numero_Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("Numero_Manual", dNumeroManual.HasValue ? (object)dNumeroManual.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Clas_Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("Fecha_Emision", Fecha));
            context.Parametros.Add(new SqlParameter("Concepto_TMP", sConcepto));
            context.Parametros.Add(new SqlParameter("Numero_Entrada", iNumeroEntrada));
            context.Parametros.Add(new SqlParameter("Numero_Almacen", iNumeroAlmacen));
            context.Parametros.Add(new SqlParameter("Numero_Nota", iNumeroNota));
            context.Parametros.Add(new SqlParameter("Tipo_Nota", iTipoNota));
            context.Parametros.Add(new SqlParameter("Numero_Devolucion", iNumeroDevolucion));
            context.Parametros.Add(new SqlParameter("Genera_Entrada", iGeneraEntrada));
            context.Parametros.Add(new SqlParameter("Genera_Nota", iGeneraNota));
            context.Parametros.Add(new SqlParameter("Genera_Movimiento", iGeneraMovimiento));
            context.Parametros.Add(new SqlParameter("Numero_Cliente", iCliente));
            context.Parametros.Add(new SqlParameter("Total", dTotal));
            context.Parametros.Add(new SqlParameter("Fecha", Fecha));
            context.Parametros.Add(new SqlParameter("Clas_Iva", iClas_Iva));
            context.Parametros.Add(new SqlParameter("Folio_Fiscal", sFolioFiscal));

            dt = context.ExecuteProcedure("sp_Contabilidad_Devolucion_Automatica_Reversa", true).Copy();
            return dt;

        }


        public static DataTable ObtenNumeroTransaccion(int iEmpresa, int iConcepto, int iNumeroPrograma)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("concept", iConcepto));
            context.Parametros.Add(new SqlParameter("programNumber", iNumeroPrograma));
            dt = context.ExecuteProcedure("sp_Devoluciones_ObtieneNumeroDeTransaccion", true).Copy();
            return dt;

        }


        public static DataTable ObtenNumeroTransaccionContable(int iEmpresa, int iTransaccion, int iNumeroPrograma)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("transaccion ", iTransaccion));
            context.Parametros.Add(new SqlParameter("programNumber", iNumeroPrograma));
            dt = context.ExecuteProcedure("sp_Devoluciones_ObtieneNumeroDeTransaccionContable", true).Copy();
            return dt;

        }



        public static DataTable ObtenNumeroTransaccion2(int iEmpresa, int iConcepto)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("clasnumero", iConcepto));
            dt = context.ExecuteProcedure("sp_Devolucion_ObtieneNumeroDeTransaccion", true).Copy();
            return dt;

        }




        public static DataTable ObtenPrefijoNota(int iEmpresa, int iNumeroNota)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("number", iNumeroNota));
            dt = context.ExecuteProcedure("sp_NotaDeCredito_ObtienePrefijoDeNota_v1", true).Copy();
            return dt;
        }


        public static DataTable ObtenClasAlmacen(int iEmpresa, int iAlmacen)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("numeroalmacen", iAlmacen));

            dt = context.ExecuteProcedure("sp_Devoluciones_Clas_Almacen", true).Copy();
            return dt;

        }
        public static DataTable ObtenClasCliente(int iEmpresa, int iPersona)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("numeropersona", iPersona));

            dt = context.ExecuteProcedure("sp_Devoluciones_Clas_Cliente", true).Copy();
            return dt;

        }



        public static DataTable ObtenEstatusACancelar(int iEmpresa, int iNumeroFactura)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Numero_Factura", iNumeroFactura));

            dt = context.ExecuteProcedure("sp_Devolucion_EstatusACancelar_v2", true).Copy();//probando sp, estaba el sp_Devolucion_EstatusACancelar
            return dt;

        }


        public static DataTable RegistroBancos(int iMovimiento, int iEmpresa, int iNumero, int iNumeroBanco, string sCuenta, DateTime Fecha, string sFolio, decimal dEfectivo, decimal? dDocumentos,
                                               int iClasConcepto, int iBeneficiario, string sComentario, int iTipo, decimal? dSaldoInicial, decimal? dSaldoFinal, int iClasCuenta, int iClasFacultad,
                                               int iUsuarioReg, int? iNumeroTransaccion, string sAuxiliar, int iUsuario, int? iRetiroChequeTransafer, decimal? dTipoCambio, DateTime? FechaCancelacion, string sCausa)
        {


            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("TipoMovimiento", iMovimiento));
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Numero", iNumero));
            context.Parametros.Add(new SqlParameter("Numero_Banco", iNumeroBanco));
            context.Parametros.Add(new SqlParameter("Cuenta", sCuenta));
            context.Parametros.Add(new SqlParameter("Fecha", Fecha));
            context.Parametros.Add(new SqlParameter("Folio", sFolio));
            context.Parametros.Add(new SqlParameter("Efectivo", dEfectivo));
            context.Parametros.Add(new SqlParameter("Documentos", dDocumentos));
            context.Parametros.Add(new SqlParameter("Clas_Concepto", iClasConcepto));
            context.Parametros.Add(new SqlParameter("Beneficiario", iBeneficiario));
            context.Parametros.Add(new SqlParameter("Comentario", sComentario));
            context.Parametros.Add(new SqlParameter("Tipo", iTipo));
            context.Parametros.Add(new SqlParameter("Saldo_Inicial", dSaldoInicial));
            context.Parametros.Add(new SqlParameter("Saldo_Final", dSaldoFinal));
            context.Parametros.Add(new SqlParameter("Clas_Cuenta", iClasCuenta));
            context.Parametros.Add(new SqlParameter("Clas_Facultad", iClasFacultad));
            context.Parametros.Add(new SqlParameter("Usuario_Reg", iUsuario));
            context.Parametros.Add(new SqlParameter("Numero_Transaccion", iNumeroTransaccion));
            context.Parametros.Add(new SqlParameter("Auxiliar", sAuxiliar));
            context.Parametros.Add(new SqlParameter("Numero_Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("Retiro_Cheque_Transafer", iRetiroChequeTransafer));
            context.Parametros.Add(new SqlParameter("Tipo_Cambio", dTipoCambio));
            context.Parametros.Add(new SqlParameter("Fecha_Cancelacion", FechaCancelacion));
            context.Parametros.Add(new SqlParameter("Causa", sCausa));


            dt = context.ExecuteProcedure("sp_Bancos_Registro_Movimiento_v2", true).Copy();
            return dt;

        }
        public static DataTable gfdt_GetPartidasGeneral(int numeroregistro, int contabilizada, int documentType, int documetInternet, int companynumber, int type = 1)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroregistro", numeroregistro));
            context.Parametros.Add(new SqlParameter("@contabilizada", contabilizada));
            context.Parametros.Add(new SqlParameter("@documentType", documentType));
            context.Parametros.Add(new SqlParameter("@documetInternet", documetInternet));
            context.Parametros.Add(new SqlParameter("@companynumber", companynumber));
            context.Parametros.Add(new SqlParameter("@type", type));
            dt = context.ExecuteProcedure("sp_CFDI_ObtienePartidasGeneral_v1", true).Copy();
            return dt;
        }
        public static DataTable ConsultaNotaEncabezado(int iNumero)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero", iNumero));

            dt = context.ExecuteProcedure("sp_Consulta_Nota_Credito", true).Copy();
            return dt;
        }


        public static DataTable ObtenConceptosDevolucionCliente(int iEmpresa, int iNumeroConcepto)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("NumeroEmpresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("NumeroConcepto", iNumeroConcepto));

            dt = context.ExecuteProcedure("sp_DevolucionesConceptos", true).Copy();
            return dt;

        }
        public static DataTable ObtieneConceptoDevolucion(int iempresa)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numeroEmpresa", iempresa));
            dt = context.ExecuteProcedure("[sp_Devolucion_ObtieneNumeroConcepto]", true).Copy();

            return dt;
        }
        public static DataTable ObtenFolioFiscal(int iEmpresa, int iNumeroFactura,int iNumeroCliente)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("NumeroEmpresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("NumeroFactura", iNumeroFactura));
            context.Parametros.Add(new SqlParameter("NumeroCliente", iNumeroCliente));

            dt = context.ExecuteProcedure("sp_FolioFactura", true).Copy();
            return dt;

        }




    }
}
