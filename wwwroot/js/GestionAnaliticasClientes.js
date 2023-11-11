$(document).ready(function() {

    const urlClientesFelices = '/Administracion/GetAllReviewsByPlan';
    const urlReclamosMes = '/Administracion/GetAllComplaints'
    //const token = 'TU_TOKEN_DE_AUTORIZACION';
  
    $('#mostrarBtn').on('click', function() {
        const tipoGraficoSeleccionado = $('#tipoGrafico').val();
    
        if (tipoGraficoSeleccionado === 'clientesFelices') {

          $.ajax({
            url: urlClientesFelices,
            type: 'GET',
            dataType: 'json',
            headers: {
              'Content-Type': 'application/json',
              //'Authorization': 'Bearer ' + token,
            },
            success: function(data) {
              // Procesar los datos y crear el gráfico
              const labels = data.map(plan => plan.NombrePlan);
              const valores = data.map(plan => plan.ReseñasPositivas);
    
              // Crear el gráfico de barras
              const ctx = document.getElementById('graficoReseñas').getContext('2d');
              const myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                  labels: labels,
                  datasets: [{
                    label: 'Reseñas Positivas',
                    data: valores,
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1,
                  }],
                },
                options: {
                  scales: {
                    y: {
                      beginAtZero: true,
                    },
                  },
                },
              });
            },
            error: function(error) {
              console.error('Error al obtener reseñas:', error);
            }
          });
        } else if (tipoGraficoSeleccionado === 'reclamosMes') {
            $.ajax({
                url: urlReclamosMes,
                type: 'GET',
                dataType: 'json',
                headers: {
                  'Content-Type': 'application/json',
                  //'Authorization': 'Bearer ' + token,
                },
                success: function(data) {
                  // Procesar los datos y crear el gráfico
                  const meses = data.map(item => `${item.Mes}/${item.Año}`);
                  const cantidades = data.map(item => item.CantidadReclamaciones);
        
                  // Crear el gráfico de barras para reclamaciones por mes
                  const ctx = document.getElementById('graficoReclamaciones').getContext('2d');
                  const myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                      labels: meses,
                      datasets: [{
                        label: 'Reclamaciones por Mes',
                        data: cantidades,
                        backgroundColor: 'rgba(255, 99, 132, 0.2)',
                        borderColor: 'rgba(255, 99, 132, 1)',
                        borderWidth: 1,
                      }],
                    },
                    options: {
                      scales: {
                        y: {
                          beginAtZero: true,
                        },
                      },
                    },
                  });
                },
                error: function(error) {
                  console.error('Error al obtener reclamaciones por mes:', error);
                }
            });
        }
      });
  });