document.onkeydown = function (e) {
    var event = e || window.event;
    var code = event.keyCode || event.which || event.charCode;
    if (code == 13) {
        login();
    }
}
$(function () {
    $("input[name='txtLoginName']").focus();
});
function cleardata() {
    $('#loginForm').form('clear');
}
function login() {
    if ($("input[name='txtLoginName']").val() == "" || $("input[name='txtPassword']").val() == "") {
        $("#showMsg").html("用户名或密码为空，请输入");
        $("input[name='txtLoginName']").focus();
    } else {
        //ajax异步提交  
        $.ajax({
            url: "../Account/CheckUserLogin",
            type: "POST",
            dataType: "json",
            data: { "UserName": $("#txtLoginName").val(), "UserPassword": $("#txtPassword").val() },
            success: function (data) {
                if (data.result == "success") {
                    window.location.href = "../Home/Index";
                }
                else {
                    alert(data.content);
                    window.location.href = "../Account/Login/";
                }
            },
            error: function (xhr, error, ex) {
                alert("出现错误，请稍后再试，带来不便，敬请谅解");
                window.location.href = "../Account/Login/";
            }
        });
    }
}