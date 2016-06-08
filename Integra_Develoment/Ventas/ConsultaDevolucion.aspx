<%@ Page Title="" Language="C#" MasterPageFile="~/mpIntegraCompany.Master" AutoEventWireup="true" CodeBehind="ConsultaDevolucion.aspx.cs" Inherits="Integra_Develoment.Ventas.ConsultaDevolucion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHIntegraCompany" runat="server">

    <script src="../Scripts/jsVentas.js" type="text/javascript"></script>
    <script src="../Scripts/Scripts.js" type="text/javascript"></script>

    <div id="divMenuRight">
        <div id="divMenuRightItem_1" style="color: #000; background-color: #f5f5f5; margin-bottom: 3px; display: block; box-shadow: 1px 1px 3px #e7e7e7; cursor: pointer;">
            &nbsp;
            <img src="../Images/ModIcoEvento.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="btnLimpiar" runat="server" Text=" Limpiar datos" OnClick="LimpiarClick"></asp:LinkButton>
        </div>
        <div id="divMenuRightItem_2" onclick="lfToogle(this);" style="color: #000; background-color: #f5f5f5; margin-bottom: 3px; display: block; box-shadow: 1px 1px 3px #e7e7e7; cursor: pointer;">
            &nbsp;
            <img src="../Images/ModIcoEvento.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="btnBuscar" runat="server" Text=" Buscar ventas" OnClick="BuscarClick"></asp:LinkButton>
        </div>
        <div id="div1" onclick="lfToogle(this);" style="color: #000; background-color: #f5f5f5; margin-bottom: 3px; display: block; box-shadow: 1px 1px 3px #e7e7e7; cursor: pointer;">
            &nbsp;
            <img src="../Images/ModIcoEvento.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="linkBusquedaDevoluciones" runat="server" Text=" Búsqueda de devoluciones"
                OnClick="linkBusquedaDevoluciones_Click"></asp:LinkButton>
        </div>
        <div id="div2" onclick="lfToogle(this);" style="color: #000; background-color: #f5f5f5; margin-bottom: 3px; display: block; box-shadow: 1px 1px 3px #e7e7e7; cursor: pointer;">
            &nbsp;
            <img src="../Images/ModIcoEvento.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="btnRegresar" runat="server" Text=" Regresar" OnClick="btnRegresar_Click"></asp:LinkButton>
        </div>
    </div>
    <fieldset class="fieldset">
        <div id="divProceso" class="cCargando" style="visibility: hidden;">
            <div id="divProcesoMsg" class="cCargandoImg">
                <br />
                <asp:Image ID="imgload" runat="server" ImageUrl="../Images/Ventas/loading.gif" />
                <br />
            </div>
        </div>
        <legend>Devoluciones </legend>
        <asp:HiddenField ID="HdnEmpresa" runat="server" />
        <asp:HiddenField ID="HdnUsuario" runat="server" />
        <asp:HiddenField ID="HdnSucursal" runat="server" />
        <asp:Label ID="lblIdCliente" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LblNombreCorto" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblIdBeneficiario" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblNombreBeneficiario" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblClasIva" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblDesIva" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTipoCambio" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblFecha" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblFolioFiscal" runat="server" Visible="false"></asp:Label>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Label ID="lblError" runat="server" Visible="false" CssClass="label_title"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="display: none">
            <asp:LinkButton ID="linkImprime" runat="server" OnClick="linkImprime_Click">imprime</asp:LinkButton>
            <asp:LinkButton ID="linkDescarga" runat="server" OnClick="Descarga_Click">imprime</asp:LinkButton>
            <asp:LinkButton ID="linkRefresh" runat="server" OnClick="lnkRefresca_Click">refresca</asp:LinkButton>

            <asp:LinkButton ID="LinkTerminaProceso" runat="server" OnClick="lnkTerminaProceso_Click">refresca</asp:LinkButton>
        </div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblNombreCliente" runat="server" Text="Cliente" CssClass="label_title"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="TextBoxEstilo" AutoPostBack="true"
                        Width="90%" OnTextChanged="txtNombreCliente_TextChanged"> </asp:TextBox>
                    <asp:AutoCompleteExtender ID="acmptClientesVentas" runat="server" TargetControlID="txtNombreCliente"
                        ServicePath="~/ClienteFacturaWebService.asmx" MinimumPrefixLength="1" ServiceMethod="GetClients"
                        EnableCaching="true" UseContextKey="true" CompletionInterval="0" CompletionSetCount="10"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem">
                    </asp:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lblCliente" runat="server" CssClass="label"> </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDocumento" runat="server" Text="Documento" CssClass="label_title"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpDocumento" runat="server" AutoPostBack="true" Width="105px"
                        CssClass="DropDown" OnSelectedIndexChanged="drpDocumento_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSerie" runat="server" Text="Serie" CssClass="label_title"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpSerie" runat="server" Width="105px" CssClass="DropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFechaDe" runat="server" Text="Fecha de" CssClass="label_title"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaDe" CssClass="TextBoxEstilo" Width="100px" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="ImgBntCalc" runat="server" ImageUrl="~/Images/calendario.png" />
                    <asp:MaskedEditExtender ID="dateMaskedEditExtender" runat="server" TargetControlID="txtFechaDe"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="True"></asp:MaskedEditExtender>
                    <asp:MaskedEditValidator ID="dateMaskedEditValidator" runat="server" ControlExtender="dateMaskedEditExtender"
                        ControlToValidate="txtFechaDe" EmptyValueMessage="La fecha es requerida" InvalidValueMessage="La fecha es Incorrecta"
                        Display="Dynamic" TooltipMessage=""></asp:MaskedEditValidator>
                    <asp:ValidatorCalloutExtender ID="dateMaskedEditValidator_vce" TargetControlID="dateMaskedEditValidator"
                        Enabled="True" runat="server">
                    </asp:ValidatorCalloutExtender>
                    <asp:CalendarExtender ID="cexCalendarExtender" CssClass="cal_Theme1" TargetControlID="txtFechaDe"
                        PopupButtonID="ImgBntCalc" runat="server"></asp:CalendarExtender>
                </td>
                <td>
                    <asp:Label ID="lblFechaHasta" runat="server" Text="Hasta" CssClass="label_title"> </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaHasta" CssClass="TextBoxEstilo" Width="100px" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="imgCalen" runat="server" ImageUrl="~/Images/calendario.png" />
                    <asp:MaskedEditExtender ID="MaskedEditExtender" runat="server" TargetControlID="txtFechaHasta"
                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="True"></asp:MaskedEditExtender>
                    <asp:MaskedEditValidator ID="MaskedEditValidator" runat="server" ControlExtender="MaskedEditExtender"
                        ControlToValidate="txtFechaHasta" EmptyValueMessage="La fecha es requerida" InvalidValueMessage="La fecha es requerida"
                        Display="Dynamic" TooltipMessage=""></asp:MaskedEditValidator>
                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender" TargetControlID="MaskedEditValidator"
                        Enabled="True" runat="server">
                    </asp:ValidatorCalloutExtender>
                    <asp:CalendarExtender ID="CalendarioDos" CssClass="cal_Theme1" TargetControlID="txtFechaHasta"
                        PopupButtonID="imgCalen" runat="server"></asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNumeroDe" runat="server" Text="Número de" CssClass="label_title"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumeroDe" CssClass="TextBoxEstilo" Width="100px" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblNumeroHasta" runat="server" Text="Hasta" CssClass="label_title"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumeroHasta" CssClass="TextBoxEstilo" Width="100px" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Moneda" CssClass="label_title" Style="color: #444444;"> </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpMoneda" Enabled="false" runat="server" CssClass="DropDown"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset class="fieldset">
        <legend>&nbsp;</legend>
        <asp:UpdatePanel ID="UpdtDocumentos" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="grvDocumentos" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView ID="grvDocumentos" runat="server" Font-Size="Small" AutoGenerateColumns="false"
                    CellPadding="4" GridLines="None" AllowPaging="True"
                    PageSize="8" OnRowDataBound="grvDocumentos_RowDataBound" OnPageIndexChanging="grvDocumentos_PageIndexChanging"
                    DataKeyNames="NUMERO_FACTURA,NUMERO,NUMERO_COTIZA,NUMERO_CLIENTE,CLIENTE,TIPO_CAMBIO,Clas_IVA,FECHA,Des_Iva,Folio_Fiscal"
                    OnSelectedIndexChanged="grvDocumentos_SelectedIndexChanged" Width="99%">
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" SelectText="" ItemStyle-Width="0" HeaderStyle-CssClass="NoMostrar"
                            ItemStyle-CssClass="NoMostrar">
                            <ItemStyle Width="0px"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="NUMERO" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="NUMERO_FACTURA" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="NUMERO_COTIZA" Visible="false"></asp:BoundField>
                        <asp:BoundField HeaderText="Cliente" DataField="CLIENTE" ItemStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Fecha" DataField="FECHA" ItemStyle-HorizontalAlign="Center"
                            DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Documento" DataField="CONCATENADO" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total" DataField="TOTAL" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:c2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Estatus" DataField="ESTATUS" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NUMERO_CLIENTE" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="TIPO_CAMBIO" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="Clas_IVA" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="Des_Iva" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="Folio_Fiscal" Visible="false"></asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <FooterStyle CssClass="GridViewFooterStyles" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="6" Position="Bottom" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <asp:Panel ID="outerpartidas" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innerpartidas" runat="server" CssClass="ModalWindow">
            <asp:UpdatePanel ID="updtPatidas" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkGuardar" />
                    <asp:AsyncPostBackTrigger ControlID="grvDocumentos" />
                    <asp:PostBackTrigger ControlID="linkImprime" />
                    <asp:AsyncPostBackTrigger ControlID="drpConceptoConta" />
                </Triggers>
                <ContentTemplate>
                    <div class="GridViewHeaderStyle">
                        <table width="100%">
                            <tr>
                                <td></td>
                                <td align="right" valign="top">
                                    <asp:ImageButton ID="imgpartidas" runat="server" ImageUrl="~/Images/x_14x14.png"
                                        OnClick="imgpartidas_Click" Width="20px" Height="20px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="background-color: #f1f1f1">
                        <asp:Label ID="lblmsjerror" runat="server" CssClass="label" Style="margin-left: 13px;">
                        </asp:Label>
                        <fieldset>
                            <legend>Productos</legend>
                            <div id="ScrollDetalles" runat="server" class="GridScrollSales">
                                <table width="100%">
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="GrvDetalles" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                                PageSize="8" Width="100%"
                                                GridLines="None" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" ViewStateMode="Enabled">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Seleccionar">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll" Enabled="true" CssClass="CheckBox" OnCheckedChanged="AllChecks"
                                                                runat="server" AutoPostBack="true" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox CssClass="CheckBox" ID="chkPartida" runat="server" AutoPostBack="true"
                                                                OnCheckedChanged="chkPartidaChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Artículo" DataField="Des_Concepto" ItemStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="200" />
                                                    <asp:TemplateField HeaderText="Precio">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrecioConIEPS" runat="server" Style="text-align: right" Text='<%# Eval("PrecioConIEPS", "{0:c}")%>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrecio" runat="server" Visible="false" Style="text-align: right"
                                                                Text='<%# Eval("Precio", "{0:c}")%>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cant.original">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Cantoriginal" runat="server" Style="text-align: right" Text='<%# Bind("Cantidad") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cant.devuelta">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCantidad" MaxLength="10" Text='<%# Bind("Cantidad") %>' runat="server"
                                                                OnTextChanged="txtCantidad_TextChanged" Width="100px" AutoPostBack="true" CssClass="TextBoxEstilo"
                                                                Style="text-align: right">
                                                            </asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="cantidadfilter" runat="server" TargetControlID="txtCantidad"
                                                                FilterType="Custom, Numbers" ValidChars=",."></asp:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Motivo dev.">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="MotivoDropDownList" runat="server" Width="120px" CssClass="DropDown"
                                                                Font-Names="Arial" Font-Size="9px" Height="15px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCosto" runat="server" Text='<%# Bind("costo") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTProducto" runat="server" Text='<%# Bind("TProducto") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProducto" runat="server" Text='<%# Bind("Producto") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTConcepto" runat="server" Text='<%# Bind("TConcepto") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblConcepto" runat="server" Text='<%# Bind("Concepto") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnidad" runat="server" Text='<%# Bind("Unidad_Medida") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUbicacion" runat="server" Text='<%# Bind("Numero_Ubicacion") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFamilia" runat="server" Text='<%# Bind("Familia_Producto_Tipo_Concepto_Concepto") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSecuencia" runat="server" Text='<%# Bind("secuencia") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValorIva" runat="server" Text='<%# Bind("ValorIva") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                <FooterStyle CssClass="GridViewFooterStyle" />
                                                <HeaderStyle CssClass="GridViewHeaderStyle" />
                                                <PagerStyle CssClass="GridViewPagerStyle" />
                                                <RowStyle CssClass="GridViewRowStyle" />
                                                <SelectedRowStyle CssClass="GridViewRowStyle" />

                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </fieldset>
                        <br />
                        <fieldset class="fieldsetmdl2">
                            <legend>Punto de devolución</legend>
                            <table width="90%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSucursal" runat="server" Text="Sucursal" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpSucursal" runat="server" CssClass="DropDown">
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblAlmacen" runat="server" Text="Almacén entrada" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpAlmacen" runat="server" CssClass="DropDown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtObservaciones" runat="server" TextMode="multiline"
                                            Rows="3" Columns="30" CssClass="TextBoxEstilo"> </asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblConceptoConta" runat="server" Text="Concepto" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpConceptoConta" runat="server" CssClass="DropDown" AutoPostBack="true"
                                            OnSelectedIndexChanged="drpConceptoConta_Changed">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset class="fieldsetmdl2">
                            <legend>Método compensación</legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFormaPago" runat="server" Text="Forma de pago" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpFormaPago" runat="server" CssClass="DropDown" AutoPostBack="true"
                                            OnSelectedIndexChanged="drpFormaPago_Changed">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbltiTotal" runat="server" Text="Total $" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotal" runat="server" CssClass="TextBoxEstilo" Width="105px"
                                            DataFormatString="{0:c2}" Enabled="false" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTotal" runat="server" Text="" Visible="false">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblIva" runat="server" Text="" Visible="false">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSubtotal" runat="server" Text="" Visible="false">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblBanco" runat="server" Text="Banco" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpBanco" runat="server" CssClass="DropDown" AutoPostBack="true"
                                            OnSelectedIndexChanged="drpBanco_Changed">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCuenta" runat="server" Text="No. Cuenta" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpCuenta" runat="server" CssClass="DropDown" AutoPostBack="true"
                                            OnSelectedIndexChanged="drpCuenta_Changed">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFolioBanco" runat="server" Text="Folio" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFolioBanco" runat="server" CssClass="TextBoxEstilo" Width="105px"
                                            Enabled="false" MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblBeneficiario" runat="server" Text="Beneficiario" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtBeneficiario" runat="server" CssClass="TextBoxEstilo" Width="99%"
                                            AutoPostBack="true" OnTextChanged="txtBeneficiario_TextChanged"> </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="4">
                                        <asp:Label ID="LblNbreBeneficiario" runat="server" Text="" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbltipoDocumento" runat="server" Text="Documento" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drptipoDocumento" OnSelectedIndexChanged="drptipoDocumento_Changed"
                                            AutoPostBack="true" runat="server" CssClass="DropDown">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbldocSerie" runat="server" Text="Serie" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpdocSerie" runat="server" OnSelectedIndexChanged="drpdocSerie_Changed"
                                            AutoPostBack="true" CssClass="DropDown">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNumero" runat="server" Text="Folio" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNumero" runat="server" CssClass="TextBoxEstilo" Width="105px"
                                            MaxLength="10"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblConcepto" runat="server" Text="Concepto NC" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpConcepto" runat="server" CssClass="DropDown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                    <div class="modalfooter" style="overflow: hidden;">
                        <table width="100%">
                            <tr align="center">
                                <td align="center" width="100%">
                                    <asp:LinkButton Width="100%" ID="lnkGuardar" runat="server" OnClick="GeneraDevolucion_Click" ForeColor="White">Guardar 
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:AutoCompleteExtender ID="AutoExtBeneficiario" runat="server" ServicePath="~/ClienteFacturaWebService.asmx"
                        MinimumPrefixLength="1" ServiceMethod="GetClients" OnClientShown="ShowOptions"
                        EnableCaching="true" TargetControlID="txtBeneficiario" CompletionSetCount="10"
                        CompletionInterval="0" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        CompletionListItemCssClass="autocomplete_listItem">
                    </asp:AutoCompleteExtender>
                    <asp:HiddenField ID="HdnNumeroFactura" runat="server" />
                    <asp:HiddenField ID="HdnNumeroCotiza" runat="server" />
                    <asp:HiddenField ID="HdnNumero" runat="server" />
                    <asp:HiddenField ID="HdnProcesoOk" runat="server" />
                    <asp:HiddenField ID="HdnNumeroNota" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerTransaccion" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innerTransaccion" runat="server" CssClass="ModalWindow">
            <asp:UpdatePanel ID="updtTransaccion" runat="server" UpdateMode="Conditional">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="modalheader">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAvisoTransaccion" runat="server" Text="Aviso" ForeColor="White"
                                        Font-Size="Large" Font-Bold="true" Font-Names="Arial" />
                                </td>
                                <td align="right" valign="top">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                        Width="20px" Height="20px" OnClick="cierraModalTransaccion_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalbody">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMsjConcepto" runat="server" CssClass="label_title" Text="">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMsjContinua" runat="server" CssClass="label_title" Text="¿Desea continuar?">
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalfooter">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnkDevolucionSinTransaccion" CssClass="linkmodal" ForeColor="White"
                                        runat="server" OnClick="lnkDevolucionSinTransaccion_Click">Si 
                                    </asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkCancelacionSinTransaccion" CssClass="linkmodal" runat="server"
                                        ForeColor="White" OnClick="NoContinua_Click">No
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField" runat="server" />
    <asp:HiddenField ID="hdnTransaccion" runat="server" />
    <asp:ModalPopupExtender ID="mdlpartidas" runat="server" TargetControlID="HiddenField"
        PopupControlID="outerpartidas" BackgroundCssClass="ModalBackGround" PopupDragHandleControlID="drag">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="mdlTransaccion" runat="server" TargetControlID="hdnTransaccion"
        PopupControlID="outerTransaccion" BackgroundCssClass="ModalBackGround">
    </asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="rceDetalles" runat="server" TargetControlID="innerpartidas"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>
    <asp:RoundedCornersExtender ID="rceTransaccion" runat="server" TargetControlID="innerTransaccion"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>
</asp:Content>
