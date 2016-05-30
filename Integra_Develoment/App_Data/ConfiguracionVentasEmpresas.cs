using System;
using System.Data;
using IntegraBussines;
namespace Integra_Develoment
{
    public class ConfiguracionVentasEmpresas
    {
        SaleBLL sale = new SaleBLL();
        private int inumeroempresa;
        private int isalidaautoalmacen;
        private string svalidacionventa;
        private int icostoreferencia;
        public int iNumeroEmpresa { get { return inumeroempresa; } set { inumeroempresa = value; } }
        public int iSalidaAutoAlmacen { get { return isalidaautoalmacen; } set { isalidaautoalmacen = value; } }
        public int iCostoReferencia { get { return icostoreferencia; } set { icostoreferencia = value; } }
        public string sValidacionVenta { get { return svalidacionventa; } set { svalidacionventa = value; } }

        public ConfiguracionVentasEmpresas(int inumero_empresa)
        {
            inumeroempresa = inumero_empresa;

            ConfiguracionVentaporEmpresa();

            svalidacionventa = ValidacionEmpresa();
        }
        private void ConfiguracionVentaporEmpresa()
        {

            DataTable dtValidaciones = new ConfiguracionVentaController().obtieneConfiguracionVenta(inumeroempresa);

            if (dtValidaciones == null)
            {

            }
            else
            {
                if (dtValidaciones.Rows.Count > 0)
                {
                    isalidaautoalmacen = Convert.ToInt32(dtValidaciones.Rows[0][1]);
                    icostoreferencia = Convert.ToInt32(dtValidaciones.Rows[0][2]);
                }
            }
        }
        private string ValidacionEmpresa()
        {
            DataTable dtValidacion = new ConfiguracionVentaController().obtieneValidacionDeEmpresa(inumeroempresa);

            string sValidacion = null;

            int validation_cero, validation_store, validation_type;

            if (dtValidacion == null)
                return null;
            else
            {

                if (Convert.ToString(dtValidacion.Rows[0][1]) != "")
                {
                    if (Convert.ToInt32(dtValidacion.Rows[0][1]) != 0)
                    {
                        if (Convert.ToString(dtValidacion.Rows[0][0]) != "" && Convert.ToString(dtValidacion.Rows[0][2]) != "")
                        {
                            validation_cero = Convert.ToInt32(dtValidacion.Rows[0][0]);
                            validation_store = Convert.ToInt32(dtValidacion.Rows[0][1]);
                            validation_type = Convert.ToInt32(dtValidacion.Rows[0][2]);


                            if (validation_store == 1)
                            {
                                if (validation_cero == 0)
                                {
                                    switch (validation_type)
                                    {
                                        case 0:
                                            sValidacion = "CambiarEstatusGeneral";
                                            break;
                                        case 1:
                                            sValidacion = "AgregarPartida";
                                            break;
                                        case 2:
                                            sValidacion = "CambiarEstatusPartida";
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (validation_type)
                                    {
                                        case 0:
                                            sValidacion = "CambiarEstatusGeneralSE";
                                            break;
                                        case 1:
                                            sValidacion = "AgregarPartidaSE";
                                            break;
                                        case 2:
                                            sValidacion = "CambiarEstatusPartidaSE";
                                            break;
                                    }
                                }
                            }
                            else
                                sValidacion = "NOTVAL";
                        }
                    }
                    else
                    {
                        sValidacion = "NOTVAL";
                    }
                }
            }

            return sValidacion;
        }

    }
}