<%@ Page Title="" Language="C#" MasterPageFile="~/mpIntegraCompany.Master" AutoEventWireup="true" CodeBehind="Groups.aspx.cs" Inherits="Integra_Develoment.Ventas.Groups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHIntegraCompany" runat="server">
    <fieldset class="fieldset">
        <div id="divProceso" class="cCargando" style="visibility: hidden;">
            <div id="divProcesoMsg" class="cCargandoImg">
                <br />
                <asp:Image ID="imgload" runat="server" ImageUrl="#" />
                <br />
            </div>
        </div>
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label_title" Visible="false">
        </asp:Label>
        <legend>Datos cliente</legend>

        <input type="radio" class="radio" checked="checked" name="x" value="y" id="y" style="display: inline;" />
        <label for="y">Nuevo</label>
        <input type="radio" class="radio" name="x" value="z" id="z" style="display: inline;" />
        <label for="z">Existente</label>
        <br />
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <th colspan="2">Datos generales</th>
                        </tr>
                        <tr>
                            <td>
                                <label class="label">Razón social</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="TextBoxEstilo"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="label">Nombre corto</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreCorto" runat="server" CssClass="TextBoxEstilo"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="label">Giro comercial </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGiroComercial" runat="server" CssClass="TextBoxEstilo"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <label class="label">Codigo postal</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCP" runat="server" CssClass="TextBoxEstilo"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="label">Calle y número</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCalleNumero" runat="server" CssClass="TextBoxEstilo"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="label">Colonia</label></td>
                            <td>
                                <asp:TextBox ID="txtColonia" runat="server" CssClass="TextBoxEstilo"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="label">Estado</label></td>
                            <td>
                                <asp:TextBox ID="txtEstado" runat="server" CssClass="TextBoxEstilo"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="label">Delegación/Municipio</label></td>
                            <td>
                                <asp:TextBox ID="txtDelMun" runat="server" CssClass="TextBoxEstilo"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="label">Teléfono</label></td>
                            <td>
                                <asp:TextBox ID="txtTel" runat="server" CssClass="TextBoxEstilo"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="label">Email</label></td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="TextBoxEstilo"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <label class="label">RFC</label></td>
                            <td>
                                <asp:TextBox ID="txtRFC" runat="server" CssClass="TextBoxEstilo"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>


    </fieldset>
</asp:Content>
