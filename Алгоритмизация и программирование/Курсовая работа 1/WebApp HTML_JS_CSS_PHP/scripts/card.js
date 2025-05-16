$(document).ready(function () {
    let cart = JSON.parse(localStorage.getItem('cart'));
    console.log(cart);

    let table = $('.table'); 
    let tbody = $('<tbody class="table-group-divider">');
    table.append(tbody);
    console.log(table.html()); // проверить, действительно ли tbody добавляется к таблице
    $.each(cart, function (index, item) {
        let row = $('<tr data-id="' + item.Id + '">');
        row.append('<th scope="row">' + (index + 1) + '</th>');
        row.append('<td>' + item.Name + '</td>');
        row.append('<td>' + item.Category + '</td>');
        row.append('<td>' + item.Type + '</td>');
        row.append('<td>' + item.Year + '</td>');
        row.append('<td>' + item.Manufacturer + '</td>');
        row.append('<td>' + item.Price + '</td>');
        row.append('<td>' + item.Count + '</td>');
        row.append('<td><a class="btn btn-outline-danger">&#x2715;</a></a></td>');
        tbody.append(row);
    })

    $('.clear-card').click(function () {
        localStorage.removeItem('cart');
        location.reload();
    })
})