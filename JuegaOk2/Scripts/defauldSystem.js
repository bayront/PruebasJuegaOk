$(document).ready(function () {
    $('#table-views').DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ registros por página",
            "zeroRecords": "No se encontraron registros coincidentes",
            "info": "Mostrando Página _PAGE_ de _PAGES_, de un total de _TOTAL_ registros",
            "infoEmpty": "No hay registros disponibles",
            "infoFiltered": "(filtrado de un total de _MAX_ registros)",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });

});

//Evitar archivos duplicados por el boton submit
enviando = false; //Para entrar al if en el primer submit


function checkSubmit() {
    if (!enviando) {
        enviando = true;
        return true;
    } else {
        //Si llega hasta aca significa que pulsaron 2 veces el boton submit
        //alert("El formulario ya se esta enviando");
        return false;
    }
}

$(function () {
    $('.datetimepicker1').datetimepicker({
        format: "yyyy-MM-DD"
    });
});

function Reporte() {

    swal("Elige una opción para descargar el reporte", {
        buttons: {
            Todos: {
                text: "Todos",
                value: "Todos"
            },
            Eliminados: {
                text: "Eliminados",
                value: "Eliminados"
            },
            Activos: {
                text: "Activos",
                value: "Activos"
            }
        }
    })
        .then((value) => {
            switch (value) {

                case "Todos":
                    leerDatos("Todos");
                    break;

                case "Eliminados":
                    leerDatos("Eliminados");
                    break;

                case "Activos":
                    leerDatos("Activos");
                    break;
            }
        });
}

function leerDatos(opcion) {
fetch('http://localhost:55571/Employees/GenerarReporte')
    .then(function (res) {
        return res.json();
    })
    .then(function (miJson) {
        var dato = [];
        dato.push([{ text: "Nombre", style: 'tableHeader' }, { text: "Apellido", style: 'tableHeader' }]);
        for (let i = 0; i < miJson.length; i++) {
            if (opcion === "Todos") {
                dato.push([{ text: miJson[i].FirstName }, { text: miJson[i].LastName }]);
            }
            else if (opcion === "Eliminados" && miJson[i].Eliminado === true) {
                dato.push([{ text: miJson[i].FirstName }, { text: miJson[i].LastName }]);
            }

            else if (opcion === "Activos" && miJson[i].Eliminado === false) {
                dato.push([{ text: miJson[i].FirstName }, { text: miJson[i].LastName }]);
            }
        }
        Imprimir(dato);
        });
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

function Imprimir(datos) {

    var fecha = new Date();
    var dd = fecha.getDate();
    var mm = fecha.getMonth()+1;
    var yyyy = fecha.getFullYear();

    var base64 = getBase64Image(document.getElementById("imgLogo"));

    var docDefinition = {
        footer: {

            columns: [
                { text:'JuegaOk', alignment: 'center' }
            ]
        },
        content: [
            {
                image: base64,
                width: 150
            },
            {
                columns: [
                    {
                        text: dd + "/" + mm + "/" + yyyy, alignment: 'right'


                    },
                    {
                        text: 'Trabajadores \n\n',
                        style: 'header'

                    }
                ]
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
                fontSize: 22,
                bold: true
            },
            tableExample: {
                margin: [0, 5, 0, 15]
            },
            tableHeader: {
                bold: true,
                fontSize: 13,
                color: 'black'
            }
        }
    };
    pdfMake.createPdf(docDefinition).download();
}