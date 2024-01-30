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
            type: "POST",   //post提交方式默认是get
            url: "Home/CheckUserLogin",
            data: $("#loginForm").serialize(),   //序列化               
            error: function (request) {      // 设置表单提交出错                 
                $("#showMsg").html(request);  //登录错误提示信息
            },
            success: function (data) {
                document.location = "Home/Index";
            }
        });
    }
}