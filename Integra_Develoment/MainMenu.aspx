<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="Integra_Develoment.MainMenu" %>

<!DOCTYPE html>
<html lang="es-mx">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="menu" />
    <meta name="author" content="steve.blanquel">
    <link rel="icon" href="Images/konexusico.png">

    <title>Konexus Dashboard</title>
    <!-- Bootstrap core CSS -->
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
    <link href="Styles/album.css" rel="stylesheet">

    <style type="text/css">
        #divresponse_info_company {
            font-size: 10px;
            font-weight: 100;
            font-family: Verdana,Arial, Helvetica;
            display: inline-block;
        }

        #divresponse_info_user {
            left: 55%;
            position: absolute;
            height: 82px;
            font-size: 10px;
            font-weight: 100;
            font-family: Tahoma;
            text-align: right;
            top: 0px;
            margin-top: 1%;
        }
    </style>

    <!-- JS library core -->
    <script defer src="Scripts/jquery-2.2.3.min.js" type="text/javascript"></script>
    <script defer src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script defer src="Scripts/jscoreknxs.js" type="text/javascript"></script>
    <script src="Scripts/jsIntegra.js" type="text/javascript"></script>

    <script defer type="text/javascript">

        function lfSetIDModule(idmodule) {

            var url = document.location.href;
            switch (idmodule) {
                case 1:
                    $("body").hide(100).delay(500);

                    url = url.replace('MainMenu.aspx', 'Ventas/Ventas.aspx')
                    window.location.href = url.toString();
                    break;
                case 5:
                    $("body").hide(100).delay(500);
                    url = url.replace('MainMenu.aspx', 'Contabilidad/ReportePoliza.aspx')
                    window.location.href = url.toString();
                    break;
                case 7:
                    debugger;
                    $("body").hide(100).delay(500);
                    url = url.replace('MainMenu.aspx', 'Tesoreria/PagarCxP_CxC_Archivo.aspx')
                    window.location.href = url.toString();
                    break;
                case 8:
                    $("body").hide(100).delay(500);
                    url = url.replace('MainMenu.aspx', 'Almacen/AlmacenEntradas.aspx')
                    window.location.href = url.toString();
                    break;

                default:
                    alert('En construccíon');
                    break;
            }
            /*
            // Verífica acceso a la aplicación
            $.ajax({
                type: "POST",
                url: "Background.aspx/lfsetIDModule",
                data: "{idmodule:" + idmodule + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == 'OK') {
                        var url = document.location.href;
                        switch (idmodule) {
                            case 1:
                                $("body").hide(100).delay(500);
                                url = url.replace('MainMenuV2.html', '/Ventas/Ventas.aspx')
                                window.location.href = url.toString();
                                break;
                            case 5:
                                $("body").hide(100).delay(500);
                                url = url.replace('MainMenuV2.html', '/Contabilidad/ReportePoliza.aspx')
                                window.location.href = url.toString();
                                break;
                            case 7:
                                debugger;
                                $("body").hide(100).delay(500);
                                url = url.replace('MainMenuV2.html', '/Tesoreria/PagarCxP_CxC_Archivo.aspx')
                                window.location.href = url.toString();
                                break;
                            case 8:
                                $("body").hide(100).delay(500);
                                url = url.replace('MainMenuV2.html', '/Almacen/AlmacenEntradas.aspx')
                                window.location.href = url.toString();
                                break;

                            default:
                                alert('En construccíon');
                                break;
                        }
                    } else {
                        alert('No se encontraron los derechos para este programa.');
                    }
                    return false;


                },
                error: function (msg) {
                    alert('no se encontro recurso');
                    //console.log(msg.d);
                    //window.location.href = "../Default.aspx/gfGetCredentials"
                }
            });*/
        }
    </script>


</head>
<body style="display: none;">
    <input type="hidden" id="hc" value="hi!" />

    <div class="row" style="border-color: transparent; border-bottom-style: solid; border-bottom-color: #a2160c; border-bottom-width: 8px; height: 116px;">
        <div class="col-sm-4">
            <img src="Images/Logo.png" alt="Logo" />
        </div>
        <div class="col-sm-4">
            <div id="divresponse_info_company"></div>
        </div>
        <div class="col-sm-4">
            <div id="divresponse_info_user"></div>
        </div>
    </div>

    <div class="container">
        <div class="album text-muted">
            <div class="row">
                <div class="card">
                    <img class="card-img-top" src="Images/Menu/ventas.png" width="100" height="100" id="imgVentas" alt="Ventas" data-toggle="Modulo de ventas">
                    <div class="card-block">
                        <h5 class="card-title">Ventas</h5>
                    </div>
                </div>
                <div class="card">
                    <img class="card-img-top" data-src="..." src="Images/Menu/Tesoreria.png" width="100" height="100" id="imgTesoreria" alt="Tesorería">
                    <div class="card-block">
                        <h5 class="card-title">Tesoreria</h5>
                    </div>
                </div>
                <div class="card">
                    <img class="card-img-top" data-src="..." src="Images/Menu/Almacen.png" width="100" height="100" alt="Card image cap" id="imgAlmacen">
                    <div class="card-block">
                        <h5 class="card-title">Almacén</h5>
                    </div>
                </div>
                <div class="card">
                    <img class="card-img-top" data-src="..." src="Images/Menu/Contabilidad.png" width="100" height="100" alt="Card image cap" id="imgContabilidad">
                    <div class="card-block">
                        <h5 class="card-title">Contabilidad</h5>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="card">
                    <img class="card-img-top" data-src="..." src="Images/Menu/Caja.png" width="100" height="100" alt="Card image cap" id="imgCaja">
                    <div class="card-block">
                        <h5 class="card-title">Caja</h5>
                    </div>
                </div>
                <div class="card">
                    <img class="card-img-top" data-src="..." src="Images/Menu/Compras.png" width="100" height="100" alt="Card image cap" id="imgCompras">
                    <div class="card-block">
                        <h5 class="card-title">Compras</h5>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
