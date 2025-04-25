// Количество лекарств
$(document).ready(function () {
    $('.minus').click(function () {
        let $count = $(this).next('.count')
        let count = parseInt($count.text())
        let maxCount = parseInt($count.attr('data-max'))
        if (count > 0) {
            $count.text((count - 1) + '/' + maxCount)
        }
    })

    $('.plus').click(function () {
        let $count = $(this).prev('.count')
        let count = parseInt($count.text())
        let maxCount = parseInt($count.attr('data-max'))
        if (count < maxCount) {
            $count.text((count + 1) + '/' + maxCount + '')
        }
    })

    $(function () {
        $('#card').click(function (e) {
            const itemsToAdd = $('table tbody tr').map(function () {
                const $row = $(this)
                const $countEl = $row.find('.count')
                const count = parseInt(($countEl.text().match(/\d+/) || ['0'])[0], 10)

                if (count <= 0) return null

                return {
                    id: $row.attr('data-id'),
                    name: $row.find('td:nth-child(2)').text(),
                    type: $row.find('td:nth-child(3)').text(),
                    year: $row.find('td:nth-child(4)').text(),
                    country: $row.find('td:nth-child(5)').text(),
                    price: $row.find('td:nth-child(6)').text(),
                    count: count,
                    maxCount: parseInt($countEl.attr('data-max') || '0', 10)
                }
            }).get()

            if (!itemsToAdd.length) return
            let cartData
            try {
                cartData = JSON.parse(localStorage.getItem('cart') || '[]')
                if (!Array.isArray(cartData)) cartData = []
            } catch (error) {
                cartData = []
                console.error("Error parsing cart data:", error)
            }

            let limitExceeded = false
            itemsToAdd.forEach(item => {
                const existingItem = cartData.find(i => i.id === item.id)
                const availableMax = item.maxCount

                if (existingItem) {
                    // Товар уже есть в корзине
                    if (existingItem.count + item.count > availableMax) {
                        alert(`Превышен лимит для "${item.name}"! Макс: ${availableMax}`)
                        limitExceeded = true
                    } else {
                        existingItem.count += item.count
                    }
                } else {
                    // Новый товар
                    if (item.count > availableMax) {
                        alert(`Превышен лимит для "${item.name}"! Макс: ${availableMax}`)
                        limitExceeded = true
                    } else {
                        const { maxCount, ...itemToStore } = item
                        cartData.push(itemToStore)
                    }
                }
            })

            localStorage.setItem('cart', JSON.stringify(cartData))

            if (limitExceeded) {
                e.preventDefault()
            }
        })
    })
})

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