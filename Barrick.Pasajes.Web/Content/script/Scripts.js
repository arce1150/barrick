function ValidarFecha(selDia, selMes, selAno) {
    var Ano = selAno;
    var Mes = selMes;
    var Dia = selDia;

    if (Dia > '' && Mes > '' && Ano > '') {
        if (isNaN(Ano) || Ano.length < 4 || parseFloat(Ano) < 1900)
        { return false; }
        if (isNaN(Mes) || parseFloat(Mes) < 1 || parseFloat(Mes) > 12)
        { return false; }
        if (isNaN(Dia) || parseInt(Dia, 10) < 1 || parseInt(Dia, 10) > 31)
        { return false; }
        if (Mes == 4 || Mes == 6 || Mes == 9 || Mes == 11 || Mes == 2) {
            if (Ano % 4 == 0) {
                if (Mes == 2 && Dia > 29 || Dia > 30)
                { return false; }
            }
            else {
                if (Mes == 2 && Dia > 28 || Dia > 30)
                { return false; }
            }
        }
    }
    else {
        if (Dia > '' || Mes > '' || Ano > '')
        { return false }
        else
        { return false }
    }
}

function validarSoloNumeros() {
    if (event.keyCode < 45 || event.keyCode > 57) {
        event.returnValue = false;
    }
}

function protegercodigo() {
    if (event.button == 2 || event.button == 3) {
        alert('Minera Barrick Misquichilca S.A.\n\n Dpto. Contabilidad y Finanzas');
    }
}

function objXHR() {
    var xmlhttp = false;
    try {
        xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
    } catch (e) {
        try {
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        } catch (E) {
            xmlhttp = false;
        }
    }

    if (!xmlhttp && typeof XMLHttpRequest != 'undefined') {
        xmlhttp = new XMLHttpRequest();
    }
    return xmlhttp;
}

function listarDias() {
    var fecha = new Date();
    var dia = fecha.getDate();
    var mes = fecha.getMonth();
    var anio = fecha.getFullYear();
    var lista = new Array();
    for (var i = 1; i <= 31; i++) {
        lista.push({
            valorDia: i
            , textoDia: (i < 10 ? "0" + i : i)
        });
    }
    //switch (mes) {
    //    case 0: case 2: case 4: case 6: case 7: case 9: case 11:
    //        for (var i = 1; i <= 31; i++) {
    //            lista.push({
    //                valorDia: i
    //                ,textoDia: (i<10? "0"+ i:i)
    //            });
    //        }
    //        break;
    //    case 3: case 5: case 8: case 10:
    //        for (var i = 1; i <= 30; i++) {
    //            lista.push({
    //                valorDia: i
    //                , textoDia: (i < 10 ? "0" + i : i)
    //            });
    //        }
    //        break;
    //    case 1:
    //        if (anio % 4 == 0) {
    //            for (var i = 1; i <= 29; i++) {
    //                lista.push({
    //                    valorDia: i
    //                    , textoDia: (i < 10 ? "0" + i : i)
    //                });
    //            }
    //        } else {
    //            for (var i = 1; i <= 28; i++) {
    //                lista.push({
    //                   valorDia: i
    //                    , textoDia: (i < 10 ? "0" + i : i)
    //                });
    //            }
    //        }
    //        break;
    //}
    return lista;
}

function listarMes() {
    var fecha = new Date();
    var mes = fecha.getMonth();
    lista = new Array();
    for (var i = 1; i <= 12 ; i++) {
        switch (i) {
            case 1:
                lista.push({
                    valorMes: i
                    , textoMes: "Ene"
                });
                break;
            case 2:
                lista.push({
                    valorMes: i
                    , textoMes: "Feb"
                });
                break;
            case 3:
                lista.push({
                    valorMes: i
                    , textoMes: "Mar"
                });
                break;
            case 4:
                lista.push({
                    valorMes: i
                    , textoMes: "Abr"
                });
                break;
            case 5:
                lista.push({
                    valorMes: i
                    , textoMes: "May"
                });
                break;
            case 6:
                lista.push({
                    valorMes: i
                    , textoMes: "Jun"
                });
                break;
            case 7:
                lista.push({
                    valorMes: i
                    , textoMes: "Jul"
                });
                break;
            case 8:
                lista.push({
                    valorMes: i
                    , textoMes: "Ago"
                });
                break;
            case 9:
                lista.push({
                    valorMes: i
                    , textoMes: "Set"
                });
                break;
            case 10:
                lista.push({
                    valorMes: i
                    , textoMes: "Oct"
                });
                break;
            case 11:
                lista.push({
                    valorMes: i
                    , textoMes: "Nov"
                });
                break;
            case 12:
                lista.push({
                    valorMes: i
                    , textoMes: "Dic"
                });
                break;
        }

    }
    return lista;
}

function listarAnios() {
    var fecha = new Date();
    var anio = fecha.getFullYear();
    var lista = new Array();
    for (var i = anio; i <= (anio + 1) ; i++) {
        lista.push({
            anioValor: i,
            anioexto: i
        });
    }
    return lista;
}

function ConvertToDateTime(o) { //str=13-09-2015  13.09.2015   25/11/2018
    var parms = o.value.split(/[\.\-\/]/);
    var yyyy = parseInt(parms[2], 10);
    var mm = parseInt(parms[1], 10);
    var dd = parseInt(parms[0], 10);
    var date = null;
    if (o.value != null || o.value != '' || o.value.length > 0)
        date = new Date(yyyy, mm - 1, dd, 0, 0, 0, 0);
    else date = new Date();
    return date;

}
function FormatJSONDate(jsonDate) {

    var dateString = jsonDate.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    var date = (day < 10 ? '0' + day.toString() : day.toString()) +
        "/" + (month < 10 ? '0' + month.toString() : month.toString()) + "/" + year;
    
    if ((parseInt(day * 1) + parseInt(month * 1) + parseInt(year * 1)) == 3) { 
        date = "";
    }
    return (date);
}

 