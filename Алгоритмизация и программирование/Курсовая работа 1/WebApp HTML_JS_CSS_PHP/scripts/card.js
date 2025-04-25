$(document).ready(function () {
    let cart = JSON.parse(localStorage.getItem('cart'));
    console.log(cart);

    let table = $('.table'); 
    let tbody = $('<tbody class="table-group-divider">');
    table.append(tbody);
    console.log(table.html()); // проверить, действительно ли tbody добавляется к таблице
    $.each(cart, function (index, item) {
        let row = $('<tr data-id="' + item.id + '">');
        row.append('<th scope="row">' + (index + 1) + '</th>');
        row.append('<td>' + item.name + '</td>');
        row.append('<td>' + item.type + '</td>');
        row.append('<td>' + item.year + '</td>');
        row.append('<td>' + item.country + '</td>');
        row.append('<td>' + item.price + '</td>');
        row.append('<td>' + item.count + '</td>');
        row.append('<td><a class="btn btn-outline-danger">&#x2715;</a></a></td>');
        tbody.append(row);
    })

    $('.clear-card').click(function () {
        localStorage.removeItem('cart');
        location.reload();
    })
})