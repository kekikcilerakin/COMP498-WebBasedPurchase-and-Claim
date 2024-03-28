<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>COMP 498 - Purchase Items</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <link rel="stylesheet" href="styles.css">
</head>

<body>
    <?php

    session_start();

    require_once "connection.php";

    //logout start
    if ($_SERVER["REQUEST_METHOD"] == "POST") {
        $_SESSION = array();

        session_destroy();

        header("Location: index.php");
        exit;
    }
    //logout end

    // Check if user is logged in
    if (!isset($_SESSION["loggedin"]) || $_SESSION["loggedin"] !== true) {
        header("Location: auth/login.php");
        exit;
    }
    ?>


<form action="admin/insert_item.php" method="post">
        <label for="item_name">Item Name:</label><br>
        <input type="text" id="item_name" name="item_name" required><br><br>

        <label for="item_description">Description:</label><br>
        <textarea id="item_description" name="item_description" rows="4" cols="50" required></textarea><br><br>

        <label for="item_image_url">Image URL:</label><br>
        <input type="text" id="item_image_url" name="item_image_url" required><br><br>

        <label for="item_price">Price:</label><br>
        <input type="number" id="item_price" name="item_price" min="0" step="0.01" required><br><br>

        <input type="submit" value="Add Item">
    </form>


</body>
</html>

