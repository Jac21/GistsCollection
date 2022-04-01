import { ImagePool } from "@squoosh/lib";
import { cpus } from "os";
import fs from "fs/promises";

async function processFiles() {
  // create new image pools
  const imagePool = new ImagePool(cpus().length);

  // ingest image
  const file = await fs.readFile("./tests/image.png");

  const image = imagePool.ingestImage(file);

  const jobs = [];

  // preprocess and encode image
  const encodeOptions = {
    oxipng: {
      quality: 10,
    },
  };

  const job = image.encode(encodeOptions).then(async () => {
    // write encoded images to the file system
    const newImagePath = "./tests/encoded/image."; //extension is added automatically

    for (const encodedImage of Object.values(image.encodedWith)) {
      fs.writeFile(
        newImagePath + (await encodedImage).extension,
        (await encodedImage).binary
      );
    }
  });

  jobs.push(job);

  // Wait for all jobs to finish
  await Promise.all(jobs);

  // close image pool
  await imagePool.close();
}

processFiles();
