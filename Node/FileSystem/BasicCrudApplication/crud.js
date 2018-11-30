'use strict';

// import the file-system and path modules
const fs = require('fs');
const path = require('path');

const {
    promisify
} = require("util");
const readFile = promisify(fs.readFile);

// create the CRUD object and its base directory
const crud = {
    baseDir: path.join(__dirname, './database')
};

// creation function
crud.create = (file, data) => {
    fs.open(`${crud.baseDir}/${file}.json`, 'wx',
        (err, identifier) => {
            if (!err && identifier) {
                let jsonArray = [];

                jsonArray.push(data);

                let stringData = JSON.stringify(jsonArray, null, 3);

                fs.writeFile(identifier, stringData, (err) => {
                    if (!err) {
                        fs.close(identifier, (err) => {
                            if (!err) {
                                console.log('Creation success!');
                            } else {
                                console.error(err);
                            }
                        });
                    } else {
                        console.error(err);
                    }
                });
            } else {
                console.error(err);
            }
        });
};

// run the creation function
crud.create('cars', {
    'name': 'innoson',
    'price': '$4000'
});

// read function
crud.read = (file) => {
    fs.readFile(`${crud.baseDir}/${file}.json`, 'utf8',
        (err, data) => {
            if (!err) {
                console.log(data);
            } else {
                console.error(err);
            }
        });
};

// run the read function
crud.read('cars');

// update function
crud.update = (file, data) => {
    //readFile returns promises
    readFile(`${crud.baseDir}/${file}.json`, 'utf8')
        .then(newStream => {
            //Change the string to a JS object
            let newData = JSON.parse(newStream);

            // Push our update to the array
            newData.push(data);

            // return our data as a string 
            return JSON.stringify(newData, null, 3);
        })
        .then(finalData => {
            //replace the content in the file, with the updated data.
            fs.truncate(`${crud.baseDir}/${file}.json`,
                (err) => {
                    if (!err) {
                        fs.writeFile(`${crud.baseDir}/${file}.json`, finalData, (err) => {
                            if (err) return err;
                        });
                    } else return err;
                });
        })
        .catch(err => console.log(err))
};

// run the update function
crud.update('cars', {
    name: 'Tesla',
    price: '$10'
});

// deletion function
crud.delete = (file) => {
    fs.unlink(`${crud.baseDir}/${file}.json`, (err) => {
        if (!err) {
            console.log('Deletion successful!');
        } else {
            console.error(err);
        }
    });
};

// run the delete function
crud.delete('cars');