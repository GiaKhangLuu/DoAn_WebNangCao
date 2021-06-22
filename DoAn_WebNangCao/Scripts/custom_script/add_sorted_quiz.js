$('#btn-add-sorted-quiz').click(function (e) {
	e.preventDefault()
	var order = ($('.answer-container', '.sorted-quiz').children().length) + 1
	var title = 'Đáp án ' + order
	var answer_row = '<div class="answer-row">' +
		'<span>' + title + '</span>' +
		'<input type="text" class="sorted-quiz-answer-content" placeholder="Nội dung đáp án"></div>'
	$('.answer-container', '.sorted-quiz').append(answer_row)
})