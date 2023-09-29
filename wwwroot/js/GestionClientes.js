$(document).ready(function() {
    // Función para obtener los usuarios seleccionados
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

    // Agrega un controlador de eventos a los botones "Asignar Descuento" y "Asignar Promoción"
    $('#btnAsignarDescuento, #btnAsignarPromocion').click(function() {
        var accion = $(this).attr('id'); // Obtenemos el ID del botón clickeado
        var usuariosSeleccionados = obtenerUsuariosSeleccionados();

        // Imprime el arreglo de usuarios seleccionados y la acción en la consola
        console.log('Acción:', accion);
        console.log('Usuarios Seleccionados:', usuariosSeleccionados);

        // Aquí comienza la adaptación para crear los enlaces <a>
        var enlace = $('#accionAdministrativa'); // Creamos un elemento <a>
        var form = $('#formAccion')
        var btnAccionAdministrativa = $('#accionAdministrativa2')

        // Verificamos la acción y configuramos los atributos asp-action y asp-controller adecuados
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

        // Agregamos el enlace creado al contenedor de usuarios seleccionados en el PopUpContainer
        //$('#usuariosSeleccionadosContainer').append(enlace);

        // Continuamos con el código original para mostrar el PopUpContainer
        if (accion === 'btnAsignarDescuento') {
            $('#popupTitle').text('Asignar Descuento');
        } else if (accion === 'btnAsignarPromocion') {
            $('#popupTitle').text('Asignar Promoción');
        }

        // Limpia el contenido previo del PopUpContainer
        $('#usuariosSeleccionadosContainer').empty();

        // Para cada usuario seleccionado, crea un div y agrega sus datos
        for (var i = 0; i < usuariosSeleccionados.length; i++) {
            var usuario = usuariosSeleccionados[i];
            var usuarioDiv = $('<div>').addClass('usuarioSeleccionado');


            
            //var inputId = $('<input>').attr('name', 'id').val(usuario.id);
            var labelNombre = $('<label>').text(usuario.nombre);
            var labelDNI = $('<label>').text(usuario.dni);

            //usuarioDiv.append(inputId);
            usuarioDiv.append(labelNombre);
            usuarioDiv.append(labelDNI);
            // Agrega los datos del usuario al nuevo div
            //usuarioDiv.html('ID: ' + usuario.id + '<br>Nombre: ' + usuario.nombre + '<br>DNI: ' + usuario.dni);

            // Agrega el nuevo div al contenedor de usuarios seleccionados en el PopUpContainer
            $('#usuariosSeleccionadosContainer').append(usuarioDiv);
        }

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


        // Muestra el PopUpContainer
        $('#popUpContainer').show();
    });

    $('#cerrarPopup').click(function() {
        // Oculta el PopUpContainer al hacer clic en el botón de cierre
        $('#popUpContainer').hide();
    });
});