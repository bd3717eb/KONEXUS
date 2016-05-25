using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;
using System.Data.SqlClient;
using System.Web;

namespace IntegraBussines
{
    public static class ControlsBLL
    {
        public static DataTable GetCurrencies(int icompany, int ifather, int ifamily)
        {

            try
            {

                return DevolucionController.ObtieneCatalogo(icompany, ifather, ifamily);
            }
            catch
            {
                return null;
            }

        }
        public static DataTable ObtieneDescripcionMonedaPorNumero(int iEmpresa, int iMoneda, int iFamilia)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("currency", iMoneda));
            context.Parametros.Add(new SqlParameter("family", iFamilia));

            dt = context.ExecuteProcedure("sp_Parametros_ObtieneDescripcionDeMonedaPorNumeroDeMoneda_v1", true).Copy();
            return dt;
        }



        public static DataTable ObtieneMonedaPorNumeroEmpresa(int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));

            dt = context.ExecuteProcedure("sp_CXP_Facturas_Filtro_Moneda", true).Copy();
            return dt;
        }



        public static DataTable GetOffices(int iEmpresa, int iUsuario)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("user", iUsuario));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneOficinas_v1", true).Copy();
            return dt;
        }


        public static DataTable ObtieneEmpresa(int iUsuario)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Usuario", iUsuario));

            dt = context.ExecuteProcedure("sp_Empresa_Consulta", true).Copy();
            return dt;
        }



        public static DataTable GetIVA(int icompany, int ifather, int ifamily)
        {


            try
            {

                return DevolucionController.ObtieneCatalogo(icompany, ifather, ifamily);
            }
            catch
            {
                return null;
            }

        }

        public static Decimal GetExchange(int iEmpresa, int iMonedaOrigen, int iMonedaDestino, DateTime date)
        {

            DataTable table = new DataTable();

            try
            {
                DataTable dt = new DataTable();
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
                context.Parametros.Add(new SqlParameter("OriginCurrency", iMonedaOrigen));
                context.Parametros.Add(new SqlParameter("DestinyCurrency", iMonedaDestino));
                context.Parametros.Add(new SqlParameter("date", date));

                dt = context.ExecuteProcedure("sp_Controles_ObtieneTipoDeCambio_v1", true).Copy();


                if (dt.Rows.Count > 0)
                    return Math.Round(Convert.ToDecimal(dt.Rows[0][0]), 4);
                else
                    return 1;
            }
            catch
            {
                return -1;
            }


        }


        public static DataTable GetDocuments(int iEmpresa, int iUsuario, int iFamilia)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("user", iUsuario));
            context.Parametros.Add(new SqlParameter("family", iFamilia));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneDocumentos_v1", true).Copy();
            return dt;
        }



        public static DataTable GetSeriesDoctos(int iEmpresa, int iUsuario, int iDocumento, int? iSucursal)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("user", iUsuario));
            context.Parametros.Add(new SqlParameter("docto", iDocumento));
            context.Parametros.Add(new SqlParameter("sucursal", iSucursal.HasValue ? (object)iSucursal.Value : DBNull.Value));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneSeriesDocumentos_v1", true).Copy();
            return dt;
        }




        public static DataTable GetSeries(int iEmpresa, int iUsuario, int iDocumento)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("user", iUsuario));
            context.Parametros.Add(new SqlParameter("docto", iDocumento));


            dt = context.ExecuteProcedure("sp_Controles_ObtieneSeries_v1", true).Copy();
            return dt;
        }

        public static DataTable GetStatus(int icompany, int ifather, int ifamily)
        {

            return DevolucionController.ObtieneCatalogo(icompany, ifather, ifamily);
        }

        public static DataTable GetAddresType(int icompany, int ifather, int ifamily)
        {

            return DevolucionController.ObtieneCatalogo(icompany, ifather, ifamily);
        }

        public static DataTable GetCountry(int icompany, int ifather, int ifamily)
        {

            return DevolucionController.ObtieneCatalogo(icompany, ifather, ifamily);
        }

        public static DataTable GetCustomerStatus(int icompany, int ifather, int ifamily)
        {

            return DevolucionController.ObtieneCatalogo(icompany, ifather, ifamily);
        }
        public static DataTable GetProviderStatus(int icompany, int ifather, int ifamily)
        {
            return DevolucionController.ObtieneCatalogo(icompany, ifather, ifamily);

        }


        public static DataTable GetCustomerType(int iEmpresa, int iUsuario)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("usernumber", iUsuario));


            dt = context.ExecuteProcedure("sp_Controles_ObtieneTipoDeCliente_v1", true).Copy();
            return dt;
        }



        public static DataTable GetCustomerZone(int iEmpresa, int iUsuario)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("personnumber", iUsuario));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneZonaDeCliente_v1", true).Copy();
            return dt;
        }


        public static DataTable GetCustomerPrice(int iEmpresa, int iUsuario)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("personnumber", iUsuario));

            dt = context.ExecuteProcedure("sp_Controles_ObtienePrecioDelCliente_v1", true).Copy();
            return dt;
        }
        public static DataTable GetCustomerPeriod(int icompany, int ifather, int ifamily)
        {
            return DevolucionController.ObtieneCatalogo(icompany, ifather, ifamily);
        }



        public static DataTable GetCustomerDataPay(int iEmpresa, int iPadre, int iFamilia)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("family", iFamilia));
            context.Parametros.Add(new SqlParameter("father", iPadre));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneDatosDePagoDelCliente_v1", true).Copy();
            return dt;
        }


        public static DataTable GetCustomerExisting(string sRFC, string sNombreCorto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("rfc", sRFC));
            context.Parametros.Add(new SqlParameter("nombrecorto", sNombreCorto));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneExistenciaDelCliente_v1", true).Copy();
            return dt;
        }

        public static DataTable GetCustomerExistingTable(int iEmpresa, int iNumero)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("number", iNumero));

            dt = context.ExecuteProcedure("sp_Controles_ConfirmaExistenciaDelCliente_v1", true).Copy();
            return dt;
        }


        public static DataTable GetDocument(int iEmpresa, int iUser)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("office", iUser));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneDocumentoParaSucursal_v1", true).Copy();
            return dt;
        }


        public static DataTable GetTypeDocument(int iEmpresa, int iUser)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("user", iUser));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneTipoDeDocumento_v1", true).Copy();
            return dt;
        }


        public static DataTable GetClasAlmacen(int iEmpresa, int iNumero)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("number", iNumero));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneTipoDeAlmacen_v1", true).Copy();
            return dt;
        }



        public static DataTable GetDataMail(int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneDatosDeEmailDeEmpresa_v1", true).Copy();
            return dt;
        }


        public static DataTable GetSearchPerson(int iEmpresa, int iFamilia, string sQry)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("family", iFamilia));
            context.Parametros.Add(new SqlParameter("sql", sQry));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneBusquedaDePersona_v2", true).Copy();
            return dt;
        }


        public static DataTable GetSearchProvider(int iEmpresa, string sQry)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("sql", sQry));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneBusquedaDelProveedor_v1", true).Copy();
            return dt;
        }


        public static DataTable GetProducts(int iEmpresa, string sParametro, int iTipoProducto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", iEmpresa));
            context.Parametros.Add(new SqlParameter("parameter", sParametro));
            context.Parametros.Add(new SqlParameter("producttype", iTipoProducto));

            dt = context.ExecuteProcedure("sp_WSProductos_ObtieneProductos_v1", true).Copy();
            return dt;
        }

        public static DataTable gfObtieneDocumentos(int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));

            dt = context.ExecuteProcedure("sp_Devoluciones_LLenaDrpDocumentos", true).Copy();
            return dt;
        }
        public static DataTable gfObtieneSeries(int iEmpresa, int iTipodocto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("tipodocto", iTipodocto));

            dt = context.ExecuteProcedure("sp_Devoluciones_LLenaDrpSeries", true).Copy();
            return dt;
        }

        public static DataTable gfObtieneListaPrecioCliente(int iEmpresa, int iCliente)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("numerocliente", iCliente));

            dt = context.ExecuteProcedure("sp_ListaPreciosCliente", true).Copy();
            return dt;
        }


        public static DataTable GetDataPay(int iEmpresa, int iUsuario, DateTime fechaPago, int iMoneda)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Numero_Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("Fecha", fechaPago));
            context.Parametros.Add(new SqlParameter("Moneda", iMoneda));

            dt = context.ExecuteProcedure("sp_cuentas_cobrar_cobranza_trae_datos", true).Copy();
            return dt;
        }


        public static DataTable GetMultipleSucursal(int iEmpresa, string sSucursal)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Nombre_Sucursal", sSucursal));

            dt = context.ExecuteProcedure("sp_CC_Multiple_Sucursal", true).Copy();
            return dt;
        }


        public static DataTable ObtenCausasCancelacion(int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));

            dt = context.ExecuteProcedure("sp_CC_Elimina_Causa", true).Copy();
            return dt;
        }
        public static DataTable GetCustomerExistingFacturaLibre(int intNumeroEmpresa, string sNombreCompleto)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("numeroempresa", intNumeroEmpresa));
            context.Parametros.Add(new SqlParameter("nombrecompleto", sNombreCompleto));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneExistenciaDelCliente_v2", true).Copy();
            return dt;
        }


        public static DataTable ObtenMetodoPagoEmpresa(int intEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", intEmpresa));

            dt = context.ExecuteProcedure("sp_Metodo_Pago", true).Copy();
            return dt;
        }

        public static DataTable VerificaUsuarioEnLinea(int usuario, string password)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Usuario", usuario));
            context.Parametros.Add(new SqlParameter("@Contraseña", password));

            dt = context.ExecuteProcedure("sp_VerificaUsuarioEnLinea", true).Copy();
            return dt;
        }

        public static DataTable GetDatosBancoAux(int numEmpresa, int numUsuario, int numBanco)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", numUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Banco", numBanco));
            dt = context.ExecuteProcedure("sp_ObtieneDatosBanco", true).Copy();
            return dt;
        }
        #region Merge
        public static DataTable gfUserCheckOut(int iUser, string sPWD, int sCompany, int sOffice)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            string sOS_BROWSER = string.Empty;
            try
            {
                HttpBrowserCapabilities brObject = HttpContext.Current.Request.Browser;
                sOS_BROWSER = "Tipo:" + brObject.Type + "|Version: " + brObject.Version;
                sOS_BROWSER = (sOS_BROWSER.Length > 50) ? sOS_BROWSER.Substring(0, 50) : sOS_BROWSER;
            }
            catch (Exception)
            {
                sOS_BROWSER = "localhost";
            }

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Idusario", iUser));
            context.Parametros.Add(new SqlParameter("@PWD", sPWD));
            context.Parametros.Add(new SqlParameter("@OS_BROWSER", sOS_BROWSER));
            context.Parametros.Add(new SqlParameter("@MODULE", string.Concat(iUser, "|", sCompany, "|", sOffice)));
            context.Parametros.Add(new SqlParameter("@COMPANY", sCompany));
            // parameter user 0 = check in,  1 =   check out 
            context.Parametros.Add(new SqlParameter("@OPCION", "1"));
            dt = context.ExecuteProcedure("sp_VerificaUsuarioEnLinea_V2", true).Copy();
            return dt;
        }

        public static DataTable gfUserCheckExist(int iUser, string sPWD, int sCompany, int sOffice)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();
            string sOS_BROWSER = string.Empty;
            try
            {
                HttpBrowserCapabilities brObject = HttpContext.Current.Request.Browser;
                sOS_BROWSER = "Tipo:" + brObject.Type + "|Version: " + brObject.Version;
                sOS_BROWSER = (sOS_BROWSER.Length > 50) ? sOS_BROWSER.Substring(0, 50) : sOS_BROWSER;
            }
            catch (Exception)
            {
                sOS_BROWSER = "localhost";
            }

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Idusario", iUser));
            context.Parametros.Add(new SqlParameter("@PWD", sPWD));
            context.Parametros.Add(new SqlParameter("@OS_BROWSER", sOS_BROWSER));
            context.Parametros.Add(new SqlParameter("@MODULE", string.Concat(iUser, "|", sCompany, "|", sOffice)));
            context.Parametros.Add(new SqlParameter("@COMPANY", sCompany));
            context.Parametros.Add(new SqlParameter("@OPCION", "0"));
            dt = context.ExecuteProcedure("sp_VerificaUsuarioEnLinea_V2", true).Copy();
            return dt;
        }

        #endregion 
    }
}
