using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IntegraData;


namespace IntegraBussines
{
    // consultas entradas y salidas almacén
    public class AlmacenConsulta_E_S
    {

        public int intEmpresa { get; set; }
        public int? intUsuario { get; set; }
        public int? intAlmacen { get; set; }
        public string sAlmacen { get; set; }
        public int? Numero_De { get; set; }
        public int? Numero_Hasta { get; set; }
        public DateTime? Fecha_De { get; set; }
        public DateTime? Fecha_Hasta { get; set; }
        public int? Estatus { get; set; }
        public int? Concepto { get; set; }
        public int? Tipo_Docto { get; set; }
        public int? Num_Docto { get; set; }


        public DataTable sp_Entrada_Almacen_Busca()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@sAlmacen", sAlmacen));

            dt = context.ExecuteProcedure("[sp_Entrada_Almacen_Busca]", true).Copy();

            return dt;
        }

        public DataTable sp_EntradaAlmacen_Busca_Numero()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_EntradaAlmacen_Busca_Numero]", true).Copy();

            return dt;
        }

        public DataTable sp_EntradaAlmacen_Busca_Estatus()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_EntradaAlmacen_Busca_Estatus]", true).Copy();

            return dt;
        }

        public DataTable sp_EntradaAlmacen_Busca_Concepto()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_EntradaAlmacen_Busca_Concepto]", true).Copy();

            return dt;
        }

        public DataTable sp_EntradaAlmacen_Busca_TipoEntrada()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_EntradaAlmacen_Busca_TipoEntrada]", true).Copy();

            return dt;
        }

        public DataTable sp_EntradaAlmacen_Busca_Usuario()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_EntradaAlmacen_Busca_Usuario]", true).Copy();

            return dt;
        }

        public DataTable sp_EntradaAlmacen_Busca_Grid()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Entrada_De", Numero_De.HasValue ? (object)Numero_De.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Entrada_Hasta", Numero_Hasta.HasValue ? (object)Numero_Hasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_De", Fecha_De.HasValue ? (object)Fecha_De.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Hasta", Fecha_Hasta.HasValue ? (object)Fecha_Hasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Estatus", Estatus.HasValue ? (object)Estatus.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Concepto", Concepto.HasValue ? (object)Concepto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Docto", Tipo_Docto.HasValue ? (object)Tipo_Docto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Num_Docto", Num_Docto.HasValue ? (object)Num_Docto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario.HasValue ? (object)intUsuario.Value : DBNull.Value));
         
            dt = context.ExecuteProcedure("[sp_EntradaAlmacen_Busca_Grid]", true).Copy();

            return dt;
        }


        public DataTable sp_EntradaAlmacen_Busca_Impresion()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Entrada_De", Numero_De.HasValue ? (object)Numero_De.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Entrada_Hasta", Numero_Hasta.HasValue ? (object)Numero_Hasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_De", Fecha_De.HasValue ? (object)Fecha_De.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Hasta", Fecha_Hasta.HasValue ? (object)Fecha_Hasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Estatus", Estatus.HasValue ? (object)Estatus.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Concepto", Concepto.HasValue ? (object)Concepto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Docto", Tipo_Docto.HasValue ? (object)Tipo_Docto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Num_Docto", Num_Docto.HasValue ? (object)Num_Docto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario.HasValue ? (object)intUsuario.Value : DBNull.Value));

      


            dt = context.ExecuteProcedure("[sp_EntradaAlmacen_Busca_Impresion]", true).Copy();

            return dt;
        }



        ////////CONSULTAS SALIDAS ALMACEN////////////////////


        public DataTable sp_Salida_Almacen_Busca()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@sAlmacen", sAlmacen));

            dt = context.ExecuteProcedure("[sp_Salida_Almacen_Busca]", true).Copy();

            return dt;
        }

        public DataTable sp_SalidaAlmacen_Busca_Numero()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_SalidaAlmacen_Busca_Numero]", true).Copy();

            return dt;
        }

        public DataTable sp_SalidaAlmacen_Busca_Estatus()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_SalidaAlmacen_Busca_Estatus]", true).Copy();

            return dt;
        }

        public DataTable sp_SalidaAlmacen_Busca_Concepto()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_SalidaAlmacen_Busca_Concepto]", true).Copy();

            return dt;
        }

        public DataTable sp_SalidaAlmacen_Busca_TipoSalida()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_SalidaAlmacen_Busca_TipoSalida]", true).Copy();

            return dt;
        }

        public DataTable sp_SalidaAlmacen_Busca_Usuario()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));


            dt = context.ExecuteProcedure("[sp_SalidaAlmacen_Busca_Usuario]", true).Copy();

            return dt;
        }

        public DataTable sp_SalidaAlmacen_Busca_Grid()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Salida_De", Numero_De.HasValue ? (object)Numero_De.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Salida_Hasta", Numero_Hasta.HasValue ? (object)Numero_Hasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_De", Fecha_De.HasValue ? (object)Fecha_De.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Hasta", Fecha_Hasta.HasValue ? (object)Fecha_Hasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Estatus", Estatus.HasValue ? (object)Estatus.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Concepto", Concepto.HasValue ? (object)Concepto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Docto", Tipo_Docto.HasValue ? (object)Tipo_Docto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Num_Docto", Num_Docto.HasValue ? (object)Num_Docto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario.HasValue ? (object)intUsuario.Value : DBNull.Value));

            dt = context.ExecuteProcedure("[sp_SalidaAlmacen_Busca_Grid]", true).Copy();

            return dt;
        }


        public DataTable sp_SalidaAlmacen_Busca_Impresion()
        {
            DataTable dt = new DataTable();

            SQLConection context = new SQLConection();
            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", intEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Almacen", intAlmacen.HasValue ? (object)intAlmacen.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Salida_De", Numero_De.HasValue ? (object)Numero_De.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Numero_Salida_Hasta", Numero_Hasta.HasValue ? (object)Numero_Hasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_De", Fecha_De.HasValue ? (object)Fecha_De.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Fecha_Hasta", Fecha_Hasta.HasValue ? (object)Fecha_Hasta.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Estatus", Estatus.HasValue ? (object)Estatus.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Concepto", Concepto.HasValue ? (object)Concepto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Tipo_Docto", Tipo_Docto.HasValue ? (object)Tipo_Docto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Num_Docto", Num_Docto.HasValue ? (object)Num_Docto.Value : DBNull.Value));
            context.Parametros.Add(new SqlParameter("@Usuario", intUsuario.HasValue ? (object)intUsuario.Value : DBNull.Value));




            dt = context.ExecuteProcedure("[sp_SalidaAlmacen_Busca_Impresion]", true).Copy();

            return dt;
        }

    }
}
