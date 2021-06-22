$('#btn-add-one-correct-quiz').click(function (e) {
	e.preventDefault()
	var num_of_dap_ans = ($('.answer-container', '.one-correct-quiz').children().length) + 1
	Set_Num_Of_Dap_An(num_of_dap_ans)
	var dapan_content_name = "one-correct-quiz-noi-dung-" + num_of_dap_ans;
	var answer_row = '<div class="answer-row">' +
		'<input type="radio" name="one-correct-quiz-radio" value=' + num_of_dap_ans + '>' +
		'<input type="text" name=' + dapan_content_name + ' class="one-correct-quiz-answer-content" placeholder = "Nội dung đáp án" ></div > '
	$('.answer-container', '.one-correct-quiz').append(answer_row)
})

const Set_Num_Of_Dap_An = num_of_dap_ans => {
	$('input[name=one-correct-quiz-num-of-dap-an]').val(num_of_dap_ans)
}