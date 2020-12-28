var map;
var markersArray = [];

// Initialize and add the map
function clearMarkers() {
    for (var i = 0; i < markersArray.length; i++) {
        markersArray[i].setMap(null);
    }
    markersArray = [];
}
function startTimer(createdMap) {
    //внутри этой функции будет идти обработка
    let lastTimer = $.timer(function () {
        loadData(createdMap);
    });

    lastTimer.set({
        time: 10000, autostart: true
    });
}
function loadData(map) {
    //запрос стартует по факту вызова, но
    $.ajax({
        url: '/api/container/getboxeslocation/',
        type: 'GET',
        contentType: "application/json",
        //результат ты получаешь как бы в отдельном потоке по факту ответа от серверного метода
        success: function (boxes) {
            //map.data.addGeoJson(boxes);
            //и тогда уже продолжаешь работу
            setMarkers(map, boxes);
        }
    });
    //но не здесь, здесь код продолжит выполняться без ожидания ответа от ajax
}

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 2,
        center: new google.maps.LatLng(2.8, -187.3),
        mapTypeId: 'terrain'
    });
    //теперь запускается таймер
    startTimer(map);

}
function setMarkers(map, objects) {
    clearMarkers();
    //используй let вместо var, это объявление локалной переменной
    for (let i = 0; i < objects.length; i++) {
        let object = objects[i];
        let myLatLng = new google.maps.LatLng(object.latitude, object.longitude);

        let smartBoxId = object.smartBoxId;
        let boxName = object.name;
        let boxBatteryPower = object.batteryPower;
        let isOpenedDoor = (object.isOpenedDoor == false) ? "заблокирована" : "разблокирована";
        let boxState = (object.boxState == "1") ? "на складе" : (object.boxState == "2") ? "на автомобиле" : (object.boxState == "3") ?
            "выгружен у грузоотправителя" : (object.boxState == "4") ? "разгружен у грузополучателя" : "не указано";
        let code = (object.code == null) ? "не указан" : object.code;
        let isOpenedBox = (object.isOpenedBox == false) ? "закрыт" : "раскрыт";
        let light = object.light;
        let temperature = object.temperature;
        let weight = object.weight;
        let wetness = object.wetness;

        let marker = new google.maps.Marker({
            position: myLatLng,
            map: map,
            title: 'Click to zoom',
        });
        markersArray.push(marker);
        marker.addListener('click', function () {
            $("#name").val(boxName);
            $("#state").val(boxState);
            $("#isOpenedBox").val(isOpenedBox);
            $("#isOpenedDoor").val(isOpenedDoor);
            $("#code").val(code);
            $("#weight").val(weight);
            $("#temp").val(temperature);
            $("#light").val(light);
            $("#batteryPower").val(boxBatteryPower);
            $("#wetness").val(wetness);
            //alert("Наименование: " + boxName + "\n" +
            //    "Состояние: " + boxState + "\n" +
            //    "Положение: " + isOpenedBox + "\n" +
            //    "Дверь: " + isOpenedDoor + "\n" +
            //    "PIN-код для доступа: " + code + "\n" +
            //    "Вес груза: " + weight + " кг\n" +
            //    "Температура: " + temperature + "°C\n" +
            //    "Датчик света: " + light + "\n" +
            //    "Уровень заряда аккумулятора: " + boxBatteryPower + "%\n" +
            //    "Влажность: " + wetness + "%\n");

        });
    }
}