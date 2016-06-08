// ===========================================================
// 26.05.2016
// lfToogle se paso a jsIntegra para que sea visible en todas las webforms heredades de la masterpage mpIntegraCompany
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
// ===========================================================

// BEGIN FUNCIONES JAVASCRIPT USADAS 
// ============================================================
// plugin js
// ============================================================
//$.fn.ForceNumericOnly =
//function () {
//    return this.each(function () {
//        $(this).keydown(function (e) {
//            var key = e.charCode || e.keyCode || 0;
//            // @eblaher
//            // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
//            // home, end, period, and numpad decimal
//            return (
//                key == 8 ||
//                key == 9 ||
//                key == 46 ||
//                key == 110 ||
//                key == 190 ||
//                (key >= 35 && key <= 40) ||
//                (key >= 48 && key <= 57) ||
//                (key >= 96 && key <= 105));
//        });
//    });
//};

// ============================================================
// end plugin js
// ============================================================

function gfSalirSistema() {
    if (confirm("¿ Desea salir del sistema?")) {
        $.ajax({
            type: "POST",
            url: "../Background.aspx/gfSalirdelSistema",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                console.log(msg.d);
                // ______ alert("message" + '&nbsp;' + msg.d);
                // return false;
                //
                //window.location.href("../Default.aspx");
                return true;
            },
            error: function (msg) {
                //alert(msg.d + "error");
                console.log(msg.d + "error");
                //window.location.href("../Default.aspx");
                return true;
            }
        });
        //localStorage.myPageDataArr = undefined;
        localStorage.setItem('lfcredentials', '');
        window.location.href = "../Default.aspx";
        return true;
    }
}

function lfUnidadNegocio(parametro) {

    $.ajax({
        type: "POST",
        url: "../Background.aspx/CambiaValorUnidad",
        data: "{ valor:'" + parametro + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //______ alert("message" + '&nbsp;' + msg.d);
            return false;
        },
        error: function (msg) {
            alert(msg.d + "error");
        }
    });

};



// END FUNCIONES JAVASCRIPT USADAS 
//function funciona() {
//    //debugger;
//    var iframe = document.getElementById('integramenu');
//    var frameDoc = iframe.contentDocument || iframe.contentWindow.document;
//    var el = frameDoc.getElementById(divProceso2);
//    if (document.getElementById("divProceso2").style.visibility == "visible")
//        document.getElementById("divProceso2").style.visibility = "hidden";
//    else
//        document.getElementById("divProceso2").style.visibility = "visible";
//}

function gfMuestraNotificacion(pRecibetxt) {
    debugger;
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


//function gfProceso() {
//    //debugger;
//    if (document.getElementById("divProceso").style.visibility == "visible")
//        document.getElementById("divProceso").style.visibility = "hidden";
//    else
//        document.getElementById("divProceso").style.visibility = "visible";
//}

//function gfProcesoDefault() {
//    //debugger;

//    if (document.getElementById("divProceso").style.visibility == "visible")
//        document.getElementById("divProceso").style.visibility = "hidden";
//    else
//        document.getElementById("divProceso").style.visibility = "visible";
//}

//function gfFunciona() {
//    try {

//        $.ajax({
//            type: "POST",
//            url: "FacturacionCancelacion.aspx/gfTest",
//            data: "{}",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (msg) {
//                //______ alert("message" + '&nbsp;' + msg.d);
//                return false;
//            },
//            error: function (msg) {
//                alert(msg.d + "error");
//            }
//        });
//    } catch (e) {
//        alert(e.get_Message + '\t' + e.get_Description);
//    }
//}

///***************************************************
//Menu master*/
//$(document).ready(function () {    // everything goes here
//    $(".nav").children("li").each(function () {
//        $(this).children("a").css({ backgroundImage: "none" });
//    });
//});

function gfProceso() {
    // debugger

    if (document.getElementById("divProceso").style.visibility == "visible")
        document.getElementById("divProceso").style.visibility = "hidden";

    else
        document.getElementById("divProceso").style.visibility = "visible";

}

function gfProcesoEsconde() {
    //  debugger
    try {
        if (document.getElementById("divProceso").style.visibility == "visible") {

            document.getElementById("divProceso").style.visibility = "hidden";
        }
    } catch (e) {
        console.log(e.message);
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

function GetKey(sender, eventArgs) {
    // debugger;
    try {
        document.getElementById('CPHIntegraCompany_keycontainer').value = eventArgs.get_value();
    } catch (e) {
        document.getElementById('keycontainer').value = eventArgs.get_value();
    }
}