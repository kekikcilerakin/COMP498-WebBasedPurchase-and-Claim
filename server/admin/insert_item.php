<?php

session_start();

require_once "../connection.php";

// Validate input data
if (!isset($_POST['item_name'], $_POST['item_description'], $_POST['item_image_url'], $_POST['item_price'])) {
    exit("Invalid input data.");
}

// Sanitize input data to prevent SQL injection
$itemName = mysqli_real_escape_string($conn, $_POST['item_name']);
$itemDescription = mysqli_real_escape_string($conn, $_POST['item_description']);
$itemImageUrl = mysqli_real_escape_string($conn, $_POST['item_image_url']);
$itemPrice = floatval($_POST['item_price']);

// Set session variables for image URL and price
$_SESSION['item_image_url'] = $itemImageUrl;
$_SESSION['item_price'] = $itemPrice;


// Prepare SQL statement
$sql = "INSERT INTO items (name, description, image_url, price) VALUES ('$itemName', '$itemDescription', '$itemImageUrl', '$itemPrice')";

// Execute SQL statement
if (mysqli_query($conn, $sql)) {
    echo "Records added successfully.";
} else {
    echo "ERROR: Could not able to execute $sql. " . mysqli_error($conn);
}

// Redirect to index.php
header("Location: ../index.php");
exit();
