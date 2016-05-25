using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using IntegraData;


namespace IntegraBussines
{
    public class ConfiguracionVentaController
    {
        public DataTable obtieneConfiguracionVenta(int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneValidacionDeVentas_v1", true);

            if (dt != null)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }

        public DataTable obtieneValidacionDeEmpresa(int iEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@companynumber", iEmpresa));

            dt = context.ExecuteProcedure("sp_Venta_ObtieneValidacionDeTiendasYMailDeCompania_v1", true);

            if (dt != null)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }
        public static DataTable ConsultaConceptosContables(int iNumeroEmpresa, int iTipo)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumeroEmpresa));
            context.Parametros.Add(new SqlParameter("@Tipo", iTipo));
            dt = context.ExecuteProcedure("[sp_configuracion_ventas_controller]", true).Copy();
            return dt;
        }
        public static bool Modifica(int Tipo, int Numero_Empresa, bool Salida_Automatica, bool Valida_Costo_Referencia, int Dias_Plazo_Factura, bool Aut_Venta_Credito, bool Solo_Prod_Lista, bool Cancelacion_Automatica_Cot, bool Cancel_Fac_Estatus_Cotiza, int Concepto_Contable, bool ESTATUS_CFDI_SAT)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo", Tipo));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Salida_Automatica", Salida_Automatica));
            context.Parametros.Add(new SqlParameter("@Valida_Costo_Referencia", Valida_Costo_Referencia));
            context.Parametros.Add(new SqlParameter("@Dias_Plazo_Factura", Dias_Plazo_Factura));
            context.Parametros.Add(new SqlParameter("@Aut_Venta_Credito", Aut_Venta_Credito));
            context.Parametros.Add(new SqlParameter("@Solo_Prod_Lista", Solo_Prod_Lista));
            context.Parametros.Add(new SqlParameter("@Cancelacion_Automatica_Cot", Cancelacion_Automatica_Cot));
            context.Parametros.Add(new SqlParameter("@Cancel_Fac_Estatus_Cotiza", Cancel_Fac_Estatus_Cotiza));
            context.Parametros.Add(new SqlParameter("@Concepto_Contable", Concepto_Contable));
            context.Parametros.Add(new SqlParameter("@ESTATUS_CFDI_SAT", ESTATUS_CFDI_SAT));
            dt = context.ExecuteProcedure("[sp_Ventas_Configuracion_V2]", true).Copy();
            return true;
        }
    }
}
