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
		$(".result_pw").text( "✘" );
		$(".result_pw").css("color", "red");
		$("#Password").css("border-color", "rgb(220, 53, 69)");
	}
}

function pwmessagebuilder(pw){
		message = '';
		
		if ((/^[a-zA-Z0-9]+$/.test(pw)) || !pw){
			message += '<font color="red">✘</font>'
		} else {
			message += '<font color="green">✔</font>'
		}
		message += ' É necessário pelo menos um caracter não alfanumérico.<br/>';
		
		
		if (!/\d/.test(pw)){
			message += '<font color="red">✘</font>'
		} else {
			message += '<font color="green">✔</font>'
		}
		message += ' É necessário pelo menos um número (0-9).<br/>';
		
		
		if (!/[a-z]/.test(pw)){
			message += '<font color="red">✘</font>'
		} else {
			message += '<font color="green">✔</font>'
		}
		message += ' É necessário pelo menos uma letra minúscula (a-z).<br/>';
		
		if (!/[A-Z]/.test(pw)){
			message += '<font color="red">✘</font>'
		} else {
			message += '<font color="green">✔</font>'
		}
		message += ' É necessário pelo menos uma letra maiúscula (A-Z).<br/>';
		
		if (!pw.length < 6){
			message += '<font color="red">✘</font>'
		} else {
			message += '<font color="green">✔</font>'
		}
		message += ' É necessário pelo menos 6 caracteres.';
		
		return message;
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
		result.css("color", "red");
		$("#Email").css({"border-color": "rgb(220, 53, 69)"});
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

function hoverdiv(e,divid){
	
	if(!($('#' + e.currentTarget.id).text() == '✔')){
		
    var left  = 5 + e.clientX  + "px";
    var top  = 5 + e.clientY  + "px";

    var div = document.getElementById(divid);

    div.style.left = left;
    div.style.top = top;
	
	console.log(e.currentTarget.id);
	switch(e.currentTarget.id) {
    case 'result_mail':
        message = 'É necessário um e-mail válido. Ex: mail@mail.com'
        break;
    case 'result_pw':
        message = pwmessagebuilder($("#Password").val());
        break;
	case 'helper_pw_c':
        message = 'pass c'
        break;
	case 'helper_name':
        message = 'name'
        break;
	case 'helper_bd':
        message = 'bd'
        break;
	case 'helper_pn':
        message = 'pn'
        break;
    default:
        message = 'Help not avaliable'
	} 
	
	$("#"+divid).html(message);
    $("#"+divid).toggle();
    return false;
	}
}

function hoverdiv_hide(e, divid){
	
	if(!($('#' + e.currentTarget.id).text() == '✔')){
	var div = document.getElementById(divid);
    $("#"+divid).toggle();
	}
}

$(function(){
		
	//HELPER CLASS
	$('.helper').mouseover(function(event){
		hoverdiv(event, 'divtoshow'); 
	});
	
	$('.helper').mouseout(function(event){
		hoverdiv_hide(event, 'divtoshow'); 
	});
		
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
	
	$("#Password").focusin(function(){
        $(".result_pw").html( '<font style="margin-right:5px; font-size: 18px; color: orange;"> <b>?</b> </font>' );
    });
	
	$("#Password").focusout(function(){
		$(".result_pw").html( '' );
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
		$(".result_mail").html( '<font style="margin-right:5px; font-size: 18px; color: orange;"> <b>?</b> </font>' );
    });
	
	$("#Email").focusout(function(){
		$(".result_mail").html( '' );
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
