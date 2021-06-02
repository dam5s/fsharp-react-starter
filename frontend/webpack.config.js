const path = require("path");

const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = (env, argv) => ({
    mode: argv.mode || "development",
    entry: {
        env: "./src/env.js",
        app: "./build/src/App.js",
    },
    output: {
        path: path.join(__dirname, "./dist"),
        filename: "[name].js",
    },
    devServer: {
        publicPath: "/",
        contentBase: "./dist",
        port: 3000,
        historyApiFallback: true
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: 'index.html',
        }),
        new MiniCssExtractPlugin(),
    ],
    module: {
        rules: [
            {
                test: /\.s[ac]ss$/i,
                use: [MiniCssExtractPlugin.loader, 'css-loader', 'sass-loader'],
            },
        ]
    }
});
