$(document).ready(function() {

    $('.identificadorRecibo_downloadContainer').click(function(event){

        var idRecibo = $(this).closest(".contenedorRecibos_recibo").find("#reciboName").text();

            fetch(`/GenerarRecibos?idRecibo=${idRecibo}`, {
                method: "GET"
            })
                .then(response => {
                    if (response.ok) {
                        return response.blob(); 
                    } else {
                        console.error("Error al realizar la solicitud: " + response.status);
                    }
                })
                .then(blob => {
                    
                    const url = window.URL.createObjectURL(blob);
                    const a = document.createElement('a');
                    a.style.display = 'none';
                    a.href = url;
                    a.download = idRecibo + ".pdf"; 
                    document.body.appendChild(a);
                    a.click();
                    window.URL.revokeObjectURL(url);
                })
                .catch(error => {
                    console.error("Error en la solicitud: " + error);
                });   


        console.log(`HE PRESIONADO DESCARGAR - ${idRecibo}`)
    })










})