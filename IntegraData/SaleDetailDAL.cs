using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace IntegraData
{
    public class SaleDetailDAL
    {
        public bool SaveSaleDetail(int iMoviment, int iCompany, int iSaleNumber, int iNumber, int iProduct, int iTConcept, int iConcept, decimal? dWidth, decimal? dHigh,
                                   decimal dQuantity, decimal dPrice, decimal? dTo, decimal dAmount, string sSection, int iOrderNumber, int iActive, int iStatusOrder,
                                   int iMeasuringUnit, decimal? dMeasuringUnitQty, int iMeasuringUnitFam, int iProductConceptFam, string sCharacteristics, DateTime DeliveryDate,
                                   int? iZone, decimal? dReferenceCost)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("iType", iMoviment),
                new SqlParameter("iEmpresa", iCompany),
                new SqlParameter("iCotiza", iSaleNumber),
                new SqlParameter("iNumero", iNumber),
                new SqlParameter("iProducto", iProduct),
                new SqlParameter("iTipo_Concepto", iTConcept),
                new SqlParameter("iConcepto", iConcept),
                new SqlParameter("dAncho", dWidth.HasValue ? (object)dHigh.Value : DBNull.Value),
                new SqlParameter("dAlto", dHigh.HasValue ? (object)dWidth.Value : DBNull.Value),
                new SqlParameter("dCantidad", dQuantity),
                new SqlParameter("dPrecio", dPrice),
                new SqlParameter("dHasta", dTo.HasValue ? (object)dTo.Value : DBNull.Value),
                new SqlParameter("dImporte", dAmount),
                new SqlParameter("vSeccion", sSection != null ? (object)sSection : DBNull.Value),
                new SqlParameter("iNumero_Orden", iOrderNumber),
                new SqlParameter("Activo", iActive),
                new SqlParameter("EStatus_Orden", iStatusOrder),
                new SqlParameter("Unidad_Medida", iMeasuringUnit),
                new SqlParameter("Cantidad_Unidad_Medida", dMeasuringUnitQty.HasValue ? (object)dMeasuringUnitQty.Value : DBNull.Value),
                new SqlParameter("Familia_Unidad_Medida", iMeasuringUnitFam),
                new SqlParameter("Familia_Producto_Tipo_Concepto_Concepto", iProductConceptFam),
                new SqlParameter("Caracteristicas", sCharacteristics != null ? (object)sCharacteristics : DBNull.Value),
                new SqlParameter("Fecha_Entrega_Partida", DeliveryDate),
                new SqlParameter("Clas_Zona", iZone.HasValue ? (object)iZone.Value : DBNull.Value),
                new SqlParameter("Costo_Referencia", dReferenceCost.HasValue ? (object)dReferenceCost.Value : DBNull.Value),               
            };

            return sqlDBHelper.ExecuteNonQuery("sp_Cotizacion_Detalle", System.Data.CommandType.StoredProcedure, parameters);
        }

        public bool SaveSaleDetailFacturaLibre(int iMoviment, int iCompany, int iSaleNumber, int iNumber, int TProduct, int Product, int TConcept, int Concept, string sCharacteristics,
                                             int iMeasuringUnit, decimal dQuantity, decimal Price, double iIVA, int iClasIVA, int MeasuringUnitFam, DateTime DeliveryDate, int? NotaCargo, 
                                             decimal? TotalCargo,decimal? dTotalPartida)
      
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
    }
}
