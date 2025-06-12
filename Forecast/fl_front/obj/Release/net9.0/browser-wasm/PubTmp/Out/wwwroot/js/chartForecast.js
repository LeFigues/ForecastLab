window.renderForecastCharts = (insumos, practicas, semestre) => {
    // Chart 1: Barras Insumos
    const ctx1 = document.getElementById('chartInsumos');
    new Chart(ctx1, {
        type: 'bar',
        data: {
            labels: insumos.map(x => x.insumoNombre),
            datasets: [{
                label: 'Total Pronosticado',
                data: insumos.map(x => x.totalPronosticado),
            }]
        },
        options: {
            indexAxis: 'y',
            responsive: true,
            plugins: {
                legend: { display: false }
            }
        }
    });

    // Chart 2: Pastel de Prácticas
    const ctx2 = document.getElementById('chartPracticas');
    new Chart(ctx2, {
        type: 'pie',
        data: {
            labels: practicas.map(x => x.practica),
            datasets: [{
                label: 'Total Insumos',
                data: practicas.map(x => x.totalInsumos)
            }]
        }
    });

    // Chart 3: Stock vs Pronóstico
    const ctx3 = document.getElementById('chartSemestre');
    new Chart(ctx3, {
        type: 'bar',
        data: {
            labels: semestre.map(x => x.insumoNombre),
            datasets: [
                {
                    label: 'Stock Actual',
                    data: semestre.map(x => x.stockActual),
                    backgroundColor: 'rgba(54, 162, 235, 0.7)'
                },
                {
                    label: 'Pronóstico Semestral',
                    data: semestre.map(x => x.totalPronosticadoSemestre),
                    backgroundColor: 'rgba(255, 99, 132, 0.7)'
                }
            ]
        },
        options: {
            responsive: true,
            plugins: {
                legend: { position: 'top' }
            }
        }
    });
};
