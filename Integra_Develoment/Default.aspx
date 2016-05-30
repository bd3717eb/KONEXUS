<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Integra_Develoment.Default" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="CFDI" />
    <meta name="author" content="eban" />
    <link rel="icon" href="Images/konexusico.png" />

    <title>Konexus Inicia sesión</title>
    <link href="Styles/bootstrap.css" type="text/css" rel="stylesheet" />
    <style>
        body {
            padding-top: 40px;
            padding-bottom: 40px;
            background-color: #f1f1f1;
        }

        .form-signin {
            max-width: 330px;
            padding: 15px;
            margin: 0 auto;
        }

            .form-signin .form-signin-heading,
            .form-signin .checkbox {
                margin-bottom: 10px;
            }

            .form-signin .checkbox {
                font-weight: normal;
            }

            .form-signin .form-control {
                position: relative;
                height: auto;
                -webkit-box-sizing: border-box;
                -moz-box-sizing: border-box;
                box-sizing: border-box;
                padding: 10px;
                font-size: 16px;
            }

                .form-signin .form-control:focus {
                    z-index: 2;
                }

            .form-signin input[type="email"] {
                margin-bottom: -1px;
                border-bottom-right-radius: 0;
                border-bottom-left-radius: 0;
            }

            .form-signin input[type="password"] {
                margin-bottom: 10px;
                border-top-left-radius: 0;
                border-top-right-radius: 0;
            }
    </style>
    <script type="text/javascript" src="Scripts/jquery-2.2.3.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>

    <script type="text/javascript">

        function lfCompanySetValue(argument) {

            //debugger;
            var vidcompany = argument.getAttribute('iddrop');
            var vidperson = document.getElementById("txtUser").value;

            // set text 
            $("#dropcompany").text(argument.outerText);
            $("#dropcompany").attr("iddropcompanyselect", vidcompany);

            $.ajax({
                type: "POST",
                url: "Background.aspx/lfFillDropOffice",
                data: "{ idcompany:'" + vidcompany + "',idperson:'" + vidperson + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var vco = msg.d;
                    $("#lblOficce").html(vco);
                    return true;
                },
                error: function (msg) {
                    //alert(msg.d + "error");
                    console.log(msg.d + "error");
                    //window.location.href("../Default.aspx");
                    return true;
                }
            });

        }

        function lfOfficeSetValue(argument) {
            var vidoffice = argument.getAttribute('iddrop');
            $("#dropoffice").text(argument.outerText);
            $("#dropoffice").attr("iddropofficeselect", vidoffice);
        }

        $(document).ready(function () {
            //alert('hello world');

            $("#txtUser").change(function () {
                //debugger;
                var viduser = $("#txtUser").val();
                if (viduser != '') {
                    document.getElementById('divCompanyOffice').style.visibility = 'visible';
                    document.getElementById('btnCompanyOffice').style.visibility = 'hidden';
                    document.getElementById('btnCompanyOffice').style.margin = '-1.5em';

                    $.ajax({
                        type: "POST",
                        url: "Background.aspx/lfGetCompaniesxUser",
                        data: "{ sidUser:'" + viduser + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            //debugger;
                            if (msg.d != '') {
                                var vco = msg.d.split('|');
                                $("#lbldropcompany").html(vco[0]);
                                $("#lblOficce").html(vco[1]);
                                return true;
                            }
                            else {
                                document.getElementById('divCompanyOffice').style.visibility = 'hidden';
                                document.getElementById('btnCompanyOffice').style.visibility = 'visible';
                                document.getElementById('btnCompanyOffice').style.margin = '0 0 10px';
                                //alert('Usuario no existe')
                            }


                        },
                        error: function (msg) {
                            //debugger;
                            //alert(msg.d + "error");
                            console.log(msg.d + "error");
                            //window.location.href("../Default.aspx");
                            return true;
                        }
                    });
                } else {
                    document.getElementById('divCompanyOffice').style.visibility = 'hidden';
                    document.getElementById('btnCompanyOffice').style.visibility = 'visible';
                    document.getElementById('btnCompanyOffice').style.margin = '0 0 10px';
                }
            });

            $("#form1").submit(function () {
                // debugger;
                var vUser = document.getElementById('txtUser').value;
                var vPWD = document.getElementById('inputPassword').value;

                if (document.getElementById('dropcompany') == null) {
                    alert('Verificar usuario, contraseña y/o Empresa')
                    return false;
                }

                var vidCompany = document.getElementById('dropcompany').getAttribute('iddropcompanyselect');
                var vidOffice = 0;
                if (document.getElementById('dropoffice') != null) {
                    vidOffice = document.getElementById('dropoffice').getAttribute('iddropofficeselect');
                }

                if (vUser != '' && vPWD != '') {
                    $.ajax({
                        type: "POST",
                        url: "Background.aspx/User_Validation",
                        data: "{ user:'" + vUser + "',pwd:'" + vPWD + "',company:'" + vidCompany + "',office:'" + vidOffice + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            // debugger;
                            if (msg.d != '200') {
                                alert('Verificar usuario y/o contraseña');
                                return false;
                            } else {
                                window.location = 'MainMenu.aspx';
                            }
                        },
                        error: function (msg) {
                            //alert(msg.d + "error");
                            console.log(msg.d + "error");
                            //window.location.href("../Default.aspx");
                            return true;
                        }
                    });

                }
                return false;
            });
        });
    </script>
</head>
<body>
    <div class="container">
        <div id="divImage" style="position: relative; text-align: left;">
            <img src="Images/Logo.png" alt="Konexus" />
        </div>
        <form id="form1" runat="server" class="form-signin">
            <h2 class="form-signin-heading">Iniciar Sesión</h2>
            <h6 class="form-signin-heading">Especifique su Número de Usuario y Contraseña.</h6>
            <label for="txtUser" class="sr-only" autofocus>Número de usuario</label>
            <input type="number" id="txtUser" class="form-control" placeholder="Número de usuario" required title="Especifique su Número de Usuario." />
            <br />
            <label for="inputPassword" class="sr-only">Contraseña</label>
            <input type="password" id="inputPassword" class="form-control" placeholder="Contraseña" required title="Especifique su Contraseña." />
            <p id="btnCompanyOffice" style="visibility: visible;">
                <button class="btn btn-lg btn-primary btn-block" type="#">Entrar</button>
            </p>
            <div id="divCompanyOffice" style="visibility: hidden;">

                <h4>Empresa<span class="label label-danger"></span></h4>
                <div class="dropdown">
                    <asp:Label ID="lbldropcompany" runat="server" Text=""></asp:Label>
                </div>
                <h4>Unidad de negocio <span class="label label-success"></span></h4>
                <div class="dropdown">
                    <asp:Label ID="lblOficce" runat="server" Text=""></asp:Label>
                </div>
                <br />
                <button class="btn btn-lg btn-primary btn-block" type="submit">Entrar</button>
            </div>

        </form>
    </div>
    <!-- /container -->
</body>
</html>
