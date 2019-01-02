// ISVALID REGEX


function isUnit(number) {
	var regex = /^-?\d*[.,]?\d*$/;
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



function validatename() {
	result = $(".result_name");	
	vname = $("#Name");
	
	if (vname.val().length > 1){
		textboxapprove(vname, result);
	  } else {
		textboxdisapprove(vname, result);
	  }
}

function hoverdiv(e){
	
	if(!($('#' + e.currentTarget.id).text() == '✔')){
		
	switch(e.currentTarget.id) {
	case 'result_name':
        message = ToolTip_name;
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

function setInputFilter(textbox, inputFilter) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function(event) {
      textbox.addEventListener(event, function() {
        if (inputFilter(this.value)) {
          this.oldValue = this.value;
          this.oldSelectionStart = this.selectionStart;
          this.oldSelectionEnd = this.selectionEnd;
        } else if (this.hasOwnProperty("oldValue")) {
          this.value = this.oldValue;
          this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
        }
      });
    });
  }
  

$(document).ready(function() {		

    // setInputFilter(document.getElementById("animal-height"), function(value) {
    //     return /^-?\d{0,3}[.,]?\d{0,2}$/.test(value); });

    // setInputFilter(document.getElementById("animal-weight"), function(value) {
    //     return /^-?\d{0,3}[.,]?\d{0,2}$/.test(value); });


    $(":input").inputmask();
	//DATE PICKER
	$(function () {
		
		$("#datepicker").datepicker({ 
			format: 'dd/mm/yyyy',
			startDate: '-30y',
			endDate: '1d',
			autoclose: true
		}).datepicker('setDate',  $( "#Input_Birthday" ).val());
		

		if($("#Name").val().length > 0){
			validatename();
		}

		});

	//HELPER CLASS
	$('.helper').mouseover(function(event){
		hoverdiv(event); 
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
	

	
	
});
