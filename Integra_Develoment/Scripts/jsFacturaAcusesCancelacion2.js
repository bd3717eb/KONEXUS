
function gfllenadosgrids() { 
    //debugger;
    lfllenadogridI();
    lfllenadogridII();
}

function lfllenadogridI() {
    //debugger;
    var data = new Array();
    try {
        var rfcData = eval(document.getElementById('CPHIntegraCompany_rfcStringHidden').value);
        var clienteData = eval(document.getElementById('CPHIntegraCompany_clienteStringHidden').value);
        var numeroData = eval(document.getElementById('CPHIntegraCompany_numeroDataHidden').value);
        var dataSize = eval(document.getElementById('CPHIntegraCompany_dataSizeHidden').value);
    } catch (e) {

        var rfcData = eval(document.getElementById('rfcStringHidden').value);
        var clienteData = eval(document.getElementById('clienteStringHidden').value);
        var numeroData = eval(document.getElementById('numeroDataHidden').value);
        var dataSize = eval(document.getElementById('dataSizeHidden').value);
    }

    for (var i = 0; i < dataSize; i++) {
        var row = {};
        row["rfc"] = rfcData[i];
        row["cliente"] = clienteData[i];
        row["numero"] = numeroData[i]; data[i] = row
    }
    
    var source = {
        localdata: data,
        datatype: "array",
        datafields: [{ name: 'rfc', type: 'string' },
                     { name: 'cliente', type: 'string' },
                     { name: 'numero', type: 'number'}],
        pager: function (pagenum, pagesize, oldpagenum) { }
    };
    //var dataAdapter = new $.jqx.dataAdapter(source);

    var dataAdapter = new $.jqx.dataAdapter(source);

    $("#jqxgrid").jqxGrid({
        width: 325,
        height: 345,
        source: dataAdapter,
        columnsresize: false,
        showfilterrow: true,
        filterable: true,
        pageable: true,
        autoheight: false,
        selectionmode: 'singlerow',
        columns: [{
            text: 'RFC',
            dataField: 'rfc',
            width: 100,
            filtertype: 'textbox',
            filtercondition: 'starts_with'
        }, {
            text: 'Cliente',
            dataField: 'cliente',
            width: 225,
            filtertype: 'textbox',
            filtercondition: 'starts_with'
        }, {
            text: 'Num',
            editable: false,
            dataField: 'numero',
            width: 0, hidden: true
        }]
    });
    $("#jqxgrid").on('rowselect', function (event) {
      //  debugger;
        var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', event.args.rowindex);
        try {
            document.getElementById('selectedCustomerHidden').value = dataRecord.numero;
            document.getElementById('linkcustomerSelectedBtn').click()
        } catch (e) {
            document.getElementById('CPHIntegraCompany_selectedCustomerHidden').value = dataRecord.numero;
            document.getElementById('CPHIntegraCompany_linkcustomerSelectedBtn').click()
         }
    })
    document.getElementById("divProceso").style.visibility = "hidden";
}

function lfllenadogridII() {
    //debugger;
    var data = new Array();
    try{
        var vHidden_dataSizeF = eval(document.getElementById('CPHIntegraCompany_Hidden_dataSizeF').value);
        var vHidden_IDCliente = eval(document.getElementById('CPHIntegraCompany_Hidden_IDCliente').value);
        var vHidden_FechaEmision = eval(document.getElementById('CPHIntegraCompany_Hidden_FechaEmision').value);
        var vHidden_Documento = eval(document.getElementById('CPHIntegraCompany_Hidden_Documento').value);
        var vHidden_Total = eval(document.getElementById('CPHIntegraCompany_Hidden_Total').value);
        var vHidden_RFC = eval(document.getElementById('CPHIntegraCompany_Hidden_RFC').value);
        var vHidden_EstatusSAT = eval(document.getElementById('CPHIntegraCompany_Hidden_EstatusSAT').value);
        var vHidden_Detalle = eval(document.getElementById('CPHIntegraCompany_Hidden_Detalle').value);
        var dataSizeF = document.getElementById('CPHIntegraCompany_Hidden_dataSizeF').value;
    }
    catch(e){
        var vHidden_dataSizeF = eval(document.getElementById('Hidden_dataSizeF').value);
        var vHidden_IDCliente = eval(document.getElementById('Hidden_IDCliente').value);
        var vHidden_FechaEmision = eval(document.getElementById('Hidden_FechaEmision').value);
        var vHidden_Documento = eval(document.getElementById('Hidden_Documento').value);
        var vHidden_Total = eval(document.getElementById('Hidden_Total').value);
        var vHidden_RFC = eval(document.getElementById('Hidden_RFC').value);
        var vHidden_EstatusSAT = eval(document.getElementById('Hidden_EstatusSAT').value);
        var vHidden_Detalle = eval(document.getElementById('Hidden_Detalle').value);
        var dataSizeF = document.getElementById('Hidden_dataSizeF').value;
    }

    for (var i = 0; i < dataSizeF; i++) {
        var row = {};
        row["vHidden_IDCliente"] = vHidden_IDCliente[i];
        row["vHidden_FechaEmision"] = vHidden_FechaEmision[i];
        row["vHidden_Documento"] = vHidden_Documento[i];
        row["vHidden_Total"] = vHidden_Total[i];
        row["vHidden_RFC"] = vHidden_RFC[i];
        row["vHidden_EstatusSAT"] = vHidden_EstatusSAT[i];
        row["vHidden_Detalle"] = vHidden_Detalle[i];
        data[i] = row
    }

    var source = {
        localdata: data,
        datatype: "array", datafields: [
            { name: 'vHidden_IDCliente', type: 'number' },
            { name: 'vHidden_FechaEmision', type: 'string' },
            { name: 'vHidden_Documento', type: 'string' },
            { name: 'vHidden_Total', type: 'string' },
            { name: 'vHidden_RFC', type: 'string' },
            { name: 'vHidden_EstatusSAT', type: 'string' },
            { name: 'vHidden_Detalle', type: 'string' }
        ],
        pager: function (pagenum, pagesize, oldpagenum) { }
    };
    var dataAdapter = new $.jqx.dataAdapter(source);
    $("#jqxgridInvoices").jqxGrid({
        //    width: 570,
        width: 630,
        height: 345,
        source: dataAdapter,
        columnsresize: true,
        showfilterrow: true,
        filterable: true,
        pageable: true,
        autoheight: true,
        selectionmode: 'singlerow',
        columns: [{ text: '', editable: false, dataField: 'vHidden_IDCliente', width: 1, hidden: true },
                 { text: 'Fecha Emisión', dataField: 'vHidden_FechaEmision', filtertype: 'date', filtercondition: 'starts_with', width: 80, resizable: false },
                 { text: 'Documento', dataField: 'vHidden_Documento', filtertype: 'textbox', filtercondition: 'contains', width: 100, resizable: true },
                 { text: 'Total', dataField: 'vHidden_Total', filtertype: 'textbox', filtercondition: 'starts_with', width: 70, resizable: true },
                 { text: 'RFC', editable: false, dataField: 'vHidden_RFC', width: 100, resizable: true, filtercondition: 'starts_with' },
                 { text: 'Estatus SAT ', datafield: 'vHidden_EstatusSAT', cellsalign: 'right', cellsformat: 'c2', filtertype: 'textbox', filtercondition: 'starts_with', resizable: true },
                 { text: '', editable: false, dataField: 'vHidden_Detalle', width: 1, hidden: true}]
    });

    $("#jqxgridInvoices").on('rowselect', function (event) {
        //debugger;
        var dataRecord = $("#jqxgridInvoices").jqxGrid('getrowdata', event.args.rowindex);
        var docpdf = '../Ventas/DocumentoPDF.aspx';
        var str = document.getElementById('HiddenField_Select').value = dataRecord.vHidden_Detalle;
        var res = str.split("|");
        if (res[7] != "0") {
            alert(dataRecord.vHidden_EstatusSAT);
            return false;
        } else {
            $.ajax({
                type: "POST",
                url: "FacturaAcusesCancelacion2.aspx/gfFacturaRecuperaAcuseCancelacion",
                data: "{'pDetalle' : '" + dataRecord.vHidden_Detalle + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function (msg) {
                    document.getElementById("divProceso").style.visibility = "visible";
                },
                success: function (msg) {
                    window.open(msg.d, "ventana1", "width=300,height=175,scrollbars=YES,toolbar=no,location=0, directories=0, status=0")
                    document.getElementById("divProceso").style.visibility = "hidden";
                },
                error: function (msg) {
                    //alert(msg.d);
                    document.getElementById("divProceso").style.visibility = "hidden";
                }
            });
        }


    })

    $('#jqxgridInvoices').on('rowclick', function (event) {
        //debugger;
        try {
            var docpdf = '../Ventas/DocumentoPDF.aspx';
            var str = (event.args.owner.rows.records[event.args.rowindex].bounddata.vHidden_Detalle);
            var res = str.split("|");

            if (res[7] != "0") {
                alert(event.args.owner.rows.records[event.args.rowindex].bounddata.vHidden_EstatusSAT);
                return false;
            } else {
                $.ajax({
                    type: "POST",
                    url: "FacturaAcusesCancelacion2.aspx/gfFacturaRecuperaAcuseCancelacion",
                    data: "{'pDetalle' : '" + event.args.owner.rows.records[event.args.rowindex].bounddata.vHidden_Detalle + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function (msg) {
                        document.getElementById("divProceso").style.visibility = "visible";
                    },
                    success: function (msg) {
                        window.open(msg.d, "ventana1", "width=300,height=175,scrollbars=YES,toolbar=no,location=0, directories=0, status=0")
                        document.getElementById("divProceso").style.visibility = "hidden";
                    },
                    error: function (msg) {
                        //alert(msg.d);
                        document.getElementById("divProceso").style.visibility = "hidden";
                    }
                });
            }
        } catch (e) {
            alert(e.message);
        }
    });
}