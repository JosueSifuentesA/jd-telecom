$(document).ready(function() {
    function obtenerUsuariosSeleccionados() {
        var usuariosSeleccionados = [];

        // Itera a través de los elementos checkbox marcados dentro de "userContainer_user"
        $('.userContainer_user input[type="checkbox"]:checked').each(function() {
            var $userContainer = $(this).closest('.userContainer_user');
            var userId = $userContainer.find('label:first').text(); // Obtén el valor oculto
            var userName = $userContainer.find('label:nth-child(3)').text(); // Nombre del usuario
            var userDNI = $userContainer.find('label:last').text(); // DNI del usuario

            // Crea un objeto de usuario y agrégalo al arreglo
            var usuario = {
                id: userId,
                nombre: userName,
                dni: userDNI
            };

            usuariosSeleccionados.push(usuario);
        });

        return usuariosSeleccionados;
    }

    $('#btnAsignarDescuento, #btnAsignarPromocion').click(function() {
        var accion = $(this).attr('id');
        var usuariosSeleccionados = obtenerUsuariosSeleccionados();

        console.log('Acción:', accion);
        console.log('Usuarios Seleccionados:', usuariosSeleccionados);

        var enlace = $('#accionAdministrativa');
        var form = $('#formAccion')
        var btnAccionAdministrativa = $('#accionAdministrativa2')

        if (accion === 'btnAsignarDescuento') {
            enlace.attr(
                "href", "/Administracion/AsignarPromocion"  
            ).text('Asignar Descuento');
            form.attr('action', 'AsignarPromocion');
            btnAccionAdministrativa.text('Asignar Descuento')
            
        } else if (accion === 'btnAsignarPromocion') {
            enlace.attr(
                "href", "/Administracion/AsignarPromocion"  
            ).text('Asignar Promoción');
            form.attr('action', 'AsignarPromocion');
            btnAccionAdministrativa.text('Asignar Promocion')
            
        }

        if (accion === 'btnAsignarDescuento') {
            $('#popupTitle').text('Asignar Descuento');
        } else if (accion === 'btnAsignarPromocion') {
            $('#popupTitle').text('Asignar Promoción');
        }

        $('#usuariosSeleccionadosContainer').empty();

        for (var i = 0; i < usuariosSeleccionados.length; i++) {
            var usuario = usuariosSeleccionados[i];
            var usuarioDiv = $('<div>').addClass('usuarioSeleccionado');

            var labelNombre = $('<label>').text(usuario.nombre);
            var labelDNI = $('<label>').text(usuario.dni);

            usuarioDiv.append(labelNombre);
            usuarioDiv.append(labelDNI);

            $('#usuariosSeleccionadosContainer').append(usuarioDiv);
            
        }

        fetch('/Administracion/ObtenerPromociones', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        })
            .then(response => response.json())
            .then(data => {
                var select = document.createElement('select');
                select.id = 'nuevoSelect';
                select.name = 'efectoId'
        
                data.forEach(promocion => {
                    var optionElement = document.createElement('option');
                    optionElement.value = promocion.id_promocion;
                    optionElement.text = promocion.nombre_promocion;
                    select.appendChild(optionElement);
                });
        
                var usuariosSeleccionadosContainer = document.getElementById('usuariosSeleccionadosContainer');
                usuariosSeleccionadosContainer.appendChild(select);
            })
            .catch(error => {
                console.error('Error al obtener datos: ' + error);
            });
        

        $('#accionAdministrativa2').click(function (event) {
        
            event.preventDefault(); // Previene la acción predeterminada del botón (enviar el formulario)
            var usuariosSeleccionados = obtenerUsuariosSeleccionados();

            // Crea un arreglo para almacenar los valores de inputId
            var inputIdsArray = [];

            // Recorre los usuarios seleccionados y agrega sus inputId al arreglo
            for (var i = 0; i < usuariosSeleccionados.length; i++) {
                inputIdsArray.push(parseInt(usuariosSeleccionados[i].id));
            }

            console.log(inputIdsArray);
            // Convierte el arreglo en una cadena JSON
            var inputIdsJson = JSON.stringify(inputIdsArray);

            // Asigna la cadena JSON al campo oculto inputIds
            $('#inputIds').val(inputIdsJson);
            
            console.log(inputIdsJson);
            // Envía el formulario
            $('#formAccion').submit();
        
        })

        $('#popUpContainer').show();
    });

    $('#cerrarPopup').click(function() {
        // Oculta el PopUpContainer al hacer clic en el botón de cierre
        $('#popUpContainer').hide();
    });

});