var ELASTIC = process.env.ELASTIC || 'localhost';

var Seneca = require('seneca');

Seneca({ tag: 'search' })
    .test('print')
    .use('../search.js', {
        elastic: {
            host: ELASTIC
        }
    })
    .use('seneca-repl', { port: 10020 })
    .listen(9020)
    .client({ pin: 'role:suggest', port: 9060 })
    .ready(function () {
        this.add('role:search,cmd:search', function cmd_search_suggest(msg, reply) {
            this.prior(this.util.clean(msg), reply);
            this.act({ role: 'suggest', cmd: 'add', query: msg.query });
        });
    });