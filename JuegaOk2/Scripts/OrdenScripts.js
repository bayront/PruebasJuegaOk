var DatosOrdenPush = "";
var Total;
var DescuentoTotal;

function OcultarTable(OrdenID) {
    var x = document.getElementById("table-orden");
    var y = document.getElementById("Detalles-Orden");
    if (x.style.display === "none") {
        x.style.display = "block";
        y.style.display = "none";
    } else {
        y.style.display = "block";
        x.style.display = "none";
        OrderDetalle(OrdenID, 1);
    }
}

function FechaFormat(number,size) {
    var s = "0000" + number;
    return s.substr(s.length - size);
}

function getBase64Image(img) {
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0);
    var dataURL = canvas.toDataURL();
    return dataURL;
}

function DatosOrden(ID, datosProductos) {
    fetch('http://localhost:55571/Ordenes/Detalle/'+ID)
        .then(function (res) {
            return res.json();
        })
        .then(function (miJson) {
            var dt = new Date(parseInt(miJson.OrderDate.replace('/Date(', '')));
            var fechaCompra = FechaFormat(dt.getDate(), 2) + '-' +
                FechaFormat(dt.getMonth() + 1, 2) + '-' +
                FechaFormat(dt.getFullYear(), 4);

            DatosOrdenPush = "Código de la Orden: " + ID + "\n" + "Empelado Encargado: " + miJson.Empleado + "\n" + "Nombre del Cliente: " + miJson.ContactName + "\n" + "Fecha de la Compra: " + fechaCompra + "\n\n";
            Total = miJson.Total;
            DescuentoTotal = miJson.DescuentoTotal;

            let miResultado = "";
            miResultado +=
                "<div class='form-group'><a onclick='OcultarTable(null);' class='btn btn-success'>Volver</a>|<a onclick=OrderDetalle("+ID+","+0+"); class='btn btn-danger' title='Exportar Reporte'>.PDF</a></div>" +
                "<img src='/Content/imagenes/logo.png' />" + "<br />" +
                "<dt>Código de la Orden: " + ID + " </dt >" +
                "<dt>Empleado encargado: " + miJson.Empleado + " </dt >" +
                "<dt>Nombre del cliente: " + miJson.ContactName + " </dt >" +
                "<dt>Fecha de la compra: " + fechaCompra + "</dt >" + "<br />" +
                "<table class='table'><thead><tr style='background-color: #eeeeee;'>" +
                "<th scope='col'>Producto</th>" +
                "<th scope='col'>Precio Unitario</th>" +
                "<th scope='col'>Cantidad</th>" +
                "<th scope='col'>Descuento</th>" +
                "<th scope='col'>Total</th>" + "</tr></thead><tbody>" +
                datosProductos+
                "<tr><td></td><td></td><td></td><td></td><td></td></tr>"+
                "<tr><td></td>"+
                "<td></td>"+
                "<td></td>"+
                "<td style='font-weight:bold; text-align:right'>Descuento Total: </td>"+
                "<td>"+DescuentoTotal+"</td></tr>"+
                "<tr><td></td>"+
                "<td></td>"+
                "<td></td>"+
                "<td style='font-weight:bold; text-align:right'>Total: </td>"+
                "<td>"+Total+"</td></tr>"+
                "</tbody></table>";             
            document.getElementById("Detalles-Orden").innerHTML = miResultado;
        });
}

function OrderDetalle(ID,Opcion) {
    fetch('http://localhost:55571/Ordenes/GetOrderDetalleExtend/' + ID)
        .then(function (res) {
            return res.json();
        })
        .then(function (miJson) {
            if (Opcion === 1) { 
                let datosTabla = "";
                for (let i = 0; i < miJson.length; i++) {                 
                    datosTabla += "<tr>" + "<td>" + miJson[i].ProductName + "</td>" + "<td>" + miJson[i].UnitPrice + "</td>" + "<td>" + miJson[i].Quantity + "</td>" + "<td>" + miJson[i].Discount + "%" + "</td>" + "<td>" + miJson[i].ExtendedPrice + "</td></tr>";
                }
                DatosOrden(ID, datosTabla);
            }
            else if(Opcion===0){
                var dato = [];
                dato.push([{ text: "Producto", style: 'tableHeader' }, { text: "Precio Unitario", style: 'tableHeader' }, { text: "Cantidad", style: 'tableHeader' }, { text: "Descuento", style: 'tableHeader' }, { text: "Total", style: 'tableHeader' }]);
                for (let i = 0; i < miJson.length; i++) {
                    dato.push([{ text: miJson[i].ProductName }, { text: miJson[i].UnitPrice.toString() }, { text: miJson[i].Quantity.toString() }, { text: miJson[i].Discount.toString() + "%" }, { text: miJson[i].ExtendedPrice.toString() }]);                    
                }
                dato.push([{ text: "" }, { text: "" }, { text: "" }, { text: "" }, { text: "" }]);
                dato.push([{ text: "" }, { text: "" }, { text: "" }, { text: "Descuento Total: ", alignment: "right", bold: true }, { text: DescuentoTotal.toString() }]);
                dato.push([{ text: "" }, { text: "" }, { text: "" }, { text: "Total: ", alignment: "right", bold: true }, { text: Total.toString() }]);
                ImprimirPDF(dato);
            }
        });
}

function ImprimirPDF(datos) {
    var base64 = getBase64Image(document.getElementById("imgLogo"));
    var docDefinition = {
        footer: {

            columns: [
                { text: 'JuegaOk', alignment: 'center' }
            ]
        },
        content: [
            {
                image: base64,
                width: 150
            },
            {
                text: DatosOrdenPush,                
                style: 'header'

            },
            {
                style: 'tableExample',
                table: {
                    body: datos
                }, layout: 'lightHorizontalLines'

            }

        ],
        styles: {
            header: {
                fontSize: 10,
                bold: true
            },
            tableExample: {
                margin: [0, 5, 0, 5]
            },
            tableHeader: {
                bold: true,
                fontSize: 13,
                color: 'black',
                fillColor: '#eeeeee'
            }
        }
    };
    pdfMake.createPdf(docDefinition).download();
}