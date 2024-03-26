const { MongoClient } = require("mongodb");
const express = require("express");
const cors = require("cors");

const app = express();
const port = 3000;

const uri = "mongodb://localhost:27017";
const client = new MongoClient(uri);

async function getAllItems() {
	try {
		await client.connect();
		const database = client.db("comp-498");
		const items = database.collection("items");

		// Find all items
		const allItems = await items.find({}).toArray();
		return allItems;
	} finally {
		await client.close();
	}
}

app.use(cors());

app.get("/items", async (req, res) => {
	try {
		const items = await getAllItems();
		res.json(items);
	} catch (error) {
		console.error("Error:", error);
		res.status(500).json({ error: "An error occurred" });
	}
});

app.listen(port, () => {
	console.log(`Server is running on http://localhost:${port}`);
});
