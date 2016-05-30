// =====================================================================
// MainMenuV2
// =====================================================================
$(document).ready(function () {

    $("body").css("display", "none");
    $("body").fadeIn(2500);

    lfcredentials();

    $('.card img').click(function () {
        //debugger;
        var vIDModulo = $(this)[0].id
        var iModule = -1;

        switch (vIDModulo) {
            case 'imgVentas':
                iModule = 1;
                lfSetIDModule(iModule);
                break;
            case 'imgTesoreria':
                iModule = 7;
                lfSetIDModule(iModule);
                break;
            case 'imgAlmacen':
                iModule = 8;
                lfSetIDModule(iModule);
                break;

            case 'imgContabilidad':
                iModule = 5;
                lfSetIDModule(iModule);
                break;

            case 'imgCaja':
                iModule = 0;
                lfSetIDModule(iModule);
                break;
            case 'imgCompras':
                window.open("http://189.206.75.142/Citrix/XenApp/auth/login.aspx");
                window.location.href = "MainMenuV2.html";
                break;
            default:
                alert(vIDModulo);
        }
    });

});

function lfcredentials() {
    //debugger;

    //console.log(document.getElementById('hc').value);
    var bflag = false;

    if (localStorage.getItem('lfcredentials') == null || localStorage.getItem('lfcredentials') == '') {
        $.ajax({
            type: "POST",
            url: "Background.aspx/lfgetCredentials",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == -409) { window.location.href = "../Default.aspx"; }

                localStorage.setItem('lfcredentials', msg.d);

                var va = msg.d.split('|');
                $("#divresponse_info_company").append(va[1]);

                var tmp = va[0].replace('../Images', 'Images');
                tmp = tmp.replace('../Images', 'Images');

                $("#divresponse_info_user").append(tmp);
                bflag = true;
            },
            error: function (msg) {
                window.location.href = "../Default.aspx"
                bflag = false;
            }
        });
    } else {
        //console.log(localStorage.getItem('lfcredentials'));
        var va = localStorage.getItem('lfcredentials').split('|');
        var tmp = va[0].replace('../Images', 'Images');
        tmp = tmp.replace('../Images', 'Images');
        $("#divresponse_info_company").append(va[1]);
        $("#divresponse_info_user").append(tmp);
        bflag = true;
    }
    return bflag;
}


// =====================================================================
