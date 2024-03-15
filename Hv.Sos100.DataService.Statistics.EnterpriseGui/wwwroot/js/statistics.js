// Funktion för att uppdatera statistiken
function updateStatistics() {
    var selectedEventId = document.getElementById('events').value;
    // Gör ett AJAX-anrop till ditt API för att hämta statistiken för det valda evenemanget
    fetch('/api/statistics/' + selectedEventId)
        .then(response => {
            if (!response.ok) {
                throw new Error('Något gick fel vid hämtning av statistik');
            }
            return response.json();
        })
        .then(data => {
            // Uppdatera din statistik UI med den hämtade datan
            document.getElementById('statistics').innerText = 'Statistik för ' + data.eventName + ': ' + data.count;
        })
        .catch(error => {
            console.error('Fel vid hämtning av statistik:', error);
        });
}

        var ctxGender = document.getElementById('GenderChart').getContext('2d');
        var genderChart = new Chart(ctxGender, {
            type: 'pie',
            data: {
                labels: ['Kvinnor', 'Män'],
                datasets: [{
                    data: [50, 70], // Hårdkodade värden för kvinnliga och manliga anmälningar
                    backgroundColor: ['#000000', '#999999']
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false
            }
        });

        // Skapa diagram för åldersgrupper
        var ctxAge = document.getElementById('AgeChart').getContext('2d');
        var ageChart = new Chart(ctxAge, {
            type: 'pie',
            data: {
                labels: ['Under 16', '16-30', '31-50', 'Över 50'],
                datasets: [{
                    data: [20, 23, 56, 43],
                    backgroundColor: ['#666666', '#000000', '#999999', '#CCCCCC']
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false
            }
        });

        // Skapa diagram för antal anmälningar
        var ctxViews = document.getElementById('SignupChart').getContext('2d');
        var viewsChart = new Chart(ctxViews, {
            type: 'bar',
            data: {
                labels: ['Totalt antal anmälningar'],
                datasets: [{
                    label: 'Totala anmälningar',
                    data: [selectedEvent.totalSignups],
                    backgroundColor: '#e9e9e9',
                    borderColor: '#FFF',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });

        // Skapa diagram för kategori av evenemang
        var ctxCategory = document.getElementById('EventCategoryChart').getContext('2d');
        var categoryChart = new Chart(ctxCategory, {
            type: 'bar',
            data: {
                labels: ['Kategori'],
                datasets: [{
                    label: 'Antal anmälningar per kategori',
                    data: [selectedEvent.category],
                    backgroundColor: ['#F3F3F3'],
                    borderColor: '#000',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true,
                            stepSize: 1
                        }
                    }]
                }
            }
        });
    

