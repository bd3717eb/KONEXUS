using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegraData;
using System.Data.SqlClient;
using IntegraData;


namespace IntegraBussines
{
    public class ControlProveedorAcreedor
    {


        public DataTable ObtieneDatosControles(int numEmpresa, int numFamilia, int numPadre)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Add(new SqlParameter("@companynumber", numEmpresa));
            context.Parametros.Add(new SqlParameter("@family", numFamilia));
            context.Parametros.Add(new SqlParameter("@father", numPadre));

            dt = context.ExecuteProcedure("sp_Controles_ObtieneEstatusProveedor_v1", true).Copy();
            return dt;
        }

        public DataTable ObtieneDatosProveedorAcreedor(int numEmpresa, string TipoPersona, int numPersona, string NomProvedor, int TipoProveedor, int Estatus,
                                                        string ApPaterno, string ApMaterno, string nomCorto, int TipoPersonalidad, string numTelefono, string email,
                                                        string RFC, int TipoProvLocGen, int TipoRolPersona, string Domicilio, string Colonia, string CpPostal,
                                                        string TelDireccion, int Tipodomicilio)
        {
            SQLConection context = new SQLConection();
            DataTable dtDatProveedor = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Tipo_Persona", TipoPersona));
            context.Parametros.Add(new SqlParameter("@Numero_Persona", numPersona));
            context.Parametros.Add(new SqlParameter("@Nombre_Provedor", NomProvedor));
            context.Parametros.Add(new SqlParameter("@Tipo_Proveedor", TipoProveedor));
            context.Parametros.Add(new SqlParameter("@Estatus", Estatus));
            context.Parametros.Add(new SqlParameter("@Ap_Paterno", ApPaterno));
            context.Parametros.Add(new SqlParameter("@Ap_Materno", ApMaterno));
            context.Parametros.Add(new SqlParameter("@Nombre_Corto", nomCorto));
            context.Parametros.Add(new SqlParameter("@Tipo_Personalidad", TipoPersonalidad));
            context.Parametros.Add(new SqlParameter("@Num_Telefono", numTelefono));
            context.Parametros.Add(new SqlParameter("@Email", email));
            context.Parametros.Add(new SqlParameter("@RFC", RFC));
            context.Parametros.Add(new SqlParameter("@TipoProvLocGen", TipoProvLocGen));
            context.Parametros.Add(new SqlParameter("@TipoRolPersona", TipoRolPersona));
            context.Parametros.Add(new SqlParameter("@Domicilio", Domicilio));
            context.Parametros.Add(new SqlParameter("@Colonia", Colonia));
            context.Parametros.Add(new SqlParameter("@Cp_Postal", CpPostal));
            context.Parametros.Add(new SqlParameter("@Telefono_Direc", TelDireccion));
            context.Parametros.Add(new SqlParameter("@Tipo_Domicilio", Tipodomicilio));


            dtDatProveedor = context.ExecuteProcedure("sp_BuscaDatosProvedor", true).Copy();
            return dtDatProveedor;
        }


        public DataTable ObtenDatosProvedor(int tipo, int NumEmpresa, string Nombre, string TipoPersona, int TipoProvedor)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Add(new SqlParameter("@Tipo", tipo));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", NumEmpresa));
            context.Parametros.Add(new SqlParameter("@Nombre_Proveedor", Nombre));
            context.Parametros.Add(new SqlParameter("@Tipo_Persona", TipoPersona));
            context.Parametros.Add(new SqlParameter("@Tipo_Proveedor", TipoProvedor));


            dt = context.ExecuteProcedure("sp_Autocomplete_Proveedor_Acreedor", true).Copy();
            return dt;
        }

        public DataTable ObtieneDatosPersonalesProveedor(int numEmpresa, int Personalidad, int numProveedor,
                                                           string perJuridica, int TipoLocGen)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Personalidad", Personalidad));
            context.Parametros.Add(new SqlParameter("@Num_Proveedor", numProveedor));
            context.Parametros.Add(new SqlParameter("@Persona_Juridica", perJuridica));
            context.Parametros.Add(new SqlParameter("@Tipo_Loc_Gen", TipoLocGen));



            dt = context.ExecuteProcedure("sp_ObtieneDatosPersonalesProveedorAcreedor", true).Copy();
            return dt;
        }

        public DataTable ObtieneDatosDePagoproveedor(int numEmpresa, int numProveedor)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Proveedor", numProveedor));

            dt = context.ExecuteProcedure("sp_ObtieneFormasPagoProvedor", true).Copy();
            return dt;
        }


        public DataTable ObtenDomicilioProveedor(int numProveedor)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Persona", numProveedor));

            dt = context.ExecuteProcedure("sp_ObtieneDireccionProvedor", true).Copy();
            return dt;
        }

        public DataTable VereificaTipoProvedor(int numEmpresa, int nunPersona)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Persona", nunPersona));

            dt = context.ExecuteProcedure("sp_ObtieneTipoProveedor", true).Copy();
            return dt;
        }

        public DataTable VerificaTipoPersona(int tipo, int numEmpresa, int numPersona)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Tipo", tipo));
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Persona", numPersona));

            dt = context.ExecuteProcedure("sp_VerificaTipoPersona", true).Copy();
            return dt;
        }

        public DataTable verificaCuentaProveedor(int numEmpresa, int numCuenta)
        {
            SQLConection contex = new SQLConection();
            DataTable dt = new DataTable();

            contex.Parametros.Clear();
            contex.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            contex.Parametros.Add(new SqlParameter("@Numero_Cuenta", numCuenta));

            dt = contex.ExecuteProcedure("sp_VerificaCuentaProveedorCliente", true).Copy();
            return dt;
        }

        public DataTable VerificaExistenciaClienteOtrasEmpresas(int numEmpresa, string nomCliente, string perJuridica)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Nombre_Cliente", nomCliente));
            context.Parametros.Add(new SqlParameter("@Personalidad_Juridica", perJuridica));

            dt = context.ExecuteProcedure("sp_VerificaExisteciaCliente", true).Copy();
            return dt;
        }

        public bool GeneraModificacionCliente(int tipoMovimiento, int numEmpresa, int? numero, string nomCompleto, string apPaterno, string apMaterno, string nombre,
                                                      string nomCorto, string rfc, string curp, string telefono, string fax, string radiolocalizador, string celular,
                                                      string Email, string PersonalidadJuridica, int General, string sFechaAlta)
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@TipoMovimiento", tipoMovimiento));
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero", numero.HasValue ? (object)numero.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Nombre_Completo", nomCompleto));
                context.Parametros.Add(new SqlParameter("@Paterno", apPaterno));
                context.Parametros.Add(new SqlParameter("@Materno", apMaterno));
                context.Parametros.Add(new SqlParameter("@Nombre", nombre));
                context.Parametros.Add(new SqlParameter("@Nombre_Corto", nomCorto));
                context.Parametros.Add(new SqlParameter("@RFC", rfc));
                context.Parametros.Add(new SqlParameter("@CURP", curp));
                context.Parametros.Add(new SqlParameter("@Telefono", telefono));
                context.Parametros.Add(new SqlParameter("@Fax", fax));
                context.Parametros.Add(new SqlParameter("@Radiolocalizador", radiolocalizador));
                context.Parametros.Add(new SqlParameter("@Celular", celular));
                context.Parametros.Add(new SqlParameter("@Correo_Electronico", Email));
                context.Parametros.Add(new SqlParameter("@Personalidad_Juridica", PersonalidadJuridica));
                context.Parametros.Add(new SqlParameter("@General", General));
                context.Parametros.Add(new SqlParameter("@Numero_Confia", ""));
                context.Parametros.Add(new SqlParameter("@Transportista", ""));
                context.Parametros.Add(new SqlParameter("@Fecha_Alta", sFechaAlta));


                context.ExecuteProcedure("sp_Persona", true).Copy();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public DataTable VerificaexixtenciaProveedor(int numEmpresa, int numPersona)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Persona", numPersona));

            dt = context.ExecuteProcedure("sp_VerificaExixtenciaProveedor", true).Copy();
            return dt;
        }






        public bool GeneraModificacionProveedor(int tipoMovimiento, int numeroEmpresa, int numeroPersona, int ICProvedor, int Estatus, string OBS, string NombreCompleto,
                                                int Tipo, int formPago, int clasFormPago, int clasMoneda, int TipoNE)
        {
            try
            {
                SQLConection context = new SQLConection();
                DataTable dt = new DataTable();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@TipoMovimiento", tipoMovimiento));
                context.Parametros.Add(new SqlParameter("@iNumEmpresa", numeroEmpresa));
                context.Parametros.Add(new SqlParameter("@Numero_Persona", numeroPersona));
                context.Parametros.Add(new SqlParameter("@iCProveedor", ICProvedor));
                context.Parametros.Add(new SqlParameter("@iEstatus", Estatus));
                context.Parametros.Add(new SqlParameter("@Obs", OBS));
                context.Parametros.Add(new SqlParameter("@Nombre_Completo", NombreCompleto));
                context.Parametros.Add(new SqlParameter("@Tipo", Tipo));
                context.Parametros.Add(new SqlParameter("@Forma_Pago", formPago));
                context.Parametros.Add(new SqlParameter("@Clas_Forma_Pago", clasFormPago));
                context.Parametros.Add(new SqlParameter("@Clas_Moneda", clasMoneda));
                context.Parametros.Add(new SqlParameter("@Tipo_NE", TipoNE));


                context.ExecuteProcedure("sp_Proveedor", true).Copy();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable ValidaExistenciaDomicilioFiscal(int NumPersona)
        {
            SQLConection context = new SQLConection();
            DataTable dtDom = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Cliente", NumPersona));

            dtDom = context.ExecuteProcedure("sp_ObtieneDomicilioFiscal", true).Copy();
            return dtDom;
        }
        //modificar para traer el numero de domicilio
        public bool GeneraModificacionDomicilio(int tipoMovimiento, int? numDom, int numPersona, string Domicilio, string Colonia, string Delegacion,
                                                string Estado, string CodPostal, string Telefono, string Fax, int Fiscal, int ClasTipDom)
        {
            try
            {
                SQLConection context = new SQLConection();
                DataTable dt = new DataTable();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@TipoMovimiento", tipoMovimiento));
                context.Parametros.Add(new SqlParameter("@Numero", numDom.HasValue ? (object)numDom.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Numero_Persona", numPersona));
                context.Parametros.Add(new SqlParameter("@Domicilio", Domicilio));
                context.Parametros.Add(new SqlParameter("@Colonia", Colonia));
                context.Parametros.Add(new SqlParameter("@Delegacion_Municipio", Delegacion));
                context.Parametros.Add(new SqlParameter("@Estado", Estado));
                context.Parametros.Add(new SqlParameter("@Codigo_Postal", CodPostal));
                context.Parametros.Add(new SqlParameter("@Telefono", Telefono));
                context.Parametros.Add(new SqlParameter("@Fax", Fax));
                context.Parametros.Add(new SqlParameter("@Fiscal", Fiscal));
                context.Parametros.Add(new SqlParameter("@Clas_Tipo_Dom", ClasTipDom));

                context.ExecuteProcedure("sp_Domicilio", true).Copy();
                return true;
            }
            catch (Exception ex)
            {
              
                return false;
            }
        }


        public int AgregaNuevoDomicilio(int tipoMovimiento, int? numDom, int numPersona, string Domicilio, string Colonia, string Delegacion,
                                                string Estado, string CodPostal, string Telefono, string Fax, int Fiscal, int ClasTipDom)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                       
            new SqlParameter("TipoMovimiento", tipoMovimiento),
            new SqlParameter("Numero", numDom.HasValue ? (object)numDom.Value : DBNull.Value),
            new SqlParameter("Numero_Persona", numPersona),
            new SqlParameter("Domicilio", Domicilio),
            new SqlParameter("Colonia", Colonia),
            new SqlParameter("Delegacion_Municipio", Delegacion),
            new SqlParameter("Estado", Estado),
            new SqlParameter("Codigo_Postal", CodPostal),
            new SqlParameter("Telefono", Telefono),
            new SqlParameter("Fax", Fax),
            new SqlParameter("Fiscal", Fiscal),
            new SqlParameter("Clas_Tipo_Dom", ClasTipDom),
          
        };
            try
            {


                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        if (param.ParameterName == "Numero" && numDom == 0)
                        {
                            param.Direction = ParameterDirection.Output;
                        }
                    }
                }

                return sqlDBHelper.ExecuteNonQueryOutputValue("sp_Domicilio", "Numero", CommandType.StoredProcedure, parameters);


            }
            catch (Exception)
            {

                return -1;
            }

        }

        public DataTable ObtieneDatosGeneralesProveedor(int numEmpresa, int numPersona)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Persona", numPersona));

            dt = context.ExecuteProcedure("sp_ObtieneDatosGeneralesProveedor", true).Copy();
            return dt;
        }


        public DataTable ObtieneDatosBanco(int numEmpresa, int Concepto, int numPersona)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Concepto", Concepto));
            context.Parametros.Add(new SqlParameter("@Num_Persona", numPersona));

            dt = context.ExecuteProcedure("sp_Proveedor_Conceptos_Tipo", true).Copy();
            return dt;
        }

        public DataTable ObtieneDatosCuenta(int numEmpresa, string Cuenta, int concepto, int banco)
        {
            SQLConection context = new SQLConection();
            DataTable dtCuenta = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@sTemp", Cuenta));
            context.Parametros.Add(new SqlParameter("@Concepto", concepto));
            context.Parametros.Add(new SqlParameter("@Tipo_Gasto", banco));

            dtCuenta = context.ExecuteProcedure("sp_Proveedor_Conceptos_Tipo_Gasto", true).Copy();
            return dtCuenta;
        }

        public DataTable ObtieneDatosConcepto(int numEmpresa, int numProveedor, string decripccion)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
            context.Parametros.Add(new SqlParameter("@Numero_Proveedor", numProveedor));
            context.Parametros.Add(new SqlParameter("@Descripcion", decripccion));


            dt = context.ExecuteProcedure("sp_Proveedor_Conceptos", true).Copy();
            return dt;
        }

        public DataTable obtieneConceptoProveedor(int numCompani, int numPersona)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@Numero_Empresa", numCompani));
            context.Parametros.Add(new SqlParameter("@Numero_Proveedor", numPersona));

            dt = context.ExecuteProcedure("sp_Proveedor_Conceptos_CP_Trae", true).Copy();
            return dt;

        }

        public bool AgeregaModificaConcepto(int tipoMovimiento, int numEmpresa, int ID, int numProveedor, int numConcepto, int? Tipo, int? gasCosto)
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@TipoMovimiento", tipoMovimiento));
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
                context.Parametros.Add(new SqlParameter("@ID", ID));
                context.Parametros.Add(new SqlParameter("@Numero_Proveedor", numProveedor));
                context.Parametros.Add(new SqlParameter("@Numero_Concepto", numConcepto));
                context.Parametros.Add(new SqlParameter("@Tipo", Tipo.HasValue ? (object)Tipo.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Gasto_Costo", gasCosto.HasValue ? (object)gasCosto.Value : DBNull.Value));

                context.ExecuteProcedure("sp_Proveedor_Conceptos_CP", true).Copy();

                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }

        public bool EliminaConceptoProveedor(int tipoMovimiento, int numEmpresa, int ID, int? numProveedor, int? numConcepto, int? Tipo, int? gasCosto)
        {
            try
            {
                SQLConection context = new SQLConection();

                context.Parametros.Clear();
                context.Parametros.Add(new SqlParameter("@TipoMovimiento", tipoMovimiento));
                context.Parametros.Add(new SqlParameter("@Numero_Empresa", numEmpresa));
                context.Parametros.Add(new SqlParameter("@ID", ID));
                context.Parametros.Add(new SqlParameter("@Numero_Proveedor", numProveedor.HasValue ? (object)Tipo.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Numero_Concepto", numConcepto.HasValue ? (object)Tipo.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Tipo", Tipo.HasValue ? (object)Tipo.Value : DBNull.Value));
                context.Parametros.Add(new SqlParameter("@Gasto_Costo", gasCosto.HasValue ? (object)gasCosto.Value : DBNull.Value));

                context.ExecuteProcedure("sp_Proveedor_Conceptos_CP", true).Copy();

                return true;
            }
            catch (Exception ex)
            {
              
                return false;
            }

        }


        public DataTable ObtieneNumeroPersona(int numEmpresa)
        {
            SQLConection context = new SQLConection();
            DataTable dt = new DataTable();

            context.Parametros.Clear();
            context.Parametros.Add(new SqlParameter("@companynumber", numEmpresa));

            dt = context.ExecuteProcedure("sp_ObtieneUltimaPersona", true).Copy();
            return dt;
        }
    
    }
}
