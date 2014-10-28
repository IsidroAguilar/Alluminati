/// Autor: Isidro Aguilar
/// Fecha: 21/10/2014
/// Descripción: Funciones JavaScript del Login de Usuarios Alluminati

$(document).ready(function () {
    // Establecer default para bootbox.
    bootbox.setDefaults({
        title: 'Notifiación',
        size: 'small',
    });

    // Validaciones.
    $('#formLogin').bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            Usuario: {
                validators: {
                    notEmpty: {
                        message: ' '
                    }
                }
            },
            Contrasena: {
                validators: {
                    notEmpty: {
                        message: ' '
                    }
                }
            }
        }
    }).on('success.form.bv', function (e) {
        e.preventDefault();
    });
});

//Login
$("#formLogin").submit(function () {
    debugger;
    $(this).data('bootstrapValidator').validate();
    if ($(this).data('bootstrapValidator').isValid()) {
        var contrasena = $("#Contrasena").val();
        $.ajax({
            url: $("#Ingresar").attr("data-url"),
            type: 'POST',
            data: $(this).serialize(),
            success: function (data) {
                if (data.result === true) {
                    window.location.replace(data.url);
                }
                else {
                    $("#Contrasena").val('');
                    bootbox.alert("Error. Verifique sus datos.");
                }
            },
            error: function (data) {
                $("#Contrasena").val('');
                bootbox.alert("Ha ocurrido un error.");
            }
        });
    }
    return false;
});