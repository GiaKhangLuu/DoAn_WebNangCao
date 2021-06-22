$quiz_content = $('input[name=fill-in-blank-quiz-content')

$('#btn-add-blank').click(function (event) {
	event.preventDefault();
	var quiz_content = $quiz_content.val()
	console.log(quiz_content)
	quiz_content = quiz_content.concat(" {'blank'} ")
	$quiz_content.val(quiz_content)
	$quiz_content.focus()
	Append_Blank_Answer()
})

$('#btn-add-fill-in-blank-quiz').click(function (e) {
	e.preventDefault()
	var order = ($('.wrong-answer-container').children().length) + 1
	var title = 'Đáp án ' + order
	var answer_row = '<div class="answer-row">' +
		'<span>' + title + '</span>' +
		'<input type="text" class="fill-in-blank-quiz-answer-content" placeholder="Nội dung đáp án"></div>'
	$('.wrong-answer-container', '.fill-in-blank-quiz').append(answer_row)
})

const Append_Blank_Answer = () => {
	var order = ($('.blank-answer-container').children().length) + 1
	var title = 'Ô trống ' + order
	var answer_row = '<div class="answer-row">' +
		'<span>' + title + '</span>' +
		'<input type="text" class="fill-in-blank-quiz-answer-content" placeholder="Nội dung đáp án"></div>'
	$('.blank-answer-container', '.fill-in-blank-quiz').append(answer_row)

}