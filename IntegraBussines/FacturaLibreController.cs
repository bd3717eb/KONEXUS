using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;

using System.Data.SqlClient;

namespace IntegraBussines
{
  public  class FacturaLibreController
    {

        public DataTable ObtenUnidadesDeMedida(int intEmpresa)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", intEmpresa));

            dt = context.ExecuteProcedure("sp_Trae_UM", true).Copy();
            return dt;
        }

        public DataTable GetProductosAutocomplete(int Numero_Empresa, string Descripcion)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("Descripcion", Descripcion));
            dt = context.ExecuteProcedure("sp_Productos_Conceptos_Consulta", true).Copy();
            return dt;

        }

        public string ObtenNumeroTicket(int intItipo, string sDatabase, string sEmpresa, string sSucursal, string sTipoDocto,
                                        string sSerie, string sFolio, string sNumtkt)
        {
            DataTable dt = new DataTable();
            try
            {

                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("Tipo", intItipo));
                context.Parametros.Add(new SqlParameter("Nombre_Base", sDatabase));
                context.Parametros.Add(new SqlParameter("Numero_Empresa", sEmpresa));
                context.Parametros.Add(new SqlParameter("Sucursal", sSucursal));
                context.Parametros.Add(new SqlParameter("Tipo_Docto", sTipoDocto));
                context.Parametros.Add(new SqlParameter("Serie", sSerie));
                context.Parametros.Add(new SqlParameter("Numero_Docto", sFolio));
                context.Parametros.Add(new SqlParameter("Num_Ticket", sNumtkt));
              
                dt = context.ExecuteProcedure("sp_Obtener_Num_Ticket", true).Copy();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0] != DBNull.Value)
                    {
                        return Convert.ToString(dt.Rows[0][0]);
                    }
                }

            }
            catch 
            {
               return null;
                
            }
            return null;
           

        }

        public DataTable ObtenNumeroCotizacion(int intNumeroEmpresa, int intNumeroFactura)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Empresa", intNumeroEmpresa));
            context.Parametros.Add(new SqlParameter("NumeroFactura", intNumeroFactura));

            dt = context.ExecuteProcedure("Sp_Trae_Numero_Cotizacion", true).Copy();
            return dt;

        }
        public static int gfFacturaActualizaPersonaFisica(decimal RET_ISR, decimal RET_IVA, decimal TotalTotal, int iIDFacturaNext, int iClientNumber, int iNumeroEmpresa)
        {
            int iRespuesta = 0;
            try 
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();

                context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumeroEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Cliente", iClientNumber));
                context.Parametros.Add(new SqlParameter("@Ret_Isr",  RET_ISR));
                context.Parametros.Add(new SqlParameter("@Ret_IVA", RET_IVA));
                context.Parametros.Add(new SqlParameter("@Total", TotalTotal));
                context.Parametros.Add(new SqlParameter("@Num_Factura", iIDFacturaNext));

                context.ExecuteProcedure("sp_FacturaActualizaPersonaFisica", true).Copy();
                return iRespuesta; 

            }
            catch { 
                return -1;
            }

        }
        public DataTable gfClienteDetalle(int Numero_Empresa, string Nombre_Completo)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            int iOpcion;
            context.Parametros.Clear();
            try
            {
                int.Parse(Nombre_Completo);
                iOpcion = 1;
            }
            catch
            {
                iOpcion = 0;
            }
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", Numero_Empresa));
            context.Parametros.Add(new SqlParameter("@Nombre_Completo", Nombre_Completo));
            context.Parametros.Add(new SqlParameter("@Opcion", iOpcion));
            dt = context.ExecuteProcedure("sp_WSAutoCompletaClienteDetalles", true);
            if (dt != null)
                return dt;
            else
                return new DataTable();
        }


        public bool ActualizaEstatusVenta(int NumEmpresa ,int NumFac) 
        {
            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", NumEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Fac", NumFac));

                context.ExecuteProcedure("sp_ActualizaEstatusPagada", true).Copy();
                return true;

            }
            catch 
            {
                return false;
            }
        }

        public DataTable ObtieneEstatusVenta(int NumEmpresa, int NumVenta) 
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", NumEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Factura", NumVenta));

            dt = context.ExecuteProcedure("sp_ObtieneEstatusVenta", true).Copy();
            return dt;
        }

    }
}
