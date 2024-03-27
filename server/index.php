<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>COMP 498 - Purchase Items</title>
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Cinzel:wght@400..900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="styles.css">
</head>

<body>
    <?php
    session_start();
    require_once "connection.php";

    //logout start
    if ($_SERVER["REQUEST_METHOD"] == "POST") {
        $_SESSION["id"] = null;
        $_SESSION["username"] = null;
        $_SESSION["loggedin"] = false;
        header("location: index.php");
    }
    //logout end

    // Check if user is logged in
    if (isset($_SESSION["loggedin"]) && $_SESSION["loggedin"] === true) {
        ?>
        <div></div>
        <?php
    } else {
        readfile('a.html');
    }
    ?>
</body>
</html>