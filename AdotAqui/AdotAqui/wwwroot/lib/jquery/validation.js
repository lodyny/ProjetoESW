function validatepw() {
  var password = $("#Password");
  var password_confirm = $("#ConfirmPassword");
  var both = $("#Password,#ConfirmPassword")
  result = $(".result_pw");

  if (password.val() == password_confirm.val()) {
    result.text( "✔" );
	result.css({"margin-top" : "-32px", "margin-right" : "5px", "color" : "green", "float" : "right" });
	both.css("border-color", "green");
	password_confirm.attr('data-original-title', '');
  } else {
	result.text( "✘" );
	result.css({"margin-top" : "-32px", "margin-right" : "5px", "color" : "red", "float" : "right"});
	both.css("border-color", "rgb(220, 53, 69)");
	password_confirm.attr({'data-original-title': 'Passwords diferentes', 'data-placement': 'right'})
          .tooltip('show');
  }
}


function isEmail(email) {
  var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
  return regex.test(email);
}	

function validatemail() {
	result = $(".result_mail");	
	if (isEmail($("#Email").val())){
        result.text( "✔" );
		result.css({"margin-top" : "-32px", "margin-right" : "5px", "color" : "green", "float" : "right"});
		$("#Email").css("border-color", "green");
		$("#Email").attr('data-original-title', '');
	  } else {
		result.text( "✘" );
		result.css({"margin-top" : "-32px", "margin-right" : "5px", "color" : "red", "float" : "right"});
		$("#Email").css({"border-color": "rgb(220, 53, 69)"});
		$("#Email").attr({'data-original-title': 'E-mail inválido', 'data-placement': 'right'})
          .tooltip('show');
	  }
}

function validatename() {
	result = $(".result_name");	
	if ($("#Name").val().length > 2){
        result.text( "✔" );
		result.css({"margin-top" : "-32px", "margin-right" : "5px", "color" : "green", "float" : "right"});
		$("#Name").css("border-color", "green");
		$("#Name").attr('data-original-title', '');
	  } else {
		result.text( "✘" );
		result.css({"margin-top" : "-32px", "margin-right" : "5px", "color" : "red", "float" : "right"});
		$("#Name").css({"border-color": "rgb(220, 53, 69)"});
		$("#Name").attr({'data-original-title': 'O nome tem de ter pelo menos 3 caracteres', 'data-placement': 'right'})
          .tooltip('show');
	  }
}



$(function(){
		
	// PASSWORD
    $("#Password,#ConfirmPassword").focusin(function(){
        $("#Password").css("border-color", "");
        $("#ConfirmPassword").css("border-color", "");
		$(".result_pw").text( "" );
		$("#ConfirmPassword").attr('data-original-title', '');
    });
	
	$("#ConfirmPassword").focusout(function(){
        if ($("#Password").val().length > 0){
        validatepw();
		}
    });
	
	$("#Password").focusout(function(){
		if ($("#ConfirmPassword").val().length > 0){
        validatepw();
		}
    });
	
	//EMAIL
	$("#Email").focusin(function(){
        $("#Email").css("border-color", "");
		$("#Email").tooltip('dispose');
		$(".result_mail").text( "" );
		$("#Email").attr('data-original-title', '');
    });
	
	$("#Email").focusout(function(){
		if ($("#Email").val().length > 0){
        validatemail();
		}
	});
	
	//NAME
	$("#Name").focusin(function(){
        $("#Name").css("border-color", "");
		$("#Name").tooltip('dispose');
		$(".result_name").text( "" );
		$("#Name").attr('data-original-title', '');
    });
	
	$("#Name").focusout(function(){
		if ($("#Name").val().length > 0){
        validatename();
		}
	});
	

	
});
