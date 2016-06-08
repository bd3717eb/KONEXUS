using System;
using System.Data;
using IntegraData;
using System.Data.SqlClient;
namespace IntegraBussines
{
    public class SaleBLL
    {
        public int Company { get; set; }
        public int Number { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public int Customer { get; set; }
        public int User { get; set; }
        public int? Seller { get; set; }
        public int Responsible { get; set; }
        public string Project { get; set; }
        public int Currency { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int? Contact { get; set; }
        public string Observations { get; set; }
        public int PayNumber { get; set; }
        public int CPayPeriod { get; set; }
        public int? Document { get; set; }
        public int Documento { get; set; }
        public int Serie { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int OrderNumber { get; set; }
        public int CIVA { get; set; }
        public int? DocumentRelation { get; set; }
        public int CurrencyInvoice { get; set; }
        public int ConceptNumber { get; set; }
        public int? ReferenceNumber { get; set; }
        public int AddressNumber { get; set; }
        public int Credit { get; set; }
        public int COffice { get; set; }
        public int DirectDelivery { get; set; }
        public int? CNumber { get; set; }
        public int? CStore { get; set; }
        public int? PriceList { get; set; }
        public int? RealPriceList { get; set; }
        public int? CPayData { get; set; }
        public int? PayData { get; set; }
        public int Type { get; set; }
        /*Facturacion variables necesarias*/
        public int NumeroDocto { get; set; }
        public string Reference { get; set; }
        public decimal TypeChange { get; set; }
        public int? Notacargo { get; set; }
        public int Clas_Sucursal { get; set; }
        public string SPayData { get; set; }


        public decimal? dSub { get; set; }
        public decimal? dIva { get; set; }
        public decimal? dTotal { get; set; }

        public string sCondicionPago { get; set; }
        /*Facturacion variables necesarias*/

        public int SaveSale(int Moviment)
        {
            IntegraData.SaleDAL SaleDAL = new IntegraData.SaleDAL();

            try
            {
                return SaleDAL.SaveSale(Moviment, Company, Number, Status, Date, Customer, User, Seller, Responsible, Project,
                                        Currency, DeliveryDate, Contact, Observations, PayNumber, CPayPeriod, Document,
                                        ExpirationDate, OrderNumber, CIVA, DocumentRelation, CurrencyInvoice, ConceptNumber,
                                        ReferenceNumber, AddressNumber, Credit, COffice, DirectDelivery, CNumber, CStore);
            }
            catch
            {
                return -1;
            }
        }

        public int GetStatusNumberFromDescription(string sdescription, int ifamily, int ifather)
        {
            IntegraData.SaleDAL Sale = new IntegraData.SaleDAL();
            DataTable StatusTable;

            using (StatusTable = Sale.GetStatusNumberFromDescription(sdescription, ifamily, ifather))
            {
                if (StatusTable != null && StatusTable.Rows.Count > 0)
                {
                    return Convert.ToInt32(StatusTable.Rows[0][0]);
                }
            }

            return -1;
        }

        public int GetStatusNumberFromSaleNumber(int company, int salenumber)
        {
            IntegraData.SaleDAL Sale = new IntegraData.SaleDAL();
            DataTable StatusTable;

            using (StatusTable = Sale.GetStatusNumberAndDescFromSaleNumber(company, salenumber))
            {
                if (StatusTable != null && StatusTable.Rows.Count > 0)
                {
                    return Convert.ToInt32(StatusTable.Rows[0][0]);
                }
            }

            return -1;
        }

        public string GetStatusDescriptionFromSaleNumber(int company, int salenumber)
        {
            IntegraData.SaleDAL Sale = new IntegraData.SaleDAL();
            DataTable StatusTable;

            using (StatusTable = Sale.GetStatusNumberAndDescFromSaleNumber(company, salenumber))
            {
                if (StatusTable != null && StatusTable.Rows.Count > 0)
                {
                    return StatusTable.Rows[0][1].ToString();
                }
            }

            return null;
        }

        public bool IsProduct(int company, int product)
        {
            IntegraData.SaleDAL Sale = new IntegraData.SaleDAL();
            DataTable ProductTable;

            using (ProductTable = Sale.IsProduct(company, product))
            {
                if (ProductTable == null)
                {
                    return false;
                }
                else
                {
                    if (ProductTable.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(ProductTable.Rows[0][0]) == 113)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
        }

        public DataTable GetStoreDataFromSaleNumber(int icompany, int isalenumber, int igamesale, int ifamily)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@salenumber", isalenumber));
            context.Parametros.Add(new SqlParameter("@gamesale", igamesale));
            context.Parametros.Add(new SqlParameter("@family", ifamily));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneDatosDeTiendaPorNumeroDeVenta_v1", true).Copy();
            return dt;
        }

        public decimal GetStockStore(int icompany, int istore, int iproduct, int iconcept, int ifamily)
        {
            IntegraData.SaleDAL Sale = new IntegraData.SaleDAL();
            DataTable StockTable;

            using (StockTable = Sale.GetStockStore(icompany, istore, iproduct, iconcept, ifamily))
            {
                if (StockTable == null)
                {
                    return -1;
                }
                else
                {
                    if (StockTable.Rows.Count > 0)
                    {
                        return Convert.ToDecimal(StockTable.Rows[0][0]);
                    }
                    else
                        return 0;
                }
            }
        }


        //EL PROCEDIMIENTO NO SE ENCUENTRA EN INTEGRA DEMO
        //Al PARECER ESTE METODO NO SE UTILIZA

        public DataTable GetSaleData(int company, int salenumber)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", company));
            context.Parametros.Add(new SqlParameter("@salenumber", salenumber));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneDatosDeVenta_v1", true).Copy();
            return dt;
        }


        public DataTable GetDetailsTableFromSaleNumber(int company, int salenumber)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", company));
            context.Parametros.Add(new SqlParameter("@salenumber", salenumber));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneDetallesDeTablaPorNumeroDeVenta_v1", true).Copy();
            return dt;

        }

        public bool AwayProducts(int moviment, int company, int salenumber)
        {
            IntegraData.SaleDAL Sale = new IntegraData.SaleDAL();
            return Sale.AwayProducts(moviment, company, salenumber);
        }

        public bool StoreDetailsTMPTable(int moviment, int company, int salenumber, int gamesale, int store, decimal quantity, int user)
        {
            IntegraData.SaleDAL Sale = new IntegraData.SaleDAL();
            return Sale.StoreDetailsTMPTable(moviment, company, salenumber, gamesale, store, quantity, user);
        }

        public int GetFhaterNumberFromChildNumber(int company, int childnumber, int family)
        {
            IntegraData.SaleDAL Sale = new IntegraData.SaleDAL();
            DataTable NumberTable;

            using (NumberTable = Sale.GetFhaterNumberFromChildNumber(company, childnumber, family))
            {
                if (NumberTable != null && NumberTable.Rows.Count > 0)
                {
                    return Convert.ToInt32(NumberTable.Rows[0][0]);
                }
            }

            return 0;
        }

        //AL PARECER ESTE EVENTO NO SE UTILIZA EN LA SOLUCION
        public DataTable SearchSales(int icompany, int ioffice, int ifamily, int ifather, int istatus, DateTime? datefrom, DateTime? dateto, int isalefrom, int isaleto, int iclient)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@office", ioffice));
            context.Parametros.Add(new SqlParameter("@family", ifamily));
            context.Parametros.Add(new SqlParameter("@father", ifather));
            context.Parametros.Add(new SqlParameter("@status", istatus));
            context.Parametros.Add(new SqlParameter("@datefrom", datefrom.HasValue ? (object)datefrom.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@dateto", dateto.HasValue ? (object)dateto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@salefrom", isalefrom));
            context.Parametros.Add(new SqlParameter("@saleto", isaleto));
            context.Parametros.Add(new SqlParameter("@client", iclient));

            dt = context.ExecuteProcedure("sp_Venta_BusquedaDeVentas_v2", true).Copy();
            return dt;

        }

        public DataTable GetZoneOffice(int icompany, int ioffice)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@office", ioffice));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneZonaDeOficina_v1", true).Copy();
            return dt;

        }


        //EL PROCEDIMIENTO NO SE ENCUENTRA EN LA BASE DE DATOS INTEGRA_DEMO
        public DataTable GetOfficeInvoice(int icompany, int inumero)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", icompany));
            context.Parametros.Add(new SqlParameter("numero", inumero));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneOficinaDeCotizacion_v1", true).Copy();
            return dt;

        }

        public DataTable GetZoneRelationOffice(int company, int office)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", company));
            context.Parametros.Add(new SqlParameter("@office", office));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneZonaRelacionOficina_v1", true).Copy();
            return dt;
        }


        public DataTable GetReferenceCost(int icompany, int ioffice, int iproduct, int iconcept, int iproductsFamily)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@office", ioffice));
            context.Parametros.Add(new SqlParameter("@product", iproduct));
            context.Parametros.Add(new SqlParameter("@concept", iconcept));
            context.Parametros.Add(new SqlParameter("@productsFamily", iproductsFamily));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneCostoReferencia_v1", true).Copy();
            return dt;
        }


        //EL PROCEDIMIENTO NO SE ENCUENTRA EN LA BASE DE DATOS INTEGRA_DEMO
        //AL PARECER ESTE EVENTO NO SE UTILIZA
        public DataTable GetConceptDescuento()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", Company));
            context.Parametros.Add(new SqlParameter("customer", Customer));
            context.Parametros.Add(new SqlParameter("concept", ConceptNumber));
            context.Parametros.Add(new SqlParameter("date", Date));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneConceptoDeDescuento_v1", true).Copy();
            return dt;
        }

        //El procedimiento no se encuentra en la base de datos INTEGRA_DEMO
        //AL PARECER ESTE EVENTO NO SE UTILIZA EN LA SOLUCION
        public DataTable GetArtDescuento()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", Company));
            context.Parametros.Add(new SqlParameter("customer", Customer));
            context.Parametros.Add(new SqlParameter("concept", ConceptNumber));
            context.Parametros.Add(new SqlParameter("date", Date));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneDescuentoArt_v1", true).Copy();
            return dt;
        }

        public DataTable GetDatosDocto()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", Company));
            context.Parametros.Add(new SqlParameter("@document", Documento));
            context.Parametros.Add(new SqlParameter("@serie", Serie));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneDatosDeDocumento_v1", true).Copy();
            return dt;
        }


        public DataTable GetDatosDoctoE()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", Company));
            context.Parametros.Add(new SqlParameter("@number", Number));
            context.Parametros.Add(new SqlParameter("@type", Type));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneDatosDeDocumentoE_v1", true).Copy();
            return dt;
        }

        public DataTable GetDatosDoctoEmisor()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", Company));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneDatosDeDocumentoEmisor_v1", true).Copy();
            return dt;

        }

        public DataTable GetDatosDoctoReceptor()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", Company));
            context.Parametros.Add(new SqlParameter("@number", Number));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneDocumentoReceptor_v1", true).Copy();
            return dt;
        }


        public DataTable GetDatosEmpresa()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", Company));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneDatosDeEmpresa_v1", true).Copy();
            return dt;
        }


        public DataTable GetInvoices()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", Company));
            context.Parametros.Add(new SqlParameter("@customer", Customer));
            context.Parametros.Add(new SqlParameter("@typeDoc", Type));
            context.Parametros.Add(new SqlParameter("@contabiliza", Documento));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneFacturas_v1", true).Copy();
            return dt;
        }

        public DataTable GetTraeConcepto_Tienda_C()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Company));
            context.Parametros.Add(new SqlParameter("@Numero_Cotizacion", Number));

            dt = context.ExecuteProcedure("sp_Cuentas_Cobrar_TraeConcepto_Tienda_C", true).Copy();
            return dt;
        }


        public DataTable GetAddress()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", Company));
            context.Parametros.Add(new SqlParameter("@customer", Customer));
            context.Parametros.Add(new SqlParameter("@typeAddressrate", Type));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneDireccion_v1", true).Copy();
            return dt;
        }


        public DataTable GetDatosInternet()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", Company));
            context.Parametros.Add(new SqlParameter("@serie", Serie));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneDatosDeInternet_v1", true).Copy();
            return dt;
        }


        public DataTable GetDatosCorreo()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", Company));
            context.Parametros.Add(new SqlParameter("@cliente", Customer));
            context.Parametros.Add(new SqlParameter("@sendemail", Type));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneDatosDeCorreo_v1", true).Copy();
            return dt;
        }


        public DataTable GetIvaReceptor(int icompany, int inumero, int ifamily)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@family", ifamily));
            context.Parametros.Add(new SqlParameter("@number", inumero));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneIvaReceptor_v1", true).Copy();
            return dt;
        }


        public DataTable GetNumeroFactura(int company, int invoicenumber)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", company));
            context.Parametros.Add(new SqlParameter("@number", invoicenumber));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneNumeroFactura_v1", true).Copy();
            return dt;
        }

        public bool StoreEjecutar(string cadena)
        {
            IntegraData.SaleDAL Sale = new IntegraData.SaleDAL();
            return Sale.StoreExe(cadena);
        }

        public DataTable GetCharacteristics(int company, int product, int concept, int family)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", company));
            context.Parametros.Add(new SqlParameter("product", product));
            context.Parametros.Add(new SqlParameter("concept", concept));
            context.Parametros.Add(new SqlParameter("family", family));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneCaracteristicas_v1", true).Copy();
            return dt;
        }

        // public DataTable GetStockStores(int company, int person, int product, int concept)
        //{
        //    SQLConection context = new SQLConection();
        //    DataTable dt = new DataTable();

        //    context.Parametros.Clear();
        //    context.Parametros.Add(new SqlParameter("@companynumber", company));
        //    context.Parametros.Add(new SqlParameter("@usernumber", person));
        //    context.Parametros.Add(new SqlParameter("@product", product));
        //    context.Parametros.Add(new SqlParameter("@concept", concept));

        //    dt = context.ExecuteProcedure("sp_Venta_ObtieneStockDeTiendas_v1", true);
        //    return dt;
        //}


        public DataTable GetAllStores(int company, int person)
        {

            return IntegraBussines.SalesStoresBLL.GetAllStores(company, person);
        }


        //El procedimiento no se encuentra en labase INTEGRA_DEMO
        public DataTable GetStock(int icompany, int istore, int iproduct, int iconcept, int ifamily)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", icompany));
            context.Parametros.Add(new SqlParameter("store", istore));
            context.Parametros.Add(new SqlParameter("product", iproduct));
            context.Parametros.Add(new SqlParameter("concept", iconcept));
            context.Parametros.Add(new SqlParameter("family", ifamily));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneStock_v1", true).Copy();
            return dt;
        }

        public DataTable GetNormalStores(int company, int person, int office)
        {

            return IntegraBussines.SalesStoresBLL.GetNormalStores(company, person, office);
        }

        //El procedimiento no se encuentra en la base INTEGRA_DEMO
        //Al parecer este metodo no se utiliza en la solucion
        public DataTable GetNormalStoresIndex(int company, int person, int office, int product, int concept)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", company));
            context.Parametros.Add(new SqlParameter("usernumber", person));
            context.Parametros.Add(new SqlParameter("office", office));
            context.Parametros.Add(new SqlParameter("product", product));
            context.Parametros.Add(new SqlParameter("concept", concept));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneIndiceDeTiendasNormales_v1", true).Copy();
            return dt;
        }

        public DataTable GetVirtualStores(int company, int person, int office)
        {

            return IntegraBussines.SalesStoresBLL.GetVirtualStores(company, person, office);
        }


        //public DataTable GetPriceFromListPrice(int company, int client, int t_concept, int product, int concept, decimal price)// si / ventas 
        //{
        //    DataTable dt = new DataTable();
        //    SQLConection context = new SQLConection();
        //    string sQuery = "SELECT dbo.fn_ObtenPrecio_Cliente(" + @company + " , " + @client + " , " + @product + " , " + @t_concept + " , " + @concept + " , " + @price + " , " + "1 )";
        //    dt = context.ExecuteQuery(sQuery).Copy();

        //    return dt;

        //}

        public DataTable GetPayForms(int company, int quotenumber)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", company));
            context.Parametros.Add(new SqlParameter("@Numero_Cotizacion", quotenumber));

            dt = context.ExecuteProcedure("sp_Cuentas_Cobrar_TraeConcepto_Tienda", true).Copy();
            return dt;
        }



        public DataTable GetSalesValidations(int company)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", company));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneValidacionDeVentas_v1", true).Copy();
            return dt;
        }


        public DataTable GetStoresValidationsAndMailCompany(int company)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", company));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneValidacionDeTiendasYMailDeCompania_v1", true).Copy();
            return dt;
        }

        public int SpFacturaLibre(int Moviment)
        {
            IntegraData.SaleDAL SaleDAL = new IntegraData.SaleDAL();

            try
            {
                return SaleDAL.SpFacturaLibre(Moviment, Company, Number, Customer, Date, Documento, Serie, NumeroDocto, Reference,
                                             CurrencyInvoice, TypeChange, ConceptNumber, User, Credit, Observations, Notacargo,
                                             CPayData, SPayData, Clas_Sucursal, dSub, dIva, dTotal, sCondicionPago);
            }
            catch
            {
                return -1;
            }
        }

        public int spFacturaContabilidadCC()
        {
            IntegraData.SaleDAL SaleDAL = new IntegraData.SaleDAL();
            try
            {
                return SaleDAL.spFacturaContabilidadCC(Company, Number, User);
            }
            catch
            {
                return -1;
            }
        }

        public DataTable GetDatosCorreoCliente(int cliente)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@cliente", cliente));

            dt = context.ExecuteProcedure("[sp_Venta_ObtieneDatosDeCorreo]", true).Copy();
            return dt;
        }

        public bool GenerarFolioFiscalPagar(int numFactura, int numEmpresa)
        {
            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@NumeroFactura", numFactura));
                context.Parametros.Add(new SqlParameter("@NumeroEmpresa", numEmpresa));


                context.ExecuteProcedure("sp_ActualizaFolioFiscal", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable ObtieneDetalleCotizacion(int intEmpresa, int intNumeroCotizacion)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@numero_empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@numero_Cotizacion", intNumeroCotizacion));
            dt = context.ExecuteProcedure("sp_Venta_ObtieneDatosCotizacion", true).Copy();
            return dt;
        }

        #region Facturacion
        public DataTable GetDatosCorreoCliente()
        {
            IntegraData.SaleDAL Sale = new IntegraData.SaleDAL();
            return Sale.GetDatosCorreoCliente(Customer);
        }

        public bool StoreEjecutarCopiado(string cadena)
        {
            IntegraData.SaleDAL Sale = new IntegraData.SaleDAL();
            return Sale.StoreCopiado(cadena);
        }
        #endregion



    }
}
