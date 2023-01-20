function fngetformatDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    var vmonth = (dt.getMonth() + 1);
    var vday = dt.getDate();
    var vhour = dt.getHours();
    var vmin = dt.getMinutes();
    var vsec = dt.getSeconds();
    if (vmonth < 10) { vmonth = "0" + vmonth; }
    if (vday < 10) { vday = "0" + vday; }
    if (vhour < 10) { vhour = "0" + vhour; }
    if (vmin < 10) { vmin = "0" + vmin; }
    if (vsec < 10) { vsec = "0" + vsec; }
    return vday + "/" + vmonth + "/" + dt.getFullYear() + " " + vhour + ":" + vmin + ":" + vsec;
}

//compare dates function
function fncompareDates(ini, fin) {
    var result = false;
    var startDay = ini.split('/')[0],
        startMonth = ini.split('/')[1],
        startYear = ini.split('/')[2].split(' ')[0],
        startHour = ini.split(' ')[1].split(':')[0],
        startMin = ini.split(' ')[1].split(':')[1],
        startSec = ini.split(' ')[1].split(':')[2];

    var endDay = fin.split('/')[0],
        endMonth = fin.split('/')[1],
        endYear = fin.split('/')[2].split(' ')[0],
        endHour = fin.split(' ')[1].split(':')[0],
        endMin = fin.split(' ')[1].split(':')[1],
        endSec = fin.split(' ')[1].split(':')[2];
    var startDate = new Date(startYear, startMonth, startDay, startHour, startMin, startSec);
    var endDate = new Date(endYear, endMonth, endDay, endHour, endMin, endSec);
    if (endDate < startDate) {
        return result;
    }
}

function fnSetFileName() {
    var dateNow = new Date();
    var yearNow = dateNow.getFullYear();
    var monthNow = dateNow.getMonth();
    var dayNow = dateNow.getDate();
    var hourNow = dateNow.getHours();
    var minNow = dateNow.getMinutes();
    var secNow = dateNow.getSeconds();
    var millsecNow = dateNow.getMilliseconds();
    return { yearNow, monthNow, dayNow, hourNow, minNow, secNow, millsecNow };
}