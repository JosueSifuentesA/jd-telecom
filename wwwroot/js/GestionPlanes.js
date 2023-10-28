$(document).ready(function () {

    var clonedPlanCard = null;

    $('.PlanCard #btnSeleccionar').click(function (event) {
        event.preventDefault()
        var selectedPlanCard = $(this).closest('.PlanCard');

        var newClonedPlanCard = selectedPlanCard.clone();
        newClonedPlanCard.find('#btnSeleccionar').replaceWith('<button id="btnSolicitar">Solicitalo</button>');

        /*if (clonedPlanCard) {
            $('.planSelectedHandler').empty().append('<h1>Plan seleccionado:</h1>', newClonedPlanCard);
        } else {
            $('.planSelectedHandler').empty().append('<h1>Plan seleccionado:</h1>', newClonedPlanCard);
        }*/
        
        var planSelectedContainer = $('.PlanModule_PlanSelectedContainer');


        if (planSelectedContainer.find('.PlanSelectedContainer_actualPlan').length > 0) {
            if (clonedPlanCard) {
                $('.planSelectedHandler').empty().append('<h1>Plan seleccionado:</h1>', newClonedPlanCard);
            } else {
                $('.planSelectedHandler').empty().append('<h1>Plan seleccionado:</h1>', newClonedPlanCard);
            }
        } else {
            if (clonedPlanCard) {
                $('.planSelectedHandler').empty().append('<h1>Plan seleccionado:</h1>', newClonedPlanCard);
                $('.PlanSelectedContainer_actualPlan_Inactive').hide();
            }else {
                $('.planSelectedHandler').empty().append('<h1>Plan seleccionado:</h1>', newClonedPlanCard);
            }
        }



        clonedPlanCard = newClonedPlanCard;
    });


    $(".planSelectedHandler").on("click", "#btnSolicitar", function(event) {
        event.preventDefault()
        var planId = $(this).closest(".PlanCard").find("label:hidden").text();
    
        console.log("El plan ID es: " + planId);
     
        var planId = $(this).closest(".PlanCard").find("label:hidden").text();

        var url = `/Cliente/SolicitarPlan?planId=${planId}`;
        
        fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
        }).then(()=>{
            window.location.href = window.location
        })
        
        


    });







    function crearPlan(nombrePlan,precioPlan,velocidadPlan,descripcionPlan){

        // Crear un objeto de plan ficticio para demostración
        var plan = {
            nombre_plan: nombrePlan,
            precio_plan: precioPlan,
            velocidad_plan: velocidadPlan,
            descripcion_plan: descripcionPlan
        };
    
        // Crear un elemento div para la PlanCard
        var planCard = $('<div>').addClass('PlanCard');
    
        // Crear el encabezado
        var header = $('<div>').addClass('PlanCard_header');
        header.append($('<label>').text(plan.nombre_plan));
        planCard.append(header);
    
        // Crear el precio
        var price = $('<div>').addClass('PlanCard_price');
        price.append($('<label>').text('S/' + plan.precio_plan.toString("0") + ' x mes'));
        planCard.append(price);
    
        // Crear la descripción
        var description = $('<div>').addClass('PlanCard_description');
        description.append($('<label>').addClass('description_webSpeed').text(plan.velocidad_plan + ' Mbps'));
        description.append($('<label>').addClass('description_info').text(plan.descripcion_plan));
        planCard.append(description);
    
        // Crear el contenedor de información
        var infoContainer = $('<div>').addClass('PlanCard_infoContainer');
    
        // Crear información
        var info = $('<div>').addClass('infoContainer_info');
        info.append($('<label>').text('Modem WI-FI'));
        info.append($('<label>').text('Descargas ilimitadas'));
        info.append($('<label>').text('+ canales HD/SD'));
        infoContainer.append(info);
    
        // Crear iconos
        var icons = $('<div>').addClass('infoContainer_icons');
        icons.append($('<img>').attr('alt', 'Icon of a Switch o Computer'));
        icons.append($('<img>').attr('alt', 'Icon of a Switch o Computer'));
        infoContainer.append(icons);
    
        planCard.append(infoContainer);
    
        // Crear el botón "Seleccionar"
        planCard.append($('<button>').attr('id', 'btnSeleccionar').text('Seleccionar'));
    
        // Agregar la PlanCard al elemento con ID "container" en el documento
        $('#container').append(planCard);


    }

    

});