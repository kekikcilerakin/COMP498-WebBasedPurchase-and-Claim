<?php

require_once "connection.php";

if ($_SERVER["REQUEST_METHOD"] === "POST") {

    $username = trim($_POST["username"]);
    $newGold = $_POST["gold"];
    $newLevel = $_POST["level"];

    $sql = "UPDATE users SET gold = ?, level = ? WHERE username = ?";

    if ($stmt = $conn->prepare($sql)) {
        $stmt->bind_param("iis", $newGold, $newLevel, $username);

        if (!$stmt->execute()) {
            echo "Failed to update gold and level.";
        }

        $stmt->close();
    }

    $conn->close();

    echo "0";
}
