<?php

session_start();
require_once "connection.php";

// Check if user is logged in
if (!isset($_SESSION["loggedin"]) || $_SESSION["loggedin"] !== true) {
    header("Location: auth/login.php");
    exit;
}

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $username = $_SESSION["username"];
    $item_id = $_POST["item_id"];

    // Fetch item details
    $sql = "SELECT name, price FROM items WHERE id = ?";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("i", $item_id);
    $stmt->execute();
    $stmt->bind_result($item_name, $item_price);
    $stmt->fetch();
    $stmt->close();

    // Fetch user's current gold and stats
    $sql = "SELECT gold, damage, crit_chance, auto_click, gold_multiplier FROM users WHERE username = ?";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("s", $username);
    $stmt->execute();
    $stmt->bind_result($gold, $damage, $crit_chance, $auto_click, $gold_multiplier);
    $stmt->fetch();
    $stmt->close();

    // Check if user has enough gold
    if ($gold >= $item_price) {
        // Update user's stats
        $new_gold = $gold - $item_price;
        $new_damage = $damage;
        $new_crit_chance = $crit_chance;
        $new_auto_click = $auto_click;
        $new_gold_multiplier = $gold_multiplier;

        switch ($item_name) {
            case 'Increased Click Damage':
                $new_damage += 1;
                break;
            case 'Critical Hit Chance':
                $new_crit_chance += 1;
                break;
            case 'Auto Clicker':
                $new_auto_click += 1;
                break;
            case 'Gold Multiplier':
                $new_gold_multiplier += 1;
                break;
            default:
                //
                break;
        }

        $sql = "UPDATE users SET gold = ?, damage = ?, crit_chance = ?, auto_click = ?, gold_multiplier = ? WHERE username = ?";
        $stmt = $conn->prepare($sql);
        $stmt->bind_param("iiidis", $new_gold, $new_damage, $new_crit_chance, $new_auto_click, $new_gold_multiplier, $username);
        $stmt->execute();
        $stmt->close();

        // Update session data
        $_SESSION["gold"] = $new_gold;
        $_SESSION["damage"] = $new_damage;
        $_SESSION["crit_chance"] = $new_crit_chance;
        $_SESSION["auto_click"] = $new_auto_click;
        $_SESSION["gold_multiplier"] = $new_gold_multiplier;

        header("Location: index.php?success=1");
        exit;
    } else {
        header("Location: index.php?error=not_enough_gold");
        exit;
    }
} else {
    header("Location: index.php");
    exit;
}
