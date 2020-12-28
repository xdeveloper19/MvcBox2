$(document).ready(function () {
    $("#registerBox").hide();
    $("#hideLogin").click(function () {
        $("#registerBox").show();
        $("#loginBox").hide();
    });
    $("#hideRegister").click(function () {
        $("#loginBox").show();
        $("#registerBox").hide();
    });

    // Авторизация
    function Login(userEmail, userPass) {
        $.ajax({
            url: "../../api/auth/signin?email=" + userEmail + "&password=" + userPass,
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                email: userEmail,
                password: userPass
            }),
            success: function (html) {
                document.write(html);
                window.location.replace("https://localhost:44396/");
            },
            error: function (jxqr, error, status) {
                // парсинг json-объекта
                console.log(jxqr);
                if (jxqr.responseText === "") {
                    $('#errors').html("<h3>" + jxqr.statusText + "</h3>");
                }
                else {
                    var response = JSON.parse(jxqr.responseText);
                    // добавляем общие ошибки модели
                    if (response['']) {

                        $.each(response[''], function (index, item) {
                            $('#errors').html("<p>" + item + "</p>");
                        });
                    }
                    // добавляем ошибки свойства Name
                    if (response['Email']) {

                        $.each(response['Email'], function (index, item) {
                            $('#errors').html("<p>" + item + "</p>");
                        });
                    }
                    // добавляем ошибки свойства Age
                    if (response['Password']) {
                        $.each(response['Password'], function (index, item) {
                            $('#errors').html("<p>" + item + "</p>");
                        });
                    }
                }

                $('#errors').show();
            }
        })
    }


    //Регистрация
    function Register(userEmail, userFirstName, userLastName, userPass, userPassConfirm, Role) {
        $.ajax({
            url: "../../api/auth/signup?email=" + userEmail + "&firstName=" + userFirstName + "&lastName=" + userLastName + "&password=" + userPass + "&passwordConfirm=" + userPassConfirm + "&RoleName=" + Role,
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                email: userEmail,
                firstName: userFirstName,
                lastName: userLastName,
                password: userPass,
                passwordConfirm: userPassConfirm,
                RoleName: Role
            }),
            success: function (html) {
                document.write(html);
                window.location.replace("https://localhost:44396/");
            },
            error: function (jxqr, error, status) {
                // парсинг json-объекта
                console.log(jxqr);
                if (jxqr.responseText === "") {
                    $('#errorsRegister').html("<h3>" + jxqr.statusText + "</h3>");
                }
                else {
                    var response = JSON.parse(jxqr.responseText);
                    // добавляем общие ошибки модели
                    if (response['']) {

                        $.each(response[''], function (index, item) {
                            $('#errorsRegister').html("<p>" + item + "</p>");
                        });
                    }

                    // добавляем ошибки свойства Email
                    if (response['Email']) {

                        $.each(response['Email'], function (index, item) {
                            $('#errorsRegister').html("<p>" + item + "</p>");
                        });
                    }

                    // добавляем ошибки свойства FirstName
                    if (response['FirstName']) {
                        $.each(response['FirstName'], function (index, item) {
                            $('#errorsRegister').html("<p>" + item + "</p>");
                        });
                    }

                    // добавляем ошибки свойства LastName
                    if (response['LastName']) {
                        $.each(response['LastName'], function (index, item) {
                            $('#errorsRegister').html("<p>" + item + "</p>");
                        });
                    }

                    // добавляем ошибки свойства Password
                    if (response['Password']) {
                        $.each(response['Password'], function (index, item) {
                            $('#errorsRegister').html("<p>" + item + "</p>");
                        });
                    }
                    // добавляем ошибки свойства PasswordConfirm
                    if (response['PasswordConfirm']) {
                        $.each(response['PasswordConfirm'], function (index, item) {
                            $('#errorsRegister').html("<p>" + item + "</p>");
                        });
                    }

                    // добавляем ошибки свойства UserRole
                    if (response['UserRole']) {
                        $.each(response['UserRole'], function (index, item) {
                            $('#errorsRegister').html("<p>" + item + "</p>");
                        });
                    }
                }

                $('#errorsRegister').show();
            }
        })
    }

    //Отправка формы авторизации
    $("#btnLogin").click(function (e) {
        e.preventDefault(e);
        $('#errors').empty();
        $('#errors').hide();
        var email = $("#exampleInputEmail1").val();
        var password = $("#exampleInputPassword1").val();
        Login(email, password);
    });

    //отправка формы регистрации
    $("#btnRegister").click(function (e) {
        e.preventDefault(e);
        $('#errorsRegister').empty();
        $('#errorsRegister').hide();
        var email = $("#exampleInputEmail2").val();
        var firstName = $("#inputFirstName").val();
        var lastName = $("#inputLastName").val();
        var password = $("#exampleInputPassword2").val();
        var passwordConfirm = $("#exampleInputConfirmPassword1").val();
        var roleName = $("#inputGroupRoleSelect01").val();
        Register(email, firstName, lastName, password, passwordConfirm, roleName);
    });
});

