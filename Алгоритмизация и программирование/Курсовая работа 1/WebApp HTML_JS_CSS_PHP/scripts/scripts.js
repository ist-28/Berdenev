$(document).ready(function () {
    // Глобальные переменные
    let currentSearchQuery = '';

    // Вызов функций
    dataTable();
    loadAndPopulateFilters();

    // Вывод таблицы
    function dataTable() {
        $.ajax({
            type: 'GET',
            url: 'http://localhost/API/data',
            dataType: 'json',
            success: function (data) {
                updateTable(data)
            },
            error: function (error) {
                console.error('Error fetching data:', error)
            }
        })
    }

    // Обновление фильтров
    function loadAndPopulateFilters() {
        $.ajax({
            type: 'GET',
            url: 'http://localhost/API/filter', // <<< ИЗМЕНИТЕ ЭТОТ URL
            dataType: 'json',
            success: function (response) {
                if (response.state === 'success' && response.filters) {
                    populateDropdown('categories-dropdown', response.filters.categories, 'category', 'Категории не найдены');
                    populateDropdown('manufacturers-dropdown', response.filters.manufacturers, 'manufacturer', 'Производители не найдены');
                    populateDropdown('countries-dropdown', response.filters.countries, 'country', 'Страны не найдены');
                } else {
                    console.error('Ошибка загрузки фильтров:', response.message || 'Неизвестная ошибка');
                    alert('Не удалось загрузить данные для фильтров: ' + (response.message || ''));
                    populateDropdown('categories-dropdown', [], 'category', 'Ошибка загрузки категорий');
                    populateDropdown('manufacturers-dropdown', [], 'manufacturer', 'Ошибка загрузки производителей');
                    populateDropdown('countries-dropdown', [], 'country', 'Ошибка загрузки стран');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("AJAX Error loading filters: " + textStatus, errorThrown);
                alert('Сетевая ошибка при загрузке фильтров. Статус: ' + jqXHR.status);
                populateDropdown('categories-dropdown', [], 'category', 'Ошибка сети');
                populateDropdown('manufacturers-dropdown', [], 'manufacturer', 'Ошибка сети');
                populateDropdown('countries-dropdown', [], 'country', 'Ошибка сети');
            }
        });
    }

    // Функция для заполнения дропдаунов
    function populateDropdown(dropdownId, items, dataAttributePrefix, noItemsText) {
        const $dropdown = $(`#${dropdownId}`);
        $dropdown.empty();

        if (items && items.length > 0) {
            items.forEach(function (item) {
                $dropdown.append(
                    `<li><a class="dropdown-item" role="button" data-${dataAttributePrefix}-id="${item.id}">${item.name}</a></li>`
                );
            });
        } else {
            $dropdown.append(`<li><span class="dropdown-item-text">${noItemsText}</span></li>`);
        }
    }

    // Количество лекарств
    $('table tbody').on('click', '.minus', function () {
        let $countSpan = $(this).siblings('.count');
        let currentCount = parseInt($countSpan.text().split('/')[0]);
        let maxCount = parseInt($countSpan.attr('data-max'));

        if (currentCount > 0) {
            $countSpan.text((currentCount - 1) + '/' + maxCount);
        }
    });

    $('table tbody').on('click', '.plus', function () {
        let $countSpan = $(this).siblings('.count');
        let currentCount = parseInt($countSpan.text().split('/')[0]);
        let maxCount = parseInt($countSpan.attr('data-max'));

        if (currentCount < maxCount) {
            $countSpan.text((currentCount + 1) + '/' + maxCount);
        }
    });

    // Добавление в корзину
    $(function () {
        $('#card').click(function (e) {
            const itemsToAdd = $('table tbody tr').map(function () {
                const $row = $(this)
                const $countEl = $row.find('.count')
                const count = parseInt(($countEl.text().match(/\d+/) || ['0'])[0], 10)

                if (count <= 0) return null

                return {
                    Id: $row.attr('data-id'),
                    Name: $row.find('td:nth-child(2)').text(),
                    Category: $row.find('td:nth-child(3)').text(),
                    Type: $row.find('td:nth-child(4)').text(),
                    Year: $row.find('td:nth-child(5)').text(),
                    Manufacturer: $row.find('td:nth-child(6)').text(),
                    Price: $row.find('td:nth-child(7)').text(),
                    Count: count,
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
                const existingItem = cartData.find(cartItem => cartItem.Id === item.Id)
                const availableMax = item.maxCount

                if (existingItem) {
                    // Товар уже есть в корзине
                    if (existingItem.Count + item.Count > availableMax) {
                        alert(`Превышен лимит для "${item.Name}"! Макс: ${availableMax}`)
                        limitExceeded = true
                    } else {
                        existingItem.Count += item.Count
                    }
                } else {
                    // Новый товар
                    if (item.Count > availableMax) {
                        alert(`Превышен лимит для "${item.Name}"! Макс: ${availableMax}`)
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

    // Сортировка таблицы
    $('thead th[role=button]').on('click', function () {
        const $this = $(this);
        const col = $this.attr('aria-label');

        if (!col) {
            console.error('Сортировка не возможна, не найден aria-label');
            return;
        }

        let newSortDirection;
        const isCurrentlyAsc = $this.hasClass('asc');
        const isCurrentlyDesc = $this.hasClass('desc');

        $('thead th[role=button]').not($this).removeClass('asc desc');

        if (isCurrentlyAsc) {
            newSortDirection = 'desc';
            $this.removeClass('asc').addClass('desc');
        } else if (isCurrentlyDesc) {
            newSortDirection = 'asc';
            $this.removeClass('desc').addClass('asc');
        } else {
            newSortDirection = 'asc';
            $this.addClass('asc');
        }

        console.log('Sorting by:', col, 'Direction:', newSortDirection);

        let apiUrl = 'http://localhost/API/sort/' + newSortDirection + '/' + col;

        if (currentSearchQuery && currentSearchQuery.trim() !== '') {
            apiUrl += '/' + encodeURIComponent(currentSearchQuery.trim());
            console.log('Sorting with search query:', currentSearchQuery);
        } else {
            console.log('Sorting without search query.');
        }

        $.ajax({
            type: 'GET',
            url: apiUrl,
            dataType: 'json',
            success: function (response) {
                if (response.state === 'success' && Array.isArray(response.data)) {
                    console.log('Data received after sort:', response);
                    if (response.data.length === 0 && response.message) {
                        updateTable(response.data, response.message);
                    } else {
                        updateTable(response.data);
                    }
                } else if (response && response.state === 'error') {
                    console.error('API Error (Sort):', response.message);
                    alert('Ошибка сортировки: ' + (response.message || 'Неизвестная ошибка'));
                    updateTable([], response.message || 'Ошибка сортировки');
                } else {
                    console.error('Неожиданный формат ответа от API (Sort):', response);
                    alert('Произошла неожиданная ошибка при сортировке.');
                    updateTable([], 'Произошла неожиданная ошибка при сортировке');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("AJAX Error (Sort): " + textStatus, errorThrown);
                alert('Ошибка сети при сортировке. Статус: ' + jqXHR.status);
                updateTable([], 'Ошибка сети при сортировке');
            }
        });
    });

    // Обновление таблицы
    function updateTable(data, messageIfEmpty = 'Нет данных для отображения') {
        const $tbody = $('table tbody');
        if (!$tbody.length) {
            console.error("Тег <tbody> не найден в таблице!");
            return;
        }
        $tbody.empty();

        if (!data || data.length === 0) {
            const colCount = $('table thead th').length;
            $tbody.append(`<tr><td colspan="${colCount}">${messageIfEmpty}</td></tr>`);
            return;
        }

        const rowsHtml = data.map((item, index) => {
            const id = item.id ?? '';
            const title = item.title ?? '';
            const category = item.category ?? '';
            const type = item.type ?? '';
            const year = item.year ?? '';
            const manufacturer = item.manufacturer ?? '';
            const price = item.price ?? '';
            const count = item.count ?? 0;
            return `
            <tr data-id="${id}">
                <th scope="row">${index + 1}</th>
                <td>${title}</td>
                <td>${category}</td>
                <td>${type}</td>
                <td>${year}</td>
                <td>${manufacturer}</td>
                <td>${price}</td>
                <td>
                    <div class="counter d-flex justify-content-between">
                        <button class="minus btn btn-outline-danger me-2">-</button>
                        <span class="count" data-max="${count}">0/${count}</span>
                        <button class="plus btn btn-outline-success ms-2">+</button>
                    </div>
                </td>
            </tr>
            `
        })

        const Content = rowsHtml.join('')

        $tbody.append(Content)
    }

    // Поиск
    $('#search-form').on('submit', function (e) {
        e.preventDefault()
        const query = $('#search').val()
        currentSearchQuery = query
        if (!query || query.trim() === '') {
            dataTable()
            return
        }
        $.ajax({
            type: 'GET',
            url: 'http://localhost/API/search/' + encodeURIComponent(query),
            dataType: 'json',
            success: function (response) {
                if (response.state === 'success' && Array.isArray(response.data)) {
                    $('#search').val('')
                    if (response.data.length === 0 && response.message) {
                        updateTable(response.data, response.message);
                    } else {
                        updateTable(response.data);
                    }
                } else if (response && response.state === 'error') {
                    console.error('API Error:', response.message);
                    alert('Ошибка поиска: ' + response.message);
                } else {
                    console.error('Неожиданный формат ответа от API:', response);
                    alert('Произошла неожиданная ошибка при поиске.');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("AJAX Error: " + textStatus, errorThrown);
                alert('Ошибка сети при поиске. Статус: ' + jqXHR.status);
            }
        })
    })
})  
