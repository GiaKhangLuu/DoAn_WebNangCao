$('#btn-add-sorted-quiz').click(function (e) {
	e.preventDefault()
	var order = ($('.answer-container', '.sorted-quiz').children().length) + 1
	$('input[name=sorted-quiz-num-of-dap-an]').val(order)
	var title = 'Đáp án ' + order
	var dapan_content_name = "sorted-quiz-noidung-" + order;
	var answer_row = '<div class="answer-row">' +
		'<span>' + title + '</span>' +
		'<input type="text" name=' + dapan_content_name + ' class="sorted-quiz-answer-content" placeholder="Nội dung đáp án"></div>'
	$('.answer-container', '.sorted-quiz').append(answer_row)
})