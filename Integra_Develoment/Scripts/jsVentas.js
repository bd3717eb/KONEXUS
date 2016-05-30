


function getElement(id) {
    //debugger;
    var el = document.all ? document.all(id) : document.getElementById(id);
    return el;
};
function doClearProductsTextBox() {
    try {
        document.getElementById('CPHIntegraCompany_txtProductos').value = "";
    } catch (e) {

    }
}

$(document).ready(function () {
    //debugger;
    // posicionamos el scroll de la capa en la última posición guardada en la cookie
    // si no hubiese en la posición 0
    $("#scrollagregar").scrollTop($.cookies.get("scroll") || 0);
});
// ejecución del código antes de salir de la página
window.onbeforeunload = function () {
    // almacenamos en la cookie la posición del scroll de la capa de listado
    $.cookies.set('scroll', $("#scrollagregar").scrollTop());
}
function CerrarPopup() {
    //debugger;
    var mpu = $find('paidmdl');
    mpu.hide();
    var mpv = $find('colletmdl');
    mpv.hide();
    //    setTimeout("openpdf()", 3000)

};
function CerrarPopupDevo() {
    //debugger;
    var mpu = $find('CPHIntegraCompany_mdlpartidas');
    mpu.hide();

    //    setTimeout("openpdf()", 3000)

};

function AbrePago() {
    //debugger;
    var mpu = $find('paidmdl');
    mpu.show();


};
function CerrarPopupSalesOk() {
    //debugger;
    var mpu = $find('CPHIntegraCompany_salemsgmdl');
    mpu.hide();
    setTimeout("openpdf()", 4000);
}


//Cuando no realiza pago ok, no modificar
function CerrarPopupNoPay() {
    //debugger;
    var mpv = $find('confirmarmdl');
    mpv.hide();
    setTimeout("openpdf()", 3000);


};
function CerrarPopupFormaPago() {
    //debugger;
    var mpv = $find('CPHIntegraCompany_mdlFormaPago');
    mpv.hide();
    setTimeout("openpdf()", 3000);


};

function Prueba() {
    //debugger;
    alert('entra');
    setTimeout("openpdf()", 3000);


};
function gfRefresca() {

    __doPostBack('ctl00$CPHIntegraCompany$lnkRefresca', '');

}

function CerrarPopupRpt() {
    try {
        var mpv = $find('salerptmdl');
        mpv.hide();

    } catch (e) {
        var mpv = $find('CPHIntegraCompany_salerptmdl');
        mpv.hide();
    }
};

function CerrarPopupRptV2() {
    //debugger;
    setTimeout('CierraModal()', 4000);
}

function CierraModal() {
    //debugger; 
    var Cierra;

    try {
        Cierra = document.getElementById('HndModal').value;
    } catch (e) {
        Cierra = document.getElementById('CPHIntegraCompany_HndModal').value;
    }

    if (Cierra == '1') {
        try {
            var mpv = $find('salerptmdl');
            mpv.hide();

        } catch (e) {
            var mpv = $find('CPHIntegraCompany_salerptmdl');
            mpv.hide();
        }
    }
}

//Desarrollado para prueba de reportes Operaciones diaraias 
//y ventas diarias

function CerrarModalRptOperacionesDiarias() {
    debugger;
    setTimeout("CierraModalOperaciones()", 4000);
}

function CierraModalOperaciones() {
    debugger;
    try {
        var modal = $find('mdlOperacion');
        modal.hide();
    }
    catch (e) {
        var modal = $find('CPHIntegraCompany_mdlOperacion');
        modal.hide();
    }
}


function CerrarModalVentaDiaria() {
    debugger;
    setTimeout("CierraModalVentaDia()", 4000);
}

function CierraModalVentaDia() {
    try {
        debugger;
        var modalven = $find('mdlVentaDia');
        modalven.hide();
    } catch (e) {
        var modalven = $find('CPHIntegraCompany_mdlVentaDia');
        modalven.hide();
    }
}

//Desarrollado para prueba de reportes Operaciones diaraias 
//y ventas diarias

// proceso despues de realizar pago exitoso
function gfProcesoEscondeRefresh2() {
    //debugger;
    if (document.getElementById("divProceso").style.visibility == "visible") {

        document.getElementById("divProceso").style.visibility = "hidden";
    }

    __doPostBack('ctl00$CPHIntegraCompany$linkRefresh', '');

    setInterval("openpdf()", 3000);



}
function gfAbrePdfVentaPagada() {
  //  debugger
    setInterval("openpdf()", 7000);

}

function ver(e, m) {
    var t = e.keyCode || e.wich;
    if (t == 13) {
        agregar(m);
        return false;
    }
    return true;
}


function agregar(m) {
    //debugger;
    document.getElementById('ScrollProductos').innerHTML += '<br />' + m;
    document.forms[0].textarea.value = '';
}
//onload = function () {
//    debugger;
//    setInterval(function () { if (window.parar) return; document.getElementById('ScrollProductos').scrollTop = document.getElementById('ScrollProductos').scrollHeight }, 30);
//}

function openpdf() {

    debugger;
    var docpdf = '../Ventas/Documento.aspx';

    window.open(docpdf, "ventana1", "width=500,height=600,scrollbars=YES,toolbar=no,location=0, directories=0, status=0")

    __doPostBack('CPHIntegraCompany_lnkNuevaVenta', '')
}

function limpiar() {

    __doPostBack('linkTest', '')
}


function CerrarCancelaPago() {
    var mpu = $find('mdlPago');
    mpu.hide();

    //    setTimeout("openpdf()", 4000)

}

function redir(parametro) {
    //debugger;
    var tipocambio = document.getElementsByClassName('LabelStyleMoneda');

    try {
        var valorrestante = document.getElementById("txtRestaPago").value;

    } catch (e) {
        var valorrestante = document.getElementById("CPHIntegraCompany_txtRestaPago").value;
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
                    document.getElementById("CPHIntegraCompany_txtRestaPago").value = "$0.00";
                }
                else {
                    document.getElementById(parametro.id).value = '';
                }
            } catch (e) {
                var porcion = nombre.substring(42);
                mn = tipocambio[porcion].textContent;
                if (mn != "110") {
                    //                    parametro.id = nombre;
                    document.getElementById(parametro.id).value = valorrestante;
                    document.getElementById("CPHIntegraCompany_txtRestaPago").value = "$0.00";
                }
                else {
                    document.getElementById(parametro.id).value = '';
                }
            }
        }
    }

    document.getElementById(parametro.id).select();
   
}

function redirb(parametro) {
    // debugger;
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


function redircambio(parametro) {
    //debugger;
    var tipocambio = document.getElementsByClassName('LabelStyleCambioMoneda');

    try {
        var valorcambio = document.getElementById("MontoCambiotxt").value;
    } catch (e) {
        var valorcambio = document.getElementById("CPHIntegraCompany_MontoCambiotxt").value;
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

function GetKey(sender, eventArgs) {
    //debugger;
    try {
        document.getElementById('CPHIntegraCompany_keycontainer').value = eventArgs.get_value();
    } catch (e) {
        document.getElementById('keycontainer').value = eventArgs.get_value();
    }

}

function redirbcambio(parametro) {
    // debugger;
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
function gfProbando() {
 
   //debugger;
    //    alert('entra');
  
    __doPostBack('ctl00$CPHIntegraCompany$linkHolamundo', '');
    
}
function gfProbando2(parametro) {
    alert('entra');

}


/**************************************************************/
