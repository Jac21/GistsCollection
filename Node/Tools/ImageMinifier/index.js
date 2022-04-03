import { ImagePool } from "@squoosh/lib";
import { cpus } from "os";
import fs from "fs/promises";

async function processFiles(fileName, newImagePath) {
  // create new image pools
  const imagePool = new ImagePool(cpus().length);

  // ingest image
  const file = await fs.readFile(fileName);

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

processFiles("./tests/image.png", "./tests/encoded/image.");
