$('#btn-add-multi-correct-quiz').click(function (e) {
	e.preventDefault()
	var num_of_dap_ans = ($('.answer-container', '.multi-correct-quiz').children().length) + 1
	$('input[name=multi-correct-quiz-num-of-dap-an]').val(num_of_dap_ans)
	var checkbox_name = "multi-correct-quiz-checkbox-" + num_of_dap_ans
	var dapan_content_name = "multi-correct-quiz-noi-dung-" + num_of_dap_ans
	var answer_row = '<div class="answer-row">' +
		'<input type="checkbox" name=' + checkbox_name + '>' +
		'<input type="text" name=' + dapan_content_name + ' class="multi-correct-quiz-answer-content" placeholder="Nội dung đáp án"></div>'
	$('.answer-container', '.multi-correct-quiz').append(answer_row)
})


