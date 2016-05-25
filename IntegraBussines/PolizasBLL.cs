using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;
using System.Data.SqlClient;

namespace IntegraBussines
{
    public  class PolizasBLL
    {
        public DataTable ObtieneTipoPolizas(int numEmpresa) 
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));

            dt = context.ExecuteProcedure("sp_ObtieneTipopolizas", true).Copy();
            return dt;
        }

        public DataTable ObtienePolizasImportadas(int numEmpresa, int numUsuario, DateTime? Fecha, DateTime? FechaFin, int tipoPoliza, int numPolizaDel, int numPolizaAl)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", numUsuario));
            context.Parametros.Add(new SqlParameter("@Fecha", Fecha.HasValue ? (object)Fecha.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Fin", FechaFin.HasValue ? (object)FechaFin.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Poliza", tipoPoliza));
            context.Parametros.Add(new SqlParameter("@Num_PolizaDel", numPolizaDel));
            context.Parametros.Add(new SqlParameter("@Num_PolizaAl", numPolizaAl));

            dt = context.ExecuteProcedure("sp_Obtiene_Polizas_Importadas_v2", true).Copy();
            return dt;
        }
        public DataTable ObtieneEncabezadoPolizaImportadas(int numEmpresa, int numPersona, int tipoPoliza) 
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Persona", numPersona));
            context.Parametros.Add(new SqlParameter("@TipoPoliza", tipoPoliza));

            dt = context.ExecuteProcedure("sp_ObtieneDatosEmpresaPoliza", true).Copy();
            return dt;
        
        }

        
        public DataTable ObtienePulizasImportadasLLenarGrid(int numEmpresa, int numUsuario, DateTime? Fecha, DateTime? FechaFin, int tipoPoliza, int numPolizaDel, int numPolizaAl)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", numUsuario));
            context.Parametros.Add(new SqlParameter("@Fecha", Fecha.HasValue ? (object)Fecha.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Fin", FechaFin.HasValue ? (object)FechaFin.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Poliza", tipoPoliza));
            context.Parametros.Add(new SqlParameter("@Num_PolizaDel", numPolizaDel));
            context.Parametros.Add(new SqlParameter("@Num_PolizaAl", numPolizaAl));

            dt = context.ExecuteProcedure("sp_Obtiene_Datos_Polizas_Importadas", true).Copy();
            return dt;
        }

        public static DataTable ObtieneBusquedaPolizas(int intTipoMovimiento, int intNumeroEmpresa, int intTipoPoliza, DateTime? FechaDe, DateTime? FechaHasta, int? intNumeroPoliza, int? intPeriodo, int? intAnio)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo_Movimiento", intTipoMovimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intNumeroEmpresa));
            context.Parametros.Add(new SqlParameter("@Tipo_Poliza", intTipoPoliza));
            context.Parametros.Add(new SqlParameter("@Fecha_De", FechaDe.HasValue ? (object)FechaDe.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Hasta", FechaHasta.HasValue ? (object)FechaHasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Poliza", intNumeroPoliza.HasValue ? (object)intNumeroPoliza.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Periodo", intPeriodo.HasValue ? (object)intPeriodo.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Anio", intAnio.HasValue ? (object)intAnio.Value : DBNull.Value));
            dt = context.ExecuteProcedure("sp_Contabilidad_BusquedaPolizas", true).Copy();
            return dt;
        }



        public static DataTable ObtieneCuentasContables(int intOpcion, int intNumeroEmpresa, int? intNumero)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Opcion", intOpcion));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intNumeroEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero", intNumero.HasValue ? (object)intNumero.Value : DBNull.Value));
         
            dt = context.ExecuteProcedure("sp_CuentasContables", true).Copy();
            return dt;
        }
        public static DataTable gfsp_C_Polizas_Tipo(int iMovimiento,int iNumero_Empresa, int? iNumPartida,int? iNumero,int? intNumeroPoliza,string sNombreTipo,string sTitulo,int? intTipoPoliza, int? intClasCuenta,int? intCargoAbono,
                                                    string sConcepto,int? intFamilia)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Movimiento", iMovimiento));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@NumPartida", iNumPartida.HasValue ? (object)iNumPartida.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero", iNumero.HasValue ? (object)iNumero.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Poliza", intNumeroPoliza.HasValue ? (object)intNumeroPoliza.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Nombre_Tipo", sNombreTipo));
            context.Parametros.Add(new SqlParameter("@Titulo", sTitulo));
            context.Parametros.Add(new SqlParameter("@Tipo_Poliza", intTipoPoliza.HasValue ? (object)intTipoPoliza.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Clas_Cuenta", intClasCuenta.HasValue ? (object)intClasCuenta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Cargo_Abono", intCargoAbono.HasValue ? (object)intCargoAbono.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Concepto", sConcepto));
            context.Parametros.Add(new SqlParameter("@Familia", intFamilia.HasValue ? (object)intFamilia.Value : DBNull.Value));

            dt = context.ExecuteProcedure("[sp_C_Polizas_Tipo]", true).Copy();
            if (dt != null)
                return dt;
            else
                return new DataTable();

        }

        public DataTable GetNivelAuxiliar(int tipo, int numEmpresa, string CuentaMayor, string Nivel1, string Nivel2) 
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo", tipo));
            context.Parametros.Add(new SqlParameter("@Numero_empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@NumCuenta_Mayor", CuentaMayor));
            context.Parametros.Add(new SqlParameter("@NumCuenta_N1", Nivel1));
            context.Parametros.Add(new SqlParameter("@NumCuenta_N2", Nivel2));

            dt=context.ExecuteProcedure("spObtieneNivelesAuxiliares",true).Copy();
            return dt;
        }

        public DataTable ObtieneReportesContables(int numEmpresa, int tipoPoliza, DateTime? FechaInicio, DateTime? FechaFin, string PeriodoIni, string PeriodoFin, string AñoInicio, string AñoFin,
                                                  int numPolizaDe, int numPolizaHasta, string cMayorIni, string cMayorFin, string N1Inicio, string N1Fin, string N2Inicio, string N2Fin, string N3Inicio, string N3Fin,
                                                  string UUID)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Tipo_Poliza", tipoPoliza));
            context.Parametros.Add(new SqlParameter("@FechFacIni", FechaInicio.HasValue ? (object)FechaInicio.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@FechFacFin", FechaFin.HasValue ? (object)FechaFin.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@PeriodoInicio", PeriodoIni));
            context.Parametros.Add(new SqlParameter("@PeriodoFin", PeriodoFin));
            context.Parametros.Add(new SqlParameter("@AñoInicio", AñoInicio));
            context.Parametros.Add(new SqlParameter("@AñoFin", AñoFin));
            context.Parametros.Add(new SqlParameter("@NumPolizaDe", numPolizaDe));
            context.Parametros.Add(new SqlParameter("@NumPolizaHasta", numPolizaHasta));
            context.Parametros.Add(new SqlParameter("@CuentaMayorIni", cMayorIni));
            context.Parametros.Add(new SqlParameter("@CuentaMayorFin", cMayorFin));
            context.Parametros.Add(new SqlParameter("@Nivel1Inicio", N1Inicio));
            context.Parametros.Add(new SqlParameter("@Nivel1Fin", N1Fin));
            context.Parametros.Add(new SqlParameter("@Nivel2Inicio", N2Inicio));
            context.Parametros.Add(new SqlParameter("@Nivel2Fin", N2Fin));
            context.Parametros.Add(new SqlParameter("@Nivel3Inicio", N3Inicio));
            context.Parametros.Add(new SqlParameter("@Nivel3Fin", N3Fin));
            context.Parametros.Add(new SqlParameter("@UUID", UUID));


            dt = context.ExecuteProcedure("spObtieneReportesContables", true).Copy();
            return dt;
        }

        public DataTable ObtieneReporteAuxiliarContable(int numEmpresa, int numUsuario, int tipoPoliza, string FechaInicio, string FechaFin, string PeriodoIni, string PeriodoFin, string AñoInicio, string AñoFin,
                                               int numPolizaDe, int numPolizaHasta, string cMayorIni, string cMayorFin, string N1Inicio, string N1Fin, string N2Inicio, string N2Fin, string N3Inicio, string N3Fin)
        //string UUID)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Usuario", numUsuario));
            context.Parametros.Add(new SqlParameter("@Fecha_Desde", FechaInicio));
            context.Parametros.Add(new SqlParameter("@Fecha_Hasta", FechaFin));

            if (cMayorIni != "-1")
            {
                context.Parametros.Add(new SqlParameter("@CuentaMayor_Desde", cMayorIni));
            }
            else
            {
                context.Parametros.Add(new SqlParameter("@CuentaMayor_Desde", DBNull.Value));
            }

            if (cMayorFin != "-1")
            {
                context.Parametros.Add(new SqlParameter("@CuentaMayor_Hasta", cMayorFin));
            }
            else
            {
                context.Parametros.Add(new SqlParameter("@CuentaMayor_Hasta", DBNull.Value));
            }

            if (N1Inicio != "-1")
            {
                context.Parametros.Add(new SqlParameter("@Nivel1_Desde", N1Inicio));
            }
            else
            {
                context.Parametros.Add(new SqlParameter("@Nivel1_Desde", DBNull.Value));
            }

            if (N1Fin != "-1")
            {
                context.Parametros.Add(new SqlParameter("@Nivel1_Hasta", N1Fin));
            }
            else
            {
                context.Parametros.Add(new SqlParameter("@Nivel1_Hasta", DBNull.Value));
            }

            if (N2Inicio != "-1")
            {
                context.Parametros.Add(new SqlParameter("@Nivel2_Desde", N2Inicio));
            }
            else
            {
                context.Parametros.Add(new SqlParameter("@Nivel2_Desde", DBNull.Value));
            }

            if (N2Fin != "-1")
            {
                context.Parametros.Add(new SqlParameter("@Nivel2_Hasta", N2Fin));
            }
            else
            {
                context.Parametros.Add(new SqlParameter("@Nivel2_Hasta", DBNull.Value));
            }

            if (N3Inicio != "-1")
            {
                context.Parametros.Add(new SqlParameter("@Nivel3_Desde", N3Inicio));
            }
            else
            {
                context.Parametros.Add(new SqlParameter("@Nivel3_Desde", DBNull.Value));
            }
            if (N3Fin != "-1")
            {
                context.Parametros.Add(new SqlParameter("@Nivel3_Hasta", N3Fin));
            }
            else
            {
                context.Parametros.Add(new SqlParameter("@Nivel3_Hasta", DBNull.Value));
            }



            dt = context.ExecuteProcedure("sp_AuxiliarTMP", true).Copy();
            return dt;
        }


        //public DataTable ObtieneReporteAuxiliarContable(int numEmpresa, int numUsuario, int tipoPoliza, DateTime? FechaInicio, DateTime? FechaFin, string PeriodoIni, string PeriodoFin, string AñoInicio, string AñoFin,
        //                                          int numPolizaDe, int numPolizaHasta, string cMayorIni, string cMayorFin, string N1Inicio, string N1Fin, string N2Inicio, string N2Fin, string N3Inicio, string N3Fin)
        //                                          //string UUID)
        //{
        //    DataTable dt = new DataTable();
        //    SQLConection context = new SQLConection();

        //    context.Parametros.Clear();
        //    context.Parametros.Add(new SqlParameter("@Numero_empresa", numEmpresa));
        //    context.Parametros.Add(new SqlParameter("@Usuario", numUsuario));
        //    context.Parametros.Add(new SqlParameter("@Tipo_Poliza", tipoPoliza));
        //    context.Parametros.Add(new SqlParameter("@FechFacIni", FechaInicio.HasValue ? (object)FechaInicio.Value : DBNull.Value));
        //    context.Parametros.Add(new SqlParameter("@FechFacFin", FechaFin.HasValue ? (object)FechaFin.Value : DBNull.Value));
        //    context.Parametros.Add(new SqlParameter("@PeriodoInicio", PeriodoIni));
        //    context.Parametros.Add(new SqlParameter("@PeriodoFin", PeriodoFin));
        //    context.Parametros.Add(new SqlParameter("@AñoInicio", AñoInicio));
        //    context.Parametros.Add(new SqlParameter("@AñoFin", AñoFin));
        //    context.Parametros.Add(new SqlParameter("@NumPolizaDe", numPolizaDe));
        //    context.Parametros.Add(new SqlParameter("@NumPolizaHasta", numPolizaHasta));
        //    context.Parametros.Add(new SqlParameter("@CuentaMayorIni", cMayorIni));
        //    context.Parametros.Add(new SqlParameter("@CuentaMayorFin", cMayorFin));
        //    context.Parametros.Add(new SqlParameter("@Nivel1Inicio", N1Inicio));
        //    context.Parametros.Add(new SqlParameter("@Nivel1Fin", N1Fin));
        //    context.Parametros.Add(new SqlParameter("@Nivel2Inicio", N2Inicio));
        //    context.Parametros.Add(new SqlParameter("@Nivel2Fin", N2Fin));
        //    context.Parametros.Add(new SqlParameter("@Nivel3Inicio", N3Inicio));
        //    context.Parametros.Add(new SqlParameter("@Nivel3Fin", N3Fin));
        //    //context.Parametros.Add(new SqlParameter("@UUID", UUID));


        //    dt = context.ExecuteProcedure("spObtieneReportesAuxiliarContableV2", true).Copy();
        //    return dt;
        //}


        public static DataTable gfsp_Proveedor_TerminalCuenta(int iNumero_Empresa,int iNumero)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
          
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero", iNumero));
           

            dt = context.ExecuteProcedure("[sp_Proveedor_TerminalCuenta]", true).Copy();
            if (dt != null)
                return dt;
            else
                return new DataTable();

        }

        public static DataTable gfsp_Proveedor_Cuenta(int iNumero_Empresa, int iNumeroProveedor, int iNumeroPadre)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iNumero_Empresa));
            context.Parametros.Add(new SqlParameter("@Numero_Proveedor", iNumeroProveedor));
            context.Parametros.Add(new SqlParameter("@Numero_Padre", iNumeroPadre));

            dt = context.ExecuteProcedure("[sp_Proveedor_Cuenta]", true).Copy();
            if (dt != null)
                return dt;
            else
                return new DataTable();

        }
        

    } 
}
