$(document).ready(function () {

    var clonedPlanCard = null;

    $('.PlanCard #btnSeleccionar').click(function (event) {
        event.preventDefault();
    
        var selectedPlanCard = $(this).closest('.PlanCard');
        var planContent = selectedPlanCard.clone(); // Clonamos toda la tarjeta
    
        var planSelectedContainer = $('.PlanModule_PlanSelectedContainer');
        const hasActualPlan = planSelectedContainer.find('.PlanSelectedContainer_actualPlan').length > 0;
    
        if (hasActualPlan) {
            planContent.find('#btnSeleccionar').replaceWith('');
        } else {
            planContent.find('#btnSeleccionar').replaceWith('<button id="btnSolicitar">Solicitalo</button>');
        }
    
        var planSelectedHandler = $('.PlanModule_PlanSelectedContainer .planSelectedHandler');
        if(hasActualPlan){
            planSelectedHandler.empty().append('<h1>Plan seleccionado:</h1>');
            planSelectedHandler.append(planContent);
            planSelectedHandler.append('<button id="btnCambiar">Cambiar Plan</button>')
        }else{
            planSelectedHandler.empty().append('<h1>Plan seleccionado:</h1>');
            planSelectedHandler.append(planContent);
        }
    
        console.log(hasActualPlan ? "CAMBIAR PLAN" : "SOLICITAR PLAN");
    });

    $(".planSelectedHandler").on("click", "#btnCambiar", function(event) {
        event.preventDefault();
        //var planCard = $(this).closest('.PlanCard');
        //console.log(planCard);
        var planId = $(this).closest('.planSelectedHandler').find("label[hidden]").text();
        console.log("El plan ID es: " + planId);
        var url = `/Cliente/CambiarPlan?planId=${planId}`;
        fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
        }).then(() => {
            window.location.href = `/Cliente/MisPlanes`;
        });
        
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
            window.location.href = `/Cliente/MisPlanes`
        })
        
        


    });

});