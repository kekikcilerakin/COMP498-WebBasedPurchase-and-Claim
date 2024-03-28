<?php

// Define database connection parameters
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "comp498";

function connectToDatabase($servername, $username, $password, $dbname)
{
    $conn = new mysqli($servername, $username, $password, $dbname);
    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    }
    return $conn;
}

$conn = connectToDatabase($servername, $username, $password, $dbname);
