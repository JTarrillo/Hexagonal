$(function () {
  fncreatefeedbackToUser("Cargando dashboard...",1);
  fnDefaultDate("#min_date_users", "#max_date_users");
  filter();
  $("#btnFIlter").on("click", function () {
    filter();
  });

  function chart_1(values) {
    var options = {
      series: [
        {
          name: "Cantidad",
          data: values,
        },
      ],
      chart: {
        type: "area",
        height: 450,
      },
      dataLabels: {
        enabled: false,
      },
      xaxis: {
        type: "datetime",
      },
      yaxis: {
        opposite: false,
      },
      title: {
        text: "Cupones generados",
      },
      style: {
        colors: ["#0050B5"],
      },
      annotations: {
        xaxis: [
          {
            x: new Date("05 Dec 2019").getTime(),
            borderColor: "#999",
            yAxisIndex: 0,
            label: {
              show: true,
              text: "Lanzamiento",
              style: {
                color: "#fff",
                background: "#0050B5",
              },
            },
          },
        ],
      },
      stroke: {
        width: 3,
        curve: "straight",
      },
      labels: ["sholi", "sdada"],
      fill: {
        type: "gradient",
        gradient: {
          shadeIntensity: 1,
          opacityFrom: 0.8,
          opacityTo: 0,
          colorStops: [
            [
              { offset: 0, color: "#0050B5", opacity: 1 },
              { offset: 0.6, color: "#0050B5", opacity: 0.6 },
              { offset: 100, color: "#0050B5", opacity: 0 },
            ],
          ],
          stops: [50, 100],
        },
      },
    };
    document.getElementById("chart-timeline").innerHTML = "";
    var chart = new ApexCharts(
      document.querySelector("#chart-timeline"),
      options
    );
    chart.render();
  }
  function chart_2(values) {
    var options2 = {
      colors: ["#0050B5", "#E31C79", "#FF6900", "#511E84", "#C2DF0A"],
      series: [
        {
          name: "Salud",
          data: values.bien,
        },
        {
          name: "Nutrición",
          data: values.nutr,
        },
        {
          name: "Energía",
          data: values.rend,
        },
        {
          name: "Tranquilidad",
          data: values.rela,
        },
        {
          name: "Experiencias",
          data: values.entr,
        },
      ],
      title: {
        text: "Cupones generados por categoría",
      },
      chart: {
        height: 450,
        type: "area",
      },
      dataLabels: {
        enabled: false,
      },
      fill: {
        type: "gradient",
        gradient: {
          shadeIntensity: 1,
          opacityFrom: 0.8,
          opacityTo: 0,
          colorStops: [
            [
              { offset: 0, color: "#005D83", opacity: 0.6 },
              { offset: 0.4, color: "#005D83", opacity: 0.2 },
              { offset: 100, color: "#005D83", opacity: 0 },
            ],
            [
              { offset: 0, color: "#FF9E18", opacity: 0.6 },
              { offset: 0.4, color: "#FF9E18", opacity: 0.2 },
              { offset: 100, color: "#FF9E18", opacity: 0 },
            ],
            [
              { offset: 0, color: "#C2D500", opacity: 0.6 },
              {
                offset: 0.4,
                color: "#C2D500",
                opacity: 0.2,
              },
              { offset: 100, color: "#C2D500", opacity: 0 },
            ],
            [
              { offset: 0, color: "#9164CC", opacity: 0.6 },
              { offset: 0.4, color: "#9164CC", opacity: 0.2 },
              { offset: 100, color: "#00ABC8", opacity: 0 },
            ],
            [
              { offset: 0, color: "#00ABC8", opacity: 0.6 },
              { offset: 0.4, color: "#00ABC8", opacity: 0.2 },
              { offset: 100, color: "#00ABC8", opacity: 0 },
            ],
          ],
          stops: [50, 100],
        },
      },
      stroke: {
        width: [2, 2, 2, 2, 2],
        curve: "smooth",
      },
      xaxis: {
        type: "datetime",
        categories: values.days,
      },
      tooltip: {
        x: {
          format: "dd MMM yyyy",
        },
      },
    };
    document.getElementById("chart").innerHTML = "";
    var chart = new ApexCharts(document.querySelector("#chart"), options2);
    chart.render();
  }
  function chart_3(values) {
    var options3 = {
      series: [
        {
          name: "Generados",
          type: "column",
          data: values.generated,
        },
        {
          name: "Usados",
          type: "line",
          data: values.used,
        },
      ],
      chart: {
        height: 450,
        type: "line",
      },
      colors: ["#0050B5", "#C2DF0A"],

      stroke: {
        curve: "smooth",
        width: [0, 2],
      },

      title: {
        text: "Cupones generados y usados",
      },
      dataLabels: {
        enabled: false,
        enabledOnSeries: [1],
      },
      labels: values.days,
      xaxis: {
        type: "datetime",
      },
    };
    document.getElementById("alliance").innerHTML = "";
    var chart = new ApexCharts(document.querySelector("#alliance"), options3);
    chart.render();
  }

  function filter() {
    $("#loading").addClass("flex");
    var fi = $("#min_date_users").val();
    var ff = $("#max_date_users").val();
    var ch = $("#channels").val();
    var datas = { start: fi, end: ff, channel: ch };
    $.ajax({
      method: "POST",
      contentType: "application/json; charset=utf-8",
      data: JSON.stringify(datas),
      url: "../Home/DashboardCounters",
      success: function (response) {
        if (response.STATUS) {
          document.getElementById("div-UsersCount").innerHTML = "";
          document.getElementById("div-UsersCount").innerHTML =
            response.CounterUserAll;

            document.getElementById("div-UsersActive").innerHTML = "";
            document.getElementById("div-UsersActive").innerHTML = response.CounterUserActive;

          document.getElementById("div-ScheduleCount").innerHTML = "";
          document.getElementById("div-ScheduleCount").innerHTML =
            response.CounterSchedule;

          document.getElementById("counter_session").innerHTML = "";
          document.getElementById("counter_session").innerHTML =
            response.CounterSession;

          document.getElementById("counter_coupons_generated").innerHTML = "";
          document.getElementById("counter_coupons_generated").innerHTML =
            response.CounterCouponGenerated;

          document.getElementById("counter_coupon_approved").innerHTML = "";
          document.getElementById("counter_coupon_approved").innerHTML =
            response.CounterCouponApproved;

          document.getElementById("counter_user_updateinfo").innerHTML = "";
          document.getElementById("counter_user_updateinfo").innerHTML =
                response.CounterUserUpdateInfo;

            document.getElementById("div-UsersPlataform").innerHTML = "";
            document.getElementById("div-UsersPlataform").innerHTML = ' <i class="fab fa-android text-success"></i> : ' + response.CounterUserAndroid + '<br/>' + '<i class="fab fa-apple"></i> : ' + response.CounterUserIos;

          chart_1(response.chart__1);
          chart_2(response.chart__2);
          chart_3(response.chart__3);
        } else {
          console.log(response);
        }
      },
      error: function (err) {
        console.log(err);
      },
      complete: function () {
        $("#loading").removeClass("flex");
      },
    });
  }
});
