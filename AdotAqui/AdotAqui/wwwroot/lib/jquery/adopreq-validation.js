


$(document).ready(function(){

	$.fn.datepicker.dates['pt'] = {
		days: ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado"],
		daysShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb"],
		daysMin: ["Do", "Se", "Te", "Qu", "Qu", "Se", "Sa"],
		months: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
		monthsShort: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
		today: "Hoje",
		monthsTitle: "Meses",
		clear: "Limpar",
		format: "dd/mm/yyyy"
	};
	
	$.fn.datepicker.dates['en'] = {
    days: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
    daysShort: ["Sud", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
    daysMin: ["Sn", "Mo", "Tu", "We", "Th", "Fr", "Sa"],
    months: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
    monthsShort: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
    today: "Today",
    clear: "Clear",
    format: "mm/dd/yyyy",
    titleFormat: "MM yyyy", /* Leverages same syntax as 'format' */
    weekStart: 0
	};

	$('.dates-vis').hide();
	checkLanguage();
	
    $("#datepicker_start").datepicker({
        todayBtn:  1,
		format: 'dd/mm/yyyy',
		startDate: 'now',
        autoclose: true
    })
	.datepicker('setDate',  'now')
	.on('changeDate', function (selected) {
		var minDate = new Date(selected.date.valueOf());
		$('#datepicker_end').datepicker('setStartDate', minDate);
	});

    $("#datepicker_end").datepicker({
		format: 'dd/mm/yyyy',
		startDate: 'now',
		autoclose: true
	})
	.datepicker('setDate', '')
	.on('changeDate', function (selected) {
		var maxDate = new Date(selected.date.valueOf());
		$('#datepicker_start').datepicker('setEndDate', maxDate);
	});


$('#adopt-choice').change(function() {
  if ($('#adopt-choice').prop('selectedIndex') == 0){
	  $('.dates-vis').hide();
  } else {
	  $('.dates-vis').show();
  };
});



$('#culture-dropdown').on('select2:select', function (e) {
  checkLanguage();
});


function checkLanguage(){if ($('#culture-dropdown').select2().select2('data')[0].id  == "pt-PT"){
	$('#datepicker_start').datepicker.defaults.language = 'pt';
	console.log('here');
  } else {
		$('#datepicker_start').datepicker.defaults.language = 'en';
	console.log('here2');
  };}

});


