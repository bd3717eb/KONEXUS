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

