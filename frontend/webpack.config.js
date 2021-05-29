const path = require("path");

module.exports = (env, argv) => ({
    mode: argv.mode || "development",
    entry: "./dist/src/App.js",
    output: {
        path: path.join(__dirname, "./public"),
        filename: "bundle.js",
    },
    devServer: {
        publicPath: "/",
        contentBase: "./public",
        port: 3000,
    },
    module: {
    }
});
