using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegraBussines
{
    public class SaleDetailBLL
    {
        public int Company { get; set; }
        public int SaleNumber { get; set; }
        public int Number { get; set; }
        public int Product { get; set; }
        public int TConcept { get; set; }
        public int Concept { get; set; }
        public decimal? Width { get; set; }
        public decimal? High { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? To { get; set; }
        public decimal Amount { get; set; }
        public string Section { get; set; }
        public int OrderNumber { get; set; }
        public int Active { get; set; }
        public int StatusOrder { get; set; }
        public int MeasuringUnit { get; set; }
        public decimal? MeasuringUnitQty { get; set; }
        public int MeasuringUnitFam { get; set; }
        public int ProductConceptFam { get; set; }
        public string Characteristics { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int? Zone { get; set; }
        public decimal? ReferenceCost { get; set; }
        #region Facturacion
        public int TProduct { get; set; }
        public double IVA { get; set; }
        public int ClasIVA { get; set; }
        public int? NotaCargo { get; set; }
        public decimal? TotalCargo { get; set; }

        public decimal? dSub { get; set; }
        public decimal? dIva { get; set; }
        public decimal? dTotal { get; set; }
        public decimal? dTotalPartida { get; set; }

        #endregion
        public bool SaveDetail(int Moviment)
        {
            IntegraData.SaleDetailDAL SaleDetail = new IntegraData.SaleDetailDAL();
            return SaleDetail.SaveSaleDetail(Moviment, Company, SaleNumber, Number, Product, TConcept, Concept, High, Width,
                                             Quantity, Price, To, Amount, Section, OrderNumber, Active, StatusOrder, MeasuringUnit,
                                             MeasuringUnitQty, MeasuringUnitFam, ProductConceptFam, Characteristics, DeliveryDate,
                                             Zone, ReferenceCost);
        }

        public bool SaveDetailFacturaLibre(int Moviment)
        {
            IntegraData.SaleDetailDAL SaleDetail = new IntegraData.SaleDetailDAL();
            // eblaher
            return SaleDetail.SaveSaleDetailFacturaLibre(Moviment, Company, SaleNumber, Number, TProduct, Product, TConcept, Concept, Characteristics,
                                             MeasuringUnit, Quantity, Price, IVA, ClasIVA, MeasuringUnitFam, DeliveryDate, NotaCargo, TotalCargo, dTotalPartida);

      




        }
    }
}
