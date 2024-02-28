// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Hämta dropdown-länken
var dropdownToggle = document.querySelector('.dropdown-toggle');

// Hämta dropdown-menyn
var dropdownMenu = document.querySelector('.dropdown-menu');

// Lägg till en klickhändelse för dropdown-länken
dropdownToggle.addEventListener('click', function () {
    // Om dropdown-menyn redan är synlig, dölj den
    if (dropdownMenu.style.display === 'block') {
        dropdownMenu.style.display = 'none';
    } else {
        // Annars visa dropdown-menyn
        dropdownMenu.style.display = 'block';
    }
});

// Lägg till en klickhändelse för att dölja dropdown-menyn när användaren klickar någon annanstans på sidan
document.addEventListener('click', function (event) {
    var targetElement = event.target;

    // Om klickhändelsen inte är på dropdown-länken eller dropdown-menyn, dölj dropdown-menyn
    if (!targetElement.matches('.dropdown-toggle') && !targetElement.matches('.dropdown-menu')) {
        dropdownMenu.style.display = 'none';
    }
});

var femaleSignups = @Html.Raw(ViewBag.femaleSignups);
var maleSignups = @Html.Raw(ViewBag.maleSignups);
var Signups16_30 = @Html.Raw(ViewBag.Signups16_30);
var Signups31_50 = @Html.Raw(ViewBag.Signups31_50);
var Signups50plus = @Html.Raw(ViewBag.Signups50plus);
var views = @Html.Raw(ViewBag.Views);
var totalSignups = @Html.Raw(ViewBag.TotalSignups);
var savedEvents = @Html.Raw(ViewBag.SavedEvents);

var ctxGender = document.getElementById('genderChart').getContext('2d');
var genderChart = new Chart(ctxGender, {
    type: 'pie',
    data: {
        labels: ['Kvinnor', 'Män'],
        datasets: [{
            data: [femaleSignups, maleSignups],
            backgroundColor: ['#000000', '#999999']
        }]
    },
    options: {
        responsive: true,
        maintainAspectRatio: false
    }
});

var ctxAge = document.getElementById('ageChart').getContext('2d');
var ageChart = new Chart(ctxAge, {
    type: 'pie',
    data: {
        labels: ['16-30', '31-50', '50+'],
        datasets: [{
            data: [Signups16_30, Signups31_50, Signups50plus],
            backgroundColor: ['#666666', '#000000', '#999999']
        }]
    },
    options: {
        responsive: true,
        maintainAspectRatio: false
    }
});

var ctxViews = document.getElementById('ViewsChart').getContext('2d');
var viewsChart = new Chart(ctxViews, {
    type: 'bar',
    data: {
        labels: [''],
        datasets: [{
            label: 'Visningar',
            data: [views],
            backgroundColor: '#000000',
            borderColor: '#FFFFFF',
            borderWidth: 1
        }, {
            label: 'Sparade',
            data: [savedEvents],
            backgroundColor: '#999999',
            borderColor: '#FFFFFF',
            borderWidth: 1
        },
        {
            label: 'Anmälningar',
            data: [totalSignups],
            backgroundColor: '#e9e9e9',
            borderColor: '#FFF',
            borderWidth: 1
        }
        ]
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

var oneRating = @Html.Raw(Json.Serialize(ViewBag.oneRating));
var twoRating = @Html.Raw(Json.Serialize(ViewBag.twoRating));
var threeRating = @Html.Raw(Json.Serialize(ViewBag.threeRating));
var fourRating = @Html.Raw(Json.Serialize(ViewBag.fourRating));
var fiveRating = @Html.Raw(Json.Serialize(ViewBag.fiveRating));

var ctx = document.getElementById('starRatingsChart').getContext('2d');
var starRatingsChart = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: ['1 Rating', '2 Rating', '3 Rating', '4 Rating', '5 Rating'],
        datasets: [{
            label: 'Antal röster',
            data: [oneRating, twoRating, threeRating, fourRating, fiveRating],
            backgroundColor: ['#999999', '#000000', '#666666', '#F3F3F3', '#CCCCCC'],
            borderColor: ['#000000', '#000000', '#000000', '#000000', '#000000'],
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