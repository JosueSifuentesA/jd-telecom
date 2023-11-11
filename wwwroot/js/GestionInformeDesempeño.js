$(document).ready(function () {
    var myChartMensual; // Variable para almacenar la instancia del gráfico mensual
    var myChartAnual;   // Variable para almacenar la instancia del gráfico anual

    // Lógica para manejar el clic en el botón "Filtrar"
    $('#filtroForm').submit(function (event) {
        event.preventDefault(); // Evitar la recarga de la página al enviar el formulario

        var periodoSeleccionado = $('select[name="periodoInforme"]').val();

        // Ocultar ambos gráficos al cambiar de periodo
        $('#graficaMensual').hide();
        $('#graficaAnual').hide();

        // Destruir los gráficos existentes
        if (myChartMensual) {
            myChartMensual.destroy();
        }

        if (myChartAnual) {
            myChartAnual.destroy();
        }

        // Realizar la solicitud AJAX y mostrar el gráfico según el período seleccionado
        if (periodoSeleccionado === 'Mensual') {
            mostrarGraficoMensual();
        } else if (periodoSeleccionado === 'Anual') {
            mostrarGraficoAnual();
        }
    });

    // Función para mostrar el gráfico mensual
    function mostrarGraficoMensual() {
        // Realizar la solicitud AJAX para obtener los datos mensuales
        $.ajax({
            url: '/Administracion/GetMonthlyCompletedTickets',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                try {
                    // Acceder a los datos específicos en el formato proporcionado
                    var meses = data.$values.map(item => item.month);
                    var totalTickets = data.$values.map(item => item.totalTickets);
    
                    // Crear el gráfico de barras mensual
                    var ctxMensual = document.getElementById('graficaMensual').getContext('2d');
    
                    // Destruir el gráfico existente si hay uno
                    if (myChartMensual) {
                        myChartMensual.destroy();
                    }
    
                    myChartMensual = new Chart(ctxMensual, {
                        type: 'bar',
                        data: {
                            labels: meses,
                            datasets: [{
                                label: 'Total de Tickets Mensuales',
                                data: totalTickets,
                                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                                borderColor: 'rgba(75, 192, 192, 1)',
                                borderWidth: 1
                            }]
                        },
                        options: {
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                            }
                        }
                    });
    
                    // Mostrar el gráfico mensual
                    $('#graficaMensual').show();
                } catch (error) {
                    console.error('Error al procesar datos mensuales:', error);
                }
            },
            error: function (error) {
                console.error('Error al obtener datos mensuales:', error);
            }
        });
    }
    

    // Función para mostrar el gráfico anual
    function mostrarGraficoAnual() {
        // Realizar la solicitud AJAX para obtener los datos anuales
        $.ajax({
            url: '/Administracion/GetYearlyCompletedTickets',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                try {
                    // Acceder a los datos específicos en el formato proporcionado
                    var años = data.$values.map(item => item.year);
                    var totalTickets = data.$values.map(item => item.totalTickets);
    
                    // Crear el gráfico de barras anual
                    var ctxAnual = document.getElementById('graficaAnual').getContext('2d');
    
                    // Destruir el gráfico existente si hay uno
                    if (myChartAnual) {
                        myChartAnual.destroy();
                    }
    
                    myChartAnual = new Chart(ctxAnual, {
                        type: 'bar',
                        data: {
                            labels: años,
                            datasets: [{
                                label: 'Total de Tickets Anuales',
                                data: totalTickets,
                                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                                borderColor: 'rgba(255, 99, 132, 1)',
                                borderWidth: 1
                            }]
                        },
                        options: {
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                            }
                        }
                    });
    
                    // Mostrar el gráfico anual
                    $('#graficaAnual').show();
                } catch (error) {
                    console.error('Error al procesar datos anuales:', error);
                }
            },
            error: function (error) {
                console.error('Error al obtener datos anuales:', error);
            }
        });
    }
    
});
