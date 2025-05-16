<?php

header('Access-Control-Allow-Origin: *'); // тут будет адрес сайта, сейчас работает на локалке
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type, Authorization');
header('Access-Control-Allow-Credentials: true');
header('Content-Type: application/json');

require('func.php');
require('connect.php');

$method = $_SERVER['REQUEST_METHOD'];

$q = $q = isset($_GET['q']) ? trim($_GET['q'], '/') : '';;
try {
  $params = !empty($q) ? explode('/', $q) : [];
  if (empty($params)) {
    throw new Exception('Параметры не переданы');
  }
  $command = $params[0];

  switch ($method) {
    case 'GET':
      if ($command == 'data') {
        getData($pdo);
      } elseif ($command == 'sort') {
        // ----------------------------------------------------------------
        if (!isset($params[1])) {
          throw new Exception('Тип запроса не передан');
        }
        $type = $params[1];
        if (!isset($params[2])) {
          throw new Exception('Колонка не передана');
        }
        $col = $params[2];
        if (!isset($params[3])) {
          getSort($pdo, $type, $col, $searchQueryParam = null);
        } else{
          $searchQueryParam = $params[3];
          getSort($pdo, $type, $col, $searchQueryParam);
        }
        // ----------------------------------------------------------------
      } elseif ($command == 'filter'){
        getFilter($pdo);
      } elseif ($command == 'search') {
        if (!isset($params[1])) {
          throw new Exception('Запрос не передан');
        }
        $query = $params[1];
        getSearch($pdo, $query);
      }
       else {
        $res = [
          'state' => 'error',
          'message' => 'Страница не найдена'
        ];
        http_response_code(404);
        echo json_encode($res);
      }
      break;
    case 'POST':
      break;
  }
} catch (Exception $e) {
  $res = [
    'state' => 'error',
    'message' => $e->getMessage()
  ];
  http_response_code(405);
  echo json_encode($res);
}
