<?php

require_once "../connection.php";

$username = $password = "";
$username_err = $password_err = $login_err = "";

// Process form data when form is submitted
if ($_SERVER["REQUEST_METHOD"] == "POST") {

    // Check if username is empty
    $username = trim($_POST["username"]);
    if (empty($username)) {
        die("1: Please enter an username.");
    }

    // Check if password is empty
    $password = trim($_POST["password"]);
    if (empty($password)) {
        die("2: Please enter a password.");
    }

    // Validate credentials
    if (empty($username_err) && empty($password_err)) {

        $sql = "SELECT id, username, password, gold, level, damage, crit_chance, auto_click_damage, gold_multiplier FROM users WHERE BINARY username = ?";

        if ($stmt = $conn->prepare($sql)) {
            $stmt->bind_param("s", $username);

            if ($stmt->execute()) {
                $stmt->store_result();

                // Check if username exists
                if ($stmt->num_rows == 1) {
                    $stmt->bind_result($id, $username, $hashed_password, $gold_value, $level_value, $damage_value, $crit_chance_value, $auto_click_damage_value, $gold_multiplier);
                    if ($stmt->fetch()) {
                        if (password_verify($password, $hashed_password)) {
                            // Password is correct, start a new session
                            session_start();
                            $_SESSION["loggedin"] = true;
                            $_SESSION["id"] = $id;
                            $_SESSION["username"] = $username;

                            // Send data to Unity
                            echo "0\t" . $gold_value . "\t" . $level_value . "\t" . $damage_value . "\t" . $crit_chance_value . "\t" . $auto_click_damage_value . "\t" . $gold_multiplier;
                            exit;
                        } else {
                            // Password is not valid
                            die("3: Invalid password.");
                        }
                    }
                } else {
                    // Username doesn't exist
                    die("3: Invalid username or password.");
                }
            } else {
                echo "Something went wrong. Please try again later.";
            }

            $stmt->close();
        }
    }


}
