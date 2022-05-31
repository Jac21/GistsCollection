import { create } from "ipfs-http-client";
import express from "express";
import Types from "./types.js";
import fs from "fs/promises";
import path from "path";

const ipfsApiUrl = "https://ipfs.io:5001";
const ipfsWebUrl = "http://ipfs.io/ipfs/";
const ipfsLocalDaemon = "/ip4/127.0.0.1/tcp/5001";

let ipfsClient;
let app;

async function initialize(ipfsUrl) {
  ipfsClient = create(ipfsUrl);
  console.log(ipfsClient);

  app = express();
  app.use(express.json());
  console.log(app);
}

async function addText({ ipfsPath, content }) {
  const file = { path: ipfsPath, content: Buffer.from(content) };

  return await ipfsClient.add(file);
}

async function addFile({ ipfsPath, content }) {
  const fileRead = await fs.readFile(content);

  console.log(fileRead);

  const file = { path: ipfsPath, content: fileRead };

  return await ipfsClient.add(file);
}

async function addFolder({ ipfsPath, content }) {
  let writtenFiles = [];

  var __dirname = path.resolve();

  const directoryPath = path.join(__dirname, content);

  fs.readdir(directoryPath, async function (err, files) {
    if (err) {
      return console.log("Unable to scan directory: " + err);
    }

    files.forEach(async function (fileInDirectory) {
      console.log(fileInDirectory);

      const fileRead = await fs.readFile(fileInDirectory);

      console.log(fileRead);

      const file = { path: ipfsPath, content: fileRead };

      writtenFiles.push(await ipfsClient.add(file));
    });
  });

  return writtenFiles;
}

// > ipfs daemon
initialize(ipfsLocalDaemon);

app.get("/", (req, res) => {
  return res.send("Welcome to my IPFS client application!");
});

app.post("/upload", async (req, res) => {
  const data = req.body;
  console.log(data);

  let requestType = new Types(data.type);

  let fileData;

  switch (requestType.name) {
    case "text":
      fileData = await addText(data);

      console.log(fileData);
      return res.send(`https://ipfs.io/ipfs/${fileData.cid}`);
    case "file":
      fileData = await addFile(data);

      console.log(fileData);
      return res.send(`https://ipfs.io/ipfs/${fileData.cid}`);
    case "folder":
      fileData = await addFolder(data);

      console.log(fileData);

      let ipfsUrls = [];

      fileData.forEach(function (entry) {
        ipfsUrls.push(`https://ipfs.io/ipfs/${entry.cid}`);
      });

      return res.send(ipfsUrls);
    default:
      console.error("Not a valid request type!");
      return res.send("Invalid request type!");
  }
});

app.listen(3000, () => {
  console.log("Server listening on port 3000");
});
