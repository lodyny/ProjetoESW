


$(document).ready(function(){


	$('.dates-vis').hide();
	
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
	.on('changeDate', function (selected) {
		var maxDate = new Date(selected.date.valueOf());
		$('#datepicker_start').datepicker('setEndDate', maxDate);
	});
	$('.input-daterange input').each(function() {
    $(this).datepicker('clearDates');
});

$('#adopt-choice').change(function() {
  if ($('#adopt-choice').prop('selectedIndex') == 0){
	  $('.dates-vis').hide();
  } else {
	  $('.dates-vis').show();
  };
});
});

