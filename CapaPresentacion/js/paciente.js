function templateRow() {
    var template = "<tr>";
    template += ("<td>" + "123" + "</td>");
    template += ("<td>" + "123" + "</td>");
    template += ("<td>" + "123" + "</td>");
    template += ("<td>" + "123" + "</td>");
    template += ("<td>" + "123" + "</td>");
    template += ("<td>" + "123" + "</td>");
    template += ("<td>" + "123" + "</td>");
    template += ("<td>" + "123" + "</td>");
    return template;
}

function addRow() {
    var template = templateRow();
    for (var i = 0; i < 10; i++) {
        $("#tbl_body_table").append(template);
    }
}
var tabla, data;
function addRowDT(data) {
    tabla = $("#tbl_pacientes").DataTable({
        "aaSorting": [[0, 'asc']],
        "bSort": true,
        "aoColumns": [
            null,
            null,
            null,
            null,
            null,
            null,
            { "bSortable": false }
        ]
    });

    tabla.fnClearTable();

    for (var i = 0; i < data.length; i++) {
        tabla.fnAddData([
            data[i].IdPaciente,
            data[i].Nombres,
            (data[i].ApPaterno + " " + data[i].ApMaterno),
            ((data[i].Sexo == 'M') ? "Masculino" : "Femenino"),
            data[i].Edad,
            data[i].Direccion,
            '<button type="button" value="Actualizar" title="Actualizar" class="btn btn-primary btn-edit" data-target="#imodal" data-toggle="modal"><i class="fa fa-check-square-o" aria-hidden="true"></i></button>&nbsp;' +
            '<button type="button" value="Eliminar" title="Eliminar" class="btn btn-danger btn-delete"><i class="fa fa-minus-square-o" aria-hidden="true"></i></button>'

        ]);
    }
}

function sendDataAjax() {
    $.ajax({
        type: "POST",
        url: "GestionarPaciente.aspx/ListarPacientes",
        data: {},
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (data) {
            //console.log(data);
            addRowDT(data.d);
        }
    });
}
//LLamando a la funcion de ajax al cargar el documento
sendDataAjax();