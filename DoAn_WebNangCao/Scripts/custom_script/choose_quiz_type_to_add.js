const ID_CAU_HOI_1_DAP_AN = 1;
const ID_CAU_HOI_NHIEU_DAP_AN = 2;
const ID_CAU_HOI_SAP_XEP_THEO_THU_TU = 3;
const ID_CAU_HOI_DIEN_VAO_CHO_TRONG = 4;

$quiz_type = $('#quiz-type')
$quiz_type_id = $('#quiz-type-id')
$answer_container = $('.answer-container')
$one_correct_quiz = $('.one-correct-quiz')
$multi_correct_quiz = $('.multi-correct-quiz')
$sorted_quiz = $('.sorted-quiz')
$fill_in_blank_quiz = $('.fill-in-blank-quiz')
$quiz_title = $('.quiz-content')

$quiz_type.change(function (e) {
	e.preventDefault()

	// Clear quiz content
	$quiz_title.val('')

	if ($quiz_type.val() == 'one_correct') {
		Show_one_correct_quiz()
	} else if ($quiz_type.val() == 'multi_correct') {
		Show_multi_correct_quiz()
	} else if ($quiz_type.val() == 'sort') {
		Show_sorted_quiz()
	} else if ($quiz_type.val() == 'fill') {
		Show_fill_in_blank_quiz()
	}

})

const Show_one_correct_quiz = () => {
	// turn on one correct quiz container
	$one_correct_quiz.css('display', 'flex')

	// turn off others
	$multi_correct_quiz.css('display', 'none')
	$sorted_quiz.css('display', 'none')
	$fill_in_blank_quiz.css('display', 'none')

	// clear all answer row
	$answer_container.empty()

	// set value
	$quiz_type_id.val(ID_CAU_HOI_1_DAP_AN)
}

const Show_multi_correct_quiz = () => {
	// turn on one correct quiz container
	$multi_correct_quiz.css('display', 'flex')

	// turn off others
	$one_correct_quiz.css('display', 'none')
	$sorted_quiz.css('display', 'none')
	$fill_in_blank_quiz.css('display', 'none')

	// clear all answer row
	$answer_container.empty()

	// set value
	$quiz_type_id.val(ID_CAU_HOI_NHIEU_DAP_AN)
}

const Show_sorted_quiz = () => {
	// turn on one correct quiz container
	$sorted_quiz.css('display', 'flex')

	// turn off others
	$one_correct_quiz.css('display', 'none')
	$multi_correct_quiz.css('display', 'none')
	$fill_in_blank_quiz.css('display', 'none')

	// clear all answer row
	$answer_container.empty()

	// set value
	$quiz_type_id.val(ID_CAU_HOI_SAP_XEP_THEO_THU_TU)
}

const Show_fill_in_blank_quiz = () => {
	// turn on one correct quiz container
	$fill_in_blank_quiz.css('display', 'flex')

	// turn off others
	$one_correct_quiz.css('display', 'none')
	$multi_correct_quiz.css('display', 'none')
	$sorted_quiz.css('display', 'none')

	// clear all answer row
	$answer_container.empty()

	// set value
	$quiz_type_id.val(ID_CAU_HOI_DIEN_VAO_CHO_TRONG)

	// add blank-answer-container & wrong-answer-container
	var element = '<div class="blank-answer-container"></div>' +
		'<hr>' +
		'<div class="wrong-answer-container"></div>'
	$('.answer-container', '.fill-in-blank-quiz').append(element)
}