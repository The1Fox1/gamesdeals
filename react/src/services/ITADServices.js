import axios from "axios";
import * as helpr from "./serviceHelper";

const search = game => {
  game = game.replace(/\s/g, "%20");
  const config = {
    method: "GET",
    url: `${helpr.API_ITAD_PREFIX}/v01/search/search/?key=${
      helpr.API_ITAD_KEY
    }&q=${game}&limit=20`,
    withCredentials: true,
    crossdomain: true,
    headers: {
      "Access-Control-Allow-Origin": "http://localhost:3000",
      "content-type": "application/x-www-form-urlencoded"
    }
  };

  return axios(config)
    .then(helpr.onGlobalSuccess)
    .catch(helpr.onGlobalError);
};

export { search };
