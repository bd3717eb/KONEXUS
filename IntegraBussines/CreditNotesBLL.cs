using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;

namespace IntegraBussines
{
    public class CreditNotesBLL
    {
        public int iCompany { get; set; }
        public int? iAuxiliar { get; set; }
        public int iAux { get; set; }
        public int iTipoPoliza { get; set; }
        public decimal? dAbono { get; set; }
        public int iPeriodo { get; set; }
        public int iAnio { get; set; }
        public int iValor { get; set; }
        public int iClient { get; set; }
        public int iPerson { get; set; }
        public int iNumNota { get; set; }
        public int iNumber { get; set; }
        public int? iNumberManual { get; set; }
        public int iFolio { get; set; }
        public int iTipoTransaccion { get; set; }
        public int iNumberTransaccion { get; set; }
        public int iInvoiceNumber { get; set; }
        public string sDocumento { get; set; }
        public int iConcept { get; set; }
        public decimal dIva { get; set; }
        public DateTime Date { get; set; }
        public int iNumberFolio { get; set; }
        public int iTipoConceptoImporte { get; set; }
        public int iCIva { get; set; }
        public int iTipoDoctoNota { get; set; }
        public int iTipoDoctoNotaRelacion { get; set; }
        public int iNumberNote { get; set; }
        public int iCCurrency { get; set; }
        public int iCConcepto { get; set; }
        public string SConcept { get; set; }
        public string sObservations { get; set; }
        public string sNameClient { get; set; }
        public decimal dSubtotal { get; set; }
        public int iDescuento { get; set; }
        public decimal dTotal { get; set; }
        public string sImporteLetra { get; set; }
        public decimal dTipoCambio { get; set; }
        public decimal? dCantidad { get; set; }
        public int iNPersonaAplicada { get; set; }
        public int? iReferenceNumber { get; set; }
        public int iCEstatus { get; set; }
        public int? iTipoDocto { get; set; }
        public int? iTipoDoctoRelacion { get; set; }
        public int? iNumeroRegDocto { get; set; }
        public int? iNumeroDocto { get; set; }
        public int? iNumeroFacturaRelacion { get; set; }
        public int? iNumeroCXC { get; set; }
        public int? iUser { get; set; }
        public int iCSucursal { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public string sCausa { get; set; }
        public int iUserCaptura { get; set; }
        public int iRelacionFactura { get; set; }
        public int? iCargo { get; set; }
        public decimal? dSaldo { get; set; }
        public int? iNumFactura { get; set; }
        public int? iNumberCaja { get; set; }
        public int iCurrency { get; set; }
        public int? iNumberBank { get; set; }
        public int? iAccount { get; set; }
        public int? iMovimentBank { get; set; }
        public int? iCredito { get; set; }
        public int? iConceptoCancel { get; set; }
        public int? iUserCancel { get; set; }
        public int iNumberCot { get; set; }
        public int iNumberCotDet { get; set; }
        public int? iSecuencia { get; set; }

        public int GetFolio(int icompany, int idocument, int iserie)
        {
            IntegraData.CreditNotesDAL CreditNotesDL = new IntegraData.CreditNotesDAL();
            DataTable table = new DataTable();

            try
            {
                using (table = CreditNotesDL.GetFolio(icompany, idocument, iserie))
                {
                    if (table.Rows.Count > 0)
                    {
                        if (table.Rows[0][0] != DBNull.Value)
                        {
                            return Convert.ToInt32(table.Rows[0][0]);
                        }
                    }
                }
            }
            catch
            {
                return 0;
            }
            finally
            {
                CreditNotesDL = null;
            }

            return 0;
        }



    }
}
