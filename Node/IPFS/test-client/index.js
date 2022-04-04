import { create } from "ipfs-http-client";
import express from "express";

const ipfsApiUrl = "https://ipfs.infura.io:5001";
const ipfsWebUrl = "http://ipfs.infura.io/ipfs/";

let ipfsClient;
let app;

async function initialize(ipfsUrl) {
  ipfsClient = create(ipfsUrl);
  console.log(ipfsClient);

  app = express();
  app.use(express.json());
  console.log(app);
}

async function addFile({ path, content }) {
  const file = { path: path, content: Buffer.from(content) };
  return await ipfsClient.add(file);
}

initialize(ipfsApiUrl);

app.get("/", (req, res) => {
  return res.send("Welcome to my IPFS client application!");
});

app.post("/upload", async (req, res) => {
  const data = req.body;
  console.log(data);

  const fileData = await addFile(data);
  console.log(fileData);

  return res.send(`https://ipfs.infura.io/ipfs/${fileData.cid}`);
});

app.listen(3000, () => {
  console.log("Server listening on port 3000");
});
