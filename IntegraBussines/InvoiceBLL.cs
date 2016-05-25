using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;
using System.Data.SqlClient;
using System.IO;


namespace IntegraBussines
{
    public class InvoiceBLL
    {

        public int? Person { get; set; }
        public int Client { get; set; }
        public int iCompany { get; set; }
        public int iInvoiceNumber { get; set; }
        public int? iConceptNumber { get; set; }
        public int? iCurrency { get; set; }
        public int iNumberTransaction { get; set; }
        public int iNumber { get; set; }
        public int? iNumberAux { get; set; }
        public int iNumberCta { get; set; }
        public int iNumberProgram { get; set; }
        public int iPeriodo { get; set; }
        public int iType { get; set; }
        public decimal? dExchange { get; set; }
        public int? iNumberPoliza { get; set; }
        public int iNumberFolio { get; set; }
        public string sName { get; set; }
        public string sAux { get; set; }
        public int? iAuxi { get; set; }
        public double dAuxd { get; set; }
        public decimal? dDebe { get; set; }
        public decimal? dHaber { get; set; }
        public decimal? dIngreso { get; set; }
        public decimal? dEgreso { get; set; }
        public DateTime Date { get; set; }

        public int CreateInvoice(int company, int salenumber, int document, int serie, int folio, DateTime date, int client)
        {

            IntegraData.InvoiceDAL InvoiceDAL = new IntegraData.InvoiceDAL();

            DataTable AddressTable;
            int itypeperson = 13;

            using (AddressTable = PersonBLL.ObtenDomicilioPersonaPorTipoDomicilio(client, itypeperson))
            {
                if (AddressTable != null && AddressTable.Rows.Count > 0)
                {
                    try
                    {

                        SQLConection context = new SQLConection();
                        context.Parametros.Clear();

                        context.Parametros.Add(new SqlParameter("@Numero_Empresa", company));
                        context.Parametros.Add(new SqlParameter("@Numero_Cotiza", salenumber));
                        context.Parametros.Add(new SqlParameter("@Tipo_Docto", document));
                        context.Parametros.Add(new SqlParameter("@Serie", serie));
                        context.Parametros.Add(new SqlParameter("@Numero_Documento", folio));
                        context.Parametros.Add(new SqlParameter("@Fecha", date));
                        context.Parametros.Add(new SqlParameter("@Num_Domicilio", Convert.ToInt32(AddressTable.Rows[0][0])));
                        context.Parametros.Add(new SqlParameter("@Calle", AddressTable.Rows[0][2].ToString()));
                        context.Parametros.Add(new SqlParameter("@Colonia", AddressTable.Rows[0][3].ToString()));
                        context.Parametros.Add(new SqlParameter("@CP", AddressTable.Rows[0][6].ToString()));
                        context.Parametros.Add(new SqlParameter("@Estado", AddressTable.Rows[0][5].ToString()));
                        context.Parametros.Add(new SqlParameter("@Delegacion", AddressTable.Rows[0][4].ToString()));
                        context.Parametros.Add(new SqlParameter("@Telefono", AddressTable.Rows[0][7].ToString()));
                        context.Parametros.Add(new SqlParameter("@Tipo_Domicilio", AddressTable.Rows[0][11].ToString()));

                        context.ExecuteProcedure("sp_Factura_Tienda_v2", true);
                        return 1;

                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        InvoiceDAL = null;
                    }
                }
                else
                {
                    return -1;
                }
            }

        }


        public int CreateInvoiceFacturasProgramadas(int company, int salenumber, int document, int serie, int folio, DateTime date, int client, int iParcialTotal,int iAnticipo)
        {

            IntegraData.InvoiceDAL InvoiceDAL = new IntegraData.InvoiceDAL();

            DataTable AddressTable;
            int itypeperson = 13;

            using (AddressTable = PersonBLL.ObtenDomicilioPersonaPorTipoDomicilio(client, itypeperson))
            {
                if (AddressTable != null && AddressTable.Rows.Count > 0)
                {
                    try
                    {

                        SQLConection context = new SQLConection();
                        context.Parametros.Clear();

                        context.Parametros.Add(new SqlParameter("@Numero_Empresa", company));
                        context.Parametros.Add(new SqlParameter("@Numero_Cotiza", salenumber));
                        context.Parametros.Add(new SqlParameter("@Tipo_Docto", document));
                        context.Parametros.Add(new SqlParameter("@Serie", serie));
                        context.Parametros.Add(new SqlParameter("@Numero_Documento", folio));
                        context.Parametros.Add(new SqlParameter("@Fecha", date));
                        context.Parametros.Add(new SqlParameter("@Num_Domicilio", Convert.ToInt32(AddressTable.Rows[0][0])));
                        context.Parametros.Add(new SqlParameter("@Calle", AddressTable.Rows[0][2].ToString()));
                        context.Parametros.Add(new SqlParameter("@Colonia", AddressTable.Rows[0][3].ToString()));
                        context.Parametros.Add(new SqlParameter("@CP", AddressTable.Rows[0][6].ToString()));
                        context.Parametros.Add(new SqlParameter("@Estado", AddressTable.Rows[0][5].ToString()));
                        context.Parametros.Add(new SqlParameter("@Delegacion", AddressTable.Rows[0][4].ToString()));
                        context.Parametros.Add(new SqlParameter("@Telefono", AddressTable.Rows[0][7].ToString()));
                        context.Parametros.Add(new SqlParameter("@Tipo_Domicilio", AddressTable.Rows[0][11].ToString()));
                        context.Parametros.Add(new SqlParameter("@Tipo_Parcial_Total", iParcialTotal));
                        context.Parametros.Add(new SqlParameter("@Anticipo", iAnticipo));

                        context.ExecuteProcedure("sp_Factura_Tienda_v2", true);
                        return 1;

                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        InvoiceDAL = null;
                    }
                }
                else
                {
                    return -1;
                }
            }

        }

        public string GetExepcion()
        {
            IntegraData.InvoiceDAL InvoiceDAL = new IntegraData.InvoiceDAL();
            return InvoiceDAL.RetornaError();
        }
       

        public int CreateContabilidad(int iMovimiento, int iEmpresa, int iPersona)
        {

            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@iMovimiento", iMovimiento));
                context.Parametros.Add(new SqlParameter("@iEmpresa", iEmpresa));
                context.Parametros.Add(new SqlParameter("@Usuario", iPersona));

                context.ExecuteProcedure("sp_Inserta_Contabilidad", true);
                return 1;
            }
            catch
            {
                return 0;
            }

        }

        public int CancelInvoice(int company, int typeinvoice, int invoicenumber, DateTime date, int person, string causa, int NumeroTransaccion, string auxcliente, decimal totalinvoice, string tituloinvoice, DateTime dateinvoice)
        {
            IntegraData.InvoiceDAL InvoiceDAL = new IntegraData.InvoiceDAL();

            try
            {
                if (InvoiceDAL.CancelInvoice(company, typeinvoice, invoicenumber, date, person, causa, NumeroTransaccion, auxcliente, totalinvoice, tituloinvoice, dateinvoice))
                {
                    return 1;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                InvoiceDAL = null;
            }


            return 0;
        }
     

        public int CancelOut(int iEmpresa, int iNumeroFactura, int iOpcion)
        {

            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Factura", iNumeroFactura));
                context.Parametros.Add(new SqlParameter("@Opcion", iOpcion));

                context.ExecuteProcedure("sp_Salida_Almacen_Factura_Cancelada", true);
                return 1;
            }
            catch
            {
                return 0;
            }

        }
       

        public int CancelCotizacion(int iEmpresa, int iNumeroFactura)
        {

            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Factura", iNumeroFactura));

                context.ExecuteProcedure("sp_Factura_Cancela_Cotizacion", true);
                return 1;
            }
            catch
            {
                return 0;
            }

        }
        

        public int CancelacionAutomaticaCiclo(int iEmpresa, int iNumeroFactura, DateTime Fecha, int iPersona)
        {

            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Factura", iNumeroFactura));
                context.Parametros.Add(new SqlParameter("@Fecha_Cancelacion", Fecha));
                context.Parametros.Add(new SqlParameter("@Usuario", iPersona));

                context.ExecuteProcedure("sp_Cotizacion_Cancelacion_Automatioca_Ciclo", true);
                return 1;
            }
            catch
            {
                return 0;
            }


        }
        

        public static DataTable ContabilidadManual(int iMovimento, int iEmpresa, int iNumero, int? iNumeroPoliza, int? iTipoPoliza,
                                                    int iNumeroCuenta, DateTime Fecha, string sNombre, double? Debe, double? Haber, int iPersona, int iPeriodo, string Aux)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("TipoMovimiento", iMovimento));
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Numero", iNumero));
            context.Parametros.Add(new SqlParameter("Numero_Poliza", iNumeroPoliza));
            context.Parametros.Add(new SqlParameter("Tipo_Poliza", iTipoPoliza));
            context.Parametros.Add(new SqlParameter("Numero_Cuenta", iNumeroCuenta));
            context.Parametros.Add(new SqlParameter("Fecha", Fecha));
            context.Parametros.Add(new SqlParameter("Concepto", sNombre));
            context.Parametros.Add(new SqlParameter("Debe", Debe));
            context.Parametros.Add(new SqlParameter("Haber", Haber));
            context.Parametros.Add(new SqlParameter("Usuario", iPersona));
            context.Parametros.Add(new SqlParameter("Periodo", iPeriodo));
            context.Parametros.Add(new SqlParameter("Numero_Manual", DBNull.Value));
            context.Parametros.Add(new SqlParameter("Titulo", sNombre));
            context.Parametros.Add(new SqlParameter("Clas_Moneda", DBNull.Value));
            context.Parametros.Add(new SqlParameter("Tipo_Cambio", DBNull.Value));
            context.Parametros.Add(new SqlParameter("Bandera", DBNull.Value));

            dt = context.ExecuteProcedure("sp_Contabilidad_Manual_TMP2", true).Copy();
            return dt;
        }

        public int GetInvoiceNumber(int company, int salenumber)
        {
            DataTable table = new DataTable();

            IntegraData.InvoiceDAL InvoiceDAL = new IntegraData.InvoiceDAL();

            try
            {
                using (table = ObtieneNumeroFactura(company, salenumber))
                {
                    if (table.Rows.Count > 0)
                        if (table.Rows[0][0] != DBNull.Value)
                            return Convert.ToInt32(table.Rows[0][0]);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                InvoiceDAL = null;
            }

            return 0;
        }

        public static DataTable ObtieneNumeroFactura(int iEmpresa, int iNumeroFactura)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("salenumber", iNumeroFactura));

            dt = context.ExecuteProcedure("sp_Factura_ObtieneNumeroDeFactura_v1", true).Copy();
            return dt;
        }

        

        public static DataTable ObtieneDetallesFactura(int iEmpresa, int iNumeroFactura, int iNumeroCliente, int iPadre)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("invoicenumber", iNumeroFactura));
            context.Parametros.Add(new SqlParameter("clientnumber", iNumeroCliente));
            context.Parametros.Add(new SqlParameter("father", iPadre));

            dt = context.ExecuteProcedure("sp_Factura_ObtieneDetallesDeFactura_v1", true).Copy();
            return dt;
        }

        

        public DataTable GetEDocto(int iEmpresa, int iNumeroFactura)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("invoicenumber", iNumeroFactura));

            dt = context.ExecuteProcedure("sp_Factura_ObtieneObtieneEDocto_v1", true).Copy();
            return dt;
        }

       

        public DataTable GetTicket(int iEmpresa, int iNumeroFactura)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("invoicenumber", iNumeroFactura));

            dt = context.ExecuteProcedure("sp_Factura_ObtieneTicket_v1", true).Copy();
            return dt;
        }

       

        public static DataTable GetCancelProcess(int iEmpresa, int iNumeroFactura)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));

            dt = context.ExecuteProcedure("sp_Factura_ObtieneProcesoDeCancelacion_v1", true).Copy();
            return dt;
        }


        public DataTable GetCheckAccounting()
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iCompany));
            context.Parametros.Add(new SqlParameter("invoicenumber", iInvoiceNumber));

            dt = context.ExecuteProcedure("sp_Factura_ObtieneRevisionContable_v1", true).Copy();
            return dt;
        }


        public DataTable GetDataPoliza()
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iCompany));
            context.Parametros.Add(new SqlParameter("numbertransaccion", iNumberTransaction));

            dt = context.ExecuteProcedure("sp_Factura_ObtieneDatosDePoliza_v1", true).Copy();
            return dt;
        }
              
        public DataTable GetNumberConcept()
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iCompany));
            context.Parametros.Add(new SqlParameter("invoicenumber", iInvoiceNumber));

            dt = context.ExecuteProcedure("sp_Factura_ObtieneNumeroDeConcepto_v1", true).Copy();
            return dt;
        }
     
        public DataTable GetConceptInvoiceV()
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iCompany));
            context.Parametros.Add(new SqlParameter("invoicenumber", iInvoiceNumber));

            dt = context.ExecuteProcedure("sp_Factura_ObtieneConceptoDeFactura_v1", true).Copy();
            return dt;
        }
        
        public DataTable GetValidateTransaction()
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iCompany));
            context.Parametros.Add(new SqlParameter("transactionnumber", iNumberTransaction));
            context.Parametros.Add(new SqlParameter("programnumber", iNumberProgram));

            dt = context.ExecuteProcedure("sp_Factura_ObtieneTransaccionValida_v1", true).Copy();
            return dt;
        }

        
        public DataTable GetContabilidadHaber(int iEmpresa, int iPersona)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@user", iPersona));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneContabilidadHaber]", true).Copy();
            return dt;

        }

        
        public DataTable GetContabilidadDebe()
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iCompany));
            context.Parametros.Add(new SqlParameter("@user", Person));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneContabilidadDebe]", true).Copy();
            return dt;

        }


        public bool UpdateCxc()
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("companynumber", iCompany));
                context.Parametros.Add(new SqlParameter("invoicenumber", iInvoiceNumber));
                context.Parametros.Add(new SqlParameter("date", Date));

                context.ExecuteProcedure("sp_Factura_ActualizaCuentasPorCobrar_v1", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }

     
        public bool DeleteCAD()
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("companynumber", iCompany));
                context.Parametros.Add(new SqlParameter("invoicenumber", iInvoiceNumber));
                context.Parametros.Add(new SqlParameter("numeroDetalle", iNumber));

                context.ExecuteProcedure("sp_Factura_BorraCAD_v1", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCpd()
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@companynumber", iCompany));
                context.Parametros.Add(new SqlParameter("@NumeroRegistro", iNumber));
                context.Parametros.Add(new SqlParameter("@diferencia", dAuxd));

                context.ExecuteProcedure("sp_Factura_ActualizaCPD_v1", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCpd(int intcompany, int intnumeroregistro, double ddiferencia)
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("companynumber", intcompany));
                context.Parametros.Add(new SqlParameter("NumeroRegistro", intnumeroregistro));
                context.Parametros.Add(new SqlParameter("diferencia", ddiferencia));

                context.ExecuteProcedure("sp_Factura_ActualizaCPD_v1", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }

     

        public bool UpdateCph()
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@companynumber", iCompany));
                context.Parametros.Add(new SqlParameter("@NumeroRegistro", iNumber));
                context.Parametros.Add(new SqlParameter("@diferencia", dAuxd));

                context.ExecuteProcedure("sp_Factura_ActualizaCPH_v1", true).Copy();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCp()
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("companynumber", iCompany));
                context.Parametros.Add(new SqlParameter("user", Person));

                context.ExecuteProcedure("sp_Factura_BorraCP_v1", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCp(int icompany, int? iperson)
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("companynumber", icompany));
                context.Parametros.Add(new SqlParameter("user", iperson));

                context.ExecuteProcedure("sp_Factura_BorraCP_v1", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool UpdateTkt()
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@companynumber", iCompany));
                context.Parametros.Add(new SqlParameter("@invoicenumber", iInvoiceNumber));

                context.ExecuteProcedure("sp_Factura_ActualizaTKT_v1", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateTkt(int icompany, int iinvoicenumber)
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("companynumber", icompany));
                context.Parametros.Add(new SqlParameter("invoicenumber", iinvoicenumber));

                context.ExecuteProcedure("sp_Factura_ActualizaTKT_v1", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool UpdateCot1()
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@companynumber", iCompany));
                context.Parametros.Add(new SqlParameter("@invoicenumber", iInvoiceNumber));

                context.ExecuteProcedure("sp_Factura_ActualizaCot1_v1", true).Copy();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCot1(int icompany, int iinvoicenumber)
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("companynumber", icompany));
                context.Parametros.Add(new SqlParameter("invoicenumber", iinvoicenumber));

                context.ExecuteProcedure("sp_Factura_ActualizaCot1_v1", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool UpdateCot2()
        {
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iCompany));
            context.Parametros.Add(new SqlParameter("@invoicenumber", iInvoiceNumber));

            context.ExecuteProcedure("sp_Factura_ActualizaCot2_v1", true).Copy();
            return true;
        }

        public int GetFolio(int company, int document, int serie)
        {
            try
            {
                SQLConection context = new SQLConection();
                DataTable dt = new DataTable();
                string sQuery = "SELECT dbo.fn_Obten_Siguiente_Folio( " + @company + " , " + @document + " , " + @serie + " )";
                dt = context.ExecuteQuery(sQuery).Copy();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0] != DBNull.Value)
                    {
                        return Convert.ToInt32(dt.Rows[0][0]);
                    }
                }

            }
            catch
            {
                return 0;
            }

            return 0;
        }

 
        //Al parecer este metodo no se utiliza en la solucion Verificar
        public int GetInvoiceNumberFromFolio(int company, int folio, int client)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", company));
            context.Parametros.Add(new SqlParameter("folio", folio));
            context.Parametros.Add(new SqlParameter("clientnumber", client));

            dt = context.ExecuteProcedure("sp_Factura_ObtieneNumeroDeFacturaPorFolio_v1", true).Copy();

            if (dt == null)
            {
                return -1;
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0][0]);
                }
            }


            return 0;
        }


        //Al parecer el evento nop se ocupa en la solucion Verificar
        public int CheckFolio(int company, int nota, int document, int serie, int folio)
        {
            try
            {
                SQLConection context = new SQLConection();
                DataTable dt = new DataTable();

                string sQuery = "SELECT dbo.fn_Valida_Folio( " + @company + " , " + @nota + " , " + @document + " , " + @serie + " , " + @folio + " )";
                dt = context.ExecuteQuery(sQuery).Copy();


                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0] != DBNull.Value)
                    {
                        return Convert.ToInt32(dt.Rows[0][0]);
                    }
                }

            }
            catch
            {
                throw;
            }

            return 0;
        }
                

        public DataTable GetInvoices(int iEmpresa, int iCliente, DateTime? FechaDe, DateTime? FechaHasta)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@customernumber", iCliente));
            context.Parametros.Add(new SqlParameter("@dateFrom", FechaDe.HasValue ? (object)FechaDe.Value.ToShortDateString() : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@dateTo", FechaHasta.HasValue ? (object)FechaHasta.Value.ToShortDateString() : DBNull.Value));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneFacturas_v4]", true).Copy();
            return dt;

        }
        

        public static DataTable GetInvoicesView(int iEmpresa, int iCliente, int iMoneda, int iDocumento, int iSerie, DateTime? FechaDe, DateTime? FechaHasta, int iDocumentoDe, int iDocumentoHasta)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@iClient", iCliente));
            context.Parametros.Add(new SqlParameter("@iCurrency", iMoneda));
            context.Parametros.Add(new SqlParameter("@idocument", iDocumento));
            context.Parametros.Add(new SqlParameter("@iSerie", iSerie));
            context.Parametros.Add(new SqlParameter("@DateFrom", FechaDe.HasValue ? (object)FechaDe.Value.ToShortDateString() : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@DateTo", FechaHasta.HasValue ? (object)FechaHasta.Value.ToShortDateString() : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@idocFrom", iDocumentoDe));
            context.Parametros.Add(new SqlParameter("@idocTo", iDocumentoHasta));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneVistaDeFacturas_v2]", true).Copy();
            return dt;

        }
        
        public DataTable GetInvoiceDetails(int iFactura, int iEmpresa, int iFamilia)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@invoicenumber", iFactura));
            context.Parametros.Add(new SqlParameter("@family", iFamilia));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneDetallesDeFactura_BIS_v1]", true).Copy();
            return dt;

        }

        public string GetStatusDescriptionFromStatusNumber(int icompany, int istatus, int ifamily)
        {
            IntegraData.InvoiceDAL Invoice = new IntegraData.InvoiceDAL();
            DataTable StatusTable;

            using (StatusTable = ObtenDescripcionDeEstatusporsuNumero(icompany, istatus, ifamily))
            {
                if (StatusTable != null && StatusTable.Rows.Count > 0)
                {
                    return StatusTable.Rows[0][0].ToString();
                }
            }

            return null;
        }

        public DataTable ObtenDescripcionDeEstatusporsuNumero(int iEmpresa, int iEstatus, int iFamilia)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@status", iEstatus));
            context.Parametros.Add(new SqlParameter("@family", iFamilia));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneDescripcionDeStatusPorNumeroDeStatus_v1]", true).Copy();
            return dt;
        }

        
        public DataTable GetCountOfficeDocto(int iEmpresa, int iSucursal)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@office", iSucursal));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneElNumeroDeDocumentos_v1]", true).Copy();
            return dt;
        }
                

        public DataTable GetOfficeQuote(int iEmpresa, int iNumero)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@numero", iNumero));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneCuotaDeOficina_v1]", true).Copy();
            return dt;
        }
        

        public DataTable GetRelationship(int iEmpresa, int iNumeroUsuario)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@user", iNumeroUsuario));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneRelaciones_v1]", true).Copy();
            return dt;
        }

        

        public DataTable GetClassDocto(int iEmpresa, int iNumeroUsuario)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@user", iNumeroUsuario));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneClaseDeDocumentos_v1]", true).Copy();
            return dt;
        }

      
        public DataTable Getprefijo()
        {
            try
            {
                DataTable dt = new DataTable();
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@companynumber", iCompany));
                context.Parametros.Add(new SqlParameter("@number", iNumberFolio));

                dt = context.ExecuteProcedure("[sp_Factura_ObtienePrefijoDeFactura_v1]", true).Copy();
                return dt;
            }
            catch
            {
                return null;
            }
        }

     
        public DataTable GetClientExchangeConceptAndEstatus()
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iCompany));
            context.Parametros.Add(new SqlParameter("@number", iInvoiceNumber));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneConceptoDeIntercambioDelCliente_v1]", true).Copy();
            return dt;
        }

        public static DataTable GetClientExchangeConceptAndEstatus(int iEmpresa, int iNumeroFactura)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@number", iNumeroFactura));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneConceptoDeIntercambioDelCliente_v1]", true).Copy();
            return dt;
        }


        public DataTable GetIVAInvoice()
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iCompany));
            context.Parametros.Add(new SqlParameter("@number", iNumberFolio));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneIvaDeFactura_v1]", true).Copy();
            return dt;
        }

        public DataTable GetIVA()
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iCompany));
            context.Parametros.Add(new SqlParameter("@number", iNumberFolio));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneIVA_v1]", true).Copy();
            return dt;
        }
       
        public DataTable CheckPoliza()
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iCompany));
            context.Parametros.Add(new SqlParameter("@user", Person.HasValue ? (object)Person.Value : DBNull.Value));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneRevisionDePoliza_v1]", true).Copy();
            return dt;
        }
       
        public DataTable GetDoctoCxC(int iEmpresa, int iNumeroFactura)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@invoicenumber", iNumeroFactura));

            dt = context.ExecuteProcedure("[sp_Factura_ObtieneDocumetosCxC_v1]", true).Copy();
            return dt;
        }
      

        public DataTable ObtieneDisponibleDeFactura(int iEmpresa, int iNumeroFcatura)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("invoicenumber", iNumeroFcatura));

            dt = context.ExecuteProcedure("sp_Factura_ObtieneStockDeFactura_v1", true).Copy();
            return dt;
        }
        
        public DataTable ObtieneOrigenFactura(int iEmpresa, int iNumeroFcatura)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("invoicenumber", iNumeroFcatura));

            dt = context.ExecuteProcedure("sp_Factura_ObtieneOrigenDeFactura_v1", true).Copy();
            return dt;
        }

        public bool PayInvoice()
        {
            IntegraData.InvoiceDAL PayDAL = new IntegraData.InvoiceDAL();

            try
            {

                return PayDAL.PayInvoice(iType, iCompany, iInvoiceNumber, iNumberAux, iConceptNumber, iCurrency, dExchange,
                                        dIngreso, dEgreso, sAux, Person);
            }

            catch
            {
                return false;
            }
        }

        public bool PayInvoiceProcess()
        {
            IntegraData.InvoiceDAL PayDAL = new IntegraData.InvoiceDAL();

            try
            {

                return PayDAL.PayInvoiceProcess(iCompany, iInvoiceNumber);
            }


            catch
            {
                return false;
            }
        }

        public DataTable CalculaImporteCxC(int icompany, int iinvoicenumber)
        {
            IntegraData.InvoiceDAL InvoiceDAL = new IntegraData.InvoiceDAL();


            try
            {
                return InvoiceDAL.CalculaImporteCxC(icompany, iinvoicenumber);
            }
            catch
            {
                return null;
            }

        }

        

        public DataTable ValidaConceptoContable(int icompany, int inumber, int iconcept)
        {
            try
            {
                DataTable dt = new DataTable();
                SQLConection context = new SQLConection();
                string sQuery = "SELECT dbo.fn_Valida_Concepto_Contable(" + icompany + "," + inumber + "," + iconcept + ")";
                dt = context.ExecuteQuery(sQuery).Copy();
                return dt;
            }
            catch
            {
                return null;
            }
        }


        //SE CREÓ UN PROCEDIMIENTO
     

        public DataTable GetRutaTransporte(int icompany, int inumber)
        {
            try
            {
                SQLConection context = new SQLConection();
                DataTable dt = new DataTable();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@companynumber", icompany));
                context.Parametros.Add(new SqlParameter("@salenumber", inumber));

                dt = context.ExecuteProcedure("sp_ObtenRutaTransporte", true).Copy();
                return dt;
            }
            catch
            {
                return null;
            }
        }


        #region Facturacion
        public int GetFnFacturaConcepto(int company, int? iSucursal)
        {
            IntegraData.InvoiceDAL InvoiceDAL = new IntegraData.InvoiceDAL();
            DataTable table = new DataTable();

            try
            {
                using (table = InvoiceDAL.GetFnFacturaConcepto(company, iSucursal))
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
                InvoiceDAL = null;
            }

            return 0;


        }


        public int GetfnIDFactura(int company, int inumero)
        {
            IntegraData.InvoiceDAL InvoiceDAL = new IntegraData.InvoiceDAL();
            DataTable table = new DataTable();

            try
            {
                using (table = InvoiceDAL.GetfnIDFactura(company, inumero))
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
                InvoiceDAL = null;
            }

            return 0;
        }

        #endregion


        //PRUEBAS
        public int sp_Registro_Pago(int iMovimiento, int iEmpresa, int intNumeroCotizacion, int intNumero, DateTime Fecha_Emision, DateTime Fecha_Revision, DateTime Fecha_Pago,
                                    int intMoneda, decimal dDescuento, decimal dSubtotal, decimal dIva, decimal dTotal, string sImporteLetra, int intTotalRegistro, int intNumeroCliente,
                                    int intPersonaRecibo, int intEstatus, int intNumeroOrden, int intTipoDocumento, int? intTipoFacturaRelacion, string sNombreProyecto,
                                    DateTime? FechaCotizacion, decimal? dTipoCambio, int? intNumRegistroCondicionesPago, int? intClasMonedaCambio, int? intNumeroFactura, int? intNumeroConcepto)
        {

            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@TipoMovimiento", iMovimiento));
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Cotizacion", intNumeroCotizacion));
                context.Parametros.Add(new SqlParameter("@Numero", intNumero));
                context.Parametros.Add(new SqlParameter("@Fecha_Emision", Fecha_Emision));
                context.Parametros.Add(new SqlParameter("@Fecha_Revision", Fecha_Revision));
                context.Parametros.Add(new SqlParameter("@Fecha_Pago", Fecha_Pago));
                context.Parametros.Add(new SqlParameter("@Moneda", intMoneda));
                context.Parametros.Add(new SqlParameter("@Descuento", dDescuento));
                context.Parametros.Add(new SqlParameter("@SubTotal", dSubtotal));
                context.Parametros.Add(new SqlParameter("@IVA", dIva));
                context.Parametros.Add(new SqlParameter("@Total", dTotal));
                context.Parametros.Add(new SqlParameter("@Importe_Letra", sImporteLetra));
                context.Parametros.Add(new SqlParameter("@Total_Registro", intTotalRegistro));
                context.Parametros.Add(new SqlParameter("@Numero_Cliente", intNumeroCliente));
                context.Parametros.Add(new SqlParameter("@Persona_Recibo", intPersonaRecibo));
                context.Parametros.Add(new SqlParameter("@Estatus", intEstatus));
                context.Parametros.Add(new SqlParameter("@Numero_Orden", intNumeroOrden));
                context.Parametros.Add(new SqlParameter("@Tipo_Documento", intTipoDocumento));
                context.Parametros.Add(new SqlParameter("@Tipo_Factura_Relacion", intTipoFacturaRelacion.HasValue ? (object)intTipoFacturaRelacion.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Nombre_Proyecto", sNombreProyecto));
                context.Parametros.Add(new SqlParameter("@Fecha_Cotizacion", FechaCotizacion.HasValue ? (object)FechaCotizacion.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Tipo_Cambio", dTipoCambio.HasValue ? (object)dTipoCambio.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Numero_Registro_Condiciones_Pago", intNumRegistroCondicionesPago.HasValue ? (object)intNumRegistroCondicionesPago.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Clas_Moneda_Cambio", intClasMonedaCambio.HasValue ? (object)intClasMonedaCambio.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Numero_Factura", intNumeroFactura.HasValue ? (object)intNumeroFactura.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Num_Concepto", intNumeroConcepto.HasValue ? (object)intNumeroConcepto.Value : DBNull.Value));

                context.ExecuteProcedure("sp_Registro_Pago", true);
                return 1;
            }
            catch
            {
                return 0;
            }

        }



        public static DataTable ObtieneFacturasDiferidas(int iEmpresa, int? iCliente, DateTime? FechaDe, DateTime? FechaHasta, int iDocumentoDe, int iDocumentoHasta)
        {

            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("@iClient", iCliente.HasValue ? (object)iCliente.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@DateFrom", FechaDe.HasValue ? (object)FechaDe.Value.ToShortDateString() : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@DateTo", FechaHasta.HasValue ? (object)FechaHasta.Value.ToShortDateString() : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@idocFrom", iDocumentoDe));
            context.Parametros.Add(new SqlParameter("@idocTo", iDocumentoHasta));

            dt = context.ExecuteProcedure("[sp_Venta_BusquedaDeVentasDiferidas]", true).Copy();
            return dt;

        }
    }
}
