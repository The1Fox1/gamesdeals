import axios from "axios";
import * as helpr from "./serviceHelper";

const search = game => {
  game = game.replace(/\s/g, "%20");
  const config = {
    method: "GET",
    url: `${helpr.API_ITAD_PREFIX}/v01/search/search/?key=${
      helpr.API_ITAD_KEY
    }&q=${game}&limit=20`
  };

  return axios(config)
    .then(helpr.onGlobalSuccess)
    .catch(helpr.onGlobalError);
};

const getTop20 = () => {
  const config = {
    method: "GET",
    url: `${helpr.API_HOST_PREFIX}/api/testing/SteamTop`,
    withCredentials: true,
    crossdomain: true,
    headers: {
      "content-type": "application/json"
    }
  };

  return axios(config)
    .then(helpr.onGlobalSuccess)
    .catch(helpr.onGlobalError);
};

export { search, getTop20 };
