using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace IntegraData
{
     public class CfdiDAL
     {

        
         
        public DataTable GetDatosEmisor(int icompany)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
			   new SqlParameter("companynumber", icompany),
                     
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDatosDelEmisor_v2", CommandType.StoredProcedure, parameters);
        }
                  
        public DataTable GetDatosReceptor(int inumeroregistro, int icompany, int iTipoDoc)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
			   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", inumeroregistro),
               new SqlParameter("DocType", iTipoDoc),
                     
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDatosDelReceptor_v1", CommandType.StoredProcedure, parameters);
        }
         
        

        public DataTable GetDatosArchivo(int iNumeroRegistro, int iTipo, int icompany)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
			   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),
               new SqlParameter("type", iTipo),
                     
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDatosDeArchivo_v1", CommandType.StoredProcedure, parameters);
        }

        

        public DataTable GetDatosEncabezado(int iNumeroRegistro, int iTipo, int icompany, int icontabilizada, int documenttype, int internetdocument)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
			   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),
               new SqlParameter("type", iTipo),
               new SqlParameter("contabilizada", icontabilizada),
               new SqlParameter("documentType", documenttype),
               new SqlParameter("internetDocument", internetdocument),
                     
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDatosDeEncabezado_v1", CommandType.StoredProcedure, parameters);
        }

    

        public DataTable GetPaisEmisor(string spais_emisor, int icompany, int ifamily)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    		new SqlParameter("companynumber", icompany),
                new SqlParameter("pais", spais_emisor),
                new SqlParameter("family", ifamily),
                     
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDatosDePaisEmisor_v1", CommandType.StoredProcedure, parameters);
        }

        

        public DataTable GetDatosSucursal(int isucursal, int icompany)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    		new SqlParameter("companynumber", icompany),
                new SqlParameter("sucursal", isucursal),
                     
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDatosSucursal_v1", CommandType.StoredProcedure, parameters);
        }


        
        public DataTable GetDatosSucursalCFD(int inumero, int icompany, int iTipoDoc)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    		new SqlParameter("companynumber", icompany),
                new SqlParameter("numeroregistro", inumero),
                new SqlParameter("type", iTipoDoc),
                     
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDatosDeSucursalCFD_v1", CommandType.StoredProcedure, parameters);
        }

        
        public DataTable GetPartidasTDS(int iNumeroRegistro, int iType, int icompany, int icontabilizada, int idocumenttype, int idocumentinternet)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),
               new SqlParameter("type", iType),
               new SqlParameter("contabilizada", icontabilizada),
               new SqlParameter("documentType", idocumenttype),
               new SqlParameter("documetInternet", idocumentinternet),
                             
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtienePartidasCFD_v1", CommandType.StoredProcedure, parameters);
        }

        

        public DataTable GetPartidasCpae(int iNumeroRegistro, int iType, int icompany, int icontabilizada, int idocumenttype, int idocumentinternet)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),
               new SqlParameter("type", iType),
               new SqlParameter("contabilizada", icontabilizada),
               new SqlParameter("documentType", idocumenttype),
               new SqlParameter("documetInternet", idocumentinternet),
                             
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtienePartidasCpae_v1", CommandType.StoredProcedure, parameters);
        }

        

        public DataTable GetPartidasCarton(int iNumeroRegistro, int iType, int icompany, int icontabilizada, int idocumenttype, int idocumentinternet)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),
               new SqlParameter("type", iType),
               new SqlParameter("contabilizada", icontabilizada),
               new SqlParameter("documentType", idocumenttype),
               new SqlParameter("documetInternet", idocumentinternet),
                             
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtienePartidasCARTON_v1", CommandType.StoredProcedure, parameters);
        }


        

        public DataTable GetPartidasCirrus(int iNumeroRegistro, int iType, int icompany, int icontabilizada, int idocumenttype, int idocumentinternet)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),
               new SqlParameter("type", iType),
               new SqlParameter("contabilizada", icontabilizada),
               new SqlParameter("documentType", idocumenttype),
               new SqlParameter("documetInternet", idocumentinternet),
                             
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtienePartidasCIRRUS_v1", CommandType.StoredProcedure, parameters);
        }


        
 
        public DataTable GetPartidasGeneral(int iNumeroRegistro, int iType, int icompany, int icontabilizada, int idocumenttype, int idocumentinternet)
        {     

            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),
               new SqlParameter("type", iType),
               new SqlParameter("contabilizada", icontabilizada),
               new SqlParameter("documentType", idocumenttype),
               new SqlParameter("documetInternet", idocumentinternet),
                             
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtienePartidasGeneral_v1", CommandType.StoredProcedure, parameters);
        }



        public DataTable GetAdenda(int iNumeroRegistro, int icompany)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),
                                       
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneAdenda_v1", CommandType.StoredProcedure, parameters);
        }



        public DataTable GetNumberQuote(int iNumeroRegistro, int icompany)
        {

            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),
                                       
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneNumeroCuota_v1", CommandType.StoredProcedure, parameters);
        }
                         
        

        public DataTable GetReference(int iNumeroRegistro, int icompany)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),
                                       
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneReferencia_v1", CommandType.StoredProcedure, parameters);
        }
         
        

        public DataTable GetCotizacionPartidaAdenda(int iNumeroRegistro, int icompany, int ipartida)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),
               new SqlParameter("partida", ipartida),
                                       
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneCotizacionPartidaAdenda_v1", CommandType.StoredProcedure, parameters);
        }
         

        

        public DataTable GetCompanyShortName(int icompany)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
                                              
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneNombreCortoDeCompania_v1", CommandType.StoredProcedure, parameters);
        }

                    
        public DataTable GetCustomReport(int icompany)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
                                              
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneNombreDelReporteCustomizado_v1", CommandType.StoredProcedure, parameters);
        }


        public DataTable GetDataSerieInvoice(int iNumeroRegistro, int icompany, int iTipoDoc)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),    
               new SqlParameter("docType", iTipoDoc),                                
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneNumeroDeSerieDeFactura_v1", CommandType.StoredProcedure, parameters);
        }
   
         
        public DataTable GetConfigurationFiles(int iNumeroRegistro, int icompany, int iTipoDoc)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),    
               new SqlParameter("docType", iTipoDoc),                                
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneArchivosDeConfiguracion_v1", CommandType.StoredProcedure, parameters);
        }

        

        public DataTable GetDatosDocto(int iNumeroRegistro, int icompany, int iTipoDoc)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),    
               new SqlParameter("type", iTipoDoc),                                
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDatosDeDocumento_v1", CommandType.StoredProcedure, parameters);
        }

           public DataTable GetDataInvoice(int iNumeroRegistro, int icompany, int iTipoDoc)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),    
               new SqlParameter("type", iTipoDoc),                                
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDatosDeFactura_v1", CommandType.StoredProcedure, parameters);
        }

   

        public DataTable GetDateInvoice(int iNumeroRegistro, int icompany, int iTipoDoc)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),    
               new SqlParameter("type", iTipoDoc),                                
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneFechaDeFactura_v1", CommandType.StoredProcedure, parameters);
        }



        public DataTable GetDataAhorro(int iNumeroRegistro, int icompany)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("companynumber", icompany),
               new SqlParameter("numeroregistro", iNumeroRegistro),    
                                
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDatosDeAhorro_v1", CommandType.StoredProcedure, parameters);
        }


        public DataTable GetNamePerson(int ielaboro)// pendiente para mi 
        {
            string sQry = "SELECT Nombre_Completo " +
                          "FROM Persona " +
                            "WHERE Numero = @elaboro ";
                                        
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("elaboro", ielaboro),
          
            };

            return sqlDBHelper.ExecuteParamerizedSelectCommand(sQry, parameters);
        }

       

        public DataTable GetNameCountry(string spais, int icompany, int ifamily)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("pais", spais),
               new SqlParameter("company", icompany), 
               new SqlParameter("family", ifamily),
                                
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneNombreDelPais_v1", CommandType.StoredProcedure, parameters);
        }



        public DataTable GetAduanasPedimentos(int iNumeroRegistro, int icompany)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("numeroregistro", iNumeroRegistro),    
               new SqlParameter("companynumber", icompany),
                                             
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneAduanasPedimentos_v1", CommandType.StoredProcedure, parameters);
        }
         

        


        public DataTable GetObservaciones(int iNumeroRegistro, int icompany)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("numeroregistro", iNumeroRegistro),    
               new SqlParameter("companynumber", icompany),
                                             
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneObservaciones_v1", CommandType.StoredProcedure, parameters);
        }

        

        public DataTable GetRelaciones(int iNumeroRegistro)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("numeroregistro", iNumeroRegistro),    
                                                        
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneRelaciones_v1", CommandType.StoredProcedure, parameters);
        }

        
         
        public DataTable GetPartidaTipo(int iNumeroRegistro, int icompany, int iTipoDoc)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("numeroregistro", iNumeroRegistro), 
               new SqlParameter("companynumber", icompany),
               new SqlParameter("type", iTipoDoc),     
                                                        
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtienePartidaTipo_v1", CommandType.StoredProcedure, parameters);
        }



        public DataTable GetDetalleFactura(int iNumeroRegistro, int icompany, string spantalla)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("numeroregistro", iNumeroRegistro), 
               new SqlParameter("companynumber", icompany),
               new SqlParameter("spantalla", spantalla),
                                                        
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDetalleDeFactura_v1", CommandType.StoredProcedure, parameters);
        }


   

        public DataTable GetDetalleNotaCredito(int iNumeroRegistro, int icompany, int iTipoDatos)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("numeroregistro", iNumeroRegistro), 
               new SqlParameter("companynumber", icompany),
               new SqlParameter("type", iTipoDatos),      
                                                        
		    };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDetalleDeNotaDeCredito_v1", CommandType.StoredProcedure, parameters);
        }

        //public DataTable GetDetalleNotaCreditoIntegra(int iNumeroRegistro, int icompany, int iTipoDatos)
        //{

        //    string sQry;

        //    if (iTipoDatos == 0)


        //        sQry = "SELECT ISNULL(NCD.Cantidad, CD.Cantidad) AS Cantidad, " +
        //            "C_Concepto.Clave, C_Concepto.Descripcion, " +
        //            "(NCD.Importe/ ISNULL(NCD.Cantidad, CD.Cantidad)) AS Precio_U, " +
        //            "NCD.Importe AS Total_Partida, '1' AS UM " +
        //        "FROM Nota_Credito_Detalle AS NCD " +
        //            "INNER JOIN Cotizacion_Detalle AS CD " +
        //                "ON NCD.Numero_Empresa = CD.Numero_Empresa " +
        //                "AND NCD.Numero_Cotiza = CD.Numero_Cotiza " +
        //                "AND NCD.Numero_Cotiza_detalle = CD.Numero " +
        //            "INNER JOIN Nota_Credito AS NC " +
        //                "ON NCD.Numero_Empresa = NC.Numero_Empresa " +
        //                "AND NCD.Numero_Registro_Nota = NC.Numero " +
        //            "LEFT OUTER JOIN Clasificacion AS C_Concepto " +
        //                "ON CD.Numero_Empresa = C_Concepto.Empresa " +
        //                "AND CD.Familia_Producto_Tipo_Concepto_Concepto = C_Concepto.Familia " +
        //                "AND CD.concepto = C_Concepto.numero " +
        //        "WHERE NC.Numero_Empresa = @companynumber " +
        //            "AND NC.Numero = @numeroregistro ";


        //    else

        //        sQry = "SELECT '1' AS Cantidad, '1' AS Clave, C_Concepto.Descripcion, " +
        //                "NCD.Importe AS Precio_U, NCD.Importe AS Total_Partida, " +
        //                "'N.C.' AS UM " +
        //            "FROM Nota_Credito_Detalle AS NCD " +
        //                "INNER JOIN Nota_Credito AS NC " +
        //                    "ON NCD.Numero_Empresa = NC.Numero_Empresa " +
        //                    "AND NCD.Numero_Registro_Nota = NC.Numero " +
        //                "INNER JOIN Clasificacion AS C_Concepto " +
        //                    "ON NC.Clas_Concepto = C_Concepto.Numero " +
        //                    "AND NC.Numero_Empresa = C_Concepto.Empresa " +
        //                    "AND NC.Familia_Concepto = C_Concepto.Familia " +
        //            "WHERE NC.Numero_Empresa = @companynumber " +
        //                "AND NC.Numero = @numeroregistro ";




        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("numeroregistro", iNumeroRegistro),
        //        new SqlParameter("companynumber", icompany),
                             
        //    };

        //    return sqlDBHelper.ExecuteParamerizedSelectCommand(sQry, parameters);
        //}


        public DataTable GetDetalleNotaCreditoIntegra(int iNumeroRegistro, int icompany, int iTipoDatos)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("numeroregistro", iNumeroRegistro), 
               new SqlParameter("companynumber", icompany),
               new SqlParameter("type", iTipoDatos),      
            };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDetalleDeNotaDeCreditoIntegra_v1", CommandType.StoredProcedure, parameters);
        }

  

        public DataTable GetDetalleNotaCreditoDev(int iNumeroRegistro, int icompany)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("numeroregistro", iNumeroRegistro), 
               new SqlParameter("companynumber", icompany),
               
            };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDetalleDeNotaDeCreditoDev_v1", CommandType.StoredProcedure, parameters);
        }
     


        public DataTable GetFolioCampo(int iNumeroRegistro, int icompany, int inivel)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("numeroregistro", iNumeroRegistro),
               new SqlParameter("companynumber", icompany),
               new SqlParameter("nivel", inivel),
               
            };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneFolioCampo_v1", CommandType.StoredProcedure, parameters);
        }

   


        public DataTable GetCondicionesPago(int iNumeroRegistro, int icompany)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	   new SqlParameter("numeroregistro", iNumeroRegistro), 
               new SqlParameter("companynumber", icompany),
               
            };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneCondicionesDePago_v1", CommandType.StoredProcedure, parameters);
        }

  

        public DataTable GetCondicionPagoCredito(int icompany, int icliente, int imoneda)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	    	  new SqlParameter("cliente", icliente),
              new SqlParameter("moneda", imoneda),
              new SqlParameter("companynumber",icompany),
               
            };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneCondicionesDePagoDeCredito_v1", CommandType.StoredProcedure, parameters);
        }

        public DataTable GetDatosPago(int iNumeroRegistro, int icompany)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	        	 new SqlParameter("numeroregistro", iNumeroRegistro),
                 new SqlParameter("companynumber", icompany),
               
            };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDatosDePago_v1", CommandType.StoredProcedure, parameters);
        }



        public DataTable GetDatosMoneda(int icompany, int iNumeroRegistro)
        {
            SqlParameter[] parameters = new SqlParameter[]
		    {                
	        	 new SqlParameter("numeroregistro", iNumeroRegistro),
                 new SqlParameter("companynumber", icompany),
               
            };

            return sqlDBHelper.ExecuteNonQuerySELECTStatement("sp_CFDI_ObtieneDatosDeMoneda_v1", CommandType.StoredProcedure, parameters);
        }

     }
}
