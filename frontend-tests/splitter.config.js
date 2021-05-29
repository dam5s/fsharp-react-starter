const path = require("path");

module.exports = {
    allFiles: true,
    entry: path.join(__dirname, "./frontend-tests.fsproj"),
    outDir: path.join(__dirname, "./dist"),
    babel: {
        plugins: ["@babel/plugin-transform-modules-commonjs"],
        sourceMaps: "inline"
    },
};
