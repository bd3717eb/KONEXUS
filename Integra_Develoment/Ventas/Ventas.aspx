<%@ Page Title="" Language="C#" MasterPageFile="~/mpIntegraCompany.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="Integra_Develoment.Ventas.Ventas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHIntegraCompany" runat="server">
    <script src="../Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.cookies.2.2.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/scrollsaver.js" type="text/javascript"></script>
    <script src="../Scripts/jsVentas.js" type="text/javascript"></script>

    <div id="divMenuRightVentas">
        <%----------------------------------------------------------------------------%>
        <%--Eventos de pagina--%>
        <div id="divMenuRightItem_10" class="linkMenu">
            &nbsp;
            <img src="../Images/ModIcoEvento.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="linkLimpiar" runat="server" Text="Nueva venta" OnClick="linkLimpiar_Click"> </asp:LinkButton>
        </div>
        <div id="divMenuRightItem_2" class="linkMenu">
            &nbsp;
            <img src="../Images/ModIcoEvento.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="lnkBuscaVentas" runat="server" Text="Buscar ventas" OnClick="lnkBuscaVentas_Click"></asp:LinkButton>
        </div>
        <div id="divMenuRightItem_6" class="linkMenu">
            &nbsp;
            <img src="../Images/ModIcoEvento.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="AddClientsbtn" runat="server" Text="Alta de clientes" OnClick="AddClients"> </asp:LinkButton>
        </div>
        <%----------------------------------------------------------------------------%>
        <%--REGION DE MENUS --%>
        <%----------------------------------------------------------------------------%>
        <%----------------------------------------------------------------------------%>
        <%--FACTURACION--%>
        <div id="divMenuRightItem_3" class="linkMenu" onclick="lfToogle(this);">
            &nbsp;
            <img src="../Images/ModIcoBlue.png" alt="img" />
            &nbsp; FACTURACIÓN
        </div>
        <div class="divMenuRightItem_3" style="background-color: #f5f5f5; display: none;">
            &nbsp; &nbsp;
            <img src="../Images/AppIcoGreen.png" alt="img" />
            &nbsp; &nbsp;
            <asp:LinkButton ID="btnFacturaLibre" runat="server" Text="Factura libre" OnClick="FacturaLibreClick"> </asp:LinkButton>
        </div>
        <div class="divMenuRightItem_3" style="background-color: #f5f5f5; display: none;">
            &nbsp; &nbsp;
            <img src="../Images/AppIcoGreen.png" alt="img" />
            &nbsp; &nbsp;
            <asp:LinkButton ID="btnFacturaGobierno" runat="server" Text="Factura gobierno" OnClick="FacturaGobiernoClick"> </asp:LinkButton>
        </div>
        <div class="divMenuRightItem_3" style="background-color: #f5f5f5; display: none;">
            &nbsp; &nbsp;
            <img src="../Images/AppIcoGreen.png" alt="img" />
            &nbsp; &nbsp;
            <asp:LinkButton ID="reprintbtn" runat="server" Text="Reimpresión de facturas" OnClick="RePrintClick"> </asp:LinkButton>
        </div>
        <div class="divMenuRightItem_3" style="background-color: #f5f5f5; display: none;">
            &nbsp; &nbsp;
            <img src="../Images/AppIcoGreen.png" alt="img" />
            &nbsp; &nbsp;
            <asp:LinkButton ID="btnFacturaDocumentos" runat="server" OnClick="FacturaDocumentos_Click">Facturación documentos de ventas </asp:LinkButton>
        </div>
        <div class="divMenuRightItem_3" style="background-color: #f5f5f5; display: none;">
            &nbsp; &nbsp;
            <img src="../Images/AppIcoGreen.png" alt="img" />
            &nbsp; &nbsp;
            <asp:LinkButton ID="cancelinvoicebtn" runat="server" OnClick="cancelacionFacturas_Click">Cancelación de facturas</asp:LinkButton>
        </div>
        <%--VENTAS PROGRAMADAS--%>
        <div id="divMenuRightItem_4" class="linkMenu" onclick="lfToogle(this);">
            &nbsp;
            <img src="../Images/ModIcoBlue.png" alt="img" />
            &nbsp; FACTURACIÓN PROGRAMADA
        </div>
        <div class="divMenuRightItem_4" style="background-color: #f5f5f5; display: none;">
            &nbsp; &nbsp;
            <img src="../Images/AppIcoGreen.png" alt="img" />
            &nbsp; &nbsp;
            <asp:LinkButton ID="lnkFactProgramadas" runat="server" Text="Facturación programada"
                OnClick="FacturaProgramadaClick"> </asp:LinkButton>
        </div>
        <%-- DEVOLUCIONES --%>
        <div id="divMenuRightItem_5" onclick="lfToogle(this);" class="linkMenu">
            &nbsp;
            <img src="../Images/ModIcoBlue.png" alt="img" />
            &nbsp; DEVOLUCIONES
        </div>
        <div class="divMenuRightItem_5" style="background-color: #f5f5f5; display: none;">
            &nbsp;
            <img src="../Images/AppIcoGreen.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="lnkDevoluciones" runat="server" PostBackUrl="~/Ventas/ConsultaDevolucion.aspx"
                Text="Devoluciones "></asp:LinkButton>
        </div>

        <%-- ADMINISTRACIÓN --%>
        <div id="divMenuRightItem_7" onclick="lfToogle(this);" class="linkMenu">
            &nbsp;
            <img src="../Images/ModIcoBlue.png" alt="img" />
            &nbsp; ADMINISTRACIÓN
        </div>
        <div class="divMenuRightItem_7" style="background-color: #f5f5f5; display: none;">
            &nbsp;
            <img src="../Images/AppIcoGreen.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="lnkAdministracion" runat="server" OnClick="Mensaje_Click"
                Text="Mensajes "></asp:LinkButton>
        </div>


        <div class="downmenu" style="position: absolute; right: 0; margin-top: 2em;">
            <table>
                <tr>
                    <td>
                        <asp:DropDownList ID="drpIva" runat="server" AutoPostBack="True" CssClass="DropDown"
                            OnSelectedIndexChanged="IVAChanged" Width="105px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="drpMoneda" runat="server" AutoPostBack="True" CssClass="DropDown"
                            OnSelectedIndexChanged="CurrencyChanged" Width="105px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="drpFormadePago" runat="server" AutoPostBack="True" CssClass="DropDown"
                            Width="105px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkEntregaDirecta" runat="server" Text="Entrega directa" AutoPostBack="true"
                            CssClass="CheckBox" OnCheckedChanged="chkEntregaDirectaChanged" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="downmenulabels" style="right: 0; margin-top: 2em; margin-right: 120px;">
            <table class="labels" style="width: 155px;">
                <tr>
                    <td>
                        <asp:Label ID="lblPorcentajeIva" runat="server" Text="Porcentaje de IVA"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMoneda" runat="server" Text="Moneda"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFormaPago" runat="server" Text="Forma de pago"> </asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="downmenu" style="position: absolute; right: 0; margin-top: 9em;">
            <asp:UpdatePanel ID="updtTotales" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtSubtotal" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                    Enabled="false" TabIndex="30" Style="height: 1em; margin-bottom: -1em;">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtIva" runat="server" CssClass="TextBoxEstilo" Width="100px" Enabled="false"
                                    TabIndex="30" Style="height: 1em; margin-bottom: -1em;">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtTotal" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                    Enabled="false" TabIndex="30" Style="height: 1em;">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="downmenulabels" style="position: absolute; right: 0; margin-right: 120px; margin-top: 9.2em; margin-bottom: 3em;">
            <table class="labels" style="width: 155px;">
                <tr>
                    <td>
                        <asp:Label ID="lblSubtotal" runat="server" Text="Subtotal" CssClass="label_header" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIVA" runat="server" Text="IVA" CssClass="label_header" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTotal" runat="server" Text="Total" CssClass="label_header" />
                    </td>
                </tr>
            </table>
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
        <asp:Label ID="lblError" runat="server" Text="" CssClass="label_title" Visible="false">
        </asp:Label>
        <legend>Datos cliente</legend>
        <asp:HiddenField ID="HdnCliente" runat="server" />
        <asp:HiddenField ID="hdnRuta" runat="server" />
        <asp:HiddenField ID="keycontainer" runat="server" />
        <%--    <asp:UpdatePanel ID="updtValorincorrecto" runat="server" UpdateMode="Always">
     
            <ContentTemplate>
              
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        <div style="display: none">

            <asp:LinkButton ID="linkRefresh" runat="server" OnClick="linkRefresh_Click">test test test</asp:LinkButton>
            <asp:LinkButton ID="linkTerminaProceso" runat="server" OnClick="linkTerminaProceso_Click">test test test</asp:LinkButton>
            <asp:LinkButton ID="lnkRefresca" runat="server" OnClick="lnkRefresca_Click">test test test</asp:LinkButton>
        </div>
        <table>
            <tr>
                <td>
                    <%--<asp:Label ID="lblCliente" runat="server" Text="Cliente" CssClass="label_title">
                    </asp:Label>--%>
                </td>
                <td>
                    <asp:TextBox ID="txtCliente" runat="server" CssClass="TextBoxEstilo" AutoPostBack="True"
                        OnTextChanged="Cliente_Changed" Width="250px" Height="18px"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="ace_txtCliente" runat="server" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        CompletionListItemCssClass="autocomplete_listItem" Enabled="true" ServicePath="~/ClientAutocompleteWebService.asmx" MinimumPrefixLength="1" ServiceMethod="GetClients" EnableCaching="true" TargetControlID="txtCliente" UseContextKey="true" CompletionSetCount="10" CompletionInterval="0">
                    </asp:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lblNombrecliente" runat="server" CssClass="label">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lblRfc" runat="server" CssClass="label">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lblCalle" runat="server" CssClass="label">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lblColonia" runat="server" CssClass="label">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lblEstado" runat="server" CssClass="label">
                    </asp:Label>
                </td>
            </tr>
        </table>
        <%--<asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="~/ClientAutocompleteWebService.asmx"
            MinimumPrefixLength="1" ServiceMethod="GetClients" EnableCaching="true" TargetControlID="txtCliente"
            UseContextKey="true" CompletionSetCount="10" CompletionInterval="0" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
            CompletionListItemCssClass="autocomplete_listItem" OnClientItemSelected="GetKey">
        </asp:AutoCompleteExtender>--%>
    </fieldset>
    <fieldset class="fieldset">
        <legend>&nbsp;</legend>
        <asp:HiddenField ID="HdnEmpresa" runat="server" />
        <asp:HiddenField ID="HdnSucursal" runat="server" />
        <asp:HiddenField ID="HdnUsuario" runat="server" />
        <asp:HiddenField ID="collethdn" runat="server" />
        <asp:HiddenField ID="generalstorehdn" runat="server" />
        <asp:HiddenField ID="confirmhdn" runat="server" />
        <asp:HiddenField ID="invoicehdn" runat="server" />
        <asp:HiddenField ID="salemsghdn" runat="server" />
        <asp:HiddenField ID="printhdn" runat="server" />
        <asp:HiddenField ID="paidhdn" runat="server" />
        <asp:HiddenField ID="officehdn" runat="server" />
        <asp:HiddenField ID="hidden1" runat="server" />
        <asp:HiddenField ID="hidden3" runat="server" />
        <asp:HiddenField ID="hidden4" runat="server" />
        <asp:HiddenField ID="hidd4" runat="server" />
        <asp:HiddenField ID="Hidden6" runat="server" />
        <asp:HiddenField ID="hdnTransaccionCambio" runat="server" />
        <asp:HiddenField ID="hdnTransaccion" runat="server" />
        <asp:HiddenField ID="changehdn" runat="server" />
        <asp:HiddenField ID="iIDFactura" runat="server" />
        <asp:Label ID="lblPagoRealizado" runat="server" Text="" Visible="false">
        </asp:Label>
        <asp:TextBox ID="txtfolio" runat="server" Visible="false" EnableViewState="true"></asp:TextBox>
        <table>
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="Label13" runat="server" Text="Familias" CssClass="label_title">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="RdBtnListProductos" runat="server" AutoPostBack="true" CellSpacing="10"
                        RepeatColumns="2" CssClass="RadioButton" OnSelectedIndexChanged="RdBtnListProductos_SelectedIndexChanged">
                        <asp:ListItem>Productos</asp:ListItem>
                        <asp:ListItem>Servicios</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:AutoCompleteExtender ID="AcxProductos" runat="server" ServicePath="../GetProducts.asmx"
                        ServiceMethod="GetProduct" MinimumPrefixLength="1" EnableCaching="true" TargetControlID="txtProductos"
                        UseContextKey="true" CompletionSetCount="10" CompletionInterval="0" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        CompletionListItemCssClass="autocomplete_listItem" OnClientItemSelected="GetKey">
                    </asp:AutoCompleteExtender>
                    <asp:TextBox ID="txtProductos" runat="server" AutoPostBack="True" OnTextChanged="txtProductos_TextChanged"
                        ToolTip="Ingresar el producto deseado" CssClass="TextBoxEstilo" Width="150px">
                    </asp:TextBox>
                </td>
                <td width="10%"></td>
                <td>
                    <asp:Label ID="lblCodigoBarras" runat="server" Text="Producto" CssClass="label_title">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodigoBarras" runat="server" AutoPostBack="true" OnTextChanged="txtCodigoBarras_TextChanged"
                        ToolTip="Ingresar el producto deseado" CssClass="TextBoxEstilo" Width="150px">
                    </asp:TextBox>
                    <asp:AutoCompleteExtender ID="AxcCodigoBarras" runat="server" ServicePath="../GetProducts.asmx"
                        ServiceMethod="GetConceptosPorProducto" MinimumPrefixLength="1" EnableCaching="true"
                        TargetControlID="txtCodigoBarras" UseContextKey="true" CompletionSetCount="10"
                        CompletionInterval="0" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        CompletionListItemCssClass="autocomplete_listItem" OnClientItemSelected="GetKey">
                    </asp:AutoCompleteExtender>
                </td>
            </tr>
        </table>
        <table id="TablaTitulos" runat="server" width="97%">
            <tr class="GridViewHeaderStyle" style="background-color: #8DB3E2;">
                <td align="center" width="5%" class="RadioButton">Partida
                </td>
                <td align="center" width="9%" class="RadioButton">Disponibles
                </td>
                <td align="center" width="17%" class="RadioButton">Almacén
                </td>
                <td align="center" width="35%px" class="RadioButton">Producto
                </td>
                <td align="center" width="65px" class="RadioButton">Cantidad
                </td>
                <td align="center" width="65px" class="RadioButton">Precio
                </td>
                <td align="center" width="50px" class="RadioButton">Unidad
                </td>
                <td align="center" width="65px" class="RadioButton">Total
                </td>
            </tr>
        </table>
        <table class="TablegridSales" width="98%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="updtPartidas" runat="server" UpdateMode="Always">

                        <ContentTemplate>
                            <asp:GridView ID="grvPartidas" runat="server" CellPadding="0" AutoGenerateColumns="False"
                                GridLines="None" ShowHeader="false" Width="99%" CssClass="GridViewStyle" OnSelectedIndexChanged="grvPartidas_SelectedIndexChanged"
                                ViewStateMode="Enabled">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" HeaderText="Partida" ItemStyle-Width="38"
                                        SelectText="Eliminar" ButtonType="Image" SelectImageUrl="~/Images/Ventas/eliminar2.png" />
                                    <asp:BoundField HeaderText="Disponibles" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="71" />
                                    <asp:TemplateField HeaderText="Almacén" ItemStyle-Width="129" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="storeDropDownList" runat="server" Width="89%" CssClass="DropDown"
                                                AutoPostBack="true" Font-Names="Arial" Font-Size="9px" Height="15px" OnSelectedIndexChanged="storeDropDownList_SelectedIndexChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Descripcion_Producto" ItemStyle-Width="266" />
                                    <asp:TemplateField ItemStyle-Width="71">
                                        <ItemTemplate>
                                            <asp:TextBox ID="cantidadtxt" MaxLength="50" runat="server" Text='<%# Bind("Cantidad") %>'
                                                CssClass="TextBoxStyle" OnTextChanged="cantidadtxt_TextChanged" AutoPostBack="True">
                 
                                            </asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FtdCantidad" runat="server" TargetControlID="cantidadtxt"
                                                FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Precio" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:c2}"
                                        ItemStyle-Width="72" />
                                    <asp:BoundField DataField="Descripcion_Unidad" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="58" />
                                    <asp:BoundField DataField="Total" DataFormatString="{0:c2}" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="65" />
                                    <%--   <asp:BoundField DataField="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="71"  ReadOnly="false" />--%>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <FooterStyle CssClass="GridViewFooterStyle" BackColor="#5D7B9D" Font-Bold="True"
                                    ForeColor="White" />
                                <HeaderStyle BackColor="#8DB3E2" CssClass="GridViewHeaderStyle" />
                                <PagerStyle BackColor="#284775" CssClass="GridViewPagerStyle" />
                                <RowStyle BackColor="#F7F6F3" CssClass="GridViewRowStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" BackColor="#E2DED6" Font-Bold="True"
                                    ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <asp:HiddenField ID="HdnValorIncorrecto" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:LinkButton ID="lnkGuardarCotizacion" runat="server" Text="Guardar cotización"
                        CssClass="link" OnClick="GuardarCotizacion_Click">                                
                    </asp:LinkButton>
                </td>
                <td width="145px"></td>
                <td>
                    <asp:LinkButton ID="lnkCerrarVenta" runat="server" Text="Cerrar venta" CssClass="link"
                        OnClick="CerrarVenta_Click">                                
                    </asp:LinkButton>
                </td>
                <td width="145px"></td>
                <td>
                    <asp:LinkButton ID="lnkImprimirDocumento" runat="server" CssClass="link" OnClick="PrintDocument"
                        Style="display: none;">
                    </asp:LinkButton>
                </td>
                <td width="145px"></td>
                <td>
                    <asp:LinkButton ID="lnkPagarVenta" runat="server" Text="Pagar venta" CssClass="link"
                        OnClick="lnkPagarVentaClick">
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:Panel ID="outerproducts" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="productsPanel" runat="server" DefaultButton="lnkAgregaProductos" CssClass="ModalWindow">
            <asp:UpdatePanel ID="updProductos" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <%-- <asp:AsyncPostBackTrigger ControlID="txtProductos" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="RdBtnListProductos" EventName="SelectedIndexChanged" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="modalheader">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblProductosEncabezado" runat="server" Text="Agregar productos" ForeColor="White"
                                        Font-Size="Large" Font-Bold="true" Font-Names="Arial" />
                                </td>
                                <td align="right" valign="top">
                                    <asp:ImageButton ID="productsimgbtn" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                        Width="20px" Height="20px" OnClick="productsimgbtn_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="TablegridSalesProducts">
                        <table id="TablaTituloProductos" runat="server" width="96%">
                            <tr class="GridViewHeaderStyle" style="background-color: #8DB3E2;">
                                <td align="center" width="60px" class="RadioButton">Cantidad
                                </td>
                                <td align="center" width="190px" class="RadioButton">Producto
                                </td>
                                <td align="center" width="60px" class="RadioButton">Precio
                                </td>
                                <td align="center" width="53px" class="RadioButton">Unidad
                                </td>
                                <td align="center" width="80px" class="RadioButton">Disponibles
                                </td>
                                <td align="center" width="135px" class="RadioButton">Almacén
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalbodyadd">
                        <div id="scrollagregar" class="GridScroll">
                            <asp:GridView ID="GrvProductos" runat="server" CellPadding="4" CssClass="GridViewStyle"
                                Width="99%" AutoGenerateColumns="false" ForeColor="#333333" GridLines="None"
                                Font-Size="Small" EnableViewState="true" ShowHeader="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Cantidad">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="test" runat="server">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="checkTextBox" EventName="TextChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:TextBox ID="checkTextBox" MaxLength="6" TabIndex="1" runat="server" AutoPostBack="true"
                                                        CssClass="TextBoxEstilo" Width="50px" AutoCompleteType="Disabled" />
                                                    <asp:FilteredTextBoxExtender ID="FtdTxTExtender1" runat="server" TargetControlID="checkTextBox"
                                                        FilterType="Custom, Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Descripcion_Producto" ItemStyle-Width="175px" HeaderText="Descripción" />
                                    <asp:BoundField DataField="Precio" ItemStyle-Width="55px" />
                                    <asp:BoundField DataField="Descripcion_Unidad" HeaderText="Unidad" ItemStyle-Width="55px" />
                                    <asp:BoundField HeaderText="Disponibles" ItemStyle-Width="65px" />
                                    <asp:TemplateField HeaderText="Almacén" ItemStyle-Width="120">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="storeDropDownList" runat="server" Width="120px" CssClass="DropDown"
                                                AutoPostBack="true" Font-Names="Arial" Font-Size="9px" Height="15px" OnSelectedIndexChanged="storeDropDownListProducts_SelectedIndexChanged" />
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
                        </div>
                    </div>
                    <div class="modalfooter" style="overflow: hidden;">
                        <asp:LinkButton Width="100%" ID="lnkAgregaProductos" ForeColor="White" CssClass="linkmodal"
                            runat="server" OnClick="lnkAgregaProductos_Click">Agregar </asp:LinkButton>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerprint" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="printpnl" runat="server" CssClass="ModalWindow">
            <div class="modalheader">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="printhdrlbl" runat="server" Text="Aviso" ForeColor="White" Font-Size="Large"
                                Font-Bold="true" Font-Names="Arial" />
                        </td>
                        <td align="right" valign="top">
                            <asp:ImageButton ID="printhdrimgbtn" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                Width="20px" Height="20px" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modalbody">
                <table>
                    <tr>
                        <td valign="top">
                            <asp:Label ID="lblMensajeDoctos" runat="server" CssClass="label_title" Height="20px"
                                Text="Documentos generados satisfatoriamente">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblNumeroVenta" runat="server" CssClass="label_title" Height="20px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modalfooter">
                <asp:LinkButton ID="okprintbtnl" runat="server" ForeColor="White" CssClass="linkmodal"
                    Text="Aceptar" OnClick="aceptclickok">
                  
                </asp:LinkButton>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerinvoice" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innerinvoice" runat="server" CssClass="ModalWindow">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="modalheader">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="invoicehdrlbl" runat="server" Text="Comprobante de venta" ForeColor="White"
                                        Font-Size="Large" Font-Bold="true" Font-Names="Arial" />
                                </td>
                                <td align="right" valign="top">
                                    <asp:ImageButton ID="invoicehdrimgbtn" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                        Width="20px" Height="20px" OnClick="CloseInvoice" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalbody">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblDocumento" runat="server" Text="Tipo" CssClass="label" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpDocumento" runat="server" AutoPostBack="True" CssClass="DropDown"
                                        OnSelectedIndexChanged="doctodrpChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSerie" runat="server" Text="Serie" CssClass="label" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpSerie" runat="server" AutoPostBack="True" CssClass="DropDown"
                                        OnSelectedIndexChanged="seriedrpChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNumeroDocto" runat="server" Text="Núm. Docto" CssClass="label" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNumeroDocto" MaxLength="20" runat="server" Width="100px" CssClass="TextBoxEstilo"
                                        Style="text-align: right; padding-right: 2px;"> </asp:TextBox>
                                </td>
                                <asp:FilteredTextBoxExtender ID="FtdTxTExdr" runat="server" TargetControlID="txtNumeroDocto"
                                    FilterType="Numbers"></asp:FilteredTextBoxExtender>
                            </tr>
                        </table>
                    </div>
                    <div class="modalfooter">
                        <asp:LinkButton ID="invoicebtnl" runat="server" ForeColor="White" OnClick="creaFactura_Click"
                            CssClass="linkmodal" Text="Aceptar"> </asp:LinkButton>
                    </div>
                    <asp:HiddenField ID="iNumeroVenta" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outersalemsg" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innersalemsg" runat="server" CssClass="ModalWindow">
            <div class="modalheader">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblAviso" runat="server" Text="Aviso" ForeColor="White" Font-Size="Large"
                                Font-Bold="true" Font-Names="Arial" />
                        </td>
                        <td align="right" valign="top">
                            <asp:ImageButton ID="salemsgimgbtn" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                Width="20px" Height="20px" OnClick="AceptClick" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modalbody">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblVenta" runat="server" CssClass="label_title" Height="20px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modalfooter">
                <asp:LinkButton ID="aceptlinkbnt" runat="server" OnClick="PrintDocument" OnClientClick="CerrarPopupSalesOk(this);"
                    CssClass="linkmodal" Text="Aceptar">
                </asp:LinkButton>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerselectclient" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="selectclient" class="ModalWindow" runat="server">
            <div class="modalheader">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="messageselect" runat="server" Text="Seleccionar Cliente" ForeColor="White"
                                Font-Size="Large" Font-Bold="true" Font-Names="Arial" />
                        </td>
                        <td align="right" valign="top">
                            <asp:ImageButton ID="selectimg" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                Width="20px" Height="20px" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modalbody">
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="grvClientes" CssClass="GridViewStyle" runat="server" CellPadding="4"
                                ShowHeader="false" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="grvClientes_SelectedIndexChanged"
                                AutoGenerateColumns="false" OnRowDataBound="grvClientes_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="../Images/GridImages/select.png" />
                                    <asp:BoundField DataField="Numero" HeaderText="Número" />
                                    <asp:BoundField DataField="Nombre_Completo" HeaderText="Nombre" />
                                    <asp:BoundField DataField="RFC" HeaderText="RFC" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle CssClass="GridViewFooterStyle" BackColor="#5D7B9D" Font-Bold="True"
                                    ForeColor="White" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" BackColor="#5D7B9D" Font-Bold="True"
                                    ForeColor="White" />
                                <PagerStyle CssClass="GridViewPagerStyle" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle CssClass="GridViewRowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" BackColor="#E2DED6" Font-Bold="True"
                                    ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="acceptButton" runat="server" />
            <asp:ModalPopupExtender ID="ModalPopupExtender5" runat="server" TargetControlID="acceptButton"
                PopupControlID="outerselectclient" PopupDragHandleControlID="selectclientdrag"
                BackgroundCssClass="ModalBackGround">
            </asp:ModalPopupExtender>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outermessage" runat="server" Style="display: none; background-color: transparent; border: 0px; border-color: transparent;">
        <asp:Panel ID="innermessage" runat="server" CssClass="ModalWindow">
            <div class="modalheader">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="headerlbl" runat="server" Text="Aviso" ForeColor="White" Font-Size="Large"
                                Font-Bold="true" Font-Names="Arial" />
                        </td>
                        <td align="right" valign="top">
                            <asp:ImageButton ID="closeimgbtn" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                Width="20px" Height="20px" OnClick="closeimgbtn_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkAgregaProductos" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modalbody">
                        <table>
                            <tr>
                                <td valign="middle">
                                    <asp:Label ID="messagelbl" runat="server" CssClass="label_title" Text="">
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="modalfooter">
                <asp:LinkButton ID="LinkButton3" align="center" Width="95%" CssClass="linkmodal"
                    runat="server" OnClick="Acept_Click">Aceptar
                </asp:LinkButton>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerpaid" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innerpaid" runat="server" CssClass="ModalWindow">
            <div class="modalheader">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Aviso" ForeColor="White" Font-Size="Large"
                                Font-Bold="true" Font-Names="Arial" />
                        </td>
                        <td align="right" valign="top">
                            <asp:ImageButton ID="paidimgclose" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                Width="20px" Height="20px" OnClick="PrintDocument" OnClientClick="CerrarPopup(this);" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modalbody">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="paidlbl" Text="" runat="server" CssClass="label_title" Height="20px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modalfooter">
                <asp:LinkButton ID="paidlnkacept" runat="server" CssClass="linkmodal" Text="Aceptar"
                    OnClick="PrintDocument" OnClientClick="CerrarPopup(this);">
                </asp:LinkButton>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outercharacteristics" runat="server" Style="display: none; background-color: transparent; border: 0px; border-color: transparent;">
        <asp:Panel ID="innercharacteristics" runat="server" CssClass="ModalWindow">
            <div class="modalheader">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="chartitlelbl" runat="server" Text="Características del producto" ForeColor="White"
                                Font-Size="Large" Font-Bold="true" Font-Names="Arial" />
                        </td>
                        <td align="right" valign="top">
                            <asp:ImageButton ID="charimgbtn" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                Width="20px" Height="20px" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modalbody">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="charlbl" runat="server" Text="" CssClass="label_title">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="chargrv" runat="server" CellPadding="4" CssClass="GridViewStyle"
                                AutoGenerateColumns="false" ForeColor="#333333" GridLines="None" Font-Size="Small"
                                EnableViewState="true" ShowHeader="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Cantidad">
                                        <ItemTemplate>
                                            <asp:TextBox ID="charTextBox" runat="server" CssClass="TextBoxEstilo" Width="40px"
                                                AutoCompleteType="Disabled" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="charconcept" />
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
            <div class="modalfooter">
                <asp:LinkButton ID="charbtn" runat="server" CssClass="linkmodal" Text="Aceptar" OnClick="Caracteristicas_Click">
                </asp:LinkButton>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outercollet" runat="server" Style="display: none; background-color: #00709e; border-width: 0px; border-color: #00709e; border-width: 2px; border-style: solid;">
        <asp:Panel ID="innercollet" runat="server" CssClass="ModalWindow">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="modalheader">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="colletheaderlbl" runat="server" Text="Formas de pago" ForeColor="White"
                                        Font-Size="Large" Font-Bold="true" Font-Names="Arial" />
                                </td>
                                <td align="right" valign="top">
                                    <asp:ImageButton ID="colletimgbtn" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                        Width="20px" Height="20px" OnClick="PrintDocument" OnClientClick="CerrarPopupFormaPago(this);" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalbody">
                        <table style="margin-bottom: 2px;">
                            <tr>
                                <td>
                                    <asp:Label ID="cmessagelbl" runat="server" CssClass="label"> </asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Pnlpayleft" runat="server" class="payleft">
                            <div id="colletpaydiv">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Cliente" CssClass="label"> </asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="clientpaytxt" runat="server" CssClass="TextBoxEstilo" AutoPostBack="True"
                                                Width="200px" Enabled="false"> </asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="Moneda" CssClass="label"> </asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="currencypaytxt" Enabled="false" runat="server" Width="100px" CssClass="TextBoxEstilo"
                                                Style="text-align: left; padding-right: 2px;"> </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="32px">
                                            <asp:Label ID="Label5" runat="server" Text="Tipo" CssClass="label" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="doctopaydrp" Enabled="false" runat="server" Width="100px" CssClass="TextBoxEstilo"
                                                Style="text-align: left; padding-right: 2px;"> </asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="Serie" CssClass="label" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="seriepaydrp" runat="server" Enabled="false" Width="100px" CssClass="TextBoxEstilo"
                                                Style="text-align: left; padding-right: 2px;"> </asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Docto" CssClass="label" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="numdoctopaytxt" Enabled="false" runat="server" Width="100px" CssClass="TextBoxEstilo"
                                                Style="text-align: left; padding-right: 2px;"> </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="TablegridSalesPay">
                                <table id="Table1" runat="server" width="96%">
                                    <tr class="GridViewHeaderStyle" style="background-color: #8DB3E2;">
                                        <td align="center" width="15px" class="RadioButton">+
                                        </td>
                                        <td align="center" width="103px" class="RadioButton">Concepto
                                        </td>
                                        <td align="center" width="82px" class="RadioButton">Referencia
                                        </td>
                                        <td align="center" width="82px" class="RadioButton">Importe
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="pagodiv" class="GridScroll">
                                <table width="100%" style="padding-left: 5px; padding-right: 5px;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grvFormaPago" runat="server" CellPadding="4" CssClass="GridViewStyle"
                                                AutoGenerateColumns="false" ForeColor="#333333" GridLines="None" Font-Size="Small"
                                                EnableViewState="true" ShowHeader="false" DataKeyNames="Numero, Mas,Ingreso, Descripcion"
                                                Width="99%">
                                                <Columns>
                                                    <asp:TemplateField Visible="false" HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label Visible="false" ID="lblNumber" runat="server" Text='<%# Bind("Numero") %>'> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblAdd" runat="server" OnClick="AgregaOpcionPago_Click" Text='<%# Bind("Mas") %>'> </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Descripcion" ItemStyle-Width="150" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtReferencia" MaxLength="50" runat="server" Text='<%# Eval("Referencia") %>'
                                                                CssClass="TextBoxEstilo" Width="100px"> </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtIngreso" Text='<%# "$" + Eval("Ingreso") %>' runat="server" MaxLength="20"
                                                                onclick="redir(this)" onblur="redirb(this)" CssClass="TextBoxStyleImport" Style="text-align: right;"
                                                                Width="100px" AutoPostBack="true" class="ingreso" OnTextChanged="ImporteTextChanged"> </asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FtdTxTExtimporte" runat="server" TargetControlID="txtIngreso"
                                                                FilterType="Custom, Numbers" ValidChars=".,$"></asp:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipocambio" runat="server" Width="0px" CssClass="LabelStyleImport"
                                                                Text='<%# Bind("TC") %>'> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" ItemStyle-Width="0">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMoneda" runat="server" Width="0px" CssClass="LabelStyleMoneda"
                                                                Text='<%# Bind("Clas_Moneda") %>'> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false" HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label Visible="false" ID="lblChange" runat="server" Text='<%# Bind("Cambio") %>'> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                <FooterStyle BackColor="#5D7B9D" CssClass="GridViewFooterStyle" />
                                                <HeaderStyle BackColor="#69bbef" CssClass="GridViewHeaderStyle" />
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
                        <asp:Panel ID="Pnlpay" runat="server" BackImageUrl="~/Images/Ventas/ticket.jpg" Height="361px"
                            Width="214px" class="payright">
                            <div id="Div2" runat="server" class="GridScrollTkt" onmouseover="parar=1" onmouseout="parar=0">
                                <table align="center">
                                    <tr>
                                        <td width="100%">
                                            <asp:Label Width="100%" ID="companypaylbl" runat="server" Text="" CssClass="labelTicket"
                                                ItemStyle-HorizontalAlign="Center"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="officepaylbl" runat="server" Text="" CssClass="labelTicket" align="center"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="addresdpaylbl" runat="server" Text="" CssClass="labelTicket" align="center"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="coloniapaylbl" runat="server" Text="" CssClass="labelTicket" align="center"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="delegacionpaylbl" runat="server" Text="" CssClass="labelTicket" align="center"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="cppaylbl" runat="server" Text="" CssClass="labelTicket" align="center"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="**************************" CssClass="labelAsterisco"> </asp:Label>
                                        </td>
                                    </tr>
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
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="**************************" CssClass="labelAsterisco"> </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table style="margin-left: 30%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="subtotaltktlbl" runat="server" Text="Subtotal:" CssClass="labelTicket"> </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="sbttaltktlbl" runat="server" Text="" CssClass="labelTicket" ItemStyle-HorizontalAlign="Left"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="ivatktlbl" runat="server" Text="IVA:" CssClass="labelTicket"> </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="ivatktdlbl" runat="server" Text="" CssClass="labelTicket" ItemStyle-HorizontalAlign="Left"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="totaltktlbl" runat="server" Text="Total" CssClass="labelTicket"> </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="totaltktdlbl" runat="server" Text="" CssClass="labelTicket" ItemStyle-HorizontalAlign="Left"> </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Lbl" runat="server" Text="**************************" CssClass="labelAsterisco"> </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="Total pago:" CssClass="labelTicket"
                                                Width="20px"> </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotalpagotkt" runat="server" Text="" CssClass="labelTicket" Width="20px"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDCambiotkt" runat="server" Text="Cambio:" CssClass="labelTicket"> </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCambiotkt" runat="server" Text="" CssClass="labelTicket"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text="Desglose forma de pago" CssClass="labelTicket"> </asp:Label>
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
                                                <RowStyle BackColor="#fff38f" BorderStyle="None" BorderWidth="0px" CssClass="labelTicket" />
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
                        <table>
                            <tr>
                                <td width="250px"></td>
                                <td>
                                    <asp:Label ID="importlbl" runat="server" Text="Total venta:" CssClass="label"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalPago" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                        Enabled="false" Style="text-align: right;"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="250px"></td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Importe pago:" CssClass="label"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPagoImporte" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                        Enabled="false" Style="text-align: right;"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="250px"></td>
                                <td>
                                    <asp:Label ID="restanlbl" runat="server" Text="Restan:" CssClass="label"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRestaPago" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                        Enabled="false" Style="text-align: right;"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="250px"></td>
                                <td>
                                    <asp:Label ID="totvtalbl" runat="server" Text="Cambio M.N.:" CssClass="label"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCambiopago" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                        Enabled="false" Style="text-align: right;"> </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalfooter">
                        <asp:LinkButton ID="LbtnPay" CssClass="linkmodal" ForeColor="White" runat="server"
                            OnClick="Pagar_Click">Aceptar </asp:LinkButton>
                    </div>
                    <asp:HiddenField ID="HdnImportePago" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerconfirm" runat="server" Style="display: none; background-color: transparent; border: 0px; border-color: transparent;">
        <asp:Panel ID="innerconfirm" runat="server" CssClass="ModalWindow">
            <div class="modalheader">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="confirmheaderlbl" runat="server" Text="Aviso" ForeColor="White" Font-Size="Large"
                                Font-Bold="true" Font-Names="Arial" />
                        </td>
                        <td align="right" valign="top">
                            <asp:ImageButton ID="confirmheaderimgbtn" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                Width="20px" Height="20px" OnClick="CloseConfirm" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modalbody">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="advertiselbl" runat="server" Text="Desea realizar el pago de la venta?"
                                CssClass="label_title">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="paymdlfooter" class="modalfooter" style="overflow: hidden;">
                <table>
                    <tr>
                        <td width="65px"></td>
                        <td>
                            <asp:LinkButton ID="LinkYes" CssClass="linkmodal" runat="server" OnClick="ConfirmClick">Si
                            </asp:LinkButton>
                        </td>
                        <td width="65px"></td>
                        <td>
                            <asp:LinkButton ID="LinkNo" CssClass="linkmodal" runat="server" OnClick="PrintDocument"
                                OnClientClick="CerrarPopupNoPay(this);" Text="No">
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="outeralmacengeneral" runat="server" Style="display: none; background-color: transparent; border: 0px; border-color: transparent;">
        <asp:Panel ID="inneralmacengeneral" runat="server" CssClass="ModalWindow">
            <div class="modalheader">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="generalstorehdrlbl" runat="server" Text="Seleccionar un almacén" ForeColor="White"
                                Font-Size="Large" Font-Bold="true" Font-Names="Arial" />
                        </td>
                        <td align="right" valign="top">
                            <asp:ImageButton ID="generalstoreimgbtn" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                Width="20px" Height="20px" OnClick="GeneralStoreClose" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modalbody">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="generalstorelbl" runat="server" Text="Almacén" CssClass="label">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="generalstoreddl" runat="server" CssClass="DropDown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modalfooter">
                <table>
                    <tr>
                        <td width="75px"></td>
                        <td>
                            <asp:LinkButton ID="generalstorebtn" runat="server" Text="Aceptar" CssClass="link"
                                OnClick="GeneralStoreClick">
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerchange" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innerchange" runat="server" DefaultButton="lnkAgregaProductos" CssClass="ModalWindow">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="LbtnPay" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modalheader">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="titlechangelbl" runat="server" Text="Cambio" ForeColor="White" Font-Size="Large"
                                        Font-Bold="true" Font-Names="Arial" />
                                </td>
                                <td align="right" valign="top">
                                    <asp:ImageButton ID="closechangeimg" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                        Width="20px" Height="20px" OnClick="closechange_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalbody">
                        <table style="margin-bottom: 2px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMensajeCambio" runat="server" CssClass="label"> </asp:Label>
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
                                            <asp:Label Visible="false" Width="0px" ID="lblNumber" runat="server" Text='<%# Bind("Numero") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="0" HeaderStyle-CssClass="NoMostrar"
                                        ItemStyle-CssClass="NoMostrar">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMoneda" runat="server" Width="0px" CssClass="LabelStyleCambioMoneda"
                                                Text='<%# Bind("Clas_Moneda") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="0" HeaderStyle-CssClass="NoMostrar"
                                        ItemStyle-CssClass="NoMostrar">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipocambio" Width="0px" runat="server" CssClass="LabelStyleChange"
                                                Text='<%# Bind("TC") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Descripcion" HeaderText="Concepto" />
                                    <asp:TemplateField HeaderText="Importe">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtIngreso" Text='<%# "$" + Eval("Egreso") %>' runat="server" MaxLength="20"
                                                onclick="redircambio(this)" onblur="redirbcambio(this)" CssClass="TextBoxStyleChange"
                                                Style="text-align: right;" Width="100px" AutoPostBack="true" OnTextChanged="ImporteCambioTextChanged"> </asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FtdTxTExtimporte" runat="server" TargetControlID="txtIngreso"
                                                FilterType="Custom, Numbers" ValidChars=".,$"></asp:FilteredTextBoxExtender>
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
                                        <asp:Label ID="MontoCambiolbl" runat="server" Text="Cambio:" CssClass="label"> </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MontoCambiotxt" runat="server" CssClass="TextBoxEstilo" Enabled="false"
                                            Width="100px" Style="text-align: right;"> </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="modalfooter">
                        <asp:LinkButton ID="addchangelnk" CssClass="link" ForeColor="White" runat="server"
                            OnClick="agregaCambio_Click">Aceptar </asp:LinkButton>
                    </div>
                    <asp:HiddenField ID="HdnImporteCambio" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerTransaccion" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innerafectacioncontable" runat="server" DefaultButton="lnkAgregaProductos"
            CssClass="ModalWindow">
            <div class="modalheader">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Aviso" ForeColor="White" Font-Size="Large"
                                Font-Bold="true" Font-Names="Arial" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modalbody">
                <div id="Div3">
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
            </div>
            <div class="modalfooter">
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
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerTransaccionCambio" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innerTransaccionCambio" runat="server" CssClass="ModalWindow">
            <div class="modalheader">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblAvisoTransaccionCambio" runat="server" Text="Aviso" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modalbody">
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
            <div class="modalfooter">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton2" CssClass="link" runat="server" OnClick="CambioSinTransaccionClick">Si 
                            </asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton1" CssClass="link" runat="server" OnClick="NoContinuaCambioClick">No
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ModalPopupExtender ID="mdlFormaPago" runat="server" TargetControlID="collethdn"
        PopupControlID="outercollet" BackgroundCssClass="ModalBackGround">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="salemsgmdl" runat="server" TargetControlID="salemsghdn"
        PopupControlID="outersalemsg" BackgroundCssClass="ModalBackGround" />
    <asp:ModalPopupExtender ID="paidmdl" runat="server" TargetControlID="paidhdn" PopupDragHandleControlID="quotedrag"
        PopupControlID="outerpaid" BackgroundCssClass="ModalBackGround" />
    <asp:ModalPopupExtender BehaviorID="confirmarmdl" ID="confirmmdl" runat="server"
        TargetControlID="confirmhdn" PopupDragHandleControlID="confirmdrag" PopupControlID="outerconfirm"
        BackgroundCssClass="ModalBackGround" />
    <asp:ModalPopupExtender ID="invoicemdl" runat="server" TargetControlID="invoicehdn"
        PopupDragHandleControlID="invoicedrag" PopupControlID="outerinvoice" BackgroundCssClass="ModalBackGround" />
    <asp:ModalPopupExtender BehaviorID="imprimirmdl" ID="printmdl" runat="server" TargetControlID="printhdn"
        PopupControlID="outerprint" BackgroundCssClass="ModalBackGround" Y="50" />
    <asp:ModalPopupExtender ID="generalstoremdl" runat="server" TargetControlID="generalstorehdn"
        PopupControlID="outeralmacengeneral" BackgroundCssClass="ModalBackGround">
    </asp:ModalPopupExtender>

    <asp:ModalPopupExtender ID="messagemdl" runat="server" TargetControlID="hidden3"
        PopupDragHandleControlID="messagedrag" PopupControlID="outermessage" BackgroundCssClass="ModalBackGround">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hidden3"
        PopupDragHandleControlID="messagedrag" PopupControlID="outermessage" BackgroundCssClass="ModalBackGround">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="productsmdl" runat="server" TargetControlID="hidden1"
        PopupControlID="outerproducts" BackgroundCssClass="ModalBackGround">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="mdlCambio" runat="server" TargetControlID="changehdn"
        PopupControlID="outerchange" BackgroundCssClass="ModalBackGroundFront">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="mdlTransaccion" runat="server" TargetControlID="hdnTransaccion"
        PopupControlID="outerTransaccion" BackgroundCssClass="ModalBackGroundFront">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="mdlTransaccionCambio" runat="server" TargetControlID="hdnTransaccionCambio"
        PopupControlID="outerTransaccionCambio" BackgroundCssClass="ModalBackGroundFront">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="charmdl" runat="server" TargetControlID="hidden4" PopupDragHandleControlID="chardrag"
        PopupControlID="outercharacteristics" BackgroundCssClass="ModalBackGround">
    </asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="rdnAlmacenGeneral" runat="server" TargetControlID="inneralmacengeneral"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>
    <asp:RoundedCornersExtender ID="outerprdn" runat="server" TargetControlID="productsPanel"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>
    <asp:RoundedCornersExtender ID="innerincoicerdn" runat="server" TargetControlID="innerinvoice"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>
    <asp:RoundedCornersExtender ID="printrdn" runat="server" TargetControlID="printpnl"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>
    <asp:RoundedCornersExtender ID="innersalerdn" runat="server" TargetControlID="innersalemsg"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>
    <asp:RoundedCornersExtender ID="innermessagerdn" runat="server" TargetControlID="innermessage"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>
    <asp:RoundedCornersExtender ID="innerpaidrdn" runat="server" TargetControlID="innerpaid"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>
    <asp:RoundedCornersExtender ID="innercolletrdn" runat="server" TargetControlID="innercollet"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>
    <asp:RoundedCornersExtender ID="RDOinnerchange" runat="server" TargetControlID="innerchange"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>
    <asp:RoundedCornersExtender ID="RDOinnerconfirm" runat="server" TargetControlID="innerconfirm"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>

    <div id="hidden" style="display: none;">
        <input name="container" id="container" runat="server" />
    </div>

</asp:Content>

