// ISVALID REGEX

function isvalidpw(pw){
	if(!/^[a-zA-Z0-9]+$/.test(pw) && /\d/.test(pw) && /[a-z]/.test(pw) && /[A-Z]/.test(pw) && pw.length > 5)
	{return true}
	return false;
}

function isEmail(email) {
  var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
  return regex.test(email);
}	

function isNumber(number) {
	var regex = /^([0-9]{9})$/;
	return regex.test(number);
}

// MARKS

function textboxapprove(textb_name, result) {
	result.text( "✔" );
	result.css({"margin-top" : "-32px", "margin-right" : "5px", "color" : "green", "float" : "right"});
	textb_name.css("border-color", "green");
	textb_name.attr('data-original-title', '');
}

function textboxdisapprove(textb_name, result) {
	result.text( "✘" );
	result.css("color", "red");
	textb_name.css({"border-color": "rgb(220, 53, 69)"});
}

function replacequestionmark(target) {
	target.html( '<font style="margin-right:5px; font-size: 18px; color: orange;"> <b>?</b> </font>' );
}


function validatepw() {
  password = $("#Password");
  password_confirm = $("#ConfirmPassword");
  result = $(".result_pw_c");
	
  if (password.val() == password_confirm.val()) {
		textboxapprove(password_confirm, result);
  } else {
		textboxdisapprove(password_confirm, result);
  }
}

function validpw(pw){
	password = $("#Password");
	result = $(".result_pw");
	
	if(isvalidpw(pw)){
		textboxapprove(password, result);
		return true;
	}
	else {
		textboxdisapprove(password, result);
	}
}

function pwmessagebuilder(pw){
		message = '';
		
		if ((/^[a-zA-Z0-9]+$/.test(pw)) || !pw){
			message += '<font color="red">✘</font>'
		} else {
			message += '<font color="green">✔</font>'
		}
		message += ToolTip_nonalpha + ' <br/>';
		
		
		if (!/\d/.test(pw)){
			message += '<font color="red">✘</font>'
		} else {
			message += '<font color="green">✔</font>'
		}
		message += ToolTip_numreq + ' <br/>';
		
		
		if (!/[a-z]/.test(pw)){
			message += '<font color="red">✘</font>'
		} else {
			message += '<font color="green">✔</font>'
		}
		message += ToolTip_lowreq + ' <br/>';
		
		if (!/[A-Z]/.test(pw)){
			message += '<font color="red">✘</font>'
		} else {
			message += '<font color="green">✔</font>'
		}
		message += ToolTip_upreq + ' <br/>';
		
		if (!pw.length < 6){
			message += '<font color="red">✘</font>'
		} else {
			message += '<font color="green">✔</font>'
		}
		message += ToolTip_pwlng;
		
		return message;
}

function validatemail() {
	result = $(".result_mail");	
	email = $("#Email");
	
	if (isEmail(email.val())){
		textboxapprove(email, result);
	  } else {
		textboxdisapprove(email, result);
	  }
}

function validatename() {
	result = $(".result_name");	
	vname = $("#Name");
	
	if (vname.val().length > 2){
		textboxapprove(vname, result);
	  } else {
		textboxdisapprove(vname, result);
	  }
}

function validatephone() {
	result = $(".result_pn");	
	phone = $("#Phone");
	
	if (isNumber(phone.val())){
		textboxapprove(phone, result);
	  } else {
		textboxdisapprove(phone, result);
	  }
}

function hoverdiv(e){
	
	if(!($('#' + e.currentTarget.id).text() == '✔')){
		
	switch(e.currentTarget.id) {
    case 'result_mail':
        message = ToolTip_mail;
        break;
    case 'result_pw':	
        message = pwmessagebuilder($("#Password").val());
        break;
	case 'result_pw_c':
        message = ToolTip_pw_c;
        break;
	case 'result_name':
        message = ToolTip_name;
        break;
	case 'result_bd':
        message = ToolTip_bday;
        break;
	case 'result_pn':
        message = ToolTip_phone;
        break;
    default:
        message = 'Help not avaliable'
	} 
	
	$('#' + e.currentTarget.id).attr({'data-original-title': message, 'data-placement': 'top', 'data-html' : 'true'})
          .tooltip('show');
	
    return false;
	}	else {
		$("#"+ e.currentTarget.id).attr('data-original-title', '');
		$("#"+ e.currentTarget.id).tooltip('dispose');
	}
}

$(document).ready(function() {		

	//DATE PICKER
	$(function () {
		
		$("#datepicker").datepicker({ 
			format: 'dd/mm/yyyy',
			startDate: '-90y',
			endDate: '-16y',
			autoclose: true
		}).datepicker('setDate',  $( "#Input_Birthday" ).val());
		
		if($("#Email").val().length > 0){
			validatemail();
		}
		if($("#Name").val().length > 0){
			validatename();
		}
		if($("#Phone").val().length > 0){
			validatephone();
		}
		});

	//HELPER CLASS
	$('.helper').mouseover(function(event){
		hoverdiv(event); 
	});
		
		
	// PASSWORD
    $("#ConfirmPassword").focusin(function(){
        $("#ConfirmPassword").css("border-color", "");
		replacequestionmark($(".result_pw_c"));
    });
	
	$("#ConfirmPassword").focusout(function(){
        if ($("#Password").val().length > 0 && validpw($("#Password").val())){
			validatepw();
		} else {
			$("#ConfirmPassword").css("border-color", "");
			replacequestionmark($(".result_pw_c"));
		}
    });
	
	$("#Password").focusout(function(){
		replacequestionmark($(".result_pw"));
		
		if ($("#Password").val().length > 0){
			validpw($("#Password").val());
		}
		if ($("#ConfirmPassword").val().length > 0){
			validatepw();
		}
    });
	
	//EMAIL
	$("#Email").focusin(function(){
        $("#Email").css("border-color", "");
    });
	
	$("#Email").focusout(function(){
		replacequestionmark($(".result_mail"));
		
		if ($("#Email").val().length > 0){
        validatemail();
		}
	});
	
	//NAME
	$("#Name").focusin(function(){
        $("#Name").css("border-color", "");
    });
	
	$("#Name").focusout(function(){
		replacequestionmark($(".result_name"));
		if ($("#Name").val().length > 0){
        validatename();
		}
	});
	
	//PHONE
	$("#Phone").focusin(function(){
        $("#Phone").css("border-color", "");
    });
	
	$("#Phone").focusout(function(){
		replacequestionmark($(".result_pn"));
		if ($("#Phone").val().length > 0){
        validatephone();
		}
	});
	
	
});
