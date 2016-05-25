using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;
using System.Data.SqlClient;



namespace IntegraBussines
{
    public class FacturacionBLL
    {



        public DataTable ObtieneConsultaFactura(int iTipo, int iEmpresa, DateTime Fechaini, DateTime FechaFin, int? iCliente, int? iTipoDocto, int? iSerie, int? iNumIni, int? iNumFin, decimal? dImporteIni, decimal? dImporteFin)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("Tipo", iTipo));
            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Fecha_Ini", Fechaini));
            context.Parametros.Add(new SqlParameter("Fecha_Fin", FechaFin));
            context.Parametros.Add(new SqlParameter("Numero_Cliente", iCliente.HasValue ? (object)iCliente.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Tipo_Docto", iTipoDocto.HasValue ? (object)iTipoDocto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Serie", iSerie.HasValue ? (object)iSerie.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Num_Ini", iNumIni.HasValue ? (object)iNumIni.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Num_Fin", iNumFin.HasValue ? (object)iNumFin.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Importe_Ini", dImporteIni.HasValue ? (object)dImporteIni.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Importe_Fin", dImporteFin.HasValue ? (object)dImporteFin.Value : DBNull.Value));

            dt = context.ExecuteProcedure("sp_Factura_Consulta", true).Copy();
            return dt;
        }
        //checar , parece no se ocupa
        //public bool AntiguedadSaldosSucursal(int iEmpresa, int iUsuario, DateTime? Date, int? iSucursal, int? iZona, int? iCliente, int? iTipoCliente,
        //                     int? iMoneda, int? iFormaPagoFiltro, int iPeriodo1, int iPeriodo2, int iPeriodo3, int iPeriodo4)
        //{
        //    IntegraData.FacturacionDAL Factura = new IntegraData.FacturacionDAL();

        //    try
        //    {
        //        return AntiguedadSaldosSucursalBool(iEmpresa, iUsuario, Date, iSucursal, iZona, iCliente, iTipoCliente, iMoneda, iFormaPagoFiltro, iPeriodo1, iPeriodo2, iPeriodo3, iPeriodo4);
        //    }

        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public bool AntiguedadSaldosSucursalBool(int iEmpresa, int iUsuario, DateTime? Date, int? iSucursal, int? iZona, int? iCliente, int? iTipoCliente,
        //                     int? iMoneda, int? iFormaPagoFiltro, int iPeriodo1, int iPeriodo2, int iPeriodo3, int iPeriodo4)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {

        //        new SqlParameter("Numero_Empresa", iEmpresa),
        //        new SqlParameter("Numero_Usuario", iUsuario),
        //        new SqlParameter("Fecha", Date.HasValue ? (object)Date.Value : DBNull.Value),
        //        new SqlParameter("Clas_Sucursal1", iSucursal.HasValue ? (object)iSucursal.Value : DBNull.Value),
        //        new SqlParameter("Clas_Zona1", iZona.HasValue ? (object)iZona.Value : DBNull.Value),
        //        new SqlParameter("Numero_Cliente", iCliente.HasValue ? (object)iCliente.Value : DBNull.Value),
        //        new SqlParameter("Tipo_Cliente", iTipoCliente.HasValue ? (object)iTipoCliente.Value : DBNull.Value),
        //        new SqlParameter("Clas_Moneda",iMoneda.HasValue ? (object)iMoneda.Value : DBNull.Value),
        //        new SqlParameter("Forma_Pago_Filtro", iFormaPagoFiltro.HasValue ? (object)iFormaPagoFiltro.Value : DBNull.Value),
        //        new SqlParameter("Periodo1", iPeriodo1),
        //        new SqlParameter("Periodo2", iPeriodo2),
        //        new SqlParameter("Periodo3", iPeriodo3),
        //        new SqlParameter("Periodo4", iPeriodo4),

        //    };

        //    return sqlDBHelper.ExecuteNonQuery2("sp_Antiguedad_Saldos_Sucursal", CommandType.StoredProcedure, parameters);
        //}



        //public DataTable ObtenAntiguedadSaldosSucursal(int iCompany, int iUsuario, int iTipoConsulta)
        //{
        //    IntegraData.FacturacionDAL Factura = new IntegraData.FacturacionDAL();

        //    return Factura.ObtenAntiguedadSaldosSucursal(iCompany, iUsuario,iTipoConsulta);
        //}

        //public DataTable ObtenAntiguedadSaldosSucursal(int iEmpresa, int iUsuario, int iTipoConsulta)
        //{
        //    DataTable dt = new DataTable();
        //    SQLConection context = new SQLConection();
        //    context.Parametros.Clear();

        //    context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
        //    context.Parametros.Add(new SqlParameter("Usuario", iUsuario));
        //    context.Parametros.Add(new SqlParameter("Tipo", iTipoConsulta));



        //    dt = context.ExecuteProcedure("spc_Antiguedad_Saldos_Consulta", true).Copy();
        //    return dt;
        //}


        public DataTable ObtenFacturasCliente(int iEmpresa, int iUsuario, int iTipo, int? iCliente, DateTime Fechaini, DateTime FechaFin, int iMoneda, int? iSucursal)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Numero_Usuario", iUsuario));
            context.Parametros.Add(new SqlParameter("Tipo", iTipo));
            context.Parametros.Add(new SqlParameter("Numero_Cliente", iCliente.HasValue ? (object)iCliente.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Fecha_De", Fechaini));
            context.Parametros.Add(new SqlParameter("Fecha_Hasta", FechaFin));
            context.Parametros.Add(new SqlParameter("Clas_Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("Clas_Sucursal", iSucursal.HasValue ? (object)iSucursal.Value : DBNull.Value));


            dt = context.ExecuteProcedure("sp_cc_Obten_Facturas_ClienteFolioFiscal", true).Copy();
            return dt;
        }


        public DataTable ObtenPagos(int iEmpresa, int iUsuario, string sCliente, int iMoneda, DateTime? FechaIni, DateTime? FechaFin)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Num_Persona", iUsuario));
            context.Parametros.Add(new SqlParameter("Des_Persona", sCliente));
            context.Parametros.Add(new SqlParameter("Moneda", iMoneda));
            context.Parametros.Add(new SqlParameter("Fecha_Ini", FechaIni.HasValue ? (object)FechaIni.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Fecha_Fin", FechaFin.HasValue ? (object)FechaFin.Value : DBNull.Value));

            dt = context.ExecuteProcedure("sp_cc_Pagos_Consulta", true).Copy();
            return dt;
        }


        public bool CancelaPago(int iEmpresa, int iUsuario, int iNumeroPago, DateTime FechaCancelacion, int iConceptoCancelacion)
        {
            IntegraData.FacturacionDAL DatosCancelaPagos = new IntegraData.FacturacionDAL();

            return DatosCancelaPagos.CancelaPago(iEmpresa, iUsuario, iNumeroPago, FechaCancelacion, iConceptoCancelacion);
        }

        /// ///////////////////////////////////////////FACTURACIÓN PROGRAMADA ////////////////////////////////////

        public DataTable ObtenCotizacionesFacturasProgramadas(int iEmpresa, int? iNumCotIni, int? iNumCotFin, int? iCliente, DateTime? Fechaini, DateTime? FechaFin)
        {
            DataTable dt = new DataTable();
            SQLConection context = new SQLConection();
            context.Parametros.Clear();

            context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
            context.Parametros.Add(new SqlParameter("Numero_Cotiza_Ini", iNumCotIni.HasValue ? (object)iNumCotIni.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Numero_Cotiza_Fin", iNumCotFin.HasValue ? (object)iNumCotFin.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Numero_Cliente", iCliente.HasValue ? (object)iCliente.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Fecha_Ini", Fechaini.HasValue ? (object)Fechaini.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("Fecha_Fin", FechaFin.HasValue ? (object)FechaFin.Value : DBNull.Value));

            dt = context.ExecuteProcedure("sp_cotizacion_detalle_pagos_consulta", true).Copy();
            return dt;
        }



        public bool ActualizaEstatusCotizacionPagos(int intNumeroEmpresa, int intNumeroCotizacion, int intNumeroPartida, int intNumeroPago, int intUsuario)
        {
            try
            {
                SQLConection context = new SQLConection();
                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", intNumeroEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Cotiza", intNumeroCotizacion));
                context.Parametros.Add(new SqlParameter("@Numero_Partida", intNumeroPartida));
                context.Parametros.Add(new SqlParameter("@Numero_Pago", intNumeroPago));
                context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));


                context.ExecuteProcedure("sp_Actualiza_Cotizacion_Detalle_Pagos", true).Copy();
                return true;

            }
            catch
            {
                return false;
            }
        }

        //public DataTable sp_factura_programada(int iEmpresa, int iUsuario, int iTipoDocto, int iSerie, int iNumeroDocumento, int intId)
        //{
        //    DataTable dt = new DataTable();
        //    SQLConection context = new SQLConection();
        //    context.Parametros.Clear();

        //    context.Parametros.Add(new SqlParameter("Numero_Empresa", iEmpresa));
        //    context.Parametros.Add(new SqlParameter("Usuario", iUsuario));
        //    context.Parametros.Add(new SqlParameter("Tipo_Docto", iTipoDocto));
        //    context.Parametros.Add(new SqlParameter("Serie", iSerie));
        //    context.Parametros.Add(new SqlParameter("Numero_Documento", iNumeroDocumento));
        //    context.Parametros.Add(new SqlParameter("ID", intId));

        //    dt = context.ExecuteProcedure("sp_factura_programada", true).Copy();
        //    return dt;
        //}

        //public int sp_factura_programada(int iEmpresa, int iUsuario, int iTipoDocto, int iSerie, int iNumeroDocumento, int intId)
        //{
        //    SQLConection context = new SQLConection();

        //    context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));
        //    context.Parametros.Add(new SqlParameter("@Usuario", iUsuario));
        //    context.Parametros.Add(new SqlParameter("@Tipo_Docto", iTipoDocto));
        //    context.Parametros.Add(new SqlParameter("@Serie", iSerie));
        //    context.Parametros.Add(new SqlParameter("@Numero_Documento", iNumeroDocumento));
        //    context.Parametros.Add(new SqlParameter("@ID", intId));



        //    if (context.Parametros != null)
        //    {
        //        foreach (SqlParameter param in context.Parametros)
        //        {
        //            if (param.ParameterName == "ID" && intId == 0)
        //            {
        //                param.Direction = ParameterDirection.Output;
        //            }
        //        }
        //    }




        //    return sqlDBHelper.ExecuteNonQueryOutputValue("sp_factura_programada", "ID", CommandType.StoredProcedure, parameters);


        //}


        public int sp_factura_programada(int iEmpresa, int iUsuario, int iTipoDocto, int iSerie, int iNumeroDocumento, int intId)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                       
            new SqlParameter("Numero_Empresa", iEmpresa),
            new SqlParameter("Usuario", iUsuario),
            new SqlParameter("Tipo_Docto", iTipoDocto),
            new SqlParameter("Serie", iSerie),
            new SqlParameter("Numero_Documento", iNumeroDocumento),
            new SqlParameter("ID", intId),
          
        };
            try
            {


                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        if (param.ParameterName == "ID" && intId == 0)
                        {
                            param.Direction = ParameterDirection.Output;
                        }
                    }
                }

                return sqlDBHelper.ExecuteNonQueryOutputValue("sp_factura_programada", "ID", CommandType.StoredProcedure, parameters);


            }
            catch (Exception)
            {

                return -1;
            }

        }


    }
}
