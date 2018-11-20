function validatepw() {
  var password = $("#Password");
  var password_confirm = $("#ConfirmPassword");
  var both = $("#Password,#ConfirmPassword")
  result = $(".result_pw_c");
	
  if (password.val() == password_confirm.val()) {
    result.text( "✔" );
	result.css({"margin-top" : "-32px", "margin-right" : "5px", "color" : "green", "float" : "right" });
	password_confirm.css("border-color", "green");
	password_confirm.attr('data-original-title', '');
  } else {
	result.text( "✘" );
	result.css({"margin-top" : "-32px", "margin-right" : "5px", "color" : "red", "float" : "right"});
	password_confirm.css("border-color", "rgb(220, 53, 69)");
	password_confirm.attr({'data-original-title': 'Passwords diferentes', 'data-placement': 'right'})
          .tooltip('show');
  }
}

function isvalidpw(pw){
	if(!/^[a-zA-Z0-9]+$/.test(pw) && /\d/.test(pw) && /[a-z]/.test(pw) && /[A-Z]/.test(pw) && pw.length > 5)
	{
		$(".result_pw").text( "✔" );
		$(".result_pw").css({"margin-top" : "-32px", "margin-right" : "5px", "color" : "green", "float" : "right" });
		$("#Password").css("border-color", "green");
		$("#Password").attr('data-original-title', '');
		return true;
	}
	else {
		if(/^[a-zA-Z0-9]+$/.test(pw)){
			message = 'É necessário pelo menos um caracter não alfanumérico';
		}else if (!/\d/.test(pw)){
			message = 'É necessário pelo menos um número (0-9).';
		}else if (!/[a-z]/.test(pw)){
			message = 'É necessário pelo menos uma letra minúscula (a-z).';
		}else if (!/[A-Z]/.test(pw)){
			message = 'É necessário pelo menos uma letra maiúscula (A-Z).';
		}else if (!pw.length < 6){
			message = 'É necessário pelo menos 6 caracteres.';
		}
		$(".result_pw").text( "✘" );
		$(".result_pw").css({"margin-top" : "-32px", "margin-right" : "5px", "color" : "red", "float" : "right" });
		$("#Password").css("border-color", "rgb(220, 53, 69)");
		$("#Password").attr({'data-original-title': message, 'data-placement': 'right'})
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
		$("#Name").attr({'data-original-title': 'O nome precisa de ter pelo menos 3 caracteres', 'data-placement': 'right'})
          .tooltip('show');
	  }
}



$(function(){
		
	// PASSWORD
    $("#ConfirmPassword").focusin(function(){
        $("#ConfirmPassword").css("border-color", "");
		$(".result_pw_c").text( "" );
		$("#ConfirmPassword").attr('data-original-title', '');
    });
	
	$("#ConfirmPassword").focusout(function(){
        if ($("#Password").val().length > 0 && isvalidpw($("#Password").val())){
			validatepw();
		} else {
			$("#ConfirmPassword").css("border-color", "");
			$(".result_pw_c").text( "" );
			$("#ConfirmPassword").attr('data-original-title', '');
		}
    });
	
	$("#Password").focusout(function(){
		if ($("#Password").val().length > 0){
			isvalidpw($("#Password").val());
		}
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
