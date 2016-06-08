<%@ Page Title="" Language="C#" MasterPageFile="~/mpIntegraCompany.Master" AutoEventWireup="true" CodeBehind="FacturaLibreMorales.aspx.cs" Inherits="Integra_Develoment.Ventas.FacturaLibreMorales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHIntegraCompany" runat="server">

    <%--MENU--%>
    <div id="divMenuRight">
        <div id="divMenuRightItem_1" runat="server" class="linkMenu">
            &nbsp;
            <img src="../Images/ModIcoEvento.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="lnkNueva" Text="Nueva venta" OnClick="Nueva_Click" runat="server">
            </asp:LinkButton>
        </div>
        <div id="divMenuRightItem_2" runat="server" class="linkMenu">
            &nbsp;
            <img src="../Images/ModIcoEvento.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="LnkGenerar" runat="server" Text="Generar factura" OnClick="Generar_Click"
                TabIndex="16"><%--OnClientClick="getElement('divhidden').style.display='block';  refresh(1);"--%>
            </asp:LinkButton>
        </div>
        <div id="divMenuRightItem_3" runat="server" class="linkMenu">
            &nbsp;
            <img src="../Images/ModIcoEvento.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="lnkRegresar" runat="server" Text=" Regresar" TabIndex="16" OnClick="lnkRegresar_Click"><%--OnClientClick="getElement('divhidden').style.display='block';  refresh(1);"--%>
            </asp:LinkButton>
        </div>
        <div id="divhidden" style="display: none">
            <asp:LinkButton ID="LinkButton1" runat="server" Text="Procesando..." OnClientClick="this.innerHTML='Procesando...'; this.removeAttribute('href');"> 
            </asp:LinkButton>
        </div>
        <div id="div1" style="display: none">
            <asp:LinkButton ID="btnRefresh" runat="server" Text="Procesando..." OnClick="Refresh_Click"> 
            </asp:LinkButton>
        </div>
    </div>
    <%--FIN MENU --%>
    <fieldset class="fieldset">
        <legend>Factura libre</legend>
        <div id="divProceso" class="cCargando" style="visibility: hidden;">
            <div id="divProcesoMsg" class="cCargandoImg">
                <br />
                <asp:Image ID="imgload" runat="server" ImageUrl="../Images/Ventas/loading.gif" />
                <br />
            </div>
        </div>
        <asp:Label ID="errorLabel" runat="server" Text="" CssClass="label_title" Visible="false">
        </asp:Label>
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="customerlbl" runat="server" Text="Cliente" CssClass="label_title"
                        Style="color: #444444; padding-left: 8px;">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="customertxt" runat="server" CssClass="TextBoxEstilo" AutoPostBack="True"
                        TabIndex="1" Width="200px" OnTextChanged="CustomerChanged">
                    </asp:TextBox>
                </td>
                <td align="left">
                    <asp:LinkButton ID="lnkaddclient" runat="server" TabIndex="2" Text="Nuevo cliente"
                        AutoPostBack="True" OnClick="ShowAddClient_Click" />
                </td>
                <td align="right">
                    <asp:Button ID="btnPersonaMoral" CssClass="btnPersona" runat="server" Text="Sin retención" />
                    <asp:Button ID="btnPersonaFisica" CssClass="btnPersonaII" runat="server" Text="Con retención"
                        OnClick="btnPersonaFisica_Click" OnClientClick=" return confirm(' ¿ Emitir factura con retenciones (personas físicas) ? ');" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="detalleclientediv" class="collapsePanelHeader" runat="server">
            <table width="100%">
                <tr>
                    <td width="98%">
                        <asp:LinkButton ID="detallelnk" runat="server" CssClass="label_title" />
                    </td>
                    <td width="2%">
                        <asp:ImageButton ID="detalleImg" runat="server" Style="float: right;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="clientdatapnl" runat="server">
            <table width="80%">
                <tr>
                    <td width="55%">
                        <asp:Label ID="rfclbl" runat="server" CssClass="label">
                        </asp:Label>
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="lnkModificaCliente" runat="server" TabIndex="2" Text="Modificar cliente"
                            Visible="false" AutoPostBack="True" OnClick="ModificaClient_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="customernamelbl" runat="server" CssClass="label">
                        </asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="streetlbl" runat="server" CssClass="label">
                        </asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="neighborlbl" runat="server" CssClass="label">
                        </asp:Label>
                    </td>
                    <td></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:AutoCompleteExtender ID="AutoCompleteExtender22"
            runat="server"
            ServicePath="~/ClientAutocompleteWebService.asmx"
            MinimumPrefixLength="1"
            ServiceMethod="gfClientesObtenDetalles"
            EnableCaching="true"
            TargetControlID="customertxt"
            UseContextKey="true"
            CompletionSetCount="10"
            CompletionInterval="0"
            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
            CompletionListItemCssClass="autocomplete_listItem"
            OnClientItemSelected="GetKey">
        </asp:AutoCompleteExtender>

        <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="clientdatapnl"
            CollapsedSize="0" ExpandedSize="70" Collapsed="False" CollapseControlID="detalleclientediv"
            ExpandControlID="detalleclientediv" AutoCollapse="False" AutoExpand="False" ScrollContents="False"
            TextLabelID="detallelnk" CollapsedText="Mostrar Cliente..." ExpandedText="Cliente"
            ImageControlID="detalleImg" ExpandedImage="~/Images/move_vertical_9x24.png"
            CollapsedImage="~/Images/expand_blue.png"></asp:CollapsiblePanelExtender>
    </fieldset>
    <fieldset class="fieldset">
        <legend></legend>
        <div id="detallefacturadiv" class="collapsePanelHeader">
            <table width="100%">
                <tr>
                    <td width="98%">
                        <asp:LinkButton ID="detallefacturalnk" runat="server" CssClass="label_title" />
                    </td>
                    <td width="2%">
                        <asp:ImageButton ID="detallefacturaimg" runat="server" Style="float: right;" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="invoicedatapnl" runat="server" Style="padding-left: 2px;">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="typelbl" runat="server" Text="Forma de pago" CssClass="label">

                                </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlforpago" runat="server" Width="105px" AutoPostBack="True"
                                    CssClass="DropDown" TabIndex="3">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblmoneda" runat="server" Text="Moneda" CssClass="label">

                                </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlmoneda" runat="server" Width="105px" AutoPostBack="True"
                                    CssClass="DropDown" TabIndex="4" OnSelectedIndexChanged="monedaChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lbldatosIva" runat="server" Text="IVA" CssClass="label">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlIva" runat="server" AutoPostBack="True" CssClass="DropDown"
                                    OnSelectedIndexChanged="IVAChanged" Width="105px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblCondicionPago" runat="server" Text="Condición pago:" CssClass="label">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCondicionPago" runat="server" Width="150px" CssClass="TextBoxEstilo"
                                    MaxLength="100">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMpago" runat="server" Text="Método pago" CssClass="label">

                                </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlmetodo" runat="server" Width="105px" AutoPostBack="True"
                                    CssClass="DropDown" TabIndex="5">
                                </asp:DropDownList>
                            </td>
                            <td width="77px">
                                <asp:Label ID="lblNtC" runat="server" Text="No.Tarjeta/Cuenta" CssClass="label">

                                </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtTC" runat="server" Width="100px" CssClass="TextBoxEstilo" TabIndex="6"
                                    MaxLength="4">
                                </asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FtdTc" runat="server" TargetControlID="TxtTC" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                            </td>
                            <td width="77px">
                                <asp:Label ID="lblNumReferencia" runat="server" Text="No.Referencia" CssClass="label">

                                </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumReferencia" runat="server" Width="100px" CssClass="TextBoxEstilo"
                                    TabIndex="7" MaxLength="50">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="43px">
                                <asp:Label ID="lblDocumento" runat="server" Text="Documento" CssClass="label">

                                </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddldocto" runat="server" OnSelectedIndexChanged="doctodrpChanged"
                                    Width="105px" AutoPostBack="True" CssClass="DropDown" TabIndex="7">
                                </asp:DropDownList>
                            </td>
                            <td width="43px">
                                <asp:Label ID="lblSerieDocto" runat="server" Text="Serie" CssClass="label">

                                </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlserie" runat="server" OnSelectedIndexChanged="seriedrpChanged"
                                    Width="105px" AutoPostBack="True" CssClass="DropDown" TabIndex="8">
                                </asp:DropDownList>
                            </td>
                            <td width="43px">
                                <asp:Label ID="lblfolio" runat="server" Text="Folio" CssClass="label">

                                </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfolio" runat="server" Width="100px" CssClass="TextBoxEstilo" TabIndex="9"
                                    MaxLength="10">
                                </asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FtdFolio" runat="server" TargetControlID="txtfolio"
                                    FilterType="Numbers"></asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="invoicedatapnl"
                    CollapsedSize="0" ExpandedSize="100" Collapsed="False" CollapseControlID="detallefacturadiv"
                    ExpandControlID="detallefacturadiv" AutoCollapse="False" AutoExpand="False" ScrollContents="False"
                    CollapsedText="Mostrar datos factura..." TextLabelID="detallefacturalnk" ImageControlID="detallefacturaimg"
                    ExpandedImage="~/Images/move_vertical_9x24.png" CollapsedImage="~/Images/expand_blue.png"
                    ExpandedText="Datos factura"></asp:CollapsiblePanelExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdtDetallePartidas" runat="server" UpdateMode="Conditional">
            <Triggers>

                <asp:AsyncPostBackTrigger ControlID="invoiceGridView" />

            </Triggers>
            <ContentTemplate>
                <div id="detallepartidasdiv" class="collapsePanelHeader">
                    <table width="100%">
                        <tr>
                            <td width="98%">
                                <asp:LinkButton ID="partidasfacturalnk" runat="server" CssClass="label_title" />
                            </td>
                            <td width="2%">
                                <asp:ImageButton ID="partidasfacturaimg" runat="server" Style="float: right;" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="padding-left: 8px; padding-top: 4px; padding-bottom: 15px; height: 13px">
                    <table style="width: 150px">
                        <tr>
                            <td>
                                <asp:LinkButton ID="nuevaPartidaBtn" OnClick="nuevaPartida_Click" CssClass="btnagregarpartida"
                                    TabIndex="10" runat="server" Text=" +  Agregar concepto" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="partidaspnl" runat="server">
                    <div class="TablegridInvoice1">
                        <table id="TableTitles" runat="server" width="99%" style="padding-left: 2px;">
                            <tr class="GridViewHeaderStyle">
                                <td align="center" width="11%" class="label_title">Cantidad
                                </td>
                                <td align="center" width="40%" class="label_title">Producto
                                </td>
                                <td align="center" width="11%" class="label_title">Precio
                                </td>
                                <td align="center" width="12%" class="label_title">Unidad
                                </td>
                                <td align="center" width="12%" class="label_title">Total
                                </td>
                                <td align="center" width="13%" class="label_title">Acciones
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="ScrollProducts" runat="server" class="GridScrollInvoice2" onmouseover="parar=1"
                        onmouseout="parar=0">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="invoiceGridView" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                        GridLines="None" HorizontalAlign="Center" ShowHeader="False" OnRowCommand="invoiceGridView_RowCommand"
                                        Width="100%" CssClass="GridViewStyle" ViewStateMode="Enabled" OnSelectedIndexChanged="invoiceGridView_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField Visible="false" HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label Visible="false" ID="Numberlbl" runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label Visible="false" ID="lbltipo_producto" runat="server" Text='<%# Bind("TipoProducto") %>'>
               
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label Visible="false" ID="lblproducto" runat="server" Text='<%# Bind("Producto") %>'>
               
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label Visible="false" ID="lbltipo_concepto" runat="server" Text='<%# Bind("TipoConcepto") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label Visible="false" ID="lblconcepto" runat="server" Text='<%# Bind("Concepto") %>'>
               
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label Visible="false" ID="lblclas_um" runat="server" Text='<%# Bind("Clas_UM") %>'>
               
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label Visible="false" ID="lblFamilia" runat="server" Text='<%# Bind("Familia") %>'>
               
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="cantidadtxt" MaxLength="50" runat="server" Text='<%# Bind("Cantidad") %>'
                                                        CssClass="TextBoxEstilo" Width="97%" OnTextChanged="cantidadtxt_TextChanged" TabIndex="11"
                                                        AutoPostBack="True">
                 
                                                    </asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FtdCantidad" runat="server" TargetControlID="cantidadtxt"
                                                        FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="40%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="productotxt" MaxLength="100" OnTextChanged="productsTextBox_TextChanged"
                                                        runat="server" AutoCompleteType="none" AutoPostBack="True" CssClass="TextBoxEstilo"
                                                        TabIndex="12" Text='<%# Bind("Descripcion") %>' Width="97%">
                                                           
                                                    </asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="acextProductos" runat="server" ServicePath="~/ProductosWebService.asmx"
                                                        MinimumPrefixLength="1" ServiceMethod="GetProducts" EnableCaching="true" TargetControlID="productotxt"
                                                        UseContextKey="true" CompletionSetCount="10" CompletionInterval="0" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        CompletionListItemCssClass="autocomplete_listItem" OnClientItemSelected="GetKey">
                                                    </asp:AutoCompleteExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label Visible="false" ID="lblNumeroProducto" runat="server" Text='<%# Bind("NumeroProducto") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="preciotxt" TabIndex="13" MaxLength="20" runat="server" Text='<%# Bind("Precio") %>'
                                                        CssClass="TextBoxEstilo" AutoPostBack="True" Width="97%" OnTextChanged="preciotxt_TextChanged">
                                                    </asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FtdPrecio" runat="server" TargetControlID="preciotxt"
                                                        FilterType="Custom, Numbers" ValidChars=".,-"></asp:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <%-- <asp:TextBox ID="unidadtxt" MaxLength="50" runat="server" Text='<%# Bind("Unidad") %>'
                                                        TabIndex="14" CssClass="TextBoxEstilo" Width="97%">
                                                    </asp:TextBox>--%>
                                                    <asp:DropDownList ID="ddlunidad" runat="server" OnSelectedIndexChanged="ddlunidadChanged"
                                                        Width="105px" AutoPostBack="True" CssClass="DropDown" TabIndex="8">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotal" runat="server" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="99%"
                                                        Text='<%# Bind("Total") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowSelectButton="True" HeaderText="Partida" ItemStyle-Width="6%"
                                                SelectText="Eliminar" ButtonType="Image" SelectImageUrl="~/Images/x_14x14.png"></asp:CommandField>
                                            <asp:ButtonField CommandName="Show" Text="Características" ItemStyle-Width="6%" />
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCaracteristicas" Visible="false" runat="server" Text='<%# Bind("Caracteristicas") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPorcentajeIva" runat="server" Text='<%# Bind("PorcentajeIva") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIVAParametrizado" runat="server" Text='<%# Bind("IVAParametrizado") %>'>
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
                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />

                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtenderPartidas" runat="server"
                    TargetControlID="partidaspnl" CollapsedSize="0" ExpandedSize="500" Collapsed="False"
                    CollapseControlID="detallepartidasdiv" ExpandControlID="detallepartidasdiv" AutoCollapse="False"
                    AutoExpand="False" ScrollContents="False" CollapsedText="Mostrar conceptos..."
                    TextLabelID="partidasfacturalnk" ImageControlID="partidasfacturaimg" ExpandedImage="~/Images/move_vertical_9x24.png"
                    CollapsedImage="~/Images/expand_blue.png" ExpandedText="Conceptos"></asp:CollapsiblePanelExtender>
                <asp:HiddenField ID="hdnRow" runat="server" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:ModalPopupExtender ID="AddClientMdl" runat="server" TargetControlID="HiddenField1"
                    PopupControlID="outeraddclientpnl" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updtTotalesyObservaciones" runat="server" UpdateMode="Conditional">
            <Triggers>
                <%--  <asp:AsyncPostBackTrigger ControlID="nuevaPartidaBtn" EventName="Click" />--%>
                <asp:AsyncPostBackTrigger ControlID="ddlIva" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="invoiceGridView" />
                <%--       <asp:PostBackTrigger ControlID="LnkGenerar" />
                <asp:AsyncPostBackTrigger ControlID="lnkNueva" EventName="Click" />--%>
            </Triggers>
            <ContentTemplate>
                <%--   <div class="labelsInvoice">--%>
                <table>
                    <tr>
                        <td width="700px">
                            <table>
                                <tr>
                                    <td align="top">
                                        <asp:Label ID="lblObser" runat="server" Text="Observaciones:" CssClass="label_title"> </asp:Label>

                                        <p></p>
                                        <asp:TextBox ID="txtObservaciones" runat="server" TextMode="multiline" MaxLength="75"
                                            TabIndex="15" Style="border: 1px solid #8DB3E2;" Rows="3" Columns="40" CssClass="TextBoxEstilo"> </asp:TextBox>

                                    </td>
                                </tr>

                            </table>
                        </td>
                        <td width="300px">
                            <table>
                                <tr>
                                    <td width="20%">
                                        <asp:Label ID="Label4" runat="server" Text="Subtotal:" CssClass="label_title"> </asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblsigno" runat="server" Text="$" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="lblSubtotal" runat="server" Text="" CssClass="label_title"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>

                                    <td width="25%">
                                        <asp:Label ID="Label5" runat="server" Text="Iva:" CssClass="label_title"> </asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="$" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td width="25%">
                                        <asp:Label ID="lblIva" runat="server" Text="" CssClass="label_title"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%" class="style1">
                                        <asp:Label ID="Label6" runat="server" Text="Total:" CssClass="label_title"> </asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="$" CssClass="label_title">
                                        </asp:Label>
                                    </td>
                                    <td width="25%" class="style1">
                                        <asp:Label ID="lblTotal" runat="server" Text="" CssClass="label_title"> </asp:Label>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                </table>

                <%--</div>--%>
                <%--      <div class="observacionesInvoice">
     
                </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <asp:Panel ID="outeraddclientpnl" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="inneraddclientpnl" class="ModalWindowBlue" runat="server">
            <asp:UpdatePanel ID="UpdtPnl" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <%-- <asp:AsyncPostBackTrigger ControlID="lnkaddclient" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkModificaCliente" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkNueva" EventName="Click" />--%>
                </Triggers>
                <ContentTemplate>
                    <asp:Accordion ID="Accordion1" runat="Server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                        AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                        <Panes>
                            <asp:AccordionPane ID="AccordionPane1" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent" runat="server">
                                <Header>
                                    Datos generales</Header>
                                <Content>
                                    <table style="padding-left: 20px;" width="85%">
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="newpersonrdbt" runat="server" CssClass="RadioButton" RepeatColumns="2"
                                                    OnSelectedIndexChanged="NewPersonChanged" AutoPostBack="true" Style="padding-left: 0px; margin-left: 35px; padding-top: 0px; margin-top: 0px; margin-bottom: 0px;">
                                                    <asp:ListItem>Moral&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem>Fisica</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="nameval" runat="server" Text="*" CssClass="validation_label"> </asp:Label>
                                            </td>
                                            <td width="77px">
                                                <asp:Label ID="namelbl" runat="server" Text="" CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="nametxt" runat="server" AutoPostBack="true" CssClass="TextBoxEstilo"
                                                    MaxLength="100" Width="100px"> </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="fathernameval" runat="server" Text="*" CssClass="validation_label"> </asp:Label>
                                            </td>
                                            <td width="77px">
                                                <asp:Label Width="77px" ID="fathernamelbl" runat="server" Text="Apellido paterno"
                                                    CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtpaterno" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                                    MaxLength="100"> </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="maternoval" runat="server" Text="*" CssClass="validation_label"> </asp:Label>
                                            </td>
                                            <td width="77px">
                                                <asp:Label Width="77px" ID="maternolbl" runat="server" Text="Apellido materno" CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtmaterno" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                                    MaxLength="100"> </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="rfcval" runat="server" Text="*" CssClass="validation_label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="rfcaddlbl" runat="server" Text="RFC" CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtrfc" runat="server" CssClass="TextBoxEstilo" Width="100px" Style="text-transform: uppercase;"> 
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="cortoval" runat="server" Text="*" CssClass="validation_label"> </asp:Label>
                                            </td>
                                            <td width="77px">
                                                <asp:Label Width="77px" ID="cortolbl" runat="server" Text="Nombre corto" CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcorto" runat="server" CssClass="TextBoxEstilo" Width="100px" MaxLength="100"> </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="correoval" runat="server" Text="*" CssClass="validation_label"> </asp:Label>
                                            </td>
                                            <td width="77px">
                                                <asp:Label Width="77px" ID="correolbl" runat="server" Text="Correo electrónico" CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcorreo" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                                    MaxLength="100"> </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </Content>
                            </asp:AccordionPane>
                            <asp:AccordionPane ID="AccordionPane2" runat="server">
                                <Header>
                                    Datos domicilio</Header>
                                <Content>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="streetval" runat="server" Text="*" CssClass="validation_label"> </asp:Label>
                                            </td>
                                            <td width="77px">
                                                <asp:Label ID="callelbl" runat="server" Text="Calle y número" CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCalle" runat="server" CssClass="TextBoxEstilo" Width="100px" MaxLength="100"> </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblNumeroDomicilio" runat="server" Text="" CssClass="label" Visible="false"> </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="neighborval" runat="server" Text="*" CssClass="validation_label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="colonialbl" runat="server" Text="Colonia" CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtColonia" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                                    MaxLength="40"> </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="cpval" runat="server" Text="*" CssClass="validation_label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="cplbl" runat="server" Text="Código postal" CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCp" runat="server" CssClass="TextBoxEstilo" Width="100px" MaxLength="5"> </asp:TextBox><asp:FilteredTextBoxExtender
                                                    ID="cpfltr" runat="server" TargetControlID="txtCp" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="stateval" runat="server" Text="*" CssClass="validation_label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="statelbl" runat="server" Text="Estado" CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEstado" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                                    MaxLength="40"> </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="townval" runat="server" Text="*" CssClass="validation_label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="delegacionlbl" runat="server" Text="  Deleg/Munpio" CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDelegacion" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                                    MaxLength="50"> </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="paisval" runat="server" Text="*" CssClass="validation_label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="paislbl" runat="server" Text="País" CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpais" runat="server" Width="105px" OnSelectedIndexChanged="paisddlChanged"
                                                    CssClass="DropDown" AutoPostBack="True" DataTextField="Descripcion" DataValueField="Numero">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </Content>
                            </asp:AccordionPane>
                            <asp:AccordionPane ID="AccordionPane3" runat="server">
                                <Header>
                                    Datos pago</Header>
                                <Content>
                                    <table>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="pagolbl" runat="server" Text="Datos de pago" CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddldatospago" runat="server" OnSelectedIndexChanged="datospagoddlChanged"
                                                    Width="105px" CssClass="DropDown" AutoPostBack="True" DataTextField="Descripcion"
                                                    DataValueField="Numero">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="formapagolbl" runat="server" Text="Forma de pago" CssClass="label"> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlformapago" runat="server" Width="105px" CssClass="DropDown"
                                                    OnSelectedIndexChanged="formapagoddlChanged" AutoPostBack="True" DataTextField="Descripcion"
                                                    DataValueField="Numero">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </Content>
                            </asp:AccordionPane>
                        </Panes>
                    </asp:Accordion>
                    <table width="100%">
                        <tr>
                            <td colspan="3" valign="top">
                                <asp:Label ID="lblerrorvalidacion" runat="server" Font-Names="Arial" Font-Size="8px"
                                    Height="10px">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="NoLnk" CssClass="link" runat="server" OnClick="CancelAddClick">Cancelar
                                </asp:LinkButton>
                            </td>
                            <td width="70px"></td>
                            <td>
                                <asp:LinkButton ID="SiLnk" CssClass="link" runat="server" OnClick="AddClientClick">Guardar 
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hdnMovimiento" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outersalemsg" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innersalemsg" runat="server" CssClass="ModalWindowBlue">
            <asp:UpdatePanel ID="saleupdt" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="aceptlinkbnt" />
                    <asp:AsyncPostBackTrigger ControlID="lnkRealizarPago" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="accordionHeaderSelected">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="salemsglbl" runat="server" Text="Aviso" CssClass="label" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalbody">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="salelbl" runat="server" CssClass="label_title"
                                        Height="20px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table width="95%">
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkRealizarPago" CssClass="link" runat="server" Style="float: right;"
                                    Text="Si" OnClick="AbreModalPago_Click">
                                </asp:LinkButton>
                            </td>
                            <td width="60%"></td>
                            <td>
                                <asp:LinkButton ID="aceptlinkbnt" CssClass="link" runat="server" Style="float: right;"
                                    Width="100%" Text="No" OnClientClick="CerrarPopupSalesOk(this);" OnClick="PrintDocument">
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outercollet" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innercollet" runat="server" CssClass="ModalWindowPago">
            <asp:UpdatePanel ID="colletupdt" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkRealizarPago" EventName="Click" />
                    <asp:PostBackTrigger ControlID="lnkCancelPago" />
                    <asp:AsyncPostBackTrigger ControlID="grvCollet" />
                    <asp:AsyncPostBackTrigger ControlID="addchangelnk" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="accordionHeaderSelected">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblcolletheader" runat="server" Text="Formas de pago" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalbodypago">
                        <table style="margin-bottom: 2px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblCmessage" runat="server" CssClass="label">
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Pnlpayleft" runat="server" class="payleft">
                            <div id="colletpaydiv">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblClientepago" runat="server" Text="Cliente" CssClass="label">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtClientepago" runat="server" CssClass="TextBoxEstilo" AutoPostBack="True"
                                                Width="200px" Enabled="false">
                                            </asp:TextBox>
                                        </td>
                                        <td width="22px"></td>
                                        <td>
                                            <asp:Label ID="lblMonedapago" runat="server" Text="Moneda" CssClass="label">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonedapago" Enabled="false" runat="server" Width="100px" CssClass="TextBoxEstilo"
                                                Style="text-align: left; padding-right: 2px;">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td width="32px">
                                            <asp:Label ID="lblTipopago" runat="server" Text="Tipo" CssClass="label" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="doctopaydrp" Enabled="false" runat="server" Width="100px" CssClass="TextBoxEstilo"
                                                Style="text-align: left; padding-right: 2px;">
                                            </asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTiposerie" runat="server" Text="Serie" CssClass="label" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="seriepaydrp" runat="server" Enabled="false" Width="100px" CssClass="TextBoxEstilo"
                                                Style="text-align: left; padding-right: 2px;">
                                            </asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipodocto" runat="server" Text="Docto" CssClass="label" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="numdoctopaytxt" Enabled="false" runat="server" Width="100px" CssClass="TextBoxEstilo"
                                                Style="text-align: left; padding-right: 2px;">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="TablegridSalesPay">
                                <table id="Table1" runat="server" width="96%">
                                    <tr class="GridViewHeaderStyle" style="background-color: #8DB3E2;">
                                        <td align="center" width="15px" class="label_title">+
                                        </td>
                                        <td align="center" width="103px" class="label_title">Concepto
                                        </td>
                                        <td align="center" width="82px" class="label_title">Referencia
                                        </td>
                                        <td align="center" width="82px" class="label_title">Importe
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="colletdiv" class="GridScroll">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grvCollet" runat="server" CellPadding="4" CssClass="GridViewStyle"
                                                AutoGenerateColumns="false" ForeColor="#333333" GridLines="None" Font-Size="Small"
                                                EnableViewState="true" ShowHeader="false" DataKeyNames="Numero, Mas,Ingreso, Descripcion"
                                                Width="99%">
                                                <Columns>
                                                    <asp:TemplateField Visible="false" HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label Visible="false" ID="lblNumber" runat="server" Text='<%# Bind("Numero") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblAdd" runat="server" OnClick="AddOption" Text='<%# Bind("Mas") %>'>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Descripcion" ItemStyle-Width="140" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtReferencia" MaxLength="50" runat="server" Text='<%# Eval("Referencia") %>'
                                                                CssClass="TextBoxEstilo" Width="100px">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtIngreso" Text='<%# "$" + Eval("Ingreso") %>' runat="server" MaxLength="20"
                                                                onclick="redirFL(this)" onblur="redirbFL(this)" CssClass="TextBoxStyleImport"
                                                                Style="text-align: right;" Width="100px" AutoPostBack="true" OnTextChanged="ImporteTextChanged">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label Width="1px" ID="lblTipocambio" runat="server" CssClass="LabelStyleImport"
                                                                Text='<%# Bind("TC") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false" HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label Visible="false" ID="lblCurrency" runat="server" Text='<%# Bind("Clas_Moneda") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false" HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label Visible="false" ID="lblChange" runat="server" Text='<%# Bind("Cambio") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                <FooterStyle BackColor="#5D7B9D" CssClass="GridViewFooterStyle" />
                                                <HeaderStyle BackColor="#8DB3E2" CssClass="GridViewHeaderStyle" />
                                                <PagerStyle BackColor="#284775" CssClass="GridViewPagerStyle" />
                                                <RowStyle BackColor="#F7F6F3" CssClass="GridViewRowStyle" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Pnlpay" runat="server" class="payright" BackImageUrl="~/Images/Ventas/ticket.jpg"
                            Height="361px" Width="214px">
                            <div id="Div2" runat="server" class="GridScrollTkt" onmouseover="parar=1" onmouseout="parar=0">
                                <table align="center">
                                    <tr>
                                        <td width="100%">
                                            <asp:Label Width="100%" ID="lblUno" Height="5px" runat="server" Text="" CssClass="label"
                                                ItemStyle-HorizontalAlign="Center">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%">
                                            <asp:Label Width="100%" ID="lblEmpresapago" runat="server" Text="" CssClass="label"
                                                ItemStyle-HorizontalAlign="Center">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSucursalpago" runat="server" Text="" CssClass="label" align="center">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCallepago" runat="server" Text="" CssClass="label" align="center">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblColoniapago" runat="server" Text="" CssClass="label" align="center">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDelegacionpago" runat="server" Text="" CssClass="label" align="center">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCppago" runat="server" Text="" CssClass="label" align="center">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblAstericos" runat="server" Text="******************************"
                                                CssClass="label">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="productspayGvw" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                                GridLines="None" HorizontalAlign="Center" ShowHeader="false" Width="180px" CssClass="GridViewStyletkt"
                                                ViewStateMode="Enabled">
                                                <Columns>
                                                    <asp:BoundField DataField="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35" />
                                                    <asp:BoundField DataField="Descripcion_Producto" ItemStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="65" />
                                                    <asp:BoundField DataField="Total" DataFormatString="{0:c2}" ItemStyle-HorizontalAlign="Right" />
                                                </Columns>
                                                <AlternatingRowStyle BackColor="#fff38f" BorderStyle="None" BorderWidth="0px" Font-Names="Arial, Calibri, Tahoma"
                                                    Font-Size="XX-Small" />
                                                <PagerStyle BackColor="#fff38f" CssClass="GridViewPagerStyle" />
                                                <RowStyle BackColor="#fff38f" BorderStyle="None" BorderWidth="0px" Font-Names="Arial, Calibri, Tahoma"
                                                    Font-Size="XX-Small" />
                                                <SortedAscendingCellStyle BackColor="#fff38f" />
                                                <SortedAscendingHeaderStyle BackColor="#fff38f" />
                                                <SortedDescendingCellStyle BackColor="#fff38f" />
                                                <SortedDescendingHeaderStyle BackColor="#fff38f" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblAstericospago" runat="server" Text="******************************"
                                                CssClass="label">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td width="70px"></td>
                                        <td>
                                            <asp:Label ID="lblSubtotaltkt" runat="server" Text="Subtotal:" CssClass="label">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSpeso" runat="server" Text="$" CssClass="label">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSbttaltkt" runat="server" Text="" CssClass="label" ItemStyle-HorizontalAlign="Right">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="lblIvatkt" runat="server" Text="IVA:" CssClass="label">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSignoPeso" runat="server" Text="$" CssClass="label">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblIvatktd" runat="server" Text="" CssClass="label" ItemStyle-HorizontalAlign="Right">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="lblTotaltkt" runat="server" Text="Total:" CssClass="label">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSigPeso" runat="server" Text="$" CssClass="label">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotaltktd" runat="server" Text="" CssClass="label" ItemStyle-HorizontalAlign="Right">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl" runat="server" Text="******************************" CssClass="label">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTitulotot" runat="server" Text="Total pago:" CssClass="label">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotalpagotkt" runat="server" Text="" CssClass="label">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTitulocambio" runat="server" Text="Cambio:" CssClass="label">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblChangetkt" runat="server" Text="" CssClass="label">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30px"></td>
                                        <td>
                                            <asp:Label ID="lblTitulodesglose" runat="server" Text="Desglose forma de pago" CssClass="label">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grvDesglose" runat="server" CellPadding="4" AutoGenerateColumns="False"
                                                GridLines="None" HorizontalAlign="Center" ShowHeader="false" Width="180px" CssClass="GridViewStyletkt"
                                                ViewStateMode="Enabled">
                                                <Columns>
                                                    <asp:BoundField DataField="Descripcion" />
                                                    <asp:BoundField DataField="Ingreso" />
                                                </Columns>
                                                <AlternatingRowStyle BackColor="#fff38f" BorderStyle="None" BorderWidth="0px" Font-Names="Arial, Calibri, Tahoma"
                                                    Font-Size="XX-Small" />
                                                <PagerStyle BackColor="#fff38f" CssClass="GridViewPagerStyle" />
                                                <RowStyle BackColor="#fff38f" BorderStyle="None" BorderWidth="0px" Font-Names="Arial, Calibri, Tahoma"
                                                    Font-Size="XX-Small" />
                                                <SortedAscendingCellStyle BackColor="#fff38f" />
                                                <SortedAscendingHeaderStyle BackColor="#fff38f" />
                                                <SortedDescendingCellStyle BackColor="#fff38f" />
                                                <SortedDescendingHeaderStyle BackColor="#fff38f" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <br />
                        <table class="paytable">
                            <tr>
                                <td width="250px"></td>
                                <td>
                                    <asp:Label ID="LblImport" runat="server" Text="Total venta:" CssClass="label">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPayTotal" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                        Enabled="false" Style="text-align: right;">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="250px"></td>
                                <td>
                                    <asp:Label ID="lblTituloimportepago" runat="server" Text="Importe pago:" CssClass="label">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPagoImporte" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                        Enabled="false" Style="text-align: right;">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="250px"></td>
                                <td>
                                    <asp:Label ID="lblRestan" runat="server" Text="Restan:" CssClass="label">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRestan" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                        Enabled="false" Style="text-align: right;">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="250px"></td>
                                <td>
                                    <asp:Label ID="lblTitulocambiotkt" runat="server" Text="Cambio M.N.:" CssClass="label">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCambiopago" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                        Enabled="false" Style="text-align: right;">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkCancelPago" CssClass="link" Style="float: right;" runat="server"
                                    OnClick="PrintDocument" OnClientClick="CerrarCancelaPago(this);">Cancelar
                                </asp:LinkButton>
                            </td>
                            <td width="40%"></td>
                            <td>
                                <asp:LinkButton ID="LbtnPay" CssClass="link" Style="float: left;" runat="server"
                                    OnClick="PayClick">Aceptar
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerchange" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innerchange" runat="server" CssClass="ModalWindowPago">

            <asp:UpdatePanel ID="changeupdt" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="LbtnPay" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lnkRzaPagoSinTransaccion" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="accordionHeaderSelected">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="titlechangelbl" runat="server" Text="Cambio" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalbodypago">
                        <table style="margin-bottom: 2px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMensajeCambio" runat="server" CssClass="label">
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div id="changediv">
                            <asp:GridView ID="grvCambio" runat="server" CellPadding="4" CssClass="GridViewStyle"
                                Width="200px" AutoGenerateColumns="false" ForeColor="#333333" GridLines="None"
                                Font-Size="Small" EnableViewState="true" ShowHeader="true">
                                <Columns>
                                    <asp:TemplateField Visible="false" HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label Visible="false" ID="Numberlbl" runat="server" Text='<%# Bind("Numero") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label Visible="false" ID="lblClasMoneda" runat="server" Text='<%# Bind("Clas_Moneda") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTypechange" runat="server" CssClass="LabelStyleChange" Text='<%# Bind("TC") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Descripcion" HeaderText="Concepto" />
                                    <asp:TemplateField HeaderText="Importe">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtIngreso" Text='<%# "$" + Eval("Egreso") %>' runat="server" MaxLength="20"
                                                onclick="redircambioFL(this)" onblur="redirbcambioFL(this)" CssClass="TextBoxStyleChange"
                                                Style="text-align: right;" Width="100px" AutoPostBack="true" OnTextChanged="ImporteChangeTextChanged">
                                            </asp:TextBox>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <FooterStyle BackColor="#5D7B9D" CssClass="GridViewFooterStyle" />
                                <HeaderStyle BackColor="#8DB3E2" CssClass="GridViewHeaderStyle" />
                                <PagerStyle BackColor="#284775" CssClass="GridViewPagerStyle" />
                                <RowStyle BackColor="#F7F6F3" CssClass="GridViewRowStyle" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <table>
                                <tr>
                                    <td width="45px"></td>
                                    <td>
                                        <asp:Label ID="MontoCambiolbl" runat="server" Text="Cambio:" CssClass="label">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MontoCambiotxt" runat="server" CssClass="TextBoxEstilo" Enabled="false"
                                            Width="100px" Style="text-align: right;">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <table width="70%" style="padding-left: 25%;">
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkCancelaCambio" CssClass="link" runat="server" OnClick="lnkCancelaCambio_Click">Cancelar
                                </asp:LinkButton>
                            </td>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="addchangelnk" CssClass="link" runat="server" OnClick="addchangeClick">Aceptar 
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerpaid" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innerpaid" runat="server" CssClass="ModalWindowBlue">
            <asp:UpdatePanel ID="paidupdt" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="addchangelnk" EventName="Click" />
                    <asp:PostBackTrigger ControlID="paidlnkacept" />
                </Triggers>
                <ContentTemplate>
                    <div class="accordionHeaderSelected">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text="Aviso" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalbody">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblAvisopago" Text="El pago se ha realizado satisfactoriamente!!!"
                                        runat="server" CssClass="label_title" Height="20px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table width="100%">
                        <tr>
                            <td></td>
                            <td width="10%" align="center">
                                <asp:LinkButton ID="paidlnkacept" runat="server" CssClass="link" Text="Aceptar" OnClick="PrintDocument"
                                    OnClientClick="CerrarPopup(this);">
                                </asp:LinkButton>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerTransaccion" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innerTransaccion" runat="server" CssClass="ModalWindowPago">
            <asp:UpdatePanel ID="updtTransaccion" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="LbtnPay" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="accordionHeaderSelected">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAvisoTransaccion" runat="server" Text="Aviso" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalbodypago">
                        <table style="margin-bottom: 2px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMsjTransaccion" runat="server" CssClass="label_title">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMsjConcepto" runat="server" CssClass="label_title" Text="no tiene afectación contable definida">
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
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkRzaPagoSinTransaccion" CssClass="link" runat="server" OnClick="PagoSinTransaccionClick">Si 
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkNoRzaPagoSinTransaccion" CssClass="link" runat="server" OnClick="NoContinuaClick">No
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerTransaccionCambio" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innerTransaccionCambio" runat="server" CssClass="ModalWindowPago">
            <asp:UpdatePanel ID="UpdtTransaccionCambio" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <%--  <asp:AsyncPostBackTrigger ControlID="LbtnPay" EventName="Click" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="accordionHeaderSelected">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblAvisoTransaccionCambio" runat="server" Text="Aviso" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalbodypago">
                        <table style="margin-bottom: 2px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMsjTransaccionCambio" runat="server" CssClass="label_title">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMsjConceptoCambio" runat="server" CssClass="label_title" Text="no tiene afectación contable definida">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMsjContinuaCambio" runat="server" CssClass="label_title" Text="¿Desea continuar?">
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:LinkButton ID="LinkButton2" CssClass="link" runat="server" OnClick="CambioSinTransaccionClick">Si 
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="LinkButton3" CssClass="link" runat="server" OnClick="NoContinuaCambioClick">No
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerCaracteristicas" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innerCaracteristicas" runat="server" CssClass="ModalWindowBlue">
            <asp:UpdatePanel ID="UpdtCaracteristicas" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="invoiceGridView" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="invoiceGridView" />
                </Triggers>
                <ContentTemplate>
                    <div class="accordionHeaderSelected">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Características" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalbodypago">
                        <table style="margin-bottom: 2px;">
                            <tr>
                                <td>
                                    <td>
                                        <asp:TextBox ID="txtCaracteristicas" runat="server" TextMode="multiline" Rows="4"
                                            Columns="40" Style="border: 1px solid #8DB3E2;" CssClass="TextBoxEstilo"> </asp:TextBox>
                                    </td>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkAceptarCar" CssClass="link" Style="width: 50%; text-align: right;"
                                    OnClick="AgregaCaracteristica_Click" runat="server">Aceptar 
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkLimpiarCar" CssClass="link" Style="width: 50%; text-align: right;"
                                    runat="server" OnClick="lnkLimpiarCar_Click">Eliminar 
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkCancelaCar" CssClass="link" Style="width: 50%; text-align: right;"
                                    runat="server" OnClick="lnkCancelaCar_Click">Cerrar 
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:HiddenField ID="salemsghdn" runat="server" />
    <asp:HiddenField ID="hdncarateristicas" runat="server" />
    <asp:HiddenField ID="collethdn" runat="server" />
    <asp:HiddenField ID="changehdn" runat="server" />
    <asp:HiddenField ID="hdnTransaccion" runat="server" />
    <asp:HiddenField ID="hdnTransaccionCambio" runat="server" />
    <asp:HiddenField ID="hdnpaid" runat="server" />
    <asp:ModalPopupExtender ID="mdlcaractetisticas" runat="server" TargetControlID="hdncarateristicas"
        PopupControlID="outerCaracteristicas" BackgroundCssClass="modalBackground" />
    <asp:ModalPopupExtender ID="salemsgmdl" runat="server" TargetControlID="salemsghdn"
        PopupDragHandleControlID="quotedrag" PopupControlID="outersalemsg" BackgroundCssClass="modalBackground" />
    <asp:ModalPopupExtender ID="mdlPago" runat="server" TargetControlID="collethdn" PopupControlID="outercollet"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="mdlCambio" runat="server" TargetControlID="changehdn"
        PopupControlID="outerchange" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="mdlTransaccion" runat="server" TargetControlID="hdnTransaccion"
        PopupControlID="outerTransaccion" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="mdlTransaccionCambio" runat="server" TargetControlID="hdnTransaccionCambio"
        PopupControlID="outerTransaccionCambio" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="mdlPagoAviso" runat="server" TargetControlID="hdnpaid"
        PopupControlID="outerpaid" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>

    <asp:HiddenField ID="keycontainer" runat="server" />
    <asp:HiddenField ID="iClientNumber" runat="server" />
    <asp:HiddenField ID="iIDFactura" runat="server" />
    <asp:HiddenField ID="ImporteCambio" runat="server" />

    <div id="hidden" style="display: none;">
        <input name="container" id="container" runat="server" />
    </div>
</asp:Content>

