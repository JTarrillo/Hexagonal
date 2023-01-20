let myChart;
let mycanvas;

$(function () {
    fnchartInit();
});

function fnchartInit() {
    mycanvas = document.getElementById("mycanvas");
    myChart = new Chart(mycanvas, {
        type: "bar",
        data: {
            labels: [],
            datasets: [{
                data: [],
                borderWidth: 1,
                borderColor: '#00c0ef',
                backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                label: ' Cantidad de cupones',
            }]
        },
        options: {
            responsive: true,
            title: {
                display: true,
                text: "Cupones redimidos (por fecha)",
            },
            legend: {
                display: false
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        userCallback: function (label, index, labels) {
                            //evita decimales en el y
                            if (Math.floor(label) === label) {
                                return label;
                            }
                        }
                    }
                }]
            }
        }
    });
}


function removeData(chart) {
    chart.data.labels.pop();
    chart.data.datasets.forEach((dataset) => {
        dataset.data.pop();
    });
    chart.update();
}

function addData(chart, label, data) {
    chart.data.labels.push(label);
    chart.data.datasets.forEach((dataset) => {
        dataset.data.push(data);
    });
    chart.update();
}

function fnloadChart(fi, ff) {

    var obj = {
        "fi": fi,
        "ff": ff
    };

    $.ajax({
        method: "GET",
        url: "../CategoryCodeQr/GetChart",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: obj,
        success: function (d) {
            myChart.destroy();
            fnchartInit();
            for (var i = 0; i < d.length; i++) {
                myChart.data.labels.push(moment(d[i].fecha).format("DD/MM/YYYY"));
                myChart.data.datasets[0].data.push(d[i].num_cupones);
                myChart.update();
            }
           
        }
    });
}