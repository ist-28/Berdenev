<?php
$host = "localhost";
$user = "root";
$password = "";
$dbname = "apteka";

try {
    $pdo = new PDO("mysql:host=$host;dbname=$dbname", $user, $password);
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
} catch (PDOException $e) {
    $error = $e->getMessage();
    $errorCode = $e->getCode();
    $errorFile = $e->getFile();
    $errorLine = $e->getLine();

    echo "Ошибка подключения к БД: $error ($errorCode) в файле $errorFile на строке $errorLine";
} catch (Exception $e) {
    echo "Неизвестная ошибка: " . $e->getMessage();
}
