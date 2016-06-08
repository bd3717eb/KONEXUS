
// -----------------------------------------------------
//befoe

//var $asd;

//function pageLoad(sender, args) {

//    if (args.get_isPartialLoad()) // executes the code after asynchronous postbacks.
//    {
//        test();
//        $("#container").val($asd);
//    }
//    else // for postback 
//    { 
//        test();
//        $("#container").val($asd);
//    }
//}

//function test() {
//    var obj = document.getElementById('acceptsalebtn');
//    if (obj) {
//        var curtop = 0;
//        if (obj.offsetParent) {

//            do {
//                curtop += obj.offsetTop;
//            } while (obj = obj.offsetParent);

//            $asd = curtop - 200;
//        }
//    }
//}

function ShowDiv() {
    document.getElementById('printdiv').style.cssText = 'display: block; position:fixed; left:500px;' + 'top:' + $asd + 'px;';
}

function HideDiv() {
    document.getElementById('printdiv').style.cssText = 'display: none;';
}

function GetKey(sender, eventArgs) {
    // debugger;
    try {
        document.getElementById('CPHIntegraCompany_keycontainer').value = eventArgs.get_value();
    } catch (e) {
        document.getElementById('keycontainer').value = eventArgs.get_value();
    }

}

function GetKeyUsuario(sender, eventArgs) {
    //debugger;
    try {
        document.getElementById('CPHIntegraCompany_keycontainerUsuario').value = eventArgs.get_value();
    } catch (e) {
        document.getElementById('keycontainerUsuario').value = eventArgs.get_value();
    }

}


function ShowOptions(control, args) {
    control._completionListElement.style.zIndex = 10000001;
}

function closeWindow() {
    window.open('', '_parent', '');
    window.close();
}

function formatCurrency(number) {
    number = number.toString().replace(/\$|\,/g, '');

    if (isNaN(number))
        number = "0";

    sign = (number == (number = Math.abs(number)));
    number = Math.floor(number * 100 + 0.50000000001);
    cents = number % 100;
    number = Math.floor(number / 100).toString();

    if (cents < 10)
        cents = "0" + cents;

    for (var i = 0; i < Math.floor((number.length - (1 + i)) / 3) ; i++)
        number = number.substring(0, number.length - (4 * i + 3)) + ',' + number.substring(number.length - (4 * i + 3));

    return (((sign) ? '' : '-') + '$' + number + '.' + cents);
}

function gfProceso() {
    // debugger

    if (document.getElementById("divProceso").style.visibility == "visible")
        document.getElementById("divProceso").style.visibility = "hidden";

    else
        document.getElementById("divProceso").style.visibility = "visible";

}

function gfProcesoEsconde() {
    //  debugger
    if (document.getElementById("divProceso").style.visibility == "visible") {

        document.getElementById("divProceso").style.visibility = "hidden";
    }


}


function gfMuestraNotificacion(pRecibetxt) {
    // debugger;
    var w = window.innerWidth;
    var h = window.innerHeight;
    document.getElementById("divNotificacion").style.visibility = "visible";
    document.getElementById("lblNotificacion").innerHTML = pRecibetxt;
    setTimeout('gfMuestraNotificacion2()', 3500);
}

function gfMuestraNotificacion2() {
    try {
        //  debugger;
        document.getElementById("divNotificacion").style.visibility = "hidden";
    } catch (e) {

    }
}

function gfProcesoEscondeRefresh() {
    // debugger
    if (document.getElementById("divProceso").style.visibility == "visible") {

        document.getElementById("divProceso").style.visibility = "hidden";
    }
    __doPostBack('linkRefresh', '');
}
function gfProcesoEscondeRefresh1() {
    // debugger
    if (document.getElementById("divProceso").style.visibility == "visible") {

        document.getElementById("divProceso").style.visibility = "hidden";
    }
    __doPostBack('ctl00$CPHIntegraCompany$lnkRefresca', '');
}



function gfProcesoRefresh() {

    __doPostBack('ctl00$CPHIntegraCompany$linkRefresh', '');

}

function redir(parametro) {
    // debugger;

    var valorrestante = document.getElementById("restantxt").value;
    var tipocambio = document.getElementsByClassName('LabelStyleImport');

    if (document.getElementById(parametro.id).value == "$0.00" || document.getElementById(parametro.id).value == 0 || document.getElementById(parametro.id).value == "$0" || document.getElementById(parametro.id).value == "$0.0") {
        if (tipocambio) {

            var cajas = document.getElementsByClassName('TextBoxStyleImport');

            for (var recorre = 0; recorre < cajas.length; recorre++) {
                cajas[recorre].disabled = true;
            }

            document.getElementById(parametro.id).disabled = false;
            var nombre = parametro.id;
            var porcion = nombre.substring(24);

            mn = tipocambio[porcion].textContent;
            if (mn == "1.0000") {
                document.getElementById(parametro.id).value = valorrestante;

            }
            else {
                document.getElementById(parametro.id).value = '';
            }
        }
    }

}

function redircambio(parametro) {
    //  debugger;

    var valorcambio = document.getElementById("CPHIntegraCompany_payCambiotxt").value;
    var tipocambio = document.getElementsByClassName('LabelStyleChange');

    if (document.getElementById(parametro.id).value == "$0.00" || document.getElementById(parametro.id).value == 0 || document.getElementById(parametro.id).value == "$0" || document.getElementById(parametro.id).value == "$0.0") {
        if (tipocambio) {

            var cajas = document.getElementsByClassName('TextBoxStyleChange');

            for (var recorre = 0; recorre < cajas.length; recorre++) {
                cajas[recorre].disabled = true;
            }

            document.getElementById(parametro.id).disabled = false;
            var nombre = parametro.id;
            var porcion = nombre.substring(21);

            mn = tipocambio[porcion].textContent;
            if (mn == "1.0000") {
                document.getElementById(parametro.id).value = valorcambio;

            }
            else {
                document.getElementById(parametro.id).value = '';
            }
        }
    }

}

function focusIt() {

    var credittext = document.getElementById("credittxt");
    var contacttext = document.getElementById("contacnametxt");

    if (credittext != null) {
        credittext.focus();
        credittext.value = formatCurrency(credittext.value);
    }

    if (contacttext != null) {
        contacttext.focus();
    }
}

onload = focusIt;
function ValidNum(e) {
    var tecla = document.all ? tecla = e.keyCode : tecla = e.which;

    return ((tecla > 47 && tecla < 58) || tecla == 46);
}

function ValidNumFecha(e) {
    var tecla = document.all ? tecla = e.keyCode : tecla = e.which;

    return ((tecla > 46 && tecla < 58));
}


function ValidarCaracteres(textareaControl, maxlength) {
    if (textareaControl.value.length > maxlength) {
        textareaControl.value = textareaControl.value.substring(0, maxlength);

    }
}
function gfProcesoImprime() {
    //    debugger;
    gfProcesoEsconde();
    var mdl = $find('CPHIntegraCompany_mdlpartidas');
    /*mdl.hide();*/
    __doPostBack('ctl00$CPHIntegraCompany$linkRefresh', '');
    /*setInterval("gfImprimeDevolucion()", 2000);*/
    __doPostBack('ctl00$CPHIntegraCompany$linkImprime', '');
};

function gfImprimeDevolucion() {
    //    debugger;
    __doPostBack('ctl00$CPHIntegraCompany$linkImprime', '');

}
//function gfProcesoDescarga() {
//    // debugger;
//    __doPostBack('ctl00$CPHIntegraCompany$linkDescarga', '');
//    gfProcesoEsconde();
//};
//function gfProcesoTerminaProceso() {
//     debugger;
//    __doPostBack('ctl00$CPHIntegraCompany$LinkTerminaProceso', ''); 
//    gfProcesoEsconde();
//};

function gfProcesoDescarga() {
    // debugger;
    __doPostBack('ctl00$CPHIntegraCompany$linkDescarga', '');
    gfProcesoEsconde();
    setTimeout("openpdfdevo()", 3000);
};
function openpdfdevo() {


    var docpdf = '../Ventas/Documento.aspx';

    window.open(docpdf, "ventana1", "width=500,height=600,scrollbars=YES,toolbar=no,location=0, directories=0, status=0")


}




function lfToogle(pPropiedades) {
    var bandera = true;

    //debugger;
    var divs = document.getElementsByClassName(pPropiedades.id);
    if (divs[0].style.display == 'none')
        bandera = false;

    if (bandera) {
        for (var i = 0; i < divs.length; i++) {
            divs[i].style.display = 'none';
            bandera = false;
        }
    } else {
        for (var i = 0; i < divs.length; i++) {
            divs[i].style.display = 'block';
            bandera = true;
        }
    }
}
// FIN

/***************MENSAJES*********************/

//debugger;
//$(document).ready(function () {

//    document.getElementById("dtMensaje_0").style.visibility = ;
//    document.getElementById("dtMensaje_1").style.visibility = "hidden";
//    document.getElementById("dtMensaje_2").style.visibility = "hidden";

//    setTimeout(function Reset() {
//        debugger;
//        var rango_superior = 0;
//        var rango_inferior = 2;
//        var aleatorio = Math.floor(Math.random() * (rango_superior - (rango_inferior - 1))) + rango_inferior;
//     
//    }, 5000);

//});


function mostrarMensaje() {
    //  debugger;

    try {
        var numTablas = document.getElementById('CPHIntegraCompany_HdnTablasMensajes').value;
    } catch (e) {
        var numTablas = document.getElementById('HdnTablasMensajes').value;
    }
    if (numTablas >= 1) {
        for (var i = 0; i < numTablas; i++) {
            var tabla = document.getElementById("dtMensaje_" + i)
            tabla.style.display = 'none';
        }

        var rango_superior = numTablas - 1;
        var rango_inferior = 0;
        var aleatorio = Math.floor(Math.random() * (rango_superior - (rango_inferior - 1))) + rango_inferior;
        var tabla = document.getElementById("dtMensaje_" + aleatorio)
        tabla.style.display = '';
    }

}
function resetTiempo() {

    clearTimeout(pop)
}

$(document).ready(function () {
    //    debugger;
    //     INCIDENCIA
    try {
        var Time = document.getElementById('CPHIntegraCompany_HdnTiempo').value * 1000;
    } catch (e) {
        var Time = document.getElementById('HdnTiempo').value * 1000;
    }

    if (Time == "") {
        pop = setInterval("mostrarMensaje()", 5000)
    } else {
        pop = setInterval("mostrarMensaje()", Time)
    }
});

/*******************REGISTRO DE PAGOS MULTIPLES*****************************************/


function gfProcesoEscondeRefresh2() {
    // debugger;
    if (document.getElementById("divProceso").style.visibility == "visible") {

        document.getElementById("divProceso").style.visibility = "hidden";
    }
    __doPostBack('CPHIntegraCompany_linkRefresh', '')
    setInterval("openpdf()", 3000);
}