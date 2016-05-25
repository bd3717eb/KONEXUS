using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using IntegraData;

namespace Integra_Develoment
{
    public class PersonaController
    {
        static private string snombre_completo;
        private string spaterno;
        private string smaterno;
        private string snombre;
        private string snombre_corto;
        private string stelefono;
        private string sfax;
        private string scorreo_electronico;
        private string spersonalidad_juridica;
        private string srfc;
        private string scurp;


        public string sNombre_Completo { get { return snombre_completo; } set { snombre_completo = value; } }
        public string sPaterno { get { return spaterno; } set { spaterno = value; } }
        public string sMaterno { get { return smaterno; } set { smaterno = value; } }
        public string sNombre { get { return snombre; } set { snombre = value; } }
        public string sNombre_Corto { get { return snombre_corto; } set { snombre_corto = value; } }
        public string sTelefono { get { return stelefono; } set { stelefono = value; } }
        public string sFax { get { return sfax; } set { sfax = value; } }
        public string sCorreo_Electronico { get { return scorreo_electronico; } set { scorreo_electronico = value; } }
        public string sPersonalidad_Juridica { get { return spersonalidad_juridica; } set { spersonalidad_juridica = value; } }
        public string sRFC { get { return srfc; } set { srfc = value; } }
        public string sCURP { get { return scurp; } set { scurp = value; } }


        //En ete metodo se ejecuta el procedimiento "sp_Persona", en el cual se puede agregar, modificar y eliminar a una persona de la base de datos.
        public bool mantenerPersona(int tipo, int empresa, string Nombre_Completo, string Paterno, string Materno, string Nombre, string Nombre_Corto, string RFC,
                                    string CURP, string Telefono, string Celular, string Correo_Electronico, string PersonalidadJuridica, int general, DateTime Fecha)
        {
            int result = 0;

            SQLConection context = new SQLConection();
            try
            {
                Nombre_Completo = Nombre + " " + Paterno + " " + Materno;
                context.Parametros.Add(new SqlParameter("@TipoMovimiento ", tipo));
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", empresa));
                context.Parametros.Add(new SqlParameter("@Numero", 0));
                context.Parametros.Add(new SqlParameter("@Nombre_Completo", Nombre_Completo));
                context.Parametros.Add(new SqlParameter("@Paterno", Paterno));
                context.Parametros.Add(new SqlParameter("@Materno", Materno));
                context.Parametros.Add(new SqlParameter("@Nombre", Nombre));
                context.Parametros.Add(new SqlParameter("@Nombre_Corto", Nombre_Corto));
                context.Parametros.Add(new SqlParameter("@RFC", RFC));
                context.Parametros.Add(new SqlParameter("@CURP", CURP));
                context.Parametros.Add(new SqlParameter("@Telefono", Telefono));
                context.Parametros.Add(new SqlParameter("@Fax", Telefono));
                context.Parametros.Add(new SqlParameter("@Radiolocalizador", Telefono));
                context.Parametros.Add(new SqlParameter("@Celular", Celular));
                context.Parametros.Add(new SqlParameter("@Correo_Electronico", Correo_Electronico));
                context.Parametros.Add(new SqlParameter("@Personalidad_Juridica", PersonalidadJuridica));
                context.Parametros.Add(new SqlParameter("@General", general));
                context.Parametros.Add(new SqlParameter("@Numero_Confia", DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Transportista", DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Fecha_Alta", Fecha));
                
                DataTable dt = new DataTable();
                dt = context.ExecuteProcedure("sp_Persona", true);
                result = Convert.ToInt32(dt.Rows[0][0]);

            }
            catch (Exception)
            {
                //logear la excepcion

                return false;
            }


            if (result < 0)
            {
                return false;
            }

            return true;


        }


        //En este metodo se ejecuta el procedimiento "sp_ClienteAlta" en el cual se da de alta y se modifica un cliente
        public bool mantenerCliente(int tipo, string Nombre_Completo, string Paterno, string Materno, string Nombre, string Nombre_Corto, string Telefono, string Correo_Electronico,
                                    string PersonalidadJuridica, int empresa, int Clasificacion_Cliente, string Fecha, int estatus, string RFC, string Giro, string CURP, int Lista_Precio,
                                    int Periodo_Pago, int Clas_Periodo_Pago, int general, int Numero_Persona, int Sucursal, int Forma_Pago, int Clas_Zona, int Lista_Pago,
                                    int Clas_Dato_Pago, string Dato_Pago)
        {
            int result = 0;
            
            SQLConection context = new SQLConection();

            try
            {
                if (tipo == 1 || tipo == 2)
                {

                    context.Parametros.Add(new SqlParameter("@TipoMovimiento", tipo));
                    context.Parametros.Add(new SqlParameter("@Nombre_Completo", Nombre_Completo));
                    context.Parametros.Add(new SqlParameter("@Paterno", Paterno));
                    context.Parametros.Add(new SqlParameter("@Materno", Materno));
                    context.Parametros.Add(new SqlParameter("@Nombre", Nombre));
                    context.Parametros.Add(new SqlParameter("@NombreCorto", Nombre_Corto));
                    context.Parametros.Add(new SqlParameter("@TelefonoCliente", Telefono));
                    context.Parametros.Add(new SqlParameter("@Fax", Telefono));
                    context.Parametros.Add(new SqlParameter("@Email", Correo_Electronico));
                    context.Parametros.Add(new SqlParameter("@Personalidad_Juridica", PersonalidadJuridica));
                    context.Parametros.Add(new SqlParameter("@Empresa", empresa));
                    context.Parametros.Add(new SqlParameter("@Clasificacion_Cliente", Clasificacion_Cliente));
                    context.Parametros.Add(new SqlParameter("@Fecha", Convert.ToDateTime(Fecha)));
                    context.Parametros.Add(new SqlParameter("@Estatus", estatus));
                    context.Parametros.Add(new SqlParameter("@RFC", RFC));
                    context.Parametros.Add(new SqlParameter("@sGiro", Giro));
                    context.Parametros.Add(new SqlParameter("@sCurp", CURP));
                    context.Parametros.Add(new SqlParameter("@Periodo_Pago", Periodo_Pago));
                    context.Parametros.Add(new SqlParameter("@Clas_Periodo_Pago", Clas_Periodo_Pago));
                    context.Parametros.Add(new SqlParameter("@General", general));
                    context.Parametros.Add(new SqlParameter("@Numero_Persona", Numero_Persona));
                    context.Parametros.Add(new SqlParameter("@Clas_Sucursal", Sucursal));
                    context.Parametros.Add(new SqlParameter("@Forma_Pago", Forma_Pago));
                    context.Parametros.Add(new SqlParameter("@Clas_Zona", Clas_Zona));
                    context.Parametros.Add(new SqlParameter("@Lista_Precio", Lista_Precio));
                    context.Parametros.Add(new SqlParameter("@Clas_Dato_Pago", Clas_Dato_Pago));
                    context.Parametros.Add(new SqlParameter("@Dato_Pago", DBNull.Value));

                    DataTable dt = new DataTable();
                    dt = context.ExecuteProcedure("dbo.sp_TipoMovimiento", true);
                    result = Convert.ToInt32(dt.Rows[0][0]);

                } 
                else if(tipo == 3)
                {
                    context.Parametros.Add(new SqlParameter("@Cliente", Numero_Persona));
                    context.Parametros.Add(new SqlParameter("@Empresa", empresa));

                    DataTable dt = new DataTable();
                    dt = context.ExecuteProcedure("dbo.SP_EliminaClientes", true);
                    result = Convert.ToInt32(dt.Rows[0][0]);
                }
                


            }            
            catch (Exception)
            {
                //logear la excepcion

                return false;
            }

            if (result < 0)
            {
                return false;
            }

            return true;
            
        }


        //En este metodo se ejecuta el procedimiento "sp_Domicilio" en el cual se puede agregar, modificar y eliminar un Domicilio
        public bool mantenerDomicilio(int tipo, int Numero_Domicilio, int Numero_Persona, string Domicilio, string Colonia, string Delegacion_Municipio, string Estado, string Codigo_Postal,
                                        string Telefono, string Fax, int Fiscal, int Clas_Tipo_Dom, int Clas_Pais)
        {
            int result = 0;

            SQLConection context = new SQLConection();

            try
            {
                context.Parametros.Add(new SqlParameter("@TipoMovimiento", tipo));
                context.Parametros.Add(new SqlParameter("@Numero", Numero_Domicilio));
                context.Parametros.Add(new SqlParameter("@Numero_Persona", Numero_Persona));
                context.Parametros.Add(new SqlParameter("@Domicilio", Domicilio));
                context.Parametros.Add(new SqlParameter("@Colonia", Colonia));
                context.Parametros.Add(new SqlParameter("@Delegacion_Municipio", Delegacion_Municipio));
                context.Parametros.Add(new SqlParameter("@Estado", Estado));
                context.Parametros.Add(new SqlParameter("@Codigo_Postal", Codigo_Postal));
                context.Parametros.Add(new SqlParameter("@Telefono", Telefono));
                context.Parametros.Add(new SqlParameter("@Fax", Fax));
                context.Parametros.Add(new SqlParameter("@Fiscal", Fiscal));
                context.Parametros.Add(new SqlParameter("@Clas_Tipo_Dom", Clas_Tipo_Dom));
                context.Parametros.Add(new SqlParameter("@Clas_Pais", Clas_Pais));

                DataTable dt = new DataTable();
                dt = context.ExecuteProcedure("dbo.sp_Domicilio", true);
                result = Convert.ToInt32(dt.Rows[0][0]);


            }
            catch (Exception )
            {
                //logear la excepcion

                return false;
            }

            if (result < 0)
            {
                return false;
            }

            return true;
        }


        //En este metodo se realiza la consulta de los datos de la person seleccionada (cliente)y los acomoda en una tabla
        public DataTable obtenerDatosPorNumeroPersona(int Empresa, int Numero_Persona)
        {
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@personnumber", Numero_Persona));
            context.Parametros.Add(new SqlParameter("@companynumber", Empresa));

            DataTable dt = new DataTable();
            dt = context.ExecuteProcedure("sp_Persona_ObtieneDatosDePersona_v1", true);
            DataTable DataPerson = dt;

            if (dt != null && dt.Rows.Count > 0)
            {

                sNombre_Completo = (DataPerson.Rows[0][2] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][2]);
                sPaterno = (DataPerson.Rows[0][3] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][3]);
                sMaterno = (DataPerson.Rows[0][4] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][4]);
                sNombre = (DataPerson.Rows[0][5] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][5]);
                sNombre_Corto = (DataPerson.Rows[0][6] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][6]);
                sRFC = (DataPerson.Rows[0][7] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][7]);
                sCURP = (DataPerson.Rows[0][8] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][8]);
                sTelefono = (DataPerson.Rows[0][9] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][9]);
                sFax = sTelefono;
                sCorreo_Electronico = (DataPerson.Rows[0][13] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][13]);
                sPersonalidad_Juridica = (DataPerson.Rows[0][14] == DBNull.Value) ? null : Convert.ToString(DataPerson.Rows[0][14]);

                return dt;
            }
            else
            {
                return new DataTable();
            }
        }


        //Se obtiene el domiciio de la persona
        public DataTable obtenerDomicilioFiscalDePersona (int Numero_Persona, int Tipo_Domicilio)
        {

            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@person", Numero_Persona));
            context.Parametros.Add(new SqlParameter("@typeAddress", Tipo_Domicilio));

            DataTable dt = new DataTable();
            dt = context.ExecuteProcedure("sp_Persona_ObtieneDomicilio", true);

            if (dt != null)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
              
        }


        //Se obtienen los datos del Cliente
        public DataTable obtenerDatosCliente(int Empresa, int Numero_Cliente)
        {
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@personnumber", Numero_Cliente));
            context.Parametros.Add(new SqlParameter("@companynumber", Empresa));

            DataTable dt = new DataTable();
            dt = context.ExecuteProcedure("sp_Cliente_ObtieneDatosDeCliente_v1", true);
            return dt;

        }

        public DataTable obtenerNombrePersona(int iEmpresa, int iNumero_Persona)
        {
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@Numero_Persona", iNumero_Persona));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", iEmpresa));

            DataTable dt = new DataTable();
            dt = context.ExecuteProcedure("sp_PersonaObtieneNombre", true);
            return dt;

        }


        //Se obtienen los contactod del Cliente
        public DataTable obtenerContactosCliente(int Empresa, int Numero_Cliente)
        {
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@customernumber", Numero_Cliente));
            context.Parametros.Add(new SqlParameter("@companynumber", Empresa));

            DataTable dt = new DataTable();
            dt = context.ExecuteProcedure("sp_Cliente_ObtieneContactos_BIS_v1", true);
            return dt;
        
        }


        //Se obtienen los creditos del Cliente
        public DataTable obtenerCreditoDeCliente(int Empresa, int Numero_Cliente, int Familia)
        {
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@companynumber", Empresa));
            context.Parametros.Add(new SqlParameter("@clientnumber", Numero_Cliente));
            context.Parametros.Add(new SqlParameter("@family", Familia));

            DataTable dt = new DataTable();
            context.ExecuteProcedure("sp_Cliente_ObtieneDatosDeCreditos_v1", true);

            return dt;

        }


        //Se obtiene los datos de pago del cliente para llenar el control DDL
        public virtual DataTable obtenerDatosDePagoDelCliente(int Empresa, int Padre, int Familia)
        {
            SQLConection context = new SQLConection();

            context.Parametros.Add(new SqlParameter("@companynumber", Empresa));
            context.Parametros.Add(new SqlParameter("@father", Padre));
            context.Parametros.Add(new SqlParameter("@familia", Familia));

            DataTable dt = new DataTable();
            dt = context.ExecuteProcedure("sp_Controles_ObtieneDatosDePagoDelCliente_v1", true);

            return dt;

        }
    }
}