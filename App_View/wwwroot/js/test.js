import themeColors from '~/Admin_assets/es6/constant/theme-constant'

chart() {
    //Stacked Area Chart
    const stackedAreaChart = document.getElementById("stacked-area-chart");
    const stackedAreaCtx = stackedAreaChart.getContext('2d');
    stackedAreaChart.height = 120;
    const stackedAreaConfig = new Chart(stackedAreaCtx, {
        type: 'line',
        data: {
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [{
                label: 'Series A',
                backgroundColor: themeColors.blueLight,
                borderColor: themeColors.blue,
                pointBackgroundColor: themeColors.blue,
                pointBorderColor: themeColors.white,
                pointHoverBackgroundColor: themeColors.blueLight,
                pointHoverBorderColor: themeColors.blueLight,
                data: [65, 59, 80, 81, 56, 55, 40]
            }]
        },
        options: {
            responsive: true,
            hover: {
                mode: 'nearest',
                intersect: true
            },
            elements: {
                line: {
                    tension: 0.5
                },
                point: {
                    radius: 0
                }
            },
            scales: {
                xAxes: [{
                    gridLines: [{
                        display: false,
                    }],
                    ticks: {
                        fontColor: themeColors.grayLight,
                        display: true,
                        beginAtZero: true,
                        fontSize: 13,
                        padding: 10
                    }
                }],
                yAxes: [{
                    gridLines: {
                        drawBorder: false,
                        drawTicks: false,
                        borderDash: [3, 4],
                        zeroLineWidth: 1,
                        zeroLineBorderDash: [3, 4],
                        scaleLabel: {
                            display: false,
                            labelString: 'Value'
                        }
                    },
                    ticks: {
                        max: 100,
                        stepSize: 20,
                        display: true,
                        beginAtZero: true,
                        fontColor: themeColors.grayLight,
                        fontSize: 13,
                        padding: 10
                    }
                }],
            }
        }
    });
}