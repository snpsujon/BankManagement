$(document).ready(function () {
    var model = {};
    $("#LoginBtn").click(function () {

        model = {};
        model.UserName = $("#usernameL").val();
        model.Password = $("#userpasswordL").val()
        var redir = window.location.search.split("=");
        model.redirecturl = redir[1];
        $.post("/login", model, function (data) {
            debugger;
            if (data != 0 && data != null) {
                if (data == 5) {
                    window.location.href = "/uploadpic";
                }
                else {
                    var uu = decodeURIComponent(data);
                    window.location.href = decodeURIComponent(data);
                }

            }
            else {
                window.location.href = "Home/Index";
            }

        });
    });

    $("#RegiBtn").click(function () {

        model = $('#register').serialize();
        $.post("/register", model, function (data) {
            //window.location.href = "home/index";

            window.location.reload();
        });
    });



    //jQuery.fn.serializeObject = function () {
    //    var results = {},
    //        arr = this.serializeArray();
    //    for (var i = 0, len = arr.length; i < len; i++) {
    //        obj = arr[i];
    //        //Check if results have a property with given name
    //        if (results.hasOwnProperty(obj.name)) {
    //            //Check if given object is an array
    //            if (!results[obj.name].push) {
    //                results[obj.name] = [results[obj.name]];
    //            }
    //            results[obj.name].push(obj.value || '');
    //        } else {
    //            results[obj.name] = obj.value || '';
    //        }
    //    }
    //    return results;
    //}

});