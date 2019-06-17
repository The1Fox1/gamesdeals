const proxy = require("http-proxy-middleware");

module.exports = function(app) {
  app.use(proxy("/api", { target: "http://localhost:62359/", secure: false }));
  app.use(
    proxy("/ITAD", { target: "https://api.isthereanydeal.com/", secure: false })
  );
};
