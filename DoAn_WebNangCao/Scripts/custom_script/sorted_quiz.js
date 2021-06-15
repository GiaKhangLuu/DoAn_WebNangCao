$('#sortedable, #sortable li').disableSelection()

$('#sortable').sortable({
    revert: true,
    cursor: 'move',
    opacity: 0.5
})

$('button').click(() => {
    var sortedIDs = $('#sortable').sortable('toArray')
    console.log(sortedIDs)
})

$('#sortable').on('sortstop', (event, ui) => {
    var sortedIDs = $('#sortable').sortable('toArray')
    $('#user_choice_order').val(sortedIDs)
})