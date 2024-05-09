<?php

require_once "connection.php";

if ($_SERVER["REQUEST_METHOD"] === "POST") {

    $username = trim($_POST["username"]);
    $newGold = $_POST["gold"];
    $newScore = $_POST["score"];

    $sql = "UPDATE users SET gold = ?, score = ? WHERE username = ?";

    if ($stmt = $conn->prepare($sql)) {
        $stmt->bind_param("iis", $newGold, $newScore, $username);

        if (!$stmt->execute()) {
            echo "Failed to update gold and score.";
        }

        $stmt->close();
    }

    $conn->close();

    echo "0";
}
