import axios from "axios";
axios.defaults.withCredentials = true;
// Add a request interceptor
axios.interceptors.request.use(function(config) {
  config.withCredentials = true;
  return config;
});

/**
 * Will unpack the response body from reponse object
 * @param {*} response
 *
 */
const onGlobalSuccess = response => {
  /// Should not use if you need access to anything other than the data
  return response.data;
};

const onGlobalError = err => {
  return Promise.reject(err);
};

const API_HOST_PREFIX = "https://localhost:50001";

const API_ITAD_PREFIX = "https://api.isthereanydeal.com";

const API_ITAD_KEY = "d604f8dd0a126041cd957c04ac413711d652f580";

const API_ITAD_CLIENT_ID = "29cdae3036453a00";

const API_ITAD_CLIENT_SECRET = "32bbab1ee5338fba87d59598ec610da8872284c7";

export {
  onGlobalError,
  onGlobalSuccess,
  API_HOST_PREFIX,
  API_ITAD_PREFIX,
  API_ITAD_KEY,
  API_ITAD_CLIENT_ID,
  API_ITAD_CLIENT_SECRET
};
