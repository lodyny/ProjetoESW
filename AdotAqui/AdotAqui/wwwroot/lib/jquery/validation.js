function compare() {
  var password = $("#Password");
  var password_confirm = $("#ConfirmPassword");
  var both = $("#Password,#ConfirmPassword")
  result = $(".result");

  if (password.val() == password_confirm.val()) {
    result.text( "✔" );
	result.css({"margin-top" : "-32px", "margin-right" : "5px", "color" : "green", "float" : "right" });
	both.css("border-color", "green");
  } else {
	result.text( "✘" );
	result.css({"margin-top" : "-32px", "margin-right" : "5px", "color" : "red", "float" : "right"});
	both.css("border-color", "rgb(220, 53, 69)");
  }
}

$(function(){
    $("#Password,#ConfirmPassword").focusin(function(){
        $("#Password").css("border-color", "");
        $("#ConfirmPassword").css("border-color", "");
		result.text( "" );
    });
});

$(function(){
    $("#ConfirmPassword").focusout(function(){
        if ($("#Password").val().length > 1){
        compare();
		}
    });
});

$(function(){
    $("#Password").focusout(function(){
		if ($("#ConfirmPassword").val().length > 1){
        compare();
		}
    });
});