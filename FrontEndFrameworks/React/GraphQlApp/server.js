var webpack = require('webpack');
var WebpackDevServer = require('webpack-dev-server');
var express = require('express');
var graphqlHTTP = require('express-graphql');
var graphql = require('graphql');

var GraphQLSchema = graphql.GraphQLSchema;
var GraphQLObjectType = graphql.GraphQLObjectType;
var GraphQLString = graphql.GraphQLString;
var GraphQLInt = graphql.GraphQLInt;

// data import
var goldbergs = require('./data.js');

function getGoldberg(id) {
  return goldbergs[id]
}

/* 
GraphQL model type, mirror image of goldberg data, creates
new instance of GraphQLObjectType, 'type' represents GraphQL
data type, and description self-documents
*/
var goldbergType = new GraphQLObjectType({
  name: 'Goldberg',
  description: "Member of The Goldbergs",
  fields: {
    character: {
      type: GraphQLString,
      description: "Name of the character",
    },
    actor: {
      type: GraphQLString,
      description: "Actor playing the character",
    },
    role: {
      type: GraphQLString,
      description: "Family role"
    },
    traits: {
      type: GraphQLString,
      description: "Traits this Goldberg is known for"
    },
    id: {
      type: GraphQLInt,
      description: "ID of this Goldberg",
    }
  }
});

/*
GraphQL query type specifies how we'll query the data, args
accepts id as an argument to getGoldberg(id) function
*/
var queryType = new GraphQLObjectType({
  name: 'query',
  description: "Goldberg query",
  fields: {
    goldberg: {
      type: goldbergType,
      args: {
        id: {
          type: GraphQLInt
        }
      },
      resolve: function(_, args){
        return getGoldberg(args.id)
      }
    }
  }
});

/*
GraphQL Schema type
*/
var schema = new GraphQLSchema({
  query: queryType
});

// serving the schema
var graphQLServer = express();
graphQLServer.use('/', graphqlHTTP({ schema: schema, graphiql: true }));
graphQLServer.listen(8080);
console.log("The GraphQL Server is running.")

// webpack will take index.js and write an es6 transpiled version to
// /static/bundle.js
var compiler = webpack({
  entry: "./index.js",
    output: {
        path: __dirname,
        filename: "bundle.js",
        publicPath: "/static/"
    },
    module: {
        loaders: [
            { test: /\.js$/, exclude: /node_modules/, loader: "babel-loader"
          }
        ]
    }
});

// serve the bundled project
var app = new WebpackDevServer(compiler, {
  contentBase: '/public/',
  proxy: {'/graphql': 'http://localhost:8080'},
  publicPath: '/static/',
  stats: {colors: true}
});

// Serve static resources
app.use('/', express.static('static'));
app.listen(3000);
console.log("The App Server is running.")