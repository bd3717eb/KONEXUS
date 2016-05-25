using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace IntegraData
{
    public class SaleDAL
    {
        public int SaveSale(int iMoviment, int iCompany, int iNumber, int iStatus, DateTime Date, int iCustomer, int iUser, int? iSeller,
                             int iResponsible, string sProject, int iCurrency, DateTime DeliveryDate, int? iContact, string sObservations,
                             int iPayNumber, int iCPayPeriod, int? iDocument, DateTime ExpirationDate, int iOrderNumber, int iCTaxes,
                             int? iDocumentRelation, int iCurrencyInvoice, int iConceptNumber, int? iReferenceNumber, int iAddressNumber,
                             int iCredit, int iCOffice, int iDirectDelivery, int? iCNumber, int? iCStore)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("iType", iMoviment),
                new SqlParameter("iEmpresa", iCompany),
                new SqlParameter("iNumero", iNumber),
                new SqlParameter("iEstatus", iStatus),
                new SqlParameter("dFecha", Date),
                new SqlParameter("iCliente", iCustomer),
                new SqlParameter("iUsuario", iUser),
                new SqlParameter("iVendedor", iSeller.HasValue ? (object)iSeller.Value : DBNull.Value),
                new SqlParameter("iResponsable", iResponsible),
                new SqlParameter("vNombre_Proyecto", sProject != "" ? (object)sProject : DBNull.Value),
                new SqlParameter("iMoneda", iCurrency),
                new SqlParameter("dFecha_Entrega", DeliveryDate),
                new SqlParameter("iContacto", iContact.HasValue ? (object)iContact.Value : DBNull.Value),
                new SqlParameter("vObservaciones", sObservations != "" ? (object)sObservations : DBNull.Value),
                new SqlParameter("iNumero_Pago", iPayNumber),
                new SqlParameter("iClas_Periodo_Pago", iCPayPeriod),
                new SqlParameter("iTipoDocto", iDocument.HasValue ? (object)iDocument.Value : DBNull.Value),
                new SqlParameter("Fecha_Vencimiento", ExpirationDate),
                new SqlParameter("iNumero_Orden", iOrderNumber),
                new SqlParameter("Clas_IVA", iCTaxes),
                new SqlParameter("Relacion_Tipo_Documento", iDocumentRelation.HasValue ? (object)iDocumentRelation.Value : DBNull.Value),
                new SqlParameter("Moneda_Factura", iCurrencyInvoice),
                new SqlParameter("Observaciones2", DBNull.Value),
                new SqlParameter("Num_Concepto", iConceptNumber),
                new SqlParameter("Num_Referencia", iReferenceNumber.HasValue ? (object)iReferenceNumber.Value : DBNull.Value),
                new SqlParameter("Numero_Domicilio", iAddressNumber),
                new SqlParameter("Credito", iCredit),
                new SqlParameter("Clas_Sucursal", iCOffice),
                new SqlParameter("Entrega_Directa", iDirectDelivery),
                new SqlParameter("Numero_Cotiza", iCNumber.HasValue ? (object)iCNumber.Value : DBNull.Value),
                new SqlParameter("Clas_Almacen_Cot", iCStore.HasValue ? (object)iCStore.Value : DBNull.Value),               
            };

            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    if (param.ParameterName == "iNumero" && iNumber == 0)
                    {
                        param.Direction = ParameterDirection.Output;
                    }
                }
            }

            return sqlDBHelper.ExecuteNonQueryOutputValue("sp_cotizacion", "iNumero", CommandType.StoredProcedure, parameters);
        }

        public DataTable GetPaymenttypes(int icompany, string stype)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
			    new SqlParameter("companynumber", icompany),
			    new SqlParameter("tipo", stype),
		    };
            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_Venta_ObtieneTiposDePago_v1", CommandType.StoredProcedure, parameters);
        }


        public DataTable GetStatusNumberFromDescription(string sdescription, int ifamily, int ifather)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {      
                new SqlParameter("description", sdescription),
			    new SqlParameter("family", ifamily),
			    new SqlParameter("father", ifather),
		    };
            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_Venta_ObtieneNumeroDeEstatusDeDescripcion_v1", CommandType.StoredProcedure, parameters);
        }


        public DataTable GetStatusNumberAndDescFromSaleNumber(int icompany, int isalenumber)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {      
                new SqlParameter("companynumber", icompany),
                new SqlParameter("salenumber", isalenumber),
		    };
            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_Venta_ObtieneEstatusDelNumeroDeVenta_v1", CommandType.StoredProcedure, parameters);
        }


        public DataTable IsProduct(int icompany, int iproduct)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
			    new SqlParameter("companynumber", icompany),
                new SqlParameter("product", iproduct),
		    };
            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_Venta_EsProducto_v1", CommandType.StoredProcedure, parameters);
        }






        public DataTable GetStockStore(int icompany, int istore, int iproduct, int iconcept, int ifamily)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
			    new SqlParameter("companynumber", icompany),
                new SqlParameter("store", istore),
                new SqlParameter("product", iproduct),
                new SqlParameter("concept", iconcept),
                new SqlParameter("family", ifamily),
		    };
            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_Venta_ObtieneStockDeLaTienda_v1", CommandType.StoredProcedure, parameters);
        }


        public bool AwayProducts(int imoviment, int icompany, int isalenumber)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Numero_Empresa", icompany),
                new SqlParameter("Numero_Cotiza", isalenumber),
                new SqlParameter("Tipo", imoviment),
            };

            return sqlDBHelper.ExecuteNonQuery("sp_Cotizacion_Aparta_Articulos", CommandType.StoredProcedure, parameters);
        }

        public bool StoreDetailsTMPTable(int imoviment, int icompany, int isalenumber, int igamesale, int istore, decimal dquantity, int iuser)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Tipo", imoviment),
                new SqlParameter("Numero_Empresa", icompany),
                new SqlParameter("Numero_Cotiza", isalenumber),
                new SqlParameter("Partida", igamesale),
                new SqlParameter("Clas_Almacen", istore),
                new SqlParameter("Cantidad", dquantity),
                new SqlParameter("Usuario", iuser),
            };

            return sqlDBHelper.ExecuteNonQuery("sp_Cotizacion_Detalle_Almacen_TMP", CommandType.StoredProcedure, parameters);
        }


        public DataTable GetFhaterNumberFromChildNumber(int icompany, int ichildnumber, int ifamily)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
			    new SqlParameter("companynumber", icompany),                
                new SqlParameter("childnumber", ichildnumber),
                new SqlParameter("family", ifamily),
		    };
            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_Venta_ObtieneNumeroDePadrePorNumeroDeHijo_v1", CommandType.StoredProcedure, parameters);
        }


        public bool StoreExe(string scadena)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("comando", scadena),
              
            };

            return sqlDBHelper.ExecuteNonQuery("Sp_ImpresionCFD", CommandType.StoredProcedure, parameters);
        }


        public int SpFacturaLibre(int iMoviment, int iCompany, int iNumber, int iCustomer, DateTime Date, int iDocumento, int iSerie, int iNumeroDocto,
                                 string sReference, int iCurrency, decimal TypeChange, int iconceptnumber, int iuser, int icredit, string sobservaciones,
                                 int? inotacargo, int? iclaspagodato, string SPayData, int Clas_Sucursal,decimal? dSub, decimal? dIva, decimal? dTotal)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Tipo_Movimiento", iMoviment),
                new SqlParameter("Numero_Empresa", iCompany),
                new SqlParameter("Numero", iNumber),
                new SqlParameter("Cliente", iCustomer),
                new SqlParameter("Fecha_Emision", Date),
                new SqlParameter("Fecha_Vencimiento", Date),
                new SqlParameter("Tipo_Docto", iDocumento),
                new SqlParameter("Serie", iSerie),
                new SqlParameter("Numero_Docto", iNumeroDocto),
                new SqlParameter("Referencia", sReference),    
                new SqlParameter("Clas_Moneda", iCurrency),
                new SqlParameter("TC", TypeChange),
                new SqlParameter("Concepto_Factura", iconceptnumber),
                new SqlParameter("Usuario", iuser),
                new SqlParameter("Credito", icredit),
                new SqlParameter("Observaciones", sobservaciones),
                new SqlParameter("Referencia2", sReference),
                new SqlParameter("Referencia3", sReference),
                new SqlParameter("Nota_Cargo", inotacargo.HasValue ? (object)inotacargo.Value : DBNull.Value),
                new SqlParameter("Clas_Dato_Pago", iclaspagodato.HasValue ? (object)iclaspagodato.Value : DBNull.Value),
                new SqlParameter("Dato_Pago",  SPayData ),
                new SqlParameter("Clas_Sucursal",  Clas_Sucursal ),

                new SqlParameter("Sub", dSub.HasValue ? (object)dSub.Value : DBNull.Value),
                new SqlParameter("Iva", dIva.HasValue ? (object)dIva.Value : DBNull.Value),
                new SqlParameter("Total", dTotal.HasValue ? (object)dTotal.Value : DBNull.Value),

            };

            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    if (param.ParameterName == "Numero" && iNumber == 0)
                    {
                        param.Direction = ParameterDirection.Output;
                    }
                }
            }

            return sqlDBHelper.ExecuteNonQueryOutputValue("sp_factura_libre", "Numero", CommandType.StoredProcedure, parameters);
        }

        public int spFacturaContabilidadCC(int iCompany, int iNumber, int iUser)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Numero_Empresa", iCompany),
                new SqlParameter("Numero", iNumber),
                new SqlParameter("Usuario", iUser),
            };

            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    if (param.ParameterName == "Numero" && iNumber == 0)
                    {
                        param.Direction = ParameterDirection.Output;
                    }
                }
            }
            return sqlDBHelper.ExecuteNonQueryOutputValue("sp_Factura_Contabilidad_CC", "Numero", CommandType.StoredProcedure, parameters);
        }

        public bool SaveSaleDetailFacturaLibre(int iMoviment, int iCompany, int iSaleNumber, int iNumber, int TProduct, int Product, int TConcept, int Concept, string sCharacteristics,
                                             int iMeasuringUnit, decimal dQuantity, decimal Price, double iIVA, int iClasIVA, int MeasuringUnitFam, DateTime DeliveryDate, 
                                             int? NotaCargo, decimal? TotalCargo, decimal? dTotalPartida)
       
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Tipo_Movimiento", iMoviment),
                new SqlParameter("Numero_Empresa", iCompany),
                new SqlParameter("Numero_Factura_Libre", iSaleNumber),
                new SqlParameter("Partida", iNumber),
                new SqlParameter("Clas_TProducto", TProduct),
                new SqlParameter("Clas_Producto", Product),
                new SqlParameter("Clas_TConcepto", TConcept),
                new SqlParameter("Clas_Concepto", Concept),
                new SqlParameter("Caracteristicas", sCharacteristics!= null ? (object)sCharacteristics : DBNull.Value),
                new SqlParameter("Clas_UM", iMeasuringUnit),
                new SqlParameter("Cantidad", dQuantity),
                new SqlParameter("Precio", Price),
                new SqlParameter("Iva", iIVA),
                new SqlParameter("Clas_Iva", iClasIVA),
                new SqlParameter("Familia_UM", MeasuringUnitFam),
                new SqlParameter("Fecha_Entrega", DeliveryDate),
                new SqlParameter("Nota_Cargo",  NotaCargo.HasValue ? (object)NotaCargo.Value : DBNull.Value),
                new SqlParameter("Total_Cargo", TotalCargo.HasValue ? (object)TotalCargo.Value : DBNull.Value),
                new SqlParameter("Importe1", dTotalPartida.HasValue ? (object)dTotalPartida.Value : DBNull.Value),
            };
            return sqlDBHelper.ExecuteNonQuery("sp_Factura_Libre_Detalle", System.Data.CommandType.StoredProcedure, parameters);

        }

        #region Facturacion

        public DataTable GetDatosCorreoCliente(int icliente)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
               new SqlParameter("cliente", icliente),
		    };
            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_Venta_ObtieneDatosDeCorreo", CommandType.StoredProcedure, parameters);
        }

        public bool StoreCopiado(string scadena)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("comando", scadena),
              
            };

            return sqlDBHelper.ExecuteNonQuery("Sp_CopiadoArchivosCFDI", CommandType.StoredProcedure, parameters);
        }

        #endregion

    }
}
