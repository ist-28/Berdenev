// Добавление лекарств
$('#add-type').change(function () {
    let selectedValue = $(this).val()
    if (selectedValue === 'before' || selectedValue === 'after') {
        $('#medicine-select').css('display', 'block')
        $('#medicine').empty()
        for (let i = 0; i < 5; i++) {
            $('#medicine').append($('<option>', {
                value: i + 1,
                text: 'Лекарство ' + (i + 1)
            }))
        }
    } else {
        $('#medicine-select').css('display', 'none')
    }
})