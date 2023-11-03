$(document).ready(function() {

    $('.identificadorRecibo_downloadContainer').click(function(event){

        var idRecibo = $(this).closest(".contenedorRecibos_recibo").find("#reciboName").text();

        /*fetch(`/GenerarRecibos?idRecibo=${idRecibo}`, {
            method: "GET",
            /*headers: {
                "Content-Type": "application/json"
            },
        })
            .catch(error => {
                console.error("Error al realizar la solicitud: " + error);
            });*/

            fetch(`/GenerarRecibos?idRecibo=${idRecibo}`, {
                method: "GET"
            })
                .then(response => {
                    if (response.ok) {
                        return response.blob(); // Convertir la respuesta en un objeto Blob
                    } else {
                        console.error("Error al realizar la solicitud: " + response.status);
                    }
                })
                .then(blob => {
                    // Ahora puedes trabajar con el objeto Blob, por ejemplo, para descargarlo
                    const url = window.URL.createObjectURL(blob);
                    const a = document.createElement('a');
                    a.style.display = 'none';
                    a.href = url;
                    a.download = idRecibo + ".pdf"; // Especifica el nombre del archivo
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