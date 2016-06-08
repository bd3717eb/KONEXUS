<%@ Page Title="" Language="C#" MasterPageFile="~/mpIntegraCompany.Master" AutoEventWireup="true" CodeBehind="BuscarDevoluciones.aspx.cs" Inherits="Integra_Develoment.Ventas.BuscarDevoluciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHIntegraCompany" runat="server">

    <div id="divMenuRight">
        <div id="divMenuRightItem_11" class="linkMenu">
            &nbsp;
            <img src="../Images/ModIcoEvento.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="btnLimpiar" runat="server" Text=" Limpiar datos" OnClick="btnLimpiar_Click"></asp:LinkButton>
        </div>
        <div id="divMenuRightItem_22" class="linkMenu">
            &nbsp;
            <img src="../Images/ModIcoEvento.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="btnBuscar" runat="server" Text=" Buscar devoluciones" OnClick="btnBuscar_Click"></asp:LinkButton>
        </div>
        <div id="divMenuRightItem_33" class="linkMenu">
            &nbsp;
            <img src="../Images/ModIcoEvento.png" alt="img" />
            &nbsp;
            <asp:LinkButton ID="btnRegresar" runat="server" Text=" Regresar" OnClick="btnRegresar_Click" PostBackUrl="~/Ventas/ConsultaDevolucion.aspx"></asp:LinkButton>
        </div>
    </div>
    <fieldset class="fieldset">
        <legend>Búsqueda devoluciones</legend>
        <asp:HiddenField ID="keycontainer" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="linkCancelacion" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="grvDevoluciones" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Label ID="lblError" runat="server" Visible="false" CssClass="label_title" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblNombreCorto" runat="server" Visible="false" CssClass="label_title"></asp:Label>
                <asp:Label ID="lblAlmacen" runat="server" Visible="false" CssClass="label_title"></asp:Label>
                <asp:Label ID="lblIdCliente" runat="server" Visible="false" CssClass="label_title"></asp:Label>
                <asp:Label ID="lblTotal" runat="server" Visible="false" CssClass="label_title"></asp:Label>
                <asp:Label ID="lblClasIva" runat="server" Visible="false" CssClass="label_title"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblCliente" runat="server" Text="Cliente" CssClass="label_title" Width="80px"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtCliente" runat="server" CssClass="TextBoxEstilo" AutoPostBack="True"
                        Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblConcDev" runat="server" Text="Concepto devoluciones" CssClass="label_title"
                        Width="80px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlConcDev" runat="server" CssClass="DropDown" Width="105px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblEstatus" runat="server" Text="Estatus" CssClass="label_title"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEstatus" runat="server" CssClass="DropDown" Width="105px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFechaDe" runat="server" Text="Fecha de" CssClass="label_title"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaDe" runat="server" CssClass="TextBoxEstilo" Width="100px"
                        MaxLength="10"></asp:TextBox>
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
                    <asp:Label ID="lblFechaHasta" runat="server" Text="Hasta" CssClass="label_title"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="TextBoxEstilo" Width="100px"
                        MaxLength="10"></asp:TextBox>
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
                    <asp:Label ID="lblNumeroDe" runat="server" Text="Número de" Width="80px" CssClass="label_title"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumeroDe" runat="server" CssClass="TextBoxEstilo" Width="100px"
                        MaxLength="10"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="fltrNumeroDe" runat="server" TargetControlID="txtNumeroDe"
                        FilterType="Numbers"></asp:FilteredTextBoxExtender>
                </td>
                <td>
                    <asp:Label ID="lblNumeroHasta" runat="server" Text="Hasta" Width="43px" CssClass="label_title"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNumeroHasta" runat="server" CssClass="TextBoxEstilo" Width="100px"
                        MaxLength="10"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="fltrNumeroHasta" runat="server" TargetControlID="txtNumeroHasta"
                        FilterType="Numbers"></asp:FilteredTextBoxExtender>
                </td>
            </tr>
        </table>
        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="~/ClientNotasAutoWebService.asmx"
            MinimumPrefixLength="1" ServiceMethod="ObtieneClientesDevoluciones" EnableCaching="true"
            TargetControlID="txtCliente" UseContextKey="true" CompletionSetCount="10" CompletionInterval="0"
            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
            OnClientItemSelected="GetKey">
        </asp:AutoCompleteExtender>
    </fieldset>
    <fieldset class="fieldset">
        <legend>Resultados de búsqueda</legend>
        <asp:UpdatePanel ID="UpdtGrid" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="linkCancelacion" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="grvDevoluciones" />
            </Triggers>
            <ContentTemplate>
                <div id="scrollagregar" runat="server">
                    <asp:GridView ID="grvDevoluciones" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="Numero,Estatus,Nombre_Corto,Clas_Almacen,Numero_Cliente,Total,Numero_Entrada,Numero_Factura,Tipo_Devolucion,Clas_Iva,Folio"
                        CellPadding="4" GridLines="None" AllowPaging="True"
                        PageSize="8" OnPageIndexChanging="grvDevoluciones_PageIndexChanging" OnRowDeleting="btnConfirmarClick"
                        OnRowDataBound="grvDevoluciones_RowDataBound" Width="99%">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" SelectText="" ItemStyle-Width="0" HeaderStyle-CssClass="NoMostrar"
                                ItemStyle-CssClass="NoMostrar" />
                            <asp:TemplateField HeaderText="Eliminar" ItemStyle-Width="60">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnBorrar" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                        CommandName="Delete" ImageUrl="~/Images/Ventas/eliminar2.png" runat="server"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Numero" HeaderText="Número" SortExpression="Numero" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="72" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-Width="72" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Des_Estatus" HeaderText="Estatus" SortExpression="Des_Estatus"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Numero_Cliente" HeaderText="Núm.Cliente" SortExpression="Numero_Cliente"
                                Visible="false" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Nombre_Cliente" HeaderText="Cliente" SortExpression="Nombre_Cliente"
                                ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Des_Concepto_Dev" HeaderText="Concepto" SortExpression="Des_Concepto_Dev"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Des_Almacen" HeaderText="Almacén" SortExpression="Des_Almacen"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Des_Causa_Dev" HeaderText="Causa" SortExpression="Des_Causa_Dev"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Folio" HeaderText="Folio" SortExpression="Folio" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Des_Moneda" HeaderText="Moneda" SortExpression="Des_Moneda"
                                ItemStyle-HorizontalAlign="Center" />
                            <%--    <asp:BoundField DataField="Tipo_Cambio" HeaderText="T.C" SortExpression="Tipo_Cambio"
                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:F2}" />--%>
                            <asp:BoundField DataField="SubTotal" HeaderText="Subtotal" SortExpression="SubTotal"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F2}" />
                            <asp:BoundField DataField="Iva" HeaderText="Iva" SortExpression="Iva" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:F2}" />
                            <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:F2}" />
                            <asp:BoundField DataField="Tipo_Devolucion" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="Des_Tipo_Devolucion" HeaderText="Tipo devolución" SortExpression="Des_Tipo_Devolucion"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Estatus" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="Nombre_Corto" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="Clas_Almacen" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="Numero_Factura" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="Numero_Entrada" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="Clas_Iva" Visible="false"></asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyles" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />

                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <asp:Panel ID="outercancelacion" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innercancelacion" runat="server" CssClass="ModalWindow">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="grvDevoluciones" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="linkCancelacion" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modalheader">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="invoicehdrlbl" runat="server" Text="Datos cancelación" ForeColor="White"
                                        Font-Size="Large" Font-Bold="true" Font-Names="Arial" />
                                </td>
                                <td align="right" valign="top">
                                    <asp:ImageButton ID="imgcierra" runat="server" ImageUrl="../Images/GridImages/delete_grid.png"
                                        Width="20px" Height="20px" OnClick="cierraModal_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalbody">
                        <table width="100%">
                            <tr>
                                <td>
                                    <td>
                                        <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="label" />
                                    </td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFechaCancelacion" runat="server" Text="Fecha cancelación" CssClass="label_title" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFechaCancelacion" runat="server" CssClass="TextBoxEstilo" Width="100px"
                                        MaxLength="10">
                                    </asp:TextBox>
                                    <asp:CalendarExtender ID="calFechaCancelacion" runat="server" TargetControlID="txtFechaCancelacion"
                                        CssClass="cal_Theme1"></asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCausaCancelacion" runat="server" Text="Causa cancelación" CssClass="label_title" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCausaCancelacion" runat="server" TextMode="multiline" Style="border: 1px solid #8DB3E2;"
                                        Rows="3" Columns="20" CssClass="TextAreaStyle"> 
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modalfooter">
                        <asp:LinkButton ID="linkCancelacion" runat="server" ForeColor="White" OnClick="linkCancelacion_Click"
                            CssClass="linkmodal" Text="Aceptar">
                        </asp:LinkButton>
                    </div>
                    <asp:HiddenField ID="hdnNumeroDevolucion" runat="server" />
                    <asp:HiddenField ID="hdnEstatus" runat="server" />
                    <asp:HiddenField ID="hdnTipoDevolucion" runat="server" />
                    <asp:HiddenField ID="hdnEntradaAlmacen" runat="server" />
                    <asp:HiddenField ID="hdnNumeroFactura" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="outerTransaccion" runat="server" Style="display: none; background-color: transparent; border-width: 0px; border-color: transparent;">
        <asp:Panel ID="innerTransaccion" runat="server" CssClass="ModalWindow">
            <asp:UpdatePanel ID="updtTransaccion" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="linkCancelacion" EventName="Click" />
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
    <asp:ModalPopupExtender ID="mdlDatosCancelacion" runat="server" TargetControlID="HiddenField"
        PopupControlID="outercancelacion" BackgroundCssClass="ModalBackGround">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="mdlTransaccion" runat="server" TargetControlID="hdnTransaccion"
        PopupControlID="outerTransaccion" BackgroundCssClass="ModalBackGround">
    </asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="rceDatosCancelacion" runat="server" TargetControlID="innercancelacion"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>
    <asp:RoundedCornersExtender ID="rceTransaccion" runat="server" TargetControlID="innerTransaccion"
        BorderColor="#467c9e" Color="#467c9e" Radius="8" Corners="Top"></asp:RoundedCornersExtender>
</asp:Content>

