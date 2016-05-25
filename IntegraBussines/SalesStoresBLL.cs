using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using IntegraData;

namespace IntegraBussines
{
    public class SalesStoresBLL
    {
        public int Company { get; set; }
        public int User { get; set; }
        public int Office { get; set; }

        protected string Validation;


        //El procedimiento no se encuentra en la base de INTEGRA_DEMO
        public int GetNumberOfVirtualStores()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", Company));
            context.Parametros.Add(new SqlParameter("usernumber", User));
            context.Parametros.Add(new SqlParameter("office", Office));

            dt = context.ExecuteProcedure("sp_PuntosDeVenta_ObtieneNumeroDeTiendasVirtuales_v1", true).Copy();

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
                else
                    return 0;
            }

        }


        //EL PROCEDIMIENTO NO SE ENCUENTRA EN LA BASE DE DATOS INTEGRA DEMO
        private DataTable GetVirtualStores()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", Company));
            context.Parametros.Add(new SqlParameter("usernumber", User));
            context.Parametros.Add(new SqlParameter("office", Office));

            dt = context.ExecuteProcedure("sp_PuntosDeVenta_ObtieneTiendasVirtuales_v1", true).Copy();
            return dt;

        }

        public static DataTable GetVirtualStores(int icompany, int iuser, int ioffice)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", icompany));
            context.Parametros.Add(new SqlParameter("usernumber", iuser));
            context.Parametros.Add(new SqlParameter("office", ioffice));

            dt = context.ExecuteProcedure("sp_PuntosDeVenta_ObtieneTiendasVirtuales_v1", true).Copy();
            return dt;
        }

      
        private DataTable GetNormalStores()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", Company));
            context.Parametros.Add(new SqlParameter("@usernumber", User));
            context.Parametros.Add(new SqlParameter("@Office", Office));

            dt = context.ExecuteProcedure("sp_PuntosDeVenta_ObtieneTiendasNormales_v1", true).Copy();
            return dt;
        }

        public static DataTable GetNormalStores(int icompany, int iuser, int ioffice)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", icompany));
            context.Parametros.Add(new SqlParameter("@usernumber", iuser));
            context.Parametros.Add(new SqlParameter("@Office", ioffice));

            dt = context.ExecuteProcedure("sp_PuntosDeVenta_ObtieneTiendasNormales_v1", true).Copy();
            return dt;
        }


        //EL PROCEDIMIENTO NO SE ENCUENTRA EN LA BASE INTEGRA_DEMO
        private DataTable GetAllStores()
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", Company));
            context.Parametros.Add(new SqlParameter("usernumber", User));

            dt = context.ExecuteProcedure("sp_PuntosDeVenta_ObtieneTodasLasTiendas_v1", true).Copy();
            return dt;
        }

        public static DataTable GetAllStores(int icompany, int iuser)
        {

            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", icompany));
            context.Parametros.Add(new SqlParameter("usernumber", iuser));

            dt = context.ExecuteProcedure("sp_PuntosDeVenta_ObtieneTodasLasTiendas_v1", true).Copy();
            return dt;
        }

        public bool DeniedProducts(int moviment, int company, int type, int salenumber, int partialsale, int store, int Tproduct, int product, int Tconcept, int concept, decimal quantity, DateTime date, int Fconcept, decimal price)
        {
            return Convert.ToInt32(IntegraData.SaleStoresDAL.DeniedProducts(moviment, company, type, salenumber, partialsale, store, Tproduct, product, Tconcept, concept, quantity, date, Fconcept, price)) > 0;
        }

        //El procedimiento no se encuentra en LA BASE INTEGRA_DEMO
        //Al parecer este evento no se utiliza en la solución
        public int GetStoreFromSaleNumber(int company, int salenumber)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("companynumber", company));
            context.Parametros.Add(new SqlParameter("salenumber", salenumber));

            dt = context.ExecuteProcedure("sp_PuntosDeVenta_ObtieneTiendaPorNumeroDeVenta_v1", true).Copy();

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                    return Convert.ToInt32(dt.Rows[0][0]);
            }

            return 0;
        }
    }
}
