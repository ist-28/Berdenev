<?php
error_reporting(E_ALL);
function getData($pdo)
{
    $sql = 'SELECT
                m.id AS id,
                m.Title AS title,
                cat.category_title AS category,
                m.Type AS type,
                m.Year AS year,
                man.name AS manufacturer,
                m.Price AS price,
                m.Count AS count
            FROM
                medicine m
            JOIN
                category cat ON m.id_category = cat.id
            JOIN
                manufacturer man ON m.id_manufacturer = man.id';
    $stmt = $pdo->prepare($sql);
    $stmt->execute();

    $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
    echo json_encode($data);
}
function getSort($pdo, $type, $col, $searchQuery)
{
    $columnMapping = [
        'Id' => 'm.id',
        'Title' => 'm.Title',
        'Category' => 'cat.category_title',
        'Type' => 'm.Type',
        'Year' => 'm.Year',
        'Manufacturer' => 'man.name',
        'Price' => 'm.Price',
        'Count' => 'm.Count'
    ];

    $allowedColumns = array_keys($columnMapping);
    $allowedMethod = ['ASC', 'DESC'];

    if (!in_array($col, $allowedColumns)) {
        $res = [
            'state' => 'error',
            'message' => 'Неверное имя колонки для сортировки: ' . htmlspecialchars($col)
        ];
        http_response_code(400);
        header('Content-Type: application/json; charset=utf-8');
        echo json_encode($res, JSON_UNESCAPED_UNICODE);
        die;
    }

    $type = strtoupper($type);
    if (!in_array($type, $allowedMethod)) {
        $res = [
            'state' => 'error',
            'message' => 'Неверный способ сортировки: ' . htmlspecialchars($type)
        ];
        http_response_code(400);
        header('Content-Type: application/json; charset=utf-8');
        echo json_encode($res, JSON_UNESCAPED_UNICODE);
        die;
    }

    $sqlColumnForOrderBy = $columnMapping[$col];
    $params = [];

    $sql = "SELECT
                m.id AS id,
                m.Title AS title,
                m.Type AS type,
                m.Year AS year,
                m.Price AS price,
                m.Count AS count,
                cat.category_title AS category,
                man.name AS manufacturer,
                cy.country_name AS country -- Добавил страну, как в поиске
            FROM
                medicine AS m
            JOIN
                category AS cat ON m.id_category = cat.id
            JOIN
                manufacturer AS man ON m.id_manufacturer = man.id
            JOIN
                country AS cy ON man.id_country = cy.id";

    $whereClauses = [];
    if ($searchQuery !== null && !empty(trim($searchQuery))) {
        $searchTerm = "%" . trim($searchQuery) . "%";
        $whereClauses[] = "(m.Title LIKE :searchTerm OR
                           cat.category_title LIKE :searchTerm OR
                           man.name LIKE :searchTerm OR
                           m.Type LIKE :searchTerm OR
                           cy.country_name LIKE :searchTerm)";
        $params[':searchTerm'] = $searchTerm;
    }


    if (!empty($whereClauses)) {
        $sql .= " WHERE " . implode(" AND ", $whereClauses); // Используем AND, если будет несколько типов фильтров
    }

    $sql .= " ORDER BY {$sqlColumnForOrderBy} {$type};";

    try {

        $stmt = $pdo->prepare($sql);
        $stmt->execute($params);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
        $res = [
            'state' => 'success',
            'data' => $data
        ];
        header('Content-Type: application/json; charset=utf-8');
        echo json_encode($res, JSON_UNESCAPED_UNICODE | JSON_PRETTY_PRINT);
    } catch (PDOException $e) {
        $res = [
            'state' => 'error',
            'message' => 'Ошибка базы данных при сортировке: ' . $e->getMessage()
        ];
        http_response_code(500);
        header('Content-Type: application/json; charset=utf-8');
        echo json_encode($res, JSON_UNESCAPED_UNICODE);
    }
}

function getFilter($pdo)
{
    $response = [
        'state' => 'success',
        'filters' => [
            'categories' => [],
            'manufacturers' => [],
            'countries' => []
        ]
    ];

    try {
        $stmt_categories = $pdo->query("SELECT id, category_title FROM category ORDER BY category_title ASC");
        while ($row = $stmt_categories->fetch(PDO::FETCH_ASSOC)) {
            $response['filters']['categories'][] = ['id' => $row['id'], 'name' => $row['category_title']];
        }

        $stmt_manufacturers = $pdo->query("SELECT id, name FROM manufacturer ORDER BY name ASC");
        while ($row = $stmt_manufacturers->fetch(PDO::FETCH_ASSOC)) {
            $response['filters']['manufacturers'][] = ['id' => $row['id'], 'name' => $row['name']];
        }

        $stmt_countries = $pdo->query("SELECT id, country_name FROM country ORDER BY country_name ASC");
        while ($row = $stmt_countries->fetch(PDO::FETCH_ASSOC)) {
            $response['filters']['countries'][] = ['id' => $row['id'], 'name' => $row['country_name']];
        }
    } catch (PDOException $e) {
        $response['state'] = 'error';
        $response['message'] = 'Ошибка получения данных для фильтров: ' . $e->getMessage();
        unset($response['filters']);
        http_response_code(500);
    }

    echo json_encode($response);
}

function getSearch($pdo, $query)
{
    $response = [
        'state' => 'success',
        'data' => []
    ];

    $searchTerm = "%" . trim($query) . "%";

    try {
        $sql = "SELECT
                    m.id AS id,
                    m.Title AS title,
                    m.Type AS type,
                    m.Year AS year,
                    m.Price AS price,
                    m.Count AS count,
                    cat.category_title AS category,
                    man.name AS manufacturer,
                    cy.country_name AS country
                FROM
                    medicine AS m
                JOIN
                    category AS cat ON m.id_category = cat.id
                JOIN
                    manufacturer AS man ON m.id_manufacturer = man.id
                JOIN
                    country AS cy ON man.id_country = cy.id
                WHERE
                    m.Title LIKE :searchTerm OR          
                    cat.category_title LIKE :searchTerm OR -- 
                    man.name LIKE :searchTerm OR           
                    m.Type LIKE :searchTerm OR             
                    cy.country_name LIKE :searchTerm     
                ORDER BY m.Title ASC";

        $stmt = $pdo->prepare($sql);
        $stmt->bindValue(':searchTerm', $searchTerm, PDO::PARAM_STR);
        $stmt->execute();
        $response['data'] = $stmt->fetchAll(PDO::FETCH_ASSOC);

        if (empty($response['data'])) {
            $response['message'] = 'По вашему запросу ничего не найдено.';
        }
    } catch (PDOException $e) {
        $response['state'] = 'error';
        $response['message'] = 'Ошибка базы данных при поиске: ' . $e->getMessage();
        http_response_code(500);
    }

    echo json_encode($response);
}
