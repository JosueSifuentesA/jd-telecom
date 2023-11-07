/*$(document).ready(function () {
    $.ajax({
        url: '/Administracion/GetAllMontlyCompletedVouchers',
        type: 'GET',
        success: function (data) {
            var ctx = document.getElementById('graficaRecibosPagadosPorMes').getContext('2d');

            var labels = data.map(function (item) {
                return item.mes_recibo;
            });

            var montos = data.map(function (item) {
                return item.monto_recibo;
            });

            var chartData = {
                labels: labels,
                datasets: [{
                    label: 'Monto Recibos Pagados',
                    data: montos,
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1
                }]
            };

            var chartOptions = {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            };

            var myChart = new Chart(ctx, {
                type: 'line',
                data: chartData,
                options: chartOptions
            });
        }
    });
});*/

$(document).ready(function () {
    // Hacer una solicitud al servidor para obtener los datos
    $.ajax({
        url: '/Administracion/GetAllMontlyCompletedVouchers',
        type: 'GET',
        success: function (data) {
            var ctx = document.getElementById('graficaRecibosPagadosPorMes').getContext('2d');

            var labels = data.map(function (item) {
                return item.mes_recibo;
            });

            var montos = data.map(function (item) {
                return item.monto_recibo;
            });

            var backgroundColors = generateRandomColors(data.length);
            
            var chartData = {
                labels: labels,
                datasets: [{
                    label: 'Monto Recibos Pagados',
                    data: montos,
                    backgroundColor: backgroundColors,
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1
                }]
            };

            var chartOptions = {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            };

            var myChart = new Chart(ctx, {
                type: 'line',
                data: chartData,
                options: chartOptions
            });

            // Escuchar eventos para cambiar entre tipos de gráficos
            $('#lineChartButton').click(function () {
                updateChart('line');
            });
            $('#pieChartButton').click(function () {
                updateChart('pie');
            });
            $('#barChartButton').click(function () {
                updateChart('bar');
            });

            // Función para actualizar el tipo de gráfico
            function updateChart(chartType) {
                if (myChart) {
                    myChart.destroy(); // Destruir el gráfico existente
                }

                // Crear un nuevo gráfico con el tipo seleccionado
                myChart = new Chart(ctx, {
                    type: chartType,
                    data: chartData,
                    options: chartOptions
                });
            }

            function generateRandomColors(count) {
                var colors = [];
                for (var i = 0; i < count; i++) {
                    var color = 'rgba(' + getRandomInt(0, 255) + ',' + getRandomInt(0, 255) + ',' + getRandomInt(0, 255) + ', 0.2)';
                    colors.push(color);
                }
                return colors;
            }

            function getRandomInt(min, max) {
                return Math.floor(Math.random() * (max - min + 1)) + min;
            }




        }
    });
});