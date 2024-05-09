
<?php
session_start();
require_once "connection.php";

// Logout start
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $_SESSION = array();
    session_destroy();
    header("Location: index.php");
    exit;
}
// Logout end

// Check if user is logged in
if (!isset($_SESSION["loggedin"]) || $_SESSION["loggedin"] !== true) {
    header("Location: auth/login.php");
    exit;
}

if(isset($_SESSION["username"])) {
    $username = $_SESSION["username"];

    $sql = "SELECT gold, score, is_admin FROM users WHERE username = ?";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("s", $username);
    $stmt->execute();
    $stmt->bind_result($gold_value, $score_value, $is_admin);
    $stmt->fetch();
    $stmt->close();

} else {
    echo "Username not found in session";
}

?>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>COMP 498 - Purchase Items</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <link rel="stylesheet" href="styles.css">
</head>

<body>
    <nav class="navbar">
    <div class="container-fluid">
        <a class="navbar-brand"><?php echo $username; ?> / Gold: <?php echo $gold_value; ?> / Score: <?php echo $score_value; ?></a>
        <form class="form-inline my-2 my-lg-0" action="" method="post">
        <input class="form-control mr-sm-2" type="submit" value="Logout">
        </form>
    </div>
    </nav>

<?php
$itemsQuery = "SELECT name, description, image_url, price FROM items";
$result = $conn->query($itemsQuery);
?>

<div class="item-container">
    <?php if ($result->num_rows > 0): ?>
        <?php foreach ($result as $item): ?>
            <div class="item">
                <img class="item-image" src="<?php echo $item['image_url']; ?>">
                <br>
                <h5><?php echo $item['name']; ?></h5>
                <h6>Price: <?php echo $item['price']; ?></h6>
            </div>
        <?php endforeach; ?>
    <?php endif; ?>
</div>



    <?php if ($is_admin) : ?>
    <div class="create-item-div">
        <h2>Create a New Item</h2>
        <hr>
        
        <form action="admin/insert_item.php" method="post">
        <div class="form-group">
            <label for="item_name">Name</label><br>
            <input class="form-control" ype="text" id="item_name" name="item_name" required>
        </div>

        <div class="form-group">
            <label for="item_description">Description</label><br>
            <textarea class="form-control"  id="item_description" name="item_description" rows="4" cols="50" required></textarea>
        </div>
        
        <div class="form-group">
            <label for="item_image_url">Image URL</label><br>
            <input class="form-control" type="text" id="item_image_url" name="item_image_url" required>
        </div>
        
        <div class="form-group">
            <label for="item_price">Price</label><br>
            <input class="form-control" type="number" id="item_price" name="item_price" min="0" step="0.01" required>
        </div>
        
        <div class="form-group">
            <input class="btn btn-primary" type="submit" value="Add Item">
        </div>
        </form>
        
    </div>
    <?php endif; ?>
    
</body>
</html>

