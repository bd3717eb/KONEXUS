﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Integra_Develoment.FacturaServicio {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServicioSoap", Namespace="http://www.integrasoftware.com.mx/")]
    public partial class Servicio : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback gf_CreaPolizaContableOperationCompleted;
        
        private System.Threading.SendOrPostCallback gfs_Crea_FacturaXMLaPDFOperationCompleted;
        
        private System.Threading.SendOrPostCallback gfs_generaComprobanteFisicaOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Servicio() {
            this.Url = global::Integra_Develoment.Properties.Settings.Default.Integra_Develoment_FacturaServicio_Servicio;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event gf_CreaPolizaContableCompletedEventHandler gf_CreaPolizaContableCompleted;
        
        /// <remarks/>
        public event gfs_Crea_FacturaXMLaPDFCompletedEventHandler gfs_Crea_FacturaXMLaPDFCompleted;
        
        /// <remarks/>
        public event gfs_generaComprobanteFisicaCompletedEventHandler gfs_generaComprobanteFisicaCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.integrasoftware.com.mx/gf_CreaPolizaContable", RequestNamespace="http://www.integrasoftware.com.mx/", ResponseNamespace="http://www.integrasoftware.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string gf_CreaPolizaContable(int iMes, int iAnio, int iEmpresa, string sTipoSolicitud, string sBaseDatos, string sNumeroOrden, string sParametrosOpcional) {
            object[] results = this.Invoke("gf_CreaPolizaContable", new object[] {
                        iMes,
                        iAnio,
                        iEmpresa,
                        sTipoSolicitud,
                        sBaseDatos,
                        sNumeroOrden,
                        sParametrosOpcional});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void gf_CreaPolizaContableAsync(int iMes, int iAnio, int iEmpresa, string sTipoSolicitud, string sBaseDatos, string sNumeroOrden, string sParametrosOpcional) {
            this.gf_CreaPolizaContableAsync(iMes, iAnio, iEmpresa, sTipoSolicitud, sBaseDatos, sNumeroOrden, sParametrosOpcional, null);
        }
        
        /// <remarks/>
        public void gf_CreaPolizaContableAsync(int iMes, int iAnio, int iEmpresa, string sTipoSolicitud, string sBaseDatos, string sNumeroOrden, string sParametrosOpcional, object userState) {
            if ((this.gf_CreaPolizaContableOperationCompleted == null)) {
                this.gf_CreaPolizaContableOperationCompleted = new System.Threading.SendOrPostCallback(this.Ongf_CreaPolizaContableOperationCompleted);
            }
            this.InvokeAsync("gf_CreaPolizaContable", new object[] {
                        iMes,
                        iAnio,
                        iEmpresa,
                        sTipoSolicitud,
                        sBaseDatos,
                        sNumeroOrden,
                        sParametrosOpcional}, this.gf_CreaPolizaContableOperationCompleted, userState);
        }
        
        private void Ongf_CreaPolizaContableOperationCompleted(object arg) {
            if ((this.gf_CreaPolizaContableCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.gf_CreaPolizaContableCompleted(this, new gf_CreaPolizaContableCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.integrasoftware.com.mx/gfs_Crea_FacturaXMLaPDF", RequestNamespace="http://www.integrasoftware.com.mx/", ResponseNamespace="http://www.integrasoftware.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string gfs_Crea_FacturaXMLaPDF(string sBodyXML, string sNombreArchivoConExtension, string sBaseDatos, string sRutaAGuardar, string sImagen, int iTipoFacturaNotaCredito) {
            object[] results = this.Invoke("gfs_Crea_FacturaXMLaPDF", new object[] {
                        sBodyXML,
                        sNombreArchivoConExtension,
                        sBaseDatos,
                        sRutaAGuardar,
                        sImagen,
                        iTipoFacturaNotaCredito});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void gfs_Crea_FacturaXMLaPDFAsync(string sBodyXML, string sNombreArchivoConExtension, string sBaseDatos, string sRutaAGuardar, string sImagen, int iTipoFacturaNotaCredito) {
            this.gfs_Crea_FacturaXMLaPDFAsync(sBodyXML, sNombreArchivoConExtension, sBaseDatos, sRutaAGuardar, sImagen, iTipoFacturaNotaCredito, null);
        }
        
        /// <remarks/>
        public void gfs_Crea_FacturaXMLaPDFAsync(string sBodyXML, string sNombreArchivoConExtension, string sBaseDatos, string sRutaAGuardar, string sImagen, int iTipoFacturaNotaCredito, object userState) {
            if ((this.gfs_Crea_FacturaXMLaPDFOperationCompleted == null)) {
                this.gfs_Crea_FacturaXMLaPDFOperationCompleted = new System.Threading.SendOrPostCallback(this.Ongfs_Crea_FacturaXMLaPDFOperationCompleted);
            }
            this.InvokeAsync("gfs_Crea_FacturaXMLaPDF", new object[] {
                        sBodyXML,
                        sNombreArchivoConExtension,
                        sBaseDatos,
                        sRutaAGuardar,
                        sImagen,
                        iTipoFacturaNotaCredito}, this.gfs_Crea_FacturaXMLaPDFOperationCompleted, userState);
        }
        
        private void Ongfs_Crea_FacturaXMLaPDFOperationCompleted(object arg) {
            if ((this.gfs_Crea_FacturaXMLaPDFCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.gfs_Crea_FacturaXMLaPDFCompleted(this, new gfs_Crea_FacturaXMLaPDFCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.integrasoftware.com.mx/gfs_generaComprobanteFisica", RequestNamespace="http://www.integrasoftware.com.mx/", ResponseNamespace="http://www.integrasoftware.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string gfs_generaComprobanteFisica(int iNumeroFactura, int iTipoFacturaNotaCredito, string sDataBaseName, int iNumeroCompania, int iNumeroPersona, string sParametroOpcional) {
            object[] results = this.Invoke("gfs_generaComprobanteFisica", new object[] {
                        iNumeroFactura,
                        iTipoFacturaNotaCredito,
                        sDataBaseName,
                        iNumeroCompania,
                        iNumeroPersona,
                        sParametroOpcional});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void gfs_generaComprobanteFisicaAsync(int iNumeroFactura, int iTipoFacturaNotaCredito, string sDataBaseName, int iNumeroCompania, int iNumeroPersona, string sParametroOpcional) {
            this.gfs_generaComprobanteFisicaAsync(iNumeroFactura, iTipoFacturaNotaCredito, sDataBaseName, iNumeroCompania, iNumeroPersona, sParametroOpcional, null);
        }
        
        /// <remarks/>
        public void gfs_generaComprobanteFisicaAsync(int iNumeroFactura, int iTipoFacturaNotaCredito, string sDataBaseName, int iNumeroCompania, int iNumeroPersona, string sParametroOpcional, object userState) {
            if ((this.gfs_generaComprobanteFisicaOperationCompleted == null)) {
                this.gfs_generaComprobanteFisicaOperationCompleted = new System.Threading.SendOrPostCallback(this.Ongfs_generaComprobanteFisicaOperationCompleted);
            }
            this.InvokeAsync("gfs_generaComprobanteFisica", new object[] {
                        iNumeroFactura,
                        iTipoFacturaNotaCredito,
                        sDataBaseName,
                        iNumeroCompania,
                        iNumeroPersona,
                        sParametroOpcional}, this.gfs_generaComprobanteFisicaOperationCompleted, userState);
        }
        
        private void Ongfs_generaComprobanteFisicaOperationCompleted(object arg) {
            if ((this.gfs_generaComprobanteFisicaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.gfs_generaComprobanteFisicaCompleted(this, new gfs_generaComprobanteFisicaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void gf_CreaPolizaContableCompletedEventHandler(object sender, gf_CreaPolizaContableCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class gf_CreaPolizaContableCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal gf_CreaPolizaContableCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void gfs_Crea_FacturaXMLaPDFCompletedEventHandler(object sender, gfs_Crea_FacturaXMLaPDFCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class gfs_Crea_FacturaXMLaPDFCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal gfs_Crea_FacturaXMLaPDFCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void gfs_generaComprobanteFisicaCompletedEventHandler(object sender, gfs_generaComprobanteFisicaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class gfs_generaComprobanteFisicaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal gfs_generaComprobanteFisicaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591