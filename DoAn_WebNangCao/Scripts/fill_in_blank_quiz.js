var $list_answers = $('#list-answers')
var $selected_region = $('.selected-region')

$('li', '.draggable').draggable({
    revert: 'invalid',
    cursor: 'move',
    start: function (event, ui) {
        $(this).css({ 'background-color': '#4e73df', 'color': 'white' })
    },
    stop: function (event, ui) {
        $(this).css({ 'background-color': '', 'color': '#4e73df' })
    }
})

$selected_region.droppable({
    accept: '.draggable > li',
    classes: {
        "ui-droppable-active": "ui-state-highlight"
    },
    tolerance: 'pointer',
    drop: function (event, ui) {
        SelectItem(ui.draggable, $(this))
    }
})

$list_answers.droppable({
    accept: '.selected-answer-list > li',
    classes: {
        "ui-droppable-active": "custom-state-active"
    },
    tolerance: 'pointer',
    drop: function (event, ui) {
        var $selected_container = ui.draggable.parents('.selected-region')
        RecycleItem(ui.draggable, $selected_container)
    }
})

SelectItem = ($item, $item_container) => {
    Reset_Answer_Id_Of_Old_Region($item)
    $item.fadeOut(() => {
        var $selected_list = $("ul", $item_container);
        // If selected region contains old answer and old answer's id != new answer'id
        // then remove old answer from selected region
        if ($('li', $selected_list).size() > 0) {
            var old_selected_answer = $('li', $selected_list).first()
            var old_selected_answer_id = old_selected_answer.attr('id')
            var new_selected_answer_id = $item.attr('id')
            if (old_selected_answer_id != new_selected_answer_id) {
                RecycleItem(old_selected_answer, $item_container)
            }
        }
        $item.appendTo($selected_list)
        $item.css({
            'top': '0',
            'left': '0',
        })
        $item.fadeIn(() => {
            $item
                .animate({ width: "100%" })
                .animate({ height: '100%' })
        })
        var answer_id = $item.attr('id')
        Set_Answer_Id($item_container, answer_id)
    })
}

RecycleItem = ($item, $item_container) => {
    $item.fadeOut(() => {
        $item.appendTo($list_answers)
        $item.css({
            'top': '0',
            'left': '0',
            'display': 'block',
        })
        $item.fadeIn(() => {
            $item
                .animate({ width: '250px' })
                .animate({ height: '50px' })
        })
    })
    Set_Answer_Id($item_container, -1)
}

Set_Answer_Id = ($item_container, answer_id) => {
    $('input', $item_container).val(answer_id)
}

Reset_Answer_Id_Of_Old_Region = $item => {
    // Check whether answer was selected
    var $selected_container = $item.parents('.selected-region')[0]
    if ($selected_container != undefined) {
        Set_Answer_Id($selected_container, -1)
    }
}