var Wreck = require('wreck');

module.exports = function search(options) {
    var seneca = this;

    options = seneca.util.deepextend({
        elastic: {
            host: 'localhost',
            post: 9200,
            base: 'tao'
        },
    }, options);

    seneca.add('role:search,cmd:insert', cmd_insert);
    seneca.add('role:search,cmd:search', cmd_search);

    function cmd_insert(msg, reply) {
        var seneca = this;

        var elastic = options.elastic;

        var url = 'http://' + elastic.host + ':' + elastic.port + '/' + elastic.base + '/mod/' + msg.data.name;

        console.log('INSERT URL: ' + url);

        Wreck.post(
            url,
            { json: true, payload: seneca.util.clean(msg.data) },
            function (err, res, payload) {
                console.log(err);

                reply(err, payload);
            }
        );
    };

    function cmd_search(msg, reply) {
        var seneca = this;

        var elastic = options.elastic;

        var url = 'http://' + elastic.host + ':' + elastic.port + '/' + elastic.base + '/_search?q=' + encodeURIComponent(msg.query);

        console.log('SEARCH URL: ' + url);

        Wreck.get(
            url,
            { json: true },
            function (err, res, payload) {
                console.log(err);

                if (err) {
                    reply(err, payload);
                }

                var qr = payload;
                var items = [];

                var hits = qr.hits && qr.hits.hits;

                if (hits) {
                    for (var i = 0; i < hits.length; i++) {
                        var hit = hits[i];
                        items.push(seneca.util.clean(hit._source));
                    }
                }

                setTimeout(function () {
                    reply({ items: items })
                }, 400);
            }
        );
    };
}