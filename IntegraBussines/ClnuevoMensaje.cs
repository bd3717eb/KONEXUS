using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using IntegraData;
/// <summary>
/// Descripción breve de ClnuevoMensaje
/// </summary>
public class ClnuevoMensaje
{
    int idEmpresa;
    int idMensaje;
    string Mensaje;
    string Fuente;
    int Tamaño;
    string Color;
    string fech_Inicio;
    string fech_Venci;
    int id_Fondo;
    int tiempo;



    public int IdEmpresa
    {
        get { return idEmpresa; }
        set { idEmpresa = value; }
    }
    public int IdMensaje
    {
        get { return idMensaje; }
        set { idMensaje = value; }
    }
    public string selectMensaje
    {
        get { return Mensaje; }
        set { Mensaje = value; }
    }
    public string selectFuente
    {
        get { return Fuente; }
        set { Fuente = value; }
    }
    public int selectTamaño
    {
        get { return Tamaño; }
        set { Tamaño = value; }
    }
    public string selectColor
    {
        get { return Color; }
        set { Color = value; }
    }
    public string selectFechaIni
    {
        get { return fech_Inicio; }
        set { fech_Inicio = value; }
    }
    public string selectFechaVenci
    {
        get { return fech_Venci; }
        set { fech_Venci = value; }
    }
    public int selectIDFondo
    {
        get { return id_Fondo; }
        set { id_Fondo = value; }
    }
    public int Tiempo
    {
        get { return tiempo; }
        set { tiempo = value; }
    }


    public ClnuevoMensaje()
    {

    }

    public ClnuevoMensaje(int idMensaje)
    {
        IdMensaje = idMensaje;
    }


    public ClnuevoMensaje(string FechaSistema, int empresa)
    {
        idEmpresa = empresa;
    }


    public ClnuevoMensaje(int Tiempo, int empresa)
    {
        tiempo = Tiempo;
        idEmpresa = empresa;
    }

    public ClnuevoMensaje(string mensaje, string fuente, int tamaño, string color, string fech_inicio, string fech_venci, int id_fondo, int empresa)
    {
        Mensaje = mensaje;
        Fuente = fuente;
        Tamaño = tamaño;
        Color = color;
        fech_Inicio = fech_inicio;
        fech_Venci = fech_venci;
        id_Fondo = id_fondo;
        idEmpresa = empresa;
    }

    public ClnuevoMensaje(int idMensaje, string mensaje, string fuente, int tamaño, string color, string fech_inicio, string fech_venci, int id_fondo, int empresa)
    {
        IdMensaje = idMensaje;
        Mensaje = mensaje;
        Fuente = fuente;
        Tamaño = tamaño;
        Color = color;
        fech_Inicio = fech_inicio;
        fech_Venci = fech_venci;
        id_Fondo = id_fondo;
        idEmpresa = empresa;
    }

    public int sqlInsert(ClnuevoMensaje mj)
    {
        SQLConection context = new SQLConection();
        context.Parametros.Clear();
        context.Parametros.Add(new SqlParameter("@Mensaje_Texto", "" + mj.Mensaje + ""));
        context.Parametros.Add(new SqlParameter("@Fuente", "" + mj.Fuente + ""));
        context.Parametros.Add(new SqlParameter("@Tamaño", "" + mj.Tamaño + ""));
        context.Parametros.Add(new SqlParameter("@Color", "" + Color + ""));
        context.Parametros.Add(new SqlParameter("@Fecha_Inicio", "" + fech_Inicio + ""));
        context.Parametros.Add(new SqlParameter("@Fecha_Vencimiento", "" + fech_Venci + ""));
        context.Parametros.Add(new SqlParameter("@Id_Fondo", "" + id_Fondo + ""));
        context.Parametros.Add(new SqlParameter("@Numero_Empresa", "" + idEmpresa + ""));

        DataTable dt = context.ExecuteProcedure("sp_Mensaje_Agregar", true);

        return 0;
    }

    public DataTable sqlmostrarDatos()
    {
        SQLConection context = new SQLConection();
        try
        {
            DataTable dt = new DataTable();
            //dt = context.ExecuteQuery(sQuery);
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", "" + IdMensaje + ""));
            dt = context.ExecuteProcedure("sp_Mensaje_Consulta", true);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable sqlSelectTodoMensaje(int empresa)
    {
        SQLConection context = new SQLConection();
        try
        {
            context.Parametros.Add(new SqlParameter("@Numero_empresa", empresa));
            DataTable dt = new DataTable();
            dt = context.ExecuteProcedure("sp_Mensaje_Mostrar_Todos", true);

            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int sqlDelete(ClnuevoMensaje mj)
    {
        SQLConection context = new SQLConection();
        context.Parametros.Clear();
        context.Parametros.Add(new SqlParameter("Id_Mensaje", "" + mj.IdMensaje + ""));
        DataTable dt = context.ExecuteProcedure("sp_Mensaje_Eliminar", true);


        return 0;
    }

    public int sqlUpdate(ClnuevoMensaje mj)
    {
        SQLConection context = new SQLConection();
        context.Parametros.Clear();
        context.Parametros.Add(new SqlParameter("@Id_Mensaje", "" + mj.idMensaje + ""));
        context.Parametros.Add(new SqlParameter("@Mensaje_Texto", "" + mj.Mensaje + ""));
        context.Parametros.Add(new SqlParameter("@Fuente", "" + mj.Fuente + ""));
        context.Parametros.Add(new SqlParameter("@Tamaño", "" + mj.Tamaño + ""));
        context.Parametros.Add(new SqlParameter("@Color", "" + Color + ""));
        context.Parametros.Add(new SqlParameter("@Fecha_Inicio", "" + fech_Inicio + ""));
        context.Parametros.Add(new SqlParameter("@Fecha_Vencimiento", "" + fech_Venci + ""));
        context.Parametros.Add(new SqlParameter("@Id_Fondo", "" + id_Fondo + ""));

        DataTable dt = context.ExecuteProcedure("sp_Mensaje_Modificar", true);


        return 0;
    }

    public int sqlBuscarTiempo(ClnuevoMensaje mj)
    {
        int res;
        SQLConection context = new SQLConection();

        context.Parametros.Add(new SqlParameter("@Numero", "" + mj.idMensaje + ""));

        DataTable dt = new DataTable();
        dt = context.ExecuteProcedure("sp_Mensajes_Buscar_Tiempo", true);
        res = Convert.ToInt32(dt.Rows[0][0]);
        return res;
    }


    public void sqlModificarTiempo(ClnuevoMensaje mj)
    {
        try
        {
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@Tiempo_Mensajes", "" + mj.tiempo + ""));
            context.Parametros.Add(new SqlParameter("@Numero", "" + mj.idEmpresa + ""));

            DataTable dt = new DataTable();
            dt = context.ExecuteProcedure("sp_Mensajes_Modificar_Tiempo", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}