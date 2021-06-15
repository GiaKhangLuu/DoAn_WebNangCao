$('input[type=checkbox]').change(function() {
    if ($(this).is(':checked')) {
        $(this).val(true)
    }
    else {
        $(this).val(false)
    }
})