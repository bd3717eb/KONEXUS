//-----------FUNCIONES PARA FACTURA LIBRE-------
function openpdf() {
    //debugger;
    var docpdf = '../Ventas/Documento.aspx';
    if (docpdf != "") {

        window.open(docpdf, "ventana1", "width=500,height=600,scrollbars=YES,toolbar=no,location=0, directories=0, status=0")
    } else {
        refresh(2);
    }
    __doPostBack('lnkNueva', '')
}
function CerrarPopupSalesOk() {
    var mpu = $find('CPHIntegraCompany_salemsgmdl');
    mpu.hide();
    setTimeout("openpdf()", 3000)

}
function CerrarCancelaPago() {
    var mpu = $find('CPHIntegraCompany_mdlPago');
    mpu.hide();

}

function CerrarPopupVenta() {
    var mpu = $find('CPHIntegraCompany_mdlPagoAviso');
    mpu.hide();
    setTimeout("openpdf()", 3000)
}

function CerrarPopup() {
    var mpu = $find('CPHIntegraCompany_mdlPagoAviso');
    mpu.hide();
    setTimeout("openpdf()", 3000)

}

//------------------FUNCIONES PERSONAS FISICAS--------------

function redirFis(parametro) {
    debugger;
    var tipocambio = document.getElementsByClassName('LabelStyleImport');

    try {
        var valorrestante = document.getElementById("txtRestan").value;

    } catch (e) {
        var valorrestante = document.getElementById("CPHIntegraCompany_txtRestan").value;
    }

    if (document.getElementById(parametro.id).value == "$0.00" || document.getElementById(parametro.id).value == 0 || document.getElementById(parametro.id).value == "$0" || document.getElementById(parametro.id).value == "$0.0") {
        if (tipocambio) {

            var cajas = document.getElementsByClassName('TextBoxStyleImport');

            for (var recorre = 0; recorre < cajas.length; recorre++) {
                cajas[recorre].disabled = true;
            }

            document.getElementById(parametro.id).disabled = false;
            var nombre = parametro.id;
            var porcion = nombre.substring(24);

            try {
                mn = tipocambio[porcion].textContent;
                if (mn != "110") {
                    document.getElementById(parametro.id).value = valorrestante;
                    document.getElementById("CPHIntegraCompany_txtRestan").value = "$0.00";
                }
                else {
                    document.getElementById(parametro.id).value = '';
                }
            } catch (e) {
                var porcion = nombre.substring(39);
                mn = tipocambio[porcion].textContent;
                if (mn != "110") {
                    //                    parametro.id = nombre;
                    document.getElementById(parametro.id).value = valorrestante;
                    document.getElementById("CPHIntegraCompany_txtRestan").value = "$0.00";
                }
                else {
                    document.getElementById(parametro.id).value = '';
                }
            }
        }
    }

    document.getElementById(parametro.id).select();

}


function redirbFis(parametro) {
    debugger;
    var valor = document.getElementById(parametro.id).value;
    if (valor == 0 || valor == undefined) {
        document.getElementById(parametro.id).value = '0';
        var cajas = document.getElementsByClassName('TextBoxStyleImport');

        for (var recorre = 0; recorre < cajas.length; recorre++) {
            cajas[recorre].disabled = false;
        }
    }
    else {
        document.getElementById(parametro.id).value = valor;
    }

}

function redircambioFis(parametro) {
    debugger;
    var tipocambio = document.getElementsByClassName('LabelStyleChange');

    try {
        var valorcambio = document.getElementById("txtCambiopago").value;
    } catch (e) {
        var valorcambio = document.getElementById("CPHIntegraCompany_txtCambiopago").value;
    }
    if (document.getElementById(parametro.id).value == "$0.00" || document.getElementById(parametro.id).value == 0 || document.getElementById(parametro.id).value == "$0" || document.getElementById(parametro.id).value == "$0.0") {
        if (tipocambio) {

            var cajas = document.getElementsByClassName('TextBoxStyleChange');

            for (var recorre = 0; recorre < cajas.length; recorre++) {
                cajas[recorre].disabled = true;
            }

            document.getElementById(parametro.id).disabled = false;
            var nombre = parametro.id;
            var porcion = nombre.substring(18);

            try {
                mn = tipocambio[porcion].textContent;
                if (mn != "110") {
                    document.getElementById(parametro.id).value = valorcambio;

                }
                else {
                    document.getElementById(parametro.id).value = '';
                }

            } catch (e) {
                var porcion = nombre.substring(39);
                mn = tipocambio[porcion].textContent;
                if (mn != "110") {
                    document.getElementById(parametro.id).value = valorcambio;

                }
                else {
                    document.getElementById(parametro.id).value = '';
                }
            }
        }
    }
    document.getElementById(parametro.id).select();
}

function redirbcambioFis(parametro) {
    debugger;
    var valor = document.getElementById(parametro.id).value;
    if (valor == 0 || valor == undefined) {
        document.getElementById(parametro.id).value = '0';
        var cajas = document.getElementsByClassName('TextBoxStyleChange');

        for (var recorre = 0; recorre < cajas.length; recorre++) {
            cajas[recorre].disabled = false;
        }
    }
    else {
        document.getElementById(parametro.id).value = valor;
    }

}

// ====================================================================================================
var bandera = false;
function gfConfirmaEmision() {
    //event.preventDefault();

    var Respuesta = confirm(" ¿ Desea " + "+ evento");
    if (Respuesta == true) {
        //vElementoSeleccionadoText.checked = true;
        //window.location.href("FacturacionMorales.aspx");
        return true;
    } else {
        // 

        return false;
    }
}
var vAnterior = 0;
var vDespues = 0;
function gfProcesoConfirma(event) {
    debugger;

    var vElementoSeleccionado = document.getElementsByName('rbTipoEmisionFactura');
    var vElementoSeleccionadoText;
    var vElementoTextoAMostrar = '';

    vElementoSeleccionadoText = lfProcesoConfirma(vElementoSeleccionado, 0);
    if (parseInt(vElementoSeleccionadoText[0].value) == 0 && parseInt(vElementoSeleccionadoText[0].value) != vDespues) {
        vElementoTextoAMostrar = ' Emitir factura sin  retenciones (personas morales)?';
    } else {
        vElementoTextoAMostrar = ' Emitir factura con retenciones (personas físicas)?'
    }

    var Respuesta = confirm(" ¿ Desea " + vElementoTextoAMostrar);
    if (Respuesta == true) {
        //vElementoSeleccionadoText.checked = true;
        window.location.href("FacturaLibreMorales.aspx");
        return true;
    } else {
        event.preventDefault();
        vElementoSeleccionadoText[vDespues].checked = true;
        return false;
    }
}

function lfProcesoConfirma(vElementoSeleccionado, opcion) {
    var vElementoSeleccionado;

    if (opcion == 0) {
        for (var i = 0; i < vElementoSeleccionado.length; i++) {

            if (vElementoSeleccionado[i].checked == false) {
                vElementoSeleccionado(vElementoSeleccionado[i].value);
                vAnterior = vElementoSeleccionado[i].value;
            } else {
                vDespues = vElementoSeleccionado[i].value;

            }
        }
    }
    else {
        for (var i = 0; i < vElementoSeleccionado.length; i++) {

            if (vElementoSeleccionado[i].checked) {
                vElementoSeleccionado(vElementoSeleccionado[i].value);
                vAnterior = vElementoSeleccionado[i].value;
            } else {
                vDespues = vElementoSeleccionado[i].value;

            }
        }
    }

    return vElementoSeleccionado;
}
// ====================================================================================================
//-------FUNCIONES FACTURA MORALES-----------------

function redirFL(parametro) {

    var valorrestante = document.getElementById("CPHIntegraCompany_txtRestan").value;
    var tipocambio = document.getElementsByClassName('LabelStyleImport');

    if (document.getElementById(parametro.id).value == "$0.00" || document.getElementById(parametro.id).value == 0 || document.getElementById(parametro.id).value == "$0" || document.getElementById(parametro.id).value == "$0.0") {
        if (tipocambio) {

            var cajas = document.getElementsByClassName('TextBoxStyleImport');

            for (var recorre = 0; recorre < cajas.length; recorre++) {
                cajas[recorre].disabled = true;
            }

            document.getElementById(parametro.id).disabled = false;
            var nombre = parametro.id;
            var porcion = nombre.substring(39);

            mn = tipocambio[porcion].textContent;
            if (mn == "1.0000") {
                document.getElementById(parametro.id).value = valorrestante;
                document.getElementById("CPHIntegraCompany_txtRestan").value = "$0.00";

            }
            else {
                document.getElementById(parametro.id).value = '';
            }
        }
    }
    document.getElementById(parametro.id).select();
}

function redirbFL(parametro) {

    var valor = document.getElementById(parametro.id).value;
    if (valor == 0 || valor == undefined) {
        document.getElementById(parametro.id).value = '0';
        var cajas = document.getElementsByClassName('TextBoxStyleImport');

        for (var recorre = 0; recorre < cajas.length; recorre++) {
            cajas[recorre].disabled = false;
        }
    }
    else {
        document.getElementById(parametro.id).value = valor;
    }

}



function redircambioFL(parametro) {


    var valorcambio = document.getElementById("CPHIntegraCompany_txtCambiopago").value;
    var tipocambio = document.getElementsByClassName('LabelStyleChange');

    if (document.getElementById(parametro.id).value == "$0.00" || document.getElementById(parametro.id).value == 0 || document.getElementById(parametro.id).value == "$0" || document.getElementById(parametro.id).value == "$0.0") {
        if (tipocambio) {

            var cajas = document.getElementsByClassName('TextBoxStyleChange');

            for (var recorre = 0; recorre < cajas.length; recorre++) {
                cajas[recorre].disabled = true;
            }

            document.getElementById(parametro.id).disabled = false;
            var nombre = parametro.id;
            var porcion = nombre.substring(39);

            mn = tipocambio[porcion].textContent;
            if (mn == "1.0000") {
                document.getElementById(parametro.id).value = valorcambio;

            }
            else {
                document.getElementById(parametro.id).value = '';
            }
        }
    }
    document.getElementById(parametro.id).select();
}

function redirbcambioFL(parametro) {

    var valor = document.getElementById(parametro.id).value;
    if (valor == 0 || valor == undefined) {
        document.getElementById(parametro.id).value = '0';
        var cajas = document.getElementsByClassName('TextBoxStyleChange');

        for (var recorre = 0; recorre < cajas.length; recorre++) {
            cajas[recorre].disabled = false;
        }
    }
    else {
        document.getElementById(parametro.id).value = valor;
    }

}


