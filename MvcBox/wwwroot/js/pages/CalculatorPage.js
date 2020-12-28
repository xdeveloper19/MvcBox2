var map;
var directionsService;
var directionsRenderer;
var inputValues = [];

$(document).ready(function () {
    //$(function () {
        $('[data-toggle="tooltip"]').tooltip()
   // })


    $('#inputLength').on('input', function () {
        inputValues[0] = $(this).val();
        volumeComputation(inputValues);
    });

    $('#inputWidth').on('input', function () {
        inputValues[1] = $(this).val();
        volumeComputation(inputValues);
    });

    $('#inputHeight').on('input', function () {
        inputValues[2] = $(this).val();
        volumeComputation(inputValues);
    });

    $('#inputVolume').on('input', function () {
        $("#inputLength").val("");
        $("#inputWidth").val("");
        $("#inputHeight").val("");

        for (var i = 0; i < inputValues.length; i++) {
            inputValues[i] = "";
        }
    });

    $("#btnSubmitValue").unbind().bind('click', function (e) {

        $('#errorsPrice').empty();
        $('#errorsPrice').hide();

        var forms = document.getElementsByClassName('needs-validation');

        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {

            if (form.checkValidity() === false) {
                e.preventDefault();
                e.stopPropagation();
            }
            else {
                initDirectionsService();
            }
            form.classList.add('was-validated');

        });
        //$('#errorsRegister').empty();
        //$('#errorsRegister').hide();
    });

    $("#btnToApply").click(function (e) {
        e.preventDefault(e);
        $('#errorsOrder').empty();
        $('#errorsOrder').hide();

        // Disable form submissions if there are invalid fields
        (function () {
            'use strict';
            window.addEventListener('load', function () {
                // Get the forms we want to add validation styles to
                var forms = document.getElementsByClassName('needs-validation');
                // Loop over them and prevent submission
                var validation = Array.prototype.filter.call(forms, function (form) {
                    form.addEventListener('submit', function (event) {
                        if (form.checkValidity() === false) {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                        form.classList.add('was-validated');
                    }, false);
                });
            }, false);
        })();  
        //let origin = $("#inputTime").val();
        //let destination = $("#inputDate").val();
        //let origin = $("#inputLength").val();
        //let origin = $("#inputWidth").val();
        //let origin = $("#inputHeight").val();
        //let origin = $("#inputVolume").val();
        //let origin = $("#InputCountPlaces").val();
        //let origin = $("#inputFromCity").val();
        //let origin = $("#inputToCity").val();
        //let origin = $("#InputWeight").val();
        //let origin = $("#CustomSelectCargo").val();
        //let origin = $("#CustomSelectDangerClass").val();
        //let origin = $("#exampleCheck2").val();
        //let origin = $("#InputValueCarge").val();
        //let origin = $("#CustomSelectShipLoad").val();

        //orderRegistration();
    });

})


function volumeComputation(objects) {
    for (var i = 0; i < objects.length; i++) {
        if (objects[i] === "")
            return;
    }
    let volume = objects[0] * objects[1] * objects[2];
    $("#inputVolume").val(volume.toFixed(2));
}



function priceComputation(total, cweight, isInsured1, value, cargeType1, dangerClass) {
    $.ajax({
        url: "../../api/container/pricecomputation?length=" + total + "&weight=" + cweight + "&isInsured=" + isInsured1 + "&cargeValue=" + value + "&cargeType=" + cargeType1 + "&dangerClassType=" + dangerClass,
        contentType: "application/json",
        method: "GET",
        cache: false,
        data: JSON.stringify({
            length: total,
            weight: cweight,
            isInsured: isInsured1,
            cargeValue: value,
            cargeType: cargeType1,
            dangerClassType: dangerClass
        }),
        success: function (response) {
            if (response.status == "0") {
                $("#OutputResult").val(response.responseData.price);
            }
            else {
                $('#errorsPrice').html("<p>" + response.message + "</p>");
                $('#errorsPrice').show();
            }
        },
        error: function (jxqr, error, status) {
            // парсинг json-объекта
            console.log(jxqr);
            if (jxqr.responseText === "") {
                $('#errorsPrice').html("<h3>" + jxqr.statusText + "</h3>");
            }
            $('#errorsPrice').show();
        }
    })
}

function orderRegistration() {

    //$.ajax({
    //    url: "../../api/container/orderRegistration?length=" + total + "&weight=" + cweight + "&isInsured=" + isInsured1 + "&cargeValue=" + value + "&cargeType=" + cargeType1 + "&dangerClassType=" + dangerClass,
    //    contentType: "application/json",
    //    method: "GET",
    //    data: JSON.stringify({
    //        length: total,
    //        weight: cweight,
    //        isInsured: isInsured1,
    //        cargeValue: value,
    //        cargeType: cargeType1,
    //        dangerClassType: dangerClass
    //    }),
    //    success: function (response) {
    //        if (response.status == "0") {
    //            $("#OutputResult").val(response.responseData.price);
    //        }
    //        else {
    //            $.each(response.responseData.errors, function (index, item) {
    //                $('#errorsPrice').append("<p>" + item + "</p>");
    //            });
    //            $('#errorsPrice').show();
    //        }
    //    },
    //    error: function (jxqr, error, status) {
    //        // парсинг json-объекта
    //        console.log(jxqr);
    //        if (jxqr.responseText === "") {
    //            $('#errorsPrice').append("<h3>" + jxqr.statusText + "</h3>");
    //        }
    //        $('#errorsPrice').show();
    //    }
    //})
}

function initMap() {
    map = new google.maps.Map(document.getElementById('mapDistance'), {
        zoom: 7,
        center: { lat: 47.411918, lng: 40.104163 }
    });
    directionsService = new google.maps.DirectionsService();
    directionsRenderer = new google.maps.DirectionsRenderer(); 

    inputValues = new Array(3);
    var inputOrigin = document.getElementById('inputFromCity');
    var inputDestination = document.getElementById('inputToCity');

    var autocompleteOrigin = new google.maps.places.Autocomplete(inputOrigin);
    var autocompleteDestination = new google.maps.places.Autocomplete(inputDestination);
    
    //autocompleteDestination.bindTo('bounds', map);
    var outputDistance = document.getElementById('outputTotal');
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(outputDistance);
    //// Specify just the place data fields that you need.
    //autocomplete.setFields(['place_id', 'geometry', 'name']);

    
    //map.controls[google.maps.ControlPosition.TOP_LEFT].push(inputDestination);
    //document.getElementById('inputFromCity').addEventListener('change', onChangeHandler);
    //document.getElementById('inputToCity').addEventListener('change', onChangeHandler);
}

function initDirectionsService() {
    directionsRenderer.setMap(null);
    directionsRenderer.setMap(map);

    let origin = $("#inputFromCity").val();
    let destination = $("#inputToCity").val();

    displayRoute(origin, destination, directionsService,
        directionsRenderer);

    directionsRenderer.addListener('directions_changed', function () {
        computeTotalDistance(directionsRenderer.getDirections());
    });
}

function displayRoute(origin, destination, directionsService, directionsRenderer) {
    directionsService.route(
        {
            origin: origin,
            destination: destination,
            travelMode: 'DRIVING'
        },
        function (response, status) {
            if (status === 'OK') {
                directionsRenderer.setDirections(response);
            } else {
                $('#errorsPrice').html("<p>" + "Введите корректный маршрут" + "</p>");
                $('#errorsPrice').show();
            }
        });
}

function computeTotalDistance(result) {
    var total = 0;
    var myroute = result.routes[0];
    for (var i = 0; i < myroute.legs.length; i++) {
        total += myroute.legs[i].distance.value;
    }
    total = total / 1000;

    //let height = $("#inputLength").val();
    //let width = $("#InputWidth").val();
    //let length = $("#InputHeight").val();

    //$("#InputVolume").val(height * width * length);
    let weight = $("#InputWeight").val();
    let isInsured = $("#exampleCheck2").is(":checked");
    let value = $("#InputValueCarge").val();
    let cargeType = $("#CustomSelectCargo").val();
    let dangerClass = $("#CustomSelectDangerClass").val();
    $("#outputTotal").val(total + " км");
    priceComputation(total, weight, isInsured, value, cargeType, dangerClass);
}