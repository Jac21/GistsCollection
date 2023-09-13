var Seneca = require('seneca');
var app = require('../web.js');

var seneca = Seneca({ tag: 'web' });

seneca
    .client({ pin: 'role:search', port: 9020 })
    .client({ pin: 'role:info', port: 9030 })
    .ready(function () {
        app({ seneca: this })
    });

var mock = Seneca({ tag: 'mock' });

if (process.env.MOCK_SEARCH) {
    mock
        .listen(9020)
        .add('role:search', function (msg, reply) {
            reply({
                items: msg.query.split(/\s+/).map(function (term) {
                    return { name: term, version: '1.0.0', desc: term + '!' }
                })
            });
        });
}

if (process.env.MOCK_INFO) {
    mock
        .listen(9030)
        .add('role:info', function (msg, reply) {
            reply({ npm: { name: msg.name, version: '1.0.0' } });
        });
}
