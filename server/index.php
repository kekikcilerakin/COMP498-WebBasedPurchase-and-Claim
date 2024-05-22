
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

    $sql = "SELECT gold, level, is_admin, damage, crit_chance, auto_click, gold_multiplier FROM users WHERE username = ?";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("s", $username);
    $stmt->execute();
    $stmt->bind_result($gold_value, $level_value, $is_admin, $damage, $crit_chance, $auto_click, $gold_multiplier);
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
        <a class="navbar-brand"><?php echo $username; ?> / Gold: <?php echo $gold_value; ?></a>
        <form class="form-inline my-2 my-lg-0" action="" method="post">
        <input class="form-control mr-sm-2" type="submit" value="Logout">
        </form>
    </div>
    </nav>

    <?php if (isset($_GET['success'])): ?>
        <div class="alert alert-success" role="alert">
            Purchase successful!
        </div>
    <?php elseif (isset($_GET['error'])): ?>
        <div class="alert alert-danger" role="alert">
            <?php if ($_GET['error'] == 'not_enough_gold'): ?>
                Not enough gold to purchase this item.
            <?php endif; ?>
        </div>
    <?php else: ?>
        <div class="alert" role="alert">
            .
        </div>
    <?php endif; ?>

<?php
$itemsQuery = "SELECT id, name, description, image_url, price FROM items";
$result = $conn->query($itemsQuery);
?>

    <div class="item-container">
        <?php if ($result->num_rows > 0): ?>
            <?php foreach ($result as $item): ?>
                <div class="item">
                    <!-- <img class="item-image" src="<?php echo $item['image_url']; ?>">
                    <br> -->
                    <h5><?php echo $item['name']; ?></h5>
                    <h6> Price: <a class="price"><?php echo $item['price']; ?> Gold </a></h6>
                    <!-- <div class="form-group">
                        <input type="submit" class="btn btn-primary" value="Buy">
                    </div> -->
                    <form action="buy_item.php" method="post">
                        <input type="hidden" name="item_id" value="<?php echo $item['id']; ?>">
                        <input type="submit" class="btn btn-primary" value="Buy">
                    </form>
                </div>
            <?php endforeach; ?>
        <?php endif; ?>
    </div>


    <div class="player-stats">
        <h1>Player Stats</h1>
        <h5>Increased Click Damage: <?php echo $damage; ?></h5>
        <h5>Critical Hit Chance: <?php echo $crit_chance; ?></h5>
        <h5>Auto Clicker: <?php echo $auto_click; ?></h5>
        <h5>Gold Multiplier: <?php echo $gold_multiplier; ?></h5>
    </div>

    <!-- <?php if ($is_admin) : ?>
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
            <input class="form-control" type="number" id="item_price" name="item_price" min="0" step="1" required>
        </div>
        
        <div class="form-group">
            <input class="btn btn-primary" type="submit" value="Add Item">
        </div>
        </form>
        
    </div>
    <?php endif; ?> -->
    
</body>
</html>

