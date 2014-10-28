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
    $('#formRegistro').bootstrapValidator({
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
            },
            Nombre: {
                validators: {
                    notEmpty: {
                        message: ' '
                    }
                }
             },
            ApellidoP: {
                 validators: {
                     notEmpty: {
                         message: ' '
                     }
                 }
             },
            CorreoElectronico: {
                 validators: {
                     notEmpty: {
                         message: ' '
                     }
                 }
             },
            Direccion: {
                 validators: {
                     notEmpty: {
                         message: ' '
                     }
                 }
            },
            Telefono: {
                validators: {
                    notEmpty: {
                        message: ' '
                    }
                }
            },
        }
    }).on('success.form.bv', function (e) {
        e.preventDefault();
    });
});

//Login
$("#formRegistro").submit(function () {
    debugger;
    $(this).data('bootstrapValidator').validate();
    if ($(this).data('bootstrapValidator').isValid()) {
        var contrasena = $("#Contrasena").val();
        var validaContrasena = $("#ValidaContrasena").val();
        if (contrasena === validaContrasena) {
            $.ajax({
                url: $("#Registrar").attr("data-url"),
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
        } else {
            error: function (data) {
                $("#Contrasena").val('');
                bootbox.alert("Verifique que su contraseña sea igual que en la confirmación.");
            }    
        }
    }
    return false;
});