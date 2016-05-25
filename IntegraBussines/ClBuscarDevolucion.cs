using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using IntegraData;

namespace IntegraBussines
{
    public class ClBuscarDevolucion
    {
        public ClBuscarDevolucion()
        {
        }

        public DataTable sqlLlenarConceptoDevoluciones(int Empresa) {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Add(new SqlParameter("@Numero_Empresa", "" + Empresa + ""));

                DataTable dt = new DataTable();
                dt = context.ExecuteProcedure("sp_LlenaConceptoDevoluciones", true);

                return dt;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public DataTable sqlAutoCompleteCliente(int Empresa,string prefijo) {
            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", "" + Empresa + ""));
                context.Parametros.Add(new SqlParameter("@Nombre_Completo", "" + prefijo + ""));

                DataTable dt = new DataTable();
                dt = context.ExecuteProcedure("sp_Devoluciones_Clientes", true);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable sqlSelectTodoDevoluciones(int Empresa, int Persona, int Cliente,int ConDev, int Estatus, DateTime? FechaDe, DateTime? FechaHasta,int IdNumeroDe,int IdNumeroHasta) 
        {
            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", "" + Empresa + ""));
                context.Parametros.Add(new SqlParameter("@Persona", "" + Persona + ""));
                context.Parametros.Add(new SqlParameter("@Cliente", "" + Cliente + ""));
                context.Parametros.Add(new SqlParameter("@ConDev", "" + ConDev + ""));
                context.Parametros.Add(new SqlParameter("@Estatus", "" + Estatus + ""));
                context.Parametros.Add(new SqlParameter("@FechaDe", FechaDe.HasValue ? (object)FechaDe.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@FechaHasta", FechaHasta.HasValue ? (object)FechaHasta.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@NumeroDe",""+IdNumeroDe+""));
                context.Parametros.Add(new SqlParameter("@NumeroHasta", "" + IdNumeroHasta + ""));

                DataTable dt = new DataTable();
                dt = context.ExecuteProcedure("sp_Consultar_Devoluciones_Cliente_V2", true);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable sqlAutoCompleteClienteEntradaPorDevolucion(int Empresa, string prefijo)
        {
            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", "" + Empresa + ""));
                context.Parametros.Add(new SqlParameter("@Nombre_Completo", "" + prefijo + ""));

                DataTable dt = new DataTable();
                dt = context.ExecuteProcedure("sp_Clientes_Devoluciones", true);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    } 
}
